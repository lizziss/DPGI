using System;
using System.Collections.Generic;
using System.Linq;
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

namespace lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Визначення курсів обміну валют (відношення до долара)
        double[] exchangeRates = { 1, 38.92, 0.92, 0.79, 151.79, 0.9 }; // USD, UAH, EUR, GBP, JPY, CHF
        string[] fullNameRates = { "Долар", "Гривня", "Євро", "Фунт стерлінгів", "Єна", "Франк" };
        string[] shortNameRates = { "USD", "UAH", "EUR", "GBP", "JPY", "CHF" };
        public MainWindow()
        {
            InitializeComponent();


            CmbTo.Items.Add("--Select--");
            CmbFrom.Items.Add("--Select--");
            for (int i = 0; i < fullNameRates.Length; i++)
            {
                CmbFrom.Items.Add($"{fullNameRates[i]}, {shortNameRates[i]}");
                CmbTo.Items.Add($"{fullNameRates[i]}, {shortNameRates[i]}");
            }


            CmbFrom.SelectedIndex = 0;
            CmbTo.SelectedIndex = 0;


        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        private void BtnMaximize_Click(Object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void BtnClose_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnConvert_Click(object sender, RoutedEventArgs e)
        {
            Convert();

        }

        private void Convert()
        {
            if (TxtAmount.Text.Trim().Length == 0)
            {
                MessageBox.Show("Enter the amount");
                return;
            }
            else if (CmbFrom.SelectedIndex == 0 || CmbTo.SelectedIndex == 0)
            {
                MessageBox.Show("Select rate");
                return;
            }
            else if (!double.TryParse(TxtAmount.Text.Replace('.', ','), out _))
            {
                MessageBox.Show("Enter the correct number");
                return;
            }

            var convertedAmount = double.Parse(TxtAmount.Text.Replace('.', ',')) * (1 / exchangeRates[CmbFrom.SelectedIndex - 1]) * exchangeRates[CmbTo.SelectedIndex - 1];
            LbResult.Content = Math.Round(convertedAmount, 4);

            LbRate.Content = $"1 {shortNameRates[CmbFrom.SelectedIndex - 1]} = {Math.Round(convertedAmount / double.Parse(TxtAmount.Text.Replace('.', ',')), 4)} {shortNameRates[CmbTo.SelectedIndex - 1]}";

        }
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtAmount.Text = "";
            LbResult.Content = "";
            LbRate.Content = "";
            CmbFrom.SelectedIndex = 0;
            CmbTo.SelectedIndex = 0;
        }

        private void CmbFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbFrom.SelectedIndex == 0 || CmbTo.SelectedIndex == 0)
            {
                LbRate.Content = "";
            }

        }

        private void BtnExchange_Click(object sender, RoutedEventArgs e)
        {
            var temp = CmbTo.SelectedIndex;
            CmbTo.SelectedIndex = CmbFrom.SelectedIndex;
            CmbFrom.SelectedIndex = temp;

            Convert();
        }

        private void TxtAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9.]+").IsMatch(e.Text);
        }
    }
}
