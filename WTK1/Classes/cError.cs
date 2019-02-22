using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Management;
using WinToolkit.Classes;

namespace WinToolkit
{

    public class cError
    {
        public static List<string> UploadList = new List<string>();
        public static List<string> SaveList = new List<string>();
    }

    public abstract class CustomError
    {
        protected string RandomID = cMain.RandomName(100000, 999999);
        protected string Title = "";
        protected string BriefDescription = "";
        protected string ExtendedDescription = "";
        protected ListViewItem ListViewItem = null;
        protected Exception Exception = null;
        private string Date = DateTime.Now.ToString("dddd dd MMMM yyyy H:mm:ss tt zzz", System.Globalization.CultureInfo.InvariantCulture) + " " + TimeZone.CurrentTimeZone.StandardName;
        string SavedFileName = "";
        string ToSend = "";

        public override string ToString()
        {
            string sReturn = Identifier + ": " + BriefDescription;
            if (!string.IsNullOrEmpty(ExtendedDescription)) { sReturn += "\r\n\r\n" + ExtendedDescription; }

            if (Exception != null)
            {
                sReturn += "\r\n\r\nException: " + Exception.Message;
            }

            return sReturn;
        }

        #region PREPARE_LOG
        private string PrepareLog()
        {
            if (!string.IsNullOrEmpty(ToSend)) { return ToSend; }
            ToSend = Date + "\r\n\r\n";
            ToSend += "Log ID: " + Identifier + "_" + RandomID + "\r\n\r\nNOTE: Use the 'Log ID' when referring to your issue on the forum.\r\nIt will allow me to find your log and have a look at the problem easier.";

            if (!string.IsNullOrEmpty(Title))
            {
                ToSend += "\r\n\r\n***TITLE***\r\n" + Title;
            }

            ToSend += "\r\n\r\n***BASIC***\r\n" + BriefDescription;

            if (!string.IsNullOrEmpty(ExtendedDescription))
            {
                ToSend += "\r\n\r\n***EXTENDED***\r\n" + ExtendedDescription;
            }

            ToSend += GetSubItems;

            ToSend += GetExtendedException;

            ToSend += GetAppInfo;

            ToSend += GetComputerInfo;

            ToSend += GetStatusHistory;

            ToSend += GetRunningProcesses;

            ToSend += GetOptions;

            ToSend += GetStackTrace;

            ToSend += GetRawException;

            return ToSend;

        }

        #endregion

        #region UPLOADING_AND_SAVING

        public void Upload(bool Save = true)
        {

            if (Save) { this.Save(); }


            //Upload code removed.
        }

        protected void Save()
        {
            string sFilePath = cMain.ErrLogSavePath + "Error_" + cMain.WinToolkitVersion() + "_" + Identifier + "_" + RandomID + ".log";
            if (cError.SaveList.Contains(this.Identifier)) { return; }
            cError.SaveList.Add(this.Identifier);
            string ToSend = PrepareLog();

            if (!Directory.Exists(Path.GetDirectoryName(sFilePath))) { cMain.CreateDirectory(Path.GetDirectoryName(sFilePath)); }

            try
            {

                using (StreamWriter SW = new StreamWriter(sFilePath, false, Encoding.UTF8))
                {
                    SW.Write(ToSend + "\r\n");
                }
                cNotify.ShowNotification("Log Saved", "An error log has just been saved.\r\n\r\nClick here to view it.", ToolTipIcon.Info, sFilePath);
                SavedFileName = sFilePath;
            }
            catch (Exception Ex)
            {
                cNotify.ShowNotification("Log Failed", "Error saving log: " + sFilePath + "\r\n\r\n" + Ex.Message, ToolTipIcon.Warning);
            }


        }
        #endregion

        public void ShowDialog()
        {
            var TS = new frmErrorBox();
            TS.Text = this.GetType().ToString() + "[" + Identifier + "]";
            TS.lblTitle.Text = Title;
            TS.lblDesc.Text = BriefDescription;
            TS.txtEx.Text = PrepareLog();
            TS.ShowDialog();

        }

        #region GETINFO

        private string GetExtendedException
        {
            get
            {
                if (Exception == null) { return ""; }
                string Err = "\r\n\r\n***EXCEPTION***\r\n";
                Err += Exception.Message + "\r\n";
                Err += "InnerException: " + Exception.InnerException + "\r\n";
                Err += "Source: " + Exception.Source + "\r\n";
                Err += "TargetSite: " + Exception.TargetSite;
                return Err;
            }
        }

        private string GetStackTrace
        {
            get
            {
                StackTrace stackTrace = new StackTrace();
                if (stackTrace.FrameCount == 0) { return ""; }
                string sStack = "\r\n\r\n***STACK***";

                foreach (StackFrame SF in stackTrace.GetFrames())
                {
                    sStack += "\r\n" + SF.GetMethod().Name;
                }
                return sStack;
            }
        }

