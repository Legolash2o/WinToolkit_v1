using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WinToolkit.Classes;

namespace WinToolkit {
	public partial class frmErrorBox : Form {
		Bitmap bmpScreenshot;

		public frmErrorBox() {
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			MouseWheel += MouseScroll;
		}

		private void MouseScroll(object sender, MouseEventArgs e) {
			txtEx.Select();
			txtEx.SelectionLength = 0;
		}

		private void frmError_Load(object sender, EventArgs e) {
			cmdTS.Enabled = false;
			foreach (Form F in Application.OpenForms) {
				if (F.Name != "frmStartup" && F.Name != "frmError" && F.Name != "frmAntiVirus" && F.Name != "frmD_ISO" && F.Name != "frmErrorReport" && F.Name != "frmShutdown" && F.Name != "frmErrorReport" && F.Name != "frmTweaks" && F.Name != "frmUnmount" && F.IsDisposed == false) {
					try {
						if (F.WindowState == FormWindowState.Minimized) { F.WindowState = FormWindowState.Normal; }
						bmpScreenshot = new Bitmap(F.Width, F.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
						F.DrawToBitmap(bmpScreenshot, new Rectangle(Point.Empty, bmpScreenshot.Size));

						cmdTS.Enabled = true;
						break;
					}
					catch (Exception Ex) { F.TopMost = false; cmdTS.Enabled = false; }
				}
			}

			if (bmpScreenshot == null) { cmdTS.Enabled = false; }
			Height = cmdMI.Top + cmdMI.Height + 41;
			cMain.FormIcon(this);
			cmdMI.Enabled = false;
			if (!string.IsNullOrEmpty(txtEx.Text)) {
				cmdMI.Enabled = true;
			}
			Visible = true;
			cMain.FreeRAM();

		}

		private void mnuSTF_Click(object sender, EventArgs e) {
			try {
				var SFD = new SaveFileDialog();
				SFD.Title = "Save Error Log";
				SFD.Filter = "Error *.txt|*.txt";

				if (SFD.ShowDialog() != DialogResult.OK) { return; }
				using (var SW = new StreamWriter(SFD.FileName, false, System.Text.Encoding.Unicode)) {
					SW.Write("Title: " + lblTitle.Text + Environment.NewLine + "ErrType: " + Text + Environment.NewLine + "Description: " + lblDesc.Text + Environment.NewLine + "Exception: " + Environment.NewLine + txtEx.Text);
				}
			}
			catch (Exception Ex) {
				MessageBox.Show(Ex.Message, "Error saving log");
			}
		}

		private void mnuSCB_Click(object sender, EventArgs e) {
			try {
				Clipboard.Clear();
				Clipboard.SetText("Title: " + lblTitle.Text + Environment.NewLine + "ErrType: " + Text + Environment.NewLine + "Description: " + lblDesc.Text + Environment.NewLine + "Exception: " + Environment.NewLine + txtEx.Text);
				MessageBox.Show("Information has been copied to the clipboard successfully.", "Done");
			}
			catch (Exception Ex) {
				MessageBox.Show(Ex.Message, "Error copying to clipboard.");
			}
		}

		private void cmdOK_Click(object sender, EventArgs e) {
			Close();
		}

		private void cmdMI_Click(object sender, EventArgs e) {
            if (cmdMI.Text.EqualsIgnoreCase(">> Details"))
            {
				cmdMI.Text = "<< Details";
				Height = txtEx.Top + txtEx.Height + 41;
				txtEx.Visible = true;
			}
			else {
				cmdMI.Text = ">> Details";
				Height = cmdMI.Top + cmdMI.Height + 41;
				txtEx.Visible = false;
			}
			cMain.CenterObject(this);
			cMain.FreeRAM();
		}

		private void cmdTS_Click(object sender, EventArgs e) {
			var SFD = new SaveFileDialog();
			SFD.Filter = "Screenshot *.png|*.png";
			SFD.InitialDirectory = cMain.Desktop;
			SFD.Title = "Save Screenshot";

			if (SFD.ShowDialog() != DialogResult.OK) { return; }
			cmdTS.Enabled = false;

			Text += " [Saving Image :: " + SFD.FileName + "]";
			Application.DoEvents();
			try {
				bmpScreenshot.Save(SFD.FileName, System.Drawing.Imaging.ImageFormat.Png);
			}
			catch { }
			Text = Text.ReplaceIgnoreCase(" [Saving Image :: " + SFD.FileName + "]", "");
			cmdTS.Enabled = true;
			cMain.FreeRAM();
		}

	}
}
