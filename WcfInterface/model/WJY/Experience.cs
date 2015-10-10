using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model.WJY
{
    /// <summary>
    /// 体验券
    /// </summary>
    public class Experience
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Annount { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal Rceharge { get; set; }
        /// <summary>
        /// 张数
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CreatID { get; set; }
        /// <summary>
        /// 是否有效 0:有效 1:失效
        /// </summary>
        public int Effective { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime EffectiveTime { get; set; }
    }
}
