using System;
using System.Collections.Generic;
using System.Text;

namespace sklad
{
    /// <summary>
    /// Класс контейнер.
    /// </summary>
    class Container
    {
        bool isAlive;
        Random rnd = new Random();
        uint id;
        double weight;
        double random;
        double price;

        // Свойство для вывода поврежденности.
        public double Random
        {
            get
            {
                return random;
            }
        }
        // Свойство для вывода id.
        public uint Id
        {
            get
            {
                return id;
            }
        }
        // Свойство для задачи и вывода веса.
        public double Weight
        {
            set
            {
                // Сразу вычитается оставшийся
                weight -= value;
            }
            get
            {
                return weight;
            }
        }
        // Свойство для задачи и вывода цены.
        public double Price
        {
            set
            {
                //Цена сразу прибавляется
                price += value;
            }
            get
            {
                return price;
            }
        }
        // Свойство для задачи и вывода существования.
        public bool IsAlive
        {
            set
            {
                isAlive = value;
            }
            get
            {
                return isAlive;
            }
        }

        /// <summary>
        /// Конструктор по одному параметру.
        /// </summary>
        /// <param name="id"></Идентификатор>
        public Container(uint id)
        {
            this.id = id;
            random = rnd.NextDouble() * 0.5;
            weight = rnd.Next(50, 1001);
            isAlive = true;
            price = 0;
        }
        /// <summary>
        /// Конструктор по двум параметрам.
        /// </summary>
        /// <param name="id"></Идентификатор>
        /// <param name="isAlive"></Существование>
        public Container(uint id, bool isAlive)
        {
            this.id = id;
            random = rnd.NextDouble() * 0.5;
            weight = rnd.Next(50, 1001);
            this.isAlive = isAlive;
            price = 0;
        }
    }
}
