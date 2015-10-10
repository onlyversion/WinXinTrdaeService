﻿//*******************************************************************************
//  文 件 名：Orders.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/01
//
//  修改标识：
//  修改说明：
//*******************************************************************************

namespace WcfInterface.model
{
    /// <summary>
    /// 挂单下单返回类型
    /// </summary>
    public class Orders
    {
        /// <summary>
        /// Gets or sets a value indicating whether
        /// 结果(true成功 false失败)
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
        /// Gets or sets 返回代码
        /// </summary>
        public string ReturnCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 挂单信息
        /// </summary>
        public TradeHoldOrder TradeHoldOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 库存
        /// </summary>
        public MoneyInventory MoneyInventory
        {
            get;
            set;
        }
    }
}
