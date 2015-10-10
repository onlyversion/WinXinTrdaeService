//*******************************************************************************
//  文 件 名：OrdersLncoming.cs
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
    /// 挂单接口 输入类型
    /// </summary>
    public class OrdersLncoming
    {

        /// <summary>
        /// Gets or sets 交易账户
        /// </summary>
        public string TradeAccount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 登陆标识
        /// </summary>
        public string LoginID
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
        /// Gets or sets 挂单价格
        /// </summary>
        public double HoldPrice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 挂单数量
        /// </summary>
        public double Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 保证金百分比
        /// </summary>
        public double OrderMoney
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
        /// Gets or sets 止损价
        /// </summary>
        public double LossPrice
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
        /// Gets or sets 挂单类型
        /// </summary>
        public string OrderType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 客户端实时价
        /// </summary>
        public double RtimePrices
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 客户端实时价时间
        /// </summary>
        public DateTime CurrentTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Mac地址
        /// </summary>
        public string Mac
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 用户类型(0交易用户 1管理员 2金商)
        /// </summary>
        public int UserType
        {
            get;
            set;
        }
    }
}
