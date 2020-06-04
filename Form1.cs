using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
            /*
            //FileInfo Originalfile = new FileInfo(@"C:\Users\williamyu\Desktop\Alexa Logs\OOR_BT_Log_Test.txt");
            //FileInfo CopyFile = new FileInfo(@"C:\Users\williamyu\Desktop\Alexa Logs\OOR_BT_Log_Test - Copy.txt");

            //if (Originalfile.Length > CopyFile.Length)
            //{
                //txt_ReadLogs.Text = "";
                //var readlog = System.IO.File.ReadAllLines(@"C:\Users\williamyu\Desktop\Alexa Logs\OOR_BT_Log_Test.txt");
                if (System.IO.File.Exists(@"C:\Users\williamyu\Desktop\OTA - VS.txt"))
                {
                    System.IO.File.Delete(@"C:\Users\williamyu\Desktop\OTA - VS.txt");
                    //System.IO.File.WriteAllLines(@"C:\Users\williamyu\Desktop\Alexa Logs\OOR_BT_Log_Test - Copy.txt", readlog);
                }
                //else
                //{
                System.IO.File.Copy(@"C:\Users\williamyu\Desktop\OTA.txt", @"C:\Users\williamyu\Desktop\OTA - VS.txt");
                //}

                var readcopylog = System.IO.File.ReadAllLines(@"C:\Users\williamyu\Desktop\OTA - VS.txt");
                foreach (string lines in readcopylog)
                {
                    if (lines.Contains(txt_search.Text))
                    {
                        txt_ReadLogs.AppendText(lines + "\r\n");
                    }
                }
            //}
            */
            txt_search.Text = "reconnect attempt";
        }
        private async Task test_log()
        {
            string line;
            string file = @"C:\Users\williamyu\Desktop\BT.txt";
            bool cont = true;

            txt_ReadLogs.Text = "";
            using (FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(stream))
                while (cont == true)
                {
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        if (line.Contains(txt_search.Text))
                        {
                            txt_ReadLogs.AppendText(line + "\r\n");
                        }
                    }
                }
        }
        private async void btn_reload_ClickAsync(object sender, EventArgs e)
        {

            await test_log();
            /*
            int counter = 0;
            string line;
            while (counter == 0)
            {
                txt_ReadLogs.Text = "";
                using (FileStream stream = File.Open(@"C:\Users\williamyu\Desktop\BT.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader reader = new StreamReader(stream))

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(txt_search.Text))
                        {
                            txt_ReadLogs.AppendText(line + "\r\n");
                            counter++;
                        }
                        counter = 0;
                        //counter++;
                    }
                counter = 0;
            }
            */

            /*
            using (FileStream stream = File.Open(@"C:\Users\williamyu\Desktop\BT.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    txt_ReadLogs.Text = "";
                    while (!reader.EndOfStream)
                    {
                        
                        if (reader.ReadLine().Contains(txt_search.Text))
                        {
                            txt_ReadLogs.AppendText(reader.ReadLine() + "\r\n");
                        }  
                        
                        var readlog = System.IO.File.ReadAllLines(@"C:\Users\williamyu\Desktop\BT.txt");
                        foreach (string lines in readlog)
                        {
                            if (lines.Contains(txt_search.Text))
                            {
                                txt_ReadLogs.AppendText(lines + "\r\n");
                            }
                        }
                    }
                }
            }

            */

            /*
            var infile = @"C:\Users\williamyu\Desktop\BT.txt";
            var outfile = @"C:\Users\williamyu\Desktop\BT - VS.txt";
            //FileInfo Originalfile = new FileInfo(@"C:\Users\williamyu\Desktop\Alexa Logs\OOR_BT_Log_Test.txt");
            //FileInfo CopyFile = new FileInfo(@"C:\Users\williamyu\Desktop\Alexa Logs\OOR_BT_Log_Test - Copy.txt");

            //while (Originalfile.Length > CopyFile.Length)
            //{
                txt_ReadLogs.Text = "";
                if (System.IO.File.Exists(outfile))
                {
                    System.IO.File.Delete(outfile);
                    //System.IO.File.WriteAllLines(@"C:\Users\williamyu\Desktop\Alexa Logs\OOR_BT_Log_Test - Copy.txt", readlog);
                }
                //else
                //{
                System.IO.File.Copy(infile, outfile);
                //}

                var readcopylog = System.IO.File.ReadAllLines(outfile);
                foreach (string lines in readcopylog)
                {
                    if (lines.Contains(txt_search.Text))
                    {
                        txt_ReadLogs.AppendText(lines + "\r\n");
                    }
                }  
                */
        }
    }     
}
