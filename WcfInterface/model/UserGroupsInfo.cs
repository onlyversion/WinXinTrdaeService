using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    public class UserGroupsInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether 
        /// 结果(1成功 0失败)
        /// </summary>
        public bool Result
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 描述
        /// </summary>
        public string Desc
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 商品信息表
        /// </summary>
        public List<UserGroups> UserGroupsInfoList
        {
            get;
            set;
        }
    }
}
