using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;

namespace WinToolkit.Classes
{
    public partial class frmCleanup : Form
    {
        private string _mountPath = "";

        public frmCleanup(string mountPath)
        {
            InitializeComponent();
            _mountPath = mountPath;
            lblPath.Text = _mountPath;
            cMain.ToolStripIcons(ToolStrip1);
            CheckForIllegalCrossThreadCalls = false;
        }

        private void frmCleanup_Load(object sender, EventArgs e)
        {
            splitContainer2.Scale4K(_4KHelper.Panel.Pan1);
            splitContainer2.Scale4K(_4KHelper.Panel.Pan2);
            if (!File.Exists(_mountPath + "\\Windows\\Explorer.exe")) { return; }

            try
            {
                if (DISM.Latest.Version >= DISM.Win8_0DISM)
                {
                    bool Win8_0_Image = FileVersionInfo.GetVersionInfo(_mountPath + "\\Windows\\Explorer.exe").FileVersion.StartsWithIgnoreCase("6.2");
                    bool Win8_1_Image = FileVersionInfo.GetVersionInfo(_mountPath + "\\Windows\\Explorer.exe").FileVersion.StartsWithIgnoreCase("6.3");

                    if (DISM.Latest.Version >= DISM.Win8_1DISM && Win8_1_Image && !File.Exists(_mountPath + "\\Windows\\winsxs\\pending.xml"))
                    {
                        ListViewItem newItem = new ListViewItem("Reset Base");
                        newItem.SubItems.Add("N/A");
                        newItem.SubItems.Add(DISM.Latest.Location + "|/Image:\"" + _mountPath +
                                             "\" /Cleanup-Image /StartComponentCleanup /ResetBase");
                        newItem.Checked = false;
                        newItem.ImageIndex = 2;
                        newItem.ToolTipText = "Using the /ResetBase switch with the /StartComponentCleanup\n" +
                                              "parameter of DISM.exe on a running version of Windows 8.1\n" +
                                              "removes all superseded (outdated) versions of every component\n" +
                                              "in the component store. ";
                        newItem.Group = lstCleanup.Groups[3];
                        lstCleanup.Items.Add(newItem);

                        ListViewItem newItem2 = new ListViewItem("Service Pack Cleanup");
                        newItem2.SubItems.Add("N/A");
                        newItem2.SubItems.Add(DISM.Latest.Location + "|/Image:\"" + _mountPath +
                                             "\" /Cleanup-Image /SPSuperseded");
                        newItem2.Checked = false;
                        newItem2.ImageIndex = 2;
                        newItem2.ToolTipText = "To reduce the amount of space used by a Service Pack, use\n" +
                                              "the /SPSuperseded parameter of Dism.exe on a running version\n" +
                                              "of Windows 8.1 to remove any backup components needed for\n" +
                                              "uninstallation of the service pack. A service pack is a\n" +
                                              "collection of cumulative updates for a particular release\n" +
                                              "of Windows.";
                        newItem2.Group = lstCleanup.Groups[3];
                        lstCleanup.Items.Add(newItem2);
                    }

                    if (!File.Exists(_mountPath + "\\Windows\\winsxs\\pending.xml"))
                    {
                        ListViewItem newItem = new ListViewItem("Component Cleanup");
                        newItem.SubItems.Add("N/A");
                        newItem.SubItems.Add(DISM.Latest.Location + "|/Image:\"" + _mountPath +
                                             "\" /Cleanup-Image /StartComponentCleanup");
                        newItem.Checked = false;
                        newItem.ImageIndex = 2;
                        newItem.ToolTipText = "The StartComponentCleanup task was introduced in Windows 8 to\n" +
                                              "regularly clean up components automatically when the system\n" +
                                              "is not in use.";
                        newItem.Group = lstCleanup.Groups[3];
                        lstCleanup.Items.Add(newItem);
                    }
                }
            }
            catch (Exception Ex)
            {
                new SmallError("Error Making Cleanup Items", Ex).Upload();
            }
            bwGetSize.RunWorkerAsync();
        }

