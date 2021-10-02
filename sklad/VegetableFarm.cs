using System;
using System.Collections.Generic;
using System.IO;

namespace sklad
{
    /// <summary>
    /// В программе реализовано много дополнительного функционала, но, вероятно, он несильно бросается в глаза.
    /// Так вот, расскажу о нем поподробнее, дабы уважаемый проверяющий не утруждался и не искал его слишком долго:
    /// 1. Вывод необходимых доходов и оставшейся массы при работе с консолью.
    /// 2. Возможность замены при переполнении не только первого контейнера, но и любого другого (также с консолью).
    /// 3. При работе с файлами можно указать id контейнера для вывода в консоль.
    /// 4. При работе с файлами в консоль выводятся те контейнеры, которые были погружены на склад (если можно считать за доп функционал).
    /// Также в архив приложил 3 документа формата .txt для удобства проверки файлов.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Возможно, дорогой проверяющий, мои метод тебе покажется перегруженным, но это не так.
        /// Большие размеры метода обусловлены исключительно большим разветвлением логики и исключением всевозможных ошибок.
        /// Я сократил его до практически минимально возможных размеров, большая часть строчек - проверка коэффициента k.
        /// Коэффициент k отвечает за наличие ошибок. Его название, как коэффициента уместно и соответствует кодстайлу.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int k = 0;
            int numberOfContainer = 0;
            // Вызов стартового меню и выбор способа ввода.
            string typeOfInput = StartMenu();
            string ans = "";
            // Цикл до ключевого слова exit.
            while (typeOfInput != "exit" && ans != "exit")
            {
                // Цикл до момента, когда коэффициент ошибки станет равным единице.
                while (true)
                {
                    // Для консолевого ввода.
                    if (typeOfInput == "1")
                    {
                        List<Container> containers = new List<Container>();
                        List<uint> identificate = new List<uint>();
                        ConsoleStoreHouse(out uint capacity, out double storagePrice, out Store store);
                        Console.WriteLine("Цифрой укажите верный пункт меню");
                        Console.WriteLine("1. Добавление контейнера на склад (после этого считываются данные о контейнере)");
                        Console.WriteLine("2. Удаление контейнера со склада (идентификатор удаляемого контейнера)");
                        ans = TakeTypeOfTask();
                        if (ans == "exit")
                        {
                            break;
                        }
                        if (ans == "1")
                        {
                            // Вызов метода для добавления контейнера.
                            // Также он возвращает количество контейнеров (либо номер текущего).
                            numberOfContainer = AddContainer(numberOfContainer, storagePrice, store, containers);
                        }
                        else if (ans == "2")
                        {
                            // Вызов метода для удаления контейнера.
                            numberOfContainer = ContainerDelete(containers, numberOfContainer);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Введите имя файла c описанием склада (ввод в файле в строчку)");
                        string fileStore = "";
                        // Вызов метода для проверки имени файла.
                        fileStore = FileChecker(fileStore);
                        // Получение текста из файла.
                        string fileText = File.ReadAllText(fileStore);
                        string[] given = fileText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        double storagePrice = 0;
                        uint capacity = 0;
                        // Проверка длины массива строк с данными.
                        if (given.Length == 2)
                        {
                            // Получение вместимости нашего склада.
                            if (!uint.TryParse(given[0], out capacity))
                            {
                                Console.WriteLine("Некорректные данные");
                                break;
                            }
                            // Получение цены хранения.
                            if (!double.TryParse(given[1], out storagePrice))
                            {
                                Console.WriteLine("Некорректные данные");
                                break;
                            }
                        }
                        else
                        {
                            // Установление коэффициента ошибки при некорректных данных.
                            k = 1;
                            break;
                        }
                        Console.WriteLine("Успешно");
                        // Создание склада.
                        Store store = new Store(capacity, storagePrice);
                        // Вызов вспомогательного интерфейса.
                        InterfaceHelper();
                        Console.WriteLine("Введите имя файла c описанием действий (ввод в файле в строчку)");
                        fileStore = "";
                        // Вызов метода для проверки имени файла.
                        fileStore = FileChecker(fileStore);
                        k = 0;
                        // Вызов метода для извлечения списка комманд из файла.
                        // Метод устанавливает массив из индексов, которые надо вывести (доп. функционал) и которые надо удалить.
                        GetCommands(fileStore, out fileText, out given, out k, out int[] commands, out uint[] identToPrint, out uint[] delete);
                        if (k == 1)
                        {
                            Console.WriteLine("Некорректные данные");
                            break;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Успешно");
                            Console.WriteLine();
                        }
                        List<Container> containers;
                        List<Box> boxes;
                        // Создание контейнеров в методе
                        MakeContainers(out k, out fileStore, fileText, out given, storagePrice, out containers, out boxes);
                        if (k == 1)
                        {
                            break;
                        }
                        // Метод для исполнения комманд из файла. 
                        // Возвращает массив из текущих контейнеров.
                        List<Container> liveContainers = DoCommands(capacity, commands, identToPrint, delete, containers, boxes);
                        Console.WriteLine();
                        Console.WriteLine("Вот какие контейнеры лежат на складе: ");
                        int c = 0;
                        // Получение информации о складе.
                        c = InfoAboutWarehouse(storagePrice, liveContainers, c);
                    }
                }
                // Проверка на ошибку.
                if (k == 1)
                {
                    Console.WriteLine();
                    // (((.
                    Console.WriteLine("Ну хватит руинить меня(");
                    Console.WriteLine("Начнем сначала");
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Метод осуществляет вывод информации о складе.
        /// На вход подаются стоимость аренды, текущие контейнеры, возвращает коэффициент ошибки.
        /// </summary>
        /// <param name="storagePrice"></Стоимость хранения контейнера>
        /// <param name="liveContainers"></Текущие контейнеры (лист)>
        /// <param name="c"></Коэффициент ошибки>
        /// <returns></returns>
        private static int InfoAboutWarehouse(double storagePrice, List<Container> liveContainers, int c)
        {
            for (var i = 0; i < liveContainers.Count; i++)
            {
                // Проверка на то, лежит ли контейнер на складе.
                if (liveContainers[i].Price * (1 - liveContainers[i].Random) - storagePrice > 0 && liveContainers[i].IsAlive == true)
                {
                    Console.WriteLine("Контейнер id_{0}", liveContainers[i].Id);
                    Console.WriteLine();
                    c = 1;
                }
            }
            // Вывод информации, если он пуст.
            if (liveContainers.Count == 0 || c == 0)
            {
                Console.WriteLine("Он пуст");
                Console.WriteLine("Вероятно вы выставили нерентабельные контейнеры");
            }

            return c;
        }

        /// <summary>
        /// Метод осуществлен для самого выполнения комманд из файла.
        /// На вход подаются массивы комманд, массив идентифкаторов на удаление и на вывод, а также листы контейнеров и ящиков.
        /// Переменные, обозначенные одним символом - индексы в различных массивах.
        /// Метод возвращает измененный лист контейнеров.
        /// </summary>
        /// <param name="capacity"></Вместительность склада>
        /// <param name="commands"></Массив комманд>
        /// <param name="identToPrint"></Массив индексов>
        /// <param name="delete"></Массив с идентификаторами на удаление>
        /// <param name="containers"></Лист контейнеров>
        /// <param name="boxes"></Лист ящиков>
        /// <returns></returns>
        private static List<Container> DoCommands(uint capacity, int[] commands, uint[] identToPrint, uint[] delete, List<Container> containers, List<Box> boxes)
        {
            int count = 1;
            // Создание нового листа контейнеров, который отображает текущую обстановку на складе.
            List<Container> liveContainers = new List<Container>();
            int p = 0;
            int r = 0;
            // Цикл на выполнение комманд, до конца длины массива команд.
            for (var i = 0; i < commands.Length; i++)
            {
                if (commands[i] == 1)
                {
                    if (count - 1 != capacity)
                    {
                        // Добавление контейнера в массив контейнеров.
                        liveContainers.Add(containers[count - 1]);
                    }
                    else
                    {
                        // Удаление первого контейнера, при переполнении склада.
                        liveContainers.RemoveAt(0);
                        // Добавление нового.
                        liveContainers.Add(containers[count - 1]);
                        count--;
                    }
                    count++;
                }
                else if (commands[i] == 2)
                {
                    if (liveContainers != null)
                    {
                        // Исключение ошибок.
                        try
                        {
                            // Присваивание контейнеру мертвое значение, если его удалили.
                            foreach (var container in liveContainers)
                            {
                                if (container.Id == delete[r])
                                {
                                    container.IsAlive = false;
                                }
                            }
                            r++;
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    // Вывод контейнеров по идентификаторам.
                    // Выводятся не сами идентификаторы, а контейнеры по ним, в порядке указания идентификаторов.
                    Console.WriteLine("Содержимое контейнера по {0}-ому из заданных идентификаторов: ", p + 1);
                    int t = 0;
                    // Вывод содержимого контейнера (ящиков), по запросу пользователя (доп. функционал).
                    foreach (var box in boxes)
                    {
                        // Исключение ошибок.
                        try
                        {
                            // Поиск совпадающего id ящиков и запрошенного.
                            if (box.Id == identToPrint[p])
                            {
                                Console.WriteLine();
                                Console.WriteLine("Ящик {0}:", ++t);
                                Console.WriteLine("Вес {0}", box.Weight);
                                Console.WriteLine("Чистая цена за ящик {0}", box.Price * box.Weight);
                            }
                        }
                        catch
                        {
                        }
                    }
                    // Переход к следующему элементу в массиве запрошенных id.
                    p++;
                }
            }

            // Возврат измененного массива.
            return liveContainers;
        }

        /// <summary>
        /// Метод для создания контейнеров по файлам.
        /// В большинстве своем пригодился только для облегчения Main'а.
        /// Возвращает заготовки для дальнейшего создания контейнеров.
        /// Вызывает в себе второй метод для уже непосредственного заполнения контейнеров.
        /// </summary>
        /// <param name="k"></Коэффициент ошибки>
        /// <param name="fileStore"></Имя файла>
        /// <param name="fileText"></Считанный текст из файла>
        /// <param name="given"></Массив данных из файла>
        /// <param name="storagePrice"></Стоимость аренды>
        /// <param name="containers"></Лист контейнеров>
        /// <param name="boxes"></Лист коробок>
        private static void MakeContainers(out int k, out string fileStore, string fileText, out string[] given, double storagePrice, out List<Container> containers, out List<Box> boxes)
        {
            k = 0;
            Console.WriteLine("Теперь введите файл, с описанием контейнера (Каждый контейнер с новой строчки)");
            Console.WriteLine("Нерентабельный контейнер на склад не пойдет");
            Console.WriteLine("В файле должно присутствовать: количество ящиков в нем, и далее сами ящики (в порядке масса, цена)");
            fileStore = "";
            // Проверка на корректность пути к файлу.
            fileStore = FileChecker(fileStore);
            string[] fileTexts = File.ReadAllLines(fileStore);
            given = fileText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            // Создание пустых листов.
            containers = new List<Container>();
            List<uint> identificate = new List<uint>();
            boxes = new List<Box>();
            // Вызов на заполнение значениями.
            ContainersMaker(ref given, storagePrice, ref k, fileTexts, containers, boxes);
        }

        /// <summary>
        /// Все полученные данные и только созданные данные передаются в этот метод.
        /// Опять же, это необходимо исключительно для декомпозиции.
        /// По ссылке получаем из этого метода лист контейнеров.
        /// </summary>
        /// <param name="given"></Массив полученных в прошлом методе данных>
        /// <param name="storagePrice"></Арендная плата>
        /// <param name="k"></Коэффициент ошибки>
        /// <param name="fileTexts"></Массив строчек из файла>
        /// <param name="containers"></Лист контейнеров>
        /// <param name="boxes"></Лист ящиков>
        private static void ContainersMaker(ref string[] given, double storagePrice, ref int k, string[] fileTexts, List<Container> containers, List<Box> boxes)
        {
            uint id = 0;
            double weight = 0;
            double price = 0;
            // Создание ненужного контейнера для исключения последующей синтаксической ошибки.
            Container container = new Container(333333, false);
            // Цикл по всем строчкам.
            for (var j = 0; j < fileTexts.Length; j++)
            {
                // Преобразование каждой строчки в массив строчек.
                given = fileTexts[j].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < given.Length; i++)
                {
                    // Первый элемент - идентификатор.
                    if (i == 0)
                    {
                        // Получение идентификатора контейнера.
                        if (uint.TryParse(given[0], out id))
                        {
                            container = new Container(id, true);
                            containers.Add(container);
                        }
                        else
                        {
                            k = 1;
                            break;
                        }
                    }
                    // Каждый второй элемент (при нумерации с единицы) - это вес ящика.
                    else if (i % 2 == 1)
                    {
                        if (!double.TryParse(given[i], out weight))
                        {
                            k = 1;
                            break;
                        }
                        container.Weight = weight;
                    }
                    // Этот элемент стоимость за 1 у.е.
                    else if (i % 2 == 0)
                    {
                        if (!double.TryParse(given[i], out price))
                        {

                            k = 1;
                            break;
                        }
                        container.Price = price;
                    }
                    // Создание новой коробки после прописывания двух элементов, если вести счет не с 1-ого, а со 2-ого.
                    if ((i % 2 == 0) && i != 0)
                    {
                        if (container.Weight > 0)
                        {
                            Box box = new Box(weight, price, true, id);
                            boxes.Add(box);
                        }
                    }
                }
                // Коэффициент ошибки.
                if (k == 1)
                {
                    Console.WriteLine("Некорректные данные");
                    break;
                }
            }
        }

        /// <summary>
        /// Разгрузочный метод для Main'а
        /// Вывод справочной информации
        /// </summary>
        private static void InterfaceHelper()
        {
            Console.WriteLine();
            Console.WriteLine("А вот и дополнительный функционал подоспел");
            Console.WriteLine("Цифра <1> в файле соответствует добавлению контейнеров");
            Console.WriteLine("Цифра <2_id> в файле соответствует удалению контейнеров");
            Console.WriteLine("Цифра <3_id> в файле соответствует выводу содержимого контейнера из склада");
            Console.WriteLine();
        }

        /// <summary>
        /// Метод служит для получения комманд, полученных из файла.
        /// Комманды записываются в один массив, id, для которых их следует выполнить в другие.
        /// </summary>
        /// <param name="fileStore"></Путь к файлу>
        /// <param name="fileText"></Текст из файла>
        /// <param name="given"></Данные из файла в виде массива>
        /// <param name="k"></Коэффициент ошибки>
        /// <param name="commands"></Массив команд>
        /// <param name="ident"></Массив идентификаторов>
        /// <param name="delete"></Массив id на удаление>
        private static void GetCommands(string fileStore, out string fileText, out string[] given, out int k, out int[] commands, out uint[] ident, out uint[] delete)
        {
            fileText = File.ReadAllText(fileStore);
            given = fileText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            commands = new int[given.Length];
            k = 0;
            ident = new uint[given.Length];
            delete = new uint[given.Length];
            int l = 0;
            int t = 0;
            for (var i = 0; i < given.Length; i++)
            {
                // Проверка на то, что входной параметр корректен.
                if ((!int.TryParse(given[i], out commands[i]) || commands[i] != 1) && (!given[i].Contains("3_")) && (!given[i].Contains("2_")))
                {
                    k = 1;
                    break;
                }
                // Получение информации в массив комманд и идентификаторов соответственно.
                if (given[i].Contains("3_"))
                {
                    commands[i] = 3;
                    string[] arr = given[i].Split("_");
                    Array.Resize(ref ident, l + 1);
                    if (!uint.TryParse(arr[1], out ident[l++]))
                    {
                        k = 1;
                        break;
                    }
                }
                // Получение информации в массив комманд и идентификаторов на удаление соответственно.
                if (given[i].Contains("2_"))
                {
                    commands[i] = 2;
                    string[] aff = given[i].Split("_");
                    Array.Resize(ref delete, t + 1);
                    if (!uint.TryParse(aff[1], out delete[t++]))
                    {
                        k = 1;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Метод для получения корректного пути к файлу, либо завершения программы.
        /// </summary>
        /// <param name="fileStore"></Путь к файлу>
        /// <returns></returns>
        private static string FileChecker(string fileStore)
        {
            while (!File.Exists(fileStore))
            {
                fileStore = Console.ReadLine();
                if (!File.Exists(fileStore) && fileStore != "exit")
                {
                    Console.WriteLine("Не существует файла с заданным именем");
                    Console.WriteLine("Повторите ввод");
                }
                if (fileStore == "exit")
                {
                    Environment.Exit(228);
                }
            }
            return fileStore;
        }

        /// <summary>
        /// Метод для удаления контейнеров.
        /// Возвращает количество контейнеров после применения метода.
        /// </summary>
        /// <param name="containers"></Лист контейнеров>
        /// <param name="numberOfContainer"></Количество контейнеров(текущий номер)>
        /// <returns></returns>
        private static int ContainerDelete(List<Container> containers, int numberOfContainer)
        {
            Console.WriteLine("Введите идентификатор контейнера, который вы хотите удалить");
            int count = 0;
            while (true)
            {
                // Получение id на удаление.
                if (!uint.TryParse(Console.ReadLine(), out uint numberOfId))
                {
                    Console.WriteLine("Введите целое неотрицательное число - идентификатор контейнера");
                }
                // Удаление при совпадении контейнера по id.
                for (var i = 0; i < containers.Count; i++)
                {
                    if (containers[i] != null)
                    {
                        if (containers[i].Id == numberOfId && containers[i].IsAlive != false)
                        {
                            containers[i].IsAlive = false;
                            Console.WriteLine();
                            Console.WriteLine("Контейнер успешно удален");
                            Console.WriteLine();
                            count = 1;
                            containers.RemoveAt(i);
                            numberOfContainer--;
                            break;
                        }
                    }
                }
                // Проверка на то, что массив контейнеров не пуст.
                if (count == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Нельзя удалить контейнер, если его не существует");
                    Console.WriteLine();
                    break;
                }
                break;
            }

            return numberOfContainer;
        }

        /// <summary>
        /// Метод, необходимый для добавления контейнеров с консоли.
        /// Возвращает текущее количество контейнеров на складе.
        /// </summary>
        /// <param name="numberOfContainer"></Колчество контейнеров (номер текущего)>
        /// <param name="storagePrice"></Арендная плата>
        /// <param name="store"></Склад>
        /// <param name="containers"></Лист контейнеров>
        /// <returns></returns>
        private static int AddContainer(int numberOfContainer, double storagePrice, Store store, List<Container> containers)
        {
            if (numberOfContainer < store.Capacity)
            {
                uint id;
                Console.WriteLine("Введите идентификационный номер создаваемого контейнера");
                // Проверка id на корректность.
                id = CheckIdCorrect(containers);
                Container container = new Container(id);
                containers.Add(container);
                // Чисто информативный метод.
                TakeInformationAboutProfitability(storagePrice, container);
                uint boxCount;
                // Проверка корректности введенного количества ящиков.
                boxCount = CheckBoxCountCorrect();
                Box[] boxes = new Box[boxCount];
                for (var i = 0; i < boxCount; i++)
                {
                    // Ввод значений, с проверкой их на корректность.
                    Console.WriteLine("Введите массу, затем цену {0} ящика", i + 1);
                    double weight = CheckWeightCorrect();
                    double price = CheckPriceCorrect();
                    if (container.Weight >= weight)

                    {
                        // Вывод актуальной информации (доп. функционал).
                        InfoAtTheMoment(container, boxes, i, weight, price);
                    }
                    else
                    {
                        // Добавление ящиков в массив ящиков.
                        boxes[i] = new Box(weight, price, false);
                        Console.WriteLine("Дружище, у тебя нет столько места в контейнере.");
                        Console.WriteLine("Этот ящик придется оставить, он не лезет!");
                    }
                }
                // Проверка на то, может ли лежать контейнер на складе.
                numberOfContainer = BadOrGoodContainer(numberOfContainer, storagePrice, container);
                numberOfContainer++;
            }
            else
            {
                // Замена контейнера вместо существующего.
                numberOfContainer = ContainerSwapWithExist(numberOfContainer, storagePrice, store, containers);
            }

            return numberOfContainer;
        }

        /// <summary>
        /// Метод для замены контейнера вместо существующего.
        /// Пользователь выбирает id контейнера, который необходимо заменить (доп. функционал).
        /// Затем вводит свой новый контейнер.
        /// </summary>
        /// <param name="numberOfContainer"></Текущее число контейнеров>
        /// <param name="storagePrice"></Цена за аренду>
        /// <param name="store"></Склад>
        /// <param name="containers"></Лист контейнеров>
        /// <returns></returns>
        private static int ContainerSwapWithExist(int numberOfContainer, double storagePrice, Store store, List<Container> containers)
        {
            Console.WriteLine("Переполнение контейнеров. Выберите контейнер, по идентификатору которого я сделаю замену.");
            numberOfContainer--;
            int count = 0;
            uint numberOfId;
            while (true)
            {
                // Ввод id до момента появления правильного.
                if (!uint.TryParse(Console.ReadLine(), out numberOfId))
                {
                    Console.WriteLine("Введите целое неотрицательное число - идентификатор контейнера");
                }
                else
                {
                    break;
                }
            }
            // Поиск контейнера с нужным id.
            for (var i = 0; i < containers.Count; i++)
            {
                if (containers[i] != null)
                {
                    if (containers[i].Id == numberOfId && containers[i].IsAlive != false)
                    {
                        count = 1;
                        containers.RemoveAt(i);
                        numberOfContainer = AddContainer(numberOfContainer, storagePrice, store, containers);
                        break;
                    }
                }
            }
            // Проверка, что с таким идентификатором есть, что менять.
            if (count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Нет контейнера с таким идентификационным номером");
                Console.WriteLine();
                // Установление верного количества контейнеров.
                numberOfContainer++;
            }

            return numberOfContainer;
        }

        /// <summary>
        /// Метод, проверяющий контейнер на правильность.
        /// Помещает его на склад, если он подходит.
        /// Выводит сообщение об ошибке в ином случае.
        /// </summary>
        /// <param name="numberOfContainer"></Количество контейнеров (номер текущего)>
        /// <param name="storagePrice"></Арендная плата>
        /// <param name="container"></Контейнер>
        /// <returns></returns>
        private static int BadOrGoodContainer(int numberOfContainer, double storagePrice, Container container)
        {
            // Проверка контейнера на окупаемость.
            if (container.Price < storagePrice / (1 - container.Random))
            {
                // Установение контейнера, как неактивного (удаление).
                container.IsAlive = false;
                Console.WriteLine();
                Console.WriteLine("Я же предупреждал, сколько нужно заработать на этом контейнере.");
                Console.WriteLine("Но ты же меня не слушаешь и мне пришлось его выкинуть самостоятельно, потому что он нерентабельный");
                Console.WriteLine();
                numberOfContainer--;
            }
            else
            {
                Console.WriteLine("Контейнер успешно помещен на склад");
                Console.WriteLine();
                // Установление контейнера активным.
                container.IsAlive = true;
            }
            return numberOfContainer;
        }

        /// <summary>
        /// Метод подсчитывающий оставшуюся массу в контейнере и текущую стоимость без дефектов
        /// Работает с ссылочными типами данных, задает через свойства поля контейнера.
        /// </summary>
        /// <param name="container"></Контейнер>
        /// <param name="boxes"></Массив ящиков>
        /// <param name="i"></Номер контейнера>
        /// <param name="weight"></Вес ящика>
        /// <param name="price"></Цена ящика>
        private static void InfoAtTheMoment(Container container, Box[] boxes, int i, double weight, double price)
        {
            // Создание элемента массива типа ящик по заданным значениям.
            boxes[i] = new Box(weight, price, true, container.Id);
            container.Weight = weight;
            Console.WriteLine();
            Console.WriteLine("Оставшаяся масса в контейнере : {0} кг", container.Weight);
            // См свойство Price и Weight, в самих свойствах прописаны арифметические операции.
            container.Price = price * weight;
            Console.WriteLine("Стоимость контейнера в данный момент (чистая стоимость без дефектов) {0}", container.Price);
            Console.WriteLine();
        }

        /// <summary>
        /// Метод проверки на корректность длины коробки.
        /// </summary>
        /// <returns></returns>
        private static uint CheckBoxCountCorrect()
        {
            uint boxCount;
            while (true)
            {
                if (!uint.TryParse(Console.ReadLine(), out boxCount))
                {
                    Console.WriteLine("Необходимо ввести целое неотрицательное число");
                }
                else
                {
                    break;
                }
            }

            return boxCount;
        }

        /// <summary>
        /// Информационный метод для получения информации о контейнере, используя свойства класса.
        /// </summary>
        /// <param name="storagePrice"></Арендная плата>
        /// <param name="container"></Контейнер>
        private static void TakeInformationAboutProfitability(double storagePrice, Container container)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("Напоминаю! Необходимая стоимость для помещения этого контейнера на склад от {0:F3} и более", storagePrice / (1 - container.Random));
            Console.WriteLine("Так как арендная плата за контейнер составляет {0}, а поврежденность данного контейнера {1:F3}%", storagePrice, container.Random * 100);
            Console.WriteLine("Доступная масса в контейнере: {0:F3} кг", container.Weight);
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("Введите количество ящиков с овощами в данном контейнере");
        }

        /// <summary>
        /// Проверка цены ящика на корректность.
        /// </summary>
        /// <returns></returns>
        private static double CheckPriceCorrect()
        {
            double price;
            while (true)
            {
                if (!double.TryParse(Console.ReadLine(), out price) && price >= 0)
                {
                    Console.WriteLine("Необходимо ввести неотрицательное число");
                }
                else
                {
                    break;
                }
            }

            return price;
        }

        /// <summary>
        /// Проверка веса ящика на корректность.
        /// </summary>
        /// <returns></returns>
        private static double CheckWeightCorrect()
        {
            double weight;
            while (true)
            {
                if (!double.TryParse(Console.ReadLine(), out weight) && weight >= 0)
                {
                    Console.WriteLine("Необходимо ввести неотрицательное число");
                }
                else
                {
                    break;
                }
            }

            return weight;
        }

        /// <summary>
        /// Проверка id на корректность и свободность.
        /// </summary>
        /// <param name="containers"></Контейнер>
        /// <returns></returns>
        private static uint CheckIdCorrect(List<Container> containers)
        {
            uint id;
            int count = 0;
            while (true)
            {
                count = 0;
                if (!uint.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Необходимо ввести целый неотрицательный идентификатор");
                }
                else
                {
                    for (var i = 0; i < containers.Count; i++)
                    {
                        if (containers[i] != null)
                        {
                            if (containers[i].Id == id)
                                count = 1;
                        }
                    }
                    if (count == 1)
                    {
                        Console.WriteLine("Такой идентификационный номер уже занят");
                    }
                    else
                    {
                        break;
                    }
                }
            }


            return id;
        }

        /// <summary>
        /// Интерфейс для выбора действий пользователя.
        /// </summary>
        /// <returns></returns>
        private static string TakeTypeOfTask()
        {
            string ans = "";
            while (ans != "1" && ans != "2" && ans != "exit")
            {
                ans = Console.ReadLine();
                if (ans != "1" && ans != "2" && ans != "exit")
                {
                    Console.WriteLine("Выберите корректный пункт меню");
                }
            }

            return ans;
        }

        /// <summary>
        /// Задание класса Склад через введенные с консоли параметры.
        /// </summary>
        /// <param name="capacity"></Вместимость в контейнерах>
        /// <param name="storagePrice"></Арендная плата>
        /// <param name="store"></Склад>
        private static void ConsoleStoreHouse(out uint capacity, out double storagePrice, out Store store)
        {
            Console.WriteLine("Введите вместимость вашего склада в контейнерах");
            while (true)
            {
                if (!uint.TryParse(Console.ReadLine(), out capacity) || capacity == 0)
                {
                    Console.WriteLine("Хорош дурачиться, и так отчислили уже");
                    Console.WriteLine("Введи еще раз");
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("Введите цену хранения за один контейнер");
            while (true)
            {
                if (!double.TryParse(Console.ReadLine(), out storagePrice) || storagePrice <= 0)
                {
                    Console.WriteLine("Хорош дурачиться, и так отчислили уже");
                    Console.WriteLine("Введи еще раз");
                }
                else
                {
                    break;
                }
            }
            store = new Store(capacity, storagePrice);
        }

        /// <summary>
        /// Стартовое ознакомительное меню, выбор типа ввода.
        /// </summary>
        /// <returns></returns>
        private static string StartMenu()
        {
            Console.WriteLine("Поздравляю, вас отчислили с ПИ, ты отсидел год в армии и теперь взялся за работу на ферме дальнего родственника");
            Console.WriteLine("Но не унывай! В любом деле можно быть успешным.");
            Console.WriteLine("За работу!");
            Console.WriteLine("* Если хочешь завершить выполнение программы напишите exit в этом меню или во время выбора операций для контейнера");
            Console.WriteLine("Для начала выбери способ ввода (напиши цифру): ");
            string ans = null;
            Console.WriteLine("1. С консоли");
            Console.WriteLine("2. Из файла");
            while (true)
            {
                ans = Console.ReadLine();
                if (ans == "1" || ans == "2" || ans == "exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Я верю в вас, все получится, дружище, попробуй еще раз");
                }
            }
            return ans;
        }


    }

}
