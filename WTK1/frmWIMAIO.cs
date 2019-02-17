using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;
using WinToolkit.Properties;

namespace WinToolkit {
	public partial class frmAIODiskCreator : Form {
		private readonly OpenFileDialog OFD = new OpenFileDialog();
		private bool Selected;
		private bool dupli;
		private string iErr = "";
		private string nStatus = "";
		private string nWIM = "";

		public frmAIODiskCreator() {
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			FormClosing += frmWIMAIO_FormClosing;
			FormClosed += frmWIMAIO_FormClosed;
			BWMerge.RunWorkerCompleted += BWMerge_RunWorkerCompleted;
			BWR.RunWorkerCompleted += BWR_RunWorkerCompleted;
			CheckForIllegalCrossThreadCalls = false;
			Activated += cMain.FormActivated;
			Deactivate += cMain.FormActivated;
			MouseWheel += MouseScroll;
		}

		private void frmWIMAIO_Load(object sender, EventArgs e) {
			cMain.FormIcon(this); cMain.eLBL = lblWIM; cMain.eForm = this;
			cMain.ToolStripIcons(ToolStrip1);
			lblWIM.Text = "Please select a WIM File...\nNOTE: If you are merging x86 and x64 images, Win Toolkit will place the 1st x86 image at the top!";
            SplitContainer1.Scale4K(_4KHelper.Panel.Pan2);
            SplitContainer2.Scale4K(_4KHelper.Panel.Pan1);
            lstImages.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}

		private void MouseScroll(object sender, MouseEventArgs e) {
			lstImages.Select();
		}

		private void frmWIMAIO_FormClosing(object sender, FormClosingEventArgs e) {
			cMain.OnFormClosing(this);
			if (BWR.IsBusy) {
				MessageBox.Show("You can't close while an image is being scanned!", "Scan in Progress");
				e.Cancel = true;
				return;
			}
			if (BWMerge.IsBusy) {
				MessageBox.Show("You can't close while an image is being merged!", "Merge in Progress");
				e.Cancel = true;
				return;
			}
			if (Selected == false) {
				foreach (ListViewItem I in lstImages.Items) {
					if (I.Font.Bold) {
						cMount.CWIM_UnmountImage(I.Text, lblWIM, false, true, true, this, I.SubItems[6].Text, true);
						if (cMount.uChoice == cMount.MountStatus.None) {
							e.Cancel = true;
							break;
						}
					}
				}
			}
		}

		private void frmWIMAIO_FormClosed(object sender, FormClosedEventArgs e) {
			if (Selected == false) {
				cMain.ReturnME();
			}
		}

		private void TaskInProgress(bool Enable) {
			lstImages.Enabled = Enable;
			cmdClear.Visible = Enable;
			cmdBrowse.Enabled = Enable;
			cmdMakeUSB.Visible = Enable;
			cmdMakeISO.Visible = Enable;
			cmdEI.Visible = Enable;
			cmdEdit.Visible = Enable;
			cmdDeleteImage.Visible = Enable;
			cmdRecovery64.Visible = Enable;
		}

