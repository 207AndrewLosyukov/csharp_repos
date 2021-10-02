using System;
using System.Collections.Generic;
using System.Text;

namespace ManagmentLibrary
{
    [Serializable]
    public class Story : Tasks, IAssignable
    {
        // Список исполнителей.
        public List<User> users = new List<User>();

        /// <summary>
        /// Беспараметрический конструктор для сериализации.
        /// </summary>
        public Story()
        {
        }

        /// <summary>
        /// Конструктор с ссылкой на базовый.
        /// </summary>
        /// <param name="status">Статус</param>
        /// <param name="name">Имя задачи</param>
        public Story(string status, string name) : base(status, name)
        {
        }

        /// <summary>
        /// Сокрытие метода добавления исполнителя.
        /// </summary>
        /// <param name="task">Задача</param>
        public new static void Append(Tasks task)
        {
            try
            {
                // Ограничение на количество исполнителей, равное 10.
                if (task.Users.Count <= 10)
                {
                    Console.WriteLine("Введите имя исполнителя из этого списка:");
                    // Вывод списка исполнителей.
                    User.GetUsers();
                    string name = Console.ReadLine();
                    // Проверка на корректность введенного исполнителя.
                    if (!Equals(User.users.Find(x => x.username == name), null))
                    {
                        // Добавление исполнителя в список.
                        task.Users.Add(User.users.Find(x => x.username == name));
                        Console.WriteLine("Исполнитель успешно добавлен");
                    }
                    else
                    {
                        Console.WriteLine("Не найдено такого исполнителя");
                    }
                }
                else
                {
                    Console.WriteLine("У задачи этого типа может быть не более 10 исполнителей");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("");
            }
        }
    }
}
