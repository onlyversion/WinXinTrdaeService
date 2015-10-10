//*******************************************************************************
//  文 件 名：TradeOrderInfo.cs
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
    /// 订单信息
    /// </summary>
    public class TradeOrderInfo
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
        /// Gets or sets 占用资金
        /// </summary>
        public double OccMoney
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets 基础工费
        /// </summary>
        public double Tradefee
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets 仓储费
        /// </summary>
        public double Storagefee
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 订单表
        /// </summary>
        public List<TradeOrder> TdOrderList
        {
            get;
            set;
        }
    }
}
