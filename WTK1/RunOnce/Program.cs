using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
namespace RunOnce
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [DllImport("user32.dll")]
        public extern static bool ShutdownBlockReasonCreate(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string pwszReason);

        [DllImport("user32.dll")]
        public extern static bool ShutdownBlockReasonDestroy(IntPtr hWnd);

        static System.Reflection.Assembly AssemblyResolveHandler(object sender, ResolveEventArgs args)
        {
            string rName = args.Name;
            while (rName.Contains(","))
            {
                rName = rName.Substring(0, rName.Length - 1);
            }

            switch (rName)
            {//Interop.SHDocVw
                case "System.Threading":
                    return System.Reflection.Assembly.Load(Properties.Resources.System_Threading);
            }

            return null;
        }

        [STAThread]
        static void Main()
        {
            bool createdNew;

            using (new Mutex(true, System.Reflection.Assembly.GetExecutingAssembly().FullName, out createdNew))
            {
                if (createdNew)
                {
                    cFunctions.WriteLog("\r\n-----------------STARTING--------------\r\n\r\n");
                    AppDomain.CurrentDomain.UnhandledException += cError.MyHandler;

                    cFunctions.WriteLog("IsSystem: " + IsSystem() + "\r\nIsAdministrator: " + IsAdministrator() + "\r\nUser: " + WindowsIdentity.GetCurrent().Name);

                    if (IsSystem())
                    {
                        cFunctions.CleanupReg();
                        cFunctions.WriteValue(Microsoft.Win32.Registry.LocalMachine, "Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", "1WinToolkit", "WinToolkitRunOnce.exe");

                        cFunctions.WriteLog("Running as system. Aborting...");
                        return;
                    }

                    if (!IsAdministrator())
                    {
                        cFunctions.WriteLog("Running as user. Aborting...");
                        return;
                    }

                    cFunctions.CleanupReg();

                    var bShutdownHandle = false;
                    try
                    {
                        cFunctions.WriteLog("Creating Shutdown Handle.");
                        bShutdownHandle = true;
                        ShutdownBlockReasonCreate(Process.GetCurrentProcess().Handle, "Installing applications");
                    }
                    catch (Exception ex)
                    {
                        cFunctions.WriteLog("Error blocking shutdown: " + ex.Message);
                    }

                    AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolveHandler;

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    cFunctions.WriteLog("Loading...");

                    Application.Run(new frmStartup());

                    cFunctions.WriteLog("Manual: " + global.ManualInstalls.Count + " | Driver: " + global.DriverInstalls.Count + " | Auto: " + global.AutoInstalls.Count);
                    cFunctions.WriteLog("InstallPaths: " + global.InstallPaths.Count);
                    if (global.ManualInstalls.Count > 0 || global.DriverInstalls.Count > 0 || global.AutoInstalls.Count > 0)
                    {
                        cFunctions.WriteLog("Starting...");
                        Application.Run(new FrmInstall());
                    }

                    cFunctions.WriteLog("Deleting dpinst.exe");
                    cFunctions.DeleteFile(global.Root + "dpinst.exe");

                    if (bShutdownHandle)
                    {
                        try
                        {
                            cFunctions.WriteLog("Removing Shutdown Handle.");
                            ShutdownBlockReasonDestroy(Process.GetCurrentProcess().Handle);
                        }
                        catch (Exception ex)
                        {
                            cFunctions.WriteLog("Error ApplicationExitCall: " + ex.Message);
                        }
                    }

                    if (global.bRestart)
                    {
                        if (global.bShowCountdown)
                            Application.Run(new frmRestart("All Done", "Your system will now restart.", Color.LightGreen, true));
                        cFunctions.OpenProgram("shutdown.exe", "-R -T 1", false, ProcessWindowStyle.Hidden);
                    }

                    //The script which removes the RunOnce installer after it has finished.
                    cFunctions.WriteScript();
                }
            }
        }

        private static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static bool IsSystem()
        {
            using (var identity = WindowsIdentity.GetCurrent())
            {
                return identity != null && identity.IsSystem;
            }
        }




    }
}
