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
using System.Runtime.InteropServices.ComTypes;
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
        public struct GlobalVar
        {
            public string tmpfile { get; set; }
            public bool loopthis { get; set; }
            public bool btn_press { get; set; }
            public TextBox txt_multilogs { get; set; }
            public List<string> list_commands { get; set; }
            public bool RequestCurrentReaderPOS { get; set; }
            public long GetCurrentReaderPOS { get; set; }
        }
        public GlobalVar _GlobalVar = new GlobalVar();
        List<string> list_commands = new List<string>();

        private async void Form1_Load(object sender, EventArgs e)
        {
            //txt_search.Text = "(dsp_earcon) Attempting to start a new download of version,reconnect attempt";
            //txt_Infile.Text = @"C:\Users\williamyu\Desktop\Frames_Logs\";
            this.Location = new Point(this.Location.X - 300, this.Location.Y);
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
            using (FileStream FS = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                long filesize = FS.Length / 1024;
                txt_filesize.Text = filesize.ToString() + " KB";
            }
        }
        
        private void CreateTags(string searchText)
        {
            //GlobalVar _GlobalVar = new GlobalVar();

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
            Panel_Keywords.Left = Panel_Main.Width + Panel_Keywords.Width * (Panel_Tag.Controls.Count - 1);

            Panel_Keywords.Controls.Add(btn_close_keyword);
            Panel_Keywords.Controls.Add(btn_SearchText);
            Panel_Tag.Controls.Add(Panel_Keywords);
            
            _GlobalVar.txt_multilogs = new TextBox();
            _GlobalVar.txt_multilogs.Name = searchText;
            _GlobalVar.txt_multilogs.Dock = DockStyle.Fill;
            _GlobalVar.txt_multilogs.Visible = false;
            _GlobalVar.txt_multilogs.Multiline = true;
            _GlobalVar.txt_multilogs.Font = new Font("Serif", 11);
            _GlobalVar.txt_multilogs.Width = txt_ReadLogs.Width;
            _GlobalVar.txt_multilogs.Height = txt_ReadLogs.Height;
            _GlobalVar.txt_multilogs.Location = txt_ReadLogs.Location;
            _GlobalVar.txt_multilogs.ScrollBars = ScrollBars.Vertical;
            _GlobalVar.txt_multilogs.ReadOnly = true;
            _GlobalVar.txt_multilogs.BackColor = Color.Black;
            _GlobalVar.txt_multilogs.ForeColor = Color.GhostWhite;
            Panel_ReadLogs.Controls.Add(_GlobalVar.txt_multilogs);
        }
        private void RemoveReadLogsControls()
        {
            //must remove any existing controls before we proceed again.
            foreach (TextBox TB in Panel_ReadLogs.Controls)
            {
                if (TB.Name != "txt_ReadLogs")
                {
                    TB.Dispose();
                    Panel_ReadLogs.Controls.Remove(TB);
                }
            }
        }
        private void Remove_Panel_Tags()
        {
            while (Panel_Tag.Controls.Count > 1)
            {
                foreach (Panel _Panel in Panel_Tag.Controls)
                {
                    if (_Panel.Name != "Panel_Main")
                    {
                        _Panel.Dispose();
                        Panel_Tag.Controls.Remove(_Panel);
                    }
                }
            }
        }

        private async Task CreateNewStreamReaderAsync(long ReaderPOS, TextBox TB, int linenum)
        {
            string line;
            long ReaderPOSMAx = ReaderPOS + linenum;
            using (FileStream stream = File.Open(_GlobalVar.tmpfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
            try
            {
                reader.BaseStream.Seek(ReaderPOS, SeekOrigin.Begin);
                while ((line = await reader.ReadLineAsync()) != null)
                {                 
                    txt_ReadLogs.AppendText("\r" + line + "\r\n");
                    TB.AppendText("\r" + line + "\r\n");                       
                    if (ReaderPOS > ReaderPOSMAx)
                    {
                        reader.BaseStream.Seek(ReaderPOS, SeekOrigin.End);
                        txt_ReadLogs.AppendText("****************" + "\r\n");
                        TB.AppendText("****************" + "\r\n");
                        break;
                    }
                        ReaderPOS++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CreateNewStreamReader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task Log_Output()
        {
            string line;
            string kw_line;
            _GlobalVar.tmpfile = txt_Infile.Text;
            string[] splitText;
            _GlobalVar.loopthis = true;
            bool check_keywords = false;
            long retReaderLinePOS;

            splitText = txt_search.Text.Split(',');

            if (_GlobalVar.btn_press == false)
            {
                RemoveReadLogsControls();             
            }

            foreach (string searchText in splitText)
            {
                if (searchText.Substring(0, 1) == "$")
                {
                    var newSearchText = searchText.Substring(1, searchText.Length - 1);
                    CreateTags(newSearchText);
                }
                else
                {
                    CreateTags(searchText);
                }
            }

            List_command().Clear();
            foreach (string searchText in splitText)
            {             
                if (searchText.Substring(0,1) == "$")
                {
                    //Add searchString to Command list here.
                    List_command().Add(searchText.Replace("$",""));
                    check_keywords = true;
                }                           
            }
            txt_search.Text = "";
            using (FileStream stream = File.Open(_GlobalVar.tmpfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
            while (_GlobalVar.loopthis == true)
            {
                Thread.Sleep(int.Parse(txt_wait.Text));
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (_GlobalVar.RequestCurrentReaderPOS == true)
                    {
                        _GlobalVar.GetCurrentReaderPOS = stream.Length;
                        _GlobalVar.RequestCurrentReaderPOS = false;
                    }
                    foreach (TextBox TB in Panel_ReadLogs.Controls)
                    {
                        foreach (string keyword in splitText)
                        {
                            if (TB.Name == keyword)
                            {
                                if (line.IndexOf(keyword.Replace(keyword.Substring(keyword.Length - 3), string.Empty), StringComparison.InvariantCultureIgnoreCase) >= 0)
                                {
                                    if (keyword.Contains('[') && keyword.Contains(']'))
                                    {
                                        txt_ReadLogs.AppendText(line + "\r\n");
                                        TB.AppendText(line + "\r\n");
                                        int getNum = int.Parse(keyword.Substring(keyword.Length - 2, 1)) - 1;
                                        await CreateNewStreamReaderAsync(GetActualPosition(reader), TB, getNum);
                                    }
                                    else if (line.IndexOf(keyword,StringComparison.InvariantCultureIgnoreCase) >=0)
                                    {
                                        //Calling GetActualPosition each time may cause slowdown. This need to be tested.
                                        if (_GlobalVar.GetCurrentReaderPOS < GetActualPosition(reader))
                                        {
                                            txt_ReadLogs.AppendText(line + "\r\n");
                                        }
                                        TB.AppendText(line + "\r\n");
                                    }
                                }                                                                
                            }
                        }
                    }
                    //Should only run this block of code if we are searching commands!
                    if (check_keywords == true)
                    {
                        kw_line = line;
                        foreach (TextBox TB in Panel_ReadLogs.Controls)
                        {
                            foreach (string kw_keyword in List_command()) // change List_Sub_Keywords() to List_command
                            {
                                if (TB.Name == kw_keyword)
                                {
                                    if (kw_line.IndexOf(kw_keyword, StringComparison.InvariantCultureIgnoreCase) >= 0)
                                    {
                                        txt_ReadLogs.AppendText(line + "\r\n");
                                        TB.AppendText(line + "\r\n");
                                        retReaderLinePOS = GetActualPosition(reader);
                                        using (FileStream stream2 = File.Open(_GlobalVar.tmpfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                        using (StreamReader reader2 = new StreamReader(stream2))
                                            while ((kw_line = await reader.ReadLineAsync()) != null)
                                            {
                                                try
                                                {
                                                    reader2.BaseStream.Seek(retReaderLinePOS, SeekOrigin.Begin);
                                                    txt_ReadLogs.AppendText(kw_line + "\r\n");
                                                    TB.AppendText(kw_line + "\r\n");
                                                    if (kw_line.IndexOf("$", StringComparison.InvariantCultureIgnoreCase) >= 0)
                                                    {
                                                        reader2.BaseStream.Seek(retReaderLinePOS, SeekOrigin.End);
                                                        txt_ReadLogs.AppendText("****************" + "\r\n");
                                                        TB.AppendText("****************" + "\r\n");
                                                        break;
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.Message, "Reader2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                                #region Stop
                                                if (_GlobalVar.loopthis == false)
                                                {
                                                    lbl_StreamReader.Text = "Close";
                                                    lbl_StreamReader.ForeColor = Color.Red;
                                                    break;
                                                }
                                                #endregion
                                            }
                                    }
                                }
                            }
                        }
                    }
                    if (line != null)
                    {
                        lbl_StreamReader.Text = "reading";
                        lbl_StreamReader.ForeColor = Color.Blue;
                    }
                }
                await Get_CPU_Usage();
                Get_FileSize();
                Get_Ram_Usage();
                Thread.Sleep(int.Parse(txt_wait.Text));

                if (line == null)
                {
                    lbl_StreamReader.Text = "pending";
                    lbl_StreamReader.ForeColor = Color.Green;
                }

                if (_GlobalVar.loopthis == false)
                {
                    lbl_StreamReader.Text = "Close";
                    lbl_StreamReader.ForeColor = Color.Red;
                }
            }               
        }
        private async Task Blank_Log_Output()
        {
            string line;
            _GlobalVar.tmpfile = txt_Infile.Text;
            _GlobalVar.loopthis = true;
            txt_ReadLogs.Text = "";
            txt_search.Text = "";
            using (FileStream stream = File.Open(_GlobalVar.tmpfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
            while (_GlobalVar.loopthis == true)
            {
                Thread.Sleep(int.Parse(txt_wait.Text));
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.IndexOf(txt_search.Text, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        txt_ReadLogs.AppendText(line + "\r\n");
                    }
                    
                    if (_GlobalVar.loopthis == false)
                    {
                        lbl_StreamReader.Text = "Close";
                        lbl_StreamReader.ForeColor = Color.Red;
                        break;
                    }
                    if (line != null) //Let us know that we are still getting new lines and this loop is alive
                    {
                        lbl_StreamReader.Text = "reading";
                        lbl_StreamReader.ForeColor = Color.Blue;
                    }
                }
                await Get_CPU_Usage();
                Get_FileSize();
                Get_Ram_Usage();
                Thread.Sleep(int.Parse(txt_wait.Text));

                if (line == null) //We are no longer getting new lines so we're now waiting.
                {
                    lbl_StreamReader.Text = "pending";
                    lbl_StreamReader.ForeColor = Color.Green;
                }

                if (_GlobalVar.loopthis == false) //Connection is closed. We should not be getting anything.
                {
                    lbl_StreamReader.Text = "Close";
                    lbl_StreamReader.ForeColor = Color.Red;
                }
            }         
        }
        private void btn_close_keyword_Click(object sender, EventArgs e)
        {
            Button _btn = (Button)sender;
            foreach(Panel _panel in Panel_Tag.Controls)
            {
                if(_panel.Name == _btn.Name)
                {
                    _panel.Dispose();
                    Panel_Tag.Controls.Remove(_panel);
                    break;
                }
            }
            foreach(TextBox TB in Panel_ReadLogs.Controls)
            {
                if(TB.Name == _btn.Name)
                {
                    TB.Dispose();
                    Panel_ReadLogs.Controls.Remove(TB);
                    break;
                }
            }
            if (Panel_Tag.Controls.Count == 1)
            {
                txt_ReadLogs.BringToFront();
                txt_ReadLogs.Visible = true;
            }
            else if (Panel_Tag.Controls.Count > 1)
            {
                int PanelLeftPos = 0;
                foreach (Panel _panel in Panel_Tag.Controls)
                {
                    _panel.Left = Panel_Main.Width + _panel.Width * (PanelLeftPos - 1);
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
                    _GlobalVar.btn_press = true;
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

        private async void btn_Start_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (btn_Start.Text == "Run")
                {
                    _GlobalVar.GetCurrentReaderPOS = 0;
                    _GlobalVar.RequestCurrentReaderPOS = false;
                    txt_Infile.Enabled = false;
                    btn_Browse.Enabled = false;
                    _GlobalVar.btn_press = false;
                    Get_FileSize();
                    txt_ReadLogs.BringToFront();
                    txt_ReadLogs.Visible = true;
                    if (Panel_Tag.Controls.Count == 1 && txt_search.Text == string.Empty)
                    {
                        var msg = MessageBox.Show("Read all lines?", "Think carefully", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (msg == DialogResult.OK)
                        {
                            RemoveReadLogsControls();
                            btn_Start.Text = "Stop";
                            await Blank_Log_Output();
                        }
                        else
                        {
                            txt_Infile.Enabled = true;
                            btn_Browse.Enabled = true;
                        }
                    }
                    else if (txt_search.Text.Last() == ',')
                    {
                        RemoveReadLogsControls();
                        txt_search.Text = txt_search.Text.Substring(0, txt_search.Text.Length - 1);
                        btn_Start.Text = "Stop";
                        await Log_Output();
                    }
                    else
                    {
                        RemoveReadLogsControls();
                        btn_Start.Text = "Stop";
                        await Log_Output();
                    }              
                }
                else if(btn_Start.Text == "Stop")
                {
                    _GlobalVar.loopthis = false;
                    btn_Start.Text = "Run";
                    txt_Infile.Enabled = true;
                    btn_Browse.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "btn_Start_ClickAsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_Infile.Enabled = true;
                btn_Browse.Enabled = true;
            }
        }
        private async void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbl_StreamReader.Text == "Close" || lbl_StreamReader.Text == "Ready")
                {
                    Remove_Panel_Tags();
                    RemoveReadLogsControls();
                }
                _GlobalVar.btn_press = true;
                if (Panel_Tag.Controls.Count > 1 && txt_search.Text == string.Empty)
                {
                    foreach (TextBox TB in Panel_ReadLogs.Controls)
                    {
                        if (TB.Name != "txt_ReadLogs")
                        {
                            TB.Visible = false;
                        }
                    }
                }
                _GlobalVar.RequestCurrentReaderPOS = true;
                btn_Start.Text = "Stop";
                await Log_Output();           
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "btn_Add_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        KW_Ref_Redesign KWrefRedesign = new KW_Ref_Redesign();
        private void F1KeyPress(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F12)
            {
                KW_Ref KWref = new KW_Ref();
                KWref.Show();
            }
            else if(e.KeyCode == Keys.F1)
            {
                KWrefRedesign.Show();
                KWrefRedesign.BringToFront();
            }
        }
        private void btn_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
            var dlgOK = dlg.ShowDialog();
            if (dlgOK == DialogResult.OK)
            {
                txt_Infile.Text = dlg.FileName;
                RemoveReadLogsControls();
                Remove_Panel_Tags();
                txt_ReadLogs.Text = "";
                txt_ReadLogs.Refresh(); //properly disposed main?
                _GlobalVar.btn_press = false;
            }
        }
        private List<string> List_command()
        {
            return list_commands;
            //return _GlobalVar.list_commands;
        }
        private void tag_readlog_Click(object sender, EventArgs e)
        {
            txt_ReadLogs.Visible = true;
            txt_ReadLogs.BringToFront();
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
