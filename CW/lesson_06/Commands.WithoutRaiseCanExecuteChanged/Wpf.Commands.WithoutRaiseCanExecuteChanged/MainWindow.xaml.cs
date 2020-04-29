using System.Windows;

namespace Wpf.Commands.WithoutRaiseCanExecuteChanged
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