using System;
using System.Collections.Generic;
using System.Text;

namespace ManagmentLibrary
{
    /// <summary>
    /// Интерфейс для задач.
    /// </summary>
    public interface IAssignable
    {
        // Лист исполнителей задачи.
        public List<User> Users
        {
            get;
            set;
        }
        /// <summary>
        /// Метод для добавления исполнителя.
        /// </summary>
        /// <param name="user">Исполнитель</param>
        public static void Append(User user)
        {
        }
        /// <summary>
        /// Метод для замены исполнителя.
        /// </summary>
        /// <param name="project">Проект</param>
        /// <param name="taskName">Имя задачи</param>
        /// <param name="name">Имя заменяемого исполнителя</param>
        public static void Change(Project project, string taskName, string name)
        {
        }
        /// <summary>
        /// Метод для удаления исполнителя.
        /// </summary>
        /// <param name="user">Исполнитель</param>
        public static void Delete(User user)
        {
        }
    }
}
