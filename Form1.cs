using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Read_logs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(CloseApp);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //txt_ReadLogs.BackColor = Color.White;
            //btn_stop.Enabled = false;
            //txt_search.Text = "(dsp_earcon) Attempting to start a new download of version,reconnect attempt";
            txt_Infile.Text = @"C:\Users\williamyu\Desktop\Frames_Logs\";
            await Get_CPU_Usage();
            Get_Ram_Usage();
        }
        
        private async Task Get_CPU_Usage()
        {
            try
            {
                PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                dynamic firstValue = cpuCounter.NextValue();
                await Task.Delay(1000);
                dynamic secondvalue = cpuCounter.NextValue();
                txt_CPU.Text = secondvalue + "%";

                if (Convert.ToDouble(txt_CPU.Text.Length - 1) >= 50)
                {
                    txt_CPU.BackColor = Color.Red;
                }
                else
                {
                    txt_CPU.BackColor = Color.Green;
                }
                //txt_Debugger.AppendText("\r\n" + secondvalue + "%");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Get_CPU_Usage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Get_Ram_Usage()
        {
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            txt_Ram.Text = ramCounter.NextValue()+" MB";
        }
        private void Get_FileSize()
        {
            string file = txt_Infile.Text;
            FileStream FS = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);              
            long filesize = FS.Length / 1024;
            txt_filesize.Text = filesize.ToString() + " KB";
        }
        
        string tmpfile;
        bool loopthis = false;
        bool btn_press = false;
        TextBox txt_multilogs;
        private void CreateTags(string searchText)
        {


            Button btn_close_keyword = new Button();
            btn_close_keyword.Name = searchText;
            btn_close_keyword.Text = "X";
            btn_close_keyword.Width = 25;
            btn_close_keyword.Height = Panel_Tag.Height;
            btn_close_keyword.Cursor = Cursors.Hand;
            btn_close_keyword.ForeColor = Color.Red;
            btn_close_keyword.FlatStyle = FlatStyle.Popup;
            btn_close_keyword.FlatAppearance.BorderColor = Color.Aqua;
            btn_close_keyword.Click += new EventHandler(btn_close_keyword_Click);

            Button btn_SearchText = new Button();
            btn_SearchText.Name = searchText;
            btn_SearchText.Text = searchText;
            //btn_SearchText.Width = Panel_Keywords.Width - btn_close_keyword.Width;
            btn_SearchText.Width = 150;
            btn_SearchText.Height = Panel_Tag.Height;
            btn_SearchText.Location = new Point(btn_close_keyword.Width);
            btn_SearchText.Cursor = Cursors.Hand;
            btn_SearchText.ForeColor = Color.DarkBlue;
            btn_SearchText.BackColor = Color.AliceBlue;
            btn_SearchText.FlatStyle = FlatStyle.Popup;
            btn_SearchText.FlatAppearance.BorderColor = Color.Aqua;
            btn_SearchText.MouseDown += new MouseEventHandler(btn_SearchText_RightClickAsync);

            Panel Panel_Keywords = new Panel();
            Panel_Keywords.Name = searchText;
            //Panel_Keywords.Width = 150;
            Panel_Keywords.Width = btn_close_keyword.Width + btn_SearchText.Width;
            Panel_Keywords.Height = Panel_Tag.Height;
            Panel_Keywords.BackColor = Color.AliceBlue;
            Panel_Keywords.Left = Panel_Keywords.Width * (Panel_Tag.Controls.Count);


            Panel_Keywords.Controls.Add(btn_close_keyword);
            Panel_Keywords.Controls.Add(btn_SearchText);
            Panel_Tag.Controls.Add(Panel_Keywords);
            
            txt_multilogs = new TextBox();
            txt_multilogs.Name = searchText;
            txt_multilogs.Dock = DockStyle.Fill;
            txt_multilogs.Visible = false;
            txt_multilogs.Multiline = true;
            txt_multilogs.Font = new Font("Serif", 11);
            txt_multilogs.Width = txt_ReadLogs.Width;
            txt_multilogs.Height = txt_ReadLogs.Height;
            txt_multilogs.Location = txt_ReadLogs.Location;
            txt_multilogs.ScrollBars = ScrollBars.Vertical;
            txt_multilogs.ReadOnly = true;
            txt_multilogs.BackColor = Color.Black;
            txt_multilogs.ForeColor = Color.GhostWhite;
            //txt_multilogs.BackColor = txt_ReadLogs.BackColor;
            Panel_ReadLogs.Controls.Add(txt_multilogs);
        }
        private async Task Log_Output()
        {
            string line;
            tmpfile = txt_Infile.Text;
            string[] splitText;
            loopthis = true;

            splitText = txt_search.Text.Split(',');
            if (btn_press == false)
            {
                Panel_Tag.Controls.Clear();
                RemoveReadLogsControls();                            
                foreach (string searchText in splitText)
                {
                    CreateTags(searchText);
                }
            }
            txt_search.Text = "";

            using (FileStream stream = File.Open(tmpfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
                while (loopthis == true)
                {
                    Thread.Sleep(int.Parse(txt_wait.Text));
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        foreach (TextBox TB in Panel_ReadLogs.Controls)
                        {
                            foreach (string keyword in splitText)
                            {
                                if (TB.Name == keyword)
                                {
                                    if (line.IndexOf(keyword, StringComparison.InvariantCultureIgnoreCase) >= 0)
                                    {
                                        txt_ReadLogs.AppendText(line + "\r\n");
                                        txt_Debugger.AppendText("\r\n" + "Writing....");
                                        txt_Debugger.AppendText("\r\n" + "TB Name: " + TB.Name);
                                        txt_Debugger.AppendText("\r\n" + "ReadToEndAsync");
                                        TB.AppendText(line + "\r\n");
                                    }
                                    lbl_StreamReader.Text = "Open";
                                    if (lbl_StreamReader.Text == "Open")
                                    {
                                        lbl_StreamReader.ForeColor = Color.Green;
                                    }
                                }
                            }
                        }
                    }
                    await Get_CPU_Usage();
                    Get_FileSize();
                    Get_Ram_Usage();
                    Thread.Sleep(int.Parse(txt_wait.Text));
                }
            //lbl_StreamReader.Text = "Close";
            //if (lbl_StreamReader.Text == "Close")
            //{
            //    lbl_StreamReader.ForeColor = Color.Red;
            //}
        }

        private async Task Blank_Log_Output()
        {
            string line;
            tmpfile = txt_Infile.Text;
            loopthis = true;

            txt_ReadLogs.Text = "";
            if (btn_press == false)
            {
                Panel_Tag.Controls.Clear();
            }
            txt_search.Text = "";
            using (FileStream stream = File.Open(tmpfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
            while (loopthis == true)
            {
                Thread.Sleep(int.Parse(txt_wait.Text));
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.IndexOf(txt_search.Text, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        txt_ReadLogs.AppendText(line + "\r\n");
                    }
                    lbl_StreamReader.Text = "Open";
                        if(lbl_StreamReader.Text == "Open")
                        {
                            lbl_StreamReader.ForeColor = Color.Green;
                        }
                }
                await Get_CPU_Usage();
                Get_FileSize();
                Get_Ram_Usage();
                Thread.Sleep(int.Parse(txt_wait.Text));
            }
            //lbl_StreamReader.Text = "Close";
            //if (lbl_StreamReader.Text == "Close")
           // {
           //     lbl_StreamReader.ForeColor = Color.Red;
           // }
        }
        private void btn_close_keyword_Click(object sender, EventArgs e)
        {
            Button _btn = (Button)sender;
            foreach(Panel _panel in Panel_Tag.Controls)
            {
                if(_panel.Name == _btn.Name)
                {
                    Panel_Tag.Controls.Remove(_panel);
                    break;
                }
            }
            foreach(TextBox TB in Panel_ReadLogs.Controls)
            {
                if(TB.Name == _btn.Name)
                {
                    Panel_ReadLogs.Controls.Remove(TB);
                    break;
                }
            }
            if (Panel_Tag.Controls.Count == 0)
            {
                txt_ReadLogs.BringToFront();
                txt_ReadLogs.Visible = true;
            }
            else if (Panel_Tag.Controls.Count >= 1)
            {
                int PanelLeftPos = 0;
                foreach (Panel _panel in Panel_Tag.Controls)
                {
                    _panel.Left = _panel.Width * (PanelLeftPos);
                    PanelLeftPos++;
                }
            }
            Panel_Tag.Refresh();
        }
        private async void btn_SearchText_RightClickAsync(object sender, MouseEventArgs e)
        {
            Button _btn_SearchText = (Button)sender;
            if (e.Button == MouseButtons.Right)
            {
                txt_search.AppendText(_btn_SearchText.Name + ",");
            }
            else if (e.Button == MouseButtons.Left)
            {
                try
                {
                    btn_press = true;
                    await Task.Delay(3);
                    HideTextBox(_btn_SearchText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "btn_searchText_click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void HideTextBox(Button Btn_SearchName)
        {
            foreach (TextBox TB in Panel_ReadLogs.Controls)
            {
                if (TB.Name == Btn_SearchName.Name)
                {
                    //txt_Debugger.AppendText("\r\n" + "Counter: " + cnt);
                    TB.Visible = true;
                    TB.BringToFront();
                    //txt_Debugger.AppendText("\r\n" + "***TB Name***" + TB.Name);
                    //txt_Debugger.AppendText("\r\n" + "***Btn_SearchName***" + Btn_SearchName.Name);
                    txt_ReadLogs.Visible = false;
                }
                else
                {
                    TB.Visible = false;
                }             
            }
        }
        private void RemoveReadLogsControls()
        {
            //must remove any existing controls before we proceed again.
            foreach (TextBox TB in Panel_ReadLogs.Controls)
            {
                if (TB.Name != "txt_ReadLogs")
                {
                    Panel_ReadLogs.Controls.Remove(TB);
                    //txt_Debugger.AppendText("\r\n" + "Stop Remove: " + TB.Name);
                }
            }
        }
        private async void btn_Start_ClickAsync(object sender, EventArgs e)
        {
            if (btn_Start.Text == "Run")
            {
                Get_FileSize();
                if (tmpfile != txt_Infile.Text)
                {
                    //Clear all and start a new
                    Panel_Tag.Controls.Clear();
                    txt_ReadLogs.Text = string.Empty;
                }
                try
                {
                    txt_ReadLogs.BringToFront();
                    txt_ReadLogs.Visible = true;
                    if (Panel_Tag.Controls.Count == 0 && txt_search.Text == string.Empty)
                    {
                        var msg = MessageBox.Show("Read all lines?", "Think carefully", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (msg == DialogResult.OK)
                        {
                            RemoveReadLogsControls();
                            btn_Start.Text = "Stop";
                            await Blank_Log_Output();
                        }
                    }
                    else if (Panel_Tag.Controls.Count > 0 && txt_search.Text == string.Empty)
                    {
                        foreach (TextBox TB in Panel_ReadLogs.Controls)
                        {
                            if (TB.Name != "txt_ReadLogs")
                            {
                                TB.Visible = false;
                                //txt_Debugger.AppendText("\r\n" + "Stop Remove: " + TB.Name);
                            }
                        }
                        btn_press = true;
                        btn_Start.Text = "Stop";
                        await Log_Output();
                    }
                    else if (Panel_Tag.Controls.Count > 0)
                    {
                        string[] splitText;
                        splitText = txt_search.Text.Split(',');
                        foreach (string searchText in splitText)
                        {
                            CreateTags(searchText);
                        }
                        foreach (TextBox TB in Panel_ReadLogs.Controls)
                        {
                            if (TB.Name != "txt_ReadLogs")
                            {
                                TB.Visible = false;
                                //txt_Debugger.AppendText("\r\n" + "Stop Remove: " + TB.Name);
                            }
                        }
                        btn_press = true;
                        btn_Start.Text = "Stop";
                        await Log_Output();
                    }
                    else if (txt_search.Text.Last() == ',')
                    {
                        RemoveReadLogsControls();
                        btn_press = false;
                        txt_search.Text = txt_search.Text.Substring(0, txt_search.Text.Length - 1);
                        btn_Start.Text = "Stop";
                        await Log_Output();
                    }
                    else
                    {
                        RemoveReadLogsControls();
                        btn_press = false;
                        btn_Start.Text = "Stop";
                        await Log_Output();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "btn_Start_ClickAsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if(btn_Start.Text == "Stop")
            {
                loopthis = false;
                btn_Start.Text = "Run";
            }
        }

        private void btn_OpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(txt_Infile.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "btn_OpenFile_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ClearDebugger_Click(object sender, EventArgs e)
        {
            txt_Debugger.Text = "";
        }
        private void CloseApp(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }     
}
