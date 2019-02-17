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
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;

namespace WinToolkit {
	public partial class frmDrvInstaller : Form {
		private const int oA = 0;
		private string sSupport;
		string Drivers = "";

		private void TabChanged(Object sender, EventArgs e) {
			ListViewEx.LVE = tabControl1.SelectedTab == tabPage1 ? lstDrivers : lstInstalled;
		}

		public frmDrvInstaller() {
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			FormClosing += frmDrvInstaller_FormClosing;
			FormClosed += frmDrvInstaller_FormClosed;
			CheckForIllegalCrossThreadCalls = false;
			tabControl1.SelectedIndexChanged += TabChanged;
			BWA.RunWorkerCompleted += BWA_RunWorkerCompleted;
			BWI.RunWorkerCompleted += BWI_RunWorkerCompleted;
			BWU.RunWorkerCompleted += BWU_RunWorkerCompleted;
			ListViewEx.LVE = lstDrivers;
			BWScan.RunWorkerCompleted += bwScan_RunWorkerCompleted;
			Activated += cMain.FormActivated;
			Deactivate += cMain.FormActivated;
			MouseWheel += MouseScroll;
		}

		private void frmDrvInstaller_Load(object sender, EventArgs e)
		{
		    scNew.Scale4K(_4KHelper.Panel.Pan1);
		    scInstalled.Scale4K(_4KHelper.Panel.Pan1);

            cMain.FormIcon(this); cMain.eLBL = lblStatus; cMain.eForm = this;
			cMain.ToolStripIcons(ToolStrip1);
			cMain.ToolStripIcons(toolStrip2);
			cMain.FreeRAM();
			cboForce.SelectedIndex = 0;
			cboOption.SelectedIndex = 0;
            lstDrivers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		    lstInstalled.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

		private void MouseScroll(object sender, MouseEventArgs e) {
			switch (tabControl1.SelectedTab.Text) {
				case "New":
					lstDrivers.Select();
					break;
				case "Installed":
					lstInstalled.Select();
					break;
			}
		}

		private void frmDrvInstaller_FormClosing(object sender, FormClosingEventArgs e) {
			cMain.OnFormClosing(this);
			if (BWA.IsBusy || BWScan.IsBusy) {
				MessageBox.Show("You cannot close this windows while files are being added!", "Installation in Progress");
				e.Cancel = true;
			}

			if (BWI.IsBusy || BWU.IsBusy) {
				MessageBox.Show("You cannot close this windows while installation is in progress!", "Installation in Progress");
				e.Cancel = true;
			}
		}

		private void frmDrvInstaller_FormClosed(object sender, FormClosedEventArgs e) {
			cMain.ReturnME();
		}

		private void BWA_DoWork(object sender, DoWorkEventArgs e) {


			try {
				if (!string.IsNullOrEmpty(Drivers)) {
					if (Drivers.Length <= 3) {
						MessageBox.Show(
							 "Please be careful when selecting a drive (" + Drivers +
							 "), remember DISM will integrate all the drivers in all the subfolders too and may take a while. If you have kept your drivers on a different partition then this is fine :)",
							 "Root2 Drive Detected!");
					}

					cOptions.fDrivers = Drivers;

					cMain.UpdateToolStripLabel(lblStatus, "Counting drivers...");
					Application.DoEvents();
					cMain.FreeRAM();

					int dCurrent = 1;
					lstDrivers.BeginUpdate();

					int UC = 0; string uError = "";
					foreach (string S in cMain.SearchDirectory(Drivers, "*.inf")) {
						try {
							if (lstDrivers.FindItemWithText(S) == null && File.Exists(S)) {
								cMain.UpdateToolStripLabel(lblStatus, "(" + dCurrent + "\\" + cMain.Dirs.Count.ToString(CultureInfo.InvariantCulture) + ") Adding: " + S + "...");
								Application.DoEvents();
								AddDriver(S);
								dCurrent += 1;
							}
						}
						catch (Exception Ex) {
							UC += 1;
							uError = "Error adding driver: " + S + Environment.NewLine + Ex.Message;
							LargeError LE = new LargeError("Adding Driver", "Unable to add driver.", S, Ex);
							LE.Upload();
						}
						if (BWA.CancellationPending) { break; }
					}
					lstDrivers.EndUpdate();

					cMain.UpdateToolStripLabel(lblStatus, "Finishing...");
					if (UC > 0 && !string.IsNullOrEmpty(uError)) {
						cMain.ErrorBox("Some items have not been added to the list due to unknown errors", "Items skipped", uError);
					}
				}
				lstDrivers.Columns[0].Text = "Name (" + lstDrivers.Items.Count + ")";
				foreach (ColumnHeader CHU in lstDrivers.Columns) { CHU.Width = -2; }

				if (lstDrivers.Items.Count > 0) { mnuR.Visible = false; }
			}
			catch (Exception Ex) {
				MessageBox.Show("Error adding item" + Environment.NewLine + Ex.Message);
				cMain.WriteLog(this, "Error adding item", Ex.Message, lblStatus.Text);
			}
			EnableMe(true);
			cMain.FreeRAM();
		}

		private void EnableMe(bool E) {
			cmdSI.Enabled = E;
			cmdU.Enabled = E;
			cmdRefresh.Enabled = E;
			cmdAF.Visible = E;
			mnuR.Visible = E;
			lstDrivers.Enabled = E;
			lstInstalled.Enabled = E;
			cboForce.Visible = E;
			cboOption.Visible = E;
			cmdU.Enabled = lstInstalled.Items.Count > 0 && E;
		}

		private void BWA_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			foreach (ListViewItem LST in lstDrivers.Items) { if (LST.BackColor == Color.LightPink) { LST.Remove(); } }
			mnuR.Visible = lstDrivers.Items.Count > 0;
            lstDrivers.EndUpdate();
			cMain.UpdateToolStripLabel(lblStatus, "Added " + (lstDrivers.Items.Count - oA) + " drivers...");
			cmdAF.Text = "Add Drivers";
			cmdAF.Enabled = true;
			EnableMe(true);
			cMain.FreeRAM();
		}

