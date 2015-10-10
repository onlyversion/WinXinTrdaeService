//*******************************************************************************
//  文 件 名：CTrade.ygj.cs
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
using WcfInterface;
using com.individual.helper;
using JinTong.Jyrj.Common;


namespace Trade
{
    public partial class CTrade : ITrade
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="LoginID">用户登陆标识ID</param>
        /// <param name="PwdType">0表示修改登陆密码,1表示修改资金密码</param>
        /// <param name="oldpwd">旧密码</param>
        /// <param name="newpwd">新密码</param>
        /// <returns></returns>
        public ResultDesc ModifyUserPassword(string LoginID, int PwdType, string oldpwd, string newpwd)
        {
            ResultDesc rsdc = new ResultDesc();
            try
            {
                TradeUser tdUser = new TradeUser();               

                List<string> sqlList = new List<string>();
               

                if (!ComFunction.ExistUserLoginID(LoginID, ref tdUser))
                {
                    rsdc.Result = false;
                    rsdc.ReturnCode = ResCode.UL003;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                //密码转换
                oldpwd = Des3.Des3EncodeCBC(oldpwd);
                newpwd = Des3.Des3EncodeCBC(newpwd);

                string sql1 = string.Empty;
                if (PwdType == 0)//登陆密码
                {
                    if (tdUser.LoginPwd == oldpwd)
                    {
                        sql1 = string.Format("UPDATE Base_User  SET  LoginPwd='{0}'  WHERE userid='{1}' AND LoginPwd='{2}'", newpwd, tdUser.UserID, oldpwd);
                        sqlList.Add(sql1);
                    }
                    else
                    {
                        rsdc.Result = false;
                        rsdc.Desc = "登陆密码不一致,修改失败";
                        return rsdc;
                    }
                }
                else//资金密码
                {
                    if (tdUser.CashPwd == oldpwd)
                    {
                        sql1 = string.Format("UPDATE Base_User  SET  CashPwd='{0}'  WHERE userid='{1}' AND CashPwd='{2}'", newpwd, tdUser.UserID, oldpwd);
                        sqlList.Add(sql1);
                    }
                    else
                    {
                        rsdc.Result = false;
                        rsdc.Desc = "资金密码不一致,修改失败";
                        return rsdc;
                    }
                }
                if (!ComFunction.SqlTransaction(sqlList))
                {
                    LogNet4.WriteMsg(string.Format("修改密码失败，SQL语句执行失败,SQL语句是:{0}", sql1));
                    rsdc.Result = false;
                    rsdc.Desc = "用户密码不对,修改密码失败";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "修改密码,成功";
               
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "修改密码,失败";
            }
            return rsdc;
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="LoginID">用户登陆标识ID</param>
        /// <param name="CardNum">证件号码</param>
        /// <param name="PwdType">0表示重置登陆密码,1表示重置资金密码</param>
        /// <param name="SendType">0表示通过短信告诉用户新的密码,1表示通过邮件告诉用户新的密码</param>
        /// <returns></returns>
        public ResultDesc ResetUserPassword(string LoginID, string CardNum, int PwdType, int SendType)
        {
            throw new NotImplementedException();
        }
    }
}
