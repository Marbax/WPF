using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace EngRusDictionary.ViewModel
{
    public class AviewModel<T>
    {
        protected string _selectedProvider = null;
        protected DbProviderFactory _fact = null;
        protected DbConnection _conn = null;
        protected string _connString = null;
        protected DbDataAdapter _adapter = null;
        protected DataSet _dataSet = null;
        public DataTable MainTable { get => _dataSet.Tables[MainTableName]; }
        public DataView MainView { get; set; }
        public readonly string MainTableName;
        public readonly string[] TableNames;

        public AviewModel(string[] tableNames, string connectionName, bool fill = false)
        {
            if (tableNames == null || tableNames.Length == 0)
                throw new ApplicationException("No tables passed.");

            _dataSet = new DataSet();
            MainTableName = tableNames[0];
            TableNames = tableNames;
            _connString = GetConnectionStringByConnectionName(connectionName);
            _selectedProvider = GetProviderByConnectionName(connectionName);
            _fact = DbProviderFactories.GetFactory(_selectedProvider);
            _conn = _fact.CreateConnection();
            _conn.ConnectionString = _connString;
            _adapter = _fact.CreateDataAdapter();
            _adapter.SelectCommand = GetSelectCommand();
            DbCommandBuilder builder = _fact.CreateCommandBuilder();// you can work only with dataset or table , _adapter.Update(); will automatically sinchronize changes with db(by using _adapter commands) (https://metanit.com/sharp/adonet/3.3.php)
            builder.DataAdapter = _adapter;
            SetMapping(tableNames);
            MainView = new DataView(MainTable);
            if (fill)
                _adapter.Fill(_dataSet);
        }

        public virtual DbCommand GetSelectCommand()
        {
            DbCommand cmd = _conn.CreateCommand();
            cmd.Connection = _conn;
            cmd.CommandType = CommandType.Text;
            foreach (string table in TableNames)
                cmd.CommandText += $"select * from {table};";
            return cmd;
        }
        /// <summary>
        /// Find Connection String by connection name in App.config
        /// </summary>
        /// <param name="connnectionName"></param>
        /// <returns><paramref name="ConnectionString"/></returns>
        public static string GetConnectionStringByConnectionName(string connnectionName)
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            if (settings != null)
                foreach (ConnectionStringSettings cs in settings)
                    if (cs.Name == connnectionName)
                        return cs.ConnectionString;
            throw new ApplicationException($"There no connection string for {connnectionName}.");
        }
        /// <summary>
        /// Find Provider Name by connection name in App.config
        /// </summary>
        /// <param name="connnectionName"></param>
        /// <returns><paramref name="ProviderName"/></returns>
        public static string GetProviderByConnectionName(string connnectionName)
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            if (settings != null)
                foreach (ConnectionStringSettings cs in settings)
                    if (cs.Name == connnectionName)
                        return cs.ProviderName;
            throw new ApplicationException($"There no provider for {connnectionName}.");
        }


        /// <summary>
        /// Example of mapping
        /// </summary>
        public virtual void SetMapping(string[] tableNames)
        {
            for (int i = 0; i < tableNames.Length; i++)
            {
                if (i == 0)
                    _adapter.TableMappings.Add("Table", MainTableName);
                else
                    _adapter.TableMappings.Add($"Table{i}", tableNames[i]);

            }
        }

        /// <summary>
        /// Hardcoded Example to filter Cards Table by card owner name
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public virtual DataView GetView(string word = "")
        {
            if (_dataSet == null)
                throw new ApplicationException("DataSet points to null.");
            DataViewManager dvm = new DataViewManager(_dataSet);
            dvm.DataViewSettings[MainTableName].RowFilter = $"Id like '%' +'{word}'+ '%'";
            //dvm.DataViewSettings[TableName].Sort = "MoneyOnCard asc";
            return dvm.CreateDataView(_dataSet.Tables[MainTableName]);
        }

    }
}
