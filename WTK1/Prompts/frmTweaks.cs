using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Properties;

namespace WinToolkit.Prompts {
	public partial class frmTweaks : Form {
		public frmTweaks() {
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		private void frmTweaks_Load(object sender, EventArgs e) {
            
			cMain.TweakChoice = "";
			cMain.FormIcon(this);
			cMain.ToolStripIcons(ToolStrip5);
			cmdRBrowse.Image = Resources.folder_48;
			Text = Text.ReplaceIgnoreCase( " (" + cMain.WinToolkitVersion() + ")", "");
		}

		private void cmdROK_Click(object sender, EventArgs e) {
			if (string.IsNullOrEmpty(txtRInfo.Text) && string.IsNullOrEmpty(cboRChoice.Text)) {
				cmdRCancel.PerformClick();
				return;
			}
			if (!string.IsNullOrEmpty(txtRInfo.Text)) {
                if (Text.EqualsIgnoreCase("Change Setup Background") && !txtRInfo.Text.ToUpper().EndsWithIgnoreCase(".BMP"))
                {
					try {
						string E = txtRInfo.Text;
						while (!E.EndsWithIgnoreCase("."))
							E = E.Substring(0, E.Length - 1);

						Files.DeleteFile(E + "bmp");
						using (Image img = Image.FromFile(txtRInfo.Text)) {
							img.Save(E + "bmp", ImageFormat.Bmp);
						}

						txtRInfo.Text = E + "bmp";
					}
					catch (Exception Ex) {
						LargeError LE = new LargeError("Image Conversion Error", "Unable to convert image.", Ex);
						LE.Upload(); LE.ShowDialog();
					}
				}
			}

			if (txtRInfo.Visible) {
				cMain.TweakChoice = txtRInfo.Text;
			}
			if (cboRChoice.Visible) {
				cMain.TweakChoice = cboRChoice.Text;
			}
		DialogResult = DialogResult.OK; 
		}

		private void cmdRCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel; 
		}

		private void cmdRBrowse_Click(object sender, EventArgs e) {
			cTweaks.SelectF(Text, txtRInfo);
		}
	}
}