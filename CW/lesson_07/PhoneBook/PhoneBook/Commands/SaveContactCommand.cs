using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.ViewModels;
using PhoneBook.Models;

namespace PhoneBook.Commands
{
    public class SaveContactCommand : MyRoutedCommand
    {
        public SaveContactCommand(ApplicationViewModel _avm) : base(_avm)
        { }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Contact contact = parameter as Contact;
                avm.UpdateContact(contact);
                avm.SelectedContact = null;
                System.Windows.MessageBox.Show("Контакт успешно сохранен");
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message);
            }
        }
    }
}
