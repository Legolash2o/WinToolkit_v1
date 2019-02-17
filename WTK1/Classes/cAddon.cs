using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using File = System.IO.File;

namespace WinToolkit
{


    class cAddon
    {

        public static bool AddonCancel;
        public static string CExErr = "";
        public static string AddonError = "";
        public static string GetValue(string line)
        {
            string nLine = line;
            while (nLine.ContainsIgnoreCase("="))
            {
                nLine = nLine.Substring(1);
            }

            return nLine;
        }

        public static string ReplaceString(string originalString, string oldValue, string newValue)
        {
            string nValue = originalString;
            if (originalString.ContainsIgnoreCase(oldValue.ToUpper()))
            {
                nValue = originalString.ReplaceIgnoreCase(oldValue, newValue);
            }
            while (nValue.ContainsIgnoreCase("\\\\") && nValue.StartsWithIgnoreCase("[")) { nValue = nValue.ReplaceIgnoreCase("\\\\", "\\"); }

            return nValue;
        }

        public static ListViewItem Old_ExtractInfo(string filename, Architecture architecture)
        {
            if (!Directory.Exists(cOptions.WinToolkitTemp + "\\AddonT")) { cMain.CreateDirectory(cOptions.WinToolkitTemp + "\\AddonT"); }

            cMain.eErr = "";
            Files.DeleteFile(cOptions.WinToolkitTemp + "\\AddonT\\Tasks.txt");
            cMain.ExtractFiles(filename, cOptions.WinToolkitTemp + "\\AddonT", null, "Tasks.txt");
            if (!File.Exists(cOptions.WinToolkitTemp + "\\AddonT\\Tasks.txt"))
            {
                string TA = cMain.RandomName(1, 999999);
                while (File.Exists(cOptions.WinToolkitTemp + "\\Temp_" + TA + ".WA"))
                {
                    TA = cMain.RandomName(1, 999999);
                }

                File.Copy(filename, cOptions.WinToolkitTemp + "\\Temp_" + TA + ".WA");
                cMain.ExtractFiles(cOptions.WinToolkitTemp + "\\Temp_" + TA + ".WA", cOptions.WinToolkitTemp + "\\AddonT", null, "Tasks.txt");
                Files.DeleteFile(cOptions.WinToolkitTemp + "\\Temp_" + TA + ".WA");
            }

            if (File.Exists(cOptions.WinToolkitTemp + "\\AddonT\\Tasks.txt"))
            {
                var GI = new StreamReader(cOptions.WinToolkitTemp + "\\AddonT\\Tasks.txt", true);
                string aFile = GI.ReadToEnd();
                GI.Close();

                Files.DeleteFile(cOptions.WinToolkitTemp + "\\AddonT\\Tasks.txt");
                var AA = new ListViewItem();
                var FS = new FileInfo(filename);
                string fSize = cMain.BytesToString(FS.Length);
                string aName = "";
                string aCreator = "";
                string aVersion = "";
                string aArc = "";
                string aDesc = "";
                string aWebsite = "";

                foreach (string sLine in aFile.Split(Environment.NewLine.ToCharArray()))
                {
                    string strLine = sLine;
                    if (strLine.StartsWithIgnoreCase("NAME="))
                        aName = GetValue(strLine);
                    if (strLine.StartsWithIgnoreCase("CREATOR="))
                        aCreator = GetValue(strLine);
                    if (strLine.StartsWithIgnoreCase("VERSION="))
                        aVersion = GetValue(strLine);
                    if (strLine.StartsWithIgnoreCase("ARC="))
                        aArc = GetValue(strLine);
                    if (strLine.StartsWithIgnoreCase("DESCRIPTION="))
                        aDesc = GetValue(strLine);
                    if (strLine.StartsWithIgnoreCase("WEBSITE="))
                        aWebsite = GetValue(strLine);
                }

                AA.Text = aName;
                AA.SubItems.Add(aCreator);
                AA.SubItems.Add(aVersion);
                AA.SubItems.Add(cMain.BytesToString(FS.Length));
                AA.SubItems.Add(aArc);
                AA.SubItems.Add(filename);
                AA.SubItems.Add(aDesc);
                AA.SubItems.Add(aWebsite);

                if (cMain.selectedImages[0].Architecture.ToString() != "Multi" || aArc != "DUAL")
                {
                    if (cMain.selectedImages[0].Architecture.ToString().EqualsIgnoreCase("x86") && aArc.EqualsIgnoreCase("x64"))
                    {
                        AddonError += "'" + aName + "': You cannot integrate an x64 addon into an 32bit operating system!\n[" + filename + "]" + Environment.NewLine + Environment.NewLine;
                        return null;
                    }
                    if (cMain.selectedImages[0].Architecture.ToString().EqualsIgnoreCase("x86") && aArc.EqualsIgnoreCase("x86!"))
                    {
                        AddonError += "'" + aName + "': The creator has made this addon 32bit only!\n[" + filename + "]" + Environment.NewLine + Environment.NewLine;
                        return null;
                    }
                }
                return AA;
            }
            string E = "";
            if (File.Exists(filename)) { E += "File = True"; } else { E += "File = False"; } E += Environment.NewLine;
            if (File.Exists(cOptions.WinToolkitTemp + "\\AddonT\\Temp.WA")) { E += "CopyTo = True"; } else { E += "CopyTo = False"; } E += Environment.NewLine;
            if (Directory.Exists(cOptions.WinToolkitTemp + "\\AddonT\\")) { E += "ExtractTo = True"; } else { E += "ExtractTo = False"; } E += Environment.NewLine;
            if (!string.IsNullOrEmpty(cMain.eErr))
            {
                LargeError LE = new LargeError("Addon Information", "Unable to get addon information.", filename + "\n" + E);
                LE.Upload(); LE.ShowDialog();
            }
            AddonError += "This does not seem to be a valid addon or it may have been corrupted.\n[" + filename + "]" + Environment.NewLine + Environment.NewLine;

            return null;
        }

