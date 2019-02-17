using System;
using System.CodeDom;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;
using WinToolkit.Prompts;
using WinToolkit.Properties;

namespace WinToolkit
{
    public partial class frmISOMaker : Form
    {
        public frmISOMaker()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            FormClosing += frmISO_FormClosing;
            FormClosed += frmISO_FormClosed;
            BWISO.RunWorkerCompleted += BWISO_RunWorkerCompleted;
            Activated += cMain.FormActivated;
            Deactivate += cMain.FormActivated;
            SetupToolips();
        }

        private void frmISO_Load(object sender, EventArgs e)
        {
            dtDate.MinDate = DateTime.Now.AddDays(-7);
            dtDate.MaxDate = DateTime.Now.AddYears(1);
            chkRebuild.Checked = cOptions.RebuildISOMaker;
            cMain.ToolStripIcons(ToolStrip1); cMain.eForm = this; cMain.eForm = this;
            cMain.FormIcon(this); cMain.eLBL = lblStatus; cMain.eForm = this;
            cboBoot.SelectedIndex = 0;
            CheckForIllegalCrossThreadCalls = false;

            if (!string.IsNullOrEmpty(cOptions.LastISO_Folder) && Directory.Exists(cOptions.LastISO_Folder) && lblFolder.Text.EqualsIgnoreCase("No folder selected..."))
            {
                lblFolder.Text = cOptions.LastISO_Folder;
            }

            if (!string.IsNullOrEmpty(cOptions.LastISO_ISO) && cOptions.LastISO_ISO.ToUpper().EndsWithIgnoreCase(".ISO")) { lblISO.Text = cOptions.LastISO_ISO; }

            SplitContainer1.Scale4K(_4KHelper.Panel.Pan2);
            scDate.Scale4K(_4KHelper.Panel.Pan2);
            Application.DoEvents();

        }

        private void SetupToolips()
        {
            cMain.SetToolTip(cboBoot, "This will set the boot image depending on what motherboard you will be using.\nIt is not recommended to change this option unless you really need to.", groupBox5.Text);
            cMain.SetToolTip(chkRebuild, "When an image has been modified. It is sometimes required to rebuild the image so that it decreases in size.", "Rebuild Image");
            cMain.SetToolTip(chkDebug, "If you're having issues with the ISO creation process.\nEnable this to display the command windows progress at the end. This will heopfully display the error.", "Debug Mode");
            UpdateDate();
            cMain.SetToolTip(chkCMDWindow, "This will show the command prompt windows showing the progress (%).\nWarning: Closing the window will abort the iso creation process.", "Show Progress (%)");
            cMain.SetToolTip(cmdFBrowse, "Select the folder you wish to turn into an ISO", "Select Folder");
            cMain.SetToolTip(cmdIBrowse, "Select the path where your new ISO will be saved.", "ISO Location");
            cMain.SetToolTip(txtName, "Enter the label for your new ISO.", "ISO Label");
            cMain.SetToolTip(rbCustomDate, "Select a date which all the files within the ISO will have.", "Custom Date");
            cMain.SetToolTip(rbInstallWIM, "Set all the files within the ISO to the same as the install.wim modified date.", "Install.wim Date");
            cMain.SetToolTip(chkCustomDate, "Set all the files within the ISO to a specified date.");
        }

        private void frmISO_FormClosed(object sender, FormClosedEventArgs e)
        {
            cMain.ReturnME();
        }

