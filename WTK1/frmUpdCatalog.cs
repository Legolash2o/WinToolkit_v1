using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;

namespace WinToolkit
{
    public partial class frmUpdCatalog : Form
    {
        private readonly WebClient Client = new WebClient();
        private readonly ListView LV = new ListView();
        bool Scanning, bBusy = false;
        private string StatusP = "";

        //EndCheckBox

        #region Crypto

        private const ulong FC_TAG = 0xFC010203040506CF;
        private const int BUFFER_SIZE = 128 * 1024;

        private static SymmetricAlgorithm CreateRijndael(string password, byte[] salt)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, salt, "SHA256", 1000);
            SymmetricAlgorithm sma = Rijndael.Create();
            sma.KeySize = 256;
            sma.Key = pdb.GetBytes(32);
            sma.Padding = PaddingMode.PKCS7;
            return sma;

        }


        private static string DecryptFileSHA1(string inFile)
        {
            string sContents = "";
            try
            {
                try
                {
                    using (FileStream fin = File.OpenRead(inFile))
                    {
                        int size = (int)fin.Length;
                        byte[] bytes = new byte[BUFFER_SIZE];
                        int read = -1;
                        int outValue = 0;
                        byte[] IV = new byte[16];
                        fin.Read(IV, 0, 16);
                        byte[] salt = new byte[16];
                        fin.Read(salt, 0, 16);
                        SymmetricAlgorithm sma = CreateRijndael("321kpk80020791", salt);
                        sma.IV = IV;
                        long lSize = -1;
                        HashAlgorithm hasher = SHA256.Create();
                        using (CryptoStream cin = new CryptoStream(fin, sma.CreateDecryptor(), CryptoStreamMode.Read),
                                     chash = new CryptoStream(Stream.Null, hasher, CryptoStreamMode.Write))
                        {
                            BinaryReader br = new BinaryReader(cin);
                            lSize = br.ReadInt64();
                            ulong tag = br.ReadUInt64();
                            if (FC_TAG != tag)
                            {
                                return "";
                            }
                            long numReads = lSize / BUFFER_SIZE;
                            long slack = (long)lSize % BUFFER_SIZE;
                            for (int i = 0; i < numReads; ++i)
                            {
                                read = cin.Read(bytes, 0, bytes.Length);
                                sContents += System.Text.Encoding.UTF8.GetString(bytes);
                                chash.Write(bytes, 0, read);
                                outValue += read;
                            }

                            if (slack > 0)
                            {
                                read = cin.Read(bytes, 0, (int)slack);
                                sContents += System.Text.Encoding.UTF8.GetString(bytes);
                                chash.Write(bytes, 0, read);
                                outValue += read;
                            }
                            chash.Flush();
                            chash.Close();

                            byte[] curHash = hasher.Hash;
                            byte[] oldHash = new byte[hasher.HashSize / 8];
                            read = cin.Read(oldHash, 0, oldHash.Length);
                        }
                    }
                }
                catch
                {
                }
            }
            catch
            {
            }
            return sContents;
        }
        #endregion

        public frmUpdCatalog()
        {

            InitializeComponent();
            cMain.FormIcon(this);
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            cMain.eLBL = lblStatus;
            cMain.eForm = this;
            FormClosing += frmSoLoR_FormClosing;
            Shown += frmSoLoR_Shown;
            FormClosed += frmSoLoR_FormClosed;
            Activated += cMain.FormActivated;
            Deactivate += cMain.FormActivated;
            MouseWheel += MouseScroll;
            BWDownload.RunWorkerCompleted += BWDownload_RunWorkerCompleted;
            CheckForIllegalCrossThreadCalls = false;
        }

        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                if (e.ProgressPercentage > pbProgress.Value && e.ProgressPercentage <= pbProgress.Maximum)
                {
                    pbProgress.Value = e.ProgressPercentage;
                    cMain.UpdateToolStripLabel(lblStatus, "[" + pbProgress.Value + "%] " + StatusP);
                }
            }
            catch (Exception)
            {
            }
        }



        private void MouseScroll(object sender, MouseEventArgs e)
        {
            lstNew.Select();
        }

        private void ScanOld()
        {
            try
            {
                lstDel.BeginUpdate();
                lstDel.Items.Clear();
                if (Directory.Exists(txtDownload.Text))
                {
                    string[] sFiles =
                        Directory.GetFiles(txtDownload.Text, "*.*", SearchOption.AllDirectories)
                            .Where(s => s.ContainsIgnoreCase("\\Old\\"))
                            .ToArray();
                    foreach (string sFile in sFiles)
                    {
                        try
                        {
                            ListViewItem LST = new ListViewItem();
                            LST.Text = Path.GetFileNameWithoutExtension(sFile);
                            LST.SubItems.Add(cMain.GetSize(sFile));
                            LST.SubItems.Add(Path.GetExtension(sFile).Substring(1).ToUpper());
                            LST.SubItems.Add(new FileInfo(sFile).LastWriteTime.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture));
                            LST.SubItems.Add(sFile.Substring(txtDownload.Text.Length));
                            LST.SubItems.Add(sFile);

                            LST.ImageIndex = 0;
                            if (LST.SubItems[2].Text.EqualsIgnoreCase("MSU"))
                            {
                                LST.ImageIndex = 1;
                            }
                            if (LST.SubItems[2].Text.EqualsIgnoreCase("CAB"))
                            {
                                LST.ImageIndex = 2;
                            }
                            if (LST.SubItems[2].Text.EqualsIgnoreCase("EXE"))
                            {
                                LST.ImageIndex = 3;
                            }

                            lstDel.Items.Add(LST);
                        }
                        catch (Exception Ex)
                        {
                            new SmallError("Error Scan Old File", Ex, sFile).Upload();
                        }
                    }

                    foreach (ColumnHeader CH in lstDel.Columns)
                    {
                        if (CH.Index != lstOld.Columns.Count - 1)
                        {
                            CH.Width = -2;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                new SmallError("Error Scanning Old [" + txtDownload.Text + "]", Ex).Upload();
            }
            lstDel.EndUpdate();
            tabOld.Text = "Old / Superseded [" + lstDel.Items.Count + " - All Categories]";
            Application.DoEvents();

        }

        private void frmSoLoR_Load(object sender, EventArgs e)
        {
            splitContainer1.Scale4K(_4KHelper.Panel.Pan1);
            scMain.Scale4K(_4KHelper.Panel.Pan2);
            cMain.SetToolTip(cmdRefresh, "Redownload all the update lists.", "Update Lists");
            cMain.SetToolTip(cmdBrowse, "Select where all the downloads get saved to.", "Download Location");

            try
            {
                ListViewEx.LVE = lstNew;
                splitContainer1.SplitterDistance = cboSU.Top + 35;

                if (!string.IsNullOrEmpty(cOptions.SolDownload))
                {
                    txtDownload.Text = cOptions.SolDownload;
                }

                LV.Groups.Clear();
                foreach (ListViewGroup LVG in lstNew.Groups)
                {
                    var NG = new ListViewGroup();
                    NG.Header = LVG.Header;
                    NG.HeaderAlignment = LVG.HeaderAlignment;
                    LV.Groups.Add(NG);
                }
                cMain.FreeRAM();

                CleanDir();
                cMain.UpdateToolStripLabel(lblStatus, "");
            }
            catch (Exception Ex)
            {
                new SmallError("Error UC Load", Ex).Upload();
            }

        }

        private void frmSoLoR_Shown(object sender, EventArgs e)
        {
            UpdateList();
            cboSU.Text = "Please select a category.";
            cMain.UpdateToolStripLabel(lblStatus, cboSU.Text);
            ScanOld();
        }

        private void frmSoLoR_FormClosing(object sender, FormClosingEventArgs e)
        {
            cMain.OnFormClosing(this);
            if (Scanning)
            {
                MessageBox.Show("You can't close this whilst Win Toolkit is scanning, please wait!", "Access Denied");
                e.Cancel = true;
            }

            if (BWDownload.IsBusy)
            {
                MessageBox.Show("You can't close this whilst Win Toolkit is downloading, click cancel and wait for Win Toolkit to finish!", "Access Denied");
                e.Cancel = true;
            }
        }

        private void frmSoLoR_FormClosed(object sender, FormClosedEventArgs e)
        {
            cOptions.SolDownload = txtDownload.Text;
            cMain.ReturnME();
        }

        private void cboSU_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSU.Text.EqualsIgnoreCase("Please select a category.") || string.IsNullOrEmpty(cboSU.Text)) { return; }
            Scanning = true;
            bBusy = true;
            cMain.UpdateToolStripLabel(lblStatus, "Please wait while Win Toolkit scans for updates...");
            columnHeader1.Text = "Name (Searching...)";
            mnuDownload.Visible = false;
            cboSU.Enabled = false;
            cmdBrowse.Enabled = false;
            lstNew.Enabled = false;
            lstNew.Items.Clear();
            lstOld.Items.Clear();
            LV.Items.Clear();
            Application.DoEvents();
            cMain.FreeRAM();
            string sXMLPath = cMain.Root + "UpdateLists\\";
            switch (cboSU.Text)
            {
                case "Office 2010 x64":
                    sXMLPath += "Office2010-x64.wh";
                    break;
                case "Office 2010 x86":
                    sXMLPath += "Office2010-x86.wh";
                    break;
                case "Office 2013 x64":
                    sXMLPath += "Office2013-x64.wh";
                    break;
                case "Office 2013 x86":
                    sXMLPath += "Office2013-x86.wh";
                    break;
                case "Windows 7 x64":
                    sXMLPath += "Windows7-x64.wh";
                    break;
                case "Windows 7 x86":
                    sXMLPath += "Windows7-x86.wh";
                    break;
                case "Windows 8.1 x64":
                    sXMLPath += "Windows8.1-x64.wh";
                    break;
                case "Windows 8.1 x86":
                    sXMLPath += "Windows8.1-x86.wh";
                    break;
            }
            lstNew.BeginUpdate();
            try
            {
                GetFiles(sXMLPath);
            }
            catch { }

            lstNew.EndUpdate();

            try
            {
                if (Directory.Exists(txtDownload.Text))
                {
                    if (lstNew.Items.Count > 0)
                    {
                        lstNew.BeginUpdate();
                        cMain.UpdateToolStripLabel(lblStatus, "Scanning Files...");
                        Application.DoEvents();
                        CleanDir();
                        if (chkScan.Checked) { ScanOld(); }
                        lstNew.EndUpdate();
                    }
                }
            }
            catch { }

            cMain.UpdateToolStripLabel(lblStatus, "");
            Application.DoEvents();
            cboSU.Enabled = true;
            cmdBrowse.Enabled = true;

            lstNew.Enabled = true;
            TopItem();

            foreach (ColumnHeader CH in lstNew.Columns) { if (CH.Index != lstNew.Columns.Count - 1) { CH.Width = -2; } }
            foreach (ColumnHeader CH in lstOld.Columns) { if (CH.Index != lstOld.Columns.Count - 1) { CH.Width = -2; } }

            tabNew.Text = "New [" + lstNew.Items.Count + "]";
            tabCurrent.Text = "Downloaded [" + lstOld.Items.Count + "]";

            if (lstNew.Items.Count == 0)
            {
                columnHeader1.Text = "No New Updates.";
                cMain.UpdateToolStripLabel(lblStatus, "No new updates.");
                if (lstOld.Items.Count > 0)
                {
                    tabMain.SelectedTab = tabCurrent;
                }
            }
            else
            {
                columnHeader1.Text = "Name";
                tabMain.SelectedTab = tabNew;
                foreach (ListViewGroup LVG in lstNew.Groups) { UpdateGroupHeader(LVG); }
            }

            UpdateButtons();

            Scanning = false;
            bBusy = false;
        }

        private void UpdateGroupHeader(ListViewGroup LVG)
        {
            try
            {
                string newHeader = LVG.Header;
                if (newHeader.ContainsIgnoreCase("::"))
                {
                    newHeader = newHeader.Substring(0, newHeader.IndexOf(':') - 1);
                }
                newHeader += " :: " + LVG.Items.Count;
                LVG.Header = newHeader;
            }
            catch (Exception Ex)
            {
                new SmallError("Error setting header", Ex, LVG.Header).Upload();
            }
        }

        private void GetFiles(string XMLPath)
        {
            string XMLInfo = DecryptFileSHA1(XMLPath);

            foreach (string ULine in System.Text.RegularExpressions.Regex.Split(XMLInfo, "<update extract="))
            {
                string KB = "";
                string date = "";
                string size = "";
                string Category = "";
                string Name = "";
                string Description = "";
                string URL = "";
                bool Extraction = false;
                foreach (string ULine2 in ULine.Split(Environment.NewLine.ToCharArray()))
                {
                    string Line = ULine2.Trim();
                    try
                    {
                        if (string.IsNullOrEmpty(Line)) { continue; }
                        if (Line.StartsWithIgnoreCase("\"1\">")) { Extraction = true; }

                        if (Line.Trim().StartsWithIgnoreCase("<kb>")) { KB = GetValue(Line, "<kb>"); }
                        if (Line.Trim().StartsWithIgnoreCase("<date>")) { date = GetValue(Line, "<date>"); }
                        if (Line.Trim().StartsWithIgnoreCase("<size>")) { size = GetValue(Line, "<size>"); }
                        if (Line.Trim().StartsWithIgnoreCase("<category>")) { Category = GetValue(Line, "<category>"); }
                        if (Line.Trim().StartsWithIgnoreCase("<name>")) { Name = GetValue(Line, "<name>"); }
                        if (Line.Trim().StartsWithIgnoreCase("<description>")) { Description = GetValue(Line, "<description>"); }
                        if (Line.Trim().StartsWithIgnoreCase("<url>")) { URL = GetValue(Line, "<url>"); }

                        if (!string.IsNullOrEmpty(URL) && lstOld.FindItemWithText(URL) != null) { continue; }
                        if (!string.IsNullOrEmpty(URL) && lstNew.FindItemWithText(URL) != null) { continue; }

                        if (!string.IsNullOrEmpty(KB) && !string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(size) && !string.IsNullOrEmpty(Category) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(URL))
                        {
                            var LST = new ListViewItem();
                            LST.Text = Path.GetFileNameWithoutExtension(Name);
                            while (LST.Text.ContainsIgnoreCase("~"))
                            {
                                LST.Text = LST.Text.Substring(0, LST.Text.Length - 1);
                            }

                            if (LST.Text.ContainsIgnoreCase("KB") && LST.Text.ContainsIgnoreCase("_"))
                            {
                                LST.Text = LST.Text.Substring(0, LST.Text.IndexOf('_') - 1);
                            }
                            LST.SubItems.Add(size);
                            LST.SubItems.Add(Path.GetExtension(Name).Substring(1).ToUpper());
                            LST.SubItems.Add(date);
                            LST.SubItems.Add(Category);
                            LST.SubItems.Add(URL);
                            LST.ToolTipText = Description;
                            LST.Checked = true;

                            LST.Group = lstNew.Groups[0];

                            if (Category.StartsWithIgnoreCase("Additional") || Category.StartsWithIgnoreCase("Extra"))
                            {
                                LST.Checked = false;
                                LST.Group = lstNew.Groups[1];
                            }

                            LST.ImageIndex = 0;
                            if (LST.SubItems[2].Text.EqualsIgnoreCase("MSU")) { LST.ImageIndex = 1; }
                            if (LST.SubItems[2].Text.EqualsIgnoreCase("CAB")) { LST.ImageIndex = 2; }
                            if (LST.SubItems[2].Text.EqualsIgnoreCase("EXE")) { LST.ImageIndex = 3; }
                            string FilePath = txtDownload.Text + Path.GetFileNameWithoutExtension(XMLPath) + "\\" + Category.ReplaceIgnoreCase("/", "\\") + "\\" + LST.Text + "." + LST.SubItems[2].Text.ToLower();

                            if (File.Exists(FilePath))
                            {
                                int debug = 0;
                                try
                                {
                                    DateTime DT = cMain.ParseDate(LST.SubItems[3].Text);
                                    debug++; //1
                                    if (new FileInfo(FilePath).CreationTime != DT)
                                    {
                                        debug++; //2
                                        File.SetCreationTime(FilePath, DT);
                                        debug++; //3
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    new SmallError("Error setting old update date", Ex, "Date: " + LST.SubItems[3].Text + "\r\nDebug: " + debug, LST).Upload();
                                }
                                if (lstOld.FindItemWithText(URL) == null)
                                {
                                    LST.Group = lstOld.Groups[5];
                                    if (LST.SubItems[4].Text.StartsWithIgnoreCase("General")) { LST.Group = lstOld.Groups[0]; }
                                    if (LST.SubItems[4].Text.StartsWithIgnoreCase("Hotfix")) { LST.Group = lstOld.Groups[1]; }
                                    if (LST.SubItems[4].Text.StartsWithIgnoreCase("Security")) { LST.Group = lstOld.Groups[2]; }
                                    if (LST.SubItems[4].Text.StartsWithIgnoreCase("Extra")) { LST.Group = lstOld.Groups[3]; }
                                    if (LST.SubItems[4].Text.StartsWithIgnoreCase("Additional")) { LST.Group = lstOld.Groups[4]; }
                                    LST.SubItems[5].Text = new FileInfo(FilePath).CreationTime.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                                    LST.SubItems.Add(URL);
                                    lstOld.Items.Add(LST);
                                }
                            }
                            else if (lstNew.FindItemWithText(URL) == null) { lstNew.Items.Add(LST); }

                        }
                    }
                    catch (Exception Ex)
                    {

                        new SmallError("Error Getting File", Ex, Line).Upload();
                    }
                }
            }

            foreach (ListViewGroup LVG in lstOld.Groups)
            {
                UpdateGroupHeader(LVG);
            }
        }

      

        private static string GetValue(string Input, string Value)
        {
            string newValue = Input.Substring(Value.Length);
            if (newValue.ToUpper().EndsWithIgnoreCase(Value.Insert(1, "/").ToUpper()))
            {
                return newValue.Substring(0, newValue.Length - (Value.Length + 1)).Trim();
            }
            return newValue.Trim();
        }

        private void ScanD()
        {
            Scanning = true;
            DisableMe();
            Application.DoEvents();
            foreach (ListViewItem LST in lstNew.Items)
            {
                if (LST.Group == lstNew.Groups[0]) { LST.Checked = true; } else { LST.Checked = false; }
            }

            string sDir = cboSU.Text.ReplaceIgnoreCase(" x", "-x");
            sDir = sDir.ReplaceIgnoreCase(" ", "");

            if (Directory.Exists(sDir + "\\Old"))
            {
                MessageBox.Show(sDir);
            }

            if (Directory.Exists(txtDownload.Text + sDir) && lstNew.Items.Count > 0)
            {
                string[] sFiles = Directory.GetFiles(txtDownload.Text + sDir, "*.*", SearchOption.AllDirectories);
                int i = 0;
                pbProgress.Visible = true;
                pbProgress.Value = 0;
                pbProgress.Maximum = sFiles.Length;
                foreach (string filePath in sFiles)
                {
                    if (cMain.GetSize(filePath, false).EqualsIgnoreCase("0") || cMain.GetSize(filePath, false).EqualsIgnoreCase("-1"))
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Deleting corrupt file [" + filePath + "]...");
                        Application.DoEvents();
                        Files.DeleteFile(filePath);
                    }
                    else
                    {
                        if (filePath.ContainsIgnoreCase("\\OLD\\")) { continue; }
                        if (filePath.ContainsIgnoreCase("\\USER_EXTRA_UPDATES\\")) { continue; }
                        if (filePath.ToUpper().EndsWithIgnoreCase(".ICO")) { continue; }
                        if (filePath.ToUpper().EndsWithIgnoreCase("DESKTOP.INI")) { continue; }

                        try
                        {
                            string fileName = Path.GetFileNameWithoutExtension(filePath);

                            cMain.UpdateToolStripLabel(lblStatus, "Scanning: " + fileName);
                            Application.DoEvents();
                            foreach (ListViewItem LST in lstOld.Items)
                            {
                                try
                                {
                                    if (String.Equals(LST.Text, fileName, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        var FI = new FileInfo(filePath);
                                        var convertedDate = cMain.ParseDate(LST.SubItems[5].Text);

                                        var fcDate = DateTime.Parse(FI.CreationTime.ToString(CultureInfo.InvariantCulture));
                                        fcDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(fcDate, "W. Europe Standard Time");
                                        var fmDate = DateTime.Parse(FI.LastWriteTime.ToString(CultureInfo.InvariantCulture));
                                        fmDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(fmDate, "W. Europe Standard Time");

                                        if (fmDate >= convertedDate || fcDate >= convertedDate)
                                        {
                                            LST.Checked = false;
                                            LST.Group = lstNew.Groups[2];
                                            LST.ToolTipText = GetPath(LST);
                                        }

                                    }
                                }
                                catch { }
                            }

                            if (lstOld.FindItemWithText(fileName, false, 0) == null)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Moving: " + fileName);
                                Application.DoEvents();

                                string Old = txtDownload.Text + sDir + "\\Old";

                                string EXT = filePath;
                                while (EXT.ContainsIgnoreCase("."))
                                    EXT = EXT.Substring(1);
                                if (!string.IsNullOrEmpty(EXT))
                                {
                                    EXT = "." + EXT;
                                }

                                if (!Directory.Exists(Old))
                                {
                                    cMain.CreateDirectory(Old);
                                }
                                if (!File.Exists(Old + "\\" + fileName + EXT))
                                {
                                    File.Move(filePath, Old + "\\" + fileName + EXT);
                                }
                            }

                        }
                        catch
                        {
                        }
                    }
                    i++;
                    pbProgress.Value++;
                    Application.DoEvents();
                }

                foreach (ListViewItem LST in lstNew.Items)
                {
                    try
                    {
                        if (LST.Checked == false && LST.SubItems[4].Text != "N/A")
                        {
                            string F = LST.Text + "." + LST.SubItems[2].Text.ToLower();

                            string mDir = txtDownload.Text + sDir;
                            string cDir = txtDownload.Text + sDir + "\\" + LST.SubItems[4].Text;

                            if (File.Exists(mDir + "\\" + F))
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Sorting: " + LST.Text);
                                Application.DoEvents();
                                cMain.CreateDirectory(cDir);
                                File.Move(mDir + "\\" + F, cDir + "\\" + F);
                            }
                        }
                    }
                    catch { }

                }

                cMain.FreeRAM();
            }
            Scanning = false;
            //DisableMe(true);
        }

        private void TopItem()
        {
            if (lstNew.Items.Count > 0)
            {
                lstNew.BeginUpdate();
                lstNew.ShowGroups = false;
                lstNew.EnsureVisible(0);
                lstNew.ShowGroups = true;
                lstNew.EndUpdate();
            }
        }
        private void CleanDir()
        {
            try
            {
                if (Directory.Exists(txtDownload.Text))
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Cleaning Directory...");
                    Application.DoEvents();
                    if (cMain.IsEmpty(txtDownload.Text))
                    {
                        Files.DeleteFolder(txtDownload.Text, false);
                    }
                    else
                    {
                        foreach (string D in Directory.GetDirectories(txtDownload.Text, "*", SearchOption.AllDirectories))
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Cleaning Directory [" + D + "]...");
                            Application.DoEvents();
                            if (cMain.IsEmpty(D)) { Files.DeleteFolder(D, false); }
                        }

                        cMain.UpdateToolStripLabel(lblStatus, "Cleaning Directory...");
                        Application.DoEvents();
                        if (Directory.Exists(txtDownload.Text + "Windows 7 x64")) { Directory.Move(txtDownload.Text + "Windows 7 x64", txtDownload.Text + "McRip Windows 7 x64"); }
                        if (Directory.Exists(txtDownload.Text + "Windows 7 x86")) { Directory.Move(txtDownload.Text + "Windows 7 x86", txtDownload.Text + "McRip Windows 7 x86"); }
                    }
                }
            }
            catch (Exception Ex) { }
        }

        private void mnuDownload_Click(object sender, EventArgs e)
        {
            if (mnuDownload.Text.EqualsIgnoreCase("Download"))
            {
                if (lstNew.CheckedItems.Count == 0)
                {
                    MessageBox.Show("You need to tick at least one item!", "Invalid Selection");
                    return;
                }

                mnuDownload.Enabled = false;
                bBusy = true;
                DisableMe();
                CleanDir();

                lblStatus.Visible = true;
                pbProgress.Visible = true;
                mnuDownload.Visible = true;
                mnuDownload.Enabled = true;
                mnuDownload.Text = "Cancel";
                mnuDownload.Image = mnuDownload.Image = Properties.Resources.Close;
                Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
                cMain.FreeRAM();

                BWDownload.RunWorkerAsync();

            }
            else
            {
                DialogResult DR = MessageBox.Show("Are you sure you wish to cancel the download?", "Abort?", MessageBoxButtons.YesNo);
                if (DR != System.Windows.Forms.DialogResult.Yes) { return; }
                mnuDownload.Enabled = false;
                BWDownload.CancelAsync();
                Client.CancelAsync();
                cMain.UpdateToolStripLabel(lblStatus, "Stopping...");
            }
        }

        private void DownloadAll()
        {
            bBusy = true;
            string sDir = cboSU.Text.ReplaceIgnoreCase(" x", "-x");
            sDir = sDir.ReplaceIgnoreCase(" ", "");


            int n = 1;
            if (!Directory.Exists(txtDownload.Text + sDir))
            {
                cMain.CreateDirectory(txtDownload.Text + sDir);
            }

            int CI = lstNew.CheckedItems.Count;

            foreach (ListViewItem LST in lstNew.CheckedItems)
            {
                pbProgress.Value = 0;

                LST.EnsureVisible();
                cMain.UpdateToolStripLabel(lblStatus, "Downloading (" + n + "/" + CI + "): " + LST.Text + " :: " + LST.SubItems[1].Text);
                StatusP = lblStatus.Text;
                Application.DoEvents();

                string downloadDirectory = txtDownload.Text + sDir;
                if (LST.SubItems[4].Text != "N/A")
                {
                    downloadDirectory = txtDownload.Text + sDir + "\\" + LST.SubItems[4].Text;
                }

                cMain.CreateDirectory(downloadDirectory);
                string sExtension = Path.GetExtension(LST.SubItems[5].Text).ToLower();
                if (string.IsNullOrEmpty(sExtension)) { sExtension = "." + LST.SubItems[2].Text.ToLower(); }
                string D = downloadDirectory + "\\" + LST.Text + sExtension;

                D = D.ReplaceIgnoreCase( "/", "\\");
                while (D.ContainsIgnoreCase("\\\\")) { D = D.ReplaceIgnoreCase( "\\\\", "\\"); }

                //Makes backup of current file.
                if (File.Exists(D)) { Files.DeleteFile(D + ".bak"); File.Move(D, D + ".bak"); }
            }
        }

        private void cmsSelectAll_Click(object sender, EventArgs e)
        {
            lstNew.BeginUpdate();
            foreach (ListViewItem LST in lstNew.SelectedItems[0].Group.Items)
            {
                LST.Checked = true;
            }
            lstNew.EndUpdate();
        }

        private void BWDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            bBusy = true;
            string sDir = cboSU.Text.ReplaceIgnoreCase(" x", "-x");
            sDir = sDir.ReplaceIgnoreCase(" ", "");


            int n = 1;
            if (!Directory.Exists(txtDownload.Text + sDir))
            {
                cMain.CreateDirectory(txtDownload.Text + sDir);
            }

            int CI = lstNew.CheckedItems.Count;

            for (int i = 0; i < lstNew.CheckedItems.Count; i++)
            {
                ListViewItem LST = lstNew.CheckedItems[i];

                pbProgress.Value = 0;



                //var thread0 = new Action(delegate
                //{
                lstNew.CheckedItems[i].EnsureVisible();
                //});
                //thread0.Invoke();
                cMain.UpdateToolStripLabel(lblStatus, "Downloading (" + n + "/" + CI + "): " + LST.Text + " :: " + LST.SubItems[1].Text);
                StatusP = lblStatus.Text;
                Application.DoEvents();

                string downloadDirectory;
                if (LST.SubItems[4].Text.EqualsIgnoreCase("N/A"))
                {
                    downloadDirectory = txtDownload.Text + sDir;
                }
                else
                {
                    downloadDirectory = txtDownload.Text + sDir + "\\" + LST.SubItems[4].Text;
                }

                cMain.CreateDirectory(downloadDirectory);
                string sExtension = Path.GetExtension(LST.SubItems[5].Text).ToLower();
                if (string.IsNullOrEmpty(sExtension)) { sExtension = "." + LST.SubItems[2].Text.ToLower(); }
                string D = downloadDirectory + "\\" + LST.Text + sExtension;

                D = D.ReplaceIgnoreCase( "/", "\\");
                while (D.ContainsIgnoreCase("\\\\")) { D = D.ReplaceIgnoreCase( "\\\\", "\\"); }

                try
                {
                    if (File.Exists(D)) { Files.DeleteFile(D + ".bak"); File.Move(D, D + ".bak"); }

                    //Client.Headers.Add("User-Agent:Win Toolkit_135792468");
                    Client.Proxy = null;
                    Client.DownloadProgressChanged += DownloadProgressCallback;

                    Client.DownloadFileAsync(new Uri(LST.SubItems[5].Text), D);
                    while (Client.IsBusy)
                    {
                        Thread.Sleep(250);
                        Application.DoEvents();
                        cMain.FreeRAM();
                    }
                    lblStatus.Text = "Checking";
                    cMain.UpdateToolStripLabel(lblStatus, "Checking (" + n + "/" + CI + "): " + LST.Text + " :: " + LST.SubItems[1].Text);

                    if (cMain.GetSize(D, false).EqualsIgnoreCase("0") || cMain.GetSize(D, false).EqualsIgnoreCase("-1") || BWDownload.CancellationPending)
                    {
                        Files.DeleteFile(D);
                        if (File.Exists(D + ".bak"))
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Restoring (" + n + "/" + CI + "): " + LST.Text + " :: " + LST.SubItems[1].Text);
                            File.Move(D + ".bak", D);
                        }
                    }
                    else
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Cleaning (" + n + "/" + CI + "): " + LST.Text + " :: " + LST.SubItems[1].Text);

                        Files.DeleteFile(D + ".bak");
                    }


                    if (File.Exists(D) && LST.SubItems[5].Text.ToUpper().EndsWithIgnoreCase("EXE") && !LST.SubItems[2].Text.EndsWithIgnoreCase("EXE"))
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Extracting (" + n + "/" + CI + "): " + LST.Text + " :: " + LST.SubItems[1].Text);
                        cMain.ExtractFiles(D, downloadDirectory, this, "*." + LST.SubItems[2].Text.ToLower(), false);
                        Files.DeleteFile(D);
                        D = D.Substring(0, D.Length - 3) + LST.SubItems[2].Text.ToLower();
                    }

                    try
                    {
                        if (File.Exists(D) && !BWDownload.CancellationPending)
                        {
                            if (cMain.IsFileLocked(D))
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Waiting (" + n + "/" + CI + "): " + LST.Text + " :: " + LST.SubItems[1].Text);

                                while (cMain.IsFileLocked(D))
                                {
                                    Thread.Sleep(250);
                                    Application.DoEvents();
                                }
                            }

                            cMain.UpdateToolStripLabel(lblStatus, "Updating (" + n + "/" + CI + "): " + LST.Text + " :: " + LST.SubItems[1].Text);

                            DateTime Dt = cMain.ParseDate(LST.SubItems[3].Text);
                            File.SetCreationTime(D, Dt);
                            File.SetLastAccessTime(D, DateTime.Now);
                            File.SetLastWriteTime(D, DateTime.Now);
                        }
                    }
                    catch (Exception Ex)
                    {
                        new SmallError("Error setting new update date", Ex, LST).Upload();
                    }
                }
                catch (Exception Ex)
                {
                    Files.DeleteFile(D);
                    if (File.Exists(D + ".bak")) { File.Move(D + ".bak", D); }
                    cMain.ErrorBox("Win Toolkit was unable to download the following file: " + LST.Text, "Download Error", Ex.Message);
                }

                cMain.UpdateToolStripLabel(lblStatus, "Downloaded (" + n + "/" + CI + "): " + LST.Text + " :: " + LST.SubItems[1].Text);

                if (BWDownload.CancellationPending)
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Deleting (" + n + "/" + CI + "): " + LST.Text + " :: " + LST.SubItems[1].Text);
                    Files.DeleteFile(D);
                }

                if (File.Exists(D))
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Unticking (" + n + "/" + CI + "): " + LST.Text + " :: " + LST.SubItems[1].Text);

                  lstNew.CheckedItems[i].Checked = false;
                
                    cMain.UpdateToolStripLabel(lblStatus, "Moving #1 (" + n + "/" + CI + "): " + LST.Text + " :: " + LST.SubItems[1].Text);

                    ListViewItem LSTNew = (ListViewItem)LST.Clone();

                    LSTNew.Group = lstOld.Groups[5];
                    if (LST.SubItems[4].Text.StartsWithIgnoreCase("General")) { LSTNew.Group = lstOld.Groups[0]; }
                    if (LST.SubItems[4].Text.StartsWithIgnoreCase("Hotfix")) { LSTNew.Group = lstOld.Groups[1]; }
                    if (LST.SubItems[4].Text.StartsWithIgnoreCase("Security")) { LSTNew.Group = lstOld.Groups[2]; }
                    if (LST.SubItems[4].Text.StartsWithIgnoreCase("Extra")) { LSTNew.Group = lstOld.Groups[3]; }
                    if (LST.SubItems[4].Text.StartsWithIgnoreCase("Additional")) { LSTNew.Group = lstOld.Groups[4]; }
                    LSTNew.SubItems.Add(LSTNew.SubItems[5].Text);
                    LSTNew.SubItems[5].Text = new FileInfo(D).LastWriteTime.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                   
                    cMain.UpdateToolStripLabel(lblStatus, "Moving #2 (" + n + "/" + CI + "): " + LST.Text + " :: " + LST.SubItems[1].Text);
                    //var thread2 = new Action(delegate
                    //{
                        lstOld.Items.Add(LSTNew);
                        lstNew.Items.Remove(LST);

                        int newItems = lstNew.Items.Count;
                        int oldItems = lstOld.Items.Count;
                        int newCheckedItems = lstNew.CheckedItems.Count;

                        tabNew.Text = "New [" + newItems + "]";
                        tabCurrent.Text = "Downloaded [" + oldItems + "]";
                        columnHeader1.Text = "Name (" + newItems + " - " + newCheckedItems + " Selected)";
                    //});
                   i--;
                    //thread2.Invoke();
                }

                cMain.UpdateToolStripLabel(lblStatus, "Finishing (" + n + "/" + CI + "): " + LST.Text + " :: " + LST.SubItems[1].Text);
                Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt16(n), Convert.ToUInt16(CI));
                n++;

                Application.DoEvents();
                if (BWDownload.CancellationPending)
                {
                    break;
                }
                cMain.FreeRAM();
            }

            cMain.UpdateToolStripLabel(lblStatus, "Deleting empty folders...");

            if (Directory.GetFiles(txtDownload.Text + sDir, "*.*", SearchOption.AllDirectories).Length < 1)
            {
                Files.DeleteFolder(txtDownload.Text + sDir, false);
            }

            cMain.UpdateToolStripLabel(lblStatus, "Preparing to scan old...");

            if (chkScan.Checked) { ScanOld(); }


        }

        private void BWDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cMain.UpdateToolStripLabel(lblStatus, "Finishing...");
            pbProgress.Value = pbProgress.Maximum;
            Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
            pbProgress.Visible = false;
            Application.DoEvents();
            mnuDownload.Text = "Download";
            mnuDownload.Enabled = true;
            mnuDownload.Image = Properties.Resources.download_package;
            bBusy = false;

            cMain.UpdateToolStripLabel(lblStatus, "Setting Top Item...");
            TopItem();

            cMain.UpdateToolStripLabel(lblStatus, "Updating Headers...");
            tabNew.Text = "New [" + lstNew.Items.Count + "]";
            tabCurrent.Text = "Downloaded [" + lstOld.Items.Count + "]";

            if (lstNew.Items.Count > 0)
            {
                foreach (ListViewGroup LVG in lstNew.Groups) { UpdateGroupHeader(LVG); }
                columnHeader1.Text = "Name (" + lstNew.Items.Count + " - " + lstNew.CheckedItems.Count + " Selected)";
            }
            else
            {
                columnHeader1.Text = "No New Updates.";
            }
            if (lstOld.Items.Count > 0)
            {
                foreach (ListViewGroup LVG in lstOld.Groups) { UpdateGroupHeader(LVG); }
            }
            cMain.UpdateToolStripLabel(lblStatus, "");
            DisableMe(true);
            bBusy = false;
        }

        private void UpdateButtons()
        {
            cmdDelOld.Visible = false;
            cmdScanOld.Visible = false;
            mnuDownload.Visible = false;
            switch (tabMain.SelectedIndex)
            {
                case 0:
                    ListViewEx.LVE = lstNew;
                    if (cboSU.SelectedIndex == -1)
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Please select a category.");
                    }
                    else
                    {
                        if (lstNew.Items.Count > 0) { mnuDownload.Visible = true; cMain.UpdateToolStripLabel(lblStatus, "Please select which updates you want to download."); }
                        else
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "No new updates.");
                        }
                    }

                    break;
                case 1:
                    ListViewEx.LVE = lstOld;
                    if (lstOld.Items.Count > 0) { cmdScanOld.Visible = true; cMain.UpdateToolStripLabel(lblStatus, "These are all of your downloaded updates."); }
                    else
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "You haven't downloaded any updates.");
                    }
                    break;
                case 2:
                    ListViewEx.LVE = lstDel;
                    if (lstDel.Items.Count > 0) { cmdDelOld.Visible = true; cMain.UpdateToolStripLabel(lblStatus, "These can safely be deleted."); }
                    else
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "No old/superseded updates.");
                    }
                    break;
            }
        }

        private void TabChanged(Object sender, EventArgs e)
        {
            if (!bBusy)
            {
                UpdateButtons();
            }
        }

        private void DisableMe(bool E = false)
        {
            cmdBrowse.Enabled = E;
            cmdRefresh.Enabled = E;
            cboSU.Enabled = E;
            lstNew.Enabled = E;
            lstOld.Enabled = E;
            lstDel.Enabled = E;
            chkScan.Enabled = E;

            if (E)
            {
                UpdateButtons();
            }
            else
            {
                mnuDownload.Visible = false;
                cmdScanOld.Visible = false;
                cmdDelOld.Visible = false;
            }
            this.ControlBox = E;
            if (lstNew.Items.Count == 0) { mnuDownload.Visible = false; }
            Application.DoEvents();
        }

        private void lstS_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (bBusy) { return; }
            lock (lstNew)
            {
                if (lstNew.InvokeRequired)
                {
                    var thread1 = new Action(delegate
                    {
                        columnHeader1.Text = "Name (" + lstNew.Items.Count + " - " + lstNew.CheckedItems.Count + " Selected)";
                    });
                    thread1.Invoke();
                }
                else
                {
                    columnHeader1.Text = "Name (" + lstNew.Items.Count + " - " + lstNew.CheckedItems.Count + " Selected)";
                }
            }
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            string FBD = cMain.FolderBrowserVista("Set download location...", true, true);

            if (string.IsNullOrEmpty(FBD))
            {
                return;
            }


            txtDownload.Text = FBD + "\\";
            cOptions.SolDownload = txtDownload.Text;
            cOptions.SaveSettings();

            string sDir = cboSU.Text.ReplaceIgnoreCase(" x", "-x");
            sDir = sDir.ReplaceIgnoreCase(" ", "");
            //if (sDir.ContainsIgnoreCase("Windows 7 x64")) { sDir = "Windows 7 x64"; }
            //if (sDir.ContainsIgnoreCase("Windows 7 x86")) { sDir = "Windows 7 x86"; }
            bBusy = true;
            if (Directory.Exists(txtDownload.Text + sDir))
            {
                cMain.UpdateToolStripLabel(lblStatus, "Cleaning Directory...");
                Application.DoEvents();
                CleanDir();
            }
            else
            {
                try
                {


                    cMain.UpdateToolStripLabel(lblStatus, "Preparing List...");
                    Application.DoEvents();
                    foreach (ListViewItem LST in lstNew.Items)
                    {
                        if (LST.Group == lstNew.Groups[0]) { LST.Checked = true; } else { LST.Checked = false; }
                    }
                }
                catch (Exception Ex)
                {
                    new SmallError("Error Preparing List.", Ex).Upload();
                }
            }
            bBusy = false;
            columnHeader1.Text = "Name (" + lstNew.Items.Count + " - " + lstNew.CheckedItems.Count + " Selected)";
            cMain.UpdateToolStripLabel(lblStatus, "Looking for old updates...");
            Application.DoEvents();
            ScanOld();
            DisableMe(true);
            cMain.UpdateToolStripLabel(lblStatus, "");
        }

        private void txtDownload_TextChanged(object sender, EventArgs e)
        {
            if (!txtDownload.Text.EndsWithIgnoreCase("\\") && !string.IsNullOrEmpty(txtDownload.Text))
            {
                txtDownload.Text += "\\";
            }
        }

        private string GetPath(ListViewItem LST)
        {
            string Dir;
            string sDir = cboSU.Text.ReplaceIgnoreCase(" x", "-x");
            sDir = sDir.ReplaceIgnoreCase(" ", "");
            if (LST.SubItems[4].Text.EqualsIgnoreCase("N/A"))
            {
                Dir = txtDownload.Text + sDir;
            }
            else
            {
                Dir = txtDownload.Text + sDir + "\\" + LST.SubItems[4].Text;
            }

            return Dir + "\\" + LST.Text + "." + LST.SubItems[2].Text.ToLower();

        }

        private void cmsUSelectAll_Click(object sender, EventArgs e)
        {
            lstNew.BeginUpdate();
            foreach (ListViewItem LST in lstNew.SelectedItems[0].Group.Items)
            {
                LST.Checked = false;
            }
            lstNew.EndUpdate();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (BWDownload.IsBusy || bBusy || lstNew.SelectedItems.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            cmdACheckAll.Visible = true;
            cmdAUnCheckAll.Visible = true;
            if (lstNew.CheckedItems.Count == lstNew.Items.Count)
            {
                cmdACheckAll.Visible = false;
            }
            if (lstNew.CheckedItems.Count == 0) { cmdAUnCheckAll.Visible = false; }

            if (lstNew.SelectedItems.Count > 0)
            {
                cmsSelectAll.Visible = false;
                cmsUSelectAll.Visible = false;
                foreach (ListViewItem LST in lstNew.SelectedItems[0].Group.Items)
                {
                    if (LST.Checked) { cmsUSelectAll.Visible = true; } else { cmsSelectAll.Visible = true; }
                    if (cmsUSelectAll.Visible && cmsSelectAll.Visible) { break; }
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DisableMe();
            bBusy = true;
            ScanD();
            pbProgress.Visible = false;
            lblStatus.Visible = false;
            ScanOld();
            lblStatus.Visible = true;
            DisableMe(true);
            bBusy = false;
        }

        private void UpdateList()
        {
            try
            {
                string sPrev = cboSU.Text;
                cboSU.Items.Clear();
                if (File.Exists(cMain.Root + "UpdateLists\\Windows7-x64.wh")) { cboSU.Items.Add("Windows 7 x64"); }
                if (File.Exists(cMain.Root + "UpdateLists\\Windows7-x86.wh")) { cboSU.Items.Add("Windows 7 x86"); }
                if (File.Exists(cMain.Root + "UpdateLists\\Windows8.1-x64.wh")) { cboSU.Items.Add("Windows 8.1 x64"); }
                if (File.Exists(cMain.Root + "UpdateLists\\Windows8.1-x86.wh")) { cboSU.Items.Add("Windows 8.1 x86"); }
                if (File.Exists(cMain.Root + "UpdateLists\\Office2010-x64.wh")) { cboSU.Items.Add("Office 2010 x64"); }
                if (File.Exists(cMain.Root + "UpdateLists\\Office2010-x86.wh")) { cboSU.Items.Add("Office 2010 x86"); }
                if (File.Exists(cMain.Root + "UpdateLists\\Office2013-x64.wh")) { cboSU.Items.Add("Office 2013 x64"); }
                if (File.Exists(cMain.Root + "UpdateLists\\Office2013-x86.wh")) { cboSU.Items.Add("Office 2013 x86"); }
                cboSU.Text = sPrev;
            }
            catch (Exception Ex)
            {
                new SmallError("Error Updating Lists", Ex).Upload();
            }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            var UC = new Prompts.frmAWDownloading();
            UC.bRefresh = true;

            if (UC.ShowDialog() != System.Windows.Forms.DialogResult.OK) { return; }
            UpdateList();
        }

        private void cmdDelOld_Click(object sender, EventArgs e)
        {
            DisableMe();
            bBusy = true;
            foreach (ListViewItem LST in lstDel.CheckedItems)
            {
                cMain.UpdateToolStripLabel(lblStatus, "Deleting: " + LST.Text);
                Application.DoEvents();
                Files.DeleteFile(LST.SubItems[5].Text);
                LST.Remove();
            }

            ScanOld();
            bBusy = false;
            DisableMe(true);
        }

        private void cmdACheckAll_Click(object sender, EventArgs e)
        {
            lstNew.BeginUpdate();
            foreach (ListViewItem LST in lstNew.Items)
            {
                LST.Checked = true;
            }
            lstNew.EndUpdate();
        }

        private void cmdAUnCheckAll_Click(object sender, EventArgs e)
        {
            lstNew.BeginUpdate();
            foreach (ListViewItem LST in lstNew.Items)
            {
                LST.Checked = false;
            }
            lstNew.EndUpdate();
        }

    }
}