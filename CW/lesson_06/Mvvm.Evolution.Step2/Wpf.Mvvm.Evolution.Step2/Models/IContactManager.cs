using System;
using System.Collections.Generic;

namespace Wpf.Mvvm.Evolution.Step2.Models
{
    internal interface IContactManager
    {
        IEnumerable<Contact> Contacts { get; }

        event EventHandler<ContactEventArgs> ContactAdded;
        event EventHandler<ContactEventArgs> ContactDeleted;

        void AddContact(Contact contact);
        void DeleteContact(Contact contact);
        void UpdateContact(Contact contact);
    }
}