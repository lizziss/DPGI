using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Policy;
using System.Net;
using System.Xml.Linq;

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
                string query = "SELECT * FROM Clients";
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

        

        public void UpdateClient(int id, string name, string phone, string address, decimal income, decimal expenses)
        {
           

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "UPDATE Clients SET Name = @Name, Phone = @Phone, Address = @Address, " +
                  "Income = @Income, Expenses = @Expenses WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Income", income);
                    command.Parameters.AddWithValue("@Expenses", expenses);
                    command.Parameters.AddWithValue("@ID", id);


                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Updated successfully" ,"" ,MessageBoxButton.OK , MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating client data: {ex.Message}");
                }

                connection.Close();
            }
        }

       
        public void AddClient(string name, string phone, string address, decimal income, decimal expenses)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "INSERT INTO Clients (Name, Phone, Address, Income, Expenses) " +
                                   "VALUES (@Name, @Phone, @Address, @Income, @Expenses)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Income", income);
                    command.Parameters.AddWithValue("@Expenses", expenses);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Create successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка додавання клієнта: {ex.Message}");
                }
                connection.Close();
            }
        }


        public void DeleteClient(int id)
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    string query = $"DELETE FROM Clients WHERE ID = {id}";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Delete successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting client: {ex.Message}");
                }
                connection.Close();
            }

        }
    }

}
