using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Data
{
    public class AdoClients
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public DataTable GetAllClients()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ID, Name, Phone, Address, Income, Expenses FROM Clients";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                }
                catch (SqlException ex)
                {
                    // Обробка помилки підключення до бази даних
                    Console.WriteLine("Помилка підключення до бази даних: " + ex.Message);
                }
                connection.Close();
            }

            return dataTable;
        }
    }

}
