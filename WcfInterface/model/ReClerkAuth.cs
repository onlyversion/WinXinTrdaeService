//*******************************************************************************
//  文 件 名：ReClerkAuth.cs
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
    /// 金商店员返回信息
    /// </summary>
    public class ReClerkAuth
    {
        /// <summary>
        /// Gets or sets 登录标识
        /// </summary>
        public string LoginID
        {
            set;
            get;
        }

        /// <summary>
        ///  Gets or sets 金商账号
        /// </summary>
        public string AgentId
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 描述
        /// </summary>
        public string Desc
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 行情IP地址
        /// </summary>
        public string QuotesAddressIP
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 行情端口
        /// </summary>
        public int QuotesPort
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 店员权限 
        /// </summary>
        public AgentAuth AtAuth
        {
            set;
            get;
        }
    }
}
