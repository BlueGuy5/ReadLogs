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
            this.btn_Start = new System.Windows.Forms.Button();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.btn_OpenFile = new System.Windows.Forms.Button();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.Panel_Main = new System.Windows.Forms.Panel();
            this.tag_readlog = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_StreamReader = new System.Windows.Forms.Label();
            this.Panel_ReadLogs = new System.Windows.Forms.Panel();
            this.txt_ReadLogs = new System.Windows.Forms.RichTextBox();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.groupBox_infile = new System.Windows.Forms.GroupBox();
            this.lbl_filesize = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Panel_Tag = new System.Windows.Forms.Panel();
            this.Panel_Main.SuspendLayout();
            this.Panel_ReadLogs.SuspendLayout();
            this.groupBox_infile.SuspendLayout();
            this.Panel_Tag.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Start.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_Start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Start.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Start.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_Start.Location = new System.Drawing.Point(173, 15);
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
            this.txt_search.Location = new System.Drawing.Point(9, 43);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(855, 26);
            this.txt_search.TabIndex = 2;
            // 
            // btn_OpenFile
            // 
            this.btn_OpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_OpenFile.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_OpenFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_OpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_OpenFile.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_OpenFile.Location = new System.Drawing.Point(91, 15);
            this.btn_OpenFile.Name = "btn_OpenFile";
            this.btn_OpenFile.Size = new System.Drawing.Size(76, 26);
            this.btn_OpenFile.TabIndex = 5;
            this.btn_OpenFile.Text = "Open File";
            this.btn_OpenFile.UseVisualStyleBackColor = false;
            this.btn_OpenFile.Click += new System.EventHandler(this.btn_OpenFile_Click);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(831, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 30);
            this.vScrollBar1.TabIndex = 2;
            this.vScrollBar1.Visible = false;
            // 
            // Panel_Main
            // 
            this.Panel_Main.Controls.Add(this.tag_readlog);
            this.Panel_Main.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel_Main.Location = new System.Drawing.Point(0, 0);
            this.Panel_Main.Name = "Panel_Main";
            this.Panel_Main.Size = new System.Drawing.Size(50, 30);
            this.Panel_Main.TabIndex = 1;
            // 
            // tag_readlog
            // 
            this.tag_readlog.BackColor = System.Drawing.Color.AliceBlue;
            this.tag_readlog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tag_readlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tag_readlog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.tag_readlog.ForeColor = System.Drawing.Color.DarkBlue;
            this.tag_readlog.Location = new System.Drawing.Point(0, 0);
            this.tag_readlog.Name = "tag_readlog";
            this.tag_readlog.Size = new System.Drawing.Size(50, 30);
            this.tag_readlog.TabIndex = 19;
            this.tag_readlog.Text = "Main";
            this.tag_readlog.UseVisualStyleBackColor = false;
            this.tag_readlog.Click += new System.EventHandler(this.tag_readlog_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(293, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Status:";
            // 
            // lbl_StreamReader
            // 
            this.lbl_StreamReader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_StreamReader.AutoSize = true;
            this.lbl_StreamReader.Location = new System.Drawing.Point(339, 15);
            this.lbl_StreamReader.Name = "lbl_StreamReader";
            this.lbl_StreamReader.Size = new System.Drawing.Size(38, 13);
            this.lbl_StreamReader.TabIndex = 8;
            this.lbl_StreamReader.Text = "Ready";
            // 
            // Panel_ReadLogs
            // 
            this.Panel_ReadLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_ReadLogs.Controls.Add(this.txt_ReadLogs);
            this.Panel_ReadLogs.Location = new System.Drawing.Point(12, 49);
            this.Panel_ReadLogs.Name = "Panel_ReadLogs";
            this.Panel_ReadLogs.Size = new System.Drawing.Size(852, 373);
            this.Panel_ReadLogs.TabIndex = 9;
            // 
            // txt_ReadLogs
            // 
            this.txt_ReadLogs.BackColor = System.Drawing.Color.Black;
            this.txt_ReadLogs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_ReadLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_ReadLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ReadLogs.ForeColor = System.Drawing.Color.GhostWhite;
            this.txt_ReadLogs.HideSelection = false;
            this.txt_ReadLogs.Location = new System.Drawing.Point(0, 0);
            this.txt_ReadLogs.Name = "txt_ReadLogs";
            this.txt_ReadLogs.ReadOnly = true;
            this.txt_ReadLogs.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txt_ReadLogs.Size = new System.Drawing.Size(852, 373);
            this.txt_ReadLogs.TabIndex = 0;
            this.txt_ReadLogs.Text = "";
            // 
            // btn_Browse
            // 
            this.btn_Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Browse.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_Browse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Browse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Browse.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_Browse.Location = new System.Drawing.Point(9, 15);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(76, 26);
            this.btn_Browse.TabIndex = 18;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.UseVisualStyleBackColor = false;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Add.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_Add.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Add.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_Add.Location = new System.Drawing.Point(255, 15);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(32, 26);
            this.btn_Add.TabIndex = 19;
            this.btn_Add.Text = "+";
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // groupBox_infile
            // 
            this.groupBox_infile.BackColor = System.Drawing.Color.Black;
            this.groupBox_infile.Controls.Add(this.lbl_filesize);
            this.groupBox_infile.Controls.Add(this.label2);
            this.groupBox_infile.Controls.Add(this.label1);
            this.groupBox_infile.Controls.Add(this.btn_OpenFile);
            this.groupBox_infile.Controls.Add(this.btn_Browse);
            this.groupBox_infile.Controls.Add(this.btn_Add);
            this.groupBox_infile.Controls.Add(this.lbl_StreamReader);
            this.groupBox_infile.Controls.Add(this.btn_Start);
            this.groupBox_infile.Controls.Add(this.txt_search);
            this.groupBox_infile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox_infile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox_infile.ForeColor = System.Drawing.Color.GhostWhite;
            this.groupBox_infile.Location = new System.Drawing.Point(0, 428);
            this.groupBox_infile.Name = "groupBox_infile";
            this.groupBox_infile.Size = new System.Drawing.Size(876, 75);
            this.groupBox_infile.TabIndex = 1;
            this.groupBox_infile.TabStop = false;
            this.groupBox_infile.Text = "FileStream";
            // 
            // lbl_filesize
            // 
            this.lbl_filesize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_filesize.AutoSize = true;
            this.lbl_filesize.Location = new System.Drawing.Point(435, 15);
            this.lbl_filesize.Name = "lbl_filesize";
            this.lbl_filesize.Size = new System.Drawing.Size(13, 13);
            this.lbl_filesize.TabIndex = 21;
            this.lbl_filesize.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(383, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "FileSize:";
            // 
            // Panel_Tag
            // 
            this.Panel_Tag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Tag.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel_Tag.Controls.Add(this.vScrollBar1);
            this.Panel_Tag.Controls.Add(this.Panel_Main);
            this.Panel_Tag.Location = new System.Drawing.Point(12, 12);
            this.Panel_Tag.Name = "Panel_Tag";
            this.Panel_Tag.Size = new System.Drawing.Size(852, 34);
            this.Panel_Tag.TabIndex = 6;
            // 
            // Form1
            // 
            this.AcceptButton = this.btn_Add;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(876, 503);
            this.Controls.Add(this.groupBox_infile);
            this.Controls.Add(this.Panel_ReadLogs);
            this.Controls.Add(this.Panel_Tag);
            this.ForeColor = System.Drawing.Color.GhostWhite;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Better than Cygwin";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Panel_Main.ResumeLayout(false);
            this.Panel_ReadLogs.ResumeLayout(false);
            this.groupBox_infile.ResumeLayout(false);
            this.groupBox_infile.PerformLayout();
            this.Panel_Tag.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_OpenFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_StreamReader;
        private System.Windows.Forms.Panel Panel_ReadLogs;
        public System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.Button tag_readlog;
        private System.Windows.Forms.Panel Panel_Main;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.GroupBox groupBox_infile;
        private System.Windows.Forms.RichTextBox txt_ReadLogs;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Panel Panel_Tag;
        private System.Windows.Forms.Label lbl_filesize;
        private System.Windows.Forms.Label label2;
    }
}

