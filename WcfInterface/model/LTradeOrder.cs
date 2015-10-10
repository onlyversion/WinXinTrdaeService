//*******************************************************************************
//  文 件 名：LTradeOrder.cs
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
    /// 市价单历史查询 返回类型
    /// </summary>
    public class LTradeOrder
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
        /// 客户名称
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 组织编码
        /// </summary>
        public string Telephone
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
        /// Gets or sets 货款
        /// </summary>
        public double ProductMoney
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
        /// Gets or sets 历史单ID
        /// </summary>
        public string HistoryOrderId
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
        /// Gets or sets 订单价格
        /// </summary>
        public double OrderPrice
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 平仓方式
        /// </summary>
        public string OverType
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
        /// Gets or sets 平仓价
        /// </summary>
        public double OverPrice
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 盈亏
        /// </summary>
        public double ProfitValue
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 基础工费
        /// </summary>
        public double TradeFee
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 仓储费
        /// </summary>
        public double StorageFee
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 平仓时间
        /// </summary>
        public DateTime OverTime
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 订单时间
        /// </summary>
        public DateTime OrderTime
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 关联订单ID
        /// </summary>
        public string OrderId
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 止盈价
        /// </summary>
        public double ProfitPrice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 止损价
        /// </summary>
        public double LossPrice
        {
            get;
            set;
        }

    }
}
