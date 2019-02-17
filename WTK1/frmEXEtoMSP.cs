using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;

namespace WinToolkit
{
    public partial class frmEXEtoMSP : Form
    {
        private string Folder = "";
        private bool extractSubfolder = true;
        private bool showOutput = true;

        public frmEXEtoMSP()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            FormClosing += frmEXEtoMSP_FormClosing;
            FormClosed += frmEXEtoMSP_FormClosed;
            CheckForIllegalCrossThreadCalls = false;
            BWConvert.RunWorkerCompleted += BWConvert_RunWorkerCompleted;
            Activated += cMain.FormActivated;
            Deactivate += cMain.FormActivated;
            MouseWheel += MouseScroll;
        }

        private bool IsChecked(string Entry)
        {
            ListViewItem LST = lstFilters.FindItemWithText(Entry);
            if (LST == null) { return false; }
            return LST.Checked;
        }

        private void MouseScroll(object sender, MouseEventArgs e)
        {
            lstMSP.Select();
        }

        private void frmEXEtoMSP_Load(object sender, EventArgs e)
        {
            SplitContainer1.Scale4K(_4KHelper.Panel.Pan1);
            SplitContainer2.Scale4K(_4KHelper.Panel.Pan2);

            cMain.FormIcon(this); cMain.eLBL = lblStatus; cMain.eForm = this;
            cMain.ToolStripIcons(ToolStrip1);

            foreach (ListViewItem lvi in lstFilters.Items)
            {
                lvi.SubItems.Add(lvi.ToolTipText);
            }
            lstFilters.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstMSP.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void frmEXEtoMSP_FormClosing(object sender, FormClosingEventArgs e)
        {
            cMain.OnFormClosing(this);
            if (BWConvert.IsBusy)
            {
                MessageBox.Show("Conversion in progress", "N/A");
                e.Cancel = true;
            }
        }

        private void frmEXEtoMSP_FormClosed(object sender, FormClosedEventArgs e)
        {
            cMain.ReturnME();
        }

        private bool Valid(string sFileDescription)
        {
            if (string.IsNullOrEmpty(sFileDescription)) { return false; }
            if (sFileDescription.ContainsIgnoreCase("Microsoft Filter Pack")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("Word")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("Office")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("Outlook")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("Excel")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("PowerPoint")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("Access")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("OneNote")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("Publisher")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("SharePoint Workspace")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("Visio")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("InfoPath")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("Lync")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("Project")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("SkyDrive Pro")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("OneDrive for Business")) { return true; }
            if (sFileDescription.ContainsIgnoreCase("Skype for Business")) { return true; }
            return false;
        }

        private void ListAdd(Array strList)
        {
            lstMSP.Sorting = SortOrder.None;
            int T = cMain.GetAdditions(strList);
            int N = 1;

            foreach (string strFilename in strList)
            {
                try
                {
                    if (File.Exists(strFilename) && strFilename.ToUpper().EndsWithIgnoreCase(".EXE") && lstMSP.FindItemWithText(strFilename) == null)
                    {
                        cMain.UpdateToolStripLabel(lblStatus, "(" + N + "/" + T + ") Adding " + strFilename + "...");
                        Application.DoEvents();

                        FileVersionInfo F = FileVersionInfo.GetVersionInfo(strFilename);

                        bool validUpdate = Valid(F.FileDescription);
                        
                        if (!validUpdate)
                        {
                            MessageBox.Show(
                                "This does not appear to be an Office update.\n\n[" + strFilename + "]\n\nDescription:" +
                                F.FileDescription, "Invalid item");
                            continue;

                            //DialogResult DR = MessageBox.Show(
                            //         "This does not appear to be an Office update.\n\n[" + strFilename + "]\n\nDescription:" + F.FileDescription + "\n\nWould you like to add it anyway?", "Invalid File", MessageBoxButtons.YesNoCancel);

                            //if (DR == DialogResult.Cancel) { break; }
                            //if (DR != DialogResult.Yes) { continue; }

                        }

                        if (lstMSP.FindItemWithText(strFilename) == null)
                        {
                            var NI = new ListViewItem();
                            NI.Text = cMain.GetFName(strFilename);
                            NI.Text = NI.Text.Substring(0, 1).ToUpper() + NI.Text.Substring(1);
                            NI.SubItems.Add(cMain.GetSize(strFilename, true));
                            string OV = "N/A";

                            if (F.FileDescription.ContainsIgnoreCase("XP"))
                            {
                                OV = "XP";
                            }
                            if (F.FileDescription.ContainsIgnoreCase("2003"))
                            {
                                OV = "2003";
                            }
                            if (F.FileDescription.ContainsIgnoreCase("2007"))
                            {
                                OV = "2007";
                            }
                            if (F.FileDescription.ContainsIgnoreCase("2010"))
                            {
                                OV = "2010";
                            }

                            NI.SubItems.Add(OV);
                            NI.SubItems.Add(F.FileDescription);
                            NI.SubItems.Add(strFilename);
                            NI.ToolTipText = strFilename;

                            lstMSP.Items.Add(NI);
                        }

                    }
                }
                catch (Exception Ex)
                {
                    LargeError LE = new LargeError("Adding EXE", "Error trying to add exe.", strFilename + "\n" + lblStatus.Text, Ex);
                    LE.Upload(); LE.ShowDialog();
                }
                N += 1;
            }
            ColumnHeader1.Text = "Update (" + lstMSP.Items.Count + ")";
            cMain.AutoSizeColums(lstMSP);
            cMain.UpdateToolStripLabel(lblStatus, "");
        }

        private void BWConvert_DoWork(object sender, DoWorkEventArgs e)
        {

            Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
            cMain.CreateDirectory(Folder);
            foreach (ListViewItem LST in lstMSP.Items)
            {
                LST.Selected = true;
                LST.EnsureVisible();
                try
                {
                    int fc = Directory.GetFiles(Folder, "*.msp", SearchOption.TopDirectoryOnly).Length;

                    cMain.UpdateToolStripLabel(lblStatus, "Extracting (" + (LST.Index + 1) + " of " + lstMSP.Items.Count + ") : " + LST.Text);
                    Application.DoEvents();

                    string F = Folder;
                    if (extractSubfolder)
                    {
                        F += "\\" + Path.GetFileNameWithoutExtension(F);
                    }

                    if (!Directory.Exists(F))
                    {
                        cMain.CreateDirectory(F);
                    }

                    cMain.UpdateToolStripLabel(lblStatus, "Extracting :: EXE (" + (LST.Index + 1) + " of " + lstMSP.Items.Count + ") : " + LST.Text);
                    Application.DoEvents();

                    FileVersionInfo FI = FileVersionInfo.GetVersionInfo(LST.SubItems[4].Text);
                    string tempDir = F + "\\" + cMain.RandomName() + "_Temp";
                    cMain.CreateDirectory(tempDir);

                    if (FI.OriginalFilename.ContainsIgnoreCase("WEXTRACT"))
                    {
                        cMain.OpenProgram("\"" + LST.SubItems[4].Text + "\"", "/C /T:\"" + tempDir + "\" /Q", true, ProcessWindowStyle.Hidden);
                    }
                    else
                    {
                        cMain.OpenProgram("\"" + LST.SubItems[4].Text + "\"", "/extract:\"" + tempDir + "\" /quiet", true, ProcessWindowStyle.Hidden);
                    }

                    string[] files = Directory.GetFiles(tempDir, "*", SearchOption.TopDirectoryOnly);

                    foreach (string extracted in files)
                    {
                        bool moved = false;
                        string fileName = Path.GetFileName(extracted);

                        foreach (ListViewItem filter in lstFilters.CheckedItems)
                        {
                            try
                            {
                                if (fileName.ContainsIgnoreCase("PROOF") || fileName.ContainsIgnoreCase("-NONE") || fileName.ContainsIgnoreCase(filter.ToolTipText.ToUpper()))
                                {
                                    string moveTo = F + "\\" + fileName;

                                    if (!File.Exists(moveTo))
                                    {
                                        File.Move(extracted, moveTo);
                                    }
                                    break;
                                }
                            }
                            catch (Exception Ex)
                            {
                                LargeError error = new LargeError("MSP Error", "Error moving MSP.", Ex);
                                error.Upload();
                                error.ShowDialog();
                            }
                        }
                    }

                    Files.DeleteFolder(tempDir, false);

                }
                catch
                {
                }
                LST.Selected = false;
                PB.Value += 1;

                Windows7Taskbar.SetProgressValue(Handle, Convert.ToUInt16(PB.Value), Convert.ToUInt16(lstMSP.Items.Count));
                if (BWConvert.CancellationPending)
                {
                    break;
                }
            }

            cMain.UpdateToolStripLabel(lblStatus, "Cleaning non-essential files...");
            Application.DoEvents();
            foreach (string D in Directory.GetFiles(Folder, "*.*", SearchOption.AllDirectories))
            {
                if (!D.ToUpper().EndsWithIgnoreCase(".MSP"))
                {
                    Files.DeleteFile(D);
                }
            }
            Windows7Taskbar.SetProgressState(Handle, ThumbnailProgressState.NoProgress);
        }

        private void BWConvert_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cMain.UpdateToolStripLabel(lblStatus, "Completed...");
            MessageBox.Show(
                "You need to place the MSP files in an 'Updates' folder of your Office installation." +
                Environment.NewLine + Environment.NewLine + "Example:\n\\Office\\Setup.exe\n\\Office\\Updates\\*.msp",
                "Completed");

            if (showOutput)
            {
                cMain.OpenExplorer(this, Folder);
            }

            cmdSC.Text = "Start";
            cmdSC.Image = Properties.Resources.OK;
            PB.Visible = false;
            cmdAU.Visible = true;
            cmdRA.Visible = true;
            cmdR.Visible = true;
            cmdShowOutput.Visible = true;
            cmdExtractSubfolders.Visible = true;
        }

