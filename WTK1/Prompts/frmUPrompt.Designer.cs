namespace WinToolkit.Prompts
{
    partial class frmUnattendedPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUnattendedPrompt));
            this.cmdRBrowse = new System.Windows.Forms.Button();
            this.txtRInfo = new System.Windows.Forms.TextBox();
            this.cmdFull = new System.Windows.Forms.Button();
            this.cmdQuick = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.txtNotice = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdRBrowse
            // 
            this.cmdRBrowse.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmdRBrowse.Image = global::WinToolkit.Properties.Resources.File_icon;
            this.cmdRBrowse.Location = new System.Drawing.Point(357, 4);
            this.cmdRBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.cmdRBrowse.Name = "cmdRBrowse";
            this.cmdRBrowse.Size = new System.Drawing.Size(24, 21);
            this.cmdRBrowse.TabIndex = 17;
            this.cmdRBrowse.UseVisualStyleBackColor = true;
            this.cmdRBrowse.Click += new System.EventHandler(this.cmdRBrowse_Click);
            // 
            // txtRInfo
            // 
            this.txtRInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtRInfo.Location = new System.Drawing.Point(4, 4);
            this.txtRInfo.Margin = new System.Windows.Forms.Padding(4);
            this.txtRInfo.Name = "txtRInfo";
            this.txtRInfo.ReadOnly = true;
            this.txtRInfo.Size = new System.Drawing.Size(349, 22);
            this.txtRInfo.TabIndex = 0;
            this.txtRInfo.TextChanged += new System.EventHandler(this.txtRInfo_TextChanged);
            // 
            // cmdFull
            // 
            this.cmdFull.Enabled = false;
            this.cmdFull.Image = ((System.Drawing.Image)(resources.GetObject("cmdFull.Image")));
            this.cmdFull.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdFull.Location = new System.Drawing.Point(8, 18);
            this.cmdFull.Margin = new System.Windows.Forms.Padding(4);
            this.cmdFull.Name = "cmdFull";
            this.cmdFull.Size = new System.Drawing.Size(115, 66);
            this.cmdFull.TabIndex = 18;
            this.cmdFull.Text = "Full";
            this.cmdFull.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdFull.UseVisualStyleBackColor = true;
            this.cmdFull.Click += new System.EventHandler(this.cmdFull_Click);
            // 
            // cmdQuick
            // 
            this.cmdQuick.Enabled = false;
            this.cmdQuick.Image = ((System.Drawing.Image)(resources.GetObject("cmdQuick.Image")));
            this.cmdQuick.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdQuick.Location = new System.Drawing.Point(131, 18);
            this.cmdQuick.Margin = new System.Windows.Forms.Padding(4);
            this.cmdQuick.Name = "cmdQuick";
            this.cmdQuick.Size = new System.Drawing.Size(115, 66);
            this.cmdQuick.TabIndex = 19;
            this.cmdQuick.Text = "Quick";
            this.cmdQuick.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdQuick.UseVisualStyleBackColor = true;
            this.cmdQuick.Click += new System.EventHandler(this.cmdQuick_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCancel.Location = new System.Drawing.Point(253, 18);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(4);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(115, 66);
            this.cmdCancel.TabIndex = 20;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // txtNotice
            // 
            this.txtNotice.BackColor = System.Drawing.Color.White;
            this.txtNotice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNotice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotice.Location = new System.Drawing.Point(4, 4);
            this.txtNotice.Margin = new System.Windows.Forms.Padding(4);
            this.txtNotice.Multiline = true;
            this.txtNotice.Name = "txtNotice";
            this.txtNotice.ReadOnly = true;
            this.txtNotice.Size = new System.Drawing.Size(377, 42);
            this.txtNotice.TabIndex = 21;
            this.txtNotice.Text = "Please select an unattended xml file...";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtRInfo);
            this.splitContainer1.Panel1.Controls.Add(this.cmdRBrowse);
            this.splitContainer1.Panel1MinSize = 28;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(385, 173);
            this.splitContainer1.SplitterDistance = 28;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 22;
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
            this.splitContainer2.Panel1.Controls.Add(this.cmdQuick);
            this.splitContainer2.Panel1.Controls.Add(this.cmdCancel);
            this.splitContainer2.Panel1.Controls.Add(this.cmdFull);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtNotice);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Panel2MinSize = 50;
            this.splitContainer2.Size = new System.Drawing.Size(385, 144);
            this.splitContainer2.SplitterDistance = 93;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 23;
            // 
            // frmUnattendedPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(385, 173);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUnattendedPrompt";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Unattended";
            this.Load += new System.EventHandler(this.frmUPrompt_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button cmdRBrowse;
        internal System.Windows.Forms.TextBox txtRInfo;
        private System.Windows.Forms.Button cmdFull;
        private System.Windows.Forms.Button cmdQuick;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.TextBox txtNotice;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}