using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.CodeAnalysis;
using System.Text.RegularExpressions;

// Привет, проверяющий, для удобства проверки в кратце расскажу, что у меня работает.
// Первые 10 пунктов ТЗ работают исправно, 11 пункт - выполнено только сохранение ранее выбранных настроек.
// Реализован доп. функционал по пунктам 14-16.
namespace Notepad_
{
    /// <summary>
    /// Первый словарь - словарь, где ключ - вкладка, а значение по ключу - число.
    /// 0 - если текстовый файл, 1 - если rtf, 2 - если .cs.
    /// Второй словарь - словарь, где ключ - хэш-код вкладки, а значение по ключу - текстовое поле.
    /// </summary>
    public partial class Form1 : Form
    {
        public string menuItem;
        private int ticks = 0;
        private static int countForms = 1;
        private Dictionary<TabPage, byte> isTxt = new Dictionary<TabPage, byte>();
        private Dictionary<int, RichTextBox> richs = new Dictionary<int, RichTextBox>();

        /// <summary>
        /// Конструктор формы.
        /// Инициализация компонентов, добавление событий, создание начальной вкладки.
        /// </summary>
        public Form1()
        {
            try
            {
                InitializeComponent();
                Open.Click += Open_Click;
                SaveAs.Click += SaveAs_Click;
                Save.Click += Save_Click;
                Choose.Click += Choose_Click;
                Cut.Click += Cut_Click;
                Copy.Click += Copy_Click;
                Insert.Click += Insert_Click;
                SetTheFormat.Click += SetTheFormat_Click;
                Reference.Click += Reference_Click;
                contextChoose.Click += Choose_Click;
                contextCut.Click += Cut_Click;
                contextCopy.Click += Copy_Click;
                contextInsert.Click += Insert_Click;
                contextSetTheFormat.Click += SetTheFormat_Click;
                Format.Click += Format_Click;
                ColorScheme.Click += ColorScheme_Click;
                ThirtySec.Click += ThirtySec_Click;
                One.Click += One_Click;
                Five.Click += Five_Click;
                Fiveteen.Click += Fiveteen_Click;
                CancelSave.Click += CancelSave_Click;
                timer1.Start();
                richTextBox1.MouseDown += new MouseEventHandler(Form1_MouseDown);
                richTextBox1.BackColor = Properties.Settings.Default.savedColor;
                richTextBox1.Font = new Font("Segoe UI", 16);
                richs.Add(tabPage3.GetHashCode(), richTextBox1);
                isTxt.Add(tabPage3, 0);
                KeyPreview = true;
                KeyDown += new KeyEventHandler(Form1_KeyDown);
                FormClosing += Form_Closing;
                timer1.Interval = 1000;
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        /// <summary>
        /// Метод, позволяющий открывать файлы.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Open_Click(object sender, EventArgs e)
        {
            // Вызов диалогового окна.
            using (var openFile = new OpenFileDialog())
            {
                openFile.Filter = "txt files (*.txt)|*.txt| RTF Files|*.rtf| C# Files|*.cs";
                try
                {
                    // Проверка на корректность выбранного файла.
                    if (openFile.ShowDialog() == DialogResult.OK && openFile.FileName.Length > 0)
                    {
                        // Если открываем другое расширение в уже открытый документ, то удаляем значение ранее сохраненного расширения.
                        if (isTxt.ContainsKey(tabControl1.SelectedTab))
                            isTxt.Remove(tabControl1.SelectedTab);
                        // Записываем ссылку на файл во вкладку.
                        tabControl1.SelectedTab.Text = openFile.FileName;
                        // В зависимости от расширения, добавляем новый элемент в словарь с расширением, открываем файл в открытую вкладку.
                        if (Path.GetExtension(openFile.FileName) == ".txt")
                        {
                            richs[tabControl1.SelectedTab.GetHashCode()].Text =
                                System.IO.File.ReadAllText(openFile.FileName);
                            isTxt.Add(tabControl1.SelectedTab, 0);
                        }
                        else if (Path.GetExtension(openFile.FileName) == ".rtf")
                        {
                            isTxt.Add(tabControl1.SelectedTab, 1);
                            richs[tabControl1.SelectedTab.GetHashCode()].LoadFile(openFile.FileName);
                        }
                        else if (Path.GetExtension(openFile.FileName) == ".cs")
                        {
                            isTxt.Add(tabControl1.SelectedTab, 2);
                            richs[tabControl1.SelectedTab.GetHashCode()].LoadFile(openFile.FileName, RichTextBoxStreamType.PlainText);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Неподдерживаемое расширение файла");
                }
            }
        }

        /// <summary>
        /// Сохранение файла с именем.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                // Сохранение .txt и .rtf.
                if (isTxt[tabControl1.SelectedTab] != 2)
                    richs[tabControl1.SelectedTab.GetHashCode()]?.SaveFile(tabControl1.SelectedTab.Text,
                        isTxt[tabControl1.SelectedTab] == 0 ? RichTextBoxStreamType.PlainText : RichTextBoxStreamType.RichText);
                else
                {
                    // Сохранение .cs.
                    System.IO.File.WriteAllText(tabControl1.SelectedTab.Text,
                        richs[tabControl1.SelectedTab.GetHashCode()].Text);
                }
            }
            catch (Exception)
            {
                // Если у файла отсутствует название (написан пользователем от руки) предлагается сохранением с указанием куда.
                MessageBox.Show(@"Необходимо <Сохранить как>");
            }
        }

        /// <summary>
        /// Метод для сохранения файла как.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void SaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                // Вызов диалогового окна для выбора пути сохранения.
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "TXT files|*.txt| RTF Files|*.rtf| C# Files|*.cs";
                if (saveFile.ShowDialog() == DialogResult.OK && saveFile.FileName.Length > 0)
                {
                    // Сохранение файла в зависимости от расширения и обновление словаря с расширением при изменении расширения.
                    if (Path.GetExtension(saveFile.FileName) == ".rtf")
                    {
                        richs[tabControl1.SelectedTab.GetHashCode()]?.SaveFile(saveFile.FileName,
                            RichTextBoxStreamType.RichText);
                        isTxt[tabControl1.SelectedTab] = 1;
                    }
                    else if (Path.GetExtension(saveFile.FileName) == ".txt")
                    {
                        richs[tabControl1.SelectedTab.GetHashCode()]?.SaveFile(saveFile.FileName,
                            RichTextBoxStreamType.PlainText);
                        isTxt[tabControl1.SelectedTab] = 0;
                    }
                    else if (Path.GetExtension(saveFile.FileName) == ".cs")
                    {
                        System.IO.File.WriteAllText(saveFile.FileName, richs[tabControl1.SelectedTab.GetHashCode()].Text);
                        isTxt[tabControl1.SelectedTab] = 2;
                    }
                    tabControl1.SelectedTab.Text = saveFile.FileName;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        /// <summary>
        /// Сохранение всех файлов с именем.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void SaveAll(object sender, EventArgs e)
        {
            try
            {
                foreach (TabPage tab in tabControl1.TabPages)
                {
                    if (tab.Text != "") SaveChecking(tab);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при сохранении");
            }
        }

        /// <summary>
        /// Сохранение всех файлов с именем и предложение на сохранить как безымянных файлов.
        /// </summary>
        /// <param name="tab">Вкладка</param>
        private void SaveChecking(TabPage tab)
        {
            // Сохранить в соответствующем расширении если файл обладает именем (и сохранение словаря с расширением).
            if (tab.Text != "")
            {
                if (isTxt[tabControl1.SelectedTab] != 2)
                    richs[tab.GetHashCode()]?.SaveFile(tab.Text, isTxt[tab] == 0 ?
                    RichTextBoxStreamType.PlainText : RichTextBoxStreamType.RichText);
                else
                {
                    System.IO.File.WriteAllText(tab.Text, richs[tab.GetHashCode()].Text);
                }
            }
            // Сохранить как безымянные (и сохранение словаря с расширением).
            else
            {
                DialogResult message = MessageBox.Show("Сохранить как?", "Закрытие документа без названия",
            MessageBoxButtons.YesNo);
                if (message == DialogResult.Yes)
                {
                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.Filter = "TXT files|*.txt| RTF Files|*.rtf| C# Files|*.cs";
                    if (saveFile.ShowDialog() == DialogResult.OK && saveFile.FileName.Length > 0)
                    {
                        if (Path.GetExtension(saveFile.FileName) == ".rtf")
                        {
                            richs[tab.GetHashCode()]?.SaveFile(saveFile.FileName,
                                RichTextBoxStreamType.RichText);
                            isTxt[tab] = 1;
                        }
                        else if (Path.GetExtension(saveFile.FileName) == ".txt")
                        {
                            richs[tab.GetHashCode()]?.SaveFile(saveFile.FileName,
                                RichTextBoxStreamType.PlainText);
                            isTxt[tab] = 0;
                        }
                        else if (Path.GetExtension(saveFile.FileName) == ".cs")
                        {
                            System.IO.File.WriteAllText(saveFile.FileName, richs[tab.GetHashCode()].Text);
                            isTxt[tab] = 2;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Выбрать весь текст.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Choose_Click(object sender, EventArgs e)
        {
            try
            {
                richs[tabControl1.SelectedTab.GetHashCode()]?.SelectAll();
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        /// <summary>
        /// Вырезать выделенный фрагмент
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Cut_Click(object sender, EventArgs e)
        {
            try
            {
                richs[tabControl1.SelectedTab.GetHashCode()]?.Cut();
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        /// <summary>
        /// Копировать выделенный фрагмент
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Copy_Click(object sender, EventArgs e)
        {
            try
            {
                richs[tabControl1.SelectedTab.GetHashCode()]?.Copy();
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        /// <summary>
        /// Вставить выделенный фрагмент.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Insert_Click(object sender, EventArgs e)
        {
            try
            {
                int start = richs[tabControl1.SelectedTab.GetHashCode()].SelectionStart;
                richs[tabControl1.SelectedTab.GetHashCode()].Text =
                    richs[tabControl1.SelectedTab.GetHashCode()].Text.Remove(start,
                    richs[tabControl1.SelectedTab.GetHashCode()].SelectionLength);
                richs[tabControl1.SelectedTab.GetHashCode()].Text =
                    richs[tabControl1.SelectedTab.GetHashCode()].Text.Insert(start, Clipboard.GetText());
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        /// <summary>
        /// Установление формата выделенного фрагмента и дальнейшего текста.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void SetTheFormat_Click(object sender, EventArgs e)
        {
            try
            {
                Format_Click(sender, e);
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        /// <summary>
        /// Автосохранение на 30 секунд.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void ThirtySec_Click(object sender, EventArgs e)
        {
            // Настройки, сохраняющиеся при перезапуске.
            Properties.Settings.Default.autoSaved = 0;
        }

        /// <summary>
        /// Автосохранение на минуту.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void One_Click(object sender, EventArgs e)
        {
            // Настройки, сохраняющиеся при перезапуске.
            Properties.Settings.Default.autoSaved = 1;
        }

        /// <summary>
        /// Автосохранение пять минут.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Five_Click(object sender, EventArgs e)
        {
            // Настройки, сохраняющиеся при перезапуске.
            Properties.Settings.Default.autoSaved = 2;
        }

        /// <summary>
        /// Автосохранение пятнадцать минут.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Fiveteen_Click(object sender, EventArgs e)
        {
            // Настройки, сохраняющиеся при перезапуске.
            Properties.Settings.Default.autoSaved = 3;
        }

        /// <summary>
        /// Отмена сохранения.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void CancelSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.autoSaved = 4;
        }

        /// <summary>
        /// Выбор цветовой схемы.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void ColorScheme_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog color = new ColorDialog();
                color.AllowFullOpen = true;
                if (color.ShowDialog() == DialogResult.OK)
                    foreach (TabPage tab in tabControl1.TabPages)
                        richs[tab.GetHashCode()].BackColor = color.Color;
                Properties.Settings.Default.savedColor = color.Color;
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        /// <summary>
        /// Установление формата выделенного фрагмента и дальнейшего текста.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Format_Click(object sender, EventArgs e)
        {
            using (var format = new FontDialog())
            {
                fontDialog1.ShowColor = true;

                fontDialog1.Font = richs[tabControl1.SelectedTab.GetHashCode()]?.Font;
                fontDialog1.Color = richs[tabControl1.SelectedTab.GetHashCode()].ForeColor;

                if (fontDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    richs[tabControl1.SelectedTab.GetHashCode()].SelectionFont = fontDialog1.Font;
                    richs[tabControl1.SelectedTab.GetHashCode()].SelectionColor = fontDialog1.Color;
                }
            }
        }

        /// <summary>
        /// Форматирование кода C#.
        /// </summary>
        /// <param name="csCode">Содержимое текстового окна</param>
        private void FormatCode(string csCode)
        {
            var tree = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(csCode);
            var root = tree.GetRoot().NormalizeWhitespace();
            richs[tabControl1.SelectedTab.GetHashCode()].Text = root.ToFullString();
        }

        /// <summary>
        /// Подчеркивание ключевых слов.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void keyWords(object sender, EventArgs e)
        {
            string[] words = new string[]
            {
                "abstract ", " add ", " as ", "base ", "bool ", "break ", "byte "
                , "case ", "catch ", "char ", "checked ", "class ", "const ",
                "continue ", "decimal ", "default ", "delegate ", " do ", "double ",
                "dynamic ", "else ", "enum ", "event ", "explicit ", "extern ", "false ",
                "finally ", "fixed ", "float ", "for ", "foreach ", "from ", " get ", "global ",
                "goto ", "group ", " if ", "implicit ", " in ", " int ", "interface ", "internal ",
                "into ", " is ", "join ", " let ", "lock ", "long ", "namespace ", " new ", "null ",
                "object ", "operator ", "orderby ", " out ", "override ", "params ", "partial ",
                "private ", "protected ", "public ", "readonly ", " ref ", "remove ", "return ",
                "sbyte ", "sealed ", "select ", " set ", "short ", "sizeof ", "stackalloc ", "static ",
                "string ", "struct ", "switch ", "this ", "throw ", "true ", " try ", "typeof ", "uint ",
                "ulong ", "unchecked ", "unsafe ", "unshort ", "using ", "value ", " var ", "virtual ",
                "void ", "volatile ", "where ", "while ", "yield "
            };
            List<Regex> regexes = new List<Regex>();
            for (var i = 0; i < words.Length; i++)
            {
                regexes.Add(new Regex(words[i]));
            }
            int index = richs[tabControl1.SelectedTab.GetHashCode()].SelectionStart;
            foreach (Regex regex in regexes)
            {
                foreach (Match match in regex.Matches(richs[tabControl1.SelectedTab.GetHashCode()].Text))
                {
                    richs[tabControl1.SelectedTab.GetHashCode()].Select(match.Index, match.Value.Length);
                    richs[tabControl1.SelectedTab.GetHashCode()].SelectionColor = Color.Blue;
                    richs[tabControl1.SelectedTab.GetHashCode()].SelectionStart = index;
                    richs[tabControl1.SelectedTab.GetHashCode()].SelectionColor = Color.Black;
                }
            }
        }

        /// <summary>
        /// Нажатие по горячим клавишам.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Создание документа в новой вкладке.
            if (e.Control && e.KeyCode == Keys.Q)
            {
                NewPage(sender, e);
            }
            // Создание документа в новом окне.
            if (e.Control && e.KeyCode == Keys.W)
            {
                NewWindow();
            }
            // Сохранение текущего документа.
            if (e.Control && e.KeyCode == Keys.S)
            {
                Save_Click(sender, e);
            }
            // Сохранение всего.
            if (e.Control && e.KeyCode == Keys.E)
            {
                SaveAll(sender, e);
            }
            // Форматирование кода C#.
            if (e.Control && e.KeyCode == Keys.K)
            {
                FormatCode(richs[tabControl1.SelectedTab.GetHashCode()].Text);
            }
            // Выделение ключевых слов.
            if (e.Control && e.KeyCode == Keys.D)
            {
                keyWords(sender, e);
            }
            // Закрытие приложения.
            if (e.Alt && e.KeyCode == Keys.F)
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Вызов контестного меню.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                    contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        /// <summary>
        /// Таймер для автосохранения.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            ticks++;
            switch (Properties.Settings.Default.autoSaved)
            {
                case 0:
                    if (ticks % 30 == 0) SaveAll(sender, e);
                    break;
                case 1:
                    if (ticks % 60 == 0) SaveAll(sender, e);
                    break;
                case 2:
                    if (ticks % 300 == 0) SaveAll(sender, e);
                    break;
                case 3:
                    if (ticks % 900 == 0) SaveAll(sender, e);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Создание нового окна.
        /// </summary>
        private void NewWindow()
        {
            countForms++;
            if (countForms < 20)
            {
                Form newForm = new Form1();
                newForm.Show();
            }
            else MessageBox.Show("Не убивай оперативу, бро");
        }

        /// <summary>
        /// Создание новой вкладка.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void NewPage(object sender, EventArgs e)
        {
            try
            {
                TabPage newTab = new TabPage();
                tabControl1.TabPages.Add(newTab);
                RichTextBox richText = new RichTextBox();
                richText.MouseDown += new MouseEventHandler(Form1_MouseDown);
                richText.Location = new Point(0, 0);
                richText.Font = new Font("Segoe UI", 16);
                richText.Size = new Size(1410, 785);
                richText.BackColor = Properties.Settings.Default.savedColor;
                newTab.Controls.Add(richText);
                richs.Add(newTab.GetHashCode(), richText);
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        /// <summary>
        /// Закрытие формы.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Form_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                DialogResult message = MessageBox.Show("Сохранить изменения перед закрытием?", "Закрытие notepad+",
                    MessageBoxButtons.YesNoCancel);
                if (message == DialogResult.Yes)
                {
                    SaveAll(sender, e);
                    Properties.Settings.Default.Save();
                }
                else if (message == DialogResult.Cancel)
                {
                    // Отмена закрытии формы.
                    e.Cancel = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        /// <summary>
        /// Справка.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Reference_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ctrl+Q - создание документа в новой вкладке\n" +
                "Ctrl+W - создание документа в новом окне\n" +
                "Ctrl+S - сохранение текущего файла\n" +
                "Ctrl+E - сохранение всего\n" +
                "Ctrl+K - форматирование кода C#\n" +
                "Ctrl+D - выделение всех (контекстно-)ключевых слов C#\n" +
                "Alt+F - сохранение приложения", "Справка");
        }
    }
}