		private void BWMerge_DoWork(object sender, DoWorkEventArgs e) {
			iErr = "";

			lblWIM.Text = "Creating AIO Disk, please wait...";
			cMain.AppErrC = 0;

			string sTemp = lstImages.Groups[0].Header;

			if (cOptions.QuickMerge == false) {
				Files.DeleteFile(sTemp + ".bak");
				foreach (ListViewGroup LVG in lstImages.Groups) {
					nStatus = LVG.Header;
					iErr = "";
					mImagex("/export \"" + LVG.Header + "\" * \"" + sTemp + ".bak\"" +
							  " /compress maximum");
					if (BWMerge.CancellationPending) {
						break;
					}
				}
				if (BWMerge.CancellationPending) {
					Files.DeleteFile(sTemp + ".bak");
					return;
				}
			}
			else {
				foreach (ListViewGroup LVG in lstImages.Groups) {
					if (LVG.Header != sTemp) {
						nStatus = "Quick " + LVG.Header;
						iErr = "";
						mImagex("/export \"" + LVG.Header + "\" * \"" + sTemp + "\"" +
								  " /compress maximum");
					}
					if (BWMerge.CancellationPending) {
						break;
					}
				}
			}
			if (cMain.AppErrC == 0) {
				if (cOptions.QuickMerge == false) {
					lblWIM.Text = "Removing backups...";
					Application.DoEvents();
					Files.DeleteFile(sTemp);
					File.Move(sTemp + ".bak", sTemp);
				}
			}
			else {
				MessageBox.Show("An unexpected error has occurred!" + Environment.NewLine + Environment.NewLine + iErr,
									 "Error (" + Convert.ToString(cMain.AppErrC) + ")");
				Files.DeleteFile(sTemp + ".bak");
			}

			if (cmdRecovery64.Checked && cMain.AppErrC == 0) {
				string To = "";
				string From = "";
				foreach (ListViewGroup LVG in lstImages.Groups) {
					if (LVG.Tag != "x64" && LVG.Header.ToUpper().EndsWithIgnoreCase("\\SOURCES\\INSTALL.WIM")) {
						To = LVG.Header.ReplaceIgnoreCase( "\\sources\\install.wim", "");
						break;
					}
				}

				foreach (ListViewGroup LVG in lstImages.Groups) {
                    if (LVG.Tag.ToString().EqualsIgnoreCase("x64") && LVG.Header.ToUpper().EndsWithIgnoreCase("\\SOURCES\\INSTALL.WIM"))
                    {
						From = LVG.Header.ReplaceIgnoreCase("\\sources\\install.wim", "");
						break;
					}
				}

				if (string.IsNullOrEmpty(To) || string.IsNullOrEmpty(From)) {
					MessageBox.Show("Win Toolkit can't detect a sources folder for one of the images!", "Missing folder");
					return;
				}

				try {
					lblWIM.Text = "Preparing UEFI boot...";
					Application.DoEvents();
					if (Directory.Exists(To + "\\efi\\microsoft\\boot") && !Directory.Exists(To + "\\efi\\boot")) {
						cMain.CopyDirectory(To + "\\efi\\microsoft\\boot", To + "\\efi\\boot", false, true);
						cMain.WriteResource(Properties.Resources.bootx64, To + "\\efi\\boot\\bootx64.efi", this);
					}
				}
				catch (Exception Ex) { new SmallError("Unable to copy UEFI files.", Ex, To).Upload(); }

				if (Directory.Exists(From + "\\WinToolkit_Apps")) {
					lblWIM.Text = "Copying WinToolkit_Apps folder...";
					cMain.CopyDirectory(From + "\\WinToolkit_Apps", To + "\\WinToolkit_Apps", false, true);
				}


				if (!Directory.Exists(To + "\\sourc64")) {

					lblWIM.Text = "Copying sourc64 folder...";
					cMain.CopyDirectory(From + "\\sources", To + "\\sourc64", false, true, "install.wim");
					lblWIM.Text = "Getting GUID...";

					string GUID = cMain.RunExternal("\"" + cMain.SysFolder + "\\bcdedit.exe\"", "/store \"" + To + "\\boot\\bcd\" /copy {default} /d \"x64 Recovery Mode\"");
					while (GUID.ContainsIgnoreCase("{")) {
						GUID = GUID.Substring(1);
					}
					while (GUID.ContainsIgnoreCase("}")) {
						GUID = GUID.Substring(0, GUID.Length - 1);
					}
					lblWIM.Text = "Setting GUID...";

					cMain.RunExternal("\"" + cMain.SysFolder + "\\bcdedit.exe\"", "/store \"" + To + "\\boot\\bcd\" /set {" + GUID + "} device ramdisk=[boot]\\sourc64\\boot.wim,{7619dcc8-fafe-11d9-b411-000476eba25f}");
					cMain.RunExternal("\"" + cMain.SysFolder + "\\bcdedit.exe\"", "/store \"" + To + "\\boot\\bcd\" /set {" + GUID + "} osdevice ramdisk=[boot]\\sourc64\\boot.wim,{7619dcc8-fafe-11d9-b411-000476eba25f}");
					cMain.RunExternal("\"" + cMain.SysFolder + "\\bcdedit.exe\"", "/store \"" + To + "\\boot\\bcd\" /timeout 3");

					lblWIM.Text = "Update Autorun.exe...";
					cMain.WriteResource(Resources.WIMAIO64, cMain.UserTempPath + "\\WIMAIO.7z", this);
					cMain.ExtractFiles(cMain.UserTempPath + "\\WIMAIO.7z", To, this, "*.*", false);
					lblWIM.Text = "Finishing...";
				}
			}
		}

