//*******************************************************************************
//  文 件 名：Hedging.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/01
//
//  修改标识：
//  修改说明：
//*******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 对冲信息
    /// </summary>
    public class Hedging
    {

        /// <summary>
        /// Gets or sets 商品名称
        /// </summary>
        public string ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 订单类型("0"买 "1"卖)
        /// </summary>
        public string OrderType
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
        /// Gets or sets 平均价
        /// </summary>
        public double AvgOrderPrice
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets 实时价
        /// </summary>
        public double RealPrice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 盈亏
        /// </summary>
        public double ProfitLoss
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
        /// Gets or sets  对冲数量
        /// </summary>
        public double HedgingQuantity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets  对冲盈亏
        /// </summary>
        public double HedgingProfitLoss
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets  对冲工费
        /// </summary>
        public double HedgingTradeFee
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets  对冲仓储费
        /// </summary>
        public double HedgingStorageFee
        {
            get;
            set;
        }

    }
}
