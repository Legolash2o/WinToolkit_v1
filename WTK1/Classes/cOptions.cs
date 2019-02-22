using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using Microsoft.Win32;
using WinToolkit.Classes;

namespace WinToolkit
{
    public static class cOptions
    {
      public static string NF = "";
        public static string NV = "";
        public static string NM = "";
        public static string NL = "";
        //These are settings which are stored in the text file on close and then loaded on startup.
        public static bool TransparencyAll;
        public static double Transparency = 100;

        private static string _lastWIM = "";

        public static string LastWim
        {
            get
            {
                if (string.IsNullOrEmpty(_lastWIM))
                    return "";

                string DVDP = _lastWIM.ReplaceIgnoreCase("\\\\", "\\");
                if (cMain.IsReadOnly(Path.GetDirectoryName(DVDP)))
                {
                    return "";
                }
                return _lastWIM;
            }
            set
            {
                _lastWIM = value;
            }
        }

        public static void AddUpdateToRemember(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && !RememberSelectedUpdates.Contains(filePath, StringComparer.CurrentCultureIgnoreCase))
                RememberSelectedUpdates.Add(filePath);
        }

        /// <summary>
        /// Stores what the user last selected in the 'Known Updates' screen.
        /// </summary>
        public static List<string> RememberSelectedUpdates = new List<string>();

        public static string pLastWim = "";
        public static string WinToolkitTemp = cMain.UserTempPath;
        public static string MountTemp = "";
        public static string WinToolkitPri = "Normal";
        public static string WinToolkitExt = "Normal";
        public static string WinToolkitDISM = "Normal";
        public static string LastDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static bool AICommands = true;
        public static string AIOTime = "";
        public static bool AVScan = true;
        public static bool DLogging = true;
        public static bool AIOSave = true;
        public static bool UpdFilter = true;
        public static bool CheckForUpdates = true;
        public static string FormSize = "";
        public static bool mVerify;
        public static bool mCheck;
        public static bool QuickMerge = true;
        public static bool DISMUpdate = true;
        public static string SolDownload = cMain.Root + "Updates\\";
        public static bool UploadLogs = true;
        public static string LastISO_Folder = "";
        public static string LastISO_ISO = "";
        public static bool MountLog = true;
        public static bool RegistryLog;
        public static bool PresetManager = true;
        public static bool FreeRAM = true;
        public static bool PreventSleep = true;
        public static bool ShowWelcome = true;
        public static string DISMLoc = "";
        public static bool AddRunOnce = true;
        public static bool DeleteMount = true;
        public static bool RebuildISOMaker = true;

        public static bool AutoClean = true;
        public static bool AutoCleanLogs = false;
        public static bool AutoCleanSXS = false;
        public static bool AutoCleanManCache = false;

        public static string fAddons = "";
        public static string fDrivers = "";
        public static string fGadgets = "";
        public static string fSilents = "";
        public static string fThemes = "";
        public static string fTweaks = "";
        public static string fUpdates = "";
        public static string fWallpapers = "";
        public static int ScaleOptions = 0;


        //CommandLine Args
        public static bool SkipOSCheck;
        public static bool SkipSettings;
        public static bool SkipUpdate;
        public static bool SkipWimFltr;
        public static bool SkipWimFltrRestart;



