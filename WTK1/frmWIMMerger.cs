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

namespace WinToolkit {
	public partial class frmWIMMerger : Form {
		public frmWIMMerger() {
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			CheckForIllegalCrossThreadCalls = false;
			FormClosing += frmWIMMerger_FormClosing;
			FormClosed += frmWIMMerger_FormClosed;
			BWMerge.RunWorkerCompleted += BWSplit_RunWorkerCompleted;

			lblWF.TextChanged += lblWF_TextChanged;
			Activated += cMain.FormActivated;
			Deactivate += cMain.FormActivated;
		}

		private void frmWIMMerger_Load(object sender, EventArgs e) {
            SplitContainer1.Scale4K(_4KHelper.Panel.Pan2);
			cMain.FormIcon(this); cMain.eLBL = lblStatus; cMain.eForm = this;
			cMain.ToolStripIcons(ToolStrip1);
			cMain.FreeRAM();
		}

		private void frmWIMMerger_FormClosing(object sender, FormClosingEventArgs e) {
			cMain.OnFormClosing(this);
			if (BWMerge.IsBusy) {
				e.Cancel = true;
				MessageBox.Show("You can't close this tool while a merge is in progress!", "Merge in Progress!");
			}
		}

		private void frmWIMMerger_FormClosed(object sender, FormClosedEventArgs e) {
			cMain.ReturnME();
		}

		private void BWSplit_DoWork(object sender, DoWorkEventArgs e) {
			 
			cMain.TakeOwnership(lblWF.Text);
			cMain.ClearAttributeFile(lblWF.Text);

			//imagex /ref source*.swm  /export source.swm 1 target.wim "UniqueName"
			cMain.OpenProgram("\"" + cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe") + "\"",
									"/ref \"" + lblWF.Text.ReplaceIgnoreCase( ".swm", "*.swm") + "\" /export \"" +
									lblWF.Text + "\" * \"" + lblOutput.Text + "\"", true, ProcessWindowStyle.Hidden);
		}

		private void BWSplit_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			cMain.FreeRAM();
			SplitContainer1.Enabled = true;
			cMain.UpdateToolStripLabel(lblStatus, "");
			if (cMain.AppErrC != 0) {
				if (cMain.AppErrC == -1073741510) {
					MessageBox.Show("You cancelled the merge", "Aborted");
				}
				else {
					MessageBox.Show("An error has occurred!", "Error (" + Convert.ToString(cMain.AppErrC) + ")");
				}

				string O = lblOutput.Text;
				while (!O.EndsWithIgnoreCase("\\")) {
					O = O.Substring(0, O.Length - 1);
				}

				foreach (string F in Directory.GetFiles(O, "*.swm")) {
					Files.DeleteFile(F);
				}
			}
		}

		private void cmdWF_Click(object sender, EventArgs e) {
			var OFD = new OpenFileDialog();
			OFD.Title = "Load SWM file...";
			OFD.Filter = "SWM File *.swm|*.swm";
			if (OFD.ShowDialog() != DialogResult.OK) {
				return;
			}
			string S = cMount.CWIM_GetWimInfo(OFD.FileName);

			if (string.IsNullOrEmpty(S)) {
				MessageBox.Show("Win Toolkit was unable to get the information it needed", "Aborting");
				return;
			}

			if (!S.ContainsIgnoreCase(": 1/")) {
				MessageBox.Show("This is not the main SWM file, please select the first part for this image. Also make sure the file is not corrupted.",
									 "Aborting");
				return;
			}

			lblWF.Text = OFD.FileName;
			try {
				var DI = new DriveInfo(lblWF.Text.Substring(0, 1));
				GroupBox1.Text = "SWM File :: " + DI.DriveType.ToString();
			}
			catch {
				GroupBox1.Text = "SWM File";
			}

			cMain.FreeRAM();
		}

		private void cmdOutput_Click(object sender, EventArgs e) {
			var SFD = new SaveFileDialog();
			SFD.Title = "Save WIM File...";
			SFD.Filter = "WIM File *.wim|*.wim";
			SFD.AutoUpgradeEnabled = true;
			if (SFD.ShowDialog() != DialogResult.OK) {
				return;
			}
			lblOutput.Text = SFD.FileName;
			cMain.FreeRAM();
		}

		private void cmdStart_Click(object sender, EventArgs e) {
			if (!lblWF.Text.ContainsIgnoreCase(".SWM")) {
				MessageBox.Show("You have not selected a valid wim file", "Invalid SWM");
				return;
			}

			if (!File.Exists(lblWF.Text)) {
				MessageBox.Show("Win Toolkit can't find the selected SWM file!", "Invalid SWM");
				return;
			}
			if (!lblOutput.Text.ContainsIgnoreCase("\\")) {
				MessageBox.Show("You have not selected a valid output path!", "Invalid Path");
				return;
			}
			cMain.UpdateToolStripLabel(lblStatus, "Merging...");
			SplitContainer1.Enabled = false;
			BWMerge.RunWorkerAsync();
		}

		private static string GetWIMValue(string L) {
			while (L.ContainsIgnoreCase(">")) {
				L = L.Substring(1);
			}
			return L;
		}

		private void lblWF_TextChanged(object sender, EventArgs e) {
			try {
				lstImages.Items.Clear();
				if (lblWF.Text.ToUpper().EndsWithIgnoreCase(".SWM")) {
					lblSize.Text = "Size: " + cMain.GetSize(lblWF.Text,true);
					try {
                        string WimInfo = cMount.CWIM_GetWimImageInfo(lblWF.Text,lblStatus);
						string Images = "";
						int cImages = 0;
						if (!string.IsNullOrEmpty(WimInfo)) {
							foreach (string mImage in Regex.Split(WimInfo, "<IMAGE INDEX=", RegexOptions.IgnoreCase)) {
								foreach (string mLine in mImage.Split('<')) {
									if (mLine.StartsWithIgnoreCase("NAME>")) {
										Images += GetWIMValue(mLine) + ", ";
										cImages += 1;
									}
								}
							}
							Images = Images.Substring(0, Images.Length - 2);
							lstImages.Items.Add(Images);
						}
					}
					catch {
						lstImages.Items.Add("Could not detect images in SWM File...");
					}
					try {
						var FI = new FileInfo(lblWF.Text);
						lblModified.Text = "Modified: " + FI.LastWriteTime.ToString(CultureInfo.InvariantCulture);
					}
					catch {
					}
				}
				else {
					lstImages.Items.Add("No Image Selected...");
					lblSize.Text = "Size: N/A";
					lblSize.Text = "Images: N/A";
					lblModified.Text = "Modified: N/A";
				}
			}
			catch {
			}

			cMain.FreeRAM();
		}

		private void lblWF_Click(object sender, EventArgs e) {
		}
	}
}