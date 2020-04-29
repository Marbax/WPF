using System;
using System.Windows;
using System.Windows.Threading;

namespace Wpf.MarkupExtensions.Binding.NotifiableSource
{
    internal sealed partial class MainWindow : Window
    {
        private readonly DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000) };
            timer.Tick += Timer_Tick;

            DataContext = new DataSource();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ++((DataSource)DataContext).Value;

            UpdateTextBlock();
        }

        private void UpdateTextBlock()
        {
            textBlock.Text = ((DataSource)DataContext).Value.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTextBlock();
        }
    }
}