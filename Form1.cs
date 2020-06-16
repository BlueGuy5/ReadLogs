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

        private void Form1_Load(object sender, EventArgs e)
        {
            txt_ReadLogs.BackColor = Color.White;
            btn_stop.Enabled = false;
            txt_search.Text = "(dsp_earcon) Attempting to start a new download of version,reconnect attempt";
            txt_Infile.Text = @"C:\Users\williamyu\Desktop\Frames_Logs\TestLogs.txt";
            Get_CPU_Usage();
            Get_Ram_Usage();
        }
        
        private async Task Get_CPU_Usage()
        {
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            dynamic firstValue = cpuCounter.NextValue();
            await Task.Delay(1000);
            //Thread.Sleep(500);
            dynamic secondvalue = cpuCounter.NextValue();
            txt_CPU.Text = secondvalue + "%";
            //txt_Debugger.AppendText("\r\n" + secondvalue + "%");
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
            long filesize = FS.Length;
            txt_filesize.Text = filesize.ToString() + " KB";
        }
        
        bool loopthis = false;
        bool btn_press = false;
        TextBox txt_multilogs;
        private async Task Log_Output()
        {
            string line;
            string file = txt_Infile.Text;
            string[] splitText;
            loopthis = true;

            //txt_ReadLogs.Text = "";

            splitText = txt_search.Text.Split(',');
            if (btn_press == false)
            {
                Panel_Tag.Controls.Clear();
                
                foreach (TextBox TB in Panel_ReadLogs.Controls)
                {
                    if(TB.Name != "txt_ReadLogs")
                    {
                        //TB.Text = string.Empty;
                        Panel_ReadLogs.Controls.Remove(TB);
                        txt_Debugger.AppendText("\r\n" + "Remove: " + TB.Name);
                    }
                    else if(TB.Name == "txt_ReadLogs")
                    {
                       TB.Text = string.Empty;
                    }      
                    
                }       
                
                foreach (string searchText in splitText)
                {
                    Button btn_SearchText = new Button();
                    btn_SearchText.Name = searchText;
                    btn_SearchText.Text = searchText;
                    btn_SearchText.Width = 150;
                    btn_SearchText.Height = Panel_Tag.Height;
                    btn_SearchText.BackColor = Color.AliceBlue;
                    btn_SearchText.Left = btn_SearchText.Width * (Panel_Tag.Controls.Count);
                    btn_SearchText.MouseDown += new MouseEventHandler(btn_SearchText_RightClickAsync);
                    Panel_Tag.Controls.Add(btn_SearchText);
                
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
                    //txt_Debugger.AppendText("\r\n" + "Tab Name: " + txt_multilogs.Name);
                    Panel_ReadLogs.Controls.Add(txt_multilogs);
                    
                }
            }
            txt_search.Text = "";

            foreach(TextBox TB in Panel_ReadLogs.Controls)
            {
                txt_Debugger.AppendText("\r\n" + "Log_Output" + "\r\n" + TB.Name + "\r\n" + TB.Text);
            }

            using (FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
                while (loopthis == true)
                {
                    Thread.Sleep(1);
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
                    Thread.Sleep(1);
                }
            lbl_StreamReader.Text = "Close";
            if (lbl_StreamReader.Text == "Close")
            {
                lbl_StreamReader.ForeColor = Color.Red;
            }
        }

        private async Task Blank_Log_Output()
        {
            string line;
            string file = txt_Infile.Text;
            loopthis = true;

            txt_ReadLogs.Text = "";
            if (btn_press == false)
            {
                Panel_Tag.Controls.Clear();
            }
            txt_search.Text = "";
            using (FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
            while (loopthis == true)
            {
                Thread.Sleep(1);
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
            }
            lbl_StreamReader.Text = "Close";
            if (lbl_StreamReader.Text == "Close")
            {
                lbl_StreamReader.ForeColor = Color.Red;
            }
            await Get_CPU_Usage();
            Get_FileSize();
            Get_Ram_Usage();
            Thread.Sleep(1);
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
            //int cnt = 0;
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
                    //cnt++;
                }
                else
                {
                    TB.Visible = false;
                }             
            }
        }
        private async void btn_Start_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                //must remove any existing controls before we proceed again.
                foreach (TextBox TB in Panel_ReadLogs.Controls)
                {
                    if (TB.Name != "txt_ReadLogs")
                    {
                        Panel_ReadLogs.Controls.Remove(TB);
                        //txt_Debugger.AppendText("\r\n" + "Stop Remove: " + TB.Name);
                    }
                    //else if (TB.Name == "txt_ReadLogs")
                    //{
                    //    TB.Text = string.Empty;
                    //}
                }
                txt_ReadLogs.BringToFront();
                txt_ReadLogs.Visible = true;
                if (txt_search.Text == string.Empty)
                {
                    btn_stop.Enabled = true;
                    btn_Start.Enabled = false;
                    await Blank_Log_Output();
                }
                else if(txt_search.Text.Last() == ',')
                {
                    btn_press = false;
                    txt_search.Text = txt_search.Text.Substring(0,txt_search.Text.Length - 1);
                    btn_stop.Enabled = true;
                    btn_Start.Enabled = false;
                    await Log_Output();
                }
                else
                {
                    btn_press = false;
                    btn_stop.Enabled = true;
                    btn_Start.Enabled = false;
                    await Log_Output();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "btn_Start_ClickAsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            loopthis = false;
            btn_stop.Enabled = false;
            btn_Start.Enabled = true;
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
