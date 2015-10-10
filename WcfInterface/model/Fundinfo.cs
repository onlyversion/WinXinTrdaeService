//*******************************************************************************
//  文 件 名：Fundinfo.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/01
//
//  修改标识：
//  修改说明：
//*******************************************************************************
using System;

namespace WcfInterface.model
{
    /// <summary>
    /// 账号资金
    /// </summary>
    public class Fundinfo
    {
        /// <summary>
        /// Gets or sets 资金账户
        /// </summary>
        public string CashUser
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 子账号
        /// </summary>
        public string SubUser
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 摊位号
        /// </summary>
        public string TanUser
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 
        /// 0 代表未绑定银行状态
        /// 1 代表银行审核中
        /// 2 代表银行已经绑定成功
        /// 3 代表银行审核失败
        /// 4 已解约
        /// </summary>
        public string State
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
        /// Gets or sets 保证金
        /// </summary>
        public double OccMoney
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 预付款
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
        /// Gets or sets 银行类型代码
        /// </summary>
        public string ConBankType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开户银行
        /// </summary>
        public string OpenBank
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 银行行号
        /// </summary>
        public string BankAccount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 银行卡户名
        /// </summary>
        public string AccountName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 银行卡卡号
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

        /// <summary>
        /// Gets or sets 签约时间
        /// </summary>
        public DateTime? ConTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 解约时间
        /// </summary>
        public DateTime? RescindTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 签约行和开户行是否相同
        /// </summary>
        public bool SameBank
        {
            get;
            set;
        }
        
    }
}
