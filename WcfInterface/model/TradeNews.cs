using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    public class TradeNews
    {
        /// <summary>
        /// ID标识
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string NewsTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string NewsContent
        {
            get;
            set;
        }
        /// <summary>
        /// 发布人
        /// </summary>
        public string PubPerson
        {
            get;
            set;
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PubTime
        {
            get;
            set;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public NewsStatus Status
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets 新闻类型
        /// </summary>
        public NewsType NType
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets 摘要
        /// </summary>
        public string OverView
        {
            get;
            set;
        }
    }
}
