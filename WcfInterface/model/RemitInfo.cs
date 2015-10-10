//*******************************************************************************
//  文 件 名：RemitInfo.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/25
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
    /// 用户通过银行出入金接口参数
    /// </summary>
    public class RemitInfo
    {
        /// <summary>
        /// Gets or sets 用户ID
        /// </summary>
        public string userid
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 支付密码
        /// </summary>
        public string PasswordChar
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 汇款日期(YYYYMMDD)
        /// </summary>
        public string RemitTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 汇款方式
        /// </summary>
        public int RemitType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 汇款人姓名
        /// </summary>
        public string RemitName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 汇款银行
        /// </summary>
        public string RemitBank
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 汇款账号
        /// </summary>
        public string RemitAccount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 要出入的资金金额
        /// </summary>
        public double Money
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 原因
        /// </summary>
        public int ReasonType
        {
            get;
            set;
        }

    }
}
