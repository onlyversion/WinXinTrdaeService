using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.individual.helper
{
    /// <summary>
    /// 数据库通用操作帮助类
    /// </summary>
    public class DbHelper
    {
        protected static string connectionString =
               EncryptionHelper.Decrypt(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        protected static DbProviderFactory provider =
                 DbProviderFactories.GetFactory(
                 ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName);

        public static string GetConnectionString()
        {
            return connectionString;
        }

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand cmd = provider.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = SQLString;
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (DbException E)
                    {
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                        cmd.Dispose();
                    }
                    
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>        
        public static void ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                using (DbCommand cmd = provider.CreateCommand())
                {
                    cmd.Connection = conn;
                    using (DbTransaction tx = conn.BeginTransaction())
                    {
                        cmd.Transaction = tx;
                        try
                        {
                            for (int n = 0; n < SQLStringList.Count; n++)
                            {
                                string strsql = SQLStringList[n].ToString();
                                if (strsql.Trim().Length > 1)
                                {
                                    cmd.CommandText = strsql;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            tx.Commit();
                        }
                        catch (DbException ex)
                        {
                            tx.Rollback();
                            throw ex;
                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand cmd = provider.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = SQLString;
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (DbException e)
                    {

                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DbDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>DbDataReader</returns>
        public static DbDataReader ExecuteReader(string strSQL)
        {
            DbConnection connection = provider.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = provider.CreateCommand();
            cmd.Connection = connection;
            cmd.CommandText = strSQL;
            try
            {
                connection.Open();
                DbDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (System.Data.Common.DbException e)
            {
                connection.Close();
                connection.Dispose();
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string SQLString)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand cmd = provider.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = SQLString;
                    try
                    {
                        DataSet ds = new DataSet();
                        DbDataAdapter adapter = provider.CreateDataAdapter();
                        adapter.SelectCommand = cmd;
                        adapter.Fill(ds, "ds");
                        return ds;
                    }
                    catch (DbException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }
        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, DbParameter[] cmdParms)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand cmd = provider.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = SQLString;
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        //cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (DbException E)
                    {
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表
        ///（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    using (DbCommand cmd = provider.CreateCommand())
                    {
                        try
                        {
                            //循环
                            foreach (DictionaryEntry myDE in SQLStringList)
                            {
                                string cmdText = myDE.Key.ToString();
                                DbParameter[] cmdParms = (DbParameter[])myDE.Value;
                                PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                                int val = cmd.ExecuteNonQuery();
                            }
                            trans.Commit();
                        }
                        catch (DbException ex)
                        {
                            trans.Rollback();

                            throw ex;
                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）,返回首行首列的值;
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, DbParameter[] cmdParms)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand cmd = provider.CreateCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        //cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) ||
                           (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (DbException e)
                    {

                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DbDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>DbDataReader</returns>
        public static DbDataReader ExecuteReader(string SQLString,
            DbParameter[] cmdParms)
        {
            DbConnection connection = provider.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = provider.CreateCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                DbDataReader myReader =
                     cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //cmd.Parameters.Clear();
                return myReader;
            }
            catch (DbException e)
            {
                connection.Close();
                connection.Dispose();
                throw new Exception(e.Message);
            }


        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string SQLString, DbParameter[] cmdParms)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand cmd = provider.CreateCommand())
                {
                    using (DbDataAdapter da = provider.CreateDataAdapter())
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        da.SelectCommand = cmd;
                        DataSet ds = new DataSet();
                        try
                        {
                            da.Fill(ds, "ds");
                            //cmd.Parameters.Clear();
                            return ds;
                        }
                        catch (DbException ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        finally
                        {
                            connection.Close();
                            connection.Dispose();
                        }
                    }
                }
            }
        }

        private static void PrepareCommand(DbCommand cmd, DbConnection conn,
             DbTransaction trans, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (DbParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }

        #endregion

        #region 存储过程操作
        /// <summary>
        /// 执行存储过程;
        /// </summary>
        /// <param name="storeProcName">存储过程名</param>
        /// <param name="parameters">所需要的参数</param>
        /// <returns>返回受影响的行数</returns>
        public static int RunProcedureExecuteSql(string storeProcName,
              DbParameter[] parameters)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                DbCommand cmd =
                    BuildQueryCommand(connection, storeProcName, parameters);
                int rows = cmd.ExecuteNonQuery();
                //cmd.Parameters.Clear();
                connection.Close();
                connection.Dispose();
                return rows;
            }
        }

        /// <summary>
        /// 执行存储过程,返回首行首列的值
        /// </summary>
        /// <param name="storeProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>返回首行首列的值</returns>
        public static Object RunProcedureGetSingle(string storeProcName,
            DbParameter[] parameters)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                try
                {
                    DbCommand cmd =
                         BuildQueryCommand(connection, storeProcName, parameters);
                    object obj = cmd.ExecuteScalar();
                    //cmd.Parameters.Clear();
                    if ((Object.Equals(obj, null)) ||
                         (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (DbException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>DbDataReader</returns>
        public static DbDataReader RunProcedureGetDataReader(string storedProcName,
            DbParameter[] parameters)
        {
            DbConnection connection = provider.CreateConnection();
            connection.ConnectionString = connectionString;
            DbDataReader returnReader;
            DbCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
            cmd.CommandType = CommandType.StoredProcedure;
            returnReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return returnReader;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedureGetDataSet(string storedProcName,
             DbParameter[] parameters)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                DataSet dataSet = new DataSet();
                DbDataAdapter sqlDA = provider.CreateDataAdapter();
                sqlDA.SelectCommand =
                     BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet);
                //sqlDA.SelectCommand.Parameters.Clear();
                sqlDA.Dispose();
                connection.Close();
                connection.Dispose();
                return dataSet;
            }
        }

        /// <summary>
        /// 执行多个存储过程，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">存储过程的哈希表
        ///（value为存储过程语句，key是该语句的DbParameter[]）</param>
        public static bool RunProcedureTran(Hashtable SQLStringList)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                using (DbTransaction trans = connection.BeginTransaction())
                {
                    using (DbCommand cmd = provider.CreateCommand())
                    {
                        try
                        {
                            //循环
                            foreach (DictionaryEntry myDE in SQLStringList)
                            {
                                cmd.Connection = connection;
                                string storeName = myDE.Value.ToString();
                                DbParameter[] cmdParms = (DbParameter[])myDE.Key;

                                cmd.Transaction = trans;
                                cmd.CommandText = storeName;
                                cmd.CommandType = CommandType.StoredProcedure;
                                if (cmdParms != null)
                                {
                                    foreach (DbParameter parameter in cmdParms)
                                    {
                                        cmd.Parameters.Add(parameter);
                                    }
                                }
                                int val = cmd.ExecuteNonQuery();
                                //cmd.Parameters.Clear();
                            }
                            trans.Commit();
                            return true;
                        }
                        catch
                        {
                            trans.Rollback();

                            return false;
                        }
                        finally
                        {
                            connection.Close();
                            connection.Dispose();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 构建 DbCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>DbCommand</returns>
        private static DbCommand BuildQueryCommand(DbConnection connection,
             string storedProcName, DbParameter[] parameters)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            DbCommand command = provider.CreateCommand();
            command.CommandText = storedProcName;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (DbParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }
        #endregion

        /// <summary>
        /// 初始化 com.individual.helper.ComDbParameter 类的新实例
        /// </summary>
        /// <param name="DataBaseType">数据库类型</param>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DbType">参数数据类型</param>
        /// <param name="Value">参数值</param>
        /// <param name="Direction">该值指示参数是只可输入、只可输出、双向还是存储过程返回值参数,System.Data.ParameterDirection 值之一</param>
        public static DbParameter CreateDbParameter(DataBaseType DataBaseType, string ParameterName, DbParameterType DbType, object Value, ParameterDirection Direction)
        {
            if (DataBaseType == helper.DataBaseType.Access)
            {
                System.Data.OleDb.OleDbParameter param = new System.Data.OleDb.OleDbParameter(ParameterName, GetOleDbType(DbType));
                param.Direction = Direction;
                param.Value = Value;
                param.Size = 8000;
                return param;
            }
            else if (DataBaseType == helper.DataBaseType.MySql)
            {
                MySql.Data.MySqlClient.MySqlParameter param = new MySql.Data.MySqlClient.MySqlParameter(ParameterName, GetMySqlDbType(DbType));
                param.Direction = Direction;
                param.Value = Value;
                param.Size = 8000;
                return param;
            }

            else if (DataBaseType == helper.DataBaseType.Oracle)
            {
                System.Data.OracleClient.OracleParameter param = new System.Data.OracleClient.OracleParameter(ParameterName, GetOracleType(DbType));
                param.Direction = Direction;
                param.Value = Value;
                param.Size = 8000;
                return param;
            }
            else
            {
                System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter(ParameterName, GetSqlDbType(DbType));

                param.Direction = Direction;
                param.Value = Value;
                param.Size = 8000;
                return param;
            }

        }

        private static System.Data.OleDb.OleDbType GetOleDbType(DbParameterType DbType)
        {

            if (DbType == DbParameterType.DateTime)
            {
                return System.Data.OleDb.OleDbType.DBTimeStamp;
            }
            else if (DbType == DbParameterType.Float)
            {
                return System.Data.OleDb.OleDbType.Double;
            }
            else if (DbType == DbParameterType.Int)
            {
                return System.Data.OleDb.OleDbType.Integer;
            }
            else
            {
                return System.Data.OleDb.OleDbType.VarChar;
            }
        }

        private static System.Data.OracleClient.OracleType GetOracleType(DbParameterType DbType)
        {
            if (DbType == DbParameterType.DateTime)
            {
                return System.Data.OracleClient.OracleType.DateTime;
            }
            else if (DbType == DbParameterType.Float)
            {
                return System.Data.OracleClient.OracleType.Float;
            }
            else if (DbType == DbParameterType.Int)
            {
                return System.Data.OracleClient.OracleType.Int32;
            }
            else
            {
                return System.Data.OracleClient.OracleType.VarChar;
            }
        }

        private static System.Data.SqlDbType GetSqlDbType(DbParameterType DbType)
        {
            if (DbType == DbParameterType.DateTime)
            {
                return System.Data.SqlDbType.DateTime;
            }
            else if (DbType == DbParameterType.Float)
            {
                return System.Data.SqlDbType.Float;
            }
            else if (DbType == DbParameterType.Int)
            {
                return System.Data.SqlDbType.Int;
            }
            else
            {
                return System.Data.SqlDbType.VarChar;
            }

        }

        private static MySql.Data.MySqlClient.MySqlDbType GetMySqlDbType(DbParameterType DbType)
        {
            if (DbType == DbParameterType.DateTime)
            {
                return MySql.Data.MySqlClient.MySqlDbType.DateTime;
            }
            else if (DbType == DbParameterType.Float)
            {
                return MySql.Data.MySqlClient.MySqlDbType.Float;
            }
            else if (DbType == DbParameterType.Int)
            {
                return MySql.Data.MySqlClient.MySqlDbType.Int32;
            }
            else
            {
                return MySql.Data.MySqlClient.MySqlDbType.String;
            }

        }
    }

    public class DbHelper2
    {
        protected static string connectionString =
                EncryptionHelper.Decrypt(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);

        protected static DbProviderFactory provider =
                 DbProviderFactories.GetFactory(
                 ConfigurationManager.ConnectionStrings["ConnectionString2"].ProviderName);

        public static string GetConnectionString()
        {
            return connectionString;
        }

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand cmd = provider.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = SQLString;
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (DbException E)
                    {

                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>        
        public static void ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                using (DbCommand cmd = provider.CreateCommand())
                {
                    cmd.Connection = conn;
                    using (DbTransaction tx = conn.BeginTransaction())
                    {
                        cmd.Transaction = tx;
                        try
                        {
                            for (int n = 0; n < SQLStringList.Count; n++)
                            {
                                string strsql = SQLStringList[n].ToString();
                                if (strsql.Trim().Length > 1)
                                {
                                    cmd.CommandText = strsql;
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            tx.Commit();
                        }
                        catch (DbException ex)
                        {
                            tx.Rollback();

                            throw ex;
                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand cmd = provider.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = SQLString;
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) ||
                            (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (DbException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DbDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>DbDataReader</returns>
        public static DbDataReader ExecuteReader(string strSQL)
        {
            DbConnection connection = provider.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = provider.CreateCommand();
            cmd.Connection = connection;
            cmd.CommandText = strSQL;
            try
            {
                connection.Open();
                DbDataReader myReader =
                    cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (System.Data.Common.DbException e)
            {
                connection.Close();
                connection.Dispose();
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string SQLString)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand cmd = provider.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = SQLString;
                    try
                    {
                        DataSet ds = new DataSet();
                        DbDataAdapter adapter = provider.CreateDataAdapter();
                        adapter.SelectCommand = cmd;
                        adapter.Fill(ds, "ds");
                        return ds;
                    }
                    catch (DbException ex)
                    {

                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }
        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, DbParameter[] cmdParms)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand cmd = provider.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = SQLString;
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        //cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (DbException E)
                    {
                        connection.Close();
                        connection.Dispose();
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表
        ///（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                using (DbTransaction trans = conn.BeginTransaction())
                {
                    using (DbCommand cmd = provider.CreateCommand())
                    {
                        try
                        {
                            //循环
                            foreach (DictionaryEntry myDE in SQLStringList)
                            {
                                string cmdText = myDE.Key.ToString();
                                DbParameter[] cmdParms = (DbParameter[])myDE.Value;
                                PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                                int val = cmd.ExecuteNonQuery();
                                //cmd.Parameters.Clear();
                            }
                            trans.Commit();
                        }
                        catch (DbException ex)
                        {
                            trans.Rollback();

                            throw ex;
                        }
                        finally
                        {
                            conn.Close();
                            conn.Dispose();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）,返回首行首列的值;
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, DbParameter[] cmdParms)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand cmd = provider.CreateCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        //cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) ||
                           (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (DbException e)
                    {
                        connection.Close();
                        connection.Dispose();
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DbDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>DbDataReader</returns>
        public static DbDataReader ExecuteReader(string SQLString,
            DbParameter[] cmdParms)
        {
            DbConnection connection = provider.CreateConnection();
            connection.ConnectionString = connectionString;
            DbCommand cmd = provider.CreateCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                DbDataReader myReader =
                     cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //cmd.Parameters.Clear();
                return myReader;
            }
            catch (DbException e)
            {
                connection.Close();
                connection.Dispose();
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSet(string SQLString,
            DbParameter[] cmdParms)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                using (DbCommand cmd = provider.CreateCommand())
                {
                    using (DbDataAdapter da = provider.CreateDataAdapter())
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        da.SelectCommand = cmd;
                        DataSet ds = new DataSet();
                        try
                        {
                            da.Fill(ds, "ds");
                            //cmd.Parameters.Clear();
                            return ds;
                        }
                        catch (DbException ex)
                        {
                            connection.Close();
                            connection.Dispose();
                            throw new Exception(ex.Message);
                        }
                        finally
                        {
                            connection.Close();
                            connection.Dispose();
                        }
                    }
                }
            }
        }

        private static void PrepareCommand(DbCommand cmd, DbConnection conn,
             DbTransaction trans, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (DbParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }

        #endregion

        #region 存储过程操作
        /// <summary>
        /// 执行存储过程;
        /// </summary>
        /// <param name="storeProcName">存储过程名</param>
        /// <param name="parameters">所需要的参数</param>
        /// <returns>返回受影响的行数</returns>
        public static int RunProcedureExecuteSql(string storeProcName,
              DbParameter[] parameters)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                DbCommand cmd =
                    BuildQueryCommand(connection, storeProcName, parameters);
                int rows = cmd.ExecuteNonQuery();
                //cmd.Parameters.Clear();
                connection.Close();
                connection.Dispose();
                return rows;
            }
        }

        /// <summary>
        /// 执行存储过程,返回首行首列的值
        /// </summary>
        /// <param name="storeProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>返回首行首列的值</returns>
        public static Object RunProcedureGetSingle(string storeProcName,
            DbParameter[] parameters)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                try
                {
                    DbCommand cmd =
                         BuildQueryCommand(connection, storeProcName, parameters);
                    object obj = cmd.ExecuteScalar();
                    //cmd.Parameters.Clear();
                    if ((Object.Equals(obj, null)) ||
                         (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (DbException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>DbDataReader</returns>
        public static DbDataReader RunProcedureGetDataReader(string storedProcName,
            DbParameter[] parameters)
        {
            DbConnection connection = provider.CreateConnection();
            connection.ConnectionString = connectionString;
            DbDataReader returnReader;
            DbCommand cmd =
                 BuildQueryCommand(connection, storedProcName, parameters);
            cmd.CommandType =
                 CommandType.StoredProcedure;
            returnReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //returnReader = cmd.ExecuteReader();
            //cmd.Parameters.Clear(); // 不要Clear 不然输出参数的值获取不到
            return returnReader;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedureGetDataSet(string storedProcName,
             DbParameter[] parameters)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                DataSet dataSet = new DataSet();
                DbDataAdapter sqlDA = provider.CreateDataAdapter();
                sqlDA.SelectCommand =
                     BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet);
                sqlDA.Dispose();
                connection.Close();
                connection.Dispose();
                return dataSet;
            }
        }

        /// <summary>
        /// 执行多个存储过程，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">存储过程的哈希表
        ///（value为存储过程语句，key是该语句的DbParameter[]）</param>
        public static bool RunProcedureTran(Hashtable SQLStringList)
        {
            using (DbConnection connection = provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                using (DbTransaction trans = connection.BeginTransaction())
                {
                    using (DbCommand cmd = provider.CreateCommand())
                    {
                        try
                        {
                            //循环
                            foreach (DictionaryEntry myDE in SQLStringList)
                            {
                                cmd.Connection = connection;
                                string storeName = myDE.Value.ToString();
                                DbParameter[] cmdParms = (DbParameter[])myDE.Key;

                                cmd.Transaction = trans;
                                cmd.CommandText = storeName;
                                cmd.CommandType = CommandType.StoredProcedure;
                                if (cmdParms != null)
                                {
                                    foreach (DbParameter parameter in cmdParms)
                                    {
                                        cmd.Parameters.Add(parameter);
                                    }
                                }
                                int val = cmd.ExecuteNonQuery();
                                //cmd.Parameters.Clear();
                            }
                            trans.Commit();
                            return true;
                        }
                        catch
                        {
                            trans.Rollback();
                            return false;
                        }
                        finally
                        {
                            connection.Close();
                            connection.Dispose();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 构建 DbCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>DbCommand</returns>
        private static DbCommand BuildQueryCommand(DbConnection connection,
             string storedProcName, DbParameter[] parameters)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            DbCommand command = provider.CreateCommand();
            command.CommandText = storedProcName;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (DbParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }
        #endregion

        /// <summary>
        /// 初始化 com.individual.helper.ComDbParameter 类的新实例
        /// </summary>
        /// <param name="DataBaseType">数据库类型</param>
        /// <param name="ParameterName">参数名称</param>
        /// <param name="DbType">参数数据类型</param>
        /// <param name="Value">参数值</param>
        /// <param name="Direction">该值指示参数是只可输入、只可输出、双向还是存储过程返回值参数,System.Data.ParameterDirection 值之一</param>
        public static DbParameter CreateDbParameter(DataBaseType DataBaseType, string ParameterName, DbParameterType DbType, object Value, ParameterDirection Direction)
        {
            if (DataBaseType == helper.DataBaseType.Access)
            {
                System.Data.OleDb.OleDbParameter param = new System.Data.OleDb.OleDbParameter(ParameterName, GetOleDbType(DbType));
                param.Direction = Direction;
                param.Value = Value;
                param.Size = 8000;
                return param;
            }
            else if (DataBaseType == helper.DataBaseType.MySql)
            {
                MySql.Data.MySqlClient.MySqlParameter param = new MySql.Data.MySqlClient.MySqlParameter(ParameterName, GetMySqlDbType(DbType));
                param.Direction = Direction;
                param.Value = Value;
                param.Size = 8000;
                return param;
            }

            else if (DataBaseType == helper.DataBaseType.Oracle)
            {
                System.Data.OracleClient.OracleParameter param = new System.Data.OracleClient.OracleParameter(ParameterName, GetOracleType(DbType));
                param.Direction = Direction;
                param.Value = Value;
                param.Size = 8000;
                return param;
            }
            else
            {
                System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter(ParameterName, GetSqlDbType(DbType));

                param.Direction = Direction;
                param.Value = Value;
                param.Size = 8000;
                return param;
            }

        }

        private static System.Data.OleDb.OleDbType GetOleDbType(DbParameterType DbType)
        {

            if (DbType == DbParameterType.DateTime)
            {
                return System.Data.OleDb.OleDbType.DBTimeStamp;
            }
            else if (DbType == DbParameterType.Float)
            {
                return System.Data.OleDb.OleDbType.Double;
            }
            else if (DbType == DbParameterType.Int)
            {
                return System.Data.OleDb.OleDbType.Integer;
            }
            else
            {
                return System.Data.OleDb.OleDbType.VarChar;
            }
        }

        private static System.Data.OracleClient.OracleType GetOracleType(DbParameterType DbType)
        {
            if (DbType == DbParameterType.DateTime)
            {
                return System.Data.OracleClient.OracleType.DateTime;
            }
            else if (DbType == DbParameterType.Float)
            {
                return System.Data.OracleClient.OracleType.Float;
            }
            else if (DbType == DbParameterType.Int)
            {
                return System.Data.OracleClient.OracleType.Int32;
            }
            else
            {
                return System.Data.OracleClient.OracleType.VarChar;
            }
        }

        private static System.Data.SqlDbType GetSqlDbType(DbParameterType DbType)
        {
            if (DbType == DbParameterType.DateTime)
            {
                return System.Data.SqlDbType.DateTime;
            }
            else if (DbType == DbParameterType.Float)
            {
                return System.Data.SqlDbType.Float;
            }
            else if (DbType == DbParameterType.Int)
            {
                return System.Data.SqlDbType.Int;
            }
            else
            {
                return System.Data.SqlDbType.VarChar;
            }

        }

        private static MySql.Data.MySqlClient.MySqlDbType GetMySqlDbType(DbParameterType DbType)
        {
            if (DbType == DbParameterType.DateTime)
            {
                return MySql.Data.MySqlClient.MySqlDbType.DateTime;
            }
            else if (DbType == DbParameterType.Float)
            {
                return MySql.Data.MySqlClient.MySqlDbType.Float;
            }
            else if (DbType == DbParameterType.Int)
            {
                return MySql.Data.MySqlClient.MySqlDbType.Int32;
            }
            else
            {
                return MySql.Data.MySqlClient.MySqlDbType.String;
            }

        }
    }

    public enum DbParameterType
    {
        String,
        DateTime,
        Int,
        Float
    }
}

