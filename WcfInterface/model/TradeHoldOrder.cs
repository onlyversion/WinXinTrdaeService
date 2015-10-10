//*******************************************************************************
//  文 件 名：TradeHoldOrder.cs
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
    /// 挂单查询 返回类型
    /// </summary>
    public class TradeHoldOrder
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
        /// Gets or sets 冻结资金
        /// </summary>
        public double FrozenMoney
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 挂单ID
        /// </summary>
        public string HoldOrderID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 商品名称
        /// </summary>
        public string ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 商品编码
        /// </summary>
        public string ProductCode
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
        /// Gets or sets 挂单类型
        /// </summary>
        public string OrderType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 挂单价
        /// </summary>
        public double HoldPrice
        {
            get;
            set;
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

        /// <summary>
        /// Gets or sets 有效期
        /// </summary>
        public DateTime ValidTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 挂单时间
        /// </summary>
        public DateTime OrderTime
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
    }
}
