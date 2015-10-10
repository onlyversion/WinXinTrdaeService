//*******************************************************************************
//  文 件 名：AgentUser.cs
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
    /// 金商信息
    /// </summary>
    public class AgentUser
    {
        /// <summary>
        /// Gets or sets 金商账号
        /// </summary>
        public string AgentId
        {
            get;
            set;
        }  
        
        /// <summary>
        /// Gets or sets 金商密码
        /// </summary>
        public string PassWord
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 公司名称
        /// </summary>
        public string ComName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 法人
        /// </summary>
        public string Coperson
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 证件类型
        /// </summary>
        public string CardType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 证件号码
        /// </summary>
        public string CardNum
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 金商
        /// </summary>
        public string Agent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 负责人
        /// </summary>
        public string Reperson
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 手机
        /// </summary>
        public string PhoneNum
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 固定电话
        /// </summary>
        public string Telephone
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 邮箱
        /// </summary>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 地址
        /// </summary>
        public string Address
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 该金商被添加的时间
        /// </summary>
        public DateTime AddTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 最后登陆时间
        /// </summary>
        public DateTime LastLoginTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 登陆标识
        /// </summary>
        public string LoginId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets IP
        /// </summary>
        public string Ip
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets MAC
        /// </summary>
        public string Mac
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 是否启用
        /// </summary>
        public bool Enable
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether 是否在线
        /// </summary>
        public bool OnLine
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 绑定账号
        /// </summary>
        public string TradeAccount
        {
            get;
            set;
        }

    }
}
