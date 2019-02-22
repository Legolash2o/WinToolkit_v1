using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;
using WinToolkit.Prompts;

namespace WinToolkit
{


    public partial class frmToolsManager : Form
    {
        private int tFile;



        public frmToolsManager()
        {

            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void frmToolsManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormCollection Forms = Application.OpenForms;
            int FO = Forms.Cast<Form>().Count();
            if (FO == 1)
            {
                Application.Exit();
            }
            else
            {
                cMain.FreeRAM();
            }
        }

        private void frmToolsManager_MouseWheel(object sender, MouseEventArgs e)
        {
            TabPage SelTab = tabControl2.SelectedTab;
            switch (SelTab.Text)
            {
                case "Basic":
                    FLPPop.Select();
                    break;
                case "Intermediate":
                    FLPIntermediate.Select();
                    break;
                case "Advanced":
                    FLPAdvanced.Select();
                    break;
                case "Updates":
                    FLPUpdates.Select();
                    break;
            }
        }

        private void frmToolsManager_FormClosing(object sender, CancelEventArgs e)
        {
            cMain.OnFormClosing(this);
            cMain.FreeRAM();
            FormCollection Forms = Application.OpenForms;
            int FO = Forms.Cast<Form>().Count();

        if (cMain.openForms > 0)
            {
                MessageBox.Show("You have extra forms open. Please close these first.","Notice");
                e.Cancel = true;
                return;
            }

            if (FO > 2)
            {
                cMain.tmLast = tabControl2.SelectedTab.Text;
            }
            if (FO <= 2)
            {
                if (cOptions.AVScan && cMain.DetectAntivirus() == false && cMain.AVShown)
                {
                    var AV = new Prompts.frmAntiVirus();
                    AV.GBAV.Visible = true;
                    AV.chkAV.Visible = false;
                    AV.GBAV.Text = "Anti-Virus has been disabled!";
                    AV.lblAV.Text = "Win Toolkit has detected that you had turned off your Anti-Virus after starting Win Toolkit. This is just to remind you to turn it back on.";
                    AV.ShowDialog();
                }

                Enabled = false;

                Text = "Saving Settings...";
                Application.DoEvents();
                cOptions.SaveSettings();
                cMain.FreeRAM();



                cMain.CleanExit(this);
                Text = "Exiting...";
                Application.DoEvents();

            }
        }

        private void frmToolsManager_Shown(object sender, EventArgs e)
        {
            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Closing Forms...");
                Application.DoEvents();
                var oForms = (from Form F in Application.OpenForms where F != this && F.Name != "frmStartup" && F.Name != "frmUpdate" && !F.IsDisposed select F).ToList();
                foreach (Form F in oForms)
                {
                    try
                    {
                        F.Close();
                        F.Dispose();
                    }
                    catch (Exception Ex) { }
                }
            }
            catch (Exception Ex) { }

            cMain.FreeRAM();

            Text = "Win Toolkit";
            Application.DoEvents();

            cMain.UpdateToolStripLabel(lblStatus, "Scanning for AV...");
            if (cOptions.AVScan && cMain.DetectAntivirus() && cMain.AVShown == false)
            {
                cMain.AVShown = true;
                Application.DoEvents();
                var AV = new Prompts.frmAntiVirus();
                AV.GBAV.Visible = true;
                AV.ShowDialog();
            }
            cMain.FreeRAM();
            Visible = true;
            cMain.UpdateToolStripLabel(lblStatus, "Loaded");
            cMain.StatusHistory.Clear();
            Enabled = true;
            if (cOptions.ShowWelcome)
            {
                new Prompts.frmWelcome().ShowDialog();
            }

            if (!cMain.DismShown && cOptions.DISMUpdate)
            {
                var newVersion = false;

                var dismVersion = DISM.Latest.Version;
                if (Environment.OSVersion.Version.Major < 6 && dismVersion < new Version("6.2.9200.16384"))
                {
                    newVersion = true;
                }
                if (Environment.OSVersion.Version.Major >= 6 && dismVersion < new Version("6.3.9600.16384"))
                {
                    newVersion = true;
                }

                if (newVersion)
                {
                    cMain.DismShown = true;
                    DialogResult DR = MessageBox.Show("A newer version of DISM is available which will allow you to work with Windows 8 images. It is highly recommended to download this!\n\nWould you like to download it now?", "New DISM Available", MessageBoxButtons.YesNo);
                    if (DR == DialogResult.Yes)
                    {
                        cMain.OpenLink("http://www.wincert.net/leli55PK/DISM/");
                    }
                }
            }

           
        }