        public static void ConvertRegAddon(string file, string arc, bool testRegCon, WIMImage image)
        {
            string RString = null;
            var SA = new ArrayList();
            if (Directory.Exists(file))
            {
                var d = new DirectoryInfo(file);
                foreach (FileInfo f in d.GetFiles("*.reg"))
                {
                    SA.Add(f.FullName);
                }
            }

            if (File.Exists(file))
            {
                SA.Add(file);
            }

            string fs = cOptions.WinToolkitTemp + "\\Conv.reg";
            foreach (string f in SA)
            {
                Encoding encoding = Encoding.GetEncoding(1252);
                string text = File.ReadAllText(f, encoding);

                foreach (string R in text.Split(Environment.NewLine.ToCharArray()))
                {
                    string RLine = R;

                    if (!string.IsNullOrEmpty(RLine))
                    {
                        RLine = RLine.ReplaceIgnoreCase("REGEDIT4", "Windows Registry Editor Version 5.00");

                        if (arc.ToUpper() != "DUAL")
                        {
                            if (arc.ToUpper().EqualsIgnoreCase("X86") && !Directory.Exists(image.MountPath + "\\Windows\\SysWOW64"))
                            {
                                RLine = RLine.ReplaceIgnoreCase("\\SOFTWARE\\WOW6432NODE\\", "\\SOFTWARE\\");
                                RLine = RLine.ReplaceIgnoreCase("\\PROGRAM FILES (X86)\\", "\\PROGRAM FILES\\");
                                RLine = RLine.ReplaceIgnoreCase("\\SYSWOW64\\", "\\SYSTEM32\\");
                            }

                            if (arc.ToUpper().EqualsIgnoreCase("X86") && Directory.Exists(image.MountPath + "\\Windows\\SysWOW64"))
                            {
                                RLine = RLine.ReplaceIgnoreCase("\\SOFTWARE\\", "\\SOFTWARE\\WOW6432NODE\\");
                                RLine = RLine.ReplaceIgnoreCase("\\PROGRAM FILES\\", "\\PROGRAM FILES (x86)\\");
                                RLine = RLine.ReplaceIgnoreCase("\\SYSTEM32\\", "\\SYSWOW64\\");
                            }
                        }
                        RLine = RLine.ReplaceIgnoreCase("HKEY_CLASSES_ROOT\\", "HKEY_LOCAL_MACHINE\\WIM_Software\\Classes\\");

                        if (RLine.StartsWithIgnoreCase("[HKEY_USERS\\S-") && RLine.ContainsIgnoreCase("-1000_CLASSES\\"))
                        {
                            while (!RLine.StartsWithIgnoreCase("_Classes"))
                            {
                                RLine = RLine.Substring(1);
                            }
                            RLine = "[HKEY_LOCAL_MACHINE\\WIM_Software\\" + RLine.Substring(1);
                        }

                        if (RLine.StartsWithIgnoreCase("[HKEY_USERS\\S-") && RLine.ContainsIgnoreCase("\\SOFTWARE\\"))
                        {
                            while (!RLine.StartsWithIgnoreCase("\\SOFTWARE\\"))
                            {
                                RLine = RLine.Substring(1);
                            }
                            RLine = "[HKEY_LOCAL_MACHINE\\WIM_Default" + RLine.Substring(9);
                        }

                        if (RLine.StartsWithIgnoreCase("[HKEY_USERS\\S-"))
                        {
                            while (!RLine.StartsWithIgnoreCase("S-"))
                            {
                                RLine = RLine.Substring(1);
                            }
                            while (!RLine.StartsWithIgnoreCase("\\"))
                            {
                                RLine = RLine.Substring(1);
                            }
                            RLine = "[HKEY_LOCAL_MACHINE\\WIM_Default\\" + RLine.Substring(9);
                        }

                        if (RLine.StartsWithIgnoreCase("[-HKEY_USERS\\S-") && RLine.ContainsIgnoreCase("-1000_CLASSES\\"))
                        {
                            while (RLine.StartsWithIgnoreCase("_Classes"))
                            {
                                RLine = RLine.Substring(1);
                            }
                            RLine = "[-HKEY_LOCAL_MACHINE\\WIM_Software\\" + RLine.Substring(1);
                        }

                        if (RLine.StartsWithIgnoreCase("[-HKEY_USERS\\S-") && RLine.ContainsIgnoreCase("\\SOFTWARE\\"))
                        {
                            while (!RLine.StartsWithIgnoreCase("\\SOFTWARE\\"))
                            {
                                RLine = RLine.Substring(1);
                            }
                            RLine = "[-HKEY_LOCAL_MACHINE\\WIM_Software" + RLine.Substring(9);
                        }

                        RLine = RLine.ReplaceIgnoreCase("HKEY_USERS\\.DEFAULT\\", "HKEY_LOCAL_MACHINE\\WIM_Default\\");
                        RLine = RLine.ReplaceIgnoreCase("HKEY_CURRENT_USER\\", "HKEY_LOCAL_MACHINE\\WIM_Default\\");

                        RLine = RLine.ReplaceIgnoreCase("CurrentControlSet", "ControlSet001");

                        if (!RLine.StartsWithIgnoreCase("[HKEY_LOCAL_MACHINE\\WIM_") && RLine.StartsWithIgnoreCase("[HKEY_LOCAL_MACHINE\\"))
                        {
                            RLine = RLine.ReplaceIgnoreCase("HKEY_LOCAL_MACHINE\\", "HKEY_LOCAL_MACHINE\\WIM_");
                        }

                        RLine = RLine.ReplaceIgnoreCase("WIM_\\WIM_", "WIM_");

                        while (RLine.StartsWithIgnoreCase("[") && RLine.ContainsIgnoreCase("\\\\")) { RLine = RLine.ReplaceIgnoreCase("\\\\", "\\"); }

                        if (!testRegCon && RLine.StartsWithIgnoreCase("[HKEY_LOCAL_MACHINE\\WIM_Software\\Classes\\"))
                        {
                            string sCheckAccess = RLine.Substring(20);
                            sCheckAccess = sCheckAccess.Substring(0, sCheckAccess.Length - 1);
                            bool bHaveWrite = HaveWritePermission(Registry.LocalMachine, sCheckAccess);
                            if (!bHaveWrite)
                            {
                                cReg.RegSetOwnership(Registry.LocalMachine, sCheckAccess);
                            }
                        }

                        RString = RString + RLine + Environment.NewLine;
                    }
                    if (AddonCancel)
                        break;
                }
                if (AddonCancel)
                    break;

                cMain.CreateDirectory(cOptions.WinToolkitTemp);

                Files.DeleteFile(fs);

                var IRR = new StreamWriter(fs, true);
                IRR.Write(RString);
                IRR.Close();
                if (testRegCon)
                    cMain.OpenProgram("\"" + cMain.SysFolder + "\\Notepad.exe\"", fs, false, ProcessWindowStyle.Normal);
                cMain.FreeRAM();
                if (AddonCancel)
                    break;

                cMain.OpenProgram("\"" + cMain.SysRoot + "\\regedit.exe\"", "/s \"" + fs + "\"", true, ProcessWindowStyle.Hidden);

                Files.DeleteFile(fs);
            }
        }

