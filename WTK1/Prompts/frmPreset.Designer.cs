namespace WinToolkit.Prompts
{
    partial class frmAIOPresetName
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAIOPresetName));
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdContinue = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdAccept = new System.Windows.Forms.ToolStripMenuItem();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblChar = new System.Windows.Forms.ToolStripLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPresetName = new System.Windows.Forms.TextBox();
            this.ToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ToolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdCancel,
            this.toolStripSeparator1,
            this.cmdContinue,
            this.cmdAccept,
            this.lblStatus,
            this.toolStripSeparator2,
            this.lblChar});
            this.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolStrip1.Location = new System.Drawing.Point(0, 88);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.ToolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ToolStrip1.Size = new System.Drawing.Size(504, 36);
            this.ToolStrip1.TabIndex = 1;
            this.ToolStrip1.TabStop = true;
            this.ToolStrip1.Text = "ToolStrip1";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(81, 36);
            this.cmdCancel.Text = "Abort";
            this.cmdCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.cmdCancel.ToolTipText = "This will abort the current task (without saving).";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 36);
            // 
            // cmdContinue
            // 
            this.cmdContinue.Image = ((System.Drawing.Image)(resources.GetObject("cmdContinue.Image")));
            this.cmdContinue.Name = "cmdContinue";
            this.cmdContinue.Size = new System.Drawing.Size(100, 36);
            this.cmdContinue.Text = "Continue";
            this.cmdContinue.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.cmdContinue.ToolTipText = "Continue without saving a preset.";
            this.cmdContinue.Click += new System.EventHandler(this.cmdContinue_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Image = ((System.Drawing.Image)(resources.GetObject("cmdAccept.Image")));
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(88, 36);
            this.cmdAccept.Text = "Accept";
            this.cmdAccept.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.cmdAccept.ToolTipText = "This will accept your name, save the preset and then continue with the tasks.";
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 33);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 36);
            // 
            // lblChar
            // 
            this.lblChar.Name = "lblChar";
            this.lblChar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblChar.Size = new System.Drawing.Size(138, 33);
            this.lblChar.Text = "50 Characters Remaining";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(484, 36);
            this.label1.TabIndex = 18;
            this.label1.Text = "Please enter a custom name for this task. This will allow you do to the same task" +
    " at a later date.";
            // 
            // txtPresetName
            // 
            this.txtPresetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPresetName.Location = new System.Drawing.Point(11, 52);
            this.txtPresetName.MaxLength = 50;
            this.txtPresetName.Name = "txtPresetName";
            this.txtPresetName.Size = new System.Drawing.Size(481, 22);
            this.txtPresetName.TabIndex = 0;
            this.txtPresetName.TextChanged += new System.EventHandler(this.txtPresetName_TextChanged);
            // 
            // frmAIOPresetName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 124);
            this.Controls.Add(this.txtPresetName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ToolStrip1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAIOPresetName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preset Name";
            this.Load += new System.EventHandler(this.frmPresetPrompt_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPresetPrompt_KeyDown);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip ToolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem cmdCancel;
        internal System.Windows.Forms.ToolStripMenuItem cmdAccept;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem cmdContinue;
        private System.Windows.Forms.ToolStripLabel lblStatus;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtPresetName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblChar;
    }
}