using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.individual.helper;

namespace JinTong.Jyrj.Common
{
    /// <summary>
    /// 日志管理
    /// </summary>
  public  class ManagerLog
    {
        /// <summary>
        /// 写异常日志
        /// </summary>
        /// <param name="ex">异常</param>
        public static void WriteErr(Exception ex)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger("Exception");
            //log.Error(ex.Message, ex);
            LogNet4.WriteErr(ex);
        }
        public static void WriteErr(string message)
        {
            //log4net.ILog log = log4net.LogManager.GetLogger("Exception");
            //log.Error(message);
            LogNet4.WriteMsg(message);
        }
    }
}
