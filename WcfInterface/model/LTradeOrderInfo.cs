using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    public class LTradeOrderInfo
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
        /// Gets or sets 数量
        /// </summary>
        public double Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 盈亏
        /// </summary>
        public double ProfitValue
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets 基础工费
        /// </summary>
        public double Tradefee
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets 仓储费
        /// </summary>
        public double Storagefee
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 订单表
        /// </summary>
        public List<LTradeOrder> LTdOrderList
        {
            get;
            set;
        }
    }
}