        public static bool HaveWritePermission(RegistryKey Loc, string Key)
        {
            try
            {
                using (var oRegKey = Loc.OpenSubKey(Key, true))
                {
                }
            }
            catch (Exception Ex)
            {
                return false;
            }
            return true;
        }

        public static void IntegrateAddon(WIMImage image, string addon, string arc, string addonName, bool testRegCon, string index)
        {
            cMain.eErr = "";

            string AddDir = cOptions.WinToolkitTemp + "\\Addon-" + index + "\\";
            Files.DeleteFolder(AddDir, true);
            cMain.ExtractFiles(addon, AddDir);
            //if (!File.Exists(AddDir + "Tasks.txt") || cMain.DetectForChars(addon))
            //{
            // cMain.DeleteFolder(AddDir, true);
            // File.Copy(addon, cOptions.WinToolkitTemp + "\\" + index + "-Temp.WA", true);
            // cMain.ExtractFiles(cOptions.WinToolkitTemp + "\\" + index + "-Temp.WA", AddDir);
            // cMain.DeleteFile(cOptions.WinToolkitTemp + "\\" + index + "-Temp.WA");
            //}

            if (File.Exists(AddDir + "Tasks.txt"))
            {
                // 

                try
                {
                    ConvertRegAddon(AddDir, arc, testRegCon, image);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, AddDir);
                }
                var IA = new StreamReader(AddDir + "Tasks.txt", true);
                int Task = 0;
                while (IA.Peek() != -1)
                {
                    string strline = IA.ReadLine().Trim();
                    if (!string.IsNullOrEmpty(strline) && !strline.StartsWithIgnoreCase(";"))
                    {
                        string aSource = null;
                        if (!strline.StartsWithIgnoreCase("[") && !string.IsNullOrEmpty(strline))
                        {
                            if (strline.ContainsIgnoreCase("::"))
                            {
                                aSource = GetCopyTo(strline);
                                if (arc.ToUpper() != "DUAL")
                                {
                                    if (arc.ToUpper().EqualsIgnoreCase("X86") && !Directory.Exists(image.MountPath + "\\Windows\\SysWOW64"))
                                    {
                                        if (aSource.StartsWithIgnoreCase("PROGRAM FILES (X86)"))
                                        {
                                            aSource = "Program Files" + aSource.Substring(19);
                                        }
                                        if (aSource.StartsWithIgnoreCase("WINDOWS\\SYSWOW64"))
                                        {
                                            aSource = "Windows\\System32" + aSource.Substring(16);
                                        }
                                    }
                                    if (arc.ToUpper().EqualsIgnoreCase("X86") && Directory.Exists(image.MountPath + "\\Windows\\SysWOW64"))
                                    {
                                        if (aSource.StartsWithIgnoreCase("PROGRAM FILES") &&
                                         !aSource.StartsWithIgnoreCase("PROGRAM FILES (X86)"))
                                        {
                                            aSource = "Program Files (x86)" + aSource.Substring(13);
                                        }
                                        if (aSource.StartsWithIgnoreCase("WINDOWS\\SYSTEM32"))
                                        {
                                            aSource = "Windows\\SysWOW64" + aSource.Substring(16);
                                        }
                                    }
                                }
                            }
                            if (Task == 1 || Task == 7)
                            {
                                //WriteLogA(Addon + " Copying Folder " + strline);
                                string CopyFrom = AddDir + GetSource(strline);
                                string CopyTo = image.MountPath + "\\" + aSource;
                                try
                                {
                                    if (CopyTo.ContainsIgnoreCase("*"))
                                    {
                                        CopyTo = CopyTo.ReplaceIgnoreCase("*", "\\");
                                    }
                                    else
                                    {
                                        CopyTo += "\\" + GetSource(strline);
                                    }

                                    if (Directory.Exists(CopyFrom)) { cMain.CopyDirectory(CopyFrom, CopyTo, true, false); }
                                }
                                catch
                                {
                                    try
                                    {
                                        if (Directory.Exists(CopyTo))
                                        {
                                            cMain.TakeOwnership(CopyTo);
                                            cMain.ClearAttributeFile(CopyTo);
                                        }

                                        cMain.CopyDirectory(CopyFrom, CopyTo, true, false);
                                    }
                                    catch (Exception Ex)
                                    {
                                        LargeError LE = new LargeError("Copying Folder", "Error copying folder: " + CopyFrom, CopyFrom + Environment.NewLine + Environment.NewLine + CopyTo + Environment.NewLine + Environment.NewLine, Ex);
                                        LE.Upload(); LE.ShowDialog();
                                    }
                                }
                            }
                            if (Task == 2 || Task == 8)
                            {
                                //Copy File
                                string CopyFrom = AddDir + GetSource(strline);
                                string CopyTo = image.MountPath + "\\" + aSource;

                                if (CopyTo.ContainsIgnoreCase("*"))
                                {
                                    CopyTo = CopyTo.ReplaceIgnoreCase("*", "\\");
                                }
                                else
                                {
                                    CopyTo += "\\" + GetSource(strline);
                                }

                                try
                                {
                                    if (File.Exists(CopyFrom))
                                    {
                                        cMain.CreateDirectory(image.MountPath + "\\" + aSource);
                                        File.Copy(CopyFrom, CopyTo, true);
                                    }
                                }
                                catch
                                {
                                    try
                                    {
                                        if (File.Exists(CopyTo))
                                        {
                                            cMain.TakeOwnership(CopyTo);
                                            cMain.ClearAttributeFile(CopyTo);
                                        }

                                        File.Copy(CopyFrom, CopyTo, true);
                                    }
                                    catch (Exception ex)
                                    {
                                        cMain.WriteLog(null, "Can't copy file (Addon)", ex.Message, strline);
                                        MessageBox.Show(
                                         CopyFrom + Environment.NewLine + Environment.NewLine + CopyTo +
                                         Environment.NewLine + Environment.NewLine + ex.Message, "Error [CopyFile]");
                                    }
                                }
                            }
                            if (Task == 3)
                            {
                                try
                                {
                                    if (File.Exists(AddDir + GetSource(strline)))
                                    {
                                        File.Copy(AddDir + GetSource(strline), cMount.SourceToFolder(cOptions.LastWim) + GetSource(strline), true);
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    cMain.WriteLog(null, "Can't copy file [Disk] (Addon)", Ex.Message, strline);
                                    MessageBox.Show(
                                     "3Cannot copy file \"" + GetSource(strline) + "\" to \"" +
                                     cMount.SourceToFolder(cOptions.LastWim) + GetSource(strline) + "\": " +
                                     Ex.Message, "Error");
                                }
                            }
                            if (Task == 4)
                            {
                                try
                                {
                                    if (Directory.Exists(AddDir + GetSource(strline)))
                                    {
                                        cMain.CopyDirectory(AddDir + GetSource(strline), cMount.SourceToFolder(cOptions.LastWim) + GetSource(strline), true, false);
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    cMain.WriteLog(null, "Can't copy file [Disk] (Addon)", Ex.Message, strline);
                                    MessageBox.Show(
                                     "4Cannot copy folder \"" + GetSource(strline) + "\" to \"" +
                                     cMount.SourceToFolder(cOptions.LastWim) + GetSource(strline) + "\": " +
                                     Ex.Message, "Error");
                                }
                            }
                            if (Task == 5 || Task == 9)
                            {
                                try
                                {
                                    if (File.Exists(image.MountPath + "\\" + strline))
                                        Files.DeleteFile(image.MountPath + "\\" + strline);
                                    if (Directory.Exists(image.MountPath + "\\" + strline))
                                        Files.DeleteFolder(image.MountPath + "\\" + strline, false);
                                }
                                catch
                                {
                                    try
                                    {
                                        cMain.TakeOwnership(image.MountPath + "\\" + strline);
                                        cMain.ClearAttributeFile(image.MountPath + "\\" + strline);
                                        if (File.Exists(image.MountPath + "\\" + strline))
                                            Files.DeleteFile(image.MountPath + "\\" + strline);
                                        if (Directory.Exists(image.MountPath + "\\" + strline))
                                            Files.DeleteFolder(image.MountPath + "\\" + strline, false);
                                    }
                                    catch (Exception ex)
                                    {
                                        cMain.WriteLog(null, "Can't delete file/folder (Addon)", ex.Message, strline);
                                    }
                                }
                            }
                            if (Task == 6)
                            {
                                //WriteLogA(Addon + " Running Command " + strline);

                                bool AllowC = cOptions.AICommands;

                                if (strline.ContainsIgnoreCase("DISM")) { AllowC = true; }
                                if (strline.ContainsIgnoreCase("PKGMGR")) { AllowC = true; }

                                try
                                {
                                    if (AllowC)
                                    {
                                        string Fi = strline;
                                        string Ar = strline;
                                        while (Fi.ContainsIgnoreCase("|"))
                                        {
                                            Fi = Fi.Substring(0, Fi.Length - 1);
                                        }
                                        while (Ar.ContainsIgnoreCase("|"))
                                        {
                                            Ar = Ar.Substring(1);
                                        }

                                        if (Ar.ContainsIgnoreCase("\"%WIM%\""))
                                            Ar = Ar.ReplaceIgnoreCase("\"%WIM%\"", "\"" + image.MountPath + "\"");
                                        if (Ar.ContainsIgnoreCase("%WIM%"))
                                            Ar = Ar.ReplaceIgnoreCase("%WIM%", "\"" + image.MountPath + "\"");

                                        cReg.RegUnLoadAll();
                                        cMain.OpenProgram("\"" + Fi + "\"", Ar, true, ProcessWindowStyle.Hidden);
                                        string SHiveL = image.MountPath + "\\Windows\\System32\\Config\\";
                                        cReg.RegLoad("WIM_Software", SHiveL + "SOFTWARE");
                                        cReg.RegLoad("WIM_Admin", image.MountPath + "\\Users\\Administrator\\NTUSER.DAT");
                                        cReg.RegLoad("WIM_Default", image.MountPath + "\\Users\\Default\\NTUSER.DAT");
                                        cReg.RegLoad("WIM_SAM", SHiveL + "SAM");
                                        cReg.RegLoad("WIM_Security", SHiveL + "SECURITY");
                                        cReg.RegLoad("WIM_System", SHiveL + "SYSTEM");
                                        cReg.RegLoad("WIM_Components", SHiveL + "COMPONENTS");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("6Cannot run command :" + ex.Message, "Error");
                                    cMain.WriteLog(null, "Can't run comment (Addon)", ex.Message, strline);
                                }
                            }
                        }

                        if (strline.StartsWithIgnoreCase("["))
                        {
                            Task = 0;
                            if (string.Equals(strline, "[COPYFOLDER]"))
                                Task = 1;
                            if (string.Equals(strline, "[COPYFILE]"))
                                Task = 2;
                            if (string.Equals(strline, "[COPYFILEDISK]"))
                                Task = 3;
                            if (string.Equals(strline, "[COPYFOLDERDISK]"))
                                Task = 4;
                            if (string.Equals(strline, "[DELETE]"))
                                Task = 5;
                            if (string.Equals(strline, "[RUNCMD]"))
                                Task = 6;
                            if (Task == 0)
                                MessageBox.Show("You may have created a spelling mistake in the tasks.txt file.\n\n[" + strline + "]", "Error");
                        }
                        if (AddonCancel)
                            break;
                    }
                }
                IA.Close();

                try
                {
                    cMain.CreateDirectory(image.MountPath + "\\Windows\\WinToolkit\\Addons");
                    File.Copy(AddDir + "Tasks.txt", image.MountPath + "\\Windows\\WinToolkit\\Addons\\" + addonName + ".txt",
                            true);
                }
                catch (Exception Ex)
                {
                    cMain.WriteLog(null, "Error saving addon info to image.", Ex.Message, AddDir + Environment.NewLine + image.MountPath + "\\Windows\\WinToolkit\\Addons\\" + addonName + ".txt");
                }

                var shell2 = new IWshRuntimeLibrary.WshShell();
                var a = new DirectoryInfo(AddDir);
                FileInfo[] aryFi = a.GetFiles("*.lnk");
                if (aryFi.Length > 0)
                {
                    if (arc.EqualsIgnoreCase("x86") && Directory.Exists(image.MountPath + "\\Windows\\SysWOW64"))
                    {
                        foreach (FileInfo f in aryFi)
                        {
                            try
                            {
                                var link = (IWshRuntimeLibrary.IWshShortcut)shell2.CreateShortcut(AddDir + f.Name);
                                if (link.TargetPath.StartsWithIgnoreCase("C:\\PROGRAM FILES\\"))
                                {
                                    link.WorkingDirectory =
                                     Path.GetDirectoryName("%SystemDrive%\\Program Files (x86)\\" +
                                                    link.TargetPath.Substring(17));
                                    link.Save();
                                }
                            }
                            catch
                            {
                            }
                            if (AddonCancel)
                                break;
                        }
                    }

                    if (arc.EqualsIgnoreCase("x86") && !Directory.Exists(image.MountPath + "\\Windows\\SysWOW64"))
                    {
                        foreach (FileInfo f in aryFi)
                        {
                            try
                            {
                                var link = (IWshRuntimeLibrary.IWshShortcut)shell2.CreateShortcut(AddDir + f.Name);
                                if (link.TargetPath.StartsWithIgnoreCase("C:\\PROGRAM FILES (X86)\\"))
                                {
                                    link.WorkingDirectory =
                                     Path.GetDirectoryName("%SystemDrive%\\Program Files (x86)\\" +
                                                    link.TargetPath.Substring(23));
                                    link.Save();
                                }
                            }
                            catch
                            {
                            }
                            if (AddonCancel)
                                break;
                        }
                    }

                    foreach (FileInfo f in aryFi)
                    {

                        var link = (IWshRuntimeLibrary.IWshShortcut)shell2.CreateShortcut(AddDir + f.Name);

                        try
                        {
                            if (link.TargetPath.Substring(1, 2).EqualsIgnoreCase(":\\"))
                            {
                                link.WorkingDirectory =
                                 Path.GetDirectoryName("%SystemDrive%\\" + link.TargetPath.Substring(3));
                                link.Save();
                            }
                        }
                        catch
                        {
                        }

                    }
                }
            }
            else
            {
                // File.Copy(Addon, cOptions.WinToolkitTemp + "\\" + Index + "-Temp.WA", true);
                string E = "";
                if (File.Exists(addon)) { E += "Addon = True"; } else { E += "Addon = False"; } E += Environment.NewLine;
                if (File.Exists(cOptions.WinToolkitTemp + "\\" + index + "-Temp.WA")) { E += "CopyTo = True"; } else { E += "CopyTo = False"; } E += Environment.NewLine;
                if (Directory.Exists(AddDir)) { E += "ExtractTo = True"; } else { E += "ExtractTo = False"; } E += Environment.NewLine;
                cMain.WriteLog(null, "Could not get addon files" + Environment.NewLine + E, cMain.eErr, addon);
                MessageBox.Show("Win Toolkit was unable to extract the specified files", "Error");
            }

            Files.DeleteFolder(AddDir, false);
        }

        private static string GetSource(string line)
        {
            string strlineCFL = line;
            while (strlineCFL.ContainsIgnoreCase("::"))
            {
                strlineCFL = strlineCFL.Substring(0, strlineCFL.Length - 1);
            }
            return strlineCFL.Substring(0, strlineCFL.Length - 1);
        }

        private static string GetCopyTo(string line)
        {
            string strlineCFT = line;
            while (strlineCFT.ContainsIgnoreCase("::"))
            {
                strlineCFT = strlineCFT.Substring(1);
            }
            return strlineCFT.Substring(1);
        }
    }
}