using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinToolkit.Classes.Helpers;

namespace WinToolkit.Prompts
{
    public partial class frmIntegrate : Form
    {
        private ListViewItem _LST = null;
        private string _URL = null;

        public frmIntegrate(ListViewItem LST)
        {
            InitializeComponent();
            cMain.ToolStripIcons(ToolStrip1);
            cMain.FormIcon(this);
            label1.Text = LST.Text;
            _LST = LST;
            if (!string.IsNullOrEmpty(LST.SubItems[7].Text))
            {
                _URL = LST.SubItems[7].Text;
            }
            else
            {
                cmdSupport.Visible = false;
            }

           

        }

        private void AddSubItem(string name, string property)
        {
            ListViewItem LST = new ListViewItem(name);
            LST.SubItems.Add(property);
            lstStatus.Items.Add(LST);

        }

        private void frmIntegrate_Load(object sender, EventArgs e)
        {
            splitContainer2.Scale4K(_4KHelper.Panel.Pan1);
            splitContainer1.Scale4K(_4KHelper.Panel.Pan2);
            WinToolkit.frmAllInOne.UpdateInfo updateInfo = (WinToolkit.frmAllInOne.UpdateInfo)_LST.Tag;
            AddSubItem("Name", _LST.Text);
            AddSubItem("Description", _LST.SubItems[1].Text);
            AddSubItem("Language", _LST.SubItems[2].Text);
            AddSubItem("Size", _LST.SubItems[3].Text);
            AddSubItem("Architecture", _LST.SubItems[4].Text);
            lblLocation.Text = _LST.SubItems[5].Text;
            AddSubItem("MD5", _LST.SubItems[6].Text);
            AddSubItem("Support", _LST.SubItems[7].Text);
            AddSubItem("Date", _LST.SubItems[8].Text);

            switch (updateInfo.UpdateType)
            {
                case Classes.UpdateCache.UpdateType.LDR:
                    AddSubItem("Update Type", "LDR");
                    tabPage1.ImageIndex = 14;
                    break;
                case Classes.UpdateCache.UpdateType.GDR:
                    AddSubItem("Update Type", "GDR");
                    tabPage1.ImageIndex = 13;
                    break;
                default:
                    AddSubItem("Update Type", "Unknown");
                    tabPage1.ImageIndex = 12;
                    break;
            }

            if (updateInfo.PackageName != null)
            {
                AddSubItem("Package Name", updateInfo.PackageName);
            }
            if (updateInfo.PackageVersion != null)
            {
                AddSubItem("Package Version", updateInfo.PackageVersion);
            }
            foreach (var info in updateInfo.Info.OrderBy(i => i.ImageIndex).ThenBy(i => i.Date))
            {
                TabPage TP = new TabPage();
                TP.ImageIndex = info.StatusIndex;
                TP.Text = string.Format("[{0}] {1}", info.ImageIndex, info.ImageName);


                SplitContainer SC = new SplitContainer();

                TP.Controls.Add(SC);
                SC.Orientation = Orientation.Horizontal;
                SC.IsSplitterFixed = true;
                SC.SplitterDistance = 50;
                SC.FixedPanel = FixedPanel.Panel1;
                SC.SplitterWidth = 1;
                SC.Dock = DockStyle.Fill;

                Label L = new Label();
                L.BackColor = Color.White;
                L.TextAlign = ContentAlignment.MiddleCenter;
                L.AutoSize = false;
                L.Dock = DockStyle.Fill;
                L.Text = info.Date + "\r\n";
                if (info.StatusIndex == 2)
                {
                    L.Text += "File not found.";
                }
                else if (info.StatusIndex == 6)
                {
                    L.Text += "Update integrated successfully.";
                    L.BackColor = Color.LightGreen;
                }
                else if (info.StatusIndex == 11)
                {
                    L.Text += "Update was already integrated.";
                    L.BackColor = Color.LightGreen;
                }
                else if (string.IsNullOrEmpty(info.Note))
                {
                    L.Text += "No additional information available.";
                }
                else
                {
                    L.Text += info.Note;
                }

                SC.Panel1.Controls.Add(L);

                TextBox T = new TextBox();
                SC.Panel2.Controls.Add(T);
                T.Multiline = true;

                T.Dock = DockStyle.Fill;
                T.ReadOnly = true;
                if (string.IsNullOrEmpty(info.DISM))
                {
                    T.Text = "No DISM information available.";
                }
                else
                {
                    T.Text = info.DISM;
                }
                T.ScrollBars = ScrollBars.Vertical;

                tabControl1.TabPages.Add(TP);
            }
        }

        private void cmdSupport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_URL))
            {
                cMain.OpenLink(_URL);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