        private string GetSubItems
        {
            get
            {
                if (ListViewItem == null) { return ""; }

                string sItems = "";
                int I = 0;
                if (ListViewItem.SubItems.Count > 0)
                {
                    sItems = this.ListViewItem.SubItems.Cast<ListViewItem.ListViewSubItem>()
                        .Aggregate(sItems, (current, si) => current + "\r\n" + I++.ToString("0#") + ": " + si.Text);
                }
                if (ListViewItem.Tag != null)
                {
                    sItems += "\r\nTag: " + ListViewItem.Tag.ToString();
                }

                return "\r\n\r\n***LIST_VIEW_ITEM***" + sItems;
            }
        }

        private string GetRawException
        {
            get
            {
                if (Exception == null) { return ""; }
                return "\r\n\r\n***RAW_EXCEPTION***\r\n" + Exception.ToString();
            }
        }



        protected string Identifier
        {
            get
            {
                string ID = "";


                if (this.GetType() == typeof(TestError))
                {
                    ID = "Tx" + BriefDescription.CreateMD5();
                }
                else if (this.GetType() == typeof(UnhandledError))
                {
                    ID = "Ux" + BriefDescription.CreateMD5();
                }
                else if (this.GetType() == typeof(LegacyError))
                {
                    ID = "Lx" + BriefDescription.CreateMD5();
                }
                else if (this.GetType() == typeof(LargeError))
                {
                    ID = "0x" + Title.CreateMD5();
                }
                else if (this.GetType() == typeof(SmallError))
                {
                    ID = "1x" + BriefDescription.CreateMD5();
                }
                else
                {
                    ID = "3x" + BriefDescription.CreateMD5();
                }


                if (Exception != null)
                {
                    ID += "_" + String.Format("Ex{0:X8}", Exception.TargetSite.MetadataToken);
                }

                List<Form> OpenForms = Application.OpenForms.Cast<Form>().ToList();
                Form fError = OpenForms.FirstOrDefault(f => f.Name.EqualsIgnoreCase("frmErrorBox"));

                if (fError != null) { OpenForms.Remove(fError); }

                if (OpenForms.Count > 0)
                {
                    ID += "_" + OpenForms[OpenForms.Count - 1].Name;
                }

                return ID + "_" + cMain.DefaultLang;
            }
        }

        private string GetStatusHistory
        {
            get
            {
                const string statusHistory = "\r\n\r\n***STATUS HISTORY***\r\n";
                if (cMain.StatusHistory.Count == 0)
                {
                    return statusHistory + "No History";
                }
                return statusHistory + cMain.StatusHistory.Aggregate((current, next) => current + Environment.NewLine + next);
            }
        }

        private string GetMountedImages
        {
            get
            {
                string mountInfo = "\r\n\r\n***MOUNT INFO***\r\n";
                if (cReg.RegCheckMounted("SOFTWARE\\Microsoft\\WIMMount\\Mounted Images\\"))
                {
                    foreach (var RK in cReg.RegCheckMountDism())
                    {
                        mountInfo += string.Format("WIMFile: {0}\r\nMount: {1}\r\nState: {2}\r\n", RK.WimFile, RK.MountPath, RK.State);
                    }
                    return mountInfo;
                }
                return "No mount info";
            }
        }

        private string GetAppInfo
        {
            get
            {
                string AppInfo = "\r\n\r\n***WINTOOLKIT INFO***\r\n";
                AppInfo += "Win Toolkit v" + cMain.WinToolkitVersion(true);
            
                AppInfo += "\r\nWin Toolkit Directory: " + Environment.CurrentDirectory + "\r\n";

                List<Form> OpenForms = Application.OpenForms.Cast<Form>().ToList();
                if (OpenForms.Count > 1) { OpenForms.RemoveAt(0); }

                AppInfo += "Anti-Virus: " + cMain.DetectAntivirus().ToString() + "\r\n";
                AppInfo += "User Temp Path: " + cMain.UserTempPath + "\r\n";
                AppInfo += "WTK Temp Path: " + cOptions.WinToolkitTemp + "\r\n";
                if (OpenForms.Count > 0)
                {
                    string oForm = OpenForms.Aggregate("", (current, oF) => current + (oF.Name + ", "));
                    AppInfo += "Open Forms: " + oForm.Substring(0, oForm.Length - 2) + "\r\n";
                }
                if (cMain.FormHistory.Count > 0)
                {
                    string hForm = cMain.FormHistory.Cast<Form>()
                        .Aggregate("", (current, oF) => current + (oF.Name + ", "));
                    AppInfo += "\r\nForm History: " + hForm.Substring(0, hForm.Length - 2);
                }
                return AppInfo;
            }
        }

