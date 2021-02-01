using BTC;
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
            public bool keepalive { get; set; }
            public bool btn_press { get; set; }
            public RichTextBox txt_multilogs { get; set; }
            public List<string> list_commands { get; set; }
            public bool RequestCurrentReaderPOS { get; set; }
            public long GetCurrentReaderPOS { get; set; }
            public int DebugMode { get; set; }
        }
        public GlobalVar _GlobalVar = new GlobalVar();
        List<string> list_commands = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            //txt_search.Text = "(dsp_earcon) Attempting to start a new download of version,reconnect attempt";
            //txt_Infile.Text = @"C:\Users\williamyu\Desktop\Frames_Logs\";
            this.Location = new Point(this.Location.X - 300, this.Location.Y);
            //await Get_CPU_Usage();
            //Get_Ram_Usage();
        }
        /*
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
        */
        private void Get_FileSize()
        {

            string file = groupBox_infile.Text;
            using (FileStream FS = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                long filesize = FS.Length / 1024;
                lbl_filesize.Text = filesize.ToString() + " KB";
            }

        }
        
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
            Panel_Keywords.Left = Panel_Main.Width + Panel_Keywords.Width * (Panel_Tag.Controls.Count - 2);

            Panel_Keywords.Controls.Add(btn_close_keyword);
            Panel_Keywords.Controls.Add(btn_SearchText);
            Panel_Tag.Controls.Add(Panel_Keywords);
            
            _GlobalVar.txt_multilogs = new RichTextBox();
            _GlobalVar.txt_multilogs.Name = searchText;
            _GlobalVar.txt_multilogs.Dock = DockStyle.Fill;
            _GlobalVar.txt_multilogs.Visible = false;
            _GlobalVar.txt_multilogs.Multiline = true;
            _GlobalVar.txt_multilogs.Font = new Font("Serif", 11);
            _GlobalVar.txt_multilogs.Width = txt_ReadLogs.Width;
            _GlobalVar.txt_multilogs.Height = txt_ReadLogs.Height;
            _GlobalVar.txt_multilogs.Location = txt_ReadLogs.Location;
            _GlobalVar.txt_multilogs.ScrollBars = RichTextBoxScrollBars.Vertical;
            _GlobalVar.txt_multilogs.ReadOnly = true;
            _GlobalVar.txt_multilogs.BackColor = Color.Black;
            _GlobalVar.txt_multilogs.ForeColor = Color.GhostWhite;
            _GlobalVar.txt_multilogs.HideSelection = false;
            _GlobalVar.txt_multilogs.BorderStyle = BorderStyle.None;
            Panel_ReadLogs.Controls.Add(_GlobalVar.txt_multilogs);
        }
        private void RemoveReadLogsControls()
        {
            //must remove any existing controls before we proceed again.
            foreach (RichTextBox TB in Panel_ReadLogs.Controls)
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
            while (Panel_Tag.Controls.Count > 2)
            {
                foreach (Panel _Panel in Panel_Tag.Controls.OfType<Panel>())
                {
                    if (_Panel.Name != "Panel_Main")
                    {
                        _Panel.Dispose();
                        Panel_Tag.Controls.Remove(_Panel);
                    }
                }
            }
        }

        private async Task CreateNewStreamReaderAsync(long ReaderPOS, RichTextBox TB, int linenum)
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
                    txt_ReadLogs.AppendText("\r" + "[" + GetActualPosition(reader).ToString() + "]" + line + "\r\n");
                    TB.AppendText("\r" + line + "\r\n");
                    DLog.Debug_Write("3", "Current:" + _GlobalVar.GetCurrentReaderPOS.ToString(), "Actual" + GetActualPosition(reader).ToString(), "CreateNewStreamReaderAsync()", TB.Name, line);
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
        Debug_Log DLog = new Debug_Log();
        private async Task Log_Output()
        {
            string line;
            string kw_line;
            _GlobalVar.tmpfile = groupBox_infile.Text;
            string[] splitText;
            _GlobalVar.keepalive = true;
            bool check_keywords = false;
            long retReaderLinePOS;
            btn_Start.Text = "Stop";

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
            long TrackActualPOS = 0; //Used to track ActualPOS to make sure AcutalPOS is different before we read it. This is the script to output duplicate lines.
            using (FileStream stream = File.Open(_GlobalVar.tmpfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
            while (_GlobalVar.keepalive == true)
            {
                //await Task.Run(() => Thread.Sleep(1));
                while ((line = await reader.ReadLineAsync()) != null)
                {                    
                    if (_GlobalVar.RequestCurrentReaderPOS == true)
                    {
                         // Only need to run this if we create a new tab/instance during log read
                        _GlobalVar.GetCurrentReaderPOS = stream.Length;
                        _GlobalVar.RequestCurrentReaderPOS = false;
                    }
                    foreach (RichTextBox TB in Panel_ReadLogs.Controls)
                    {
                        foreach (string keyword in splitText)
                        {
                            if (TB.Name == keyword)
                            {
                                if (keyword.Contains('[') && keyword.Contains(']'))
                                {
                                    if (line.IndexOf(keyword.Replace(keyword.Substring(keyword.Length - 3), string.Empty), StringComparison.InvariantCultureIgnoreCase) >= 0)
                                    {
                                        txt_ReadLogs.AppendText("[" + GetActualPosition(reader).ToString() + "]" + line + "\r\n");
                                        TB.AppendText("[" + GetActualPosition(reader).ToString() + "]" + line + "\r\n");
                                        DLog.Debug_Write("2", "Current:" + _GlobalVar.GetCurrentReaderPOS.ToString(), "Actual" + GetActualPosition(reader).ToString(), "Log_Output()", keyword, line);
                                        int getNum = int.Parse(keyword.Substring(keyword.Length - 2, 1)) - 1;
                                        await CreateNewStreamReaderAsync(GetActualPosition(reader), TB, getNum);
                                    }
                                }
                                else if (line.IndexOf(keyword, StringComparison.InvariantCultureIgnoreCase) >= 0)
                                {
                                    //Calling GetActualPosition each time may cause slowdown. This need to be tested.
                                    if (_GlobalVar.GetCurrentReaderPOS < GetActualPosition(reader) && TrackActualPOS < GetActualPosition(reader))
                                    {
                                        txt_ReadLogs.AppendText("[" + GetActualPosition(reader).ToString() + "]" + line + "\r\n");
                                        DLog.Debug_Write("1", "Current:" + _GlobalVar.GetCurrentReaderPOS.ToString(), "Actual" + GetActualPosition(reader).ToString(), "Log_Output()", keyword, line);
                                        TrackActualPOS = GetActualPosition(reader);
                                    }
                                    TB.AppendText("[" + GetActualPosition(reader).ToString() + "]" + line + "\r\n");
                                }
                            }
                        }
                    }
                    //Should only run this block of code if we are searching commands!
                    if (check_keywords == true)
                    {
                        kw_line = line;
                        foreach (RichTextBox TB in Panel_ReadLogs.Controls)
                        {
                            foreach (string kw_keyword in List_command()) // change List_Sub_Keywords() to List_command
                            {
                                if (TB.Name == kw_keyword)
                                {
                                    if (kw_line.IndexOf(kw_keyword, StringComparison.InvariantCultureIgnoreCase) >= 0)
                                    {
                                        txt_ReadLogs.AppendText("[" + GetActualPosition(reader).ToString() + "]" + line + "\r\n");
                                        TB.AppendText("[" + GetActualPosition(reader).ToString() + "]" + line + "\r\n");
                                        retReaderLinePOS = GetActualPosition(reader);
                                        using (FileStream stream2 = File.Open(_GlobalVar.tmpfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                        using (StreamReader reader2 = new StreamReader(stream2))
                                        while ((kw_line = await reader.ReadLineAsync()) != null)
                                        {
                                            try
                                            {
                                                reader2.BaseStream.Seek(retReaderLinePOS, SeekOrigin.Begin);
                                                txt_ReadLogs.AppendText("[" + GetActualPosition(reader).ToString() + "]" + kw_line + "\r\n");
                                                TB.AppendText("[" + GetActualPosition(reader).ToString() + "]" + kw_line + "\r\n");
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
                                            if (_GlobalVar.keepalive == false)
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
                Get_FileSize();
                await Task.Run(() => Thread.Sleep(1));

                if (line == null)
                {
                    lbl_StreamReader.Text = "idle";
                    lbl_StreamReader.ForeColor = Color.Green;
                }

                if (_GlobalVar.keepalive == false)
                {
                    lbl_StreamReader.Text = "Close";
                    lbl_StreamReader.ForeColor = Color.Red;
                }
            }               
        }
        private async Task Blank_Log_Output()
        {
            string line;
            _GlobalVar.tmpfile = groupBox_infile.Text;
            _GlobalVar.keepalive = true;
            txt_ReadLogs.Text = "";
            txt_search.Text = "";
            btn_Start.Text = "Stop";
            using (FileStream stream = File.Open(_GlobalVar.tmpfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
            while (_GlobalVar.keepalive == true)
            {
                //await Task.Run(() => Thread.Sleep(1));
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    //if (line.IndexOf(txt_search.Text, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    //{
                        txt_ReadLogs.AppendText("[" + GetActualPosition(reader).ToString() + "]" + line + "\r\n");
                        DLog.Debug_Write("2", "Current:" + _GlobalVar.GetCurrentReaderPOS.ToString(), "Actual" + GetActualPosition(reader).ToString(), "Blank_Log_Output()", "", line);
                    //}
                    
                    if (_GlobalVar.keepalive == false)
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
                    //Get_FileSize(); //Do not run Get_FileSize() here or it will slow down when reading lines
                    await Task.Run(() => Thread.Sleep(1)); // Sleep here so script will note freeze/lag while reading lines
                }
                Get_FileSize();
                await Task.Run(() => Thread.Sleep(1)); // Sleep here so script will not run full cpu while idle

                if (line == null) //We are no longer getting new lines so we're now waiting.
                {
                    lbl_StreamReader.Text = "idle";
                    lbl_StreamReader.ForeColor = Color.Green;
                }

                if (_GlobalVar.keepalive == false) //Connection is closed. We should not be getting anything.
                {
                    lbl_StreamReader.Text = "Close";
                    lbl_StreamReader.ForeColor = Color.Red;
                }
            }         
        }
        private async Task Blank_Log_Output_End_Line()
        {
            string line;
            _GlobalVar.tmpfile = groupBox_infile.Text;
            _GlobalVar.keepalive = true;
            txt_ReadLogs.Text = "";
            txt_search.Text = "";
            long TrackActualPOS = 0; //Used to track ActualPOS to make sure AcutalPOS is different before we read it. This is the script to output duplicate lines.
            btn_Start.Text = "Stop";
            using (FileStream stream = File.Open(_GlobalVar.tmpfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
            while (_GlobalVar.keepalive == true)
            {
                //await Task.Run(() => Thread.Sleep(1));
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (_GlobalVar.RequestCurrentReaderPOS == true)
                    {
                        // Only need to run this if we create a new tab/instance during log read
                        _GlobalVar.GetCurrentReaderPOS = stream.Length;
                        _GlobalVar.RequestCurrentReaderPOS = false;

                    }
                    //if (line.IndexOf(txt_search.Text, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    //{
                        //Calling GetActualPosition each time may cause slowdown. This need to be tested.
                        if (_GlobalVar.GetCurrentReaderPOS < GetActualPosition(reader) && TrackActualPOS < GetActualPosition(reader))
                        {
                            txt_ReadLogs.AppendText("[" + GetActualPosition(reader).ToString() + "]" + line.Trim() + "\r\n");
                            DLog.Debug_Write("1", "Current:" + _GlobalVar.GetCurrentReaderPOS.ToString(), "Actual" + GetActualPosition(reader).ToString(), "Blank_Log_Output_End_Line()", "", line);
                            TrackActualPOS = GetActualPosition(reader);
                        }
                    //}

                    if (_GlobalVar.keepalive == false)
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
                    //Get_FileSize(); //Do not run Get_FileSize() here or it will slow down when reading lines
                    await Task.Run(() => Thread.Sleep(1)); // Sleep here so script will note freeze/lag while reading lines
                }
                Get_FileSize();
                await Task.Run(() => Thread.Sleep(1)); // Sleep here so script will not run full cpu while idle

                if (line == null) //We are no longer getting new lines so we're now waiting.
                {
                    lbl_StreamReader.Text = "idle";
                    lbl_StreamReader.ForeColor = Color.Green;
                }

                if (_GlobalVar.keepalive == false) //Connection is closed. We should not be getting anything.
                {
                    lbl_StreamReader.Text = "Close";
                    lbl_StreamReader.ForeColor = Color.Red;
                }
            }
        }
        private void btn_close_keyword_Click(object sender, EventArgs e)
        {
            Button _btn = (Button)sender;
            foreach(Panel _panel in Panel_Tag.Controls.OfType<Panel>())
            {
                if(_panel.Name == _btn.Name)
                {
                    _panel.Dispose();
                    Panel_Tag.Controls.Remove(_panel);
                    break;
                }
            }
            foreach(RichTextBox TB in Panel_ReadLogs.Controls)
            {
                if(TB.Name == _btn.Name)
                {
                    TB.Dispose();
                    Panel_ReadLogs.Controls.Remove(TB);
                    break;
                }
            }
            if (Panel_Tag.Controls.Count == 2)
            {
                txt_ReadLogs.BringToFront();
                txt_ReadLogs.Visible = true;
            }
            else if (Panel_Tag.Controls.Count > 2)
            {
                int PanelLeftPos = 0;
                foreach (Panel _panel in Panel_Tag.Controls.OfType<Panel>())
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
            foreach (RichTextBox TB in Panel_ReadLogs.Controls)
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
                    btn_Browse.Enabled = false;
                    _GlobalVar.btn_press = false;
                    txt_ReadLogs.BringToFront();
                    txt_ReadLogs.Visible = true;
                    if (Panel_Tag.Controls.Count == 2 && txt_search.Text == string.Empty)
                    {
                        var msg = MessageBox.Show("Read all lines?", "Hey", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (msg == DialogResult.OK)
                        {
                            RemoveReadLogsControls();
                            await Blank_Log_Output();
                        }
                        else
                        {
                            btn_Browse.Enabled = true;
                        }
                    }
                    else if (txt_search.Text.Last() == ',')
                    {
                        RemoveReadLogsControls();
                        txt_search.Text = txt_search.Text.Substring(0, txt_search.Text.Length - 1);
                        await Log_Output();
                    }
                    else
                    {
                        RemoveReadLogsControls();
                        await Log_Output();
                    }              
                }
                else if(btn_Start.Text == "Stop")
                {
                    _GlobalVar.keepalive = false;
                    btn_Start.Text = "Run";
                    btn_Browse.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "btn_Start_ClickAsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btn_Browse.Enabled = true;
            }
        }
        private async void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (Panel_Tag.Controls.Count == 2 && txt_search.Text == string.Empty)
                {
                    var msg = MessageBox.Show("Read all new lines?", "Hey", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (msg == DialogResult.OK)
                    {
                        //MessageBox.Show("empty field", "btn_add_click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _GlobalVar.RequestCurrentReaderPOS = true;
                        await Blank_Log_Output_End_Line();
                    }
                }
                else
                {
                    if (lbl_StreamReader.Text == "Close" || lbl_StreamReader.Text == "Ready")
                    {
                        Remove_Panel_Tags();
                        RemoveReadLogsControls();
                    }
                    _GlobalVar.btn_press = true;
                    if (Panel_Tag.Controls.Count > 1 && txt_search.Text == string.Empty)
                    {
                        foreach (RichTextBox TB in Panel_ReadLogs.Controls)
                        {
                            if (TB.Name != "txt_ReadLogs")
                            {
                                TB.Visible = false;
                            }
                        }
                    }
                    _GlobalVar.RequestCurrentReaderPOS = true;
                    await Log_Output();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "btn_Add_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btn_Start.Text = "Run";
            }
        }
        private void btn_OpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(groupBox_infile.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "btn_OpenFile_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        KW_Ref_Redesign KWrefRedesign = new KW_Ref_Redesign();
        private void F1KeyPress(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1)
            {
                KWrefRedesign.Show();
                KWrefRedesign.BringToFront();
            }
            else if(e.KeyCode == Keys.F12)
            {
                DLog.Show();
                DLog.BringToFront();
                _GlobalVar.DebugMode = 1;
            }
        }
        private void btn_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
            var dlgOK = dlg.ShowDialog();
            if (dlgOK == DialogResult.OK)
            {
                groupBox_infile.Text = dlg.FileName;
                Get_FileSize();
                RemoveReadLogsControls();
                Remove_Panel_Tags();
                txt_ReadLogs.Text = "";
                _GlobalVar.btn_press = false;
                DLog.txt_DebugLog.Text = string.Empty;
            }
        }
        private List<string> List_command()
        {
            return list_commands;
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
