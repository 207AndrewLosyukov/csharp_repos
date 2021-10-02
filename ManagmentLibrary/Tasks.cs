using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ManagmentLibrary
{
    [Serializable]
    public class Tasks
    {
        // Статус задачи.
        public string status;
        // Имя задачи.
        public string name;
        // Дата создания задачи.
        public DateTime date;
        // Список исполнителей задачи.
        private List<User> users = new List<User>();

        /// <summary>
        /// Беспараметрический конструктор для сериализации.
        /// </summary>
        public Tasks()
        {
        }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="status">Статус</param>
        /// <param name="name">Имя</param>
        public Tasks(string status, string name)
        {
            this.status = status;
            this.name = name;
            date = DateTime.Now;
        }

        /// <summary>
        /// Свойство для списка исполнителей.
        /// </summary>
        public List<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
            }
        }

        /// <summary>
        /// Удаление исполнителя из задачи.
        /// </summary>
        /// <param name="project">Проект</param>
        /// <param name="taskName">Имя задачи</param>
        public static void Delete(Project project, string taskName)
        {
            string nameToDelete = Console.ReadLine();
            // Проверка существования такого исполнителя в задаче.
            if (Equals(project.tasks.Find(x => x.name == taskName).Users.Find(x => x.username == nameToDelete), null))
            {
                // Выбрасывание ошибки, если такого исполнителя у этой задачи не существует.
                throw new ArgumentException("Нет такого исполнителя");
            }
            // Удаление исполнителя.
            project.tasks.Find(x => x.name == taskName).users.Remove(User.users.Find(x => x.username == nameToDelete));
            Console.WriteLine("Исполнитель успешно удален");
        }

        /// <summary>
        /// Добавление исполнителя в задаче.
        /// </summary>
        /// <param name="task">Задача</param>
        public static void Append(Tasks task)
        {
            Console.WriteLine("Введите имя исполнителя из этого списка:");
            // Вывод списка исполнителей.
            User.GetUsers();
            string name = Console.ReadLine();
            // Проверка на существование исполнителя.
            if (!Equals(User.users.Find(x => x.username == name), null))
            {
                // Добавление исполнителя в задачу.
                task.Users.Add(User.users.Find(x => x.username == name));
                Console.WriteLine("Исполнитель успешно добавлен");
            }
            else
            {
                Console.WriteLine("Не найдено такого исполнителя");
            }
        }

        /// <summary>
        /// Смена исполнителя в задаче.
        /// </summary>
        /// <param name="project">Проект</param>
        /// <param name="taskName">Имя задачи</param>
        /// <param name="name">Меняемый исполнитель</param>
        public static void Change(Project project, string taskName, string name)
        {
            Console.WriteLine("Выберите имя исполнителя, на которого поменяем");
            // Вывод списка пользователей.
            User.GetUsers();
            string newName = Console.ReadLine();
            // Проверка на существование исполнителя с таким именем, на которого меняем.
            if (!Equals(User.users.Find(x => x.username == newName), null))
            {
                // Удаление исполнителя с прошлым именем.
                project.tasks.Find(x => x.name == taskName).Users =
                    project.tasks.Find(x => x.name == taskName).Users?.Where(x => x.username != name)?.ToList();
                // Добавление исполнителя с новым именем.
                project.tasks.Find(x => x.name == taskName).Users.Add(User.users.Find(x => x.username == newName));
                Console.WriteLine("Исполнитель успешно изменен");
            }
            else
            {
                Console.WriteLine("Такого исполнителя не существует");
            }
        }

        /// <summary>
        /// Переопределенный метод для вывода задачи.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                string result = $"{name} {GetType().ToString().Split('.')[1]} {status} {date}";
                if (users.Count != 0)
                {
                    result += "\nИмена исполнителей:";
                    foreach (User user in users)
                    {
                        result += $"\n\t{user.username}";
                    }
                }
                else
                {
                    result += "\n\tИсполнителей пока нет";
                }
                return result;
            }
            // Отлов исключений.
            catch (Exception)
            {
                return "Некорректные данные";
            }
        }
    }
}
