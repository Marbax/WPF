using System.Linq;
using DeanOffice.DataModels;
// INotifyPropertyChanged
using System.ComponentModel;
using System.Runtime.CompilerServices;
// collection for observer
using System.Collections.ObjectModel;
//sql spaces
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DeanOffice.ViewModels
{
    class StudentsView : INotifyPropertyChanged
    {
        string _connectionString;
        SqlConnection _conn;
        SqlDataAdapter _dap;
        DataTable _table;

        public ObservableCollection<Student> Students { get; set; }

        public StudentsView()
        {
            Students = new ObservableCollection<Student>();
            _connectionString = ConfigurationManager.ConnectionStrings["Deans"].ConnectionString;
            _conn = new SqlConnection(_connectionString);

            string baseQuery = "select * from Students";
            _dap = new SqlDataAdapter(baseQuery, _conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(_dap); // buildor working in background
            _table = new DataTable();
        }

        public void LoadData()
        {
            _table.Clear();
            _dap.Fill(_table);
            Students.Clear();
            foreach (DataRow row in _table.Rows)
            {
                Student s = new Student()
                {
                    Id = (int)row["Id"],
                    FName = (string)row["FName"],
                    Adress = (string)row["Adress"],
                    Phone = row["Phone"] as string,
                    Contacts = row["Contacts"] as string,
                    Perfomance = row["Perfomance"] as string,
                    Characteristic = row["Characteristic"] as string,
                    GroupId = (int)row["GroupId"]
                };
                Students.Add(s);
            }
        }

        public void LoadData(int gId)
        {
            _table.Clear();
            _dap.Fill(_table);
            var rows = _table.Select($"GroupId = {gId}");
            Students.Clear();
            foreach (DataRow row in rows)
            {
                Student s = new Student()
                {
                    Id = (int)row["Id"],
                    FName = (string)row["FName"],
                    Adress = (string)row["Adress"],
                    Phone = row["Phone"] as string,
                    Contacts = row["Contacts"] as string,
                    Perfomance = row["Perfomance"] as string,
                    Characteristic = row["Characteristic"] as string,
                    GroupId = (int)row["GroupId"]
                };
                Students.Add(s);
            }
        }

        public void AddStudent(Student student)
        {
            DataRow row = _table.NewRow();
            row["Id"] = student.Id; // передается 0
            row["FName"] = student.FName;
            row["Adress"] = student.Adress;
            row["Phone"] = student.Phone;
            row["Contacts"] = student.Contacts;
            row["Perfomance"] = student.Perfomance;
            row["Characteristic"] = student.Characteristic;
            row["FacultyId"] = student.GroupId;
            _table.Rows.Add(row);
            _dap.Update(_table); // insert сам сгенерит Id
        }

        public void RemoveStudent(int sId)
        {
            var row = _table.Select($"Id = {sId}").FirstOrDefault();
            row.Delete();
            _dap.Update(_table);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
