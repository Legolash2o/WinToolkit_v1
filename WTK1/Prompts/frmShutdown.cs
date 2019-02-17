using System;
using System.Globalization;
using System.Windows.Forms;
using WinToolkit.Classes;

namespace WinToolkit.Prompts {
	public partial class frmShutdown : Form {
		public frmShutdown() {
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}
		int Tim = 60;
		public string Task = "Shutdown";
		private void frmShutdown_Load(object sender, EventArgs e) {
            
			Text = Task + " Warning!";
			lblDesc.Text = "Win Toolkit is about to " + Task.ToLower() + " your computer in less than 1 minute.";
			cMain.TweakChoice = "";
			cMain.FormIcon(this);
			cMain.ToolStripIcons(ToolStrip5);
		}

		private void timShutdown_Tick(object sender, EventArgs e) {
			Tim -= 1;
			lblTime.Text = Tim.ToString(CultureInfo.InvariantCulture);
			if (Tim == 0) { timShutdown.Enabled = false; Shutdown(Task); }
		}

		private void cmdAbort_Click(object sender, EventArgs e) {
			timShutdown.Enabled = false;
			Close();
		}

		private void Shutdown(string sTask) {
			cOptions.SaveSettings();
            if (sTask.EqualsIgnoreCase("Shutdown")) { cMain.OpenProgram("\"" + cMain.SysFolder + "\\shutdown.exe\"", "-s -t 00", false, System.Diagnostics.ProcessWindowStyle.Hidden); }
			if (sTask.EqualsIgnoreCase("Restart")) { cMain.OpenProgram("\"" + cMain.SysFolder + "\\shutdown.exe\"", "-r -t 00", false, System.Diagnostics.ProcessWindowStyle.Hidden); }
			Application.DoEvents();
			Environment.Exit(0);
		}

		private void cmdShutdown_Click(object sender, EventArgs e) {
			Shutdown("Shutdown");
		}

		private void cmdRestart_Click(object sender, EventArgs e) {
			Shutdown("Restart");
		}

	}
}