		public void mImagex(string Argument) {
			string sImagexTemp = cMain.UserTempPath + "\\WinToolkit_Imagex";
			cMain.WriteResource(Resources.imagex, cMain.UserTempPath + "\\Files\\Imagex.exe", this);
			var p = new Process();
			p.StartInfo.FileName = "\"" + cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe") + "\"";
			p.StartInfo.Arguments = Argument;
			p.StartInfo.CreateNoWindow = true;
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardError = true;
			p.OutputDataReceived += OnDataReceived;
			p.StartInfo.Arguments += " /Temp \"" + sImagexTemp + "\"";
			Files.DeleteFolder(sImagexTemp, true);
			p.Start();
			p.BeginOutputReadLine();
			p.WaitForExit();
			cMain.AppErrC = p.ExitCode;
			p.Close();
			Files.DeleteFolder(sImagexTemp, false);

		}

		private void OnDataReceived(object sender, DataReceivedEventArgs e) {
			if (!string.IsNullOrEmpty(e.Data)) {
				if (e.Data.ContainsIgnoreCase("%")) {
					string T = e.Data;
					while (!T.EndsWithIgnoreCase("]")) {
						T = T.Substring(0, T.Length - 1);
					}
					lblWIM.Text = "Creating AIO " + T + " (" + nStatus + ")";
					Application.DoEvents();
				}
				else {
					if (e.Data.ContainsIgnoreCase("[") || e.Data.ContainsIgnoreCase("#")) {
						iErr += e.Data + Environment.NewLine;
					}
				}
			}
		}

		private void BWR_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			cmdMI.Enabled = false;
			TaskInProgress(true);
			if (lstImages.Groups.Count == 0) {
				lblWIM.Text = "Please select a WIM File > > >";
			    lstImages.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
			if (lstImages.Groups.Count == 1) {
				lblWIM.Text = "Please select another WIM File > > >";
			    lstImages.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
			if (lstImages.Groups.Count > 1) {
				cmdMI.Enabled = true;
				lblWIM.Text = "You have enough to create an AIO disk, you may select more > > >";
			}
			ToolStrip1.Enabled = lstImages.Groups.Count > 0;
		}

		private void BWMerge_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			TaskInProgress(true);
			cmdMI.Text = "Create";
			lstImages.Items.Clear();
			lstImages.Groups.Clear();
			cmdMI.Enabled = true;
			cmdBrowse.Enabled = true;
			if (BWR.IsBusy == false) {
				BWR.RunWorkerAsync();
			}
		}

		private static string GetWIMValue(string L) {
			while (L.ContainsIgnoreCase(">")) {
				L = L.Substring(1);
			}
			return L;
		}

		private void cmdBrowse_Click(object sender, EventArgs e) {
			if (lstImages.Items.Cast<ListViewItem>().Any(I => I.Font.Bold && I.Group.Header != "Already Mounted Images")) {
				MessageBox.Show("An image is still mounted, please unmount first!", "Mounted image");
				return;
			}

			OFD.Title = "Select WIM File...";
			OFD.Filter = "Windows Image *.wim *.swm|*.wim;*.swm";
           
            nWIM = "";
			if (OFD.ShowDialog() != DialogResult.OK) {
				return;
			}
			//MessageBox.Show(OFD.FileName);
			if (lstImages.Items.Cast<ListViewItem>().Any(I => I.Group.Header.ToUpper() == OFD.FileName.ToUpper())) {
				MessageBox.Show("You have already added this WIM file!", "Abort2");
				return;
			}

			string SWM = cMount.CWIM_GetWimInfo(OFD.FileName);
			if (!SWM.ContainsIgnoreCase(": 1/1")) {
				MessageBox.Show(
					 "This seems to be an *.swm file which is read-only and not supported by WinToolkit. You need to merge this back into an *.wim file first!",
					 "SWM Detected!");
				return;
			}
			nWIM = OFD.FileName;
			cmdMI.Enabled = false;
			TaskInProgress(false);
			lblWIM.Text = "Loading WIM :: " + OFD.FileName;
			BWR.RunWorkerAsync();
		}

