using System;
using System.Collections;
using Microsoft.Win32;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.Helpers;

namespace WinToolkit.Prompts
{
    public partial class frmKeys : Form
    {
        public frmKeys()
        {
            InitializeComponent();
            cMain.FormIcon(this);
        }

        private void frmKeys_Load(object sender, EventArgs e)
        {
            splitContainer1.Scale4K(_4KHelper.Panel.Pan2);
            splitContainer2.Scale4K(_4KHelper.Panel.Pan2);
            cMain.AutoSizeColums(lstKeys);
            RegistryKey oRegKeyM = null;
            try
            {
                oRegKeyM = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");
                if (oRegKeyM != null)
                {
                    var digitalProductId = oRegKeyM.GetValue("DigitalProductId") as byte[];
                    var productEdition = oRegKeyM.GetValue("EditionID") as string;
                    string ProductKey = DecodeProductKey(digitalProductId);

                    if (ProductKey != "BBBBB-BBBBB-BBBBB-BBBBB-BBBBB")
                    {
                        ListViewItem currentKey = new ListViewItem();
                        currentKey.Text = productEdition;
                        currentKey.SubItems.Add(ProductKey);
                        currentKey.ImageIndex = 0;
                        currentKey.Group = lstKeys.Groups[0];
                        lstKeys.Items.Add(currentKey);
                    }

                }
            }
            catch (Exception Ex)
            {
                new SmallError("Unable to detect serial", Ex).Upload();
            }
            if (oRegKeyM != null)
            {
                oRegKeyM.Close();
            }

            foreach (ListViewItem LST in lstKeys.Items)
            {
                if (LST.SubItems[1].Text == txtKey.Text)
                {
                    LST.Selected = true;
                    break;
                }
            }
            Height -= 1;
        }

        private void lstKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstKeys.SelectedItems.Count > 0)
            {
                txtKey.Text = lstKeys.SelectedItems[0].SubItems[1].Text;
            }
        }

        private void lstKeys_DoubleClick(object sender, MouseEventArgs e)
        {
            if (lstKeys.SelectedItems.Count > 0)
            {
                txtKey.Text = lstKeys.SelectedItems[0].SubItems[1].Text;
                DialogResult = DialogResult.OK;
            }
        }

        private static string DecodeProductKey(byte[] digitalProductId)
        {
            const int keyStartIndex = 52;
            const int keyEndIndex = keyStartIndex + 15;
            var digits = new[]
                                      {
                                            'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'M', 'P', 'Q', 'R', 'T', 'V', 'W', 'X', 'Y', '2',
                                            '3', '4', '6', '7', '8', '9'
                                      };
            const int decodeLength = 29;
            const int decodeStringLength = 15;
            var decodedChars = new char[decodeLength];
            var hexPid = new ArrayList();
            for (int i = keyStartIndex; i <= keyEndIndex; i++)
            {
                hexPid.Add(digitalProductId[i]);
            }
            for (int i = decodeLength - 1; i >= 0; i--)
            {
                // Every sixth char is a separator.
                if ((i + 1) % 6 == 0)
                {
                    decodedChars[i] = '-';
                }
                else
                {
                    // Do the actual decoding.
                    int digitMapIndex = 0;
                    for (int j = decodeStringLength - 1; j >= 0; j--)
                    {
                        int byteValue = (digitMapIndex << 8) | (byte)hexPid[j];
                        hexPid[j] = (byte)(byteValue / 24);
                        digitMapIndex = byteValue % 24;
                        decodedChars[i] = digits[digitMapIndex];
                    }
                }
            }
            return new string(decodedChars);
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {


            if (txtKey.Text.ToUpper().EqualsIgnoreCase("XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"))
            {
                DialogResult = DialogResult.Cancel;
                return;
            }

            txtKey.Text = txtKey.Text.ReplaceIgnoreCase("-", "");

            if (txtKey.Text.Length == 25)
            {
                txtKey.Text = txtKey.Text.Insert(5, "-");
                txtKey.Text = txtKey.Text.Insert(11, "-");
                txtKey.Text = txtKey.Text.Insert(17, "-");
                txtKey.Text = txtKey.Text.Insert(23, "-");

            }

            if ((txtKey.Text.Length != 25 && txtKey.Text.Length != 29) || (txtKey.Text.Length == 29 && !txtKey.Text.ContainsIgnoreCase("-")))
            {

                MessageBox.Show("You have not entered a valid serial code!", "Invalid Serial");
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cmsCopyClipboard_Click(object sender, EventArgs e)
        {
            if (lstKeys.SelectedItems.Count > 0)
            {
                Clipboard.Clear();
                Clipboard.SetText(lstKeys.SelectedItems[0].SubItems[1].Text);
            }
        }
    }
}
