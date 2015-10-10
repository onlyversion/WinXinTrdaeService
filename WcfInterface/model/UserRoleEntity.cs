using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 用户角色
    /// </summary>
   public class UserRoleEntity:EntityBase
    {
       /// <summary>
       /// 用户ID
       /// </summary>
       public string UserId
       { get; set; }
       /// <summary>
       /// 角色ID
       /// </summary>
       public string RoleID
       { get; set; }
    }
}
