namespace WinToolkit
{
    partial class frmUpdRetriever
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdRetriever));
            this.lstCR = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdCA = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdDS = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdDA = new System.Windows.Forms.ToolStripMenuItem();
            this.scCAB = new System.Windows.Forms.SplitContainer();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdCRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdCS = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowSource = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowDest = new System.Windows.Forms.ToolStripMenuItem();
            this.PB = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.BWCopy = new System.ComponentModel.BackgroundWorker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCAB = new System.Windows.Forms.TabPage();
            this.tabMSU = new System.Windows.Forms.TabPage();
            this.scWU = new System.Windows.Forms.SplitContainer();
            this.lstWU = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.cmdWRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdWU_OL = new System.Windows.Forms.ToolStripMenuItem();
            this.PB2 = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus2 = new System.Windows.Forms.ToolStripLabel();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.BWDownload = new System.ComponentModel.BackgroundWorker();
            this.scCAB.Panel1.SuspendLayout();
            this.scCAB.Panel2.SuspendLayout();
            this.scCAB.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabCAB.SuspendLayout();
            this.tabMSU.SuspendLayout();
            this.scWU.Panel1.SuspendLayout();
            this.scWU.Panel2.SuspendLayout();
            this.scWU.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // lstCR
            // 
            this.lstCR.BackColor = System.Drawing.SystemColors.Window;
            this.lstCR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstCR.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader3,
            this.ColumnHeader2});
            this.lstCR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCR.FullRowSelect = true;
            this.lstCR.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstCR.Location = new System.Drawing.Point(0, 0);
            this.lstCR.Name = "lstCR";
            this.lstCR.Size = new System.Drawing.Size(676, 298);
            this.lstCR.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstCR.TabIndex = 0;
            this.lstCR.UseCompatibleStateImageBehavior = false;
            this.lstCR.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "CAB Name";
            this.ColumnHeader1.Width = 242;
            // 
            // ColumnHeader3
            // 
            this.ColumnHeader3.Text = "Size";
            this.ColumnHeader3.Width = 136;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Location";
            this.ColumnHeader2.Width = 204;
            // 
            // cmdCA
            // 
            this.cmdCA.Name = "cmdCA";
            this.cmdCA.Size = new System.Drawing.Size(149, 22);
            this.cmdCA.Text = "Copy All";
            this.cmdCA.ToolTipText = "Copy all files to the specified folder";
            this.cmdCA.Click += new System.EventHandler(this.cmdCA_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // cmdDS
            // 
            this.cmdDS.Name = "cmdDS";
            this.cmdDS.Size = new System.Drawing.Size(154, 22);
            this.cmdDS.Tag = "";
            this.cmdDS.Text = "Delete Selected";
            this.cmdDS.ToolTipText = "Delete all selected files from the list";
            this.cmdDS.Click += new System.EventHandler(this.cmdDS_Click);
            // 
            // cmdDA
            // 
            this.cmdDA.Name = "cmdDA";
            this.cmdDA.Size = new System.Drawing.Size(154, 22);
            this.cmdDA.Tag = "";
            this.cmdDA.Text = "Delete All";
            this.cmdDA.ToolTipText = "Delete all files from the list";
            this.cmdDA.Click += new System.EventHandler(this.cmdDA_Click);
            // 
            // scCAB
            // 
            this.scCAB.BackColor = System.Drawing.SystemColors.Window;
            this.scCAB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scCAB.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scCAB.IsSplitterFixed = true;
            this.scCAB.Location = new System.Drawing.Point(0, 0);
            this.scCAB.Margin = new System.Windows.Forms.Padding(0);
            this.scCAB.Name = "scCAB";
            this.scCAB.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scCAB.Panel1
            // 
            this.scCAB.Panel1.Controls.Add(this.lstCR);
            // 
            // scCAB.Panel2
            // 
            this.scCAB.Panel2.Controls.Add(this.ToolStrip1);
            this.scCAB.Panel2MinSize = 35;
            this.scCAB.Size = new System.Drawing.Size(676, 334);
            this.scCAB.SplitterDistance = 298;
            this.scCAB.SplitterWidth = 1;
            this.scCAB.TabIndex = 6;
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdCRefresh,
            this.ToolStripSeparator1,
            this.cmdCopy,
            this.cmdDelete,
            this.toolStripSeparator2,
            this.mnuShowSource,
            this.mnuShowDest,
            this.PB,
            this.lblStatus});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.Size = new System.Drawing.Size(676, 35);
            this.ToolStrip1.Stretch = true;
            this.ToolStrip1.TabIndex = 3;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // cmdCRefresh
            // 
            this.cmdCRefresh.Enabled = false;
            this.cmdCRefresh.Image = ((System.Drawing.Image)(resources.GetObject("cmdCRefresh.Image")));
            this.cmdCRefresh.Name = "cmdCRefresh";
            this.cmdCRefresh.Size = new System.Drawing.Size(44, 35);
            this.cmdCRefresh.ToolTipText = "Recheck for CAB files.";
            this.cmdCRefresh.Click += new System.EventHandler(this.cmdCRefresh_Click);
            // 
            // cmdCopy
            // 
            this.cmdCopy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdCA,
            this.cmdCS});
            this.cmdCopy.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopy.Image")));
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(79, 35);
            this.cmdCopy.Text = "Copy";
            this.cmdCopy.ToolTipText = "Select copy task";
            this.cmdCopy.MouseHover += new System.EventHandler(this.cmdCopy_MouseHover);
            // 
            // cmdCS
            // 
            this.cmdCS.Name = "cmdCS";
            this.cmdCS.Size = new System.Drawing.Size(149, 22);
            this.cmdCS.Text = "Copy Selected";
            this.cmdCS.ToolTipText = "Copy all selected files to the specified folder";
            this.cmdCS.Click += new System.EventHandler(this.cmdCS_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdDS,
            this.cmdDA});
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(84, 35);
            this.cmdDelete.Tag = "";
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.ToolTipText = "Select delete task";
            this.cmdDelete.MouseHover += new System.EventHandler(this.cmdDelete_MouseHover);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // mnuShowSource
            // 
            this.mnuShowSource.Image = ((System.Drawing.Image)(resources.GetObject("mnuShowSource.Image")));
            this.mnuShowSource.Name = "mnuShowSource";
            this.mnuShowSource.Size = new System.Drawing.Size(123, 35);
            this.mnuShowSource.Tag = "";
            this.mnuShowSource.Text = "Source Folder";
            this.mnuShowSource.ToolTipText = "Opens the folder where the files will be copied from.";
            this.mnuShowSource.Click += new System.EventHandler(this.mnuShowFolder_Click);
            // 
            // mnuShowDest
            // 
            this.mnuShowDest.Image = ((System.Drawing.Image)(resources.GetObject("mnuShowDest.Image")));
            this.mnuShowDest.Name = "mnuShowDest";
            this.mnuShowDest.Size = new System.Drawing.Size(147, 35);
            this.mnuShowDest.Tag = "";
            this.mnuShowDest.Text = "Destination Folder";
            this.mnuShowDest.ToolTipText = "Opens the folder where you copied the *.cab files too.";
            this.mnuShowDest.Visible = false;
            this.mnuShowDest.Click += new System.EventHandler(this.mnuShowDest_Click);
            // 
            // PB
            // 
            this.PB.Name = "PB";
            this.PB.Size = new System.Drawing.Size(100, 32);
            this.PB.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 32);
            // 
            // BWCopy
            // 
            this.BWCopy.WorkerSupportsCancellation = true;
            this.BWCopy.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWCopy_DoWork);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCAB);
            this.tabControl1.Controls.Add(this.tabMSU);
            this.tabControl1.Controls.Add(this.tabInfo);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(684, 361);
            this.tabControl1.TabIndex = 7;
            // 
            // tabCAB
            // 
            this.tabCAB.Controls.Add(this.scCAB);
            this.tabCAB.ImageKey = "winrar+png.png";
            this.tabCAB.Location = new System.Drawing.Point(4, 23);
            this.tabCAB.Name = "tabCAB";
            this.tabCAB.Size = new System.Drawing.Size(676, 334);
            this.tabCAB.TabIndex = 0;
            this.tabCAB.Text = "CAB Files";
            this.tabCAB.UseVisualStyleBackColor = true;
            // 
            // tabMSU
            // 
            this.tabMSU.Controls.Add(this.scWU);
            this.tabMSU.ImageKey = "windows-update-icon_20.png";
            this.tabMSU.Location = new System.Drawing.Point(4, 23);
            this.tabMSU.Name = "tabMSU";
            this.tabMSU.Size = new System.Drawing.Size(676, 334);
            this.tabMSU.TabIndex = 1;
            this.tabMSU.Text = "Windows Update";
            this.tabMSU.UseVisualStyleBackColor = true;
            // 
            // scWU
            // 
            this.scWU.BackColor = System.Drawing.SystemColors.Window;
            this.scWU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scWU.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scWU.IsSplitterFixed = true;
            this.scWU.Location = new System.Drawing.Point(0, 0);
            this.scWU.Margin = new System.Windows.Forms.Padding(0);
            this.scWU.Name = "scWU";
            this.scWU.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scWU.Panel1
            // 
            this.scWU.Panel1.Controls.Add(this.lstWU);
            // 
            // scWU.Panel2
            // 
            this.scWU.Panel2.Controls.Add(this.toolStrip2);
            this.scWU.Panel2MinSize = 35;
            this.scWU.Size = new System.Drawing.Size(676, 334);
            this.scWU.SplitterDistance = 298;
            this.scWU.SplitterWidth = 1;
            this.scWU.TabIndex = 7;
            // 
            // lstWU
            // 
            this.lstWU.BackColor = System.Drawing.SystemColors.Window;
            this.lstWU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstWU.CheckBoxes = true;
            this.lstWU.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader6});
            this.lstWU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstWU.FullRowSelect = true;
            this.lstWU.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstWU.Location = new System.Drawing.Point(0, 0);
            this.lstWU.Name = "lstWU";
            this.lstWU.Size = new System.Drawing.Size(676, 298);
            this.lstWU.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstWU.TabIndex = 0;
            this.lstWU.UseCompatibleStateImageBehavior = false;
            this.lstWU.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Update Name";
            this.columnHeader4.Width = 276;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Location";
            this.columnHeader6.Width = 374;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdWRefresh,
            this.toolStripSeparator4,
            this.cmdDownload,
            this.cmdWU_OL,
            this.PB2,
            this.lblStatus2});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip2.Size = new System.Drawing.Size(676, 35);
            this.toolStrip2.Stretch = true;
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // cmdWRefresh
            // 
            this.cmdWRefresh.Enabled = false;
            this.cmdWRefresh.Image = ((System.Drawing.Image)(resources.GetObject("cmdWRefresh.Image")));
            this.cmdWRefresh.Name = "cmdWRefresh";
            this.cmdWRefresh.Size = new System.Drawing.Size(44, 35);
            this.cmdWRefresh.ToolTipText = "Rescan WindowsUpdate.log";
            this.cmdWRefresh.Click += new System.EventHandler(this.cmdWRefresh_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 35);
            // 
            // cmdDownload
            // 
            this.cmdDownload.Image = ((System.Drawing.Image)(resources.GetObject("cmdDownload.Image")));
            this.cmdDownload.Name = "cmdDownload";
            this.cmdDownload.Size = new System.Drawing.Size(105, 35);
            this.cmdDownload.Text = "Download";
            this.cmdDownload.ToolTipText = "Starts downloading the files";
            this.cmdDownload.Click += new System.EventHandler(this.cmdDownload_Click);
            // 
            // cmdWU_OL
            // 
            this.cmdWU_OL.Image = ((System.Drawing.Image)(resources.GetObject("cmdWU_OL.Image")));
            this.cmdWU_OL.Name = "cmdWU_OL";
            this.cmdWU_OL.Size = new System.Drawing.Size(103, 35);
            this.cmdWU_OL.Tag = "";
            this.cmdWU_OL.Text = "Open Log";
            this.cmdWU_OL.ToolTipText = "Opens the Windows Update log";
            this.cmdWU_OL.Click += new System.EventHandler(this.cmdWU_OL_Click);
            // 
            // PB2
            // 
            this.PB2.Name = "PB2";
            this.PB2.Size = new System.Drawing.Size(100, 32);
            this.PB2.Visible = false;
            // 
            // lblStatus2
            // 
            this.lblStatus2.Name = "lblStatus2";
            this.lblStatus2.Size = new System.Drawing.Size(0, 32);
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.flowLayoutPanel1);
            this.tabInfo.ImageKey = "Information.png";
            this.tabInfo.Location = new System.Drawing.Point(4, 23);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Size = new System.Drawing.Size(676, 334);
            this.tabInfo.TabIndex = 2;
            this.tabInfo.Text = "Information";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(676, 334);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(665, 91);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CAB Files";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(76, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(583, 67);
            this.label2.TabIndex = 1;
            this.label2.Text = "When you download updates via \'Windows Update\', they get stored into a *.cab form" +
    "at. \r\n\r\nThis part of the tool lets you copy those cab files so you can integrate" +
    " them into your next build.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.pictureBox2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(665, 107);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Windows Update";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(76, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(583, 88);
            this.label3.TabIndex = 1;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(5, 25);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(64, 64);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.pictureBox3);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(3, 213);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(665, 91);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Hint";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(79, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(580, 67);
            this.label4.TabIndex = 1;
            this.label4.Text = "If you have the same KB file in both *.cab and *.msu, then you only need to integ" +
    "rate one of them. Preferably the cab file.";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(6, 19);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(64, 64);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "winrar+png.png");
            this.imageList1.Images.SetKeyName(1, "windows-update-icon_20.png");
            this.imageList1.Images.SetKeyName(2, "Information.png");
            // 
            // BWDownload
            // 
            this.BWDownload.WorkerSupportsCancellation = true;
            this.BWDownload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWDownload_DoWork);
            this.BWDownload.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BWDownload_RunWorkerCompleted);
            // 
            // frmUpdRetriever
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "frmUpdRetriever";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Retriever";
            this.Load += new System.EventHandler(this.frmCabRetriever_Load);
            this.scCAB.Panel1.ResumeLayout(false);
            this.scCAB.Panel2.ResumeLayout(false);
            this.scCAB.Panel2.PerformLayout();
            this.scCAB.ResumeLayout(false);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabCAB.ResumeLayout(false);
            this.tabMSU.ResumeLayout(false);
            this.scWU.Panel1.ResumeLayout(false);
            this.scWU.Panel2.ResumeLayout(false);
            this.scWU.Panel2.PerformLayout();
            this.scWU.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabInfo.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView lstCR;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.ColumnHeader ColumnHeader3;
        internal System.Windows.Forms.ColumnHeader ColumnHeader2;
        internal System.Windows.Forms.ToolStripMenuItem cmdCA;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem cmdDS;
        internal System.Windows.Forms.ToolStripMenuItem cmdDA;
        internal System.Windows.Forms.SplitContainer scCAB;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem cmdCS;
        internal System.ComponentModel.BackgroundWorker BWCopy;
        private System.Windows.Forms.ToolStripProgressBar PB;
        private System.Windows.Forms.ToolStripLabel lblStatus;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripMenuItem mnuShowSource;
        internal System.Windows.Forms.ToolStripMenuItem mnuShowDest;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCAB;
        private System.Windows.Forms.TabPage tabMSU;
        internal System.Windows.Forms.SplitContainer scWU;
        internal System.Windows.Forms.ListView lstWU;
        internal System.Windows.Forms.ColumnHeader columnHeader4;
        internal System.Windows.Forms.ColumnHeader columnHeader6;
        internal System.Windows.Forms.ToolStrip toolStrip2;
        internal System.Windows.Forms.ToolStripMenuItem cmdDownload;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        internal System.Windows.Forms.ToolStripMenuItem cmdWU_OL;
        private System.Windows.Forms.ToolStripProgressBar PB2;
        private System.Windows.Forms.ToolStripLabel lblStatus2;
        private System.ComponentModel.BackgroundWorker BWDownload;
        internal System.Windows.Forms.ToolStripMenuItem cmdCopy;
        internal System.Windows.Forms.ToolStripMenuItem cmdDelete;
        internal System.Windows.Forms.ToolStripMenuItem cmdCRefresh;
        internal System.Windows.Forms.ToolStripMenuItem cmdWRefresh;
        private System.Windows.Forms.TabPage tabInfo;
        private int sCopy = 0;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}