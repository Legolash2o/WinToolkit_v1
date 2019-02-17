using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinToolkit.Classes.Helpers;

namespace WinToolkit.Prompts
{
    public partial class frmUpdateSilents : Form
    {

        public List<ListViewItem> Keep = new List<ListViewItem>();
        public List<ListViewItem> Move = new List<ListViewItem>();
        private List<ListViewItem> _badupdates = new List<ListViewItem>();
        public frmUpdateSilents(List<ListViewItem> updates)
        {
            _badupdates = updates;
            InitializeComponent();
            cMain.FormIcon(this);

            foreach (ListViewItem upd in _badupdates)
            {
                upd.Group = null;
                ListViewItem lvi = (ListViewItem)upd.Clone();
                lvi.Checked = true;

                if (cOptions.RememberSelectedUpdates.Contains(upd.SubItems[5].Text, StringComparer.InvariantCultureIgnoreCase))
                    lvi.Checked = false;


                lstItems.Items.Add(lvi);
            }
        }

        private void frmUpdateSilents_Load(object sender, EventArgs e)
        {
            splitContainer1.Scale4K(_4KHelper.Panel.Pan1);
            splitContainer2.Scale4K(_4KHelper.Panel.Pan2);
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lstItems.Items)
            {

                ListViewItem upd = _badupdates.FirstOrDefault(l => l.Text == lvi.Text);
                if (lvi.Checked)
                {
                    Move.Add(upd);
                }
                else
                {
                    Keep.Add(upd);
                    cOptions.AddUpdateToRemember(lvi.SubItems[5].Text);
                }
            }
            _badupdates.Clear();
            Close();
        }
    }
}
