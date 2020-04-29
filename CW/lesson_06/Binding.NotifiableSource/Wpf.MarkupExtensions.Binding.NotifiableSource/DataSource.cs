using System.ComponentModel;

namespace Wpf.MarkupExtensions.Binding.NotifiableSource
{
    internal sealed class DataSource : INotifyPropertyChanged
    {
        private int value = 1;

        public int Value
        {
            get => value;
            set
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Value)));
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