using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 金商
    /// </summary>
 public   class AgentUserEntity:EntityBase
    {
        #region 表字段

        private string _agentUserId;
        /// <summary>
        /// 编号GUID
        /// </summary>
        public string AgentUserId
        {
            get { return _agentUserId; }
            set { _agentUserId = value; }
        }

        private string _agentInfoId;
        /// <summary>
        /// 金商编号
        /// </summary>
        public string AgentInfoId
        {
            get { return _agentInfoId; }
            set { _agentInfoId = value; }
        }

        private string _account;
        /// <summary>
        /// 用户账户
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

        private string _name;
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _userType;
        /// <summary>
        /// 用户类型 1普通用户 0管理员
        /// </summary>
        public int UserType
        {
            get { return _userType; }
            set { _userType = value; }
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

        /// <summary>
        /// 金商名称
        /// </summary>
        public string AgentName { get; set; }

        #endregion
    }
}
