//*******************************************************************************
//  文 件 名：ITrade.ygj.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  ypa 
//  创建日期：2013/08/23
//
//  修改标识：
//  修改说明：
//*******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WcfInterface.model;
using System.ServiceModel;

namespace Trade
{
    public partial interface ITrade
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="LoginID">用户登陆标识ID</param>
        /// <param name="PwdType">0表示修改登陆密码,1表示修改资金密码</param>
        /// <param name="oldpwd">旧密码</param>
        /// <param name="newpwd">新密码</param>
        /// <returns></returns>
        [OperationContract]
        ResultDesc ModifyUserPassword(string LoginID, int PwdType, string oldpwd, string newpwd);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="LoginID">用户登陆标识ID</param>
        /// <param name="CardNum">证件号码</param>
        /// <param name="PwdType">0表示重置登陆密码,1表示重置资金密码</param>
        /// <param name="SendType">0表示通过短信告诉用户新的密码,1表示通过邮件告诉用户新的密码</param>
        /// <returns></returns>
        [OperationContract]
        ResultDesc ResetUserPassword(string LoginID, string CardNum, int PwdType, int SendType);
    }
}
