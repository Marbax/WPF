using System.Windows;

namespace Game2048.Views
{
    public partial class MainView : Window
    {
        public MainView(ViewModels.MainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}