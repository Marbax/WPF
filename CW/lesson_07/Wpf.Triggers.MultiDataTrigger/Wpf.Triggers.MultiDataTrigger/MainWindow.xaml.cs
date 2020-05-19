using System.Windows;

namespace Wpf.Triggers.MultiDataTrigger
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