using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 返回
    /// </summary>
    public class CheckResult
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
        /// Gets or sets a value indicating whether
        /// 证件号码存在,返回true,否则返回false
        /// </summary>
        public bool CardNumExist
        {
            get;
            set;
        }
    }
}
