using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    public class TradeJieYue
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
        /// 申请Id(自增)
        /// </summary>
        public int ApplyId
        {
            get;
            set;
        }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account
        {
            get;
            set;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId
        {
            get;
            set;
        }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankCard
        {
            get;
            set;
        }

        /// <summary>
        /// 持卡人
        /// </summary>
        public string CardName
        {
            get;
            set;
        }

        /// <summary>
        /// 开户行
        /// </summary>
        public string OpenBank
        {
            get;
            set;
        }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime
        {
            get;
            set;
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? SHTime
        {
            get;
            set;
        }

        /// <summary>
        /// 状态 "0"-已申请 "1"-已付款 "2"-已拒绝
        /// </summary>
        public string State
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
    }
}
