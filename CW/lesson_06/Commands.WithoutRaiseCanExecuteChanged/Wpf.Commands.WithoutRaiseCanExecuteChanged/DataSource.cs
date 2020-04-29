using System.ComponentModel;
using System.Windows.Input;

namespace Wpf.Commands.WithoutRaiseCanExecuteChanged
{
    internal sealed class DataSource : INotifyPropertyChanged
    {
        private readonly Command blueCommand;
        private readonly Command greenCommand;
        private readonly Command redCommand;

        private string selectedColor = "Black";

        public DataSource()
        {
            blueCommand = new DelegateCommand(() => SelectedColor = "Blue", () => SelectedColor != "Blue");
            greenCommand = new DelegateCommand(() => SelectedColor = "Green", () => SelectedColor != "Green");
            redCommand = new DelegateCommand(() => SelectedColor = "Red", () => SelectedColor != "Red");
        }

        public ICommand BlueCommand => blueCommand;

        public ICommand GreenCommand => greenCommand;

        public ICommand RedCommand => redCommand;

        public string SelectedColor
        {
            get => selectedColor;
            set
            {
                if (!selectedColor.Equals(value))
                {
                    selectedColor = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedColor)));
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