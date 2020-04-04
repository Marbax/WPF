using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DeanOffice.DataModels
{
    class Student : INotifyPropertyChanged
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private string _fName;

        public string FName
        {
            get { return _fName; }
            set
            {
                _fName = value;
                OnPropertyChanged("FName");
            }
        }

        private string _adress;

        public string Adress
        {
            get { return _adress; }
            set
            {
                _adress = value;
                OnPropertyChanged("Adress");
            }
        }

        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged("Phone");
            }
        }

        private string _contacts;

        public string Contacts
        {
            get { return _contacts; }
            set
            {
                _contacts = value;
                OnPropertyChanged("Contacts");
            }
        }

        private string _perf;

        public string Perfomance
        {
            get { return _perf; }
            set
            {
                _perf = value;
                OnPropertyChanged("Perfomance");
            }
        }

        private string _charc;

        public string Characteristic
        {
            get { return _charc; }
            set
            {
                _charc = value;
                OnPropertyChanged("Characteristic");
            }
        }

        private int _gId;

        public int GroupId
        {
            get { return _gId; }
            set
            {
                _gId = value;
                OnPropertyChanged("GroupId");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
