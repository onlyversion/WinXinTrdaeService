using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 权限
    /// </summary>
   public class PrivilegeEntity:EntityBase
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public string PrivilegeId
        { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PrivilegeName
        {
            get;
            set;
        }
        /// <summary>
        /// 上级权限ID
        /// </summary>
        public string ParentPrivilegeId
        { get; set; }
        /// <summary>
        /// 权限编码
        /// </summary>
        public string PrivilegeCode
        { get; set; }
        /// <summary>
        /// 上级权限名称
        /// </summary>
        public string ParentPrivilegeName
        { get; set; }
        /// <summary>
        /// 权限类别
        /// </summary>
        public PrivilegeType PrivilegeType
        {
            get;
            set;
        }
        /// <summary>
        /// 程序集
        /// </summary>
        public string Library
        { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string NameSpace
        { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public Status Status
        { get; set; }
       /// <summary>
       /// 检测验证
       /// </summary>
        public bool Check
        {
            get;
            set;
        }
       /// <summary>
       /// 显示顺序
       /// </summary>
        public int Displayorder
        { get; set; }
       /// <summary>
       /// 图片
       /// </summary>
        public string MenuPic
        { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        { get; set; }
    }
}
