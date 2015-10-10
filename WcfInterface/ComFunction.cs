//*******************************************************************************
//  文 件 名：ComFunction.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/05
//
//  修改标识：
//  修改说明：
//*******************************************************************************

#region 命名空间引用

using System;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using System.ServiceModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WcfInterface.model;
using WcfInterface.ServiceReference1;
using WcfInterface.ServiceReference2;
using com.individual.helper;
using System.Net;

#endregion

namespace WcfInterface
{
    /// <summary>
    /// 通用函数
    /// </summary>
    public partial class ComFunction
    {
        public const string GetProductSql = @"SELECT 1 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='XAGUSD' union 
                                                SELECT 2 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='XAUUSD' union 
                                                SELECT 3 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='XPTUSD' union 
                                                SELECT 4 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='XPDUSD' union 
                                                SELECT 5 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='Copper' union 
                                                SELECT 6 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='UKOil' union 
                                                SELECT 7 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='USOil' union 
                                                SELECT 8 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='USDOLLAR' union 
                                                SELECT 9 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='EURUSD' union 
                                                SELECT 10 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='GBPUSD' union 
                                                SELECT 11 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='EURGBP' union 
                                                SELECT 12 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='USDJPY' union 
                                                SELECT 13 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='USDCHF' union
                                                SELECT 14 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='XCU' union
                                                SELECT 15 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='XSO' union
                                                SELECT 16 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='XSAG'                                                
order by [type],[unit] desc";
        public const string GetProductSqlEx = @"SELECT 1 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='XAGUSD' and state<>'4' union 
                                                SELECT 2 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='XAUUSD' and state<>'4' union 
                                                SELECT 3 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='XPTUSD' and state<>'4' union 
                                                SELECT 4 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='XPDUSD' and state<>'4' union 
                                                SELECT 5 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='Copper' and state<>'4' union 
                                                SELECT 6 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='UKOil' and state<>'4' union 
                                                SELECT 7 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='USOil' and state<>'4' union 
                                                SELECT 8 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='USDOLLAR' and state<>'4' union 
                                                SELECT 9 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='EURUSD' and state<>'4' union 
                                                SELECT 10 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='GBPUSD' and state<>'4' union 
                                                SELECT 11 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='EURGBP' and state<>'4' union 
                                                SELECT 12 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='USDJPY' and state<>'4' union 
                                                SELECT 13 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='USDCHF'  and state<>'4' union
                                                SELECT 14 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='XCU'  and state<>'4' union
                                                SELECT 15 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='XSO'  and state<>'4' union
                                                SELECT 16 as [type], [ProductName],[productcode],[goodscode],[pricecode],[adjustbase],[adjustcount],[pricedot],[valuedot],[setBase],[holdbase],[expressionfee],[sellfee],[maxprice],[minprice],[maxtime],[Ordemoneyfee],[Ordemoney],[buystoragefee],[sellstoragefee],[state],[unit],[lowerprice],[starttime],[endtime] FROM [Trade_Product] where pricecode='XSAG'  and state<>'4'                                               
order by [type],[unit] desc";

        #region 常量
        /// <summary>
        /// 黄金原料编码
        /// </summary>
        public const string AUFlg = "XAU";

        /// <summary>
        /// 白银原料编码
        /// </summary>
        public const string AGFlg = "XAG";

        /// <summary>
        /// 铂金原料编码
        /// </summary>
        public const string PTFlg = "XPT";

        /// <summary>
        /// 钯金原料编码
        /// </summary>
        public const string PDFlg = "XPD";

        /// <summary>
        /// 华夏银行接收交易请求并处理完成(返回结果代码是0000)
        /// </summary>
        public const string HuaxiaSuc = "0000";

        /// <summary>
        /// 订单
        /// </summary>
        public const string Order = "1";

        /// <summary>
        /// 挂单
        /// </summary>
        public const string Hold = "2";

        /// <summary>
        /// 历史订单
        /// </summary>
        public const string Order_His = "3";
        /// <summary>
        /// 挂单历史
        /// </summary>
        public const string Hold_His = "4";

        /// <summary>
        /// 普通用户不能进行此操作
        /// </summary>
        public const string NotRightUser = "您无权进行此操作";

        #endregion

        #region 静态属性
        /// <summary>
        /// Gets 该值等于0.000001
        /// </summary>
        public static double dzero
        {
            get { return 0.000001; }
        }

        /// <summary>
        /// Gets 风险率
        /// </summary>
        public static double fenxian_rate
        {
            get { return 1.0; }
        }

        /// <summary>
        /// Gets SQLSERVER连接字符串
        /// </summary>
        public static string SqlConnectionString
        {
            get { return EncryptionHelper.Decrypt(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); }
        }

        /// <summary>
        /// Gets port
        /// </summary>
        public static string port
        {
            get
            {
                return ConfigurationManager.AppSettings["port"];
            }
        }

        /// <summary>
        /// Gets revcport
        /// </summary>
        public static string recvport
        {
            get
            {
                return ConfigurationManager.AppSettings["recvport"];
            }
        }

        /// <summary>
        /// Gets ip
        /// </summary>
        public static string ip
        {
            get
            {
                return ConfigurationManager.AppSettings["ip"];
            }
        }

        /// <summary>
        /// Gets recvip
        /// </summary>
        public static string recvip
        {
            get
            {
                return ConfigurationManager.AppSettings["recvip"];
            }
        }

