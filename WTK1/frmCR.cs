using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;

namespace WinToolkit
{
    public partial class frmComponentRemover : Form
    {
        private string DVDLoc = "";
        private string Err = "";
        private bool IMGChanged, MErr, Starting;
        static cComponents ComponentClass;
        WIMImage sImage = cMain.selectedImages[0];

        public frmComponentRemover()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            CheckForIllegalCrossThreadCalls = false;
            FormClosing += frmCR_FormClosing;
            FormClosed += frmCR_FormClosed;
            Shown += frmCR_Shown;
            BWRemove.RunWorkerCompleted += BWRemove_RunWorkerCompleted;
            BWStart.RunWorkerCompleted += BWStart_RunWorkerCompleted;
            Activated += cMain.FormActivated;
            Deactivate += cMain.FormActivated;
            MouseWheel += MouseScroll;
        }

        private void frmCR_Load(object sender, EventArgs e)
        {
            SplitContainer1.Scale4K(_4KHelper.Panel.Pan1);
            cMain.UpdateToolStripLabel(lblStatus, "Loading..."); cMain.eForm = this;
            Starting = true;
            cMain.FormIcon(this); cMain.eLBL = lblStatus;
            cMain.ToolStripIcons(ToolStrip1);
            Width = Screen.PrimaryScreen.WorkingArea.Width - 200;
            Height = Screen.PrimaryScreen.WorkingArea.Height - 200;
            CenterToScreen();
            ShowInTaskbar = true;
            Text = Text + " (" + cMain.selectedImages[0].Name + ")";
            Application.DoEvents();
        }

        private void frmCR_Shown(object sender, EventArgs e)
        {
            cMain.UpdateToolStripLabel(lblStatus, "Mounting WIM...");
            EnableMe(false);
            cmdStart.Enabled = false;

            Application.DoEvents();
            cMain.FreeRAM();

            sImage.Mount(lblStatus, this);
            if (string.IsNullOrEmpty(sImage.MountPath))
            {
                MErr = true;

                MessageBox.Show("Win Toolkit was not able to mount the wim file!", "Error");
                IMGChanged = true;
                Starting = false;
                Close();
                return;
            }

            ComponentClass = new cComponents(sImage);
            Application.DoEvents();
            DVDLoc = cMain.GetDVD(sImage.Location);

            BWStart.RunWorkerAsync();
            cMain.FreeRAM();

        }

