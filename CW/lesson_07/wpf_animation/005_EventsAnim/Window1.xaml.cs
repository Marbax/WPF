using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace EventsAnim
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        int _animStep = 0;

        public Window1()
        {
            InitializeComponent();

            LabLab2_Completed(null, null);
        }

        void LabLab1_Completed(object sender, EventArgs e)
        {
            DoubleAnimation LabLab2 = new DoubleAnimation();
            LabLab2.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 600));
            LabLab2.To = 1;
            LabLab2.Completed += new EventHandler(LabLab2_Completed);
            if (_animStep == 0)
            {
                label1.Content = "У Вас";
                _animStep = 1;
            }
            else if (_animStep == 1)
            {
                label1.Content = "Не";
                _animStep = 2;
            }
            else if (_animStep == 2)
            {
                label1.Content = "Загеристрированная";
                _animStep = 3;
            }
            else if (_animStep == 3)
            {
                label1.Content = "Копия";
                _animStep = 4;
            }
            else if (_animStep == 4)
            {
                label1.Content = "Программы";
                _animStep = 5;
            }
            else if (_animStep == 5)
            {
                label1.Content = "Вы";
                _animStep = 6;
            }
            else if (_animStep == 6)
            {
                label1.Content = "Редиска";
                _animStep = 0;
            }

            label1.BeginAnimation(Label.OpacityProperty, LabLab2);
        }

        void LabLab2_Completed(object sender, EventArgs e)
        {
            DoubleAnimation LabLab1 = new DoubleAnimation();
            LabLab1.To = 0;
            LabLab1.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 600));
            LabLab1.Completed += new EventHandler(LabLab1_Completed);
            label1.BeginAnimation(Label.OpacityProperty, LabLab1);
        }

    }
}
