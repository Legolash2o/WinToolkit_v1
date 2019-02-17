using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Prompts;
using WinToolkit.Properties;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;

namespace WinToolkit
{
    public partial class frmAllInOne : Form
    {
        private bool XPMode;

        private string[] fAddons;
        private readonly TabControl NewTC = new TabControl();
        private readonly TabPage NewTab = new TabPage();

        private readonly TabPage NewTabAddons = new TabPage();
        private readonly TabPage NewTabDrivers = new TabPage();
        private readonly TabPage NewTabGadgets = new TabPage();
        private readonly TabPage NewTabSilent = new TabPage();
        private readonly TabPage NewTabThemes = new TabPage();
        private readonly TabPage NewTabUpdate = new TabPage();
        private readonly TabPage NewTabWallpaper = new TabPage();

        private readonly string PEBackground = cMain.selectedImages[0].Location.ReplaceIgnoreCase("INSTALL.WIM", "background_cli.bmp");

        private readonly ListView lstIAddons = new ListView();
        private readonly ListView lstIDrivers = new ListView();
        private readonly ListView lstIGadgets = new ListView();
        private readonly ListView lstISilent = new ListView();
        private readonly ListView lstIThemes = new ListView();
        private readonly ListView lstIUpdates = new ListView();
        private readonly ListView lstIWallpapers = new ListView();
        private string DVD = "";
        private ListView LV;
        private string LangINI = "";
        private string SP = "";
        private TabPage SelTab = default(TabPage);
        private int UC;
        private string cLoad;
        private string dErr = "";
        private bool iAdding, AVNotice;
        private bool lAIO;
        private bool iBusy;
        private bool starting;
        private bool tSB;
        private string uError = "";
        private string tSBCT = "", lPreset = "";
        private string aErr = ""; private int nErr;
        private string runTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-tt", System.Globalization.CultureInfo.InvariantCulture);
        private readonly List<TreeNode> tvList = new List<TreeNode>();
        private List<ListViewItem> badUpdates = new List<ListViewItem>();
        private static WIMImage currentlyMounted = null;
        private static cComponents ComponentClass;

        bool bAbort = false;

        public frmAllInOne()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            CheckForIllegalCrossThreadCalls = false;
            FormClosing += frmAllInOne_FormClosing;
            FormClosed += frmAllInOne_FormClosed;

            Resize += frmAllInOne_Resize;
            BWAddAddons.RunWorkerCompleted += BWAddAddons_RunWorkerCompleted;
            BWRun.RunWorkerCompleted += BWRun_RunWorkerCompleted;
            Activated += cMain.FormActivated;
            Deactivate += cMain.FormActivated;
            TPMain.SelectedIndexChanged += TabChanged;
            TPBasic.SelectedIndexChanged += TabChanged;
            TPAdvanced.SelectedIndexChanged += TabChanged;
            MouseWheel += MouseScroll;
            tvComponents.AfterCheck += tvPreset_AfterCheck;
            tvTweaks.AfterCheck += tvPreset_AfterCheck;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        //HIDE TREENODE CHILD CHECKBOX

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private const int TVIF_STATE = 0x8;
        private const int TVIS_STATEIMAGEMASK = 0xF000;
        private const int TV_FIRST = 0x1100;
        private const int TVM_SETITEM = TV_FIRST + 63;

        private void HideCheckBox(TreeView tvw, TreeNode node)
        {
            try
            {
                var tvi = new TVITEM();
                tvi.hItem = node.Handle;
                tvi.mask = TVIF_STATE;
                tvi.stateMask = TVIS_STATEIMAGEMASK;
                tvi.state = 0;
                IntPtr lparam = Marshal.AllocHGlobal(Marshal.SizeOf(tvi));
                Marshal.StructureToPtr(tvi, lparam, false);

                SendMessage(tvw.Handle, TVM_SETITEM, IntPtr.Zero, lparam);
            }
            catch
            {
            }
        }

        //------------------------------------------

        private void CheckListEmpty(ListView ListName)
        {
            if (ListName.Items.Count == 0)
            {
                mnuR.Visible = false;
                cmdDefaultTheme.Visible = false;
            }
            else
            {
                mnuR.Visible = true;
            }
            EnablePan();
        }

        private void frmAllInOne_Resize(object sender, EventArgs e)
        {
            gbCInfo.Width = fpComponents.Width - 17;
            cMain.CenterObject(PanArrange);
        }

        private void SetTips()
        {
            try
            {
                cMain.SetToolTip(cmdSIUp, "This will move the selected item up once.", "Move Up");
                cMain.SetToolTip(cmdSITop, "This will move the selected item up to the top.", "Move Top");

                cMain.SetToolTip(cmdSIBottom, "This will move the selected item down to the bottom.", "Move Bottom");
                cMain.SetToolTip(cmdAdd, "This will allow you to add items to the selected list.", "Add Items");
                cMain.SetToolTip(cmdDefaultT,
                                             "This will make the selected theme default and will be shown after installation.",
                                             cmdDefaultTheme.Text);
                cMain.SetToolTip(cmdRemove, "This will remove the selected item from the list.", "Remove Item");
                cMain.SetToolTip(cmdSIDown, "This will move the selected item down once.", "Move Down");
                cMain.SetToolTip(cmdSBRestore,
                                             "This will undo all changes to the services list and restore them to their default settings.",
                                             "Restore to Default");
                cMain.SetToolTip(cmdSBTweaked,
                                             "This will disable services which people do not use to speed up their computer." +
                                             Environment.NewLine + Environment.NewLine +
                                             "For more information click the 'i' button below.", "BlackViper 'Tweaked'");
                cMain.SetToolTip(cmdSBSafe,
                                             "This will disable services which people never use and is the safest option to use." +
                                             Environment.NewLine + Environment.NewLine +
                                             "For more information click the 'i' button below.", "BlackViper 'Safe'");
                cMain.SetToolTip(cmdSBBone,
                                             "This disables quite a lot of services to have minimum services running and best for performance." +
                                             Environment.NewLine + "I would only recommend this for advanced users!" +
                                             Environment.NewLine + Environment.NewLine +
                                             "For more information click the 'i' button below.", "BlackViper 'Bare Bone'	");
                cMain.SetToolTip(cmdSBCheckAll, "This will tick all the items in the list.", "Check All");
                cMain.SetToolTip(cmdSBUnCheckAll, "This will untick everything in the list.", "Uncheck All");
                cMain.SetToolTip(cmdSBODefault, "This will restore the options to their default setting.",
                                             "Restore Default");
                cMain.SetToolTip(cmdSBSInfo,
                                             "This will show more information about services and what they do. It also" +
                                             Environment.NewLine + "will show more information about the preset buttons.",
                                             "BlackViper Info");

                //Silent Installers
                cMain.SetToolTip(txtSIName,
                                             "When Windows has finished installing and starts to install these programs" +
                                             Environment.NewLine + "this is what you will see i.e. 'Installing Office 2010..."
                                             + Environment.NewLine + Environment.NewLine +
                                             "This box can't contain any of the following characters: \\ / : * ? \" < > |", "Silent Installer Display Name");
                cMain.SetToolTip(txtSISwitch,
                                             "This is information like '/silent', '/s', '/q', etc.., however if your exe is already silent then just leave this blank.",
                                             "Install Switch/Syntax");
                cMain.SetToolTip(cmdCommon, "Shows a list of switches for common programs.", "Common Switches");
            }
            catch
            {
            }
        }

        private void MouseScroll(object sender, MouseEventArgs e)
        {
            string TabName = TPMain.SelectedTab.Text;
            if (TabName.ContainsIgnoreCase("["))
            {
                while (TabName.ContainsIgnoreCase("[")) { TabName = TabName.Substring(0, TabName.Length - 1); }
                TabName = TabName.Substring(0, TabName.Length - 1);
            }
            switch (TabName)
            {
                case "Basic":
                    if (TPBasic.SelectedTab == TabAddons) { lstAddons.Select(); }
                    if (TPBasic.SelectedTab == TabDrivers) { lstDrivers.Select(); }
                    if (TPBasic.SelectedTab == TabGadgets) { lstGadgets.Select(); }
                    if (TPBasic.SelectedTab == TabThemes) { lstThemes.Select(); }
                    if (TPBasic.SelectedTab == TabUpdates) { lstUpdates.Select(); }
                    if (TPBasic.SelectedTab == TabWallpapers) { lstWallpapers.Select(); }
                    break;
                case "Advanced":
                    if (TPAdvanced.SelectedTab == tabComponents) { lstComponents.Select(); }
                    if (TPAdvanced.SelectedTab == tabComponents2) { tvComponents.Select(); }
                    if (TPAdvanced.SelectedTab == tabFiles) { lstFiles.Select(); }
                    if (TPAdvanced.SelectedTab == tabServices) { lstServices.Select(); }
                    if (TPAdvanced.SelectedTab == tabSilent) { lstSilent.Select(); }
                    if (TPAdvanced.SelectedTab == TabTweaks) { tvTweaks.Select(); }
                    break;
                case "Options":
                    lstOptions.Select();
                    break;
            }
        }

        private void LoadPresetManager()
        {
            frmAIOPresetManager FP = new frmAIOPresetManager();
            FP.Height = this.Height - 100;
            FP.Width = this.Width - 100;

            FP.ShowDialog();

            if (FP.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                iBusy = true;
                LoadPreset(FP.sSelectedFile, null, FP.sSelectedFilter);
                iBusy = false;
            }
            cMain.FreeRAM();
        }


        public class UpdateInfo
        {
            public string PackageName = "";
            public string PackageVersion = "";
            public WinToolkit.Classes.UpdateCache.UpdateType UpdateType = UpdateCache.UpdateType.Unknown;
            public List<Information> Info = new List<Information>();

            public class Information
            {
                public Information(string imageName, int imageIndex)
                {
                    ImageName = imageName;
                    ImageIndex = imageIndex;

                }
                public string ImageName;
                public int ImageIndex;
                public int StatusIndex;
                public string DISM = "";
                public string Note = "";
                public DateTime Date = DateTime.Now;
            }
        }

        private void frmAllInOne_Shown(object sender, EventArgs e)
        {

            lstUpdates.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lstOptions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            if (cOptions.PresetManager)
            {
                LoadPresetManager();
            }
            lAIO = false;
            lstServices.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstServices.Columns[1].Width = -2;
        }

        private void frmAllInOne_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            cMain.eForm = this;
            splitContainer3.Panel1MinSize = PanArrange.Width + 5;
            SplitContainer2.Scale4K(_4KHelper.Panel.Pan1);
            SplitContainer1.Scale4K(_4KHelper.Panel.Pan2);
            scComponents.Scale4K(_4KHelper.Panel.Pan1);
            scCompMain.Scale4K(_4KHelper.Panel.Pan1);
            scOptions.Scale4K(_4KHelper.Panel.Pan1);

            lAIO = true;
            //mnuA.ToolTipText = "Add any *.WA files you may have so that your favorite programs are pre-installed after Windows installation.";
            ListViewEx.LVE = lstAddons;

            CenterToScreen();

            SetTips();
            var grades = new StringCollection();
            grades.AddRange(new[] { "Automatic (Delayed Start)", "Automatic", "Manual", "Disabled" });

            var grades_silent = new StringCollection();
            grades_silent.AddRange(new[] { "YES", "NO" });
            foreach (ListViewItem LST in lstServices.Items)
            {
                lstServices.AddComboBoxCell(-1, 1, grades);
            }
            lstSilent.AddEditableCell(-1, 1);
            lstSilent.AddEditableCell(-1, 0);
            lstSilent.AddComboBoxCell(-1, 2, grades_silent);

            cMain.UpdateToolStripLabel(lblStatus, "Detecting DVD Folder...");

            DVD = cMain.selectedImages[0].Location;

            if (DVD.ContainsIgnoreCase("\\SOURCES\\"))
            {
                try
                {
                    DVD = cMain.GetDVD(DVD);

                    cMain.UpdateToolStripLabel(lblStatus, "DVD Folder Detected: " + DVD);

                    try
                    {
                        if (lstComponents.FindItemWithText("Delete 'Support' folder") != null)
                        {
                            if (Directory.Exists(DVD + "Support"))
                            {
                                lstComponents.FindItemWithText("Delete 'Support' folder").SubItems[1].Text = DVD +
                                                                                                                                                                        "Support";
                            }
                            else
                            {
                                lstComponents.FindItemWithText("Delete 'Support' folder").Remove();
                            }
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        if (lstComponents.FindItemWithText("Delete 'Upgrade' folder") != null)
                        {
                            if (Directory.Exists(DVD + "Upgrade"))
                            {
                                lstComponents.FindItemWithText("Delete 'Upgrade' folder").SubItems[1].Text = DVD +
                                                                                                                                                                        "Upgrade";
                            }
                            else
                            {
                                lstComponents.FindItemWithText("Delete 'Upgrade' folder").Remove();
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                catch (Exception Ex)
                {
                    DVD = "";
                    TPAdvanced.TabPages.Remove(tabSilent);
                    cMain.UpdateToolStripLabel(lblStatus, "Error detecting DVD folder.");
                    cMain.WriteLog(this, "Error detecting DVD folder: " + cMain.selectedImages[0].Location, Ex.Message,
                                              lblStatus.Text);

                    try
                    {
                        if (lstComponents.FindItemWithText("Delete 'Support' folder") != null)
                        {
                            lstComponents.FindItemWithText("Delete 'Support' folder", false, 0).Remove();
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        if (lstComponents.FindItemWithText("Delete 'Support' folder") != null)
                        {
                            lstComponents.FindItemWithText("Delete 'Upgrade' folder", false, 0).Remove();
                        }
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                cMain.UpdateToolStripLabel(lblStatus, "Unable to detect DVD root...");
                DVD = "";
                TPAdvanced.TabPages.Remove(tabSilent);
            }

            if (string.IsNullOrEmpty(DVD))
            {
                TPAdvanced.TabPages.Remove(tabSilent);

                MessageBox.Show(
                      "A DVD root can't be detected, please make sure your selected wim file is in the 'Sources' folder! This error occurs when you have selected just a wim file." +
                      Environment.NewLine + Environment.NewLine + "[EXAMPLE]" + Environment.NewLine +
                      "'D:\\Sources\\install.wim' or 'D:\\Win7SP1\\Sources\\install.wim'" + Environment.NewLine +
                      Environment.NewLine + "Some features have been disabled!", "WARNING :: No DVD Detected");
            }
            try
            {
                lstComponents.Columns[0].Text = "Name (0)";
                foreach (ColumnHeader CH in lstComponents.Columns)
                {
                    CH.Width = -2;
                }
            }
            catch
            {
            }
            finally
            {
                if (lstComponents.Items.Count == 0)
                {
                    TPAdvanced.TabPages.Remove(tabComponents);
                }
            }

            cMain.FreeRAM();
            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Loading GUI...");
                cMain.FormIcon(this); cMain.eLBL = lblStatus;
                cMain.ToolStripIcons(ToolStrip1);
                cMain.ToolStripIcons(toolStrip2);
                cMain.ToolStripIcons(toolStrip4);

                var toolTip1 = new ToolTip();
                toolTip1.AutoPopDelay = 10000;
                toolTip1.InitialDelay = 100;
                toolTip1.ReshowDelay = 100;
                toolTip1.ShowAlways = true;

                toolTip1.SetToolTip(chkAS,
                                  "If this is enabled then this silent install will be installed automatically without asking the user otherwise if disable then you will be asked which software to install after Windows Installation." +
                                  Environment.NewLine +
                                  "For example: If you want 7zip to be installed on all computers then tick this.");
                toolTip1.SetToolTip(chkCopyF,
                                                  "Selecting this means that all the files and folders in the same folder as the application will get copied." +
                                                  Environment.NewLine +
                                                  "This is only useful for stuff like Office, Visual Studio, etc... where the installer has multiple files!");
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Error loading AIO GUI", Ex.Message, lblStatus.Text);
            }

            try
            {
                SelTab = TabAddons;
                if (cMain.selectedImages.Count == 1)
                    Text = Text + " (" + cMain.selectedImages[0].Name + ")";
                else
                    Text = Text + " (" + cMain.selectedImages.Count + " Images Selected)";
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Error setting AIO Title", Ex.Message, lblStatus.Text);
            }


            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Loading AIO Settings...");


                if (lstOptions.FindItemWithText("Enable CMD") != null) { lstOptions.FindItemWithText("Enable CMD").Checked = cOptions.AICommands; }
                if (lstOptions.FindItemWithText("Log Registry Changes") != null) { lstOptions.FindItemWithText("Log Registry Changes").Checked = cOptions.RegistryLog; }
                UpdateNames(true);

            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Error setting AIO settings", Ex.Message, lblStatus.Text);
            }

            cMain.FreeRAM();

            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Removing x64 .NET Services...");
                if (cMain.selectedImages[0].Architecture == Architecture.x86)
                {
                    lstServices.FindItemWithText("Microsoft .NET Framework NGEN v2.0.50727_X64").Remove();
                    lstServices.FindItemWithText("Microsoft .NET Framework NGEN v4.0.30319_X64").Remove();
                }
            }
            catch
            {
            }

            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Looking for Background_cli.bmp...");
                if (!File.Exists(PEBackground) && !File.Exists(PEBackground.ReplaceIgnoreCase("background_cli.bmp", "boot.wim")))
                {
                    FindNode(tvTweaks, "Change Setup Background").Remove();
                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Unable to remove 'Change Setup Background' entry.", Ex.Message, lblStatus.Text);
            }
            cMain.UpdateToolStripLabel(lblStatus, "Welcome to Win Toolkit All-In-One Integrator...");
            cMain.CenterObject(PanArrange);
            cMain.FreeRAM();

            cboShutdown.SelectedIndex = 0;
            tvComponents.ExpandAll();


        }

        /// <summary>
        /// The error message displayed at the end of AIO Integration 
        /// rather than during.
        /// </summary>
        /// <param name="Desc"></param>
        /// <param name="Ex"></param>
        private void LogErr(string Desc, string Ex)
        {
            nErr += 1;
            aErr += Desc + Environment.NewLine + "Exception:" + Environment.NewLine + Ex + Environment.NewLine + "----------------------------------" + Environment.NewLine;
        }

        private void ChangeProgress()
        {
            try
            {
                if (PB.Value < PB.Maximum && !IsDisposed)
                {
                    PB.Value += 1;
                    Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt32(PB.Value), Convert.ToUInt32(PB.Maximum));
                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Error whilst changing progress", Ex.Message, PB.Value.ToString(CultureInfo.InvariantCulture) + " of " + PB.Maximum.ToString(CultureInfo.InvariantCulture));
            }
        }

        private void ResetProgress(int M = 100)
        {
            try
            {
                PB.Value = 0;
                PB.Maximum = M;
                Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt32(0), Convert.ToUInt32(M));
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Error whilst resetting progress", Ex.Message, PB.Value.ToString(CultureInfo.InvariantCulture) + " of " + PB.Maximum.ToString(CultureInfo.InvariantCulture));
            }
        }

        private void frmAllInOne_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iAdding || BWAddAddons.IsBusy)
            {
                MessageBox.Show("Please wait until all items have been added before closing, this is to prevent errors!", "Please Wait!");
                e.Cancel = true; return;
            }

            if (BWRun.IsBusy || iBusy)
            {
                MessageBox.Show("Your image is being worked on, please wait or press 'Cancel'.", "Warning");
                e.Cancel = true; return;
            }


            if (PanSilent.Visible && !iAdding)
            {
                cmdSICancel.PerformClick();
            }
            cMain.OnFormClosing(this);
            CleanTemp("cInst");

        }

        private void frmAllInOne_FormClosed(object sender, FormClosedEventArgs e)
        {
            cMain.ReturnME();
        }

        private void BWAddAddons_DoWork(object sender, DoWorkEventArgs e)
        {

            int n = 1;
            cMain.UpdateToolStripLabel(lblStatus, "Deleting old temp files (" + cOptions.WinToolkitTemp + "\\AddonT)...");
            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\AddonT", false);
            lstAddons.BeginUpdate();

            foreach (string File in fAddons)
            {
                try
                {
                    if (lstAddons.FindItemWithText(File) == null && System.IO.File.Exists(File))
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "(" + n + "\\" + fAddons.Length + ") Adding - " + File + "...");
                        Application.DoEvents();

                        Addon addon = new Addon(File, cMain.selectedImages[0]);
                        if (addon.Loaded)
                        {
                            ListViewItem NI = new ListViewItem();
                            NI.Text = addon.Name;
                            NI.SubItems.Add(addon.Creator);
                            NI.SubItems.Add(addon.Version);
                            NI.SubItems.Add(addon.Size);
                            NI.SubItems.Add(addon.Architecture.ToString());
                            NI.SubItems.Add(File);
                            NI.SubItems.Add(addon.Description);
                            NI.SubItems.Add(addon.Website);
                            NI.Tag = addon;

                            NI.Group = lstAddons.Groups[0];
                            lstAddons.Items.Add(NI);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    cAddon.AddonError += File + Environment.NewLine + Ex.Message + Environment.NewLine + Environment.NewLine;
                }
                n++;
                if (BWAddAddons.CancellationPending)
                    break;
            }

            lstAddons.EndUpdate();

            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\AddonT", false);
        }

        private static bool CheckIntegration(ListViewItem LST)
        {
            try
            {
                if (LST.Text.EqualsIgnoreCase("Windows XP Mode") && File.Exists(currentlyMounted.MountPath + "\\Program Files\\Windows XP Mode\\Windows XP Mode base.vhd")) { return true; }

                if (!Directory.Exists(currentlyMounted.MountPath + "\\Windows\\servicing\\Packages")) { return false; }

                string uPName = "";
                string uPVersion = "";

                try
                {
                    if (LST.Tag != null)
                    {
                        UpdateInfo updateInfo = (UpdateInfo)LST.Tag;

                        uPName = updateInfo.PackageName;
                        uPVersion = updateInfo.PackageVersion;
                    }
                }
                catch (Exception Ex)
                {
                    new SmallError("Get update tag info", Ex, LST).Upload();
                }


                foreach (string f in Directory.GetFiles(currentlyMounted.MountPath + "\\Windows\\servicing\\Packages"))
                {
                    if (LST.Text.StartsWithIgnoreCase("IE") && LST.Text.ToUpper().EndsWithIgnoreCase("LANGPACK"))
                    {
                        string sIEDirectory = currentlyMounted.MountPath + "\\Program Files\\Internet Explorer\\" + LST.SubItems[2].Text;
                        if (Directory.Exists(sIEDirectory))
                        {
                            if (Directory.GetFiles(sIEDirectory).Length > 0) { return true; }
                        }
                        return false;
                    }
                    if (LST.Text.StartsWithIgnoreCase("Internet Explorer"))
                    {
                        string sIEVersion = LST.Text;
                        while (sIEVersion.ContainsIgnoreCase(" ")) { sIEVersion = sIEVersion.Substring(1); }

                        if (LST.Text.EndsWithIgnoreCase(sIEVersion))
                        {
                            if (f.ContainsIgnoreCase("-INTERNETEXPLORER-PACKAGE~") && f.ContainsIgnoreCase("~" + sIEVersion + ".")) { return true; }
                        }
                    }

                    if (LST.Text.EqualsIgnoreCase("Virtual PC") && f.ContainsIgnoreCase("-VirtualPC-Package~")) { return true; }

                    if (LST.Group.Header.EqualsIgnoreCase("Language Packs") && f.ContainsIgnoreCase(uPName) && f.ContainsIgnoreCase("~" + LST.SubItems[2].Text.ToUpper() + "~"))
                    {
                        return true;
                    }


                    if (!string.IsNullOrEmpty(uPName))
                    {

                        if (f.ContainsIgnoreCase(uPName.ToUpper()) && f.ContainsIgnoreCase(uPVersion.ToUpper()))
                        {
                            if (LST.SubItems[2].Text.EqualsIgnoreCase("ALL") || LST.SubItems[2].Text.ToUpper().EqualsIgnoreCase("NEUTRAL") || f.ContainsIgnoreCase(LST.SubItems[2].Text.ToUpper()))
                            {
                                return true;
                            }
                        }
                    }

                    if (f.ContainsIgnoreCase(LST.Text.ToUpper())) { return true; }
                    if (LST.SubItems[1].Text.StartsWithIgnoreCase("KB") && f.ContainsIgnoreCase(LST.SubItems[1].Text.ToUpper())) { return true; }

                }

            }
            catch (Exception Ex)
            {
                new SmallError("Error checking update integrated", Ex).Upload();
            }

            return false;
        }

        private TreeNode NSearch(TreeNode TN, string Search)
        {
            TreeNode fTN = null;
            foreach (TreeNode T in TN.Nodes)
            {
                if (T.Text.ToUpper() == Search.ToUpper()) { fTN = T; break; }
                if (T.Text.StartsWithIgnoreCase(Search.ToUpper() + " [WIN") && Search.EndsWithIgnoreCase("]")) { fTN = T; break; }
                if (T.Nodes.Count > 0 && fTN == null) { fTN = NSearch(T, Search); }
            }
            return fTN;
        }

        private TreeNode FindNode(TreeView TV, string Search)
        {
            //MessageBox.Show(Search);
            TreeNode TN = null;
            foreach (TreeNode T in TV.Nodes)
            {
                TN = NSearch(T, Search);
                if (TN != null) { break; }
            }
            return TN;
        }

        private void eNode(TreeNode TN, bool All)
        {
            foreach (TreeNode TC in TN.Nodes)
            {
                if (All == false)
                {
                    if (TC.Tag != null && TC.Tag != "C") { tvList.Add(TC); }
                }
                else
                {
                    tvList.Add(TC);
                }
                if (TC.Nodes.Count > 0) { eNode(TC, All); }
            }

        }

        private void ListNodes(TreeView TV, bool All = false)
        {
            tvList.Clear();
            foreach (TreeNode T in TV.Nodes)
            {
                if (All == false)
                {
                    if (T.Tag != null && T.Tag.ToString().EqualsIgnoreCase("Reg")) { tvList.Add(T); }
                }
                else
                {
                    tvList.Add(T);
                }
                if (T.Nodes.Count > 0) { eNode(T, All); }
            }
        }

        private int cNode(TreeNode TN)
        {
            int c = 0;
            foreach (TreeNode TC in TN.Nodes)
            {
                if (TC.Tag != null) { if (TC.Checked && TC.Tag != "C") { c += 1; } }
                if (TC.Nodes.Count > 0) { c += cNode(TC); }
            }
            return c;
        }

        private int CountNodes(TreeView TV)
        {
            int c = 0;

            foreach (TreeNode T in TV.Nodes)
            {
                if (T.Nodes.Count > 0) { c += cNode(T); }
                if (T.Tag != null)
                {
                    if (T.Tag.ToString().EqualsIgnoreCase("Reg") && T.Checked) { c += 1; }
                }
            }

            return c;
        }

        private bool RegMountNeeded()
        {
            if (lstAddons.Items.Count > 0) { return true; }
            if (lstServices.CheckedItems.Count > 0) { return true; }
            if (lstSilent.Items.Count > 0) { return true; }
            if (XPMode) { return true; }
            if (lstThemes.Items.Cast<ListViewItem>().Any(t => t.Font.Bold)) { return true; }
            if (CountNodes(tvTweaks) > 0) { return true; }
            return false;
        }

        private void BWRun_DoWork(object sender, DoWorkEventArgs e)
        {
            XPMode = false;
            cMain.PreventSleep(cOptions.PreventSleep);

            CheckForIllegalCrossThreadCalls = false;
            cMain.UpdateToolStripLabel(lblStatus, "Clearing Integrated List...");

            int i = 1;

            cMain.FreeRAM();

            if (lstComponents.CheckedItems.Count > 0 && !string.IsNullOrEmpty(DVD))
            {
                try
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Checking 'Support' folder...");
                    if (lstComponents.FindItemWithText("Delete 'Support' folder") != null)
                    {
                        if (lstComponents.FindItemWithText("Delete 'Support' folder").Checked)
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Removing 'Support' folder...");
                            Files.DeleteFolder(lstComponents.FindItemWithText("Delete 'Support' folder").SubItems[1].Text, false);
                        }
                    }

                    cMain.UpdateToolStripLabel(lblStatus, "Checking 'Support' folder...");
                    if (lstComponents.FindItemWithText("Delete 'Upgrade' folder") != null)
                    {
                        if (lstComponents.FindItemWithText("Delete 'Upgrade' folder").Checked)
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Removing 'Upgrade' folder...");
                            Files.DeleteFolder(lstComponents.FindItemWithText("Delete 'Upgrade' folder").SubItems[1].Text, false);
                        }
                    }
                }
                catch { }
            }

            cMain.UpdateToolStripLabel(lblStatus, "Detecting BOOT.WIM...");
            string bWim = cMain.selectedImages[0].Location;
            try
            {
                while (!bWim.EndsWithIgnoreCase("\\"))
                {
                    bWim = bWim.Substring(0, bWim.Length - 1);
                }
                bWim += "boot.wim";
                bWim = bWim.ReplaceIgnoreCase("\\\\", "\\");
            }
            catch (Exception Ex)
            {
                LogErr("Error trying to detect boot.wim" + Environment.NewLine + bWim, Ex.Message);
                cMain.WriteLog(this, "Error trying to detect boot.wim", Ex.Message, lblStatus.Text);
            }

            cMain.UpdateToolStripLabel(lblStatus, "Counting special drivers...");
            int SysDriver = lstDrivers.Items.Cast<ListViewItem>().Where(d => d.ImageIndex == 0).Count();
            int SysLang = lstUpdates.Items.Cast<ListViewItem>().Where(u => u.Group.Header.EqualsIgnoreCase("Language Packs")).Count();



            cMain.UpdateToolStripLabel(lblStatus, "Counting any added language packs...");

            try
            {
                SP = "";
                if (tSB && !string.IsNullOrEmpty(DVD))
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Changing Setup Background...");
                    if (!File.Exists(PEBackground + ".orig") && File.Exists(PEBackground))
                    {
                        cMain.TakeOwnership(PEBackground);
                        File.Move(PEBackground, PEBackground + ".orig");
                    }
                    if (File.Exists(tSBCT))
                    {
                        File.Copy(tSBCT, PEBackground, true);
                    }
                    SP = PEBackground.ReplaceIgnoreCase("background_cli.bmp", "spwizimg.dll");
                    cMain.WriteResource(Resources.ResHacker, cMain.UserTempPath + "\\ResHacker.exe", this);

                    cMain.UpdateToolStripLabel(lblStatus, "");
                    cMain.FreeRAM();
                }
            }
            catch (Exception Ex)
            {
                LogErr("Unable to set background (DVD Root2)" + Environment.NewLine + SP, Ex.Message);
                cMain.WriteLog(this, "Unable to set background (DVD Root2)", Ex.Message, lblStatus.Text);
            }

            if (Directory.Exists(cOptions.WinToolkitTemp + "\\IE"))
            {
                cMain.UpdateToolStripLabel(lblStatus, "Deleting IE Temp Files...");
                Files.DeleteFolder(cOptions.WinToolkitTemp + "\\IE", false);
            }
            cMain.FreeRAM();

            if (lstOptions.FindItemWithText("Boot.wim Test").Checked == false)
            {
                PB.Value = 0;
                PB.Maximum = 100;
                int M = cMain.selectedImages.Count;
                PB.Maximum = M * (lstAddons.Items.Count + lstDrivers.Items.Count + lstGadgets.Items.Count + lstThemes.Items.Count + lstUpdates.Items.Count + lstWallpapers.Items.Count + lstComponents.CheckedItems.Count + CountNodes(tvComponents) + lstFiles.Items.Count + lstServices.CheckedItems.Count + lstSilent.Items.Count + CountNodes(tvTweaks));

                cMain.UpdateToolStripLabel(lblStatus, "Starting work with images...");
                Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);

                foreach (WIMImage image in cMain.selectedImages)
                {

                    bAbort = false;
                    try
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Preparing integration group for this image...");
                        var NGU = new ListViewGroup();
                        var NGMain = new ListViewGroup();
                        var NGA = new ListViewGroup();
                        var NGT = new ListViewGroup();
                        var NGG = new ListViewGroup();
                        var NGW = new ListViewGroup();
                        var NGS = new ListViewGroup();
                        if (image.Name.ContainsIgnoreCase(image.Name.ToUpper()))
                        {
                            NGU.Header = image.Name;
                        }
                        else
                        {
                            NGU.Header = image.Name + " " + image.Architecture.ToString();
                        }
                        NGU.HeaderAlignment = HorizontalAlignment.Center;
                        if (image.Name.ContainsIgnoreCase(image.Architecture.ToString().ToUpper()))
                        {
                            NGMain.Header = image.Name;
                        }
                        else
                        {
                            NGMain.Header = image.Name + " " + image.Architecture.ToString();
                        }
                        NGMain.HeaderAlignment = HorizontalAlignment.Center;
                        if (image.Name.ContainsIgnoreCase(image.Architecture.ToString().ToUpper()))
                        {
                            NGA.Header = image.Name;
                        }
                        else
                        {
                            NGA.Header = image.Name + " " + image.Architecture.ToString();
                        }
                        NGA.HeaderAlignment = HorizontalAlignment.Center;
                        if (image.Name.ContainsIgnoreCase(image.Architecture.ToString().ToUpper()))
                        {
                            NGT.Header = image.Name;
                        }
                        else
                        {
                            NGT.Header = image.Name + " " + image.Architecture.ToString();
                        }
                        NGT.HeaderAlignment = HorizontalAlignment.Center;
                        if (image.Name.ContainsIgnoreCase(image.Architecture.ToString().ToUpper()))
                        {
                            NGG.Header = image.Name;
                        }
                        else
                        {
                            NGG.Header = image.Name + " " + image.Architecture.ToString();
                        }
                        NGG.HeaderAlignment = HorizontalAlignment.Center;
                        if (image.Name.ContainsIgnoreCase(image.Architecture.ToString().ToUpper()))
                        {
                            NGW.Header = image.Name;
                        }
                        else
                        {
                            NGW.Header = image.Name + " " + image.Architecture.ToString();
                        }
                        NGW.HeaderAlignment = HorizontalAlignment.Center;
                        if (image.Name.ContainsIgnoreCase(image.Architecture.ToString().ToUpper()))
                        {
                            NGS.Header = image.Name;
                        }
                        else
                        {
                            NGS.Header = image.Name + " " + image.Architecture.ToString();
                        }
                        NGS.HeaderAlignment = HorizontalAlignment.Center;
                        lstIUpdates.Groups.Add(NGU);
                        lstIDrivers.Groups.Add(NGMain);
                        lstIAddons.Groups.Add(NGA);
                        lstIThemes.Groups.Add(NGT);
                        lstIGadgets.Groups.Add(NGG);
                        lstIWallpapers.Groups.Add(NGW);
                        lstISilent.Groups.Add(NGS);
                        cMain.UpdateToolStripLabel(lblStatus, "");
                    }
                    catch (Exception Ex)
                    {
                        LogErr("Error adding integrated groups.", Ex.Message);
                        cMain.WriteLog(this, "Error adding integrated groups", Ex.Message, lblStatus.Text);
                    }
                    cMain.FreeRAM();

                    try
                    {
                        cMain.UpdateToolStripLabel(lblStatusM, "Image: " + i + " of " + cMain.selectedImages.Count);
                        if (BWRun.CancellationPending || bAbort) { return; }
                        cMain.UpdateToolStripLabel(lblStatus, "Preparing to mount...");

                        if (lstOptions.FindItemWithText("Prompt Mount").Checked)
                        {
                            MessageBox.Show("Win Toolkit is about to mount the following image:" + Environment.NewLine + Environment.NewLine + "[" + image.Name + "]", "Notify");
                        }
                        cMain.FreeRAM();
                        image.Mount(lblStatus, this);

                        if (!string.IsNullOrEmpty(image.MountPath))
                        {
                            cMain.FreeRAM();

                            currentlyMounted = image;
                            ComponentClass = new cComponents(image);
                            try
                            {
                                cMain.UpdateToolStripLabel(lblStatus,
                                    i + "\\" + cMain.selectedImages.Count + " - " + image.Name + " :: Unloading Registry");
                                cReg.RegUnLoadAll();
                            }
                            catch
                            {
                            }

                            try
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Checking and if needed, attempting to remount wim...");
                                cMain.OpenProgram("\"" + DISM.Latest.Location + "\"", "/Remount-Wim /MountDir:\"" + image.MountPath + "\"", true,
                                    ProcessWindowStyle.Hidden);
                            }
                            catch
                            {
                            }

                            try
                            {
                                if (FindNode(tvTweaks, "Enable Legacy .NET Framework").Checked)
                                {
                                    cMain.UpdateToolStripLabel(lblStatus, "Enabling .NET Framework [Win8]...");
                                    string IFolder = cMain.selectedImages[0].Location;
                                    while (!IFolder.EndsWithIgnoreCase("\\"))
                                    {
                                        IFolder = IFolder.Substring(0, IFolder.Length - 1);
                                    }
                                    IFolder = IFolder + "sxs";

                                    string IFolder64 = IFolder.ReplaceIgnoreCase("sources\\sxs", "sourc64\\sxs");

                                    if (cMain.selectedImages[0].Architecture == Architecture.x64 && Directory.Exists(IFolder64))
                                    {
                                        IFolder = IFolder64;
                                    }

                                    cMain.OpenProgram("\"" + DISM.Latest.Location + "\"",
                                        "/Image:\"" + image.MountPath + "\" /Enable-Feature /FeatureName:NetFx3 /Source:\"" + IFolder +
                                        "\" /LimitAccess", true, ProcessWindowStyle.Hidden);
                                    ChangeProgress();
                                }
                            }
                            catch
                            {
                            }

                            if (!string.IsNullOrEmpty(SP) && File.Exists(tSBCT))
                            {
                                cMain.WriteResource(Resources.ResHacker, cMain.UserTempPath + "\\ResHacker.exe", this);
                                cMain.UpdateToolStripLabel(lblStatus, "Changing Setup Background [Main]...");
                                string S = image.MountPath + "\\Windows\\System32\\spwizimg.dll";
                                try
                                {
                                    if (File.Exists(S))
                                    {
                                        if (!File.Exists(S + ".bak"))
                                        {
                                            File.Copy(S, S + ".bak");
                                        }
                                        cMain.TakeOwnership(S);
                                        cMain.OpenProgram("\"" + cMain.UserTempPath + "\\ResHacker.exe\"",
                                            "-addoverwrite \"" + S + "\", \"" + S + "\", \"" + tSBCT + "\", BITMAP, 517, 1033", true,
                                            ProcessWindowStyle.Hidden);
                                        cMain.OpenProgram("\"" + cMain.UserTempPath + "\\ResHacker.exe\"",
                                            "-addoverwrite \"" + S + "\", \"" + S + "\", \"" + tSBCT + "\", BITMAP, 518, 1033", true,
                                            ProcessWindowStyle.Hidden);
                                    }
                                    S = image.MountPath + "\\Windows\\SysWOW64\\spwizimg.dll";
                                    if (File.Exists(S))
                                    {
                                        cMain.TakeOwnership(S);
                                        cMain.OpenProgram("\"" + cMain.UserTempPath + "\\ResHacker.exe\"",
                                            "-addoverwrite \"" + S + "\", \"" + S + "\", \"" + tSBCT + "\", BITMAP, 517, 1033", true,
                                            ProcessWindowStyle.Hidden);
                                        cMain.OpenProgram("\"" + cMain.UserTempPath + "\\ResHacker.exe\"",
                                            "-addoverwrite \"" + S + "\", \"" + S + "\", \"" + tSBCT + "\", BITMAP, 518, 1033", true,
                                            ProcessWindowStyle.Hidden);
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    cMain.WriteLog(this, "Error setting background (main)", Ex.Message,
                                        tSBCT + " | " + SP + Environment.NewLine + S);
                                    if (File.Exists(S + ".bak"))
                                    {
                                        Files.DeleteFile(S);
                                        File.Copy(S + ".bak", S, true);
                                    }
                                }
                            }

                            cMain.UpdateToolStripLabel(lblStatus, "Checking for Updates...");
                            XPMode = false;
                            if (lstUpdates.Items.Count > 0 && !BWRun.CancellationPending && !bAbort)
                            {
                                //TabControl1.SelectedTab = TabUpdates;
                                cMain.UpdateToolStripLabel(lblStatus, "Integrating Updates...");



                                foreach (ListViewGroup LVG in lstUpdates.Groups)
                                {
                                    if (LVG.Items.Count == 0)
                                        continue;

                                    int cIdx = 1;
                                    foreach (ListViewItem LST in LVG.Items)
                                    {
                                        if (LST.Group == null)
                                            continue;

                                        try
                                        {
                                            if (LST.Text == null || string.IsNullOrEmpty(LST.Text))
                                            {
                                                string newName = Path.GetFileNameWithoutExtension(LST.SubItems[5].Text);
                                                var thread2 = new Action(delegate { LST.Text = newName; });
                                                thread2.Invoke();
                                            }
                                        }
                                        catch (Exception Ex)
                                        {
                                            new SmallError("Error attempting to fix name", Ex, LST);
                                        }

                                        try
                                        {
                                            UpdateItem(cIdx, LST, false, LVG.Items.Count, image);
                                        }
                                        catch (Exception Ex)
                                        {
                                            new SmallError("Error integrating update #1", Ex, LST).Upload();
                                        }
                                        cIdx++;
                                        if (BWRun.CancellationPending || bAbort)
                                        {
                                            break;
                                        }
                                    }
                                    if (BWRun.CancellationPending || bAbort)
                                    {
                                        break;
                                    }
                                }



                                if (lstOptions.FindItemWithText("Retry Updates").Checked)
                                {

                                    foreach (ListViewGroup LVG in lstUpdates.Groups)
                                    {
                                        int cIdx = 1;
                                        ListViewItem[] fLST = LVG.Items.Cast<ListViewItem>().Where(u => u.ImageIndex == 5).ToArray();
                                        foreach (ListViewItem LST in fLST)
                                        {
                                            try
                                            {

                                                UpdateItem(cIdx, LST, true, fLST.Count(), image);

                                                if (LST.ImageIndex == 5 && lstOptions.FindItemWithText("Failed Updates to Silent Installers").Checked)
                                                {

                                                    if (lstSilent.FindItemWithText(LST.SubItems[5].Text) == null &&
                                                         (lstSilent.FindItemWithText(LST.SubItems[6].Text) == null || string.IsNullOrEmpty(LST.SubItems[6].Text)))
                                                    {
                                                        var LST2 = new ListViewItem();

                                                        LST2.Text = LST.Text;
                                                        LST2.SubItems.Add("N/A (Not Needed)");
                                                        LST2.SubItems.Add("NO");
                                                        LST2.SubItems.Add(LST.SubItems[3].Text);
                                                        LST2.SubItems.Add(LST.SubItems[5].Text);
                                                        LST2.Group = lstSilent.Groups[0];
                                                        lstSilent.Items.Add(LST2);

                                                        UpdateNames(false);
                                                    }
                                                    LST.ImageIndex = 10;

                                                }
                                            }
                                            catch (Exception Ex)
                                            {
                                                new SmallError("Error retrying update", Ex, LST).Upload();
                                            }

                                            cIdx++;
                                            if (BWRun.CancellationPending || bAbort)
                                            {
                                                break;
                                            }
                                        }
                                        if (BWRun.CancellationPending || bAbort)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                            //LanguagePacks
                            cMain.UpdateToolStripLabel(lblStatus, "Preparing Lang.ini...");

                            if (!string.IsNullOrEmpty(DVD) && SysLang > 0)
                            {
                                cMain.WriteResource(image.Architecture.ToString().EqualsIgnoreCase("x86") ? Resources.intlcfg86 : Resources.intlcfg64,
                                    cMain.UserTempPath + "\\intlcfg.exe", this);
                                LangINI = cMain.RunExternal("\"" + cMain.UserTempPath + "\\intlcfg.exe\"",
                                    "-silent -genlangini -dist:\"" + DVD.Substring(0, DVD.Length - 1) + "\" -image:\"" + image.MountPath +
                                    "\" -F");

                                bool AddEN = false;
                                foreach (ListViewItem LST in lstUpdates.Groups[0].Items)
                                {
                                    if (LST.Text.EqualsIgnoreCase("English Lang Pack") && LST.ImageIndex == 6)
                                    {
                                        AddEN = true;
                                        break;
                                    }
                                }

                                if (AddEN)
                                {
                                    string lINI = cMain.selectedImages[0].Location;
                                    if (lINI.ContainsIgnoreCase("\\"))
                                    {
                                        while (!lINI.EndsWithIgnoreCase("\\"))
                                        {
                                            lINI = lINI.Substring(0, lINI.Length - 1);
                                        }

                                        lINI += "Lang.ini";
                                    }

                                    string sNewLangINI = "";
                                    string sCurLangINI;

                                    using (StreamReader SR = new StreamReader(lINI))
                                    {
                                        sCurLangINI = SR.ReadToEnd();
                                    }

                                    if (!sCurLangINI.ContainsIgnoreCase("EN-US = "))
                                    {
                                        foreach (string R in sCurLangINI.Split(Environment.NewLine.ToCharArray()))
                                        {
                                            if (string.IsNullOrEmpty(R))
                                            {
                                                continue;
                                            }
                                            sNewLangINI += R + "\n";
                                            if (R.StartsWithIgnoreCase("[Available UI Languages]") && !sCurLangINI.ContainsIgnoreCase("EN-US = "))
                                            {
                                                sNewLangINI += "en-US = 2" + Environment.NewLine;
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(sNewLangINI))
                                        {
                                            using (StreamWriter SW = new StreamWriter(lINI))
                                            {
                                                SW.Write(sNewLangINI);
                                            }
                                        }
                                    }
                                }
                            }

                            cMain.UpdateToolStripLabel(lblStatus, "Preparing LocalPacks...");

                            if (Directory.Exists(image.MountPath + "\\Windows\\Globalization\\"))
                            {
                                foreach (
                                    string LP in
                                        Directory.GetFiles(image.MountPath + "\\Windows\\Globalization\\", "*.theme", SearchOption.AllDirectories)
                                    )
                                {
                                    try
                                    {
                                        string n = LP;
                                        while (n.ContainsIgnoreCase("\\"))
                                        {
                                            n = n.Substring(1);
                                        }

                                        if (!File.Exists(image.MountPath + "\\Windows\\Resources\\Themes\\" + n))
                                        {
                                            File.Copy(LP, image.MountPath + "\\Windows\\Resources\\Themes\\" + n, true);
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }

                            }

                            //PROMPT UPDATES

                            if (lstOptions.FindItemWithText("Prompt Updates").Checked && !BWRun.CancellationPending && !bAbort)
                            {
                                cMain.FreeRAM();
                                try
                                {
                                    cMain.UpdateToolStripLabel(lblStatus, "Getting Update List...");
                                    string UICheck = cMain.RunExternal("\"" + DISM.Latest.Location + "\"",
                                        "/Image:\"" + image.MountPath + "\" /Get-Packages /Format:table");
                                    foreach (string Line in UICheck.Split(Environment.NewLine.ToCharArray()))
                                    {
                                        if (string.IsNullOrEmpty(Line)) { continue; }
                                        if (!Line.ContainsIgnoreCase("~~") && !Line.ContainsIgnoreCase("Windows-Client-LanguagePack-Package"))
                                        {
                                            continue;
                                        }


                                        try
                                        {
                                            var NI = new ListViewItem();
                                            foreach (string U in Line.Split('|'))
                                            {
                                                string LU = U;
                                                try
                                                {
                                                    while (!LU.EndsWithIgnoreCase(" "))
                                                    {
                                                        LU = LU.Substring(0, LU.Length - 1);
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                                if (LU.ContainsIgnoreCase("~"))
                                                {
                                                    string ST = LU;
                                                    while (!ST.ContainsIgnoreCase("~"))
                                                    {
                                                        ST = ST.Substring(0, ST.Length - 1);
                                                    }
                                                    NI.Text = ST;
                                                    while (NI.Text.EndsWithIgnoreCase(" "))
                                                        NI.Text = NI.Text.Substring(0, NI.Text.Length - 1);

                                                }
                                                while (LU.EndsWithIgnoreCase(" "))
                                                    LU = LU.Substring(0, LU.Length - 1);
                                                while (LU.StartsWithIgnoreCase(" "))
                                                    LU = LU.Substring(1);
                                                if (string.IsNullOrEmpty(LU))
                                                {
                                                    LU = "N/A";
                                                }

                                                NI.SubItems.Add(LU);
                                            }

                                            while (NI.Text.ContainsIgnoreCase("~"))
                                            {
                                                NI.Text = NI.Text.Substring(0, NI.Text.Length - 1);
                                            }
                                            NI.Group = lstIUpdates.Groups[i - 1];

                                            if (NI.Text.ContainsIgnoreCase("InternetExplorer-Package") || NI.Text.ContainsIgnoreCase("InternetExplorer-Optional-Package"))
                                            {
                                                string sIEVersion = NI.SubItems[1].Text;
                                                while (sIEVersion.ContainsIgnoreCase("~"))
                                                {
                                                    sIEVersion = sIEVersion.Substring(1);
                                                }
                                                while (sIEVersion.ContainsIgnoreCase("."))
                                                {
                                                    sIEVersion = sIEVersion.Substring(0, sIEVersion.Length - 1);
                                                }

                                                NI.Text = "Internet Explorer " + sIEVersion;
                                            }
                                            if (NI.SubItems[3].Text.EqualsIgnoreCase("Language Pack"))
                                            {
                                                NI.Text = cMain.GetLPName(NI.SubItems[1].Text) + " Language Pack";
                                            }

                                            if (!string.IsNullOrEmpty(NI.Text))
                                            {
                                                lstIUpdates.Items.Add(NI);
                                            }
                                            //if (lstIUpdates.FindItemWithText(NI.Text) == null) { lstIUpdates.Items.Add(NI); }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    LogErr("Error checking for updates.", Ex.Message);
                                    cMain.WriteLog(this, "Error checking for updates.", Ex.Message, lblStatus.Text);
                                }
                            }

                            //Component Removal

                            cMain.UpdateToolStripLabel(lblStatus, "Checking Components list...");

                            if (CountNodes(tvComponents) > 0)
                            {
                                try
                                {
                                    if (FindNode(tvComponents, "Speech and Natural Language").Checked && !BWRun.CancellationPending && !bAbort)
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "Removing Speech and Natural Language...");
                                        ComponentClass.Remove_SpeechLang(image.MountPath, false, lblStatus);
                                        ChangeProgress();
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (FindNode(tvComponents, "Remove Integrated Wallpapers").Checked && !BWRun.CancellationPending && !bAbort)
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "Removing 'Remove Integrated Wallpapers'...");
                                        ComponentClass.RemoveWinToolkitWallpapers();
                                        ChangeProgress();
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (FindNode(tvComponents, "Ease of Access Themes").Checked && !BWRun.CancellationPending && !bAbort)
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "Removing 'East of Access Themes'...");
                                        ComponentClass.RemoveThemesEOA();
                                        ChangeProgress();
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (FindNode(tvComponents, "Aero").Checked && !BWRun.CancellationPending && !bAbort)
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "Removing 'Aero Theme'...");
                                        ComponentClass.RemoveThemesA();
                                        ChangeProgress();
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (FindNode(tvComponents, "Extra Themes").Checked && !BWRun.CancellationPending && !bAbort)
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "Removing 'Non-Aero Themes'...");
                                        ComponentClass.RemoveThemesNA();
                                        ChangeProgress();
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (FindNode(tvComponents, "Delete 'WinSXS\\\\Backup' Folder").Checked && !BWRun.CancellationPending && !bAbort)
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "Removing WinSXS\\Backup folder..");
                                        int WBt = Directory.GetFiles(image.MountPath + "\\Windows\\WinSXS\\Backup").Count();
                                        int WBn = 1;
                                        foreach (string f in Directory.GetFiles(image.MountPath + "\\Windows\\WinSXS\\Backup"))
                                        {
                                            if (BWRun.CancellationPending || bAbort)
                                                break;
                                            try
                                            {
                                                cMain.UpdateToolStripLabel(lblStatus,
                                                    "(" + WBn + "/" + WBt + ") Removing WinSXS\\Backup\\" + cMain.GetFName(f).Substring(0, 35));
                                            }
                                            catch (Exception ex)
                                            {
                                                cMain.UpdateToolStripLabel(lblStatus,
                                                    "(" + WBn + "/" + WBt + ") Removing WinSXS\\Backup\\" + cMain.GetFName(f));
                                            }
                                            cMain.TakeOwnership(f);
                                            Files.DeleteFile(f);
                                            WBn = WBn + 1;
                                        }

                                        cMain.FreeRAM();
                                        if (BWRun.CancellationPending || bAbort)
                                        {
                                            return;
                                        }

                                        cMain.TakeOwnership(image.MountPath + "\\Windows\\WinSXS\\Backup");
                                        Files.DeleteFolder(image.MountPath + "\\Windows\\WinSXS\\Backup", false);
                                        cMain.FreeRAM();

                                        ChangeProgress();

                                    }
                                }
                                catch
                                {
                                }
                            }

                            if (lstComponents.CheckedItems.Count > 0 && !BWRun.CancellationPending && !bAbort)
                            {
                                //TabControl1.SelectedTab = TabComponents;
                                cMain.UpdateToolStripLabel(lblStatus, "Removing Components");
                                int iComp = 1;

                                foreach (ListViewItem LST in lstComponents.CheckedItems)
                                {
                                    if (LST.Tag == null)
                                    {
                                        try
                                        {
                                            cMain.UpdateToolStripLabel(lblStatus,
                                                "Finding: " + iComp + " of " + lstComponents.CheckedItems.Count + " (" + LST.Text + ")");

                                            foreach (
                                                string D in
                                                    Directory.GetFiles(image.MountPath + "\\Windows\\servicing\\Packages", "*.mum",
                                                        SearchOption.TopDirectoryOnly))
                                            {
                                                string F = D.ReplaceIgnoreCase(".mum", "");

                                                while (F.ContainsIgnoreCase("\\"))
                                                {
                                                    F = F.Substring(1);
                                                }

                                                if (F.StartsWithIgnoreCase(LST.SubItems[1].Text))
                                                {
                                                    cReg.ShowPackages(F, false, true, true, image);
                                                    cMain.UpdateToolStripLabel(lblStatus,
                                                        "Removing: " + iComp + " of " + lstComponents.CheckedItems.Count + " (" + LST.Text + ")");
                                                    cMain.RunExternal("\"" + DISM.Latest.Location + "\"",
                                                        "/Image:\"" + image.MountPath + "\" /Remove-Package /PackageName:" + F);
                                                }
                                            }
                                        }
                                        catch
                                        {
                                        }

                                        iComp += 1;
                                        ChangeProgress();
                                        cMain.FreeRAM();

                                    }
                                    if (BWRun.CancellationPending || bAbort)
                                    {
                                        break;
                                    }
                                }
                                cMain.UpdateToolStripLabel(lblStatus, "Making sure packages are hidden...");
                                Application.DoEvents();
                                cReg.ShowPackages("", true, false, true, image);

                                cMain.FreeRAM();

                            }

                            //Copying Files
                            try
                            {
                                if (lstFiles.Items.Count > 0 && !BWRun.CancellationPending && !bAbort)
                                {
                                    cMain.UpdateToolStripLabel(lblStatus, "Preparing Files...");
                                    //TabControl1.SelectedTab = TabFiles;
                                    var AR = new ArrayList();

                                    for (int iF = 0; iF < lstFiles.Items.Count; iF++)
                                    {
                                        try
                                        {
                                            ListViewItem LST = null;
                                            lstFiles.Invoke(new Action(() =>
                                                {
                                                    LST = lstFiles.Items[iF];
                                                }));


                                            cMain.UpdateToolStripLabel(lblStatus, "Copying file " + (iF + 1) + " of " + lstFiles.Items.Count);
                                            bool replaceFile = false;
                                            string copyFrom = "";
                                            string saveTo = "";
                                            try
                                            {
                                                copyFrom = LST.SubItems[3].Text;
                                                saveTo = LST.SubItems[4].Text;

                                                if (LST.SubItems[2].Text.ContainsIgnoreCase("TRUE"))
                                                {
                                                    replaceFile = true;
                                                }

                                                try
                                                {
                                                    string sCT = image.MountPath;

                                                    while (saveTo.ContainsIgnoreCase(":"))
                                                    {
                                                        saveTo = saveTo.Substring(1);
                                                    }
                                                    saveTo = saveTo.ReplaceIgnoreCase("%SystemRoot%", "Windows\\System32");
                                                    saveTo = saveTo.ReplaceIgnoreCase("%SystemDrive%\\", "");
                                                    saveTo = saveTo.ReplaceIgnoreCase("%DVDRoot%\\", DVD);
                                                    saveTo = sCT + "\\" + saveTo;
                                                    saveTo = saveTo.ReplaceIgnoreCase("\\\\", "\\");
                                                }
                                                catch (Exception Ex)
                                                {
                                                }

                                                if (File.Exists(copyFrom))
                                                {
                                                    string Dir = saveTo;
                                                    if (Dir.ContainsIgnoreCase("\\"))
                                                    {
                                                        while (!Dir.EndsWithIgnoreCase("\\"))
                                                            Dir = Dir.Substring(0, Dir.Length - 1);

                                                        Dir = Dir.Substring(0, Dir.Length - 1);
                                                        if (!Directory.Exists(Dir))
                                                        {
                                                            cMain.CreateDirectory(Dir);
                                                        }
                                                    }

                                                    if (File.Exists(saveTo) && replaceFile)
                                                    {
                                                        Files.MoveFile(copyFrom, saveTo, replaceFile);
                                                    }

                                                    if (!File.Exists(saveTo))
                                                    {
                                                        File.Copy(copyFrom, saveTo, replaceFile);
                                                    }
                                                }
                                            }
                                            catch (Exception Ex)
                                            {
                                                LogErr(
                                                    "Unable to copy file from '" + copyFrom + "' to '" + saveTo + "' (" + replaceFile.ToString(CultureInfo.InvariantCulture) + ")",
                                                    Ex.Message);
                                                cMain.WriteLog(this, "Unable to copy file", Ex.Message,
                                                    "From:" + copyFrom + Environment.NewLine + "Save:" + saveTo + Environment.NewLine + "Replace:" +
                                                    replaceFile.ToString(CultureInfo.InvariantCulture));
                                            }
                                            ChangeProgress();
                                            if (BWRun.CancellationPending || bAbort)
                                            {
                                                break;
                                            }
                                        }
                                        catch (Exception exx)
                                        {

                                        }
                                    }

                                }

                            }
                            catch
                            {
                            }
                            //Preparing Drivers
                            cMain.UpdateToolStripLabel(lblStatus, "Checking for non-prepared drivers...");
                            try
                            {
                                if (lstDrivers.Items.Count > 0 && !BWRun.CancellationPending && !bAbort &&
                                     lstOptions.FindItemWithText("Prepare Drivers").Checked)
                                {
                                    //TabControl1.SelectedTab = TabDrivers;
                                    cMain.UpdateToolStripLabel(lblStatus, "Preparing Drivers...");

                                    var AR = new ArrayList();

                                    foreach (ListViewItem LST in lstDrivers.Items)
                                    {
                                        try
                                        {
                                            if (Directory.Exists(LST.SubItems[8].Text)) //
                                            {
                                                foreach (string F in Directory.GetFiles(LST.SubItems[8].Text, "*.*_", SearchOption.AllDirectories)) //
                                                {
                                                    if (!AR.Contains(F))
                                                    {
                                                        AR.Add(F);
                                                    }
                                                }
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }

                                    int PDc = 1;
                                    foreach (string F in AR)
                                    {
                                        try
                                        {
                                            cMain.UpdateToolStripLabel(lblStatus, "Expanding: (" + PDc + "\\" + AR.Count + ") " + F + "...");
                                            string fD = F;
                                            try
                                            {
                                                while (!fD.EndsWithIgnoreCase("\\"))
                                                    fD = fD.Substring(0, fD.Length - 1);

                                                if (!File.Exists(fD + "difxapi.dll"))
                                                {
                                                    File.Copy(image.MountPath + "\\Windows\\System32\\difxapi.dll",
                                                        fD + "difxapi.dll", false);
                                                }
                                                if (File.Exists(fD + "nvidia-smi.1.pdd"))
                                                {
                                                    File.Move(fD + "nvidia-smi.1.pdd", fD + "nvidia-smi.1.pdf");
                                                }
                                            }
                                            catch
                                            {
                                            }
                                            bool Exp = ExtractDriver(F);


                                            if (!Exp)
                                            {
                                                new SmallError("Unknown File Type:", null, F).Upload();
                                            }
                                        }
                                        catch (Exception Ex)
                                        {
                                            LogErr("Error expanding file: " + F, Ex.Message);
                                            new SmallError("Error expanding file:", Ex, F).Upload();

                                        }
                                        PDc += 1;
                                    }
                                }
                            }
                            catch
                            {
                            }
                            cMain.FreeRAM();

                            //DRIVERS

                            string dLog = "";

                            cMain.UpdateToolStripLabel(lblStatus, "Checking for Drivers...");
                            if (lstDrivers.Items.Count > 0 && !BWRun.CancellationPending && !bAbort)
                            {
                                try
                                {
                                    dErr = "";
                                    cMain.UpdateToolStripLabel(lblStatus, "Integrating Drivers...");
                                    foreach (ListViewItem LST in lstDrivers.Items)
                                    {
                                        string IF = LST.SubItems[6].Text;
                                        string F2 = LST.SubItems[6].Text;
                                        string CF = LST.SubItems[6].Text; //
                                        if (File.Exists(IF))
                                        {
                                            try
                                            {
                                                if (IF.ContainsIgnoreCase(":") && IF.ContainsForeignCharacters())
                                                {
                                                    while (!CF.EndsWithIgnoreCase("\\"))
                                                        CF = CF.Substring(0, CF.Length - 1);

                                                    string FN = LST.SubItems[6].Text;
                                                    while (FN.ContainsIgnoreCase("\\"))
                                                        FN = FN.Substring(1);

                                                    F2 = cOptions.WinToolkitTemp + "\\Driver" +
                                                          (LST.Index + 1).ToString(CultureInfo.InvariantCulture);
                                                    IF = F2 + "\\" + FN;

                                                    Files.DeleteFolder(F2, false);

                                                    cMain.UpdateToolStripLabel(lblStatus, "Copying Driver: " + (LST.Index + 1) + " of " +
                                                                                                      lstDrivers.Items.Count + " (" + LST.SubItems[6].Text +
                                                                                                      ")");
                                                    cMain.CopyDirectory(CF, F2, true, true);
                                                }
                                            }
                                            catch (Exception Ex)
                                            {
                                                IF = LST.SubItems[6].Text;
                                                LogErr("Error copying driver from '" + CF + "' to '" + F2 + "'", Ex.Message);
                                                cMain.WriteLog(this, "Error copying driver", Ex.Message,
                                                    lblStatus.Text + Environment.NewLine + F2 + " ||| " + CF);
                                            }

                                            try
                                            {
                                                Files.DeleteFile(image.MountPath + "\\Windows\\inf\\setupapi.offline.log");

                                                try
                                                {
                                                    cMain.UpdateToolStripLabel(lblStatus,
                                                        "Integrating Driver: " + (LST.Index + 1) + " of " + lstDrivers.Items.Count + " (" + LST.SubItems[6].Text +
                                                        ")");
                                                    string DIDR = "/Image:\"" + image.MountPath + "\" /Add-Driver /Driver:\"" + IF + "\" /English";

                                                    if (lstOptions.FindItemWithText("Force Unsigned").Checked)
                                                    {
                                                        DIDR = DIDR + " /ForceUnsigned";
                                                    }

                                                    string dInt = cMain.RunExternal("\"" + DISM.Latest.Location + "\"", DIDR);

                                                    dLog += dInt + Environment.NewLine + "###-----------------###" + Environment.NewLine + Environment.NewLine;

                                                    if (dInt.ContainsIgnoreCase(".LOG"))
                                                    {
                                                        string Err = "";

                                                        try
                                                        {
                                                            bool S = true;
                                                            if (File.Exists(image.MountPath + "\\Windows\\inf\\setupapi.offline.log"))
                                                            {
                                                                string sRead = "";
                                                                using (var SR = new StreamReader(image.MountPath + "\\Windows\\inf\\setupapi.offline.log"))
                                                                {
                                                                    sRead = SR.ReadToEnd();
                                                                }
                                                                string F = IF;
                                                                while (!F.EndsWithIgnoreCase("\\"))
                                                                {
                                                                    F = F.Substring(0, F.Length - 1);
                                                                }

                                                                foreach (string L in sRead.Split(Environment.NewLine.ToCharArray()))
                                                                {
                                                                    if (L.ContainsIgnoreCase("0x00000002"))
                                                                    {
                                                                        S = false;
                                                                    }
                                                                    if (L.ContainsIgnoreCase("0x80070002"))
                                                                    {
                                                                        S = false;
                                                                    }

                                                                    if (L.ContainsIgnoreCase(F.ToUpper()) && S == false)
                                                                    {
                                                                        Err = "Possible file missing: " + L;
                                                                        while (!Err.StartsWithIgnoreCase("'"))
                                                                        {
                                                                            Err = Err.Substring(1);
                                                                        }

                                                                        while (!Err.EndsWithIgnoreCase("'"))
                                                                        {
                                                                            Err = Err.Substring(0, Err.Length - 1);
                                                                        }
                                                                        break;
                                                                    }
                                                                }

                                                            }
                                                        }
                                                        catch
                                                        {
                                                        }

                                                        if (!string.IsNullOrEmpty(Err))
                                                        {
                                                            dErr += Err + Environment.NewLine;
                                                            try
                                                            {

                                                                if (!Directory.Exists(cMain.Root + "Logs\\"))
                                                                {
                                                                    cMain.CreateDirectory(cMain.Root + "Logs\\");
                                                                }
                                                                using (var SW = new StreamWriter(cMain.Root + "Logs\\Driver_Errors.txt", true))
                                                                {
                                                                    SW.WriteLine(
                                                                        runTime + ": " +
                                                                        Err + Environment.NewLine);
                                                                }

                                                            }
                                                            catch (Exception Ex)
                                                            {
                                                                LogErr("Error creating missing driver log" + Environment.NewLine + dErr, Ex.Message);
                                                                cMain.WriteLog(this, "Error creating missing driver log",
                                                                    Ex.Message, dErr);
                                                            }
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                }

                                                if (IF.ContainsIgnoreCase(":") && IF.ContainsForeignCharacters())
                                                {
                                                    Files.DeleteFolder(F2, false);
                                                }

                                            }
                                            catch (Exception Ex)
                                            {
                                                LogErr("Error Integrating driver: " + IF, Ex.Message);
                                                cMain.WriteLog(this, "Error integrating Driver", Ex.Message, lblStatus.Text);
                                            }
                                        }

                                        cMain.FreeRAM();
                                        ChangeProgress();
                                        if (BWRun.CancellationPending || bAbort)
                                        {
                                            break;
                                        }
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    cMain.WriteLog(this, "Error integrating All Drivers", Ex.Message, lblStatus.Text);
                                }
                            }


                            //PROMPT DRIVERS

                            IntegratedDrivers(image.MountPath, i);

                            //INSTALL THEMEPACKS
                            cMain.UpdateToolStripLabel(lblStatus, "Checking for Theme Packs...");

                            if (lstThemes.Items.Count > 0 && !BWRun.CancellationPending && !bAbort)
                            {
                                //TabControl1.SelectedTab = TabThemes;
                                cMain.UpdateToolStripLabel(lblStatus, "Integrating Theme Packs...");

                                try
                                {
                                    if (Directory.Exists(image.MountPath + "\\Windows\\") &&
                                          !Directory.Exists(image.MountPath + "\\Windows\\Resources\\Themes\\"))
                                    {
                                        cMain.CreateDirectory(image.MountPath + "\\Windows\\Resources\\Themes\\");
                                    }
                                }
                                catch
                                {
                                }

                                foreach (ListViewItem LST in lstThemes.Items)
                                {
                                    string DefaultTheme = "";
                                    string name = LST.Text;
                                    cMain.UpdateToolStripLabel(lblStatus, "Integrating Theme #0: " + (LST.Index + 1) + " of " + lstThemes.Items.Count + " (" + name + ")");
                                    string TPD = "";
                                    string Tn = "";
                                    try
                                    {
                                        string ThemeLoc = LST.SubItems[2].Text;
                                        string extension = Path.GetExtension(ThemeLoc);
                                        if (File.Exists(ThemeLoc))
                                        {
                                            switch (extension.ToUpper())
                                            {
                                                case ".THEME":
                                                    TPD = image.MountPath + "\\Windows\\Resources\\Themes\\" + name;
                                                    if (File.Exists(TPD)) { Files.DeleteFile(TPD); }
                                                    File.Copy(ThemeLoc, TPD, true);

                                                    string TLF = name.Substring(0, name.Length - 6);

                                                    cMain.UpdateToolStripLabel(lblStatus, "Integrating Theme #0.5: " + (LST.Index + 1) + " of " + lstThemes.Items.Count + " (" + name + ")");
                                                    string D = ThemeLoc.ReplaceIgnoreCase(name, TLF);
                                                    if (Directory.Exists(D))
                                                    {
                                                        if (Directory.Exists(image.MountPath + "\\Windows\\Resources\\Themes\\" + TLF)) { Files.DeleteFolder(image.MountPath + "\\Windows\\Resources\\Themes\\" + TLF, false); }
                                                        cMain.CopyDirectory(D, image.MountPath + "\\Windows\\Resources\\Themes\\" + TLF, true, true);
                                                    }
                                                    cMain.UpdateToolStripLabel(lblStatus, "Integrating Theme #0.75: " + (LST.Index + 1) + " of " + lstThemes.Items.Count + " (" + name + ")");
                                                    if (string.IsNullOrEmpty(DefaultTheme) && File.Exists(TPD))
                                                    {
                                                        DefaultTheme = TPD;
                                                    }
                                                    break;
                                                case ".THEMEPACK":
                                                case ".DESKTHEMEPACK":

                                                    TPD = image.MountPath + "\\Windows\\Resources\\Themes\\" + name.Substring(0, name.Length - 10);

                                                    Files.DeleteFolder(TPD, true);

                                                    cMain.UpdateToolStripLabel(lblStatus, "Integrating Theme #1: " + (LST.Index + 1) + " of " + lstThemes.Items.Count + " (" + name + ")");
                                                    if (ThemeLoc.ContainsForeignCharacters())
                                                    {
                                                        string CT = cOptions.WinToolkitTemp + "\\" + (LST.Index + 1).ToString(CultureInfo.InvariantCulture);
                                                        Files.DeleteFolder(CT, true);

                                                        File.Copy(ThemeLoc, CT + extension, true);
                                                        cMain.eErr = cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"", "\"" + CT + extension + "\"" + TPD + "\" -F:*");
                                                        Files.DeleteFile(CT + extension);
                                                        cMain.CopyDirectory(CT, TPD, true, false);
                                                        Files.DeleteFolder(CT, false);
                                                    }
                                                    else
                                                    {
                                                        cMain.eErr = cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"", "\"" + ThemeLoc + "\" \"" + TPD + "\" -F:*");
                                                    }

                                                    cMain.UpdateToolStripLabel(lblStatus, "Integrating Theme #2: " + (LST.Index + 1) + " of " + lstThemes.Items.Count + " (" + name + ")");

                                                    foreach (string T in Directory.GetFiles(TPD, "*.*", SearchOption.AllDirectories))
                                                    {
                                                        if (T.ToUpper().EndsWithIgnoreCase(".THEME"))
                                                        {
                                                            string E = TPD;
                                                            while (E.ContainsIgnoreCase("\\"))
                                                            {
                                                                E = E.Substring(1);
                                                            }
                                                            cMain.TakeOwnership(image.MountPath + "\\Windows\\Resources\\Themes\\" + E + ".theme");
                                                            File.Copy(T, image.MountPath + "\\Windows\\Resources\\Themes\\" + E + ".theme", true);
                                                            Tn = image.MountPath + "\\Windows\\Resources\\Themes\\" + E + ".theme";
                                                            if (string.IsNullOrEmpty(DefaultTheme) && File.Exists(Tn))
                                                            {
                                                                DefaultTheme = Tn;
                                                            }
                                                            File.Delete(T);
                                                        }
                                                    }

                                                    cMain.UpdateToolStripLabel(lblStatus, "Integrating Theme #3: " + (LST.Index + 1) + " of " + lstThemes.Items.Count + " (" + name + ")");
                                                    string A = "";

                                                    try
                                                    {
                                                        if (Directory.Exists(image.MountPath + "\\Windows\\Resources\\Themes\\"))
                                                        {
                                                            int fileCount = Directory.GetFiles(TPD, "*.*", SearchOption.AllDirectories).Length;
                                                            A += "DIR: TRUE (" + fileCount.ToString(CultureInfo.InvariantCulture) + ")";
                                                        }
                                                        else
                                                        {
                                                            A += "DIR: FALSE ";
                                                        }
                                                        if (File.Exists(Tn))
                                                        {
                                                            A += "FILE: TRUE ";
                                                        }
                                                        else
                                                        {
                                                            A += "FILE: FALSE";
                                                        }
                                                    }
                                                    catch
                                                    {
                                                    }

                                                    if (!string.IsNullOrEmpty(Tn) && File.Exists(Tn))
                                                    {
                                                        cMain.UpdateToolStripLabel(lblStatus, "Integrating Theme #4: " + (LST.Index + 1) + " of " + lstThemes.Items.Count + " (" + name + ") " + A);
                                                        string tWrite = "";
                                                        using (var SR = new StreamReader(Tn))
                                                        {
                                                            while (SR.Peek() != -1)
                                                            {
                                                                string tLine = SR.ReadLine();
                                                                string tLineTemp = tLine;
                                                                while (tLineTemp.ContainsIgnoreCase("="))
                                                                {
                                                                    tLineTemp = tLineTemp.Substring(1);
                                                                }

                                                                if (tLine.StartsWithIgnoreCase("BrandImage"))
                                                                {
                                                                    tLine = "BrandImage=%SystemRoot%\\resources\\themes\\" +
                                                                                 name.Substring(0, name.Length - 10) + "\\" + tLineTemp;
                                                                }
                                                                if (tLine.ContainsIgnoreCase("Wallpaper=DesktopBackground"))
                                                                {
                                                                    tLine = "WallPaper=%SystemRoot%\\resources\\themes\\" +
                                                                                 name.Substring(0, name.Length - 10) + "\\" + tLineTemp;
                                                                }
                                                                if (tLine.StartsWithIgnoreCase("ImagesRootPIDL"))
                                                                {
                                                                    tLine = "";
                                                                }
                                                                if (tLine.StartsWithIgnoreCase("ImagesRootPath"))
                                                                {
                                                                    tLine = "ImagesRootPath=%SystemRoot%\\resources\\themes\\" +
                                                                                 name.Substring(0, name.Length - 10) +
                                                                                 "\\DesktopBackground";
                                                                }
                                                                tWrite += tLine + Environment.NewLine;
                                                            }
                                                        }

                                                        cMain.UpdateToolStripLabel(lblStatus, "Integrating Theme #5: " + (LST.Index + 1) + " of " + lstThemes.Items.Count + " (" + name + ")");

                                                        if (!string.IsNullOrEmpty(tWrite))
                                                        {
                                                            Files.DeleteFile(Tn);

                                                            using (var SW = new StreamWriter(Tn))
                                                            {
                                                                SW.Write(tWrite);
                                                            }
                                                        }
                                                    }
                                                    break;
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(DefaultTheme) && File.Exists(DefaultTheme))
                                        {
                                            cMain.UpdateToolStripLabel(lblStatus, "Integrating Theme #6: " + (LST.Index + 1) + " of " + lstThemes.Items.Count + " (" + name + ")");

                                            var thread2 = new Action(delegate { LST.SubItems[3].Text = DefaultTheme; });
                                            thread2.Invoke();
                                        }
                                        else if (string.IsNullOrEmpty(DefaultTheme))
                                        {
                                            new LargeError("Default Theme Detection", "Default theme not found", "",
                                               null, LST).Upload();
                                        }
                                    }
                                    catch (Exception Ex)
                                    {
                                        DefaultTheme = "";
                                        LogErr("Error integrating theme (" + Tn + "): " + LST.SubItems[2].Text + Environment.NewLine + "TPD: " + TPD + Environment.NewLine + Environment.NewLine + cMain.eErr, Ex.Message);
                                        cMain.WriteLog(this, "Error integrating theme (" + Tn + "): " + LST.SubItems[2].Text + Environment.NewLine + "TPD: " + TPD + Environment.NewLine + Environment.NewLine + cMain.eErr, Ex.Message, lblStatus.Text);
                                    }
                                    ChangeProgress();
                                    cMain.FreeRAM();
                                    if (BWRun.CancellationPending || bAbort)
                                    {
                                        break;
                                    }

                                }
                            }
                            cMain.FreeRAM();
                            //Prompt THEMES
                            try
                            {
                                if (lstOptions.FindItemWithText("Prompt Themes").Checked &&
                                      Directory.Exists(image.MountPath + "\\Windows\\Resources\\Themes") &&
                                      !BWRun.CancellationPending && !bAbort)
                                {
                                    cMain.UpdateToolStripLabel(lblStatus, "Getting Themes List...");
                                    foreach (string iThemes in Directory.GetFiles(image.MountPath + "\\Windows\\Resources\\Themes", "*.theme", SearchOption.TopDirectoryOnly))
                                    {
                                        try
                                        {
                                            string iName = iThemes;
                                            while (iName.ContainsIgnoreCase("\\"))
                                            {
                                                iName = iName.Substring(1);
                                            }
                                            var NI = new ListViewItem();
                                            NI.Text = iName;
                                            NI.Group = lstIThemes.Groups[i - 1];
                                            lstIThemes.Items.Add(NI);
                                        }
                                        catch
                                        {
                                        }
                                        cMain.FreeRAM();
                                    }
                                }
                            }
                            catch (Exception Ex)
                            {
                                LogErr("Error whilst searching for integrated theme packs.", Ex.Message);
                                cMain.WriteLog(this, "Error finding integrated Theme Packs", Ex.Message, lblStatus.Text);
                            }

                            cMain.UpdateToolStripLabel(lblStatus, "Gadgets...");
                            //INSTALL GADGETS

                            if (lstGadgets.Items.Count > 0 && !BWRun.CancellationPending && !bAbort)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Integrating Gadgets...");
                                //TabControl1.SelectedTab = TabGadgets;
                                foreach (ListViewItem LST in lstGadgets.Items)
                                {
                                    if (File.Exists(LST.SubItems[2].Text))
                                    {
                                        try
                                        {
                                            string GP = image.MountPath +
                                                                 "\\Program Files\\Windows Sidebar\\Shared Gadgets\\";
                                            cMain.UpdateToolStripLabel(lblStatus, "Integrating Gadget: " + (LST.Index + 1) + " of " +
                                                                         lstGadgets.Items.Count + " (" + LST.Text + ")");
                                            cMain.CreateDirectory(GP + LST.Text + "\\");

                                            cMain.ExtractFiles(LST.SubItems[2].Text, GP + LST.Text, this);

                                        }
                                        catch (Exception Ex)
                                        {
                                            LogErr("Error integrating gadget: " + LST.Text + Environment.NewLine + LST.SubItems[2].Text, Ex.Message);
                                            new SmallError("Error Integrating Gadget", Ex, lblStatus.Text, LST).Upload();
                                        }
                                    }
                                    ChangeProgress();
                                    cMain.FreeRAM();
                                    if (BWRun.CancellationPending || bAbort)
                                    {
                                        break;
                                    }
                                }
                            }

                            //Prompt GADGETS
                            try
                            {
                                if (lstOptions.FindItemWithText("Prompt Gadgets").Checked && !BWRun.CancellationPending && !bAbort && Directory.Exists(image.MountPath + "\\Program Files\\Windows Sidebar\\Shared Gadgets\\"))
                                {
                                    cMain.UpdateToolStripLabel(lblStatus, "Getting Gadgets List...");

                                    foreach (string iGadgets in Directory.GetDirectories(image.MountPath + "\\Program Files\\Windows Sidebar\\Shared Gadgets", "*.gadget", SearchOption.TopDirectoryOnly))
                                    {
                                        try
                                        {
                                            var NI = new ListViewItem();
                                            string iName = iGadgets;
                                            while (iName.ContainsIgnoreCase("\\"))
                                            {
                                                iName = iName.Substring(1);
                                            }

                                            NI.Text = iName;
                                            NI.SubItems.Add(iGadgets);
                                            NI.Group = lstIGadgets.Groups[i - 1];
                                            lstIGadgets.Items.Add(NI);
                                        }
                                        catch
                                        {
                                        }
                                        cMain.FreeRAM();
                                    }
                                }
                            }
                            catch (Exception Ex)
                            {
                                LogErr("Error detecting gadgets.", Ex.Message);
                                cMain.WriteLog(this, "Error detecting gadgets", Ex.Message, lblStatus.Text);
                            }

                            cMain.UpdateToolStripLabel(lblStatus, "Checking for Wallpapers...");

                            //INSTALL WALLPAPERS
                            if (lstWallpapers.Items.Count > 0 && !BWRun.CancellationPending && !bAbort)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Integrating Wallpapers...");
                                //TabControl1.SelectedTab = TabWallpapers;
                                string GP = image.MountPath + "\\Windows\\Web\\Wallpaper\\WinToolkit\\";
                                cMain.CreateDirectory(GP);
                                foreach (ListViewItem LST in lstWallpapers.Items)
                                {
                                    if (File.Exists(LST.SubItems[2].Text))
                                    {
                                        try
                                        {
                                            cMain.UpdateToolStripLabel(lblStatus, "Integrating Wallpaper: " + (LST.Index + 1) + " of " + lstWallpapers.Items.Count + " (" + LST.Text + ")");
                                            string CopyTo = GP + LST.Text;
                                            if (File.Exists(CopyTo))
                                            {
                                                string MD5_1 = cMain.MD5CalcFile(LST.SubItems[2].Text);
                                                string MD5_2 = cMain.MD5CalcFile(CopyTo);

                                                if (!MD5_1.EqualsIgnoreCase(MD5_2))
                                                {
                                                    CopyTo = GP + cMain.RandomName(1, 999999) + "_" + LST.Text;
                                                    while (File.Exists(CopyTo))
                                                    {
                                                        CopyTo = GP + cMain.RandomName(1, 999999) + "_" + LST.Text;
                                                    }
                                                    File.Copy(LST.SubItems[2].Text, CopyTo, true);
                                                }
                                            }
                                            else
                                            {
                                                File.Copy(LST.SubItems[2].Text, CopyTo, true);
                                            }
                                        }
                                        catch (Exception Ex)
                                        {
                                            LogErr("Error integrating wallpaper: " + LST.Text, Ex.Message);
                                            cMain.WriteLog(this, "Error integrating wallpapers" + Environment.NewLine + LST.Text, Ex.Message, lblStatus.Text);
                                        }
                                    }
                                    ChangeProgress();
                                    cMain.FreeRAM();
                                    if (BWRun.CancellationPending || bAbort)
                                    {
                                        break;
                                    }
                                }
                            }

                            //Prompt WALLPAPERS
                            try
                            {
                                if (lstOptions.FindItemWithText("Prompt Wallpapers").Checked &&
                                      !BWRun.CancellationPending && !bAbort &&
                                      Directory.Exists(image.MountPath + "\\Windows\\Web\\Wallpaper\\WinToolkit\\"))
                                {
                                    cMain.UpdateToolStripLabel(lblStatus, "Getting Wallpaper List...");

                                    foreach (string iWallpapers in Directory.GetFiles(image.MountPath + "\\Windows\\Web\\Wallpaper\\WinToolkit\\", "*.*", SearchOption.TopDirectoryOnly))
                                    {
                                        try
                                        {
                                            string iName = iWallpapers;
                                            while (iName.ContainsIgnoreCase("\\"))
                                            {
                                                iName = iName.Substring(1);
                                            }
                                            var NI = new ListViewItem();
                                            NI.Text = iName;
                                            NI.Group = lstIWallpapers.Groups[i - 1];
                                            lstIWallpapers.Items.Add(NI);
                                        }
                                        catch
                                        {
                                        }
                                        cMain.FreeRAM();
                                    }
                                }
                            }
                            catch (Exception Ex)
                            {
                                LogErr("Error detecting wallpapers.", Ex.Message);
                                cMain.WriteLog(this, "Error detecting wallpapers", Ex.Message, lblStatus.Text);
                            }
                            cMain.FreeRAM();
                            //LOAD REGISTRY
                            cMain.UpdateToolStripLabel(lblStatus, "Checking Registry Requirement...");
                            bool WIM_System = false;
                            bool WIM_Software = false;
                            bool WIM_Default = false;
                            bool WIM_SYSDefault = false;
                            bool WIM_Admin = false;

                            bool restoreTheme = lstOptions.FindItemWithText("Restore Default Theme").Checked;
                            bool newTheme = false;
                            string defaultTheme = "";

                            cMain.UpdateToolStripLabel(lblStatus, "Checking Default Theme Requirement...");

                            try
                            {
                                foreach (ListViewItem theme in lstThemes.Items.Cast<ListViewItem>().Where(l => l.Font.Bold && !string.IsNullOrEmpty(l.SubItems[3].Text)))
                                {
                                    defaultTheme = theme.SubItems[3].Text;
                                    newTheme = true;
                                    break;
                                }

                            }
                            catch (Exception Ex)
                            {
                                new LargeError("Default Theme Check", "Error checking default theme", defaultTheme, Ex).Upload();
                            }

                            cMain.UpdateToolStripLabel(lblStatus, "Registry Check Completed...");

                            if (!bAbort && !BWRun.CancellationPending && RegMountNeeded() || restoreTheme || newTheme)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Loading Registry...");
                                string SHiveL = image.MountPath + "\\Windows\\System32\\Config\\";
                                WIM_Software = cReg.RegLoad("WIM_Software", SHiveL + "SOFTWARE");
                                WIM_SYSDefault = cReg.RegLoad("WIM_SYSDefault", SHiveL + "DEFAULT");
                                WIM_Admin = cReg.RegLoad("WIM_Admin", image.MountPath + "\\Users\\Administrator\\NTUSER.DAT");
                                WIM_Default = cReg.RegLoad("WIM_Default", image.MountPath + "\\Users\\Default\\NTUSER.DAT");
                                WIM_System = cReg.RegLoad("WIM_System", SHiveL + "SYSTEM");
                                cReg.RegLoad("WIM_SAM", SHiveL + "SAM");
                                cReg.RegLoad("WIM_Components", SHiveL + "COMPONENTS");
                                cMain.UpdateToolStripLabel(lblStatus, "Registry Loaded...");

                            }

                            try
                            {
                                if (lstUpdates.Items.Count > 0 && WIM_System)
                                {
                                    if (lstUpdates.FindItemWithText("KB2727118") != null)
                                    {
                                        cReg.WriteValue(Registry.LocalMachine, "WIM_System\\ControlSet001\\Control\\Terminal Server", "LsmServiceStartTimeout", 1107296255, RegistryValueKind.DWord);
                                        cReg.WriteValue(Registry.LocalMachine, "WIM_System\\ControlSet002\\Control\\Terminal Server", "LsmServiceStartTimeout", 1107296255, RegistryValueKind.DWord);

                                    }
                                    if (lstUpdates.FindItemWithText("KB2732072") != null)
                                    {
                                        cReg.WriteValue(Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows\\CurrentVersion\\BITS", "BITSRealisticEstimate", 1, RegistryValueKind.DWord);
                                    }
                                }
                            }
                            catch { }

                            if (XPMode && WIM_Software)
                            {
                                cReg.WriteValue(Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows XP Mode", "InstallDir", "%ProgramFiles%\\Windows XP Mode\\Windows XP Mode base.vhd");
                                cReg.WriteValue(Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows XP Mode", "TutorialPath", "%ProgramFiles%\\Windows XP Mode\\Tutorial\\VXPTutorial.html");
                            }

                            //Default Theme
                            try
                            {
                                if (WIM_Software)
                                {

                                    cReg.DeleteValue(Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", "0Theme_1");
                                    cReg.DeleteValue(Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", "0Theme_2");

                                    if (restoreTheme)
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "Restoring Default Theme...");
                                        cReg.WriteValue(Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows\\CurrentVersion\\Themes",
                                            "InstallTheme", "%SystemDrive%\\Windows\\Resources\\Themes\\Aero.theme");


                                        cReg.DeleteValue(Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", "0Theme_1");
                                        cReg.DeleteValue(Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", "0Theme_2");
                                    }

                                    if (!string.IsNullOrEmpty(defaultTheme) && newTheme)
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "Setting Default Theme...");
                                        defaultTheme = defaultTheme.ReplaceIgnoreCase(image.MountPath, "%SystemDrive%");
                                        cReg.WriteValue(Registry.LocalMachine, "WIM_Default\\Software\\Policies\\Microsoft\\Windows\\Personalization",
                                           "ThemeFile", defaultTheme);


                                    }
                                }
                            }
                            catch
                            {
                            }

                            //SoundThemes
                            cMain.FreeRAM();
                            try
                            {
                                if (lstComponents.CheckedItems.Count > 0 && !BWRun.CancellationPending && !bAbort)
                                {
                                    if (lstComponents.FindItemWithText("Sound Themes") != null)
                                    {
                                        //TabControl1.SelectedTab = TabComponents;
                                        if (lstComponents.FindItemWithText("Sound Themes").Checked)
                                        {
                                            cMain.UpdateToolStripLabel(lblStatus, "Removing Sound Themes...");
                                            ComponentClass.RemoveSoundSchemes();
                                            ChangeProgress();
                                        }
                                    }

                                }
                            }
                            catch
                            {
                            }

                            //Services

                            if (lstServices.CheckedItems.Count > 0 && !BWRun.CancellationPending && !bAbort && WIM_System)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Editing Services...");
                                //TabControl1.SelectedTab = TabServices;
                                foreach (ListViewItem LST in lstServices.CheckedItems)
                                {
                                    cMain.UpdateToolStripLabel(lblStatus, "Editing Service " + (LST.Index + 1) + " of " +
                                                                 lstServices.CheckedItems.Count + " (" + LST.Text + ")");

                                    try
                                    {
                                        string sSerReg = "WIM_System\\ControlSet001\\services\\" + LST.SubItems[3].Text;

                                        if (cReg.RegCheckMounted(sSerReg))
                                        {
                                            cReg.DeleteValue(Registry.LocalMachine, sSerReg, "DelayedAutostart");

                                            switch (LST.SubItems[1].Text)
                                            {
                                                case "Automatic (Delayed Start)":
                                                    cReg.WriteValue(Registry.LocalMachine, sSerReg, "Start", 4,
                                                                            RegistryValueKind.DWord);
                                                    cReg.WriteValue(Registry.LocalMachine, sSerReg, "DelayedAutoStart", 1,
                                                                            RegistryValueKind.DWord);
                                                    break;
                                                case "Automatic":
                                                    cReg.WriteValue(Registry.LocalMachine, sSerReg, "Start", 2,
                                                                            RegistryValueKind.DWord);
                                                    break;
                                                case "Manual":
                                                    cReg.WriteValue(Registry.LocalMachine, sSerReg, "Start", 3,
                                                                            RegistryValueKind.DWord);
                                                    break;
                                                case "Disabled":
                                                    cReg.WriteValue(Registry.LocalMachine, sSerReg, "Start", 4,
                                                                            RegistryValueKind.DWord);
                                                    break;
                                                case "System":
                                                    cReg.WriteValue(Registry.LocalMachine, sSerReg, "Start", 1,
                                                                            RegistryValueKind.DWord);
                                                    break;
                                            }
                                        }
                                    }
                                    catch (Exception Ex)
                                    {
                                        LogErr("Error changing service: " + LST.Text, Ex.Message);
                                        cMain.WriteLog(this, "Error changing services" + Environment.NewLine + LST.Text, Ex.Message, lblStatus.Text);
                                    }

                                    ChangeProgress();
                                    cMain.FreeRAM();
                                    if (BWRun.CancellationPending || bAbort) { break; }
                                }
                            }


                            //SILENT INSTALLERS
                            cMain.FreeRAM();
                            cMain.UpdateToolStripLabel(lblStatus, "Checking for Silent Installers...");
                            if (lstOptions.FindItemWithText("Delete Silent Installers").Checked && !BWRun.CancellationPending && !bAbort && !string.IsNullOrEmpty(DVD) && WIM_Software)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Removing old silent installers...");
                                Application.DoEvents();
                                Files.DeleteFolder(DVD + "WinToolkit_Apps", false);

                                try
                                {
                                    cReg.DeleteKey(Registry.LocalMachine, "WIM_Software", "WinToolkit");
                                }
                                catch { }

                                foreach (var value in cReg.GetAllValues(Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce"))
                                {
                                    if (value.ContainsIgnoreCase("WINTOOLKIT") || value.ContainsIgnoreCase("WIN TOOLKIT") || value.ContainsIgnoreCase("W7T"))
                                    {
                                        cReg.DeleteValue(Registry.LocalMachine, "WIM_Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", value);
                                    }

                                }
                            }

                            //SETTING SILENT INSTALLER TO ALWAYS RUN

                            bool Install = true;

                            if (lstSilent.Items.Count == 0 && !cOptions.AddRunOnce)
                            {
                                Install = false;
                            }

                            if (Install)
                            {
                                var runOnceTimeout = lstOptions.FindItemWithText("RunOnce Idle Timeout");
                                string runCommand = "WinToolkitRunOnce.exe";

                                if (runOnceTimeout.Checked)
                                {
                                    runCommand += " /Time:" + runOnceTimeout.SubItems[1].Text;
                                }

                                cReg.WriteValue(Registry.LocalMachine,
                                    "WIM_Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", "1WinToolkit",
                                    runCommand);

                                cMain.WriteResource(Resources.RunOnce,
                                    image.MountPath + "\\Windows\\System32\\WinToolkitRunOnce.exe", null);
                                cMain.WriteResource(Resources.RunOnce_exe,
                                    image.MountPath + "\\Windows\\System32\\WinToolkitRunOnce.exe.config", null);
                            }
                            if (lstSilent.Items.Count > 0 && !BWRun.CancellationPending && !bAbort && !string.IsNullOrEmpty(DVD) && WIM_Software)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Copying Silent Installers...");
                                Application.DoEvents();
                                foreach (ListViewItem LST in lstSilent.Items)
                                {
                                    try
                                    {
                                        Integration.SilentInstaller.Integrate(LST, lblStatus, lstSilent, DVD);
                                    }
                                    catch (Exception Ex)
                                    {
                                        string extendedError = "Status: " + lblStatus.Text +
                                                               Environment.NewLine + "Text: " + LST.Text +
                                                               Environment.NewLine + "S1: " + LST.SubItems[1].Text +
                                                               Environment.NewLine + "S2: " + LST.SubItems[2].Text +
                                                               Environment.NewLine + "S3: " + LST.SubItems[3].Text +
                                                               Environment.NewLine + "S4: " + LST.SubItems[4].Text +
                                                               Environment.NewLine + "DVD: " + DVD;

                                        LogErr("Error integrating silent installer: " + LST.Text, Ex.Message);
                                        new LargeError("Silent Installer Error", "Silent Installer Integration", extendedError, Ex, LST).Upload();
                                    }

                                    ChangeProgress();
                                    cMain.FreeRAM();

                                    if (BWRun.CancellationPending) break;
                                }
                            }

                            //Prompt Silent
                            cMain.FreeRAM();
                            if (lstOptions.FindItemWithText("Prompt Silent").Checked && !BWRun.CancellationPending && !bAbort && cReg.RegCheckMounted("WIM_Software\\WinToolkit") && WIM_Software)
                            {
                                try
                                {
                                    using (var oRegKey = Registry.LocalMachine.OpenSubKey("WIM_Software\\WinToolkit",
                                                                                                                 RegistryKeyPermissionCheck.ReadSubTree))
                                    {
                                        if (oRegKey != null)
                                        {
                                            foreach (string I in oRegKey.GetValueNames())
                                            {
                                                try
                                                {
                                                    cMain.UpdateToolStripLabel(lblStatus, "Detected Silent Install: " + I);
                                                    var NI = new ListViewItem();
                                                    NI.Group = lstISilent.Groups[i - 1];

                                                    NI.Text = I;
                                                    if (NI.Text.ContainsIgnoreCase("|"))
                                                    {
                                                        while (!NI.Text.StartsWithIgnoreCase("|")) { NI.Text = NI.Text.Substring(1); }
                                                        NI.Text = NI.Text.Substring(1);
                                                    }
                                                    string VAL = oRegKey.GetValue(I).ToString();

                                                    while (VAL.ContainsIgnoreCase("*"))
                                                    {
                                                        VAL = VAL.Substring(1);
                                                    }
                                                    NI.SubItems.Add(VAL);

                                                    NI.SubItems.Add(oRegKey.GetValue(I).ToString().StartsWithIgnoreCase("|")
                                                                                      ? "YES"
                                                                                      : "NO");

                                                    VAL = oRegKey.GetValue(I).ToString();
                                                    while (VAL.ContainsIgnoreCase("*"))
                                                    {
                                                        VAL = VAL.Substring(0, VAL.Length - 1);
                                                    }
                                                    while (!VAL.EndsWithIgnoreCase("\\"))
                                                    {
                                                        VAL = VAL.Substring(0, VAL.Length - 1);
                                                    }
                                                    if (VAL.StartsWithIgnoreCase("|"))
                                                    {
                                                        VAL = VAL.Substring(1);
                                                    }

                                                    NI.SubItems.Add(cMain.GetSize(VAL.ReplaceIgnoreCase("%DVD%:\\", DVD, true)));

                                                    VAL = oRegKey.GetValue(I).ToString();
                                                    while (VAL.ContainsIgnoreCase("*"))
                                                    {
                                                        VAL = VAL.Substring(0, VAL.Length - 1);
                                                    }

                                                    if (VAL.StartsWithIgnoreCase("|"))
                                                    {
                                                        VAL = VAL.Substring(1);
                                                    }
                                                    NI.SubItems.Add(VAL);
                                                    lstISilent.Items.Add(NI);
                                                }
                                                catch
                                                {
                                                }
                                                cMain.FreeRAM();
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                }

                            }
                            cMain.UpdateToolStripLabel(lblStatus, "Checking for Addons...");
                            //ADDONS
                            cMain.FreeRAM();
                            if (lstAddons.Items.Count > 0 && !BWRun.CancellationPending && !bAbort && WIM_Software && WIM_System && WIM_Default)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Integrating Addons...");

                                foreach (ListViewItem LST in lstAddons.Items)
                                {
                                    try
                                    {
                                        if (File.Exists(LST.SubItems[5].Text))
                                        {
                                            Application.DoEvents();

                                            Addon addon = (Addon)LST.Tag;
                                            cMain.UpdateToolStripLabel(lblStatus, "Extracting Addon: " + (LST.Index + 1) + " of " + lstAddons.Items.Count + " (" + LST.Text + ")");
                                            LST.ImageIndex = 3;

                                            addon.Extract();
                                            addon.PropertyChanged += delegate (object o, PropertyChangedEventArgs args)
                                            {
                                                cMain.UpdateToolStripLabel(lblStatus, "Integrating Addon: " + (LST.Index + 1) + " of " + lstAddons.Items.Count + " (" + LST.Text + ") :: " + args.PropertyName);
                                            };

                                            cMain.UpdateToolStripLabel(lblStatus, "Integrating Addon: " + (LST.Index + 1) + " of " + lstAddons.Items.Count + " (" + LST.Text + ")");

                                            List<Exception> addonEx = addon.Integrate();

                                            cMain.UpdateToolStripLabel(lblStatus, "Cleaning Addon: " + (LST.Index + 1) + " of " + lstAddons.Items.Count + " (" + LST.Text + ")");
                                            LST.ImageIndex = 1;

                                            if (addonEx.Count > 0)
                                            {

                                                LST.ImageIndex = 9;
                                                if (lstOptions.FindItemWithText("Debug Addons").Checked)
                                                {
                                                    string exceptions = "";
                                                    foreach (Exception ex in addonEx)
                                                    {
                                                        exceptions += ex.ToString() + Environment.NewLine +
                                                                      Environment.NewLine;
                                                    }
                                                    cMain.ErrorBox("This addon has exceptions.\n\n" + Path.GetFileName(addon.AddonPath), "Debug Mode", exceptions);
                                                }
                                            }
                                            else
                                            {
                                                LST.ImageIndex = 6;
                                            }

                                            Application.DoEvents();
                                            addon.Clean();
                                        }
                                    }
                                    catch (Exception Ex)
                                    {
                                        LogErr("error integrating addons.", Ex.Message);
                                        new SmallError("Error Integrating Addon", Ex, LST.Text + "\n" + lblStatus).Upload();
                                    }
                                    ChangeProgress();
                                    cMain.FreeRAM();
                                    if (BWRun.CancellationPending || bAbort) { break; }
                                }


                            }

                            //PROMPT Addons
                            cMain.FreeRAM();
                            cMain.UpdateToolStripLabel(lblStatus, "Checking for integrated addons...");
                            if (lstOptions.FindItemWithText("Prompt Addons").Checked && !BWRun.CancellationPending && !bAbort && Directory.Exists(image.MountPath + "\\Windows\\WinToolkit\\Addons\\"))
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Retrieving integrated addons...");
                                try
                                {
                                    cMain.UpdateToolStripLabel(lblStatus, "Getting Addons List...");

                                    foreach (string F in Directory.GetFiles(image.MountPath + "\\Windows\\WinToolkit\\Addons\\", "*.txt"))
                                    {
                                        try
                                        {
                                            string rAddon = "";
                                            using (var RA = new StreamReader(F))
                                            {
                                                rAddon = RA.ReadToEnd();
                                            }
                                            string aName = null;
                                            string aCreator = null;
                                            string aVersion = null;
                                            string aArc = null;
                                            string ADesc = null;
                                            string aWebsite = "N/A";

                                            foreach (string strAddons in rAddon.Split(Environment.NewLine.ToCharArray()))
                                            {
                                                if (strAddons.StartsWithIgnoreCase("NAME="))
                                                    aName = cAddon.GetValue(strAddons);
                                                if (strAddons.StartsWithIgnoreCase("CREATOR="))
                                                    aCreator = cAddon.GetValue(strAddons);
                                                if (strAddons.StartsWithIgnoreCase("VERSION="))
                                                    aVersion = cAddon.GetValue(strAddons);
                                                if (strAddons.StartsWithIgnoreCase("ARC="))
                                                    aArc = cAddon.GetValue(strAddons);
                                                if (strAddons.StartsWithIgnoreCase("DESCRIPTION="))
                                                    ADesc = cAddon.GetValue(strAddons);
                                                if (strAddons.StartsWithIgnoreCase("WEBSITE="))
                                                    aWebsite = cAddon.GetValue(strAddons);
                                            }

                                            var NI = new ListViewItem();
                                            NI.Text = aName;
                                            NI.SubItems.Add(aCreator);
                                            NI.SubItems.Add(aVersion);
                                            NI.SubItems.Add(aArc);
                                            NI.SubItems.Add(ADesc);
                                            NI.SubItems.Add(aWebsite);
                                            NI.Group = lstIAddons.Groups[i - 1];
                                            lstIAddons.Items.Add(NI);
                                        }
                                        catch
                                        {
                                        }

                                    }
                                }
                                catch (Exception Ex)
                                {
                                    LogErr("Error detecting addons.", Ex.Message);
                                    cMain.WriteLog(this, "Error detecting addons", Ex.Message, lblStatus.Text);
                                }
                            }

                            cMain.UpdateToolStripLabel(lblStatus, "Checking for Tweaks...");
                            cMain.FreeRAM();
                            //TWEAKS

                            try
                            {
                                if (CountNodes(tvTweaks) > 0 && !BWRun.CancellationPending && !bAbort && WIM_Software && WIM_System && WIM_Default)
                                {
                                    cMain.UpdateToolStripLabel(lblStatus, "Preparing Tweaks...");
                                    //TabControl1.SelectedTab = TabTweaks;
                                    if (lstOptions.FindItemWithText("Current OS").Checked)
                                    {
                                        cTweaks.IntegrateTweaks(tvTweaks, lblStatus, cMain.Arc64 ? "x64" : "x86", "C:\\",
                                                                                  false,
                                                                                  lstOptions.FindItemWithText("Convert Registry Test").
                                                                                         Checked, PB, this, image);
                                    }
                                    else
                                    {
                                        cTweaks.IntegrateTweaks(tvTweaks, lblStatus, image.Architecture.ToString(), image.MountPath, true, lstOptions.FindItemWithText("Convert Registry Test").Checked, PB, this, image);
                                    }
                                }
                            }
                            catch (Exception Ex)
                            {
                                LogErr("Error integrating tweaks.", Ex.Message);
                                cMain.WriteLog(this, "Error integrating tweaks", Ex.Message, lblStatus.Text);
                            }

                        }
                        else
                        {
                            cMain.UpdateToolStripLabel(lblStatus, i + "\\" + cMain.selectedImages.Count + " - " + image.Name + " :: Error Mounting WIM...");
                        }
                    }
                    catch (Exception ex)
                    {
                        int sub = 1;

                        new SmallError("Error in main integration", ex, image.Name + "\n" + lblStatus.Text + "\n\n" + sub).Upload();
                        LogErr(image.Name, ex.Message);

                        if (!string.IsNullOrEmpty(image.MountPath)) { bAbort = true; }
                    }

                    cReg.WIMt = false;
                    //UNLOAD REGISTRY
                    if (lstAddons.Items.Count > 0 || CountNodes(tvTweaks) > 0 || lstServices.CheckedItems.Count > 0 || lstSilent.Items.Count > 0 || XPMode)
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Unloading Registry...");
                        cMain.UpdateToolStripLabel(lblStatus, i + "\\" + cMain.selectedImages.Count + " - " + image.Name + " :: Unloading Registry");
                        cReg.RegUnLoadAll();
                    }
                    cMain.FreeRAM();
                    XPMode = false;
                    cMain.UpdateToolStripLabel(lblStatus, "Preparing to dismount image...");
                    cMount.uChoice = cMount.MountStatus.None;

                    Application.DoEvents();

                    if (bAbort)
                    {
                        cMount.CWIM_UnmountImage(image.Name, lblStatus, true, false, false, this, image.Location, false);
                    }
                    else
                    {
                        if (!lstOptions.FindItemWithText("Prompt Unmount").Checked)
                        {
                            cMain.UpdateToolStripLabel(lblStatus, (i) + "\\" + cMain.selectedImages.Count + " - " + image.Name + " :: Unmounting Image");

                            if (cOptions.AIOSave)
                            {
                                cMount.CWIM_UnmountImage(image.Name, lblStatus, false,
                                                                            BWRun.CancellationPending, false, this,
                                                                            image.Location, false);
                                cMain.FreeRAM();
                            }
                            else
                            {
                                cMount.CWIM_UnmountImage(image.Name, lblStatus, false, true, false, this, image.Location, false);
                                cMain.FreeRAM();
                            }
                        }
                        else
                        {
                            if (cMain.selectedImages.Count == 1)
                            {
                                cMount.CWIM_UnmountImage(image.Name, lblStatus, false, true, true, this, image.Location, false);
                            }
                            else
                            {
                                MessageBox.Show("Win Toolkit is about to unmount the following image:" + Environment.NewLine + Environment.NewLine + "[" + image.Name + "]", "Notify");
                                cMount.CWIM_UnmountImage(image.Name, lblStatus, false,
                                                                          BWRun.CancellationPending, false, this,
                                                                          image.Location, false);
                            }
                            cMain.FreeRAM();

                            bAbort = false;
                        }
                    }
                    if (BWRun.CancellationPending || cMount.uChoice == cMount.MountStatus.Keep)
                        break;
                    i += 1;
                    cMain.FreeRAM();
                }
                cMain.UpdateToolStripLabel(lblStatusM, "");
                cMain.UpdateToolStripLabel(lblStatus, "Finishing Unmounting...");
                Application.DoEvents();
                Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
                cMain.FreeRAM();
            }

            cMain.UpdateToolStripLabel(lblStatus, "Deleting IE Temp Files...");
            Application.DoEvents();
            if (Directory.Exists(cOptions.WinToolkitTemp + "\\IE"))
            {
                cMain.UpdateToolStripLabel(lblStatus, "Deleting IE Temp Files...");
                Files.DeleteFolder(cOptions.WinToolkitTemp + "\\IE", false);
            }

            string BW = "";
            try
            {
                BW = cMain.selectedImages[0].Location;
            }
            catch { }

            cMain.UpdateToolStripLabel(lblStatus, "Checking if boot.wim is needed [ " + BW + "]");
            lblStatusM.Visible = false;

            try
            {
                if (File.Exists(bWim) && (tSB && File.Exists(tSBCT)) && !BW.ContainsIgnoreCase("BOOT.WIM") && !BWRun.CancellationPending && !bAbort)
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Scanning boot.wim #1...");
                    lblStatusM.Visible = false;
                    string bWimInfo1 = "";
                    string bWimInfoF1 = "";

                    try
                    {
                        bWimInfo1 = cMount.CWIM_GetWimImageInfo(bWim, lblStatus);
                        bWimInfoF1 = bWimInfo1;
                        if (bWimInfo1.ContainsIgnoreCase("</NAME>"))
                        {
                            while (bWimInfo1.ContainsIgnoreCase("</NAME>"))
                            {
                                bWimInfo1 = bWimInfo1.Substring(0, bWimInfo1.Length - 1);
                            }

                            while (bWimInfo1.ContainsIgnoreCase(">"))
                            {
                                bWimInfo1 = bWimInfo1.Substring(1);
                            }

                            while (bWimInfo1.ContainsIgnoreCase("<"))
                            {
                                bWimInfo1 = bWimInfo1.Substring(0, bWimInfo1.Length - 1);
                            }
                        }
                        else
                        {
                            bWimInfo1 = "N/A";
                        }
                    }
                    catch (Exception Ex)
                    {
                        LogErr("Could not scan boot.wim index (PE)" + Environment.NewLine + bWimInfoF1, Ex.Message);
                        cMain.WriteLog(this, "Could not scan boot.wim index (PE)", Ex.Message, bWimInfoF1);
                    }

                    cMain.UpdateToolStripLabel(lblStatus, "Preparing boot.wim...");
                    Application.DoEvents();
                    if (!Directory.Exists(cOptions.WinToolkitTemp + "\\Boot"))
                    {
                        cMain.CreateDirectory(cOptions.WinToolkitTemp + "\\Boot");
                    }

                    cMain.TakeOwnership(bWim);
                    cMain.ClearAttributeFile(bWim);
                    cMain.UpdateToolStripLabel(lblStatus, "Mounting boot.wim 1...");
                    Application.DoEvents();
                    cMain.UpdateToolStripLabel(lblStatusM, "Boot Image: 1 of 2");
                    //bWimInfo1

                    string BMPE = cMount.CWIM_MountImage(bWimInfo1, 1, bWim, cOptions.WinToolkitTemp + "\\BootPE", lblStatus, this);
                    if (!string.IsNullOrEmpty(BMPE))
                    {
                        try
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Changing Setup Background (Boot PE)...");
                            if (!BWRun.CancellationPending && !bAbort)
                            {
                                foreach (string S in Directory.GetFiles(BMPE + "\\Windows\\", "*.*", SearchOption.AllDirectories))
                                {
                                    if (BWRun.CancellationPending || bAbort)
                                    {
                                        break;
                                    }

                                    try
                                    {
                                        if (S.ToUpper().EndsWithIgnoreCase("WINPE.BMP"))
                                        {
                                            cMain.TakeOwnership(S);
                                            if (File.Exists(tSBCT))
                                            {
                                                File.Copy(tSBCT, S, true);
                                            }
                                        }
                                    }
                                    catch (Exception Ex)
                                    {
                                        LogErr("Unable to set background (Boot PE)" + Environment.NewLine + S, Ex.Message);
                                        cMain.WriteLog(this, "Unable to set background (Boot PE)" + Environment.NewLine + S, Ex.Message, lblStatus.Text);
                                    }
                                    cMain.FreeRAM();
                                }
                            }
                        }
                        catch
                        {
                        }
                        cMain.FreeRAM();
                        cMount.CWIM_UnmountImage(bWimInfo1, lblStatus, false, BWRun.CancellationPending, false,
                                                                    this, bWim, false);
                        cMain.FreeRAM();
                    }
                    else
                    {
                        string mLog = "", A;

                        try
                        {
                            if (cOptions.MountLog && File.Exists(cMain.Root + "Logs\\Mount Logs\\" + bWimInfo1 + ".log"))
                            {
                                using (var SR = new StreamReader(cMain.Root + "Logs\\Mount Logs\\" + bWimInfo1 + ".log"))
                                {
                                    mLog = SR.ReadToEnd();
                                }
                            }
                        }
                        catch
                        {
                        }

                        try
                        {
                            var DI = new DriveInfo(bWim.Substring(0, 1));
                            A = " - " + DI.DriveType;
                        }
                        catch (Exception Ex)
                        {
                            A = " - DRIVE:" + Ex.Message;
                        }



                        // + Environment.NewLine + bWimInfoF1 + Environment.NewLine
                        LogErr("Could not mount boot.wim (PE) " + A + Environment.NewLine + bWim + Environment.NewLine + mLog, cMount.sErr);


                        cMain.WriteLog(this, "Could not mount boot.wim (PE)" + A, bWim + "---" + Environment.NewLine + cMount.sErr, cMain.selectedImages[0].Architecture + Environment.NewLine + mLog);
                    }
                }
            }
            catch
            {
            }

            cMain.FreeRAM();
            cMain.UpdateToolStripLabel(lblStatus, "Checking if boot.wim(2) needs mounting...");
            Application.DoEvents();
            if (SysDriver > 0 || SysLang > 0 || (tSB && File.Exists(tSBCT)))
            {
                if (File.Exists(bWim) && !BW.ContainsIgnoreCase("BOOT.WIM") && !BWRun.CancellationPending && !bAbort)
                {
                    try
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Scanning boot #2...");
                        lblStatusM.Visible = false;
                        string bWimInfo = "";
                        string bWimInfoF = "";

                        try
                        {
                            bWimInfo = cMount.CWIM_GetWimImageInfo(bWim, lblStatus);
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
                            LogErr("Could not scan boot.wim index" + Environment.NewLine + bWimInfoF, Ex.Message);
                            cMain.WriteLog(this, "Could not scan boot.wim index", Ex.Message, bWimInfoF);
                        }

                        if (lstOptions.FindItemWithText("Boot.wim Test").Checked)
                        {
                            MessageBox.Show(bWimInfo);
                        }
                        cMain.FreeRAM();
                        cMain.UpdateToolStripLabel(lblStatus, "Mounting 2nd boot.wim image...");
                        Application.DoEvents();
                        cMain.UpdateToolStripLabel(lblStatusM, "Boot Image: 2 of 2");

                        string BM = cMount.CWIM_MountImage(bWimInfo, 2, bWim, cOptions.WinToolkitTemp + "\\Boot", lblStatus, this);

                        if (string.IsNullOrEmpty(BM))
                        {
                            cMount.CWIM_MountImage(bWimInfo, 1, bWim, cOptions.WinToolkitTemp + "\\Boot", lblStatus, this);
                        }

                        if (!string.IsNullOrEmpty(BM))
                        {
                            cMain.FreeRAM();
                            string bArc = Directory.Exists(BM + "\\Windows\\SysWOW64\\") ? "x64" : "x86";

                            if (tSB && File.Exists(tSBCT))
                            {
                                try
                                {
                                    cMain.UpdateToolStripLabel(lblStatus, "Changing Setup Background (Boot Setup)...");

                                    cMain.WriteResource(Resources.ResHacker, cMain.UserTempPath + "\\ResHacker.exe", this);
                                    try
                                    {
                                        if (File.Exists(BM + "\\Sources\\spwizimg.dll"))
                                        {
                                            if (!File.Exists(SP + ".bak") && File.Exists(SP))
                                            {
                                                File.Copy(SP, SP + ".bak");
                                                Files.DeleteFile(SP);
                                            }
                                            File.Copy(BM + "\\Sources\\spwizimg.dll", SP, true);
                                        }
                                    }
                                    catch (Exception Ex)
                                    {
                                        cMain.WriteLog(this, "Could not change setup background (Boot Setup)", Ex.Message, lblStatus.Text);
                                        LogErr("Could not change setup background (Boot Setup)\nSP=" + SP, Ex.Message);
                                        if (File.Exists(SP + ".bak")) { Files.DeleteFile(SP); File.Move(SP + ".bak", SP); }
                                    }

                                    foreach (string S in Directory.GetFiles(BM, "*.*", SearchOption.AllDirectories))
                                    {
                                        if (BWRun.CancellationPending || bAbort) { break; }

                                        try
                                        {
                                            if (S.ToUpper().EndsWithIgnoreCase("BACKGROUND.BMP") || S.ToUpper().EndsWithIgnoreCase("SETUP.BMP") || S.ToUpper().EndsWithIgnoreCase("WINPE.BMP"))
                                            {
                                                cMain.TakeOwnership(S);
                                                File.Copy(tSBCT, S, true);
                                            }

                                            if (S.ToUpper().EndsWithIgnoreCase("SPWIZIMG.DLL"))
                                            {
                                                cMain.TakeOwnership(S);
                                                cMain.OpenProgram("\"" + cMain.UserTempPath + "\\ResHacker.exe\"", "-addoverwrite \"" + S + "\", \"" + S + "\", \"" + tSBCT + "\", BITMAP, 517, 1033", true, ProcessWindowStyle.Hidden);
                                                cMain.OpenProgram("\"" + cMain.UserTempPath + "\\ResHacker.exe\"", "-addoverwrite \"" + S + "\", \"" + S + "\", \"" + tSBCT + "\", BITMAP, 518, 1033", true, ProcessWindowStyle.Hidden);
                                            }
                                        }
                                        catch (Exception Ex)
                                        {
                                            cMain.WriteLog(this, "Unable to set background (Boot Setup)" + Environment.NewLine + S, Ex.Message, lblStatus.Text);
                                            LogErr("Unable to set background (Boot Setup)" + Environment.NewLine + S, Ex.Message);
                                        }
                                        cMain.FreeRAM();
                                    }
                                }
                                catch (Exception Ex)
                                {
                                }
                                cMain.UpdateToolStripLabel(lblStatus, "");
                            }

                            if (SysLang > 0)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Copying lang.ini to boot.wim");
                                string T = "";
                                try
                                {
                                    ResetProgress(SysLang);

                                    string lINI = cMain.selectedImages[0].Location;
                                    if (lINI.ContainsIgnoreCase("\\"))
                                    {
                                        while (!lINI.EndsWithIgnoreCase("\\"))
                                        {
                                            lINI = lINI.Substring(0, lINI.Length - 1);
                                        }

                                        lINI += "Lang.ini";
                                    }

                                    //MAYBE ADD CODE HERE TO CREATE RANDOM TXT FILE SOURCES FOLDER

                                    try
                                    {
                                        T += File.Exists(lINI) ? "TRUE" : "lINI: FALSE";
                                        T += Directory.Exists(BM) ? " TRUE" : " BM: FALSE";
                                        T += Directory.Exists(BM + "\\sources\\") ? " TRUE" : " BMS: FALSE";

                                        if (File.Exists(BM + "\\sources\\lang.ini"))
                                        {
                                            Files.DeleteFile(BM + "\\sources\\lang.ini.bak");
                                            File.Move(BM + "\\sources\\lang.ini", BM + "\\sources\\lang.ini.bak");
                                        }
                                        File.Copy(lINI, BM + "\\sources\\lang.ini");
                                    }
                                    catch (Exception Ex)
                                    {
                                        LogErr("Error adding Lang.ini to DVD (" + T + ")" + Environment.NewLine + DVD, Ex.Message + "::" + bWimInfo + "[" + lINI + "]\n#" + LangINI);
                                        cMain.WriteLog(this, "Error adding Lang.ini to DVD (" + T + ")", Ex.Message + "::" + bWimInfo + "\n[lINI: " + lINI + "]\nBM: " + BM + lINI + "]\nBMSource: " + BM + "\\sources\\", DVD);
                                        if (File.Exists(BM + "\\sources\\lang.ini.bak"))
                                        {
                                            Files.DeleteFile(BM + "\\sources\\lang.ini");
                                            File.Move(BM + "\\sources\\lang.ini.bak", BM + "\\sources\\lang.ini");
                                        }
                                    }

                                    cMain.UpdateToolStripLabel(lblStatus, "Adding Languages to boot.wim");
                                    Application.DoEvents();
                                    foreach (ListViewItem LST in lstUpdates.Items)
                                    {
                                        if (BWRun.CancellationPending || bAbort)
                                        {
                                            break;
                                        }

                                        try
                                        {
                                            cMain.UpdateToolStripLabel(lblStatus, "Adding Language: " + LST.Text);
                                            string IF = LST.SubItems[5].Text; //**
                                            if (LST.Group.Header.EqualsIgnoreCase("Language Packs") && IF.ToUpper().EndsWithIgnoreCase(".CAB") && File.Exists(LST.SubItems[5].Text)) //**
                                            {
                                                if (IF.ContainsForeignCharacters())
                                                {
                                                    IF = cOptions.WinToolkitTemp + "\\" + LST.Index + IF.Substring(IF.Length - 4);

                                                    File.Copy(LST.SubItems[5].Text, IF, true); //**
                                                }

                                                Files.DeleteFolder(cOptions.WinToolkitTemp + "\\LP1", true);
                                                Files.DeleteFolder(cOptions.WinToolkitTemp + "\\LP2", true);

                                                cMain.UpdateToolStripLabel(lblStatus, "Extracting Languages #1 Boot.wim [" + LST.Text + "] - This may take some time...");
                                                cMain.ExtractFiles(IF, cOptions.WinToolkitTemp + "\\LP1", this, "sources\\*", false);
                                                cMain.UpdateToolStripLabel(lblStatus, "Extracting Languages #2 Boot.wim [" + LST.Text + "] - This may take some time...");
                                                cMain.ExtractFiles(IF, cOptions.WinToolkitTemp + "\\LP2", this, "setup\\*", false);

                                                if (Directory.Exists(cOptions.WinToolkitTemp + "\\LP1\\sources\\license"))
                                                {
                                                    cMain.UpdateToolStripLabel(lblStatus, "Copying Languages #1 Boot.wim [" + LST.Text + "]");
                                                    cMain.CopyDirectory(cOptions.WinToolkitTemp + "\\LP1\\sources\\license", BM + "\\sources\\license", true, false);
                                                }

                                                if (Directory.Exists(cOptions.WinToolkitTemp + "\\LP2\\setup\\sources"))
                                                {
                                                    cMain.UpdateToolStripLabel(lblStatus, "Copying Languages #2 Boot.wim [" + LST.Text + "]");
                                                    cMain.CopyDirectory(cOptions.WinToolkitTemp + "\\LP2\\setup\\sources", BM + "\\sources", true, false);
                                                }

                                                cMain.UpdateToolStripLabel(lblStatus, "Cleaning Language Temp Files [" + LST.Text + "]");
                                                Files.DeleteFolder(cOptions.WinToolkitTemp + "\\LP1", false);
                                                Files.DeleteFolder(cOptions.WinToolkitTemp + "\\LP2", false);

                                            }
                                        }
                                        catch (Exception Ex)
                                        {
                                            string sLog = "DirExists: " + Directory.Exists(cOptions.WinToolkitTemp + "\\LP\\setup\\sources");
                                            new SmallError("Error copying languages to boot.wim", Ex, LST.Text + "\n" + sLog, LST).Upload();

                                            LogErr("Error copying languages to boot.wim" + Environment.NewLine + LST.Text + "\n" + LST.SubItems[5].Text, Ex.Message); //**
                                        }
                                        cMain.FreeRAM();
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    LogErr("Error adding languages to boot.wim" + Environment.NewLine + DVD, Ex.Message);
                                    cMain.WriteLog(this, "Error adding languages to boot.wim", Ex.Message, DVD);
                                }
                            }

                            if (SysDriver > 0) //
                            {
                                ResetProgress(SysDriver);
                                cMain.UpdateToolStripLabel(lblStatus, "Integrating Drivers...");
                                var NGBoot = new ListViewGroup();
                                NGBoot.Header = !string.IsNullOrEmpty(bWimInfo) ? bWimInfo : "Boot Image";
                                NGBoot.HeaderAlignment = HorizontalAlignment.Center;
                                lstIDrivers.Groups.Add(NGBoot);

                                int DI = 1;
                                foreach (ListViewItem LST in lstDrivers.Items)
                                {
                                    if (BWRun.CancellationPending || bAbort) { break; }
                                    if (bArc == LST.SubItems[7].Text) //
                                    {
                                        if (LST.ImageIndex == 0)
                                        {
                                            cMain.UpdateToolStripLabel(lblStatus, "Integrating " + LST.SubItems[1].Text + " Driver: " + (DI) + " of " + SysDriver + " (" + LST.SubItems[6].Text + ")");

                                            string IF = LST.SubItems[6].Text;
                                            string F2 = LST.SubItems[6].Text;
                                            string CF = LST.SubItems[6].Text;

                                            while (!CF.EndsWithIgnoreCase("\\")) { CF = CF.Substring(0, CF.Length - 1); }

                                            if (Directory.Exists(CF) && File.Exists(IF))
                                            {
                                                try
                                                {
                                                    if (IF.ContainsIgnoreCase(":") && IF.ContainsForeignCharacters())
                                                    {
                                                        string FN = LST.SubItems[6].Text;
                                                        while (FN.ContainsIgnoreCase("\\"))
                                                            FN = FN.Substring(1);

                                                        while (F2.ContainsIgnoreCase("\\"))
                                                            F2 = F2.Substring(0, F2.Length - 1);

                                                        F2 += "\\WinToolkitSYSDriver" + (LST.Index + 1).ToString(CultureInfo.InvariantCulture);
                                                        IF = F2 + "\\" + FN;

                                                        Files.DeleteFolder(F2, false);

                                                        cMain.UpdateToolStripLabel(lblStatus, "Copying " + LST.SubItems[1].Text + " Driver: " + (DI) + " of " + SysDriver + " (" + LST.SubItems[6].Text + ")");
                                                        cMain.CopyDirectory(CF, F2, true, true);
                                                    }
                                                }
                                                catch (Exception Ex)
                                                {
                                                    IF = LST.SubItems[6].Text;
                                                    LogErr("Error moving/copying driver" + Environment.NewLine + F2 + " ||| " + CF, Ex.Message);
                                                    cMain.WriteLog(this, "Error moving/copying driver", Ex.Message, lblStatus.Text + Environment.NewLine + F2 + " ||| " + CF);
                                                }

                                                try
                                                {
                                                    cMain.UpdateToolStripLabel(lblStatus, "Integrating " + LST.SubItems[1].Text + " Driver: " + (DI) + " of " + SysDriver + " (" + LST.SubItems[6].Text + ")");
                                                    Files.DeleteFile(BM + "\\Windows\\inf\\setupapi.offline.log");
                                                    string DIDR = "/Image:\"" + BM + "\" /Add-Driver /Driver:\"" + IF + "\" /English";
                                                    if (lstOptions.FindItemWithText("Force Unsigned").Checked)
                                                    {
                                                        DIDR = DIDR + " /ForceUnsigned";
                                                    }
                                                    string dInt = cMain.RunExternal("\"" + DISM.Latest.Location + "\"", DIDR);
                                                }
                                                catch { }
                                            }

                                        }
                                    }

                                    cMain.FreeRAM();
                                    DI += 1;
                                    if (BWRun.CancellationPending || bAbort)
                                    {
                                        break;
                                    }
                                }
                            }

                            IntegratedDrivers(cOptions.WinToolkitTemp + "\\Boot", -1);

                            cMount.CWIM_UnmountImage(bWimInfo, lblStatus, false, BWRun.CancellationPending, false,
                                                                        this, bWim, false);
                        }
                        else
                        {
                            string mLog = "";
                            try
                            {
                                if (cOptions.MountLog && File.Exists(cMain.Root + "Logs\\Mount Logs\\" + bWimInfo + ".log"))
                                {
                                    using (var SR = new StreamReader(cMain.Root + "Logs\\Mount Logs\\" + bWimInfo + ".log"))
                                    {
                                        mLog = SR.ReadToEnd();
                                    }
                                }
                            }
                            catch
                            {
                            }
                            string A;

                            try
                            {
                                var DI = new DriveInfo(bWim.Substring(0, 1));
                                A = " - " + DI.DriveType;
                            }
                            catch (Exception Ex)
                            {
                                A = " - DRIVE:" + Ex.Message;
                            }
                            // + Environment.NewLine + bWimInfoF + Environment.NewLine
                            LogErr("Could not mount boot.wim" + A, bWim + "---" + Environment.NewLine + cMount.sErr + Environment.NewLine + cMain.selectedImages[0].Architecture + Environment.NewLine + mLog);
                            cMain.WriteLog(this, "Could not mount boot.wim" + A, bWim + "---" + Environment.NewLine + cMount.sErr, cMain.selectedImages[0].Architecture + Environment.NewLine + mLog);
                        }

                        cMain.FreeRAM();
                    }
                    catch (Exception Ex)
                    {
                        LogErr("Error Boot.wim (Setup)" + Environment.NewLine + bWim, Ex.Message);
                        cMain.WriteLog(this, "Error Boot.wim (Setup)", Ex.Message, lblStatus.Text + " " + bWim);
                    }
                }
            }
            cMain.UpdateToolStripLabel(lblStatusM, "");
            cMain.UpdateToolStripLabel(lblStatus, "Removing ResHacker...");
            Application.DoEvents();
            Files.DeleteFile(cMain.UserTempPath + "\\ResHacker.ini");
            Files.DeleteFile(cMain.UserTempPath + "\\ResHacker.log");

            cMain.FreeRAM();

            if (lstOptions.FindItemWithText("Rebuild Image").Checked && cMain.IsFileLocked(cMain.selectedImages[0].Location) == false && !BWRun.CancellationPending && !bAbort)
            {
                cMain.UpdateToolStripLabel(lblStatus, "Rebuilding WIM file, please wait....");
                Application.DoEvents();
                cMount.Rebuild(cMain.selectedImages[0].Location, lblStatus, this, false);

                cMain.FreeRAM();
            }
            cMain.UpdateToolStripLabel(lblStatus, "Finishing...");
        }


        private int UpdateItem(int cIdx, ListViewItem LST, bool bRetrying, int iMax, WIMImage img)
        {
            UpdateInfo.Information subUpdateInfo = new UpdateInfo.Information(img.Name, img.Index);
            bool bIntegrated = false;
            uFile = ""; uIndex = -1;
            LST.ImageIndex = 3;
            if (LST.SubItems.Count < 5)
            {
                new SmallError("Missing subitems:", null, "Retrying:" + bRetrying, LST).Upload();
                LST.ImageIndex = 5;
                ChangeProgress();
                return cIdx;
            }

            string scratchDir = cOptions.WinToolkitTemp + "\\ScratchDir_" + LST.SubItems[5].Text.CreateMD5();
            Files.DeleteFolder(scratchDir, true);



            try
            {

                cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Looking: " + cIdx + " of " + LST.Group.Items.Count + " (" + LST.Text + ")");

                if (!File.Exists(LST.SubItems[5].Text))
                {
                    LST.ImageIndex = 2;
                    ChangeProgress();
                    return cIdx;
                }


                LST.ImageIndex = 4;
                bIntegrated = CheckIntegration(LST);
                cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Starting: " + cIdx + " of " + LST.Group.Items.Count + " (" + LST.Text + ")");


                if (bIntegrated)
                {
                    LST.ImageIndex = 11;
                }
                else
                {
                    LST.ImageIndex = 3;
                    LST.EnsureVisible();
                    IntegrateUpdates(LST, cIdx, bRetrying, iMax, img, ref subUpdateInfo);

                }

            }
            catch (Exception Ex)
            {
                string uErr = LST.Text = Environment.NewLine;
                string sItems = "";
                new SmallError("Error Integrating Update", Ex, LST).Upload();
                LogErr("Error Integrating Update", uErr);
            }



            int imgIndex = LST.ImageIndex;
            LST.ImageIndex = 1;
            cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Cleaning: " + cIdx.ToString(CultureInfo.InvariantCulture) + " of " + LST.Group.Items.Count + " (" + LST.Text + ")");
            CleanTemp("cInst");


            cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Cleaning ScratchDir: " + cIdx.ToString(CultureInfo.InvariantCulture) + " of " + LST.Group.Items.Count + " (" + LST.Text + ")");
            Files.DeleteFolder(scratchDir, false);
            LST.ImageIndex = imgIndex;



            if (!bIntegrated && LST.ImageIndex == 3)
            {
                LST.ImageIndex = 4;
                cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Checking: " + cIdx.ToString() + " of " + LST.Group.Items.Count + " (" + LST.Text + ")");

                if (!bIntegrated && cMain.AppErrC == 0) { bIntegrated = true; }

                if (!bIntegrated) { bIntegrated = CheckIntegration(LST); }

                if (bIntegrated) { LST.ImageIndex = 6; }
                else
                {
                    if (File.Exists(LST.SubItems[5].Text)) { LST.ImageIndex = 5; }
                }
            }


            cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Retrieving: " + cIdx.ToString() + " of " + LST.Group.Items.Count + " (" + LST.Text + ")");
            try
            {
                UpdateInfo ui = (UpdateInfo)LST.Tag;
                if (LST.Tag == null || ui == null)
                {
                    throw new Exception("UI is null");
                }
                if (subUpdateInfo == null)
                {
                    subUpdateInfo = new UpdateInfo.Information(img.Name, img.Index);
                }
                subUpdateInfo.StatusIndex = LST.ImageIndex;
                ui.Info.Add(subUpdateInfo);
            }
            catch (Exception Ex)
            {
                new SmallError("Error setting update info", Ex, LST).Upload();
            }
            ChangeProgress();
            return cIdx;
        }

        private TabPage GetSelectedTab()
        {
            TabPage ST = null;

            if (TPMain.SelectedTab == TabBasic)
            {
                ST = TPBasic.SelectedTab; ;
            }

            if (TPMain.SelectedTab == TabAdvanced)
            {
                ST = TPAdvanced.SelectedTab;
            }

            if (ST == null) { return TPMain.SelectedTab; }
            return ST;
        }

        private void TabChanged(Object sender, EventArgs e)
        {
            mnuA.Visible = false;
            mnuClear.Visible = true;
            if (BWRun.IsBusy || BWAddAddons.IsBusy) { mnuClear.Visible = false; }
            mnuSaveLoad.Visible = false;
            try
            {
                if (BWRun.IsBusy == false)
                {
                    cmdCancel.PerformClick();
                    cmdSICancel.PerformClick();
                }
            }
            catch
            {
            }

            try
            {
                SelTab = GetSelectedTab();
                ListViewEx.LVE = GetSelectedList();

                if (BWRun.IsBusy == false)
                {
                    mnuA.Visible = true;
                    mnuSaveLoad.Visible = true;
                }
                mnuRefresh.Visible = false;
                mnuBV.Visible = false;
                cmdDefaultTheme.Visible = false;
                cmdSBCheckAll.Visible = false;
                cmdSBUnCheckAll.Visible = false;
                cmdSBODefault.Visible = false;
                mnuAddS.Visible = true;
                cmdDeleteInstallers.Visible = false;
                string TabName = GetSelectedTab().Text;
                if (TabName.ContainsIgnoreCase("["))
                {
                    while (TabName.ContainsIgnoreCase("[")) { TabName = TabName.Substring(0, TabName.Length - 1); }
                    TabName = TabName.Substring(0, TabName.Length - 1);
                }

                switch (TabName)
                {
                    case "Addons":
                        mnuA.ToolTipText = "Add any *.WA files you may have so that your favorite programs are pre-installed after Windows installation.";
                        mnuAdd.Text = "Add Addons";
                        if (BWRun.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, "This tab will allow you to add your favorite programs into your image!"); }
                        if (lstAddons.Items.Count > 0 && BWRun.IsBusy == false)
                            mnuR.Visible = true;
                        else
                            mnuR.Visible = false;
                        break;
                    case "Files":
                        mnuA.ToolTipText = "Add any file so that it replaces a file already in the image, this is useful for if you have modified *.dll files.";
                        mnuAdd.Text = "Add Files";
                        mnuAddS.Visible = false;
                        if (BWRun.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, "This tab will let you copy modified files to your image, use with caution!"); }
                        if (lstFiles.Items.Count > 0 && BWRun.IsBusy == false)
                            mnuR.Visible = true;
                        else
                            mnuR.Visible = false;
                        break;
                    case "Component Removal":
                        mnuA.Visible = false;
                        mnuR.Visible = false;
                        if (BWRun.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, "Something included with Windows which you don't use? Then remove it here!"); }
                        if (BWRun.IsBusy == false)
                        {
                            cmdSBCheckAll.Visible = true;
                            cmdSBUnCheckAll.Visible = true;
                        }
                        break;
                    case "vLite":
                        gbCInfo.Width = fpComponents.Width - 17;
                        mnuA.Visible = false;
                        mnuR.Visible = false;
                        if (BWRun.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, "The new and improved component removal tool! This works similar to vLite."); }
                        if (BWRun.IsBusy == false)
                        {
                            cmdSBCheckAll.Visible = true;
                            cmdSBUnCheckAll.Visible = true;
                        }
                        break;
                    case "Silent Installs + SFX":
                        mnuAddS.Visible = false;
                        mnuA.ToolTipText = "Add your *.exe programs so that they are installed silently after Windows installation, note some programs requires switches such as '/s'.";
                        mnuAdd.Text = "Add Silent Installer";
                        cmdDeleteInstallers.Visible = true;
                        if (BWRun.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, "Add silent installers so that your favorite programs are installed seamlessly after Windows Installation."); }
                        if (lstSilent.Items.Count > 0 && BWRun.IsBusy == false)
                            mnuR.Visible = true;
                        else
                            mnuR.Visible = false;
                        break;
                    case "Theme Packs":
                        mnuAdd.Text = "Add Themes";
                        mnuA.ToolTipText = "Add your theme packs so your users have more of a choice after Windows installation.";
                        if (BWRun.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, "Add theme packs and even set a default theme for after Windows installation."); }
                        if (lstThemes.Items.Count > 0 && BWRun.IsBusy == false)
                        {
                            mnuR.Visible = true;
                            cmdDefaultTheme.Visible = true;
                        }
                        else
                        {
                            mnuR.Visible = false;
                            cmdDefaultTheme.Visible = false;
                        }
                        break;
                    case "Gadgets":
                        mnuAdd.Text = "Add Gadgets";
                        mnuA.ToolTipText = "Any gadget added here will be available to your users when they want to add gadgets to their desktop.";
                        if (BWRun.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, "Add your favorite desktop gadgets like CPU Monitor here!"); }
                        if (lstGadgets.Items.Count > 0 && BWRun.IsBusy == false)
                            mnuR.Visible = true;
                        else
                            mnuR.Visible = false;
                        break;
                    case "Wallpapers":
                        mnuAdd.Text = "Add Wallpapers";
                        mnuA.ToolTipText = "Add more wallpapers to your image, so you have more to choose from.";
                        if (BWRun.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, "Add wallpapers to your image so your users have more to choose from!"); }
                        if (lstWallpapers.Items.Count > 0 && BWRun.IsBusy == false)
                            mnuR.Visible = true;
                        else
                            mnuR.Visible = false;
                        break;
                    case "Updates + Languages":
                        mnuAdd.Text = "Add Updates";
                        mnuA.ToolTipText = "You need to add either *.msu or *.cab files, this will help keep your computer up-to-date or even add your required language.";
                        if (BWRun.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, "Keep your image up-to-date so it's more secure, bug free and spend less time updating it!"); }
                        if (lstUpdates.Items.Count > 0 && BWRun.IsBusy == false)
                            mnuR.Visible = true;
                        else
                            mnuR.Visible = false;
                        break;
                    case "Drivers":
                        mnuAdd.Text = "Add Drivers";
                        mnuAddS.Visible = false;
                        mnuA.ToolTipText = "Select the folder containing your drivers, this helps make all of your devices work after installation.";
                        if (BWRun.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, "Hate devices not working after installation? Then add those drivers here so they work straight away!"); }
                        InvalDrivers();
                        if (lstDrivers.Items.Count > 0 && BWRun.IsBusy == false)
                            mnuR.Visible = true;
                        else
                            mnuR.Visible = false;
                        break;
                    case "Services":
                        mnuR.Visible = false;
                        mnuA.Visible = false;
                        if (BWRun.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, "Customize what services Windows runs for better performance!"); }
                        if (BWRun.IsBusy == false && starting == false)
                        {
                            mnuRefresh.Visible = true;
                            mnuBV.Visible = true;
                        }
                        break;
                    case "Tweaks":
                        mnuAdd.Text = "Add *.Reg";
                        mnuA.ToolTipText = "Add your own custom *.reg file so you have all of your favorite tweaks integrated.";
                        if (BWRun.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, "Import and/or select tweaks to customize Windows to the way you want it!"); }
                        mnuR.Visible = false;
                        foreach (TreeNode TN in tvTweaks.Nodes)
                        {
                            if (TN.Tag != null && TN.Tag.ToString().EqualsIgnoreCase("Reg") && BWRun.IsBusy == false) { mnuR.Visible = true; }
                        }
                        break;
                    case "Options":
                        mnuA.Visible = false;
                        mnuR.Visible = false;
                        if (BWRun.IsBusy == false) { cMain.UpdateToolStripLabel(lblStatus, "This tab lets you change how the All-In-One Integrator works."); }
                        if (BWRun.IsBusy == false && starting == false)
                        {
                            cmdSBODefault.Visible = true;
                        }
                        break;
                    case "Integrated":
                        mnuA.Visible = false;
                        mnuR.Visible = false;
                        break;
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show("There was an error when trying to set menu" + Environment.NewLine + Ex.Message);
                cMain.WriteLog(this, "Error settings toolstripitems", Ex.Message, lblStatus.Text);
            }

            mnuAddS.Text = mnuAdd.Text + " + Subfolders";
            mnuILoad.Text = "Load '" + GetSelectedTab().Text + "'";
            mnuISave.Text = "Save '" + GetSelectedTab().Text + "'";
            if (BWRun.IsBusy == false)
            {
                cmdAdd.Visible = true;
            }
            if (!BWRun.IsBusy) { UpdateNames(true); }

            EnablePan();
            cMain.FreeRAM();

        }

        private void UpdateNames(bool ResizeColumns)
        {
            string Status = "";

            try
            {
                Status = "Updating Tab Name [Addons]"; Application.DoEvents();
                TabAddons.Text = "Addons [" + lstAddons.Items.Count + "]";
                Status = "Updating Tab Name [Drivers]"; Application.DoEvents();
                TabDrivers.Text = "Drivers [" + lstDrivers.Items.Count + "]";
                Status = "Updating Tab Name [Gadgets]"; Application.DoEvents();
                TabGadgets.Text = "Gadgets [" + lstGadgets.Items.Count + "]";
                Status = "Updating Tab Name [Themes]"; Application.DoEvents();
                TabThemes.Text = "Theme Packs [" + lstThemes.Items.Count + "]";
                Status = "Updating Tab Name [Updates]"; Application.DoEvents();
                TabUpdates.Text = "Updates + Languages [" + lstUpdates.Items.Count + "]";
                Status = "Updating Tab Name [Wallpapers]"; Application.DoEvents();
                TabWallpapers.Text = "Wallpapers [" + lstWallpapers.Items.Count + "]";
                Status = "Updating Tab Name [Basic]"; Application.DoEvents();
                int BT = lstAddons.Items.Count + lstDrivers.Items.Count + lstGadgets.Items.Count + lstThemes.Items.Count + lstUpdates.Items.Count + lstWallpapers.Items.Count + CountNodes(tvTweaks);
                TabBasic.Text = "Basic [" + BT + "]";

                Status = "Updating Tab Name [Component Removal]"; Application.DoEvents();
                CHCompRemoval.Text = "Name (" + lstComponents.CheckedItems.Count + ")";
                tabComponents.Text = "Component Removal [" + lstComponents.CheckedItems.Count + "]";
                Status = "Updating Tab Name [vLite]"; Application.DoEvents();
                tabComponents2.Text = "vLite [" + CountNodes(tvComponents) + "]";
                Status = "Updating Tab Name [Files]"; Application.DoEvents();
                tabFiles.Text = "Files [" + lstFiles.Items.Count + "]";
                Status = "Updating Tab Name [Services]"; Application.DoEvents();
                tabServices.Text = "Services [" + lstServices.CheckedItems.Count + "]";
                Status = "Updating Tab Name [Silent]"; Application.DoEvents();
                tabSilent.Text = "Silent Installs + SFX [" + lstSilent.Items.Count + "]";
                Status = "Updating Tab Name [Tweaks]"; Application.DoEvents();
                TabTweaks.Text = "Tweaks [" + CountNodes(tvTweaks) + "]";
                Status = "Updating Tab Name [Options]"; Application.DoEvents();
                TabOptions.Text = "Options [" + lstOptions.CheckedItems.Count + "]";
                Status = "Updating Tab Name [Advanced]"; Application.DoEvents();
                int AT = lstComponents.CheckedItems.Count + CountNodes(tvComponents) + lstFiles.Items.Count + lstServices.CheckedItems.Count + lstSilent.Items.Count;
                TabAdvanced.Text = "Advanced [" + AT + "]";

                Status = "Resizing Columns"; Application.DoEvents();
                if (ResizeColumns)
                {
                    foreach (ColumnHeader CH in lstAddons.Columns) { if (CH.Text != "Location" && lstAddons.Columns.Count > 1) { CH.Width = -2; } }
                    foreach (ColumnHeader CH in lstDrivers.Columns) { if (CH.Text != "Location" && lstDrivers.Columns.Count > 1) { CH.Width = -2; } }
                    foreach (ColumnHeader CH in lstGadgets.Columns) { if (CH.Text != "Location" && lstGadgets.Columns.Count > 1) { CH.Width = -2; } }
                    foreach (ColumnHeader CH in lstThemes.Columns) { if (CH.Text != "Location" && lstThemes.Columns.Count > 1) { CH.Width = -2; } }
                    foreach (ColumnHeader CH in lstFiles.Columns) { if (CH.Text != "Location" && lstFiles.Columns.Count > 1) { CH.Width = -2; } }
                    foreach (ColumnHeader CH in lstWallpapers.Columns) { if (CH.Text != "Location" && lstWallpapers.Columns.Count > 1) { CH.Width = -2; } }
                    foreach (ColumnHeader CH in lstServices.Columns) { if (CH.Text != "Location" && CH.Text != "Setting [Click to Change]" && lstServices.Columns.Count > 1) { CH.Width = -2; } }
                    foreach (ColumnHeader CH in lstSilent.Columns) { if (CH.Text != "Location" && lstSilent.Columns.Count > 1) { CH.Width = -2; } }
                    foreach (ColumnHeader CH in lstComponents.Columns) { if (CH.Text != "Location" && lstComponents.Columns.Count > 1) { CH.Width = -2; } }
                    foreach (ColumnHeader CH in lstUpdates.Columns) { if (CH.Text != "Location" && lstUpdates.Columns.Count > 1) { CH.Width = -2; } }
                    foreach (ColumnHeader CH in lstOptions.Columns) { if (CH.Text != "Location" && lstOptions.Columns.Count > 1) { CH.Width = -2; } }
                }
                columnHeader30.Width = 160;
                Status = "Done";
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Error renaming tabs", Ex.Message, Status);
            }
            Application.DoEvents();
        }

        private void EnablePan()
        {
            cmdHelp.Visible = false;
            cmdSITop.Visible = mnuR.Visible;
            cmdSIUp.Visible = mnuR.Visible;
            cmdSIDown.Visible = mnuR.Visible;
            cmdSIBottom.Visible = mnuR.Visible;
            cmdAdd.Visible = mnuA.Visible;
            cmdDefaultT.Visible = cmdDefaultTheme.Visible;
            cmdRemove.Visible = mnuR.Visible;
            cmdSBSafe.Visible = mnuBV.Visible;
            cmdSBTweaked.Visible = mnuBV.Visible;
            cmdSBBone.Visible = mnuBV.Visible;
            cmdSBRestore.Visible = mnuRefresh.Visible;
            cmdSBSInfo.Visible = mnuBV.Visible;
            if (TPMain.SelectedTab == TabAdvanced && (TPAdvanced.SelectedTab == tabComponents || TPAdvanced.SelectedTab == tabComponents2)) { cmdHelp.Visible = true; }
            if (TPMain.SelectedTab == TabBasic && TPBasic.SelectedTab == TabUpdates)
            {
                cmdHelp.Visible = true;
            }

            if (TPMain.SelectedTab == TabAdvanced && TPAdvanced.SelectedTab == TabTweaks)
            {
                cmdSITop.Visible = false;
                cmdSIUp.Visible = false;
                cmdSIDown.Visible = false;
                cmdSIBottom.Visible = false;
            }

            cMain.CenterObject(PanArrange);
            Application.DoEvents();

        }

        private void BWAddAddons_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateNames(true);
            foreach (ColumnHeader CH in lstAddons.Columns) { CH.Width = -2; }
            cMain.UpdateToolStripLabel(lblStatus, "");
            if (lstAddons.Items.Count > 0)
            {
                mnuR.Visible = true;
            }
            EnablePan();

            ToolStrip1.Enabled = true;
            PanArrange.Enabled = true;
            mnuClear.Visible = true;
            TPMain.Enabled = true;
            iAdding = false;
            cMain.FreeRAM();

            if (!string.IsNullOrEmpty(cAddon.AddonError))
            {
                cMain.ErrorBox("Some addons could not be added. For more information, click '>> Details'.", "Invalid Addons", cAddon.AddonError);
            }
        }

        private void BWRun_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Files.DeleteFile(cOptions.WinToolkitTemp + "\\Background.bmp");

            cReg.WIMt = false;
            mnuRefresh.Visible = false;
            mnuSaveLoad.Visible = false;

            if (!string.IsNullOrEmpty(aErr))
            {
                cMain.ErrorBox("Win Toolkit has recorded errors which occurred during integration and have been stored via '>> Details'.", nErr + " error(s) have occurred", aErr);
            }

            try
            {
                //Shutdown PC
                if (cboShutdown.Text.EqualsIgnoreCase("Shutdown PC"))
                {
                    iBusy = false;
                    cMain.UpdateToolStripLabel(lblStatus, "Waiting for user...");
                    Application.DoEvents();
                    var TS = new frmShutdown();
                    TS.Text = "Shutdown";
                    TS.ShowDialog();
                    return;
                }
            }
            catch
            {
            }

            cMain.FreeRAM();

            try
            {
                if (lstOptions.FindItemWithText("Prompt Themes").Checked ||
                      lstOptions.FindItemWithText("Prompt Updates").Checked ||
                      lstOptions.FindItemWithText("Prompt Drivers").Checked ||
                      lstOptions.FindItemWithText("Prompt Addons").Checked ||
                      lstOptions.FindItemWithText("Prompt Wallpapers").Checked ||
                      lstOptions.FindItemWithText("Prompt Silent").Checked)
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Adding main 'Integrated' tab...");
                    Application.DoEvents();

                    if (lstIAddons.Items.Count > 0 || lstIDrivers.Items.Count > 0 || lstIUpdates.Items.Count > 0 || lstIThemes.Items.Count > 0 || lstIGadgets.Items.Count > 0 || lstIWallpapers.Items.Count > 0 || lstISilent.Items.Count > 0)
                    {
                        if (TPMain.TabPages.Contains(NewTab) == false)
                        {
                            NewTab.ImageIndex = 15;
                            NewTab.Text = "Integrated";
                            TPMain.TabPages.Add(NewTab);

                            if (NewTab.Controls.Contains(NewTC) == false)
                            {
                                NewTab.Controls.Add(NewTC);
                                NewTC.Dock = DockStyle.Fill;
                            }
                            Application.DoEvents();
                        }
                        cMain.UpdateToolStripLabel(lblStatus, "Creating Integrated Tab...");
                        Application.DoEvents();
                        try
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Checking Integrated Tab (Updates)...");
                            Application.DoEvents();
                            if (lstOptions.FindItemWithText("Prompt Updates").Checked && lstIUpdates.Items.Count > 0)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Creating Integrated Tabs (Updates)...");
                                Application.DoEvents();
                                var CH1 = new ColumnHeader();
                                var CH2 = new ColumnHeader();
                                var CH3 = new ColumnHeader();
                                var CH4 = new ColumnHeader();
                                var CH5 = new ColumnHeader();
                                CH1.Text = "Update Name";
                                CH2.Text = "Package Name";
                                CH3.Text = "State";
                                CH4.Text = "Release Type";
                                CH5.Text = "Install Time";

                                if (NewTC.TabPages.Contains(NewTabUpdate) == false)
                                {
                                    NewTC.TabPages.Add(NewTabUpdate);
                                    NewTabUpdate.Controls.Add(lstIUpdates);
                                    lstIUpdates.Dock = DockStyle.Fill;
                                    lstIUpdates.FullRowSelect = true;
                                    lstIUpdates.View = View.Details;
                                }

                                lstIUpdates.Columns.Add(CH1);
                                lstIUpdates.Columns.Add(CH2);
                                lstIUpdates.Columns.Add(CH3);
                                lstIUpdates.Columns.Add(CH4);
                                lstIUpdates.Columns.Add(CH5);

                                foreach (ListViewItem LST in lstIUpdates.Items)
                                {
                                    try
                                    {
                                        if (lstIUpdates.FindItemWithText(LST.Text + "_BF") != null)
                                        {
                                            lstIUpdates.FindItemWithText(LST.Text + "_BF").Text = LST.Text + "_GDR_BF";
                                            LST.Remove();
                                        }
                                        else
                                        {
                                            LST.Text = LST.Text + "_GDR";
                                        }
                                    }
                                    catch { }
                                }

                                foreach (ColumnHeader CHU in lstIUpdates.Columns) { CHU.Width = -2; }

                                foreach (ListViewGroup GPU in lstIUpdates.Groups)
                                {
                                    int ngCount = 0;
                                    foreach (ListViewItem gLST in GPU.Items)
                                    {
                                        if (!string.IsNullOrEmpty(gLST.Text)) { ngCount++; }
                                    }
                                    GPU.Header += " (" + ngCount + ")";
                                }
                                NewTabUpdate.Text = "Updates + Languages (" + lstIUpdates.Items.Count + ")";
                                CH1.Text = "Update Name (" + lstIUpdates.Items.Count + ")";
                            }
                        }
                        catch { }

                        try
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Checking Integrated Tab (Drivers)...");
                            Application.DoEvents();
                            if (lstOptions.FindItemWithText("Prompt Drivers").Checked && lstIDrivers.Items.Count > 0)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Creating Integrated Tabs (Drivers)...");
                                Application.DoEvents();
                                foreach (ListViewGroup GPD in lstIDrivers.Groups)
                                {
                                    GPD.Header = GPD.Header + " (" + GPD.Items.Count + ")";
                                }

                                NewTabDrivers.Text = "Drivers (" + lstIDrivers.Items.Count + ")";
                                if (NewTC.TabPages.Contains(NewTabDrivers) == false)
                                {
                                    NewTC.TabPages.Add(NewTabDrivers);
                                    lstIDrivers.Dock = DockStyle.Fill;
                                    lstIDrivers.FullRowSelect = true;
                                    lstIDrivers.View = View.Details;
                                    NewTabDrivers.Controls.Add(lstIDrivers);
                                }
                                var CH1 = new ColumnHeader();
                                var CH2 = new ColumnHeader();
                                var CH3 = new ColumnHeader();
                                var CH4 = new ColumnHeader();
                                var CH5 = new ColumnHeader();
                                var CH6 = new ColumnHeader();
                                var CH7 = new ColumnHeader();
                                CH1.Text = "Published Name";
                                CH2.Text = "Original Filename";
                                CH3.Text = "Inbox";
                                CH4.Text = "Class Name";
                                CH5.Text = "Provider Name";
                                CH6.Text = "Date";
                                CH7.Text = "Version";
                                lstIDrivers.Columns.Add(CH1);
                                lstIDrivers.Columns.Add(CH2);
                                lstIDrivers.Columns.Add(CH3);
                                lstIDrivers.Columns.Add(CH4);
                                lstIDrivers.Columns.Add(CH5);
                                lstIDrivers.Columns.Add(CH6);
                                lstIDrivers.Columns.Add(CH7);
                                foreach (ColumnHeader CHD in lstIDrivers.Columns)
                                {
                                    CHD.Width = -2;
                                }
                            }
                        }
                        catch { }

                        try
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Checking Integrated Tab (Addons)...");
                            Application.DoEvents();
                            if (lstOptions.FindItemWithText("Prompt Addons").Checked && lstIAddons.Items.Count > 0)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Creating Integrated Tabs (Addons)...");
                                Application.DoEvents();
                                foreach (ListViewGroup GPA in lstIAddons.Groups)
                                {
                                    GPA.Header = GPA.Header + " (" + GPA.Items.Count + ")";
                                }

                                NewTabAddons.Text = "Addons (" + lstIAddons.Items.Count + ")";
                                if (NewTC.TabPages.Contains(NewTabAddons) == false)
                                {
                                    NewTC.TabPages.Add(NewTabAddons);
                                    lstIAddons.Dock = DockStyle.Fill;
                                    lstIAddons.FullRowSelect = true;
                                    lstIAddons.View = View.Details;
                                    NewTabAddons.Controls.Add(lstIAddons);
                                }
                                var CH1 = new ColumnHeader();
                                var CH2 = new ColumnHeader();
                                var CH3 = new ColumnHeader();
                                var CH4 = new ColumnHeader();
                                var CH5 = new ColumnHeader();
                                var CH6 = new ColumnHeader();
                                var CH7 = new ColumnHeader();
                                CH1.Text = "Name";
                                CH2.Text = "Creator";
                                CH3.Text = "Version";
                                CH4.Text = "Architecture";
                                CH5.Text = "Description";
                                CH6.Text = "Website";
                                lstIAddons.Columns.Add(CH1);
                                lstIAddons.Columns.Add(CH2);
                                lstIAddons.Columns.Add(CH3);
                                lstIAddons.Columns.Add(CH4);
                                lstIAddons.Columns.Add(CH5);
                                lstIAddons.Columns.Add(CH6);
                                foreach (ColumnHeader CHa in lstIAddons.Columns)
                                {
                                    CHa.Width = -2;
                                }
                            }
                        }
                        catch { }

                        try
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Checking Integrated Tab (Themes)...");
                            Application.DoEvents();
                            if (lstOptions.FindItemWithText("Prompt Themes").Checked && lstIThemes.Items.Count > 0)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Creating Integrated Tabs (Themes)...");
                                Application.DoEvents();
                                foreach (ListViewGroup GPA in lstIThemes.Groups)
                                {
                                    GPA.Header = GPA.Header + " (" + GPA.Items.Count + ")";
                                }

                                NewTabThemes.Text = "Themes (" + lstIThemes.Items.Count + ")";
                                if (NewTC.TabPages.Contains(NewTabThemes) == false)
                                {
                                    NewTC.TabPages.Add(NewTabThemes);
                                    lstIThemes.Dock = DockStyle.Fill;
                                    lstIThemes.FullRowSelect = true;
                                    lstIThemes.View = View.Details;
                                    NewTabThemes.Controls.Add(lstIThemes);
                                }
                                var CH1 = new ColumnHeader();

                                CH1.Text = "Name";

                                lstIThemes.Columns.Add(CH1);
                                foreach (ColumnHeader CHU in lstIThemes.Columns)
                                {
                                    CHU.Width = -2;
                                }
                            }
                        }
                        catch { }

                        try
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Checking Integrated Tab (Gadgets)...");
                            Application.DoEvents();
                            if (lstOptions.FindItemWithText("Prompt Gadgets").Checked && lstIGadgets.Items.Count > 0)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Creating Integrated Tabs (Gadgets)...");
                                Application.DoEvents();
                                foreach (ListViewGroup GPA in lstIGadgets.Groups)
                                {
                                    GPA.Header = GPA.Header + " (" + GPA.Items.Count + ")";
                                }

                                NewTabGadgets.Text = "Gadgets (" + lstIGadgets.Items.Count + ")";
                                if (NewTC.TabPages.Contains(NewTabGadgets) == false)
                                {
                                    NewTC.TabPages.Add(NewTabGadgets);
                                    lstIGadgets.Dock = DockStyle.Fill;
                                    lstIGadgets.FullRowSelect = true;
                                    lstIGadgets.View = View.Details;
                                    NewTabGadgets.Controls.Add(lstIGadgets);
                                }
                                var CH1 = new ColumnHeader();

                                CH1.Text = "Name";

                                lstIGadgets.Columns.Add(CH1);
                                foreach (ColumnHeader CHU in lstIGadgets.Columns)
                                {
                                    CHU.Width = -2;
                                }
                            }
                        }
                        catch { }

                        try
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Checking Integrated Tab (Wallpapers)...");
                            Application.DoEvents();
                            if (lstOptions.FindItemWithText("Prompt Wallpapers").Checked && lstIWallpapers.Items.Count > 0)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Creating Integrated Tabs (Wallpapers)...");
                                Application.DoEvents();
                                foreach (ListViewGroup GPA in lstIWallpapers.Groups)
                                {
                                    GPA.Header = GPA.Header + " (" + GPA.Items.Count + ")";
                                }

                                NewTabWallpaper.Text = "Wallpapers (" + lstIWallpapers.Items.Count + ")";
                                if (NewTC.TabPages.Contains(NewTabWallpaper) == false)
                                {
                                    NewTC.TabPages.Add(NewTabWallpaper);
                                    lstIWallpapers.Dock = DockStyle.Fill;
                                    lstIWallpapers.FullRowSelect = true;
                                    lstIWallpapers.View = View.Details;
                                    NewTabWallpaper.Controls.Add(lstIWallpapers);
                                }
                                var CH1 = new ColumnHeader();

                                CH1.Text = "Name";

                                lstIWallpapers.Columns.Add(CH1);
                                foreach (ColumnHeader CHU in lstIWallpapers.Columns)
                                {
                                    CHU.Width = -2;
                                }
                            }
                        }
                        catch { }

                        try
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Checking Integrated Tab (Silent)...");
                            Application.DoEvents();
                            if (lstOptions.FindItemWithText("Prompt Silent").Checked && lstISilent.Items.Count > 0)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Creating Integrated Tabs (Silent Installers)...");
                                Application.DoEvents();

                                foreach (ListViewGroup GPS in lstISilent.Groups)
                                {
                                    GPS.Header = GPS.Header + " (" + GPS.Items.Count + ")";
                                }

                                NewTabSilent.Text = "Silent Installs (" + lstISilent.Items.Count + ")";
                                if (NewTC.TabPages.Contains(NewTabSilent) == false)
                                {
                                    NewTC.TabPages.Add(NewTabSilent);
                                    lstISilent.Dock = DockStyle.Fill;
                                    lstISilent.FullRowSelect = true;
                                    lstISilent.View = View.Details;

                                    NewTabSilent.Controls.Add(lstISilent);
                                }
                                var CH1 = new ColumnHeader();
                                var CH2 = new ColumnHeader();
                                var CH3 = new ColumnHeader();
                                var CH4 = new ColumnHeader();
                                var CH5 = new ColumnHeader();
                                CH1.Text = "Name";
                                CH2.Text = "Syntax";
                                CH3.Text = "Always Install";
                                CH4.Text = "Size";
                                CH5.Text = "Location";

                                CH1.Text = "Name (" + lstISilent.Items.Count + ")";

                                lstISilent.Columns.Add(CH1);
                                lstISilent.Columns.Add(CH2);
                                lstISilent.Columns.Add(CH3);
                                lstISilent.Columns.Add(CH4);
                                lstISilent.Columns.Add(CH5);
                                foreach (ColumnHeader CHU in lstISilent.Columns)
                                {
                                    CHU.Width = -2;
                                }
                            }
                        }
                        catch { }
                    }
                }
                try
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists' setting...");
                    Application.DoEvents();
                    if (lstOptions.FindItemWithText("Clear Lists").Checked)
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Clearing lists...");
                        Application.DoEvents();
                        ClearLists();
                    }
                }
                catch (Exception Ex) { }
                mnuClear.Visible = true;
                cMain.UpdateToolStripLabel(lblStatus, "Showing 'Integrated' tab...");
                Application.DoEvents();
                TPMain.SelectedIndex = TPMain.TabPages.Count - 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown error detected" + Environment.NewLine + ex.Message);
                cMain.WriteLog(this, "RunWorkerCompleted Error", ex.Message, lblStatus.Text);
            }

            try
            {
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            }
            catch { }
            cMain.UpdateToolStripLabel(lblStatus, "Almost done...");
            Application.DoEvents();

            if (!string.IsNullOrEmpty(dErr))
            {
                cMain.ErrorBox("Some drivers could not be integrated. This is most likely caused by missing files.", "Driver Errors", dErr);
            }

            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Almost done (Addons)...");
                Application.DoEvents();
                lstAddons.Enabled = true;
                cMain.UpdateToolStripLabel(lblStatus, "Almost done (Components)...");
                Application.DoEvents();
                lstComponents.Enabled = true;
                cMain.UpdateToolStripLabel(lblStatus, "Almost done (vLite)...");
                Application.DoEvents();
                tvComponents.Enabled = true;
                cMain.UpdateToolStripLabel(lblStatus, "Almost done (Drivers)...");
                Application.DoEvents();
                lstDrivers.Enabled = true;
                cMain.UpdateToolStripLabel(lblStatus, "Almost done (Gadgets)...");
                Application.DoEvents();
                lstGadgets.Enabled = true;
                cMain.UpdateToolStripLabel(lblStatus, "Almost done (Options)...");
                Application.DoEvents();
                lstOptions.Enabled = true;
                cMain.UpdateToolStripLabel(lblStatus, "Almost done (Services)...");
                Application.DoEvents();
                lstServices.Enabled = true;
                cMain.UpdateToolStripLabel(lblStatus, "Almost done (Silent)...");
                Application.DoEvents();
                lstSilent.Enabled = true;
                cMain.UpdateToolStripLabel(lblStatus, "Almost done (Themes)...");
                Application.DoEvents();
                lstThemes.Enabled = true;
                cMain.UpdateToolStripLabel(lblStatus, "Almost done (Tweaks)...");
                Application.DoEvents();
                tvTweaks.Enabled = true;
                cMain.UpdateToolStripLabel(lblStatus, "Almost done (Wallpapers)...");
                Application.DoEvents();
                lstWallpapers.Enabled = true;
                cMain.UpdateToolStripLabel(lblStatus, "Almost done (Updates)...");
                Application.DoEvents();
                lstUpdates.HeaderStyle = ColumnHeaderStyle.Clickable;
                cMain.UpdateToolStripLabel(lblStatus, "Almost done...");
                PB.Visible = false;
                cmdStart.Text = "Start";
                cmdStart.Image = Properties.Resources.OK;
                PanArrange.Enabled = true;
            }
            catch { }

            cMain.UpdateToolStripLabel(lblStatus, cAddon.AddonCancel ? "Stopped..." : "Completed...");

            timEllapsed.Enabled = false;
            cOptions.AIOTime = lblTime.Text;
            cmdStart.Enabled = true;
            mnuSaveLoad.Visible = true;

            try
            {
                if (lstOptions.FindItemWithText("Enable Beep").Checked)
                {
                    Console.Beep(1500, 500);
                    Console.Beep(1500, 500);
                    Console.Beep(1500, 500);
                }
            }
            catch { }

            iBusy = false;
            cMain.FreeRAM();
            cMain.sList = "";

            //Need to check if failed, error or cancelled.
            //cMain.ShowNotification("Integration Completed", "Your integration task in All-In-One Integrator has finished.\n\n" + cOptions.AIOTime, ToolTipIcon.Info);
        }

        private string GetFolder(string FileN)
        {
            string F = FileN;
            try
            {
                while (!F.EndsWithIgnoreCase("\\"))
                {
                    F = F.Substring(0, F.Length - 1);
                }
                F = F.Substring(0, F.Length - 1);
            }
            catch { }
            return F;
        }

        private string GetInfo(string Line)
        {
            string TempL = Line.Substring(0, Line.Length - 1);

            while (TempL.ContainsIgnoreCase("\""))
            {
                TempL = TempL.Substring(1);
            }

            return TempL;
        }

        private bool ExtractDriver(string fPath)
        {
            string sPath = cMain.UserTempPath + "\\DrvEx";
            bool bFound = false;
            try
            {
                if (fPath.ToUpper().EndsWithIgnoreCase(".98_")) { return true; }
                if (fPath.ToUpper().EndsWithIgnoreCase(".XP_")) { return true; }

                if (fPath.ToUpper().EndsWithIgnoreCase(".BM_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".bmp\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".CA_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".cap\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".CH_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".chm\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".RT_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".rtf\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".BA_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".bat\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".BI_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".bin\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".DO_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".dos\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".fPathO_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".fon\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".CP_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".cpl\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".LN_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".lng\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".DA_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".dat\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".DL_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".dll\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".AX_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".ax\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".TV_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".tvp\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".CN_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".cn\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".VX_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".vxd\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".MP_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".mpg\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".DS_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".ds\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".EX_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".exe\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".EN_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".en\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".DS_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".ds\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".NT_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".nt\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".JP_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".jpg\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".HT_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".htm\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".IC_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".icc\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".IN_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".inf\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".KM_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".kmd\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".MI_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".mid\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".SM_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".smr\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".SY_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".sys\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".TB_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".tbl\"");
                    bFound = true;
                }

                if (fPath.ToUpper().EndsWithIgnoreCase(".TX_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".txt\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".XM_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".xml\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".RESOURCE_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 10) + ".resources\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".CONfPathI_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 7) + ".config\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".LR_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".lrc\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".VP_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".vp\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".CfPath_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".cfg\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".LM_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".lmd\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".PR_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".prm\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".VI_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".vif \"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".HL_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".HLP\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".PD_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".PDD\"");
                    bFound = true;
                }
                if (fPath.ToUpper().EndsWithIgnoreCase(".BC_"))
                {
                    cMain.RunExternal("\"" + cMain.SysFolder + "\\expand.exe\"",
                        "\"" + fPath + "\" \"" + fPath.Substring(0, fPath.Length - 4) + ".BCM\"");
                    bFound = true;
                }


                if (!bFound)
                {
                    string sName = sPath + "\\" + Path.GetFileNameWithoutExtension(fPath);
                    Files.DeleteFolder(sPath, true);

                    if (fPath.ContainsForeignCharacters() || fPath.StartsWithIgnoreCase("\\\\"))
                    {
                        string p = Path.GetFileName(fPath);
                        File.Copy(fPath, cMain.UserTempPath + "\\" + p, true);
                        cMain.ExtractFiles(cMain.UserTempPath + "\\" + p, sPath, this, "*", false, false);
                        File.Delete(cMain.UserTempPath + "\\" + p);
                    }
                    else
                    {
                        cMain.ExtractFiles(fPath, sPath, this, "*", false, false);
                    }

                    foreach (string S in Directory.GetFiles(sPath))
                    {
                        if (S.StartsWithIgnoreCase(sName.ToUpper()))
                        {
                            string Dir = Path.GetDirectoryName(fPath);
                            string Fil = Path.GetFileName(S);

                            if (File.Exists(Dir + "\\" + Fil))
                            {
                                bFound = true;
                                break;
                            }

                            File.Move(S, Dir + "\\" + Fil);

                            if (File.Exists(Dir + "\\" + Fil))
                            {
                                bFound = true;
                            }
                            break;
                        }
                    }
                    Files.DeleteFolder(sPath, false);
                }

                if (bFound)
                {
                    Files.DeleteFile(fPath);
                    return true;
                }


                if (!bFound && File.Exists(fPath))
                {
                    if (fPath.ToUpper().EndsWithIgnoreCase(".TX_"))
                    {
                        string newPath = fPath.Substring(0, fPath.Length - 1) + "t";
                        File.Move(fPath, newPath);
                        if (File.Exists(newPath)) { bFound = true; }
                    }
                }
            }
            catch (Exception Ex)
            {
                new SmallError("Error extracting driver file.", Ex, fPath).Upload();
            }
            if (!bFound)
            {
                new SmallError("Unable to extract driver file.", null, fPath).Upload();
            }


            return bFound;
        }

        private string GetInfoCAB(string Info, string Line)
        {
            string TempL = Line;
            try
            {
                while (!TempL.StartsWithIgnoreCase(Info.ToUpper()))
                {
                    TempL = TempL.Substring(1);
                }
                while (!TempL.StartsWithIgnoreCase("\""))
                {
                    TempL = TempL.Substring(1);
                }
                TempL = TempL.Substring(1);

                while (TempL.ContainsIgnoreCase("\""))
                {
                    TempL = TempL.Substring(0, TempL.Length - 1);
                }
            }
            catch (Exception)
            {
                TempL = "N/A";
            }
            return TempL;
        }

        private bool AllowOffline(string Input)
        {
            if (!lstOptions.FindItemWithText("Move Known Problem Updates to Silent Installers", false, 0).Checked) { return true; }

            if (Input.ContainsIgnoreCase("KB943790")) { return false; }
            if (Input.ContainsIgnoreCase("KB947821")) { return false; }
            if (Input.ContainsIgnoreCase("KB958559")) { return false; }
            if (Input.ContainsIgnoreCase("KB2506143")) { return false; }
            if (Input.ContainsIgnoreCase("KB2533552")) { return false; }
            if (Input.ContainsIgnoreCase("KB3035583")) { return false; } //14-06-2015
            //if (Input.ContainsIgnoreCase("KB3020369")) { return false; } //14-06-2015
            if (Input.ContainsIgnoreCase("KB3177467")) { return false; } //14-10-2016

            ///Microsoft recommends this is installed after installation.
            if (Input.ContainsIgnoreCase("KB2603229")) { return false; }
            if (Input.ContainsIgnoreCase("KB2652034")) { return false; }
            //if (Input.ContainsIgnoreCase("KB2685811")) { return false; }
            //if (Input.ContainsIgnoreCase("KB2685813")) { return false; }
            if (Input.ContainsIgnoreCase("KB2697737")) { return false; }
            if (Input.ContainsIgnoreCase("KB2809215")) { return false; }
            if (Input.ContainsIgnoreCase("KB2809900")) { return false; }
            if (Input.ContainsIgnoreCase("KB2823180")) { return false; }
            //if (Input.ContainsIgnoreCase("KB2830477")) { return false; }
            //if (Input.ContainsIgnoreCase("KB2857650")) { return false; }
            if (Input.ContainsIgnoreCase("KB2862019")) { return false; }
            //if (Input.ContainsIgnoreCase("KB2862330")) { return false; }

            if (Input.ContainsIgnoreCase("KB3046269")) { return false; }
            if (Input.ContainsIgnoreCase("KB2883200")) { return false; }
            if (Input.ContainsIgnoreCase("KB2883457")) { return false; }
            if (Input.ContainsIgnoreCase("KB2884846")) { return false; }
            if (Input.ContainsIgnoreCase("KB2895729")) { return false; }

            //	if (Input.ContainsIgnoreCase("KB2955164")) { return false; }
            //if (Input.ContainsIgnoreCase("KB2962409")) { return false; }

            if (Input.ContainsIgnoreCase("KB2955164") && Environment.OSVersion.Version.Major < 6) { return false; }
            return true;
        }

        private void AddToList(string filePath, string InfoFile, string MD5)
        {
            if (filePath.ToUpper().EndsWithIgnoreCase("-EXPRESS.CAB"))
            {
                return;
            }
            string iFileName = filePath;
            string oFileName = filePath;

            if (filePath.ContainsIgnoreCase("|"))
            {
                iFileName = filePath.Split('|')[0];
                oFileName = filePath.Split('|')[1];
            }
            if (File.Exists(iFileName) && cMain.GetSize(iFileName, false).EqualsIgnoreCase("0")) { uError += "Corrupted (0 bytes): '" + iFileName + "'" + Environment.NewLine; UC += 1; return; }
            if (!File.Exists(iFileName)) { uError += "File Not Found: '" + iFileName + "'" + Environment.NewLine; UC += 1; return; }

            bool AllowedOffline = AllowOffline(oFileName);
            string strFile = "";

            string uName = "";
            string uDesc = "";
            string uArc = "";
            string uSupport = "";
            string uPName = "";
            string uPVersion = "";
            string uLang = "";
            string uDate = "";
            string uSize = cMain.GetSize(oFileName, false);
            UpdateInfo updateInfo = new UpdateInfo();
            UpdateCache.UpdateCacheItem item;
            try
            {
                item = UpdateCache.Find(Path.GetFileName(oFileName), uSize);
            }
            catch (Exception ex)
            {
                item = null;
                new SmallError("Error Finding Cache", ex, oFileName + "\r\nSize: " + uSize).Upload();
            }

            if (item != null)
            {
                uName = Path.GetFileName(oFileName);

                if (uName.ContainsIgnoreCase("KB"))
                {
                    while (!uName.StartsWithIgnoreCase("KB"))
                    {
                        uName = uName.Substring(1);
                    }

                    while (uName.ContainsIgnoreCase("-"))
                    {
                        uName = uName.Substring(0, uName.Length - 1);
                    }
                }
                uDesc = item.PackageDescription.ReplaceIgnoreCase("\"", "");
                uArc = item.Architecture.ToLower();
                uSupport = item.Support;
                uPName = item.PackageName;
                uPVersion = item.PackageVersion;
                uLang = item.Language;
                uDate = item.CreatedDate.ToShortDateString();
            }
            else
            {

                Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst", true);
                cMain.ExtractFiles(iFileName, cOptions.WinToolkitTemp + "\\cInst", this, InfoFile);

                switch (Path.GetExtension(iFileName).ToUpper())
                {
                    case ".CAB":
                        if (!File.Exists(cOptions.WinToolkitTemp + "\\cInst\\" + InfoFile))
                        {
                            UC += 1; uError += "Doesn't seem to be a valid update file: '" + iFileName + "'" + Environment.NewLine;

                            return;
                        }

                        string rUpdate = "";
                        try
                        {
                            using (var strReader = new StreamReader(cOptions.WinToolkitTemp + "\\cInst\\" + InfoFile))
                            {
                                rUpdate = strReader.ReadToEnd();
                            }
                        }
                        catch (Exception Ex)
                        {
                            new SmallError("Error trying to add update [inner:CAB #1]", Ex, filePath + "\nStatus: " + lblStatus).Upload();
                        }
                        strFile = rUpdate;

                        Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst", false);
                        if (rUpdate.ContainsIgnoreCase("ALLOWEDOFFLINE=\"FALSE\"") || AllowOffline(rUpdate) == false)
                        {
                            AllowedOffline = false;
                        }

                        //if (lstOptions.FindItemWithText("Compatibility Check").Checked && !rUpdate.ContainsIgnoreCase("KB971033")) {
                        //	string v = cMain.sImages[0].SubItems[3].Text;
                        //	//
                        //	if (v.ContainsIgnoreCase("(") && !rUpdate.ContainsIgnoreCase("Microsoft-Windows-InternetExplorer-") && !rUpdate.ContainsIgnoreCase("WUClient-SelfUpdate")) {
                        //		while (v.ContainsIgnoreCase("(")) { v = v.Substring(1); }
                        //		v = v.Substring(0, v.Length - 1);
                        //		while (v.ContainsIgnoreCase(".")) { v = v.Substring(0, v.Length - 1); }
                        //		if (!rUpdate.ContainsIgnoreCase(v)) { uError += "Not compatible with " + v + ": '" + iFileName + "'" + Environment.NewLine; UC += 1; return; }
                        //	}
                        //}
                        rUpdate = rUpdate.ReplaceIgnoreCase("<?xml version='", "<?xml ='", false);
                        rUpdate = rUpdate.ReplaceIgnoreCase("<?xml version=\"", "<?xml =\"", false);

                        foreach (string line in rUpdate.Split(Environment.NewLine.ToCharArray()))
                        {
                            string strLineUpd = line;
                            if (!string.IsNullOrEmpty(strLineUpd))
                            {
                                if (string.IsNullOrEmpty(uName) && strLineUpd.ContainsIgnoreCase("PACKAGE IDENTIFIER"))
                                    uName = GetInfoCAB("PACKAGE IDENTIFIER", strLineUpd);
                                if (string.IsNullOrEmpty(uName) && strLineUpd.ContainsIgnoreCase("ASSEMBLYIDENTITY NAME"))
                                    uName = GetInfoCAB("ASSEMBLYIDENTITY NAME", strLineUpd);
                                if (string.IsNullOrEmpty(uDesc) && strLineUpd.ContainsIgnoreCase("DESCRIPTION"))
                                    uDesc = GetInfoCAB("DESCRIPTION", strLineUpd);
                                if (string.IsNullOrEmpty(uDesc) && strLineUpd.ContainsIgnoreCase("ASSEMBLYIDENTITY NAME"))
                                    uDesc = GetInfoCAB("ASSEMBLYIDENTITY NAME", strLineUpd);
                                if (string.IsNullOrEmpty(uArc) && strLineUpd.ContainsIgnoreCase("PROCESSORARCHITECTURE"))
                                    uArc = GetInfoCAB("processorArchitecture", strLineUpd);
                                if (string.IsNullOrEmpty(uSupport) && strLineUpd.ContainsIgnoreCase("SUPPORTINFORMATION"))
                                    uSupport = GetInfoCAB("supportInformation", strLineUpd);
                                if (string.IsNullOrEmpty(uPName) && strLineUpd.ContainsIgnoreCase(" NAME"))
                                    uPName = GetInfoCAB(" NAME", strLineUpd);
                                if (string.IsNullOrEmpty(uPVersion) && strLineUpd.ContainsIgnoreCase(" VERSION"))
                                    uPVersion = GetInfoCAB(" VERSION", strLineUpd);//creationTimeStamp=
                                if (string.IsNullOrEmpty(uDate) && strLineUpd.ContainsIgnoreCase(" CREATIONTIMESTAMP"))
                                    uDate = GetInfoCAB(" CREATIONTIMESTAMP", strLineUpd);

                                if (string.IsNullOrEmpty(uLang) && strLineUpd.ContainsIgnoreCase("LANGUAGE="))
                                {
                                    try
                                    {
                                        uLang = strLineUpd;
                                        while (!uLang.StartsWithIgnoreCase("language=")) { uLang = uLang.Substring(1); }
                                        uLang = uLang.Substring(10);
                                        while (uLang.ContainsIgnoreCase("\"")) { uLang = uLang.Substring(0, uLang.Length - 1); }
                                    }
                                    catch { }
                                }

                                if (!string.IsNullOrEmpty(uName) && !string.IsNullOrEmpty(uDesc) && !string.IsNullOrEmpty(uArc) && !string.IsNullOrEmpty(uSupport))
                                {
                                    break;
                                }
                            }
                        }

                        break;
                    case ".MSU":

                        foreach (string eFile in Directory.GetFiles(cOptions.WinToolkitTemp + "\\cInst\\", "*.txt"))
                        {
                            if (eFile.ToUpper().EndsWithIgnoreCase(".TXT"))
                            {
                                InfoFile = eFile;
                                break;
                            }
                        }

                        try
                        {
                            if (!File.Exists(InfoFile))
                            {
                                Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst", false);
                                string eTo = Path.GetFileNameWithoutExtension(iFileName);
                                cMain.ExtractFiles(iFileName, cOptions.WinToolkitTemp + "\\cInst_" + eTo, this, "*KB*.cab");
                                foreach (string eFile in Directory.GetFiles(cOptions.WinToolkitTemp + "\\CInst_" + eTo + "\\", "*.cab"))
                                {
                                    if (eFile.ContainsIgnoreCase("KB"))
                                    {
                                        AddToList(eFile + "|" + iFileName, "update.mum", MD5);
                                        return;
                                    }
                                }

                            }
                        }
                        catch (Exception Ex)
                        {
                            new SmallError("Error trying to add update [inner:MSU #1]", Ex, filePath + "\nStatus: " + lblStatus).Upload();
                        }

                        string InfoFile2 = "";

                        try
                        {
                            cMain.ExtractFiles(iFileName, cOptions.WinToolkitTemp + "\\cInst", this, "*.xml", false);
                            foreach (string eFile in Directory.GetFiles(cOptions.WinToolkitTemp + "\\cInst\\", "*.*"))
                            {
                                if (eFile.ToUpper().EndsWithIgnoreCase(".XML")) { InfoFile2 = eFile; }
                            }
                        }
                        catch (Exception Ex)
                        {
                            new SmallError("Error trying to add update [inner:MSU #2]", Ex, filePath + "\nStatus: " + lblStatus).Upload();
                        }

                        uArc = "";
                        uPName = "";
                        uPVersion = "";

                        if (File.Exists(InfoFile2))
                        {
                            string rUpdate1;
                            try
                            {
                                using (var strReader1 = new StreamReader(InfoFile2))
                                {
                                    rUpdate1 = strReader1.ReadToEnd();
                                }

                                rUpdate1 = rUpdate1.ReplaceIgnoreCase("<?xml version=\"", "<?xml =\"", false);
                                rUpdate1 = rUpdate1.ReplaceIgnoreCase("<?xml version='", "<?xml ='", false);

                                if (string.IsNullOrEmpty(uArc) && rUpdate1.ContainsIgnoreCase("PROCESSORARCHITECTURE"))
                                    uArc = GetInfoCAB("processorArchitecture", rUpdate1);
                                if (string.IsNullOrEmpty(uPName) && rUpdate1.ContainsIgnoreCase(" NAME"))
                                    uPName = GetInfoCAB(" NAME", rUpdate1);
                                if (string.IsNullOrEmpty(uPVersion) && rUpdate1.ContainsIgnoreCase("VERSION"))
                                    uPVersion = GetInfoCAB("VERSION", rUpdate1);
                                if (string.IsNullOrEmpty(uLang) && rUpdate1.ContainsIgnoreCase("LANGUAGE="))
                                    uLang = GetInfoCAB("LANGUAGE", rUpdate1);
                                if (string.IsNullOrEmpty(uDate) && rUpdate1.ContainsIgnoreCase("BUILD DATE="))
                                    uLang = GetInfoCAB("BUILD DATE", rUpdate1);
                            }
                            catch (Exception Ex)
                            {
                                new SmallError("Error trying to add update [inner:MSU #3]", Ex, filePath + "\nStatus: " + lblStatus).Upload();
                            }
                        }


                        if (File.Exists(InfoFile))
                        {

                            if (AllowOffline(InfoFile) == false) { AllowedOffline = false; }

                            uSupport = "";

                            string rUpdate2;
                            try
                            {
                                using (var strReader2 = new StreamReader(InfoFile))
                                {
                                    rUpdate2 = strReader2.ReadToEnd();
                                }
                            }
                            catch (Exception Ex)
                            {
                                rUpdate2 = "";
                                new SmallError("Error trying to add update [inner:MSU_InfoFile]", Ex, filePath + "\nStatus: " + lblStatus).Upload();
                            }
                            strFile = rUpdate2;

                            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst", false);
                            if (AllowOffline(rUpdate2) == false) { AllowedOffline = false; }
                            //if (lstOptions.FindItemWithText("Compatibility Check").Checked) {
                            //	bool Valid = true;

                            //	if (cMain.sImages[0].SubItems[3].Text.ContainsIgnoreCase("SP1") && !rUpdate2.ContainsIgnoreCase("SP1") && rUpdate2.ContainsIgnoreCase("APPLICABILITYINFO") && !rUpdate2.ContainsIgnoreCase("IE10-")) { Valid = false; }
                            //	if (cMain.sImages[0].SubItems[3].Text.ContainsIgnoreCase("SP2") && !rUpdate2.ContainsIgnoreCase("SP2") && rUpdate2.ContainsIgnoreCase("APPLICABILITYINFO") && !rUpdate2.ContainsIgnoreCase("IE10-")) { Valid = false; }
                            //	if (cMain.sImages[0].SubItems[3].Text.ContainsIgnoreCase("SP3") && !rUpdate2.ContainsIgnoreCase("SP3") && rUpdate2.ContainsIgnoreCase("APPLICABILITYINFO") && !rUpdate2.ContainsIgnoreCase("IE10-")) { Valid = false; }

                            //	if (Valid == false) {
                            //		uError += "Not compatible with " + cMain.sImages[0].SubItems[3].Text + ": '" + iFileName + "'" + Environment.NewLine;
                            //		UC += 1;

                            //		return;
                            //	}
                            //}
                            foreach (string line in rUpdate2.Split(Environment.NewLine.ToCharArray()))
                            {
                                string strLineUpd = line;
                                if (!string.IsNullOrEmpty(strLineUpd))
                                {
                                    if (strLineUpd.ContainsIgnoreCase("KB Article Number"))
                                        uName = GetInfo(strLineUpd);
                                    if (string.IsNullOrEmpty(uArc) && strLineUpd.ContainsIgnoreCase("Processor Architecture"))
                                        uArc = GetInfo(strLineUpd);
                                    if (string.IsNullOrEmpty(uSupport) && strLineUpd.ContainsIgnoreCase("Support Link"))
                                        uSupport = GetInfo(strLineUpd);
                                    if (string.IsNullOrEmpty(uLang) && strLineUpd.ContainsIgnoreCase("Language"))
                                        uLang = GetInfo(strLineUpd);
                                    if (string.IsNullOrEmpty(uDesc) && strLineUpd.ContainsIgnoreCase("Package Type"))
                                        uDesc = GetInfo(strLineUpd);
                                    if (string.IsNullOrEmpty(uDate) && strLineUpd.ContainsIgnoreCase("Build Date"))
                                        uDate = GetInfo(strLineUpd);
                                    if (!string.IsNullOrEmpty(uName) && !string.IsNullOrEmpty(uSupport))
                                    {
                                        if (uName.IsNumeric()) { uName = "KB" + uName; }

                                        break;
                                    }
                                }
                            }

                        }

                        break;
                }
            }


            if (string.IsNullOrEmpty(uName))
            {
                uName = cMain.GetFName(iFileName);

                try
                {
                    if (uName.ContainsIgnoreCase("KB"))
                    {
                        while (!uName.StartsWithIgnoreCase("KB"))
                            uName = uName.Substring(1);
                        while (uName.ContainsIgnoreCase("-"))
                            uName = uName.Substring(0, uName.Length - 1);

                        uSupport = "http://support.microsoft.com/kb/" + uName.Substring(2);
                    }

                }
                catch
                {
                    uName = cMain.GetFName(iFileName);
                    uSupport = "N/A";
                }

                uDesc = uName;
                uArc = cMain.selectedImages[0].Architecture.ToString();
            }

            try
            {
                if (!uName.ContainsIgnoreCase("PACKAGE_FOR_")) { uName = uName.ReplaceIgnoreCase("KB", ""); }
                if (string.IsNullOrEmpty(uArc))
                {
                    uArc = "??";
                    if (iFileName.ContainsIgnoreCase("X64"))
                    {
                        uArc = "amd64";
                    }
                    if (iFileName.ContainsIgnoreCase("X86"))
                    {
                        uArc = "x86";
                    }
                }

                if (uArc.EqualsIgnoreCase("amd64"))
                    uArc = "x64";

                if (uArc != "??" && cMain.selectedImages[0].Architecture.ToString() != "??" && uArc != "neutral")
                {
                    if (cMain.selectedImages[0].Architecture.ToString() != uArc)
                    {
                        uError += "Invalid architecture: '" + iFileName + "'" + Environment.NewLine;
                        UC += 1;
                        return;
                    }
                }
            }
            catch (Exception Ex)
            {
                new SmallError("Error trying to add update [inner:Arc]", Ex, filePath + "\nStatus: " + lblStatus).Upload();
            }


            var NewUpdate = new ListViewItem();
            NewUpdate.Tag = updateInfo;
            try
            {

                NewUpdate.ToolTipText = "No description available.";
                NewUpdate.Group = lstUpdates.Groups[2];
                if (!string.IsNullOrEmpty(uDesc))
                {
                    if (uDesc.ContainsIgnoreCase("LANGUAGEPACK") && uDesc.ContainsIgnoreCase("Microsoft-Windows-Client"))
                    {
                        uSupport = "http://support.microsoft.com/kb/2483139";
                        uName = cMain.GetLPName(uLang) + " Lang Pack";
                        if (uDesc != cMain.GetLPName(uLang))
                        {
                            uDesc = cMain.GetLPName(uLang) + " Lang Pack " + uArc;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                new SmallError("Error trying to add update [inner:Desc]", Ex, filePath + "\nStatus: " + lblStatus).Upload();
            }


            try
            {
                if (string.IsNullOrEmpty(uName))
                {
                    uName = cMain.GetFName(iFileName);
                }
                if (uName.EqualsIgnoreCase("neutral"))
                    uName = cMain.GetFName(iFileName);
                if (uName.ToUpper().EqualsIgnoreCase("DEFAULT") && uDesc.ContainsIgnoreCase("KB"))
                {
                    uName = uDesc;
                    while (!uName.StartsWithIgnoreCase("KB")) { uName = uName.Substring(1); }
                    while (uName.ContainsIgnoreCase(" ")) { uName = uName.Substring(0, uName.Length - 1); }
                    while (uName.ContainsIgnoreCase("-")) { uName = uName.Substring(0, uName.Length - 1); }
                }

                if (uName.IsNumeric()) { uName = "KB" + uName; }
                if (uDesc.IsNumeric()) { uDesc = "KB" + uDesc; }

                if (uPName.StartsWithIgnoreCase("Microsoft-Windows-IE-Hyphenation-Parent-Package")) { uName = "IE Hypthenation"; uDesc = "Hyphenation libraries"; NewUpdate.Group = lstUpdates.Groups[3]; }
                if (uPName.StartsWithIgnoreCase("Microsoft-Windows-IE-Spelling-Parent-Package")) { uName = "IE Spelling"; uDesc = "Spelling libraries."; NewUpdate.Group = lstUpdates.Groups[3]; }

                if (uPName.StartsWithIgnoreCase("Package_for_RollupFix")) { uName += " Update Rollup"; NewUpdate.Group = lstUpdates.Groups[4]; }
                if (uPName.StartsWithIgnoreCase("Package_for_KB3125574")) { uName += " Update Rollup"; uDesc = "Convenience Rollup Update"; }

            }
            catch (Exception Ex)
            {
                new SmallError("Error trying to add update [inner:Name]", Ex, filePath + "\nStatus: " + lblStatus + "\nuName: " + uName + "\nuDesc: " + uDesc + "iFileName: " + iFileName).Upload();
            }


            try
            {
                string t = new FileInfo(iFileName).Name;
                if (t.StartsWithIgnoreCase("IE")) { NewUpdate.Group = lstUpdates.Groups[3]; }
            }
            catch (Exception Ex)
            {
                new SmallError("Error trying to add update [inner:FileInfo]", Ex, filePath + "\nStatus: " + lblStatus).Upload();
            }

            if (string.IsNullOrEmpty(uDesc) && !string.IsNullOrEmpty(uLang))
            {
                uDesc = cMain.GetLPName(uLang) + " Lang Pack " + uArc;
                uName = cMain.GetLPName(uLang) + " Lang Pack " + uArc;
            }

            try
            {

                if (uName.ContainsIgnoreCase("KB982861") || uName.EqualsIgnoreCase("Windows Internet Explorer 9") || uName.ContainsIgnoreCase("KB2718695") || uName.EqualsIgnoreCase("Windows Internet Explorer 10") || uName.ContainsIgnoreCase("KB2841134") || uName.EqualsIgnoreCase("Windows Internet Explorer 11"))
                {
                    if (uName.ContainsIgnoreCase("KB982861") || uName.EqualsIgnoreCase("Windows Internet Explorer 9")) { uName = "Internet Explorer 9"; }
                    if (uName.ContainsIgnoreCase("KB2718695") || uName.EqualsIgnoreCase("Windows Internet Explorer 10"))
                    {
                        NewUpdate.ToolTipText = "Internet Explorer 10 may require the following updates to be integrated first:\nKB2670838\nKB2756651\nKB2786081\nKB2878068\nKB289682010";
                    }
                    if (uName.ContainsIgnoreCase("KB2841134") || uName.EqualsIgnoreCase("Windows Internet Explorer 11"))
                    {
                        uName = "Internet Explorer 11";
                        NewUpdate.ToolTipText = "Internet Explorer 11 may require the following updates to be integrated first:\nKB2670838\nKB2756651\nKB2786081\nKB2834140\nKB2882822\nKB2888049\nKB2896820";
                    }

                    if (iFileName.ContainsIgnoreCase("LANGUAGEPACK") || InfoFile.EqualsIgnoreCase(".txt") || uDesc.EqualsIgnoreCase("Internet Explorer Language Pack"))
                    {
                        uName = uName.ReplaceIgnoreCase("Internet Explorer ", "IE") + " LangPack"; uDesc = uName;
                        NewUpdate.Group = lstUpdates.Groups[3];

                    }
                    else { NewUpdate.Group = lstUpdates.Groups[3]; }
                }
            }
            catch (Exception Ex)
            {
                new SmallError("Error trying to add update [inner:IE]", Ex, filePath + "\nStatus: " + lblStatus).Upload();
            }

            try
            {
                if (uName.ContainsIgnoreCase("KB2756651") || uName.ContainsIgnoreCase("KB2670838") || uName.ContainsIgnoreCase("KB2786081") || uName.ContainsIgnoreCase("KB2878068") || uName.ContainsIgnoreCase("KB2896820"))
                {
                    NewUpdate.ToolTipText = "This update may be needed so that you can integrate Internet Explorer 10/11.";
                }

                if (uName.ContainsIgnoreCase("KB2756651") || uName.ContainsIgnoreCase("KB2834140") || uName.ContainsIgnoreCase("KB2882822") || uName.ContainsIgnoreCase("KB2888049") || uName.ContainsIgnoreCase("KB2670838") || uName.ContainsIgnoreCase("KB2786081") || uName.ContainsIgnoreCase("KB2896820"))
                {
                    NewUpdate.ToolTipText = "This update may be needed so that you can integrate Internet Explorer 11.";
                }

                if (uName.ContainsIgnoreCase("KB958559") || uName.EqualsIgnoreCase("Windows Virtual PC (KB958559)"))
                {
                    uName = "Virtual PC";
                    uDesc = "Windows Virtual PC (KB958559)";
                    NewUpdate.Group = lstUpdates.Groups[1];
                }

                if (uName.ContainsIgnoreCase("KB2670838") || uName.ContainsIgnoreCase("KB3020369"))
                {
                    NewUpdate.Group = lstUpdates.Groups[1];
                }

                if (uName.ContainsIgnoreCase("2574819") || uName.EqualsIgnoreCase("KB2574819") || uName.ContainsIgnoreCase("2857650") || uName.EqualsIgnoreCase("KB2857650"))
                {
                    uDesc = "Prerequisite for RDP 8.x";
                    NewUpdate.Group = lstUpdates.Groups[1];
                }

                if (uName.ContainsIgnoreCase("Microsoft-Windows-PlatformUpdate-Win7-SRV08R2-Package-TopLevel")) { uName = "Package_for_KB2670838_GDR"; }
                if (uName.ContainsIgnoreCase("VirtualPC-Package~")) { uName = "Virtual PC"; }
                if (uName.ContainsIgnoreCase("VirtualPC-Package-TopLevel")) { uName = ""; }
                if (uName.ContainsIgnoreCase("WindowsUpdateClient-SelfUpdate-ActiveX")) { uName = uDesc = "Windows Update (ActiveX)"; }
                if (uName.ContainsIgnoreCase("WUClient-SelfUpdate-ActiveX")) { uName = uDesc = "Windows Update (ActiveX)"; }
                if (uName.ContainsIgnoreCase("WUClient-SelfUpdate-Aux")) { uName = uDesc = "Windows Update (Aux)"; }
                if (uName.ContainsIgnoreCase("WUClient-SelfUpdate-Core")) { uName = uDesc = "Windows Update (Core)"; }

                if (uName.ContainsIgnoreCase("Microsoft-Windows-Winhelp-Update-Client-TopLevel")) { uName = "Package_for_KB917607 (WinHlp32.exe)"; }
            }
            catch (Exception Ex)
            {
                new SmallError("Error trying to add update [inner:Extra]", Ex, filePath + "\nStatus: " + lblStatus).Upload();
            }

            if (!string.IsNullOrEmpty(uSupport) && !uSupport.ContainsIgnoreCase("MICROSOFT.COM")) { uSupport = "http://support.microsoft.com/kb/" + uSupport; }
            if (string.IsNullOrEmpty(uSupport)) { uSupport = "N/A"; }
            if (string.IsNullOrEmpty(uDesc)) { uDesc = uName; }
            if (string.IsNullOrEmpty(uLang) || uLang.EqualsIgnoreCase("neutral")) { uLang = "ALL"; }

            NewUpdate.Text = uName;
            try
            {
                if ((strFile.ContainsIgnoreCase("Microsoft-Windows-Client-LanguagePack-Package") || strFile.ContainsIgnoreCase("Microsoft-Windows-Client-Refresh-LanguagePack-Package")) && !strFile.ContainsIgnoreCase("KB2732059")) { NewUpdate.Group = lstUpdates.Groups[0]; }
            }
            catch (Exception Ex)
            {
                new SmallError("Error trying to add update [inner:LangPack]", Ex, filePath + "\nStatus: " + lblStatus).Upload();
            }

            NewUpdate.SubItems.Add(uDesc);
            NewUpdate.SubItems.Add(uLang);
            NewUpdate.SubItems.Add(cMain.BytesToString(double.Parse(uSize), true));
            NewUpdate.SubItems.Add(uArc);
            NewUpdate.SubItems.Add(oFileName);
            NewUpdate.SubItems.Add(MD5);

            uSupport = uSupport.ReplaceIgnoreCase("support.microsoft.com?kbid=", "support.microsoft.com/kb/", true);

            NewUpdate.SubItems.Add(uSupport);

            if (item == null)
            {
                NewUpdate.ImageIndex = 12;
            }
            else
            {
                if (item.Type == UpdateCache.UpdateType.LDR)
                {
                    updateInfo.UpdateType = UpdateCache.UpdateType.LDR;
                    NewUpdate.ImageIndex = 14;
                }
                else
                {
                    updateInfo.UpdateType = UpdateCache.UpdateType.GDR;
                    NewUpdate.ImageIndex = 13;
                }
            }

            uDate = uDate.ReplaceIgnoreCase("-", "/");
            if (uDate.Length > 10) { uDate = uDate.Substring(0, 10); }
            if (string.IsNullOrEmpty(uDate)) { uDate = "N/A"; }
            NewUpdate.SubItems.Add(uDate);

            NewUpdate.SubItems.Add(oFileName);

            if (!string.IsNullOrEmpty(uPName))
            {
                if (NewUpdate.ToolTipText.EqualsIgnoreCase("No description available.")) { NewUpdate.ToolTipText = ""; }

                try
                {
                    if (!string.IsNullOrEmpty(uPName) && !string.IsNullOrEmpty(uPVersion))
                    {
                        updateInfo.PackageName = uPName;
                        updateInfo.PackageVersion = uPVersion;
                    }
                }
                catch (Exception Ex)
                {
                    new SmallError("Set update tag info", Ex, NewUpdate).Upload();
                }
            }

            try
            {
                if (NewUpdate.Text.StartsWithIgnoreCase("KB") && NewUpdate.Text.Length == 8)
                {
                    NewUpdate.Text = NewUpdate.Text.ReplaceIgnoreCase("KB", "KB0");
                }
                if (NewUpdate.SubItems[1].Text.StartsWithIgnoreCase("KB") && NewUpdate.SubItems[1].Text.Length == 8)
                {
                    NewUpdate.SubItems[1].Text = NewUpdate.SubItems[1].Text.ReplaceIgnoreCase("KB", "KB0");
                }
            }
            catch (Exception Ex)
            {
                new SmallError("Error trying to add update [inner:KBNameLength]", Ex, filePath + "\nStatus: " + lblStatus).Upload();
            }

            if (iBusy && !AllowedOffline)
            {
                AllowedOffline = true;
                //   NewUpdate.Group = lstUpdates.Groups[6];
            }


            if (AllowedOffline)
            {
                if (NewUpdate.Text.StartsWithIgnoreCase("WINDOWS-LOCALPACK")) { NewUpdate.Group = lstUpdates.Groups[5]; }

                lstUpdates.Items.Add(NewUpdate);
            }
            else
            {
                try
                {

                    if (lstSilent.FindItemWithText(iFileName) == null && (lstSilent.FindItemWithText(MD5) == null || string.IsNullOrEmpty(MD5)))
                    {
                        // NewUpdate.Group = lstUpdates.Groups[6];
                        badUpdates.Add(NewUpdate);
                    }
                }
                catch (Exception Ex)
                {
                    new SmallError("Error trying to add update [inner:Silent]", Ex, filePath + "\nStatus: " + lblStatus).Upload();
                }
            }

            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst", false);
        }

        private string GetTabName()
        {
            string TabName = SelTab.Text;
            if (TabName.ContainsIgnoreCase("["))
            {
                while (TabName.ContainsIgnoreCase("[")) { TabName = TabName.Substring(0, TabName.Length - 1); }
            }
            TabName = TabName.Trim();
            return TabName;
        }

        private ListViewEx GetSelectedList()
        {
            ListViewEx SL = default(ListViewEx);
            string TabName = GetTabName();
            switch (TabName)
            {
                case "Addons":
                    SL = lstAddons;
                    break;
                case "Drivers":
                    SL = lstDrivers;
                    break;
                case "Files":
                    SL = lstFiles;
                    break;
                case "Gadgets":
                    SL = lstGadgets;
                    break;
                case "Services":
                    SL = lstServices;
                    break;
                case "Silent Installs + SFX":
                    SL = lstSilent;
                    break;
                case "Theme Packs":
                    SL = lstThemes;
                    break;
                case "Updates + Languages":
                    SL = lstUpdates;
                    break;
                case "Wallpapers":
                    SL = lstWallpapers;
                    break;
            }
            return SL;
        }
        private void CleanTemp(string Filter)
        {
            try
            {
                if (Directory.Exists(cOptions.WinToolkitTemp))
                {
                    foreach (string D in Directory.GetDirectories(cOptions.WinToolkitTemp, "*", SearchOption.TopDirectoryOnly))
                    {
                        if (D.ContainsIgnoreCase(Filter.ToUpper()))
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Cleaning temp directory [" + D + "]...");
                            Application.DoEvents();

                            Files.DeleteFolder(D, false);
                        }
                    }

                    cMain.FreeRAM();
                }
            }
            catch { }
        }

        private void cmdRF_Click(object sender, EventArgs e)
        {
            cMain.UpdateToolStripLabel(lblStatus, "Detecting selected list...");
            ListView SL = GetSelectedList();

            cMain.UpdateToolStripLabel(lblStatus, "Removing selected files...");

            if (SL != null)
            {
                if (SL.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select an item first!", "Invalid Item");
                    return;
                }

                foreach (ListViewItem item in SL.SelectedItems)
                {
                    item.Remove();
                    try
                    {
                        if (item.SubItems.Count > 4 && SL == lstUpdates)
                        {
                            string iD = item.SubItems[5].Text;
                            if (iD.ContainsIgnoreCase("\\") && iD.ContainsIgnoreCase("CINST_"))
                            {
                                while (!iD.EndsWithIgnoreCase("\\")) { iD = iD.Substring(0, iD.Length - 1); }
                                iD = iD.Substring(0, iD.Length - 1);
                                CleanTemp(iD);
                            }
                        }
                    }
                    catch { }
                }

                CheckListEmpty(SL);
                UpdateNames(true);
                cMain.UpdateToolStripLabel(lblStatus, "Items removed...");

            }
            try
            {
                if (TPMain.SelectedTab == TabBasic && TPBasic.SelectedTab == TabTweaks)
                {
                    mnuR.Visible = false;
                    foreach (TreeNode TN in tvTweaks.Nodes)
                    {
                        if (TN.Tag != null && TN.Tag.ToString().EqualsIgnoreCase("Reg") && TN.IsSelected) { TN.Remove(); }
                    }

                    foreach (TreeNode TN in tvTweaks.Nodes)
                    {
                        if (TN.Tag != null && TN.Tag.ToString().EqualsIgnoreCase("Reg")) { mnuR.Visible = true; break; }
                    }
                    cMain.UpdateToolStripLabel(lblStatus, "Items removed...");
                }
            }
            catch (Exception Ex)
            {
                cMain.UpdateToolStripLabel(lblStatus, "Error...");
                MessageBox.Show("Error trying to remove files" + Environment.NewLine + Ex.Message, "Error");
                cMain.WriteLog(this, "Error trying to remove file", Ex.Message, lblStatus.Text);
            }

            EnablePan();
            cMain.FreeRAM();

        }

        private void cmdRA_Click(object sender, EventArgs e)
        {
            cMain.UpdateToolStripLabel(lblStatus, "Detecting selected list...");
            ListView SL = GetSelectedList();

            if (SL != null)
            {
                cMain.UpdateToolStripLabel(lblStatus, "Removing all files...");
                SL.Items.Clear();
            }

            if (TPMain.SelectedTab == TabBasic && TPBasic.SelectedTab == TabTweaks)
            {
                cMain.UpdateToolStripLabel(lblStatus, "Removing imported regs...");
                mnuR.Visible = false;
                foreach (TreeNode TN in tvTweaks.Nodes)
                {
                    if (TN.Tag != null && TN.Tag.ToString().EqualsIgnoreCase("Reg")) { TN.Remove(); }
                }

                cMain.UpdateToolStripLabel(lblStatus, "Items removed...");
            }

            if (SL == lstUpdates)
            {
                CleanTemp("cInst");
            }

            UpdateNames(true);
            EnablePan();
            cMain.UpdateToolStripLabel(lblStatus, "All items removed...");
            cMain.FreeRAM();
        }

        private void renameSilent()
        {
            try
            {
                if (lstSilent.Items.Count > 0)
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Renaming Silent Installers...");
                    foreach (ListViewItem LST in lstSilent.Items)
                    {

                        IEnumerable<ListViewItem> results = lstSilent.Items.Cast<ListViewItem>().Where(n => n.Text.ToUpper() == LST.Text.ToUpper()).ToArray();

                        if (results.Count() > 1)
                        {
                            int i = 0;
                            foreach (ListViewItem dupLST in results)
                            {
                                dupLST.Text += "_" + (++i).ToString(CultureInfo.InvariantCulture);
                            }

                        }
                    }
                }
            }
            catch (Exception Ex) { cMain.WriteLog(this, "Error renaming silent installers", Ex.Message, lblStatus.Text); }
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            if (cmdStart.Text.EqualsIgnoreCase("Start"))
            {
                aErr = "";
                nErr = 0;

                if (lstAddons.Items.Count == 0 && lstFiles.Items.Count == 0 && lstDrivers.Items.Count == 0 &&
                      lstUpdates.Items.Count == 0 && lstComponents.CheckedItems.Count == 0 &&
                      CountNodes(tvTweaks) == 0 && lstThemes.Items.Count == 0 && lstGadgets.Items.Count == 0 &&
                      lstServices.CheckedItems.Count == 0 && lstWallpapers.Items.Count == 0 && lstSilent.Items.Count == 0 && CountNodes(tvComponents) == 0)
                {
                    MessageBox.Show("You must select at least 1 task to complete!", "Nothing Selected");
                    return;
                }

                renameSilent();

                cMain.UpdateToolStripLabel(lblStatus, "Counting IE...");
                if (lstUpdates.Items.Cast<ListViewItem>().Count(U => U.Text.StartsWithIgnoreCase("Internet Explorer")) > 1)
                {
                    DialogResult DR = MessageBox.Show("You are trying to install multiple Internet Explorer verions. Please remove all but the latest.\n\nContinue anyway?", "NOTICE", MessageBoxButtons.YesNo);
                    if (DR != DialogResult.Yes)
                    {
                        ListViewEx.LVE = GetSelectedList();
                        return;
                    }
                }

                foreach (ListViewGroup lvg in lstUpdates.Groups)
                {
                    for (int ii = 0; ii < lvg.Items.Count; ii++)
                    {
                        if (lvg.Items[ii].Group == null)
                            lvg.Items.RemoveAt(ii--);
                    }
                }



                lstUpdates.SelectedItems.Clear();
                cMain.UpdateToolStripLabel(lblStatus, "Sorting IE...");



                TabPage selectedTab = TPMain.SelectedTab;
                TabPage subTab = null;
                bool moved = false;

                if (TPMain.SelectedTab == TabBasic)
                {
                    subTab = TPBasic.SelectedTab;
                }
                else if (TPMain.SelectedTab == TabAdvanced)
                {
                    subTab = TPAdvanced.SelectedTab;
                }

                try
                {

                    foreach (ListViewItem LST in lstUpdates.Groups[3].Items)
                    {
                        if (LST.Text.StartsWithIgnoreCase("Internet Explorer") && LST.Index != 0)
                        {
                            TPMain.SelectedTab = TabBasic;
                            TPBasic.SelectedTab = TabUpdates;

                            LST.Selected = true;
                            do
                            {
                                moved = true;
                            }
                            while (cMain.MoveListViewItem(lstUpdates, cMain.MoveDirection.Up) == false);
                            break;
                        }
                    }
                }
                catch (Exception Ex)
                {
                    new SmallError("IE Move Error", Ex, "selectedTab=" + selectedTab
                                                    + Environment.NewLine + "subTab=" + subTab).Upload();
                }

                try
                {
                    if (moved)
                    {
                        TPMain.SelectedTab = selectedTab;
                        if (TPMain.SelectedTab == TabBasic)
                        {
                            TPBasic.SelectedTab = subTab;
                        }
                        else if (TPMain.SelectedTab == TabAdvanced)
                        {
                            TPAdvanced.SelectedTab = subTab;
                        }
                    }
                }
                catch (Exception Ex)
                {
                    new SmallError("IE Move Error [Tab]", Ex, "selectedTab=" + selectedTab
                                   + Environment.NewLine + "subTab=" + subTab).Upload();
                }

                Refresh();
                cMain.UpdateToolStripLabel(lblStatus, "Scanning for AntiVirus...");
                if (cMain.DetectAntivirus() && AVNotice == false)
                {
                    cMain.AVShown = true;
                    Application.DoEvents();
                    var AV = new frmAntiVirus();
                    AV.GBAV.Visible = true;
                    AV.chkAV.Visible = false;
                    AV.ShowDialog();
                    AVNotice = true;
                }

                runTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-tt", System.Globalization.CultureInfo.InvariantCulture);
                Refresh();
                cMain.FreeRAM();

                try
                {
                    cMain.sList = "";
                    SaveLog(null, false);

                    bool writeCompareList = false;
                    if (writeCompareList)
                    {
                        using (StreamWriter sw = new StreamWriter("C:\\_new.txt"))
                        {
                            sw.WriteLine(cMain.sList);
                        }

                        using (StreamWriter sw = new StreamWriter("C:\\_old.txt"))
                        {
                            sw.WriteLine(lPreset);
                        }
                    }

                    if (!cMain.sList.StartsWithIgnoreCase(lPreset) || string.IsNullOrEmpty(lPreset))
                    {
                        if (!Directory.Exists(cMain.Root + "Last Sessions"))
                        {
                            cMain.CreateDirectory(cMain.Root + "Last Sessions");
                        }
                        string SA = runTime;

                        string SL = "";
                        int dResult = 0;
                        using (var pPN = new frmAIOPresetName())
                        {
                            pPN.txtPresetName.Text = SA;
                            pPN.ShowDialog();
                            SL = pPN.txtPresetName.Text;
                            dResult = pPN.dResult;
                        }

                        if (dResult == -1)
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Task Aborted!");
                            return;
                        }
                        if (dResult == 1)
                        {
                            cmdStart.Enabled = false;
                            cMain.UpdateToolStripLabel(lblStatus, "Saving Last_Session(" + SL + ").ini...");
                            Application.DoEvents();
                            if (SA == SL)
                            {
                                SaveLog(cMain.Root + "Last Sessions\\" + SL + ".ini", false);
                                File.Copy(cMain.Root + "Last Sessions\\" + SL + ".ini", DVD + SL + ".ini");
                            }
                            else
                            {
                                SaveLog(cMain.Root + "Last Sessions\\" + SA + "_" + SL + ".ini", false);
                                File.Copy(cMain.Root + "Last Sessions\\" + SA + "_" + SL + ".ini", DVD + SA + "_" + SL + ".ini");
                            }
                            lPreset = cMain.sList;
                        }
                    }
                }
                catch
                {
                }
                iBusy = true;
                starting = true;

                try
                {
                    cmdCancel.PerformClick();
                    cmdSICancel.PerformClick();
                }
                catch
                {
                }

                starting = false;

                LangINI = "";
                cmdStart.Enabled = true;
                cMain.UpdateToolStripLabel(lblStatus, "Starting...");
                cmdStart.Text = "Cancel";
                cmdStart.Image = Properties.Resources.Close;

                if (BWRun.IsBusy)
                {
                    return;
                }
                cmdSBODefault.Visible = false;
                mnuR.Visible = false;
                mnuA.Visible = false;
                mnuSaveLoad.Visible = false;
                mnuRefresh.Visible = false;
                tvTweaks.Enabled = false;
                lstAddons.Enabled = false;
                tvComponents.Enabled = false;
                lstComponents.Enabled = false;
                lstUpdates.HeaderStyle = ColumnHeaderStyle.Nonclickable;
                mnuClear.Visible = false;
                //lstUpdates.Enabled = false;
                lstDrivers.Enabled = false;
                lstGadgets.Enabled = false;
                lstThemes.Enabled = false;
                lstServices.Enabled = false;
                lstOptions.Enabled = false;
                lstWallpapers.Enabled = false;
                lstSilent.Enabled = false;
                cAddon.AddonCancel = false;
                //PanArrange.Enabled = false;
                PB.Visible = true;
                EnablePan();

                if (lstUpdates.Items.Count > 0)
                {
                    foreach (ListViewItem LST in lstUpdates.Items)
                    {
                        LST.ImageIndex = -1;
                    }
                }

                //WriteLogA(CH);
                tSB = false;
                tSBCT = "";
                try
                {
                    if (string.IsNullOrEmpty(DVD))
                    {
                        FindNode(tvTweaks, "Change Setup Background").Remove();

                    }
                    else
                    {
                        if (FindNode(tvTweaks, "Change Setup Background").Checked)
                        {
                            tSB = true;
                            Files.DeleteFile(cOptions.WinToolkitTemp + "\\Background.bmp");
                            tSBCT = FindNode(tvTweaks, "Change Setup Background").Nodes[0].Text;
                            Image Dummy = Image.FromFile(tSBCT);
                            Dummy.Save(cOptions.WinToolkitTemp + "\\Background.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                            tSBCT = cOptions.WinToolkitTemp + "\\Background.bmp";
                        }
                    }
                }
                catch
                {
                    tSBCT = "";
                    tSB = false;
                }

                try
                {
                    lstIUpdates.Items.Clear();
                    lstIUpdates.Groups.Clear();
                    lstIUpdates.Columns.Clear();
                    lstIDrivers.Items.Clear();
                    lstIDrivers.Groups.Clear();
                    lstIDrivers.Columns.Clear();
                    lstIAddons.Items.Clear();
                    lstIAddons.Groups.Clear();
                    lstIAddons.Columns.Clear();
                    lstIThemes.Items.Clear();
                    lstIThemes.Groups.Clear();
                    lstIThemes.Columns.Clear();
                    lstIGadgets.Items.Clear();
                    lstIGadgets.Groups.Clear();
                    lstIGadgets.Columns.Clear();
                    lstIWallpapers.Items.Clear();
                    lstIWallpapers.Groups.Clear();
                    lstIWallpapers.Columns.Clear();
                    lstISilent.Items.Clear();
                    lstISilent.Groups.Clear();
                    lstISilent.Columns.Clear();
                }
                catch { }

                try
                {
                    TPMain.TabPages.Remove(NewTab);
                }
                catch { }

                cMain.tCancel = false;
                s = 0; m = 0; h = 0; d = 0;
                timEllapsed.Enabled = true;
                lblTime.Visible = true;
                BWRun.RunWorkerAsync();
            }
            else
            {
                DialogResult DR = MessageBox.Show("Are you sure you wish to cancel?", "Are you sure?", MessageBoxButtons.YesNo);
                if (DR != DialogResult.Yes)
                {
                    return;
                }

                cmdStart.Enabled = false;
                cAddon.AddonCancel = true;
                BWRun.CancelAsync();
                cMain.tCancel = true;

                if (lblStatus.Text.StartsWithIgnoreCase("Rebuilding WIM file")) { cMain.KillProcess("imagex"); }
                cMain.UpdateToolStripLabel(lblStatus, "Stopping, please wait...");
                cMain.KillProcess("DISM");

                cMain.KillProcess("pkgmgr");
                cMain.KillProcess("dismhost");
            }
        }

        private void lstOptions_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lAIO || iBusy) { return; }
            if (e.Item.Text.EqualsIgnoreCase("Enabled CMD"))
            {
                cOptions.AICommands = e.Item.Checked;
                return;
            }

            if (e.Item.Text.EqualsIgnoreCase("RunOnce Idle Timeout") && e.Item.Checked)
            {
                string result = cMain.InputBox(
                        "Please enter the amount of time (minutes) that the run once will wait if the user has been idle.",
                        e.Item.Text, e.Item.SubItems[1].Text, 1440);

                if (string.IsNullOrEmpty(result) || result.EqualsIgnoreCase("0"))
                {
                    e.Item.Checked = false;
                    return;
                }

                e.Item.SubItems[1].Text = result;
            }


            if (e.Item.Text.EqualsIgnoreCase("Delete Silent Installers"))
            {
                if (e.Item.Checked)
                {
                    cmdDeleteInstallers.Image = Properties.Resources.Checked;
                }
                else
                {
                    cmdDeleteInstallers.Image = Properties.Resources.Unchecked;
                    return;
                }
            }


            if (e.Item.Text.EqualsIgnoreCase("Log Registry Changes"))
            {
                cOptions.RegistryLog = e.Item.Checked;
                return;
            }


            if (lAIO == false)
            {
                if (e.Item.Text.EqualsIgnoreCase("Delete Silent Installers") && e.Item.Checked)
                {
                    DialogResult DR =
                        MessageBox.Show(
                               "This will delete the WinToolkit_Apps folder and any entries for the silent installers. It will be as if no silent installers have been integrated into this image previously." +
                               Environment.NewLine + Environment.NewLine + "Are you sure?", "Note",
                               MessageBoxButtons.YesNo);
                    if (DR == DialogResult.No)
                    {
                        e.Item.Checked = false;
                        return;
                    }
                }
                if (e.Item.Text.EqualsIgnoreCase("Current OS") && e.Item.Checked)
                {
                    DialogResult DR =
                          MessageBox.Show(
                                 "This will integrate the selected tweaks into the currently installed operating system NOT your selected image! This option was added to help me diagnose problems and should not be checked unless you actually want to add the tweaks to your current OS." +
                                 Environment.NewLine + Environment.NewLine + "Would you like to apply them now?", "Note",
                                 MessageBoxButtons.YesNo);
                    if (DR == DialogResult.No)
                    {
                        e.Item.Checked = false;
                        return;
                    }
                    Enabled = false;
                    cTweaks.IntegrateTweaks(tvTweaks, lblStatus, cMain.Arc64 ? "x64" : "x86", "C:\\", false,
                                                              lstOptions.FindItemWithText("Convert Registry Test").Checked, null, this, currentlyMounted);
                    cMain.UpdateToolStripLabel(lblStatus, "Tweak installation completed...");
                    Application.DoEvents();
                    Enabled = true;
                }

                if (e.Item.Text.EqualsIgnoreCase("Read Tasks.txt") && e.Item.Checked)
                {
                    e.Item.Checked = false;
                    var FAAddons = new OpenFileDialog();
                    FAAddons.Title = "Browse for Addon...";
                    FAAddons.Filter = "Win Toolkit Addons *.WA| *.WA|All Files *.*|*.*";
                    FAAddons.Multiselect = false;
                    if (FAAddons.ShowDialog() != DialogResult.OK)
                        return;
                    cMain.UpdateToolStripLabel(lblStatus, "Extracting " + FAAddons.FileName + "...");
                    cMain.ExtractFiles(FAAddons.FileName, cOptions.WinToolkitTemp + "\\AddonT\\", this, "Tasks.txt");

                    if (!File.Exists(cOptions.WinToolkitTemp + "\\AddonT\\Tasks.txt"))
                    {
                        File.Copy(FAAddons.FileName, cOptions.WinToolkitTemp + "\\Temp.WA", true);
                        cMain.ExtractFiles(cOptions.WinToolkitTemp + "\\Temp.WA", cOptions.WinToolkitTemp + "\\AddonT\\", this, "Tasks.txt");
                        File.Delete(cOptions.WinToolkitTemp + "\\Temp.WA");
                    }

                    if (File.Exists(cOptions.WinToolkitTemp + "\\AddonT\\Tasks.txt"))
                    {
                        using (var RT = new StreamReader(cOptions.WinToolkitTemp + "\\AddonT\\Tasks.txt", true))
                        {
                            MessageBox.Show(RT.ReadToEnd(), "Tasks.txt Result");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not extract addon info", "Error");
                    }

                    Files.DeleteFile("Tasks.txt");
                    cMain.UpdateToolStripLabel(lblStatus, "");
                }
            }
            UpdateNames(false);

        }

     
        private void tvTweaks_Checked(TreeNode e)
        {
            try
            {
            
                if (!e.Checked && e.Nodes.Count > 0)
                {
                    if (e.Nodes[0].Tag.ToString().EqualsIgnoreCase("C")) { e.Nodes[0].Remove(); }
                }
                if (lAIO == false && e.Checked)
                {
                    if (e.Tag == null)
                    {
                        UpdateNames(false);
                        return;
                    }

                    string CT = "";
                    if (e.Nodes.Count > 0) { CT = e.Nodes[0].Text; }
                    lAIO = true;
                    cMain.TweakChoice = "";
                    string T = "";
                    string RO = "";

                    tvTweaks.EndUpdate();

                    #region TAGS
                    if (e.Tag.ToString().EqualsIgnoreCase("chkMCSBG23"))
                    {
                        T = "File";
                    }
                    if (e.Tag.ToString().EqualsIgnoreCase("chkECDB"))
                    {
                        T = "File";
                    }
                    if (e.Tag.ToString().EqualsIgnoreCase("chkLLogBack"))
                    {
                        T = "File";
                    }
                    if (e.Tag.ToString().EqualsIgnoreCase("chkMCPFL"))
                    {
                        T = "Folder";
                    }
                    if (e.Tag.ToString().EqualsIgnoreCase("chkMCUFL"))
                    {
                        T = "Folder";
                    }
                    if (e.Tag.ToString().EqualsIgnoreCase("chkMCSB22"))
                    {
                        T = "File";
                    }

                    if (e.Tag.ToString().EqualsIgnoreCase("chkSISUBSDS48"))
                    {
                        DialogResult DR =
                              MessageBox.Show("This tweaks requires 'KB2496290', would you like to download it now?", "Notice",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (DR == DialogResult.Yes)
                        {
                            cMain.OpenLink("http://support.microsoft.com/kb/2496290");
                        }
                    }

                    if (e.Tag.ToString().EqualsIgnoreCase("chkIEHP46"))
                    {
                        T = "P";
                        RO = cMain.InputBox("Please enter the homepage you wish to use for IE",
                                                                     "Internet Explorer Homepage", CT);

                    }

                    if (e.Tag.ToString().EqualsIgnoreCase("chkIECDDL53"))
                    {
                        T = "P";
                        RO = cMain.InputBox("Please enter the location you want IE files to download to.",
                                                                     "Download Directory", CT);

                    }

                    if (e.Tag.ToString().EqualsIgnoreCase("chkIECTOIEW52"))
                    {
                        T = "P";
                        RO = cMain.InputBox("Please enter a custom title for IE.", "IE Title", CT);
                    }

                    if (e.Tag.ToString().EqualsIgnoreCase("chkMCRO70"))
                    {
                        T = "P";
                        RO = cMain.InputBox("Please enter the name of the registered owner.", "Registered Owner", CT);
                    }

                    if (e.Tag.ToString().EqualsIgnoreCase("chkMCRO71"))
                    {
                        T = "P";
                        RO = cMain.InputBox("Please enter the name of the registered organization.", "Registered Organization", CT);
                    }

                    if (T.EqualsIgnoreCase("P") && string.IsNullOrEmpty(RO)) { e.Checked = false; }
                    if (T.EqualsIgnoreCase("P") && !string.IsNullOrEmpty(RO))
                    {
                        try
                        {
                            e.Nodes[0].Text = RO;
                            e.Nodes[0].Tag = "C";
                        }
                        catch
                        {
                            e.Nodes.Add(RO);
                        }
                        HideCheckBox(tvTweaks, e.Nodes[0]);
                        e.Nodes[0].Tag = "C";
                        e.Expand();
                    }
                    if (T != "P")
                    {
                        var cboRChoice = new ComboBox();
                        if (e.Tag.ToString().EqualsIgnoreCase("chkSMRV") || e.Tag.ToString().EqualsIgnoreCase("chkSMRTV") ||
                            e.Tag.ToString().EqualsIgnoreCase("chkSMRP") || e.Tag.ToString().EqualsIgnoreCase("chkSMPF") ||
                            e.Tag.ToString().EqualsIgnoreCase("chkSMRM") || e.Tag.ToString().EqualsIgnoreCase("chkSMRG") ||
                            e.Tag.ToString().EqualsIgnoreCase("chkSMSD") || e.Tag.ToString().EqualsIgnoreCase("chkSMRD") ||
                            e.Tag.ToString().EqualsIgnoreCase("chkSMRCP") || e.Tag.ToString().EqualsIgnoreCase("chkSMC"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Hide");
                            cboRChoice.Items.Add("Link");
                            cboRChoice.Items.Add("Menu");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkSMAdminTools"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Hide");
                            cboRChoice.Items.Add("All Programs");
                            cboRChoice.Items.Add("All Programs and Start Menu");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkSMRCT") || e.Tag.ToString().EqualsIgnoreCase("chkSMRDPROG") || e.Tag.ToString().EqualsIgnoreCase("chkSMRDP") || e.Tag.ToString().EqualsIgnoreCase("chkCMSF") || e.Tag.ToString().EqualsIgnoreCase("chkSMRHS") || e.Tag.ToString().EqualsIgnoreCase("chkSHG") || e.Tag.ToString().EqualsIgnoreCase("chkSMSN") || e.Tag.ToString().EqualsIgnoreCase("chkSMSRC") || e.Tag.ToString().EqualsIgnoreCase("chkSMSR"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Hide");
                            cboRChoice.Items.Add("Show");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkSMDHNI23"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Highlight");
                            cboRChoice.Items.Add("Don't Highlight");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkSMNORPTD") || e.Tag.ToString().EqualsIgnoreCase("chkSMNRIDJL"))
                        {
                            T = "Combo";

                            cboRChoice.Items.Add("10");
                            for (int i = 1; i < 16; i++)
                            {
                                if (!cboRChoice.Items.Contains(i.ToString())) { cboRChoice.Items.Add(i.ToString()); }
                            }

                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkECDPIS43"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Default 100% (96)");
                            cboRChoice.Items.Add("102");
                            cboRChoice.Items.Add("104");
                            cboRChoice.Items.Add("106");
                            cboRChoice.Items.Add("110");
                            cboRChoice.Items.Add("Medium 125%");
                            cboRChoice.Items.Add("Larger 150%");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkSELSC3") || e.Tag.ToString().EqualsIgnoreCase("chkSMSAPMN") || e.Tag.ToString().EqualsIgnoreCase("chkSMSPCP") || e.Tag.ToString().EqualsIgnoreCase("chkSMOBWMH") || e.Tag.ToString().EqualsIgnoreCase("chkSMECMDD"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Enable");
                            cboRChoice.Items.Add("Disable");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkDDIS74"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("16");
                            cboRChoice.Items.Add("32 (Default)");
                            cboRChoice.Items.Add("48");
                            cboRChoice.Items.Add("64");
                            cboRChoice.Items.Add("80");
                            cboRChoice.Items.Add("96");
                            cboRChoice.Items.Add("112");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkSCMD"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Kernel [Default]");
                            cboRChoice.Items.Add("Complete");
                            cboRChoice.Items.Add("Small 64KB");
                            cboRChoice.Items.Add("None");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkDCTWF"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Always combine, hide labels");
                            cboRChoice.Items.Add("Combine when taskbar is full");
                            cboRChoice.Items.Add("Never combine");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkMWCMDT37"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Default");
                            cboRChoice.Items.Add("Blue");
                            cboRChoice.Items.Add("Bright Blue");
                            cboRChoice.Items.Add("Bright Cyan");
                            cboRChoice.Items.Add("Bright Green");
                            cboRChoice.Items.Add("Bright Magenta");
                            cboRChoice.Items.Add("Bright Red");
                            cboRChoice.Items.Add("Bright Yellow");
                            cboRChoice.Items.Add("Cyan");
                            cboRChoice.Items.Add("Gray");
                            cboRChoice.Items.Add("Green");
                            cboRChoice.Items.Add("Magenta");
                            cboRChoice.Items.Add("Red");
                            cboRChoice.Items.Add("Yellow/Brown");
                            cboRChoice.Items.Add("White");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkSDASSL60"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Mute all other sounds");
                            cboRChoice.Items.Add("Reduce all other by 80%");
                            cboRChoice.Items.Add("Reduce all other by 50%");
                            cboRChoice.Items.Add("Do nothing");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkEHCDOD"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Show");
                            cboRChoice.Items.Add("Hide");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("cboLLogText"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Windows Default");
                            cboRChoice.Items.Add("Dark Text Shadows and Light Buttons");
                            cboRChoice.Items.Add("No Shadows and Opaque Buttons");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkEAVS70"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Let Windows Decide");
                            cboRChoice.Items.Add("Best Appearance");
                            cboRChoice.Items.Add("Best Performance");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkSPS72"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Programs");
                            cboRChoice.Items.Add("Background Services");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkSDMDCS35"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Enable Checking");
                            cboRChoice.Items.Add("Disable Checking");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkIEWAWTO63"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("A Blank Page");
                            cboRChoice.Items.Add("First Homepage");
                            cboRChoice.Items.Add("'New Tab' Page");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkIEWPUE64"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Let IE Decide");
                            cboRChoice.Items.Add("New Window");
                            cboRChoice.Items.Add("New Tab");
                        }
                        //Show_StatusBar

                        if (e.Tag.ToString().EqualsIgnoreCase("chkIESIESB66"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Show Status Bar");
                            cboRChoice.Items.Add("Hide Status Bar");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkIEGPUSR66"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("GPU Rendering On");
                            cboRChoice.Items.Add("Software Rendering");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkEMHT1"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("20");
                            cboRChoice.Items.Add("50");
                            cboRChoice.Items.Add("100");
                            cboRChoice.Items.Add("150");
                            cboRChoice.Items.Add("200");
                            cboRChoice.Items.Add("250");
                            cboRChoice.Items.Add("300");
                            cboRChoice.Items.Add("350");
                            cboRChoice.Items.Add("400 (Default)");
                        }

                        if (e.Tag.ToString().EqualsIgnoreCase("chkESCPV22"))
                        {
                            T = "Combo";
                            cboRChoice.Items.Add("Category");
                            cboRChoice.Items.Add("Large Icons");
                            cboRChoice.Items.Add("Small Icons");
                        }

                        #endregion

                        if (!string.IsNullOrEmpty(T))
                        {
                            var F = new frmTweaks();
                            F.Text = e.Text;

                            if (T.EqualsIgnoreCase("Folder") | T.EqualsIgnoreCase("File"))
                            {
                                F.txtRInfo.Visible = true;
                                F.cmdRBrowse.Visible = true;
                                try
                                {
                                    F.txtRInfo.Text = e.Nodes[0].Text;
                                }
                                catch
                                {
                                }
                            }
                            if (T.EqualsIgnoreCase("Combo"))
                            {
                                foreach (string S in cboRChoice.Items)
                                {
                                    F.cboRChoice.Items.Add(S);
                                }
                                cboRChoice.Dispose();
                                F.cboRChoice.Visible = true;
                                F.cboRChoice.SelectedIndex = 0;
                                try
                                {
                                    F.cboRChoice.Text = e.Nodes[0].Text;
                                }
                                catch
                                {
                                }
                            }

                            cMain.TweakChoice = "";
                            F.ShowDialog();
                            F.Dispose();
                            if (!string.IsNullOrEmpty(cMain.TweakChoice))
                            {
                                try
                                {
                                    e.Nodes[0].Text = cMain.TweakChoice;
                                    e.Nodes[0].Tag = "C";
                                }
                                catch
                                {
                                    e.Nodes.Add(cMain.TweakChoice);
                                }
                                HideCheckBox(tvTweaks, e.Nodes[0]);
                                e.Nodes[0].Tag = "C";
                                e.Expand();
                            }
                            else
                            {
                                e.Checked = false;
                                if (e.Nodes.Count > 0) { e.Nodes[0].Remove(); }
                            }
                        }
                    }
                    lAIO = false;
                }
            }
            catch (Exception Ex)
            {
                string extended = "Unknown Node.";
                if (!string.IsNullOrEmpty(e.Text))
                {
                    extended = "Node:" + e.Text;
                }

                new SmallError("Tweak Checked Error", Ex, extended, null).Upload();

            }
            tvTweaks.EndUpdate();
            UpdateNames(false);
        }

        private void lstAddons_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lstAddons.FocusedItem.SubItems[7].Text))
                {
                    Process.Start(lstAddons.FocusedItem.SubItems[7].Text);
                }
            }
            catch
            {
            }
        }

        private void lstServices_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (iAdding == false)
            {
                lstServices.Columns[0].Text = "Name (" + lstServices.CheckedItems.Count + ")";
                tabServices.Text = "Services [" + lstServices.CheckedItems.Count + "]";

                int AT = lstComponents.CheckedItems.Count + CountNodes(tvComponents) + lstFiles.Items.Count + lstServices.CheckedItems.Count + lstSilent.Items.Count + CountNodes(tvTweaks);
                TabAdvanced.Text = "Advanced [" + AT + "]";
            }
        }

        private void mnuRefresh_Click(object sender, EventArgs e)
        {
            DialogResult DR =
                  MessageBox.Show(
                         "This will undo EVERYTHING you have changed to their default settings, do you wish to continue?",
                         "Are you sure?", MessageBoxButtons.YesNo);
            if (DR != DialogResult.Yes)
            {
                return;
            }
            foreach (ListViewItem LST in lstServices.Items)
            {
                LST.SubItems[1].Text = LST.SubItems[2].Text;
                LST.Checked = false;
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            var SFD = new SaveFileDialog();
            SFD.Title = "Save Settings";
            SFD.Filter = "AIO Settings *.ini|*.ini";

            if (SFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SaveLog(SFD.FileName, true);

        }

        private void ClearLists(bool empty = false, TabPage TP = null)
        {
            lAIO = true;
            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - Addons' setting..."); if (TP == TabAddons || TP == null) { lstAddons.Items.Clear(); }
                cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - Drivers' setting..."); if (TP == TabDrivers || TP == null) { lstDrivers.Items.Clear(); }
                cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - Gadgets' setting..."); if (TP == tabFiles || TP == null) { lstFiles.Items.Clear(); }
                cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - Gadgets' setting..."); if (TP == TabGadgets || TP == null) { lstGadgets.Items.Clear(); }
                cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - Silent' setting..."); if (TP == tabSilent || TP == null) { lstSilent.Items.Clear(); }
                cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - Themes' setting..."); if (TP == TabThemes || TP == null) { lstThemes.Items.Clear(); }
                cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - Updates' setting..."); if (TP == TabUpdates || TP == null) { lstUpdates.Items.Clear(); }
                cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - Wallpapers' setting..."); if (TP == TabWallpapers || TP == null) { lstWallpapers.Items.Clear(); }

                cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - Components' setting...");
                if (TP == tabComponents || TP == null)
                {
                    foreach (ListViewItem LST in lstComponents.Items) { LST.Checked = false; }
                }

                cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - Services' setting...");
                if (TP == tabServices || TP == null)
                {
                    foreach (ListViewItem LST in lstServices.Items) { LST.Checked = false; LST.SubItems[1].Text = LST.SubItems[2].Text; }
                }

                cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - Tweaks' setting...");
                if (TP == TabTweaks || TP == null)
                {
                    ListNodes(tvTweaks, true);
                    foreach (TreeNode LST in tvList) { LST.Checked = false; }
                }

                cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - vLite' setting...");
                if (TP == tabComponents2 || TP == null)
                {
                    ListNodes(tvComponents, true);
                    foreach (TreeNode LST in tvList) { LST.Checked = false; }
                }

                cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - Options' setting...");
                if (TP == TabOptions || TP == null)
                {
                    foreach (ListViewItem LST in lstOptions.Items)
                    {
                        LST.Checked = false;
                        cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - Addons' setting [" + LST.Text + "]...");
                        if (LST.SubItems.Count > 1)
                        {
                            LST.SubItems[1].Text = "";
                            LST.Checked = !empty && Convert.ToBoolean(LST.SubItems[2].Text);
                        }
                    }
                }
                cMain.UpdateToolStripLabel(lblStatus, "Checking 'Clear Lists - Names' setting...");
                UpdateNames(true);
            }
            catch { }

            cMain.UpdateToolStripLabel(lblStatus, "All lists should now be restored to their default setting.");
            lAIO = false;
            Application.DoEvents();
            cMain.FreeRAM();
        }

        private void LoadPreset(string pFile, TabPage TP, string Filter = "")
        {
            if (!File.Exists(pFile))
            {
                pFile = cMain.Root + "Last Sessions\\" + pFile + ".ini";
            }

            if (!File.Exists(pFile))
            {
                return;
            }
            string OrigText = Text;
            TPMain.Visible = false;
            Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
            this.Enabled = false;

            ClearLists(true, TP);
            lAIO = true;
            lstAddons.BeginUpdate();
            tvComponents.BeginUpdate();
            lstDrivers.BeginUpdate();
            lstFiles.BeginUpdate();
            lstGadgets.BeginUpdate();
            lstOptions.BeginUpdate();
            lstServices.BeginUpdate();
            lstSilent.BeginUpdate();
            lstThemes.BeginUpdate();
            tvTweaks.BeginUpdate();
            lstUpdates.BeginUpdate();
            lstWallpapers.BeginUpdate();

            Application.DoEvents();

            try
            {
                int intC = 1;
                string List = "";
                using (var SR = new StreamReader(pFile))
                {
                    List = SR.ReadToEnd();
                }
                lPreset = "";
                cLoad = List;
                int currentSection = 0;
                int i = 0;

                int intT = List.Split(Environment.NewLine.ToCharArray()).Count(S => !string.IsNullOrEmpty(S) && !S.StartsWithIgnoreCase("*") && !S.StartsWithIgnoreCase("#"));
                var Dummy = new TabPage(); ;

                bool options = false;
                foreach (string S in List.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    try
                    {
                        if (S.StartsWithIgnoreCase("#"))
                        {
                            if (options)
                                break;

                            if (S.StartsWithIgnoreCase("#Options"))
                                options = true;

                            continue;
                        }
                        if (!options)
                            continue;

                        if (!string.IsNullOrEmpty(Filter) && Filter.ContainsIgnoreCase("Options")) { TP = TabOptions; }
                        if (TP == TabOptions || TP == null)
                        {
                            if (S.ContainsIgnoreCase("::"))
                            {
                                string SN = S;
                                string SI = S;

                                while (SN.ContainsIgnoreCase("::"))
                                {
                                    SN = SN.Substring(0, SN.Length - 1);
                                }
                                SN = SN.Substring(0, SN.Length - 1);

                                while (SI.ContainsIgnoreCase("::"))
                                {
                                    SI = SI.Substring(1);
                                }

                                SI = SI.Substring(1);

                                if (File.Exists(SI))
                                {
                                    lstOptions.FindItemWithText(SN).Checked = true;
                                    lstOptions.FindItemWithText(SN).SubItems[1].Text = SI;
                                    lPreset += S + Environment.NewLine;
                                }
                                //Temporary
                                if (SN.StartsWithIgnoreCase("Prompt"))
                                {
                                    lstOptions.FindItemWithText(SN).Checked = true;
                                    lPreset += S + Environment.NewLine;
                                }
                                //Temporary^^^
                            }
                            else
                            {
                                lstOptions.FindItemWithText(S).Checked = true;
                                lPreset += S + Environment.NewLine;
                            }

                            try
                            {

                            }
                            catch
                            {
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                }

                lstOptions.FindItemWithText("Read Tasks.txt").Checked = false;

                foreach (string S in List.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!string.IsNullOrEmpty(Filter)) { TP = Dummy; }
                    try
                    {
                        if (!string.IsNullOrEmpty(S) && !S.StartsWithIgnoreCase("*"))
                        {
                            //Addons
                            double P = (double)intC / intT;
                            if (!string.IsNullOrEmpty(S) && !S.StartsWithIgnoreCase("*") && !S.StartsWithIgnoreCase("#")) { cMain.UpdateToolStripLabel(lblStatus, "Loading Preset (" + string.Format("{0:0.0%}", P) + " " + intC + "\\" + intT + ") - " + S + "..."); }
                            Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt32(intC), Convert.ToUInt32(intT));
                            Application.DoEvents();
                            if (!S.StartsWithIgnoreCase("#"))
                            {
                                if (currentSection == 1)
                                {
                                    if (!string.IsNullOrEmpty(Filter) && Filter.ContainsIgnoreCase("Addons")) { TP = TabAddons; }
                                    if (lstAddons.FindItemWithText(S) == null && File.Exists(S) && S.ToUpper().EndsWithIgnoreCase(".WA"))
                                    {
                                        if (TP == TabAddons || TP == null)
                                        {
                                            try
                                            {
                                                Addon addon = new Addon(S, cMain.selectedImages[0]);
                                                if (addon.Loaded)
                                                {
                                                    ListViewItem NI = new ListViewItem();
                                                    NI.Text = addon.Name;
                                                    NI.SubItems.Add(addon.Creator);
                                                    NI.SubItems.Add(addon.Version);
                                                    NI.SubItems.Add(addon.Size);
                                                    NI.SubItems.Add(addon.Architecture.ToString());
                                                    NI.SubItems.Add(S);
                                                    NI.SubItems.Add(addon.Description);
                                                    NI.SubItems.Add(addon.Website);
                                                    NI.Tag = addon;

                                                    NI.Group = lstAddons.Groups[0];
                                                    lstAddons.Items.Add(NI);
                                                    lPreset += S + Environment.NewLine;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                if (lstOptions.FindItemWithText("Debug Addons").Checked)
                                                {
                                                    MessageBox.Show(ex.Message, "Debug Mode");
                                                }
                                            }
                                        }
                                    }

                                }

                                //Components
                                if (currentSection == 2)
                                {
                                    if (!string.IsNullOrEmpty(Filter) && Filter.ContainsIgnoreCase("Components")) { TP = tabComponents; }
                                    if (TP == tabComponents || TP == null)
                                    {
                                        if (lstComponents.FindItemWithText(S) != null)
                                        {
                                            lstComponents.FindItemWithText(S).Checked = true;
                                            lPreset += S + Environment.NewLine;
                                        }
                                    }
                                }

                                //Drivers
                                if (currentSection == 3)
                                {
                                    if (!string.IsNullOrEmpty(Filter) && Filter.ContainsIgnoreCase("Drivers")) { TP = TabDrivers; }
                                    if (TP == TabDrivers || TP == null)
                                    {
                                        if (lstDrivers.FindItemWithText(S) == null && File.Exists(S) && S.ToUpper().EndsWithIgnoreCase(".INF"))
                                        {
                                            AddDriver(S);
                                            lPreset += S + Environment.NewLine;
                                        }
                                    }

                                }

                                //Gadgets
                                if (currentSection == 4)
                                {
                                    if (!string.IsNullOrEmpty(Filter) && Filter.ContainsIgnoreCase("Gadgets")) { TP = TabGadgets; }
                                    if (lstGadgets.FindItemWithText(S) == null && File.Exists(S) && S.ToUpper().EndsWithIgnoreCase(".GADGET"))
                                    {
                                        if (TP == TabGadgets || TP == null)
                                        {
                                            var NI = new ListViewItem();
                                            NI.Group = lstGadgets.Groups[0];
                                            string TPN = S;
                                            while (TPN.ContainsIgnoreCase("\\"))
                                            {
                                                TPN = TPN.Substring(1);
                                            }
                                            NI.Text = TPN;
                                            NI.SubItems.Add(cMain.GetSize(S));
                                            NI.SubItems.Add(S);
                                            lstGadgets.Items.Add(NI);
                                            lPreset += S + Environment.NewLine;
                                        }
                                    }

                                }

                                //Services
                                if (currentSection == 5)
                                {
                                    if (!string.IsNullOrEmpty(Filter) && Filter.ContainsIgnoreCase("Services")) { TP = tabServices; }
                                    if (TP == tabServices || TP == null)
                                    {
                                        string SN = S;
                                        string SI = S;

                                        while (SN.ContainsIgnoreCase("::"))
                                        {
                                            SN = SN.Substring(0, SN.Length - 1);
                                        }
                                        SN = SN.Substring(0, SN.Length - 1);

                                        while (SI.ContainsIgnoreCase("::"))
                                        {
                                            SI = SI.Substring(1);
                                        }

                                        SI = SI.Substring(1);
                                        if (lstServices.FindItemWithText(SN) != null)
                                        {
                                            lstServices.FindItemWithText(SN).Checked = true;
                                            lstServices.FindItemWithText(SN).SubItems[1].Text = SI;
                                            lPreset += S + Environment.NewLine;
                                        }
                                    }
                                }

                                //Silent 6

                                if (currentSection == 6)
                                {
                                    try
                                    {
                                        if (!string.IsNullOrEmpty(Filter) && Filter.ContainsIgnoreCase("Silent Installs")) { TP = tabSilent; }
                                        if (TP == tabSilent || TP == null)
                                        {
                                            string SN = S;
                                            string SI = S;
                                            i += 1;

                                            while (SN.ContainsIgnoreCase("::"))
                                            {
                                                SN = SN.Substring(0, SN.Length - 1);
                                            }
                                            SN = SN.Substring(0, SN.Length - 1);

                                            while (SI.ContainsIgnoreCase("::"))
                                            {
                                                SI = SI.Substring(1);
                                            }

                                            SI = SI.Substring(1);

                                            string F = S;

                                            while (!F.EndsWithIgnoreCase(";"))
                                            {
                                                F = F.Substring(0, F.Length - 1);
                                            }
                                            F = F.Substring(0, F.Length - 1);
                                            while (F.ContainsIgnoreCase(";"))
                                            {
                                                F = F.Substring(1);
                                            }

                                            if (lstSilent.Items.Count > 0 && lstSilent.FindItemWithText(SN, false, 0) != null) { SN += "_" + i.ToString(CultureInfo.InvariantCulture); }
                                            if (lstSilent.FindItemWithText(SN) == null && File.Exists(F))
                                            {
                                                var NI = new ListViewItem();
                                                NI.Text = SN;

                                                foreach (string SUB in SI.Split(';'))
                                                {
                                                    if (SUB != "Always Installed" && SUB != "Prompt Install")
                                                    {
                                                        NI.SubItems.Add(SUB);
                                                    }
                                                }
                                                if (SI.EndsWithIgnoreCase("Always Installed"))
                                                {
                                                    NI.Group = lstSilent.Groups[0];
                                                }
                                                if (SI.EndsWithIgnoreCase("Prompt Install"))
                                                {
                                                    NI.Group = lstSilent.Groups[1];
                                                }
                                                if (string.IsNullOrEmpty(NI.Text))
                                                {
                                                    NI.Text = Path.GetFileNameWithoutExtension(F);
                                                }
                                                lstSilent.Items.Add(NI);
                                                lPreset += S + Environment.NewLine;
                                            }

                                        }
                                    }
                                    catch (Exception Ex)
                                    {
                                        MessageBox.Show(Ex.Message);
                                    }

                                }

                                //Wallpapers
                                if (currentSection == 7)
                                {
                                    if (!string.IsNullOrEmpty(Filter) && Filter.ContainsIgnoreCase("Wallpapers")) { TP = TabWallpapers; }
                                    if (TP == TabWallpapers || TP == null)
                                    {
                                        if (lstWallpapers.FindItemWithText(S) == null && File.Exists(S))
                                        {
                                            var NI = new ListViewItem();
                                            NI.Group = lstWallpapers.Groups[0];
                                            string TPN = S;
                                            while (TPN.ContainsIgnoreCase("\\"))
                                            {
                                                TPN = TPN.Substring(1);
                                            }
                                            NI.Text = TPN;
                                            NI.SubItems.Add(cMain.GetSize(S));
                                            NI.SubItems.Add(S);
                                            lstWallpapers.Items.Add(NI);
                                            lPreset += S + Environment.NewLine;
                                        }
                                    }

                                }

                                //vLite
                                if (currentSection == 13)
                                {
                                    if (!string.IsNullOrEmpty(Filter) && Filter.ContainsIgnoreCase("vLite")) { TP = tabComponents2; }
                                    if (TP == tabComponents2 || TP == null)
                                    {
                                        FindNode(tvComponents, S).Checked = true;
                                        if (FindNode(tvComponents, S).Parent != null)
                                        {
                                            FindNode(tvComponents, S).Parent.Checked = true;
                                            if (FindNode(tvComponents, S).Parent.Parent != null) { FindNode(tvComponents, S).Parent.Parent.Checked = true; }
                                        }
                                    }
                                }

                                //Tweaks
                                if (currentSection == 8)
                                {
                                    if (!string.IsNullOrEmpty(Filter) && Filter.ContainsIgnoreCase("Tweaks")) { TP = TabTweaks; }
                                    if (TP == TabTweaks || TP == null)
                                    {
                                        if (S.ContainsIgnoreCase("::"))
                                        {
                                            string SN = S;
                                            string SI = S;

                                            while (SN.ContainsIgnoreCase("::"))
                                            {
                                                SN = SN.Substring(0, SN.Length - 1);
                                            }
                                            SN = SN.Substring(0, SN.Length - 1);

                                            while (SI.ContainsIgnoreCase("::"))
                                            {
                                                SI = SI.Substring(1);
                                            }

                                            SI = SI.Substring(1);
                                            if (SI.ContainsIgnoreCase("\\") && !File.Exists(SI))
                                            {
                                                SI = "";
                                            }

                                            //Add Tweaks (Preset)
                                            if (SN.ToUpper().EndsWithIgnoreCase(".REG"))
                                            {
                                                if (File.Exists(SI))
                                                {
                                                    var NI = new TreeNode();
                                                    NI.Text = SN;
                                                    NI.ToolTipText = SI;
                                                    NI.Tag = "Reg";
                                                    NI.Checked = true;
                                                    tvTweaks.Nodes.Add(NI);
                                                    lPreset += S + Environment.NewLine;
                                                }
                                            }

                                            if (string.IsNullOrEmpty(SI))
                                            {
                                                FindNode(tvTweaks, SN).Checked = false;
                                            }
                                            else
                                            {
                                                FindNode(tvTweaks, SN).Checked = true;
                                                lPreset += S + Environment.NewLine;

                                                FindNode(tvTweaks, SN).Parent.Checked = true;
                                                try
                                                {
                                                    FindNode(tvTweaks, SN).Nodes[0].Text = SI;
                                                    FindNode(tvTweaks, SN).Nodes[0].Tag = "C";
                                                }
                                                catch
                                                {
                                                    FindNode(tvTweaks, SN).Nodes.Add(SI);
                                                    FindNode(tvTweaks, SN).Nodes[0].Tag = "C";
                                                }
                                                HideCheckBox(tvTweaks, FindNode(tvTweaks, SN).Nodes[0]);
                                            }
                                        }
                                        else
                                        {
                                            FindNode(tvTweaks, S).Parent.Checked = true;
                                            FindNode(tvTweaks, S).Checked = true;
                                            lPreset += S + Environment.NewLine;
                                        }

                                    }
                                }

                                //Updates
                                if (currentSection == 9)
                                {
                                    if (!string.IsNullOrEmpty(Filter) && Filter.ContainsIgnoreCase("Updates") && (S.ToUpper().EndsWithIgnoreCase(".MSU") || S.ToUpper().EndsWithIgnoreCase(".CAB") || S.ToUpper().EndsWithIgnoreCase(".EXE"))) { TP = TabUpdates; }

                                    if ((TP == TabUpdates || TP == null) && File.Exists(S))
                                    {
                                        if (lstUpdates.FindItemWithText(S) == null)
                                        {
                                            string MD5 = "";
                                            var item = Classes.UpdateCache.Find(Path.GetFileName(S), cMain.GetSize(S, false));
                                            if (item != null)
                                            {
                                                MD5 = item.MD5;
                                            }
                                            else
                                            {
                                                MD5 = cMain.MD5CalcFile(S);
                                            }

                                            if ((lstUpdates.FindItemWithText(MD5) == null || string.IsNullOrEmpty(MD5)) && lstUpdates.FindItemWithText(S) == null)
                                            {


                                                UC = 0;
                                                if (S.ToUpper().EndsWithIgnoreCase(".CAB"))
                                                    AddToList(S, "update.mum", MD5);
                                                if (S.ToUpper().EndsWithIgnoreCase(".MSU"))
                                                    AddToList(S, "*.txt", MD5);
                                                if (S.ToUpper().EndsWithIgnoreCase(".EXE"))
                                                    AddEXE(S);
                                                lPreset += S + Environment.NewLine;
                                            }
                                        }
                                    }

                                }

                                //Files
                                if (currentSection == 11)
                                {
                                    if (!string.IsNullOrEmpty(Filter) && Filter.ContainsIgnoreCase("Files")) { TP = tabFiles; }
                                    if (TP == tabFiles || TP == null)
                                    {
                                        string TPN = S;
                                        while (TPN.ContainsIgnoreCase(":"))
                                        {
                                            TPN = TPN.Substring(0, TPN.Length - 1);
                                        }

                                        var NI = new ListViewItem();
                                        NI.Group = lstFiles.Groups[0];
                                        NI.Text = TPN;
                                        string R = "False";
                                        string L = S;
                                        string ST = S;

                                        while (L.ContainsIgnoreCase("::"))
                                            L = L.Substring(1);

                                        L = L.Substring(1);
                                        while (L.ContainsIgnoreCase(";"))
                                            L = L.Substring(0, L.Length - 1);

                                        while (!ST.EndsWithIgnoreCase(";"))
                                            ST = ST.Substring(0, ST.Length - 1);

                                        ST = ST.Substring(0, ST.Length - 1);
                                        while (ST.ContainsIgnoreCase(";"))
                                            ST = ST.Substring(1);

                                        if (S.EndsWithIgnoreCase(";True"))
                                        {
                                            R = "True";
                                        }

                                        NI.SubItems.Add(cMain.GetSize(L, true));
                                        NI.SubItems.Add(R);
                                        NI.SubItems.Add(L);
                                        NI.SubItems.Add(ST);
                                        if (File.Exists(L))
                                        {
                                            lstFiles.Items.Add(NI);
                                            lPreset += S + Environment.NewLine;
                                        }
                                    }

                                }

                                //Themes

                                if (currentSection == 12)
                                {
                                    if (!string.IsNullOrEmpty(Filter) && Filter.ContainsIgnoreCase("Theme Packs")) { TP = TabThemes; }
                                    if (TP == TabThemes || TP == null)
                                    {
                                        string F = S;
                                        string TPN = S;

                                        if (F.StartsWithIgnoreCase("|")) { F = F.Substring(1); }

                                        if (lstThemes.FindItemWithText(F) == null && File.Exists(F))
                                        {
                                            var NI = new ListViewItem();
                                            NI.Group = lstThemes.Groups[0];
                                            while (TPN.ContainsIgnoreCase("\\"))
                                            {
                                                TPN = TPN.Substring(1);
                                            }
                                            if (S.StartsWithIgnoreCase("|")) { NI.Font = new Font(NI.Font, FontStyle.Bold); }
                                            NI.Text = TPN;
                                            NI.SubItems.Add(cMain.GetSize(F, true));
                                            NI.SubItems.Add(F);
                                            NI.SubItems.Add("");
                                            lstThemes.Items.Add(NI);
                                            lPreset += S + Environment.NewLine;
                                        }
                                    }

                                }

                                //Options
                                if (currentSection == 10)
                                {
                                    continue;
                                }
                            }

                            if (!string.IsNullOrEmpty(Filter))
                            {
                                if (Filter.Split('#').Any(f => S.StartsWithIgnoreCase("#" + f) && !string.IsNullOrEmpty(f)))
                                {
                                    lPreset += S + Environment.NewLine;
                                }
                            }
                            if (S.StartsWithIgnoreCase("#"))
                            {
                                currentSection = 0;

                            }
                            if (S.StartsWithIgnoreCase("#Addons"))
                            {
                                currentSection = 1;
                            }
                            if (S.StartsWithIgnoreCase("#Components"))
                            {
                                currentSection = 2;
                            }
                            if (S.StartsWithIgnoreCase("#Drivers"))
                            {
                                currentSection = 3;
                            }
                            if (S.StartsWithIgnoreCase("#Gadgets"))
                            {
                                currentSection = 4;
                            }
                            if (S.StartsWithIgnoreCase("#Services"))
                            {
                                currentSection = 5;
                            }
                            if (S.StartsWithIgnoreCase("#Silent Installs"))
                            {
                                currentSection = 6;
                            }
                            if (S.StartsWithIgnoreCase("#Wallpapers"))
                            {
                                currentSection = 7;
                            }
                            if (S.StartsWithIgnoreCase("#Tweaks"))
                            {
                                currentSection = 8;
                            }
                            if (S.StartsWithIgnoreCase("#Updates"))
                            {
                                currentSection = 9;
                            }
                            if (S.StartsWithIgnoreCase("#Options"))
                            {
                                currentSection = 10;
                            }
                            if (S.StartsWithIgnoreCase("#Files"))
                            {
                                currentSection = 11;
                            }
                            if (S.StartsWithIgnoreCase("#Theme Packs"))
                            {
                                currentSection = 12;
                            }
                            if (S.StartsWithIgnoreCase("#vLite"))
                            {
                                currentSection = 13;
                            }

                            if (!string.IsNullOrEmpty(S) && !S.StartsWithIgnoreCase("*") && !S.StartsWithIgnoreCase("#")) { intC += 1; }
                        }
                    }
                    catch
                    {
                    }
                }
                lstOptions.FindItemWithText("Delete Silent Installers", false, 0).Checked = false;
                UpdateNames(true);

                if (lstDrivers.Items.Count > 0)
                {
                    InvalDrivers();
                }

                cMain.UpdateToolStripLabel(lblStatus, "Loading Options");
                if (!string.IsNullOrEmpty(Filter) && !Filter.ContainsIgnoreCase("Options"))
                {
                    foreach (ListViewItem LST in lstOptions.Items)
                    {
                        try
                        {
                            LST.Checked = Convert.ToBoolean(LST.SubItems[2].Text);
                        }
                        catch { }
                    }
                }

                cMain.UpdateToolStripLabel(lblStatus, "Previous settings loaded successfully!");
            }
            catch (Exception Ex)
            {
                Text = "All-In-One: Error loading settings!";
                Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.Error);
                MessageBox.Show("Error trying to load settings" + Environment.NewLine + Ex.Message);
                cMain.WriteLog(this, "Error trying to load settings", Ex.Message, Text);
            }

            lstAddons.EndUpdate();
            tvComponents.EndUpdate();
            lstDrivers.EndUpdate();
            lstFiles.EndUpdate();
            lstGadgets.EndUpdate();
            lstOptions.EndUpdate();
            lstServices.EndUpdate();
            lstSilent.EndUpdate();
            lstThemes.EndUpdate();
            tvTweaks.EndUpdate();
            lstUpdates.EndUpdate();
            lstWallpapers.EndUpdate();

            Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
            Text = OrigText;
            this.Enabled = true;
            TPMain.Visible = true;
            lAIO = false;

            TabPage cTAB = TPMain.SelectedTab;
            TPMain.SelectedTab = TabOptions;
            TPMain.SelectedTab = cTAB;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var OFD = new OpenFileDialog();
            OFD.Title = "Load Settings";
            OFD.Filter = "AIO Settings *.ini|*.ini";

            if (OFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            LoadPreset(OFD.FileName, null);
            EnablePan();
        }

        private void cmdSICancel_Click(object sender, EventArgs e)
        {
            PanSilent.Visible = false;
            cmdStart.Enabled = true;
        }

        private void cmdSIOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSIFilename.Text))
            {
                MessageBox.Show("Please select a file to install.", "Invalid Filename");
                return;
            }

            if (!File.Exists(txtSIFilename.Text))
            {
                MessageBox.Show("The file you have selected does not exist!", "Invalid File");
                return;
            }
            if (string.IsNullOrEmpty(txtSIName.Text))
            {
                MessageBox.Show("Please enter a custom name for the file.", "Invalid Name");
                return;
            }

            if (lstSilent.Items.Count > 0)
            {
                if (lstSilent.Items.Cast<ListViewItem>().Where(n => n.Text.ToUpper() == txtSIName.Text.ToUpper()).Any())
                {
                    MessageBox.Show("This name already exists, please type another name.", "Name Already Taken");
                    return;
                }

            }

            iAdding = true;
            var NI = new ListViewItem { Text = txtSIName.Text };

            NI.SubItems.Add(txtSISwitch.Text);
            NI.Group = lstSilent.Groups[0];
            NI.Group = chkAS.Checked ? lstSilent.Groups[0] : lstSilent.Groups[1];
            if (chkCopyF.Checked)
            {
                NI.SubItems.Add("YES");
                string GS = txtSIFilename.Text;
                while (!GS.EndsWithIgnoreCase("\\"))
                {
                    GS = GS.Substring(0, GS.Length - 1);
                }
                NI.SubItems.Add(cMain.GetSize(GS));
            }
            else
            {
                NI.SubItems.Add("NO");
                NI.SubItems.Add(cMain.GetSize(txtSIFilename.Text));
            }

            NI.SubItems.Add(txtSIFilename.Text);
            lstSilent.Items.Add(NI);
            mnuR.Visible = true;
            UpdateNames(true);
            PanSilent.Visible = false;
            iAdding = false;
        }

        private void cmdBrowseD_Click(object sender, EventArgs e)
        {
            var OFD = new OpenFileDialog();
            OFD.Title = "Select Installer";
            OFD.Filter = "Application *.exe,*.msi,*.msu,*.inf,*.msp,*.bat|*.exe;*.msi;*.msu;*.inf;*.msp;*.bat";
            OFD.Multiselect = true;
            if (Directory.Exists(cOptions.fSilents))
            {
                OFD.InitialDirectory = cOptions.fSilents;
            }

            if (OFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            chkCopyF.Enabled = true;
            txtSISwitch.Enabled = true;
            if (OFD.FileName.EndsWithIgnoreCase(".MSU"))
            {
                chkCopyF.Enabled = false;
                txtSISwitch.Enabled = false;
                txtSISwitch.Text = "N/A (Not Needed)";
            }

            if (OFD.FileName.EndsWithIgnoreCase(".BAT"))
            {
                txtSISwitch.Enabled = false;
                txtSISwitch.Text = "N/A (Not Needed)";
            }

            if (OFD.FileName.EndsWithIgnoreCase(".INF"))
            {
                txtSISwitch.Text = "advpack.dll,LaunchINFSection %1,defaultinstall,3";
            }

            if (OFD.FileName.EndsWithIgnoreCase(".MSI") || OFD.FileName.EndsWithIgnoreCase(".MSP"))
            {
                txtSISwitch.Text = "/passive /norestart";
            }

            cOptions.fSilents = GetFolder(OFD.FileName);
            txtSIName.ReadOnly = false;
            txtSIName.BackColor = Color.White;
            int t = OFD.FileNames.Count();

            if (t > 1)
            {
                iAdding = true;
                txtSIName.ReadOnly = true;
                txtSIName.BackColor = SystemColors.Control;
                txtSIName.Text = "";
                lstSilent.BeginUpdate();
                int i = 0;
                foreach (string S in OFD.FileNames)
                {
                    i += 1;

                    try
                    {
                        var LS = new ListViewItem();
                        string newName = Path.GetFileNameWithoutExtension(S);

                        if (lstSilent.Items.Count > 0)
                        {
                            string searchName = newName;
                            int x = 1;
                            while (lstSilent.FindItemWithText(searchName, false, 0) != null)
                            {
                                searchName = newName + "_" + x.ToString(CultureInfo.InvariantCulture);
                                x++;
                            }
                            newName = searchName;
                        }
                        cMain.UpdateToolStripLabel(lblStatus, "Adding Silent " + i + " of " + t + ": [" + newName + "]");
                        Application.DoEvents();

                        LS.Text = newName.Trim();

                        LS.SubItems.Add("");
                        LS.SubItems.Add("NO");
                        LS.SubItems.Add(cMain.GetSize(S));
                        LS.SubItems.Add(S);
                        LS.Group = chkAS.Checked ? lstSilent.Groups[0] : lstSilent.Groups[1];
                        lstSilent.Items.Add(LS);
                    }
                    catch
                    {
                    }
                }
                mnuR.Visible = true;
                UpdateNames(true);
                lstSilent.EndUpdate();
                cmdSICancel.PerformClick();
                cMain.UpdateToolStripLabel(lblStatus, "");
                iAdding = false;
            }
            else
            {
                txtSIFilename.Text = OFD.FileName;
                if (!string.IsNullOrEmpty(OFD.SafeFileName))
                {
                    txtSIName.Text = OFD.SafeFileName.Substring(0, OFD.SafeFileName.Length - 4);
                }
            }
        }

        private void chkCopyF_MouseClick(object sender, MouseEventArgs e)
        {
            if (chkCopyF.Checked)
            {
                DialogResult dResult =
                      MessageBox.Show(
                             "WARNING: Only have this box ticked if you are adding programs such as Visual Studio or Microsoft Office. These programs have other files and folders which are needed to be installed and clicking yes will also copy these files." + Environment.NewLine + Environment.NewLine + "DO NOT click yes if your program is small enough to be in one exe such as chrome_installer.exe, 7zip.msi, WinRAR.exe, etc.." + Environment.NewLine + Environment.NewLine + "If used incorrectly, your ISO will be huge! Are you sure?", "WARNING",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dResult != DialogResult.Yes)
                {
                    chkCopyF.Checked = false;
                }
            }
        }

        private void cmdSIClear_Click(object sender, EventArgs e)
        {
            txtSIFilename.Text = "";
            txtSIName.Text = "";
            txtSISwitch.Text = "";
            txtSIFilename.Enabled = true;
            txtSIName.Enabled = true;
            txtSISwitch.Enabled = true;
            chkCopyF.Enabled = true;
            chkAS.Enabled = true;
            chkAS.Checked = true;
            chkCopyF.Checked = false;
            txtSIName.ReadOnly = false;
            txtSIName.BackColor = Color.White;
        }

        private void lstComponents_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lAIO || iBusy)
                return;

            UpdateNames(false);

            if (e.Item.Checked && lAIO == false)
            {
                string Dependants = "";
                switch (e.Item.Text)
                {
                    case "Internet Information Services":
                        Dependants = "*IIS Addons 1\n*IIS Addons 2" + Environment.NewLine +
                                              "*Microsoft .NET Framework 3.5.1";
                        break;
                    case "Media Features":
                        Dependants = "*Windows Media Center\n*Windows Media Player\n*Windows Media Player DVD Registration\n*Windows Media Player Network Sharing Service";
                        break;
                    case "Client Drivers":
                        Dependants = "*Common Drivers";
                        break;
                    case "Inbox Games":
                        Dependants = "*Internet Games\n*Premium Inbox Games";
                        break;
                    case "Music and Video Examples":
                        Dependants = "Windows Experience Index";
                        break;
                }
                if (!string.IsNullOrEmpty(Dependants))
                {
                    MessageBox.Show("Removing these will also remove or affect the following components:" + Environment.NewLine + Environment.NewLine + Dependants, "Warning!");
                }
            }
        }

        private void SaveLog(string SaveTo, bool AlwaysSave, TabPage TP = null)
        {
            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Preparing List...");
                cMain.sList = "";

                if (lstOptions.CheckedItems.Count > 0)
                {
                    if (TP == TabOptions || TP == null)
                    {
                        cMain.sList += "#Options" + Environment.NewLine;
                        foreach (ListViewItem LST in lstOptions.CheckedItems)
                        {
                            try
                            {
                                if (string.IsNullOrEmpty(LST.SubItems[1].Text))
                                {
                                    cMain.sList += LST.Text + Environment.NewLine;
                                }
                                else
                                {
                                    cMain.sList += LST.Text + "::" + LST.SubItems[1].Text + Environment.NewLine;
                                }
                            }
                            catch
                            {
                                cMain.sList += LST.Text + Environment.NewLine;
                            }
                        }
                    }
                }

                if (lstAddons.Items.Count > 0)
                {
                    if (TP == TabAddons || TP == null)
                    {
                        cMain.sList += "#Addons" + Environment.NewLine;
                        foreach (ListViewItem LST in lstAddons.Items)
                        {
                            try
                            {
                                cMain.sList += LST.SubItems[5].Text + Environment.NewLine;
                            }
                            catch { cMain.sList += "Error_" + cMain.GetSubItems(LST) + Environment.NewLine; }
                        }

                    }
                }

                if (lstComponents.CheckedItems.Count > 0)
                {
                    if (TP == tabComponents || TP == null)
                    {
                        cMain.sList += "#Components" + Environment.NewLine;
                        foreach (ListViewItem LST in lstComponents.CheckedItems)
                        {
                            try
                            {
                                cMain.sList += LST.Text + Environment.NewLine;
                            }
                            catch { cMain.sList += "Error_" + cMain.GetSubItems(LST) + Environment.NewLine; }
                        }
                    }
                }

                if (lstDrivers.Items.Count > 0)
                {
                    if (TP == TabDrivers || TP == null)
                    {
                        cMain.sList += "#Drivers" + Environment.NewLine;
                        foreach (ListViewItem LST in lstDrivers.Items)
                        {
                            try
                            {
                                cMain.sList += LST.SubItems[6].Text + Environment.NewLine;
                            }
                            catch { cMain.sList += "Error_" + cMain.GetSubItems(LST) + Environment.NewLine; }
                        }
                    }
                }

                if (lstFiles.Items.Count > 0)
                {
                    if (TP == tabFiles || TP == null)
                    {
                        cMain.sList += "#Files" + Environment.NewLine;
                        foreach (ListViewItem LST in lstFiles.Items)
                        {
                            try
                            {
                                cMain.sList += LST.Text + "::" + LST.SubItems[3].Text + ";" + LST.SubItems[4].Text + ";" + LST.SubItems[2].Text + Environment.NewLine;
                            }
                            catch { cMain.sList += "Error_" + cMain.GetSubItems(LST) + Environment.NewLine; }
                        }
                    }
                }

                if (lstGadgets.Items.Count > 0)
                {
                    if (TP == TabGadgets || TP == null)
                    {
                        cMain.sList += "#Gadgets" + Environment.NewLine;
                        foreach (ListViewItem LST in lstGadgets.Items)
                        {
                            try
                            {
                                cMain.sList += LST.SubItems[2].Text + Environment.NewLine;
                            }
                            catch { cMain.sList += "Error_" + cMain.GetSubItems(LST) + Environment.NewLine; }
                        }
                    }
                }

                if (lstServices.CheckedItems.Count > 0)
                {
                    if (TP == tabServices || TP == null)
                    {
                        cMain.sList += "#Services" + Environment.NewLine;
                        foreach (ListViewItem LST in lstServices.CheckedItems)
                        {
                            try
                            {
                                cMain.sList += LST.Text + "::" + LST.SubItems[1].Text + Environment.NewLine;
                            }
                            catch { cMain.sList += "Error_" + cMain.GetSubItems(LST) + Environment.NewLine; }
                        }
                    }
                }

                if (lstSilent.Items.Count > 0)
                {
                    if (TP == tabSilent || TP == null)
                    {
                        cMain.sList += "#Silent Installs" + Environment.NewLine;
                        foreach (ListViewItem LST in lstSilent.Items)
                        {
                            try
                            {
                                cMain.sList += LST.Text + "::";
                                cMain.sList += LST.SubItems[1].Text + ";";
                                cMain.sList += LST.SubItems[2].Text + ";";
                                cMain.sList += LST.SubItems[3].Text + ";";
                                cMain.sList += LST.SubItems[4].Text + ";";
                                if (LST.Group.Header == null) { cMain.sList += "Prompt Install" + Environment.NewLine; }
                                else { cMain.sList += LST.Group.Header + Environment.NewLine; }
                            }
                            catch { cMain.sList += "Error_" + cMain.GetSubItems(LST) + Environment.NewLine; }
                        }
                    }
                }

                if (lstThemes.Items.Count > 0)
                {
                    if (TP == TabThemes || TP == null)
                    {
                        cMain.sList += "#Theme Packs" + Environment.NewLine;
                        foreach (ListViewItem LST in lstThemes.Items)
                        {
                            try
                            {
                                if (LST.Font.Bold)
                                {
                                    cMain.sList += "|" + LST.SubItems[2].Text + Environment.NewLine;
                                }
                                else
                                {
                                    cMain.sList += LST.SubItems[2].Text + Environment.NewLine;
                                }
                            }
                            catch { cMain.sList += "Error_" + cMain.GetSubItems(LST) + Environment.NewLine; }
                        }
                    }
                }

                if (CountNodes(tvTweaks) > 0)
                {
                    if (TP == TabTweaks || TP == null)
                    {
                        ListNodes(tvTweaks);
                        cMain.sList += "#Tweaks" + Environment.NewLine;
                        foreach (TreeNode LST in tvList)
                        {
                            string S = "";
                            if (LST.Checked)
                            {
                                try
                                {
                                    if (LST.Nodes.Count > 0 && LST.Nodes[0].Tag != null && LST.Nodes[0].Tag.ToString().EqualsIgnoreCase("C")) { S = LST.Nodes[0].Text; }
                                    if (LST.Tag != null && LST.Tag.ToString().EqualsIgnoreCase("Reg")) { S = LST.ToolTipText; }
                                }
                                catch { }

                                if (!string.IsNullOrEmpty(S))
                                {
                                    cMain.sList += LST.Text + "::" + S + Environment.NewLine;
                                }
                                else
                                {
                                    cMain.sList += LST.Text + Environment.NewLine;
                                }
                            }
                        }

                    }
                }

                if (CountNodes(tvComponents) > 0)
                {
                    if (TP == tabComponents2 || TP == null)
                    {
                        ListNodes(tvComponents);
                        cMain.sList += "#vLite" + Environment.NewLine;
                        foreach (TreeNode LST in tvList)
                        {
                            if (LST.Checked) { cMain.sList += LST.Text + Environment.NewLine; }
                        }

                    }
                }

                if (lstUpdates.Items.Count > 0)
                {
                    if (TP == TabUpdates || TP == null)
                    {
                        cMain.sList += "#Updates" + Environment.NewLine;
                        foreach (ListViewItem LST in lstUpdates.Items)
                        {
                            try
                            {
                                if (LST.SubItems.Count == 10 && !string.IsNullOrEmpty(LST.SubItems[9].Text))
                                {
                                    cMain.sList += LST.SubItems[9].Text + Environment.NewLine;
                                }
                                else
                                {
                                    cMain.sList += LST.SubItems[5].Text + Environment.NewLine;
                                }
                            }
                            catch { cMain.sList += "Error_" + cMain.GetSubItems(LST) + Environment.NewLine; }
                        }
                    }
                }

                if (lstWallpapers.Items.Count > 0)
                {
                    if (TP == TabWallpapers || TP == null)
                    {
                        cMain.sList += "#Wallpapers" + Environment.NewLine;
                        foreach (ListViewItem LST in lstWallpapers.Items)
                        {
                            try
                            {
                                cMain.sList += LST.SubItems[2].Text + Environment.NewLine;
                            }
                            catch { cMain.sList += "Error_" + cMain.GetSubItems(LST) + Environment.NewLine; }
                        }
                    }
                }


                if (string.IsNullOrEmpty(cMain.sList))
                {
                    cMain.UpdateToolStripLabel(lblStatus, "No settings to save...");
                    MessageBox.Show("There are no settings to save!", "Invalid selection");
                }
                else
                {
                    string PM = "*AIO|v" + cMain.WinToolkitVersion(true) + "|" + runTime + Environment.NewLine + cMain.sList;

                    try
                    {
                        if (!string.IsNullOrEmpty(SaveTo))
                        {
                            if (AlwaysSave)
                            {
                                Files.DeleteFile(SaveTo);
                                using (var SW = new StreamWriter(SaveTo, true))
                                {
                                    SW.Write(PM);
                                }
                                cMain.UpdateToolStripLabel(lblStatus, "Preset saved successfully.");
                            }
                            else
                            {
                                if (PM != cLoad)
                                {
                                    Files.DeleteFile(SaveTo);
                                    using (var SW = new StreamWriter(SaveTo, true))
                                    {
                                        SW.Write(PM);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message, SaveTo);
                    }
                }
            }
            catch (Exception Ex)
            {
                cMain.UpdateToolStripLabel(lblStatus, "Error saving preset.");
                cMain.WriteLog(this, "Unable to save preset, loaded so far: " + Environment.NewLine + cMain.sList, Ex.Message, lblStatus.Text);
                cMain.ErrorBox("An error occurred whilst trying to save your preset.", "Saving Preset: Unknown Error", "Exception: " + Ex.Message + Environment.NewLine + "Loaded so far: " + Environment.NewLine + cMain.sList);
            }
        }

        private void AddEXE(string F)
        {
            cMain.UpdateToolStripLabel(lblStatus, "Adding: [" + F + "]...");
            Application.DoEvents();
            try
            {

                FileVersionInfo FI = FileVersionInfo.GetVersionInfo(F);
                var FS = new FileInfo(F);
                long OSize = FS.Length;

                if ((FI.FileVersion.EqualsIgnoreCase("6.2.0029.0 (SRV03_QFE.031113-0918)") || FI.FileVersion.StartsWithIgnoreCase("6.3.0015.0")) && FI.FileVersion.ContainsIgnoreCase("SRV") && OSize > 314572800)
                {
                    var NI = new ListViewItem();

                    NI.Text = "Windows XP Mode";
                    NI.Group = lstUpdates.Groups[1];
                    NI.SubItems.Add(FI.FileVersion);
                    NI.SubItems.Add("neutral");
                    NI.SubItems.Add(cMain.GetSize(F, true));
                    NI.ToolTipText = "Windows XP mode lets you run older Windows XP business software on your Windows 7 desktop.";
                    NI.SubItems.Add(cMain.selectedImages[0].Architecture.ToString());
                    NI.SubItems.Add(F);
                    NI.SubItems.Add(cMain.MD5CalcFile(F));
                    NI.SubItems.Add("http://www.microsoft.com/windows/virtual-pc/download.aspx");
                    string sCreated = "N/A";
                    try
                    {
                        DateTime DT = new FileInfo(F).CreationTime;
                        sCreated = DT.ToString("yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch { }
                    NI.SubItems.Add(sCreated);
                    NI.SubItems.Add("");

                    UpdateInfo updateInfo = new UpdateInfo();
                    updateInfo.PackageName = NI.Text;
                    updateInfo.PackageVersion = FI.FileVersion;
                    NI.Tag = updateInfo;

                    lstUpdates.Items.Add(NI);
                    return;
                }

                //MessageBox.Show(FI.FileDescription);

                string sFileDesc = FI.FileDescription;
                if (FI.FileVersion.ContainsIgnoreCase("win8_gdr_soc_ie") && FI.FileVersion.StartsWithIgnoreCase("10.")) { sFileDesc = "Windows Internet Explorer 10 Setup utility"; }
                if (FI.FileVersion.ContainsIgnoreCase("winblue_gdr") && FI.FileVersion.StartsWithIgnoreCase("11.")) { sFileDesc = "Windows Internet Explorer 11 Setup utility"; }

                if ((sFileDesc.StartsWithIgnoreCase("Windows Internet Explorer") || sFileDesc.StartsWithIgnoreCase("Internet Explorer")) && sFileDesc.EndsWithIgnoreCase("Setup utility"))
                {
                    string sFileVersion = FI.FileVersion;
                    string sVersionFull = sFileVersion;
                    while (sFileVersion.ContainsIgnoreCase(".")) { sFileVersion = sFileVersion.Substring(0, sFileVersion.Length - 1); }

                    ListViewItem NI = new ListViewItem();
                    bool bInternetExplorer = false;
                    string uSupport = "http://search.microsoft.com/en-us/SupportResults.aspx?q=Internet%20Explorer%20" + sFileVersion;
                    NI.Text = "Internet Explorer " + sFileVersion;

                    if (Environment.OSVersion.Version.Major < 6 && sFileVersion.EqualsIgnoreCase("11"))
                    {
                        DialogResult DR = MessageBox.Show("Sorry, Internet Explorer " + sFileVersion + " can't be integrated from Windows XP.\n\nWould you like to add it to the silent installers list instead?", "Invalid OS", MessageBoxButtons.YesNo);

                        if (DR != DialogResult.Yes) { return; }
                        if (lstSilent.Items.Count == 0 || lstSilent.FindItemWithText("Internet Explorer " + sFileVersion, false, 0) == null)
                        {
                            var LST2 = new ListViewItem();

                            LST2.Text = "Internet Explorer " + sFileVersion;
                            LST2.SubItems.Add("/passive /norestart /update-no");
                            LST2.SubItems.Add("NO");
                            LST2.SubItems.Add(cMain.BytesToString(OSize, true));
                            LST2.SubItems.Add(F);
                            LST2.Group = lstSilent.Groups[0];
                            lstSilent.Items.Add(LST2);
                            UpdateNames(false);
                        }
                        return;
                    }

                    string sNeededUpdates = "";
                    switch (sFileVersion)
                    {
                        case "9":
                            uSupport = "http://support.microsoft.com/kb/982861";
                            break;
                        case "10":
                            uSupport = "http://support.microsoft.com/kb/2718695";
                            if (lstUpdates.FindItemWithText("KB2670838") == null) { sNeededUpdates += "KB2670838\n"; }
                            if (lstUpdates.FindItemWithText("KB2756651") == null) { sNeededUpdates += "KB2756651\n"; }
                            if (lstUpdates.FindItemWithText("KB2786081") == null) { sNeededUpdates += "KB2786081\n"; }
                            if (lstUpdates.FindItemWithText("KB2878068") == null) { sNeededUpdates += "KB2878068\n"; }
                            if (lstUpdates.FindItemWithText("KB2896820") == null) { sNeededUpdates += "KB2896820\n"; }
                            break;
                        case "11":
                            uSupport = "http://support.microsoft.com/kb/2847882";
                            if (lstUpdates.FindItemWithText("KB2670838") == null) { sNeededUpdates += "KB2670838\n"; }
                            if (lstUpdates.FindItemWithText("KB2756651") == null) { sNeededUpdates += "KB2756651\n"; }
                            if (lstUpdates.FindItemWithText("KB2786081") == null) { sNeededUpdates += "KB2786081\n"; }
                            if (lstUpdates.FindItemWithText("KB2834140") == null) { sNeededUpdates += "KB2834140\n"; }
                            if (lstUpdates.FindItemWithText("KB2882822") == null) { sNeededUpdates += "KB2882822\n"; }
                            if (lstUpdates.FindItemWithText("KB2888049") == null) { sNeededUpdates += "KB2888049\n"; }
                            if (lstUpdates.FindItemWithText("KB2896820") == null) { sNeededUpdates += "KB2896820\n"; }
                            break;
                    }

                    NI.Group = lstUpdates.Groups[1];
                    string sLang = cMain.GetLPName(F);
                    if (string.IsNullOrEmpty(sLang)) { sLang = FI.Language; }
                    NI.SubItems.Add(sVersionFull);
                    NI.SubItems.Add(sLang);
                    NI.SubItems.Add(cMain.GetSize(F, true));
                    if (!string.IsNullOrEmpty(sNeededUpdates) && lAIO == false)
                    {
                        NI.ToolTipText = "Internet Explorer " + sFileVersion + " may require the following updates to be integrated first:\n\n" + sNeededUpdates;
                    }
                    string N = Path.GetFileName(F);

                    if (N.ContainsIgnoreCase("X64") || N.ContainsIgnoreCase("X86"))
                    {
                        NI.SubItems.Add(N.ContainsIgnoreCase("X64") ? "x64" : "x86");
                    }
                    else
                    {
                        NI.SubItems.Add("N/A");
                    }
                    NI.SubItems.Add(F);
                    NI.SubItems.Add(cMain.MD5CalcFile(F));
                    NI.SubItems.Add(uSupport);

                    string sCreated = "N/A";
                    try
                    {
                        DateTime DT = new FileInfo(F).CreationTime;
                        sCreated = DT.ToString("yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch { }
                    NI.SubItems.Add(sCreated);

                    NI.SubItems.Add("");
                    NI.Group = lstUpdates.Groups[3];

                    int iFileSize = 0;
                    int.TryParse(cMain.GetSize(F, false), out iFileSize);

                    if (iFileSize > 1048576)
                    {
                        bInternetExplorer = true;
                    }

                    if (NI.SubItems[4].Text != "N/A" && NI.SubItems[4].Text != cMain.selectedImages[0].Architecture.ToString())
                    {
                        bInternetExplorer = false;
                    }

                    if (bInternetExplorer == false)
                    {
                        if (NI.SubItems[4].Text != "N/A" && NI.SubItems[4].Text != cMain.selectedImages[0].Architecture.ToString())
                        {
                            MessageBox.Show("This version of IE does not seem to be for " + cMain.selectedImages[0].Architecture + ".", "Wrong Arc");

                        }
                        else
                        {
                            MessageBox.Show("This does not seem to be the FULL version of Internet Explorer.", "Wrong Version");
                        }
                    }
                    else
                    {


                        UpdateInfo updateInfo = new UpdateInfo();
                        updateInfo.PackageName = NI.Text;
                        updateInfo.PackageVersion = FI.FileVersion;
                        NI.Tag = updateInfo;
                        lstUpdates.Items.Add(NI);
                    }
                }
                else
                {
                    MessageBox.Show("This does not seem to be Internet Explorer 9/10/11 or the latest Windows XP Mode! Any other *.exe should be done via Silent Installers instead.\n\n[" + F + "]\n" + FI.FileDescription + Environment.NewLine + FI.FileVersion, "Invalid EXE");
                }
            }
            catch
            {
            }
        }

        private void AddDriver(string F)
        {
            try
            {

                ListViewItem N = new ListViewItem();

                string MD5 = cMain.MD5CalcFile(F);

                if (!string.IsNullOrEmpty(MD5) && lstOptions.FindItemWithText("Show Duplicates").Checked)
                {
                    if (lstDrivers.FindItemWithText(MD5) != null && N.BackColor != Color.LightPink)
                    {
                        N.BackColor = Color.LightBlue;
                    }
                }

                string sRead = "";
                using (var SR = new StreamReader(F, true))
                {
                    sRead = SR.ReadToEnd();
                }
                string sClass = "Unknown", sVersion = "N/A", sDate = "N/A", sArc = "??", cArc = "??";
                string WinDrv = "Unknown";
                if (cMain.selectedImages[0].Architecture == Architecture.x86)
                {
                    sArc = "NTX86";
                }
                if (cMain.selectedImages[0].Architecture == Architecture.x64)
                {
                    sArc = "NTAMD64";
                }

                if (sRead.ContainsIgnoreCase(".NT") || sRead.ContainsIgnoreCase(".NTX86") || sRead.ContainsIgnoreCase("NTAMD64") || sRead.ContainsIgnoreCase("$WINDOWS NT$")) { WinDrv = "XP or newer"; }
                if (sRead.ContainsIgnoreCase(".NTX86.5.1") || sRead.ContainsIgnoreCase("NTAMD64.5.1") || sRead.ContainsIgnoreCase("WINDOWS XP") || sRead.ContainsIgnoreCase("WINDOWSXP") || sRead.ContainsIgnoreCase("WINXP") || sRead.ContainsIgnoreCase(" XP") && !sRead.ContainsIgnoreCase(" or later")) { WinDrv += ", XP"; }
                if (sRead.ContainsIgnoreCase(".NTX86.5.2") || sRead.ContainsIgnoreCase("NTAMD64.5.2")) { WinDrv += ", 2003"; }
                if (sRead.ContainsIgnoreCase(".NTX86.6.0") || sRead.ContainsIgnoreCase("NTAMD64.6.0") || sRead.ContainsIgnoreCase("WINDOWS VISTA") || sRead.ContainsIgnoreCase("VISTA") || sRead.ContainsIgnoreCase("WINVISTA") || F.ContainsIgnoreCase("\\VISTA\\")) { WinDrv += ", Vista"; }

                if (sRead.ContainsIgnoreCase(".NTX86.6.1") || sRead.ContainsIgnoreCase("NTAMD64.6.1") || sRead.ContainsIgnoreCase("WINDOWS 7") || sRead.ContainsIgnoreCase("WINDOWS7") || sRead.ContainsIgnoreCase("WIN7") || sRead.ContainsIgnoreCase("/7.") || F.ContainsIgnoreCase("\\WIN7\\") || F.ContainsIgnoreCase("\\WIN 7\\")) { WinDrv += ", 7"; }
                if (sRead.ContainsIgnoreCase(".NTX86.6.2") || sRead.ContainsIgnoreCase("NTAMD64.6.2") || sRead.ContainsIgnoreCase("WINDOWS 8") || sRead.ContainsIgnoreCase("WINDOWS8") || sRead.ContainsIgnoreCase("WIN8") || sRead.ContainsIgnoreCase("/8.") || F.ContainsIgnoreCase("\\WIN8\\") || F.ContainsIgnoreCase("\\WIN 8\\")) { WinDrv += ", 8"; }

                if (WinDrv.EqualsIgnoreCase("Unknown")) { N.BackColor = Color.Yellow; }
                if (WinDrv.EqualsIgnoreCase("XP, Vista, 8")) { WinDrv = "7"; }
                if (WinDrv.EqualsIgnoreCase("7")) { WinDrv = "Win 7"; }
                if (WinDrv.EqualsIgnoreCase("8")) { WinDrv = "Win 8"; }

                if (WinDrv.StartsWithIgnoreCase("XP or newer,")) { WinDrv = WinDrv.Substring(12); }

                bool a86 = false; bool a64 = false;
                if (sArc.EqualsIgnoreCase("NTX86"))
                {
                    if (sRead.ContainsIgnoreCase(".X86") || sRead.ContainsIgnoreCase("NTX86") ||
                          sRead.ContainsIgnoreCase("32-BIT") || sRead.ContainsIgnoreCase(".NT.") ||
                          sRead.ContainsIgnoreCase("[INTEL_HDC]") || sRead.ContainsIgnoreCase("[INTEL_SYS]") ||
                          sRead.ContainsIgnoreCase(";32X86") || sRead.ContainsIgnoreCase("[MARVELL]") ||
                          sRead.ContainsIgnoreCase(".X86]"))
                    {
                        a86 = true;
                    }
                }

                if (sArc.EqualsIgnoreCase("NTAMD64"))
                {
                    if (sRead.ContainsIgnoreCase(".AMD64") || sRead.ContainsIgnoreCase("NTAMD64") ||
                          sRead.ContainsIgnoreCase("64-BIT") || sRead.ContainsIgnoreCase("NTX64") || sRead.ContainsIgnoreCase(".IA64"))
                    {
                        a64 = true;
                    }
                }

                if (sRead.ContainsIgnoreCase(".AMD64") || sRead.ContainsIgnoreCase("NTAMD64") ||
                      sRead.ContainsIgnoreCase("64-BIT") || sRead.ContainsIgnoreCase("NTX64") || sRead.ContainsIgnoreCase(".IA64"))
                {
                    a64 = true;
                }

                if (sRead.ContainsIgnoreCase(".X86") || sRead.ContainsIgnoreCase("NTX86") ||
                sRead.ContainsIgnoreCase("32-BIT") || sRead.ContainsIgnoreCase("NT,") || sRead.ContainsIgnoreCase(".NT.") ||
                sRead.ContainsIgnoreCase("[INTEL_HDC]") || sRead.ContainsIgnoreCase("[INTEL_SYS]") ||
                sRead.ContainsIgnoreCase(";32X86") || sRead.ContainsIgnoreCase("[MARVELL]") ||
                sRead.ContainsIgnoreCase(".X86]") || (sRead.ContainsIgnoreCase("[INTEL.NTAMD64]") && sRead.ContainsIgnoreCase("[INTEL]")))
                {
                    a64 = a86 = true;
                }

                if (F.ContainsIgnoreCase("X86") || F.ContainsIgnoreCase("I386") || F.ContainsIgnoreCase("X32")) { a86 = true; }
                if (F.ContainsIgnoreCase("X64") || F.ContainsIgnoreCase("AMD64") || F.ContainsIgnoreCase("IA64")) { a64 = true; }

                if (a86 && a64)
                {
                    cArc = "Mix";
                }
                else if (a64)
                {
                    cArc = "x64";
                }
                else
                {
                    cArc = "x86";
                }

                string Override = Path.GetDirectoryName(F) + "\\";

                if (File.Exists(Override + "x86.txt") || File.Exists(Override + "x64.txt"))
                {
                    if (File.Exists(Override + "x86.txt"))
                    {
                        cArc = "x86";
                    }
                    if (File.Exists(Override + "x64.txt"))
                    {
                        cArc = "x64";
                    }
                    if (File.Exists(Override + "Mix.txt"))
                    {
                        cArc = "Mix";
                    }
                    if (File.Exists(Override + "x86.txt") && File.Exists(Override + "x64.txt"))
                    {
                        cArc = "Mix";
                    }
                }

                if (sRead.ContainsIgnoreCase("CLASS") || sRead.ContainsIgnoreCase("DRIVERVER"))
                {
                    foreach (string I in sRead.Split(Environment.NewLine.ToCharArray()))
                    {
                        try
                        {
                            string line = I.Split(';')[0].Trim();
                            if (I.ContainsIgnoreCase("CLASS") && I.ContainsIgnoreCase("=") && sClass.EqualsIgnoreCase("Unknown") && !I.ContainsIgnoreCase("{") && !I.ContainsIgnoreCase(";"))
                            {
                                sClass = I.Split('=')[1].Trim();
                            }

                            if (I.ContainsIgnoreCase("DRIVERVER") && I.ContainsIgnoreCase("="))
                            {
                                sDate = I.Split('=')[1].Split(',')[0].Trim();
                                if (I.ContainsIgnoreCase(","))
                                {
                                    sVersion = line.Split('=')[1].Split(',')[1].Trim();
                                }
                            }
                            if (sDate != "N/A" && sVersion != "N/A" && sClass != "Unknown") { break; }
                        }
                        catch (Exception Ex)
                        {
                            new SmallError("Unable to check driver", Ex, sRead).Upload();
                        }
                    }
                }
                else
                {
                    N.Text = "???";
                }

                if (sDate.StartsWithIgnoreCase("%")) { sDate = "N/A"; }
                if (sVersion.StartsWithIgnoreCase("%")) { sVersion = "N/A"; }

                if (N.Text != "???")
                {
                    if (sArc.EqualsIgnoreCase("NTX86") && cArc.EqualsIgnoreCase("x64"))
                    {
                        N.BackColor = Color.LightPink;
                    }

                    if (sArc.EqualsIgnoreCase("NTAMD64") && cArc.EqualsIgnoreCase("x86"))
                    {
                        N.BackColor = Color.LightPink;
                    }

                    N.Text = cMain.GetFName(F);
                    N.SubItems.Add(sClass.ToUpper());
                    N.SubItems.Add(cArc);
                    N.SubItems.Add(WinDrv.Trim());
                    sDate = sDate.ReplaceIgnoreCase("\"", "");

                    try
                    {
                        string newDate = "";
                        DateTime DT = DateTime.ParseExact(sDate, "dd/mm/yyyy", CultureInfo.InvariantCulture);

                        sDate = DT.ToString("yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch (Exception Exception) { }

                    if (string.IsNullOrEmpty(sDate)) { sDate = "N/A"; }
                    N.SubItems.Add(sDate);// ();
                    N.SubItems.Add(sVersion.ReplaceIgnoreCase("\"", "").ReplaceIgnoreCase("\"", ""));
                    N.SubItems.Add(F);
                    N.Group = lstDrivers.Groups[0];
                    N.SubItems.Add(sArc.EqualsIgnoreCase("NTX86") ? "x86" : "x64");
                    N.SubItems.Add(Path.GetDirectoryName(F));
                    N.SubItems.Add(MD5);

                    int ImgIndx = -1;
                    if (N.SubItems[1].Text.EqualsIgnoreCase("HDC") && lstOptions.FindItemWithText("Integrate 'HDC' Drivers (boot.wim)").Checked) { ImgIndx = 0; }
                    if (N.SubItems[1].Text.EqualsIgnoreCase("SYSTEM") && lstOptions.FindItemWithText("Integrate 'SYSTEM' Drivers (boot.wim)").Checked) { ImgIndx = 0; }
                    if (N.SubItems[1].Text.EqualsIgnoreCase("SCSIADAPTER") && lstOptions.FindItemWithText("Integrate 'SCSIADAPTER' Drivers (boot.wim)").Checked) { ImgIndx = 0; }
                    if (N.SubItems[1].Text.EqualsIgnoreCase("USB") && lstOptions.FindItemWithText("Integrate 'USB' Drivers (boot.wim)").Checked) { ImgIndx = 0; }

                    if (ImgIndx == 0)
                    {
                        N.ImageIndex = 0;
                        N.ToolTipText = "MD5: " + MD5 + "\nThis driver will also be integrated into the boot.wim";
                    }
                    else
                    {
                        N.ToolTipText = "MD5: " + MD5;
                    }

                    lstDrivers.Items.Add(N);
                }

            }
            catch (Exception Ex)
            {
                new SmallError("Error Adding Driver", Ex, "An error occurred whilst trying to add: " + F + "\n" + lblStatus.Text).Upload();
                dErr += "Driver Error: " + F + "\r\n" + Ex.Message + "\r\n";
            }

            if (lstDrivers.Items.Count == 0)
                lstDrivers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            else
                lstDrivers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void InvalDrivers()
        {
            try
            {
                if (lstOptions.FindItemWithText("Remove Invalid Drivers", false, 0).Checked == true)
                {
                    foreach (ListViewItem LST in lstDrivers.Items.Cast<ListViewItem>().Where(D => D.BackColor == Color.LightPink))
                    {
                        dErr += "Driver Skipped: " + LST.SubItems[6].Text + "\r\n";
                        LST.Remove();
                    }
                }
            }
            catch (Exception Ex)
            {
                new SmallError("Error removing invalid drivers", Ex).Upload();
            }
        }

        private void cmdSIUp_Click(object sender, EventArgs e)
        {
            ListViewEx.LVE = GetSelectedList();
            if (ListViewEx.LVE == null)
                return;
            if (ListViewEx.LVE.SelectedItems.Count != 1) { MessageBox.Show("You need to select one item to move, you have selected " + ListViewEx.LVE.SelectedItems.Count + ".", ListViewEx.LVE.SelectedItems.Count + " selected!"); return; }

            try
            {
                cMain.MoveListViewItem(ListViewEx.LVE, cMain.MoveDirection.Up);
            }
            catch (Exception Ex) { }
        }

        private void cmdSIDown_Click(object sender, EventArgs e)
        {
            ListViewEx.LVE = GetSelectedList();
            if (ListViewEx.LVE == null)
                return;

            if (ListViewEx.LVE.SelectedItems.Count != 1) { MessageBox.Show("You need to select one item to move, you have selected " + ListViewEx.LVE.SelectedItems.Count + ".", ListViewEx.LVE.SelectedItems.Count + " selected!"); return; }
            try
            {
                cMain.MoveListViewItem(ListViewEx.LVE, cMain.MoveDirection.Down);
            }
            catch (Exception Ex)
            {
            }
        }


        private void tvChilds(TreeNode TN)
        {
            foreach (TreeNode TC in TN.Nodes)
            {
                TC.Checked = TN.Checked;
                if (GetSelectedTab() == TabTweaks)
                {
                    lAIO = false;
                    tvTweaks_Checked(TC);
                    lAIO = true;
                }
                if (TC.Nodes.Count > 0)
                {
                    if (TC.Nodes[0].Tag != null)
                    {
                        if (TC.Nodes[0].Tag != "C" && TC.Nodes[0].Tag != "Reg") { tvChilds(TC); }
                    }
                    else
                    {
                        tvChilds(TC);
                    }
                }
            }

        }

        private void tvParents(TreeNode TN)
        {
            TN.Checked = false;
            foreach (TreeNode TC in TN.Nodes)
            {
                if (TC.Checked) { TN.Checked = true; }
                if (TN.Parent != null) { tvParents(TN.Parent); }
            }

        }

        private void tvPreset_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (lAIO == false)
            {
                lAIO = true;

                try
                {
                    if (e.Node.Parent != null) { tvParents(e.Node.Parent); }

                    if (e.Node.Nodes.Count > 0)
                    {
                        if (e.Node.Nodes[0].Tag != null)
                        {
                            if (e.Node.Nodes[0].Tag != "C" && e.Node.Nodes[0].Tag != "Reg") { tvChilds(e.Node); }
                        }
                        else
                        {
                            tvChilds(e.Node);
                        }
                    }
                }
                catch { }

                lAIO = false;
            }
            if (TPMain.SelectedTab == TabBasic && TPBasic.SelectedTab == TabTweaks)
            {
                tvTweaks_Checked(e.Node);
            }
        }

        private void mnuBVMI_Click(object sender, EventArgs e)
        {
            cMain.OpenLink(
                  "http://www.blackviper.com/2010/12/17/black-vipers-windows-7-service-pack-1-service-configurations/");
        }

        private void BlackViper(string Filter)
        {
            iAdding = true;
            foreach (ListViewItem LST in lstServices.Items)
            {
                LST.Checked = false;
                try
                {
                    if (LST.Tag.ToString().ContainsIgnoreCase(Filter))
                    {
                        LST.Checked = true;
                        LST.SubItems[1].Text = "Disabled";
                    }
                }
                catch
                {
                }
            }
            iAdding = false;
            UpdateNames(false);
        }

        private void mnuBVSafe_Click(object sender, EventArgs e)
        {
            DialogResult DR =
                  MessageBox.Show(
                         "This will untick everything you have selected and will apply the 'Safe' preset, do you wish to continue?",
                         "Safe BlackViper Preset", MessageBoxButtons.YesNo);
            if (DR != DialogResult.Yes)
            {
                return;
            }
            BlackViper("@");
        }

        private void mnuBVTweaked_Click(object sender, EventArgs e)
        {
            DialogResult DR =
                  MessageBox.Show(
                         "This will untick everything you have selected and will apply the 'Tweaked' preset, do you wish to continue?",
                         "Tweaked BlackViper Preset", MessageBoxButtons.YesNo);
            if (DR != DialogResult.Yes)
            {
                return;
            }
            BlackViper("#");
        }

        private void mnuBVBareBoned_Click(object sender, EventArgs e)
        {
            DialogResult DR =
                  MessageBox.Show(
                         "This will untick everything you have selected and will apply the 'BareBoned' preset, do you wish to continue?" +
                         Environment.NewLine + Environment.NewLine +
                         "WARNING: This will disable quite a lot of services that you may need!", "BareBoned BlackViper Preset",
                         MessageBoxButtons.YesNo);
            if (DR != DialogResult.Yes)
            {
                return;
            }
            BlackViper("$");
        }

        private void cmdFClear_Click(object sender, EventArgs e)
        {
            txtFLocation.Text = "";
            txtFSaveTo.Text = "";
            chkFReplace.Checked = true;
        }

        private void cmdFCancel_Click(object sender, EventArgs e)
        {
            panFiles.Visible = false;
            cmdStart.Enabled = true;
        }

        private void cmdFLocate_Click(object sender, EventArgs e)
        {
            var OFD = new OpenFileDialog();
            OFD.Filter = "All Files *.*|*.*";
            OFD.Title = "Select File...";
            OFD.AddExtension = true;
            if (OFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            txtFLocation.Text = OFD.FileName;

            string F = OFD.FileName;
            while (F.ContainsIgnoreCase(":")) { F = F.Substring(1); }
            while (F.ContainsIgnoreCase("\\")) { F = F.Substring(1); }

            txtFSaveTo.Text = "%SystemDrive%\\" + F;
        }

        private string ConvertEnvVar(string loc)
        {
            string L = loc;
            try
            {
                if (loc.StartsWithIgnoreCase(Environment.ExpandEnvironmentVariables("%SystemRoot%")))
                {
                    L = L.ReplaceIgnoreCase(Environment.ExpandEnvironmentVariables("%SystemRoot%"), "%SystemRoot%");
                }
                if (loc.StartsWithIgnoreCase(Environment.ExpandEnvironmentVariables("%SystemDrive%")))
                {
                    L = L.ReplaceIgnoreCase(Environment.ExpandEnvironmentVariables("%SystemDrive%"), "%SystemDrive%");
                }
            }
            catch (Exception Ex)
            {
            }
            return L;
        }

        private void cmdFSave_Click(object sender, EventArgs e)
        {
            var SFD = new SaveFileDialog();
            SFD.DefaultExt = cMain.GetExtension(txtFLocation.Text);

            SFD.OverwritePrompt = false;
            SFD.Filter = "All Files *|*";
            SFD.Title = "Select File...";
            if (SFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            txtFSaveTo.Text = ConvertEnvVar(SFD.FileName);
        }

        private void cmdFOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFLocation.Text))
            {
                MessageBox.Show("Please select a file to copy.", "Invalid Filename");
                return;
            }

            if (!File.Exists(txtFLocation.Text))
            {
                MessageBox.Show("The file you have selected does not exist!", "Invalid File");
                return;
            }

            if (txtFSaveTo.Text.ContainsIgnoreCase(":"))
            {
                MessageBox.Show("The save to location can't contain a ':'.", "Invalid Path");
                return;
            }

            if (!txtFSaveTo.Text.StartsWithIgnoreCase("%SYSTEMROOT%\\") && !txtFSaveTo.Text.StartsWithIgnoreCase("%SYSTEMDRIVE%\\") && !txtFSaveTo.Text.StartsWithIgnoreCase("%DVDROOT%\\"))
            {
                MessageBox.Show("The save to path must begin with either %SystemRoot%\\, %SystemDrive%\\ or %DVDRoot%\\.", "Invalid Path");
                return;
            }

            if (string.IsNullOrEmpty(txtFSaveTo.Text))
            {
                MessageBox.Show("Please select where you want to save the file.", "Invalid Save Location");
                return;
            }

            if (!txtFSaveTo.Text.ToUpper().EndsWithIgnoreCase(cMain.GetExtension(txtFLocation.Text).ToUpper()))
            {
                MessageBox.Show("You need to make sure you add a filename and extension to the 'Save To' path.", "Invalid Save Location");
                return;
            }

            if (string.IsNullOrEmpty(DVD) && txtFSaveTo.Text.ContainsIgnoreCase("%DVDROOT%"))
            {
                MessageBox.Show("DVD source could not be detected. %DVDRoot% can't be used.", "Stop");
                return;
            }

            var NI = new ListViewItem();
            NI.Group = lstFiles.Groups[0];
            NI.Text = cMain.GetFName(txtFSaveTo.Text);
            NI.SubItems.Add(cMain.GetSize(txtFLocation.Text));
            NI.SubItems.Add(chkFReplace.Checked.ToString(CultureInfo.InvariantCulture));
            NI.SubItems.Add(txtFLocation.Text);
            NI.SubItems.Add(ConvertEnvVar(txtFSaveTo.Text));

            if (lstFiles.FindItemWithText(txtFSaveTo.Text) == null)
            {
                lstFiles.Items.Add(NI);
            }

            mnuR.Visible = true;
            UpdateNames(true);
            panFiles.Visible = false;
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            cmdRF.PerformClick();
            ListViewEx.LVE = GetSelectedList();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            mnuAdd.PerformClick();
            ListViewEx.LVE = GetSelectedList();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            cmsCAIG.Visible = true;
            cmsCAIL.Visible = true;
            cmsSAIG.Visible = true;
            cmsSAIL.Visible = true;
            cmsSAIL.Text = "Select All in List";
            cmsSAIG.Text = "Select All in Group";
            cmsCAIL.Text = "Check All in List";
            cmsCAIG.Text = "Check All in Group";

            cmsSAIL.Image = Resources.OK;
            cmsSAIG.Image = Resources.OK;
            cmsCAIL.Image = Resources.OK;
            cmsCAIG.Image = Resources.OK;

            if (GetSelectedTab() == tabComponents)
            {
                LV = lstComponents;
            }

            if (LV == null)
            {
                e.Cancel = true;
                return;
            }

            if (LV.CheckBoxes)
            {
                cmsSAIL.Visible = false;
                cmsSAIG.Visible = false;
            }
            else
            {
                cmsCAIL.Visible = false;
                cmsCAIG.Visible = false;
            }

            if (LV.SelectedItems.Count == LV.Items.Count)
            {
                cmsSAIL.Text = "Unselect All in List";
                cmsSAIL.Image = Resources.Close;
            }
            if (LV.CheckedItems.Count == LV.Items.Count)
            {
                cmsCAIL.Text = "Uncheck All in List";
                cmsCAIL.Image = Resources.Close;
            }
            if (LV.SelectedItems.Count > 0)
            {
                if (LV.SelectedItems.Count == LV.SelectedItems[0].Group.Items.Count)
                {
                    cmsSAIG.Text = "Unselect All in Group";
                    cmsSAIG.Image = Resources.Close;
                }
                if (LV.CheckedItems.Count == LV.SelectedItems[0].Group.Items.Count)
                {
                    cmsCAIG.Text = "Uncheck All in Group";
                    cmsCAIG.Image = Resources.Close;
                }
            }
            else
            {
                cmsCAIG.Visible = false;
                cmsSAIG.Visible = false;
            }

            cMain.FreeRAM();
        }

        private void cmsSAIL_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LST in LV.Items)
            {
                LST.Selected = cmsSAIL.Text.EqualsIgnoreCase("Select All in List");
            }
            LV.Select();
        }

        private void cmsSAIG_Click(object sender, EventArgs e)
        {
            if (LV.SelectedItems.Count > 0)
            {
                foreach (ListViewItem LST in LV.SelectedItems[0].Group.Items)
                {
                    LST.Checked = cmsSAIG.Text.EqualsIgnoreCase("Select All in Group");
                }
            }
        }

        private void cmsCAIL_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LST in LV.Items)
            {
                LST.Checked = cmsCAIL.Text.EqualsIgnoreCase("Check All in List");
            }
        }

        private void cmsCAIG_Click(object sender, EventArgs e)
        {
            if (LV.SelectedItems.Count > 0)
            {
                foreach (ListViewItem LST in LV.SelectedItems[0].Group.Items)
                {
                    LST.Checked = cmsCAIG.Text.EqualsIgnoreCase("Check All in Group");
                }
            }
        }

        private string uFile;
        private int uIndex;

        private void IntegrateUpdates(ListViewItem LST, int dIndex, bool bRetrying, int iMax, WIMImage img, ref UpdateInfo.Information subUpdateInfo)
        {
            if (subUpdateInfo == null)
            {
                subUpdateInfo = new UpdateInfo.Information(img.Name, img.Index);
            }
            if (LST.SubItems.Count < 5) { return; }

            string scratchDir = cOptions.WinToolkitTemp + "\\ScratchDir_" + LST.SubItems[5].Text.CreateMD5();

            cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Starting #2 (" + LST.SubItems.Count + "): " + dIndex + " of " + iMax + " :: " + LST.Text);
            string IF = LST.SubItems[5].Text;


            if (LST.SubItems[5].Text.ContainsForeignCharacters())
            {
                if (!Directory.Exists(cOptions.WinToolkitTemp)) { cMain.CreateDirectory(cOptions.WinToolkitTemp); }
                string sFilename = cOptions.WinToolkitTemp + "\\" + cMain.RandomName(1, 1000) + IF.Substring(IF.Length - 4);
                File.Copy(IF, sFilename, true);
                IF = sFilename;
            }


            cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Removing Temp: " + dIndex + " of " + iMax + " :: " + LST.Text);

            Application.DoEvents();


            CleanTemp("cInst");


            Files.DeleteFolder(scratchDir, true);
            cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Checking #1: " + dIndex + " of " + iMax + " :: " + LST.Text);
            try
            {
                if (LST.Text.StartsWithIgnoreCase("IE") && LST.Text.EndsWithIgnoreCase("LangPack"))
                {
                    cMain.ExtractFiles(IF, cOptions.WinToolkitTemp + "\\cInst" + dIndex + "_IELangPack", this, "*KB*", false);
                    if (Directory.Exists(cOptions.WinToolkitTemp + "\\cInst" + dIndex + "_IELangPack"))
                    {
                        foreach (string SI in Directory.GetFiles(cOptions.WinToolkitTemp + "\\cInst" + dIndex + "_IELangPack", "*.cab", SearchOption.TopDirectoryOnly))
                        {
                            IF = SI;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                LogErr("Unknown Error", Ex.Message);
            }


            cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Checking #2: " + dIndex + " of " + iMax + " :: " + LST.Text);

            if (File.Exists(IF))
            {

                cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Preparing: " + dIndex + " of " + iMax + " :: " + LST.Text);

                try
                {
                    if (IF.ToUpper().EndsWithIgnoreCase(".EXE"))
                    {
                        cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Deleting Temp Folders: " + dIndex + " of " + iMax + " (" + LST.Text + ")");
                        if (LST.Text.StartsWithIgnoreCase("Internet Explorer") && !LST.SubItems[1].Text.ContainsIgnoreCase("LangPack") && IF.ToUpper().EndsWithIgnoreCase(".EXE"))
                        {
                            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\IE", true);
                            cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Extracting: " + dIndex + " of " + iMax + " :: " + LST.Text);

                            cMain.RunExternal("\"" + IF + "\"", "/x:\"" + cOptions.WinToolkitTemp + "\\IE\"");


                            foreach (string dCAB in Directory.GetFiles(cOptions.WinToolkitTemp + "\\IE\\", "*", SearchOption.TopDirectoryOnly))
                            {
                                if (!dCAB.ToUpper().EndsWithIgnoreCase(".CAB") && !dCAB.ToUpper().EndsWithIgnoreCase(".MSU")) { Files.DeleteFile(dCAB); }
                                if (dCAB.ContainsIgnoreCase("_SUPPORT")) { Files.DeleteFile(dCAB); }

                            }
                            Files.DeleteFile(cOptions.WinToolkitTemp + "\\IE\\IE9_SUPPORT.CAB");
                            Files.DeleteFile(cOptions.WinToolkitTemp + "\\IE\\IE_SUPPORT_x86_en-US.CAB");//IE_SUPPORT_amd64_en-US
                            Files.DeleteFile(cOptions.WinToolkitTemp + "\\IE\\IE_SUPPORT_amd64_en-US.CAB");//

                            if (bRetrying)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Retrying: " + dIndex + " of " + iMax + " :: " + LST.Text);
                            }
                            else
                            {
                                cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Integrating: " + dIndex + " of " + iMax + " :: " + LST.Text);
                            }

                            if (Directory.Exists(cOptions.WinToolkitTemp + "\\IE"))
                            {
                                var sorted = Directory.GetFiles(cOptions.WinToolkitTemp + "\\IE", "*.cab", SearchOption.TopDirectoryOnly).OrderByDescending(f => new FileInfo(f).Length);
                                foreach (string S in sorted)
                                {
                                    string n = S;
                                    while (n.ContainsIgnoreCase("\\")) { n = n.Substring(1); }
                                    if (bRetrying)
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Retrying: " + dIndex + " of " + iMax + " :: " + LST.Text + "_" + n); Application.DoEvents();
                                    }
                                    else
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Integrating: " + dIndex + " of " + iMax + " :: " + LST.Text + "_" + n); Application.DoEvents();
                                    }
                                    subUpdateInfo.DISM += "----------------\r\n" + S + "\r\n";
                                    subUpdateInfo.DISM += cMain.RunExternal("\"" + DISM.Latest.Location + "\"", " /Image:\"" + currentlyMounted.MountPath + "\" /Add-Package /PackagePath:\"" + S + "\" /ScratchDir:\"" + scratchDir + "\"") + "\r\n";

                                    if (subUpdateInfo.DISM.ContainsIgnoreCase("0x800f081e"))
                                    {
                                        subUpdateInfo.Note = "The update is not applicable to the image OR a required update may not have been integrated first. Package may also be an express cab.";
                                    }
                                    // cMain.OpenProgram(, "/ip /o:\"" + currentlyMounted.MountPath + ";" + currentlyMounted.MountPath + "\\Windows\"  /m:\"" + S + "\" /quiet /norestart", true, ProcessWindowStyle.Hidden);
                                }
                                foreach (string S in Directory.GetFiles(cOptions.WinToolkitTemp + "\\IE", "*.msu", SearchOption.TopDirectoryOnly))
                                {
                                    string n = S;
                                    while (n.ContainsIgnoreCase("\\")) { n = n.Substring(1); }

                                    if (bRetrying)
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Retrying: " + dIndex + " of " + iMax + " :: " + LST.Text + "_" + n); Application.DoEvents();
                                    }
                                    else
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Integrating: " + dIndex + " of " + iMax + " :: " + LST.Text + "_" + n); Application.DoEvents();
                                    }
                                    subUpdateInfo.DISM += "----------------\r\n" + S + "\r\n";
                                    subUpdateInfo.DISM += cMain.RunExternal("\"" + DISM.Latest.Location + "\"", " /Image:\"" + currentlyMounted.MountPath + "\" /Add-Package /PackagePath:\"" + S + "\" /ScratchDir:\"" + scratchDir + "\"") + "\r\n";

                                    // cMain.OpenProgram("\"" + cMain.SysFolder + "\\pkgmgr.exe\"", "/ip /o:\"" + currentlyMounted.MountPath + ";" + currentlyMounted.MountPath + "\\Windows\"  /m:\"" + S + "\" /quiet /norestart", true, ProcessWindowStyle.Hidden);
                                }
                            }
                            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\IE", false);
                        }

                        if (LST.Text.EqualsIgnoreCase("Windows XP Mode") && !File.Exists(currentlyMounted.MountPath + "\\Program Files\\Windows XP Mode\\Windows XP Mode base.vhd"))
                        {
                            subUpdateInfo.DISM = "DISM was not used.";
                            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\XPM", true);
                            cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Extracting: " + dIndex + " of " + iMax + " :: " + LST.Text);

                            cMain.RunExternal("\"" + IF + "\"", "/x:\"" + cOptions.WinToolkitTemp + "\\XPM\" /Q");
                            if (LST.SubItems[2].Text.EqualsIgnoreCase("x86"))
                            {
                                cMain.RunExternal("\"" + cMain.SysFolder + "\\msiexec.exe\"", "/a \"" + cOptions.WinToolkitTemp + "\\XPM\\sources\\xpminstl32.msi\" /qn TARGETDIR=" + cOptions.WinToolkitTemp + "\\XPM");
                            }
                            else
                            {
                                cMain.RunExternal("\"" + cMain.SysFolder + "\\msiexec.exe\"", "/a \"" + cOptions.WinToolkitTemp + "\\XPM\\sources\\xpminstl64.msi\" /qn TARGETDIR=" + cOptions.WinToolkitTemp + "\\XPM");
                            }
                            cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Copying: " + dIndex + " of " + iMax + " :: " + LST.Text);

                            cMain.CopyDirectory(cOptions.WinToolkitTemp + "\\XPM\\Program Files", currentlyMounted.MountPath + "\\Program Files", true, false);
                            XPMode = true;
                            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\XPM", false);
                        }

                    }
                    else
                    {
                        bool LDR = lstOptions.FindItemWithText("LDR-QFE Mode").Checked;

                        if (LDR)
                        {
                            string sDISMImageLoc = currentlyMounted.MountPath + "\\Windows\\System32\\DISM.exe";

                            if (LDR && File.Exists(sDISMImageLoc))
                            {
                                var vDISMVersion = new Version(FileVersionInfo.GetVersionInfo(sDISMImageLoc).ProductVersion);
                                if (vDISMVersion >= new Version("6.2"))
                                {
                                    LDR = false;
                                }
                            }

                            if (LDR)
                            {
                                if (CheckUpdate("KB2685813", LST)) { LDR = false; }
                                if (CheckUpdate("KB947821", LST)) { LDR = false; }
                                if (CheckUpdate("KB2732059", LST)) { LDR = false; }
                                if (CheckUpdate("KB2932046", LST)) { LDR = false; }
                                if (CheckUpdate("KB2919355", LST)) { LDR = false; }
                                if (CheckUpdate("KB968211", LST)) { LDR = false; }
                                if (CheckUpdate("KB958830", LST)) { LDR = false; }
                                if (CheckUpdate("KB968212", LST)) { LDR = false; }
                                if (CheckUpdate("KB958559", LST)) { LDR = false; }
                                if (CheckUpdate("KB2841134", LST)) { LDR = false; }
                                if (CheckUpdate("KB2718695", LST)) { LDR = false; }
                                if (CheckUpdate("KB2883200", LST)) { LDR = false; }
                                if (CheckUpdate("KB2887595", LST)) { LDR = false; }
                            }
                        }


                        if (CheckUpdate("KB2685811", LST)) { LDR = true; }
                        if (CheckUpdate("KB2685813", LST)) { LDR = true; }

                        if (LDR && LST.Group.Header != "Language Packs")
                        {
                            uFile = IF;
                            uIndex = dIndex;
                            BWExtract.RunWorkerAsync();
                        }



                        bool bError = false;

                        if (bRetrying)
                        {
                            cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Retrying: " + dIndex + " of " + iMax + " :: " + LST.Text + " GDR");
                        }
                        else
                        {
                            cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Integrating: " + dIndex + " of " + iMax + " :: " + LST.Text + " GDR");
                        }


                        //MessageBox.Show("\"" + cMain.DISM + "\"" + "\n\n" +
                        //    " /Image:\"" + currentlyMounted.MountPath + "\" /Add-Package /PackagePath:\"" + IF +
                        //    "\" /ScratchDir:\"" + scratchDir + "\"");
                        string Integrate = cMain.RunExternal("\"" + DISM.Latest.Location + "\"", " /Image:\"" + currentlyMounted.MountPath + "\" /Add-Package /PackagePath:\"" + IF + "\" /ScratchDir:\"" + scratchDir + "\"");
                        // cMain.OpenProgram("\"" + cMain.DISM + "\"", "/Image:\"" + currentlyMounted.MountPath + "\" /Add-Package /PackagePath:\"" + IF + "\"", true, ProcessWindowStyle.Hidden);

                        try
                        {
                            if (lstOptions.FindItemWithText("Debug Updates").Checked)
                            {
                                using (var SW = new StreamWriter(cMain.ErrLogSavePath + "\\Updates_Log_" + runTime + ".log", true, System.Text.Encoding.UTF8))
                                {
                                    try
                                    {
                                        SW.WriteLine("Name: " + LST.Text);

                                        SW.WriteLine("Path: " + IF);
                                        SW.WriteLine("Result:\n" + Integrate);
                                        SW.WriteLine("\r\n---------------------------------------\r\n");
                                    }
                                    catch (Exception Ex3)
                                    {
                                        new SmallError("Error Writing Debug File", Ex3, LST).Upload();
                                    }

                                }
                            }
                        }
                        catch (Exception Ex)
                        {
                            new SmallError("Checking Debug Updates", Ex, LST).Upload();
                        }
                        //

                        subUpdateInfo.DISM = Integrate;
                        string updError = "";

                        bool dismError = Integrate.ContainsIgnoreCase("0xc1510114");
                        bool imgError = (Integrate.ContainsIgnoreCase("0x800f0830") || Integrate.ContainsIgnoreCase("0x80073713"));

                        if ((dismError || imgError) && !bError)
                        {

                            string sErrMessage = "";

                            if (dismError && !bError)
                            {
                                sErrMessage = "You seem to be using an older version of DISM than what is required. Please download the latest ADK from Microsoft.";
                            }

                            if (imgError && !bError)
                            {
                                sErrMessage = "Something bad has happened to your image and can no longer be worked on! There was no point in continuing.\n\nNOTE: This usually happens when you try and integrate a language pack of some sort without integrating the Windows language pack first. \n-For example, if you want to integrate a French language pack for Internet Explorer. Then you need to make sure to add the French Windows language pack first!\n\nYou may also want to try the integration process without including the last update that integrated successfully.";
                            }
                            LST.ImageIndex = 8;
                            LogErr(sErrMessage, Integrate);
                            cMain.ErrorBox(sErrMessage, "Aborting Integration!", Integrate);
                            bAbort = true;

                            return;
                        }


                        if ((Integrate.ContainsIgnoreCase("0x800f081e") || Integrate.ContainsIgnoreCase("0x800f0816") || Integrate.ContainsIgnoreCase("0x8000ffff")) && !bError)
                        {
                            subUpdateInfo.Note = "The update is not applicable to the image OR a required update may not have been integrated first. Package may also be an express cab.";
                            if (CheckUpdate("KB2819745", LST)) { subUpdateInfo.Note = "This update requires .NET Framework 4.5 to be integrated first."; }
                            //The Update is not applicable to the image OR a required update has not been integrated first. Package may also be an express cab.
                            bError = true;
                            LST.ImageIndex = 9;
                        }
                        if (Integrate.ContainsIgnoreCase("0x8007000d") && !bError)
                        {
                            subUpdateInfo.Note = "File is corrupted! Please re-download the file.";
                            //The Update is not applicable to the image OR a required update has not been integrated first. Package may also be an express cab.
                            bError = true;
                            LST.ImageIndex = 9;

                        }

                        if ((Integrate.ContainsIgnoreCase("0x800f082e") || Integrate.ContainsIgnoreCase("0x80070057")) && !bError)
                        {
                            //This update can't be integrated offline. It has been automatically added to the Silent Installers List.
                            subUpdateInfo.Note = "This update can't be integrated offline. It has been automatically added to the Silent Installers List.";
                            bError = true;
                            LST.ImageIndex = 0;

                            if (lstSilent.FindItemWithText(LST.SubItems[5].Text) == null && (lstSilent.FindItemWithText(LST.SubItems[6].Text) == null || string.IsNullOrEmpty(LST.SubItems[6].Text)))
                            {
                                var LST2 = new ListViewItem();

                                LST2.Text = LST.Text;
                                LST2.SubItems.Add("N/A (Not Needed)");
                                LST2.SubItems.Add("NO");
                                LST2.SubItems.Add(LST.SubItems[3].Text);
                                LST2.SubItems.Add(LST.SubItems[5].Text);
                                LST2.Group = lstSilent.Groups[0];
                                lstSilent.Items.Add(LST2);
                                UpdateNames(false);
                                return;
                            }
                        }

                        if (Integrate.ContainsIgnoreCase("Error: ") && Integrate.ContainsIgnoreCase("0x80"))
                        {
                            subUpdateInfo.Note = "Unknown Error";
                            if (!bError)
                            {
                                LogErr("An error occurred whilst integrating an update!", Integrate);
                                LST.ImageIndex = 8;
                            }
                        }

                        if (!string.IsNullOrEmpty(uFile) && uIndex != -1 && LDR)
                        {
                            while (BWExtract.IsBusy)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Extracting: " + dIndex + " of " + iMax + " :: " + LST.Text);
                                Application.DoEvents();
                                Thread.Sleep(50);
                            }
                        }

                        string BF = cOptions.WinToolkitTemp + "\\cInst" + dIndex + "_CAB\\update-bf.mum";
                        if (LDR && !BWRun.CancellationPending && !bAbort && File.Exists(BF))
                        {
                            if (bRetrying)
                            {
                                cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Retrying: " + dIndex + " of " + iMax + " :: " + LST.Text + " QFE");
                            }
                            else
                            {
                                cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Integrating: " + dIndex + " of " + iMax + " :: " + LST.Text + " QFE");
                            }

                            Files.DeleteFolder(scratchDir, true);
                            string bfIntegrate = cMain.RunExternal("\"" + DISM.Latest.Location + "\"", "/Image:\"" + currentlyMounted.MountPath + "\" /Add-Package /PackagePath:\"" + BF + "\" /ScratchDir:\"" + scratchDir + "\"");
                            //cMain.OpenProgram("\"" + cMain.DISM + "\"", "/Image:\"" + currentlyMounted.MountPath + "\" /Add-Package /PackagePath:\"" + BF + "\" /ScratchDir:\"" + cOptions.WinToolkitTemp + "\\ScratchDir", true, ProcessWindowStyle.Hidden);
                        }

                    }
                    cMain.UpdateToolStripLabel(lblStatus, LST.Group.Header + " | Deleting Temp Files: " + dIndex + " of " + iMax + " :: " + LST.Text);
                    if (IF != LST.SubItems[5].Text && File.Exists(IF)) { Files.DeleteFile(IF); }

                }
                catch (Exception Ex)
                {
                    Files.DeleteFolder(cOptions.WinToolkitTemp + "\\XPM", false);
                    Files.DeleteFolder(cOptions.WinToolkitTemp + "\\IE", false);
                    string uErr = LST.Text = Environment.NewLine;
                    uErr += cMain.GetSubItems(LST);
                    cMain.WriteLog(this, "Error Integrating Update", uErr + Environment.NewLine + Environment.NewLine + Ex.Message, lblStatus.Text);
                    LogErr("Error Integrating Update", uErr);
                }

                if (LST.SubItems[5].Text.ContainsForeignCharacters() && IF != LST.SubItems[5].Text)
                {
                    Files.DeleteFile(IF);
                }

                if (BWRun.CancellationPending || bAbort) { return; }

            }

            cMain.FreeRAM();
        }

        private bool CheckUpdate(string sSearch, ListViewItem LST)
        {
            foreach (ListViewItem.ListViewSubItem S in LST.SubItems)
            {
                if (S.Text.ContainsIgnoreCase(sSearch.ToUpper())) { return true; }
            }
            return false;
        }

        private void lstUpdates_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            foreach (ListViewItem LST in lstUpdates.SelectedItems)
            {
                new Prompts.frmIntegrate(LST).ShowDialog();

                return;
            }
        }

        private void cmdDefaultTheme_Click(object sender, EventArgs e)
        {
            if (lstThemes.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please select just one theme which you want to make default!", "No Theme Selected");
                return;
            }
            if (lstThemes.SelectedItems[0].Font.Bold)
            {
                lstThemes.SelectedItems[0].Font = new Font(lstThemes.SelectedItems[0].Font, FontStyle.Regular);
            }
            else
            {
                string TF = lstThemes.SelectedItems[0].SubItems[2].Text;
                if (TF.ToUpper().EndsWithIgnoreCase(".THEME"))
                {
                    bool RSS = false;
                    if (File.Exists(TF))
                    {
                        using (var SR = new StreamReader(TF))
                        {
                            if (SR.ReadToEnd().ContainsIgnoreCase("RSSFEED="))
                            {
                                RSS = true;
                            }
                        }

                    }
                    if (RSS)
                    {
                        DialogResult DR =
                            MessageBox.Show(
                                "This theme has an RSS theme and Windows will prompt you after installation if it is OK to download wallpapers." +
                                Environment.NewLine + Environment.NewLine +
                                "This may annoy people who like fully unattended and i have not found a workaround for it" +
                                Environment.NewLine + Environment.NewLine +
                                "Are you sure you want to set this as your default theme?", "Are you sure?",
                                MessageBoxButtons.YesNo);
                        if (DR != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                }

                foreach (ListViewItem LST in lstThemes.Items.Cast<ListViewItem>().Where(t => t.Font.Bold && !t.Selected))
                {
                    LST.Font = new Font(LST.Font, FontStyle.Regular);
                }
                lstThemes.SelectedItems[0].Font = new Font(lstThemes.SelectedItems[0].Font, FontStyle.Bold);
            }

            foreach (ColumnHeader CH in lstThemes.Columns)
            {
                CH.Width = -2;
            }
        }

        private void cmdDefaultT_Click(object sender, EventArgs e)
        {
            cmdDefaultTheme.PerformClick();
        }

        private void cmdSBRestore_Click(object sender, EventArgs e)
        {
            mnuRefresh.PerformClick();
        }

        private void cmdSBSafe_Click(object sender, EventArgs e)
        {
            mnuBVSafe.PerformClick();
        }

        private void cmdSBTweaked_Click(object sender, EventArgs e)
        {
            mnuBVTweaked.PerformClick();
        }

        private void cmdSBBone_Click(object sender, EventArgs e)
        {
            mnuBVBareBoned.PerformClick();
        }

        private void cmdSBCheckAll_Click(object sender, EventArgs e)
        {
            lAIO = true;
            if (TPAdvanced.SelectedTab == tabComponents2)
            {
                ListNodes(tvComponents, true);
                foreach (TreeNode TN in tvList) { TN.Checked = true; }
                tvComponents.ExpandAll();
            }
            else
            {
                lstComponents.BeginUpdate();
                foreach (ListViewItem LST in lstComponents.Items)
                {
                    LST.Checked = true;
                }
                lstComponents.EndUpdate();

            }
            UpdateNames(false);
            lAIO = false;
        }

        private void cmdSBUnCheckAll_Click(object sender, EventArgs e)
        {
            lAIO = true;
            if (TPAdvanced.SelectedTab == tabComponents2)
            {
                ListNodes(tvComponents, true);
                foreach (TreeNode TN in tvList) { TN.Checked = false; }
                tvComponents.CollapseAll();
            }
            else
            {

                lstComponents.BeginUpdate();
                foreach (ListViewItem LST in lstComponents.Items)
                {
                    LST.Checked = false;
                }
                lstComponents.EndUpdate();
            }
            UpdateNames(false);
            lAIO = false;
        }

        private void cmdSBODefault_Click(object sender, EventArgs e)
        {
            DialogResult DR =
                  MessageBox.Show("This will restore all the options to their default setting, do you wish to continue?",
                                              "Options", MessageBoxButtons.YesNo);
            if (DR != DialogResult.Yes)
            {
                return;
            }
            foreach (ListViewItem LST in lstOptions.Items)
            {
                try
                {
                    LST.Checked = Convert.ToBoolean(LST.SubItems[2].Text);
                }
                catch
                {
                    MessageBox.Show(LST.Text);
                }
            }
        }

        private void cmdSBSInfo_Click(object sender, EventArgs e)
        {
            mnuBVMI.PerformClick();
        }

        #region Nested type: TVITEM

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
        private struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            private readonly string lpszText;
            private readonly int cchTextMax;
            private readonly int iImage;
            private readonly int iSelectedImage;
            private readonly int cChildren;
            private readonly IntPtr lParam;
        }

        #endregion

        int s, m, h, d;

        private void timEllapsed_Tick(object sender, EventArgs e)
        {
            try
            {
                s += 1;
                if (s == 60)
                {
                    s = 0;
                    m += 1;
                }
                if (m == 60)
                {
                    m = 0;
                    h += 1;
                }
                if (h > 60)
                {
                    h = 0;
                    d += 1;
                }

                if (h < 1)
                {
                    lblTime.Text = m.ToString("00") + "m:" + s.ToString("00") + "s";
                }

                if (h > 0 && d < 1)
                {
                    lblTime.Text = h.ToString("00") + "h:" + m.ToString("00") + "m:" + s.ToString("00") + "s";
                }

                if (d > 0)
                {
                    lblTime.Text = d.ToString("00") + "d:" + h.ToString("00") + "h:" + m.ToString("00") + "m:" + s.ToString("00") + "s";
                }
            }
            catch
            {
                timEllapsed.Enabled = false;
            }
        }

        private void IntegratedDrivers(string IMGPath, int i)
        {
            if (lstOptions.FindItemWithText("Prompt Drivers").Checked && !BWRun.CancellationPending && !bAbort)
            {
                try
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Getting Drivers List...");
                    string UIDCheck = cMain.RunExternal("\"" + DISM.Latest.Location + "\"", "/Image:\"" + IMGPath + "\" /Get-Drivers /Format:Table");
                    //string UIDCheck;
                    //using (StreamReader SR = new StreamReader("C:\\Users\\Liam\\Desktop\\Drivers.txt"))
                    //{
                    //    UIDCheck = SR.ReadToEnd();
                    //}


                    foreach (string Line in UIDCheck.Split(Environment.NewLine.ToCharArray()))
                    {
                        try
                        {
                            if (Line.ContainsIgnoreCase(".INF") && !string.IsNullOrEmpty(Line))
                            {
                                var NI = new ListViewItem();
                                foreach (string D in Line.Split('|'))
                                {
                                    string LD = D.ReplaceIgnoreCase(" ", "");
                                    if (string.IsNullOrEmpty(NI.Text)) { NI.Text = LD; } else { NI.SubItems.Add(LD); }
                                }
                                NI.Group = i == -1 ? lstIDrivers.Groups[lstIDrivers.Groups.Count - 1] : lstIDrivers.Groups[i - 1];

                                string name = NI.Text;
                                if (name.StartsWithIgnoreCase("OEM"))
                                {
                                    if (name.Length == 8) { name = name.Insert(3, "000"); }
                                    if (name.Length == 9) { name = name.Insert(3, "00"); }
                                    if (name.Length == 10) { name = name.Insert(3, "0"); }
                                    NI.Text = name;
                                }

                                lstIDrivers.Items.Add(NI);
                            }
                        }
                        catch { }
                        lstIDrivers.Sorting = SortOrder.Ascending;
                        cMain.FreeRAM();
                    }
                }
                catch (Exception Ex)
                {
                    LogErr("Error detecting drivers.", Ex.Message);
                    cMain.WriteLog(this, "Error detecting drivers", Ex.Message, lblStatus.Text);
                }
            }
        }

        private void mnuILoad_Click(object sender, EventArgs e)
        {
            var OFD = new OpenFileDialog();
            OFD.Title = "Load Settings";
            OFD.Filter = "AIO Settings *.ini|*.ini";

            if (OFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            LoadPreset(OFD.FileName, GetSelectedTab());
            EnablePan();
        }

        private void mnuISave_Click(object sender, EventArgs e)
        {
            var SFD = new SaveFileDialog();
            SFD.Title = "Save Settings";
            SFD.Filter = "AIO Settings *.ini|*.ini";

            if (SFD.ShowDialog() != DialogResult.OK) { return; }

            SaveLog(SFD.FileName, true, GetSelectedTab());
        }

        private void tvComponents_AfterSelect(object sender, TreeViewEventArgs e)
        {

            cComponents.GetCInfo(e.Node, lblCTitle, lblCSize, lblCDesc, lblCDepend, lblCSafe, pbSafe, fpComponents);

        }

        private void tvTweaks_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lblTName.Text = "Name: " + e.Node.Text;
            if (string.IsNullOrEmpty(e.Node.ToolTipText)) { lblTDesc.Text = "There is no further information on this component."; } else { lblTDesc.Text = "Description: " + e.Node.ToolTipText; }

        }

        private void lstDrivers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstDrivers.SelectedItems.Count > 0)
            {
                cMain.OpenProgram("\"" + cMain.SysFolder + "\\Notepad.exe\"", lstDrivers.SelectedItems[0].SubItems[6].Text, false, ProcessWindowStyle.Maximized);
            }
        }

        private void mnuSaveLoad_DropDownOpening(object sender, EventArgs e)
        {
            if (TPMain.SelectedTab.Text.EqualsIgnoreCase("Integrated"))
            {
                mnuILoad.Visible = false;
                mnuISave.Visible = false;
            }
            else
            {
                mnuILoad.Visible = true;
                mnuISave.Visible = true;
            }
        }



        private void mnuClear_Click(object sender, EventArgs e)
        {
            DialogResult DR = MessageBox.Show("WARNING: This will empty all lists and resets everything to their default value!" + Environment.NewLine + Environment.NewLine + "Are you sure?", "WARNING", MessageBoxButtons.YesNo);
            if (DR != DialogResult.Yes) { return; }
            ClearLists();
        }

        private void txtSIName_TextChanged(object sender, EventArgs e)
        {
            // \ / : * ? " < > |

            if (txtSIName.Text.ContainsIgnoreCase("\\") || txtSIName.Text.ContainsIgnoreCase("/") || txtSIName.Text.ContainsIgnoreCase(":") || txtSIName.Text.ContainsIgnoreCase("*") || txtSIName.Text.ContainsIgnoreCase("?") || txtSIName.Text.ContainsIgnoreCase("\"") || txtSIName.Text.ContainsIgnoreCase("<") || txtSIName.Text.ContainsIgnoreCase(">") || txtSIName.Text.ContainsIgnoreCase("|"))
            {
                txtSIName.Text = txtSIName.Text.ReplaceIgnoreCase("\\", "");
                txtSIName.Text = txtSIName.Text.ReplaceIgnoreCase("/", "");
                txtSIName.Text = txtSIName.Text.ReplaceIgnoreCase(":", "");
                txtSIName.Text = txtSIName.Text.ReplaceIgnoreCase("*", "");
                txtSIName.Text = txtSIName.Text.ReplaceIgnoreCase("?", "");
                txtSIName.Text = txtSIName.Text.ReplaceIgnoreCase("\"", "");
                txtSIName.Text = txtSIName.Text.ReplaceIgnoreCase("<", "");
                txtSIName.Text = txtSIName.Text.ReplaceIgnoreCase(">", "");
                txtSIName.Text = txtSIName.Text.ReplaceIgnoreCase("|", "");
            }
        }

        private void cmdSITop_Click(object sender, EventArgs e)
        {
            ListViewEx.LVE = GetSelectedList();
            if (ListViewEx.LVE.SelectedItems.Count != 1) { MessageBox.Show("You need to select one item to move, you have selected " + ListViewEx.LVE.SelectedItems.Count + ".", ListViewEx.LVE.SelectedItems.Count + " selected!"); return; }

            try
            {
                while (cMain.MoveListViewItem(ListViewEx.LVE, cMain.MoveDirection.Up) == false) ;
            }
            catch (Exception Ex) { }
        }

        private void cmdSIBottom_Click(object sender, EventArgs e)
        {
            ListViewEx.LVE = GetSelectedList();
            if (ListViewEx.LVE.SelectedItems.Count != 1) { MessageBox.Show("You need to select one item to move, you have selected " + ListViewEx.LVE.SelectedItems.Count + ".", ListViewEx.LVE.SelectedItems.Count + " selected!"); return; }

            try
            {
                while (cMain.MoveListViewItem(ListViewEx.LVE, cMain.MoveDirection.Down) == false) ;
            }
            catch (Exception Ex) { }
        }

        private void mnuA_Click(object sender, EventArgs e)
        {
        }

        private void mnuAdd_Click(object sender, EventArgs e)
        {

            fAddons = null;
            cAddon.AddonError = "";
            try
            {
                ToolStrip1.Enabled = false;
                TPMain.Enabled = false;
                PanArrange.Enabled = false;

                iAdding = true;

                string TabName = SelTab.Text;
                if (TabName.ContainsIgnoreCase("["))
                {
                    while (TabName.ContainsIgnoreCase("[")) { TabName = TabName.Substring(0, TabName.Length - 1); }
                    TabName = TabName.Substring(0, TabName.Length - 1);
                }

                switch (TabName)
                {
                    case "Silent Installs + SFX":
                        iAdding = false;
                        TPMain.Enabled = true;
                        PanSilent.Visible = true;
                        PanSilent.Enabled = true;
                        chkAS.Left = chkCopyF.Left + chkCopyF.Width + 6;
                        cMain.CenterObject(PanSilent);
                        cmdSIClear.PerformClick();

                        while (PanSilent.Visible)
                        {
                            Application.DoEvents();
                            Thread.Sleep(100);
                        }
                        break;
                    case "Files":
                        TPMain.Enabled = true;
                        panFiles.Visible = true;
                        panFiles.Enabled = true;
                        cMain.CenterObject(panFiles);
                        cmdFClear.PerformClick();

                        while (panFiles.Visible)
                        {
                            Application.DoEvents();
                            Thread.Sleep(100);
                        }
                        break;
                    case "Tweaks":
                        var RFD = new OpenFileDialog();

                        RFD.Title = "Browse for Registry Files...";
                        RFD.Filter = "Registry File *.reg| *.reg";
                        if (Directory.Exists(cOptions.fTweaks))
                        {
                            RFD.InitialDirectory = cOptions.fTweaks;
                        }

                        RFD.Multiselect = true;

                        if (RFD.ShowDialog() == DialogResult.OK)
                        {
                            cOptions.fTweaks = GetFolder(RFD.FileName);
                            int nRT = 0;
                            foreach (string TP in RFD.FileNames)
                            {
                                string TPN = TP;
                                while (TPN.ContainsIgnoreCase("\\"))
                                {
                                    TPN = TPN.Substring(1);
                                }

                                if (FindNode(tvTweaks, TPN) == null)
                                {
                                    nRT += 1;
                                }
                            }

                            cMain.UpdateToolStripLabel(lblStatus, "Adding Registry...");
                            int nG = 1;
                            tvTweaks.BeginUpdate();
                            foreach (string TP in RFD.FileNames)
                            {
                                try
                                {
                                    if (FindNode(tvTweaks, TP) == null)
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "Adding Registry (" + nG + " of " + nRT + "):" + TP);
                                        Application.DoEvents();
                                        var NI = new TreeNode();
                                        string TPN = TP;
                                        while (TPN.ContainsIgnoreCase("\\"))
                                        {
                                            TPN = TPN.Substring(1);
                                        }

                                        NI.Text = TPN;
                                        NI.Tag = "Reg";
                                        NI.Checked = true;
                                        NI.ToolTipText = TP;
                                        tvTweaks.Nodes.Add(NI);
                                        mnuR.Visible = true;
                                        nG += 1;
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show("Error trying to add Reg" + Environment.NewLine + TP +
                                                                Environment.NewLine + Ex.Message);
                                    cMain.WriteLog(this, "Error trying to add reg" + Environment.NewLine + TP,
                                                              Ex.Message, lblStatus.Text);
                                }
                            }

                            tvTweaks.EndUpdate();
                        }

                        break;
                    case "Addons":
                        var FAAddons = new OpenFileDialog();
                        FAAddons.Title = "Browse for Addons...";
                        FAAddons.Filter = "Win Toolkit Addons *.WA| *.WA";
                        FAAddons.Multiselect = true;
                        if (Directory.Exists(cOptions.fAddons))
                        {
                            FAAddons.InitialDirectory = cOptions.fAddons;
                        }

                        if (FAAddons.ShowDialog() == DialogResult.OK)
                        {
                            mnuClear.Visible = false;

                            cOptions.fAddons = GetFolder(FAAddons.FileName);
                            fAddons = FAAddons.FileNames;
                            cMain.UpdateToolStripLabel(lblStatus, "Adding Addons...");
                            if (!BWAddAddons.IsBusy) { BWAddAddons.RunWorkerAsync(); }
                        }
                        else
                        {
                            iAdding = false;
                            ToolStrip1.Enabled = true;
                            TPMain.Enabled = true;
                            PanArrange.Enabled = true;
                            if (lstAddons.Items.Count > 0)
                                mnuR.Visible = true;
                        }
                        break;
                    case "Theme Packs":
                        var TPD = new OpenFileDialog();
                        TPD.Title = "Browse for Theme Packs...";
                        TPD.Filter = "Theme Packs *.themepack; *.theme *.deskthemepack | *.themepack;*.theme;*.deskthemepack";
                        TPD.Multiselect = true;
                        if (Directory.Exists(cOptions.fThemes))
                        {
                            TPD.InitialDirectory = cOptions.fThemes;
                        }

                        if (TPD.ShowDialog() == DialogResult.OK)
                        {
                            cOptions.fThemes = GetFolder(TPD.FileName);
                            cMain.UpdateToolStripLabel(lblStatus, "Adding Theme Packs...");
                            lstThemes.BeginUpdate();

                            int nT = 1;
                            foreach (string TP in TPD.FileNames)
                            {
                                try
                                {
                                    string MD5 = cMain.MD5CalcFile(TP);
                                    if (lstThemes.FindItemWithText(TP) == null && (lstThemes.FindItemWithText(MD5) == null || string.IsNullOrEmpty(MD5)))
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "Adding Theme (" + nT + " of " + TPD.FileNames.Length + "):" + TP);
                                        // cMain.UpdateToolStripLabel(lblStatus, "Adding Theme: " + TP;
                                        Application.DoEvents();
                                        var NI = new ListViewItem();
                                        NI.Group = lstThemes.Groups[0];
                                        string TPN = TP;
                                        while (TPN.ContainsIgnoreCase("\\"))
                                        {
                                            TPN = TPN.Substring(1);
                                        }
                                        NI.Text = TPN;
                                        NI.SubItems.Add(cMain.GetSize(TP));
                                        NI.SubItems.Add(TP);
                                        NI.SubItems.Add("");
                                        lstThemes.Items.Add(NI);
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show("Error trying to add Theme" + Environment.NewLine + TP +
                                                                Environment.NewLine + Ex.Message);
                                    cMain.WriteLog(this, "Error trying to add theme" + Environment.NewLine + TP,
                                                              Ex.Message, lblStatus.Text);
                                }
                                nT += 1;
                            }

                        }
                        lstThemes.EndUpdate();
                        if (lstThemes.Items.Count > 0)
                        {
                            mnuR.Visible = true;
                            cmdDefaultTheme.Visible = true;
                        }
                        break;
                    case "Gadgets":
                        var OGD = new OpenFileDialog();
                        OGD.Title = "Browse for Windows Sidebar Gadgets...";
                        OGD.Filter = "Sidebar Gadgets *.Gadget| *.gadget";
                        OGD.Multiselect = true;
                        if (Directory.Exists(cOptions.fGadgets))
                        {
                            OGD.InitialDirectory = cOptions.fGadgets;
                        }

                        if (OGD.ShowDialog() == DialogResult.OK)
                        {
                            cOptions.fGadgets = GetFolder(OGD.FileName);
                            int nGT = OGD.FileNames.Count(TP => lstGadgets.FindItemWithText(TP) == null);

                            cMain.UpdateToolStripLabel(lblStatus, "Adding Gadgets...");
                            int nG = 1;
                            lstGadgets.BeginUpdate();
                            foreach (string TP in OGD.FileNames)
                            {
                                try
                                {
                                    string MD5 = cMain.MD5CalcFile(TP);
                                    if (lstGadgets.FindItemWithText(TP) == null && (lstGadgets.FindItemWithText(MD5) == null || string.IsNullOrEmpty(MD5)))
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "Adding Gadget (" + nG + " of " + nGT + "):" + TP);
                                        Application.DoEvents();
                                        var NI = new ListViewItem();
                                        NI.Group = lstGadgets.Groups[0];
                                        string TPN = TP;
                                        while (TPN.ContainsIgnoreCase("\\"))
                                        {
                                            TPN = TPN.Substring(1);
                                        }
                                        NI.Text = TPN;
                                        NI.SubItems.Add(cMain.GetSize(TP));
                                        NI.SubItems.Add(TP);
                                        lstGadgets.Items.Add(NI);
                                        nG += 1;
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show("Error trying to add Gadget" + Environment.NewLine + TP +
                                                                Environment.NewLine + Ex.Message);
                                    cMain.WriteLog(this, "Error trying to add gadget" + Environment.NewLine + TP,
                                                              Ex.Message, lblStatus.Text);
                                }
                            }

                            lstGadgets.EndUpdate();
                        }
                        if (lstGadgets.Items.Count > 0)
                            mnuR.Visible = true;
                        break;
                    case "Wallpapers":
                        var OWD = new OpenFileDialog();
                        OWD.Title = "Wallpapers...";
                        OWD.Filter = "JPG *.jpg|*.jpg|PNG *.png|*.png|All Files *.*|*.*";
                        OWD.Multiselect = true;
                        if (Directory.Exists(cOptions.fWallpapers))
                        {
                            OWD.InitialDirectory = cOptions.fWallpapers;
                        }

                        if (OWD.ShowDialog() == DialogResult.OK)
                        {
                            cOptions.fWallpapers = GetFolder(OWD.FileName);
                            int nGT = OWD.FileNames.Count(TP => lstWallpapers.FindItemWithText(TP) == null);

                            cMain.UpdateToolStripLabel(lblStatus, "Adding Wallpapers...");
                            int nG = 1;
                            lstWallpapers.BeginUpdate();
                            foreach (string TP in OWD.FileNames)
                            {
                                try
                                {
                                    string MD5 = cMain.MD5CalcFile(TP);

                                    if (lstWallpapers.FindItemWithText(TP) == null && (lstWallpapers.FindItemWithText(MD5) == null || string.IsNullOrEmpty(MD5)))
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "Adding Wallpaper (" + nG + " of " + nGT + "):" + TP);
                                        Application.DoEvents();
                                        var NI = new ListViewItem();
                                        NI.Group = lstWallpapers.Groups[0];
                                        string TPN = TP;
                                        while (TPN.ContainsIgnoreCase("\\"))
                                        {
                                            TPN = TPN.Substring(1);
                                        }
                                        NI.Text = TPN;
                                        NI.SubItems.Add(cMain.GetSize(TP));
                                        NI.SubItems.Add(TP);
                                        lstWallpapers.Items.Add(NI);
                                        nG += 1;
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    MessageBox.Show("Error trying to add Wallpaper" + Environment.NewLine + TP +
                                                                Environment.NewLine + Ex.Message);
                                    cMain.WriteLog(this, "Error trying to add wallpaper" + Environment.NewLine + TP,
                                                              Ex.Message, lblStatus.Text);
                                }
                            }

                            lstWallpapers.EndUpdate();

                            if (lstWallpapers.Items.Count == 0)
                                lstWallpapers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                            else
                                lstWallpapers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        }
                        if (lstWallpapers.Items.Count > 0)
                            mnuR.Visible = true;
                        break;
                    case "Updates + Languages":
                        UC = 0;
                        var OFD = new OpenFileDialog();
                        OFD.Multiselect = true;
                        OFD.Title = "Select Updates...";
                        OFD.Filter = "Windows Updates *.cab *.msu|*.cab;*.msu|IE/XP Mode *.exe|*.exe";
                        if (Directory.Exists(cOptions.fUpdates))
                        {
                            OFD.InitialDirectory = cOptions.fUpdates;
                        }

                        if (OFD.ShowDialog() == DialogResult.OK)
                        {
                            cOptions.fUpdates = GetFolder(OFD.FileName);
                            UC = 0;
                            int nU = 1;
                            int nUT = OFD.FileNames.Count(fUpd => lstUpdates.FindItemWithText(fUpd) == null);

                            lstUpdates.BeginUpdate();

                            badUpdates.Clear();

                            uError = "";
                            foreach (string fUpd in OFD.FileNames)
                            {
                                try
                                {
                                    if (lstUpdates.FindItemWithText(fUpd) == null && File.Exists(fUpd))
                                    {

                                        string MD5 = "";
                                        var item = Classes.UpdateCache.Find(Path.GetFileName(fUpd), cMain.GetSize(fUpd, false));
                                        if (item != null)
                                        {
                                            MD5 = item.MD5;
                                        }
                                        else
                                        {
                                            cMain.UpdateToolStripLabel(lblStatus, "(" + nU + "\\" + nUT + ") Calculating MD5: " + fUpd + "...");
                                            Application.DoEvents();
                                            MD5 = cMain.MD5CalcFile(fUpd);
                                        }

                                        if ((string.IsNullOrEmpty(MD5) || lstUpdates.FindItemWithText(MD5) == null) && lstUpdates.FindItemWithText(fUpd) == null)
                                        {
                                            cMain.UpdateToolStripLabel(lblStatus, "(" + nU + "\\" + nUT + ") Adding " + fUpd + "...");
                                            Application.DoEvents();
                                            if (fUpd.ToUpper().EndsWithIgnoreCase(".EXE"))
                                            {
                                                AddEXE(fUpd);
                                            }

                                            if ((fUpd.ToUpper().EndsWithIgnoreCase(".CAB") || fUpd.ToUpper().EndsWithIgnoreCase(".MSU")))
                                            {
                                                if (fUpd.ContainsIgnoreCase("KB947821")) { cMain.UpdateToolStripLabel(lblStatus, "Adding: " + fUpd + ", this update may take a while..."); }
                                                Application.DoEvents();
                                                try
                                                {
                                                    if (fUpd.ToUpper().EndsWithIgnoreCase(".CAB"))
                                                        AddToList(fUpd, "update.mum", MD5);
                                                    if (fUpd.ToUpper().EndsWithIgnoreCase(".MSU"))
                                                        AddToList(fUpd, "*.txt", MD5);
                                                }
                                                catch (Exception ex)
                                                {
                                                    uError += "Unknown Error: '" + fUpd + "' - " + ex.Message + Environment.NewLine;
                                                    UC += 1;
                                                    new LargeError("Error Adding Update #1", "An error occurred whilst trying to add: " + fUpd, lblStatus.Text, ex).Upload();
                                                }

                                                nU = nU + 1;

                                            }
                                            cMain.FreeRAM();
                                        }
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    LargeError LE = new LargeError("Error Adding Update #2", "An error occurred whilst trying to add: " + fUpd, lblStatus.Text, Ex);
                                    LE.Upload(); LE.ShowDialog();
                                }
                            }

                            lstUpdates.EndUpdate();
                        }

                        if (UC > 0 && !string.IsNullOrEmpty(uError))
                        {
                            cMain.ErrorBox("More information is available by clicking the '>> Details' button.", UC + " update(s) skipped.", uError);
                        }

                        BadUpdates();



                        if (lstUpdates.Items.Count > 0) { mnuR.Visible = true; }
                        break;
                    case "Drivers":
                        string AMF = cMain.FolderBrowserVista("Drivers...", false, true, cOptions.fDrivers);

                        if (!string.IsNullOrEmpty(AMF))
                        {
                            cOptions.fDrivers = AMF;

                            cMain.UpdateToolStripLabel(lblStatus, "Counting drivers...");
                            Application.DoEvents();
                            cMain.FreeRAM();

                            int dCurrent = 1;

                            dErr = "";

                            List<string> AllDri = cMain.SearchDirectory(AMF, "*.inf");

                            DirectoryInfo dirInfo = new DirectoryInfo(AMF);

                            List<string> InsDri;
                            if (lstOptions.FindItemWithText("Remove Invalid Drivers", false, 0).Checked == true)
                            {

                                if (cMain.selectedImages[0].Architecture == Architecture.x64)
                                {
                                    InsDri = AllDri.Where(d => !Path.GetDirectoryName(d).ContainsIgnoreCase("X86")).ToList();
                                    dErr = AllDri.Where(d => Path.GetDirectoryName(d).ContainsIgnoreCase("X86")).Aggregate(dErr, (current, si) => current + "Driver Skipped: " + si + "\r\n");
                                }
                                else
                                {
                                    InsDri = AllDri.Where(d => !Path.GetDirectoryName(d).ContainsIgnoreCase("X64")).ToList();
                                    dErr = AllDri.Where(d => Path.GetDirectoryName(d).ContainsIgnoreCase("X64")).Aggregate(dErr, (current, si) => current + "Driver Skipped: " + si + "\r\n");
                                }
                            }
                            else
                            {
                                InsDri = AllDri;
                            }


                            lstDrivers.BeginUpdate();

                            foreach (string S in InsDri)
                            {
                                try
                                {
                                    if (lstDrivers.FindItemWithText(S) == null && File.Exists(S))
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "(" + dCurrent + "\\" + InsDri.Count.ToString() + ") Adding: " +
                                                                          S + "...");
                                        AddDriver(S);
                                        dCurrent += 1;
                                    }
                                }
                                catch (Exception Ex)
                                {
                                    new SmallError("Error adding driver [outer]", Ex, S).Upload();
                                }
                            }


                            InvalDrivers();
                            lstDrivers.EndUpdate();

                            cMain.UpdateToolStripLabel(lblStatus, "Checking for errors...");


                            if (!string.IsNullOrEmpty(dErr))
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Waiting for user...");
                                cMain.ErrorBox("Some drivers failed to add or have been ignored. Click 'Details' for more information.", "Drivers Skipped", dErr);
                                dErr = "";
                            }

                            if (AMF.Length <= 3 && lstOptions.FindItemWithText("Force Unsigned").Checked)
                            {
                                MessageBox.Show(
                                      "Please be careful when selecting a drive (" + AMF +
                                      "), remember DISM will integrate all the drivers in all the subfolders too and may take a while. If you have kept your drivers on a different partition then this is fine :)",
                                      "Root2 Drive Detected!");
                            }
                        }

                        cMain.UpdateToolStripLabel(lblStatus, "Incompatible and duplicate drivers have not been added to the list.");
                        if (lstDrivers.Items.Count > 0)
                            mnuR.Visible = true;
                        break;
                }
                if (SelTab.Text != "Drivers") { cMain.UpdateToolStripLabel(lblStatus, ""); }

                if (SelTab.Text != "Addons")
                {
                    iAdding = false;
                    ToolStrip1.Enabled = true;
                    TPMain.Enabled = true;
                    PanArrange.Enabled = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error adding item" + Environment.NewLine + SelTab + Environment.NewLine + Ex.Message);
                cMain.WriteLog(this, "Error adding item" + Environment.NewLine + SelTab, Ex.Message, lblStatus.Text);
            }

            cMain.UpdateToolStripLabel(lblStatus, "Saving settings...");
            cOptions.SaveSettings();

            EnablePan();
            ListView SL = GetSelectedList();
            if (SL != null && (SL is ListView || SL is ListViewEx))
            {
                if (SL.Items.Count > 0) { SL.Items[0].EnsureVisible(); }
                ListViewEx.LVE = GetSelectedList();
            }
            UpdateNames(true);
            cMain.UpdateToolStripLabel(lblStatus, "Got everything added? Click 'Start' when you're ready.");
            cMain.FreeRAM();
        }

        private void mnuAddS_Click(object sender, EventArgs e)
        {
            fAddons = null;
            string TabName = SelTab.Text;
            if (TabName.ContainsIgnoreCase("["))
            {
                while (TabName.ContainsIgnoreCase("[")) { TabName = TabName.Substring(0, TabName.Length - 1); }
                TabName = TabName.Substring(0, TabName.Length - 1);
            }

            string sDir = "";
            switch (TabName)
            {
                case "Addons":
                    sDir = cOptions.fAddons;
                    break;
                case "Gadgets":
                    sDir = cOptions.fGadgets;
                    break;
                case "Theme Packs":
                    sDir = cOptions.fThemes;
                    break;
                case "Tweaks":
                    sDir = cOptions.fTweaks;
                    break;
                case "Updates + Languages":
                    sDir = cOptions.fUpdates;
                    break;
                case "Wallpapers":
                    sDir = cOptions.fWallpapers;
                    break;
            }
            if (!string.IsNullOrEmpty(sDir) && !Directory.Exists(sDir)) { sDir = ""; }

            string SFolder = "";

            SFolder = cMain.FolderBrowserVista(mnuAdd.Text, false, true, sDir);

            if (string.IsNullOrEmpty(SFolder) || !Directory.Exists(SFolder)) { return; }

            badUpdates.Clear();
            try
            {
                ToolStrip1.Enabled = false;
                TPMain.Enabled = false;
                PanArrange.Enabled = false;

                iAdding = true;

                switch (TabName)
                {
                    case "Addons":
                        mnuClear.Visible = false;
                        cOptions.fAddons = SFolder;
                        cMain.UpdateToolStripLabel(lblStatus, "Finding Addons...");
                        fAddons = Directory.GetFiles(SFolder, "*.WA", SearchOption.AllDirectories);
                        cMain.UpdateToolStripLabel(lblStatus, "Adding Addons...");
                        if (!BWAddAddons.IsBusy) { BWAddAddons.RunWorkerAsync(); }
                        return;
                    case "Gadgets":
                        cMain.UpdateToolStripLabel(lblStatus, "Finding Gadgets...");
                        Application.DoEvents();
                        var fGadgets = Directory.GetFiles(SFolder, "*.gadget", SearchOption.AllDirectories).Where(S => S.ToUpper().EndsWithIgnoreCase(".GADGET") && lstGadgets.FindItemWithText(S) == null).ToList();

                        cOptions.fGadgets = SFolder;

                        cMain.UpdateToolStripLabel(lblStatus, "Adding Gadgets...");
                        int nG = 1;
                        lstGadgets.BeginUpdate();
                        foreach (string TP in fGadgets)
                        {
                            try
                            {
                                if (lstGadgets.FindItemWithText(TP) == null)
                                {
                                    cMain.UpdateToolStripLabel(lblStatus, "Adding Gadget (" + nG + " of " + fGadgets.Count + "):" + TP);
                                    Application.DoEvents();
                                    var NI = new ListViewItem();
                                    NI.Group = lstGadgets.Groups[0];
                                    string TPN = TP;
                                    while (TPN.ContainsIgnoreCase("\\"))
                                    {
                                        TPN = TPN.Substring(1);
                                    }
                                    NI.Text = TPN;
                                    NI.SubItems.Add(cMain.GetSize(TP));
                                    NI.SubItems.Add(TP);
                                    lstGadgets.Items.Add(NI);
                                    nG += 1;
                                }
                            }
                            catch (Exception Ex)
                            {
                                MessageBox.Show("Error trying to add Gadget" + Environment.NewLine + TP +
                                                            Environment.NewLine + Ex.Message);
                                cMain.WriteLog(this, "Error trying to add gadget" + Environment.NewLine + TP,
                                                          Ex.Message, lblStatus.Text);
                            }

                            lstGadgets.EndUpdate();
                        }
                        if (lstGadgets.Items.Count > 0)
                            mnuR.Visible = true;
                        break;
                    case "Theme Packs":
                        cMain.UpdateToolStripLabel(lblStatus, "Finding Themes...");
                        Application.DoEvents();
                        var fThemes = Directory.GetFiles(SFolder, "*.*themepack", SearchOption.AllDirectories).Where(S => (S.ToUpper().EndsWithIgnoreCase(".THEMEPACK") || S.ToUpper().EndsWithIgnoreCase(".DESKTHEMEPACK")) && lstThemes.FindItemWithText(S) == null).ToList();
                        cOptions.fThemes = SFolder;
                        cMain.UpdateToolStripLabel(lblStatus, "Adding Theme Packs...");
                        lstThemes.BeginUpdate();
                        int nT = 1;
                        foreach (string TP in fThemes)
                        {
                            try
                            {
                                if (lstThemes.FindItemWithText(TP) == null)
                                {
                                    cMain.UpdateToolStripLabel(lblStatus, "Adding Theme (" + nT + " of " + fThemes.Count + "):" + TP);
                                    Application.DoEvents();
                                    var NI = new ListViewItem();
                                    NI.Group = lstThemes.Groups[0];
                                    string TPN = TP;
                                    while (TPN.ContainsIgnoreCase("\\"))
                                    {
                                        TPN = TPN.Substring(1);
                                    }
                                    NI.Text = TPN;
                                    NI.SubItems.Add(cMain.GetSize(TP));
                                    NI.SubItems.Add(TP);
                                    NI.SubItems.Add("");
                                    lstThemes.Items.Add(NI);
                                }
                            }
                            catch (Exception Ex)
                            {
                                MessageBox.Show("Error trying to add Theme" + Environment.NewLine + TP +
                                                            Environment.NewLine + Ex.Message);
                                cMain.WriteLog(this, "Error trying to add theme" + Environment.NewLine + TP,
                                                          Ex.Message, lblStatus.Text);
                            }

                            nT += 1;
                        }
                        lstThemes.EndUpdate();
                        if (lstThemes.Items.Count > 0)
                        {
                            mnuR.Visible = true;
                            cmdDefaultTheme.Visible = true;
                        }
                        break;
                    case "Tweaks":
                        cMain.UpdateToolStripLabel(lblStatus, "Finding Regs...");
                        Application.DoEvents();
                        var fRegs = Directory.GetFiles(SFolder, "*.reg", SearchOption.AllDirectories).Where(S => S.ToUpper().EndsWithIgnoreCase(".REG") && FindNode(tvTweaks, cMain.GetFName(S)) == null).ToList();
                        cOptions.fTweaks = SFolder;

                        cMain.UpdateToolStripLabel(lblStatus, "Adding Registry...");
                        int nTW = 1;
                        tvTweaks.BeginUpdate();
                        foreach (string TP in fRegs)
                        {
                            try
                            {
                                if (FindNode(tvTweaks, TP) == null)
                                {
                                    cMain.UpdateToolStripLabel(lblStatus, "Adding Registry (" + nTW + " of " + fRegs.Count + "):" + TP);
                                    Application.DoEvents();
                                    var NI = new TreeNode();
                                    string TPN = TP;
                                    while (TPN.ContainsIgnoreCase("\\"))
                                    {
                                        TPN = TPN.Substring(1);
                                    }

                                    NI.Text = TPN;
                                    NI.Tag = "Reg";
                                    NI.Checked = true;
                                    NI.ToolTipText = TP;
                                    tvTweaks.Nodes.Add(NI);
                                    mnuR.Visible = true;
                                    nTW += 1;
                                }
                            }
                            catch (Exception Ex)
                            {
                                MessageBox.Show("Error trying to add Reg" + Environment.NewLine + TP + Environment.NewLine + Ex.Message);
                                cMain.WriteLog(this, "Error trying to add reg" + Environment.NewLine + TP, Ex.Message, lblStatus.Text);
                            }

                            tvTweaks.EndUpdate();
                        }

                        break;
                    case "Updates + Languages":
                        UC = 0;

                        cMain.UpdateToolStripLabel(lblStatus, "Finding Updates...");
                        Application.DoEvents();

                        bool bAdditionals = false;
                        if (Directory.Exists(SFolder + "\\ADDITIONAL\\"))
                        {
                            DialogResult DR = MessageBox.Show("Some of those updates may cause errors during install!\n\nWould you like to add 'Additional' updates to the list?", "Additional's?", MessageBoxButtons.YesNo);
                            if (DR == System.Windows.Forms.DialogResult.Yes) { bAdditionals = true; }
                        }
                        var fUpdate = new List<string>();

                        if (Directory.Exists(SFolder))
                        {
                            foreach (string S in Directory.GetFiles(SFolder, "*.*", SearchOption.AllDirectories))
                            {
                                if (S == SFolder + "\\ADDITIONAL\\" && bAdditionals == false) { continue; }
                                if (S == SFolder + "\\OLD\\") { continue; }
                                if ((S.ToUpper().EndsWithIgnoreCase(".MSU") || S.ToUpper().EndsWithIgnoreCase(".CAB")) && lstUpdates.FindItemWithText(S) == null) { fUpdate.Add(S); }
                            }
                        }

                        cOptions.fUpdates = SFolder;
                        int nU = 1;

                        cMain.UpdateToolStripLabel(lblStatus, "Adding Updates...");
                        Application.DoEvents();

                        lstUpdates.BeginUpdate();

                        uError = "";
                        foreach (string fUpd in fUpdate)
                        {
                            try
                            {
                                if (fUpd == SFolder + "\\ADDITIONAL\\" && bAdditionals == false) { continue; }
                                if (fUpd == SFolder + "\\OLD\\") { continue; }
                                if (File.Exists(fUpd) && lstUpdates.FindItemWithText(fUpd) == null)
                                {

                                    string MD5 = "";
                                    var item = Classes.UpdateCache.Find(Path.GetFileName(fUpd), cMain.GetSize(fUpd, false));
                                    if (item != null)
                                    {
                                        MD5 = item.MD5;
                                    }
                                    else
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "(" + nU + "\\" + fUpdate.Count + ") Calculating MD5: " + fUpd + "...");
                                        Application.DoEvents();
                                        MD5 = cMain.MD5CalcFile(fUpd);
                                    }

                                    if ((string.IsNullOrEmpty(MD5) || lstUpdates.FindItemWithText(MD5) == null) && lstUpdates.FindItemWithText(fUpd) == null)
                                    {
                                        cMain.UpdateToolStripLabel(lblStatus, "(" + nU + "\\" + fUpdate.Count + ") Adding " + fUpd + "...");
                                        Application.DoEvents();
                                        if (fUpd.ToUpper().EndsWithIgnoreCase(".EXE"))
                                        {
                                            AddEXE(fUpd);
                                        }

                                        if (fUpd.ToUpper().EndsWithIgnoreCase(".CAB") || fUpd.ToUpper().EndsWithIgnoreCase(".MSU"))
                                        {
                                            if (fUpd.ContainsIgnoreCase("KB947821")) { cMain.UpdateToolStripLabel(lblStatus, "Adding: " + fUpd + ", this update may take a while..."); }
                                            Application.DoEvents();
                                            try
                                            {
                                                if (fUpd.ToUpper().EndsWithIgnoreCase(".CAB"))
                                                    AddToList(fUpd, "update.mum", MD5);
                                                if (fUpd.ToUpper().EndsWithIgnoreCase(".MSU"))
                                                    AddToList(fUpd, "*.txt", MD5);
                                            }
                                            catch (Exception ex)
                                            {
                                                new SmallError("Error trying to add update [inner]", ex, fUpd + "\nStatus: " + lblStatus).Upload();
                                            }

                                            nU = nU + 1;
                                        }
                                        cMain.FreeRAM();
                                    }
                                }
                            }
                            catch (Exception Ex)
                            {
                                MessageBox.Show("Error trying to add Update" + Environment.NewLine + fUpd + Environment.NewLine + Ex.Message);
                                new SmallError("Error trying to add update [outter]", Ex, fUpd + "\nStatus: " + lblStatus).Upload();
                            }


                            BadUpdates();
                        }

                        lstUpdates.EndUpdate();
                        if (lstUpdates.Items.Count > 0) { mnuR.Visible = true; }
                        break;
                    case "Wallpapers":
                        cMain.UpdateToolStripLabel(lblStatus, "Finding Wallpapers...");
                        Application.DoEvents();
                        var fWallpaper = Directory.GetFiles(SFolder, "*.*", SearchOption.AllDirectories).Where(S => (S.ToUpper().EndsWithIgnoreCase(".JPG") || S.ToUpper().EndsWithIgnoreCase(".PNG")) && lstWallpapers.FindItemWithText(S) == null).ToList();

                        cOptions.fWallpapers = SFolder;

                        cMain.UpdateToolStripLabel(lblStatus, "Adding Wallpapers...");
                        int nW = 1;
                        lstWallpapers.BeginUpdate();
                        foreach (string TP in fWallpaper)
                        {
                            try
                            {
                                if (lstWallpapers.FindItemWithText(TP) == null)
                                {
                                    cMain.UpdateToolStripLabel(lblStatus, "Adding Wallpaper (" + nW + " of " + fWallpaper.Count + "):" + TP);
                                    Application.DoEvents();
                                    var NI = new ListViewItem();
                                    NI.Group = lstWallpapers.Groups[0];
                                    string TPN = TP;
                                    while (TPN.ContainsIgnoreCase("\\"))
                                    {
                                        TPN = TPN.Substring(1);
                                    }
                                    NI.Text = TPN;
                                    NI.SubItems.Add(cMain.GetSize(TP));
                                    NI.SubItems.Add(TP);
                                    lstWallpapers.Items.Add(NI);
                                    nW += 1;
                                }
                            }
                            catch (Exception Ex)
                            {
                                MessageBox.Show("Error trying to add Wallpaper" + Environment.NewLine + TP +
                                                            Environment.NewLine + Ex.Message);
                                cMain.WriteLog(this, "Error trying to add wallpaper" + Environment.NewLine + TP,
                                                          Ex.Message, lblStatus.Text);
                            }
                        }

                        lstWallpapers.EndUpdate();

                        if (lstWallpapers.Items.Count > 0)
                            mnuR.Visible = true;
                        break;
                }

                if (SelTab.Text != "Drivers") { cMain.UpdateToolStripLabel(lblStatus, ""); }

                if (SelTab.Text != "Addons")
                {
                    iAdding = false;
                    ToolStrip1.Enabled = true;
                    TPMain.Enabled = true;
                    PanArrange.Enabled = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error adding item + sub" + Environment.NewLine + SelTab + Environment.NewLine + Ex.Message);
                cMain.WriteLog(this, "Error adding item + sub" + Environment.NewLine + SelTab, Ex.Message, lblStatus.Text);
            }

            cMain.UpdateToolStripLabel(lblStatus, "Saving settings...");
            cOptions.SaveSettings();

            EnablePan();
            ListView SL = GetSelectedList();
            if (SL != null && (SL is ListView || SL is ListViewEx))
            {
                if (SL.Items.Count > 0) { SL.Items[0].EnsureVisible(); }
            }
            iAdding = false;
            UpdateNames(true);
            cMain.UpdateToolStripLabel(lblStatus, "");
            cMain.FreeRAM();

        }

        private void chkCopyF_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void mnuSaveLoad_Click(object sender, EventArgs e)
        {
            mnuILoad.Text = "Load '" + GetSelectedTab().Text + "'";
            mnuISave.Text = "Save '" + GetSelectedTab().Text + "'";
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {
            if ((TPAdvanced.SelectedTab == tabComponents || TPAdvanced.SelectedTab == tabComponents2) && TPMain.SelectedTab == TabAdvanced)
            {
                MessageBox.Show("Green: These removals have been tested and are safe to remove.\n\nYellow: Not tested (Caution)\n\nRed: Not Recommended to remove. These have been known to break other features.", "Help");
            }

            if ((TPBasic.SelectedTab == TabUpdates) && TPMain.SelectedTab == TabBasic)
            {
                MessageBox.Show("Put your mouse over an item to see more details.", "Help");
            }
        }

        private void BWExtract_DoWork(object sender, DoWorkEventArgs e)
        {
            if (uFile.ToUpper().EndsWithIgnoreCase(".MSU"))
            {
                cMain.ExtractFiles(uFile, cOptions.WinToolkitTemp + "\\cInst" + uIndex + "_MSU", this, "*.cab");
                Files.DeleteFile(cOptions.WinToolkitTemp + "\\cInst" + uIndex + "_MSU\\WSUSSCAN.cab");
                if (!BWRun.CancellationPending && !bAbort)
                {
                    foreach (string FileC in Directory.GetFiles(cOptions.WinToolkitTemp + "\\cInst" + uIndex + "_MSU", "*.cab"))
                    {
                        cMain.ExtractFiles(FileC, cOptions.WinToolkitTemp + "\\cInst" + uIndex + "_CAB", this);
                        Files.DeleteFile(FileC);
                    }
                }
                Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst" + uIndex + "_MSU", false);
            }

            if (uFile.ToUpper().EndsWithIgnoreCase(".CAB"))
            {
                cMain.ExtractFiles(uFile, cOptions.WinToolkitTemp + "\\cInst" + uIndex + "_CAB", this);
            }

            if (!File.Exists(cOptions.WinToolkitTemp + "\\cInst" + uIndex + "_CAB\\update-bf.mum"))
            {
                Files.DeleteFolder(cOptions.WinToolkitTemp + "\\cInst" + uIndex + "_CAB", false);
            }
            else
            {
                lblStatus.Text += " [LDR Detected]";
            }

        }

        private void cmsUMSPrequisites_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LST in lstUpdates.SelectedItems)
            {
                if (LST.Group != lstUpdates.Groups[1])
                {
                    ListViewItem NI = LST;
                    LST.Remove();
                    NI.Group = lstUpdates.Groups[1];
                    lstUpdates.Items.Insert(lstUpdates.Groups[1].Items.Count - 1, NI);
                }
            }
        }

        private void cmsUMSUpdates_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LST in lstUpdates.SelectedItems)
            {
                if (LST.Group != lstUpdates.Groups[2])
                {
                    ListViewItem NI = (ListViewItem)LST.Clone();
                    NI.Group = lstUpdates.Groups[2];
                    lstUpdates.Items.Add(NI);
                    LST.Remove();
                }
            }
        }

        private void BadUpdates()
        {
            if (badUpdates.Count > 0)
            {
                using (frmUpdateSilents US = new frmUpdateSilents(badUpdates))
                {
                    US.ShowDialog();

                    foreach (ListViewItem lvi in US.Keep)
                    {
                        lvi.Group = lstUpdates.Groups[6];
                        lstUpdates.Items.Add(lvi);
                    }
                    foreach (ListViewItem lvi in US.Move)
                    {
                        var LST = new ListViewItem();
                        string oFileName = lvi.SubItems[5].Text;

                        LST.Text = lvi.Text;
                        if (string.IsNullOrEmpty(LST.Text))
                        {
                            LST.Text = Path.GetFileNameWithoutExtension(oFileName);
                        }

                        LST.SubItems.Add("N/A (Not Needed)");
                        LST.SubItems.Add("NO");
                        LST.SubItems.Add(cMain.GetSize(oFileName, true));
                        LST.SubItems.Add(oFileName);
                        LST.Group = lstSilent.Groups[0];
                        lstSilent.Items.Add(LST);
                        // lstUpdates.Items.Add(lvi);
                    }
                }

                badUpdates.Clear();
            }
        }

        private void cmsUpdates_Opening(object sender, CancelEventArgs e)
        {
            if (BWRun.IsBusy) { e.Cancel = true; }
        }

        private void lstUpdates_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmsUMSInternetExplorer_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LST in lstUpdates.SelectedItems)
            {
                if (LST.Group != lstUpdates.Groups[3])
                {
                    ListViewItem NI = (ListViewItem)LST.Clone();
                    NI.Group = lstUpdates.Groups[3];
                    lstUpdates.Items.Add(NI);
                    LST.Remove();
                }
            }
        }

        private void mnuMoveToSilent_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LST in lstUpdates.SelectedItems)
            {
                if (lstSilent.FindItemWithText(LST.SubItems[5].Text) == null && (lstSilent.FindItemWithText(LST.SubItems[6].Text) == null || string.IsNullOrEmpty(LST.SubItems[6].Text)))
                {
                    var LST2 = new ListViewItem();

                    LST2.Text = LST.Text;
                    LST2.SubItems.Add("N/A (Not Needed)");
                    LST2.SubItems.Add("NO");
                    LST2.SubItems.Add(LST.SubItems[3].Text);
                    LST2.SubItems.Add(LST.SubItems[5].Text);
                    LST2.Group = lstSilent.Groups[0];
                    lstSilent.Items.Add(LST2);
                    LST.Remove();
                    UpdateNames(false);
                }
            }
        }

        private void cmsDrivers_Opening(object sender, CancelEventArgs e)
        {
            if (lstDrivers.SelectedItems.Count == 0) { e.Cancel = true; }

        }

        private void cmsDIntegrateBoot_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LST in lstDrivers.SelectedItems)
            {
                if (LST.ImageIndex == -1)
                {
                    LST.ImageIndex = 0;
                    LST.ToolTipText = "MD5: " + LST.SubItems[9].Text + "\nThis driver will also be integrated into the boot.wim";
                }
                else
                {
                    LST.ImageIndex = -1;
                    LST.ToolTipText = "MD5: " + LST.SubItems[9].Text;
                }

            }

        }

        private void lstComponents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdPreset_Click(object sender, EventArgs e)
        {
            LoadPresetManager();
        }

        private void lstOptions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmCommon_Click(object sender, EventArgs e)
        {
            using (var common = new frmCommon())
            {
                common.Width = this.Width - 200;
                common.Height = this.Height - 200;
                if (common.ShowDialog() == DialogResult.OK)
                {
                    txtSISwitch.Text = common.SelectedSyntax;
                    if (string.IsNullOrEmpty(txtSIName.Text))
                    {
                        txtSIName.Text = common.SelectedName;
                    }
                }
            }
        }

        private void cmdDeleteInstallers_Click(object sender, EventArgs e)
        {
            ListViewItem oldInstallers = lstOptions.FindItemWithText("Delete Silent Installers");
            if (!oldInstallers.Checked)
            {
                oldInstallers.Checked = true;
            }
            else
            {
                oldInstallers.Checked = false;
            }
        }

        private void cmdSIDirInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "%DVD% represents the DVD drive.\n%APP% repesents the installer directory.\n\nExamples:\n/silent /config:%DVD%\\ConfigFolder\\Config.ini\n/config:%APP%\\OfficeConfig.xml",
                "Examples");
        }

    }
}