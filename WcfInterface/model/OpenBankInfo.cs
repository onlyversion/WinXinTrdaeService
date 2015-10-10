//*******************************************************************************
//  文 件 名：OpenBankInfo.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/24
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
    /// 开户银行信息
    /// </summary>
    public class OpenBankInfo
    {
        /// <summary>
        /// Gets or sets 开户银行名称
        /// </summary>
        public string OpenBank
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开户银行行号
        /// </summary>
        public string BankAccount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开户银行卡户名
        /// </summary>
        public string AccountName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开户银行卡卡号
        /// </summary>
        public string BankCard
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开户行地址
        /// </summary>
        public string OpenBankAddress
        {
            get;
            set;
        }
    }
}
