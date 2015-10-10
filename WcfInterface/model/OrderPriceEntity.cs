using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 订单回购价
    /// </summary>
    [Serializable]
    public class OrderPriceEntity : EntityBase
    {
        #region 表字段

        private string _orderPriceId;
        /// <summary>
        /// 编号GUID
        /// </summary>
        public string OrderPriceId
        {
            get { return _orderPriceId; }
            set { _orderPriceId = value; }
        }

        private string _orderId;
        /// <summary>
        /// 订单GUID
        /// </summary>
        public string OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        private string _orderNo;
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo
        {
            get { return _orderNo; }
            set { _orderNo = value; }
        }

        private string _priceId;
        /// <summary>
        /// 报价GUID
        /// </summary>
        public string PriceId
        {
            get { return _priceId; }
            set { _priceId = value; }
        }

        private decimal _auPrice;
        /// <summary>
        /// 黄金价格
        /// </summary>
        public decimal AuPrice
        {
            get { return _auPrice; }
            set { _auPrice = value; }
        }

        private decimal _agPrice;
        /// <summary>
        /// 白银价格
        /// </summary>
        public decimal AgPrice
        {
            get { return _agPrice; }
            set { _agPrice = value; }
        }

        private decimal _ptPrice;
        /// <summary>
        /// 铂金价格
        /// </summary>
        public decimal PtPrice
        {
            get { return _ptPrice; }
            set { _ptPrice = value; }
        }

        private decimal _pdPrice;
        /// <summary>
        /// 钯金价格
        /// </summary>
        public decimal PdPrice
        {
            get { return _pdPrice; }
            set { _pdPrice = value; }
        }

        #endregion

        #region 外键字段

        #endregion

    }
}
