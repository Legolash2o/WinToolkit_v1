namespace WinToolkit.Prompts
{
    partial class frmWinKey
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
            this.lblKey = new System.Windows.Forms.Label();
            this.cmdClipboard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKey.Location = new System.Drawing.Point(93, 13);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(157, 25);
            this.lblKey.TabIndex = 0;
            this.lblKey.Text = "No key found...";
            // 
            // cmdClipboard
            // 
            this.cmdClipboard.Location = new System.Drawing.Point(12, 13);
            this.cmdClipboard.Name = "cmdClipboard";
            this.cmdClipboard.Size = new System.Drawing.Size(75, 23);
            this.cmdClipboard.TabIndex = 1;
            this.cmdClipboard.Text = "Clipboard";
            this.cmdClipboard.UseVisualStyleBackColor = true;
            this.cmdClipboard.Click += new System.EventHandler(this.cmdClipboard_Click);
            // 
            // frmWinKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 49);
            this.Controls.Add(this.cmdClipboard);
            this.Controls.Add(this.lblKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWinKey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Your Windows Key";
            this.Load += new System.EventHandler(this.frmWinKey_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.Button cmdClipboard;
    }
}