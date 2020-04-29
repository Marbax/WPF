using System.Globalization;
using System.Threading;
using System.Windows;

namespace Wpf.L10n.LanguageSwitch
{
    internal sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateUI()
        {
            nameTextBlock.Text = Strings.Name;
            surnameTextBlock.Text = Strings.Surname;
            phoneTextBlock.Text = Strings.Phone;
            okButton.Content = Strings.OK;
            cancelButton.Content = Strings.Cancel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUI();
        }

        private void EnLocalization_Click(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");            
            UpdateUI();
        }

        private void RuLocalization_Click(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru");
            UpdateUI();
        }
    }
}