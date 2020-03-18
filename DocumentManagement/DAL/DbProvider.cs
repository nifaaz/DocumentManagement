using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Model
{

    public class Db
    {
        private string _connectionString;
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private SqlTransaction _transaction;
        public SqlConnection Connection
        {
            get
            {
                return _connection;
            }
        }
        public SqlCommand Command
        {
            get
            {
                return _command;
            }
        }
        public SqlDataReader Reader
        {
            get
            {
                if (_reader == null)
                {
                    _reader = _command.ExecuteReader();
                }
                return _reader;
            }
        }

        public SqlTransaction Transaction
        {
            get
            {
                return _transaction;
            }
        }
        public Db()
        {
               _connectionString = "Data Source=171.229.253.67\\SQLEXPRESS,1433;Initial Catalog=SoHoa;User ID=DucNghiep;Password=123456";
          //  _connectionString = "Data Source=DESKTOP-FJ46P29\\MSSQLSERVER2019;Initial Catalog=SoHoa;User ID=sa;Password=123456";
            _connection = new SqlConnection(_connectionString);
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }

        }

        public void Build(string cmdText, CommandType commandType)
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            if (_command == null)
            {
                _command = new SqlCommand(cmdText, _connection)
                {
                    CommandType = commandType
                };
            }
            _reader = null;
        }
    }
    public class DbProvider
    {
        private Db _db;
        public DbProvider()
        {
            _db = new Db();
        }

        /// <summary>
        /// Set the command object with the commandText and commandType. Example: if the commandType is stored procedure then cmdText should be stored procudure name. Otherwise, if the commandType is query text then cmdText should be query string.
        /// </summary>
        public DbProvider SetQuery(string cmdText, CommandType commandType)
        {
            _db.Build(cmdText, commandType);
            return this;
        }

        /// <summary>
        /// Set Command prameter
        /// </summary>
        /// <returns></returns>
        public DbProvider SetParameter(string paramName, SqlDbType sqlDbType, object value, int size, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            if (size > 0)
            {
                var param = new SqlParameter()
                {
                    ParameterName = paramName,
                    SqlDbType = sqlDbType,
                    Direction = parameterDirection,
                    Size = size
                };
                if (value != DBNull.Value)
                {
                    param.Value = value;
                }
                _db.Command.Parameters.Add(param);
            }
            return this;
        }

        public DbProvider SetParameter(string paramName, SqlDbType sqlDbType, object value, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            var param = new SqlParameter()
            {
                ParameterName = paramName,
                SqlDbType = sqlDbType,
                Direction = parameterDirection,
            };
            if (value != DBNull.Value)
            {
                param.Value = value;
            }
            _db.Command.Parameters.Add(param);
            return this;
        }

        /// <summary>
        /// This method is intended to use for UPDATE, DELETE, INSERT queries
        /// </summary>
        /// <param name="recordRows"> The number of rows affected by the command</param>
        /// <returns></returns>
        public DbProvider ExcuteNonQuery(out int recordRows)
        {
            recordRows = _db.Command.ExecuteNonQuery();
            if (recordRows != 0)
            {
                _db.Connection.Close();
            }
            return this;
        }

        public DbProvider ExcuteNonQuery()
        {
            _db.Command.ExecuteNonQuery();

            return this;
        }

        public DbProvider GetList<T>(out List<T> genericList) where T : new()
        {
            genericList = new List<T>();
            var commandReader = _db.Reader;
            if (_db.Reader.HasRows)
            {
                while (commandReader.Read())
                {
                    var obj = new T();
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            var value = CheckCurrentReaderValue(commandReader[prop.Name]);
                            prop.SetValue(obj, value);
                        }
                        catch (Exception ex)
                        {
                            prop.SetValue(obj, null);
                        }
                    }
                    genericList.Add(obj);
                }
            }

            return this;
        }

        public DbProvider GetSingle<T>(out T obj) where T : new()
        {
            obj = new T();
            var commandReader = _db.Reader;
            if (_db.Reader.HasRows)
            {
                while (commandReader.Read())
                {
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            var value = CheckCurrentReaderValue(commandReader[prop.Name]);
                            prop.SetValue(obj, value);
                        }
                        catch (Exception ex)
                        {
                            prop.SetValue(obj, null);
                        }
                    }
                    break;
                }
            }
            return this;
        }

        public DbProvider Complete()
        {
            if (_db.Transaction != null)
            {
                _db.Transaction.Dispose();
            }
            if (_db.Command != null)
            {
                _db.Command.Dispose();
            }
            if (_db.Connection != null)
            {
                _db.Connection.Close();
            }
            return this;
        }

        public DbProvider GetOutValue(string outParamName, out int outValue)
        {
            outValue = Convert.ToInt32(_db.Command.Parameters[outParamName].Value);
            return this;
        }

        public DbProvider GetOutValue(string outParamName, out string outValue)
        {
            outValue = _db.Command.Parameters[outParamName].Value.ToString();
            return this;
        }

        private object CheckCurrentReaderValue(object reader)
        {
            if (reader.GetType() == typeof(System.DBNull))
            {
                return null;
            }
            else
            {
                return reader;
            }
        }
    }
}

