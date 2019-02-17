using System;
using System.IO;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.Helpers;
using WinToolkit.Properties;

namespace WinToolkit.Prompts {
	public partial class frmUnattendedPrompt : Form {
		private bool Full;
		private bool Quick = true;
		public string WIM = "";

		public frmUnattendedPrompt() {
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		private void frmUPrompt_Load(object sender, EventArgs e) {
			cMain.UnattendedF = "";
            splitContainer1.Scale4K(_4KHelper.Panel.Pan1);
            splitContainer2.Scale4K(_4KHelper.Panel.Pan2);
			string DVD = GetDVD(WIM);

			if (File.Exists(DVD + "\\sources\\boot.wim")) {
				Full = true;
			}
			if (File.Exists(DVD + "\\sources\\full.txt")) {
				Quick = false;
			}
			cMain.FormIcon(this);
			cmdRBrowse.Image = Resources.folder_48;
			cMain.SetToolTip(cmdFull,
								  "Some USB 3.0 and external HDD users have been reporting that the usual method to" +
								  Environment.NewLine +
								  "add unattended file to DVD root does not work, this method will integrate the" +
								  Environment.NewLine + "unattended file into 2nd boot.wim image.",
								  "Full Unattended Integration");
			cMain.SetToolTip(cmdQuick,
								  "If your unattended file usually works when copying your unattended.xml to the DVD" +
								  Environment.NewLine + "root then use this option.", "Quick Unattended Integration");
			cMain.SetToolTip(cmdCancel, "This will close this screen.", "Cancel");
			if (Quick == false || Full == false) {
				if (Quick == false) {
					txtNotice.Text =
						 "Quick has been disabled because you have previously used 'Full' method with this image.";
				}

				if (Full == false) {
					txtNotice.Text = "Full has been disabled because Win Toolkit can't find 'DVD\\Sources\\boot.wim' file.";
				}
			}
		}

		private string GetDVD(string WIMLoc) {
			string DVDRoot = WIMLoc;
			if (DVDRoot.ContainsIgnoreCase("\\SOURCES\\")) {
				while (!DVDRoot.EndsWithIgnoreCase("\\")) {
					DVDRoot = DVDRoot.Substring(0, DVDRoot.Length - 1);
				}
				DVDRoot = DVDRoot.Substring(0, DVDRoot.Length - 1);
				while (!DVDRoot.EndsWithIgnoreCase("\\")) {
					DVDRoot = DVDRoot.Substring(0, DVDRoot.Length - 1);
				}
			}
			else {
				DVDRoot = "";
			}
			return DVDRoot;
		}

		private void cmdRBrowse_Click(object sender, EventArgs e) {
			var OFD = new OpenFileDialog();
			OFD.Multiselect = false;
			OFD.Title = "Select Unattended...";
			OFD.Filter = "Unattended *.xml| *.xml";

			if (OFD.ShowDialog() != DialogResult.OK) {
				return;
			}
			txtRInfo.Text = OFD.FileName;

			if (Full && Quick) {
				txtNotice.Text = "Everything should work fine...";
			}
		}

		private void txtRInfo_TextChanged(object sender, EventArgs e) {
			if (txtRInfo.Text.ToUpper().EndsWithIgnoreCase(".XML")) {
				if (Full) {
					cmdFull.Enabled = true;
				}
				if (Quick) {
					cmdQuick.Enabled = true;
				}
			}
			else {
				cmdFull.Enabled = false;
				cmdQuick.Enabled = false;
			}
		}

		private void cmdCancel_Click(object sender, EventArgs e) {
			Close();
		}

		private void cmdQuick_Click(object sender, EventArgs e) {
			cMain.UnattendedF = txtRInfo.Text;
			Close();
		}

		private void cmdFull_Click(object sender, EventArgs e) {
			cMain.UnattendedF = "|" + txtRInfo.Text;
			Close();
		}
	}
}