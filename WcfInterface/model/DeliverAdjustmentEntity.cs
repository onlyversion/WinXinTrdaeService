using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 交割单明细
    /// </summary>
    public class DeliverAdjustmentEntity : EntityBase
    {
        #region 表字段

        private string _deliverId;
        /// <summary>
        /// 交割单GUID
        /// </summary>
        public string DeliverAdjustmentID
        {
            get { return _deliverId; }
            set { _deliverId = value; }
        }

        private string _deliverNo;
        /// <summary>
        /// 交割单号
        /// </summary>
        public string DeliverAdjustmentNO
        {
            get { return _deliverNo; }
            set { _deliverNo = value; }
        }

        private string _account;
        /// <summary>
        /// 用户账号(软件同步过来的)
        /// </summary>
        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
        private int _goods;
        /// <summary>
        /// 交割物品 黄金 = 1, 白银 = 2, 铂金 = 3, 钯金 = 4
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

        private decimal _total;
        /// <summary>
        /// 交割总重量
        /// </summary>
        public decimal Total
        {
            get { return _total; }
            set { _total = value; }
        }

        private string _deliverDate;
        /// <summary>
        /// 交割日期
        /// </summary>
        public string DeliverDate
        {
            get { return _deliverDate; }
            set { _deliverDate = value; }
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

        private decimal _availableTotal;
        /// <summary>
        /// 可用重量
        /// </summary>
        public decimal AvailableTotal
        {
            get { return _availableTotal; }
            set { _availableTotal = value; }
        }

        private int _fromFlag;
        /// <summary>
        /// 来源 0软件同步 1系统操作
        /// </summary>
        public int FromFlag
        {
            get { return _fromFlag; }
            set { _fromFlag = value; }
        }

        private int _state;
        /// <summary>
        /// 状态 1未完成 0完成
        /// </summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        private string _createDate;
        /// <summary>
        /// 插入日期
        /// </summary>
        public string CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EndDate
        {
            get;
            set;
        }
        private string _operationUserID;
        /// <summary>
        /// 插入日期
        /// </summary>
        public string OperationUserID
        {
            get { return _operationUserID; }
            set { _operationUserID = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserID
        {
            get;
            set;
        }
        #endregion
    }
}
