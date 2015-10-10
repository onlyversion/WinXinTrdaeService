//*******************************************************************************
//  文 件 名：TradeSet.cs
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
    /// 交易设置
    /// </summary>
    public class TradeSet
    {
        /// <summary>
        /// Gets or sets 名称
        /// </summary>
        public string ObjName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 名称编码
        /// </summary>
        public string ObjCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 对象的值
        /// </summary>
        public string ObjValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

    }
}
