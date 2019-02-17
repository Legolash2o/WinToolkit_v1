namespace WinToolkit.Prompts
{
    partial class frmAntiVirus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAntiVirus));
            this.lblAV = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chkAV = new System.Windows.Forms.CheckBox();
            this.GBAV = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.GBTF = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblTemp = new System.Windows.Forms.Label();
            this.GBSSD = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timClose = new System.Windows.Forms.Timer(this.components);
            
            this.GBAV.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.GBTF.SuspendLayout();
            
            this.GBSSD.SuspendLayout();
            
            this.SuspendLayout();
            // 
            // lblAV
            // 
            this.lblAV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAV.Location = new System.Drawing.Point(77, 16);
            this.lblAV.Name = "lblAV";
            this.lblAV.Size = new System.Drawing.Size(408, 68);
            this.lblAV.TabIndex = 1;
            this.lblAV.Text = resources.GetString("lblAV.Text");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // chkAV
            // 
            this.chkAV.AutoSize = true;
            this.chkAV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAV.Location = new System.Drawing.Point(344, 67);
            this.chkAV.Name = "chkAV";
            this.chkAV.Size = new System.Drawing.Size(127, 17);
            this.chkAV.TabIndex = 4;
            this.chkAV.Text = "Check at next startup";
            this.chkAV.UseVisualStyleBackColor = true;
            this.chkAV.CheckedChanged += new System.EventHandler(this.chkAV_CheckedChanged);
            // 
            // GBAV
            // 
            this.GBAV.Controls.Add(this.chkAV);
            this.GBAV.Controls.Add(this.pictureBox1);
            this.GBAV.Controls.Add(this.lblAV);
            this.GBAV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBAV.Location = new System.Drawing.Point(3, 1);
            this.GBAV.Margin = new System.Windows.Forms.Padding(0);
            this.GBAV.Name = "GBAV";
            this.GBAV.Padding = new System.Windows.Forms.Padding(0);
            this.GBAV.Size = new System.Drawing.Size(491, 95);
            this.GBAV.TabIndex = 5;
            this.GBAV.TabStop = false;
            this.GBAV.Text = "AntiVirus Detected";
            this.GBAV.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.GBAV);
            this.flowLayoutPanel1.Controls.Add(this.GBTF);
            this.flowLayoutPanel1.Controls.Add(this.GBSSD);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(497, 338);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // GBTF
            // 
            this.GBTF.Controls.Add(this.pictureBox2);
            this.GBTF.Controls.Add(this.lblTemp);
            this.GBTF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBTF.Location = new System.Drawing.Point(3, 96);
            this.GBTF.Margin = new System.Windows.Forms.Padding(0);
            this.GBTF.Name = "GBTF";
            this.GBTF.Padding = new System.Windows.Forms.Padding(0);
            this.GBTF.Size = new System.Drawing.Size(491, 119);
            this.GBTF.TabIndex = 6;
            this.GBTF.TabStop = false;
            this.GBTF.Text = "Temp Folders";
            this.GBTF.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(7, 33);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(64, 64);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // lblTemp
            // 
            this.lblTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemp.Location = new System.Drawing.Point(77, 16);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(408, 97);
            this.lblTemp.TabIndex = 1;
            this.lblTemp.Text = "Win Toolkit needs to create temp folders. Based on the amount of freespace, Win T" +
    "oolkit has selected the following paths:";
            // 
            // GBSSD
            // 
            this.GBSSD.Controls.Add(this.pictureBox3);
            this.GBSSD.Controls.Add(this.label1);
            this.GBSSD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GBSSD.Location = new System.Drawing.Point(3, 215);
            this.GBSSD.Margin = new System.Windows.Forms.Padding(0);
            this.GBSSD.Name = "GBSSD";
            this.GBSSD.Padding = new System.Windows.Forms.Padding(0);
            this.GBSSD.Size = new System.Drawing.Size(491, 119);
            this.GBSSD.TabIndex = 7;
            this.GBSSD.TabStop = false;
            this.GBSSD.Text = "Speed up integration?";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(7, 33);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(64, 64);
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(77, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(408, 97);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // timClose
            // 
            this.timClose.Interval = 1000;
            this.timClose.Tick += new System.EventHandler(this.timClose_Tick);
            // 
            // frmAntiVirus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(497, 338);
            this.Controls.Add(this.flowLayoutPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAntiVirus";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Information";
            this.Load += new System.EventHandler(this.frmAntiVirus_Load);
            
            this.GBAV.ResumeLayout(false);
            this.GBAV.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.GBTF.ResumeLayout(false);
            
            this.GBSSD.ResumeLayout(false);
            
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.CheckBox chkAV;
        internal System.Windows.Forms.GroupBox GBAV;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        internal System.Windows.Forms.GroupBox GBTF;
        private System.Windows.Forms.PictureBox pictureBox2;
        internal System.Windows.Forms.Label lblTemp;
        internal System.Windows.Forms.Label lblAV;
        internal System.Windows.Forms.GroupBox GBSSD;
        private System.Windows.Forms.PictureBox pictureBox3;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timClose;
    }
}