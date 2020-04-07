using DeanOffice.DataModels;
// INotifyPropertyChanged
using System.ComponentModel;
using System.Runtime.CompilerServices;
//sql spaces
using System.Data;

namespace DeanOffice.ViewModels
{
    class FacultiesView : AViewModel<Faculty>, INotifyPropertyChanged
    {

        public FacultiesView() : base("Faculties") { }

        public override void LoadData()
        {
            _table.Clear();
            _dap.Fill(_table);
            ObjCollection.Clear();
            foreach (DataRow row in _table.Rows)
            {
                Faculty f = new Faculty() { Id = (int)row["Id"], Name = (string)row["Name"] };
                ObjCollection.Add(f);
            }
        }

        public override void AddObj(Faculty faculty)
        {
            DataRow row = _table.NewRow();
            row["Id"] = faculty.Id; // передается 0
            row["Name"] = faculty.Name;
            _table.Rows.Add(row);
            _dap.Update(_table); // insert сам сгенерит Id
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
