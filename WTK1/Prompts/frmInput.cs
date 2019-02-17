using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WinToolkit.Classes;
using WinToolkit.Classes.Helpers;

namespace WinToolkit.Prompts {
    public partial class frmInput : Form {

       
        public string Restrictions = "";
        public int MaxNumber = 0;
        public frmInput(string Title, string Message) {
            InitializeComponent();
            Text = Title;
            lblMessage.Text = Message;
 }
        private void frmInput_Shown(object sender, EventArgs e) {
            txtInput.Focus();
            txtInput.SelectAll();
        }

        private void frmInput_Load(object sender, EventArgs e) {
            splitContainer1.Scale4K(_4KHelper.Panel.Pan2);
            splitContainer2.Scale4K(_4KHelper.Panel.Pan2);

            if (Text.EqualsIgnoreCase("Computer Name")) {
                Restrictions = "` £€~!@#$%^&*()=+_[]{}\\|;:.'\",<>/?";
            }

            if (Text.EqualsIgnoreCase("Profile Directory") || Text.EqualsIgnoreCase("ProgramData Folder")) {
                Restrictions = "\",<>/?*|";
                lblMessage.Text += "\n\nExample\nX:\\FolderPath\\";
            }

            if (Text.EqualsIgnoreCase("Logo")) {
                Restrictions = "\",<>/?*|";
                lblMessage.Text += "\n\nExample\n%SystemDrive%\\Windows\\LogoFolder\\Logo.png";
            }

            if (Text.EqualsIgnoreCase("Logon Count"))
            {
                MaxNumber = 9999999;
            }

            if (MaxNumber > 0) {
                txtInput.MaxLength = MaxNumber.ToString().Length;
                lblRestrictions.Text = "Max: " + MaxNumber;
            }
            else if (string.IsNullOrEmpty(Restrictions)) {
                lblRestrictions.Visible = false;
                toolStripSeparator1.Visible = false;
            }

            else {
                lblRestrictions.Text = "Restrictions: " + Restrictions;
            }

            Height += 1;

        }

        private void cmdAccept_Click(object sender, EventArgs e) {

            if (string.IsNullOrEmpty(txtInput.Text)) {
                if (MaxNumber > 0)
                {
                    MessageBox.Show("You need to enter a number!", "No Number");
                }
                else
                {
                    MessageBox.Show("You need to enter some text!", "No Text"); 
                }
               
                return;
            }

            if (MaxNumber > 0 && int.Parse(txtInput.Text) > MaxNumber)
            {
                MessageBox.Show("The number you have entered is too high!", "Too high");
                return;
            }

            if (Text.EqualsIgnoreCase("Profile Directory") || Text.EqualsIgnoreCase("ProgramData Folder")) {

                Regex r = new Regex(@"^(([a-zA-Z]\:)|(\\))(\\{1}|((\\{1})[^\\]([^/:*?<>""|]*))+)$");

                if (txtInput.Text.Length < 3 || !txtInput.Text.ContainsIgnoreCase(":") || !r.IsMatch(txtInput.Text)) {
                    MessageBox.Show("This does not seem to be a valid path, some good examples would be:\n\nD:\\Users\nE:\\Profiles\\", "Error");
                    return;
                }
                if (txtInput.Text.Length == 3 && Text.EqualsIgnoreCase("Profile Directory"))
                {
                    txtInput.Text += "Users";
                }

                if (txtInput.Text.Length == 3 && Text.EqualsIgnoreCase("ProgramData Folder")) {
                    txtInput.Text += "ProgramData";
                }
            }

            txtInput.Text = txtInput.Text.ReplaceIgnoreCase("\"", "");

            if (!string.IsNullOrEmpty(txtInput.Text)) {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else { DialogResult = System.Windows.Forms.DialogResult.Cancel; }
        }

        private void cmdCancel_Click(object sender, EventArgs e) {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)13) {
                cmdAccept.PerformClick();
            }
            if (e.KeyChar == (char)27) {
                cmdCancel.PerformClick();
            }

            if (MaxNumber > 0) {
                if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+") && e.KeyChar != (char)8 && e.KeyChar != (char)127) {
                    txtInput.BackColor = Color.LightPink;
                    timer1.Enabled = true;
                    e.Handled = true;
                }
            }
        }

        private void txtInput_TextChanged(object sender, EventArgs e) {
            bool changed = false;
            if (!string.IsNullOrEmpty(Restrictions)) {

                foreach (char check in Restrictions.ToCharArray()) {
                    if (txtInput.Text.ContainsIgnoreCase(check.ToString().ToUpper())) {
                        txtInput.Text = txtInput.Text.ReplaceIgnoreCase(check.ToString(), "");
                        changed = true;
                    }

                }
            }

            if (txtInput.Text.Length == 1 && txtInput.Text.EqualsIgnoreCase(" "))
            {
                txtInput.Text = "";
                changed = true;
            }
            if (changed) {
                txtInput.BackColor = Color.LightPink;
                timer1.Enabled = true;
                txtInput.Select(txtInput.Text.Length, 0);
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            txtInput.BackColor = Color.White;
            timer1.Enabled = false;
        }
    }
}
