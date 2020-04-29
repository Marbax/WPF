using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Wpf.Mvvm.Evolution.Step2.Models;

namespace Wpf.Mvvm.Evolution.Step2.ViewModels
{
    internal sealed class MainViewModel : INotifyPropertyChanged
    {
        private readonly ICommand cancelCommand;
        private readonly IContactManager contactManager;
        private readonly ICollection<ContactViewModel> contacts = new ObservableCollection<ContactViewModel>();
        private readonly ICommand loadedCommand;
        private readonly IPhoneCodeManager phoneCodeManager;
        private readonly ICollection<string> phoneCodes = new ObservableCollection<string>();
        private readonly Command saveCommand;
        private readonly IViewModelFactory viewModelFactory;

        private bool canEditFirstName = true;
        private bool canEditLastName = true;
        private Contact editContact;
        private string email = string.Empty;
        private string firstName = string.Empty;
        private string lastName = string.Empty;
        private string phone = string.Empty;
        private string phoneCode;
        private ContactViewModel selectedContact;

        public MainViewModel(IContactManager contactManager, IPhoneCodeManager phoneCodeManager, IViewModelFactory viewModelFactory)
        {
            this.contactManager = contactManager;
            this.phoneCodeManager = phoneCodeManager;
            this.viewModelFactory = viewModelFactory;

            cancelCommand = new DelegateCommand(Cancel);
            loadedCommand = new DelegateCommand(Load);
            saveCommand = new DelegateCommand(Save, () => CanSave);

            contactManager.ContactAdded += ContactManager_ContactAdded;
            contactManager.ContactDeleted += ContactManager_ContactDeleted;
            PropertyChanged += MainViewModel_PropertyChanged;
        }

        public bool CanEditFirstName
        {
            get => canEditFirstName;
            set => SetProperty(ref canEditFirstName, value, nameof(CanEditFirstName));
        }

        public bool CanEditLastName
        {
            get => canEditLastName;
            set => SetProperty(ref canEditLastName, value, nameof(CanEditLastName));
        }

        private bool CanSave => FirstName.Length > 0 &&
            LastName.Length > 0 &&
            Email.Length > 0 &&
            Phone.Length == 7 &&
            PhoneCode != null;

        public ICommand CancelCommand => cancelCommand;

        public IEnumerable<ContactViewModel> Contacts => contacts;

        private Contact EditContact
        {
            get => editContact;
            set => SetProperty(ref editContact, value, nameof(EditContact));
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value, nameof(Email));
        }

        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value, nameof(FirstName));
        }

        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value, nameof(LastName));
        }

        public ICommand LoadedCommand => loadedCommand;

        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value, nameof(Phone));
        }

        public string PhoneCode
        {
            get => phoneCode;
            set => SetProperty(ref phoneCode, value, nameof(PhoneCode));
        }

        public IEnumerable<string> PhoneCodes => phoneCodes;

        public ICommand SaveCommand => saveCommand;

        public ContactViewModel SelectedContact
        {
            get => selectedContact;
            set => SetProperty(ref selectedContact, value, nameof(SelectedContact));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void AddContact(Contact contact)
        {
            ContactViewModel viewModel = viewModelFactory.CreateContactViewModel(contact);
            viewModel.DeleteRequested += ViewModel_DeleteRequested;
            viewModel.EditRequested += ViewModel_EditRequested;

            contacts.Add(viewModel);
        }

        private void Cancel()
        {
            Email = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Phone = string.Empty;
            PhoneCode = null;

            CanEditFirstName = true;
            CanEditLastName = true;
        }

        private void ContactManager_ContactAdded(object sender, ContactEventArgs e)
        {
            AddContact(e.Contact);
        }

        private void ContactManager_ContactDeleted(object sender, ContactEventArgs e)
        {
            foreach (ContactViewModel contact in contacts)
            {
                if (contact.Id.Equals(e.Contact.Id))
                {
                    contacts.Remove(contact);
                    break;
                }
            }
        }

        private void Load()
        {
            LoadPhoneCodes();
            LoadContacts();
        }

        private void LoadContacts()
        {
            foreach (Contact contact in contactManager.Contacts)
            {
                AddContact(contact);
            }
        }

        private void LoadPhoneCodes()
        {
            foreach (string phoneCode in phoneCodeManager.PhoneCodes)
            {
                phoneCodes.Add(phoneCode);
            }
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(EditContact)))
            {
                Email = EditContact.Email;
                FirstName = EditContact.FirstName;
                LastName = EditContact.LastName;
                Phone = EditContact.Phone;
                PhoneCode = EditContact.PhoneCode;

                CanEditFirstName = false;
                CanEditLastName = false;
            }
            else if (e.PropertyName.Equals(nameof(CanSave)))
            {
                saveCommand.RaiseCanExecuteChanged();
            }
            else if (e.PropertyName.Equals(nameof(Email)) ||
                e.PropertyName.Equals(nameof(FirstName)) ||
                e.PropertyName.Equals(nameof(LastName)) ||
                e.PropertyName.Equals(nameof(Phone)) ||
                e.PropertyName.Equals(nameof(PhoneCode))
            )
            {
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanSave)));
            }
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        private void Save()
        {
            Contact contact = EditContact ?? new Contact(FirstName, LastName);

            contact.Email = Email;
            contact.Phone = Phone;
            contact.PhoneCode = PhoneCode;

            if (EditContact is null)
            {
                contactManager.AddContact(contact);
            }
            else
            {
                contactManager.UpdateContact(contact);
            }
        }

        private void SetProperty<T>(ref T oldValue, T newValue, string propertyName)
        {
            if (!oldValue?.Equals(newValue) ?? newValue != null)
            {
                oldValue = newValue;

                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ViewModel_DeleteRequested(object sender, ContactEventArgs e)
        {
            contactManager.DeleteContact(e.Contact);

            if (EditContact == e.Contact)
            {
                Cancel();
            }
        }

        private void ViewModel_EditRequested(object sender, ContactEventArgs e)
        {
            EditContact = e.Contact;
        }
    }
}