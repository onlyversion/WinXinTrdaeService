using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace JinTong.Jyrj.Common
{
    public class DbTransactionWrapper : IDisposable
    {
        public DbTransaction DbTransaction { get; private set; }
        public DbTransactionWrapper(DbTransaction transaction)
        {
            this.DbTransaction = transaction;
        }

        public bool IsRollBack { get; set; }
        public void Rollback()
        {
            if (!this.IsRollBack)
            {
                this.DbTransaction.Rollback();
            }
        }
        public void Commit()
        {
            this.DbTransaction.Commit();
        }
        public void Dispose()
        {
            this.DbTransaction.Dispose();
        }
    }
}
