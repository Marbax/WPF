using System.Windows;

namespace Wpf.Commands.HotKeys
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