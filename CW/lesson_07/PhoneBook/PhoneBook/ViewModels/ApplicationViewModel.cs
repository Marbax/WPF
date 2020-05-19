using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PhoneBook.Commands;
using PhoneBook.Models;
using System.Xml.Linq;

namespace PhoneBook.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private string path;
        private XDocument doc;

        private Contact selectedContact;
        public Contact SelectedContact
        {
            get { return selectedContact; }
            set { selectedContact = value; OnPropertyChanged("SelectedContact"); }
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        private MyRoutedCommand addContact;
        public MyRoutedCommand AddContact
        {
            get
            {
                if (addContact == null)
                {
                    addContact = new AddContactCommand(this);
                }
                return addContact;
            }
        }

        private MyRoutedCommand saveContact;
        public MyRoutedCommand SaveContact
        {
            get
            {
                if (saveContact == null)
                {
                    saveContact = new SaveContactCommand(this);
                }
                return saveContact;
            }
        }

        private MyRoutedCommand delContact;
        public MyRoutedCommand DelContact
        {
            get
            {
                if (delContact == null)
                {
                    delContact = new DelContactCommand(this);
                }
                return delContact;
            }
        }

        public ApplicationViewModel()
        {
            Contacts = new ObservableCollection<Contact>();
            path = @"..\..\Data\Contacts.xml";
            doc = XDocument.Load(path);
            LoadData();
        }

        public void LoadData()
        {
            var contacts = doc.Element("contacts").Elements("contact").ToList();
            foreach(var x in contacts)
            {
                Contact c = new Contact()
                {
                    Person = x.Attribute("person").Value,
                    Phone = x.Attribute("phone").Value,
                    Email = x.Attribute("email").Value
                };
                this.Contacts.Add(c);
            }
        }

        public void UpdateContact(Contact c)
        {
            XElement elem = new XElement("contact",
                new XAttribute("person", c.Person),
                new XAttribute("phone", c.Phone),
                new XAttribute("email", c.Email));
            doc.Element("contacts").Add(elem);
            doc.Save(path);
        }

        public void RemoveContact(Contact c)
        {
            var res = doc.Element("contacts").Elements("contact")
                .Where(x => x.Attribute("person").Value == c.Person)
                .FirstOrDefault();
            if (res != null)
            {
                res.Remove();
                doc.Save(path);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
