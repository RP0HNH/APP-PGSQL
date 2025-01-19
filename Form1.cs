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

            // ������� ������ ����������� � ����� �� PostgreSQL
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=12345;Database=TestBD;";
            dbHelper = new Database(connectionString);

            // �������� ������������ �������
            btnExecute.Click += BtnExecute_Click;
            txtPercent.KeyPress += TxtPercent_KeyPress;
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                // �������� � �������������� ID ������
                if (!int.TryParse(txtDepartmentID.Text, out int departmentID))
                {
                    MessageBox.Show("����������, ������� ���������� ID ������.", "������ �����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // �������� � �������������� �������� ���������
                if (!decimal.TryParse(txtPercent.Text, out decimal percent))
                {
                    MessageBox.Show("����������, ������� ���������� ������� ���������.", "������ �����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ����� �������� ���������
                DataTable result = dbHelper.UpdateSalaryForDepartment(departmentID, percent);

                // ����������� ���������� � �������
                dataGridViewResults.DataSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ����� ��������� �� ������� Enter
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnExecute_Click(sender, e);
            }
        }
    }
}
