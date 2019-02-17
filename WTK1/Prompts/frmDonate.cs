using System;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.Helpers;

namespace WinToolkit
{
    public partial class frmDonate : Form
    {
        public frmDonate(string formTitle)
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            Text = formTitle;
        }

        private void cmdLego_Click(object sender, EventArgs e)
        {
            string currencyCode = "USD";

            if (rbEUR.Checked) { currencyCode = "EUR"; }
            if (rbGBP.Checked) { currencyCode = "GBP"; }
            cMain.OpenLink("http://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=liamc70@gmail.com&on0=Created By&os0=Legolash2o&item_name=WinToolkit Donation&quantity=1&no_shipping=1&tax=0&currency_code=" + currencyCode + "&return=http://wincert.net/leli55PK/Thanks9753124680.htm&cancel_return=http://www.wincert.net/forum/index.php?/forum/179-win-toolkit/", false);

        }

        private void frmDonate_Load(object sender, EventArgs e)
        {
            splitContainer1.Scale4K(_4KHelper.Panel.Pan2);
            cMain.FormIcon(this);
            cboName.SelectedIndex = 0;
        }



        private void cboName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sBank = "";

            switch (cboName.Text)
            {
                case "Wincert":
                    txtDescription.Text =
                "Donate to WinCert, which hosts Win Toolkit. They need all the help they can get. Win Toolkit would not be the same without it!";
                    sBank = "To: Nikica Matejic (Croatia)" + Environment.NewLine + Environment.NewLine + "Bank: Hypo Alpe-Adria-Bank d.d."
                    + Environment.NewLine + "Slavonska Avenija 6" + Environment.NewLine + "Zagreb, Croatia"
                    + Environment.NewLine + Environment.NewLine + "IBAN: HR62 2500 0093 2102 1394 4" + Environment.NewLine + "BIC: HAABHR22";
                    break;
                case "Kelsenellenelvian":
                    txtDescription.Text =
                "Help support a good friend and the create of Windows Post-Install Wizard (WPI)! More importantly, he has helped more people than I can count for 5+ years on many forums!";
                    break;
                case "Alphawaves":
                    txtDescription.Text =
                "Alpahwaves is the creator of WHDownloader. Help support his work.";
                    break;
                default:
                    txtDescription.Text = "";
                    break;
            }

            if (string.IsNullOrEmpty(sBank) && tabControl3.TabPages.Contains(tabFDonate))
            {
                tabControl3.TabPages.Remove(tabFDonate);
            }

            if (!string.IsNullOrEmpty(sBank))
            {
                if (!tabControl3.TabPages.Contains(tabFDonate))
                {
                    tabControl3.TabPages.Add(tabFDonate);
                }
                txtFriendBank.Text = sBank;
            }
        }

        private void cmdFriendDonate_Click(object sender, EventArgs e)
        {
            string currencyCode = "USD";
            string email = "donations@wincert.net";

            if (rbFEUR.Checked) { currencyCode = "EUR"; }
            if (rbFGBP.Checked) { currencyCode = "GBP"; }

            if (cboName.Text.EqualsIgnoreCase("Kelsenellenelvian")) { email = "kelsenellenelvian@gmail.com"; }
            if (cboName.Text.EqualsIgnoreCase("Alphawaves")) { email = "alphawaves90@gmail.com"; }

            cMain.OpenLink("http://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=" + email + "&on0=Created By&os0=Legolash2o&item_name=WinToolkit Donation&quantity=1&no_shipping=1&tax=0&currency_code=" + currencyCode + "&return=http://www.wincert.net/forum/forum/179-win-toolkit/&cancel_return=http://www.wincert.net/forum/forum/179-win-toolkit/", false);

        }

    }
}
