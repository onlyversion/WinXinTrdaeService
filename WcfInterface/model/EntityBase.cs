using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;

namespace WcfInterface.model
{
   /// <summary>
   /// 实体基类
   /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether 
        /// 结果(1成功 0失败)
        /// </summary>
        private bool _result;
        /// <summary>
        /// Gets or sets a value indicating whether 
        /// 结果(1成功 0失败)
        /// </summary>
        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }

        /// <summary>
        /// Gets or sets 描述
        /// </summary>
        private string _desc;
        /// <summary>
        /// Gets or sets 描述
        /// </summary>
        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }
        ///// <summary>
        ///// 数据源
        ///// </summary>
        //private DataTable _dataSource;
        //[DataMemberAttribute]
        //public DataTable DataSource
        //{
        //    get { return _dataSource; }
        //    set { _dataSource = value; }
        //}       
    }
}
