using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace TEST
{
    public partial class Form1 : Form
    {
        private readonly Database dbHelper;

        public Form1()
        {
            InitializeComponent();

            // Укажите строку подключения к вашей БД PostgreSQL
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=12345;Database=TestBD;";
            dbHelper = new Database(connectionString);

            // Привязка обработчиков событий
            btnExecute.Click += BtnExecute_Click;
            txtPercent.KeyPress += TxtPercent_KeyPress;
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверка и преобразование ID отдела
                if (!int.TryParse(txtDepartmentID.Text, out int departmentID))
                {
                    MessageBox.Show("Пожалуйста, введите правильный ID отдела.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Проверка и преобразование процента повышения
                if (!decimal.TryParse(txtPercent.Text, out decimal percent))
                {
                    MessageBox.Show("Пожалуйста, введите правильный процент повышения.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Вызов хранимой процедуры
                DataTable result = dbHelper.UpdateSalaryForDepartment(departmentID, percent);

                // Отображение результата в таблице
                dataGridViewResults.DataSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Вызов процедуры по нажатию Enter
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnExecute_Click(sender, e);
            }
        }
    }
}
