using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ManagmentLibrary
{
    [Serializable]
    public class User
    {
        // Имя исполнителя.
        public string username;

        // Список исполнителей.
        public static List<User> users = new List<User>();

        /// <summary>
        /// Беспараметрический конструктор для сериализации.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Конструктор с проверкой на то, что такого исполнителя еще нет.
        /// </summary>
        /// <param name="username">Имя исполнителя</param>
        public User(string username)
        {
            // Проверка на существование такого исполнителя.
            if (users?.Where(x => x.username == username)?.ToList()?.Count > 0)
            {
                // Выбрасывание исключения, если такой исполнитель уже существует.
                throw new ArgumentException("Такой пользователь уже существует");
            }
            this.username = username;
            // Добавление исполнителя в список.
            users.Add(this);
        }

        /// <summary>
        /// Удаление исполнителя.
        /// </summary>
        /// <param name="username">Имя исполнителя</param>
        public static void DeleteUser(string username)
        {
            // Проверка, что исполнитель с таким именем существует.
            if (users?.Where(x => x.username == username)?.ToList()?.Count > 0)
            {
                // Удаление исполнителя.
                users = users?.Where(x => x.username != username)?.ToList();
                Console.WriteLine("Пользователь успешно удален");
            }
            else
            {
                throw new ArgumentException("Не существует пользователя с таким именем");
            }
        }

        /// <summary>
        /// Вывод списка исполнителей.
        /// </summary>
        public static void GetUsers()
        {
            // Проверка, на то что есть хотя бы один исполнитель.
            if (users.Count != 0)
            {
                foreach (User user in users)
                {
                    Console.WriteLine(user);
                }
            }
            else
            {
                Console.WriteLine("Пока нет исполнителей");
            }
        }

        /// <summary>
        /// Переопределенный метод для вывода исполнителя.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return username;
        }
    }
}
