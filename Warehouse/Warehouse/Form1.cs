using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Warehouse
{
    [Serializable]
    // Приложен README.txt, обязателен к прочтению.
    public partial class Form1 : Form
    {
        BindingSource source = new BindingSource();
        // Текущий выделенный узел.
        TreeNode selected;
        /// <summary>
        /// Инициализация компонентов.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            tree.ContextMenuStrip = contextMenuStrip1;
            Create.Click += Create_Click;
            Edit.Click += Edit_Click;
            Delete.Click += Delete_Click;
            AddProduct.Click += AddProduct_Click;
            tree.MouseDown += Tree_MouseDown;
            tree.AfterLabelEdit += Tree_AfterLabelEdit;
            tree.LabelEdit = true;
            tree.NodeMouseDoubleClick += Tree_NodeMouseDoubleClick;
            Edit.Visible = false;
            Delete.Visible = false;
            AddProduct.Visible = false;
            tree.NodeMouseClick += Tree_NodeMouseClick;
            dataGridView1.KeyDown += DataGridView1_KeyDown;
            dataGridView1.AllowUserToDeleteRows = false;
            OpenFile.Click += OpenFile_Click;
            SaveFile.Click += SaveFile_Click;
            CreateCsv.Click += CreateCsv_Click;
            tree.GotFocus += Tree_GotFocus;
            openFileDialog1.Filter = "(*.dat)|*.dat";
            saveFileDialog1.Filter = "(*.dat)|*.dat";
            saveFileDialog2.Filter = "(*.csv)|*.csv";
        }

        /// <summary>
        /// Метод, вызывающийся при потери фокуса TreeView.
        /// Отвечает за видимость элементов контекстного меню.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Tree_GotFocus(object sender, EventArgs e)
        {
            if (!Equals(tree.SelectedNode, null))
            {
                Edit.Visible = true;
                AddProduct.Visible = true;
            }
            if (!Equals(tree.SelectedNode, null))
            {
                if (tree.SelectedNode.Nodes.Count == 0)
                {
                    if (Equals(tree.SelectedNode.Tag, null))
                    {
                        Delete.Visible = true;
                    }
                    else
                    {
                        if (((List<Product>)(tree.SelectedNode.Tag)).Count == 0)
                        {
                            Delete.Visible = true;
                        }
                        else
                        {
                            Delete.Visible = false;
                        }
                    }
                }
                else
                    Delete.Visible = false;
            }
            else
                Delete.Visible = false;
        }

        /// <summary>
        /// Метод, вызывающийся при нажатии узла TreeView.
        /// Редактирует видимость элементов.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Edit.Visible = true;
            AddProduct.Visible = true;
            if (!Equals(tree.SelectedNode, null))
            {
                if (tree.SelectedNode.Nodes.Count == 0)
                {
                    if (Equals(tree.SelectedNode.Tag, null))
                    {
                        Delete.Visible = true;
                    }
                    else
                    {
                        if (((List<Product>)(tree.SelectedNode.Tag)).Count == 0)
                        {
                            Delete.Visible = true;
                        }
                        else
                        {
                            Delete.Visible = false;
                        }
                    }
                }
                else
                    Delete.Visible = false;
            }
            else
                Delete.Visible = false;
        }


        /// <summary>
        /// Создание отчета CSV.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void CreateCsv_Click(object sender, EventArgs e)
        {
            if (saveFileDialog2.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {
                string ans = "Полный путь;Артикул;Название;Количество\n";
                List<Product> products = new List<Product>();
                // Создание общего списков продуктов во всех узлах дерева.
                foreach (var node in Collect(tree.Nodes))
                {
                    // Добавление продуктов.
                    if (!Equals(((List<Product>)(node.Tag)), null))
                        products.AddRange((List<Product>)(node.Tag));
                }
                try
                {
                    foreach (Product product in products)
                    {
                        // Проверка на то, что поле не пусто.
                        if (!int.TryParse(product.minRemainder, out int x))
                        {
                            product.minRemainder = "0";
                        }
                        // Проверка на то, что поле не пусто.
                        if (!int.TryParse(product.Remainder, out int y))
                        {
                            product.Remainder = "0";
                        }
                        // Добавление товаров, где их количество меньше минимально нужного.
                        if (int.Parse(product.minRemainder) > int.Parse(product.Remainder))

                            ans += $"{product.node.FullPath};{product.VendorCode};{product.Called};{product.Remainder}\n";
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("");
                }
                // Запись товаров в *.csv.
                File.WriteAllLines(saveFileDialog2.FileName, ans.Split('\n'), Encoding.UTF8);
            }
            catch (Exception)
            { 
            }
        }

        /// <summary>
        /// Рекурсивный обход всех вершин дерева.
        /// </summary>
        /// <param name="nodes">Коллекция узлов</param>
        /// <returns></returns>
        private IEnumerable<TreeNode> Collect(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                yield return node;

                foreach (var child in Collect(node.Nodes))
                    yield return child;
            }
        }

        /// <summary>
        /// Сохранение по кнопке.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void SaveFile_Click(object sender, EventArgs e)
        {
            // Открытие диалогового окна.
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {
                // Сериализация.
                using (Stream file = File.Open(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(file, tree.Nodes.Cast<TreeNode>().ToList());
                }
            }
            catch (Exception)
            { 
            }
        }

        /// <summary>
        /// Открытие по кнопке.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void OpenFile_Click(object sender, EventArgs e)
        {
            // Открытие диалогового окна.
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            try
            {
                // Десереализация.
                using (Stream file = File.Open(openFileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    object obj = bf.Deserialize(file);
                    TreeNode[] nodeList = (obj as IEnumerable<TreeNode>).ToArray();
                    tree.Nodes.Clear();
                    tree.Nodes.AddRange(nodeList);
                }
            }
            catch (Exception)
            { 
            }
        }

        /// <summary>
        /// Событие по нажатию на DataGridView.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            // Удаление строки на Shift.
            if (e.Shift)
            {
                if (!Equals(dataGridView1.CurrentRow, null))
                {
                    // Удаление строки из тэга выбранного узла.
                    ((List<Product>)(selected.Tag))
                        .Remove(((List<Product>)(selected.Tag))
                        .Find(x => x.Called == dataGridView1.CurrentRow.Cells[0]
                        .Value.ToString()));
                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                    // Обновление таблицы.
                    dataGridView1.Refresh();
                }
            }
        }

        /// <summary>
        /// Открытие таблицы по двойному щелчку на узел.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Tree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!Equals(tree.SelectedNode, null) && !Equals(tree.SelectedNode.Tag, null))
            {
                dataGridView1.DataSource = null;
                // Создание таблицы и привязка к биндингу.
                source.DataSource = ((List<Product>)(tree.SelectedNode.Tag)).GetRange(0, ((List<Product>)(tree.SelectedNode.Tag)).Count);
                dataGridView1.DataSource = source;
                selected = tree.SelectedNode;
            }
        }

        /// <summary>
        /// Добвление продукта.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void AddProduct_Click(object sender, EventArgs e)
        {
            if (Equals(tree.SelectedNode, null))
            {
                MessageBox.Show("Товар должен находиться в определенном разделе");
            }
            else
            {
                // Добавление продукта.
                ProductForm form = new ProductForm(tree.SelectedNode);
                form.Show();
                dataGridView1.Refresh();
            }
        }

        /// <summary>
        /// Метод, вызывающийся по нажатию на TreeView.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Tree_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                // Настройка видимости кнопок.
                tree.SelectedNode = null;
                Edit.Visible = false;
                AddProduct.Visible = false;
                Delete.Visible = false;
            }
        }

        /// <summary>
        /// Метод, вызывающийся после изменения имени узла с проверкой на доступность имени.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Tree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.Node.EndEdit(false);
            if (!Equals(e.Node.Parent, null))
            {
                int count = 0;
                foreach (TreeNode i in e.Node.Parent.Nodes)
                {
                    if (i == e.Node)
                        continue;
                    if (i.Text == e.Label)
                        count++;
                }
                if (count == 1)
                {
                    MessageBox.Show("Данное имя уже зарезервированно тут");
                    e.CancelEdit = true;
                }
            }
            else
            {
                int count = 0;
                foreach (TreeNode i in tree.Nodes)
                {
                    if (i == e.Node)
                        continue;
                    if (i.Text == e.Label)
                        count++;
                }
                if (count == 1)
                {
                    MessageBox.Show("Данное имя уже зарезервированно тут");
                    e.CancelEdit = true;
                }
            }
        }

        /// <summary>
        /// Создание узла по клику.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Create_Click(object sender, EventArgs e)
        {
            if (Equals(tree.SelectedNode, null))
            {
                CreateForm form = new CreateForm(tree);
                form.Show();
            }
            else
            {
                CreateForm form = new CreateForm(tree.SelectedNode);
                form.Show();
            }
        }

        /// <summary>
        /// Изменение узла по клику.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Edit_Click(object sender, EventArgs e)
        {
            if (!Equals(tree.SelectedNode, null))
                tree.SelectedNode.BeginEdit();
            else
                MessageBox.Show("Вы ничего не выбрали для изменения");
        }

        /// <summary>
        /// Удаление узла по клику.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Delete_Click(object sender, EventArgs e)
        {
            if (!Equals(tree.SelectedNode, null))
            {
                if (tree.SelectedNode.Nodes.Count == 0)
                {
                    if (Equals(tree.SelectedNode.Tag, null))
                    {
                        tree.SelectedNode.Remove();
                    }
                    else
                    {
                        if (((List<Product>)(tree.SelectedNode.Tag)).Count == 0)
                        {
                            tree.SelectedNode.Remove();
                        }
                        else
                        {
                            MessageBox.Show("Этот раздел содержит товар(-ы). Его удалять нельзя");
                        }
                    }
                }
                else
                    MessageBox.Show("Этот раздел содержит подраздел(-ы). Его удалять нельзя");
            }
            else
                MessageBox.Show("Вы не выбрали узел для удаления");
        }
    }
}
