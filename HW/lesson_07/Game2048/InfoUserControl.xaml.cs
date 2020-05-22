using System.Windows;
using System.Windows.Controls;

namespace Game2048.Views
{
    internal sealed partial class InfoUserControl : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(InfoUserControl),
            new PropertyMetadata(null)
        );
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(string),
            typeof(InfoUserControl),
            new PropertyMetadata(null)
        );

        public InfoUserControl()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}