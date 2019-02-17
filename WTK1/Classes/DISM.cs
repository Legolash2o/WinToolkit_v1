using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace WinToolkit
{
    public static class DISM
    {
        public static readonly Version Win8_0DISM = new Version("6.2");
        public static readonly Version Win8_1DISM = new Version("6.3");
        static List<DismFile> available = new List<DismFile>();


        /// <summary>
        /// Searches the computer for all available versions of DISM.
        /// </summary>
        /// <returns>The amount found.</returns>
        public static int Load()
        {
            new DismFile(cMain.SysFolder + "\\Dism.exe", DismType.System);

            if (!string.IsNullOrEmpty(cOptions.DISMLoc))
            {
                foreach (string dism in cOptions.DISMLoc.Split('|'))
                {
                    new DismFile(dism, DismType.Custom);
                }
            }

            if (cMain.Arc64)
            {
                new DismFile(cMain.SysProgFiles + "\\Windows Kits\\8.1\\Assessment and Deployment Kit\\Deployment Tools\\amd64\\DISM\\dism.exe");
                new DismFile(cMain.SysProgFiles + "\\Windows Kits\\8.0\\Assessment and Deployment Kit\\Deployment Tools\\amd64\\DISM\\dism.exe");
                new DismFile(cMain.SysProgFiles + " (x86)\\Windows Kits\\8.1\\Assessment and Deployment Kit\\Deployment Tools\\amd64\\DISM\\dism.exe");
                new DismFile(cMain.SysProgFiles + " (x86)\\Windows Kits\\8.0\\Assessment and Deployment Kit\\Deployment Tools\\amd64\\DISM\\dism.exe");
                new DismFile(cMain.SysProgFiles + "\\Windows AIK\\Tools\\amd64\\Servicing\\Dism.exe");
            }
            else
            {
                new DismFile(cMain.SysProgFiles + "\\Windows Kits\\8.1\\Assessment and Deployment Kit\\Deployment Tools\\x86\\DISM\\dism.exe");
                new DismFile(cMain.SysProgFiles + "\\Windows Kits\\8.0\\Assessment and Deployment Kit\\Deployment Tools\\x86\\DISM\\dism.exe");
                new DismFile(cMain.SysProgFiles + "\\Windows AIK\\Tools\\Servicing\\Dism.exe");
                new DismFile(cMain.SysProgFiles + "\\Windows AIK\\Tools\\x86\\Servicing\\Dism.exe");
                new DismFile(cMain.SysFolder + "\\Win8Dism\\Dism.exe");
            }

            Sort();

            return available.Count;

        }

        private static void Sort()
        {
            if (available.Count <= 1) return;
            available.Sort((v1, v2) => v1.Version.CompareTo(v2.Version));
            available.Reverse();
        }

        public static void Add(string dismPath)
        {
            new DismFile(dismPath, DismType.Custom);
            Sort();
        }

        public static void Delete(string dismPath)
        {
            var f = available.First(d => String.Equals(d.Location, dismPath, StringComparison.CurrentCultureIgnoreCase));
            if (f == null)
                return;
            available.Remove(f);

        }

        public static List<DismFile> All
        {
            get
            {
                return available;
            }

        }


        /// <summary>
        /// Return system, if that is not installed. The
        /// latest is returned.
        /// </summary>
        public static DismFile Latest
        {
            get
            {
                return available.OrderByDescending(v => v.Version).First();
            }
        }

        /// <summary>
        /// Return system, if that is not installed. The
        /// latest is returned.
        /// </summary>
        public static DismFile System
        {
            get
            {
                var system = available.FirstOrDefault(d => d.Type == DismType.System);
                if (system != null) { return system; }
                return Latest;
            }
        }



        public static int Count
        {
            get { return available.Count; }
        }


        public enum DismType
        {
            Standard,
            System,
            Custom
        }

        public class DismFile
        {
            public DismFile(string location, DismType type = DismType.Standard)
            {
                if (string.IsNullOrEmpty(location)) return;
                if (!File.Exists(location)) return;

                try
                {
                    Location = location;
                    Version = new Version(FileVersionInfo.GetVersionInfo(location).ProductVersion);
                    Type = type;
                }
                catch (Exception ex)
                {
                    string extended = string.Format("Filepath: {0}\nVersion: {1}", location,
                        FileVersionInfo.GetVersionInfo(location).ProductVersion);
                    var LE = new LargeError("New DISM", "Error tring to add DISM.", extended, ex);
                    LE.Upload();
                    LE.ShowDialog();
                }

                if (type == DismType.System)
                {
                    available.Insert(0, this);
                }
                else
                {
                    available.Add(this);
                }
            }

            public string Location { get; private set; }

            public Version Version { get; private set; }

            public DismType Type { get; private set; }

        }
    }
}
