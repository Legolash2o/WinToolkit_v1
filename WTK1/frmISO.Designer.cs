namespace WinToolkit
{
    partial class frmISOMaker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmISOMaker));
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.chkRebuild = new System.Windows.Forms.CheckBox();
            this.gbProgress = new System.Windows.Forms.GroupBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.chkCMDWindow = new System.Windows.Forms.CheckBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdFBrowse = new System.Windows.Forms.Button();
            this.lblFolder = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.cmdIBrowse = new System.Windows.Forms.Button();
            this.lblISO = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.chkCustomDate = new System.Windows.Forms.CheckBox();
            this.rbCustomDate = new System.Windows.Forms.RadioButton();
            this.rbInstallWIM = new System.Windows.Forms.RadioButton();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.gbDate = new System.Windows.Forms.GroupBox();
            this.scDate = new System.Windows.Forms.SplitContainer();
            this.dtDate = new System.Windows.Forms.MonthCalendar();
            this.dtTime = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkei = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cboBoot = new System.Windows.Forms.ComboBox();
            this.lblBoot = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdStart = new System.Windows.Forms.ToolStripMenuItem();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.BWISO = new System.ComponentModel.BackgroundWorker();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.gbProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.gbDate.SuspendLayout();
            this.scDate.Panel1.SuspendLayout();
            this.scDate.Panel2.SuspendLayout();
            this.scDate.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.ToolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.SplitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.ToolStrip1);
            this.SplitContainer1.Panel2MinSize = 40;
            this.SplitContainer1.Size = new System.Drawing.Size(488, 294);
            this.SplitContainer1.SplitterDistance = 253;
            this.SplitContainer1.SplitterWidth = 1;
            this.SplitContainer1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(488, 253);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox7);
            this.tabPage1.Controls.Add(this.gbProgress);
            this.tabPage1.Controls.Add(this.GroupBox2);
            this.tabPage1.Controls.Add(this.GroupBox4);
            this.tabPage1.Controls.Add(this.GroupBox3);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(480, 222);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.pictureBox6);
            this.groupBox7.Controls.Add(this.chkRebuild);
            this.groupBox7.Location = new System.Drawing.Point(234, 124);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(222, 41);
            this.groupBox7.TabIndex = 12;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Rebuilding";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(8, 18);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(16, 16);
            this.pictureBox6.TabIndex = 12;
            this.pictureBox6.TabStop = false;
            // 
            // chkRebuild
            // 
            this.chkRebuild.AutoSize = true;
            this.chkRebuild.Checked = true;
            this.chkRebuild.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRebuild.Enabled = false;
            this.chkRebuild.Location = new System.Drawing.Point(30, 18);
            this.chkRebuild.Name = "chkRebuild";
            this.chkRebuild.Size = new System.Drawing.Size(94, 17);
            this.chkRebuild.TabIndex = 11;
            this.chkRebuild.Text = "Rebuild Image";
            this.chkRebuild.UseVisualStyleBackColor = true;
            // 
            // gbProgress
            // 
            this.gbProgress.Controls.Add(this.pictureBox8);
            this.gbProgress.Controls.Add(this.chkCMDWindow);
            this.gbProgress.Location = new System.Drawing.Point(6, 171);
            this.gbProgress.Name = "gbProgress";
            this.gbProgress.Size = new System.Drawing.Size(222, 41);
            this.gbProgress.TabIndex = 13;
            this.gbProgress.TabStop = false;
            this.gbProgress.Text = "Progress";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(9, 19);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(16, 16);
            this.pictureBox8.TabIndex = 11;
            this.pictureBox8.TabStop = false;
            // 
            // chkCMDWindow
            // 
            this.chkCMDWindow.AutoSize = true;
            this.chkCMDWindow.Checked = true;
            this.chkCMDWindow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCMDWindow.Location = new System.Drawing.Point(34, 19);
            this.chkCMDWindow.Name = "chkCMDWindow";
            this.chkCMDWindow.Size = new System.Drawing.Size(122, 17);
            this.chkCMDWindow.TabIndex = 11;
            this.chkCMDWindow.Text = "Show CMD Window";
            this.chkCMDWindow.UseVisualStyleBackColor = true;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.cmdFBrowse);
            this.GroupBox2.Controls.Add(this.lblFolder);
            this.GroupBox2.Controls.Add(this.pictureBox5);
            this.GroupBox2.Location = new System.Drawing.Point(6, 6);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(461, 53);
            this.GroupBox2.TabIndex = 1;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Folder";
            // 
            // cmdFBrowse
            // 
            this.cmdFBrowse.Location = new System.Drawing.Point(383, 9);
            this.cmdFBrowse.Name = "cmdFBrowse";
            this.cmdFBrowse.Size = new System.Drawing.Size(75, 41);
            this.cmdFBrowse.TabIndex = 6;
            this.cmdFBrowse.Text = "Browse";
            this.cmdFBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdFBrowse.UseVisualStyleBackColor = true;
            this.cmdFBrowse.Click += new System.EventHandler(this.cmdFBrowse_Click);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoEllipsis = true;
            this.lblFolder.Location = new System.Drawing.Point(27, 25);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(350, 18);
            this.lblFolder.TabIndex = 5;
            this.lblFolder.Text = "No folder selected...";
            this.lblFolder.TextChanged += new System.EventHandler(this.lblFolder_TextChanged);
            this.lblFolder.Click += new System.EventHandler(this.lblFolder_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(8, 23);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(16, 16);
            this.pictureBox5.TabIndex = 10;
            this.pictureBox5.TabStop = false;
            // 
            // GroupBox4
            // 
            this.GroupBox4.Controls.Add(this.txtName);
            this.GroupBox4.Controls.Add(this.pictureBox3);
            this.GroupBox4.Location = new System.Drawing.Point(6, 124);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(222, 41);
            this.GroupBox4.TabIndex = 8;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Label";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.Control;
            this.txtName.Location = new System.Drawing.Point(30, 14);
            this.txtName.MaxLength = 30;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 20);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(8, 18);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(16, 16);
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.pictureBox4);
            this.GroupBox3.Controls.Add(this.cmdIBrowse);
            this.GroupBox3.Controls.Add(this.lblISO);
            this.GroupBox3.Location = new System.Drawing.Point(6, 65);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(461, 53);
            this.GroupBox3.TabIndex = 7;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "ISO Output";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(8, 22);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(16, 16);
            this.pictureBox4.TabIndex = 12;
            this.pictureBox4.TabStop = false;
            // 
            // cmdIBrowse
            // 
            this.cmdIBrowse.Location = new System.Drawing.Point(383, 9);
            this.cmdIBrowse.Name = "cmdIBrowse";
            this.cmdIBrowse.Size = new System.Drawing.Size(75, 40);
            this.cmdIBrowse.TabIndex = 6;
            this.cmdIBrowse.Text = "Browse";
            this.cmdIBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdIBrowse.UseVisualStyleBackColor = true;
            this.cmdIBrowse.Click += new System.EventHandler(this.cmdIBrowse_Click);
            // 
            // lblISO
            // 
            this.lblISO.AutoEllipsis = true;
            this.lblISO.Location = new System.Drawing.Point(27, 24);
            this.lblISO.Name = "lblISO";
            this.lblISO.Size = new System.Drawing.Size(350, 18);
            this.lblISO.TabIndex = 5;
            this.lblISO.Text = "No location selected...";
            this.lblISO.TextChanged += new System.EventHandler(this.lblISO_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.gbDate);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(480, 219);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Advanced";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.chkCustomDate);
            this.groupBox8.Controls.Add(this.rbCustomDate);
            this.groupBox8.Controls.Add(this.rbInstallWIM);
            this.groupBox8.Controls.Add(this.pictureBox7);
            this.groupBox8.Location = new System.Drawing.Point(3, 95);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(229, 74);
            this.groupBox8.TabIndex = 14;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Date";
            // 
            // chkCustomDate
            // 
            this.chkCustomDate.AutoSize = true;
            this.chkCustomDate.Location = new System.Drawing.Point(31, 19);
            this.chkCustomDate.Name = "chkCustomDate";
            this.chkCustomDate.Size = new System.Drawing.Size(59, 17);
            this.chkCustomDate.TabIndex = 13;
            this.chkCustomDate.Text = "Enable";
            this.chkCustomDate.UseVisualStyleBackColor = true;
            this.chkCustomDate.CheckedChanged += new System.EventHandler(this.chkCustomDate_CheckedChanged);
            // 
            // rbCustomDate
            // 
            this.rbCustomDate.AutoSize = true;
            this.rbCustomDate.Enabled = false;
            this.rbCustomDate.Location = new System.Drawing.Point(31, 54);
            this.rbCustomDate.Name = "rbCustomDate";
            this.rbCustomDate.Size = new System.Drawing.Size(86, 17);
            this.rbCustomDate.TabIndex = 15;
            this.rbCustomDate.Text = "Custom Date";
            this.rbCustomDate.UseVisualStyleBackColor = true;
            this.rbCustomDate.CheckedChanged += new System.EventHandler(this.rbCustomDate_CheckedChanged);
            // 
            // rbInstallWIM
            // 
            this.rbInstallWIM.AutoSize = true;
            this.rbInstallWIM.Checked = true;
            this.rbInstallWIM.Enabled = false;
            this.rbInstallWIM.Location = new System.Drawing.Point(31, 36);
            this.rbInstallWIM.Name = "rbInstallWIM";
            this.rbInstallWIM.Size = new System.Drawing.Size(142, 17);
            this.rbInstallWIM.TabIndex = 14;
            this.rbInstallWIM.TabStop = true;
            this.rbInstallWIM.Text = "Install.wim Modified Date";
            this.rbInstallWIM.UseVisualStyleBackColor = true;
            this.rbInstallWIM.CheckedChanged += new System.EventHandler(this.rbInstallWIM_CheckedChanged);
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(9, 19);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(16, 16);
            this.pictureBox7.TabIndex = 11;
            this.pictureBox7.TabStop = false;
            // 
            // gbDate
            // 
            this.gbDate.Controls.Add(this.scDate);
            this.gbDate.Enabled = false;
            this.gbDate.Location = new System.Drawing.Point(236, 3);
            this.gbDate.Name = "gbDate";
            this.gbDate.Size = new System.Drawing.Size(236, 212);
            this.gbDate.TabIndex = 11;
            this.gbDate.TabStop = false;
            this.gbDate.Text = "Custom Date";
            // 
            // scDate
            // 
            this.scDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scDate.IsSplitterFixed = true;
            this.scDate.Location = new System.Drawing.Point(3, 16);
            this.scDate.Name = "scDate";
            this.scDate.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scDate.Panel1
            // 
            this.scDate.Panel1.Controls.Add(this.dtDate);
            // 
            // scDate.Panel2
            // 
            this.scDate.Panel2.Controls.Add(this.dtTime);
            this.scDate.Panel2MinSize = 18;
            this.scDate.Size = new System.Drawing.Size(230, 193);
            this.scDate.SplitterDistance = 167;
            this.scDate.TabIndex = 15;
            // 
            // dtDate
            // 
            this.dtDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtDate.Location = new System.Drawing.Point(0, 0);
            this.dtDate.MaxSelectionCount = 1;
            this.dtDate.MinDate = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            this.dtDate.Name = "dtDate";
            this.dtDate.TabIndex = 0;
            this.dtDate.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.dtDate_DateChanged);
            // 
            // dtTime
            // 
            this.dtTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtTime.Location = new System.Drawing.Point(0, 0);
            this.dtTime.Name = "dtTime";
            this.dtTime.ShowUpDown = true;
            this.dtTime.Size = new System.Drawing.Size(230, 20);
            this.dtTime.TabIndex = 12;
            this.dtTime.ValueChanged += new System.EventHandler(this.dtTime_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.chkDebug);
            this.groupBox1.Location = new System.Drawing.Point(3, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 40);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Debugging";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(9, 18);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // chkDebug
            // 
            this.chkDebug.AutoSize = true;
            this.chkDebug.Location = new System.Drawing.Point(34, 17);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(88, 17);
            this.chkDebug.TabIndex = 11;
            this.chkDebug.Text = "Debug Mode";
            this.chkDebug.UseVisualStyleBackColor = true;
            this.chkDebug.CheckedChanged += new System.EventHandler(this.chkDebug_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkei);
            this.groupBox5.Controls.Add(this.pictureBox1);
            this.groupBox5.Controls.Add(this.cboBoot);
            this.groupBox5.Controls.Add(this.lblBoot);
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(229, 86);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Boot Image (Advanced)";
            // 
            // chkei
            // 
            this.chkei.AutoSize = true;
            this.chkei.Location = new System.Drawing.Point(31, 63);
            this.chkei.Name = "chkei";
            this.chkei.Size = new System.Drawing.Size(89, 17);
            this.chkei.TabIndex = 16;
            this.chkei.Text = "Unlock ei.cfg";
            this.chkei.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // cboBoot
            // 
            this.cboBoot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBoot.FormattingEnabled = true;
            this.cboBoot.Items.AddRange(new object[] {
            "BIOS and UEFI",
            "UEFI",
            "BIOS",
            "Custom",
            "None"});
            this.cboBoot.Location = new System.Drawing.Point(31, 18);
            this.cboBoot.Name = "cboBoot";
            this.cboBoot.Size = new System.Drawing.Size(190, 21);
            this.cboBoot.TabIndex = 3;
            this.cboBoot.SelectedIndexChanged += new System.EventHandler(this.cboBoot_SelectedIndexChanged);
            // 
            // lblBoot
            // 
            this.lblBoot.AutoEllipsis = true;
            this.lblBoot.Location = new System.Drawing.Point(31, 42);
            this.lblBoot.Name = "lblBoot";
            this.lblBoot.Size = new System.Drawing.Size(190, 18);
            this.lblBoot.TabIndex = 4;
            this.lblBoot.Text = "No custom boot file selected...";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "MagicISO_Icon.png");
            this.imageList1.Images.SetKeyName(1, "Gear.png");
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdStart,
            this.lblStatus});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.Size = new System.Drawing.Size(488, 40);
            this.ToolStrip1.TabIndex = 2;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // cmdStart
            // 
            this.cmdStart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(96, 40);
            this.cmdStart.Tag = "OK";
            this.cmdStart.Text = "Create ISO";
            this.cmdStart.ToolTipText = "Start making your new image!";
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 37);
            // 
            // BWISO
            // 
            this.BWISO.WorkerSupportsCancellation = true;
            this.BWISO.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWISO_DoWork);
            // 
            // frmISOMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(488, 294);
            this.Controls.Add(this.SplitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmISOMaker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ISO Maker";
            this.Load += new System.EventHandler(this.frmISO_Load);
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.Panel2.PerformLayout();
            this.SplitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.gbProgress.ResumeLayout(false);
            this.gbProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.GroupBox4.ResumeLayout(false);
            this.GroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.gbDate.ResumeLayout(false);
            this.scDate.Panel1.ResumeLayout(false);
            this.scDate.Panel2.ResumeLayout(false);
            this.scDate.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal System.Windows.Forms.GroupBox GroupBox4;
		  internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.Button cmdIBrowse;
        internal System.Windows.Forms.Label lblISO;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Button cmdFBrowse;
        internal System.Windows.Forms.Label lblFolder;
        internal System.Windows.Forms.Label lblBoot;
        internal System.Windows.Forms.ComboBox cboBoot;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem cmdStart;
        internal System.Windows.Forms.ToolStripLabel lblStatus;
        internal System.ComponentModel.BackgroundWorker BWISO;
		  private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
		  private System.Windows.Forms.PictureBox pictureBox5;
		  private System.Windows.Forms.TabControl tabControl1;
		  private System.Windows.Forms.TabPage tabPage1;
		  private System.Windows.Forms.CheckBox chkRebuild;
		  private System.Windows.Forms.TabPage tabPage2;
		  private System.Windows.Forms.GroupBox gbDate;
		  private System.Windows.Forms.DateTimePicker dtTime;
		  private System.Windows.Forms.MonthCalendar dtDate;
		  private System.Windows.Forms.GroupBox groupBox1;
		  private System.Windows.Forms.PictureBox pictureBox2;
		  private System.Windows.Forms.CheckBox chkDebug;
		  private System.Windows.Forms.GroupBox groupBox7;
		  private System.Windows.Forms.PictureBox pictureBox6;
		  private System.Windows.Forms.PictureBox pictureBox7;
		  private System.Windows.Forms.GroupBox gbProgress;
		  private System.Windows.Forms.PictureBox pictureBox8;
		  private System.Windows.Forms.CheckBox chkCMDWindow;
		  private System.Windows.Forms.ImageList imageList1;
		  private System.Windows.Forms.GroupBox groupBox8;
		  private System.Windows.Forms.CheckBox chkCustomDate;
		  private System.Windows.Forms.RadioButton rbCustomDate;
		  private System.Windows.Forms.RadioButton rbInstallWIM;
        private System.Windows.Forms.CheckBox chkei;
        private System.Windows.Forms.SplitContainer scDate;
    }
}