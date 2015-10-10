using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 金商记录
    /// </summary>
   public class AgentDeliverEntity
    {
        #region 表字段

        private string _agentDeliverId;
        /// <summary>
        /// 编号GUID
        /// </summary>
        public string AgentDeliverId
        {
            get { return _agentDeliverId; }
            set { _agentDeliverId = value; }
        }

        private string _agentInfoId;
        /// <summary>
        /// 金商GUID
        /// </summary>
        public string AgentInfoId
        {
            get { return _agentInfoId; }
            set { _agentInfoId = value; }
        }

        private int _fromTo;
        /// <summary>
        /// 本次记录来源(1 用户->金商 为用户订单号 0 金商->总店 金商编号 2手动操作 3 卖转库存)
        /// </summary>
        public int FromTo
        {
            get { return _fromTo; }
            set { _fromTo = value; }
        }

        private string _orderId;
        /// <summary>
        /// 订单编号(用户->金商 为用户订单编号 金商->总店 为金商编号)
        /// </summary>
        public string OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        private Direction _direction;
        /// <summary>
        /// 方向
        /// </summary>
        public Direction Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        private decimal _au;
        /// <summary>
        /// 黄金
        /// </summary>
        public decimal Au
        {
            get { return _au; }
            set { _au = value; }
        }

        private decimal _ag;
        /// <summary>
        /// 白银
        /// </summary>
        public decimal Ag
        {
            get { return _ag; }
            set { _ag = value; }
        }

        private decimal _pt;
        /// <summary>
        /// 铂金
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

        private decimal _availableAu;
        /// <summary>
        /// 可用黄金
        /// </summary>
        public decimal AvailableAu
        {
            get { return _availableAu; }
            set { _availableAu = value; }
        }

        private decimal _availableAg;
        /// <summary>
        /// 可用白银
        /// </summary>
        public decimal AvailableAg
        {
            get { return _availableAg; }
            set { _availableAg = value; }
        }

        private decimal _availablePt;
        /// <summary>
        /// 可用铂金
        /// </summary>
        public decimal AvailablePt
        {
            get { return _availablePt; }
            set { _availablePt = value; }
        }

        private decimal _availablePd;
        /// <summary>
        /// 可用钯金
        /// </summary>
        public decimal AvailablePd
        {
            get { return _availablePd; }
            set { _availablePd = value; }
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

        private int _state;
        /// <summary>
        /// 状态 1未完成 0完成
        /// </summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        #endregion

        #region 外键字段

        /// <summary>
        /// 金商编号
        /// </summary>
        public string AgentCode { get; set; }

        /// <summary>
        /// 金商名称
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// 金商类别 0电子商务 1 分店 2旗舰店 3总店
        /// </summary>
        public int AgentType { get; set; }
       /// <summary>
       /// 
       /// </summary>
        public string AgentTypeName
        {
            get
            {
                switch (AgentType)
                {
                    case 0:
                        return "电子商务";
                    case 1:
                        return "分店";
                    case 2: return "旗舰店";
                    case 3:
                        return "总店";
                }
                return "";
            }
        }

        #endregion
    }
}
