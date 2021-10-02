using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Serialization;

namespace ManagmentLibrary
{
    [Serializable]
    [XmlInclude(typeof(Task))]
    [XmlInclude(typeof(Epic))]
    [XmlInclude(typeof(Bug))]
    [XmlInclude(typeof(Story))]
    public class Project
    {
        // Имя проекта.
        public string name;
        // Список проектов.
        public static List<Project> projects = new List<Project>();
        // Список Epic задач.
        public List<Epic> epics = new List<Epic>();
        // Список задач.
        public List<Tasks> tasks = new List<Tasks>();

        /// <summary>
        /// Беспараметрический конструктор для сериализации.
        /// </summary>
        public Project()
        {
        }

        /// <summary>
        /// Конструктор с именем проекта.
        /// </summary>
        /// <param name="name">Имя</param>
        public Project(string name)
        {
            // Проверка имени на уникальность.
            if (projects?.Where(x => x.name == name)?.ToList()?.Count > 0)
            {
                throw new ArgumentException("Проект с таким именем уже создан");
            }
            this.name = name;
            // Добавление проекта в список всех проектов.
            projects.Add(this);
        }

        /// <summary>
        /// Переименование проекта
        /// </summary>
        /// <param name="wasName">Старое имя</param>
        /// <param name="newName">Новое имя</param>
        public static void Rename(string wasName, string newName)
        {
            // Проверка на доступность имени.
            if (projects?.Where(x => x.name == newName)?.ToList()?.Count > 0)
            {
                throw new ArgumentException("Это имя уже занято");
            }
            Project project = projects.Find(x => x.name == wasName);
            // Присваивание нового имени.
            project.name = newName;
        }

        /// <summary>
        /// Удаление проекта по имени.
        /// </summary>
        /// <param name="name">Имя</param>
        public static void DeleteProject(string name)
        {
            // Проверка на наличие проекта с таким именем.
            if (projects?.Where(x => x.name == name)?.ToList()?.Count == 0)
            {
                throw new ArgumentException("Не существует проекта с таким именем");
            }
            // Удаление проекта с этим именем.
            projects = projects?.Where(x => x.name != name)?.ToList();
        }

        /// <summary>
        /// Группировка задач в проекте по статусу.
        /// </summary>
        public void GrouppingByStatus()
        {
            tasks = (from t in tasks
                     orderby t.status.Length ascending
                     select t).ToList();
        }
        
        /// <summary>
        /// Вывод всех проектов.
        /// </summary>
        public static void GetProjects()
        {
            foreach (Project project in projects)
            {
                Console.WriteLine(project);
            }
        }

        /// <summary>
        /// Вывод всех задач в проекте.
        /// </summary>
        public void GetTasks()
        {
            foreach (Tasks task in tasks)
            {
                Console.WriteLine(task);
            }
        }

        /// <summary>
        /// Переопределенный метод для вывода проекта.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Проект {name}, количество задач {tasks.Count}";
        }
    }
}
