namespace WinToolkit.Prompts {
    partial class frmInput {
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.ToolStrip5 = new System.Windows.Forms.ToolStrip();
            this.cmdAccept = new System.Windows.Forms.ToolStripButton();
            this.cmdCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblRestrictions = new System.Windows.Forms.ToolStripLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.ToolStrip5.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ToolStrip5);
            this.splitContainer1.Size = new System.Drawing.Size(401, 129);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lblMessage);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtInput);
            this.splitContainer2.Panel2MinSize = 21;
            this.splitContainer2.Size = new System.Drawing.Size(401, 100);
            this.splitContainer2.SplitterDistance = 78;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Location = new System.Drawing.Point(0, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(401, 78);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "No message. If you are seeing this then please report it immediately. Thanks :)";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(0, 0);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(401, 20);
            this.txtInput.TabIndex = 0;
            this.txtInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtInput.TextChanged += new System.EventHandler(this.txtInput_TextChanged);
            this.txtInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInput_KeyPress);
            // 
            // ToolStrip5
            // 
            this.ToolStrip5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolStrip5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdAccept,
            this.cmdCancel,
            this.toolStripSeparator1,
            this.lblRestrictions});
            this.ToolStrip5.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip5.Name = "ToolStrip5";
            this.ToolStrip5.Size = new System.Drawing.Size(401, 28);
            this.ToolStrip5.TabIndex = 13;
            this.ToolStrip5.TabStop = true;
            this.ToolStrip5.Text = "ToolStrip5";
            // 
            // cmdAccept
            // 
            this.cmdAccept.Image = global::WinToolkit.Properties.Resources.OK;
            this.cmdAccept.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(43, 25);
            this.cmdAccept.Text = "OK";
            this.cmdAccept.ToolTipText = "Use the entered input";
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Image = global::WinToolkit.Properties.Resources.Close;
            this.cmdCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(63, 25);
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.ToolTipText = "Close this window without selecting anything.";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // lblRestrictions
            // 
            this.lblRestrictions.Name = "lblRestrictions";
            this.lblRestrictions.Size = new System.Drawing.Size(103, 25);
            this.lblRestrictions.Text = "Restrictions: None";
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(401, 129);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Title Missing";
            this.Load += new System.EventHandler(this.frmInput_Load);
            this.Shown += new System.EventHandler(this.frmInput_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ToolStrip5.ResumeLayout(false);
            this.ToolStrip5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        internal System.Windows.Forms.ToolStrip ToolStrip5;
        private System.Windows.Forms.ToolStripButton cmdAccept;
        private System.Windows.Forms.ToolStripButton cmdCancel;
        public System.Windows.Forms.Label lblMessage;
        public System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblRestrictions;
        private System.Windows.Forms.Timer timer1;
    }
}