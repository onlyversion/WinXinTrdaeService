using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    public class FundChangeInfo
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
        /// Gets or sets 入金
        /// </summary>
        public double Amt
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 出金
        /// </summary>
        public double Amt2
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 手工调帐入金
        /// </summary>
        public double Amt3
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 手工调帐出金
        /// </summary>
        public double Amt4
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 经纪人提成
        /// </summary>
        public double Amt5
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 赠金
        /// </summary>
        public double Amt6
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 交易日信息表
        /// </summary>
        public List<FundChange> FundChangeList
        {
            get;
            set;
        }
    }
}
