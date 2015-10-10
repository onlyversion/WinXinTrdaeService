using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;

namespace JinTong.Jyrj.Data
{
    public class SqlDbHelper
    {
        public DbProviderFactory DbProviderFactory { get; private set; }
        public string ConnectionString { get; private set; }
        public virtual DbParameter BuildDbParameter(string name, object value)
        {
            DbParameter parameter = this.DbProviderFactory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }

        public SqlDbHelper(string cnnStringName)
        {
            var cnnStringSection = ConfigurationManager.ConnectionStrings[cnnStringName];
            this.DbProviderFactory = DbProviderFactories.GetFactory(cnnStringSection.ProviderName);
            this.ConnectionString = cnnStringSection.ConnectionString;
        }

        public SqlDbHelper()
            : this("ConnectionString")
        {
        }

        public static DbProviderFactory GetFactory(string cnnStringName)
        {
            var cnnStringSection = ConfigurationManager.ConnectionStrings[cnnStringName];
            return DbProviderFactories.GetFactory(cnnStringSection.ProviderName);
        }

        #region ExecuteNonQuery

        public int ExecuteNonQuery(string commandText)
        {
            return this.ExecuteNonQuery(commandText, null);
        }

        public int ExecuteNonQuery(string commandText, Dictionary<string, object> parameters)
        {
            DbConnection connection = null;
            DbCommand command = this.DbProviderFactory.CreateCommand();
            DbTransaction dbTransaction = null;
            try
            {
                command.CommandText = commandText;
                parameters = parameters ?? new Dictionary<string, object>();
                foreach (var item in parameters)
                {
                    command.Parameters.Add(this.BuildDbParameter(item.Key, item.Value));
                }
                if (JinTong.Jyrj.Common.Transaction.Current != null)
                {
                    //此处留意，如果开启了事务，会自动切换为事务指定的数据库连接
                    //也就是从业务逻辑层读取连接
                    command.Connection = JinTong.Jyrj.Common.Transaction.Current.DbTransactionWrapper.DbTransaction.Connection;
                    command.Transaction = JinTong.Jyrj.Common.Transaction.Current.DbTransactionWrapper.DbTransaction;
                }
                else
                {
                    connection = this.DbProviderFactory.CreateConnection();
                    connection.ConnectionString = this.ConnectionString;
                    command.Connection = connection;
                    connection.Open();
                    if (System.Transactions.Transaction.Current == null)
                    {
                        dbTransaction = connection.BeginTransaction();
                        command.Transaction = dbTransaction;
                    }
                }
                int result = command.ExecuteNonQuery();
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                }
                return result;
            }
            catch (Exception e)
            {
                if (dbTransaction != null)
                {
                    dbTransaction.Rollback();
                }
                //LogHelper.Write(e.Message + "\r\nSQL：" + command.CommandText);
                throw new Exception(e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                }
                command.Dispose();
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public int ExecuteNonQueryProcedure(string procedureName)
        {
            return this.ExecuteNonQueryProcedure(procedureName, null);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public int ExecuteNonQueryProcedure(string procedureName, Dictionary<string, object> parameters)
        {
            DbConnection connection = null;
            DbCommand command = this.DbProviderFactory.CreateCommand();
            DbTransaction dbTransaction = null;
            try
            {
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                parameters = parameters ?? new Dictionary<string, object>();
                foreach (var item in parameters)
                {
                    command.Parameters.Add(this.BuildDbParameter(item.Key, item.Value));
                }
                if (JinTong.Jyrj.Common.Transaction.Current != null)
                {
                    //此处留意，如果开启了事务，会自动切换为事务指定的数据库连接
                    //也就是从业务逻辑层读取连接
                    command.Connection = JinTong.Jyrj.Common.Transaction.Current.DbTransactionWrapper.DbTransaction.Connection;
                    command.Transaction = JinTong.Jyrj.Common.Transaction.Current.DbTransactionWrapper.DbTransaction;
                }
                else
                {
                    connection = this.DbProviderFactory.CreateConnection();
                    connection.ConnectionString = this.ConnectionString;
                    command.Connection = connection;
                    connection.Open();
                    if (System.Transactions.Transaction.Current == null)
                    {
                        dbTransaction = connection.BeginTransaction();
                        command.Transaction = dbTransaction;
                    }
                }
                int result = command.ExecuteNonQuery();
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                }
                return result;
            }
            catch (Exception e)
            {
                if (dbTransaction != null)
                {
                    dbTransaction.Rollback();
                }
                //LogHelper.Write(e.Message + "\r\nSQL：" + command.CommandText);
                throw new Exception(e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                }
                command.Dispose();
            }
        }

        #endregion

        #region ExecuteScalar

        public object ExecuteScalar(string commandText)
        {
            return this.ExecuteScalar(commandText, null);
        }

        public object ExecuteScalar(string commandText, IDictionary<string, object> parameters)
        {
            DbConnection connection = null;
            DbCommand command = this.DbProviderFactory.CreateCommand();
            DbTransaction dbTransaction = null;
            try
            {
                command.CommandText = commandText;
                parameters = parameters ?? new Dictionary<string, object>();
                foreach (var item in parameters)
                {
                    command.Parameters.Add(this.BuildDbParameter(item.Key, item.Value));
                }
                if (JinTong.Jyrj.Common.Transaction.Current != null)
                {
                    //此处留意，如果开启了事务，会自动切换为事务指定的数据库连接
                    //也就是从业务逻辑层读取连接
                    command.Connection = JinTong.Jyrj.Common.Transaction.Current.DbTransactionWrapper.DbTransaction.Connection;
                    command.Transaction = JinTong.Jyrj.Common.Transaction.Current.DbTransactionWrapper.DbTransaction;
                }
                else
                {
                    connection = this.DbProviderFactory.CreateConnection();
                    connection.ConnectionString = this.ConnectionString;
                    command.Connection = connection;
                    connection.Open();
                    if (System.Transactions.Transaction.Current == null)
                    {
                        dbTransaction = connection.BeginTransaction();
                        command.Transaction = dbTransaction;
                    }
                }
                object result = command.ExecuteScalar();
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                }
                return result;
            }
            catch (SqlException e)
            {
                if (dbTransaction != null)
                {
                    dbTransaction.Rollback();
                }
                //LogHelper.Write(e.Message + "\r\nSQL：" + command.CommandText);
                throw new Exception(e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                }
                command.Dispose();
            }
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public object ExecuteScalarProcedure(string procedureName)
        {
            return this.ExecuteScalarProcedure(procedureName, null);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalarProcedure(string procedureName, Dictionary<string, object> parameters)
        {
            DbConnection connection = null;
            DbCommand command = this.DbProviderFactory.CreateCommand();
            DbTransaction dbTransaction = null;
            try
            {
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                parameters = parameters ?? new Dictionary<string, object>();
                foreach (var item in parameters)
                {
                    command.Parameters.Add(this.BuildDbParameter(item.Key, item.Value));
                }
                if (JinTong.Jyrj.Common.Transaction.Current != null)
                {
                    //此处留意，如果开启了事务，会自动切换为事务指定的数据库连接
                    //也就是从业务逻辑层读取连接
                    command.Connection = JinTong.Jyrj.Common.Transaction.Current.DbTransactionWrapper.DbTransaction.Connection;
                    command.Transaction = JinTong.Jyrj.Common.Transaction.Current.DbTransactionWrapper.DbTransaction;
                }
                else
                {
                    connection = this.DbProviderFactory.CreateConnection();
                    connection.ConnectionString = this.ConnectionString;
                    command.Connection = connection;
                    connection.Open();
                    if (System.Transactions.Transaction.Current == null)
                    {
                        dbTransaction = connection.BeginTransaction();
                        command.Transaction = dbTransaction;
                    }
                }
                object result = command.ExecuteScalar();
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                }
                return result;
            }
            catch (Exception e)
            {
                if (dbTransaction != null)
                {
                    dbTransaction.Rollback();
                }
                //LogHelper.Write(e.Message + "\r\nSQL：" + command.CommandText);
                throw new Exception(e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (dbTransaction != null)
                {
                    dbTransaction.Dispose();
                }
                command.Dispose();
            }
        }

        #endregion

        #region ExecuteReader

        public DbDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(commandText, null);
        }

        public DbDataReader ExecuteReader(string commandText, IDictionary<string, object> parameters)
        {
            DbConnection connection = null;
            DbCommand command = this.DbProviderFactory.CreateCommand();
            try
            {
                command.CommandText = commandText;
                parameters = parameters ?? new Dictionary<string, object>();
                foreach (var item in parameters)
                {
                    command.Parameters.Add(this.BuildDbParameter(item.Key, item.Value));
                }
                connection = this.DbProviderFactory.CreateConnection();
                connection.ConnectionString = this.ConnectionString;
                command.Connection = connection;
                connection.Open();
                DbDataReader result = command.ExecuteReader();
                return result;
            }
            catch (SqlException e)
            {
                connection.Close();
                //LogHelper.Write(e.Message + "\r\nSQL：" + command.CommandText);
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Query

        public DataSet Query(string commandText)
        {
            return Query(commandText, null);
        }

        public DataSet Query(string commandText, IDictionary<string, object> parameters)
        {
            SqlConnection connection = null;
            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandText = commandText;
                parameters = parameters ?? new Dictionary<string, object>();
                foreach (var item in parameters)
                {
                    command.Parameters.Add(this.BuildDbParameter(item.Key, item.Value));
                }
                connection = new SqlConnection(this.ConnectionString);
                connection.Open();
                command.Connection = connection;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
            catch (SqlException e)
            {
                connection.Close();
                //LogHelper.Write(e.Message + "\r\nSQL：" + command.CommandText);
                throw new Exception(e.Message);
            }
        }

        public DataTable ExecQuery(string commandTest)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConnectionString); //Sql链接类的实例化
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(commandTest, connection);
                DataSet ds = new DataSet();
                da.Fill(ds);
                connection.Close();

                return ds.Tables[0];
            }
            catch (SqlException e)
            {
                connection.Close();
                //LogHelper.Write(e.Message);
                throw e;
            }
        }
        #endregion
    }
}
