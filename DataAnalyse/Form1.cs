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
using System.Windows.Forms.DataVisualization.Charting;


namespace DataAnalyse
{

    public partial class Form1 : Form
    {
        private string filename;
        private int strEmptyCount = 0;
        private Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
        private NumericUpDown numeric;
        private List<PointF> points;
        private Chart chart = new Chart();
        public Form1()
        {
            InitializeComponent();
            dgv.Dock = DockStyle.Fill;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
            dgv.MouseDown += new MouseEventHandler(Form1_MouseDown);
        }
        /// <summary>
        /// Кнопка загрузки таблицы.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void buttonToDownload_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            filename = openFileDialog1.FileName;

            BindData();
        }

        /// <summary>
        /// Загрузка таблицы в контрол.
        /// </summary>
        private void BindData()
        {
            DataTable dataTable = new DataTable();
            string[] lines;
            lines = CorrectParse();
            if (lines.Length > 0)
            {
                string[] titles = lines[0].Split('\f');
                for (var i = 0; i < titles.Length; i++)
                {
                    if (titles[i] == String.Empty)
                    {
                        strEmptyCount++;
                        for (var j = 0; j < strEmptyCount; j++)
                            titles[i] += " ";
                    }
                    dataTable.Columns.Add(new DataColumn(titles[i]));
                }
                for (var i = 1; i < lines.Length; i++)
                {
                    string[] dataField = lines[i].Split('\f');
                    DataRow dataRow = dataTable.NewRow();
                    int index = 0;
                    foreach (var title in titles)
                    {
                        dataRow[title] = dataField[index++];
                    }
                    dataTable.Rows.Add(dataRow);
                }
                CorrectBind();
                dgv.DataSource = dataTable;
            }
        }

