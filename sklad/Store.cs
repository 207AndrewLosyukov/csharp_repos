using System;
using System.Collections.Generic;
using System.Text;

namespace sklad
{
    /// <summary>
    /// Класс ящиков.
    /// </summary>
    class Store
    {
        // Поле - вместимость.
        private uint capacity;
        // Поле - арендная плата.
        private double storagePrice;

        /// <summary>
        /// Конструктор для создания элемента класса.
        /// </summary>
        /// <param name="capacity"></Вместимость>
        /// <param name="storagePrice"></Арендная платаram>
        public Store(uint capacity, double storagePrice)
        {
            this.capacity = capacity;
            this.storagePrice = storagePrice;
        }
        // Свойство для вывода вместимости.
        public uint Capacity
        {
            get
            {
                return capacity;
            }
        }
        // Свойство для вывода цены.
        public double StoragePrice
        {
            get
            {
                return storagePrice;
            }
        }
    }
}
