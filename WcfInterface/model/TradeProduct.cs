//*******************************************************************************
//  文 件 名：TradeProduct.cs
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
    /// 商品信息
    /// </summary>
    public class TradeProduct
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
        /// Gets or sets 商品编码
        /// </summary>
        public string Productcode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 原料编码
        /// </summary>
        public string Goodscode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 行情编码
        /// </summary>
        public string Pricecode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 点差基值
        /// </summary>
        public double Adjustbase
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 点差位数
        /// </summary>
        public int Adjustcount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 点差
        /// </summary>
        public int Pricedot
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 点值
        /// </summary>
        public double Valuedot
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 止损止盈点差
        /// </summary>
        public int SetBase
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 挂单价点差
        /// </summary>
        public int Holdbase
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 交易工费公式
        /// </summary>
        public string Expressionfee
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 卖手续费公式
        /// </summary>
        public string Sellfee
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 允许最高价格
        /// </summary>
        public double Maxprice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 允许最低价格
        /// </summary>
        public double Minprice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 允许交易的最大时间差
        /// </summary>
        public int Maxtime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 定金计算公式
        /// </summary>
        public string Ordemoneyfee
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 定金百分比
        /// </summary>
        public double Ordemoney
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 订购仓储费公式
        /// </summary>
        public string Buystoragefee
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 卖仓储费公式
        /// </summary>
        public string Sellstoragefee
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 状态(0 正常交易 1 只报价 2 只买 3 只卖 4 全部禁止)
        /// </summary>
        public string State
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 单位数
        /// </summary>
        public double Unit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 下浮价格
        /// </summary>
        public double Lowerprice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开始时间,时间存储格式为: HH:mm:ss
        /// </summary>
        public string Starttime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 结束时间,时间存储格式为: HH:mm:ss
        /// </summary>
        public string Endtime
        {
            get;
            set;
        }

        /// <summary>
        /// 精确到秒的时间数字串
        /// </summary>
        public string weektime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 商品实时价格
        /// </summary>
        public double realprice
        {
            get;
            set;
        }
    }
}
