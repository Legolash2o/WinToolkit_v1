namespace WinToolkit
{
    partial class frmOptions
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
            System.Windows.Forms.ListViewGroup listViewGroup9 = new System.Windows.Forms.ListViewGroup("General", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup10 = new System.Windows.Forms.ListViewGroup("Startup and Exit", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup11 = new System.Windows.Forms.ListViewGroup("All-In-One Integrator", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup12 = new System.Windows.Forms.ListViewGroup("WIM Images", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup13 = new System.Windows.Forms.ListViewGroup("Logging", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem(new string[] {
            "Add RunOnce Installer",
            "True",
            "True"}, -1);
            System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem(new string[] {
            "Auto Save and Unmount",
            "True",
            "True"}, -1);
            System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem(new string[] {
            "Check for new DISM",
            "True",
            "True"}, -1);
            System.Windows.Forms.ListViewItem listViewItem22 = new System.Windows.Forms.ListViewItem(new string[] {
            "Check For Updates",
            "True",
            "True"}, -1);
            System.Windows.Forms.ListViewItem listViewItem23 = new System.Windows.Forms.ListViewItem(new string[] {
            "Check Images",
            "False",
            "False"}, -1);
            System.Windows.Forms.ListViewItem listViewItem24 = new System.Windows.Forms.ListViewItem(new string[] {
            "Delete Mount Folder",
            "True",
            "True"}, -1);
            System.Windows.Forms.ListViewItem listViewItem25 = new System.Windows.Forms.ListViewItem(new string[] {
            "Detect Antivirus",
            "True",
            "True"}, -1);
            System.Windows.Forms.ListViewItem listViewItem26 = new System.Windows.Forms.ListViewItem(new string[] {
            "Error Logging",
            "True",
            "True"}, -1);
            System.Windows.Forms.ListViewItem listViewItem27 = new System.Windows.Forms.ListViewItem(new string[] {
            "FreeRAM",
            "True",
            "True"}, -1);
            System.Windows.Forms.ListViewItem listViewItem28 = new System.Windows.Forms.ListViewItem(new string[] {
            "Mount Logging",
            "True",
            "True"}, -1);
            System.Windows.Forms.ListViewItem listViewItem29 = new System.Windows.Forms.ListViewItem(new string[] {
            "Prevent Sleep",
            "True",
            "True"}, -1);
            System.Windows.Forms.ListViewItem listViewItem30 = new System.Windows.Forms.ListViewItem(new string[] {
            "Quicker Merging",
            "True",
            "True"}, -1);
            System.Windows.Forms.ListViewItem listViewItem31 = new System.Windows.Forms.ListViewItem(new string[] {
            "Registry Logging",
            "False",
            "False"}, -1);
            System.Windows.Forms.ListViewItem listViewItem32 = new System.Windows.Forms.ListViewItem(new string[] {
            "Show Network Drives",
            "False",
            "False"}, -1);
            System.Windows.Forms.ListViewItem listViewItem33 = new System.Windows.Forms.ListViewItem(new string[] {
            "Show Preset Manager",
            "True",
            "True"}, -1);
            System.Windows.Forms.ListViewItem listViewItem34 = new System.Windows.Forms.ListViewItem(new string[] {
            "Update Catalog Filter",
            "True",
            "True"}, -1);
            System.Windows.Forms.ListViewItem listViewItem35 = new System.Windows.Forms.ListViewItem(new string[] {
            "Upload Error Logs",
            "True",
            "True"}, -1);
            System.Windows.Forms.ListViewItem listViewItem36 = new System.Windows.Forms.ListViewItem(new string[] {
            "Verify Images",
            "False",
            "False"}, -1);
            System.Windows.Forms.ListViewGroup listViewGroup14 = new System.Windows.Forms.ListViewGroup("Custom", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup15 = new System.Windows.Forms.ListViewGroup("System", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup16 = new System.Windows.Forms.ListViewGroup("Installed", System.Windows.Forms.HorizontalAlignment.Center);
            this.GBWinToolkitemp = new System.Windows.Forms.GroupBox();
            this.txtWinToolkitTemp = new System.Windows.Forms.TextBox();
            this.cmdTempRD = new System.Windows.Forms.Button();
            this.cmdTempBrowse = new System.Windows.Forms.Button();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.HSBO = new System.Windows.Forms.TrackBar();
            this.chkT = new System.Windows.Forms.CheckBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.lstOptions = new System.Windows.Forms.ListView();
            this.CHOptions = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.cboPriDISM = new System.Windows.Forms.ComboBox();
            this.cmdApply = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdClose = new System.Windows.Forms.ToolStripMenuItem();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabMain = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.cboLang = new System.Windows.Forms.ComboBox();
            this.TabProcesses = new System.Windows.Forms.TabPage();
            this.gbCleanup = new System.Windows.Forms.GroupBox();
            this.lblNoticeClean = new System.Windows.Forms.Label();
            this.chkACLogs = new System.Windows.Forms.CheckBox();
            this.chkACSXS = new System.Windows.Forms.CheckBox();
            this.chkACManifest = new System.Windows.Forms.CheckBox();
            this.chkACGeneric = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtSoLoR = new System.Windows.Forms.TextBox();
            this.cmdSoLoRD = new System.Windows.Forms.Button();
            this.cmdSoLoRB = new System.Windows.Forms.Button();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.txtMountTemp = new System.Windows.Forms.TextBox();
            this.cmdMountRD = new System.Windows.Forms.Button();
            this.cmdMountBrowse = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.cboPriWinToolkit = new System.Windows.Forms.ComboBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.cboPriExt = new System.Windows.Forms.ComboBox();
            this.TabMisc = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboScaling = new System.Windows.Forms.ComboBox();
            this.TabDISM = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.cmdAddDISM = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdRemoveDISM = new System.Windows.Forms.ToolStripMenuItem();
            this.lstDISM = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbCPUDism = new System.Windows.Forms.TrackBar();
            this.lblCPUDISM = new System.Windows.Forms.Label();
            this.TabFolders = new System.Windows.Forms.TabPage();
            this.gbDISMAff = new System.Windows.Forms.GroupBox();
            this.GBWinToolkitemp.SuspendLayout();
            this.GroupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HSBO)).BeginInit();
            this.GroupBox3.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.TabMain.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.TabProcesses.SuspendLayout();
            this.gbCleanup.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.TabMisc.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.TabDISM.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbCPUDism)).BeginInit();
            this.TabFolders.SuspendLayout();
            this.gbDISMAff.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBWinToolkitemp
            // 
            this.GBWinToolkitemp.Controls.Add(this.txtWinToolkitTemp);
            this.GBWinToolkitemp.Controls.Add(this.cmdTempRD);
            this.GBWinToolkitemp.Controls.Add(this.cmdTempBrowse);
            this.GBWinToolkitemp.Location = new System.Drawing.Point(3, 3);
            this.GBWinToolkitemp.Name = "GBWinToolkitemp";
            this.GBWinToolkitemp.Size = new System.Drawing.Size(298, 64);
            this.GBWinToolkitemp.TabIndex = 9;
            this.GBWinToolkitemp.TabStop = false;
            this.GBWinToolkitemp.Text = "Win Toolkit Temp Folder";
            // 
            // txtWinToolkitTemp
            // 
            this.txtWinToolkitTemp.Location = new System.Drawing.Point(6, 15);
            this.txtWinToolkitTemp.Name = "txtWinToolkitTemp";
            this.txtWinToolkitTemp.ReadOnly = true;
            this.txtWinToolkitTemp.Size = new System.Drawing.Size(285, 20);
            this.txtWinToolkitTemp.TabIndex = 0;
            this.txtWinToolkitTemp.TabStop = false;
            this.txtWinToolkitTemp.TextChanged += new System.EventHandler(this.txtWinToolkitTemp_TextChanged);
            // 
            // cmdTempRD
            // 
            this.cmdTempRD.Location = new System.Drawing.Point(128, 38);
            this.cmdTempRD.Name = "cmdTempRD";
            this.cmdTempRD.Size = new System.Drawing.Size(79, 23);
            this.cmdTempRD.TabIndex = 9;
            this.cmdTempRD.Text = "Default";
            this.cmdTempRD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdTempRD.UseVisualStyleBackColor = true;
            this.cmdTempRD.Click += new System.EventHandler(this.cmdTempRD_Click);
            // 
            // cmdTempBrowse
            // 
            this.cmdTempBrowse.Location = new System.Drawing.Point(213, 38);
            this.cmdTempBrowse.Name = "cmdTempBrowse";
            this.cmdTempBrowse.Size = new System.Drawing.Size(78, 23);
            this.cmdTempBrowse.TabIndex = 10;
            this.cmdTempBrowse.Text = "Browse";
            this.cmdTempBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdTempBrowse.UseVisualStyleBackColor = true;
            this.cmdTempBrowse.Click += new System.EventHandler(this.cmdTempBrowse_Click);
            // 
            // GroupBox5
            // 
            this.GroupBox5.Controls.Add(this.HSBO);
            this.GroupBox5.Controls.Add(this.chkT);
            this.GroupBox5.Location = new System.Drawing.Point(9, 70);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(297, 88);
            this.GroupBox5.TabIndex = 8;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "Transparency";
            // 
            // HSBO
            // 
            this.HSBO.BackColor = System.Drawing.SystemColors.Window;
            this.HSBO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HSBO.Location = new System.Drawing.Point(7, 37);
            this.HSBO.Maximum = 100;
            this.HSBO.Minimum = 40;
            this.HSBO.Name = "HSBO";
            this.HSBO.Size = new System.Drawing.Size(277, 45);
            this.HSBO.SmallChange = 2;
            this.HSBO.TabIndex = 4;
            this.HSBO.TickFrequency = 2;
            this.HSBO.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.HSBO.Value = 100;
            this.HSBO.Scroll += new System.EventHandler(this.HSBO_Scroll);
            // 
            // chkT
            // 
            this.chkT.AutoSize = true;
            this.chkT.Checked = true;
            this.chkT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkT.Location = new System.Drawing.Point(7, 19);
            this.chkT.Name = "chkT";
            this.chkT.Size = new System.Drawing.Size(127, 17);
            this.chkT.TabIndex = 3;
            this.chkT.Text = "Enable Transparency";
            this.chkT.UseVisualStyleBackColor = true;
            this.chkT.CheckedChanged += new System.EventHandler(this.chkT_CheckedChanged);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(6, 43);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(254, 13);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "Any apps that Win Toolkit needs to use (i.e. pkgmgr)";
            // 
            // lstOptions
            // 
            this.lstOptions.BackColor = System.Drawing.SystemColors.Window;
            this.lstOptions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstOptions.CheckBoxes = true;
            this.lstOptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CHOptions});
            this.lstOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOptions.FullRowSelect = true;
            listViewGroup9.Header = "General";
            listViewGroup9.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup9.Name = "LVGGeneral";
            listViewGroup10.Header = "Startup and Exit";
            listViewGroup10.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup10.Name = "LVGStart";
            listViewGroup11.Header = "All-In-One Integrator";
            listViewGroup11.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup11.Name = "LVGAIO";
            listViewGroup12.Header = "WIM Images";
            listViewGroup12.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup12.Name = "LVGWIM";
            listViewGroup13.Header = "Logging";
            listViewGroup13.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup13.Name = "LVGLogging";
            this.lstOptions.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup9,
            listViewGroup10,
            listViewGroup11,
            listViewGroup12,
            listViewGroup13});
            listViewItem19.Checked = true;
            listViewItem19.Group = listViewGroup12;
            listViewItem19.StateImageIndex = 1;
            listViewItem19.Tag = "chkmRunOnce";
            listViewItem19.ToolTipText = "This will add RunOnce installers when an image is mounted.";
            listViewItem20.Checked = true;
            listViewItem20.Group = listViewGroup11;
            listViewItem20.StateImageIndex = 1;
            listViewItem20.Tag = "chkAIOSave";
            listViewItem20.ToolTipText = "When multiple images have been selected, it automatically saves and unmounts the " +
    "images.";
            listViewItem21.Checked = true;
            listViewItem21.Group = listViewGroup10;
            listViewItem21.StateImageIndex = 1;
            listViewItem21.Tag = "chkDISM";
            listViewItem21.ToolTipText = "Prompts to show if a newer version of DISM is available or not.";
            listViewItem22.Checked = true;
            listViewItem22.Group = listViewGroup10;
            listViewItem22.StateImageIndex = 1;
            listViewItem22.Tag = "chkUpdates";
            listViewItem22.ToolTipText = "Checks for newer version on start.";
            listViewItem23.Group = listViewGroup12;
            listViewItem23.StateImageIndex = 0;
            listViewItem23.Tag = "chkmCheck";
            listViewItem23.ToolTipText = "This option checks that files have been copied correctly during saving and also c" +
    "hecks to make sure the WIM hasn\'t become corrupted. This may slow down some imag" +
    "e tasks and is rarely needed.";
            listViewItem24.Checked = true;
            listViewItem24.Group = listViewGroup12;
            listViewItem24.StateImageIndex = 1;
            listViewItem24.Tag = "chkMDeleteMount";
            listViewItem25.Checked = true;
            listViewItem25.Group = listViewGroup10;
            listViewItem25.StateImageIndex = 1;
            listViewItem25.Tag = "chkAV";
            listViewItem25.ToolTipText = "This will detect any antivirus on startup and warn you it may affect Win Toolkit " +
    "performance.";
            listViewItem26.Checked = true;
            listViewItem26.Group = listViewGroup13;
            listViewItem26.StateImageIndex = 1;
            listViewItem26.Tag = "chkDLogging";
            listViewItem26.ToolTipText = "Just basic logging of what tasks have been completed.";
            listViewItem27.Checked = true;
            listViewItem27.Group = listViewGroup9;
            listViewItem27.StateImageIndex = 1;
            listViewItem27.Tag = "chkFreeRAM";
            listViewItem27.ToolTipText = "Keep releasing available Win Toolkit memory.";
            listViewItem28.Group = listViewGroup13;
            listViewItem28.StateImageIndex = 0;
            listViewItem28.Tag = "chkMountLog";
            listViewItem28.ToolTipText = "Writes mount logs after each mount/unmount.";
            listViewItem29.Checked = true;
            listViewItem29.Group = listViewGroup11;
            listViewItem29.StateImageIndex = 1;
            listViewItem29.Tag = "chkPreventSleep";
            listViewItem29.ToolTipText = "Prevents your computer from going to sleep whilst Win Toolkit is running.";
            listViewItem30.Checked = true;
            listViewItem30.Group = listViewGroup11;
            listViewItem30.StateImageIndex = 1;
            listViewItem30.Tag = "chkQMerge";
            listViewItem30.ToolTipText = "Merge (WIM Manager) and AIO Creator work a lot faster!";
            listViewItem31.Group = listViewGroup13;
            listViewItem31.StateImageIndex = 0;
            listViewItem31.Tag = "chkRLogging";
            listViewItem31.ToolTipText = "Logs all of the registry changes into a text file.";
            listViewItem32.Group = listViewGroup9;
            listViewItem32.StateImageIndex = 0;
            listViewItem32.Tag = "chkGNetwork";
            listViewItem32.ToolTipText = "This lets you browse network drives by setting \'EnableLinkedConnections\' to 1";
            listViewItem33.Checked = true;
            listViewItem33.Group = listViewGroup11;
            listViewItem33.StateImageIndex = 1;
            listViewItem33.Tag = "chkAIOPM";
            listViewItem33.ToolTipText = "Shows the Preset Manager when you open AIO Tool.";
            listViewItem34.Checked = true;
            listViewItem34.Group = listViewGroup9;
            listViewItem34.StateImageIndex = 1;
            listViewItem34.Tag = "chkUpdCatFilter";
            listViewItem34.ToolTipText = "Update Catalog will only show .msu .cab and .msp files";
            listViewItem35.Checked = true;
            listViewItem35.Group = listViewGroup10;
            listViewItem35.StateImageIndex = 1;
            listViewItem35.Tag = "chkUELogs";
            listViewItem35.ToolTipText = "This will upload the error log when you exit Win Toolkit.";
            listViewItem36.Group = listViewGroup12;
            listViewItem36.StateImageIndex = 0;
            listViewItem36.Tag = "chkmVerify";
            listViewItem36.ToolTipText = "It checks that either a capture or apply operation worked correctly. This may slo" +
    "w down some image tasks and is rarely needed.";
            this.lstOptions.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
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
            listViewItem29,
            listViewItem30,
            listViewItem31,
            listViewItem32,
            listViewItem33,
            listViewItem34,
            listViewItem35,
            listViewItem36});
            this.lstOptions.Location = new System.Drawing.Point(0, 0);
            this.lstOptions.Name = "lstOptions";
            this.lstOptions.ShowItemToolTips = true;
            this.lstOptions.Size = new System.Drawing.Size(513, 272);
            this.lstOptions.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstOptions.TabIndex = 0;
            this.lstOptions.UseCompatibleStateImageBehavior = false;
            this.lstOptions.View = System.Windows.Forms.View.Details;
            this.lstOptions.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstOptions_ItemChecked);
            // 
            // CHOptions
            // 
            this.CHOptions.Text = "Options";
            this.CHOptions.Width = 496;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.Label4);
            this.GroupBox3.Controls.Add(this.cboPriDISM);
            this.GroupBox3.Location = new System.Drawing.Point(8, 3);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(298, 84);
            this.GroupBox3.TabIndex = 1;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "DISM Priority";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(6, 46);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(271, 13);
            this.Label4.TabIndex = 3;
            this.Label4.Text = "The processes that require DISM (i.e. mount && unmount)";
            // 
            // cboPriDISM
            // 
            this.cboPriDISM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPriDISM.FormattingEnabled = true;
            this.cboPriDISM.Items.AddRange(new object[] {
            "RealTime",
            "High",
            "AboveNormal",
            "Normal",
            "BelowNormal",
            "Idle"});
            this.cboPriDISM.Location = new System.Drawing.Point(6, 19);
            this.cboPriDISM.Name = "cboPriDISM";
            this.cboPriDISM.Size = new System.Drawing.Size(285, 21);
            this.cboPriDISM.TabIndex = 2;
            // 
            // cmdApply
            // 
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(95, 25);
            this.cmdApply.Tag = "OK";
            this.cmdApply.Text = "Apply Settings";
            this.cmdApply.ToolTipText = "Apply and Save your chosen settings";
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdApply,
            this.cmdUndo,
            this.cmdClose});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new System.Drawing.Size(523, 25);
            this.ToolStrip1.TabIndex = 0;
            this.ToolStrip1.TabStop = true;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // cmdUndo
            // 
            this.cmdUndo.Name = "cmdUndo";
            this.cmdUndo.Size = new System.Drawing.Size(97, 25);
            this.cmdUndo.Tag = "Refresh";
            this.cmdUndo.Text = "Undo Changes";
            this.cmdUndo.ToolTipText = "Undo all the settings you have changed";
            this.cmdUndo.Click += new System.EventHandler(this.cmdUndo_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(130, 25);
            this.cmdClose.Tag = "Cancel";
            this.cmdClose.Text = "Close without Saving";
            this.cmdClose.ToolTipText = "Close Options with saving any settings";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitContainer1.IsSplitterFixed = true;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer1.Name = "SplitContainer1";
            this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.TabControl1);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.ToolStrip1);
            this.SplitContainer1.Size = new System.Drawing.Size(523, 361);
            this.SplitContainer1.SplitterDistance = 334;
            this.SplitContainer1.SplitterWidth = 2;
            this.SplitContainer1.TabIndex = 2;
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.TabMain);
            this.TabControl1.Controls.Add(this.TabProcesses);
            this.TabControl1.Controls.Add(this.TabFolders);
            this.TabControl1.Controls.Add(this.TabMisc);
            this.TabControl1.Controls.Add(this.TabDISM);
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl1.Location = new System.Drawing.Point(0, 0);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.Padding = new System.Drawing.Point(0, 0);
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(523, 334);
            this.TabControl1.TabIndex = 0;
            // 
            // TabMain
            // 
            this.TabMain.Controls.Add(this.splitContainer2);
            this.TabMain.Location = new System.Drawing.Point(4, 22);
            this.TabMain.Name = "TabMain";
            this.TabMain.Size = new System.Drawing.Size(515, 308);
            this.TabMain.TabIndex = 2;
            this.TabMain.Text = "Main";
            this.TabMain.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.cboLang);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lstOptions);
            this.splitContainer2.Size = new System.Drawing.Size(515, 308);
            this.splitContainer2.SplitterDistance = 33;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select your language:";
            // 
            // cboLang
            // 
            this.cboLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLang.Enabled = false;
            this.cboLang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLang.FormattingEnabled = true;
            this.cboLang.Items.AddRange(new object[] {
            "English"});
            this.cboLang.Location = new System.Drawing.Point(151, 3);
            this.cboLang.Name = "cboLang";
            this.cboLang.Size = new System.Drawing.Size(219, 24);
            this.cboLang.TabIndex = 2;
            // 
            // TabProcesses
            // 
            this.TabProcesses.AutoScroll = true;
            this.TabProcesses.BackColor = System.Drawing.SystemColors.Window;
            this.TabProcesses.Controls.Add(this.gbDISMAff);
            this.TabProcesses.Controls.Add(this.GroupBox1);
            this.TabProcesses.Controls.Add(this.GroupBox2);
            this.TabProcesses.Controls.Add(this.GroupBox3);
            this.TabProcesses.Location = new System.Drawing.Point(4, 22);
            this.TabProcesses.Name = "TabProcesses";
            this.TabProcesses.Size = new System.Drawing.Size(515, 308);
            this.TabProcesses.TabIndex = 4;
            this.TabProcesses.Text = "Processes";
            // 
            // gbCleanup
            // 
            this.gbCleanup.Controls.Add(this.lblNoticeClean);
            this.gbCleanup.Controls.Add(this.chkACLogs);
            this.gbCleanup.Controls.Add(this.chkACSXS);
            this.gbCleanup.Controls.Add(this.chkACManifest);
            this.gbCleanup.Controls.Add(this.chkACGeneric);
            this.gbCleanup.Enabled = false;
            this.gbCleanup.Location = new System.Drawing.Point(9, 164);
            this.gbCleanup.Name = "gbCleanup";
            this.gbCleanup.Size = new System.Drawing.Size(298, 82);
            this.gbCleanup.TabIndex = 12;
            this.gbCleanup.TabStop = false;
            this.gbCleanup.Text = "Auto Cleanup [Exclusive]";
            // 
            // lblNoticeClean
            // 
            this.lblNoticeClean.AutoSize = true;
            this.lblNoticeClean.Location = new System.Drawing.Point(7, 63);
            this.lblNoticeClean.Name = "lblNoticeClean";
            this.lblNoticeClean.Size = new System.Drawing.Size(176, 13);
            this.lblNoticeClean.TabIndex = 4;
            this.lblNoticeClean.Text = "This section required a product key.";
            // 
            // chkACLogs
            // 
            this.chkACLogs.AutoSize = true;
            this.chkACLogs.Location = new System.Drawing.Point(6, 42);
            this.chkACLogs.Name = "chkACLogs";
            this.chkACLogs.Size = new System.Drawing.Size(49, 17);
            this.chkACLogs.TabIndex = 3;
            this.chkACLogs.Text = "Logs";
            this.chkACLogs.UseVisualStyleBackColor = true;
            // 
            // chkACSXS
            // 
            this.chkACSXS.AutoSize = true;
            this.chkACSXS.Location = new System.Drawing.Point(118, 19);
            this.chkACSXS.Name = "chkACSXS";
            this.chkACSXS.Size = new System.Drawing.Size(106, 17);
            this.chkACSXS.TabIndex = 2;
            this.chkACSXS.Text = "WinSXS Backup";
            this.chkACSXS.UseVisualStyleBackColor = true;
            // 
            // chkACManifest
            // 
            this.chkACManifest.AutoSize = true;
            this.chkACManifest.Location = new System.Drawing.Point(118, 42);
            this.chkACManifest.Name = "chkACManifest";
            this.chkACManifest.Size = new System.Drawing.Size(100, 17);
            this.chkACManifest.TabIndex = 1;
            this.chkACManifest.Text = "Manifest Cache";
            this.chkACManifest.UseVisualStyleBackColor = true;
            // 
            // chkACGeneric
            // 
            this.chkACGeneric.AutoSize = true;
            this.chkACGeneric.Location = new System.Drawing.Point(6, 19);
            this.chkACGeneric.Name = "chkACGeneric";
            this.chkACGeneric.Size = new System.Drawing.Size(63, 17);
            this.chkACGeneric.TabIndex = 0;
            this.chkACGeneric.Text = "Generic";
            this.chkACGeneric.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtSoLoR);
            this.groupBox6.Controls.Add(this.cmdSoLoRD);
            this.groupBox6.Controls.Add(this.cmdSoLoRB);
            this.groupBox6.Enabled = false;
            this.groupBox6.Location = new System.Drawing.Point(3, 147);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(298, 62);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "\'Update Catalog\' Download Folder";
            // 
            // txtSoLoR
            // 
            this.txtSoLoR.Location = new System.Drawing.Point(6, 15);
            this.txtSoLoR.Name = "txtSoLoR";
            this.txtSoLoR.ReadOnly = true;
            this.txtSoLoR.Size = new System.Drawing.Size(285, 20);
            this.txtSoLoR.TabIndex = 0;
            this.txtSoLoR.TabStop = false;
            this.txtSoLoR.TextChanged += new System.EventHandler(this.txtSoLoR_TextChanged);
            // 
            // cmdSoLoRD
            // 
            this.cmdSoLoRD.Location = new System.Drawing.Point(128, 36);
            this.cmdSoLoRD.Name = "cmdSoLoRD";
            this.cmdSoLoRD.Size = new System.Drawing.Size(79, 23);
            this.cmdSoLoRD.TabIndex = 7;
            this.cmdSoLoRD.Text = "Default";
            this.cmdSoLoRD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdSoLoRD.UseVisualStyleBackColor = true;
            this.cmdSoLoRD.Click += new System.EventHandler(this.cmdSoLoRD_Click);
            // 
            // cmdSoLoRB
            // 
            this.cmdSoLoRB.Location = new System.Drawing.Point(213, 36);
            this.cmdSoLoRB.Name = "cmdSoLoRB";
            this.cmdSoLoRB.Size = new System.Drawing.Size(78, 23);
            this.cmdSoLoRB.TabIndex = 8;
            this.cmdSoLoRB.Text = "Browse";
            this.cmdSoLoRB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdSoLoRB.UseVisualStyleBackColor = true;
            this.cmdSoLoRB.Click += new System.EventHandler(this.cmdSoLoRB_Click);
            // 
            // GroupBox4
            // 
            this.GroupBox4.Controls.Add(this.txtMountTemp);
            this.GroupBox4.Controls.Add(this.cmdMountRD);
            this.GroupBox4.Controls.Add(this.cmdMountBrowse);
            this.GroupBox4.Location = new System.Drawing.Point(3, 77);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(298, 63);
            this.GroupBox4.TabIndex = 10;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Win Toolkit Mount Folder";
            // 
            // txtMountTemp
            // 
            this.txtMountTemp.Location = new System.Drawing.Point(6, 15);
            this.txtMountTemp.Name = "txtMountTemp";
            this.txtMountTemp.ReadOnly = true;
            this.txtMountTemp.Size = new System.Drawing.Size(285, 20);
            this.txtMountTemp.TabIndex = 0;
            this.txtMountTemp.TabStop = false;
            this.txtMountTemp.TextChanged += new System.EventHandler(this.txtMountTemp_TextChanged);
            // 
            // cmdMountRD
            // 
            this.cmdMountRD.Location = new System.Drawing.Point(128, 37);
            this.cmdMountRD.Name = "cmdMountRD";
            this.cmdMountRD.Size = new System.Drawing.Size(79, 23);
            this.cmdMountRD.TabIndex = 5;
            this.cmdMountRD.Text = "Auto";
            this.cmdMountRD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdMountRD.UseVisualStyleBackColor = true;
            this.cmdMountRD.Click += new System.EventHandler(this.cmdMountRD_Click);
            // 
            // cmdMountBrowse
            // 
            this.cmdMountBrowse.Location = new System.Drawing.Point(213, 37);
            this.cmdMountBrowse.Name = "cmdMountBrowse";
            this.cmdMountBrowse.Size = new System.Drawing.Size(78, 23);
            this.cmdMountBrowse.TabIndex = 6;
            this.cmdMountBrowse.Text = "Browse";
            this.cmdMountBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdMountBrowse.UseVisualStyleBackColor = true;
            this.cmdMountBrowse.Click += new System.EventHandler(this.cmdMountBrowse_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.cboPriWinToolkit);
            this.GroupBox1.Location = new System.Drawing.Point(8, 183);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(298, 84);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Win Toolkit Priority";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(6, 43);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(152, 13);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Set priority of Win Toolkit itself.";
            // 
            // cboPriWinToolkit
            // 
            this.cboPriWinToolkit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPriWinToolkit.FormattingEnabled = true;
            this.cboPriWinToolkit.Items.AddRange(new object[] {
            "RealTime",
            "High",
            "AboveNormal",
            "Normal",
            "BelowNormal",
            "Idle"});
            this.cboPriWinToolkit.Location = new System.Drawing.Point(6, 19);
            this.cboPriWinToolkit.Name = "cboPriWinToolkit";
            this.cboPriWinToolkit.Size = new System.Drawing.Size(285, 21);
            this.cboPriWinToolkit.TabIndex = 0;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.Label3);
            this.GroupBox2.Controls.Add(this.cboPriExt);
            this.GroupBox2.Location = new System.Drawing.Point(8, 93);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(298, 84);
            this.GroupBox2.TabIndex = 1;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "External Apps Priority";
            // 
            // cboPriExt
            // 
            this.cboPriExt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPriExt.FormattingEnabled = true;
            this.cboPriExt.Items.AddRange(new object[] {
            "RealTime",
            "High",
            "AboveNormal",
            "Normal",
            "BelowNormal",
            "Idle"});
            this.cboPriExt.Location = new System.Drawing.Point(6, 19);
            this.cboPriExt.Name = "cboPriExt";
            this.cboPriExt.Size = new System.Drawing.Size(285, 21);
            this.cboPriExt.TabIndex = 1;
            // 
            // TabMisc
            // 
            this.TabMisc.Controls.Add(this.gbCleanup);
            this.TabMisc.Controls.Add(this.groupBox7);
            this.TabMisc.Controls.Add(this.GroupBox5);
            this.TabMisc.Location = new System.Drawing.Point(4, 22);
            this.TabMisc.Name = "TabMisc";
            this.TabMisc.Size = new System.Drawing.Size(515, 308);
            this.TabMisc.TabIndex = 6;
            this.TabMisc.Text = "Misc";
            this.TabMisc.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.cboScaling);
            this.groupBox7.Location = new System.Drawing.Point(8, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(298, 61);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Scaling";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Set menu scaling";
            // 
            // cboScaling
            // 
            this.cboScaling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScaling.FormattingEnabled = true;
            this.cboScaling.Items.AddRange(new object[] {
            "Auto",
            "Default",
            "2",
            "3",
            "4"});
            this.cboScaling.Location = new System.Drawing.Point(6, 19);
            this.cboScaling.Name = "cboScaling";
            this.cboScaling.Size = new System.Drawing.Size(285, 21);
            this.cboScaling.TabIndex = 0;
            // 
            // TabDISM
            // 
            this.TabDISM.Controls.Add(this.splitContainer3);
            this.TabDISM.Location = new System.Drawing.Point(4, 22);
            this.TabDISM.Margin = new System.Windows.Forms.Padding(0);
            this.TabDISM.Name = "TabDISM";
            this.TabDISM.Size = new System.Drawing.Size(515, 308);
            this.TabDISM.TabIndex = 5;
            this.TabDISM.Text = "DISM";
            this.TabDISM.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.toolStrip2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.lstDISM);
            this.splitContainer3.Size = new System.Drawing.Size(515, 308);
            this.splitContainer3.SplitterDistance = 27;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 13;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdAddDISM,
            this.cmdRemoveDISM});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip2.Size = new System.Drawing.Size(515, 27);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.TabStop = true;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // cmdAddDISM
            // 
            this.cmdAddDISM.Name = "cmdAddDISM";
            this.cmdAddDISM.Size = new System.Drawing.Size(41, 27);
            this.cmdAddDISM.Tag = "Add";
            this.cmdAddDISM.Text = "Add";
            this.cmdAddDISM.ToolTipText = "Add a custom DISM entry.";
            this.cmdAddDISM.Click += new System.EventHandler(this.cmsAddDISM);
            // 
            // cmdRemoveDISM
            // 
            this.cmdRemoveDISM.Name = "cmdRemoveDISM";
            this.cmdRemoveDISM.Size = new System.Drawing.Size(52, 27);
            this.cmdRemoveDISM.Tag = "Remove";
            this.cmdRemoveDISM.Text = "Delete";
            this.cmdRemoveDISM.ToolTipText = "Delete a custom DISM entry.";
            this.cmdRemoveDISM.Click += new System.EventHandler(this.cmdRemoveDISM_Click);
            // 
            // lstDISM
            // 
            this.lstDISM.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstDISM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDISM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDISM.FullRowSelect = true;
            listViewGroup14.Header = "Custom";
            listViewGroup14.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup14.Name = "listViewGroup1";
            listViewGroup15.Header = "System";
            listViewGroup15.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup15.Name = "listViewGroup3";
            listViewGroup16.Header = "Installed";
            listViewGroup16.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup16.Name = "listViewGroup2";
            this.lstDISM.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup14,
            listViewGroup15,
            listViewGroup16});
            this.lstDISM.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstDISM.Location = new System.Drawing.Point(0, 0);
            this.lstDISM.Name = "lstDISM";
            this.lstDISM.ShowItemToolTips = true;
            this.lstDISM.Size = new System.Drawing.Size(515, 280);
            this.lstDISM.TabIndex = 0;
            this.lstDISM.TileSize = new System.Drawing.Size(168, 40);
            this.lstDISM.UseCompatibleStateImageBehavior = false;
            this.lstDISM.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Version";
            this.columnHeader1.Width = 111;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Location";
            this.columnHeader2.Width = 366;
            // 
            // tbCPUDism
            // 
            this.tbCPUDism.BackColor = System.Drawing.SystemColors.Window;
            this.tbCPUDism.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tbCPUDism.Enabled = false;
            this.tbCPUDism.Location = new System.Drawing.Point(7, 14);
            this.tbCPUDism.Maximum = 8;
            this.tbCPUDism.Minimum = 1;
            this.tbCPUDism.Name = "tbCPUDism";
            this.tbCPUDism.Size = new System.Drawing.Size(187, 45);
            this.tbCPUDism.SmallChange = 2;
            this.tbCPUDism.TabIndex = 5;
            this.tbCPUDism.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbCPUDism.Value = 8;
            this.tbCPUDism.Scroll += new System.EventHandler(this.tbCPUDism_Scroll);
            // 
            // lblCPUDISM
            // 
            this.lblCPUDISM.AutoSize = true;
            this.lblCPUDISM.Location = new System.Drawing.Point(6, 62);
            this.lblCPUDISM.Name = "lblCPUDISM";
            this.lblCPUDISM.Size = new System.Drawing.Size(105, 13);
            this.lblCPUDISM.TabIndex = 6;
            this.lblCPUDISM.Text = "Set processor affinity";
           // 
            // TabFolders
            // 
            this.TabFolders.Controls.Add(this.GBWinToolkitemp);
            this.TabFolders.Controls.Add(this.groupBox6);
            this.TabFolders.Controls.Add(this.GroupBox4);
            this.TabFolders.Location = new System.Drawing.Point(4, 22);
            this.TabFolders.Name = "TabFolders";
            this.TabFolders.Size = new System.Drawing.Size(515, 308);
            this.TabFolders.TabIndex = 7;
            this.TabFolders.Text = "Folders";
            this.TabFolders.UseVisualStyleBackColor = true;
            // 
            // gbDISMAff
            // 
            this.gbDISMAff.Controls.Add(this.lblCPUDISM);
            this.gbDISMAff.Controls.Add(this.tbCPUDism);
            this.gbDISMAff.Location = new System.Drawing.Point(312, 3);
            this.gbDISMAff.Name = "gbDISMAff";
            this.gbDISMAff.Size = new System.Drawing.Size(200, 84);
            this.gbDISMAff.TabIndex = 7;
            this.gbDISMAff.TabStop = false;
            this.gbDISMAff.Text = "DISM Affinity";
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 361);
            this.Controls.Add(this.SplitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(539, 400);
            this.Name = "frmOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Win Toolkit Options";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOptions_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmOptions_FormClosed);
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MouseScroll);
            this.GBWinToolkitemp.ResumeLayout(false);
            this.GBWinToolkitemp.PerformLayout();
            this.GroupBox5.ResumeLayout(false);
            this.GroupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HSBO)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.Panel2.PerformLayout();
            this.SplitContainer1.ResumeLayout(false);
            this.TabControl1.ResumeLayout(false);
            this.TabMain.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.TabProcesses.ResumeLayout(false);
            this.gbCleanup.ResumeLayout(false);
            this.gbCleanup.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.GroupBox4.ResumeLayout(false);
            this.GroupBox4.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.TabMisc.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.TabDISM.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbCPUDism)).EndInit();
            this.TabFolders.ResumeLayout(false);
            this.gbDISMAff.ResumeLayout(false);
            this.gbDISMAff.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GBWinToolkitemp;
        internal System.Windows.Forms.TextBox txtWinToolkitTemp;
        internal System.Windows.Forms.Button cmdTempRD;
        internal System.Windows.Forms.Button cmdTempBrowse;
        internal System.Windows.Forms.GroupBox GroupBox5;
        internal System.Windows.Forms.TrackBar HSBO;
        internal System.Windows.Forms.CheckBox chkT;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.ListView lstOptions;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ComboBox cboPriDISM;
        internal System.Windows.Forms.ToolStripMenuItem cmdApply;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem cmdUndo;
        internal System.Windows.Forms.ToolStripMenuItem cmdClose;
        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal System.Windows.Forms.TabControl TabControl1;
        internal System.Windows.Forms.TabPage TabMain;
        internal System.Windows.Forms.TabPage TabProcesses;
        internal System.Windows.Forms.GroupBox GroupBox4;
        internal System.Windows.Forms.TextBox txtMountTemp;
        internal System.Windows.Forms.Button cmdMountRD;
        internal System.Windows.Forms.Button cmdMountBrowse;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.ComboBox cboPriWinToolkit;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.ComboBox cboPriExt;
        internal System.Windows.Forms.GroupBox groupBox6;
        internal System.Windows.Forms.TextBox txtSoLoR;
        internal System.Windows.Forms.Button cmdSoLoRD;
        internal System.Windows.Forms.Button cmdSoLoRB;
        private System.Windows.Forms.SplitContainer splitContainer2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox cboLang;
        private System.Windows.Forms.ColumnHeader CHOptions;
        private System.Windows.Forms.TabPage TabDISM;
        private System.Windows.Forms.SplitContainer splitContainer3;
        internal System.Windows.Forms.ToolStrip toolStrip2;
        internal System.Windows.Forms.ToolStripMenuItem cmdAddDISM;
        internal System.Windows.Forms.ToolStripMenuItem cmdRemoveDISM;
        private System.Windows.Forms.ListView lstDISM;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox gbCleanup;
        private System.Windows.Forms.CheckBox chkACLogs;
        private System.Windows.Forms.CheckBox chkACSXS;
        private System.Windows.Forms.CheckBox chkACManifest;
        private System.Windows.Forms.CheckBox chkACGeneric;
        private System.Windows.Forms.Label lblNoticeClean;
        private System.Windows.Forms.TabPage TabMisc;
        internal System.Windows.Forms.GroupBox groupBox7;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.ComboBox cboScaling;
        internal System.Windows.Forms.Label lblCPUDISM;
        internal System.Windows.Forms.TrackBar tbCPUDism;
        private System.Windows.Forms.TabPage TabFolders;
        internal System.Windows.Forms.GroupBox gbDISMAff;
    }
}