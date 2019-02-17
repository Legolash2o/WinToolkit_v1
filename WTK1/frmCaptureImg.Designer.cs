namespace WinToolkit
{
    partial class frmCaptureImg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCaptureImg));
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.pnRadio = new System.Windows.Forms.Panel();
            this.RBN = new System.Windows.Forms.RadioButton();
            this.RBE = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmdBrowseF = new System.Windows.Forms.Button();
            this.lblWIM = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.cboFlags = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.chkReArm = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.chkCheck = new System.Windows.Forms.CheckBox();
            this.gbVerify = new System.Windows.Forms.GroupBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.chkVerify = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.cmdRPFixHelp = new System.Windows.Forms.Button();
            this.chkRPFix = new System.Windows.Forms.CheckBox();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.cboCompression = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cmdConfig = new System.Windows.Forms.Button();
            this.cboConfig = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdStart = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSepClean = new System.Windows.Forms.ToolStripSeparator();
            this.cmdCleanup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.BWCapture = new System.ComponentModel.BackgroundWorker();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.pnRadio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.gbVerify.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.GroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.ToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Location = new System.Drawing.Point(531, 17);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(63, 25);
            this.cmdBrowse.TabIndex = 3;
            this.cmdBrowse.Text = "Browse";
            this.cmdBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
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
            this.SplitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.ToolStrip1);
            this.SplitContainer1.Panel2MinSize = 30;
            this.SplitContainer1.Size = new System.Drawing.Size(622, 314);
            this.SplitContainer1.SplitterDistance = 283;
            this.SplitContainer1.SplitterWidth = 1;
            this.SplitContainer1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(622, 283);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GroupBox4);
            this.tabPage1.Controls.Add(this.GroupBox2);
            this.tabPage1.Controls.Add(this.GroupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(614, 257);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // GroupBox4
            // 
            this.GroupBox4.Controls.Add(this.pnRadio);
            this.GroupBox4.Controls.Add(this.pictureBox1);
            this.GroupBox4.Controls.Add(this.cmdBrowseF);
            this.GroupBox4.Controls.Add(this.lblWIM);
            this.GroupBox4.Controls.Add(this.Label7);
            this.GroupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox4.Location = new System.Drawing.Point(6, 6);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(600, 74);
            this.GroupBox4.TabIndex = 3;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Select a WIM File";
            // 
            // pnRadio
            // 
            this.pnRadio.Controls.Add(this.RBN);
            this.pnRadio.Controls.Add(this.RBE);
            this.pnRadio.Location = new System.Drawing.Point(65, 19);
            this.pnRadio.Name = "pnRadio";
            this.pnRadio.Size = new System.Drawing.Size(231, 28);
            this.pnRadio.TabIndex = 7;
            // 
            // RBN
            // 
            this.RBN.AutoSize = true;
            this.RBN.Checked = true;
            this.RBN.Location = new System.Drawing.Point(3, 2);
            this.RBN.Name = "RBN";
            this.RBN.Size = new System.Drawing.Size(78, 19);
            this.RBN.TabIndex = 4;
            this.RBN.TabStop = true;
            this.RBN.Text = "New WIM";
            this.RBN.UseVisualStyleBackColor = true;
            this.RBN.Click += new System.EventHandler(this.RBN_Click);
            // 
            // RBE
            // 
            this.RBE.AutoSize = true;
            this.RBE.Location = new System.Drawing.Point(98, 2);
            this.RBE.Name = "RBE";
            this.RBE.Size = new System.Drawing.Size(96, 19);
            this.RBE.TabIndex = 5;
            this.RBE.Text = "Existing WIM";
            this.RBE.UseVisualStyleBackColor = true;
            this.RBE.Click += new System.EventHandler(this.RBE_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // cmdBrowseF
            // 
            this.cmdBrowseF.Location = new System.Drawing.Point(531, 11);
            this.cmdBrowseF.Name = "cmdBrowseF";
            this.cmdBrowseF.Size = new System.Drawing.Size(63, 25);
            this.cmdBrowseF.TabIndex = 3;
            this.cmdBrowseF.Text = "Save";
            this.cmdBrowseF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdBrowseF.UseVisualStyleBackColor = true;
            this.cmdBrowseF.Click += new System.EventHandler(this.cmdBrowseF_Click);
            // 
            // lblWIM
            // 
            this.lblWIM.AutoEllipsis = true;
            this.lblWIM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWIM.Location = new System.Drawing.Point(99, 46);
            this.lblWIM.Name = "lblWIM";
            this.lblWIM.Size = new System.Drawing.Size(493, 25);
            this.lblWIM.TabIndex = 2;
            this.lblWIM.Text = "Save a new WIM...";
            this.lblWIM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(62, 50);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(35, 15);
            this.Label7.TabIndex = 0;
            this.Label7.Text = "WIM:";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.pictureBox3);
            this.GroupBox2.Controls.Add(this.cboFlags);
            this.GroupBox2.Controls.Add(this.Label5);
            this.GroupBox2.Controls.Add(this.Label3);
            this.GroupBox2.Controls.Add(this.txtDesc);
            this.GroupBox2.Controls.Add(this.Label2);
            this.GroupBox2.Controls.Add(this.txtName);
            this.GroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox2.Location = new System.Drawing.Point(5, 156);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(600, 90);
            this.GroupBox2.TabIndex = 1;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Info";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(6, 23);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(48, 48);
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // cboFlags
            // 
            this.cboFlags.FormattingEnabled = true;
            this.cboFlags.Items.AddRange(new object[] {
            "None",
            "Business",
            "Core",
            "CoreConnected",
            "Education",
            "Enterprise",
            "HomeBasic",
            "HomePremium",
            "Professional",
            "ProfessionalWMC",
            "ServerDatacenter",
            "ServerEnterprise",
            "ServerStandard",
            "Starter",
            "Ultimate",
            "CoreSingleLanguage",
            "ProfessionalEducation",
            "ProfessionalWorkstation",
            "ProfessionalCountrySpecific",
            "ProfessionalSingleLanguage",
            "ServerRdsh"});
            this.cboFlags.Location = new System.Drawing.Point(191, 62);
            this.cboFlags.Name = "cboFlags";
            this.cboFlags.Size = new System.Drawing.Size(401, 23);
            this.cboFlags.TabIndex = 5;
            this.cboFlags.Text = "None";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(62, 65);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(40, 15);
            this.Label5.TabIndex = 4;
            this.Label5.Text = "Flags:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(62, 40);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(110, 15);
            this.Label3.TabIndex = 3;
            this.Label3.Text = "Image Description:";
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.SystemColors.Control;
            this.txtDesc.Location = new System.Drawing.Point(191, 37);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(401, 21);
            this.txtDesc.TabIndex = 2;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(61, 16);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(82, 15);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Image Name:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.Control;
            this.txtName.Location = new System.Drawing.Point(191, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(401, 21);
            this.txtName.TabIndex = 0;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.label6);
            this.GroupBox1.Controls.Add(this.pictureBox5);
            this.GroupBox1.Controls.Add(this.cmdBrowse);
            this.GroupBox1.Controls.Add(this.lblFolder);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox1.Location = new System.Drawing.Point(6, 83);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(600, 70);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Folder to Capture";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(56, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(229, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "TIP: Select the folder you wish to capture.";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(6, 19);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(48, 48);
            this.pictureBox5.TabIndex = 7;
            this.pictureBox5.TabStop = false;
            // 
            // lblFolder
            // 
            this.lblFolder.AutoEllipsis = true;
            this.lblFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolder.Location = new System.Drawing.Point(101, 43);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(491, 24);
            this.lblFolder.TabIndex = 2;
            this.lblFolder.Text = "Select a folder...";
            this.lblFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(56, 47);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(45, 15);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Folder:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.gbVerify);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.GroupBox3);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(614, 254);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Options";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.pictureBox9);
            this.groupBox7.Controls.Add(this.chkReArm);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(428, 86);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(134, 50);
            this.groupBox7.TabIndex = 12;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Reset Re-Arm";
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.Location = new System.Drawing.Point(6, 18);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(25, 25);
            this.pictureBox9.TabIndex = 9;
            this.pictureBox9.TabStop = false;
            // 
            // chkReArm
            // 
            this.chkReArm.AutoSize = true;
            this.chkReArm.Enabled = false;
            this.chkReArm.Location = new System.Drawing.Point(37, 21);
            this.chkReArm.Name = "chkReArm";
            this.chkReArm.Size = new System.Drawing.Size(65, 19);
            this.chkReArm.TabIndex = 9;
            this.chkReArm.Text = "Enable";
            this.chkReArm.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.pictureBox8);
            this.groupBox8.Controls.Add(this.chkCheck);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(8, 86);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(134, 50);
            this.groupBox8.TabIndex = 12;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Check";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(6, 18);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(25, 25);
            this.pictureBox8.TabIndex = 9;
            this.pictureBox8.TabStop = false;
            // 
            // chkCheck
            // 
            this.chkCheck.AutoSize = true;
            this.chkCheck.Location = new System.Drawing.Point(37, 21);
            this.chkCheck.Name = "chkCheck";
            this.chkCheck.Size = new System.Drawing.Size(65, 19);
            this.chkCheck.TabIndex = 9;
            this.chkCheck.Text = "Enable";
            this.chkCheck.UseVisualStyleBackColor = true;
            // 
            // gbVerify
            // 
            this.gbVerify.Controls.Add(this.pictureBox7);
            this.gbVerify.Controls.Add(this.chkVerify);
            this.gbVerify.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbVerify.Location = new System.Drawing.Point(148, 86);
            this.gbVerify.Name = "gbVerify";
            this.gbVerify.Size = new System.Drawing.Size(134, 50);
            this.gbVerify.TabIndex = 12;
            this.gbVerify.TabStop = false;
            this.gbVerify.Text = "Verify";
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(6, 18);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(25, 25);
            this.pictureBox7.TabIndex = 9;
            this.pictureBox7.TabStop = false;
            // 
            // chkVerify
            // 
            this.chkVerify.AutoSize = true;
            this.chkVerify.Location = new System.Drawing.Point(37, 21);
            this.chkVerify.Name = "chkVerify";
            this.chkVerify.Size = new System.Drawing.Size(65, 19);
            this.chkVerify.TabIndex = 9;
            this.chkVerify.Text = "Enable";
            this.chkVerify.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.pictureBox6);
            this.groupBox6.Controls.Add(this.cmdRPFixHelp);
            this.groupBox6.Controls.Add(this.chkRPFix);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(288, 86);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(134, 50);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "NoRPFix";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(6, 18);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(25, 25);
            this.pictureBox6.TabIndex = 9;
            this.pictureBox6.TabStop = false;
            // 
            // cmdRPFixHelp
            // 
            this.cmdRPFixHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdRPFixHelp.BackgroundImage")));
            this.cmdRPFixHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdRPFixHelp.Location = new System.Drawing.Point(108, 20);
            this.cmdRPFixHelp.Name = "cmdRPFixHelp";
            this.cmdRPFixHelp.Size = new System.Drawing.Size(22, 22);
            this.cmdRPFixHelp.TabIndex = 11;
            this.cmdRPFixHelp.UseVisualStyleBackColor = true;
            this.cmdRPFixHelp.Click += new System.EventHandler(this.cmdRPFixHelp_Click);
            // 
            // chkRPFix
            // 
            this.chkRPFix.AutoSize = true;
            this.chkRPFix.Location = new System.Drawing.Point(37, 21);
            this.chkRPFix.Name = "chkRPFix";
            this.chkRPFix.Size = new System.Drawing.Size(65, 19);
            this.chkRPFix.TabIndex = 9;
            this.chkRPFix.Text = "Enable";
            this.chkRPFix.UseVisualStyleBackColor = true;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.pictureBox4);
            this.GroupBox3.Controls.Add(this.cboCompression);
            this.GroupBox3.Controls.Add(this.Label4);
            this.GroupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox3.Location = new System.Drawing.Point(8, 6);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(256, 74);
            this.GroupBox3.TabIndex = 2;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "Compression";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(6, 20);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(48, 48);
            this.pictureBox4.TabIndex = 9;
            this.pictureBox4.TabStop = false;
            // 
            // cboCompression
            // 
            this.cboCompression.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompression.FormattingEnabled = true;
            this.cboCompression.Items.AddRange(new object[] {
            "None",
            "Fast",
            "Maximum"});
            this.cboCompression.Location = new System.Drawing.Point(64, 45);
            this.cboCompression.Name = "cboCompression";
            this.cboCompression.Size = new System.Drawing.Size(187, 23);
            this.cboCompression.TabIndex = 1;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(60, 20);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(128, 15);
            this.Label4.TabIndex = 0;
            this.Label4.Text = "Compression Method:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cmdConfig);
            this.groupBox5.Controls.Add(this.cboConfig);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.pictureBox2);
            this.groupBox5.Location = new System.Drawing.Point(268, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(338, 74);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Config";
            // 
            // cmdConfig
            // 
            this.cmdConfig.Location = new System.Drawing.Point(269, 44);
            this.cmdConfig.Name = "cmdConfig";
            this.cmdConfig.Size = new System.Drawing.Size(63, 25);
            this.cmdConfig.TabIndex = 9;
            this.cmdConfig.Text = "Browse";
            this.cmdConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdConfig.UseVisualStyleBackColor = true;
            this.cmdConfig.Click += new System.EventHandler(this.cmdConfig_Click);
            // 
            // cboConfig
            // 
            this.cboConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConfig.FormattingEnabled = true;
            this.cboConfig.Items.AddRange(new object[] {
            "None"});
            this.cboConfig.Location = new System.Drawing.Point(63, 47);
            this.cboConfig.Name = "cboConfig";
            this.cboConfig.Size = new System.Drawing.Size(199, 21);
            this.cboConfig.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(60, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Custom Configuration:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(6, 20);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.CanOverflow = false;
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdStart,
            this.tsSepClean,
            this.cmdCleanup,
            this.toolStripSeparator1,
            this.lblStatus});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new System.Drawing.Size(622, 30);
            this.ToolStrip1.Stretch = true;
            this.ToolStrip1.TabIndex = 17;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // cmdStart
            // 
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(51, 30);
            this.cmdStart.Tag = "OK";
            this.cmdStart.Text = "Start";
            this.cmdStart.ToolTipText = "Start Capturing!";
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // tsSepClean
            // 
            this.tsSepClean.Name = "tsSepClean";
            this.tsSepClean.Size = new System.Drawing.Size(6, 30);
            // 
            // cmdCleanup
            // 
            this.cmdCleanup.Image = ((System.Drawing.Image)(resources.GetObject("cmdCleanup.Image")));
            this.cmdCleanup.Name = "cmdCleanup";
            this.cmdCleanup.Size = new System.Drawing.Size(152, 30);
            this.cmdCleanup.Tag = "";
            this.cmdCleanup.Text = "Cleanup Manager";
            this.cmdCleanup.ToolTipText = "Try to reduce image size.";
            this.cmdCleanup.Click += new System.EventHandler(this.cmdCleanup_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 27);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "File_icon.png");
            this.imageList1.Images.SetKeyName(1, "folder_closed.png");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "star_32.png");
            this.imageList2.Images.SetKeyName(1, "Gear.png");
            // 
            // BWCapture
            // 
            this.BWCapture.WorkerSupportsCancellation = true;
            this.BWCapture.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWCapture_DoWork);
            this.BWCapture.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BWCapture_RunWorkerCompleted);
            // 
            // frmCaptureImg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(622, 314);
            this.Controls.Add(this.SplitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(638, 353);
            this.Name = "frmCaptureImg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Capture Image";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCaptureImg_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCaptureImg_FormClosed);
            this.Load += new System.EventHandler(this.frmCaptureImg_Load);
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.Panel2.PerformLayout();
            this.SplitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.GroupBox4.ResumeLayout(false);
            this.GroupBox4.PerformLayout();
            this.pnRadio.ResumeLayout(false);
            this.pnRadio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.gbVerify.ResumeLayout(false);
            this.gbVerify.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button cmdBrowse;
        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal System.Windows.Forms.GroupBox GroupBox4;
        internal System.Windows.Forms.RadioButton RBE;
        internal System.Windows.Forms.RadioButton RBN;
        internal System.Windows.Forms.Button cmdBrowseF;
        internal System.Windows.Forms.Label lblWIM;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.ComboBox cboCompression;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.ComboBox cboFlags;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtDesc;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Label lblFolder;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem cmdStart;
        internal System.ComponentModel.BackgroundWorker BWCapture;
        internal System.Windows.Forms.ToolStripLabel lblStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.PictureBox pictureBox2;
        internal System.Windows.Forms.Button cmdConfig;
        private System.Windows.Forms.ComboBox cboConfig;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator tsSepClean;
        internal System.Windows.Forms.ToolStripMenuItem cmdCleanup;
        private System.Windows.Forms.CheckBox chkRPFix;
        private System.Windows.Forms.Button cmdRPFixHelp;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        internal System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.PictureBox pictureBox6;
        internal System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.CheckBox chkCheck;
        internal System.Windows.Forms.GroupBox gbVerify;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.CheckBox chkVerify;
        private System.Windows.Forms.Panel pnRadio;
        internal System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.CheckBox chkReArm;
    }
}