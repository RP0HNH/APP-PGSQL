using System;
using System.Data;
using Npgsql;

namespace TEST
{
    public class Database
    {
        private readonly string connectionString;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Метод для проверки существования отдела в базе данных
        public bool IsDepartmentExists(int departmentID)
        {
            bool exists = false;

            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Выполняем запрос для проверки существования отдела
                    using (var command = new NpgsqlCommand("SELECT COUNT(*) FROM departments WHERE department_id = @DepartmentID", connection))
                    {
                        command.Parameters.AddWithValue("DepartmentID", departmentID);
                        var result = command.ExecuteScalar();
                        exists = Convert.ToInt32(result) > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при проверке существования отдела: " + ex.Message);
                }
            }

            return exists;
        }

        public DataTable UpdateSalaryForDepartment(int departmentID, decimal percent)
        {
            DataTable dataTable = new DataTable();

            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); 

                    using (var command = new NpgsqlCommand("SELECT * FROM UPDATESALARYFORDEPARTMENT(@DepartmentID, @Percent)", connection))
                    {
                        command.Parameters.AddWithValue("DepartmentID", departmentID);
                        command.Parameters.AddWithValue("Percent", percent);

                        using (var dataAdapter = new NpgsqlDataAdapter(command))
                        {
                            dataAdapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при вызове процедуры: " + ex.Message);
                }
            }

            return dataTable;
        }
    }
}
