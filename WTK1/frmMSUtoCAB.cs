using System;
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

namespace WinToolkit {
	public partial class frmMSUtoCAB : Form {
		private bool Cancelled;
		private string FBD = "";
		private const string strLine = "";

		private string uError = ""; private int UC;

		public frmMSUtoCAB() {
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			CheckForIllegalCrossThreadCalls = false;
			FormClosing += frmMSUtoCAB_FormClosing;
			FormClosed += frmMSUtoCAB_FormClosed;
			BWConvert.RunWorkerCompleted += BWConvert_RunWorkerCompleted;
			Activated += cMain.FormActivated;
			Deactivate += cMain.FormActivated;
			MouseWheel += MouseScroll;
		}

		private void AddToList(string Filename) {
			if (lstMSU.FindItemWithText(Filename) == null && cMain.GetSize(Filename, false) != "0") {
				string uArc = "";
				string uName = Path.GetFileNameWithoutExtension(Filename);

				var NI = new ListViewItem();
				lstMSU.Sorting = SortOrder.None;

				if (string.IsNullOrEmpty(uArc)) {
					uArc = "??";
					if (Filename.ContainsIgnoreCase("X64")) {
						uArc = "amd64";
					}
					if (Filename.ContainsIgnoreCase("X86")) {
						uArc = "x86";
					}
				}
                if (uArc.EqualsIgnoreCase("amd64")) { uArc = "x64"; }

				if (uName.ContainsIgnoreCase("KB982861") && uArc != "??") { uName = "Internet Explorer 9 " + uArc; }
				NI.Text = uName;
				NI.SubItems.Add(cMain.GetSize(Filename,true));

				NI.SubItems.Add(uArc);
				NI.SubItems.Add(Filename);
				NI.SubItems.Add(cMain.MD5CalcFile(Filename));
				lstMSU.Items.Add(NI);
			}

		}

		private void frmMSUtoCAB_Load(object sender, EventArgs e) {
		    SplitContainer1.Scale4K(_4KHelper.Panel.Pan1);
		    SplitContainer2.Scale4K(_4KHelper.Panel.Pan2);
            cMain.FormIcon(this); cMain.eLBL = lblStatus; cMain.eForm = this;
			cMain.ToolStripIcons(ToolStrip1);
		    lstMSU.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

		private void MouseScroll(object sender, MouseEventArgs e) {
			lstMSU.Select();
		}

		private void frmMSUtoCAB_FormClosing(object sender, FormClosingEventArgs e) {
			cMain.OnFormClosing(this);
			if (BWConvert.IsBusy) {
				e.Cancel = true;
				MessageBox.Show("You can't close this window while conversion is still in progress!", "Task in Progress");
			}
			else {
				Cancelled = true;
			}
		}

		private void frmMSUtoCAB_FormClosed(object sender, FormClosedEventArgs e) {
			cMain.ReturnME();
		}

		private void BWConvert_DoWork(object sender, DoWorkEventArgs e) {

			PB.Value = 0;
			Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt16(PB.Value), Convert.ToUInt16(lstMSU.Items.Count));
			var N = new System.Collections.ArrayList();
			foreach (string F in Directory.GetFiles(FBD, "*.*", SearchOption.TopDirectoryOnly)) {
				N.Add(cMain.GetFName(F, false));
			}
			foreach (ListViewItem LST in lstMSU.Items) {
				string cTo = FBD + "\\" + LST.Text + ".cab";



				if (!File.Exists(cTo)) {
					try {
						LST.BackColor = Color.LightYellow;
						LST.EnsureVisible();
						cMain.UpdateToolStripLabel(lblStatus, "Converting " + (LST.Index + 1) + " of " + lstMSU.Items.Count + " - " + cMain.GetFName(LST.Text));

						Application.DoEvents();

						int PrevCount = Directory.GetFiles(FBD, "*.cab", SearchOption.TopDirectoryOnly).Count();

						cMain.OpenProgram("\"" + cMain.SysFolder + "\\expand.exe\"", "\"" + LST.SubItems[3].Text + "\" -F:*Windows*.cab \"" + FBD + "\"", true, ProcessWindowStyle.Hidden);
						foreach (string F in Directory.GetFiles(FBD, "*.cab", SearchOption.TopDirectoryOnly)) {
							string F2 = cMain.GetFName(F, false);

							if (lstMSU.FindItemWithText(F2) == null && !N.Contains(F2) && F != cTo && !File.Exists(cTo)) {
								File.Move(F, cTo);
							}
						}

						int NewCount = Directory.GetFiles(FBD, "*.cab", SearchOption.TopDirectoryOnly).Count();
						if (NewCount > PrevCount) { LST.BackColor = Color.LightGreen; } else { LST.BackColor = Color.LightPink; }

					}
					catch (Exception Ex) {
						LST.BackColor = Color.LightPink;

						LargeError LE = new LargeError("MSU Conversion", "Error Converting MSU: " + LST.Text + "\n" + LST.Text, Ex);
						LE.Upload(); LE.ShowDialog();
					}
				}
				else {
					LST.BackColor = Color.LightGreen;
				}

				PB.Value += 1;
				Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt16(PB.Value),
															Convert.ToUInt16(lstMSU.Items.Count));
				if (BWConvert.CancellationPending) {
					break;
				}
			}
			Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
		}

