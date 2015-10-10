//*******************************************************************************
//  文 件 名：LTradeHoldOrder.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/01
//
//  修改标识：
//  修改说明：
//*******************************************************************************
using System;

namespace WcfInterface.model
{
    /// <summary>
    /// 挂单历史查询 返回类型
    /// </summary>
    public class LTradeHoldOrder
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
        /// Gets or sets 客户账号
        /// </summary>
        public string TradeAccount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 数量
        /// </summary>
        public double Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 挂单ID
        /// </summary>
        public string HoldOrderID
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 商品名称
        /// </summary>
        public string ProductName
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 商品编码
        /// </summary>
        public string ProductCode
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 订单类型
        /// </summary>
        public string OrderType
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 挂单价格
        /// </summary>
        public double HoldPrice
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 止损价
        /// </summary>
        public double LossPrice
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 止盈价
        /// </summary>
        public double ProfitPrice
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 冻结资金
        /// </summary>
        public double FrozenMoney
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 有效期
        /// </summary>
        public DateTime ValidTime
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 下单时间
        /// </summary>
        public DateTime OrderTime
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 成交时间
        /// </summary>
        public DateTime TradeTime
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 状态
        /// </summary>
        public string State
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 成交订单ID
        /// </summary>
        public string OrderID
        {
            set;
            get;
        }

    }
}
