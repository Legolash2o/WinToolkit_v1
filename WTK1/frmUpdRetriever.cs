using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;

namespace WinToolkit {
	public partial class frmUpdRetriever : Form {
		private readonly WebClient Client = new WebClient();
		bool Scanning;
		private string FBD = "";
		private string T;
		private readonly string WULog = cMain.SysRoot + "\\WindowsUpdate.log";

		public frmUpdRetriever() {
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			Shown += frmCabRetriever_Shown;
			FormClosing += frmCabRetriever_FormClosing;
			FormClosed += frmCabRetriever_FormClosed;
			BWCopy.RunWorkerCompleted += BWCopy_RunWorkerCompleted;
			CheckForIllegalCrossThreadCalls = false;
			Activated += cMain.FormActivated;
			Deactivate += cMain.FormActivated;
			MouseWheel += MouseScroll;
		}

		private void frmCabRetriever_Load(object sender, EventArgs e) {
            scCAB.Scale4K(_4KHelper.Panel.Pan2);
		    scWU.Scale4K(_4KHelper.Panel.Pan2);
            Scanning = true; cMain.eForm = this;
			cMain.FormIcon(this); cMain.eLBL = lblStatus;
		}

		private void MouseScroll(object sender, MouseEventArgs e) {
			switch (tabControl1.SelectedTab.Text) {
				case "CAB Files":
					lstCR.Select();
					break;
				case "Windows Update":
					lstWU.Select();
					break;

			}
		}

		private void ScanCABS() {
			try {
				cMain.UpdateToolStripLabel(lblStatus, "Looking for CAB Files...");
				Application.DoEvents();
				lstCR.Items.Clear();

				if (Directory.Exists(cMain.SysRoot + "\\SoftwareDistribution\\Download\\")) {
					foreach (string S in Directory.GetFiles(cMain.SysRoot + "\\SoftwareDistribution\\Download\\", "*.cab", SearchOption.AllDirectories))
					{
					    if (Path.GetFileNameWithoutExtension(S).ToUpper().EndsWithIgnoreCase("-EXPRESS"))
					    {
					        continue;
                        }
						var NI = new ListViewItem();
						NI.Text = cMain.GetFName(S);
						NI.SubItems.Add(cMain.GetSize(S,true));
						NI.SubItems.Add(S);
						lstCR.Items.Add(NI);
					}
				}
			}
			catch { }

			tabCAB.Text = "CAB Files [" + lstCR.Items.Count + "]";
			if (lstCR.Items.Count == 0) { CABVisibleMe(); cMain.UpdateToolStripLabel(lblStatus, "No CAB Files..."); }
			else {
				cMain.UpdateToolStripLabel(lblStatus, lstCR.Items.Count + " CAB Files...");
				CABVisibleMe(true); cMain.UpdateToolStripLabel(lblStatus, "");
			}
		}

