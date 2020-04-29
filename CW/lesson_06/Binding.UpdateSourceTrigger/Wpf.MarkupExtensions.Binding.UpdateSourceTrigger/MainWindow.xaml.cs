using System.Windows;

namespace Wpf.MarkupExtensions.Binding.UpdateSourceTrigger
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