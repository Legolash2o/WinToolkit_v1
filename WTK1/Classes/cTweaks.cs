using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Properties;

namespace WinToolkit
{
    class cTweaks
    {
        static WIMImage image;
        public cTweaks(WIMImage currentImage)
        {
            image = currentImage;
        }

        private static readonly List<TreeNode> tvList = new List<TreeNode>();
        public static void SelectF(string GBText, TextBox TBox)
        {
            bool F = false;
            var OFD = new OpenFileDialog();

            switch (GBText)
            {
                case "Change Default Background":
                    OFD.Title = "Select default background";
                    OFD.Filter = "JPG *.jpg| *.jpg";
                    break;
                case "Change Logon Background":
                    OFD.Title = "Select Logon background";
                    OFD.Filter = "JPG *.jpg| *.jpg";
                    break;
                case "Change Setup Background":
                    OFD.Title = "Select Setup background";
                    OFD.Filter =
                         "Image (*.bmp *.jpg; *.tif; *.gif; *.png; *.jfif; *.jpe; *.jpeg)| *.bmp; *.jpg; *.tif; *.gif; *.png; *.jfif; *.jpe; *.jpeg";
                    break;
                case "Change Users Folders Location":
                    F = true;
                    break;
                case "Change Public Folder Location":
                    F = true;
                    break;
            }

            if (F)
            {
                string FBD = cMain.FolderBrowserVista(GBText, false, true);
                if (string.IsNullOrEmpty(FBD))
                {
                    return;
                }
                TBox.Text = FBD;
            }
            else
            {
                if (OFD.ShowDialog() != DialogResult.OK)
                    return;
                TBox.Text = OFD.FileName;
            }
        }

        private static void eNode(TreeNode TN)
        {
            foreach (TreeNode TC in TN.Nodes)
            {
                if (TC.Tag != null && TC.Tag != "C" && TC.Checked) { tvList.Add(TC); }
                if (TC.Nodes.Count > 0) { eNode(TC); }
            }

        }

        private static void ListNodes(TreeView TV)
        {
            tvList.Clear();
            foreach (TreeNode T in TV.Nodes)
            {
                if (T.Tag != null && T.Tag.ToString().EqualsIgnoreCase("Reg") && T.Checked) { tvList.Add(T); }
                if (T.Nodes.Count > 0) { eNode(T); }
            }
        }

