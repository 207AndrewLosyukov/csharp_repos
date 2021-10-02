using System;
using System.Collections.Generic;

namespace ManagmentLibrary
{
    [Serializable]
    public class Bug : Tasks, IAssignable
    {
        /// <summary>
        /// Беспараметрический конструктор для сериализации.
        /// </summary>
        public Bug()
        {
        }

        /// <summary>
        /// Конструктор с ссылкой на базовый.
        /// </summary>
        /// <param name="status">Статус</param>
        /// <param name="name">Имя задачи</param>
        public Bug(string status, string name) : base(status, name)
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
                // Добавление исполнителя, только если его еще нет.
                if (task.Users.Count == 0)
                {
                    Console.WriteLine("Введите имя исполнителя из этого списка:");
                    // Вывод списка исполнителей.
                    User.GetUsers();
                    string name = Console.ReadLine();
                    // Проверка на существование заданного исполнителя.
                    if (!Equals(User.users.Find(x => x.username == name), null))
                    {
                        // Добавление исполнителя.
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
                    Console.WriteLine("У задачи этого типа может быть максимум один исполнитель");
                }
            }
            // Отлов исключений.
            catch (Exception)
            {
                Console.WriteLine("Некорректные данные");
            }
        }
    }
}