		private void ScanWU() {
			try {
				lblStatus2.Text = "Looking for MSU Files...";
				Application.DoEvents();

				if (File.Exists(WULog)) {
					string WU = "";

					using (var stream = new FileStream(WULog, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
						using (var WUF = new StreamReader(stream)) {
							WU = WUF.ReadToEnd();
						}
					}

					foreach (string S in WU.Split(Environment.NewLine.ToCharArray())) {
						//download.windowsupdate.com
						try {
							if (S.ContainsIgnoreCase(".MSU") && S.ContainsIgnoreCase("HTTP://") && S.ContainsIgnoreCase("DOWNLOAD.WINDOWSUPDATE.COM")) {
								string URL = S;
								while (!URL.StartsWithIgnoreCase("http://")) { URL = URL.Substring(1); }
								while (URL.ContainsIgnoreCase(" ")) { URL = URL.Substring(0, URL.Length - 1); }

								string name = URL;
								while (name.ContainsIgnoreCase("/")) { name = name.Substring(1); }
								if (name.ContainsIgnoreCase("_")) { while (name.ContainsIgnoreCase("_")) { name = name.Substring(0, name.Length - 1); } }
								name = name.ReplaceIgnoreCase( "kb", "KB");
								name = name.ReplaceIgnoreCase( "windows", "Windows");
								var NI = new ListViewItem();
								NI.Text = name;
								NI.Checked = true;
								NI.SubItems.Add(URL);

								if (lstWU.FindItemWithText(name) == null) { lstWU.Items.Add(NI); }
							}
						}
						catch { }
					}
				}

			}
			catch (Exception Ex) { }

			tabMSU.Text = "Windows Update [" + lstWU.Items.Count + "]";
			if (lstWU.Items.Count == 0) { WUVisibleMe(); lblStatus2.Text = "No MSU Files..."; } else { lblStatus2.Text = lstWU.Items.Count + " MSU Files..."; WUVisibleMe(true); cMain.UpdateToolStripLabel(lblStatus, ""); }
		}

		private void frmCabRetriever_Shown(object sender, EventArgs e) {
			lstCR.BeginUpdate();
			lstWU.BeginUpdate();

			ScanCABS();
			ScanWU();

			lstCR.EndUpdate();
			lstWU.EndUpdate();

			cMain.AutoSizeColums(lstCR);
			cMain.AutoSizeColums(lstWU);

			if (lstCR.Items.Count == 0 && lstWU.Items.Count > 0) { tabControl1.SelectedTab = tabMSU; }

			Scanning = false;
			cmdCRefresh.Enabled = true;
			cmdWRefresh.Enabled = true;
			cMain.FreeRAM();
		}

		private void frmCabRetriever_FormClosing(object sender, FormClosingEventArgs e) {
			cMain.OnFormClosing(this);
			if (BWCopy.IsBusy || BWDownload.IsBusy) {
				MessageBox.Show("You can't close this tool while a task is in progress!", "Task In Progress");
				e.Cancel = true;
			}

			if (Scanning) {
				MessageBox.Show("You can't close this tool while Win Toolkit is scanning!", "Task In Progress");
				e.Cancel = true;
			}
		}

		private void frmCabRetriever_FormClosed(object sender, FormClosedEventArgs e) {
			cMain.ReturnME();
		}

		private void CABVisibleMe(bool V = false) {
			cmdCopy.Visible = V;
			cmdDelete.Visible = V;
			mnuShowSource.Visible = V;
			toolStripSeparator2.Visible = V;
			lstCR.Enabled = V;
		}
		private void WUVisibleMe(bool V = false) {
			cmdWU_OL.Visible = V;
			cmdDownload.Visible = V;
			toolStripSeparator4.Visible = V;
		}

		private void cmdCS_Click(object sender, EventArgs e) {
			if (lstCR.SelectedItems.Count == 0) {
				MessageBox.Show("You need to select at least 1 item to copy", "Invalid Selection");
				return;
			}
			FBD = cMain.FolderBrowserVista("Browse folder...", false, true);
			if (string.IsNullOrEmpty(FBD)) {
				return;
			}
			T = "CopySel";
			mnuShowDest.Visible = false;
			PB.Visible = true;
			cmdCRefresh.Enabled = false;
			cmdWRefresh.Enabled = false;
			CABVisibleMe();
			WUVisibleMe();
			BWCopy.RunWorkerAsync();
		}

		private void cmdCA_Click(object sender, EventArgs e) {
			FBD = cMain.FolderBrowserVista("Browse folder...", false, true);
			if (string.IsNullOrEmpty(FBD)) {
				return;
			}
			T = "CopyAll";
			mnuShowDest.Visible = false;
			CABVisibleMe();
			WUVisibleMe();
			PB.Visible = true;
			cmdCRefresh.Enabled = false;
			cmdWRefresh.Enabled = false;
			BWCopy.RunWorkerAsync();
		}

		private void cmdDS_Click(object sender, EventArgs e) {
			DialogResult DResult = MessageBox.Show("Are you sure you wish to delete all the selected files?", "Are you sure?", MessageBoxButtons.YesNo);
			if (DResult != DialogResult.Yes) {
				return;
			}
			T = "DelSel";
			mnuShowDest.Visible = false;
			CABVisibleMe();
			WUVisibleMe();
			PB.Visible = true;
			cmdCRefresh.Enabled = false;
			cmdWRefresh.Enabled = false;
			BWCopy.RunWorkerAsync();
		}

		private void cmdDA_Click(object sender, EventArgs e) {
			DialogResult DResult = MessageBox.Show("Are you sure you wish to delete all files?", "Are you sure?", MessageBoxButtons.YesNo);
			if (DResult != DialogResult.Yes) {
				return;
			}
			T = "DelAll";
			mnuShowDest.Visible = false;
			CABVisibleMe();
			WUVisibleMe();
			cmdCRefresh.Enabled = false;
			cmdWRefresh.Enabled = false;
			PB.Visible = true;
			BWCopy.RunWorkerAsync();
		}

		private void BWCopy_DoWork(object sender, DoWorkEventArgs e) {

			PB.Value = 0;
			sCopy = 0;
			Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);

			if (!Directory.Exists(FBD)) { cMain.CreateDirectory(FBD); }

			if (T.ContainsIgnoreCase("All")) {
				PB.Maximum = lstCR.Items.Count;
				foreach (ListViewItem LST in lstCR.Items) {
					if (File.Exists(LST.SubItems[2].Text)) {
						try {
							if (T.EqualsIgnoreCase("CopyAll")) {
								cMain.UpdateToolStripLabel(lblStatus, "Copying " + (PB.Value + 1) + " of " + PB.Maximum + " :: " + LST.Text);
								File.Copy(LST.SubItems[2].Text, FBD + "\\" + LST.Text, true);
							}

                            if (T.EqualsIgnoreCase("DelAll"))
                            {
								cMain.UpdateToolStripLabel(lblStatus, "Deleting " + (PB.Value + 1) + " of " + PB.Maximum + " :: " + LST.Text);
								Files.DeleteFile(LST.SubItems[2].Text);
							}

							if (T.ContainsIgnoreCase("Copy")) {
								if (File.Exists(FBD + "\\" + LST.Text)) {
									LST.BackColor = Color.LightGreen; sCopy += 1;
								}
								else {
									LST.BackColor = Color.LightPink;
								}
							}
							if (T.ContainsIgnoreCase("Del")) {
								if (!File.Exists(LST.SubItems[2].Text)) {
									LST.BackColor = Color.LightGreen;
									LST.Remove();
								}
								else {
									LST.BackColor = Color.LightPink;
								}
							}
						}
						catch (Exception Ex) {
							LargeError LE = new LargeError("Addon Task Error", "Unable to do task - " + T + " " + LST.Text, lblStatus.Text, Ex);
							LE.Upload(); LE.ShowDialog();
						}
					}
					PB.Value += 1;
					Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt16(PB.Value), Convert.ToUInt16(PB.Maximum));
				}
			}
			if (T.ContainsIgnoreCase("Sel")) {
				PB.Maximum = lstCR.SelectedItems.Count;
				foreach (ListViewItem LST in lstCR.SelectedItems) {
					if (File.Exists(LST.SubItems[2].Text)) {
						try {
							if (T.EqualsIgnoreCase("CopySel")) {
								cMain.UpdateToolStripLabel(lblStatus, "Copying " + (PB.Value + 1) + " of " + PB.Maximum + " :: " + LST.Text);
								File.Copy(LST.SubItems[2].Text, FBD + "\\" + LST.Text, true);
							}

							if (T.EqualsIgnoreCase("DelSel")) {
								cMain.UpdateToolStripLabel(lblStatus, "Deleting " + (PB.Value + 1) + " of " + PB.Maximum + " :: " + LST.Text);
								Files.DeleteFile(LST.SubItems[2].Text);
							}

							if (T.ContainsIgnoreCase("Copy")) {
								if (File.Exists(FBD + "\\" + LST.Text)) {
									LST.BackColor = Color.LightGreen; sCopy += 1;
								}
								else {
									LST.BackColor = Color.LightPink;
								}
							}
							if (T.ContainsIgnoreCase("Del")) {
								if (!File.Exists(LST.SubItems[2].Text)) {
									LST.BackColor = Color.LightGreen;
									LST.Remove();
								}
								else {
									LST.BackColor = Color.LightPink;
								}
							}
						}
						catch (Exception Ex) {
							MessageBox.Show(LST.Text + Environment.NewLine + Ex.Message, "Error");
							cMain.WriteLog(this, "Unable to do task - " + T + " " + LST.Text, Ex.Message, lblStatus.Text);
						}
					}
					PB.Value += 1;
					Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt16(PB.Value), Convert.ToUInt16(PB.Maximum));
				}
			}
			Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
		}

		private void BWCopy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			tabCAB.Text = "CAB Files [" + lstCR.Items.Count + "]";
			tabMSU.Text = "Windows Update [" + lstWU.Items.Count + "]";
			if (lstCR.Items.Count == 0 && lstWU.Items.Count > 0) { tabControl1.SelectedTab = tabMSU; }
			cMain.AutoSizeColums(lstCR);
			PB.Visible = false;
			CABVisibleMe(true);
			WUVisibleMe(true);
			cmdCRefresh.Enabled = true;
			cmdWRefresh.Enabled = true;
			if (sCopy > 0 && lstCR.Items.Count > 0) { mnuShowDest.Visible = true; } else { mnuShowDest.Visible = false; }
			cMain.UpdateToolStripLabel(lblStatus, "");
			PB.Value = 0;
			if (sCopy > 0) {
				MessageBox.Show("Win Toolkit successfully copied " + sCopy.ToString(CultureInfo.InvariantCulture) + " file(s) to:" + Environment.NewLine + Environment.NewLine + FBD, "Completed");
			}

			cMain.FreeRAM();
		}

		private void mnuShowFolder_Click(object sender, EventArgs e) {
			cMain.OpenExplorer(this, cMain.SysRoot + "\\SoftwareDistribution\\Download");
		}

		private void mnuShowDest_Click(object sender, EventArgs e) {
			if (!string.IsNullOrEmpty(FBD)) { cMain.OpenExplorer(this, FBD); }
		}

		private void cmdWU_OL_Click(object sender, EventArgs e) {
			if (File.Exists(WULog)) {
				cMain.OpenProgram("\"" + cMain.SysFolder + "\\notepad.exe\"", WULog, false, System.Diagnostics.ProcessWindowStyle.Maximized);
			}
		}

		private void cmdDownload_Click(object sender, EventArgs e) {
            if (cmdDownload.Text.EqualsIgnoreCase("Download"))
            {
				if (lstWU.CheckedItems.Count == 0) {
					MessageBox.Show("You need to select at least 1 item to download", "Invalid Selection");
					return;
				}
				FBD = cMain.FolderBrowserVista("Browse folder...", false, true);
				if (string.IsNullOrEmpty(FBD)) {
					return;
				}

				cmdWRefresh.Enabled = false;
				cmdCRefresh.Enabled = false;
				PB2.Value = 0;
				PB2.Maximum = lstWU.Items.Count;
				PB2.Visible = true;

				CABVisibleMe();
				WUVisibleMe();
				cmdDownload.Text = "Cancel";
				cmdDownload.Visible = true;
				BWDownload.RunWorkerAsync();
			}
			else {
				cmdDownload.Enabled = false;
				BWDownload.CancelAsync();
				Client.CancelAsync();
				lblStatus2.Text = "Stopping...";
			}
		}

		private void BWDownload_DoWork(object sender, DoWorkEventArgs e) {

			int n = 1;
			if (!Directory.Exists(FBD)) {
				cMain.CreateDirectory(FBD);
			}

			int CI = lstWU.CheckedItems.Count;
			foreach (ListViewItem LST in lstWU.CheckedItems) {
				PB2.Value = 0;

				LST.EnsureVisible();
				lblStatus2.Text = "Downloading (" + n + "/" + CI + "): " + LST.Text;
				Application.DoEvents();

				string F = FBD + "\\" + LST.Text + ".msu";

				try {
					LST.BackColor = Color.Yellow;
					if (!File.Exists(F)) {
						Client.DownloadProgressChanged += DownloadProgressCallback;
						Client.DownloadFileAsync(new Uri(LST.SubItems[1].Text), F);
						while (Client.IsBusy) {
							Thread.Sleep(250);
							Application.DoEvents();
							cMain.FreeRAM();
						}
					}
				}
				catch (Exception Ex) {
					Files.DeleteFile(F);
					LST.BackColor = Color.LightPink;
					cMain.ErrorBox("Win Toolkit was unable to download the following file: " + LST.Text, "Download Error", Ex.Message);
				}

				if (BWDownload.CancellationPending) {
					Files.DeleteFile(F);
					LST.BackColor = Color.LightPink;
				}

				if (File.Exists(F)) { LST.BackColor = Color.LightGreen; }

				if (Directory.GetFiles(FBD, "*.*", SearchOption.AllDirectories).Length < 1) {
					Files.DeleteFolder(FBD, false);
				}

				Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt16(n), Convert.ToUInt16(CI));
				n += 1;

				Application.DoEvents();
				if (BWDownload.CancellationPending) {
					break;
				}
				cMain.FreeRAM();
			}
		}

		private void BWDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);

			tabCAB.Text = "CAB Files [" + lstCR.Items.Count + "]";
			tabMSU.Text = "Windows Update [" + lstWU.Items.Count + "]";
			if (lstCR.Items.Count == 0 && lstWU.Items.Count > 0) { tabControl1.SelectedTab = tabMSU; }
			cMain.AutoSizeColums(lstWU);
			cMain.AutoSizeColums(lstCR);
			PB2.Visible = false;
			CABVisibleMe(true);
			cmdCRefresh.Enabled = true;
			cmdWRefresh.Enabled = true;
			WUVisibleMe(true);
			cmdDownload.Text = "Download";
			lblStatus2.Text = "";
			PB2.Value = 0;

			cMain.FreeRAM();
		}
		private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e) {
			try {
				if (e.ProgressPercentage > PB2.Value && e.ProgressPercentage <= PB2.Maximum) {
					PB2.Value = e.ProgressPercentage;
				}
			}
			catch (Exception) {
			}
		}

		private void cmdCopy_MouseHover(object sender, EventArgs e) {
			cmdCopy.ShowDropDown();
		}

		private void cmdDelete_MouseHover(object sender, EventArgs e) {
			cmdDelete.ShowDropDown();
		}

		private void cmdCRefresh_Click(object sender, EventArgs e) {
			Scanning = true;
			cmdCRefresh.Enabled = false;
			cmdWRefresh.Enabled = false;
			CABVisibleMe();
			lstCR.BeginUpdate();
			ScanCABS();
			lstCR.EndUpdate();
			cmdCRefresh.Enabled = true;
			cmdWRefresh.Enabled = true;
			Scanning = false;
		}

		private void cmdWRefresh_Click(object sender, EventArgs e) {
			Scanning = true;
			cmdCRefresh.Enabled = false;
			cmdWRefresh.Enabled = false;
			WUVisibleMe();
			lstWU.BeginUpdate();
			ScanWU();
			lstWU.EndUpdate();
			cmdCRefresh.Enabled = true;
			cmdWRefresh.Enabled = true;
			Scanning = false;
		}


	}
}