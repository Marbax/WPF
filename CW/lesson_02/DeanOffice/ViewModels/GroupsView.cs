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
    public class GroupsView : INotifyPropertyChanged
    {
        private string _connectionString;
        SqlConnection _conn;
        SqlDataAdapter _dap;
        DataTable _table;

        public ObservableCollection<Group> Groups { get; set; }

        public GroupsView()
        {
            Groups = new ObservableCollection<Group>();
            _connectionString = ConfigurationManager.ConnectionStrings["Deans"].ConnectionString;
            _conn = new SqlConnection(_connectionString);

            string baseQuery = "select * from Groups";
            _dap = new SqlDataAdapter(baseQuery, _conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(_dap); // buildor working in background
            _table = new DataTable();
        }

        public void LoadData()
        {
            _table.Clear();
            _dap.Fill(_table);
            Groups.Clear();
            foreach (DataRow row in _table.Rows)
            {
                Group g = new Group() { Id = (int)row["Id"], Name = (string)row["Name"], FacultyId = (int)row["FacultyId"] };
                Groups.Add(g);
            }
        }

        public void LoadData(int fId)
        {
            _table.Clear();
            _dap.Fill(_table);
            var rows = _table.Select($"FacultyId = {fId}");
            Groups.Clear();
            foreach (DataRow row in rows)
            {
                Group g = new Group() { Id = (int)row["Id"], Name = (string)row["Name"], FacultyId = (int)row["FacultyId"] };
                Groups.Add(g);
            }
        }

        public void AddGroup(Group group)
        {
            DataRow row = _table.NewRow();
            row["Id"] = group.Id; // передается 0
            row["Name"] = group.Name;
            row["FacultyId"] = group.FacultyId;
            _table.Rows.Add(row);
            _dap.Update(_table); // insert сам сгенерит Id
        }

        public void RemoveGroup(int gId)
        {
            var row = _table.Select($"Id = {gId}").FirstOrDefault();
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
