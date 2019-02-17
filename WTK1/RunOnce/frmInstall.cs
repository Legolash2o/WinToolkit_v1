using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Linq;

namespace RunOnce
{
    public partial class FrmInstall : Form
    {

        public int WAIT_FOR_USER = 0;

        private bool Finished = false;
        bool bSelectedItems;


        private Task[] Tasks = new Task[2];
        bool bCancel = false;

        public FrmInstall()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.W7T_128;
            CheckForIllegalCrossThreadCalls = false;
        }

        #region UpdateGUI
        private int CountProgress(ListView LV)
        {
            int iCount = 0;
            foreach (ListViewItem LST in LV.Items)
            {
                if (LST.ImageIndex != -1) { iCount++; }
            }
            return iCount;
        }
        private int CountProgress(List<ListViewItem> LV)
        {
            int iCount = 0;
            foreach (ListViewItem LST in LV)
            {
                if (LST.ImageIndex != -1) { iCount++; }
            }
            return iCount;
        }

        private void UpdatePercentage()
        {

            try
            {
                int Total = lstInstalling.Items.Count + lstDrivers.Items.Count + global.ManualInstalls.Count + global.AutoInstalls.Count;
                decimal Auto = CountProgress(global.AutoInstalls);
                decimal Installs = CountProgress(lstInstalling);
                decimal Commands = CountProgress(global.PostCommands);
                decimal Drivers = CountProgress(lstDrivers);

                decimal P = ((Auto + Installs + Drivers + Commands) / Total) * 100;
                cFunctions.UpdateLabel(this, "RunOnce Installer [" + P.ToString("0.0") + "%]");
            }
            catch { }
        }

        private void UpdateEntry(ListView lv, string sFilter, int iImageIdx)
        {

            foreach (ListViewItem LST in lv.Items.Cast<ListViewItem>().Where(S => S.SubItems[1].Text == sFilter))
            {
                LST.EnsureVisible();
                lv.BeginInvoke((Action)(() =>
                {
                    LST.ImageIndex = iImageIdx;
                }));

            }
        }
        #endregion

        #region Threads

        private void threadInstall_DoWork()
        {
            int i = 1;
            foreach (ListViewItem LST in lstInstalling.Items.Cast<ListViewItem>().Where(s => s.ImageIndex == -1))
            {

                cFunctions.UpdateLabel(tabPrograms, "Programs [" + i + " / " + lstInstalling.Items.Count + "]");
                cFunctions.WriteLog("Installing Software #" + i + ": " + LST.Text);

                try
                {
                    if (File.Exists(global.SysFolder + "\\mswinsck.ocx") && new FileInfo(global.SysFolder + "\\mswinsck.ocx").IsReadOnly)
                    {
                        File.SetAttributes(global.SysFolder + "\\mswinsck.ocx", FileAttributes.Normal);
                    }
                }
                catch { }

                try
                {
                    UpdateEntry(lstInstalling, LST.SubItems[1].Text, 1);

                    LST.EnsureVisible();
                    if (!File.Exists(LST.SubItems[1].Text))
                    {
                        UpdateEntry(lstInstalling, LST.SubItems[1].Text, 2);
                    }
                    else
                    {
                        cFunctions.InstallFile(LST);
                        UpdateEntry(lstInstalling, LST.SubItems[1].Text, 0);
                    }
                }
                catch (Exception Ex)
                {
                    cFunctions.WriteLog("Error installing file:\n\n" + Ex.Message);
                }
                if (bCancel) { return; }
                UpdatePercentage();

                i++;
            }

            if (global.ManualInstalls.Count > 0)
            {
                try
                {
                    while (bSelectedItems == false)
                    {

                        if (WAIT_FOR_USER > 0)
                        {
                            try
                            {
                                if (TimeSpan.FromMilliseconds(IdleTimeFinder.GetIdleTime()).Minutes >= WAIT_FOR_USER) { break; }
                            }
                            catch (Exception Ex)
                            {
                                cFunctions.WriteLog("Error getting idle: " + Ex.Message);
                            }
                        }
                        System.Threading.Thread.Sleep(500);
                        if (bCancel) { return; }
                    }
                }
                catch (Exception Ex)
                {
                    cFunctions.WriteLog("Error waiting: " + Ex.Message);
                }

                if (bCancel) { return; }


                foreach (ListViewItem LST in lstInstalling.Items.Cast<ListViewItem>().Where(s => s.ImageIndex == -1))
                {

                    try
                    {
                        cFunctions.WriteLog("Installing Software *" + i + ": " + LST.Text);
                        cFunctions.UpdateLabel(tabPrograms, "Programs [" + i + " / " + lstInstalling.Items.Count + "]");

                        LST.EnsureVisible();
                        UpdateEntry(lstInstalling, LST.SubItems[1].Text, 1);
                        if (!File.Exists(LST.SubItems[1].Text))
                        {
                            UpdateEntry(lstInstalling, LST.SubItems[1].Text, 2);
                        }
                        else
                        {
                            cFunctions.InstallFile(LST);
                            UpdateEntry(lstInstalling, LST.SubItems[1].Text, 0);
                        }
                    }
                    catch (Exception Ex)
                    {
                        cFunctions.WriteLog("Error installing file:\n\n" + Ex.Message);
                    }
                    if (bCancel) { break; }
                    UpdatePercentage();
                    i++;

                }

                if (bCancel) { return; }

                foreach (ListViewItem LST in global.PostCommands)
                {
                    cFunctions.WriteLog("Running Post-Command *" + i + ": " + LST.Text);
                    cFunctions.UpdateLabel(tabPrograms, "Command [" + i + " / " + lstInstalling.Items.Count + "]");
                    UpdateEntry(lstInstalling, LST.SubItems[1].Text, 1);
                    try
                    {
                        cFunctions.InstallFile(LST);
                        UpdateEntry(lstInstalling, LST.SubItems[1].Text, 0);
                    }
                    catch (Exception Ex)
                    {
                        UpdateEntry(lstInstalling, LST.SubItems[1].Text, 2);
                        cFunctions.WriteLog("Error running command:\n\n" + Ex.Message);
                    }
                }
            }
        }

