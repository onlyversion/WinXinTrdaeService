using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model.WJY
{
    /// <summary>
    /// 返回保证金信息
    /// </summary>
    public  class ReturnCashFundInfo:ResultDesc
    {
        /// <summary>
        /// 可返回的保证金金额
        /// </summary>
        public double Money { get; set; }
    }
}