        private void frmCleanup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bwGetSize.IsBusy)
            {
                lblPath.Text = "Cancelling. Please wait...";
                cmdStart.Enabled = false;
                lstCleanup.Enabled = false;

                bwGetSize.CancelAsync();

                while (bwGetSize.IsBusy)
                {
                    System.Threading.Thread.Sleep(100);
                    Application.DoEvents();
                }
            }
        }

        public static string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern)
                              .ReplaceIgnoreCase(@"\*", ".*")
                              .ReplaceIgnoreCase(@"\?", ".")
                       + "$";
        }

        private void bwGetSize_DoWork(object sender, DoWorkEventArgs e)
        {
            double _totalSize = 0;

            cMain.UpdateToolStripLabel(lblStatus, "Calculating. Please wait...");
            lstCleanup.Invoke(new MethodInvoker(() =>
            {
                foreach (ListViewItem lvi in lstCleanup.Items)
                {
                    lvi.SubItems[1].Text = "Queued...";
                }
            }));

            cMain.UpdateToolStripLabel(lblStatus, "Getting files. Please wait...");

            var files = new List<string>();
            cMain.GetFiles(ref files, _mountPath, "*");

            foreach (ListViewGroup lvg in lstCleanup.Groups)
            {
                foreach (ListViewItem lvi in lvg.Items)
                {
                    if (bwGetSize.CancellationPending) { return; }

                    lstCleanup.Invoke(new MethodInvoker(() =>
                    {
                        lvi.SubItems[1].Text = "Calculating...";
                    }));

                    string newText = "0 bytes";

                    if (!bwGetSize.CancellationPending)
                    {
                        switch (lvi.ImageIndex)
                        {
                            //Files
                            case 0:
                                string search = lvi.SubItems[2].Text;
                                if (search.ContainsIgnoreCase("*"))
                                {
                                    string filterfolder = search.Split('*')[0];
                                    string filter = search.Substring(search.IndexOf('*'));

                                    var foundFiles = new List<string>();

                                    var searchPattern = new Regex(WildcardToRegex(filter));
                                    foundFiles = files.Where(f => searchPattern.IsMatch(Path.GetFileName(f))).ToList();
                                    //cMain.GetFiles(ref foundFiles, _mountPath + "\\" + filterfolder,  filter);

                                    for (int i = 0; i < foundFiles.Count; i++)
                                    {
                                       if (Exclusion(foundFiles[i]))
                                        {
                                            txtLog.Text += "Excluded: " +  foundFiles[i] + Environment.NewLine;
                                            foundFiles.RemoveAt(i--);
                                            continue;
                                        }
                                        txtLog.Text += foundFiles[i] + Environment.NewLine;
                                    }

                                    if (foundFiles.Count > 0)
                                    {
                                        string prepareSize = foundFiles.Aggregate((a, b) => a + "|" + b);

                                        newText = "[" + foundFiles.Count + "] " + cMain.GetSize(prepareSize, false);

                                        lstCleanup.Invoke(new MethodInvoker(() =>
                                        {
                                            lvi.SubItems.Add(prepareSize);
                                        }));
                                    }
                                }
                                else
                                {
                                    newText = cMain.GetSize(_mountPath + "\\" + search, false);
                                }
                                break;

                            //Folders
                            case 1:
                                string folder = lvi.SubItems[2].Text;
                                if (folder.EndsWithIgnoreCase("\\*")) { folder = folder.Substring(0, folder.Length - 2); }
                                newText = cMain.GetSize(_mountPath + "\\" + folder, false);
                                txtLog.Text += _mountPath + "\\" + folder + Environment.NewLine;
                                break;

                            //Process
                            default:
                                newText = "N/A";
                                break;
                        }
                    }

                    if (newText != "N/A")
                    {
                        double _size = 0;
                        if (newText.ContainsIgnoreCase(" "))
                        {
                            double.TryParse(newText.Split(' ')[1], out _size);
                        }
                        else
                        {
                            double.TryParse(newText, out _size);
                        }
                        if (_size > 0)
                        {
                            _totalSize += _size;
                            lstCleanup.Columns[1].Text = "Size [" + cMain.BytesToString(_totalSize) + "]";
                        }
                        newText = cMain.BytesToString(_size);

                        if (newText.StartsWithIgnoreCase("-1")) { newText = "0 bytes"; }

                        // if (lvi.SubItems[1].Text.EqualsIgnoreCase("0 bytes") { lvg.Items.Remove(lvi); }

                    }

                    lvi.SubItems[1].Text = newText;
                }
            }

            lstCleanup.Invoke(new MethodInvoker(() =>
            {
                foreach (ListViewItem lvi in lstCleanup.Items.Cast<ListViewItem>().Where(lvi => lvi.SubItems[1].Text.EndsWithIgnoreCase("0 bytes")))
                {
                    lvi.Remove();
                }
            }));


            if (lstCleanup.Items.Count == 0)
            {
                cMain.UpdateToolStripLabel(lblStatus, "Nothing to clean.");
                cmdStart.Enabled = false;
            }
            else
            {
                cmdStart.Text = "Clean";
                cMain.UpdateToolStripLabel(lblStatus, "Ready.");
            }
        }

        private bool Exclusion(string filePath)
        {
            string path = Path.GetFileName(filePath).ToUpper();
            if (path.EndsWithIgnoreCase(".LOG"))
            {
                if (path.EqualsIgnoreCase("MSDTC.LOG")) { return true; }
                if (path.EqualsIgnoreCase("CBS.LOG")) { return true; }
                return false;
            }

            if (path.EndsWithIgnoreCase(".CAB"))
            {
                if (path.EqualsIgnoreCase("NPAD2.CAB")) { return true; }
                if (filePath.ContainsIgnoreCase("drivers\\x64\\PCC\\")) { return true; }
                if (filePath.ContainsIgnoreCase("drivers\\W32X86\\PCC\\")) { return true; }
                if (filePath.ContainsIgnoreCase("\\winsxs\\")) { return true; }
                if (filePath.EndsWithIgnoreCase("GWX\\Download\\Config.cab")) { return true; }
                return false;
            }
            return false;
        }
        private void deleteFile(string file)
        {
            foreach (string f in file.Split('|'))
            {
                string fileName = Path.GetFileName(f);

                cMain.UpdateToolStripLabel(lblStatus, "Cleaning Image [" + fileName + "]...");
                if (!f.ContainsIgnoreCase(":"))
                {
                    Files.DeleteFile(_mountPath + "\\" + f);
                }
                else
                {
                    Files.DeleteFile(f);
                }

            }
        }

        private bool IsChecked(string Entry)
        {
            ListViewItem LST = lstCleanup.FindItemWithText(Entry);
            if (LST == null) { return false; }
            return LST.Checked;
        }

        private void deleteFolder(string path, bool reCreate)
        {
            cMain.UpdateToolStripLabel(lblStatus, "Cleaning Image [" + path + "]...");
            Files.DeleteFolder(_mountPath + "\\" + path, reCreate);
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            if (cmdStart.Text.EqualsIgnoreCase("Clean") || cmdStart.Text.EqualsIgnoreCase("Queue Clean"))
            {
                if (lstCleanup.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Please select something to clean first!", "Invalid Selection");
                    return;
                }

                cmdStart.Enabled = false;

                if (bwGetSize.IsBusy)
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Waiting for calculations to finish...");
                    while (bwGetSize.IsBusy)
                    {
                        System.Threading.Thread.Sleep(200);
                        Application.DoEvents();
                    }
                }

                lstCleanup.Enabled = false;
                cmdStart.Text = "Cancel";
                cmdStart.Enabled = true;

                cMain.UpdateToolStripLabel(lblStatus, "Cleaning Image...");
                bwClean.RunWorkerAsync();
            }
            else
            {
                cmdStart.Enabled = false;
                bwClean.CancelAsync();
            }
            //Files

        }

        private void bwClean_DoWork(object sender, DoWorkEventArgs e)
        {

            foreach (ListViewItem item in lstCleanup.CheckedItems)
            {
                if (item.SubItems[1].Text.EndsWithIgnoreCase("0 bytes"))
                    continue;

                cMain.UpdateToolStripLabel(lblStatus, "Cleaning Image [" + item.Text + "]...");
                switch (item.ImageIndex)
                {
                    //Files
                    case 0:
                        if (item.SubItems[2].Text.StartsWithIgnoreCase("*"))
                        {
                            deleteFile(item.SubItems[3].Text);
                        }
                        else
                        {
                            deleteFile(item.SubItems[2].Text);
                        }
                        break;

                    //Folders
                    case 1:
                        string folder = item.SubItems[2].Text;
                        if (folder.EndsWithIgnoreCase("\\*"))
                        {
                            folder = folder.Substring(0, folder.Length - 2);
                            deleteFolder(folder, true);
                        }
                        else
                        {
                            deleteFolder(folder, false);
                        }

                        break;

                    //Process
                    case 2:
                        string file = item.SubItems[2].Text.Split('|')[0];
                        string arg = item.SubItems[2].Text.Split('|')[1];
                        cMain.RunExternal(file, arg);
                        break;
                }
            }
        }

        private void bwClean_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cmdStart.Text = "Clean";
            cmdStart.Enabled = true;
            lstCleanup.Enabled = true;
            lblStatus.Text = "Cleaned.";

        }

    }
}
