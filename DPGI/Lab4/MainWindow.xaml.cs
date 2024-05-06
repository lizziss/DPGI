using Lab4.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            listClients.Focus();
            listClients.DataContext = myTable.GetAllClients();
            listClients.SelectedIndex = 0;
        }


    }
}
