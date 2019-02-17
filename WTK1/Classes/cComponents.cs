using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;

namespace WinToolkit
{
    public class cComponents
    {
        static WIMImage image;
        public cComponents(WIMImage currentImage)
        {
            image = currentImage;
        }

        public void GetMetro(ListView list)
        {
            
            string key = @"WIM_Software\Classes\Local Settings\Software\Microsoft\Windows\CurrentVersion\AppModel\PackageRepository\Packages\";
            using (var oRegKeyM = Registry.LocalMachine.OpenSubKey(key, RegistryKeyPermissionCheck.ReadSubTree))
            {
                if (oRegKeyM == null) return;

                foreach (string S in oRegKeyM.GetSubKeyNames())
                {
                    string pRegName = cReg.GetValue(Registry.LocalMachine, key + S + "\\", "Path");
                    if (string.IsNullOrEmpty(pRegName) | !pRegName.Contains("\\WindowsApps\\"))
                        continue;

                    string pPackage = S;
                    string pName = S.Substring(0, S.IndexOf('_'));

                    if (!string.IsNullOrEmpty(pName) && !string.IsNullOrEmpty(pPackage))
                    {
                        var NI = new ListViewItem();
                        NI.Text = pName;
                        NI.Group = list.Groups[11];
                        NI.BackColor = Color.LightGreen;
                        NI.SubItems.Add("Installed");
                        NI.SubItems.Add(pPackage);
                        NI.SubItems.Add(pRegName);
                        list.Items.Add(NI);
                    }
                }

            }
        }