        private string GetRunningProcesses
        {
            get
            {
                string processes = "\r\n\r\n***PROCESSES***\r\n";
                try
                {
                    foreach (Process p in Process.GetProcesses().OrderBy(pn => pn.ProcessName))
                    {
                        try
                        {
                            processes += p.ProcessName;
                            string desc = p.MainModule.FileVersionInfo.FileDescription;
                            if (!string.IsNullOrEmpty(desc))
                            {
                                processes += " [" + desc + "]";
                            }
                            processes += " - " + cMain.BytesToString(p.WorkingSet64);
                            processes += Environment.NewLine;
                        }
                        catch (Exception)
                        { }
                    }
                }
                catch (Exception Ex)
                {
                    processes += "Error getting processes.\r\nEx: " + Ex.Message + Environment.NewLine;
                }
                return processes;
            }
        }

        private string GetComputerInfo
        {
            //GET OS INFO.
            get
            {
                string CompInfo = "\r\n\r\n***COMPUTER INFO***\r\n";
                CompInfo += "Computer Name: " + Environment.MachineName + "\r\n";
                CompInfo += "System Directory: " + Environment.SystemDirectory + "\r\n";
                CompInfo += "DISM Latest: " + DISM.Latest.Location + " _ " + DISM.Latest.Version + "\r\n";
                CompInfo += Environment.OSVersion + " (" + cMain.DefaultLang + ")\r\n";
                CompInfo += cReg.GetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ProductName");

                if (cMain.Arc64)
                {
                    CompInfo += " (64-bit)\r\n";
                }
                else
                {
                    CompInfo += " (32-bit)\r\n";
                }
                CompInfo += cReg.GetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "BuildLabEx") + "\r\n";
                CompInfo += "Resolution: W " + Screen.PrimaryScreen.Bounds.Width + " H " + Screen.PrimaryScreen.Bounds.Height + " D " + Screen.PrimaryScreen.BitsPerPixel + "\r\n";

                try
                {

                    ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
                    CompInfo += "\r\nCPU:\r\n";
                    foreach (ManagementObject mo in mos.Get())
                    {
                        CompInfo += "Name- " + mo["Name"] + "\r\n";
                        CompInfo += "Speed- " + mo["CurrentClockSpeed"] + "Mhz / " + mo["MaxClockSpeed"] + "Mhz\r\n";
                        CompInfo += "Cores- " + mo["NumberOfCores"] + " / " + mo["NumberOfLogicalProcessors"] + "\r\n";
                        CompInfo += "Status- " + mo["LoadPercentage"] + "% " + mo["Status"] + "\r\n";
                    }
                }
                catch
                {
                    CompInfo += "CPU Count: " + Environment.ProcessorCount.ToString() + "\r\n";
                }
                try
                {
                    if (cReg.RegCheckWIMFltr() || File.Exists(cMain.SysFolder + "\\driver\\wimfltr.sys"))
                    {
                        CompInfo += "WARNING: WIM Filter detected!\r\n";
                    }
                }
                catch { }

                try
                {
                    ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
                    ManagementObjectCollection results = searcher.Get();
                    CompInfo += "\r\n";
                    foreach (ManagementObject result in results)
                    {
                        int TVM = (Convert.ToInt32(result["TotalVisibleMemorySize"]));
                        int FPM = (Convert.ToInt32(result["FreePhysicalMemory"]));
                        int VM = (Convert.ToInt32(result["TotalVirtualMemorySize"]));
                        int FVM = (Convert.ToInt32(result["FreeVirtualMemory"]));

                        CompInfo += string.Format("Physical Memory: {0:#,##0} MB / {1:#,##0} MB", (TVM - FPM) / 1024, TVM / 1024);
                        CompInfo += " (" + string.Format("{0:#,##0} MB", FPM / 1024) + " Free)\r\n";

                        CompInfo += string.Format("Virtual Memory : {0:#,##0} MB / {1:#,##0} MB", (VM - FVM) / 1024, VM / 1024);
                        CompInfo += " (" + string.Format("{0:#,##0} MB", FVM / 1024) + " Free)\r\n";

                        break;
                    }
                }
                catch { }

                CompInfo += "\r\nDrives";
                DriveInfo[] DriveList = DriveInfo.GetDrives().Where(d => d.IsReady && (d.DriveType == DriveType.Fixed || d.DriveType == DriveType.Removable || d.DriveType == DriveType.CDRom)).ToArray();
                foreach (DriveInfo Drive in DriveList)
                {
                    CompInfo += "\r\n" + Drive.Name + "\t" + Drive.DriveType.ToString().Substring(0, 3) + "\t" + Drive.DriveFormat + "\t" + cMain.BytesToString(Drive.AvailableFreeSpace, true) + "  / " + cMain.BytesToString(Drive.TotalSize, true) + "\t" + Drive.VolumeLabel;
                }

                CompInfo += "\r\nnon-Ready Drives";
                try
                {

                    DriveInfo[] DriveList2 = DriveInfo.GetDrives().Where(d => !d.IsReady && (d.DriveType == DriveType.Fixed || d.DriveType == DriveType.Removable || d.DriveType == DriveType.CDRom)).ToArray();
                    if (DriveList2.Length > 0)
                    {
                        foreach (DriveInfo Drive in DriveList)
                        {
                            CompInfo += "\r\n" + Drive.Name + "\t" + Drive.DriveType.ToString().Substring(0, 3) + "\t" + Drive.VolumeLabel;
                        }
                    }
                    else
                    {
                        CompInfo = "\r\nNone";
                    }
                }
                catch { CompInfo += "\r\nError"; }
                return CompInfo;
            }
        }

