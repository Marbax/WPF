using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace RepeatAnim
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            //MessageBox.Show(Button.ForegroundProperty.PropertyType.ToString());
            InitializeComponent();
            
            ColorAnimation ColAnim = new ColorAnimation();
            ColAnim.From = Brushes.Blue.Color;
            ColAnim.To = Brushes.Red.Color;
            ColAnim.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 600));
            ColAnim.AutoReverse = true;
            ColAnim.RepeatBehavior =  RepeatBehavior.Forever;

            ButtonBack.BeginAnimation(SolidColorBrush.ColorProperty, ColAnim);

            ButtonBack2.BeginAnimation(GradientStop.ColorProperty, ColAnim);
        }

    }
}
