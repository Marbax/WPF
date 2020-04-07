using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using DeanOffice.DataModels;
using DeanOffice.ViewModels;

namespace DeanOffice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FacultiesView _fv = new FacultiesView();
        GroupsView _gv = new GroupsView();
        StudentsView _sv = new StudentsView();

        public MainWindow()
        {
            InitializeComponent();
            LoadFaculties();
        }

        public void LoadFaculties()
        {
            _fv.LoadData();
            if (_fv.ObjCollection.Count > 0)
            {
                cbFaculties.ItemsSource = _fv.ObjCollection; // привязка
                cbFaculties.DisplayMemberPath = "Name";
                cbFaculties.SelectedValuePath = "Id";
                if (cbFaculties.Items.Count > 0)
                    cbFaculties.SelectedIndex = 0;
            }
        }

        public void LoadGroups(int fId)
        {
            _gv.LoadData(fId);
            if (_gv.ObjCollection.Count > 0)
            {
                lbGroups.ItemsSource = _gv.ObjCollection; // привязка
                lbGroups.DisplayMemberPath = "Name";
                lbGroups.SelectedValuePath = "Id";
                if (lbGroups.Items.Count > 0)
                    lbGroups.SelectedIndex = 0;
            }
        }

        public void LoadStudents(int gId)
        {
            _sv.LoadData(gId);
            if (_sv.ObjCollection.Count > 0)
            {
                lbStudents.ItemsSource = _sv.ObjCollection; // привязка
                lbStudents.DisplayMemberPath = "FName";
                lbStudents.SelectedValuePath = "Id";
                if (lbStudents.Items.Count > 0)
                    lbStudents.SelectedIndex = 0;
            }
        }

        private void ClearStudentsTextBoxes()
        {
            tbStudentName.Clear();
            tbStudentAdress.Clear();
            tbStudentChares.Clear();
            tbStudentContacts.Clear();
            tbStudentPerf.Clear();
            tbStudentPhone.Clear();
        }

        private void btnAddName_Click(object sender, RoutedEventArgs e)
        {
            string fName = tbFacName.Text.Trim(' ');
            if (string.IsNullOrEmpty(fName))
                MessageBox.Show("Wrong name!", "Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                Faculty faculty = new Faculty() { Name = fName };
                _fv.AddObj(faculty);
                tbFacName.Clear();
                LoadFaculties();
            }
        }

        private void brnRemoveName_Click(object sender, RoutedEventArgs e)
        {
            if (cbFaculties.SelectedIndex != -1)
                if (MessageBox.Show($"Rly remove {(cbFaculties.SelectedItem as Faculty).Name} ?", "Info", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _gv.RemoveByFacultyId((int)cbFaculties.SelectedValue);
                    _fv.RemoveObj((int)cbFaculties.SelectedValue);
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

        private void btnAddGroup_Click(object sender, RoutedEventArgs e)
        {
            if (cbFaculties.SelectedIndex != -1)
            {
                string gName = tbGroupName.Text.Trim(' ');
                if (string.IsNullOrEmpty(gName))
                    MessageBox.Show("Wrong name!", "Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    Group group = new Group() { Name = gName, FacultyId = (int)cbFaculties.SelectedValue };
                    _gv.AddObj(group);
                    tbGroupName.Clear();
                    LoadGroups((int)cbFaculties.SelectedValue);
                }
            }
            else
                MessageBox.Show("No faculty has been selected!", "Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void brnRemoveGroup_Click(object sender, RoutedEventArgs e)
        {
            if (lbGroups.SelectedIndex != -1)
                if (MessageBox.Show($"Rly remove {(lbGroups.SelectedItem as Group).Name} ?", "Info", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _sv.RemoveByGroupId((int)lbGroups.SelectedValue);
                    _gv.RemoveObj((int)lbGroups.SelectedValue);
                    LoadGroups((int)cbFaculties.SelectedValue);
                }
        }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (lbGroups.SelectedIndex != -1)
            {
                string sName = tbStudentName.Text.Trim(' ');
                if (!string.IsNullOrEmpty(sName))
                {
                    Student student = new Student()
                    {
                        FName = sName,
                        GroupId = (int)lbGroups.SelectedValue,
                        Adress = tbStudentAdress.Text,
                        Characteristic = tbStudentChares.Text,
                        Contacts = tbStudentContacts.Text,
                        Perfomance = tbStudentPerf.Text,
                        Phone = tbStudentPhone.Text
                    };

                    try
                    {
                        _sv.AddObj(student);
                        ClearStudentsTextBoxes();
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show($"{sName} already exist!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        LoadStudents((int)lbGroups.SelectedValue); // обновление таблицы , т.к. в локальной копии хранится все , а у базы имя студента уникальное ,следовательно возникают коллизии
                    }
                }
                else
                    MessageBox.Show("Wrong name!", "Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
                MessageBox.Show("No faculty has been selected!", "Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void brnClearStudent_Click(object sender, RoutedEventArgs e)
        {
            ClearStudentsTextBoxes();
        }

        private void brnRemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            if (lbStudents.SelectedIndex != -1)
                if (MessageBox.Show($"Rly remove {(lbStudents.SelectedItem as Student).FName} ?", "Info", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _sv.RemoveObj((int)lbStudents.SelectedValue);
                    LoadStudents((int)lbGroups.SelectedValue);
                }
        }

        private void brnEditStudent_Click(object sender, RoutedEventArgs e)
        {
            if (lbGroups.SelectedIndex != -1)
            {
                string sName = tbStudentName.Text.Trim(' ');
                if (!string.IsNullOrEmpty(sName))
                {
                    Student student = new Student()
                    {
                        Id = (int)lbStudents.SelectedValue,
                        FName = sName,
                        GroupId = (int)lbGroups.SelectedValue,
                        Adress = tbStudentAdress.Text,
                        Characteristic = tbStudentChares.Text,
                        Contacts = tbStudentContacts.Text,
                        Perfomance = tbStudentPerf.Text,
                        Phone = tbStudentPhone.Text
                    };
                    _sv.EditStudentData(student);
                    ClearStudentsTextBoxes();
                    LoadStudents((int)lbGroups.SelectedValue);
                }
                else
                    MessageBox.Show("Wrong name!", "Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
                MessageBox.Show("No faculty has been selected!", "Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);

        }
    }
}
