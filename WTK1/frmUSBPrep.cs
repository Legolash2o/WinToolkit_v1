using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Management;
using System.Threading;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;
using WinToolkit.Properties;

namespace WinToolkit
{
    public partial class frmUSBPrep : Form
    {
        private readonly ManagementEventWatcher watcher = new ManagementEventWatcher();

        private string D = "0", P = "1";
        private bool PreventS;
        private bool Quick, Scanning;

        public frmUSBPrep()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            CheckForIllegalCrossThreadCalls = false;
            Shown += frmUSBPrep_Shown;
            FormClosing += frmUSBPrep_FormClosing;
            FormClosed += frmUSBPrep_FormClosed;
            bwUSB.RunWorkerCompleted += bwUSB_RunWorkerCompleted;
            Activated += cMain.FormActivated;
            Deactivate += cMain.FormActivated;
            MouseWheel += MouseScroll;

            try
            {
                var query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2 OR EventType = 3");
                //*2=Insert,3 = remove
                watcher.EventArrived += watcher_EventArrived;
                watcher.Query = query;
                watcher.Start();
            }
            catch (Exception Ex)
            {
                LargeError LE = new LargeError("EventWatcher", "Unable to start EventWatcher.", lblStatus.Text, Ex);
                LE.Upload();
            }
        }

        private void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            lstDisk.Items.Clear();
            Scanning = true;
            cmdQuick.Visible = false;
            cmdStart.Visible = false;
            cboFS.Visible = false;

            cMain.UpdateToolStripLabel(lblStatus, "Waiting for devices...");
            Application.DoEvents();

            watcher.Stop();
            Thread.Sleep(500);

