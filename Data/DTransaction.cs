using System;

using Microsoft.Practices.EnterpriseLibrary.Data;

using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using com.individual.helper;

namespace JinTong.Jyrj.Data
{
  
        /// <summary>
    /// 事务操作基类
        /// </summary>
        public class DTransaction : IDisposable
        {
            /// <summary>
            /// 数据库类型
            /// </summary>
            public static DbProviderFactory DbProviderFactory
            {
                get { return DbProviderFactories.GetFactory("System.Data.SqlClient"); }
            }
            public static string ConnectionString
            {
                get { return EncryptionHelper.Decrypt(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString); }
            }
            private DbConnection _conn;
            private DbTransaction _tran;

            public DbTransaction Tran
            {
                get { return _tran; }
                set { _tran = value; }
            }

            /// <summary>
            /// 设置数据库连接
            /// </summary>
            public DTransaction()
            {
                _conn = new SqlConnection(ConnectionString); 
            }
            /// <summary>
            /// 事务开始
            /// </summary>
            public void BeginTransaction()
            {
                _conn.Open();
                _tran = _conn.BeginTransaction();
            }
            /// <summary>
            /// 事务提交
            /// </summary>
            public void Commit()
            {
                _tran.Commit();
                if (_conn.State != System.Data.ConnectionState.Closed)
                    _conn.Close();
            }
            /// <summary>
            /// 事务回滚
            /// </summary>
            public void RollBack()
            {
                _tran.Rollback();
                if (_conn.State != System.Data.ConnectionState.Closed)
                    _conn.Close();
            }

            #region IDisposable Members
            private bool disposed = false;
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            private void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                    }
                    _tran.Dispose();
                }
                disposed = true;
            }
            ~DTransaction()
            {
                Dispose(false);
            }
            #endregion
        }
   
}
