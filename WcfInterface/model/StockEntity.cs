using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 保证金库存
    /// </summary>
    public class StockEntity : EntityBase
    {
        /// <summary>
        /// 库存ID
        /// </summary>
        public string StockID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 黄金数量
        /// </summary>
        public decimal Au { get; set; }
        /// <summary>
        /// 白银重量
        /// </summary>
        public decimal Ag { get; set; }
        /// <summary>
        /// 铂金重量
        /// </summary>
        public decimal Pt { get; set; }
        /// <summary>
        /// 钯金重量
        /// </summary>
        public decimal Pd { get; set; }
        /// <summary>
        /// 可卖黄金
        /// </summary>
        public decimal Au_b { get; set; }
        /// <summary>
        /// 可卖白银
        /// </summary>
        public decimal Ag_b { get; set; }
        /// <summary>
        /// 可卖铂金
        /// </summary>
        public decimal Pt_b { get; set; }
        /// <summary>
        /// 可卖钯金
        /// </summary>
        public decimal Pd_b { get; set; }
        /// <summary>
        /// 黄金平均价
        /// </summary>
        public decimal AuPrice { get; set; }
        /// <summary>
        /// 白银平均价
        /// </summary>
        public decimal AgPrice { get; set; }
        /// <summary>
        /// 铂金平均价
        /// </summary>
        public decimal PtPrice { get; set; }
        /// <summary>
        /// 钯金平均价
        /// </summary>
        public decimal PdPrice { get; set; }
        /// <summary>
        /// 黄金总数量
        /// </summary>
        public decimal AuTotal { get; set; }
        /// <summary>
        /// 白银总数量
        /// </summary>
        public decimal AgTotal { get; set; }
        /// <summary>
        /// 铂金总数量
        /// </summary>
        public decimal PtTotal { get; set; }
        /// <summary>
        /// 钯金总数量
        /// </summary>
        public decimal PdTotal { get; set; }
        /// <summary>
        /// 黄金可开票数量
        /// </summary>
        public decimal AuAmount { get; set; }
        /// <summary>
        /// 白银可开票数量
        /// </summary>
        public decimal AgAmount { get; set; }
        /// <summary>
        /// 铂金可开票数量
        /// </summary>
        public decimal PtAmount { get; set; }
        /// <summary>
        /// 钯金可开票数量
        /// </summary>
        public decimal PdAmount { get; set; }

    }
}