		private void BWR_DoWork(object sender, DoWorkEventArgs e) {


			Application.DoEvents();
			try {
				string WimInfo;
				var LVG = new ListViewGroup();

				if (!string.IsNullOrEmpty(nWIM)) {
                    WimInfo = cMount.CWIM_GetWimImageInfo(nWIM, lblWIM);
					LVG.Header = nWIM;
				}
				else {
					WimInfo = cMount.CWIM_GetWimImageInfo(OFD.FileName,lblWIM);
					LVG.Header = OFD.FileName;
				}

				if (string.IsNullOrEmpty(WimInfo)) {
					return;
				}

				int N = 1;

				bool x86 = false;
				bool x861st = false;

				try {
					if (lstImages.Groups[0].Tag != "x64") {
						x861st = true;
					}
				}
				catch {
				}
				foreach (string mImage in Regex.Split(WimInfo, "<IMAGE INDEX=", RegexOptions.IgnoreCase)) {
					if (!mImage.ContainsIgnoreCase("<WIM>")) {
						var NI = new ListViewItem();
						string name = "N/A", dName = "", Description = "", dDesc = "", size = "N/A", Arc = "??", Build = "??", Flag = "??";

						foreach (string mLine in mImage.Split('<')) {
							string Line = mLine.ReplaceIgnoreCase( "\r", "");
							Line = Line.ReplaceIgnoreCase("\n", "");

							if (Line.StartsWithIgnoreCase("EDITIONID>")) {
								NI.Tag = GetWIMValue(Line);
							}
							if (Line.StartsWithIgnoreCase("NAME>")) {
								name = GetWIMValue(Line);
							}
							if (Line.StartsWithIgnoreCase("DISPLAYNAME>")) {
								dName = GetWIMValue(Line);
							}
							if (Line.StartsWithIgnoreCase("FLAGS>")) {
								Flag = GetWIMValue(Line);
							}
							if (Line.StartsWithIgnoreCase("DESCRIPTION>")) {
								Description = GetWIMValue(Line);
							}
							if (Line.StartsWithIgnoreCase("DISPLAYDESCRIPTION>")) {
								dDesc = GetWIMValue(Line);
							}
							if (Line.StartsWithIgnoreCase("ARCH>")) {
								if (GetWIMValue(Line).EqualsIgnoreCase("0")) {
									Arc = "x86";
								}
								if (GetWIMValue(Line).EqualsIgnoreCase("9")) {
									Arc = "x64";
								}
							}
							if (Line.StartsWithIgnoreCase("BUILD>")) {
								Build = GetWIMValue(Line);
							}
							if (Line.StartsWithIgnoreCase("TOTALBYTES>")) {
								size = cMain.BytesToString(Convert.ToDouble(GetWIMValue(Line)));
							}
						}
						NI.Text = name;
						if (string.IsNullOrEmpty(dName)) { dName = name; }

						NI.Group = LVG;

						if (string.IsNullOrEmpty(Description)) {
							Description = "No Description";
						}
						NI.SubItems.Add(Description);
						NI.SubItems.Add(Arc);

						NI.SubItems.Add(Build);
						NI.SubItems.Add(size);
						NI.ToolTipText = dName + Environment.NewLine + cOptions.LastWim;
						NI.SubItems.Add(Convert.ToString(N));
						NI.SubItems.Add(cOptions.LastWim);
						NI.SubItems.Add("");

						NI.SubItems.Add(dName);
						NI.SubItems.Add(dDesc);
						NI.SubItems.Add(Flag);
						NI.ToolTipText = cMain.UpdateTooltips(name, Description, dName, dDesc, Flag);
						lstImages.Items.Add(NI);

						N += 1;
					}
				}

				if (x861st == false && x86) {
					lstImages.Groups.Insert(0, LVG);
				}
				else {
					lstImages.Groups.Add(LVG);
				}
			}
			catch (Exception Ex) {
				MessageBox.Show(Ex.Message, "Error");
			}

			try {
				cmdEI.Visible = false;
				cmdEI.Checked = false;
				string EI = lstImages.Groups[0].Header;
				while (!EI.EndsWithIgnoreCase("\\")) {
					EI = EI.Substring(0, EI.Length - 1);
				}
				if (File.Exists(EI + "ei.cfg")) {
					cmdEI.Visible = true;
					cmdEI.Checked = true;
				}
			}
			catch {
			}

			try {
				cmdRecovery64.Visible = false;
				if (lstImages.Groups.Count > 0) {
					bool compat = false;

					string DVD = lstImages.Groups[0].Header;

					if (DVD.ContainsIgnoreCase("SOURCES\\INSTALL.WIM")) {
						DVD = DVD.ReplaceIgnoreCase("sources\\install.wim", "");
						if (!Directory.Exists(DVD + "sourc64")) { compat = true; }
					}
					int x86 = 0;
					int x64 = 0;

					foreach (ListViewGroup LVG in lstImages.Groups) {
						int x86t = 0;
						int x64t = 0;
						foreach (ListViewItem LST in LVG.Items) {
                            if (LST.SubItems[2].Text.EqualsIgnoreCase("x86"))
                            {
								x86t += 1;
							}
							if (LST.SubItems[2].Text.EqualsIgnoreCase("x64")) {
								x64t += 1;
							}
						}
						if (x86t == 0 && x64t > 0) { LVG.Tag = "x64"; x64 += 1; }
						if (x86t > 0 && x64t == 0) { LVG.Tag = "x86"; x86 += 1; }
						if (x86t > 0 && x64t > 0) { LVG.Tag = "Mixed"; x86 += 1; }
					}

					if (x86 > 0 && x64 > 0 && compat) { cmdRecovery64.Visible = true; }
				}
			}
			catch (Exception Ex) { MessageBox.Show(Ex.Message); }
		}

