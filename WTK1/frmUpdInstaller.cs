using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;

namespace WinToolkit
{
    public partial class frmUpdInstaller : Form
    {
        public static DialogResult UnUpdates = DialogResult.None;
        private readonly OpenFileDialog OFD = new OpenFileDialog();
        private int oA;
        private string sSupport;
        string V = cReg.GetValue(Microsoft.Win32.Registry.LocalMachine, "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "BuildLabEx") + Environment.NewLine;
        string vWB = "";

        List<string> sDeleteList = new List<string>();

        int UC;
        string uError = "";

        private void TabChanged(Object sender, EventArgs e)
        {
            ListViewEx.LVE = tabControl1.SelectedTab == tabPage1 ? lstIC : lstInstalled;
        }

        private void MouseScroll(object sender, MouseEventArgs e)
        {
            switch (tabControl1.SelectedTab.Text)
            {
                case "New":
                    lstIC.Select();
                    break;
                case "Installed":
                    lstInstalled.Select();
                    break;
            }
        }

        public frmUpdInstaller()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            FormClosing += frmCabInstaller_FormClosing;
            FormClosed += frmCabInstaller_FormClosed;
            CheckForIllegalCrossThreadCalls = false;
            MouseWheel += MouseScroll;
            BWA.RunWorkerCompleted += BWA_RunWorkerCompleted;
            BWI.RunWorkerCompleted += BWI_RunWorkerCompleted;
            BWScan.RunWorkerCompleted += BWScan_RunWorkerCompleted;
            BWU.RunWorkerCompleted += BWU_RunWorkerCompleted;
            tabControl1.SelectedIndexChanged += TabChanged;
            ListViewEx.LVE = lstIC;
            Activated += cMain.FormActivated;
            Deactivate += cMain.FormActivated;
        }

        private void frmCabInstaller_Load(object sender, EventArgs e)
        {
            scNew.Scale4K(_4KHelper.Panel.Pan1);
            scInstalled.Scale4K(_4KHelper.Panel.Pan1);
            cMain.FormIcon(this); cMain.eLBL = lblStatus; cMain.eForm = this;
            cMain.ToolStripIcons(ToolStrip1);
            cMain.ToolStripIcons(toolStrip2);
            cMain.FreeRAM();
            tsAfter.SelectedIndex = 0;
            cmdLDR.ToolTipText = "LDR Mode: This will install updates normally but will also extra fixes and possible features.\nNote: LDR Placeholders are not needed with this option.";
            tsAfter.ToolTipText = "Select what you would like to happen to your updates have been installed.";

            while (V.ContainsIgnoreCase(".")) { V = V.Substring(0, V.Length - 1); }
            string WinBuild = V;
            vWB = "SP" + V.Substring(V.Length - 1);
            vWB = vWB.ReplaceIgnoreCase("SP0", "RTM");

            lstIC.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lstInstalled.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void frmCabInstaller_FormClosing(object sender, FormClosingEventArgs e)
        {
            cMain.OnFormClosing(this);
            if (BWA.IsBusy || BWScan.IsBusy)
            {
                MessageBox.Show("You cannot close this windows while files are being added!", "Installation in Progress");
                e.Cancel = true;
            }

            if (BWI.IsBusy || BWU.IsBusy)
            {
                MessageBox.Show("You cannot close this windows while installation is in progress!", "Installation in Progress");
                e.Cancel = true;
            }

            while (BWDelete.IsBusy)
            {
                cMain.UpdateToolStripLabel(lblStatus, "Deleteing temp files...");
                System.Threading.Thread.Sleep(500);
                Application.DoEvents();
            }
        }

        private void frmCabInstaller_FormClosed(object sender, FormClosedEventArgs e)
        {
            cMain.ReturnME();
        }

        private void BWA_DoWork(object sender, DoWorkEventArgs e)
        {

            int nU = 1;
            int nUT = OFD.FileNames.Count(fUpd => lstIC.FindItemWithText(fUpd) == null);

            UC = 0;
            uError = "";

            lstIC.BeginUpdate();
            foreach (string fUpd in OFD.FileNames)
            {
                try
                {
                    if (lstIC.FindItemWithText(fUpd) == null && File.Exists(fUpd))
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "(" + nU + "\\" + nUT + ") Adding " + fUpd + "...");

                        string MD5 = cMain.MD5CalcFile(fUpd);
                        if (lstIC.FindItemWithText(fUpd) == null || string.IsNullOrEmpty(MD5))
                        {
                            if (fUpd.ToUpper().EndsWithIgnoreCase(".CAB") || fUpd.ToUpper().EndsWithIgnoreCase(".MSU"))
                            {
                                nU++;
                                if (fUpd.ContainsIgnoreCase("KB947821"))
                                    cMain.UpdateToolStripLabel(lblStatus, "Adding " + fUpd + ", this update may take a while...");
                                Application.DoEvents();
                                if (fUpd.ToUpper().EndsWithIgnoreCase(".CAB"))
                                    AddToList(fUpd, "update.mum", MD5);
                                if (fUpd.ToUpper().EndsWithIgnoreCase(".MSU"))
                                    AddToList(fUpd, "*.txt", MD5);

                            }
                            cMain.FreeRAM();
                        }
                    }
                }
                catch (Exception Ex)
                {
                    uError += "Unknown Error: '" + fUpd + "' - " + Ex.Message + Environment.NewLine;
                    UC += 1;

                    LargeError LE = new LargeError("Adding Update", "Error trying to add an update.", fUpd + "\n" + lblStatus.Text, Ex);
                    LE.Upload();
                }
                cMain.FreeRAM();
                if (BWA.CancellationPending) { break; }
            }

