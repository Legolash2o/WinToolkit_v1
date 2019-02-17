using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;
using File = System.IO.File;

namespace WinToolkit
{
    public partial class frmAddonMaker : Form
    {
        private readonly string ATemp = cOptions.WinToolkitTemp + "\\AddonC";
        private readonly SaveFileDialog SFD = new SaveFileDialog();
        private ListView L;

        public frmAddonMaker()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            FormClosing += frmAddonMaker_FormClosing;
            FormClosed += frmAddonMaker_FormClosed;
            Activated += cMain.FormActivated;
            Deactivate += cMain.FormActivated;
            BWMaker.RunWorkerCompleted += BWMaker_RunWorkerCompleted;
            CheckForIllegalCrossThreadCalls = false;
            MouseWheel += MouseScroll;
            cboCT.DrawMode = DrawMode.OwnerDrawFixed;
            cboCT.DrawItem += cboCT_DrawItem;
            cboCT.DropDownClosed += cboCT_DropDownClosed;
            toolTip1.ToolTipIcon = ToolTipIcon.Info;
        }

        private void MouseScroll(object sender, MouseEventArgs e)
        {
            switch (TabControl1.SelectedTab.Text)
            {
                case "Copy Files":
                    lstCF.Select();
                    break;
                case "Copy Folders":
                    lstFolders.Select();
                    break;
                case "Import Registry":
                    lstReg.Select();
                    break;
                case "Delete":
                    lstD.Select();
                    break;
                case "Shortcut":
                    lstShortcut.Select();
                    break;
                case "Commands":
                    lstCommands.Select();
                    break;
                case "Variables":
                    lstAV.Select();
                    break;
            }
        }

        readonly ToolTip toolTip1 = new ToolTip();
        private void cboCT_DropDownClosed(object sender, EventArgs e)
        {
            toolTip1.Hide(cboCT);
        }

