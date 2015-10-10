//*******************************************************************************
//  文 件 名：DateSet.cs
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
    /// 交易日信息
    /// </summary>
    public class DateSet
    {
        /// <summary>
        /// Gets or sets 行情编码
        /// </summary>
        public string PriceCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 周几(1周一 2周二 3周三 4周四 5 周五 6周六 0周日)
        /// </summary>
        public string Weekday
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开始时间,时间存储格式为: HH:mm:ss
        /// </summary>
        public string Starttime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 结束时间,时间存储格式为: HH:mm:ss
        /// </summary>
        public string Endtime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether  是否禁止交易(0  不禁止 ;1 禁止交易)
        /// </summary>
        public bool Istrade
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

    }
}
