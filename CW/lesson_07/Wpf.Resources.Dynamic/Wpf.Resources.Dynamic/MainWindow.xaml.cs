using System.Windows;
using System.Windows.Media;

namespace Wpf.Resources.Dynamic
{
    internal sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Resources["borderBackgroundBrush"] = Brushes.Red;
        }
    }
}