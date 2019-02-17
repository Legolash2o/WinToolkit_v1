namespace WinToolkit
{
    partial class frmComponentRemover
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
            System.Windows.Forms.ListViewGroup listViewGroup16 = new System.Windows.Forms.ListViewGroup("Accessories", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup17 = new System.Windows.Forms.ListViewGroup("Drivers", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup18 = new System.Windows.Forms.ListViewGroup("Language Packs", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup19 = new System.Windows.Forms.ListViewGroup("Multimedia", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup20 = new System.Windows.Forms.ListViewGroup("Networking", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup21 = new System.Windows.Forms.ListViewGroup("Internet Information Services", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup22 = new System.Windows.Forms.ListViewGroup("Mobile PC", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup23 = new System.Windows.Forms.ListViewGroup("Printing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup24 = new System.Windows.Forms.ListViewGroup("System", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup25 = new System.Windows.Forms.ListViewGroup("Virtual PC", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup26 = new System.Windows.Forms.ListViewGroup("Misc", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup27 = new System.Windows.Forms.ListViewGroup("Metro", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup28 = new System.Windows.Forms.ListViewGroup("Unknown", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup29 = new System.Windows.Forms.ListViewGroup("Windows Setup DVD", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup30 = new System.Windows.Forms.ListViewGroup("Updates", System.Windows.Forms.HorizontalAlignment.Left);
            this.lstCL = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsCR = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsSUI = new System.Windows.Forms.ToolStripMenuItem();
            this.CMSsf = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdOpenPackageMum = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.PBRemove = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.BWRemove = new System.ComponentModel.BackgroundWorker();
            this.cmdStart = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BWStart = new System.ComponentModel.BackgroundWorker();
            this.cmdUnselectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCR.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstCL
            // 
            this.lstCL.BackColor = System.Drawing.SystemColors.Window;
            this.lstCL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstCL.CheckBoxes = true;
            this.lstCL.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.columnHeader6,
            this.columnHeader7});
            this.lstCL.ContextMenuStrip = this.cmsCR;
            this.lstCL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCL.FullRowSelect = true;
            listViewGroup16.Header = "Accessories";
            listViewGroup16.Name = "ListViewGroup1";
            listViewGroup17.Header = "Drivers";
            listViewGroup17.Name = "ListViewGroup2";
            listViewGroup18.Header = "Language Packs";
            listViewGroup18.Name = "ListViewGroup16";
            listViewGroup19.Header = "Multimedia";
            listViewGroup19.Name = "ListViewGroup5";
            listViewGroup20.Header = "Networking";
            listViewGroup20.Name = "ListViewGroup7";
            listViewGroup21.Header = "Internet Information Services";
            listViewGroup21.Name = "ListViewGroup4";
            listViewGroup22.Header = "Mobile PC";
            listViewGroup22.Name = "ListViewGroup6";
            listViewGroup23.Header = "Printing";
            listViewGroup23.Name = "ListViewGroup8";
            listViewGroup24.Header = "System";
            listViewGroup24.Name = "ListViewGroup9";
            listViewGroup25.Header = "Virtual PC";
            listViewGroup25.Name = "ListViewGroup11";
            listViewGroup26.Header = "Misc";
            listViewGroup26.Name = "ListViewGroup13";
            listViewGroup27.Header = "Metro";
            listViewGroup27.Name = "listViewGroup2";
            listViewGroup28.Header = "Unknown";
            listViewGroup28.Name = "ListViewGroup14";
            listViewGroup29.Header = "Windows Setup DVD";
            listViewGroup29.Name = "ListViewGroup17";
            listViewGroup30.Header = "Updates";
            listViewGroup30.Name = "listViewGroup1";
            this.lstCL.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup16,
            listViewGroup17,
            listViewGroup18,
            listViewGroup19,
            listViewGroup20,
            listViewGroup21,
            listViewGroup22,
            listViewGroup23,
            listViewGroup24,
            listViewGroup25,
            listViewGroup26,
            listViewGroup27,
            listViewGroup28,
            listViewGroup29,
            listViewGroup30});
            this.lstCL.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstCL.Location = new System.Drawing.Point(0, 0);
            this.lstCL.Name = "lstCL";
            this.lstCL.ShowItemToolTips = true;
            this.lstCL.Size = new System.Drawing.Size(734, 335);
            this.lstCL.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstCL.TabIndex = 0;
            this.lstCL.UseCompatibleStateImageBehavior = false;
            this.lstCL.View = System.Windows.Forms.View.Details;
            this.lstCL.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstCL_ItemChecked);
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Name";
            this.ColumnHeader1.Width = 218;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "State";
            this.columnHeader6.Width = 104;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Package";
            this.columnHeader7.Width = 299;
            // 
            // cmsCR
            // 
            this.cmsCR.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsSUI,
            this.CMSsf,
            this.cmdOpenPackageMum});
            this.cmsCR.Name = "cmsCR";
            this.cmsCR.Size = new System.Drawing.Size(194, 70);
            this.cmsCR.Opening += new System.ComponentModel.CancelEventHandler(this.cmsCR_Opening);
            // 
            // cmsSUI
            // 
            this.cmsSUI.Image = global::WinToolkit.Properties.Resources.Save;
            this.cmsSUI.Name = "cmsSUI";
            this.cmsSUI.Size = new System.Drawing.Size(193, 22);
            this.cmsSUI.Text = "Save \'Unknown\' to File";
            this.cmsSUI.Click += new System.EventHandler(this.cmsSUI_Click);
            // 
            // CMSsf
            // 
            this.CMSsf.Image = global::WinToolkit.Properties.Resources.Save;
            this.CMSsf.Name = "CMSsf";
            this.CMSsf.Size = new System.Drawing.Size(193, 22);
            this.CMSsf.Text = "Save to File";
            this.CMSsf.Click += new System.EventHandler(this.CMSsf_Click);
            // 
            // cmdOpenPackageMum
            // 
            this.cmdOpenPackageMum.Image = global::WinToolkit.Properties.Resources.Search;
            this.cmdOpenPackageMum.Name = "cmdOpenPackageMum";
            this.cmdOpenPackageMum.Size = new System.Drawing.Size(193, 22);
            this.cmdOpenPackageMum.Text = "Open Package MUM";
            this.cmdOpenPackageMum.Click += new System.EventHandler(this.cmdOpenPackageMum_Click);
            // 
            // cmdSelectAll
            // 
            this.cmdSelectAll.Name = "cmdSelectAll";
            this.cmdSelectAll.Size = new System.Drawing.Size(67, 25);
            this.cmdSelectAll.Tag = "Refresh";
            this.cmdSelectAll.Text = "Select All";
            this.cmdSelectAll.ToolTipText = "Select all components";
            this.cmdSelectAll.Click += new System.EventHandler(this.cmdSelectAll_Click);
            // 
            // cmdHelp
            // 
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(44, 25);
            this.cmdHelp.Tag = "Info";
            this.cmdHelp.Text = "Help";
            this.cmdHelp.ToolTipText = "Help Information";
            this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
            // 
            // PBRemove
            // 
            this.PBRemove.Name = "PBRemove";
            this.PBRemove.Size = new System.Drawing.Size(150, 22);
            this.PBRemove.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.lblStatus.Size = new System.Drawing.Size(26, 22);
            this.lblStatus.Text = "Idle";
            // 
            // BWRemove
            // 
            this.BWRemove.WorkerSupportsCancellation = true;
            this.BWRemove.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWRemove_DoWork);
            // 
            // cmdStart
            // 
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(134, 25);
            this.cmdStart.Tag = "OK";
            this.cmdStart.Text = "Remove Components";
            this.cmdStart.ToolTipText = "Start removing components!";
            this.cmdStart.Visible = false;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.AutoSize = false;
            this.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ToolStrip1.CanOverflow = false;
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdStart,
            this.cmdSelectAll,
            this.cmdUnselectAll,
            this.cmdHelp,
            this.PBRemove,
            this.toolStripSeparator1,
            this.lblStatus});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.Size = new System.Drawing.Size(734, 25);
            this.ToolStrip1.TabIndex = 2;
            this.ToolStrip1.TabStop = true;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer1.IsSplitterFixed = true;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer1.Name = "SplitContainer1";
            this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.ToolStrip1);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.lstCL);
            this.SplitContainer1.Size = new System.Drawing.Size(734, 361);
            this.SplitContainer1.SplitterDistance = 25;
            this.SplitContainer1.SplitterWidth = 1;
            this.SplitContainer1.TabIndex = 5;
            // 
            // BWStart
            // 
            this.BWStart.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWStart_DoWork);
            // 
            // cmdUnselectAll
            // 
            this.cmdUnselectAll.Name = "cmdUnselectAll";
            this.cmdUnselectAll.Size = new System.Drawing.Size(81, 25);
            this.cmdUnselectAll.Tag = "Refresh";
            this.cmdUnselectAll.Text = "Unselect All";
            this.cmdUnselectAll.ToolTipText = "Unselect all components";
            this.cmdUnselectAll.Click += new System.EventHandler(this.cmdUnselectAll_Click);
            // 
            // frmComponentRemover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(734, 361);
            this.Controls.Add(this.SplitContainer1);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(750, 400);
            this.Name = "frmComponentRemover";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Component Remover";
            this.Load += new System.EventHandler(this.frmCR_Load);
            this.cmsCR.ResumeLayout(false);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView lstCL;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.ToolStripMenuItem cmdSelectAll;
        internal System.Windows.Forms.ToolStripMenuItem cmdHelp;
        internal System.Windows.Forms.ToolStripProgressBar PBRemove;
        internal System.Windows.Forms.ToolStripLabel lblStatus;
        internal System.ComponentModel.BackgroundWorker BWRemove;
        internal System.Windows.Forms.ToolStripMenuItem cmdStart;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal System.ComponentModel.BackgroundWorker BWStart;
        private System.Windows.Forms.ContextMenuStrip cmsCR;
        private System.Windows.Forms.ToolStripMenuItem CMSsf;
        private System.Windows.Forms.ToolStripMenuItem cmsSUI;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cmdOpenPackageMum;
        internal System.Windows.Forms.ToolStripMenuItem cmdUnselectAll;
    }
}