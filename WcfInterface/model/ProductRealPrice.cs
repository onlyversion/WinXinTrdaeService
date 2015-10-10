//*******************************************************************************
//  文 件 名：ITrade.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/08/26
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
    /// 各种行情实时价格
    /// </summary>
    public class ProductRealPrice
    {
        /// <summary>
        /// Gets or sets 黄金实时价
        /// </summary>
        public double au_realprice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 白银实时价
        /// </summary>
        public double ag_realprice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 铂金实时价
        /// </summary>
        public double pt_realprice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 钯金实时价
        /// </summary>
        public double pd_realprice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 在实时价的基础上下浮后的 黄金卖价
        /// </summary>
        public double aub_price
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 在实时价的基础上下浮后的 白银卖价
        /// </summary>
        public double agb_price
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 在实时价的基础上下浮后的 铂金卖价
        /// </summary>
        public double ptb_price
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 在实时价的基础上下浮后的 钯金卖价
        /// </summary>
        public double pdb_price
        {
            get;
            set;
        }

    }
}
