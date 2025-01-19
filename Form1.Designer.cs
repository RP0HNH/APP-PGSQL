using System;
using System.Drawing;
using System.Windows.Forms;

namespace TEST
{
    partial class Form1
    {
        private Label lblDepartmentID;
        private TextBox txtDepartmentID;
        private Label lblPercent;
        private TextBox txtPercent;
        private Button btnExecute;
        private DataGridView dataGridViewResults;

        private void InitializeComponent()
        {
            // Устанавливаем стиль окна
            this.Text = "Обновление зарплаты";
            this.Size = new Size(800, 600); // Задаем размер формы
            this.MinimumSize = new Size(600, 400); // Минимальный размер формы

            // Label для ввода ID отдела
            lblDepartmentID = new Label
            {
                Text = "ID отдела:",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            this.Controls.Add(lblDepartmentID);

            // TextBox для ввода ID отдела
            txtDepartmentID = new TextBox
            {
                Name = "txtDepartmentID",
                Location = new Point(150, 16),
                Size = new Size(150, 22),
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            this.Controls.Add(txtDepartmentID);

            // Label для ввода процента повышения
            lblPercent = new Label
            {
                Text = "Процент повышения:",
                Location = new Point(20, 60),
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            this.Controls.Add(lblPercent);

            // TextBox для ввода процента повышения
            txtPercent = new TextBox
            {
                Name = "txtPercent",
                Location = new Point(180, 56),
                Size = new Size(100, 22),
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            this.Controls.Add(txtPercent);

            // Button для выполнения запроса
            btnExecute = new Button
            {
                Text = "Выполнить",
                Location = new Point(20, 100),
                Size = new Size(100, 30),
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.LightSkyBlue,
                FlatStyle = FlatStyle.Flat
            };
            btnExecute.Click += BtnExecute_Click;
            this.Controls.Add(btnExecute);

            // DataGridView для отображения результатов
            dataGridViewResults = new DataGridView
            {
                Name = "dataGridViewResults",
                Location = new Point(20, 150),
                Size = new Size(740, 300),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Arial", 10, FontStyle.Regular)
            };
            this.Controls.Add(dataGridViewResults);

            // Настройка растягивания формы по размеру данных
            this.Resize += (s, e) =>
            {
                dataGridViewResults.Width = this.ClientSize.Width - 40;
                dataGridViewResults.Height = this.ClientSize.Height - 200;
            };
        }
    }
}
