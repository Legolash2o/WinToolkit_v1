namespace WinToolkit
{
    partial class frmDonate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDonate));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmdLego = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.rbEUR = new System.Windows.Forms.RadioButton();
            this.rbUSD = new System.Windows.Forms.RadioButton();
            this.rbGBP = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.rbFEUR = new System.Windows.Forms.RadioButton();
            this.rbFUSD = new System.Windows.Forms.RadioButton();
            this.rbFGBP = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.cmdFriendDonate = new System.Windows.Forms.Button();
            this.tabFDonate = new System.Windows.Forms.TabPage();
            this.txtFriendBank = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboName = new System.Windows.Forms.ComboBox();
            this.imageListTabs = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabFDonate.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // cmdLego
            // 
            this.cmdLego.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdLego.BackgroundImage")));
            this.cmdLego.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdLego.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmdLego.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLego.Location = new System.Drawing.Point(3, 86);
            this.cmdLego.Name = "cmdLego";
            this.cmdLego.Size = new System.Drawing.Size(421, 30);
            this.cmdLego.TabIndex = 1;
            this.cmdLego.Text = "Donate";
            this.cmdLego.UseVisualStyleBackColor = true;
            this.cmdLego.Click += new System.EventHandler(this.cmdLego_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(3, 30);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(435, 54);
            this.txtDescription.TabIndex = 2;
            this.txtDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(3, 155);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(435, 78);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Did you know?";
            this.groupBox3.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(66, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(318, 53);
            this.label3.TabIndex = 2;
            this.label3.Text = "Here is what you get when you donate:\r\n*Removes adverts when opening links\r\n*Acce" +
    "ss to exclusive areas";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageListTabs;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(449, 263);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(441, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Win Toolkit";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl2.ImageList = this.imageList1;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(435, 146);
            this.tabControl2.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.rbEUR);
            this.tabPage3.Controls.Add(this.rbUSD);
            this.tabPage3.Controls.Add(this.rbGBP);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.cmdLego);
            this.tabPage3.ImageIndex = 0;
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(427, 119);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "PayPal";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // rbEUR
            // 
            this.rbEUR.AutoSize = true;
            this.rbEUR.Location = new System.Drawing.Point(63, 53);
            this.rbEUR.Name = "rbEUR";
            this.rbEUR.Size = new System.Drawing.Size(88, 17);
            this.rbEUR.TabIndex = 8;
            this.rbEUR.Text = "€ EUR (Euro)";
            this.rbEUR.UseVisualStyleBackColor = true;
            // 
            // rbUSD
            // 
            this.rbUSD.AutoSize = true;
            this.rbUSD.Location = new System.Drawing.Point(64, 30);
            this.rbUSD.Name = "rbUSD";
            this.rbUSD.Size = new System.Drawing.Size(160, 17);
            this.rbUSD.TabIndex = 5;
            this.rbUSD.Text = "$ USD (United States Dollar)";
            this.rbUSD.UseVisualStyleBackColor = true;
            // 
            // rbGBP
            // 
            this.rbGBP.AutoSize = true;
            this.rbGBP.Checked = true;
            this.rbGBP.Location = new System.Drawing.Point(64, 7);
            this.rbGBP.Name = "rbGBP";
            this.rbGBP.Size = new System.Drawing.Size(132, 17);
            this.rbGBP.TabIndex = 4;
            this.rbGBP.TabStop = true;
            this.rbGBP.Text = "£ GBP (British Pounds)";
            this.rbGBP.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Currency:";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "paypal.png");
            this.imageList1.Images.SetKeyName(1, "Bank-16.png");
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtDescription);
            this.tabPage2.Controls.Add(this.tabControl3);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.cboName);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(441, 236);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Friends";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage5);
            this.tabControl3.Controls.Add(this.tabFDonate);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl3.ImageList = this.imageList1;
            this.tabControl3.Location = new System.Drawing.Point(3, 87);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(435, 146);
            this.tabControl3.TabIndex = 8;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.rbFEUR);
            this.tabPage5.Controls.Add(this.rbFUSD);
            this.tabPage5.Controls.Add(this.rbFGBP);
            this.tabPage5.Controls.Add(this.label7);
            this.tabPage5.Controls.Add(this.cmdFriendDonate);
            this.tabPage5.ImageIndex = 0;
            this.tabPage5.Location = new System.Drawing.Point(4, 23);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(427, 119);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "PayPal";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // rbFEUR
            // 
            this.rbFEUR.AutoSize = true;
            this.rbFEUR.Location = new System.Drawing.Point(63, 53);
            this.rbFEUR.Name = "rbFEUR";
            this.rbFEUR.Size = new System.Drawing.Size(88, 17);
            this.rbFEUR.TabIndex = 8;
            this.rbFEUR.Text = "€ EUR (Euro)";
            this.rbFEUR.UseVisualStyleBackColor = true;
            // 
            // rbFUSD
            // 
            this.rbFUSD.AutoSize = true;
            this.rbFUSD.Location = new System.Drawing.Point(64, 30);
            this.rbFUSD.Name = "rbFUSD";
            this.rbFUSD.Size = new System.Drawing.Size(160, 17);
            this.rbFUSD.TabIndex = 5;
            this.rbFUSD.Text = "$ USD (United States Dollar)";
            this.rbFUSD.UseVisualStyleBackColor = true;
            // 
            // rbFGBP
            // 
            this.rbFGBP.AutoSize = true;
            this.rbFGBP.Checked = true;
            this.rbFGBP.Location = new System.Drawing.Point(64, 7);
            this.rbFGBP.Name = "rbFGBP";
            this.rbFGBP.Size = new System.Drawing.Size(132, 17);
            this.rbFGBP.TabIndex = 4;
            this.rbFGBP.TabStop = true;
            this.rbFGBP.Text = "£ GBP (British Pounds)";
            this.rbFGBP.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Currency:";
            // 
            // cmdFriendDonate
            // 
            this.cmdFriendDonate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdFriendDonate.BackgroundImage")));
            this.cmdFriendDonate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdFriendDonate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmdFriendDonate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFriendDonate.Location = new System.Drawing.Point(3, 86);
            this.cmdFriendDonate.Name = "cmdFriendDonate";
            this.cmdFriendDonate.Size = new System.Drawing.Size(421, 30);
            this.cmdFriendDonate.TabIndex = 1;
            this.cmdFriendDonate.Text = "Donate";
            this.cmdFriendDonate.UseVisualStyleBackColor = true;
            this.cmdFriendDonate.Click += new System.EventHandler(this.cmdFriendDonate_Click);
            // 
            // tabFDonate
            // 
            this.tabFDonate.Controls.Add(this.txtFriendBank);
            this.tabFDonate.ImageIndex = 1;
            this.tabFDonate.Location = new System.Drawing.Point(4, 23);
            this.tabFDonate.Name = "tabFDonate";
            this.tabFDonate.Padding = new System.Windows.Forms.Padding(3);
            this.tabFDonate.Size = new System.Drawing.Size(427, 119);
            this.tabFDonate.TabIndex = 1;
            this.tabFDonate.Text = "Bank Transfer";
            this.tabFDonate.UseVisualStyleBackColor = true;
            // 
            // txtFriendBank
            // 
            this.txtFriendBank.AcceptsReturn = true;
            this.txtFriendBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFriendBank.Location = new System.Drawing.Point(3, 3);
            this.txtFriendBank.Multiline = true;
            this.txtFriendBank.Name = "txtFriendBank";
            this.txtFriendBank.ReadOnly = true;
            this.txtFriendBank.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFriendBank.Size = new System.Drawing.Size(421, 113);
            this.txtFriendBank.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Receiver:";
            // 
            // cboName
            // 
            this.cboName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboName.FormattingEnabled = true;
            this.cboName.Items.AddRange(new object[] {
            "Wincert",
            "Kelsenellenelvian",
            "Alphawaves"});
            this.cboName.Location = new System.Drawing.Point(65, 6);
            this.cboName.Name = "cboName";
            this.cboName.Size = new System.Drawing.Size(193, 21);
            this.cboName.TabIndex = 6;
            this.cboName.SelectedIndexChanged += new System.EventHandler(this.cboName_SelectedIndexChanged);
            // 
            // imageListTabs
            // 
            this.imageListTabs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTabs.ImageStream")));
            this.imageListTabs.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTabs.Images.SetKeyName(0, "Favorites.png");
            this.imageListTabs.Images.SetKeyName(1, "User.png");
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
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Panel2MinSize = 30;
            this.splitContainer1.Size = new System.Drawing.Size(449, 294);
            this.splitContainer1.SplitterDistance = 263;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 7;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(449, 30);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(275, 27);
            this.toolStripLabel1.Text = "Thank you for any contribution you decide to give!";
            // 
            // frmDonate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(449, 294);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDonate";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Donate";
            this.Load += new System.EventHandler(this.frmDonate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabFDonate.ResumeLayout(false);
            this.tabFDonate.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cmdLego;
        private System.Windows.Forms.Label txtDescription;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageListTabs;
        private System.Windows.Forms.RadioButton rbUSD;
        private System.Windows.Forms.RadioButton rbGBP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbEUR;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.RadioButton rbFEUR;
        private System.Windows.Forms.RadioButton rbFUSD;
        private System.Windows.Forms.RadioButton rbFGBP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cmdFriendDonate;
        private System.Windows.Forms.TabPage tabFDonate;
        private System.Windows.Forms.TextBox txtFriendBank;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboName;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}