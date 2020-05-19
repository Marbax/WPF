using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Style_Lesson_File
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ResourceDictionary Temp;
        public Window1()
        {
            Temp = new ResourceDictionary();
            InitializeComponent();
            comboBox1.Items.Add("First Style");
            comboBox1.Items.Add("Second Style");
            comboBox1.Items.Add("Third Style");
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Temp.Source = new Uri(Environment.CurrentDirectory + "\\Dictionary1.xaml");
                    this.Resources = Temp;
                    break;
                case 1:
                    Temp.Source = new Uri(Environment.CurrentDirectory + "\\Dictionary2.xaml");
                    this.Resources = Temp;
                    break;
                case 2:
                    Temp.Source = new Uri(Environment.CurrentDirectory + "\\Dictionary3.xaml");
                    this.Resources = Temp;
                    break;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            listBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            comboBox1.SelectedIndex = 1;
            listBox1.SelectedIndex = 1;

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            comboBox1.SelectedIndex = 2;
            listBox1.SelectedIndex = 2;

        }

        private void ListBoxItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (listBox1.SelectedIndex)
            { 
                case 0:
                    comboBox1.SelectedIndex = 0;
                    break;
                case 1:
                    comboBox1.SelectedIndex = 1;
                    break;
                case 2:
                    comboBox1.SelectedIndex = 2;
                    break;
                default:
                    comboBox1.SelectedIndex=-1;
                    break;
            }
        }



        //private void button1_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    //button1.Style = (Style)this.Resources["Font"];
        //    ResourceDictionary Temp = new ResourceDictionary();
        //    try
        //    {
        //        Temp.Source = new Uri("Dictionary1.xaml", UriKind.Relative);
        //        this.Resources = Temp;
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message);
        //    }
        //}
        
    }
}
