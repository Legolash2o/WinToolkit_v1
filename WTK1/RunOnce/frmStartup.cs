using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Linq;

namespace RunOnce
{




    public partial class frmStartup : Form
    {



        private const int TIME_TO_WAIT = 50;
        public frmStartup()
        {
            InitializeComponent();
            Icon = Properties.Resources.W7T_128;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        private void frmStartup_Shown(object sender, EventArgs e)
        {
            lblStatus.Text = "Detecting Drives";
            cFunctions.WriteLog("Detecting Drives");
            Application.DoEvents();
            System.Threading.Thread.Sleep(TIME_TO_WAIT);

            string driveScan = "\r\nDRIVE SCAN\n------------------------------\r\n\r\n";

            foreach (DiskDrive d in global.DriveList)
            {
                driveScan += string.Format("Drive: {0}, Media Type: {1}, Size: {2}, Free: {3}, Name: {4}, Initial: {5}", d.DriveLetter, d.MediaType, d.Size, d.Freespace, d.VolumeName, d.InitialFind) + "\r\n";
            }

            driveScan += "------------------------------";

            cFunctions.WriteLog(driveScan);

            var Tasks = new List<Task>();

            //Detect App Folder
            Tasks.Add(Task.Factory.StartNew(() =>
            {
                Parallel.ForEach(global.DriveList, D =>
                {
                    if (Directory.Exists(D.DriveLetter + "WinToolkit_Apps"))
                        new InstallLocation(D.DriveLetter, InstallLocation.Type.Setup).Add();
                    if (Directory.Exists(D.DriveLetter + "Sources\\WinToolkit_Apps"))
                        new InstallLocation(D.DriveLetter + "Sources", InstallLocation.Type.Setup).Add();
                });
            }));

            Task.WaitAll(Tasks.ToArray());
            Tasks.Clear();

            //Detect drivers folder
            cFunctions.WriteLog("Detecting Paths");
            lblStatus.Text = "Detecting Paths";
            pbLoad.Value++;
            Application.DoEvents();
            System.Threading.Thread.Sleep(TIME_TO_WAIT);

            Tasks.Add(Task.Factory.StartNew(() =>
            {
                Parallel.ForEach(global.DriveList, drive =>
                {
                    if (Directory.Exists(drive.DriveLetter + "Drivers"))
                        new InstallLocation(drive.DriveLetter + "Drivers", InstallLocation.Type.Driver).Add();
                    if (Directory.Exists(drive.DriveLetter + "Sources\\Drivers"))
                        new InstallLocation(drive.DriveLetter + "Sources\\Drivers", InstallLocation.Type.Driver).Add();
                });
            }));

            cFunctions.WriteLog("Detecting Sources");
            lblStatus.Text = "Detecting Sources";
            pbLoad.Value++;
            Application.DoEvents();
            System.Threading.Thread.Sleep(TIME_TO_WAIT);

            //Read install.ini file
            Tasks.Add(Task.Factory.StartNew(() =>
            {
                foreach (DiskDrive D in global.DriveList)
                {
                    string sFile = D.DriveLetter + "sources\\install.ini";
                    if (!File.Exists(sFile))
                    {
                        continue;
                    }

                    new InstallLocation(D.DriveLetter, InstallLocation.Type.Setup).Add();
                    cFunctions.WriteLog("INI Found: " + sFile);

                    string sInput = "";
                    using (var SR = new StreamReader(sFile))
                    {
                        sInput = SR.ReadToEnd();
                    }

                    int T = -1;
                    foreach (string sLine in sInput.Split(Environment.NewLine.ToCharArray()))
                    {
                        if (sLine.StartsWith("#") || string.IsNullOrEmpty(sLine))
                        {
                            continue;
                        }
                        if (sLine.ToUpper().StartsWith("["))
                        {
                            if (T == 0)
                            {
                                break;
                            }
                            T = -1;
                        }
                        if (sLine.ToUpper().StartsWith("[CONFIG]"))
                        {
                            T = 0;
                            continue;
                        }

                        if (T == 0 && sLine.Contains("="))
                        {
                            string sPart1 = sLine.Split('=')[0].Trim();
                            string sPart2 = sLine.Split('=')[1].Trim();

                            if (sPart1.ToUpper().StartsWith("INSTALLDIR"))
                            {
                                new InstallLocation(sPart2, InstallLocation.Type.Setup).Add();
                            }
                            if (sPart1.ToUpper().StartsWith("DRIVERDIR"))
                            {
                                new InstallLocation(sPart2, InstallLocation.Type.Driver).Add();
                            }
                            if (sPart1.ToUpper().StartsWith("REBOOT"))
                            {
                                global.bRestart = bool.Parse(sPart2);
                                cFunctions.WriteLog("Restart set to: " + global.bRestart);
                            }
                            if (sPart1.ToUpper().StartsWith("COUNTDOWN"))
                            {
                                global.bShowCountdown = bool.Parse(sPart2);
                                cFunctions.WriteLog("ShowCountdown set to: " + global.bShowCountdown);
                            }
                        }
                    }
                }
            }));

            Task.WaitAll(Tasks.ToArray());
            Tasks.Clear();

            cFunctions.WriteLog("Detecting Installs");
            lblStatus.Text = "Detecting Installs";
            pbLoad.Value++;
            Application.DoEvents();
            System.Threading.Thread.Sleep(TIME_TO_WAIT);

            Tasks.Add(Task.Factory.StartNew(() =>
            {
                foreach (DiskDrive D in global.DriveList)
                {
                    string sFile = D.DriveLetter + "sources\\install.ini";
                    if (!File.Exists(sFile))
                    {
                        continue;
                    }
                    cFunctions.WriteLog("INI Found: " + sFile);

                    string sInput = "";
                    using (StreamReader SR = new StreamReader(sFile))
                    {
                        sInput = SR.ReadToEnd();
                    }

                    int T = -1;


                    foreach (string sLine in sInput.Split(Environment.NewLine.ToCharArray()))
                    {
                        try
                        {
                            if (sLine.StartsWith("#") || string.IsNullOrEmpty(sLine)) { continue; }
                            if (sLine.ToUpper().StartsWith("[")) { T = -1; }
                            if (sLine.ToUpper().StartsWith("[AUTOMATIC]")) { T = 1; continue; }
                            if (sLine.ToUpper().StartsWith("[MANUAL]")) { T = 2; continue; }
                            if (sLine.ToUpper().StartsWith("[COMMANDS]")) { T = 3; continue; }
                            if (sLine.ToUpper().StartsWith("INSTALLDIR")) { continue; }
                            if (sLine.ToUpper().StartsWith("DRIVERDIR")) { continue; }

                            if (sLine.Contains("=") && (T == 1 || T == 2 || T == 3))
                            {
                                string sPart1 = sLine.Split('=')[0].Trim();
                                string sPart2 = sLine.Substring(sPart1.Length + 1);
                                string sPart3 = "";
                                if (sPart2.Contains("*"))
                                {
                                    sPart2 = sPart2.Split('*')[0].Trim();
                                    sPart3 = sLine.Substring(sPart1.Length + sPart2.Length + 2);
                                }

                                string log = "";


                                string foundDVD = "";

                                if (sPart2.ToUpper().StartsWith("%DVD%"))
                                {
                                    Parallel.ForEach(global.InstallPaths.Where(t => t.InstallType == InstallLocation.Type.Setup), p =>
                                    {
                                        string testPath = cFunctions.ReplaceText(sPart2, "%DVD%:", p.Location);
                                        testPath = cFunctions.ReplaceText(testPath, "%DVD%", p.Location);

                                        log += "P2: " + p.Location + " || " + testPath + Environment.NewLine;

                                        if (File.Exists(testPath))
                                        {
                                            sPart2 = testPath;
                                            foundDVD = p.Location;
                                            return;
                                        }

                                        string duplicateCheck = cFunctions.RemoveDuplication(testPath);
                                        if (File.Exists(duplicateCheck))
                                        {
                                            sPart2 = duplicateCheck;
                                            foundDVD = Directory.GetParent(duplicateCheck).Parent.FullName;
                                            log += "P2_duplicate: " + p.Location + " || " + sPart2 + Environment.NewLine;
                                        }
                                    });
                                }

                                if (sPart3.ToUpperInvariant().Contains("%DVD%") && !string.IsNullOrEmpty(foundDVD))
                                {
                                    sPart3 = cFunctions.ReplaceText(sPart3, "%DVD%:", foundDVD);
                                    sPart3 = cFunctions.ReplaceText(sPart3, "%DVD%", foundDVD);


                                    log += "P3: " + sPart3 + Environment.NewLine;
                                }

                                if (sPart3.ToUpperInvariant().Contains("%APP%"))
                                {
                                    sPart3 = cFunctions.ReplaceText(sPart3, "%APP%:", Path.GetDirectoryName(sPart2));
                                    sPart3 = cFunctions.ReplaceText(sPart3, "%APP%", Path.GetDirectoryName(sPart2));

                                    log += "P3_2: " + sPart3 + Environment.NewLine;
                                }

                                if (sPart2.StartsWith("%") || (!File.Exists(sPart2) && !File.Exists(global.SysFolder + "\\" + sPart2)))
                                {
                                    cFunctions.WriteLog("ERROR INI P1: " + sPart1 + "\r\nP2: " + sPart2 + "\r\nP3: " + sPart3);
                                    cFunctions.WriteLog(log);
                                    continue;
                                }

                                ListViewItem LST = new ListViewItem();
                                LST.Text = sPart1;
                                if (String.Equals(sPart2, "N/A (Not Needed)",
                                   StringComparison.InvariantCultureIgnoreCase))
                                {
                                    sPart2 = "";
                                }

                                LST.SubItems.Add(Clean(sPart2));
                                LST.SubItems.Add(Clean(sPart3));

                                //Commented out because it does not work if a file contains both
                                //x86 and x64 in the file name.

                                //bool skip = false;

                                //foreach (ListViewItem.ListViewSubItem lv in LST.SubItems)
                                //{
                                //    if (global.Is64bit && lv.Text.Contains("x86", StringComparison.OrdinalIgnoreCase))
                                //    {
                                //        skip = true;
                                //        break;
                                //    }

                                //    if (!global.Is64bit && lv.Text.Contains("x64", StringComparison.OrdinalIgnoreCase))
                                //    {
                                //        skip = true;
                                //        break;
                                //    }
                                //}

                                //if (skip)
                                //    continue;


                                global.Found++;
                                if (sPart2.ToUpper().EndsWith(".MSU") || sPart2.ToUpper().EndsWith(".CAB")) { T = 3; }

                                switch (T)
                                {
                                    case 1:
                                        lock (global.AutoInstalls)
                                        {
                                            cFunctions.WriteLog("Auto INI Added: " + LST.Text + "\r\nP1: " + sPart1 + "\r\nP2: " + sPart2 + "\r\nP3: " + sPart3);
                                            global.AutoInstalls.Add(LST);
                                        }
                                        continue;
                                    case 2:
                                        lock (global.ManualInstalls)
                                        {
                                            cFunctions.WriteLog("Manual INI Added: " + LST.Text + "\r\nP1: " + sPart1 + "\r\nP2: " + sPart2 + "\r\nP3: " + sPart3);
                                            global.ManualInstalls.Add(LST);
                                        }
                                        continue;
                                    case 3:
                                        lock (global.PostCommands)
                                        {
                                            cFunctions.WriteLog("Command INI Added: " + LST.Text + "\r\nP1: " + sPart1 + "\r\nP2: " + sPart2 + "\r\nP3: " + sPart3);
                                            global.PostCommands.Add(LST);
                                        }
                                        continue;
                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            cFunctions.WriteLog("Error getting Text: " + sLine + "\nEx: " + Ex.Message);
                        }
                    }
                };
            }));

            Tasks.Add(Task.Factory.StartNew(() =>
            {

                using (RegistryKey Key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WinToolkit"))
                    if (Key != null)
                    {

                        foreach (string RK in Key.GetValueNames())
                        {

                            if (RK.Length < 6) { continue; }
                            if (RK.Substring(5, 1) != "|") { continue; }

                            string rkValue = Key.GetValue(RK).ToString();
                            try
                            {

                                cFunctions.WriteLog("Entry Found: " + RK + "\r\nValue: " + rkValue);

                                bool Auto = false;
                                if (rkValue.StartsWith("|"))
                                {
                                    Auto = true;
                                    rkValue = rkValue.Substring(1);
                                }

                                string sPart1 = rkValue;
                                string sPart2 = "";
                                if (rkValue.ToUpperInvariant().Contains("*"))
                                {
                                    sPart1 = rkValue.Split('*')[0];
                                    sPart2 = rkValue.Substring(sPart1.Length + 1);
                                }

                                if (sPart1.ToUpper().StartsWith("%DVD%:"))
                                {
                                    Parallel.ForEach(global.InstallPaths.Where(t => t.InstallType == InstallLocation.Type.Setup), p =>
                                    {
                                        string testPath = p.Location + cFunctions.ReplaceText(sPart1, "%DVD%:", "", true);
                                        testPath = cFunctions.ReplaceText(testPath, "%DVD%", "", true);

                                        if (File.Exists(testPath)) { sPart1 = testPath; }
                                    });
                                }

                                if (sPart2.ToUpperInvariant().Contains("%DVD%"))
                                {
                                    Parallel.ForEach(global.InstallPaths.Where(t => t.InstallType == InstallLocation.Type.Setup), p =>
                                    {
                                        sPart2 = cFunctions.ReplaceText(sPart2, "%DVD%:", p.Location, true);
                                        sPart2 = cFunctions.ReplaceText(sPart2, "%DVD%", p.Location, true);
                                    });
                                }

                                if (sPart2.ToUpperInvariant().Contains("%APP%"))
                                {
                                    sPart2 = cFunctions.ReplaceText(sPart2, "%APP%", Path.GetDirectoryName(sPart1) + "\\", true);
                                    sPart2 = sPart2.ToUpperInvariant().Replace("\\\\", "\\");
                                }
                                cFunctions.WriteLog("File Found P1: " + sPart1 + "\r\nP2: " + sPart2);
                                if (sPart1.StartsWith("%") || !File.Exists(sPart1))
                                {
                                    if (sPart2.StartsWith("%") || !File.Exists(sPart2))
                                    {
                                        cFunctions.WriteLog("ERROR REG P1: " + sPart1 + "\r\nP2: " + sPart2);
                                    }
                                    continue;
                                }

                                ListViewItem LST = new ListViewItem();
                              
                                LST.Text = RK.Substring(6);
                                LST.SubItems.Add(sPart1);
                                if (String.Equals(sPart2, "N/A (Not Needed)",
                                    StringComparison.InvariantCultureIgnoreCase))
                                {
                                    sPart2 = "";
                                }

                                LST.SubItems.Add(Clean(sPart2));
                                LST.ToolTipText = sPart2;


                                //Commented out because it does not work if a file contains both
                                //x86 and x64 in the file name.

                                //bool skip = false;

                                //foreach (ListViewItem.ListViewSubItem lv in LST.SubItems)
                                //{
                                //    if (global.Is64bit && lv.Text.Contains("x86", StringComparison.OrdinalIgnoreCase))
                                //    {
                                //        skip = true;
                                //        break;
                                //    }

                                //    if (!global.Is64bit && lv.Text.Contains("x64", StringComparison.OrdinalIgnoreCase))
                                //    {
                                //        skip = true;
                                //        break;
                                //    }
                                //}

                                //if (skip)
                                //    continue;

                                global.Found++;
                                if (Auto)
                                {
                                    cFunctions.WriteLog("Auto Added: " + LST.Text + "\r\nP1: " + sPart1 + "\r\nP2: " + sPart2);
                                    lock (global.AutoInstalls)
                                    {
                                        global.AutoInstalls.Add(LST);
                                    }
                                }
                                else
                                {
                                    cFunctions.WriteLog("Manual Added: " + LST.Text + "\r\nP1: " + sPart1 + "\r\nP2: " + sPart2);
                                    lock (global.ManualInstalls)
                                    {
                                        global.ManualInstalls.Add(LST);
                                    }
                                }
                            }
                            catch (Exception Ex)
                            {
                                cFunctions.WriteLog("Error getting Key: " + RK + "\nValue" + rkValue + "\nEx: " + Ex.Message);
                            }
                        }

                    }

            }));

            Task.WaitAll(Tasks.ToArray());
            Tasks.Clear();

            cFunctions.WriteLog("Detecting Drivers");
            lblStatus.Text = "Detecting Drivers";
            pbLoad.Value++;
            Application.DoEvents();
            System.Threading.Thread.Sleep(TIME_TO_WAIT);

            bool x64 = Directory.Exists(global.SysRoot + "\\SysWOW64");
            cFunctions.WriteLog("x64: " + x64);
            Tasks.Add(Task.Factory.StartNew(() =>
            {

                Parallel.ForEach(global.InstallPaths.Where(I => I.InstallType == InstallLocation.Type.Driver), D =>
                {
                    try
                    {
                        string[] INFiles = null;

                        if (x64)
                        {
                            INFiles = Directory.GetFiles(D.Location, "*.inf", SearchOption.AllDirectories).Where(s => !s.ToUpperInvariant().Contains("\\X86") && !s.ToUpperInvariant().Contains("X86\\")).ToArray();
                        }
                        else
                        {
                            INFiles = Directory.GetFiles(D.Location, "*.inf", SearchOption.AllDirectories).Where(s => !s.ToUpperInvariant().Contains("\\X64") && !s.ToUpperInvariant().Contains("X64\\")).ToArray();
                        }

                        Parallel.ForEach(INFiles, INF =>
                        {

                            lock (global.DriverInstalls)
                            {
                                ListViewItem LST = new ListViewItem();

                                string sName = cFunctions.ReplaceText(INF, D.Location, "", true);
                                if (!sName.EndsWith("\\")) { sName += "\\"; }
                                LST.Text = sName + Path.GetFileName(INF);

                                LST.SubItems.Add(Path.GetDirectoryName(INF));
                                LST.SubItems.Add(INF);
                                LST.ToolTipText = INF;
                                global.DriverInstalls.Add(LST);
                            }
                        });
                    }
                    catch (Exception Ex)
                    {
                        cFunctions.WriteLog("Error Detecting Drivers: " + D.Location + "\nEx: " + Ex.Message);
                    }
                });

            }));

            Task.WaitAll(Tasks.ToArray());
            Tasks.Clear();

            lblStatus.Text = "Removing Duplicates";
            cFunctions.WriteLog("Removing Duplicates.");
            pbLoad.Value++;
            Application.DoEvents();
            System.Threading.Thread.Sleep(TIME_TO_WAIT);

            global.AutoInstalls = global.AutoInstalls.Distinct().ToList();
            global.ManualInstalls = global.ManualInstalls.Distinct().ToList();

            global.DriverInstalls = global.DriverInstalls.Distinct().OrderBy(o => o.SubItems[1].Text).ToList();

            pbLoad.Value = pbLoad.Maximum; ;
            if (global.ManualInstalls.Count == 0 && global.DriverInstalls.Count == 0 && global.AutoInstalls.Count == 0)
            {


                if (global.InstallPaths.Count == 0 && global.Found > 0)
                {
                    lblStatus.Text = "Aborting";
                    cFunctions.WriteLog("No installation Paths");
                    if (global.bShowCountdown)
                        new frmRestart("No Installation Paths", "Your installation media could not be found.", Color.LightPink, false).ShowDialog();
                }
                else
                {
                    lblStatus.Text = "Closing";
                    if (global.bShowCountdown)
                        new frmRestart("No Installations", "Windows installation will now continue.", Color.LightGreen,
                            false, 5).ShowDialog();
                }


                Close();
            }
            lblStatus.Text = "Starting";
            cFunctions.WriteLog("Starting");
            Close();
        }

        private void frmStartup_Load(object sender, EventArgs e)
        {

        }

        private string Clean(string input)
        {
            string i = input;
            while (i.Contains("\\\\"))
            {
                i = i.Replace("\\\\", "\\");
            }
            return i;
        }

    }


}
