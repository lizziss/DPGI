using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;




namespace lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DBLab5Entities _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new DBLab5Entities();
            LoadData();
            JoinTables();
        }

        private void LoadData()
        {
            var clients = _context.Clients.ToList();
            var companies = _context.Companies.ToList();


            dataGridClients.ItemsSource = clients;
            dataGridCompanies.ItemsSource = companies;

        }

        private void btnSearchClients_Click(object sender, RoutedEventArgs e)
        {
            string id = txtClientID.Text.Trim();
            string name = txtClientName.Text.Trim();
            string phone = txtClientPhone.Text.Trim();
            string companyID = txtClientCompanyID.Text.Trim();
            string revenue = txtClientRevenue.Text.Trim();
            string expenses = txtClientExpenses.Text.Trim();

            var query = _context.Clients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(id))
            {
                if (int.TryParse(id, out int parsedId))
                {
                    query = query.Where(c => c.ID == parsedId);
                }
                else
                {
                    MessageBox.Show("Invalid ID format");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(c => c.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                query = query.Where(c => c.Phone.ToLower().Contains(phone.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(companyID))
            {
                if (int.TryParse(companyID, out int parsedCompanyId))
                {
                    query = query.Where(c => c.Company_ID == parsedCompanyId);
                }
                else
                {
                    MessageBox.Show("Invalid Company ID format");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(revenue))
            {
                if (decimal.TryParse(revenue, out decimal parsedRevenue))
                {
                    query = query.Where(c => c.Revenue == parsedRevenue);
                }
                else
                {
                    MessageBox.Show("Invalid Revenue format");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(expenses))
            {
                if (decimal.TryParse(expenses, out decimal parsedExpenses))
                {
                    query = query.Where(c => c.Expenses == parsedExpenses);
                }
                else
                {
                    MessageBox.Show("Invalid Expenses format");
                    return;
                }
            }

            var filteredClients = query.ToList();
            dataGridSearchClients.ItemsSource = filteredClients;
        }


        private void btnSearchCompanies_Click(object sender, RoutedEventArgs e)
        {
            string companyId = txtCompaniesID.Text.Trim();
            string companyName = txtCompaniesName.Text.Trim();

            var sqlQuery = "SELECT * FROM Companies WHERE 1=1";

            if (!string.IsNullOrWhiteSpace(companyId))
            {
                if (int.TryParse(companyId, out int parsedCompanyId))
                {
                    sqlQuery += $" AND Company_ID = {parsedCompanyId}";
                }
                else
                {
                    MessageBox.Show("Invalid Company ID format");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(companyName))
            {
                sqlQuery += $" AND Company_Name LIKE '%{companyName}%'";
            }

            var companies = _context.Companies.SqlQuery(sqlQuery).ToList();

            dataGridSearchCompanies.ItemsSource = companies;
        }



        private void JoinTables()
        {
            try
            {
                var query = from client in _context.Clients
                            join company in _context.Companies
                                on client.Company_ID equals company.Company_ID
                            select new
                            {
                                client.ID,
                                client.Name,
                                client.Phone,
                                company.Company_Name,
                                client.Revenue,
                                client.Expenses
                            };

                dataGriJoinTable.ItemsSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка під час виконання запиту: " + ex.Message);
            }

        }
    }
}
