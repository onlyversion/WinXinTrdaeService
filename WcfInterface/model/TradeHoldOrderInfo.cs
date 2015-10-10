//*******************************************************************************
//  文 件 名：TradeHoldOrderInfo.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/08/12
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
    /// 挂单信息
    /// </summary>
    public class TradeHoldOrderInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether 
        /// 结果(1成功 0失败)
        /// </summary>
        public bool Result
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 描述
        /// </summary>
        public string Desc
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
        /// Gets or sets 冻结资金
        /// </summary>
        public double FrozenMoney
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 订单表
        /// </summary>
        public List<TradeHoldOrder> TdHoldOrderList
        {
            get;
            set;
        }
    }
}
