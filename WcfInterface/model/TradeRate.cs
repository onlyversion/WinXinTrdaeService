//*******************************************************************************
//  文 件 名：TradeRate.cs
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
    /// 汇率和水
    /// </summary>
    public class TradeRate
    {
        /// <summary>
        /// Gets or sets 行情编码
        /// </summary>
        public string PriceCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 汇率
        /// </summary>
        public double Rate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 水
        /// </summary>
        public double Water
        {
            get;
            set;
        }

    }
}
