using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.ViewModels;
using PhoneBook.Models;

namespace PhoneBook.Commands
{
    public class AddContactCommand : MyRoutedCommand
    {
        public AddContactCommand(ApplicationViewModel _avm) : base(_avm)
        { }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Contact contact = new Contact();
                avm.SelectedContact = contact;
                avm.Contacts.Add(contact);                
                //avm.SaveContact(contact);
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message);
            }
        }
    }
}
