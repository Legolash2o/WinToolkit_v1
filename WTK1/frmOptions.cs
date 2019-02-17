using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WinToolkit.Classes;
using WinToolkit.Classes.Helpers;

namespace WinToolkit
{
    public partial class frmOptions : Form
    {
        private bool Changed, Loaded, Saved;

        public frmOptions()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void CheckSettings()
        {
            foreach (ListViewItem item in lstOptions.Items)
            {
                switch (Convert.ToString(item.Tag))
                {
                    case "chkUELogs":
                        item.Checked = cOptions.UploadLogs;
                        break;
                    case "chkQMerge":
                        item.Checked = cOptions.QuickMerge;
                        break;
                    case "chkDLogging":
                        item.Checked = cOptions.DLogging;
                        break;
                    case "chkAIOSave":
                        item.Checked = cOptions.AIOSave;
                        break;
                    case "chkUpdates":
                        item.Checked = cOptions.CheckForUpdates;
                        break;
                    case "chkmCheck":
                        item.Checked = cOptions.mCheck;
                        break;
                    case "chkmRunOnce":
                        item.Checked = cOptions.AddRunOnce;
                        break;
                    case "chkmVerify":
                        item.Checked = cOptions.mVerify;
                        break;
                    case "chkMountLog":
                        item.Checked = cOptions.MountLog;
                        break;
                    case "chkAIOPM":
                        item.Checked = cOptions.PresetManager;
                        break;
                    case "chkMDeleteMount":
                        item.Checked = cOptions.DeleteMount;
                        break;
                    case "chkFreeRAM":
                        item.Checked = cOptions.FreeRAM;
                        break;
                    case "chkRLogging":
                        item.Checked = cOptions.RegistryLog;
                        break;
                    case "chkPreventSleep":
                        item.Checked = cOptions.PreventSleep;
                        break;
                    case "chkAV":
                        item.Checked = cOptions.AVScan;
                        break;
                    case "chkDISM":
                        item.Checked = cOptions.DISMUpdate;
                        break;
                    case "chkUpdCatFilter":
                        item.Checked = cOptions.UpdFilter;
                        break;
                    case "chkGNetwork":
                        if (cReg.GetValue(Microsoft.Win32.Registry.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", "EnableLinkedConnections").EqualsIgnoreCase("1"))
                        {
                            item.Checked = true;
                        }
                        break;
                }
                item.SubItems[1].Text = Convert.ToString(item.Checked);
            }

            switch (cOptions.ScaleOptions)
            {
                default:
                    cboScaling.SelectedIndex = 0;
                    break;
                case 1:
                    cboScaling.SelectedIndex = 1;
                    break;
                case 2:
                    cboScaling.SelectedIndex = 2;
                    break;
                case 3:
                    cboScaling.SelectedIndex = 3;
                    break;
                case 4:
                    cboScaling.SelectedIndex = 4;
                    break;

            }

            chkACGeneric.Checked = cOptions.AutoClean;
            chkACLogs.Checked = cOptions.AutoCleanLogs;
            chkACSXS.Checked = cOptions.AutoCleanSXS;
            chkACManifest.Checked = cOptions.AutoCleanManCache;

            chkT.Checked = cOptions.TransparencyAll;
            CheckPriority(cboPriWinToolkit, cOptions.WinToolkitPri);
            CheckPriority(cboPriExt, cOptions.WinToolkitExt);
            CheckPriority(cboPriDISM, cOptions.WinToolkitDISM);
            txtSoLoR.Text = cOptions.SolDownload;
            txtWinToolkitTemp.Text = cOptions.WinToolkitTemp;
            txtMountTemp.Text = cOptions.MountTemp;
            UpdateDism();

            int Trans = Convert.ToInt16(cOptions.Transparency);
            if (Trans >= HSBO.Minimum)
            {
                HSBO.Value = Trans;
            }
            else
            {
                HSBO.Value = HSBO.Minimum;
            }
        }

        private void UpdateDism()
        {
            lstDISM.Items.Clear();
            foreach (DISM.DismFile D in DISM.All)
            {
                ListViewItem n = new ListViewItem();
                n.UseItemStyleForSubItems = false;
                n.Text = D.Version.ToString();
                n.SubItems.Add(D.Location);
                n.ToolTipText = D.Location;
                switch (D.Type)
                {
                    case DISM.DismType.Custom:
                        n.Group = lstDISM.Groups[0];
                        break;
                    case DISM.DismType.System:
                        n.Group = lstDISM.Groups[1];
                        break;
                    default:
                        n.Group = lstDISM.Groups[2];
                        break;

                }
                lstDISM.Items.Add(n);
            }
        }

        private void CheckPriority(ComboBox CB, String P)
        {
            switch (P)
            {
                case "RealTime":
                    CB.SelectedIndex = 0;
                    break;
                case "High":
                    CB.SelectedIndex = 1;
                    break;
                case "AboveNormal":
                    CB.SelectedIndex = 2;
                    break;
                case "Normal":
                    CB.SelectedIndex = 3;
                    break;
                case "BelowNormal":
                    CB.SelectedIndex = 4;
                    break;
                case "Idle":
                    CB.SelectedIndex = 5;
                    break;
            }
        }

        private void frmOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            cMain.ReturnME();
        }

        private bool CheckChanges()
        {
            bool changes = false;
            foreach (ListViewItem item in lstOptions.Items)
            {
                if (item.Font.Bold)
                {
                    changes = true;
                    if (item.Text.EqualsIgnoreCase("Show Network Drives"))
                    {
                        MessageBox.Show("This option won't take effect until you restart your computer.", "Show Network Drives");
                    }
                }
            }
            if (chkT.Font.Bold)
            {
                changes = true;
            }
            if (txtMountTemp.Text != cOptions.MountTemp) { changes = true; }
            if (txtWinToolkitTemp.Text != cOptions.WinToolkitTemp) { changes = true; }
            if (txtSoLoR.Text != cOptions.SolDownload) { changes = true; }
            if (cboPriDISM.Text != cOptions.WinToolkitDISM) { changes = true; }
            if (cboPriExt.Text != cOptions.WinToolkitExt) { changes = true; }
            if (cboPriWinToolkit.Text != cOptions.WinToolkitPri) { changes = true; }
            if (HSBO.Value != cOptions.Transparency) { changes = true; }

            return changes;
        }

        private void frmOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            cMain.OnFormClosing(this, false);
            Changed = CheckChanges();

            if (Changed && Saved == false)
            {
                DialogResult DResult = MessageBox.Show("Settings have been changed, do you wish to save them?",
                                                                    "Changed Settings", MessageBoxButtons.YesNoCancel);
                switch (DResult)
                {
                    case DialogResult.Yes:
                        e.Cancel = true;
                        cmdApply.PerformClick();
                        break;
                    case DialogResult.No:
                        Saved = true;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            Loaded = false;
            cboLang.SelectedIndex = 0; cMain.eForm = this;

            if (cOptions.ValidKey || Debugger.IsAttached)
            {
                gbCleanup.Enabled = true;
                lblNoticeClean.Visible = false;
                gbCleanup.Text = "Auto Clean";
            }

            

            cMain.FormIcon(this);
            cMain.ToolStripIcons(ToolStrip1);
            cMain.ToolStripIcons(toolStrip2);

            tbCPUDism.Maximum = Environment.ProcessorCount;
            tbCPUDism.Value = tbCPUDism.Maximum;
            gbDISMAff.Text = "DISM Affinity [" + tbCPUDism.Value + "]";

            CheckSettings();
            cMain.SetToolTip(cboPriWinToolkit, "Select what mode Win Toolkit process runs. This is the same as opening Task Manager and setting a process 'Priority'.\n*Increasing the priority may speed up Win Toolkit, but may also slow down all other apps.\n*Lowering the priority may slow down Win Toolkit, but may speed up all other apps.", "Win Toolkit Priority");
            cMain.SetToolTip(cboPriDISM, "Select what mode Win Toolkit process runs. This is the same as opening Task Manager and setting a process 'Priority'.\n*Increasing the priority may speed up DISM, but may also slow down all other apps.\n*Lowering the priority may slow down DISM, but may speed up all other apps.", "Win Toolkit Priority");
            cMain.SetToolTip(cboPriExt, "Select what mode Win Toolkit process runs. This is the same as opening Task Manager and setting a process 'Priority'.\n*Increasing the priority may speed up other processes started by Win Toolkit, but may also slow down all other apps.\n*Lowering the priority may slow down other processes started by Win Toolkit, but may speed up all other apps.", "Win Toolkit Priority");

            cMain.SetToolTip(cmdMountBrowse, "Select where you want Win Toolkit to mount your images.", "Image Mount Path");
            cMain.SetToolTip(cmdTempBrowse, "Select where you want Win Toolkit to store temporary files. For example, extracted files.", "Win Toolkit Temp Folder");
            cMain.SetToolTip(cmdSoLoRB, "Select where you want 'Update Catalog' updates to download to.", "Update Catalog");
            Loaded = true;

            SplitContainer1.Scale4K(_4KHelper.Panel.Pan2);
            splitContainer2.Scale4K(_4KHelper.Panel.Pan1);
            splitContainer3.Scale4K(_4KHelper.Panel.Pan1);

            lstDISM.Columns[0].Width = -1;
            lstDISM.Columns[1].Width = -2;
        }

        private void MouseScroll(object sender, MouseEventArgs e)
        {
            if (TabControl1.SelectedTab.Text.EqualsIgnoreCase("Main")) { lstOptions.Select(); }
        }

        private void HSBO_Scroll(object sender, EventArgs e)
        {
            if (chkT.Checked)
            {
                Opacity = Convert.ToDouble(HSBO.Value) / 100;
            }
            else
            {
                Opacity = 1;
            }
        }

        private void cmdMountRD_Click(object sender, EventArgs e)
        {
            txtMountTemp.Text = "Please Wait...";
            txtMountTemp.Text = cMain.DetectLargestDrive(cMain.SysDrive, 26843545600) + "WinToolkit_Mount";
        }

        private void cmdTempRD_Click(object sender, EventArgs e)
        {
            var mDI = new DriveInfo(cMain.SysDrive);
            txtWinToolkitTemp.Text = cMain.UserTempPath;
        }

        private void cmdTempBrowse_Click(object sender, EventArgs e)
        {
            string FBD = cMain.FolderBrowserVista("Select Temp Folder...", true, true);

            if (string.IsNullOrEmpty(FBD))
            {
                return;
            }

            if (FBD.ContainsForeignCharacters())
            {
                MessageBox.Show("The selected temp path has invalid characters and can't be used.", "Foreign characters detected");
                return;
            }

            try
            {
                DriveInfo DI = new DriveInfo(FBD);

                if (!DI.IsReady || DI.DriveType != DriveType.Fixed)
                {
                    throw new Exception("Invalid Drive");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("You seem to have selected an invalid drive. Please make sure that the drive is ready and fixed (not removable).", "Invalid Drive");
                return;
            }

            if (FBD.Length < 4)
            {
                FBD = FBD.Substring(0, 1) + ":\\WinToolkit_Temp";
            }

            if (!FBD.ToUpper().EndsWithIgnoreCase("\\WINTOOLKIT_TEMP"))
            {
                FBD += "\\WinToolkit_Temp";
            }

            if (FBD.StartsWithIgnoreCase(txtMountTemp.Text))
            {
                MessageBox.Show("Your temp folder is in the same directory as your mount folder. This will cause errors!" + Environment.NewLine + Environment.NewLine + "Mount: " + txtMountTemp.Text + Environment.NewLine + "Temp: " + FBD, "Invalid Directory");
                return;
            }

            if (FBD.ContainsForeignCharacters())
            {
                MessageBox.Show("This mount path has foreign characters (non-English) which can greatly affect the All-In-One Integrator, please chose something simple such as 'C:\\Temp\\WinToolkit'.", "Invalid Location");
                return;
            }
            txtWinToolkitTemp.Text = FBD;
        }

        private void cmdMountBrowse_Click(object sender, EventArgs e)
        {
            string FBD = cMain.FolderBrowserVista("Select Mount Folder...", true, true);

            if (string.IsNullOrEmpty(FBD))
            {
                return;
            }
            if (FBD.Length < 4)
            {
                FBD = FBD.Substring(0, 1) + ":\\Mount";
            }

            if (txtWinToolkitTemp.Text.StartsWithIgnoreCase(FBD))
            {
                MessageBox.Show("Your temp folder is in the same directory as your mount folder. This will cause errors!" + Environment.NewLine + Environment.NewLine + "Mount: " + FBD + Environment.NewLine + "Temp: " + txtWinToolkitTemp.Text, "Invalid Directory");
                return;
            }

            try
            {
                var DI = new DriveInfo(FBD.Substring(0, 1));
                if (!DI.IsReady || DI.DriveType != DriveType.Fixed || DI.DriveFormat != "NTFS")
                {
                    MessageBox.Show("The path you have selected is not supported, please check the following:\n\n*The drive is fixed and non-removable.USB sticks can't be used.\n*The drive format is NTFS\n*The drive is ready.", "Invalid Location");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("The path you have selected is not supported!", "Invalid Location");
                return;
            }

            if (FBD.ContainsForeignCharacters())
            {
                MessageBox.Show("This mount path has foreign characters (non-English) which can greatly affect the All-In-One Integrator, please choose something simple such as 'C:\\WinToolkit_Mount'.", "Invalid Location");
                return;
            }
            txtMountTemp.Text = FBD;
        }

        private void txtWinToolkitTemp_TextChanged(object sender, EventArgs e)
        {
            txtWinToolkitTemp.Font = txtWinToolkitTemp.Text != cOptions.WinToolkitTemp ? new Font(txtWinToolkitTemp.Font, FontStyle.Bold) : new Font(txtWinToolkitTemp.Font, FontStyle.Regular);
        }

        private void txtMountTemp_TextChanged(object sender, EventArgs e)
        {
            txtMountTemp.Font = txtMountTemp.Text != cOptions.MountTemp ? new Font(txtMountTemp.Font, FontStyle.Bold) : new Font(txtMountTemp.Font, FontStyle.Regular);
        }

        private void lstOptions_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (Loaded)
            {
                e.Item.Font = e.Item.Checked != Convert.ToBoolean(e.Item.SubItems[1].Text) ? new Font(e.Item.Font, FontStyle.Bold) : new Font(e.Item.Font, FontStyle.Regular);
            }
        }

        private void chkT_CheckedChanged(object sender, EventArgs e)
        {
            chkT.Font = chkT.Checked != cOptions.TransparencyAll ? new Font(chkT.Font, FontStyle.Bold) : new Font(chkT.Font, FontStyle.Regular);
            if (chkT.Checked)
            {
                Opacity = Convert.ToDouble(HSBO.Value) / 100;
                HSBO.Enabled = true;
            }
            else
            {
                Opacity = 1;
                HSBO.Enabled = false;
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Changed = CheckChanges();

            if (Changed && Saved == false)
            {
                DialogResult DResult = MessageBox.Show("All the new settings you have selected will not be saved!",
                                                                    "Are you sure?", MessageBoxButtons.YesNo);
                if (DResult != DialogResult.Yes)
                {
                    return;
                }
            }
            Saved = true;
            Close();
        }

        private void cmdUndo_Click(object sender, EventArgs e)
        {
            Changed = CheckChanges();

            if (Changed == false)
            {
                MessageBox.Show("There is nothing to undo!", "N/A");
                return;
            }

            DialogResult DResult = MessageBox.Show("Are you sure you wish to undo everything you have changed?", "Are you sure?", MessageBoxButtons.YesNo);
            if (DResult == DialogResult.Yes)
            {
                foreach (ListViewItem I in lstOptions.Items)
                {
                    I.Checked = Convert.ToBoolean(I.SubItems[1].Text);
                }
                txtMountTemp.Text = cOptions.MountTemp;
                txtWinToolkitTemp.Text = cOptions.WinToolkitTemp;
                txtSoLoR.Text = cOptions.SolDownload;
                cboPriDISM.Text = cOptions.WinToolkitDISM;
                cboPriExt.Text = cOptions.WinToolkitExt;
                cboPriWinToolkit.Text = cOptions.WinToolkitPri;
                chkT.Checked = cOptions.TransparencyAll;
                HSBO.Value = Convert.ToInt16(cOptions.Transparency);
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstOptions.Items)
            {
                switch (Convert.ToString(item.Tag))
                {
                    case "chkQMerge":
                        cOptions.QuickMerge = item.Checked;
                        break;
                    case "chkUELogs":
                        cOptions.UploadLogs = item.Checked;
                        break;
                    case "chkDLogging":
                        cOptions.DLogging = item.Checked;
                        break;
                    case "chkAIOSave":
                        cOptions.AIOSave = item.Checked;
                        break;
                    case "chkUpdates":
                        cOptions.CheckForUpdates = item.Checked;
                        break;
                    case "chkmRunOnce":
                        cOptions.AddRunOnce = item.Checked;
                        break;
                    case "chkmCheck":
                        cOptions.mCheck = item.Checked;
                        break;
                    case "chkmVerify":
                        cOptions.mVerify = item.Checked;
                        break;
                    case "chkMountLog":
                        cOptions.MountLog = item.Checked;
                        break;
                    case "chkAIOPM":
                        cOptions.PresetManager = item.Checked;
                        break;
                    case "chkFreeRAM":
                        cOptions.FreeRAM = item.Checked;
                        break;
                    case "chkMDeleteMount":
                        cOptions.DeleteMount = item.Checked;
                        break;
                    case "chkRLogging":
                        cOptions.RegistryLog = item.Checked;
                        break;
                    case "chkDISM":
                        cOptions.DISMUpdate = item.Checked;
                        break;
                    case "chkAV":
                        cOptions.AVScan = item.Checked;
                        break;
                    case "chkPreventSleep":
                        cOptions.PreventSleep = item.Checked;
                        break;
                    case "chkUpdCatFilter":
                        cOptions.UpdFilter = item.Checked;
                        break;
                    case "chkGNetwork":
                        if (item.Checked)
                        {
                            cReg.WriteValue(Microsoft.Win32.Registry.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", "EnableLinkedConnections", 1, Microsoft.Win32.RegistryValueKind.DWord);
                        }
                        else
                        {
                            cReg.DeleteValue(Microsoft.Win32.Registry.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", "EnableLinkedConnections");
                        }
                        break;
                }
            }

            switch (cboScaling.SelectedIndex)
            {
                default:
                    cOptions.ScaleOptions = 0;
                    break;
                case 1:
                    cOptions.ScaleOptions = 1;
                    break;
                case 2:
                    cOptions.ScaleOptions = 2;
                    break;
                case 3:
                    cOptions.ScaleOptions = 3;
                    break;
                case 4:
                    cOptions.ScaleOptions = 4;
                    break;

            }
            cOptions.TransparencyAll = chkT.Checked;

            cOptions.WinToolkitPri = cboPriWinToolkit.Text;
            cOptions.WinToolkitExt = cboPriExt.Text;
            cOptions.WinToolkitDISM = cboPriDISM.Text;
            cOptions.WinToolkitTemp = txtWinToolkitTemp.Text;
            cOptions.MountTemp = txtMountTemp.Text;
            cOptions.SolDownload = txtSoLoR.Text;
            cOptions.Transparency = HSBO.Value;

            cOptions.AutoClean = chkACGeneric.Checked;
            cOptions.AutoCleanLogs = chkACLogs.Checked;
            cOptions.AutoCleanSXS = chkACSXS.Checked;
            cOptions.AutoCleanManCache = chkACManifest.Checked;

            cOptions.DISMLoc = "";
            if (DISM.All.Any(d => d.Type == DISM.DismType.Custom))
            {
                cOptions.DISMLoc = DISM.All.Where(p => p.Type == DISM.DismType.Custom).Aggregate("", (current, S) => current + (S.Location + "|"));
            }

            switch (cOptions.ScaleOptions)
            {
                default:
                    cboScaling.SelectedIndex = 0;
                    break;
                case 1:
                    cboScaling.SelectedIndex = 1;
                    break;
                case 2:
                    cboScaling.SelectedIndex = 2;
                    break;
                case 4:
                    cboScaling.SelectedIndex = 3;
                    break;

            }

            cMain.PreventSleep(cOptions.PreventSleep);

            switch (cboPriWinToolkit.Text)
            {
                case "RealTime":
                    Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
                    break;
                case "High":
                    Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
                    break;
                case "AboveNormal":
                    Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.AboveNormal;
                    break;
                case "Normal":
                    Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Normal;
                    break;
                case "BelowNormal":
                    Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;
                    break;
                case "Idle":
                    Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Idle;
                    break;
            }
            Saved = true;
            cOptions.SaveSettings();
            Close();
        }

        private void cmdSoLoRD_Click(object sender, EventArgs e)
        {
            txtSoLoR.Text = cMain.Root + "UCatalog\\";
        }

        private void cmdSoLoRB_Click(object sender, EventArgs e)
        {
            string FBD = cMain.FolderBrowserVista("Set 'Update Catalog' download folder...", true, true);

            if (string.IsNullOrEmpty(FBD))
            {
                return;
            }
            txtSoLoR.Text = FBD;
        }

        private void cmsAddDISM(object sender, EventArgs e)
        {
            BrowseDISM();
        }

        private void cmdRemoveDISM_Click(object sender, EventArgs e)
        {
            if (lstDISM.SelectedItems.Count == 0)
            {
                MessageBox.Show("You need to select at least item!", "Invalid Selection");
                return;
            }

            int i = lstDISM.SelectedItems.Cast<ListViewItem>().Count(c => c.Group != lstDISM.Groups[0]);
            if (i > 0)
            {
                MessageBox.Show("You can only remove custom DISM files!", "Invalid Selection");
                return;
            }

            foreach (ListViewItem LST in lstDISM.SelectedItems)
            {
                DISM.Delete(LST.SubItems[1].Text);
            }

            UpdateDism();
        }

     

        private void tbCPUDism_Scroll(object sender, EventArgs e)
        {
            gbDISMAff.Text = "DISM Affinity [" + tbCPUDism.Value + "]";
        }

     
        private void txtSoLoR_TextChanged(object sender, EventArgs e)
        {
            txtSoLoR.Font = txtSoLoR.Text != cOptions.SolDownload ? new Font(txtSoLoR.Font, FontStyle.Bold) : new Font(txtSoLoR.Font, FontStyle.Regular);
        }

        private void BrowseDISM()
        {
            var OFD = new OpenFileDialog();

            OFD.Filter = "DISM *.exe|Dism.exe";
            OFD.Title = "DISM";

            if (OFD.ShowDialog() != DialogResult.OK) { return; }

            FileVersionInfo F = FileVersionInfo.GetVersionInfo(OFD.FileName);
            string description = F.FileDescription;
            string version = F.FileVersion;

            var regEx = new Regex(@"\d+.\d+.\d+.\d+", RegexOptions.IgnoreCase);
            if (string.IsNullOrEmpty(version) || string.IsNullOrEmpty(description) || !regEx.IsMatch(version))
            {
                MessageBox.Show("You've selected an invalid or corrupt file.", "Aborting");
                return;
            }

            if (description != "Dism Image Servicing Utility" && !description.ContainsIgnoreCase("DISM"))
            {
                DialogResult DR = MessageBox.Show("This does not seem to be DISM.\n\n[" + description + "]\n\nWould you like to add it anyway?", "Invalid DISM", MessageBoxButtons.YesNo);
                if (DR != DialogResult.Yes) { return; }
            }

            if (DISM.Count > 0 &&
                  DISM.All.Count(d => String.Equals(d.Location, OFD.FileName, StringComparison.CurrentCultureIgnoreCase)) >
                  0)
            {
                MessageBox.Show("The specified item already exists in the database.", "Duplicate");
                return;
            }

            DISM.Add(OFD.FileName);
            UpdateDism();
            //txtDISM.Text = OFD.FileName;
        }

    }
}