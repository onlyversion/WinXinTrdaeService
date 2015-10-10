using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model.WJY
{
    /// <summary>
    /// 微交易用户信息
    /// </summary>
    public class WTradeUserInfo
    {
        /// <summary>
        /// 微交易用户信息
        /// </summary>
        public Base_WUser WUser { get; set; }
        /// <summary>
        /// 交易账户信息
        /// </summary>
        public TradeUser TdUser { get; set; }

        /// <summary>
        /// 交易总手数
        /// </summary>
        public int Count { get; set; }

        public WTradeUserInfo()
        {
            WUser = new Base_WUser();
            TdUser = new TradeUser();
        }
    }
}
