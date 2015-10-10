//*******************************************************************************
//  文 件 名：Clerk.cs
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
    /// 店员
    /// </summary>
    public class Clerk
    {
        /// <summary>
        /// Gets or sets 店员姓名
        /// </summary>
        public string ClerkName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 店员账号
        /// </summary>
        public string ClerkId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 店员密码
        /// </summary>
        public string ClerkPwd
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 店员手机号码
        /// </summary>
        public string ClerkPhone
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 金商
        /// </summary>
        public string AgentId
        {
            get;
            set;
        }
    }
}
