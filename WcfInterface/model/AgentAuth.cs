//*******************************************************************************
//  文 件 名：AgentAuth.cs
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
    /// 金商权限
    /// </summary>
    public class AgentAuth
    { 

        /// <summary>
        /// Gets or sets a value indicating whether 账户管理
        /// </summary>
        public bool IdManage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 客户管理
        /// </summary>
        public bool UserManage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 金商管理
        /// </summary>
        public bool AgentManage
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
        /// Gets or sets a value indicating whether 有效订单
        /// </summary>
        public bool OrderManage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 限价挂单
        /// </summary>
        public bool HoldManage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 入库单
        /// </summary>
        public bool StoreManage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 平仓记录
        /// </summary>
        public bool DelManage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 对冲交易
        /// </summary>
        public bool PtoManage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 到处报表
        /// </summary>
        public bool ReportManage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 有效订单报表
        /// </summary>
        public bool OrderReport
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 入库单报表
        /// </summary>
        public bool OrderStoreReport
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 平仓报表
        /// </summary>
        public bool OrderCancelReport
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 挂单报表
        /// </summary>
        public bool HoldReport
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 资金变动报表
        /// </summary>
        public bool MoneyReport
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 持仓新单
        /// </summary>
        public bool Orders
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 订单平仓
        /// </summary>
        public bool OrdersCancel
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 订单入库
        /// </summary>
        public bool OrdersStore
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 挂单
        /// </summary>
        public bool HoldOrder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 挂单撤销
        /// </summary>
        public bool HoldOrderCancel
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 新增客户
        /// </summary>
        public bool AddUserManage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 删除客户
        /// </summary>
        public bool DelUserManage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 资金调整
        /// </summary>
        public bool CashTzManage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 出入金
        /// </summary>
        public bool ChuRuManage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 客户资料
        /// </summary>
        public bool UserInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 交易明细
        /// </summary>
        public bool TradeInfo
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


        /// <summary>
        /// Gets or sets a value indicating whether 只读权限
        /// </summary>
        public bool ReadOnly
        {
            get;
            set;
        }

        //-------------添加 2013.11.22 因添加保证后台功能 
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
        /// Gets or sets a value indicating whether 金商店员
        /// </summary>
        public bool AgentClerk
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

        //-------------添加 2013.12.09 添加保证后台 报表权限

        /// <summary>
        /// Gets or sets a value indicating whether 买交割单报表
        /// </summary>
        public bool OrderTakeReport
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets a value indicating whether 回购单报表
        /// </summary>
        public bool OrderBackReport
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets a value indicating whether 卖交割单报表
        /// </summary>
        public bool OrderCheckReport
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets a value indicating whether 转金生金报表
        /// </summary>
        public bool JgjReport
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets a value indicating whether 提货受理报表
        /// </summary>
        public bool AgentDoDetail
        {
            get;
            set;
        }
    }
}
