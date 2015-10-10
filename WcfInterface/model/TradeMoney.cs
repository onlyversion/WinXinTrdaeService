using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 出入金和解约记录
    /// </summary>
    public class TradeMoney
    {
        /// <summary>
        /// 帐号
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string opertime { get; set; }
        /// <summary>
        /// 原有金额
        /// </summary>
        public string oldvalue { get; set; }
        /// <summary>
        /// 现有金额
        /// </summary>
        public string newvalue { get; set; }
        /// <summary>
        /// 变动金额
        /// </summary>
        public string changevalue { get; set; }
        /// <summary>
        /// 操作:入金,出金,解约
        /// </summary>
        public string reason { get; set; }
        /// <summary>
        /// 状态:已申请 已付款 已审核 已拒绝
        /// </summary>
        public string state { get; set; }
    }
}
