using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    public class FundChange
    {
        /// <summary>
        /// Gets or sets 组织名称
        /// </summary>
        public string OrgName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 客户账户
        /// </summary>
        public string Account
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 客户名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 组织编码
        /// </summary>
        public string Telephone
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 
        /// 4 入金
        /// 5 出金
        /// </summary>
        public string Reason
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 金额
        /// </summary>
        public double Amt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 时间
        /// </summary>
        public DateTime OpTime
        {
            get;
            set;
        }

        
    }
}
