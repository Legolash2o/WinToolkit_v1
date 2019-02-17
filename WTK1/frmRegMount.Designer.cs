namespace WinToolkit
{
    partial class frmRegMount
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Main", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Users", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Misc", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Administrator",
            "WIM_Admin",
            "Registry for Administrator account.",
            "Users\\Administrator\\NTUSER.DAT"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Components",
            "WIM_Components",
            "Unknown",
            "Windows\\System32\\Config\\COMPONENTS"}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Default User",
            "WIM_Default",
            "Registry settings for the default user account",
            "Users\\Default\\NTUSER.DAT"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "SAM",
            "WIM_SAM",
            "HKLM\\SAM",
            "Windows\\System32\\Config\\SAM"}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "Software",
            "WIM_Software",
            "HKLM\\SOFTWARE",
            "Windows\\System32\\Config\\SOFTWARE"}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "System",
            "WIM_System",
            "HKLM\\SYSTEM",
            "Windows\\System32\\Config\\SYSTEM"}, -1);
            this.lstRegs = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdMA = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdU = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdUS = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdUD = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdIReg = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdMS = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ToolStrip1.SuspendLayout();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstRegs
            // 
            this.lstRegs.BackColor = System.Drawing.SystemColors.Window;
            this.lstRegs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstRegs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader4,
            this.ColumnHeader2});
            this.lstRegs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRegs.FullRowSelect = true;
            listViewGroup1.Header = "Main";
            listViewGroup1.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup1.Name = "GHMain";
            listViewGroup2.Header = "Users";
            listViewGroup2.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup2.Name = "GHUsers";
            listViewGroup3.Header = "Misc";
            listViewGroup3.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup3.Name = "GHMisc";
            this.lstRegs.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.lstRegs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem1.Group = listViewGroup2;
            listViewItem1.ToolTipText = "Location: <MountPath>\\Users\\Administrator\\NTUSER.DAT";
            listViewItem2.Group = listViewGroup3;
            listViewItem2.ToolTipText = "Location: <MountPath>\\Windows\\System32\\Config\\COMPONENTS";
            listViewItem3.Group = listViewGroup2;
            listViewItem3.ToolTipText = "Location: <MountPath>\\Users\\Default\\NTUSER.DAT";
            listViewItem4.Group = listViewGroup3;
            listViewItem4.ToolTipText = "Location: <MountPath>\\Windows\\System32\\Config\\SAM";
            listViewItem5.Group = listViewGroup1;
            listViewItem5.ToolTipText = "Location: <MountPath>\\Windows\\System32\\Config\\SOFTWARE";
            listViewItem6.Group = listViewGroup1;
            listViewItem6.ToolTipText = "Location: <MountPath>\\Windows\\System32\\Config\\SYSTEM";
            this.lstRegs.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.lstRegs.Location = new System.Drawing.Point(0, 0);
            this.lstRegs.Name = "lstRegs";
            this.lstRegs.Scrollable = false;
            this.lstRegs.Size = new System.Drawing.Size(684, 205);
            this.lstRegs.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstRegs.TabIndex = 0;
            this.lstRegs.UseCompatibleStateImageBehavior = false;
            this.lstRegs.View = System.Windows.Forms.View.Details;
            this.lstRegs.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstRegs_ItemSelectionChanged);
            this.lstRegs.DoubleClick += new System.EventHandler(this.lstRegs_DoubleClick);
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Hive Name";
            this.ColumnHeader1.Width = 83;
            // 
            // ColumnHeader4
            // 
            this.ColumnHeader4.Text = "Mount Path Key";
            this.ColumnHeader4.Width = 121;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Description";
            this.ColumnHeader2.Width = 479;
            // 
            // cmdMA
            // 
            this.cmdMA.Name = "cmdMA";
            this.cmdMA.Size = new System.Drawing.Size(147, 22);
            this.cmdMA.Tag = "Reg";
            this.cmdMA.Text = "Load All";
            this.cmdMA.ToolTipText = "Load all hives";
            this.cmdMA.Click += new System.EventHandler(this.cmdMA_Click);
            // 
            // cmdU
            // 
            this.cmdU.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdUS,
            this.cmdUD});
            this.cmdU.Name = "cmdU";
            this.cmdU.Size = new System.Drawing.Size(89, 25);
            this.cmdU.Tag = "Reg";
            this.cmdU.Text = "Unload Hives";
            this.cmdU.ToolTipText = "Unload Hive";
            this.cmdU.Visible = false;
            // 
            // cmdUS
            // 
            this.cmdUS.Name = "cmdUS";
            this.cmdUS.Size = new System.Drawing.Size(159, 22);
            this.cmdUS.Tag = "Reg";
            this.cmdUS.Text = "Unload Selected";
            this.cmdUS.ToolTipText = "Unload the selected hive";
            this.cmdUS.Click += new System.EventHandler(this.cmdUS_Click);
            // 
            // cmdUD
            // 
            this.cmdUD.Name = "cmdUD";
            this.cmdUD.Size = new System.Drawing.Size(159, 22);
            this.cmdUD.Tag = "Reg";
            this.cmdUD.Text = "Unload All";
            this.cmdUD.ToolTipText = "Unload all hives";
            this.cmdUD.Click += new System.EventHandler(this.cmdUD_Click);
            // 
            // cmdIReg
            // 
            this.cmdIReg.Name = "cmdIReg";
            this.cmdIReg.Size = new System.Drawing.Size(83, 25);
            this.cmdIReg.Tag = "Reg";
            this.cmdIReg.Text = "Import *.reg";
            this.cmdIReg.ToolTipText = "Imports a *.reg file";
            this.cmdIReg.Click += new System.EventHandler(this.cmdIReg_Click);
            // 
            // cmdMS
            // 
            this.cmdMS.Name = "cmdMS";
            this.cmdMS.Size = new System.Drawing.Size(147, 22);
            this.cmdMS.Tag = "Reg";
            this.cmdMS.Text = "Load Selected";
            this.cmdMS.ToolTipText = "Load the selected hive";
            this.cmdMS.Click += new System.EventHandler(this.cmdMS_Click);
            // 
            // cmdSelect
            // 
            this.cmdSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdMS,
            this.cmdMA});
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(77, 25);
            this.cmdSelect.Tag = "Reg";
            this.cmdSelect.Text = "Load Hives";
            this.cmdSelect.ToolTipText = "Load Hives";
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdSelect,
            this.cmdU,
            this.cmdIReg,
            this.lblStatus});
            this.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new System.Drawing.Size(684, 25);
            this.ToolStrip1.TabIndex = 1;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 22);
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
            this.SplitContainer1.Panel1.Controls.Add(this.lstRegs);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.ToolStrip1);
            this.SplitContainer1.Size = new System.Drawing.Size(684, 231);
            this.SplitContainer1.SplitterDistance = 205;
            this.SplitContainer1.SplitterWidth = 1;
            this.SplitContainer1.TabIndex = 1;
            // 
            // frmRegMount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 231);
            this.Controls.Add(this.SplitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(700, 270);
            this.Name = "frmRegMount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WIM Registry Editor";
            this.Load += new System.EventHandler(this.frmRegMount_Load);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.Panel2.PerformLayout();
            this.SplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView lstRegs;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.ColumnHeader ColumnHeader2;
        internal System.Windows.Forms.ColumnHeader ColumnHeader4;
        internal System.Windows.Forms.ToolStripMenuItem cmdMA;
        internal System.Windows.Forms.ToolStripMenuItem cmdU;
        internal System.Windows.Forms.ToolStripMenuItem cmdUS;
        internal System.Windows.Forms.ToolStripMenuItem cmdUD;
        internal System.Windows.Forms.ToolStripMenuItem cmdIReg;
        internal System.Windows.Forms.ToolStripMenuItem cmdMS;
        internal System.Windows.Forms.ToolStripMenuItem cmdSelect;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripLabel lblStatus;
        internal System.Windows.Forms.SplitContainer SplitContainer1;
    }
}