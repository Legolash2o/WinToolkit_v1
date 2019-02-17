using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WinToolkit.Classes;

namespace WinToolkit
{
    public class WIMImage
    {

        private const int WINDOWS10YEAR = 2020;
        private string _location;
        private string _mountPath;
        private string _scratchDirectory;

        private Architecture _architecture;

        private int _major; //6
        private int _minor; //.1
        private int _build; //.9600
        private int _revision; //.xxxxx
        private int _sp;


        private long _size = 0;
        private int _index = 0;

        private string _name;
        private string _description;
        private string _displayName;
        private string _displayDescription;
        private string _flag;
        private bool _supported = true;
        private bool _metro;


        private System.Drawing.Bitmap _image;

        private bool _isCalculated = false;


        //Unmounted Image
        public WIMImage(string wimFile, string wimData, int imageIndex)
        {
            _location = wimFile;
            _index = imageIndex;
            _image = Properties.Resources.Mount;
            string name = "N/A", dName = "", Description = "", dDesc = "", size = "N/A", Arc = "??", Build = "??", Flag = "??";

            foreach (string mLine in wimData.Split('<'))
            {
                string Line = mLine.ReplaceIgnoreCase( "\r", "");
                Line = Line.ReplaceIgnoreCase( "\n", "");

                if (Line.StartsWithIgnoreCase("NAME>"))
                {
                    _name = cMain.GetWIMValue(Line);
                }
                if (Line.StartsWithIgnoreCase("DISPLAYNAME>"))
                {
                    _displayName = cMain.GetWIMValue(Line);
                }
                if (Line.StartsWithIgnoreCase("FLAGS>"))
                {
                    _flag = cMain.GetWIMValue(Line);
                }
                if (Line.StartsWithIgnoreCase("DESCRIPTION>"))
                {
                    _description = cMain.GetWIMValue(Line);
                }
                if (Line.StartsWithIgnoreCase("DISPLAYDESCRIPTION>"))
                {
                    _displayDescription = cMain.GetWIMValue(Line);
                }
                if (Line.StartsWithIgnoreCase("ARCH>"))
                {
                    if (cMain.GetWIMValue(Line).EqualsIgnoreCase("0"))
                    {
                        _architecture = WinToolkit.Architecture.x86;
                    }
                    if (cMain.GetWIMValue(Line).EqualsIgnoreCase("9"))
                    {
                        _architecture = WinToolkit.Architecture.x64;
                    }
                }
                if (Line.StartsWithIgnoreCase("MAJOR>"))
                {
                    _major = int.Parse(cMain.GetWIMValue(Line));
                }
                if (Line.StartsWithIgnoreCase("MINOR>"))
                {
                    _minor = int.Parse(cMain.GetWIMValue(Line));
                }
                if (Line.StartsWithIgnoreCase("BUILD>"))
                {
                    _build = int.Parse(cMain.GetWIMValue(Line));
                }
                if (Line.StartsWithIgnoreCase("SPBUILD>"))
                {
                    _revision = int.Parse(cMain.GetWIMValue(Line));
                }
                if (Line.StartsWithIgnoreCase("SPLEVEL>"))
                {
                    _sp = int.Parse(cMain.GetWIMValue(Line));
                }
                if (Line.StartsWithIgnoreCase("TOTALBYTES>"))
                {
                    _size = Convert.ToInt64(cMain.GetWIMValue(Line));
                }
            }

            if ((_major == 6 && _minor >= 2)) { _metro = true; }



            //Used to make people use WTK 2.x but seems appropiate to remove since no work has been done on it for a while.
            //if (((_major == 6 && _minor >= 4) || _major > 6) && cMain.GetLatestDate().Year >= WINDOWS10YEAR) { _supported = false; }
        }

