using Microsoft.Win32.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;

namespace WinToolkit
{
    public static class cMain
    {
        #region EXECUTION_STATE enum

        [Flags]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        #endregion

        //Normal
        private const int WM_CLOSE = 0x10;

        public static string Root = Application.StartupPath + "\\";
        public static string SysProgFiles = Environment.GetEnvironmentVariable("ProgramFiles");
        public static string SysFolder = Environment.GetEnvironmentVariable("SystemRoot") + "\\System32";
        public static string SysRoot = Environment.GetEnvironmentVariable("SystemRoot");
        public static string SysDrive = Environment.GetEnvironmentVariable("SystemDrive");

        public static string WinToolkitVersion(bool Full = false)
        {
            int iMajor = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major;
            int iMinor = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor;
            int iBuild = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build;
            int iRevision = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision;

            if (Full)
            {
                return iMajor + "." + iMinor + "." + iBuild + "." + iRevision;
            }
            return iMajor + "." + iMinor + "." + iBuild;

        }
        public static string DefaultLang = Thread.CurrentThread.CurrentUICulture.ToString();
        public static string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static string UserTempPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\WinToolkit_Temp";
        private static string _ErrLogSavePath = cMain.Root + "Logs\\";

        public static string ErrLogSavePath
        {
            get
            {
                if (!Directory.Exists(_ErrLogSavePath))
                {
                    Directory.CreateDirectory(_ErrLogSavePath);
                }
                return _ErrLogSavePath;
            }
            set { _ErrLogSavePath = value; }
        }

        public static int openForms = 0;
        public static int AppErrC, SWT;
        public static bool Arc64, wimRebuild, tCancel, AVShown, DismShown;
        public static List<WIMImage> selectedImages = new List<WIMImage>();
        public static List<string> Dirs = new List<string>();
        public static string AppErrM, UnattendedF = "";
        public static string sList = "";
        public static string eErr = "";
        public static string tmLast = null;
        public static List<Form> FormHistory = new List<Form>();
        public static List<string> StatusHistory = new List<string>();
        public static string TweakChoice = "";

        //Close exe2cab
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        //FreeRAM
        [DllImport("psapi.dll")]
        private static extern int EmptyWorkingSet(IntPtr hwProc);

        //Disable Sleep

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        //END Disable Sleep Mode

        /// <summary>
        /// Returns the extension of a filename
        /// </summary>
        /// <param name="FilePath">The file you wish to retrieve the filename of.</param>
        /// <returns></returns>
        public static string GetExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        public static void PreventSleep(bool PreventSleep = true)
        {
            try
            {
                if (PreventSleep)
                {
                    SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED |
                                                    EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
                }
                else
                {
                    SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
                }
            }
            catch { }
        }


        public static void SetToolTip(Control item, string Tooltip, string Title = "", ToolTipIcon TTI = ToolTipIcon.Info)
        {
            try
            {
                string T = Title;
                if (string.IsNullOrEmpty(T))
                {
                    T = item.Text;
                }
                T = T.ReplaceIgnoreCase("&", "");
                T = T.ReplaceIgnoreCase(Environment.NewLine, " ");
                T = T.ReplaceIgnoreCase("  ", " ");

                var toolTip1 = new ToolTip();

                toolTip1.SetToolTip(item, null);
                //toolTip1.AutomaticDelay = 1000;
                toolTip1.InitialDelay = 500;
                toolTip1.ReshowDelay = 750;
                toolTip1.AutoPopDelay = 30000;
                toolTip1.ToolTipTitle = T;

                toolTip1.ToolTipIcon = TTI;
                toolTip1.IsBalloon = true;

                toolTip1.SetToolTip(item, Tooltip);

            }
            catch
            {
            }
        }

        public static void KillProcess(string Name)
        {
            Process[] ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                try
                {
                    if (String.Equals(p.ProcessName, Name, StringComparison.CurrentCultureIgnoreCase) && p.HasExited == false)
                    {
                        p.Kill();
                    }
                }
                catch { }
            }

        }

        public static bool IsApplicationAlreadyRunning(string Desc, int Count = 0)
        {
            int c = Process.GetProcessesByName(Desc).Count();

            if (c > Count)
                return true;

            return false;
        }

        public enum UpdateAvailable
        {
            Yes,
            No,
            Offline,
            Error
        }

        public static UpdateAvailable CheckForUpdates()
        {
            UpdateAvailable nUpdate = UpdateAvailable.No;
            cOptions.NF = "";
            cOptions.NV = "";

            if (string.IsNullOrEmpty(cReg.GetValue(Registry.LocalMachine, "Software\\Microsoft\\.NETFramework", "LegacyWPADSupport")))
            {
                cReg.WriteValue(Registry.LocalMachine, "Software\\Microsoft\\.NETFramework", "LegacyWPADSupport", 0,
                    RegistryValueKind.DWord);
                if (Arc64)
                {
                    cReg.WriteValue(Registry.LocalMachine, "Software\\Wow6432Node\\Microsoft\\.NETFramework",
                        "LegacyWPADSupport", 0, RegistryValueKind.DWord);
                }
            }
            try
            {
                string strResponse = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://update.wintoolkit.co.uk/index.htm");
                request.UserAgent = "Win Toolkit_135792468";
                request.Accept = "text/html";
                request.Proxy = WebRequest.DefaultWebProxy;
                request.Timeout = 5000;
                request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream resStream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(resStream, Encoding.GetEncoding(response.CharacterSet));
                    strResponse = streamReader.ReadToEnd();
                }

                if (!string.IsNullOrEmpty(strResponse))
                {
                    string NF = "";
                    int intT = 0;
                    foreach (string L in strResponse.Split(Environment.NewLine.ToCharArray()))
                    {
                        if (!string.IsNullOrEmpty(L))
                        {
                            if (L.StartsWithIgnoreCase("|"))
                            {
                                cOptions.NM = L.Substring(1);
                                intT -= 1;
                            }
                            if (L.StartsWithIgnoreCase("#"))
                            {
                                cOptions.NL = L.Substring(1);
                                intT -= 1;
                            }
                            if (string.IsNullOrEmpty(cOptions.NV))
                            {
                                cOptions.NV = L;
                            }
                            else
                            {
                                NF += Environment.NewLine + L;
                                intT += 1;
                            }
                        }
                    }

                    while (!NF.StartsWithIgnoreCase("*"))
                    {
                        NF = NF.Substring(1);
                    }

                    var newVersion = new Version(cOptions.NV);
                    var currentVersion = new Version(Application.ProductVersion);
                    if (newVersion > currentVersion) { cOptions.NF = NF; return UpdateAvailable.Yes; }

                }

                FreeRAM();
            }
            catch (Exception Ex)
            {
                cOptions.NV = ""; nUpdate = UpdateAvailable.Error;
            }

