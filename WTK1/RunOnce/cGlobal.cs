using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Linq;
namespace RunOnce
{

    class global
    {
        public static string SysFolder = Environment.GetEnvironmentVariable("SystemRoot") + "\\System32";
        public static string SysRoot = Environment.GetEnvironmentVariable("SystemRoot");
        public static string Root = Application.StartupPath + "\\";

        public static bool Is64bit = Directory.Exists(Environment.GetEnvironmentVariable("SystemRoot") + "\\SysWOW64");

        public static bool bRestart = false;
        public static bool bShowCountdown = true;

        public static int Found = 0;
        public static List<InstallLocation> InstallPaths = new List<InstallLocation>();

        public static List<ListViewItem> ManualInstalls = new List<ListViewItem>();
        public static List<ListViewItem> AutoInstalls = new List<ListViewItem>();
        public static List<ListViewItem> PostCommands = new List<ListViewItem>();
        public static List<ListViewItem> DriverInstalls = new List<ListViewItem>();

        public static List<DiskDrive> DriveList = DriveDetection.GetAvailableDisks();
    }

    #region InstallLocation Class
    class InstallLocation
    {

        public enum Type
        {
            Setup,
            Driver
        }

        public string Location;
        public Type InstallType;

        public InstallLocation(string Location, Type InstallType)
        {
            this.InstallType = InstallType;
            this.Location = Location;

        }

        public void Add()
        {
            bool contains = false;

            for (int i = 0; i < global.InstallPaths.Count; i++)
            {
                if (global.InstallPaths[i].Location.ToUpper() == Location.ToUpper())
                {
                    contains = true; break;
                }
            }

            if (!contains)
            {

                if (!Location.Contains(":"))
                {
                    Parallel.ForEach(global.DriveList, D =>
                    {
                        if (Directory.Exists(D.DriveLetter + Location))
                        {
                            new InstallLocation(D.DriveLetter + Location, InstallType).Add();
                        }
                    });
                }
                else if (Directory.Exists(Location))
                {
                    cFunctions.WriteLog("Install Location Found: " + InstallType + "\t" + Location);
                    lock (global.InstallPaths)
                    {
                        global.InstallPaths.Add(this);
                    }

                }
            }
            else if (!Directory.Exists(Location))
            {
                cFunctions.WriteLog("Install Location Missing: " + InstallType + "\t" + Location);
                lock (global.InstallPaths)
                {
                    global.InstallPaths.Add(this);
                }
            }

        }

    }
    #endregion

    #region Installersn Class
    class Installer
    {

        public enum Type
        {
            Automatic,
            Selected,
            Manual
        }

        private string _location;
        private string _syntax;

        public string AppDirectory
        {
            get { return Path.GetDirectoryName(_location); }
        }

        public Installer(string filePath, string syntax, Type installType)
        {
            InstallType = installType;
            _location = filePath;
            _syntax = syntax;
           
            if (filePath.Contains("%DVD%"))
            {
                Parallel.ForEach(global.InstallPaths.Where(t => t.InstallType == InstallLocation.Type.Setup), p =>
                {
                    string tempPath = filePath;
                    tempPath = cFunctions.ReplaceText(tempPath, "%DVD%:", p.Location);
                    tempPath = cFunctions.ReplaceText(tempPath, "%DVD%", p.Location);

                    if (File.Exists(tempPath))
                    {
                        _location = tempPath;
                    }
                    else
                    {
                        string duplicateCheck = cFunctions.RemoveDuplication(tempPath);
                        if (File.Exists(duplicateCheck))
                        {
                            _location = duplicateCheck;
                        }
                    }
                });
            }

            
        }

        public string Location
        {
            get
            {
                return _location;
            }
        }

        public Type InstallType { get; set; }

        public string Syntax
        {
            get { return _syntax; }
        }

        public bool Install()
        {
            return false;
        }

        //if (sPart2.ToUpper().StartsWith("%DVD%"))
        //                       {
        //                           Parallel.ForEach(global.InstallPaths.Where(t => t.InstallType == InstallLocation.Type.Setup), p =>
        //                           {
        //                               string testPath = cFunctions.ReplaceText(sPart2, "%DVD%:", p.Location);
        //                               testPath = cFunctions.ReplaceText(testPath, "%DVD%", p.Location);

        //                               lock (log)
        //                               {
        //                                   log += "P2: " + p.Location + " || " + testPath + Environment.NewLine;
        //                               }

        //                               if (File.Exists(testPath))
        //                               {
        //                                   sPart2 = testPath;
        //                                   foundDVD = p.Location;
        //                                   return;
        //                               }

        //                               string duplicateCheck = cFunctions.RemoveDuplication(testPath);
        //                               if (File.Exists(duplicateCheck))
        //                               {
        //                                   sPart2 = duplicateCheck;
        //                                   foundDVD = Directory.GetParent(duplicateCheck).Parent.FullName;
        //                                   log += "P2_duplicate: " + p.Location + " || " + sPart2 + Environment.NewLine;
        //                               }
        //                           });
        //                       }

        //                       if (sPart3.ContainsIgnoreCase("%DVD%") && !string.IsNullOrEmpty(foundDVD))
        //                       {
        //                           sPart3 = cFunctions.ReplaceText(sPart3, "%DVD%:", foundDVD);
        //                           sPart3 = cFunctions.ReplaceText(sPart3, "%DVD%", foundDVD);

        //                           lock (log)
        //                           {
        //                               log += "P3: " + sPart3 + Environment.NewLine;
        //                           }
        //                       }

        //                       if (sPart3.ContainsIgnoreCase("%APP%"))
        //                       {
        //                           sPart3 = cFunctions.ReplaceText(sPart3, "%APP%:", Path.GetDirectoryName(sPart2) + "\\");
        //                           sPart3 = cFunctions.ReplaceText(sPart3, "%APP%", Path.GetDirectoryName(sPart2) + "\\");

        //                           log += "P3_2: " + sPart3 + Environment.NewLine;
        //                       }
    }
    #endregion
}

