using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model.WJY
{
    /// <summary>
    /// 体验券
    /// </summary>
    public class ExperienceInfo
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
        /// Gets or sets 体验券列表
        /// </summary>
        public List<Experience> ExperienceList
        {
            get;
            set;
        }
    }
}
