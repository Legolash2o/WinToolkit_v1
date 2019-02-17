using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinToolkit.Classes.Helpers;

namespace WinToolkit {
	public partial class frmRegMount : Form {
		
		private bool ShowReg = true;
		private bool sImport;
		private bool sLoad = true;
		private bool sUnLoad;

        
        WIMImage sImage = cMain.selectedImages[0];

		public frmRegMount() {
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			FormClosing += frmRegMount_FormClosing;
			FormClosed += frmRegMount_FormClosed;
			Shown += frmRegMount_Shown;
			Activated += cMain.FormActivated;
			Deactivate += cMain.FormActivated;
		}

		private void frmRegMount_Shown(object sender, EventArgs e) {
			lstRegs.Enabled = false;
			cmdSelect.Visible = false;
			cmdIReg.Visible = false;

			cMain.UpdateToolStripLabel(lblStatus, "Dismounting previous hives...");
			Application.DoEvents();
			cReg.RegUnLoadAll();
			cMain.UpdateToolStripLabel(lblStatus, "Mounting Image...");
			Application.DoEvents();

           
            sImage.Mount(lblStatus,this);

			if (string.IsNullOrEmpty(sImage.MountPath)) {
				MessageBox.Show("Win Toolkit was not able to mount the wim file", "Error");
				Close();
				return;
			}

			cMain.UpdateToolStripLabel(lblStatus, "Checking for mounting registry hives...");
			Application.DoEvents();
			foreach (ListViewItem LST in lstRegs.Items) {
				if (!File.Exists(sImage.MountPath+ "\\" + LST.SubItems[3].Text)) {
					LST.Remove();
				}
			}

			if (lstRegs.Items.Count == 0) {
				MessageBox.Show("There doesn't seem to be any registry files to mount." + Environment.NewLine + sImage.MountPath + "\\", "Aborting");
				Mounting = false;
				Close();
			}
			lstRegs.Enabled = true;
			cmdSelect.Visible = true;
			cmdIReg.Visible = true;
			cMain.UpdateToolStripLabel(lblStatus, "");
			Mounting = false;
		}
		bool Mounting = true;
		private void frmRegMount_Load(object sender, EventArgs e) {
            SplitContainer1.Scale4K(_4KHelper.Panel.Pan2);
			cMain.FormIcon(this); cMain.eLBL = lblStatus; cMain.eForm = this;
			cMain.ToolStripIcons(ToolStrip1);
			ColumnHeader2.Width = -2;
			Text = "WIM Registry Editor (" + cMain.selectedImages[0].Name + ")";
			Mounting = true;
		}
		bool FClosing;
		private void frmRegMount_FormClosing(object sender, FormClosingEventArgs e) {
			if (FClosing) { e.Cancel = true; }
			cMain.OnFormClosing(this);
			if (FClosing == false) {
				FClosing = true;
				if (Mounting) {
					MessageBox.Show("You can't close this tool whilst it's still loading.", "Access Denied");
					FClosing = false;
					e.Cancel = true;
					return;
				}

				lstRegs.Enabled = false;
				cmdSelect.Visible = false;
				cmdIReg.Visible = false;
				cmdU.Visible = false;
                if (!string.IsNullOrEmpty(sImage.MountPath)) {
					cMain.UpdateToolStripLabel(lblStatus, "Unloading Hives...");
					Application.DoEvents();
					cReg.RegUnLoadAll();
					foreach (ListViewItem LST in lstRegs.Items) {
						LST.BackColor = Color.White;
					}
					cMain.UpdateToolStripLabel(lblStatus, "Unloading Image...");
					Application.DoEvents();
                    
                    sImage.UnMount(lblStatus,this);

					if (cMount.uChoice == cMount.MountStatus.None) {
						lstRegs.Enabled = true;
						cmdSelect.Visible = true;
						cmdIReg.Visible = true;
						cMain.UpdateToolStripLabel(lblStatus, "");
						FClosing = false;
						e.Cancel = true;
					}
				}
			}
		}

		private void frmRegMount_FormClosed(object sender, FormClosedEventArgs e) {
			cMain.ReturnME();
		}

		private void cmdUD_Click(object sender, EventArgs e) {
			foreach (ListViewItem Reg in lstRegs.Items) {
				Reg.Selected = Reg.BackColor == Color.LightGreen;
			}

			cmdUS.PerformClick();
			cmdSelect.Visible = true;
			cmdU.Visible = false;
		}

		private void cmdMA_Click(object sender, EventArgs e) {
			int c = 0;
			foreach (ListViewItem Reg in lstRegs.Items) {
				if (Reg.BackColor != Color.LightGreen) {
					Reg.Selected = true;
				}
				else {
					Reg.Selected = false; c += 1;
				}
			}

			if (c != lstRegs.Items.Count) { cmdMS.PerformClick(); }
			cmdSelect.Visible = false;
			cmdU.Visible = true;
		}

