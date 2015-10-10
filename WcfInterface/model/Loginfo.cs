//*******************************************************************************
//  文 件 名：Loginfo.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/01
//
//  修改标识：
//  修改说明：
//*******************************************************************************

namespace WcfInterface.model
{
    /// <summary>
    /// 登陆接口 返回类型
    /// </summary>
    public class Loginfo
    {
        /// <summary>
        /// Gets or sets 登录标识
        /// </summary>
        public string LoginID
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 行情IP地址
        /// </summary>
        public string QuotesAddressIP
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 行情端口
        /// </summary>
        public int QuotesPort
        {
            set;
            get;
        }

        /// <summary>
        /// Gets or sets 用户信息
        /// </summary>
        public TradeUser TdUser
        {
            get;
            set;
        }       
    }

}
