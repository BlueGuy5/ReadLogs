using Read_logs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTC
{
    public partial class KW_Ref_Redesign : Form
    {
        public KW_Ref_Redesign()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(F2KeyPress);
            this.FormClosing += new FormClosingEventHandler(Form_Closing);
        }
        public struct GlobalVar
        {
            public bool NodeClicked { get; set; }
        }
        GlobalVar _GlobalVar = new GlobalVar();
        private void KW_Ref_Redesign_Load(object sender, EventArgs e)
        {
            Form f = Application.OpenForms["Form1"];
            var ext_Form = ((Form1)f);
            //this.Height = ext_Form.Height / 2;
            if (ext_Form.WindowState == FormWindowState.Maximized)
            {
                this.Height = ext_Form.Height / 2;
                this.Location = new Point((ext_Form.Location.X + ext_Form.Width) - this.Width, ext_Form.Location.Y);
            }
            else
            {
                this.Height = ext_Form.Height;
                this.Location = new Point(ext_Form.Location.X + ext_Form.Width - 20, ext_Form.Location.Y);
            }
            treeView1.ExpandAll();          
            //treeView1.SelectedNode = null; //Cancel out selection that automatically gets selected by ExpandAll()
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_GlobalVar.NodeClicked == true)
            {
                Form f = Application.OpenForms["Form1"];
                var ext_Form = ((Form1)f);

                if (treeView1.SelectedNode.Nodes.Count == 0) //This mean nodes does not have any child
                {
                    if (treeView1.SelectedNode.Text == "(Select All)")
                    {
                        try
                        {
                            var nodesList = new List<TreeNode>();
                            Addchildren(nodesList, treeView1.Nodes[0].Nodes[e.Node.Parent.Index]);//[treeview][child nodes index][Parent Node Index]

                            foreach (TreeNode strNode in nodesList)
                            {
                                ext_Form.txt_search.AppendText(strNode.Text + ",");
                            }
                            ext_Form.txt_search.Text = ext_Form.txt_search.Text.Substring(0, ext_Form.txt_search.Text.Length - 1);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "treeView1_AfterSelect()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {

                        if (ext_Form.txt_search.Text != string.Empty)
                        {
                            ext_Form.txt_search.AppendText("," + treeView1.SelectedNode.Text);
                        }
                        else
                        {
                            ext_Form.txt_search.AppendText(treeView1.SelectedNode.Text);
                        }
                    }
                }
                treeView1.SelectedNode = null; //Cancel out selection so we can re-select the same node
            }
        }
        private void Treeview1Click(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                _GlobalVar.NodeClicked = true;
            }
        }
        private void Addchildren(List<TreeNode> Nodes, TreeNode Node)
        {
            try
            {
                foreach (TreeNode nodes in Node.Nodes)
                {
                    if (nodes.Text != "(Select All)")
                    {
                        Nodes.Add(nodes);
                        Addchildren(Nodes, nodes);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Addchildren()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void F2KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
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