		private void cmdMS_Click(object sender, EventArgs e) {
			if (lstRegs.SelectedItems.Count == 0) {
				MessageBox.Show("Please select at least 1 item!", "Invalid Item");
				return;
			}

			bool Mounted = false;
            string SHiveL = sImage.MountPath + "\\";
			Enable(false);
			foreach (ListViewItem Reg in lstRegs.SelectedItems) {
				if (Reg.BackColor == Color.LightGreen) { continue; }
				cMain.UpdateToolStripLabel(lblStatus, "Loading " + Reg.Text + "...");
				Application.DoEvents();
				try {
					cReg.RegLoad(Reg.SubItems[1].Text, SHiveL + Reg.SubItems[3].Text);

					if (cReg.RegCheckMounted(Reg.SubItems[1].Text)) {
						Reg.BackColor = Color.LightGreen;
						Mounted = true;
					}
				}
				catch {
					MessageBox.Show(Reg.Text + " has not been loaded", "Error");
				}
			}

			if (Mounted) {
				cmdSelect.Visible = false;
				cmdU.Visible = true;
				if (ShowReg && lstRegs.SelectedItems.Count > 0) {
					cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Applets\\Regedit", "LastKey", "Computer\\HKEY_LOCAL_MACHINE\\" + lstRegs.SelectedItems[0].SubItems[1].Text);
				}
				Process.Start("regedit.exe");
			}

			lstRegs.SelectedItems.Clear();

			Enable(true);
			cMain.UpdateToolStripLabel(lblStatus, "");
		}

		private void Enable(bool E) {
			if (E == false) {
				sLoad = cmdSelect.Visible;
				sUnLoad = cmdU.Visible;
				sImport = cmdIReg.Visible;
				cmdSelect.Visible = false;
				cmdU.Visible = false;
				cmdIReg.Visible = false;
			}
			else {
				cmdSelect.Visible = sLoad;
				cmdU.Visible = sUnLoad;
				cmdIReg.Visible = sImport;
			}
			lstRegs.Enabled = E;

			Application.DoEvents();
		}

		private void cmdUS_Click(object sender, EventArgs e) {
			Enable(false);

			foreach (ListViewItem Reg in lstRegs.SelectedItems) {
				cMain.UpdateToolStripLabel(lblStatus, "Unloading " + Reg.Text);
				Application.DoEvents();
				try {
					cReg.RegUnLoad(Reg.SubItems[1].Text);
					Reg.BackColor = Color.White;
				}
				catch {
					MessageBox.Show(Reg.Text + " has not been unloaded", "Error");
				}
			}
			Enable(true);

			cMain.UpdateToolStripLabel(lblStatus, "");
			cmdU.Visible = false;
			cmdSelect.Visible = true;
			lstRegs.SelectedItems.Clear();

		}

		private void cmdIReg_Click(object sender, EventArgs e) {
			var OFD = new OpenFileDialog();
			OFD.Title = "Select Registry File";
			OFD.Filter = "Registry File *.reg|*.reg";
			OFD.Multiselect = true;

			if (OFD.ShowDialog() != DialogResult.OK) {
				return;
			}

			ShowReg = false;
			Enable(false);

			Application.DoEvents();
			bool Mounted = false;
            
            string SHiveL = sImage.MountPath + "\\";

			foreach (ListViewItem Reg in lstRegs.Items) {
				if (Reg.BackColor == Color.LightGreen) { continue; }
				cMain.UpdateToolStripLabel(lblStatus, "Loading " + Reg.Text + "...");
				Application.DoEvents();

				cReg.RegLoad(Reg.SubItems[1].Text, SHiveL + Reg.SubItems[3].Text);

				if (cReg.RegCheckMounted(Reg.SubItems[1].Text)) {
					Reg.BackColor = Color.LightGreen;
					Mounted = true;
				}
			}

			ShowReg = true;
			foreach (string S in OFD.FileNames) {
				cMain.UpdateToolStripLabel(lblStatus, "Converting " + cMain.GetFName(S) + "...");
				Application.DoEvents();
				cAddon.ConvertRegAddon(S, cMain.selectedImages[0].Architecture.ToString(), false,sImage);
			}

			cMain.UpdateToolStripLabel(lblStatus, "");
			Enable(true);

			MessageBox.Show("The selected registry files have been imported!", "Done");
		}

		private void lstRegs_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			if (e.Item.BackColor == Color.LightGreen) {
				cmdSelect.Visible = false;
				cmdU.Visible = true;
			}
			else {
				cmdSelect.Visible = true;
				cmdU.Visible = false;
			}
		}

		private void lstRegs_DoubleClick(object sender, EventArgs e) {
			if (lstRegs.SelectedItems.Count == 1) { cmdMS.PerformClick(); }
		}
	}
}