//*******************************************************************************
//  文 件 名：TradeIp.cs
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
    /// IP 过滤表
    /// </summary>
    public class TradeIp
    {
        /// <summary>
        /// Gets or sets 开始IP
        /// </summary>
        public string StartIp
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 结束IP
        /// </summary>
        public string EndIp
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 描述
        /// </summary>
        public string Desc
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets ID
        /// </summary>
        public string ID
        {
            get;
            set;
        }
    }
}
