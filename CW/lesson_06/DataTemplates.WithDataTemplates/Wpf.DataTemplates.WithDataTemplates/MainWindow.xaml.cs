using System.Windows;

namespace Wpf.DataTemplates.WithDataTemplates
{
    internal sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new DataSource();
        }
    }
}