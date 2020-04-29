using System;
using System.ComponentModel;
using System.Windows.Input;
using Wpf.Mvvm.Evolution.Step2.Models;

namespace Wpf.Mvvm.Evolution.Step2.ViewModels
{
    internal sealed class ContactViewModel : INotifyPropertyChanged
    {
        private readonly Contact contact;
        private readonly ICommand deleteCommand;
        private readonly ICommand editCommand;
        private readonly IPhoneCodeManager phoneCodeManager;

        private string area = string.Empty;

        public ContactViewModel(Contact contact, IPhoneCodeManager phoneCodeManager)
        {
            this.contact = contact;
            this.phoneCodeManager = phoneCodeManager;

            UpdateArea();

            deleteCommand = new DelegateCommand(Delete);
            editCommand = new DelegateCommand(Edit);

            contact.EmailChanged += Contact_EmailChanged;
            contact.PhoneChanged += Contact_PhoneChanged;
            contact.PhoneCodeChanged += Contact_PhoneCodeChanged;
        }

        public string Area
        {
            get => area;
            set => SetProperty(ref area, value, nameof(Area));
        }

        public ICommand DeleteCommand => deleteCommand;

        public ICommand EditCommand => editCommand;

        public string Email => contact.Email.ToLower();

        public Guid Id => contact.Id;

        public string Name => $"{contact.FirstName} {contact.LastName}";

        public string Phone => $"({contact.PhoneCode}) {contact.Phone.Substring(0, 3)}-{contact.Phone.Substring(3, 4)}";

        public event EventHandler<ContactEventArgs> DeleteRequested;
        public event EventHandler<ContactEventArgs> EditRequested;
        public event PropertyChangedEventHandler PropertyChanged;

        private void Contact_EmailChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Email)));
        }

        private void Contact_PhoneChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Phone)));
        }

        private void Contact_PhoneCodeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Phone)));

            UpdateArea();
        }

        private void Delete()
        {
            OnDeleteRequested(new ContactEventArgs(contact));
        }

        private void Edit()
        {
            OnEditRequested(new ContactEventArgs(contact));
        }

        private void OnDeleteRequested(ContactEventArgs e)
        {
            DeleteRequested?.Invoke(this, e);
        }

        private void OnEditRequested(ContactEventArgs e)
        {
            EditRequested?.Invoke(this, e);
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        private void SetProperty<T>(ref T oldValue, T newValue, string propertyName)
        {
            if (!oldValue?.Equals(newValue) ?? newValue != null)
            {
                oldValue = newValue;

                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        private void UpdateArea()
        {
            Area = phoneCodeManager.GetAreaByPhoneCode(contact.PhoneCode);
        }
    }
}