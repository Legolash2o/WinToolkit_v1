using System.Linq;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using WinToolkit.Classes;

namespace WinToolkit
{
    internal static class cReg
    {
        public static bool WIMt;


        [StructLayout(LayoutKind.Sequential)]
        private struct LUID
        {
            public uint LowPart;
            public int HighPart;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LUID_AND_ATTRIBUTES
        {
            public LUID pLuid;
            public UInt32 Attributes;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct TokPriv1Luid
        {
            public int Count;
            public LUID Luid;
            public UInt32 Attr;
        }

        private const Int32 ANYSIZE_ARRAY = 1;
        private const UInt32 SE_PRIVILEGE_ENABLED = 0x00000002;
        private const UInt32 TOKEN_ADJUST_PRIVILEGES = 0x0020;
        private const UInt32 TOKEN_QUERY = 0x0008;

        private const uint HKEY_USERS = 0x80000003;
        private const uint HKEY_LOCAL_MACHINE = 0x80000002;
        private const string SE_RESTORE_NAME = "SeRestorePrivilege";
        private const string SE_BACKUP_NAME = "SeBackupPrivilege";

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, out LUID lpLuid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern bool AdjustTokenPrivileges(
             IntPtr htok,
             bool disableAllPrivileges,
             ref TokPriv1Luid newState,
             int len,
             IntPtr prev,
             IntPtr relen);

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern int RegLoadKey(UInt32 hKey, String lpSubKey, String lpFile);

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern long RegUnLoadKey(UInt32 hKey, string lpSubKey);

        private static IntPtr _myToken;
        private static TokPriv1Luid _tokenPrivileges = new TokPriv1Luid();
        private static TokPriv1Luid _tokenPrivileges2 = new TokPriv1Luid();

        private static LUID _restoreLuid;
        private static LUID _backupLuid;

        public static bool RegLoad(string lpSubKey, string lpFile)
        {
            if (RegCheckMounted(lpSubKey)) { return true; }

            if (File.Exists(lpFile))
            {
                if (cMain.IsFileLocked(lpFile))
                {
                    while (cMain.IsFileLocked(lpFile))
                    {
                        Thread.Sleep(100);
                        Application.DoEvents();
                    }
                }

                try
                {
                    cMain.TakeOwnership(lpFile);
                    cMain.ClearAttributeFile(lpFile);
                }
                catch { }
                string sError = "";
                try
                {
                    if (!OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out _myToken))
                        sError += "OpenProcess Error" + Environment.NewLine;

                    if (!LookupPrivilegeValue(null, SE_RESTORE_NAME, out _restoreLuid))
                        sError += "LookupPrivilegeValue Error [RESTORE_NAME]" + Environment.NewLine;

                    if (!LookupPrivilegeValue(null, SE_BACKUP_NAME, out _backupLuid))
                        sError += "LookupPrivilegeValue Error [BACKUP_NAME]" + Environment.NewLine;

                    _tokenPrivileges.Attr = SE_PRIVILEGE_ENABLED;
                    _tokenPrivileges.Luid = _restoreLuid;
                    _tokenPrivileges.Count = 1;

                    _tokenPrivileges2.Attr = SE_PRIVILEGE_ENABLED;
                    _tokenPrivileges2.Luid = _backupLuid;
                    _tokenPrivileges2.Count = 1;

                    if (!AdjustTokenPrivileges(_myToken, false, ref _tokenPrivileges, 0, IntPtr.Zero, IntPtr.Zero))
                        sError += "\nAdjustTokenPrivileges Error: " + new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error()).Message + Environment.NewLine;

                    if (!AdjustTokenPrivileges(_myToken, false, ref _tokenPrivileges2, 0, IntPtr.Zero, IntPtr.Zero))
                        sError += "\nAdjustTokenPrivileges2 Error: " + new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error()).Message + Environment.NewLine;
                }
                catch (Exception Ex)
                {
                    sError += "\nAdjustTokenPrivilegesMain Error: " + new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error()).Message + Environment.NewLine;
                }

                if (cMain.IsFileLocked(lpFile))
                {
                    while (cMain.IsFileLocked(lpFile))
                    {
                        Thread.Sleep(100);
                        Application.DoEvents();
                    }
                }

