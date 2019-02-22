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

	private void txtCL_TextChanged(object sender, EventArgs e) {

		}
	}
}
