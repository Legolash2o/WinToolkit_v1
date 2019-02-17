namespace WinToolkit.Prompts
{
    partial class frmTweaks
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
            this.cboRChoice = new System.Windows.Forms.ComboBox();
            this.cmdRBrowse = new System.Windows.Forms.Button();
            this.txtRInfo = new System.Windows.Forms.TextBox();
            this.ToolStrip5 = new System.Windows.Forms.ToolStrip();
            this.cmdROK = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdRCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStrip5.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboRChoice
            // 
            this.cboRChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRChoice.FormattingEnabled = true;
            this.cboRChoice.Location = new System.Drawing.Point(6, 11);
            this.cboRChoice.Name = "cboRChoice";
            this.cboRChoice.Size = new System.Drawing.Size(306, 21);
            this.cboRChoice.TabIndex = 18;
            this.cboRChoice.Visible = false;
            // 
            // cmdRBrowse
            // 
            this.cmdRBrowse.Location = new System.Drawing.Point(294, 14);
            this.cmdRBrowse.Name = "cmdRBrowse";
            this.cmdRBrowse.Size = new System.Drawing.Size(18, 18);
            this.cmdRBrowse.TabIndex = 17;
            this.cmdRBrowse.UseVisualStyleBackColor = true;
            this.cmdRBrowse.Visible = false;
            this.cmdRBrowse.Click += new System.EventHandler(this.cmdRBrowse_Click);
            // 
            // txtRInfo
            // 
            this.txtRInfo.Location = new System.Drawing.Point(6, 12);
            this.txtRInfo.Name = "txtRInfo";
            this.txtRInfo.Size = new System.Drawing.Size(282, 20);
            this.txtRInfo.TabIndex = 0;
            this.txtRInfo.Visible = false;
            // 
            // ToolStrip5
            // 
            this.ToolStrip5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ToolStrip5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdROK,
            this.cmdRCancel});
            this.ToolStrip5.Location = new System.Drawing.Point(0, 45);
            this.ToolStrip5.Name = "ToolStrip5";
            this.ToolStrip5.Size = new System.Drawing.Size(318, 25);
            this.ToolStrip5.TabIndex = 10;
            this.ToolStrip5.TabStop = true;
            this.ToolStrip5.Text = "ToolStrip5";
            // 
            // cmdROK
            // 
            this.cmdROK.Name = "cmdROK";
            this.cmdROK.Size = new System.Drawing.Size(35, 25);
            this.cmdROK.Tag = "OK";
            this.cmdROK.Text = "OK";
            this.cmdROK.ToolTipText = "Accept new setting.";
            this.cmdROK.Click += new System.EventHandler(this.cmdROK_Click);
            // 
            // cmdRCancel
            // 
            this.cmdRCancel.Name = "cmdRCancel";
            this.cmdRCancel.Size = new System.Drawing.Size(55, 25);
            this.cmdRCancel.Tag = "Cancel";
            this.cmdRCancel.Text = "Cancel";
            this.cmdRCancel.ToolTipText = "Cancel Task";
            this.cmdRCancel.Click += new System.EventHandler(this.cmdRCancel_Click);
            // 
            // frmTweaks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(318, 70);
            this.Controls.Add(this.cboRChoice);
            this.Controls.Add(this.txtRInfo);
            this.Controls.Add(this.ToolStrip5);
            this.Controls.Add(this.cmdRBrowse);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTweaks";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tweak Prompt";
            this.Load += new System.EventHandler(this.frmTweaks_Load);
            this.ToolStrip5.ResumeLayout(false);
            this.ToolStrip5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox cboRChoice;
        internal System.Windows.Forms.Button cmdRBrowse;
        internal System.Windows.Forms.TextBox txtRInfo;
        internal System.Windows.Forms.ToolStrip ToolStrip5;
        internal System.Windows.Forms.ToolStripMenuItem cmdROK;
        internal System.Windows.Forms.ToolStripMenuItem cmdRCancel;
    }
}