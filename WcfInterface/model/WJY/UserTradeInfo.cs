using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model.WJY
{
    /// <summary>
    /// 微交易用户信息
    /// </summary>
    public class UserTradeInfo:ResultDesc
    {
        /// <summary>
        /// 微交易用户信息
        /// </summary>
        public List<WTradeUserInfo> Users { get; set; }
      
        public UserTradeInfo()
        {
            Users = new List<WTradeUserInfo>();
        }
    }
}
