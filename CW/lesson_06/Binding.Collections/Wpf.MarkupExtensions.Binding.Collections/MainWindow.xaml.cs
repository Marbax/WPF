using System.Windows;

namespace Wpf.MarkupExtensions.Binding.Collections
{
    internal sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new DataSource();
        }

        private DataSource DataSource => (DataSource)DataContext;

        private void AddNonNotifiableValue_Click(object sender, RoutedEventArgs e)
        {
            DataSource.AddValueToNonNotifiableCollection();
        }

        private void AddNotifiableValue_Click(object sender, RoutedEventArgs e)
        {
            DataSource.AddValueToNotifiableCollection();
        }
    }
}