using System;
using System.Windows.Forms;

namespace WinToolkit.Prompts {
	public partial class frmAntiVirus : Form {
		public static bool TempChange = false;
		int Timer = 15;
		public frmAntiVirus() {
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			FormClosing += frmAntiVirus_FormClosing;
			KeyDown += frmAntiVirus_KeyDown;
			cMain.FormIcon(this);
		}

		private void frmAntiVirus_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Escape) { Close(); }
		}

		private void frmAntiVirus_Load(object sender, EventArgs e) {
			chkAV.Checked = cOptions.AVScan;
			if (cMain.AVShown && cMain.DetectAntivirus() == false) {
				chkAV.Visible = false;
				lblAV.Text = "Win Toolkit has detected that you have turned off your anti-virus. This is just a notice to remind you to turn it back on!";
			}
			Text = "Notice [" + Timer.ToString() + "]";

			if (TempChange) {
				GBTF.Visible = true;
				lblTemp.Text += Environment.NewLine + Environment.NewLine + "[" + cOptions.WinToolkitTemp + "]" + Environment.NewLine + Environment.NewLine + "This can be changed via 'Options'.";
			}
			GBSSD.Visible = true;

			int S = GBAV.Top + GBAV.Height + 30;
			foreach (Control C in flowLayoutPanel1.Controls) {
				if (C.Visible) { S = C.Top + C.Height; }
			}
			Height = S + 30;
			CenterToScreen();
			timClose.Enabled = true;
		}

		private void frmAntiVirus_FormClosing(object sender, FormClosingEventArgs e) {
			TempChange = false;
		}

		private void chkAV_CheckedChanged(object sender, EventArgs e) {
			cOptions.AVScan = chkAV.Checked;
			cOptions.SaveSettings();
		}

		private void timClose_Tick(object sender, EventArgs e) {
			Timer -= 1;
			Text = "Notice [" + Timer.ToString() + "]";
			if (Timer == 0) { this.Close(); }
		}
	}
}
