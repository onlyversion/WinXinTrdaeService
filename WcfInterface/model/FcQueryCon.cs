using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    public class FcQueryCon
    {
        /// <summary>
        /// Gets or sets 登陆标识
        /// </summary>
        public string LoginID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 交易账号
        /// </summary>
        public string Account
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 类型("4"-入金 "5"-出金)
        /// </summary>
        public string Reason
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 组织名称
        /// </summary>
        public string OrgName
        {
            get;
            set;
        } 
    }
}
