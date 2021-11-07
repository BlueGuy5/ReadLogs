using Read_logs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

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
            XML_Patterns_Treeview();
            treeView1.ExpandAll();          
            treeView1.SelectedNode = null; //Cancel out selection that automatically gets selected by ExpandAll()
        }
        private void Form_Visibility_Change(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                XML_Patterns_Treeview();
                treeView1.ExpandAll();
                treeView1.SelectedNode = null; //Cancel out selection that automatically gets selected by ExpandAll()
            }
        }
        private void XML_Patterns_Treeview()
        {
            try
            {
                treeView1.Nodes.Clear();
                TreeNode rootnodes = new TreeNode();

                XDocument xml = XDocument.Load(@"C:\Users\" + Environment.UserName + @"\Documents\BTC\Patterns.xml");
                IEnumerable<string> ProjectNode = xml.Descendants("Node").Select(t => (string)t.Attribute("Project"));
                int node_cnt = 0;
                foreach (string t_node in ProjectNode)
                {
                    if (t_node != null) //for some reason titlenode has null values.
                    {
                        rootnodes = new TreeNode(t_node);
                        treeView1.Nodes.Add(rootnodes);
                        int c_node_cnt = 0;
                        var p_nodes = xml.Descendants("Node").Attributes(t_node);
                        foreach (string p_node in p_nodes)
                        {
                            if (p_node != null)
                            {
                                treeView1.Nodes[node_cnt].Nodes.Add(p_node);
                                var c_nodes = xml.Descendants("Node").Where(p => (string)p.Attribute(t_node) == p_node).Elements("pattern");
                                treeView1.Nodes[node_cnt].Nodes[c_node_cnt].Nodes.Add("(Select All)");
                                foreach (string c_node in c_nodes)
                                {
                                    treeView1.Nodes[node_cnt].Nodes[c_node_cnt].Nodes.Add(c_node.Trim());
                                }
                                c_node_cnt++;
                            }
                        }
                        node_cnt++; //this cnt needs to be last?
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "XML_Patterns_Treeview()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                            Addchildren(nodesList, treeView1.Nodes[e.Node.Parent.Parent.Index].Nodes[e.Node.Parent.Index]);//[treeview][calls parent twice to go up 2 levels][child Node Index]
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
            }
            _GlobalVar.NodeClicked = false;
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