        private void MouseScroll(object sender, MouseEventArgs e)
        {
            lstCL.Select();
        }
        bool FClosing;
        private void frmCR_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FClosing) { e.Cancel = true; return; }
            cMain.OnFormClosing(this);
            if (FClosing == false)
            {
                FClosing = true;
                if (BWRemove.IsBusy || Starting)
                {
                    e.Cancel = true;
                    MessageBox.Show("Please cancel the task first or wait for it to finish!", "Task in progress");
                    FClosing = false;
                    return;
                }

                if (IMGChanged == false)
                {
                    DialogResult DResult = MessageBox.Show("Are you sure you wish to close this tool?", "Are you sure?", MessageBoxButtons.YesNo);
                    if (DResult == DialogResult.No)
                    {
                        FClosing = false;
                        e.Cancel = true;
                        return;
                    }
                }

                if (MErr == false)
                {
                    cMain.FreeRAM();
                    cmdHelp.Visible = false;
                    cmdSelectAll.Visible = false;
                    cmdUnselectAll.Visible = false;
                    cmdStart.Visible = false;
                    cMain.UpdateToolStripLabel(lblStatus, "Hiding Packages...");
                    Application.DoEvents();
                    cReg.ShowPackages("", true, false, true, sImage);

                    cMain.UpdateToolStripLabel(lblStatus, "Preparing to dismount...");
                    Application.DoEvents();
                    cMount.CWIM_UnmountImage(sImage.Name, lblStatus, false, true, true, this, sImage.Location, true);

                    if (cMount.uChoice == cMount.MountStatus.None)
                    {
                        cmdHelp.Visible = true;
                        cmdSelectAll.Visible = true;
                        cmdUnselectAll.Visible = true;
                        cmdStart.Visible = true;
                        cMain.UpdateToolStripLabel(lblStatus, "Any item you select will be removed permanently!");
                        cmdStart.Text = "Remove Components";
                        cmdStart.Image = Properties.Resources.OK;
                        e.Cancel = true; FClosing = false;
                    }
                }
            }
        }

        private void frmCR_FormClosed(object sender, FormClosedEventArgs e)
        {
            cMain.ReturnME();
        }

        private void RemoveItem(string ItemName, string input)
        {
            string SR = cMain.RunExternal("\"" + DISM.Latest.Location + "\"", "/Image:\"" + sImage.MountPath + "\" /Remove-Package /PackageName:" + input);
            if (SR.ContainsIgnoreCase("0x"))
            {
                if (!SR.ContainsIgnoreCase("0x800f0805"))
                {
                    Err = Err + SR + Environment.NewLine;
                }
            }
            else
            {
                if (!Directory.Exists(sImage.MountPath + "\\Windows\\WinToolkit\\"))
                {
                    cMain.CreateDirectory(sImage.MountPath + "\\Windows\\WinToolkit\\");
                }
                try
                {
                    var SW = new StreamWriter(sImage.MountPath + "\\Windows\\WinToolkit\\Removed.txt", true);
                    SW.WriteLine(ItemName + Environment.NewLine);
                    SW.Close();
                }
                catch { }
            }
        }

        private bool IsChecked(string Entry)
        {
            ListViewItem LST = lstCL.FindItemWithText(Entry);
            if (LST == null) { return false; }
            return LST.Checked;
        }

        private void RemoveMetro(string ItemName, string input)
        {

            string SR = cMain.RunExternal("\"" + DISM.Latest.Location + "\"", "/Image:\"" + sImage.MountPath + "\" /Remove-ProvisionedAppxPackage /PackageName:" + input);
            if (SR.ContainsIgnoreCase("0x"))
            {
                Err = Err + SR + Environment.NewLine;
            }
            else
            {
                if (!Directory.Exists(sImage.MountPath + "\\Windows\\WinToolkit\\"))
                {
                    cMain.CreateDirectory(sImage.MountPath + "\\Windows\\WinToolkit\\");
                }
                try
                {
                    var SW = new StreamWriter(sImage.MountPath + "\\Windows\\WinToolkit\\Removed.txt", true);
                    SW.WriteLine(ItemName + Environment.NewLine);
                    SW.Close();
                }
                catch { }
            }
        }

        private void BWRemove_DoWork(object sender, DoWorkEventArgs e)
        {
            Err = "";

            int n = 1;

            //SoundThemes

            try
            {
                if (IsChecked("Sound Themes"))
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Removing Sound Themes...");
                    ComponentClass.RemoveSoundSchemes(true);
                }
            }
            catch
            {
            }

            cMain.FreeRAM();

            bool bRegLoad = false;

            foreach (ListViewItem I in lstCL.CheckedItems)
            {
                foreach (string C in I.SubItems[2].Text.Split(';'))
                {
                    if (!string.IsNullOrEmpty(C))
                    {
                        if (!bRegLoad)
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Loading Registry...");
                            Application.DoEvents();

                            bRegLoad = cReg.RegLoad("WIM_Software", sImage.MountPath + "\\Windows\\System32\\Config\\SOFTWARE");
                        }
                        if (I.Group.Header.StartsWithIgnoreCase("Metro"))
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Removing " + I.Text +  "...");
                            Application.DoEvents();
                            string path = sImage.MountPath + "\\Program Files\\WindowsApps\\" + I.SubItems[2].Text;
                            cMain.TakeOwnership(path);
                            Files.DeleteFolder(path,false);
                            cReg.DeleteKey(Registry.LocalMachine,@"WIM_Software\Classes\Local Settings\Software\Microsoft\Windows\CurrentVersion\AppModel\PackageRepository\Packages",I.SubItems[2].Text);
                            cReg.DeleteKey(Registry.LocalMachine, @"WIM_Software\Microsoft\Windows\CurrentVersion\Appx\AppxAllUserStore\Applications", I.SubItems[2].Text);

                        }
                        else
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "(" + PBRemove.Value + "/ " + PBRemove.Maximum + ") Finding: " + C.ReplaceIgnoreCase(";", ""));
                            cReg.ShowPackages(C.ReplaceIgnoreCase(";", ""), false, true, false, sImage);
                        }
                      
                    }
                }
            }
            if (bRegLoad)
            {
                cMain.UpdateToolStripLabel(lblStatus, "Unloading Registry...");
                Application.DoEvents();
                cReg.RegUnLoad("WIM_Software");
            }
            cMain.UpdateToolStripLabel(lblStatus, "Removing Components...");
            Application.DoEvents();
            foreach (ListViewItem I in lstCL.CheckedItems)
            {
                if (BWRemove.CancellationPending)
                {
                    return;
                }
                try
                {
                    if (I.Tag == null)
                    {
                        cMain.UpdateToolStripLabel(lblStatus, n + "\\" + lstCL.CheckedItems.Count + " - " + I.Text);

                        if (!I.Group.Header.StartsWithIgnoreCase("Metro"))
                        { 
                            var NRA = (from C in I.SubItems[2].Text.Split(';') where !string.IsNullOrEmpty(C) select C.ReplaceIgnoreCase(";", "")).ToList();

                            foreach (string S in NRA)
                            {
                                Application.DoEvents();
                                cMain.UpdateToolStripLabel(lblStatus, "(" + n + "/ " + PBRemove.Maximum + ") Removing: " + I.Text);
                                RemoveItem(I.Text, S);
                            }
                        }
                        n += 1;
                        I.Remove();
                    }
                    ColumnHeader1.Text = "Name [" + lstCL.CheckedItems.Count + " / " + lstCL.Items.Count + "]";
                }
                catch (Exception Ex)
                {
                    LargeError LE = new LargeError("Component Removal", "Error removing: " + I.Text, lblStatus.Text, Ex);
                    LE.Upload(); LE.ShowDialog();

                }
                cMain.UpdateToolStripLabel(lblStatus, "Finishing component removal");
                PBRemove.Value += 1;
                Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt16(PBRemove.Value),
                                                            Convert.ToUInt16(PBRemove.Maximum));
                cMain.FreeRAM();
            }

            ColumnHeader1.Text = "Name (" + lstCL.Items.Count + ")";

            if (BWRemove.CancellationPending)
                return;
            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Checking [Picture Samples]...");
                if (IsChecked("Picture Samples"))
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Removing Sample Pictures...");
                    Files.DeleteFolder(sImage.MountPath + "\\Users\\Public\\Pictures", false);
                }
            }
            catch (Exception ex)
            {
            }

            ColumnHeader1.Text = "Name (" + lstCL.Items.Count + ")";

            if (BWRemove.CancellationPending)
                return;
            Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
            Application.DoEvents();
            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Checking [Speech and Natural Language]...");
                if (IsChecked("Speech and Natural Language"))
                {
                    PBRemove.Value = 0;
                    cMain.UpdateToolStripLabel(lblStatus, "Removing 'Speech and Natural Language'...");
                    ComponentClass.Remove_SpeechLang(sImage.MountPath, false, lblStatus);
                }
            }
            catch
            {
            }

            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Checking [WinSXS Backup]...");
                if (IsChecked("Delete WinSXS\\Backup"))
                {
                    PBRemove.Value = 0;
                    cMain.UpdateToolStripLabel(lblStatus, "Removing WinSXS\\Backup folder..");
                    int WBt = Directory.GetFiles(sImage.MountPath + "\\Windows\\WinSXS\\Backup").Count();
                    PBRemove.Maximum = WBt;
                    Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
                    int WBn = 1;
                    foreach (string f in Directory.GetFiles(sImage.MountPath + "\\Windows\\WinSXS\\Backup"))
                    {
                        if (BWRemove.CancellationPending)
                            return;
                        try
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "(" + WBn + "/" + WBt + ") Removing WinSXS\\Backup\\" +
                                                  cMain.GetFName(f).Substring(0, 35));
                        }
                        catch (Exception ex)
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "(" + WBn + "/" + WBt + ") Removing WinSXS\\Backup\\" + cMain.GetFName(f));
                        }
                        cMain.TakeOwnership(f);
                        Files.DeleteFile(f);
                        try
                        {
                            WBn = WBn + 1;
                            PBRemove.Value += 1;
                            Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt16(PBRemove.Value),
                                                                        Convert.ToUInt16(PBRemove.Maximum));
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    if (BWRemove.CancellationPending)
                        return;
                    cMain.UpdateToolStripLabel(lblStatus, "Deleting WinSXS Backup folder");
                    cMain.TakeOwnership(sImage.MountPath + "\\Windows\\WinSXS\\Backup");
                    Files.DeleteFolder(sImage.MountPath + "\\Windows\\WinSXS\\Backup", false);
                    if (BWRemove.CancellationPending) { return; }

                    PBRemove.Value = 1;
                    cMain.FreeRAM();
                }
            }
            catch (Exception ex)
            {
            }

            if (BWRemove.CancellationPending)
                return;
            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Checking [DVD Support Folder]...");
                if (IsChecked("Delete 'Support' folder"))
                    Files.DeleteFolder(DVDLoc + "Support", false);
            }
            catch
            {
            }

            if (BWRemove.CancellationPending)
                return;
            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Checking [DVD Upgrade Folder]...");
                if (IsChecked("Delete 'Upgrade' folder"))
                    Files.DeleteFolder(DVDLoc + "Upgrade", false);
            }
            catch
            {
            }

            cMain.UpdateToolStripLabel(lblStatus, "Checking [Ease of Access Themes]...");
            if (IsChecked("Themes: Ease of Access Themes"))
            {
                cMain.UpdateToolStripLabel(lblStatus, "Removing 'East of Access Themes'...");

                ComponentClass.RemoveThemesEOA();
            }

            cMain.UpdateToolStripLabel(lblStatus, "Checking [Aero Theme]...");
            if (IsChecked("Themes: Aero Theme"))
            {
                cMain.UpdateToolStripLabel(lblStatus, "Removing 'Aero Theme'...");
                ComponentClass.RemoveThemesA();
            }

            cMain.UpdateToolStripLabel(lblStatus, "Checking [Non-Aero Themes]...");
            if (IsChecked("Themes: Non-Aero Themes"))
            {
                cMain.UpdateToolStripLabel(lblStatus, "Removing 'Non-Aero Themes'...");
                ComponentClass.RemoveThemesNA();
            }

            cMain.UpdateToolStripLabel(lblStatus, "Finishing background worker...");
        }

        private void BWStart_DoWork(object sender, DoWorkEventArgs e)
        {
            Starting = true;


            cmdStart.Enabled = false;
            cMain.UpdateToolStripLabel(lblStatus, "Making Packages Visible...");
            Application.DoEvents();
            cReg.ShowPackages("", false, false, true, sImage);

            cMain.UpdateToolStripLabel(lblStatus, "Getting Packages...");
            Application.DoEvents();
            cmdStart.Enabled = false;
            string GP = cMain.RunExternal("\"" + DISM.Latest.Location + "\"", "/Image:\"" + sImage.MountPath + "\" /Get-Packages /Format:Table");

            cMain.UpdateToolStripLabel(lblStatus, "Hiding Packages...");
            Application.DoEvents();

            cReg.ShowPackages("", true, false, true, sImage);

            cMain.UpdateToolStripLabel(lblStatus, "Listing Packages...");
            Application.DoEvents();
            //MessageBox.Show(GP);
            lstCL.BeginUpdate();

            ComponentClass.GetPackages(GP, lstCL, DVDLoc, lblStatus);

            if (sImage.Metro && DISM.Latest.Version >= DISM.Win8_0DISM)
            {
                cMain.UpdateToolStripLabel(lblStatus, "Checking for Metro Apps...");
                Application.DoEvents();

                //string mApps = cMain.RunExternal("\"" + DISM.Latest.Location + "\"", "/Image:\"" + sImage.MountPath + "\" /Get-ProvisionedAppxPackages /Format:Table");
                //cMain.UpdateToolStripLabel(lblStatus, "Listing Metro Apps...");
                //Application.DoEvents();
                bool m = cReg.RegLoad("WIM_Software", sImage.MountPath + "\\Windows\\System32\\Config\\SOFTWARE");
                if (m)
                {
                    ComponentClass.GetMetro(lstCL);
                    cReg.RegUnLoad("WIM_Software");
                }
            }

            cMain.UpdateToolStripLabel(lblStatus, "Finishing...");
            Application.DoEvents();

            lstCL.EndUpdate();
            lstCL.Visible = true;
            Application.DoEvents();
            ColumnHeader1.Width = -2;
            columnHeader6.Width = -2;
            columnHeader7.Width = -2;

            Application.DoEvents();
        }

        private void BWRemove_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
            PBRemove.Visible = false;
            cMain.FreeRAM();
            BWStart.RunWorkerAsync();
            Application.DoEvents();
        }

        private void BWStart_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EnableMe(true);
            cmdStart.Enabled = true;
            cmdStart.Visible = true;
            lstCL.Columns[0].Text = "Name [ " + lstCL.CheckedItems.Count + " / " + lstCL.Items.Count + "]";
            if (lstCL.Items.Count == 0)
            {
                var NINone = new ListViewItem();
                NINone.Text = "None";
                NINone.SubItems.Add("Everything has been removed");
                NINone.SubItems.Add("N/A");
                NINone.SubItems.Add("N/A");
                lstCL.Items.Add(NINone);
                lstCL.CheckBoxes = false;
                lstCL.ShowGroups = false;
                cmdStart.Enabled = false;
                lstCL.Enabled = false;
                cmdSelectAll.Enabled = false;
                cmdUnselectAll.Visible = false;
                cMain.UpdateToolStripLabel(lblStatus, "You're crazy! There is nothing left to remove. Good luck!");
                cmdStart.Visible = false;
                cmdSelectAll.Visible = false;
                cmdUnselectAll.Visible = false;
            }
            else
            {
                foreach (ListViewGroup LVG in lstCL.Groups)
                {
                    if (LVG.Header.ContainsIgnoreCase("("))
                    {
                        while (LVG.Header.ContainsIgnoreCase("("))
                        {
                            LVG.Header = LVG.Header.Substring(0, LVG.Header.Length - 1);
                        }
                        LVG.Header = LVG.Header.Substring(0, LVG.Header.Length - 1);
                    }
                    LVG.Header = LVG.Header + " (" + LVG.Items.Count + ")";

                }
                cMain.UpdateToolStripLabel(lblStatus, "Any item you select will be removed permanently!");
                cmdStart.Text = "Remove Components";
                cmdStart.Image = Properties.Resources.OK;
            }
            foreach (ColumnHeader C in lstCL.Columns)
            {
                C.Width = -2;
            }
            Starting = false;
            cMain.FreeRAM();
        }

        private void cmdSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstCL.Items)
            {
                item.Checked = true;
            }
            cMain.FreeRAM();
        }

        private void EnableMe(bool E)
        {
            lstCL.Enabled = E;
            cmdSelectAll.Visible = E;
            cmdUnselectAll.Visible = E;
            cmdHelp.Visible = E;
            cMain.FreeRAM();
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                 "Green: These removals have been tested and are safe to remove.\n\nYellow: Not tested (Caution)\n\nRed: Not Recommended to remove. These have been known to break other features.", "Help");
        }

        private void lstCL_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                string Dependants = null;
                switch (e.Item.Text)
                {
                    case "Internet Information Services":
                        Dependants = "*IIS Addons 1\n*IIS Addons 2" + Environment.NewLine +
                                         "*Microsoft .NET Framework 3.5.1";
                        break;
                    case "Media Features":
                        Dependants = "*Windows Media Center\n*Windows Media Player" +
                                         Environment.NewLine + "*Windows Media Player DVD Registration" +
                                         Environment.NewLine + "*Windows Media Player Network Sharing Service";
                        break;
                    case "Client Drivers":
                        Dependants = "*Common Drivers";
                        break;
                    case "Inbox Games":
                        Dependants = "*Internet Games\n*Premium Inbox Games";
                        break;
                    case "Music and Video Examples":
                        MessageBox.Show("This is needed for Windows Experience Index to function!", "Warning!");
                        return;
                }

                if (!string.IsNullOrEmpty(Dependants))
                {
                    DialogResult DResult =
                         MessageBox.Show(
                              "Removing \"" + e.Item.Text + "\" will also remove the following features:" +
                              Environment.NewLine + Environment.NewLine + Dependants + Environment.NewLine +
                              Environment.NewLine + "Are you sure you wish to remove this component?", "Are you sure?",
                              MessageBoxButtons.YesNo);
                    if (DResult != DialogResult.Yes)
                        e.Item.Checked = false;
                    if (DResult == DialogResult.Yes)
                    {
                        foreach (string I in Dependants.Split(Environment.NewLine.ToCharArray()))
                        {
                            if (!string.IsNullOrEmpty(I))
                            {
                                try
                                {
                                    string T = I;
                                    T = T.ReplaceIgnoreCase("*", "");
                                    ListViewItem LST = lstCL.FindItemWithText(T);
                                    if (LST != null)
                                    {
                                        LST.Checked = true;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }
            lstCL.Columns[0].Text = "Name [ " + lstCL.CheckedItems.Count + " / " + lstCL.Items.Count + "]";

        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            if (cmdStart.Text.EqualsIgnoreCase("Remove Components"))
            {
                DialogResult DR = MessageBox.Show("Once these components are removed, you can't restore them. You would need a fresh install of Windows. Would you like to continue?", "Caution", MessageBoxButtons.YesNo);
                if (DR != DialogResult.Yes) { return; }

                cmdStart.Text = "Stop";
                cmdStart.Image = Properties.Resources.Close;
                EnableMe(false);
                PBRemove.Value = 0;
                Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
                PBRemove.Maximum = lstCL.CheckedItems.Count;
                PBRemove.Visible = true;

                //WriteLogA(Ch);
                IMGChanged = true;

                BWRemove.RunWorkerAsync();
            }
            else
            {
                cmdStart.Enabled = false;
                cMain.UpdateToolStripLabel(lblStatus, "Stopping....");
                BWRemove.CancelAsync();
            }
            cMain.FreeRAM();
        }

        private void CMSsf_Click(object sender, EventArgs e)
        {
            try
            {

                string S = "";
                foreach (ListViewGroup LVG in lstCL.Groups)
                {
                    if (LVG.Items.Count == 0)
                        continue;
                    S += "[" + LVG.Header + "]" + Environment.NewLine;
                    S = LVG.Items.Cast<ListViewItem>().OrderBy(i => i.Text).Aggregate(S, (current, LVI) => current + (LVI.Text + ": " + LVI.SubItems[2].Text + Environment.NewLine));
                }
                if (string.IsNullOrEmpty(S))
                {
                    MessageBox.Show("There is nothing to save.", "Aborting");
                    return;
                }

                var SFD = new SaveFileDialog();
                SFD.Title = "Save log...";
                SFD.Filter = "Component List *.txt|*.txt";
                if (SFD.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                var SW = new StreamWriter(SFD.FileName);
                SW.Write(S);
                SW.Close();
            }
            catch
            {
            }
        }

        private void cmsCR_Opening(object sender, CancelEventArgs e)
        {
            cmdOpenPackageMum.Visible = false;
            if (lstCL.Items.Count == 0)
            {
                e.Cancel = true;
            }

            if (lstCL.SelectedItems.Count == 1)
            {
                string P = lstCL.SelectedItems[0].SubItems[2].Text;
                P = P.ReplaceIgnoreCase(";", "");
                P = sImage.MountPath + "\\Windows\\Servicing\\Packages\\" + P + ".mum";
                if (File.Exists(P))
                {
                    cmdOpenPackageMum.Visible = true;
                }
            }
        }

        private void cmdUnselectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstCL.Items)
            {
                item.Checked = false;
            }
            cMain.FreeRAM();
        }

        private void cmsSUI_Click(object sender, EventArgs e)
        {
            try
            {
                string S = "";
                foreach (ListViewGroup LVG in lstCL.Groups.Cast<ListViewGroup>().Where(h => h.Header.StartsWithIgnoreCase("Unknown (")))
                {
                    if (LVG.Items.Count == 0)
                        continue;
                    S += "[" + LVG.Header + "]" + Environment.NewLine;
                    S = LVG.Items.Cast<ListViewItem>().OrderBy(i => i.Text).Aggregate(S, (current, LVI) => current + (LVI.Text + ": " + LVI.SubItems[2].Text + Environment.NewLine));
                }
                if (string.IsNullOrEmpty(S))
                {
                    MessageBox.Show("There is nothing to save.", "Aborting");
                    return;
                }
                var SFD = new SaveFileDialog();
                SFD.Title = "Save log...";
                SFD.Filter = "Component List *.txt|*.txt";
                if (SFD.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                var SW = new StreamWriter(SFD.FileName);
                SW.Write(S);
                SW.Close();
            }
            catch
            {
            }
            cMain.FreeRAM();
        }

        private void cmdOpenPackageMum_Click(object sender, EventArgs e)
        {
            string P = lstCL.SelectedItems[0].SubItems[2].Text;
            P = P.ReplaceIgnoreCase(";", "");
            P = sImage.MountPath + "\\Windows\\Servicing\\Packages\\" + P + ".mum";
            if (File.Exists(P))
            {
                cMain.OpenProgram("\"" + cMain.SysFolder + "\\notepad.exe\"", P, false, System.Diagnostics.ProcessWindowStyle.Normal);
            }
        }

    }
}