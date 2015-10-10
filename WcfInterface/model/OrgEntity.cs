using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 组织机构
    /// </summary>
    public class OrgEntity : EntityBase
    {
        /// <summary>
        /// 带父级组织编码的组织名称
        /// </summary>
        public string CodeOrgName
        {
            get;
            set;
        }

        /// <summary>
        /// 组织ID
        /// </summary>
        public string OrgID
        {
            get;
            set;
        }
        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrgName
        {
            get;
            set;
        }
        /// <summary>
        /// 法人
        /// </summary>
        public string Coperson
        {
            get;
            set;
        }
        /// <summary>
        /// 证件类型
        /// </summary>
        public IDType CardType
        {
            get;
            set;
        }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string CardNum
        {
            get;
            set;

        }
        /// <summary>
        /// 上级组织ID
        /// </summary>
        public string ParentOrgId
        { get; set; }
        /// <summary>
        /// 上级组织名称
        /// </summary>
        public string ParentOrgName
        { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Reperson
        {
            get;
            set;
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string PhoneNum
        {
            get;
            set;
        }
        /// <summary>
        /// 固定电话
        /// </summary>
        public string TelePhone
        {
            get;
            set;
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get;
            set;
        }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address
        {
            get;
            set;
        }
        /// <summary>
        /// 新增时间
        /// </summary>
        public string AddTime
         {
             get;
             set;
         }
        /// <summary>
        /// 是否启用
        /// </summary>
        public Status Status
        {
            get;
            set;
        }
    }
}
