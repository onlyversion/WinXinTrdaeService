//*******************************************************************************
//  文 件 名：CManager.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/01
//
//  修改标识：
//  修改说明：
//*******************************************************************************

using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using System.Net;
using System.Net.Sockets;

using JinTong.Bzj.Common.Encrypt;
using WcfInterface;
using WcfInterface.model;
using com.individual.helper;
using WcfInterface.model.WJY;

namespace GssManager
{
    /// <summary>
    /// 后台管理类
    /// </summary>
    public partial class CManager : IManager
    {

        /// <summary>
        /// 验证客户证件号码(在启用状态的客户中进行检查)
        /// </summary>
        /// <param name="TradeAccount">要添加或修改的客户账号</param>
        /// <param name="CardNum">证件号码</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>验证结果</returns>
        public CheckResult CheckTradeUser(string TradeAccount, string CardNum, string LoginId)
        {
            CheckResult rsdc = new CheckResult();
            string operUser = string.Empty;//操作人
            try
            {

                if (string.IsNullOrEmpty(CardNum.Trim()))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "证件号码不能为空";
                    return rsdc;
                }
                //判断用户账号 或 证件号码是否已经被使用
                rsdc.CardNumExist = ComFunction.CardNumIsExist(TradeAccount, CardNum.Trim());
                rsdc.Result = true;
                rsdc.Desc = "检查证件号码成功";

            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "检查证件号码失败";
            }
            return rsdc;
        }
        /// <summary>
        /// 新增交易用户
        /// </summary>
        /// <param name="TdUser">用户信息</param>
        /// <param name="UType">用户类型</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>结果描述</returns>
        public ResultDesc AddTradeUser(TradeUser TdUser, UserType UType, string LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            TradeUser RefTdUser = new TradeUser();
            try
            {
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref RefTdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                #endregion

                if (UserType.NormalType == RefTdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = RefTdUser.Account;

                if (string.IsNullOrEmpty(TdUser.Account))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户账号不能为空";
                    return rsdc;
                }
                if (string.IsNullOrEmpty(TdUser.LoginPwd))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户密码不能为空";
                    return rsdc;
                }
                if (UserType.NormalType == UType && string.IsNullOrEmpty(TdUser.CashPwd))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "资金密码不能为空";
                    return rsdc;
                }
                if (string.IsNullOrEmpty(TdUser.CardNum))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "证件号码不能为空";
                    return rsdc;
                }
                if (string.IsNullOrEmpty(TdUser.UserID))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户ID不能为空";
                    return rsdc;
                }
                string OrgId = string.Empty;

                if (!string.IsNullOrEmpty(TdUser.OrgName))
                {
                    //判断组织ID是否存在
                    if (!ComFunction.ExistOrgName(TdUser.OrgName, ref OrgId))
                    {
                        rsdc.Result = false;
                        rsdc.Desc = "你所指定的组织名称不存在";
                        return rsdc;
                    }

                }

                bool tradeAccountExist = false;
                //判断用户账号 或 证件号码是否已经被使用
                if (ComFunction.CardNumIsExist(TdUser.Account, TdUser.CardNum, ref tradeAccountExist))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "证件号码已经被使用";
                    return rsdc;
                }
                if (tradeAccountExist)
                {
                    rsdc.Result = false;
                    rsdc.Desc = string.Format("账号{0}已经存在", TdUser.Account);
                    return rsdc;
                }

                if (UserType.OrgType == UType && !string.IsNullOrEmpty(OrgId) && !string.IsNullOrEmpty(TdUser.BindAccount))
                {
                    if (!ComFunction.ExistNormalAccount(TdUser.BindAccount, OrgId))
                    {
                        rsdc.Result = false;
                        rsdc.Desc = "你所指定的绑定帐号不存在";
                        return rsdc;
                    }
                }

                int ute = (int)UType;

                //SQL语句
                List<string> sqlList = new List<string>();

                StringBuilder strbld = new StringBuilder();
                //string userid = System.Guid.NewGuid().ToString().Replace("-", "");
                string strdt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                //TdUser.OrderUnit = 1;//手数单位默认为1
                if (UserType.NormalType == UType)
                {
                    //构造新增基本用户信息的sql语句
                    strbld.AppendFormat(@"insert into Base_User([userId],[userName],[status],[Accounttype],[CorporationName],
                        [Account],[LoginPwd],[cashPwd],[CardType],[CardNum],[OrgId],[PhoneNum],[TelNum],[Email],[LinkMan],[LinkAdress],
                        [OrderPhone],[sex],[OpenMan],[OpenTime],[LastUpdateTime],[LastUpdateID],[LoginID],[Ip],[Mac],[Online],[MinTrade],
                        [OrderUnit],[MaxTrade],[PermitRcash],[PermitCcash],[PermitDhuo],[PermitHshou],[PermitRstore],[PermitDelOrder],
                        [UserType]) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',", TdUser.UserID, string.IsNullOrEmpty(TdUser.UserName) ? string.Empty : TdUser.UserName,
                                                                                  TdUser.Status, TdUser.AccountType, string.IsNullOrEmpty(TdUser.CorporationName) ? string.Empty : TdUser.CorporationName, TdUser.Account, Des3.Des3EncodeCBC(TdUser.LoginPwd), Des3.Des3EncodeCBC(TdUser.CashPwd));
                    strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}',", TdUser.CardType, TdUser.CardNum, string.IsNullOrEmpty(OrgId) ? string.Empty : OrgId,
                        TdUser.PhoneNum, TdUser.TelNum, TdUser.Email, TdUser.LinkMan);
                    strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',", TdUser.LinkAdress, TdUser.OrderPhone, TdUser.Sex, TdUser.OpenMan, strdt, strdt, operUser, string.Empty);
                    strbld.AppendFormat("'{0}','{1}',{2},{3},{4},{5},", string.Empty, string.Empty, 0, TdUser.MinTrade, TdUser.OrderUnit, TdUser.MaxTrade);
                    strbld.AppendFormat("{0},{1},{2},{3},{4},{5},{6})", TdUser.PermitRcash ? 1 : 0, TdUser.PermitCcash ? 1 : 0, TdUser.PermitDhuo ? 1 : 0, TdUser.PermitHshou ? 1 : 0, TdUser.PermitRstore ? 1 : 0, TdUser.PermitDelOrder ? 1 : 0, ute);
                    sqlList.Add(strbld.ToString());

                    //构造新增用户资金信息的sql语句
                    sqlList.Add(string.Format("insert into Trade_FundInfo([userId],[state],[money],[OccMoney],[frozenMoney],[BankAccount],[AccountName],[BankCard],[CashUser],[SubUser],[ConBankType],[OpenBank],[OpenBankAddress],[SameBank]) values('{0}','{1}',{2},{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',{13})", TdUser.UserID, 0, 0, 0, 0, string.Empty, string.Empty, string.Empty, System.Guid.NewGuid().ToString().Replace("-", ""), string.Empty, string.Empty, string.Empty, string.Empty, 0));
                    //添加操作记录
                    sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)RefTdUser.UType, string.Format("新增账号{0}", TdUser.Account)));
                }
                else if (UserType.AdminType == UType)
                {
                    //构造新增管理员信息的sql语句
                    strbld.AppendFormat(@"insert into Base_User([userId],[userName],[status],[Accounttype],[Account],[LoginPwd],
                                        [CardType],[CardNum],[PhoneNum],[TelNum],[Email],[LinkAdress],[sex],[OpenTime],
                                        [LastUpdateTime],[LastUpdateID],[Online],[UserType]) values('{0}','{1}','{2}','{3}','{4}','{5}',",
                                         TdUser.UserID, string.IsNullOrEmpty(TdUser.UserName) ? string.Empty : TdUser.UserName,
                                         TdUser.Status, 0, TdUser.Account, Des3.Des3EncodeCBC(TdUser.LoginPwd));
                    strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}',",
                                        1, TdUser.CardNum, TdUser.PhoneNum, TdUser.TelNum, TdUser.Email);
                    strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}',{5},{6})",
                                        TdUser.LinkAdress, TdUser.Sex, strdt, strdt, operUser, 0, ute);
                    sqlList.Add(strbld.ToString());

                    //添加操作记录
                    sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)RefTdUser.UType, string.Format("新增管理员{0}", TdUser.Account)));
                }
                else if (UType == UserType.OrgType)
                {
                    //构造新增组织用户信息的sql语句
                    strbld.AppendFormat(@"insert into Base_User([userId],[userName],[status],[Accounttype],[Account],[LoginPwd],
                                        [CardType],[CardNum],[OrgId],[PhoneNum],[TelNum],[Email],[LinkAdress],[sex],[OpenTime],
                                        [LastUpdateTime],[LastUpdateID],[Online],[UserType],[BindAccount]) 
                                        values('{0}','{1}','{2}','{3}','{4}','{5}',",
                                        TdUser.UserID, string.IsNullOrEmpty(TdUser.UserName) ? string.Empty : TdUser.UserName,
                                        TdUser.Status, 1, TdUser.Account, Des3.Des3EncodeCBC(TdUser.LoginPwd));
                    strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}',", TdUser.CardType, TdUser.CardNum, string.IsNullOrEmpty(OrgId) ? string.Empty : OrgId,
                        TdUser.PhoneNum, TdUser.TelNum, TdUser.Email);
                    strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}',{5},{6},'{7}')", TdUser.LinkAdress, TdUser.Sex, strdt, strdt, operUser, 0, ute, string.IsNullOrEmpty(TdUser.BindAccount) ? string.Empty : TdUser.BindAccount);
                    sqlList.Add(strbld.ToString());

                    //添加操作记录
                    sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)RefTdUser.UType, string.Format("新增组织用户{0}", TdUser.Account)));

                }

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "新增用户出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "新增用户成功";

            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "新增用户失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 新增交易用户
        /// </summary>
        /// <param name="TdUser">用户信息</param>
        /// <param name="Fdinfo">资金信息</param>
        /// <param name="UType">用户类型</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>结果描述</returns>
        public ResultDesc AddTradeUserEx(TradeUser TdUser, Fundinfo Fdinfo, UserType UType, string LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            TradeUser RefTdUser = new TradeUser();
            try
            {
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref RefTdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                #endregion

                if (UserType.NormalType == RefTdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = RefTdUser.Account;

                if (string.IsNullOrEmpty(TdUser.Account))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户账号不能为空";
                    return rsdc;
                }
                if (string.IsNullOrEmpty(TdUser.LoginPwd))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户密码不能为空";
                    return rsdc;
                }
                if (UserType.NormalType == UType && string.IsNullOrEmpty(TdUser.CashPwd))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "资金密码不能为空";
                    return rsdc;
                }
                if (string.IsNullOrEmpty(TdUser.CardNum))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "证件号码不能为空";
                    return rsdc;
                }
                if (string.IsNullOrEmpty(TdUser.UserID))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户ID不能为空";
                    return rsdc;
                }
                string OrgId = string.Empty;

                if (!string.IsNullOrEmpty(TdUser.OrgName))
                {
                    //判断组织ID是否存在
                    if (!ComFunction.ExistOrgName(TdUser.OrgName, ref OrgId))
                    {
                        rsdc.Result = false;
                        rsdc.Desc = "你所指定的组织名称不存在";
                        return rsdc;
                    }

                }

                bool tradeAccountExist = false;
                //判断用户账号 或 证件号码是否已经被使用
                if (ComFunction.CardNumIsExist(TdUser.Account, TdUser.CardNum, ref tradeAccountExist))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "证件号码已经被使用";
                    return rsdc;
                }
                if (tradeAccountExist)
                {
                    rsdc.Result = false;
                    rsdc.Desc = string.Format("账号{0}已经存在", TdUser.Account);
                    return rsdc;
                }

                if (UserType.OrgType == UType && !string.IsNullOrEmpty(OrgId) && !string.IsNullOrEmpty(TdUser.BindAccount))
                {
                    if (!ComFunction.ExistNormalAccount(TdUser.BindAccount, OrgId))
                    {
                        rsdc.Result = false;
                        rsdc.Desc = "你所指定的绑定帐号不存在";
                        return rsdc;
                    }
                }

                int ute = (int)UType;

                //SQL语句
                List<string> sqlList = new List<string>();

                StringBuilder strbld = new StringBuilder();
                //string userid = System.Guid.NewGuid().ToString().Replace("-", "");
                string strdt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string ipmac = ComFunction.GetIpMac(RefTdUser.Ip, RefTdUser.Mac);
                //TdUser.OrderUnit = 1;//手数单位默认为1
                if (UserType.NormalType == UType)
                {
                    //构造新增基本用户信息的sql语句
                    strbld.AppendFormat(@"insert into Base_User([userId],[userName],[status],[Accounttype],[CorporationName],
                        [Account],[LoginPwd],[cashPwd],[CardType],[CardNum],[OrgId],[PhoneNum],[TelNum],[Email],[LinkMan],[LinkAdress],
                        [OrderPhone],[sex],[OpenMan],[OpenTime],[LastUpdateTime],[LastUpdateID],[LoginID],[Ip],[Mac],[Online],[MinTrade],
                        [OrderUnit],[MaxTrade],[PermitRcash],[PermitCcash],[PermitDhuo],[PermitHshou],[PermitRstore],[PermitDelOrder],
                        [UserType]) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',", TdUser.UserID, string.IsNullOrEmpty(TdUser.UserName) ? string.Empty : TdUser.UserName,
                                                                                  TdUser.Status, TdUser.AccountType, string.IsNullOrEmpty(TdUser.CorporationName) ? string.Empty : TdUser.CorporationName, TdUser.Account, Des3.Des3EncodeCBC(TdUser.LoginPwd), Des3.Des3EncodeCBC(TdUser.CashPwd));
                    strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}',", TdUser.CardType, TdUser.CardNum, string.IsNullOrEmpty(OrgId) ? string.Empty : OrgId,
                        TdUser.PhoneNum, TdUser.TelNum, TdUser.Email, TdUser.LinkMan);
                    strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',", TdUser.LinkAdress, TdUser.OrderPhone, TdUser.Sex, TdUser.OpenMan, strdt, strdt, operUser, string.Empty);
                    strbld.AppendFormat("'{0}','{1}',{2},{3},{4},{5},", string.Empty, string.Empty, 0, TdUser.MinTrade, TdUser.OrderUnit, TdUser.MaxTrade);
                    strbld.AppendFormat("{0},{1},{2},{3},{4},{5},{6})", TdUser.PermitRcash ? 1 : 0, TdUser.PermitCcash ? 1 : 0, TdUser.PermitDhuo ? 1 : 0, TdUser.PermitHshou ? 1 : 0, TdUser.PermitRstore ? 1 : 0, TdUser.PermitDelOrder ? 1 : 0, ute);
                    sqlList.Add(strbld.ToString());

                    //构造新增用户资金信息的sql语句
                    sqlList.Add(string.Format("insert into Trade_FundInfo([userId],[state],[money],[OccMoney],[frozenMoney],[BankAccount],[AccountName],[BankCard],[CashUser],[SubUser],[ConBankType],[OpenBank],[OpenBankAddress],[SameBank]) values('{0}','{1}',{2},{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',{13})",
                        TdUser.UserID, 0, 0, 0, 0, string.Empty, TdUser.UserName, Fdinfo.BankCard, System.Guid.NewGuid().ToString().Replace("-", ""), string.Empty, Fdinfo.ConBankType, Fdinfo.OpenBank, string.Empty, 0));
                    //添加默认分组
                    string UserGroupId = ComFunction.GetUserGroupId();
                    if (!string.IsNullOrEmpty(UserGroupId))
                    {
                        sqlList.Add(string.Format("insert into Trade_User_Group(UserId, UserGroupId) values('{0}','{1}') ", TdUser.UserID, UserGroupId));
                    }
                    //添加操作记录
                    sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)RefTdUser.UType, string.Format("{1}新增账号{0}", TdUser.Account, ipmac)));
                }
                else if (UserType.AdminType == UType)
                {
                    //构造新增管理员信息的sql语句
                    strbld.AppendFormat(@"insert into Base_User([userId],[userName],[status],[Accounttype],[Account],[LoginPwd],
                                        [CardType],[CardNum],[PhoneNum],[TelNum],[Email],[LinkAdress],[sex],[OpenTime],
                                        [LastUpdateTime],[LastUpdateID],[Online],[UserType]) values('{0}','{1}','{2}','{3}','{4}','{5}',",
                                         TdUser.UserID, string.IsNullOrEmpty(TdUser.UserName) ? string.Empty : TdUser.UserName,
                                         TdUser.Status, 0, TdUser.Account, Des3.Des3EncodeCBC(TdUser.LoginPwd));
                    strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}',",
                                        1, TdUser.CardNum, TdUser.PhoneNum, TdUser.TelNum, TdUser.Email);
                    strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}',{5},{6})",
                                        TdUser.LinkAdress, TdUser.Sex, strdt, strdt, operUser, 0, ute);
                    sqlList.Add(strbld.ToString());

                    //添加操作记录
                    sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)RefTdUser.UType, string.Format("{1}新增管理员{0}", TdUser.Account, ipmac)));
                }
                else if (UType == UserType.OrgType)
                {
                    //构造新增组织用户信息的sql语句
                    strbld.AppendFormat(@"insert into Base_User([userId],[userName],[status],[Accounttype],[Account],[LoginPwd],
                                        [CardType],[CardNum],[OrgId],[PhoneNum],[TelNum],[Email],[LinkAdress],[sex],[OpenTime],
                                        [LastUpdateTime],[LastUpdateID],[Online],[UserType],[BindAccount]) 
                                        values('{0}','{1}','{2}','{3}','{4}','{5}',",
                                        TdUser.UserID, string.IsNullOrEmpty(TdUser.UserName) ? string.Empty : TdUser.UserName,
                                        TdUser.Status, 1, TdUser.Account, Des3.Des3EncodeCBC(TdUser.LoginPwd));
                    strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}',", TdUser.CardType, TdUser.CardNum, string.IsNullOrEmpty(OrgId) ? string.Empty : OrgId,
                        TdUser.PhoneNum, TdUser.TelNum, TdUser.Email);
                    strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}',{5},{6},'{7}')", TdUser.LinkAdress, TdUser.Sex, strdt, strdt, operUser, 0, ute, string.IsNullOrEmpty(TdUser.BindAccount) ? string.Empty : TdUser.BindAccount);
                    sqlList.Add(strbld.ToString());

                    //添加操作记录
                    sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)RefTdUser.UType, string.Format("{1}新增组织用户{0}", TdUser.Account, ipmac)));

                }

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "新增用户出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "新增用户成功";

            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "新增用户失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 获取用户资金信息
        /// </summary>
        /// <param name="LoginId">登陆标识</param>
        /// <param name="TradeAccount">客户账号</param>
        /// <returns>用户资金信息</returns>
        public UserFundinfo GetUserFundinfo(string LoginId, string TradeAccount)
        {
            UserFundinfo userFundinfo = new UserFundinfo();
            TradeUser TdUser = new TradeUser();
            try
            {
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    userFundinfo.Result = false;
                    userFundinfo.Desc = ResCode.UL003Desc;
                    return userFundinfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    userFundinfo.Result = false;
                    userFundinfo.Desc = ComFunction.NotRightUser;
                    return userFundinfo;
                }

                #endregion
                userFundinfo.Fdinfo = ComFunction.GetFdinfo(string.Format("select * from Trade_FundInfo where userid='{0}' and state<>'4'", ComFunction.GetUserId(TradeAccount)));
                userFundinfo.Result = true;
                userFundinfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                userFundinfo.Result = false;
                userFundinfo.Desc = "查询成功";
            }
            return userFundinfo;
        }

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="TradeAccount">要删除的客户账号</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>删除结果</returns>
        public ResultDesc DelTradeUser(string TradeAccount, string LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            TradeUser RefTdUser = new TradeUser();
            try
            {
                if ("ROOT" == TradeAccount.ToUpper() || "ADMIN" == TradeAccount.ToUpper())
                {
                    rsdc.Result = false;
                    rsdc.Desc = "此管理员不能被删除";
                    return rsdc;
                }

                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref RefTdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                #endregion

                if (UserType.NormalType == RefTdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = RefTdUser.Account;
                if ("ROOT" != operUser.ToUpper())
                {
                    rsdc.Result = false;
                    rsdc.Desc = "您没有删除权限";
                    return rsdc;
                }
                string userid = ComFunction.GetUserId(TradeAccount);
                if (string.IsNullOrEmpty(userid))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "此账号不存在";
                    return rsdc;
                }
                if (ComFunction.UserExistOrderAndHoldOrder(userid))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "此用户存在有效订单或挂单,不能删除";
                    return rsdc;
                }
                //获取现有资金信息
                Fundinfo Fdinfo = ComFunction.GetFdinfo(string.Format("select * from Trade_FundInfo where userid='{0}' and state<>'4'", userid));
                if (Fdinfo.Money > ComFunction.dzero)
                {
                    rsdc.Result = false;
                    rsdc.Desc = "此用户帐户余额不为零,不能删除";
                    return rsdc;
                }
                List<string> sqlList = new List<string>();
                string ipmac = ComFunction.GetIpMac(RefTdUser.Ip, RefTdUser.Mac);
                sqlList.Add(string.Format("delete from base_user where userid='{0}'", userid));
                sqlList.Add(string.Format("delete from trade_fundinfo where userid='{0}'", userid));
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)RefTdUser.UType, string.Format("{1}删除账号:{0}", TradeAccount, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "删除账号出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "删除账号成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "删除账号失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 修改客户资料
        /// </summary>
        /// <param name="TdUser">用户信息</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>修改结果</returns>
        public ResultDesc ModifyTradeUser(TradeUser TdUser, string LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            TradeUser RefTdUser = new TradeUser();
            try
            {
                #region 判断登陆标识是否过期
                if (!ComFunction.ExistUserLoginID(LoginId, ref RefTdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }


                if (UserType.NormalType == RefTdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = RefTdUser.Account;
                #endregion

                if (string.IsNullOrEmpty(TdUser.Account))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户账号不能为空";
                    return rsdc;
                }
                if (string.IsNullOrEmpty(TdUser.LoginPwd))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户密码不能为空";
                    return rsdc;
                }

                if (string.IsNullOrEmpty(TdUser.CardNum))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "证件号码不能为空";
                    return rsdc;
                }

                string OrgId = string.Empty;
                if (!string.IsNullOrEmpty(TdUser.OrgName))
                {
                    //判断组织ID是否存在
                    if (!ComFunction.ExistOrgName(TdUser.OrgName, ref OrgId))
                    {
                        rsdc.Result = false;
                        rsdc.Desc = "你所指定的组织名称不存在";
                        return rsdc;
                    }
                }
                //判断用户输入的证件号码在其他有效用户中是否已经被使用
                if (ComFunction.CardNumIsExist(TdUser.Account, TdUser.CardNum))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "证件号码已经被使用";
                    return rsdc;
                }
                UserType Utype = ComFunction.GetUserType(TdUser.Account);
                if (UserType.OrgType == Utype && !string.IsNullOrEmpty(OrgId) && !string.IsNullOrEmpty(TdUser.BindAccount))
                {
                    if (!ComFunction.ExistNormalAccount(TdUser.BindAccount, OrgId))
                    {
                        rsdc.Result = false;
                        rsdc.Desc = "你所指定的绑定帐号不存在";
                        return rsdc;
                    }
                }
                if (UserType.NormalType == Utype && string.IsNullOrEmpty(TdUser.CashPwd))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "资金密码不能为空";
                    return rsdc;
                }
                //SQL语句
                List<string> sqlList = new List<string>();
                string userid = ComFunction.GetUserId(TdUser.Account);
                string strdt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //构造修改用户信息的sql语句
                StringBuilder strbld = new StringBuilder();
                //TdUser.OrderUnit = 1;//手数单位默认为1
                strbld.AppendFormat("update Base_User set ");

                strbld.AppendFormat("UserName='{0}',", string.IsNullOrEmpty(TdUser.UserName) ? string.Empty : TdUser.UserName);

                strbld.AppendFormat("CardNum='{0}',", string.IsNullOrEmpty(TdUser.CardNum) ? string.Empty : TdUser.CardNum);
                strbld.AppendFormat("OrgId='{0}',", string.IsNullOrEmpty(OrgId) ? string.Empty : OrgId);
                strbld.AppendFormat("PhoneNum='{0}',", string.IsNullOrEmpty(TdUser.PhoneNum) ? string.Empty : TdUser.PhoneNum);
                strbld.AppendFormat("TelNum='{0}',", string.IsNullOrEmpty(TdUser.TelNum) ? string.Empty : TdUser.TelNum);
                strbld.AppendFormat("Email='{0}',", string.IsNullOrEmpty(TdUser.Email) ? string.Empty : TdUser.Email);
                strbld.AppendFormat("LinkMan='{0}',", string.IsNullOrEmpty(TdUser.LinkMan) ? string.Empty : TdUser.LinkMan);
                strbld.AppendFormat("LinkAdress='{0}',", string.IsNullOrEmpty(TdUser.LinkAdress) ? string.Empty : TdUser.LinkAdress);
                strbld.AppendFormat("Sex='{0}',", string.IsNullOrEmpty(TdUser.Sex) ? "1" : TdUser.Sex);
                strbld.AppendFormat("[Status]='{0}',", string.IsNullOrEmpty(TdUser.Status) ? "0" : TdUser.Status);
                strbld.AppendFormat("LoginPwd='{0}',", Des3.Des3EncodeCBC(string.IsNullOrEmpty(TdUser.LoginPwd) ? "123456" : TdUser.LoginPwd));
                if (Utype == UserType.OrgType)
                {
                    strbld.AppendFormat("CardType='{0}',", string.IsNullOrEmpty(TdUser.CardType) ? "1" : TdUser.CardType);
                    strbld.AppendFormat("BindAccount='{0}',", string.IsNullOrEmpty(TdUser.BindAccount) ? string.Empty : TdUser.BindAccount);
                }
                else if (Utype == UserType.NormalType)
                {
                    strbld.AppendFormat("CardType='{0}',", string.IsNullOrEmpty(TdUser.CardType) ? "1" : TdUser.CardType);
                    strbld.AppendFormat("AccountType='{0}',", string.IsNullOrEmpty(TdUser.AccountType) ? "0" : TdUser.AccountType);
                    strbld.AppendFormat("OpenMan='{0}',", string.IsNullOrEmpty(TdUser.OpenMan) ? string.Empty : TdUser.OpenMan);
                    strbld.AppendFormat("OrderPhone='{0}',", string.IsNullOrEmpty(TdUser.OrderPhone) ? string.Empty : TdUser.OrderPhone);
                    strbld.AppendFormat("CashPwd='{0}',", Des3.Des3EncodeCBC(string.IsNullOrEmpty(TdUser.CashPwd) ? "123456" : TdUser.CashPwd));

                    strbld.AppendFormat("MinTrade={0},", TdUser.MinTrade);
                    strbld.AppendFormat("OrderUnit={0},", TdUser.OrderUnit);
                    strbld.AppendFormat("MaxTrade={0},", TdUser.MaxTrade);
                    strbld.AppendFormat("CorporationName='{0}',", string.IsNullOrEmpty(TdUser.CorporationName) ? string.Empty : TdUser.CorporationName);

                    strbld.AppendFormat("PermitRcash={0},", TdUser.PermitRcash ? 1 : 0);
                    strbld.AppendFormat("PermitCcash={0},", TdUser.PermitCcash ? 1 : 0);
                    strbld.AppendFormat("PermitDhuo={0},", TdUser.PermitDhuo ? 1 : 0);
                    strbld.AppendFormat("PermitHshou={0},", TdUser.PermitHshou ? 1 : 0);
                    strbld.AppendFormat("PermitRstore={0},", TdUser.PermitRstore ? 1 : 0);
                    strbld.AppendFormat("PermitDelOrder={0},", TdUser.PermitDelOrder ? 1 : 0);
                }
                strbld.AppendFormat("LastUpdateTime='{0}',", strdt); //修改时间
                strbld.AppendFormat("LastUpdateID='{0}'", operUser); //修改人
                strbld.AppendFormat(" where Account='{0}' and userid='{1}'", TdUser.Account, userid);
                sqlList.Add(strbld.ToString());
                //com.individual.helper.LogNet4.WriteMsg(strbld.ToString());
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)RefTdUser.UType, string.Format("修改用户{0}", TdUser.Account)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改用户资料出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "修改用户资料成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "修改用户资料失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 修改客户资料
        /// </summary>
        /// <param name="TdUser">用户信息</param>
        /// <param name="Fdinfo">资金信息</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>修改结果</returns>
        public ResultDesc ModifyTradeUserEx(TradeUser TdUser, Fundinfo Fdinfo, string LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            TradeUser RefTdUser = new TradeUser();
            try
            {
                #region 判断登陆标识是否过期
                if (!ComFunction.ExistUserLoginID(LoginId, ref RefTdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }


                if (UserType.NormalType == RefTdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = RefTdUser.Account;
                #endregion

                if (string.IsNullOrEmpty(TdUser.Account))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户账号不能为空";
                    return rsdc;
                }
                if (string.IsNullOrEmpty(TdUser.LoginPwd))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户密码不能为空";
                    return rsdc;
                }

                //if (string.IsNullOrEmpty(TdUser.CardNum))
                //{
                //    rsdc.Result = false;
                //    rsdc.Desc = "证件号码不能为空";
                //    return rsdc;
                //}

                string OrgId = string.Empty;
                if (!string.IsNullOrEmpty(TdUser.OrgName))
                {
                    //判断组织ID是否存在
                    if (!ComFunction.ExistOrgName(TdUser.OrgName, ref OrgId))
                    {
                        rsdc.Result = false;
                        rsdc.Desc = "你所指定的组织名称不存在";
                        return rsdc;
                    }
                }
                //判断用户输入的证件号码在其他有效用户中是否已经被使用
                //if (ComFunction.CardNumIsExist(TdUser.Account, TdUser.CardNum))
                //{
                //    rsdc.Result = false;
                //    rsdc.Desc = "证件号码已经被使用";
                //    return rsdc;
                //}
                UserType Utype = ComFunction.GetUserType(TdUser.Account);
                //if (UserType.OrgType == Utype && !string.IsNullOrEmpty(OrgId) && !string.IsNullOrEmpty(TdUser.BindAccount))
                //{
                //    if (!ComFunction.ExistNormalAccount(TdUser.BindAccount, OrgId))
                //    {
                //        rsdc.Result = false;
                //        rsdc.Desc = "你所指定的绑定帐号不存在";
                //        return rsdc;
                //    }
                //}
                //if (UserType.NormalType == Utype && string.IsNullOrEmpty(TdUser.CashPwd))
                //{
                //    rsdc.Result = false;
                //    rsdc.Desc = "资金密码不能为空";
                //    return rsdc;
                //}
                //SQL语句
                List<string> sqlList = new List<string>();
                string userid = ComFunction.GetUserId(TdUser.Account);
                string strdt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //构造修改用户信息的sql语句
                StringBuilder strbld = new StringBuilder();
                string ipmac = ComFunction.GetIpMac(RefTdUser.Ip, RefTdUser.Mac);
                //TdUser.OrderUnit = 1;//手数单位默认为1
                strbld.AppendFormat("update Base_User set ");

                strbld.AppendFormat("UserName='{0}',", string.IsNullOrEmpty(TdUser.UserName) ? string.Empty : TdUser.UserName);

                strbld.AppendFormat("CardNum='{0}',", string.IsNullOrEmpty(TdUser.CardNum) ? string.Empty : TdUser.CardNum);
                strbld.AppendFormat("OrgId='{0}',", string.IsNullOrEmpty(OrgId) ? string.Empty : OrgId);
                strbld.AppendFormat("PhoneNum='{0}',", string.IsNullOrEmpty(TdUser.PhoneNum) ? string.Empty : TdUser.PhoneNum);
                strbld.AppendFormat("TelNum='{0}',", string.IsNullOrEmpty(TdUser.TelNum) ? string.Empty : TdUser.TelNum);
                strbld.AppendFormat("Email='{0}',", string.IsNullOrEmpty(TdUser.Email) ? string.Empty : TdUser.Email);
                strbld.AppendFormat("LinkMan='{0}',", string.IsNullOrEmpty(TdUser.LinkMan) ? string.Empty : TdUser.LinkMan);
                strbld.AppendFormat("LinkAdress='{0}',", string.IsNullOrEmpty(TdUser.LinkAdress) ? string.Empty : TdUser.LinkAdress);
                strbld.AppendFormat("Sex='{0}',", string.IsNullOrEmpty(TdUser.Sex) ? "1" : TdUser.Sex);
                strbld.AppendFormat("[Status]='{0}',", string.IsNullOrEmpty(TdUser.Status) ? "0" : TdUser.Status);
                strbld.AppendFormat("LoginPwd='{0}',", Des3.Des3EncodeCBC(string.IsNullOrEmpty(TdUser.LoginPwd) ? "123456" : TdUser.LoginPwd));
                if (Utype == UserType.OrgType)
                {
                    strbld.AppendFormat("CardType='{0}',", string.IsNullOrEmpty(TdUser.CardType) ? "1" : TdUser.CardType);
                    strbld.AppendFormat("BindAccount='{0}',", string.IsNullOrEmpty(TdUser.BindAccount) ? string.Empty : TdUser.BindAccount);
                }
                else if (Utype == UserType.NormalType)
                {
                    strbld.AppendFormat("CardType='{0}',", string.IsNullOrEmpty(TdUser.CardType) ? "1" : TdUser.CardType);
                    strbld.AppendFormat("AccountType='{0}',", string.IsNullOrEmpty(TdUser.AccountType) ? "0" : TdUser.AccountType);
                    strbld.AppendFormat("OpenMan='{0}',", string.IsNullOrEmpty(TdUser.OpenMan) ? string.Empty : TdUser.OpenMan);
                    strbld.AppendFormat("OrderPhone='{0}',", string.IsNullOrEmpty(TdUser.OrderPhone) ? string.Empty : TdUser.OrderPhone);
                    strbld.AppendFormat("CashPwd='{0}',", Des3.Des3EncodeCBC(string.IsNullOrEmpty(TdUser.CashPwd) ? "123456" : TdUser.CashPwd));

                    strbld.AppendFormat("MinTrade={0},", TdUser.MinTrade);
                    strbld.AppendFormat("OrderUnit={0},", TdUser.OrderUnit);
                    strbld.AppendFormat("MaxTrade={0},", TdUser.MaxTrade);
                    strbld.AppendFormat("CorporationName='{0}',", string.IsNullOrEmpty(TdUser.CorporationName) ? string.Empty : TdUser.CorporationName);

                    strbld.AppendFormat("PermitRcash={0},", TdUser.PermitRcash ? 1 : 0);
                    strbld.AppendFormat("PermitCcash={0},", TdUser.PermitCcash ? 1 : 0);
                    strbld.AppendFormat("PermitDhuo={0},", TdUser.PermitDhuo ? 1 : 0);
                    strbld.AppendFormat("PermitHshou={0},", TdUser.PermitHshou ? 1 : 0);
                    strbld.AppendFormat("PermitRstore={0},", TdUser.PermitRstore ? 1 : 0);
                    strbld.AppendFormat("PermitDelOrder={0},", TdUser.PermitDelOrder ? 1 : 0);
                }
                strbld.AppendFormat("LastUpdateTime='{0}',", strdt); //修改时间
                strbld.AppendFormat("LastUpdateID='{0}'", operUser); //修改人
                strbld.AppendFormat(" where Account='{0}' and userid='{1}'", TdUser.Account, userid);
                sqlList.Add(strbld.ToString());
                //修改资金信息
                sqlList.Add(string.Format("update Trade_FundInfo set AccountName='{0}',BankCard='{1}',OpenBank='{2}',ConBankType='{4}' where userid='{3}'",
                    TdUser.UserName, Fdinfo.BankCard, Fdinfo.OpenBank, userid, Fdinfo.ConBankType));
                //com.individual.helper.LogNet4.WriteMsg(strbld.ToString());
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)RefTdUser.UType, string.Format("{1}修改用户{0}", TdUser.Account, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改用户资料出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "修改用户资料成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "修改用户资料失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 客户资金资料修改
        /// </summary>
        /// <param name="Fdinfo">资金信息</param>
        /// <param name="tradeAccount">用户帐号</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>修改结果</returns>
        public ResultDesc ModifyUserFundinfo(Fundinfo Fdinfo, string tradeAccount, string LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            TradeUser RefTdUser = new TradeUser();
            try
            {
                #region 判断登陆标识是否过期
                if (!ComFunction.ExistUserLoginID(LoginId, ref RefTdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }


                if (UserType.NormalType == RefTdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = RefTdUser.Account;
                #endregion

                //SQL语句
                List<string> sqlList = new List<string>();
                string userid = ComFunction.GetUserId(tradeAccount);
                string strdt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //构造修改用户信息的sql语句
                StringBuilder strbld = new StringBuilder();
                strbld.AppendFormat("update Trade_FundInfo set ");

                //strbld.AppendFormat("ConBankType='{0}', ", string.IsNullOrEmpty(Fdinfo.ConBankType) ? string.Empty : Fdinfo.ConBankType);

                //strbld.AppendFormat("OpenBank='{0}', ", string.IsNullOrEmpty(Fdinfo.OpenBank) ? string.Empty : Fdinfo.OpenBank);
                //strbld.AppendFormat("BankAccount='{0}', ", string.IsNullOrEmpty(Fdinfo.BankAccount) ? string.Empty : Fdinfo.BankAccount);
                //strbld.AppendFormat("BankCard='{0}', ", string.IsNullOrEmpty(Fdinfo.BankCard) ? string.Empty : Fdinfo.BankCard);
                //strbld.AppendFormat("AccountName='{0}', ", string.IsNullOrEmpty(Fdinfo.AccountName) ? string.Empty : Fdinfo.AccountName);
                //strbld.AppendFormat("OpenBankAddress='{0}' ", string.IsNullOrEmpty(Fdinfo.OpenBankAddress) ? string.Empty : Fdinfo.OpenBankAddress);
                strbld.AppendFormat("DongJieMoney={0} ", Fdinfo.DongJieMoney);

                strbld.AppendFormat(" where userid='{0}' and [state]<>'4'", userid);
                sqlList.Add(strbld.ToString());
                //添加操作记录
                string ipmac = ComFunction.GetIpMac(RefTdUser.Ip, RefTdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)RefTdUser.UType,
                    string.Format("{2}修改{0}的冻结资金为{1}", tradeAccount, Fdinfo.DongJieMoney, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改用户资金资料出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "修改用户资金资料成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "修改用户资金资料失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 管理员登陆
        /// </summary>
        /// <param name="AdminId">管理员帐号</param>
        /// <param name="Password">管理员密码</param>
        /// <param name="mac">MAC地址</param>
        /// <returns>管理员权限</returns>
        public ReAdminAuth AdminLogin(string AdminId, string Password, string mac)
        {
            ReAdminAuth reAdminAuth = new ReAdminAuth();
            try
            {
                Password = Des3.Des3EncodeCBC(Password);
                string UserId = string.Empty;
                //判断管理是否存在
                if (!ComFunction.ExistAdmin(AdminId, Password, ref UserId))
                {
                    reAdminAuth.LoginID = "-1";
                    reAdminAuth.Desc = "账号不存在";
                    return reAdminAuth;
                }
                if ("root" == AdminId) //root帐户 固定登陆标识为20ec93a65639481a98043668090c2ffc 因为root帐户不需要做登陆过期的判断 支持多个用户同时使用root登陆
                {
                    reAdminAuth.LoginID = "20ec93a65639481a98043668090c2ffc";
                }
                else
                {
                    //更新登陆标识 并返回权限
                    reAdminAuth.LoginID = System.Guid.NewGuid().ToString().Replace("-", "");
                }
                string sql = "update Base_User set LoginID=@LoginID where Account=@tradeAccount and LoginPwd=@tradePwd and Status='1' and usertype<>3";

                DbHelper.ExecuteSql(sql,
                     new System.Data.Common.DbParameter[]{
                       DbHelper.CreateDbParameter(DataBaseType.SqlServer,"@LoginID",DbParameterType.String,reAdminAuth.LoginID,ParameterDirection.Input),
                       DbHelper.CreateDbParameter(DataBaseType.SqlServer,"@tradeAccount",DbParameterType.String,AdminId,ParameterDirection.Input),
                       DbHelper.CreateDbParameter(DataBaseType.SqlServer,"@tradePwd",DbParameterType.String,Password,ParameterDirection.Input),
                   });
                reAdminAuth.QuotesAddressIP = ComFunction.ip;
                reAdminAuth.QuotesPort = Convert.ToInt32(ComFunction.port);
                reAdminAuth.UserID = UserId;
                reAdminAuth.Desc = "登陆成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                reAdminAuth.LoginID = "-1";
                reAdminAuth.Desc = "登陆出错";
            }
            return reAdminAuth;
        }


        /// <summary>
        /// 获取交易日信息
        /// </summary>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>交易日信息</returns>
        public DateSetInfo GetDateSetInfo(string LoginId)
        {
            DateSetInfo dateSetInfo = new DateSetInfo();
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    dateSetInfo.Result = false;
                    dateSetInfo.Desc = ResCode.UL003Desc;
                    return dateSetInfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    dateSetInfo.Result = false;
                    dateSetInfo.Desc = ComFunction.NotRightUser;
                    return dateSetInfo;
                }
                #endregion
                dateSetInfo.DtSetInfoList = ComFunction.GetDateSetList("select * from Trade_DateSet");
                dateSetInfo.Result = true;
                dateSetInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                dateSetInfo.Result = false;
                dateSetInfo.Desc = "查询交易日失败";
            }
            return dateSetInfo;
        }

        /// <summary>
        /// 修改交易日
        /// </summary>
        /// <param name="DtSet">交易日信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        public ResultDesc ModifyDateSet(DateSet DtSet, string LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;
                #endregion
                List<string> sqlList = new List<string>();
                StringBuilder strbld = new StringBuilder();
                strbld.AppendFormat("update Trade_DateSet set ");

                strbld.AppendFormat("Istrade={0},", DtSet.Istrade ? 0 : 1);
                strbld.AppendFormat("Starttime='{0}',", DtSet.Starttime);
                strbld.AppendFormat("Endtime='{0}',", DtSet.Endtime);
                strbld.AppendFormat("[Desc]='{0}'", DtSet.Desc);

                strbld.AppendFormat(" where Weekday='{0}'", DtSet.Weekday);
                sqlList.Add(strbld.ToString());

                //添加操作记录

                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType,
                    string.Format("修改交易日{0},{1}-{2}", DtSet.Weekday, DtSet.Starttime, DtSet.Endtime)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改交易日出错";
                    return rsdc;
                }
                if ("1" == ComFunction.InterType)
                {
                    ComFunction.ModifyDateSet(DtSet);
                }
                rsdc.Result = true;
                rsdc.Desc = "修该交易日成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "修改交易日失败";
            }
            return rsdc;
        }


        /// <summary>
        /// 获取交易日信息
        /// </summary>
        /// <param name="pricecode">行情编码</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>交易日信息</returns>
        public DateSetInfo GetDateSetInfoEx(string pricecode, string LoginId)
        {
            DateSetInfo dateSetInfo = new DateSetInfo();
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    dateSetInfo.Result = false;
                    dateSetInfo.Desc = ResCode.UL003Desc;
                    return dateSetInfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    dateSetInfo.Result = false;
                    dateSetInfo.Desc = ComFunction.NotRightUser;
                    return dateSetInfo;
                }
                #endregion
                dateSetInfo.DtSetInfoList = ComFunction.GetDateSetListEx(string.Format("select * from Trade_DateSetEx where pricecode='{0}'", pricecode));
                dateSetInfo.Result = true;
                dateSetInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                dateSetInfo.Result = false;
                dateSetInfo.Desc = "查询交易日失败";
            }
            return dateSetInfo;
        }

        /// <summary>
        /// 修改交易日
        /// </summary>
        /// <param name="DtSet">交易日信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        public ResultDesc ModifyDateSetEx(DateSet DtSet, string LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;
                #endregion
                List<string> sqlList = new List<string>();
                StringBuilder strbld = new StringBuilder();
                strbld.AppendFormat("update Trade_DateSetEx set ");

                strbld.AppendFormat("Istrade={0},", DtSet.Istrade ? 0 : 1);
                strbld.AppendFormat("Starttime='{0}',", DtSet.Starttime);
                strbld.AppendFormat("Endtime='{0}',", DtSet.Endtime);
                strbld.AppendFormat("[Desc]='{0}'", DtSet.Desc);

                strbld.AppendFormat(" where pricecode='{0}' and Weekday='{1}'", DtSet.PriceCode, DtSet.Weekday);
                sqlList.Add(strbld.ToString());

                //添加操作记录
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType,
                    string.Format("{4}修改交易日{0}-{1},{2}-{3}", DtSet.PriceCode, DtSet.Weekday, DtSet.Starttime, DtSet.Endtime, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改交易日出错";
                    return rsdc;
                }
                if ("1" == ComFunction.InterType)
                {
                    ComFunction.ModifyDateSetEx(DtSet);
                }
                rsdc.Result = true;
                rsdc.Desc = "修该交易日成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "修改交易日失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 获取节假日信息
        /// </summary>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>节假日信息</returns>
        public DateHolidayInfo GetHolidayInfo(string LoginId)
        {
            DateHolidayInfo dateHolidayInfo = new DateHolidayInfo();
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    dateHolidayInfo.Result = false;
                    dateHolidayInfo.Desc = ResCode.UL003Desc;
                    return dateHolidayInfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    dateHolidayInfo.Result = false;
                    dateHolidayInfo.Desc = ComFunction.NotRightUser;
                    return dateHolidayInfo;
                }
                #endregion
                dateHolidayInfo.DtHolidayInfoList = ComFunction.GetDateHolidayList("select * from Trade_HolidayEx");
                dateHolidayInfo.Result = true;
                dateHolidayInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                dateHolidayInfo.Result = false;
                dateHolidayInfo.Desc = "查询节假日失败";
            }
            return dateHolidayInfo;
        }

        /// <summary>
        /// 修改节假日
        /// </summary>
        /// <param name="Hliday">节假日信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        public ResultDesc ModifyHoliday(DateHoliday Hliday, string LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;
                #endregion
                List<string> sqlList = new List<string>();
                StringBuilder strbld = new StringBuilder();
                strbld.AppendFormat("update Trade_HolidayEx set ");

                strbld.AppendFormat("Starttime='{0}',", Hliday.Starttime);
                strbld.AppendFormat("Endtime='{0}',", Hliday.Endtime);
                strbld.AppendFormat("[Desc]='{0}',", Hliday.Desc);
                strbld.AppendFormat("HoliName='{0}',", Hliday.HoliName);
                strbld.AppendFormat("pricecode='{0}' ", Hliday.PriceCode);
                strbld.AppendFormat(" where ID='{0}'", Hliday.ID);
                sqlList.Add(strbld.ToString());
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType,
                    string.Format("{3}修改节假日:{0},{1}-{2}", Hliday.ID, Hliday.Starttime, Hliday.Endtime, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改节假日出错";
                    return rsdc;
                }
                if ("1" == ComFunction.InterType)
                {
                    ComFunction.ModifyHoliday(Hliday);
                }
                rsdc.Result = true;
                //rsdc.Desc = "修该节假日成功";
                rsdc.Desc = strbld.ToString();
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "修改节假日失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 添加节假日
        /// </summary>
        /// <param name="Hliday">节假日信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <param name="ID">返回ID</param>
        /// <returns>添加结果</returns>
        public ResultDesc AddHoliday(DateHoliday Hliday, string LoginId, ref string ID)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();
                ID = System.Guid.NewGuid().ToString().Replace("-", "");
                sqlList.Add(string.Format("insert into Trade_HolidayEx([pricecode],[HoliName],[starttime],[endtime],[desc],[ID]) values('{0}','{1}','{2}','{3}','{4}','{5}')",
                   Hliday.PriceCode, Hliday.HoliName, Hliday.Starttime.ToString("yyyy-MM-dd HH:mm:ss"), Hliday.Endtime.ToString("yyyy-MM-dd HH:mm:ss"), Hliday.Desc, ID));
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType,
                    string.Format("{3}添加节假日:{0},{1}-{2}", Hliday.ID, Hliday.Starttime, Hliday.Endtime, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "添加节假日出错";
                    return rsdc;
                }
                if ("1" == ComFunction.InterType)
                {
                    Hliday.ID = ID;
                    ComFunction.AddHoliday(Hliday);
                }
                rsdc.Result = true;
                rsdc.Desc = "添加节假日成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "添加节假日失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 删除节假日
        /// </summary>
        /// <param name="ID">要删除的节假日名称</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>删除结果</returns>
        public ResultDesc DelHoliday(string ID, string LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();

                sqlList.Add(string.Format("delete from Trade_HolidayEx where ID='{0}'", ID));
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}删除节假日:{0}", ID, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "删除节假日出错";
                    return rsdc;
                }
                if ("1" == ComFunction.InterType)
                {
                    ComFunction.DelHoliday(ID);
                }
                rsdc.Result = true;
                rsdc.Desc = "删除节假日成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "删除节假日失败";
            }
            return rsdc;
        }


        /// <summary>
        /// 修改交易设置
        /// </summary>
        /// <param name="TdSet">交易设置</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        public ResultDesc ModifyTradeSet(TradeSet TdSet, string LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;
                #endregion

                #region 验证数据
                if (!ComFunction.ValidateTradeSet(TdSet))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "值不正确";
                    return rsdc;
                }
                #endregion

                List<string> sqlList = new List<string>();
                StringBuilder strbld = new StringBuilder();
                strbld.AppendFormat("update Trade_Set set ");

                strbld.AppendFormat("ObjName='{0}',", TdSet.ObjName);
                strbld.AppendFormat("ObjValue='{0}',", TdSet.ObjValue);
                strbld.AppendFormat("Remark='{0}' ", TdSet.Remark);
                strbld.AppendFormat("where ObjCode='{0}'", TdSet.ObjCode);
                sqlList.Add(strbld.ToString());
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}修改交易设置,{0}", TdSet.ObjCode, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改交易设置出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "修该交易设置成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "修改交易设置失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 添加交易设置
        /// </summary>
        /// <param name="TdSet">交易设置信息</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>添加结果</returns>
        public ResultDesc AddTradeSet(TradeSet TdSet, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();
                sqlList.Add(string.Format("insert into Trade_Set([ObjName],[ObjCode],[ObjValue],[Remark]) values('{0}','{1}','{2}','{3}')", TdSet.ObjName, TdSet.ObjCode, TdSet.ObjValue, TdSet.Remark));

                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}添加交易设置:{0}", TdSet.ObjCode, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "添加交易设置出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "添加交易设置成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "添加交易设置失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 删除交易设置
        /// </summary>
        /// <param name="ObjCode">名称编码</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>删除结果</returns>
        public ResultDesc DelTradeSet(string ObjCode, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();

                sqlList.Add(string.Format("delete from Trade_Set where ObjCode='{0}'", ObjCode));
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}删除交易设置:{0}", ObjCode, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "删除交易设置出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "删除交易设置成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "删除交易设置失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 新闻公告添加
        /// </summary>
        /// <param name="TdNews">新闻</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>添加结果</returns>
        public ResultDesc AddTradeNews(TradeNews TdNews, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion

                if (string.IsNullOrEmpty(TdNews.ID))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "ID不能为空";
                    return rsdc;
                }
                if (string.IsNullOrEmpty(TdNews.NewsTitle))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "标题不能为空";
                    return rsdc;
                }
                List<string> sqlList = new List<string>();
                sqlList.Add(string.Format("insert into Trade_News([ID],[NewsTitle],[NewsContent],[PubPerson],[PubTime],[Status],[NewsType],[OverView]) values('{0}','{1}','{2}','{3}','{4}',{5},{6},'{7}')", TdNews.ID, TdNews.NewsTitle, TdNews.NewsContent, TdNews.PubPerson, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (int)TdNews.Status, (int)TdNews.NType, string.IsNullOrEmpty(TdNews.OverView) ? string.Empty : TdNews.OverView));
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}添加新闻公告:{0}", TdNews.NewsTitle, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "添加新闻公告出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "添加新闻公告成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "添加新闻公告失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 新闻公告修改
        /// </summary>
        /// <param name="TdNews">新闻</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>修改结果</returns>
        public ResultDesc ModifyTradeNews(TradeNews TdNews, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;
                #endregion
                List<string> sqlList = new List<string>();
                StringBuilder strbld = new StringBuilder();
                strbld.AppendFormat("update Trade_News set ");

                strbld.AppendFormat("NewsContent='{0}',", TdNews.NewsContent);
                strbld.AppendFormat("NewsType={0},", (int)TdNews.NType);
                strbld.AppendFormat("Status={0},", (int)TdNews.Status);
                //  strbld.AppendFormat("PubTime='{0}',", TdNews.PubTime.ToString("yyyy-MM-dd"));
                strbld.AppendFormat("PubPerson='{0}',", TdNews.PubPerson);
                strbld.AppendFormat("OverView='{0}',", string.IsNullOrEmpty(TdNews.OverView) ? string.Empty : TdNews.OverView);

                strbld.AppendFormat("NewsTitle='{0}' ", TdNews.NewsTitle);
                strbld.AppendFormat(" where ID='{0}'", TdNews.ID);
                sqlList.Add(strbld.ToString());
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}修改新闻公告,{0}", TdNews.ID, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改新闻公告出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "修该新闻公告成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "修改新闻公告失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 读取单条新闻
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns></returns>
        public DataTable GetNewsInfo(string id)
        {
            return ComFunction.GetNewsInfo(id);
        }
        /// <summary>
        /// 新闻公告删除
        /// </summary>
        /// <param name="Id">ID标识</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>删除结果</returns>
        public ResultDesc DelTradeNews(string Id, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();

                sqlList.Add(string.Format("delete from Trade_News where ID='{0}'", Id));
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}删除新闻公告:{0}", Id, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "删除新闻公告出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "删除新闻公告成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "删除新闻公告失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 获取过滤IP
        /// </summary>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>过滤IP信息</returns>
        public TradeIpInfo GetTradeIpInfo(String LoginId)
        {
            TradeIpInfo tradeIpInfo = new TradeIpInfo();
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    tradeIpInfo.Result = false;
                    tradeIpInfo.Desc = ResCode.UL003Desc;
                    return tradeIpInfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    tradeIpInfo.Result = false;
                    tradeIpInfo.Desc = ComFunction.NotRightUser;
                    return tradeIpInfo;
                }
                #endregion
                tradeIpInfo.TradeIpInfoList = ComFunction.GetTradeIpList("select * from Trade_IP");
                tradeIpInfo.Result = true;
                tradeIpInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                tradeIpInfo.Result = false;
                tradeIpInfo.Desc = "查询过滤IP失败";
            }
            return tradeIpInfo;
        }

        /// <summary>
        /// 修改过滤IP
        /// </summary>
        /// <param name="TdIp">过滤IP信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        public ResultDesc ModifyTradeIp(TradeIp TdIp, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();
                StringBuilder strbld = new StringBuilder();
                strbld.AppendFormat("update Trade_IP set ");

                strbld.AppendFormat("StartIp='{0}',", TdIp.StartIp);
                strbld.AppendFormat("EndIp='{0}',", TdIp.EndIp);
                strbld.AppendFormat("[desc]='{0}'", TdIp.Desc);
                strbld.AppendFormat(" where ID='{0}'", TdIp.ID);
                sqlList.Add(strbld.ToString());
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}修改过滤IP:{0}", TdIp.ID, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改过滤IP出错";
                    return rsdc;
                }
                rsdc.Result = true;
                //rsdc.Desc = "修该过滤IP成功";
                rsdc.Desc = strbld.ToString();
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "修改过滤IP失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 添加过滤IP
        /// </summary>
        /// <param name="TdIp">过滤IP信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <param name="ID">返回ID</param>
        /// <returns>添加结果</returns>
        public ResultDesc AddTradeIp(TradeIp TdIp, String LoginId, ref string ID)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();
                ID = System.Guid.NewGuid().ToString().Replace("-", "");
                sqlList.Add(string.Format("insert into Trade_IP([startip],[endip],[desc],[ID]) values('{0}','{1}','{2}','{3}')", TdIp.StartIp, TdIp.EndIp, TdIp.Desc, ID));
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}添加过滤IP:{0}", TdIp.ID, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "添加过滤IP出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "添加过滤IP成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "添加过滤IP失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 删除过滤IP
        /// </summary>
        /// <param name="ID">ID标识唯一记录</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>删除结果</returns>
        public ResultDesc DelTradeIp(string ID, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();
                sqlList.Add(string.Format("delete from Trade_IP where ID='{0}'", ID));
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}删除过滤IP:{0}", ID, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "删除过滤IP出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "删除过滤IP成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "删除过滤IP失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 获取过滤Mac
        /// </summary>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>过滤Mac信息</returns>
        public TradeMacInfo GetTradeMacInfo(String LoginId)
        {
            TradeMacInfo tradeMacInfo = new TradeMacInfo();
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    tradeMacInfo.Result = false;
                    tradeMacInfo.Desc = ResCode.UL003Desc;
                    return tradeMacInfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    tradeMacInfo.Result = false;
                    tradeMacInfo.Desc = ComFunction.NotRightUser;
                    return tradeMacInfo;
                }
                #endregion
                tradeMacInfo.TradeMacInfoList = ComFunction.GetTradeMacList("select * from Trade_Mac");
                tradeMacInfo.Result = true;
                tradeMacInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                tradeMacInfo.Result = false;
                tradeMacInfo.Desc = "查询过滤Mac失败";
            }
            return tradeMacInfo;
        }

        /// <summary>
        /// 修改过滤Mac
        /// </summary>
        /// <param name="TdMac">过滤Mac信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        public ResultDesc ModifyTradeMac(TradeMac TdMac, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();
                StringBuilder strbld = new StringBuilder();
                strbld.AppendFormat("update Trade_Mac set ");

                strbld.AppendFormat("Mac='{0}',", TdMac.MAC);
                strbld.AppendFormat("[desc]='{0}'", TdMac.Desc);
                strbld.AppendFormat(" where ID='{0}'", TdMac.ID);
                sqlList.Add(strbld.ToString());

                //添加操作记录
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}修改过滤Mac:{0}", TdMac.ID, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改过滤Mac出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "修该过滤Mac成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "修改过滤Mac失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 添加过滤Mac
        /// </summary>
        /// <param name="TdMac">过滤Mac信息</param>
        /// <param name="LoginId">管理员登陆标识</param>\
        /// <param name="ID">返回ID</param>
        /// <returns>添加结果</returns>
        public ResultDesc AddTradeMac(TradeMac TdMac, String LoginId, ref string ID)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();
                ID = System.Guid.NewGuid().ToString().Replace("-", "");
                sqlList.Add(string.Format("insert into Trade_Mac([mac],[desc],[ID]) values('{0}','{1}','{2}')", TdMac.MAC, TdMac.Desc, ID));
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}添加过滤Mac:{0}", TdMac.ID, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "添加过滤Mac出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "添加过滤Mac成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "添加过滤Mac失败";

            }
            return rsdc;
        }

        /// <summary>
        /// 删除过滤Mac
        /// </summary>
        /// <param name="ID">要删除的过滤Mac名称</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>删除结果</returns>
        public ResultDesc DelTradeMac(string ID, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();
                sqlList.Add(string.Format("delete from Trade_Mac where ID='{0}'", ID));
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}删除过滤Mac:{0}", ID, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "删除过滤Mac出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "删除过滤Mac成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "删除过滤Mac失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>商品信息信息</returns>
        public TradeProductInfo GetTradeProductInfo(String LoginId)
        {
            TradeProductInfo TradeProductInfo = new TradeProductInfo();
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    TradeProductInfo.Result = false;
                    TradeProductInfo.Desc = ResCode.UL003Desc;
                    return TradeProductInfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    TradeProductInfo.Result = false;
                    TradeProductInfo.Desc = ComFunction.NotRightUser;
                    return TradeProductInfo;
                }
                #endregion

                TradeProductInfo.TradeProductInfoList = ComFunction.GetTradeProductList(ComFunction.GetProductSql);
                TradeProductInfo.Result = true;
                TradeProductInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                TradeProductInfo.Result = false;
                TradeProductInfo.Desc = "查询商品信息失败";
            }
            return TradeProductInfo;
        }

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="TdProduct">商品信息信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        public ResultDesc ModifyTradeProduct(TradeProduct TdProduct, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();
                StringBuilder strbld = new StringBuilder();
                strbld.AppendFormat("update Trade_Product set ");
                strbld.AppendFormat("ProductName='{0}',", TdProduct.ProductName);
                strbld.AppendFormat("Adjustbase={0},", TdProduct.Adjustbase);
                //strbld.AppendFormat("Adjustcount={0},", TdProduct.Adjustcount);
                strbld.AppendFormat("Expressionfee='{0}',", TdProduct.Expressionfee);
                strbld.AppendFormat("Holdbase={0},", TdProduct.Holdbase);
                strbld.AppendFormat("Lowerprice={0},", TdProduct.Lowerprice);
                strbld.AppendFormat("Maxprice={0},", TdProduct.Maxprice);
                strbld.AppendFormat("Maxtime={0},", TdProduct.Maxtime);
                strbld.AppendFormat("Minprice={0},", TdProduct.Minprice);
                //strbld.AppendFormat("Ordemoney={0},", TdProduct.Ordemoney);
                strbld.AppendFormat("Ordemoneyfee='{0}',", TdProduct.Ordemoneyfee);
                strbld.AppendFormat("Pricedot={0},", TdProduct.Pricedot);
                //strbld.AppendFormat("Sellfee='{0}',", TdProduct.Sellfee);
                strbld.AppendFormat("Sellstoragefee='{0}',", TdProduct.Sellstoragefee);
                strbld.AppendFormat("SetBase={0},", TdProduct.SetBase);
                strbld.AppendFormat("State='{0}',", TdProduct.State);
                strbld.AppendFormat("Unit={0},", TdProduct.Unit);
                strbld.AppendFormat("Valuedot={0},", TdProduct.Valuedot);
                strbld.AppendFormat("Buystoragefee='{0}',", TdProduct.Buystoragefee);
                strbld.AppendFormat("Pricecode='{0}',", TdProduct.Pricecode);
                //strbld.AppendFormat("Goodscode='{0}',", TdProduct.Goodscode);
                strbld.AppendFormat("starttime='{0}',", TdProduct.Starttime);
                strbld.AppendFormat("endtime='{0}'", TdProduct.Endtime);
                strbld.AppendFormat(" where Productcode='{0}'", TdProduct.Productcode);
                sqlList.Add(strbld.ToString());
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}修改商品信息:{0}", TdProduct.ProductName, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改商品信息出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "修该商品信息成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "修改商品信息失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 添加商品信息
        /// </summary>
        /// <param name="TdProduct">商品信息信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>添加结果</returns>
        public ResultDesc AddTradeProduct(TradeProduct TdProduct, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();
                TradeDataSource tdsource = ComFunction.GetTradeDataSource(TdProduct.Pricecode);
                //Goodscode用Pricecode赋值
                sqlList.Add(string.Format("insert into Trade_Product([ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime]) values('{0}','{1}','{2}','{3}',{4},{5},{6},{7},{8},{9},'{10}','{11}',{12},{13},{14},'{15}',{16},'{17}','{18}','{19}',{20},{21},'{22}','{23}')",
                    TdProduct.ProductName, TdProduct.Productcode, TdProduct.Pricecode, TdProduct.Pricecode, TdProduct.Adjustbase, tdsource.adjustcount,
                    TdProduct.Pricedot, TdProduct.Valuedot, TdProduct.SetBase, TdProduct.Holdbase, TdProduct.Expressionfee, TdProduct.Sellfee, TdProduct.Maxprice,
                    TdProduct.Minprice, TdProduct.Maxtime, TdProduct.Ordemoneyfee, TdProduct.Ordemoney, TdProduct.Buystoragefee, TdProduct.Sellstoragefee,
                    TdProduct.State, TdProduct.Unit, TdProduct.Lowerprice, TdProduct.Starttime, TdProduct.Endtime));
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}添加商品信息:{0}", TdProduct.ProductName, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "添加商品信息出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "添加商品信息成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "添加商品信息失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 删除商品信息
        /// </summary>
        /// <param name="ProductCode">要删除的商品编码</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>删除结果</returns>
        public ResultDesc DelTradeProduct(string ProductCode, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;
                #endregion
                List<string> sqlList = new List<string>();
                sqlList.Add(string.Format("delete from Trade_Product where ProductCode='{0}'", ProductCode));
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}删除商品信息:{0}", ProductCode, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "删除商品信息出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "删除商品信息成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "删除商品信息失败";
            }
            return rsdc;
        }

        /// <summary>ModifyTradeUserEx(
        /// 获取汇率和水
        /// </summary>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>汇率和水信息</returns>
        public TradeRateInfo GetTradeRateInfo(String LoginId)
        {
            TradeRateInfo TradeRateInfo = new TradeRateInfo();
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    TradeRateInfo.Result = false;
                    TradeRateInfo.Desc = ResCode.UL003Desc;
                    return TradeRateInfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    TradeRateInfo.Result = false;
                    TradeRateInfo.Desc = ComFunction.NotRightUser;
                    return TradeRateInfo;
                }
                #endregion
                TradeRateInfo.TradeRateInfoList = ComFunction.GetTradeRateList();
                TradeRateInfo.Result = true;
                TradeRateInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                TradeRateInfo.Result = false;
                TradeRateInfo.Desc = "查询汇率和水失败";
            }
            return TradeRateInfo;
        }

        /// <summary>
        /// 修改汇率和水
        /// </summary>
        /// <param name="TdRate">汇率和水信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        public ResultDesc ModifyTradeRate(TradeRate TdRate, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;
                #endregion
                List<string> sqlList = new List<string>();

                int result = ComFunction.ModifyWaterAndRate(TdRate);

                //添加操作记录
                if (1 == result)
                {
                    string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                    sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}修改汇率和水:{0}", TdRate.PriceCode, ipmac)));

                    if (!ComFunction.SqlTransaction(sqlList))
                    {
                        rsdc.Result = false;
                        rsdc.Desc = "修改汇率和水出错1";
                        return rsdc;
                    }
                    rsdc.Result = true;
                    rsdc.Desc = "修该汇率和水成功";
                }
                else
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改汇率和水出错2";
                    return rsdc;
                }
            }
            catch (Exception ex)
            {
                rsdc.Result = false;
                rsdc.Desc = "修改汇率和水失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 获取交易对冲信息
        /// </summary>
        /// <param name="LoginId">登陆标识</param>
        /// <param name="Starttime">开始时间</param>
        /// <param name="Endtime">结束时间</param>
        /// <returns>交易对冲信息</returns>
        public HedgingInfo GetHedgingInfo(string LoginId, DateTime Starttime, DateTime Endtime)
        {
            HedgingInfo hedgingInfo = new HedgingInfo();
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    hedgingInfo.Result = false;
                    hedgingInfo.Desc = ResCode.UL003Desc;
                    return hedgingInfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    hedgingInfo.Result = false;
                    hedgingInfo.Desc = ComFunction.NotRightUser;
                    return hedgingInfo;
                }

                #endregion

                hedgingInfo.HedgingList = new List<Hedging>();
                ComFunction.GetHedgingList(string.Format("where ordertime>='{0}' and ordertime<='{1}'",
                                            Starttime.ToString("yyyy-MM-dd HH:mm:ss"), Endtime.ToString("yyyy-MM-dd HH:mm:ss")), ref hedgingInfo);
                hedgingInfo.Result = true;
                hedgingInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                hedgingInfo.Result = false;
                hedgingInfo.Desc = "查询交易对冲信息失败";
            }
            return hedgingInfo;
        }


        /// <summary>
        /// 手工调账或库存结算
        /// </summary>
        /// <param name="LoginId">登陆标识</param>
        /// <param name="TradeAccount">用户交易账号</param>
        /// <param name="money">调整资金</param>
        /// <param name="ReasonType">原因(3-库存结算,6-手工调账)</param>
        /// <returns>调整结果</returns>
        public ResultDesc ModifyUserMoney(string LoginId, string TradeAccount, double money, int ReasonType)
        {
            //ReasonType字段说明:(该接口取3,6)
            //0----入金(组织或管理员操作);
            //1----出金(组织或管理员操作);
            //2----订单操作
            //3----库存结算(组织或管理员操作);
            //4----银行入金 (用户操作);
            //5----银行出金 (用户操作)
            //6----手工调账(组织或管理员操作)
            //7----在线回购(用户操作);
            //8----其他费用;

            ResultDesc rsdc = new ResultDesc();
            string userid = string.Empty; //用户ID
            string operUser = string.Empty; //操作人

            TradeUser TdUser = new TradeUser();
            try
            {
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;
                #endregion

                userid = ComFunction.GetUserId(TradeAccount);

                //获取现有资金信息
                Fundinfo Fdinfo = ComFunction.GetFdinfo(string.Format("select * from Trade_FundInfo where userid='{0}' and state<>'4'", userid));

                if (Fdinfo.Money + money < 0)
                {
                    rsdc.Result = false;
                    rsdc.Desc = string.Format("{0}失败!因为用户{1}现有余额为{2},调整金额为{3},调整后的金额小于0", 3 == ReasonType ? "库存结算" : "手工调账", TradeAccount, Fdinfo.Money, money);
                    return rsdc;
                }

                #region 数据库事务处理

                //SQL语句
                List<string> sqlList = new List<string>();

                //添加调整资金sql语句
                sqlList.Add(string.Format("update Trade_FundInfo set money=money+{0} where userid='{1}' and state<>'4'", money, userid));

                //添加资金变动记录sql语句
                sqlList.Add(string.Format("insert into Fund_Change([userId],[reason],[Oldvalue],[NewValue],[OperUser],[OperTime],[RelaOrder],[ChangeValue],[CashUser]) values('{0}','{1}',{2},{3},'{4}','{5}','{6}',{7},'{8}')", userid, ReasonType, Fdinfo.Money, Fdinfo.Money + money, operUser, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), string.Empty, money, Fdinfo.CashUser));

                string operrationDesc = string.Empty;
                if (3 == ReasonType)
                {
                    operrationDesc = string.Format("对用户{0}，库存结算{1}", TradeAccount, money);
                }
                else if (6 == ReasonType)
                {
                    operrationDesc = string.Format("对用户{0}，手工调帐{1}", TradeAccount, money);
                }
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, ipmac + operrationDesc));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "调整资金出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "调整资金成功";
                #endregion
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "调整资金失败";
            }
            return rsdc;
        }


        /// <summary>
        /// 导出报表
        /// </summary>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="reporttype">报表类型(0有效订单,1限价挂单,2入库单,3平仓,4资金变动,5账户结余)</param>
        /// <param name="LoginID">管理员或组织登陆标识</param>
        /// <returns>报表地址</returns>
        public ReportForms GetReportForms(DateTime starttime, DateTime endtime, int reporttype, string LoginID)
        {
            string reportAddr = string.Empty;
            ReportForms forms = new ReportForms();
            string rfile = string.Empty;
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginID, ref TdUser))
                {
                    forms.Result = false;
                    forms.Desc = ResCode.UL003Desc;
                    return forms;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    forms.Result = false;
                    forms.Desc = ComFunction.NotRightUser;
                    return forms;
                }
                #endregion
                if (!(reporttype >= 0 && reporttype <= 10))
                {
                    forms.Result = false;
                    forms.Desc = "报表类型错误";
                    return forms;
                }

                DateTime dt = DateTime.Now;

                switch (reporttype) //确定导出文件名称
                {
                    case 0: rfile = string.Format("Y{0}.xls", dt.ToString("yyyyMMddHHmmssfff"));
                        break;
                    case 1: rfile = string.Format("X{0}.xls", dt.ToString("yyyyMMddHHmmssfff"));
                        break;
                    case 2: rfile = string.Format("R{0}.xls", dt.ToString("yyyyMMddHHmmssfff"));
                        break;
                    case 3: rfile = string.Format("T{0}.xls", dt.ToString("yyyyMMddHHmmssfff"));
                        break;
                    case 4: rfile = string.Format("Z{0}.xls", dt.ToString("yyyyMMddHHmmssfff"));
                        break;
                    case 5: rfile = string.Format("M{0}.xls", dt.ToString("yyyyMMddHHmmssfff"));
                        break;
                    default: rfile = string.Format("B{0}.xls", dt.ToString("yyyyMMddHHmmssfff"));
                        break;
                }

                System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@Result", DbParameterType.String, 1, ParameterDirection.Output);

                if (UserType.OrgType == TdUser.UType && !string.IsNullOrEmpty(TdUser.OrgId))
                {
                    DbHelper.RunProcedureExecuteSql("P_ReportExportExcelAgent",
                       new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@BeginDate",DbParameterType.DateTime,starttime,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@EndDate",DbParameterType.DateTime,endtime,ParameterDirection.Input),
                    DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@Agentid",DbParameterType.String,TdUser.OrgId,ParameterDirection.Input), 
                       DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ReportType",DbParameterType.Int,reporttype,ParameterDirection.Input),
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ReportDir",DbParameterType.String,ComFunction.ReportFilePath + "\\" + dt.ToString("yyyyMM"),ParameterDirection.Input),
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ReportFile",DbParameterType.String,rfile,ParameterDirection.Input),
                            OutputParam});
                }
                else
                {
                    string path = ComFunction.ReportFilePath + "\\" + dt.ToString("yyyyMM");
                    DbHelper.RunProcedureExecuteSql("P_ReportExportExcel",
                        new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@BeginDate",DbParameterType.DateTime,starttime,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@EndDate",DbParameterType.DateTime,endtime,ParameterDirection.Input),
                       DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ReportType",DbParameterType.Int,reporttype,ParameterDirection.Input), 
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ReportDir",DbParameterType.String,path,ParameterDirection.Input),
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ReportFile",DbParameterType.String,rfile,ParameterDirection.Input),
                            OutputParam});
                }


                if (0 == Convert.ToInt32(OutputParam.Value))
                {
                    forms.Result = true;
                    forms.Desc = "报表导出成功";
                    forms.ReportAddr = ComFunction.ReportAddr + dt.ToString("yyyyMM") + "/" + rfile; //URL地址
                }
                else
                {
                    forms.Result = false;
                    forms.Desc = "报表导出失败";
                    forms.ReportAddr = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                forms.Result = false;
                forms.Desc = "报表导出错误";
                forms.ReportAddr = string.Empty;
            }
            return forms;
        }

        /// <summary>
        /// 导出报表(用户数据)
        /// </summary>
        /// <param name="TradeAccount">客户账号</param>
        /// <param name="UserName">客户姓名</param>
        /// <param name="TelPhone">手机号码</param>
        /// <param name="Broker">经纪人</param>
        /// <param name="orgid">会员名称</param>
        /// <param name="IsBroker">是否经纪人</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="reporttype">报表类型</param>
        /// <param name="LoginID">登陆标识</param>
        /// <returns>报表地址</returns>
        public ReportForms GetReportFormsUser(string TradeAccount,string UserName,string TelPhone,string Broker,string orgid,string IsBroker,
            DateTime starttime, DateTime endtime, int reporttype, string LoginID)
        {
            ReportForms forms = new ReportForms();
            string rfile = string.Empty;
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginID, ref TdUser))
                {
                    forms.Result = false;
                    forms.Desc = ResCode.UL003Desc;
                    return forms;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    forms.Result = false;
                    forms.Desc = ComFunction.NotRightUser;
                    return forms;
                }
                string LoginOrgID = "";
                if (UserType.OrgType == TdUser.UType && !string.IsNullOrEmpty(TdUser.OrgId))
                {
                    //PartSearchCondition = " and orgid in (select orgid from #tmp) ";
                    LoginOrgID = TdUser.OrgId;
                }
                #endregion
                if (!(reporttype >= 0 && reporttype <= 11))
                {
                    forms.Result = false;
                    forms.Desc = "报表类型错误";
                    return forms;
                }

                DateTime dt = DateTime.Now;

                switch (reporttype) //确定导出文件名称
                {
                    case 11: rfile = string.Format("U{0}.xls", dt.ToString("yyyyMMddHHmmssfff"));
                        break;
                }

                System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@Result", DbParameterType.String, 1, ParameterDirection.Output);
      
                string path = ComFunction.ReportFilePath + "\\" + dt.ToString("yyyyMM");
                DbHelper.RunProcedureExecuteSql("P_ReportExportExcelUser",
                    new System.Data.Common.DbParameter[]{
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@LoginOrgID",DbParameterType.String,LoginOrgID,ParameterDirection.Input),
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TradeAccount",DbParameterType.String,TradeAccount,ParameterDirection.Input),
                     DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@UserName",DbParameterType.String,UserName,ParameterDirection.Input),
                     DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TelPhone",DbParameterType.String,TelPhone,ParameterDirection.Input),
                     DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@Broker",DbParameterType.String,Broker,ParameterDirection.Input),
                     DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@orgid",DbParameterType.String,orgid,ParameterDirection.Input),
                     DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@IsBroker",DbParameterType.String,IsBroker,ParameterDirection.Input),



                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@BeginDate",DbParameterType.DateTime,starttime,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@EndDate",DbParameterType.DateTime,endtime,ParameterDirection.Input),
                       DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ReportType",DbParameterType.Int,reporttype,ParameterDirection.Input), 
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ReportDir",DbParameterType.String,path,ParameterDirection.Input),
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ReportFile",DbParameterType.String,rfile,ParameterDirection.Input),
                            OutputParam});


                if (0 == Convert.ToInt32(OutputParam.Value))
                {
                    forms.Result = true;
                    forms.Desc = "报表导出成功";
                    forms.ReportAddr = ComFunction.ReportAddr + dt.ToString("yyyyMM") + "/" + rfile; //URL地址
                }
                else
                {
                    forms.Result = false;
                    forms.Desc = "报表导出失败";
                    forms.ReportAddr = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                forms.Result = false;
                forms.Desc = "报表导出错误";
                forms.ReportAddr = string.Empty;
            }
            return forms;
        }


        /// <summary>
        /// 有效订单分页查询
        /// </summary>
        /// <param name="Cxqc">查询条条</param>
        /// <param name="pageindex">第几页,从1开始</param>
        /// <param name="pagesize">每页多少条</param>
        /// <param name="page">输出参数(总页数)</param>
        /// <returns>订单信息</returns>
        public TradeOrderInfo GetMultiTradeOrderWithPage(CxQueryCon Cxqc, int pageindex, int pagesize, ref int page)
        {
            TradeOrderInfo TdOrderInfo = new TradeOrderInfo();

            System.Data.Common.DbDataReader dbreader = null;
            System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                 "@PageCount", DbParameterType.Int, 0, ParameterDirection.Output);
            string SearchCondition = string.Empty;
            try
            {
                string AndStr = string.Empty;
                string PartSearchCondition = string.Empty;
                string ParentOrgID = string.Empty;
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期


                if (!ComFunction.ExistUserLoginID(Cxqc.LoginID, ref TdUser))
                {
                    TdOrderInfo.Result = false;
                    TdOrderInfo.Desc = ResCode.UL003Desc;
                    return TdOrderInfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    TdOrderInfo.Result = false;
                    TdOrderInfo.Desc = ComFunction.NotRightUser;
                    return TdOrderInfo;
                }

                #endregion

                if (!string.IsNullOrEmpty(Cxqc.TradeAccount)) //交易账号不为空 表示查询该用户的历史单 否则查询所有用户的历史单
                {
                    AndStr = string.Format(" and [Account] like '{0}%' ", Cxqc.TradeAccount);
                }
                string productname = string.Empty;
                if ("ALL" == Cxqc.ProductName.ToUpper())
                {
                    productname = string.Empty;
                }
                else
                {
                    productname = string.Format(" and ProductName='{0}'", Cxqc.ProductName);
                }

                if (!string.IsNullOrEmpty(Cxqc.OrgName))
                {
                    //AndStr += string.Format("and [orgname] like '{0}%' ", Cxqc.OrgName);
                    AndStr += string.Format("and [orgid]='{0}' ", Cxqc.OrgName);
                }

                if (UserType.OrgType == TdUser.UType && !string.IsNullOrEmpty(TdUser.OrgId))
                {
                    //AndStr += string.Format(" and [orgid] in ({0}) ", ComFunction.GetOrgIds(TdUser.OrgId));
                    PartSearchCondition = " and orgid in (select orgid from #tmp) ";
                    ParentOrgID = TdUser.OrgId;
                }
                if ("ALL" != Cxqc.OrderType.ToUpper())
                {
                    AndStr += string.Format(" and ordertype='{0}'", Cxqc.OrderType);
                }

                if (!string.IsNullOrEmpty(Cxqc.PriceCode))
                {
                    AndStr += string.Format(" and [PriceCode]='{0}' ", Cxqc.PriceCode);
                }
                //else
                //{
                //    com.individual.helper.LogNet4.WriteMsg("行情编码未知!");
                //}
                string SumSelectList = "round(isnull(sum(OccMoney),0),2) as OccMoney,round(isnull(sum(usequantity),0),2) as quantity,round(isnull(sum(tradefee),0),2) as tradefee,round(isnull(sum(storagefee),0),2) as storagefee  ";
                string selectlist = "orderunit,orgname,telephone,username,TotalWeight,account,ProductName,productcode,Orderid,quantity,usequantity,Orderprice,profitPrice,lossPrice,OccMoney,tradefee,storagefee,Ordertime,Ordertype,AllowStore ";


                SearchCondition = string.Format("where ordertime >= '{0}' and ordertime <='{1}' {2} {3} {4}",
                    Cxqc.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), Cxqc.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    productname, AndStr, PartSearchCondition);

                #region 获取行情价格
                string data = ComFunction.GetProgramData();
                if (string.IsNullOrEmpty(data))
                {
                    TdOrderInfo.Desc = "查询失败!!";
                    TdOrderInfo.Result = false;
                    return TdOrderInfo;
                }
                #endregion

                dbreader = DbHelper.RunProcedureGetDataReader("GetRecordFromPageExWithSumEx2",
                      new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@selectlist",DbParameterType.String,selectlist,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SumSelectList",DbParameterType.String,SumSelectList,ParameterDirection.Input),
                     DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                        "@data",DbParameterType.String,data,ParameterDirection.Input), //参数格式[行情编码,价格|行情编码,价格|行情编码,价格|行情编码,价格|行情编码,价格]
                       DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableSource",DbParameterType.String,"V_Trade_Order ",ParameterDirection.Input), //表名或视图表 
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableOrder",DbParameterType.String,"a",ParameterDirection.Input), //排序后的表名称 即子查询结果集的别名
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SearchCondition",DbParameterType.String,SearchCondition,ParameterDirection.Input),
                          DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@OrderExpression",DbParameterType.String,"order by ordertime desc",ParameterDirection.Input),//排序 表达式
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ParentOrgID",DbParameterType.String,ParentOrgID,ParameterDirection.Input),//父级组织ID
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageIndex",DbParameterType.Int,pageindex,ParameterDirection.Input),
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageSize",DbParameterType.Int,pagesize,ParameterDirection.Input),
                            OutputParam});
                TdOrderInfo.TdOrderList = new List<TradeOrder>();
                while (dbreader.Read())
                {
                    TradeOrder tdr = new TradeOrder();
                    tdr.Telephone = System.DBNull.Value != dbreader["telephone"] ? dbreader["telephone"].ToString() : string.Empty;
                    tdr.UserName = System.DBNull.Value != dbreader["username"] ? dbreader["username"].ToString() : string.Empty;
                    tdr.OrgName = System.DBNull.Value != dbreader["orgname"] ? dbreader["orgname"].ToString() : string.Empty;
                    tdr.TradeAccount = System.DBNull.Value != dbreader["account"] ? dbreader["account"].ToString() : string.Empty;
                    tdr.OrderId = System.DBNull.Value != dbreader["Orderid"] ? dbreader["Orderid"].ToString() : string.Empty;
                    tdr.ProductName = System.DBNull.Value != dbreader["ProductName"] ? dbreader["ProductName"].ToString() : string.Empty;
                    tdr.ProductCode = System.DBNull.Value != dbreader["productcode"] ? dbreader["productcode"].ToString() : string.Empty;
                    tdr.Quantity = System.DBNull.Value != dbreader["quantity"] ? Convert.ToDouble(dbreader["quantity"]) : 0;
                    tdr.UseQuantity = System.DBNull.Value != dbreader["usequantity"] ? Convert.ToDouble(dbreader["usequantity"]) : 0;
                    tdr.OrderPrice = System.DBNull.Value != dbreader["Orderprice"] ? Convert.ToDouble(dbreader["Orderprice"]) : 0;
                    tdr.ProfitPrice = System.DBNull.Value != dbreader["profitPrice"] ? Convert.ToDouble(dbreader["profitPrice"]) : 0;
                    tdr.LossPrice = System.DBNull.Value != dbreader["lossPrice"] ? Convert.ToDouble(dbreader["lossPrice"]) : 0;
                    tdr.OccMoney = System.DBNull.Value != dbreader["OccMoney"] ? Convert.ToDouble(dbreader["OccMoney"]) : 0;
                    tdr.TradeFee = System.DBNull.Value != dbreader["tradefee"] ? Convert.ToDouble(dbreader["tradefee"]) : 0;
                    tdr.StorageFee = System.DBNull.Value != dbreader["storagefee"] ? Convert.ToDouble(dbreader["storagefee"]) : 0;
                    tdr.OrderTime = System.DBNull.Value != dbreader["Ordertime"] ? Convert.ToDateTime(dbreader["Ordertime"]) : DateTime.MinValue;
                    tdr.OrderType = System.DBNull.Value != dbreader["Ordertype"] ? dbreader["Ordertype"].ToString() : string.Empty;
                    tdr.AllowStore = System.DBNull.Value != dbreader["AllowStore"] ? Convert.ToBoolean(dbreader["AllowStore"]) : false;
                    tdr.TotalWeight = System.DBNull.Value != dbreader["TotalWeight"] ? Convert.ToDouble(dbreader["TotalWeight"]) : 0;

                    tdr.DongJieMoney = System.DBNull.Value != dbreader["DongJieMoney"] ? Convert.ToDouble(dbreader["DongJieMoney"]) : 0;
                    tdr.Money = System.DBNull.Value != dbreader["money"] ? Convert.ToDouble(dbreader["money"]) : 0;
                    tdr.ProfitValue = System.DBNull.Value != dbreader["profitValue"] ? Convert.ToDouble(dbreader["profitValue"]) : 0;
                    tdr.AllOccMoney = System.DBNull.Value != dbreader["alloccmoney"] ? Convert.ToDouble(dbreader["alloccmoney"]) : 0;
                    tdr.FrozenMoney = System.DBNull.Value != dbreader["frozenMoney"] ? Convert.ToDouble(dbreader["frozenMoney"]) : 0;

                    tdr.Orderunit = System.DBNull.Value != dbreader["orderunit"] ? Convert.ToDouble(dbreader["orderunit"]) : 0.5;

                    TdOrderInfo.TdOrderList.Add(tdr);
                }
                if (dbreader.NextResult()) //前进到下一结果集 读取汇总数据
                {
                    if (dbreader.Read())
                    {
                        TdOrderInfo.OccMoney = System.DBNull.Value != dbreader["OccMoney"] ? Convert.ToDouble(dbreader["OccMoney"]) : 0;
                        TdOrderInfo.Quantity = System.DBNull.Value != dbreader["quantity"] ? Convert.ToDouble(dbreader["quantity"]) : 0;
                        TdOrderInfo.Tradefee = System.DBNull.Value != dbreader["tradefee"] ? Convert.ToDouble(dbreader["tradefee"]) : 0;
                        TdOrderInfo.Storagefee = System.DBNull.Value != dbreader["storagefee"] ? Convert.ToDouble(dbreader["storagefee"]) : 0;
                    }
                }

                TdOrderInfo.Result = true;
                TdOrderInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (null != TdOrderInfo.TdOrderList && TdOrderInfo.TdOrderList.Count > 0)
                {
                    TdOrderInfo.TdOrderList.Clear();
                }
                TdOrderInfo.Desc = "查询失败";
                TdOrderInfo.Result = false;
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                    page = Convert.ToInt32(OutputParam.Value);
                }
            }
            return TdOrderInfo;
        }


        /// <summary>
        /// 订单历史分页查询
        /// </summary>
        /// <param name="Lqc"></param>
        /// <param name="Ltype"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public LTradeOrderInfo GetMultiLTradeOrderWithPage(LQueryCon Lqc, string Ltype, int pageindex, int pagesize, ref int page)
        {
            int i = 0;
            LTradeOrderInfo LdOrderInfo = new LTradeOrderInfo();
            LdOrderInfo.LTdOrderList = new List<LTradeOrder>();
            System.Data.Common.DbDataReader dbreader = null;
            TradeUser TdUser = new TradeUser();
            System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
             "@PageCount", DbParameterType.String, 0, ParameterDirection.Output);
            string SearchCondition = string.Empty;
            try
            {
                string AndStr = string.Empty;
                string PartSearchCondition = string.Empty;
                string ParentOrgID = string.Empty;
                if (!ComFunction.ExistUserLoginID(Lqc.LoginID, ref TdUser))
                {
                    return LdOrderInfo;
                }

                if (UserType.NormalType == TdUser.UType) //普通用户
                {
                    AndStr += string.Format("and userid='{0}' ", TdUser.UserID);
                }
                else
                {
                    if (UserType.OrgType == TdUser.UType && !string.IsNullOrEmpty(TdUser.OrgId))
                    {
                        PartSearchCondition = " and orgid in (select orgid from #tmp) ";
                        ParentOrgID = TdUser.OrgId;
                    }
                    if (!string.IsNullOrEmpty(Lqc.TradeAccount))
                    {
                        AndStr += string.Format(" and [Account] like '{0}%' ", Lqc.TradeAccount);
                    }
                }
                if (!string.IsNullOrEmpty(Lqc.OrgName))
                {
                    //AndStr += string.Format(" and [orgname] like '{0}%' ", Lqc.OrgName);
                    AndStr += string.Format(" and [orgid]='{0}' ", Lqc.OrgName);
                }

                //入库单查询
                if ("2" == Ltype)
                {
                    AndStr += " and overtype='2'";
                }
                else
                {
                    AndStr += " and overtype<>'2'";//平仓单查询
                }
                if ("ALL" != Lqc.ProductName.ToUpper())
                {
                    AndStr += string.Format(" and ProductName='{0}'", Lqc.ProductName);
                }
                if ("ALL" != Lqc.OrderType.ToUpper())
                {
                    AndStr += string.Format(" and ordertype='{0}'", Lqc.OrderType);
                }
                if (!string.IsNullOrEmpty(Lqc.PriceCode))
                {
                    AndStr += string.Format(" and [PriceCode]='{0}' ", Lqc.PriceCode);
                }

                //内部子查询字段列表
                string SumSelectList = "round(isnull(sum(profitValue),0),2) as profitValue,round(isnull(sum(quantity),0),2) as quantity,round(isnull(sum(tradefee),0),2) as tradefee,round(isnull(sum(storagefee),0),2) as storagefee ";
                //选择字段列表
                string selectlist = "orgname,telephone,username,Account,ProductName,historyOrderId,productcode,lossprice,profitPrice,Orderprice,overType,overprice,profitValue,tradefee,storagefee,Overtime,Orderid,ordertype,quantity,ordertime,adjustbase,valuedot,unit,lowerprice ";

                //查询条件
                SearchCondition = string.Format("where overtime >= '{0}' and overtime <='{1}' {2} {3} ",
                    Lqc.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), Lqc.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), AndStr, PartSearchCondition);


                dbreader = DbHelper.RunProcedureGetDataReader("GetRecordFromPageExWithSum",
                     new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@selectlist",DbParameterType.String,selectlist,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SumSelectList",DbParameterType.String,SumSelectList,ParameterDirection.Input),
                       DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableSource",DbParameterType.String,"V_L_Trade_Order",ParameterDirection.Input), //表名或视图表 
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableOrder",DbParameterType.String,"a",ParameterDirection.Input), //排序后的表名称 即子查询结果集的别名
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SearchCondition",DbParameterType.String,SearchCondition,ParameterDirection.Input),
                          DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@OrderExpression",DbParameterType.String,"order by overtime desc",ParameterDirection.Input),//排序 表达式
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ParentOrgID",DbParameterType.String,ParentOrgID,ParameterDirection.Input),//父级组织ID
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageIndex",DbParameterType.Int,pageindex,ParameterDirection.Input),
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageSize",DbParameterType.Int,pagesize,ParameterDirection.Input),
                            OutputParam});
                while (dbreader.Read())
                {
                    LTradeOrder ltradeOrder = new LTradeOrder();
                    ltradeOrder.Telephone = System.DBNull.Value != dbreader["telephone"] ? dbreader["telephone"].ToString() : string.Empty;
                    ltradeOrder.UserName = System.DBNull.Value != dbreader["username"] ? dbreader["username"].ToString() : string.Empty;
                    ltradeOrder.OrgName = System.DBNull.Value != dbreader["orgname"] ? dbreader["orgname"].ToString() : string.Empty;
                    ltradeOrder.TradeAccount = System.DBNull.Value != dbreader["Account"] ? dbreader["Account"].ToString() : string.Empty;
                    ltradeOrder.HistoryOrderId = System.DBNull.Value != dbreader["historyOrderId"] ? dbreader["historyOrderId"].ToString() : string.Empty;
                    ltradeOrder.ProductName = System.DBNull.Value != dbreader["ProductName"] ? dbreader["ProductName"].ToString() : string.Empty;
                    ltradeOrder.ProductCode = System.DBNull.Value != dbreader["productcode"] ? dbreader["productcode"].ToString() : string.Empty;
                    ltradeOrder.OrderPrice = System.DBNull.Value != dbreader["Orderprice"] ? Convert.ToDouble(dbreader["Orderprice"]) : 0;
                    ltradeOrder.OverType = System.DBNull.Value != dbreader["overType"] ? dbreader["overType"].ToString() : string.Empty;
                    ltradeOrder.OverPrice = System.DBNull.Value != dbreader["overprice"] ? Convert.ToDouble(dbreader["overprice"]) : 0;
                    ltradeOrder.ProfitValue = System.DBNull.Value != dbreader["profitValue"] ? Convert.ToDouble(dbreader["profitValue"]) : 0;
                    ltradeOrder.TradeFee = System.DBNull.Value != dbreader["tradefee"] ? Convert.ToDouble(dbreader["tradefee"]) : 0;
                    ltradeOrder.StorageFee = System.DBNull.Value != dbreader["storagefee"] ? Convert.ToDouble(dbreader["storagefee"]) : 0;
                    ltradeOrder.LossPrice = System.DBNull.Value != dbreader["lossprice"] ? Convert.ToDouble(dbreader["lossprice"]) : 0;
                    ltradeOrder.ProfitPrice = System.DBNull.Value != dbreader["profitPrice"] ? Convert.ToDouble(dbreader["profitPrice"]) : 0;
                    ltradeOrder.OverTime = System.DBNull.Value != dbreader["Overtime"] ? Convert.ToDateTime(dbreader["Overtime"]) : DateTime.MinValue;
                    ltradeOrder.OrderId = System.DBNull.Value != dbreader["Orderid"] ? dbreader["Orderid"].ToString() : string.Empty;
                    ltradeOrder.OrderType = System.DBNull.Value != dbreader["ordertype"] ? dbreader["ordertype"].ToString() : string.Empty;
                    ltradeOrder.Quantity = System.DBNull.Value != dbreader["quantity"] ? Convert.ToDouble(dbreader["quantity"]) : 0;
                    ltradeOrder.OrderTime = System.DBNull.Value != dbreader["ordertime"] ? Convert.ToDateTime(dbreader["ordertime"]) : DateTime.MinValue;
                    if ("0" == ltradeOrder.OrderType && DBNull.Value != dbreader["adjustbase"] && DBNull.Value != dbreader["valuedot"] && DBNull.Value != dbreader["quantity"])
                    {
                        ltradeOrder.ProductMoney = System.Math.Round(ltradeOrder.OverPrice / Convert.ToDouble(dbreader["adjustbase"]) * Convert.ToDouble(dbreader["valuedot"]) * Convert.ToDouble(dbreader["quantity"]), 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        //ltradeOrder.ProductMoney = System.Math.Round(Convert.ToDouble(sqldr["unit"]) * Convert.ToDouble(sqldr["lowerprice"]) * Convert.ToDouble(sqldr["quantity"]), 2, MidpointRounding.AwayFromZero);
                        //卖单入库的货款(即所谓的折旧费) 已经计算在盈亏里面了 所以没有所谓的货款
                        ltradeOrder.ProductMoney = 0;
                    }
                    LdOrderInfo.LTdOrderList.Add(ltradeOrder);
                    i++;
                }
                if (dbreader.NextResult())
                {
                    if (dbreader.Read())
                    {
                        LdOrderInfo.ProfitValue = System.DBNull.Value != dbreader["profitValue"] ? Convert.ToDouble(dbreader["profitValue"]) : 0;
                        LdOrderInfo.Quantity = System.DBNull.Value != dbreader["quantity"] ? Convert.ToDouble(dbreader["quantity"]) : 0;
                        LdOrderInfo.Tradefee = System.DBNull.Value != dbreader["tradefee"] ? Convert.ToDouble(dbreader["tradefee"]) : 0;
                        LdOrderInfo.Storagefee = System.DBNull.Value != dbreader["storagefee"] ? Convert.ToDouble(dbreader["storagefee"]) : 0;
                    }
                }
                LdOrderInfo.Result = true;
                LdOrderInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (null != LdOrderInfo.LTdOrderList && LdOrderInfo.LTdOrderList.Count > 0)
                {
                    LdOrderInfo.LTdOrderList.Clear();
                }
                LdOrderInfo.Desc = "查询失败";
                LdOrderInfo.Result = false;
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                    page = Convert.ToInt32(OutputParam.Value);
                }
            }

            return LdOrderInfo;
        }

        /// <summary>
        /// 限价挂单分页查询
        /// </summary>
        /// <param name="Cxqc">查询条条</param>
        /// <param name="pageindex">第几页,从1开始</param>
        /// <param name="pagesize">每页多少条</param>
        /// <param name="page">输出参数(总页数)</param>
        /// <returns>订单信息</returns>
        public TradeHoldOrderInfo GetMultiTradeHoldOrderWithPage(CxQueryCon Cxqc, int pageindex, int pagesize, ref int page)
        {
            TradeHoldOrderInfo TdHoldOrderInfo = new TradeHoldOrderInfo();

            System.Data.Common.DbDataReader dbreader = null;
            System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                "@PageCount", DbParameterType.Int, 0, ParameterDirection.Output);
            string SearchCondition = string.Empty;
            try
            {
                string AndStr = string.Empty;
                string PartSearchCondition = string.Empty;
                string ParentOrgID = string.Empty;
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期


                if (!ComFunction.ExistUserLoginID(Cxqc.LoginID, ref TdUser))
                {
                    TdHoldOrderInfo.Result = false;
                    TdHoldOrderInfo.Desc = ResCode.UL003Desc;
                    return TdHoldOrderInfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    TdHoldOrderInfo.Result = false;
                    TdHoldOrderInfo.Desc = ComFunction.NotRightUser;
                    return TdHoldOrderInfo;
                }
                #endregion

                if (!string.IsNullOrEmpty(Cxqc.TradeAccount)) //交易账号不为空 表示查询该用户的历史单 否则查询所有用户的历史单
                {
                    AndStr = string.Format(" and [Account] like '{0}%' ", Cxqc.TradeAccount);
                }
                string productname = string.Empty;
                if ("ALL" == Cxqc.ProductName.ToUpper())
                {
                    productname = string.Empty;
                }
                else
                {
                    productname = string.Format(" and ProductName='{0}'", Cxqc.ProductName);
                }

                if (!string.IsNullOrEmpty(Cxqc.OrgName))
                {
                    //AndStr += string.Format("and [orgname] like '{0}%' ", Cxqc.OrgName);
                    AndStr += string.Format("and [orgid]='{0}' ", Cxqc.OrgName);
                }

                if (UserType.OrgType == TdUser.UType && !string.IsNullOrEmpty(TdUser.OrgId))
                {
                    //AndStr += string.Format(" and [OrgId] in ({0}) ", ComFunction.GetOrgIds(TdUser.OrgId));
                    PartSearchCondition = " and orgid in (select orgid from #tmp) ";
                    ParentOrgID = TdUser.OrgId;
                }
                if ("ALL" != Cxqc.OrderType.ToUpper())
                {
                    AndStr += string.Format(" and ordertype='{0}'", Cxqc.OrderType);
                }
                if (!string.IsNullOrEmpty(Cxqc.PriceCode))
                {
                    AndStr += string.Format(" and [PriceCode]='{0}' ", Cxqc.PriceCode);
                }
                string SumSelectList = "round(isnull(sum(frozenMoney),0),2) as frozenMoney,round(isnull(sum(quantity),0),2) as quantity ";

                string selectlist = "orgname,telephone,username, account,ProductName,HoldOrderID,productcode,quantity,frozenMoney,OrderType,HoldPrice,profitPrice,lossPrice,validtime,ordertime ";


                SearchCondition = string.Format("where ordertime >= '{0}' and ordertime <='{1}' {2} {3} {4}",
                    Cxqc.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), Cxqc.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    productname, AndStr, PartSearchCondition);

                dbreader = DbHelper.RunProcedureGetDataReader("GetRecordFromPageExWithSum",
                     new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@selectlist",DbParameterType.String,selectlist,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SumSelectList",DbParameterType.String,SumSelectList,ParameterDirection.Input),
                       DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableSource",DbParameterType.String,"V_Trade_HoldOrder",ParameterDirection.Input), //表名或视图表 
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableOrder",DbParameterType.String,"a",ParameterDirection.Input), //排序后的表名称 即子查询结果集的别名
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SearchCondition",DbParameterType.String,SearchCondition,ParameterDirection.Input),
                          DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@OrderExpression",DbParameterType.String,"order by ordertime desc",ParameterDirection.Input),//排序 表达式
                            DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ParentOrgID",DbParameterType.String,ParentOrgID,ParameterDirection.Input),//父级组织ID
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageIndex",DbParameterType.Int,pageindex,ParameterDirection.Input),
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageSize",DbParameterType.Int,pagesize,ParameterDirection.Input),
                            OutputParam});
                TdHoldOrderInfo.TdHoldOrderList = new List<TradeHoldOrder>();
                while (dbreader.Read())
                {
                    TradeHoldOrder thdr = new TradeHoldOrder();
                    thdr.Telephone = System.DBNull.Value != dbreader["telephone"] ? dbreader["telephone"].ToString() : string.Empty;
                    thdr.UserName = System.DBNull.Value != dbreader["username"] ? dbreader["username"].ToString() : string.Empty;
                    thdr.OrgName = System.DBNull.Value != dbreader["orgname"] ? dbreader["orgname"].ToString() : string.Empty;
                    thdr.TradeAccount = System.DBNull.Value != dbreader["account"] ? dbreader["account"].ToString() : string.Empty;
                    thdr.HoldOrderID = System.DBNull.Value != dbreader["HoldOrderID"] ? dbreader["HoldOrderID"].ToString() : string.Empty;
                    thdr.ProductName = System.DBNull.Value != dbreader["ProductName"] ? dbreader["ProductName"].ToString() : string.Empty;
                    thdr.ProductCode = System.DBNull.Value != dbreader["productcode"] ? dbreader["productcode"].ToString() : string.Empty;
                    thdr.Quantity = System.DBNull.Value != dbreader["quantity"] ? Convert.ToDouble(dbreader["quantity"]) : 0;
                    thdr.FrozenMoney = System.DBNull.Value != dbreader["frozenMoney"] ? Convert.ToDouble(dbreader["frozenMoney"]) : 0;

                    thdr.OrderType = System.DBNull.Value != dbreader["OrderType"] ? dbreader["OrderType"].ToString() : string.Empty;
                    thdr.HoldPrice = System.DBNull.Value != dbreader["HoldPrice"] ? Convert.ToDouble(dbreader["HoldPrice"]) : 0;
                    thdr.ProfitPrice = System.DBNull.Value != dbreader["profitPrice"] ? Convert.ToDouble(dbreader["profitPrice"]) : 0;
                    thdr.LossPrice = System.DBNull.Value != dbreader["lossPrice"] ? Convert.ToDouble(dbreader["lossPrice"]) : 0;
                    thdr.ValidTime = System.DBNull.Value != dbreader["validtime"] ? Convert.ToDateTime(dbreader["validtime"]) : DateTime.MinValue;
                    thdr.OrderTime = System.DBNull.Value != dbreader["Ordertime"] ? Convert.ToDateTime(dbreader["Ordertime"]) : DateTime.MinValue;
                    TdHoldOrderInfo.TdHoldOrderList.Add(thdr);
                }
                if (dbreader.NextResult())//前进到下一结果集 
                {
                    if (dbreader.Read()) //获取汇总数据
                    {
                        TdHoldOrderInfo.Quantity = System.DBNull.Value != dbreader["quantity"] ? Convert.ToDouble(dbreader["quantity"]) : 0;
                        TdHoldOrderInfo.FrozenMoney = System.DBNull.Value != dbreader["frozenMoney"] ? Convert.ToDouble(dbreader["frozenMoney"]) : 0;
                    }
                }
                TdHoldOrderInfo.Result = true;
                TdHoldOrderInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (null != TdHoldOrderInfo.TdHoldOrderList && TdHoldOrderInfo.TdHoldOrderList.Count > 0)
                {
                    TdHoldOrderInfo.TdHoldOrderList.Clear();
                }
                TdHoldOrderInfo.Desc = "查询失败";
                TdHoldOrderInfo.Result = false;
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                    page = Convert.ToInt32(OutputParam.Value);
                }
            }

            return TdHoldOrderInfo;
        }

        /// <summary>
        /// 日志记录分页查询
        /// </summary>
        /// <param name="Logqc">查询条条</param>
        /// <param name="pageindex">第几页,从1开始</param>
        /// <param name="pagesize">每页多少条</param>
        /// <param name="page">输出参数(总页数)</param>
        /// <returns>日志信息</returns>
        public TradeALogInfo GetTradeALogInfoWithPage(LogQueryCon Logqc, int pageindex, int pagesize, ref int page)
        {
            TradeALogInfo tradeALogInfo = new TradeALogInfo();

            System.Data.Common.DbDataReader dbreader = null;
            TradeUser TdUser = new TradeUser();
            System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageCount", DbParameterType.Int, 0, ParameterDirection.Output);
            try
            {
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(Logqc.LoginID, ref TdUser))
                {
                    tradeALogInfo.Result = false;
                    tradeALogInfo.Desc = ResCode.UL003Desc;
                    return tradeALogInfo;
                }
                if (UserType.NormalType == TdUser.UType || UserType.OrgType == TdUser.UType)
                {
                    tradeALogInfo.Result = false;
                    tradeALogInfo.Desc = ComFunction.NotRightUser;
                    return tradeALogInfo;
                }
                #endregion
                string andstr = " and Account<>'admin' and remark not like'%手工调帐%' ";

                if ("ROOT" != TdUser.Account.ToUpper())
                {
                    andstr += " and Account<>'root'";
                }
                if (!string.IsNullOrEmpty(Logqc.Account))
                {
                    andstr += string.Format(" and Account like'{0}%'", Logqc.Account);
                }
                string SearchCondition = string.Format("where a.opertime >= '{0}' and a.opertime <='{1}' and UserType in(0,1,2,3) {2} ", Logqc.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), Logqc.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), andstr);

                dbreader = DbHelper.RunProcedureGetDataReader("GetRecordFromPage",
                      new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@selectlist",DbParameterType.String,"[OperTime],[Account],[UserType],[Remark]",ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SubSelectList",DbParameterType.String,"[OperTime],[Account],[UserType],[Remark]",ParameterDirection.Input),
                       DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableSource",DbParameterType.String,"Base_OperrationLog a",ParameterDirection.Input), //表名或视图表 
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableOrder",DbParameterType.String,"a",ParameterDirection.Input), //排序后的表名称 即子查询结果集的别名
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SearchCondition",DbParameterType.String,SearchCondition,ParameterDirection.Input),
                          DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@OrderExpression",DbParameterType.String,"order by a.opertime desc",ParameterDirection.Input),//排序 表达式
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageIndex",DbParameterType.Int,pageindex,ParameterDirection.Input),
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageSize",DbParameterType.Int,pagesize,ParameterDirection.Input),
                            OutputParam});
                tradeALogInfo.ALogList = new List<ALog>();
                while (dbreader.Read())
                {
                    ALog aLog = new ALog();
                    aLog.Account = System.DBNull.Value != dbreader["Account"] ? dbreader["Account"].ToString() : string.Empty;
                    aLog.OperTime = System.DBNull.Value != dbreader["OperTime"] ? Convert.ToDateTime(dbreader["OperTime"]) : DateTime.MinValue;
                    aLog.Desc = System.DBNull.Value != dbreader["Remark"] ? dbreader["Remark"].ToString() : string.Empty;
                    aLog.UType = System.DBNull.Value != dbreader["UserType"] ? (UserType)dbreader["UserType"] : UserType.AdminType;
                    tradeALogInfo.ALogList.Add(aLog);
                }


                tradeALogInfo.Result = true;
                tradeALogInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                tradeALogInfo.Result = false;
                tradeALogInfo.Desc = "查询日志失败";
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                    page = Convert.ToInt32(OutputParam.Value);
                }
            }
            return tradeALogInfo;
        }

        /// <summary>
        /// 客户资料分页查询
        /// </summary>
        /// <param name="Uqc">查询条条</param>
        /// <param name="pageindex">第几页,从1开始</param>
        /// <param name="pagesize">每页多少条</param>
        /// <param name="page">输出参数(总页数)</param>
        /// <returns>客户信息</returns>
        public UserBaseInfo GetUserBaseInfoWithPage(UserQueryCon Uqc, int pageindex, int pagesize, ref int page)
        {
            UserBaseInfo userBaseinfo = new UserBaseInfo();
            TradeUser RefTdUser = new TradeUser();

            System.Data.Common.DbDataReader dbreader = null;
            System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                 "@PageCount", DbParameterType.Int, page, ParameterDirection.Output);
            string SearchCondition = string.Empty;
            try
            {
                #region 判断登陆标识是否过期
                if (!ComFunction.ExistUserLoginID(Uqc.LoginId, ref RefTdUser))
                {
                    userBaseinfo.Result = false;
                    userBaseinfo.Desc = ResCode.UL003Desc;
                    return userBaseinfo;
                }
                if (UserType.NormalType == RefTdUser.UType)
                {
                    userBaseinfo.Result = false;
                    userBaseinfo.Desc = ComFunction.NotRightUser;
                    return userBaseinfo;
                }
                #endregion
                string AndStr = string.Empty;
                string PartSearchCondition = string.Empty;
                string ParentOrgID = string.Empty;
                if (UserType.OrgType == RefTdUser.UType && !string.IsNullOrEmpty(RefTdUser.OrgId))
                {
                    //AndStr = string.Format("and [OrgId] in ({0}) ", ComFunction.GetOrgIds(RefTdUser.OrgId));
                    PartSearchCondition = " and orgid in (select orgid from #tmp) ";
                    ParentOrgID = RefTdUser.OrgId;
                }
                if (!string.IsNullOrEmpty(Uqc.TradeAccount))
                {
                    AndStr += string.Format("and [Account] like '{0}%' ", Uqc.TradeAccount);
                }
                if (!string.IsNullOrEmpty(Uqc.CardNum))
                {
                    AndStr += string.Format("and [CardNum] like '{0}%' ", Uqc.CardNum);
                }
                if (!string.IsNullOrEmpty(Uqc.UserName))
                {
                    AndStr += string.Format("and [userName] like '{0}%' ", Uqc.UserName);
                }
                if (Uqc.OnLine) //查询在线用户
                {
                    AndStr += string.Format(" and [Online]=1 ");
                }
                if (!string.IsNullOrEmpty(Uqc.OrgName))
                {
                    //AndStr += string.Format(" and [orgname] like '{0}%' ", Uqc.OrgName);
                    AndStr += string.Format(" and [orgid]='{0}' ", Uqc.OrgName);
                }

                if (!string.IsNullOrEmpty(Uqc.TelPhone))
                {
                    //AndStr += string.Format(" and [orgname] like '{0}%' ", Uqc.OrgName);
                    AndStr += string.Format(" and  [PhoneNum]='{0}' ", Uqc.TelPhone);
                }

                int UType = (int)Uqc.UType;
                string usertype = string.Format(" and [UserType]={0} ", UType);
                if (UserType.RootType == RefTdUser.UType && UserType.AdminType == Uqc.UType) // 如果是超级管理员
                {
                    usertype = string.Format(" and ([UserType]={0} or [UserType]={1})", UType, (int)UserType.RootType);
                }
                if (RefTdUser.UType == UserType.AdminType && RefTdUser.Account != "root")
                {
                    AndStr += string.Format(" and [Account]<>'root' ");
                }
                if (!string.IsNullOrEmpty(Uqc.UserGroupId))
                {
                    AndStr += string.Format(" and [usergroupid]='{0}' ", Uqc.UserGroupId);
                }
                if (!Uqc.StartTime.ToString().Contains("0001"))//0001-01-01 00:00:00
                    AndStr += string.Format(" and OpenTime >= '{0}' and OpenTime <='{1}'",
                        Uqc.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), Uqc.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                if (!string.IsNullOrEmpty(Uqc.IsBroker))
                {

                    if (Uqc.IsBroker == "1")//Y
                        AndStr += string.Format(" and [WAccountType]='4' ");
                    if (Uqc.IsBroker == "2")
                        AndStr += string.Format(" and [WAccountType]!='4' ");
                }
                if (!string.IsNullOrEmpty(Uqc.Broker))
                {
                    AndStr += string.Format(" and ([PAccount]='{0}' or [PUserName]='{0}') ", Uqc.Broker);
                }

                string selectlist = string.Format(@"[orgid],[Reperson],[OrgName],[telephone],[BindAccount],[userName],[UserId],[status],[Accounttype],[CorporationName],[Account],[LoginPwd],[cashPwd],
        [CardType],[CardNum],[PhoneNum],[TelNum],[Email],[LinkMan],[LinkAdress],[OrderPhone],
        [sex],[OpenMan],[OpenTime],[LastUpdateTime],[LastUpdateID],[Ip],[Mac],[LastLoginTime],[Online],
        [MinTrade],[OrderUnit],[MaxTrade],[PermitRcash],[PermitCcash],[PermitDhuo],[PermitHshou],[DongJieMoney],
        [PermitRstore],[PermitDelOrder], [BankState],[money],[OccMoney],[frozenMoney],[BankAccount],
        [AccountName],[BankCard],[SubUser],[TanUser],[ConBankType],[OpenBank],[OpenBankAddress],[UserType],[usergroupid],[WAccountType],[PAccount],[PUserName],[PUserId] ");

                string SumSelectList = string.Format(@"round(isnull(sum(frozenMoney),0),2) as frozenMoney,round(isnull(sum(OccMoney),0),2) as OccMoney,round(isnull(sum([money]),0),2) as [money],round(isnull(sum([DongJieMoney]),0),2) as [DongJieMoney] ");

                SearchCondition = string.Format("where ([BankState]<>'4' or [BankState] is null) {0} {1} {2}", AndStr, usertype, PartSearchCondition);

                dbreader = DbHelper.RunProcedureGetDataReader("GetRecordFromPageExWithSum", new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@selectlist",DbParameterType.String,selectlist,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SumSelectList",DbParameterType.String,SumSelectList,ParameterDirection.Input),
                       DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableSource",DbParameterType.String,"V_TradeUser a",ParameterDirection.Input), //表名或视图表 
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableOrder",DbParameterType.String,"a",ParameterDirection.Input), //排序后的表名称 即子查询结果集的别名
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SearchCondition",DbParameterType.String,SearchCondition,ParameterDirection.Input),
                          DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@OrderExpression",DbParameterType.String,"order by Account",ParameterDirection.Input),//排序 表达式
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ParentOrgID",DbParameterType.String,ParentOrgID,ParameterDirection.Input),//父级组织ID
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageIndex",DbParameterType.Int,pageindex,ParameterDirection.Input),
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageSize",DbParameterType.Int,pagesize,ParameterDirection.Input),
                            OutputParam});
                userBaseinfo.TdUserList = new List<TradeUser>();
                while (dbreader.Read())
                {
                    TradeUser TdUser = new TradeUser();
                    TdUser.Reperson = System.DBNull.Value != dbreader["Reperson"] ? dbreader["Reperson"].ToString() : string.Empty;
                    TdUser.BindAccount = System.DBNull.Value != dbreader["BindAccount"] ? dbreader["BindAccount"].ToString() : string.Empty;
                    TdUser.UserID = System.DBNull.Value != dbreader["UserID"] ? dbreader["UserID"].ToString() : string.Empty;
                    TdUser.UserName = System.DBNull.Value != dbreader["UserName"] ? dbreader["UserName"].ToString() : string.Empty;
                    TdUser.Status = System.DBNull.Value != dbreader["Status"] ? dbreader["Status"].ToString() : string.Empty;
                    TdUser.AccountType = System.DBNull.Value != dbreader["AccountType"] ? dbreader["AccountType"].ToString() : string.Empty;
                    TdUser.Account = System.DBNull.Value != dbreader["Account"] ? dbreader["Account"].ToString() : string.Empty;
                    TdUser.LoginPwd = System.DBNull.Value != dbreader["LoginPwd"] ? Des3.Des3DecodeCBC(dbreader["LoginPwd"].ToString()) : string.Empty;
                    TdUser.CashPwd = System.DBNull.Value != dbreader["CashPwd"] ? Des3.Des3DecodeCBC(dbreader["CashPwd"].ToString()) : string.Empty;
                    TdUser.CardType = System.DBNull.Value != dbreader["CardType"] ? dbreader["CardType"].ToString() : string.Empty;
                    TdUser.CardNum = System.DBNull.Value != dbreader["CardNum"] ? dbreader["CardNum"].ToString() : string.Empty;
                    TdUser.PhoneNum = System.DBNull.Value != dbreader["PhoneNum"] ? dbreader["PhoneNum"].ToString() : string.Empty;
                    TdUser.TelNum = System.DBNull.Value != dbreader["TelNum"] ? dbreader["TelNum"].ToString() : string.Empty;
                    TdUser.Email = System.DBNull.Value != dbreader["Email"] ? dbreader["Email"].ToString() : string.Empty;
                    TdUser.LinkMan = System.DBNull.Value != dbreader["LinkMan"] ? dbreader["LinkMan"].ToString() : string.Empty;
                    TdUser.LinkAdress = System.DBNull.Value != dbreader["LinkAdress"] ? dbreader["LinkAdress"].ToString() : string.Empty;
                    TdUser.Sex = System.DBNull.Value != dbreader["Sex"] ? dbreader["Sex"].ToString() : string.Empty;
                    TdUser.OpenMan = System.DBNull.Value != dbreader["OpenMan"] ? dbreader["OpenMan"].ToString() : string.Empty;
                    TdUser.OpenTime = System.DBNull.Value != dbreader["OpenTime"] ? Convert.ToDateTime(dbreader["OpenTime"]) : DateTime.MinValue;
                    TdUser.LastUpdateTime = System.DBNull.Value != dbreader["LastUpdateTime"] ? Convert.ToDateTime(dbreader["LastUpdateTime"]) : DateTime.MinValue;
                    TdUser.LastUpdateID = System.DBNull.Value != dbreader["LastUpdateID"] ? dbreader["LastUpdateID"].ToString() : string.Empty;

                    TdUser.Ip = System.DBNull.Value != dbreader["Ip"] ? dbreader["Ip"].ToString() : string.Empty;
                    TdUser.Mac = System.DBNull.Value != dbreader["Mac"] ? dbreader["Mac"].ToString() : string.Empty;
                    TdUser.LastLoginTime = System.DBNull.Value != dbreader["LastLoginTime"] ? Convert.ToDateTime(dbreader["LastLoginTime"]) : DateTime.MinValue;
                    TdUser.Online = System.DBNull.Value != dbreader["Online"] ? Convert.ToBoolean(dbreader["Online"]) : false;
                    TdUser.MinTrade = System.DBNull.Value != dbreader["MinTrade"] ? Convert.ToDouble(dbreader["MinTrade"]) : 0;
                    TdUser.OrderUnit = System.DBNull.Value != dbreader["OrderUnit"] ? Convert.ToDouble(dbreader["OrderUnit"]) : 0;
                    TdUser.MaxTrade = System.DBNull.Value != dbreader["MaxTrade"] ? Convert.ToDouble(dbreader["MaxTrade"]) : 0;
                    TdUser.PermitRcash = System.DBNull.Value != dbreader["PermitRcash"] ? Convert.ToBoolean(dbreader["PermitRcash"]) : false;
                    TdUser.PermitCcash = System.DBNull.Value != dbreader["PermitCcash"] ? Convert.ToBoolean(dbreader["PermitCcash"]) : false;
                    TdUser.PermitDhuo = System.DBNull.Value != dbreader["PermitDhuo"] ? Convert.ToBoolean(dbreader["PermitDhuo"]) : false;
                    TdUser.PermitHshou = System.DBNull.Value != dbreader["PermitHshou"] ? Convert.ToBoolean(dbreader["PermitHshou"]) : false;
                    TdUser.PermitRstore = System.DBNull.Value != dbreader["PermitRstore"] ? Convert.ToBoolean(dbreader["PermitRstore"]) : false;
                    TdUser.PermitDelOrder = System.DBNull.Value != dbreader["PermitDelOrder"] ? Convert.ToBoolean(dbreader["PermitDelOrder"]) : false;
                    TdUser.CorporationName = System.DBNull.Value != dbreader["CorporationName"] ? dbreader["CorporationName"].ToString() : string.Empty;
                    TdUser.OrderPhone = System.DBNull.Value != dbreader["OrderPhone"] ? dbreader["OrderPhone"].ToString() : string.Empty;

                    TdUser.BankState = System.DBNull.Value != dbreader["BankState"] ? dbreader["BankState"].ToString() : string.Empty;
                    TdUser.Money = System.DBNull.Value != dbreader["Money"] ? Convert.ToDouble(dbreader["Money"]) : 0;
                    TdUser.OccMoney = System.DBNull.Value != dbreader["OccMoney"] ? Convert.ToDouble(dbreader["OccMoney"]) : 0;
                    TdUser.FrozenMoney = System.DBNull.Value != dbreader["FrozenMoney"] ? Convert.ToDouble(dbreader["FrozenMoney"]) : 0;
                    TdUser.DongJieMoney = System.DBNull.Value != dbreader["DongJieMoney"] ? Convert.ToDouble(dbreader["DongJieMoney"]) : 0;
                    TdUser.BankAccount = System.DBNull.Value != dbreader["BankAccount"] ? dbreader["BankAccount"].ToString() : string.Empty;
                    TdUser.AccountName = System.DBNull.Value != dbreader["AccountName"] ? dbreader["AccountName"].ToString() : string.Empty;
                    TdUser.BankCard = System.DBNull.Value != dbreader["BankCard"] ? dbreader["BankCard"].ToString() : string.Empty;

                    TdUser.SubUser = System.DBNull.Value != dbreader["SubUser"] ? dbreader["SubUser"].ToString() : string.Empty;
                    TdUser.TanUser = System.DBNull.Value != dbreader["TanUser"] ? dbreader["TanUser"].ToString() : string.Empty;
                    TdUser.ConBankType = System.DBNull.Value != dbreader["ConBankType"] ? dbreader["ConBankType"].ToString() : string.Empty;
                    TdUser.OpenBank = System.DBNull.Value != dbreader["OpenBank"] ? dbreader["OpenBank"].ToString() : string.Empty;
                    TdUser.OpenBankAddress = System.DBNull.Value != dbreader["OpenBankAddress"] ? dbreader["OpenBankAddress"].ToString() : string.Empty;

                    TdUser.OrgName = System.DBNull.Value != dbreader["OrgName"] ? dbreader["OrgName"].ToString() : string.Empty;
                    TdUser.Telephone = System.DBNull.Value != dbreader["telephone"] ? dbreader["telephone"].ToString() : string.Empty;
                    TdUser.OrgId = System.DBNull.Value != dbreader["orgid"] ? dbreader["orgid"].ToString() : string.Empty;

                    TdUser.IsBroker = System.DBNull.Value != dbreader["WAccountType"] && dbreader["WAccountType"].ToString() == "4" ? true : false;

                    TdUser.PAccount = System.DBNull.Value != dbreader["PAccount"] ? dbreader["PAccount"].ToString() : string.Empty;
                    TdUser.PUserName = System.DBNull.Value != dbreader["PUserName"] ? dbreader["PUserName"].ToString() : string.Empty;
                    TdUser.PUserId = System.DBNull.Value != dbreader["PUserId"] ? dbreader["PUserId"].ToString() : string.Empty;
                    userBaseinfo.TdUserList.Add(TdUser);
                }
                if (dbreader.NextResult())//前进到下一结果集 
                {
                    if (dbreader.Read()) //获取汇总数据
                    {
                        userBaseinfo.Money = System.DBNull.Value != dbreader["money"] ? Convert.ToDouble(dbreader["money"]) : 0;
                        userBaseinfo.FrozenMoney = System.DBNull.Value != dbreader["frozenMoney"] ? Convert.ToDouble(dbreader["frozenMoney"]) : 0;
                        userBaseinfo.DongJieMoney = System.DBNull.Value != dbreader["DongJieMoney"] ? Convert.ToDouble(dbreader["DongJieMoney"]) : 0;
                        userBaseinfo.OccMoney = System.DBNull.Value != dbreader["OccMoney"] ? Convert.ToDouble(dbreader["OccMoney"]) : 0;
                    }
                }
                userBaseinfo.Result = true;
                userBaseinfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(new Exception(Uqc.StartTime.ToString() + ex.Message));
                userBaseinfo.Result = false;
                userBaseinfo.Desc = "查询失败";
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                    page = Convert.ToInt32(OutputParam.Value); //在close之后获取输出参数的值
                }
            }
            return userBaseinfo;
        }

        /// <summary>
        /// 手动报价
        /// </summary>
        /// <param name="LoginId">登陆标识</param>
        /// <param name="PriceCode">行情编码</param>
        /// <param name="RealPrice">实时价</param>
        /// <returns>结果描述</returns>
        public ResultDesc ManualPrice(String LoginId, string PriceCode, double RealPrice)
        {
            ResultDesc rsdc = new ResultDesc();
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse(ComFunction.recvip), Convert.ToInt32(ComFunction.recvport)));
                NetworkStream NetStream = client.GetStream();

                double water = 0;
                double rate = 1;
                double coefxs = 1;
                double coefficient = 1;
                if (!ComFunction.GetWaterAndRate(PriceCode, ref water, ref rate, ref coefxs, ref coefficient))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "手工报价遇到错误";
                    return rsdc;
                }
                RealPrice = ((RealPrice - water) / (coefficient * rate)) * coefxs;

                //数据格式：Manual + Tab键 + 行情编码 +Tab键 + 时间(yyyyMMddHHmmss) + Tab键 +开盘价 + Tab键 +最高价 +　Tab键 +　最低价　＋Tab键 +　收盘价　＋Tab键 +成交量
                //Manual是手动报价标识
                string data = string.Format("Manual\t{0}\t{1}\t{2}\t{2}\t{2}\t{2}\t1", PriceCode, DateTime.Now.ToString("yyyyMMddHHmmss"), RealPrice);
                byte[] temp = Encoding.ASCII.GetBytes(string.Format("[datalen={0}]{1}", data.Length, data));

                NetStream.Write(temp, 0, temp.Length);
                NetStream.Flush();
                client.Close();
                NetStream.Close();
                rsdc.Result = true;
                rsdc.Desc = "手工报价成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "手工报价失败";
            }
            return rsdc;
        }


        /// <summary>
        /// 出金查询
        /// </summary>
        /// <param name="Cjqc"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public TradeChuJinInfo GetMultiTradeChuJinWithPage(CJQueryCon Cjqc, int pageindex, int pagesize, ref int page)
        {
            TradeChuJinInfo TdChuJinInfo = new TradeChuJinInfo();

            System.Data.Common.DbDataReader dbreader = null;
            System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                "@PageCount", DbParameterType.Int, 0, ParameterDirection.Output);
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期


                if (!ComFunction.ExistUserLoginID(Cjqc.LoginID, ref TdUser))
                {
                    TdChuJinInfo.Result = false;
                    TdChuJinInfo.Desc = ResCode.UL003Desc;
                    return TdChuJinInfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    TdChuJinInfo.Result = false;
                    TdChuJinInfo.Desc = ComFunction.NotRightUser;
                    return TdChuJinInfo;
                }
                #endregion

                string AndStr = string.Empty;
                string PartSearchCondition = string.Empty;
                string ParentOrgID = string.Empty;
                if (UserType.OrgType == TdUser.UType && !string.IsNullOrEmpty(TdUser.OrgId))
                {
                    PartSearchCondition = " and orgid in (select orgid from #tmp) ";
                    ParentOrgID = TdUser.OrgId;
                }

                //if (Cjqc.PayStartTime != null)
                //{
                //    AndStr += string.Format(" and FkTime>='{0}' ", Cjqc.PayStartTime.ToString("yyyy-MM-dd"));
                //}

                //if (Cjqc.PayEndTime != null)
                //{
                //    AndStr += string.Format(" and FkTime<='{0}' ", Cjqc.PayEndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                //}

                if (!string.IsNullOrEmpty(Cjqc.Account)) //交易账号不为空 表示查询该用户的历史单 否则查询所有用户的历史单
                {
                    AndStr += string.Format(" and account like '{0}%' ", Cjqc.Account);
                }
                if ("ALL" != Cjqc.State.ToUpper())
                {
                    AndStr += string.Format(" and State='{0}' ", Cjqc.State);
                }
                if (!string.IsNullOrEmpty(Cjqc.OrgName))
                {
                    //AndStr += string.Format(" and [orgname] like '{0}%' ", Cjqc.OrgName);
                    AndStr += string.Format(" and [orgid]= '{0}' ", Cjqc.OrgName);
                }
                string SumSelectList = "round(isnull(sum(Amt1),0),2) as Amt,round(isnull(sum(Amt2),0),2) as Amt2,round(isnull(sum(Amt3),0),2) as Amt3  ";

                string selectlist = "telephone,orgname,account,ApplyId, UserId, BankCard, CardName, OpenBank, Amt, ApplyTime, FkTime, State,errMsg,UserName ";


                string SearchCondition = string.Format("where ((ApplyTime >= '{0}' and ApplyTime <='{1}') or (FkTime >= '{2}' and FkTime <='{3}')) {4} {5}",
                    Cjqc.StartTime.ToString("yyyy-MM-dd"), Cjqc.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    Cjqc.PayStartTime.ToString("yyyy-MM-dd"), Cjqc.PayEndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), AndStr, PartSearchCondition);

                dbreader = DbHelper.RunProcedureGetDataReader("GetRecordFromPageExWithSumEx",
                     new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@selectlist",DbParameterType.String,selectlist,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SumSelectList",DbParameterType.String,SumSelectList,ParameterDirection.Input),
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SumTableSource",DbParameterType.String,"V_Trade_ChuJinEx",ParameterDirection.Input), //汇总字段列表的表名或视图表 
                       DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableSource",DbParameterType.String,"V_Trade_ChuJin",ParameterDirection.Input), //表名或视图表 
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableOrder",DbParameterType.String,"a",ParameterDirection.Input), //排序后的表名称 即子查询结果集的别名
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SearchCondition",DbParameterType.String,SearchCondition,ParameterDirection.Input),
                          DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@OrderExpression",DbParameterType.String,"order by ApplyTime desc",ParameterDirection.Input),//排序 表达式
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ParentOrgID",DbParameterType.String,ParentOrgID,ParameterDirection.Input),//父级组织ID
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageIndex",DbParameterType.Int,pageindex,ParameterDirection.Input),
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageSize",DbParameterType.Int,pagesize,ParameterDirection.Input),
                            OutputParam});
                TdChuJinInfo.TdChuJinList = new List<TradeChuJin>();
                while (dbreader.Read())
                {
                    TradeChuJin tdcj = new TradeChuJin();
                    tdcj.Telephone = System.DBNull.Value != dbreader["telephone"] ? dbreader["telephone"].ToString() : string.Empty;
                    tdcj.OrgName = System.DBNull.Value != dbreader["orgname"] ? dbreader["orgname"].ToString() : string.Empty;
                    tdcj.Account = System.DBNull.Value != dbreader["account"] ? dbreader["account"].ToString() : string.Empty;
                    tdcj.UserName = System.DBNull.Value != dbreader["UserName"] ? dbreader["UserName"].ToString() : string.Empty;
                    tdcj.UserId = System.DBNull.Value != dbreader["UserId"] ? dbreader["UserId"].ToString() : string.Empty;
                    tdcj.CardName = System.DBNull.Value != dbreader["CardName"] ? dbreader["CardName"].ToString() : string.Empty;
                    tdcj.ApplyId = System.DBNull.Value != dbreader["ApplyId"] ? Convert.ToInt32(dbreader["ApplyId"]) : 0;
                    tdcj.BankCard = System.DBNull.Value != dbreader["BankCard"] ? dbreader["BankCard"].ToString() : string.Empty;

                    tdcj.Amt = System.DBNull.Value != dbreader["Amt"] ? Convert.ToDouble(dbreader["Amt"]) : 0;
                    tdcj.ApplyTime = System.DBNull.Value != dbreader["ApplyTime"] ? Convert.ToDateTime(dbreader["ApplyTime"]) : DateTime.MinValue;
                    tdcj.OpenBank = System.DBNull.Value != dbreader["OpenBank"] ? dbreader["OpenBank"].ToString() : string.Empty;
                    tdcj.State = System.DBNull.Value != dbreader["State"] ? dbreader["State"].ToString() : string.Empty;
                    tdcj.ErrMsg = System.DBNull.Value != dbreader["errMsg"] ? dbreader["errMsg"].ToString() : string.Empty;
                    if (System.DBNull.Value == dbreader["FkTime"])
                    {
                        tdcj.FkTime = null;
                    }
                    else
                    {
                        tdcj.FkTime = Convert.ToDateTime(dbreader["FkTime"]); ;
                    }
                    TdChuJinInfo.TdChuJinList.Add(tdcj);
                }
                if (dbreader.NextResult())//前进到下一结果集 
                {
                    if (dbreader.Read()) //获取汇总数据
                    {
                        TdChuJinInfo.Amt = System.DBNull.Value != dbreader["Amt"] ? Convert.ToDouble(dbreader["Amt"]) : 0;
                        TdChuJinInfo.Amt2 = System.DBNull.Value != dbreader["Amt2"] ? Convert.ToDouble(dbreader["Amt2"]) : 0;
                        TdChuJinInfo.Amt3 = System.DBNull.Value != dbreader["Amt3"] ? Convert.ToDouble(dbreader["Amt3"]) : 0;
                    }
                }
                TdChuJinInfo.Result = true;
                TdChuJinInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (null != TdChuJinInfo.TdChuJinList && TdChuJinInfo.TdChuJinList.Count > 0)
                {
                    TdChuJinInfo.TdChuJinList.Clear();
                }
                TdChuJinInfo.Result = false;
                TdChuJinInfo.Desc = "查询失败";
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                    page = Convert.ToInt32(OutputParam.Value);
                }
            }

            return TdChuJinInfo;
        }

        /// <summary>
        /// 资金报表
        /// </summary>
        /// <param name="Fcqc"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public FundChangeInfo GetMultiFundChangeWithPage(FcQueryCon Fcqc, int pageindex, int pagesize, ref int page)
        {
            FundChangeInfo FdInfo = new FundChangeInfo();

            System.Data.Common.DbDataReader dbreader = null;
            System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                "@PageCount", DbParameterType.Int, 0, ParameterDirection.Output);
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期


                if (!ComFunction.ExistUserLoginID(Fcqc.LoginID, ref TdUser))
                {
                    FdInfo.Result = false;
                    FdInfo.Desc = ResCode.UL003Desc;
                    return FdInfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    FdInfo.Result = false;
                    FdInfo.Desc = ComFunction.NotRightUser;
                    return FdInfo;
                }
                #endregion

                string AndStr = string.Empty;
                string PartSearchCondition = string.Empty;
                string ParentOrgID = string.Empty;
                if (UserType.OrgType == TdUser.UType && !string.IsNullOrEmpty(TdUser.OrgId))
                {
                    //AndStr = string.Format("and [OrgId] in ({0}) ", ComFunction.GetOrgIds(RefTdUser.OrgId));
                    PartSearchCondition = " and orgid in (select orgid from #tmp) ";
                    ParentOrgID = TdUser.OrgId;
                }
                if (!string.IsNullOrEmpty(Fcqc.Account)) //交易账号不为空 表示查询该用户的历史单 否则查询所有用户的历史单
                {
                    AndStr = string.Format(" and account like '{0}%' ", Fcqc.Account);
                }
                if ("ALL" != Fcqc.Reason.ToUpper())
                {
                    AndStr += string.Format(" and Reason='{0}' ", Fcqc.Reason);
                }
                if (!string.IsNullOrEmpty(Fcqc.OrgName))
                {
                    //AndStr += string.Format(" and [orgname] like '{0}%' ", Fcqc.OrgName);
                    AndStr += string.Format(" and [orgid]='{0}' ", Fcqc.OrgName);
                }
                string SumSelectList = "round(isnull(sum(cv1),0),2) as Amt,round(isnull(sum(cv2),0),2) as Amt2,round(isnull(sum(cv3),0),2) as Amt3," +
                                       "round(isnull(sum(cv4),0),2) as Amt4 ,round(isnull(sum(cv5),0),2) as Amt5 ,round(isnull(sum(cv6),0),2) as Amt6 ";

                string selectlist = "username,telephone,reason,orgname,account,ChangeValue,OperTime ";


                string SearchCondition = string.Format("where OperTime >= '{0}' and OperTime <='{1}' {2} {3}",
                    Fcqc.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), Fcqc.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), AndStr, PartSearchCondition);

                dbreader = DbHelper.RunProcedureGetDataReader("GetRecordFromPageExWithSumEx",
                     new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@selectlist",DbParameterType.String,selectlist,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SumSelectList",DbParameterType.String,SumSelectList,ParameterDirection.Input),
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SumTableSource",DbParameterType.String,"V_Fund_ChangeEx",ParameterDirection.Input), //汇总字段列表的表名或视图表 
                       DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableSource",DbParameterType.String,"V_Fund_Change",ParameterDirection.Input), //表名或视图表 
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableOrder",DbParameterType.String,"a",ParameterDirection.Input), //排序后的表名称 即子查询结果集的别名
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SearchCondition",DbParameterType.String,SearchCondition,ParameterDirection.Input),
                          DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@OrderExpression",DbParameterType.String,"order by OperTime desc",ParameterDirection.Input),//排序 表达式
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ParentOrgID",DbParameterType.String,ParentOrgID,ParameterDirection.Input),//父级组织ID
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageIndex",DbParameterType.Int,pageindex,ParameterDirection.Input),
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageSize",DbParameterType.Int,pagesize,ParameterDirection.Input),
                            OutputParam});
                FdInfo.FundChangeList = new List<FundChange>();
                while (dbreader.Read())
                {
                    FundChange fd = new FundChange();
                    fd.Telephone = System.DBNull.Value != dbreader["telephone"] ? dbreader["telephone"].ToString() : string.Empty;
                    fd.OrgName = System.DBNull.Value != dbreader["orgname"] ? dbreader["orgname"].ToString() : string.Empty;
                    fd.Account = System.DBNull.Value != dbreader["account"] ? dbreader["account"].ToString() : string.Empty;
                    fd.UserName = System.DBNull.Value != dbreader["username"] ? dbreader["username"].ToString() : string.Empty;

                    fd.Amt = System.DBNull.Value != dbreader["ChangeValue"] ? Convert.ToDouble(dbreader["ChangeValue"]) : 0;
                    fd.OpTime = System.DBNull.Value != dbreader["OperTime"] ? Convert.ToDateTime(dbreader["OperTime"]) : DateTime.MinValue;
                    fd.Reason = System.DBNull.Value != dbreader["reason"] ? dbreader["reason"].ToString() : string.Empty;


                    FdInfo.FundChangeList.Add(fd);
                }
                if (dbreader.NextResult())//前进到下一结果集 
                {
                    if (dbreader.Read()) //获取汇总数据
                    {
                        FdInfo.Amt = System.DBNull.Value != dbreader["Amt"] ? Convert.ToDouble(dbreader["Amt"]) : 0;
                        FdInfo.Amt2 = System.DBNull.Value != dbreader["Amt2"] ? Convert.ToDouble(dbreader["Amt2"]) : 0;
                        FdInfo.Amt3 = System.DBNull.Value != dbreader["Amt3"] ? Convert.ToDouble(dbreader["Amt3"]) : 0;
                        FdInfo.Amt4 = System.DBNull.Value != dbreader["Amt4"] ? Convert.ToDouble(dbreader["Amt4"]) : 0;
                        FdInfo.Amt5 = System.DBNull.Value != dbreader["Amt5"] ? Convert.ToDouble(dbreader["Amt5"]) : 0;
                        FdInfo.Amt6 = System.DBNull.Value != dbreader["Amt6"] ? Convert.ToDouble(dbreader["Amt6"]) : 0;
                    }
                }
                FdInfo.Result = true;
                FdInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (null != FdInfo.FundChangeList && FdInfo.FundChangeList.Count > 0)
                {
                    FdInfo.FundChangeList.Clear();
                }
                FdInfo.Result = false;
                FdInfo.Desc = "查询失败";
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                    page = Convert.ToInt32(OutputParam.Value);
                }
            }
            return FdInfo;
        }

        /// <summary>
        /// 出金付款处理
        /// </summary>
        /// <param name="ApplyId"></param>
        /// <param name="LoginId"></param>
        /// <param name="state">处理状态，"1"-已付款,"2"已拒绝 "3"处理中 "4"处理失败</param>
        /// <returns></returns>
        public ResultDesc ProcessChuJin(int ApplyId, String LoginId,ref string state)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;
                #endregion
                List<string> sqlList = new List<string>();
                StringBuilder strbld = new StringBuilder();

                #region 暂时作废
                //string ret_cod = string.Empty;
                //string ret_msg = string.Empty;
                //string ret_cod2 = string.Empty;//中间状态2000 2001 2003 2005 2007 2008
                //string ret_msg2 = string.Empty;
                //int itmp = 0;//临时变量
                //#region 调银行接口
                //ComFunction.ProcessChuJin(ApplyId, ref ret_cod, ref ret_msg, ref ret_cod2, ref ret_msg2);
                //#endregion

                //strbld.AppendFormat("update Trade_ChuJin set ");
                //if ("0000" == ret_cod && "0000" == ret_cod2)
                //{
                //    strbld.AppendFormat(" State=1, FkTime='{0}' ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                //    strbld.AppendFormat(" where ApplyId={0}", ApplyId);
                //    sqlList.Add(strbld.ToString());
                //}
                //else if ("2000" == ret_cod2 || "2001" == ret_cod2 || "2003" == ret_cod2 || "2005" == ret_cod2 || "2007" == ret_cod2 || "2008" == ret_cod2)
                //{
                //    strbld.AppendFormat(" FkTime='{0}',", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                //    strbld.AppendFormat(" State=3 where ApplyId={0}", ApplyId);//状态改为正在处理
                //    sqlList.Add(strbld.ToString());
                //    itmp = 1;
                //}
                ////失败代码0001 0002 1000 1001 1002 1999 2002 2004 2006
                //else if ("0001" == ret_cod2 || "0002" == ret_cod2 || "1000" == ret_cod2 || "1001" == ret_cod2 || "1002" == ret_cod2
                //    || "1999" == ret_cod2 || "2002" == ret_cod2 || "2004" == ret_cod2 || "2006" == ret_cod2)
                //{
                //    strbld.AppendFormat(" State=4, FkTime='{0}' ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                //    strbld.AppendFormat(" where ApplyId={0}", ApplyId);
                //    sqlList.Add(strbld.ToString());
                //    itmp = 2;
                //}
                //else
                //{
                //    rsdc.Result = false;
                //    rsdc.Desc = "付款失败!";
                //    return rsdc;
                //} 
                #endregion

                #region 改为微信支付后，付款功能直接修改状态为处理中
                strbld.AppendFormat("update Trade_ChuJin set ");
                //strbld.AppendFormat(" FkTime='{0}',", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                if (ConfigurationManager.AppSettings["VersionFlag"] == "HFB")
                {
                    strbld.AppendFormat(" State=1 where ApplyId={0}", ApplyId); //状态改为已付款（黑琥珀）
                    state = "1";
                }

                else
                {
                    strbld.AppendFormat(" State=3 where ApplyId={0}", ApplyId);//状态改为正在处理
                    state = "3";
                }
                    
                sqlList.Add(strbld.ToString());
                #endregion

                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}出金处理:ApplyId={0}", ApplyId, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    com.individual.helper.LogNet4.WriteMsg("付款已处理，但数据库sql执行失败了!");
                    state = "4";
                }
                rsdc.Result = true;
                rsdc.Desc = "处理中";
                //if (1 == itmp)
                //{
                //    rsdc.Desc = "3";//表示正在处理
                //}
                //else if (2 == itmp)
                //{
                //    rsdc.Desc = "4";//表示交易处理失败
                //}
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "付款失败!";
                state = "4";
            }
            return rsdc;
        }

        /// <summary>
        /// 拒绝出金
        /// </summary>
        /// <param name="ApplyId"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        public ResultDesc RefusedChuJin(int ApplyId, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            int result = 0;
            System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                "@Result", DbParameterType.Int, result, ParameterDirection.Output);
            try
            {
                TradeUser TdUser = new TradeUser();

                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;
                #endregion
                DbHelper.RunProcedureExecuteSql("Proc_RefusedChuJin",
                   new System.Data.Common.DbParameter[]{
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                 "@ApplyID", DbParameterType.Int, ApplyId, ParameterDirection.Input),
                    DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                 "@OperUser", DbParameterType.String, operUser, ParameterDirection.Input),
                    OutputParam});
                result = Convert.ToInt32(OutputParam.Value);
                if (0 == result)
                {
                    rsdc.Result = false;
                    rsdc.Desc = "操作失败";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "操作成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "操作失败!";
            }
            return rsdc;
        }

        /// <summary>
        /// 解约查询
        /// </summary>
        /// <param name="JYqc"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public TradeJieYueInfo GetMultiTradeJieYueWithPage(JYQueryCon JYqc, int pageindex, int pagesize, ref int page)
        {
            TradeJieYueInfo TdJieYueInfo = new TradeJieYueInfo();

            System.Data.Common.DbDataReader dbreader = null;
            System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                "@PageCount", DbParameterType.Int, 0, ParameterDirection.Output);
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期


                if (!ComFunction.ExistUserLoginID(JYqc.LoginID, ref TdUser))
                {
                    TdJieYueInfo.Result = false;
                    TdJieYueInfo.Desc = ResCode.UL003Desc;
                    return TdJieYueInfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    TdJieYueInfo.Result = false;
                    TdJieYueInfo.Desc = ComFunction.NotRightUser;
                    return TdJieYueInfo;
                }
                #endregion

                string AndStr = string.Empty;
                string PartSearchCondition = string.Empty;
                string ParentOrgID = string.Empty;
                if (UserType.OrgType == TdUser.UType && !string.IsNullOrEmpty(TdUser.OrgId))
                {
                    PartSearchCondition = " and orgid in (select orgid from #tmp) ";
                    ParentOrgID = TdUser.OrgId;
                }


                if (!string.IsNullOrEmpty(JYqc.Account)) //交易账号不为空 表示查询该用户的历史单 否则查询所有用户的历史单
                {
                    AndStr += string.Format(" and account like '{0}%' ", JYqc.Account);
                }
                if ("ALL" != JYqc.State.ToUpper())
                {
                    AndStr += string.Format(" and State='{0}' ", JYqc.State);
                }
                if (!string.IsNullOrEmpty(JYqc.OrgName))
                {
                    //AndStr += string.Format(" and [orgname] like '{0}%' ", JYqc.OrgName);
                    AndStr += string.Format(" and [orgid]='{0}' ", JYqc.OrgName);
                }
                string SubSelectList = "telephone,orgname,account,ApplyId, UserId, BankCard, CardName, OpenBank, ApplyTime, SHTime, State ";

                string selectlist = "telephone,orgname,account,ApplyId, UserId, BankCard, CardName, OpenBank, ApplyTime, SHTime, State ";


                string SearchCondition = string.Format("where ApplyTime >= '{0}' and ApplyTime <='{1}' {2} {3}",
                    JYqc.StartTime.ToString("yyyy-MM-dd"), JYqc.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), AndStr, PartSearchCondition);

                dbreader = DbHelper.RunProcedureGetDataReader("GetRecordFromPageEx",
                     new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@selectlist",DbParameterType.String,selectlist,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SubSelectList",DbParameterType.String,SubSelectList,ParameterDirection.Input),
                       DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableSource",DbParameterType.String,"V_Trade_JieYue",ParameterDirection.Input), //表名或视图表 
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableOrder",DbParameterType.String,"a",ParameterDirection.Input), //排序后的表名称 即子查询结果集的别名
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SearchCondition",DbParameterType.String,SearchCondition,ParameterDirection.Input),
                          DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@OrderExpression",DbParameterType.String,"order by ApplyTime desc",ParameterDirection.Input),//排序 表达式
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ParentOrgID",DbParameterType.String,ParentOrgID,ParameterDirection.Input),//父级组织ID
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageIndex",DbParameterType.Int,pageindex,ParameterDirection.Input),
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageSize",DbParameterType.Int,pagesize,ParameterDirection.Input),
                            OutputParam});
                TdJieYueInfo.TdJieYueList = new List<TradeJieYue>();
                while (dbreader.Read())
                {
                    TradeJieYue tdjy = new TradeJieYue();
                    tdjy.Telephone = System.DBNull.Value != dbreader["telephone"] ? dbreader["telephone"].ToString() : string.Empty;
                    tdjy.OrgName = System.DBNull.Value != dbreader["orgname"] ? dbreader["orgname"].ToString() : string.Empty;
                    tdjy.Account = System.DBNull.Value != dbreader["account"] ? dbreader["account"].ToString() : string.Empty;
                    tdjy.UserId = System.DBNull.Value != dbreader["UserId"] ? dbreader["UserId"].ToString() : string.Empty;
                    tdjy.CardName = System.DBNull.Value != dbreader["CardName"] ? dbreader["CardName"].ToString() : string.Empty;
                    tdjy.ApplyId = System.DBNull.Value != dbreader["ApplyId"] ? Convert.ToInt32(dbreader["ApplyId"]) : 0;
                    tdjy.BankCard = System.DBNull.Value != dbreader["BankCard"] ? dbreader["BankCard"].ToString() : string.Empty;

                    tdjy.ApplyTime = System.DBNull.Value != dbreader["ApplyTime"] ? Convert.ToDateTime(dbreader["ApplyTime"]) : DateTime.MinValue;
                    tdjy.OpenBank = System.DBNull.Value != dbreader["OpenBank"] ? dbreader["OpenBank"].ToString() : string.Empty;
                    tdjy.State = System.DBNull.Value != dbreader["State"] ? dbreader["State"].ToString() : string.Empty;

                    if (System.DBNull.Value == dbreader["SHTime"])
                    {
                        tdjy.SHTime = null;
                    }
                    else
                    {
                        tdjy.SHTime = Convert.ToDateTime(dbreader["SHTime"]); ;
                    }
                    TdJieYueInfo.TdJieYueList.Add(tdjy);
                }
                TdJieYueInfo.Result = true;
                TdJieYueInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (null != TdJieYueInfo.TdJieYueList && TdJieYueInfo.TdJieYueList.Count > 0)
                {
                    TdJieYueInfo.TdJieYueList.Clear();
                }
                TdJieYueInfo.Result = false;
                TdJieYueInfo.Desc = "查询失败";
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                    page = Convert.ToInt32(OutputParam.Value);
                }
            }
            return TdJieYueInfo;
        }

        /// <summary>
        /// 解约处理-审核
        /// </summary>
        /// <param name="ApplyId"></param>
        /// <param name="userid"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        public ResultDesc ProcessJieYue(int ApplyId, string userid, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;
                #endregion
                List<string> sqlList = new List<string>();
                StringBuilder strbld = new StringBuilder();
                strbld.AppendFormat("update Trade_JieYue set ");

                strbld.AppendFormat("State=1, SHTime='{0}' ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                strbld.AppendFormat(" where ApplyId={0}", ApplyId);
                sqlList.Add(strbld.ToString());
                //通过审核 需要把Trade_fundinfo表的state状态改为0 并且清空持卡人，银行卡号，开户行
                sqlList.Add(string.Format("update trade_fundinfo set state='0',AccountName='',BankCard='',OpenBank='' where userid='{0}'", userid));
                //sqlList.Add(string.Format("update trade_fundinfo set state='0' where userid='{0}'", userid));//不清空持卡人，银行卡号，开户行
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}解约处理-审核:ApplyId={0}", ApplyId, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "审核失败";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "审核成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "审核失败!";
            }
            return rsdc;
        }

        /// <summary>
        /// 解约处理-拒绝
        /// </summary>
        /// <param name="ApplyId"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        public ResultDesc RefusedJieYue(int ApplyId, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            int result = 0;
            System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                "@Result", DbParameterType.Int, result, ParameterDirection.Output);
            try
            {
                TradeUser TdUser = new TradeUser();

                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;
                #endregion
                List<string> sqlList = new List<string>();
                StringBuilder strbld = new StringBuilder();
                strbld.AppendFormat("update Trade_JieYue set ");

                strbld.AppendFormat("State=2, SHTime='{0}' ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                strbld.AppendFormat(" where ApplyId={0}", ApplyId);
                sqlList.Add(strbld.ToString());
                string ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}解约处理-拒绝:ApplyId={0}", ApplyId, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "操作失败";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "操作成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "操作失败!";
            }
            return rsdc;
        }

        /// <summary>
        /// 居间管理
        /// </summary>
        /// <param name="JJqc">查询条件</param>
        /// <returns></returns>
        public TradeJuJianInfo GetTradeJuJianInfo(JJQueryCon JJqc)
        {
            TradeJuJianInfo TdInfo = new TradeJuJianInfo();
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(JJqc.LoginID, ref TdUser))
                {
                    TdInfo.Result = false;
                    TdInfo.Desc = ResCode.UL003Desc;
                    return TdInfo;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    TdInfo.Result = false;
                    TdInfo.Desc = ComFunction.NotRightUser;
                    return TdInfo;
                }

                #endregion

                string ParentOrgID = string.Empty;
                if (UserType.OrgType == TdUser.UType && !string.IsNullOrEmpty(TdUser.OrgId))
                {
                    ParentOrgID = TdUser.OrgId;
                }

                TdInfo.TdJuJianList = new List<TradeJuJian>();
                ComFunction.GetTradeJuJianList(JJqc, ParentOrgID, ref TdInfo);
                TdInfo.Result = true;
                TdInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                TdInfo.Result = false;
                TdInfo.Desc = "查询居间管理失败";
            }
            return TdInfo;
        }

        /// <summary>
        /// 获取子级组织编码
        /// </summary>
        /// <param name="orgid">上级组织orgid</param>
        /// <param name="Telephone">上级组织编码</param>
        /// <returns></returns>
        public string GetOrgcode(string orgid, string Telephone)
        {
            string orgcode = string.Empty;
            System.Data.Common.DbDataReader dbreader = null;
            try
            {
                TradeUser TdUser = new TradeUser();
                dbreader = DbHelper.RunProcedureGetDataReader("GetOrgCode",
                     new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@orgid",DbParameterType.String,orgid,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@telephone",DbParameterType.String,Telephone,ParameterDirection.Input)});

                if (dbreader.Read())
                {
                    orgcode = System.DBNull.Value != dbreader["OrgCode"] ? dbreader["OrgCode"].ToString() : string.Empty;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return orgcode;
        }
        /// <summary>
        /// 获取出入金和解约记录
        /// </summary>
        /// <returns></returns>
        public TradeMoneyInfo GetTradeMoneyInfo()
        {
            TradeMoneyInfo tminfo = new TradeMoneyInfo();
            tminfo.TradeMoneyList = new List<TradeMoney>();
            System.Data.Common.DbDataReader dbreader = null;
            try
            {
                TradeUser TdUser = new TradeUser();
                dbreader = DbHelper.RunProcedureGetDataReader("GetChuRuJinAndJieYue", null);

                while (dbreader.Read())
                {
                    TradeMoney tmy = new TradeMoney();
                    tmy.account = System.DBNull.Value != dbreader["account"] ? dbreader["account"].ToString() : string.Empty;
                    tmy.opertime = System.DBNull.Value != dbreader["opertime"] ? dbreader["opertime"].ToString() : string.Empty;
                    tmy.reason = System.DBNull.Value != dbreader["reason"] ? dbreader["reason"].ToString() : string.Empty;
                    tmy.state = System.DBNull.Value != dbreader["state"] ? dbreader["state"].ToString() : string.Empty;
                    tmy.oldvalue = System.DBNull.Value != dbreader["oldvalue"] ? dbreader["oldvalue"].ToString() : string.Empty;
                    tmy.newvalue = System.DBNull.Value != dbreader["newvalue"] ? dbreader["newvalue"].ToString() : string.Empty;
                    tmy.changevalue = System.DBNull.Value != dbreader["changevalue"] ? dbreader["changevalue"].ToString() : string.Empty;
                    tminfo.TradeMoneyList.Add(tmy);
                }
                tminfo.Result = true;
                tminfo.Desc = "查询成功";
            }
            catch (Exception)
            {
                tminfo.Result = false;
                tminfo.Desc = "未获取数据";
                tminfo.TradeMoneyList.Clear();
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }

            return tminfo;
        }

        /// <summary>
        /// 获取银行集合
        /// </summary>
        /// <returns></returns>
        public List<TradeBank> GetTradeBank()
        {
            System.Data.Common.DbDataReader dbreader = DbHelper.ExecuteReader("select inter_type, inter_name, inter_bankcode, ConBankType, BankName from trade_bank where inter_type=1 ");

            List<TradeBank> banklist = new List<TradeBank>();
            try
            {
                while (dbreader.Read())
                {
                    TradeBank bank = new TradeBank();
                    bank.ConBankType = dbreader["ConBankType"].ToString();
                    bank.BankName = dbreader["BankName"].ToString();
                    banklist.Add(bank);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                dbreader.Close();
                dbreader.Dispose();
            }
            return banklist;
        }

        #region 客户分组相关接口
        /// <summary>
        /// 2.3.4.6	删除客户组的客户
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserGroupId"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        public ResultDesc DelUserFromUserGroups(string UserId, string UserGroupId, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();
                sqlList.Add(string.Format("delete from Trade_User_Group where UserId='{0}' and UserGroupId='{1}'", UserId, UserGroupId));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "删除客户出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "删除客户成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "删除客户失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 2.3.4.5	添加客户到客户组
        /// </summary>
        /// <param name="account">客户账号</param>
        /// <param name="UserGroupId">客户组ID</param>
        /// <param name="LoginId"></param>
        /// <param name="tduser">添加成功后,返回的被添加的客户的信息</param>
        /// <returns></returns>
        public ResultDesc AddUserToUserGroups(string account, string UserGroupId, String LoginId, ref TradeUser tduser)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();
                string UserId = ComFunction.GetUserId(account);
                if (string.IsNullOrEmpty(UserId))
                {
                    rsdc.Result = false;
                    rsdc.Desc = string.Format("帐号{0}不存在", account);
                    return rsdc;
                }
                if (ComFunction.ExistUserInTheGroup(UserId, UserGroupId))
                {
                    rsdc.Result = false;
                    rsdc.Desc = string.Format("帐号{0}已经添加到该组了", account);
                    return rsdc;
                }
                tduser = ComFunction.GetTradeUserByUserid(UserId);
                sqlList.Add(string.Format("delete from Trade_User_Group where UserId='{0}' ", UserId));
                sqlList.Add(string.Format("insert into Trade_User_Group(UserId, UserGroupId) values('{0}','{1}') ", UserId, UserGroupId));
                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "添加客户出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "添加客户成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "添加客户失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 2.3.4.4	修改客户组
        /// </summary>
        /// <param name="ugs"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        public ResultDesc ModifyUserGroups(UserGroups ugs, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion

                if (string.IsNullOrEmpty(ugs.UserGroupName))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "客户组名称不能为空";
                    return rsdc;
                }
                List<string> sqlList = new List<string>();
                StringBuilder strbld = new StringBuilder();
                strbld.Append("update Trade_UserGroups set ");
                strbld.AppendFormat(" UserGroupName='{0}',", string.IsNullOrEmpty(ugs.UserGroupName) ? string.Empty : ugs.UserGroupName);
                strbld.AppendFormat(" IsDefaultGroup={0},", ugs.IsDefaultGroup);
                strbld.AppendFormat(" AfterSecond={0},", ugs.AfterSecond);
                strbld.AppendFormat(" PlaceOrderSlipPoint={0},", ugs.PlaceOrderSlipPoint);
                strbld.AppendFormat(" FlatOrderSlipPoint={0},", ugs.FlatOrderSlipPoint);
                strbld.AppendFormat(" DelayPlaceOrder={0},", ugs.DelayPlaceOrder);
                strbld.AppendFormat(" DelayFlatOrder={0} ", ugs.DelayFlatOrder);
                strbld.AppendFormat(" where UserGroupId='{0}'", ugs.UserGroupId);
                sqlList.Add(strbld.ToString());
                if (1 == ugs.IsDefaultGroup)//如果修改的组被设置为默认组时，需要把其他组全部修改为非默认组
                {
                    sqlList.Add(string.Format("update Trade_UserGroups set IsDefaultGroup=0 where UserGroupId<>'{0}'", ugs.UserGroupId));
                }
                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改客户组出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "修改客户组成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "修改客户组失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 2.3.4.3	删除客户组
        /// </summary>
        /// <param name="UserGroupId"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        public ResultDesc DelUserGroups(string UserGroupId, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                List<string> sqlList = new List<string>();
                sqlList.Add(string.Format("delete from Trade_UserGroups where UserGroupId='{0}'", UserGroupId));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "删除客户组出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "删除客户组成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "删除客户组失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 2.3.4.2	添加客户组
        /// </summary>
        /// <param name="ugs"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        public ResultDesc AddUserGroups(UserGroups ugs, String LoginId)
        {
            ResultDesc rsdc = new ResultDesc();
            string operUser = string.Empty;//操作人
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                if (UserType.NormalType == TdUser.UType)
                {
                    rsdc.Result = false;
                    rsdc.Desc = ComFunction.NotRightUser;
                    return rsdc;
                }
                operUser = TdUser.Account;

                #endregion
                if (string.IsNullOrEmpty(ugs.UserGroupName))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "客户组名称不能为空";
                    return rsdc;
                }
                List<string> sqlList = new List<string>();
                sqlList.Add(string.Format(@"insert into Trade_UserGroups(UserGroupId, UserGroupName, IsDefaultGroup, AfterSecond, PlaceOrderSlipPoint,
                                FlatOrderSlipPoint, DelayPlaceOrder, DelayFlatOrder) values('{0}','{1}',{2},{3},{4},{5},{6},{7}) ",
                                ugs.UserGroupId, ugs.UserGroupName, ugs.IsDefaultGroup, ugs.AfterSecond, ugs.PlaceOrderSlipPoint,
                                ugs.FlatOrderSlipPoint, ugs.DelayPlaceOrder, ugs.DelayFlatOrder));
                if (1 == ugs.IsDefaultGroup)//如果新加的组为默认组时，需要把其他组全部修改为非默认组
                {
                    sqlList.Add(string.Format("update Trade_UserGroups set IsDefaultGroup=0 where UserGroupId<>'{0}'", ugs.UserGroupId));
                }
                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "添加客户组出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "添加客户组成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "添加客户组失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 客户分组查询
        /// </summary>
        /// <returns></returns>
        public UserGroupsInfo GetUserGroupsInfo()
        {
            System.Data.Common.DbDataReader dbreader = DbHelper.ExecuteReader(@"select UserGroupId, UserGroupName,IsDefaultGroup, AfterSecond, PlaceOrderSlipPoint, 
                    FlatOrderSlipPoint, DelayPlaceOrder, DelayFlatOrder from Trade_UserGroups ");
            UserGroupsInfo ugsinfo = new UserGroupsInfo();
            ugsinfo.UserGroupsInfoList = new List<UserGroups>();
            try
            {
                ugsinfo.Result = false;

                while (dbreader.Read())
                {
                    UserGroups ugs = new UserGroups();
                    ugs.UserGroupId = dbreader["UserGroupId"].ToString();
                    ugs.UserGroupName = System.DBNull.Value != dbreader["UserGroupName"] ? dbreader["UserGroupName"].ToString() : string.Empty;
                    ugs.IsDefaultGroup = System.DBNull.Value != dbreader["IsDefaultGroup"] ? Convert.ToInt32(dbreader["IsDefaultGroup"]) : 0;
                    ugs.AfterSecond = System.DBNull.Value != dbreader["AfterSecond"] ? Convert.ToInt32(dbreader["AfterSecond"]) : 0;
                    ugs.PlaceOrderSlipPoint = System.DBNull.Value != dbreader["PlaceOrderSlipPoint"] ? Convert.ToInt32(dbreader["PlaceOrderSlipPoint"]) : 0;
                    ugs.FlatOrderSlipPoint = System.DBNull.Value != dbreader["FlatOrderSlipPoint"] ? Convert.ToInt32(dbreader["FlatOrderSlipPoint"]) : 0;
                    ugs.DelayPlaceOrder = System.DBNull.Value != dbreader["DelayPlaceOrder"] ? Convert.ToDouble(dbreader["DelayPlaceOrder"]) : 0;
                    ugs.DelayFlatOrder = System.DBNull.Value != dbreader["DelayFlatOrder"] ? Convert.ToDouble(dbreader["DelayFlatOrder"]) : 0;
                    ugsinfo.UserGroupsInfoList.Add(ugs);
                }
                ugsinfo.Result = true;
                ugsinfo.Desc = "客户分组查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                ugsinfo.UserGroupsInfoList.Clear();
                ugsinfo.Result = false;
                ugsinfo.Desc = "客户分组查询失败";
            }
            finally
            {
                dbreader.Close();
                dbreader.Dispose();
            }
            return ugsinfo;
        }
        #endregion


        /// <summary>
        /// 获取体验券信息
        /// </summary>
        /// <param name="loginId">登录标识</param>
        /// <param name="type">类型</param>
        /// <param name="isEffciive">是否启用</param>
        /// <param name="endTime">到期时间</param>
        /// <returns>ExperienceInfo</returns>
        public ExperienceInfo GetExperienceInfo(string loginId, int type, int isEffciive, DateTime? endTime)
        {
            ExperienceInfo info = new ExperienceInfo();
            var tdUser = new TradeUser();
            try
            {
                #region 判断登陆标识是否过期
                if (!ComFunction.ExistUserLoginID(loginId, ref tdUser))
                {
                    info.Result = false;
                    info.Desc = ResCode.UL003Desc;
                    return info;
                }
                #endregion

                string sql = string.Format("SELECT * FROM [TRADE_EXPERIENCE] where 1=1");
                if (type != -1)
                    sql += " and EX_TYPE=" + type;
                if (isEffciive != -1)
                    sql += " and EX_EFFECTIVE=" + isEffciive;
                if (endTime != null)
                    sql += " and EX_EFFECTIVETIME <='" + endTime + "'";
                info.ExperienceList = ComFunction.GetExperienceList(sql);

                info.Result = true;
                info.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                info.Result = false;
                info.Desc = "查询失败";
            }
            return info;
        }

        /// <summary>
        /// 添加体验券
        /// </summary>
        /// <param name="loginId">登录标识</param>
        /// <param name="exp">体验券</param>
        /// <returns>ResultDesc</returns>
        public ResultDesc AddExperience(string loginId, Experience exp)
        {
            ResultDesc rsdc = new ResultDesc();
            var tdUser = new TradeUser();
            try
            {
                #region 判断登陆标识是否过期
                if (!ComFunction.ExistUserLoginID(loginId, ref tdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                #endregion
                var sqlList = new List<string>();
                sqlList.Add(string.Format("INSERT INTO [TRADE_EXPERIENCE]([EX_NAME] ,[EX_TYPE] ,[EX_ANMOUNT] ,[EX_RECHARGE] ,[EX_NUM] ,[EX_STARTDATE] ,[EX_ENDDATE] ," +
                             "[EX_CREATID] ,[EX_EFFECTIVE],[EX_EFFECTIVETIME]) VALUES ('{0}',{1},{2},{3},{4},'{5}','{6}','{7}',{8},'{9}')",
                             exp.Name, exp.Type, exp.Annount, exp.Rceharge, exp.Num, exp.StartDate,
                             exp.EndDate, exp.CreatID, exp.Effective, exp.EffectiveTime
                    ));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "添加体验券出错！";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "添加体验券成功！";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "";
            }
            return rsdc;
        }

        /// <summary>
        /// 编辑体验券
        /// </summary>
        /// <param name="loginId">登录标识</param>
        /// <param name="exp">体验券</param>
        /// <returns>ResultDesc</returns>
        public ResultDesc EditExperience(string loginId, Experience exp)
        {
            ResultDesc rsdc = new ResultDesc();
            var tdUser = new TradeUser();
            try
            {
                #region 判断登陆标识是否过期
                if (!ComFunction.ExistUserLoginID(loginId, ref tdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                #endregion
                var sqlList = new List<string>();
                exp.StartDate = exp.StartDate != null && exp.StartDate == DateTime.MinValue ? DateTime.MinValue : exp.StartDate;
                exp.EndDate = exp.EndDate != null && exp.EndDate == DateTime.MinValue ? DateTime.MinValue : exp.EndDate;
                exp.EffectiveTime = exp.EffectiveTime != null && exp.EffectiveTime == DateTime.MinValue ? DateTime.MinValue : exp.EffectiveTime;
                sqlList.Add(string.Format("UPDATE [TRADE_EXPERIENCE] SET [EX_NAME] ='{0}',[EX_TYPE] ={1},[EX_ANMOUNT] = {2} ,[EX_RECHARGE] ={3} ,[EX_NUM] = {4} ," +
                                          "[EX_STARTDATE] = '{5}',[EX_ENDDATE] = '{6}'  ,[EX_CREATID] ='{7}' ,[EX_EFFECTIVE] ={8} ,[EX_EFFECTIVETIME] ='{9}' WHERE ID ={10}",
                             exp.Name, exp.Type, exp.Annount, exp.Rceharge, exp.Num, exp.StartDate,
                             exp.EndDate, exp.CreatID, exp.Effective, exp.EffectiveTime,
                             exp.Id));
                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "编辑体验券出错！";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "编辑体验券成功！";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "";
            }
            return rsdc;
        }

        /// <summary>
        /// 删除体验券
        /// </summary>
        /// <param name="loginId">登录标识</param>
        /// <param name="id">体验券标识</param>
        /// <returns>ResultDesc</returns>
        public ResultDesc DelExperience(string loginId, int id)
        {
            ResultDesc rsdc = new ResultDesc();
            var tdUser = new TradeUser();
            try
            {
                #region 判断登陆标识是否过期
                if (!ComFunction.ExistUserLoginID(loginId, ref tdUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = ResCode.UL003Desc;
                    return rsdc;
                }
                #endregion
                var sqlList = new List<string>();
                sqlList.Add(string.Format("DELETE FROM [TRADE_EXPERIENCE] WHERE ID={0}", id));
                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "删除体验券出错！";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "删除体验券成功！";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "";
            }
            return rsdc;
        }
    }
}
