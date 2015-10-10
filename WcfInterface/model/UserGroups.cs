using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 客户分组信息
    /// </summary>
    public class UserGroups
    {
        /// <summary>
        /// 客户组ID
        /// </summary>
        public string UserGroupId { get; set; }
        /// <summary>
        /// 客户组名称
        /// </summary>
        public string UserGroupName { get; set; }
        /// <summary>
        /// 1是默认组,0不是默认组
        /// </summary>
        public int IsDefaultGroup { get; set; }
        /// <summary>
        /// 平仓后多少秒不能下单
        /// </summary>
        public int AfterSecond { get; set; }
        /// <summary>
        /// 下单滑点
        /// </summary>
        public int PlaceOrderSlipPoint { get; set; }
        /// <summary>
        /// 平仓滑点
        /// </summary>
        public int FlatOrderSlipPoint { get; set; }
        /// <summary>
        /// 下单延迟多少秒
        /// </summary>
        public double DelayPlaceOrder { get; set; }
        /// <summary>
        /// 平仓延迟多少秒
        /// </summary>
        public double DelayFlatOrder { get; set; }
    }
}
