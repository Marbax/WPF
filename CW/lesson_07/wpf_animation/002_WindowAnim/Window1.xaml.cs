using System;
using System.Windows;

using System.Windows.Media.Animation;

namespace WindowAnim
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation WindowAnim = new DoubleAnimation();
            WindowAnim.To = 900;
            WindowAnim.From = 300;
            WindowAnim.Duration = new Duration(new TimeSpan(0,0,0,0,400));
            this.BeginAnimation(WidthProperty,WindowAnim);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation WindowAnim = new DoubleAnimation();
            WindowAnim.To = 300;
            WindowAnim.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 600));            
            this.BeginAnimation(WidthProperty, WindowAnim);
        }
    }
}
