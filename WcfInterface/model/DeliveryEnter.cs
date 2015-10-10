﻿//*******************************************************************************
//  文 件 名：DeliveryEnter.cs
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
    /// 市价单入库输入类型
    /// </summary>
    public class DeliveryEnter
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
        /// Gets or sets 订单ID
        /// </summary>
        public string Orderid
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
        /// Gets or sets 允许成交的最大点偏差
        /// </summary>
        public double MaxPrice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 客户端实时价格
        /// </summary>
        public double RtimePrices
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 客户端实时报价的时间
        /// </summary>
        public DateTime CurrentTime
        {
            get;
            set;
        }
    }

}
