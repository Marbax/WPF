using Wpf.Mvvm.Evolution.Step2.Models;

namespace Wpf.Mvvm.Evolution.Step2.ViewModels
{
    internal interface IViewModelFactory
    {
        ContactViewModel CreateContactViewModel(Contact contact);
    }
}