        /// <summary>
        /// Корректный парсинг строк.
        /// </summary>
        /// <returns></returns>
        private string[] CorrectParse()
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(filename);
                string line = "";
                for (var i = 0; i < lines.Length; i++)
                {
                    line = "";
                    if (lines[i].Length == 0) continue;
                    for (var j = 0; j < lines[i].Length; j++)
                    {
                        if (lines[i][j] == ',' && lines[i][j + 1] != ' ')
                        {
                            line += '\f';
                        }
                        else
                            line += lines[i][j];
                    }
                    lines[i] = line;
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                lines = new string[0];
                MessageBox.Show("Таблица не найдена");
            }
            return lines;
        }

        /// <summary>
        /// Корректная загрука в контрол.
        /// </summary>
        private void CorrectBind()
        {
            try
            {
                dgv.ClearSelection();
                dgv.CurrentCell = null;
                dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                for (var i = 0; i < dgv.Columns.Count; i++)
                    dgv.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            catch (Exception)
            {
                MessageBox.Show("Отмените выделение столбца");
            }
        }

        /// <summary>
        /// Кнопка для постройки гистограммы.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void buttonToGistogramm_Click(object sender, EventArgs e)
        {
            bool intFlag = true;
            if (dgv.SelectedColumns.Count == 0)
            {
                MessageBox.Show("Вы не выделили столбец для гистограммы");
                return;
            }
            if (dgv.SelectedColumns.Count > 1)
            {
                MessageBox.Show("Вы выбрали более одного столбца");
                return;
            }
            Form form = CreateDictAndForm(ref intFlag);
            Chart chart = CreateComponent(form);
            if (intFlag)
            {
                DoubleChart(form, chart);
            }
            else
                StringChart(chart);
            Button saveButton = new Button();
            saveButton.Text = "Сохранить";
            saveButton.Location = new Point(0, 40);
            saveButton.Size = new Size(90, 25);
            saveButton.Dock = DockStyle.Bottom;
            saveButton.Click += buttonSave;
            form.Controls.Add(saveButton);
            form.Controls.Add(chart);
            form.Show();
        }

        /// <summary>
        /// Создание компонентов формы.
        /// </summary>
        /// <param name="form">Форма</param>
        /// <returns></returns>
        private Chart CreateComponent(Form form)
        {
            form.Size = new Size(500, 300);
            Chart chart = new Chart();
            chart.Legends.Add(new Legend("Legend1"));
            chart.AutoSize = true;
            chart.Dock = DockStyle.Fill;
            var area = new ChartArea("Gistogramm");
            chart.ChartAreas.Add(area);
            chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart.MouseWheel += Chart_MouseWheel;
            return chart;
        }

        /// <summary>
        /// Создание формы и словаря для нее.
        /// </summary>
        /// <param name="intFlag">Тип гистограммы.</param>
        /// <returns></returns>
        private Form CreateDictAndForm(ref bool intFlag)
        {
            keyValuePairs = new Dictionary<string, int>();
            var index = dgv.SelectedColumns[0].Index;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (Equals(row.Cells[index].Value, null)) continue;

                string key = row.Cells[index].Value.ToString();

                if (!keyValuePairs.ContainsKey(key))
                    keyValuePairs.Add(key, 1);
                else
                    keyValuePairs[key]++;
            }
            Form form = new Form();
            foreach (var key in keyValuePairs.Keys)
            {
                if (!double.TryParse(key.Trim('\"'), out double k))
                {
                    intFlag = false;
                    break;
                }
            }

            return form;
        }

        /// <summary>
        /// Отстройка строковой гистограммы.
        /// </summary>
        /// <param name="chart">Гистограмма</param>
        private void StringChart(Chart chart)
        {
            foreach (var key in keyValuePairs.Keys)
            {
                Series series = new Series(key);
                series.ChartArea = "Gistogramm";
                series.ChartType = SeriesChartType.Column;
                series.Points.Add(keyValuePairs[key]);
                chart.Series.Add(series);
            }
        }

        /// <summary>
        /// Отстройка числовой гистограммы.
        /// </summary>
        /// <param name="form">Форма</param>
        /// <param name="chart">Гистограмма</param>
        private void DoubleChart(Form form, Chart chart)
        {
            NumericUpDown numeric = new NumericUpDown();
            numeric.ValueChanged += Numeric_ValueChange;
            form.Controls.Add(numeric);
            numeric.Location = new Point(0, 0);
            numeric.Dock = DockStyle.Fill;
            numeric.Size = new Size(70, 40);
            List<PointF> points = new List<PointF>();
            foreach (var key in keyValuePairs.Keys)
                points.Add(new PointF(float.Parse(key.Trim('\"')), keyValuePairs[key]));
            points = points.OrderBy(x => x.X).ToList();
            foreach (PointF point in points)
            {
                Series series = new Series(point.X.ToString());
                series.ChartArea = "Gistogramm";
                series.ChartType = SeriesChartType.Column;
                series.Points.AddXY(point.X.ToString(), point.Y.ToString());
                chart.Series.Add(series);
            }
            this.chart = chart;
            this.numeric = numeric;
            this.points = points;
            numeric.Minimum = 1;
            numeric.Maximum = points.Count;
            numeric.Value = points.Count;
        }

        /// <summary>
        /// Нажатие на мышку на форме.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        /// <summary>
        /// Выбор столбцов в таблице целиком.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void PickAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (var i = 0; i < dgv.Columns.Count; i++)
                    dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgv.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;

            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка");
            }
        }

        /// <summary>
        /// Частичный выбор на форме
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void PickSomething_Click(object sender, EventArgs e)
        {
            try
            {
                dgv.ClearSelection();
                dgv.CurrentCell = null;
                dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                for (var i = 0; i < dgv.Columns.Count; i++)
                    dgv.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            catch (Exception)
            {
                MessageBox.Show("Отмените выделение столбца");
            }
        }

        /// <summary>
        /// Отрисовка графика.
        /// </summary>
        /// <param name="chart">График</param>
        /// <param name="Xs">Координаты X</param>
        /// <param name="Ys">Координаты Y</param>
        public void DrawGraphic(Chart chart, List<float> Xs, List<float> Ys)
        {

            PointF[] gr = new PointF[Xs.Count];
            for (int i = 0; i < Xs.Count; i++)
            {
                gr[i].X = Xs[i];
                gr[i].Y = Ys[i];
            }
            gr = gr.OrderBy(x => x.X).ToArray();
            Series series = new Series("Graphic");
            series.ChartArea = "Graphic";
            series.ChartType = SeriesChartType.Spline;

            chart.Series.Add(series);
            for (int i = 0; i < gr.Length; i++)
            {
                chart.Series["Graphic"].Points.AddXY(gr[i].X, gr[i].Y);
            }
        }

        /// <summary>
        /// Загрузка графика по клику.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void GraphicLoad_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedColumns.Count < 2)
            {
                MessageBox.Show("Вы не выделили 2 столбца для графика");
                return;
            }
            if (dgv.SelectedColumns.Count > 2)
            {
                MessageBox.Show("Вы выбрали более двух столбцов");
                return;
            }
            Form form;
            Chart chart;
            List<float> Xs, Ys;
            Button saveButton = new Button();
            saveButton.Text = "Сохранить";
            saveButton.Location = new Point(0, 40);
            saveButton.Size = new Size(90, 25);
            saveButton.Dock = DockStyle.Bottom;
            saveButton.Click += buttonSave;
            int firstColumn, secondColumn;
            LoadComponents(out form, out chart, out Xs, out Ys, out firstColumn, out secondColumn);
            try
            {
                MakeGraphic(Xs, Ys, firstColumn, secondColumn);
                form.Controls.Add(saveButton);
            }
            catch (Exception)
            {
                MessageBox.Show("Как минимум в одном из столбцов существуют не числовые значения");
                return;
            }
            ComponentInit(form, chart, Xs, Ys, firstColumn, secondColumn);
        }

        /// <summary>
        /// Инициализация компонентов формы.
        /// </summary>
        /// <param name="form">Форма</param>
        /// <param name="chart">График</param>
        /// <param name="Xs">Координаты X</param>
        /// <param name="Ys">Координаты Y</param>
        /// <param name="firstColumn">Первый столбец</param>
        /// <param name="secondColumn">Второй столбец</param>
        private void ComponentInit(Form form, Chart chart, List<float> Xs, List<float> Ys, int firstColumn, int secondColumn)
        {
            var area = new ChartArea("Graphic");
            chart.ChartAreas.Add(area);
            Axis xName = new Axis();
            xName.Title = dgv.Columns[firstColumn].HeaderText;
            chart.ChartAreas[0].AxisX = xName;
            Axis yName = new Axis();
            yName.Title = dgv.Columns[secondColumn].HeaderText;
            chart.ChartAreas[0].AxisY = yName;
            chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart.MouseWheel += Chart_MouseWheel;
            DrawGraphic(chart, Xs, Ys);
            form.Show();
        }

        /// <summary>
        /// Загрузка компонентов графика.
        /// </summary>
        /// <param name="form">Форма</param>
        /// <param name="chart">График</param>
        /// <param name="Xs">Координаты X</param>
        /// <param name="Ys">Координаты Y</param>
        /// <param name="firstColumn">Первый столбец</param>
        /// <param name="secondColumn">Второй столбец</param>
        private void LoadComponents(out Form form, out Chart chart, out List<float> Xs, out List<float> Ys, out int firstColumn, out int secondColumn)
        {
            form = new Form();
            form.Size = new Size(500, 300);
            Button saveButton = new Button();
            saveButton.Text = "Сохранить";
            saveButton.Location = new Point(0, 40);
            saveButton.Size = new Size(90, 25);
            saveButton.Dock = DockStyle.Bottom;
            saveButton.Click += buttonSave;
            chart = new Chart();
            form.Controls.Add(chart);
            chart.AutoSize = true;
            chart.Dock = DockStyle.Fill;
            Xs = new List<float>();
            Ys = new List<float>();
            firstColumn = dgv.SelectedColumns[0].Index;
            secondColumn = dgv.SelectedColumns[1].Index;
        }

        /// <summary>
        /// Создание графика.
        /// </summary>
        /// <param name="Xs">Координаты X</param>
        /// <param name="Ys">Координаты Y</param>
        /// <param name="firstColumn">Первый столбец</param>
        /// <param name="secondColumn">Второй столбец</param>
        private void MakeGraphic(List<float> Xs, List<float> Ys, int firstColumn, int secondColumn)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (Equals(row.Cells[firstColumn].Value, null) ||
                    Equals(row.Cells[secondColumn].Value, null)) continue;

                string value = row.Cells[firstColumn].Value.ToString();
                if (value.Contains("\""))
                    Xs.Add(float.Parse(value.Split('\"')[1]));
                else
                    Xs.Add(float.Parse(value));

                value = row.Cells[secondColumn].Value.ToString();
                if (value.Contains("\""))
                    Ys.Add(float.Parse(value.Split('\"')[1]));
                else
                    Ys.Add(float.Parse(value));
            }
        }

        /// <summary>
        /// Среднее значение по клику.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void AverageValue_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedColumns.Count == 0)
            {
                MessageBox.Show("Вы не выделили столбец для гистограммы");
                return;
            }
            if (dgv.SelectedColumns.Count > 1)
            {
                MessageBox.Show("Вы выбрали более одного столбца");
                return;
            }
            int index = dgv.SelectedColumns[0].Index;
            double sum = 0;
            double count = 0;
            try
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (Equals(row.Cells[index].Value, null)) continue;

                    string value = row.Cells[index].Value.ToString();
                    if (value.Contains("\""))
                    {
                        sum += double.Parse(value.Split('\"')[1]);
                        count++;
                    }
                    else
                    {
                        sum += double.Parse(value);
                        count++;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Как минимум в одном из столбцов существуют не числовые значения");
                return;
            }
            MessageBox.Show($"Среднее значение в столбце: {(sum / count).ToString("F3")}");
        }

        /// <summary>
        /// Медианное значение по клику.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Median_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedColumns.Count == 0)
            {
                MessageBox.Show("Вы не выделили столбец для гистограммы");
                return;
            }
            if (dgv.SelectedColumns.Count > 1)
            {
                MessageBox.Show("Вы выбрали более одного столбца");
                return;
            }
            int index = dgv.SelectedColumns[0].Index;
            List<double> values = new List<double>();
            try
            {
                DataParsing(index, values);
            }
            catch (Exception)
            {
                MessageBox.Show("Как минимум в одном из столбцов существуют не числовые значения");
                return;
            }
            values.Sort();
            double ans = 0;
            if (values.Count % 2 == 0)
                ans = (values[values.Count / 2] + values[values.Count / 2 - 1]) / 2.0;
            else
                ans = values[values.Count / 2];
            MessageBox.Show($"Медиана в столбце: {ans.ToString("F3")}");
        }

        /// <summary>
        /// Парсинг таблицы.
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <param name="values">Значение</param>
        private void DataParsing(int index, List<double> values)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (Equals(row.Cells[index].Value, null)) continue;

                string value = row.Cells[index].Value.ToString();
                if (value.Contains("\""))
                {
                    values.Add(double.Parse(value.Split('\"')[1]));
                }
                else
                {
                    values.Add(double.Parse(value));
                }
            }
        }

        /// <summary>
        /// Среднее квадратичное значение по клику.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void StandardDeviation_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedColumns.Count == 0)
            {
                MessageBox.Show("Вы не выделили столбец для гистограммы");
                return;
            }
            if (dgv.SelectedColumns.Count > 1)
            {
                MessageBox.Show("Вы выбрали более одного столбца");
                return;
            }
            int index = dgv.SelectedColumns[0].Index;
            List<double> values = new List<double>();
            double sum = 0;
            try
            {
                sum = SumValues(index, values, sum);
            }
            catch (Exception)
            {
                MessageBox.Show("Как минимум в одном из столбцов существуют не числовые значения");
                return;
            }
            double mid = sum / (double)values.Count;
            double ans = 0;
            foreach (var value in values)
                ans += (value - mid) * (value - mid);
            ans = Math.Sqrt(ans / (double)(values.Count - 1));
            MessageBox.Show($"Среднее квадратичное отклонение в столбце: {ans.ToString("F3")}");
        }

        /// <summary>
        /// Суммирование значений.
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <param name="values">Значения</param>
        /// <param name="sum">Сумма</param>
        /// <returns></returns>
        private double SumValues(int index, List<double> values, double sum)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (Equals(row.Cells[index].Value, null)) continue;

                string value = row.Cells[index].Value.ToString();
                if (value.Contains("\""))
                {
                    values.Add(double.Parse(value.Split('\"')[1]));
                    sum += double.Parse(value.Split('\"')[1]);
                }
                else
                {
                    values.Add(double.Parse(value));
                    sum += double.Parse(value);
                }
            }

            return sum;
        }

        /// <summary>
        /// Дисперсия.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Variance_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedColumns.Count == 0)
            {
                MessageBox.Show("Вы не выделили столбец для гистограммы");
                return;
            }
            if (dgv.SelectedColumns.Count > 1)
            {
                MessageBox.Show("Вы выбрали более одного столбца");
                return;
            }
            int index = dgv.SelectedColumns[0].Index;
            List<double> values = new List<double>();
            double sum = 0;
            try
            {
                sum = CreateSum(index, values, sum);
            }
            catch (Exception)
            {
                MessageBox.Show("Как минимум в одном из столбцов существуют не числовые значения");
                return;
            }
            double mid = sum / (double)values.Count;
            double ans = 0;
            foreach (var value in values)
                ans += (value - mid) * (value - mid);
            ans = ans / (double)(values.Count - 1);
            MessageBox.Show($"Дисперсия в столбце: {ans.ToString("F3")}");
        }

        /// <summary>
        /// Подсчет суммы.
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <param name="values">Значения</param>
        /// <param name="sum">Сумма</param>
        /// <returns></returns>
        private double CreateSum(int index, List<double> values, double sum)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (Equals(row.Cells[index].Value, null)) continue;

                string value = row.Cells[index].Value.ToString();
                if (value.Contains("\""))
                {
                    values.Add(double.Parse(value.Split('\"')[1]));
                    sum += double.Parse(value.Split('\"')[1]);
                }
                else
                {
                    values.Add(double.Parse(value));
                    sum += double.Parse(value);
                }
            }

            return sum;
        }

        /// <summary>
        /// Метод, вызывающийся после прокрута колесика мыши.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Chart_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart = (Chart)sender;
            var xAxis = chart.ChartAreas[0].AxisX;
            var yAxis = chart.ChartAreas[0].AxisY;
            try
            {
                if (e.Delta < 0)
                {
                    xAxis.ScaleView.ZoomReset();
                    yAxis.ScaleView.ZoomReset();
                }
                else if (e.Delta > 0)
                {
                    var xMin = xAxis.ScaleView.ViewMinimum;
                    var xMax = xAxis.ScaleView.ViewMaximum;
                    var yMin = yAxis.ScaleView.ViewMinimum;
                    var yMax = yAxis.ScaleView.ViewMaximum;

                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                    yAxis.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Метод, вызывающийся после изменения значения Numeric.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Numeric_ValueChange(object sender, EventArgs e)
        {
            List<PointF> newInformations = new List<PointF>();
            int count = (int)numeric.Value;
            chart.Series.Clear();
            int[] number = new int[count];
            int length = points.Count;
            int l = 0;
            while (length % count != 0)
            {
                int k = length / count + 1;
                number[l] = k;
                l++;
                length -= k;
                count -= 1;
            }
            int ind = length / count;
            NumericGetValue(count, number, ind);
        }

        /// <summary>
        /// Метод устанавливающий новую гистограмму с новыми значениями.
        /// </summary>
        /// <param name="count">Величина Numeric</param>
        /// <param name="number">Новые координаты</param>
        /// <param name="ind">Индекс</param>
        private void NumericGetValue(int count, int[] number, int ind)
        {
            for (int i = number.Length - count; i < number.Length; i++)
            {
                number[i] = ind;
            }
            string result = "";
            for (int i = 0; i < number.Length; i++)
            {
                result += number[i].ToString() + ' ';
            }
            int index = 0;
            for (int i = 0; i < number.Length; i++)
            {
                double sum = 0;
                for (int j = index; j < index + number[i]; j++)
                {
                    sum += points[j].Y;
                }
                Series series;
                if (points[index].X != points[index + number[i] - 1].X)
                {
                    series = new Series(points[index].X.ToString() + " - " + points[index + number[i] - 1].X.ToString());

                }
                else
                {
                    series = new Series(points[index].X.ToString());
                }
                series.Points.Add(sum);
                chart.Series.Add(series);
                index += number[i];
            }
        }

        /// <summary>
        /// Сохранение графика.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void buttonSave(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Сохранить изображение как ...";
                    saveFileDialog.Filter = "*.jpg|*.jpg";
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.FileName = "ChartImage";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        chart.SaveImage(saveFileDialog.FileName, ChartImageFormat.Jpeg);
                        MessageBox.Show("Успешно сохранено");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка сохранения");
            }
        }

        /// <summary>
        /// Справка по нажатию.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие</param>
        private void Reference_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ПКМ - сменить режим выбора.\n" +
                "Движение колесика мышки на графике - приближает и отдаляет его.\n" +
                "Дроби указываются через запятую.");
        }
    }
}
