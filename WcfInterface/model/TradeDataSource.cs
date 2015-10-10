using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    public class TradeDataSource
    {
        /// <summary>
        /// 保留小数位数
        /// </summary>
        public int adjustcount { get; set; }

        /// <summary>
        /// 汇率
        /// </summary>
        public double rate { get; set; }

        /// <summary>
        /// 水
        /// </summary>
        public double water { get; set; }

        /// <summary>
        /// 行情编码
        /// </summary>
        public string pricecode { get; set; }

        /// <summary>
        /// 价格调整系数 白银系数为1000 其他默认为1
        /// </summary>
        public double coefficient { get; set; }

        /// <summary>
        /// 比例系数
        /// </summary>
        public double coefxs { get; set; }

        /// <summary>
        /// 是否转换价格 1转换 0不转换
        /// </summary>
        public int IsConvert { get; set; }
    }
}
