namespace WinToolkit.Prompts
{
    partial class frmShutdown
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
            this.ToolStrip5 = new System.Windows.Forms.ToolStrip();
            this.cmdShutdown = new System.Windows.Forms.ToolStripButton();
            this.cmdRestart = new System.Windows.Forms.ToolStripButton();
            this.cmdAbort = new System.Windows.Forms.ToolStripButton();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.timShutdown = new System.Windows.Forms.Timer(this.components);
            this.ToolStrip5.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolStrip5
            // 
            this.ToolStrip5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ToolStrip5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdShutdown,
            this.cmdRestart,
            this.cmdAbort});
            this.ToolStrip5.Location = new System.Drawing.Point(0, 86);
            this.ToolStrip5.Name = "ToolStrip5";
            this.ToolStrip5.Size = new System.Drawing.Size(334, 25);
            this.ToolStrip5.TabIndex = 10;
            this.ToolStrip5.TabStop = true;
            this.ToolStrip5.Text = "ToolStrip5";
            // 
            // cmdShutdown
            // 
            this.cmdShutdown.Image = global::WinToolkit.Properties.Resources.OK;
            this.cmdShutdown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdShutdown.Name = "cmdShutdown";
            this.cmdShutdown.Size = new System.Drawing.Size(109, 22);
            this.cmdShutdown.Text = "Shutdown Now";
            this.cmdShutdown.Click += new System.EventHandler(this.cmdShutdown_Click);
            // 
            // cmdRestart
            // 
            this.cmdRestart.Image = global::WinToolkit.Properties.Resources.Refresh;
            this.cmdRestart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdRestart.Name = "cmdRestart";
            this.cmdRestart.Size = new System.Drawing.Size(91, 22);
            this.cmdRestart.Text = "Restart Now";
            this.cmdRestart.Click += new System.EventHandler(this.cmdRestart_Click);
            // 
            // cmdAbort
            // 
            this.cmdAbort.Image = global::WinToolkit.Properties.Resources.Close;
            this.cmdAbort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdAbort.Name = "cmdAbort";
            this.cmdAbort.Size = new System.Drawing.Size(57, 22);
            this.cmdAbort.Text = "Abort";
            this.cmdAbort.Click += new System.EventHandler(this.cmdAbort_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(10, 22);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(55, 39);
            this.lblTime.TabIndex = 11;
            this.lblTime.Text = "60";
            // 
            // lblDesc
            // 
            this.lblDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.Location = new System.Drawing.Point(73, 9);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(239, 72);
            this.lblDesc.TabIndex = 12;
            this.lblDesc.Text = "Win Toolkit is about to shutdown your computer in less than 1 minute.";
            // 
            // timShutdown
            // 
            this.timShutdown.Enabled = true;
            this.timShutdown.Interval = 1000;
            this.timShutdown.Tick += new System.EventHandler(this.timShutdown_Tick);
            // 
            // frmShutdown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(334, 111);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.ToolStrip5);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShutdown";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shutdown Warning";
            this.Load += new System.EventHandler(this.frmShutdown_Load);
            this.ToolStrip5.ResumeLayout(false);
            this.ToolStrip5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ToolStrip ToolStrip5;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.ToolStripButton cmdShutdown;
        private System.Windows.Forms.ToolStripButton cmdRestart;
        private System.Windows.Forms.ToolStripButton cmdAbort;
        private System.Windows.Forms.Timer timShutdown;
    }
}