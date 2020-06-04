namespace Read_logs
{
    partial class Form1
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
            this.txt_ReadLogs = new System.Windows.Forms.TextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.txt_Infile = new System.Windows.Forms.TextBox();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_OpenFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_ReadLogs
            // 
            this.txt_ReadLogs.Location = new System.Drawing.Point(12, 81);
            this.txt_ReadLogs.Multiline = true;
            this.txt_ReadLogs.Name = "txt_ReadLogs";
            this.txt_ReadLogs.ReadOnly = true;
            this.txt_ReadLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_ReadLogs.Size = new System.Drawing.Size(776, 357);
            this.txt_ReadLogs.TabIndex = 0;
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(579, 44);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(76, 31);
            this.btn_Start.TabIndex = 1;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_ClickAsync);
            // 
            // txt_search
            // 
            this.txt_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_search.Location = new System.Drawing.Point(12, 44);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(561, 26);
            this.txt_search.TabIndex = 2;
            // 
            // txt_Infile
            // 
            this.txt_Infile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Infile.Location = new System.Drawing.Point(12, 12);
            this.txt_Infile.Name = "txt_Infile";
            this.txt_Infile.Size = new System.Drawing.Size(561, 26);
            this.txt_Infile.TabIndex = 3;
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(661, 43);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(76, 31);
            this.btn_stop.TabIndex = 4;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_OpenFile
            // 
            this.btn_OpenFile.Location = new System.Drawing.Point(579, 7);
            this.btn_OpenFile.Name = "btn_OpenFile";
            this.btn_OpenFile.Size = new System.Drawing.Size(76, 31);
            this.btn_OpenFile.TabIndex = 5;
            this.btn_OpenFile.Text = "Open File";
            this.btn_OpenFile.UseVisualStyleBackColor = true;
            this.btn_OpenFile.Click += new System.EventHandler(this.btn_OpenFile_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btn_Start;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_OpenFile);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.txt_Infile);
            this.Controls.Add(this.txt_search);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.txt_ReadLogs);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_ReadLogs;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.TextBox txt_Infile;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_OpenFile;
    }
}

