using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using WinToolkit.Classes;

namespace WinToolkit
{
    static class Program
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static System.Reflection.Assembly AssemblyResolveHandler(object sender, ResolveEventArgs args)
        {
            string rName = args.Name;
            while (rName.ContainsIgnoreCase(","))
            {
                rName = rName.Substring(0, rName.Length - 1);
            }

            switch (rName)
            {//Interop.SHDocVw
                case "Interop.IWshRuntimeLibrary":
                    return System.Reflection.Assembly.Load(Properties.Resources.Interop_IWshRuntimeLibrary);
                case "Interop.SHDocVw":
                    return System.Reflection.Assembly.Load(Properties.Resources.Interop_SHDocVw);
                case "Vista Api":
                    return System.Reflection.Assembly.Load(Properties.Resources.Vista_Api);
            }

            return null;
        }

        [STAThread]
        static void Main()
        {
            bool createdNew;

            using (new Mutex(true, System.Reflection.Assembly.GetExecutingAssembly().FullName, out createdNew))
            {
                if (createdNew || Environment.CommandLine.ContainsIgnoreCase("/MULTI"))
                {
                    if (Environment.CommandLine.ContainsIgnoreCase("/?") || Environment.CommandLine.ContainsIgnoreCase("/HELP"))
                    {
                        MessageBox.Show(
                            "/SKIPOSCheck - Skips OS detection on startup.\n/SKIPSettings - Do not load 'Settings.txt' on startup.\n/SKIPUPDATE - Does not look for new versions on startup.\n/SKIPWIMFILTER - Does not check for WimFilter\n/SKIPWIMFILTERRESTART - Uninstalls WimFilter but doesn't require restart.",
                            "HELP");
                        Environment.Exit(0);
                    }

                    if (IsUserAdministrator() == false)
                    {
                        MessageBox.Show("You need to be an administrator to use this application!", "Exiting!");
                        Environment.Exit(0);
                    }

                    Application.ThreadException += cMain.MyHandler;
                    AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolveHandler;

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    CheckArgs();

                    if (!System.IO.Directory.Exists(cMain.UserTempPath + "\\Files\\"))
                    {
                        cMain.CreateDirectory(cMain.UserTempPath + "\\Files\\");
                    }
                    AppDomain.CurrentDomain.AppendPrivatePath(cMain.UserTempPath + "\\Files\\");
                    Application.Run(new frmStartup());
                    try
                    {
                        cNotify.Notify.Visible = false;
                        cNotify.Notify.Dispose();
                    }
                    catch { }

                }
                else
                {
                    var current = Process.GetCurrentProcess();
                    foreach (var process in Process.GetProcessesByName(current.ProcessName).Where(process => process.Id != current.Id))
                    {
                        SetForegroundWindow(process.MainWindowHandle);
                        Environment.Exit(0);
                    }
                }
            }
        }

        private static void CheckArgs()
        {
            foreach (string A in Environment.GetCommandLineArgs())
            {
                if (A.ToUpper().EqualsIgnoreCase("/SKIPOSCHECK")) { cOptions.SkipOSCheck = true; }
                if (A.ToUpper().EqualsIgnoreCase("/SKIPSETTINGS")) { cOptions.SkipSettings = true; }
                if (A.ToUpper().EqualsIgnoreCase("/SKIPWIMFILTER")) { cOptions.SkipWimFltr = true; }
                if (A.ToUpper().EqualsIgnoreCase("/SKIPWIMFILTERRESTART")) { cOptions.SkipWimFltrRestart = true; }
                if (A.ToUpper().EqualsIgnoreCase("/SKIPUPDATE")) { cOptions.SkipUpdate = true; }
            }
        }

        private static bool IsUserAdministrator()
        {
            var user = WindowsIdentity.GetCurrent();

            if (user != null)
            {
                return new WindowsPrincipal(user).IsInRole(WindowsBuiltInRole.Administrator);
            }
            return false;
        }
    }
}
