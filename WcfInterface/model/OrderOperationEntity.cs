using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 订单操作记录
    /// </summary>
 public   class OrderOperationEntity
    {
        #region 表字段

        private string _operationId;
        /// <summary>
        /// 编号GUID
        /// </summary>
        public string OperationId
        {
            get { return _operationId; }
            set { _operationId = value; }
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

        private int _type;
        /// <summary>
        /// 订单操作类型
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _account;
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }

        private string _operationIP;
        /// <summary>
        /// 处理IP
        /// </summary>
        public string OperationIP
        {
            get { return _operationIP; }
            set { _operationIP = value; }
        }

        private string _operationDate;
        /// <summary>
        /// 处理日期
        /// </summary>
        public string OperationDate
        {
            get { return _operationDate; }
            set { _operationDate = value; }
        }

        private string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        #endregion
    }
}
