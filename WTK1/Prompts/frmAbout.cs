using System;
using System.IO;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.Helpers;

namespace WinToolkit.Prompts {
	public partial class frmAbout : Form {
		public frmAbout() {
			InitializeComponent();
		}

		private void frmAbout_Shown(object sender, EventArgs e) {
			txtCL.Select();
			txtCL.SelectionLength = 0;
		}

		private void frmAbout_Load(object sender, EventArgs e) {
            scAbout.Scale4K(_4KHelper.Panel.Pan1);
			cMain.FormIcon(this);

			lblVersion.Text = "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

			cmdKey.Visible = cOptions.ValidKey;
		
			int N = 0, C = 0, B = 0;
			bool Cb = true;
			foreach (string I in txtCL.Lines) {
				if (I.ContainsIgnoreCase("*")) {
					N += 1;
					if (Cb) { C += 1; if (I.ContainsIgnoreCase("FIX:")) { B += 1; } }
				}
				if (string.IsNullOrEmpty(I)) { Cb = false; }
			}

			lblTC.Text = "Current: " + C + " | Bug Fixes: " + B + " | Total: " + N;

		}

		private void cmdKey_Click(object sender, EventArgs e) {
			SaveFileDialog SFD = new SaveFileDialog();
			SFD.Filter = "Win Toolkit Key|*.reg";
			SFD.Title = "Save Win Toolkit Key";
			SFD.FileName = "WinToolkit_Key";

			if (SFD.ShowDialog() != System.Windows.Forms.DialogResult.OK) { return; }
			try {
				using (StreamWriter SW = new StreamWriter(SFD.FileName)) {
					SW.WriteLine("Windows Registry Editor Version 5.00");
					SW.WriteLine("");
					SW.WriteLine(";DO NOT DISTRIBUTE THIS KEY, IT HAS BEEN ASSIGNED TO YOUR NAME/EMAIL ADDRESS. THANKS.\n");
					SW.WriteLine("");
					SW.WriteLine("[HKEY_LOCAL_MACHINE\\Software\\WinToolkit]");
					string sKey = cReg.GetValue(Microsoft.Win32.Registry.LocalMachine, "Software\\WinToolkit", "Key");
					SW.WriteLine("\"Key\"=\"" + sKey + "\"");
				}
			}
			catch (Exception Ex) {
				LargeError LE = new LargeError("Exporting Key", "Unable to export key.", SFD.FileName, Ex);
				LE.Upload(); LE.ShowDialog();
			}
		}

		private void txtCL_TextChanged(object sender, EventArgs e) {

		}
	}
}
