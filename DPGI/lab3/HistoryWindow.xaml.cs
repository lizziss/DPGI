using System;
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
using System.Windows.Shapes;

namespace lab3
{
    /// <summary>
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        private DBCurrentConverterEntities _context;
        public HistoryWindow()
        {
            InitializeComponent();
            _context = new DBCurrentConverterEntities();
            LoadHistory();
        }
        private void LoadHistory()
        {
            var history = _context.ConversionHistory
                .Select(h => new
                {
                    h.ConversionDate,
                    h.Amount,
                    FromCurrency = h.CurrencyRate.ShortName,
                    ToCurrency = h.CurrencyRate.ShortName,
                    h.ConvertedAmount
                }).ToList();
            HistoryDataGrid.ItemsSource = history;
        }

        private void DeleteSelectedHistory_Click(object sender, RoutedEventArgs e)
        {
            if (HistoryDataGrid.SelectedItem is ConversionHistory selectedHistory)
            {
                var id = selectedHistory.Id;

                // Use parameterized query to prevent SQL injection
                var sqlQuery = "DELETE FROM ConversionHistory WHERE Id = @p0";

                // Execute the query and save changes
                _context.Database.ExecuteSqlCommand(sqlQuery, id);
                _context.SaveChanges();

                // Reload history data to refresh the DataGrid
                LoadHistory();
            }
        }

        /*
         private void BtnDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            if (DgHistory.SelectedItem is not null)
            {
                var selected = DgHistory.SelectedItem;
                var history = _context.ConversionHistory.FirstOrDefault(h => h.ConversionDate == (DateTime)selected.ConversionDate
                                                                            && h.Amount == selected.Amount
                                                                            && h.CurrencyRate.ShortName == selected.FromCurrency
                                                                            && h.CurrencyRate1.ShortName == selected.ToCurrency
                                                                            && h.ConvertedAmount == selected.ConvertedAmount);
                if (history != null)
                {
                    _context.ConversionHistory.Remove(history);
                    _context.SaveChanges();
                    LoadHistory();
                }
            }
        }*/

        private void ClearHistory_Click(object sender, RoutedEventArgs e)
        {
            var sqlQuery = "DELETE FROM ConversionHistories";
            _context.Database.SqlQuery<int>(sqlQuery).ToList(); 
            LoadHistory();
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

      

        private void BackToConverter_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
