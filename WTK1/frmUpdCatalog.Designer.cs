namespace WinToolkit
{
	 partial class frmUpdCatalog
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
            System.Windows.Forms.ListViewGroup listViewGroup10 = new System.Windows.Forms.ListViewGroup("New Updates [Recommended]", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup11 = new System.Windows.Forms.ListViewGroup("Additional\'s [Optional]", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup12 = new System.Windows.Forms.ListViewGroup("Already Downloaded Updates", System.Windows.Forms.HorizontalAlignment.Center);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdCatalog));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("General", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Hotfix", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Security", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup13 = new System.Windows.Forms.ListViewGroup("Extra", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup14 = new System.Windows.Forms.ListViewGroup("Additional", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup15 = new System.Windows.Forms.ListViewGroup("Misc", System.Windows.Forms.HorizontalAlignment.Center);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chkScan = new System.Windows.Forms.CheckBox();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.txtDownload = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboSU = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabNew = new System.Windows.Forms.TabPage();
            this.lstNew = new WinToolkit.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsUSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdACheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdAUnCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabCurrent = new System.Windows.Forms.TabPage();
            this.lstOld = new WinToolkit.ListViewEx();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabOld = new System.Windows.Forms.TabPage();
            this.lstDel = new WinToolkit.ListViewEx();
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.mnuDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdScanOld = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdDelOld = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pbProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.BWDownload = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabNew.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabCurrent.SuspendLayout();
            this.tabOld.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chkScan);
            this.splitContainer1.Panel1.Controls.Add(this.cmdRefresh);
            this.splitContainer1.Panel1.Controls.Add(this.cmdBrowse);
            this.splitContainer1.Panel1.Controls.Add(this.txtDownload);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.cboSU);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1MinSize = 50;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.scMain);
            this.splitContainer1.Size = new System.Drawing.Size(880, 393);
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // chkScan
            // 
            this.chkScan.AutoSize = true;
            this.chkScan.Checked = true;
            this.chkScan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkScan.Location = new System.Drawing.Point(45, 31);
            this.chkScan.Name = "chkScan";
            this.chkScan.Size = new System.Drawing.Size(196, 17);
            this.chkScan.TabIndex = 8;
            this.chkScan.Text = "Scan for Old / Superseded Updates";
            this.chkScan.UseVisualStyleBackColor = true;
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.BackgroundImage = global::WinToolkit.Properties.Resources.Refresh;
            this.cmdRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdRefresh.Location = new System.Drawing.Point(329, 5);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(25, 25);
            this.cmdRefresh.TabIndex = 7;
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Image = global::WinToolkit.Properties.Resources.folder_48;
            this.cmdBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdBrowse.Location = new System.Drawing.Point(848, 5);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(25, 25);
            this.cmdBrowse.TabIndex = 6;
            this.cmdBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // txtDownload
            // 
            this.txtDownload.Location = new System.Drawing.Point(421, 8);
            this.txtDownload.Name = "txtDownload";
            this.txtDownload.ReadOnly = true;
            this.txtDownload.Size = new System.Drawing.Size(421, 20);
            this.txtDownload.TabIndex = 5;
            this.txtDownload.TextChanged += new System.EventHandler(this.txtDownload_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(360, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Location:";
            // 
            // cboSU
            // 
            this.cboSU.FormattingEnabled = true;
            this.cboSU.Items.AddRange(new object[] {
            "Windows 7 x64",
            "Windows 7 x86",
            "Windows 8.1 x64",
            "Windows 8.1 x86",
            "Office 2013 x64",
            "Office 2013 x86"});
            this.cboSU.Location = new System.Drawing.Point(45, 7);
            this.cboSU.Name = "cboSU";
            this.cboSU.Size = new System.Drawing.Size(278, 21);
            this.cboSU.TabIndex = 1;
            this.cboSU.SelectedIndexChanged += new System.EventHandler(this.cboSU_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type:";
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scMain.IsSplitterFixed = true;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Margin = new System.Windows.Forms.Padding(0);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.tabMain);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.tsMain);
            this.scMain.Panel2MinSize = 35;
            this.scMain.Size = new System.Drawing.Size(878, 340);
            this.scMain.SplitterDistance = 304;
            this.scMain.SplitterWidth = 1;
            this.scMain.TabIndex = 1;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabNew);
            this.tabMain.Controls.Add(this.tabCurrent);
            this.tabMain.Controls.Add(this.tabOld);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.ImageList = this.imageList2;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(878, 304);
            this.tabMain.TabIndex = 1;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.TabChanged);
            // 
            // tabNew
            // 
            this.tabNew.Controls.Add(this.lstNew);
            this.tabNew.ImageIndex = 0;
            this.tabNew.Location = new System.Drawing.Point(4, 39);
            this.tabNew.Name = "tabNew";
            this.tabNew.Size = new System.Drawing.Size(870, 261);
            this.tabNew.TabIndex = 0;
            this.tabNew.Text = "New";
            this.tabNew.UseVisualStyleBackColor = true;
            // 
            // lstNew
            // 
            this.lstNew.CheckBoxes = true;
            this.lstNew.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lstNew.ContextMenuStrip = this.contextMenuStrip1;
            this.lstNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstNew.FullRowSelect = true;
            listViewGroup10.Header = "New Updates [Recommended]";
            listViewGroup10.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup10.Name = "listViewGroup1";
            listViewGroup11.Header = "Additional\'s [Optional]";
            listViewGroup11.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup11.Name = "listViewGroup3";
            listViewGroup12.Header = "Already Downloaded Updates";
            listViewGroup12.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup12.Name = "listViewGroup2";
            this.lstNew.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup10,
            listViewGroup11,
            listViewGroup12});
            this.lstNew.Location = new System.Drawing.Point(0, 0);
            this.lstNew.Name = "lstNew";
            this.lstNew.ShowItemToolTips = true;
            this.lstNew.Size = new System.Drawing.Size(870, 261);
            this.lstNew.SmallImageList = this.imageList1;
            this.lstNew.TabIndex = 0;
            this.lstNew.UseCompatibleStateImageBehavior = false;
            this.lstNew.View = System.Windows.Forms.View.Details;
            this.lstNew.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstS_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 169;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.Width = 81;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            this.columnHeader3.Width = 61;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Date (YYYY/MM/DD)";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Folder";
            this.columnHeader5.Width = 185;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsSelectAll,
            this.cmsUSelectAll,
            this.toolStripSeparator2,
            this.cmdACheckAll,
            this.cmdAUnCheckAll});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(204, 98);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // cmsSelectAll
            // 
            this.cmsSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("cmsSelectAll.Image")));
            this.cmsSelectAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmsSelectAll.Name = "cmsSelectAll";
            this.cmsSelectAll.Size = new System.Drawing.Size(203, 22);
            this.cmsSelectAll.Text = "Check All [Group]";
            this.cmsSelectAll.Click += new System.EventHandler(this.cmsSelectAll_Click);
            // 
            // cmsUSelectAll
            // 
            this.cmsUSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("cmsUSelectAll.Image")));
            this.cmsUSelectAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmsUSelectAll.Name = "cmsUSelectAll";
            this.cmsUSelectAll.Size = new System.Drawing.Size(203, 22);
            this.cmsUSelectAll.Text = "Uncheck All [Group]";
            this.cmsUSelectAll.Click += new System.EventHandler(this.cmsUSelectAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(200, 6);
            // 
            // cmdACheckAll
            // 
            this.cmdACheckAll.Image = ((System.Drawing.Image)(resources.GetObject("cmdACheckAll.Image")));
            this.cmdACheckAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdACheckAll.Name = "cmdACheckAll";
            this.cmdACheckAll.Size = new System.Drawing.Size(203, 22);
            this.cmdACheckAll.Text = "Check All [All Groups]";
            this.cmdACheckAll.Click += new System.EventHandler(this.cmdACheckAll_Click);
            // 
            // cmdAUnCheckAll
            // 
            this.cmdAUnCheckAll.Image = ((System.Drawing.Image)(resources.GetObject("cmdAUnCheckAll.Image")));
            this.cmdAUnCheckAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdAUnCheckAll.Name = "cmdAUnCheckAll";
            this.cmdAUnCheckAll.Size = new System.Drawing.Size(203, 22);
            this.cmdAUnCheckAll.Text = "Uncheck All [All Groups]";
            this.cmdAUnCheckAll.Click += new System.EventHandler(this.cmdAUnCheckAll_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icon.png");
            this.imageList1.Images.SetKeyName(1, "adept_installer.png");
            this.imageList1.Images.SetKeyName(2, "File CAB.png");
            this.imageList1.Images.SetKeyName(3, "AIO_Addons_20x20.png");
            // 
            // tabCurrent
            // 
            this.tabCurrent.Controls.Add(this.lstOld);
            this.tabCurrent.ImageIndex = 1;
            this.tabCurrent.Location = new System.Drawing.Point(4, 39);
            this.tabCurrent.Name = "tabCurrent";
            this.tabCurrent.Size = new System.Drawing.Size(870, 258);
            this.tabCurrent.TabIndex = 1;
            this.tabCurrent.Text = "Downloaded";
            this.tabCurrent.UseVisualStyleBackColor = true;
            // 
            // lstOld
            // 
            this.lstOld.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader16});
            this.lstOld.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOld.FullRowSelect = true;
            listViewGroup1.Header = "General";
            listViewGroup1.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "Hotfix";
            listViewGroup2.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup2.Name = "listViewGroup3";
            listViewGroup3.Header = "Security";
            listViewGroup3.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup3.Name = "listViewGroup2";
            listViewGroup13.Header = "Extra";
            listViewGroup13.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup13.Name = "listViewGroup4";
            listViewGroup14.Header = "Additional";
            listViewGroup14.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup14.Name = "listViewGroup5";
            listViewGroup15.Header = "Misc";
            listViewGroup15.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup15.Name = "listViewGroup6";
            this.lstOld.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup13,
            listViewGroup14,
            listViewGroup15});
            this.lstOld.Location = new System.Drawing.Point(0, 0);
            this.lstOld.Name = "lstOld";
            this.lstOld.ShowItemToolTips = true;
            this.lstOld.Size = new System.Drawing.Size(870, 258);
            this.lstOld.SmallImageList = this.imageList1;
            this.lstOld.TabIndex = 0;
            this.lstOld.UseCompatibleStateImageBehavior = false;
            this.lstOld.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Name";
            this.columnHeader6.Width = 169;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Size";
            this.columnHeader7.Width = 81;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Type";
            this.columnHeader8.Width = 61;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Date (YYYY/MM/DD)";
            this.columnHeader9.Width = 119;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Folder";
            this.columnHeader10.Width = 162;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Downloaded (YYYY/MM/DD)";
            this.columnHeader16.Width = 161;
            // 
            // tabOld
            // 
            this.tabOld.Controls.Add(this.lstDel);
            this.tabOld.ImageIndex = 2;
            this.tabOld.Location = new System.Drawing.Point(4, 39);
            this.tabOld.Name = "tabOld";
            this.tabOld.Size = new System.Drawing.Size(870, 258);
            this.tabOld.TabIndex = 2;
            this.tabOld.Text = "Old / Superseded";
            this.tabOld.UseVisualStyleBackColor = true;
            // 
            // lstDel
            // 
            this.lstDel.CheckBoxes = true;
            this.lstDel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.lstDel.ContextMenuStrip = this.contextMenuStrip1;
            this.lstDel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDel.FullRowSelect = true;
            this.lstDel.GridLines = true;
            this.lstDel.Location = new System.Drawing.Point(0, 0);
            this.lstDel.Name = "lstDel";
            this.lstDel.ShowGroups = false;
            this.lstDel.ShowItemToolTips = true;
            this.lstDel.Size = new System.Drawing.Size(870, 258);
            this.lstDel.SmallImageList = this.imageList1;
            this.lstDel.TabIndex = 0;
            this.lstDel.UseCompatibleStateImageBehavior = false;
            this.lstDel.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Name";
            this.columnHeader11.Width = 169;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Size";
            this.columnHeader12.Width = 81;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Type";
            this.columnHeader13.Width = 61;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Date (YYYY/MM/DD)";
            this.columnHeader14.Width = 135;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Location [From All Categories]";
            this.columnHeader15.Width = 289;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "new_sticker.png");
            this.imageList2.Images.SetKeyName(1, "button_ok_32x32.png");
            this.imageList2.Images.SetKeyName(2, "trash-icon.png");
            // 
            // tsMain
            // 
            this.tsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsMain.GripMargin = new System.Windows.Forms.Padding(0);
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDownload,
            this.cmdScanOld,
            this.cmdDelOld,
            this.toolStripSeparator1,
            this.pbProgress,
            this.lblStatus});
            this.tsMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(0);
            this.tsMain.Size = new System.Drawing.Size(878, 35);
            this.tsMain.TabIndex = 16;
            this.tsMain.TabStop = true;
            this.tsMain.Text = "ToolStrip1";
            // 
            // mnuDownload
            // 
            this.mnuDownload.Image = ((System.Drawing.Image)(resources.GetObject("mnuDownload.Image")));
            this.mnuDownload.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnuDownload.Name = "mnuDownload";
            this.mnuDownload.Size = new System.Drawing.Size(98, 35);
            this.mnuDownload.Text = "Download";
            this.mnuDownload.ToolTipText = "Download selected files";
            this.mnuDownload.Visible = false;
            this.mnuDownload.Click += new System.EventHandler(this.mnuDownload_Click);
            // 
            // cmdScanOld
            // 
            this.cmdScanOld.Image = ((System.Drawing.Image)(resources.GetObject("cmdScanOld.Image")));
            this.cmdScanOld.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdScanOld.Name = "cmdScanOld";
            this.cmdScanOld.Size = new System.Drawing.Size(163, 35);
            this.cmdScanOld.Text = "Check for Old Updates";
            this.cmdScanOld.ToolTipText = "Check for old/superseded updates.";
            this.cmdScanOld.Visible = false;
            this.cmdScanOld.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // cmdDelOld
            // 
            this.cmdDelOld.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelOld.Image")));
            this.cmdDelOld.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdDelOld.Name = "cmdDelOld";
            this.cmdDelOld.Size = new System.Drawing.Size(99, 35);
            this.cmdDelOld.Text = "Delete Old";
            this.cmdDelOld.ToolTipText = "Delete all old and superseded updates";
            this.cmdDelOld.Visible = false;
            this.cmdDelOld.Click += new System.EventHandler(this.cmdDelOld_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // pbProgress
            // 
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(100, 32);
            this.pbProgress.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 32);
            // 
            // BWDownload
            // 
            this.BWDownload.WorkerSupportsCancellation = true;
            this.BWDownload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWDownload_DoWork);
            // 
            // frmUpdCatalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(880, 393);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(750, 400);
            this.Name = "frmUpdCatalog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alphawaves\' Downloader";
            this.Load += new System.EventHandler(this.frmSoLoR_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.Panel2.PerformLayout();
            this.scMain.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabNew.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabCurrent.ResumeLayout(false);
            this.tabOld.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);

		  }

		  #endregion

		  private System.Windows.Forms.SplitContainer splitContainer1;
		  private System.Windows.Forms.ComboBox cboSU;
		  private System.Windows.Forms.Label label1;
		  private System.Windows.Forms.SplitContainer scMain;
		  private WinToolkit.ListViewEx lstNew;
		  private System.Windows.Forms.ColumnHeader columnHeader1;
		  private System.Windows.Forms.ColumnHeader columnHeader2;
		  private System.Windows.Forms.ColumnHeader columnHeader3;
		  private System.Windows.Forms.ColumnHeader columnHeader4;
		  internal System.Windows.Forms.ToolStrip tsMain;
		  internal System.Windows.Forms.ToolStripMenuItem mnuDownload;
		  private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		  private System.Windows.Forms.ToolStripMenuItem cmsSelectAll;
		  private System.Windows.Forms.ToolStripLabel lblStatus;
		  private System.ComponentModel.BackgroundWorker BWDownload;
		  private System.Windows.Forms.Button cmdBrowse;
		  private System.Windows.Forms.TextBox txtDownload;
		  private System.Windows.Forms.Label label2;
		  private System.Windows.Forms.ToolStripProgressBar pbProgress;
		  private System.Windows.Forms.ColumnHeader columnHeader5;
		  private System.Windows.Forms.ToolStripMenuItem cmsUSelectAll;
		  private System.Windows.Forms.TabControl tabMain;
		  private System.Windows.Forms.TabPage tabNew;
		  private System.Windows.Forms.TabPage tabCurrent;
		  private ListViewEx lstOld;
		  private System.Windows.Forms.ColumnHeader columnHeader6;
		  private System.Windows.Forms.ColumnHeader columnHeader7;
		  private System.Windows.Forms.ColumnHeader columnHeader8;
		  private System.Windows.Forms.ColumnHeader columnHeader9;
		  private System.Windows.Forms.ColumnHeader columnHeader10;
		  internal System.Windows.Forms.ToolStripMenuItem cmdScanOld;
		  private System.Windows.Forms.ImageList imageList1;
		  private System.Windows.Forms.ImageList imageList2;
		  private System.Windows.Forms.Button cmdRefresh;
		  private System.Windows.Forms.TabPage tabOld;
		  private ListViewEx lstDel;
		  private System.Windows.Forms.ColumnHeader columnHeader11;
		  private System.Windows.Forms.ColumnHeader columnHeader12;
		  private System.Windows.Forms.ColumnHeader columnHeader13;
		  private System.Windows.Forms.ColumnHeader columnHeader14;
		  private System.Windows.Forms.ColumnHeader columnHeader15;
		  internal System.Windows.Forms.ToolStripMenuItem cmdDelOld;
		  private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		  private System.Windows.Forms.CheckBox chkScan;
		  private System.Windows.Forms.ColumnHeader columnHeader16;
		  private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		  private System.Windows.Forms.ToolStripMenuItem cmdACheckAll;
		  private System.Windows.Forms.ToolStripMenuItem cmdAUnCheckAll;
	 }
}