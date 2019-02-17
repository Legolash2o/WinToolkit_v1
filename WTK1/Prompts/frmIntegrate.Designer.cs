namespace WinToolkit.Prompts
{
    partial class frmIntegrate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIntegrate));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lstStatus = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgListUpdates = new System.Windows.Forms.ImageList(this.components);
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdSupport = new System.Windows.Forms.ToolStripMenuItem();
            this.lblLocation = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ToolStrip1);
            this.splitContainer1.Panel2MinSize = 30;
            this.splitContainer1.Size = new System.Drawing.Size(534, 371);
            this.splitContainer1.SplitterDistance = 340;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 15;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lblLocation);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.pictureBox6);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(534, 340);
            this.splitContainer2.SplitterDistance = 61;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(64, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(466, 31);
            this.label1.TabIndex = 13;
            this.label1.Text = "Update Information";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox6.BackgroundImage")));
            this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox6.Location = new System.Drawing.Point(10, 6);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(48, 48);
            this.pictureBox6.TabIndex = 12;
            this.pictureBox6.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imgListUpdates;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(534, 278);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lstStatus);
            this.tabPage1.ImageIndex = 12;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(526, 251);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Update";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lstStatus
            // 
            this.lstStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstStatus.FullRowSelect = true;
            this.lstStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstStatus.Location = new System.Drawing.Point(0, 0);
            this.lstStatus.MultiSelect = false;
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(526, 251);
            this.lstStatus.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstStatus.TabIndex = 0;
            this.lstStatus.UseCompatibleStateImageBehavior = false;
            this.lstStatus.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item";
            this.columnHeader1.Width = 139;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 352;
            // 
            // imgListUpdates
            // 
            this.imgListUpdates.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListUpdates.ImageStream")));
            this.imgListUpdates.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListUpdates.Images.SetKeyName(0, "AIO_Silent_20x20.png");
            this.imgListUpdates.Images.SetKeyName(1, "Gnome-Edit-Clear-64.png");
            this.imgListUpdates.Images.SetKeyName(2, "image-missing.png");
            this.imgListUpdates.Images.SetKeyName(3, "Play-icon.png");
            this.imgListUpdates.Images.SetKeyName(4, "Search.png");
            this.imgListUpdates.Images.SetKeyName(5, "Close.png");
            this.imgListUpdates.Images.SetKeyName(6, "OK.png");
            this.imgListUpdates.Images.SetKeyName(7, "Mount.png");
            this.imgListUpdates.Images.SetKeyName(8, "Question.png");
            this.imgListUpdates.Images.SetKeyName(9, "gtk-missing-image.png");
            this.imgListUpdates.Images.SetKeyName(10, "SI_Failed.png");
            this.imgListUpdates.Images.SetKeyName(11, "linguist-check-warning.png");
            this.imgListUpdates.Images.SetKeyName(12, "MSUUpdate_16.png");
            this.imgListUpdates.Images.SetKeyName(13, "MSUUpdateGDR_16.png");
            this.imgListUpdates.Images.SetKeyName(14, "MSUUpdateLDR_16.png");
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.CanOverflow = false;
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdClose,
            this.toolStripSeparator1,
            this.cmdSupport});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new System.Drawing.Size(534, 30);
            this.ToolStrip1.Stretch = true;
            this.ToolStrip1.TabIndex = 18;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(55, 30);
            this.cmdClose.Tag = "Cancel";
            this.cmdClose.Text = "Close";
            this.cmdClose.ToolTipText = "Start Cleaning...";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // cmdSupport
            // 
            this.cmdSupport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSupport.Name = "cmdSupport";
            this.cmdSupport.Size = new System.Drawing.Size(77, 30);
            this.cmdSupport.Tag = "Info";
            this.cmdSupport.Text = "KB Article";
            this.cmdSupport.ToolTipText = "Start Cleaning...";
            this.cmdSupport.Click += new System.EventHandler(this.cmdSupport_Click);
            // 
            // lblLocation
            // 
            this.lblLocation.Location = new System.Drawing.Point(64, 36);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(466, 23);
            this.lblLocation.TabIndex = 14;
            this.lblLocation.Text = "N/A";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmIntegrate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(534, 371);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 410);
            this.Name = "frmIntegrate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Information";
            this.Load += new System.EventHandler(this.frmIntegrate_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox6;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem cmdSupport;
        internal System.Windows.Forms.ToolStripMenuItem cmdClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ImageList imgListUpdates;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView lstStatus;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label lblLocation;
    }
}