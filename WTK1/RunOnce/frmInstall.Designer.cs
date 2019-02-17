namespace RunOnce
{
	 partial class FrmInstall
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInstall));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSelect = new System.Windows.Forms.TabPage();
            this.scSelect = new System.Windows.Forms.SplitContainer();
            this.PanApps = new System.Windows.Forms.Panel();
            this.lstManual = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeselectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdStart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdSelectAll = new System.Windows.Forms.ToolStripButton();
            this.cmdDeselectAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tabPrograms = new System.Windows.Forms.TabPage();
            this.lstInstalling = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgIntegration = new System.Windows.Forms.ImageList(this.components);
            this.tabDrivers = new System.Windows.Forms.TabPage();
            this.lstDrivers = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgTab = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabSelect.SuspendLayout();
            this.scSelect.Panel1.SuspendLayout();
            this.scSelect.Panel2.SuspendLayout();
            this.scSelect.SuspendLayout();
            this.PanApps.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPrograms.SuspendLayout();
            this.tabDrivers.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(684, 391);
            this.splitContainer1.SplitterDistance = 72;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(76, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(596, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Please Wait...";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(74, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(598, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "WinToolkit Installer";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSelect);
            this.tabControl1.Controls.Add(this.tabPrograms);
            this.tabControl1.Controls.Add(this.tabDrivers);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imgTab;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(684, 318);
            this.tabControl1.TabIndex = 0;
            // 
            // tabSelect
            // 
            this.tabSelect.BackColor = System.Drawing.Color.Transparent;
            this.tabSelect.Controls.Add(this.scSelect);
            this.tabSelect.ImageIndex = 0;
            this.tabSelect.Location = new System.Drawing.Point(4, 27);
            this.tabSelect.Name = "tabSelect";
            this.tabSelect.Size = new System.Drawing.Size(676, 287);
            this.tabSelect.TabIndex = 0;
            this.tabSelect.Text = "Select";
            // 
            // scSelect
            // 
            this.scSelect.BackColor = System.Drawing.Color.Transparent;
            this.scSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scSelect.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scSelect.IsSplitterFixed = true;
            this.scSelect.Location = new System.Drawing.Point(0, 0);
            this.scSelect.Name = "scSelect";
            this.scSelect.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scSelect.Panel1
            // 
            this.scSelect.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.scSelect.Panel1.Controls.Add(this.PanApps);
            // 
            // scSelect.Panel2
            // 
            this.scSelect.Panel2.Controls.Add(this.toolStrip1);
            this.scSelect.Size = new System.Drawing.Size(676, 287);
            this.scSelect.SplitterDistance = 261;
            this.scSelect.SplitterWidth = 1;
            this.scSelect.TabIndex = 12;
            // 
            // PanApps
            // 
            this.PanApps.Controls.Add(this.lstManual);
            this.PanApps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanApps.Location = new System.Drawing.Point(0, 0);
            this.PanApps.Name = "PanApps";
            this.PanApps.Size = new System.Drawing.Size(676, 261);
            this.PanApps.TabIndex = 14;
            // 
            // lstManual
            // 
            this.lstManual.BackColor = System.Drawing.Color.White;
            this.lstManual.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstManual.CheckBoxes = true;
            this.lstManual.ContextMenuStrip = this.contextMenuStrip1;
            this.lstManual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstManual.FullRowSelect = true;
            this.lstManual.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstManual.Location = new System.Drawing.Point(0, 0);
            this.lstManual.Name = "lstManual";
            this.lstManual.ShowItemToolTips = true;
            this.lstManual.Size = new System.Drawing.Size(676, 261);
            this.lstManual.TabIndex = 0;
            this.lstManual.UseCompatibleStateImageBehavior = false;
            this.lstManual.View = System.Windows.Forms.View.List;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSelectAll,
            this.mnuDeselectAll});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(136, 48);
            // 
            // mnuSelectAll
            // 
            this.mnuSelectAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuSelectAll.Name = "mnuSelectAll";
            this.mnuSelectAll.Size = new System.Drawing.Size(135, 22);
            this.mnuSelectAll.Text = "Select All";
            this.mnuSelectAll.Click += new System.EventHandler(this.mnuSelectAll_Click);
            // 
            // mnuDeselectAll
            // 
            this.mnuDeselectAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuDeselectAll.Name = "mnuDeselectAll";
            this.mnuDeselectAll.Size = new System.Drawing.Size(135, 22);
            this.mnuDeselectAll.Text = "Deselect All";
            this.mnuDeselectAll.Click += new System.EventHandler(this.mnuDeselectAll_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdStart,
            this.toolStripSeparator2,
            this.cmdSelectAll,
            this.cmdDeselectAll,
            this.toolStripSeparator1,
            this.toolStripLabel2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(676, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdStart
            // 
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(93, 25);
            this.cmdStart.Tag = "";
            this.cmdStart.Text = "Add to Queue";
            this.cmdStart.ToolTipText = "Add these to the installation process.";
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdSelectAll
            // 
            this.cmdSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("cmdSelectAll.Image")));
            this.cmdSelectAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSelectAll.Name = "cmdSelectAll";
            this.cmdSelectAll.Size = new System.Drawing.Size(23, 22);
            this.cmdSelectAll.Text = "toolStripButton1";
            this.cmdSelectAll.ToolTipText = "Select all items";
            this.cmdSelectAll.Click += new System.EventHandler(this.cmdSelectAll_Click);
            // 
            // cmdDeselectAll
            // 
            this.cmdDeselectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdDeselectAll.Image = ((System.Drawing.Image)(resources.GetObject("cmdDeselectAll.Image")));
            this.cmdDeselectAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdDeselectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDeselectAll.Name = "cmdDeselectAll";
            this.cmdDeselectAll.Size = new System.Drawing.Size(23, 22);
            this.cmdDeselectAll.Text = "toolStripButton2";
            this.cmdDeselectAll.ToolTipText = "Deselect all items.";
            this.cmdDeselectAll.Click += new System.EventHandler(this.cmdDeselectAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(242, 22);
            this.toolStripLabel2.Text = "Please select the software you wish to install.";
            // 
            // tabPrograms
            // 
            this.tabPrograms.Controls.Add(this.lstInstalling);
            this.tabPrograms.ImageIndex = 2;
            this.tabPrograms.Location = new System.Drawing.Point(4, 27);
            this.tabPrograms.Name = "tabPrograms";
            this.tabPrograms.Size = new System.Drawing.Size(676, 287);
            this.tabPrograms.TabIndex = 1;
            this.tabPrograms.Text = "Programs";
            this.tabPrograms.UseVisualStyleBackColor = true;
            // 
            // lstInstalling
            // 
            this.lstInstalling.BackColor = System.Drawing.Color.White;
            this.lstInstalling.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstInstalling.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lstInstalling.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstInstalling.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstInstalling.FullRowSelect = true;
            this.lstInstalling.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstInstalling.Location = new System.Drawing.Point(0, 0);
            this.lstInstalling.MultiSelect = false;
            this.lstInstalling.Name = "lstInstalling";
            this.lstInstalling.ShowItemToolTips = true;
            this.lstInstalling.Size = new System.Drawing.Size(676, 287);
            this.lstInstalling.SmallImageList = this.imgIntegration;
            this.lstInstalling.TabIndex = 0;
            this.lstInstalling.UseCompatibleStateImageBehavior = false;
            this.lstInstalling.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Software...";
            this.columnHeader2.Width = 342;
            // 
            // imgIntegration
            // 
            this.imgIntegration.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgIntegration.ImageStream")));
            this.imgIntegration.TransparentColor = System.Drawing.Color.Transparent;
            this.imgIntegration.Images.SetKeyName(0, "OK.png");
            this.imgIntegration.Images.SetKeyName(1, "Play-1-Hot-icon.png");
            this.imgIntegration.Images.SetKeyName(2, "gtk-missing-image.png");
            this.imgIntegration.Images.SetKeyName(3, "image-missing.png");
            // 
            // tabDrivers
            // 
            this.tabDrivers.Controls.Add(this.lstDrivers);
            this.tabDrivers.ImageIndex = 1;
            this.tabDrivers.Location = new System.Drawing.Point(4, 27);
            this.tabDrivers.Name = "tabDrivers";
            this.tabDrivers.Size = new System.Drawing.Size(676, 287);
            this.tabDrivers.TabIndex = 2;
            this.tabDrivers.Text = "Drivers";
            this.tabDrivers.UseVisualStyleBackColor = true;
            // 
            // lstDrivers
            // 
            this.lstDrivers.BackColor = System.Drawing.Color.White;
            this.lstDrivers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstDrivers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstDrivers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDrivers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDrivers.FullRowSelect = true;
            this.lstDrivers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstDrivers.LabelWrap = false;
            this.lstDrivers.Location = new System.Drawing.Point(0, 0);
            this.lstDrivers.MultiSelect = false;
            this.lstDrivers.Name = "lstDrivers";
            this.lstDrivers.ShowItemToolTips = true;
            this.lstDrivers.Size = new System.Drawing.Size(676, 287);
            this.lstDrivers.SmallImageList = this.imgIntegration;
            this.lstDrivers.TabIndex = 1;
            this.lstDrivers.UseCompatibleStateImageBehavior = false;
            this.lstDrivers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Drivers...";
            this.columnHeader1.Width = 350;
            // 
            // imgTab
            // 
            this.imgTab.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTab.ImageStream")));
            this.imgTab.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTab.Images.SetKeyName(0, "Window-Add-icon.png");
            this.imgTab.Images.SetKeyName(1, "AIO_Drivers_20x20.png");
            this.imgTab.Images.SetKeyName(2, "AIO_Silent_20x20.png");
            this.imgTab.Images.SetKeyName(3, "AIO_Updates_20x20.png");
            // 
            // FrmInstall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 391);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(700, 430);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 430);
            this.Name = "FrmInstall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RunOnce Installer [0.0%]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInstall_FormClosing);
            this.Load += new System.EventHandler(this.frmInstall_Load);
            this.Shown += new System.EventHandler(this.frmInstall_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabSelect.ResumeLayout(false);
            this.scSelect.Panel1.ResumeLayout(false);
            this.scSelect.Panel2.ResumeLayout(false);
            this.scSelect.Panel2.PerformLayout();
            this.scSelect.ResumeLayout(false);
            this.PanApps.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPrograms.ResumeLayout(false);
            this.tabDrivers.ResumeLayout(false);
            this.ResumeLayout(false);

		  }

		  #endregion

		  private System.Windows.Forms.SplitContainer splitContainer1;
		  private System.Windows.Forms.TabControl tabControl1;
		  private System.Windows.Forms.TabPage tabSelect;
		  private System.Windows.Forms.TabPage tabPrograms;
		  private System.Windows.Forms.PictureBox pictureBox1;
		  internal System.Windows.Forms.SplitContainer scSelect;
		  private System.Windows.Forms.Panel PanApps;
		  private System.Windows.Forms.ListView lstManual;
		  private System.Windows.Forms.ToolStrip toolStrip1;
		  internal System.Windows.Forms.ToolStripMenuItem cmdStart;
		  private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		  private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		  private System.Windows.Forms.ListView lstInstalling;
		  private System.Windows.Forms.Label label2;
		  private System.Windows.Forms.Label label1;
		  private System.Windows.Forms.ListView lstDrivers;
		  private System.Windows.Forms.ColumnHeader columnHeader2;
		  private System.Windows.Forms.ColumnHeader columnHeader1;
		  private System.Windows.Forms.TabPage tabDrivers;
		  private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		  private System.Windows.Forms.ToolStripMenuItem mnuSelectAll;
		  private System.Windows.Forms.ToolStripMenuItem mnuDeselectAll;
		  private System.Windows.Forms.ToolStripButton cmdSelectAll;
		  private System.Windows.Forms.ToolStripButton cmdDeselectAll;
		  private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		  private System.Windows.Forms.ImageList imgTab;
		  private System.Windows.Forms.ImageList imgIntegration;
	 }
}