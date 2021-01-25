namespace BTC
{
    partial class Debug_Log
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Debug_Log));
            this.txt_DebugLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txt_DebugLog
            // 
            this.txt_DebugLog.BackColor = System.Drawing.Color.Black;
            this.txt_DebugLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_DebugLog.ForeColor = System.Drawing.Color.GhostWhite;
            this.txt_DebugLog.HideSelection = false;
            this.txt_DebugLog.Location = new System.Drawing.Point(0, 0);
            this.txt_DebugLog.Name = "txt_DebugLog";
            this.txt_DebugLog.ReadOnly = true;
            this.txt_DebugLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txt_DebugLog.Size = new System.Drawing.Size(800, 450);
            this.txt_DebugLog.TabIndex = 0;
            this.txt_DebugLog.Text = "";
            // 
            // Debug_Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txt_DebugLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Debug_Log";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Debug_Log";
            this.Load += new System.EventHandler(this.Debug_Log_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox txt_DebugLog;
    }
}