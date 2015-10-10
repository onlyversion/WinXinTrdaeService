using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserEntity : EntityBase
    {
        #region 表字段

        private string _userId;
        /// <summary>
        /// 编号GUID
        /// </summary>
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _name;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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

        private string _password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private int _userType;
        /// <summary>
        /// 用户类型 1个人 2企业或组织
        /// </summary>
        public int UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }

        private string _phone;
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private string _contact;
        /// <summary>
        /// 联系人(组织机构或企业)
        /// </summary>
        public string Contact
        {
            get { return _contact; }
            set { _contact = value; }
        }

        private string _agentName;
        /// <summary>
        /// 代理商名称
        /// </summary>
        public string AgentName
        {
            get { return _agentName; }
            set { _agentName = value; }
        }

        private IDType _iDType;
        /// <summary>
        /// 证件类型 1身份证 2机构代码 3营业执照
        /// </summary>
        public IDType IDType
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

        private string _lastLoginIP;
        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastLoginIP
        {
            get { return _lastLoginIP; }
            set { _lastLoginIP = value; }
        }

        private string _lastLoginDate;
        /// <summary>
        /// 最后登录日期
        /// </summary>
        public string LastLoginDate
        {
            get { return _lastLoginDate; }
            set { _lastLoginDate = value; }
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

        private int _isEnable;
        /// <summary>
        /// 1正常 0禁用
        /// </summary>
        public int IsEnable
        {
            get { return _isEnable; }
            set { _isEnable = value; }
        }

        private decimal _auTotal;
        /// <summary>
        /// 黄金开票总金额
        /// </summary>
        public decimal AuTotal
        {
            get { return _auTotal; }
            set { _auTotal = value; }
        }

        private decimal _agTotal;
        /// <summary>
        /// 未使用白银开票总金额
        /// </summary>
        public decimal AgTotal
        {
            get { return _agTotal; }
            set { _agTotal = value; }
        }
        private decimal _ptTotal;
        /// <summary>
        /// 未使用铂金开票总金额
        /// </summary>
        public decimal PtTotal
        {
            get { return _ptTotal; }
            set { _ptTotal = value; }
        }
        private decimal _pdTotal;
        /// <summary>
        /// 未使用钯金开票总金额
        /// </summary>
        public decimal PdTotal
        {
            get { return _pdTotal; }
            set { _pdTotal = value; }
        }
        private decimal _auPrice;
        /// <summary>
        /// 黄金平均价
        /// </summary>
        public decimal AuPrice
        {
            get { return _auPrice; }
            set { _auPrice = value; }
        }

        private decimal _agPrice;
        /// <summary>
        /// 白银平均价
        /// </summary>
        public decimal AgPrice
        {
            get { return _agPrice; }
            set { _agPrice = value; }
        }
        private decimal _ptPrice;
        /// <summary>
        /// 铂金平均价
        /// </summary>
        public decimal PtPrice
        {
            get { return _ptPrice; }
            set { _ptPrice = value; }
        }
        private decimal _pdPrice;
        /// <summary>
        /// 钯金平均价
        /// </summary>
        public decimal PdPrice
        {
            get { return _pdPrice; }
            set { _pdPrice = value; }
        }

        private decimal ptAmount;
        /// <summary>
        ///  铂金可开票数量
        /// </summary>
        public decimal PtAmount
        {
            get { return ptAmount; }
            set { ptAmount = value; }
        }
        private decimal pdAmount;
        /// <summary>
        /// 钯金可开票数量
        /// </summary>
        public decimal PdAmount
        {
            get { return pdAmount; }
            set { pdAmount = value; }
        }

        private decimal auAmount;
        /// <summary>
        /// 黄金可开票数量
        /// </summary>
        public decimal AuAmount
        {
            get { return auAmount; }
            set { auAmount = value; }
        }
        private decimal agAmount;
        /// <summary>
        /// 白银可开票数量
        /// </summary>
        public decimal AgAmount
        {
            get { return agAmount; }
            set { agAmount = value; }
        }
        #endregion

        #region 外键字段

        /// <summary>
        /// 是否已经绑定金生金 1已绑定 0未绑定
        /// </summary>
        public int IsBind { get; set; }

        #endregion

    }
}