        /// <summary>
        /// Gets 报表在服务器本地的地址,最后不要加"\"
        /// </summary>
        public static string ReportFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["ReportFilePath"];
            }
        }


        /// <summary>
        /// Gets 导出的报表URL地址 配置时,最后记得加上"/",如:http://baobiao.gss999.com/
        /// </summary>
        public static string ReportAddr
        {
            get
            {
                return ConfigurationManager.AppSettings["ReportAddr"];
            }
        }

        /// <summary>
        /// 默认是否允许卖单入库,"1"表示允许,"0"表示不允许
        /// </summary>
        public static string AllowStore
        {
            get
            {
                return ConfigurationManager.AppSettings["AllowStore"];
            }
        }

        /// <summary>
        /// 要检测的手机客户端更新文件
        /// </summary>
        public static string UpdateFile
        {
            get
            {
                return ConfigurationManager.AppSettings["UpdateFile"];
            }
        }

        /// <summary>
        /// 手机客户端更新包下载地址
        /// </summary>
        public static string UpdateAddr
        {
            get
            {
                return ConfigurationManager.AppSettings["UpdateAddr"];
            }
        }

        /// <summary>
        /// 手机客户端，新闻公告详细内容URL主机地址，带有端口,例如192.168.0.23:8369
        /// </summary>
        public static string NewsHostAddr
        {
            get
            {
                return ConfigurationManager.AppSettings["NewsHostAddr"];
            }
        }


        /// <summary>
        /// 是否允许注册模拟账号 1表示允许,0表示不允许
        /// </summary>
        public static string AllowRegister
        {
            get
            {
                return ConfigurationManager.AppSettings["AllowRegister"];
            }
        }
        /// <summary>
        /// 0表示模拟接口，1表示交易接口
        /// </summary>
        public static string InterType
        {
            get
            {
                return ConfigurationManager.AppSettings["InterType"];
            }
        }

        /// <summary>
        /// 通联支付，出金地址
        /// </summary>
        public static string BankAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["BankAddress"];
            }
        }

        #endregion

        /// <summary>
        /// 查询用户是否存在
        /// </summary>
        /// <param name="loginfo">登陆信息</param>
        /// <param name="tradeAccount">用户账账号</param>
        /// <param name="tradePwd">密码</param>
        /// <param name="mac">MAC地址</param>
        /// <returns>存在返回true 否则返回false</returns>
        public static bool ListLogin(ref Loginfo loginfo, string tradeAccount, string tradePwd, string mac)
        {
            System.Data.Common.DbDataReader dbreader = null;
            bool IsSuc = false;
            try
            {
                //tradePwd = EncryptHelper.DesEncrypt(tradePwd);

                loginfo.TdUser = new TradeUser();

                string sql = "select LinkAdress,opentime,email,LastLoginTime, userId,userName,Account,PhoneNum,CardNum,OrderPhone,sex,CardType, MinTrade,OrderUnit," +
                             "MaxTrade,PermitRcash,PermitCcash,PermitDhuo,PermitHshou,PermitRstore,PermitDelOrder,orgid from Base_User where status='1' and usertype=3 " +
                             "and Account=@tradeAccount and LoginPwd=@tradePwd";
                //select                                                      userId,userName,tradeAccount,PhoneNum,CardNum,OrderPhone,sex,CardType, MinTrade,OrderUnit,MaxTrade,PermitRcash,PermitCcash,PermitDhuo,PermitHshou,PermitRstore,PermitDelOrder,
                dbreader = DbHelper.ExecuteReader(sql,
                    new System.Data.Common.DbParameter[]{
                       DbHelper.CreateDbParameter(DataBaseType.SqlServer,"@tradeAccount",DbParameterType.String,tradeAccount,ParameterDirection.Input),
                       DbHelper.CreateDbParameter(DataBaseType.SqlServer,"@tradePwd",DbParameterType.String,tradePwd,ParameterDirection.Input)
                   });
                if (dbreader.Read())
                {
                    loginfo.TdUser.MinTrade = System.DBNull.Value != dbreader["MinTrade"] ? Convert.ToDouble(dbreader["MinTrade"]) : 1;
                    loginfo.TdUser.OrderUnit = System.DBNull.Value != dbreader["OrderUnit"] ? Convert.ToDouble(dbreader["OrderUnit"]) : 1;
                    loginfo.TdUser.MaxTrade = System.DBNull.Value != dbreader["MaxTrade"] ? Convert.ToDouble(dbreader["MaxTrade"]) : 10;
                    loginfo.TdUser.PermitRcash = System.DBNull.Value != dbreader["PermitRcash"] ? Convert.ToBoolean(dbreader["PermitRcash"]) : false;
                    loginfo.TdUser.PermitCcash = System.DBNull.Value != dbreader["PermitCcash"] ? Convert.ToBoolean(dbreader["PermitCcash"]) : false;
                    loginfo.TdUser.PermitDhuo = System.DBNull.Value != dbreader["PermitDhuo"] ? Convert.ToBoolean(dbreader["PermitDhuo"]) : false;
                    loginfo.TdUser.PermitHshou = System.DBNull.Value != dbreader["PermitHshou"] ? Convert.ToBoolean(dbreader["PermitHshou"]) : false;
                    loginfo.TdUser.PermitRstore = System.DBNull.Value != dbreader["PermitRstore"] ? Convert.ToBoolean(dbreader["PermitRstore"]) : false;
                    loginfo.TdUser.PermitDelOrder = System.DBNull.Value != dbreader["PermitDelOrder"] ? Convert.ToBoolean(dbreader["PermitDelOrder"]) : false;

                    loginfo.TdUser.UserID = System.DBNull.Value != dbreader["UserID"] ? dbreader["UserID"].ToString() : string.Empty;
                    loginfo.TdUser.Account = System.DBNull.Value != dbreader["Account"] ? dbreader["Account"].ToString() : string.Empty;
                    loginfo.TdUser.UserName = System.DBNull.Value != dbreader["UserName"] ? dbreader["UserName"].ToString() : string.Empty;
                    loginfo.TdUser.PhoneNum = System.DBNull.Value != dbreader["PhoneNum"] ? dbreader["PhoneNum"].ToString() : string.Empty;
                    loginfo.TdUser.CardNum = System.DBNull.Value != dbreader["CardNum"] ? dbreader["CardNum"].ToString() : string.Empty;
                    loginfo.TdUser.OrderPhone = System.DBNull.Value != dbreader["OrderPhone"] ? dbreader["OrderPhone"].ToString() : string.Empty;
                    loginfo.TdUser.Email = System.DBNull.Value != dbreader["Email"] ? dbreader["Email"].ToString() : string.Empty;
                    loginfo.TdUser.LinkAdress = System.DBNull.Value != dbreader["LinkAdress"] ? dbreader["LinkAdress"].ToString() : string.Empty;
                    if (System.DBNull.Value != dbreader["OpenTime"])
                    {
                        loginfo.TdUser.OpenTime = Convert.ToDateTime(dbreader["OpenTime"]);
                    }
                    else
                    {
                        loginfo.TdUser.OpenTime = null;
                    }
                    if (System.DBNull.Value != dbreader["LastLoginTime"])
                    {
                        loginfo.TdUser.LastLoginTime = Convert.ToDateTime(dbreader["LastLoginTime"]);
                    }
                    else
                    {
                        loginfo.TdUser.LastLoginTime = null;
                    }
                    loginfo.TdUser.Sex = System.DBNull.Value != dbreader["Sex"] ? dbreader["Sex"].ToString() : string.Empty;
                    loginfo.TdUser.CardType = System.DBNull.Value != dbreader["CardType"] ? dbreader["CardType"].ToString() : string.Empty;
                    loginfo.TdUser.OrgId = System.DBNull.Value != dbreader["OrgId"] ? dbreader["OrgId"].ToString() : string.Empty;

                    IsSuc = true;
                }
                //过滤MAC地址
                if (IsSuc)
                {
                    dbreader.Close();
                    dbreader = DbHelper.ExecuteReader(string.Format("select mac from Trade_mac where mac='{0}'", mac));
                    if (dbreader.Read())
                    {
                        IsSuc = false;
                    }
                }
                //过滤IP地址
                if (IsSuc)
                {
                    dbreader.Close();
                    dbreader = DbHelper.ExecuteReader("select startip,endip from Trade_ip");
                    UInt32 istartip = 0;
                    UInt32 iendip = 0;
                    UInt32 icleintip = GetUintIP(GetClientIp());
                    while (dbreader.Read())
                    {
                        istartip = GetUintIP(dbreader["startip"].ToString());
                        iendip = GetUintIP(dbreader["endip"].ToString());
                        if (icleintip >= istartip && icleintip <= iendip)
                        {
                            IsSuc = false;
                            break;
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
            return IsSuc;

        }

        /// <summary>
        /// 根据IP字符串获取IP值
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns>IP值</returns>
        public static UInt32 GetUintIP(string ip)
        {
            UInt32 uintip = 0;
            try
            {
                byte[] xs = System.Net.IPAddress.Parse(ip).GetAddressBytes();
                uintip = (UInt32)(xs[0] << 24);
                uintip += (UInt32)(xs[1] << 16);
                uintip += (UInt32)(xs[2] << 8);
                uintip += (UInt32)(xs[3]);
            }
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message, ex);
            //}
            catch //不需要抛出错误 如果出现错误 说明后台设置的过滤IP地址格式不正确 那么就不能限制用户登陆 所以此时不能抛出错误
            {
            }
            return uintip;
        }

        /// <summary>
        /// 查询登陆成功后的返回信息
        /// </summary>
        /// <param name="loginfo">登陆返回信息</param>
        /// <param name="tradeAccount">用户账号</param>
        /// <param name="tradePwd">用户密码</param>
        public static void ListLoginfo(ref Loginfo loginfo, string tradeAccount, string tradePwd)
        {
            try
            {
                //tradePwd = EncryptHelper.DesEncrypt(tradePwd);

                string sql = "update Base_User set LoginID=@LoginID where Account=@tradeAccount and LoginPwd=@tradePwd and Status='1' and usertype=3";

                DbHelper.ExecuteSql(sql,
                     new System.Data.Common.DbParameter[]{
                       DbHelper.CreateDbParameter(DataBaseType.SqlServer,"@LoginID",DbParameterType.String,loginfo.LoginID,ParameterDirection.Input),
                       DbHelper.CreateDbParameter(DataBaseType.SqlServer,"@tradeAccount",DbParameterType.String,tradeAccount,ParameterDirection.Input),
                       DbHelper.CreateDbParameter(DataBaseType.SqlServer,"@tradePwd",DbParameterType.String,tradePwd,ParameterDirection.Input),
                   });

                loginfo.QuotesAddressIP = ip;
                loginfo.QuotesPort = Convert.ToInt32(port);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        /// <summary>
        /// 根据用户账号获取用户ID
        /// </summary>
        /// <param name="TradeAccount">用户账号</param>
        /// <returns>用户ID</returns>
        public static string GetUserId(string TradeAccount)
        {
            System.Data.Common.DbDataReader dbreader = null;
            string userid = string.Empty;
            try
            {
                string sql = "select userId from Base_User where Account=@Account";
                dbreader = DbHelper.ExecuteReader(sql,
                    new System.Data.Common.DbParameter[] { DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@Account", DbParameterType.String, TradeAccount, ParameterDirection.Input) });
                if (dbreader.Read())
                {
                    userid = System.DBNull.Value != dbreader["userId"] ? dbreader["userId"].ToString() : string.Empty;
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
                }
            }
            return userid;
        }

        /// <summary>
        /// 根据用户账号获取用户类型
        /// </summary>
        /// <param name="TradeAccount">用户账号</param>
        /// <returns>用户ID</returns>
        public static UserType GetUserType(string TradeAccount)
        {
            System.Data.Common.DbDataReader dbreader = null;
            UserType Utype = UserType.NormalType;
            try
            {
                string sql = "select UserType from Base_User where Account=@Account";
                dbreader = DbHelper.ExecuteReader(sql,
                    new System.Data.Common.DbParameter[] { DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@Account", DbParameterType.String, TradeAccount, ParameterDirection.Input) });
                if (dbreader.Read())
                {
                    Utype = (UserType)dbreader["UserType"];
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
            return Utype;
        }

        /// <summary>
        /// 根据用户ID获取用户账号
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <returns>用户账号</returns>
        public static string GetTradeAccount(string UserId)
        {

            System.Data.Common.DbDataReader dbreader = null;
            string TradeAccount = string.Empty;
            try
            {
                string sql = "select Account from Base_User where UserId=@UserId and status='1'";
                dbreader = DbHelper.ExecuteReader(sql,
                    new System.Data.Common.DbParameter[] { DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@UserId", DbParameterType.String, UserId, ParameterDirection.Input) });
                if (dbreader.Read())
                {
                    TradeAccount = System.DBNull.Value != dbreader["Account"] ? dbreader["Account"].ToString() : string.Empty;
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
            return TradeAccount;
        }

        /// <summary>
        /// 根据用户账号获取用户ID
        /// </summary>
        /// <param name="TradeAccount">用户账号</param>
        /// <param name="TdUser">用户信息</param>
        /// <returns>用户ID</returns>
        public static string GetUserId(string TradeAccount, ref TradeUser TdUser)
        {

            System.Data.Common.DbDataReader dbreader = null;
            string userid = string.Empty;
            try
            {
                string sql = "select OrgId, UserType, Account, MinTrade,OrderUnit,MaxTrade,PermitRcash,PermitCcash,PermitDhuo,PermitHshou,PermitRstore,PermitDelOrder,userId,CashPwd,LoginPwd from Base_User where  Account=@Account";

                dbreader = DbHelper.ExecuteReader(sql,
                    new System.Data.Common.DbParameter[] { DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@Account", DbParameterType.String, TradeAccount, ParameterDirection.Input) });
                if (dbreader.Read())
                {
                    TdUser.OrgId = System.DBNull.Value != dbreader["OrgId"] ? dbreader["OrgId"].ToString() : string.Empty;
                    TdUser.UType = System.DBNull.Value != dbreader["UserType"] ? (UserType)(dbreader["UserType"]) : UserType.AdminType;
                    TdUser.Account = System.DBNull.Value != dbreader["Account"] ? dbreader["Account"].ToString() : string.Empty;
                    TdUser.MinTrade = System.DBNull.Value != dbreader["MinTrade"] ? Convert.ToDouble(dbreader["MinTrade"]) : 1;
                    TdUser.OrderUnit = System.DBNull.Value != dbreader["OrderUnit"] ? Convert.ToDouble(dbreader["OrderUnit"]) : 1;
                    TdUser.MaxTrade = System.DBNull.Value != dbreader["MaxTrade"] ? Convert.ToDouble(dbreader["MaxTrade"]) : 50;
                    TdUser.PermitRcash = System.DBNull.Value != dbreader["PermitRcash"] ? Convert.ToBoolean(dbreader["PermitRcash"]) : false;
                    TdUser.PermitCcash = System.DBNull.Value != dbreader["PermitCcash"] ? Convert.ToBoolean(dbreader["PermitCcash"]) : false;
                    TdUser.PermitDhuo = System.DBNull.Value != dbreader["PermitDhuo"] ? Convert.ToBoolean(dbreader["PermitDhuo"]) : false;
                    TdUser.PermitHshou = System.DBNull.Value != dbreader["PermitHshou"] ? Convert.ToBoolean(dbreader["PermitHshou"]) : false;
                    TdUser.PermitRstore = System.DBNull.Value != dbreader["PermitRstore"] ? Convert.ToBoolean(dbreader["PermitRstore"]) : false;
                    TdUser.PermitDelOrder = System.DBNull.Value != dbreader["PermitDelOrder"] ? Convert.ToBoolean(dbreader["PermitDelOrder"]) : false;
                    TdUser.LoginPwd = System.DBNull.Value != dbreader["LoginPwd"] ? dbreader["LoginPwd"].ToString() : string.Empty;
                    TdUser.CashPwd = System.DBNull.Value != dbreader["CashPwd"] ? dbreader["CashPwd"].ToString() : string.Empty;

                    userid = System.DBNull.Value != dbreader["userId"] ? dbreader["userId"].ToString() : string.Empty;
                    TdUser.UserID = userid;
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
            return userid;
        }

        /// <summary>
        /// 根据LoginID和userId判断登陆用户是否存在
        /// </summary>
        /// <param name="LoginID">登陆标识</param>
        /// <param name="userId">用户ID</param>
        /// <returns>返回结果为true or false</returns>
        public static bool ExistUserLoginID(string LoginID, ref string userId)
        {
            bool exist = false;
            System.Data.Common.DbDataReader dbreader = null;
            try
            {
                string strSql = "select userId from Base_User where LoginID=@LoginID";
                dbreader = DbHelper.ExecuteReader(strSql,
                            new System.Data.Common.DbParameter[] { DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@LoginID", DbParameterType.String, LoginID, ParameterDirection.Input) });
                if (dbreader.Read())
                {
                    userId = System.DBNull.Value != dbreader["userId"] ? dbreader["userId"].ToString() : string.Empty;
                    exist = true;
                }
                else
                {
                    exist = false;
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
            return exist;
        }

        /// <summary>
        /// 根据LoginID和userId判断登陆用户是否存在
        /// </summary>
        /// <param name="LoginID">登陆标识</param>
        /// <returns>返回结果为true or false</returns>
        public static bool ExistUserLoginID(string LoginID)
        {
            bool exist = false;
            System.Data.Common.DbDataReader dbreader = null;
            try
            {
                string strSql = "select userId from Base_User where LoginID<>'' and LoginID=@LoginID";
                dbreader = DbHelper.ExecuteReader(strSql,
                    new System.Data.Common.DbParameter[] { DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@LoginID", DbParameterType.String, LoginID, ParameterDirection.Input) });

                if (dbreader.Read())
                {
                    exist = true;
                }
                else
                {
                    exist = false;
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
            return exist;
        }


        /// <summary>
        /// 判断普通账号是否存在
        /// </summary>
        /// <param name="Account">登陆标识</param>
        /// <param name="OrgId">所属组织ID</param>
        /// <returns>返回结果为true or false</returns>
        public static bool ExistNormalAccount(string Account, string OrgId)
        {
            bool exist = false;
            System.Data.Common.DbDataReader dbreader = null;
            try
            {
                string strSql = string.Format("select Account from Base_User where Account=@Account and OrgId='{0}' and usertype=3", OrgId);
                dbreader = DbHelper.ExecuteReader(strSql,
                    new System.Data.Common.DbParameter[] { DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@Account", DbParameterType.String, Account, ParameterDirection.Input) });

                if (dbreader.Read())
                {
                    exist = true;
                }
                else
                {
                    exist = false;
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
            return exist;
        }

        /// <summary>
        /// 根据登陆ID判断用户登陆登陆用户是否存在
        /// </summary>
        /// <param name="LoginID">登陆标识</param>
        /// <param name="TdUser">用户信息</param>
        /// <returns>返回结果为true or false</returns>
        public static bool ExistUserLoginID(string LoginID, ref TradeUser TdUser)
        {
            bool exist = false;
            System.Data.Common.DbDataReader dbreader = null;
            try
            {
                string strSql = "select ip,mac,OrgId,UserType, Account, MinTrade,OrderUnit,MaxTrade,PermitRcash,PermitCcash,PermitDhuo,PermitHshou,PermitRstore,PermitDelOrder,userId,CashPwd,LoginPwd from Base_User where LoginID=@LoginID";

                dbreader = DbHelper.ExecuteReader(strSql,
                    new System.Data.Common.DbParameter[] { DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@LoginID", DbParameterType.String, LoginID, ParameterDirection.Input) });
                if (dbreader.Read())
                {
                    TdUser.OrgId = System.DBNull.Value != dbreader["OrgId"] ? dbreader["OrgId"].ToString() : string.Empty;
                    TdUser.UType = System.DBNull.Value != dbreader["UserType"] ? (UserType)(dbreader["UserType"]) : UserType.AdminType;
                    TdUser.Account = System.DBNull.Value != dbreader["Account"] ? dbreader["Account"].ToString() : string.Empty;
                    TdUser.MinTrade = System.DBNull.Value != dbreader["MinTrade"] ? Convert.ToDouble(dbreader["MinTrade"]) : 1;
                    TdUser.OrderUnit = System.DBNull.Value != dbreader["OrderUnit"] ? Convert.ToDouble(dbreader["OrderUnit"]) : 0.5;//默认为0.5
                    TdUser.MaxTrade = System.DBNull.Value != dbreader["MaxTrade"] ? Convert.ToDouble(dbreader["MaxTrade"]) : 50;
                    TdUser.PermitRcash = System.DBNull.Value != dbreader["PermitRcash"] ? Convert.ToBoolean(dbreader["PermitRcash"]) : false;
                    TdUser.PermitCcash = System.DBNull.Value != dbreader["PermitCcash"] ? Convert.ToBoolean(dbreader["PermitCcash"]) : false;
                    TdUser.PermitDhuo = System.DBNull.Value != dbreader["PermitDhuo"] ? Convert.ToBoolean(dbreader["PermitDhuo"]) : false;
                    TdUser.PermitHshou = System.DBNull.Value != dbreader["PermitHshou"] ? Convert.ToBoolean(dbreader["PermitHshou"]) : false;
                    TdUser.PermitRstore = System.DBNull.Value != dbreader["PermitRstore"] ? Convert.ToBoolean(dbreader["PermitRstore"]) : false;
                    TdUser.PermitDelOrder = System.DBNull.Value != dbreader["PermitDelOrder"] ? Convert.ToBoolean(dbreader["PermitDelOrder"]) : false;
                    TdUser.LoginPwd = System.DBNull.Value != dbreader["LoginPwd"] ? dbreader["LoginPwd"].ToString() : string.Empty;
                    TdUser.CashPwd = System.DBNull.Value != dbreader["CashPwd"] ? dbreader["CashPwd"].ToString() : string.Empty;
                    TdUser.UserID = System.DBNull.Value != dbreader["userId"] ? dbreader["userId"].ToString() : string.Empty;
                    TdUser.Ip = System.DBNull.Value != dbreader["ip"] ? dbreader["ip"].ToString() : string.Empty;
                    TdUser.Mac = System.DBNull.Value != dbreader["mac"] ? dbreader["mac"].ToString() : string.Empty;
                    exist = true;
                }
                else
                {
                    exist = false;
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
            return exist;
        }

        /// <summary>
        /// 判断管理员是否存在
        /// </summary>
        /// <param name="adminid">管理员帐号</param>
        /// <param name="Password">密码</param>
        /// <param name="UserId">用户ID</param>
        /// <returns>true or false</returns>
        public static bool ExistAdmin(string adminid, string Password, ref string UserId)
        {
            bool exist = false;

            System.Data.Common.DbDataReader dbreader = null;
            try
            {
                string strSql = "select userid from base_user where account=@account and LoginPwd=@LoginPwd and status='1' and usertype<>3";
                dbreader = DbHelper.ExecuteReader(strSql,
                    new System.Data.Common.DbParameter[] { 
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@account", DbParameterType.String, adminid, ParameterDirection.Input),
                    DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@LoginPwd", DbParameterType.String, Password, ParameterDirection.Input) });
                if (dbreader.Read())
                {
                    UserId = System.DBNull.Value != dbreader["userid"] ? dbreader["userid"].ToString() : string.Empty;
                    exist = true;
                }
                else
                {
                    exist = false;
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
            return exist;
        }

        /// <summary>
        /// 判断组织是否存在
        /// </summary>
        /// <param name="orgname">组织编码</param>
        /// <param name="OrgId">组织ID</param>
        /// <returns>true or false</returns>
        public static bool ExistOrgName(string orgname, ref string OrgId)
        {
            bool exist = false;

            System.Data.Common.DbDataReader dbreader = null;
            try
            {

                string strSql = "select OrgId from Base_Org where orgname=@orgname";
                dbreader = DbHelper.ExecuteReader(strSql,
                    new System.Data.Common.DbParameter[] { DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@orgname", DbParameterType.String, orgname, ParameterDirection.Input) });
                if (dbreader.Read())
                {
                    OrgId = System.DBNull.Value != dbreader["OrgId"] ? dbreader["OrgId"].ToString() : string.Empty;
                    exist = true;
                }
                else
                {
                    exist = false;
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
            return exist;
        }

        /// <summary>
        /// 根据商品编码 获取商品信息
        /// </summary>
        /// <param name="productCode">商品编码</param>
        /// <returns>商品信息</returns>
        public static ProductConfig GetProductInfo(string productCode)
        {
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            ProductConfig ptc = new ProductConfig();
            try
            {
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = "select productcode, ProductName,goodscode,adjustbase,adjustcount,pricedot,valuedot,setBase,holdbase,Ordemoney, ordemoneyfee,"
                + " maxprice,minprice,maxtime,state,unit,pricecode,expressionfee,buystoragefee,sellstoragefee,sellfee,lowerprice,starttime,endtime from Trade_Product where productcode=@productcode";
                SqlParameter p_pcode = new SqlParameter();
                p_pcode.ParameterName = "@productcode";
                p_pcode.DbType = DbType.String;
                p_pcode.Value = productCode;
                sqlcmd.Parameters.Add(p_pcode);

                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    ptc.ProductCode = sqldr["ProductCode"].ToString();
                    ptc.ProductName = sqldr["ProductName"].ToString();
                    ptc.GoodsCode = sqldr["GoodsCode"].ToString();
                    ptc.AdjustBase = Convert.ToDouble(sqldr["AdjustBase"]);
                    ptc.AdjustCount = Convert.ToInt32(sqldr["AdjustCount"]);
                    ptc.PriceDot = Convert.ToInt32(sqldr["PriceDot"]);
                    ptc.ValueDot = Convert.ToDouble(sqldr["ValueDot"]);
                    ptc.SetBase = Convert.ToInt32(sqldr["SetBase"]);
                    ptc.HoldBase = Convert.ToInt32(sqldr["HoldBase"]);
                    ptc.OrderMoney = Convert.ToDouble(sqldr["Ordemoney"]);
                    ptc.OrderMoneyFee = sqldr["ordemoneyfee"].ToString();
                    ptc.MaxPrice = Convert.ToDouble(sqldr["MaxPrice"]);
                    ptc.MinPrice = Convert.ToDouble(sqldr["MinPrice"]);
                    ptc.MaxTime = Convert.ToDouble(sqldr["MaxTime"]);
                    ptc.State = sqldr["State"].ToString();
                    ptc.Unit = Convert.ToDouble(sqldr["Unit"]);
                    ptc.PriceCode = sqldr["PriceCode"].ToString();
                    ptc.ExpressionFee = sqldr["ExpressionFee"].ToString();
                    ptc.BuyStorageFee = sqldr["BuyStorageFee"].ToString();
                    ptc.SellStorageFee = sqldr["SellStorageFee"].ToString();
                    ptc.SellFee = sqldr["SellFee"].ToString();
                    ptc.Starttime = sqldr["starttime"].ToString();
                    ptc.Endtime = sqldr["endtime"].ToString();
                    ptc.LowerPrice = Convert.ToDouble(sqldr["lowerprice"]);
                    break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != sqldr)
                {
                    sqldr.Close();
                }
            }
            return ptc;
        }

        /// <summary>
        /// 获取买价的点差额
        /// </summary>
        /// <returns>返回点差额</returns>
        public static ProductRealPrice GetProductRealPrice()
        {
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            ProductRealPrice prc = new ProductRealPrice();
            try
            {
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = "select distinct goodscode,pricecode,lowerprice from Trade_Product";

                //SqlParameter p_pcode = new SqlParameter();
                //p_pcode.ParameterName = "@pricecode";
                //p_pcode.DbType = DbType.String;
                //p_pcode.Value = priceCode;
                //sqlcmd.Parameters.Add(p_pcode);
                string goodscode = string.Empty;
                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    goodscode = sqldr["goodscode"].ToString();
                    switch (goodscode)
                    {
                        case ComFunction.AUFlg:
                            if (prc.aub_price < ComFunction.dzero)
                            {
                                prc.aub_price = Convert.ToInt32(sqldr["lowerprice"]);
                            }
                            break;
                        case ComFunction.AGFlg:
                            if (prc.agb_price < ComFunction.dzero)
                            {
                                prc.agb_price = Convert.ToInt32(sqldr["lowerprice"]);
                            }
                            break;
                        case ComFunction.PTFlg:
                            if (prc.ptb_price < ComFunction.dzero)
                            {
                                prc.ptb_price = Convert.ToInt32(sqldr["lowerprice"]);
                            }
                            break;
                        case ComFunction.PDFlg:
                            if (prc.pdb_price < ComFunction.dzero)
                            {
                                prc.pdb_price = Convert.ToInt32(sqldr["lowerprice"]);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != sqldr)
                {
                    sqldr.Close();
                }
            }
            return prc;

        }

        /// <summary>
        ///  根据原料编码 获取商品信息
        /// </summary>
        /// <param name="goodsCode">原料编码</param>
        /// <param name="lowerprice">输出：下浮价格</param>
        /// <returns>商品信息</returns>
        public static ProductConfig GetProductInfoWithGoodscode(string goodsCode, ref double lowerprice)
        {
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            ProductConfig ptc = new ProductConfig();
            try
            {
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = "select top 1 productcode, ProductName,goodscode,adjustbase,adjustcount,pricedot,valuedot,setBase,holdbase,Ordemoney, "
                + " maxprice,minprice,maxtime,state,unit,pricecode,expressionfee,buystoragefee,sellstoragefee,sellfee,lowerprice from Trade_Product where goodscode=@goodscode";
                SqlParameter p_pcode = new SqlParameter();
                p_pcode.ParameterName = "@goodscode";
                p_pcode.DbType = DbType.String;
                p_pcode.Value = goodsCode;
                sqlcmd.Parameters.Add(p_pcode);

                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    ptc.ProductCode = sqldr["ProductCode"].ToString();
                    ptc.ProductName = sqldr["ProductName"].ToString();
                    ptc.GoodsCode = sqldr["goodscode"].ToString();
                    ptc.AdjustBase = Convert.ToDouble(sqldr["adjustbase"]);
                    ptc.AdjustCount = Convert.ToInt32(sqldr["adjustcount"]);
                    ptc.PriceDot = Convert.ToInt32(sqldr["pricedot"]);
                    ptc.ValueDot = Convert.ToDouble(sqldr["valuedot"]);
                    ptc.SetBase = Convert.ToInt32(sqldr["setBase"]);
                    ptc.HoldBase = Convert.ToInt32(sqldr["holdbase"]);
                    ptc.OrderMoney = Convert.ToDouble(sqldr["Ordemoney"]);
                    ptc.MaxPrice = Convert.ToDouble(sqldr["MaxPrice"]);
                    ptc.MinPrice = Convert.ToDouble(sqldr["MinPrice"]);
                    ptc.MaxTime = Convert.ToDouble(sqldr["MaxTime"]);
                    ptc.State = sqldr["State"].ToString();
                    ptc.Unit = Convert.ToDouble(sqldr["Unit"]);
                    ptc.PriceCode = sqldr["PriceCode"].ToString();
                    ptc.ExpressionFee = sqldr["ExpressionFee"].ToString();
                    ptc.BuyStorageFee = sqldr["BuyStorageFee"].ToString();
                    ptc.SellStorageFee = sqldr["SellStorageFee"].ToString();
                    ptc.SellFee = sqldr["SellFee"].ToString();
                    lowerprice = Convert.ToDouble(sqldr["lowerprice"]);
                    break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != sqldr)
                {
                    sqldr.Close();
                }
            }
            return ptc;
        }

        /// <summary>
        /// 根据用户ID获取库存信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>库存信息</returns>
        public static MoneyInventory GetMoneyInventoryByUserId(string userId)
        {
            MoneyInventory moneyInventory = new MoneyInventory();
            moneyInventory.StorageQuantity = new Storagequantity();
            moneyInventory.FdInfo = new Fundinfo();
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;

            try
            {
                moneyInventory.Result = false;
                bool IsGetMoney = false;
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = "select DongJieMoney,money,frozenMoney,OccMoney,state,CashUser,SubUser,TanUser,ConBankType,OpenBank,BankAccount,AccountName,BankCard from Trade_FundInfo where state<>'4' and userId=@userId";
                SqlParameter lh = new SqlParameter();
                lh.ParameterName = "@userId";
                lh.DbType = DbType.String;
                lh.Value = userId;
                sqlcmd.Parameters.Add(lh);
                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    moneyInventory.FdInfo.DongJieMoney = System.DBNull.Value != sqldr["DongJieMoney"] ? Convert.ToDouble(sqldr["DongJieMoney"]) : 0;
                    moneyInventory.FdInfo.Money = System.DBNull.Value != sqldr["money"] ? Convert.ToDouble(sqldr["money"]) : 0;
                    moneyInventory.FdInfo.FrozenMoney = System.DBNull.Value != sqldr["frozenMoney"] ? Convert.ToDouble(sqldr["frozenMoney"]) : 0;
                    moneyInventory.FdInfo.OccMoney = System.DBNull.Value != sqldr["OccMoney"] ? Convert.ToDouble(sqldr["OccMoney"]) : 0;
                    moneyInventory.FdInfo.State = System.DBNull.Value != sqldr["state"] ? sqldr["state"].ToString() : string.Empty;
                    moneyInventory.FdInfo.CashUser = System.DBNull.Value != sqldr["CashUser"] ? sqldr["CashUser"].ToString() : string.Empty;
                    moneyInventory.FdInfo.SubUser = System.DBNull.Value != sqldr["SubUser"] ? sqldr["SubUser"].ToString() : string.Empty;
                    moneyInventory.FdInfo.TanUser = System.DBNull.Value != sqldr["TanUser"] ? sqldr["TanUser"].ToString() : string.Empty;
                    moneyInventory.FdInfo.ConBankType = System.DBNull.Value != sqldr["ConBankType"] ? sqldr["ConBankType"].ToString() : string.Empty;
                    moneyInventory.FdInfo.OpenBank = System.DBNull.Value != sqldr["OpenBank"] ? sqldr["OpenBank"].ToString() : string.Empty;
                    moneyInventory.FdInfo.BankAccount = System.DBNull.Value != sqldr["BankAccount"] ? sqldr["BankAccount"].ToString() : string.Empty;
                    moneyInventory.FdInfo.AccountName = System.DBNull.Value != sqldr["AccountName"] ? sqldr["AccountName"].ToString() : string.Empty;
                    moneyInventory.FdInfo.BankCard = System.DBNull.Value != sqldr["BankCard"] ? sqldr["BankCard"].ToString() : string.Empty;
                    IsGetMoney = true;
                    break;
                }
                ////没有获取资金
                //if (!IsGetMoney)
                //{
                //    return moneyInventory;
                //}
                ////查询 库存 
                //sqldr.Close();
                //sqlcmd.CommandText = "select au,ag,pt,pd from Stock_BZJ where userId=@userId ";
                //sqldr = sqlcmd.ExecuteReader();
                //if (sqldr.Read())
                //{
                //    moneyInventory.StorageQuantity.xau = System.DBNull.Value != sqldr["au"] ? Convert.ToDouble(sqldr["au"]) : 0;

                //    moneyInventory.StorageQuantity.xag = System.DBNull.Value != sqldr["ag"] ? Convert.ToDouble(sqldr["ag"]) : 0;

                //    moneyInventory.StorageQuantity.xpt = System.DBNull.Value != sqldr["pt"] ? Convert.ToDouble(sqldr["pt"]) : 0;

                //    moneyInventory.StorageQuantity.xpd = System.DBNull.Value != sqldr["pd"] ? Convert.ToDouble(sqldr["pd"]) : 0;
                //}

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != sqldr)
                {
                    sqldr.Close();
                }
            }
            moneyInventory.Result = true;
            return moneyInventory;
        }

        /// <summary>
        /// 获取卖库中现有的库存
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="goodscode">原料编码</param>
        /// <returns>现有的库存</returns>
        public static double GetSaleBeforeStore(string userId, string goodscode)
        {
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            double _beforeStore = 0;
            try
            {
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = string.Format("select storagequantity from Trade_Sale where userId='{0}' and goodscode='{1}' ", userId, goodscode);
                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    _beforeStore = Convert.ToDouble(sqldr["storagequantity"]);
                    break;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != sqldr)
                {
                    sqldr.Close();
                }
            }
            return _beforeStore;
        }

        /// <summary>
        /// 获取服务器本地时间 判断当前时间是否处于交易时段的时间之内
        /// </summary>
        /// <param name="pricecode">行情编码</param>
        /// <param name="dt">服务器本地时间</param>
        /// <returns>返回结果为true or false</returns>
        public static bool GetDateset(string pricecode, DateTime dt)
        {
            int weekday = (int)dt.DayOfWeek;
            bool IsTradeTime = false;
            string starttime = string.Empty;//数据库的数据格式为 HH:mm:ss
            string endtime = string.Empty;//数据库的数据格式为 HH:mm:ss
            try
            {
                //先判断交易日
                //根据周几 获取交易时段
                GetTradeTime(pricecode, weekday, ref starttime, ref endtime);
                //判断dt是否在开始时间和结束时间之间
                if (string.Empty != starttime && string.Empty != endtime)
                {
                    DateTime st = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}", dt.Year, dt.Month, dt.Day, starttime));
                    DateTime et = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}", dt.Year, dt.Month, dt.Day, endtime));

                    IsTradeTime = GetIsTradeTime(dt, st, et);
                }
                //再判断当前时间是否是不允许交易的节假日的时段
                if (IsTradeTime)
                {
                    //获取节假日
                    List<Holiday> list = GetHoliday(pricecode);
                    foreach (Holiday holiday in list)
                    {
                        if (holiday.PriceCode.Contains(pricecode) && dt >= holiday.StartTime && dt <= holiday.EndTime)
                        {
                            IsTradeTime = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return IsTradeTime;
        }

        /// <summary>
        /// 根据商品的交易时间,判断当前时间是否允许该商品交易
        /// </summary>
        /// <param name="starttime">开始时间 数据库的数据格式为 HH:mm:ss</param>
        ///<param name="endtime">结束时间 数据库的数据格式为 HH:mm:ss</param>
        /// <returns>允许交易返回true 不允许返回false </returns>
        public static bool ProductCanTrade(string starttime, string endtime)
        {
            DateTime dt = DateTime.Now;
            bool IsTradeTime = false;
            try
            {
                //判断dt是否在开始时间和结束时间之间
                if (string.Empty != starttime && string.Empty != endtime)
                {
                    DateTime st = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}", dt.Year, dt.Month, dt.Day, starttime));
                    DateTime et = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}", dt.Year, dt.Month, dt.Day, endtime));
                    IsTradeTime = GetIsTradeTime(dt, st, et);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return IsTradeTime;
        }

        private static bool GetIsTradeTime(DateTime dt,DateTime st,DateTime et)
        {
            bool IsTradeTime = false;
            //非正常时间段(开始时间比结束时间大)
            if (st > et)
            {
                if (dt <= et) //比结束时间小 允许交易
                {
                    IsTradeTime = true;
                }
                else
                {
                    if (dt >= st) //比结束时间大 且>=开始时间 允许交易
                    {
                        IsTradeTime = true;
                    }
                }
            }
            else//正常时间段
            {

                if (dt >= st && dt <= et)
                {
                    IsTradeTime = true;
                }
            }
            return IsTradeTime;
        }

        /// <summary>
        /// 获取允许交易的交易日的时段
        /// </summary>
        /// <param name="pricecode">行情编码</param>
        /// <param name="weekday">周期几</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        public static void GetTradeTime(string pricecode, int weekday, ref string starttime, ref string endtime)
        {
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            try
            {
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText =string.Format("select starttime,endtime from Trade_DateSetEx where weekday=@weekday and istrade=0 and pricecode='{0}'",pricecode);

                SqlParameter p_wekday = new SqlParameter();
                p_wekday.ParameterName = "@weekday";
                p_wekday.DbType = DbType.String;
                p_wekday.Value = weekday;
                sqlcmd.Parameters.Add(p_wekday);

                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    starttime = System.DBNull.Value != sqldr["starttime"] ? sqldr["starttime"].ToString() : string.Empty;
                    endtime = System.DBNull.Value != sqldr["endtime"] ? sqldr["endtime"].ToString() : string.Empty;
                    break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != sqldr)
                {
                    sqldr.Close();
                }
            }
        }

        /// <summary>
        /// 获取不允许交易的节假日的时段
        /// </summary>
        /// <param name="pricecode">行情编码</param>
        /// <returns>不允许交易的节假日的时段</returns>
        public static List<Holiday> GetHoliday(string pricecode)
        {
            List<Holiday> list = new List<Holiday>();
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            Holiday holiday;
            try
            {
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = string.Format("select pricecode,starttime,endtime from Trade_HolidayEx where pricecode like'%{0}%'",pricecode);

                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    holiday.StartTime = System.DBNull.Value != sqldr["starttime"] ? Convert.ToDateTime(sqldr["starttime"]) : DateTime.MinValue;
                    holiday.EndTime = System.DBNull.Value != sqldr["endtime"] ? Convert.ToDateTime(sqldr["endtime"]) : DateTime.MinValue;
                    holiday.PriceCode = System.DBNull.Value != sqldr["pricecode"] ? Convert.ToString(sqldr["pricecode"]) : string.Empty;
                    list.Add(holiday);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != sqldr)
                {
                    sqldr.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// 根据挂单ID查询挂单记录
        /// </summary>
        /// <param name="holdid">挂单ID</param>
        /// <param name="userid">要返回的用户ID</param>
        /// <param name="ip">要返回的IP</param>
        /// <param name="mac">要返回的MAC</param>
        /// <returns>挂单记录</returns>
        public static TradeHoldOrder GetTradeHoldOrder(string holdid, ref string userid, ref string ip, ref string mac)
        {
            TradeHoldOrder tradeHoldOrder = new TradeHoldOrder();
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            try
            {
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = "select userId,HoldOrderID,productcode,quantity,frozenMoney,OrderType,HoldPrice,profitPrice,lossPrice,validtime,ordertime, " +
                                    " ip,mac from Trade_HoldOrder where HoldOrderID=@HoldOrderID ";
                SqlParameter p_holdid = new SqlParameter();
                p_holdid.ParameterName = "@HoldOrderID";
                p_holdid.DbType = DbType.String;
                p_holdid.Value = holdid;
                sqlcmd.Parameters.Add(p_holdid);

                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    userid = System.DBNull.Value != sqldr["userId"] ? sqldr["userId"].ToString() : string.Empty;
                    tradeHoldOrder.HoldOrderID = System.DBNull.Value != sqldr["HoldOrderID"] ? sqldr["HoldOrderID"].ToString() : string.Empty;
                    tradeHoldOrder.ProductCode = System.DBNull.Value != sqldr["productcode"] ? sqldr["productcode"].ToString() : string.Empty;
                    tradeHoldOrder.Quantity = System.DBNull.Value != sqldr["quantity"] ? Convert.ToDouble(sqldr["quantity"]) : 0;
                    tradeHoldOrder.FrozenMoney = System.DBNull.Value != sqldr["frozenMoney"] ? Convert.ToDouble(sqldr["frozenMoney"]) : 0;

                    tradeHoldOrder.OrderType = System.DBNull.Value != sqldr["OrderType"] ? sqldr["OrderType"].ToString() : string.Empty;
                    tradeHoldOrder.HoldPrice = System.DBNull.Value != sqldr["HoldPrice"] ? Convert.ToDouble(sqldr["HoldPrice"]) : 0;
                    tradeHoldOrder.ProfitPrice = System.DBNull.Value != sqldr["profitPrice"] ? Convert.ToDouble(sqldr["profitPrice"]) : 0;
                    tradeHoldOrder.LossPrice = System.DBNull.Value != sqldr["lossPrice"] ? Convert.ToDouble(sqldr["lossPrice"]) : 0;
                    tradeHoldOrder.ValidTime = System.DBNull.Value != sqldr["validtime"] ? Convert.ToDateTime(sqldr["validtime"]) : DateTime.MinValue;
                    tradeHoldOrder.OrderTime = System.DBNull.Value != sqldr["ordertime"] ? Convert.ToDateTime(sqldr["ordertime"]) : DateTime.MinValue;
                    ip = System.DBNull.Value != sqldr["ip"] ? sqldr["ip"].ToString() : string.Empty;
                    mac = System.DBNull.Value != sqldr["mac"] ? sqldr["mac"].ToString() : string.Empty;
                    break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != sqldr)
                {
                    sqldr.Close();
                }
            }
            return tradeHoldOrder;
        }

        /// <summary>
        /// 根据订单ID查询订单记录
        /// </summary>
        /// <param name="orderid">订单ID</param>
        /// <param name="ip">要返回的IP</param>
        /// <param name="mac">要返回的MAC</param>
        /// <returns>订单记录</returns>
        public static TradeOrder GetTradeOrder(string orderid, ref string ip, ref string mac)
        {
            TradeOrder tradeOrder = new TradeOrder();
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            try
            {
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = "select userId,Orderid,productcode,OrderType,Orderprice,usequantity,quantity,lossPrice,profitPrice,OccMoney,tradefee,storagefee, " +
                                    " Ordertime,OperType,ip,mac,allowStore,IsExperience from Trade_Order where OrderID=@OrderID ";
                SqlParameter p_orderid = new SqlParameter();
                p_orderid.ParameterName = "@OrderID";
                p_orderid.DbType = DbType.String;
                p_orderid.Value = orderid;
                sqlcmd.Parameters.Add(p_orderid);

                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    tradeOrder.OrderId = sqldr["OrderId"].ToString();
                    tradeOrder.ProductCode = sqldr["ProductCode"].ToString();
                    tradeOrder.OrderType = sqldr["OrderType"].ToString();
                    tradeOrder.OrderPrice = Convert.ToDouble(sqldr["OrderPrice"]);

                    tradeOrder.UseQuantity = Convert.ToDouble(sqldr["UseQuantity"]);
                    tradeOrder.Quantity = Convert.ToDouble(sqldr["Quantity"]);
                    tradeOrder.LossPrice = Convert.ToDouble(sqldr["LossPrice"]);
                    tradeOrder.ProfitPrice = Convert.ToDouble(sqldr["ProfitPrice"]);
                    tradeOrder.OccMoney = Convert.ToDouble(sqldr["OccMoney"]);
                    tradeOrder.TradeFee = Convert.ToDouble(sqldr["TradeFee"]);
                    tradeOrder.StorageFee = Convert.ToDouble(sqldr["StorageFee"]);
                    tradeOrder.OrderTime = Convert.ToDateTime(sqldr["OrderTime"]);
                    tradeOrder.OperType = sqldr["OperType"].ToString();
                    ip = sqldr["ip"].ToString();
                    mac = sqldr["mac"].ToString();
                    tradeOrder.AllowStore = Convert.ToBoolean(sqldr["AllowStore"]);
                    tradeOrder.IsExperience = Convert.ToBoolean(sqldr["IsExperience"]);
                    break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != sqldr)
                {
                    sqldr.Close();
                }
            }
            return tradeOrder;
        }

        /// <summary>
        /// 根据行情编码 获取实时价格
        /// </summary>
        /// <param name="pcode">行情编码</param>
        /// <returns>实时价格</returns>
        public static double GetRealPrice(string pcode)
        {
            double realprice = 0;
            TradeClient tc = new TradeClient();
            try
            {
                //WSHttpBinding binding = new WSHttpBinding(SecurityMode.None);
                //EndpointAddress address = new EndpointAddress(ConfigurationManager.AppSettings["wcfaddr1"]);
                //ChannelFactory<WcfInterface.ServiceReference1.ITrade> ttgService = new ChannelFactory<WcfInterface.ServiceReference1.ITrade>(binding, address);
                //realprice = ttgService.CreateChannel().GetRealprice(pcode);
                realprice = tc.GetRealprice(pcode);
                tc.Close();
            }
            catch (Exception ex)
            {
                tc.Abort();
                throw new Exception(ex.Message, ex);
            }
            return realprice;
        }

        /// <summary>
        /// 根据不同的费用计算公式 获取费用
        /// </summary>
        /// <param name="expressionfee">费用计算公式</param>
        /// <param name="ordemoney">定金比</param>
        /// <param name="orderprice">下单价</param>
        /// <param name="quantity">数量</param>
        /// <returns>费用</returns>
        public static double Getfee(string expressionfee, double ordemoney, double orderprice, double quantity)
        {
            double _expressionfee = 0;
            try
            {
                if (!string.IsNullOrEmpty(expressionfee))
                {
                    //string strtmp = expressionfee.Replace("[数量]", quantity.ToString()).Replace("[定金比]", ordemoney.ToString()).Replace("[建仓价]", orderprice.ToString());
                    string strtmp = expressionfee.Replace("[数量]", quantity.ToString()).Replace("[建仓价]", orderprice.ToString());
                    _expressionfee = Convert.ToDouble(Microsoft.JScript.Eval.JScriptEvaluate(strtmp, Microsoft.JScript.Vsa.VsaEngine.CreateEngine()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return _expressionfee;
        }

        /// <summary>
        /// SQL事务处理
        /// </summary>
        /// <param name="sqlList">SQL语句列表</param>
        /// <returns>返回结果为true or false</returns>
        public static bool SqlTransaction(List<string> sqlList)
        {
            bool Issuc = false;
            try
            {
                DbHelper.ExecuteSqlTran(new System.Collections.ArrayList(sqlList));
                Issuc = true;
            }
            catch (Exception ex)
            {
                string sql = "";
                foreach (string str in sqlList)
                    sql += str + "\n";
                WriteErr(new Exception("Sql事务出错跟踪：见此提示转马友春"+ex.Message+sql));
                Issuc = false;
            }

            return Issuc;
        }

        /// <summary>
        /// 获取客户端的IP地址
        /// </summary>
        /// <returns>IP地址</returns>
        public static string GetClientIp()
        {
            try
            {
                OperationContext context = OperationContext.Current;
                System.ServiceModel.Channels.MessageProperties properties = context.IncomingMessageProperties;
                System.ServiceModel.Channels.RemoteEndpointMessageProperty endpoint = properties[System.ServiceModel.Channels.RemoteEndpointMessageProperty.Name] as System.ServiceModel.Channels.RemoteEndpointMessageProperty;
                //return endpoint.Address + ";" + endpoint.Port.ToString();
                return endpoint.Address;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据当前报价及回购重量计算 账户余额和库存变动记录
        /// </summary>
        /// <param name="moneygoods">账户余额和库存变动记录</param>
        public static void GetMoneyGoods(ref MoneyGoods moneygoods)
        {
            try
            {
                //回购重量>=总的库存
                if (moneygoods.Quantity - moneygoods.Xusd >= dzero)
                {
                    moneygoods.UserChangeMoney = Math.Round(moneygoods.Xusd * moneygoods.RealPrice - moneygoods.NXusd * moneygoods.LowerPrice, 2, MidpointRounding.AwayFromZero);
                    moneygoods.StorageQuantity = 0;
                    moneygoods.NoUseStorage = 0;
                    moneygoods.ChangeStorage = -moneygoods.Xusd;
                }
                //回购重量<=未办理业务的库存
                else if (moneygoods.Quantity - moneygoods.NXusd <= dzero)
                {
                    moneygoods.UserChangeMoney = Math.Round(moneygoods.Quantity * (moneygoods.RealPrice - moneygoods.LowerPrice), 2, MidpointRounding.AwayFromZero);
                    moneygoods.StorageQuantity = moneygoods.Xusd - moneygoods.Quantity;
                    moneygoods.NoUseStorage = moneygoods.NXusd - moneygoods.Quantity;
                    moneygoods.ChangeStorage = -moneygoods.Quantity;
                }
                //回购重量>未办理业务的库存 且 回购重量<总的库存
                else if (moneygoods.Quantity - moneygoods.NXusd > dzero && moneygoods.Quantity - moneygoods.Xusd < dzero)
                {
                    moneygoods.UserChangeMoney = Math.Round(moneygoods.Quantity * moneygoods.RealPrice - moneygoods.NXusd * moneygoods.LowerPrice, 2, MidpointRounding.AwayFromZero);
                    moneygoods.StorageQuantity = moneygoods.Xusd - moneygoods.Quantity;
                    moneygoods.NoUseStorage = 0;
                    moneygoods.ChangeStorage = -moneygoods.Quantity;
                }
                moneygoods.OldStorageQuantity = moneygoods.Xusd;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取交易用户信息列表
        /// </summary>
        /// <param name="sql">查询SQL</param>
        /// <returns>用户信息列表</returns>
        public static List<TradeUser> GetTdUserList(string sql)
        {
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            List<TradeUser> list = new List<TradeUser>();

            try
            {
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = sql;
                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    TradeUser TdUser = new TradeUser();
                    TdUser.UserName = System.DBNull.Value != sqldr["UserName"] ? sqldr["UserName"].ToString() : string.Empty;
                    TdUser.Status = System.DBNull.Value != sqldr["State"] ? sqldr["State"].ToString() : string.Empty;
                    TdUser.AccountType = System.DBNull.Value != sqldr["UserType"] ? sqldr["UserType"].ToString() : string.Empty;
                    TdUser.Account = System.DBNull.Value != sqldr["TradeAccount"] ? sqldr["TradeAccount"].ToString() : string.Empty;
                    TdUser.LoginPwd = System.DBNull.Value != sqldr["TradePwd"] ? Des3.Des3DecodeCBC(sqldr["TradePwd"].ToString()) : string.Empty;
                    TdUser.CashPwd = System.DBNull.Value != sqldr["CashPwd"] ? Des3.Des3DecodeCBC(sqldr["CashPwd"].ToString()) : string.Empty;
                    TdUser.CardType = System.DBNull.Value != sqldr["CardType"] ? sqldr["CardType"].ToString() : string.Empty;
                    TdUser.CardNum = System.DBNull.Value != sqldr["CardNum"] ? sqldr["CardNum"].ToString() : string.Empty;
                    TdUser.PhoneNum = System.DBNull.Value != sqldr["PhoneNum"] ? sqldr["PhoneNum"].ToString() : string.Empty;
                    TdUser.TelNum = System.DBNull.Value != sqldr["TelNum"] ? sqldr["TelNum"].ToString() : string.Empty;
                    TdUser.Email = System.DBNull.Value != sqldr["Email"] ? sqldr["Email"].ToString() : string.Empty;
                    TdUser.LinkMan = System.DBNull.Value != sqldr["LinkMan"] ? sqldr["LinkMan"].ToString() : string.Empty;
                    TdUser.LinkAdress = System.DBNull.Value != sqldr["LinkAdress"] ? sqldr["LinkAdress"].ToString() : string.Empty;
                    TdUser.Sex = System.DBNull.Value != sqldr["Sex"] ? sqldr["Sex"].ToString() : string.Empty;
                    TdUser.OpenMan = System.DBNull.Value != sqldr["OpenMan"] ? sqldr["OpenMan"].ToString() : string.Empty;
                    TdUser.OpenTime = System.DBNull.Value != sqldr["OpenTime"] ? Convert.ToDateTime(sqldr["OpenTime"]) : DateTime.MinValue;
                    TdUser.LastUpdateTime = System.DBNull.Value != sqldr["LastUpdateTime"] ? Convert.ToDateTime(sqldr["LastUpdateTime"]) : DateTime.MinValue;
                    TdUser.LastUpdateID = System.DBNull.Value != sqldr["LastUpdateID"] ? sqldr["LastUpdateID"].ToString() : string.Empty;

                    TdUser.Ip = System.DBNull.Value != sqldr["Ip"] ? sqldr["Ip"].ToString() : string.Empty;
                    TdUser.Mac = System.DBNull.Value != sqldr["Mac"] ? sqldr["Mac"].ToString() : string.Empty;
                    TdUser.LastLoginTime = System.DBNull.Value != sqldr["LastLoginTime"] ? Convert.ToDateTime(sqldr["LastLoginTime"]) : DateTime.MinValue;
                    TdUser.Online = System.DBNull.Value != sqldr["Online"] ? Convert.ToBoolean(sqldr["Online"]) : false;
                    TdUser.MinTrade = System.DBNull.Value != sqldr["MinTrade"] ? Convert.ToDouble(sqldr["MinTrade"]) : 0;
                    TdUser.OrderUnit = System.DBNull.Value != sqldr["OrderUnit"] ? Convert.ToDouble(sqldr["OrderUnit"]) : 0;
                    TdUser.MaxTrade = System.DBNull.Value != sqldr["MaxTrade"] ? Convert.ToDouble(sqldr["MaxTrade"]) : 0;
                    TdUser.PermitRcash = System.DBNull.Value != sqldr["PermitRcash"] ? Convert.ToBoolean(sqldr["PermitRcash"]) : false;
                    TdUser.PermitCcash = System.DBNull.Value != sqldr["PermitCcash"] ? Convert.ToBoolean(sqldr["PermitCcash"]) : false;
                    TdUser.PermitDhuo = System.DBNull.Value != sqldr["PermitDhuo"] ? Convert.ToBoolean(sqldr["PermitDhuo"]) : false;
                    TdUser.PermitHshou = System.DBNull.Value != sqldr["PermitHshou"] ? Convert.ToBoolean(sqldr["PermitHshou"]) : false;
                    TdUser.PermitRstore = System.DBNull.Value != sqldr["PermitRstore"] ? Convert.ToBoolean(sqldr["PermitRstore"]) : false;
                    TdUser.PermitDelOrder = System.DBNull.Value != sqldr["PermitDelOrder"] ? Convert.ToBoolean(sqldr["PermitDelOrder"]) : false;
                    TdUser.CorporationName = System.DBNull.Value != sqldr["CorporationName"] ? sqldr["CorporationName"].ToString() : string.Empty;
                    TdUser.OrderPhone = System.DBNull.Value != sqldr["OrderPhone"] ? sqldr["OrderPhone"].ToString() : string.Empty;

                    TdUser.BankState = System.DBNull.Value != sqldr["BankState"] ? sqldr["BankState"].ToString() : string.Empty;
                    TdUser.Money = System.DBNull.Value != sqldr["Money"] ? Convert.ToDouble(sqldr["Money"]) : 0;
                    TdUser.OccMoney = System.DBNull.Value != sqldr["OccMoney"] ? Convert.ToDouble(sqldr["OccMoney"]) : 0;
                    TdUser.FrozenMoney = System.DBNull.Value != sqldr["FrozenMoney"] ? Convert.ToDouble(sqldr["FrozenMoney"]) : 0;
                    TdUser.BankAccount = System.DBNull.Value != sqldr["BankAccount"] ? sqldr["BankAccount"].ToString() : string.Empty;
                    TdUser.AccountName = System.DBNull.Value != sqldr["AccountName"] ? sqldr["AccountName"].ToString() : string.Empty;
                    TdUser.BankCard = System.DBNull.Value != sqldr["BankCard"] ? sqldr["BankCard"].ToString() : string.Empty;

                    TdUser.SubUser = System.DBNull.Value != sqldr["SubUser"] ? sqldr["SubUser"].ToString() : string.Empty;
                    TdUser.TanUser = System.DBNull.Value != sqldr["TanUser"] ? sqldr["TanUser"].ToString() : string.Empty;
                    TdUser.ConBankType = System.DBNull.Value != sqldr["ConBankType"] ? sqldr["ConBankType"].ToString() : string.Empty;
                    TdUser.OpenBank = System.DBNull.Value != sqldr["OpenBank"] ? sqldr["OpenBank"].ToString() : string.Empty;
                    TdUser.OpenBankAddress = System.DBNull.Value != sqldr["OpenBankAddress"] ? sqldr["OpenBankAddress"].ToString() : string.Empty;

                    TdUser.OrgId = System.DBNull.Value != sqldr["AgentId"] ? sqldr["AgentId"].ToString() : string.Empty;
                    TdUser.CashUser = System.DBNull.Value != sqldr["CashUser"] ? sqldr["CashUser"].ToString() : string.Empty;
                    list.Add(TdUser);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != sqldr)
                {
                    sqldr.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static TradeUser GetTradeUserByUserid(string userid)
        {
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader dbreader = null;
            TradeUser TdUser = new TradeUser();

            try
            {
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
              
                sqlcmd.CommandText = string.Format(@"select [orgid],[Reperson],[OrgName],[telephone],[BindAccount],[userName],[UserId],[status],[Accounttype],[CorporationName],[Account],[LoginPwd],[cashPwd],
                [CardType],[CardNum],[PhoneNum],[TelNum],[Email],[LinkMan],[LinkAdress],[OrderPhone],
                [sex],[OpenMan],[OpenTime],[LastUpdateTime],[LastUpdateID],[Ip],[Mac],[LastLoginTime],[Online],
                [MinTrade],[OrderUnit],[MaxTrade],[PermitRcash],[PermitCcash],[PermitDhuo],[PermitHshou],[DongJieMoney],
                [PermitRstore],[PermitDelOrder], [BankState],[money],[OccMoney],[frozenMoney],[BankAccount],
                [AccountName],[BankCard],[SubUser],[TanUser],[ConBankType],[OpenBank],[OpenBankAddress],[UserType],[usergroupid] from [V_TradeUser] where userid='{0}'", userid);
                dbreader = sqlcmd.ExecuteReader();
                while (dbreader.Read())
                {
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

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != dbreader)
                {
                    dbreader.Close();
                }
            }
            return TdUser;
        }

        /// <summary>
        /// 获取交易用户资金信息
        /// </summary>
        /// <param name="sql">查询SQL</param>
        /// <returns>用户资金信息</returns>
        public static Fundinfo GetFdinfo(string sql)
        {
            Fundinfo Fdinfo = new Fundinfo();

            System.Data.Common.DbDataReader dbreader = null;
            try
            {
                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    Fdinfo.State = System.DBNull.Value != dbreader["State"] ? dbreader["State"].ToString() : string.Empty;
                    Fdinfo.Money = System.DBNull.Value != dbreader["Money"] ? Convert.ToDouble(dbreader["Money"]) : 0;
                    Fdinfo.OccMoney = System.DBNull.Value != dbreader["OccMoney"] ? Convert.ToDouble(dbreader["OccMoney"]) : 0;
                    Fdinfo.FrozenMoney = System.DBNull.Value != dbreader["FrozenMoney"] ? Convert.ToDouble(dbreader["FrozenMoney"]) : 0;
                    Fdinfo.BankAccount = System.DBNull.Value != dbreader["BankAccount"] ? dbreader["BankAccount"].ToString() : string.Empty;
                    Fdinfo.AccountName = System.DBNull.Value != dbreader["AccountName"] ? dbreader["AccountName"].ToString() : string.Empty;
                    Fdinfo.BankCard = System.DBNull.Value != dbreader["BankCard"] ? dbreader["BankCard"].ToString() : string.Empty;
                    Fdinfo.CashUser = System.DBNull.Value != dbreader["CashUser"] ? dbreader["CashUser"].ToString() : string.Empty;
                    Fdinfo.SubUser = System.DBNull.Value != dbreader["SubUser"] ? dbreader["SubUser"].ToString() : string.Empty;
                    Fdinfo.TanUser = System.DBNull.Value != dbreader["TanUser"] ? dbreader["TanUser"].ToString() : string.Empty;
                    Fdinfo.ConBankType = System.DBNull.Value != dbreader["ConBankType"] ? dbreader["ConBankType"].ToString() : string.Empty;
                    Fdinfo.OpenBank = System.DBNull.Value != dbreader["OpenBank"] ? dbreader["OpenBank"].ToString() : string.Empty;
                    Fdinfo.OpenBankAddress = System.DBNull.Value != dbreader["OpenBankAddress"] ? dbreader["OpenBankAddress"].ToString() : string.Empty;
                    Fdinfo.DongJieMoney = System.DBNull.Value != dbreader["DongJieMoney"] ? Convert.ToDouble(dbreader["DongJieMoney"]) : 0; 
                    if (System.DBNull.Value != dbreader["ConTime"])
                    {
                        Fdinfo.ConTime = Convert.ToDateTime(dbreader["ConTime"]);
                    }
                    else
                    {
                        Fdinfo.ConTime = null;
                    }
                    if (System.DBNull.Value != dbreader["RescindTime"])
                    {
                        Fdinfo.RescindTime = Convert.ToDateTime(dbreader["RescindTime"]);
                    }
                    else
                    {
                        Fdinfo.RescindTime = null;
                    }
                    Fdinfo.SameBank = System.DBNull.Value != dbreader["SameBank"] ? Convert.ToBoolean(dbreader["SameBank"]) : false;
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
            return Fdinfo;
        }

        /// <summary>
        /// 获取交易用户资金信息
        /// </summary>
        /// <param name="sql">查询SQL</param>
        /// <param name="userid">返回用户ID</param>
        /// <returns>用户资金信息</returns>
        public static Fundinfo GetFdinfo(string sql, ref string userid)
        {
            Fundinfo Fdinfo = new Fundinfo();
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            try
            {
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = sql;
                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    Fdinfo.State = sqldr["State"].ToString();
                    Fdinfo.Money = Convert.ToDouble(sqldr["Money"]);
                    Fdinfo.OccMoney = Convert.ToDouble(sqldr["OccMoney"]);
                    Fdinfo.FrozenMoney = Convert.ToDouble(sqldr["FrozenMoney"]);
                    Fdinfo.BankAccount = sqldr["BankAccount"].ToString();
                    Fdinfo.AccountName = sqldr["AccountName"].ToString();
                    Fdinfo.BankCard = sqldr["BankCard"].ToString();
                    Fdinfo.CashUser = sqldr["CashUser"].ToString();
                    Fdinfo.SubUser = sqldr["SubUser"].ToString();
                    Fdinfo.TanUser = sqldr["TanUser"].ToString();
                    Fdinfo.ConBankType = sqldr["ConBankType"].ToString();
                    Fdinfo.OpenBank = sqldr["OpenBank"].ToString();
                    Fdinfo.OpenBankAddress = sqldr["OpenBankAddress"].ToString();
                    Fdinfo.ConTime = Convert.ToDateTime(sqldr["ConTime"]);
                    Fdinfo.RescindTime = Convert.ToDateTime(sqldr["RescindTime"]);
                    Fdinfo.SameBank = Convert.ToBoolean(sqldr["SameBank"]);
                    userid = sqldr["userid"].ToString();
                    break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != sqldr)
                {
                    sqldr.Close();
                }
            }
            return Fdinfo;
        }

        /// <summary>
        /// 获取交易日信息列表
        /// </summary>
        /// <param name="sql">查询SQL</param>
        /// <returns>交易日信息列表</returns>
        public static List<WcfInterface.model.DateSet> GetDateSetList(string sql)
        {
            System.Data.Common.DbDataReader dbreader = null;
            List<WcfInterface.model.DateSet> list = new List<WcfInterface.model.DateSet>();
            try
            {

                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    WcfInterface.model.DateSet dataSet = new WcfInterface.model.DateSet();
                    dataSet.Weekday = dbreader["Weekday"].ToString();
                    dataSet.Starttime = dbreader["Starttime"].ToString();
                    dataSet.Endtime = dbreader["Endtime"].ToString();

                    dataSet.Desc = dbreader["Desc"].ToString();

                    dataSet.Istrade = !Convert.ToBoolean(dbreader["Istrade"]);

                    list.Add(dataSet);
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
        /// 获取交易日信息列表
        /// </summary>
        /// <param name="sql">查询SQL</param>
        /// <returns>交易日信息列表</returns>
        public static List<WcfInterface.model.DateSet> GetDateSetListEx(string sql)
        {
            System.Data.Common.DbDataReader dbreader = null;
            List<WcfInterface.model.DateSet> list = new List<WcfInterface.model.DateSet>();
            try
            {

                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    WcfInterface.model.DateSet dataSet = new WcfInterface.model.DateSet();
                    dataSet.Weekday = dbreader["Weekday"].ToString();
                    dataSet.Starttime = dbreader["Starttime"].ToString();
                    dataSet.Endtime = dbreader["Endtime"].ToString();

                    dataSet.Desc = dbreader["Desc"].ToString();
                    dataSet.PriceCode = dbreader["pricecode"].ToString();
                    dataSet.Istrade = !Convert.ToBoolean(dbreader["Istrade"]);

                    list.Add(dataSet);
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
        /// 获取交易日信息列表
        /// </summary>
        /// <param name="sql">查询SQL</param>
        /// <returns>交易日信息列表</returns>
        public static List<WcfInterface.model.DateHoliday> GetDateHolidayList(string sql)
        {
            System.Data.Common.DbDataReader dbreader = null;
            List<WcfInterface.model.DateHoliday> list = new List<WcfInterface.model.DateHoliday>();
            try
            {
                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    WcfInterface.model.DateHoliday dateHoliday = new WcfInterface.model.DateHoliday();
                    dateHoliday.HoliName = dbreader["HoliName"].ToString();
                    dateHoliday.Starttime = Convert.ToDateTime(dbreader["Starttime"]);
                    dateHoliday.Endtime = Convert.ToDateTime(dbreader["Endtime"]);
                    dateHoliday.PriceCode = dbreader["pricecode"].ToString();
                    dateHoliday.Desc = dbreader["Desc"].ToString();
                    dateHoliday.ID = dbreader["ID"].ToString();

                    list.Add(dateHoliday);
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
        /// 获取交易设置
        /// </summary>
        /// <param name="sql">查询SQL</param>
        /// <returns>交易设置</returns>
        public static List<TradeSet> GetTradeSetInfo(string sql)
        {
            System.Data.Common.DbDataReader dbreader = null;
            List<TradeSet> TdSetList = new List<TradeSet>();
            try
            {

                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    TradeSet TdSet = new TradeSet();
                    TdSet.ObjCode = dbreader["ObjCode"].ToString();
                    TdSet.ObjName = dbreader["ObjName"].ToString();
                    TdSet.ObjValue = dbreader["ObjValue"].ToString();
                    TdSet.Remark = dbreader["Remark"].ToString();
                    TdSetList.Add(TdSet);
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
            return TdSetList;
        }
        /// <summary>
        /// 查询新闻公告
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<TradeNews> GetTradeNewsInfo(string sql)
        {
            System.Data.Common.DbDataReader dbreader = null;
            List<TradeNews> TdNewsList = new List<TradeNews>();
            try
            {

                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    TradeNews TdNews = new TradeNews();
                    TdNews.ID = dbreader["ID"].ToString();
                    TdNews.NewsTitle = dbreader["NewsTitle"].ToString();
                    TdNews.NewsContent = dbreader["NewsContent"].ToString();
                    TdNews.PubPerson = dbreader["PubPerson"].ToString();
                    TdNews.PubTime = Convert.ToDateTime(dbreader["PubTime"]);
                    TdNews.Status = (NewsStatus)dbreader["Status"];
                    TdNews.NType = (NewsType)dbreader["NewsType"];
                    TdNews.OverView = System.DBNull.Value != dbreader["OverView"] ? dbreader["OverView"].ToString() : string.Empty;
                    TdNewsList.Add(TdNews);
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
            return TdNewsList;
        }
        /// <summary>
        /// 读取单条新闻
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns></returns>
        public static DataTable GetNewsInfo(string id)
        {
            string strSql = string.Format(@"select * from trade_news where id='{0}'", id);
            return DbHelper.GetDataSet(strSql).Tables[0];
        }
        /// <summary>
        /// 获取管理员日志信息列表
        /// </summary>
        /// <param name="sql">查询SQL</param>
        /// <returns>日志信息列表</returns>
        public static List<ALog> GetAdminLogList(string sql)
        {
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            List<ALog> list = new List<ALog>();
            try
            {
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = sql;
                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    ALog aLog = new ALog();
                    aLog.Account = sqldr["AdminId"].ToString();
                    aLog.OperTime = Convert.ToDateTime(sqldr["OperTime"]);
                    aLog.Desc = sqldr["Desc"].ToString();
                    list.Add(aLog);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != sqldr)
                {
                    sqldr.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// 获取组织日志信息列表
        /// </summary>
        /// <param name="sql">查询SQL</param>
        /// <returns>日志信息列表</returns>
        public static List<ALog> GetAgentLogList(string sql)
        {
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            List<ALog> list = new List<ALog>();
            try
            {
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = sql;
                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    ALog aLog = new ALog();
                    aLog.Account = sqldr["AgentId"].ToString();
                    aLog.OperTime = Convert.ToDateTime(sqldr["OperTime"]);
                    aLog.Desc = sqldr["Desc"].ToString();
                    list.Add(aLog);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != sqldr)
                {
                    sqldr.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// 获取过滤IP信息列表
        /// </summary>
        /// <param name="sql">查询SQL</param>
        /// <returns>过滤IP信息列表</returns>
        public static List<TradeIp> GetTradeIpList(string sql)
        {
            System.Data.Common.DbDataReader dbreader = null;
            List<TradeIp> list = new List<TradeIp>();
            try
            {
                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    TradeIp tradeIp = new TradeIp();
                    tradeIp.StartIp = dbreader["StartIp"].ToString();
                    tradeIp.EndIp = dbreader["EndIp"].ToString();
                    tradeIp.Desc = dbreader["Desc"].ToString();
                    tradeIp.ID = dbreader["ID"].ToString();
                    list.Add(tradeIp);
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
        /// 获取过滤Mac信息列表
        /// </summary>
        /// <param name="sql">查询SQL</param>
        /// <returns>过滤MAC信息列表</returns>
        public static List<TradeMac> GetTradeMacList(string sql)
        {
            System.Data.Common.DbDataReader dbreader = null;
            List<TradeMac> list = new List<TradeMac>();
            try
            {
                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    TradeMac tradeMac = new TradeMac();
                    tradeMac.MAC = dbreader["MAC"].ToString();
                    tradeMac.Desc = dbreader["Desc"].ToString();
                    tradeMac.ID = dbreader["ID"].ToString();
                    list.Add(tradeMac);
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
        /// 获取商品信息列表
        /// </summary>
        /// <param name="sql">查询SQL</param>
        /// <returns>商品信息列表</returns>
        public static List<TradeProduct> GetTradeProductList(string sql)
        {

            System.Data.Common.DbDataReader dbreader = null;
            List<TradeProduct> list = new List<TradeProduct>();
            try
            {
                Dictionary<string, ProPrice> prodic = GetProPrice();
                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    TradeProduct tradeProduct = new TradeProduct();
                    tradeProduct.Adjustbase = System.DBNull.Value != dbreader["Adjustbase"] ? Convert.ToDouble(dbreader["Adjustbase"]) : 0;
                    tradeProduct.Adjustcount = System.DBNull.Value != dbreader["Adjustcount"] ? Convert.ToInt32(dbreader["Adjustcount"]) : 0;
                    tradeProduct.Buystoragefee = System.DBNull.Value != dbreader["Buystoragefee"] ? dbreader["Buystoragefee"].ToString() : string.Empty;
                    tradeProduct.Expressionfee = System.DBNull.Value != dbreader["Expressionfee"] ? dbreader["Expressionfee"].ToString() : string.Empty;
                    tradeProduct.Goodscode = System.DBNull.Value != dbreader["Goodscode"] ? dbreader["Goodscode"].ToString() : string.Empty;
                    tradeProduct.Holdbase = System.DBNull.Value != dbreader["Holdbase"] ? Convert.ToInt32(dbreader["Holdbase"]) : 0;
                    tradeProduct.Lowerprice = System.DBNull.Value != dbreader["Lowerprice"] ? Convert.ToDouble(dbreader["Lowerprice"]) : 0;
                    tradeProduct.Maxprice = System.DBNull.Value != dbreader["Maxprice"] ? Convert.ToDouble(dbreader["Maxprice"]) : 0;
                    tradeProduct.Maxtime = System.DBNull.Value != dbreader["Maxtime"] ? Convert.ToInt32(dbreader["Maxtime"]) : 0;
                    tradeProduct.Minprice = System.DBNull.Value != dbreader["Minprice"] ? Convert.ToDouble(dbreader["Minprice"]) : 0;
                    tradeProduct.Ordemoney = System.DBNull.Value != dbreader["Ordemoney"] ? Convert.ToDouble(dbreader["Ordemoney"]) : 0;
                    tradeProduct.Ordemoneyfee = System.DBNull.Value != dbreader["Ordemoneyfee"] ? dbreader["Ordemoneyfee"].ToString() : string.Empty;
                    tradeProduct.Pricecode = System.DBNull.Value != dbreader["Pricecode"] ? dbreader["Pricecode"].ToString() : string.Empty;
                    tradeProduct.Pricedot = System.DBNull.Value != dbreader["Pricedot"] ? Convert.ToInt32(dbreader["Pricedot"]) : 0;
                    tradeProduct.Productcode = System.DBNull.Value != dbreader["Productcode"] ? dbreader["Productcode"].ToString() : string.Empty;
                    tradeProduct.ProductName = System.DBNull.Value != dbreader["ProductName"] ? dbreader["ProductName"].ToString() : string.Empty;
                    tradeProduct.Sellfee = System.DBNull.Value != dbreader["Sellfee"] ? dbreader["Sellfee"].ToString() : string.Empty;
                    tradeProduct.Sellstoragefee = System.DBNull.Value != dbreader["Sellstoragefee"] ? dbreader["Sellstoragefee"].ToString() : string.Empty;
                    tradeProduct.SetBase = System.DBNull.Value != dbreader["SetBase"] ? Convert.ToInt32(dbreader["SetBase"]) : 0;
                    tradeProduct.State = System.DBNull.Value != dbreader["State"] ? dbreader["State"].ToString() : string.Empty;
                    tradeProduct.Unit = System.DBNull.Value != dbreader["Unit"] ? Convert.ToDouble(dbreader["Unit"]) : 0;
                    tradeProduct.Valuedot = System.DBNull.Value != dbreader["Valuedot"] ? Convert.ToDouble(dbreader["Valuedot"]) : 0;
                    tradeProduct.Starttime = System.DBNull.Value != dbreader["starttime"] ? dbreader["starttime"].ToString() : string.Empty;
                    tradeProduct.Endtime = System.DBNull.Value != dbreader["endtime"] ? dbreader["endtime"].ToString() : string.Empty;

                    if (prodic != null && prodic.ContainsKey(tradeProduct.Pricecode))
                    {
                        tradeProduct.weektime = prodic[tradeProduct.Pricecode].weektime;
                        tradeProduct.realprice = prodic[tradeProduct.Pricecode].realprice;
                    }
                    list.Add(tradeProduct);
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
        /// 获取汇率和水
        /// </summary>
        public static List<WcfInterface.model.TradeRate> GetTradeRateList()
        {
            System.Data.Common.DbDataReader dbreader = null;
            List<model.TradeRate> TradeRateInfoList = new List<model.TradeRate>();
            try
            {

                string sql = "select distinct pricecode,rate,water from Trade_DataSource ";

                dbreader = DbHelper.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    model.TradeRate tr = new model.TradeRate();
                    tr.PriceCode = dbreader["pricecode"].ToString();
                    tr.Rate = Convert.ToDouble(dbreader["rate"]);
                    tr.Water = Convert.ToDouble(dbreader["water"]);
                    TradeRateInfoList.Add(tr);
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

            return TradeRateInfoList;
        }
        /// <summary>
        /// 根据行情编码获取Trade_DataSource表中的数据
        /// </summary>
        /// <param name="pricecode"></param>
        /// <returns></returns>
        public static TradeDataSource GetTradeDataSource(string pricecode)
        {
            System.Data.Common.DbDataReader dbreader = null;
            TradeDataSource tdsource= new TradeDataSource();
            try
            {

                string sql = string.Format("select pricecode, rate, Coefxs, water, adjustcount, coefficient, IsConvert from Trade_DataSource where pricecode='{0}' ", pricecode);

                dbreader = DbHelper.ExecuteReader(sql);
                if (dbreader.Read())
                {
                    tdsource.pricecode = dbreader["pricecode"].ToString();
                    tdsource.rate = Convert.ToDouble(dbreader["rate"]);
                    tdsource.coefxs = Convert.ToDouble(dbreader["Coefxs"]);
                    tdsource.water = Convert.ToDouble(dbreader["water"]);
                    tdsource.adjustcount = Convert.ToInt32(dbreader["adjustcount"]);
                    tdsource.coefficient = Convert.ToDouble(dbreader["coefficient"]);
                    tdsource.IsConvert = Convert.ToInt32(dbreader["IsConvert"]);
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

            return tdsource;
        }

        /// <summary>
        /// 获取行情编码和实时价格
        /// </summary>
        /// <returns>获取行情编码和实时价格:格式为[行情编码,价格|行情编码,价格|行情编码,价格|行情编码,价格|...]</returns>
        public static string GetProgramData()
        {
            string data = string.Empty;
            Dictionary<string, ProPrice> prodic = GetProPrice();
            if (!(prodic != null && prodic.Count > 0))
            {
                return string.Empty;
            }

            foreach (var item in prodic)
            {
                data += string.Format("{0},{1}|", item.Key, item.Value.realprice);
            }
            if (data.Length > 1)
            {
                data = data.Substring(0, data.Length - 1);
            }
            else
            {
                return string.Empty;
            }
            return data;
        }

        /// <summary>
        /// 获取对冲信息
        /// </summary>
        /// <param name="sqlCondition">查询条件</param>
        /// <param name="hedgingInfo"></param>
        /// <returns>对冲信息</returns>
        public static void GetHedgingList(string sqlCondition, ref HedgingInfo hedgingInfo)
        {

            System.Data.Common.DbDataReader dbreader = null;
            try
            {

                Dictionary<string, ProPrice> prodic = GetProPrice();
                if (!(prodic != null && prodic.Count > 0))
                {
                    return ;
                }
                string data = string.Empty;
                foreach (var item in prodic)
                {
                    data += string.Format("{0},{1}|",item.Key,item.Value.realprice);
                }
                if (data.Length > 1)
                {
                    data = data.Substring(0, data.Length - 1);
                }
                else
                {
                    return ;
                }
                dbreader = DbHelper.RunProcedureGetDataReader("proc_GetHedgingEx",
                      new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@data",DbParameterType.String,data,ParameterDirection.Input),
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SearchCondition",DbParameterType.String,sqlCondition,ParameterDirection.Input)});
                while (dbreader.Read())
                {
                    Hedging hedging = new Hedging();
                    hedging.AvgOrderPrice = Convert.ToDouble(dbreader["avg_orderprice"]);
                    hedging.HedgingProfitLoss = Convert.ToDouble(dbreader["hd_yingkui"]);
                    hedging.HedgingQuantity = Convert.ToDouble(dbreader["hd_quantity"]);
                    hedging.HedgingStorageFee = Convert.ToDouble(dbreader["hd_storagefee"]);
                    hedging.HedgingTradeFee = Convert.ToDouble(dbreader["hd_tradefee"]);
                    hedging.OrderType = dbreader["ordertype"].ToString();
                    hedging.ProductName = dbreader["productname"].ToString();
                    hedging.ProfitLoss = Convert.ToDouble(dbreader["yingkui"]);
                    hedging.Quantity = Convert.ToDouble(dbreader["S_quantity"]);
                    hedging.RealPrice = Convert.ToDouble(dbreader["realprice"]);
                    hedging.StorageFee = Convert.ToDouble(dbreader["S_storagefee"]);
                    hedging.TradeFee = Convert.ToDouble(dbreader["S_tradefee"]);
                    hedgingInfo.HedgingList.Add(hedging);
                }
                if (dbreader.NextResult())//前进到下一结果集 
                {
                    if (dbreader.Read()) //获取汇总数据
                    {
                        hedgingInfo.Quantity = System.DBNull.Value != dbreader["S_quantity"] ? Convert.ToDouble(dbreader["S_quantity"]) : 0;
                        hedgingInfo.ProfitValue = System.DBNull.Value != dbreader["yingkui"] ? Convert.ToDouble(dbreader["yingkui"]) : 0;
                        hedgingInfo.StorageFee = System.DBNull.Value != dbreader["S_storagefee"] ? Convert.ToDouble(dbreader["S_storagefee"]) : 0;
                        hedgingInfo.TradeFee = System.DBNull.Value != dbreader["S_tradefee"] ? Convert.ToDouble(dbreader["S_tradefee"]) : 0;
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
                    dbreader.Close(); dbreader.Dispose();
                }
            }
        }

        public static void GetTradeJuJianList(JJQueryCon JJqc, string ParentOrgID, ref TradeJuJianInfo TdInfo)
        {

            System.Data.Common.DbDataReader dbreader = null;
            try
            {
                string orgname = string.Empty;
                if (!string.IsNullOrEmpty(JJqc.OrgName))
                {
                    orgname = JJqc.OrgName;
                }
                dbreader = DbHelper.RunProcedureGetDataReader("GetJujianDataEx",
                      new System.Data.Common.DbParameter[]{
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ParentOrgID",DbParameterType.String,ParentOrgID,ParameterDirection.Input),

                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@OrgID",DbParameterType.String,orgname,ParameterDirection.Input),

                     DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@StartTime",DbParameterType.String,JJqc.StartTime.ToString("yyyy-MM-dd"),ParameterDirection.Input),

                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@EndTime",DbParameterType.String,JJqc.EndTime.ToString("yyyy-MM-dd"),ParameterDirection.Input)});
                while (dbreader.Read())
                {
                    TradeJuJian tdjj = new TradeJuJian();
                    tdjj.OrgName = System.DBNull.Value != dbreader["orgname"] ? dbreader["orgname"].ToString() : string.Empty;
                    tdjj.qichu = System.DBNull.Value != dbreader["qichu"] ? Convert.ToDouble(dbreader["qichu"]) : 0;
                    tdjj.qimo = System.DBNull.Value != dbreader["qimo"] ? Convert.ToDouble(dbreader["qimo"]) : 0;
                    tdjj.rujin = System.DBNull.Value != dbreader["rujin"] ? Convert.ToDouble(dbreader["rujin"]) : 0;
                    tdjj.chujin = System.DBNull.Value != dbreader["chujin"] ? Convert.ToDouble(dbreader["chujin"]) : 0;
                    tdjj.Manual_rujin = System.DBNull.Value != dbreader["manual_rujin"] ? Convert.ToDouble(dbreader["manual_rujin"]) : 0;
                    tdjj.Manual_chujin = System.DBNull.Value != dbreader["manual_chujin"] ? Convert.ToDouble(dbreader["manual_chujin"]) : 0;
                    tdjj.Money = System.DBNull.Value != dbreader["money"] ? Convert.ToDouble(dbreader["money"]) : 0;
                    tdjj.Hisyingkui = System.DBNull.Value != dbreader["hisyingkui"] ? Convert.ToDouble(dbreader["hisyingkui"]) : 0;
                    tdjj.tradefee = System.DBNull.Value != dbreader["tradefee"] ? Convert.ToDouble(dbreader["tradefee"]) : 0;
                    tdjj.storagefee = System.DBNull.Value != dbreader["storagefee"] ? Convert.ToDouble(dbreader["storagefee"]) : 0;

                    tdjj.KC_Copper_20t_Num = System.DBNull.Value != dbreader["KC_Copper_20t_Num"] ? Convert.ToDouble(dbreader["KC_Copper_20t_Num"]) : 0;
                    tdjj.KC_Copper_50t_Num = System.DBNull.Value != dbreader["KC_Copper_50t_Num"] ? Convert.ToDouble(dbreader["KC_Copper_50t_Num"]) : 0;
                    tdjj.KC_UKOil_100_Num = System.DBNull.Value != dbreader["KC_UKOil_100_Num"] ? Convert.ToDouble(dbreader["KC_UKOil_100_Num"]) : 0;
                    tdjj.KC_UKOil_20_Num = System.DBNull.Value != dbreader["KC_UKOil_20_Num"] ? Convert.ToDouble(dbreader["KC_UKOil_20_Num"]) : 0;
                    tdjj.KC_UKOil_50_Num = System.DBNull.Value != dbreader["KC_UKOil_50_Num"] ? Convert.ToDouble(dbreader["KC_UKOil_50_Num"]) : 0;
                    tdjj.KC_XAG_100kg_Num = System.DBNull.Value != dbreader["KC_XAG_100kg_Num"] ? Convert.ToDouble(dbreader["KC_XAG_100kg_Num"]) : 0;
                    tdjj.KC_XAG_20kg_Num = System.DBNull.Value != dbreader["KC_XAG_20kg_Num"] ? Convert.ToDouble(dbreader["KC_XAG_20kg_Num"]) : 0;
                    tdjj.KC_XAG_50kg_Num = System.DBNull.Value != dbreader["KC_XAG_50kg_Num"] ? Convert.ToDouble(dbreader["KC_XAG_50kg_Num"]) : 0;
                    tdjj.KC_XAU_1000g_Num = System.DBNull.Value != dbreader["KC_XAU_1000g_Num"] ? Convert.ToDouble(dbreader["KC_XAU_1000g_Num"]) : 0;
                    tdjj.KC_XPD_1000g_Num = System.DBNull.Value != dbreader["KC_XPD_1000g_Num"] ? Convert.ToDouble(dbreader["KC_XPD_1000g_Num"]) : 0;
                    tdjj.KC_XPT_1000g_Num = System.DBNull.Value != dbreader["KC_XPT_1000g_Num"] ? Convert.ToDouble(dbreader["KC_XPT_1000g_Num"]) : 0;

                    tdjj.XAG_100kg_Num = System.DBNull.Value != dbreader["XAG_100kg_Num"] ? Convert.ToDouble(dbreader["XAG_100kg_Num"]) : 0;
                    tdjj.XAG_20kg_Num = System.DBNull.Value != dbreader["XAG_20kg_Num"] ? Convert.ToDouble(dbreader["XAG_20kg_Num"]) : 0;
                    tdjj.XAG_50kg_Num = System.DBNull.Value != dbreader["XAG_50kg_Num"] ? Convert.ToDouble(dbreader["XAG_50kg_Num"]) : 0;
                    tdjj.XAU_1000g_Num = System.DBNull.Value != dbreader["XAU_1000g_Num"] ? Convert.ToDouble(dbreader["XAU_1000g_Num"]) : 0;
                    tdjj.XPD_1000g_Num = System.DBNull.Value != dbreader["XPD_1000g_Num"] ? Convert.ToDouble(dbreader["XPD_1000g_Num"]) : 0;
                    tdjj.XPT_1000g_Num = System.DBNull.Value != dbreader["XPT_1000g_Num"] ? Convert.ToDouble(dbreader["XPT_1000g_Num"]) : 0;
                    tdjj.Copper_20t_Num = System.DBNull.Value != dbreader["Copper_20t_Num"] ? Convert.ToDouble(dbreader["Copper_20t_Num"]) : 0;
                    tdjj.Copper_50t_Num = System.DBNull.Value != dbreader["Copper_50t_Num"] ? Convert.ToDouble(dbreader["Copper_50t_Num"]) : 0;
                    tdjj.UKOil_100_Num = System.DBNull.Value != dbreader["UKOil_100_Num"] ? Convert.ToDouble(dbreader["UKOil_100_Num"]) : 0;
                    tdjj.UKOil_50_Num = System.DBNull.Value != dbreader["UKOil_50_Num"] ? Convert.ToDouble(dbreader["UKOil_50_Num"]) : 0;
                    tdjj.UKOil_20_Num = System.DBNull.Value != dbreader["UKOil_20_Num"] ? Convert.ToDouble(dbreader["UKOil_20_Num"]) : 0;

                    tdjj.XAUUSD_DHAVG_PRICE = System.DBNull.Value != dbreader["XAUUSD_DHAVG_PRICE"] ? Convert.ToDouble(dbreader["XAUUSD_DHAVG_PRICE"]) : 0;
                    tdjj.XAGUSD_DHAVG_PRICE = System.DBNull.Value != dbreader["XAGUSD_DHAVG_PRICE"] ? Convert.ToDouble(dbreader["XAGUSD_DHAVG_PRICE"]) : 0;
                    tdjj.XPDUSD_DHAVG_PRICE = System.DBNull.Value != dbreader["XPDUSD_DHAVG_PRICE"] ? Convert.ToDouble(dbreader["XPDUSD_DHAVG_PRICE"]) : 0;
                    tdjj.XPTUSD_DHAVG_PRICE = System.DBNull.Value != dbreader["XPTUSD_DHAVG_PRICE"] ? Convert.ToDouble(dbreader["XPTUSD_DHAVG_PRICE"]) : 0;
                    tdjj.Copper_DHAVG_PRICE = System.DBNull.Value != dbreader["Copper_DHAVG_PRICE"] ? Convert.ToDouble(dbreader["Copper_DHAVG_PRICE"]) : 0;
                    tdjj.UKOil_DHAVG_PRICE = System.DBNull.Value != dbreader["UKOil_DHAVG_PRICE"] ? Convert.ToDouble(dbreader["UKOil_DHAVG_PRICE"]) : 0;
                    tdjj.EURGBP_DHAVG_PRICE = System.DBNull.Value != dbreader["EURGBP_DHAVG_PRICE"] ? Convert.ToDouble(dbreader["EURGBP_DHAVG_PRICE"]) : 0;
                    tdjj.GBPUSD_DHAVG_PRICE = System.DBNull.Value != dbreader["GBPUSD_DHAVG_PRICE"] ? Convert.ToDouble(dbreader["GBPUSD_DHAVG_PRICE"]) : 0;
                    tdjj.EURUSD_DHAVG_PRICE = System.DBNull.Value != dbreader["EURUSD_DHAVG_PRICE"] ? Convert.ToDouble(dbreader["EURUSD_DHAVG_PRICE"]) : 0;
                    tdjj.USDJPY_DHAVG_PRICE = System.DBNull.Value != dbreader["USDJPY_DHAVG_PRICE"] ? Convert.ToDouble(dbreader["USDJPY_DHAVG_PRICE"]) : 0;
                    tdjj.USDCHF_DHAVG_PRICE = System.DBNull.Value != dbreader["USDCHF_DHAVG_PRICE"] ? Convert.ToDouble(dbreader["USDCHF_DHAVG_PRICE"]) : 0;
                    tdjj.USDOLLAR_DHAVG_PRICE = System.DBNull.Value != dbreader["USDOLLAR_DHAVG_PRICE"] ? Convert.ToDouble(dbreader["USDOLLAR_DHAVG_PRICE"]) : 0;

                    tdjj.XAUUSD_HSAVG_PRICE = System.DBNull.Value != dbreader["XAUUSD_HSAVG_PRICE"] ? Convert.ToDouble(dbreader["XAUUSD_HSAVG_PRICE"]) : 0;
                    tdjj.XAGUSD_HSAVG_PRICE = System.DBNull.Value != dbreader["XAGUSD_HSAVG_PRICE"] ? Convert.ToDouble(dbreader["XAGUSD_HSAVG_PRICE"]) : 0;
                    tdjj.XPDUSD_HSAVG_PRICE = System.DBNull.Value != dbreader["XPDUSD_HSAVG_PRICE"] ? Convert.ToDouble(dbreader["XPDUSD_HSAVG_PRICE"]) : 0;
                    tdjj.XPTUSD_HSAVG_PRICE = System.DBNull.Value != dbreader["XPTUSD_HSAVG_PRICE"] ? Convert.ToDouble(dbreader["XPTUSD_HSAVG_PRICE"]) : 0;
                    tdjj.Copper_HSAVG_PRICE = System.DBNull.Value != dbreader["Copper_HSAVG_PRICE"] ? Convert.ToDouble(dbreader["Copper_HSAVG_PRICE"]) : 0;
                    tdjj.UKOil_HSAVG_PRICE = System.DBNull.Value != dbreader["UKOil_HSAVG_PRICE"] ? Convert.ToDouble(dbreader["UKOil_HSAVG_PRICE"]) : 0;
                    tdjj.EURGBP_HSAVG_PRICE = System.DBNull.Value != dbreader["EURGBP_HSAVG_PRICE"] ? Convert.ToDouble(dbreader["EURGBP_HSAVG_PRICE"]) : 0;
                    tdjj.GBPUSD_HSAVG_PRICE = System.DBNull.Value != dbreader["GBPUSD_HSAVG_PRICE"] ? Convert.ToDouble(dbreader["GBPUSD_HSAVG_PRICE"]) : 0;
                    tdjj.EURUSD_HSAVG_PRICE = System.DBNull.Value != dbreader["EURUSD_HSAVG_PRICE"] ? Convert.ToDouble(dbreader["EURUSD_HSAVG_PRICE"]) : 0;
                    tdjj.USDJPY_HSAVG_PRICE = System.DBNull.Value != dbreader["USDJPY_HSAVG_PRICE"] ? Convert.ToDouble(dbreader["USDJPY_HSAVG_PRICE"]) : 0;
                    tdjj.USDCHF_HSAVG_PRICE = System.DBNull.Value != dbreader["USDCHF_HSAVG_PRICE"] ? Convert.ToDouble(dbreader["USDCHF_HSAVG_PRICE"]) : 0;
                    tdjj.USDOLLAR_HSAVG_PRICE = System.DBNull.Value != dbreader["USDOLLAR_HSAVG_PRICE"] ? Convert.ToDouble(dbreader["USDOLLAR_HSAVG_PRICE"]) : 0;

                    tdjj.XAUUSD_DH_NUM = System.DBNull.Value != dbreader["XAUUSD_DH_NUM"] ? Convert.ToDouble(dbreader["XAUUSD_DH_NUM"]) : 0;
                    tdjj.XAGUSD_DH_NUM = System.DBNull.Value != dbreader["XAGUSD_DH_NUM"] ? Convert.ToDouble(dbreader["XAGUSD_DH_NUM"]) : 0;
                    tdjj.XPDUSD_DH_NUM = System.DBNull.Value != dbreader["XPDUSD_DH_NUM"] ? Convert.ToDouble(dbreader["XPDUSD_DH_NUM"]) : 0;
                    tdjj.XPTUSD_DH_NUM = System.DBNull.Value != dbreader["XPTUSD_DH_NUM"] ? Convert.ToDouble(dbreader["XPTUSD_DH_NUM"]) : 0;
                    tdjj.Copper_DH_NUM = System.DBNull.Value != dbreader["Copper_DH_NUM"] ? Convert.ToDouble(dbreader["Copper_DH_NUM"]) : 0;
                    tdjj.UKOil_DH_NUM = System.DBNull.Value != dbreader["UKOil_DH_NUM"] ? Convert.ToDouble(dbreader["UKOil_DH_NUM"]) : 0;
                    tdjj.EURGBP_DH_NUM = System.DBNull.Value != dbreader["EURGBP_DH_NUM"] ? Convert.ToDouble(dbreader["EURGBP_DH_NUM"]) : 0;
                    tdjj.GBPUSD_DH_NUM = System.DBNull.Value != dbreader["GBPUSD_DH_NUM"] ? Convert.ToDouble(dbreader["GBPUSD_DH_NUM"]) : 0;
                    tdjj.EURUSD_DH_NUM = System.DBNull.Value != dbreader["EURUSD_DH_NUM"] ? Convert.ToDouble(dbreader["EURUSD_DH_NUM"]) : 0;
                    tdjj.USDJPY_DH_NUM = System.DBNull.Value != dbreader["USDJPY_DH_NUM"] ? Convert.ToDouble(dbreader["USDJPY_DH_NUM"]) : 0;
                    tdjj.USDCHF_DH_NUM = System.DBNull.Value != dbreader["USDCHF_DH_NUM"] ? Convert.ToDouble(dbreader["USDCHF_DH_NUM"]) : 0;
                    tdjj.USDOLLAR_DH_NUM = System.DBNull.Value != dbreader["USDOLLAR_DH_NUM"] ? Convert.ToDouble(dbreader["USDOLLAR_DH_NUM"]) : 0;

                    tdjj.XAUUSD_HS_NUM = System.DBNull.Value != dbreader["XAUUSD_HS_NUM"] ? Convert.ToDouble(dbreader["XAUUSD_HS_NUM"]) : 0;
                    tdjj.XAGUSD_HS_NUM = System.DBNull.Value != dbreader["XAGUSD_HS_NUM"] ? Convert.ToDouble(dbreader["XAGUSD_HS_NUM"]) : 0;
                    tdjj.XPDUSD_HS_NUM = System.DBNull.Value != dbreader["XPDUSD_HS_NUM"] ? Convert.ToDouble(dbreader["XPDUSD_HS_NUM"]) : 0;
                    tdjj.XPTUSD_HS_NUM = System.DBNull.Value != dbreader["XPTUSD_HS_NUM"] ? Convert.ToDouble(dbreader["XPTUSD_HS_NUM"]) : 0;
                    tdjj.Copper_HS_NUM = System.DBNull.Value != dbreader["Copper_HS_NUM"] ? Convert.ToDouble(dbreader["Copper_HS_NUM"]) : 0;
                    tdjj.UKOil_HS_NUM = System.DBNull.Value != dbreader["UKOil_HS_NUM"] ? Convert.ToDouble(dbreader["UKOil_HS_NUM"]) : 0;
                    tdjj.EURGBP_HS_NUM = System.DBNull.Value != dbreader["EURGBP_HS_NUM"] ? Convert.ToDouble(dbreader["EURGBP_HS_NUM"]) : 0;
                    tdjj.GBPUSD_HS_NUM = System.DBNull.Value != dbreader["GBPUSD_HS_NUM"] ? Convert.ToDouble(dbreader["GBPUSD_HS_NUM"]) : 0;
                    tdjj.EURUSD_HS_NUM = System.DBNull.Value != dbreader["EURUSD_HS_NUM"] ? Convert.ToDouble(dbreader["EURUSD_HS_NUM"]) : 0;
                    tdjj.USDJPY_HS_NUM = System.DBNull.Value != dbreader["USDJPY_HS_NUM"] ? Convert.ToDouble(dbreader["USDJPY_HS_NUM"]) : 0;
                    tdjj.USDCHF_HS_NUM = System.DBNull.Value != dbreader["USDCHF_HS_NUM"] ? Convert.ToDouble(dbreader["USDCHF_HS_NUM"]) : 0;
                    tdjj.USDOLLAR_HS_NUM = System.DBNull.Value != dbreader["USDOLLAR_HS_NUM"] ? Convert.ToDouble(dbreader["USDOLLAR_HS_NUM"]) : 0;

                    TdInfo.TdJuJianList.Add(tdjj);
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
                    dbreader.Close(); dbreader.Dispose();
                }
            }
        }



        /// <summary>
        /// 签约华夏银行 本行开户
        /// </summary>
        /// <param name="TdUser">用户信息</param>
        /// <param name="codeDesc">签约结果代码描述</param>
        /// <returns>成功返回true 失败返回fasle</returns>
        public static bool ContactToHuaxiaSelfBank(TradeUser TdUser, ref string codeDesc)
        {
            bool IsSuc = false;
            IntersServerImplClient ic = new IntersServerImplClient();
            try
            {
                StringBuilder inXml = new StringBuilder();
                inXml.Append("<HXBB2B>");
                inXml.Append("<MessageData>");

                inXml.Append("<Base>");
                inXml.Append("<Version>1.0</Version>");
                inXml.Append("<SignFlag>0</SignFlag>");
                inXml.Append("<Language>GB2312</Language>");
                inXml.Append("</Base>");

                inXml.Append("<ReqHeader>");
                inXml.AppendFormat("<ClientTime>{0}</ClientTime>", DateTime.Now.ToString("yyyyMMddHHmmss"));
                inXml.Append("<MerchantNo>600014</MerchantNo>");
                inXml.Append("</ReqHeader>");

                inXml.Append("<DataBody>");
                inXml.AppendFormat("<MerTxSerNo>{0}</MerTxSerNo>", TdUser.CashUser);
                inXml.Append("<TrnxCode>DZ010</TrnxCode>");
                inXml.Append("<AccountInfos>");
                inXml.Append("<AccountInfo>");

                inXml.Append("<AccountNo></AccountNo>");
                inXml.AppendFormat("<MerAccountNo>{0}</MerAccountNo>", GetTanUser(TdUser.TanUser));
                inXml.AppendFormat("<AccountName>{0}</AccountName>", TdUser.UserName);
                inXml.AppendFormat("<AccountProp>{0}</AccountProp>", "0" == TdUser.AccountType ? 1 : 0);//华夏银行接口中，0表示企业1表示个人
                inXml.Append("<Amt></Amt>");//总余额
                inXml.Append("<AmtUse></AmtUse>");//可用余额
                inXml.AppendFormat("<PersonName>{0}</PersonName>", string.IsNullOrEmpty(TdUser.LinkMan) ? string.Empty : TdUser.LinkMan);
                inXml.AppendFormat("<OfficeTel>{0}</OfficeTel>", string.IsNullOrEmpty(TdUser.TelNum) ? string.Empty : TdUser.TelNum);
                inXml.AppendFormat("<MobileTel>{0}</MobileTel>", string.IsNullOrEmpty(TdUser.PhoneNum) ? string.Empty : TdUser.PhoneNum);
                inXml.AppendFormat("<Addr>{0}</Addr>", string.IsNullOrEmpty(TdUser.LinkAdress) ? string.Empty : TdUser.LinkAdress);

                inXml.Append("</AccountInfo>");
                inXml.Append("</AccountInfos>");
                inXml.Append("</DataBody>");

                inXml.Append("</MessageData>");
                inXml.Append("</HXBB2B>");
                com.individual.helper.LogNet4.WriteMsg("华夏银行本行签约,请求的Xml报文:" + inXml.ToString());
                string outXml = ic.Process(inXml.ToString());
                com.individual.helper.LogNet4.WriteMsg("华夏银行本行签约,响应的Xml报文:" + outXml);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(outXml);
                if (HuaxiaSuc == xmldoc.SelectSingleNode("HXBB2B/MessageData/ResHeader/Status/Code").InnerText)
                {
                    IsSuc = true;
                }
                codeDesc = xmldoc.SelectSingleNode("HXBB2B/MessageData/ResHeader/Status/Message").InnerText;
                ic.Close();
            }
            catch (Exception ex)
            {
                ic.Abort();
                throw new Exception(ex.Message, ex);
            }

            return IsSuc;
        }

        /// <summary>
        /// 签约华夏银行 他行开户
        /// </summary>
        /// <param name="TdUser">用户信息</param>
        /// <param name="openbank">开户行信息</param>
        /// <param name="codeDesc">签约结果代码描述</param>
        /// <param name="subUser">子账号</param>
        /// <returns>成功返回true 失败返回fasle</returns>
        public static bool ContactToHuaxiaOhterBank(TradeUser TdUser, OpenBankInfo openbank, ref string codeDesc, ref string subUser)
        {
            bool IsSuc = false;
            IntersServerImplClient ic = new IntersServerImplClient();
            try
            {
                StringBuilder inXml = new StringBuilder();
                inXml.Append("<HXBB2B>");
                inXml.Append("<MessageData>");

                inXml.Append("<Base>");
                inXml.Append("<Version>1.0</Version>");
                inXml.Append("<SignFlag>0</SignFlag>");
                inXml.Append("<Language>GB2312</Language>");
                inXml.Append("</Base>");

                inXml.Append("<ReqHeader>");
                inXml.AppendFormat("<ClientTime>{0}</ClientTime>", DateTime.Now.ToString("yyyyMMddHHmmss"));
                inXml.Append("<MerchantNo>600014</MerchantNo>");
                inXml.Append("</ReqHeader>");

                inXml.Append("<DataBody>");
                inXml.AppendFormat("<MerTxSerNo>{0}</MerTxSerNo>", TdUser.CashUser);
                inXml.Append("<TrnxCode>DZ020</TrnxCode>");
                inXml.Append("<AccountInfos>");
                inXml.Append("<AccountInfo>");

                inXml.AppendFormat("<MerAccountNo>{0}</MerAccountNo>", GetTanUser(TdUser.TanUser));
                inXml.AppendFormat("<AccountName>{0}</AccountName>", TdUser.UserName);
                inXml.AppendFormat("<AccountProp>{0}</AccountProp>", "0" == TdUser.AccountType ? 1 : 0);//华夏银行接口中，0表示企业1表示个人
                inXml.AppendFormat("<RelatingAcct>{0}</RelatingAcct>", openbank.BankCard);
                inXml.AppendFormat("<RelatingAcctName>{0}</RelatingAcctName>", openbank.AccountName);
                inXml.Append("<InterBankFlag>1</InterBankFlag>");
                inXml.AppendFormat("<RelatingAcctBank>{0}</RelatingAcctBank>", openbank.OpenBank);
                inXml.AppendFormat("<RelatingAcctBankAddr>{0}</RelatingAcctBankAddr>", openbank.OpenBankAddress);
                inXml.AppendFormat("<RelatingAcctBankCode>{0}</RelatingAcctBankCode>", openbank.BankAccount);
                inXml.Append("<Amt></Amt>");
                inXml.Append("<AmtUse></AmtUse>");
                inXml.AppendFormat("<PersonName>{0}</PersonName>", string.IsNullOrEmpty(TdUser.LinkMan) ? string.Empty : TdUser.LinkMan);
                inXml.AppendFormat("<OfficeTel>{0}</OfficeTel>", string.IsNullOrEmpty(TdUser.TelNum) ? string.Empty : TdUser.TelNum);
                inXml.AppendFormat("<MobileTel>{0}</MobileTel>", string.IsNullOrEmpty(TdUser.PhoneNum) ? string.Empty : TdUser.PhoneNum);
                inXml.AppendFormat("<Addr>{0}</Addr>", string.IsNullOrEmpty(TdUser.LinkAdress) ? string.Empty : TdUser.LinkAdress);
                inXml.Append("<ZipCode></ZipCode>");
                inXml.AppendFormat("<LawName>{0}</LawName>", string.IsNullOrEmpty(TdUser.CorporationName) ? string.Empty : TdUser.CorporationName);
                inXml.AppendFormat("<LawType>{0}</LawType>", "1" == TdUser.CardType ? 1 : 6);
                //华夏证件类型:
                //1 – 个人身份证
                //2 – 军人证、警官证
                //3 – 临时证件
                //4 – 户口本
                //5 – 护照
                //6 – 其他

                inXml.AppendFormat("<LawNo>{0}</LawNo>", string.IsNullOrEmpty(TdUser.CardNum) ? string.Empty : TdUser.CardNum);
                inXml.Append("<NoteFlag>1</NoteFlag>");
                inXml.AppendFormat("<NotePhone>{0}</NotePhone>", string.IsNullOrEmpty(TdUser.PhoneNum) ? string.Empty : TdUser.PhoneNum);
                inXml.AppendFormat("<EMail>{0}</EMail>", string.IsNullOrEmpty(TdUser.Email) ? string.Empty : TdUser.Email);
                inXml.AppendFormat("<CheckFlag>{0}</CheckFlag>", "1" == TdUser.AccountType ? 1 : 0);

                inXml.Append("</AccountInfo>");
                inXml.Append("</AccountInfos>");
                inXml.Append("</DataBody>");

                inXml.Append("</MessageData>");
                inXml.Append("</HXBB2B>");
                com.individual.helper.LogNet4.WriteMsg("华夏银行他行签约,请求的Xml报文:" + inXml.ToString());
                string outXml = ic.Process(inXml.ToString());
                com.individual.helper.LogNet4.WriteMsg("华夏银行他行签约,响应的Xml报文:" + outXml);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(outXml);
                if (HuaxiaSuc == xmldoc.SelectSingleNode("HXBB2B/MessageData/ResHeader/Status/Code").InnerText)
                {
                    subUser = xmldoc.SelectSingleNode("HXBB2B/MessageData/DataBody/AccountInfos/AccountInfo/AccountNo").InnerText;
                    IsSuc = true;
                }
                codeDesc = xmldoc.SelectSingleNode("HXBB2B/MessageData/ResHeader/Status/Message").InnerText;
                ic.Close();
            }
            catch (Exception ex)
            {
                ic.Abort();
                throw new Exception(ex.Message, ex);
            }

            return IsSuc;
        }

        /// <summary>
        /// 华夏银行解约
        /// </summary>
        /// <param name="FdInfo">资金信息</param>
        /// <param name="codeDesc">解约结果代码描述</param>
        /// <returns>成功返回true 失败返回fasle</returns>
        public static bool RescindHuaxiaBank(Fundinfo FdInfo, ref string codeDesc)
        {
            bool IsSuc = false;
            IntersServerImplClient ic = new IntersServerImplClient();
            try
            {
                StringBuilder inXml = new StringBuilder();
                inXml.Append("<HXBB2B>");
                inXml.Append("<MessageData>");

                inXml.Append("<Base>");
                inXml.Append("<Version>1.0</Version>");
                inXml.Append("<SignFlag>0</SignFlag>");
                inXml.Append("<Language>GB2312</Language>");
                inXml.Append("</Base>");

                inXml.Append("<ReqHeader>");
                inXml.AppendFormat("<ClientTime>{0}</ClientTime>", DateTime.Now.ToString("yyyyMMddHHmmss"));
                inXml.Append("<MerchantNo>600014</MerchantNo>");
                inXml.Append("</ReqHeader>");

                inXml.Append("<DataBody>");
                inXml.AppendFormat("<MerTxSerNo>{0}</MerTxSerNo>", FdInfo.CashUser);
                inXml.Append("<TrnxCode>DZ012</TrnxCode>");
                inXml.AppendFormat("<AccountNo>{0}</AccountNo>", FdInfo.SubUser);
                inXml.AppendFormat("<MerAccountNo>{0}</MerAccountNo>", GetTanUser(FdInfo.TanUser));
                inXml.Append("</DataBody>");

                inXml.Append("</MessageData>");
                inXml.Append("</HXBB2B>");
                com.individual.helper.LogNet4.WriteMsg("华夏银行解约,请求的Xml报文:" + inXml.ToString());
                string outXml = ic.Process(inXml.ToString());
                com.individual.helper.LogNet4.WriteMsg("华夏银行解约,响应的Xml报文:" + outXml);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(outXml);
                if (HuaxiaSuc == xmldoc.SelectSingleNode("HXBB2B/MessageData/ResHeader/Status/Code").InnerText)
                {
                    IsSuc = true;
                }
                codeDesc = xmldoc.SelectSingleNode("HXBB2B/MessageData/ResHeader/Status/Message").InnerText;
                ic.Close();
            }
            catch (Exception ex)
            {
                ic.Abort();
                throw new Exception(ex.Message, ex);
            }

            return IsSuc;
        }

        /// <summary>
        /// 华夏银行出金
        /// </summary>
        /// <param name="FdInfo">资金信息</param>
        /// <param name="money">出金金额</param>
        /// <param name="codeDesc">出金结果代码描述</param>
        /// <returns>成功返回true 失败返回fasle</returns>
        public static bool ChuJinHuaxiaBank(Fundinfo FdInfo, double money, ref string codeDesc)
        {
            bool IsSuc = false;
            IntersServerImplClient ic = new IntersServerImplClient();
            try
            {
                StringBuilder inXml = new StringBuilder();
                inXml.Append("<HXBB2B>");
                inXml.Append("<MessageData>");

                inXml.Append("<Base>");
                inXml.Append("<Version>1.0</Version>");
                inXml.Append("<SignFlag>0</SignFlag>");
                inXml.Append("<Language>GB2312</Language>");
                inXml.Append("</Base>");

                inXml.Append("<ReqHeader>");
                inXml.AppendFormat("<ClientTime>{0}</ClientTime>", DateTime.Now.ToString("yyyyMMddHHmmss"));
                inXml.Append("<MerchantNo>600014</MerchantNo>");
                inXml.Append("</ReqHeader>");

                inXml.Append("<DataBody>");
                inXml.AppendFormat("<MerTxSerNo>{0}</MerTxSerNo>", FdInfo.CashUser);
                inXml.Append("<TrnxCode>DZ017</TrnxCode>");
                inXml.AppendFormat("<AccountNo>{0}</AccountNo>", FdInfo.SubUser);
                inXml.AppendFormat("<MerAccountNo>{0}</MerAccountNo>", GetTanUser(FdInfo.TanUser));
                inXml.AppendFormat("<Amt>{0}</Amt>", money);
                inXml.Append("</DataBody>");

                inXml.Append("</MessageData>");
                inXml.Append("</HXBB2B>");
                com.individual.helper.LogNet4.WriteMsg("华夏银行出金,请求的Xml报文:" + inXml.ToString());
                string outXml = ic.Process(inXml.ToString());
                com.individual.helper.LogNet4.WriteMsg("华夏银行出金,响应的Xml报文:" + outXml);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(outXml);
                if (HuaxiaSuc == xmldoc.SelectSingleNode("HXBB2B/MessageData/ResHeader/Status/Code").InnerText)
                {
                    IsSuc = true;
                }
                codeDesc = xmldoc.SelectSingleNode("HXBB2B/MessageData/ResHeader/Status/Message").InnerText;
                ic.Close();
            }
            catch (Exception ex)
            {
                ic.Abort();
                throw new Exception(ex.Message, ex);
            }

            return IsSuc;
        }

        /// <summary>
        /// 签约华夏银行,开户华夏银行,入金
        /// </summary>
        /// <param name="FdInfo">资金信息</param>
        /// <param name="money">入金金额</param>
        /// <param name="PasswordChar">支付密码</param>
        /// <param name="codeDesc">入金结果代码描述</param>
        /// <returns>成功返回true 失败返回fasle</returns>
        public static bool RuJinHuaxiaBank(Fundinfo FdInfo, double money, string PasswordChar, ref string codeDesc)
        {
            bool IsSuc = false;
            IntersServerImplClient ic = new IntersServerImplClient();
            try
            {
                StringBuilder inXml = new StringBuilder();
                inXml.Append("<HXBB2B>");
                inXml.Append("<MessageData>");

                inXml.Append("<Base>");
                inXml.Append("<Version>1.0</Version>");
                inXml.Append("<SignFlag>0</SignFlag>");
                inXml.Append("<Language>GB2312</Language>");
                inXml.Append("</Base>");

                inXml.Append("<ReqHeader>");
                inXml.AppendFormat("<ClientTime>{0}</ClientTime>", DateTime.Now.ToString("yyyyMMddHHmmss"));
                inXml.Append("<MerchantNo>600014</MerchantNo>");
                inXml.Append("</ReqHeader>");

                inXml.Append("<DataBody>");
                inXml.AppendFormat("<MerTxSerNo>{0}</MerTxSerNo>", FdInfo.CashUser);
                inXml.Append("<TrnxCode>DZ021</TrnxCode>");
                inXml.AppendFormat("<AccountNo>{0}</AccountNo>", FdInfo.SubUser);
                inXml.AppendFormat("<MerAccountNo>{0}</MerAccountNo>", GetTanUser(FdInfo.TanUser));
                inXml.AppendFormat("<Amt>{0}</Amt>", money);
                inXml.AppendFormat("<PasswordChar>{0}</PasswordChar>", PasswordChar);//密码
                inXml.Append("</DataBody>");

                inXml.Append("</MessageData>");
                inXml.Append("</HXBB2B>");
                com.individual.helper.LogNet4.WriteMsg("华夏银行入金,请求的Xml报文:" + inXml.ToString());
                string outXml = ic.Process(inXml.ToString());
                com.individual.helper.LogNet4.WriteMsg("华夏银行入金,响应的Xml报文:" + outXml);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(outXml);
                if (HuaxiaSuc == xmldoc.SelectSingleNode("HXBB2B/MessageData/ResHeader/Status/Code").InnerText)
                {
                    IsSuc = true;
                }
                codeDesc = xmldoc.SelectSingleNode("HXBB2B/MessageData/ResHeader/Status/Message").InnerText;
                ic.Close();
            }
            catch (Exception ex)
            {
                ic.Abort();
                throw new Exception(ex.Message, ex);
            }

            return IsSuc;
        }

        /// <summary>
        /// 签约华夏银行,开户他行,入金登记
        /// </summary>
        /// <param name="remit">汇款信息</param>
        /// <param name="FdInfo">资金信息</param>
        /// <param name="codeDesc">入金登记结果代码描述</param>
        /// <returns>成功返回true 失败返回fasle</returns>
        public static bool RuJinDengJiHuaxiaBank(RemitInfo remit, Fundinfo FdInfo, ref string codeDesc)
        {
            bool IsSuc = false;
            IntersServerImplClient ic = new IntersServerImplClient();
            try
            {
                string MerAccountNo = GetTanUser(FdInfo.TanUser);
                StringBuilder inXml = new StringBuilder();
                inXml.Append("<HXBB2B>");
                inXml.Append("<MessageData>");

                inXml.Append("<Base>");
                inXml.Append("<Version>1.0</Version>");
                inXml.Append("<SignFlag>0</SignFlag>");
                inXml.Append("<Language>GB2312</Language>");
                inXml.Append("</Base>");

                inXml.Append("<ReqHeader>");
                inXml.AppendFormat("<ClientTime>{0}</ClientTime>", DateTime.Now.ToString("yyyyMMddHHmmss"));
                inXml.Append("<MerchantNo>600014</MerchantNo>");
                inXml.Append("</ReqHeader>");

                inXml.Append("<DataBody>");
                inXml.AppendFormat("<MerTxSerNo>{0}</MerTxSerNo>", FdInfo.CashUser);
                inXml.Append("<TrnxCode>DZ022</TrnxCode>");
                inXml.AppendFormat("<AccountNo>{0}</AccountNo>", FdInfo.SubUser);
                inXml.AppendFormat("<MerAccountNo>{0}</MerAccountNo>", MerAccountNo);
                inXml.AppendFormat("<Amt>{0}</Amt>", remit.Money);
                inXml.AppendFormat("<InOutStart>{0}</InOutStart>", remit.RemitType);
                inXml.AppendFormat("<PersonName>{0}</PersonName>", remit.RemitName);
                inXml.AppendFormat("<AmoutDate>{0}</AmoutDate>", remit.RemitTime);
                inXml.AppendFormat("<BankName>{0}</BankName>", remit.RemitBank);
                inXml.AppendFormat("<OutAccount>{0}</OutAccount>", remit.RemitAccount);
                inXml.Append("</DataBody>");

                inXml.Append("</MessageData>");
                inXml.Append("</HXBB2B>");
                com.individual.helper.LogNet4.WriteMsg("华夏银行入金登记,请求的Xml报文:" + inXml.ToString());
                string outXml = ic.Process(inXml.ToString());
                com.individual.helper.LogNet4.WriteMsg("华夏银行入金登记,响应的Xml报文:" + outXml);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(outXml);
                if (HuaxiaSuc == xmldoc.SelectSingleNode("HXBB2B/MessageData/ResHeader/Status/Code").InnerText)
                {
                    IsSuc = true;
                }
                codeDesc = xmldoc.SelectSingleNode("HXBB2B/MessageData/ResHeader/Status/Message").InnerText;
                ic.Close();
            }
            catch (Exception ex)
            {
                ic.Abort();
                throw new Exception(ex.Message, ex);
            }

            return IsSuc;
        }

        /// <summary>
        /// 判断用户是否存在有效订单和挂单
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>存在返回true不存在返回false</returns>
        public static bool UserExistOrderAndHoldOrder(string userid)
        {
            bool exist = true; //默认存在
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            try
            {
                sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();

                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandText = string.Format("select userId from Trade_Order where userid='{0}'", userid);
                sqlcmd.CommandType = CommandType.Text;

                sqldr = sqlcmd.ExecuteReader();
                if (sqldr.Read())
                {
                    exist = true;
                }
                else
                {
                    exist = false;
                }
                if (!exist)
                {
                    sqlcmd.CommandText = string.Format("select userId  from Trade_HoldOrder where userid='{0}'", userid);
                    sqldr.Close();
                    sqldr = sqlcmd.ExecuteReader();
                    if (sqldr.Read())
                    {
                        exist = true;
                    }
                    else
                    {
                        exist = false;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (null != sqlconn)
                {
                    sqlconn.Close();
                }
                if (null != sqldr)
                {
                    sqldr.Close();
                }
            }
            return exist;
        }

        /// <summary>
        /// 如果摊位号长度小于5位,补齐5位,左边补0
        /// </summary>
        /// <param name="tanUser">摊位号</param>
        /// <returns>返回长度大于或等于5的摊位号</returns>
        public static string GetTanUser(string tanUser)
        {
            string reTanUser = tanUser;
            try
            {
                switch (tanUser.Length)
                {
                    case 1:
                        reTanUser = string.Format("0000{0}", tanUser);
                        break;
                    case 2:
                        reTanUser = string.Format("000{0}", tanUser);
                        break;
                    case 3:
                        reTanUser = string.Format("00{0}", tanUser);
                        break;
                    case 4:
                        reTanUser = string.Format("0{0}", tanUser);
                        break;
                    default:
                        reTanUser = tanUser;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return reTanUser;
        }

        /// <summary>
        /// 写异常日志
        /// </summary>
        /// <param name="ex">异常</param>
        public static void WriteErr(Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Exception");
            log.Error(ex.Message, ex);
        }

        /// <summary>
        /// 执行交割单存储过程
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="paras">SQL参数列表</param>
        /// <returns>成功返回true失败返回false</returns>
        public static bool ExecProcedure(string procName, SqlParameter[] paras)
        {
            bool IsSuc = false;
            try
            {
                SqlConnection sqlconn = new SqlConnection(SqlConnectionString);
                sqlconn.Open();
                SqlCommand sqlcmd = sqlconn.CreateCommand();
                sqlcmd.Parameters.AddRange(paras);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = procName;
                sqlcmd.ExecuteNonQuery();
                int returnv = Convert.ToInt32(sqlcmd.Parameters["@Result"].Value);
                if (0 == returnv)
                {
                    IsSuc = true;
                }
            }
            catch (Exception ex)
            {
                WriteErr(ex);
            }
            return IsSuc;
        }

        /// <summary>
        /// 获取组织及子级组织，每个组织用单引号括起来，并用逗号分隔所有组织
        /// </summary>
        /// <param name="orgId">组织</param>
        /// <returns>逗号分隔的所有组织</returns>
        public static string GetOrgIds(string orgId)
        {
            string orgids = string.Empty;

            System.Data.Common.DbDataReader dbreader = null;
            System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                "@OutPut", DbParameterType.String, orgids, ParameterDirection.Output);
            try
            {
                dbreader = DbHelper.RunProcedureGetDataReader("Proc_GetOrgID", new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ParentOrgID",DbParameterType.String,orgId,ParameterDirection.Input),
                            OutputParam});
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
                    orgids =Convert.ToString(OutputParam.Value); //在close之后获取输出参数的值
                }
            }
            return orgids;
        }

        /// <summary>
        /// 用户帐号 或 证件号码是否被使用
        /// </summary>
        /// <param name="TradeAccount">用户帐号</param>
        /// <param name="cardNum">证件号码</param>
        /// <param name="TradeAccountExist">账号是否存在</param>
        /// <returns>证件号码存在返回true,不存在返回false</returns>
        public static bool CardNumIsExist(string TradeAccount, string cardNum, ref bool TradeAccountExist)
        {

            System.Data.Common.DbDataReader dbreader = null;
            bool IsExist = false;
            TradeAccountExist = false;
            try
            {

                string sql = string.Format("select CardNum from base_user where [status]='1' and CardNum='{0}'", cardNum);//判断启用状态中的用户是否有使用这个证件号码
                dbreader = DbHelper.ExecuteReader(sql);
                if (dbreader.Read())
                {
                    IsExist = true;
                }

                dbreader.Close();
                sql = string.Format("select Account from base_user where Account='{0}'", TradeAccount);//判断账号是否存在
                dbreader = DbHelper.ExecuteReader(sql);
                if (dbreader.Read())
                {
                    TradeAccountExist = true;
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
            return IsExist;
        }

        /// <summary>
        /// 判断用户输入的证件号码在其他有效用户中是否已经被使用
        /// </summary>
        /// <param name="TradeAccount">用户帐号</param>
        /// <param name="cardNum">证件号码</param>
        /// <returns>证件号码存在返回true,不存在返回false</returns>
        public static bool CardNumIsExist(string TradeAccount, string cardNum)
        {
            System.Data.Common.DbDataReader dbreader = null;
            bool IsExist = false;
            try
            {
                string sql = string.Format("select CardNum from base_user where [status]='1' and Account<>'{0}' and CardNum='{1}'", TradeAccount, cardNum);
                dbreader = DbHelper.ExecuteReader(sql);
                if (dbreader.Read())
                {
                    IsExist = true;
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
            return IsExist;
        }

        /// <summary>
        /// 判断帐号是否存在
        /// </summary>
        /// <param name="TradeAccount">交易账号</param>
        /// <returns></returns>
        public static bool TradeAccountExist(string TradeAccount)
        {
            System.Data.Common.DbDataReader dbreader = null;
            bool IsExist = false;
            try
            {

                string sql = string.Format("select Account from base_user where Account='{0}'", TradeAccount);
                dbreader = DbHelper.ExecuteReader(sql);
                if (dbreader.Read())
                {
                    IsExist = true;
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
            return IsExist;

        }

        /// <summary>
        /// 获取水和汇率
        /// </summary>
        /// <param name="PriceCode">行情编码</param>
        /// <param name="water">水</param>
        /// <param name="rate">汇率</param>
        /// <param name="coefxs">系数</param>
        /// <param name="coefficient">调整系数</param>
        /// <returns></returns>
        public static bool GetWaterAndRate(string PriceCode, ref double water, ref double rate,ref double coefxs,ref double coefficient)
        {
            bool IsSuc = false;


            System.Data.Common.DbDataReader dbreader = null;

            try
            {

                string sql = string.Format("select pricecode,rate,water,coefxs,coefficient from Trade_DataSource where pricecode='{0}'", PriceCode);

                dbreader = DbHelper.ExecuteReader(sql);
                if (dbreader.Read())
                {
                    coefxs = Convert.ToDouble(dbreader["coefxs"]);
                    water = Convert.ToDouble(dbreader["water"]);
                    rate = Convert.ToDouble(dbreader["rate"]);
                    coefficient = Convert.ToDouble(dbreader["coefficient"]);
                    IsSuc = true;
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
            return IsSuc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TdRate"></param>
        /// <returns></returns>
        public static int ModifyWaterAndRate(WcfInterface.model.TradeRate TdRate)
        {
            TradeClient tc = new TradeClient();
            int result = 0;
            try
            {
                result = tc.SetRateAndWater(TdRate.PriceCode, TdRate.Rate, TdRate.Water);
                tc.Close();
            }
            catch (Exception ex)
            {
                tc.Abort();
                throw new Exception(ex.Message, ex);
            }
            return result;
        }

        /// <summary>
        /// 获取修改时间
        /// </summary>
        /// <param name="weekflg"></param>
        /// <param name="pricecode"></param>
        /// <returns></returns>
        public static DateTime GetMdTime(string weekflg,string pricecode)
        {
            System.Data.Common.DbDataReader dbreader = null;
            DateTime mdtime = new DateTime(1990,1,1);
            try
            {

                string sql = string.Format("select mdtime from Data_Stamp where pricecode='{0}' and weekflg='{1}'", pricecode, weekflg);
                dbreader = DbHelper.ExecuteReader(sql);
                if (dbreader.Read())
                {
                    mdtime = Convert.ToDateTime(dbreader["mdtime"]);
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
            return mdtime;
        }

        /// <summary>
        /// 获取商品时间和最新价格
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, ProPrice> GetProPrice()
        {
            Dictionary<string, ProPrice> prodic = new Dictionary<string, ProPrice>();
            TradeClient tc = new TradeClient();
            try
            {
                prodic = tc.GetProPrice();
                tc.Close();
            }
            catch (Exception ex)
            {
                tc.Abort();
                throw new Exception(ex.Message, ex);
            }
            return prodic;
        }

        /// <summary>
        /// 修改交易日
        /// </summary>
        /// <param name="DtSet"></param>
        public static void ModifyDateSet(WcfInterface.model.DateSet DtSet)
        {
            TradeClient tc = new TradeClient();
            try
            {
                WcfInterface.ServiceReference1.DateSet ds = new ServiceReference1.DateSet();
                ds.Desc = DtSet.Desc;
                ds.Endtime = DtSet.Endtime;
                ds.Starttime = DtSet.Starttime;
                ds.Istrade = DtSet.Istrade;
                ds.Weekday = DtSet.Weekday;
                tc.ModifyDateSet(ds);
                tc.Close();
            }
            catch (Exception)
            {
                tc.Abort();
            }
        }

        /// <summary>
        /// 修改交易日
        /// </summary>
        /// <param name="DtSet"></param>
        public static void ModifyDateSetEx(WcfInterface.model.DateSet DtSet)
        {
            TradeClient tc = new TradeClient();
            try
            {
                WcfInterface.ServiceReference1.DateSet ds = new ServiceReference1.DateSet();
                ds.Desc = DtSet.Desc;
                ds.Endtime = DtSet.Endtime;
                ds.Starttime = DtSet.Starttime;
                ds.Istrade = !DtSet.Istrade;
                ds.Weekday = DtSet.Weekday;
                ds.PriceCode = DtSet.PriceCode;
                tc.ModifyDateSet(ds);
                tc.Close();
            }
            catch (Exception)
            {
                tc.Abort();
            }
        }

        /// <summary>
        /// 修改节假日
        /// </summary>
        /// <param name="Hliday"></param>
        public static void ModifyHoliday(WcfInterface.model.DateHoliday Hliday)
        {
            TradeClient tc = new TradeClient();
            try
            {
                WcfInterface.ServiceReference1.DateHoliday dh = new ServiceReference1.DateHoliday();
                dh.Desc = Hliday.Desc;
                dh.Endtime = Hliday.Endtime;
                dh.Starttime = Hliday.Starttime;
                dh.ID = Hliday.ID;
                dh.HoliName = Hliday.HoliName;
                dh.PriceCode = Hliday.PriceCode;
                tc.ModifyHoliday(dh);
                tc.Close();
            }
            catch (Exception)
            {
                tc.Abort();
            }
        }

        /// <summary>
        /// 添加节假日
        /// </summary>
        /// <param name="Hliday"></param>
        public static void AddHoliday(WcfInterface.model.DateHoliday Hliday)
        {
            TradeClient tc = new TradeClient();
            try
            {
                WcfInterface.ServiceReference1.DateHoliday dh = new ServiceReference1.DateHoliday();
                dh.Desc = Hliday.Desc;
                dh.Endtime = Hliday.Endtime;
                dh.Starttime = Hliday.Starttime;
                dh.ID = Hliday.ID;
                dh.HoliName = Hliday.HoliName;
                dh.PriceCode = Hliday.PriceCode;
                tc.AddHoliday(dh);
                tc.Close();
            }
            catch (Exception)
            {
                tc.Abort();
            }
        }
        /// <summary>
        /// 删除节假日
        /// </summary>
        /// <param name="ID"></param>
        public static void DelHoliday(string ID)
        {
            TradeClient tc = new TradeClient();
            try
            {
                tc.DelHoliday(ID);
                tc.Close();
            }
            catch (Exception)
            {
                tc.Abort();
            }
        }

        /// <summary>
        /// 根据周期获取最后一根柱子的信息
        /// </summary>
        /// <param name="weekflg"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetAllLastPillar(string weekflg)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            TradeClient tc = new TradeClient();
            try
            {
                dic = tc.GetAllLastPillar(weekflg);
                tc.Close();
            }
            catch (Exception ex)
            {
                tc.Abort();
                throw new Exception(ex.Message, ex);
            }
            return dic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, double> GetYesterdayPrice()
        {
            System.Data.Common.DbDataReader dbreader = null;
            Dictionary<string, double> dic = new Dictionary<string, double>();
            try
            {

                dbreader = DbHelper2.ExecuteReader(@"select a.openprice,b.pricecode from data_d1 a,
                                                    (select pricecode,max(weektime) weektime from data_d1 group by pricecode) b
                                                    where a.pricecode=b.pricecode and a.weektime=b.weektime");
                string pricecode = string.Empty;
                double openprice = 0;
                while (dbreader.Read())
                {
                    openprice = System.DBNull.Value != dbreader["openprice"] ? Convert.ToDouble(dbreader["openprice"]) : 0;
                    pricecode = System.DBNull.Value != dbreader["pricecode"] ?dbreader["pricecode"].ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(pricecode))
                    {
                        dic[pricecode] = openprice;
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
            return dic;
        }

        /// <summary>
        /// 验证交易手数
        /// </summary>
        /// <param name="Quantity"></param>
        /// <param name="OrderUnit"></param>
        /// <returns></returns>
        public static bool ValidateQuantity(double Quantity, double OrderUnit)
        {
            if (OrderUnit <= dzero)
            {
                OrderUnit = 0.5;//默认为0.5
            }
            if (Quantity <= dzero)
            {
                return false;
            }
            if ((Quantity % OrderUnit) <= dzero)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证交易设置是否合法
        /// </summary>
        /// <param name="TdSet"></param>
        /// <returns></returns>
        public static bool ValidateTradeSet(TradeSet TdSet)
        {
            bool result = true;
            double _expressionfee = 0;
             
            switch (TdSet.ObjCode)
            {
                case "CCFJSSJ":
                    break;
                case "GDYXQ":
                    break;
                case "YKGS":

                    try
                    {   //([平仓价]-[建仓价])/[点差基值]*[点值]*[数量]-[工费]-[仓储费]
                        string expressionfee = TdSet.ObjValue;

                        string strtmp = expressionfee.Replace("[平仓价]", "1").Replace("[建仓价]", "0").Replace("[数量]", "1").Replace("[点差基值]", "1").Replace("[点值]", "1").Replace("[工费]", "0").Replace("[仓储费]", "0");
                        _expressionfee = Convert.ToDouble(Microsoft.JScript.Eval.JScriptEvaluate(strtmp, Microsoft.JScript.Vsa.VsaEngine.CreateEngine()));

                    }
                    catch (Exception ex)
                    {
                        result = false;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// 获取IP和MAC的组合字符串
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static string GetIpMac(string ip, string mac)
        {
            string ipmac = string.Empty;
            if (!string.IsNullOrEmpty(ip))
            {
                ipmac += string.Format("IP={0},", ip);
            }
            if (!string.IsNullOrEmpty(mac))
            {
                ipmac += string.Format("MAC={0},", mac);
            }
            return ipmac;
        }

        /// <summary>
        /// 根据业务类型获取将要生成的订单号,
        /// 订单号生成规则：业务类型+时间+当前业务ID+随机数(3位)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetOrderId(string type)
        {
            string orderid = string.Empty;
            System.Data.Common.DbDataReader dbreader = null;
            try
            {

                
                dbreader = DbHelper.RunProcedureGetDataReader("Proc_GetOrderId",
                    new System.Data.Common.DbParameter[]
                    {
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                            "@type", DbParameterType.String, type, ParameterDirection.Input)
                    });
                if (dbreader.Read())
                {
                    orderid = dbreader[0].ToString();
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (dbreader != null)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return orderid;
        }

        /// <summary>
        /// 通联支付 出金
        /// </summary>
        /// <param name="applyid"></param>
        /// <param name="ret_cod">处理结果代码</param>
        /// <param name="ret_msg">处理结果消息</param>
        /// <param name="ret_cod2">中间状态代码</param>
        /// <param name="ret_msg2">中间状态消息</param>
        public static void ProcessChuJin(int applyid, ref string ret_cod, ref string ret_msg,ref string ret_cod2, ref string ret_msg2)
        {
            string sql = string.Format("select applyid,orderid,amt,accountname,bankcard,inter_bankcode from V_Trade_Chujin_Online where applyid={0}", applyid);
            System.Data.Common.DbDataReader dbreader = DbHelper.ExecuteReader(sql);
            try
            {
                //获取订单金额 银行类型 银行卡号 持卡人

                double amt = 0;
                string accountName = string.Empty;
                string bankcard = string.Empty;
                string bankcode = string.Empty;
                string orderid = string.Empty;
                if (dbreader.Read())
                {
                    amt = Convert.ToDouble(dbreader["amt"]);
                    accountName = dbreader["accountname"].ToString();
                    bankcard = dbreader["bankcard"].ToString();
                    bankcode = dbreader["inter_bankcode"].ToString();
                    orderid = dbreader["orderid"].ToString();
                }
                string url = string.Format(@"{0}?accountName={1}&accountNO={2}&amount={3}&bankCode={4}&orderID={5}",
                    BankAddress, accountName, bankcard, amt, bankcode, orderid);
                WebClient wc = new WebClient();
                byte[] bResponse = wc.DownloadData(url);
                string strResponse = Encoding.UTF8.GetString(bResponse);
                LogNet4.WriteMsg(strResponse); //日志记录接口返回的信息
                XmlDocument rexml = new XmlDocument();
                rexml.LoadXml(strResponse);
                ret_cod2 = rexml.SelectSingleNode("AIPG/INFO/RET_CODE").InnerXml;
                ret_msg2 = rexml.SelectSingleNode("AIPG/INFO/ERR_MSG").InnerXml;
                if ("0000" == ret_cod2)
                {
                    ret_cod = rexml.SelectSingleNode("AIPG/TRANSRET/RET_CODE").InnerXml;
                    ret_msg = rexml.SelectSingleNode("AIPG/TRANSRET/ERR_MSG").InnerXml;
                }
            }
            catch (Exception)
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
        }


        /// <summary>
        /// 挂单处理
        /// </summary>
        /// <param name="HoldOrderID"></param>
        /// <param name="type">处理类型 0-挂单到期自动撤销 1-手动撤销 2-挂单转订单</param>
        /// <param name="tradefee">手续费 只有type=2时有用</param>
        /// <returns></returns>
        public static int DoHoldTradeOrder(string HoldOrderID, int type, double tradefee)
        {
            int Result = 99;//99表示失败,100成功 
            System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                 "@Result", DbParameterType.Int, Result, ParameterDirection.Output);
            try
            {
               DbHelper.RunProcedureExecuteSql("proc_DoHoldOrder", new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@HoldOrderID",DbParameterType.String,HoldOrderID,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@type",DbParameterType.Int,type,ParameterDirection.Input),
                       DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@tradefee",DbParameterType.Float,tradefee,ParameterDirection.Input),
                    OutputParam});

               Result = Convert.ToInt32(OutputParam.Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return Result;
        }

        /// <summary>
        /// 获取默认客户分组ID
        /// </summary>
        /// <returns></returns>
        public static string GetUserGroupId()
        {
            System.Data.Common.DbDataReader dbreader = null;
            string UserGroupId = string.Empty;
            try
            {
                dbreader = DbHelper.ExecuteReader("select UserGroupId from Trade_UserGroups where IsDefaultGroup=1");
                if (dbreader.Read())
                {
                    UserGroupId = System.DBNull.Value != dbreader["UserGroupId"] ? dbreader["UserGroupId"].ToString() : string.Empty;
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
            return UserGroupId;
        }

        /// <summary>
        /// 获取客户分组信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static UserGroups GetUserGroups(string UserId)
        {
            System.Data.Common.DbDataReader dbreader = null;
            UserGroups ugs = new UserGroups();
            ugs.UserGroupId = string.Empty;
            ugs.UserGroupName = string.Empty;
            ugs.IsDefaultGroup = 0;
            ugs.AfterSecond = 0;
            ugs.PlaceOrderSlipPoint = 0;
            ugs.FlatOrderSlipPoint = 0;
            ugs.DelayPlaceOrder = 0;
            ugs.DelayFlatOrder = 0;
            try
            {
                string sql =
                    string.Format(
                        @"select a.UserGroupId, a.UserGroupName,a.IsDefaultGroup, a.AfterSecond, a.PlaceOrderSlipPoint, 
                    a.FlatOrderSlipPoint, a.DelayPlaceOrder, a.DelayFlatOrder from Trade_UserGroups a,Trade_User_Group b 
                        where a.UserGroupId=b.UserGroupId and b.userid=@userid"); //使用参数化的sql语句，防止sql注入
                dbreader = DbHelper.ExecuteReader(sql,
                    new System.Data.Common.DbParameter[]
                    {
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                            "@userid", DbParameterType.String, UserId, ParameterDirection.Input)
                    });
                if (dbreader.Read())
                {
                    ugs.UserGroupId = dbreader["UserGroupId"].ToString();
                    ugs.UserGroupName = System.DBNull.Value != dbreader["UserGroupName"]
                        ? dbreader["UserGroupName"].ToString()
                        : string.Empty;
                    ugs.IsDefaultGroup = System.DBNull.Value != dbreader["IsDefaultGroup"]
                        ? Convert.ToInt32(dbreader["IsDefaultGroup"])
                        : 0;
                    ugs.AfterSecond = System.DBNull.Value != dbreader["AfterSecond"]
                        ? Convert.ToInt32(dbreader["AfterSecond"])
                        : 0;
                    ugs.PlaceOrderSlipPoint = System.DBNull.Value != dbreader["PlaceOrderSlipPoint"]
                        ? Convert.ToInt32(dbreader["PlaceOrderSlipPoint"])
                        : 0;
                    ugs.FlatOrderSlipPoint = System.DBNull.Value != dbreader["FlatOrderSlipPoint"]
                        ? Convert.ToInt32(dbreader["FlatOrderSlipPoint"])
                        : 0;
                    ugs.DelayPlaceOrder = System.DBNull.Value != dbreader["DelayPlaceOrder"]
                        ? Convert.ToDouble(dbreader["DelayPlaceOrder"])
                        : 0;
                    ugs.DelayFlatOrder = System.DBNull.Value != dbreader["DelayFlatOrder"]
                        ? Convert.ToDouble(dbreader["DelayFlatOrder"])
                        : 0;
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return ugs;
        }

        /// <summary>
        /// 在历史订单表中查询此用户此行情在最近xxx秒内是否存在历史订单，如果存在，则不允许下单；否则允许下单
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PriceCode"></param>
        /// <param name="AfterSecond"></param>
        /// <returns></returns>
        public static bool JudgeExistLTradeOrder(string UserId, string PriceCode, int AfterSecond)
        {
            System.Data.Common.DbDataReader dbreader = null;
            bool ExistOrder = false;
            try
            {
                string sql = string.Format(@"select orderid from v_l_trade_order
                        where userid=@UserId and pricecode=@PriceCode and abs(datediff(s,overtime,getdate()))<@AfterSecond");
                    //使用参数化的sql语句，防止sql注入
                dbreader = DbHelper.ExecuteReader(sql,
                    new System.Data.Common.DbParameter[]
                    {
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@UserId", DbParameterType.String,
                            UserId, ParameterDirection.Input),
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@PriceCode", DbParameterType.String,
                            PriceCode, ParameterDirection.Input),
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@AfterSecond", DbParameterType.Int,
                            AfterSecond, ParameterDirection.Input)
                    });
                if (dbreader.Read())
                {
                    ExistOrder = true;
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return ExistOrder;
        }

        /// <summary>
        /// 判断用户是否已经添加到指定组
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserGroupId"></param>
        /// <returns></returns>
        public static bool ExistUserInTheGroup(string UserId, string UserGroupId)
        {
            System.Data.Common.DbDataReader dbreader = null;
            bool ExistUser = false;
            try
            {
                string sql = string.Format(@"select userid from Trade_User_Group
                        where userid=@UserId and UserGroupId=@UserGroupId "); //使用参数化的sql语句，防止sql注入
                dbreader = DbHelper.ExecuteReader(sql,
                    new System.Data.Common.DbParameter[]
                    {
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@UserId", DbParameterType.String,
                            UserId, ParameterDirection.Input),
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type, "@UserGroupId",
                            DbParameterType.String, UserGroupId, ParameterDirection.Input)
                    });
                if (dbreader.Read())
                {
                    ExistUser = true;
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return ExistUser;
        }

        /// <summary>
        /// 根据用户ID获取用户的盈亏
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static double GetUserYingKui(string userid)
        {
            System.Data.Common.DbDataReader dbreader = null;
            double yingkui = 0;
            TradeClient tc = new TradeClient();
            try
            {
                string data = string.Empty;
                Dictionary<string, ProPrice> prodic = tc.GetProPrice();
                foreach (var item in prodic)
                {
                    data += string.Format("{0},{1}|", item.Key, item.Value.realprice);
                }
                if (data.Length > 1)
                {
                    data = data.Substring(0, data.Length - 1);
                }
                else
                {
                    return 0;
                }
                //@data 参数格式[行情编码,价格|行情编码,价格|行情编码,价格|行情编码,价格|行情编码,价格]
                //存储过程proc_GetUserYingKui计算一个用户所有订单的盈亏之和
                dbreader = DbHelper.RunProcedureGetDataReader("proc_GetUserYingKui",
                    new System.Data.Common.DbParameter[]
                    {
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                            "@userid", DbParameterType.String, userid, ParameterDirection.Input),
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                            "@data", DbParameterType.String, data, ParameterDirection.Input)
                    });

                if (dbreader.Read())
                {
                    yingkui = System.DBNull.Value != dbreader["yingkui"] ? Convert.ToDouble(dbreader["yingkui"]) : 0;
                }
                tc.Close();
            }
            catch (Exception ex)
            {
                tc.Abort();
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (dbreader != null)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return yingkui;

        }

    }
}