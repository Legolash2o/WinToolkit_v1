using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Prompts;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;

namespace WinToolkit
{
    public partial class frmWIMManager : Form
    {
        private bool Selected;
        const int DVD_COUNT = 5;
        private List<Thread> threads = new List<Thread>();

        private string WIMM2 = "";
        private bool nAllow = true;
        private int nMerge = 1;
        private string nStatus = "";

        List<WIMImage> Images = new List<WIMImage>();
        List<string> DVDs = new List<string>();

        public frmWIMManager()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            FormClosing += frmWIMManager_FormClosing;
            FormClosed += frmWIMManager_FormClosed;
            Shown += frmWIMManager_Shown;
            BWMerge.RunWorkerCompleted += BWMerge_RunWorkerCompleted;
            BWR.RunWorkerCompleted += BWR_RunWorkerCompleted;
            BWRebuild.RunWorkerCompleted += BWRebuild_RunWorkerCompleted;
            CheckForIllegalCrossThreadCalls = false;
            Activated += cMain.FormActivated;
            Deactivate += cMain.FormActivated;
            MouseWheel += MouseScroll;
            KeyDown += KeyDownEvent;
        }
        private void KeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 && mnuBrowse.Visible)
            {
                cmdRefresh.PerformClick(); return;
            }

            if ((e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter) && cmdS.Visible)
            {
                cmdSelect.PerformClick(); return;
            }

            if (e.KeyCode == Keys.Delete && cmdDeleteImage.Visible)
            {
                cmdDeleteImage.PerformClick(); return;
            }

            if (e.Control && e.KeyCode == Keys.W && mnuBrowse.Visible)
            {
                cmdBrowseWIM.PerformClick(); return;
            }

            if (e.Control && e.KeyCode == Keys.I && mnuBrowse.Visible)
            {
                cmdBrowseISO.PerformClick(); return;
            }

            if (e.Control && e.KeyCode == Keys.O && mnuBrowse.Visible)
            {
                cmdBrowseDVD.PerformClick(); return;
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); return;
            }
        }

        private void MouseScroll(object sender, MouseEventArgs e)
        {
            lstImages.Select();
        }

        private void LoadDVDMenu()
        {
            while (DVDs.Count > DVD_COUNT)
            {
                DVDs.RemoveAt(DVDs.Count - 1);
            }

            mnuDVD1.Visible = false; mnuDVD2.Visible = false; mnuDVD3.Visible = false; mnuDVD4.Visible = false; mnuDVD5.Visible = false;
            int DVDindex = 1;
            foreach (string DVD in DVDs)
            {

                if (CheckDVD(DVD) || cMain.IsReadOnly(Path.GetDirectoryName(DVD)))
                {
                    continue;
                }
                if (DVDindex > DVD_COUNT) { break; }
                if (DVD != "N/A")
                {
                    switch (DVDindex)
                    {
                        case 1:
                            mnuDVD1.Text = DVD; mnuDVD1.Visible = true;
                            break;
                        case 2:
                            mnuDVD2.Text = DVD; mnuDVD2.Visible = true;
                            break;
                        case 3:
                            mnuDVD3.Text = DVD; mnuDVD3.Visible = true;
                            break;
                        case 4:
                            mnuDVD4.Text = DVD; mnuDVD4.Visible = true;
                            break;
                        case 5:
                            mnuDVD5.Text = DVD; mnuDVD5.Visible = true;
                            break;
                    }
                    DVDindex++;

                }
            }
        }

        private void frmWIMManager_Load(object sender, EventArgs e)
        {
            cMain.FormIcon(this); cMain.eLBL = lblWIM; cMain.eForm = this;
            SplitContainer1.Panel2Collapsed = true;
            cMain.ToolStripIcons(ToolStrip1);
            cMain.selectedImages.Clear();
            DVDs.Clear();

            if (!string.IsNullOrEmpty(cOptions.pLastWim))
            {
                string LW = cOptions.pLastWim;
                foreach (string S in LW.Split('|'))
                {
                    if (!string.IsNullOrEmpty(S) && S != "N/A" && !DVDs.Contains(S))
                    {
                        DVDs.Add(S);
                    }

                }
                LoadDVDMenu();
            }

            try
            {
                var toolTip1 = new ToolTip();
                toolTip1.AutoPopDelay = 10000;
                toolTip1.InitialDelay = 500;
                toolTip1.ReshowDelay = 500;
                toolTip1.IsBalloon = true;
                toolTip1.ShowAlways = true;
            }
            catch { }

            if (cMain.SWT != 0)
            {
                switch (cMain.SWT)
                {
                    case 1:
                        mnuAIO.Visible = false;
                        Text = "WIM Manager [All-In-One Integrator]";
                        break;
                    case 2:
                        mnuRHM.Visible = false;
                        Text = "WIM Manager [WIM Registry Editor]";
                        break;
                    case 3:
                        mnuComp.Visible = false;
                        Text = "WIM Manager [Component Remover]";
                        break;
                }

            }
            else
            {
                mnuWM.Visible = false;
            }
        }

        private void frmWIMManager_Shown(object sender, EventArgs e)
        {
            SplitContainer1.Scale4K(_4KHelper.Panel.Pan1);
            SplitContainer2.Scale4K(_4KHelper.Panel.Pan1);
            if (!string.IsNullOrEmpty(cOptions.LastWim) && File.Exists(cOptions.LastWim))
            {
                lblWIM.Text = cOptions.LastWim;
            }

            if (lstImages.Items.Count == 0)
                lstImages.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            else
                lstImages.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            Application.DoEvents();
            RunWorker();
        }

        private void frmWIMManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            cMain.OnFormClosing(this);

            if (FBusy)
            {
                MessageBox.Show("You can't close whilst WIM Manager is busy.", "Busy");
                e.Cancel = true;
                return;
            }

            if (BWRebuild.IsBusy)
            {
                MessageBox.Show("You can't close while an image is being rebuilt!", "Rebuild in Progress");
                e.Cancel = true;
                return;
            }
            if (BWR.IsBusy)
            {
                MessageBox.Show("You can't close while an image is being scanned!", "Scan in Progress");
                e.Cancel = true;
                return;
            }
            if (BWMerge.IsBusy)
            {
                MessageBox.Show("You can't close while an image is being merged!", "Merge in Progress");
                e.Cancel = true;
            }
            foreach (Thread t in threads.Where(t => t.IsAlive))
            {
                t.Abort();
            }
        }

        private void SavePWIm()
        {
            string P = "";
            if (mnuDVD1.Text != "N/A" && !P.ContainsIgnoreCase(mnuDVD1.Text)) { P += mnuDVD1.Text + "|"; } else { P += "N/A|"; }
            if (mnuDVD2.Text != "N/A" && !P.ContainsIgnoreCase(mnuDVD2.Text)) { P += mnuDVD2.Text + "|"; } else { P += "N/A|"; }
            if (mnuDVD3.Text != "N/A" && !P.ContainsIgnoreCase(mnuDVD3.Text)) { P += mnuDVD3.Text + "|"; } else { P += "N/A|"; }
            if (mnuDVD4.Text != "N/A" && !P.ContainsIgnoreCase(mnuDVD4.Text)) { P += mnuDVD4.Text + "|"; } else { P += "N/A|"; }
            if (mnuDVD5.Text != "N/A" && !P.ContainsIgnoreCase(mnuDVD5.Text)) { P += mnuDVD5.Text + "|"; } else { P += "N/A"; }
            cOptions.pLastWim = P;
            cOptions.SaveSettings();
        }

        private void frmWIMManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            cOptions.SaveSettings();
            if (Selected == false)
            {
                cMain.ReturnME();
            }
        }

        private void BWRebuild_DoWork(object sender, DoWorkEventArgs e)
        {
            WIMImage rebuildFile = Images.FirstOrDefault(w => string.IsNullOrEmpty(w.MountPath));
            if (rebuildFile == null)
            {
                MessageBox.Show("No WIM file has been selected.", "Aborted - File Missing");
                return;
            }

            cMount.Rebuild(rebuildFile.Location, lblWIM, this, true);
        }

        bool FBusy;
        private void TaskInProgress(bool Enable = false)
        {
            FBusy = Enable == false;
            mnuTools.Visible = Enable;
            mnuImage.Visible = Enabled;
            toolStripSeparator1.Visible = Enable;
            lstImages.Enabled = Enable;
            ToolStrip1.Enabled = Enable;

            mnuBrowse.Visible = Enable;
            cmdBrowseDVD.Enabled = Enable;

            Application.DoEvents();
        }

        string SWM = "";
        int _notSupported = 0;
        private void BWR_DoWork(object sender, DoWorkEventArgs e)
        {

            SWM = "";
            cmdEI.Visible = false;

            TaskInProgress();
            lstImages.Items.Clear();
            Images.Clear();
            int iCount = 0;

            cMain.UpdateToolStripLabel(lblWIM, "Unmounting Registries...");
            Application.DoEvents();
            cReg.RegUnLoadAll();

            cMain.UpdateToolStripLabel(lblWIM, "Loading previous mount data, please wait...");
            Application.DoEvents();

            _notSupported = 0;
            try
            {
                if (cReg.RegCheckMounted("SOFTWARE\\Microsoft\\WIMMount\\Mounted Images\\"))
                {
                    foreach (cReg.Mounted RK in cReg.RegCheckMountDism())
                    {
                        string WimMount = RK.MountPath;

                        try
                        {

                            int State = RK.State;

                            bool Valid = Directory.Exists(WimMount);

                            if (Valid == false)
                            {
                                cMain.UpdateToolStripLabel(lblWIM, "Removing Invalid: [" + WimMount + "]");
                                Application.DoEvents();
                                cReg.DeleteKey(Registry.LocalMachine, "SOFTWARE\\Microsoft\\WIMMount\\Mounted Images", RK.Key);
                                Files.DeleteFolder(WimMount, false);
                            }
                            else
                            {
                                cMain.UpdateToolStripLabel(lblWIM, "Recovering Mount: [" + WimMount + "]");
                                Application.DoEvents();
                                cMain.OpenProgram("\"" + cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe") + "\"", "/remount \"" + WimMount + "\"", true, System.Diagnostics.ProcessWindowStyle.Hidden);

                                cMain.UpdateToolStripLabel(lblWIM, "Loading Mount: [" + WimMount + "]");
                                Application.DoEvents();
                                if (State == 1 || State == 2 || State == 3)
                                {
                                    var NI = new ListViewItem();
                                    WIMImage sImage = new WIMImage(RK.Key);
                                    if (!sImage.Supported)
                                    {
                                        _notSupported++;
                                        continue;
                                    }

                                    bool M = cMain.IsApplicationAlreadyRunning("Imagex");

                                    if (!File.Exists(sImage.Location))
                                    {
                                        NI.BackColor = Color.LightPink;
                                    }
                                    else if (State == 1 || State == 3)
                                    {
                                        if (M == false)
                                        {
                                            NI.BackColor = Color.LightPink;
                                        }
                                        else
                                        {
                                            NI.BackColor = Color.Yellow;
                                        }
                                    }

                                    NI.Text = sImage.Name;

                                    NI.SubItems.Add(sImage.MountPath);
                                    NI.SubItems.Add(sImage.Architecture.ToString());
                                    NI.SubItems.Add(sImage.Build);
                                    NI.SubItems.Add(sImage.Size);
                                    NI.SubItems.Add(sImage.Index.ToString());
                                    NI.SubItems.Add(sImage.Location.ToString());
                                    NI.ToolTipText = sImage.ToolTipText;
                                    NI.SubItems.Add(sImage.MountPath);

                                    NI.SubItems.Add(sImage.Name);
                                    NI.SubItems.Add(sImage.Name);
                                    NI.SubItems.Add(sImage.Edition);
                                    NI.Tag = sImage;

                                    NI.Font = new Font(NI.Font, FontStyle.Bold);
                                    NI.Group = lstImages.Groups[0];

                                    if (!string.IsNullOrEmpty(sImage.MountPath))
                                    {
                                        Thread t = new Thread(() =>
                                        {
                                            while (!sImage.IsCalculated)
                                            {
                                                if (sImage == null) { return; }
                                                string newSize = sImage.Size;
                                                if (NI.SubItems[4].Text != newSize)
                                                {
                                                    NI.SubItems[4].Text = newSize;
                                                }
                                                Thread.Sleep(100);
                                            }
                                            NI.SubItems[4].Text = sImage.Size + " ✓";
                                        });
                                        t.IsBackground = true;
                                        threads.Add(t);
                                        t.Start();

                                    }
                                    lstImages.Items.Add(NI);

                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            LargeError LE = new LargeError("Unknown Error", "Could not get previous mount data\nMount Dir: " + WimMount, Ex);
                            LE.Upload(); LE.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                LargeError LE = new LargeError("Mount Checking", "Win Toolkit was unable to check for mounted images.", lblWIM.Text, Ex);
                LE.Upload(); LE.ShowDialog();
            }

            cMain.FreeRAM();
            try
            {
                if (iCount > 0)
                {
                    cmdU.Visible = false;
                    cmdCleanMGR.Visible = false;
                    cmdSetProductKey.Visible = false;
                    cmdDeleteImage.Visible = false;
                    cmdMountImage.Visible = false;
                    mnuImage.Visible = false;
                    cmdMakeISO.Visible = false;
                    cmdEdit.Visible = false;
                    cmdRebuildImg.Visible = false;
                    cmdS.Visible = cMain.SWT != 0;
                    //  ToolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                }
            }
            catch
            {
            }

            cReg.RegUnLoadAll();

            cMain.UpdateToolStripLabel(lblWIM, "Checking for previous WIM File...");
            Application.DoEvents();
            try
            {
                if (!string.IsNullOrEmpty(cOptions.LastWim) && File.Exists(cOptions.LastWim) &&
                     lstImages.FindItemWithText(cOptions.LastWim) == null)
                {

                    var directories = new List<string>();
                    foreach (string line in cOptions.LastWim.Split('\\'))
                    {
                        if (!directories.Contains(line))
                        {
                            directories.Add(line);
                        }
                    }

                    string newPath = directories.Aggregate((current, next) => current + "\\" + next);
                    string dir = Path.GetDirectoryName(newPath).ToUpper();
                    if (newPath != cOptions.LastWim && dir.EndsWithIgnoreCase("\\SOURCES"))
                    {
                        string oldDir = cMain.GetDVD(cOptions.LastWim);
                        string newDir = cMain.GetDVD(newPath);
                        int count = Directory.GetFileSystemEntries(newDir, "*").Length;

                        if (count == 1)
                        {
                            cMain.UpdateToolStripLabel(lblWIM, "Moving WIM...");
                            cMain.MoveDirectory(oldDir, newDir, false);
                            cOptions.LastWim = newDir + "sources\\install.wim";
                        }
                    }

                    cMain.UpdateToolStripLabel(lblWIM, "Loading WIM...");
                    Application.DoEvents();
                    string cEI = "";
                    try
                    {
                        string L = Path.GetDirectoryName(cOptions.LastWim) + "\\";

                        if (File.Exists(L + "ei.cfg"))
                        {
                            cmdEI.Visible = true;
                            cmdEI.Text = "Remove 'ei.cfg'";
                            var SR = new StreamReader(L + "ei.cfg");
                            cEI = SR.ReadToEnd();
                            SR.Close();
                        }


                        if (L.ToUpper().EndsWithIgnoreCase("\\SOURCES\\") && !File.Exists(L + "install.ini") && cOptions.AddRunOnce)
                        {
                            WriteSample(L + "install.ini");
                        }

                        if (File.Exists(L + "ei.cfg.bak"))
                        {
                            cmdEI.Visible = true;
                            cmdEI.Text = "Restore 'ei.cfg'";
                        }
                    }
                    catch
                    {
                        cEI = "";
                    }

                    try
                    {
                        string WimInfo = cMount.CWIM_GetWimImageInfo(cOptions.LastWim, lblWIM);

                        if (string.IsNullOrEmpty(WimInfo))
                        {
                            return;
                        }
                        int N = 1;
                        foreach (string mImage in Regex.Split(WimInfo, "<IMAGE INDEX=", RegexOptions.IgnoreCase))
                        {
                            if (!mImage.ContainsIgnoreCase("<WIM>"))
                            {
                                WIMImage W = new WIMImage(cOptions.LastWim, mImage, N);
                                if (!W.Supported)
                                {
                                    _notSupported++;
                                    continue;
                                }

                                Images.Add(W);

                                var NI = new ListViewItem();
                                NI.Text = W.Name;
                                NI.Tag = W;
                                NI.SubItems.Add(W.Description);
                                NI.SubItems.Add(W.Architecture.ToString());

                                NI.SubItems.Add(W.Build);
                                NI.SubItems.Add(W.Size);

                                NI.SubItems.Add(Convert.ToString(N));
                                NI.SubItems.Add(cOptions.LastWim);
                                NI.SubItems.Add("");

                                if (!string.IsNullOrEmpty(cEI) && !string.IsNullOrEmpty(W.Edition))
                                {
                                    if (cEI.ContainsIgnoreCase(W.Edition.ToUpper()))
                                    {
                                        NI.BackColor = Color.LightGreen;
                                    }
                                }
                                NI.SubItems.Add(W.DisplayName);
                                NI.SubItems.Add(W.DisplayDescription);
                                NI.SubItems.Add(W.Edition);
                                NI.ToolTipText = W.ToolTipText;

                                if (W.Build.EqualsIgnoreCase("??") && W.Architecture == Architecture.Unknown)
                                {
                                    NI.Group = lstImages.Groups[2];
                                }
                                else
                                {
                                    NI.Group = lstImages.Groups[1];
                                }

                                lstImages.Items.Add(NI);
                                N += 1;
                            }
                        }
                        SWM = cMount.CWIM_GetWimInfo(cOptions.LastWim);

                    }
                    catch (Exception Ex)
                    {
                    }
                }

            }
            catch (Exception Ex)
            {
                cMain.UpdateToolStripLabel(lblWIM, "Please select a WIM File...");
                cMain.ErrorBox("Win Toolkit encountered an unknown error", "Unknown Error", Ex.Message);
            }

            cMain.UpdateToolStripLabel(lblWIM, "Please select a WIM File...");
            cmdRefresh.Enabled = false;

            Application.DoEvents();
            cMain.FreeRAM();
        }

        private void WriteSample(string sSaveTo)
        {
            string saveDrive = Path.GetPathRoot(sSaveTo);

            if (cMain.IsReadOnly(saveDrive)) { return; }

            if (new DriveInfo(saveDrive).AvailableFreeSpace == 10240)
            {
                return;
            }

            try
            {
                using (StreamWriter SW = new StreamWriter(sSaveTo))
                {
                    SW.WriteLine("#Any line starting with a # are comments and will be ignored.");
                    SW.WriteLine();
                    SW.WriteLine("#Lets you specify the configuration of the installers");
                    SW.WriteLine("#InstallDir is where your apps are located.");
                    SW.WriteLine("#DriverDir is which directories to scan for inf files.");
                    SW.WriteLine("#Reboot makes RunOnce reboot Windows after completion.");
                    SW.WriteLine("# %DVD% is the DVD root.");
                    SW.WriteLine("# %APP% is the setup exe folder root.");
                    SW.WriteLine();
                    SW.WriteLine("#NOTE: INSTALLDIR will ALWAYS check the following directories even if not specified in the config section:");
                    SW.WriteLine("#INSTALLDIR=Sources\\WinToolkit_Apps");
                    SW.WriteLine("#INSTALLDIR=WinToolkit_Apps");
                    SW.WriteLine();
                    SW.WriteLine("[Config]");
                    SW.WriteLine("INSTALLDIR=Sources\\Apps");
                    SW.WriteLine("DRIVERDIR=Sources\\Drivers");
                    SW.WriteLine();
                    SW.WriteLine("#Here are some other samples.");
                    SW.WriteLine("#INSTALLDIR=E:\\Software");
                    SW.WriteLine("#DRIVERDIR=X:\\MyDriverCollection");
                    SW.WriteLine("");
                    SW.WriteLine("#Determines if the countdown dialog is shown");
                    SW.WriteLine("COUNTDOWN=TRUE");
                    SW.WriteLine("");
                    SW.WriteLine("#Determines if the computer will automatically restart once completed.");
                    SW.WriteLine("REBOOT=FALSE");
                    SW.WriteLine();
                    SW.WriteLine("#Things which will always get installed. Anything after * is a switch i.e. /S /Q /Silent");
                    SW.WriteLine("[Automatic]");
                    SW.WriteLine("#KBXXXXXX-x64=%DVD%:\\WinToolkit_Apps\\Windows6.1-KBXXXXXX-x64\\Windows6.1-KBXXXXXX-x64.msu");
                    SW.WriteLine();
                    SW.WriteLine("#User will be asked to choose which of the following apps to install.");
                    SW.WriteLine("[Manual]");
                    SW.WriteLine("#Microsoft Office 2013=%DVD%:\\WinToolkit_Apps\\Office2013\\Setup.exe*/config %APP%:\\MyConfig.xml");
                    SW.WriteLine("#Example 1=%DVD%:\\WinToolkit_Apps\\ExampleProgram\\Example.exe*/s");
                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Unable to write sample.", Ex.Message, cOptions.LastWim);
            }
        }



        private void BWMerge_DoWork(object sender, DoWorkEventArgs e)
        {

            string WIM = lblWIM.Text;
            cMain.UpdateToolStripLabel(lblWIM, "Rebuilding and Merging WIM files, please wait...");

            try
            {
                Files.DeleteFile(WIM + ".bak");
                Files.DeleteFile(WIM + ".bak");
                nMerge = 0;
                nStatus = ": Rebuilding WIM";
                string C = "";
                if (cOptions.mCheck)
                {
                    C = " /CHECK";
                }

                if (cOptions.QuickMerge == false)
                {
                    mImagex("/export \"" + WIM + "\" * \"" + WIM + ".bak\" /compress maximum" + C);
                    cMain.UpdateToolStripLabel(lblWIM, "Checking WIM #2...");
                    nStatus = ": Merging WIM";
                    mImagex("/export \"" + WIMM2 + "\" * \"" + WIM + ".bak\" /compress maximum" +
                              C);
                }
                else
                {
                    nStatus = ": Quick Merging WIM";
                    mImagex("/export \"" + WIMM2 + "\" * \"" + WIM + "\" /compress maximum" + C);
                }
                if (cMain.AppErrC == 0)
                {
                    if (cOptions.QuickMerge == false)
                    {
                        cMain.UpdateToolStripLabel(lblWIM, "Removing backups...");
                        Application.DoEvents();
                        File.Move(WIM, WIM + ".bak");
                        File.Move(WIM + ".bak", WIM);
                        Files.DeleteFile(WIM + ".bak");
                    }
                }
                else
                {
                    MessageBox.Show(
                         "Sometimes this error happens because some of the images in your 2nd wim file has the same name as an image in your main wim file, try renaming an image!",
                         "Error (" + Convert.ToString(cMain.AppErrC) + ")");
                    Files.DeleteFile(WIM + ".bak");
                }
            }
            catch (Exception Ex)
            {
                if (File.Exists(WIM + ".bak"))
                {
                    Files.DeleteFile(WIM);
                    Files.DeleteFile(WIM + ".bak");
                    File.Move(WIM + ".bak", WIM);
                }
                MessageBox.Show(Ex.Message, "Error!");
            }
        }

        private void mImagex(string Argument)
        {
            string sImagexTemp = cMain.UserTempPath + "\\WinToolkit_Imagex";
            cMain.WriteResource(Properties.Resources.imagex, cMain.UserTempPath + "\\Files\\Imagex.exe", this);
            cMain.AppErrM = "";
            using (var p = new Process())
            {
                p.StartInfo.FileName = "\"" + cMain.DetectXPImagex(cMain.UserTempPath + "\\Files\\Imagex.exe") + "\"";
                p.StartInfo.Arguments = Argument;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.OutputDataReceived += OnDataReceived;
                p.StartInfo.Arguments += " /Temp \"" + sImagexTemp + "\"";
                Files.DeleteFolder(sImagexTemp, true);
                p.Start();
                p.BeginOutputReadLine();
                p.WaitForExit();
                p.CancelOutputRead();
                Files.DeleteFolder(sImagexTemp, false);
                cMain.AppErrC = p.ExitCode;

            }

        }

        private void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null && !string.IsNullOrEmpty(e.Data))
            {

                if (!e.Data.ContainsIgnoreCase("%"))
                {
                    cMain.AppErrM += e.Data + Environment.NewLine;
                }

                if (e.Data.ContainsIgnoreCase(" 1%") && nAllow)
                {
                    nMerge += 1;
                    nAllow = false;
                }
                if (e.Data.ContainsIgnoreCase(" 2%") && nAllow == false)
                {
                    nAllow = true;
                }

                if (e.Data.ContainsIgnoreCase("%"))
                {
                    string T = e.Data;
                    while (!T.EndsWithIgnoreCase("]"))
                    {
                        T = T.Substring(0, T.Length - 1);
                    }
                    lblWIM.Text = T + nStatus + " (#" + nMerge + ")...";
                    Application.DoEvents();
                }
            }
        }

        private void BWRebuild_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TaskInProgress(true);
            lblWIM.Text = cOptions.LastWim;
        }

        private void BWR_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_notSupported > 0)
            {
                if (lstImages.Items.Count == 0)
                {
                    cMain.UpdateToolStripLabel(lblWIM, "Not supported.");
                }
                DialogResult DR = MessageBox.Show("Some versions selected are not supported on this version of Win Toolkit.\n\nWould you like to try Win Toolkit v2.x instead?", "Notice", MessageBoxButtons.YesNo);
                if (DR == DialogResult.Yes)
                {
                    cMain.OpenLink("http://www.wincert.net/forum/index.php?/forum/213-win-toolkit-v2x/");
                }
            }

            if ((!SWM.ContainsIgnoreCase(": 1/1") && !string.IsNullOrEmpty(SWM)) && (SWM.ContainsIgnoreCase("GUID") || SWM.ContainsIgnoreCase("{")))
            {
                cMain.ErrorBox("This seems to be an *.swm file which is read-only and not supported by Win Toolkit. You need to merge this back into an *.wim file first!", "*.SWM files are not supported!", SWM);
            }
            SWM = "";
            cmdApplyUN.Visible = false;

            SplitContainer1.Panel2Collapsed = true;
            if (!string.IsNullOrEmpty(cOptions.LastWim) && File.Exists(cOptions.LastWim))
            {
                lblWIM.Text = cOptions.LastWim;
                cmdRefresh.Enabled = true;
            }

            SavePWIm();

            try
            {
                if (cMain.SWT > 0) { cmdSelect.Visible = true; }
                if (lstImages.Items.Count > 1 && cMain.SWT == 1)
                {
                    cmdSelectAll.Visible = true;
                }
                else
                {
                    cmdSelectAll.Visible = false;
                }
            }
            catch
            {
            }
            TaskInProgress(true);
            Application.DoEvents();
            if (cMain.SWT == 1 && lstImages.Items.Count > 0)
            {
                bool f86 = false;
                bool f64 = false;
                foreach (ListViewItem LST in lstImages.Items)
                {
                    if (LST.SubItems[2].Text.EqualsIgnoreCase("x86")) { f86 = true; }
                    if (LST.SubItems[2].Text.EqualsIgnoreCase("x64")) { f64 = true; }
                    if (f86 && f64) { cmdSelectSep.Visible = true; cmdSelectAll86.Visible = true; cmdSelectAll64.Visible = true; cmdSelectAll.Visible = false; break; }
                }
            }
            if (lstImages.Items.Count > 0)
            {
                cmdApplyUN.Visible = true;
                SplitContainer1.Panel2Collapsed = false;
                lstImages.Items[_lastImageSelected].Selected = true;
                lstImages.Select();

                lstImages.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            else
            {
                lstImages.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lstImages.Columns[lstImages.Columns.Count - 1].Width = -2;
            }

            _lastImageSelected = 0;
        }

        private void BWMerge_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblWIM.Text = cOptions.LastWim;
            TaskInProgress(true);
            lstImages.Items.Clear();
            Images.Clear();

            RunWorker();
        }

        private static string GetWIMValue(string L)
        {
            while (L.ContainsIgnoreCase(">"))
            {
                L = L.Substring(1);
            }
            return L;
        }

        private void cmdMountImage_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please select 1 image!", "Invalid Selected");
                return;
            }

            if (lstImages.SelectedItems.Count > 1)
            {
                MessageBox.Show("You can only mount 1 image at a time!", "Too many images");
                return;
            }

            string F = lstImages.SelectedItems[0].SubItems[6].Text;
            if (!File.Exists(F))
            {
                MessageBox.Show("Win Toolkit is no longer able to find the following file and can't continue!" + Environment.NewLine + Environment.NewLine + F, "File Missing");
                return;
            }

            foreach (ListViewItem Image in lstImages.Items)
            {
                if (Image.Font.Bold && Image.Group.Header != "Already Mounted Images")
                {
                    MessageBox.Show("There is already an image mounted", "Unmount first!");
                }
            }

            string FBD = cMain.FolderBrowserVista("Select Mount Path...", true, true, cOptions.MountTemp);

            if (string.IsNullOrEmpty(FBD))
            {
                return;
            }

            if (FBD.Length < 4)
            {
                FBD = FBD.Substring(0, 1) + ":\\Mount";
            }

            try
            {
                var DI = new DriveInfo(FBD.Substring(0, 1));
                if (DI.DriveFormat != "NTFS")
                {
                    MessageBox.Show("The path you have selected is not supported, please make sure it is NTFS!",
                                         "Invalid Location");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("The path you have selected is not supported!", "Invalid Location");
                return;
            }

            if (FBD.ContainsForeignCharacters())
            {
                MessageBox.Show(
                     "This mount path has foreign characters (non-English) which can greatly affect the All-In-One Integrator, please chose something simple such as 'C:\\WinToolkit_Mount'.",
                     "Invalid Location");
                return;
            }

            TaskInProgress();
            foreach (ListViewItem Image in lstImages.SelectedItems)
            {
                WIMImage sImage = (WIMImage)Image.Tag;
                sImage.Mount(FBD, lblWIM, this);

                if (!string.IsNullOrEmpty(sImage.MountPath))
                {
                    Image.SubItems[1].Text = sImage.MountPath;
                    Image.SubItems[7].Text = FBD;
                    Image.Font = new Font(Image.Font, FontStyle.Bold);
                    cmdU.Visible = true;
                    cmdCleanMGR.Visible = true;
                    cmdSetProductKey.Visible = true;
                    cmdMountImage.Visible = false;
                    cmdDeleteImage.Visible = false;
                    mnuImage.Visible = false;
                    cmdEdit.Visible = false;
                    Image.Group = lstImages.Groups[0];
                    //    ToolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                    cMain.OpenExplorer(this, sImage.MountPath);
                }
            }

            lblWIM.Text = cOptions.LastWim;
            RunWorker();
        }

        private int _lastImageSelected = 0;
        private void cmdEditName_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please select 1 image!", "Invalid Selected");
                return;
            }

            ListViewItem selected = lstImages.SelectedItems[0];
            WIMImage sImage = (WIMImage)selected.Tag;
            string Answer = cMain.InputBox("Please enter a new name for the image", "New Name", sImage.Name);
            if (Answer == sImage.Name || string.IsNullOrEmpty(Answer)) { return; }

            if (lstImages.FindItemWithText(Answer, false, 0) != null && lstImages.FindItemWithText(Answer, false, 0).Text == Answer)
            {
                MessageBox.Show("'" + Answer + "' name already exists!", "Invalid Name");
                return;
            }

            if (!File.Exists(sImage.Location))
            {
                MessageBox.Show("Win Toolkit can't find the following file:" + Environment.NewLine + Environment.NewLine + sImage.Location, "Aborted - File Missing");
                return;
            }
            _lastImageSelected = lstImages.SelectedItems[0].Index;

            string originalMessage = lblWIM.Text;
            cMain.UpdateToolStripLabel(lblWIM, "Renaming...");
            Application.DoEvents();
            sImage.Name = Answer;

            cMain.UpdateToolStripLabel(lblWIM, "Finished...");
            Application.DoEvents();
            lblWIM.Text = originalMessage;

            lstImages.Items.Clear();
            Images.Clear();
            Application.DoEvents();
            cMain.FreeRAM();
            RunWorker();
            TaskInProgress();
        }

        private void cmdEditDesc_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please select 1 image!", "Invalid Selected");
                return;
            }

            ListViewItem selected = lstImages.SelectedItems[0];
            WIMImage sImage = (WIMImage)selected.Tag;
            string Answer = cMain.InputBox("Please enter a new description for the image", "New Description", sImage.Description);
            if (Answer == sImage.Description || string.IsNullOrEmpty(Answer)) { return; }

            if (!File.Exists(sImage.Location))
            {
                MessageBox.Show("Win Toolkit can't find the following file:" + Environment.NewLine + Environment.NewLine + sImage.Location, "Aborted - File Missing");
                return;
            }

            _lastImageSelected = lstImages.SelectedItems[0].Index;
            string originalMessage = lblWIM.Text;

            cMain.UpdateToolStripLabel(lblWIM, "Renaming...");
            Application.DoEvents();
            sImage.Description = Answer;

            lstImages.Items.Clear();
            Images.Clear();
            Application.DoEvents();
            cMain.FreeRAM();
            RunWorker();
            TaskInProgress();
        }

        private void cmdDeleteImage_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one item!", "Invalid Selection");
                return;
            }

            if (lstImages.SelectedItems.Count == lstImages.Items.Count)
            {
                MessageBox.Show("You need to keep at least 1 image in a wim file!", "Access Denied");
                return;
            }

            string F = lstImages.SelectedItems[0].SubItems[6].Text;
            if (!File.Exists(F))
            {
                MessageBox.Show("Win Toolkit can't find the following file:" + Environment.NewLine + Environment.NewLine + F, "Aborted - File Missing");
                return;
            }

            DialogResult Dresult = MessageBox.Show("Are you sure you wish to permanently delete the selected images?",
                                                                "Are you sure?", MessageBoxButtons.YesNo);
            if (Dresult != DialogResult.Yes)
            {
                return;
            }
            TaskInProgress();
            string L = lblWIM.Text;

            foreach (ListViewItem LST in lstImages.SelectedItems)
            {
                cMain.UpdateToolStripLabel(lblWIM, "Deleting Image: " + LST.Text);
                Application.DoEvents();

                cMount.CWIM_DeleteWIM(LST.SubItems[6].Text, (LST.Index + 1));
                LST.Remove();
            }
            Application.DoEvents();
            lblWIM.Text = L;
            lstImages.Items.Clear();
            Images.Clear();
            RunWorker();
        }

        private void lstImages_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                cmdEI.Visible = false;
                if (lstImages.SelectedItems.Count > 0)
                {
                    if (lstImages.SelectedItems.Count == 1) { cmdSelect.Text = "Select [" + e.Item.Text + "]"; } else { cmdSelect.Text = "Select [" + lstImages.SelectedItems.Count + " Images]"; }
                    string L = lstImages.SelectedItems[0].SubItems[6].Text;

                    while (!L.EndsWithIgnoreCase("\\"))
                    {
                        L = L.Substring(0, L.Length - 1);
                    }

                    if (File.Exists(L + "ei.cfg") || File.Exists(L + "ei.cfg.bak"))
                    {
                        cmdEI.Visible = true;
                        if (File.Exists(L + "ei.cfg.bak")) { cmdEI.Text = "Restore 'ei.cfg'"; }
                        if (File.Exists(L + "ei.cfg")) { cmdEI.Text = "Remove 'ei.cfg'"; }
                    }
                }
            }
            catch { }

            try
            {
                cmdU.Visible = false;
                cmdCleanMGR.Visible = false;
                cmdSetProductKey.Visible = false;
                cmdMountImage.Visible = false;
                cmdDeleteImage.Visible = false;
                mnuImage.Visible = false;
                cmdEdit.Visible = false;
                cmdMergeSWM.Visible = false;
                cmdOpenMount.Visible = false;
                cmdMakeISO.Visible = false;
                cmdRebuildImg.Visible = false;
                cmdS.Visible = false;
                cmdApplyUN.Visible = false;
                //  ToolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;

                if (e.Item.SubItems[6].Text.ToUpper().EndsWithIgnoreCase(".SWM"))
                {
                    cmdMergeSWM.Visible = true;
                    return;
                }

                if (cMain.SWT != 0) { cmdS.Visible = true; }
                if (e.Item.Group.Header.EqualsIgnoreCase("Already Mounted Images"))
                {

                    if (e.Item.BackColor != Color.Yellow && e.Item.BackColor != Color.LightPink)
                    {
                        cmdU.Visible = true;
                        cmdCleanMGR.Visible = true;
                        cmdSetProductKey.Visible = true;
                        cmdApplyUN.Visible = true;
                    }

                    if (e.Item.BackColor == Color.LightPink)
                    {
                        cmdU.Visible = true;
                        cmdS.Visible = false;
                    }

                    if (e.Item.Text != "Image is still being unmounted...")
                    {
                        cmdOpenMount.Visible = true;
                    }
                }
                else
                {
                    cmdApplyUN.Visible = true;
                    cmdMakeISO.Visible = true;
                    cmdRebuildImg.Visible = true;
                    cmdDeleteImage.Visible = true;
                    cmdEdit.Visible = true;
                    if (cMain.SWT == 0)
                    {
                        // ToolStrip1.LayoutStyle = ToolStripLayoutStyle.Flow;
                        cmdMountImage.Visible = true;
                        mnuImage.Visible = true;
                    }
                }
            }
            catch { }
        }

        private void cmdU_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one item!", "Invalid Selection");
                return;
            }

            bool Abort = false;
            foreach (ListViewItem LST in lstImages.SelectedItems)
            {
                if (LST.Text.EqualsIgnoreCase("Image is still being mounted..."))
                {
                    Abort = true;
                    MessageBox.Show("There is an image still being mounted, please try again later", "Please Wait");
                }
                if (LST.Text.EqualsIgnoreCase("Image is still being unmounted..."))
                {
                    Abort = true;
                    MessageBox.Show("There is an image still being mounted, please try again later", "Please Wait");
                }
            }
            TaskInProgress();
            if (Abort)
            {
                string DVDP = lblWIM.Text;
                cMain.UpdateToolStripLabel(lblWIM, "Loading WIM...");
                Application.DoEvents();
                cOptions.LastWim = DVDP;
                lblWIM.Text = DVDP;
                lstImages.Items.Clear();
                Images.Clear();
                RunWorker();
                return;
            }

            foreach (ListViewItem LST in lstImages.SelectedItems)
            {
                try
                {
                    cMain.selectedImages.Add((WIMImage)LST.Tag);
                    bool U = LST.BackColor == Color.LightPink ? cMount.CWIM_UnmountImage(LST.Text, lblWIM, true, false, false, this, LST.SubItems[6].Text, false) : cMount.CWIM_UnmountImage(LST.Text, lblWIM, false, true, true, this, LST.SubItems[6].Text, true);

                    if (U)
                    {
                        TaskInProgress();
                        LST.SubItems[7].Text = "";
                        LST.Font = new Font(LST.Font, FontStyle.Regular);
                        cmdU.Visible = false;
                        cmdCleanMGR.Visible = false;
                        cmdSetProductKey.Visible = false;
                        //ToolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                        if (LST.Group.Header.EqualsIgnoreCase("Already Mounted Images"))
                        {
                            LST.Remove();
                        }
                    }
                }
                catch
                {
                }
                cMain.selectedImages.Clear();
            }
            TaskInProgress(true);
            cMain.UpdateToolStripLabel(lblWIM, "Please select a WIM File...");
            cmdRefresh.Enabled = false;
            RunWorker();
        }

        private void cmdMI_Click(object sender, EventArgs e)
        {
            var OFD = new OpenFileDialog();
            OFD.Title = "Select WIM File";
            OFD.Filter = "Wim File *.wim|*.wim";
            OFD.Multiselect = false;
            if (OFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            WIMM2 = OFD.FileName;
            TaskInProgress();
            var FS = new DriveInfo(Directory.GetDirectoryRoot(lblWIM.Text));
            long FSWIM = new FileInfo(lblWIM.Text).Length;

            if (FS.AvailableFreeSpace < (FSWIM + 309715200))
            {
                MessageBox.Show(
                     "Not enough space, you need at least " +
                     cMain.BytesToString((FSWIM + 309715200) - FS.AvailableFreeSpace) + " extra free space",
                     cMain.BytesToString(FS.AvailableFreeSpace) + "free space on " + FS.Name);
                TaskInProgress(true);
                return;
            }
            while (BWMerge.IsBusy) { Thread.Sleep(100); Application.DoEvents(); }
            BWMerge.RunWorkerAsync();
        }

        private void cmdMakeISO_Click(object sender, EventArgs e)
        {
            cmdMakeISO.Enabled = false;
            string F = Path.GetDirectoryName(lblWIM.Text);
            try
            {
                if (lblWIM.Text.ContainsIgnoreCase("\\SOURCES\\"))
                {
                    F = Path.GetDirectoryName(F);
                }
            }
            catch (Exception)
            {
            }

            var TS = new frmISOMaker();
            try
            {
                if (!string.IsNullOrEmpty(F))
                {
                    TS.lblFolder.Text = F;
                    while (F.ContainsIgnoreCase("\\"))
                    {
                        F = F.Substring(1);
                    }
                    TS.txtName.Text = F;
                }

            }
            catch (Exception)
            {
            }

            TS.Show();

            Selected = true;
            Close();
        }

        private void cmdImportImg_Click(object sender, EventArgs e)
        {
            cmdImportImg.Enabled = false;
            var TS = new frmCaptureImg();
            TS.Show();

            TS.lblWIM.Text = lblWIM.Text;
            TS.cboCompression.SelectedIndex = 2;
            TS.RBE.Checked = true;
            TS.RBN.Enabled = false;
            TS.cmdBrowseF.Enabled = false;
            Selected = true;
            Close();
        }

        private string GetDVD(string WIM)
        {
            string DVDRoot = WIM;
            if (DVDRoot.ContainsIgnoreCase("\\SOURCES\\"))
            {
                while (!DVDRoot.EndsWithIgnoreCase("\\"))
                {
                    DVDRoot = DVDRoot.Substring(0, DVDRoot.Length - 1);
                }
                DVDRoot = DVDRoot.Substring(0, DVDRoot.Length - 1);
                while (!DVDRoot.EndsWithIgnoreCase("\\"))
                {
                    DVDRoot = DVDRoot.Substring(0, DVDRoot.Length - 1);
                }
            }
            else
            {
                DVDRoot = "";
            }
            return DVDRoot;
        }

        private void cmdApplyUN_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please select one image to integrate the unattended file into.", "Invalid Image!");
                return;
            }

            string DVD = GetDVD(lstImages.SelectedItems[0].SubItems[6].Text);

            if (string.IsNullOrEmpty(DVD))
            {
                MessageBox.Show(
                     "Win Toolkit is unable to detect a DVD root with a 'Sources' folder so this feature can't be used.",
                     "Invalid DVD");
                return;
            }

            cMain.UnattendedF = "";
            bool Full = false;
            var F = new frmUnattendedPrompt();
            F.WIM = lstImages.SelectedItems[0].SubItems[6].Text;
            F.ShowDialog();
            F.Dispose();

            string U = cMain.UnattendedF;
            if (string.IsNullOrEmpty(U))
            {
                return;
            }

            TaskInProgress();

            if (U.StartsWithIgnoreCase("|"))
            {
                Full = true;
                U = U.Substring(1);
            }

            string DVD_UF = DVD + "\\Autounattend.xml";
            DVD_UF = DVD_UF.ReplaceIgnoreCase("\\\\", "\\");

            if (DVD_UF.ToUpper() != U.ToUpper() && File.Exists(U))
            {
                try
                {
                    cMain.UpdateToolStripLabel(lblWIM, "Applying unattended to DVD [" + U + "]");
                    File.Copy(U, DVD_UF, true);
                }
                catch (Exception Ex)
                {
                    cMain.WriteLog(this, "Error adding unattended to DVD", Ex.Message, "CopyFrom: " + U + Environment.NewLine + "CopyTo: " + DVD_UF + Environment.NewLine + "Full: " + Full.ToString());
                }
            }

            string bWim = DVD + "sources\\boot.wim";
            if (Full && File.Exists(bWim))
            {
                string bWimInfo = "";
                string bWimInfoF = "";

                try
                {
                    bWimInfo = cMount.CWIM_GetWimImageInfo(bWim, lblWIM);
                    bWimInfoF = bWimInfo;
                    if (bWimInfo.ContainsIgnoreCase("<NAME>"))
                    {
                        while (bWimInfo.ContainsIgnoreCase("<NAME>"))
                        {
                            bWimInfo = bWimInfo.Substring(1);
                        }
                        while (bWimInfo.ContainsIgnoreCase("<"))
                        {
                            bWimInfo = bWimInfo.Substring(0, bWimInfo.Length - 1);
                        }
                        while (bWimInfo.ContainsIgnoreCase(">"))
                        {
                            bWimInfo = bWimInfo.Substring(1);
                        }
                    }
                    else
                    {
                        bWimInfo = "N/A";
                    }
                }
                catch (Exception Ex)
                {
                    cMain.WriteLog(this, "Could not scan boot.wim index", Ex.Message, bWimInfoF);
                }

                cMain.FreeRAM();
                cMain.UpdateToolStripLabel(lblWIM, "Mounting 2nd boot.wim image...");
                Application.DoEvents();
                cMain.selectedImages.Clear();
                cMain.selectedImages.Add((WIMImage)lstImages.SelectedItems[0].Tag);
                string BM = cMount.CWIM_MountImage(bWimInfo, 2, bWim, cOptions.WinToolkitTemp + "\\Boot", lblWIM, this);

                if (DVD_UF != U && File.Exists(U))
                {
                    try
                    {
                        cMain.UpdateToolStripLabel(lblWIM, "Applying unattended to DVD [" + U + "]");
                        File.Copy(U, BM + "\\Autounattend.xml", true);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message, "Error");
                        cMain.WriteLog(this, "Error adding unattended to WIM", Ex.Message, U);
                    }
                }

                if (!File.Exists(DVD + "sources\\Full.txt") && File.Exists(BM + "\\Autounattend.xml"))
                {
                    var SW = new StreamWriter(DVD + "sources\\Full.txt");
                    SW.WriteLine("Please do not delete");
                    SW.Close();
                }
                cMount.CWIM_UnmountImage(bWimInfo, lblWIM, false, false, false, this, bWim, false);
                cMain.selectedImages.Clear();
            }

            TaskInProgress(true);
            lblWIM.Text = cOptions.LastWim;
        }

        private void cmdRebuildImg_Click(object sender, EventArgs e)
        {
            WIMImage rebuildFile = Images.FirstOrDefault(w => string.IsNullOrEmpty(w.MountPath));
            if (rebuildFile == null)
            {
                MessageBox.Show("No WIM file has been selected.", "Aborted - File Missing");
                return;
            }

            string WIMFile = rebuildFile.Location;
            if (!File.Exists(WIMFile))
            {
                MessageBox.Show("Win Toolkit can't find the following file:\n\n" + WIMFile, "Aborted - File Missing");
                return;
            }

            var FS = new DriveInfo(Directory.GetDirectoryRoot(WIMFile));
            long FSWIM = new FileInfo(WIMFile).Length;

            if (FS.AvailableFreeSpace < (FSWIM + 209715200))
            {
                MessageBox.Show(
                     "Not enough space, you need at least " +
                     cMain.BytesToString((FSWIM + 209715200) - FS.AvailableFreeSpace) + " extra free space",
                     cMain.BytesToString(FS.AvailableFreeSpace) + "free space on " + FS.Name);
                return;
            }

            TaskInProgress();
            while (BWRebuild.IsBusy) { Thread.Sleep(100); Application.DoEvents(); }
            BWRebuild.RunWorkerAsync();
        }

        private void cmdExportImg_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 item!", "Invalid Selection");
                return;
            }

            string F = lstImages.SelectedItems[0].SubItems[6].Text;
            if (!File.Exists(F))
            {
                MessageBox.Show("Win Toolkit can't find the following file:" + Environment.NewLine + Environment.NewLine + F, "Aborted - File Missing");
                return;
            }

            var SFD = new SaveFileDialog();
            SFD.Title = "Select WIM File";
            SFD.Filter = "Wim File *.wim|*.wim";
            SFD.OverwritePrompt = false;
            if (SFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (File.Exists(SFD.FileName))
            {
                DialogResult DR =
                    MessageBox.Show(
                        "The file already exists. Would you like to OVERWRITE it?\n\nClicking no will add the images to 			the existing wim file.",
                        "Image Found", MessageBoxButtons.YesNoCancel);
                if (DR == DialogResult.Cancel) { return; }
                if (DR == DialogResult.Yes)
                {
                    Files.DeleteFile(SFD.FileName);
                }
            }

            cMain.UpdateToolStripLabel(lblWIM, "Exporting Image...");
            TaskInProgress();

            string skipped = "";
            foreach (ListViewItem LST in lstImages.SelectedItems)
            {
                nMerge = 0;
                nAllow = true;
                ;
                nStatus = "Exporting " + LST.Text;
                string C = "";
                if (cOptions.mCheck)
                {
                    C = " /CHECK";
                }
                mImagex("/export \"" + LST.SubItems[6].Text + "\" " + LST.SubItems[5].Text + " \"" + SFD.FileName + "\" /compress maximum" + C);

                if (cMain.AppErrC == 0) continue;

                if (cMain.AppErrC == 2 && cMain.AppErrM.ContainsIgnoreCase("[" + LST.Text + "]"))
                {
                    skipped += LST.Text + Environment.NewLine;
                }
                else
                {
                    Files.DeleteFile(SFD.FileName);
                    cMain.ErrorBox("An error occured whilst trying to export an image.", "Error", cMain.AppErrM);
                    break;
                }
            }
            lblWIM.Text = cOptions.LastWim;
            TaskInProgress(true);

            if (!string.IsNullOrEmpty(skipped))
            {
                MessageBox.Show("The following image(s) have been skipped because an image with their name already exists 		within the output image: " + Environment.NewLine + Environment.NewLine + skipped,
                    "Note");
            }
        }


        private void Selectimage()
        {
            string DVDP = lblWIM.Text;
            if (lstImages.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least 1 item", "Invalid Selection");
                return;
            }

            string dvdPath = lblWIM.Text.ReplaceIgnoreCase("\\\\", "\\");
            if (CheckDVD(dvdPath) || cMain.IsReadOnly(Path.GetDirectoryName(dvdPath)))
            {
                MessageBox.Show(
                    "The specified WIM is currently on a read-only device. Please copy it to your hard drive and try again.",
                    "Read-only");
                return;
            }

            string F = lstImages.SelectedItems[0].SubItems[6].Text;
            if (!File.Exists(F))
            {
                MessageBox.Show("Win Toolkit is no longer able to find the following file and can't continue!" + Environment.NewLine + Environment.NewLine + F, "File Missing");
                return;
            }

            bool Abort = false;
            cMain.UpdateToolStripLabel(lblWIM, "Checking Image Status...");
            Enabled = false;
            Application.DoEvents();

            foreach (ListViewItem LST in lstImages.SelectedItems)
            {
                if (LST.BackColor == Color.LightPink)
                {
                    Abort = true;
                    MessageBox.Show("This image has been corrupted and needs unmounting or the install.wim is not longer available!", "Error");
                }
                if (LST.Text.EqualsIgnoreCase("Image is still being mounted..."))
                {
                    Abort = true;
                    MessageBox.Show("There is an image still being mounted, please try again later", "Please Wait");
                }
                if (LST.Text.EqualsIgnoreCase("Image is still being unmounted..."))
                {
                    Abort = true;
                    MessageBox.Show("There is an image still being mounted, please try again later", "Please Wait");
                }
            }

            foreach (Thread t in threads.Where(t => t.IsAlive))
            {
                t.Abort();
            }

            cMain.UpdateToolStripLabel(lblWIM, "Enumerating lists...");
            Application.DoEvents();

            cMain.selectedImages.Clear();
            string selectedArc = "";

            foreach (ListViewItem LST in lstImages.SelectedItems)
            {
                cMain.selectedImages.Add((WIMImage)LST.Tag);
                if (selectedArc != "Multi")
                {
                    if (string.IsNullOrEmpty(selectedArc))
                    {
                        selectedArc = LST.SubItems[2].Text;
                    }
                    else
                    {
                        if (selectedArc != LST.SubItems[2].Text)
                        {
                            selectedArc = "Multi";
                        }
                    }
                }
                if (selectedArc.EqualsIgnoreCase("Multi"))
                {
                    MessageBox.Show("You can't select multiple architectures!", "Multiple Architectures");
                    cMain.selectedImages.Clear();
                    Abort = true;
                }

            }

            if (Abort)
            {
                TaskInProgress();
                cMain.UpdateToolStripLabel(lblWIM, "Loading WIM...");
                Application.DoEvents();
                cOptions.LastWim = DVDP;
                lblWIM.Text = DVDP;
                lstImages.Items.Clear();
                Images.Clear();
                RunWorker();
                Enabled = true;
                return;
            }
            Form TS = null;

            switch (cMain.SWT)
            {
                case 1:
                    TS = new frmAllInOne();

                    if (WindowState != FormWindowState.Maximized)
                    {
                        int W = Screen.PrimaryScreen.WorkingArea.Width;
                        int H = Screen.PrimaryScreen.WorkingArea.Height;
                        if (H <= 768 || W <= 1024)
                        {
                            WindowState = FormWindowState.Maximized;
                        }
                        else
                        {
                            WindowState = FormWindowState.Normal;
                            TS.Width = W - 200;
                            TS.Height = H - 100;
                        }
                    }
                    break;
                case 2:
                    TS = new frmRegMount();
                    break;
                case 3:
                    TS = new frmComponentRemover();
                    break;
            }
            cMain.UpdateToolStripLabel(lblWIM, "Loading '" + TS.Text + "...");
            Application.DoEvents();
            if (TS != null)
            {
                Selected = true;
                TS.Show();
                Close();
            }
        }

        private void cmdSelect_Click(object sender, EventArgs e)
        {
            Selectimage();
        }

        private void cmdSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LST in lstImages.Items) { LST.Selected = true; }
            Selectimage();
        }

        private void lstImages_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (cMain.SWT == 0)
            {
                foreach (ListViewItem LST in lstImages.SelectedItems) { if (LST.Font.Bold) { cMain.OpenExplorer(this, LST.SubItems[7].Text); break; } }
            }
            {
                if (cmdS.Visible)
                {
                    Selectimage();
                }
            }
        }

        private void cmdS_MouseEnter(object sender, EventArgs e)
        {
            cmdS.ShowDropDown();

        }

        private void lblWIM_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblWIM.Text))
            {
                try
                {
                    if (string.IsNullOrEmpty(cOptions.LastWim) &&
                         !string.IsNullOrEmpty(lstImages.Items[0].SubItems[6].Text))
                    {
                        lblWIM.Text = lstImages.Items[0].SubItems[6].Text.ReplaceIgnoreCase("\\\\", "\\");
                        cOptions.LastWim = lblWIM.Text;
                    }
                }
                catch
                {
                }
                if (string.IsNullOrEmpty(lblWIM.Text) && !string.IsNullOrEmpty(cOptions.LastWim))
                {
                    lblWIM.Text = cOptions.LastWim;
                }
            }
            ////Switch to boot.wim
            if (!string.IsNullOrEmpty(lblWIM.Text))
            {
                cmdSBoot.Enabled = false;
                if (lblWIM.Text.ToUpper().EndsWithIgnoreCase("\\INSTALL.WIM") || lblWIM.Text.ToUpper().EndsWithIgnoreCase("\\BOOT.WIM"))
                {
                    if (lblWIM.Text.ToUpper().EndsWithIgnoreCase("\\INSTALL.WIM"))
                    {
                        cmdSBoot.Text = "Switch to 'boot.wim'";
                        cmdSBoot.ToolTipText = "This will switch to the boot.wim in the same folder as the install.wim";
                        if (File.Exists(lblWIM.Text.ReplaceIgnoreCase("install.wim", "boot.wim")))
                        {
                            cmdSBoot.Enabled = true;
                        }
                    }

                    if (lblWIM.Text.ToUpper().EndsWithIgnoreCase("\\BOOT.WIM"))
                    {
                        cmdSBoot.Text = "Switch to 'install.wim'";
                        cmdSBoot.ToolTipText = "This will switch to the install.wim in the same folder as the boot.wim";
                        if (File.Exists(lblWIM.Text.ReplaceIgnoreCase("boot.wim", "install.wim")))
                        {
                            cmdSBoot.Enabled = true;
                        }
                    }
                }
            }
        }

        private void cmdEI_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItems.Count == 0) { return; }

            string L = lstImages.SelectedItems[0].SubItems[6].Text;

            while (!L.EndsWithIgnoreCase("\\"))
            {
                L = L.Substring(0, L.Length - 1);
            }

            if (!File.Exists(L + "ei.cfg") && !File.Exists(L + "ei.cfg.bak"))
            {
                cmdEI.Visible = false;
                return;
            }

            try
            {
                if (cmdEI.Text.EqualsIgnoreCase("Remove 'ei.cfg'"))
                {
                    cmdEI.Text = "Restore 'ei.cfg'";
                    if (File.Exists(L + "ei.cfg"))
                    {
                        File.Move(L + "ei.cfg", L + "ei.cfg.bak");
                    }

                    foreach (ListViewItem LST in lstImages.Items)
                    {
                        LST.BackColor = Color.Transparent;
                    }
                }
                else
                {
                    cmdEI.Text = "Remove 'ei.cfg'";
                    if (File.Exists(L + "ei.cfg.bak"))
                    {
                        File.Move(L + "ei.cfg.bak", L + "ei.cfg");
                        var SR = new StreamReader(L + "ei.cfg");
                        string cEI = SR.ReadToEnd();
                        SR.Close();

                        foreach (ListViewItem LST in lstImages.Items)
                        {
                            LST.BackColor = cEI.ContainsIgnoreCase(LST.SubItems[10].Text.ToUpper()) ? Color.LightGreen : Color.Transparent;
                        }
                    }
                }
            }
            catch
            {
            }

            if (!File.Exists(L + "ei.cfg") && !File.Exists(L + "ei.cfg.bak"))
            {
                cmdEI.Visible = false;
            }
        }

        private bool CheckDVD(string Path)
        {
            try
            {
                var DI = new DriveInfo(Path.Substring(0, 1));
                if (DI.DriveType == DriveType.CDRom)
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }
            return false;
        }

        private void cmdMergeSWM_Click(object sender, EventArgs e)
        {
            cmdMergeSWM.Enabled = false;
            Selected = true;
            new frmWIMManager().Show();
            Close();
        }

        private void cmdSetProductKey_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItems.Count != 1)
            {
                MessageBox.Show("You need to select an image!", "Invalid Selection");
                return;
            }

            WIMImage sImage = (WIMImage)lstImages.SelectedItems[0].Tag;
            if (!File.Exists(sImage.Location))
            {
                MessageBox.Show("Win Toolkit can't find the following file:" + Environment.NewLine + Environment.NewLine + sImage.Location, "Aborted - File Missing");
                return;
            }

            string PK = "";

            using (var prompt = new frmKeys())
            {
                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    PK = prompt.txtKey.Text;
                }
            }

            if (string.IsNullOrEmpty(PK))
            {
                return;
            }



            cMain.UpdateToolStripLabel(lblWIM, "Setting Product Key [" + lstImages.SelectedItems[0].Text + "]...");
            Application.DoEvents();
            string SPK =
                 cMain.RunExternal("\"" + DISM.Latest.Location + "\"", "/Image:\"" + lstImages.SelectedItems[0].SubItems[1].Text + "\" /Set-ProductKey:" + PK);

            if (SPK.ContainsIgnoreCase(": 0x"))
            {
                string SPKe = "";
                foreach (string S in SPK.Split(Environment.NewLine.ToCharArray()))
                {
                    if (S.ContainsIgnoreCase(": 0x"))
                    {
                        SPKe += Environment.NewLine;
                    }
                    if (!S.ContainsIgnoreCase("\\") && !string.IsNullOrEmpty(S))
                    {
                        SPKe += S + Environment.NewLine;
                    }
                }
            }
            else
            {
                MessageBox.Show("The product key has been integrated.", lstImages.SelectedItems[0].Text);
            }
            lblWIM.Text = cOptions.LastWim;
        }

        private void mnuAIO_Click(object sender, EventArgs e)
        {
            SwitchTool(1);
        }

        private void mnuComp_Click(object sender, EventArgs e)
        {
            SwitchTool(3);
        }

        private void mnuRHM_Click(object sender, EventArgs e)
        {
            SwitchTool(2);
        }

        private void mnuWM_Click(object sender, EventArgs e)
        {
            SwitchTool(0);
        }

        private void SwitchTool(int Tnumber)
        {
            var F = new frmWIMManager();
            cMain.SWT = Tnumber;
            Selected = true;
            F.Show();
            Close();
        }

        private void mnuTM_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuTools_ButtonClick(object sender, EventArgs e)
        {
            mnuTools.ShowDropDown();
        }

        private void cmdBrowseDVD_Click(object sender, EventArgs e)
        {
            if (lstImages.Items.Cast<ListViewItem>().Any(I => I.Font.Bold && I.Group.Header != "Already Mounted Images"))
            {
                MessageBox.Show("An image is still mounted, please unmount first!", "Mounted image");
                return;
            }
            string FBD = cMain.FolderBrowserVista("Select extracted DVD...", false, true);

            if (string.IsNullOrEmpty(FBD))
            {
                return;
            }

            if (Directory.Exists(FBD + "\\Sources\\EMB") || Directory.Exists(FBD + "\\EMB"))
            {
                DialogResult DR = MessageBox.Show("Win Toolkit has notice that you have selected an 'Windows Embedded' image, Win Toolkit does not fully support these types of images." +
                          Environment.NewLine + Environment.NewLine + "Continue Anyway?", "Windows Thin PC Detected!", MessageBoxButtons.YesNo);
                if (DR != DialogResult.Yes) { return; }
            }

            if (File.Exists(FBD + "\\sources\\install.esd") || File.Exists(FBD + "\\install.esd"))
            {
                NotifyESD();
                return;
            }


            string DVDP = "";

            try
            {
                if (File.Exists(FBD + "\\SOURCES\\install.wim") || File.Exists(FBD + "\\install.wim"))
                {
                    if (File.Exists(FBD + "\\SOURCES\\install.wim"))
                    {
                        DVDP = FBD.ReplaceIgnoreCase("\\\\", "\\") + "\\SOURCES\\install.wim";
                    }
                    else
                    {
                        DVDP = FBD.ReplaceIgnoreCase("\\\\", "\\") + "\\install.wim";
                    }
                }
                else
                {
                    MessageBox.Show(
                         "This does not seem to be a valid Windows Installation DVD, please make sure that the folder you select has '\\sources\\install.wim' in it",
                         "Invalid DVD");
                    return;
                }
            }
            catch { }

            if (CheckDVD(DVDP) || cMain.IsReadOnly(FBD))
            {
                DialogResult DRDVD = MessageBox.Show("You seemed to have selected a DVD Drive, which is read-only. Would you like to copy this to your hard drive?", "DVD Detected", MessageBoxButtons.YesNo);
                if (DRDVD != DialogResult.Yes) { return; }

                bool fExisted = false;
                string fExtractTo = cMain.SysDrive + "\\WinISO";
                if (Directory.Exists(fExtractTo))
                {
                    fExisted = true;
                }
                else
                {
                    cMain.CreateDirectory(fExtractTo);
                }
                string SP = cMain.FolderBrowserVista("Select folder to copy DVD", true, true, fExtractTo);

                if (string.IsNullOrEmpty(SP)) { return; }

                if (cMain.IsNetworkPath(SP))
                {
                    MessageBox.Show("Can't copy to network. This will make the ISO processing slower.", "Not Supported");
                    return;
                }

                if (cMain.IsReadOnly(SP))
                {
                    MessageBox.Show("Can't copy to read-only directory.", "Access Denied");
                    return;
                }

                long size = new DriveInfo(FBD).TotalSize;

                if (new DriveInfo(SP).AvailableFreeSpace < (size + 20971520)) // 200MB
                {
                    MessageBox.Show("Not enough free space. " + cMain.BytesToString(size) + " needed.", "Not enough room");
                    return;
                }

                if (SP.ToUpper() != fExtractTo.ToUpper() && fExisted == false) { Files.DeleteFolder(fExtractTo, false); }

                string OLBL = lblWIM.Text;
                cMain.UpdateToolStripLabel(lblWIM, "Copying Folder: [" + FBD + "] to [" + fExtractTo + "]");
                Application.DoEvents();
                FBusy = true;
                var newThread = new Thread(() => cMain.CopyDirectory(FBD, fExtractTo, true, true));

                mnuBrowse.Visible = false;
                mnuTools.Visible = false;

                newThread.Start();
                while (newThread.IsAlive)
                {
                    Thread.Sleep(250);
                    Application.DoEvents();
                }

                DVDP = fExtractTo + "\\sources\\install.wim";
                if (!File.Exists(fExtractTo + "\\sources\\install.wim")) { return; }
            }

            LoadPWIM(DVDP, false);
            TaskInProgress();
            cMain.UpdateToolStripLabel(lblWIM, "Loading WIM...");
            Application.DoEvents();
            cOptions.LastWim = DVDP;
            lblWIM.Text = DVDP;
            cOptions.SaveSettings();
            lstImages.Items.Clear();
            Images.Clear();
            RunWorker();
        }

        private void NotifyESD()
        {
            MessageBox.Show(
                "ESD format is not supported. Please do not rename the extension to .wim as it still will NOT work.\r\n\r\nWinToolkit 2.x will support ESD files.",
                "Not Supported");
        }

        private void cmdBrowseWIM_Click(object sender, EventArgs e)
        {
            if (lstImages.Items.Cast<ListViewItem>().Any(I => I.Font.Bold && I.Group.Header != "Already Mounted Images"))
            {
                MessageBox.Show("An image is still mounted, please unmount first!", "Mounted image");
                return;
            }

            var OFD = new OpenFileDialog();
            OFD.Title = "Select WIM File...";
            OFD.Filter = "Windows Image *.wim|*.wim";

            if (OFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (OFD.FileName.EndsWithIgnoreCase(".esd"))
            {
                NotifyESD();
                return;
            }

            string DVDP = OFD.FileName.ReplaceIgnoreCase("\\\\", "\\");
            if (CheckDVD(DVDP) || cMain.IsReadOnly(Path.GetDirectoryName(DVDP)))
            {
                MessageBox.Show(
                    "The specified WIM is currently on a read-only device. Please copy it to your hard drive and try again.",
                    "Read-only");
                return;
            }

            LoadPWIM(OFD.FileName, false);

            try
            {
                string EMB = OFD.FileName;
                if (EMB.ContainsIgnoreCase("\\SOURCES\\"))
                {
                    while (!EMB.EndsWithIgnoreCase("\\"))
                    {
                        EMB = EMB.Substring(0, EMB.Length - 1);
                    }
                    EMB = EMB + "emb\\background_emb.bmp";
                    if (File.Exists(EMB))
                    {
                        DialogResult DR =
                             MessageBox.Show(
                                  "Win Toolkit has notice that you have selected an 'Windows Embedded' image, Win Toolkit does not fully support these types of images." +
                                  Environment.NewLine + Environment.NewLine + "Continue Anyway?",
                                  "Windows Thin PC Detected!", MessageBoxButtons.YesNo);
                        if (DR != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                }
            }
            catch
            {
            }



            TaskInProgress();
            cMain.UpdateToolStripLabel(lblWIM, "Loading WIM...");
            Application.DoEvents();
            cOptions.LastWim = DVDP;
            lblWIM.Text = DVDP;
            lstImages.Items.Clear();
            Images.Clear();
            cOptions.SaveSettings();
            RunWorker();
        }

        private void mnuBrowse_ButtonClick(object sender, EventArgs e)
        {
            mnuBrowse.ShowDropDown();
            mnuDVD1.Enabled = File.Exists(mnuDVD1.Text);
            mnuDVD2.Enabled = File.Exists(mnuDVD2.Text);
            mnuDVD3.Enabled = File.Exists(mnuDVD3.Text);
            mnuDVD4.Enabled = File.Exists(mnuDVD4.Text);
            mnuDVD5.Enabled = File.Exists(mnuDVD5.Text);
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            cOptions.LastWim = lblWIM.Text;
            lstImages.Items.Clear();
            Images.Clear();
            cOptions.SaveSettings();
            RunWorker();
        }

        private void cmdSBoot_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblWIM.Text))
            {
                return;
            }
            cmdSBoot.Enabled = false;

            if (lblWIM.Text.ToUpper().EndsWithIgnoreCase("\\INSTALL.WIM") || lblWIM.Text.ToUpper().EndsWithIgnoreCase("\\BOOT.WIM"))
            {
                if (lblWIM.Text.ToUpper().EndsWithIgnoreCase("\\INSTALL.WIM"))
                {
                    string I = lblWIM.Text.ReplaceIgnoreCase("install.wim", "boot.wim");
                    if (File.Exists(I))
                    {
                        cOptions.LastWim = I;
                    }
                }

                if (lblWIM.Text.ToUpper().EndsWithIgnoreCase("\\BOOT.WIM"))
                {
                    string B = lblWIM.Text.ReplaceIgnoreCase("boot.wim", "install.wim");
                    if (File.Exists(B))
                    {
                        cOptions.LastWim = B;
                    }
                }

            }

            lstImages.Items.Clear();
            Images.Clear();
            cOptions.SaveSettings();
            RunWorker();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
        }

        private void cmdOpenMount_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LST in lstImages.SelectedItems) { if (LST.Font.Bold) { cMain.OpenExplorer(this, LST.SubItems[7].Text); } }
        }

        private void mnuDVD1_Click(object sender, EventArgs e)
        {
            LoadPWIM(mnuDVD1.Text, true);
        }

        private void mnuDVD2_Click(object sender, EventArgs e)
        {
            LoadPWIM(mnuDVD2.Text, true);
        }

        private void mnuDVD3_Click(object sender, EventArgs e)
        {
            LoadPWIM(mnuDVD3.Text, true);
        }

        private void mnuDVD4_Click(object sender, EventArgs e)
        {
            LoadPWIM(mnuDVD4.Text, true);
        }

        private void mnuDVD5_Click(object sender, EventArgs e)
        {
            LoadPWIM(mnuDVD5.Text, true);
        }

        private void LoadPWIM(string WIM, bool Load)
        {
            if (!string.IsNullOrEmpty(WIM) && WIM != "N/A" && File.Exists(WIM))
            {
                if (DVDs.Contains(WIM))
                {
                    DVDs.Remove(WIM);
                }
                DVDs.Insert(0, WIM);
                LoadDVDMenu();

                if (Load)
                {
                    cOptions.LastWim = WIM;
                    lblWIM.Text = WIM;

                    lstImages.Items.Clear();
                    Images.Clear();
                    cOptions.SaveSettings();
                    RunWorker();
                }
            }
        }

        private void RunWorker()
        {
            while (BWR.IsBusy) { Thread.Sleep(100); Application.DoEvents(); }
            lock (BWR)
            {
                BWR.RunWorkerAsync();
            }

        }

        private void cmdSelectAll86_Click(object sender, EventArgs e)
        {
            lstImages.SelectedItems.Clear();
            foreach (ListViewItem LST in lstImages.Items)
            {
                LST.Selected = LST.SubItems[2].Text.EqualsIgnoreCase("x86");
            }
            Selectimage();
        }

        private void cmdSelectAll64_Click(object sender, EventArgs e)
        {
            lstImages.SelectedItems.Clear();
            foreach (ListViewItem LST in lstImages.Items)
            {
                LST.Selected = LST.SubItems[2].Text.EqualsIgnoreCase("x64");
            }
            Selectimage();
        }


        private void mnuEditDName_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please select 1 image!", "Invalid Selected");
                return;
            }

            ListViewItem selected = lstImages.SelectedItems[0];
            WIMImage sImage = (WIMImage)selected.Tag;

            string Answer = cMain.InputBox("Please enter a new display name for the image", "Display Name", sImage.DisplayName);
            if (Answer == sImage.DisplayName || string.IsNullOrEmpty(Answer)) { return; }


            if (!File.Exists(sImage.Location))
            {
                MessageBox.Show("Win Toolkit can't find the following file:" + Environment.NewLine + Environment.NewLine + sImage.Location, "Aborted - File Missing");
                return;
            }

            _lastImageSelected = lstImages.SelectedItems[0].Index;
            string W = lblWIM.Text;

            cMain.UpdateToolStripLabel(lblWIM, "Renaming...");
            Application.DoEvents();
            sImage.DisplayName = Answer;

            cMain.UpdateToolStripLabel(lblWIM, "Finished...");
            Application.DoEvents();
            lblWIM.Text = W;

            lstImages.Items.Clear();
            Images.Clear();
            Application.DoEvents();
            cMain.FreeRAM();
            RunWorker();
            TaskInProgress();
        }

        private void mnuEditDDesc_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please select 1 image!", "Invalid Selected");
                return;
            }

            ListViewItem selected = lstImages.SelectedItems[0];
            WIMImage sImage = (WIMImage)selected.Tag;

            string Answer = cMain.InputBox("Please enter a new display description for the image", "Display Description", sImage.DisplayDescription);
            if (Answer == sImage.DisplayDescription || string.IsNullOrEmpty(Answer)) { return; }


            if (!File.Exists(sImage.Location))
            {
                MessageBox.Show("Win Toolkit can't find the following file:" + Environment.NewLine + Environment.NewLine + sImage.Location, "Aborted - File Missing");
                return;
            }

            _lastImageSelected = lstImages.SelectedItems[0].Index;
            string W = lblWIM.Text;

            cMain.UpdateToolStripLabel(lblWIM, "Renaming...");
            Application.DoEvents();
            sImage.DisplayDescription = Answer;

            lstImages.Items.Clear();
            Images.Clear();
            Application.DoEvents();
            cMain.FreeRAM();
            RunWorker();
            TaskInProgress();
        }

        private void cmdBrowseISO_Click(object sender, EventArgs e)
        {
            if (lstImages.Items.Cast<ListViewItem>().Any(I => I.Font.Bold && I.Group.Header != "Already Mounted Images"))
            {
                MessageBox.Show("An image is still mounted, please unmount first!", "Mounted image");
                return;
            }

            var OFD = new OpenFileDialog();
            OFD.Title = "Select ISO File...";
            OFD.Filter = "Windows DVD Image *.iso|*.iso";
            OFD.InitialDirectory = cOptions.LastISO_Folder;

            if (OFD.ShowDialog() != DialogResult.OK) { return; }

            if (OFD.FileName.ContainsForeignCharacters())
            {
                MessageBox.Show("The ISO you have selected contains non-English characters. Unfortunately, this affects the 7z extraction. Please remove non-English characters and try again. For example:\n\nX:\\Win7.iso\nX:\\DVD\\Win7SP1.iso", "Aborted");
                return;
            }

            bool fExisted = false;
            string fExtractTo = Path.GetDirectoryName(OFD.FileName) + "\\" + Path.GetFileNameWithoutExtension(OFD.FileName);
            if (Directory.Exists(fExtractTo))
            {
                fExisted = true;
            }
            else
            {
                try
                {
                    cMain.CreateDirectory(fExtractTo);
                }
                catch (Exception)
                {
                    fExtractTo = cMain.SysDrive + "\\" + Path.GetFileNameWithoutExtension(OFD.FileName);
                    cMain.CreateDirectory(fExtractTo);
                }

            }
            string SP = cMain.FolderBrowserVista("Select folder to extract ISO", true, true, fExtractTo);
            if (SP.ToUpper() != fExtractTo.ToUpper() && fExisted == false) { Files.DeleteFolder(fExtractTo, false); }
            if (string.IsNullOrEmpty(SP)) { return; }


            if (cMain.IsNetworkPath(SP))
            {
                MessageBox.Show("Can't copy to network. This will make the ISO processing slower.", "Not Supported");
                return;
            }

            if (cMain.IsReadOnly(SP))
            {
                if (!fExisted) { Files.DeleteFolder(SP, false); }
                MessageBox.Show("You are trying to save the ISO to a CD/DVD which is read-only!", "Aborted");
                return;
            }

            if (SP.ContainsForeignCharacters())
            {
                if (!fExisted) { Files.DeleteFolder(SP, false); }
                MessageBox.Show("The extraction path you have selected contains non-English characters. Unfortunately, this affects the 7z extraction. Please remove non-English characters and try again. For example:\n\nX:\\Win7ISO\nX:\\DVD\\WIN7", "Aborted");
                return;
            }

            long size = long.Parse(cMain.GetSize(OFD.FileName, false));

            if (new DriveInfo(SP).AvailableFreeSpace < (size + 104857600)) // 100MB
            {
                if (!fExisted) { Files.DeleteFolder(SP, false); }
                MessageBox.Show("Not enough free space. " + cMain.BytesToString(size) + " needed.", "Not enough room");
                return;
            }

            string OLBL = lblWIM.Text;
            cMain.UpdateToolStripLabel(lblWIM, "Extracting ISO: [" + OFD.FileName + "]");
            Application.DoEvents();

            if (!cMain.IsEmpty(SP)) { SP += "\\Extracted_ISO"; }

            FBusy = true;
            mnuTools.Visible = false;
            mnuBrowse.Visible = false;

            Thread newThread = new Thread(() =>
            {
                cMain.ExtractFiles(OFD.FileName, SP, this);
            });

            newThread.Start();
            while (newThread.IsAlive)
            {
                Thread.Sleep(250);
                Application.DoEvents();
            }


            if (File.Exists(SP + "\\sources\\install.esd"))
            {
                NotifyESD();
                return;
            }

            if (File.Exists(SP + "\\sources\\install.wim"))
            {
                LoadPWIM(SP + "\\sources\\install.wim", true);
            }
            else
            {
                FBusy = false;
                mnuTools.Visible = true;
                mnuBrowse.Visible = true;
                MessageBox.Show("This does not seem to be the correct ISO. The following file is missing:\n\n[" + SP + "\\sources\\install.wim]", "Invalid ISO");
                if (fExisted == false) { Files.DeleteFolder(fExtractTo, false); }
                lblWIM.Text = OLBL;
            }
        }

        private void cmdEditFlag_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please select 1 image!", "Invalid Selected");
                return;
            }

            string F = lstImages.SelectedItems[0].SubItems[6].Text;
            if (!File.Exists(F))
            {
                MessageBox.Show("Win Toolkit can't find the following file:" + Environment.NewLine + Environment.NewLine + F, "Aborted - File Missing");
                return;
            }
            string W = lblWIM.Text;
            string S = cMount.CWIM_GetWimImageInfo(W, lblWIM);
            string T = "";


            var frmPrompt = new frmTweaks();
            frmPrompt.Text = "Select Flag";

            string sWinVer = lstImages.SelectedItems[0].SubItems[3].Text;

            if (sWinVer.ContainsIgnoreCase("(920") || sWinVer.ContainsIgnoreCase("(960"))
            {
                frmPrompt.cboRChoice.Items.Add("Core");
                frmPrompt.cboRChoice.Items.Add("Professional");
                frmPrompt.cboRChoice.Items.Add("ProfessionalWMC");
            }
            else if (sWinVer.StartsWith("10.0"))
            {
                frmPrompt.cboRChoice.Items.Add("Core");
                frmPrompt.cboRChoice.Items.Add("CoreSingleLanguage");
                frmPrompt.cboRChoice.Items.Add("Professional");
                frmPrompt.cboRChoice.Items.Add("ProfessionalEducation");
                frmPrompt.cboRChoice.Items.Add("ProfessionalWorkstation");
                frmPrompt.cboRChoice.Items.Add("Education");
                frmPrompt.cboRChoice.Items.Add("ProfessionalCountrySpecific");
                frmPrompt.cboRChoice.Items.Add("ProfessionalSingleLanguage");
                frmPrompt.cboRChoice.Items.Add("ServerRdsh");
                frmPrompt.cboRChoice.Items.Add("Enterprise");

            }
            else
            {
                frmPrompt.cboRChoice.Items.Add("Starter");
                frmPrompt.cboRChoice.Items.Add("HomeBasic");
                frmPrompt.cboRChoice.Items.Add("HomePremium");
                frmPrompt.cboRChoice.Items.Add("Professional");
                frmPrompt.cboRChoice.Items.Add("Ultimate");
                frmPrompt.cboRChoice.Items.Add("Business");
                frmPrompt.cboRChoice.Items.Add("Enterprise");
                frmPrompt.cboRChoice.Items.Add("ServerDatacenter");
                frmPrompt.cboRChoice.Items.Add("ServerEnterprise");
                frmPrompt.cboRChoice.Items.Add("ServerStandard");
            }
            frmPrompt.cboRChoice.Visible = true;
            
            foreach (ListViewItem LST in lstImages.SelectedItems)
            {
                lblWIM.Text = W;
                if (!frmPrompt.cboRChoice.Items.Contains(LST.SubItems[10].Text))
                {
                    frmPrompt.cboRChoice.Items.Add(LST.SubItems[10].Text);
                }
                frmPrompt.cboRChoice.Text = LST.SubItems[10].Text;
                cMain.TweakChoice = "";
                frmPrompt.ShowDialog();
                string Answer = cMain.TweakChoice;

                if (Answer == LST.SubItems[10].Text || string.IsNullOrEmpty(Answer)) { return; }

                T = cMount.RenameImage(S, "FLAGS", int.Parse(LST.SubItems[5].Text), LST.SubItems[10].Text, Answer.ToUpper());
                LST.SubItems[10].Text = Answer;
                LST.ToolTipText = cMain.UpdateTooltips(LST.Text, LST.SubItems[1].Text, LST.SubItems[8].Text, LST.SubItems[9].Text, LST.SubItems[10].Text);
            }
            cMain.UpdateToolStripLabel(lblWIM, "Renaming...");
            Application.DoEvents();

            cMount.CWIM_SetWimInfo(W, T);
            cMain.UpdateToolStripLabel(lblWIM, "Finished...");
            Application.DoEvents();
            lblWIM.Text = W;

            lstImages.Items.Clear();
            Images.Clear();
            Application.DoEvents();
            cMain.FreeRAM();
            RunWorker();
            TaskInProgress();
        }

        string sChosen = "";
        string sChosenName = "";
        private void cmdUpgradeImage_Click(object sender, EventArgs e)
        {
            sChosen = "";
            sChosenName = "";
            if (lstImages.SelectedItems.Count != 1)
            {
                MessageBox.Show("You have to selected one image to upgrade", "Invalid Selected");
                return;
            }
            ListViewItem sItem = lstImages.SelectedItems[0];

            if (sItem.Group.Header.EqualsIgnoreCase("Custom"))
            {
                MessageBox.Show("Custom images do not support upgrades.", "Sorry");
                return;
            }
            string iName = "Windows";
            if (sItem.SubItems[3].Text.ContainsIgnoreCase("760")) { iName = "Windows 7"; }
            if (sItem.SubItems[3].Text.ContainsIgnoreCase("920")) { iName = "Windows 8"; }
            if (sItem.SubItems[3].Text.ContainsIgnoreCase("960")) { iName = "Windows 8.1"; }
            using (frmUpgradeImage fUI = new frmUpgradeImage())
            {
                fUI.Text += " [" + sItem.SubItems[10].Text + "]";
                fUI.sName = iName;
                fUI.sArc = sItem.SubItems[2].Text;

                foreach (ListViewItem LST in lstImages.Items)
                {
                    fUI.sImageName.Add(LST.Text);
                }
                if (sItem.SubItems[3].Text.ContainsIgnoreCase("960") || sItem.SubItems[3].Text.ContainsIgnoreCase("920") || sItem.SubItems[3].Text.ContainsIgnoreCase("760") || sItem.SubItems[3].Text.StartsWith("10.0."))
                {
                    //Windows 10
                    if (sItem.SubItems[3].Text.StartsWith("10.0."))
                    {
                        switch (sItem.SubItems[10].Text.ToUpper())
                        {
                            case "CORE":
                                fUI.cboUpgrade.Items.Add("Professional");
                                fUI.cboUpgrade.Items.Add("Education");
                                break;
                            case "PROFESSIONAL":
                                fUI.cboUpgrade.Items.Add("Education");
                                break;
                            default:
                                MessageBox.Show("This image can't be upgraded any higher!", "Aborting");
                                return;
                        }
                    }

                    //Windows 8
                    if (sItem.SubItems[3].Text.ContainsIgnoreCase("920") || sItem.SubItems[3].Text.ContainsIgnoreCase("960"))
                    {
                        switch (sItem.SubItems[10].Text.ToUpper())
                        {
                            case "CORECONNECTED":
                                fUI.cboUpgrade.Items.Add("Core");
                                fUI.cboUpgrade.Items.Add("Professional");
                                fUI.cboUpgrade.Items.Add("ProfessionalWMC");
                                break;
                            case "CORE":
                                fUI.cboUpgrade.Items.Add("Professional");
                                fUI.cboUpgrade.Items.Add("ProfessionalWMC");
                                break;
                            case "PROFESSIONAL":
                                fUI.cboUpgrade.Items.Add("ProfessionalWMC");
                                break;
                            default:
                                MessageBox.Show("This image can't be upgraded any higher!", "Aborting");
                                return;
                        }
                    }

                    //Windows 7
                    if (sItem.SubItems[3].Text.ContainsIgnoreCase("760"))
                    {
                        switch (sItem.SubItems[10].Text.ToUpper())
                        {
                            case "STARTER":
                                fUI.cboUpgrade.Items.Add("HomeBasic");
                                fUI.cboUpgrade.Items.Add("HomePremium");
                                fUI.cboUpgrade.Items.Add("Professional");
                                fUI.cboUpgrade.Items.Add("Ultimate");
                                break;
                            case "HOMEBASIC":
                            case "BASIC":
                                fUI.cboUpgrade.Items.Add("HomePremium");
                                fUI.cboUpgrade.Items.Add("Professional");
                                fUI.cboUpgrade.Items.Add("Ultimate");
                                break;
                            case "HOMEPREMIUM":
                                fUI.cboUpgrade.Items.Add("Professional");
                                fUI.cboUpgrade.Items.Add("Ultimate");
                                break;
                            case "PROFESSIONAL":
                                fUI.cboUpgrade.Items.Add("Ultimate");
                                break;
                            case "SERVERSTANDARD":
                                fUI.cboUpgrade.Items.Add("ServerDatacenter");
                                fUI.cboUpgrade.Items.Add("ServerEnterprise");
                                break;
                            case "SERVERENTERPRISE":
                                fUI.cboUpgrade.Items.Add("ServerDatacenter");
                                break;
                            default:
                                MessageBox.Show("This image can't be upgraded any higher!", "Aborting");
                                return;

                        }
                    }

                }
                else
                {
                    MessageBox.Show("Upgrade option is not available for this version of Windows", "Invalid OS");
                    return;
                }
                fUI.cboUpgrade.SelectedIndex = 0;
                fUI.ShowDialog();

                sChosen = fUI.cboUpgrade.Text;
                sChosenName = fUI.txtImageName.Text;
                if (!fUI.bSelected) { return; }

                TaskInProgress();

                bwUpgrade.RunWorkerAsync();

            }

        }

        private void bwUpgrade_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cMain.selectedImages.Clear();

            cMain.UpdateToolStripLabel(lblWIM, "Please select a WIM File...");
            cmdRefresh.Enabled = false;
            RunWorker();
            cMain.UpdateToolStripLabel(lblWIM, cOptions.LastWim);
            TaskInProgress(true);
        }
        private void bwUpgrade_DoWork(object sender, DoWorkEventArgs e)
        {
            ListViewItem sItem = lstImages.SelectedItems[0];

            string sWIMFile = sItem.SubItems[6].Text;
            cMain.UpdateToolStripLabel(lblWIM, "Mounting WIM...");
            string sMountPath = cMount.CWIM_MountImage(sItem.Text, Convert.ToInt16(sItem.SubItems[5].Text), sWIMFile, sItem.SubItems[7].Text, lblWIM, this);

            if (string.IsNullOrEmpty(sMountPath))
            {
                MessageBox.Show("Image could not be mounted.", "Unknown Error");
                return;
            }

            cMain.UpdateToolStripLabel(lblWIM, "Upgrading Image [" + sItem.SubItems[10].Text + " -> " + sChosen + "]...");
            string sUpgrade = cMain.RunExternal(DISM.Latest.Location, "/Image:\"" + sMountPath + "\" /Set-Edition:\"" + sChosen + "\" /English");
            bool bUpgraded = true;
            if (sUpgrade.ContainsIgnoreCase("Error: "))
            {
                bUpgraded = false;
                cMain.UpdateToolStripLabel(lblWIM, "Error :: Upgrading Image [" + sItem.SubItems[10].Text + " -> " + sChosen + "]...");
                cMain.ErrorBox("An unknown error caused the image not to upgrade. :(", "Unknown Upgrade Error", sUpgrade);
            }

            cMain.UpdateToolStripLabel(lblWIM, "Unmounting WIM...");
            bool bUnmount = false;
            if (bUpgraded)
            {
                bUnmount = cMount.CWIM_UnmountImage(sChosenName, lblWIM, false, true, false, this, sWIMFile, true);
            }
            else
            {
                bUnmount = cMount.CWIM_UnmountImage(sItem.Text, lblWIM, false, true, false, this, sWIMFile, true);
            }

            if (bUpgraded && bUnmount && cMount.uChoice == cMount.MountStatus.Save)
            {
                cMain.UpdateToolStripLabel(lblWIM, "Renaming Image...");
                string S = cMount.CWIM_GetWimImageInfo(sWIMFile, lblWIM);
                S = cMount.RenameImage(S, "NAME", int.Parse(sItem.SubItems[5].Text), sItem.Text, sChosenName);
                S = cMount.RenameImage(S, "DESCRIPTION", int.Parse(sItem.SubItems[5].Text), sItem.SubItems[1].Text, sChosenName);
                S = cMount.RenameImage(S, "DISPLAYNAME", int.Parse(sItem.SubItems[5].Text), sItem.SubItems[8].Text, sChosenName);
                S = cMount.RenameImage(S, "DISPLAYDESCRIPTION", int.Parse(sItem.SubItems[5].Text), sItem.SubItems[9].Text, sChosenName);
                S = cMount.RenameImage(S, "FLAGS", int.Parse(sItem.SubItems[5].Text), sItem.SubItems[10].Text, sChosen);

                if (!string.IsNullOrEmpty(S)) { cMount.CWIM_SetWimInfo(sWIMFile, S); }
            }

        }

        private void cmdCleanMGR_Click(object sender, EventArgs e)
        {
            if (lstImages.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please select one image which may require cleaning.", "Invalid Image!");
                return;
            }

            WIMImage wimFile = lstImages.SelectedItems[0].Tag as WIMImage;

            new frmCleanup(wimFile.MountPath).ShowDialog();
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

    }
}