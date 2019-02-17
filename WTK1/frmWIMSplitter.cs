using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;

namespace WinToolkit
{
    public partial class frmWIMSplitter : Form
    {
        public frmWIMSplitter()
        { 
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            CheckForIllegalCrossThreadCalls = false;
            FormClosing += frmWIMSplitter_FormClosing;
            FormClosed += frmWIMSplitter_FormClosed;
            BWSplit.RunWorkerCompleted += BWSplit_RunWorkerCompleted;
            BWRebuild.RunWorkerCompleted += BWRebuild_RunWorkerCompleted;
            lblWF.TextChanged += lblWF_TextChanged;
            Activated += cMain.FormActivated;
            Deactivate += cMain.FormActivated;
        }

        private void frmWIMSplitter_Load(object sender, EventArgs e)
        {
            SplitContainer1.Scale4K(_4KHelper.Panel.Pan2);
            cMain.FormIcon(this); cMain.eLBL = lblStatus; cMain.eForm = this;
            cMain.ToolStripIcons(ToolStrip1);
            cboSize.SelectedIndex = 0;
            cMain.FreeRAM();
        }

        private void frmWIMSplitter_FormClosing(object sender, FormClosingEventArgs e)
        {
            cMain.OnFormClosing(this);
            if (BWSplit.IsBusy)
            {
                e.Cancel = true;
                MessageBox.Show("You can't close this tool while a split is in progress!", "Split in Progress!");
            }
        }

        private void frmWIMSplitter_FormClosed(object sender, FormClosedEventArgs e)
        {
            cMain.ReturnME();
        }

        private void BWSplit_DoWork(object sender, DoWorkEventArgs e)
        {

            cMain.TakeOwnership(lblWF.Text);
            cMain.ClearAttributeFile(lblWF.Text);
            cMain.OpenProgram("\"" + cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe") + "\"", "/Split \"" + lblWF.Text + "\" \"" + lblOutput.Text + "\" " + txtSize.Text, true, ProcessWindowStyle.Hidden);
        }

        private void BWSplit_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cMain.FreeRAM();
            SplitContainer1.Enabled = true;
            cMain.UpdateToolStripLabel(lblStatus, "");
            if (cMain.AppErrC != 0)
            {
                if (cMain.AppErrC == -1073741510)
                {
                    MessageBox.Show("You cancelled the splitting", "Aborted");
                }
                else
                {
                    MessageBox.Show("An error has occurred!", "Error (" + Convert.ToString(cMain.AppErrC) + ")");
                }

                string O = lblOutput.Text;
                while (!O.EndsWithIgnoreCase("\\"))
                {
                    O = O.Substring(0, O.Length - 1);
                }

                foreach (string F in Directory.GetFiles(O, "*.swm"))
                {
                    Files.DeleteFile(F);
                }
            }
        }