        public static string SaveSettings()
        {
            string nSettings = "";
            string progress = "Preparing...";
            try
            {
                nSettings += "fAddons=" + fAddons + Environment.NewLine;
                nSettings += "fDrivers=" + fDrivers + Environment.NewLine;
                nSettings += "fGadgets=" + fGadgets + Environment.NewLine;
                nSettings += "fSilents=" + fSilents + Environment.NewLine;
                nSettings += "fThemes=" + fThemes + Environment.NewLine;
                nSettings += "fTweaks=" + fTweaks + Environment.NewLine;
                nSettings += "fUpdates=" + fUpdates + Environment.NewLine;
                nSettings += "fWallpapers=" + fWallpapers + Environment.NewLine;
                nSettings += Environment.NewLine;
                if (WinToolkitTemp != cMain.UserTempPath)
                {
                    nSettings += "sWinToolkitTemp=" + WinToolkitTemp + Environment.NewLine;
                }
                nSettings += "sMountTemp=" + MountTemp + Environment.NewLine;
                nSettings += Environment.NewLine;

                nSettings += "sAddRunOnce=" + AddRunOnce + Environment.NewLine;
                nSettings += "sAICommands=" + AICommands + Environment.NewLine;
                nSettings += "sAVScan=" + AVScan + Environment.NewLine;
                nSettings += "sAIOSave=" + AIOSave + Environment.NewLine;
                nSettings += "sScaleOptions=" + ScaleOptions + Environment.NewLine;
                nSettings += "sUpdFilter=" + UpdFilter + Environment.NewLine;
                nSettings += "sAIOTIME=" + AIOTime + Environment.NewLine;
                nSettings += "sPreventSleep=" + PreventSleep + Environment.NewLine;
                nSettings += "sCheckForUpdates=" + CheckForUpdates + Environment.NewLine;
                nSettings += "sDLogging=" + DLogging + Environment.NewLine;
                nSettings += "sDISMUpdate=" + DISMUpdate + Environment.NewLine;
                nSettings += "sFreeRAM=" + FreeRAM + Environment.NewLine;
                nSettings += "sLastDir=" + LastDir + Environment.NewLine;
                nSettings += "sLastWIM=" + LastWim + Environment.NewLine;
                nSettings += "sPLastWIM=" + pLastWim + Environment.NewLine;
                nSettings += "sDeleteMount=" + DeleteMount + Environment.NewLine;
                nSettings += "smCheck=" + mCheck + Environment.NewLine;
                nSettings += "sAIOPM=" + PresetManager + Environment.NewLine;
                nSettings += "sQuickMerge=" + QuickMerge + Environment.NewLine;
                nSettings += "sFormSize=" + FormSize + Environment.NewLine;
                nSettings += "smVerify=" + mVerify + Environment.NewLine;
                nSettings += "sSolDownload=" + SolDownload + Environment.NewLine;
                nSettings += "sTransparency=" + Transparency + Environment.NewLine;
                nSettings += "sTransparencyAll=" + TransparencyAll + Environment.NewLine;
                nSettings += "sWinToolkitDISM=" + WinToolkitDISM + Environment.NewLine;
                nSettings += "sWinToolkitExt=" + WinToolkitExt + Environment.NewLine;
                nSettings += "sWinToolkitPRI=" + WinToolkitPri + Environment.NewLine;
                //nSettings += "sShowDonateAd=" + ShowDonateAd + Environment.NewLine;
                nSettings += "sUploadLogs=" + UploadLogs + Environment.NewLine;
                nSettings += "sLastISO_Folder=" + LastISO_Folder + Environment.NewLine;
                nSettings += "sLastISO_ISO=" + LastISO_ISO + Environment.NewLine;
                nSettings += "sMountLog=" + MountLog + Environment.NewLine;
                nSettings += "sShowWelcome=" + ShowWelcome + Environment.NewLine;
                nSettings += "sRegistryLog=" + RegistryLog + Environment.NewLine;
                nSettings += "sDismLoc=" + DISMLoc + Environment.NewLine;
                nSettings += "sRebuildISOMaker=" + RebuildISOMaker + Environment.NewLine;
                nSettings += "sKnownUpdates=" + RememberSelectedUpdates.Where(ss => !string.IsNullOrEmpty(ss)).Aggregate("", (current, s) => current + (s + ";")) + Environment.NewLine;
                nSettings += "sAutoClean=" + AutoClean + Environment.NewLine;
                nSettings += "sAutoCleanSXS=" + AutoCleanSXS + Environment.NewLine;
                nSettings += "sAutoCleanManCache=" + AutoCleanManCache + Environment.NewLine;
                nSettings += "sAutoCleanLogs=" + AutoCleanLogs + Environment.NewLine;


                progress = "Tidying...";
                string nSettingsClean = nSettings;
                nSettings = "";
                foreach (string sSetting in nSettingsClean.Split(Environment.NewLine.ToCharArray()))
                {
                    if (String.IsNullOrEmpty(sSetting)) { continue; }
                    if (sSetting.EndsWithIgnoreCase("=")) { continue; }
                    nSettings += sSetting + Environment.NewLine;
                }
                progress = "Waiting...";
                string saveTo = cMain.Root + "Settings.txt";
                while (cMain.IsFileLocked(saveTo))
                {
                    System.Threading.Thread.Sleep(100);
                    Application.DoEvents();
                }

                progress = "Saving...";
                using (var sF = new StreamWriter(saveTo, false, Encoding.UTF8))
                {
                    sF.Write(nSettings);
                }
            }
            catch (Exception Ex)
            {
                LargeError LE = new LargeError("Saving Settings", "Unable to save settings.\n\nTry turning your anti-virus off.", "Progress: " + progress + "\r\nExists: " + File.Exists(cMain.Root + "Settings.txt") + "\r\nSettings:\r\n" + nSettings, Ex);
                LE.Upload();
                LE.ShowDialog();
            }
            return nSettings.Trim();
        }

