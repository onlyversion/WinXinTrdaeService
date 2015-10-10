using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using com.individual.helper;
using WcfInterface.model;
using WcfInterface.model.WJY;

namespace WcfInterface
{
    /// <summary>
    /// 微交易
    /// 马友春
    /// </summary>
    public partial class ComFunction
    {
        /// <summary>
        /// 获取微交易用户信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static Base_WUser GetWUser(string sql)
        {
            System.Data.Common.DbDataReader dbreader = null;
            Base_WUser wuser = new Base_WUser();
            try
            {
                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    wuser.AccountType = System.DBNull.Value != dbreader["AccountType"] ? dbreader["AccountType"].ToString() : string.Empty;
                    wuser.Name = System.DBNull.Value != dbreader["Name"] ? dbreader["Name"].ToString() : string.Empty;
                    wuser.PhoneNum = System.DBNull.Value != dbreader["PhoneNum"] ? dbreader["PhoneNum"].ToString() : string.Empty;
                    wuser.PWUserId = System.DBNull.Value != dbreader["PWUserId"] ? dbreader["PWUserId"].ToString() : string.Empty;
                    wuser.Ticket = System.DBNull.Value != dbreader["Ticket"] ? dbreader["Ticket"].ToString() : string.Empty;
                    wuser.Url = System.DBNull.Value != dbreader["Url"] ? dbreader["Url"].ToString() : string.Empty;
                    wuser.UserID = System.DBNull.Value != dbreader["UserID"] ? dbreader["UserID"].ToString() : string.Empty;
                    wuser.WAccount = System.DBNull.Value != dbreader["WAccount"] ? dbreader["WAccount"].ToString() : string.Empty;
                    wuser.WUserId = System.DBNull.Value != dbreader["WUserId"] ? dbreader["WUserId"].ToString() : string.Empty;

                    break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return wuser;
        }

        /// <summary>
        /// 获取交易用户信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static TradeUser GetTdUser(string sql)
        {
            System.Data.Common.DbDataReader dbreader = null;
            TradeUser tdUser = new TradeUser();
            try
            {
                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    tdUser.UserID = System.DBNull.Value != dbreader["UserID"] ? dbreader["UserID"].ToString() : string.Empty;
                    break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return tdUser;
        }

        /// <summary>
        /// 判断是否转为经纪人
        /// </summary>
        /// <param name="sql">查询SQL</param>
        /// <returns>bool</returns>
        public static bool IsCanEconomicMan(string sql)
        {
            System.Data.Common.DbDataReader dbreader = null;
            try
            {
                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    double countOccMoney = System.DBNull.Value != dbreader["countOccMoney"] ? Convert.ToDouble(dbreader["countOccMoney"]) : 0;
                    if (countOccMoney >= 0.1)
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return false;
        }

        /// <summary>
        /// 获取微交易用户UserID
        /// </summary>
        /// <param name="WUserId">WUserId</param>
        /// <returns>bool</returns>
        public static string GetWUserID(string WUserId)
        {
            System.Data.Common.DbDataReader dbreader = null;
            string id = "";
            try
            {
                dbreader = DbHelper.ExecuteReader(string.Format(@"select UserID from Base_WUser where WUserId='{0}'", WUserId));
                while (dbreader.Read())
                {
                    id = DBNull.Value != dbreader["UserID"] ? dbreader["UserID"].ToString() : "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return id;
        }

        /// <summary>
        /// 判断是否能返回保证金
        /// </summary>
        /// <param name="sql">查询SQL</param>
        /// <param name="CashFund">引用参数，可返回的保证金</param>
        /// <returns>bool</returns>
        public static bool IsCanReturnEconomicManCashFund(string sql, ref double CashFund)
        {
            System.Data.Common.DbDataReader dbreader = null;
            try
            {
                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    //double countOccMoney = System.DBNull.Value != dbreader["countOccMoney"] ? Convert.ToDouble(dbreader["countOccMoney"]) : 0;
                    //if (countOccMoney >= 100)
                    //    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return false;
        }


        /// <summary>
        /// 获取体验券
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<Experience> GetExperienceList(string sql)
        {
            System.Data.Common.DbDataReader dbreader = null;
            List<Experience> list = new List<Experience>();
            try
            {
                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    Experience exp = new Experience();
                    exp.Id = DBNull.Value != dbreader["Id"] ? Convert.ToInt16(dbreader["Id"].ToString()) : 0;
                    exp.Name = DBNull.Value != dbreader["EX_NAME"] ? dbreader["EX_NAME"].ToString() : string.Empty;
                    exp.Type = DBNull.Value != dbreader["EX_TYPE"] ? Convert.ToInt16(dbreader["EX_TYPE"].ToString()) : 0;
                    exp.Annount = DBNull.Value != dbreader["EX_ANMOUNT"] ? Convert.ToDecimal(dbreader["EX_ANMOUNT"].ToString()) : 0;
                    exp.Rceharge = DBNull.Value != dbreader["EX_RECHARGE"] ? Convert.ToDecimal(dbreader["EX_RECHARGE"].ToString()) : 0;
                    exp.Num = DBNull.Value != dbreader["EX_NUM"] ? Convert.ToInt16(dbreader["EX_NUM"].ToString()) : 0;
                    //if (dbreader["EX_STARTDATE"] != null && !string.IsNullOrEmpty(dbreader["EX_STARTDATE"].ToString()))
                    //    exp.StartDate = Convert.ToDateTime(dbreader["EX_STARTDATE"]);
                    //if (dbreader["EX_ENDDATE"] != null && !string.IsNullOrEmpty(dbreader["EX_ENDDATE"].ToString()))
                    //    exp.EndDate = Convert.ToDateTime(dbreader["EX_ENDDATE"]);
                    if (System.DBNull.Value != dbreader["EX_STARTDATE"])
                        exp.StartDate = Convert.ToDateTime(dbreader["EX_STARTDATE"]);
                    else
                        exp.StartDate = DateTime.MinValue;
                    if (System.DBNull.Value != dbreader["EX_ENDDATE"])
                        exp.EndDate = Convert.ToDateTime(dbreader["EX_ENDDATE"]);
                    else
                        exp.EndDate = DateTime.MinValue;
                    //exp.EndDate = System.DBNull.Value != dbreader["EX_ENDDATE"] ? Convert.ToDateTime(dbreader["EX_ENDDATE"]) : DateTime.MinValue;
                    exp.CreatID = DBNull.Value != dbreader["EX_CREATID"] ? dbreader["EX_CREATID"].ToString() : string.Empty;
                    exp.Effective = DBNull.Value != dbreader["EX_EFFECTIVE"] ? Convert.ToInt16(dbreader["EX_EFFECTIVE"].ToString()) : 0;
                    //exp.EffectiveTime = System.DBNull.Value != dbreader["EX_EFFECTIVETIME"] ? Convert.ToDateTime(dbreader["EX_EFFECTIVETIME"]) : DateTime.MinValue;
                    if (System.DBNull.Value != dbreader["EX_EFFECTIVETIME"])
                        exp.EffectiveTime = Convert.ToDateTime(dbreader["EX_EFFECTIVETIME"]);
                    else
                        exp.EffectiveTime = DateTime.MinValue;
                    list.Add(exp);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return list;
        }

        /// <summary>
        /// 判断会员是否能承接头寸
        /// </summary>
        /// <param name="orgid">组织机构ID</param>
        /// <param name="yingkui">盈亏</param>
        /// <param name="money">输出参数，如返回true则该值会员账户的余额，如返回false则该值为平台账户的余额</param>
        /// <returns>bool:true会员承接头寸;false平台承接头寸</returns>
        public static bool IsCanOrgTrade(string orgid, double yingkui,ref double money)
        {
            bool result = true;//默认为会员承担头寸
            System.Data.Common.DbDataReader dbreader = null;
            try
            {
                string sql = string.Format(@"select IsTrade from Base_Org where OrgID='{0}'",orgid);
                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    bool IsTrade = System.DBNull.Value != dbreader["IsTrade"] ? Convert.ToBoolean(dbreader["IsTrade"]) : false;
                    if (IsTrade == false)//会员不承担头寸，则由平台承担
                    {
                        result = false;//会员不承担头寸
                        //获取平台账户余额
                        sql = string.Format(@"select money from Trade_OrgFund where OrgID='system'");
                        dbreader = DbHelper.ExecuteReader(sql);
                        while (dbreader.Read())
                        {
                            money = System.DBNull.Value != dbreader["money"] ? Convert.ToDouble(dbreader["money"]) : 0;
                        }
                    }
                    else//会员承担头寸
                    {
                        //获取会员账户余额
                        sql = string.Format(@"select money from Trade_OrgFund where OrgID='{0}'", orgid);
                        dbreader = DbHelper.ExecuteReader(sql);
                        while (dbreader.Read())
                        {
                            money = System.DBNull.Value != dbreader["money"] ? Convert.ToDouble(dbreader["money"]) : 0;
                            if (money < yingkui) //如果此时会员账户余额不足以支付客户盈利，则由平台承接头寸
                            {
                                result = false;//会员单位不承担头寸，由平台承接
                                sql = string.Format(@"select money from Trade_OrgFund where OrgID='system'");
                                dbreader = DbHelper.ExecuteReader(sql);
                                while (dbreader.Read())
                                {
                                    money = System.DBNull.Value != dbreader["money"] ? Convert.ToDouble(dbreader["money"]) : 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return result;
        }
    }
}
