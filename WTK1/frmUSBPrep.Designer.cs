namespace WinToolkit
{
    partial class frmUSBPrep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUSBPrep));
            this.lstDisk = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdQuick = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdStart = new System.Windows.Forms.ToolStripMenuItem();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.cboFS = new System.Windows.Forms.ToolStripComboBox();
            this.PBRemove = new System.Windows.Forms.ToolStripProgressBar();
            this.bwUSB = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ToolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstDisk
            // 
            this.lstDisk.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1,
            this.columnHeader11,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader8,
            this.columnHeader5,
            this.columnHeader10,
            this.columnHeader7,
            this.columnHeader9});
            this.lstDisk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDisk.FullRowSelect = true;
            this.lstDisk.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstDisk.Location = new System.Drawing.Point(0, 0);
            this.lstDisk.Name = "lstDisk";
            this.lstDisk.Size = new System.Drawing.Size(734, 233);
            this.lstDisk.TabIndex = 4;
            this.lstDisk.UseCompatibleStateImageBehavior = false;
            this.lstDisk.View = System.Windows.Forms.View.Details;
            this.lstDisk.SelectedIndexChanged += new System.EventHandler(this.lstDisk_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Letter";
            this.columnHeader2.Width = 49;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Disk";
            this.columnHeader1.Width = 41;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Partition";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Label";
            this.columnHeader3.Width = 52;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Format";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Used";
            this.columnHeader6.Width = 50;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Free";
            this.columnHeader8.Width = 42;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Size";
            this.columnHeader5.Width = 41;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Type";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Bootable";
            this.columnHeader7.Width = 72;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Status";
            this.columnHeader9.Width = 55;
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ToolStrip1.CanOverflow = false;
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdRefresh,
            this.toolStripSeparator1,
            this.cmdQuick,
            this.cmdStart,
            this.lblStatus,
            this.cboFS,
            this.PBRemove});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.Size = new System.Drawing.Size(734, 27);
            this.ToolStrip1.TabIndex = 5;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Image = global::WinToolkit.Properties.Resources.refresh2_20x20;
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(32, 25);
            this.cmdRefresh.Tag = "";
            this.cmdRefresh.ToolTipText = "Rescan for devices";
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdQuick
            // 
            this.cmdQuick.Image = ((System.Drawing.Image)(resources.GetObject("cmdQuick.Image")));
            this.cmdQuick.Name = "cmdQuick";
            this.cmdQuick.Size = new System.Drawing.Size(70, 27);
            this.cmdQuick.Tag = "";
            this.cmdQuick.Text = "Quick";
            this.cmdQuick.ToolTipText = "Quick method does not require a format.";
            this.cmdQuick.Click += new System.EventHandler(this.cmdQuick_Click);
            // 
            // cmdStart
            // 
            this.cmdStart.Image = ((System.Drawing.Image)(resources.GetObject("cmdStart.Image")));
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(107, 25);
            this.cmdStart.Tag = "";
            this.cmdStart.Text = "Full (Format)";
            this.cmdStart.ToolTipText = "Full required a format, all data will be lost!";
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.lblStatus.Size = new System.Drawing.Size(0, 22);
            // 
            // cboFS
            // 
            this.cboFS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFS.Items.AddRange(new object[] {
            "NTFS",
            "FAT32"});
            this.cboFS.Name = "cboFS";
            this.cboFS.Size = new System.Drawing.Size(121, 25);
            // 
            // PBRemove
            // 
            this.PBRemove.Name = "PBRemove";
            this.PBRemove.Size = new System.Drawing.Size(150, 22);
            this.PBRemove.Visible = false;
            // 
            // bwUSB
            // 
            this.bwUSB.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwUSB_DoWork);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstDisk);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ToolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(734, 261);
            this.splitContainer1.SplitterDistance = 233;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 6;
            this.splitContainer1.TabStop = false;
            // 
            // frmUSBPrep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(734, 261);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(750, 300);
            this.Name = "frmUSBPrep";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "USB Prep Tool";
            this.Load += new System.EventHandler(this.frmUSBPrep_Load);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstDisk;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem cmdStart;
        internal System.Windows.Forms.ToolStripProgressBar PBRemove;
        internal System.Windows.Forms.ToolStripLabel lblStatus;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.ComponentModel.BackgroundWorker bwUSB;
        internal System.Windows.Forms.ToolStripMenuItem cmdQuick;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ToolStripComboBox cboFS;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        internal System.Windows.Forms.ToolStripMenuItem cmdRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}