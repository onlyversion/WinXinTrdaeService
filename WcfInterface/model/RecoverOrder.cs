//*******************************************************************************
//  文 件 名：RecoverOrder.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/11/23
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
    public class RecoverOrder
    {
       //客户账号、姓名、单号、商品类型、卖价、卖重量、卖款、卖时间、付款时间、状态（待受理，已受理）

        /// <summary>
        /// Gets or sets  单号
        /// </summary>
        public string OrderId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets  卖款
        /// </summary>
        public double PayMoney
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets  用户账户
        /// </summary>
        public string TradeAccount
        {
            get;
            set;
        }
        /// <summary>
        ///  Gets or sets  用户姓名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets  商品名称
        /// </summary>
        public string ProductName
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets  卖价
        /// </summary>
        public double OverPrice
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets  卖重量
        /// </summary>
        public double RealWeight
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets  卖时间
        /// </summary>
        public DateTime Overtime
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets  付款时间
        /// </summary>
        public DateTime? PayTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets  状态 0待受理 1已受理
        /// </summary>
        public string State
        {
            get;
            set;
        }

        /// <summary>
        /// 操作员
        /// </summary>
        public string DoPerson
        {
            get;
            set;
        }
    }
}
