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
    // Форма продукта
    public partial class ProductForm : Form
    {
        // Узел.
        TreeNode node;
        public ProductForm(TreeNode node)
        {
            // Инициализация компонентов, привязка методов.
            InitializeComponent();
            this.node = node;
            button1.Click += button1_Click;
            button2.Click += button1_Click;
        }

        /// <summary>
        /// Метод по нажатию на кнопку.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка полей на корректность.
            if (!Equals(VendorCode.Text, String.Empty) && !Equals(Code.Text, String.Empty) && !Equals(Called.Text, String.Empty) &&
                !Equals(Remainder1.Text, String.Empty) && !Equals(minReminder.Text, String.Empty) &&
                 !Equals(Purchase.Text, String.Empty) && !Equals(Sale.Text, String.Empty))
            {
                // Создание продукта в коллекции узла при корректных данных.
                if (Equals(((List<Product>)(node.Tag)), null))
                {
                    node.Tag = new List<Product>();
                    ((List<Product>)(node.Tag)).Add(new Product(node, VendorCode.Text, Called.Text, Code.Text, 
                        Remainder1.Value.ToString(), Purchase.Value.ToString(), Sale.Value.ToString(), minReminder.Value.ToString()));
                    Close();
                }
                else
                {
                    // Создание продукта в коллекции узла при корректных данных.
                    if (!Equals(((List<Product>)(node.Tag)).Find(x => x.Called == Called.Text), null))
                        MessageBox.Show("Такой продукт уже существует");
                    else
                    {
                        ((List<Product>)(node.Tag)).Add(new Product(node, VendorCode.Text, Called.Text, Code.Text, 
                            Remainder1.Value.ToString(), Purchase.Value.ToString(), Sale.Value.ToString(), minReminder.Value.ToString()));
                        Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните обязательные поля");
            }
        }
    }
}
