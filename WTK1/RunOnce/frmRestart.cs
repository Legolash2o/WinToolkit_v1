using System;
using System.Drawing;
using System.Windows.Forms;

namespace RunOnce
{
    public partial class frmRestart : Form
    {

        int _time = 10;

        public frmRestart(string title, string message, Color color, bool showCancel = false, int time = 10)
        {

            InitializeComponent();
            this.BackColor = color;
            if (!showCancel)
            {
                splitContainer3.Panel2Collapsed = true;
                lblTitle.Top = ((lblTitle.Parent.Height - lblTitle.Height) / 2) - (lblTitle.Height / 2);
                lblMessage.Top = ((lblMessage.Parent.Height - lblMessage.Height) / 2) + (lblMessage.Height / 2);
            }
            cmdAbort.Visible = showCancel;
            _time = time;
            lblTitle.Text = title;
            lblMessage.Text = message;

            CenterWidth(lblMessage);
            CenterWidth(lblTitle);
            CenterWidth(cmdAbort);
        }

        private void CenterWidth(Control control)
        {
            control.Left = (control.Parent.Width - control.Width) / 2;
        }


        private void frmExit_Load(object sender, EventArgs e)
        {

        }

        private void timeShutdown_Tick(object sender, EventArgs e)
        {
            _time--;
            lblTime.Text = _time.ToString("0#");
            pbLoad.Value--;
            if (_time <= 0)
            {
                timeShutdown.Enabled = false;
                Close();
            }
        }

        private void cmdAbort_Click(object sender, EventArgs e)
        {
            cFunctions.WriteScript();
            Environment.Exit(0);
        }
    }
}
