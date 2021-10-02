using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warehouse
{
    public partial class CreateForm : Form
    {
        TreeView tree;
        TreeNode node;
        public CreateForm(TreeView tree)
        {
            InitializeComponent();
            this.tree = tree;
        }
        public CreateForm(TreeNode node)
        {
            InitializeComponent();
            this.node = node;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            if (Equals(node, null))
            {
                foreach (TreeNode treeNode in tree.Nodes)
                {
                    if (treeNode.Text == name)
                    {
                        MessageBox.Show("Данное имя уже зарезервированно тут");
                        return;
                    }
                }
                tree.Nodes.Add(name);
            }
            else
            {
                foreach (TreeNode treeNode in node.Nodes)
                {
                    if (treeNode.Text == name)
                    {
                        MessageBox.Show("Данное имя уже зарезервированно тут");
                        return;
                    }
                }
                node.Nodes.Add(name);
            }
            Close();
        }
    }
}
