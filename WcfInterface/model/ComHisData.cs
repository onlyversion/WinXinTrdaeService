using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    public class ComHisData
    {
        /// <summary>
        /// 历史数据
        /// </summary>
        public List<HisData> HisDataList
        {
            get;
            set;
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime mdtime
        {
            get;
            set;
        }
    }
}
