using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using WinToolkit.Classes;
using Microsoft.Win32;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;

namespace WinToolkit
{
    public partial class frmCaptureImg : Form
    {
        private string Err = "";
        private string nStatus = "";
        private bool busy = false;

        public frmCaptureImg()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            CheckForIllegalCrossThreadCalls = false;
        }

        private void frmCaptureImg_Load(object sender, EventArgs e)
        {
            cMain.openForms++;
            SplitContainer1.Scale4K(_4KHelper.Panel.Pan2);
            cMain.FormIcon(this); cMain.eLBL = lblStatus; cMain.eForm = this;
            cMain.ToolStripIcons(ToolStrip1);
            cboCompression.SelectedIndex = 2;
            cboFlags.SelectedIndex = 0;
            cboConfig.SelectedIndex = 0;
            chkVerify.Checked = cOptions.mVerify;
            chkCheck.Checked = cOptions.mCheck;

            cMain.FreeRAM();

            cMain.SetToolTip(RBN, "This will make a new image or replace an existing install.wim to only include the folder you're about to make.");
            cMain.SetToolTip(RBE, "This will add the folder you are about to capture to an existing install.wim");
            cMain.SetToolTip(chkRPFix, "Press the ? to find out more information", "NoRPFix");
            cMain.SetToolTip(cmdRPFixHelp, "Find out what NoRPFix is and why it is disable by default.", "What is NoRPFix?");
            cMain.SetToolTip(chkReArm,
                "Resets the amount Windows can be rearmed. Useful for when an image has been prepared a lot.");
            cMain.SetToolTip(chkVerify, "Checks the integrity of the .wim file.", "Verify");
            cMain.SetToolTip(chkCheck, "Enables file resource verification by checking for errors and file duplication.", "Check");
        }

        private void frmCaptureImg_FormClosing(object sender, FormClosingEventArgs e)
        {
            cMain.OnFormClosing(this);
            if (BWCapture.IsBusy)
            {
                MessageBox.Show("You can't close this tool while an image is being captured!", "Capture in Progress");
                e.Cancel = true;
            }


        }

        private void frmCaptureImg_FormClosed(object sender, FormClosedEventArgs e)
        {
            cMain.openForms--;
            //cMain.ReturnME();
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                chkReArm.Checked = false;
                chkReArm.Enabled = false;

                string FBD = cMain.FolderBrowserVista("Browse location to capture...", false, true);
                if (string.IsNullOrEmpty(FBD)) { return; }
                if (FBD == cMain.SysRoot.Substring(0, 3) || FBD == cMain.SysRoot)
                {
                    MessageBox.Show("You can't select your currently installed OS, it just won't work!", "Aborting");
                    return;
                }
                if (FBD.EndsWithIgnoreCase("\\")) { FBD = FBD.Substring(0, FBD.Length - 1); }

                lblFolder.Text = FBD;

                string sName = "", sArc = "";

                if (Directory.Exists(FBD + "\\Windows\\"))
                {

                    if (File.Exists(FBD + "\\Windows\\explorer.exe") && File.Exists(FBD + "\\Windows\\System32\\config\\system"))
                    {
                        var fi = FileVersionInfo.GetVersionInfo(FBD + "\\Windows\\explorer.exe");
                        if (fi.FileMajorPart == 6 && fi.FileMinorPart == 1)
                        {
                            chkReArm.Enabled = true;
                        }
                    }


                    foreach (string V in Directory.GetFiles(FBD + "\\Windows\\", "*.xml", SearchOption.TopDirectoryOnly))
                    {
                        if (!V.ToUpper().EndsWithIgnoreCase("STARTER.XML"))
                        {
                            sName = V.ReplaceIgnoreCase(".xml", "");
                            while (sName.ContainsIgnoreCase("\\")) { sName = sName.Substring(1); }

                        }
                    }

                    if (string.IsNullOrEmpty(sName) && File.Exists(FBD + "\\Windows\\Starter.xml")) { sName = "Starter"; }

                    if (!cboFlags.Items.Contains(sName)) { cboFlags.Items.Add(sName); }
                    cboFlags.Text = sName;

                    if (Directory.Exists(FBD + "\\Windows\\System32")) { sArc = " x86"; }
                    if (Directory.Exists(FBD + "\\Windows\\SysWOW64")) { sArc = " x64"; }

                    sName += sArc;
                    if (string.IsNullOrEmpty(txtName.Text)) { txtName.Text = sName; }
                    if (string.IsNullOrEmpty(txtDesc.Text)) { txtDesc.Text = sName; }



                }
            }
            catch (Exception Ex)
            {
                LargeError LE = new LargeError("Image Capture", "Unable to select the folder to capture.", lblStatus.Text, Ex);
                LE.Upload(); LE.ShowDialog();
            }