		private void CheckDuplicate() {
			foreach (ListViewItem LST in lstImages.Items) {
				LST.BackColor = Color.Yellow;
				int T = 0;
				foreach (ListViewItem LST2 in lstImages.Items) {
					if (LST2.Text == LST.Text) {
						T += 1;
						LST.BackColor = Color.LightPink;
					}
				}
				if (T > 1) {
					dupli = true;
					LST.BackColor = Color.LightPink;
				}
				else {
					LST.BackColor = Color.LightGreen;
				}
			}
		}

		private void cmdMI_Click(object sender, EventArgs e) {
            if (cmdMI.Text.EqualsIgnoreCase("Create") && BWMerge.IsBusy == false)
            {
				if (lstImages.Groups.Count <= 1) {
					MessageBox.Show("You need at least two WIM files", "N/A");
					return;
				}
				dupli = false;
				CheckDuplicate();

				if (dupli) {
					lblWIM.Text = "Attempting to auto-rename images to prevent duplication errors...";
					Application.DoEvents();

					foreach (ListViewGroup LVG in lstImages.Groups) {
						string S = cMount.CWIM_GetWimImageInfo(LVG.Header, lblWIM);
						foreach (ListViewItem LST in LVG.Items) {
							if (!LST.Text.ContainsIgnoreCase(LST.SubItems[2].Text.ToUpper())) {
								S = cMount.RenameImage(S, "NAME", int.Parse(LST.SubItems[5].Text), LST.Text, LST.Text + " " + LST.SubItems[2].Text);
								LST.Text = LST.Text + " " + LST.SubItems[2].Text;
							}
						}
						if (!string.IsNullOrEmpty(S)) { cMount.CWIM_SetWimInfo(LVG.Header, S); }
					}
				}

				lblWIM.Text = "Checking for more duplicate names...";
				dupli = false;
				CheckDuplicate();
				if (dupli) {
					MessageBox.Show(
						 "Some images from the different WIM files have the same name, rename one of the images!",
						 "Duplicate Names Detected!");
					lblWIM.Text = "Please rename some images...";
					return;
				}

				if (cmdEI.Checked) {
					try {
						string L = lstImages.Groups[0].Header;
						while (!L.EndsWithIgnoreCase("\\")) {
							L = L.Substring(0, L.Length - 1);
						}
						if (File.Exists(L + "ei.cfg")) {
							File.Move(L + "ei.cfg", L + "ei.cfg.bak");
						}
						cmdEI.Checked = false;
						cmdEI.Visible = false;
					}
					catch {
					}
				}
				cmdBrowse.Enabled = false;
				cmdMI.Text = "Cancel";
				TaskInProgress(false);
				nWIM = lstImages.Groups[0].Header;

				BWMerge.RunWorkerAsync();
			}
			else {
				cmdMI.Enabled = false;
				BWMerge.CancelAsync();
				nStatus = "Stopping";
				lblWIM.Text = "Stopping";
				cMain.KillProcess("Imagex.exe");
			}
		}

