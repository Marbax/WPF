using System.Windows;

namespace Wpf.Commands.Events
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