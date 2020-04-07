using System.Linq;
using DeanOffice.DataModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data;

namespace DeanOffice.ViewModels
{
    class StudentsView : AViewModel<Student>, INotifyPropertyChanged
    {

        public StudentsView() : base("Students") { }

        public override void LoadData()
        {
            _table.Clear();
            _dap.Fill(_table);
            ObjCollection.Clear();
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
                ObjCollection.Add(s);
            }
        }

        public void LoadData(int gId)
        {
            _table.Clear();
            _dap.Fill(_table);
            var rows = _table.Select($"GroupId = {gId}");
            ObjCollection.Clear();
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
                ObjCollection.Add(s);
            }
        }

        public override void AddObj(Student toAdd)
        {
            DataRow row = _table.NewRow();
            row["Id"] = toAdd.Id; // передается 0
            row["FName"] = toAdd.FName;
            row["Adress"] = toAdd.Adress;
            row["Phone"] = toAdd.Phone;
            row["Contacts"] = toAdd.Contacts;
            row["Perfomance"] = toAdd.Perfomance;
            row["Characteristic"] = toAdd.Characteristic;
            row["GroupId"] = toAdd.GroupId;
            _table.Rows.Add(row);
            _dap.Update(_table); // insert сам сгенерит Id
        }

        public void RemoveByGroupId(int gId)
        {
            var rows = _table.Select($"GroupId = {gId}");
            if (rows.Count() > 0)
            {
                foreach (var row in rows)
                    row.Delete();
                _dap.Update(_table);
            }
        }

        public void EditStudentData(Student toEdit)
        {
            var row = _table.Select($"Id = {toEdit.Id}").FirstOrDefault();
            row["FName"] = toEdit.FName;
            row["Adress"] = toEdit.Adress;
            row["Phone"] = toEdit.Phone;
            row["Contacts"] = toEdit.Contacts;
            row["Perfomance"] = toEdit.Perfomance;
            row["Characteristic"] = toEdit.Characteristic;
            row["GroupId"] = toEdit.GroupId;
            _dap.Update(_table);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
