namespace WinToolkit
{
    partial class frmMSUtoCAB
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
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdSC = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdAU = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdR = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdRU = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdRA = new System.Windows.Forms.ToolStripMenuItem();
            this.lstMSU = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PB = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.BWConvert = new System.ComponentModel.BackgroundWorker();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            this.SplitContainer2.Panel1.SuspendLayout();
            this.SplitContainer2.Panel2.SuspendLayout();
            this.SplitContainer2.SuspendLayout();
            this.StatusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer1.IsSplitterFixed = true;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer1.Name = "SplitContainer1";
            this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.ToolStrip1);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.lstMSU);
            this.SplitContainer1.Size = new System.Drawing.Size(734, 335);
            this.SplitContainer1.SplitterDistance = 25;
            this.SplitContainer1.SplitterWidth = 1;
            this.SplitContainer1.TabIndex = 0;
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdSC,
            this.cmdAU,
            this.cmdR});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new System.Drawing.Size(734, 25);
            this.ToolStrip1.TabIndex = 0;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // cmdSC
            // 
            this.cmdSC.Name = "cmdSC";
            this.cmdSC.Size = new System.Drawing.Size(43, 25);
            this.cmdSC.Tag = "OK";
            this.cmdSC.Text = "Start";
            this.cmdSC.ToolTipText = "Start converting all (.msu) to (.cab)";
            this.cmdSC.Click += new System.EventHandler(this.cmdSC_Click);
            // 
            // cmdAU
            // 
            this.cmdAU.Name = "cmdAU";
            this.cmdAU.Size = new System.Drawing.Size(87, 25);
            this.cmdAU.Tag = "Add";
            this.cmdAU.Text = "Add Updates";
            this.cmdAU.ToolTipText = "Add Updates to convert";
            this.cmdAU.Click += new System.EventHandler(this.cmdAU_Click);
            // 
            // cmdR
            // 
            this.cmdR.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdRU,
            this.cmdRA});
            this.cmdR.Name = "cmdR";
            this.cmdR.Size = new System.Drawing.Size(71, 25);
            this.cmdR.Tag = "Remove";
            this.cmdR.Text = "Remove...";
            this.cmdR.ToolTipText = "Remove...";
            // 
            // cmdRU
            // 
            this.cmdRU.Name = "cmdRU";
            this.cmdRU.Size = new System.Drawing.Size(164, 22);
            this.cmdRU.Tag = "Remove";
            this.cmdRU.Text = "Remove Selected";
            this.cmdRU.ToolTipText = "Remove all selected files from the list";
            this.cmdRU.Click += new System.EventHandler(this.cmdRU_Click);
            // 
            // cmdRA
            // 
            this.cmdRA.Name = "cmdRA";
            this.cmdRA.Size = new System.Drawing.Size(164, 22);
            this.cmdRA.Tag = "Remove";
            this.cmdRA.Text = "Remove All";
            this.cmdRA.ToolTipText = "Remove all files from the list";
            this.cmdRA.Click += new System.EventHandler(this.cmdRA_Click);
            // 
            // lstMSU
            // 
            this.lstMSU.AllowDrop = true;
            this.lstMSU.BackColor = System.Drawing.SystemColors.Window;
            this.lstMSU.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstMSU.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader3,
            this.ColumnHeader4,
            this.ColumnHeader5,
            this.ColumnHeader6});
            this.lstMSU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMSU.FullRowSelect = true;
            this.lstMSU.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstMSU.Location = new System.Drawing.Point(0, 0);
            this.lstMSU.Name = "lstMSU";
            this.lstMSU.Size = new System.Drawing.Size(734, 309);
            this.lstMSU.TabIndex = 1;
            this.lstMSU.UseCompatibleStateImageBehavior = false;
            this.lstMSU.View = System.Windows.Forms.View.Details;
            this.lstMSU.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstMSU_DragDrop);
            this.lstMSU.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstMSU_DragEnter);
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Name";
            this.ColumnHeader1.Width = 147;
            // 
            // ColumnHeader3
            // 
            this.ColumnHeader3.Text = "Size";
            this.ColumnHeader3.Width = 69;
            // 
            // ColumnHeader4
            // 
            this.ColumnHeader4.Text = "Architecture";
            this.ColumnHeader4.Width = 71;
            // 
            // ColumnHeader5
            // 
            this.ColumnHeader5.Text = "Location";
            this.ColumnHeader5.Width = 74;
            // 
            // ColumnHeader6
            // 
            this.ColumnHeader6.Text = "MD5";
            // 
            // SplitContainer2
            // 
            this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer2.Name = "SplitContainer2";
            this.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer2.Panel1
            // 
            this.SplitContainer2.Panel1.Controls.Add(this.SplitContainer1);
            // 
            // SplitContainer2.Panel2
            // 
            this.SplitContainer2.Panel2.Controls.Add(this.StatusStrip1);
            this.SplitContainer2.Size = new System.Drawing.Size(734, 361);
            this.SplitContainer2.SplitterDistance = 335;
            this.SplitContainer2.SplitterWidth = 1;
            this.SplitContainer2.TabIndex = 2;
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PB,
            this.lblStatus});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 0);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(734, 25);
            this.StatusStrip1.SizingGrip = false;
            this.StatusStrip1.TabIndex = 0;
            this.StatusStrip1.Text = "StatusStrip1";
            // 
            // PB
            // 
            this.PB.Name = "PB";
            this.PB.Size = new System.Drawing.Size(100, 19);
            this.PB.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(150, 20);
            this.lblStatus.Text ="Please add some .MSU files";
            // 
            // BWConvert
            // 
            this.BWConvert.WorkerSupportsCancellation = true;
            this.BWConvert.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BWConvert_DoWork);
            // 
            // frmMSUtoCAB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 361);
            this.Controls.Add(this.SplitContainer2);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(750, 400);
            this.Name = "frmMSUtoCAB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MSU to CAB Converter";
            this.Load += new System.EventHandler(this.frmMSUtoCAB_Load);
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel1.PerformLayout();
            this.SplitContainer1.Panel2.ResumeLayout(false);
            this.SplitContainer1.ResumeLayout(false);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.SplitContainer2.Panel1.ResumeLayout(false);
            this.SplitContainer2.Panel2.ResumeLayout(false);
            this.SplitContainer2.Panel2.PerformLayout();
            this.SplitContainer2.ResumeLayout(false);
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem cmdSC;
        internal System.Windows.Forms.ToolStripMenuItem cmdAU;
        internal System.Windows.Forms.ToolStripMenuItem cmdR;
        internal System.Windows.Forms.ToolStripMenuItem cmdRU;
        internal System.Windows.Forms.ToolStripMenuItem cmdRA;
        internal System.Windows.Forms.ListView lstMSU;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.ColumnHeader ColumnHeader3;
        internal System.Windows.Forms.ColumnHeader ColumnHeader4;
        internal System.Windows.Forms.ColumnHeader ColumnHeader5;
        internal System.Windows.Forms.ColumnHeader ColumnHeader6;
        internal System.Windows.Forms.SplitContainer SplitContainer2;
        internal System.Windows.Forms.StatusStrip StatusStrip1;
        internal System.Windows.Forms.ToolStripProgressBar PB;
        internal System.Windows.Forms.ToolStripStatusLabel lblStatus;
        internal System.ComponentModel.BackgroundWorker BWConvert;
    }
}