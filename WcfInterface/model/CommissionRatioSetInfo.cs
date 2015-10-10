using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 会员佣金比例
    /// </summary>
    public class CommissionRatioSetInfo : ResultDesc
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { set; get; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrgID { set; get; }

        /// <summary>
        /// 一级返佣比例
        /// </summary>
        public double Ratio1 { set; get; }

        /// <summary>
        ///二级返佣比例
        /// </summary>
        public double Ratio2 { set; get; }

        /// <summary>
        /// 三级返佣比例
        /// </summary>
        public double Ratio3 { set; get; }
    }
}
