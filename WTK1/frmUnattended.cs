using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.Helpers;
using WinToolkit.Prompts;

namespace WinToolkit
{
    public partial class frmUnattendedCreator : Form
    {
        private readonly ListView LVTZ = new ListView();
        private readonly OpenFileDialog OFD = new OpenFileDialog();

        private string ProductKey = "";
        private TabPage TP;
        private bool iAdd;

        public frmUnattendedCreator()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            CheckForIllegalCrossThreadCalls = false;
            FormClosing += frmUnattended_FormClosing;
            FormClosed += frmUnattended_FormClosed;
            Activated += cMain.FormActivated;
            Deactivate += cMain.FormActivated;
            MouseWheel += MouseScroll;
        }



        private void MouseScroll(object sender, MouseEventArgs e)
        {
            switch (tabControl1.SelectedTab.Text)
            {
                case "Main":
                    lstUnattended.Select();
                    break;
                case "Users":
                    lstUsers.Select();
                    break;
                case "Editor":
                    txtUnattended.Select();
                    txtUnattended.SelectionLength = 0;
                    break;
            }
        }

        private void frmUnattended_Load(object sender, EventArgs e)
        {
            cMain.FormIcon(this); cMain.eForm = this;
            cMain.eLBL = lblStatus;
            cMain.ToolStripIcons(ToolStrip1);
            cMain.ToolStripIcons(ToolStrip5);
            SCUsers.Scale4K(_4KHelper.Panel.Pan1);
            splitContainer2.Scale4K(_4KHelper.Panel.Pan2);


            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Adding time zone...");
                LVTZ.Items.Add("(-12:00) International Date Line West");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Dateline Standard Time");
                LVTZ.Items.Add("(-11:00) Coordinated Universal Time-11");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("UTC-11");
                LVTZ.Items.Add("(-11:00) Samoa");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Samoa Standard Time");
                LVTZ.Items.Add("(-10:00) Hawaii");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Hawaiian Standard Time");
                LVTZ.Items.Add("(-09:00) Alaska");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Alaskan Standard Time");
                LVTZ.Items.Add("(-08:00) Baja California");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Pacific Standard Time (Mexico)");
                LVTZ.Items.Add("(-08:00) Pacific Time (US & Canada)");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Pacific Standard Time");
                LVTZ.Items.Add("(-07:00) Arizona");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("US Mountain Standard Time");
                LVTZ.Items.Add("(-07:00) Chihuahua, La Paz, Mazatlan");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Mountain Standard Time (Mexico)");
                LVTZ.Items.Add("(-07:00) Mountain Time (US & Canada)");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Mountain Standard Time");
                LVTZ.Items.Add("(-06:00) Central America");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Central America Standard Time");
                LVTZ.Items.Add("(-06:00) Central Time (US & Canada)");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Central Standard Time");
                LVTZ.Items.Add("(-06:00) Guadalajara, Mexico City, Monterrey");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Central Standard Time (Mexico)");
                LVTZ.Items.Add("(-06:00) Saskatchewan");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Canada Central Standard Time");
                LVTZ.Items.Add("(-05:00) Bogota, Lima, Quito");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("SA Pacific Standard Time");
                LVTZ.Items.Add("(-05:00) Eastern Time (US & Canada)");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Eastern Standard Time");
                LVTZ.Items.Add("(-05:00) Indiana (East)");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("US Eastern Standard Time");
                LVTZ.Items.Add("(-04:30) Caracas");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Venezuela Standard Time");
                LVTZ.Items.Add("(-04:00) Asuncion");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Paraguay Standard Time");
                LVTZ.Items.Add("(-04:00) Atlantic Time (Canada)");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Atlantic Standard Time");
                LVTZ.Items.Add("(-04:00) Cuiaba");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Central Brazilian Standard Time");
                LVTZ.Items.Add("(-04:00) Georgetown, La Paz, Manaus, San Juan");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("SA Western Standard Time");
                LVTZ.Items.Add("(-04:00) Santiago");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Pacific SA Standard Time");
                LVTZ.Items.Add("(-03:30) Newfoundland");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Newfoundland Standard Time");
                LVTZ.Items.Add("(-03:00) Brasilia");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("E. South America Standard Time");
                LVTZ.Items.Add("(-03:00) Buenos Aires");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Argentina Standard Time");
                LVTZ.Items.Add("(-03:00) Cayenne, Fortaleza");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("SA Eastern Standard Time");
                LVTZ.Items.Add("(-03:00) Greenland");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Greenland Standard Time");
                LVTZ.Items.Add("(-03:00) Montevideo");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Montevideo Standard Time");
                LVTZ.Items.Add("(-02:00) Coordinated Universal Time-02");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("UTC-02");
                LVTZ.Items.Add("(-02:00) Mid-Atlantic");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Mid-Atlantic Standard Time");
                LVTZ.Items.Add("(-01:00) Azores");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Azores Standard Time");
                LVTZ.Items.Add("(-01:00) Cape Verde Is.");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Cape Verde Standard Time");
                LVTZ.Items.Add("(00:00) Casablanca");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Morocco Standard Time");
                LVTZ.Items.Add("(00:00) Coordinated Universal Time");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Coordinated Universal Time");
                LVTZ.Items.Add("(00:00) Dublin, Edinburgh, Lisbon, London");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("GMT Standard Time");
                LVTZ.Items.Add("(00:00) Monrovia, Reykjavik");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Greenwich Standard Time");
                LVTZ.Items.Add("(+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("W. Europe Standard Time");
                LVTZ.Items.Add("(+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Central Europe Standard Time");
                LVTZ.Items.Add("(+01:00) Brussels, Copenhagen, Madrid, Paris");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Romance Standard Time");
                LVTZ.Items.Add("(+01:00) Sarajevo, Skopje, Warsaw, Zagreb");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Central European Standard Time");
                LVTZ.Items.Add("(+01:00) West Central Africa");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("W. Central Africa Standard Time");
                LVTZ.Items.Add("(+01:00) Windhoek");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Namibia Standard Time");
                LVTZ.Items.Add("(+02:00) Amman");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Jordan Standard Time");
                LVTZ.Items.Add("(+02:00) Athens, Bucharest");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("GTB Standard Time");
                LVTZ.Items.Add("(+02:00) Beirut");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Middle East Standard Time");
                LVTZ.Items.Add("(+02:00) Cairo");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Egypt Standard Time");
                LVTZ.Items.Add("(+02:00) Damascus");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Syria Standard Time");
                LVTZ.Items.Add("(+02:00) Harare, Pretoria");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("South Africa Standard Time");
                LVTZ.Items.Add("(+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("FLE Standard Time");
                LVTZ.Items.Add("(+02:00) Istanbul");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Turkey Standard Time");
                LVTZ.Items.Add("(+02:00) Jerusalem");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Israel Standard Time");
                LVTZ.Items.Add("(+02:00) Minsk");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("E. Europe Standard Time");
                LVTZ.Items.Add("(+03:00) Baghdad");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Arabic Standard Time");
                LVTZ.Items.Add("(+03:00) Kuwait, Riyadh");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Arab Standard Time");
                LVTZ.Items.Add("(+03:00) Moscow, St. Petersburg, Volgograd");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Russian Standard Time");
                LVTZ.Items.Add("(+03:00) Nairobi");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("E. Africa Standard Time");
                LVTZ.Items.Add("(+03:30) Tehran");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Iran Standard Time");
                LVTZ.Items.Add("(+04:00) Abu Dhabi, Muscat");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Arabian Standard Time");
                LVTZ.Items.Add("(+04:00) Baku");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Azerbaijan Standard Time");
                LVTZ.Items.Add("(+04:00) Port Louis");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Mauritius Standard Time");
                LVTZ.Items.Add("(+04:00) Tbilisi");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Georgian Standard Time");
                LVTZ.Items.Add("(+04:00) Yerevan");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Caucasus Standard Time");
                LVTZ.Items.Add("(+04:30) Kabul");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Afghanistan Standard Time");
                LVTZ.Items.Add("(+05:00) Ekaterinburg");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Ekaterinburg Standard Time");
                LVTZ.Items.Add("(+05:00) Islamabad, Karachi");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Pakistan Standard Time");
                LVTZ.Items.Add("(+05:00) Tashkent");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("West Asia Standard Time");
                LVTZ.Items.Add("(+05:30) Chennai, Kolkata, Mumbai, New Delhi");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("India Standard Time");
                LVTZ.Items.Add("(+05:30) Sri Jayawardenepura");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Sri Lanka Standard Time");
                LVTZ.Items.Add("(+05:45) Kathmandu");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Nepal Standard Time");
                LVTZ.Items.Add("(+06:00) Astana");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Central Asia Standard Time");
                LVTZ.Items.Add("(+06:00) Dhaka");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Bangladesh Standard Time");
                LVTZ.Items.Add("(+06:00) Novosibirsk");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("N. Central Asia Standard Time");
                LVTZ.Items.Add("(+06:30) Yangon (Rangoon)");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Myanmar Standard Time");
                LVTZ.Items.Add("(+07:00) Bangkok, Hanoi, Jakarta");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("SE Asia Standard Time");
                LVTZ.Items.Add("(+07:00) Krasnoyarsk");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("North Asia Standard Time");
                LVTZ.Items.Add("(+08:00) Beijing, Chongqing, Hong Kong, Urumqi");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("China Standard Time");
                LVTZ.Items.Add("(+08:00) Irkutsk");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("North Asia East Standard Time");
                LVTZ.Items.Add("(+08:00) Kuala Lumpur, Singapore");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Singapore Standard Time");
                LVTZ.Items.Add("(+08:00) Perth");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("W. Australia Standard Time");
                LVTZ.Items.Add("(+08:00) Taipei");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Taipei Standard Time");
                LVTZ.Items.Add("(+08:00) Ulaanbaatar");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Ulaanbaatar Standard Time");
                LVTZ.Items.Add("(+09:00) Osaka, Sapporo, Tokyo");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Tokyo Standard Time");
                LVTZ.Items.Add("(+09:00) Seoul");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Korea Standard Time");
                LVTZ.Items.Add("(+09:00) Yakutsk");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Yakutsk Standard Time");
                LVTZ.Items.Add("(+09:30) Adelaide");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Cen. Australia Standard Time");
                LVTZ.Items.Add("(+09:30) Darwin");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("AUS Central Standard Time");
                LVTZ.Items.Add("(+10:00) Brisbane");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("E. Australia Standard Time");
                LVTZ.Items.Add("(+10:00) Canberra, Melbourne, Sydney");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("AUS Eastern Standard Time");
                LVTZ.Items.Add("(+10:00) Guam, Port Moresby");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("West Pacific Standard Time");
                LVTZ.Items.Add("(+10:00) Hobart");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Tasmania Standard Time");
                LVTZ.Items.Add("(+10:00) Vladivostok");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Vladivostok Standard Time");
                LVTZ.Items.Add("(+11:00) Magadan");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Magadan Standard Time");
                LVTZ.Items.Add("(+11:00) Solomon Is., New Caledonia");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Central Pacific Standard Time");
                LVTZ.Items.Add("(+12:00) Auckland, Wellington");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("New Zealand Standard Time");
                LVTZ.Items.Add("(+12:00) Coordinated Universal Time+12");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("UTC+12");
                LVTZ.Items.Add("(+12:00) Fiji");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Fiji Standard Time");
                LVTZ.Items.Add("(+13:00) Nuku'alofa");
                LVTZ.Items[LVTZ.Items.Count - 1].SubItems.Add("Tonga Standard Time");
            }
            catch (Exception Ex)
            {
            }

            cMain.AutoSizeColums(lstUnattended);
            cMain.AutoSizeColums(lstUsers);
            cMain.UpdateToolStripLabel(lblStatus, "");
        }

        private void frmUnattended_FormClosing(object sender, FormClosingEventArgs e)
        {
            cMain.OnFormClosing(this);
        }

        private void frmUnattended_FormClosed(object sender, FormClosedEventArgs e)
        {
            cMain.ReturnME();
        }

        private void lstUnattended_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                if (e.Item.Checked && iAdd == false)
                {
                    cMain.CenterObject(PanRInfo);
                    cboRChoice.Items.Clear();

                    if (e.Item.Text.EqualsIgnoreCase("Serial"))
                    {

                        using (var Key = new frmKeys())
                        {
                            Key.Width = this.Width - 100;
                            Key.Height = this.Height - 100;
                            if (!string.IsNullOrEmpty(e.Item.SubItems[1].Text))
                            {
                                Key.txtKey.Text = e.Item.SubItems[1].Text.ToUpper();
                            }

                            if (Key.ShowDialog() == DialogResult.OK)
                            {
                                e.Item.SubItems[1].Text = Key.txtKey.Text.ToUpper();
                            }
                            else
                            {
                                e.Item.Checked = false;
                            }

                            if (iAdd == false) { UpdateText(); }
                            return;
                        }

                    }

                    if (e.Item.Tag.ToString().EqualsIgnoreCase("Text") || e.Item.Tag.ToString().EqualsIgnoreCase("Number"))
                    {
                        string response = cMain.InputBox("Please enter " + e.Item.Text.ToLower(), e.Item.Text, e.Item.SubItems[1].Text);


                        if (string.IsNullOrEmpty(response))
                        {
                            e.Item.Checked = false;
                        }
                        else
                        {
                            e.Item.SubItems[1].Text = response;
                        }


                    }

                    if (e.Item.Text.EqualsIgnoreCase("Username"))
                    {
                        cboRChoice.Items.Add("Administrator");
                        foreach (ListViewItem LST in lstUsers.Items)
                        {
                            if (!cboRChoice.Items.Contains(LST.Text))
                            {
                                cboRChoice.Items.Add(LST.Text);
                            }
                        }
                    }

                    if (e.Item.Text.EqualsIgnoreCase("Network Location"))
                    {
                        cboRChoice.Items.Add("Home");
                        cboRChoice.Items.Add("Work");
                        cboRChoice.Items.Add("Other");
                    }

                    if (e.Item.Text.EqualsIgnoreCase("Colour Depth"))
                    {
                        cboRChoice.Items.Add("32bit");
                        cboRChoice.Items.Add("16bit");
                    }

                    if (e.Item.Text.EqualsIgnoreCase("Screen Resolution"))
                    {
                        string R = SystemInformation.PrimaryMonitorSize.Width.ToString(CultureInfo.InvariantCulture) + "x" +
                                      SystemInformation.PrimaryMonitorSize.Height.ToString(CultureInfo.InvariantCulture);

                        cboRChoice.Items.Add("800x600");
                        cboRChoice.Items.Add("1024x768");
                        cboRChoice.Items.Add("1152x864");
                        cboRChoice.Items.Add("1240x768");
                        cboRChoice.Items.Add("1280x720");
                        cboRChoice.Items.Add("1280x1024");
                        cboRChoice.Items.Add("1366x768");
                        cboRChoice.Items.Add("1440x900");
                        cboRChoice.Items.Add("1600x1200");
                        try
                        {
                            cboRChoice.Items.Remove(R);
                        }
                        catch
                        {
                        }

                        cboRChoice.Items.Insert(0, R);
                    }

                    if (e.Item.Text.EqualsIgnoreCase("Refresh Rate"))
                    {
                        cboRChoice.Items.Add("60Hz");
                        cboRChoice.Items.Add("75Hz");
                        cboRChoice.Items.Add("80Hz");
                        cboRChoice.Items.Add("90Hz");
                        cboRChoice.Items.Add("100Hz");
                        cboRChoice.Items.Add("110Hz");
                        cboRChoice.Items.Add("120Hz");
                    }

                    if (e.Item.Text.EqualsIgnoreCase("Time Zone"))
                    {
                        foreach (ListViewItem LST in LVTZ.Items)
                        {
                            cboRChoice.Items.Add(LST.Text);
                        }
                    }

                    if (e.Item.Text.EqualsIgnoreCase("Updates"))
                    {
                        cboRChoice.Items.Add("(1) Recommended");
                        cboRChoice.Items.Add("(2) Download, but don't install");
                        cboRChoice.Items.Add("(3) Don't download or install");
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(e.Item.Tag)))
                    {
                        if (e.Item.Tag != "Text" && e.Item.Tag != "Number")
                        {
                            TP = tabControl1.SelectedTab;

                            PanRInfo.Visible = true;
                            lstUnattended.Enabled = false;
                            GBRInfo.Text = e.Item.Text;
                            txtRInfo.Visible = false;
                            cmdRBrowse.Visible = false;
                            cboRChoice.Visible = false;
                            mnuCreate.Enabled = false;

                            if (e.Item.Tag.ToString().EqualsIgnoreCase("Folder") | e.Item.Tag.ToString().EqualsIgnoreCase("File"))
                            {
                                txtRInfo.Visible = true;
                                cmdRBrowse.Visible = true;
                                cmdRBrowse.Tag = e.Item.Tag;
                            }

                            if (e.Item.Tag.ToString().EqualsIgnoreCase("List"))
                            {
                                try
                                {
                                    cboRChoice.SelectedIndex = e.Item.Text.EqualsIgnoreCase("Time Zone") ? 35 : 0;
                                }
                                catch
                                {
                                }
                                cboRChoice.Visible = true;
                            }

                            while (PanRInfo.Visible)
                            {
                                Thread.Sleep(100);
                                Application.DoEvents();
                            }
                        }
                    }
                }
                if (iAdd == false)
                {
                    UpdateText();
                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, e.Item.Text, Ex.Message, iAdd + Environment.NewLine + lblStatus.Text);
                cMain.ErrorBox(Ex.Message, "Unexpected error: " + e.Item.Text);
            }
        }

        private void cmdRBrowse_Click(object sender, EventArgs e)
        {
            if (cmdRBrowse.Tag.ToString().EqualsIgnoreCase("Folder"))
            {
                string FBD = cMain.FolderBrowserVista(GBRInfo.Text, false, true);
                if (string.IsNullOrEmpty(FBD))
                {
                    return;
                }
                txtRInfo.Text = FBD;
            }

            if (cmdRBrowse.Tag.ToString().EqualsIgnoreCase("File"))
            {
                if (OFD.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                txtRInfo.Text = OFD.FileName;
            }
        }

        private void mnuCreate_Click(object sender, EventArgs e)
        {
            if (lstUnattended.CheckedItems.Count == 0 && lstUsers.Items.Count == 0)
            {
                MessageBox.Show("You need to select at least one item!", "Invalid Selection");
                return;
            }

            if (lstUnattended.FindItemWithText("Username").Checked == false &&
                 lstUnattended.FindItemWithText("Logon Count").Checked)
            {
                MessageBox.Show("You can't have 'Logon Count' checked if you haven't selected a 'Username'!!",
                                     "Invalid Selection");
                return;
            }

            if (lstUnattended.FindItemWithText("Skip Auto-Activation").Checked && lstUnattended.FindItemWithText("Serial").Checked == false)
            {
                MessageBox.Show(
                     "You can't have 'Skip Auto-Activation' checked if you haven't checked the 'Serial' option. If required, use a default key from the 'Serial Keys' tab.",
                     "Invalid Selection");
                return;
            }

            if (lstUnattended.FindItemWithText("Username").Checked && lstUsers.Items.Count == 0 && !lstUnattended.FindItemWithText("Admin Password").Checked)
            {
                MessageBox.Show("You need to set an Administrator password. This will also make your computer more secure!", "Invalid 'Admin Password'");
                return;
            }

            if (lstUnattended.FindItemWithText("Username").Checked && lstUsers.Items.Count == 0)
            {
                var LST = new ListViewItem();
                LST.Text = "Administrator";
                LST.SubItems.Add("Administrator");

                LST.SubItems.Add(lstUnattended.FindItemWithText("Admin Password").Checked
                                            ? lstUnattended.FindItemWithText("Admin Password").SubItems[1].Text
                                            : "");
                LST.SubItems.Add("Administrators");
                LST.SubItems.Add("Administrator Account");
                lstUsers.Items.Add(LST);
            }

            var SFD = new SaveFileDialog();
            SFD.Title = "Save Unattended File";
            SFD.Filter = "Unattended *.xml|*.xml";
            SFD.FileName = "Autounattend.xml";

            if (SFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            try
            {
                var SW = new StreamWriter(SFD.FileName, false);
                SW.Write(txtUnattended.Text);
                SW.Close();
                MessageBox.Show(
                     "'" + SFD.FileName + "' has been saved." + Environment.NewLine + Environment.NewLine +
                     "You need to place this on the DVD/USB root in order for it to work.", "Success");
            }
            catch (Exception EX)
            {
                cMain.ErrorBox("Win Toolkit could not save the unattended file to the selected location.", "Unable to save unattended", EX.Message);
            }
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            ToolStrip1.Enabled = false;
            cMain.UpdateToolStripLabel(lblStatus, "Importing Unattended...");
            Application.DoEvents();
            try
            {
                var ofd = new OpenFileDialog();
                ofd.Title = "Autounattend.xml File";
                ofd.Filter = "Autotunattended.xml *.xml|*.xml";
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    ToolStrip1.Enabled = true;
                    cMain.UpdateToolStripLabel(lblStatus, "");
                    return;
                }
                iAdd = true;

                try
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Cleaning List...");
                    lstUsers.Items.Clear();
                    foreach (ListViewItem LST in lstUnattended.Items)
                    {
                        LST.Checked = false;
                        if (LST.SubItems.Count == 2)
                        {
                            LST.SubItems[1].Text = "";
                        }
                    }
                    lstUnattended.FindItemWithText("Serial").SubItems[1].Text = ProductKey;
                }
                catch { }

                var SR = new StreamReader(ofd.FileName);
                string S = SR.ReadToEnd();
                SR.Close();

                try
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Importing Users...");
                    foreach (string sLine in Regex.Split(S, "<LocalAccount", RegexOptions.IgnoreCase))
                    {
                        if (sLine.ContainsIgnoreCase("wcm:action"))
                        {
                            string AName = "", DName = "", NGroup = "", NDesc = "", NPass = "";

                            foreach (string sLine2 in sLine.Split(Environment.NewLine.ToCharArray()))
                            {
                                try
                                {
                                    if (sLine2.ContainsIgnoreCase("<Name>") && string.IsNullOrEmpty(AName))
                                    {
                                        AName = GetValue(sLine2);
                                    }
                                    if (sLine2.ContainsIgnoreCase("<DisplayName>") && string.IsNullOrEmpty(DName))
                                    {
                                        DName = GetValue(sLine2);
                                    }
                                    if (sLine2.ContainsIgnoreCase("<Value>") && string.IsNullOrEmpty(NPass))
                                    {
                                        NPass = GetValue(sLine2);
                                    }
                                    if (sLine2.ContainsIgnoreCase("<Group>") && string.IsNullOrEmpty(NGroup))
                                    {
                                        NGroup = GetValue(sLine2);
                                    }
                                    if (sLine2.ContainsIgnoreCase("<Description>") && string.IsNullOrEmpty(NDesc))
                                    {
                                        NDesc = GetValue(sLine2);
                                    }
                                }
                                catch
                                {
                                }
                            }
                            if (!string.IsNullOrEmpty(AName) && lstUsers.FindItemWithText(AName) == null)
                            {
                                var NI = new ListViewItem();

                                NI.Text = AName;
                                NI.SubItems.Add(DName);
                                NI.SubItems.Add(NPass);
                                NI.SubItems.Add(NGroup);
                                NI.SubItems.Add(NDesc);
                                lstUsers.Items.Add(NI);
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    cMain.WriteLog(this, "Error importing users", Ex.Message, null);
                    cMain.ErrorBox("Win Toolkit could not import the users from the selected unattended file.", "Error importing users.", Ex.Message);
                }

                int T = 0;

                cMain.UpdateToolStripLabel(lblStatus, "Importing General...");
                foreach (string sLine in S.Split(Environment.NewLine.ToCharArray()))
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(sLine))
                        {
                            if (sLine.ContainsIgnoreCase("<RegisteredOwner>"))
                            {
                                lstUnattended.FindItemWithText("Your Full Name").Checked = true;
                                lstUnattended.FindItemWithText("Your Full Name").SubItems[1].Text = GetValue(sLine);
                            }
                            if (sLine.ContainsIgnoreCase("<AcceptEula>"))
                            {
                                lstUnattended.FindItemWithText("Accept EULA").Checked = true;
                            }
                            if (sLine.ContainsIgnoreCase("<ComputerName>"))
                            {
                                lstUnattended.FindItemWithText("Computer Name").Checked = true;
                                lstUnattended.FindItemWithText("Computer Name").SubItems[1].Text = GetValue(sLine);
                            }
                            if (sLine.ContainsIgnoreCase("<RegisteredOrganization>"))
                            {
                                lstUnattended.FindItemWithText("Company Name").Checked = true;
                                lstUnattended.FindItemWithText("Company Name").SubItems[1].Text = GetValue(sLine);
                            }
                            if (sLine.ContainsIgnoreCase("<ProductKey>") || sLine.ContainsIgnoreCase("<Key>"))
                            {
                                lstUnattended.FindItemWithText("Serial").Checked = true;
                                lstUnattended.FindItemWithText("Serial").SubItems[1].Text = GetValue(sLine);
                            }
                            if (sLine.ContainsIgnoreCase("<ColorDepth>"))
                            {
                                lstUnattended.FindItemWithText("Colour Depth").Checked = true;
                                lstUnattended.FindItemWithText("Colour Depth").SubItems[1].Text = GetValue(sLine) + "bit";
                            }
                            if (sLine.ContainsIgnoreCase("<RefreshRate>"))
                            {
                                lstUnattended.FindItemWithText("Refresh Rate").Checked = true;
                                lstUnattended.FindItemWithText("Refresh Rate").SubItems[1].Text = GetValue(sLine) + "Hz";
                            }
                            if (sLine.ContainsIgnoreCase("<HorizontalResolution>"))
                            {
                                lstUnattended.FindItemWithText("Screen Resolution").Checked = true;
                                lstUnattended.FindItemWithText("Screen Resolution").SubItems[1].Text = GetValue(sLine) +
                                                                                                                              "x";
                            }
                            if (sLine.ContainsIgnoreCase("<VerticalResolution>"))
                            {
                                lstUnattended.FindItemWithText("Screen Resolution").Checked = true;
                                lstUnattended.FindItemWithText("Screen Resolution").SubItems[1].Text += GetValue(sLine);
                            }
                            if (sLine.ContainsIgnoreCase("<NetworkLocation>"))
                            {
                                lstUnattended.FindItemWithText("Network Location").Checked = true;
                                lstUnattended.FindItemWithText("Network Location").SubItems[1].Text = GetValue(sLine);
                            }
                            if (sLine.ContainsIgnoreCase("<ProtectYourPC>"))
                            {
                                lstUnattended.FindItemWithText("Updates").Checked = true;
                                if (GetValue(sLine).EqualsIgnoreCase("1"))
                                {
                                    lstUnattended.FindItemWithText("Updates").SubItems[1].Text = "(1) Recommended";
                                }
                                if (GetValue(sLine).EqualsIgnoreCase("2"))
                                {
                                    lstUnattended.FindItemWithText("Updates").SubItems[1].Text =
                                         "(2) Download, but don't install";
                                }
                                if (GetValue(sLine).EqualsIgnoreCase("3"))
                                {
                                    lstUnattended.FindItemWithText("Updates").SubItems[1].Text =
                                         "(3) Don't download or install";
                                }
                            }
                            if (sLine.ContainsIgnoreCase("<HideWirelessSetupInOOBE>"))
                            {
                                lstUnattended.FindItemWithText("Hide Wireless Setup").Checked = true;
                            }
                            if (sLine.ContainsIgnoreCase("<TimeZone>"))
                            {
                                lstUnattended.FindItemWithText("Time Zone").Checked = true;
                                lstUnattended.FindItemWithText("Time Zone").SubItems[1].Text = GetValue(sLine);
                            }

                            if (sLine.ContainsIgnoreCase("<Manufacturer>"))
                            {
                                lstUnattended.FindItemWithText("Manufacturer").Checked = true;
                                lstUnattended.FindItemWithText("Manufacturer").SubItems[1].Text = GetValue(sLine);
                            }
                            if (sLine.ContainsIgnoreCase("<SupportHours>"))
                            {
                                lstUnattended.FindItemWithText("Support Hours").Checked = true;
                                lstUnattended.FindItemWithText("Support Hours").SubItems[1].Text = GetValue(sLine);
                            }
                            if (sLine.ContainsIgnoreCase("<SupportPhone>"))
                            {
                                lstUnattended.FindItemWithText("Support Phone").Checked = true;
                                lstUnattended.FindItemWithText("Support Phone").SubItems[1].Text = GetValue(sLine);
                            }
                            if (sLine.ContainsIgnoreCase("<SupportURL>"))
                            {
                                lstUnattended.FindItemWithText("Support URL").Checked = true;
                                lstUnattended.FindItemWithText("Support URL").SubItems[1].Text = GetValue(sLine);
                            }
                            if (sLine.ContainsIgnoreCase("<Model>"))
                            {
                                lstUnattended.FindItemWithText("Model").Checked = true;
                                lstUnattended.FindItemWithText("Model").SubItems[1].Text = GetValue(sLine);
                            }
                            if (sLine.ContainsIgnoreCase("<Logo>"))
                            {
                                lstUnattended.FindItemWithText("Logo").Checked = true;
                                lstUnattended.FindItemWithText("Logo").SubItems[1].Text = GetValue(sLine);
                            }

                            if (sLine.ContainsIgnoreCase("<AdministratorPassword>"))
                            {
                                T = 1;
                            }
                            if (sLine.ContainsIgnoreCase("<Value>") && T == 1)
                            {
                                lstUnattended.FindItemWithText("Admin Password").Checked = true;
                                lstUnattended.FindItemWithText("Admin Password").SubItems[1].Text = GetValue(sLine);
                                T = 0;
                            }

                            if (sLine.ContainsIgnoreCase("<Username>"))
                            {
                                lstUnattended.FindItemWithText("Username").Checked = true;
                                lstUnattended.FindItemWithText("Username").SubItems[1].Text = GetValue(sLine);
                            }
                            if (sLine.ContainsIgnoreCase("<LogonCount>"))
                            {
                                lstUnattended.FindItemWithText("Logon Count").Checked = true;
                                lstUnattended.FindItemWithText("Logon Count").SubItems[1].Text = GetValue(sLine);
                            }
                            if (sLine.ContainsIgnoreCase("<SkipAutoActivation>"))
                            {
                                lstUnattended.FindItemWithText("Skip Auto-Activation").Checked = true;
                            }

                            if (sLine.ContainsIgnoreCase("<ProfilesDirectory>"))
                            {
                                lstUnattended.FindItemWithText("Profile Directory").Checked = true;
                                lstUnattended.FindItemWithText("Profile Directory").SubItems[1].Text = GetValue(sLine);
                            }
                            if (sLine.ContainsIgnoreCase("<ProgramData>"))
                            {
                                lstUnattended.FindItemWithText("ProgramData Folder").Checked = true;
                                lstUnattended.FindItemWithText("ProgramData Folder").SubItems[1].Text = GetValue(sLine);
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        cMain.ErrorBox("Win Toolkit could not load all of the unattended file. Please report this error.", "Could not load unattended.", Ex.Message);
                        cMain.WriteLog(this, "Error loading unattended", Ex.Message, sLine);
                    }
                }
            }
            catch
            {
            }
            iAdd = false;
            UpdateText();
            cMain.UpdateToolStripLabel(lblStatus, "");
            ToolStrip1.Enabled = true;
        }

        private string GetValue(string Value)
        {
            string S = Value;
            try
            {
                while (S.ContainsIgnoreCase("</"))
                {
                    S = S.Substring(0, S.Length - 1);
                }

                while (S.ContainsIgnoreCase(">"))
                {
                    S = S.Substring(1);
                }

                while (S.ContainsIgnoreCase("<"))
                {
                    S = S.Substring(0, S.Length - 1);
                }
            }
            catch
            {
            }
            return S;
        }

        private void TabControl1_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (TP != null)
            {
                tabControl1.SelectedTab = TP;
            }
        }

        private void cmdUNew_Click(object sender, EventArgs e)
        {
            toolStrip2.Enabled = false;
            ToolStrip1.Enabled = false;
            lstUsers.Enabled = false;
            txtUUsername.Text = "";
            txtUDescription.Text = "";
            txtUDisplay.Text = "";
            txtUPassword.Text = "";
            cboUAccount.SelectedIndex = 0;
            cMain.CenterObject(panUser);
            panUser.Visible = true;
            TP = tabControl1.SelectedTab;
        }

        private void cmdUCancel_Click(object sender, EventArgs e)
        {
            TP = null;
            toolStrip2.Enabled = true;
            ToolStrip1.Enabled = true;
            lstUsers.Enabled = true;
            panUser.Visible = false;
        }

        private void cmdUOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUUsername.Text))
            {
                MessageBox.Show("You need to enter a username!", "Invalid Username");
                txtUUsername.Focus();
                return;
            }

            if (lstUsers.FindItemWithText(txtUUsername.Text) != null)
            {
                MessageBox.Show("This user already exists!", "Invalid Username");
                txtUUsername.Focus();
                return;
            }

            var NI = new ListViewItem();
            NI.Text = txtUUsername.Text;
            NI.SubItems.Add(txtUDisplay.Text);
            NI.SubItems.Add(txtUPassword.Text);
            NI.SubItems.Add(cboUAccount.Text);
            NI.SubItems.Add(txtUDescription.Text);
            lstUsers.Items.Add(NI);

            foreach (ColumnHeader CH in lstUsers.Columns)
            {
                CH.Width = -2;
            }

            TP = null;
            toolStrip2.Enabled = true;
            ToolStrip1.Enabled = true;
            lstUsers.Enabled = true;
            panUser.Visible = false;
            UpdateText();
        }

        private void cmdUDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LST in lstUsers.SelectedItems)
            {
                LST.Remove();
            }
            UpdateText();
        }

        private void lstUnattended_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmdROK_Click(object sender, EventArgs e)
        {
            string T;
            if (cboRChoice.Visible)
            {
                T = cboRChoice.Text;
                if (GBRInfo.Text.EqualsIgnoreCase("Time Zone"))
                {
                    T = LVTZ.FindItemWithText(cboRChoice.Text).SubItems[1].Text;
                }
            }
            else
            {
                T = txtRInfo.Text;
            }
            try
            {
                lstUnattended.FindItemWithText(GBRInfo.Text).SubItems[1].Text = T;
            }
            catch
            {
                lstUnattended.FindItemWithText(GBRInfo.Text).SubItems.Add(T);
            }
            TP = null;
            PanRInfo.Visible = false;
            lstUnattended.Enabled = true;
            mnuCreate.Enabled = true;
        }

        private void cmdRCancel_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lstUnattended.CheckedItems)
            {
                if (item.Text == GBRInfo.Text)
                    item.Checked = false;
            }
            TP = null;
            PanRInfo.Visible = false;
            lstUnattended.Enabled = true;
            mnuCreate.Enabled = true;
        }

        private string Generate_WindowsPE(string Arc)
        {
            string S = "";
            S += "        <component name=\"Microsoft-Windows-Setup\" processorArchitecture=\"" + Arc +
                  "\" publicKeyToken=\"31bf3856ad364e35\" language=\"neutral\" versionScope=\"nonSxS\" xmlns:wcm=\"http://schemas.microsoft.com/WMIConfig/2002/State\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">" +
                  Environment.NewLine;
            S += "            <UserData>" + Environment.NewLine;
            S += "                <AcceptEula>" + lstUnattended.FindItemWithText("Accept EULA").Checked.ToString(CultureInfo.InvariantCulture).ToLower() + "</AcceptEula>" + Environment.NewLine;
            if (lstUnattended.FindItemWithText("Serial").Checked)
            {
                S += "                <ProductKey>" + Environment.NewLine;
                S += "                    <Key>" + lstUnattended.FindItemWithText("Serial").SubItems[1].Text + "</Key>" + Environment.NewLine;
                S += "                <WillShowUI>Always</WillShowUI>" + Environment.NewLine;
                S += "                </ProductKey>" + Environment.NewLine;
            }
            S += "            </UserData>" + Environment.NewLine;
            S += "        </component>" + Environment.NewLine;
            return S;
        }

        private string Generate_ShellSetup(string Arc)
        {
            string S = "";
            S += "        <component name=\"Microsoft-Windows-Shell-Setup\" processorArchitecture=\"" + Arc + "\" publicKeyToken=\"31bf3856ad364e35\" language=\"neutral\" versionScope=\"nonSxS\" xmlns:wcm=\"http://schemas.microsoft.com/WMIConfig/2002/State\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">" +
                  Environment.NewLine;

            //if (lstUnattended.FindItemWithText("Serial").Checked) { S += "            <ProductKey>" + lstUnattended.FindItemWithText("Serial").SubItems[1].Text + "</ProductKey>" + Environment.NewLine; }
            if (lstUnattended.FindItemWithText("Serial").Checked)
            {
                S += "            <ProductKey>" + lstUnattended.FindItemWithText("Serial").SubItems[1].Text + "</ProductKey>" + Environment.NewLine;
            }
            if (lstUnattended.FindItemWithText("Computer Name").Checked)
            {
                S += "            <ComputerName>" + lstUnattended.FindItemWithText("Computer Name").SubItems[1].Text +
                      "</ComputerName>" + Environment.NewLine;
            }
            S += "        </component>" + Environment.NewLine;
            return S;
        }

        private string Generate_SecuritySPPUX(string Arc)
        {
            string S = "";
            S += "        <component name=\"Microsoft-Windows-Security-SPP-UX\" processorArchitecture=\"" + Arc +
                  "\" publicKeyToken=\"31bf3856ad364e35\" language=\"neutral\" versionScope=\"nonSxS\" xmlns:wcm=\"http://schemas.microsoft.com/WMIConfig/2002/State\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">" +
                  Environment.NewLine;
            if (lstUnattended.FindItemWithText("Skip Auto-Activation").Checked)
            {
                S += "            <SkipAutoActivation>true</SkipAutoActivation>" + Environment.NewLine;
            }
            S += "        </component>" + Environment.NewLine;
            return S;
        }

        private string Generate_OOBE(string Arc)
        {
            string S = "";
            int A = 0;
            try
            {
                S += "        <component name=\"Microsoft-Windows-Shell-Setup\" processorArchitecture=\"" + Arc +
                      "\" publicKeyToken=\"31bf3856ad364e35\" language=\"neutral\" versionScope=\"nonSxS\" xmlns:wcm=\"http://schemas.microsoft.com/WMIConfig/2002/State\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">" +
                      Environment.NewLine;
                if (lstUnattended.FindItemWithText("Colour Depth").Checked ||
                     lstUnattended.FindItemWithText("Refresh Rate").Checked ||
                     lstUnattended.FindItemWithText("Screen Resolution").Checked)
                {
                    S += "            <Display>" + Environment.NewLine;
                    if (lstUnattended.FindItemWithText("Colour Depth").Checked)
                    {
                        A = 1;
                        S += "                <ColorDepth>" +
                              lstUnattended.FindItemWithText("Colour Depth").SubItems[1].Text.Substring(0, 2) +
                              "</ColorDepth>" + Environment.NewLine;
                    }
                    if (lstUnattended.FindItemWithText("Screen Resolution").Checked)
                    {
                        A = 2;
                        string HR = lstUnattended.FindItemWithText("Screen Resolution").SubItems[1].Text;
                        string VR = lstUnattended.FindItemWithText("Screen Resolution").SubItems[1].Text;

                        while (HR.ContainsIgnoreCase("x"))
                        {
                            HR = HR.Substring(0, HR.Length - 1);
                        }
                        A = 3;
                        while (VR.ContainsIgnoreCase("x"))
                        {
                            VR = VR.Substring(1);
                        }
                        S += "                <HorizontalResolution>" + HR + "</HorizontalResolution>" + Environment.NewLine;
                        S += "                <VerticalResolution>" + VR + "</VerticalResolution>" + Environment.NewLine;
                    }
                    if (lstUnattended.FindItemWithText("Refresh Rate").Checked)
                    {
                        A = 4;
                        S += "                <RefreshRate>" +
                             lstUnattended.FindItemWithText("Refresh Rate").SubItems[1].Text.ReplaceIgnoreCase("Hz", "") +
                              "</RefreshRate>" + Environment.NewLine;
                    }

                    S += "            </Display>" + Environment.NewLine;
                }
                if (lstUnattended.FindItemWithText("Manufacturer").Checked ||
                     lstUnattended.FindItemWithText("Support Phone").Checked ||
                     lstUnattended.FindItemWithText("Support Hours").Checked ||
                     lstUnattended.FindItemWithText("Support URL").Checked || lstUnattended.FindItemWithText("Model").Checked ||
                     lstUnattended.FindItemWithText("Logo").Checked)
                {
                    S += "            <OEMInformation>" + Environment.NewLine;
                    if (lstUnattended.FindItemWithText("Model").Checked)
                    {
                        A = 5;
                        S += "                <Model>" + lstUnattended.FindItemWithText("Model").SubItems[1].Text +
                              "</Model>" + Environment.NewLine;
                    }
                    if (lstUnattended.FindItemWithText("Support Hours").Checked)
                    {
                        A = 6;
                        S += "                <SupportHours>" +
                              lstUnattended.FindItemWithText("Support Hours").SubItems[1].Text + "</SupportHours>" +
                              Environment.NewLine;
                    }
                    if (lstUnattended.FindItemWithText("Support Phone").Checked)
                    {
                        A = 7;
                        S += "                <SupportPhone>" +
                              lstUnattended.FindItemWithText("Support Phone").SubItems[1].Text + "</SupportPhone>" +
                              Environment.NewLine;
                    }
                    if (lstUnattended.FindItemWithText("Support URL").Checked)
                    {
                        A = 8;
                        S += "                <SupportURL>" + lstUnattended.FindItemWithText("Support URL").SubItems[1].Text +
                              "</SupportURL>" + Environment.NewLine;
                    }
                    if (lstUnattended.FindItemWithText("Manufacturer").Checked)
                    {
                        A = 9;
                        S += "                <Manufacturer>" +
                              lstUnattended.FindItemWithText("Manufacturer").SubItems[1].Text + "</Manufacturer>" +
                              Environment.NewLine;
                    }
                    if (lstUnattended.FindItemWithText("Logo").Checked)
                    {
                        A = 10;
                        S += "                <Logo>" + lstUnattended.FindItemWithText("Logo").SubItems[1].Text + "</Logo>" +
                              Environment.NewLine;
                    }
                    S += "            </OEMInformation>" + Environment.NewLine;
                }
                if (lstUnattended.FindItemWithText("Accept EULA").Checked ||
                     lstUnattended.FindItemWithText("Hide Wireless Setup").Checked ||
                     lstUnattended.FindItemWithText("Network Location").Checked ||
                     lstUnattended.FindItemWithText("Updates").Checked)
                {
                    S += "            <OOBE>" + Environment.NewLine;
                    if (lstUnattended.FindItemWithText("Accept EULA").Checked)
                    {
                        A = 11;
                        S += "                <HideEULAPage>" +
                              lstUnattended.FindItemWithText("Accept EULA").Checked.ToString(CultureInfo.InvariantCulture).ToLower() + "</HideEULAPage>" +
                              Environment.NewLine;
                    }
                    if (lstUnattended.FindItemWithText("Hide Wireless Setup").Checked)
                    {
                        A = 12;
                        S += "                <HideWirelessSetupInOOBE>" +
                              lstUnattended.FindItemWithText("Hide Wireless Setup").Checked.ToString(CultureInfo.InvariantCulture).ToLower() +
                              "</HideWirelessSetupInOOBE>" + Environment.NewLine;
                    }
                    if (lstUnattended.FindItemWithText("Network Location").Checked)
                    {
                        A = 13;
                        S += "                <NetworkLocation>" +
                              lstUnattended.FindItemWithText("Network Location").SubItems[1].Text + "</NetworkLocation>" +
                              Environment.NewLine;
                    }
                    if (lstUnattended.FindItemWithText("Updates").Checked)
                    {
                        A = 14;
                        S += "                <ProtectYourPC>" +
                              lstUnattended.FindItemWithText("Updates").SubItems[1].Text.Substring(1, 1) + "</ProtectYourPC>" +
                              Environment.NewLine;
                    }
                    S += "            </OOBE>" + Environment.NewLine;
                }
                if (lstUnattended.FindItemWithText("Your Full Name").Checked)
                {
                    A = 15;
                    S += "            <RegisteredOwner>" + lstUnattended.FindItemWithText("Your Full Name").SubItems[1].Text +
                          "</RegisteredOwner>" + Environment.NewLine;
                }
                if (lstUnattended.FindItemWithText("Company Name").Checked)
                {
                    A = 16;
                    S += "            <RegisteredOrganization>" +
                          lstUnattended.FindItemWithText("Company Name").SubItems[1].Text + "</RegisteredOrganization>" +
                          Environment.NewLine;
                }

                if (lstUnattended.FindItemWithText("Profile Directory").Checked ||
                     lstUnattended.FindItemWithText("ProgramData Folder").Checked)
                {

                    S += "            <FolderLocations>" + Environment.NewLine;
                    if (lstUnattended.FindItemWithText("Profile Directory").Checked)
                    {
                        A = 17;
                        S += "                        <ProfilesDirectory>" +
                              lstUnattended.FindItemWithText("Profile Directory").SubItems[1].Text + "</ProfilesDirectory>" +
                              Environment.NewLine;
                    }
                    if (lstUnattended.FindItemWithText("ProgramData Folder").Checked)
                    {
                        A = 18;
                        S += "                        <ProgramData>" +
                              lstUnattended.FindItemWithText("ProgramData Folder").SubItems[1].Text + "</ProgramData>" +
                              Environment.NewLine;
                    }
                    S += "            </FolderLocations>" + Environment.NewLine;
                }

                if (lstUnattended.FindItemWithText("Admin Password").Checked || lstUsers.Items.Count > 0)
                {
                    S += "            <UserAccounts>" + Environment.NewLine;
                    if (lstUnattended.FindItemWithText("Admin Password").Checked)
                    {
                        A = 19;
                        S += "                <AdministratorPassword>" + Environment.NewLine;
                        S += "                    <Value>" +
                              lstUnattended.FindItemWithText("Admin Password").SubItems[1].Text + "</Value>" +
                              Environment.NewLine;
                        S += "                    <PlainText>true</PlainText>" + Environment.NewLine;
                        S += "                </AdministratorPassword>" + Environment.NewLine;
                    }
                    if (lstUsers.Items.Count > 0)
                    {
                        A = 20;
                        S += "                <LocalAccounts>" + Environment.NewLine;
                        foreach (ListViewItem LST in lstUsers.Items)
                        {
                            S += "                    <LocalAccount wcm:action=\"add\">" + Environment.NewLine;
                            S += "                        <Name>" + LST.Text + "</Name>" + Environment.NewLine;
                            S += "                        <Group>" + LST.SubItems[3].Text + "</Group>" + Environment.NewLine;
                            S += "                        <Password>" + Environment.NewLine;
                            if (!string.IsNullOrEmpty(LST.SubItems[2].Text))
                            {
                                A = -1;
                                S += "                            <Value>" + LST.SubItems[2].Text + "</Value>" +
                                      Environment.NewLine;
                                S += "                            <PlainText>true</PlainText>" + Environment.NewLine;
                            }
                            else
                            {
                                A = -2;
                                S += "                            <Value />" + Environment.NewLine;
                            }
                            S += "                        </Password>" + Environment.NewLine;

                            if (!string.IsNullOrEmpty(LST.SubItems[1].Text))
                            {
                                A = -3;
                                S += "                        <DisplayName>" + LST.SubItems[1].Text + "</DisplayName>" +
                                      Environment.NewLine;
                            }
                            else
                            {
                                A = -4;
                                S += "                        <DisplayName>" + LST.Text + "</DisplayName>" +
                                      Environment.NewLine;
                            }

                            if (!string.IsNullOrEmpty(LST.SubItems[4].Text))
                            {
                                A = -5;
                                S += "                        <Description>" + LST.SubItems[4].Text + "</Description>" +
                                      Environment.NewLine;
                            }
                            else
                            {
                                A = -6;
                                S += "                        <Description>" + LST.Text + "</Description>" +
                                      Environment.NewLine;
                            }

                            S += "                    </LocalAccount>" + Environment.NewLine;
                        }
                        S += "                </LocalAccounts>" + Environment.NewLine;
                    }

                    S += "            </UserAccounts>" + Environment.NewLine;
                }

                if (lstUnattended.FindItemWithText("Username").Checked)
                {
                    A = 21;
                    string ALPassword = "";
                    try
                    {
                        var lstAdmin = lstUnattended.FindItemWithText("Admin Password");
                        if (lstUnattended.FindItemWithText("Username").SubItems[1].Text.EqualsIgnoreCase("Administrator") && lstAdmin.Checked)
                        {
                            A = 22;
                            ALPassword = lstAdmin.SubItems[1].Text;
                        }
                        else
                        {
                            A = 23;
                            string ALUsername = lstUnattended.FindItemWithText("Username").SubItems[1].Text;
                            ALPassword = lstUsers.FindItemWithText(ALUsername).SubItems[2].Text;
                        }
                    }
                    catch
                    {
                    }

                    S += "            <AutoLogon>" + Environment.NewLine;

                    S += "                <Password>" + Environment.NewLine;
                    if (!string.IsNullOrEmpty(ALPassword))
                    {
                        A = 24;
                        S += "                <Value>" + ALPassword + "</Value>" + Environment.NewLine;
                        S += "                <PlainText>true</PlainText>" + Environment.NewLine;
                    }
                    else
                    {
                        A = 25;
                        S += "                        <Value />" + Environment.NewLine;
                    }
                    S += "                </Password>" + Environment.NewLine;
                    S += "                <Enabled>true</Enabled>" + Environment.NewLine;

                    if (lstUnattended.FindItemWithText("Logon Count").Checked)
                    {
                        A = 26;
                        S += "                <LogonCount>" + lstUnattended.FindItemWithText("Logon Count").SubItems[1].Text +
                              "</LogonCount>" + Environment.NewLine;
                    }
                    else
                    {
                        A = 27;
                        S += "                <LogonCount>9999999</LogonCount>" + Environment.NewLine;
                    }
                    A = 28;
                    S += "                <Username>" + lstUnattended.FindItemWithText("Username").SubItems[1].Text +
                          "</Username>" + Environment.NewLine;

                    S += "            </AutoLogon>" + Environment.NewLine;
                }
                if (lstUnattended.FindItemWithText("Time Zone").Checked)
                {
                    A = 29;
                    S += "            <TimeZone>" + lstUnattended.FindItemWithText("Time Zone").SubItems[1].Text +
                          "</TimeZone>" + Environment.NewLine;
                }

                S += "        </component>" + Environment.NewLine;

            }
            catch (Exception Ex)
            {
                LargeError LE = new LargeError("OOBE Error", "Error Generating OOBE", "Error in Area: " + A, Ex);
                LE.Upload();
                LE.ShowDialog();
            }
            return S;
        }

        private void UpdateText()
        {
            if (lstUnattended.CheckedItems.Count == 0 && lstUsers.Items.Count == 0)
            {
                txtUnattended.Text = "Please select options from the other tabs.";
                return;
            }

            txtUnattended.Text = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine;
            txtUnattended.Text += "<!--Created by Win Toolkit v" + cMain.WinToolkitVersion(true) + "-->" + Environment.NewLine;
            txtUnattended.Text += "<unattend xmlns=\"urn:schemas-microsoft-com:unattend\">" + Environment.NewLine;

            //WindowsPE
            txtUnattended.Text += "    <settings pass=\"windowsPE\">" + Environment.NewLine;

            txtUnattended.Text += Generate_WindowsPE("x86");
            txtUnattended.Text += Generate_WindowsPE("amd64");

            txtUnattended.Text += "    </settings>" + Environment.NewLine;

            //Generalize

            //Specialize
            if (lstUnattended.FindItemWithText("Serial").Checked || lstUnattended.FindItemWithText("Computer Name").Checked)
            {
                txtUnattended.Text += "    <settings pass=\"specialize\">" + Environment.NewLine;

                if (lstUnattended.FindItemWithText("Serial").Checked ||
                     lstUnattended.FindItemWithText("Computer Name").Checked ||
                     lstUnattended.FindItemWithText("Skip Auto-Activation").Checked)
                {
                    txtUnattended.Text += Generate_ShellSetup("x86");
                    txtUnattended.Text += Generate_ShellSetup("amd64");
                }

                if (lstUnattended.FindItemWithText("Skip Auto-Activation").Checked)
                {
                    txtUnattended.Text += Generate_SecuritySPPUX("x86");
                    txtUnattended.Text += Generate_SecuritySPPUX("amd64");
                }

                txtUnattended.Text += "    </settings>" + Environment.NewLine;
            }

            //oobeSystem
            if (lstUnattended.FindItemWithText("Colour Depth").Checked ||
                 lstUnattended.FindItemWithText("Refresh Rate").Checked ||
                 lstUnattended.FindItemWithText("Screen Resolution").Checked ||
                 lstUnattended.FindItemWithText("Manufacturer").Checked ||
                 lstUnattended.FindItemWithText("Support Phone").Checked ||
                 lstUnattended.FindItemWithText("Support Hours").Checked ||
                 lstUnattended.FindItemWithText("Support URL").Checked || lstUnattended.FindItemWithText("Model").Checked ||
                 lstUnattended.FindItemWithText("Logo").Checked || lstUnattended.FindItemWithText("Accept EULA").Checked ||
                 lstUnattended.FindItemWithText("Hide Wireless Setup").Checked ||
                 lstUnattended.FindItemWithText("Network Location").Checked ||
                 lstUnattended.FindItemWithText("Updates").Checked || lstUnattended.FindItemWithText("Your Full Name").Checked ||
                 lstUnattended.FindItemWithText("Company Name").Checked ||
                 lstUnattended.FindItemWithText("Admin Password").Checked || lstUsers.Items.Count > 0 ||
                 lstUnattended.FindItemWithText("Time Zone").Checked ||
                 lstUnattended.FindItemWithText("Profile Directory").Checked ||
                 lstUnattended.FindItemWithText("ProgramData Folder").Checked || lstUsers.Items.Count > 0 ||
                 lstUnattended.FindItemWithText("Username").Checked)
            {
                txtUnattended.Text += "    <settings pass=\"oobeSystem\">" + Environment.NewLine;

                txtUnattended.Text += Generate_OOBE("x86");
                txtUnattended.Text += Generate_OOBE("amd64");

                txtUnattended.Text += "    </settings>" + Environment.NewLine;
            }

            txtUnattended.Text += "</unattend>" + Environment.NewLine;

            mnuCreate.Enabled = true;
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}