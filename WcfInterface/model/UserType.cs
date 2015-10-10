using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 用户类型:取值包括RootType AdminType OrgType NormalType
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        RootType =0,

        /// <summary>
        /// 管理员类型
        /// </summary>
        AdminType = 1,

        /// <summary>
        /// 组织类型
        /// </summary>
        OrgType = 2,

        /// <summary>
        /// 普通类型
        /// </summary>
        NormalType =3,
    }
}
