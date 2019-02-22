namespace WinToolkit
{
    partial class frmDownload_ISO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDownload_ISO));
            this.cmdWin7SP1 = new System.Windows.Forms.Button();
            this.gbWin7 = new System.Windows.Forms.GroupBox();
            this.txtWin7 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdWin10 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdWindows8Buy = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmdWin81 = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdWin2012R2 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PBProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.gbWin7.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdWin7SP1
            // 
            this.cmdWin7SP1.BackColor = System.Drawing.SystemColors.Control;
            this.cmdWin7SP1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdWin7SP1.BackgroundImage")));
            this.cmdWin7SP1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdWin7SP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cmdWin7SP1.Image = ((System.Drawing.Image)(resources.GetObject("cmdWin7SP1.Image")));
            this.cmdWin7SP1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdWin7SP1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdWin7SP1.Location = new System.Drawing.Point(8, 24);
            this.cmdWin7SP1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdWin7SP1.Name = "cmdWin7SP1";
            this.cmdWin7SP1.Size = new System.Drawing.Size(135, 135);
            this.cmdWin7SP1.TabIndex = 4;
            this.cmdWin7SP1.Text = "Download";
            this.cmdWin7SP1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdWin7SP1.UseVisualStyleBackColor = true;
            this.cmdWin7SP1.Click += new System.EventHandler(this.cmdWin7SP1_Click);
            // 
            // gbWin7
            // 
            this.gbWin7.Controls.Add(this.txtWin7);
            this.gbWin7.Controls.Add(this.cmdWin7SP1);
            this.gbWin7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbWin7.Location = new System.Drawing.Point(6, 540);
            this.gbWin7.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gbWin7.Name = "gbWin7";
            this.gbWin7.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.gbWin7.Size = new System.Drawing.Size(1060, 166);
            this.gbWin7.TabIndex = 5;
            this.gbWin7.TabStop = false;
            this.gbWin7.Text = "Windows 7 SP1 Refresh";
            // 
            // txtWin7
            // 
            this.txtWin7.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtWin7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWin7.Location = new System.Drawing.Point(146, 29);
            this.txtWin7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtWin7.Name = "txtWin7";
            this.txtWin7.Size = new System.Drawing.Size(908, 131);
            this.txtWin7.TabIndex = 5;
            this.txtWin7.Text = "These are free and legal ISOs which allow you to have all the latest updates and " +
    "features without going through the hassle of integrating SP1.\r\n\r\nNOTE: Product k" +
    "ey required.";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Controls.Add(this.gbWin7);
            this.flowLayoutPanel1.Controls.Add(this.groupBox11);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1113, 595);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmdWin10);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Size = new System.Drawing.Size(1060, 166);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Windows 10 ISO";
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(146, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(908, 131);
            this.label3.TabIndex = 5;
            this.label3.Text = "Windows 10 is the best Windows ever and the full version is still available as it" +
    " always has been. Get Windows and start doing great things.\r\n\r\nKey: No key requi" +
    "red.";
            // 
            // cmdWin10
            // 
            this.cmdWin10.BackColor = System.Drawing.SystemColors.Control;
            this.cmdWin10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdWin10.BackgroundImage")));
            this.cmdWin10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdWin10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cmdWin10.Image = ((System.Drawing.Image)(resources.GetObject("cmdWin10.Image")));
            this.cmdWin10.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdWin10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdWin10.Location = new System.Drawing.Point(8, 24);
            this.cmdWin10.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdWin10.Name = "cmdWin10";
            this.cmdWin10.Size = new System.Drawing.Size(135, 135);
            this.cmdWin10.TabIndex = 4;
            this.cmdWin10.Text = "Download";
            this.cmdWin10.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdWin10.UseVisualStyleBackColor = true;
            this.cmdWin10.Click += new System.EventHandler(this.cmdWin10_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cmdWindows8Buy);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(6, 184);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox3.Size = new System.Drawing.Size(1060, 166);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Purchase Windows 10";
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(146, 29);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(908, 131);
            this.label5.TabIndex = 5;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // cmdWindows8Buy
            // 
            this.cmdWindows8Buy.BackColor = System.Drawing.SystemColors.Control;
            this.cmdWindows8Buy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdWindows8Buy.BackgroundImage")));
            this.cmdWindows8Buy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdWindows8Buy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cmdWindows8Buy.Image = ((System.Drawing.Image)(resources.GetObject("cmdWindows8Buy.Image")));
            this.cmdWindows8Buy.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdWindows8Buy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdWindows8Buy.Location = new System.Drawing.Point(8, 24);
            this.cmdWindows8Buy.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdWindows8Buy.Name = "cmdWindows8Buy";
            this.cmdWindows8Buy.Size = new System.Drawing.Size(135, 135);
            this.cmdWindows8Buy.TabIndex = 4;
            this.cmdWindows8Buy.Text = "Purchase";
            this.cmdWindows8Buy.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdWindows8Buy.UseVisualStyleBackColor = true;
            this.cmdWindows8Buy.Click += new System.EventHandler(this.cmdWindows8Buy_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.cmdWin81);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(6, 362);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox4.Size = new System.Drawing.Size(1060, 166);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Windows 8.1 ISO - Enterprise Trial";
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(146, 29);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(908, 131);
            this.label7.TabIndex = 5;
            this.label7.Text = resources.GetString("label7.Text");
            // 
            // cmdWin81
            // 
            this.cmdWin81.BackColor = System.Drawing.SystemColors.Control;
            this.cmdWin81.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdWin81.BackgroundImage")));
            this.cmdWin81.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdWin81.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cmdWin81.Image = ((System.Drawing.Image)(resources.GetObject("cmdWin81.Image")));
            this.cmdWin81.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdWin81.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdWin81.Location = new System.Drawing.Point(8, 24);
            this.cmdWin81.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdWin81.Name = "cmdWin81";
            this.cmdWin81.Size = new System.Drawing.Size(135, 135);
            this.cmdWin81.TabIndex = 4;
            this.cmdWin81.Text = "Download";
            this.cmdWin81.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdWin81.UseVisualStyleBackColor = true;
            this.cmdWin81.Click += new System.EventHandler(this.cmdWin81_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.label2);
            this.groupBox11.Controls.Add(this.cmdWin2012R2);
            this.groupBox11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox11.Location = new System.Drawing.Point(6, 718);
            this.groupBox11.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox11.Size = new System.Drawing.Size(1060, 166);
            this.groupBox11.TabIndex = 6;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Windows Server 2012 R2";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(151, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(903, 131);
            this.label2.TabIndex = 5;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // cmdWin2012R2
            // 
            this.cmdWin2012R2.BackColor = System.Drawing.SystemColors.Control;
            this.cmdWin2012R2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdWin2012R2.BackgroundImage")));
            this.cmdWin2012R2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdWin2012R2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cmdWin2012R2.Image = ((System.Drawing.Image)(resources.GetObject("cmdWin2012R2.Image")));
            this.cmdWin2012R2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdWin2012R2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmdWin2012R2.Location = new System.Drawing.Point(8, 24);
            this.cmdWin2012R2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdWin2012R2.Name = "cmdWin2012R2";
            this.cmdWin2012R2.Size = new System.Drawing.Size(135, 135);
            this.cmdWin2012R2.TabIndex = 4;
            this.cmdWin2012R2.Text = "Download";
            this.cmdWin2012R2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdWin2012R2.UseVisualStyleBackColor = true;
            this.cmdWin2012R2.Click += new System.EventHandler(this.cmdWin2012R2_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "DVD Blue.png");
            this.imageList1.Images.SetKeyName(1, "LockIcon.png");
            this.imageList1.Images.SetKeyName(2, "icon-download.png");
            this.imageList1.Images.SetKeyName(3, "info_icon.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PBProgress,
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1113, 25);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // PBProgress
            // 
            this.PBProgress.Name = "PBProgress";
            this.PBProgress.Size = new System.Drawing.Size(400, 19);
            this.PBProgress.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(827, 20);
            this.lblStatus.Text = "Asking for serial, keys, cracks, keygens, etc... will get you banned and I won\'t " +
    "even reply to the requests.";
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scMain.IsSplitterFixed = true;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.statusStrip1);
            this.scMain.Size = new System.Drawing.Size(1113, 622);
            this.scMain.SplitterDistance = 595;
            this.scMain.SplitterWidth = 2;
            this.scMain.TabIndex = 9;
            // 
            // frmDownload_ISO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1113, 622);
            this.Controls.Add(this.scMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1126, 653);
            this.Name = "frmDownload_ISO";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Windows ISOs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmD_ISO_FormClosing);
            this.Load += new System.EventHandler(this.frmD_ISO_Load);
            this.gbWin7.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.Panel2.PerformLayout();
            this.scMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button cmdWin7SP1;
        private System.Windows.Forms.GroupBox gbWin7;
        private System.Windows.Forms.Label txtWin7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Button cmdWindows8Buy;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar PBProgress;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Button cmdWin81;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button cmdWin2012R2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button cmdWin10;
    }
}