using atf.toolbox.configuration;
using atf.toolbox.database;
using atf.toolbox.interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atf.toolbox.managers
{
    public class DatabaseAutomationManager
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(DatabaseAutomationManager));

        /// <summary>
        /// DatabaseConfiguration
        /// Configuration section DatabaseSettings to be used to setup the database driver for the test run
        /// </summary>
        public DatabaseSettings DatabaseConfiguration
        {
            get { return (DatabaseSettings)ConfigurationManager.GetSection("DatabaseSettings"); }
        }

        private Dictionary<string, IDatabaseService> _DatabaseServices;

        public DatabaseAutomationManager()
        {
            _DatabaseServices = new Dictionary<string, IDatabaseService>();
        }

        /// <summary>
        /// AddDatabaseService
        /// Adds a Database Service to the collection with the key provided
        /// If the key already exists, the Database Service will be replaced within the collection
        /// </summary>
        /// <param name="key">key for the Database Service in the collction</param>
        /// <param name="DatabaseService">Database Service</param>
        public void AddDatabaseService(string key, IDatabaseService DatabaseService)
        {
            if (_DatabaseServices.ContainsKey(key))
            {
                _log.Info("Replaced database service for key :" + key);
                _DatabaseServices[key] = DatabaseService;
            }
            else
            {
                _log.Info("Added database service with key: " + key);
                _DatabaseServices.Add(key, DatabaseService);
            }
        }

        /// <summary>
        /// RemoveDatabaseService
        /// </summary>
        /// <param name="key">key to locate the Database Service to remove from the collection</param>
        public void RemoveDatabaseService(string key)
        {
            if (_DatabaseServices.ContainsKey(key))
            {
                // reset the value for this key
                bool success = _DatabaseServices.Remove(key);
                if (success) _log.Info("Successfully removed database service : " + key);
                else _log.Info("Unable to remove database service: " + key);
            }
            else
            {
                _log.Info("Unable to remove database service. No database service found with key : " + key);
            }            
        }

        /// <summary>
        /// DatabaseService
        /// </summary>
        /// <param name="key">Key to locate the database service</param>
        /// <returns>IDatabase Service for the key provided</returns>
        public IDatabaseService DatabaseService(string key)
        {
            if (_DatabaseServices.ContainsKey(key))
            {
                return _DatabaseServices[key];
            }
            else
            {
                _log.Warn("Unable to locate Database Service for key: " + key+ " returning null.");
                return null;
            }
        }

        public void TearDown()
        {
            
        }

        /// <summary>
        /// SelectStatementColumnBuilder
        /// </summary>
        /// <param name="columnsToReturn">columns to return from a select statement</param>
        /// <returns>list of columns to return</returns>
        public string SelectStatementColumnBuilder(string[] columnsToReturn)
        {
            if (columnsToReturn != null)
            {
                StringBuilder columns = new StringBuilder();
                foreach (string columnName in columnsToReturn)
                {
                    if (columns.ToString() != string.Empty) columns.Append(", ");
                    columns.Append(columnName);
                }
                return columns.ToString();
            }
            return "*";
        }

        /// <summary>
        /// SelectStatementConditionsBuilder
        /// </summary>
        /// <param name="selectParams">select parameters</param>
        /// <returns>Select Statement</returns>
        public string SelectStatementConditionsBuilder(List<SelectParam> selectParams)
        {
            StringBuilder selectStatement = new StringBuilder();

            foreach (SelectParam param in selectParams)
            {
                if (Type.GetTypeCode(param.ColumnValue.GetType()) == TypeCode.Int32)
                {
                    if (selectStatement.Length > 0) selectStatement.Append(" AND ");
                    selectStatement.Append(param.ColumnName);
                    selectStatement.Append(" = ");
                    selectStatement.Append((int)param.ColumnValue);
                }
                else if (Type.GetTypeCode(param.ColumnValue.GetType()) == TypeCode.Decimal)
                {
                    if (selectStatement.Length > 0) selectStatement.Append(" AND ");
                    selectStatement.Append(param.ColumnName);
                    selectStatement.Append(" = ");
                    selectStatement.Append((decimal)param.ColumnValue);
                }
                else if (Type.GetTypeCode(param.ColumnValue.GetType()) == TypeCode.Double)
                {
                    if (selectStatement.Length > 0) selectStatement.Append(" AND ");
                    selectStatement.Append(param.ColumnName);
                    selectStatement.Append(" = ");
                    selectStatement.Append((double)param.ColumnValue);
                }
                else if (Type.GetTypeCode(param.ColumnValue.GetType()) == TypeCode.DateTime)
                {
                    string resetStatement = selectStatement.ToString();
                    try
                    {
                        DateTime currDay = Convert.ToDateTime(param.ColumnValue.ToString());
                        DateTime nextDay = Convert.ToDateTime(param.ColumnValue.ToString()).AddDays(1);

                        string currDate = currDay.ToString("yyyy-MM-dd");
                        string nextDate = nextDay.ToString("yyyy-MM-dd");

                        if (selectStatement.Length > 0) selectStatement.Append(" AND (");
                        selectStatement.Append(param.ColumnName);
                        selectStatement.Append(" >= '");
                        selectStatement.Append(currDate);
                        selectStatement.Append("T00:00:00.000'");
                        selectStatement.Append(" AND ");
                        selectStatement.Append(param.ColumnName);
                        selectStatement.Append(" < '");
                        selectStatement.Append(nextDate);
                        selectStatement.Append("T00:00:00.000')");

                    }
                    catch (Exception ex)
                    {
                        _log.Error("Unable to use date in selection. Invalid date provided.", ex);
                        selectStatement.Clear();
                        selectStatement.Append(resetStatement);
                    }
                }
                else
                {
                    if (selectStatement.Length > 0) selectStatement.Append(" AND ");
                    selectStatement.Append(param.ColumnName);
                    selectStatement.Append(" = '");
                    selectStatement.Append(param.ColumnValue.ToString());
                    selectStatement.Append("'");
                }
            }

            return selectStatement.ToString();
        }
    }
}
