namespace WinToolkit
{
    partial class frmAIODiskCreator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAIODiskCreator));
            this.lstImages = new System.Windows.Forms.ListView();
            this.ColumnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdMI = new System.Windows.Forms.ToolStripMenuItem();
            this.BWR = new System.ComponentModel.BackgroundWorker();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.lblWIM = new System.Windows.Forms.ToolStripLabel();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdEditName = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdEditDesc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditDName = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditDDesc = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdDeleteImage = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdMakeUSB = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdMakeISO = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdClear = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdEI = new System.Windows.Forms.ToolStripButton();
            this.cmdRecovery64 = new System.Windows.Forms.ToolStripButton();
            this.BWMerge = new System.ComponentModel.BackgroundWorker();
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
            this.lstImages.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstImages.HideSelection = false;
            this.lstImages.Location = new System.Drawing.Point(0, 0);
            this.lstImages.Margin = new System.Windows.Forms.Padding(0);
            this.lstImages.MultiSelect = false;
            this.lstImages.Name = "lstImages";
            this.lstImages.ShowItemToolTips = true;
            this.lstImages.Size = new System.Drawing.Size(744, 328);
            this.lstImages.TabIndex = 0;
            this.lstImages.UseCompatibleStateImageBehavior = false;
            this.lstImages.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader5
            // 
            this.ColumnHeader5.Text = "Image Name";
            this.ColumnHeader5.Width = 225;
            // 
            // ColumnHeader6
            // 
            this.ColumnHeader6.Text = "Description";
            this.ColumnHeader6.Width = 236;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Arc";
            // 
            // ColumnHeader7
            // 
            this.ColumnHeader7.Text = "Build";
            this.ColumnHeader7.Width = 91;
            // 
            // ColumnHeader8
            // 
            this.ColumnHeader8.Text = "Size";
            this.ColumnHeader8.Width = 104;
            // 
            // cmdMI
            // 
            this.cmdMI.Image = global::WinToolkit.Properties.Resources.OK;
            this.cmdMI.Name = "cmdMI";
            this.cmdMI.Size = new System.Drawing.Size(66, 40);
            this.cmdMI.Text = "Create";
            this.cmdMI.ToolTipText = "Create AIO Image";
            this.cmdMI.Click += new System.EventHandler(this.cmdMI_Click);
            // 
            // BWR
            // 
            this.BWR.WorkerSupportsCancellation = true;
            this.BWR.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWR_DoWork);
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdBrowse.Location = new System.Drawing.Point(657, 12);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(75, 32);
            this.cmdBrowse.TabIndex = 1;
            this.cmdBrowse.Text = "Browse";
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
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
            this.SplitContainer2.Panel1.Controls.Add(this.cmdBrowse);
            this.SplitContainer2.Panel1.Controls.Add(this.toolStrip2);
            this.SplitContainer2.Panel1MinSize = 55;
            // 
            // SplitContainer2.Panel2
            // 
            this.SplitContainer2.Panel2.Controls.Add(this.SplitContainer1);
            this.SplitContainer2.Size = new System.Drawing.Size(744, 425);
            this.SplitContainer2.SplitterDistance = 55;
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
            this.lblWIM});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip2.ShowItemToolTips = false;
            this.toolStrip2.Size = new System.Drawing.Size(744, 55);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // lblWIM
            // 
            this.lblWIM.Margin = new System.Windows.Forms.Padding(0);
            this.lblWIM.Name = "lblWIM";
            this.lblWIM.Size = new System.Drawing.Size(182, 55);
            this.lblWIM.Text = "Please select a WIM File...";
            this.lblWIM.ToolTipText = "Please select a WIM File...";
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
            this.SplitContainer1.Size = new System.Drawing.Size(744, 369);
            this.SplitContainer1.SplitterDistance = 328;
            this.SplitContainer1.SplitterWidth = 1;
            this.SplitContainer1.TabIndex = 0;
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.CanOverflow = false;
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.Enabled = false;
            this.ToolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdMI,
            this.cmdEdit,
            this.cmdDeleteImage,
            this.cmdMakeUSB,
            this.cmdMakeISO,
            this.cmdClear,
            this.cmdEI,
            this.cmdRecovery64});
            this.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.Size = new System.Drawing.Size(744, 40);
            this.ToolStrip1.TabIndex = 0;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // cmdEdit
            // 
            this.cmdEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdEditName,
            this.cmdEditDesc,
            this.mnuEditDName,
            this.mnuEditDDesc});
            this.cmdEdit.Image = ((System.Drawing.Image)(resources.GetObject("cmdEdit.Image")));
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(62, 40);
            this.cmdEdit.Tag = "Edit";
            this.cmdEdit.Text = "Edit...";
            this.cmdEdit.ToolTipText = "Edit image details...";
            // 
            // cmdEditName
            // 
            this.cmdEditName.Name = "cmdEditName";
            this.cmdEditName.Size = new System.Drawing.Size(185, 22);
            this.cmdEditName.Tag = "Edit";
            this.cmdEditName.Text = "Edit Name";
            this.cmdEditName.ToolTipText = "Edit the Name of the selected image";
            this.cmdEditName.Click += new System.EventHandler(this.cmdEditName_Click);
            // 
            // cmdEditDesc
            // 
            this.cmdEditDesc.Name = "cmdEditDesc";
            this.cmdEditDesc.Size = new System.Drawing.Size(185, 22);
            this.cmdEditDesc.Tag = "Edit";
            this.cmdEditDesc.Text = "Edit Desc";
            this.cmdEditDesc.ToolTipText = "Edit the Description of the selected image";
            this.cmdEditDesc.Click += new System.EventHandler(this.cmdEditDesc_Click);
            // 
            // mnuEditDName
            // 
            this.mnuEditDName.Name = "mnuEditDName";
            this.mnuEditDName.Size = new System.Drawing.Size(185, 22);
            this.mnuEditDName.Tag = "Edit";
            this.mnuEditDName.Text = "Edit Display Name";
            this.mnuEditDName.ToolTipText = "Edit the display name of the image. This is the name you will see when you\'re ins" +
    "talling Windows and need to select an image.";
            this.mnuEditDName.Click += new System.EventHandler(this.mnuEditDName_Click);
            // 
            // mnuEditDDesc
            // 
            this.mnuEditDDesc.Name = "mnuEditDDesc";
            this.mnuEditDDesc.Size = new System.Drawing.Size(185, 22);
            this.mnuEditDDesc.Tag = "Edit";
            this.mnuEditDDesc.Text = "Edit Display Description";
            this.mnuEditDDesc.ToolTipText = "Edit the display description of the image. This is the name you will see when you" +
    "\'re installing Windows and need to select an image.";
            this.mnuEditDDesc.Click += new System.EventHandler(this.mnuEditDDesc_Click);
            // 
            // cmdDeleteImage
            // 
            this.cmdDeleteImage.Image = ((System.Drawing.Image)(resources.GetObject("cmdDeleteImage.Image")));
            this.cmdDeleteImage.Name = "cmdDeleteImage";
            this.cmdDeleteImage.Size = new System.Drawing.Size(98, 40);
            this.cmdDeleteImage.Text = "Delete Image";
            this.cmdDeleteImage.ToolTipText = "Delete the selected image";
            this.cmdDeleteImage.Click += new System.EventHandler(this.cmdDeleteImage_Click);
            // 
            // cmdMakeUSB
            // 
            this.cmdMakeUSB.Image = ((System.Drawing.Image)(resources.GetObject("cmdMakeUSB.Image")));
            this.cmdMakeUSB.Name = "cmdMakeUSB";
            this.cmdMakeUSB.Size = new System.Drawing.Size(97, 40);
            this.cmdMakeUSB.Text = "Prepare USB";
            this.cmdMakeUSB.ToolTipText = "Prepare a USB to install Windows from.";
            this.cmdMakeUSB.Click += new System.EventHandler(this.cmdMakeUSB_Click);
            // 
            // cmdMakeISO
            // 
            this.cmdMakeISO.Image = ((System.Drawing.Image)(resources.GetObject("cmdMakeISO.Image")));
            this.cmdMakeISO.Name = "cmdMakeISO";
            this.cmdMakeISO.Size = new System.Drawing.Size(83, 40);
            this.cmdMakeISO.Text = "Make ISO";
            this.cmdMakeISO.ToolTipText = "Make a new ISO with the selected WIM";
            this.cmdMakeISO.Click += new System.EventHandler(this.cmdMakeISO_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.Image = global::WinToolkit.Properties.Resources.Close;
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(59, 40);
            this.cmdClear.Text = "Clear";
            this.cmdClear.ToolTipText = "Start fresh!";
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdEI
            // 
            this.cmdEI.CheckOnClick = true;
            this.cmdEI.Image = global::WinToolkit.Properties.Resources.Unchecked;
            this.cmdEI.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdEI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdEI.Name = "cmdEI";
            this.cmdEI.Size = new System.Drawing.Size(112, 37);
            this.cmdEI.Text = "Unlock All Editions";
            this.cmdEI.ToolTipText = "This allows you to install any version of Windows during installation.";
            this.cmdEI.Visible = false;
            this.cmdEI.Click += new System.EventHandler(this.cmdEI_Click);
            // 
            // cmdRecovery64
            // 
            this.cmdRecovery64.CheckOnClick = true;
            this.cmdRecovery64.Image = global::WinToolkit.Properties.Resources.Unchecked;
            this.cmdRecovery64.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdRecovery64.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdRecovery64.Name = "cmdRecovery64";
            this.cmdRecovery64.Size = new System.Drawing.Size(120, 37);
            this.cmdRecovery64.Text = "x64 Recovery Mode";
            this.cmdRecovery64.ToolTipText = "Adds the 64bit recovery mode options if needed.";
            this.cmdRecovery64.Visible = false;
            this.cmdRecovery64.Click += new System.EventHandler(this.cmdRecovery64_Click);
            // 
            // BWMerge
            // 
            this.BWMerge.WorkerSupportsCancellation = true;
            this.BWMerge.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWMerge_DoWork);
            // 
            // frmAIODiskCreator
            // 
            this.AcceptButton = this.cmdBrowse;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 425);
            this.Controls.Add(this.SplitContainer2);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(760, 464);
            this.Name = "frmAIODiskCreator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AIO Creator";
            this.Load += new System.EventHandler(this.frmWIMAIO_Load);
            this.SplitContainer2.Panel1.ResumeLayout(false);
            this.SplitContainer2.Panel1.PerformLayout();
            this.SplitContainer2.Panel2.ResumeLayout(false);
            this.SplitContainer2.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.Panel2.PerformLayout();
            this.SplitContainer1.ResumeLayout(false);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView lstImages;
        internal System.Windows.Forms.ColumnHeader ColumnHeader5;
        internal System.Windows.Forms.ColumnHeader ColumnHeader6;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.ColumnHeader ColumnHeader7;
        internal System.Windows.Forms.ColumnHeader ColumnHeader8;
        internal System.Windows.Forms.ToolStripMenuItem cmdMI;
        internal System.ComponentModel.BackgroundWorker BWR;
        internal System.Windows.Forms.Button cmdBrowse;
        internal System.Windows.Forms.SplitContainer SplitContainer2;
        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.ComponentModel.BackgroundWorker BWMerge;
        internal System.Windows.Forms.ToolStrip toolStrip2;
        internal System.Windows.Forms.ToolStripLabel lblWIM;
        internal System.Windows.Forms.ToolStripMenuItem cmdClear;
        internal System.Windows.Forms.ToolStripMenuItem cmdEdit;
        internal System.Windows.Forms.ToolStripMenuItem cmdEditName;
        internal System.Windows.Forms.ToolStripMenuItem cmdEditDesc;
        internal System.Windows.Forms.ToolStripMenuItem cmdDeleteImage;
        internal System.Windows.Forms.ToolStripMenuItem cmdMakeISO;
        internal System.Windows.Forms.ToolStripMenuItem cmdMakeUSB;
        private System.Windows.Forms.ToolStripButton cmdEI;
        private System.Windows.Forms.ToolStripButton cmdRecovery64;
        internal System.Windows.Forms.ToolStripMenuItem mnuEditDName;
        internal System.Windows.Forms.ToolStripMenuItem mnuEditDDesc;

    }
}