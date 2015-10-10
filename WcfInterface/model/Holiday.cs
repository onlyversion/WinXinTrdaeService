//*******************************************************************************
//  文 件 名：Holiday.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/01
//
//  修改标识：
//  修改说明：
//*******************************************************************************
using System;

namespace WcfInterface.model
{
    /// <summary>
    /// Gets or sets 节假日
    /// </summary>
    public struct Holiday
    {
        /// <summary>
        /// 行情编码 可以为多个，逗号隔开
        /// </summary>
        public string PriceCode;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime;

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime;
    }
}
