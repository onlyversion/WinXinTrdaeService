using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WcfInterface.model
{
    public class UserLogEntity
    {
        /// <summary>
        /// OperTime
        /// </summary>
        public string OperTime
        {
            get;
            set;
        }
        /// <summary>
        /// UserID
        /// </summary>
        public string Account
        {
            get;
            set;
        }
        /// <summary>
        /// Remark
        /// </summary>
        public string DESC
        {
            get;
            set;
        }
        /// <summary>
        /// LogType
        /// </summary>
        public int UserType
        {
            get;
            set;
        }
    }
}
