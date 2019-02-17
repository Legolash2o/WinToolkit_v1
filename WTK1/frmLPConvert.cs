using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.Helpers;
using WinToolkit.Properties;

namespace WinToolkit {
	public partial class frmLPConvert : Form {
		private string FBD = "";

		public frmLPConvert() {
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			CheckForIllegalCrossThreadCalls = false;
			FormClosing += frmLPConvert_FormClosing;
			FormClosed += frmLPConvert_FormClosed;
			BWConvert.RunWorkerCompleted += BWConvert_RunWorkerCompleted;
			Activated += cMain.FormActivated;
			Deactivate += cMain.FormActivated;
			lstLP.DragDrop += DragDropData;
			lstLP.DragEnter += DragEnterData;
			MouseWheel += MouseScroll;
		}

		private void MouseScroll(object sender, MouseEventArgs e) {
			lstLP.Select();
		}

		private void ListAdd(IEnumerable<string> strList) {
			foreach (string strFilename in strList) {
				try {
					FileVersionInfo F = FileVersionInfo.GetVersionInfo(strFilename);
					if (strFilename.ToUpper().EndsWithIgnoreCase(".EXE") && lstLP.FindItemWithText(strFilename) == null &&
						 cMain.GetLPName(F.OriginalFilename) != F.OriginalFilename) {
						var NI = new ListViewItem();
						string FN = cMain.GetLPName(strFilename).Substring(0, cMain.GetLPName(strFilename).Length - 4);

						NI.Text = cMain.GetLPName(F.OriginalFilename);
						NI.SubItems.Add(cMain.GetSize(strFilename,true));
						NI.SubItems.Add("N/A");
						NI.SubItems.Add("N/A");
						NI.SubItems[3].Text = F.OriginalFilename;

						if (NI.SubItems[3].Text.ContainsIgnoreCase("X64") || strFilename.ContainsIgnoreCase("X64")) {
							NI.SubItems[2].Text = "x64";
						}
						if (NI.SubItems[3].Text.ContainsIgnoreCase("X86") || strFilename.ContainsIgnoreCase("X86")) {
							NI.SubItems[2].Text = "x86";
						}

						if (F.OriginalFilename.ContainsIgnoreCase("KB972813")) {
							NI.Text = NI.Text + " Win7 SP0";
						}
						if (F.OriginalFilename.ContainsIgnoreCase("KB2483139")) {
							NI.Text = NI.Text + " Win7 SP1";
						}
						if (F.ProductPrivatePart.ToString(CultureInfo.InvariantCulture).ContainsIgnoreCase("16386")) {
							NI.Text = NI.Text + " Vista";
						}
						if (F.ProductPrivatePart.ToString(CultureInfo.InvariantCulture).ContainsIgnoreCase("18037")) {
							NI.Text = NI.Text + " Vista SP2";
						}

						if (!NI.Text.ContainsIgnoreCase("X86") || !NI.Text.ContainsIgnoreCase("X64")) {
							NI.Text = NI.Text + " " + NI.SubItems[2].Text;
						}

						int e = 1 + lstLP.Items.Cast<ListViewItem>().Count(L => L.Text == NI.Text);

						if (e > 1) {
							NI.Text = NI.Text + " (" + e + ")";
						}

						NI.SubItems.Add(strFilename);
						NI.ToolTipText = strFilename;
						NI.Text = NI.Text.ReplaceIgnoreCase("N/A", cMain.RandomName(1, 10000));
						lstLP.Items.Add(NI);
						ColumnHeader1.Text = "Language (" + lstLP.Items.Count + ")";
					}
					else {
						MessageBox.Show(
							 "This does not appear to be a Language Pack!" + Environment.NewLine + Environment.NewLine +
							 "[" + strFilename + "]", "Invalid file");
					}
				}
				catch (Exception Ex) {
					MessageBox.Show(
						 "This does not appear to be a Language Pack!" + Environment.NewLine + Environment.NewLine + "[" +
						 strFilename + "]", "Invalid file");
				}
			}
			cmdSC.Visible = true;
			cmdRA.Visible = true;
		}

		private void frmLPConvert_Load(object sender, EventArgs e) {
            SplitContainer1.Scale4K(_4KHelper.Panel.Pan1);
		    SplitContainer2.Scale4K(_4KHelper.Panel.Pan2);
            cMain.FormIcon(this); cMain.eLBL = lblStatus; cMain.eForm = this;
			cMain.ToolStripIcons(ToolStrip1);
            lstLP.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}

