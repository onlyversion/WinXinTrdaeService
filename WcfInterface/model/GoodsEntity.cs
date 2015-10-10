using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
  public  class GoodsEntity:EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                Dictionary<string, string> dicGoodsName = new Dictionary<string, string>();
                dicGoodsName.Add("Au", "黄金");
                dicGoodsName.Add("Ag", "白银");
                dicGoodsName.Add("Pd", "钯金");
                dicGoodsName.Add("Pt", "铂金");

                dicGoodsName.Add("AuP", "黄金平均价");
                dicGoodsName.Add("AgP", "白银平均价");
                dicGoodsName.Add("PdP", "钯金平均价");
                dicGoodsName.Add("PtP", "铂金平均价");

                //dicGoodsName.Add("AuT", "黄金");
                //dicGoodsName.Add("AgT", "白银");
                //dicGoodsName.Add("PdT", "钯金");
                //dicGoodsName.Add("PtT", "铂金");

                return dicGoodsName[Symbol.ToString()];
            }
        }

        /// <summary>
        /// Au = 1,Ag = 2,Pt = 3,Pd = 4
        /// </summary>
        public GoodsType Symbol { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public decimal Total { get; set; }
    }
}
