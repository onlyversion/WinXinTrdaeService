using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    public class HoldValidDate
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
        /// ObjValue>=-1
        /// -1表示客户可以通过时间选择控件选择有效期,但是选取的有效期必须>=当天的日期;
        /// 其他值表示客户不能选择,时间控件上默认的值=当天日期+ObjValue天。
        /// </summary>
        public int ObjValue
        {
            get;
            set;
        }
    }
}
