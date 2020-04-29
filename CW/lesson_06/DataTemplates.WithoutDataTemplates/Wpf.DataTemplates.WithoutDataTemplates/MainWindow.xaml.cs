using System.Windows;

namespace Wpf.DataTemplates.WithoutDataTemplates
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