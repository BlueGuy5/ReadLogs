using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txt_ReadLogs.BackColor = Color.White;
            btn_stop.Enabled = false;
            //txt_search.Text = "reconnect attempt,Freeing txn cmd data";
            txt_Infile.Text = @"C:\Users\williamyu\Desktop\";
        }
        bool loopthis = false;
        //bool clickloop = false;
        bool btn_press = false;
        private async Task Log_Output()
        {
            string line;
            string file = txt_Infile.Text;
            string[] splitText;
            loopthis = true;

            //Panel_Tag.Controls.Clear();
            txt_ReadLogs.Text = "";

            splitText = txt_search.Text.Split(',');
            if (btn_press == false)
            {
                Panel_Tag.Controls.Clear();
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
                }
            }
            txt_search.Text = "";
            using (FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
            while (loopthis == true)
            {
                Thread.Sleep(1);
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    foreach (string Logline in splitText)
                    {
                        if (line.IndexOf(Logline, StringComparison.InvariantCultureIgnoreCase) >= 0)
                        {
                            txt_ReadLogs.AppendText(line + "\r\n");
                        } 
                        lbl_StreamReader.Text = "Open";
                    }                   
                }
            }
            lbl_StreamReader.Text = "Close";
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
                }
            }
            lbl_StreamReader.Text = "Close";
        }

        private async void btn_SearchText_RightClickAsync(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("Enter this rightclick sub", "mouse check");
            Button _btn_SearchText = (Button)sender;
            //btn_press = true;
            if (e.Button == MouseButtons.Right)
            {
                //btn_press = true;
                txt_search.AppendText(_btn_SearchText.Name + ",");
            }
            else if (e.Button == MouseButtons.Left)
            {
                //script must be manually stopped, if not, for some reason it'll freeze after clicking the tabs.
                if (btn_stop.Enabled == true)
                {
                    loopthis = false;
                    btn_stop.Enabled = false;
                    btn_Start.Enabled = true;
                    //MessageBox.Show("Button Stop State = " + btn_stop.Enabled.ToString(), "StreamReader is Open", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    await Task.Delay(3);
                //}
                //else
                //{

                    btn_press = true;
                    //btn_stop_Click(sender, e);
                    //Button _btn_SearchText = (Button)sender;
                    txt_search.AppendText(_btn_SearchText.Name);
                    //Thread.Sleep(2);
                    try
                    {
                        btn_stop.Enabled = true;
                        btn_Start.Enabled = false;
                        await Log_Output();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "btn_searchText_click", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private async void btn_Start_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                //btn_press = false;
                //Panel_Tag.Controls.Clear();
                if(txt_search.Text == string.Empty)
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
                System.Diagnostics.Process.Start(txt_Infile.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "btn_OpenFile_Click", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }     
}
