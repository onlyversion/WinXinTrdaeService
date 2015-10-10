using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Common;
using System.Configuration;
using com.individual.helper;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JinTong.Jyrj.Common;
using System.Data.Odbc;

namespace JinTong.Jyrj.Data
{
    public static class DbHelper
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DbProviderFactory DbProviderFactory
        {
            get { return DbProviderFactories.GetFactory("System.Data.SqlClient"); }
        }
        public static DbParameter BuildDbParameter(string name, object value)
        {
            DbParameter parameter = DbProviderFactory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }

        public static string ConnectionString
        {
            get { return EncryptionHelper.Decrypt(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); }
        }
        #region ExecuteNonQuery

        public static int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(commandText, null);
        }
        public static int ExecuteNonQuery(string commandText, Dictionary<string, object> parameters)
        {
            DbConnection connection = null;
            DbCommand command = DbProviderFactory.CreateCommand();
            try
            {
                command.CommandText = commandText;
                parameters = parameters ?? new Dictionary<string, object>();
                foreach (var item in parameters)
                {
                    command.Parameters.Add(BuildDbParameter(item.Key, item.Value));
                }
                connection = DbProviderFactory.CreateConnection();
                connection.ConnectionString = ConnectionString;
                command.Connection = connection;
                connection.Open();
                int result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                command.Dispose();
            }
        }

        public static DbProviderFactory GetFactory(string cnnStringName)
        {
            return DbProviderFactories.GetFactory("System.Data.SqlClient");
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public static int ExecuteNonQueryProcedure(string procedureName)
        {
            return ExecuteNonQueryProcedure(procedureName, null);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static int ExecuteNonQueryProcedure(string procedureName, Dictionary<string, object> parameters)
        {
            DbConnection connection = null;
            DbCommand command = DbProviderFactory.CreateCommand();
            try
            {
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                parameters = parameters ?? new Dictionary<string, object>();
                foreach (var item in parameters)
                {
                    command.Parameters.Add(BuildDbParameter(item.Key, item.Value));
                }

                connection = DbProviderFactory.CreateConnection();
                connection.ConnectionString = ConnectionString;
                command.Connection = connection;
                connection.Open();
                int result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                command.Dispose();
            }
        }

        #endregion

        #region ExecuteScalar

        public static object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(commandText, null);
        }

        public static object ExecuteScalar(string commandText, IDictionary<string, object> parameters)
        {
            DbConnection connection = null;
            DbCommand command = DbProviderFactory.CreateCommand();
            try
            {
                command.CommandText = commandText;
                parameters = parameters ?? new Dictionary<string, object>();
                foreach (var item in parameters)
                {
                    command.Parameters.Add(BuildDbParameter(item.Key, item.Value));
                }
                connection = DbProviderFactory.CreateConnection();
                connection.ConnectionString = ConnectionString;
                command.Connection = connection;
                connection.Open();
                object result = command.ExecuteScalar();
                return result;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                command.Dispose();
            }
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        public static object ExecuteScalarProcedure(string procedureName)
        {
            return ExecuteScalarProcedure(procedureName, null);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalarProcedure(string procedureName, Dictionary<string, object> parameters)
        {
            DbConnection connection = null;
            DbCommand command = DbProviderFactory.CreateCommand();
            try
            {
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                parameters = parameters ?? new Dictionary<string, object>();
                foreach (var item in parameters)
                {
                    command.Parameters.Add(BuildDbParameter(item.Key, item.Value));
                }
                connection = DbProviderFactory.CreateConnection();
                connection.ConnectionString = ConnectionString;
                command.Connection = connection;
                connection.Open();
                object result = command.ExecuteScalar();
                return result;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                command.Dispose();
            }
        }


        #endregion

        #region ExecuteReader

        public static DbDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(commandText, null);
        }

        public static DbDataReader ExecuteReader(string commandText, IDictionary<string, object> parameters)
        {
            DbConnection connection = null;
            DbCommand command = DbProviderFactory.CreateCommand();
            try
            {
                command.CommandText = commandText;
                parameters = parameters ?? new Dictionary<string, object>();
                foreach (var item in parameters)
                {
                    command.Parameters.Add(BuildDbParameter(item.Key, item.Value));
                }
                connection = DbProviderFactory.CreateConnection();
                connection.ConnectionString = ConnectionString;
                command.Connection = connection;
                connection.Open();
                DbDataReader result = command.ExecuteReader();
                return result;
            }
            catch (SqlException e)
            {
                connection.Close();
                connection.Dispose();
                throw e;
            }
        }
        #endregion

        #region Query

        public static DataSet Query(string commandText)
        {
            return Query(commandText, null);
        }

        public static DataSet Query(string commandText, IDictionary<string, object> parameters)
        {
            DbConnection connection = null;
            DbCommand command = DbProviderFactory.CreateCommand();
            try
            {
                command.CommandText = commandText;
                parameters = parameters ?? new Dictionary<string, object>();
                foreach (var item in parameters)
                {
                    command.Parameters.Add(BuildDbParameter(item.Key, item.Value));
                }
                connection = DbProviderFactory.CreateConnection();
                connection.ConnectionString = ConnectionString;
                command.Connection = connection;
                connection.Open();
                DataAdapter adapter = DbProviderFactory.CreateDataAdapter();
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {

                connection.Close();
                connection.Dispose();
            }
        }
        #endregion

        public static DataTable ExecQuery(string commandTest)
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
                connection.Dispose();
                return ds.Tables[0];
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {

                connection.Close();
                connection.Dispose();
            }
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataReader returnReader;
            connection.Open();
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return returnReader;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <param name="result">判断是否登录标识过期（兄弟必须的@Result）</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                //result = (int)sqlDA.SelectCommand.Parameters["@Result"].Value;
                connection.Close();
                connection.Dispose();
                return dataSet;
            }
        }

        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.SelectCommand.CommandTimeout = Times;
                sqlDA.Fill(dataSet, tableName);
                connection.Close(); connection.Dispose();
                return dataSet;
            }
        }
        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数   
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
                return rowsAffected;
            }
        }
        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值) 
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }

        #region 事务处理

        /// <summary>
        /// 传入sql语句返回影响记录数
        /// </summary>
        /// <param name="sqlCommand">sql查询语句</param>
        /// <param name="transaction">事务对象</param>
        /// <returns>影响记录数</returns>
        public static int ExecuteNonQuery(string sqlCommand, IDataParameter[] parameters, DbTransaction transaction)
        {
            return TExecuteNonQuery(sqlCommand, parameters, transaction);
        }

        public static int TExecuteNonQuery(string sqlCommand, IDataParameter[] parameters, DbTransaction transaction)
        {
            int affectRows = 0;
            DbCommand dbCommand = DbProviderFactory.CreateCommand();
            dbCommand.CommandText = sqlCommand;
            if (parameters != null)
            {
                dbCommand.Parameters.AddRange(parameters);
            }
            GenericDatabase db = new GenericDatabase(ConnectionString, OdbcFactory.Instance);
            //if (parameters != null)
            //{
            //    foreach (SqlParameter parm in parameters)
            //    {
            //        dbCommand.Parameters.Add(parm);
            //    }
            //}
            if (transaction != null)
            {
                affectRows = db.ExecuteNonQuery(dbCommand, transaction);
            }
            else
            {
                affectRows = db.ExecuteNonQuery(dbCommand);
            }
            return affectRows;
        }
        #endregion
    }
}