        private void frmISO_FormClosing(object sender, FormClosingEventArgs e)
        {
            cMain.OnFormClosing(this);
            if (BWISO.IsBusy)
            {
                MessageBox.Show("Can't close while ISO is being created!", "ISO creation in progress",
                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private void cmdFBrowse_Click(object sender, EventArgs e)
        {
            string F = cMain.FolderBrowserVista("Browse ISO Folder...", false, true);
            if (!string.IsNullOrEmpty(F))
            {
                lblFolder.Text = F;
            }


            cOptions.LastISO_Folder = lblFolder.Text;
            cOptions.SaveSettings();
            Application.DoEvents();
        }

        private void cmdIBrowse_Click(object sender, EventArgs e)
        {
            var SFD = new SaveFileDialog();
            SFD.Title = "New ISO File";
            SFD.Filter = "ISO *.iso|*.iso";

            string F = lblFolder.Text;
            SFD.InitialDirectory = F;
            if (!string.IsNullOrEmpty(cOptions.LastISO_ISO))
            {
                if (Directory.Exists(cOptions.LastISO_ISO)) { SFD.InitialDirectory = cOptions.LastISO_ISO; }
                if (File.Exists(cOptions.LastISO_ISO))
                {
                    string F1 = cOptions.LastISO_ISO;
                    string F2 = cMain.GetFName(F1);

                    while (!F1.EndsWithIgnoreCase("\\")) { F1 = F1.Substring(0, F1.Length - 1); }

                    SFD.InitialDirectory = F1;
                    SFD.FileName = F2;
                }
            }
            else
            {
                SFD.InitialDirectory = F;
            }

            if (SFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            lblISO.Text = SFD.FileName;
            cOptions.LastISO_ISO = SFD.FileName;
            cOptions.SaveSettings();
            Application.DoEvents();
        }

        private void cboBoot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBoot.Text.EqualsIgnoreCase("Custom"))
            {
                var OFD = new OpenFileDialog();
                OFD.Title = "Select boot.bin";
                OFD.Filter = "Boot File *.bin|*.bin|Any File *.*|*.*";
                if (OFD.ShowDialog() != DialogResult.OK)
                {
                    cboBoot.SelectedIndex = 0;
                }
                else
                {
                    lblBoot.Text = OFD.FileName;
                }
            }
            else
            {
                lblBoot.Text = "No custom boot file selected...";
            }
            Application.DoEvents();
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (cmdStart.Text.EqualsIgnoreCase("Create ISO"))
            {
                if (lblFolder.Text.EqualsIgnoreCase("No folder selected..."))
                {
                    MessageBox.Show("You have selected an invalid folder to make an image from!", "Invalid Folder");
                    return;
                }
                if (lblISO.Text.EqualsIgnoreCase("No location selected..."))
                {
                    MessageBox.Show("You have selected an invalid place to save an ISO!", "Invalid ISO Location");
                    return;
                }
                if (cboBoot.Text.EqualsIgnoreCase("Custom") &&
                    lblBoot.Text.EqualsIgnoreCase("No custom boot file selected..."))
                {
                    MessageBox.Show("Please select a valid custom boot image!", "Invalid Boot Image");
                    return;
                }
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Please enter a valid name for your ISO!", "Invalid ISO Name");
                    return;
                }

                if (File.Exists(lblISO.Text))
                {
                    DialogResult DR =
                        MessageBox.Show(
                            "Are you sure you wish to overwrite the existing ISO image?" + Environment.NewLine +
                            Environment.NewLine + "[" + lblISO.Text + "]", "ISO already exists",
                            MessageBoxButtons.YesNo);
                    if (DR != DialogResult.Yes)
                    {
                        return;
                    }
                }

                try
                {
                    cOptions.RebuildISOMaker = chkRebuild.Checked;
                    var DI = new DriveInfo(lblISO.Text.Substring(0, 1));

                    long OSize = Directory.GetFiles(lblFolder.Text, "*", SearchOption.AllDirectories)
                        .Select(F => new FileInfo(F)).Select(FI => FI.Length).Sum();

                    if (DI.AvailableFreeSpace < OSize)
                    {
                        MessageBox.Show(
                            "You need at least " + cMain.BytesToString(OSize) + " free space to make an ISO!",
                            "Not enough space!");
                        return;
                    }
                }
                catch (Exception Ex)
                {
                }

                if (chkei.Checked)
                {
                    WriteEI();
                }

                SplitContainer1.Panel1.Enabled = false;
                cOptions.LastISO_Folder = lblFolder.Text;
                cOptions.LastISO_ISO = lblISO.Text;
                cmdStart.Text = "Cancel";
                cmdStart.Image = Properties.Resources.Close;
                Application.DoEvents();
                BWISO.RunWorkerAsync();
            }
            else
            {
                cmdStart.Enabled = false;
                cMain.UpdateToolStripLabel(lblStatus, "Stopping...");
                BWISO.CancelAsync();
                cMain.KillProcess("imagex");
                cMain.KillProcess("cdimage");
                Files.DeleteFile(lblISO.Text);
                Application.DoEvents();
            }
        }

        private void BWISO_DoWork(object sender, DoWorkEventArgs e)
        {
            bool readOnly = cMain.IsReadOnly(lblFolder.Text + "\\");

            if (!readOnly)
            {
                try
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Preparing UEFI boot...");
                    Application.DoEvents();

                    if (Directory.Exists(lblFolder.Text + "\\efi\\microsoft\\boot") &&
                        !Directory.Exists(lblFolder.Text + "\\efi\\boot"))
                    {
                        cMain.CopyDirectory(lblFolder.Text + "\\efi\\microsoft\\boot",
                                 lblFolder.Text + "\\efi\\boot", false, true);
                        cMain.WriteResource(Resources.bootx64,
                            lblFolder.Text + "\\efi\\boot\\bootx64.efi", this);
                    }
                }
                catch (Exception Ex) { new SmallError("Unable to copy UEFI files.", Ex, lblFolder.Text).Upload(); }

                try
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Preparing Image Rebuild...");
                    Application.DoEvents();
                    if (chkRebuild.Checked && File.Exists(lblFolder.Text + "\\sources\\install.wim") && BWISO.CancellationPending == false)
                    {
                        cMount.Rebuild(lblFolder.Text + "\\sources\\install.wim", lblStatus, this, false);
                    }
                }
                catch { }
            }