		private void cmdClear_Click(object sender, EventArgs e) {
			DialogResult DR = MessageBox.Show("Are you sure you wish to clear all of images from the list?", "Are you sure?", MessageBoxButtons.YesNo);
			if (DR == DialogResult.Yes) {
				nWIM = "";
				lstImages.Items.Clear();
				lstImages.Groups.Clear();
				lblWIM.Text = "Please select a WIM File...\nNOTE: If you are merging x86 and x64 images, Win Toolkit will place the 1st x86 image at the top!";
				cmdEI.Visible = false;
				ToolStrip1.Enabled = false;
			}
		}

		private void cmdEditName_Click(object sender, EventArgs e) {
			if (lstImages.SelectedItems.Count == 0) {
				MessageBox.Show("Please select at least one item!", "Invalid Selection");
				return;
			}
			string S = cMount.CWIM_GetWimImageInfo(lstImages.SelectedItems[0].Group.Header,lblWIM);
			string T = "";
			foreach (ListViewItem LST in lstImages.SelectedItems) {
				string Answer = cMain.InputBox("Please enter a new name for the image", "New Name", LST.Text);
				if (Answer == LST.Text || string.IsNullOrEmpty(Answer)) { return; }
				if (lstImages.FindItemWithText(Answer, false, 0) != null && lstImages.FindItemWithText(Answer, false, 0).Text == Answer) {
					MessageBox.Show("'" + Answer + "' name already exists!", "Invalid Name");
					return;
				}

				T = cMount.RenameImage(S, "NAME", int.Parse(LST.SubItems[5].Text), LST.Text, Answer);
				LST.Text = Answer;
				LST.ToolTipText = cMain.UpdateTooltips(LST.Text, LST.SubItems[1].Text, LST.SubItems[8].Text, LST.SubItems[9].Text, LST.SubItems[10].Text);
			}
			cMount.CWIM_SetWimInfo(lstImages.SelectedItems[0].Group.Header, T);
		}

		private void cmdEditDesc_Click(object sender, EventArgs e) {
			if (lstImages.SelectedItems.Count == 0) {
				MessageBox.Show("Please select at least one item!", "Invalid Selection");
				return;
			}

			string S = cMount.CWIM_GetWimImageInfo(lstImages.SelectedItems[0].Group.Header,lblWIM);
			string T = "";
			foreach (ListViewItem LST in lstImages.SelectedItems) {
				string Answer = cMain.InputBox("Please enter a new description for the image", "New Description",
																 LST.SubItems[1].Text);
				if (string.IsNullOrEmpty(Answer) || Answer == LST.SubItems[1].Text) {
					return;
				}
				T = cMount.RenameImage(S, "DESCRIPTION", int.Parse(LST.SubItems[5].Text), LST.SubItems[1].Text, Answer);
				LST.SubItems[1].Text = Answer;
				LST.ToolTipText = cMain.UpdateTooltips(LST.Text, LST.SubItems[1].Text, LST.SubItems[8].Text, LST.SubItems[9].Text, LST.SubItems[10].Text);
			}

			cMount.CWIM_SetWimInfo(lstImages.SelectedItems[0].Group.Header, T);
		}

		private void cmdMakeISO_Click(object sender, EventArgs e) {
			if (lstImages.Items.Count == 0) {
				MessageBox.Show("You need to select an image first!", "Invalid Image");
				return;
			}

			string F = lstImages.Groups[0].Header;
			if (F.ContainsIgnoreCase("\\SOURCES\\INSTALL.WIM")) {
				F = F.Substring(0, F.Length - 20);
			}

			var TS = new frmISOMaker();
           
			if (!string.IsNullOrEmpty(F)) {
				TS.lblFolder.Text = F;
				TS.cmdFBrowse.Enabled = false;
				while (F.ContainsIgnoreCase("\\")) {
					F = F.Substring(1);
				}
				TS.txtName.Text = F;
			}
            Selected = true;
			TS.Show();
			Close();
		}

