using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 金生金绑定
    /// </summary>
   public class UserBindEntity:EntityBase
    {
        #region 表字段

        private string _userBindId;
        /// <summary>
        /// 客户绑定编号GUID
        /// </summary>
        public string UserBindId
        {
            get { return _userBindId; }
            set { _userBindId = value; }
        }

        private string _userId;
        /// <summary>
        /// 保证金客户GUID
        /// </summary>
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private string _jUserId;
        /// <summary>
        /// 金生金用户ID
        /// </summary>
        public string JUserId
        {
            get { return _jUserId; }
            set { _jUserId = value; }
        }

        private int _isEnable;
        /// <summary>
        /// 1 正常 0禁用
        /// </summary>
        public int IsEnable
        {
            get { return _isEnable; }
            set { _isEnable = value; }
        }

        private string _createDate;
        /// <summary>
        /// 绑定日期
        /// </summary>
        public string CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        #endregion
    }
}
