using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model.WJY
{
    /// <summary>
    /// 经纪人支付保证金
    /// 返回验证码
    /// </summary>
    public class PayCashFundInfo:ResultDesc
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string IdentifyingCode { set; get; }
    }
}
