namespace WinToolkit.Prompts {
	partial class frmAbout {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.scAbout = new System.Windows.Forms.SplitContainer();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblTC = new System.Windows.Forms.Label();
            this.lblMain = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtCL = new System.Windows.Forms.TextBox();
            this.scAbout.Panel1.SuspendLayout();
            this.scAbout.Panel2.SuspendLayout();
            this.scAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // scAbout
            // 
            this.scAbout.BackColor = System.Drawing.Color.White;
            this.scAbout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scAbout.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scAbout.IsSplitterFixed = true;
            this.scAbout.Location = new System.Drawing.Point(0, 0);
            this.scAbout.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.scAbout.Name = "scAbout";
            this.scAbout.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scAbout.Panel1
            // 
            this.scAbout.Panel1.Controls.Add(this.pictureBox2);
            this.scAbout.Panel1.Controls.Add(this.lblTC);
            this.scAbout.Panel1.Controls.Add(this.lblMain);
            this.scAbout.Panel1.Controls.Add(this.lblVersion);
            this.scAbout.Panel1MinSize = 70;
            // 
            // scAbout.Panel2
            // 
            this.scAbout.Panel2.Controls.Add(this.txtCL);
            this.scAbout.Size = new System.Drawing.Size(968, 477);
            this.scAbout.SplitterDistance = 70;
            this.scAbout.SplitterWidth = 2;
            this.scAbout.TabIndex = 23;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox2.Location = new System.Drawing.Point(4, 5);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(96, 98);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // lblTC
            // 
            this.lblTC.AutoSize = true;
            this.lblTC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblTC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTC.Location = new System.Drawing.Point(114, 78);
            this.lblTC.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblTC.Name = "lblTC";
            this.lblTC.Size = new System.Drawing.Size(138, 22);
            this.lblTC.TabIndex = 2;
            this.lblTC.Text = "Total Changes: ";
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMain.Location = new System.Drawing.Point(114, 5);
            this.lblMain.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(328, 22);
            this.lblMain.TabIndex = 0;
            this.lblMain.Text = "Win Toolkit by Legolash2o [Wincert.net]";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblVersion.Location = new System.Drawing.Point(114, 38);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(81, 22);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "Version: ";
            // 
            // txtCL
            // 
            this.txtCL.BackColor = System.Drawing.Color.White;
            this.txtCL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCL.Location = new System.Drawing.Point(0, 0);
            this.txtCL.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtCL.Multiline = true;
            this.txtCL.Name = "txtCL";
            this.txtCL.ReadOnly = true;
            this.txtCL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCL.Size = new System.Drawing.Size(966, 403);
            this.txtCL.TabIndex = 21;
            this.txtCL.Text = resources.GetString("txtCL.Text");
            this.txtCL.TextChanged += new System.EventHandler(this.txtCL_TextChanged);
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 477);
            this.Controls.Add(this.scAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Changelog";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            this.Shown += new System.EventHandler(this.frmAbout_Shown);
            this.scAbout.Panel1.ResumeLayout(false);
            this.scAbout.Panel1.PerformLayout();
            this.scAbout.Panel2.ResumeLayout(false);
            this.scAbout.Panel2.PerformLayout();
            this.scAbout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer scAbout;
		private System.Windows.Forms.PictureBox pictureBox2;
		internal System.Windows.Forms.Label lblTC;
		internal System.Windows.Forms.Label lblMain;
		internal System.Windows.Forms.Label lblVersion;
		internal System.Windows.Forms.TextBox txtCL;
	}
}