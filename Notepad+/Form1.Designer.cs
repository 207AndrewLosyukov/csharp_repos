namespace Notepad_
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.File = new System.Windows.Forms.ToolStripMenuItem();
            this.Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Save = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.Choose = new System.Windows.Forms.ToolStripMenuItem();
            this.Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.Insert = new System.Windows.Forms.ToolStripMenuItem();
            this.SetTheFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.Format = new System.Windows.Forms.ToolStripMenuItem();
            this.Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.Frequency = new System.Windows.Forms.ToolStripMenuItem();
            this.Fiveteen = new System.Windows.Forms.ToolStripMenuItem();
            this.Five = new System.Windows.Forms.ToolStripMenuItem();
            this.One = new System.Windows.Forms.ToolStripMenuItem();
            this.ThirtySec = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CancelSave = new System.Windows.Forms.ToolStripMenuItem();
            this.ColorScheme = new System.Windows.Forms.ToolStripMenuItem();
            this.Reference = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextChoose = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCut = new System.Windows.Forms.ToolStripMenuItem();
            this.contextCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.contextInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.contextSetTheFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File,
            this.Edit,
            this.Format,
            this.Settings,
            this.Reference});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1422, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // File
            // 
            this.File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Open,
            this.toolStripSeparator1,
            this.Save,
            this.SaveAs});
            this.File.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(69, 29);
            this.File.Text = "Файл";
            // 
            // Open
            // 
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(210, 30);
            this.Open.Text = "Открыть";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // Save
            // 
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(210, 30);
            this.Save.Text = "Сохранить";
            // 
            // SaveAs
            // 
            this.SaveAs.Name = "SaveAs";
            this.SaveAs.Size = new System.Drawing.Size(210, 30);
            this.SaveAs.Text = "Сохранить как";
            // 
            // Edit
            // 
            this.Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Choose,
            this.Cut,
            this.Copy,
            this.Insert,
            this.SetTheFormat});
            this.Edit.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(88, 29);
            this.Edit.Text = "Правка";
            // 
            // Choose
            // 
            this.Choose.Name = "Choose";
            this.Choose.Size = new System.Drawing.Size(251, 30);
            this.Choose.Text = "Выбрать весь текст";
            // 
            // Cut
            // 
            this.Cut.Name = "Cut";
            this.Cut.Size = new System.Drawing.Size(251, 30);
            this.Cut.Text = "Вырезать";
            // 
            // Copy
            // 
            this.Copy.Name = "Copy";
            this.Copy.Size = new System.Drawing.Size(251, 30);
            this.Copy.Text = "Копировать";
            // 
            // Insert
            // 
            this.Insert.Name = "Insert";
            this.Insert.Size = new System.Drawing.Size(251, 30);
            this.Insert.Text = "Вставить";
            // 
            // SetTheFormat
            // 
            this.SetTheFormat.Name = "SetTheFormat";
            this.SetTheFormat.Size = new System.Drawing.Size(251, 30);
            this.SetTheFormat.Text = "Задать формат";
            // 
            // Format
            // 
            this.Format.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Format.Name = "Format";
            this.Format.Size = new System.Drawing.Size(91, 29);
            this.Format.Text = "Формат";
            // 
            // Settings
            // 
            this.Settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Frequency,
            this.ColorScheme});
            this.Settings.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(117, 29);
            this.Settings.Text = "Настройки";
            // 
            // Frequency
            // 
            this.Frequency.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Fiveteen,
            this.Five,
            this.One,
            this.ThirtySec,
            this.toolStripSeparator2,
            this.CancelSave});
            this.Frequency.Name = "Frequency";
            this.Frequency.Size = new System.Drawing.Size(301, 30);
            this.Frequency.Text = "Частота автосохранений";
            // 
            // Fiveteen
            // 
            this.Fiveteen.Name = "Fiveteen";
            this.Fiveteen.Size = new System.Drawing.Size(316, 30);
            this.Fiveteen.Text = "15 минут";
            // 
            // Five
            // 
            this.Five.Name = "Five";
            this.Five.Size = new System.Drawing.Size(316, 30);
            this.Five.Text = "5 минут";
            // 
            // One
            // 
            this.One.Name = "One";
            this.One.Size = new System.Drawing.Size(316, 30);
            this.One.Text = "Минута";
            // 
            // ThirtySec
            // 
            this.ThirtySec.Name = "ThirtySec";
            this.ThirtySec.Size = new System.Drawing.Size(316, 30);
            this.ThirtySec.Text = "30 секунд";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(313, 6);
            // 
            // CancelSave
            // 
            this.CancelSave.Name = "CancelSave";
            this.CancelSave.Size = new System.Drawing.Size(316, 30);
            this.CancelSave.Text = "Отменить автосохранение";
            // 
            // ColorScheme
            // 
            this.ColorScheme.Name = "ColorScheme";
            this.ColorScheme.Size = new System.Drawing.Size(301, 30);
            this.ColorScheme.Text = "Цветовая схема";
            // 
            // Reference
            // 
            this.Reference.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Reference.Name = "Reference";
            this.Reference.Size = new System.Drawing.Size(97, 29);
            this.Reference.Text = "Справка";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.AutoCompleteCustomSource.AddRange(new string[] {
            "ауе",
            "ауе",
            "ауе"});
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 29);
            this.toolStripTextBox1.Text = "ауе\r\nауе\r\nауе";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextChoose,
            this.contextCut,
            this.contextCopy,
            this.contextInsert,
            this.contextSetTheFormat});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(180, 114);
            // 
            // contextChoose
            // 
            this.contextChoose.Name = "contextChoose";
            this.contextChoose.Size = new System.Drawing.Size(179, 22);
            this.contextChoose.Text = "Выбрать весь текст";
            // 
            // contextCut
            // 
            this.contextCut.Name = "contextCut";
            this.contextCut.Size = new System.Drawing.Size(179, 22);
            this.contextCut.Text = "Вырезать";
            // 
            // contextCopy
            // 
            this.contextCopy.Name = "contextCopy";
            this.contextCopy.Size = new System.Drawing.Size(179, 22);
            this.contextCopy.Text = "Копировать";
            // 
            // contextInsert
            // 
            this.contextInsert.Name = "contextInsert";
            this.contextInsert.Size = new System.Drawing.Size(179, 22);
            this.contextInsert.Text = "Вставить";
            // 
            // contextSetTheFormat
            // 
            this.contextSetTheFormat.Name = "contextSetTheFormat";
            this.contextSetTheFormat.Size = new System.Drawing.Size(179, 22);
            this.contextSetTheFormat.Text = "Задать формат";
            // 
            // tabControl2
            // 
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(221, 139);
            this.tabControl2.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 36);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1410, 823);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.richTextBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1402, 795);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(-4, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1410, 799);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1422, 857);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Notepad+";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem File;
        private System.Windows.Forms.ToolStripMenuItem Edit;
        private System.Windows.Forms.ToolStripMenuItem Choose;
        private System.Windows.Forms.ToolStripMenuItem Cut;
        private System.Windows.Forms.ToolStripMenuItem Copy;
        private System.Windows.Forms.ToolStripMenuItem Insert;
        private System.Windows.Forms.ToolStripMenuItem SetTheFormat;
        private System.Windows.Forms.ToolStripMenuItem Format;
        private System.Windows.Forms.ToolStripMenuItem Settings;
        private System.Windows.Forms.ToolStripMenuItem Frequency;
        private System.Windows.Forms.ToolStripMenuItem ColorScheme;
        private System.Windows.Forms.ToolStripMenuItem Reference;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem Open;
        private System.Windows.Forms.ToolStripMenuItem SaveAs;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem Save;
        private System.Windows.Forms.ToolStripMenuItem ThirtySec;
        private System.Windows.Forms.ToolStripMenuItem Fiveteen;
        private System.Windows.Forms.ToolStripMenuItem Five;
        private System.Windows.Forms.ToolStripMenuItem One;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem CancelSave;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem StripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem contextCut;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem context;
        private System.Windows.Forms.ToolStripMenuItem contextChoose;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem contextCopy;
        private System.Windows.Forms.ToolStripMenuItem contextInsert;
        private System.Windows.Forms.ToolStripMenuItem contextSetTheFormat;
    }
}

