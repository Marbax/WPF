using System.Windows;
using Wpf.Mvvm.Evolution.Step2.ViewModels;

namespace Wpf.Mvvm.Evolution.Step2.Views
{
    internal sealed partial class MainView : Window
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}