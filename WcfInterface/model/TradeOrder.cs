//*******************************************************************************
//  文 件 名：TradeOrder.cs
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
    /// 市价单查询
    /// </summary>
    public class TradeOrder
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
        /// Gets or sets 下单方式
        /// </summary>
        public string OperType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 订单ID
        /// </summary>
        public string OrderId
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
        /// 行情编码
        /// </summary>
        public string PriceCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 下单类型
        /// </summary>
        public string OrderType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 用户的下单单位数
        /// </summary>
        public double Orderunit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 商品总数量
        /// </summary>
        public double Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 商品剩余数量
        /// </summary>
        public double UseQuantity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 下单价格
        /// </summary>
        public double OrderPrice
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
        /// Gets or sets 保证金
        /// </summary>
        public double OccMoney
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 冻结资金
        /// </summary>
        public double DongJieMoney
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 基础工费
        /// </summary>
        public double TradeFee
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 仓储费
        /// </summary>
        public double StorageFee
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 下单时间
        /// </summary>
        public DateTime OrderTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether是否允许入库
        /// </summary>
        public bool AllowStore
        {
            get;
            set;
        }

        /// <summary>
        /// 是否体验券下单
        /// </summary>
        public bool IsExperience
        {
            get;
            set;
        }
        

        /// <summary>
        /// 总重量
        /// </summary>
        public double TotalWeight
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
        /// Gets or sets 账户余额
        /// </summary>
        public double Money
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 用户所有的保证金
        /// </summary>
        public double AllOccMoney
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 用户所有的预付款
        /// </summary>
        public double FrozenMoney
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 盈亏
        /// </summary>
        public double ProfitValue
        {
            set;
            get;
        }
    }
}
