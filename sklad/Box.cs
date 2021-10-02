using System;
using System.Collections.Generic;
using System.Text;

namespace sklad
{
    /// <summary>
    /// Класс ящиков.
    /// </summary>
    class Box
    {
        // Поля класса.
        double weight;
        double price;
        bool inContainer;
        uint id;
        // Свойство для вывода id.
        public uint Id
        {
            get
            {
                return id;
            }
        }
        // Свойство для вывода веса.
        public double Weight
        {
            get
            {
                return weight;
            }
        }
        // Свойство для вывода цены.
        public double Price
        {
            get
            {
                return price;
            }
        }
        /// <summary>
        /// Конструктор по 3 параметрам.
        /// </summary>
        /// <param name="weight"></Вес>
        /// <param name="price"></Цена>
        /// <param name="inContainer"></Существование>
        public Box(double weight, double price, bool inContainer)
        {
            this.weight = weight;
            this.price = price;
            this.inContainer = inContainer;
        }
        /// <summary>
        /// Перегрузка конструктора по 4 параметрам.
        /// </summary>
        /// <param name="weight"></Вес>
        /// <param name="price"></Цена>
        /// <param name="inContainer"></Существование>
        /// <param name="id"></Идентификатор>
        public Box(double weight, double price, bool inContainer, uint id)
        {
            this.weight = weight;
            this.price = price;
            this.inContainer = inContainer;
            this.id = id;
        }
    }
}