                int retVal = RegLoadKey(HKEY_LOCAL_MACHINE, lpSubKey, lpFile);
                if (retVal != 0)
                { //retVal: 1168 - Element not found, check if image needs restoring
                    sError += "\nRegLoadKey Error: " + new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error()).Message + Environment.NewLine;

                }

                if (!RegCheckMounted(lpSubKey))
                {
                    sError += "Unable to Mount" + Environment.NewLine
                              + "Error Code: " + retVal + Environment.NewLine
                              + "Error Message: " + cMain.RunExternal("net", "helpmsg " + retVal)
                              + "Key: HKLM\\" + lpSubKey + Environment.NewLine
                              + "File: '" + lpFile + "'" + Environment.NewLine;
                    try
                    {
                        FileInfo fi = new FileInfo(lpFile);
                        sError += "Attributes: " + fi.Attributes.ToString();
                    }
                    catch (Exception)
                    {
                        sError += "Attributes: ERROR";
                    }


                    new SmallError("Registry Mount Error [" + retVal + "]", null, sError).Upload();
                    cMain.ErrorBox("Win Toolkit could not mount '" + lpFile + "'. This can be caused by an anti-virus so try temporarily disabling it and try again.", "Registry Mount Error [" + retVal + "]", sError);
                    return false;
                }

