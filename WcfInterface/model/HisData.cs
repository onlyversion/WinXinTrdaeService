using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    public class HisData
    {
        /// <summary>
        /// Gets or sets 周期时间
        /// </summary>
        public string weektime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 开盘价
        /// </summary>
        public string openprice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 最高价
        /// </summary>
        public string highprice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 最低价
        /// </summary>
        public string lowprice
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 收盘价
        /// </summary>
        public string closeprice
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets 成交量
        /// </summary>
        public string volnum
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 修改时间
        /// </summary>
        public DateTime mdtime
        {
            get;
            set;
        }
    }
}
