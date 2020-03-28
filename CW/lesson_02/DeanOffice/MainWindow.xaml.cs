using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DeanOffice.ViewModels;

namespace DeanOffice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FacultiesView _vm = new FacultiesView();

        public MainWindow()
        {
            InitializeComponent();
            cbFaculties.ItemsSource = _vm.Faculties; // привязка
            cbFaculties.DisplayMemberPath = "Name";
            cbFaculties.SelectedValuePath = "Id";
            if (cbFaculties.Items.Count > 0)
                cbFaculties.SelectedIndex = 0;
        }

        private void btnAddName_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
