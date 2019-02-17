using System;
using System.Windows.Forms;

namespace WinToolkit.Prompts {
	public partial class frmWelcome : Form {
		public frmWelcome() {
			InitializeComponent();
		}

		private void frmWelcome_Load(object sender, EventArgs e) {
			cMain.FormIcon(this);
			this.Text = "Welcome";
			cOptions.ShowWelcome = false;
			cOptions.SaveSettings();
		}

		private void cmdGuides_Click(object sender, EventArgs e) {
			cMain.OpenLink("http://www.wincert.net/forum/index.php?showforum=192");
		}

		private void cmdDownload_Click(object sender, EventArgs e) {
			new frmDownload_ISO().ShowDialog();
		}

	}
}
