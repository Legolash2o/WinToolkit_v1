using System;
using System.Linq;
using System.Windows.Forms;
using WinToolkit.Classes;

namespace WinToolkit
{
    public partial class frmWTUpdate : Form
    {
        public frmWTUpdate()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            cMain.FormIcon(this);
            FormClosed += frmUpdate_FormClosed;
            Activated += cMain.FormActivated;
            Deactivate += cMain.FormActivated;
            Resize += frmUpdate_Resize;
            Shown += frmUpdate_Shown;
            MouseWheel += MouseScroll;
            BWLoad.RunWorkerCompleted += BWLoad_RunWorkerCompleted;
        }
        bool loading = true;

        private void frmUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            cOptions.SaveSettings();
        }

        private void frmUpdate_Resize(object sender, EventArgs e)
        {
            chkUpdates.Left = cmdExit.Left + (cmdExit.Width + 20);
        }

        private void MouseScroll(object sender, MouseEventArgs e)
        {
            lstNew.Select();
        }

        private void frmUpdate_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            BWLoad.RunWorkerAsync();

            if (!string.IsNullOrEmpty(cOptions.NM)) { label2.Text = cOptions.NM; }
            chkUpdates.Checked = cOptions.CheckForUpdates;
            if (!BWLoad.IsBusy) { loading = false; }
        }

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            //125
            splitContainer1.SplitterDistance = label2.Top + label2.Height + 10;
            splitContainer2.SplitterDistance = cmdYes.Top + cmdYes.Height + 10;

            Opacity = 1;
            Text = "Win Toolkit v" + cOptions.NV.Substring(0, cOptions.NV.LastIndexOf('.'));
            label4.Text = "New:    " + cOptions.NV + "\nCurrent: " + cMain.WinToolkitVersion(true);
            chkUpdates.Left = cmdExit.Left + (cmdExit.Width + 20);

        }

        private void chkUpdates_CheckedChanged(object sender, EventArgs e)
        {
            if (loading == false)
            {
                cOptions.CheckForUpdates = chkUpdates.Checked;
                cOptions.SaveSettings();
            }
        }

        private void cmdYes_Click(object sender, EventArgs e)
        {
            Text = "Opening Link...";
            Hide();
            Application.DoEvents();
            cMain.OpenLink(string.IsNullOrEmpty(cOptions.NL) ? "http://wintoolkit.co.uk/Home/Downloads" : cOptions.NL);

            CloseWT();
        }

        private void cmdNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            CloseWT();
        }

        private void CloseWT()
        {
            try
            {
                Text = "Saving Settings..."; Application.DoEvents();
                cOptions.SaveSettings();
                Text = "Cleaning..."; Application.DoEvents();
                cMain.CleanExit(this);
                Text = "Closing Win Toolkit..."; Application.DoEvents();
                Environment.Exit(0);
            }
            catch (Exception Ex)
            {
                LargeError LE = new LargeError("Win Toolkit Exit", "Error closing Win Tooolkit", Text, Ex);
                LE.Upload(); LE.ShowDialog();
                Close();
            }
        }

        private void BWLoad_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            lstNew.BeginUpdate();
            foreach (string S in cOptions.NF.Split(Environment.NewLine.ToCharArray()))
            {
                if (S.StartsWithIgnoreCase("*"))
                {
                    string Group = "v" + S.Substring(1);
                    while (Group.ContainsIgnoreCase("^"))
                        Group = Group.Substring(0, Group.Length - 1);

                    if (!Group.StartsWithIgnoreCase("v1.4.1"))
                    {
                        Group = Group.Substring(0, Group.LastIndexOf('.'));
                    }

                    var NI = new ListViewItem();
                    NI.ImageIndex = 0;
                    NI.Text = S;

                    while (NI.Text.ContainsIgnoreCase("^"))
                        NI.Text = NI.Text.Substring(1);

                    foreach (ListViewGroup G in lstNew.Groups)
                    {
                        if (G.Header == Group)
                        {
                            NI.Group = G;
                            break;
                        }
                    }

                    if (NI.Group == null)
                    {
                        ListViewGroup newGroup = new ListViewGroup();
                        newGroup.Name = "gbU" + lstNew.Groups.Count;
                        newGroup.Header = Group;
                        lstNew.Groups.Add(newGroup);
                        NI.Group = newGroup;
                    }

                    if (NI.Text.StartsWithIgnoreCase("FIX:"))
                    {
                        NI.ImageIndex = 1;
                        NI.Text = NI.Text.Trim();
                    }
                    if (NI.Text.StartsWithIgnoreCase("NEW:"))
                    {
                        NI.ImageIndex = 2;
                        NI.Text = NI.Text.Trim();
                    }

                    lstNew.Items.Add(NI);
                }
            }

            try
            {
                int iNew = lstNew.Groups[0].Items.Cast<ListViewItem>().Where(u => u.ImageIndex == 2).Count();
                int iFixes = lstNew.Groups[0].Items.Cast<ListViewItem>().Where(u => u.ImageIndex == 1).Count();
                int iMisc = lstNew.Groups[0].Items.Cast<ListViewItem>().Where(u => u.ImageIndex == 0).Count();
                columnHeader2.Text = iNew + " New | " + iFixes + " Fixes | " + iMisc + " |  Miscellaneous";
            }
            catch (Exception Ex)
            {
            }


            lstNew.EndUpdate();
        }

        private void BWLoad_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            loading = false;
        }
    }
}