                return true;
            }
            return false;
        }

        public static void RegUnLoadAll()
        {
            cMain.KillProcess("regedit");

            try
            {
                using (var oRegKey = Registry.LocalMachine.OpenSubKey("", RegistryKeyPermissionCheck.ReadSubTree))
                {
                    foreach (string I in oRegKey.GetSubKeyNames())
                    {
                        cMain.RunExternal("\"" + cMain.SysFolder + "\\reg.exe\"", "unload \"HKLM\\" + I + "\"");
                    }
                }
            }
            catch { }

        }

        public static void RegUnLoad(string lpSubKey)
        {
            cMain.KillProcess("regedit");

            string S = cMain.RunExternal("\"" + cMain.SysFolder + "\\reg.exe\"", "unload HKLM\\" + lpSubKey);
            if (RegCheckMounted(lpSubKey))
            {
                MessageBox.Show(S, "Error Unloading Registry");
            }
        }

        public class Mounted
        {
            public string WimFile;
            public string MountPath;
            public int State;
            public int Index;
            public string Key;
        }

        public static List<Mounted> RegCheckMountDism()
        {

            List<Mounted> mounted = new List<Mounted>();
            try
            {
                using (var oRegKeyM = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\WIMMount\\Mounted Images", RegistryKeyPermissionCheck.ReadSubTree))
                {
                    if (oRegKeyM == null) return mounted;

                    foreach (string rk in oRegKeyM.GetSubKeyNames())
                    {
                        try
                        {
                            var M = new Mounted
                            {
                                Key = rk,
                                WimFile = GetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\WIMMount\\Mounted Images\\" + rk, "WIM Path"),
                                MountPath = GetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\WIMMount\\Mounted Images\\" + rk, "Mount Path")
                            };

                            string state = GetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\WIMMount\\Mounted Images\\" + rk, "Status");
                            M.State = int.Parse(state);

                            string index = GetValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\WIMMount\\Mounted Images\\" + rk, "Image Index");
                            M.Index = int.Parse(index);

                            mounted.Add(M);
                        }
                        catch (Exception Ex) {
                            LargeError LE = new LargeError("Mount Checking Error", "Error getting mount information.", Ex);
                            LE.Upload();
                            LE.ShowDialog();
                        }
                    }
                    return mounted;
                }
            }
            catch { return mounted; }
        }

        public static string GetValue(RegistryKey Loc, string Key, string Item)
        {
            if (WIMt)
            {
                Key = ConvertKey(Loc, Key);
                Loc = Registry.LocalMachine;
            }
            Key = Key.ReplaceIgnoreCase("\\\\", "\\");

            try
            {
                using (var oRegKey = Loc.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadSubTree))
                {
                    if (oRegKey == null) { return ""; }
                    return oRegKey.GetValue(Item,null).ToString();
                }
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static IEnumerable<string> GetAllValues(RegistryKey Loc, string Key)
        {
            if (WIMt)
            {
                Key = ConvertKey(Loc, Key);
                Loc = Registry.LocalMachine;
            }
            Key = Key.ReplaceIgnoreCase("\\\\", "\\");

            try
            {
                using (var oRegKey = Loc.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadSubTree))
                {
                    if (oRegKey != null) return oRegKey.GetValueNames();
                }
            }
            catch { }
            return Enumerable.Empty<string>();
        }

        public static bool RegCheckMounted(string Key)
        {
            try
            {
                using (var oRegKey = Registry.LocalMachine.OpenSubKey(Key, RegistryKeyPermissionCheck.ReadSubTree))
                {
                    if (oRegKey == null) { return false; }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void CreateRegKey(RegistryKey Loc, string Key)
        {
            if (WIMt)
            {
                Key = ConvertKey(Loc, Key);
                Loc = Registry.LocalMachine;
            }
            Key = Key.ReplaceIgnoreCase("\\\\", "\\");

            if (Key.StartsWithIgnoreCase("WIM_Default\\"))
            {
                if (RegCheckMounted("WIM_Admin"))
                {
                    CreateRegKey(Registry.LocalMachine, Key.ReplaceIgnoreCase("WIM_Default\\", "WIM_Admin\\"));
                }

                if (RegCheckMounted("WIM_SYSDefault"))
                {
                    CreateRegKey(Registry.LocalMachine, Key.ReplaceIgnoreCase("WIM_Default\\", "WIM_SYSDefault\\"));
                }
            }

            try
            {
                using (var oRegFold = Loc.CreateSubKey(Key))
                {
                    WriteLog("CreateKey:" + Loc + "\\" + Key);
                }
            }
            catch { }
        }

        public static bool RegCheckWIMFltr()
        {
            try
            {
                using (var oRegKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\services\\WimFltr", RegistryKeyPermissionCheck.ReadSubTree))
                {
                    if (oRegKey == null) { return false; }
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }

        private static string ConvertKey(RegistryKey Loc, string Key)
        {
            string K = Key;
            if (Key.ContainsIgnoreCase("CURRENTCONTROLSET")) { K = K.ReplaceIgnoreCase( "CurrentControlSet", "ControlSet001"); }
            if (!K.StartsWithIgnoreCase("WIM_"))
            {
                try
                {
                    switch (Convert.ToString(Loc))
                    {
                        case "HKEY_CURRENT_USER":
                            K = "WIM_Default\\" + Key;
                            break;
                        case "HKEY_LOCAL_MACHINE":
                            if (Key.StartsWithIgnoreCase("SOFTWARE\\"))
                            {
                                K = "WIM_" + Key;
                            }
                            break;
                        case "HKEY_CLASSES_ROOT":
                            K = "WIM_Software\\Classes" + Key;
                            break;
                    }

                    if (Key.StartsWithIgnoreCase("SYSTEM"))
                    {
                        K = "WIM_" + Key;
                    }
                    if (Convert.ToString(Loc).EqualsIgnoreCase("HKEY_USERS") && Key.ContainsIgnoreCase(".DEFAULT\\"))
                    {
                        K = "WIM_Default\\" + K.ReplaceIgnoreCase( ".DEFAULT\\", "");
                    }
                }
                catch
                {
                }
            }
            K = K.ReplaceIgnoreCase("\\\\", "\\");
            //MessageBox.Show(K, WIMt.ToString());
            return K;
        }

        public static void WriteValue(RegistryKey Loc, string Key, string Reg, object Value, RegistryValueKind Kind = RegistryValueKind.String)
        {
            if (WIMt)
            {
                Key = ConvertKey(Loc, Key);
                Loc = Registry.LocalMachine;
            }
            Key = Key.ReplaceIgnoreCase("\\\\", "\\");

            if (Key.StartsWithIgnoreCase("WIM_Default\\"))
            {
                if (RegCheckMounted("WIM_Admin"))
                {
                    WriteValue(Registry.LocalMachine, Key.ReplaceIgnoreCase("WIM_Default\\", "WIM_Admin\\"), Reg, Value, Kind);
                }

                if (RegCheckMounted("WIM_SYSDefault"))
                {
                    WriteValue(Registry.LocalMachine, Key.ReplaceIgnoreCase("WIM_Default\\", "WIM_SYSDefault\\"), Reg, Value, Kind);
                }
            }

            try
            {
                using (var oRegFold = Loc.CreateSubKey(Key)) ;
            }
            catch (Exception Ex) { }

            try
            {
                using (var oRegKey = Loc.OpenSubKey(Key, true))
                {
                    oRegKey.SetValue(Reg, Value, Kind);
                    WriteLog("WriteValue:" + Loc + "\\" + Key + " | " + Reg + "=" + Value + " | " + Kind);
                }
            }
            catch (Exception Ex) { }
        }

        public static void DeleteValue(RegistryKey Loc, string Key, string Reg)
        {
            if (WIMt)
            {
                Key = ConvertKey(Loc, Key);
                Loc = Registry.LocalMachine;
            }
            Key = Key.ReplaceIgnoreCase( "\\\\", "\\");

            if (Key.StartsWithIgnoreCase("WIM_Default\\"))
            {
                if (RegCheckMounted("WIM_Admin"))
                {
                    DeleteValue(Registry.LocalMachine, Key.ReplaceIgnoreCase("WIM_Default\\", "WIM_Admin\\"), Reg);
                }

                if (RegCheckMounted("WIM_SYSDefault"))
                {
                    DeleteValue(Registry.LocalMachine, Key.ReplaceIgnoreCase("WIM_Default\\", "WIM_SYSDefault\\"), Reg);
                }
            }

            try
            {
                using (var regKey = Loc.OpenSubKey(Key, true))
                {
                    regKey.DeleteValue(Reg, false);
                    WriteLog("DeleteValue:" + Loc + "\\" + Key + " | " + Reg);
                }
            }
            catch
            {
            }
        }

        private static void WriteLog(string Log)
        {
            try
            {
                if (cOptions.RegistryLog)
                {
                    if (!Directory.Exists(cMain.Root + "Logs")) { cMain.CreateDirectory(cMain.Root + "Logs"); }
                    using (var SW = new StreamWriter(cMain.Root + "Logs\\Registry.txt", true))
                    {
                        SW.Write(DateTime.Now.ToString("dd-MM-yyyy_HH:mmtt", System.Globalization.CultureInfo.InvariantCulture) + " | " + Log + Environment.NewLine);
                    }
                }
            }
            catch (Exception Ex)
            {
                new SmallError("Error writing registry log", Ex, Log).Upload();
            }
        }

        public static void DeleteKey(RegistryKey Loc, string pKey, string cKey)
        {
            if (WIMt)
            {
                pKey = ConvertKey(Loc, pKey);
                Loc = Registry.LocalMachine;
            }
            pKey = pKey.ReplaceIgnoreCase( "\\\\", "\\");
            if (pKey.StartsWithIgnoreCase("WIM_Default\\"))
            {
                if (RegCheckMounted("WIM_Admin"))
                {
                    DeleteKey(Registry.LocalMachine, pKey.ReplaceIgnoreCase( "WIM_Default\\", "WIM_Admin\\"), cKey);
                }

                if (RegCheckMounted("WIM_SYSDefault"))
                {
                    DeleteKey(Registry.LocalMachine, pKey.ReplaceIgnoreCase( "WIM_Default\\", "WIM_SYSDefault\\"), cKey);
                }
            }

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

        public static void RegSetOwnership(RegistryKey Loc, string nKey)
        {
            if (WIMt)
            {
                nKey = ConvertKey(Loc, nKey);
                Loc = Registry.LocalMachine;
            }
            nKey = nKey.ReplaceIgnoreCase( "\\\\", "\\");

            if (nKey.StartsWithIgnoreCase("WIM_Default\\"))
            {
                if (RegCheckMounted("WIM_Admin"))
                {
                    RegSetOwnership(Registry.LocalMachine, nKey.ReplaceIgnoreCase( "WIM_Default\\", "WIM_Admin\\"));
                }

                if (RegCheckMounted("WIM_SYSDefault"))
                {
                    RegSetOwnership(Registry.LocalMachine, nKey.ReplaceIgnoreCase( "WIM_Default\\", "WIM_SYSDefault\\"));
                }
            }

            var sid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
            var account = (NTAccount)sid.Translate(typeof(NTAccount));
            try
            {
                var myProcToken = new AccessTokenProcess(Process.GetCurrentProcess().Id,
                                                                      TokenAccessType.TOKEN_ALL_ACCESS |
                                                                      TokenAccessType.TOKEN_ADJUST_PRIVILEGES);
                myProcToken.EnablePrivilege(new TokenPrivilege(TokenPrivilege.SE_TAKE_OWNERSHIP_NAME, true));
            }
            catch (Exception Ex)
            {
            }

            try
            {
                using (var nParentKey = Loc.OpenSubKey(nKey, RegistryKeyPermissionCheck.ReadWriteSubTree,
                                                     RegistryRights.TakeOwnership | RegistryRights.ReadKey |
                                                     RegistryRights.ReadPermissions))
                {
                    RegistrySecurity nSubKeySec = nParentKey.GetAccessControl(AccessControlSections.Owner);
                    nSubKeySec.SetOwner(new NTAccount(account.Value));
                    nParentKey.SetAccessControl(nSubKeySec);
                    nSubKeySec = nParentKey.GetAccessControl(AccessControlSections.Access);
                    var nAccRule = new RegistryAccessRule(account.Value, RegistryRights.FullControl,
                                            InheritanceFlags.ContainerInherit |
                                            InheritanceFlags.ObjectInherit, PropagationFlags.None,
                                            AccessControlType.Allow);
                    nSubKeySec.AddAccessRule(nAccRule);
                    nParentKey.SetAccessControl(nSubKeySec);

                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(null, "Could not take ownership", Ex.Message, nKey + Environment.NewLine + account.Value);
            }

        }

        //HiveMountDir + "Software\\Microsoft\\Windows\\CurrentVersion\\Component Based Servicing\\"

        public static void ShowPackages(string CN, bool DefVis, bool Owners, bool MountReg, WIMImage image)
        {
            if (MountReg)
            {
                RegLoad("WIM_Software", image.MountPath + "\\Windows\\System32\\Config\\SOFTWARE");
            }

            if (RegCheckMounted("WIM_Software"))
            {
                if (RegCheckMounted("WIM_Software\\Microsoft\\Windows\\CurrentVersion\\Component Based Servicing\\Packages"))
                {
                    RegSetOwnership(Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows\\CurrentVersion\\Component Based Servicing\\Packages\\");
                    CleanComponentSubkeys("WIM_Software\\Microsoft\\Windows\\CurrentVersion\\Component Based Servicing\\Packages\\", CN, DefVis, Owners);
                }

                if (RegCheckMounted("WIM_Software\\Microsoft\\Windows\\CurrentVersion\\Component Based Servicing\\PackagesPending"))
                {
                    RegSetOwnership(Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows\\CurrentVersion\\Component Based Servicing\\PackagesPending\\");
                    CleanComponentSubkeys("WIM_Software\\Microsoft\\Windows\\CurrentVersion\\Component Based Servicing\\PackagesPending\\", CN, DefVis, Owners);
                }

                if (MountReg)
                {
                    RegUnLoad("WIM_Software");
                }
            }

        }

        public static void CleanComponentSubkeys(string registryPath, string CN, bool DefVis, bool Owners)
        {
            try
            {
                using (var MyKey = Registry.LocalMachine.OpenSubKey(registryPath, true))
                {
                    var windowsIdentity = WindowsIdentity.GetCurrent();
                    if (windowsIdentity != null)
                    {
                        IdentityReference CurUser = windowsIdentity.User;

                        int count = 1;

                        var myProcToken = new AccessTokenProcess(Process.GetCurrentProcess().Id, TokenAccessType.TOKEN_ALL_ACCESS | TokenAccessType.TOKEN_ADJUST_PRIVILEGES);
                        myProcToken.EnablePrivilege(new TokenPrivilege(TokenPrivilege.SE_TAKE_OWNERSHIP_NAME, true));

                        foreach (string subkeyname in MyKey.GetSubKeyNames())
                        {
                            if (subkeyname.ContainsIgnoreCase(CN))
                            {
                                try
                                {
                                    RegSetOwnership(MyKey, subkeyname);
                                    RegistryAccessRule ntmp = RegSetFullAccess(MyKey, subkeyname, CurUser);
                                    try
                                    {
                                        using (var nSubKey = MyKey.OpenSubKey(subkeyname, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.FullControl))
                                        {
                                            if (nSubKey.GetValue("Visibility") != null)
                                            {
                                                if (DefVis == false)
                                                {
                                                    if (nSubKey.GetValue("DefVis") == null)
                                                    {
                                                        nSubKey.SetValue("DefVis", nSubKey.GetValue("Visibility"), RegistryValueKind.DWord);
                                                    }
                                                    nSubKey.SetValue("Visibility", 0x00000001, RegistryValueKind.DWord);
                                                }
                                                else
                                                {
                                                    if (nSubKey.GetValue("DefVis") != null)
                                                    {
                                                        nSubKey.SetValue("Visibility", nSubKey.GetValue("DefVis"), RegistryValueKind.DWord);
                                                    }
                                                }
                                            }

                                            if (Owners)
                                            {
                                                foreach (string RK in nSubKey.GetSubKeyNames())
                                                {
                                                    if (RK.ToUpper().EndsWithIgnoreCase("OWNERS"))
                                                    {
                                                        RegSetOwnership(MyKey, subkeyname + "\\Owners");
                                                        RegSetFullAccess(MyKey, subkeyname + "\\Owners", CurUser);
                                                        nSubKey.DeleteSubKey("Owners");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception Ex)
                                    {
                                        MessageBox.Show(Ex.Message);
                                    }

                                    RegRemoveAccess(MyKey, subkeyname, ntmp);
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show(Ex.Message, subkeyname);
                                }
                                count++;
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, registryPath);
            }
        }

        private static void RegRemoveAccess(RegistryKey nParentKey, string nkey, RegistryAccessRule nacc)
        {
            if (nkey.StartsWithIgnoreCase("WIM_Default\\"))
            {
                if (RegCheckMounted("WIM_Admin"))
                {
                    RegRemoveAccess(Registry.LocalMachine,nkey.ReplaceIgnoreCase( "WIM_Default\\", "WIM_Admin\\"), nacc);
                }

                if (RegCheckMounted("WIM_SYSDefault"))
                {
                    RegRemoveAccess(Registry.LocalMachine,nkey.ReplaceIgnoreCase( "WIM_Default\\", "WIM_SYSDefault\\"), nacc);
                }
            }
            try
            {
                using (var nSubKey = nParentKey.OpenSubKey(nkey, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.ChangePermissions | RegistryRights.ReadKey))
                {
                    RegistrySecurity nSubKeySec = nSubKey.GetAccessControl(AccessControlSections.Access);
                    nSubKeySec.RemoveAccessRule(nacc);
                    nSubKey.SetAccessControl(nSubKeySec);
                }
            }
            catch { }
        }

        private static RegistryAccessRule RegSetFullAccess(RegistryKey nParentKey, string nkey, IdentityReference nuser)
        {
            if (nkey.StartsWithIgnoreCase("WIM_Default\\"))
            {
                if (RegCheckMounted("WIM_Admin"))
                {
                    RegSetFullAccess(nParentKey, nkey.ReplaceIgnoreCase( "WIM_Default\\", "WIM_Admin\\"), nuser);
                }

                if (RegCheckMounted("WIM_SYSDefault"))
                {
                    RegSetFullAccess(nParentKey, nkey.ReplaceIgnoreCase( "WIM_Default\\", "WIM_SYSDefault\\"), nuser);
                }
            }

            try
            {
                using (var nSubKey = nParentKey.OpenSubKey(nkey, RegistryKeyPermissionCheck.ReadWriteSubTree,
                                                          RegistryRights.ReadKey | RegistryRights.ChangePermissions |
                                                          RegistryRights.ReadPermissions))
                {
                    RegistrySecurity nSubKeySec = nSubKey.GetAccessControl(AccessControlSections.Access);
                    var nAccRule = new RegistryAccessRule(nuser, RegistryRights.FullControl, AccessControlType.Allow);
                    nSubKeySec.AddAccessRule(nAccRule);
                    nSubKey.SetAccessControl(nSubKeySec);
                    nSubKey.Close();
                    return nAccRule;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}