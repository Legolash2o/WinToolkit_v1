namespace WinToolkit
{
    partial class frmWIMMerger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWIMMerger));
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lstImages = new System.Windows.Forms.ListBox();
            this.lblModified = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.cmdWF = new System.Windows.Forms.Button();
            this.lblWF = new System.Windows.Forms.Label();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdStart = new System.Windows.Forms.ToolStripMenuItem();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.cmdOutput = new System.Windows.Forms.Button();
            this.lblOutput = new System.Windows.Forms.Label();
            this.BWMerge = new System.ComponentModel.BackgroundWorker();
            this.GroupBox1.SuspendLayout();
            
            this.ToolStrip1.SuspendLayout();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            
            this.GroupBox2.SuspendLayout();
            
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.pictureBox1);
            this.GroupBox1.Controls.Add(this.lstImages);
            this.GroupBox1.Controls.Add(this.lblModified);
            this.GroupBox1.Controls.Add(this.lblSize);
            this.GroupBox1.Controls.Add(this.cmdWF);
            this.GroupBox1.Controls.Add(this.lblWF);
            this.GroupBox1.Location = new System.Drawing.Point(8, 60);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(506, 147);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "SWM File";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 54);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // lstImages
            // 
            this.lstImages.FormattingEnabled = true;
            this.lstImages.Items.AddRange(new object[] {
            "No Image Selected..."});
            this.lstImages.Location = new System.Drawing.Point(63, 68);
            this.lstImages.Name = "lstImages";
            this.lstImages.ScrollAlwaysVisible = true;
            this.lstImages.Size = new System.Drawing.Size(422, 69);
            this.lstImages.TabIndex = 5;
            // 
            // lblModified
            // 
            this.lblModified.AutoEllipsis = true;
            this.lblModified.Location = new System.Drawing.Point(60, 54);
            this.lblModified.Name = "lblModified";
            this.lblModified.Size = new System.Drawing.Size(377, 19);
            this.lblModified.TabIndex = 4;
            this.lblModified.Text = "Modified: N/A";
            // 
            // lblSize
            // 
            this.lblSize.AutoEllipsis = true;
            this.lblSize.Location = new System.Drawing.Point(60, 35);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(377, 19);
            this.lblSize.TabIndex = 2;
            this.lblSize.Text = "Size: N/A";
            // 
            // cmdWF
            // 
            this.cmdWF.BackColor = System.Drawing.Color.White;
            this.cmdWF.Location = new System.Drawing.Point(443, 10);
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
            this.lblWF.Size = new System.Drawing.Size(377, 19);
            this.lblWF.TabIndex = 0;
            this.lblWF.Text = "Please select *.swm File...";
            this.lblWF.Click += new System.EventHandler(this.lblWF_Click);
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.CanOverflow = false;
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdStart,
            this.lblStatus});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.Size = new System.Drawing.Size(522, 25);
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
            this.SplitContainer1.Panel1.Controls.Add(this.GroupBox1);
            this.SplitContainer1.Panel1.Controls.Add(this.GroupBox2);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.ToolStrip1);
            this.SplitContainer1.Size = new System.Drawing.Size(522, 314);
            this.SplitContainer1.SplitterDistance = 288;
            this.SplitContainer1.SplitterWidth = 1;
            this.SplitContainer1.TabIndex = 3;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(8, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(62, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(437, 48);
            this.label1.TabIndex = 3;
            this.label1.Text = "The SWM Merger will convert many *.swm files into a single *.wim file so you can " +
    "edit it. *.swm files are read-only.";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.label2);
            this.GroupBox2.Controls.Add(this.pictureBox5);
            this.GroupBox2.Controls.Add(this.cmdOutput);
            this.GroupBox2.Controls.Add(this.lblOutput);
            this.GroupBox2.Location = new System.Drawing.Point(8, 213);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(506, 68);
            this.GroupBox2.TabIndex = 1;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Output";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Output File:";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(6, 14);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(48, 48);
            this.pictureBox5.TabIndex = 9;
            this.pictureBox5.TabStop = false;
            // 
            // cmdOutput
            // 
            this.cmdOutput.BackColor = System.Drawing.Color.White;
            this.cmdOutput.Location = new System.Drawing.Point(443, 10);
            this.cmdOutput.Name = "cmdOutput";
            this.cmdOutput.Size = new System.Drawing.Size(54, 25);
            this.cmdOutput.TabIndex = 2;
            this.cmdOutput.Text = "Browse";
            this.cmdOutput.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdOutput.UseVisualStyleBackColor = true;
            this.cmdOutput.Click += new System.EventHandler(this.cmdOutput_Click);
            // 
            // lblOutput
            // 
            this.lblOutput.AutoEllipsis = true;
            this.lblOutput.Location = new System.Drawing.Point(60, 43);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(437, 19);
            this.lblOutput.TabIndex = 2;
            this.lblOutput.Text = "Please select output location...";
            // 
            // BWMerge
            // 
            this.BWMerge.WorkerSupportsCancellation = true;
            this.BWMerge.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWSplit_DoWork);
            // 
            // frmWIMMerger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(522, 314);
            this.Controls.Add(this.SplitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(538, 353);
            this.Name = "frmWIMMerger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SWM Merger";
            this.Load += new System.EventHandler(this.frmWIMMerger_Load);
            this.GroupBox1.ResumeLayout(false);
            
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.Panel2.PerformLayout();
            this.SplitContainer1.ResumeLayout(false);
            
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();

            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Button cmdWF;
        internal System.Windows.Forms.Label lblWF;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem cmdStart;
        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Button cmdOutput;
        internal System.Windows.Forms.Label lblOutput;
        internal System.ComponentModel.BackgroundWorker BWMerge;
        internal System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripLabel lblStatus;
        internal System.Windows.Forms.Label lblModified;
        private System.Windows.Forms.ListBox lstImages;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox5;
    }
}