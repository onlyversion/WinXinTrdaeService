//*******************************************************************************
//  文 件 名：DateHoliday.cs
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
    /// 节假日信息
    /// </summary>
    public class DateHoliday
    {

        /// <summary>
        /// Gets or sets 行情编码 可以为多个,用逗号隔开
        /// </summary>
        public string PriceCode
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 节假日名称
        /// </summary>
        public string HoliName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开始时间
        /// </summary>
        public DateTime Starttime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 结束时间 
        /// </summary>
        public DateTime Endtime
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