            cMain.UpdateToolStripLabel(lblStatus, "Preparing Commands...");
            Application.DoEvents();

            if (BWISO.CancellationPending == false)
            {
                string C = "-L\"" + txtName.Text + "\" -m -o -u2 -udfver102 -h ";

                string efiFile = $"{lblFolder.Text}\\efi\\Microsoft\\boot\\efisys.bin";
                string biosFile = $"{lblFolder.Text}\\boot\\etfsboot.com";

                switch (cboBoot.Text)
                {
                    case "BIOS":
                        C = C + $"-b\"{biosFile}\"";
                        cMain.WriteResource(Resources.BIOS, biosFile, this);
                        break;
                    case "UEFI":
                        C = C + $"-b\"{efiFile}\" -pEF";
                        cMain.WriteResource(Resources.efisys, efiFile, this);
                        break;
                    case "BIOS and UEFI":
                        C = C + $"-bootdata:2#p0,e,b\"{biosFile}\"#pEF,e,b\"{efiFile}\"";
                        cMain.WriteResource(Resources.efisys, efiFile, this);
                        cMain.WriteResource(Resources.BIOS, biosFile, this);
                        break;
                    case "Custom":
                        C = C + "-b\"" + lblBoot.Text + "\"";
                        break;
                    case "None":
                        break;
                }

                C = C + " \"" + lblFolder.Text + "\"";
                C = C + " \"" + lblISO.Text + "\"";

                if (chkCustomDate.Checked)
                {
                    C = C + " -t" + UpdateDate();
                }

                cMain.WriteResource(Resources.cdimage, cMain.UserTempPath + "\\cdimage.exe", this);
                cMain.UpdateToolStripLabel(lblStatus, "Creating ISO...");
                Application.DoEvents();

                if (chkDebug.Checked)
                {
                    string D = cMain.RunExternal("\"" + cMain.UserTempPath + "\\cdimage.exe\"", C);
                    cMain.ErrorBox("You requested some debug information.", "Debug Results", D);
                }
                else
                {
                    bool showCMD = !chkDebug.Checked && chkCMDWindow.Checked;
                    cMain.OpenProgram("\"" + cMain.UserTempPath + "\\cdimage.exe\"", C, true,
                                            showCMD
                                                 ? ProcessWindowStyle.Normal
                                                 : ProcessWindowStyle.Hidden);

                    if (cMain.AppErrC != 0)
                    {
                        MessageBox.Show(
                             "ISO creation was cancelled!" + Environment.NewLine + Environment.NewLine + "[cdimage.exe " +
                             C + "]", "Aborted (" + cMain.AppErrC + ")");
                    }
                }
            }

