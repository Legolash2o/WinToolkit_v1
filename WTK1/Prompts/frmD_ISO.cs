using System;
using System.Collections;
using System.Windows.Forms;
using Microsoft.Win32;
using WinToolkit.Classes.Helpers;

namespace WinToolkit
{
    public partial class frmDownload_ISO : Form
    {
        public frmDownload_ISO()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            CheckForIllegalCrossThreadCalls = false;
        }

        private void cmdWin7SP1_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://www.microsoft.com/en-us/software-recovery");
        }

        private void frmD_ISO_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void frmD_ISO_Load(object sender, EventArgs e)
        {
            cMain.FormIcon(this);
            scMain.Scale4K(_4KHelper.Panel.Pan2);
            cMain.SetToolTip(cmdWin7SP1, "Download Windows 7 SP1, Microsoft's latest service pack.");
            cMain.SetToolTip(cmdWin81, "It's Windows re-imagined and reinvented from a solid core of Windows 7 speed and reliability.\nIt's an all-new touch interface. It's a new Windows for new devices.");

          

           //tabControl3.Visible = cOptions.ValidKey;
            //GBNotice.Visible = !cOptions.ValidKey;
           
        }

     

        private void cmdWindows8Buy_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://www.microsoftstore.com/store/msusa/en_US/cat/categoryID.70036700");
        }

        private void cmdWin81_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://msdn.microsoft.com/evalcenter/jj554510");
        }



        private void cmdGx86_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://drive.google.com/uc?id=0B09oiSXI1SmoelRpMU5oeHc5YUk&export=download");
        }

        private void cmdGx64_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://drive.google.com/uc?id=0B09oiSXI1Smob3BZQ2R1NWlZLTg&export=download");
        }

        private void cmdDx86_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://www.dropbox.com/s/pusv6zo9khrp928/Win7SP1x86_Sept2014.iso?dl=0");
        }

        private void cmdDx64_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://www.dropbox.com/s/014xrz7u0f6jbe2/Win7SP1x64_Sept2014.iso?dl=0");
        }

    
        private void cmdDx64NET_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://www.dropbox.com/s/r6c7n1xscoczn2q/Win7SP1x64_NET_Sept2014.iso?dl=0");
        }

        private void cmdDx86NET_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://www.dropbox.com/s/vpqmvmk6sj0oxqo/Win7SP1x86_NET_Sept2014.iso?dl=0");
        }

        private void cmdGx64NET_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://drive.google.com/uc?id=0B09oiSXI1SmodzZ3cEl0YVZXN1U&export=download");
        }

        private void cmdGx86NET_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://drive.google.com/uc?id=0B09oiSXI1SmocTBMSjVYLVNJeWc&export=download");
        }

        private void cmdWin2012R2_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://technet.microsoft.com/en-US/evalcenter/dn205286.aspx");
        }


        private void cmdWin10_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://www.microsoft.com/en-au/software-download/windows10");
        }
    }
}
