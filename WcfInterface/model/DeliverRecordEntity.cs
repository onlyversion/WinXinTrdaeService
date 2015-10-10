using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 交割单记录
    /// </summary>
  public  class DeliverRecordEntity:EntityBase
    {
        #region 表字段

        private string _deliverRecordId;
        /// <summary>
        /// 编号GUID
        /// </summary>
        public string DeliverRecordId
        {
            get { return _deliverRecordId; }
            set { _deliverRecordId = value; }
        }

        private string _deliverId;
        /// <summary>
        /// 交割单GUID
        /// </summary>
        public string DeliverId
        {
            get { return _deliverId; }
            set { _deliverId = value; }
        }

        private string _deliverNo;
        /// <summary>
        /// 交割单号
        /// </summary>
        public string DeliverNo
        {
            get { return _deliverNo; }
            set { _deliverNo = value; }
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

        private int _orderType;

        /// <summary>
        /// 订单类型 1提货订单 2卖订单 3 回购订单 4金生金
        /// </summary>
        public int OrderType
        {
            get { return _orderType; }
            set { _orderType = value; }
        }

        private int _goods;
        /// <summary>
        /// 物品类型
        /// </summary>
        public int Goods
        {
            get { return _goods; }
            set { _goods = value; }
        }

        private int _direction;
        /// <summary>
        /// 交割的方向提货单=1,卖单=2
        /// </summary>
        public int Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        private decimal _useTotal;
        /// <summary>
        /// 使用重量
        /// </summary>
        public decimal UseTotal
        {
            get { return _useTotal; }
            set { _useTotal = value; }
        }

        private decimal _lockPrice;
        /// <summary>
        /// 锁定价格
        /// </summary>
        public decimal LockPrice
        {
            get { return _lockPrice; }
            set { _lockPrice = value; }
        }

        private string _createDate;
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        #endregion

        #region 外键字段

        #endregion
    }
}
