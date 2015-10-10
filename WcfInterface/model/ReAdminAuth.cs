//*******************************************************************************
//  文 件 名：ReAdminAuth.cs
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
    /// 管理员登陆返回信息
    /// </summary>
    public class ReAdminAuth
    {
        /// <summary>
        /// Gets or sets 登录标识 失败返回"-1"
        /// </summary>
        public string LoginID
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 用户ID
        /// </summary>
        public string UserID
        {
            get;
            set;
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
    }
}
