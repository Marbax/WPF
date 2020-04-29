using System;

namespace Wpf.Mvvm.Evolution.Step2.Models
{
    internal sealed class ContactEventArgs : EventArgs
    {
        private readonly Contact contact;

        public ContactEventArgs(Contact contact)
        {
            this.contact = contact;
        }

        public Contact Contact => contact;
    }
}