//*******************************************************************************
//  文 件 名：CxQueryCon.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/09/11
//
//  修改标识：
//  修改说明：
//*******************************************************************************
using System;

namespace WcfInterface.model
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public class CxQueryCon
    {
        /// <summary>
        /// Gets or sets 登陆标识
        /// </summary>
        public string LoginID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 交易账号
        /// </summary>
        public string TradeAccount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 商品名称(全部,用all)
        /// </summary>
        public string ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// 行情编码
        /// </summary>
        public string PriceCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 组织名称
        /// </summary>
        public string OrgName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 订单类型（交易类型）
        /// </summary>
        public string OrderType
        {
            get;
            set;
        }
    }
}