        private void txtSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cMain.NumericOnly(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmdWF_Click(object sender, EventArgs e)
        {
            var OFD = new OpenFileDialog();
            OFD.Title = "Browse WIM file...";
            OFD.Filter = "WIM File *.wim|*.wim";
            if (OFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string SWM = cMount.CWIM_GetWimInfo(OFD.FileName);
            if (!SWM.ContainsIgnoreCase(": 1/1"))
            {
                MessageBox.Show(
                     "This seems to be an *.swm file which is read-only and not supported by WinToolkit. You need to merge this back into an *.wim file first!",
                     "SWM Detected!");
                return;
            }
            lblWF.Text = OFD.FileName;
            try
            {
                var DI = new DriveInfo(lblWF.Text.Substring(0, 1));
                GroupBox1.Text = "WIM File :: " + DI.DriveType.ToString();
            }
            catch
            {
                GroupBox1.Text = "WIM File";
            }

            cMain.FreeRAM();
        }

        private void cmdOutput_Click(object sender, EventArgs e)
        {
            var SFD = new SaveFileDialog();
            SFD.Title = "Save SWM File...";
            SFD.Filter = "SWM File *.swm|*.swm";
            SFD.AutoUpgradeEnabled = true;
            if (SFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            lblOutput.Text = SFD.FileName;
            cMain.FreeRAM();
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            if (!lblWF.Text.ContainsIgnoreCase(".WIM"))
            {
                MessageBox.Show("You have not selected a valid wim file", "Invalid WIM");
                return;
            }

            if (!File.Exists(lblWF.Text))
            {
                MessageBox.Show("Win Toolkit can't find the selected WIM file!", "Invalid WIM");
                return;
            }
            if (!lblOutput.Text.ContainsIgnoreCase("\\"))
            {
                MessageBox.Show("You have not selected a valid output path!", "Invalid Path");
                return;
            }
            cMain.UpdateToolStripLabel(lblStatus, "Splitting...");
            SplitContainer1.Enabled = false;
            BWSplit.RunWorkerAsync();
        }

        private static string GetWIMValue(string L)
        {
            while (L.ContainsIgnoreCase(">"))
            {
                L = L.Substring(1);
            }
            return L;
        }

        private void lblWF_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lstImages.Items.Clear();
                if (lblWF.Text.ToUpper().EndsWithIgnoreCase(".WIM"))
                {
                    lblSize.Text = "Size: " + cMain.GetSize(lblWF.Text, true);
                    try
                    {
                        string WimInfo = cMount.CWIM_GetWimImageInfo(lblWF.Text,lblStatus);
                        int cImages = 0;
                        if (!string.IsNullOrEmpty(WimInfo))
                        {
                            foreach (string mImage in Regex.Split(WimInfo, "<IMAGE INDEX=", RegexOptions.IgnoreCase))
                            {
                                foreach (string mLine in mImage.Split('<'))
                                {
                                    if (mLine.StartsWithIgnoreCase("NAME>"))
                                    {
                                        lstImages.Items.Add(GetWIMValue(mLine));
                                        cImages += 1;
                                    }
                                }
                            }

                        }
                    }
                    catch
                    {
                        lstImages.Items.Add("Could not detect images in SWM File...");
                    }
                    try
                    {
                        var FI = new FileInfo(lblWF.Text);
                        lblModified.Text = "Modified: " + FI.LastWriteTime.ToString(CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                    }
                    try
                    {
                        var DI = new DriveInfo(lblWF.Text.Substring(0, 1));
                        if (DI.DriveType != DriveType.CDRom)
                        {
                            cmdRebuild.Enabled = true;
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    cmdRebuild.Enabled = false;
                    lstImages.Items.Add("No Image Selected...");
                    lblSize.Text = "Size: N/A";
                    lblSize.Text = "Images: N/A";
                    lblModified.Text = "Modified: N/A";
                }

                var FIS = new FileInfo(lblWF.Text);
                if (FIS.Length < 734003200)
                {
                    cboSize.SelectedIndex = 3;
                    txtSize.Text = cMain.BytesToString(FIS.Length / 2, false);
                }
            }
            catch
            {
            }

            cMain.FreeRAM();
        }

        private void cmdRebuild_Click(object sender, EventArgs e)
        {
            SplitContainer1.Panel1.Enabled = false;
            cmdStart.Enabled = false;
            if (cmdRebuild.Text.EqualsIgnoreCase("Rebuild"))
            {
                cMain.FreeRAM();
                cmdRebuild.Text = "Cancel";
                if (BWRebuild.IsBusy == false)
                {
                    BWRebuild.RunWorkerAsync();
                }
            }
            else
            {
                BWRebuild.CancelAsync();
                cmdRebuild.Enabled = false;
                cMain.KillProcess("Imagex");
            }
        }

        private void BWRebuild_DoWork(object sender, DoWorkEventArgs e)
        {
            cMount.Rebuild(lblWF.Text, lblStatus, this, true);
        }

        private void BWRebuild_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cMain.FreeRAM();
            cmdRebuild.Enabled = true;
            cmdRebuild.Text = "Rebuild";
            cMain.UpdateToolStripLabel(lblStatus, "");
            SplitContainer1.Panel1.Enabled = true;
            cmdStart.Enabled = true;
        }

        private void cboSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSize.ReadOnly = true;
            txtSize.Enabled = false;
            switch (cboSize.Text)
            {
                case "CD - 700MB":
                    txtSize.Text = "700";
                    break;
                case "DVD - 4.7GB":
                    txtSize.Text = "4500";
                    break;
                case "DVD-DL - 8GB":
                    txtSize.Text = "7600";
                    break;
                case "Custom":
                    txtSize.ReadOnly = false;
                    txtSize.Enabled = true;
                    txtSize.Text = "600";
                    break;
            }
        }

        private void lblWF_Click(object sender, EventArgs e)
        {
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
        }
    }
}