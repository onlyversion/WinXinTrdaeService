using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 订单汇总
    /// </summary>
   public class OrderTypeTotal:EntityBase
    {
       /// <summary>
        /// 已预约提货单
       /// </summary>
       public int BidOrder { get; set; }
       /// <summary>
       /// 已成功提货
       /// </summary>
       public int BidSuccess { get; set; }
       /// <summary>
       /// 已预约卖单
       /// </summary>
       public int AskOrder { get; set; }
       /// <summary>
       /// 已成功卖
       /// </summary>
       public int AskSuccess { get; set; }
    }
}
