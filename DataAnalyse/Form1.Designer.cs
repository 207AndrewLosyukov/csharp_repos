
namespace DataAnalyse
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonToDownload = new System.Windows.Forms.Button();
            this.buttonToGistogramm = new System.Windows.Forms.Button();
            this.GraficLoad = new System.Windows.Forms.Button();
            this.AverageValue = new System.Windows.Forms.Button();
            this.Median = new System.Windows.Forms.Button();
            this.StandardDeviation = new System.Windows.Forms.Button();
            this.Variance = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PickAll = new System.Windows.Forms.ToolStripMenuItem();
            this.PickSomething = new System.Windows.Forms.ToolStripMenuItem();
            this.Reference = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dgv
            // 
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(3, 63);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(1366, 695);
            this.dgv.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgv, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1372, 782);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.Controls.Add(this.buttonToDownload, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonToGistogramm, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.GraficLoad, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.AverageValue, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.Median, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.StandardDeviation, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.Variance, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.Reference, 7, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1366, 54);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // buttonToDownload
            // 
            this.buttonToDownload.AutoSize = true;
            this.buttonToDownload.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonToDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonToDownload.Location = new System.Drawing.Point(3, 3);
            this.buttonToDownload.Name = "buttonToDownload";
            this.buttonToDownload.Size = new System.Drawing.Size(164, 48);
            this.buttonToDownload.TabIndex = 2;
            this.buttonToDownload.Text = "Загрузить таблицу";
            this.buttonToDownload.UseVisualStyleBackColor = true;
            this.buttonToDownload.Click += new System.EventHandler(this.buttonToDownload_Click);
            // 
            // buttonToGistogramm
            // 
            this.buttonToGistogramm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonToGistogramm.Location = new System.Drawing.Point(173, 3);
            this.buttonToGistogramm.Name = "buttonToGistogramm";
            this.buttonToGistogramm.Size = new System.Drawing.Size(164, 48);
            this.buttonToGistogramm.TabIndex = 3;
            this.buttonToGistogramm.Text = "Построить гистограмму";
            this.buttonToGistogramm.UseVisualStyleBackColor = true;
            this.buttonToGistogramm.Click += new System.EventHandler(this.buttonToGistogramm_Click);
            // 
            // GraficLoad
            // 
            this.GraficLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GraficLoad.Location = new System.Drawing.Point(343, 3);
            this.GraficLoad.Name = "GraficLoad";
            this.GraficLoad.Size = new System.Drawing.Size(164, 48);
            this.GraficLoad.TabIndex = 4;
            this.GraficLoad.Text = "Построить график";
            this.GraficLoad.UseVisualStyleBackColor = true;
            this.GraficLoad.Click += new System.EventHandler(this.GraphicLoad_Click);
            // 
            // AverageValue
            // 
            this.AverageValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AverageValue.Location = new System.Drawing.Point(513, 3);
            this.AverageValue.Name = "AverageValue";
            this.AverageValue.Size = new System.Drawing.Size(164, 48);
            this.AverageValue.TabIndex = 5;
            this.AverageValue.Text = "Среднее значение";
            this.AverageValue.UseVisualStyleBackColor = true;
            this.AverageValue.Click += new System.EventHandler(this.AverageValue_Click);
            // 
            // Median
            // 
            this.Median.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Median.Location = new System.Drawing.Point(683, 3);
            this.Median.Name = "Median";
            this.Median.Size = new System.Drawing.Size(164, 48);
            this.Median.TabIndex = 6;
            this.Median.Text = "Медиана";
            this.Median.UseVisualStyleBackColor = true;
            this.Median.Click += new System.EventHandler(this.Median_Click);
            // 
            // StandardDeviation
            // 
            this.StandardDeviation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StandardDeviation.Location = new System.Drawing.Point(853, 3);
            this.StandardDeviation.Name = "StandardDeviation";
            this.StandardDeviation.Size = new System.Drawing.Size(164, 48);
            this.StandardDeviation.TabIndex = 7;
            this.StandardDeviation.Text = "Среднеквадратичное отклонение";
            this.StandardDeviation.UseVisualStyleBackColor = true;
            this.StandardDeviation.Click += new System.EventHandler(this.StandardDeviation_Click);
            // 
            // Variance
            // 
            this.Variance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Variance.Location = new System.Drawing.Point(1023, 3);
            this.Variance.Name = "Variance";
            this.Variance.Size = new System.Drawing.Size(164, 48);
            this.Variance.TabIndex = 8;
            this.Variance.Text = "Дисперсия";
            this.Variance.UseVisualStyleBackColor = true;
            this.Variance.Click += new System.EventHandler(this.Variance_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PickAll,
            this.PickSomething});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(230, 48);
            // 
            // PickAll
            // 
            this.PickAll.Name = "PickAll";
            this.PickAll.Size = new System.Drawing.Size(229, 22);
            this.PickAll.Text = "Выделять столбцы целиком";
            this.PickAll.Click += new System.EventHandler(this.PickAll_Click);
            // 
            // PickSomething
            // 
            this.PickSomething.Name = "PickSomething";
            this.PickSomething.Size = new System.Drawing.Size(229, 22);
            this.PickSomething.Text = "Выделять области самому";
            this.PickSomething.Click += new System.EventHandler(this.PickSomething_Click);
            // 
            // Reference
            // 
            this.Reference.AutoSize = true;
            this.Reference.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Reference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Reference.Location = new System.Drawing.Point(1193, 3);
            this.Reference.Name = "Reference";
            this.Reference.Size = new System.Drawing.Size(170, 48);
            this.Reference.TabIndex = 9;
            this.Reference.Text = "Справка";
            this.Reference.UseVisualStyleBackColor = true;
            this.Reference.Click += new System.EventHandler(this.Reference_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1372, 782);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonToDownload;
        private System.Windows.Forms.Button buttonToGistogramm;
        private System.Windows.Forms.Button GraficLoad;
        private System.Windows.Forms.Button Median;
        private System.Windows.Forms.Button StandardDeviation;
        private System.Windows.Forms.Button Variance;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem PickAll;
        private System.Windows.Forms.ToolStripMenuItem PickSomething;
        private System.Windows.Forms.Button Reference;
        private System.Windows.Forms.Button AverageValue;
    }
}

