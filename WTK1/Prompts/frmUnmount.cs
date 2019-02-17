using System;
using System.Diagnostics;
using System.Windows.Forms;
using WinToolkit.Classes;
using System.IO;
namespace WinToolkit.Prompts {
	public partial class frmUnmount : Form {
		private string _imageName = "";
	    private string _mountPath = "";

		public frmUnmount(string mountPath, string imageName) {
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			CheckForIllegalCrossThreadCalls = false;
			FormClosing += frmUnmount_Closing;
			Activated += cMain.FormActivated;
			Deactivate += cMain.FormActivated;

		    _mountPath = mountPath;
		    _imageName = imageName;
		}

		private void frmUnmount_Load(object sender, EventArgs e) {   //vLite, RTWin7Lite, Regedit, DISM, PkgMgr, DismHost
			splitContainer1.SplitterDistance = pictureBox5.Top + pictureBox5.Height + 10;
			cMain.FormIcon(this);
			cMain.SetToolTip(cmdSaveRebuild, "This will save all changes you have made to the wim file.\n\nWhen making changes even when removing components, your \ninstall.wim will increase in size. Rebuilding your image will \nre-compress all of the images, making it smaller and therefore\ngive you a smaller ISO.", "Shrink Image");
			cMain.SetToolTip(cmdSave, "This will save all changes you have made to the wim file.", "Save Image");
			cMain.SetToolTip(cmdDiscard, "This will unmount the image without saving ANY changes you have made.", "Discard Changes");
			cMain.SetToolTip(cmdKeep, "This will keep the image mounted so you can continue working on\nit. However, please make sure you save it later.", "Discard Changes");

            cMain.SetToolTip(chkDeleteMount,"Determines where or not the mount image folder is deleted after successful dismount.");
            cMain.SetToolTip(cmdCleanMGR,"Help reduce the size of your image by removing useless files.");

			if (!string.IsNullOrEmpty(_imageName)) {
				lblImage.Text = "Image: " + _imageName;
			}
			cmdKeep.Enabled = cMount.SupCan;
			cMount.uChoice = cMount.MountStatus.None;
		    chkDeleteMount.Checked = cOptions.DeleteMount;
                                 
		}



		private void frmUnmount_Closing(object sender, FormClosingEventArgs e) {
			string T = Text;

			if (cMount.uChoice == cMount.MountStatus.Save || cMount.uChoice == cMount.MountStatus.Discard) {
				Text = "Closing registry hives...";
				Application.DoEvents();
				cReg.RegUnLoadAll();
			}

			Text = T;
			Application.DoEvents();

			if (cMount.SupCan && cMount.uChoice == cMount.MountStatus.None) {
				cMain.wimRebuild = false;
			}
			if (cMount.uChoice == cMount.MountStatus.None) {
				e.Cancel = true;
				cMain.wimRebuild = false;
			}
		}

		private void cmdKeep_Click(object sender, EventArgs e) {
			cMain.wimRebuild = false;
			cMount.uChoice = cMount.MountStatus.Keep;
			Close();
		}

		private void cmdDiscard_Click(object sender, EventArgs e) {
			cMain.wimRebuild = false;
			cMount.uChoice = cMount.MountStatus.Discard;
			Close();
		}

		private void cmdSave_Click(object sender, EventArgs e) {
			cMain.wimRebuild = false;
			SaveImage();
		}

		private void SaveImage() {
			string rApps = "";
			Process[] ps = Process.GetProcesses();
			foreach (Process p in ps) {
				if (p.ProcessName.ToUpper().EqualsIgnoreCase("VLITE")) { rApps += "-vLite" + Environment.NewLine; }
				if (p.ProcessName.ToUpper().EqualsIgnoreCase("RTWIN7LITE")) { rApps += "-RTWin7Lite" + Environment.NewLine; }
				if (p.ProcessName.ToUpper().EqualsIgnoreCase("REGEDIT")) { rApps += "-regedit" + Environment.NewLine; }
				if (p.ProcessName.ToUpper().EqualsIgnoreCase("DISM")) { rApps += "-dism" + Environment.NewLine; }
				if (p.ProcessName.ToUpper().EqualsIgnoreCase("PKGMGR")) { rApps += "-pkgmgr" + Environment.NewLine; }
				if (p.ProcessName.ToUpper().EqualsIgnoreCase("DISMHOST")) { rApps += "-dismhost" + Environment.NewLine; }
                if (p.ProcessName.ToUpper().EqualsIgnoreCase("IMAGEX")) { rApps += "-imagex" + Environment.NewLine; }
			}

			if (!string.IsNullOrEmpty(rApps)) {
				DialogResult DR = MessageBox.Show("The following applications are running and could result in your image not being saved properly. Please close these first!" + Environment.NewLine + Environment.NewLine + rApps + Environment.NewLine + "Are you sure you wish to continue?", "WARNING", MessageBoxButtons.YesNo);
				if (DR != DialogResult.Yes) { return; }
			}

			cMount.uChoice = cMount.MountStatus.Save;
			Close();
		}

		private void cmdSaveRebuild_Click(object sender, EventArgs e) {
			cMain.wimRebuild = true;
			SaveImage();
		}

        private void chkDeleteMount_CheckedChanged(object sender, EventArgs e)
        {
            cOptions.DeleteMount = chkDeleteMount.Checked;
        }

        private void cmdCleanMGR_Click(object sender, EventArgs e)
        {
            new frmCleanup(_mountPath).ShowDialog();
        }

	}
}