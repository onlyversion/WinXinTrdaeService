//*******************************************************************************
//  文 件 名：ClerkQueryCon.cs
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
    /// 店员查询条件
    /// </summary>
    public class ClerkQueryCon
    {/// <summary>
        ///  Gets or sets 管理员或金商登陆标识
        /// </summary>
        public string LoginId
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets 用户类型（0表示管理员 1表示金商）
        /// </summary>
        public int UserType
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets 金商账号(UserType=1时起作用)
        /// </summary>
        public string AgentId
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets 店员帐号
        /// </summary>
        public string ClerkId
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets 店员姓名
        /// </summary>
        public string ClerkName
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets 店员手机号码
        /// </summary>
        public string ClerkPhone
        {
            get;
            set;
        }
    }
}
