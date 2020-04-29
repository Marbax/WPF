using System.ComponentModel;

namespace Wpf.MarkupExtensions.Binding.UpdateSourceTrigger
{
    internal sealed class DataSource : INotifyPropertyChanged
    {
        private string name = string.Empty;
        private string surname = string.Empty;

        public string Name
        {
            get => name;
            set
            {
                if (!name.Equals(value))
                {
                    name = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        public string Surname
        {
            get => surname;
            set
            {
                if (!surname.Equals(value))
                {
                    surname = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Surname)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}