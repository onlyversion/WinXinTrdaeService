//*******************************************************************************
//  文 件 名：TradeSetInfo.cs
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
    /// 交易设置信息
    /// </summary>
    public class TradeSetInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether 
        /// 结果(1成功 0失败)
        /// </summary>
        public bool Result
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
        /// Gets or sets 返回代码
        /// </summary>
        public string ReturnCode
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 交易设置
        /// </summary>
        public List<TradeSet> TdSetList
        {
            get;
            set;
        }
    }
}
