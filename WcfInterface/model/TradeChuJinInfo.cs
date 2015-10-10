using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    public class TradeChuJinInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether 
        /// 结果(1成功 0失败)
        /// </summary>
        public bool Result
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 描述
        /// </summary>
        public string Desc
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 已申请金额
        /// </summary>
        public double Amt
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 已付款金额
        /// </summary>
        public double Amt2
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 已拒绝金额
        /// </summary>
        public double Amt3
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 订单表
        /// </summary>
        public List<TradeChuJin> TdChuJinList
        {
            get;
            set;
        }
    }
}
