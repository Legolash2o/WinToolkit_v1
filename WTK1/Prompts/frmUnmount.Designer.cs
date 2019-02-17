namespace WinToolkit.Prompts
{
    partial class frmUnmount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUnmount));
            this.BWRebuild = new System.ComponentModel.BackgroundWorker();
            this.lblImage = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.cmdCleanMGR = new System.Windows.Forms.Button();
            this.cmdSaveRebuild = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkDeleteMount = new System.Windows.Forms.CheckBox();
            this.cmdKeep = new System.Windows.Forms.Button();
            this.cmdDiscard = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // lblImage
            // 
            this.lblImage.AutoEllipsis = true;
            this.lblImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImage.Location = new System.Drawing.Point(65, 7);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(458, 23);
            this.lblImage.TabIndex = 9;
            this.lblImage.Text = "Image: N/A";
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
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox5);
            this.splitContainer1.Panel1.Controls.Add(this.lblImage);
            this.splitContainer1.Panel1MinSize = 70;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdCleanMGR);
            this.splitContainer1.Panel2.Controls.Add(this.cmdSaveRebuild);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.chkDeleteMount);
            this.splitContainer1.Panel2.Controls.Add(this.cmdKeep);
            this.splitContainer1.Panel2.Controls.Add(this.cmdDiscard);
            this.splitContainer1.Panel2.Controls.Add(this.cmdSave);
            this.splitContainer1.Size = new System.Drawing.Size(552, 229);
            this.splitContainer1.SplitterDistance = 70;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(66, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(438, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "Win Toolkit is about to unmount an image, please select an option below.";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(10, 7);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(48, 48);
            this.pictureBox5.TabIndex = 10;
            this.pictureBox5.TabStop = false;
            // 
            // cmdCleanMGR
            // 
            this.cmdCleanMGR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdCleanMGR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCleanMGR.Image = ((System.Drawing.Image)(resources.GetObject("cmdCleanMGR.Image")));
            this.cmdCleanMGR.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCleanMGR.Location = new System.Drawing.Point(3, 111);
            this.cmdCleanMGR.Name = "cmdCleanMGR";
            this.cmdCleanMGR.Size = new System.Drawing.Size(135, 42);
            this.cmdCleanMGR.TabIndex = 18;
            this.cmdCleanMGR.Text = "Cleanup Manager";
            this.cmdCleanMGR.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCleanMGR.UseVisualStyleBackColor = true;
            this.cmdCleanMGR.Click += new System.EventHandler(this.cmdCleanMGR_Click);
            // 
            // cmdSaveRebuild
            // 
            this.cmdSaveRebuild.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdSaveRebuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSaveRebuild.Image = ((System.Drawing.Image)(resources.GetObject("cmdSaveRebuild.Image")));
            this.cmdSaveRebuild.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdSaveRebuild.Location = new System.Drawing.Point(3, 5);
            this.cmdSaveRebuild.Name = "cmdSaveRebuild";
            this.cmdSaveRebuild.Size = new System.Drawing.Size(135, 105);
            this.cmdSaveRebuild.TabIndex = 17;
            this.cmdSaveRebuild.Text = "Save and Rebuild";
            this.cmdSaveRebuild.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdSaveRebuild.UseVisualStyleBackColor = true;
            this.cmdSaveRebuild.Click += new System.EventHandler(this.cmdSaveRebuild_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(286, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 42);
            this.label1.TabIndex = 16;
            this.label1.Text = "Hover your mouse over each button for a description.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkDeleteMount
            // 
            this.chkDeleteMount.AutoSize = true;
            this.chkDeleteMount.Location = new System.Drawing.Point(144, 125);
            this.chkDeleteMount.Name = "chkDeleteMount";
            this.chkDeleteMount.Size = new System.Drawing.Size(122, 17);
            this.chkDeleteMount.TabIndex = 12;
            this.chkDeleteMount.Text = "Delete Mount Folder";
            this.chkDeleteMount.UseVisualStyleBackColor = true;
            this.chkDeleteMount.CheckedChanged += new System.EventHandler(this.chkDeleteMount_CheckedChanged);
            // 
            // cmdKeep
            // 
            this.cmdKeep.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdKeep.Image = ((System.Drawing.Image)(resources.GetObject("cmdKeep.Image")));
            this.cmdKeep.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdKeep.Location = new System.Drawing.Point(409, 5);
            this.cmdKeep.Name = "cmdKeep";
            this.cmdKeep.Size = new System.Drawing.Size(135, 105);
            this.cmdKeep.TabIndex = 15;
            this.cmdKeep.Text = "Keep Mounted";
            this.cmdKeep.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdKeep.UseVisualStyleBackColor = true;
            this.cmdKeep.Click += new System.EventHandler(this.cmdKeep_Click);
            // 
            // cmdDiscard
            // 
            this.cmdDiscard.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDiscard.Image = ((System.Drawing.Image)(resources.GetObject("cmdDiscard.Image")));
            this.cmdDiscard.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdDiscard.Location = new System.Drawing.Point(274, 5);
            this.cmdDiscard.Name = "cmdDiscard";
            this.cmdDiscard.Size = new System.Drawing.Size(135, 105);
            this.cmdDiscard.TabIndex = 14;
            this.cmdDiscard.Text = "Discard Changes";
            this.cmdDiscard.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdDiscard.UseVisualStyleBackColor = true;
            this.cmdDiscard.Click += new System.EventHandler(this.cmdDiscard_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdSave.Location = new System.Drawing.Point(138, 5);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(135, 105);
            this.cmdSave.TabIndex = 13;
            this.cmdSave.Text = "Save Changes";
            this.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // frmUnmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(552, 229);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUnmount";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Please select an option...";
            this.Load += new System.EventHandler(this.frmUnmount_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.ComponentModel.BackgroundWorker BWRebuild;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label9;
		  private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdKeep;
        private System.Windows.Forms.Button cmdDiscard;
		  private System.Windows.Forms.Button cmdSave;
		  protected internal System.Windows.Forms.Button cmdSaveRebuild;
          private System.Windows.Forms.CheckBox chkDeleteMount;
          protected internal System.Windows.Forms.Button cmdCleanMGR;
    }
}