using System.Linq;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DeanOffice.ViewModels
{
    abstract class AViewModel<T>
    {
        string _connectionString;
        string _tabName;
        SqlConnection _conn;
        protected SqlDataAdapter _dap;
        protected DataTable _table;

        public ObservableCollection<T> ObjCollection { get; set; }

        public AViewModel(string tabName)
        {
            ObjCollection = new ObservableCollection<T>();
            _connectionString = ConfigurationManager.ConnectionStrings["Deans"].ConnectionString;
            _conn = new SqlConnection(_connectionString);
            _tabName = tabName;
            string baseQuery = $"select * from {_tabName}";
            _dap = new SqlDataAdapter(baseQuery, _conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(_dap);
            _table = new DataTable();
        }

        abstract public void LoadData();

        abstract public void AddObj(T toAdd);

        virtual public void RemoveObj(int index)
        {
            var row = _table.Select($"Id = {index}").FirstOrDefault();
            row.Delete();
            _dap.Update(_table);
        }
    }
}
