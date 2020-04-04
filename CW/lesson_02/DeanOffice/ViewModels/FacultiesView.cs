﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeanOffice.DataModels;
// INotifyPropertyChanged
using System.ComponentModel;
using System.Runtime.CompilerServices;
// collection
using System.Collections.ObjectModel;

//sql spaces
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DeanOffice.ViewModels
{
    class FacultiesView : INotifyPropertyChanged
    {

        private string _connectionString;
        SqlConnection _conn;
        SqlDataAdapter _dap;
        DataTable _table;

        public ObservableCollection<DataModels.Faculty> Faculties { get; set; }

        public FacultiesView()
        {
            Faculties = new ObservableCollection<Faculty>();
            _connectionString = ConfigurationManager.ConnectionStrings["Deans"].ConnectionString;
            _conn = new SqlConnection(_connectionString);

            string baseQuery = "select * from Faculties";
            _dap = new SqlDataAdapter(baseQuery, _conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(_dap);
            _table = new DataTable();
            //LoadData();
        }

        public void LoadData()
        {
            _table.Clear();
            _dap.Fill(_table);
            Faculties.Clear();
            foreach (DataRow row in _table.Rows)
            {
                Faculty f = new Faculty() { Id = (int)row["Id"], Name = (string)row["Name"] };
                Faculties.Add(f);
            }
        }

        public void AddFaculty(Faculty faculty)
        {
            DataRow row = _table.NewRow();
            row["Id"] = faculty.Id; // передается 0
            row["Name"] = faculty.Name;
            _table.Rows.Add(row);
            _dap.Update(_table); // insert сам сгенерит Id
        }

        public void RemoveFaculty(int fId)
        {
            var row = _table.Select($"Id = {fId}").FirstOrDefault();
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