        private string GetOptions
        {
            get
            {

                return "\r\n\r\n***OPTIONS***\r\n" + cOptions.SaveSettings();
            }
        }

        #endregion
    }

    /// <summary>
    /// Large errors which use the custom errorbox to show further information.
    /// </summary>
    public class LargeError : CustomError
    {
        public LargeError(string Title, string BriefDescription, Exception Exception) : this(Title, BriefDescription, "", Exception, null) { }

        public LargeError(string Title, string BriefDescription, string ExtendedDescription) : this(Title, BriefDescription, ExtendedDescription, null, null) { }

        public LargeError(string Title, string BriefDescription, string ExtendedDescription, Exception Exception) : this(Title, BriefDescription, ExtendedDescription, Exception, null) { }

        public LargeError(string Title, string BriefDescription, string ExtendedDescription, Exception Exception, ListViewItem ListViewItem)
        {
            this.Title = Title;
            this.BriefDescription = BriefDescription;
            this.ExtendedDescription = ExtendedDescription;
            this.ListViewItem = ListViewItem;
            this.Exception = Exception;
        }
    }

    /// <summary>
    /// Error type only used for testing.
    /// </summary>
    public class TestError : CustomError
    {
        public TestError()
        {
            this.BriefDescription = "This is a test upload.";
            this.ExtendedDescription = "A string with international characters: \r\nNorwegian: ÆØÅæøå\r\nChinese: 喂谢漢字/汉字\r\nGreek: δοκιμαστικό\r\nFrench: è à ù Î Â Ê\r\nGerman: ß Ö Ä\r\nItalian: ç À È Ì Ò Ù\r\nSpanish: ¿ º\r\nJapanese: 繋がって つながって";

            ListViewItem exampleLST = new ListViewItem();
            exampleLST.Text = "Test ListViewItem";
            exampleLST.SubItems.Add("Sub 1");
            exampleLST.SubItems.Add("Sub 2");
            this.ListViewItem = exampleLST;

            try
            {
                File.Move("sdfdf:\\4535", "derewr:\\");
            }
            catch (Exception Ex)
            {
                this.Exception = Ex;
            }
        }

        public void TestUpload(bool Save = true)
        {
            if (Save) { this.TestSave(); }
            cError.UploadList.Remove(this.Identifier);
            this.Upload();
        }

        public void TestSave()
        {
            cError.SaveList.Remove(this.Identifier);
            this.Save();
        }
    }

    /// <summary>
    /// Minor, low priority errors (non-Exceptions)
    /// </summary>
    public class SmallError : CustomError
    {

        public SmallError(string Description, Exception Exception) : this(Description, Exception, "", null) { }

        public SmallError(string Description, Exception Exception, string ExtendedDescription) : this(Description, Exception, ExtendedDescription, null) { }

        public SmallError(string Description, Exception Exception, ListViewItem ListViewItem) : this(Description, Exception, "", ListViewItem) { }

        public SmallError(string Description, Exception Exception, string ExtendedDescription, ListViewItem ListViewItem)
        {
            this.BriefDescription = Description;
            this.ExtendedDescription = ExtendedDescription;
            this.ListViewItem = ListViewItem;
            this.Exception = Exception;
        }

    }

    /// <summary>
    /// Any errors NOT in a try-catch statement.
    /// </summary>
    public class UnhandledError : CustomError
    {
        public UnhandledError(string Title, string BriefDescription, Exception Exception)
        {
            this.Title = Title;
            this.BriefDescription = BriefDescription;
            this.Exception = Exception;
        }
    }

    /// <summary>
    /// Old style error message which hasn't been updated yet.
    /// </summary>
    public class LegacyError : CustomError
    {
        public LegacyError(Form F, string BriefDescription, string Extra, string Ex)
        {
            this.Title = "Form: " + F.Name;
            this.BriefDescription = BriefDescription;
            this.ExtendedDescription = Extra + "\r\n\r\nException:\r\n" + Ex;

        }
    }
}
