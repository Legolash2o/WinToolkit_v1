namespace WinToolkit
{
    partial class frmWIMSplitter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWIMSplitter));
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.lstImages = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblModified = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.cmdWF = new System.Windows.Forms.Button();
            this.lblWF = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdStart = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdRebuild = new System.Windows.Forms.ToolStripMenuItem();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.cboSize = new System.Windows.Forms.ComboBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdOutput = new System.Windows.Forms.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.BWSplit = new System.ComponentModel.BackgroundWorker();
            this.BWRebuild = new System.ComponentModel.BackgroundWorker();
            this.GroupBox1.SuspendLayout();
            
            this.ToolStrip1.SuspendLayout();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            
            this.groupBox3.SuspendLayout();
            
            this.GroupBox2.SuspendLayout();
            
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.lstImages);
            this.GroupBox1.Controls.Add(this.pictureBox1);
            this.GroupBox1.Controls.Add(this.lblModified);
            this.GroupBox1.Controls.Add(this.lblSize);
            this.GroupBox1.Controls.Add(this.cmdWF);
            this.GroupBox1.Controls.Add(this.lblWF);
            this.GroupBox1.Location = new System.Drawing.Point(12, 62);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(584, 147);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "WIM File";
            // 
            // lstImages
            // 
            this.lstImages.FormattingEnabled = true;
            this.lstImages.Items.AddRange(new object[] {
            "No Image Selected..."});
            this.lstImages.Location = new System.Drawing.Point(60, 69);
            this.lstImages.Name = "lstImages";
            this.lstImages.ScrollAlwaysVisible = true;
            this.lstImages.Size = new System.Drawing.Size(517, 69);
            this.lstImages.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 54);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // lblModified
            // 
            this.lblModified.AutoEllipsis = true;
            this.lblModified.Location = new System.Drawing.Point(60, 54);
            this.lblModified.Name = "lblModified";
            this.lblModified.Size = new System.Drawing.Size(458, 19);
            this.lblModified.TabIndex = 4;
            this.lblModified.Text = "Modified: N/A";
            // 
            // lblSize
            // 
            this.lblSize.AutoEllipsis = true;
            this.lblSize.Location = new System.Drawing.Point(60, 35);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(458, 19);
            this.lblSize.TabIndex = 2;
            this.lblSize.Text = "Size: N/A";
            // 
            // cmdWF
            // 
            this.cmdWF.BackColor = System.Drawing.Color.White;
            this.cmdWF.Location = new System.Drawing.Point(524, 10);
            this.cmdWF.Name = "cmdWF";
            this.cmdWF.Size = new System.Drawing.Size(54, 25);
            this.cmdWF.TabIndex = 1;
            this.cmdWF.Text = "Browse";
            this.cmdWF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdWF.UseVisualStyleBackColor = true;
            this.cmdWF.Click += new System.EventHandler(this.cmdWF_Click);
            // 
            // lblWF
            // 
            this.lblWF.AutoEllipsis = true;
            this.lblWF.Location = new System.Drawing.Point(60, 16);
            this.lblWF.Name = "lblWF";
            this.lblWF.Size = new System.Drawing.Size(458, 19);
            this.lblWF.TabIndex = 0;
            this.lblWF.Text = "Please select WIM File...";
            this.lblWF.Click += new System.EventHandler(this.lblWF_Click);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(112, 41);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(112, 13);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "MB (Size of each part)";
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(60, 38);
            this.txtSize.MaxLength = 4;
            this.txtSize.Name = "txtSize";
            this.txtSize.ReadOnly = true;
            this.txtSize.Size = new System.Drawing.Size(46, 20);
            this.txtSize.TabIndex = 3;
            this.txtSize.Text = "700";
            this.txtSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSize_KeyPress);
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.CanOverflow = false;
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdStart,
            this.cmdRebuild,
            this.lblStatus});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.Size = new System.Drawing.Size(609, 25);
            this.ToolStrip1.Stretch = true;
            this.ToolStrip1.TabIndex = 17;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // cmdStart
            // 
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(43, 25);
            this.cmdStart.Tag = "OK";
            this.cmdStart.Text = "Start";
            this.cmdStart.ToolTipText = "Start Splitting the WIM Image";
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // cmdRebuild
            // 
            this.cmdRebuild.Enabled = false;
            this.cmdRebuild.Name = "cmdRebuild";
            this.cmdRebuild.Size = new System.Drawing.Size(59, 25);
            this.cmdRebuild.Tag = "Refresh";
            this.cmdRebuild.Text = "Rebuild";
            this.cmdRebuild.ToolTipText = "Rebuilds the WIM Image";
            this.cmdRebuild.Click += new System.EventHandler(this.cmdRebuild_Click);
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
            this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer1.Name = "SplitContainer1";
            this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.pictureBox2);
            this.SplitContainer1.Panel1.Controls.Add(this.label1);
            this.SplitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.SplitContainer1.Panel1.Controls.Add(this.GroupBox1);
            this.SplitContainer1.Panel1.Controls.Add(this.GroupBox2);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.ToolStrip1);
            this.SplitContainer1.Size = new System.Drawing.Size(609, 316);
            this.SplitContainer1.SplitterDistance = 290;
            this.SplitContainer1.SplitterWidth = 1;
            this.SplitContainer1.TabIndex = 3;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(66, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(530, 54);
            this.label1.TabIndex = 3;
            this.label1.Text = "The WIM Splitter will convert one large *.wim file into multiple read-only *.swm " +
    "files so you can burn each part onto a CD to install Windows on computers without a DVD Drive.";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBox3);
            this.groupBox3.Controls.Add(this.cboSize);
            this.groupBox3.Controls.Add(this.Label3);
            this.groupBox3.Controls.Add(this.txtSize);
            this.groupBox3.Location = new System.Drawing.Point(365, 216);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(231, 65);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Size";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(6, 13);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(48, 48);
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            // 
            // cboSize
            // 
            this.cboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSize.FormattingEnabled = true;
            this.cboSize.Items.AddRange(new object[] {
            "CD - 700MB",
            "DVD - 4.7GB",
            "DVD-DL - 8GB",
            "Custom"});
            this.cboSize.Location = new System.Drawing.Point(60, 13);
            this.cboSize.Name = "cboSize";
            this.cboSize.Size = new System.Drawing.Size(164, 21);
            this.cboSize.TabIndex = 5;
            this.cboSize.SelectedIndexChanged += new System.EventHandler(this.cboSize_SelectedIndexChanged);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.label2);
            this.GroupBox2.Controls.Add(this.cmdOutput);
            this.GroupBox2.Controls.Add(this.pictureBox5);
            this.GroupBox2.Controls.Add(this.lblOutput);
            this.GroupBox2.Location = new System.Drawing.Point(12, 216);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(347, 65);
            this.GroupBox2.TabIndex = 1;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Output";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Image Output:";
            // 
            // cmdOutput
            // 
            this.cmdOutput.BackColor = System.Drawing.Color.White;
            this.cmdOutput.Location = new System.Drawing.Point(287, 10);
            this.cmdOutput.Name = "cmdOutput";
            this.cmdOutput.Size = new System.Drawing.Size(54, 25);
            this.cmdOutput.TabIndex = 2;
            this.cmdOutput.Text = "Browse";
            this.cmdOutput.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdOutput.UseVisualStyleBackColor = true;
            this.cmdOutput.Click += new System.EventHandler(this.cmdOutput_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(5, 13);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(48, 48);
            this.pictureBox5.TabIndex = 11;
            this.pictureBox5.TabStop = false;
            // 
            // lblOutput
            // 
            this.lblOutput.AutoEllipsis = true;
            this.lblOutput.Location = new System.Drawing.Point(59, 42);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(282, 19);
            this.lblOutput.TabIndex = 2;
            this.lblOutput.Text = "Please select output location...";
            // 
            // BWSplit
            // 
            this.BWSplit.WorkerSupportsCancellation = true;
            this.BWSplit.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWSplit_DoWork);
            // 
            // BWRebuild
            // 
            this.BWRebuild.WorkerSupportsCancellation = true;
            this.BWRebuild.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWRebuild_DoWork);
            // 
            // frmWIMSplitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(609, 316);
            this.Controls.Add(this.SplitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(625, 355);
            this.Name = "frmWIMSplitter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WIM Splitter";
            this.Load += new System.EventHandler(this.frmWIMSplitter_Load);
            this.GroupBox1.ResumeLayout(false);

            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.Panel2.PerformLayout();
            this.SplitContainer1.ResumeLayout(false);
            
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Button cmdWF;
        internal System.Windows.Forms.Label lblWF;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtSize;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem cmdStart;
        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Button cmdOutput;
        internal System.Windows.Forms.Label lblOutput;
        internal System.ComponentModel.BackgroundWorker BWSplit;
        internal System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.ToolStripMenuItem cmdRebuild;
        private System.Windows.Forms.ToolStripLabel lblStatus;
        private System.ComponentModel.BackgroundWorker BWRebuild;
        internal System.Windows.Forms.Label lblModified;
        private System.Windows.Forms.ComboBox cboSize;
        private System.Windows.Forms.ListBox lstImages;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label2;
    }
}