        private void frmToolsManager_Load(object sender, EventArgs e)
        {

            mnuVersion.Text = "v" + cMain.WinToolkitVersion(true);

            ListViewEx.LVE = null; cMain.eForm = this;

            if (!string.IsNullOrEmpty(cOptions.AIOTime))
            {
                groupBox9.Text += "Last image completed in " + cOptions.AIOTime;
                lblAIOInt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            }

            if (Environment.OSVersion.Version.Major < 6)
            {
                cmdUSBBootPrep.Visible = false;
                mnuInstallers.Visible = false;
                cmdUpdateRetriever.Visible = false;
            }

            cMain.FormIcon(this); cMain.eLBL = lblStatus;

            scMain.Scale4K(_4KHelper.Panel.Pan1);
            scBasic.Scale4K(_4KHelper.Panel.Pan1);
            scIntermediate.Scale4K(_4KHelper.Panel.Pan1);
            scAdvanced.Scale4K(_4KHelper.Panel.Pan1);
            scUpdates.Scale4K(_4KHelper.Panel.Pan1);
            cMain.UpdateToolStripLabel(lblStatus, "Setting ToolTips...");
            Application.DoEvents();
            try
            {
                //Popular
                cMain.SetToolTip(cmdAIOI,
                                      "Have everything pre-installed next time you re-install Windows. This tool lets you integrate:" +
                                      Environment.NewLine + Environment.NewLine + "*Addons - Your favorite programs." +
                                      Environment.NewLine +
                                      "*Component Removal - Remove features from Windows to reduce the installation size." +
                                      Environment.NewLine + "*Drivers - Lets all your drivers work out-of-the-box." +
                                      Environment.NewLine +
                                      "*Files - Copy and replace files into your WIM image; very useful for modified files." +
                                      Environment.NewLine +
                                      "*Gadgets - Have all your favorite desktop gadgets in the gadgets list." +
                                      Environment.NewLine + "*Language Packs - Have Windows in any language you want." +
                                      Environment.NewLine +
                                      "*Services - Set the default state for services, including Black Viper's presets!" +
                                      Environment.NewLine +
                                      "*Silent Installs - Installs all your apps silently at first logon." +
                                      Environment.NewLine + "*Theme Packs - Integrate all your favorite theme packs." +
                                      Environment.NewLine +
                                      "*Tweaks - Registry enhancements, custom backgrounds and/or logon screen." +
                                      Environment.NewLine + "*Updates - Keep your computer up-to-date." + Environment.NewLine +
                                      "*Wallpapers - Have your favorite wallpapers ready for selection.");
               // cMain.SetToolTip(cmdUpdCache, "The updateCache.xml files helps make updates load into the AIO list quicker. It saves known updates from being extracted to find out any information.", "Update updateCache.db");
                cMain.SetToolTip(cmdISOMaker, "Once you've finished with your image customization. You can\nmake a new ISO and install your new Windows.", "ISO Maker");
                cMain.SetToolTip(cmdUSBBootPrep, "Save your DVDs and install from a USB or memory stick instead.\nOnce prepared you can then install Windows from your device.", "USB Boot Prep");
                cMain.SetToolTip(cmdAIODisk,
                                      "Merge all your images into one file so you can have them all on one DVD. For example, you can merge x86 and x64 images together!");
                cMain.SetToolTip(cmdCR, "This tool lets you uninstall Windows packages and features, such as Windows Media\nPlayer, Internet Explorer, Media Samples, Language Packs, Drivers and Updates to\nname a few from an offline image. This feature has more options than the All-In-One\nIntegrator 'Components' list because it scans the image, whereas AIO just displays\ncommon ones. This tool is also great for Windows 8.");
                cMain.SetToolTip(cmdWHD,
                                      "Download 'WHDownloader' to keep track of all the latest Windows updates for Win 7, 8.1 and Office 2013.", "WHDownloader");
                cMain.SetToolTip(cmdUC,
                                      "Install Windows automatically so you can watch your favorite films, have a brew or just go outdoors whilst Windows is installing.");
                cMain.SetToolTip(cmdWM,
                                      "Manage all the images into one Windows Installation Image (*.wim file)." +
                                      Environment.NewLine + Environment.NewLine +
                                      "You can delete images, edit names, edit descriptions, make ISOs, import images, merge images," +
                                      Environment.NewLine +
                                      "export images, mount/unmount images, rebuild (shrink) and apply unattended files.");
                cMain.SetToolTip(cmdRHM,
                                      "Mount the registry within an image, so you can add tweaks and the usual registry stuff." +
                                      Environment.NewLine + "This tool also lets you import your custom registry tweaks.");

                //Tools

                //SetToolTip(cmdAM, "");
            }
            catch (Exception Ex) { }

            cMain.UpdateToolStripLabel(lblStatus, "Removing tabs...");

            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Selecting tabs...");
                if (cMain.tmLast != null && tabControl2.IsDisposed == false)
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Selecting [" + cMain.tmLast + "] Tab...");

                    foreach (TabPage TP in tabControl2.TabPages)
                    {
                        if (TP.Text == cMain.tmLast) { tabControl2.SelectedTab = TP; break; }
                    }

                }

            }
            catch { }

            try
            {
                BringToFront();
            }
            catch
            {
            }
        }

        private void cmdWM_Click(object sender, EventArgs e)
        {
            cMain.SWT = 0;
            cMain.OpenForm(new frmWIMManager(), this, cmdWM);
        }

        private void cmdRHM_Click(object sender, EventArgs e)
        {
            cMain.SWT = 2;
            cMain.OpenForm(new frmWIMManager(), this, cmdRHM);
        }

        private void cmdAIOI_Click(object sender, EventArgs e)
        {
            cMain.SWT = 1;
            cMain.OpenForm(new frmWIMManager(), this, cmdAIOI);
        }

        private void cmdCR_Click(object sender, EventArgs e)
        {
            cMain.SWT = 3;
            cMain.OpenForm(new frmWIMManager(), this, cmdCR);
        }

        private void cmdSU_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://www.majorgeeks.com/files/details/windows_hotfix_downloader.html");
        }

        private void cmdUC_Click(object sender, EventArgs e)
        {
            cMain.OpenForm(new frmUnattendedCreator(), this, cmdUC);
        }

        private void cmdAIODisk_Click(object sender, EventArgs e)
        {
            cMain.OpenForm(new frmAIODiskCreator(), this, cmdAIODisk);
        }

        private void cmdWin7SP1_Click(object sender, EventArgs e)
        {
            var TS = new frmDownload_ISO().ShowDialog();
        }

        private void BWUpdCat_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Text = " [Loading Update Catalog...]";
            Application.DoEvents();
            cMain.OpenForm(new frmUpdCatalog(), this, cmdWHD);

        }

        private void BWUpdCat_DoWork(object sender, DoWorkEventArgs e)
        {
        }
        

        private void cmdOptions_Click(object sender, EventArgs e)
        {
            cMain.OpenForm(new frmOptions(), this, cmdUpdateRetriever);
        }

        private void cmdReportBug_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://www.wincert.net/forum/index.php?/forum/179-win-toolkit/&do=add");
        }
        

        private void cmdForum_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://www.wincert.net/forum/index.php?/forum/179-win-toolkit/");
        }

      
        private void cmdAbout_Click(object sender, EventArgs e)
        {
            new Prompts.frmAbout().ShowDialog();
        }

        private void cmdLangToCab_Click(object sender, EventArgs e)
        {
            cMain.OpenForm(new frmLPConvert(), this, cmdLangToCab);
        }

        private void cmdExeToMsp_Click(object sender, EventArgs e)
        {
            cMain.OpenForm(new frmEXEtoMSP(), this, cmdExeToMsp);
        }

        private void cmdMsuToCab_Click(object sender, EventArgs e)
        {
            cMain.OpenForm(new frmMSUtoCAB(), this, cmdMsuToCab);
        }

        private void cmdCaptureImage_Click(object sender, EventArgs e)
        {
            cMain.OpenForm(new frmCaptureImg(), null, cmdCaptureImage);
        }

        private void cmdSWMMerger_Click(object sender, EventArgs e)
        {
            cMain.OpenForm(new frmWIMMerger(), this, cmdSWMMerger);
        }

        private void cmdWIMSplitter_Click(object sender, EventArgs e)
        {
            cMain.OpenForm(new frmWIMSplitter(), this, cmdWIMSplitter);
        }

        private void cmdDriverInstaller_Click(object sender, EventArgs e)
        {
            cMain.OpenForm(new frmDrvInstaller(), this, cmdDriverInstaller);
        }

        private void cmdUpdateInstaller_Click(object sender, EventArgs e)
        {
            cMain.OpenForm(new frmUpdInstaller(), this, cmdUpdateInstaller);
        }

        private void cmdAddonMaker_Click(object sender, EventArgs e)
        {
            cMain.OpenForm(new frmAddonMaker(), this, cmdAddonMaker);
        }

        private void cmdUpdateRetriever_Click(object sender, EventArgs e)
        {
            cMain.OpenForm(new frmUpdRetriever(), this, cmdUpdateRetriever);
        }


        private void cmdWindowsISO_Click(object sender, EventArgs e)
        {
            new frmDownload_ISO().ShowDialog();
        }

        private void cmdDAddons_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://www.wincert.net/forum/index.php?/forum/180-windows-7-toolkit-addons/");
        }

     
        private void cmdDSFX_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://www.wincert.net/forum/index.php?/forum/129-switchless-installers/");
        }

        private void cmdDThemePacks_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://windows.microsoft.com/en-US/windows/downloads/personalize/themes");
        }

        private void cmdDWallpapers_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://windows.microsoft.com/en-US/windows/downloads/personalize/wallpaper-desktop-background");
        }

        private void cmdDWin7SP1LangPacks_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://www.pcdiy.com/146/windows-7-service-pack-1-language-packs-download");
        }

        private void cmdDVirtualPC_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://www.microsoft.com/en-us/download/details.aspx?id=3702");
        }

        private void cmdDVirtualBox_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://www.virtualbox.org/wiki/Downloads");
        }

        private void cmdDXPMode_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://www.microsoft.com/en-gb/download/details.aspx?id=8002");
        }

        private void cmdVMWare_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://my.vmware.com/web/vmware/free#desktop_end_user_computing/vmware_player/");
        }

        private void cmdIE11_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://windows.microsoft.com/en-us/internet-explorer/ie-11-worldwide-languages");
        }

        private void cmdDGoogleChrome_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://support.google.com/chrome/answer/95346?co=GENIE.Platform%3DDesktop&hl=en-GB");
        }

        private void cmdDFirefox_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://www.mozilla.org/en-US/firefox/all/");
        }

        private void cmdDOpera_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://www.opera.com/download/");
        }

        private void cmdDSafari_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://support.apple.com/en_US/downloads/#safari");
        }

        private void cmdDDISM_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://wintoolkit.co.uk/Home/Downloads");
        }
     

        private void cmdDSlim451Installer_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("https://repacks.net/forum/viewtopic.php?t=7");
        }
        

        private void cmdGuides_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://www.wincert.net/forum/index.php?showforum=192");
        }

        private void cmdDisclaimer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Use this tool at your own risk. I don't take any responsibility for damages, errors, bugs or data loss caused by you using this program.", "Notice");
        }

        private void cmdUSBBootPrep_Click(object sender, EventArgs e)
        {
            Files.DeleteFile(cMain.UserTempPath + "\\USBPrep.txt");
            cMain.OpenForm(new frmUSBPrep(), this, cmdUSBBootPrep);
        }

        private void cmdISOMaker_Click(object sender, EventArgs e)
        {
            cMain.OpenForm(new frmISOMaker(), this, cmdISOMaker);
        }

   
        private void cmdWallpapersWide_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://wallpaperswide.com/");
        }
        
     


        private static string GetValue(string Input, string Value)
        {
            string newValue = Input.Substring(Value.Length);
            if (newValue.ToUpper().EndsWithIgnoreCase(Value.Insert(1, "/").ToUpper()))
            {
                return newValue.Substring(0, newValue.Length - (Value.Length + 1)).Trim();
            }
            return newValue.Trim();
        }
        

        private void mnuDNTLITE_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://www.ntlite.com/");
        }


        //private void cmdUpdCache_Click(object sender, EventArgs e)
        //{
        //    this.Enabled = false;
        //    cMain.UpdateText(lblCache, "Checking for updateCache.db update.");
        //    UpdateCache.Update();
        //    cMain.UpdateText(lblCache, "Loading Update Cache...");
        //    try
        //    {
        //        UpdateCache.Load();
        //        cMain.UpdateText(lblCache, "Update Cache Loaded...");
        //    }
           
        //    catch (Exception ex)
        //    {
        //        cMain.UpdateText(lblCache, "Update Cache Error...");
        //        new SmallError("Error loading new cache.", ex).Upload();
        //    }
        //    this.Enabled = true;
        //}

        private void cmdBrowseUpdateCache_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://wincert.net/leli55PK/updateCache.xml");
        }

        private void mnuWinKey_Click(object sender, EventArgs e)
        {
            frmWinKey key = new frmWinKey();
            key.ShowDialog();
        }

      
    }
}