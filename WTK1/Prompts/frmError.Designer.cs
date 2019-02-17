namespace WinToolkit {
	partial class frmErrorBox {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmErrorBox));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuSCB = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSTF = new System.Windows.Forms.ToolStripMenuItem();
            this.txtEx = new System.Windows.Forms.TextBox();
            this.cmdMI = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdTS = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(66, 29);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(552, 39);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "An error has occurred";
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.Location = new System.Drawing.Point(67, 73);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(551, 83);
            this.lblDesc.TabIndex = 2;
            this.lblDesc.Text = "Description";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSCB,
            this.mnuSTF});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(621, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuSCB
            // 
            this.mnuSCB.Image = ((System.Drawing.Image)(resources.GetObject("mnuSCB.Image")));
            this.mnuSCB.Name = "mnuSCB";
            this.mnuSCB.Size = new System.Drawing.Size(128, 20);
            this.mnuSCB.Text = "Save to Clipboard";
            this.mnuSCB.Click += new System.EventHandler(this.mnuSCB_Click);
            // 
            // mnuSTF
            // 
            this.mnuSTF.Image = ((System.Drawing.Image)(resources.GetObject("mnuSTF.Image")));
            this.mnuSTF.Name = "mnuSTF";
            this.mnuSTF.Size = new System.Drawing.Size(118, 20);
            this.mnuSTF.Text = "Save to Text File";
            this.mnuSTF.Click += new System.EventHandler(this.mnuSTF_Click);
            // 
            // txtEx
            // 
            this.txtEx.BackColor = System.Drawing.Color.White;
            this.txtEx.Location = new System.Drawing.Point(6, 186);
            this.txtEx.Multiline = true;
            this.txtEx.Name = "txtEx";
            this.txtEx.ReadOnly = true;
            this.txtEx.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEx.Size = new System.Drawing.Size(611, 156);
            this.txtEx.TabIndex = 4;
            this.txtEx.TabStop = false;
            this.txtEx.Visible = false;
            // 
            // cmdMI
            // 
            this.cmdMI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMI.Location = new System.Drawing.Point(311, 159);
            this.cmdMI.Name = "cmdMI";
            this.cmdMI.Size = new System.Drawing.Size(84, 23);
            this.cmdMI.TabIndex = 5;
            this.cmdMI.Text = ">> Details";
            this.cmdMI.UseVisualStyleBackColor = true;
            this.cmdMI.Click += new System.EventHandler(this.cmdMI_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Location = new System.Drawing.Point(534, 159);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(84, 23);
            this.cmdOK.TabIndex = 6;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdTS
            // 
            this.cmdTS.Enabled = false;
            this.cmdTS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTS.Location = new System.Drawing.Point(401, 159);
            this.cmdTS.Name = "cmdTS";
            this.cmdTS.Size = new System.Drawing.Size(127, 23);
            this.cmdTS.TabIndex = 7;
            this.cmdTS.Text = "Save Screenshot";
            this.cmdTS.UseVisualStyleBackColor = true;
            this.cmdTS.Click += new System.EventHandler(this.cmdTS_Click);
            // 
            // frmErrorBox
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(621, 184);
            this.Controls.Add(this.cmdTS);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdMI);
            this.Controls.Add(this.txtEx);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(637, 215);
            this.Name = "frmErrorBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Error";
            this.Load += new System.EventHandler(this.frmError_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnuSCB;
		private System.Windows.Forms.ToolStripMenuItem mnuSTF;
		private System.Windows.Forms.Button cmdMI;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdTS;
		public System.Windows.Forms.Label lblTitle;
		public System.Windows.Forms.Label lblDesc;
		public System.Windows.Forms.TextBox txtEx;
	}
}