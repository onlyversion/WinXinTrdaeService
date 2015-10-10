using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 金商信息
    /// </summary>
 public   class AgentInfoEntity:EntityBase
    {
        #region 表字段

        private string _agentInfoId;
        /// <summary>
        /// 金商信息编号GUID
        /// </summary>
        public string AgentInfoId
        {
            get { return _agentInfoId; }
            set { _agentInfoId = value; }
        }

        private string _agentCode;
        /// <summary>
        /// 金商编号
        /// </summary>
        public string AgentCode
        {
            get { return _agentCode; }
            set { _agentCode = value; }
        }

        private string _agentName;
        /// <summary>
        /// 金商名称
        /// </summary>
        public string AgentName
        {
            get { return _agentName; }
            set { _agentName = value; }
        }

        private string _contact;
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }

        private int _agentType;
        /// <summary>
        /// 金商类别 0电子商务 1 分店 2旗舰店 3总店
        /// </summary>
        public int AgentType
        {
            get { return _agentType; }
            set { _agentType = value; }
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
        /// 钯金重量
        /// </summary>
        public decimal Pd
        {
            get { return _pd; }
            set { _pd = value; }
        }

        private decimal _au_b;
        /// <summary>
        /// 可卖黄金
        /// </summary>
        public decimal Au_b
        {
            get { return _au_b; }
            set { _au_b = value; }
        }

        private decimal _ag_b;
        /// <summary>
        /// 可卖白银
        /// </summary>
        public decimal Ag_b
        {
            get { return _ag_b; }
            set { _ag_b = value; }
        }

        private decimal _pt_b;
        /// <summary>
        /// 可卖铂金
        /// </summary>
        public decimal Pt_b
        {
            get { return _pt_b; }
            set { _pt_b = value; }
        }

        private decimal _pd_b;
        /// <summary>
        /// 可卖钯金
        /// </summary>
        public decimal Pd_b
        {
            get { return _pd_b; }
            set { _pd_b = value; }
        }

        private string _tel;
        /// <summary>
        /// 金商联系电话
        /// </summary>
        public string Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }

        private string _address;
        /// <summary>
        /// 金商地址
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
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

        private int _isEnable;
        /// <summary>
        /// 1正常 0禁用
        /// </summary>
        public int IsEnable
        {
            get { return _isEnable; }
            set { _isEnable = value; }
        }

        #endregion

        #region 外键字段

        #endregion
     /// <summary>
        ///  金商
     /// </summary>
        public string AgentTypeName
        {
            get
            {
                string[] arr = { "电子商务", "分店", "旗舰店", "总店" };
                return arr[_agentType];
            }
        }
     /// <summary>
     /// 金商绑定用户ID
     /// </summary>
        public string UserID
        { get; set; }
    }
}
