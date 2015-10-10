using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class VerifyCodeEntity:EntityBase
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string VierfyCode
        {
            get;
            set;
        }
    }
}