        private void threadDrivers_DoWork()
        {
            cFunctions.DeleteFile(global.Root + "dpinst.exe");
            if (Directory.Exists(global.SysRoot + "\\SysWOW64"))
            {
                cFunctions.WriteResource(Properties.Resources.dpinst64, global.Root + "dpinst.exe");
            }
            else
            {
                cFunctions.WriteResource(Properties.Resources.dpinst32, global.Root + "dpinst.exe");
            }

            foreach (ListViewItem LST in lstDrivers.Items)
            {
                if (bCancel) { break; }
                try
                {
                    if (LST.ImageIndex == -1)
                    {
                        cFunctions.UpdateLabel(tabDrivers, "Drivers [" + (LST.Index + 1) + " / " + lstDrivers.Items.Count + "]");
                        cFunctions.WriteLog("Installing Driver: " + LST.Text);

                        UpdateEntry(lstDrivers, LST.SubItems[1].Text, 1);

                        if (!Directory.Exists(LST.SubItems[1].Text))
                        {
                            UpdateEntry(lstDrivers, LST.SubItems[1].Text, 2);
                        }
                        else
                        {


                            int returnCode = cFunctions.OpenProgram("\"" + global.Root + "dpinst.exe\"", " /SH /LM /SA /SW /S /SE /D /PATH \"" + LST.SubItems[1].Text.Trim(), true, System.Diagnostics.ProcessWindowStyle.Hidden);
                            if (returnCode == 0)
                            {
                                UpdateEntry(lstDrivers, LST.SubItems[1].Text, 3);
                            }
                            else
                            {
                                UpdateEntry(lstDrivers, LST.SubItems[1].Text, 0);
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    cFunctions.WriteLog("Error installing driver:" + LST.SubItems[1].Text + "\n" + Ex.Message);
                }
                UpdatePercentage();
            }

            cFunctions.DeleteFile(global.Root + "dpinst.exe");
        }

        #endregion

        #region FormEvents
        private void frmInstall_Load(object sender, EventArgs e)
        {


            foreach (var arg in Environment.GetCommandLineArgs().Where(arg => arg.Contains(":")))
            {
                string part1 = arg.Split(':')[0];
                string part2 = arg.Split(':')[1];

                if (part1.Equals("/TIME", StringComparison.InvariantCultureIgnoreCase))
                {
                    cFunctions.WriteLog("Idle Timer: " + part2);
                    WAIT_FOR_USER = int.Parse(part2);
                }
            }

            label1.Text += " v" + cFunctions.WinToolkitVersion(true);

            foreach (var LST in global.AutoInstalls) { cFunctions.WriteLog("Auto List: " + LST.Text); lstInstalling.Items.Add(LST); }
            if (global.DriverInstalls.Count == 0)
            {
                tabControl1.TabPages.Remove(tabDrivers);
                cFunctions.WriteLog("No Drivers");
            }
            else
            {
                foreach (var LST in global.DriverInstalls) { cFunctions.WriteLog("Driver List: " + LST.Text); lstDrivers.Items.Add(LST); }
            }


            if (global.ManualInstalls.Count > 0)
            {
                foreach (var LST in global.ManualInstalls) { cFunctions.WriteLog("Manual List: " + LST.Text); lstManual.Items.Add(LST); }
            }
            else
            {
                tabControl1.TabPages.Remove(tabSelect);
                cFunctions.WriteLog("No Manual.");
                bSelectedItems = true;
            }

            if (lstInstalling.Items.Count == 0)
            {
                cFunctions.WriteLog("No Programs");
                tabControl1.TabPages.Remove(tabPrograms);
            }
        }

        private void threadsFinished(Task[] tasks)
        {
            cFunctions.WriteLog("Tasks Finished");
            bCancel = true;
            Finished = true;
            Close();
        }

        private void frmInstall_Shown(object sender, EventArgs e)
        {
            Tasks[0] = Task.Factory.StartNew(threadInstall_DoWork);
            Tasks[1] = Task.Factory.StartNew(threadDrivers_DoWork);

            Task.Factory.ContinueWhenAll(Tasks, threadsFinished);
            cFunctions.WriteLog("Tasks Started");
            //Task.WaitAll(Tasks);



        }

        private void frmInstall_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (Finished)
            {
                bCancel = true;
                cFunctions.WriteLog("Finishing");
                return;
            }
            try
            {
                if (e.CloseReason.Equals(CloseReason.WindowsShutDown))
                {
                    cFunctions.OpenProgram("Shutdown.exe", "-a", true, System.Diagnostics.ProcessWindowStyle.Hidden);
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception Ex)
            {
                cFunctions.WriteLog("Error WindowsShutDown: " + Ex.Message);
            }

            try
            {
                if (!Finished && e.CloseReason.Equals(CloseReason.UserClosing))
                {
                    var DR = MessageBox.Show("Closing this window will cancel all installations. Do you wish to cancel all items?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    if (DR != DialogResult.Yes) { e.Cancel = true; return; }
                    if (tabControl1.Contains(tabSelect)) { tabControl1.TabPages.Remove(tabSelect); }
                    bCancel = true;
                    cFunctions.UpdateLabel(label2, "Please wait for current files to finish...");
                    cFunctions.UpdateLabel(this, "Aborting...");
                    ControlBox = false;
                    e.Cancel = true;

                    return;
                }
            }
            catch (Exception Ex)
            {
                cFunctions.WriteLog("Error UserClosing: " + Ex.Message);
            }

            cFunctions.WriteLog("Finishing");
        }
        #endregion

        #region ButtonEvents
        private void cmdStart_Click(object sender, EventArgs e)
        {
            if (lstManual.CheckedItems.Count == 0)
            {
                DialogResult DR = MessageBox.Show("You are about to install without any manual installs. Do you wish to continue?\n\nOnce you click 'Yes' you can't change your mind.", "Are you sure?", MessageBoxButtons.YesNo);
                if (DR != DialogResult.Yes) { return; }
                lstManual.Enabled = false;
                cmdStart.Enabled = false;
            }
            foreach (ListViewItem LST in lstManual.CheckedItems)
            {
                ListViewItem nLST = (ListViewItem)LST.Clone();
                cFunctions.WriteLog("Selected List: " + nLST.Text);
                lstInstalling.Items.Add(nLST);
            }
            if (lstManual.CheckedItems.Count > 0)
            {

                if (!tabControl1.TabPages.Contains(tabPrograms))
                {
                    tabControl1.TabPages.Insert(0, tabPrograms);
                }
                tabControl1.SelectedTab = tabPrograms;
            }
            tabControl1.TabPages.Remove(tabSelect);
            UpdatePercentage();
            bSelectedItems = true;
        }

        private void mnuSelectAll_Click(object sender, EventArgs e)
        {
            cmdSelectAll.PerformClick();
        }

        private void mnuDeselectAll_Click(object sender, EventArgs e)
        {
            cmdDeselectAll.PerformClick();
        }

        private void cmdSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LST in lstManual.Items)
            {
                LST.Checked = true;
            }
        }

        private void cmdDeselectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem LST in lstManual.Items)
            {
                LST.Checked = false;
            }
        }
        #endregion
    }
}
