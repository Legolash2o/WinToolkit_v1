namespace WinToolkit
{
    partial class frmUpdInstaller
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdInstaller));
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdSI = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdAF = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdR = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdRS = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdRA = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdLDR = new System.Windows.Forms.ToolStripButton();
            this.cmdCC = new System.Windows.Forms.ToolStripButton();
            this.tsAfter = new System.Windows.Forms.ToolStripComboBox();
            this.scNew = new System.Windows.Forms.SplitContainer();
            this.lstIC = new WinToolkit.ListViewEx();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
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
            this.lstInstalled = new WinToolkit.ListViewEx();
            this.chUN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BWScan = new System.ComponentModel.BackgroundWorker();
            this.BWU = new System.ComponentModel.BackgroundWorker();
            this.BWDelete = new System.ComponentModel.BackgroundWorker();
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
            this.cmdR,
            this.toolStripSeparator2,
            this.cmdLDR,
            this.cmdCC,
            this.tsAfter});
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
            this.cmdAF.Size = new System.Drawing.Size(41, 25);
            this.cmdAF.Tag = "Add";
            this.cmdAF.Text = "Add";
            this.cmdAF.ToolTipText = "Add updates to the list to be installed.";
            this.cmdAF.Click += new System.EventHandler(this.cmdAF_Click);
            // 
            // cmdR
            // 
            this.cmdR.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdRS,
            this.cmdRA});
            this.cmdR.Name = "cmdR";
            this.cmdR.Size = new System.Drawing.Size(62, 25);
            this.cmdR.Tag = "Remove";
            this.cmdR.Text = "Remove";
            this.cmdR.ToolTipText = "Remove all updates from the list";
            // 
            // cmdRS
            // 
            this.cmdRS.Name = "cmdRS";
            this.cmdRS.Size = new System.Drawing.Size(210, 22);
            this.cmdRS.Text = "Remove Selected Updates";
            this.cmdRS.ToolTipText = "Removes all selected updates from the list.";
            this.cmdRS.Click += new System.EventHandler(this.cmdRS_Click);
            // 
            // cmdRA
            // 
            this.cmdRA.Name = "cmdRA";
            this.cmdRA.Size = new System.Drawing.Size(210, 22);
            this.cmdRA.Text = "Remove All Updates";
            this.cmdRA.ToolTipText = "Removes all updates from the list.";
            this.cmdRA.Click += new System.EventHandler(this.cmdRA_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdLDR
            // 
            this.cmdLDR.Checked = true;
            this.cmdLDR.CheckOnClick = true;
            this.cmdLDR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmdLDR.Image = global::WinToolkit.Properties.Resources.Checked;
            this.cmdLDR.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdLDR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdLDR.Name = "cmdLDR";
            this.cmdLDR.Size = new System.Drawing.Size(105, 22);
            this.cmdLDR.Text = "LDR/QFE Mode";
            this.cmdLDR.Click += new System.EventHandler(this.cmdLDR_Click);
            // 
            // cmdCC
            // 
            this.cmdCC.Checked = true;
            this.cmdCC.CheckOnClick = true;
            this.cmdCC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmdCC.Image = global::WinToolkit.Properties.Resources.Checked;
            this.cmdCC.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdCC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdCC.Name = "cmdCC";
            this.cmdCC.Size = new System.Drawing.Size(149, 22);
            this.cmdCC.Text = "Compatibility Checking";
            this.cmdCC.Click += new System.EventHandler(this.cmdCC_Click);
            // 
            // tsAfter
            // 
            this.tsAfter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsAfter.Items.AddRange(new object[] {
            "Do Nothing",
            "Restart",
            "Shutdown"});
            this.tsAfter.Name = "tsAfter";
            this.tsAfter.Size = new System.Drawing.Size(120, 25);
            // 
            // scNew
            // 
            this.scNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scNew.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scNew.IsSplitterFixed = true;
            this.scNew.Location = new System.Drawing.Point(0, 0);
            this.scNew.Margin = new System.Windows.Forms.Padding(0);
            this.scNew.Name = "scNew";
            this.scNew.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scNew.Panel1
            // 
            this.scNew.Panel1.Controls.Add(this.ToolStrip1);
            // 
            // scNew.Panel2
            // 
            this.scNew.Panel2.Controls.Add(this.lstIC);
            this.scNew.Size = new System.Drawing.Size(726, 313);
            this.scNew.SplitterDistance = 25;
            this.scNew.SplitterWidth = 1;
            this.scNew.TabIndex = 9;
            // 
            // lstIC
            // 
            this.lstIC.AllowDrop = true;
            this.lstIC.BackColor = System.Drawing.SystemColors.Window;
            this.lstIC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstIC.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2,
            this.ColumnHeader3,
            this.ColumnHeader4,
            this.ColumnHeader5,
            this.ColumnHeader6,
            this.ColumnHeader7});
            this.lstIC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstIC.FullRowSelect = true;
            this.lstIC.Location = new System.Drawing.Point(0, 0);
            this.lstIC.Name = "lstIC";
            this.lstIC.Size = new System.Drawing.Size(726, 287);
            this.lstIC.SmallImageList = this.imageList1;
            this.lstIC.TabIndex = 0;
            this.lstIC.UseCompatibleStateImageBehavior = false;
            this.lstIC.View = System.Windows.Forms.View.Details;
            this.lstIC.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstIC_ItemSelectionChanged);
            this.lstIC.SelectedIndexChanged += new System.EventHandler(this.lstIC_SelectedIndexChanged);
            this.lstIC.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstIC_DragDrop);
            this.lstIC.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstIC_DragEnter);
            this.lstIC.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstIC_MouseDoubleClick);
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Name";
            this.ColumnHeader1.Width = 147;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Description";
            this.ColumnHeader2.Width = 200;
            // 
            // ColumnHeader3
            // 
            this.ColumnHeader3.Text = "Size";
            // 
            // ColumnHeader4
            // 
            this.ColumnHeader4.Text = "Architecture";
            this.ColumnHeader4.Width = 71;
            // 
            // ColumnHeader5
            // 
            this.ColumnHeader5.Text = "Location";
            this.ColumnHeader5.Width = 74;
            // 
            // ColumnHeader6
            // 
            this.ColumnHeader6.Text = "MD5";
            // 
            // ColumnHeader7
            // 
            this.ColumnHeader7.Text = "Support";
            this.ColumnHeader7.Width = 82;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Play-icon.png");
            this.imageList1.Images.SetKeyName(1, "OK.png");
            this.imageList1.Images.SetKeyName(2, "Close.png");
            // 
            // BWI
            // 
            this.BWI.WorkerSupportsCancellation = true;
            this.BWI.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWI_DoWork);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(418, 17);
            this.lblStatus.Text = "This tool allows your to install or remove updates from your current OS install.";
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
            this.scInstalled.TabIndex = 10;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdRefresh,
            this.toolStripSeparator1,
            this.cmdU});
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
            this.cmdU.ToolTipText = "Uninstall updates...";
            this.cmdU.Click += new System.EventHandler(this.cmdU_Click);
            // 
            // lstInstalled
            // 
            this.lstInstalled.AllowDrop = true;
            this.lstInstalled.BackColor = System.Drawing.SystemColors.Window;
            this.lstInstalled.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstInstalled.CheckBoxes = true;
            this.lstInstalled.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chUN,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18});
            this.lstInstalled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstInstalled.FullRowSelect = true;
            this.lstInstalled.Location = new System.Drawing.Point(0, 0);
            this.lstInstalled.Name = "lstInstalled";
            this.lstInstalled.Size = new System.Drawing.Size(726, 287);
            this.lstInstalled.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstInstalled.TabIndex = 0;
            this.lstInstalled.UseCompatibleStateImageBehavior = false;
            this.lstInstalled.View = System.Windows.Forms.View.Details;
            // 
            // chUN
            // 
            this.chUN.Text = "Update Name";
            this.chUN.Width = 128;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Package Name";
            this.columnHeader15.Width = 99;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "State";
            this.columnHeader16.Width = 87;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Release Type";
            this.columnHeader17.Width = 117;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "Install Time";
            this.columnHeader18.Width = 116;
            // 
            // BWScan
            // 
            this.BWScan.WorkerSupportsCancellation = true;
            this.BWScan.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWScan_DoWork);
            // 
            // BWU
            // 
            this.BWU.WorkerSupportsCancellation = true;
            this.BWU.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWU_DoWork);
            // 
            // BWDelete
            // 
            this.BWDelete.WorkerSupportsCancellation = true;
            this.BWDelete.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWDelete_DoWork);
            // 
            // frmUpdInstaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 361);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.StatusStrip1);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(750, 400);
            this.Name = "frmUpdInstaller";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Installer";
            this.Load += new System.EventHandler(this.frmCabInstaller_Load);
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
        internal ListViewEx lstIC;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.ColumnHeader ColumnHeader2;
        internal System.Windows.Forms.ColumnHeader ColumnHeader3;
        internal System.Windows.Forms.ColumnHeader ColumnHeader4;
        internal System.Windows.Forms.ColumnHeader ColumnHeader5;
        internal System.Windows.Forms.ColumnHeader ColumnHeader6;
        internal System.Windows.Forms.ColumnHeader ColumnHeader7;
        internal System.Windows.Forms.SplitContainer scNew;
        internal System.ComponentModel.BackgroundWorker BWI;
        internal System.Windows.Forms.ToolStripStatusLabel lblStatus;
        internal System.Windows.Forms.StatusStrip StatusStrip1;
        internal System.ComponentModel.BackgroundWorker BWA;
        internal System.Windows.Forms.ToolStripProgressBar PB;
        private System.Windows.Forms.ToolStripComboBox tsAfter;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        internal System.Windows.Forms.SplitContainer scInstalled;
        internal System.Windows.Forms.ToolStrip toolStrip2;
        internal System.Windows.Forms.ToolStripMenuItem cmdU;
        internal ListViewEx lstInstalled;
        private System.Windows.Forms.ColumnHeader chUN;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        internal System.ComponentModel.BackgroundWorker BWScan;
        internal System.ComponentModel.BackgroundWorker BWU;
        internal System.Windows.Forms.ToolStripMenuItem cmdRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem cmdR;
        private System.Windows.Forms.ToolStripMenuItem cmdRS;
        private System.Windows.Forms.ToolStripMenuItem cmdRA;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cmdCC;
        private System.Windows.Forms.ToolStripButton cmdLDR;
        private System.Windows.Forms.ImageList imageList1;
        private System.ComponentModel.BackgroundWorker BWDelete;
    }
}