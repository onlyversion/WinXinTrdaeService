﻿//*******************************************************************************
//  文 件 名：UserBaseInfo.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/03
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
    /// 所有交易用户信息
    /// </summary>
    public class UserBaseInfo
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
        /// Gets or sets 帐户余额
        /// </summary>
        public double Money
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
        /// Gets or sets 冻结资金
        /// </summary>
        public double FrozenMoney
        {
            get;
            set;
        }

         /// <summary>
        /// Gets or sets 冻结资金
        /// </summary>
        public double DongJieMoney
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets 交易用户列表
        /// </summary>
        public List<TradeUser> TdUserList
        {
            get;
            set;
        }
    }
}
