namespace WinToolkit
{
    partial class frmWIMManager
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Already Mounted Images", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Operating Systems", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Custom", System.Windows.Forms.HorizontalAlignment.Center);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWIMManager));
            this.cmdU = new System.Windows.Forms.ToolStripMenuItem();
            this.lstImages = new System.Windows.Forms.ListView();
            this.ColumnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdRebuildImg = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdMergeImage = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdMountImage = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdApplyUN = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdExportImg = new System.Windows.Forms.ToolStripMenuItem();
            this.BWRebuild = new System.ComponentModel.BackgroundWorker();
            this.BWR = new System.ComponentModel.BackgroundWorker();
            this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.mnuTools = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuAIO = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuComp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRHM = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTM = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBrowse = new System.Windows.Forms.ToolStripSplitButton();
            this.cmdBrowseDVD = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdBrowseISO = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdBrowseWIM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdSBoot = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDVD1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDVD2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDVD3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDVD4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDVD5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblWIM = new System.Windows.Forms.ToolStripLabel();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdS = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSelectSep = new System.Windows.Forms.ToolStripSeparator();
            this.cmdSelectAll86 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSelectAll64 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdOpenMount = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdCleanMGR = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdDeleteImage = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdEditName = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdEditDesc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditDName = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditDDesc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdEditFlag = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImage = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdImportImg = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdUpgradeImage = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdMakeISO = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdMergeSWM = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdEI = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdSetProductKey = new System.Windows.Forms.ToolStripMenuItem();
            this.BWMerge = new System.ComponentModel.BackgroundWorker();
            this.bwUpgrade = new System.ComponentModel.BackgroundWorker();
            this.SplitContainer2.Panel1.SuspendLayout();
            this.SplitContainer2.Panel2.SuspendLayout();
            this.SplitContainer2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdU
            // 
            this.cmdU.Name = "cmdU";
            this.cmdU.Size = new System.Drawing.Size(70, 40);
            this.cmdU.Tag = "Mount";
            this.cmdU.Text = "Unmount";
            this.cmdU.ToolTipText = "Unmount the image saving all changes";
            this.cmdU.Visible = false;
            this.cmdU.Click += new System.EventHandler(this.cmdU_Click);
            // 
            // lstImages
            // 
            this.lstImages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstImages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader5,
            this.ColumnHeader6,
            this.ColumnHeader1,
            this.ColumnHeader7,
            this.ColumnHeader8});
            this.lstImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstImages.FullRowSelect = true;
            listViewGroup1.Header = "Already Mounted Images";
            listViewGroup1.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup1.Name = "LVGA";
            listViewGroup2.Header = "Operating Systems";
            listViewGroup2.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup2.Name = "Windows OS";
            listViewGroup3.Header = "Custom";
            listViewGroup3.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup3.Name = "Custom";
            this.lstImages.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.lstImages.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstImages.HideSelection = false;
            this.lstImages.Location = new System.Drawing.Point(0, 0);
            this.lstImages.Margin = new System.Windows.Forms.Padding(0);
            this.lstImages.Name = "lstImages";
            this.lstImages.ShowItemToolTips = true;
            this.lstImages.Size = new System.Drawing.Size(774, 216);
            this.lstImages.TabIndex = 0;
            this.lstImages.UseCompatibleStateImageBehavior = false;
            this.lstImages.View = System.Windows.Forms.View.Details;
            this.lstImages.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstImages_ItemSelectionChanged);
            this.lstImages.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstImages_MouseDoubleClick);
            // 
            // ColumnHeader5
            // 
            this.ColumnHeader5.Text = "Image Name";
            this.ColumnHeader5.Width = 289;
            // 
            // ColumnHeader6
            // 
            this.ColumnHeader6.Text = "Description";
            this.ColumnHeader6.Width = 304;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Arc";
            this.ColumnHeader1.Width = 39;
            // 
            // ColumnHeader7
            // 
            this.ColumnHeader7.Text = "Build";
            this.ColumnHeader7.Width = 51;
            // 
            // ColumnHeader8
            // 
            this.ColumnHeader8.Text = "Size";
            this.ColumnHeader8.Width = 70;
            // 
            // cmdRebuildImg
            // 
            this.cmdRebuildImg.Name = "cmdRebuildImg";
            this.cmdRebuildImg.Size = new System.Drawing.Size(59, 40);
            this.cmdRebuildImg.Tag = "Refresh";
            this.cmdRebuildImg.Text = "Rebuild";
            this.cmdRebuildImg.ToolTipText = "Rebuild the WIM with MAX compression to save space!";
            this.cmdRebuildImg.Visible = false;
            this.cmdRebuildImg.Click += new System.EventHandler(this.cmdRebuildImg_Click);
            // 
            // cmdMergeImage
            // 
            this.cmdMergeImage.Image = ((System.Drawing.Image)(resources.GetObject("cmdMergeImage.Image")));
            this.cmdMergeImage.Name = "cmdMergeImage";
            this.cmdMergeImage.Size = new System.Drawing.Size(155, 22);
            this.cmdMergeImage.Text = "Merge Image";
            this.cmdMergeImage.ToolTipText = "Merge two WIM files together.";
            this.cmdMergeImage.Click += new System.EventHandler(this.cmdMI_Click);
            // 
            // cmdMountImage
            // 
            this.cmdMountImage.Name = "cmdMountImage";
            this.cmdMountImage.Size = new System.Drawing.Size(55, 40);
            this.cmdMountImage.Tag = "Mount";
            this.cmdMountImage.Text = "Mount";
            this.cmdMountImage.ToolTipText = "Mount the selected image.";
            this.cmdMountImage.Visible = false;
            this.cmdMountImage.Click += new System.EventHandler(this.cmdMountImage_Click);
            // 
            // cmdApplyUN
            // 
            this.cmdApplyUN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdApplyUN.Image = ((System.Drawing.Image)(resources.GetObject("cmdApplyUN.Image")));
            this.cmdApplyUN.Name = "cmdApplyUN";
            this.cmdApplyUN.Size = new System.Drawing.Size(131, 40);
            this.cmdApplyUN.Text = "Apply Unattended";
            this.cmdApplyUN.ToolTipText = "Copies the selected unattended file to the DVD root.";
            this.cmdApplyUN.Visible = false;
            this.cmdApplyUN.Click += new System.EventHandler(this.cmdApplyUN_Click);
            // 
            // cmdExportImg
            // 
            this.cmdExportImg.Image = ((System.Drawing.Image)(resources.GetObject("cmdExportImg.Image")));
            this.cmdExportImg.Name = "cmdExportImg";
            this.cmdExportImg.Size = new System.Drawing.Size(155, 22);
            this.cmdExportImg.Text = "Export Image";
            this.cmdExportImg.ToolTipText = "Make a new WIM with the selected image!";
            this.cmdExportImg.Click += new System.EventHandler(this.cmdExportImg_Click);
            // 
            // BWRebuild
            // 
            this.BWRebuild.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWRebuild_DoWork);
            // 
            // BWR
            // 
            this.BWR.WorkerSupportsCancellation = true;
            this.BWR.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWR_DoWork);
            // 
            // SplitContainer2
            // 
            this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer2.IsSplitterFixed = true;
            this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer2.Name = "SplitContainer2";
            this.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer2.Panel1
            // 
            this.SplitContainer2.Panel1.Controls.Add(this.toolStrip2);
            // 
            // SplitContainer2.Panel2
            // 
            this.SplitContainer2.Panel2.Controls.Add(this.SplitContainer1);
            this.SplitContainer2.Size = new System.Drawing.Size(774, 311);
            this.SplitContainer2.SplitterDistance = 53;
            this.SplitContainer2.SplitterWidth = 1;
            this.SplitContainer2.TabIndex = 2;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTools,
            this.mnuBrowse,
            this.toolStripSeparator1,
            this.lblWIM});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip2.ShowItemToolTips = false;
            this.toolStrip2.Size = new System.Drawing.Size(774, 53);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            this.toolStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip2_ItemClicked);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAIO,
            this.mnuComp,
            this.mnuRHM,
            this.mnuWM,
            this.toolStripSeparator2,
            this.mnuTM});
            this.mnuTools.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuTools.Image = ((System.Drawing.Image)(resources.GetObject("mnuTools.Image")));
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(102, 50);
            this.mnuTools.Tag = "";
            this.mnuTools.Text = "Other Tools";
            this.mnuTools.ButtonClick += new System.EventHandler(this.mnuTools_ButtonClick);
            this.mnuTools.MouseHover += new System.EventHandler(this.mnuTools_ButtonClick);
            // 
            // mnuAIO
            // 
            this.mnuAIO.Image = ((System.Drawing.Image)(resources.GetObject("mnuAIO.Image")));
            this.mnuAIO.Name = "mnuAIO";
            this.mnuAIO.Size = new System.Drawing.Size(194, 22);
            this.mnuAIO.Text = "All-In-One Integrator";
            this.mnuAIO.ToolTipText = "Load the All-In-One Integrator.";
            this.mnuAIO.Click += new System.EventHandler(this.mnuAIO_Click);
            // 
            // mnuComp
            // 
            this.mnuComp.Image = ((System.Drawing.Image)(resources.GetObject("mnuComp.Image")));
            this.mnuComp.Name = "mnuComp";
            this.mnuComp.Size = new System.Drawing.Size(194, 22);
            this.mnuComp.Text = "Component Removal";
            this.mnuComp.ToolTipText = "Load the Component Removal tool.";
            this.mnuComp.Click += new System.EventHandler(this.mnuComp_Click);
            // 
            // mnuRHM
            // 
            this.mnuRHM.Image = ((System.Drawing.Image)(resources.GetObject("mnuRHM.Image")));
            this.mnuRHM.Name = "mnuRHM";
            this.mnuRHM.Size = new System.Drawing.Size(194, 22);
            this.mnuRHM.Text = "Registry Hive Mounter";
            this.mnuRHM.ToolTipText = "Load the Registry Hive Mounter tool.";
            this.mnuRHM.Click += new System.EventHandler(this.mnuRHM_Click);
            // 
            // mnuWM
            // 
            this.mnuWM.Image = ((System.Drawing.Image)(resources.GetObject("mnuWM.Image")));
            this.mnuWM.Name = "mnuWM";
            this.mnuWM.Size = new System.Drawing.Size(194, 22);
            this.mnuWM.Text = "WIM Manager";
            this.mnuWM.ToolTipText = "Load the WIM Manager.";
            this.mnuWM.Click += new System.EventHandler(this.mnuWM_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(191, 6);
            // 
            // mnuTM
            // 
            this.mnuTM.Image = ((System.Drawing.Image)(resources.GetObject("mnuTM.Image")));
            this.mnuTM.Name = "mnuTM";
            this.mnuTM.Size = new System.Drawing.Size(194, 22);
            this.mnuTM.Text = "Tools Manager";
            this.mnuTM.ToolTipText = "Return to the start screen.";
            this.mnuTM.Click += new System.EventHandler(this.mnuTM_Click);
            // 
            // mnuBrowse
            // 
            this.mnuBrowse.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdBrowseDVD,
            this.cmdBrowseISO,
            this.cmdBrowseWIM,
            this.toolStripSeparator3,
            this.cmdSBoot,
            this.toolStripSeparator5,
            this.mnuDVD1,
            this.mnuDVD2,
            this.mnuDVD3,
            this.mnuDVD4,
            this.mnuDVD5,
            this.toolStripSeparator4,
            this.cmdRefresh});
            this.mnuBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuBrowse.Image = global::WinToolkit.Properties.Resources.Search;
            this.mnuBrowse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuBrowse.Name = "mnuBrowse";
            this.mnuBrowse.Size = new System.Drawing.Size(80, 50);
            this.mnuBrowse.Text = "Browse";
            this.mnuBrowse.ButtonClick += new System.EventHandler(this.mnuBrowse_ButtonClick);
            this.mnuBrowse.MouseHover += new System.EventHandler(this.mnuBrowse_ButtonClick);
            // 
            // cmdBrowseDVD
            // 
            this.cmdBrowseDVD.Image = ((System.Drawing.Image)(resources.GetObject("cmdBrowseDVD.Image")));
            this.cmdBrowseDVD.Name = "cmdBrowseDVD";
            this.cmdBrowseDVD.Size = new System.Drawing.Size(255, 22);
            this.cmdBrowseDVD.Text = "Browse for DVD / Folder (Ctrl + O)";
            this.cmdBrowseDVD.ToolTipText = "When you extract an ISO, use this to browse to that folder and it will automatica" +
    "lly select the \'Sources\\\\install.wim\' file.";
            this.cmdBrowseDVD.Click += new System.EventHandler(this.cmdBrowseDVD_Click);
            // 
            // cmdBrowseISO
            // 
            this.cmdBrowseISO.Image = ((System.Drawing.Image)(resources.GetObject("cmdBrowseISO.Image")));
            this.cmdBrowseISO.Name = "cmdBrowseISO";
            this.cmdBrowseISO.Size = new System.Drawing.Size(255, 22);
            this.cmdBrowseISO.Text = "Browse for ISO (Ctrl + I)";
            this.cmdBrowseISO.ToolTipText = "Manually browse for the ISO to be extracted.";
            this.cmdBrowseISO.Click += new System.EventHandler(this.cmdBrowseISO_Click);
            // 
            // cmdBrowseWIM
            // 
            this.cmdBrowseWIM.Image = ((System.Drawing.Image)(resources.GetObject("cmdBrowseWIM.Image")));
            this.cmdBrowseWIM.Name = "cmdBrowseWIM";
            this.cmdBrowseWIM.Size = new System.Drawing.Size(255, 22);
            this.cmdBrowseWIM.Text = "Browse for WIM  (Ctrl + W)";
            this.cmdBrowseWIM.ToolTipText = "Manually browse for the *.wim file. This file is normally located within the extr" +
    "acted image (ISO) of the Windows 7 SP1 disk.";
            this.cmdBrowseWIM.Click += new System.EventHandler(this.cmdBrowseWIM_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(252, 6);
            // 
            // cmdSBoot
            // 
            this.cmdSBoot.Enabled = false;
            this.cmdSBoot.Image = ((System.Drawing.Image)(resources.GetObject("cmdSBoot.Image")));
            this.cmdSBoot.Name = "cmdSBoot";
            this.cmdSBoot.Size = new System.Drawing.Size(255, 22);
            this.cmdSBoot.Text = "Switch to \'boot.wim\'";
            this.cmdSBoot.Click += new System.EventHandler(this.cmdSBoot_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(252, 6);
            // 
            // mnuDVD1
            // 
            this.mnuDVD1.Image = ((System.Drawing.Image)(resources.GetObject("mnuDVD1.Image")));
            this.mnuDVD1.Name = "mnuDVD1";
            this.mnuDVD1.Size = new System.Drawing.Size(255, 22);
            this.mnuDVD1.Text = "N/A";
            this.mnuDVD1.Visible = false;
            this.mnuDVD1.Click += new System.EventHandler(this.mnuDVD1_Click);
            // 
            // mnuDVD2
            // 
            this.mnuDVD2.Image = ((System.Drawing.Image)(resources.GetObject("mnuDVD2.Image")));
            this.mnuDVD2.Name = "mnuDVD2";
            this.mnuDVD2.Size = new System.Drawing.Size(255, 22);
            this.mnuDVD2.Text = "N/A";
            this.mnuDVD2.Visible = false;
            this.mnuDVD2.Click += new System.EventHandler(this.mnuDVD2_Click);
            // 
            // mnuDVD3
            // 
            this.mnuDVD3.Image = ((System.Drawing.Image)(resources.GetObject("mnuDVD3.Image")));
            this.mnuDVD3.Name = "mnuDVD3";
            this.mnuDVD3.Size = new System.Drawing.Size(255, 22);
            this.mnuDVD3.Text = "N/A";
            this.mnuDVD3.Visible = false;
            this.mnuDVD3.Click += new System.EventHandler(this.mnuDVD3_Click);
            // 
            // mnuDVD4
            // 
            this.mnuDVD4.Image = ((System.Drawing.Image)(resources.GetObject("mnuDVD4.Image")));
            this.mnuDVD4.Name = "mnuDVD4";
            this.mnuDVD4.Size = new System.Drawing.Size(255, 22);
            this.mnuDVD4.Text = "N/A";
            this.mnuDVD4.Visible = false;
            this.mnuDVD4.Click += new System.EventHandler(this.mnuDVD4_Click);
            // 
            // mnuDVD5
            // 
            this.mnuDVD5.Image = ((System.Drawing.Image)(resources.GetObject("mnuDVD5.Image")));
            this.mnuDVD5.Name = "mnuDVD5";
            this.mnuDVD5.Size = new System.Drawing.Size(255, 22);
            this.mnuDVD5.Text = "N/A";
            this.mnuDVD5.Visible = false;
            this.mnuDVD5.Click += new System.EventHandler(this.mnuDVD5_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(252, 6);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Enabled = false;
            this.cmdRefresh.Image = global::WinToolkit.Properties.Resources.refresh2_20x20;
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(255, 22);
            this.cmdRefresh.Text = "Refresh (F5)";
            this.cmdRefresh.ToolTipText = "Reload the current image. Useful for if you\'ve edited the image outside Win Toolk" +
    "it.";
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 53);
            // 
            // lblWIM
            // 
            this.lblWIM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblWIM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWIM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWIM.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.lblWIM.Margin = new System.Windows.Forms.Padding(0);
            this.lblWIM.Name = "lblWIM";
            this.lblWIM.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.lblWIM.Size = new System.Drawing.Size(150, 53);
            this.lblWIM.Text = "Please select a WIM File...";
            this.lblWIM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWIM.ToolTipText = "Please select a WIM File...";
            this.lblWIM.TextChanged += new System.EventHandler(this.lblWIM_TextChanged);
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
            this.SplitContainer1.Panel1.Controls.Add(this.lstImages);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.ToolStrip1);
            this.SplitContainer1.Panel2MinSize = 40;
            this.SplitContainer1.Size = new System.Drawing.Size(774, 257);
            this.SplitContainer1.SplitterDistance = 216;
            this.SplitContainer1.SplitterWidth = 1;
            this.SplitContainer1.TabIndex = 0;
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.AutoSize = false;
            this.ToolStrip1.CanOverflow = false;
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdS,
            this.cmdOpenMount,
            this.cmdApplyUN,
            this.cmdCleanMGR,
            this.cmdDeleteImage,
            this.cmdEdit,
            this.mnuImage,
            this.cmdMakeISO,
            this.cmdMergeSWM,
            this.cmdMountImage,
            this.cmdRebuildImg,
            this.cmdEI,
            this.cmdSetProductKey,
            this.cmdU});
            this.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.MaximumSize = new System.Drawing.Size(0, 100);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.Size = new System.Drawing.Size(774, 40);
            this.ToolStrip1.TabIndex = 3;
            this.ToolStrip1.TabStop = true;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // cmdS
            // 
            this.cmdS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdSelect,
            this.cmdSelectAll,
            this.cmdSelectSep,
            this.cmdSelectAll86,
            this.cmdSelectAll64});
            this.cmdS.Image = ((System.Drawing.Image)(resources.GetObject("cmdS.Image")));
            this.cmdS.Name = "cmdS";
            this.cmdS.Size = new System.Drawing.Size(75, 40);
            this.cmdS.Tag = "";
            this.cmdS.Text = "Select...";
            this.cmdS.ToolTipText = "Use the selected image";
            this.cmdS.Visible = false;
            this.cmdS.MouseEnter += new System.EventHandler(this.cmdS_MouseEnter);
            // 
            // cmdSelect
            // 
            this.cmdSelect.Image = ((System.Drawing.Image)(resources.GetObject("cmdSelect.Image")));
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(180, 22);
            this.cmdSelect.Tag = "OK";
            this.cmdSelect.Text = "Select";
            this.cmdSelect.ToolTipText = "Use the selected image";
            this.cmdSelect.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // cmdSelectAll
            // 
            this.cmdSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("cmdSelectAll.Image")));
            this.cmdSelectAll.Name = "cmdSelectAll";
            this.cmdSelectAll.Size = new System.Drawing.Size(180, 22);
            this.cmdSelectAll.Tag = "OK";
            this.cmdSelectAll.Text = "Select All";
            this.cmdSelectAll.ToolTipText = "Use all images";
            this.cmdSelectAll.Click += new System.EventHandler(this.cmdSelectAll_Click);
            // 
            // cmdSelectSep
            // 
            this.cmdSelectSep.Name = "cmdSelectSep";
            this.cmdSelectSep.Size = new System.Drawing.Size(177, 6);
            this.cmdSelectSep.Visible = false;
            // 
            // cmdSelectAll86
            // 
            this.cmdSelectAll86.Image = ((System.Drawing.Image)(resources.GetObject("cmdSelectAll86.Image")));
            this.cmdSelectAll86.Name = "cmdSelectAll86";
            this.cmdSelectAll86.Size = new System.Drawing.Size(180, 22);
            this.cmdSelectAll86.Tag = "OK";
            this.cmdSelectAll86.Text = "Select All (x86)";
            this.cmdSelectAll86.ToolTipText = "Use all images";
            this.cmdSelectAll86.Visible = false;
            this.cmdSelectAll86.Click += new System.EventHandler(this.cmdSelectAll86_Click);
            // 
            // cmdSelectAll64
            // 
            this.cmdSelectAll64.Image = ((System.Drawing.Image)(resources.GetObject("cmdSelectAll64.Image")));
            this.cmdSelectAll64.Name = "cmdSelectAll64";
            this.cmdSelectAll64.Size = new System.Drawing.Size(180, 22);
            this.cmdSelectAll64.Tag = "OK";
            this.cmdSelectAll64.Text = "Select All (x64)";
            this.cmdSelectAll64.ToolTipText = "Use all images";
            this.cmdSelectAll64.Visible = false;
            this.cmdSelectAll64.Click += new System.EventHandler(this.cmdSelectAll64_Click);
            // 
            // cmdOpenMount
            // 
            this.cmdOpenMount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOpenMount.Image = global::WinToolkit.Properties.Resources.folder_48;
            this.cmdOpenMount.Name = "cmdOpenMount";
            this.cmdOpenMount.Size = new System.Drawing.Size(103, 40);
            this.cmdOpenMount.Text = "Open Mount";
            this.cmdOpenMount.ToolTipText = "Opens the selected mount folder";
            this.cmdOpenMount.Visible = false;
            this.cmdOpenMount.Click += new System.EventHandler(this.cmdOpenMount_Click);
            // 
            // cmdCleanMGR
            // 
            this.cmdCleanMGR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCleanMGR.Image = ((System.Drawing.Image)(resources.GetObject("cmdCleanMGR.Image")));
            this.cmdCleanMGR.Name = "cmdCleanMGR";
            this.cmdCleanMGR.Size = new System.Drawing.Size(134, 40);
            this.cmdCleanMGR.Text = "Cleanup Manager";
            this.cmdCleanMGR.ToolTipText = "Tries to clean up the image a little bit.";
            this.cmdCleanMGR.Visible = false;
            this.cmdCleanMGR.Click += new System.EventHandler(this.cmdCleanMGR_Click);
            // 
            // cmdDeleteImage
            // 
            this.cmdDeleteImage.Image = ((System.Drawing.Image)(resources.GetObject("cmdDeleteImage.Image")));
            this.cmdDeleteImage.Name = "cmdDeleteImage";
            this.cmdDeleteImage.Size = new System.Drawing.Size(68, 40);
            this.cmdDeleteImage.Text = "Delete";
            this.cmdDeleteImage.ToolTipText = "Delete the selected image.";
            this.cmdDeleteImage.Visible = false;
            this.cmdDeleteImage.Click += new System.EventHandler(this.cmdDeleteImage_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdEditName,
            this.cmdEditDesc,
            this.toolStripSeparator7,
            this.mnuEditDName,
            this.mnuEditDDesc,
            this.toolStripSeparator6,
            this.cmdEditFlag});
            this.cmdEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdEdit.Image")));
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(64, 40);
            this.cmdEdit.Tag = "";
            this.cmdEdit.Text = "Edit...";
            this.cmdEdit.ToolTipText = "Edit image details...";
            this.cmdEdit.Visible = false;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // cmdEditName
            // 
            this.cmdEditName.Image = ((System.Drawing.Image)(resources.GetObject("cmdEditName.Image")));
            this.cmdEditName.Name = "cmdEditName";
            this.cmdEditName.Size = new System.Drawing.Size(198, 22);
            this.cmdEditName.Tag = "Edit";
            this.cmdEditName.Text = "Edit Name";
            this.cmdEditName.ToolTipText = "Edit the Name of the selected image";
            this.cmdEditName.Click += new System.EventHandler(this.cmdEditName_Click);
            // 
            // cmdEditDesc
            // 
            this.cmdEditDesc.Image = ((System.Drawing.Image)(resources.GetObject("cmdEditDesc.Image")));
            this.cmdEditDesc.Name = "cmdEditDesc";
            this.cmdEditDesc.Size = new System.Drawing.Size(198, 22);
            this.cmdEditDesc.Tag = "Edit";
            this.cmdEditDesc.Text = "Edit Desc";
            this.cmdEditDesc.ToolTipText = "Edit the Description of the selected image.";
            this.cmdEditDesc.Click += new System.EventHandler(this.cmdEditDesc_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(195, 6);
            // 
            // mnuEditDName
            // 
            this.mnuEditDName.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditDName.Image")));
            this.mnuEditDName.Name = "mnuEditDName";
            this.mnuEditDName.Size = new System.Drawing.Size(198, 22);
            this.mnuEditDName.Tag = "Edit";
            this.mnuEditDName.Text = "Edit Display Name";
            this.mnuEditDName.ToolTipText = "Edit the display name of the image. This is the name you will see when you\'re ins" +
    "talling Windows and need to select an image.";
            this.mnuEditDName.Click += new System.EventHandler(this.mnuEditDName_Click);
            // 
            // mnuEditDDesc
            // 
            this.mnuEditDDesc.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditDDesc.Image")));
            this.mnuEditDDesc.Name = "mnuEditDDesc";
            this.mnuEditDDesc.Size = new System.Drawing.Size(198, 22);
            this.mnuEditDDesc.Tag = "Edit";
            this.mnuEditDDesc.Text = "Edit Display Description";
            this.mnuEditDDesc.ToolTipText = "Edit the display description of the image. This is the name you will see when you" +
    "\'re installing Windows and need to select an image.";
            this.mnuEditDDesc.Click += new System.EventHandler(this.mnuEditDDesc_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(195, 6);
            // 
            // cmdEditFlag
            // 
            this.cmdEditFlag.Image = ((System.Drawing.Image)(resources.GetObject("cmdEditFlag.Image")));
            this.cmdEditFlag.Name = "cmdEditFlag";
            this.cmdEditFlag.Size = new System.Drawing.Size(198, 22);
            this.cmdEditFlag.Text = "Edit Flag";
            this.cmdEditFlag.Click += new System.EventHandler(this.cmdEditFlag_Click);
            // 
            // mnuImage
            // 
            this.mnuImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdImportImg,
            this.cmdExportImg,
            this.cmdUpgradeImage,
            this.cmdMergeImage});
            this.mnuImage.Image = ((System.Drawing.Image)(resources.GetObject("mnuImage.Image")));
            this.mnuImage.Name = "mnuImage";
            this.mnuImage.Size = new System.Drawing.Size(77, 40);
            this.mnuImage.Tag = "";
            this.mnuImage.Text = "Image...";
            this.mnuImage.Visible = false;
            // 
            // cmdImportImg
            // 
            this.cmdImportImg.Image = ((System.Drawing.Image)(resources.GetObject("cmdImportImg.Image")));
            this.cmdImportImg.Name = "cmdImportImg";
            this.cmdImportImg.Size = new System.Drawing.Size(155, 22);
            this.cmdImportImg.Text = "Capture Image";
            this.cmdImportImg.ToolTipText = "Add a new image to the selected WIM file.";
            this.cmdImportImg.Click += new System.EventHandler(this.cmdImportImg_Click);
            // 
            // cmdUpgradeImage
            // 
            this.cmdUpgradeImage.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpgradeImage.Image")));
            this.cmdUpgradeImage.Name = "cmdUpgradeImage";
            this.cmdUpgradeImage.Size = new System.Drawing.Size(155, 22);
            this.cmdUpgradeImage.Text = "Upgrade Image";
            this.cmdUpgradeImage.ToolTipText = "This will upgrade the image to a different SKU version.";
            this.cmdUpgradeImage.Click += new System.EventHandler(this.cmdUpgradeImage_Click);
            // 
            // cmdMakeISO
            // 
            this.cmdMakeISO.Image = ((System.Drawing.Image)(resources.GetObject("cmdMakeISO.Image")));
            this.cmdMakeISO.Name = "cmdMakeISO";
            this.cmdMakeISO.Size = new System.Drawing.Size(85, 40);
            this.cmdMakeISO.Text = "Make ISO";
            this.cmdMakeISO.ToolTipText = "Make a new ISO with the selected WIM";
            this.cmdMakeISO.Visible = false;
            this.cmdMakeISO.Click += new System.EventHandler(this.cmdMakeISO_Click);
            // 
            // cmdMergeSWM
            // 
            this.cmdMergeSWM.Image = ((System.Drawing.Image)(resources.GetObject("cmdMergeSWM.Image")));
            this.cmdMergeSWM.Name = "cmdMergeSWM";
            this.cmdMergeSWM.Size = new System.Drawing.Size(100, 40);
            this.cmdMergeSWM.Text = "Merge SWM";
            this.cmdMergeSWM.ToolTipText = "Merge SWM files together";
            this.cmdMergeSWM.Visible = false;
            this.cmdMergeSWM.Click += new System.EventHandler(this.cmdMergeSWM_Click);
            // 
            // cmdEI
            // 
            this.cmdEI.Image = ((System.Drawing.Image)(resources.GetObject("cmdEI.Image")));
            this.cmdEI.Name = "cmdEI";
            this.cmdEI.Size = new System.Drawing.Size(116, 40);
            this.cmdEI.Text = "Remove \'ei.cfg\'";
            this.cmdEI.ToolTipText = "Unlocks this image so you can install all editions.";
            this.cmdEI.Visible = false;
            this.cmdEI.Click += new System.EventHandler(this.cmdEI_Click);
            // 
            // cmdSetProductKey
            // 
            this.cmdSetProductKey.Image = ((System.Drawing.Image)(resources.GetObject("cmdSetProductKey.Image")));
            this.cmdSetProductKey.Name = "cmdSetProductKey";
            this.cmdSetProductKey.Size = new System.Drawing.Size(118, 40);
            this.cmdSetProductKey.Text = "Set Product Key";
            this.cmdSetProductKey.ToolTipText = "Sets the product key for the selected image.";
            this.cmdSetProductKey.Visible = false;
            this.cmdSetProductKey.Click += new System.EventHandler(this.cmdSetProductKey_Click);
            // 
            // BWMerge
            // 
            this.BWMerge.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWMerge_DoWork);
            // 
            // bwUpgrade
            // 
            this.bwUpgrade.WorkerSupportsCancellation = true;
            this.bwUpgrade.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwUpgrade_DoWork);
            this.bwUpgrade.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwUpgrade_RunWorkerCompleted);
            // 
            // frmWIMManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 311);
            this.Controls.Add(this.SplitContainer2);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(790, 350);
            this.Name = "frmWIMManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WIM Manager";
            this.Load += new System.EventHandler(this.frmWIMManager_Load);
            this.SplitContainer2.Panel1.ResumeLayout(false);
            this.SplitContainer2.Panel1.PerformLayout();
            this.SplitContainer2.Panel2.ResumeLayout(false);
            this.SplitContainer2.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.ResumeLayout(false);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ToolStripMenuItem cmdU;
        internal System.Windows.Forms.ListView lstImages;
        internal System.Windows.Forms.ColumnHeader ColumnHeader5;
        internal System.Windows.Forms.ColumnHeader ColumnHeader6;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.ColumnHeader ColumnHeader7;
        internal System.Windows.Forms.ColumnHeader ColumnHeader8;
        internal System.Windows.Forms.ToolStripMenuItem cmdRebuildImg;
        internal System.Windows.Forms.ToolStripMenuItem cmdMergeImage;
        internal System.Windows.Forms.ToolStripMenuItem cmdMountImage;
        internal System.Windows.Forms.ToolStripMenuItem cmdApplyUN;
        internal System.Windows.Forms.ToolStripMenuItem cmdExportImg;
        internal System.ComponentModel.BackgroundWorker BWRebuild;
        internal System.ComponentModel.BackgroundWorker BWR;
        internal System.Windows.Forms.SplitContainer SplitContainer2;
        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem cmdS;
        internal System.Windows.Forms.ToolStripMenuItem cmdSelect;
        internal System.Windows.Forms.ToolStripMenuItem cmdSelectAll;
        internal System.Windows.Forms.ToolStripMenuItem cmdDeleteImage;
        internal System.Windows.Forms.ToolStripMenuItem cmdEdit;
        internal System.Windows.Forms.ToolStripMenuItem cmdEditName;
        internal System.Windows.Forms.ToolStripMenuItem cmdEditDesc;
        internal System.Windows.Forms.ToolStripMenuItem cmdMakeISO;
        internal System.Windows.Forms.ToolStripMenuItem cmdImportImg;
        internal System.ComponentModel.BackgroundWorker BWMerge;
        internal System.Windows.Forms.ToolStrip toolStrip2;
        internal System.Windows.Forms.ToolStripLabel lblWIM;
        internal System.Windows.Forms.ToolStripMenuItem cmdEI;
        internal System.Windows.Forms.ToolStripMenuItem cmdMergeSWM;
        internal System.Windows.Forms.ToolStripMenuItem cmdSetProductKey;
        internal System.Windows.Forms.ToolStripSplitButton mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuAIO;
        private System.Windows.Forms.ToolStripMenuItem mnuComp;
        private System.Windows.Forms.ToolStripMenuItem mnuRHM;
        private System.Windows.Forms.ToolStripMenuItem mnuWM;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuTM;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSplitButton mnuBrowse;
        private System.Windows.Forms.ToolStripMenuItem cmdBrowseDVD;
        private System.Windows.Forms.ToolStripMenuItem cmdBrowseWIM;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cmdRefresh;
        private System.Windows.Forms.ToolStripMenuItem cmdSBoot;
        internal System.Windows.Forms.ToolStripMenuItem cmdOpenMount;
        private System.Windows.Forms.ToolStripMenuItem mnuDVD1;
        private System.Windows.Forms.ToolStripMenuItem mnuDVD2;
        private System.Windows.Forms.ToolStripMenuItem mnuDVD3;
        private System.Windows.Forms.ToolStripMenuItem mnuDVD4;
        private System.Windows.Forms.ToolStripMenuItem mnuDVD5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator cmdSelectSep;
        internal System.Windows.Forms.ToolStripMenuItem cmdSelectAll86;
        internal System.Windows.Forms.ToolStripMenuItem cmdSelectAll64;
        internal System.Windows.Forms.ToolStripMenuItem mnuEditDName;
        internal System.Windows.Forms.ToolStripMenuItem mnuEditDDesc;
        private System.Windows.Forms.ToolStripMenuItem cmdBrowseISO;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem cmdEditFlag;
        internal System.Windows.Forms.ToolStripMenuItem cmdUpgradeImage;
        internal System.ComponentModel.BackgroundWorker bwUpgrade;
        internal System.Windows.Forms.ToolStripMenuItem mnuImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        internal System.Windows.Forms.ToolStripMenuItem cmdCleanMGR;

    }
}