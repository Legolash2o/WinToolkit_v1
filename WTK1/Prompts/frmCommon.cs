using System;
using System.Drawing;
using System.Windows.Forms;
using WinToolkit.Classes.Helpers;

namespace WinToolkit.Prompts {
	public partial class frmCommon : Form {
		public frmCommon() {
			InitializeComponent();
			cMain.FormIcon(this);
		}

		public string SelectedSyntax = "";
		public string SelectedName = "";
		private void frmCommon_Load(object sender, EventArgs e) {
            splitContainer1.Scale4K(_4KHelper.Panel.Pan2);
			cMain.AutoSizeColums(lstSwitches);
            
            foreach (ListViewItem LST in lstSwitches.Items) {
                if (LST.Index % 2 == 0) {
                    LST.BackColor = Color.AliceBlue;
                }
            }
		    Height -= 1;
		}

		private void lstSwitches_DoubleClick(object sender, MouseEventArgs e) {
			if (lstSwitches.SelectedItems.Count > 0) {
				cmdAccept.PerformClick();
			}
		}

		private void cmsCopyClipboard_Click(object sender, EventArgs e) {
			if (lstSwitches.SelectedItems.Count > 0) {
				Clipboard.Clear();
				Clipboard.SetText(lstSwitches.SelectedItems[0].SubItems[1].Text);
			}
		}

		private void cmdCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
		}

		private void cmdAccept_Click(object sender, EventArgs e) {
			if (lstSwitches.SelectedItems.Count == 0) {
				MessageBox.Show("You need to select an item first!", "Invalid item");
				return;
			}
			SelectedSyntax = lstSwitches.SelectedItems[0].SubItems[1].Text;
			SelectedName = lstSwitches.SelectedItems[0].Text;
			DialogResult = DialogResult.OK;

		}


	}
}