		private void cmdAF_Click(object sender, EventArgs e) {
            if (cmdAF.Text.EqualsIgnoreCase("Add Drivers"))
            {
				Drivers = cMain.FolderBrowserVista("Drivers", false, true, cOptions.fDrivers);
				if (string.IsNullOrEmpty(Drivers)) { return; }
				cOptions.fDrivers = Drivers;
				EnableMe(false);
				cmdAF.Text = "Cancel";
				cMain.FreeRAM();
                lstDrivers.BeginUpdate();
				BWA.RunWorkerAsync();
			}
			else {
				cmdAF.Enabled = false;
				BWA.CancelAsync();
				cMain.UpdateToolStripLabel(lblStatus, "Stopping...");
			}
		}

		private void BWI_DoWork(object sender, DoWorkEventArgs e) {

			Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
			PB.Value = 0;
			PB.Maximum = lstDrivers.Items.Count;

			int UC = 0;
			string uError = "";

			foreach (ListViewItem LST in lstDrivers.Items) {
				string ID = "";
				try {
					string IF = LST.SubItems[6].Text;
					Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst", true);

					if (File.Exists(IF)) {
						cMain.UpdateToolStripLabel(lblStatus, "Installing Driver: " + (LST.Index + 1) + " of " + lstDrivers.Items.Count + " (" + LST.Text + ")");

                        if (cboOption.Text.EqualsIgnoreCase("Add and Install Driver"))
                        {
							ID = cMain.RunExternal("\"" + cMain.SysFolder + "\\pnputil.exe\"", "-i -a \"" + IF + "\"");
						}

						if (cboOption.Text.EqualsIgnoreCase("Add Driver")) {
							ID = cMain.RunExternal("\"" + cMain.SysFolder + "\\pnputil.exe\"", "-a \"" + IF + "\"");
						}

						if (ID.ContainsIgnoreCase(": 0")) {
							UC += 1;
							uError += "Unknown Error " + (UC + 1).ToString(CultureInfo.InvariantCulture) + ": Status: " + lblStatus.Text + Environment.NewLine;
						}
					}
				}
				catch (Exception Ex) {
					UC += 1;

					string n = lstDrivers.SelectedItems[0].SubItems.Cast<string>().Aggregate("", (current, S) => current + (S + " | "));

					uError += "Unknown Error " + (UC + 1).ToString(CultureInfo.InvariantCulture) + ": " + Ex.Message + Environment.NewLine + "Status: " + lblStatus.Text + Environment.NewLine + "Items: " + n + Environment.NewLine + "Info: " + ID + Environment.NewLine;

					LargeError LE = new LargeError("Unable to integrate driver.", Ex.Message, lblStatus.Text, Ex);
                    LE.Upload();
				}
				if (BWI.CancellationPending) {
					cMain.UpdateToolStripLabel(lblStatus, "Installation Aborted!");
					break;
				}
				cMain.FreeRAM();
				PB.Value += 1;
				Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt16(PB.Value), Convert.ToUInt16(lstDrivers.Items.Count));
			}
			PB.Value = 0;
			Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
			cMain.UpdateToolStripLabel(lblStatus, "Finishing...");
			if (UC > 0 && !string.IsNullOrEmpty(uError)) {
				cMain.ErrorBox("One or more drivers have not been installed due to an unknown error, you may be trying to install a driver not designed for your OS architecture or the device has not been found.", "Some drivers were not installed.", uError);
			}
		}