        public WIMImage(string registryEntry)
        {
            try
            {
                _image = Properties.Resources.folder_48;
                _mountPath = cReg.GetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\WIMMount\\Mounted Images\\" + registryEntry, "Mount Path");

                string State = cReg.GetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\WIMMount\\Mounted Images\\" + registryEntry, "Status");

                _index = Convert.ToInt16(cReg.GetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\WIMMount\\Mounted Images\\" + registryEntry, "Image Index"));
                _location = cReg.GetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\WIMMount\\Mounted Images\\" + registryEntry, "WIM Path");
                _flag = "N/A";
                _description = _location;
                _size = -1;

                if (File.Exists(_mountPath + "\\Windows\\System32\\Config\\SOFTWARE") && !cMain.IsApplicationAlreadyRunning("Dism Image Servicing Utility"))
                {
                    cReg.RegLoad("WIM_Software", _mountPath + "\\Windows\\System32\\Config\\SOFTWARE");
                    if (cReg.RegCheckMounted("WIM_Software"))
                    {
                        try
                        {
                            _name = cReg.GetValue(Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows NT\\CurrentVersion", "ProductName");
                        }
                        catch { }
                        try
                        {
                            _flag = cReg.GetValue(Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows NT\\CurrentVersion", "EditionID");
                        }
                        catch { }
                        cReg.RegUnLoad("WIM_Software");
                    }
                }

                if (File.Exists(_mountPath + "\\sources\\background.bmp"))
                    _name = "Microsoft Windows Setup (Boot.wim)";
                if (!File.Exists(_mountPath + "\\sources\\background.bmp") && Directory.Exists(_mountPath + "\\sources"))
                    _name = "Microsoft Windows PE (Boot.wim)";

                if (File.Exists(_location) && !cMain.IsFileReadLocked(_location))
                {
                    string wimInfo = cMount.CWIM_GetWimImageInfo(_location, null);
                    foreach (string mImage in Regex.Split(wimInfo, "<IMAGE INDEX=", RegexOptions.IgnoreCase))
                    {
                        if (!mImage.ContainsIgnoreCase("<WIM>") && mImage.StartsWithIgnoreCase("\"" + _index + "\""))
                        {
                            foreach (string mLine in mImage.Split('<'))
                            {
                                string Line = mLine.ReplaceIgnoreCase( "\r", "");
                                Line = Line.ReplaceIgnoreCase( "\n", "");

                                if (Line.StartsWithIgnoreCase("NAME>"))
                                {
                                    _name = cMain.GetWIMValue(Line);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                if (Directory.Exists(_mountPath + "\\Windows\\SysWOW64"))
                    _architecture = WinToolkit.Architecture.x64;
                else if (Directory.Exists(_mountPath + "\\Windows\\System32"))
                    _architecture = WinToolkit.Architecture.x86;
                else
                    _architecture = WinToolkit.Architecture.Unknown;

                try
                {
                    string bFile = _mountPath + "\\Windows\\System32\\winlogon.exe";
                    if (File.Exists(bFile))
                    {
                        FileVersionInfo F = FileVersionInfo.GetVersionInfo(bFile);
                        _major = F.FileMajorPart;
                        _minor = F.FileMinorPart;
                        _build = F.FileBuildPart;
                        _revision = F.FilePrivatePart;
                        _sp = 0;
                    }
                    else
                    {
                        _major = 0;
                        _minor = 0;
                        _build = 0;
                        _revision = 0;
                        _sp = 0;
                    }
                }
                catch (Exception Ex)
                {
                    _name = "Corrupt";
                    _major = 0;
                    _minor = 0;
                    _build = 0;
                    _revision = 0;
                    _sp = 0;
                }

                if (string.IsNullOrEmpty(_name))
                    _name = "Unknown";

                bool M = cMain.IsApplicationAlreadyRunning("Imagex");

                if (State.EqualsIgnoreCase("1") || State.EqualsIgnoreCase("3"))
                {
                    if (M == false)
                    {
                        _name = "Corrupt";
                    }
                    else
                    {
                        switch (State)
                        {
                            case "1":
                                _name = "Image is still being mounted...";
                                break;
                            case "3":
                                _name = "Image is still being unmounted...";
                                break;
                        }
                    }
                }
                if (State.EqualsIgnoreCase("2"))
                {
                    Thread t = new Thread(() =>
                    {
                        GetFiles(_mountPath, "*");
                        _isCalculated = true;

                    });
                    t.Start();
                }
            }
            catch (Exception Ex)
            {
                LargeError LE = new LargeError("Unknown Error", "Could not get previous mount data\nMount Dir: " + _mountPath, Ex);
                LE.Upload(); LE.ShowDialog();
            }

            if ((_major == 6 && _minor >= 2)) { _metro = true; }
            if (((_major == 6 && _minor >= 4) || _major > 6) && cMain.GetLatestDate().Year >= WINDOWS10YEAR) { _supported = false; }
        }

        private void GetFiles(string dirPath, string fileType)
        {
            DirectoryInfo di = new DirectoryInfo(dirPath);
            _size += di.GetFiles(fileType, SearchOption.TopDirectoryOnly).Sum(fi => fi.Length);
            foreach (string dir in Directory.GetDirectories(dirPath, "*", SearchOption.TopDirectoryOnly))
            {
                FileAttributes attributes = new DirectoryInfo(dir).Attributes;
                if ((attributes & FileAttributes.ReparsePoint) != 0) { continue; }
                if (dir.Length >= 248) { continue; }

                try
                {
                    GetFiles(dir, fileType);
                }
                catch (Exception Ex) { }
            }
        }

      

        public System.Drawing.Bitmap Image
        {
            get { return _image; }
        }

        [System.ComponentModel.BrowsableAttribute(false)]
        public string ToolTipText
        {
            get
            {
                string outText = "";
                if (string.IsNullOrEmpty(_mountPath))
                {
                    if (string.IsNullOrEmpty(_displayName))
                    {
                        outText = "Display name not set.";
                    }
                    else
                    {
                        outText += "Display Name: " + _displayName;
                    }

                    if (string.IsNullOrEmpty(_displayDescription))
                    {
                        outText += "\nDisplay description not set.";
                    }
                    else
                    {
                        outText += "\nDisplay Description: " + _displayDescription;
                    }
                    outText += "\nWIM File: " + _location;
                }
                else { outText = "Mount Path: " + _mountPath + "\nWIM File: " + _location; }

                if (!File.Exists(_location))
                {
                    outText += " [NOT AVAILABLE]";
                }
                return outText;
            }
        }

        [System.ComponentModel.BrowsableAttribute(false)]
        public string Location
        {
            get { return _location; }
        }



        [System.ComponentModel.BrowsableAttribute(false)]
        public string MountPath
        {
            get { return _mountPath; }
        }

        [System.ComponentModel.BrowsableAttribute(false)]
        public string TempDirectory
        {
            get
            {
                string tempDirectory = cOptions.WinToolkitTemp + "\\MountTemp_" + (_name + _location).CreateMD5() + "\\";
                cMain.CreateDirectory(tempDirectory);

                return tempDirectory;
            }
        }


        [System.ComponentModel.BrowsableAttribute(false)]
        public int Index
        {
            get { return _index; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                try
                {
                    string originalInput = cMount.CWIM_GetWimImageInfo(_location, null);
                    string updatedInput = cMount.RenameImage(originalInput, "NAME", _index, _name, value);

                    cMount.CWIM_SetWimInfo(_location, updatedInput);

                    _name = value;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Unable to set new name.\n\n" + Ex.Message, "Error");
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                try
                {
                    string originalInput = cMount.CWIM_GetWimImageInfo(_location, null);
                    string updatedInput = cMount.RenameImage(originalInput, "DESCRIPTION", _index, _description, value);

                    cMount.CWIM_SetWimInfo(_location, updatedInput);

                    _description = value;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Unable to set new description.\n\n" + Ex.Message, "Error");
                }
            }
        }

        [System.ComponentModel.BrowsableAttribute(false)]
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                try
                {
                    string originalInput = cMount.CWIM_GetWimImageInfo(_location, null);
                    string updatedInput = cMount.RenameImage(originalInput, "DISPLAYNAME", _index, _displayName, value);

                    cMount.CWIM_SetWimInfo(_location, updatedInput);

                    _displayName = value;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Unable to set new display name.\n\n" + Ex.Message, "Error");
                }
            }
        }

        [System.ComponentModel.BrowsableAttribute(false)]
        public string DisplayDescription
        {
            get { return _displayDescription; }
            set
            {
                try
                {
                    string originalInput = cMount.CWIM_GetWimImageInfo(_location, null);
                    string updatedInput = cMount.RenameImage(originalInput, "DISPLAYDESCRIPTION", _index, _displayDescription, value);

                    cMount.CWIM_SetWimInfo(_location, updatedInput);

                    _displayDescription = value;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Unable to set new display description.\n\n" + Ex.Message, "Error");
                }
            }
        }
        public string Edition
        {
            get { return _flag; }
            set { _flag = value; }
        }

        public Architecture Architecture
        {
            get { return _architecture; }
        }

        public string Build
        {
            get
            {
                return _major + "." + _minor + "." + _build + "." + _revision;
            }
        }

        public bool IsCalculated
        {
            get { return _isCalculated; }
        }

        public string Size
        {
            get
            {
                if (_size == -1) { return "N/A"; }
                return cMain.BytesToString(_size, true);
            }
        }

        public bool Supported
        {
            get { return _supported; }
        }

        public bool Metro
        {
            get { return _metro; }
        }

        #region MOUNT/UNMOUNT
        public bool Mount(ToolStripLabel statusLabel, Form currentForm)
        {
            if (string.IsNullOrEmpty(_mountPath))
            {
                _mountPath = cOptions.MountTemp + "_" + (_location + _name).CreateMD5();
            }
            return this.Mount(_mountPath, statusLabel, currentForm);
        }

        public bool Mount(string mountPath, ToolStripLabel statusLabel, Form currentForm)
        {
            _mountPath = cMount.CWIM_MountImage(_name, _index, _location, mountPath, statusLabel, currentForm);

            if (!string.IsNullOrEmpty(_mountPath))
            {
                return true;
            }
            return false;
        }

        public bool UnMount(ToolStripLabel statusLabel, Form currentForm)
        {
            return cMount.CWIM_UnmountImage(_name, statusLabel, false, true, true, currentForm, _location, true);
        }
        #endregion


    }


    public enum Architecture
    {
        x86,
        x64,
        Unknown
    }


}
