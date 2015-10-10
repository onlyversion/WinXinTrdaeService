using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WcfInterface.model
{
    /// <summary>
    /// 系统角色
    /// </summary>  
    public class RoleEntity : EntityBase
    {
        /// <summary>
        /// 角色ID
        /// </summary>      
        public string RoleID
        { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>       
        public string RoleName
        { get; set; }
        /// <summary>
        /// 备注
        /// </summary>      
        public string Remark
        { get; set; }

    }
}
