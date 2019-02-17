using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinToolkit.Prompts
{
    public partial class frmWinKey : Form
    {
        public frmWinKey()
        {
            InitializeComponent();
        }

        private void frmWinKey_Load(object sender, EventArgs e)
        {
            cMain.FormIcon(this);
            string key = cMain.GetWindowsKey();

            if (string.IsNullOrEmpty(key))
            {
                lblKey.Text = "Error getting key.";
                cmdClipboard.Enabled = false;
                return;
            }
            lblKey.Text = key;
        }

        private void cmdClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblKey.Text);
        }
    }
}