            Application.DoEvents();
        }

        private void BWISO_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SplitContainer1.Panel1.Enabled = true;
            cmdStart.Enabled = true;
            cMain.UpdateToolStripLabel(lblStatus, "");
            cmdStart.Text = "Create ISO";
            cmdStart.Image = Properties.Resources.OK;
            Application.DoEvents();
            if (cMain.AppErrC == 0)
            {
                MessageBox.Show("Your ISO has now been created!", "Completed");
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text.Length > 30)
                {
                    txtName.Text = txtName.Text.Substring(0, 30);
                }
                Application.DoEvents();
            }
            catch
            {
            }
        }

        private void lblFolder_TextChanged(object sender, EventArgs e)
        {

            if (lblFolder.Text != "No folder selected...")
            {
                if (Uri.IsWellFormedUriString(lblFolder.Text, UriKind.Absolute))
                {
                    MessageBox.Show("This does not seem to be a valid path.", "Invalid Path");
                    return;
                }

                GroupBox2.Text = "Folder [Calculating...]";
                Application.DoEvents();
                if (lblFolder.Text.EndsWithIgnoreCase("\\"))
                {
                    lblFolder.Text = lblFolder.Text.Substring(0, lblFolder.Text.Length - 1);
                }

                if (File.Exists(lblFolder.Text + "\\sources\\install.wim"))
                {
                    chkRebuild.Enabled = true;
                    if (chkCustomDate.Checked)
                    {
                        rbInstallWIM.Enabled = true;
                        rbCustomDate.Enabled = true;
                    }
                }
                else
                {
                    chkRebuild.Enabled = false;
                    if (chkCustomDate.Checked)
                    {
                        rbInstallWIM.Enabled = false;
                        rbCustomDate.Checked = true;
                    }
                }
                try
                {
                    var DI = new DriveInfo(lblFolder.Text.Substring(0, 1));
                    GroupBox2.Text = "Folder [" + DI.DriveType + " :: " + cMain.GetSize(lblFolder.Text, true) + "]";
                }
                catch { }

                cboBoot.Items.Clear();
                if (cMain.IsReadOnly(lblFolder.Text + "\\"))
                {
                    chkRebuild.Enabled = false;
                    chkRebuild.Checked = false;
                }
                else
                {
                    chkRebuild.Checked = true;
                    cboBoot.Items.Add("BIOS and UEFI");
                    cboBoot.Items.Add("UEFI");
                }

                cboBoot.Items.Add("BIOS");
                cboBoot.Items.Add("Custom");
                cboBoot.Items.Add("None");

                cboBoot.SelectedIndex = 0;

                if (!Directory.Exists(lblFolder.Text + "\\sources") && !Directory.Exists(lblFolder.Text + "\\boot"))
                    cboBoot.SelectedIndex = cboBoot.Items.Count - 1;

                if (string.IsNullOrEmpty(txtName.Text))
                {
                    txtName.Text = new DirectoryInfo(lblFolder.Text).Name;
                }
            }
        }

        private void lblISO_TextChanged(object sender, EventArgs e)
        {
            if (lblISO.Text != "No location selected...")
            {
                try
                {
                    var DI = new DriveInfo(lblISO.Text.Substring(0, 1));
                    GroupBox3.Text = "ISO Output [" + DI.DriveType + " :: FreeSpace: " +
                                          cMain.BytesToString(DI.AvailableFreeSpace) + "]";
                }
                catch
                {
                    GroupBox3.Text = "ISO Output";
                }
            }
        }

        private string UpdateDate()
        {
            string newDate = "";
            if (File.Exists(lblFolder.Text + "\\sources\\install.wim") && rbInstallWIM.Checked)
            {
                chkei.Enabled = true;
                FileInfo file1 = new System.IO.FileInfo(lblFolder.Text + "\\sources\\install.wim");
                newDate = file1.LastWriteTime.ToString("MM/dd/yyyy,H:mm:ss");

                if (!File.Exists(lblFolder.Text + "\\sources\\ei.cfg"))
                    chkei.Checked = true;
            }
            else
            {
                chkei.Enabled = false;
                newDate = dtDate.SelectionStart.ToString("MM/dd/yyyy") + "," + dtTime.Value.ToString("H:mm:ss");
            }

            gbDate.Text = "Custom Date: " + newDate.ReplaceIgnoreCase(",", " ");
            return newDate;
        }

        private void dtDate_DateChanged(object sender, DateRangeEventArgs e)
        {
            UpdateDate();
        }

        private void dtTime_ValueChanged(object sender, EventArgs e)
        {
            UpdateDate();
        }

        private void WriteEI()
        {
            using (StreamWriter sw = new StreamWriter(lblFolder.Text + "\\sources\\ei.cfg", false))
            {
                sw.WriteLine("[EditionID]");
                sw.WriteLine("");
                sw.WriteLine("[Channel]");
                sw.WriteLine("OEM");
                sw.WriteLine("[VL]");
                sw.WriteLine("0");
            }

        }

        private void chkDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDebug.Checked)
            {
                chkCMDWindow.Enabled = false;
                gbProgress.Text = "Progress [Debug Mode Enabled]";
            }
            else
            {
                chkCMDWindow.Enabled = true;
                gbProgress.Text = "Progress";
            }
        }

        private void lblFolder_Click(object sender, EventArgs e)
        {

        }

        private void chkCustomDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCustomDate.Checked)
            {
                rbCustomDate.Enabled = true;

                if (File.Exists(lblFolder.Text + "\\sources\\install.wim"))
                {
                    rbInstallWIM.Enabled = true;
                }
                else
                {
                    rbInstallWIM.Enabled = false;
                    rbCustomDate.Checked = true;
                }
            }
            else
            {
                rbCustomDate.Enabled = false;
                rbInstallWIM.Enabled = false;
            }
            UpdateDate();
        }

        private void rbInstallWIM_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCustomDate.Checked)
            {
                gbDate.Enabled = true;
            }
            else
            {
                gbDate.Enabled = false;
            }
            UpdateDate();
        }

        private void rbCustomDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCustomDate.Checked)
            {
                gbDate.Enabled = true;
            }
            else
            {
                gbDate.Enabled = false;
            }
            UpdateDate();
        }


    }
}