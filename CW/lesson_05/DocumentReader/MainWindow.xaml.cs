using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Xml;
using System.IO;
using System.Windows.Markup;

namespace DocumentReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }

        private void SwitchProvider(string key)
        {
            XmlDataProvider xmlData = FindResource(key) as XmlDataProvider;
            Binding binding = new Binding() { Source = xmlData };
            BindingOperations.SetBinding(tvContext, TreeView.ItemsSourceProperty, binding);
        }

        private void btnUserGuide_Click(object sender, RoutedEventArgs e)
        {
            SwitchProvider("UserProvider");
        }

        private void btnAdminGuide_Click(object sender, RoutedEventArgs e)
        {
            SwitchProvider("AdminProvider");
        }

        private void btnDevGuide_Click(object sender, RoutedEventArgs e)
        {
            SwitchProvider("DevProvider");
        }

        private void tvContext_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                var tree = sender as TreeView;
                var item = tree.SelectedItem as XmlElement;
                string name = item.Attributes["name"].Value;
                string path = @"..\..\Pages\" + name + ".xaml";

                if (File.Exists(path))
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                        fdrReader.Document = XamlReader.Load(fs) as FlowDocument;
                else
                    fdrReader.Document = new FlowDocument();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Tv item cahnge ex : {ex}");
                MessageBox.Show("404");
            }
        }
    }
}