		private void BWConvert_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			cmdSC.Text = "Start";
			cmdSC.Image = Properties.Resources.OK;
			Cancelled = false;
			PB.Visible = false;
			cmdR.Visible = true;
			cmdAU.Visible = true;
			cMain.UpdateToolStripLabel(lblStatus, "");
		}

		private void cmdRU_Click(object sender, EventArgs e) {
			if (lstMSU.SelectedItems.Count == 0) {
				MessageBox.Show("Please select at least 1 file!", "Invalid Selection");
				return;
			}
			foreach (ListViewItem LST in lstMSU.Items) {
				if (LST.Selected) {
					LST.Remove();
				}
			}
			ColumnHeader1.Text = "Name (" + lstMSU.Items.Count + ")";
		}

		private void cmdRA_Click(object sender, EventArgs e) {
			lstMSU.Items.Clear();
			ColumnHeader1.Text = "Name (" + lstMSU.Items.Count + ")";
		}

		private void lstMSU_DragDrop(object sender, DragEventArgs e) {
			lstMSU.Enabled = false;
			ToolStrip1.Enabled = false;
			var FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			Cancelled = false;

			int n = 1;
			int T = FileList.Count(strFilename => !strFilename.ToUpper().EndsWithIgnoreCase(".MSU"));

			uError = ""; UC = 0;

			foreach (string strFilename in FileList) {
				if (!strFilename.ToUpper().EndsWithIgnoreCase(".MSU")) {
					cMain.UpdateToolStripLabel(lblStatus, "Adding " + n + " of " + T + ": " + strFilename + "...");
					Application.DoEvents();
					try {
						AddToList(strFilename);

					}
					catch (Exception Ex) {
						if (Cancelled == false) {
							uError += "Unknown Error: '" + strFilename + "' - " + Ex.Message + Environment.NewLine;
							UC += 1;
						}
					}
					n += 1;
				}
				if (Cancelled) { break; }
			}
			lstMSU.Enabled = true;
			ToolStrip1.Enabled = true;

			if (UC > 0 && !string.IsNullOrEmpty(uError)) {
				cMain.ErrorBox("More information is available by clicking the '>> Details' button.", UC + " update(s) skipped.", uError);
			}
		}

		private void lstMSU_DragEnter(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void cmdSC_Click(object sender, EventArgs e) {
            if (cmdSC.Text.EqualsIgnoreCase("Start"))
            {
				if (lstMSU.Items.Count == 0) {
					MessageBox.Show("You need to add some *.msu files before you can start!", "No Updates");
					return;
				}
				FBD = cMain.FolderBrowserVista("Select CAB save location...", false, true);
				if (FBD.EndsWithIgnoreCase("\\")) { FBD = FBD.Substring(0, FBD.Length - 1); }
				if (string.IsNullOrEmpty(FBD)) {
					return;
				}

				Cancelled = false;
				cmdAU.Visible = false;
				cmdR.Visible = false;
				PB.Value = 0;
				PB.Maximum = lstMSU.Items.Count;
				cmdSC.Text = "Cancel";
				cmdSC.Image = Properties.Resources.Close;
				PB.Visible = true;
				//WriteLogA
				BWConvert.RunWorkerAsync();
			}
			else {
				Cancelled = true;
				BWConvert.CancelAsync();
			}
		}

		private void cmdAU_Click(object sender, EventArgs e) {
			var OFD = new OpenFileDialog();
			OFD.Title = "Select Updates";
			OFD.Filter = "Windows Update(s) *.msu|*.msu";
			OFD.Multiselect = true;
			if (cOptions.fUpdates != null && Directory.Exists(cOptions.fUpdates)) { OFD.InitialDirectory = cOptions.fUpdates; }
			if (OFD.ShowDialog() != DialogResult.OK) {
				return;
			}
			Cancelled = false;
			lstMSU.Enabled = false;
			ToolStrip1.Enabled = false;
			int P = lstMSU.Items.Count;
			int N = 1;
			int T = cMain.GetAdditions(OFD.FileNames);
			if (T == 0) { return; }

			UC = 0; uError = "";
			//WriteLogD
			lstMSU.BeginUpdate();
			foreach (string strFilename in OFD.FileNames) {
				try {
					if (strFilename.ToUpper().EndsWithIgnoreCase(".MSU")) {
						cMain.UpdateToolStripLabel(lblStatus, "(" + N + "/" + T + ") Adding: " + strFilename + "...");
						Application.DoEvents();
						AddToList(strFilename);
					}
				}
				catch (Exception Ex) {
					if (Cancelled == false) {
						uError += "Unknown Error: '" + strFilename + "' - " + Ex.Message + Environment.NewLine;
						UC += 1;
					}
				}
				N += 1;
				if (Cancelled) { break; }
			}
			lstMSU.EndUpdate();
			cMain.UpdateToolStripLabel(lblStatus, "");
			Cancelled = true;
			lstMSU.Enabled = true;
			ToolStrip1.Enabled = true;
			lstMSU.Columns[0].Text = "Name (" + lstMSU.Items.Count + ")";
			lstMSU.Columns[0].Width = -2;
			cMain.UpdateToolStripLabel(lblStatus, (lstMSU.Items.Count - P).ToString(CultureInfo.InvariantCulture) + " items added.");
			if (UC > 0 && !string.IsNullOrEmpty(uError)) {
				cMain.ErrorBox("More information is available by clicking the '>> Details' button.", UC + " update(s) skipped.", uError);
			}

		}

	}
}