using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 订单操作记录
    /// </summary>
  public  class OrderAttachEntity
    {
        #region 表字段

        private string _orderAttachId;
        /// <summary>
        /// 编号GUID
        /// </summary>
        public string OrderAttachId
        {
            get { return _orderAttachId; }
            set { _orderAttachId = value; }
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
        /// 订单编号
        /// </summary>
        public string OrderNo
        {
            get { return _orderNo; }
            set { _orderNo = value; }
        }

        private int _userType;
        /// <summary>
        /// 提货 卖用户类型 本人 = 1, 他人代提 = 2
        /// </summary>
        public int UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }

        private string _name;
        /// <summary>
        /// 姓名(本人提货卖，为默认用户姓名)
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _phone;
        /// <summary>
        /// 下单电话
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private int _iDType;
        /// <summary>
        /// 证件类型
        /// </summary>
        public int IDType
        {
            get { return _iDType; }
            set { _iDType = value; }
        }

        private string _iDNo;
        /// <summary>
        /// 证件号
        /// </summary>
        public string IDNo
        {
            get { return _iDNo; }
            set { _iDNo = value; }
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