		private void BWI_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			if (!lblStatus.Text.ContainsIgnoreCase("Aborted")) { cMain.UpdateToolStripLabel(lblStatus, "Install Completed!"); }
			mnuR.Visible = true;
			cmdAF.Visible = true;
			PB.Visible = false;
			cmdSI.Text = "Start";
			cmdSI.Image = Properties.Resources.OK;
			cmdU.Text = "Uninstall";
			cmdU.Image = Properties.Resources.OK;
			cMain.FreeRAM();
			cmdRefresh.PerformClick();
		}

		private void cmdSI_Click(object sender, EventArgs e) {
            if (cmdSI.Text.EqualsIgnoreCase("Start"))
            {
				if (lstDrivers.Items.Count == 0) {
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
			else {
				cMain.UpdateToolStripLabel(lblStatus, "Aborting installation, please wait...");
				BWI.CancelAsync();
				cmdSI.Enabled = false;
			}
		}

		private void lstIC_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			sSupport = e.Item.SubItems[7].Text;
		}
		private void AddDriver(string F) {
			var N = new ListViewItem();

			try {
				string MD5 = cMain.MD5CalcFile(F);

				if (lstDrivers.FindItemWithText(F) != null) { return; }
				if (lstDrivers.FindItemWithText(MD5) != null && !string.IsNullOrEmpty(MD5)) { return; }

				string T = F;

				while (!T.EndsWithIgnoreCase("\\")) {
					T = T.Substring(0, T.Length - 1);
				}

				var SR = new StreamReader(F);
				string sRead = SR.ReadToEnd();
				string sClass = "Unknown", sVersion = "N/A", sDate = "N/A", cArc = "??";
				string WinDrv = "Unknown";

				if (sRead.ContainsIgnoreCase(".NT") || sRead.ContainsIgnoreCase(".NTX86") || sRead.ContainsIgnoreCase("NTAMD64") || sRead.ContainsIgnoreCase("$WINDOWS NT$")) { WinDrv = "XP or newer"; }
				if (sRead.ContainsIgnoreCase(".NTX86.5.1") || sRead.ContainsIgnoreCase("NTAMD64.5.1") || sRead.ContainsIgnoreCase("WINDOWS XP") || sRead.ContainsIgnoreCase("WINDOWSXP") || sRead.ContainsIgnoreCase("WINXP") || sRead.ContainsIgnoreCase(" XP") && !sRead.ContainsIgnoreCase(" or later")) { WinDrv += ", XP"; }
				if (sRead.ContainsIgnoreCase(".NTX86.5.2") || sRead.ContainsIgnoreCase("NTAMD64.5.2")) { WinDrv += ", 2003"; }
				if (sRead.ContainsIgnoreCase(".NTX86.6.0") || sRead.ContainsIgnoreCase("NTAMD64.6.0") || sRead.ContainsIgnoreCase("WINDOWS VISTA") || sRead.ContainsIgnoreCase("VISTA") || sRead.ContainsIgnoreCase("WINVISTA") || F.ContainsIgnoreCase("\\VISTA\\")) { WinDrv += ", Vista"; }
				if (sRead.ContainsIgnoreCase(".NTX86.6.1") || sRead.ContainsIgnoreCase("NTAMD64.6.1") || sRead.ContainsIgnoreCase("WINDOWS 7") || sRead.ContainsIgnoreCase("WINDOWS7") || sRead.ContainsIgnoreCase("WIN7") || sRead.ContainsIgnoreCase("/7.") || F.ContainsIgnoreCase("\\WIN7\\") || F.ContainsIgnoreCase("\\WIN 7\\")) { WinDrv += ", 7"; }
				if (sRead.ContainsIgnoreCase(".NTX86.6.2") || sRead.ContainsIgnoreCase("NTAMD64.6.2") || sRead.ContainsIgnoreCase("WINDOWS 8") || sRead.ContainsIgnoreCase("WINDOWS8") || sRead.ContainsIgnoreCase("WIN8") || sRead.ContainsIgnoreCase("/8.") || F.ContainsIgnoreCase("\\WIN8\\") || F.ContainsIgnoreCase("\\WIN 8\\")) { WinDrv += ", 8"; }

				if (WinDrv.ContainsIgnoreCase(",")) {
					while (!WinDrv.StartsWithIgnoreCase(",")) { WinDrv = WinDrv.Substring(1); }
					WinDrv = WinDrv.Substring(2);
				}
				if (WinDrv.EqualsIgnoreCase("Unknown")) { N.BackColor = Color.Yellow; }
				if (WinDrv.EqualsIgnoreCase("XP, Vista, 8")) { WinDrv = "7"; }
				if (WinDrv.EqualsIgnoreCase("7")) { WinDrv = "Win 7"; }
				if (WinDrv.EqualsIgnoreCase("8")) { WinDrv = "Win 8"; }

				if (sRead.ContainsIgnoreCase(".AMD64") || sRead.ContainsIgnoreCase("NTAMD64") || sRead.ContainsIgnoreCase("64-BIT") || sRead.ContainsIgnoreCase("NTX64") || sRead.ContainsIgnoreCase(".IA64")) {
					cArc = "x64";
				}

				if (sRead.ContainsIgnoreCase(".X86") || sRead.ContainsIgnoreCase("NTX86") ||
                     sRead.ContainsIgnoreCase("32-BIT") || sRead.ContainsIgnoreCase("NT,") || sRead.ContainsIgnoreCase(".NT.") ||
					 sRead.ContainsIgnoreCase("[INTEL_HDC]") || sRead.ContainsIgnoreCase("[INTEL_SYS]") ||
					 sRead.ContainsIgnoreCase(";32X86") || sRead.ContainsIgnoreCase("[MARVELL]") ||
					 sRead.ContainsIgnoreCase(".X86]") || (sRead.ContainsIgnoreCase("[INTEL.NTAMD64]") && sRead.ContainsIgnoreCase("[INTEL]"))) {
					cArc = cArc.EqualsIgnoreCase("x64") ? "Mix" : "x86";
				}

				bool a86 = false; bool a64 = false;
				if (F.ContainsIgnoreCase("X86") || F.ContainsIgnoreCase("I386") || F.ContainsIgnoreCase("X32")) { a86 = true; cArc = "x86"; }
				if (F.ContainsIgnoreCase("X64") || F.ContainsIgnoreCase("AMD64") || F.ContainsIgnoreCase(".IA64")) { a64 = true; cArc = "x64"; }
				if (a86 && a64) { cArc = "Mix"; }

				string Override = F;
				while (!Override.EndsWithIgnoreCase("\\")) {
					Override = Override.Substring(0, Override.Length - 1);
				}

				if (File.Exists(Override + "x86.txt") || File.Exists(Override + "x64.txt")) {
					if (File.Exists(Override + "x86.txt")) {
						cArc = "x86";
					}
					if (File.Exists(Override + "x64.txt")) {
						cArc = "x64";
					}
					if (File.Exists(Override + "Mix.txt")) {
						cArc = "Mix";
					}
					if (File.Exists(Override + "x86.txt") && File.Exists(Override + "x64.txt")) {
						cArc = "Mix";
					}
				}

				if (sRead.ContainsIgnoreCase("CLASS") && sRead.ContainsIgnoreCase("DRIVERVER")) {
					foreach (string I in sRead.Split(Environment.NewLine.ToCharArray())) {
						if (I.ContainsIgnoreCase("CLASS") && I.ContainsIgnoreCase("=") && sClass.EqualsIgnoreCase("Unknown") && I.ContainsIgnoreCase("{") && !I.ContainsIgnoreCase(";")) {
							sClass = I;
							while (sClass.ContainsIgnoreCase("=")) {
								sClass = sClass.Substring(1);
							}
							while (sClass.StartsWithIgnoreCase(" ")) {
								sClass = sClass.Substring(1);
							}
						}

						if (I.ContainsIgnoreCase("DRIVERVER") && I.ContainsIgnoreCase("=")) {
							foreach (string sDV in I.Split(',')) {
								try {
									string sDV2 = sDV;
									while (sDV2.ContainsIgnoreCase("=")) { sDV2 = sDV2.Substring(1); }
									while (sDV2.StartsWithIgnoreCase(" ")) { sDV2 = sDV2.Substring(1); }
									while (sDV2.ContainsIgnoreCase(",")) { sDV2 = sDV2.Substring(0, sDV2.Length - 1); }
									while (sDV2.ContainsIgnoreCase(" ")) { sDV2 = sDV2.Substring(0, sDV2.Length - 1); }
									if (sDV2.ContainsIgnoreCase("/")) { sDate = sDV2; }
									if (sDV2.ContainsIgnoreCase(".")) { sVersion = sDV2; }
								}
								catch { }
							}

						}
						if (sDate != "N/A" && sVersion != "N/A" && sClass != "Unknown") { break; }

					}
					SR.Close();
				}
				else {
					N.Text = "???";
				}

                if (cArc.EqualsIgnoreCase("??"))
                {
					cArc = "x86";
				}

				if (N.Text != "???") {
					if ((cMain.Arc64 && cArc != "x64" && cArc != "Mix") || (cMain.Arc64 == false && cArc != "x86" && cArc != "Mix")) {
						N.BackColor = Color.LightPink;
					}

					N.Text = cMain.GetFName(F);
					N.SubItems.Add(sClass.ToUpper());
					N.SubItems.Add(cArc);
					N.SubItems.Add(WinDrv);
					N.SubItems.Add(sDate.ReplaceIgnoreCase( "\"", ""));
					N.SubItems.Add(sVersion.ReplaceIgnoreCase( "\"", ""));
					N.SubItems.Add(F);
					N.SubItems.Add(T);
					N.SubItems.Add(MD5);

					lstDrivers.Items.Add(N);
				}
			}
			catch (Exception Ex) { }

		}

		private void mnuRF_Click(object sender, EventArgs e) {
			if (lstDrivers.SelectedItems.Count == 0) {
				MessageBox.Show("Please select at least 1 file", "Invalid Selection");
				return;
			}
			foreach (ListViewItem LST in lstDrivers.SelectedItems) {
				if (LST.Selected) { LST.Remove(); }
			}
			lstDrivers.Columns[0].Text = "Name (" + lstDrivers.Items.Count + ")";
			cMain.UpdateToolStripLabel(lblStatus, "");
		}

		private void mnuRA_Click(object sender, EventArgs e) {
			lstDrivers.Items.Clear();
			lstDrivers.Columns[0].Text = "Name (" + lstDrivers.Items.Count + ")";
			cMain.UpdateToolStripLabel(lblStatus, "");
		}

		private void bwScan_DoWork(object sender, DoWorkEventArgs e) {
			cMain.UpdateToolStripLabel(lblStatus, "Looking for Drivers...");
			cmdU.Enabled = false;
			lstInstalled.Items.Clear();
			Application.DoEvents();
			try {
				string UIDCheck = cMain.RunExternal("\"" + DISM.Latest.Location + "\"", "/Online /Get-Drivers /Format:Table");
				foreach (string Line in UIDCheck.Split(Environment.NewLine.ToCharArray())) {
					try {
						if (Line.ContainsIgnoreCase(".INF") && !string.IsNullOrEmpty(Line)) {
							var NI = new ListViewItem();
							foreach (string D in Line.Split('|')) {
								string LD = D;
								try {
									while (!LD.EndsWithIgnoreCase(" ")) {
										LD = LD.Substring(0, LD.Length - 1);
									}
								}
								catch {
								}
								if (LD.ContainsIgnoreCase("OEM") && LD.ContainsIgnoreCase(".INF")) {
									NI.Text = LD;
									while (NI.Text.EndsWithIgnoreCase(" "))
										NI.Text = NI.Text.Substring(0, NI.Text.Length - 1);
								}
								else {
									while (LD.EndsWithIgnoreCase(" "))
										LD = LD.Substring(0, LD.Length - 1);
									NI.SubItems.Add(LD);
								}
							}
							lstInstalled.Items.Add(NI);
							CHPN.Text = "Published Name (" + lstInstalled.Items.Count + ")";
						}
					}
					catch {
					}
					cMain.FreeRAM();
				}
			}
			catch (Exception Ex) {
				cMain.WriteLog(this, "Error detecting drivers", Ex.Message, lblStatus.Text);
			}

			foreach (ColumnHeader CH in lstInstalled.Columns) { CH.Width = -2; }

		}

		private void bwScan_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			EnableMe(true);
			if (lstInstalled.Items.Count > 0) { cboForce.Enabled = true; cmdU.Enabled = true; } else { cboForce.Enabled = false; cmdU.Enabled = false; }
			if (BWI.IsBusy == false && BWA.IsBusy == false && BWU.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, ""); }
            lstInstalled.EndUpdate();

		    if (lstInstalled.Items.Count == 0)
		        lstInstalled.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		    else
		        lstInstalled.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

		private void BWU_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			EnableMe(true);
			mnuR.Visible = true;
			cmdAF.Visible = true;
			PB.Visible = false;
			cmdU.Image = Properties.Resources.OK;
			cmdU.Text = "Uninstall";
			cMain.FreeRAM();
			cmdRefresh.PerformClick();
		}

		private void bwU_DoWork(object sender, DoWorkEventArgs e) {

			Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
			PB.Value = 0;
			PB.Maximum = lstInstalled.CheckedItems.Count;
			foreach (ListViewItem LST in lstInstalled.CheckedItems) {
				try {
					cMain.UpdateToolStripLabel(lblStatus, "Removing Driver: " + (PB.Value + 1) + " of " + lstInstalled.CheckedItems.Count + " (" + LST.Text + ")");
                    if (cboForce.Text.EqualsIgnoreCase("Force"))
                    {
						cMain.OpenProgram("\"" + cMain.SysFolder + "\\pnputil.exe\"", "-f -d " + LST.Text, true, ProcessWindowStyle.Hidden);
					}
					else {
						cMain.OpenProgram("\"" + cMain.SysFolder + "\\pnputil.exe\"", "-d " + LST.Text, true, ProcessWindowStyle.Hidden);
					}
				}
				catch (Exception Ex) {
					cMain.WriteLog(this, "Unable to remove driver.", Ex.Message, lblStatus.Text);
					cMain.ErrorBox("Win Toolkit was unable to driver '" + LST.Text + "' update.", "Error removing update", Ex.Message);
				}
				if (BWU.CancellationPending) {
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

		private void cmdU_Click(object sender, EventArgs e) {
            if (cmdU.Text.EqualsIgnoreCase("Uninstall"))
            {
				if (lstInstalled.CheckedItems.Count == 0) {
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
			else {
				cMain.UpdateToolStripLabel(lblStatus, "Aborting task, please wait...");
				BWU.CancelAsync();
				cmdU.Enabled = false;
			}
		}

		private void cmdRefresh_Click(object sender, EventArgs e) {
			EnableMe(false);
		    lstInstalled.BeginUpdate();
            BWScan.RunWorkerAsync();
		}

		private void lstDrivers_MouseDoubleClick(object sender, MouseEventArgs e) {
			try {
				if (lstDrivers.SelectedItems.Count > 0) {
					cMain.OpenProgram("\"" + cMain.SysFolder + "\\Notepad.exe\"", lstDrivers.SelectedItems[0].SubItems[6].Text, false, ProcessWindowStyle.Maximized);
				}
			}
			catch (Exception Ex) {
				string n = lstDrivers.SelectedItems[0].SubItems.Cast<string>().Aggregate("", (current, S) => current + (S + " | "));
				cMain.WriteLog(this, "Unable to open link", Ex.Message, n + Environment.NewLine + lblStatus.Text);
				MessageBox.Show(Ex.Message, "Error");
			}
		}

	}
}