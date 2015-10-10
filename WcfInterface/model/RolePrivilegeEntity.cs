using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 角色权限
    /// </summary>
   public class RolePrivilegeEntity
    {
       /// <summary>
        /// 角色
       /// </summary>
       public string RoleID
       {
           get;
           set;
       }
       /// <summary>
       /// 权限
       /// </summary>
       public string PrivilegeId
       {
           get;
           set;
       }
    }
}
