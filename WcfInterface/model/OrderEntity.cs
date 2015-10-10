using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 订单
    /// </summary>
    public class OrderEntity : EntityBase
    {
        #region 表字段
        private string _orderId;
        /// <summary>
        /// 订单编号GUID
        /// </summary>
        public string OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        private string _orderNo;
        /// <summary>
        /// 订单编号 xxgold201211222038ffff 名称+类别+时间
        /// </summary>
        public string OrderNo
        {
            get { return _orderNo; }
            set { _orderNo = value; }
        }

        private string _orderCode;
        /// <summary>
        /// 订单 提货或卖验证码 200000ssffff(没有则为空)
        /// </summary>
        public string OrderCode
        {
            get { return _orderCode; }
            set { _orderCode = value; }
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

        private int _carryWay;
        /// <summary>
        /// 提货，卖方式 1在线 2金店提货 3邮寄 4金店卖 0非提货卖
        /// </summary>
        public int CarryWay
        {
            get { return _carryWay; }
            set { _carryWay = value; }
        }

        private string _userId;
        /// <summary>
        /// 用户GUID
        /// </summary>
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _account;
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }

        private string _jUserId;
        /// <summary>
        /// 金生金账户
        /// </summary>
        public string JUserId
        {
            get { return _jUserId; }
            set { _jUserId = value; }
        }

        private decimal _au;
        /// <summary>
        /// 黄金重量
        /// </summary>
        public decimal Au
        {
            get { return _au; }
            set { _au = value; }
        }

        private decimal _ag;
        /// <summary>
        /// 白银重量
        /// </summary>
        public decimal Ag
        {
            get { return _ag; }
            set { _ag = value; }
        }

        private decimal _pt;
        /// <summary>
        /// 铂金重量
        /// </summary>
        public decimal Pt
        {
            get { return _pt; }
            set { _pt = value; }
        }

        private decimal _pd;
        /// <summary>
        /// 钯金
        /// </summary>
        public decimal Pd
        {
            get { return _pd; }
            set { _pd = value; }
        }

        private string _createDate;
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public string CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }
        /// <summary>
        /// 操作日期
        /// </summary>
        public string OperationDate
        {
            get;
            set;
        }
        private string _endDate;
        /// <summary>
        /// 订单最后处理日期
        /// </summary>
        public string EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        private int _state;
        /// <summary>
        /// 订单状态 1新订单 2交易完成 3取消订单 4过期订单
        /// </summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        private int _tmpId;
        /// <summary>
        /// 用于生成增ID
        /// </summary>
        public int TmpId
        {
            get { return _tmpId; }
            set { _tmpId = value; }
        }

        private int _version;
        /// <summary>
        /// 版本控制
        /// </summary>
        public int Version
        {
            get { return _version; }
            set { _version = value; }
        }



        private decimal _auQuantity;
        /// <summary>
        /// 黄金开票数量
        /// </summary>
        public decimal AuQuantity
        {
            get { return _auQuantity; }
            set { _auQuantity = value; }
        }
        private decimal _agQuantity;
        /// <summary>
        /// 白银开票数量
        /// </summary>
        public decimal AgQuantity
        {
            get { return _agQuantity; }
            set { _agQuantity = value; }
        }
        private decimal _ptQuantity;
        /// <summary>
        /// 铂金开票数量
        /// </summary>
        public decimal PtQuantity
        {
            get { return _ptQuantity; }
            set { _ptQuantity = value; }
        }
        private decimal _pdQuantity;
        /// <summary>
        /// 钯金开票数量
        /// </summary>
        public decimal PdQuantity
        {
            get { return _pdQuantity; }
            set { _pdQuantity = value; }
        }

        /// <summary>
        /// 交易帐号
        /// </summary>
        public string tradeAccount { get; set; }

        private decimal _auP;
        /// <summary>
        /// 黄金价
        /// </summary>
        public decimal AuP
        {
            get { return _auP; }
            set { _auP = value; }
        }
        private decimal _agP;
        /// <summary>
        /// 白银价
        /// </summary>
        public decimal AgP
        {
            get { return _agP; }
            set { _agP = value; }
        }
        private decimal _ptP;
        /// <summary>
        /// 铂金价
        /// </summary>
        public decimal PtP
        {
            get { return _ptP; }
            set { _ptP = value; }
        }
        private decimal _pdP;
        /// <summary>
        /// 钯金价
        /// </summary>
        public decimal PdP
        {
            get { return _pdP; }
            set { _pdP = value; }
        }
        /// <summary>
        /// 黄金成本价
        /// </summary>
        public decimal AuCost { get; set; }

        /// <summary>
        /// 白银成本价
        /// </summary>
        public decimal AgCost { get; set; }
        /// <summary>
        /// 铂金成本价
        /// </summary>
        public decimal PtCost { get; set; }
        /// <summary>
        /// 钯金成本价
        /// </summary>
        public decimal PdCost { get; set; }

        private decimal _auT;
        /// <summary>
        /// 
        /// </summary>
        public decimal AuT
        {
            get { return _auT; }
            set { _auT = value; }
        }
        private decimal _agT;
        /// <summary>
        /// 
        /// </summary>
        public decimal AgT
        {
            get { return _agT; }
            set { _agT = value; }
        }
        private decimal _pdT;
        /// <summary>
        /// 
        /// </summary>
        public decimal PdT
        {
            get { return _pdT; }
            set { _pdT = value; }
        }
        private decimal _ptT;
        /// <summary>
        /// 
        /// </summary>
        public decimal PtT
        {
            get { return _ptT; }
            set { _ptT = value; }
        }

        #endregion

        #region 外键字段  订单附属信息

        /// <summary>
        /// 用户提货卖类型 1本人 2他人代提(或用户类型 个人、组织)
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CardType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CardNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNum { get; set; }
        /// <summary>
        /// 证件类型
        /// </summary>
        public int IDType { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        public string IDNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        #endregion

        #region 外键字段 订单回购价信息

        /// <summary>
        /// 黄金回购价
        /// </summary>
        public decimal Au_b { get; set; }

        /// <summary>
        /// 白银回购价
        /// </summary>
        public decimal Ag_b { get; set; }

        /// <summary>
        /// 铂金回购价
        /// </summary>
        public decimal Pt_b { get; set; }

        /// <summary>
        /// 钯金回购价
        /// </summary>
        public decimal Pd_b { get; set; }
        /// <summary>
        /// 金商绑定用户表ID
        /// </summary>
        public string AgentUserID { get; set; }
        /// <summary>
        /// 金店店员ID
        /// </summary>
        public string ClerkID { get; set; }

        /// <summary>
        /// 金商ID
        /// </summary>
        public string AgentID { get; set; }
        #endregion

        #region 外键字段 订单用户信息

        /// <summary>
        /// 代理商名称
        /// </summary>
        public string AgentName { get; set; }
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

        #endregion
    }
}