        public static void GetSettings()
        {
            string sSettings;
            using (var sF = new StreamReader(cMain.Root + "Settings.txt", Encoding.UTF8))
            {
                sSettings = sF.ReadToEnd();
            }

            foreach (string S in sSettings.Split(Environment.NewLine.ToCharArray()))
            {
                if (String.IsNullOrEmpty(S) || S.StartsWithIgnoreCase("#")) { continue; }
                try
                {
                    if (S.StartsWithIgnoreCase("FADDONS")) { fAddons = GetValue(S); }
                    if (S.StartsWithIgnoreCase("FDRIVERS")) { fDrivers = GetValue(S); }
                    if (S.StartsWithIgnoreCase("FGADGETS")) { fGadgets = GetValue(S); }
                    if (S.StartsWithIgnoreCase("FSILENTS")) { fSilents = GetValue(S); }
                    if (S.StartsWithIgnoreCase("FTHEMES")) { fThemes = GetValue(S); }
                    if (S.StartsWithIgnoreCase("FTWEAKS")) { fTweaks = GetValue(S); }
                    if (S.StartsWithIgnoreCase("FUPDATES")) { fUpdates = GetValue(S); }
                    if (S.StartsWithIgnoreCase("FWALLPAPERS")) { fWallpapers = GetValue(S); }

                    if (S.StartsWithIgnoreCase("SADDRUNONCE")) { AddRunOnce = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SAICOMMANDS")) { AICommands = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SAVSCAN")) { AVScan = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SAIOSAVE")) { AIOSave = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SUPDFILTER")) { UpdFilter = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SAIOTIME")) { AIOTime = GetValue(S); }
                    if (S.StartsWithIgnoreCase("SPREVENTSLEEP")) { PreventSleep = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SCHECKFORUPDATES")) { CheckForUpdates = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SDLOGGING")) { DLogging = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SFREERAM")) { FreeRAM = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SLASTDIR")) { LastDir = GetValue(S); }
                    if (S.StartsWithIgnoreCase("SSCALEOPTIONS")) { ScaleOptions = Convert.ToInt16(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SPLASTWIM")) { pLastWim = GetValue(S); }
                    if (S.StartsWithIgnoreCase("SLASTISO_ISO")) { LastISO_ISO = GetValue(S); }
                    if (S.StartsWithIgnoreCase("SLASTISO_FOLDER")) { LastISO_Folder = GetValue(S); }
                    if (S.StartsWithIgnoreCase("SFORMSIZE")) { FormSize = GetValue(S); }
                    if (S.StartsWithIgnoreCase("SDISMUPDATE")) { DISMUpdate = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SLASTWIM")) { LastWim = GetValue(S).ReplaceIgnoreCase("\\\\", "\\"); }
                    if (S.StartsWithIgnoreCase("SMCHECK")) { mCheck = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SAIOPM")) { PresetManager = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SMOUNTTEMP"))
                    {
                        string FBD = GetValue(S);

                        if (Directory.Exists(FBD.Substring(0, 3)) && !FBD.ContainsForeignCharacters())
                        {
                            try
                            {
                                var DI = new DriveInfo(FBD.Substring(0, 1));
                                if (DI.IsReady && DI.DriveFormat.EqualsIgnoreCase("NTFS") && DI.DriveType == DriveType.Fixed)
                                {
                                    FBD = FBD.ReplaceIgnoreCase("\\\\", "\\");
                                    MountTemp = FBD;
                                }
                            }
                            catch { }
                        }
                    }
                    if (S.StartsWithIgnoreCase("SMVERIFY")) { mVerify = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SSOLDOWNLOAD")) { SolDownload = GetValue(S); }
                    if (S.StartsWithIgnoreCase("STRANSPARENCY=")) { Transparency = Convert.ToInt16(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("STRANSPARENCYALL")) { TransparencyAll = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SQUICKMERGE")) { QuickMerge = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SWINTOOLKITDISM")) { WinToolkitDISM = GetValue(S); }
                    if (S.StartsWithIgnoreCase("SDELETEMOUNT")) { DeleteMount = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SREBUILDISOMAKER")) { RebuildISOMaker = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SWINTOOLKITEXT")) { WinToolkitExt = GetValue(S); }
                    if (S.StartsWithIgnoreCase("SWINTOOLKITPRI")) { WinToolkitPri = GetValue(S); }

                    if (S.StartsWithIgnoreCase("SWINTOOLKITTEMP"))
                    {
                        string sTemp = GetValue(S);
                        try
                        {
                            DriveInfo DI = new DriveInfo(sTemp);
                            if (!DI.IsReady) { continue; }
                            if (DI.DriveType != DriveType.Fixed) { continue; }
                            if (sTemp.Length > 3 && sTemp.Substring(1, 2).EqualsIgnoreCase(":\\") && !sTemp.ContainsForeignCharacters()) { WinToolkitTemp = sTemp; }
                        }
                        catch { }
                    }

                    if (S.StartsWithIgnoreCase("SMOUNTLOG")) { MountLog = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SREGISTRYLOG")) { RegistryLog = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SUPLOADLOGS")) { UploadLogs = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SSHOWWELCOME")) { ShowWelcome = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SDISMLOC")) { DISMLoc = GetValue(S); }
                    if (S.StartsWithIgnoreCase("SKNOWNUPDATES"))
                    {
                        string known = GetValue(S);
                        foreach (string s in known.Split(';'))
                            RememberSelectedUpdates.Add(s);
                    }

                    if (S.StartsWithIgnoreCase("SAUTOCLEAN")) { AutoClean = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SAUTOCLEANSXS")) { AutoCleanSXS = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SAUTOCLEANLOGS")) { AutoCleanLogs = Convert.ToBoolean(GetValue(S)); }
                    if (S.StartsWithIgnoreCase("SAUTOCLEANMANCACHE")) { AutoCleanManCache = Convert.ToBoolean(GetValue(S)); }
                }
                catch { }

            }
        }

        private static string GetValue(string Line)
        {
            string nLine = Line;
            while (nLine.ContainsIgnoreCase("="))
            {
                nLine = nLine.Substring(1);
            }
            return nLine;
        }
    }
}
