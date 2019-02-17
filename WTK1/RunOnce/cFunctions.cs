using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;

namespace RunOnce
{

    public static class Extensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }

    public class IdleTimeFinder
    {
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        protected struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }

       

        public static uint GetIdleTime()
        {
            var lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return ((uint)Environment.TickCount - lastInPut.dwTime);
        }
        /// <summary>
        /// Get the Last input time in ticks
        /// </summary>
        /// <returns></returns>
        public static long GetLastInputTime()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
            if (!GetLastInputInfo(ref lastInPut))
            {
                throw new Exception(GetLastError().ToString());
            }
            return lastInPut.dwTime;
        }
    }

    class cError
    {
        public static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            string Err = Environment.NewLine + "***********************************" + Environment.NewLine;
            Err += "Version: " + cFunctions.WinToolkitVersion(true) + Environment.NewLine;
            Err += "Unhandled Exception: " + e.Message + Environment.NewLine;
            Err += "StackTrace: " + e.StackTrace + Environment.NewLine + Environment.NewLine;
            Err += "InnerException: " + e.InnerException + Environment.NewLine;
            Err += "Source: " + e.Source + Environment.NewLine;
            Err += "TargetSite: " + e.TargetSite + Environment.NewLine;
            Err += "Data: " + e.Data + Environment.NewLine;

            using (var SW = new StreamWriter(global.Root + "RunOnce_Error.log", true))
            {
                SW.Write(Err);
            }

        }
    }

    class cFunctions
    {
        public static string RemoveDuplication(string path)
        {
            var folders = new List<string>();

            foreach (string line in path.Split('\\').Where(line => !String.IsNullOrEmpty(line) && !folders.Contains(line)))
            {
                folders.Add(line);
            }
            return folders.Aggregate((first, second) => first + "\\" + second);
        }

        public static void WriteScript()
        {

            var sCurrentName = Process.GetCurrentProcess().ProcessName.Replace(".vshost", "");
            using (var streamWriter = new StreamWriter(global.Root + sCurrentName + ".vbs"))
            {

                streamWriter.WriteLine("If Not WScript.Arguments.Named.Exists(\"elevate\") Then");
                streamWriter.WriteLine("  CreateObject(\"Shell.Application\").ShellExecute WScript.FullName _");
                streamWriter.WriteLine("    , WScript.ScriptFullName & \" /elevate\", \"\", \"runas\", 1");
                streamWriter.WriteLine("  WScript.Quit");
                streamWriter.WriteLine("End If");
                streamWriter.WriteLine("");
                streamWriter.WriteLine("set svc=getobject(\"winmgmts:root\\cimv2\")");
                streamWriter.WriteLine("sQuery=\"select * from win32_process where name='WinToolkitRunOnce.exe'\"");
                streamWriter.WriteLine("set cproc=svc.execquery(sQuery)");
                streamWriter.WriteLine("iniproc=cproc.count    'it can be more than 1");
                streamWriter.WriteLine("Do While iniproc = 1");
                streamWriter.WriteLine("    wscript.sleep 1000");
                streamWriter.WriteLine("    set svc=getobject(\"winmgmts:root\\cimv2\")");
                streamWriter.WriteLine("    sQuery=\"select * from win32_process where name='WinToolkitRunOnce.exe'\"");
                streamWriter.WriteLine("    set cproc=svc.execquery(sQuery)");
                streamWriter.WriteLine("    iniproc=cproc.count");
                streamWriter.WriteLine("Loop");
                streamWriter.WriteLine("set cproc=nothing");
                streamWriter.WriteLine("set svc=nothing");
                streamWriter.WriteLine();
                streamWriter.WriteLine("wscript.sleep 500");
                streamWriter.WriteLine();
                streamWriter.WriteLine("Dim fso");
                streamWriter.WriteLine("Set fso = CreateObject(\"Scripting.FileSystemObject\")");
                streamWriter.WriteLine();
                streamWriter.WriteLine("if fso.FileExists(\"" + sCurrentName + ".exe\") then");
                streamWriter.WriteLine("            fso.DeleteFile \"" + sCurrentName + ".exe\"");
                streamWriter.WriteLine("end if");
                streamWriter.WriteLine("if fso.FileExists(\"" + sCurrentName + ".exe.config\") then");
                streamWriter.WriteLine("            fso.DeleteFile \"" + sCurrentName + ".exe.config\"");
                streamWriter.WriteLine("end if");
                streamWriter.WriteLine("if fso.FileExists(\"" + sCurrentName + ".vbs\") then");
                streamWriter.WriteLine("            fso.DeleteFile \"" + sCurrentName + ".vbs\"");
                streamWriter.WriteLine("end if");
            }

            OpenProgram(global.Root + sCurrentName + ".vbs", "", false, ProcessWindowStyle.Normal);
        }

        public static string RunExternal(string Filename, string Argument)
        {


            Filename = Filename.Replace("\\\\", "\\");
            Argument = Argument.Replace("\\\\", "\\");


            string R = "";

            try
            {
                using (var CMDprocess = new Process())
                {
                    CMDprocess.StartInfo.FileName = "cmd.exe";
                    CMDprocess.StartInfo.Arguments = "";
                    CMDprocess.StartInfo.RedirectStandardInput = true;
                    CMDprocess.StartInfo.RedirectStandardError = true;
                    CMDprocess.StartInfo.RedirectStandardOutput = true;
                    CMDprocess.StartInfo.UseShellExecute = false;
                    CMDprocess.StartInfo.CreateNoWindow = true;

                    CMDprocess.Start();

                    if (!Filename.StartsWith("\"")) { Filename = "\"" + Filename; }
                    if (!Filename.EndsWith("\"")) { Filename = Filename + "\""; }
                    using (StreamWriter SW = CMDprocess.StandardInput)
                    {
                        SW.WriteLine("Set SEE_MASK_NOZONECHECKS=1");
                        SW.WriteLine(Filename.Trim() + " " + Argument.Trim());
                        SW.WriteLine("exit");
                    }
                    using (StreamReader SR = CMDprocess.StandardOutput)
                    {
                        R = SR.ReadToEnd();
                    }
                }
            }
            catch (Exception Ex)
            {
                WriteLog("Error with RunOnce: \nFilename: " + Filename + "\nArgument: " + Argument + "\nException: " + Ex.Message);
            }

            return R;
        }

        public static void CleanupReg()
        {
            foreach (var value in GetAllValues(Registry.LocalMachine, "Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce"))
            {

                if (value.ToUpper().Contains("WINTOOLKIT") || value.ToUpper().Contains("WIN TOOLKIT") || value.ToUpper().Contains("W7T"))
                {
                    DeleteValue(Registry.LocalMachine, "Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", value);
                }

            }
        }

        public static void WriteResource(byte[] R, string Output)
        {
            Exception mainEx = null;
            if (!File.Exists(Output))
            {
                try
                {
                    string D = Path.GetDirectoryName(Output);
                    if (!Directory.Exists(D))
                    {
                        Directory.CreateDirectory(D);
                    }
                    File.WriteAllBytes(Output, R);
                }
                catch { }
            }

            if (!File.Exists(Output))
            {
                WriteLog("New resource missing!\n\n" + mainEx.Message);
            }
        }

        public static string[] GetAllValues(RegistryKey Loc, string Key)
        {

            Key = Key.Replace("\\\\", "\\");

            try
            {
                using (var oRegKey = Loc.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadSubTree))
                {
                    if (oRegKey != null) return oRegKey.GetValueNames();
                }
            }
            catch { }
            return null;
        }


        private static object locker = new object();
        public static void WriteLog(string sLog)
        {
            lock (locker)
            {
                string EC = "";

                if (!File.Exists(global.Root + "WinToolkit_RunOnce_Log.log"))
                {
                    EC += "Version: " + WinToolkitVersion(true) + Environment.NewLine;
                }

                EC += DateTime.Now + " Description: " + sLog + Environment.NewLine;
                using (var streamW = new StreamWriter(global.Root + "WinToolkit_RunOnce_Log.log", true))
                {
                    streamW.WriteLine(EC);
                }
            }
        }

        public static void WriteValue(RegistryKey Loc, string Key, string Reg, object Value, RegistryValueKind Kind = RegistryValueKind.String)
        {
            try
            {
                using (var oRegKey = Loc.OpenSubKey(Key, true))
                {
                    oRegKey.SetValue(Reg, Value, Kind);
                    WriteLog("WriteValue:" + Loc + "\\" + Key + " | " + Reg + "=" + Value + " | " + Kind);
                }
            }
            catch { }
        }

        public static void DeleteValue(RegistryKey Loc, string Key, string Reg)
        {
            try
            {
                using (var oRegKey = Loc.OpenSubKey(Key, true))
                {
                    if (oRegKey != null) oRegKey.DeleteValue(Reg);
                    WriteLog("DeleteValue:" + Loc + "\\" + Key + " | " + Reg);
                }
            }
            catch { }
        }

        public static string WinToolkitVersion(bool Full = false)
        {
            int iMajor = Assembly.GetExecutingAssembly().GetName().Version.Major;
            int iMinor = Assembly.GetExecutingAssembly().GetName().Version.Minor;
            int iBuild = Assembly.GetExecutingAssembly().GetName().Version.Build;
            int iRevision = Assembly.GetExecutingAssembly().GetName().Version.Revision;

            if (Full)
            {
                return iMajor + "." + iMinor + "." + iBuild + "." + iRevision;
            }
            return iMajor + "." + iMinor + "." + iBuild;
        }

        public static void UpdateLabel(Control lbl, string text)
        {
            lbl.BeginInvoke((Action)(() =>
            {
                lbl.Text = text;
            }));
        }

        public static void DeleteKey(RegistryKey Loc, string pKey, string cKey)
        {
            pKey = ReplaceText(pKey, "\\\\", "\\");
            try
            {
                using (var nRegKey = Loc.OpenSubKey(pKey, true))
                {
                    nRegKey.DeleteSubKeyTree(cKey);
                    WriteLog("DeleteKey:" + Loc + "\\" + pKey + "\\" + cKey);
                }
            }
            catch { }
        }


        public static void DeleteFile(string F)
        {
            if (File.Exists(F))
            {
                try
                {
                    File.Delete(F);
                }
                catch { }
            }
        }

        private static bool MSIRequireQuiet(string switchString)
        {
            if (switchString.Contains("/PASSIVE")) { return false; }
            if (switchString.Contains("/QUIET")) { return false; }
            if (switchString.Contains("/QN")) { return false; }
            if (switchString.Contains("/QB")) { return false; }
            if (switchString.Contains("/QR")) { return false; }
            if (switchString.Contains("/QF")) { return false; }
            return true;
        }

        public static void InstallFile(ListViewItem LST)
        {
            string sMSWINSCK = global.SysFolder + "\\mswinsck.ocx";
            if (File.Exists(sMSWINSCK) && File.GetAttributes(sMSWINSCK) != FileAttributes.Normal)
            {
                File.SetAttributes(sMSWINSCK, FileAttributes.Normal);
            }

            string sExtension = Path.GetExtension(LST.SubItems[1].Text).ToUpper();
            switch (sExtension)
            {
                case ".MSP":
                    //msiexec /update C:\1boot\Office2013.MSP /q /passive
                    RunExternal("\"" + global.SysFolder + "\\msiexec.exe\" /update \"" + LST.SubItems[1].Text + "\" /q /passive /norestart", LST.SubItems[2].Text);
                    break;
                case ".MSI":
                    string installSwitch = "/i";
                    if (MSIRequireQuiet(LST.SubItems[2].Text)) { LST.SubItems[2].Text += " /passive"; }
                    if (!LST.SubItems[2].Text.ToUpper().Contains("/NORESTART")) { LST.SubItems[2].Text += " /norestart"; }

                    if (sExtension.Equals(".MSP", StringComparison.InvariantCultureIgnoreCase)) { installSwitch = "/p"; }
                    RunExternal("\"" + global.SysFolder + "\\msiexec.exe\" " + installSwitch + " \"" + LST.SubItems[1].Text + "\"", LST.SubItems[2].Text);
                    break;
               case ".MSU":
                    RunExternal("\"" + global.SysFolder + "\\wusa.exe\"", "\"" + LST.SubItems[1].Text + "\" /quiet /norestart");
                    break;
                case ".BAT":
                    RunExternal("\"" + LST.SubItems[1].Text + "\"","");
                    break;
                case ".CAB":
                    RunExternal("\"" + global.SysFolder + "\\DISM.exe\"", "/Online /Add-Package /PackagePath:\"" + LST.SubItems[1].Text + "\" /Quiet /NoRestart");
                    break;
                default:
                    RunExternal("\"" + LST.SubItems[1].Text + "\"", LST.SubItems[2].Text);
                    break;
            }

            Thread.Sleep(1000);
            OpenProgram("Shutdown.exe", "-a", true, ProcessWindowStyle.Hidden);
        }

        /// <summary>
        /// Replaces text ignoring case.
        /// </summary>
        /// <param name="Input">Text to be inspected</param>
        /// <param name="Replace">Text to search for</param>
        /// <param name="ReplaceWith">New text for 'Replace'</param>
        /// <returns></returns>
        public static string ReplaceText(string Input, string Replace, string ReplaceWith, bool TrimEnds = true)
        {
            string NewString = Input;

            try
            {
                StringBuilder sb = new StringBuilder();

                int previousIndex = 0;
                int index = NewString.IndexOf(Replace, StringComparison.CurrentCultureIgnoreCase);
                while (index != -1)
                {
                    sb.Append(NewString.Substring(previousIndex, index - previousIndex));
                    sb.Append(ReplaceWith);
                    index += Replace.Length;

                    previousIndex = index;
                    index = NewString.IndexOf(Replace, index, StringComparison.CurrentCultureIgnoreCase);
                }
                sb.Append(NewString.Substring(previousIndex));

                NewString = sb.ToString();
            }
            catch
            {

            }
            return NewString;
        }

        public static int OpenProgram(string Filename, string Arguments, bool Wait, ProcessWindowStyle WindowStyle)
        {
            int iAppErr = -1;
            try
            {
                using (var SI = new Process())
                {
                    if (!String.IsNullOrEmpty(Arguments))
                    {
                        SI.StartInfo.Arguments = Arguments;
                    }
                    SI.StartInfo.WindowStyle = WindowStyle;

                    if (!Filename.EndsWith("\"")) { Filename += "\""; }
                    if (!Filename.StartsWith("\"")) { Filename = "\"" + Filename; }

                    if (!Filename.Contains("\\") && File.Exists(global.SysFolder + "\\" + Filename))
                    {
                        Filename = Filename.Replace(Filename, global.SysFolder + "\\" + Filename);
                    }

                    SI.StartInfo.FileName = Filename;
                    SI.Start();

                    if (Wait) { SI.WaitForExit(); }
                    else
                    {
                        return 0;
                    }
                    iAppErr = SI.ExitCode;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + Environment.NewLine + Filename + " " + Arguments, "Error");
            }
            return iAppErr;
        }
    }
}
