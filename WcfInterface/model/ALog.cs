//*******************************************************************************
//  文 件 名：ALog.cs
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
    /// 操作日志
    /// </summary>
    public class ALog
    {
        /// <summary>
        /// Gets or sets 操作时间
        /// </summary>
        public DateTime OperTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 操作账号
        /// </summary>
        public string Account
        {
            get;
            set;
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UType
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 操作描述
        /// </summary>
        public string Desc
        {
            get;
            set;
        }

    }
}
