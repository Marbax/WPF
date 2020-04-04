using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DeanOffice.DataModels
{
    public class Group : INotifyPropertyChanged
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

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private int _facId;

        public int FacultyId
        {
            get { return _facId; }
            set
            {
                _facId = value;
                OnPropertyChanged("FacultyId");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}