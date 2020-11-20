using Read_logs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTC
{
    public partial class KW_Ref : Form
    {
        public KW_Ref()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(F1KeyPress);
        }

        private void KW_Ref_Load(object sender, EventArgs e)
        {

        }
        private void btn_click(object sender, EventArgs e)
        {
            Button getbtn = (Button)sender;
            Form f = Application.OpenForms["Form1"];
            var ext_Form = ((Form1)f);

            if (ext_Form.txt_search.Text != string.Empty)
            {
                ext_Form.txt_search.AppendText("," + getbtn.Text);
            }
            else
            {
                ext_Form.txt_search.AppendText(getbtn.Text);
            }
        }
        private void btn_title_Click(object sender, EventArgs e)
        {
            Form f = Application.OpenForms["Form1"];
            var ext_Form = ((Form1)f);
            Button getbtn = (Button)sender;
            var Get_Panel_Name = getbtn.Parent.Name;
            switch(Get_Panel_Name)
            {
                case "Panel_OTA":
                {
                    Panel_Case_Statements(Panel_OTA, getbtn.Text);
                    break;
                }
                case "Panel_Phone":
                    {
                        Panel_Case_Statements(Panel_Phone, getbtn.Text);
                        break;
                    }
                case "Panel_OOR":
                    {
                        Panel_Case_Statements(Panel_OOR, getbtn.Text);
                        break;
                    }
                case "Panel_Shipmode":
                    {
                        Panel_Case_Statements(Panel_Shipmode, getbtn.Text);
                        break;
                    }
            }
            //MessageBox.Show(test.ToString(), "dfsd");
        }
        private void Panel_Case_Statements(Panel _panels, string getbtn)
        {
            Form f = Application.OpenForms["Form1"];
            var ext_Form = ((Form1)f);
            foreach (Button btn in _panels.Controls)
            {
                if (btn.Text != getbtn)
                {
                    ext_Form.txt_search.AppendText(btn.Text + ",");
                }
            }
            //rebuild string to take away the comma from the last item.
            ext_Form.txt_search.Text = ext_Form.txt_search.Text.Substring(0, ext_Form.txt_search.Text.Length - 1); 
        }
        private void F1KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                this.Dispose();
            }
        }
    }
}
