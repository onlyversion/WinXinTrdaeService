using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JinTong.Jyrj.Common
{
    public class DependentTransaction : Transaction
    {
        public Transaction InnerTransaction { get; private set; }
        internal DependentTransaction(Transaction innerTransaction)
        {
            this.InnerTransaction = innerTransaction;
            this.DbTransactionWrapper = this.InnerTransaction.DbTransactionWrapper;
        }
    }
}