		private void frmLPConvert_FormClosing(object sender, FormClosingEventArgs e) {
			cMain.OnFormClosing(this);
			if (BWConvert.IsBusy) {
				e.Cancel = true;
				MessageBox.Show("Conversion is in progress", "Abort");
			}
		}

		private void frmLPConvert_FormClosed(object sender, FormClosedEventArgs e) {
			cMain.ReturnME();
		}

		private void BWConvert_DoWork(object sender, DoWorkEventArgs e) {
			 
			cMain.UpdateToolStripLabel(lblStatus, "Starting...");

			cMain.WriteResource(Resources.exe2cab, cMain.UserTempPath + "\\exe2cab.exe", this);
			Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
			foreach (ListViewItem LST in lstLP.Items) {
				try {
					cMain.UpdateToolStripLabel(lblStatus, "Converting " + (LST.Index + 1) + " of " + lstLP.Items.Count + " - " + LST.Text +
										  "...");
					if (!File.Exists(FBD + "\\" + LST.Text + ".cab"))
						cMain.OpenProgram("\"" + cMain.UserTempPath + "\\exe2cab.exe\"", "\"" + LST.SubItems[4].Text + "\" \"" + FBD + "\\" + LST.Text + ".cab\"", true, ProcessWindowStyle.Hidden);

					lstLP.FindItemWithText(LST.Text).BackColor = File.Exists(FBD + "\\" + LST.Text + ".cab") ? Color.LightGreen : Color.LightPink;
					PB.Value++;
					Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt16(PB.Value), Convert.ToUInt16(PB.Maximum));
				}
				catch (Exception Ex) {
					LargeError LE = new LargeError("Language Pack Convertion", "Error converting language pack: " + LST.Text,lblStatus.Text, Ex);
					LE.Upload(); LE.ShowDialog();
				}
				if (BWConvert.CancellationPending)
					return;
			}

			Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
		}

		private void BWConvert_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			cMain.UpdateToolStripLabel(lblStatus, "Completed!");
			cmdSC.Text = "Start";
			cmdSC.Image = Properties.Resources.OK;
			PB.Visible = false;
			cmdAU.Visible = true;
			cmdRA.Visible = true;
			cmdRU.Visible = true;
		}

		private void cmdAU_Click(object sender, EventArgs e) {
			var OFD = new OpenFileDialog();
			OFD.Title = "Select Language Packs";
			OFD.Filter = "Microsoft Language Packs *.exe|*.exe";
			OFD.Multiselect = true;
			if (OFD.ShowDialog() != DialogResult.OK) {
				return;
			}
			lstLP.BeginUpdate();
			ListAdd(OFD.FileNames);
            lstLP.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			foreach (ColumnHeader CH in lstLP.Columns) {
				CH.Width = -2;
			}
			lstLP.EndUpdate();
		}

		private void DragEnterData(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void DragDropData(object sender, DragEventArgs e) {
			ListAdd((string[])e.Data.GetData(DataFormats.FileDrop, true));
		}

		private void cmdSC_Click(object sender, EventArgs e) {
            if (cmdSC.Text.EqualsIgnoreCase("Start"))
            {
				if (lstLP.Items.Count == 0) {
					MessageBox.Show("You need to add at least 1 language pack to the list!", "Invalid items");
					return;
				}
				FBD = cMain.FolderBrowserVista("Select CAB save location...", false, true);

				if (!string.IsNullOrEmpty(FBD)) {
					cmdRU.Visible = false;
					cmdAU.Visible = false;
					cmdRA.Visible = false;
					cmdSC.Text = "Cancel";
					cmdSC.Image = Properties.Resources.Close;
					PB.Value = 0;
					PB.Maximum = lstLP.Items.Count;
					PB.Visible = true;

					BWConvert.RunWorkerAsync();
				}
			}
			else {
				cmdSC.Enabled = false;
				cMain.UpdateToolStripLabel(lblStatus, "Stopping, please wait...");
				BWConvert.CancelAsync();
			}
		}

		private void cmdRA_Click(object sender, EventArgs e) {
			lstLP.Items.Clear();
			ColumnHeader1.Text = "Language (" + lstLP.Items.Count + ")";
		}

		private void cmdRU_Click(object sender, EventArgs e) {
			if (lstLP.SelectedItems.Count == 0) {
				MessageBox.Show("Please select a file", "Invalid File");
			}
			else {
				foreach (ListViewItem LST in lstLP.SelectedItems) {
					LST.Remove();
				}
			}
			ColumnHeader1.Text = "Language (" + lstLP.Items.Count + ")";
		}
	}
}