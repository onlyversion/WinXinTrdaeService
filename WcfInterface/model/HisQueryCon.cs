using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    public class HisQueryCon
    {

        /// <summary>
        /// Gets or sets 行情编码
        /// </summary>
        public string pricecode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 周期（M1,M5,M15,...MN）
        /// </summary>
        public string weekflg
        {
            get;
            set;
        }

        /// <summary>
        /// 时间，格式为:yyyyMMddHHmm
        /// </summary>
        public string weektime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }
    }
}
