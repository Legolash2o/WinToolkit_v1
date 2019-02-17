namespace WinToolkit
{
    partial class frmAIOPresetManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAIOPresetManager));
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.chkPresetMan = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tvPreset = new System.Windows.Forms.TreeView();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.cmdLoadPreset = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSkip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdRenamePreset = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdDeletePreset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdImportPreset = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdExportPreset = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer5);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.toolStrip3);
            this.splitContainer4.Size = new System.Drawing.Size(1016, 432);
            this.splitContainer4.SplitterDistance = 406;
            this.splitContainer4.SplitterWidth = 1;
            this.splitContainer4.TabIndex = 3;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer5.IsSplitterFixed = true;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.chkPresetMan);
            this.splitContainer5.Panel1.Controls.Add(this.label5);
            this.splitContainer5.Panel1MinSize = 50;
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.tvPreset);
            this.splitContainer5.Size = new System.Drawing.Size(1016, 406);
            this.splitContainer5.SplitterWidth = 1;
            this.splitContainer5.TabIndex = 0;
            // 
            // chkPresetMan
            // 
            this.chkPresetMan.AutoSize = true;
            this.chkPresetMan.Location = new System.Drawing.Point(3, 33);
            this.chkPresetMan.Name = "chkPresetMan";
            this.chkPresetMan.Size = new System.Drawing.Size(131, 17);
            this.chkPresetMan.TabIndex = 1;
            this.chkPresetMan.Text = "Show Preset Manager";
            this.chkPresetMan.UseVisualStyleBackColor = true;
            this.chkPresetMan.CheckedChanged += new System.EventHandler(this.chkPresetMan_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1016, 50);
            this.label5.TabIndex = 0;
            this.label5.Text = "If you have used the \'All-In-One Integrator\' before then this screen will let you" +
    " load previous presets. This will save you time adding all the files and select " +
    "components and/or tweaks again.";
            // 
            // tvPreset
            // 
            this.tvPreset.CheckBoxes = true;
            this.tvPreset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvPreset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvPreset.Location = new System.Drawing.Point(0, 0);
            this.tvPreset.Name = "tvPreset";
            this.tvPreset.Size = new System.Drawing.Size(1016, 355);
            this.tvPreset.TabIndex = 0;
            this.tvPreset.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvPreset_AfterCheck);
            this.tvPreset.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPreset_AfterSelect);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip3.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdLoadPreset,
            this.cmdSkip,
            this.toolStripSeparator1,
            this.cmdRenamePreset,
            this.cmdDeletePreset,
            this.toolStripSeparator2,
            this.cmdImportPreset,
            this.cmdExportPreset});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1016, 25);
            this.toolStrip3.TabIndex = 2;
            this.toolStrip3.TabStop = true;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // cmdLoadPreset
            // 
            this.cmdLoadPreset.Image = ((System.Drawing.Image)(resources.GetObject("cmdLoadPreset.Image")));
            this.cmdLoadPreset.Name = "cmdLoadPreset";
            this.cmdLoadPreset.Size = new System.Drawing.Size(96, 25);
            this.cmdLoadPreset.Tag = "OK";
            this.cmdLoadPreset.Text = "Load Preset";
            this.cmdLoadPreset.ToolTipText = "Load the selected preset.";
            this.cmdLoadPreset.Click += new System.EventHandler(this.cmdLoadPreset_Click);
            // 
            // cmdSkip
            // 
            this.cmdSkip.Image = ((System.Drawing.Image)(resources.GetObject("cmdSkip.Image")));
            this.cmdSkip.Name = "cmdSkip";
            this.cmdSkip.Size = new System.Drawing.Size(119, 25);
            this.cmdSkip.Tag = "";
            this.cmdSkip.Text = "Skip (No Preset)";
            this.cmdSkip.ToolTipText = "Continue to AIO Tool with no preset.";
            this.cmdSkip.Click += new System.EventHandler(this.cmdSkip_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdRenamePreset
            // 
            this.cmdRenamePreset.Image = ((System.Drawing.Image)(resources.GetObject("cmdRenamePreset.Image")));
            this.cmdRenamePreset.Name = "cmdRenamePreset";
            this.cmdRenamePreset.Size = new System.Drawing.Size(78, 25);
            this.cmdRenamePreset.Tag = "Edit";
            this.cmdRenamePreset.Text = "Rename";
            this.cmdRenamePreset.ToolTipText = "Rename the selected preset";
            this.cmdRenamePreset.Click += new System.EventHandler(this.cmdRenamePreset_Click);
            // 
            // cmdDeletePreset
            // 
            this.cmdDeletePreset.Image = ((System.Drawing.Image)(resources.GetObject("cmdDeletePreset.Image")));
            this.cmdDeletePreset.Name = "cmdDeletePreset";
            this.cmdDeletePreset.Size = new System.Drawing.Size(68, 25);
            this.cmdDeletePreset.Tag = "Remove";
            this.cmdDeletePreset.Text = "Delete";
            this.cmdDeletePreset.ToolTipText = "Delete the selected preset";
            this.cmdDeletePreset.Click += new System.EventHandler(this.cmdDeletePreset_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdImportPreset
            // 
            this.cmdImportPreset.Image = ((System.Drawing.Image)(resources.GetObject("cmdImportPreset.Image")));
            this.cmdImportPreset.Name = "cmdImportPreset";
            this.cmdImportPreset.Size = new System.Drawing.Size(71, 25);
            this.cmdImportPreset.Tag = "OK";
            this.cmdImportPreset.Text = "Import";
            this.cmdImportPreset.ToolTipText = "Import a preset.";
            this.cmdImportPreset.Click += new System.EventHandler(this.cmdImportPreset_Click);
            // 
            // cmdExportPreset
            // 
            this.cmdExportPreset.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportPreset.Image")));
            this.cmdExportPreset.Name = "cmdExportPreset";
            this.cmdExportPreset.Size = new System.Drawing.Size(68, 25);
            this.cmdExportPreset.Tag = "Search";
            this.cmdExportPreset.Text = "Export";
            this.cmdExportPreset.ToolTipText = "Export a preset.";
            this.cmdExportPreset.Click += new System.EventHandler(this.cmdExportPreset_Click);
            // 
            // frmAIOPresetManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1016, 432);
            this.Controls.Add(this.splitContainer4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAIOPresetManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preset List";
            this.Load += new System.EventHandler(this.frmPreset_Load);
            this.Shown += new System.EventHandler(this.frmPreset_Shown);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.CheckBox chkPresetMan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TreeView tvPreset;
        internal System.Windows.Forms.ToolStrip toolStrip3;
        internal System.Windows.Forms.ToolStripMenuItem cmdLoadPreset;
        internal System.Windows.Forms.ToolStripMenuItem cmdSkip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem cmdRenamePreset;
        internal System.Windows.Forms.ToolStripMenuItem cmdDeletePreset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripMenuItem cmdImportPreset;
        internal System.Windows.Forms.ToolStripMenuItem cmdExportPreset;
    }
}