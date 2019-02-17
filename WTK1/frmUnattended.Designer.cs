using System.Windows.Forms;

namespace WinToolkit
{
    partial class frmUnattendedCreator
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("General", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Display", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("OOBE", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Users", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Misc", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("OEM", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("AutoLogon", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Accept EULA",
            ""}, 2);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Admin Password",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Colour Depth",
            ""}, 1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Company Name",
            "Home"}, 0);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "Computer Name",
            ""}, 1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "Hide Wireless Setup",
            ""}, 2);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "Logo",
            ""}, 4);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "Logon Count",
            ""}, 5);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "Manufacturer",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "Model",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
            "Network Location",
            ""}, 1);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
            "Profile Directory",
            ""}, 3);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
            "ProgramData Folder",
            ""}, 3);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
            "Refresh Rate",
            ""}, 1);
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
            "Screen Resolution",
            ""}, 1);
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(new string[] {
            "Serial",
            "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"}, 1);
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem(new string[] {
            "Skip Auto-Activation",
            ""}, 2);
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem(new string[] {
            "Support Hours",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem(new string[] {
            "Support Phone",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem(new string[] {
            "Support URL",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem(new string[] {
            "Time Zone",
            ""}, 1);
            System.Windows.Forms.ListViewItem listViewItem22 = new System.Windows.Forms.ListViewItem(new string[] {
            "Updates",
            ""}, 1);
            System.Windows.Forms.ListViewItem listViewItem23 = new System.Windows.Forms.ListViewItem(new string[] {
            "Username",
            ""}, 1);
            System.Windows.Forms.ListViewItem listViewItem24 = new System.Windows.Forms.ListViewItem(new string[] {
            "Your Full Name",
            ""}, 0);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUnattendedCreator));
            this.PanRInfo = new System.Windows.Forms.Panel();
            this.ToolStrip5 = new System.Windows.Forms.ToolStrip();
            this.cmdROK = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdRCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.GBRInfo = new System.Windows.Forms.GroupBox();
            this.cboRChoice = new System.Windows.Forms.ComboBox();
            this.cmdRBrowse = new System.Windows.Forms.Button();
            this.txtRInfo = new System.Windows.Forms.TextBox();
            this.lstUnattended = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListItems = new System.Windows.Forms.ImageList(this.components);
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.SCUsers = new System.Windows.Forms.SplitContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.cmdUNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmdUDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panUser = new System.Windows.Forms.Panel();
            this.txtUDescription = new System.Windows.Forms.TextBox();
            this.txtUPassword = new System.Windows.Forms.TextBox();
            this.txtUDisplay = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUUsername = new System.Windows.Forms.TextBox();
            this.cboUAccount = new System.Windows.Forms.ComboBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.cmdUOK = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdUCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.lstUsers = new WinToolkit.ListViewEx();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabEditor = new System.Windows.Forms.TabPage();
            this.txtUnattended = new System.Windows.Forms.TextBox();
            this.imageListTabs = new System.Windows.Forms.ImageList(this.components);
            this.PanRInfo.SuspendLayout();
            this.ToolStrip5.SuspendLayout();
            this.GBRInfo.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabUsers.SuspendLayout();
            this.SCUsers.Panel1.SuspendLayout();
            this.SCUsers.Panel2.SuspendLayout();
            this.SCUsers.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panUser.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tabEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanRInfo
            // 
            this.PanRInfo.BackColor = System.Drawing.SystemColors.Window;
            this.PanRInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanRInfo.Controls.Add(this.ToolStrip5);
            this.PanRInfo.Controls.Add(this.GBRInfo);
            this.PanRInfo.Location = new System.Drawing.Point(141, 157);
            this.PanRInfo.Name = "PanRInfo";
            this.PanRInfo.Size = new System.Drawing.Size(365, 84);
            this.PanRInfo.TabIndex = 2;
            this.PanRInfo.Visible = false;
            // 
            // ToolStrip5
            // 
            this.ToolStrip5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ToolStrip5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdROK,
            this.cmdRCancel});
            this.ToolStrip5.Location = new System.Drawing.Point(0, 57);
            this.ToolStrip5.Name = "ToolStrip5";
            this.ToolStrip5.Size = new System.Drawing.Size(363, 25);
            this.ToolStrip5.TabIndex = 13;
            this.ToolStrip5.Text = "toolStrip4";
            // 
            // cmdROK
            // 
            this.cmdROK.Image = global::WinToolkit.Properties.Resources.OK;
            this.cmdROK.Name = "cmdROK";
            this.cmdROK.Size = new System.Drawing.Size(51, 25);
            this.cmdROK.Tag = "OK";
            this.cmdROK.Text = "OK";
            this.cmdROK.ToolTipText = "Accept new setting.";
            this.cmdROK.Click += new System.EventHandler(this.cmdROK_Click);
            // 
            // cmdRCancel
            // 
            this.cmdRCancel.Image = global::WinToolkit.Properties.Resources.Close;
            this.cmdRCancel.Name = "cmdRCancel";
            this.cmdRCancel.Size = new System.Drawing.Size(71, 25);
            this.cmdRCancel.Tag = "Cancel";
            this.cmdRCancel.Text = "Cancel";
            this.cmdRCancel.ToolTipText = "Cancel Task";
            this.cmdRCancel.Click += new System.EventHandler(this.cmdRCancel_Click);
            // 
            // GBRInfo
            // 
            this.GBRInfo.BackColor = System.Drawing.SystemColors.Window;
            this.GBRInfo.Controls.Add(this.cboRChoice);
            this.GBRInfo.Controls.Add(this.cmdRBrowse);
            this.GBRInfo.Controls.Add(this.txtRInfo);
            this.GBRInfo.Location = new System.Drawing.Point(3, 7);
            this.GBRInfo.Name = "GBRInfo";
            this.GBRInfo.Size = new System.Drawing.Size(354, 47);
            this.GBRInfo.TabIndex = 12;
            this.GBRInfo.TabStop = false;
            // 
            // cboRChoice
            // 
            this.cboRChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRChoice.FormattingEnabled = true;
            this.cboRChoice.Items.AddRange(new object[] {
            "Windows Default",
            "Dark Text Shadows and Light Buttons",
            "No Shadows and Opaque Buttons"});
            this.cboRChoice.Location = new System.Drawing.Point(6, 18);
            this.cboRChoice.Name = "cboRChoice";
            this.cboRChoice.Size = new System.Drawing.Size(318, 21);
            this.cboRChoice.TabIndex = 18;
            this.cboRChoice.Visible = false;
            // 
            // cmdRBrowse
            // 
            this.cmdRBrowse.Location = new System.Drawing.Point(330, 19);
            this.cmdRBrowse.Name = "cmdRBrowse";
            this.cmdRBrowse.Size = new System.Drawing.Size(18, 18);
            this.cmdRBrowse.TabIndex = 17;
            this.cmdRBrowse.UseVisualStyleBackColor = true;
            this.cmdRBrowse.Click += new System.EventHandler(this.cmdRBrowse_Click);
            // 
            // txtRInfo
            // 
            this.txtRInfo.Enabled = false;
            this.txtRInfo.Location = new System.Drawing.Point(6, 19);
            this.txtRInfo.Name = "txtRInfo";
            this.txtRInfo.Size = new System.Drawing.Size(276, 20);
            this.txtRInfo.TabIndex = 0;
            // 
            // lstUnattended
            // 
            this.lstUnattended.CheckBoxes = true;
            this.lstUnattended.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstUnattended.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUnattended.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstUnattended.FullRowSelect = true;
            this.lstUnattended.GridLines = true;
            listViewGroup1.Header = "General";
            listViewGroup1.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup1.Name = "LVG";
            listViewGroup2.Header = "Display";
            listViewGroup2.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup2.Name = "LVEDisplay";
            listViewGroup3.Header = "OOBE";
            listViewGroup3.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup3.Name = "LVGOOBE";
            listViewGroup4.Header = "Users";
            listViewGroup4.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup4.Name = "LVGUsers";
            listViewGroup5.Header = "Misc";
            listViewGroup5.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup5.Name = "LVGMisc";
            listViewGroup6.Header = "OEM";
            listViewGroup6.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup6.Name = "LVGOEM";
            listViewGroup7.Header = "AutoLogon";
            listViewGroup7.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup7.Name = "LVGAutoLogon";
            this.lstUnattended.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4,
            listViewGroup5,
            listViewGroup6,
            listViewGroup7});
            this.lstUnattended.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem1.Group = listViewGroup1;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.Group = listViewGroup1;
            listViewItem2.StateImageIndex = 0;
            listViewItem2.Tag = "Text";
            listViewItem2.ToolTipText = "Type the default administrator password.";
            listViewItem3.Group = listViewGroup2;
            listViewItem3.StateImageIndex = 0;
            listViewItem3.Tag = "List";
            listViewItem4.Group = listViewGroup1;
            listViewItem4.StateImageIndex = 0;
            listViewItem4.Tag = "Text";
            listViewItem5.Group = listViewGroup1;
            listViewItem5.StateImageIndex = 0;
            listViewItem5.Tag = "Text";
            listViewItem6.Group = listViewGroup3;
            listViewItem6.StateImageIndex = 0;
            listViewItem7.Group = listViewGroup6;
            listViewItem7.StateImageIndex = 0;
            listViewItem7.Tag = "Text";
            listViewItem8.Group = listViewGroup7;
            listViewItem8.StateImageIndex = 0;
            listViewItem8.Tag = "Number";
            listViewItem9.Group = listViewGroup6;
            listViewItem9.StateImageIndex = 0;
            listViewItem9.Tag = "Text";
            listViewItem10.Group = listViewGroup6;
            listViewItem10.StateImageIndex = 0;
            listViewItem10.Tag = "Text";
            listViewItem11.Group = listViewGroup3;
            listViewItem11.StateImageIndex = 0;
            listViewItem11.Tag = "List";
            listViewItem12.Group = listViewGroup5;
            listViewItem12.StateImageIndex = 0;
            listViewItem12.Tag = "Text";
            listViewItem12.ToolTipText = "This will change the directory where the profiles will go (e.g. C:\\Users\\*)";
            listViewItem13.Group = listViewGroup5;
            listViewItem13.StateImageIndex = 0;
            listViewItem13.Tag = "Text";
            listViewItem13.ToolTipText = "This will change the \'ProgramData\' directory to anything of your choice (default:" +
    " C:\\ProgramData)";
            listViewItem14.Group = listViewGroup2;
            listViewItem14.StateImageIndex = 0;
            listViewItem14.Tag = "List";
            listViewItem15.Group = listViewGroup2;
            listViewItem15.StateImageIndex = 0;
            listViewItem15.Tag = "List";
            listViewItem16.Group = listViewGroup1;
            listViewItem16.StateImageIndex = 0;
            listViewItem16.Tag = "Custom";
            listViewItem17.Group = listViewGroup3;
            listViewItem17.StateImageIndex = 0;
            listViewItem18.Group = listViewGroup6;
            listViewItem18.StateImageIndex = 0;
            listViewItem18.Tag = "Text";
            listViewItem19.Group = listViewGroup6;
            listViewItem19.StateImageIndex = 0;
            listViewItem19.Tag = "Text";
            listViewItem20.Group = listViewGroup6;
            listViewItem20.StateImageIndex = 0;
            listViewItem20.Tag = "Text";
            listViewItem21.Group = listViewGroup3;
            listViewItem21.StateImageIndex = 0;
            listViewItem21.Tag = "List";
            listViewItem22.Group = listViewGroup3;
            listViewItem22.StateImageIndex = 0;
            listViewItem22.Tag = "List";
            listViewItem23.Group = listViewGroup7;
            listViewItem23.StateImageIndex = 0;
            listViewItem23.Tag = "List";
            listViewItem24.Group = listViewGroup1;
            listViewItem24.StateImageIndex = 0;
            listViewItem24.Tag = "Text";
            this.lstUnattended.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17,
            listViewItem18,
            listViewItem19,
            listViewItem20,
            listViewItem21,
            listViewItem22,
            listViewItem23,
            listViewItem24});
            this.lstUnattended.Location = new System.Drawing.Point(0, 0);
            this.lstUnattended.MultiSelect = false;
            this.lstUnattended.Name = "lstUnattended";
            this.lstUnattended.ShowItemToolTips = true;
            this.lstUnattended.Size = new System.Drawing.Size(676, 387);
            this.lstUnattended.SmallImageList = this.imageListItems;
            this.lstUnattended.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstUnattended.TabIndex = 0;
            this.lstUnattended.UseCompatibleStateImageBehavior = false;
            this.lstUnattended.View = System.Windows.Forms.View.Details;
            this.lstUnattended.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstUnattended_ItemChecked);
            this.lstUnattended.SelectedIndexChanged += new System.EventHandler(this.lstUnattended_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item";
            this.columnHeader1.Width = 152;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            this.columnHeader2.Width = 265;
            // 
            // imageListItems
            // 
            this.imageListItems.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListItems.ImageStream")));
            this.imageListItems.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListItems.Images.SetKeyName(0, "edit.png");
            this.imageListItems.Images.SetKeyName(1, "ListBox_16x16.png");
            this.imageListItems.Images.SetKeyName(2, "tick (1).png");
            this.imageListItems.Images.SetKeyName(3, "folder_closed.png");
            this.imageListItems.Images.SetKeyName(4, "File_icon.png");
            this.imageListItems.Images.SetKeyName(5, "Char - Number.png");
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCreate,
            this.cmdLoad,
            this.lblStatus});
            this.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.Size = new System.Drawing.Size(684, 46);
            this.ToolStrip1.TabIndex = 17;
            this.ToolStrip1.TabStop = true;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // mnuCreate
            // 
            this.mnuCreate.Image = ((System.Drawing.Image)(resources.GetObject("mnuCreate.Image")));
            this.mnuCreate.Name = "mnuCreate";
            this.mnuCreate.Size = new System.Drawing.Size(63, 46);
            this.mnuCreate.Text = "Save";
            this.mnuCreate.ToolTipText = "Create your unattended xml file!";
            this.mnuCreate.Click += new System.EventHandler(this.mnuCreate_Click);
            // 
            // cmdLoad
            // 
            this.cmdLoad.Image = ((System.Drawing.Image)(resources.GetObject("cmdLoad.Image")));
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(75, 46);
            this.cmdLoad.Text = "Import";
            this.cmdLoad.ToolTipText = "Import an unattended file";
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 43);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ToolStrip1);
            this.splitContainer2.Panel2MinSize = 30;
            this.splitContainer2.Size = new System.Drawing.Size(684, 461);
            this.splitContainer2.SplitterDistance = 414;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMain);
            this.tabControl1.Controls.Add(this.tabUsers);
            this.tabControl1.Controls.Add(this.tabEditor);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageListTabs;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(684, 414);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.PanRInfo);
            this.tabMain.Controls.Add(this.lstUnattended);
            this.tabMain.ImageIndex = 0;
            this.tabMain.Location = new System.Drawing.Point(4, 23);
            this.tabMain.Margin = new System.Windows.Forms.Padding(0);
            this.tabMain.Name = "tabMain";
            this.tabMain.Size = new System.Drawing.Size(676, 387);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // tabUsers
            // 
            this.tabUsers.Controls.Add(this.SCUsers);
            this.tabUsers.ImageIndex = 1;
            this.tabUsers.Location = new System.Drawing.Point(4, 23);
            this.tabUsers.Margin = new System.Windows.Forms.Padding(0);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Size = new System.Drawing.Size(676, 387);
            this.tabUsers.TabIndex = 2;
            this.tabUsers.Text = "Users";
            this.tabUsers.UseVisualStyleBackColor = true;
            // 
            // SCUsers
            // 
            this.SCUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SCUsers.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SCUsers.IsSplitterFixed = true;
            this.SCUsers.Location = new System.Drawing.Point(0, 0);
            this.SCUsers.Name = "SCUsers";
            this.SCUsers.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SCUsers.Panel1
            // 
            this.SCUsers.Panel1.Controls.Add(this.toolStrip2);
            // 
            // SCUsers.Panel2
            // 
            this.SCUsers.Panel2.Controls.Add(this.panUser);
            this.SCUsers.Panel2.Controls.Add(this.lstUsers);
            this.SCUsers.Size = new System.Drawing.Size(676, 387);
            this.SCUsers.SplitterDistance = 25;
            this.SCUsers.SplitterWidth = 1;
            this.SCUsers.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdUNew,
            this.toolStripLabel1,
            this.cmdUDelete});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip2.Size = new System.Drawing.Size(676, 25);
            this.toolStrip2.TabIndex = 18;
            this.toolStrip2.TabStop = true;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // cmdUNew
            // 
            this.cmdUNew.Image = global::WinToolkit.Properties.Resources.Add;
            this.cmdUNew.Name = "cmdUNew";
            this.cmdUNew.Size = new System.Drawing.Size(85, 25);
            this.cmdUNew.Text = "New User";
            this.cmdUNew.ToolTipText = "Create a new user.";
            this.cmdUNew.Click += new System.EventHandler(this.cmdUNew_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 22);
            // 
            // cmdUDelete
            // 
            this.cmdUDelete.Image = global::WinToolkit.Properties.Resources.Remove;
            this.cmdUDelete.Name = "cmdUDelete";
            this.cmdUDelete.Size = new System.Drawing.Size(94, 25);
            this.cmdUDelete.Text = "Delete User";
            this.cmdUDelete.ToolTipText = "Delete selected user.";
            this.cmdUDelete.Click += new System.EventHandler(this.cmdUDelete_Click);
            // 
            // panUser
            // 
            this.panUser.BackColor = System.Drawing.SystemColors.Window;
            this.panUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panUser.Controls.Add(this.txtUDescription);
            this.panUser.Controls.Add(this.txtUPassword);
            this.panUser.Controls.Add(this.txtUDisplay);
            this.panUser.Controls.Add(this.label6);
            this.panUser.Controls.Add(this.label5);
            this.panUser.Controls.Add(this.label4);
            this.panUser.Controls.Add(this.label3);
            this.panUser.Controls.Add(this.label2);
            this.panUser.Controls.Add(this.txtUUsername);
            this.panUser.Controls.Add(this.cboUAccount);
            this.panUser.Controls.Add(this.toolStrip3);
            this.panUser.Location = new System.Drawing.Point(118, 47);
            this.panUser.Name = "panUser";
            this.panUser.Size = new System.Drawing.Size(365, 173);
            this.panUser.TabIndex = 3;
            this.panUser.Visible = false;
            // 
            // txtUDescription
            // 
            this.txtUDescription.Location = new System.Drawing.Point(95, 120);
            this.txtUDescription.Name = "txtUDescription";
            this.txtUDescription.Size = new System.Drawing.Size(250, 20);
            this.txtUDescription.TabIndex = 4;
            // 
            // txtUPassword
            // 
            this.txtUPassword.Location = new System.Drawing.Point(95, 68);
            this.txtUPassword.Name = "txtUPassword";
            this.txtUPassword.Size = new System.Drawing.Size(250, 20);
            this.txtUPassword.TabIndex = 2;
            // 
            // txtUDisplay
            // 
            this.txtUDisplay.Location = new System.Drawing.Point(95, 42);
            this.txtUDisplay.Name = "txtUDisplay";
            this.txtUDisplay.Size = new System.Drawing.Size(250, 20);
            this.txtUDisplay.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Description:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Account Type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Display Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Username";
            // 
            // txtUUsername
            // 
            this.txtUUsername.Location = new System.Drawing.Point(95, 16);
            this.txtUUsername.Name = "txtUUsername";
            this.txtUUsername.Size = new System.Drawing.Size(250, 20);
            this.txtUUsername.TabIndex = 0;
            // 
            // cboUAccount
            // 
            this.cboUAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUAccount.FormattingEnabled = true;
            this.cboUAccount.Items.AddRange(new object[] {
            "Administrators",
            "Users"});
            this.cboUAccount.Location = new System.Drawing.Point(95, 94);
            this.cboUAccount.Name = "cboUAccount";
            this.cboUAccount.Size = new System.Drawing.Size(250, 21);
            this.cboUAccount.TabIndex = 3;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdUOK,
            this.cmdUCancel});
            this.toolStrip3.Location = new System.Drawing.Point(0, 146);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(363, 25);
            this.toolStrip3.TabIndex = 10;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // cmdUOK
            // 
            this.cmdUOK.Image = global::WinToolkit.Properties.Resources.OK;
            this.cmdUOK.Name = "cmdUOK";
            this.cmdUOK.Size = new System.Drawing.Size(51, 25);
            this.cmdUOK.Tag = "OK";
            this.cmdUOK.Text = "OK";
            this.cmdUOK.ToolTipText = "Add user.";
            this.cmdUOK.Click += new System.EventHandler(this.cmdUOK_Click);
            // 
            // cmdUCancel
            // 
            this.cmdUCancel.Image = global::WinToolkit.Properties.Resources.Close;
            this.cmdUCancel.Name = "cmdUCancel";
            this.cmdUCancel.Size = new System.Drawing.Size(71, 25);
            this.cmdUCancel.Tag = "Cancel";
            this.cmdUCancel.Text = "Cancel";
            this.cmdUCancel.ToolTipText = "Cancel user creation.";
            this.cmdUCancel.Click += new System.EventHandler(this.cmdUCancel_Click);
            // 
            // lstUsers
            // 
            this.lstUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lstUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUsers.FullRowSelect = true;
            this.lstUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstUsers.Location = new System.Drawing.Point(0, 0);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(676, 361);
            this.lstUsers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstUsers.TabIndex = 0;
            this.lstUsers.UseCompatibleStateImageBehavior = false;
            this.lstUsers.View = System.Windows.Forms.View.Details;
            this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.lstUsers_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Username";
            this.columnHeader3.Width = 94;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Display Name";
            this.columnHeader4.Width = 138;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Password";
            this.columnHeader5.Width = 127;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Account Type";
            this.columnHeader6.Width = 138;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Description";
            this.columnHeader7.Width = 145;
            // 
            // tabEditor
            // 
            this.tabEditor.Controls.Add(this.txtUnattended);
            this.tabEditor.ImageIndex = 2;
            this.tabEditor.Location = new System.Drawing.Point(4, 23);
            this.tabEditor.Margin = new System.Windows.Forms.Padding(0);
            this.tabEditor.Name = "tabEditor";
            this.tabEditor.Size = new System.Drawing.Size(676, 387);
            this.tabEditor.TabIndex = 1;
            this.tabEditor.Text = "Editor";
            this.tabEditor.UseVisualStyleBackColor = true;
            // 
            // txtUnattended
            // 
            this.txtUnattended.BackColor = System.Drawing.Color.White;
            this.txtUnattended.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUnattended.Location = new System.Drawing.Point(0, 0);
            this.txtUnattended.Multiline = true;
            this.txtUnattended.Name = "txtUnattended";
            this.txtUnattended.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtUnattended.Size = new System.Drawing.Size(676, 387);
            this.txtUnattended.TabIndex = 0;
            this.txtUnattended.Text = "Please select options from the other tabs.";
            this.txtUnattended.WordWrap = false;
            // 
            // imageListTabs
            // 
            this.imageListTabs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTabs.ImageStream")));
            this.imageListTabs.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTabs.Images.SetKeyName(0, "Favorites.png");
            this.imageListTabs.Images.SetKeyName(1, "User.png");
            this.imageListTabs.Images.SetKeyName(2, "edit_icon.png");
            // 
            // frmUnattendedCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.splitContainer2);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "frmUnattendedCreator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unattended Creator";
            this.Load += new System.EventHandler(this.frmUnattended_Load);
            this.PanRInfo.ResumeLayout(false);
            this.PanRInfo.PerformLayout();
            this.ToolStrip5.ResumeLayout(false);
            this.ToolStrip5.PerformLayout();
            this.GBRInfo.ResumeLayout(false);
            this.GBRInfo.PerformLayout();
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabUsers.ResumeLayout(false);
            this.SCUsers.Panel1.ResumeLayout(false);
            this.SCUsers.Panel1.PerformLayout();
            this.SCUsers.Panel2.ResumeLayout(false);
            this.SCUsers.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panUser.ResumeLayout(false);
            this.panUser.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabEditor.ResumeLayout(false);
            this.tabEditor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstUnattended;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem mnuCreate;
        private System.Windows.Forms.ToolStripLabel lblStatus;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox txtUnattended;
        internal System.Windows.Forms.ToolStripMenuItem cmdLoad;
        internal System.Windows.Forms.Panel PanRInfo;
        internal System.Windows.Forms.GroupBox GBRInfo;
        internal System.Windows.Forms.ComboBox cboRChoice;
        internal System.Windows.Forms.Button cmdRBrowse;
        internal System.Windows.Forms.TextBox txtRInfo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TabPage tabEditor;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.SplitContainer SCUsers;
        private ListViewEx lstUsers;
        internal System.Windows.Forms.ToolStrip toolStrip2;
        internal System.Windows.Forms.ToolStripMenuItem cmdUNew;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        internal System.Windows.Forms.ToolStripMenuItem cmdUDelete;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        internal Panel panUser;
        internal TextBox txtUDescription;
        internal TextBox txtUPassword;
        internal TextBox txtUDisplay;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        internal TextBox txtUUsername;
        internal ComboBox cboUAccount;
        internal ToolStrip toolStrip3;
        internal ToolStripMenuItem cmdUOK;
        internal ToolStripMenuItem cmdUCancel;
        internal ToolStrip ToolStrip5;
        internal ToolStripMenuItem cmdROK;
		  internal ToolStripMenuItem cmdRCancel;
		  private ImageList imageListTabs;
          private ImageList imageListItems;
    }
}