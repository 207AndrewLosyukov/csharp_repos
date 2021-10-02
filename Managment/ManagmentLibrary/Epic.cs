using System;
using System.Collections.Generic;
using System.Text;

namespace ManagmentLibrary
{
    [Serializable]
    public class Epic : Tasks
    {
        // Лист задач, вложенных в Epic.
        public List<Tasks> epicTask = new List<Tasks>();

        /// <summary>
        /// Беспараметрический конструктор для сериализации.
        /// </summary>
        public Epic()
        {
        }

        /// <summary>
        /// Конструктор с ссылкой на базовый.
        /// </summary>
        /// <param name="status">Статус</param>
        /// <param name="name">Имя задачи</param>
        public Epic(string status, string name) : base(status, name)
        {
        }
        /// <summary>
        /// Сокрытый метод для добавления исполнителя в задачу.
        /// </summary>
        /// <param name="task">Задача</param>
        public new static void Append(Tasks task)
        {
            Console.WriteLine("У задачи этого типа не может быть исполнителей, т. к. она включает в себя подзадачи");
        }
        /// <summary>
        /// Переопределенный метод для вывода задачи Epic.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = $"{name} {GetType().ToString().Split('.')[1]} {status} {date}";
            if (epicTask.Count != 0)
            {
                result += "\nСуществуют следующие подзадачи:";
                foreach (Tasks task in epicTask)
                {
                    result += $"\n\t{task.GetType().ToString().Split('.')[1]} {task.name}";
                }
            }
            else
            {
                result += "\n\tПодзадач нет";
            }
            return result;
        }
    }
}
