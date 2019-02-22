using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WinToolkit.Prompts;
using System.Diagnostics;
using System.Threading;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;

namespace WinToolkit
{
    public static class cMount
    {
        static ToolStripLabel lblP;

        public static string rLog = "";
        static string nStatus = "";
        static int nImage = 1;
        static bool bImage;

        static Form FP;
        public static string sErr = "";
        public static bool SupCan;
        public static MountStatus uChoice = MountStatus.None;

        public enum MountStatus
        {
            Save,
            Discard,
            Keep,
            None
        }

        public static string CWIM_MountImage(string ImgName, int ImgIndex, string WIMFile, string MountPath,
            ToolStripLabel lbl, Form F)
        {
            lblP = lbl;
            if (string.IsNullOrEmpty(cOptions.MountTemp))
            {
                cOptions.MountTemp = cOptions.WinToolkitTemp + "_Mount";
            }
            if (string.IsNullOrEmpty(MountPath))
            {
                MountPath = cOptions.MountTemp;
            }

          

        bool cMounted = false;
            try
            {
                cMain.UpdateToolStripLabel(lbl, "Taking Ownership...");
                Application.DoEvents();
                cMain.TakeOwnership(WIMFile);
                cMain.ClearAttributeFile(WIMFile);
            }
            catch { }

            cMain.UpdateToolStripLabel(lbl, "Working...");
            Application.DoEvents();
            cMain.FreeRAM();

            cMain.UpdateToolStripLabel(lbl, "Checking if WIM is mounted...");

            string MP = "", MF = "";
            try
            {
                if (cReg.RegCheckMounted("SOFTWARE\\Microsoft\\WIMMount\\Mounted Images\\"))
                {
                    foreach (var RK in cReg.RegCheckMountDism())
                    {
                        try
                        {
                            string WimM = RK.MountPath;
                            string WimF = RK.WimFile;

                            if (WimF == WIMFile) { MF = WimF; }
                            if (WimM == MountPath) { MP = WimM; }

                            if (MF == WIMFile && MP != MountPath) { MountPath += "_" + cMain.RandomName(1, 9999999); break; }
                            if (MF != WIMFile && MP == MountPath) { MountPath += "_" + cMain.RandomName(1, 9999999); break; }

                            if (MF == WIMFile && MP == MountPath) { cMounted = true; }

                        }
                        catch { }

                        try
                        {
                            if (Directory.Exists(MountPath))
                            {
                                string NP = MountPath + "_" + cMain.RandomName(1, 1000);
                                while (Directory.Exists(NP)) { NP = MountPath + "_" + cMain.RandomName(1, 1000); }

                                Directory.Move(MountPath, NP);
                                Directory.Move(NP, MountPath);
                                cMain.UpdateToolStripLabel(lbl, " Recovering: [" + ImgName + "]...");
                                nStatus = " Recovering: [" + ImgName + "]...";
                                Application.DoEvents();
                                cMain.OpenProgram("\"" + cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe") + "\"", "/remount \"" + MountPath + "\"", true, ProcessWindowStyle.Hidden);

                                if (MF == WIMFile && MP == MountPath) { cMounted = true; }
                            }
                        }
                        catch { cMounted = true; }
                        if (MF == WIMFile && MP == MountPath && cMounted) { return MountPath; }
                    }
                }
            }
            catch
            {
                // ignored
            }

            cMain.UpdateToolStripLabel(lbl, "Checked (" + MountPath + ")...");

            Application.DoEvents();

            if (cMounted == false)
            {
                if (WIMFile.StartsWithIgnoreCase(MountPath.ToUpper() + "\\"))
                {
                    MessageBox.Show("Your mount path seems to be the same path as your selected image DVD, this is not recommend and Win Toolkit will abort!\nWIM FIle: [" + WIMFile + "]\nMount Path: [" + MountPath + "\\]", "Access Denied");
                    return "";
                }

                if (Directory.Exists(MountPath))
                {
                    int fCount = 0;
                    try
                    {
                        if (Directory.GetFiles(MountPath, "*", SearchOption.AllDirectories).Any())
                        {
                            fCount += 1;
                        }

                        if (Directory.GetDirectories(MountPath, "*", SearchOption.AllDirectories).Any())
                        {
                            fCount += 1; ;
                        }
                    }
                    catch (Exception) { }
                    if (fCount > 0) { MountPath += "\\Mount_" + cMain.RandomName(1, 999999); }
                }

                string dCheck = MountPath;

                while (dCheck.EndsWithIgnoreCase("\\")) { dCheck = dCheck.Substring(0, dCheck.Length - 1); }

                if (dCheck.ToUpper().EndsWithIgnoreCase(cMain.Desktop.ToUpper())) { MountPath += "\\Mount_" + cMain.RandomName(1, 999999); }

                cMain.UpdateToolStripLabel(lbl, "Removing previous mount folder....");
                Application.DoEvents();
                Files.DeleteFolder(MountPath, true);

                cMain.FreeRAM();

                cMain.UpdateToolStripLabel(lbl, " Mounting: [" + ImgName + "]...");
                nStatus = " Mounting: [" + ImgName + "]...";
                Application.DoEvents();

                string A = "/mountrw \"" + WIMFile + "\" " + ImgIndex + " \"" + MountPath + "\"";
                if (ImgIndex == 0)
                {
                    A = "/mountrw \"" + WIMFile + "\" \"" + ImgName + "\" \"" + MountPath + "\"";
                }

                if (!Directory.Exists(MountPath)) { cMain.CreateDirectory(MountPath); }

                mImagex(A, F, ImgName, WIMFile, MountPath);

                Files.DeleteFile(MountPath + "\\Windows\\WinToolkit.txt");
                cMain.FreeRAM();
            }

            if (cReg.RegCheckMounted("SOFTWARE\\Microsoft\\WIMMount\\Mounted Images\\"))
            {

                if (cReg.RegCheckMountDism().Any(RK => String.Equals(RK.MountPath, MountPath, StringComparison.CurrentCultureIgnoreCase) && RK.State == 2))
                {
                    return MountPath;
                }
            }

            cMain.ErrorBox("Win Toolkit was unable to mount an image!", "Unknown Mount Error", rLog);

            return "";
        }

