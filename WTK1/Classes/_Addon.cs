using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using Microsoft.Win32;
using WinToolkit.Classes.FileHandling;

namespace WinToolkit.Classes
{
    class Addon
    {
        protected List<_Tasks> _tasks = new List<_Tasks>();
        private List<string> _invalid = new List<string>();
        protected static WIMImage _image = null;
        protected static string _tempDir;
        private string _addonPath;
        private string _taskName;


        public event PropertyChangedEventHandler PropertyChanged;

        long _size;
        string _name = "";
        static AddonArchitecture _architecture = AddonArchitecture.Unknown;
        string _description = "";
        string _version = "";

        string _creator = "";
        string _website = "";

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        private bool _loaded;

        #region ACCESSORS
        public List<string> Invalid
        {
            get { return _invalid; }
        }

        protected List<_Tasks> Tasks
        {
            get { return Tasks; }
        }

        public bool Loaded
        {
            get { return _loaded; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public AddonArchitecture Architecture
        {
            get
            {
                return _architecture;
            }
        }

        public string Version
        {
            get { return _version; }
        }

        public string Creator
        {
            get { return _creator; }
        }

        public string Website
        {
            get
            {
                if (string.IsNullOrEmpty(_website))
                {
                    return "http://www.wincert.net/forum/index.php?/forum/180-win-toolkit-addons/";
                }
                return _website;
            }
        }

        public string Size
        {
            get { return cMain.BytesToString(_size); }
        }

        public string AddonPath
        {
            get { return _addonPath; }
        }
        #endregion

        public Addon(string filePath, WIMImage wimImage)
        {
            _addonPath = filePath;
            var FS = new FileInfo(filePath);
            _tempDir = cOptions.WinToolkitTemp + "\\AddonTemp_" + filePath.CreateMD5() + "\\";
            _size = FS.Length;
            _image = wimImage;

            List<string> tasks = LoadTasks(filePath);
            if (tasks == null || tasks.Count == 0)
            {
                throw new Exception("Unable to get task information.");
            }

            GetBasicInfo(ref tasks);

            if (_architecture != AddonArchitecture.Dual)
            {
                if (wimImage.Architecture == WinToolkit.Architecture.x86 && _architecture == AddonArchitecture.x64)
                {
                    throw new InvalidOperationException("A 64bit addon can't be integrated into 32bit OS.");
                }
                if (wimImage.Architecture != WinToolkit.Architecture.x86 && _architecture == AddonArchitecture.x86Only)
                {
                    throw new InvalidOperationException("This addon is for a 32bit OS only.");
                }
            }
            GetExtendedInfo(ref tasks);

            if (_invalid.Count > 0)
            {
                string invalid = "Path: " + filePath + Environment.NewLine + Environment.NewLine;
                invalid += String.Join(Environment.NewLine, _invalid.ToArray());

                cMain.ErrorBox("This addon has invalid lines and can't be integrated.\n\n" + Path.GetFileName(filePath) + "", "Error", invalid);
                throw new Exception("Invalid addon");
            }

            GetExtraFiles();
            _loaded = true;
        }

        public List<Exception> Integrate()
        {
            List<Exception> exceptions = new List<Exception>();

            int totalTasks = _tasks.Count;
            for (int i = 0; i < totalTasks; i++)
            {
                double progress = (i / (double)totalTasks) * 100;
                OnPropertyChanged(progress.ToString("F2", CultureInfo.InvariantCulture) + "% " + _tasks[i].FilePath);

                _tasks[i].Run();
                if (_tasks[i].GetError != null)
                    exceptions.Add(_tasks[i].GetError);
            }

            if (exceptions.Count == 0)
            {

                cMain.CreateDirectory(_image.MountPath + "\\Windows\\WinToolkit\\Addons");
                File.Copy(_tempDir + "\\Tasks.txt", _image.MountPath + "\\Windows\\WinToolkit\\Addons\\" + Name + ".txt",
                        true);
            }



            return exceptions;
        }

        public void Extract()
        {
            cMain.ExtractFiles(_addonPath, _tempDir);
        }

        private List<string> LoadTasks(string filePath)
        {
            cMain.ExtractFiles(filePath, _tempDir, null, "Tasks.txt");
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            using (var streamReader = new StreamReader(_tempDir + "Tasks.txt", true))
            {
                string content = streamReader.ReadToEnd();
                return content.Split(Environment.NewLine.ToCharArray()).Where(x => !string.IsNullOrEmpty(x)).ToList();
            }
        }

        private void GetBasicInfo(ref List<string> taskFile)
        {
            for (int i = 0; i < taskFile.Count; i++)
            {
                string strLine = taskFile[i];
                if (strLine.StartsWithIgnoreCase("NAME="))
                {
                    _name = GetValue(strLine);
                    goto FOUND;
                }

                if (strLine.StartsWithIgnoreCase("CREATOR="))
                {
                    _creator = GetValue(strLine);
                    goto FOUND;
                }
                if (strLine.StartsWithIgnoreCase("VERSION="))
                {
                    _version = GetValue(strLine);
                    goto FOUND;
                }
                if (strLine.StartsWithIgnoreCase("ARC="))
                {
                    var val = GetValue(strLine).ToUpperInvariant();
                    switch (val)
                    {
                        case "X64":
                        case "X64!":
                            _architecture = AddonArchitecture.x64;
                            break;
                        case "X86":
                            _architecture = AddonArchitecture.x86;
                            break;
                        case "X86!":
                            _architecture = AddonArchitecture.x86Only;
                            break;
                        case "DUAL":
                            _architecture = AddonArchitecture.Dual;
                            break;
                    }
                    goto FOUND;
                }
                if (strLine.StartsWithIgnoreCase("DESCRIPTION="))
                {
                    _description = GetValue(strLine);
                    goto FOUND;
                }
                if (strLine.StartsWithIgnoreCase("WEBSITE="))
                {
                    _website = GetValue(strLine);
                    goto FOUND;
                }

                if (strLine.StartsWithIgnoreCase("["))
                    return;

                FOUND:
                taskFile.RemoveAt(i--);
            }


        }

        public void Clean()
        {
            Files.DeleteFolder(_tempDir, false);
        }

        private void GetExtendedInfo(ref List<string> taskFile)
        {
            TaskName currentTask = TaskName.None;
            for (int i = 0; i < taskFile.Count; i++)
            {

                if (taskFile[i].StartsWithIgnoreCase("["))
                {
                    currentTask = GetTask(taskFile[i]);
                    goto FOUND;
                }

                string oldPath = taskFile[i];
                string newPath = "";

                if (taskFile[i].ToUpperInvariant().ContainsIgnoreCase("::"))
                {
                    oldPath = taskFile[i].Split(':')[0];
                    newPath = taskFile[i].Split(':')[2];
                }

                if (taskFile[i].ToLowerInvariant().ContainsIgnoreCase("|"))
                {
                    string current = taskFile[i];
                    oldPath = current.Substring(0, current.IndexOf('|'));
                    newPath = current.Substring(oldPath.Length + 1);
                }

                _Tasks newTask = null;
                switch (currentTask)
                {
                    case TaskName.CopyFile:
                        newTask = new CopyFile(oldPath, newPath);
                        break;
                    case TaskName.CopyFolder:
                        newTask = new CopyDirectory(oldPath, newPath);
                        break;
                    case TaskName.CopyFileDisk:
                        newTask = new CopyFileDisk(oldPath, newPath);
                        break;
                    case TaskName.CopyFolderDisk:
                        newTask = new CopyDirectoryDisk(oldPath, newPath);
                        break;
                    case TaskName.Reg:
                        newTask = new RegistryFile(oldPath);
                        break;
                    case TaskName.Delete:
                        newTask = new DeleteFile(oldPath);
                        break;
                    case TaskName.CMD:
                        newTask = new Command(oldPath, newPath);
                        break;
                }
                if (newTask != null)
                {
                    _tasks.Add(newTask);
                }
                else
                {
                    _invalid.Add(taskFile[i].ToUpperInvariant() + " | " + taskFile[i]);
                }


                FOUND:
                taskFile.RemoveAt(i--);
            }
        }

        private void GetExtraFiles()
        {
            cMain.ExtractFiles(_addonPath, _tempDir, null, "*.reg", false, false);
            foreach (var f in Directory.GetFileSystemEntries(_tempDir, "*.reg"))
            {
                _tasks.Add(new RegistryFile(f));
            }

            cMain.ExtractFiles(_addonPath, _tempDir, null, "*.lnk", false, false);
            foreach (var f in Directory.GetFileSystemEntries(_tempDir, "*.lnk"))
            {
                _tasks.Add(new Shortcut(f));
            }
        }
        private string GetValue(string line)
        {
            return line.Substring(line.IndexOf("=", Extensions.DefaultComparison) + 1);
        }

        private TaskName GetTask(string line)
        {
            switch (line.ToUpperInvariant())
            {
                case "[COPYFILE]":
                    return TaskName.CopyFile;
                case "[COPYFOLDER]":
                    return TaskName.CopyFolder;
                case "[COPYFILEDISK]":
                    return TaskName.CopyFileDisk;
                case "[COPYFOLDERDISK]":
                    return TaskName.CopyFolderDisk;
                case "[DELETE]":
                    return TaskName.Delete;
                case "[RUNCMD]":
                    return TaskName.CMD;
            }
            _invalid.Add(line.ToUpperInvariant() + " | " + line);
            return TaskName.None;
        }

        #region CLASSES & ENUMS

        public enum AddonArchitecture
        {
            x86,
            x86Only,
            x64,
            Dual,
            Unknown
        }

        public enum TaskName
        {
            None,
            CopyFile,
            CopyFolder,
            CopyFileDisk,
            CopyFolderDisk,
            Delete,
            Reg,
            CMD,
            Shortcut
        }

        public abstract class _Tasks
        {
            protected Exception _exception = null;
            protected string _filePath;
            protected string _copyTo;

            protected Version _requirement;

            public abstract bool Run();

            public Exception GetError
            {
                get { return _exception; }
            }

            public string FilePath
            {
                get { return _filePath; }
            }

            protected void CheckRequirement(Version version)
            {
                if (Environment.Version < version)
                {
                    throw new InvalidOperationException("This addon was designed for a newer version of WinToolkit.");
                }
            }

            protected virtual string Convert(string input)
            {
                if (_architecture == AddonArchitecture.Dual)
                    return input;

                if (_architecture == AddonArchitecture.x86 && _image.Architecture == WinToolkit.Architecture.x86)
                {
                    if (input.StartsWithIgnoreCase("PROGRAM FILES (X86)"))
                    {
                        return "Program Files" + input.Substring(19);
                    }
                    if (input.StartsWithIgnoreCase("WINDOWS\\SYSWOW64"))
                    {
                        return "Windows\\System32" + input.Substring(16);
                    }

                    if (_architecture == AddonArchitecture.x86 && _image.Architecture == WinToolkit.Architecture.x64)
                    {
                        if (input.StartsWithIgnoreCase("PROGRAM FILES") && !input.StartsWithIgnoreCase("PROGRAM FILES (X86)"))
                        {
                            return "Program Files (x86)" + input.Substring(13);
                        }
                        if (input.StartsWithIgnoreCase("WINDOWS\\SYSTEM32"))
                        {
                            return "Windows\\SysWOW64" + input.Substring(16);
                        }
                    }
                }
                return input;
            }



        }

        //If a new class is created. CheckRequirement(new Version(1, 0)); has to be ran.


        private class CopyFile : _Tasks
        {

            public CopyFile(string filePath, string copyTo)
            {

                _filePath = filePath;
                _copyTo = copyTo;
            }
            public override bool Run()
            {
                string path = _tempDir + _filePath;
                string copyTo = _image.MountPath + "\\" + Convert(_copyTo);
                try
                {
                    if (File.Exists(path))
                    {

                        cMain.CreateDirectory(copyTo);
                        if (Directory.Exists(copyTo))
                        {
                            copyTo += "\\" + Path.GetFileName(path);
                            File.Copy(path, copyTo, true);
                            return true;
                        }
                    }
                    throw new FileNotFoundException("File not found");
                }
                catch (Exception exception)
                {
                    _exception.Data.Add("CopyFrom", path);
                    _exception.Data.Add("CopyTo", copyTo);
                    _exception = exception;
                }

                return false;
            }

        }

        private class CopyDirectory : _Tasks
        {
            public CopyDirectory(string folderPath, string copyTo)
            {
                _filePath = folderPath;
                _copyTo = copyTo;
            }
            public override bool Run()
            {
                string path = _tempDir + _filePath;

                try
                {
                    if (Directory.Exists(path))
                    {
                        string copyTo = string.Format("{0}\\{1}\\{2}", _image.MountPath, Convert(_copyTo), _filePath);
                        cMain.CopyDirectory(path, copyTo, true, false);
                        return true;
                    }
                    throw new FileNotFoundException("Directory not found");
                }
                catch (Exception exception)
                {
                    _exception = exception;
                }
                return false;
            }
        }

        private class CopyFileDisk : _Tasks
        {
            public CopyFileDisk(string filePath, string copyTo)
            {
                _filePath = filePath;
                _copyTo = copyTo;
            }
            public override bool Run()
            {
                try
                {
                    string source = _tempDir + _filePath;
                    if (File.Exists(source))
                    {
                        string copyTo = cMount.SourceToFolder(_image.Location) + _copyTo;
                        cMain.CreateDirectory(cMount.SourceToFolder(_image.Location) + _copyTo);
                        if (Directory.Exists(copyTo))
                        {
                            copyTo += "\\" + Path.GetFileName(source);
                            File.Copy(source, copyTo, true);
                            return true;
                        }
                    }

                    throw new FileNotFoundException("File not found");
                }
                catch (Exception exception)
                {
                    _exception = exception;
                }
                return false;
            }
        }

        private class CopyDirectoryDisk : _Tasks
        {
            public CopyDirectoryDisk(string folderPath, string copyTo)
            {
                _filePath = folderPath;
                _copyTo = copyTo;
            }
            public override bool Run()
            {
                try
                {
                    if (Directory.Exists(_tempDir + _filePath))
                    {
                        string copyTo = string.Format("{0}\\{1}\\{2}", cMount.SourceToFolder(_image.Location),
                            _copyTo, _filePath);

                        cMain.CopyDirectory(_tempDir + FilePath, _copyTo, true, false);
                        return true;
                    }
                    throw new FileNotFoundException("Directory not found");
                }
                catch (Exception exception)
                {
                    _exception = exception;
                }
                return false;
            }
        }

        private class RegistryFile : _Tasks
        {

            public RegistryFile(string registryFile)
            {
                _filePath = registryFile;
            }

            public override bool Run()
            {
                try
                {
                    if (!Convert())
                    {
                        throw new InvalidOperationException("Convertion failed.");
                    }
                    cMain.OpenProgram("\"" + cMain.SysRoot + "\\regedit.exe\"", "/s \"" + _filePath + "\"", true, ProcessWindowStyle.Hidden);
                    return true;
                }
                catch (Exception exception)
                {
                    _exception = exception;
                }
                return false;
            }

            private bool Convert()
            {

                Encoding encoding = Encoding.GetEncoding(1252);
                string registry = File.ReadAllText(_filePath, encoding);
                if (string.IsNullOrEmpty(registry))
                {
                    throw new InvalidOperationException("No registry");
                }

                string convertedRegistry = null;
                foreach (string line in registry.Split(Environment.NewLine.ToCharArray()))
                {
                    if (string.IsNullOrEmpty(line))
                        continue;

                    string newLine = line.ReplaceIgnoreCase("REGEDIT4", "Windows Registry Editor Version 5.00");

                    if (_architecture != AddonArchitecture.Dual)
                    {
                        if (_architecture == AddonArchitecture.x86 && _image.Architecture == WinToolkit.Architecture.x86)
                        {
                            newLine = newLine.ReplaceIgnoreCase("\\SOFTWARE\\WOW6432NODE\\", "\\SOFTWARE\\");
                            newLine = newLine.ReplaceIgnoreCase("\\PROGRAM FILES (X86)\\", "\\PROGRAM FILES\\");
                            newLine = newLine.ReplaceIgnoreCase("\\SYSWOW64\\", "\\SYSTEM32\\");
                        }

                        if (_architecture == AddonArchitecture.x86 && _image.Architecture == WinToolkit.Architecture.x64)
                        {
                            newLine = newLine.ReplaceIgnoreCase("\\SOFTWARE\\", "\\SOFTWARE\\WOW6432NODE\\");
                            newLine = newLine.ReplaceIgnoreCase("\\PROGRAM FILES\\", "\\PROGRAM FILES (x86)\\");
                            newLine = newLine.ReplaceIgnoreCase("\\SYSTEM32\\", "\\SYSWOW64\\");
                        }
                    }
                    newLine = newLine.ReplaceIgnoreCase("HKEY_CLASSES_ROOT\\", "HKEY_LOCAL_MACHINE\\WIM_Software\\Classes\\");

                    if (newLine.StartsWithIgnoreCase("[HKEY_USERS\\S-") &&
                        newLine.ContainsIgnoreCase("-1000_CLASSES\\"))
                    {
                        while (!newLine.StartsWithIgnoreCase("_Classes"))
                        {
                            newLine = newLine.Substring(1);
                        }
                        newLine = "[HKEY_LOCAL_MACHINE\\WIM_Software\\" + newLine.Substring(1);
                    }

                    if (newLine.StartsWithIgnoreCase("[HKEY_USERS\\S-") && newLine.ContainsIgnoreCase("\\SOFTWARE\\"))
                    {
                        while (!newLine.StartsWithIgnoreCase("\\SOFTWARE\\"))
                        {
                            newLine = newLine.Substring(1);
                        }
                        newLine = "[HKEY_LOCAL_MACHINE\\WIM_Default" + newLine.Substring(9);
                    }

                    if (newLine.StartsWithIgnoreCase("[HKEY_USERS\\S-"))
                    {
                        while (!newLine.StartsWithIgnoreCase("S-"))
                        {
                            newLine = newLine.Substring(1);
                        }
                        while (!newLine.StartsWithIgnoreCase("\\"))
                        {
                            newLine = newLine.Substring(1);
                        }
                        newLine = "[HKEY_LOCAL_MACHINE\\WIM_Default\\" + newLine.Substring(9);
                    }

                    if (newLine.StartsWithIgnoreCase("[-HKEY_USERS\\S-") &&
                        newLine.ContainsIgnoreCase("-1000_CLASSES\\"))
                    {
                        while (newLine.StartsWithIgnoreCase("_Classes"))
                        {
                            newLine = newLine.Substring(1);
                        }
                        newLine = "[-HKEY_LOCAL_MACHINE\\WIM_Software\\" + newLine.Substring(1);
                    }

                    if (newLine.StartsWithIgnoreCase("[-HKEY_USERS\\S-") && newLine.ContainsIgnoreCase("\\SOFTWARE\\"))
                    {
                        while (!newLine.StartsWithIgnoreCase("\\SOFTWARE\\"))
                        {
                            newLine = newLine.Substring(1);
                        }
                        newLine = "[-HKEY_LOCAL_MACHINE\\WIM_Software" + newLine.Substring(9);
                    }

                    newLine = newLine.ReplaceIgnoreCase("HKEY_USERS\\.DEFAULT\\",
                        "HKEY_LOCAL_MACHINE\\WIM_Default\\");
                    newLine = newLine.ReplaceIgnoreCase("HKEY_CURRENT_USER\\",
                        "HKEY_LOCAL_MACHINE\\WIM_Default\\");

                    newLine = newLine.ReplaceIgnoreCase("CurrentControlSet", "ControlSet001");

                    if (!newLine.StartsWithIgnoreCase("[HKEY_LOCAL_MACHINE\\WIM_") &&
                        newLine.StartsWithIgnoreCase("[HKEY_LOCAL_MACHINE\\"))
                    {
                        newLine = newLine.ReplaceIgnoreCase("HKEY_LOCAL_MACHINE\\", "HKEY_LOCAL_MACHINE\\WIM_");
                    }

                    newLine = newLine.ReplaceIgnoreCase("WIM_\\WIM_", "WIM_");

                    while (newLine.StartsWithIgnoreCase("[") && newLine.ContainsIgnoreCase("\\\\"))
                    {
                        newLine = newLine.ReplaceIgnoreCase("\\\\", "\\");
                    }

                    if (newLine.StartsWithIgnoreCase("[HKEY_LOCAL_MACHINE\\WIM_Software\\Classes\\"))
                    {
                        string sCheckAccess = newLine.Substring(20);
                        sCheckAccess = sCheckAccess.Substring(0, sCheckAccess.Length - 1);
                        bool bHaveWrite = cAddon.HaveWritePermission(Registry.LocalMachine, sCheckAccess);
                        if (!bHaveWrite)
                        {
                            cReg.RegSetOwnership(Registry.LocalMachine, sCheckAccess);
                        }
                    }

                    convertedRegistry += newLine + Environment.NewLine;
                }

                using (var streamWriter = new StreamWriter(_filePath,false))
                {
                    streamWriter.Write(convertedRegistry);
                }


                return true;

            }
        }

        private class DeleteFile : _Tasks
        {

            public DeleteFile(string path)
            {
                _filePath = path;
            }
            public override bool Run()
            {
                try
                {
                    if (File.Exists(_image.MountPath + "\\" + _filePath))
                    {
                        Files.DeleteFile(_image.MountPath + "\\" + _filePath);
                        return true;
                    }
                    if (Directory.Exists(_image.MountPath + "\\" + _filePath))
                    {
                        Files.DeleteFolder(_image.MountPath + "\\" + _filePath, false);
                        return true;
                    }
                }
                catch (Exception exception)
                {
                    _exception = exception;
                }
                return false;
            }
        }

        private class Shortcut : _Tasks
        {
            public Shortcut(string filePath)
            {
                _filePath = filePath;
            }

            public override bool Run()
            {
                try
                {
                    var shell2 = new IWshRuntimeLibrary.WshShell();
                    var link = (IWshRuntimeLibrary.IWshShortcut)shell2.CreateShortcut(_filePath);

                    string targetPath = link.TargetPath;

                    string newTargetPath = Convert(targetPath);

                    if (!String.Equals(targetPath, newTargetPath))
                    {
                        link.TargetPath = newTargetPath;
                        link.WorkingDirectory = Path.GetDirectoryName(newTargetPath);
                        link.Save();
                    }
                    return true;
                }
                catch (Exception exception)
                {
                    _exception = exception;
                }
                return false;
            }

            protected override string Convert(string input)
            {
                if (_architecture == AddonArchitecture.x86 && _image.Architecture == WinToolkit.Architecture.x86)
                {
                    if (input.StartsWithIgnoreCase("C:\\PROGRAM FILES (X86)\\"))
                    {
                        return "%SystemDrive%\\Program Files\\" + input.Substring(23);
                    }
                }
                if (_architecture == AddonArchitecture.x86 && _image.Architecture == WinToolkit.Architecture.x64)
                {

                    if (input.StartsWithIgnoreCase("C:\\PROGRAM FILES\\"))
                    {
                        return "%SystemDrive%\\Program Files (x86)\\" + input.Substring(17);
                    }
                }
                if (input.StartsWithIgnoreCase("C:\\"))
                {
                    return "%SystemDrive%\\" + input.Substring(3);
                }

                return input;
            }


        }

        private class Command : _Tasks
        {
            private string _argument;

            public Command(string file, string argument)
            {
                _filePath = file;
                _argument = argument;
            }

            public override bool Run()
            {
                bool allowCommands = cOptions.AICommands;

                try
                {
                    if (_filePath.ContainsIgnoreCase("DISM.exe")) { allowCommands = false; } //temporarily disabled
                    if (_filePath.ContainsIgnoreCase("PKGMGR.EXE")) { allowCommands = false; } //temporarily disabled

                    if (!allowCommands)
                        return false;

                    if (_argument.ContainsIgnoreCase("\"%WIM%\""))
                        _argument = _argument.ReplaceIgnoreCase("\"%WIM%\"", "\"" + _image.MountPath + "\"");
                    if (_argument.ContainsIgnoreCase("%WIM%"))
                        _argument = _argument.ReplaceIgnoreCase("%WIM%", "\"" + _image.MountPath + "\"");

                    cMain.OpenProgram("\"" + _filePath + "\"", _argument, true, ProcessWindowStyle.Hidden);
                    return true;
                }
                catch (Exception exception)
                {
                    _exception = exception;
                }
                return false;
            }
        }


        #endregion
    }
}
