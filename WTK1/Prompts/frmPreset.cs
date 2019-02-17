using System;
using System.Windows.Forms;
using WinToolkit.Classes;

namespace WinToolkit.Prompts {
	public partial class frmAIOPresetName : Form {
		public frmAIOPresetName() {
			InitializeComponent();
		}

		internal int dResult = 0;

		private void cmdAccept_Click(object sender, EventArgs e) {
			if (string.IsNullOrEmpty(txtPresetName.Text)) {
				MessageBox.Show("You haven't entered a value preset name!", "Invalid");
				return;
			}

			dResult = 1;
			Close();
		}

		private void cmdContinue_Click(object sender, EventArgs e) {
			dResult = 0;
			Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e) {
			dResult = -1;
			Close();
		}

		private void frmPresetPrompt_Load(object sender, EventArgs e) {
			cMain.FormIcon(this);
			txtPresetName.Focus();
			lblChar.Text = (txtPresetName.MaxLength - txtPresetName.Text.Length) + " Characters Remaining";
		}

		private void frmPresetPrompt_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter) { cmdAccept.PerformClick(); }
			if (e.KeyCode == Keys.Escape) { cmdContinue.PerformClick(); }
		}

		private void txtPresetName_TextChanged(object sender, EventArgs e) {
			if (txtPresetName.Text.ContainsIgnoreCase("|")) { txtPresetName.Text = txtPresetName.Text.ReplaceIgnoreCase("|", ""); }
			lblChar.Text = (txtPresetName.MaxLength - txtPresetName.Text.Length) + " Characters Remaining";
		}
	}
}
