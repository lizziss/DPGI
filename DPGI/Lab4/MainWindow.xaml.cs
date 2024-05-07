using Lab4.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AdoClients myTable = new AdoClients();
        public MainWindow()
        {
            InitializeComponent();
        }
        
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Reload();
        }
        private void Reload()
        {
            listClients.Focus();
            listClients.DataContext = myTable.GetAllClients();
            listClients.SelectedIndex = 0;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string phone = PhoneTextBox.Text;
            string address = AddressTextBox.Text;
            decimal income;
            decimal expenses;
        
            if (listClients.SelectedIndex != -1)
            {
                MessageBox.Show("Press 'Clear' button");
                return;
            }

            // Перевірка на заповненість всіх полів
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(address) || string.IsNullOrEmpty(IncomeTextBox.Text) ||
                string.IsNullOrEmpty(ExpensesTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

           

            // Перевірка на коректність введених значень до обробки
            if (!decimal.TryParse(IncomeTextBox.Text.Replace('.', ','), out income) ||
                !decimal.TryParse(ExpensesTextBox.Text.Replace('.', ','), out expenses))
            {
                MessageBox.Show("Income and Expenses must be decimal values.");
                return;
            }


            myTable.AddClient(name, phone, address, income, expenses);
            Reload();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            string name = NameTextBox.Text;
            string phone = PhoneTextBox.Text;
            string address = AddressTextBox.Text;
            decimal income;
            decimal expenses;

            if (listClients.SelectedIndex == -1)
            {
                MessageBox.Show("Select item to update");
                return;
            }

            // Перевірка на заповненість всіх полів
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) ||
                string.IsNullOrEmpty(address) || string.IsNullOrEmpty(IncomeTextBox.Text) ||
                string.IsNullOrEmpty(ExpensesTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }



            // Перевірка на коректність введених значень до обробки
            if (!decimal.TryParse(IncomeTextBox.Text.Replace('.', ','), out income) ||
                !decimal.TryParse(ExpensesTextBox.Text.Replace('.', ','), out expenses) || 
                !int.TryParse(IdTextBox.Text.Replace('.', ','), out id))
            {
                MessageBox.Show("Income and Expenses must be decimal values.");
                return;
            }


            myTable.UpdateClient(id,name, phone, address, income, expenses);
            Reload();
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            listClients.SelectedIndex = -1;
            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (listClients.SelectedIndex == -1)
            {
                MessageBox.Show("Оберіть продукт який необхідно видалити");
                return;
            }
         
            myTable.DeleteClient(int.Parse(IdTextBox.Text));
            Reload();

        }


        private void TxtAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9.]+").IsMatch(e.Text);
        }

    }
}
