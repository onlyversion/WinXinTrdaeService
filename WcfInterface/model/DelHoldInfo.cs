//*******************************************************************************
//  文 件 名：DelHoldInfo.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/03
//
//  修改标识：
//  修改说明：
//*******************************************************************************

using System;

namespace WcfInterface.model
{
    /// <summary>
    /// 挂单取消输入类型
    /// </summary>
    public class DelHoldInfo
    {
        /// <summary>
        /// Gets or sets 用户类型(0交易用户 1管理员 2金商)
        /// </summary>
        public int UserType
        {
            get;
            set;
        }
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
        /// Gets or sets 挂单ID
        /// </summary>
        public string HoldOrderID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 客户端实时价
        /// </summary>
        public double RtimePrices
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 当前客户端实时价时间
        /// </summary>
        public DateTime CurrentTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 挂单取消原因类型("0"自动取消 "1"手动取消)
        /// </summary>
        public string ReasonType
        {
            get;
            set;
        }

    }
}