using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.ViewModels;
using PhoneBook.Models;

namespace PhoneBook.Commands
{
    public class DelContactCommand : MyRoutedCommand
    {
        public DelContactCommand(ApplicationViewModel _avm) : base(_avm)
        { }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            Contact c = avm.SelectedContact;
            avm.Contacts.Remove(c);
            avm.RemoveContact(c);
        }
    }
}
