using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Data.SqlClient;
using com.individual.helper;
namespace JinTong.Jyrj.Common
{
    public class YicelTransaction : IDisposable
    {
        

        /// <summary>
        /// Oracle数据库连接
        /// </summary>
        private DbConnection _connection = null;

        /// <summary>
        /// 事务
        /// </summary>
        private DbTransaction _transaction = null;

        public static string ConnectionString
        {
            get { return EncryptionHelper.Decrypt(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); }
           
        }
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public YicelTransaction()
        {
            //_db = DatabaseFactory.CreateDatabase();
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {
            _connection = new SqlConnection(ConnectionString); 
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            _transaction.Commit();
            _connection.Close();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();
            _connection.Close();
        }

        /// <summary>
        /// 事务属性
        /// </summary>
        public DbTransaction Transaction
        {
            get
            {
                return _transaction;
            }
        }

        #region 实现IDispose接口
        // Track whether Dispose has been called.
        private bool disposed = false;
        /// <summary>
        /// Implement IDisposable.
        /// Do not make this method virtual.
        /// A derived class should not be able to override this method.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }
                _connection.Dispose();
                _transaction.Dispose();
            }
            disposed = true;
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~YicelTransaction()
        {
            Dispose(false);
        }
        #endregion
    }
}
