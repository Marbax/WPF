using System.Windows;
using System.Windows.Controls;

namespace Game2048.Views
{
    internal sealed partial class TileUserControl : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(int),
            typeof(TileUserControl),
            new PropertyMetadata(0)
        );

        public TileUserControl()
        {
            InitializeComponent();
        }

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}