using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 
    /// </summary>
   public class UpdateDataMember
    {
        /// <summary>
        /// 数据库字段映射名
        /// </summary>
        public string field { get; set; }

        /// <summary>
        /// 字段的值
        /// </summary>
        public object value { get; set; }
    }
}
