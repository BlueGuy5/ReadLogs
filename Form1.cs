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
            btn_stop.Enabled = false;
            txt_search.Text = "reconnect attempt";
            txt_Infile.Text = @"C:\Users\williamyu\Desktop\BT.txt";
        }
        bool cont = false;
        private async Task Log_Output()
        {
            string line;
            string file = txt_Infile.Text;
            string[] splitText;
            cont = true;

            txt_ReadLogs.Text = "";
            splitText = txt_search.Text.Split(',');

            //FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //StreamReader reader = new StreamReader(stream);
            using (FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
            while (cont == true)
            {
                Thread.Sleep(1);
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    foreach(string Logline in splitText)
                    {
                        if (line.Contains(Logline))
                        {
                            txt_ReadLogs.AppendText(line + "\r\n");
                        }
                    }
                       
                }
            }
        }
        private async void btn_Start_ClickAsync(object sender, EventArgs e)
        {
            btn_stop.Enabled = true;
            btn_Start.Enabled = false;
            await Log_Output();    
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            cont = false;
            btn_stop.Enabled = false;
            btn_Start.Enabled = true;
        }

        private void btn_OpenFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(txt_Infile.Text);
        }
    }     
}
