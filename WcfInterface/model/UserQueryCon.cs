//*******************************************************************************
//  文 件 名：UserQueryCon.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/08/29
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
    /// 用户查询条件
    /// </summary>
    public class UserQueryCon
    {
        /// <summary>
        ///  Gets or sets 管理员或组织登陆标识
        /// </summary>
        public string LoginId
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets 用户帐号
        /// </summary>
        public string TradeAccount
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets 用户名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets 证件号码
        /// </summary>
        public string CardNum
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 用户类型
        /// </summary>
        public UserType UType
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets a value indicating whether 
        ///  是否在线
        /// </summary>
        public bool OnLine
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
        /// Gets or sets 用户组ID
        /// </summary>
        public string UserGroupId
        {
            get;
            set;
        }

        /// <summary>
        /// 手机
        /// </summary>
        public string TelPhone
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
        /// 是否经济人，0全部，1是，2，否
        /// </summary>
        public string IsBroker
        {
            get;
            set;
        }

        /// <summary>
        /// 经济人
        /// </summary>
        public string Broker
        {
            get;
            set;
        }
    }
}