        public void GetPackages(string inPut, ListView list, string DVDLoc, ToolStripLabel lblStatus)
        {
            var NIDVDUpgrade = new ListViewItem();
            var NIDVDSupport = new ListViewItem();
            var NIWinSXSBackup = new ListViewItem();
            var NIPictureSamples = new ListViewItem();

            var NITEase = new ListViewItem();
            var NITAero = new ListViewItem();
            var NITNAero = new ListViewItem();
            list.Visible = false;
            list.Items.Clear();
            list.Columns[0].Text = "Name (" + list.Items.Count + ")";

            Application.DoEvents();
            int iCount = 0, iTotal = 0;
            foreach (string Line in inPut.Split(Environment.NewLine.ToCharArray()))
            {
                if (Line.ContainsIgnoreCase("~~") && !Line.ContainsIgnoreCase("WUClient") && !Line.ContainsIgnoreCase("Uninstall") && !string.IsNullOrEmpty(Line))
                {
                    iTotal++;
                }
            }

            foreach (string Line in inPut.Split(Environment.NewLine.ToCharArray()))
            {
                if (string.IsNullOrEmpty(Line)) { continue; }
                if (Line.ContainsIgnoreCase("~~") && !Line.ContainsIgnoreCase("WUClient") && !Line.ContainsIgnoreCase("Uninstall") && !string.IsNullOrEmpty(Line))
                {
                    iCount++;
                    try
                    {
                        var A = new ListViewItem();
                        string AName = Line;
                        string APackage = Line;
                        while (AName.ContainsIgnoreCase("~"))
                        {
                            AName = AName.Substring(0, AName.Length - 1);
                        }
                        A.Text = AName;

                        if (iCount.ToString().EndsWithIgnoreCase("0"))
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Finding Package [" + iCount + "/" + iTotal + "] :: " + AName);
                            Application.DoEvents();
                        }

                        //while (APackage.ContainsIgnoreCase("~"))
                        //{
                        //    APackage = APackage.Substring(0, APackage.Length - 1);
                        //}
                        string State = Line;

                        try
                        {
                            while (!State.StartsWithIgnoreCase("|"))
                                State = State.Substring(1);

                            State = State.Substring(2);
                            while (State.ContainsIgnoreCase("|"))
                                State = State.Substring(0, State.Length - 1);

                            while (State.EndsWithIgnoreCase(" "))
                                State = State.Substring(0, State.Length - 1);

                        }
                        catch (Exception Ex)
                        {
                            State = "N/A";
                        }

                        A.SubItems.Add(State);
                        A.SubItems.Add("");
                        A.SubItems.Add("");
                        A.Group = list.Groups[12];

                        while (APackage.ContainsIgnoreCase(" "))
                        {
                            APackage = APackage.Substring(0, APackage.Length - 1);
                        }

                        if (Line.ContainsIgnoreCase("_for_KB"))
                        {
                            if (A.Text.ContainsIgnoreCase("KB"))
                            {
                                while (!A.Text.StartsWithIgnoreCase("KB"))
                                {
                                    A.Text = A.Text.Substring(1);
                                }
                            }

                            A.Text = A.Text.ReplaceIgnoreCase("_RTM", "");
                            A.Text = A.Text.ReplaceIgnoreCase("_SP1", "");
                            A.Text = A.Text.ReplaceIgnoreCase("_SP2", "");

                            if (inPut.ContainsIgnoreCase(A.Text + "_BF"))
                            {
                                A.Text = A.Text + "_BF";
                            }

                            A.Group = list.Groups[14];
                            A.BackColor = Color.LightGreen;
                            if (Line.ContainsIgnoreCase("KB976902") || Line.ContainsIgnoreCase("KB976932") || Line.ContainsIgnoreCase("KB976933"))
                            {
                                //Windows 7 SP1
                                A.Text = "N/A";
                            }

                            if (Line.ContainsIgnoreCase("KB2534111"))
                            {
                                A.Text = "KB2534111 Refresh Fix";
                                A.ToolTipText =
                                     "Built-in: Fixes 'Computer name cannot contain only numbers' error message when you install Windows 7 by using Windows 7 SP1 integrated installation media";
                            }
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Hyper-V-ClientEdition-Package"))
                        {
                            A.Text = "Hyper-V";
                            A.ToolTipText =
                                 "Provides services and management tools for creating and running virtual machines and their resources.";
                            A.Group = list.Groups[9];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Hyper-V-Management-Clients-Package"))
                        {
                            A.Text = "Hyper-V Tools";
                            A.ToolTipText = "Hyper-V Tools";
                            A.Group = list.Groups[9];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Hyper-V-Common-Drivers"))
                        {
                            A.Text = "Hyper-V VMBUS Driver";
                            A.ToolTipText = "Hyper-V VMBUS Driver (Common)";
                            A.Group = list.Groups[9];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Hyper-V-Management-PowerShell-Package"))
                        {
                            A.Text = "Hyper-V Management PowerShell";
                            A.ToolTipText = "Hyper-V Management PowerShell";
                            A.Group = list.Groups[9];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Virtualization-Client-Interop-Package"))
                        {
                            A.Text = "Hyper-V Management Client Interop";
                            A.ToolTipText = "Hyper-V Management Client Interop";
                            A.Group = list.Groups[9];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Hyper-V-Guest"))
                        {
                            A.Text = "Hyper-V Network VSC Driver";
                            A.ToolTipText = "Hyper-V Network VSC Driver (Guest)";
                            A.Group = list.Groups[9];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-") && Line.ContainsIgnoreCase("Edition~"))
                        {
                            //NEW
                            A.Text = "N/A";
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Media-Foundation-Package"))
                        {
                            //NEW
                            A.Text = "N/A";
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Printing-Foundation-Starter-Package"))
                        {
                            //NEW
                            A.Text = "Windows Fax and Scan";
                            A.ToolTipText = "Enable fax and scan tasks on this computer";
                            A.Group = list.Groups[7];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Display-ChangeDesktopBackground-Disabled-Package"))
                        {
                            //NEW

                            A.Text = "Disabled Change Desktop Background in Display Control Panel";
                            A.ToolTipText = "Disabled Change Desktop Background in Display Control Panel";
                            A.Group = list.Groups[12];
                            A.BackColor = Color.Yellow;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Starter-Features-Package"))
                        {
                            //NEW
                            A.Text = "N/A";
                        }

                        if (Line.ContainsIgnoreCase("Anytime-Upgrade-Results-Package"))
                        {
                            A.Text = "Windows Anytime Upgrade Results";
                            A.ToolTipText = "Lets you upgrade your edition of Windows i.e. Home Premium to Ultimate";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Windows-Backup-Package"))
                        {
                            A.Text = "Windows Backup";
                            A.ToolTipText = "Adds or removes access to Windows Backup from the Start Menu and Desktop";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Windows-BLB-Client"))
                        {
                            A.Text = "Windows Block Level Backup";
                            A.ToolTipText =
                                 "Windows Block Level Backup provides protection of windows OS and data for disaster recovery scenarios";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Branding-") && Line.ContainsIgnoreCase("-Client-Package"))
                        {
                            A.Text = "N/A";
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-BusinessScanning-Feature-Package"))
                        {
                            A.Text = "Scan Management";
                            A.ToolTipText = "Manages distributed scanners, scan processes, and scan servers";
                            A.Group = list.Groups[7];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Client-Drivers-Package"))
                        {
                            A.Text = "N/A";
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Client-Features-Package"))
                        {
                            A.Text = "N/A";
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Client-Wired-Network-Drivers-Package"))
                        {
                            A.Text = "Ethernet Drivers";
                            A.ToolTipText = "Removes all wired and wireless network drivers";
                            A.Group = list.Groups[1];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-ClipsInTheLibrary-Package"))
                        {
                            A.Text = "Clips In The Library";
                            A.ToolTipText = "Allows video clips to work in the Windows Photo Gallery";
                            A.Group = list.Groups[3];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-CodecPack-Basic-Encoder-Package"))
                        {
                            A.Text = "Windows Media Codec Pack Encoder";
                            A.ToolTipText = "Windows Media Codec Pack Encoder";
                            A.Group = list.Groups[3];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-CodecPack-Basic-Package"))
                        {
                            A.Text = "Windows Media Codec Pack";
                            A.ToolTipText = "Windows Media Codec Pack";
                            A.Group = list.Groups[3];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Common-Drivers-Package"))
                        {
                            A.Text = "N/A";
                            A.ToolTipText = "Keyboard, USB, Display, System, HDD";
                            A.Group = list.Groups[1];
                            A.BackColor = Color.Red;
                        }
                        ///'''''''''''''''''''''''''''
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Common-Modem-Drivers"))
                        {
                            A.Text = "Common Modem Drivers + Support";
                            A.ToolTipText =
                                 "Modem Drivers, I have received reports that if you remove this you can't install modems or mobile phones.";
                            A.Group = list.Groups[1];
                            A.BackColor = Color.LightPink;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-DesktopWindowManager-uDWM-Package"))
                        {
                            A.Text = "Desktop Window Manager";
                            A.ToolTipText = "Install the Desktop Window Manager. WARNING: Breaks Aero GUI.";
                            A.BackColor = Color.LightPink;
                            A.Group = list.Groups[8];
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Disk-Diagnosis-Package"))
                        {
                            A.Text = "N/A";
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Editions-Client-Package"))
                        {
                            A.Text = "N/A";
                            A.BackColor = Color.LightPink;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Foundation-Package"))
                        {
                            A.Text = "N/A";
                            A.ToolTipText = "Delivers Windows Foundation Deployment";
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Gadget-Platform-Package"))
                        {
                            A.Text = "Windows Gadget Platform";
                            A.ToolTipText = "Allows you to display gadgets on your desktop.";
                            A.Group = list.Groups[0];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-GPUPipeline-Package"))
                        {
                            A.Text = "N/A";
                            A.BackColor = Color.Yellow;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-GroupPolicy-ClientExtensions-Package"))
                        {
                            A.Text = "Group Policy Client Extensions";
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-GroupPolicy-ClientTools-Package"))
                        {
                            A.Text = "Group Policy Client Tools";
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Help-CoreClient") && Line.ContainsIgnoreCase("-Package"))
                        {
                            A.Text = "Core Client Help";
                            A.ToolTipText = "Core User Assistance";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Help-Customization-Package"))
                        {
                            A.Text = "OEMHelpCustomization";
                            A.ToolTipText = "Help content customization for OEM";
                            A.Group = list.Groups[10];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-ICM-Package"))
                        {
                            A.Text = "Windows Color System"; //missing \\system32\\mscms.dll
                            A.Text = "N/A";
                            A.ToolTipText = "Windows Color System Core";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.Yellow;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-IE-Troubleshooters-Package"))
                        {
                            A.Text = "Internet Explorer Troubleshooters";
                            A.ToolTipText = "Internet Explorer Troubleshooters";
                            A.Group = list.Groups[10];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-IIS-WebServer-AddOn-Package"))
                        {
                            A.Text = "IIS Addons 1";
                            A.ToolTipText =
                                 "Static Content, Default Document, Directory Browsing, WebDav Publishing, ASP .NET,IIS-ASP, etc.. ";
                            A.Group = list.Groups[5];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-IIS-WebServer-AddOn-2-Package"))
                        {
                            A.Text = "IIS Addons 2";
                            A.ToolTipText =
                                 "Windows Authentication, Digest Authentication, ODBC Logging, WAS Process Model, etc... ";
                            A.Group = list.Groups[5];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-IIS-WebServer-Package"))
                        {
                            A.Text = "Internet Information Services";
                            A.ToolTipText =
                                 "Internet Information Services provides support for Web and FTP servers, along with support for ASP.NET web sites, dynamic content such as Classic ASP and CGI, and local and remote management.";
                            A.Group = list.Groups[5];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Indexing-Service-Package"))
                        {
                            A.Text = "Indexing Service";
                            A.ToolTipText = "Turns the indexing service on or off.";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }
                        //
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-InternetExplorer-Optional-Package") || Line.ContainsIgnoreCase("Microsoft-Windows-InternetExplorer-VistaPlus"))
                        {
                            A.Text = "Internet Explorer";
                            A.ToolTipText = "Finds and displays information and Web sites on the Internet.";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-InternetExplorer-Package"))
                        {
                            A.Text = "HTML Engine";
                            A.ToolTipText = "Windows Internet Platform Components. WARNING: Breaks IETab.";
                            A.SubItems[3].Text = ";_ie";
                            A.BackColor = Color.LightPink;
                            A.Group = list.Groups[8];
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Killbits-Package"))
                        {
                            A.Text = "N/A";
                            A.ToolTipText = "Windows Killbits";
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Links-Package"))
                        {
                            A.Text = "N/A";
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-LocalPack-AU-Package"))
                        {
                            A.Text = "Australia Local Pack";
                            A.ToolTipText = "Australia Theme";
                            A.Group = list.Groups[0];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-LocalPack-CA-Package"))
                        {
                            A.Text = "Canada Local Pack";
                            A.ToolTipText = "Canada Theme";
                            A.Group = list.Groups[0];
                            A.BackColor = Color.LightGreen;
                        }
                        ///Errr
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-LocalPack-GB-Package"))
                        {
                            A.Text = "Great Britain Local Pack";
                            A.ToolTipText = "Great Britain Theme";
                            A.Group = list.Groups[0];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-LocalPack-US-Package"))
                        {
                            A.Text = "United States Local Pack";
                            A.ToolTipText = "United States Theme";
                            A.Group = list.Groups[0];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-LocalPack-ZA-Package"))
                        {
                            A.Text = "New Zealand Local Pack";
                            A.ToolTipText = "New Zealand Theme";
                            A.Group = list.Groups[0];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Media-Format-Package"))
                        {
                            A.Text = "Windows Media Format Runtime";
                            A.ToolTipText = "Windows Media Format Runtime";
                            A.Group = list.Groups[3];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-MediaCenter-Package"))
                        {
                            A.Text = "Windows Media Center";
                            A.ToolTipText = "Windows Media Center";
                            A.SubItems[3].Text = ";mediacenter";
                            A.Group = list.Groups[3];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-MediaPlayback-OC-Package"))
                        {
                            A.Text = "Media Features";
                            A.ToolTipText =
                                 "Controls media features, such as Windows Media Player, Windows Media Center, and Windows DVD Maker.";
                            A.Group = list.Groups[3];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-MediaPlayer-DVDRegistration-Package"))
                        {
                            A.Text = "Windows Media Player DVD Registration";
                            A.ToolTipText = "Windows Media Player DVD Registration";
                            A.Group = list.Groups[3];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-MediaPlayer-Package"))
                        {
                            A.Text = "Windows Media Player";
                            A.ToolTipText = "Windows Media Player [Breaks Windows 8 WinSat if removed]";
                            A.Group = list.Groups[3];
                            A.BackColor = Color.LightPink;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-MobilePC-Client-") && Line.ContainsIgnoreCase("-Package") &&
                             !Line.ContainsIgnoreCase("Sensors") && !Line.ContainsIgnoreCase("SideShow"))
                        {
                            A.Text = "Mobile PC Client Components";
                            A.ToolTipText = "Mobile PC ClientComponents";
                            A.Group = list.Groups[6];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-MobilePC-Client-Sensors-Package"))
                        {
                            A.Text = "Mobile PC Sensors ALS Driver";
                            A.ToolTipText = "Mobile PC Sensors ALS Driver";
                            A.Group = list.Groups[6];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-MobilePC-Client-SideShow-Package"))
                        {
                            A.Text = "Mobile PC Client SideShow Components";
                            A.ToolTipText = "Mobile PC Client SideShow Components";
                            A.Group = list.Groups[6];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-MSMQ-Client-Package~31bf3856ad364e35"))
                        {
                            A.Text = "Microsoft Message Queue (MSMQ) Server";
                            A.ToolTipText = "Microsoft Message Queue (MSMQ) Server";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-NetFx3-OC-Package"))
                        {
                            A.Text = "Microsoft .NET Framework 3.5.1";
                            A.ToolTipText = "Microsoft .NET Framework 3.5.1 WARNING: Can affect Visual Studio 2010.";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-NetworkDiagnostics-DirectAccessEntry-Package"))
                        {
                            A.Text = "Network Diagnostics";
                            A.ToolTipText = "DirectAccess Diagnostics Entry Point";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-NFS-ClientSKU-Package"))
                        {
                            A.Text = "Network File System (NFS) protocol";
                            A.ToolTipText =
                                 "Enables this computer to gain access to files on UNIX-based computers, Tools for managing Services for NFS on local and remote computers and Allows you to access files using the Network File System (NFS) protocol.";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-OfflineFiles") && Line.ContainsIgnoreCase("-Package"))
                        {
                            A.Text = "Offline Files";
                            A.ToolTipText = "Network file caching and offline access";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-OpticalMediaDisc-Package"))
                        {
                            A.Text = "DVD Maker";
                            A.ToolTipText = "Video DVD creation wizard";
                            A.Group = list.Groups[3];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-ParentalControls-Package"))
                        {
                            A.Text = "Parental Controls";
                            A.ToolTipText = "Parental Controls. Reported that removing this can prevent certain games from working.";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightPink;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-PeerDist-Client-Package"))
                        {
                            A.Text = "BranchCache";
                            A.ToolTipText =
                                 "Decrease the time branch office users spend waiting to download files across the network.";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-PeerToPeer-Full-Package"))
                        {
                            A.Text = "Peer networking Infrastructure";
                            A.ToolTipText = "Peer networking Infrastructure Feature";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Personalization-Package"))
                        {
                            A.Text = "Personalization Control Panel";
                            A.ToolTipText = "Lets you change wallpapers, screen savers, etc..";
                            A.BackColor = Color.LightPink;
                            A.Group = list.Groups[8];
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Photo") && Line.ContainsIgnoreCase("Package"))
                        {
                            A.Text = "Photo Experience";
                            A.ToolTipText = "Photo Experience. WARNING: Breaks Windows Picture Viewer.";
                            A.BackColor = Color.LightPink;
                            A.Group = list.Groups[3];
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Printer-Drivers-Package"))
                        {
                            A.Text = "Printer Drivers + Support";
                            A.ToolTipText =
                                 "Common Printer Drivers, I have received reports that if you remove this you can't install printers.";
                            A.Group = list.Groups[1];
                            A.BackColor = Color.LightPink;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Printing-Foundation-Package"))
                        {
                            A.Text = "Print and Document Services";
                            A.ToolTipText = "Enable print, fax, and scan tasks on this computer";
                            A.Group = list.Groups[7];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Printing-LocalPrinting-Home-Package"))
                        {
                            A.Text = "Printing LocalPrinting Enterprise";
                            A.ToolTipText = "Printing LocalPrinting Enterprise";
                            A.Group = list.Groups[7];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Printing-LocalPrinting-Enterprise-Package"))
                        {
                            A.Text = "Printing LocalPrinting Home";
                            A.ToolTipText = "Local Home Printing";
                            A.Group = list.Groups[7];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Printing-PremiumTools-Package"))
                        {
                            A.Text = "Printing Premium Tools Collection";
                            A.ToolTipText = "Printing Premium Tools Collection";
                            A.Group = list.Groups[7];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Printing-XPSServices-Package"))
                        {
                            A.Text = "Microsoft XPS Document Writer";
                            A.ToolTipText =
                                 "Provides binaries on the system for creating the XPS Document Writer Print Queue";
                            A.Group = list.Groups[7];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-RasRip-Package"))
                        {
                            A.Text = "RIP Listener";
                            A.ToolTipText =
                                 "Listens for route updates sent by routers that use the Routing Information Protocol version 1 (RIPv1)";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-RDC-Package"))
                        {
                            A.Text = "Remote Differential Compression";
                            A.ToolTipText =
                                 "Installs Remote Differential Compression (RDC) support for use in third-party applications.";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Windows-Winhelp-Update-Client"))
                        {
                            A.Text = "Microsoft Winhelp Update Client";
                            A.ToolTipText =
                                 "Update for Microsoft Winhelp Update Client (KB917607)";
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-RecDisc-SDP-Package"))
                        {
                            A.Text = "Windows Recovery Disc";
                            A.ToolTipText =
                                 "Application to create recovery discs to boot into Windows Recovery Environment, from where image backups can be restored";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-RemoteAssistance-Package-Client"))
                        {
                            A.Text = "Remote Assistance";
                            A.ToolTipText =
                                 "Lets you invite a friend to connect to your computer and help you with problems";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-SampleContent-Music-Package"))
                        {
                            A.Text = "Music and Video Examples";
                            A.ToolTipText = "Music and Video Examples";
                            A.Group = list.Groups[3];
                            A.BackColor = Color.LightPink;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-SampleContent-Ringtones-Package"))
                        {
                            A.Text = "Music and Video Examples (Ringtones)";
                            A.ToolTipText = "Music and Video Examples (Ringtones)";
                            A.Group = list.Groups[3];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-SearchEngine-Client-Package"))
                        {
                            A.Text = "Windows Search";
                            A.ToolTipText =
                                 "Provides content indexing, property caching, and search results for files, e-mail, and other content.";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-SecureStartup") && Line.ContainsIgnoreCase("-Package"))
                        {
                            A.Text = "BitLocker Drive Encryption";
                            A.ToolTipText = "Provides full volume disk encryption for boot volumes. BitLocker is required for the 'Windows to Go'.";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-RasCMAK-Package"))
                        {
                            A.Text = "RAS Connection Manager Administration Kit (CMAK)";
                            A.ToolTipText =
                                 "Create profiles for connecting to remote servers and networks on computers Windows 7";
                            A.BackColor = Color.LightGreen;
                            A.Group = list.Groups[4];
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Security-SPP-Component-SKU-") && Line.ContainsIgnoreCase("-Package"))
                        {
                            A.Text = "N/A";
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-ServicingBaseline-") && Line.ContainsIgnoreCase("-Package"))
                        {
                            A.Text = "N/A";
                            //Programs && Features
                            A.ToolTipText = "ServicingBaseline. DO NOT REMOVE FOR ULTIMATE!!!";
                            A.BackColor = Color.LightPink;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-ShareMedia-ControlPanel-Package"))
                        {
                            A.Text = "Share Media Control Panel";
                            A.ToolTipText = "Share Media Control Panel";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Shell-HomeGroup-Package"))
                        {
                            A.Text = "HomeGroup";
                            A.ToolTipText = "HomeGroup";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Shell-InboxGames-Package"))
                        {
                            A.Text = "Inbox Games";
                            A.ToolTipText = "Standard Inbox Games";
                            A.SubItems[3].Text = ";inboxgames";
                            A.Group = list.Groups[0];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Shell-MultiplayerInboxGames-Package"))
                        {
                            A.Text = "Internet Games";
                            A.ToolTipText = "Internet Games";
                            A.Group = list.Groups[0];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Shell-PremiumInboxGames-Package"))
                        {
                            A.Text = "Premium Inbox Games";
                            A.ToolTipText = "Chess Titans, Mahjong Titans";
                            A.Group = list.Groups[0];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Shell-SoundThemes-Package"))
                        {
                            A.Text = "Sound Themes";
                            A.ToolTipText = "Windows Sound Themes";
                            A.Group = list.Groups[3];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Sidebar-Killbits-SDP-Package"))
                        {
                            A.Text = "Windows Gadget Platform";
                            A.ToolTipText =
                                 "Component to install and maintain Windows Sidebar Killbit list (banned gadgets with security holes)";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-SimpleTCP-Package"))
                        {
                            A.Text = "SimpleTCP Services";
                            A.ToolTipText = "Simple TCPIP services (i.e. echo, daytime etc)";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-SnippingTool-Package"))
                        {
                            A.Text = "Snipping Tools";
                            A.ToolTipText = "The Windows Snipping Tool Package.";
                            A.Group = list.Groups[0];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-SNMP-Package"))
                        {
                            A.Text = "Simple Network Management Protocol (SNMP)";
                            A.ToolTipText =
                                 "This feature includes Simple Network Management Protocol agents that monitor the activity in network devices and report to the network console workstation";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-StickyNotes-Package"))
                        {
                            A.Text = "Sticky Notes";
                            A.ToolTipText = "The Windows Sticky Notes Package";
                            A.Group = list.Groups[0];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-SUA-Package"))
                        {
                            A.Text = "Subsystem for UNIX-based Applications";
                            A.ToolTipText =
                                 "Subsystem for UNIX-based Applications (SUA) is a source-compatibility subsystem for compiling and running custom UNIX-based applications and scripts on a computer running Windows operating system.";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-SystemRestore-Package"))
                        {
                            A.Text = "System Restore";
                            A.ToolTipText =
                                 "Allows you to recover from system problems by providing rollback to a restore point maintained via Shared Protection Points";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-TabletPC-OC-Package"))
                        {
                            A.Text = "Tablet PC Components";
                            A.ToolTipText =
                                 "Adds or removes Tablet PC Input Panel, Windows Journal, Math Input Panel, and other handwriting recognition features.";
                            A.Group = list.Groups[0];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Telnet-Client-Package"))
                        {
                            A.Text = "Telnet Client";
                            A.ToolTipText = "Connect to remote computers by using the Telnet protocol";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Telnet-Server-Package"))
                        {
                            A.Text = "Telnet Server";
                            A.ToolTipText = "Allow others to connect to your computer by using the Telnet protocol";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-TerminalServices-CommandLineTools-Package"))
                        {
                            A.Text = "Remote Desktop Services Command Line Tools";
                            A.ToolTipText = "Remote Desktop Services Command Line Tools";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-TerminalServices-MiscRedirection-Package"))
                        {
                            A.Text = "Terminal Services Miscellaneous Redirection";
                            A.ToolTipText = "Terminal Services Miscellaneous Redirection";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-TerminalServices-Publishing-WMIProvider-Package"))
                        {
                            A.Text = "Remote Desktop Services Publishing WMI Provider";
                            A.ToolTipText = "Remote Desktop Services Publishing WMI Provider";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-TerminalServices-RemoteApplications-Client-Package"))
                        {
                            A.Text = "Terminal Services Remote Applications Client";
                            A.ToolTipText = "Terminal Services Remote Applications Client";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-VirtualPC-Package") || Line.ContainsIgnoreCase("Microsoft-Windows-vpc-Package-SP1-Restore"))
                        {
                            A.Text = "Windows Virtual PC (KB958559)";
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Media-Format-OOB-Package"))
                        {
                            A.Text = "Windows Media Format Feature for Windows 7N (KB968212)";
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-MediaFeaturePack-TopLevel-OOB-Package"))
                        {
                            A.Text = "Media Feature Pack for Windows 7N (KB968211)";
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-wmf-Package"))
                        {
                            A.Text = "Windows Management Framework";
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-RemoteServerAdministrationTools-Package"))
                        {
                            A.Text = "Remote Server Administration Tools";
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-NTBackup-Package"))
                        {
                            A.Text = "Windows Backup Utility";
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-TerminalServices-UsbRedirector-Package"))
                        {
                            A.Text = "Terminal Services Usb Redirector";
                            A.ToolTipText = "Terminal Services Usb Redirector";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-TerminalServices-WMIProvider-Package"))
                        {
                            A.Text = "Remote Desktop Services WMI Provider";
                            A.ToolTipText = "Remote Desktop Services WMI Provider";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-TFTP-Client-Package"))
                        {
                            A.Text = "TFTP Client";
                            A.ToolTipText = "Transfer files using the Trivial File Transfer Protocol";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Tuner-Drivers-Package"))
                        {
                            A.Text = "TV Tuner Drivers";
                            A.ToolTipText = "TV Tuner Drivers";
                            A.Group = list.Groups[1];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-VirtualPC-Licensing-Package"))
                        {
                            A.Text = "VirtualPC Licensing Policies";
                            A.ToolTipText = "Licensing policies for VirtualPC";
                            A.Group = list.Groups[9];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-VirtualPC-USB-RPM-Package"))
                        {
                            A.Text = "VirtualPC (USB)";
                            A.ToolTipText = "VirtualPC USB RPM Package";
                            A.Group = list.Groups[9];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-VirtualXP-Licensing-Package"))
                        {
                            A.Text = "Virtual XP Licensing Policies";
                            A.ToolTipText = "Licensing policies for Virtual XP";
                            A.Group = list.Groups[9];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-WindowsMediaPlayer-Troubleshooters-Package"))
                        {
                            A.Text = "Windows Media Player Troubleshooters";
                            A.ToolTipText = "Windows Media Player Troubleshooters";
                            A.Group = list.Groups[10];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-WinOcr-Package"))
                        {
                            A.Text = "Windows TIFF IFilter";
                            A.ToolTipText =
                                 "Enables the indexing and searching of Tagged Image File Format (TIFF) files using Optical Character Recognition (OCR).";
                            A.SubItems[3].Text = ";ocr";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-WMI-SNMP-Provider-Package"))
                        {
                            A.Text = "WMI SNMP Provider";
                            A.ToolTipText =
                                 "The SNMP WMI Provider enables WMI clients to  consume SNMP information through the CIM model as implemented by WMI";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-WMPNetworkSharingService-Package"))
                        {
                            A.Text = "Windows Media Player Network Sharing Service";
                            A.ToolTipText = "Windows Media Player Network Sharing Service";
                            A.Group = list.Groups[4];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Xps-Foundation-Client-Package"))
                        {
                            A.Text = "XPS Viewer";
                            A.ToolTipText =
                                 "Allows you to read, copy, print, sign, and set permissions for XPS documents";
                            A.Group = list.Groups[7];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Networking-MPSSVC-Rules-") && Line.ContainsIgnoreCase("-Package"))
                        {
                            A.Text = "N/A";
                        }

                        if (Line.ContainsIgnoreCase("Server-Help-Package"))
                        {
                            A.Text = "Windows User Assistance";
                            A.ToolTipText = "Microsoft Windows Help";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;

                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-") && Line.ContainsIgnoreCase("-wrapper"))
                        {
                            A.Text = "N/A";
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-ApplicationServerExtensions"))
                        {
                            if (Line.ContainsIgnoreCase("Microsoft-Windows-ApplicationServerExtensions-Package-TopLevel"))
                            {
                                A.Text = "AppFrabic";
                                A.ToolTipText =
                                     "Windows Server AppFabric is a set of integrated technologies that make it easier to build, scale and manage Web and composite applications that run on IIS.";
                                A.Group = list.Groups[5];
                            }
                            else
                            {
                                A.Text = "N/A";
                            }
                            A.BackColor = Color.LightGreen;
                        }
                        //
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Agent-Package-TopLevel"))
                        {
                            A.Text = "Microsoft Agent";
                            A.ToolTipText = "Microsoft Agent";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-DirectoryServices-ADAM"))
                        {
                            A.Text = "Active Directory Application Mode";
                            A.ToolTipText = "This installs Active Directory Application Mode (AD/AM)";
                            A.BackColor = Color.LightGreen;
                            A.Group = list.Groups[4];
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Fmapi-Package"))
                        {
                            A.Text = "Windows File Management API";
                            A.ToolTipText = "Windows File Management API";
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Identity-Foundation-Package-TopLevel"))
                        {
                            A.Text = "Windows Identity Foundation Runtime";
                            A.ToolTipText = "Windows Identity Foundation Runtime";
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-NtBackup-RestoreUtility-Package-TopLevel"))
                        {
                            A.Text = "Windows Restore Utility";
                            A.ToolTipText = "Windows restore utility used for restoring data.";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Security-WindowsActivationTechnologies"))
                        {
                            A.Text = "N/A";
                        }

                        if (Line.ContainsIgnoreCase("Windows7SP1-KB976933"))
                        {
                            A.Text = "N/A";
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Winhelp-Update-Client-TopLevel"))
                        {
                            A.Text = "Windows Help (WinHlp32.exe)";
                            A.ToolTipText = "Windows Help (WinHlp32.exe)";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Anytime-Upgrade-Package"))
                        {
                            A.Text = "Windows Anytime Upgrade";
                            A.ToolTipText = "Windows Anytime Upgrade";
                            A.BackColor = Color.LightGreen;
                        }

                        //
                        if (Line.ContainsIgnoreCase("Microsoft-Windows-StorageService-Package"))
                        {
                            A.Text = "Microsoft Storage Service";
                            A.ToolTipText = "Microsoft Storage Service";
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-RemoteFX-VM-Setup"))
                        {
                            A.Text = "RemoteFX Remote Server VM Components";
                            A.ToolTipText = "Windows Server RemoteFX VM Components";
                            A.BackColor = Color.LightGreen;
                            A.Group = list.Groups[4];
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-RemoteFX-Synth3dVsc-IC-Package"))
                        {
                            A.Text = "RemoteFX Remote Server Synthc3dVsc Components";
                            A.ToolTipText = "synth3dvsc.inf";
                            A.BackColor = Color.LightGreen;
                            A.Group = list.Groups[4];
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-RemoteFX-RemoteClient-Setup"))
                        {
                            A.Text = "RemoteFX Remote Client Components";
                            A.ToolTipText = "Windows Server RemoteFX Remote Client Components";
                            A.BackColor = Color.LightGreen;
                            A.Group = list.Groups[4];
                        }
                        if (Line.ContainsIgnoreCase("££"))
                        {
                            A.Text = "";
                            A.ToolTipText = "";
                            A.BackColor = Color.Yellow;
                            A.Group = list.Groups[4];
                        }

                        //Windows 8
                        if (Line.ContainsIgnoreCase("Windows-Defender") && Line.ContainsIgnoreCase("-Package"))
                        {
                            A.Text = "Windows Defender";
                            A.ToolTipText =
                                 "Windows Defender is a free program that helps you stay productive by protecting your computer against pop-ups, slow performance and security threats caused by spy ware and other potentially unwanted software.";
                            A.Group = list.Groups[8];
                            A.BackColor = Color.LightGreen;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-WebcamExperience-Package"))
                        {
                            A.Text = "WebCam Experience";
                            A.ToolTipText = "";
                            A.BackColor = Color.Yellow;

                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-Store-Client-Package"))
                        {
                            A.Text = "Windows Store";
                            A.ToolTipText = "";
                            A.BackColor = Color.Yellow;
                            A.Group = list.Groups[8];
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-WinMDE-Package"))
                        {
                            A.Text = "WinMDE-Package";
                            A.ToolTipText =
                                 "Wraps all components from base depot contributing to Microsoft-Windows-WinMDE-Package-avcore";
                            A.BackColor = Color.Yellow;
                        }

                        if (Line.ContainsIgnoreCase("Microsoft-Windows-TextPrediction-Package"))
                        {
                            A.Text = "Text Prediction";
                            A.ToolTipText = "Adds or removes the Text Prediction engine.";
                            A.BackColor = Color.Yellow;
                        }

                        if (Line.ContainsIgnoreCase("Redhawk-v1.0-package"))
                        {
                            A.Text = "Redhawk v1.0";
                            A.ToolTipText = "Pulls in Slr100.dll Redhawk runtime.";
                            A.BackColor = Color.Yellow;
                            A.Group = list.Groups[4];
                        }

                        //if (Line.Containts("")) 
                        //{
                        //	A.Text = A.Text.Substring(1);
                        //}

                        A.SubItems[2].Text = APackage;
                        if (string.IsNullOrEmpty(A.ToolTipText)) { A.ToolTipText = "No Description Available"; }

                        if (A.Text != "N/A")
                        {
                            if (list.FindItemWithText(APackage) == null)
                            {
                                bool CF = list.Items.Cast<ListViewItem>().Any(C => C.Text == A.Text);



                                if (CF == false)
                                {
                                    list.Items.Add(A);
                                }
                                else
                                {
                                    A.Group = null;
                                    list.FindItemWithText(A.Text).SubItems[2].Text = APackage + ";" + list.FindItemWithText(A.Text).SubItems[2].Text;
                                }
                            }
                        }
                        else
                        {
                            A.Group = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    cMain.FreeRAM();
                }

            }

            cMain.UpdateToolStripLabel(lblStatus, "Detecting extra descriptions");
            Application.DoEvents();
            iCount = 0; iTotal = 0;
            foreach (ListViewItem LST in list.Items)
            {
                string sPackage = LST.SubItems[2].Text;
                while (sPackage.ContainsIgnoreCase(";")) { sPackage = sPackage.Substring(0, sPackage.Length - 1); }

                string sPackageName = sPackage;
                while (sPackageName.ContainsIgnoreCase("~")) { sPackageName = sPackageName.Substring(0, sPackageName.Length - 1); }

                string pPath = image.MountPath + "\\Windows\\Servicing\\Packages\\" + sPackage + ".mum";
                if (File.Exists(pPath) && (LST.ToolTipText.EqualsIgnoreCase("No Description Available") || LST.Text == sPackageName))
                {
                    iTotal++;
                }
            }

            foreach (ListViewItem LST in list.Items)
            {
                string sPackage = LST.SubItems[2].Text;
                while (sPackage.ContainsIgnoreCase(";")) { sPackage = sPackage.Substring(0, sPackage.Length - 1); }

                string sPackageName = sPackage;
                while (sPackageName.ContainsIgnoreCase("~")) { sPackageName = sPackageName.Substring(0, sPackageName.Length - 1); }
                string pPath = pPath = image.MountPath + "\\Windows\\Servicing\\Packages\\" + sPackage + ".mum";
                if (File.Exists(pPath) && (LST.ToolTipText.EqualsIgnoreCase("No Description Available") || LST.Text == sPackage))
                {
                    iCount++;
                    if (iCount.ToString().EndsWithIgnoreCase("0") || iCount.ToString().EndsWithIgnoreCase("5"))
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Finding Description [" + iCount + "/" + iTotal + "] :: " + LST.Text);
                        Application.DoEvents();
                    }
                    string P = "";
                    using (var SR = new StreamReader(pPath))
                    {
                        string sP = SR.ReadToEnd();
                        bool bFound = false;
                        foreach (string sLine in sP.Split(Environment.NewLine.ToCharArray()))
                        {
                            if (sLine.ContainsIgnoreCase("DESCRIPTION=") || sLine.ContainsIgnoreCase("DISPLAYNAME="))
                            {
                                P += sLine;
                            }
                            if (P.ContainsIgnoreCase("DESCRIPTION=") && P.ContainsIgnoreCase("DISPLAYNAME=")) { break; }
                            if (P.ContainsIgnoreCase("DESCRIPTION=") && !sP.ContainsIgnoreCase("DISPLAYNAME=")) { break; }
                            if (P.ContainsIgnoreCase("DISPLAYNAME=") && !sP.ContainsIgnoreCase("DESCRIPTION=")) { break; }
                        }
                    }

                    if (P.ContainsIgnoreCase("DESCRIPTION") && LST.ToolTipText.EqualsIgnoreCase("No Description Available"))
                    {
                        string pDesc = P;
                        while (!pDesc.StartsWithIgnoreCase("DESCRIPTION="))
                        {
                            pDesc = pDesc.Substring(1);
                        }
                        pDesc = pDesc.Substring(13);
                        while (pDesc.ContainsIgnoreCase("\""))
                        {
                            pDesc = pDesc.Substring(0, pDesc.Length - 1);
                        }
                        LST.ToolTipText = pDesc;
                    }

                    if (P.ContainsIgnoreCase("DISPLAYNAME") && LST.Text == sPackageName)
                    {
                        string pName = P;
                        while (!pName.StartsWithIgnoreCase("DISPLAYNAME="))
                        {
                            pName = pName.Substring(1);
                        }
                        pName = pName.Substring(13);
                        while (pName.ContainsIgnoreCase("\""))
                        {
                            pName = pName.Substring(0, pName.Length - 1);
                        }
                        LST.Text = pName;
                    }
                }

                if (LST.Text.EqualsIgnoreCase("default") || string.IsNullOrEmpty(LST.Text))
                {
                    LST.Text = LST.SubItems[2].Text.Split('~')[0];
                }

            }

            cMain.UpdateToolStripLabel(lblStatus, "Detecting language files...");
            Application.DoEvents();
            try
            {
                foreach (
                     var Line in
                          inPut.Split(Environment.NewLine.ToCharArray()).Where(
                                Line => Line.ContainsIgnoreCase("Package_for_KB") && Line.ContainsIgnoreCase("~") && !Line.ContainsIgnoreCase("~~")))
                {
                    var A = new ListViewItem();
                    string AName = Line;
                    string APackage = Line;

                    A.Text = "N/A";
                    A.SubItems.Add("");
                    A.SubItems.Add("");
                    A.SubItems.Add("");
                    A.SubItems.Add("");
                    A.Group = list.Groups[2];
                    A.BackColor = Color.LightGreen;
                    while (APackage.ContainsIgnoreCase(" "))
                    {
                        APackage = APackage.Substring(0, APackage.Length - 1);
                    }
                    ///'''''''''''''''''

                    if (Line.ContainsIgnoreCase("AR-SA"))
                    {
                        A.Text = cMain.GetLPName("ar-SA");
                        A.ToolTipText = "Removes " + cMain.GetLPName("ar-SA") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("BG-BG"))
                    {
                        A.Text = cMain.GetLPName("bg-BG");
                        A.ToolTipText = "Removes " + cMain.GetLPName("bg-BG") + " language files";

                    }
                    if (Line.ContainsIgnoreCase("CS-CZ"))
                    {
                        A.Text = cMain.GetLPName("cs-CZ");
                        A.ToolTipText = "Removes " + cMain.GetLPName("cs-CZ") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("DA-DK"))
                    {
                        A.Text = cMain.GetLPName("DA-DK");
                        A.ToolTipText = "Removes " + cMain.GetLPName("DA-DK") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("DE-DE"))
                    {
                        A.Text = cMain.GetLPName("DE-DE");
                        A.ToolTipText = "Removes " + cMain.GetLPName("DE-DE") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("EL-GR"))
                    {
                        A.Text = cMain.GetLPName("el-gr");
                        A.ToolTipText = "Removes " + cMain.GetLPName("el-gr") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("ES-ES"))
                    {
                        A.Text = cMain.GetLPName("es-ES");
                        A.ToolTipText = "Removes " + cMain.GetLPName("es-es") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("ET-EE"))
                    {
                        A.Text = cMain.GetLPName("et-ee");
                        A.ToolTipText = "Removes " + cMain.GetLPName("et-ee") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("FI-FI"))
                    {
                        A.Text = cMain.GetLPName("fi-fi");
                        A.ToolTipText = "Removes " + cMain.GetLPName("fi-fi") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("FR-FR"))
                    {
                        A.Text = cMain.GetLPName("fr-fr");
                        A.ToolTipText = "Removes " + cMain.GetLPName("fr-fr") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("HE-IL"))
                    {
                        A.Text = cMain.GetLPName("he-il");
                        A.ToolTipText = "Removes " + cMain.GetLPName("he-il") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("HR-HR"))
                    {
                        A.Text = cMain.GetLPName("hr-hr");
                        A.ToolTipText = "Removes " + cMain.GetLPName("hr-hr") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("HU-HU"))
                    {
                        A.Text = cMain.GetLPName("hu-hu");
                        A.ToolTipText = "Removes " + cMain.GetLPName("hu-hu") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("IT-IT"))
                    {
                        A.Text = cMain.GetLPName("it-it");
                        A.ToolTipText = "Removes " + cMain.GetLPName("it-it") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("JA-JP"))
                    {
                        A.Text = cMain.GetLPName("ja-jp");
                        A.ToolTipText = "Removes " + cMain.GetLPName("ja-jp") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("KO-KR"))
                    {
                        A.Text = cMain.GetLPName("ko-kr");
                        A.ToolTipText = "Removes " + cMain.GetLPName("ko-kr") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("LT-LT"))
                    {
                        A.Text = cMain.GetLPName("lt-lt");
                        A.ToolTipText = "Removes " + cMain.GetLPName("lt-lt") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("LV-LV"))
                    {
                        A.Text = cMain.GetLPName("lv-lv");
                        A.ToolTipText = "Removes " + cMain.GetLPName("lv-lv") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("NB-NO"))
                    {
                        A.Text = cMain.GetLPName("nb-no");
                        A.ToolTipText = "Removes " + cMain.GetLPName("nb-no") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("NL-NL"))
                    {
                        A.Text = cMain.GetLPName("nl-nl");
                        A.ToolTipText = "Removes " + cMain.GetLPName("nl-nl") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("PL-PL"))
                    {
                        A.Text = cMain.GetLPName("pl-pl");
                        A.ToolTipText = "Removes " + cMain.GetLPName("pl-pl") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("PT-BR"))
                    {
                        A.Text = cMain.GetLPName("pt-br");
                        A.ToolTipText = "Removes " + cMain.GetLPName("pt-br") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("PT-PT"))
                    {
                        A.Text = cMain.GetLPName("pt-pt");
                        A.ToolTipText = "Removes " + cMain.GetLPName("pt-pt") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("RO-RO"))
                    {
                        A.Text = cMain.GetLPName("ro-ro");
                        A.ToolTipText = "Removes " + cMain.GetLPName("ro-ro") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("RU-RU"))
                    {
                        A.Text = cMain.GetLPName("ru-ru");
                        A.ToolTipText = "Removes " + cMain.GetLPName("ru-ru") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("SK-SK"))
                    {
                        A.Text = cMain.GetLPName("sk-sk");
                        A.ToolTipText = "Removes " + cMain.GetLPName("sk-sk") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("SL-SI"))
                    {
                        A.Text = cMain.GetLPName("sl-SI");
                        A.ToolTipText = "Removes " + cMain.GetLPName("sl-si") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("SR-LATN-CS"))
                    {
                        A.Text = cMain.GetLPName("SR-LATN-CS");
                        A.ToolTipText = "Removes " + cMain.GetLPName("SR-LATN-CS") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("SV-SE"))
                    {
                        A.Text = cMain.GetLPName("sv-se");
                        A.ToolTipText = "Removes " + cMain.GetLPName("sv-se") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("TH-TH"))
                    {
                        A.Text = cMain.GetLPName("th-th");
                        A.ToolTipText = "Removes " + cMain.GetLPName("th-th") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("TR-TR"))
                    {
                        A.Text = cMain.GetLPName("tr-tr");
                        A.ToolTipText = "Removes " + cMain.GetLPName("tr-tr") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("UK-UA"))
                    {
                        A.Text = cMain.GetLPName("uk-UA");
                        A.ToolTipText = "Removes " + cMain.GetLPName("uk-ua") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("ZH-CN"))
                    {
                        A.Text = cMain.GetLPName("zh-cn");
                        A.ToolTipText = "Removes " + cMain.GetLPName("zh-cn") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("ZH-HK"))
                    {
                        A.Text = cMain.GetLPName("zh-hk");
                        A.ToolTipText = "Removes " + cMain.GetLPName("zh-hk") + " language files";
                    }
                    if (Line.ContainsIgnoreCase("ZH-TW"))
                    {
                        A.Text = cMain.GetLPName("zh-tw");
                        A.ToolTipText = "Removes " + cMain.GetLPName("zh-tw") + " language files";
                    }

                    if (A.ToolTipText.EndsWithIgnoreCase(" language files"))
                    {
                        A.SubItems[1].Text = "Installed";
                    }

                    ///''''''''''''''''

                    A.SubItems[2].Text = APackage;
                    if (string.IsNullOrEmpty(A.ToolTipText))
                    {
                        A.ToolTipText = "No Description Available";
                    }


                    if (A.Text != "N/A")
                    {
                        bool CF = list.Items.Cast<ListViewItem>().Any(C => C.Text == A.Text);

                        if (CF == false)
                        {
                            list.Items.Add(A);
                        }
                        else
                        {
                            A.Group = null;
                            list.FindItemWithText(A.Text).SubItems[2].Text = APackage + ";" + list.FindItemWithText(A.Text).SubItems[2].Text;
                        }
                    }

                    cMain.FreeRAM();
                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(null, "Error detecting language files.", Ex.Message, inPut + Environment.NewLine + DVDLoc);
            }

            cMain.UpdateToolStripLabel(lblStatus, "Detecting DVD files...");
            Application.DoEvents();

            try
            {
                if (!string.IsNullOrEmpty(DVDLoc))
                {
                    if (Directory.Exists(DVDLoc + "Support"))
                    {
                        NIDVDSupport.Text = "Delete 'Support' folder";
                        NIDVDSupport.SubItems.Add(cMain.GetSize(DVDLoc + "Support", true));
                        NIDVDSupport.SubItems.Add("Deletes the Support folder on the DVD.");
                        NIDVDSupport.Group = list.Groups[13];
                        NIDVDSupport.Tag = "DVD";
                        NIDVDSupport.BackColor = Color.LightGreen;
                        list.Items.Add(NIDVDSupport);
                    }

                    if (Directory.Exists(DVDLoc + "Upgrade"))
                    {
                        NIDVDUpgrade.Text = "Delete 'Upgrade' folder";
                        NIDVDUpgrade.SubItems.Add(cMain.GetSize(DVDLoc + "Upgrade"));
                        NIDVDUpgrade.SubItems.Add("Deletes the Upgrade folder on the DVD.");
                        NIDVDUpgrade.Group = list.Groups[13];
                        NIDVDUpgrade.Tag = "DVD";
                        NIDVDUpgrade.BackColor = Color.LightGreen;
                        list.Items.Add(NIDVDUpgrade);
                    }

                }
            }
            catch (Exception ex)
            {
                cMain.WriteLog(null, "Error detecting DVD Support/Update folders", ex.Message, DVDLoc);
            }
            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Detecting Picture Samples...");
                Application.DoEvents();
                NIPictureSamples.Text = "Picture Samples";
                NIPictureSamples.SubItems.Add(
                cMain.GetSize(image.MountPath + "\\Users\\Public\\Pictures\\Sample Pictures", true));
                NIPictureSamples.SubItems.Add("Deletes Picture Samples");
                NIPictureSamples.Group = list.Groups[3];
                NIPictureSamples.BackColor = Color.LightGreen;
                NIPictureSamples.Tag = "WIM";
                if (Directory.Exists(image.MountPath + "\\Users\\Public\\Pictures\\Sample Pictures") ||
                     list.Name.EqualsIgnoreCase("lstComponents"))
                {
                    list.Items.Add(NIPictureSamples);
                }
            }
            catch (Exception ex)
            {
                cMain.WriteLog(null, "Error detecting picture samples", ex.Message, image.MountPath);
            }

            if (Directory.Exists(image.MountPath + "\\Windows\\Resources\\Ease of Access Themes"))
            {
                try
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Detecting Ease of Access Themes...");
                    Application.DoEvents();
                    NITEase.Text = "Themes: Ease of Access Themes";
                    NITEase.SubItems.Add(cMain.GetSize(image.MountPath + "\\Windows\\Resources\\Ease of Access Themes", true));
                    NITEase.SubItems.Add("Windows\\Resources\\Ease of Access Themes\\*.*");
                    NITEase.Group = list.Groups[0];
                    NITEase.BackColor = Color.LightGreen;
                    NITEase.Tag = "WIM";
                    if (Directory.Exists(image.MountPath + "\\Windows\\Resources\\Ease of Access Themes"))
                    {
                        list.Items.Add(NITEase);
                    }

                }
                catch (Exception ex)
                {
                    cMain.WriteLog(null, "Error detecting EoA Themes.", ex.Message, image.MountPath);
                }
            }

            if (Directory.Exists(image.MountPath + "\\Windows\\Resources\\Themes\\Aero\\"))
            {
                try
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Detecting Aero Theme...");
                    Application.DoEvents();
                    NITAero.Text = "Themes: Aero Theme";
                    NITAero.SubItems.Add(cMain.GetSize(image.MountPath + "\\Windows\\Resources\\Themes\\Aero\\"));
                    NITAero.SubItems.Add("Windows\\Resources\\Themes\\Aero.theme");
                    NITAero.Group = list.Groups[0];
                    NITAero.BackColor = Color.LightGreen;
                    NITAero.Tag = "WIM";
                    if (File.Exists(image.MountPath + "\\Windows\\Resources\\Themes\\Aero.theme") ||
                         list.Name.EqualsIgnoreCase("lstComponents"))
                    {
                        list.Items.Add(NITAero);
                    }
                }
                catch (Exception ex)
                {
                    cMain.WriteLog(null, "Error detecting Aero Themes.", ex.Message, image.MountPath);
                }
            }

            if (Directory.Exists(image.MountPath + "\\Windows\\Resources\\Themes"))
            {
                try
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Detecting Non-Aero Themes...");
                    Application.DoEvents();
                    NITNAero.Text = "Themes: Non-Aero Themes";
                    NITNAero.SubItems.Add(cMain.GetSize(image.MountPath + "\\Windows\\Resources\\Themes", true));
                    NITNAero.SubItems.Add("Windows\\Resources\\Themes\\*.*~Aero.theme");
                    NITNAero.Group = list.Groups[0];
                    NITNAero.BackColor = Color.LightGreen;
                    NITNAero.Tag = "WIM";
                    int themes =
                         Directory.GetFiles(image.MountPath + "\\Windows\\Resources\\Themes", "*.*", SearchOption.TopDirectoryOnly).Count();
                    if (themes > 1)
                    {
                        list.Items.Add(NITNAero);
                    }

                }
                catch (Exception ex)
                {
                    cMain.WriteLog(null, "Error detecting non-Aero themes.", ex.Message, image.MountPath);
                }
            }
            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Detecting WinSXS Backup...");
                Application.DoEvents();

                NIWinSXSBackup.Text = "Delete WinSXS\\Backup";
                NIWinSXSBackup.SubItems.Add(cMain.GetSize(image.MountPath + "\\Windows\\WinSXS\\Backup"));
                NIWinSXSBackup.SubItems.Add("Deletes WinSXS backup folder, you will not be able to uninstall updates.");
                NIWinSXSBackup.Group = list.Groups[8];
                NIWinSXSBackup.Tag = "WinSXSBackup";
                NIWinSXSBackup.BackColor = Color.LightGreen;

                if (Directory.Exists(image.MountPath + "\\Windows\\WinSXS\\Backup") || list.Name.EqualsIgnoreCase("lstComponents"))
                {
                    list.Items.Add(NIWinSXSBackup);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                foreach (ListViewItem I in from ListViewItem I in list.Items where (list.FindItemWithText(I.Text + "-TopLevel") != null) select I)
                {
                    I.Remove();
                }
            }
            catch (Exception ex)
            {
                cMain.WriteLog(null, "Error removing top-level package.", ex.Message, image.MountPath);
            }

            list.Columns[0].Text = "Name (" + list.Items.Count + ")";
            list.Visible = true;
            cMain.UpdateToolStripLabel(lblStatus, "Done...");
            cMain.FreeRAM();
        }

        public void RemoveSoundSchemes(bool LoadReg = false)
        {
            if (LoadReg) { cReg.RegLoad("WIM_Default", image.MountPath + "\\Users\\Default\\NTUSER.DAT"); }
            try
            {
                var oRegKeyM = Registry.LocalMachine.OpenSubKey("WIM_Default\\AppEvents\\Schemes\\Names\\", RegistryKeyPermissionCheck.ReadWriteSubTree);
                if (oRegKeyM != null)
                {
                    foreach (string R in oRegKeyM.GetSubKeyNames().Where(R => R != ".None"))
                    {
                        try
                        {
                            oRegKeyM.DeleteSubKey(R);
                        }
                        catch (Exception Ex)
                        {
                            cMain.WriteLog(null, "Error removing registry key (sound)", Ex.Message, R);

                        }
                    }
                }

                if (oRegKeyM != null) { oRegKeyM.Close(); }
                cReg.WriteValue(Registry.LocalMachine, "WIM_Default\\AppEvents\\Schemes\\", "", ".None");

                RegistryKey oRegKey2 = null;

                try
                {
                    oRegKey2 = Registry.LocalMachine.OpenSubKey("WIM_Default\\AppEvents\\Schemes\\Apps\\.Default", RegistryKeyPermissionCheck.ReadWriteSubTree);
                    if (oRegKey2 != null)
                    {
                        foreach (string R in oRegKey2.GetSubKeyNames())
                        {
                            RegistryKey oRegKey3 = null;
                            try
                            {
                                oRegKey3 = Registry.LocalMachine.OpenSubKey("WIM_Default\\AppEvents\\Schemes\\Apps\\.Default\\" + R, RegistryKeyPermissionCheck.ReadWriteSubTree);
                                if (oRegKey3 != null)
                                {
                                    foreach (string R2 in oRegKey3.GetSubKeyNames())
                                    {
                                        try
                                        {
                                            if (R2.EqualsIgnoreCase(".Default"))
                                            {
                                                cReg.WriteValue(Registry.LocalMachine,
                                                                  "WIM_Default\\AppEvents\\Schemes\\Apps\\.Default\\" + R +
                                                                  "\\" +
                                                                  R2, "", "");
                                            }
                                            else
                                            {
                                                oRegKey3.DeleteSubKey(R2);
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                            if (oRegKey3 != null)
                            {
                                oRegKey3.Close();
                            }
                            cMain.FreeRAM();
                        }
                    }
                }
                catch
                {
                }

                if (oRegKey2 != null) { oRegKey2.Close(); }
            }
            catch { }
            if (LoadReg) { cReg.RegUnLoadAll(); }
        }

        private static void RemoveFile(string Component, string File, ToolStripLabel L)
        {
            string S = File;
            while (S.ContainsIgnoreCase("\\"))
                S = S.Substring(1);

            L.Text = "Removing [" + Component + " - " + S + "]...";
            Application.DoEvents();
            Files.DeleteFile(File);

        }

        private static void RemoveFolder(string Component, string Folder, ToolStripLabel L)
        {
            string S = Folder;
            while (S.ContainsIgnoreCase("\\"))
                S = S.Substring(1);

            L.Text = "Removing [" + Component + " - " + S + "]...";
            Application.DoEvents();
            Files.DeleteFolder(Folder, false);

        }

        public void RemovePNF(string MountPath, bool RegMounted, ToolStripLabel TL)
        {
            if (Directory.Exists(MountPath + "\\Windows\\Inf"))
            {
                foreach (string PNF in Directory.GetFiles(MountPath + "\\Windows\\Inf", "*.PNF", SearchOption.TopDirectoryOnly))
                {
                    cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL);
                }
            }
            if (Directory.Exists(MountPath + "\\Windows\\System32\\DriverStore\\FileRepository"))
            {
                foreach (string PNF in Directory.GetFiles(MountPath + "\\Windows\\System32\\DriverStore\\FileRepository", "*.PNF", SearchOption.AllDirectories))
                {
                    cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL);
                }
            }
        }

        public void Remove_IIS(string MountPath, bool RegMounted, ToolStripLabel TL)
        {
            RemovePNF(MountPath, RegMounted, TL);
        }

        public void Remove_SpeechLang(string MountPath, bool RegMounted, ToolStripLabel TL)
        {
            try
            {
                RemoveFile("Speech and Natural Language", MountPath + "\\ProgramData\\Microsoft\\Windows\\Start Menu\\Programs\\Accessories\\Accessibility\\Speech Recognition.lnk", TL);
                RemoveFile("Speech and Natural Language", MountPath + "\\Users\\All Users\\Microsoft\\Windows\\Start Menu\\Programs\\Accessories\\Accessibility\\Speech Recognition.lnk", TL);
                RemoveFile("Speech and Natural Language", MountPath + "\\Windows\\sysWOW64\\wdi\\perftrack\\Spux.ptxml", TL);
                RemoveFile("Speech and Natural Language", MountPath + "\\Windows\\System32\\wdi\\perftrack\\Spux.ptxml", TL);
                RemoveFile("Speech and Natural Language", MountPath + "\\Windows\\sysWOW64\\migration\\SCGMigPlugin.dll", TL);
                RemoveFile("Speech and Natural Language", MountPath + "\\Windows\\System32\\migration\\SCGMigPlugin.dll", TL);
                RemoveFile("Speech and Natural Language", MountPath + "\\Windows\\Help\\Windows\\en-us\\speech.h1s", TL);
                RemoveFile("Speech and Natural Language", MountPath + "\\Windows\\IME\\SPTIP.DLL", TL);
                RemoveFile("Speech and Natural Language", MountPath + "\\Windows\\IME\\en-US\\SpTip.dll.mui", TL);

                RemoveFolder("Speech and Natural Language", MountPath + "\\Program Files\\Common Files\\SpeechEngines", TL);
                RemoveFolder("Speech and Natural Language", MountPath + "\\Program Files (x86)\\Common Files\\SpeechEngines", TL);
                RemoveFolder("Speech and Natural Language", MountPath + "\\Windows\\rescache\\rc0000", TL);
                RemoveFolder("Speech and Natural Language", MountPath + "\\Windows\\rescache\\rc0001", TL);
                RemoveFolder("Speech and Natural Language", MountPath + "\\Windows\\Speech", TL);
                RemoveFolder("Speech and Natural Language", MountPath + "\\Windows\\System32\\Speech", TL);
                RemoveFolder("Speech and Natural Language", MountPath + "\\Windows\\sysWOW64\\Speech", TL);

                RemovePNF(MountPath, RegMounted, TL);
                if (Directory.Exists(MountPath + "\\Windows\\Media"))
                {
                    foreach (string PNF in Directory.GetFiles(MountPath + "\\Windows\\Media", "*.*", SearchOption.TopDirectoryOnly))
                    {
                        if (PNF.ContainsIgnoreCase("SPEECH")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                    }
                }
                if (Directory.Exists(MountPath + "\\Windows\\System32"))
                {
                    foreach (string PNF in Directory.GetFiles(MountPath + "\\Windows\\System32", "*.*", SearchOption.TopDirectoryOnly))
                    {
                        if (PNF.StartsWithIgnoreCase("NLSDATA")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.StartsWithIgnoreCase("NLSLEXICONS")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.StartsWithIgnoreCase("NLSMODELS")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                    }
                }
                if (Directory.Exists(MountPath + "\\Windows\\SysWOW64"))
                {
                    foreach (string PNF in Directory.GetFiles(MountPath + "\\Windows\\SysWOW64", "*.*", SearchOption.TopDirectoryOnly))
                    {
                        if (PNF.StartsWithIgnoreCase("NLSDATA")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.StartsWithIgnoreCase("NLSLEXICONS")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.StartsWithIgnoreCase("NLSMODELS")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                    }
                }

                if (Directory.Exists(MountPath + "\\Windows\\WinSXS\\FileMaps"))
                {
                    foreach (string PNF in Directory.GetFiles(MountPath + "\\Windows\\WinSXS\\FileMaps", "*.*", SearchOption.TopDirectoryOnly))
                    {
                        if (PNF.ContainsIgnoreCase("SPEECH")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("NATURALLANGUAGE")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("S..RECOGNIZE")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("S..DLANGUAGE")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("VOICEFRONTEND")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("VOICECOMMON")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("S..XPERIENCE")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                    }
                }

                if (Directory.Exists(MountPath + "\\Windows\\WinSXS\\Manifests"))
                {
                    foreach (string PNF in Directory.GetFiles(MountPath + "\\Windows\\WinSXS\\Manifests", "*.*", SearchOption.TopDirectoryOnly))
                    {
                        if (PNF.ContainsIgnoreCase("SPEECH")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("NATURALLANGUAGE")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("S..RECOGNIZE")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("S..DLANGUAGE")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("VOICEFRONTEND")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("VOICECOMMON")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("S..XPERIENCE")) { cMain.TakeOwnership(PNF); RemoveFile("Speech and Natural Language", PNF, TL); }
                    }
                }

                if (Directory.Exists(MountPath + "\\Windows\\WinSXS"))
                {
                    foreach (string PNF in Directory.GetDirectories(MountPath + "\\Windows\\WinSXS", "*.*", SearchOption.TopDirectoryOnly))
                    {
                        if (PNF.ContainsIgnoreCase("SPEECH")) { cMain.TakeOwnership(PNF); RemoveFolder("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("NATURALLANGUAGE")) { cMain.TakeOwnership(PNF); RemoveFolder("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("S..RECOGNIZE")) { cMain.TakeOwnership(PNF); RemoveFolder("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("S..DLANGUAGE")) { cMain.TakeOwnership(PNF); RemoveFolder("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("VOICEFRONTEND")) { cMain.TakeOwnership(PNF); RemoveFolder("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("VOICECOMMON")) { cMain.TakeOwnership(PNF); RemoveFolder("Speech and Natural Language", PNF, TL); }
                        if (PNF.ContainsIgnoreCase("S..XPERIENCE")) { cMain.TakeOwnership(PNF); RemoveFolder("Speech and Natural Language", PNF, TL); }
                    }
                }

            }
            catch (Exception Ex) { MessageBox.Show(Ex.Message); }

            try
            {
                if (RegMounted == false)
                {
                    string SHiveL = MountPath + "\\Windows\\System32\\Config\\";
                    cReg.RegLoad("WIM_Software", SHiveL + "SOFTWARE");
                    cReg.RegLoad("WIM_Admin", MountPath + "\\Users\\Administrator\\NTUSER.DAT");
                    cReg.RegLoad("WIM_Default", image.MountPath + "\\Users\\Default\\NTUSER.DAT");
                    cReg.RegLoad("WIM_SAM", SHiveL + "SAM");
                    cReg.RegLoad("WIM_System", SHiveL + "SYSTEM");
                    cReg.RegLoad("WIM_Components", SHiveL + "COMPONENTS");
                }

                var SW = new StreamWriter(MountPath + "\\Speech.reg");
                SW.Write(Properties.Resources.Speech_NaturalLang);
                SW.Close();

                TL.Text = "Removing registry...";
                Application.DoEvents();
                cMain.OpenProgram("\"" + cMain.SysRoot + "\\regedit.exe\"", "/s \"" + MountPath + "\\Speech.reg\"", true, System.Diagnostics.ProcessWindowStyle.Hidden);

                RemoveFile("Speech and Natural Language", MountPath + "\\Speech.reg", TL);
                if (RegMounted == false) { cReg.RegUnLoadAll(); }
            }
            catch { }
        }

        public void RemoveWinToolkitWallpapers()
        {
            try
            {
                Files.DeleteFolder(image.MountPath + "\\Windows\\Web\\Wallpaper\\WinToolkit\\", false);
            }
            catch { }
        }

        public void RemoveThemesEOA()
        {
            try
            {
                cMain.TakeOwnership(image.MountPath + "\\Windows\\Resources\\Ease of Access Themes");
                Files.DeleteFolder(image.MountPath + "\\Windows\\Resources\\Ease of Access Themes", false);
            }
            catch { }
        }
        public void RemoveThemesA()
        {
            try
            {
                cMain.TakeOwnership(image.MountPath + "\\Windows\\Resources\\Themes\\Aero.theme");
                cMain.TakeOwnership(image.MountPath + "\\Windows\\Resources\\Themes\\Aero");
                Files.DeleteFolder(image.MountPath + "\\Windows\\Resources\\Themes\\Aero", false);
                Files.DeleteFile(image.MountPath + "\\Windows\\Resources\\Themes\\Aero.theme");
            }
            catch { }
        }
        public void RemoveThemesNA()
        {
            try
            {
                foreach (string F in Directory.GetFiles(image.MountPath + "\\Windows\\Resources\\Themes", "*", SearchOption.TopDirectoryOnly))
                {
                    if (!F.ToUpper().EndsWithIgnoreCase("\\AERO.THEME"))
                    {
                        Files.DeleteFile(F);
                    }
                }
                foreach (string F in Directory.GetDirectories(image.MountPath + "\\Windows\\Resources\\Themes", "*", SearchOption.TopDirectoryOnly))
                {
                    if (!F.ContainsIgnoreCase("\\AERO"))
                    {
                        Files.DeleteFolder(F, false);
                    }
                }
            }
            catch
            {
            }
        }

        public static void GetCInfo(TreeNode TN, Label Title, Label Size, Label Desc, Label Depend, Label Safe,
            PictureBox PB, FlowLayoutPanel FLP)
        {
            bool bWin8 = false;
            foreach (WIMImage currentImage in cMain.selectedImages)
            {
                if (currentImage.Build.ContainsIgnoreCase("6.2")) { bWin8 = true; }
                if (currentImage.Build.ContainsIgnoreCase("6.3")) { bWin8 = true; }
                if (currentImage.Build.ContainsIgnoreCase("9600")) { bWin8 = true; }
                if (currentImage.Build.ContainsIgnoreCase("9200")) { bWin8 = true; }
                if (bWin8) { break; }
            }

            Title.Text = TN.Text;
            Depend.Text = "Dependencies: None";
            Size.Text = "";
            Desc.Text = string.IsNullOrEmpty(TN.ToolTipText) ? "There is no further information on this component." : TN.ToolTipText;
            Safe.Text = "";

            FLP.BackColor = TN.BackColor == Color.Empty ? SystemColors.GradientInactiveCaption : TN.BackColor;
            if (TN.BackColor == Color.LightGreen) { Safe.Text = "This item is safe to remove."; }
            if (TN.BackColor == Color.LightPink) { Safe.Text = "Not recommended to remove."; }

            if (TN.Text.EqualsIgnoreCase("Aero"))
            {
                PB.Image = Properties.Resources.Themes;
                Size.Text = "Size: 1.58MB";
                return;
            }

            if (TN.Text.EqualsIgnoreCase("Ease of Access Themes"))
            {
                PB.Image = Properties.Resources.Themes;
                Size.Text = "Size: 11.3KB";
                return;
            }

            if (TN.Text.EqualsIgnoreCase("Extra Themes"))
            {
                PB.Image = Properties.Resources.Themes;
                Size.Text = "Size: 38.7MB";
                return;
            }

            if (TN.Text.EqualsIgnoreCase("Speech and Natural Language"))
            {
                Size.Text = "Size: 590-890MB";

                if (bWin8)
                {
                    Desc.Text += "\n\nIt has been reported that removing this on Windows 8.1 can prevent .NET Framework 3.5 from being enabled. If you remove this and want to install .NET Framework 3.5, then you need to add it as a Silent Installer.";
                }
                return;
            }

            if (TN.Text.EqualsIgnoreCase("Delete 'WinSXS\\\\Backup' Folder"))
            {
                Size.Text = "Size: ~385MB";
                return;
            }

            //No Description
            PB.Image = Properties.Resources.None;
            Depend.Text = "";

        }
    }
}
