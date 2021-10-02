using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warehouse
{
    [Serializable]
    // Класс продукта.
    public class Product
    {
        // Узел.
        public TreeNode node;
        // Артикул.
        string vendorCode;
        // Название.
        string called;
        // Код.
        string code;
        // Остаток.
        string remainder;
        // Цена продажи.
        string sale;
        // Цена покупки.
        string purchase;
        // Минимальный остаток.
        public string minRemainder;
        public Product(TreeNode node, string vendorCode, string called, string code, string remainder, string purchase, string sale, string minRemainder)
        {
            this.node = node;
            this.vendorCode = vendorCode;
            this.called = called;
            this.code = code;
            this.remainder = remainder;
            this.purchase = purchase;
            this.sale = sale;
            this.minRemainder = minRemainder;
        }

        /// <summary>
        /// Свойство - название.
        /// </summary>
        public string Called
        {
            set
            {
                if (value == Called)
                    return;
                if (!Equals(((List<Product>)(node.Tag)).Find(x => x.Called == value), null))
                    MessageBox.Show("Данное имя уже зарезервированно");
                else if (value == null)
                    MessageBox.Show("Введите непустую строку");
                else
                {
                    called = value;
                }
            }
            get
            {
                return called;
            }
        }

        /// <summary>
        /// Свойство - код.
        /// </summary>
        public string Code
        {
            set
            {
                MessageBox.Show("Код товара менять нельзя");
            }
            get
            {
                return code;
            }
        }

        /// <summary>
        /// Свойство - артикул.
        /// </summary>
        public string VendorCode
        {
            set
            {
                if (value == null)
                    MessageBox.Show("Введите непустую строку");
                else
                    vendorCode = value;
            }
            get
            {
                return vendorCode;
            }
        }

        /// <summary>
        /// Свойство - остаток.
        /// </summary>
        public string Remainder
        {
            set
            {
                if (value == null)
                    MessageBox.Show("Введите непустую строку");
                else if (!int.TryParse(value, out int x))
                    MessageBox.Show("Необходимо ввести целое число");
                else
                {
                    if (int.Parse(value) < 0 || int.Parse(value) > 999999)
                        MessageBox.Show("Значение считается корректным, если оно попадает в интервал (0, 999999)");
                    else
                        remainder = value;
                }
            }
            get
            {
                return remainder;
            }
        }

        /// <summary>
        /// Свойство - цена покупки.
        /// </summary>
        public string Purchase
        {
            set
            {
                if (value == null)
                    MessageBox.Show("Введите непустую строку");
                else if (!int.TryParse(value, out int x))
                    MessageBox.Show("Необходимо ввести целое число");
                else
                {
                    if (int.Parse(value) < 0 || int.Parse(value) > 999999)
                        MessageBox.Show("Значение считается корректным, если оно попадает в интервал (0, 999999)");
                    else
                        purchase = value;
                }
            }
            get
            {
                return purchase;
            }
        }

        /// <summary>
        /// Свойство - цена продажи.
        /// </summary>
        public string Sale
        {
            set
            {
                if (value == null)
                    MessageBox.Show("Введите непустую строку");
                else if (!int.TryParse(value, out int x))
                    MessageBox.Show("Необходимо ввести целое число");
                else
                {
                    if (int.Parse(value) < 0 || int.Parse(value) > 999999)
                        MessageBox.Show("Значение считается корректным, если оно попадает в интервал (0, 999999)");
                    else
                        sale = value;
                }
            }
            get
            {
                return sale;
            }
        }
    }
}
