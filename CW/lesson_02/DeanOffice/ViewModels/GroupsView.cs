using System.Linq;
using DeanOffice.DataModels;
// INotifyPropertyChanged
using System.ComponentModel;
using System.Runtime.CompilerServices;
//sql spaces
using System.Data;

namespace DeanOffice.ViewModels
{
    class GroupsView : AViewModel<Group>, INotifyPropertyChanged
    {
        public GroupsView() : base("Groups") { }

        public override void LoadData()
        {
            _table.Clear();
            _dap.Fill(_table);
            ObjCollection.Clear();
            foreach (DataRow row in _table.Rows)
            {
                Group g = new Group() { Id = (int)row["Id"], Name = (string)row["Name"], FacultyId = (int)row["FacultyId"] };
                ObjCollection.Add(g);
            }
        }

        public void LoadData(int fId)
        {
            _table.Clear();
            _dap.Fill(_table);
            var rows = _table.Select($"FacultyId = {fId}");
            ObjCollection.Clear();
            foreach (DataRow row in rows)
            {
                Group g = new Group() { Id = (int)row["Id"], Name = (string)row["Name"], FacultyId = (int)row["FacultyId"] };
                ObjCollection.Add(g);
            }
        }

        public override void AddObj(Group group)
        {
            DataRow row = _table.NewRow();
            row["Id"] = group.Id; // передается 0
            row["Name"] = group.Name;
            row["FacultyId"] = group.FacultyId;
            _table.Rows.Add(row);
            _dap.Update(_table); // insert сам сгенерит Id
        }

        public void RemoveByFacultyId(int fId)
        {
            var rows = _table.Select($"FacultyId = {fId}");
            if (rows.Count() > 0)
            {
                foreach (var row in rows)
                    row.Delete();
                _dap.Update(_table);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