            return nUpdate;
        }

        public static string GetDVD(string WIM)
        {
            string DVD = WIM;

            if (DVD.ContainsIgnoreCase("\\SOURCES\\"))
            {
                try
                {
                    while (!DVD.EndsWithIgnoreCase("\\"))
                    {
                        DVD = DVD.Substring(0, DVD.Length - 1);
                    }
                    DVD = DVD.Substring(0, DVD.Length - 1);

                    while (!DVD.EndsWithIgnoreCase("\\"))
                    {
                        DVD = DVD.Substring(0, DVD.Length - 1);
                    }
                }
                catch
                {
                }
            }
            return DVD;
        }
        public static void CleanExit(Form F)
        {

            try
            {

                F.Text = "Cleaning...";
                Application.DoEvents();
                Files.DeleteFolder(Root + "AddonT", false);

                foreach (string sFile in Directory.GetFiles(cMain.UserTempPath, "*", SearchOption.TopDirectoryOnly))
                {
                    Files.DeleteFile(sFile);
                }

                Files.DeleteFile(Root + "Tasks.txt");
                Files.DeleteFile(Root + "Imagex.exe");
                Files.DeleteFile(Root + "boot.bin");
                Files.DeleteFile(Root + "BIOS.com");
                Files.DeleteFile(Root + "UEFI.bin");
                Files.DeleteFile(Root + "cdimage.exe");
                Files.DeleteFile(Root + "7z.dll");
                Files.DeleteFile(Root + "7z.exe");
                Files.DeleteFile(Root + "SevenZipSharp.dll");
                Files.DeleteFile(Root + "UTH.exe");
                Files.DeleteFile(Root + "intlcfg.exe");
                Files.DeleteFile(Root + "ResHacker.exe");
                Files.DeleteFile(Root + "ResHacker.ini");
                Files.DeleteFile(Root + "ResHacker.log");
                Files.DeleteFile(Root + "exe2cab.exe");
                Files.DeleteFile(Root + "wimfltr.inf");
                Files.DeleteFile(Root + "Interop.IWshRuntimeLibrary.dll");
                Files.DeleteFile(Root + "Interop.SHDocVw.dll");
                Files.DeleteFile(Root + "WinToolkit.exe.config");
                Files.DeleteFile(Root + "WinToolkit.vshost.exe.config");
                Files.DeleteFile(Root + "WinToolkit.vshost.exe.manifest");
                Files.DeleteFile(Root + "WinToolkit.pdb");
                Files.DeleteFile(Root + "WinToolkit.vshost.exe");
                Files.DeleteFile(Root + "Vista Api.dll");
                Files.DeleteFile(Root + "Donate.htm");

                FreeRAM();

                F.Text = "Cleaning Win Toolkit Temp Folder...";
                Application.DoEvents();
                try
                {
                    if (Directory.Exists(cOptions.WinToolkitTemp))
                    {
                        foreach (string S in Directory.GetFiles(cOptions.WinToolkitTemp, "*", SearchOption.TopDirectoryOnly))
                        {
                            if (S.ContainsIgnoreCase("\\ScratchDir_")) { continue; }
                            F.Text = "Cleaning Win Toolkit Temp Folder (" + S + ")...";
                            Application.DoEvents();
                            Files.DeleteFile(S);
                        }

                        bool FoundMounted = false;
                        foreach (string S in Directory.GetDirectories(cOptions.WinToolkitTemp, "*", SearchOption.TopDirectoryOnly))
                        {
                            bool M = false;
                            if (cReg.RegCheckMounted("SOFTWARE\\Microsoft\\WIMMount\\Mounted Images\\"))
                            {
                                foreach (var RK in cReg.RegCheckMountDism())
                                {
                                    if (String.Equals(S, RK.MountPath, StringComparison.CurrentCultureIgnoreCase) || RK.MountPath.StartsWithIgnoreCase(S.ToUpper()))
                                    {
                                        M = true;
                                        FoundMounted = true;
                                    }
                                }
                            }

                            if (M == false && !Directory.Exists(S + "\\Windows\\"))
                            {
                                F.Text = "Cleaning Win Toolkit Temp Folder (" + S + ")...";
                                Application.DoEvents();
                                Files.DeleteFolder(S, false);
                            }
                        }
                        if (FoundMounted == false)
                        {
                            if (IsEmpty(cOptions.WinToolkitTemp))
                            {
                                Files.DeleteFolder(cOptions.WinToolkitTemp, false);
                            }
                        }
                        // if (!Directory.Exists(cOptions.WinToolkitTemp + "\\Mount") && !Directory.Exists(cOptions.WinToolkitTemp + "\\Boot")) { DeleteFolder(cOptions.WinToolkitTemp, false); }
                    }
                }
                catch
                {
                }
            }
            catch
            {
            }
        }

        public static void CenterObject(Control O)
        {
            if (O.Parent != null)
            {
                O.Top = (O.Parent.Height - O.Height) / 2;
                O.Left = (O.Parent.Width - O.Width) / 2;
            }
            else
            {
                O.Top = (Screen.PrimaryScreen.WorkingArea.Height - O.Height) / 2;
                O.Left = (Screen.PrimaryScreen.WorkingArea.Width - O.Width) / 2;
            }
        }

        public static bool IsFileReadLocked(string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                {
                    return !fs.CanRead;
                }
            }
            catch (IOException ex)
            {
                return true;
            }
        }

        public static bool IsFileLocked(string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                {
                    return !fs.CanWrite;
                }
            }
            catch (IOException ex)
            {
                return true;
            }
        }

        public static string FolderBrowserVista(string Description, bool ShowNewFolder, bool ShowDescAsTitle, string SelectedPath = "")
        {
            string Folder = "";
            string pFolder = Desktop;
            if (!string.IsNullOrEmpty(cOptions.LastDir) && Directory.Exists(cOptions.LastDir)) { pFolder = cOptions.LastDir; }
            if (!string.IsNullOrEmpty(SelectedPath) && Directory.Exists(SelectedPath)) { pFolder = SelectedPath; }
            if (!pFolder.EndsWithIgnoreCase("\\")) { pFolder += "\\"; }

            bool Backup = false;
            try
            {
                Folder = FBVista(Description, ShowNewFolder, ShowDescAsTitle, pFolder);

            }
            catch (Exception E)
            {
                Backup = true;
            }

            if (Backup)
            {
                var FBD = new FolderBrowserDialog();
                FBD.Description = Description;
                FBD.ShowNewFolderButton = ShowNewFolder;
                FBD.SelectedPath = pFolder;
                if (FBD.ShowDialog() == DialogResult.OK)
                {
                    Folder = FBD.SelectedPath;
                    cOptions.LastDir = Folder;
                }
            }
            return Folder;
        }

        public static string FBVista(string Description, bool ShowNewFolder, bool ShowDescAsTitle, string SelectedPath = "")
        {
            string Folder = "";
            try
            {
                using (var vFBD = new Vista_Api.FolderBrowserDialog())
                {
                    vFBD.Description = Description;
                    vFBD.ShowNewFolderButton = ShowNewFolder;
                    vFBD.SelectedPath = SelectedPath;
                    vFBD.UseDescriptionForTitle = ShowDescAsTitle;
                    if (vFBD.ShowDialog() != DialogResult.OK)
                    {
                        Folder = "";
                    }
                    else
                    {
                        Folder = vFBD.SelectedPath;
                        cOptions.LastDir = Folder;
                    }
                }
            }
            catch (Exception Ex)
            {
            }

            return Folder;
        }


        public static void FormActivated(object sender, EventArgs e)
        {
            FreeRAM();
        }

        public static void FormIcon(Form F)
        {
            try
            {
                if (F.FormBorderStyle == FormBorderStyle.Sizable)
                {

                    foreach (string N in cOptions.FormSize.Split('|'))
                    {
                        if (N.ContainsIgnoreCase(F.Name))
                        {
                            string nForm = N;

                            int scWidth = Screen.PrimaryScreen.WorkingArea.Width;
                            int scHeight = Screen.PrimaryScreen.WorkingArea.Height;
                            while (nForm.ContainsIgnoreCase(":")) { nForm = nForm.Substring(1); }
                            string sHeight = nForm;
                            while (sHeight.ContainsIgnoreCase("*")) { sHeight = sHeight.Substring(1); }
                            int nHeight = Convert.ToInt16(sHeight);
                            int nWidth = Convert.ToInt16(nForm.Substring(0, nForm.Length - (sHeight.Length + 1)));

                            if (nWidth >= scWidth && nHeight >= scHeight)
                            {
                                F.WindowState = FormWindowState.Maximized;
                            }
                            else
                            {
                                if (nWidth > scWidth) { nWidth = scWidth; }
                                if (nHeight > scHeight) { nHeight = scHeight; }
                                F.Width = nWidth;
                                F.Height = nHeight;
                            }

                            CenterObject(F);

                        }
                    }


                }
            }
            catch (Exception Ex)
            {
                new SmallError("Could not set form size", Ex, cOptions.FormSize).Upload();
            }


            FormHistory.Add(F);


            if (F.Opacity < 0.4) { F.Opacity = 0.4; }
            try
            {
                F.Icon = Properties.Resources.W7T_128;
            }
            catch { }

            var ns = F.GetType().Namespace;
            if (ns != null && !ns.EndsWithIgnoreCase("Prompts"))
            {
                try
                {
                    if (cOptions.TransparencyAll && Environment.OSVersion.Version.Major >= 6)
                    {
                        F.Opacity = cOptions.Transparency / 100;
                    }
                }
                catch { }
                StatusHistory.Clear();
            }

            FreeRAM();
        }

        /// <summary>
        /// Gets the date that Windows was installed on the local machine.
        /// </summary>
        /// <returns></returns>
        public static DateTime GetWindowsInstallationDateTime()
        {
            DateTime startDate = new DateTime(1970, 1, 1, 0, 0, 0);
            string val = cReg.GetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "InstallDate");

            Int64 regVal = Convert.ToInt64(val);
            DateTime installDate = startDate.AddSeconds(regVal);

            return installDate;
        }

        public static DateTime GetNistTime()
        {
            DateTime dateTime = DateTime.MinValue;

            try
            {
                var client = new System.Net.Sockets.TcpClient("time.nist.gov", 13);
                client.ReceiveTimeout = 2000;

                using (var streamReader = new StreamReader(client.GetStream()))
                {
                    var response = streamReader.ReadToEnd();
                    if (response.Length >= 7)
                    {
                        var utcDateTimeString = response.Substring(7, 17);
                        dateTime = DateTime.ParseExact(utcDateTimeString, "yy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                    }
                }
            }
            catch { }

            return dateTime;
        }

        /// <summary>
        /// Gets the latest date to try and prevent user changing
        /// the system date to overwrite preventions.
        /// </summary>
        /// <returns></returns>
        public static DateTime GetLatestDate()
        {
            DateTime _latestDate = DateTime.UtcNow;
            DateTime _internetDate = GetNistTime();
            DateTime _windowsDate = GetWindowsInstallationDateTime();
            DateTime _fileCreationDate = File.GetCreationTimeUtc(Application.ExecutablePath);
            DateTime _fileAccessedDate = File.GetLastAccessTimeUtc(Application.ExecutablePath);

            if (_windowsDate > _latestDate) { _latestDate = _windowsDate; }
            if (_internetDate > _latestDate) { _latestDate = _internetDate; }
            if (_fileCreationDate > _latestDate) { _latestDate = _fileCreationDate; }
            if (_fileAccessedDate > _latestDate) { _latestDate = _fileAccessedDate; }

            return _latestDate;
        }

        /// <summary>
        /// Detects the first drive big enough for the specified space requirement.
        /// </summary>
        /// <param name="DefaultDrive">The drive used if something goes wrong.</param>
        /// <param name="SpaceRequirement">How much space needed for the required task.</param>
        /// <returns></returns>
        public static string DetectLargestDrive(string DefaultDrive, Int64 SpaceRequirement)
        {
            long C = 0;
            string dName = DefaultDrive;
            try
            {
                DriveInfo[] drives = DriveInfo.GetDrives().Where(d => d.IsReady && d.DriveFormat.EqualsIgnoreCase("NTFS") && (d.DriveType == DriveType.Fixed || d.DriveType == DriveType.Ram)).ToArray();

                foreach (DriveInfo DI in drives)
                {
                    try
                    {
                        long lFreeSpace = DI.AvailableFreeSpace;

                        if (lFreeSpace > C && lFreeSpace >= SpaceRequirement)
                        {
                            dName = DI.Name;
                            C = lFreeSpace;
                        }
                    }
                    catch { }
                }
            }
            catch (Exception Ex)
            {
                new SmallError("Error trying to detect largest drive.", Ex).Upload();
            }
            return dName;
        }

        public static void WriteLog(Form F, string sBriefDescription, string EX, string sExtraData = "")
        {
            LegacyError LE = new LegacyError(F, sBriefDescription, sExtraData, EX);
            LE.Upload();
        }


        public static void OpenExplorer(Form F, string Folder)
        {
            try
            {
                var proc = new Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = "explorer.exe";
                proc.StartInfo.Arguments = "\"" + Folder + "\"";
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                proc.Start();
            }
            catch (Exception Ex)
            {
                LargeError LE = new LargeError("Explorer Error", "Error trying to open Windows Explorer folder.", Ex);
                LE.Upload(); LE.ShowDialog();
            }
        }

        public static void ClearAttributeFile(string FolFil)
        {
            try
            {
                if (!File.Exists(FolFil) && !Directory.Exists(FolFil)) { return; }

                FileAttributes attr = File.GetAttributes(FolFil);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(FolFil);
                    foreach (FileSystemInfo item in dirInfo.GetFileSystemInfos())
                    {
                        if (item is FileInfo)
                        {
                            File.SetAttributes(item.FullName, FileAttributes.Normal);
                        }
                        else if (item is DirectoryInfo)
                        {
                            ClearAttributeFile(item.FullName);
                        }
                    }
                }
                else
                {
                    File.SetAttributes(FolFil, FileAttributes.Normal);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void TakeOwnership(string F)
        {
            try
            {
                if (!File.Exists(F) && !Directory.Exists(F)) { return; }

                var myProcToken = new AccessTokenProcess(Process.GetCurrentProcess().Id,
                                                                      TokenAccessType.TOKEN_ALL_ACCESS |
                                                                      TokenAccessType.TOKEN_ADJUST_PRIVILEGES);
                myProcToken.EnablePrivilege(new TokenPrivilege(TokenPrivilege.SE_TAKE_OWNERSHIP_NAME, true));

                var sid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
                var account = (NTAccount)sid.Translate(typeof(NTAccount));

                FileAttributes attr = File.GetAttributes(F);

                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    var dInfo = new DirectoryInfo(F);
                    DirectorySecurity fSecurity = dInfo.GetAccessControl(AccessControlSections.All);

                    fSecurity.SetOwner(account);
                    dInfo.SetAccessControl(fSecurity);
                    fSecurity.AddAccessRule(new FileSystemAccessRule(account, FileSystemRights.FullControl,
                                                                                     InheritanceFlags.ContainerInherit |
                                                                                     InheritanceFlags.ObjectInherit,
                                                                                     PropagationFlags.None, AccessControlType.Allow));
                    dInfo.SetAccessControl(fSecurity);
                }
                else
                {
                    var fInfo = new FileInfo(F);

                    var nAccRule = new FileSystemAccessRule(account.Value, FileSystemRights.FullControl,
                                                                         AccessControlType.Allow);
                    ;
                    FileSecurity fSecurity = fInfo.GetAccessControl(AccessControlSections.Owner);

                    fSecurity.SetOwner(new NTAccount(account.Value));
                    fInfo.SetAccessControl(fSecurity);
                    fSecurity.AddAccessRule(nAccRule);
                    fInfo.SetAccessControl(fSecurity);
                    return;
                }


                ClearAttributeFile(F);
            }
            catch (Exception Ex)
            {
            }
        }

        public static string GetSubItems(ListViewItem LST)
        {
            string e = "";
            int s = 0;
            try
            {
                foreach (ListViewItem.ListViewSubItem S in LST.SubItems) { e += s + ": " + S.Text + " | "; s += 1; }
            }
            catch (Exception Ex) { }
            return e;
        }

        public static void ToolStripIcons(ToolStrip TS)
        {
            //TS.Renderer = new MySR();
            foreach (ToolStripItem C in TS.Items)
            {

                if (C.Tag != null)
                {
                    string tag = (string)C.Tag;
                    if (tag.EqualsIgnoreCase("Add"))
                    {
                        C.Image = Properties.Resources.Add;
                    }
                    if (tag.EqualsIgnoreCase("Remove"))
                    {
                        C.Image = Properties.Resources.Remove;
                    }
                    if (tag.EqualsIgnoreCase("OK"))
                    {
                        C.Image = Properties.Resources.OK;
                    }
                    if (tag.EqualsIgnoreCase("Info"))
                    {
                        C.Image = Properties.Resources.Info;
                    }
                    if (tag.EqualsIgnoreCase("Wincert"))
                    {
                        C.Image = Properties.Resources.Wincert;
                    }
                    if (tag.EqualsIgnoreCase("Addons"))
                    {
                        C.Image = Properties.Resources.Addons;
                    }
                    if (tag.EqualsIgnoreCase("Mount"))
                    {
                        C.Image = Properties.Resources.Mount;
                    }
                    if (tag.EqualsIgnoreCase("Reg"))
                    {
                        C.Image = Properties.Resources.Reg;
                    }
                    if (tag.EqualsIgnoreCase("Search"))
                    {
                        C.Image = Properties.Resources.Search;
                    }
                    if (tag.EqualsIgnoreCase("Updates"))
                    {
                        C.Image = Properties.Resources.Updates;
                    }
                    if (tag.EqualsIgnoreCase("Cancel"))
                    {
                        C.Image = Properties.Resources.Close;
                    }
                    if (tag.EqualsIgnoreCase("Refresh"))
                    {
                        C.Image = Properties.Resources.Refresh;
                    }
                    if (tag.EqualsIgnoreCase("Tool"))
                    {
                        C.Image = Properties.Resources.Tool;
                    }
                    if (tag.EqualsIgnoreCase("Edit"))
                    {
                        C.Image = Properties.Resources.Edit;
                    }
                }
            }
            FreeRAM();
        }

        public static string GetDefaultBrowser()
        {
            try
            {
                string sBrowserPath = cReg.GetValue(Microsoft.Win32.Registry.CurrentUser, "Software\\Classes\\http\\shell\\open\\command", "").ReplaceIgnoreCase("\"", "");

                if (!sBrowserPath.ToUpper().EndsWithIgnoreCase(".EXE"))
                {
                    sBrowserPath = cReg.GetValue(Microsoft.Win32.Registry.CurrentUser, "Software\\Classes\\ActivatableClasses\\Package\\DefaultBrowser_NOPUBLISHERID\\Server\\DefaultBrowserServer", "ExePath").ReplaceIgnoreCase("\"", "");
                }

                if (!sBrowserPath.ToUpper().EndsWithIgnoreCase(".EXE"))
                {
                    sBrowserPath = cReg.GetValue(Microsoft.Win32.Registry.ClassesRoot, "http\\shell\\open\\command", "").ReplaceIgnoreCase("\"", "");
                }

                sBrowserPath = sBrowserPath.Substring(0, sBrowserPath.LastIndexOf(".exe", StringComparison.Ordinal) + 4);
                if (File.Exists(sBrowserPath)) { return sBrowserPath; }
            }
            catch { }

            return "explorer.exe";
        }

        public static DateTime ParseDate(string date, string format = "yyyy/mm/dd")
        {
            //DateTime.ParseExact(date, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            date = date.Trim();
            int year = 0;
            int month = 1;
            int day = 2;
            switch (format.ToUpper())
            {
                case "DD/MM/YYYY":
                    year = 2;
                    month = 1;
                    day = 0;
                    break;
                case "MM/DD/YYYY":
                    year = 2;
                    month = 0;
                    day = 0;
                    break;
            }

            if (date.ContainsIgnoreCase(" "))
            {
                date = date.Substring(0, date.IndexOf(' '));
            }

            try
            {
                char splitChar = '/';

                if (date.Contains("-"))
                    splitChar = '-';

                int years = int.Parse(date.Split(splitChar)[year]);
                int months = int.Parse(date.Split(splitChar)[month]);
                int days = int.Parse(date.Split(splitChar)[day]);
                return new DateTime(years, months, days);
            }
            catch (Exception Ex)
            {
                string extended = string.Format("Date: {0}, Format: {1}", date, format.ToUpper());
                new SmallError("Error parsing date", Ex, extended).Upload();
            }
            return DateTime.Now;
        }

        public static string GetWindowsKey()
        {
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
                        return ProductKey;

                    }

                }
            }
            catch (Exception Ex)
            {
                new SmallError("Unable to detect serial", Ex).Upload();
            }
            return "";
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


        static string sWebSite = "";
        public static void OpenLink(string Site, bool AdFly = true)
        {
            string S = Site;
            bool showAd = false;
            if (!Site.ContainsIgnoreCase("ADF.LY") && AdFly && !cOptions.ValidKey)
            {
                S = "http://adf.ly/1964538/" + Site;
                showAd = true;
            }

            if (!new Uri(S).IsFile)
            {
                S = S.ReplaceIgnoreCase(" ", "%20");
            }

            try
            {
                var P = new Process();

                P.StartInfo.FileName = GetDefaultBrowser();

                P.StartInfo.Arguments = "\"" + S + "\"";

                P.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                P.Start();
            }
            catch (Exception Ex1)
            {
                try
                {
                    string IE = SysProgFiles + "\\Internet Explorer\\iexplore.exe";
                    if (File.Exists(IE))
                    {
                        OpenProgram("\"" + IE + "\"", "\"" + S + "\"", false, ProcessWindowStyle.Normal);
                    }
                    else
                    {
                        MessageBox.Show("There seems to be an issue detecting a default browser.", "Error");
                    }
                }
                catch { }
            }

            if (showAd && cOptions.ShowDonateAd)
            {
                new frmDonate("Hate Adverts?").ShowDialog();
                cOptions.ShowDonateAd = false;
            }
        }
        const int BYTES_TO_READ = sizeof(Int64);

        public static bool FilesAreEqual(string firstFile, string secondFile)
        {
            FileInfo first = new FileInfo(firstFile);
            FileInfo second = new FileInfo(secondFile);
            if (first.Length != second.Length)
                return false;

            int iterations = (int)Math.Ceiling((double)first.Length / BYTES_TO_READ);

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                byte[] one = new byte[BYTES_TO_READ];
                byte[] two = new byte[BYTES_TO_READ];

                for (int i = 0; i < iterations; i++)
                {
                    fs1.Read(one, 0, BYTES_TO_READ);
                    fs2.Read(two, 0, BYTES_TO_READ);

                    if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        return false;
                }
            }

            return true;
        }

        public static string UpdateTooltips(string iName, string Desc, string dName, string dDesc, string dFlags)
        {
            if (string.IsNullOrEmpty(dName)) { dName = "[None]"; }
            if (string.IsNullOrEmpty(dDesc)) { dDesc = "[None]"; }
            string TT = "Name: " + iName + Environment.NewLine + "Desc: " + Desc + Environment.NewLine + "Display Name: " + dName + Environment.NewLine + "Display Desc: " + dDesc + Environment.NewLine + "Flag: " + dFlags;
            return TT;
        }

        public static void OpenForm(Form F, Form Sender, object B = null)
        {
            if (B != null)
            {
                if (B is Button) { Button A = (Button)B; A.Enabled = false; }
                if (B is ToolStripButton) { ToolStripButton A = (ToolStripButton)B; A.Enabled = false; }
            }

            foreach (Form FN in Application.OpenForms)
            {
                if (String.Equals(FN.Name, F.Name, StringComparison.CurrentCultureIgnoreCase)) { return; }
            }

            F.Show();

            if (Sender != null)
            {
                Sender.Close();
            }
        }

        public static void ErrorBox(string Description, string Title, string Extra = "")
        {
            using (frmErrorBox EB = new frmErrorBox())
            {
                EB.lblTitle.Text = Title;
                EB.lblDesc.Text = Description;
                EB.txtEx.Text = Extra;
                EB.TopMost = true;
                EB.ShowDialog();
            }
        }

        public static void ReturnME()
        {
            OpenForm(new frmToolsManager(), null);
            FreeRAM();
        }


        public static void GetFiles(ref List<string> foundList, string dirPath, string fileType)
        {
            foundList.AddRange(Directory.GetFiles(dirPath, fileType, SearchOption.TopDirectoryOnly));

            foreach (string dir in Directory.GetDirectories(dirPath, "*", SearchOption.TopDirectoryOnly))
            {
                FileAttributes attributes = new DirectoryInfo(dir).Attributes;
                if ((attributes & FileAttributes.ReparsePoint) != 0) { continue; }

                if (dir.Length >= 248) { continue; }

                try
                {
                    GetFiles(ref foundList, dir, fileType);
                }
                catch (Exception Ex) { }
            }
        }

        /// <summary>
        /// Checks to see if the path is network path.
        /// </summary>
        /// <param name="path">The directory you wish to check.</param>
        /// <returns>True is on network.</returns>
        public static bool IsNetworkPath(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            return directoryInfo.FullName.StartsWithIgnoreCase(String.Empty.PadLeft(2, Path.DirectorySeparatorChar));
        }

        /// <summary>
        /// Checks to see if the specified directroy is read-only.
        /// </summary>
        /// <param name="directory">The directory that needs checking.</param>
        /// <returns>True if read-only.</returns>
        public static bool IsReadOnly(string directory)
        {
            try
            {
                using (var SW = new StreamWriter(directory + "\\test.0"))
                {
                    SW.WriteLine("test");
                }
                Files.DeleteFile(directory + "\\test.0");
                return false;
            }
            catch { return true; }
        }


        public static string GetSize(string dPath, bool addString = true, string filter = "*")
        {
            long OSize = -1;

            foreach (string path in dPath.Split('|'))
            {
                try
                {
                    if (!Directory.Exists(path) && !File.Exists(path)) { continue; }

                    if (OSize == -1) { OSize = 0; }

                    if (Directory.Exists(path))
                    {
                        OSize += Directory.GetFiles(path, filter, SearchOption.AllDirectories).Select(F => new FileInfo(F)).Select(FI => FI.Length).Sum();
                    }
                    else if (File.Exists(path))
                    {
                        var FI = new FileInfo(path);
                        OSize += FI.Length;
                    }
                }
                catch (Exception Ex)
                {
                    new SmallError("Error getting size", Ex, path);
                }
            }
            if (addString)
            {
                return BytesToString(OSize);
            }

            return OSize.ToString(CultureInfo.InvariantCulture);
        }

        public static string BytesToString(double Size, bool AppendS = true)
        {
            try
            {
                if (AppendS)
                {
                    if (Size < 1024)
                    {
                        return Size.ToString(CultureInfo.InvariantCulture) + " bytes";
                    }
                    if (Size >= 1024 && Size <= 1048576)
                    {
                        return (Size / 1024).ToString("N") + " KB";
                    }
                    if (Size >= 1048576 && Size <= 1073741824)
                    {
                        return (Size / 1024 / 1024).ToString("N") + " MB";
                    }
                    if (Size >= 1073741824)
                    {
                        return (Size / 1024 / 1024 / 1024).ToString("N") + " GB";
                    }
                    if (Size >= 1099511627776)
                    {
                        return (Size / 1024 / 1024 / 1024 / 1024).ToString("N") + " TB";
                    }
                }
                else
                {
                    if (Size < 1024)
                    {
                        return Size.ToString(CultureInfo.InvariantCulture);
                    }
                    if (Size >= 1024 && Size <= 1048576)
                    {
                        return (Size / 1024).ToString("N");
                    }
                    if (Size >= 1048576 && Size <= 1073741824)
                    {
                        return (Size / 1024 / 1024).ToString("N");
                    }
                    if (Size >= 1073741824)
                    {
                        return (Size / 1024 / 1024 / 1024).ToString("N");
                    }
                    if (Size >= 1099511627776)
                    {
                        return (Size / 1024 / 1024 / 1024 / 1024).ToString("N");
                    }
                }
            }
            catch (Exception Ex)
            {
                new SmallError("Unable to convert size", Ex, Size.ToString()).Upload();
                return "N/A";
            }
            return Size.ToString("N") + " bytes";
        }

        public static string GetFName(string S, bool Extension = true)
        {
            if (Extension)
            {
                return Path.GetFileName(S);
            }

            return Path.GetFileNameWithoutExtension(S);
        }

        public static void WriteResource(byte[] R, string Output, Form F)
        {
            if (Environment.OSVersion.Version.Major < 6 && R == Properties.Resources.imagex) { return; }
            Exception mainEx = null;

            if (!File.Exists(Output))
            {

                try
                {
                    string D = Path.GetDirectoryName(Output);
                    if (!Directory.Exists(D))
                    {
                        cMain.CreateDirectory(D);
                    }

                    File.WriteAllBytes(Output, R);
                }
                catch (Exception Ex)
                {
                    mainEx = Ex;
                    if (!Output.ContainsIgnoreCase((cMain.UserTempPath + "\\").ToUpper()))
                    {
                        string O = Path.GetFileName(Output);

                        WriteResource(R, cMain.UserTempPath + "\\" + O, F);
                    }
                }


            }

            if (!File.Exists(Output))
            {
                new SmallError("Missing Resource", mainEx, Output).Upload();
            }

        }

        public static bool CreateDirectory(string dirPath)
        {
            if (Directory.Exists(dirPath)) { return true; }
            Directory.CreateDirectory(dirPath);

            return Directory.Exists(dirPath);
        }

     

        public static string MD5CalcFile(string strPath)
        {
            byte[] arrHash;
            try
            {
                using (var objMD5 = new MD5CryptoServiceProvider())
                {
                    using (Stream objReader = new FileStream(strPath, FileMode.Open, FileAccess.Read, FileShare.Read, 8192))
                    {
                        arrHash = objMD5.ComputeHash(objReader);
                        objMD5.Clear();
                        return ByteArrayToString(arrHash);
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        private static string ByteArrayToString(byte[] arrInput)
        {
            if (arrInput == null) { return null; }
            var strOutput = new StringBuilder(arrInput.Length);
            foreach (byte b in arrInput)
            {
                strOutput.Append(string.Format("{0:X2}", b));
            }

            return strOutput.ToString().ToUpper();
        }

        public static string GetLPName(string OrigFilename)
        {
            if (OrigFilename.ContainsIgnoreCase("LV-LV"))
            {
                OrigFilename = "Latvian";
            }
            if (OrigFilename.ContainsIgnoreCase("EN-US"))
            {
                OrigFilename = "English";
            }
            if (OrigFilename.ContainsIgnoreCase("AR-SA"))
            {
                OrigFilename = "Arabic";
            }
            if (OrigFilename.ContainsIgnoreCase("BG-BG"))
            {
                OrigFilename = "Bulgarian";
            }
            if (OrigFilename.ContainsIgnoreCase("CS-CZ"))
            {
                OrigFilename = "Czech";
            }
            if (OrigFilename.ContainsIgnoreCase("DA-DK"))
            {
                OrigFilename = "Danish";
            }
            if (OrigFilename.ContainsIgnoreCase("DE-DE"))
            {
                OrigFilename = "German";
            }
            if (OrigFilename.ContainsIgnoreCase("EL-GR"))
            {
                OrigFilename = "Greek";
            }
            if (OrigFilename.ContainsIgnoreCase("ES-ES"))
            {
                OrigFilename = "Spanish";
            }
            if (OrigFilename.ContainsIgnoreCase("ET-EE"))
            {
                OrigFilename = "Estonian";
            }
            if (OrigFilename.ContainsIgnoreCase("FI-FI"))
            {
                OrigFilename = "Finnish";
            }
            if (OrigFilename.ContainsIgnoreCase("FR-FR"))
            {
                OrigFilename = "French";
            }
            if (OrigFilename.ContainsIgnoreCase("HE-IL"))
            {
                OrigFilename = "Hebrew";
            }
            if (OrigFilename.ContainsIgnoreCase("HR-HR"))
            {
                OrigFilename = "Croatian";
            }
            if (OrigFilename.ContainsIgnoreCase("HU-HU"))
            {
                OrigFilename = "Hungarian";
            }
            if (OrigFilename.ContainsIgnoreCase("IT-IT"))
            {
                OrigFilename = "Italian";
            }
            if (OrigFilename.ContainsIgnoreCase("JA-JP"))
            {
                OrigFilename = "Japanese";
            }
            if (OrigFilename.ContainsIgnoreCase("KO-KR"))
            {
                OrigFilename = "Korean";
            }
            if (OrigFilename.ContainsIgnoreCase("LT-LT"))
            {
                OrigFilename = "Lithuanian";
            }
            if (OrigFilename.ContainsIgnoreCase("NB-NO"))
            {
                OrigFilename = "Norwegian";
            }
            if (OrigFilename.ContainsIgnoreCase("NL-NL"))
            {
                OrigFilename = "Dutch";
            }
            if (OrigFilename.ContainsIgnoreCase("PL-PL"))
            {
                OrigFilename = "Polish";
            }
            if (OrigFilename.ContainsIgnoreCase("PT-BR"))
            {
                OrigFilename = "Brazilian Portuguese";
            }
            if (OrigFilename.ContainsIgnoreCase("PT-PT"))
            {
                OrigFilename = "Portuguese";
            }
            if (OrigFilename.ContainsIgnoreCase("RO-RO"))
            {
                OrigFilename = "Romanian";
            }
            if (OrigFilename.ContainsIgnoreCase("RU-RU"))
            {
                OrigFilename = "Russian";
            }
            if (OrigFilename.ContainsIgnoreCase("SK-SK"))
            {
                OrigFilename = "Slovak";
            }
            if (OrigFilename.ContainsIgnoreCase("SL-SI"))
            {
                OrigFilename = "Slovenian";
            }
            if (OrigFilename.ContainsIgnoreCase("SR-LATN-CS"))
            {
                OrigFilename = "Serbian-Latin";
            }
            if (OrigFilename.ContainsIgnoreCase("SV-SE"))
            {
                OrigFilename = "Swedish";
            }
            if (OrigFilename.ContainsIgnoreCase("TH-TH"))
            {
                OrigFilename = "Thai";
            }
            if (OrigFilename.ContainsIgnoreCase("TR-TR"))
            {
                OrigFilename = "Turkish";
            }
            if (OrigFilename.ContainsIgnoreCase("UK-UA"))
            {
                OrigFilename = "Ukrainian";
            }
            if (OrigFilename.ContainsIgnoreCase("ZH-CN"))
            {
                OrigFilename = "Chinese (Simplified)";
            }
            if (OrigFilename.ContainsIgnoreCase("ZH-HK"))
            {
                OrigFilename = "Chinese (Hong Kong)";
            }
            if (OrigFilename.ContainsIgnoreCase("ZH-TW"))
            {
                OrigFilename = "Chinese (Traditional-Taiwan)";
            }
            return OrigFilename;
        }

        public static void FreeRAM()
        {
            try
            {
                if (cOptions.FreeRAM)
                {
                    EmptyWorkingSet(Process.GetCurrentProcess().Handle);
                }
            }
            catch { }
        }



        public static void AutoSizeColums(ListView L)
        {
            int totalWidth = 0;
            foreach (ColumnHeader CH in L.Columns)
            {
                if (CH.Index == L.Columns.Count - 1) { break; }
                CH.Width = -2;
                totalWidth += CH.Width;
            }

            if (totalWidth < L.Width)
            {
                L.Columns[L.Columns.Count - 1].Width = L.Width - (totalWidth + 5 + SystemInformation.VerticalScrollBarWidth);
            }
            else { L.Columns[L.Columns.Count - 1].Width = -2; }
        }

        public static int GetAdditions(Array FileList)
        {
            return FileList.Cast<string>().Count();
        }

        public static string RandomName(int Min = 1, int Max = 999999999)
        {
            var rand = new Random();
            string V = Convert.ToString(rand.Next(Min, Max));
            return V;
        }

        public static bool NumericOnly(char Key)
        {
            bool Handled;
            if (Key >= (char)48 && Key <= (char)57)
            {
                Handled = false;
            }
            else if (Key == (char)8)
            {
                Handled = false;
            }
            else
            {
                Handled = true;
            }

            return Handled;
        }
        public static void CompressFiles(string Folder, string SaveTo, Form FM)
        {
            string sSaveTo = SaveTo;
            if (SaveTo.ContainsForeignCharacters())
            {
                sSaveTo = cOptions.WinToolkitTemp + "\\" + RandomName(1, 1000) + sSaveTo.Substring(sSaveTo.Length - 4);
                // File.Copy(SaveTo, sSaveTo, true);
            }

            Files.DeleteFile(SaveTo);
            if (Directory.Exists(Folder))
            {
                WriteResource(Properties.Resources._7z_x32, UserTempPath + "\\Files\\7z.exe", null);
                WriteResource(Properties.Resources._7z_x32_DLL, UserTempPath + "\\Files\\7z.dll", null);
                string sCompress = RunExternal("\"" + UserTempPath + "\\Files\\7z.exe\"", "a -t7z \"" + sSaveTo + "\" \"" + Folder + "\"*");
            }

            if (SaveTo.ContainsForeignCharacters() && File.Exists(sSaveTo))
            {
                File.Move(sSaveTo, SaveTo);
            }
        }

     
        public static string ExtractFiles(string filename, string extractTo, Form FM = null, string sFile = "*", bool delete = true, bool ShowError = true)
        {
            string sFilename = filename;
            if (!File.Exists(sFilename)) { return ""; }

            try
            {
                if (filename.ContainsForeignCharacters())
                {
                    if (!Directory.Exists(cOptions.WinToolkitTemp)) { cMain.CreateDirectory(cOptions.WinToolkitTemp); }
                    sFilename = cOptions.WinToolkitTemp + "\\" + RandomName(1, 1000) + sFilename.Substring(sFilename.Length - 4);
                    File.Copy(filename, sFilename, true);
                }
            }
            catch (Exception Ex)
            {
                new SmallError("Copy file with foreign characters", Ex, "From: " + filename + "\nTo: " + sFilename).Upload();
                sFilename = filename;
            }



            string sExtractData = "";
            try
            {
                if (File.Exists(sFilename))
                {
                    if (delete) {
                        Files.DeleteFolder(extractTo, true); }

                    if (!Directory.Exists(extractTo)) { cMain.CreateDirectory(extractTo); }
                    WriteResource(Properties.Resources._7z_x32, UserTempPath + "\\Files\\7z.exe", null);
                    WriteResource(Properties.Resources._7z_x32_DLL, UserTempPath + "\\Files\\7z.dll", null);


                    if (string.IsNullOrEmpty(sFile) || sFile.EqualsIgnoreCase("*"))
                    {
                        sExtractData = RunExternal(UserTempPath + "\\Files\\7z.exe", "x \"" + sFilename + "\" -o\"" + extractTo + "\" -aoa");
                    }
                    else
                    {
                        sExtractData = RunExternal(UserTempPath + "\\Files\\7z.exe", "x \"" + sFilename + "\" -o\"" + extractTo + "\" " + sFile + " -aoa");
                    }

                    if (!sExtractData.ContainsIgnoreCase("Everything is Ok"))
                    {
                        if (ShowError && IsEmpty(extractTo) && !filename.ToUpper().EndsWithIgnoreCase(".CAB") && !filename.ToUpper().EndsWithIgnoreCase(".MSU") && !sExtractData.ContainsIgnoreCase("Error: Can not open file as archive"))
                        {
                            LargeError LE = new LargeError("File Extraction", "Unable to extract file: " + filename + "\r\n\r\nFile may be corrupt.", "ExtractData: " + sExtractData, null);
                            LE.Upload();
                            if (ShowError) { LE.ShowDialog(); }
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException UAE)
            {
                if (ShowError)
                {
                    LargeError LE = new LargeError("File Extraction", "Unable to extract: '" + filename + "'\n\nMake sure the folder is not read-only and that you have write permissions.", "Access Denied.\nFrom: " + filename + "\nTo: " + extractTo, UAE);
                    LE.ShowDialog();
                }
            }
            catch (Exception Ex)
            {
                if (ShowError)
                {
                    LargeError LE = new LargeError("File Extraction", "Unable to extract: '" + filename + "'\n\n" + Ex.Message, "From: " + filename + "\nTo: " + extractTo, Ex);
                    LE.Upload();
                    LE.ShowDialog();
                }
            }

            if (filename.ContainsForeignCharacters())
            {
                Files.DeleteFile(sFilename);
            }

            return sExtractData;
        }

        /// <summary>
        /// Checks to see if the directory contains any files.
        /// </summary>
        /// <param name="path">The directory you wish to check.</param>
        /// <returns>True if empty</returns>
        public static bool IsEmpty(string path)
        {
            if (!Directory.Exists(path)) { return true; }

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);

                if (dirInfo.GetFileSystemInfos().Where(f => f is FileInfo).Any())
                {
                    return false;
                }

                foreach (DirectoryInfo dir in dirInfo.GetFileSystemInfos().Where(f => f is DirectoryInfo))
                {
                    if (!IsEmpty(dir.FullName))
                    {
                        return false;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            return true;
        }

        public static string InputBox(string text, string title)
        {
            return InputBox(text, title, "");
        }

        public static string InputBox(string text, string title, string defaultInput, int maxNumber = 0)
        {
            using (var prompt = new Prompts.frmInput(title, text))
            {

                prompt.txtInput.Text = defaultInput;
                prompt.MaxNumber = maxNumber;
                DialogResult DR = prompt.ShowDialog();

                if (DR != System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(defaultInput))
                {
                    return defaultInput;
                }
                else if (DR == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(prompt.txtInput.Text))
                {
                    return prompt.txtInput.Text;
                }

                return "";

            }
            return "";
        }

        public static void OpenProgram(string Filename, string Arguments, bool Wait, ProcessWindowStyle WindowStyle)
        {
            try
            {
                AppErrC = 0;
                if (Filename.ContainsIgnoreCase("IMAGEX"))
                {
                    WriteResource(Properties.Resources.imagex, cMain.UserTempPath + "\\Files\\Imagex.exe", null);
                }

                using (var SI = new Process())
                {
                    if (!string.IsNullOrEmpty(Arguments))
                    {
                        SI.StartInfo.Arguments = Arguments;
                    }
                    SI.StartInfo.WindowStyle = WindowStyle;

                    if (!Filename.EndsWithIgnoreCase("\"")) { Filename += "\""; }
                    if (!Filename.StartsWithIgnoreCase("\"")) { Filename = "\"" + Filename; }

                    if (!Filename.ContainsIgnoreCase("\\") && File.Exists(SysFolder + "\\" + Filename))
                    {
                        Filename = Filename.ReplaceIgnoreCase(Filename, SysFolder + "\\" + Filename);
                    }

                    SI.StartInfo.FileName = Filename;
                    tCancel = false;

                    SI.Start();

                    try
                    {
                        string SPri = Filename.ContainsIgnoreCase("DISM") ? cOptions.WinToolkitDISM : cOptions.WinToolkitExt;

                      
                        switch (SPri)
                        {
                            case "RealTime":
                                SI.PriorityClass = ProcessPriorityClass.RealTime;
                                break;
                            case "High":
                                SI.PriorityClass = ProcessPriorityClass.High;
                                break;
                            case "AboveNormal":
                                SI.PriorityClass = ProcessPriorityClass.AboveNormal;
                                break;
                            case "Normal":
                                SI.PriorityClass = ProcessPriorityClass.Normal;
                                break;
                            case "BelowNormal":
                                SI.PriorityClass = ProcessPriorityClass.BelowNormal;
                                break;
                            case "Idle":
                                SI.PriorityClass = ProcessPriorityClass.Idle;
                                break;
                        }
                    }
                    catch { }

                    if (Wait)
                    {
                        bool bExiting = false;
                        while (!SI.HasExited)
                        {
                            if (tCancel && bExiting == false) { SI.Kill(); }
                            Application.DoEvents();

                            if (Filename.ContainsIgnoreCase("EXE2CAB"))
                            {
                                Thread.Sleep(250);
                                try
                                {
                                    IntPtr handle = FindWindow("#32770", "EXE2CAB");
                                    if (handle.ToString() != "0")
                                    {
                                        SendMessage(new HandleRef(null, handle), WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        AppErrC = SI.ExitCode;
                    }
                    tCancel = false;

                }
            }
            catch (Exception Ex)
            {
                LargeError LE = new LargeError("Open Program", "An error occurred whilst trying to run: " + Filename, Ex);
                LE.Upload(); LE.ShowDialog();

            }
        }

        public static string DetectXPImagex(string ImagexPath)
        {
            string newImagexPath = ImagexPath;
            if (newImagexPath.StartsWithIgnoreCase("\"")) { newImagexPath = newImagexPath.Substring(1); }
            if (newImagexPath.EndsWithIgnoreCase("\"")) { newImagexPath = newImagexPath.Substring(0, newImagexPath.Length - 1); }
            if (Environment.OSVersion.Version.Major < 6 && File.Exists(SysFolder + "\\Imagex.exe"))
            {
                newImagexPath = SysFolder + "\\Imagex.exe";
            }
            return newImagexPath;
        }

        public static string RunExternal(string Filename, string Argument)
        {
            AppErrC = 0;
            string R = "";

            if (Filename.ContainsIgnoreCase("DISM.EXE") && !Argument.ContainsIgnoreCase("/ENGLISH"))
            {
             Argument += " /English";
            }

            if (Filename.ContainsIgnoreCase("IMAGEX"))
            {
                WriteResource(Properties.Resources.imagex, cMain.UserTempPath + "\\Files\\Imagex.exe", null);
            }

            try
            {
                using (var CMDprocess = new Process())
                {
                    CMDprocess.StartInfo.FileName = "cmd.exe";
                    CMDprocess.StartInfo.Arguments = "";
                    CMDprocess.StartInfo.RedirectStandardInput = true;
                    CMDprocess.StartInfo.RedirectStandardError = true;
                    CMDprocess.StartInfo.RedirectStandardOutput = true;
                    CMDprocess.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                    CMDprocess.StartInfo.UseShellExecute = false;
                    CMDprocess.StartInfo.CreateNoWindow = true;
                    CMDprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    CMDprocess.Start();

                    try
                    {
                        string SPri = Filename.ContainsIgnoreCase("DISM") ? cOptions.WinToolkitDISM : cOptions.WinToolkitExt;
                        switch (SPri)
                        {
                            case "RealTime":
                                CMDprocess.PriorityClass = ProcessPriorityClass.RealTime;
                                break;
                            case "High":
                                CMDprocess.PriorityClass = ProcessPriorityClass.High;
                                break;
                            case "AboveNormal":
                                CMDprocess.PriorityClass = ProcessPriorityClass.AboveNormal;
                                break;
                            case "Normal":
                                CMDprocess.PriorityClass = ProcessPriorityClass.Normal;
                                break;
                            case "BelowNormal":
                                CMDprocess.PriorityClass = ProcessPriorityClass.BelowNormal;
                                break;
                            case "Idle":
                                CMDprocess.PriorityClass = ProcessPriorityClass.Idle;
                                break;
                        }
                    }
                    catch { }

                    if (!Filename.StartsWithIgnoreCase("\"")) { Filename = "\"" + Filename; }
                    if (!Filename.EndsWithIgnoreCase("\"")) { Filename = Filename + "\""; }

                    using (StreamWriter SW = new StreamWriter(CMDprocess.StandardInput.BaseStream, Encoding.UTF8))
                    {
                        //using (StreamWriter SW = CMDprocess.StandardInput) {
                        SW.WriteLine("chcp 65001");
                        SW.WriteLine("Set SEE_MASK_NOZONECHECKS=1");
                        SW.WriteLine(Filename + " " + Argument);
                        SW.WriteLine("exit");
                    }
                    using (StreamReader SR = new StreamReader(CMDprocess.StandardOutput.BaseStream, Encoding.UTF8))
                    {
                        R = SR.ReadToEnd();
                    }

                    try
                    {
                        AppErrC = CMDprocess.ExitCode;
                    }
                    catch { }
                }
            }
            catch (Exception Ex)
            {
                LargeError LE = new LargeError("Unable to RunExternal", Ex.Message, Ex);
                LE.Upload();
                LE.ShowDialog();
            }

            return R;
        }

        public static List<string> SearchDirectory(string Directory, string SearchFilter)
        {
            Dirs.Clear();
            var DI = new DirectoryInfo(Directory);
            cycleThroughDirs(DI, SearchFilter);

            return Dirs;
        }

        private static void cycleThroughDirs(DirectoryInfo dir, string sSearch)
        {
            try
            {
                foreach (DirectoryInfo dirInfo in dir.GetDirectories())
                {
                    bool skip = false;
                    foreach (string s in sSearch.Split(';'))
                    {
                        if (dirInfo.Name.ContainsIgnoreCase(s.ToUpper()))
                        {
                            skip = true;
                        }
                    }
                    if (skip == false)
                    {
                        cycleThroughDirs(dirInfo, sSearch);
                    }
                }
            }
            catch (Exception) { }

            try
            {
               // FileSystemInfo[] fii = dir.GetFileSystemInfos();
                foreach (FileSystemInfo fileInfo in dir.GetFileSystemInfos())
                {
                    foreach (string s in sSearch.Split(';'))
                    {
                        if (s.StartsWithIgnoreCase("*"))
                        {
                            if (fileInfo.Name.ToUpper().EndsWithIgnoreCase(s.Substring(1).ToUpper()))
                            {
                                Dirs.Add(fileInfo.FullName);
                            }
                        }
                        else
                        {
                            if (fileInfo.Name.ContainsIgnoreCase(s.ToUpper()))
                            {
                                Dirs.Add(fileInfo.FullName);
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        public static void OnFormClosing(Form F, bool bSaveSettings = true)
        {
            //Tasks which are done when the form is closing.
            if (F.FormBorderStyle == FormBorderStyle.Sizable)
            {
                if (cOptions.FormSize.ContainsIgnoreCase(F.Name))
                {
                    string sTemp = cOptions.FormSize.Split('|').Where(N => !string.IsNullOrEmpty(N) && !N.ContainsIgnoreCase(F.Name)).Aggregate("", (current, N) => current + ("|" + N));
                    cOptions.FormSize = sTemp;
                }
                cOptions.FormSize += "|" + F.Name + ":" + F.Width + "*" + F.Height;
            }
            if (bSaveSettings) { cOptions.SaveSettings(); }
        }

        public static void CopyDirectory(string SourcePath, string DestPath, bool Overwrite, bool KeepOriginal, string Exclude = "")
        {
            var SourceDir = new DirectoryInfo(SourcePath);
            var DestDir = new DirectoryInfo(DestPath);

            if (DestDir.Parent != null)
            {
                if (!DestDir.Parent.Exists) { DestDir.Parent.Create(); }
                if (!DestDir.Exists) { DestDir.Create(); }
            }

            foreach (FileInfo ChildFile in SourceDir.GetFiles())
            {
                bool Copy = !(!string.IsNullOrEmpty(Exclude) && ChildFile.FullName.ContainsIgnoreCase(Exclude.ToUpper()));
                string CopyTo = Path.Combine(DestDir.FullName, ChildFile.Name);

                if (Copy)
                {
                    try
                    {
                        if (Overwrite && File.Exists(CopyTo)) {
                            Files.DeleteFile(CopyTo); }
                        if (!File.Exists(CopyTo)) { ChildFile.CopyTo(CopyTo); }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            TakeOwnership(CopyTo);
                            ClearAttributeFile(CopyTo);
                            if (Overwrite && File.Exists(CopyTo)) {
                                Files.DeleteFile(CopyTo); }
                            if (!File.Exists(CopyTo)) { ChildFile.CopyTo(CopyTo); }
                        }
                        catch (Exception Ex)
                        {
                            new SmallError("Error Copying Files", Ex,
                                "Source: " + ChildFile.FullName + " [" + cMain.GetSize(ChildFile.FullName) + "]" + Environment.NewLine +
                                "Dest: " + CopyTo + Environment.NewLine + " [" + cMain.BytesToString(new DriveInfo(CopyTo).AvailableFreeSpace) + "]" + Environment.NewLine +
                                "KeepOriginal: " + KeepOriginal.ToString() + Environment.NewLine +
                                "Overwrite: " + Overwrite.ToString()).Upload();
                        }

                    }
                }
                if (KeepOriginal == false) {
                    Files.DeleteFile(ChildFile.FullName); }
            }

            foreach (DirectoryInfo SubDir in SourceDir.GetDirectories())
            {
                CopyDirectory(SubDir.FullName, Path.Combine(DestDir.FullName, SubDir.Name), Overwrite, KeepOriginal);
            }

            if (!KeepOriginal && IsEmpty(SourcePath))
            {
                Files.DeleteFolder(SourcePath, false);
            }
        }

        public static void MoveDirectory(string SourcePath, string DestPath, bool Overwrite, string Exclude = "")
        {
            var SourceDir = new DirectoryInfo(SourcePath);
            var DestDir = new DirectoryInfo(DestPath);

            if (DestDir.Parent != null)
            {
                if (!DestDir.Parent.Exists) { DestDir.Parent.Create(); }
                if (!DestDir.Exists) { DestDir.Create(); }
            }

            foreach (FileInfo ChildFile in SourceDir.GetFiles())
            {
                bool Move = !(!string.IsNullOrEmpty(Exclude) && ChildFile.FullName.ContainsIgnoreCase(Exclude.ToUpper()));

                if (Move)
                {
                    string MoveTo = Path.Combine(DestDir.FullName, ChildFile.Name);

                    try
                    {
                        if (Overwrite && File.Exists(MoveTo)) {
                            Files.DeleteFile(MoveTo); }
                        if (!File.Exists(MoveTo)) { ChildFile.MoveTo(MoveTo); }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            TakeOwnership(MoveTo);
                            ClearAttributeFile(MoveTo);
                            if (Overwrite && File.Exists(MoveTo)) {
                                Files.DeleteFile(MoveTo); }
                            if (!File.Exists(MoveTo)) { ChildFile.MoveTo(MoveTo); }
                        }
                        catch (Exception Ex)
                        {
                            new SmallError("Error Moving Files", Ex,
                                "Source: " + ChildFile.FullName + " [" + GetSize(ChildFile.FullName) + "]" + Environment.NewLine +
                                "Dest: " + MoveTo + Environment.NewLine + " [" + BytesToString(new DriveInfo(MoveTo).AvailableFreeSpace) + "]" + Environment.NewLine +
                               "Overwrite: " + Overwrite).Upload();
                        }

                    }
                }

            }

            foreach (DirectoryInfo SubDir in SourceDir.GetDirectories())
            {
                MoveDirectory(SubDir.FullName, Path.Combine(DestDir.FullName, SubDir.Name), Overwrite);
            }

            if (IsEmpty(SourcePath))
            {
                Files.DeleteFolder(SourcePath, false);
            }
        }


        public static string GetWIMValue(string L)
        {
            while (L.ContainsIgnoreCase(">"))
            {
                L = L.Substring(1);
            }
            return L;
        }

        public enum MoveDirection
        {
            Up,
            Down
        }

        public static bool MoveListViewItem(ListView lv, MoveDirection direction)
        {
            if (lv.SelectedItems.Count != 1)
            {
                MessageBox.Show("You have to select 1 item to move!", "Invalid Selection");
                return true;
            }

            bool sEnd = false;
            string cache;
            string cacheTooltip;
            Color cacheColor;
            int iImageIndex;
            object cacheTag;
            int sIndex;
            int nIndex;

            switch (direction)
            {
                case MoveDirection.Up:
                    sIndex = 0;
                    foreach (ListViewItem LSTM in lv.SelectedItems)
                    {
                        int cIDX = LSTM.Index;
                        if (cIDX == 0)
                        {
                            sIndex++;
                            sEnd = true;
                            continue;
                        }

                        if (LSTM != LSTM.Group.Items[sIndex])
                        {
                            nIndex = cIDX - 1;

                            while (lv.Items[cIDX].Group.Header != lv.Items[nIndex].Group.Header && nIndex > 0) { nIndex -= 1; }
                            lv.Items[cIDX].Selected = false;

                            cacheTag = lv.Items[nIndex].Tag;
                            lv.Items[nIndex].Tag = lv.Items[cIDX].Tag;
                            lv.Items[cIDX].Tag = cacheTag;

                            cacheTooltip = lv.Items[nIndex].ToolTipText;
                            lv.Items[nIndex].ToolTipText = lv.Items[cIDX].ToolTipText;
                            lv.Items[cIDX].ToolTipText = cacheTooltip;

                            cacheColor = lv.Items[nIndex].BackColor;
                            lv.Items[nIndex].BackColor = lv.Items[cIDX].BackColor;
                            lv.Items[cIDX].BackColor = cacheColor;

                            iImageIndex = lv.Items[nIndex].ImageIndex;
                            lv.Items[nIndex].ImageIndex = lv.Items[cIDX].ImageIndex;
                            lv.Items[cIDX].ImageIndex = iImageIndex;

                            for (int i = 0; i < lv.Items[cIDX].SubItems.Count; i++)
                            {
                                cache = lv.Items[nIndex].SubItems[i].Text;
                                lv.Items[nIndex].SubItems[i].Text = lv.Items[cIDX].SubItems[i].Text;
                                lv.Items[cIDX].SubItems[i].Text = cache;
                            }

                            lv.Items[nIndex].Selected = true;
                        }
                        else
                        {
                            sEnd = true;
                        }
                        sIndex++;
                    }
                    break;
                case MoveDirection.Down:
                    sIndex = 1;
                    foreach (ListViewItem LSTM in lv.SelectedItems)
                    {
                        int cIDX = LSTM.Index;
                        if (cIDX == lv.Items.Count - 1)
                        {
                            sIndex++;
                            sEnd = true;
                            continue;
                        }

                        nIndex = cIDX + 1;
                        if (LSTM != LSTM.Group.Items[LSTM.Group.Items.Count - sIndex])
                        {
                            while (lv.Items[cIDX].Group.Header != lv.Items[nIndex].Group.Header && nIndex < lv.Items.Count) { nIndex += 1; }
                            lv.Items[cIDX].Selected = false;

                            cacheTag = lv.Items[nIndex].Tag;
                            lv.Items[nIndex].Tag = lv.Items[cIDX].Tag;
                            lv.Items[cIDX].Tag = cacheTag;

                            cacheTooltip = lv.Items[nIndex].ToolTipText;
                            lv.Items[nIndex].ToolTipText = lv.Items[cIDX].ToolTipText;
                            lv.Items[cIDX].ToolTipText = cacheTooltip;

                            cacheColor = lv.Items[nIndex].BackColor;
                            lv.Items[nIndex].BackColor = lv.Items[cIDX].BackColor;
                            lv.Items[cIDX].BackColor = cacheColor;

                            iImageIndex = lv.Items[nIndex].ImageIndex;
                            lv.Items[nIndex].ImageIndex = lv.Items[cIDX].ImageIndex;
                            lv.Items[cIDX].ImageIndex = iImageIndex;

                            for (int i = 0; i < lv.Items[cIDX].SubItems.Count; i++)
                            {
                                cache = lv.Items[nIndex].SubItems[i].Text;
                                lv.Items[nIndex].SubItems[i].Text = lv.Items[cIDX].SubItems[i].Text;
                                lv.Items[cIDX].SubItems[i].Text = cache;
                            }

                            lv.Items[nIndex].Selected = true;
                        }
                        else
                        {
                            sEnd = true;
                        }
                        sIndex++;
                    }
                    break;
            }

            lv.Focus();
            return sEnd;
        }

        public static bool DetectAntivirus()
        {
            bool E = AntivirusInstalled("\\root\\SecurityCenter");
            if (E != true) { E = AntivirusInstalled("\\root\\SecurityCenter2"); }
            return E;
        }

        public static bool AntivirusInstalled(string root)
        {
            bool E = false;
            string wmipathstr = @"\\" + Environment.MachineName + root;
            try
            {
                var searcher = new ManagementObjectSearcher(wmipathstr, "SELECT * FROM AntivirusProduct");
                ManagementObjectCollection instances = searcher.Get();

                foreach (ManagementObject item in instances)
                {
                    string State = item["productState"].ToString();
                    switch (State)
                    {
                        case "266240":
                            E = true;
                            break;
                        case "266256":
                            E = true;
                            break;
                        case "397312":
                            E = true;
                            break;
                        case "397328":
                            E = true;
                            break;
                        case "397584":
                            E = true;
                            break;
                    }
                }
                return E;
            }
            catch (Exception Ex) { }

            return E;
        }

        private static void UpdateStatusHistory(string text)
        {
            if (string.IsNullOrEmpty(text))
                text = "[(null)]";

            lock (StatusHistory)
            {
                if (StatusHistory.Count >= 20)
                {
                    StatusHistory.RemoveAt(0);
                }
                StatusHistory.Add(text);
            }
        }

        public static void UpdateText(Control lbl, string text)
        {

            try
            {
                if (lbl.InvokeRequired)
                {
                    lbl.Invoke((MethodInvoker)delegate
                    {
                        UpdateText(lbl, text);
                    });
                    return;
                }
                lbl.Text = text;

                UpdateStatusHistory(text);
            }
            catch { }

        }

        public static void UpdateToolStripLabel(ToolStripLabel lbl, string text)
        {

            try
            {
                Control control = lbl.GetCurrentParent();

                if (control != null && control.InvokeRequired)
                {
                    control.Invoke((MethodInvoker)delegate
                    {
                        UpdateToolStripLabel(lbl, text);
                    });
                    return;
                }
                lbl.Text = text;

                UpdateStatusHistory(text);
            }
            catch { }

            Application.DoEvents();
        }

        public static Form eForm = null;
        public static ToolStripLabel eLBL = null;
        public static void MyHandler(object sender, ThreadExceptionEventArgs args)
        {
            var e = args.Exception;
            UnhandledError UE = new UnhandledError("Unknown Error", "A major unhandled error has occurred. A log should have been written and uploaded.", e);
            UE.Upload(); UE.ShowDialog();

        }
    }
}