//*******************************************************************************
//  文 件 名：CTrade.cs
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
using System.Data.SqlClient;
using System.Data;
using System.ServiceModel;
using WcfInterface.model;
using WcfInterface;
using com.individual.helper;
using System.Text;
using WcfInterface.ServiceReference1;

namespace Trade
{
    /// <summary>
    /// 交易类
    /// </summary>
    public partial class CTrade : ITrade
    {

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="tradeAccount">登陆账号</param>
        /// <param name="tradePwd">登陆密码</param>
        /// <param name="mac">mac地址</param>
        /// <returns>Loginfo</returns>
        public Loginfo GetLogin(string tradeAccount, string tradePwd, string mac)
        {
            //实例化实体类
            Loginfo loginfo = new Loginfo();
            loginfo.LoginID = "-1";
            try
            {
                tradePwd = Des3.Des3EncodeCBC(tradePwd);
                //判断用户是否存在 函数1 
                if (ComFunction.ListLogin(ref loginfo, tradeAccount, tradePwd, mac))
                {
                    loginfo.LoginID = System.Guid.NewGuid().ToString().Replace("-", "");
   
                    //如果存在 随机生存登陆标识 并更新到数据库 查询行情IP和PORT 返回实体类
                    ComFunction.ListLoginfo(ref loginfo, tradeAccount, tradePwd);
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                loginfo.LoginID = "-1";
            }
            return loginfo;
        }

        /// <summary>
        /// 行情客户端用户登陆
        /// </summary>
        /// <param name="tradeAccount">登陆账号</param>
        /// <param name="tradePwd">登陆密码</param>
        /// <param name="mac">mac地址</param>
        /// <returns>Loginfo</returns>
        public Loginfo GetLoginEx(string tradeAccount, string tradePwd, string mac)
        {
            //实例化实体类
            Loginfo loginfo = new Loginfo();
            loginfo.LoginID = "-1";
            try
            {
                tradePwd = Des3.Des3EncodeCBC(tradePwd);
                //判断用户是否存在 函数1 
                if (ComFunction.ListLogin(ref loginfo, tradeAccount, tradePwd, mac))
                {
                    loginfo.LoginID = System.Guid.NewGuid().ToString().Replace("-", "");
                    loginfo.QuotesAddressIP = ComFunction.ip;
                    loginfo.QuotesPort = Convert.ToInt32(ComFunction.port);
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                loginfo.LoginID = "-1";
            }
            return loginfo;
        }

        /// <summary>
        /// 商品配置
        /// </summary>
        /// <param name="LoginID">用户登陆标识</param>
        /// <returns>商品列表</returns>
        public List<ProductConfig> GetProductConfig(string LoginID)
        {
            List<ProductConfig> list = new List<ProductConfig>();

            System.Data.Common.DbDataReader dbreader = null;
            try
            {

                if (!ComFunction.ExistUserLoginID(LoginID))
                {
                    return list;
                }

                Dictionary<string, ProPrice> prodic = ComFunction.GetProPrice();

                dbreader = DbHelper.ExecuteReader(ComFunction.GetProductSql);
                //string[] splits = null;
                while (dbreader.Read())
                {
                    ProductConfig ptc = new ProductConfig();
                    ptc.ProductCode = System.DBNull.Value != dbreader["productcode"] ? dbreader["productcode"].ToString() : string.Empty;
                    ptc.ProductName = System.DBNull.Value != dbreader["ProductName"] ? dbreader["ProductName"].ToString() : string.Empty;
                    ptc.GoodsCode = System.DBNull.Value != dbreader["goodscode"] ? dbreader["goodscode"].ToString() : string.Empty;
                    ptc.AdjustBase = System.DBNull.Value != dbreader["adjustbase"] ? Convert.ToDouble(dbreader["adjustbase"]) : 0;
                    ptc.AdjustCount = System.DBNull.Value != dbreader["adjustcount"] ? Convert.ToInt32(dbreader["adjustcount"]) : 0;
                    ptc.PriceDot = System.DBNull.Value != dbreader["pricedot"] ? Convert.ToInt32(dbreader["pricedot"]) : 0;
                    ptc.ValueDot = System.DBNull.Value != dbreader["valuedot"] ? Convert.ToDouble(dbreader["valuedot"]) : 0;
                    ptc.SetBase = System.DBNull.Value != dbreader["setBase"] ? Convert.ToInt32(dbreader["setBase"]) : 0;
                    ptc.HoldBase = System.DBNull.Value != dbreader["holdbase"] ? Convert.ToInt32(dbreader["holdbase"]) : 0;
                    ptc.OrderMoney = System.DBNull.Value != dbreader["Ordemoney"] ? Convert.ToDouble(dbreader["Ordemoney"]) : 1;
                    ptc.MaxPrice = System.DBNull.Value != dbreader["maxprice"] ? Convert.ToDouble(dbreader["maxprice"]) : 8000;
                    ptc.MinPrice = System.DBNull.Value != dbreader["minprice"] ? Convert.ToDouble(dbreader["minprice"]) : 1;
                    ptc.MaxTime = System.DBNull.Value != dbreader["maxtime"] ? Convert.ToDouble(dbreader["maxtime"]) : 60;
                    ptc.State = System.DBNull.Value != dbreader["state"] ? dbreader["state"].ToString() : string.Empty;
                    ptc.Unit = System.DBNull.Value != dbreader["unit"] ? Convert.ToDouble(dbreader["unit"]) : 1;
                    ptc.PriceCode = System.DBNull.Value != dbreader["pricecode"] ? dbreader["pricecode"].ToString() : string.Empty;

                    if (prodic != null && prodic.ContainsKey(ptc.PriceCode))
                    {
                        ptc.weektime = prodic[ptc.PriceCode].weektime;
                        ptc.realprice = prodic[ptc.PriceCode].realprice;
                    }
                    list.Add(ptc);
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (null != list && list.Count > 0)
                {
                    list.Clear();
                }
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
        /// 商品配置
        /// </summary>
        /// <param name="LoginID">用户登陆标识</param>
        /// <returns>商品列表</returns>
        public List<ProductConfig> GetProductConfigEx(string LoginID)
        {
            List<ProductConfig> list = new List<ProductConfig>();

            System.Data.Common.DbDataReader dbreader = null;
            try
            {

                //Dictionary<string, ProPrice> prodic = ComFunction.GetProPrice();
                Dictionary<string, string> dic = ComFunction.GetAllLastPillar("D1");
                Dictionary<string, double> opendic = ComFunction.GetYesterdayPrice();

                dbreader = DbHelper.ExecuteReader(ComFunction.GetProductSqlEx);
                string[] splits = null;
                while (dbreader.Read())
                {
                    ProductConfig ptc = new ProductConfig();
                    ptc.ProductCode = System.DBNull.Value != dbreader["productcode"] ? dbreader["productcode"].ToString() : string.Empty;
                    ptc.ProductName = System.DBNull.Value != dbreader["ProductName"] ? dbreader["ProductName"].ToString() : string.Empty;
                    ptc.GoodsCode = System.DBNull.Value != dbreader["goodscode"] ? dbreader["goodscode"].ToString() : string.Empty;
                    ptc.AdjustBase = System.DBNull.Value != dbreader["adjustbase"] ? Convert.ToDouble(dbreader["adjustbase"]) : 0;
                    ptc.AdjustCount = System.DBNull.Value != dbreader["adjustcount"] ? Convert.ToInt32(dbreader["adjustcount"]) : 0;
                    ptc.PriceDot = System.DBNull.Value != dbreader["pricedot"] ? Convert.ToInt32(dbreader["pricedot"]) : 0;
                    ptc.ValueDot = System.DBNull.Value != dbreader["valuedot"] ? Convert.ToDouble(dbreader["valuedot"]) : 0;
                    ptc.SetBase = System.DBNull.Value != dbreader["setBase"] ? Convert.ToInt32(dbreader["setBase"]) : 0;
                    ptc.HoldBase = System.DBNull.Value != dbreader["holdbase"] ? Convert.ToInt32(dbreader["holdbase"]) : 0;
                    ptc.OrderMoney = System.DBNull.Value != dbreader["Ordemoney"] ? Convert.ToDouble(dbreader["Ordemoney"]) : 1;
                    ptc.MaxPrice = System.DBNull.Value != dbreader["maxprice"] ? Convert.ToDouble(dbreader["maxprice"]) : 8000;
                    ptc.MinPrice = System.DBNull.Value != dbreader["minprice"] ? Convert.ToDouble(dbreader["minprice"]) : 1;
                    ptc.MaxTime = System.DBNull.Value != dbreader["maxtime"] ? Convert.ToDouble(dbreader["maxtime"]) : 60;
                    ptc.State = System.DBNull.Value != dbreader["state"] ? dbreader["state"].ToString() : string.Empty;
                    ptc.Unit = System.DBNull.Value != dbreader["unit"] ? Convert.ToDouble(dbreader["unit"]) : 1;
                    ptc.PriceCode = System.DBNull.Value != dbreader["pricecode"] ? dbreader["pricecode"].ToString() : string.Empty;

                    //if (prodic != null && prodic.ContainsKey(ptc.PriceCode))
                    //{
                    //    ptc.weektime = prodic[ptc.PriceCode].weektime;
                    //    ptc.realprice = prodic[ptc.PriceCode].realprice;
                    //}
                    if (dic != null && dic.ContainsKey(ptc.PriceCode))
                    {
                        splits = dic[ptc.PriceCode].Split('\t');//周期 编码 时间 开盘 最高 最低 收盘 成交量
                        ptc.weektime = splits[2] + "00";
                        ptc.OpenPrice = Convert.ToDouble(splits[3]);
                        ptc.HighPrice = Convert.ToDouble(splits[4]);
                        ptc.LowPrice = Convert.ToDouble(splits[5]);
                        ptc.realprice = Convert.ToDouble(splits[6]);
                    }
                    if (opendic != null && opendic.ContainsKey(ptc.PriceCode))
                    {
                        ptc.YesterdayPrice = opendic[ptc.PriceCode];
                    }
                    list.Add(ptc);
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (null != list && list.Count > 0)
                {
                    list.Clear();
                }
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
        /// 资金库存信息查询
        /// </summary>
        /// <param name="LoginID">登陆标识</param>
        /// <returns>资金库存信息</returns>
        public MoneyInventory GetMoneyInventory(string LoginID)
        {
            MoneyInventory moneyInventory = new MoneyInventory();
            moneyInventory.StorageQuantity = new Storagequantity();
            moneyInventory.FdInfo = new Fundinfo();

            System.Data.Common.DbDataReader dbreader = null;
            //bool IsGetMoney = false;
            try
            {
                string userId = string.Empty;
                moneyInventory.Result = false;
                //查看 logid在 数据库 存在否
                if (!ComFunction.ExistUserLoginID(LoginID, ref userId))
                {
                    moneyInventory.Result = false;
                    moneyInventory.ReturnCode = ResCode.UL003;
                    moneyInventory.Desc = ResCode.UL003Desc;
                    return moneyInventory;
                }
                string sql ="select DongJieMoney, money,frozenMoney,OccMoney,state,CashUser,SubUser,TanUser,ConBankType,OpenBank,BankAccount,AccountName,BankCard from Trade_FundInfo where state<>'4' and userId=@userId";

                dbreader = DbHelper.ExecuteReader(sql,
                    new System.Data.Common.DbParameter[]{DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@userId",DbParameterType.String,userId,ParameterDirection.Input)});
                while (dbreader.Read())
                {
                    moneyInventory.FdInfo.DongJieMoney = System.DBNull.Value != dbreader["DongJieMoney"] ? Convert.ToDouble(dbreader["DongJieMoney"]) : 0;
                    moneyInventory.FdInfo.Money = System.DBNull.Value != dbreader["money"] ? Convert.ToDouble(dbreader["money"]) : 0;
                    moneyInventory.FdInfo.FrozenMoney = System.DBNull.Value != dbreader["frozenMoney"] ? Convert.ToDouble(dbreader["frozenMoney"]) : 0;
                    moneyInventory.FdInfo.OccMoney = System.DBNull.Value != dbreader["OccMoney"] ? Convert.ToDouble(dbreader["OccMoney"]) : 0;
                    moneyInventory.FdInfo.State = System.DBNull.Value != dbreader["state"] ? dbreader["state"].ToString() : string.Empty;
                    moneyInventory.FdInfo.CashUser = System.DBNull.Value != dbreader["CashUser"] ? dbreader["CashUser"].ToString() : string.Empty;
                    moneyInventory.FdInfo.SubUser = System.DBNull.Value != dbreader["SubUser"] ? dbreader["SubUser"].ToString() : string.Empty;
                    moneyInventory.FdInfo.TanUser = System.DBNull.Value != dbreader["TanUser"] ? dbreader["TanUser"].ToString() : string.Empty;
                    moneyInventory.FdInfo.ConBankType = System.DBNull.Value != dbreader["ConBankType"] ? dbreader["ConBankType"].ToString() : string.Empty;
                    moneyInventory.FdInfo.OpenBank = System.DBNull.Value != dbreader["OpenBank"] ? dbreader["OpenBank"].ToString() : string.Empty;
                    moneyInventory.FdInfo.BankAccount = System.DBNull.Value != dbreader["BankAccount"] ? dbreader["BankAccount"].ToString() : string.Empty;
                    moneyInventory.FdInfo.AccountName = System.DBNull.Value != dbreader["AccountName"] ? dbreader["AccountName"].ToString() : string.Empty;
                    moneyInventory.FdInfo.BankCard = System.DBNull.Value != dbreader["BankCard"] ? dbreader["BankCard"].ToString() : string.Empty;
                    //IsGetMoney = true;
                    break;
                }
                //if (!IsGetMoney)
                //{
                //    return moneyInventory;
                //}
                //查询 库存 
                dbreader.Close();
                //sqlcmd.CommandText = "select au,ag,pt,pd from Stock_BZJ where userId=@userId ";
                //sqldr = sqlcmd.ExecuteReader();
                //if (sqldr.Read())
                //{
                //    moneyInventory.StorageQuantity.xau = System.DBNull.Value != sqldr["au"] ? Convert.ToDouble(sqldr["au"]) : 0;

                //    moneyInventory.StorageQuantity.xag = System.DBNull.Value != sqldr["ag"] ? Convert.ToDouble(sqldr["ag"]) : 0;

                //    moneyInventory.StorageQuantity.xpt = System.DBNull.Value != sqldr["pt"] ? Convert.ToDouble(sqldr["pt"]) : 0;

                //    moneyInventory.StorageQuantity.xpd = System.DBNull.Value != sqldr["pd"] ? Convert.ToDouble(sqldr["pd"]) : 0;
                //}
                moneyInventory.Result = true;
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                moneyInventory.Result = false;
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return moneyInventory;
        }


        #region 订单信息

        /// <summary>
        /// 资金库存信息查询
        /// </summary>
        /// <param name="account">要被查询的账号</param>
        /// <param name="LoginID">登陆标识</param>
        /// <returns>资金库存信息</returns>
        public MoneyInventory GetMoneyInventoryEx(string account, string LoginID)
        {
            MoneyInventory moneyInventory = new MoneyInventory();
            moneyInventory.StorageQuantity = new Storagequantity();
            moneyInventory.FdInfo = new Fundinfo();

            System.Data.Common.DbDataReader dbreader = null;
            //bool IsGetMoney = false;
            try
            {
                string userId = string.Empty;
                moneyInventory.Result = false;
                //查看 logid在 数据库 存在否
                //if (!ComFunction.ExistUserLoginID(LoginID))
                //{
                //    moneyInventory.Result = false;
                //    moneyInventory.ReturnCode = ResCode.UL003;
                //    moneyInventory.Desc = ResCode.UL003Desc;
                //    return moneyInventory;
                //}
                userId = ComFunction.GetUserId(account);
                if (string.IsNullOrEmpty(userId))//如果用户不存在
                {
                    moneyInventory.Result = false;
                    moneyInventory.ReturnCode = ResCode.UL005;
                    moneyInventory.Desc = ResCode.UL005Desc;
                    return moneyInventory;
                }
                string sql = "select DongJieMoney,money,frozenMoney,OccMoney,state,CashUser,SubUser,TanUser,ConBankType,OpenBank,BankAccount,AccountName,BankCard from Trade_FundInfo where state<>'4' and userId=@userId";

                dbreader = DbHelper.ExecuteReader(sql,
                    new System.Data.Common.DbParameter[]{DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@userId",DbParameterType.String,userId,ParameterDirection.Input)});
                while (dbreader.Read())
                {
                    moneyInventory.FdInfo.DongJieMoney = System.DBNull.Value != dbreader["DongJieMoney"] ? Convert.ToDouble(dbreader["DongJieMoney"]) : 0;
                    moneyInventory.FdInfo.Money = System.DBNull.Value != dbreader["money"] ? Convert.ToDouble(dbreader["money"]) : 0;
                    moneyInventory.FdInfo.FrozenMoney = System.DBNull.Value != dbreader["frozenMoney"] ? Convert.ToDouble(dbreader["frozenMoney"]) : 0;
                    moneyInventory.FdInfo.OccMoney = System.DBNull.Value != dbreader["OccMoney"] ? Convert.ToDouble(dbreader["OccMoney"]) : 0;
                    moneyInventory.FdInfo.State = System.DBNull.Value != dbreader["state"] ? dbreader["state"].ToString() : string.Empty;
                    moneyInventory.FdInfo.CashUser = System.DBNull.Value != dbreader["CashUser"] ? dbreader["CashUser"].ToString() : string.Empty;
                    moneyInventory.FdInfo.SubUser = System.DBNull.Value != dbreader["SubUser"] ? dbreader["SubUser"].ToString() : string.Empty;
                    moneyInventory.FdInfo.TanUser = System.DBNull.Value != dbreader["TanUser"] ? dbreader["TanUser"].ToString() : string.Empty;
                    moneyInventory.FdInfo.ConBankType = System.DBNull.Value != dbreader["ConBankType"] ? dbreader["ConBankType"].ToString() : string.Empty;
                    moneyInventory.FdInfo.OpenBank = System.DBNull.Value != dbreader["OpenBank"] ? dbreader["OpenBank"].ToString() : string.Empty;
                    moneyInventory.FdInfo.BankAccount = System.DBNull.Value != dbreader["BankAccount"] ? dbreader["BankAccount"].ToString() : string.Empty;
                    moneyInventory.FdInfo.AccountName = System.DBNull.Value != dbreader["AccountName"] ? dbreader["AccountName"].ToString() : string.Empty;
                    moneyInventory.FdInfo.BankCard = System.DBNull.Value != dbreader["BankCard"] ? dbreader["BankCard"].ToString() : string.Empty;
                    //IsGetMoney = true;
                    break;
                }
                //if (!IsGetMoney)
                //{
                //    return moneyInventory;
                //}
                //查询 库存 
                dbreader.Close();
                //sqlcmd.CommandText = "select au,ag,pt,pd from Stock_BZJ where userId=@userId ";
                //sqldr = sqlcmd.ExecuteReader();
                //if (sqldr.Read())
                //{
                //    moneyInventory.StorageQuantity.xau = System.DBNull.Value != sqldr["au"] ? Convert.ToDouble(sqldr["au"]) : 0;

                //    moneyInventory.StorageQuantity.xag = System.DBNull.Value != sqldr["ag"] ? Convert.ToDouble(sqldr["ag"]) : 0;

                //    moneyInventory.StorageQuantity.xpt = System.DBNull.Value != sqldr["pt"] ? Convert.ToDouble(sqldr["pt"]) : 0;

                //    moneyInventory.StorageQuantity.xpd = System.DBNull.Value != sqldr["pd"] ? Convert.ToDouble(sqldr["pd"]) : 0;
                //}
                moneyInventory.Result = true;
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                moneyInventory.Result = false;
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return moneyInventory;
        }

        /// <summary>
        /// 市价单查询
        /// </summary>
        /// <param name="account">被查询的账号</param>
        /// <param name="LoginID">登陆标识</param>
        /// <returns>市价单记录</returns>
        public List<TradeOrder> GetTradeOrderEx(string account, string LoginID)
        {
            List<TradeOrder> list = new List<TradeOrder>();

            System.Data.Common.DbDataReader dbreader = null;

            try
            {
                string userId = string.Empty;

                if (!ComFunction.ExistUserLoginID(LoginID))
                {
                    return list;
                }
                userId = ComFunction.GetUserId(account);
                if (string.IsNullOrEmpty(userId))//如果用户不存在
                {
                    return list;
                }
                string sql = "select c.account,a.Orderid,b.ProductName,b.pricecode,isnull(b.unit*a.UseQuantity,0) TotalWeight, a.productcode,a.quantity,a.usequantity,a.Orderprice,a.profitPrice,a.lossPrice,a.OccMoney,a.tradefee,a.storagefee,a.Ordertime,a.Ordertype " +
                                    "from Trade_Order a,Trade_Product b,base_user c where a.userId=@userId and a.productcode=b.productcode and c.userid=a.userid and c.status='1' order by a.Ordertime desc";
                dbreader = DbHelper.ExecuteReader(sql,
                        new System.Data.Common.DbParameter[]{DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@userId",DbParameterType.String,userId,ParameterDirection.Input)});
                while (dbreader.Read())
                {
                    TradeOrder tdr = new TradeOrder();
                    tdr.TradeAccount = dbreader["account"].ToString();
                    tdr.OrderId = dbreader["Orderid"].ToString();
                    tdr.ProductName = dbreader["ProductName"].ToString();
                    tdr.ProductCode = dbreader["productcode"].ToString();
                    tdr.PriceCode = dbreader["pricecode"].ToString();
                    tdr.Quantity = Convert.ToDouble(dbreader["quantity"]);
                    tdr.UseQuantity = Convert.ToDouble(dbreader["usequantity"]);
                    tdr.OrderPrice = Convert.ToDouble(dbreader["Orderprice"]);
                    tdr.ProfitPrice = Convert.ToDouble(dbreader["profitPrice"]);
                    tdr.LossPrice = Convert.ToDouble(dbreader["lossPrice"]);
                    tdr.OccMoney = Convert.ToDouble(dbreader["OccMoney"]);
                    tdr.TradeFee = Convert.ToDouble(dbreader["tradefee"]);
                    tdr.StorageFee = Convert.ToDouble(dbreader["storagefee"]);
                    tdr.OrderTime = Convert.ToDateTime(dbreader["Ordertime"]);
                    tdr.OrderType = dbreader["Ordertype"].ToString();
                    tdr.TotalWeight = Convert.ToDouble(dbreader["TotalWeight"]);
                    list.Add(tdr);
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (null != list && list.Count > 0)
                {
                    list.Clear();
                }

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
        /// 挂单查询
        /// </summary>
        /// <param name="account">被查询的账号</param>
        /// <param name="LoginID">登陆标识</param>
        /// <returns>挂单记录</returns>
        public List<TradeHoldOrder> GetTradeHoldOrderEx(string account, string LoginID)
        {
            List<TradeHoldOrder> list = new List<TradeHoldOrder>();

            System.Data.Common.DbDataReader dbreader = null;

            try
            {
                string userId = string.Empty;

                if (!ComFunction.ExistUserLoginID(LoginID))
                {
                    return list;
                }
                userId = ComFunction.GetUserId(account);
                if(string.IsNullOrEmpty(userId))//如果用户不存在
                {
                    return list;
                }
                string sql = "select  c.account, a.HoldOrderID,b.ProductName, a.productcode,a.quantity,a.frozenMoney,a.OrderType,a.HoldPrice,a.profitPrice,a.lossPrice,a.validtime,a.ordertime " +
                                    "from Trade_HoldOrder a,Trade_Product b,base_user c  where a.userId=@userId and a.productcode=b.productcode and c.userid=a.userid and c.status='1' order by a.ordertime desc";
                dbreader = DbHelper.ExecuteReader(sql,
                                  new System.Data.Common.DbParameter[]{DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@userId",DbParameterType.String,userId,ParameterDirection.Input)});
                while (dbreader.Read())
                {
                    TradeHoldOrder thdr = new TradeHoldOrder();
                    thdr.TradeAccount = dbreader["account"].ToString();
                    thdr.HoldOrderID = dbreader["HoldOrderID"].ToString();
                    thdr.ProductName = dbreader["ProductName"].ToString();
                    thdr.ProductCode = dbreader["productcode"].ToString();
                    thdr.Quantity = Convert.ToDouble(dbreader["quantity"]);
                    thdr.FrozenMoney = Convert.ToDouble(dbreader["frozenMoney"]);

                    thdr.OrderType = dbreader["OrderType"].ToString();
                    thdr.HoldPrice = Convert.ToDouble(dbreader["HoldPrice"]);
                    thdr.ProfitPrice = Convert.ToDouble(dbreader["profitPrice"]);
                    thdr.LossPrice = Convert.ToDouble(dbreader["lossPrice"]);
                    thdr.ValidTime = Convert.ToDateTime(dbreader["validtime"]);
                    thdr.OrderTime = Convert.ToDateTime(dbreader["Ordertime"]);
                    list.Add(thdr);
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (null != list && list.Count > 0)
                {
                    list.Clear();
                }

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
        /// 市价单历史查询
        /// </summary>
        /// <param name="Lqc">查询条件</param>
        /// <param name="Ltype">"1"平仓历史 "2"入库历史</param>
        /// <param name="pageindex">第几页,从1开始</param>
        /// <param name="pagesize">每页多少条</param>
        /// <param name="page">输出参数(总页数)</param>
        /// <returns>市价单历史记录</returns>
        public List<LTradeOrder> GetLTradeOrder(LQueryCon Lqc, string Ltype, int pageindex, int pagesize, ref int page)
        {

            List<LTradeOrder> list = new List<LTradeOrder>();

            System.Data.Common.DbDataReader dbreader = null;
            TradeUser TdUser = new TradeUser();
            System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
             "@PageCount", DbParameterType.String, 0, ParameterDirection.Output);
            try
            {
                string AndStr = string.Empty;
                string PartSearchCondition = string.Empty;
                string ParentOrgID = string.Empty;
                if (!ComFunction.ExistUserLoginID(Lqc.LoginID, ref TdUser))
                {
                    return list;
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
                    AndStr += string.Format(" and [orgname] like '{0}%' ", Lqc.OrgName);
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


                //内部子查询字段列表
                string SubSelectList = "orgname,Account,ProductName,historyOrderId,productcode,lossprice,profitPrice,Orderprice,overType,overprice,profitValue,tradefee,storagefee,Overtime,Orderid,ordertype,quantity,ordertime,adjustbase,valuedot,unit,lowerprice ";
                //选择字段列表
                string selectlist = "orgname,Account,ProductName,historyOrderId,productcode,lossprice,profitPrice,Orderprice,overType,overprice,profitValue,tradefee,storagefee,Overtime,Orderid,ordertype,quantity,ordertime,adjustbase,valuedot,unit,lowerprice ";

                //查询条件
                string SearchCondition = string.Format("where overtime >= '{0}' and overtime <='{1}' {2} {3} ",
                    Lqc.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), Lqc.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), AndStr, PartSearchCondition);


                dbreader = DbHelper.RunProcedureGetDataReader("GetRecordFromPageEx",
                     new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@selectlist",DbParameterType.String,selectlist,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SubSelectList",DbParameterType.String,SubSelectList,ParameterDirection.Input),
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
                    if ("0" == ltradeOrder.OrderType)
                    {
                        ltradeOrder.ProductMoney = System.Math.Round(ltradeOrder.OverPrice / Convert.ToDouble(dbreader["adjustbase"]) * Convert.ToDouble(dbreader["valuedot"]) * Convert.ToDouble(dbreader["quantity"]), 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        //ltradeOrder.ProductMoney = System.Math.Round(Convert.ToDouble(sqldr["unit"]) * Convert.ToDouble(sqldr["lowerprice"]) * Convert.ToDouble(sqldr["quantity"]), 2, MidpointRounding.AwayFromZero);
                        //卖单入库的货款(即所谓的折旧费) 已经计算在盈亏里面了 所以没有所谓的货款
                        ltradeOrder.ProductMoney = 0;
                    }
                    list.Add(ltradeOrder);
                }

            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (null != list && list.Count > 0)
                {
                    list.Clear();
                }
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
            return list;
        }

        #endregion

        
        /// <summary>
        /// 挂单历史查询
        /// </summary>
        /// <param name="Lqc">查询条件</param>
        /// <param name="pageindex">第几页,从1开始</param>
        /// <param name="pagesize">每页多少条</param>
        /// <param name="page">输出参数(总页数)</param>
        /// <returns>挂单历史记录</returns>
        public List<LTradeHoldOrder> GetLTradeHoldOrder(LQueryCon Lqc, int pageindex, int pagesize, ref int page)
        {
            List<LTradeHoldOrder> list = new List<LTradeHoldOrder>();
            System.Data.Common.DbDataReader dbreader = null;
            TradeUser TdUser = new TradeUser();
            System.Data.Common.DbParameter OutputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                 "@PageCount", DbParameterType.String, 0, ParameterDirection.Output);
            try
            {
                string userId = string.Empty;
                string AndStr = string.Empty;
                string PartSearchCondition = string.Empty;
                string ParentOrgID = string.Empty;
                if (!ComFunction.ExistUserLoginID(Lqc.LoginID, ref TdUser))
                {
                    return list;
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
                        AndStr += string.Format(" and userid='{0}' ", ComFunction.GetUserId(Lqc.TradeAccount));
                    }
                }

                if ("ALL" != Lqc.ProductName.ToUpper())
                {
                    AndStr += string.Format(" and ProductName='{0}'", Lqc.ProductName);
                }
                if ("ALL" != Lqc.OrderType.ToUpper())
                {
                    AndStr += string.Format(" and ordertype='{0}'", Lqc.OrderType);
                }

                string SubSelectList = "orgname,Account,ProductName,HoldOrderID,productCode,OrderType,HoldPrice,lossPrice,profitPrice,frozenMoney,validtime,ordertime,istrade,OrderID,quantity,tradetime ";
                string selectlist = "orgname,Account,ProductName,HoldOrderID,productCode,OrderType,HoldPrice,lossPrice,profitPrice,frozenMoney,validtime,ordertime,istrade,OrderID,quantity,tradetime ";


                string SearchCondition = string.Format("where tradetime >= '{0}' and tradetime <='{1}' {2} {3} ",
                    Lqc.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), Lqc.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), AndStr,PartSearchCondition);


                dbreader = DbHelper.RunProcedureGetDataReader("GetRecordFromPageEx",
                    new System.Data.Common.DbParameter[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@selectlist",DbParameterType.String,selectlist,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SubSelectList",DbParameterType.String,SubSelectList,ParameterDirection.Input),
                       DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableSource",DbParameterType.String,"V_L_Trade_HoldOrder",ParameterDirection.Input), //表名或视图表 
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableOrder",DbParameterType.String,"a",ParameterDirection.Input), //排序后的表名称 即子查询结果集的别名
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SearchCondition",DbParameterType.String,SearchCondition,ParameterDirection.Input),
                          DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@OrderExpression",DbParameterType.String,"order by tradetime desc",ParameterDirection.Input),//排序 表达式
                            DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ParentOrgID",DbParameterType.String,ParentOrgID,ParameterDirection.Input),//父级组织ID
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageIndex",DbParameterType.Int,pageindex,ParameterDirection.Input),
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageSize",DbParameterType.Int,pagesize,ParameterDirection.Input),
                            OutputParam});
                while (dbreader.Read())
                {
                    LTradeHoldOrder ltradeHoldOrder = new LTradeHoldOrder();
                    ltradeHoldOrder.OrgName = System.DBNull.Value != dbreader["orgname"] ? dbreader["orgname"].ToString() : string.Empty;
                    ltradeHoldOrder.TradeAccount = System.DBNull.Value != dbreader["Account"] ? dbreader["Account"].ToString() : string.Empty;
                    ltradeHoldOrder.HoldOrderID = System.DBNull.Value != dbreader["HoldOrderID"] ? dbreader["HoldOrderID"].ToString() : string.Empty;
                    ltradeHoldOrder.ProductName = System.DBNull.Value != dbreader["ProductName"] ? dbreader["ProductName"].ToString() : string.Empty;
                    ltradeHoldOrder.ProductCode = System.DBNull.Value != dbreader["productCode"] ? dbreader["productCode"].ToString() : string.Empty;
                    ltradeHoldOrder.OrderType = System.DBNull.Value != dbreader["OrderType"] ? dbreader["OrderType"].ToString() : string.Empty;
                    ltradeHoldOrder.HoldPrice = System.DBNull.Value != dbreader["HoldPrice"] ? Convert.ToDouble(dbreader["HoldPrice"]) : 0;
                    ltradeHoldOrder.LossPrice = System.DBNull.Value != dbreader["lossPrice"] ? Convert.ToDouble(dbreader["lossPrice"]) : 0;
                    ltradeHoldOrder.ProfitPrice = System.DBNull.Value != dbreader["profitPrice"] ? Convert.ToDouble(dbreader["profitPrice"]) : 0;
                    ltradeHoldOrder.FrozenMoney = System.DBNull.Value != dbreader["frozenMoney"] ? Convert.ToDouble(dbreader["frozenMoney"]) : 0;
                    ltradeHoldOrder.ValidTime = System.DBNull.Value != dbreader["validtime"] ? Convert.ToDateTime(dbreader["validtime"]) : DateTime.MinValue;
                    ltradeHoldOrder.OrderTime = System.DBNull.Value != dbreader["ordertime"] ? Convert.ToDateTime(dbreader["ordertime"]) : DateTime.MinValue;
                    ltradeHoldOrder.State = System.DBNull.Value != dbreader["istrade"] ? dbreader["istrade"].ToString() : string.Empty;
                    ltradeHoldOrder.OrderID = System.DBNull.Value != dbreader["OrderID"] ? dbreader["OrderID"].ToString() : string.Empty;
                    ltradeHoldOrder.Quantity = System.DBNull.Value != dbreader["quantity"] ? Convert.ToDouble(dbreader["quantity"]) : 0;
                    ltradeHoldOrder.TradeTime = System.DBNull.Value != dbreader["tradetime"] ? Convert.ToDateTime(dbreader["tradetime"]) : DateTime.MinValue;
                    list.Add(ltradeHoldOrder);
                }
                
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (null != list && list.Count > 0)
                {
                    list.Clear();
                }
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close(); dbreader.Dispose();
                    page = Convert.ToInt32(OutputParam.Value);
                }
            }
            return list;
        }


        /// <summary>
        /// 下单延迟多少秒
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public double GetDelayPlaceOrder(string userid)
        {
            System.Data.Common.DbDataReader dbreader = null;
            double DelayPlaceOrder = 0;
            try
            {
                string sql = string.Format(@"select a.DelayPlaceOrder from Trade_UserGroups a,Trade_User_Group b 
                        where a.UserGroupId=b.UserGroupId and b.userid=@userid"); //使用参数化的sql语句，防止sql注入
                dbreader = DbHelper.ExecuteReader(sql,
                    new System.Data.Common.DbParameter[]
                    {
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                            "@userid", DbParameterType.String, userid, ParameterDirection.Input)
                    });
                if (dbreader.Read())
                {
                    DelayPlaceOrder = Convert.ToDouble(dbreader["DelayPlaceOrder"]);
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
            return DelayPlaceOrder;
        }
        /// <summary>
        /// 平仓延迟多少秒
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public double GetDelayFlatOrder(string userid)
        {
            System.Data.Common.DbDataReader dbreader = null;
            double DelayFlatOrder = 0;
            try
            {
                string sql = string.Format(@"select a.DelayFlatOrder from Trade_UserGroups a,Trade_User_Group b 
                        where a.UserGroupId=b.UserGroupId and b.userid=@userid"); //使用参数化的sql语句，防止sql注入
                dbreader = DbHelper.ExecuteReader(sql,
                    new System.Data.Common.DbParameter[]
                    {
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                            "@userid", DbParameterType.String, userid, ParameterDirection.Input)
                    });
                if (dbreader.Read())
                {
                    DelayFlatOrder = Convert.ToDouble(dbreader["DelayFlatOrder"]);
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
            return DelayFlatOrder;
        }
    }
}