            lstIC.EndUpdate();
            lstIC.Columns[0].Text = "Name (" + lstIC.Items.Count + ")";

            if (UC > 0 && !string.IsNullOrEmpty(uError))
            {
                cMain.ErrorBox("More information is available by clicking the '>> Details' button.", UC + " update(s) skipped.", uError);
            }

        }

        private void EnableMe(bool E)
        {
            cmdSI.Enabled = E;
            cmdU.Enabled = E;
            cmdRefresh.Enabled = E;
            cmdAF.Visible = E;
            cmdR.Visible = E;
            cmdLDR.Visible = E;
            cmdCC.Visible = E;
            tsAfter.Visible = E;
            lstIC.Enabled = E;
            lstInstalled.Enabled = E;

            cmdU.Enabled = lstInstalled.Items.Count > 0 && E;
        }

        private void BWA_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (lstIC.Items.Count > 0)
            {
                lstIC.Items[0].EnsureVisible();
                cmdR.Visible = true;
            }
            else
            {
                cmdR.Visible = false;
            }
            cMain.UpdateToolStripLabel(lblStatus, "Added " + (lstIC.Items.Count - oA) + " updates...");
            cmdAF.Text = "Add";
            EnableMe(true);
            cmdU.Enabled = true;
            cMain.FreeRAM();
        }

        private string GetInfoCAB(string Info, string Line)
        {
            string TempL = Line;
            try
            {
                while (!TempL.StartsWithIgnoreCase(Info.ToUpper()))
                {
                    TempL = TempL.Substring(1);
                }
                while (!TempL.StartsWithIgnoreCase("\""))
                {
                    TempL = TempL.Substring(1);
                }
                TempL = TempL.Substring(1);

                while (TempL.ContainsIgnoreCase("\""))
                {
                    TempL = TempL.Substring(0, TempL.Length - 1);
                }
            }
            catch (Exception)
            {
                TempL = "N/A";
            }
            return TempL;
        }

        private string GetFolder(string FileN)
        {
            string F = FileN;
            try
            {
                while (!F.EndsWithIgnoreCase("\\"))
                {
                    F = F.Substring(0, F.Length - 1);
                }
                F = F.Substring(0, F.Length - 1);
            }
            catch
            {
            }
            return F;
        }

        private void cmdAF_Click(object sender, EventArgs e)
        {
            if (cmdAF.Text.EqualsIgnoreCase("Add"))
            {
                OFD.Multiselect = true;
                OFD.Title = "Select Updates...";
                OFD.Filter = "Windows Updates *.cab *.msu|*.cab;*.msu;";
                if (Directory.Exists(cOptions.fUpdates))
                {
                    OFD.InitialDirectory = cOptions.fUpdates;
                }
                if (OFD.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                cOptions.fUpdates = GetFolder(OFD.FileName);
                EnableMe(false);
                cmdAF.Text = "Cancel";
                cmdAF.Enabled = true;
                cMain.FreeRAM();
                BWA.RunWorkerAsync();
            }
            else
            {
                cmdAF.Enabled = false;
                BWA.CancelAsync();
                cMain.UpdateToolStripLabel(lblStatus, "Stopping...");
            }
        }

        private void IntegrateUpdates(ListViewItem LST, string dIndex)
        {
            if (LST.SubItems.Count < 4) { return; }

            cMain.UpdateToolStripLabel(lblStatus, "Starting #2 (" + LST.SubItems.Count + "): " + dIndex + " of " + lstIC.Items.Count + " (" + LST.Text + ")");
            string IF = LST.SubItems[4].Text;

            cMain.UpdateToolStripLabel(lblStatus, "Checking #1: " + dIndex + " of " + lstIC.Items.Count + " (" + LST.Text + ")");
            if (IF.ContainsForeignCharacters() && File.Exists(IF))
            {
                IF = cOptions.WinToolkitTemp + "\\" + LST.Index + IF.Substring(IF.Length - 4);
                cMain.UpdateToolStripLabel(lblStatus, "Copying: " + dIndex + " of " + lstIC.Items.Count + " (" + LST.Text + ")");
                File.Copy(LST.SubItems[4].Text, IF, true);
            }

            try
            {
                if (Directory.Exists(cOptions.WinToolkitTemp))
                {
                    cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Deleting Temp Folders: " + dIndex + " of " + LST.Group.Items.Count + " (" + LST.Text + ")");
                    foreach (string D in Directory.GetDirectories(cOptions.WinToolkitTemp, "*", SearchOption.TopDirectoryOnly))
                    {
                        if (D.StartsWithIgnoreCase("CINST")) { Files.DeleteFolder(D, false); }
                    }

                }
            }
            catch (Exception Ex)
            {
            }

            cMain.UpdateToolStripLabel(lblStatus, "Checking #2: " + dIndex + " of " + lstIC.Items.Count + " (" + LST.Text + ")");

            if (File.Exists(IF))
            {
                cMain.UpdateToolStripLabel(lblStatus, "Preparing: " + dIndex + " of " + lstIC.Items.Count + " (" + LST.Text + ")");

                try
                {
                    Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst", true);

                    bool LDR = cmdLDR.Checked;

                    if (IF.ContainsIgnoreCase("KB2685811") || LST.Text.ContainsIgnoreCase("KB2685811") || LST.SubItems[1].Text.ContainsIgnoreCase("KB2685811")) { LDR = true; }
                    if (IF.ContainsIgnoreCase("KB2685813") || LST.Text.ContainsIgnoreCase("KB2685813") || LST.SubItems[1].Text.ContainsIgnoreCase("KB2685813")) { LDR = true; }
                    if (IF.ContainsIgnoreCase("KB2727118") || LST.Text.ContainsIgnoreCase("KB2727118") || LST.SubItems[1].Text.ContainsIgnoreCase("KB2727118"))
                    {
                        cReg.WriteValue(Microsoft.Win32.Registry.LocalMachine, "System\\CurrentControlSet\\Control\\Terminal Server", "LsmServiceStartTimeout", 1107296255, Microsoft.Win32.RegistryValueKind.DWord);
                    }

                    cMain.UpdateToolStripLabel(lblStatus, "Integrating: " + dIndex + " of " + lstIC.Items.Count + " (" + LST.Text + ")");
                    switch (Path.GetExtension(IF).ToUpper())
                    {
                        case ".MSU":
                            cMain.OpenProgram("\"" + cMain.SysFolder + "\\wusa.exe\"", "\"" + IF + "\" /quiet /norestart", true, ProcessWindowStyle.Hidden);
                            break;
                        case ".CAB":
                            cMain.OpenProgram("\"" + DISM.Latest.Location + "\"", "/Online /Add-Package /PackagePath:\"" + IF + "\" /Quiet /NoRestart", true, ProcessWindowStyle.Hidden);
                            break;
                    }

                    if (LDR && !IF.ContainsIgnoreCase("KB947821") && !LST.Text.ContainsIgnoreCase("KB947821") && BWI.CancellationPending == false)
                    {
                        if (IF.ToUpper().EndsWithIgnoreCase(".MSU"))
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Extracting MSU: " + dIndex + " of " + lstIC.Items.Count + " (" + LST.Text + ")");
                            cMain.ExtractFiles(IF, cOptions.WinToolkitTemp + "\\cInst_" + dIndex + "A", this, "*.cab");
                            Files.DeleteFile(cOptions.WinToolkitTemp + "\\cInst_" + dIndex + "A\\WSUSSCAN.cab");
                            foreach (string FileC in Directory.GetFiles(cOptions.WinToolkitTemp + "\\cInst_" + dIndex + "A", "*.cab"))
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Extracting CAB: " + dIndex + " of " + lstIC.Items.Count + " (" + LST.Text + ")");
                                cMain.ExtractFiles(FileC, cOptions.WinToolkitTemp + "\\cInst_" + dIndex + "B", this);
                                Files.DeleteFile(FileC);
                            }
                            sDeleteList.Add(cOptions.WinToolkitTemp + "\\cInst_" + dIndex + "A");
                        }

                        if (IF.ToUpper().EndsWithIgnoreCase(".CAB"))
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Extracting CAB: " + dIndex + " of " + lstIC.Items.Count + " (" + LST.Text + ")");
                            cMain.ExtractFiles(IF, cOptions.WinToolkitTemp + "\\cInst_" + dIndex + "B", this);

                        }

                        string BF = cOptions.WinToolkitTemp + "\\cInst_" + dIndex + "B\\update-bf.mum";
                        if (File.Exists(BF))
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Integrating: " + dIndex + " of " + lstIC.Items.Count + " (" + LST.Text + " QFE)");
                            cMain.OpenProgram("\"" + DISM.Latest.Location + "\"", "/Online /Add-Package /PackagePath:\"" + BF + "\" /Quiet /NoRestart", true, ProcessWindowStyle.Hidden);
                        }

                    }

                    sDeleteList.Add(cOptions.WinToolkitTemp + "\\cInst_" + dIndex + "A");
                    sDeleteList.Add(cOptions.WinToolkitTemp + "\\cInst_" + dIndex + "B");
                    Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst", false);

                    if (IF != LST.SubItems[4].Text && File.Exists(IF)) { Files.DeleteFile(IF); }
                    if (!BWDelete.IsBusy) { BWDelete.RunWorkerAsync(); }

                }
                catch (Exception Ex)
                {
                    string uErr = LST.Text = Environment.NewLine;
                    int sub = 1;
                    foreach (ListViewItem.ListViewSubItem uSub in LST.SubItems) { uErr += sub.ToString(CultureInfo.InvariantCulture) + ": " + uSub.Text + Environment.NewLine; sub += 1; }
                    new SmallError("Error trying to integrate update.", Ex).Upload();
                }

                if (BWI.CancellationPending) { return; }

            }

            cMain.FreeRAM();
            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst", false);

        }

        private static bool CheckIntegration(ListViewItem LST)
        {
            try
            {
                if (Directory.GetFiles(cMain.SysDrive + "\\Windows\\servicing\\Packages").Where(i => i.ContainsIgnoreCase(LST.Text.ToUpper()) || i.ContainsIgnoreCase(LST.SubItems[1].Text.ToUpper())).Count() > 0) { return true; }
            }
            catch { }

            return false;
        }

        private void BWI_DoWork(object sender, DoWorkEventArgs e)
        {

            Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
            PB.Value = 0;
            PB.Maximum = lstIC.Items.Count;
            int cIdx = 1;
            foreach (ListViewItem LST in lstIC.Items)
            {
                try
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Starting: " + LST.Index + " of " + lstIC.Items.Count + " (" + LST.Text + ")");

                    LST.EnsureVisible();
                    Application.DoEvents();
                    if (CheckIntegration(LST) == false)
                    {
                        LST.ImageIndex = 0;
                        IntegrateUpdates(LST, cIdx.ToString(CultureInfo.InvariantCulture));
                        if (CheckIntegration(LST))
                        {
                            LST.ImageIndex = 1;
                        }
                        else
                        {
                            LST.ImageIndex = 2;
                        }
                    }
                    else
                    {
                        LST.ImageIndex = 1;
                    }

                }
                catch (Exception Ex)
                {
                    string uErr = LST.Text = Environment.NewLine;
                    int sub = 1;
                    foreach (ListViewItem.ListViewSubItem uSub in LST.SubItems) { uErr += sub.ToString(CultureInfo.InvariantCulture) + ": " + uSub.Text + Environment.NewLine; sub += 1; }
                    cMain.WriteLog(this, "Error Integrating Update", uErr + Environment.NewLine + Environment.NewLine + Ex.Message, lblStatus.Text);
                }

                if (BWI.CancellationPending)
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Installation Aborted!");
                    break;
                }

                if (PB.Value <= PB.Maximum)
                {
                    PB.Value++;
                    Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt16(PB.Value), Convert.ToUInt16(lstIC.Items.Count));
                }
                cMain.FreeRAM();
                cIdx++;
            }
            PB.Value = 0;
            Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst", false);
        }

        private void BWI_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PB.Visible = false;
            EnableMe(true);
            cmdSI.Text = "Start";
            cmdSI.Image = Properties.Resources.OK;
            cmdU.Text = "Uninstall";
            cmdU.Image = Properties.Resources.OK;
            cMain.FreeRAM();

            if (tsAfter.Text.EqualsIgnoreCase("Restart") || tsAfter.Text.EqualsIgnoreCase("Shutdown"))
            {
                cMain.UpdateToolStripLabel(lblStatus, "Waiting for user...");
                Application.DoEvents();
                var TS = new Prompts.frmShutdown();
                TS.Text = tsAfter.Text;
                TS.ShowDialog();
            }

            cmdRefresh.PerformClick();

            try
            {
                if (!lblStatus.Text.ContainsIgnoreCase("Aborted"))
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Installation Completed!");
                    if (cReg.RegCheckMounted("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate\\Auto Update\\RebootRequired"))
                    {
                        DialogResult DResult = MessageBox.Show("Some updates require a restart, would you like to restart now? Please make sure you have saved your work before clicking yes!", "Restart Required", MessageBoxButtons.YesNo);
                        if (DResult == DialogResult.Yes)
                        {
                            cMain.OpenProgram("\"" + cMain.SysFolder + "\\shutdown.exe\"", "-r -t 5", true, ProcessWindowStyle.Hidden);
                            return;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Unable to check for reboot.", Ex.Message, lblStatus.Text);
            }

        }

        private void AddToList(string FileName, string InfoFile, string MD5)
        {
            if (File.Exists(FileName) && cMain.GetSize(FileName, false).EqualsIgnoreCase("0")) { uError += "Corrupted (0 bytes): '" + FileName + "'" + Environment.NewLine; UC += 1; return; }
            if (!File.Exists(FileName)) { uError += "File Not Found: '" + FileName + "'" + Environment.NewLine; UC += 1; return; }

            string cArc = "x86";
            if (cMain.Arc64)
            {
                cArc = "x64";
            }

            string uName = null;
            string uDesc = null;
            string uArc = null;
            string uSupport = "";
            string uLang = "";
            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst", true);
            cMain.ExtractFiles(FileName, cOptions.WinToolkitTemp + "\\cInst", this, InfoFile);

            switch (FileName.ToUpper().Substring(FileName.Length - 4))
            {
                case ".CAB":
                    if (!File.Exists(cOptions.WinToolkitTemp + "\\cInst\\" + InfoFile))
                    {
                        MessageBox.Show("The file \"" + FileName + "\" is not supported!", "File not supported");
                        return;
                    }
                    uName = "";
                    uArc = "";
                    uDesc = "";
                    uSupport = "N/A";
                    uLang = "";

                    string rUpdate;
                    using (var strReader = new StreamReader(cOptions.WinToolkitTemp + "\\cInst\\" + InfoFile))
                    {
                        rUpdate = strReader.ReadToEnd();
                    }
                    Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst", false);

                    if (cmdCC.Checked)
                    {
                        if (!rUpdate.ContainsIgnoreCase(V) && !rUpdate.ContainsIgnoreCase("Microsoft-Windows-InternetExplorer-") && !rUpdate.ContainsIgnoreCase("WUClient-SelfUpdate")) { uError += "Not compatible with " + V + ": '" + FileName + "'" + Environment.NewLine; UC += 1; return; }
                    }

                    foreach (string line in rUpdate.Split(Environment.NewLine.ToCharArray()))
                    {
                        string strLineUpd = line;
                        if (!string.IsNullOrEmpty(strLineUpd))
                        {
                            if (string.IsNullOrEmpty(uName) && strLineUpd.ContainsIgnoreCase("ASSEMBLYIDENTITY NAME"))
                                uName = GetInfoCAB("ASSEMBLYIDENTITY NAME", strLineUpd);

                            if (string.IsNullOrEmpty(uDesc) && strLineUpd.ContainsIgnoreCase("DESCRIPTION"))
                                uDesc = GetInfoCAB("Description", strLineUpd);
                            if (string.IsNullOrEmpty(uArc) && strLineUpd.ContainsIgnoreCase("PROCESSORARCHITECTURE"))
                                uArc = GetInfoCAB("processorArchitecture", strLineUpd);
                            if (uSupport.EqualsIgnoreCase("N/A") && strLineUpd.ContainsIgnoreCase("SUPPORTINFORMATION"))
                                uSupport = GetInfoCAB("supportInformation", strLineUpd);

                            if (strLineUpd.ContainsIgnoreCase("Microsoft-Windows-Client-LanguagePack-Package"))
                            {
                                uLang = "LP_" + cMain.GetLPName(strLineUpd);
                            }

                            if (!string.IsNullOrEmpty(uName) && !string.IsNullOrEmpty(uDesc) && !string.IsNullOrEmpty(uArc) && !string.IsNullOrEmpty(uSupport))
                            {
                                break;
                            }
                        }
                    }

                    break;
                case ".MSU":
                    InfoFile = "";
                    foreach (string eFile in Directory.GetFiles(cOptions.WinToolkitTemp + "\\CInst\\", "*.txt"))
                    {
                        InfoFile = eFile;
                    }

                    if (File.Exists(InfoFile))
                    {
                        uName = "";
                        uArc = "";
                        uSupport = "N/A";
                        uLang = "";

                        string rUpdate2;
                        using (var strReader = new StreamReader(InfoFile))
                        {
                            rUpdate2 = strReader.ReadToEnd();
                        }
                        Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst", false);

                        if (cmdCC.Checked)
                        {
                            if (!vWB.ContainsIgnoreCase("RTM") && !vWB.ContainsIgnoreCase("SP0"))
                            {
                                if (!rUpdate2.ContainsIgnoreCase(vWB) && rUpdate2.ContainsIgnoreCase("APPLICABILITYINFO") && !rUpdate2.ContainsIgnoreCase("IE10-"))
                                {
                                    uError += "Not compatible with " + vWB + ": '" + FileName + "'" + Environment.NewLine;
                                    UC += 1;
                                    break;
                                }
                            }
                        }
                        foreach (string line in rUpdate2.Split(Environment.NewLine.ToCharArray()))
                        {
                            string strLineUpd = line;
                            if (!string.IsNullOrEmpty(strLineUpd))
                            {
                                if (string.IsNullOrEmpty(uName) && strLineUpd.ContainsIgnoreCase("KB Article Number"))
                                    uName = GetInfo(strLineUpd);
                                if (string.IsNullOrEmpty(uArc) && strLineUpd.ContainsIgnoreCase("Processor Architecture"))
                                    uArc = GetInfo(strLineUpd);
                                if (uSupport.EqualsIgnoreCase("N/A") && strLineUpd.ContainsIgnoreCase("Support Link"))
                                    uSupport = GetInfo(strLineUpd);
                                if (!string.IsNullOrEmpty(uName) && !string.IsNullOrEmpty(uArc) && uSupport != "N/A")
                                {
                                    if (uName.IsNumeric()) { uName = "KB" + uName; }
                                    uDesc = uName;
                                    break;
                                }
                            }
                        }

                    }
                    else
                    {
                        uName = cMain.GetFName(FileName);
                        uDesc = cMain.GetFName(FileName);
                        uArc = cArc;
                    }
                    break;
            }

            if (!string.IsNullOrEmpty(uName) && !uName.ContainsIgnoreCase("PACKAGE_FOR_")) { uName = uName.ReplaceIgnoreCase("KB", ""); }
            if (uName.IsNumeric()) { uName = "KB" + uName; }
            if (string.IsNullOrEmpty(uArc))
            {
                uArc = "??";
                if (FileName.ContainsIgnoreCase("X64"))
                {
                    uArc = "amd64";
                }
                if (FileName.ContainsIgnoreCase("X86"))
                {
                    uArc = "x86";
                }
            }

            if (uArc.EqualsIgnoreCase("amd64"))
                uArc = "x64";

            if (uArc != "??" && cArc != "??")
            {
                if (cArc != uArc)
                {
                    uError += "Invalid architecture: '" + FileName + "'" + Environment.NewLine;
                    UC += 1;
                    return;
                }
            }

            var NewUpdate = new ListViewItem();

            if (!string.IsNullOrEmpty(uDesc) && uDesc.ContainsIgnoreCase("LANGUAGE PACK"))
            {
                uSupport = "http://support.microsoft.com/kb/2483139";
                uName = cMain.GetLPName(uName);
                if (uDesc != cMain.GetLPName(uDesc))
                {
                    uDesc = cMain.GetLPName(uDesc) + " Language Pack for " + uArc;
                }
            }

            if (uName.EqualsIgnoreCase("neutral"))
                uName = cMain.GetFName(FileName);
            if (string.IsNullOrEmpty(uName))
            {
                uName = cMain.GetFName(FileName);
            }

            if (string.IsNullOrEmpty(uDesc) && !string.IsNullOrEmpty(uLang))
            {
                uDesc = cMain.GetLPName(uLang) + " Language Pack for " + uArc;
                uName = cMain.GetLPName(uLang) + " Language Pack";
            }

            if (!string.IsNullOrEmpty(uDesc) && uName.ToUpper().EqualsIgnoreCase("DEFAULT") && uDesc.ContainsIgnoreCase("KB"))
            {
                uName = uDesc;
                while (!uName.StartsWithIgnoreCase("KB")) { uName = uName.Substring(1); }
                while (uName.ContainsIgnoreCase(" ")) { uName = uName.Substring(0, uName.Length - 1); }
            }

            if (uName.ContainsIgnoreCase("KB982861"))
            {
                uName = "Internet Explorer 9";
            }

            if (uName.ContainsIgnoreCase("KBKB2647516")) { uDesc = "Cumulative Security Update for Internet Explorer 9"; }

            NewUpdate.Text = uName;
            NewUpdate.Tag = uLang;
            NewUpdate.SubItems.Add(uDesc);
            NewUpdate.SubItems.Add(cMain.GetSize(FileName, true));
            NewUpdate.SubItems.Add(uArc);
            NewUpdate.SubItems.Add(FileName);
            NewUpdate.SubItems.Add(MD5);

            if (!string.IsNullOrEmpty(uSupport))
            {
                uSupport = uSupport.ReplaceIgnoreCase("support.microsoft.com?kbid=", "support.microsoft.com/kb/", true);
            }
            else
            {
                uSupport = "http://www.google.co.uk/?#q=" + uName;
            }


            NewUpdate.SubItems.Add(uSupport);

            lstIC.Items.Add(NewUpdate);

            cMain.FreeRAM();
            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst", false);
        }

        private string GetInfo(string Info)
        {
            string TempL = Info;
            try
            {
                while (!TempL.StartsWithIgnoreCase(Info.ToUpper()))
                {
                    TempL = TempL.Substring(1);
                }
                while (!TempL.StartsWithIgnoreCase("\""))
                {
                    TempL = TempL.Substring(1);
                }
                TempL = TempL.Substring(1);

                while (TempL.ContainsIgnoreCase("\""))
                {
                    TempL = TempL.Substring(0, TempL.Length - 1);
                }
            }
            catch (Exception)
            {
                TempL = "N/A";
            }
            return TempL;
        }

        private void cmdSI_Click(object sender, EventArgs e)
        {
            if (cmdSI.Text.EqualsIgnoreCase("Start"))
            {
                if (lstIC.Items.Count == 0)
                {
                    MessageBox.Show("You need at least 1 item to install before you can continue!", "Not enough files!");
                    return;
                }
                EnableMe(false);
                cmdSI.Text = "Cancel";
                cmdSI.Image = Properties.Resources.Close;
                cmdSI.Enabled = true;
                PB.Visible = true;
                BWI.RunWorkerAsync();
            }
            else
            {
                cMain.UpdateToolStripLabel(lblStatus, "Aborting installation, please wait...");
                BWI.CancelAsync();
                cmdSI.Enabled = false;
            }
        }

        private void lstIC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (sSupport != "")
            {
                Process.Start(sSupport);
            }
        }

        private void lstIC_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            sSupport = e.Item.SubItems[6].Text;
        }

        private void lstIC_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lstIC_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                EnableMe(false);
                var FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                oA = lstIC.Items.Count;
                int n = 1;
                int T = FileList.Count(strFilename => strFilename.ToUpper().EndsWithIgnoreCase(".MSU") || strFilename.ToUpper().EndsWithIgnoreCase(".CAB"));

                UC = 0;
                uError = "";

                foreach (string strFilename in FileList)
                {
                    if (strFilename.ToUpper().EndsWithIgnoreCase(".MSU") || strFilename.ToUpper().EndsWithIgnoreCase(".CAB"))
                    {
                        try
                        {
                            if (lstIC.FindItemWithText(strFilename) == null && File.Exists(strFilename))
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Adding " + n + " of " + T + ": " + strFilename + "...");
                                Application.DoEvents();
                                string MD5 = cMain.MD5CalcFile(strFilename);

                                if (lstIC.FindItemWithText(MD5) == null || string.IsNullOrEmpty(MD5))
                                {
                                    if (strFilename.ToUpper().EndsWithIgnoreCase(".CAB")) { AddToList(strFilename, "update.mum", MD5); }
                                    if (strFilename.ToUpper().EndsWithIgnoreCase(".MSU")) { AddToList(strFilename, "*.txt", MD5); }
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            uError += "Unknown Error: '" + strFilename + "' - " + Ex.Message + Environment.NewLine;
                            UC += 1;
                            cMain.WriteLog(this, "Error trying to add update (DragDrop)" + Environment.NewLine + strFilename, Ex.Message, lblStatus.Text);
                        }
                        n += 1;
                    }
                }

                if (UC > 0 && !string.IsNullOrEmpty(uError))
                {
                    cMain.ErrorBox("More information is available by clicking the '>> Details' button.", UC + " update(s) skipped.", uError);
                }

                cMain.UpdateToolStripLabel(lblStatus, "Added " + (lstIC.Items.Count - oA) + " updates...");
                EnableMe(true);
            }
            catch (Exception Ex)
            {
                cMain.ErrorBox("Win Toolkit could not add the selected update(s) via DragDrop", "DragDrop Error", Ex.Message);
                cMain.WriteLog(this, "Unable to add update via DragDrop.", Ex.Message, lblStatus.Text);
            }
        }

        private void BWScan_DoWork(object sender, DoWorkEventArgs e)
        {
            cMain.UpdateToolStripLabel(lblStatus, "Looking for Updates...");
            cmdU.Enabled = false;
            lstInstalled.Items.Clear();
            Application.DoEvents();
            try
            {
                string UICheck = cMain.RunExternal("\"" + DISM.Latest.Location + "\"", "/Online /Get-Packages /Format:Table");
                foreach (string Line in UICheck.Split(Environment.NewLine.ToCharArray()))
                {
                    if (Line.ContainsIgnoreCase("~~") && !string.IsNullOrEmpty(Line) &&
                         !Line.ContainsIgnoreCase("LocalPack") && !Line.ContainsIgnoreCase("Agent-Package") &&
                         !Line.ContainsIgnoreCase("CodecPack") && !Line.ContainsIgnoreCase("Foundation-Package") &&
                         !Line.ContainsIgnoreCase("Windows-IE-Troubleshooters") &&
                         !Line.ContainsIgnoreCase("InternetExplorer-Optional-Package"))
                    {
                        try
                        {
                            var NI = new ListViewItem();
                            foreach (string U in Line.Split('|'))
                            {
                                string LU = U;
                                try
                                {
                                    while (!LU.EndsWithIgnoreCase(" "))
                                    {
                                        LU = LU.Substring(0, LU.Length - 1);
                                    }
                                }
                                catch
                                {
                                }
                                if (LU.ContainsIgnoreCase("~"))
                                {
                                    string ST = LU;
                                    while (!ST.ContainsIgnoreCase("~"))
                                    {
                                        ST = ST.Substring(0, ST.Length - 1);
                                    }
                                    NI.Text = ST;
                                    while (NI.Text.EndsWithIgnoreCase(" "))
                                        NI.Text = NI.Text.Substring(0, NI.Text.Length - 1);
                                    while (LU.EndsWithIgnoreCase(" "))
                                        LU = LU.Substring(0, LU.Length - 1);
                                    NI.SubItems.Add(LU);
                                }
                                else
                                {
                                    NI.SubItems.Add(LU);
                                }
                            }

                            while (NI.Text.ContainsIgnoreCase("~"))
                            {
                                NI.Text = NI.Text.Substring(0, NI.Text.Length - 1);
                            }

                            if (NI.Text.ContainsIgnoreCase("InternetExplorer-Package") &&
                                 NI.SubItems[1].Text.ContainsIgnoreCase("~~9"))
                            {
                                NI.Text = "Internet Explorer 9";
                            }

                            if (
                                 NI.Text.ContainsIgnoreCase("Microsoft-Windows-Winhelp-Update-Client-TopLevel") &&
                                 NI.SubItems[1].Text.ContainsIgnoreCase("6.1.0.1"))
                            {
                                NI.Text = "Package_for_KB917607 (WinHlp32.exe)";
                            }
                            //if (lstInstalled.FindItemWithText(NI.Text) == null)
                            //{
                            lstInstalled.Items.Add(NI);
                            //}

                        }
                        catch
                        {
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Error checking for updates.", Ex.Message, lblStatus.Text);
            }

            foreach (ColumnHeader CH in lstInstalled.Columns) { CH.Width = -2; }

        }
        private void BWScan_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            chUN.Text = "Update Name (" + lstInstalled.Items.Count + ")";
            lstInstalled.EndUpdate();
            EnableMe(true);
            cmdU.Enabled = lstInstalled.Items.Count > 0;
            if (BWI.IsBusy == false && BWA.IsBusy == false && BWU.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, ""); }

            if (lstInstalled.Items.Count == 0)
                lstInstalled.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            else
                lstInstalled.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void cmdU_Click(object sender, EventArgs e)
        {
            if (cmdU.Text.EqualsIgnoreCase("Uninstall"))
            {
                if (lstInstalled.CheckedItems.Count == 0)
                {
                    MessageBox.Show("You need at least 1 item to uninstall before you can continue!", "Not enough files!");
                    return;
                }
                EnableMe(false);
                cmdU.Text = "Cancel";
                cmdU.Image = Properties.Resources.Close;
                cmdU.Enabled = true;
                PB.Visible = true;
                BWU.RunWorkerAsync();
            }
            else
            {
                cMain.UpdateToolStripLabel(lblStatus, "Aborting task, please wait...");
                BWU.CancelAsync();
                cmdU.Enabled = false;
            }
        }

        private void BWU_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EnableMe(true);
            cmdR.Visible = true;
            cmdAF.Visible = true;
            PB.Visible = false;
            cmdSI.Text = "Start";
            cmdSI.Image = Properties.Resources.OK;
            cmdU.Text = "Uninstall";
            cmdU.Image = Properties.Resources.OK;
            cMain.FreeRAM();
            cmdRefresh.PerformClick();
        }

        private void BWU_DoWork(object sender, DoWorkEventArgs e)
        {

            Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
            PB.Value = 0;
            PB.Maximum = lstInstalled.CheckedItems.Count;
            foreach (ListViewItem LST in lstInstalled.CheckedItems)
            {
                try
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Removing Update: " + (PB.Value + 1) + " of " + lstInstalled.CheckedItems.Count + " (" + LST.Text + ")");
                    cMain.OpenProgram("\"" + DISM.Latest.Location + "\"", "/Online /Remove-Package /PackageName:" + LST.SubItems[1].Text + " /NoRestart", true, ProcessWindowStyle.Hidden);
                }
                catch (Exception Ex)
                {
                    cMain.WriteLog(this, "Unable to remove package.", Ex.Message, lblStatus.Text);
                    cMain.ErrorBox("Win Toolkit was unable to remove '" + LST.Text + "' update.", "Error removing update", Ex.Message);
                }
                if (BWU.CancellationPending)
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Installation Aborted!");
                    break;
                }
                cMain.FreeRAM();
                PB.Value += 1;
                Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt16(PB.Value), Convert.ToUInt16(lstInstalled.CheckedItems.Count));
            }
            PB.Value = 0;
            Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);

        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            EnableMe(false);
            lstInstalled.BeginUpdate();
            BWScan.RunWorkerAsync();
        }

        private void cmdRA_Click(object sender, EventArgs e)
        {
            lstIC.Items.Clear();
        }

        private void cmdRS_Click(object sender, EventArgs e)
        {
            if (lstIC.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 file", "Invalid Selection");
                return;
            }
            foreach (ListViewItem LST in lstIC.Items)
            {
                if (LST.Selected)
                {
                    LST.Remove();
                }
            }
        }

        private void cmdCC_Click(object sender, EventArgs e)
        {
            cmdCC.Image = cmdCC.Checked ? Properties.Resources.Checked : Properties.Resources.Unchecked;
        }

        private void cmdLDR_Click(object sender, EventArgs e)
        {
            cmdLDR.Image = cmdLDR.Checked ? Properties.Resources.Checked : Properties.Resources.Unchecked;
        }

        private void lstIC_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void BWDelete_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= sDeleteList.Count; i++)
            {
                string s = sDeleteList[sDeleteList.Count - 1];
                Files.DeleteFolder(s, false);
                sDeleteList.Remove(s);
            }
        }

    }
}