        private void cmdRA_Click(object sender, EventArgs e)
        {
            lstMSP.Items.Clear();
            ColumnHeader1.Text = "Update (" + lstMSP.Items.Count + ")";
            cMain.AutoSizeColums(lstMSP);
        }

        private void cmdRS_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LST in lstMSP.SelectedItems)
            {
                LST.Remove();
            }
            ColumnHeader1.Text = "Update (" + lstMSP.Items.Count + ")";
            cMain.AutoSizeColums(lstMSP);
        }

        private void cmdSC_Click(object sender, EventArgs e)
        {
            if (cmdSC.Text.EqualsIgnoreCase("Start"))
            {
                if (lstMSP.Items.Count == 0)
                {
                    MessageBox.Show("please make sure there is at least 1 item in the list", "No items!");
                    return;
                }

                string FBD = cMain.FolderBrowserVista("Select MSP save location...", false, true);

                if (string.IsNullOrEmpty(FBD))
                {
                    return;
                }

                Folder = FBD;
                if (!Folder.ToUpper().EndsWithIgnoreCase("\\OFFICEUPDATES") && !cMain.IsEmpty(Folder))
                {
                    Folder += "\\OfficeUpdates";
                }

                cmdR.Visible = false;
                cmdRA.Visible = false;
                cmdAU.Visible = false;
                cmdShowOutput.Visible = false;
                cmdExtractSubfolders.Visible = false;
                cmdSC.Text = "Cancel";
                cmdSC.Image = Properties.Resources.Close;
                PB.Value = 0;
                PB.Maximum = lstMSP.Items.Count;
                PB.Visible = true;

                BWConvert.RunWorkerAsync();
            }
            else
            {
                cmdSC.Enabled = false;
                cMain.UpdateToolStripLabel(lblStatus, "Stopping, please wait...");
                BWConvert.CancelAsync();
            }
        }

        private void cmdAU_Click(object sender, EventArgs e)
        {
            var OFD = new OpenFileDialog();
            OFD.Title = "Select Updates";
            OFD.Filter = "Office Update *.exe|*.exe";
            OFD.Multiselect = true;
            if (OFD.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            lstMSP.BeginUpdate();
            ListAdd(OFD.FileNames);
            lstMSP.EndUpdate();
            lstMSP.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void lstMSP_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lstMSP_DragDrop(object sender, DragEventArgs e)
        {
            var FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            ListAdd(FileList);
        }

        private void cmdShowOutput_Click(object sender, EventArgs e)
        {
            if (showOutput)
            {
                cmdShowOutput.Image = Properties.Resources.Unchecked;
            }
            else
            {
                cmdShowOutput.Image = Properties.Resources.Checked;
            }
        }

        private void cmdExtractSubfolders_Click(object sender, EventArgs e)
        {
            if (extractSubfolder)
            {
                cmdExtractSubfolders.Image = Properties.Resources.Unchecked;
            }
            else
            {
                cmdExtractSubfolders.Image = Properties.Resources.Checked;
            }
        }

    }
}