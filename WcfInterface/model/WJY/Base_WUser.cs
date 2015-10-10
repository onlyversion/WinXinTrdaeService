using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model.WJY
{
    /// <summary>
    /// 微交易用户信息
    /// </summary>
    public class Base_WUser
    {
        /// <summary>
        /// ID
        /// </summary>
        public string WUserId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNum { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WAccount { get; set; }

        /// <summary>
        /// 上线ID
        /// </summary>
        public string PWUserId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 账户类型
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// 二维码Ticket
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>
        /// 二维码图片Url
        /// </summary>
        public string Url { get; set; }
    }
}
