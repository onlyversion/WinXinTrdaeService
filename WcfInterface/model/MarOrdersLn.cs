//*******************************************************************************
//  文 件 名：MarOrdersLn.cs
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

namespace WcfInterface.model
{
    /// <summary>
    /// 市价单下单接口 输入类型
    /// </summary>
    public class MarOrdersLn
    {
        //public MarOrdersLn()
        //{
        //    Experiences = new List<UserExperience>();
        //}

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
        /// Gets or sets 商品编码
        /// </summary>
        public string ProductCode
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
        /// Gets or sets 保证金百分比
        /// </summary>
        public double OrderMoney
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
        /// Gets or sets 止盈价
        /// </summary>
        public double ProfitPrice
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
        /// Gets or sets 客户端实时价格
        /// </summary>
        public double RtimePrices
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 客户端实时报价时间
        /// </summary>
        public DateTime CurrentTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 允许成交的最大偏差点数
        /// </summary>
        public double MaxPrice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Mac地址
        /// </summary>
        public string Mac
        {
            get;
            set;
        }

     
    }

    /// <summary>
    /// 用户券领取表
    /// </summary>
    public class  UserExperience
    {
        /// <summary>
        /// 体验券ID
        /// </summary>
        public int ID{get;set;}
        /// <summary>
        /// 体验券剩余数量
        /// </summary>
        public int Num{get;set;}
    }
}