            cMain.FreeRAM();
        }

        private void cmdBrowseF_Click(object sender, EventArgs e)
        {
            try
            {
                if (RBN.Checked)
                {
                    var SFD = new SaveFileDialog();
                    SFD.Title = "Save *.wim file...";
                    SFD.Filter = "WIM File *.wim|*.wim";
                    if (SFD.ShowDialog() != DialogResult.OK) { return; }
                    lblWIM.Text = SFD.FileName;
                }
                else
                {
                    var OFD = new OpenFileDialog();
                    OFD.Title = "Find *.wim file...";
                    OFD.Filter = "WIM File *.wim|*.wim";
                    if (OFD.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    lblWIM.Text = OFD.FileName;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error");
            }
            cMain.FreeRAM();
        }

        private void RBE_Click(object sender, EventArgs e)
        {
            if (!busy)
            {
                cmdBrowseF.Text = "Browse";
                lblWIM.Text = "Select a WIM...";
            }
        }

        private void RBN_Click(object sender, EventArgs e)
        {
            if (!busy)
            {
                cmdBrowseF.Text = "Save";
                lblWIM.Text = "Save a new WIM...";
            }
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            if (!lblWIM.Text.ToUpper().EndsWithIgnoreCase(".WIM"))
            {
                MessageBox.Show("Please select/find a WIM file!", "Invalid WIM");
                return;
            }

            if (!lblFolder.Text.ContainsIgnoreCase(":"))
            {
                MessageBox.Show("Please select a folder to capture!", "Invalid Folder");
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please enter a valid name!", "Invalid Name");
                return;
            }
            if (string.IsNullOrEmpty(txtDesc.Text))
            {
                MessageBox.Show("Please enter a valid description!", "Invalid Description");
                return;
            }

            busy = true;
            Enabled(false);
            cmdStart.Enabled = false;

            cMain.FreeRAM();

            BWCapture.RunWorkerAsync();
        }

        private void CaptureImage(string Argument)
        {
            string sTempPath = cMain.UserTempPath + "\\WinToolkit_Imagex";
            cMain.FreeRAM();
            try
            {
                cMain.WriteResource(Properties.Resources.imagex, cMain.UserTempPath + "\\Files\\Imagex.exe", this);

                Files.DeleteFolder(sTempPath, true);
                var p = new Process();
                p.StartInfo.FileName = "\"" + cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe") + "\"";
                p.StartInfo.Arguments = Argument + " /Temp \"" + sTempPath + "\"";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.OutputDataReceived += OnDataReceived;
                p.Start();
                p.BeginOutputReadLine();
                p.WaitForExit();
                cMain.AppErrC = p.ExitCode;
                p.Close();
            }
            catch (Exception Ex)
            {
                string E = "";
                if (File.Exists(cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe")))
                {
                    E += "Imagex has been found." + Environment.NewLine;
                }
                else
                {
                    E += "Imagex has NOT been found." + Environment.NewLine;
                }
                if (File.Exists(lblWIM.Text))
                {
                    E += "WIM has been found. [" + lblWIM.Text + "]" + Environment.NewLine;
                }
                else
                {
                    E += "WIM has NOT been found. [" + lblWIM.Text + "]" + Environment.NewLine;
                }
                E += "New: " + RBN.Checked.ToString(CultureInfo.InvariantCulture);
                cMain.WriteLog(this, "Unable to run capture" + Environment.NewLine + E, Ex.Message, lblStatus.Text);

                cMain.UpdateToolStripLabel(lblStatus, "Deleting temp folder...");

                Files.DeleteFolder(sTempPath, false);
            }

        }

        private void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                if (e.Data.ContainsIgnoreCase("%"))
                {
                    string T = e.Data;
                    while (!T.EndsWithIgnoreCase("]"))
                    {
                        T = T.Substring(0, T.Length - 1);
                    }

                    cMain.UpdateToolStripLabel(lblStatus, nStatus + " Image " + T);
                }
                else
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        Err += e.Data + Environment.NewLine;
                    }
                }
            }
        }

        private void Enabled(bool enabled)
        {

            chkCheck.Enabled = enabled;
            chkVerify.Enabled = enabled;
            chkReArm.Enabled = enabled;
            chkRPFix.Enabled = enabled;
            cboCompression.Enabled = enabled;
            cboConfig.Enabled = enabled;
            cmdBrowseF.Enabled = enabled;
            cmdConfig.Enabled = enabled;
            pnRadio.Enabled = enabled;
            cmdBrowse.Enabled = enabled;
            txtName.Enabled = txtDesc.Enabled = cboFlags.Enabled = enabled;
            cmdCleanup.Visible = tsSepClean.Visible = enabled;
        }



        private void BWCapture_DoWork(object sender, DoWorkEventArgs e)
        {

            Err = "";

            string systemReg = lblFolder.Text + "\\Windows\\System32\\config\\SYSTEM";
            if (chkReArm.Checked && File.Exists(systemReg))
            {
                cMain.UpdateToolStripLabel(lblStatus, "Mounting Registry...");
                bool loaded = cReg.RegLoad("WIM_SYSTEM", systemReg);
                if (loaded)
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Deleting WPA...");
                    cReg.DeleteKey(Registry.LocalMachine, "WIM_SYSTEM", "WPA");

                    cMain.UpdateToolStripLabel(lblStatus, "Saving Registry...");
                    cReg.RegUnLoad("WIM_SYSTEM");
                }
            }

            if (RBN.Checked)
            {
                cMain.UpdateToolStripLabel(lblStatus, "Capturing Image...");
                nStatus = "Capturing";
                string C = "/Capture \"" + lblFolder.Text + "\" \"" + lblWIM.Text + "\" \"" +
                              txtName.Text + "\" \"" + txtDesc.Text + "\" /Compress " + cboCompression.Text +
                              " /Flags \"" + cboFlags.Text + "\"";

                if (chkRPFix.Checked)
                {
                    C += " /NORPFIX";
                }

                if (cOptions.mCheck)
                {
                    C += " /CHECK";
                }
                if (cOptions.mVerify)
                {
                    C += " /VERIFY";
                }

                if (cboConfig.SelectedIndex == 1)
                {
                    C += " /CONFIG \"" + cboConfig.SelectedText + "\"";
                }

                CaptureImage(C);

                if (cMain.AppErrC == -1073741510)
                {
                    Files.DeleteFile(lblWIM.Text);
                }
            }
            else
            {
                nStatus = "Amending";
                cMain.UpdateToolStripLabel(lblStatus, "Amending Image...");
                string A = "/Append \"" + lblFolder.Text + "\" \"" + lblWIM.Text + "\" \"" + txtName.Text + "\" \"" + txtDesc.Text + "\" /Compress " + cboCompression.Text + " /Flags \"" + cboFlags.Text + "\"";

                if (cOptions.mCheck || chkCheck.Checked)
                {
                    A += " /CHECK";
                }
                if (cOptions.mVerify || chkVerify.Checked)
                {
                    A += " /VERIFY";
                }

                if (cboConfig.SelectedIndex == 1)
                {
                    A += " /CONFIG \"" + cboConfig.SelectedText + "\"";
                }

                CaptureImage(A);
            }

            if (cMain.AppErrC != 0)
            {
                cMain.FreeRAM();
                if (cMain.AppErrC == -1073741510)
                {
                    MessageBox.Show("The operation was cancelled!", "Aborted!");
                }
                else
                {
                    if (Err.ContainsIgnoreCase("["))
                    {
                        while (!Err.StartsWithIgnoreCase("["))
                        {
                            Err = Err.Substring(1);
                        }
                    }

                    string Er = "An error has occurred";
                    if (cMain.AppErrC == 2)
                    {
                        Er = "An error has occurred, this error number usually means an image with the same name already exists! Try changing a new of an image in 'WIM Manager'.\n\nAlso make sure the wim file selected does not have a mounted image.";
                    }
                    MessageBox.Show(Er, "Error (" + cMain.AppErrC + ")");
                }
            }
            cMain.FreeRAM();
        }

        private void BWCapture_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Enabled(true);
            cmdStart.Enabled = true;
            cMain.UpdateToolStripLabel(lblStatus, "");
            cMain.FreeRAM();
            busy = false;
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            var SFD = new SaveFileDialog();
            SFD.Title = "Select Config File";
            SFD.Filter = "Config *.ini|*.ini";
            if (SFD.ShowDialog() != DialogResult.OK) { return; }

            if (cboConfig.Items.Count == 1)
            {
                cboConfig.Items.Add(SFD.FileName);
            }
            else
            {
                cboConfig.Items[1] = SFD.FileName;
            }

            cboConfig.SelectedIndex = 1;
        }

        private void cmdCleanup_Click(object sender, EventArgs e)
        {
            if (!lblFolder.Text.ContainsIgnoreCase(":"))
            {
                MessageBox.Show("Please select a folder to capture!", "Invalid Folder");
                return;
            }

            if (!Directory.Exists(lblFolder.Text + "\\Windows"))
            {
                MessageBox.Show("This is not a Windows installation", "Invalid Folder");
                return;
            }

            new frmCleanup(lblFolder.Text).ShowDialog();
        }

        private void cmdRPFixHelp_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://blogs.technet.com/b/zhou_minxiao/archive/2007/04/05/what-is-norpfix-switch-and-what-does-it-do.aspx");
        }


    }
}