using System.Windows;
using Wpf.Mvvm.Evolution.Step2.Models;
using Wpf.Mvvm.Evolution.Step2.ViewModels;
using Wpf.Mvvm.Evolution.Step2.Views;

namespace Wpf.Mvvm.Evolution.Step2
{
    internal sealed partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var contactManager = new ContactManager();
            var phoneCodeManager = new PhoneCodeManager();

            var viewModelFactory = new ViewModelFactory(phoneCodeManager);

            var viewModel = new MainViewModel(contactManager, phoneCodeManager, viewModelFactory);
            var view = new MainView(viewModel);
            view.Show();
        }
    }
}