        private static void mImagex(string Argument, Form F, string ImageName, string WimFile, string MountPath)
        {

            bool deleteTempImmediately = true;
            string sImagexTemp = cOptions.WinToolkitTemp + "\\MountTemp_" + (ImageName + WimFile).CreateMD5();
            Files.DeleteFolder(sImagexTemp, true);

            rLog = "Starting" + Environment.NewLine;
            string ErrDesc = "Error working with imagex";
            if (Argument.ContainsIgnoreCase("/mount")) { ErrDesc = "Error trying to mount image"; deleteTempImmediately = false; }
            if (Argument.ContainsIgnoreCase("/Unmount")) { ErrDesc = "Error trying to discard image"; deleteTempImmediately = cOptions.DeleteMount; }
            if (Argument.ContainsIgnoreCase("/Unmount") && Argument.ContainsIgnoreCase("/commit")) { ErrDesc = "Error trying to commit image"; deleteTempImmediately = cOptions.DeleteMount; }
            if (Argument.ContainsIgnoreCase("/export")) { ErrDesc = "Error trying to rebuild image"; }

            try
            {
                rLog += "Tool Name: " + F.Text + Environment.NewLine;
                rLog += "Image Name: " + ImageName + Environment.NewLine;
                rLog += "WIM File: " + WimFile + Environment.NewLine;
                rLog += "Mount Logging: " + cOptions.MountLog.ToString() + Environment.NewLine;


                try
                {
                    if (WimFile.Length > 0)
                    {
                        var DI = new DriveInfo(WimFile.Substring(0, 1));
                        rLog += "Drive Type: " + DI.DriveType.ToString() + Environment.NewLine;
                        rLog += "Free Space: " + cMain.BytesToString(DI.AvailableFreeSpace) + Environment.NewLine;
                        rLog += "IsReady: " + DI.IsReady + Environment.NewLine;
                    }
                    rLog += "File Exists: " + File.Exists(WimFile) + Environment.NewLine;
                    rLog += "File Size: " + cMain.GetSize(WimFile) + Environment.NewLine;

                    if (Directory.Exists(MountPath))
                    {
                        rLog += "Directory Exists: True" + Environment.NewLine;
                        rLog += "Files: " + Environment.NewLine;

                        rLog = Directory.GetFiles(MountPath, "*.*", SearchOption.TopDirectoryOnly).Aggregate(rLog, (current, xF) => current + (xF + " * " + Environment.NewLine));
                        rLog += Environment.NewLine + "Directories: " + Environment.NewLine;
                        rLog = Directory.GetDirectories(MountPath, "*.*", SearchOption.TopDirectoryOnly).Aggregate(rLog, (current, xF) => current + (" *" + xF + Environment.NewLine));
                    }
                    else
                    {
                        rLog += "Directory Exists: False" + Environment.NewLine;
                    }

                }
                catch (Exception Ex) { rLog += "No Extra Details\n[" + WimFile + "]" + Environment.NewLine; }
            }
            catch (Exception Ex) { rLog += "No Getting details\n[" + WimFile + "]" + Environment.NewLine; }


            try
            {
                cMain.FreeRAM();

                cMain.WriteResource(Properties.Resources.imagex, cMain.UserTempPath + "\\Files\\Imagex.exe", null);
                using (var p = new Process())
                {

                    p.StartInfo.FileName = "\"" + cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe") + "\"";

                    if (!p.StartInfo.FileName.EndsWithIgnoreCase("\"")) { p.StartInfo.FileName += "\""; }
                    if (!p.StartInfo.FileName.StartsWithIgnoreCase("\"")) { p.StartInfo.FileName = "\"" + p.StartInfo.FileName; }

                    p.StartInfo.Arguments = Argument;

                    if (cOptions.MountLog && !string.IsNullOrEmpty(ImageName))
                    {
                        string sMountLogs = cMain.Root + "Logs\\Mount_Logs";
                        if (!Directory.Exists(sMountLogs)) { cMain.CreateDirectory(sMountLogs); }
                        Argument += " /logfile \"" + sMountLogs + "\\" + ImageName.ReplaceIgnoreCase(" ", "_") + "_" + cMain.RandomName(1, 90000) + ".log\"";
                        rLog += "Logging: " + "\"" + sMountLogs + "\\" + ImageName.ReplaceIgnoreCase(" ", "_") + "_" + cMain.RandomName(1, 90000) + ".log\"" + Environment.NewLine;
                    }


                    rLog += "TempDir (" + Directory.Exists(sImagexTemp) + "): " + sImagexTemp + Environment.NewLine;
                    rLog += "FileName: \"" + cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe") + "\"" + Environment.NewLine;
                    rLog += "Argument: " + Argument + Environment.NewLine;

                    p.StartInfo.Arguments = Argument;

                    sErr = "";

                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    FP = F;

                    cMain.eForm = F;
                    rLog += Environment.NewLine + "Setting taskbar Progress" + Environment.NewLine;
                    if (!FP.IsDisposed && FP != null)
                    {
                        rLog += "Setting taskbar Colour" + Environment.NewLine;
                        Windows7Taskbar.SetProgressState(FP.Handle,
                                                                                  uChoice == MountStatus.Discard
                                                                                         ? ThumbnailProgressState.Error
                                                                                         : ThumbnailProgressState.Paused);
                    }

                    p.OutputDataReceived += OnDataReceived;
                    rLog += "Dumping Registry" + Environment.NewLine;
                    cReg.RegUnLoadAll();
                    rLog += "Starting" + Environment.NewLine;

                    p.StartInfo.Arguments += " /Temp \"" + sImagexTemp + "\"";

                    if (F.InvokeRequired)
                    {
                        p.Start();
                        rLog += "Started [Background Thread]" + Environment.NewLine;
                        p.BeginOutputReadLine();

                        p.WaitForExit();
                    }
                    else
                    {
                        Thread t = new Thread(() =>
                        {
                            p.Start();
                            rLog += "Started [New Thread]" + Environment.NewLine;
                            p.BeginOutputReadLine();

                            p.WaitForExit();
                        });

                        t.Start();

                        while (t.IsAlive)
                        {
                            Thread.Sleep(100);
                            Application.DoEvents();
                        }
                    }

                    rLog += "Closing #0" + Environment.NewLine;

                    p.CancelOutputRead();

                    rLog += "Closing #1" + Environment.NewLine;
                    cMain.AppErrC = p.ExitCode;
                    rLog += "Closing #2" + Environment.NewLine;
                }
                rLog += "Finished" + Environment.NewLine;

            }
            catch (Exception Ex)
            {
                rLog += "Detecting Imagex: " + Environment.NewLine;
                if (File.Exists(cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe")))
                {
                    new SmallError("Imagex is available.", Ex, "Imagex available ('" + cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe") + "')\n" + Ex.Message + "\n\nsErr: " + sErr + "\n\n" + rLog).Upload();
                }
                else
                {
                    new SmallError("Imagex is missing.", Ex, "Imagex available ('" + cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe") + "')\n" + Ex.Message + "\n\nsErr: " + sErr + "\n\n" + rLog).Upload();
                }
            }

            try
            {
                if (!string.IsNullOrEmpty(sErr) && !sErr.ContainsIgnoreCase("%") && !sErr.EndsWithIgnoreCase("..." + Environment.NewLine))
                {
                    rLog += "sErr: " + sErr;
                    rLog += "\n\nDetecting cause of error #1" + Environment.NewLine;
                    string ssErr = sErr.Split(Environment.NewLine.ToCharArray()).Where(S => !S.ContainsIgnoreCase("%") && !string.IsNullOrEmpty(S)).Aggregate("", (current, S) => current + (S + Environment.NewLine));
                    rLog += "Detecting cause of error #2" + Environment.NewLine;
                    try
                    {
                        var DI = new DriveInfo(MountPath.Substring(0, 1));
                        sErr += Environment.NewLine + DI.DriveType + Environment.NewLine + DI.Name + Environment.NewLine + cMain.BytesToString(DI.AvailableFreeSpace);
                    }
                    catch (Exception Ex) { rLog += "Detecting cause of error :: Get drive info" + Environment.NewLine; }
                    rLog += "Detecting cause of error #3" + Environment.NewLine;
                    bool showed = false;

                    if (sErr.ContainsIgnoreCase("RT_Mount") || sErr.ContainsIgnoreCase("RT_Mount_Boot"))
                    {
                        cMain.ErrorBox("This image could not be unmounted because RT7Lite unmounted it first!", ErrDesc, ImageName + Environment.NewLine + Environment.NewLine + rLog + Environment.NewLine + Environment.NewLine + sErr);
                        showed = true;
                    }

                    if (sErr.ContainsIgnoreCase("WIMMountImageHandle:(832):") || sErr.ContainsIgnoreCase("reparse points"))
                    {
                        cMain.ErrorBox("You need to make sure the drive you are mounting your image to is NTFS!", ErrDesc, ImageName + Environment.NewLine + Environment.NewLine + rLog + Environment.NewLine + Environment.NewLine + sErr);
                        showed = true;
                    }

                    if (sErr.ContainsIgnoreCase("WIMMountImageHandle:(782):") || sErr.ContainsIgnoreCase("already mounted"))
                    {
                        cMain.ErrorBox("'" + ImageName + "' has already been mounted! This usually happens when your are using another tool at the same time as Win Toolkit!" + Environment.NewLine + Environment.NewLine + "[" + MountPath + "]", ErrDesc, ImageName + Environment.NewLine + Environment.NewLine + rLog + Environment.NewLine + Environment.NewLine + sErr);
                        showed = true;
                    }
                    rLog += "Detecting cause of error #4" + Environment.NewLine;
                    if (showed == false && !WimFile.ToUpper().EndsWithIgnoreCase("BOOT.WIM"))
                    {
                        cMain.ErrorBox(ErrDesc, ImageName, rLog + Environment.NewLine + Environment.NewLine + sErr);
                        rLog += "Uploading Log" + Environment.NewLine;
                        cMain.WriteLog(F, ErrDesc, sErr, rLog);
                    }
                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(F, "Error after rebuild", Ex.Message, rLog);
            }
            if (!F.IsDisposed && F != null) { Windows7Taskbar.SetProgressState(F.Handle, ThumbnailProgressState.NoProgress); }

            if (deleteTempImmediately)
            {
                Files.DeleteFolder(sImagexTemp, false);
            }
        }

        private static void OnDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            try
            {
                if (FP.IsDisposed || e == null || e.Data == null || FP == null || string.IsNullOrEmpty(e.Data)) { return; }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(FP, "could not detect data", Ex.Message, cMain.eLBL.Text);
            }

            if (e != null && (!string.IsNullOrEmpty(e.Data) && !rLog.EndsWithIgnoreCase(e.Data + Environment.NewLine))) { rLog += "Output: " + e.Data + Environment.NewLine; }

            try
            {
                if (e != null && !string.IsNullOrEmpty(e.Data))
                {
                    sErr += e.Data + Environment.NewLine;
                    if (e.Data.ContainsIgnoreCase("%"))
                    {
                        if (!rLog.EndsWithIgnoreCase("Output #1: " + Environment.NewLine)) { rLog += "Output #1: " + e.Data + Environment.NewLine; }
                        string T = e.Data;
                        string PT = "";
                        ulong CP = 0;
                        if (T.ContainsIgnoreCase("%"))
                        {
                            string P = T;

                            while (P.ContainsIgnoreCase("%"))
                            {
                                P = P.Substring(0, P.Length - 1);
                            }

                            while (P.ContainsIgnoreCase(" "))
                            {
                                P = P.Substring(1);
                            }
                            CP = Convert.ToUInt16(P);

                            if (bImage && CP < 10) { nImage += 1; bImage = false; }
                            if (CP > 90) { bImage = true; }
                            if (nStatus.ContainsIgnoreCase("Saving Image ["))
                            {
                                if (nImage == 1)
                                {
                                    CP = (ulong)(((double)CP / 200) * 100);
                                }
                                else
                                {
                                    CP += 100;
                                    CP = (ulong)(((double)CP / 200) * 100);
                                }
                            }

                            {
                            }
                        }
                        if (T.ContainsIgnoreCase("]"))
                        {
                            if (!rLog.EndsWithIgnoreCase("Output #2: " + Environment.NewLine)) { rLog += "Output #2: " + T + Environment.NewLine; }
                            while (!T.EndsWithIgnoreCase("]"))
                            {
                                T = T.Substring(0, T.Length - 1);
                            }
                            if (!rLog.EndsWithIgnoreCase("Output #3: " + Environment.NewLine)) { rLog += "Output #3: " + T + Environment.NewLine; }
                            if (e.Data.ContainsIgnoreCase(":"))
                            {
                                PT = e.Data;
                                while (!PT.StartsWithIgnoreCase(":"))
                                {
                                    PT = PT.Substring(1);
                                }

                                //   T = cMain.ReplaceText(T, "]", "");
                                PT = PT.Substring(1) + "-";
                            }
                            if (!rLog.EndsWithIgnoreCase("Output #4: " + Environment.NewLine)) { rLog += "Output #4: " + PT + Environment.NewLine; }
                            if (lblP != null && !lblP.IsDisposed)
                            {
                                if (!rLog.EndsWithIgnoreCase("Writing Status" + Environment.NewLine)) { rLog += "Writing Status T: " + T + " | PT: " + PT + Environment.NewLine; }

                                if (nStatus.ContainsIgnoreCase(": Rebuilding WIM"))
                                {
                                    cMain.UpdateToolStripLabel(lblP, T + PT + " " + nStatus + " #" + nImage.ToString(CultureInfo.InvariantCulture));
                                }
                                else
                                {
                                    if (nStatus.ContainsIgnoreCase("Saving Image ["))
                                    {
                                        if (nImage == 1)
                                        {
                                            cMain.UpdateToolStripLabel(lblP, "[ " + String.Format("{0,3}", CP) + "% ]" + PT + nStatus);
                                        }
                                        else
                                        {
                                            cMain.UpdateToolStripLabel(lblP, "[ " + String.Format("{0,3}", CP) + "% ]" + PT + nStatus.ReplaceIgnoreCase("Saving Image", "Unmounting Image") + ": Saved");
                                        }
                                    }
                                    else
                                    {
                                        cMain.UpdateToolStripLabel(lblP, T + PT + " " + nStatus);
                                    }
                                    // Saving Image

                                }

                                if (!rLog.EndsWithIgnoreCase("Done Status" + Environment.NewLine)) { rLog += "Done Status" + Environment.NewLine; }
                            }
                        }

                        if (FP != null && T.Length > 0)
                        {
                            if (CP < 100 && FP != null && !FP.IsDisposed)
                            {
                                if (!rLog.EndsWithIgnoreCase("Writing Taskbar" + Environment.NewLine)) { rLog += "Writing Taskbar" + Environment.NewLine; }
                                Windows7Taskbar.SetProgressValue(FP.Handle, CP, 100);
                                if (!rLog.EndsWithIgnoreCase("Done Taskbar" + Environment.NewLine)) { rLog += "Done Taskbar" + Environment.NewLine; }

                            }
                        }
                        Application.DoEvents();
                    }
                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(null, "Error with Rebuilding", Ex.Message, e.Data);
            }

        }

        private static void CloseExplorer()
        {
            try
            {
                SHDocVw.ShellWindows shellWindows = new SHDocVw.ShellWindowsClass();

                foreach (SHDocVw.InternetExplorer ie in shellWindows)
                {
                    try
                    {
                        string filename; filename = System.IO.Path.GetFileNameWithoutExtension(ie.FullName).ToLower();
                        if (filename.Equals("explorer"))
                        {
                            string sFolder = ie.LocationURL.Substring(8).ReplaceIgnoreCase("/", "\\");
                            if (sFolder.StartsWithIgnoreCase(cMain.selectedImages[0].MountPath.ToUpper()))
                            {
                                ie.Quit();
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                    }
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(shellWindows);
                shellWindows = null;
            }
            catch (Exception Ex)
            {
            }
        }

        public static bool CheckUnmounted(string MountPath)
        {
            bool Unmounted = true;
            if (Directory.Exists(MountPath))
            {
                try
                {
                    foreach (string FO in Directory.GetFiles(MountPath, "*", SearchOption.AllDirectories))
                    {
                        Unmounted = false;
                        break;
                    }
                }
                catch { }

                try
                {
                    foreach (string FO in Directory.GetDirectories(MountPath, "*", SearchOption.AllDirectories))
                    {
                        Unmounted = false;
                        break;
                    }
                }
                catch { }
            }
            return Unmounted;
        }

        //public static bool CWIM_UnmountImage(string ImageName, int ImgIndex, string MountPath, ToolStripLabel lbl, bool Force, bool Ask, bool SupportCancel, Form F, string WIMFile, bool SupRebuild)

        public static bool CWIM_UnmountImage(string ImageName, ToolStripLabel lbl, bool Force, bool Ask, bool SupportCancel, Form F, string WIMFile, bool SupRebuild)
        {

            bImage = false; nImage = 1;
            uChoice = MountStatus.Save;
            string MountPath = "N/A";
            if (cReg.RegCheckMounted("SOFTWARE\\Microsoft\\WIMMount\\Mounted Images\\"))
            {
                foreach (var RK in cReg.RegCheckMountDism())
                {
                    if (String.Equals(RK.WimFile, WIMFile, StringComparison.CurrentCultureIgnoreCase)) { MountPath = RK.MountPath; }
                }
            }


            if (string.IsNullOrEmpty(MountPath) || MountPath.EqualsIgnoreCase("N/A")) { return true; }



 cMain.UpdateToolStripLabel(lbl, "Taking Ownership...");
            Application.DoEvents();
            cMain.TakeOwnership(WIMFile);
            cMain.ClearAttributeFile(WIMFile);


            if (cMount.uChoice != MountStatus.Keep && cMount.uChoice != MountStatus.None)
            {
                cMain.UpdateToolStripLabel(lbl, "Closing WIM editing tools...");
                Application.DoEvents();

                cMain.KillProcess("vLite");
                cMain.KillProcess("RTWin7Lite");
                cMain.KillProcess("regedit");
                cMain.KillProcess("dism");
                cMain.KillProcess("pkgmgr");
                cMain.KillProcess("dismhost");
                cMain.KillProcess("imagex");
            }


            if (Directory.Exists(MountPath + "\\Windows\\System32"))
            {

                if (File.Exists(MountPath + "\\Windows\\System32\\UTH.exe"))
                {
                    File.Copy(MountPath + "\\Windows\\System32\\UTH.exe", MountPath + "\\Windows\\UTH.exe", true);
                    Files.DeleteFile(MountPath + "\\Windows\\System32\\UTH.exe");
                }

                InstallRunOnce(lbl, MountPath);
            }


            cMain.UpdateToolStripLabel(lbl, "Continuing...");
            cMain.wimRebuild = false;

            lblP = lbl;
            string Olbl = lbl.Text;

            cMain.FreeRAM();
            cReg.RegUnLoadAll();


            Application.DoEvents();

            while (Olbl.ContainsIgnoreCase(":: ") && !Olbl.EndsWithIgnoreCase("::")) { Olbl = Olbl.Substring(0, Olbl.Length - 1); }

            SupCan = SupportCancel;
            if (Force == false)
            {
                if (Ask || cMain.selectedImages.Count < 1)
                {
                    cMain.UpdateToolStripLabel(lbl, "Loading Unmount Screen...");

                    uChoice = MountStatus.None;
                    var TS = new frmUnmount(MountPath, ImageName);
                    TS.cmdSaveRebuild.Enabled = SupRebuild;

                    cMain.UpdateToolStripLabel(lbl, "Waiting for user input...");
                    TS.ShowDialog(F);
                    cMain.UpdateToolStripLabel(lbl, "Continuing...");
                }
            }
            else
            {
                uChoice = MountStatus.Discard;
            }

            if (uChoice != MountStatus.Discard)
            {
                if (Debugger.IsAttached)
                {

                    if (cOptions.AutoClean)
                    {
                        cMain.UpdateToolStripLabel(lbl, "Cleaning (" + MountPath + ")...");
                        Files.DeleteFolder(MountPath + @"\Users\Public\Music\Sample Music", false);
                        Files.DeleteFolder(MountPath + @"\$Recycle.Bin", false);
                        Files.DeleteFolder(MountPath + @"\Users\Public\Pictures\Sample Pictures", false);
                        Files.DeleteFolder(MountPath + @"\Users\Public\Recorded TV\Sample Media", false);
                        Files.DeleteFolder(MountPath + @"\Users\Public\Videos\Sample Videos", false);
                        Files.DeleteFolder(MountPath + @"\Users\Administrator", false);

                        Files.DeleteFolder(MountPath + @"\Users\Temp", false);
                        Files.DeleteFolder(MountPath + @"\Windows\Temp", false);
                    }
                    if (cOptions.AutoCleanLogs)
                    {
                        cMain.UpdateToolStripLabel(lbl, "Cleaning Logs (" + MountPath + ")...");
                        Files.DeleteFolder(MountPath + @"\PerfLogs", false);
                        Files.DeleteFolder(MountPath + @"\Windows\Logs\PBR", false);
                        Files.DeleteFolder(MountPath + @"\Windows\System32\winevt\Logs", false);
                        Files.DeleteFolder(MountPath + @"\Windows\Logs\CBS", false);

                        Files.DeleteFile(@"\ProgramData\Microsoft\Windows Defender\Support\MPLog-07132009-215552.log");
                        Files.DeleteFile(@"\Windows\DtcInstall.log");
                        Files.DeleteFile(@"\Windows\PFRO.log");
                        Files.DeleteFile(@"\Windows\setupact.log");
                        Files.DeleteFile(@"\Windows\setuperr.log");
                        Files.DeleteFile(@"\Windows\WindowsUpdate.log");
                        Files.DeleteFile(@"\Windows\inf\setupapi.app.log");
                        Files.DeleteFile(@"\Windows\inf\setupapi.offline.log");
                        Files.DeleteFile(@"\Windows\Microsoft.NET\Framework\v2.0.50727\ngen.log");
                        Files.DeleteFile(@"\Windows\Microsoft.NET\Framework\v2.0.50727\ngen_service.log");
                        Files.DeleteFile(@"\Windows\Performance\WinSAT\winsat.log");
                        Files.DeleteFile(@"\Windows\security\logs\scesetup.log");
                        Files.DeleteFile(
                            @"\Windows\ServiceProfiles\LocalService\AppData\Local\Microsoft\Windows\WindowsUpdate.log");
                        Files.DeleteFile(@"\Windows\System32\LocalGroupAdminAdd.log");
                        Files.DeleteFile(@"\Windows\System32\Local_LLU.log");
                        Files.DeleteFile(@"\Windows\System32\Network_LLU.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb00326.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb00327.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb00328.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb00329.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb0032A.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb0032B.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb0032C.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb0032D.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb0032E.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb0032F.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb00330.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb00331.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb00332.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb00333.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb00334.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb00335.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb00336.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb00337.log");
                        Files.DeleteFile(@"\Windows\System32\catroot2\edb00338.log");
                    }

                    if (cOptions.AutoCleanSXS)
                    {
                        cMain.UpdateToolStripLabel(lbl, "Cleaning WinSXS Backup (" + MountPath + ")...");
                        Files.DeleteFolder(MountPath + @"\Windows\WinSxS\Backup", false);
                    }

                    if (cOptions.AutoCleanManCache)
                    {
                        cMain.UpdateToolStripLabel(lbl, "Cleaning Manifest Cache (" + MountPath + ")...");
                        Files.DeleteFolder(MountPath + @"\Windows\WinSxS\ManifestCache", false);
                    }

                }
            }


            if (uChoice == MountStatus.Keep || uChoice == MountStatus.None)
            {
                return false;
            }


            if (uChoice != MountStatus.Keep && uChoice != MountStatus.None)
            {
                cMain.UpdateToolStripLabel(lbl, "Closing Explorer Windows...");
                Application.DoEvents();
                CloseExplorer();
            }

            switch (uChoice)
            {
                case MountStatus.Save:
                    cMain.UpdateToolStripLabel(lbl, " Saving Image [" + ImageName + "]...");
                    nStatus = " Saving Image [" + ImageName + "]";
                    Application.DoEvents();

                    mImagex("/Unmount \"" + MountPath + "\" /commit", F, ImageName, WIMFile, MountPath);
                    break;
                case MountStatus.Discard:

                    cMain.UpdateToolStripLabel(lbl, " Discarding Image [" + ImageName + "]...");
                    nStatus = " Discarding Image [" + ImageName + "]...";
                    Application.DoEvents();

                    mImagex("/Unmount \"" + MountPath + "\"", F, ImageName, WIMFile, MountPath);
                    break;
            }

            bool Unmounted = CheckUnmounted(MountPath);

            if (Unmounted == false && (uChoice == MountStatus.Discard || uChoice == MountStatus.Save))
            {
                if (Directory.Exists(MountPath))
                {
                    cMain.UpdateToolStripLabel(lbl, "Cleaning (This may take a while)..."); Application.DoEvents();
                    string sCleanup = cMain.RunExternal("\"" + cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe") + "\"", "/Cleanup");

                    if (sCleanup.ContainsIgnoreCase("100%"))
                    {
                        Files.DeleteFolder(MountPath, false);
                    }
                    Unmounted = !Directory.Exists(MountPath);
                }

                if (Unmounted == false && Directory.Exists(MountPath))
                {
                    cMain.UpdateToolStripLabel(lbl, " Logging error...");
                    Application.DoEvents();

                    LargeError LE = new LargeError("Unmounting Image Error", "Win Toolkit could not unmount the image properly because some files and/or folders are in use, please close these files.", "MountPath: " + MountPath);
                    LE.Upload();
                    LE.ShowDialog();

                }
            }
            cMain.FreeRAM();

            if (Unmounted && (uChoice == MountStatus.Save || uChoice == MountStatus.Discard))
            {
                cMain.UpdateToolStripLabel(lbl, "Removing mount folder...");
                Application.DoEvents();
                Files.DeleteFolder(MountPath, !cOptions.DeleteMount);
                string sImagexTemp = cOptions.WinToolkitTemp + "\\ImagexTemp_" + (WIMFile + MountPath).CreateMD5();
                Files.DeleteFolder(sImagexTemp, false);
            }

            if (Unmounted && cMain.wimRebuild && Ask && cMain.selectedImages.Count > 0)
            {
                Rebuild(cMain.selectedImages[0].Location, lbl, F, false);
                cMain.FreeRAM();
            }

            return Unmounted;
        }

      

        private static void InstallRunOnce(ToolStripLabel lbl, string MountPath)
        {
            cMain.UpdateToolStripLabel(lbl, "Installing RunOnce Module...");

            string RO = MountPath + "\\Windows\\System32\\WinToolkitRunOnce.exe";
            bool InstallRequired = false;

            if (!File.Exists(RO))
            {
                if (!cOptions.AddRunOnce) { return; }
                InstallRequired = true;
            }

            if (InstallRequired)
            {
                bool WIM_Software = cReg.RegLoad("WIM_Software", MountPath + "\\Windows\\System32\\Config\\SOFTWARE");
                if (WIM_Software)
                {
                    cReg.WriteValue(Microsoft.Win32.Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", "1WinToolkit", "WinToolkitRunOnce.exe");
                    cReg.RegUnLoad("WIM_Software");
                }
            }

            cMain.UpdateToolStripLabel(lbl, "Updating RunOnce Module...");

            Files.DeleteFile(MountPath + "\\Windows\\System32\\WinToolkitRunOnce.exe");
            Files.DeleteFile(MountPath + "\\Windows\\System32\\WinToolkitRunOnce.exe.config");

            cMain.WriteResource(Properties.Resources.RunOnce, MountPath + "\\Windows\\System32\\WinToolkitRunOnce.exe", null);
            cMain.WriteResource(Properties.Resources.RunOnce_exe, MountPath + "\\Windows\\System32\\WinToolkitRunOnce.exe.config", null);
        }

        public static void Rebuild(string WimFile, ToolStripLabel lbl, Form F, bool ShowErr)
        {

            try
            {
                if (!File.Exists(WimFile))
                {
                    MessageBox.Show("Win Toolkit can't find the following file:\n\n" + WimFile, "Aborted - File Missing");
                    return;
                }

                if (!WimFile.StartsWithIgnoreCase("\\\\"))
                {
                    cMain.UpdateToolStripLabel(lbl, "Checking Directory...");
                    Application.DoEvents();
                    var DI = new DriveInfo(WimFile.Substring(0, 1));
                    if (cMain.BytesToString(DI.AvailableFreeSpace, false).EqualsIgnoreCase("0"))
                    {
                        MessageBox.Show("There is no space to rebuild this image! The image you are using may be located on a CD or DVD.", "No room");
                        return;
                    }
                }
            }
            catch (Exception Ex) { cMain.WriteLog(F, "Error checking directory", Ex.Message, WimFile + Environment.NewLine + "Label: " + lbl.Text); }

            try
            {
                cMain.UpdateToolStripLabel(lbl, "Taking Ownership...");
                Application.DoEvents();
                cMain.TakeOwnership(WimFile);
                cMain.ClearAttributeFile(WimFile);
            }
            catch { }

            lblP = lbl;

            cMain.UpdateToolStripLabel(lbl, "Rebuilding...");
            Application.DoEvents();
            try
            {
                Files.DeleteFile(WimFile + ".bak");

                nStatus = ": Rebuilding WIM";
                nImage = 1; bImage = false;
                string C = "";
                if (cOptions.mCheck)
                {
                    C = " /CHECK";
                }

                mImagex("/export \"" + WimFile + "\" * \"" + WimFile + ".bak\" /compress maximum" + C, F, null, WimFile, WimFile);

                cMain.UpdateToolStripLabel(lbl, "Almost Done...");

                if (cMain.AppErrC == 0 && File.Exists(WimFile + ".bak"))
                {
                    cMain.UpdateToolStripLabel(lbl, "Deleting backup...");

                    Files.DeleteFile(WimFile);
                    cMain.UpdateToolStripLabel(lbl, "Moving new wim...");

                    if (!File.Exists(WimFile)) { File.Move(WimFile + ".bak", WimFile); }
                }
                else
                {
                    Files.DeleteFile(WimFile + ".bak");
                }

            }
            catch (Exception Ex)
            {
                Files.DeleteFile(WimFile + ".bak");
                cMain.WriteLog(F, "Error in Rebuild void", Ex.Message, WimFile + Environment.NewLine + "Label: " + lbl.Text);
            }
            cMain.FreeRAM();
        }

        public static string SourceToFolder(string WimFile)
        {
            string t = WimFile;
            while (!t.EndsWithIgnoreCase("\\"))
            {
                t = t.Substring(0, t.Length - 1);
            }
            t = t.Substring(0, t.Length - 1);
            while (!t.EndsWithIgnoreCase("\\"))
            {
                t = t.Substring(0, t.Length - 1);
            }
            cMain.FreeRAM();
            return t;
        }

        public static string CWIM_GetWimInfo(string WimFile)
        {
            return cMain.RunExternal("\"" + cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe") + "\"", "/Info \"" + WimFile + "\"");
        }

        public static string CWIM_GetWimImageInfo(string WimFile, ToolStripLabel lbl)
        {
            if (cMain.IsFileReadLocked(WimFile))
            {
                string temp = lbl.Text;
                cMain.UpdateToolStripLabel(lbl, "File in use: [" + Path.GetFileName(WimFile) + "] Waiting...");

                while (cMain.IsFileLocked(WimFile))
                {
                    Thread.Sleep(100);
                }

                cMain.UpdateToolStripLabel(lbl, temp);
            }

            IntPtr lHandle = WimApi.WIMCreateFile(WimFile, WimApi.WIM_GENERIC_READ, WimApi.WIM_OPEN_EXISTING, WimApi.WIM_FLAG_SHARE_WRITE, WimApi.WIM_COMPRESS_NONE, 0);
            IntPtr info, sizeOfInfo;

            bool status = WimApi.WimGetImageInformation(lHandle, out info, out sizeOfInfo);

            if (status == false)
            {
                string errorMessage = new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error()).Message;
                new SmallError("WIM Image Info Error", null, "WIMFile: " + WimFile + Environment.NewLine + "Error: " + errorMessage).Upload();
                MessageBox.Show(errorMessage, "Error getting wim info");
            }

            WimApi.WIMCloseHandle(lHandle);
            cMain.FreeRAM();

            return Marshal.PtrToStringUni(info);
        }

        public static void CWIM_DeleteWIM(string WimFile, int Index)
        {
            IntPtr lHandle = WimApi.WIMCreateFile(WimFile, WimApi.WIM_GENERIC_WRITE, WimApi.WIM_OPEN_EXISTING, WimApi.WIM_FLAG_SHARE_WRITE, WimApi.WIM_COMPRESS_NONE, 0);
            Files.DeleteFolder(cMain.UserTempPath + "\\WIMGAPI", true);
            WimApi.WimSetTemporaryPath(lHandle, cMain.UserTempPath + "\\WIMGAPI");
            bool status = WimApi.WIMDeleteImage(lHandle, Index);

            if (status == false)
            {
                string errorMessage = new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error()).Message;
                MessageBox.Show(errorMessage, "Error deleting image.");
            }
            Files.DeleteFolder(cMain.UserTempPath + "\\WIMGAPI", false);
            WimApi.WIMCloseHandle(lHandle);
        }

        public static void CWIM_SetWimInfo(string WimFile, string imageInfo)
        {
            string S = imageInfo.ReplaceIgnoreCase(Environment.NewLine, "");
            while (S.ContainsIgnoreCase("> "))
            {
                S = S.ReplaceIgnoreCase("> ", ">");
            }

            IntPtr handle = WimApi.WIMCreateFile(WimFile, WimApi.WIM_GENERIC_WRITE, WimApi.WIM_OPEN_EXISTING, WimApi.WIM_FLAG_SHARE_WRITE, WimApi.WIM_COMPRESS_NONE, 0);
            Files.DeleteFolder(cMain.UserTempPath + "\\WIMGAPI", true);
            WimApi.WimSetTemporaryPath(handle, cMain.UserTempPath + "\\WIMGAPI");
            byte[] byteBuffer = Encoding.Unicode.GetBytes(S);
            int byteBufferSize = byteBuffer.Length;
            IntPtr xmlBuffer = Marshal.AllocHGlobal(byteBufferSize);
            Marshal.Copy(byteBuffer, 0, xmlBuffer, byteBufferSize);

            bool status = WimApi.WimSetImageInformation(handle, xmlBuffer, (uint)byteBufferSize);
            if (status == false)
            {
                string errorMessage = new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error()).Message;
                var LE = new LargeError("WIM Info", "Error setting WIM info.", errorMessage + "\r\nFilePath: " + WimFile + "\r\nWimInfo:\r\n" + imageInfo);
                LE.Upload(); LE.ShowDialog();
            }

            WimApi.WIMCloseHandle(handle);
            Files.DeleteFolder(cMain.UserTempPath + "\\WIMGAPI", false);
            cMain.FreeRAM();
        }

        public static string RenameImage(string Data, string Rename, int Index, string currentValue, string newValue)
        {
            string nData = "";
            try
            {
                foreach (string D in System.Text.RegularExpressions.Regex.Split(Data, "<IMAGE INDEX="))
                {
                    string DE = D;
                    if (DE.StartsWithIgnoreCase("\"")) { DE = "<IMAGE INDEX=" + DE; }
                    if (DE.StartsWithIgnoreCase("<IMAGE INDEX=\"" + Index + "\">"))
                    {
                        if (DE.ContainsIgnoreCase("<" + Rename + ">"))
                        {
                            if (DE.ContainsIgnoreCase("<" + Rename + ">" + currentValue + "</" + Rename + ">"))
                            {
                                DE = DE.ReplaceIgnoreCase("<" + Rename + ">" + currentValue + "</" + Rename + ">", "<" + Rename + ">" + newValue + "</" + Rename + ">", false);
                            }

                            if (DE.ContainsIgnoreCase("<" + Rename + "></" + Rename + ">"))
                            {
                                DE = DE.ReplaceIgnoreCase("<" + Rename + "></" + Rename + ">", "<" + Rename + ">" + newValue + "</" + Rename + ">", false);
                            }
                        }
                        else
                        {
                            DE = DE.ReplaceIgnoreCase("<IMAGE INDEX=\"" + Index + "\">", "<IMAGE INDEX=\"" + Index + "\">\n    <" + Rename + ">" + newValue + "</" + Rename + ">", false);
                        }
                    }
                    nData += DE;
                }
            }
            catch
            {
                nData = Data;
            }
            return nData;
        }
    }
}