        private void cboCT_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                string text = cboCT.GetItemText(cboCT.Items[e.Index]);
                e.DrawBackground();
                using (var br = new SolidBrush(e.ForeColor)) { e.Graphics.DrawString(text, e.Font, br, e.Bounds); }
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) { toolTip1.ToolTipTitle = text; toolTip1.Show(lstAV.FindItemWithText(text, false, 0).SubItems[1].Text, cboCT, cboCT.Width + 10, e.Bounds.Bottom); }
                e.DrawFocusRectangle();
            }
            catch { }
        }

        private void frmAddonMaker_Load(object sender, EventArgs e)
        {
            SCMain.Scale4K(_4KHelper.Panel.Pan1);
            SCCommands.Scale4K(_4KHelper.Panel.Pan2);
            SCDelete.Scale4K(_4KHelper.Panel.Pan2);
            SCFiles.Scale4K(_4KHelper.Panel.Pan2);
            SCFolders.Scale4K(_4KHelper.Panel.Pan2);
            SCRegistry.Scale4K(_4KHelper.Panel.Pan2);
            SCShortcut.Scale4K(_4KHelper.Panel.Pan2);

            cMain.FormIcon(this); cMain.eLBL = lblStatus; cMain.eForm = this;
            cMain.ToolStripIcons(ToolStrip1);
            cMain.ToolStripIcons(ToolStrip2);
            cMain.ToolStripIcons(ToolStrip3);
            cMain.ToolStripIcons(ToolStrip4);
            cMain.ToolStripIcons(ToolStrip5);
            cMain.ToolStripIcons(ToolStrip6);
            cMain.ToolStripIcons(ToolStrip7);
            cMain.ToolStripIcons(ToolStrip8);
            cMain.FreeRAM();
            lstAV.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            lstCF.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lstCommands.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lstFolders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lstReg.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lstShortcut.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lstD.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void frmAddonMaker_FormClosing(object sender, FormClosingEventArgs e)
        {
            cMain.OnFormClosing(this);
            if (BWMaker.IsBusy)
            {
                MessageBox.Show("You can't close this window while your addon is being created!", "Please Wait!");
                e.Cancel = true;
            }
        }

        private void frmAddonMaker_Shown(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem item in lstAV.Items)
                {
                    cboCT.Items.Add(item.Text);
                }
            }
            catch (Exception Ex)
            {
                LargeError LE = new LargeError("Variable Additions", "Unable to add variable to cboCT.", lblStatus.Text, Ex);
                LE.Upload(); LE.ShowDialog();
            }
        }

        private void frmAddonMaker_FormClosed(object sender, FormClosedEventArgs e)
        {
            cMain.ReturnME();
        }

        private void BWMaker_DoWork(object sender, DoWorkEventArgs e)
        {


            try
            {
                cMain.UpdateToolStripLabel(lblStatus, "Deleting previous Addon Creator Temp Files...");
                if (!Directory.Exists(cOptions.WinToolkitTemp + "\\AddonC\\")) { cMain.CreateDirectory(cOptions.WinToolkitTemp + "\\AddonC\\"); }

                try
                {
                    if (lstShortcut.Items.Count > 0)
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Creating Shortcuts...");
                        //var shell2 = new WshShell();

                        foreach (ListViewItem SC in lstShortcut.Items)
                        {
                            foreach (string sSC in SC.SubItems[5].Text.Split('|'))
                            {


                                try
                                {
                                    var shell2 = new IWshRuntimeLibrary.WshShell();
                                    var link = (IWshRuntimeLibrary.IWshShortcut)
                                         shell2.CreateShortcut(cOptions.WinToolkitTemp + "\\AddonC\\" +
                                                                      cMain.GetFName(sSC));
                                    link.Description = SC.Text;
                                    if (!string.IsNullOrEmpty(SC.SubItems[1].Text))
                                        link.IconLocation = SC.SubItems[1].Text;
                                    if (!string.IsNullOrEmpty(SC.SubItems[2].Text)) link.Hotkey = SC.SubItems[2].Text;
                                    if (!string.IsNullOrEmpty(SC.SubItems[3].Text))
                                        link.TargetPath = SC.SubItems[3].Text;
                                    if (!string.IsNullOrEmpty(SC.SubItems[4].Text))
                                        link.Arguments = SC.SubItems[4].Text;

                                    string T = SC.SubItems[3].Text;
                                    while (!T.EndsWithIgnoreCase("\\") && !string.IsNullOrEmpty(T))
                                    {
                                        T = T.Substring(0, T.Length - 1);
                                    }
                                    link.WorkingDirectory = T;

                                    link.Save();

                                    var NCF = new ListViewItem();
                                    NCF.Text = cMain.GetFName(sSC);
                                    NCF.SubItems.Add(Path.GetDirectoryName(sSC));
                                    NCF.SubItems.Add(cOptions.WinToolkitTemp + "\\AddonC\\" +
                                                          cMain.GetFName(sSC));
                                    NCF.Tag = "Shortcut";
                                    lstCF.Items.Add(NCF);
                                }
                                catch (Exception Ex)
                                {
                                    LargeError LE = new LargeError("Shortcut Creation", "Error creating shortcut: " + SC.Text, lblStatus.Text + Environment.NewLine + "Destination: " + cboDestination.Text);
                                    LE.Upload(); LE.ShowDialog();
                                }
                            }
                        }
                    }
                }
                catch
                {
                }

                try
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Creating Tasks.txt [Main]");
                    Application.DoEvents();
                    string s = "Name=" + lblName.Text + Environment.NewLine;
                    s += "Creator=" + lblCreator.Text + Environment.NewLine;
                    s += "Version=" + lblVersion.Text + Environment.NewLine;

                    switch (cboArc.Text)
                    {
                        case "x86 only":
                            s += "Arc=x86!" + Environment.NewLine;
                            break;
                        default:
                            s += "Arc=" + cboArc.Text + Environment.NewLine;
                            break;
                    }
                    s += "Description=" + lblDescription.Text + Environment.NewLine;

                    if (!string.IsNullOrEmpty(lblWebsite.Text) && lblWebsite.Text != "http://www.")
                        s += "Website=" + lblWebsite.Text + Environment.NewLine;

                    if (lstCommands.Items.Count > 0)
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Creating Tasks.txt [Commands]");
                        Application.DoEvents();
                        s += Environment.NewLine;
                        s += Environment.NewLine + "[RUNCMD]" + Environment.NewLine;
                        s = lstCommands.Items.Cast<ListViewItem>().Aggregate(s, (current, item) => current + item.Text + "|" + item.SubItems[1].Text + Environment.NewLine);
                    }

                    int NCount = lstFolders.Items.Cast<ListViewItem>().Count();

                    if (NCount > 0)
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Creating Tasks.txt [CopyFolder]");
                        Application.DoEvents();
                        s += Environment.NewLine + "[CopyFolder]" + Environment.NewLine;
                        foreach (ListViewItem item in lstFolders.Items)
                        {
                            if (item.Font.Bold == false)
                            {
                                s = item.SubItems[1].Text.Split('|').Aggregate(s, (current, CF) => current + (item.Text + "::" + CF + Environment.NewLine));
                            }
                        }
                    }

                    NCount = lstCF.Items.Cast<ListViewItem>().Count();

                    if (NCount > 0)
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Creating Tasks.txt [CopyFile]");
                        Application.DoEvents();
                        s += Environment.NewLine;
                        s += Environment.NewLine + "[CopyFile]" + Environment.NewLine;
                        foreach (ListViewItem item in lstCF.Items)
                        {
                            if (item.Font.Bold == false)
                            {
                                s = item.SubItems[1].Text.Split('|').Aggregate(s, (current, CF) => current + (item.Text + "::" + CF + Environment.NewLine));
                            }
                        }
                    }

                    NCount = lstD.Items.Cast<ListViewItem>().Count();

                    if (NCount > 0)
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Creating Tasks.txt [Delete]");
                        Application.DoEvents();
                        s += Environment.NewLine;
                        s += Environment.NewLine + "[Delete]" + Environment.NewLine;
                        s = lstD.Items.Cast<ListViewItem>().Where(item => item.Font.Bold == false).Aggregate(s, (current, item) => current + item.Text + Environment.NewLine);
                    }

                    cMain.UpdateToolStripLabel(lblStatus, "Writing Tasks.txt...");

                    var WT = new StreamWriter(ATemp + "\\Tasks.txt", false);
                    WT.Write(s);
                    WT.Close();
                }
                catch (Exception Ex)
                {
                    cMain.ErrorBox("Win Toolkit was unable to write a new Tasks.txt file for the new addon." + Environment.NewLine + Environment.NewLine + ATemp + "\\Tasks.txt", "Error creating Tasks.txt", Ex.Message);
                    cMain.WriteLog(this, "Unable to create Tasks.txt", Ex.Message, lblStatus.Text);
                }

                if (lstFolders.Items.Count > 0)
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Copying Folders");
                    foreach (ListViewItem item in lstFolders.Items)
                    {
                        try
                        {
                            if (!Directory.Exists(cOptions.WinToolkitTemp + "\\AddonC\\" + item.Text) &&
                                 Directory.Exists(item.SubItems[2].Text))
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Copying Folder - " + item.Text);
                                item.BackColor = Color.LightYellow;
                                Application.DoEvents();
                                cMain.CopyDirectory(item.SubItems[2].Text, cOptions.WinToolkitTemp + "\\AddonC\\" + item.Text,
                                                          true, true);
                                item.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                item.BackColor = string.IsNullOrEmpty(item.SubItems[2].Text) ? Color.LightGreen : Color.LightPink;
                            }
                        }
                        catch (Exception Ex)
                        {
                            item.BackColor = Color.LightPink;
                            cMain.ErrorBox("Win Toolkit was unable to copy folder '" + item.SubItems[2].Text + "' to '" + cOptions.WinToolkitTemp + "\\AddonC\\" + item.Text + "'", "Error copying folders.", Ex.Message);
                        }
                    }
                }

                if (lstCF.Items.Count > 0)
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Copying Files");
                    foreach (ListViewItem item in lstCF.Items)
                    {
                        try
                        {
                            if (!File.Exists(cOptions.WinToolkitTemp + "\\AddonC\\" + item.Text) && File.Exists(item.SubItems[2].Text))
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Copying File - " + item.Text);
                                item.BackColor = Color.LightYellow;
                                Application.DoEvents();
                                File.Copy(item.SubItems[2].Text, cOptions.WinToolkitTemp + "\\AddonC\\" + item.Text);
                                item.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                item.BackColor = string.IsNullOrEmpty(item.SubItems[2].Text)
                                                            ? Color.LightGreen
                                                            : Color.LightPink;
                            }
                        }
                        catch (Exception Ex)
                        {
                            item.BackColor = Color.LightPink;
                            cMain.ErrorBox(
                                 "Win Toolkit was unable to copy file '" + item.SubItems[2].Text + "' to '" +
                                 cOptions.WinToolkitTemp + "\\AddonC\\" + item.Text + "'", "Error copying file.",
                                 Ex.Message);
                        }

                    }
                }

                if (lstReg.Items.Count > 0)
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Copying Reg Files");
                    foreach (ListViewItem item in lstReg.Items)
                    {
                        try
                        {
                            if (!File.Exists(cOptions.WinToolkitTemp + "\\AddonC\\" + item.Text) && File.Exists(item.SubItems[1].Text))
                            {
                                cMain.UpdateToolStripLabel(lblStatus, "Copying Reg File - " + item.Text);
                                item.BackColor = Color.LightYellow;
                                Application.DoEvents();
                                File.Copy(item.SubItems[1].Text, cOptions.WinToolkitTemp + "\\AddonC\\" + item.Text);
                                item.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                item.BackColor = string.IsNullOrEmpty(item.SubItems[1].Text) ? Color.LightGreen : Color.LightPink;
                            }
                        }
                        catch (Exception Ex)
                        {
                            item.BackColor = Color.LightPink;
                            cMain.ErrorBox("Win Toolkit was unable to copy registry '" + item.SubItems[1].Text + "' to '" + cOptions.WinToolkitTemp + "\\AddonC\\" + item.Text + "'", "Error copying registry.", Ex.Message);
                        }
                    }

                    if (Directory.Exists(cOptions.WinToolkitTemp + "\\AddonC\\"))
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "Cleaning Registries...");
                        Application.DoEvents();

                        foreach (string File in Directory.GetFiles(cOptions.WinToolkitTemp + "\\AddonC\\", "*.reg"))
                        {
                            cMain.UpdateToolStripLabel(lblStatus, "Cleaning Registries [" + File + "]...");
                            Application.DoEvents();
                            try
                            {
                                string nRF = null;
                                using (var RF = new StreamReader(File, true))
                                {
                                    int n = 0;
                                    while (RF.Peek() != -1)
                                    {
                                        string St = RF.ReadLine();
                                        if (!string.IsNullOrEmpty(St))
                                        {
                                            if (St.StartsWithIgnoreCase("[")) n = 0;
                                            if (St.ContainsIgnoreCase("["))
                                            {
                                                if (St.ContainsIgnoreCase("\\StartPage2]")) n = 1;
                                                if (St.ContainsIgnoreCase("BagMRU")) n = 1;
                                                if (St.ContainsIgnoreCase("Shell\\Bags")) n = 1;
                                                if (St.ContainsIgnoreCase("ComDlg32")) n = 1;
                                                if (St.ContainsIgnoreCase("UserAssist")) n = 1;
                                                if (St.ContainsIgnoreCase("RecentDocs")) n = 1;
                                                if (St.ContainsIgnoreCase("MuiCache")) n = 1;
                                                if (St.ContainsIgnoreCase("Action Center")) n = 1;
                                            }
                                            if (n == 0)
                                            {
                                                nRF += St + Environment.NewLine;
                                            }
                                        }
                                    }
                                }
                                if (!string.IsNullOrEmpty(nRF))
                                {
                                    using (var wRF = new StreamWriter(File, false))
                                    {
                                        wRF.Write(nRF);
                                    }
                                }
                            }
                            catch (Exception Ex)
                            {
                                cMain.ErrorBox("Win Toolkit was unable to clean the registry for: " + File, "Registry Clean Error.", Ex.Message);

                                cMain.WriteLog(this, "Unable to clean registry", Ex.Message, lblStatus.Text);
                            }
                        }
                    }
                }
                cMain.UpdateToolStripLabel(lblStatus, "Creating Addon (*.WA) file...");
                cMain.CompressFiles(cOptions.WinToolkitTemp + "\\AddonC\\", SFD.FileName, this);

            }
            catch (Exception Ex)
            {
                cMain.ErrorBox("Win Toolkit failed to create the addon due to an unknown error.", "Unknown Error", Ex.Message);
                cMain.WriteLog(this, "Unable to create addon (all)", Ex.Message, lblStatus.Text);
            }

            cMain.UpdateToolStripLabel(lblStatus, "Deleting temp files...");
            Application.DoEvents();
            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\AddonC\\", true);
        }

        private void BWMaker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ClearLists();

            mnuOpenWA.Enabled = true;
            cmdClearAll.Enabled = true;
            TabControl1.Enabled = true;
            cmdIT.Enabled = true;

            cMain.UpdateToolStripLabel(lblStatus, "Your addon has been successfully created!");
        }

        private void cmdArc_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                 "x86 - will work on both x64 and x86 OS, if integrated on x64 the files will go in 'Program Files (x86)' or 'SysWOW64' if needed." +
                 Environment.NewLine + Environment.NewLine + "x86 only - This addon can't be integrated into an x64 OS." + Environment.NewLine + Environment.NewLine +
                 "x64 - This is for 64bit apps and can't be integrated into an x86 OS." + Environment.NewLine + Environment.NewLine +
                 "Dual - No conversion regarding architecture will be done.", "Help");
        }

        private void AddPan(string T, ListView list)
        {
            L = list;
            txtAdd.Text = "";
            cboCT.Text = "";
            cmdApply.Text = "Add " + T;
            TabControl1.Enabled = false;
            ToolStrip1.Enabled = false;
            cMain.CenterObject(PanAdd);
            PanAdd.BringToFront();
            PanAdd.Visible = true;
        }


        private string AMShortcuts(string ShortC)
        {
            string RL = ShortC;
            try
            {
                while (!RL.EndsWithIgnoreCase("%"))
                {
                    RL = RL.Substring(0, RL.Length - 1);
                }
                int N = RL.Length;
                foreach (ListViewItem LST in lstAV.Items)
                {
                    ShortC = ShortC.ReplaceIgnoreCase(LST.Text, LST.SubItems[1].Text.Substring(3));
                    while (ShortC.ContainsIgnoreCase("\\\\") && ShortC.StartsWithIgnoreCase("[")) { ShortC = ShortC.ReplaceIgnoreCase("\\\\", "\\"); }

                }

                //If ShortC.ToUpper.StartsWithIgnoreCase("") Then ShortC = "" && R.Substring(N)

                if (ShortC.EndsWithIgnoreCase("\\")) { ShortC = ShortC.Substring(0, ShortC.Length - 1); }
                ShortC.ReplaceIgnoreCase("\\|", "|");
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Unable to convert AMShortcut", Ex.Message, lblStatus.Text);
            }
            return ShortC;
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {
            string Help = "";
            switch (TabControl1.SelectedTab.Text)
            {
                case "Info [Example]":
                    Help = Help + "Name : Crap Cleaner" + Environment.NewLine;
                    Help = Help + "Version : 2.33.1184" + Environment.NewLine;
                    Help = Help + "Description : Cleans crap, registry and so on." + Environment.NewLine;
                    Help = Help + "Creator : Legolash2o" + Environment.NewLine;
                    Help = Help + "Architecture : x86" + Environment.NewLine;
                    Help = Help + "Website:http://www.piriform.com/" + Environment.NewLine;
                    Help = Help + Environment.NewLine + "Build : 7600 (This comes in useful for stuff where you replace a file for a specific windows build i.e. DreamScene.dll)\nLeave build empty if you want it to work with all versions";
                    break;
                case "Copy Files":
                    Help = Help +
                             "Just select a file and then type where you want it to go i.e. Program Files\\ABC\\Example.exe";
                    break;
                case "Copy Folders":
                    Help = Help + "Just select a folder and then type where you want it to go i.e. Program Files\\";
                    break;
                case "Import Registry":
                    Help = Help +
                             "Select an exported registry file that you want to be apart of the addon, there is more info about how to delete keys and entries if you click " +
                             "\"Online Guide\"at the bottom";
                    break;
                case "Delete":
                    Help = Help +
                             "This will delete a file or folder you require, for example if you want to delete the folder C:\\Windows\\Logs so it is not there after install, just type Windows\\Logs. A file works the same Windows\\WindowsUpdate.log you just don't need the C:\\ part at the beginning!";
                    break;
                case "Shortcut":
                    Help = Help +
                             "This tool will create a shortcut for your addon if required, although it is easier just to add the shortcut already created in the " +
                             "\"Add Files\"" +
                             " tab, there is no need to edit the shortcut as this is done automatically during integration.";
                    break;
                case "Commands":
                    Help = Help + "These are commands which are opened during integration." + Environment.NewLine +
                             "An example would be... \"" +
                             "DISM.exe /Image:%WIM% /Disable-feature /FeatureName:WindowsMediaPlayer /Featurename:MediaCenter" +
                             "\"" + Environment.NewLine + Environment.NewLine +
                             "%WIM% - This will be translated into the default mount folder during integration (C:\\WinToolkit_Mount\\) as some people change their Win Toolkit Temp folder so its best to use %WIM%";
                    break;
            }

            MessageBox.Show(string.IsNullOrEmpty(Help) ? "No info available about this page" : Help,
                                 TabControl1.SelectedTab.Text);
        }

        private void cmdCFAF_Click(object sender, EventArgs e)
        {
            AddPan("File", lstCF);
        }

        private void chkCFolAF_Click(object sender, EventArgs e)
        {
            AddPan("Folder", lstFolders);
        }

        private void chkAR_Click(object sender, EventArgs e)
        {
            try
            {
                var OFD = new OpenFileDialog();
                OFD.Title = "Select reg file...";
                OFD.Filter = "Registry File *.reg|*.reg";
                OFD.Multiselect = true;

                if (OFD.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                foreach (string R in OFD.FileNames)
                {
                    string N = R;
                    while (N.ContainsIgnoreCase("\\"))
                    {
                        N = N.Substring(1);
                    }
                    var NI = new ListViewItem();
                    NI.Text = N;
                    NI.SubItems.Add(R);
                    lstReg.Items.Add(NI);
                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Unable to add registry to list.", Ex.Message, lblStatus.Text);
            }
        }

        private void chkRS_Click(object sender, EventArgs e)
        {
            if (lstReg.SelectedItems.Count == 0)
            {
                MessageBox.Show("You need to select at least one item!", "Invalid selection"); return;
            }

            foreach (ListViewItem LST in lstReg.SelectedItems)
            {
                LST.Remove();
            }
        }

        private void chkRA_Click(object sender, EventArgs e)
        {
            lstReg.Items.Clear();
        }

        private void cmdRFolS_Click(object sender, EventArgs e)
        {
            if (lstFolders.SelectedItems.Count == 0)
            {
                MessageBox.Show("You need to select at least one item!", "Invalid selection"); return;
            }
            foreach (ListViewItem LST in lstFolders.SelectedItems)
            {
                LST.Remove();
            }
        }

        private void cmdRFS_Click(object sender, EventArgs e)
        {
            if (lstCF.SelectedItems.Count == 0)
            {
                MessageBox.Show("You need to select at least one item!", "Invalid selection"); return;
            }
            foreach (ListViewItem LST in lstCF.SelectedItems)
            {
                LST.Remove();
            }
        }

        private void mnuDRS_Click(object sender, EventArgs e)
        {
            if (lstD.SelectedItems.Count == 0)
            {
                MessageBox.Show("You need to select at least one item!", "Invalid selection"); return;
            }
            foreach (ListViewItem LST in lstD.SelectedItems)
            {
                LST.Remove();
            }
        }

        private void cmdShortS_Click(object sender, EventArgs e)
        {
            if (lstShortcut.SelectedItems.Count == 0)
            {
                MessageBox.Show("You need to select at least one item!", "Invalid selection"); return;
            }
            foreach (ListViewItem LST in lstShortcut.SelectedItems)
            {
                LST.Remove();
            }
        }

        private void cmdRCS_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LST in lstCommands.SelectedItems)
            {
                LST.Remove();
            }
        }

        private void CMDRFA_Click(object sender, EventArgs e)
        {
            lstCF.Items.Clear();
        }

        private void cmdRFolA_Click(object sender, EventArgs e)
        {
            lstFolders.Items.Clear();
        }

        private void mnuDRA_Click(object sender, EventArgs e)
        {
            lstD.Items.Clear();
        }

        private void cmdShortAll_Click(object sender, EventArgs e)
        {
            lstShortcut.Items.Clear();
        }

        private void cmdRCA_Click(object sender, EventArgs e)
        {
            lstCommands.Items.Clear();
        }

        private void cmdAC_Click(object sender, EventArgs e)
        {
            try
            {
                string C =
                     cMain.InputBox(
                          "Enter a command you wish to run during the integration process." + Environment.NewLine +
                          "i.e. DISM.exe /Image:%WIM% /Disable-feature....", "Command Input");

                if (string.IsNullOrEmpty(C))
                {
                    return;
                }

                if (!C.ContainsIgnoreCase("DISM"))
                {
                    MessageBox.Show(
                         "For security reasons you can only use DISM commands, feel free to contact me if you require another command.",
                         "Invalid Command!");
                    return;
                }
                string F = C;

                while (F.ContainsIgnoreCase(" "))
                {
                    F = F.Substring(0, F.Length - 1);
                }
                string A = C.Substring(F.Length + 1);
                var NI = new ListViewItem();
                NI.Text = F;
                NI.SubItems.Add(A);
                lstCommands.Items.Add(NI);
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Unable to add DISM command.", Ex.Message, lblStatus.Text);
            }
        }

        private void cmdNShortcut_Click(object sender, EventArgs e)
        {
            lstShortcut.Enabled = false;
            ToolStrip6.Enabled = false;
            cMain.CenterObject(PanShortcut);
            PanShortcut.BringToFront();
            PanShortcut.Visible = true;
            cmdClear.PerformClick();
        }

        private void mnuFoL_Click(object sender, EventArgs e)
        {
            try
            {
                string S =
                     cMain.InputBox(
                          "Please enter the file or folder you wish to delete for example...\n\"" +
                          "Windows\\Media\"\n\"Windows\\Media\\Example.mp3\"",
                          "Delete File...");
                if (!string.IsNullOrEmpty(S))
                {
                    if (S.StartsWithIgnoreCase("%"))
                    {
                        S = AMShortcuts(S);
                    }
                    var N = new ListViewItem();
                    S = S.ReplaceIgnoreCase( "*", "");
                    N.Text = S;
                    if (!string.IsNullOrEmpty(N.Text))
                    {
                        lstD.Items.Add(N);
                    }
                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Unable to add folder for deletion to list.", Ex.Message, lblStatus.Text);
            }
        }

        private void mnuOG_Click(object sender, EventArgs e)
        {
            cMain.OpenLink("http://www.wincert.net/forum/index.php?showtopic=5833");
        }

        private void cmdAF_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmdApply.Text.ContainsIgnoreCase("FILE"))
                {
                    var OFD = new OpenFileDialog();
                    OFD.Title = "Select Files...";
                    OFD.Filter = "All files *.*|*.*";
                    OFD.Multiselect = true;
                    OFD.DereferenceLinks = false;
                    if (OFD.ShowDialog() != DialogResult.OK)
                        return;
                    txtAdd.Text = "";
                    foreach (string I in OFD.FileNames)
                    {
                        txtAdd.Text = txtAdd.Text + I + "|";
                    }
                    txtAdd.Text = txtAdd.Text.Substring(0, txtAdd.Text.Length - 1);
                    return;
                }
                if (cmdApply.Text.ContainsIgnoreCase("REG"))
                {
                    var OFD = new OpenFileDialog();
                    OFD.Title = "Select Files...";
                    OFD.Filter = "Registry File *.reg|*.reg";
                    OFD.Multiselect = true;
                    OFD.DereferenceLinks = false;
                    if (OFD.ShowDialog() != DialogResult.OK)
                        return;
                    txtAdd.Text = "";
                    foreach (string I in OFD.FileNames)
                    {
                        txtAdd.Text = txtAdd.Text + I + "|";
                    }
                    txtAdd.Text = txtAdd.Text.Substring(0, txtAdd.Text.Length - 1);
                    return;
                }

                if (cmdApply.Text.ContainsIgnoreCase("FOLDER"))
                {
                    string FBD = cMain.FolderBrowserVista("Browse folder to copy...", false, true);
                    if (string.IsNullOrEmpty(FBD))
                    {
                        return;
                    }
                    txtAdd.Text = FBD;
                    return;
                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Unable to add item.", Ex.Message, lblStatus.Text);
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAdd.Text))
                {
                    MessageBox.Show("Please select an item to copy", "Invalid Item");
                    txtAdd.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cboCT.Text))
                {
                    MessageBox.Show("Please select where to copy the item", "Invalid Location");
                    cboCT.Focus();
                    return;
                }

                if (cboCT.Text.ContainsIgnoreCase(":") || cboCT.Text.ContainsIgnoreCase(";") || cboCT.Text.ContainsIgnoreCase("/") || cboCT.Text.ContainsIgnoreCase("\"") || cboCT.Text.ContainsIgnoreCase("*") || cboCT.Text.ContainsIgnoreCase("?") || cboCT.Text.ContainsIgnoreCase("<") || cboCT.Text.ContainsIgnoreCase(">"))
                {
                    MessageBox.Show("You have included some invalid characters in the Copy To box, please remove them.\n: ; / \" < > * ?", "Invalid Characters");
                    return;
                }

                if (cmdApply.Text.ContainsIgnoreCase("FILE") || cmdApply.Text.ContainsIgnoreCase("REG"))
                {
                    if (cboCT.Text.StartsWithIgnoreCase("%")) { cboCT.Text = AMShortcuts(cboCT.Text); }
                    foreach (string Filename in txtAdd.Text.Split('|'))
                    {
                        string F =Filename.ReplaceIgnoreCase( "|", "");
                        var NI = new ListViewItem();
                        if (!Filename.ContainsIgnoreCase("TASKS.TXT"))
                        {
                            NI.Text = cMain.GetFName(F);
                            NI.SubItems.Add(cboCT.Text);
                            NI.SubItems.Add(F);
                            if (!string.IsNullOrEmpty(cMain.GetFName(F)))
                                L.Items.Add(NI);
                        }
                    }
                }

                if (cmdApply.Text.ContainsIgnoreCase("FOLDER"))
                {
                    if (cboCT.Text.StartsWithIgnoreCase("%"))
                        cboCT.Text = AMShortcuts(cboCT.Text);
                    var NI = new ListViewItem();
                    string FolName = txtAdd.Text;
                    while (FolName.ContainsIgnoreCase("\\"))
                    {
                        FolName = FolName.Substring(1);
                    }
                    NI.Text = FolName;
                    NI.SubItems.Add(cboCT.Text);
                    NI.SubItems.Add(txtAdd.Text);
                    if (!string.IsNullOrEmpty(NI.Text))
                        L.Items.Add(NI);
                }
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Enable to apply items to list.", Ex.Message, lblStatus.Text);
            }

            PanAdd.Visible = false;
            TabControl1.Enabled = true;
            ToolStrip1.Enabled = true;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            PanAdd.Visible = false;
            TabControl1.Enabled = true;
            ToolStrip1.Enabled = true;
        }

        private void cmdTP_Click(object sender, EventArgs e)
        {
            var N = new OpenFileDialog();
            N.Title = "Select Filename...";
            N.Multiselect = false;
            N.Filter = "All Files *.*|*.*";
            if (N.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            cboDestination.Enabled = true;
            txtTP.Text = N.FileName;
        }

        private void cmdBrowseI_Click(object sender, EventArgs e)
        {
            var N = new OpenFileDialog();
            N.Title = "Select Icon...";
            N.Multiselect = false;
            N.Filter = "Icon *.ico|*.ico";
            if (N.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            txtIcon.Text = N.FileName;
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtDescription.Text = "";
            txtIcon.Text = "";
            txtHotkey.Text = "";
            txtTP.Text = "";
            txtArg.Text = "";
            cboDestination.Text = "";
            cboDestination.Items.Clear();
            cboDestination.Items.Add("%Desktop%\\");
            cboDestination.Items.Add("%StartMenu%\\");
            cboDestination.SelectedIndex = 0;
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTP.Text))
                {
                    MessageBox.Show("Please enter a filepath!", "Invalid Target Path");
                    txtTP.Text = "";
                    txtTP.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDescription.Text))
                {
                    MessageBox.Show("You need to enter a description!", "Invalid Description");
                    txtDescription.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtTP.Text))
                {
                    MessageBox.Show("You need to enter a target path!", "Invalid Target Path");
                    txtTP.Focus();
                    return;
                }

                foreach (string sSC in cboDestination.Text.Split('|'))
                {
                    if (!sSC.EndsWithIgnoreCase(".lnk") || !sSC.ContainsIgnoreCase("\\"))
                    {
                        MessageBox.Show(
                        "You need to enter a valid shortcut path....\n%Desktop%\\Example.lnk" +
                        Environment.NewLine + "%STARTMENU%\\Program.lnk" + Environment.NewLine +
                        "ProgramData\\Microsoft\\Windows\\Start Menu\\Programs", "Invalid Destination");
                        cboDestination.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(cboDestination.Text))
                {
                    MessageBox.Show("You need to enter a target path!", "Invalid Destination");
                    cboDestination.Focus();
                    return;
                }

                var NS = new ListViewItem();
                NS.Text = txtDescription.Text;
                NS.SubItems.Add(txtIcon.Text);
                NS.SubItems.Add(txtHotkey.Text);
                NS.SubItems.Add(txtTP.Text);
                NS.SubItems.Add(txtArg.Text);
                NS.SubItems.Add(AMShortcuts(cboDestination.Text));
                lstShortcut.Items.Add(NS);
                foreach (ColumnHeader C in lstShortcut.Columns) { C.Width = -2; }
                cmdClear.PerformClick();
                cmdClose.PerformClick();
            }
            catch (Exception Ex)
            {
                cMain.WriteLog(this, "Unable to add shortcut to list.", Ex.Message, lblStatus.Text);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            lstShortcut.Enabled = true;
            ToolStrip6.Enabled = true;
            PanShortcut.Visible = false;
        }

        private void lstAV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                try
                {
                    Clipboard.Clear();
                    Clipboard.SetText(lstAV.SelectedItems[0].Text);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message, "Error copying to clipboard.");
                }
            }
        }

        private void cmsClipVar_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAV.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select a variable to copy", "Invalid Item");
                    return;
                }
                Clipboard.Clear();
                Clipboard.SetText(lstAV.SelectedItems[0].Text);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error copying to clipboard.");
            }
        }

        private void cmdIT_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    if (string.IsNullOrEmpty(lblName.Text))
                    {
                        MessageBox.Show("Please enter a name for the addon", "Invalid Name");
                        lblName.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(lblVersion.Text))
                    {
                        MessageBox.Show("Please enter a version for the addon", "Invalid Version");
                        lblVersion.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(lblCreator.Text))
                    {
                        MessageBox.Show("Please enter the name of the creator", "Invalid Creator");
                        lblCreator.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(lblDescription.Text))
                    {
                        MessageBox.Show("Please enter a description for the addon", "Invalid Description");
                        lblDescription.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(cboArc.Text))
                    {
                        MessageBox.Show("You have not select if the addon is x86 or x64", "Invalid Architecture");
                        cboArc.DroppedDown = true;
                        return;
                    }

                    if (lstCF.Items.Count == 0 && lstFolders.Items.Count == 0 && lstReg.Items.Count == 0 &&
                         lstD.Items.Count == 0 && lstShortcut.Items.Count == 0 && lstCommands.Items.Count == 0)
                    {
                        MessageBox.Show("You must select at least 1 file, folder or registry file!", "Not enough files");
                        return;
                    }

                    SFD.Title = "Save Addon...";
                    SFD.Filter = "Win Toolkit Addon *.WA|*.WA";
                    SFD.FileName = lblName.Text.ReplaceIgnoreCase(" ", "_");
                    if (SFD.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    cMain.UpdateToolStripLabel(lblStatus, "Creating Addon...");
                    TabControl1.Enabled = false;
                    cmdIT.Enabled = false;
                    mnuOpenWA.Enabled = false;
                    cmdClearAll.Enabled = false;
                    BWMaker.RunWorkerAsync();
                }
                catch (Exception Ex)
                {
                    cMain.WriteLog(this, "Unable to begin making addon.", Ex.Message, lblStatus.Text);
                }
            }
        }

        private void ClearLists()
        {
            lblName.Text = "";
            lblCreator.Text = "";
            lblDescription.Text = "";
            lblVersion.Text = "";
            lblWebsite.Text = "http://www.";
            cboArc.SelectedIndex = -1;
            lstCF.Items.Clear();
            lstFolders.Items.Clear();
            lstReg.Items.Clear();
            lstD.Items.Clear();
            lstShortcut.Items.Clear();
            lstCommands.Items.Clear();
        }

        private void LoadAddon(string Filename)
        {
            cMain.UpdateToolStripLabel(lblStatus, "Loading Addon [" + Filename + "]");
            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\AddonC\\", true);
            cMain.ExtractFiles(Filename, cOptions.WinToolkitTemp + "\\AddonC\\", this);

            if (File.Exists(cOptions.WinToolkitTemp + "\\AddonC\\Tasks.txt"))
            {
                cMain.OpenExplorer(this, cOptions.WinToolkitTemp + "\\AddonC\\");
                var SR = new StreamReader(cOptions.WinToolkitTemp + "\\AddonC\\Tasks.txt");
                string aFile = SR.ReadToEnd();
                SR.Close();
                int Task = 0;
                ListView SL = null;
                foreach (string S in aFile.Split(Environment.NewLine.ToCharArray()))
                {
                    if (!string.IsNullOrEmpty(S))
                    {
                        if (S.StartsWithIgnoreCase("NAME="))
                        {
                            lblName.Text = S.Substring(5);
                        }
                        if (S.StartsWithIgnoreCase("CREATOR="))
                        {
                            lblCreator.Text = S.Substring(8);
                        }
                        if (S.StartsWithIgnoreCase("VERSION="))
                        {
                            lblVersion.Text = S.Substring(8);
                        }
                        if (S.StartsWithIgnoreCase("ARC="))
                        {
                            cboArc.Text = S.Substring(4);
                        }
                        if (S.StartsWithIgnoreCase("ARC=X86!"))
                        {
                            cboArc.Text = "x86 only";
                        }
                        if (S.StartsWithIgnoreCase("DESCRIPTION="))
                        {
                            lblDescription.Text = S.Substring(12);
                        }
                        if (S.StartsWithIgnoreCase("WEBSITE="))
                        {
                            lblWebsite.Text = S.Substring(8);
                        }

                        if (S.ContainsIgnoreCase("[CopyFolder]"))
                        {
                            Task = 1;
                            SL = lstFolders;
                        }
                        if (S.ContainsIgnoreCase("[CopyFile]"))
                        {
                            Task = 2;
                            SL = lstCF;
                        }
                        if (S.ContainsIgnoreCase("[RUNCMD]"))
                        {
                            Task = 3;
                            SL = lstCommands;
                        }
                        if (S.ContainsIgnoreCase("[Delete]"))
                        {
                            Task = 4;
                            SL = lstD;
                        }

                        if (Task != 0 && !S.StartsWithIgnoreCase("[") && !string.IsNullOrEmpty(S) && SL != null)
                        {
                            string N = S, D = S;
                            var NI = new ListViewItem();
                            if (S.ContainsIgnoreCase("::"))
                            {
                                while (N.ContainsIgnoreCase(":"))
                                {
                                    N = N.Substring(0, N.Length - 1);
                                }

                                while (D.ContainsIgnoreCase(":"))
                                {
                                    D = D.Substring(1);
                                }
                            }

                            if (Task == 1 || Task == 2)
                            {
                                NI.Text = N;
                                NI.SubItems.Add(D);
                                NI.SubItems.Add("");
                                if (N.ToUpper().EndsWithIgnoreCase(".LNK")) { NI.Tag = "Shortcut"; }
                            }

                            if (Task == 3)
                            {
                                string F = S;
                                while (F.ContainsIgnoreCase("|"))
                                {
                                    F = F.Substring(0, F.Length - 1);
                                }
                                NI.Text = F;
                                NI.SubItems.Add(S.Substring(F.Length + 1));
                            }

                            if (Task == 4)
                            {
                                NI.Text = S;
                            }
                            SL.Items.Add(NI);
                        }
                    }
                }

                foreach (ListViewItem item in lstCF.Items)
                {
                    if (item.Tag != null)
                    {
                        if (item.Tag.ToString().EqualsIgnoreCase("Shortcut")) { item.Remove(); }
                    }
                }

                foreach (string R in Directory.GetFiles(cOptions.WinToolkitTemp + "\\AddonC", "*.reg", SearchOption.TopDirectoryOnly))
                {
                    string F = R;
                    while (F.ContainsIgnoreCase("\\"))
                    {
                        F = F.Substring(1);
                    }

                    var NI = new ListViewItem();
                    NI.Text = F;
                    NI.SubItems.Add("");
                    lstReg.Items.Add(NI);
                }

                foreach (string R in Directory.GetFiles(cOptions.WinToolkitTemp + "\\AddonC", "*.lnk", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        var NI = new ListViewItem();
                        var shell2 = new IWshRuntimeLibrary.WshShell();
                        var link = (IWshRuntimeLibrary.IWshShortcut)shell2.CreateShortcut(R);
                        string sName = cMain.GetFName(R);

                        NI.Text = link.Description;
                        if (string.IsNullOrEmpty(NI.Text)) { NI.Text = sName.ReplaceIgnoreCase(".lnk", ""); }
                        NI.SubItems.Add(link.IconLocation);
                        NI.SubItems.Add(link.Hotkey);
                        NI.SubItems.Add(link.TargetPath);
                        NI.SubItems.Add(link.Arguments);
                        NI.Tag = "Shortcut";
                        link.Save();
                        foreach (string SS in aFile.Split(Environment.NewLine.ToCharArray()))
                        {
                            if (SS.StartsWithIgnoreCase(sName))
                            {
                                NI.SubItems.Add(SS.ReplaceIgnoreCase(sName + "::", "") + "\\" + sName);
                                break;
                            }
                        }

                        lstShortcut.Items.Add(NI);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("This is not a valid addon!", "Error");
            }
            foreach (ColumnHeader C in lstShortcut.Columns) { C.Width = -2; }
        }

        private void mnuOpenWA_Click(object sender, EventArgs e)
        {
            ClearLists();
            var OFD = new OpenFileDialog();
            try
            {
                OFD.Title = "Select .WA file...";
                OFD.Filter = "Win Toolkit Addon *.wa|*.wa";
                OFD.Multiselect = false;

                if (OFD.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                TabControl1.Enabled = false;
                cmdIT.Enabled = false;
                mnuOpenWA.Enabled = false;
                cmdClearAll.Enabled = false;
                cMain.UpdateToolStripLabel(lblStatus, "Loading Addon...");
                Application.DoEvents();
                LoadAddon(OFD.FileName);

            }
            catch (Exception Ex)
            {
                cMain.ErrorBox("Win Toolkit could not open the requested file, it may not be an addon. " + OFD.FileName, "Addon Load Error.", Ex.Message);
                cMain.WriteLog(this, "Unable to open addon.", Ex.Message, lblStatus.Text);
            }
            cMain.UpdateToolStripLabel(lblStatus, "");
            TabControl1.Enabled = true;
            cmdIT.Enabled = true;
            mnuOpenWA.Enabled = true;
            cmdClearAll.Enabled = true;
        }

        private void txtTP_TextChanged(object sender, EventArgs e)
        {
            cboDestination.Items.Clear();
            foreach (ListViewItem LST in lstAV.Items) { cboDestination.Items.Add(LST.Text); }
            if (!string.IsNullOrEmpty(txtTP.Text))
            {
                try
                {
                    string S = txtTP.Text;
                    while (S.ContainsIgnoreCase("\\"))
                        S = S.Substring(1);
                    if (S.ContainsIgnoreCase("."))
                    {
                        while (!S.EndsWithIgnoreCase("."))
                            S = S.Substring(0, S.Length - 1);

                        S = S.Substring(0, S.Length - 1);
                    }
                    if (!string.IsNullOrEmpty(S))
                    {
                        cboDestination.Items.Clear();
                        foreach (ListViewItem LST in lstAV.Items) { cboDestination.Items.Add(LST.Text + "\\" + S + ".lnk"); }
                    }
                }
                catch
                {
                }
            }
            cboDestination.SelectedIndex = 5;
        }

        private void cmdClearAll_Click(object sender, EventArgs e)
        {
            DialogResult DR = MessageBox.Show("This will clear all the forms and lists, do you wish to continue?", "New Addon", MessageBoxButtons.YesNo);
            if (DR != DialogResult.Yes) { return; }
            Files.DeleteFolder(cOptions.WinToolkitTemp + "\\AddonC\\", true);
            ClearLists();
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
        }
    }
}