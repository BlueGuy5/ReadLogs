﻿using BTC;
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
            KeyDown += new KeyEventHandler(F1KeyPress);
            this.FormClosing += new FormClosingEventHandler(CloseApp);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //txt_search.Text = "(dsp_earcon) Attempting to start a new download of version,reconnect attempt";
            //txt_Infile.Text = @"C:\Users\williamyu\Desktop\Frames_Logs\";
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
            Panel_ReadLogs.Controls.Add(txt_multilogs);
        }
        private Dictionary<string,string> Dict_Sub_keywords()
        {
            var keywords = new Dictionary<string, string>();
            keywords.Add("bt dump profile_status", "Connected");
            keywords.Add("edsp fw_version", "DSP Addon FW version");
            //Useful shipmode commands
            keywords.Add("dtest battery_capacity", "Capacity");
            keywords.Add("bt unpair_all", "Bt_CB_MSG_PROFILE_DISCONNECTED");
            keywords.Add("dtest shipmode", "Set power mode");
            keywords.Add("zapp runtime", "Charging Mode");
            return keywords;
        }
        private async Task Log_Output()
        {
            string line;
            tmpfile = txt_Infile.Text;
            string[] splitText;
            loopthis = true;
            bool check_keywords = false;

            splitText = txt_search.Text.Split(',');
            if (btn_press == false)
            {
                Panel_Tag.Controls.Clear();
                RemoveReadLogsControls();
                foreach (string searchText in splitText)
                {
                    CreateTags(searchText);
                    foreach (KeyValuePair<string, string> kvp in Dict_Sub_keywords())
                    {
                        if (searchText.IndexOf(kvp.Key, StringComparison.InvariantCultureIgnoreCase) >= 0)
                        {
                            check_keywords = true;
                            break;
                        }
                    }
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
                                        TB.AppendText(line + "\r\n");
                                    }
                                    if (check_keywords == true)
                                    {
                                        foreach (KeyValuePair<string, string> kvp in Dict_Sub_keywords())
                                        {

                                            if (keyword.IndexOf(kvp.Key, StringComparison.InvariantCultureIgnoreCase) >= 0)
                                            {
                                                if (line.IndexOf(kvp.Value, StringComparison.InvariantCultureIgnoreCase) >= 0)
                                                {
                                                    txt_ReadLogs.AppendText(line + "\r\n");
                                                    TB.AppendText(line + "\r\n");
                                                }
                                            }
                                        }
                                    }
                                    
                                    /*
                                    foreach (KeyValuePair<string, string> kvp in Dict_Sub_keywords())
                                    {
                                        if (keyword.IndexOf(kvp.Key, StringComparison.InvariantCultureIgnoreCase) >= 0)
                                        {
                                            var test = GetActualPosition(reader);
                                            stream.Seek(test, SeekOrigin.Begin);
                                            line = await reader.ReadLineAsync();
                                            txt_ReadLogs.AppendText(line + "\r\n");
                                            TB.AppendText(line + "\r\n");
                                            if (line.IndexOf("$", StringComparison.InvariantCultureIgnoreCase) >= 0)
                                            {
                                                stream.Seek(test, SeekOrigin.End);
                                                //line = await reader.ReadLineAsync();
                                                txt_ReadLogs.AppendText(line + "\r\n");
                                                TB.AppendText(line + "\r\n");
                                            }
                                        }
                                    }
                                    */

                                }
                                //if (line.IndexOf(keyword, StringComparison.InvariantCultureIgnoreCase) >= 0)
                                //{

                                //}                                     

                            }

                        }
                    }
                    lbl_StreamReader.Text = "Open";
                    if (lbl_StreamReader.Text == "Open")
                    {
                        lbl_StreamReader.ForeColor = Color.Green;
                    }
                    if (btn_Start.Text != "Stop")
                    {
                        lbl_StreamReader.Text = "Close";
                        lbl_StreamReader.ForeColor = Color.Red;
                        break;
                    }
                }
            await Get_CPU_Usage();
            Get_FileSize();
            Get_Ram_Usage();
            Thread.Sleep(int.Parse(txt_wait.Text));     
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
                    if (btn_Start.Text != "Stop")
                    {
                        lbl_StreamReader.Text = "Close";
                        lbl_StreamReader.ForeColor = Color.Red;
                        break;
                    }
                }
                await Get_CPU_Usage();
                Get_FileSize();
                Get_Ram_Usage();
                Thread.Sleep(int.Parse(txt_wait.Text));
            }
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
                    TB.Visible = true;
                    TB.BringToFront();
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
                }
            }
        }
        private async void btn_Start_ClickAsync(object sender, EventArgs e)
        {
            if (btn_Start.Text == "Run")
            {
                txt_Infile.Enabled = false;
                btn_Browse.Enabled = false;
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
                txt_Infile.Enabled = true;
                btn_Browse.Enabled = true;
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
        private void F1KeyPress(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                KW_Ref KWref = new KW_Ref();
                KWref.Show();
            }
        }
        private void btn_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
            dlg.ShowDialog();
            txt_Infile.Text = dlg.FileName;
        }
        private void CloseApp(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        public static long GetActualPosition(StreamReader reader)
        {
            System.Reflection.BindingFlags flags = System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField;

            // The current buffer of decoded characters
            char[] charBuffer = (char[])reader.GetType().InvokeMember("charBuffer", flags, null, reader, null);

            // The index of the next char to be read from charBuffer
            int charPos = (int)reader.GetType().InvokeMember("charPos", flags, null, reader, null);

            // The number of decoded chars presently used in charBuffer
            int charLen = (int)reader.GetType().InvokeMember("charLen", flags, null, reader, null);

            // The current buffer of read bytes (byteBuffer.Length = 1024; this is critical).
            byte[] byteBuffer = (byte[])reader.GetType().InvokeMember("byteBuffer", flags, null, reader, null);

            // The number of bytes read while advancing reader.BaseStream.Position to (re)fill charBuffer
            int byteLen = (int)reader.GetType().InvokeMember("byteLen", flags, null, reader, null);

            // The number of bytes the remaining chars use in the original encoding.
            int numBytesLeft = reader.CurrentEncoding.GetByteCount(charBuffer, charPos, charLen - charPos);

            // For variable-byte encodings, deal with partial chars at the end of the buffer
            int numFragments = 0;
            if (byteLen > 0 && !reader.CurrentEncoding.IsSingleByte)
            {
                if (reader.CurrentEncoding.CodePage == 65001) // UTF-8
                {
                    byte byteCountMask = 0;
                    while ((byteBuffer[byteLen - numFragments - 1] >> 6) == 2) // if the byte is "10xx xxxx", it's a continuation-byte
                        byteCountMask |= (byte)(1 << ++numFragments); // count bytes & build the "complete char" mask
                    if ((byteBuffer[byteLen - numFragments - 1] >> 6) == 3) // if the byte is "11xx xxxx", it starts a multi-byte char.
                        byteCountMask |= (byte)(1 << ++numFragments); // count bytes & build the "complete char" mask
                                                                      // see if we found as many bytes as the leading-byte says to expect
                    if (numFragments > 1 && ((byteBuffer[byteLen - numFragments] >> 7 - numFragments) == byteCountMask))
                        numFragments = 0; // no partial-char in the byte-buffer to account for
                }
                else if (reader.CurrentEncoding.CodePage == 1200) // UTF-16LE
                {
                    if (byteBuffer[byteLen - 1] >= 0xd8) // high-surrogate
                        numFragments = 2; // account for the partial character
                }
                else if (reader.CurrentEncoding.CodePage == 1201) // UTF-16BE
                {
                    if (byteBuffer[byteLen - 2] >= 0xd8) // high-surrogate
                        numFragments = 2; // account for the partial character
                }
            }
            return reader.BaseStream.Position - numBytesLeft - numFragments;
        }
    }     
}
