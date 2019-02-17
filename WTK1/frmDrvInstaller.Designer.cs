namespace WinToolkit
{
    partial class frmDrvInstaller
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
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdSI = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdAF = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuR = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRF = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRA = new System.Windows.Forms.ToolStripMenuItem();
            this.cboOption = new System.Windows.Forms.ToolStripComboBox();
            this.scNew = new System.Windows.Forms.SplitContainer();
            this.lstDrivers = new WinToolkit.ListViewEx();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BWI = new System.ComponentModel.BackgroundWorker();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PB = new System.Windows.Forms.ToolStripProgressBar();
            this.BWA = new System.ComponentModel.BackgroundWorker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.scInstalled = new System.Windows.Forms.SplitContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.cmdRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdU = new System.Windows.Forms.ToolStripMenuItem();
            this.cboForce = new System.Windows.Forms.ToolStripComboBox();
            this.lstInstalled = new WinToolkit.ListViewEx();
            this.CHPN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BWScan = new System.ComponentModel.BackgroundWorker();
            this.BWU = new System.ComponentModel.BackgroundWorker();
            this.ToolStrip1.SuspendLayout();
            this.scNew.Panel1.SuspendLayout();
            this.scNew.Panel2.SuspendLayout();
            this.scNew.SuspendLayout();
            this.StatusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.scInstalled.Panel1.SuspendLayout();
            this.scInstalled.Panel2.SuspendLayout();
            this.scInstalled.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdSI,
            this.cmdAF,
            this.mnuR,
            this.cboOption});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.Size = new System.Drawing.Size(726, 25);
            this.ToolStrip1.TabIndex = 8;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // cmdSI
            // 
            this.cmdSI.Name = "cmdSI";
            this.cmdSI.Size = new System.Drawing.Size(43, 25);
            this.cmdSI.Tag = "OK";
            this.cmdSI.Text = "Start";
            this.cmdSI.ToolTipText = "Start or Stop installing files";
            this.cmdSI.Click += new System.EventHandler(this.cmdSI_Click);
            // 
            // cmdAF
            // 
            this.cmdAF.Name = "cmdAF";
            this.cmdAF.Size = new System.Drawing.Size(80, 25);
            this.cmdAF.Tag = "Add";
            this.cmdAF.Text = "Add Drivers";
            this.cmdAF.ToolTipText = "Add drivers to the list";
            this.cmdAF.Click += new System.EventHandler(this.cmdAF_Click);
            // 
            // mnuR
            // 
            this.mnuR.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRF,
            this.mnuRA});
            this.mnuR.Name = "mnuR";
            this.mnuR.Size = new System.Drawing.Size(62, 25);
            this.mnuR.Tag = "Remove";
            this.mnuR.Text = "Remove";
            this.mnuR.ToolTipText = "Removal options";
            // 
            // mnuRF
            // 
            this.mnuRF.Name = "mnuRF";
            this.mnuRF.Size = new System.Drawing.Size(156, 22);
            this.mnuRF.Text = "Remove Drivers";
            this.mnuRF.ToolTipText = "Remove selected drivers from the list";
            this.mnuRF.Click += new System.EventHandler(this.mnuRF_Click);
            // 
            // mnuRA
            // 
            this.mnuRA.Name = "mnuRA";
            this.mnuRA.Size = new System.Drawing.Size(156, 22);
            this.mnuRA.Text = "Remove All";
            this.mnuRA.ToolTipText = "Remove all drivers from the list";
            this.mnuRA.Click += new System.EventHandler(this.mnuRA_Click);
            // 
            // cboOption
            // 
            this.cboOption.Items.AddRange(new object[] {
            "Add and Install Driver",
            "Add Driver"});
            this.cboOption.Name = "cboOption";
            this.cboOption.Size = new System.Drawing.Size(180, 25);
            // 
            // scNew
            // 
            this.scNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scNew.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scNew.IsSplitterFixed = true;
            this.scNew.Location = new System.Drawing.Point(0, 0);
            this.scNew.Name = "scNew";
            this.scNew.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scNew.Panel1
            // 
            this.scNew.Panel1.Controls.Add(this.ToolStrip1);
            // 
            // scNew.Panel2
            // 
            this.scNew.Panel2.Controls.Add(this.lstDrivers);
            this.scNew.Size = new System.Drawing.Size(726, 313);
            this.scNew.SplitterDistance = 25;
            this.scNew.SplitterWidth = 1;
            this.scNew.TabIndex = 9;
            // 
            // lstDrivers
            // 
            this.lstDrivers.AllowDrop = true;
            this.lstDrivers.BackColor = System.Drawing.SystemColors.Window;
            this.lstDrivers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstDrivers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2,
            this.ColumnHeader3,
            this.columnHeader7,
            this.ColumnHeader4,
            this.ColumnHeader5,
            this.ColumnHeader6});
            this.lstDrivers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDrivers.FullRowSelect = true;
            this.lstDrivers.Location = new System.Drawing.Point(0, 0);
            this.lstDrivers.Name = "lstDrivers";
            this.lstDrivers.Size = new System.Drawing.Size(726, 287);
            this.lstDrivers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstDrivers.TabIndex = 0;
            this.lstDrivers.UseCompatibleStateImageBehavior = false;
            this.lstDrivers.View = System.Windows.Forms.View.Details;
            this.lstDrivers.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstIC_ItemSelectionChanged);
            this.lstDrivers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstDrivers_MouseDoubleClick);
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Name";
            this.ColumnHeader1.Width = 147;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Type";
            this.ColumnHeader2.Width = 175;
            // 
            // ColumnHeader3
            // 
            this.ColumnHeader3.Text = "Arc";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Designed For";
            this.columnHeader7.Width = 77;
            // 
            // ColumnHeader4
            // 
            this.ColumnHeader4.Text = "Date";
            this.ColumnHeader4.Width = 71;
            // 
            // ColumnHeader5
            // 
            this.ColumnHeader5.Text = "Version";
            this.ColumnHeader5.Width = 74;
            // 
            // ColumnHeader6
            // 
            this.ColumnHeader6.Text = "Location";
            // 
            // BWI
            // 
            this.BWI.WorkerSupportsCancellation = true;
            this.BWI.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWI_DoWork);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PB,
            this.lblStatus});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 339);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(734, 22);
            this.StatusStrip1.SizingGrip = false;
            this.StatusStrip1.TabIndex = 8;
            this.StatusStrip1.Text = "StatusStrip1";
            // 
            // PB
            // 
            this.PB.Name = "PB";
            this.PB.Size = new System.Drawing.Size(100, 19);
            this.PB.Visible = false;
            // 
            // BWA
            // 
            this.BWA.WorkerSupportsCancellation = true;
            this.BWA.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWA_DoWork);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(734, 339);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.scNew);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(726, 313);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "New";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.scInstalled);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(726, 313);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Installed";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // scInstalled
            // 
            this.scInstalled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scInstalled.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scInstalled.IsSplitterFixed = true;
            this.scInstalled.Location = new System.Drawing.Point(0, 0);
            this.scInstalled.Margin = new System.Windows.Forms.Padding(0);
            this.scInstalled.Name = "scInstalled";
            this.scInstalled.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scInstalled.Panel1
            // 
            this.scInstalled.Panel1.Controls.Add(this.toolStrip2);
            // 
            // scInstalled.Panel2
            // 
            this.scInstalled.Panel2.Controls.Add(this.lstInstalled);
            this.scInstalled.Size = new System.Drawing.Size(726, 313);
            this.scInstalled.SplitterDistance = 25;
            this.scInstalled.SplitterWidth = 1;
            this.scInstalled.TabIndex = 11;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdRefresh,
            this.toolStripSeparator1,
            this.cmdU,
            this.cboForce});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip2.Size = new System.Drawing.Size(726, 25);
            this.toolStrip2.TabIndex = 8;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Image = global::WinToolkit.Properties.Resources.refresh2_20x20;
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(60, 25);
            this.cmdRefresh.Tag = "";
            this.cmdRefresh.Text = "Scan";
            this.cmdRefresh.ToolTipText = "Rescan for devices";
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdU
            // 
            this.cmdU.Enabled = false;
            this.cmdU.Name = "cmdU";
            this.cmdU.Size = new System.Drawing.Size(65, 25);
            this.cmdU.Tag = "OK";
            this.cmdU.Text = "Uninstall";
            this.cmdU.ToolTipText = "Uninstall drivers...";
            this.cmdU.Click += new System.EventHandler(this.cmdU_Click);
            // 
            // cboForce
            // 
            this.cboForce.Enabled = false;
            this.cboForce.Items.AddRange(new object[] {
            "Normal",
            "Force"});
            this.cboForce.Name = "cboForce";
            this.cboForce.Size = new System.Drawing.Size(121, 25);
            // 
            // lstInstalled
            // 
            this.lstInstalled.AllowDrop = true;
            this.lstInstalled.BackColor = System.Drawing.SystemColors.Window;
            this.lstInstalled.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstInstalled.CheckBoxes = true;
            this.lstInstalled.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CHPN,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader19});
            this.lstInstalled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstInstalled.FullRowSelect = true;
            this.lstInstalled.Location = new System.Drawing.Point(0, 0);
            this.lstInstalled.Name = "lstInstalled";
            this.lstInstalled.Size = new System.Drawing.Size(726, 287);
            this.lstInstalled.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstInstalled.TabIndex = 1;
            this.lstInstalled.UseCompatibleStateImageBehavior = false;
            this.lstInstalled.View = System.Windows.Forms.View.Details;
            // 
            // CHPN
            // 
            this.CHPN.Text = "Published Name";
            this.CHPN.Width = 92;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Original Filename";
            this.columnHeader14.Width = 102;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Inbox";
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Class Name";
            this.columnHeader16.Width = 74;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Provider Name";
            this.columnHeader17.Width = 90;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "Date";
            this.columnHeader18.Width = 75;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "Version";
            this.columnHeader19.Width = 80;
            // 
            // BWScan
            // 
            this.BWScan.WorkerSupportsCancellation = true;
            this.BWScan.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwScan_DoWork);
            // 
            // BWU
            // 
            this.BWU.WorkerSupportsCancellation = true;
            this.BWU.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwU_DoWork);
            // 
            // frmDrvInstaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 361);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.StatusStrip1);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(750, 400);
            this.Name = "frmDrvInstaller";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Driver Installer";
            this.Load += new System.EventHandler(this.frmDrvInstaller_Load);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.scNew.Panel1.ResumeLayout(false);
            this.scNew.Panel1.PerformLayout();
            this.scNew.Panel2.ResumeLayout(false);
            this.scNew.ResumeLayout(false);
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.scInstalled.Panel1.ResumeLayout(false);
            this.scInstalled.Panel1.PerformLayout();
            this.scInstalled.Panel2.ResumeLayout(false);
            this.scInstalled.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem cmdSI;
        internal System.Windows.Forms.ToolStripMenuItem cmdAF;
        internal ListViewEx lstDrivers;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.ColumnHeader ColumnHeader2;
        internal System.Windows.Forms.ColumnHeader ColumnHeader3;
        internal System.Windows.Forms.ColumnHeader ColumnHeader4;
        internal System.Windows.Forms.ColumnHeader ColumnHeader5;
        internal System.Windows.Forms.ColumnHeader ColumnHeader6;
        internal System.Windows.Forms.SplitContainer scNew;
        internal System.ComponentModel.BackgroundWorker BWI;
        internal System.Windows.Forms.ToolStripStatusLabel lblStatus;
        internal System.Windows.Forms.StatusStrip StatusStrip1;
        internal System.ComponentModel.BackgroundWorker BWA;
        internal System.Windows.Forms.ToolStripProgressBar PB;
        internal System.Windows.Forms.ToolStripMenuItem mnuR;
        private System.Windows.Forms.ToolStripMenuItem mnuRF;
        private System.Windows.Forms.ToolStripMenuItem mnuRA;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        internal System.Windows.Forms.SplitContainer scInstalled;
        internal System.Windows.Forms.ToolStrip toolStrip2;
        internal System.Windows.Forms.ToolStripMenuItem cmdU;
        internal System.ComponentModel.BackgroundWorker BWScan;
        internal ListViewEx lstInstalled;
        private System.Windows.Forms.ColumnHeader CHPN;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        internal System.ComponentModel.BackgroundWorker BWU;
        private System.Windows.Forms.ToolStripComboBox cboForce;
        private System.Windows.Forms.ToolStripComboBox cboOption;
        internal System.Windows.Forms.ToolStripMenuItem cmdRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ColumnHeader columnHeader7;
    }
}