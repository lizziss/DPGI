using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Створення прив'язки та приєднання обробників
            CommandBinding saveCommand = new CommandBinding(ApplicationCommands.Save, execute_Save, canExecute_Save);
            //Реєстрація прив'язки
            CommandBindings.Add(saveCommand);


            // Створення прив'язки та приєднання обробників для команди Open
            CommandBinding openCommand = new CommandBinding(ApplicationCommands.Open, execute_Open, canExecute_Open);
            CommandBindings.Add(openCommand);
            // Створення прив'язки та приєднання обробників для власної команди Clear
            CommandBinding clearCommand = new CommandBinding(ApplicationCommands.Cut, execute_Clear, canExecute_Clear);
            CommandBindings.Add(clearCommand);

        }

        void canExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            if (inputTextBox.Text.Trim().Length > 0) e.CanExecute = true; else e.CanExecute = false;
        }
        void execute_Save(object sender, ExecutedRoutedEventArgs e)
        {
            System.IO.File.WriteAllText("D:\\ГІ\\DPGI\\DPGI\\lab2\\myFile.txt", inputTextBox.Text);
            MessageBox.Show("The file was saved!");
        }
        void canExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            if (inputTextBox.Text.Trim().Length == 0) e.CanExecute = true; else e.CanExecute = false;
        }
        void execute_Open(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                inputTextBox.Text = System.IO.File.ReadAllText(filePath);
                MessageBox.Show("File opened successfully!");
            }
              
        }

        void canExecute_Clear(object sender, CanExecuteRoutedEventArgs e)
        {
            if (inputTextBox.Text.Trim().Length > 0) e.CanExecute = true; else e.CanExecute = false;
        }
        void execute_Clear(object sender, ExecutedRoutedEventArgs e)
        {
            inputTextBox.Text = "";
        }

    }
}
