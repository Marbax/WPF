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
using DeanOffice.DataModels;
using DeanOffice.ViewModels;

namespace DeanOffice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// на 11
        /// Group ops 
        /// btns edit (не удалять,а именно редактировать)
        /// не добавлять одинаковых студентов
        /// 
        /// на 12
        /// сделать базовый класс для ViewModels
        /// </summary>
        FacultiesView _vm = new FacultiesView();
        GroupsView _gv = new GroupsView();
        StudentsView _sv = new StudentsView();

        public MainWindow()
        {
            InitializeComponent();
            LoadFaculties();
        }

        public void LoadFaculties()
        {
            _vm.LoadData();
            if (_vm.Faculties.Count > 0)
            {
                cbFaculties.ItemsSource = _vm.Faculties; // привязка
                cbFaculties.DisplayMemberPath = "Name";
                cbFaculties.SelectedValuePath = "Id";
                if (cbFaculties.Items.Count > 0)
                    cbFaculties.SelectedIndex = 0;
            }
        }

        public void LoadGroups(int fId)
        {
            _gv.LoadData(fId);
            if (_gv.Groups.Count > 0)
            {
                lbGroups.ItemsSource = _gv.Groups; // привязка
                lbGroups.DisplayMemberPath = "Name";
                lbGroups.SelectedValuePath = "Id";
                if (lbGroups.Items.Count > 0)
                    lbGroups.SelectedIndex = 0;
            }
        }

        public void LoadStudents(int gId)
        {
            _sv.LoadData(gId);
            if (_sv.Students.Count > 0)
            {
                lbStudents.ItemsSource = _sv.Students; // привязка
                lbStudents.DisplayMemberPath = "FName";
                lbStudents.SelectedValuePath = "Id";
                if (lbStudents.Items.Count > 0)
                    lbStudents.SelectedIndex = 0;
            }
        }

        private void btnAddName_Click(object sender, RoutedEventArgs e)
        {
            string fName = tbFacName.Text;
            if (string.IsNullOrEmpty(fName.Trim(' ')))
                MessageBox.Show("Wrong name!", "Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                Faculty faculty = new Faculty() { Name = fName };
                _vm.AddFaculty(faculty);
                tbFacName.Clear();
                LoadFaculties();
            }
        }

        private void brnRemoveName_Click(object sender, RoutedEventArgs e)
        {
            if (cbFaculties.SelectedIndex != -1)
                if (MessageBox.Show($"Rly remove {(cbFaculties.SelectedItem as Faculty).Name} ?", "Info", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _vm.RemoveFaculty((int)cbFaculties.SelectedValue);
                    LoadFaculties();
                }
        }

        private void cbFaculties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFaculties.SelectedIndex != -1)
                LoadGroups((int)cbFaculties.SelectedValue);
        }

        private void lbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbGroups.SelectedIndex != -1)
                LoadStudents((int)lbGroups.SelectedValue);
        }

        private void lbStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbStudents.SelectedIndex != -1)
            {
                Student curSt = lbStudents.SelectedItem as Student;
                tbStudentName.Text = curSt.FName;
                tbStudentAdress.Text = curSt.Adress;
                tbStudentPhone.Text = curSt.Phone;
                tbStudentContacts.Text = curSt.Contacts;
                tbStudentPerf.Text = curSt.Perfomance;
                tbStudentChares.Text = curSt.Characteristic;
            }
            else
                tbStudentName.Text = tbStudentAdress.Text = tbStudentPhone.Text =
                    tbStudentContacts.Text = tbStudentPerf.Text = tbStudentChares.Text = "";
        }
    }
}
