//*******************************************************************************
//  文 件 名：ClerkAuth.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/11/22
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
    /// 金商店员权限
    /// </summary>
    public class ClerkAuth
    {
        /// <summary>
        /// Gets or sets a value indicating whether 受理明细
        /// </summary>
        public bool ShouLiMingXi
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 提货
        /// </summary>
        public bool TiHuo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 绑定账号
        /// </summary>
        public bool BangDingUser
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 交易管理
        /// </summary>
        public bool TradeManage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 提货受理
        /// </summary>
        public bool TiHuoShouLi
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 金商金单
        /// </summary>
        public bool JgjOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 回购单
        /// </summary>
        public bool HuiGouOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 提货单
        /// </summary>
        public bool TihuoOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 交割单
        /// </summary>
        public bool DeliverOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 卖检测
        /// </summary>
        public bool CheckManage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 卖处理
        /// </summary>
        public bool CheckDel
        {
            get;
            set;
        }
    }
}
