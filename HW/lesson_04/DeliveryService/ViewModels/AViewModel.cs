using System.Linq;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DeliveryService.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DeliveryService.ViewModels
{
    public abstract class AViewModel<T> : INotifyPropertyChanged
    {
        protected DeliveryServiceModel _dsm = new DeliveryServiceModel();


        public ObservableCollection<T> ObjCollection { get; set; }

        public AViewModel()
        {
            ObjCollection = new ObservableCollection<T>();
        }

        abstract public void LoadData();

        abstract public void AddObj(T toAdd);

        abstract public void EditObj(T toEdit);

        abstract public void RemoveObj(int id);


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
