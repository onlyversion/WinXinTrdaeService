﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    /// <summary>
    /// 广告
    /// </summary>
    public class AdvertListInfo
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
        /// Gets or sets 返回代码
        /// </summary>
        public string ReturnCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets 节假日信息表
        /// </summary>
        public List<Advert> AdvertList
        {
            get;
            set;
        }
    }
}