        public static void IntegrateTweaks(TreeView LV, ToolStripLabel lbl, string cArc, string sDrv, bool WIMt, bool TestRegCon, ToolStripProgressBar PB, Form F, WIMImage image)
        {
            ListNodes(LV);
            int cT = tvList.Count;
            if (!sDrv.EndsWithIgnoreCase("\\"))
                sDrv = sDrv + "\\";
            try
            {
                cReg.WIMt = WIMt;
                int t = 1;

                TreeNode[] TN = LV.Nodes.Find("", true);
                foreach (TreeNode I in tvList)
                {
                    if (I.Checked && I.Tag != null)
                        {

                        try
                        {
                            string tag = (string) I.Tag;
                            if (tag.EqualsIgnoreCase("Reg"))
                            {
                                lbl.Text = "Integrating Reg: " + t + " of " + cT + " (" + I.Text + ")";
                                if (WIMt)
                                {
                                    cAddon.ConvertRegAddon(I.ToolTipText, cArc, TestRegCon, image);
                                }
                                else
                                {
                                    cMain.OpenProgram("\"" + cMain.SysRoot + "\\regedit.exe\"", "/s \"" + I.ToolTipText + "\"", true, ProcessWindowStyle.Hidden);
                                }
                            }
                            else
                            {
                                lbl.Text = "Integrating Tweaks: " + t + " of " + cT + " (" + I.Text + ")";
                            }
                            Application.DoEvents();
                            if (tag.EqualsIgnoreCase("chkEDDA"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\AutoplayHandlers",
                                                  "DisableAutoplay", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer",
                                                  "NoDriveTypeAutoRun", 255, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEFA"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "UseAlternateButtons", 1,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "EnableMachineCheck", 0,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "Glass", 1,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "Metal", 0,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "ForceSoftwareD3D", 0,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "DebugMessages", 0,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "DebugZOrder", 0,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "DebugMouse", 0,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "DebugDumpTree", 0,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "Composition", 1,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "Blur", 1,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "AnimationsShiftKey", 1,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "Animations", 1,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "UseDPIScaling", 1,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "RenderClientArea", 1,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "perUser", 0,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\DWM", "MagnificationPercent", 64,
                                                  RegistryValueKind.DWord);
                            }
                            if (tag.EqualsIgnoreCase("chkEDWVD"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Control Panel\\Desktop", "PaintDesktopVersion", 1,
                                                  RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkLDDLU"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System",
                                                  "DontDisplayLastUsername", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIEUSD"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings",
                                                  "MaxConnectionsPerServer", 14, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings",
                                                  "MaxConnectionsPer1_0Server", 14, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings",
                                                  "MaxConnectionsPerServer", 14, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings",
                                                  "MaxConnectionsPer1_0Server", 14, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEDUAC"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System",
                                                  "ConsentPromptBehaviorAdmin", 0, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", "EnableLUA",
                                                  0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEACRFCM"))
                            {
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\AllFilesystemObjects\\shellex\\ContextMenuHandlers\\{C2FBB630-2971-11D1-A18C-00C04FD75D13}");
                            }
                            if (tag.EqualsIgnoreCase("chkEAMTFCM"))
                            {
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\AllFilesystemObjects\\shellex\\ContextMenuHandlers\\{C2FBB631-2971-11D1-A18C-00C04FD75D13}");
                            }

                            if (tag.EqualsIgnoreCase("chkELDSN"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer",
                                                  "NoLowDiskSpaceChecks", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSSCLPFS"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SYSTEM\\ControlSet001\\Control\\Session Manager\\Memory Management",
                                                  "ClearPageFileAtShutdown", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SYSTEM\\ControlSet002\\Control\\Session Manager\\Memory Management",
                                                  "ClearPageFileAtShutdown", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkDARAIU"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU",
                                                  "NoAutoRebootWithLoggedOnUsers", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEATOCM"))
                            {
                                cReg.CreateRegKey(Registry.LocalMachine, "SOFTWARE\\Classes\\*\\shell\\TO");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\*\\shell\\TO", "SubCommands", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\*\\shell\\TO", "MUIVerb",
                                                  "Ownership");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\*\\shell\\TO", "Icon", "cmd.exe");
                                cReg.CreateRegKey(Registry.LocalMachine, "SOFTWARE\\Classes\\*\\shell\\TO\\shell\\runas");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\*\\shell\\TO\\shell\\runas",
                                                  "MUIVerb", "Take Ownership");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\*\\shell\\TO\\shell\\runas", "Icon",
                                                  "cmd.exe");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\*\\shell\\TO\\shell\\runas",
                                                  "HasLUAShield", "");
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\*\\shell\\TO\\shell\\runas\\command");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\*\\shell\\TO\\shell\\runas\\command", "",
                                                  "cmd.exe /c takeown /f \"%V\" && icacls \"%V" +
                                                  "\" /grant administrators:F");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\*\\shell\\TO\\shell\\runas\\command", "IsolatedCommand",
                                                  "cmd.exe /c takeown /f \"%V\" && icacls \"%V" +
                                                  "\" /grant administrators:F");
                                cReg.DeleteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\*\\TO\\CMD", "Extended");

                                cReg.CreateRegKey(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\TO");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\TO",
                                                  "SubCommands", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\TO", "MUIVerb",
                                                  "Ownership");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\TO", "Icon",
                                                  "cmd.exe");
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\Directory\\shell\\TO\\shell\\runas");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\TO\\shell\\runas",
                                                  "MUIVerb", "Take Ownership");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\TO\\shell\\runas",
                                                  "Icon", "cmd.exe");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\TO\\shell\\runas",
                                                  "HasLUAShield", "");
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\Directory\\shell\\TO\\shell\\runas\\command");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\shell\\TO\\shell\\runas\\command", "",
                                                  "cmd.exe /c takeown /f \"%V\" /r /d y && icacls \"" +
                                                  "%V\" /grant administrators:F");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\shell\\TO\\shell\\runas\\command",
                                                  "IsolatedCommand",
                                                  "cmd.exe /c takeown /f \"%V\" /r /d y && icacls \"" +
                                                  "%V\" /grant administrators:F");
                                cReg.DeleteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\TO\\CMD", "Extended");
                            }

                            if (tag.EqualsIgnoreCase("chkEACTFCM"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\AllFilesystemObjects\\shellex\\ContextMenuHandlers\\{C2FBB630-2971-11D1-A18C-00C04FD75D13}",
                                                  "", "");
                            }

                            if (tag.EqualsIgnoreCase("chkEAMTFCM"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\AllFilesystemObjects\\shellex\\ContextMenuHandlers\\{C2FBB631-2971-11D1-A18C-00C04FD75D13}",
                                                  "", "");
                            }

                            if (tag.EqualsIgnoreCase("chkECDB"))
                            {
                                string R = cMain.RandomName();

                                string S = sDrv + "Windows\\Web\\Wallpaper\\Windows\\img" + R + ".jpg";
                                while (File.Exists(S))
                                {
                                    R = cMain.RandomName();
                                    S = sDrv + "Windows\\Web\\Wallpaper\\Windows\\img" + R + ".jpg";
                                }

                                cMain.TakeOwnership(sDrv + "Windows\\Web\\Wallpaper\\Windows\\img0.jpg");

                                if (File.Exists(sDrv + "Windows\\Web\\Wallpaper\\Windows\\img0.jpg"))
                                    File.Move(sDrv + "Windows\\Web\\Wallpaper\\Windows\\img0.jpg",
                                                 sDrv + "Windows\\Web\\Wallpaper\\Windows\\img" + R + ".jpg");
                                File.Copy(I.Nodes[0].Text, sDrv + "Windows\\Web\\Wallpaper\\Windows\\img0.jpg", true);
                            }

                            if (tag.EqualsIgnoreCase("chkESUD"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Control Panel\\Desktop", "HungAppTimeout", "2000");
                                cReg.WriteValue(Registry.CurrentUser, "Control Panel\\Desktop", "MenuShowDelay", "100");
                            }

                            if (tag.EqualsIgnoreCase("chkSSSQ35"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Control Panel\\Desktop", "WaitToKillAppTimeout", "5000");
                                cReg.WriteValue(Registry.CurrentUser, "Control Panel\\Desktop", "LowLevelHooksTimeout", "3000");
                                cReg.WriteValue(Registry.CurrentUser, "Control Panel\\Desktop", "AutoEndTasks", "1");
                            }

                            if (tag.EqualsIgnoreCase("chkMCSB22") && File.Exists(I.Nodes[0].Text))
                            {
                                string S = "";
                                try
                                {
                                    cMain.WriteResource(Resources.ResHacker, cMain.UserTempPath + "\\ResHacker.exe", null);
                                    if (File.Exists(sDrv + "Windows\\System32\\oobe\\Background.bmp"))
                                    {
                                        cMain.TakeOwnership(sDrv + "Windows\\System32\\oobe\\Background.bmp");
                                        File.Copy(I.Nodes[0].Text, sDrv + "Windows\\System32\\oobe\\Background.bmp", true);
                                    }
                                    if (File.Exists(sDrv + "SysWOW64\\oobe\\Background.bmp"))
                                    {
                                        cMain.TakeOwnership(sDrv + "SysWOW64\\oobe\\Background.bmp");
                                        File.Copy(I.Nodes[0].Text, sDrv + "SysWOW64\\oobe\\Background.bmp", true);
                                    }

                                    S = sDrv + "Windows\\System32\\spwizimg.dll";
                                    if (File.Exists(S))
                                    {
                                        cMain.TakeOwnership(S);
                                        cMain.OpenProgram("\"" + cMain.UserTempPath + "\\ResHacker.exe\"",
                                                                "-addoverwrite \"" + S + "\", \"" + S + "\", " +
                                                                "\"" + I.Nodes[0].Text + "\", BITMAP, 517, 1033", true,
                                                                ProcessWindowStyle.Hidden);
                                        cMain.OpenProgram("\"" + cMain.UserTempPath + "\\ResHacker.exe\"",
                                                                "-addoverwrite \"" + S + "\", \"" + S + "\", " +
                                                                "\"" + I.Nodes[0].Text + "\", BITMAP, 518, 1033", true,
                                                                ProcessWindowStyle.Hidden);
                                    }

                                    S = sDrv + "SysWOW64\\spwizimg.dll";
                                    if (File.Exists(S))
                                    {
                                        cMain.TakeOwnership(S);
                                        cMain.OpenProgram("\"" + cMain.UserTempPath + "\\ResHacker.exe\"",
                                                                "-addoverwrite \"" + S + "\", \"" + S + "\", " +
                                                                "\"" + I.Nodes[0].Text + "\", BITMAP, 517, 1033", true,
                                                                ProcessWindowStyle.Hidden);
                                        cMain.OpenProgram("\"" + cMain.UserTempPath + "\\ResHacker.exe\"",
                                                                "-addoverwrite \"" + S + "\", \"" + S + "\", " +
                                                                "\"" + I.Nodes[0].Text + "\", BITMAP, 518, 1033", true,
                                                                ProcessWindowStyle.Hidden);
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    new SmallError("Unable to set background (Main)", Ex, S + Environment.NewLine + "Source: " + I.Nodes[0].Text).Upload();
                                }

                            }

                            if (tag.EqualsIgnoreCase("chkLLogBack"))
                            {
                                lbl.Text = "Integrating Reg: " + t + " of " + cT + " (" + I.Text + ")";
                                cReg.DeleteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Authentication\\LogonUI\\Background", "OEMBackground");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Policies\\Microsoft\\Windows\\System", "UseOEMBackground", 1, RegistryValueKind.DWord);

                                lbl.Text = "Integrating Reg [Cleaning]: " + t + " of " + cT + " (" + I.Text + ")";
                                Files.DeleteFolder(sDrv + "Windows\\System32\\oobe\\Info\\backgrounds", true);
                                if (!Directory.Exists(sDrv + "Windows\\System32\\oobe\\Info\\backgrounds")) { cMain.CreateDirectory(sDrv + "Windows\\System32\\oobe\\Info\\backgrounds"); }

                                try
                                {
                                    lbl.Text = "Integrating Reg [Copying]: " + t + " of " + cT + " (" + I.Text + ")";
                                    File.Copy(I.Nodes[0].Text, sDrv + "Windows\\System32\\oobe\\Info\\backgrounds\\tt-backgroundDefault.jpg", true);
                                    lbl.Text = "Integrating Reg [Editing]: " + t + " of " + cT + " (" + I.Text + ")";
                                    string BG = sDrv + "Windows\\System32\\oobe\\Info\\backgrounds\\tt-backgroundDefault";
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 768, 1280);
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 800, 600);
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 960, 1280);
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 900, 1440);
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 1024, 768);
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 1024, 1280);
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 1080, 720);
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 1280, 960);
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 1280, 768);
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 1280, 1024);
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 1360, 768);
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 1600, 1200);
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 1440, 900);
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 1920, 1080);
                                    cImage.ChangeRes(I.Nodes[0].Text, BG, 1920, 1200);
                                    //SaveTo + W + "x" + H + ".jpg"
                                    foreach (Screen S in Screen.AllScreens)
                                    {
                                        int iHeight = S.Bounds.Height;
                                        int iWidth = S.Bounds.Width;
                                        if (!File.Exists(BG + iWidth.ToString() + "x" + iHeight.ToString() + ".jpg"))
                                        {
                                            cImage.ChangeRes(I.Nodes[0].Text, BG, iWidth, iHeight);
                                        }

                                    }
                                }
                                catch { }

                                try
                                {
                                    lbl.Text = "Integrating Reg [Compressing]: " + t + " of " + cT + " (" + I.Text + ")";
                                    foreach (string Img in Directory.GetFiles(sDrv + "Windows\\System32\\oobe\\Info\\backgrounds", "*.jpg"))
                                    {
                                        try
                                        {
                                            if (Img.ContainsIgnoreCase("\\tt-"))
                                            {
                                                cImage.CompressImage(Img, Img.ReplaceIgnoreCase("\\tt-", "\\"), 256000);
                                            }
                                            if (File.Exists(Img.ReplaceIgnoreCase("\\tt-", "\\")))
                                            {
                                                File.Delete(Img);
                                            }
                                            else
                                            {
                                                File.Move(Img, Img.ReplaceIgnoreCase( "\\tt-", "\\"));
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                catch
                                {
                                }
                            }

                            if (tag.EqualsIgnoreCase("chkE3PT"))
                            {
                                if (WIMt)
                                {
                                    cThemes.PatchFiles(sDrv);
                                }

                            }

                            if (tag.EqualsIgnoreCase("chkEHCDOD"))
                            {
                                int idx = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide"))
                                    idx = 0;

                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer",
                                                  "ConfirmFileDelete", idx, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("cboLLogText"))
                            {
                                int idx = 0;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Windows Default"))
                                    idx = 0;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Dark Text Shadows and Light Buttons"))
                                    idx = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("No Shadows and Opaque Buttons"))
                                    idx = 2;
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Authentication\\LogonUI",
                                                  "ButtonSet", idx, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSSDGPS"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SYSTEM\\Microsoft\\Windows\\CurrentVersion\\Policies\\System",
                                                  "SynchronousMachineGroupPolicy", 0, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SYSTEM\\Microsoft\\Windows\\CurrentVersion\\Policies\\System",
                                                  "SynchronousUserGroupPolicy", 0, RegistryValueKind.DWord);
                            }
                            if (tag.EqualsIgnoreCase("chkSSEMSIS"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SYSTEM\\ControlSet001\\Control\\SafeBoot\\Network\\MSIServer", "", "Service");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SYSTEM\\ControlSet001\\Control\\SafeBoot\\Minimal\\MSIServer", "", "Service");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SYSTEM\\ControlSet002\\Control\\SafeBoot\\Network\\MSIServer", "", "Service");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SYSTEM\\ControlSet002\\Control\\SafeBoot\\Minimal\\MSIServer", "", "Service");
                            }
                            if (tag.EqualsIgnoreCase("chkERSS"))
                            {
                                byte[] binValue = { 0x0, 0x0, 0x0, 0x0 };
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer", "link", binValue, RegistryValueKind.Binary);
                            }

                            if (tag.EqualsIgnoreCase("chkSCMD"))
                            {
                                int v = 2;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("None")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Complete")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Kernel [Default]")) { v = 2; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Small 64KB")) { v = 3; }

                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet001\\Control\\CrashControl",
                                                  "CrashDumpEnabled", v, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet002\\Control\\CrashControl",
                                                  "CrashDumpEnabled", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSDWER"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\Windows Error Reporting",
                                                  "DontSendAdditionalData", 0, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\Windows Error Reporting",
                                                  "DontShowU", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\Windows Error Reporting",
                                                  "Disabled", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\Windows Error Reporting\\Consent",
                                                  "DefaultConsent", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows\\Windows Error Reporting\\WMR", "Disable", 1,
                                                  RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEAOCPHCM"))
                            {
                                cReg.CreateRegKey(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD", "", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD",
                                                  "SubCommands", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD", "MUIVerb",
                                                  "Command Prompt");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD", "Icon",
                                                  "cmd.exe");
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\Directory\\shell\\CMD\\shell\\cmd");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD\\shell\\cmd",
                                                  "MUIVerb", "Open CMD Here");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD\\shell\\cmd",
                                                  "Icon", "cmd.exe");
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\Directory\\shell\\CMD\\shell\\cmd\\command");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\shell\\CMD\\shell\\cmd\\command", "",
                                                  "cmd.exe /k pushd \"%V\"");
                                cReg.DeleteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD",
                                                      "Extended");
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD", "Command");

                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD",
                                                  "", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD",
                                                  "SubCommands", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD",
                                                  "MUIVerb", "Command Prompt");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD",
                                                  "Icon", "cmd.exe");
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD\\shell\\cmd");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD\\shell\\cmd", "MUIVerb",
                                                  "Open CMD Here");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD\\shell\\cmd", "Icon",
                                                  "cmd.exe");
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD\\shell\\cmd\\command");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD\\shell\\cmd\\command",
                                                  "", "cmd.exe /k pushd \"%V\"");
                                cReg.DeleteValue(Registry.LocalMachine,
                                                      "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD", "Extended");
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD",
                                                    "Command");

                                cReg.CreateRegKey(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD", "SubCommands",
                                                  "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD", "", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD", "MUIVerb",
                                                  "Command Prompt");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD", "Icon",
                                                  "cmd.exe");
                                cReg.CreateRegKey(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD\\shell\\cmd");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD\\shell\\cmd",
                                                  "MUIVerb", "Open CMD Here");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD\\shell\\cmd",
                                                  "Icon", "cmd.exe");
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\Drive\\shell\\CMD\\shell\\cmd\\command");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Drive\\shell\\CMD\\shell\\cmd\\command", "",
                                                  "cmd.exe /k pushd \"%V\"");
                                cReg.DeleteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD", "Extended");
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD", "Command");
                            }

                            if (tag.EqualsIgnoreCase("chkCMOECMDWCM"))
                            {
                                cReg.CreateRegKey(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD",
                                                  "SubCommands", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD", "", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD", "MUIVerb",
                                                  "Command Prompt");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD", "Icon",
                                                  "cmd.exe");
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\Directory\\shell\\CMD\\shell\\runas");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\shell\\CMD\\shell\\runas", "MUIVerb",
                                                  "Open Elevated CMD Here");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\shell\\CMD\\shell\\runas", "Icon", "cmd.exe");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\shell\\CMD\\shell\\runas", "HasLUAShield", "");
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\Directory\\shell\\CMD\\shell\\runas\\command");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\shell\\CMD\\shell\\runas\\command", "",
                                                  "cmd.exe /s /k pushd \"%V\"");
                                cReg.DeleteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD",
                                                      "Extended");
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\shell\\CMD", "Command");

                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD",
                                                  "SubCommands", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD",
                                                  "", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD",
                                                  "MUIVerb", "Command Prompt");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD",
                                                  "Icon", "cmd.exe");
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD\\shell\\runas");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD\\shell\\runas",
                                                  "MUIVerb", "Open Elevated CMD Here");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD\\shell\\runas", "Icon",
                                                  "cmd.exe");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD\\shell\\runas",
                                                  "HasLUAShield", "");
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD\\shell\\runas\\command");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD\\shell\\runas\\command",
                                                  "", "cmd.exe /s /k pushd \"%V\"");
                                cReg.DeleteValue(Registry.LocalMachine,
                                                      "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD", "Extended");
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes\\Directory\\Background\\shell\\CMD",
                                                    "Command");

                                cReg.CreateRegKey(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD", "SubCommands",
                                                  "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD", "", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD", "MUIVerb",
                                                  "Command Prompt");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD", "Icon",
                                                  "cmd.exe");
                                cReg.CreateRegKey(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD\\shell\\cmd");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD\\shell\\runas",
                                                  "MUIVerb", "Open Elevated CMD Here");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD\\shell\\runas",
                                                  "Icon", "cmd.exe");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD\\shell\\runas",
                                                  "HasLUAShield", "");
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                        "SOFTWARE\\Classes\\Drive\\shell\\CMD\\shell\\runas\\command");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Drive\\shell\\CMD\\shell\\runas\\command", "",
                                                  "cmd.exe /s /k pushd \"%V\"");
                                cReg.DeleteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD", "Extended");
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes\\Drive\\shell\\CMD", "Command");
                            }

                            if (tag.EqualsIgnoreCase("chkDAMDCM"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "MUIVerb",
                                                  "Windows");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "SubCommands", "");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "Icon",
                                                  "%SystemRoot%\\\\System32\\\\winver.exe");

                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\Manage",
                                                  "MUIVerb", "@%systemroot%\\\\system32\\\\mycomput.dll,-400");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\Manage",
                                                  "Icon", "wsqmcons.exe");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\Manage",
                                                  "SuppressionPolicy", 1073741884, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\Manage\\command",
                                                  "", "mmc.exe compmgmt.msc", RegistryValueKind.ExpandString);
                            }

                            if (tag.EqualsIgnoreCase("chkEASDCM"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "MUIVerb",
                                                  "Windows");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "SubCommands", "");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "Icon",
                                                  "%SystemRoot%\\\\System32\\\\winver.exe");

                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\Services",
                                                  "MUIVerb", "@%SystemRoot%\\\\system32\\\\shell32.dll,-22059");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\Services",
                                                  "Icon", "%SystemRoot%\\\\System32\\\\mmc.exe");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\Services",
                                                  "SuppressionPolicy", 1073741884, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\Services\\command",
                                                  "", "mmc.exe services.msc", RegistryValueKind.ExpandString);
                            }

                            if (tag.EqualsIgnoreCase("chkEAATDCM"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "MUIVerb",
                                                  "Windows");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "SubCommands", "");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "Icon",
                                                  "%SystemRoot%\\\\System32\\\\winver.exe");

                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\AllTasks",
                                                  "MUIVerb", "@%SystemRoot%\\\\system32\\\\shell32.dll,-32537");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\AllTasks",
                                                  "Icon", "%SystemRoot%\\\\System32\\\\imageres.dll,-27");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\AllTasks\\command",
                                                  "", "explorer shell:::{ED7BA470-8E54-465E-825C-99712043E01C}",
                                                  RegistryValueKind.ExpandString);
                            }

                            if (tag.EqualsIgnoreCase("chkEADDCM"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "MUIVerb",
                                                  "Windows");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "SubCommands", "");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "Icon",
                                                  "%SystemRoot%\\\\System32\\\\winver.exe");

                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\Defrag",
                                                  "MUIVerb", "@%SystemRoot%\\\\system32\\\\dfrgui.exe,-103");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\Defrag",
                                                  "Icon", "%SystemRoot%\\\\System32\\\\dfrgui.exe");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\Defrag\\command",
                                                  "", "%SystemRoot%\\System32\\dfrgui.exe", RegistryValueKind.ExpandString);
                            }

                            if (tag.EqualsIgnoreCase("chkEAATTDCM"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "MUIVerb",
                                                  "Windows");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "SubCommands", "");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "Icon",
                                                  "%SystemRoot%\\\\System32\\\\winver.exe");

                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\AdminTools",
                                                  "MUIVerb", "@%SystemRoot%\\\\system32\\\\shell32.dll,-22982");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\AdminTools",
                                                  "Icon", "%SystemRoot%\\\\System32\\\\imageres.dll,-114");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\AdminTools\\command",
                                                  "", "control admintools", RegistryValueKind.ExpandString);
                            }

                            if (tag.EqualsIgnoreCase("chkERSCA"))
                            {
                                cMain.WriteResource(Resources.blankico, sDrv + "\\Windows\\blank.ico", null);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Shell Icons", "29",
                                                  "%SystemRoot%\\\\blank.ico,0");
                            }

                            if (tag.EqualsIgnoreCase("chkCMRTSCM"))
                            {
                                cReg.DeleteKey(Registry.LocalMachine,
                                                    "SOFTWARE\\Classes\\batfile\\ShellEx\\ContextMenuHandlers", "Compatibility");
                                cReg.DeleteKey(Registry.LocalMachine,
                                                    "SOFTWARE\\Classes\\cmdfile\\ShellEx\\ContextMenuHandlers", "Compatibility");
                                cReg.DeleteKey(Registry.LocalMachine,
                                                    "SOFTWARE\\Classes\\exefile\\shellex\\ContextMenuHandlers", "Compatibility");
                                cReg.DeleteKey(Registry.LocalMachine,
                                                    "SOFTWARE\\Classes\\lnkfile\\shellex\\ContextMenuHandlers", "Compatibility");
                                cReg.DeleteKey(Registry.LocalMachine,
                                                    "SOFTWARE\\Classes\\Msi.Package\\shellex\\ContextMenuHandlers",
                                                    "Compatibility");
                            }

                            if (tag.EqualsIgnoreCase("chkCMRPCM"))
                            {
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes\\regfile\\shell", "print");
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes\\txtfile\\shell", "printto");
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes\\txtfile\\shell", "print");
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes\\VBSFile\\shell", "print");
                                cReg.DeleteKey(Registry.LocalMachine,
                                                    "SOFTWARE\\Classes\\SystemFileAssociations\\image\\shell", "print");
                                cReg.DeleteKey(Registry.LocalMachine,
                                                    "SOFTWARE\\Classes\\SystemFileAssociations\\text\\shell", "print");
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes\\cmdfile\\shell", "print");
                            }

                            if (tag.EqualsIgnoreCase("chkCMRAWMPCM"))
                            {
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes\\SystemFileAssociations\\audio",
                                                    "shell");
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes\\SystemFileAssociations\\audio",
                                                    "shellex");
                            }

                            //change user folders location
                            if (tag.EqualsIgnoreCase("chkMCUFL"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\ProfileList",
                                                  "ProfilesDirectory", I.Nodes[0].Text);
                            }

                            if (tag.EqualsIgnoreCase("chkMCPFL"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\ProfileList", "Public",
                                                  I.Nodes[0].Text);
                            }

                            if (tag.EqualsIgnoreCase("chkDACOD"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel",
                                                  "{20D04FE0-3AEA-1069-A2D8-08002B30309D}", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSUFOD"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel",
                                                  "{59031a47-3f72-44a7-89c5-5595fe6b30ee}", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkMAWLMIN"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "Software\\Microsoft\\Windows Live\\Movie Maker",
                                                  "AllowNetworkFiles", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEDSWA"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Control Panel\\Desktop", "WindowArrangementActive", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkELTP"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Taskband",
                                                  "MinThumbSizePx", 350, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSAWDF"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "SYSTEM\\ControlSet001\\Control\\CrashControl",
                                                  "AlwaysKeepMemoryDump", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SYSTEM\\ControlSet002\\Control\\CrashControl",
                                                  "AlwaysKeepMemoryDump", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSDH"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "SYSTEM\\ControlSet001\\Control\\Power",
                                                  "HibernateEnabled", 0, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SYSTEM\\ControlSet001\\Control\\Power",
                                                  "HiberFileSizePerfect", 0, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SYSTEM\\ControlSet002\\Control\\Power",
                                                  "HibernateEnabled", 0, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "SYSTEM\\ControlSet002\\Control\\Power",
                                                  "HiberFileSizePerfect", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEDSOUFT"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer",
                                                  "NoInternetOpenWith", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkDSLR"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer",
                                                  "LinkResolveIgnoreLinkInfo", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer",
                                                  "NoResolveSearch", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer",
                                                  "NoResolveTrack", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkERLPE"))
                            {
                                cReg.DeleteKey(Registry.LocalMachine,
                                                    "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace",
                                                    "{031E4825-7B94-4dc3-B131-E946B44C8DD5}");
                            }

                            if (tag.EqualsIgnoreCase("chkERHGLPE"))
                            {
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Desktop\\NameSpace", "{B4FB3F98-C1EA-428d-A78A-D1F5659CBA93}");
                            }

                            if (tag.EqualsIgnoreCase("chkSMRM"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Link")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Menu")) { v = 2; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowMyMusic", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMRV"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Link") ){ v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Menu")) { v = 2; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowVideos", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMRP"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Link")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Menu") ){ v = 2; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowMyPics", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMSD"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Link")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Menu")) { v = 2; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowDownloads", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMRD"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Link")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Menu")) { v = 2; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowMyDocs", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMRG"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Link")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Menu")) { v = 2; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowMyGames", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMRCP"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Link")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Menu")) { v = 2; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowControlPanel", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMC"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Link")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Menu")) { v = 2; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowMyComputer", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMRTV"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Link")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Menu")) { v = 2; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowRecordedTV", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMPF"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Link")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Menu")) { v = 2; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowUser", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMRDPROG"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Show")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide") ){ v = 0; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowSetProgramAccessAndDefaults", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMRHS"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Show")) { v = 1; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowHelp", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMRDP"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide") ){ v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Show")) { v = 1; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowPrinters", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkCMSF"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Show")) { v = 1; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "StartMenuFavorites", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMSR"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Show") ){ v = 1; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowRun", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMAdminTools"))
                            {
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide"))
                                {
                                    cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_AdminToolsRoot", 0, RegistryValueKind.DWord);
                                    cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "StartMenuAdminTools", 0, RegistryValueKind.DWord);
                                    cReg.DeleteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_AdminToolsTemp");
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("All Programs"))
                                {
                                    cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_AdminToolsRoot", 0, RegistryValueKind.DWord);
                                    cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_AdminToolsTemp", 1, RegistryValueKind.DWord);
                                    cReg.DeleteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "StartMenuAdminTools");
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("All Programs and Start Menu"))
                                {
                                    cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_AdminToolsRoot", 2, RegistryValueKind.DWord);
                                    cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "StartMenuAdminTools", 1, RegistryValueKind.DWord);
                                    cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_AdminToolsTemp", 2, RegistryValueKind.DWord);

                                }

                            }

                            if (tag.EqualsIgnoreCase("chkSMDHNI23"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Highlight")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Don't Highlight")) { v = 0; }

                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_NotifyNewApps", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMECMDD"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Enable")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Disable")) { v = 0; }

                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_EnableDragDrop", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMOBWMH"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Enable")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Disable")) { v = 0; }

                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_AutoCascade", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMSPCP"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Enable")) { v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Disable")) { v = 0; }

                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_SearchPrograms", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMSOFL"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Don't search")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Search with public folders")) { v = 2; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Search without public folders")) { v = 1; }

                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_SearchFiles", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMNORPTD"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_MinMFU", int.Parse(I.Nodes[0].Text), RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMNRIDJL"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_JumpListItems", int.Parse(I.Nodes[0].Text), RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMSAPMN"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Enable") ){ v = 1; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Disable") ){ v = 0; }

                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_SortByName", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSHG"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide") ){ v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Show")) { v = 1; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowHomegroup", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMSRC"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Show")) { v = 1; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowRecentDocs", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMSN"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide") ){ v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Show") ){ v = 1; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowNetPlaces", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMRCT"))
                            {
                                int v = 1;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide")) { v = 0; }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Show")) { v = 1; }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_ShowNetConn", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSMDLI"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Start_LargeMFUIcons", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkDCTWF"))
                            {
                                int v = 0;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Always combine, hide labels"))
                                {
                                    v = 0;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Combine when taskbar is full"))
                                {
                                    v = 1;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Never combine"))
                                {
                                    v = 2;
                                }
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
                                                  "TaskbarGlomLevel", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkESTP"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
                                                  "ExtendedUIHoverTime", 1000, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEDDP"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
                                                  "DisablePreviewDesktop", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEDTP"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
                                                  "TaskbarNoThumbnail", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSDW7DVD"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
                                                  "NoCDBurning", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer",
                                                  "NoCDBurning", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEMHT1"))
                            {
                                string v = "400";
                                if (I.Nodes[0].Text != "400 (Default)") { v = I.Nodes[0].Text; }

                                cReg.WriteValue(Registry.CurrentUser, "Control Panel\\Mouse", "MouseHoverTime", v);
                            }

                            if (tag.EqualsIgnoreCase("chkDDIS74"))
                            {
                                string v = "32";
                                if (I.Nodes[0].Text != "32 (Default)") { v = I.Nodes[0].Text; }

                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\Shell\\Bags\\1\\Desktop", "IconSize", v);
                                cReg.WriteValue(Registry.CurrentUser, "Control Panel\\Desktop\\WindowMetrics", "Shell Icon Size", v);
                            }

                            if (tag.EqualsIgnoreCase("chkSDMDCS35"))
                            {
                                int v = 0;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Enable Checking")) { v = 1; }

                                cReg.WriteValue(Registry.LocalMachine, "SYSTEM\\ControlSet001\\Control\\NetworkProvider", "RestoreConnection", v);
                                cReg.WriteValue(Registry.LocalMachine, "SYSTEM\\ControlSet002\\Control\\NetworkProvider", "RestoreConnection", v);
                            }

                            if (tag.EqualsIgnoreCase("chkEDMS2"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Control Panel\\Mouse", "MouseSpeed", "2");
                            }

                            if (tag.EqualsIgnoreCase("chkDDLB374"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\CTF\\LangBar", "ExtraIconsOnMinimized", 0, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\CTF\\LangBar", "ShowStatus", 3, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\CTF\\MSUTB", "ShowDeskBand", 0, RegistryValueKind.DWord);

                                cReg.DeleteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Run", "ctfmon.exe");
                            }

                            if (tag.EqualsIgnoreCase("chkESSF10"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "ShowSuperHidden", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEDTT16"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
                                                  "ShowInfoTip", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkMRMWSS17"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows Mail", "NoSplash", 1,
                                                  RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkMEDVDMP21"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\MediaPlayer\\Player\\Settings",
                                                  "EnableDVDUI", "Yes");
                            }

                            if (tag.EqualsIgnoreCase("chkEIIC6"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer", "Max Cached Icons",
                                                  2048, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSAUDLL11"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer", "AlwaysUnloadDLL", 1,
                                                  RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSSEBD12"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Dfrg\\BootOptimizeFunction",
                                                  "Enable", "Y");
                            }

                            if (tag.EqualsIgnoreCase("chkMDWMAU18"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Policies\\Microsoft\\WindowsMediaPlayer",
                                                  "DisableAutoUpdate", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSELSC3"))
                            {
                                int v = 38;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Enable"))
                                {
                                    v = 1;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Disable"))
                                {
                                    v = 0;
                                }

                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet001\\Control\\Session Manager\\Memory Management",
                                                  "LargeSystemCache", v, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet002\\Control\\Session Manager\\Memory Management",
                                                  "LargeSystemCache", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSDP5"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet001\\Control\\Session Manager\\Memory Management\\PrefetchParameters",
                                                  "EnablePrefetcher", 0, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet002\\Control\\Session Manager\\Memory Management\\PrefetchParameters",
                                                  "EnablePrefetcher", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSDS4"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet001\\Control\\Session Manager\\Memory Management\\PrefetchParameters",
                                                  "EnableSuperfetch", 0, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet002\\Control\\Session Manager\\Memory Management\\PrefetchParameters",
                                                  "EnableSuperfetch", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSDPK7"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet001\\Control\\Session Manager\\Memory Management",
                                                  "DisablePagingExecutive", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet002\\Control\\Session Manager\\Memory Management",
                                                  "DisablePagingExecutive", 1, RegistryValueKind.DWord);
                            }
                            if (tag.EqualsIgnoreCase("chkSDPF83"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet001\\Control\\Session Manager\\Memory Management",
                                                  "PagingFiles", "", RegistryValueKind.ExpandString);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet002\\Control\\Session Manager\\Memory Management",
                                                  "PagingFiles", "", RegistryValueKind.ExpandString);
                            }

                            //
                            if (tag.EqualsIgnoreCase("chkSBNDNSE8"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet001\\Services\\Dnscache\\Parameters",
                                                  "MaxNegativeCacheTtlValue", 0, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet002\\Services\\Dnscache\\Parameters",
                                                  "MaxNegativeCacheTtlValue", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSKPDNSE9"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet001\\Services\\Dnscache\\Parameters",
                                                  "MaxCacheTtl", 14400, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet002\\Services\\Dnscache\\Parameters",
                                                  "MaxCacheTtl", 14400, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEDNC14"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet001\\Control\\FileSystem",
                                                  "NtfsDisable8dot3NameCreation", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet002\\Control\\FileSystem",
                                                  "NtfsDisable8dot3NameCreation", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEDLFA13"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet001\\Control\\FileSystem",
                                                  "NtfsDisableLastAccessUpdate", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet002\\Control\\FileSystem",
                                                  "NtfsDisableLastAccessUpdate", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkMFBIE19"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet001\\Control\\Services\\AFD\\Parameters",
                                                  "BufferMultiplier", 400, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet002\\Control\\Services\\AFD\\Parameters",
                                                  "BufferMultiplier", 400, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSDAR20"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet001\\Control\\CrashControl",
                                                  "AutoReboot", 0, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet002\\Control\\CrashControl",
                                                  "AutoReboot", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkESCPV22"))
                            {
                                switch (I.Nodes[0].Text)
                                {
                                    case "Category":
                                        cReg.WriteValue(Registry.CurrentUser,
                                                          "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\ControlPanel",
                                                          "AllItemsIconView", 0, RegistryValueKind.DWord);
                                        cReg.WriteValue(Registry.CurrentUser,
                                                          "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\ControlPanel",
                                                          "StartupPage", 0, RegistryValueKind.DWord);
                                        break;
                                    case "Large Icons":
                                        cReg.WriteValue(Registry.CurrentUser,
                                                          "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\ControlPanel",
                                                          "AllItemsIconView", 0, RegistryValueKind.DWord);
                                        cReg.WriteValue(Registry.CurrentUser,
                                                          "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\ControlPanel",
                                                          "StartupPage", 1, RegistryValueKind.DWord);
                                        break;
                                    case "Small Icons":
                                        cReg.WriteValue(Registry.CurrentUser,
                                                          "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\ControlPanel",
                                                          "AllItemsIconView", 1, RegistryValueKind.DWord);
                                        cReg.WriteValue(Registry.CurrentUser,
                                                          "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\ControlPanel",
                                                          "StartupPage", 1, RegistryValueKind.DWord);
                                        break;
                                }
                            }

                            if (tag.EqualsIgnoreCase("chkSSRPFAS27"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
                                                  "PersistBrowsers", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEASMB26"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
                                                  "AlwaysShowMenus", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkDSNOD23"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel",
                                                  "{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkDSCPOD24"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel",
                                                  "{5399E694-6CE5-4D6C-8FCE-1D8870FDCBA0}", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSDDRW25"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\AeDebug", "Auto", 0,
                                                  RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSURPCPS26"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Rpc", "MaxRpcSize", 00100000,
                                                  RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSSDSL28"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\policies\\system",
                                                  "verbosestatus", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkD500msDAP29"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
                                                  "DesktopLivePreviewHoverTime", 500, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSHFF31"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "Hidden",
                                                  1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEDTC31"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
                                                  "DisableThumbnailCache", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkDUTT32"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "TaskbarSizeMove", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkCMRUDLL73"))
                            {
                                //DLL
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\dllfile\\Shell\\Register", "NeverDefault", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\dllfile\\Shell\\Register\\command", "", "regsvr32 \"%1\"");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\dllfile\\Shell\\Unregister", "NeverDefault", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\dllfile\\Shell\\Unregister\\command", "", "regsvr32 /u \"%1\"");

                                //OCX
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\ocxfile\\Shell\\Register", "NeverDefault", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\ocxfile\\Shell\\Register\\command", "", "regsvr32 \"%1\"");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\ocxfile\\Shell\\Unregister", "NeverDefault", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\ocxfile\\Shell\\Unregister\\command", "", "regsvr32 /u \"%1\"");

                                //AX
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\.ax", "", "axfile");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\axfile", "", "DirectShow Filter");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\axfile\\Shell\\Register", "NeverDefault", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\axfile\\Shell\\Register\\command", "", "regsvr32 \"%1\"");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\axfile\\Shell\\Unregister", "NeverDefault", "");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\axfile\\Shell\\Unregister\\command", "", "regsvr32 /u \"%1\"");

                            }

                            if (tag.EqualsIgnoreCase("chkSTOSB33"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Control Panel\\Sound", "beep", "no");
                            }

                            if (tag.EqualsIgnoreCase("chkMSSL34"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings",
                                                  "SecureProtocols", 2728, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings",
                                                  "WarnonBadCertRecving", 1, RegistryValueKind.DWord);//
                            }

                            if (tag.EqualsIgnoreCase("chkESFE35"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
                                                  "HideFileExt", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkMWCMDT37"))
                            {
                                int v = 0;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Default"))
                                {
                                    v = 7;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Blue"))
                                {
                                    v = 1;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Green"))
                                {
                                    v = 2;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Cyan"))
                                {
                                    v = 3;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Red"))
                                {
                                    v = 4;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Magenta"))
                                {
                                    v = 5;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Yellow/Brown"))
                                {
                                    v = 6;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Gray"))
                                {
                                    v = 8;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Bright Blue"))
                                {
                                    v = 9;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Bright Green"))
                                {
                                    v = 10;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Bright Cyan"))
                                {
                                    v = 11;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Bright Red"))
                                {
                                    v = 12;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Bright Magenta"))
                                {
                                    v = 13;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Bright Yellow"))
                                {
                                    v = 14;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("White"))
                                {
                                    v = 15;
                                }

                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Command Processor", "DefaultColor",
                                                  v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkMONFON36"))
                            {
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes", ".nfo");
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes", "nfofile");
                                cReg.CreateRegKey(Registry.LocalMachine, "SOFTWARE\\Classes\\.nfo");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\.nfo", "", "txtfile");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\.nfo", "Content Type", "text/plain");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\.nfo", "PerceivedType", "text");
                                cReg.CreateRegKey(Registry.LocalMachine, "SOFTWARE\\Classes\\.nfo\\PersistentHandler");
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Classes\\.nfo\\PersistentHandler", "",
                                                  "{5e941d80-bf96-11cd-b579-08002b30bfeb}");
                            }

                            if (tag.EqualsIgnoreCase("chkESUAAVIF30"))
                            {
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Classes\\CLSID",
                                                    "{87D62D94-71B3-4b9a-9489-5FE6850DC73E}");
                                cReg.DeleteKey(Registry.LocalMachine,
                                                    "SOFTWARE\\Classes\\SystemFileAssociations\\.avi\\shellex", "PropertyHandler");
                            }

                            if (tag.EqualsIgnoreCase("chKCMAUMSI37"))
                            {
                                cReg.CreateRegKey(Registry.LocalMachine,
                                                         "Software\\Classes\\Msi.Package\\shell\\Unpack\\command");
                                cReg.WriteValue(Registry.LocalMachine, "Software\\Classes\\Msi.Package\\shell\\Unpack\\command",
                                                  "", "msiexec.exe /a \"%1\" /qb TARGETDIR=\"%1 Content\"");
                            }

                            if (tag.EqualsIgnoreCase("chkDRACISY39"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer",
                                                  "HideSCAHealth", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkFTOSF41"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SYSTEM\\ControlSet001\\services\\SharedAccess\\Parameters\\FirewallPolicy\\StandardProfile",
                                                  "EnableFirewall", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkFTODF42"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SYSTEM\\ControlSet001\\services\\SharedAccess\\Parameters\\FirewallPolicy\\DomainProfile",
                                                  "EnableFirewall", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkFTOPF43"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SYSTEM\\ControlSet001\\services\\SharedAccess\\Parameters\\FirewallPolicy\\PublicProfile",
                                                  "EnableFirewall", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIERSB44"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Policies\\Microsoft\\Internet Explorer\\InfoDelivery\\Restrictions",
                                                  "NoSearchBox", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIEASMB45"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "SOFTWARE\\Microsoft\\Internet Explorer\\Toolbar\\WebBrowser", "ITBar7Position", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser, "SOFTWARE\\Microsoft\\Internet Explorer\\MINIE", "AlwaysShowMenus", 1, RegistryValueKind.DWord);

                            }

                            if (tag.EqualsIgnoreCase("chkIEHP46"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "SOFTWARE\\Microsoft\\Internet Explorer\\Main",
                                                  "Start Page", I.Nodes[0].Text);
                            }

                            if (tag.EqualsIgnoreCase("chkELFWISP46"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
                                                  "SeparateProcess", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkMWWN47"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Notepad", "fWrap", 1,
                                                  RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkEASINT48"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
                                                  "IconsOnly", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkCMATMTCM45"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "MUIVerb",
                                                  "Windows");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "SubCommands", "");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "Icon",
                                                  "%SystemRoot%\\\\System32\\\\winver.exe");

                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\TaskMan",
                                                  "MUIVerb", "Task Manager");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\TaskMan",
                                                  "Icon", "taskmgr.exe");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\TaskMan",
                                                  "SuppressionPolicy", 1073741884, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\TaskMan\\command",
                                                  "", "taskmgr", RegistryValueKind.ExpandString);
                            }

                            if (tag.EqualsIgnoreCase("chkCMAPFDCM47"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "MUIVerb",
                                                  "Windows");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "SubCommands", "");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows", "Icon",
                                                  "%SystemRoot%\\\\System32\\\\winver.exe");

                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\ProgFeat",
                                                  "MUIVerb", "Programs && Features");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\ProgFeat",
                                                  "Icon", "OptionalFeatures.exe");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\ProgFeat",
                                                  "SuppressionPolicy", 1073741884, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Classes\\Directory\\Background\\shell\\Windows\\shell\\ProgFeat\\command",
                                                  "", "control appwiz.cpl", RegistryValueKind.ExpandString);
                            }

                            if (tag.EqualsIgnoreCase("chkSISUBSDS48"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "SYSTEM\\ControlSet001\\Control\\usbstor\\VVVVPPPP",
                                                  "MaximumTransferLength", 2097120, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkCEBC1"))
                            {
                                cReg.RegSetOwnership(Registry.LocalMachine,
                                                            "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FolderTypes\\{5c4f28b5-f869-4e84-8e60-f11db97c5cc7}");

                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FolderTypes\\{5c4f28b5-f869-4e84-8e60-f11db97c5cc7}\\TasksItemsSelected",
                                                  "",
                                                  "Windows.Cut; Windows.Copy; Windows.Delete; Windows.rename; Windows.properties; Windows.closewindow");
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FolderTypes\\{5c4f28b5-f869-4e84-8e60-f11db97c5cc7}\\TasksNoItemsSelected",
                                                  "",
                                                  "Windows.selectall; Windows.Paste; Windows.undo; Windows.redo; Windows.menubar; Windows.previewpane; Windows.readingpane; Windows.navpane; Windows.closewindow");
                            }
                            if (tag.EqualsIgnoreCase("chkEAETCF49"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
                                                  "NavPaneExpandToCurrentFolder", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIEDIECD51"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Policies\\Microsoft\\Internet Explorer\\Restrictions",
                                                  "NoCrashDetection", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIECTOIEW52"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer\\Main",
                                                  I.Nodes[0].Text, 1);
                            }
                            if (tag.EqualsIgnoreCase("chkIECDDL53"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer",
                                                  "Download Directory", I.Nodes[0].Text);
                            }
                            if (tag.EqualsIgnoreCase("chkIEDSWCF54"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer\\Download",
                                                  "RunInvalidSignatures", 0, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer\\Download",
                                                  "CheckExeSignatures", "No");
                            }
                            if (tag.EqualsIgnoreCase("chkIETOCT55"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer\\Main",
                                                  "UseClearType", 1, RegistryValueKind.DWord);
                            }
                            if (tag.EqualsIgnoreCase("chkIEFSMD56"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer\\Main",
                                                  "FullScreen", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIETORRSF57"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer\\Feed Discovery",
                                                  "Enabled", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIERDP58"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\BrowseNewProcess",
                                                  "BrowseNewProcess", "Yes");
                            }

                            if (tag.EqualsIgnoreCase("chkIECFU59"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer\\Main",
                                                  "NoUpdateCheck", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIEDPCIE60"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings",
                                                  "DisablePasswordCaching", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIEDWOCT62"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer\\TabbedBrowsing",
                                                  "WarnOnClose", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIEWAWTO63"))
                            {
                                int v = 0;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("A Blank Page"))
                                {
                                    v = 0;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("First Homepage"))
                                {
                                    v = 1;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("'New Tab' Page"))
                                {
                                    v = 2;
                                }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer\\TabbedBrowsing",
                                                  "NewTabPageShow", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIEWPUE64 "))
                            {
                                int v = 0;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Let IE Decide"))
                                {
                                    v = 0;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("New Window"))
                                {
                                    v = 1;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("New Tab"))
                                {
                                    v = 2;
                                }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer\\TabbedBrowsing",
                                                  "PopupsUseNewWindow", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkAWNTWC65"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer\\TabbedBrowsing",
                                                  "OpenInForeground", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIEGPUSR66"))
                            {
                                int v = 0;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Software Rendering")) { v = 1; }

                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer\\GPU", "SoftwareFallback", v, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer\\GPU", "Wow64-SoftwareFallback", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSSDSS65"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Authentication\\LogonUI\\BootAnimation",
                                                  "DisableStartupSound", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSIFSMCS67"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet001\\Control\\FileSystem",
                                                  "NtfsMemoryUsage", 2, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet002\\Control\\FileSystem",
                                                  "NtfsMemoryUsage", 2, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSEMACFS67"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet001\\Control\\Lsa", "LmCompatibilityLevel", 0, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet002\\Control\\Lsa", "LmCompatibilityLevel", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkESDLF68"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer",
                                                  "ShowDriveLettersFirst", 4, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSDNTFSE68"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet001\\Control\\FileSystem",
                                                  "NtfsDisableEncryption", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet002\\Control\\FileSystem",
                                                  "NtfsDisableEncryption", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSDBL69"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Policies\\Microsoft\\Windows\\Psched",
                                                  "NonBestEfforLimit", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSDAW7TDD70"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet001\\services\\USB",
                                                  "DisableSelectiveSuspend", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet002\\services\\USB",
                                                  "DisableSelectiveSuspend", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIEWAWTO63"))
                            {
                                int v = 38;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Programs"))
                                {
                                    v = 38;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Background Services"))
                                {
                                    v = 24;
                                }
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet001\\Control\\PriorityControl",
                                                  "Win32PrioritySeparation", v, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine, "System\\ControlSet002\\Control\\PriorityControl",
                                                  "Win32PrioritySeparation", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIEWAWTO63"))
                            {
                                cReg.DeleteValue(Registry.LocalMachine,
                                                      "System\\ControlSet001\\Control\\Session Manager\\Memory Management",
                                                      "PagingFiles");
                                cReg.DeleteValue(Registry.LocalMachine, "System\\ControlSet002\\Control\\PriorityControl",
                                                      "PagingFiles");
                            }

                            if (tag.EqualsIgnoreCase("chkSDAS73"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet001\\services\\LanmanServer\\Parameters", "AutoShareWks", 0,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet002\\services\\LanmanServer\\Parameters", "AutoShareWks", 0,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet001\\services\\LanmanServer\\Parameters", "AutoShareServer",
                                                  0, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "System\\ControlSet002\\services\\LanmanServer\\Parameters", "AutoShareServer",
                                                  0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkESAIOT43"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer",
                                                  "EnableAutoTray", 0, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSDCPUP72"))
                            {
                                if (
                                     !string.IsNullOrEmpty(cReg.GetValue(Registry.LocalMachine,
                                                                                     "System\\ControlSet001\\Control\\Power\\PowerSettings\\54533251-82be-4824-96c1-47b60b740d00\\0cc5b647-c1df-4637-891a-dec35c318583",
                                                                                     "ValueMax")))
                                {
                                    cReg.WriteValue(Registry.LocalMachine,
                                                      "System\\ControlSet001\\Control\\Power\\PowerSettings\\54533251-82be-4824-96c1-47b60b740d00\\0cc5b647-c1df-4637-891a-dec35c318583",
                                                      "ValueMax", 0, RegistryValueKind.DWord);
                                    cReg.WriteValue(Registry.LocalMachine,
                                                      "System\\ControlSet002\\Control\\Power\\PowerSettings\\54533251-82be-4824-96c1-47b60b740d00\\0cc5b647-c1df-4637-891a-dec35c318583",
                                                      "ValueMax", 0, RegistryValueKind.DWord);
                                }
                            }

                            if (tag.EqualsIgnoreCase("chkDSTB68"))
                            {
                                //
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", "TaskbarSmallIcons", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkSDASSL60"))
                            {
                                int v = 0;
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Mute all other sounds"))
                                {
                                    v = 0;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Reduce all other by 80%"))
                                {
                                    v = 1;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Reduce all other by 50%"))
                                {
                                    v = 2;
                                }
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Do nothing"))
                                {
                                    v = 3;
                                }
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Multimedia\\Audio",
                                                  "UserDuckingPreference", v, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkMCRO70"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion",
                                                  "RegisteredOwner", I.Nodes[0].Text);
                            }

                            if (tag.EqualsIgnoreCase("chkMCRO71"))
                            {
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion",
                                                  "RegisteredOrganization", I.Nodes[0].Text);
                            }

                            if (tag.EqualsIgnoreCase("chkMSNAV65"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "SOFTWARE\\Microsoft\\Notepad", "StatusBar", 1,
                                                  RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIESIESB66"))
                            {
                                string v = "yes";
                                if (I.Nodes[0].Text.EqualsIgnoreCase("Hide Status Bar")) { v = "no"; }
                                cReg.WriteValue(Registry.CurrentUser, "SOFTWARE\\Microsoft\\Internet Explorer\\Main", "Show_StatusBar", v);
                                cReg.WriteValue(Registry.CurrentUser, "SOFTWARE\\Microsoft\\Internet Explorer\\Main", "Show_URLinStatusBar", v);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Internet Explorer\\Main", "Show_StatusBar", v);
                                cReg.WriteValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Internet Explorer\\Main", "Show_URLinStatusBar", v);
                            }

                            if (tag.EqualsIgnoreCase("chkESESB67"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "SOFTWARE\\Microsoft\\Internet Explorer\\Main",
                                                  "StatusBarOther", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser, "SOFTWARE\\Microsoft\\Internet Explorer\\MINIE",
                                                  "StatusBarOther", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIESGSP71"))
                            {
                                //Sert default RunOnce
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce",
                                                  "WTK_IE_Google_Search", "REG ADD HKCU\\Software\\Microsoft\\Internet Explorer\\SearchScopes /v DefaultScope /t REG_SZ /d {637D6E3C-DF93-48A5-8362-159A8AC56B11} /f");

                                //Set default
                                cReg.WriteValue(Registry.LocalMachine,
                                                 "Software\\Microsoft\\Internet Explorer\\SearchScopes",
                                                 "DefaultScope", "{637D6E3C-DF93-48A5-8362-159A8AC56B11}");

                                cReg.WriteValue(Registry.CurrentUser,
                                             "Software\\Microsoft\\Internet Explorer\\SearchScopes",
                                             "DefaultScope", "{637D6E3C-DF93-48A5-8362-159A8AC56B11}");
                                
                                //Add Google
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Microsoft\\Internet Explorer\\SearchScopes\\{637D6E3C-DF93-48A5-8362-159A8AC56B11}",
                                                  "DisplayName", "Google");
                                cReg.WriteValue(Registry.CurrentUser, "SOFTWARE\\Microsoft\\Internet Explorer\\SearchScopes\\{637D6E3C-DF93-48A5-8362-159A8AC56B11}",
                                    "URL", "http://www.google.com/search?hl=en&q={searchTerms}&meta=");
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Microsoft\\Internet Explorer\\SearchScopes\\{637D6E3C-DF93-48A5-8362-159A8AC56B11}",
                                                  "ShowSearchSuggestions", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Microsoft\\Internet Explorer\\SearchScopes\\{637D6E3C-DF93-48A5-8362-159A8AC56B11}",
                                                  "SuggestionsURL",
                                                  "http://clients5.google.com/complete/search?hl={language}&q={searchTerms}&client=ie8&inputencoding={inputEncoding}&outputencoding={outputEncoding}");
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Microsoft\\Internet Explorer\\SearchScopes\\{637D6E3C-DF93-48A5-8362-159A8AC56B11}",
                                                  "OSDFileURL", "http://www.iegallery.com/DownloadHandler.ashx?ResourceId=13438");
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Microsoft\\Internet Explorer\\SearchScopes\\{637D6E3C-DF93-48A5-8362-159A8AC56B11}",
                                                  "FaviconURL", "http://www.google.com/favicon.ico");
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Microsoft\\Internet Explorer\\SearchScopes\\{637D6E3C-DF93-48A5-8362-159A8AC56B11}",
                                                  "ShowTopResult", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Microsoft\\Internet Explorer\\SearchScopes\\{637D6E3C-DF93-48A5-8362-159A8AC56B11}",
                                                  "SortIndex", 1, RegistryValueKind.DWord);

                                cReg.WriteValue(Registry.CurrentUser,
                                                 "SOFTWARE\\Microsoft\\Internet Explorer\\SearchScopes\\{637D6E3C-DF93-48A5-8362-159A8AC56B11}",
                                                 "FaviconPath", "%HomePath%\\AppData\\LocalLow\\Microsoft\\Internet Explorer\\Services\\search_{637D6E3C-DF93-48A5-8362-159A8AC56B11}.ico", RegistryValueKind.DWord);

                                cReg.WriteValue(Registry.CurrentUser,
                                                 "SOFTWARE\\Microsoft\\Internet Explorer\\SearchScopes\\{637D6E3C-DF93-48A5-8362-159A8AC56B11}",
                                                 "FaviconURLFallback", "http://www.google.com/favicon.ico");

                                cReg.WriteValue(Registry.CurrentUser,
                                                "SOFTWARE\\Microsoft\\Internet Explorer\\SearchScopes\\{637D6E3C-DF93-48A5-8362-159A8AC56B11}",
                                                "FaviconURLFallback", "http://www.google.com/favicon.ico");

                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Internet Explorer\\SearchScopes\\{637D6E3C-DF93-48A5-8362-159A8AC56B11}",
                                                  "TopResultURLFallback", "");

                                cReg.WriteValue(Registry.CurrentUser,
                                                "Software\\Microsoft\\Internet Explorer\\SearchScopes\\{637D6E3C-DF93-48A5-8362-159A8AC56B11}",
                                                "SuggestionsURLFallback", "http://clients5.google.com/complete/search?hl={language}&q={searchTerms}&client=ie8&inputencoding={inputEncoding}&outputencoding={outputEncoding}");

                            }
                            if (tag.EqualsIgnoreCase("chkIEDDB72"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "SOFTWARE\\Microsoft\\Internet Explorer\\Main",
                                                  "Check_Associations", "no");
                            }
                            if (tag.EqualsIgnoreCase("chkIEDFRC73"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "SOFTWARE\\Policies\\Microsoft\\Internet Explorer\\Main",
                                                  "DisableFirstRunCustomize", 1, RegistryValueKind.DWord);
                                cReg.DeleteValue(Registry.LocalMachine, "SOFTWARE\\Policies\\Microsoft\\Internet Explorer\\Main",
                                                  "DisableFirstRunCustomize");
                            }
                            if (tag.EqualsIgnoreCase("chkIECHOE74"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Policies\\Microsoft\\Internet Explorer\\Privacy",
                                                  "ClearBrowsingHistoryOnExit", 1, RegistryValueKind.DWord);
                            }
                            if (tag.EqualsIgnoreCase("chkIEDSS75"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Policies\\Microsoft\\Internet Explorer\\Suggested Sites", "Enabled",
                                                  1, RegistryValueKind.DWord);
                            }
                            if (tag.EqualsIgnoreCase("chkIERZL75"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "SOFTWARE\\Policies\\Microsoft\\Internet Explorer\\Zoom",
                                                  "ResetZoomOnStartup2", 2, RegistryValueKind.DWord);
                            }
                            if (tag.EqualsIgnoreCase("chkIEECB76"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Policies\\Microsoft\\Internet Explorer\\CaretBrowsing",
                                                  "EnableOnStartup", 1, RegistryValueKind.DWord);
                            }
                            if (tag.EqualsIgnoreCase("chkIERLB77"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Policies\\Microsoft\\Internet Explorer\\Toolbars\\Restrictions",
                                                  "NoLinksbar", 1, RegistryValueKind.DWord);
                            }
                            if (tag.EqualsIgnoreCase("chkIERFM78"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "SOFTWARE\\Policies\\Microsoft\\Internet Explorer\\Restrictions",
                                                  "NoFavorites", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Policies\\Microsoft\\Internet Explorer\\Restrictions",
                                                  "NoFavorites", 1, RegistryValueKind.DWord);
                            }
                            if (tag.EqualsIgnoreCase("chkIEESEPTD79"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings",
                                                  "DisableCashingOfSSLPages", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkDDEPP81"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Control Panel\\Mouse", "MouseSpeed", 0,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser, "Control Panel\\Mouse", "MouseThreshold1", 0,
                                                  RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.CurrentUser, "Control Panel\\Mouse", "MouseThreshold2", 0,
                                                  RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkDDRN79"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer",
                                                  "NoRecycleFiles", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkDHRB80"))
                            {
                                cReg.WriteValue(Registry.CurrentUser,
                                                  "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\NonEnum",
                                                  "{645FF040-5081-101B-9F08-00AA002F954E}", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIERTS82"))
                            {
                                cReg.WriteValue(Registry.CurrentUser, "Software\\Microsoft\\Internet Explorer\\Zoom",
                                                  "ResetTextSizeOnStartup", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIEDA83"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "Software\\Policies\\Microsoft\\Internet Explorer\\Services",
                                                  "SelectionActivityButtonDisable", 1, RegistryValueKind.DWord);
                            }

                            if (tag.EqualsIgnoreCase("chkIEETPC84"))
                            {
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Policies\\Microsoft\\Internet Explorer\\Infodelivery\\Restrictions",
                                                  "NoWebJITSetup", 1, RegistryValueKind.DWord);
                                cReg.WriteValue(Registry.LocalMachine,
                                                  "SOFTWARE\\Policies\\Microsoft\\Internet Explorer\\Infodelivery\\Restrictions",
                                                  "NoJITSetup", 1, RegistryValueKind.DWord);
                            }

                            //
                        }
                        catch (Exception ex)
                        {
                            //WriteLogA(I.Text + " - -" + ex.Message);
                        }
                        cMain.FreeRAM();

                        try
                        {
                            if (PB != null && F != null && PB.Value < PB.Maximum)
                            {
                                PB.Value += 1;
                                Windows7Taskbar.SetProgressValue(F.Handle, Convert.ToUInt32(PB.Value), Convert.ToUInt32(PB.Maximum));
                            }
                        }
                        catch { }

                        if (cAddon.AddonCancel)
                            break;
                        t += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + lbl.Text);
            }
            cReg.WIMt = false;
        }

        public void GetCInfo(TreeNode TN, Label Title, Label Desc)
        {
            Title.Text = TN.Text;
            Desc.Text = string.IsNullOrEmpty(TN.ToolTipText) ? "There is no further information on this component." : TN.ToolTipText;
        }
    }

}