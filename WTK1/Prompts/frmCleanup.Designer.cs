namespace WinToolkit.Classes
{
    partial class frmCleanup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Logs", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Generic", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("DISM", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Components", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Advanced", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Administrator User Folder",
            "N/A",
            "Users\\Administrator"}, 1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Boot Configuration Data",
            "N/A",
            "BCD"}, 1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "CBS Logs",
            "N/A",
            "Windows\\Logs\\CBS\\*"}, 1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "DISM Logs",
            "N/A",
            "Windows\\Logs\\DISM"}, 1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "Hibernatation File",
            "N/A",
            "hiberfil.sys"}, 0);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "InstallShield Installation Info",
            "N/A",
            "Program Files (x86)\\InstallShield Installation Information|Program Files\\InstallS" +
                "hield Installation Information"}, 1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "MSP Patch Files",
            "N/A",
            "*.msp"}, 0);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "Offline Files",
            "N/A",
            "Windows\\CSC"}, 1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "Old Files",
            "N/A",
            "*.old"}, 0);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "Page File",
            "N/A",
            "pagefile.sys"}, 0);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
            "Patch Cache",
            "N/A",
            "Windows\\Installer\\$PatchCache$\\Managed\\*"}, 1);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
            "PBR Logs",
            "N/A",
            "Windows\\Logs\\PBR"}, 1);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
            "PerfLogs",
            "N/A",
            "PerfLogs"}, 1);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
            "PNF Files",
            "N/A",
            "Windows\\inf\\*.pnf"}, 0);
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
            "Prefetch",
            "N/A",
            "Windows\\Prefetch\\*"}, 1);
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(new string[] {
            "Recycle Bin",
            "N/A",
            "$Recycle.Bin"}, 1);
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem(new string[] {
            "Sample Music",
            "N/A",
            "Users\\Public\\Music\\Sample Music"}, 1);
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem(new string[] {
            "Sample Pictures",
            "N/A",
            "Users\\Public\\Pictures\\Sample Pictures"}, 1);
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem(new string[] {
            "Sample Recorded",
            "N/A",
            "Users\\Public\\Recorded TV\\Sample Media"}, 1);
            System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem(new string[] {
            "Sample Videos",
            "N/A",
            "Users\\Public\\Videos\\Sample Videos"}, 1);
            System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem(new string[] {
            "SoftwareDistribution",
            "N/A",
            "Windows\\SoftwareDistribution"}, 1);
            System.Windows.Forms.ListViewItem listViewItem22 = new System.Windows.Forms.ListViewItem(new string[] {
            "Swap File",
            "N/A",
            "swapfile.sys"}, 0);
            System.Windows.Forms.ListViewItem listViewItem23 = new System.Windows.Forms.ListViewItem(new string[] {
            "Sysprep_succeeded.tag",
            "N/A",
            "Windows\\System32\\Sysprep\\Sysprep_succeeded.tag"}, 0);
            System.Windows.Forms.ListViewItem listViewItem24 = new System.Windows.Forms.ListViewItem(new string[] {
            "Temp",
            "N/A",
            "Windows\\Temp\\*"}, 1);
            System.Windows.Forms.ListViewItem listViewItem25 = new System.Windows.Forms.ListViewItem(new string[] {
            "Temp User Folder",
            "N/A",
            "Users\\Temp"}, 1);
            System.Windows.Forms.ListViewItem listViewItem26 = new System.Windows.Forms.ListViewItem(new string[] {
            "Uninstall Information",
            "N/A",
            "Program Files (x86)\\Uninstall Information|Program Files\\Uninstall Information"}, 1);
            System.Windows.Forms.ListViewItem listViewItem27 = new System.Windows.Forms.ListViewItem(new string[] {
            "Windows Event Logs",
            "N/A",
            "Windows\\System32\\winevt\\Logs\\*"}, 1);
            System.Windows.Forms.ListViewItem listViewItem28 = new System.Windows.Forms.ListViewItem(new string[] {
            "WinSXS Backup",
            "N/A",
            "Windows\\WinSxS\\Backup\\*"}, 1);
            System.Windows.Forms.ListViewItem listViewItem29 = new System.Windows.Forms.ListViewItem(new string[] {
            "WinSXS Manifest Cache",
            "N/A",
            "Windows\\WinSxS\\ManifestCache\\*"}, 1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCleanup));
            this.lstCleanup = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lblPath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdStart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.bwGetSize = new System.ComponentModel.BackgroundWorker();
            this.bwClean = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstCleanup
            // 
            this.lstCleanup.CheckBoxes = true;
            this.lstCleanup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1});
            this.lstCleanup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCleanup.FullRowSelect = true;
            listViewGroup1.Header = "Logs";
            listViewGroup1.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup1.Name = "gbLogs";
            listViewGroup2.Header = "Generic";
            listViewGroup2.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup2.Name = "gbGeneric";
            listViewGroup3.Header = "DISM";
            listViewGroup3.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup3.Name = "gbDISM";
            listViewGroup4.Header = "Components";
            listViewGroup4.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup4.Name = "gbComponents";
            listViewGroup5.Header = "Advanced";
            listViewGroup5.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup5.Name = "gbAdvanced";
            this.lstCleanup.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4,
            listViewGroup5});
            listViewItem1.Group = listViewGroup5;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.Checked = true;
            listViewItem2.Group = listViewGroup2;
            listViewItem2.StateImageIndex = 1;
            listViewItem3.Group = listViewGroup1;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.Checked = true;
            listViewItem4.Group = listViewGroup1;
            listViewItem4.StateImageIndex = 1;
            listViewItem5.Checked = true;
            listViewItem5.Group = listViewGroup2;
            listViewItem5.StateImageIndex = 1;
            listViewItem6.Checked = true;
            listViewItem6.Group = listViewGroup2;
            listViewItem6.StateImageIndex = 1;
            listViewItem7.Group = listViewGroup5;
            listViewItem7.StateImageIndex = 0;
            listViewItem8.Checked = true;
            listViewItem8.Group = listViewGroup2;
            listViewItem8.StateImageIndex = 1;
            listViewItem9.Checked = true;
            listViewItem9.Group = listViewGroup2;
            listViewItem9.StateImageIndex = 1;
            listViewItem9.ToolTipText = "*.old files";
            listViewItem10.Checked = true;
            listViewItem10.Group = listViewGroup2;
            listViewItem10.StateImageIndex = 1;
            listViewItem11.Group = listViewGroup5;
            listViewItem11.StateImageIndex = 0;
            listViewItem12.Checked = true;
            listViewItem12.Group = listViewGroup1;
            listViewItem12.StateImageIndex = 1;
            listViewItem13.Checked = true;
            listViewItem13.Group = listViewGroup1;
            listViewItem13.StateImageIndex = 1;
            listViewItem14.Checked = true;
            listViewItem14.Group = listViewGroup2;
            listViewItem14.StateImageIndex = 1;
            listViewItem14.ToolTipText = "*.pnf files";
            listViewItem15.Checked = true;
            listViewItem15.Group = listViewGroup2;
            listViewItem15.StateImageIndex = 1;
            listViewItem16.Checked = true;
            listViewItem16.Group = listViewGroup2;
            listViewItem16.StateImageIndex = 1;
            listViewItem17.Checked = true;
            listViewItem17.Group = listViewGroup2;
            listViewItem17.StateImageIndex = 1;
            listViewItem18.Checked = true;
            listViewItem18.Group = listViewGroup2;
            listViewItem18.StateImageIndex = 1;
            listViewItem19.Checked = true;
            listViewItem19.Group = listViewGroup2;
            listViewItem19.StateImageIndex = 1;
            listViewItem20.Checked = true;
            listViewItem20.Group = listViewGroup2;
            listViewItem20.StateImageIndex = 1;
            listViewItem21.Checked = true;
            listViewItem21.Group = listViewGroup2;
            listViewItem21.StateImageIndex = 1;
            listViewItem22.Checked = true;
            listViewItem22.Group = listViewGroup2;
            listViewItem22.StateImageIndex = 1;
            listViewItem23.Checked = true;
            listViewItem23.Group = listViewGroup2;
            listViewItem23.StateImageIndex = 1;
            listViewItem24.Checked = true;
            listViewItem24.Group = listViewGroup2;
            listViewItem24.StateImageIndex = 1;
            listViewItem25.Checked = true;
            listViewItem25.Group = listViewGroup2;
            listViewItem25.StateImageIndex = 1;
            listViewItem26.Checked = true;
            listViewItem26.Group = listViewGroup2;
            listViewItem26.StateImageIndex = 1;
            listViewItem27.Checked = true;
            listViewItem27.Group = listViewGroup2;
            listViewItem27.StateImageIndex = 1;
            listViewItem28.Checked = true;
            listViewItem28.Group = listViewGroup5;
            listViewItem28.StateImageIndex = 1;
            listViewItem29.Checked = true;
            listViewItem29.Group = listViewGroup5;
            listViewItem29.StateImageIndex = 1;
            this.lstCleanup.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17,
            listViewItem18,
            listViewItem19,
            listViewItem20,
            listViewItem21,
            listViewItem22,
            listViewItem23,
            listViewItem24,
            listViewItem25,
            listViewItem26,
            listViewItem27,
            listViewItem28,
            listViewItem29});
            this.lstCleanup.Location = new System.Drawing.Point(0, 0);
            this.lstCleanup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstCleanup.MultiSelect = false;
            this.lstCleanup.Name = "lstCleanup";
            this.lstCleanup.ShowItemToolTips = true;
            this.lstCleanup.Size = new System.Drawing.Size(868, 581);
            this.lstCleanup.SmallImageList = this.imageList1;
            this.lstCleanup.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstCleanup.TabIndex = 13;
            this.lstCleanup.UseCompatibleStateImageBehavior = false;
            this.lstCleanup.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 348;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Size";
            this.columnHeader1.Width = 159;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "File_icon.png");
            this.imageList1.Images.SetKeyName(1, "folder_closed.png");
            this.imageList1.Images.SetKeyName(2, "gearwheel_industry_mechanism_repair_settings_system._actions_business_circle_cog_" +
        "connection_technical-20.png");
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(15, 9);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(72, 74);
            this.pictureBox6.TabIndex = 12;
            this.pictureBox6.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ToolStrip1);
            this.splitContainer1.Panel2MinSize = 30;
            this.splitContainer1.Size = new System.Drawing.Size(876, 709);
            this.splitContainer1.SplitterDistance = 677;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 14;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lblPath);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.pictureBox6);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(876, 677);
            this.splitContainer2.SplitterDistance = 61;
            this.splitContainer2.SplitterWidth = 2;
            this.splitContainer2.TabIndex = 15;
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(98, 51);
            this.lblPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(0, 20);
            this.lblPath.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(96, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 33);
            this.label1.TabIndex = 13;
            this.label1.Text = "Image Cleanup";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(876, 614);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lstCleanup);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(868, 581);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtLog);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(868, 533);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(868, 533);
            this.txtLog.TabIndex = 0;
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.CanOverflow = false;
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdStart,
            this.toolStripSeparator2,
            this.lblStatus});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.ToolStrip1.Size = new System.Drawing.Size(876, 30);
            this.ToolStrip1.Stretch = true;
            this.ToolStrip1.TabIndex = 18;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // cmdStart
            // 
            this.cmdStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(141, 30);
            this.cmdStart.Tag = "OK";
            this.cmdStart.Text = "Queue Clean";
            this.cmdStart.ToolTipText = "Start Cleaning...";
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 30);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 27);
            // 
            // bwGetSize
            // 
            this.bwGetSize.WorkerSupportsCancellation = true;
            this.bwGetSize.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwGetSize_DoWork);
            // 
            // bwClean
            // 
            this.bwClean.WorkerSupportsCancellation = true;
            this.bwClean.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwClean_DoWork);
            this.bwClean.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwClean_RunWorkerCompleted);
            // 
            // frmCleanup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(876, 709);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(889, 739);
            this.Name = "frmCleanup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cleanup Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCleanup_FormClosing);
            this.Load += new System.EventHandler(this.frmCleanup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstCleanup;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem cmdStart;
        internal System.Windows.Forms.ToolStripLabel lblStatus;
        internal System.ComponentModel.BackgroundWorker bwGetSize;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label label1;
        internal System.ComponentModel.BackgroundWorker bwClean;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtLog;
    }
}