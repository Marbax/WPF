using Wpf.Mvvm.Evolution.Step2.Models;

namespace Wpf.Mvvm.Evolution.Step2.ViewModels
{
    internal sealed class ViewModelFactory : IViewModelFactory
    {
        private readonly IPhoneCodeManager phoneCodeManager;

        public ViewModelFactory(IPhoneCodeManager phoneCodeManager)
        {
            this.phoneCodeManager = phoneCodeManager;
        }

        public ContactViewModel CreateContactViewModel(Contact contact)
        {
            return new ContactViewModel(contact, phoneCodeManager);
        }
    }
}