		private void cmdDeleteImage_Click(object sender, EventArgs e) {
			if (lstImages.SelectedItems.Count == 0) {
				MessageBox.Show("Please select at least one item!", "Invalid Selection");
				return;
			}

			if (lstImages.SelectedItems[0].Group.Items.Count < 2) {
				MessageBox.Show("The last image within a (.wim) file can't be deleted!", "Access Denied");
				return;
			}

			DialogResult Dresult = MessageBox.Show("Are you sure you wish to permanently delete the selected image?",
																"Are you sure?", MessageBoxButtons.YesNo);
			if (Dresult != DialogResult.Yes) {
				return;
			}
			TaskInProgress(false);
			foreach (ListViewItem LST in lstImages.SelectedItems) {
				cMain.OpenProgram("\"" + cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe") + "\"", "/DELETE \"" + LST.SubItems[6].Text + "\" \"" + LST.Text + "\"", true, ProcessWindowStyle.Hidden);
				int dI = 1;
				foreach (ListViewItem LST2 in LST.Group.Items) {
					if (LST2.Text != LST.Text) {
						LST2.SubItems[5].Text = dI.ToString(CultureInfo.InvariantCulture);
						dI += 1;
					}
				}
				LST.Remove();
			}

			TaskInProgress(true);
			ToolStrip1.Enabled = lstImages.Items.Count > 0;
		}

		private void cmdMakeUSB_Click(object sender, EventArgs e) {
			cmdMakeUSB.Enabled = false;
			new frmUSBPrep().Show();
			Selected = true;
			Close();
		}

		private void cmdEI_Click(object sender, EventArgs e) {
			if (cmdEI.Checked && cmdEI.Visible) {
				cmdEI.Image = Resources.Checked;
			}
			else {
				cmdEI.Image = Resources.Unchecked;
			}
		}

		private void cmdRecovery64_Click(object sender, EventArgs e) {
			cmdRecovery64.Image = cmdRecovery64.Checked ? Resources.Checked : Resources.Unchecked;
		}

		private void mnuEditDName_Click(object sender, EventArgs e) {
			if (lstImages.SelectedItems.Count == 0) {
				MessageBox.Show("Please select at least one item!", "Invalid Selection");
				return;
			}
			string S = cMount.CWIM_GetWimImageInfo(lstImages.SelectedItems[0].Group.Header,lblWIM);
			string T = "";
			foreach (ListViewItem LST in lstImages.SelectedItems) {
				string Answer = cMain.InputBox("Please enter a new display name for the image", "New Display Name", LST.SubItems[8].Text);
				if (Answer == LST.SubItems[8].Text || string.IsNullOrEmpty(Answer)) { return; }

				T = cMount.RenameImage(S, "DISPLAYNAME", int.Parse(LST.SubItems[5].Text), LST.SubItems[8].Text, Answer);
				LST.SubItems[8].Text = Answer;
				LST.ToolTipText = cMain.UpdateTooltips(LST.Text, LST.SubItems[1].Text, LST.SubItems[8].Text, LST.SubItems[9].Text, LST.SubItems[10].Text);
			}
			cMount.CWIM_SetWimInfo(lstImages.SelectedItems[0].Group.Header, T);
		}

		private void mnuEditDDesc_Click(object sender, EventArgs e) {
			if (lstImages.SelectedItems.Count == 0) {
				MessageBox.Show("Please select at least one item!", "Invalid Selection");
				return;
			}
			string S = cMount.CWIM_GetWimImageInfo(lstImages.SelectedItems[0].Group.Header,lblWIM);
			string T = "";
			foreach (ListViewItem LST in lstImages.SelectedItems) {
				string Answer = cMain.InputBox("Please enter a new display description for the image", "New Display Description", LST.SubItems[9].Text);
				if (Answer == LST.SubItems[9].Text || string.IsNullOrEmpty(Answer)) { return; }

				T = cMount.RenameImage(S, "DISPLAYDESCRIPTION", int.Parse(LST.SubItems[5].Text), LST.SubItems[9].Text, Answer);
				LST.SubItems[9].Text = Answer;
				LST.ToolTipText = cMain.UpdateTooltips(LST.Text, LST.SubItems[1].Text, LST.SubItems[8].Text, LST.SubItems[9].Text, LST.SubItems[10].Text);
			}
			cMount.CWIM_SetWimInfo(lstImages.SelectedItems[0].Group.Header, T);
		}

	}
}