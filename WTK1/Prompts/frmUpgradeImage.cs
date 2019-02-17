using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinToolkit.Classes.Helpers;

namespace WinToolkit
{
    public partial class frmUpgradeImage : Form
    {
        public frmUpgradeImage()
        {
            InitializeComponent();
        }

        public bool bSelected = false;
        public List<string> sImageName = new List<string>();

        public string sArc = "";
        public string sName = "";

        private void frmUpgradeImage_Load(object sender, EventArgs e)
        {
            splitContainer1.Scale4K(_4KHelper.Panel.Pan2);
            cMain.FormIcon(this);
            cMain.ToolStripIcons(ToolStrip1);
            Height += 1;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboUpgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtImageName.Text = sName + " " + cboUpgrade.Text + " " + sArc;
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            bSelected = true;
            this.Close();
        }
    }
}
