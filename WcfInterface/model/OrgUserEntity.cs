using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 组织机构-用户帐号
    /// </summary>
  public  class OrgUserEntity:EntityBase
    {
      /// <summary>
      /// 单位ID
      /// </summary>
      public string OrgID
      {
          get;
          set;
      }
      /// <summary>
      /// 用户帐户
      /// </summary>
      public string Account
      {
          get;
          set;
      }
      /// <summary>
      /// 状态
      /// </summary>
      public Status Status
      {
          get;
          set;
      }
    }
}
