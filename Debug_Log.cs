using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Read_logs.Form1;

namespace BTC
{
    public partial class Debug_Log : Form
    {
        public Debug_Log()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(F2KeyPress);
            this.FormClosing += new FormClosingEventHandler(Form_Closing);
        }
        public GlobalVar _globalvar = new GlobalVar();
        private void Debug_Log_Load(object sender, EventArgs e)
        {
            
            _globalvar.DebugMode = 1;
        }
        public void Debug_Write(string num, string POS, string ActualPOS, string function, string keyword, string line)
        {
            if (_globalvar.DebugMode == 1)
            {
                try
                {
                    txt_DebugLog.AppendText(string.Format("{0}{6}{1}{0}{7}{1}{0}{8}{1}{0}{3}{1}{0}{4}{1}{0}{5}{1}{2}", "[", "]", "\r\n", function, keyword, line, num, POS, ActualPOS));
                }
                catch (Exception ex)
                {
                    txt_DebugLog.AppendText(ex.Message + "\r\n");
                }
            }
        }
        private void F2KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                _globalvar.DebugMode = 0;
                //txt_DebugLog.Text = string.Empty;
                this.Hide();
            }
        }
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
