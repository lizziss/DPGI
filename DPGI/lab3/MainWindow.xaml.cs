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
        private DBCurrentConverterEntities _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new DBCurrentConverterEntities();

            CmbTo.Items.Add("--Select--");
            CmbFrom.Items.Add("--Select--");
            foreach (var rate in _context.CurrencyRate.ToList())
            {
                string displayText = $"{rate.FullName}, {rate.ShortName}";
                CmbFrom.Items.Add(displayText);
                CmbTo.Items.Add(displayText);
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

            var amount = double.Parse(TxtAmount.Text.Replace('.', ','));
            var fromRate = _context.CurrencyRate.First(r => r.Id == CmbFrom.SelectedIndex);
            var toRate = _context.CurrencyRate.First(r => r.Id == CmbTo.SelectedIndex);

            var convertedAmount = amount * (1 / fromRate.ExchangeRate) * toRate.ExchangeRate;
            LbResult.Content = Math.Round(convertedAmount, 4);
            LbRate.Content = $"1 {fromRate.ShortName} = {Math.Round(convertedAmount / amount, 4)} {toRate.ShortName}";

            var conversion = new ConversionHistory
            {
                ConversionDate = DateTime.Now,
                Amount = amount,
                ConvertedAmount = Math.Round(convertedAmount, 4),
                FromCurrencyRateId = fromRate.Id,
                ToCurrencyRateId = toRate.Id
            };

            _context.ConversionHistory.Add(conversion);
            _context.SaveChanges();
          
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

        private void OpenHistory_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.Show();
        }
    }
}
    