            Scan();
            watcher.Start();
        }

        private void frmUSBPrep_Load(object sender, EventArgs e)
        {
            splitContainer1.Scale4K(_4KHelper.Panel.Pan2);
            Scanning = true;
            cMain.FormIcon(this); cMain.eLBL = lblStatus; cMain.eForm = this;
            cMain.ToolStripIcons(ToolStrip1);
            cboFS.SelectedIndex = 0;
        }

        private void MouseScroll(object sender, MouseEventArgs e)
        {
            lstDisk.Select();
        }

        private void frmUSBPrep_Shown(object sender, EventArgs e)
        {

            Scan();

            cMain.FreeRAM();
        }

        private void frmUSBPrep_FormClosing(object sender, FormClosingEventArgs e)
        {
            cMain.OnFormClosing(this);
            if (Scanning)
            {
                MessageBox.Show("Please wait whilst Win Toolkit finishes scanning for USB sticks, it shouldn't take long.",
                                     "WAIT");
                e.Cancel = true;
                return;
            }

            if (bwUSB.IsBusy)
            {
                MessageBox.Show("You can't close this while your USB is being processes!", "STOP");
                e.Cancel = true;
            }
        }

        private void frmUSBPrep_FormClosed(object sender, FormClosedEventArgs e)
        {
            watcher.Stop();
            cMain.ReturnME();
        }

        private void Scan()
        {
            Scanning = true;
            cMain.UpdateToolStripLabel(lblStatus, "Searching for devices...");
            Application.DoEvents();
            lstDisk.Items.Clear();
            lstDisk.Groups.Clear();

            cmdQuick.Visible = false;
            cmdStart.Visible = false;
            cboFS.Visible = false;
            cmdRefresh.Enabled = false;
            try
            {
                foreach (ManagementObject drive in new ManagementObjectSearcher("select * from Win32_DiskDrive where InterfaceType='USB'").Get())
                {
                    try
                    {
                        string Drive = drive.GetPropertyValue("Index").ToString();

                        string DriveLetter = "None";
                        string Label = "";
                        string FileSystem = "";
                        string UsedS = "";
                        string FreeS = "";
                        string SizeS = "";
                        string Status = drive.GetPropertyValue("Status").ToString();
                        string MediaType = drive.GetPropertyValue("MediaType").ToString();

                        var LVG = new ListViewGroup();
                        LVG.Header = drive.GetPropertyValue("Model") + " :: " + cMain.BytesToString(Convert.ToDouble(drive.GetPropertyValue("Size")));
                        lstDisk.Groups.Add(LVG);

                        if (Convert.ToInt32(drive.GetPropertyValue("Partitions")) > 0)
                        {
                            foreach (ManagementObject partition in drive.GetRelated("Win32_DiskPartition"))
                            {
                                int Partition = Convert.ToInt32(partition.GetPropertyValue("Index")) + 1;

                                string Bootable = partition.GetPropertyValue("Bootable").ToString();
                                bool LargerThan32GB = false; ;
                                foreach (ManagementObject logical in partition.GetRelated("Win32_LogicalDisk"))
                                {
                                    DriveLetter = logical.GetPropertyValue("Name").ToString().ReplaceIgnoreCase(":", "");
                                    try
                                    {
                                        Label = logical.GetPropertyValue("VolumeName").ToString();
                                    }
                                    catch { }

                                    try
                                    {
                                        FileSystem = logical.GetPropertyValue("FileSystem").ToString();
                                        double Free = Convert.ToDouble(logical.GetPropertyValue("FreeSpace"));
                                        double size = Convert.ToDouble(logical.GetPropertyValue("Size"));
                                        FreeS = cMain.BytesToString(Free);
                                        SizeS = cMain.BytesToString(size);
                                        if (size > 34359738368) { LargerThan32GB = true; }
                                        UsedS = cMain.BytesToString(size - Free);
                                    }
                                    catch
                                    {
                                        FileSystem = "RAW";
                                        double size = Convert.ToDouble(drive.GetPropertyValue("Size"));

                                        SizeS = cMain.BytesToString(size);
                                    }
                                }
                                var NI = new ListViewItem();
                                NI.Text = DriveLetter;
                                NI.SubItems.Add(Drive);
                                NI.SubItems.Add(Partition.ToString(CultureInfo.InvariantCulture));
                                NI.SubItems.Add(Label);
                                NI.SubItems.Add(FileSystem);
                                NI.SubItems.Add(UsedS);
                                NI.SubItems.Add(FreeS);
                                NI.SubItems.Add(SizeS);
                                NI.SubItems.Add(MediaType);
                                NI.SubItems.Add(Bootable);
                                NI.SubItems.Add(LargerThan32GB.ToString());
                                NI.Group = LVG;
                                if (Bootable.EqualsIgnoreCase("True") && !FileSystem.EqualsIgnoreCase("RAW") && !DriveLetter.EqualsIgnoreCase("None"))
                                {
                                    NI.BackColor = Color.LightGreen;
                                }
                                else
                                {
                                    NI.BackColor = Color.Yellow;
                                }

                                NI.SubItems.Add(Status);
                                NI.SubItems.Add("*");
                                lstDisk.Items.Add(NI);
                            }
                        }
                        else
                        {
                            double size = Convert.ToDouble(drive.GetPropertyValue("Size"));
                            SizeS = cMain.BytesToString(size);

                            var NI = new ListViewItem();
                            NI.Text = "None";
                            NI.SubItems.Add(Drive);
                            NI.SubItems.Add("");
                            NI.SubItems.Add("");
                            NI.SubItems.Add(FileSystem);
                            NI.SubItems.Add(UsedS);
                            NI.SubItems.Add(FreeS);
                            NI.SubItems.Add(SizeS);
                            NI.SubItems.Add(MediaType);
                            NI.SubItems.Add("False");
                            NI.SubItems.Add("No Partitions");
                            //NI.SubItems.Add("#");
                            NI.BackColor = Color.LightPink;
                            NI.Group = LVG;
                            lstDisk.Items.Add(NI);
                        }
                    }
                    catch { }
                    cMain.FreeRAM();
                }
            }
            catch (Exception Ex)
            {
                cMain.UpdateToolStripLabel(lblStatus, "Error scanning for USB drives ...");
                cMain.ErrorBox("Win Toolkit encountered an error whilst scanning for USB devices.", "USB Scan Error", Ex.Message);
                cMain.WriteLog(this, "Error scanning for USB", Ex.Message, lblStatus.Text);
            }
            foreach (ColumnHeader CH in lstDisk.Columns)
            {
                CH.Width = -2;
            }
            if (PreventS == false)
            {
                if (lstDisk.Items.Count > 0)
                {
                    cboFS.Visible = true;
                    cmdStart.Visible = true;
                    cmdQuick.Visible = true;
                    cMain.UpdateToolStripLabel(lblStatus, "");
                }
                else
                {
                    cMain.UpdateToolStripLabel(lblStatus, "No disks detected...");
                }
            }
            cmdRefresh.Enabled = true;
            Scanning = false;
            Application.DoEvents();
            PreventS = false;
            cMain.FreeRAM();
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            if (lstDisk.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a USB Drive first!", "Invalid USB");
                return;
            }
            if (lstDisk.SelectedItems[0].SubItems[1].Text.EqualsIgnoreCase("0"))
            {
                MessageBox.Show(
                     "There seems an issue with detection which would have resulted in formatting the system drive!",
                     "Aborted!");
                return;
            }
         
            DialogResult DR =
                 MessageBox.Show(
                      "THIS WILL DELETE EVERYTHING FROM YOUR USB INCLUDING ALL OF YOUR PARTITIONS, DO YOU WISH TO CONTINUE?" +
                      Environment.NewLine + Environment.NewLine +
                       lstDisk.SelectedItems[0].Text + ":\\ " + lstDisk.SelectedItems[0].SubItems[3].Text +
                      Environment.NewLine + Environment.NewLine +
                      "WARNING: DO NOT REMOVE ANY USB DEVICES UNTIL THE PROCESS HAS FINISHED!!", "WARNING",
                      MessageBoxButtons.YesNo);

            if (DR != DialogResult.Yes)
            {
                return;
            }

            cmdStart.Visible = false;
            cmdQuick.Visible = false;
            lstDisk.Enabled = false;
            cboFS.Visible = false;
            cmdRefresh.Visible = false;
            Quick = false;

            bwUSB.RunWorkerAsync();
            cMain.FreeRAM();
        }

        private string PrepUSB(string DriveL, string Label, string FS)
        {
            string Tas = "";
            Tas += "Select Disk " + lstDisk.SelectedItems[0].SubItems[1].Text + Environment.NewLine;
            if (Quick == false)
            {
                Tas += "CLEAN" + Environment.NewLine;
            }
            if (Quick == false)
            {
                Tas += "CREATE PARTITION PRIMARY ALIGN=512" + Environment.NewLine;
            }
            D = lstDisk.SelectedItems[0].SubItems[1].Text;
            if (string.IsNullOrEmpty(lstDisk.SelectedItems[0].SubItems[2].Text))
            {
                Tas += "SELECT PARTITION 1" + Environment.NewLine;
                P = "1";
            }
            else
            {
                Tas += "SELECT PARTITION " + lstDisk.SelectedItems[0].SubItems[2].Text + Environment.NewLine;
                P = lstDisk.SelectedItems[0].SubItems[2].Text;
            }

            if (Quick == false && lstDisk.SelectedItems[0].Text != DriveL)
            {
                Tas += "REMOVE" + Environment.NewLine;
            }
            if (Quick == false || lstDisk.SelectedItems[0].SubItems[4].Text.EqualsIgnoreCase("RAW") || string.IsNullOrEmpty(lstDisk.SelectedItems[0].SubItems[4].Text))
            {
                Tas += "FORMAT FS=" + FS + " LABEL=\"" + Label + "\" QUICK" + Environment.NewLine;
            }
            Tas += "ACTIVE" + Environment.NewLine;
            if (Quick == false || lstDisk.SelectedItems[0].Text.EqualsIgnoreCase("None"))
            {
                Tas += "ASSIGN" + Environment.NewLine;
            }

            Tas += "EXIT";

            var SW = new StreamWriter("C:\\T.txt");
            SW.Write(Tas);
            SW.Close();
            string R = cMain.RunExternal("\"" + cMain.SysFolder + "\\DiskPart.exe\"", "/s C:\\T.txt");
            Files.DeleteFile("C:\\T.txt");
            return R;
        }

        private void bwUSB_DoWork(object sender, DoWorkEventArgs e)
        {
            watcher.Stop();
            string F = lstDisk.SelectedItems[0].Text + ":";

            cMain.UpdateToolStripLabel(lblStatus, "Waiting for user input...");

            string L = "";

            if (Quick == false)
            {
                L = cMain.InputBox("If you would like to enter a custom label for the USB drive, please enter it here or just leave it blank.", "USB Name", "USB BOOT");
            }
            string CP = lstDisk.SelectedItems[0].Group.Header + " | Disk " + lstDisk.SelectedItems[0].SubItems[1].Text + " Partition " + lstDisk.SelectedItems[0].SubItems[2].Text;

            cMain.UpdateToolStripLabel(lblStatus, "Preparing USB [" + CP + "]...");
            string sFormat = lstDisk.SelectedItems[0].SubItems[4].Text;
            string Prep = PrepUSB(lstDisk.SelectedItems[0].Text, L, cboFS.Text);

            if (Prep.ContainsIgnoreCase("100 ") && cboFS.Text.EqualsIgnoreCase("NTFS")) { sFormat = "NTFS"; }

            if (!Prep.ContainsIgnoreCase("100 "))
            {
                cMain.UpdateToolStripLabel(lblStatus, "Preparing USB #2 [" + CP + "]...");
                Prep = PrepUSB("None", L, "FAT32");
                sFormat = "FAT32";

            }

            if (cboFS.Text.EqualsIgnoreCase("NTFS") && !sFormat.EqualsIgnoreCase("NTFS"))
            {
                cMain.UpdateToolStripLabel(lblStatus, "Converting to NTFS [" + CP + "]...");
                cMain.OpenProgram("\"" + cMain.SysFolder + "\\Convert.exe\"", lstDisk.SelectedItems[0].Text + ": /FS:NTFS /NoSecurity", true, ProcessWindowStyle.Hidden);
            }

            cMain.FreeRAM();
            PreventS = true;

            foreach (ListViewItem LST in lstDisk.Items)
            {
                if (LST.SubItems[1].Text == D && LST.SubItems[2].Text == P)
                {
                    cMain.UpdateToolStripLabel(lblStatus, "Running bootsect...");
                    if (File.Exists(cMain.SysFolder + "\\bootsect.exe"))
                    {
                        cMain.RunExternal("\"" + cMain.SysFolder + "\\bootsect.exe\"", "/nt60 /mbr " + LST.Text + ":");
                    }
                    else
                    {
                        cMain.WriteResource(Resources.bootsect, cMain.UserTempPath + "\\bootsect.exe", this);
                        if (File.Exists(cMain.UserTempPath + "\\bootsect.exe"))
                        {
                            cMain.RunExternal("\"" + cMain.UserTempPath + "\\bootsect.exe\"", "/nt60 /mbr " + LST.Text + ":");
                        }
                    }
                    LST.Selected = true;
                    break;
                }
            }
            PreventS = true;
            Scan();
            cMain.FreeRAM();
            cMain.UpdateToolStripLabel(lblStatus, "");
            MessageBox.Show("Your USB stick is ready, you can now simply copy and paste the Windows DVD onto your USB!", "Congrats");
        }

        private void bwUSB_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            watcher.Start();
            lstDisk.Enabled = true;
            cmdStart.Visible = true;
            cmdQuick.Visible = true;
            cboFS.Visible = true;
            cmdRefresh.Visible = true;
            cMain.FreeRAM();
        }

        private void cmdQuick_Click(object sender, EventArgs e)
        {
            if (lstDisk.SelectedItems.Count != 1)
            {
                MessageBox.Show("Please select a USB Drive first!", "Invalid USB");
                return;
            }

            DialogResult msg =
                MessageBox.Show(
                    "THIS WILL ERASE ALL CONTENTS ON THE USB DEVICE AND IS IRREVERSIBLE!\n\n" + lstDisk.SelectedItems[0].Text + ":\\ " + lstDisk.SelectedItems[0].SubItems[3].Text + "\n\nDO YOU WISH TO CONTINUE?",
                    "WARNING", MessageBoxButtons.YesNo);

            if (msg != DialogResult.Yes)
                return;

            if (lstDisk.SelectedItems[0].SubItems[1].Text.EqualsIgnoreCase("0"))
            {
                MessageBox.Show(
                     "There seems an issue with detection which would have resulted in formatting the system drive!",
                     "Aborted!");
                return;
            }

            if (lstDisk.SelectedItems[0].SubItems[4].Text.EqualsIgnoreCase("NTFS") && cboFS.Text.EqualsIgnoreCase("FAT32"))
            {
                DialogResult DR = MessageBox.Show("An NTFS volume cannot be converted back to FAT32 using the 'Quick' method.\n\nDo you wish to keep it as NTFS and continue?", "Attention", MessageBoxButtons.YesNo);

                if (DR == System.Windows.Forms.DialogResult.Yes) { cboFS.Text = "NTFS"; } else { return; }
            }

            cmdStart.Visible = false;
            cmdQuick.Visible = false;
            cboFS.Visible = false;
            lstDisk.Enabled = false;
            cmdRefresh.Visible = false;
            Quick = true;

            bwUSB.RunWorkerAsync();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            Scan();
        }

        private void lstDisk_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdQuick.Visible = true;

            if (lstDisk.Items.Count > 0 && lstDisk.SelectedItems.Count > 0)
            {
                if (lstDisk.SelectedItems[0].BackColor == Color.LightPink)
                {
                    cmdQuick.Visible = false;
                }
                cboFS.Items.Remove("FAT32");
                if (lstDisk.SelectedItems[0].SubItems[10].Text.EqualsIgnoreCase("False"))
                {
                    cboFS.Items.Add("FAT32");
                }
                cboFS.SelectedIndex = 0;
            }
            cMain.FreeRAM();
        }
    }
}