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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txt_ReadLogs = new System.Windows.Forms.TextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.txt_Infile = new System.Windows.Forms.TextBox();
            this.btn_OpenFile = new System.Windows.Forms.Button();
            this.Panel_Tag = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_StreamReader = new System.Windows.Forms.Label();
            this.Panel_ReadLogs = new System.Windows.Forms.Panel();
            this.txt_Debugger = new System.Windows.Forms.TextBox();
            this.btn_ClearDebugger = new System.Windows.Forms.Button();
            this.txt_CPU = new System.Windows.Forms.TextBox();
            this.txt_Ram = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_filesize = new System.Windows.Forms.TextBox();
            this.txt_wait = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.Panel_ReadLogs.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_ReadLogs
            // 
            this.txt_ReadLogs.BackColor = System.Drawing.Color.Black;
            this.txt_ReadLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_ReadLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ReadLogs.ForeColor = System.Drawing.Color.GhostWhite;
            this.txt_ReadLogs.Location = new System.Drawing.Point(0, 0);
            this.txt_ReadLogs.Multiline = true;
            this.txt_ReadLogs.Name = "txt_ReadLogs";
            this.txt_ReadLogs.ReadOnly = true;
            this.txt_ReadLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_ReadLogs.Size = new System.Drawing.Size(852, 356);
            this.txt_ReadLogs.TabIndex = 0;
            // 
            // btn_Start
            // 
            this.btn_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Start.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_Start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Start.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Start.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_Start.Location = new System.Drawing.Point(788, 443);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(76, 26);
            this.btn_Start.TabIndex = 1;
            this.btn_Start.Text = "Run";
            this.btn_Start.UseVisualStyleBackColor = false;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_ClickAsync);
            // 
            // txt_search
            // 
            this.txt_search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_search.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_search.ForeColor = System.Drawing.Color.DarkBlue;
            this.txt_search.Location = new System.Drawing.Point(12, 443);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(770, 26);
            this.txt_search.TabIndex = 2;
            // 
            // txt_Infile
            // 
            this.txt_Infile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Infile.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_Infile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Infile.ForeColor = System.Drawing.Color.DarkBlue;
            this.txt_Infile.Location = new System.Drawing.Point(12, 411);
            this.txt_Infile.Name = "txt_Infile";
            this.txt_Infile.Size = new System.Drawing.Size(688, 26);
            this.txt_Infile.TabIndex = 3;
            // 
            // btn_OpenFile
            // 
            this.btn_OpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OpenFile.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_OpenFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_OpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_OpenFile.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_OpenFile.Location = new System.Drawing.Point(788, 411);
            this.btn_OpenFile.Name = "btn_OpenFile";
            this.btn_OpenFile.Size = new System.Drawing.Size(76, 26);
            this.btn_OpenFile.TabIndex = 5;
            this.btn_OpenFile.Text = "Open File";
            this.btn_OpenFile.UseVisualStyleBackColor = false;
            this.btn_OpenFile.Click += new System.EventHandler(this.btn_OpenFile_Click);
            // 
            // Panel_Tag
            // 
            this.Panel_Tag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Tag.Location = new System.Drawing.Point(12, 12);
            this.Panel_Tag.Name = "Panel_Tag";
            this.Panel_Tag.Size = new System.Drawing.Size(852, 31);
            this.Panel_Tag.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "StreamReader =";
            // 
            // lbl_StreamReader
            // 
            this.lbl_StreamReader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_StreamReader.AutoSize = true;
            this.lbl_StreamReader.Location = new System.Drawing.Point(102, 6);
            this.lbl_StreamReader.Name = "lbl_StreamReader";
            this.lbl_StreamReader.Size = new System.Drawing.Size(46, 13);
            this.lbl_StreamReader.TabIndex = 8;
            this.lbl_StreamReader.Text = "Pending";
            // 
            // Panel_ReadLogs
            // 
            this.Panel_ReadLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_ReadLogs.Controls.Add(this.txt_ReadLogs);
            this.Panel_ReadLogs.Location = new System.Drawing.Point(12, 49);
            this.Panel_ReadLogs.Name = "Panel_ReadLogs";
            this.Panel_ReadLogs.Size = new System.Drawing.Size(852, 356);
            this.Panel_ReadLogs.TabIndex = 9;
            // 
            // txt_Debugger
            // 
            this.txt_Debugger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Debugger.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Debugger.Location = new System.Drawing.Point(4, 33);
            this.txt_Debugger.Multiline = true;
            this.txt_Debugger.Name = "txt_Debugger";
            this.txt_Debugger.ReadOnly = true;
            this.txt_Debugger.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Debugger.Size = new System.Drawing.Size(224, 0);
            this.txt_Debugger.TabIndex = 10;
            // 
            // btn_ClearDebugger
            // 
            this.btn_ClearDebugger.Location = new System.Drawing.Point(32, 3);
            this.btn_ClearDebugger.Name = "btn_ClearDebugger";
            this.btn_ClearDebugger.Size = new System.Drawing.Size(99, 24);
            this.btn_ClearDebugger.TabIndex = 11;
            this.btn_ClearDebugger.Text = "Clear Debugger";
            this.btn_ClearDebugger.UseVisualStyleBackColor = true;
            this.btn_ClearDebugger.Click += new System.EventHandler(this.btn_ClearDebugger_Click);
            // 
            // txt_CPU
            // 
            this.txt_CPU.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_CPU.Location = new System.Drawing.Point(163, 5);
            this.txt_CPU.Name = "txt_CPU";
            this.txt_CPU.ReadOnly = true;
            this.txt_CPU.Size = new System.Drawing.Size(44, 20);
            this.txt_CPU.TabIndex = 12;
            // 
            // txt_Ram
            // 
            this.txt_Ram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_Ram.Location = new System.Drawing.Point(213, 5);
            this.txt_Ram.Name = "txt_Ram";
            this.txt_Ram.ReadOnly = true;
            this.txt_Ram.Size = new System.Drawing.Size(49, 20);
            this.txt_Ram.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btn_ClearDebugger);
            this.panel1.Controls.Add(this.txt_Debugger);
            this.panel1.Location = new System.Drawing.Point(432, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 0);
            this.panel1.TabIndex = 14;
            this.panel1.Visible = false;
            // 
            // txt_filesize
            // 
            this.txt_filesize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_filesize.Location = new System.Drawing.Point(268, 5);
            this.txt_filesize.Name = "txt_filesize";
            this.txt_filesize.ReadOnly = true;
            this.txt_filesize.Size = new System.Drawing.Size(49, 20);
            this.txt_filesize.TabIndex = 15;
            // 
            // txt_wait
            // 
            this.txt_wait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_wait.Location = new System.Drawing.Point(323, 6);
            this.txt_wait.Name = "txt_wait";
            this.txt_wait.Size = new System.Drawing.Size(26, 20);
            this.txt_wait.TabIndex = 16;
            this.txt_wait.Text = "1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.txt_wait);
            this.panel2.Controls.Add(this.lbl_StreamReader);
            this.panel2.Controls.Add(this.txt_filesize);
            this.panel2.Controls.Add(this.txt_CPU);
            this.panel2.Controls.Add(this.txt_Ram);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 475);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(876, 28);
            this.panel2.TabIndex = 17;
            // 
            // btn_Browse
            // 
            this.btn_Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Browse.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_Browse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Browse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Browse.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_Browse.Location = new System.Drawing.Point(706, 411);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(76, 26);
            this.btn_Browse.TabIndex = 18;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.UseVisualStyleBackColor = false;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btn_Start;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(876, 503);
            this.Controls.Add(this.btn_Browse);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btn_OpenFile);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.Panel_ReadLogs);
            this.Controls.Add(this.Panel_Tag);
            this.Controls.Add(this.txt_Infile);
            this.Controls.Add(this.txt_search);
            this.ForeColor = System.Drawing.Color.GhostWhite;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Better than Cygwin";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Panel_ReadLogs.ResumeLayout(false);
            this.Panel_ReadLogs.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_ReadLogs;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.TextBox txt_Infile;
        private System.Windows.Forms.Button btn_OpenFile;
        private System.Windows.Forms.Panel Panel_Tag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_StreamReader;
        private System.Windows.Forms.Panel Panel_ReadLogs;
        private System.Windows.Forms.TextBox txt_Debugger;
        private System.Windows.Forms.Button btn_ClearDebugger;
        private System.Windows.Forms.TextBox txt_CPU;
        private System.Windows.Forms.TextBox txt_Ram;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_filesize;
        private System.Windows.Forms.TextBox txt_wait;
        public System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Browse;
    }
}

