using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace AnimationSpeed
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
            DoubleAnimation WinAni = new DoubleAnimation();
            WinAni.To = 900;
            WinAni.Duration =new Duration( new TimeSpan(0, 0, 0, 1));
            this.BeginAnimation(Window.WidthProperty, WinAni);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation WinAni = new DoubleAnimation();
            WinAni.To = 900;
            WinAni.Duration = new Duration(new TimeSpan(0, 0, 0, 1));

            WinAni.AccelerationRatio = 0.4; // До когда ускорять
            WinAni.DecelerationRatio = 0.5; // Когда начинать замедление.
            WinAni.SpeedRatio = 1; // Скорость значение 1 нормальная скорость.
            this.BeginAnimation(Window.WidthProperty, WinAni);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation WinAni = new DoubleAnimation();
            WinAni.To = 286;
            WinAni.Duration = new Duration(new TimeSpan(0, 0, 0, 0,400));
            this.BeginAnimation(Window.WidthProperty, WinAni);
        }
    }
}
