//*******************************************************************************
//  文 件 名：CTrade.ygj.cs
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
using System.Text;
using WcfInterface.model;
using WcfInterface;
using com.individual.helper;
using JinTong.Bzj.Common.Encrypt;
using System.Collections;

namespace Trade
{
    /// <summary>
    /// 用户交易接口
    /// </summary>
    public partial class CTrade : ITrade
    {

        /// <summary>
        /// 市价单查询
        /// </summary>
        /// <param name="LoginID">登陆标识</param>
        /// <returns>市价单记录</returns>
        public List<TradeOrder> GetTradeOrder(string LoginID)
        {
            List<TradeOrder> list = new List<TradeOrder>();

            System.Data.Common.DbDataReader dbreader = null;
           
            try
            {
                string userId = string.Empty;

                if (!ComFunction.ExistUserLoginID(LoginID, ref userId))
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
        /// <param name="LoginID">登陆标识</param>
        /// <returns>挂单记录</returns>
        public List<TradeHoldOrder> GetTradeHoldOrder(string LoginID)
        {
            List<TradeHoldOrder> list = new List<TradeHoldOrder>();

            System.Data.Common.DbDataReader dbreader = null;

            try
            {
                string userId = string.Empty;
 
                if (!ComFunction.ExistUserLoginID(LoginID, ref userId))
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

        #region 挂单 挂单取消 ; 市价单下单 市价单修改 市价单平仓
        /// <summary>
        /// 挂单下单
        /// </summary>
        /// <param name="orderln">下单信息</param>
        /// <returns>下单结果</returns>
        public Orders GetOrders(OrdersLncoming orderln)
        {
            Orders orders = new Orders();
            orders.MoneyInventory = new MoneyInventory();
            orders.TradeHoldOrder = new TradeHoldOrder();
            ProductConfig ptc = new ProductConfig();
            DateTime dt = DateTime.Now;//服务器当前本地时间\
            TradeUser TdUser = new TradeUser();
            double _frozenMoney = 0;//冻结资金
            double fxrate = 0; //风险率
            string holdid = string.Empty;//挂单ID
            string userId = string.Empty; //被操作的用户ID
            string operUser = string.Empty;//操作人
            string ipmac = string.Empty;
            int operUserType = 0;
            try
            {

                #region 判断用户登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(orderln.LoginID, ref TdUser))
                {
                    orders.Result = false;
                    orders.ReturnCode = ResCode.UL003;
                    orders.Desc = ResCode.UL003Desc;
                    return orders;
                }
                operUser = TdUser.Account;
                ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                operUserType = (int)TdUser.UType;
                if (UserType.NormalType == TdUser.UType)
                {
                    userId = TdUser.UserID;
                }
                else
                {
                    userId = ComFunction.GetUserId(orderln.TradeAccount, ref TdUser);
                }

                #endregion

                #region 交易手数验证
                if (!ComFunction.ValidateQuantity(orderln.Quantity,TdUser.OrderUnit))
                {
                    orders.Result = false;
                    orders.ReturnCode = ResCode.UL044;
                    //orders.Desc = ResCode.UL044Desc;
                    orders.Desc = string.Format("交易手数必须是{0}的倍数", TdUser.OrderUnit);
                    return orders;
                }
                #endregion

                #region 判断用户是否允许挂买单或挂卖单
                if ("0" == orderln.OrderType) //挂买单
                {
                    if (!TdUser.PermitDhuo)
                    {
                        orders.Result = false;
                        orders.ReturnCode = ResCode.UL017;
                        orders.Desc = ResCode.UL017Desc;
                        return orders;
                    }
                }
                else if ("1" == orderln.OrderType) //挂卖单
                {
                    if (!TdUser.PermitHshou) 
                    {
                        orders.Result = false;
                        orders.ReturnCode = ResCode.UL018;
                        orders.Desc = ResCode.UL018Desc;
                        return orders;
                    }
                }
                else
                {
                    orders.Result = false;
                    orders.ReturnCode = ResCode.UL021;
                    orders.Desc = ResCode.UL021Desc;
                    return orders;
                }
                #endregion

                #region 获取商品信息

                ptc = ComFunction.GetProductInfo(orderln.ProductCode);
                //挂单类型（0买、1卖）
                if (string.IsNullOrEmpty(ptc.State)) //未能获取商品状态
                {
                    orders.Result = false;
                    orders.ReturnCode = ResCode.UL024;
                    orders.Desc = ResCode.UL024Desc;
                    return orders;
                }

                #endregion

                #region 判断当前时间服务器是否允许交易

                if (!ComFunction.GetDateset(ptc.PriceCode,dt))
                {
                    orders.Result = false;
                    orders.ReturnCode = ResCode.UL022;
                    orders.Desc = ResCode.UL022Desc;
                    return orders;
                }

                #endregion

                #region 挂单有效期判断

                //判断有效期是否大于挂单时间
                DateTime vlidt =
                    Convert.ToDateTime(string.Format("{0}-{1}-{2} 23:59:59", orderln.ValidTime.Year,
                        orderln.ValidTime.Month, orderln.ValidTime.Day));
                if (!(vlidt > dt))
                {
                    orders.Result = false;
                    orders.ReturnCode = ResCode.UL023;
                    orders.Desc = ResCode.UL023Desc;
                    return orders;
                }

                #endregion

                #region 判断商品是否处于交易时段
                if (!ComFunction.ProductCanTrade(ptc.Starttime, ptc.Endtime))
                {
                    orders.Result = false;
                    orders.ReturnCode = ResCode.UL025;
                    orders.Desc = ResCode.UL025Desc;
                    return orders;
                }
                #endregion

                #region 判断商品是否允许交易

                //商品的状态(0 正常交易, 1 只报价, 2 只买, 3 只卖, 4 全部禁止)
                if ("0" == ptc.State) //商品允许正常交易
                {
                    if ("0" == orderln.OrderType) //买
                    {
                        orders.Desc = "买成功";
                    }
                    else if ("1" == orderln.OrderType) //卖
                    {
                        orders.Desc = "卖成功";
                    }
                    else
                    {
                        orders.Result = false;
                        orders.ReturnCode = ResCode.UL021;
                        orders.Desc = ResCode.UL021Desc;
                        return orders;
                    }
                }
                else if ("2" == ptc.State) //商品只允许买
                {
                    if ("0" == orderln.OrderType) //买
                    {
                        orders.Desc = "买成功";
                    }
                    else
                    {
                        orders.Result = false;
                        orders.ReturnCode = ResCode.UL026;
                        orders.Desc = ResCode.UL026Desc;
                        return orders;
                    }
                }
                else if ("3" == ptc.State) //商品只允许卖
                {
                    if ("1" == orderln.OrderType) //卖
                    {
                        orders.Desc = "卖成功";
                    }
                    else
                    {
                        orders.Result = false;
                        orders.ReturnCode = ResCode.UL027;
                        orders.Desc = ResCode.UL027Desc;
                        return orders;
                    }
                }
                else
                {
                    orders.Result = false;
                    orders.ReturnCode = ResCode.UL025;
                    orders.Desc = ResCode.UL025Desc;
                    return orders;
                }

                #endregion

                #region 挂单价判断 (浮点数比较时，把两个数相减，然后和0.000001比较)

                //最小成交价格 < 挂单价 <最大成交价格
                if (!(orderln.HoldPrice - ptc.MinPrice >= ComFunction.dzero && orderln.HoldPrice - ptc.MaxPrice <= ComFunction.dzero))
                {
                    orders.Result = false;
                    orders.ReturnCode = ResCode.UL015;
                    orders.Desc = ResCode.UL015Desc;
                    return orders;
                }

                #endregion

                #region 最大交易时间差判断

                //当前客户端实时报价时间+允许最大交易时间差>=服务器时间
                if (!(orderln.CurrentTime.AddSeconds(ptc.MaxTime) >= dt))
                {
                    orders.Result = false;
                    orders.ReturnCode = ResCode.UL014;
                    orders.Desc = ResCode.UL014Desc;
                    return orders;
                }

                #endregion

                #region 风险率判断

                //获取帐户资金信息
                orders.MoneyInventory = ComFunction.GetMoneyInventoryByUserId(userId);
                if (!orders.MoneyInventory.Result)
                {
                    orders.Result = false;
                    orders.Desc = "未能获取资金库存";
                    return orders;
                }
                //冻结保证金=挂单价/[点差基值]*[点值]*下单数量*保证金百分比
                //风险率=（[占用]+[冻结]）/[账户结余]
                //风险率<=60%(加上本单的冻结保证金)
                //_frozenMoney =
                //    System.Math.Round(
                //        orderln.HoldPrice / ptc.AdjustBase * ptc.ValueDot * orderln.Quantity * orderln.OrderMoney, 2,
                //        MidpointRounding.AwayFromZero);
                _frozenMoney = System.Math.Round(ComFunction.Getfee(ptc.OrderMoneyFee, orderln.OrderMoney, orderln.HoldPrice, orderln.Quantity), 2, MidpointRounding.AwayFromZero);

                /* 老的风险率判断代码
                fxrate = (_frozenMoney + orders.MoneyInventory.FdInfo.OccMoney + orders.MoneyInventory.FdInfo.FrozenMoney) /
                         orders.MoneyInventory.FdInfo.Money;
                if (orders.MoneyInventory.FdInfo.Money - _frozenMoney - orders.MoneyInventory.FdInfo.OccMoney - orders.MoneyInventory.FdInfo.FrozenMoney<=ComFunction.dzero)
                {
                    orders.Result = false;
                    orders.ReturnCode = ResCode.UL012;
                    orders.Desc = ResCode.UL012Desc;
                    return orders;
                }
                if (!(fxrate - ComFunction.fenxian_rate <= ComFunction.dzero))
                {
                    orders.Result = false;
                    orders.ReturnCode = ResCode.UL013;
                    orders.Desc = ResCode.UL013Desc;
                    return orders;
                }
                */

                #region 新的风险率判断代码
                double DongJieMoney = 0;//冻结资金
                if (orders.MoneyInventory.FdInfo.DongJieMoney > ComFunction.dzero)
                {
                    DongJieMoney = orders.MoneyInventory.FdInfo.DongJieMoney;
                }
                double UseMoney = _frozenMoney + orders.MoneyInventory.FdInfo.OccMoney + orders.MoneyInventory.FdInfo.FrozenMoney;
                if (UseMoney <= ComFunction.dzero)//如果使用金额为0 则说明有问题 使用金额不可能小于0
                {
                    orders.Result = false;
                    orders.Desc = "挂单下单失败!";
                    return orders;
                }
                double yingkui = ComFunction.GetUserYingKui(userId);//用户的盈亏
                fxrate = (orders.MoneyInventory.FdInfo.Money - DongJieMoney + yingkui) / UseMoney;

                if (orders.MoneyInventory.FdInfo.Money - DongJieMoney - _frozenMoney - orders.MoneyInventory.FdInfo.OccMoney - orders.MoneyInventory.FdInfo.FrozenMoney <= ComFunction.dzero)
                {
                    orders.Result = false;
                    orders.ReturnCode = ResCode.UL012;
                    orders.Desc = ResCode.UL012Desc;
                    return orders;
                }
                if (fxrate - ComFunction.fenxian_rate <= ComFunction.dzero)
                {
                    orders.Result = false;
                    orders.ReturnCode = ResCode.UL013;
                    orders.Desc = ResCode.UL013Desc;
                    return orders;
                }
                //
                #endregion

                #endregion

                holdid = ComFunction.GetOrderId(ComFunction.Hold);

                #region 数据库事务处理
                List<string> sqlList = new List<string>(); 
                sqlList.Add(string.Format("INSERT INTO Trade_HoldOrder([userId],[HoldOrderID],[productCode],[quantity],[OrderType],[HoldPrice],[lossPrice],[profitPrice],[frozenMoney],[validtime],[ordertime],[ip],[mac]) VALUES('{0}','{1}','{2}',{3},{4},{5},{6},{7},{8},'{9}','{10}','{11}','{12}')",
                            userId, holdid, orderln.ProductCode,
                            orderln.Quantity, orderln.OrderType, orderln.HoldPrice, orderln.LossPrice,
                            orderln.ProfitPrice, _frozenMoney, orderln.ValidTime.ToString("yyyy-MM-dd"),
                            dt.ToString("yyyy-MM-dd HH:mm:ss.fff"), ComFunction.GetClientIp(), orderln.Mac));

                sqlList.Add(string.Format("update Trade_FundInfo set frozenMoney = frozenMoney+{0} where userid='{1}' and [state]<>'4'", _frozenMoney, userId));
                //添加操作记录
                
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, operUserType, string.Format("{1}委托订单{0}", holdid, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    orders.Result = false;
                    orders.Desc = "挂单时数据库事务处理出错";
                    return orders;
                }

                #endregion

                #region 给返回对象赋值

                orders.Result = true;
                orders.TradeHoldOrder.FrozenMoney = _frozenMoney;
                orders.TradeHoldOrder.HoldOrderID = holdid;
                orders.TradeHoldOrder.HoldPrice = orderln.HoldPrice;
                orders.TradeHoldOrder.LossPrice = orderln.LossPrice;
                orders.TradeHoldOrder.OrderTime = dt;
                orders.TradeHoldOrder.OrderType = orderln.OrderType;
                orders.TradeHoldOrder.ProductCode = orderln.ProductCode;
                orders.TradeHoldOrder.ProductName = ptc.ProductName;
                orders.TradeHoldOrder.ProfitPrice = orderln.ProfitPrice;
                orders.TradeHoldOrder.Quantity = orderln.Quantity;
                orders.TradeHoldOrder.ValidTime = orderln.ValidTime;

                orders.MoneyInventory.FdInfo.FrozenMoney += _frozenMoney;

                #endregion

            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                orders.Result = false;
                orders.Desc = "挂单下单失败";
                
            }
            return orders;
        }

        /// <summary>
        /// 挂单取消
        /// </summary>
        /// <param name="DhInfo">取消信息</param>
        /// <returns>取消结果</returns>
        public MarDelivery DelHoldOrder(DelHoldInfo DhInfo)
        {
            MarDelivery ctpOrder = new MarDelivery();
            ctpOrder.MoneyInventory = new MoneyInventory();
            DateTime dt = DateTime.Now;//服务器当前本地时间
            TradeHoldOrder tradeholdorder = new TradeHoldOrder();
            ProductConfig ptc = new ProductConfig();
            TradeUser TdUser = new TradeUser();
            //从挂单表中查询出的用户ID IP MAC
            string hold_userid = string.Empty;
            string hold_ip = string.Empty;
            string hold_mac = string.Empty;
            string userId = string.Empty;
            string operUser = DhInfo.TradeAccount;
            string ipmac = string.Empty;
            try
            {                   
                #region 判断用户登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(DhInfo.LoginID,ref TdUser))
                {
                    ctpOrder.Result = false;
                    ctpOrder.ReturnCode = ResCode.UL003;
                    ctpOrder.Desc = ResCode.UL003Desc;
                    return ctpOrder;
                }
                operUser = TdUser.Account;
                ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);

                if (UserType.NormalType == TdUser.UType)
                {
                    userId = TdUser.UserID;
                }
                else
                {
                    userId = ComFunction.GetUserId(DhInfo.TradeAccount);
                }
                #endregion

                #region 根据挂单ID获取挂单信息
                tradeholdorder = ComFunction.GetTradeHoldOrder(DhInfo.HoldOrderID, ref hold_userid, ref hold_ip, ref hold_mac);
                if (string.IsNullOrEmpty(tradeholdorder.ProductCode))
                {
                    ctpOrder.Result = false;
                    ctpOrder.ReturnCode = ResCode.UL028;
                    ctpOrder.Desc = ResCode.UL028Desc;
                    return ctpOrder;
                }
                #endregion

                #region 获取商品信息
                ptc = ComFunction.GetProductInfo(tradeholdorder.ProductCode);
                //挂单类型（0买、1卖）
                //未能获取商品状态
                if (string.IsNullOrEmpty(ptc.State))
                {
                    ctpOrder.Result = false;
                    ctpOrder.ReturnCode = ResCode.UL024;
                    ctpOrder.Desc = ResCode.UL024Desc;
                    return ctpOrder;
                }
                #endregion

                #region 判断当前时间是否允许交易
                if (!ComFunction.GetDateset(ptc.PriceCode, dt))
                {
                    ctpOrder.Result = false;
                    ctpOrder.ReturnCode = ResCode.UL022;
                    ctpOrder.Desc = ResCode.UL022Desc;
                    return ctpOrder;
                }
                #endregion
             
                #region 判断商品是否处于交易时段
                if (!ComFunction.ProductCanTrade(ptc.Starttime, ptc.Endtime))
                {
                    ctpOrder.Result = false;
                    ctpOrder.ReturnCode = ResCode.UL025;
                    ctpOrder.Desc = ResCode.UL025Desc;
                    return ctpOrder;
                }
                #endregion

                #region 最大交易时间差判断
                //当前客户端实时报价时间+允许最大交易时间差>=服务器时间
                if (!(DhInfo.CurrentTime.AddSeconds(ptc.MaxTime) >= dt))
                {
                    ctpOrder.Result = false;
                    ctpOrder.ReturnCode = ResCode.UL014;
                    ctpOrder.Desc = ResCode.UL014Desc;
                    return ctpOrder;
                }
                #endregion

                ctpOrder.MoneyInventory = ComFunction.GetMoneyInventoryByUserId(userId);
                if (!ctpOrder.MoneyInventory.Result)
                {
                    ctpOrder.Result = false;
                    ctpOrder.Desc = "未能获取资金库存";
                    return ctpOrder;
                }
                #region 数据库事务处理

                /*
                List<string> sqlList = new List<string>();
                sqlList.Add(string.Format("delete from Trade_HoldOrder where holdorderid='{0}'", DhInfo.HoldOrderID));

                sqlList.Add(string.Format("INSERT INTO L_Trade_HoldOrder([userId],[HoldOrderID],[productCode],[quantity],[OrderType],[HoldPrice],[lossPrice],[profitPrice],[frozenMoney],[validtime],[ordertime],[IsTrade],[tradetime],[OrderID],[failreason],[ip],[mac]) VALUES('{0}','{1}','{2}',{3},{4},{5},{6},{7},{8},'{9}','{10}',{11},'{12}','{13}','{14}','{15}','{16}')",
                    hold_userid, tradeholdorder.HoldOrderID, tradeholdorder.ProductCode, tradeholdorder.Quantity,
                    tradeholdorder.OrderType, tradeholdorder.HoldPrice, tradeholdorder.LossPrice, tradeholdorder.ProfitPrice,
                    tradeholdorder.FrozenMoney, tradeholdorder.ValidTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), tradeholdorder.OrderTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), 1, dt.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                   ComFunction.GetOrderId(ComFunction.Hold_His), DhInfo.ReasonType, hold_ip, hold_mac));
                double ResultfrozenMoney = ctpOrder.MoneyInventory.FdInfo.FrozenMoney - tradeholdorder.FrozenMoney;
                if (ResultfrozenMoney < ComFunction.dzero)
                {
                    ResultfrozenMoney = 0;
                }
                sqlList.Add(string.Format("update Trade_FundInfo set frozenMoney={0} where userid='{1}' and [state]<>'4'", ResultfrozenMoney, userId));

                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}撤销委托订单{0}", DhInfo.HoldOrderID,ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    ctpOrder.Result = false;
                    ctpOrder.Desc = "挂单取消时数据库事务处理出错";
                    return ctpOrder;
                }
                */
                //改用存储过程实现
                int result = ComFunction.DoHoldTradeOrder(DhInfo.HoldOrderID, 1, 0);
                #endregion

                if (99 == result)
                {
                    ctpOrder.Result = false;
                    ctpOrder.Desc = "挂单取消失败!";
                    return ctpOrder;
                }

                #region 给返回对象赋值
 
                ctpOrder.MoneyInventory.FdInfo.FrozenMoney -= tradeholdorder.FrozenMoney;
                ctpOrder.Result = true;
                switch (result)//99失败,100成功,0挂单已被自动撤销 1挂单已被手动撤销 2挂单已经自动转换为订单
                {
                    case 0: ctpOrder.Desc = "挂单已被自动撤销";
                        break;
                    case 1: ctpOrder.Desc = "挂单已被手动撤销";
                        break;
                    case 2: ctpOrder.Desc = "挂单已经自动转换为订单";
                        break;
                    default: ctpOrder.Desc = "挂单取消成功";
                        break;
                }
                #endregion
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                ctpOrder.Result = false;
                ctpOrder.Desc = "挂单取消失败";
                
            }

            return ctpOrder;
        }

        /// <summary>
        /// 市价单下单
        /// </summary>
        /// <param name="marorderln">下单信息</param>
        /// <returns>下单结果</returns>
        public Marketorders GetMarketorders(MarOrdersLn marorderln)
        {
            Marketorders marketorders = new Marketorders();
            marketorders.MoneyInventory = new MoneyInventory();
            marketorders.TradeOrder = new TradeOrder();
            ProductConfig ptc = new ProductConfig();
            TradeUser TdUser = new TradeUser();
            DateTime dt = DateTime.Now;//服务器当前本地时间
            double _occMoney = 0;//占用资金
            double realprice = 0;//服务器端实时价格(即此时此刻的建仓价)
            double _tradefee = 0;//工费
            double fxrate = 0;
            string orderid = string.Empty;//市价单ID
            string userId = string.Empty;
            string allowStore = "1";//是否允许入库 "1"允许入库 "0" 不允许入库
            string operUser = marorderln.TradeAccount;
            string ipmac = string.Empty;
            int operUserType = 0;
            try
            {

                #region 判断用户登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(marorderln.LoginID, ref TdUser))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL003;
                    marketorders.Desc = ResCode.UL003Desc;
                    return marketorders;
                }

                operUser = TdUser.Account;
                ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                operUserType = (int)TdUser.UType;

                if (UserType.NormalType == TdUser.UType)
                {
                    userId = TdUser.UserID;
                }
                else
                {
                    userId = ComFunction.GetUserId(marorderln.TradeAccount, ref TdUser);
                }
                #endregion

                #region 交易手数验证
                if (!ComFunction.ValidateQuantity(marorderln.Quantity, TdUser.OrderUnit))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL044;
                    //marketorders.Desc = ResCode.UL044Desc;
                    marketorders.Desc = string.Format("交易手数必须是{0}的倍数", TdUser.OrderUnit);
                    return marketorders;
                }
                #endregion

                #region 判断用户是否允许下买单或卖单 
                if ("0" == marorderln.OrderType) //下买单
                {
                    if (!TdUser.PermitDhuo)
                    {
                        marketorders.Result = false;
                        marketorders.ReturnCode = ResCode.UL017;
                        marketorders.Desc = ResCode.UL017Desc;
                        return marketorders;
                    }
                }
                else if ("1" == marorderln.OrderType) //下卖单
                {
                    if (!TdUser.PermitHshou)
                    {
                        marketorders.Result = false;
                        marketorders.ReturnCode = ResCode.UL018;
                        marketorders.Desc = ResCode.UL018Desc;
                        return marketorders;
                    }
                    allowStore = ComFunction.AllowStore;//是否允许卖单入库,通过配置文件读取
                }
                else
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL021;
                    marketorders.Desc = ResCode.UL021Desc;
                    return marketorders;
                }
                #endregion

                #region 获取商品信息
                ptc = ComFunction.GetProductInfo(marorderln.ProductCode);
                //订单类型（0买、1卖）
                //未能获取商品状态
                if (string.IsNullOrEmpty(ptc.State))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL024;
                    marketorders.Desc = ResCode.UL024Desc;
                    return marketorders;
                }
                #endregion

                #region 判断当前时间是否允许交易
                if (!ComFunction.GetDateset(ptc.PriceCode,dt))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL022;
                    marketorders.Desc = ResCode.UL022Desc;
                    return marketorders;
                }
                #endregion

                #region 判断商品是否处于交易时段
                if (!ComFunction.ProductCanTrade(ptc.Starttime, ptc.Endtime))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL025;
                    marketorders.Desc = ResCode.UL025Desc;
                    return marketorders;
                }
                #endregion
                //获取服务器端实时价
                realprice = ComFunction.GetRealPrice(ptc.PriceCode);

                #region 判断商品是否允许交易 并确定下单价格
                //商品的状态(0 正常交易, 1 只报价, 2 只买, 3 只卖, 4 全部禁止)
                //商品允许正常交易
                if ("0" == ptc.State)
                {
                    //买
                    if ("0" == marorderln.OrderType)
                    {
                        marketorders.Desc = "买成功";
                        realprice += ptc.PriceDot * ptc.AdjustBase;
                    }
                    //卖
                    else if ("1" == marorderln.OrderType)
                    {
                        marketorders.Desc = "卖成功";
                    }
                    else
                    {
                        marketorders.Result = false;
                        marketorders.ReturnCode = ResCode.UL021;
                        marketorders.Desc = ResCode.UL021Desc;
                        return marketorders;
                    }
                }
                //商品只允许买
                else if ("2" == ptc.State)
                {
                    //买
                    if ("0" == marorderln.OrderType)
                    {
                        marketorders.Desc = "买成功";
                        realprice += ptc.PriceDot * ptc.AdjustBase;
                    }
                    else
                    {
                        marketorders.Result = false;
                        marketorders.ReturnCode = ResCode.UL026;
                        marketorders.Desc = ResCode.UL026Desc;
                        return marketorders;
                    }
                }
                //商品只允许卖
                else if ("3" == ptc.State)
                {
                    //卖
                    if ("1" == marorderln.OrderType)
                    {
                        marketorders.Desc = "卖成功";
                    }
                    else
                    {
                        marketorders.Result = false;
                        marketorders.ReturnCode = ResCode.UL027;
                        marketorders.Desc = ResCode.UL027Desc;
                        return marketorders;
                    }
                }
                else
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL025;
                    marketorders.Desc = ResCode.UL025Desc;
                    return marketorders;
                }
                #endregion

                #region 下单价判断
                //最小成交价格 < 建仓价 <最大成交价格
                if (!(realprice - ptc.MinPrice >= ComFunction.dzero && realprice - ptc.MaxPrice <= ComFunction.dzero))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL015;
                    marketorders.Desc = ResCode.UL015Desc;
                    return marketorders;
                }
                #endregion

                #region 最大交易时间差判断
                //当前客户端实时报价时间+允许最大交易时间差>=服务器时间
                if (!(marorderln.CurrentTime.AddSeconds(ptc.MaxTime) >= dt))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL014;
                    marketorders.Desc = ResCode.UL014Desc;
                    return marketorders;
                }
                #endregion

                #region 滑点判断
                //当前客户端实时价+滑点*点差基值>=实时价&&当前客户端实时价-滑点*点差基值<=实时价

                if (!(System.Math.Abs(marorderln.RtimePrices - realprice) - marorderln.MaxPrice * ptc.AdjustBase <= ComFunction.dzero))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL016;
                    marketorders.Desc = ResCode.UL016Desc;
                    return marketorders;
                }
                #endregion

                UserGroups ugs = ComFunction.GetUserGroups(userId);

                #region 对同一行情的商品，平仓后xxx秒不能下单的判断
                //实现方法：在历史订单表中查询此用户此行情在最近xxx秒内是否存在历史订单
                //如果存在，则不允许下单；否则允许下单
                if (ComFunction.JudgeExistLTradeOrder(userId, ptc.PriceCode, ugs.AfterSecond))
                {
                    marketorders.Result = false;
                    marketorders.Desc = string.Format("连接服务器失败,请重试!", ugs.AfterSecond);
                    return marketorders;
                }
                #endregion

                #region 根据下单滑点重新计算下单价

                if ("1" == marorderln.OrderType)//卖单 下单价向下滑
                {
                    realprice -= ugs.PlaceOrderSlipPoint * ptc.AdjustBase;
                }
                else//买单 下单价向上滑
                {
                    realprice += ugs.PlaceOrderSlipPoint * ptc.AdjustBase;
                }
                if (realprice <= ComFunction.dzero)
                {
                    marketorders.Result = false;
                    marketorders.Desc = "市价单下单异常";
                    return marketorders;
                }
                #endregion
                realprice = System.Math.Round(realprice, ptc.AdjustCount, MidpointRounding.AwayFromZero);//把多余的小数位去掉
                #region 风险率判断
                marketorders.MoneyInventory = ComFunction.GetMoneyInventoryByUserId(userId);
                if (!marketorders.MoneyInventory.Result)
                {
                    marketorders.Result = false;
                    marketorders.Desc = "未能获取资金库存";
                    return marketorders;
                }
                //计算本次下单的占用资金
                //_occMoney = System.Math.Round(marorderln.RtimePrices / ptc.AdjustBase * ptc.ValueDot * marorderln.Quantity * marorderln.OrderMoney, 2, MidpointRounding.AwayFromZero);
                _occMoney = System.Math.Round(ComFunction.Getfee(ptc.OrderMoneyFee, marorderln.OrderMoney, realprice, marorderln.Quantity), 2, MidpointRounding.AwayFromZero);
                //工费
                _tradefee = System.Math.Round(ComFunction.Getfee(ptc.ExpressionFee, marorderln.OrderMoney, realprice, marorderln.Quantity), 2, MidpointRounding.AwayFromZero);

                /* 老的风险率判断代码
                //风险率<=60%(加上本单的占用保证金和工费)
                fxrate = (_tradefee + _occMoney + marketorders.MoneyInventory.FdInfo.OccMoney + marketorders.MoneyInventory.FdInfo.FrozenMoney) / marketorders.MoneyInventory.FdInfo.Money;
                if (marketorders.MoneyInventory.FdInfo.Money - _tradefee - _occMoney - marketorders.MoneyInventory.FdInfo.OccMoney - marketorders.MoneyInventory.FdInfo.FrozenMoney <= ComFunction.dzero)
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL012;
                    marketorders.Desc = ResCode.UL012Desc;
                    //com.individual.helper.LogNet4.WriteMsg(string.Format("Money={0},_tradefee + _occMoney + OccMoney={1}+{2}+{3}",
                    //    marketorders.MoneyInventory.FdInfo.Money, _tradefee, _occMoney, marketorders.MoneyInventory.FdInfo.OccMoney));
                    return marketorders;
                }
                if (!(fxrate - ComFunction.fenxian_rate <= ComFunction.dzero))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL013;
                    marketorders.Desc = ResCode.UL013Desc;
                    return marketorders;
                }
                */

                #region 新的风险率判断代码
                double DongJieMoney = 0;//冻结资金
                if (marketorders.MoneyInventory.FdInfo.DongJieMoney > ComFunction.dzero)
                {
                    DongJieMoney = marketorders.MoneyInventory.FdInfo.DongJieMoney;
                }
                double UseMoney = _occMoney + marketorders.MoneyInventory.FdInfo.OccMoney + marketorders.MoneyInventory.FdInfo.FrozenMoney;
                if (UseMoney <= ComFunction.dzero)//如果使用金额为0 则说明有问题 使用金额不可能小于0
                {
                    marketorders.Result = false;
                    marketorders.Desc = "市价单下单失败!";
                    return marketorders;
                }
                double yingkui = ComFunction.GetUserYingKui(userId);//用户的盈亏
                //com.individual.helper.LogNet4.WriteMsg(yingkui.ToString());
                fxrate = (marketorders.MoneyInventory.FdInfo.Money - DongJieMoney - _tradefee + yingkui) / UseMoney;//分子=当前帐户余额-冻结资金-本次的工费

                if (marketorders.MoneyInventory.FdInfo.Money - DongJieMoney - _tradefee - _occMoney - marketorders.MoneyInventory.FdInfo.OccMoney - marketorders.MoneyInventory.FdInfo.FrozenMoney <= ComFunction.dzero)
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL012;
                    marketorders.Desc = ResCode.UL012Desc;
                    //com.individual.helper.LogNet4.WriteMsg(string.Format("Money={0},_tradefee + _occMoney + OccMoney={1}+{2}+{3}",
                    //    marketorders.MoneyInventory.FdInfo.Money, _tradefee, _occMoney, marketorders.MoneyInventory.FdInfo.OccMoney));
                    return marketorders;
                }
                if (fxrate - ComFunction.fenxian_rate <= ComFunction.dzero)
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL013;
                    marketorders.Desc = ResCode.UL013Desc;
                    return marketorders;
                }
                #endregion

                #endregion

                //下单的时候不计算仓储费,直接赋值为0
                marketorders.TradeOrder.StorageFee = 0;
                orderid = ComFunction.GetOrderId(ComFunction.Order);
                #region 数据库事务处理
                List<string> sqlList = new List<string>();
                sqlList.Add(string.Format("INSERT INTO Trade_Order([userId],[Orderid],[productCode],[Ordertype],[Orderprice],[usequantity],[quantity],[lossprice],[profitPrice],[OccMoney],[tradefee],[storagefee],[Ordertime],[OperType],[ip],[mac],[AllowStore]) VALUES('{0}','{1}','{2}',{3},{4},{5},{6},{7},{8},{9},{10},{11},'{12}',{13},'{14}','{15}',{16})",
                    userId, orderid, marorderln.ProductCode, marorderln.OrderType, realprice, marorderln.Quantity,
                    marorderln.Quantity, marorderln.LossPrice, marorderln.ProfitPrice, _occMoney, _tradefee, marketorders.TradeOrder.StorageFee,
                    dt.ToString("yyyy-MM-dd HH:mm:ss.fff"), 1, ComFunction.GetClientIp(), marorderln.Mac, allowStore));
                sqlList.Add(string.Format("update Trade_FundInfo set occMoney = occMoney+{0},[money]=[money]-{1} where userid='{2}' and [state]<>'4'", _occMoney,_tradefee, userId));
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser,operUserType, string.Format("{1}在手订单{0}", orderid,ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    marketorders.Result = false;
                    marketorders.Desc = "市价单下单时出错";
                    return marketorders;
                }
                #endregion

                #region 给返回对象赋值
                marketorders.Result = true;
                marketorders.ChengjiaoPrice = realprice;
                marketorders.MoneyInventory.FdInfo.OccMoney += _occMoney;
                marketorders.MoneyInventory.FdInfo.Money -= _tradefee;
                marketorders.TradeOrder.LossPrice = marorderln.LossPrice;
                marketorders.TradeOrder.OccMoney = _occMoney;
                marketorders.TradeOrder.OrderId = orderid;
                marketorders.TradeOrder.OrderPrice = realprice;
                marketorders.TradeOrder.OrderTime = dt;
                marketorders.TradeOrder.OrderType = marorderln.OrderType;
                marketorders.TradeOrder.ProductCode = marorderln.ProductCode;
                marketorders.TradeOrder.ProductName = ptc.ProductName;
                marketorders.TradeOrder.ProfitPrice = marorderln.ProfitPrice;
                marketorders.TradeOrder.Quantity = marorderln.Quantity;
                marketorders.TradeOrder.TradeFee = _tradefee;
                marketorders.TradeOrder.UseQuantity = marorderln.Quantity;
                marketorders.TradeOrder.PriceCode = ptc.PriceCode;
                #endregion

            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                marketorders.Result = false;
                marketorders.Desc = "市价单下单失败";
                
            }
            return marketorders;
        }

        /// <summary>
        /// 市价单修改
        /// </summary>
        /// <param name="marln">修改信息</param>
        /// <returns>修改结果</returns>
        public Marketorders ModifyMarketorders(MarketLnEnter marln)
        {
            Marketorders marketorders = new Marketorders();
            marketorders.MoneyInventory = new MoneyInventory();
            marketorders.TradeOrder = new TradeOrder();
            ProductConfig ptc = new ProductConfig();
            DateTime dt = DateTime.Now;//服务器当前本地时间
            string userId = string.Empty;
            string order_ip = string.Empty;
            string order_mac = string.Empty;
            string operUser = marln.TradeAccount;
            TradeUser TdUser = new TradeUser();
            string ipmac = string.Empty;
            try
            {
                #region 判断用户登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(marln.LoginID,ref TdUser))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL003;
                    marketorders.Desc = ResCode.UL003Desc;
                    return marketorders;
                }

                operUser = TdUser.Account;
                ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);

                if (UserType.NormalType == TdUser.UType)
                {
                    userId = TdUser.UserID;
                }
                else
                {
                    userId = ComFunction.GetUserId(marln.TradeAccount);
                }
                #endregion

                #region 根据订单ID获取订单信息
                marketorders.TradeOrder = ComFunction.GetTradeOrder(marln.OrderId, ref order_ip, ref order_mac);
                if (string.IsNullOrEmpty(marketorders.TradeOrder.OrderId))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL028;
                    marketorders.Desc = ResCode.UL028Desc;
                    return marketorders;
                }
                #endregion

                #region 获取商品信息
                ptc = ComFunction.GetProductInfo(marketorders.TradeOrder.ProductCode);
                //订单类型（0买、1卖）
                //未能获取商品状态
                if (string.IsNullOrEmpty(ptc.State))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL024;
                    marketorders.Desc = ResCode.UL024Desc;
                    return marketorders;
                }
                #endregion

                #region 判断当前时间是否允许交易
                if (!ComFunction.GetDateset(ptc.PriceCode,dt))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL022;
                    marketorders.Desc = ResCode.UL022Desc;
                    return marketorders;
                }
                #endregion

                #region 判断商品是否处于交易时段
                if (!ComFunction.ProductCanTrade(ptc.Starttime, ptc.Endtime))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL025;
                    marketorders.Desc = ResCode.UL025Desc;
                    return marketorders;
                }
                #endregion

                #region 最大交易时间差判断
                //当前客户端实时报价时间+允许最大交易时间差>=服务器时间
                if (!(marln.CurrentTime.AddSeconds(ptc.MaxTime) >= dt))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL014;
                    marketorders.Desc = ResCode.UL014Desc;
                    return marketorders;
                }
                #endregion

                marketorders.MoneyInventory = ComFunction.GetMoneyInventoryByUserId(userId);
                if (!marketorders.MoneyInventory.Result)
                {
                    marketorders.Result = false;
                    marketorders.Desc = "未能获取资金库存";
                    return marketorders;
                }

                #region 数据库事务处理
                List<string> sqlList = new List<string>();
                sqlList.Add(string.Format("UPDATE Trade_Order set profitPrice={0},lossprice={1} where Orderid='{2}' and userid='{3}'", marln.ProfitPrice, marln.LossPrice, marln.OrderId,userId));
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)TdUser.UType, string.Format("{1}在手订单{0},设置盈损点", marln.OrderId, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    marketorders.Result = false;
                    marketorders.Desc = "更新订单错误";
                    return marketorders;
                }
                #endregion

                #region 给返回对象赋值
                marketorders.Result = true;
                marketorders.Desc = "修改成功";
                marketorders.TradeOrder.ProfitPrice = marln.ProfitPrice;
                marketorders.TradeOrder.LossPrice = marln.LossPrice;
                marketorders.TradeOrder.PriceCode = ptc.PriceCode;
                #endregion
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                marketorders.Result = false;
                marketorders.Desc = "市价单修改失败";
                
            }
            return marketorders;
        }

        /// <summary>
        /// 市价单取消接口 结算盈亏 退还保证金
        /// </summary>
        /// <param name="delen">市价单取消信息</param>
        /// <returns>市价单取消结果</returns>
        public Marketorders DelOrder(DeliveryEnter delen)
        {
            Marketorders marketorders = new Marketorders();
            marketorders.TradeOrder = new TradeOrder();
            marketorders.MoneyInventory = new MoneyInventory();
            DateTime dt = DateTime.Now;//服务器当前本地时间
            ProductConfig ptc = new ProductConfig();
            TradeUser TdUser = new TradeUser();
            string order_ip = string.Empty;
            string order_mac = string.Empty;
            double realprice = 0;
            double _occmoney = 0;
            double _yingkuifee = 0;
            string tmpsql = string.Empty;
            string userId = string.Empty;
            string restrictions = string.Empty;
            string operUser = delen.TradeAccount;
            string ipmac = string.Empty;
            int operUserType = 0;
            try
            {

                #region 判断用户登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(delen.LoginID, ref TdUser))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL003;
                    marketorders.Desc = ResCode.UL003Desc;
                    return marketorders;
                }
                operUser = TdUser.Account;
                ipmac = ComFunction.GetIpMac(TdUser.Ip, TdUser.Mac);
                operUserType = (int)TdUser.UType;
                if (UserType.NormalType == TdUser.UType)
                {
                    userId = TdUser.UserID;
                }
                else
                {
                    userId = ComFunction.GetUserId(delen.TradeAccount, ref TdUser);
                }
                #endregion

                #region 交易手数验证
                if (!ComFunction.ValidateQuantity(delen.Quantity, TdUser.OrderUnit))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL044;
                    //marketorders.Desc = ResCode.UL044Desc;
                    marketorders.Desc = string.Format("交易手数必须是{0}的倍数", TdUser.OrderUnit);
                    return marketorders;
                }
                #endregion

                #region 判断用户是否允许平仓
                if (!TdUser.PermitDelOrder)
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL019;
                    marketorders.Desc = ResCode.UL019Desc;
                    return marketorders;
                }
                #endregion

                #region 根据订单ID获取订单信息
                marketorders.TradeOrder = ComFunction.GetTradeOrder(delen.Orderid, ref order_ip, ref order_mac);
                if (null == marketorders.TradeOrder.OrderId)
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL028;
                    marketorders.Desc = ResCode.UL028Desc;
                    return marketorders;
                }
                #endregion

                #region 获取商品信息
                ptc = ComFunction.GetProductInfo(marketorders.TradeOrder.ProductCode);
                //订单类型（0买、1卖）
                //未能获取商品状态
                if (string.Empty == ptc.State)
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL024;
                    marketorders.Desc = ResCode.UL024Desc;
                    return marketorders;
                }
                #endregion

                #region 判断当前时间是否允许交易
                if (!ComFunction.GetDateset(ptc.PriceCode,dt))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL022;
                    marketorders.Desc = ResCode.UL022Desc;
                    return marketorders;
                }
                #endregion

                #region 提货数判断
                if (!(delen.Quantity - marketorders.TradeOrder.UseQuantity <= ComFunction.dzero && delen.Quantity > ComFunction.dzero))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL011;
                    marketorders.Desc = ResCode.UL011Desc;
                    return marketorders;
                }
                #endregion

                #region 判断商品是否处于交易时段
                if (!ComFunction.ProductCanTrade(ptc.Starttime, ptc.Endtime))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL025;
                    marketorders.Desc = ResCode.UL025Desc;
                    return marketorders;
                }
                #endregion

                #region 最大交易时间差判断
                //当前客户端实时报价时间+允许最大交易时间差>=服务器时间
                if (!(delen.CurrentTime.AddSeconds(ptc.MaxTime) >= dt))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL014;
                    marketorders.Desc = ResCode.UL014Desc;
                    return marketorders;
                }
                #endregion

                realprice = ComFunction.GetRealPrice(ptc.PriceCode);
                //卖类型的订单 按买价平仓，买类型的订单 按卖价平仓
                if ("1" == marketorders.TradeOrder.OrderType)
                {
                    realprice += ptc.PriceDot * ptc.AdjustBase;
                }

                #region 滑点判断
                if (!(System.Math.Abs(delen.RtimePrices - realprice) - delen.MaxPrice * ptc.AdjustBase <= ComFunction.dzero))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL016;
                    marketorders.Desc = ResCode.UL016Desc;
                    return marketorders;
                }
                #endregion

                #region 获取资金库存
                marketorders.MoneyInventory = ComFunction.GetMoneyInventoryByUserId(userId);
                if (!marketorders.MoneyInventory.Result)
                {
                    marketorders.Result = false;
                    marketorders.Desc = "未能获取资金库存";
                    return marketorders;
                }
                #endregion

                int xs = 1;//工费收取倍数

                #region 根据平仓滑点重新计算平仓价
                UserGroups ugs = ComFunction.GetUserGroups(userId);
                
                if ("1" == marketorders.TradeOrder.OrderType)//卖单 平仓价向上滑
                {
                    realprice += ugs.FlatOrderSlipPoint * ptc.AdjustBase;
                }
                else//买单 平仓价向下滑
                {
                    realprice -= ugs.FlatOrderSlipPoint * ptc.AdjustBase;
                }
                if (realprice <= ComFunction.dzero)
                {
                    marketorders.Result = false;
                    marketorders.Desc = "平仓异常";
                    return marketorders;
                }
                #endregion
                realprice = System.Math.Round(realprice, ptc.AdjustCount, MidpointRounding.AwayFromZero);//把多余的小数位去掉
                #region 计算占用资金 盈亏
                _occmoney = System.Math.Round(marketorders.TradeOrder.OccMoney * delen.Quantity / marketorders.TradeOrder.UseQuantity, 2, MidpointRounding.AwayFromZero);
                //卖
                if ("1" == marketorders.TradeOrder.OrderType)
                {
                    //_yingkuifee = System.Math.Round(-(realprice - marketorders.TradeOrder.OrderPrice) / ptc.AdjustBase * ptc.ValueDot * delen.Quantity - (marketorders.TradeOrder.TradeFee * xs + marketorders.TradeOrder.StorageFee) * delen.Quantity / marketorders.TradeOrder.UseQuantity, 2, MidpointRounding.AwayFromZero);
                    _yingkuifee = System.Math.Round(-(realprice - marketorders.TradeOrder.OrderPrice) / ptc.AdjustBase * ptc.ValueDot * delen.Quantity, 2, MidpointRounding.AwayFromZero);

                }
                //买
                else
                {
                    //_yingkuifee = System.Math.Round((realprice - marketorders.TradeOrder.OrderPrice) / ptc.AdjustBase * ptc.ValueDot * delen.Quantity - (marketorders.TradeOrder.TradeFee * xs + marketorders.TradeOrder.StorageFee) * delen.Quantity / marketorders.TradeOrder.UseQuantity, 2, MidpointRounding.AwayFromZero);
                    _yingkuifee = System.Math.Round((realprice - marketorders.TradeOrder.OrderPrice) / ptc.AdjustBase * ptc.ValueDot * delen.Quantity, 2, MidpointRounding.AwayFromZero);
                }
              
                #endregion

                #region 数据库事务处理
                if (System.Math.Abs(marketorders.TradeOrder.UseQuantity - delen.Quantity) <= ComFunction.dzero)
                {
                    tmpsql = string.Format("delete from Trade_Order where Orderid='{0}'", delen.Orderid);
                }
                else
                {
                    tmpsql = string.Format("update Trade_Order set usequantity = {1},OccMoney=OccMoney*{1}/usequantity,tradefee=tradefee*{1}/usequantity,storagefee=storagefee*{1}/usequantity where Orderid='{0}'", delen.Orderid, marketorders.TradeOrder.UseQuantity - delen.Quantity);
                }
                List<string> sqlList = new List<string>();
                double ResultOccMoney = marketorders.MoneyInventory.FdInfo.OccMoney - _occmoney;
                if (ResultOccMoney < ComFunction.dzero)
                {
                    ResultOccMoney = 0;
                }
                sqlList.Add(string.Format("update Trade_FundInfo set occMoney={0}, money=money+{1} where userid='{2}' and [state]<>'4'", ResultOccMoney, _yingkuifee, userId));
                sqlList.Add(string.Format("INSERT INTO Fund_Change([userId],[reason],[Oldvalue],[NewValue],[OperUser],[OperTime],[RelaOrder],[ChangeValue],[CashUser]) VALUES('{0}','{1}',{2},{3},'{4}','{5}','{6}',{7},'{8}')", userId, 2, marketorders.MoneyInventory.FdInfo.Money, marketorders.MoneyInventory.FdInfo.Money + _yingkuifee, operUser, dt.ToString("yyyy-MM-dd HH:mm:ss.fff"), delen.Orderid, _yingkuifee, marketorders.MoneyInventory.FdInfo.CashUser));
                sqlList.Add(tmpsql);
                sqlList.Add(string.Format("INSERT INTO L_Trade_Order([userId],[historyOrderId],[Orderid],[productCode],[OrderType],[Orderprice],[overprice],[quantity],[lossprice],[profitPrice],[OccMoney],[tradefee],[storagefee],[Ordertime],[OperType],[profitValue],[Overtime],[overtype],[ip],[mac]) VALUES('{0}','{1}','{2}','{3}',{4},{5},{6},{7},{8},{9},{10},{11},{12},'{13}',{14},{15},'{16}',{17},'{18}','{19}')",
                    userId, ComFunction.GetOrderId(ComFunction.Order_His), marketorders.TradeOrder.OrderId, marketorders.TradeOrder.ProductCode, marketorders.TradeOrder.OrderType,
                    marketorders.TradeOrder.OrderPrice, realprice, delen.Quantity, marketorders.TradeOrder.LossPrice, marketorders.TradeOrder.ProfitPrice,
                    _occmoney, marketorders.TradeOrder.TradeFee * delen.Quantity / marketorders.TradeOrder.UseQuantity, marketorders.TradeOrder.StorageFee * delen.Quantity / marketorders.TradeOrder.UseQuantity,
                    marketorders.TradeOrder.OrderTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    marketorders.TradeOrder.OperType, _yingkuifee, dt.ToString("yyyy-MM-dd HH:mm:ss.fff"), 1, order_ip, order_mac));
                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, operUserType, string.Format("{1}平仓{0}", delen.Orderid, ipmac)));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    marketorders.Result = false;
                    marketorders.Desc = "平仓出错";
                    return marketorders;
                }
                #endregion

                //给返回对象赋值
                marketorders.Result = true;
                marketorders.Desc = "交易成功";
                marketorders.ChengjiaoPrice = realprice;
                marketorders.MoneyInventory.FdInfo.Money += _yingkuifee;
                marketorders.MoneyInventory.FdInfo.OccMoney -= _occmoney;
                marketorders.TradeOrder.OccMoney = System.Math.Round(marketorders.TradeOrder.OccMoney * (marketorders.TradeOrder.UseQuantity - delen.Quantity) / marketorders.TradeOrder.UseQuantity, 2, MidpointRounding.AwayFromZero);
                marketorders.TradeOrder.TradeFee = System.Math.Round(marketorders.TradeOrder.TradeFee * (marketorders.TradeOrder.UseQuantity - delen.Quantity) / marketorders.TradeOrder.UseQuantity, 2, MidpointRounding.AwayFromZero);
                marketorders.TradeOrder.StorageFee = System.Math.Round(marketorders.TradeOrder.StorageFee * (marketorders.TradeOrder.UseQuantity - delen.Quantity) / marketorders.TradeOrder.UseQuantity, 2, MidpointRounding.AwayFromZero);
                marketorders.TradeOrder.UseQuantity -= delen.Quantity;
                marketorders.TradeOrder.PriceCode = ptc.PriceCode;
            }         
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                marketorders.Result = false;
                marketorders.Desc = "平仓失败";
                
            }

            return marketorders;
        }
        #endregion

        /// <summary>
        /// 用户出入金
        /// </summary>
        /// <param name="remit">出入金信息</param>
        /// <returns>出入金结果</returns>
        public ResultDesc HuaXiaRemitMoney(RemitInfo remit)
        {
            //ReasonType字段说明:(该接口取4,5)
            //0----入金(金商或管理员操作);
            //1----出金(金商或管理员操作);
            //2----订单操作
            //3----库存结算(金商或管理员操作);
            //4----银行入金 (用户操作);
            //5----银行出金 (用户操作)
            //6----金额调整(金商或管理员操作)
            //7----在线回购(用户操作);
            //8----其他费用;

            ResultDesc rsdc = new ResultDesc();
            string userid = remit.userid; //用户ID
            string operUser = string.Empty; //操作人
            bool IsOpenOtherBank = false;//他行开户 入金 只做入金登记
            try
            {
                #region 判断用户ID是否为空

                if (string.IsNullOrEmpty(userid))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户ID不能为空";
                    return rsdc;
                }
                #endregion

                if (remit.ReasonType != 4 && remit.ReasonType != 5)
                {
                    rsdc.Result = false;
                    rsdc.Desc = "原因类型错误";
                    return rsdc;
                }

                operUser = ComFunction.GetTradeAccount(userid);
                if (Math.Abs(remit.Money) <= ComFunction.dzero)
                {
                    rsdc.Result = false;
                    rsdc.Desc = "金额不能为0";
                    return rsdc;
                }

                if (5 == remit.ReasonType && remit.Money > ComFunction.dzero) //出金时,金额为负数
                {
                    remit.Money = -remit.Money;
                }
                //获取现有资金信息 ConBankType='1'表示签约行是华夏银行
                Fundinfo Fdinfo = ComFunction.GetFdinfo(string.Format("select * from Trade_FundInfo where userid='{0}' and state='2' and ConBankType='1'", userid));
                if (string.IsNullOrEmpty(Fdinfo.CashUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "未签约银行,不能出入金";
                    return rsdc;
                }
                #region 出入金操作
                string codeDesc = string.Empty;
                if (4 == remit.ReasonType) //入金,走银行接口
                {
                    if ("1" == Fdinfo.ConBankType) //华夏银行入金
                    {
                        if (Fdinfo.SameBank)
                        {
                            if (!ComFunction.RuJinHuaxiaBank(Fdinfo, Math.Abs(remit.Money), remit.PasswordChar, ref codeDesc))
                            {
                                rsdc.Result = false;
                                rsdc.Desc = string.Format("签约华夏银行,开户华夏银行,入金失败,华夏银行返回信息:{0}", codeDesc);
                                return rsdc;
                            }
                        }
                        else //入金登记
                        {
                            if (!ComFunction.RuJinDengJiHuaxiaBank(remit, Fdinfo, ref codeDesc))
                            {
                                rsdc.Result = false;
                                rsdc.Desc = string.Format("签约华夏银行,开户他行,入金登记失败,华夏银行返回信息:{0}", codeDesc);
                                return rsdc;
                            }
                            IsOpenOtherBank = true;
                        }
                    }
                    else
                    {
                        rsdc.Result = false;
                        rsdc.Desc = "签约银行不是华夏银行";
                        return rsdc;
                    }

                }
                else if (5 == remit.ReasonType) //出金,走银行接口
                {
                    //判断用户是否存在有效订单 有效挂单 ,如果存在不允许出金
                    if (ComFunction.UserExistOrderAndHoldOrder(userid))
                    {
                        rsdc.Result = false;
                        rsdc.Desc = "用户存在有效订单或挂单,不能出金";
                        return rsdc;
                    }
                    //判断出金金额是否小于等于帐户结余
                    if (!(Math.Abs(remit.Money) - Fdinfo.Money <= ComFunction.dzero))
                    {
                        rsdc.Result = false;
                        rsdc.Desc = "金额大于帐户结余,不能出金";
                        return rsdc;
                    }
                    if ("1" == Fdinfo.ConBankType) //华夏银行出金,可以立即出金,可能不会实时到帐
                    {
                        if (!ComFunction.ChuJinHuaxiaBank(Fdinfo, Math.Abs(remit.Money), ref codeDesc))
                        {
                            rsdc.Result = false;
                            rsdc.Desc = string.Format("华夏银行出金失败,华夏银行返回信息:{0}", codeDesc);
                            return rsdc;
                        }
                    }
                    else
                    {
                        rsdc.Result = false;
                        rsdc.Desc = "签约银行不是华夏银行";
                        return rsdc;
                    }
                }
                    
                #endregion

                #region 数据库事务处理
                if (!IsOpenOtherBank) // 开户行不是其他银行 而是华夏银行时 需要执行
                {
                    //SQL语句
                    List<string> sqlList = new List<string>();
                    string sql1 = string.Format("update Trade_FundInfo set money=money+{0} where userid='{1}' and [state]<>'4' and tanuser={2} and subuser='{3}'", remit.Money, userid, Fdinfo.TanUser, Fdinfo.SubUser);
                    //调整资金sql语句
                    sqlList.Add(sql1);

                    string sql2 = string.Format("insert into Fund_Change([userId],[reason],[Oldvalue],[NewValue],[OperUser],[OperTime],[RelaOrder],[ChangeValue],[CashUser]) values('{0}','{1}',{2},{3},'{4}','{5}','{6}',{7},'{8}')", userid, remit.ReasonType, Fdinfo.Money, Fdinfo.Money + remit.Money, operUser, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), string.Empty, remit.Money, Fdinfo.CashUser);
                    //添加资金变动记录sql语句
                    sqlList.Add(sql2);

                    if (!ComFunction.SqlTransaction(sqlList))
                    {
                        LogNet4.WriteMsg(string.Format("华夏银行出入金成功，但是SQL语句执行失败,SQL语句是:{0}\n{1}", sql1, sql2));
                    }
                }
                else
                {
                    List<string> SqlList = new List<string>();
                    DateTime dt = DateTime.Now;
                    string insertsql = string.Format("insert into Trade_InMoneyApply([Dtime],[applyid],[MerTxSerNo],[TrnxCode],[AccountNo],[MerAccountNo],[Amt],[InOutStart],[PersonName],[AmoutDate],[BankName],[OutAccount],[state]) values('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}')",
                        dt.ToString("yyyy-MM-dd HH:mm:ss"),dt.ToString("yyyyMMddHHmmssfff"), Fdinfo.CashUser, "DZ022", Fdinfo.SubUser, Fdinfo.TanUser, remit.Money, remit.RemitType, remit.RemitName,
                        remit.RemitTime, remit.RemitBank, remit.RemitAccount,"2");
                    SqlList.Add(insertsql);
                    if (!ComFunction.SqlTransaction(SqlList))
                    {
                        LogNet4.WriteMsg(string.Format("华夏银行，入金登记申请成功，但是SQL语句执行失败,SQL语句是:{0}", insertsql));
                    }
                }
                rsdc.Result = true;
                rsdc.Desc = (4 == remit.ReasonType) ? "入金成功" : "出金成功";
                #endregion
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                
                rsdc.Result = false;
                rsdc.Desc = (4 == remit.ReasonType) ? "入金失败" : "出金失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 华夏银行签约,本行开户
        /// </summary>
        /// <param name="userid">用户标识</param>
        /// <param name="bankcard">华夏卡号</param>
        /// <returns>签约结果</returns>
        public ResultDesc ContactToHuaxiaSelfBank(string userid,string bankcard)
        {
            ResultDesc rsdc = new ResultDesc();
            TradeUser TdUser = new TradeUser();
            try
            {
                #region 判断用户ID是否为空

                if (string.IsNullOrEmpty(userid))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户ID不能为空";
                    return rsdc;
                }
                #endregion
                TdUser = ComFunction.GetTradeUserByUserid(userid);
                //Fundinfo Fdinfo = ComFunction.GetFdinfo(string.Format("select * from Trade_FundInfo where userid='{0}' and state<>'4'", userid));
                string codeDesc = string.Empty;
                //调用华夏银行子帐户同步接口
                if (!ComFunction.ContactToHuaxiaSelfBank(TdUser, ref codeDesc))
                {
                    rsdc.Result = false;
                    rsdc.Desc = string.Format("华夏银行子帐户同步失败(交易市场发起),华夏返回消息：{0}", codeDesc);
                    return rsdc;
                }

                //SQL语句
                List<string> sqlList = new List<string>();

                //0代表未签约 1表示审核中 2表示签约成功 3 表示失败 4表示已解约//同步成功以后,更新签约状态 为 审核中
                string sql1 = string.Format("update Trade_FundInfo set state='{0}',ConBankType='1',ConTime='{1}',BankAccount='{2}',AccountName='{3}',BankCard='{4}',OpenBank='{5}',OpenBankAddress='{6}',samebank={9} where userid='{7}' and state<>'{8}'",
                    1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), TdUser.BankAccount, TdUser.AccountName, bankcard, TdUser.OpenBank, TdUser.OpenBankAddress, userid, 4, 1);
                
                sqlList.Add(sql1);

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    LogNet4.WriteMsg(string.Format("华夏银行子帐户同步成功(交易市场发起)，但是SQL语句执行失败,SQL语句是:{0}",sql1));
                }
                rsdc.Result = true;
                rsdc.Desc = "签约已受理,请及时去华夏银行柜台办理签约业务";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "签约已受理失败,请重新签约";
            }
            return rsdc;
 
        }

        /// <summary>
        /// 华夏银行签约,他行开户
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="OpenBank">他行开户信息</param>
        /// <returns>签约结果</returns>
        public ResultDesc ContactToHuaxiaOhterBank(string userid, OpenBankInfo OpenBank)
        {
            ResultDesc rsdc = new ResultDesc();

            TradeUser TdUser = new TradeUser();
            try
            {
                #region 判断用户ID是否为空
                
                if(string.IsNullOrEmpty(userid))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户ID不能为空";
                    return rsdc;
                }
                #endregion
                TdUser = ComFunction.GetTradeUserByUserid(userid);
                //Fundinfo Fdinfo = ComFunction.GetFdinfo(string.Format("select * from Trade_FundInfo where userid='{0}' and state<>'4'", userid));
                string codeDesc = string.Empty;
                string subUser = string.Empty;
                //调用华夏银行子帐户同步接口
                if (!ComFunction.ContactToHuaxiaOhterBank(TdUser, OpenBank, ref codeDesc, ref subUser))
                {
                    rsdc.Result = false;
                    rsdc.Desc = string.Format("华夏银行签约,他行开户时，失败,华夏返回消息：{0}", codeDesc);
                    return rsdc;
                }

                //SQL语句
                List<string> sqlList = new List<string>();
                string sql1 = string.Format("update Trade_FundInfo set state='{0}',ConBankType='1',ConTime='{1}',BankAccount='{2}',AccountName='{3}',BankCard='{4}',OpenBank='{5}',OpenBankAddress='{6}',subUser='{9}' where userid='{7}' and state<>'{8}'",
                    2, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), OpenBank.BankAccount, OpenBank.AccountName, OpenBank.BankCard, OpenBank.OpenBank, OpenBank.OpenBankAddress, userid, 4, subUser);
                sqlList.Add(sql1);//0代表未签约 1表示审核中 2表示签约成功 3 表示失败 4表示已解约

                if (!ComFunction.SqlTransaction(sqlList))
                {
                     LogNet4.WriteMsg(string.Format("华夏银行签约,他行开户成功，但是SQL语句执行失败,SQL语句是:{0}",sql1));
                }
                rsdc.Result = true;
                rsdc.Desc = "华夏银行签约,他行开户时,成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "华夏银行签约,他行开户时,失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 华夏银行解约
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>解约结果</returns>
        public ResultDesc RescindHuaxiaBank(string userid)
        {
            ResultDesc rsdc = new ResultDesc();
 
            try
            { 
                #region 判断用户ID是否为空
                
                if(string.IsNullOrEmpty(userid))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户ID不能为空";
                    return rsdc;
                }
                #endregion

                Fundinfo Fdinfo = ComFunction.GetFdinfo(string.Format("select * from Trade_FundInfo where userid='{0}' and state<>'4'", userid));
                string codeDesc = string.Empty;
                //调用华夏银行解约接口
                if (!ComFunction.RescindHuaxiaBank(Fdinfo, ref codeDesc))
                {
                    rsdc.Result = false;
                    rsdc.Desc = string.Format("华夏银行解约失败,华夏返回消息：{0}", codeDesc);
                    return rsdc;
                }

                //SQL语句
                List<string> sqlList = new List<string>();
                string sql1 = string.Format("update Trade_FundInfo set state='{0}',RescindTime='{1}' where userid='{2}' and state<>'{3}'",
                    4, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), userid, 4);
                //解约成功以后,更新签约状态 为 已经解约 ; 并使用原来的用户ID新生成1条未签约的摊位号,使用户可以签约
                sqlList.Add(sql1);//0代表未签约 1表示审核中 2表示签约成功 3 表示失败 4表示已解约
                string sql2 = string.Format("insert into Trade_FundInfo([userId],[state],[money],[OccMoney],[frozenMoney],[BankAccount],[AccountName],[BankCard],[CashUser],[SubUser],[ConBankType],[OpenBank],[OpenBankAddress],[ConTime],[RescindTime],[SameBank]) values('{0}','{1}',{2},{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',{15})", userid, 0, 0, 0, 0, string.Empty, string.Empty, string.Empty, System.Guid.NewGuid().ToString().Replace("-", ""), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0);
                sqlList.Add(sql2);

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    LogNet4.WriteMsg(string.Format("华夏银行解约成功，但是SQL语句执行失败,SQL语句是:{0}\n{1}", sql1, sql2));
                }
                rsdc.Result = true;
                rsdc.Desc = "华夏银行解约,成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "华夏银行解约,失败";
                
            }
            return rsdc;
        }

        /// <summary>
        /// 测试获取实时行情价格
        /// </summary>
        /// <param name="pcode">行情编码</param>
        /// <param name="x">测试错误输入0</param>
        /// <returns>实时价格</returns>
        public double TestGetRealPrice(string pcode,int x)
        {
            double y = 0;
            try
            {
                y = ComFunction.GetRealPrice(pcode);
                int z = 100 / x;
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
            }
            return y;
        }

        /// <summary>
        /// 注册模拟账户
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="TradeAccount">交易账号</param>
        /// <param name="TradePwd">交易密码</param>
        /// <param name="PhoneNum">电话号码</param>
        /// <param name="Email">邮箱 </param>
        /// <param name="CardNum">证件号码</param>
        /// <returns>注册结果</returns>
        public ResultDesc RegTestTradeUser(string UserName, string TradeAccount, string TradePwd, string PhoneNum, string Email,string CardNum)
        {
            ResultDesc rsdc = new ResultDesc();
            try
            {
                //判断是否允许注册模拟账号
                if ("1" != ComFunction.AllowRegister)
                {
                    rsdc.Result = false;
                    rsdc.ReturnCode = ResCode.UL038;
                    rsdc.Desc = ResCode.UL038Desc;
                    return rsdc;
                }

                #region 输入信息判断
                if (string.IsNullOrEmpty(UserName))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户名称不能为空";
                    return rsdc;
                }

                if (string.IsNullOrEmpty(TradeAccount))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户账号不能为空";
                    return rsdc;
                }
                if (string.IsNullOrEmpty(TradePwd))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "用户密码不能为空";
                    return rsdc;
                }
                if (string.IsNullOrEmpty(PhoneNum))
                {
                    //rsdc.Result = false;
                    //rsdc.Desc = "手机号码不能为空";
                    //return rsdc;
                    PhoneNum = string.Empty;
                }
                if (string.IsNullOrEmpty(Email))
                {
                    //rsdc.Result = false;
                    //rsdc.Desc = "邮箱不能为空";
                    //return rsdc;
                    Email = string.Empty;
                }
                if (string.IsNullOrEmpty(CardNum))
                {
                    //rsdc.Result = false;
                    //rsdc.Desc = "证件号码不能为空";
                    //return rsdc;
                    CardNum = string.Empty;
                }
                #endregion

                bool tradeAccountExist = false;
                //判断用户账号 或 证件号码是否已经被使用
                //if (ComFunction.CardNumIsExist(TradeAccount, CardNum, ref tradeAccountExist))
                //{
                //    if (!string.IsNullOrEmpty(CardNum)) //证件号码不为空时，才做检查
                //    {
                //        rsdc.Result = false;
                //        rsdc.ReturnCode = ResCode.UL031;
                //        rsdc.Desc = ResCode.UL031Desc;
                //        return rsdc;
                //    }
                //}
                tradeAccountExist = ComFunction.TradeAccountExist(TradeAccount);
                if (tradeAccountExist)
                {
                    rsdc.Result = false;
                    rsdc.ReturnCode = ResCode.UL030;
                    rsdc.Desc = ResCode.UL030Desc;
                    return rsdc;
                }

                //SQL语句
                List<string> sqlList = new List<string>();

                StringBuilder strbld = new StringBuilder();
                string userid = System.Guid.NewGuid().ToString().Replace("-", "");
                string strdt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //构造新增基本用户信息的sql语句
                strbld.Append("insert into Base_User([userId],[userName],[status],[Accounttype],[CorporationName],[Account],[LoginPwd],[cashPwd],[CardType],[CardNum],[OrgId],[PhoneNum],[TelNum],[Email],[LinkMan],[LinkAdress],[OrderPhone],[sex],[OpenMan],[OpenTime],[LastUpdateTime],[LastUpdateID],[LoginID],[Ip],[Mac],[LastLoginTime],[Online],[MinTrade],[OrderUnit],[MaxTrade],[PermitRcash],[PermitCcash],[PermitDhuo],[PermitHshou],[PermitRstore],[PermitDelOrder],[usertype],[remark])");
                strbld.AppendFormat(" values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',", userid, UserName, 1, 0, string.Empty, TradeAccount, Des3.Des3EncodeCBC(TradePwd), Des3.Des3EncodeCBC(TradePwd));
                strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}',", 1, CardNum, string.Empty, PhoneNum, string.Empty, Email, string.Empty);
                strbld.AppendFormat("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',", string.Empty, string.Empty, string.Empty, string.Empty, strdt, strdt, string.Empty, string.Empty);
                strbld.AppendFormat("'{0}','{1}','{2}',{3},{4},{5},{6},", string.Empty, string.Empty, string.Empty, 0, 0.5, 0.5, 50);
                strbld.AppendFormat("{0},{1},{2},{3},{4},{5},{6},'{7}')", 1, 1, 1, 1, 1, 1,3,"客户端注册用户");
                sqlList.Add(strbld.ToString());

                //构造新增用户资金信息的sql语句
                sqlList.Add(string.Format("insert into Trade_FundInfo([userId],[state],[money],[OccMoney],[frozenMoney],[BankAccount],[AccountName],[BankCard],[CashUser],[SubUser],[ConBankType],[OpenBank],[OpenBankAddress],[SameBank]) values('{0}','{1}',{2},{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',{13})", 
                    userid, 0, 1000000, 0, 0, string.Empty, string.Empty, string.Empty, System.Guid.NewGuid().ToString().Replace("-", ""), string.Empty, string.Empty, string.Empty, string.Empty, 0));
                //sqlList.Add(string.Format("insert into Stock_BZJ(StockID,UserId) values('{0}','{1}')", System.Guid.NewGuid().ToString().Replace("-", ""), userid));
                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "注册模拟账户出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "注册模拟账户成功";

            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "注册模拟账户失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 获取交易设置信息
        /// </summary>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>交易设置信息</returns>
        public TradeSetInfo GetTradeSetInfo(string LoginId)
        {
            TradeSetInfo tradeSetInfo = new TradeSetInfo();
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    tradeSetInfo.Result = false;
                    tradeSetInfo.ReturnCode = ResCode.UL003;
                    tradeSetInfo.Desc = ResCode.UL003Desc;
                    return tradeSetInfo;
                }
                //if (UserType.NormalType == TdUser.UType)
                //{
                //    tradeSetInfo.Result = false;
                //    tradeSetInfo.Desc = ComFunction.NotRightUser;
                //    return tradeSetInfo;
                //}
                #endregion
                tradeSetInfo.TdSetList = ComFunction.GetTradeSetInfo("select * from Trade_Set");
                tradeSetInfo.Result = true;
                tradeSetInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                tradeSetInfo.Result = false;
                tradeSetInfo.Desc = "查询交易设置失败";
            }
            return tradeSetInfo;
        }

        /// <summary>
        /// 新闻公告查询
        /// </summary>
        /// <param name="LoginId">登陆标识</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="NType">新闻类型</param>
        /// <returns>新闻信息</returns>
        public TradeNewsInfo GetTradeNewsInfo(string LoginId, DateTime starttime, DateTime endtime, NewsType NType)
        {
            TradeNewsInfo tradeNewsInfo = new TradeNewsInfo();
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(LoginId, ref TdUser))
                {
                    tradeNewsInfo.Result = false;
                    tradeNewsInfo.ReturnCode = ResCode.UL003;
                    tradeNewsInfo.Desc = ResCode.UL003Desc;
                    return tradeNewsInfo;
                }

                #endregion
                string newsType = string.Empty;
                if(NType!= NewsType.ALL)
                {
                    newsType = string.Format(" and NewsType={0}",(int)NType);
                }
                string status = string.Empty;
                if (UserType.NormalType == TdUser.UType) //客户端只返回启用的新闻
                {
                    status = string.Format(" and Status=1");
                }
                string sql = string.Format("select * from Trade_News where PubTime>='{0}' and PubTime<='{1}' {2} {3}", starttime.ToString("yyyy-MM-dd"),
                    endtime.ToString("yyyy-MM-dd"), newsType, status);
                tradeNewsInfo.TradeNewsInfoList = ComFunction.GetTradeNewsInfo(sql);
                tradeNewsInfo.Result = true;
                tradeNewsInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                tradeNewsInfo.Result = false;
                tradeNewsInfo.Desc = "查询失败";
            }
            return tradeNewsInfo;
        }

        /// <summary>
        /// 新闻公告查询
        /// </summary>
        /// <param name="lqc">新闻查询条件</param>
        /// <param name="pageindex">第几页,从1开始</param>
        /// <param name="pagesize">每页多少条</param>
        /// <param name="page">输出参数(总页数)</param>
        /// <returns>新闻信息</returns>
        public TradeNewsInfo GetTradeNewsInfoWithPage(NewsLqc lqc, int pageindex, int pagesize, ref int page)
        {
            TradeNewsInfo tradeNewsInfo = new TradeNewsInfo();
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            tradeNewsInfo.TradeNewsInfoList = new List<TradeNews>();
            try
            {
                TradeUser TdUser = new TradeUser();
                #region 判断登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(lqc.LoginID, ref TdUser))
                {
                    tradeNewsInfo.Result = false;
                    tradeNewsInfo.ReturnCode = ResCode.UL003;
                    tradeNewsInfo.Desc = ResCode.UL003Desc;
                    return tradeNewsInfo;
                }

                #endregion
                string newsType = string.Empty;
                if (lqc.NType != NewsType.ALL)
                {
                    newsType = string.Format(" and NewsType={0}", (int)lqc.NType);
                }
                string status = string.Empty;
                if (UserType.NormalType == TdUser.UType) //客户端只返回启用的新闻
                {
                    status = string.Format(" and Status=1");
                }

                string sqlcondition = string.Format("where PubTime>='{0}' and PubTime<='{1}' {2} {3}", lqc.StartTime.ToString("yyyy-MM-dd"),
                     lqc.EndTime.ToString("yyyy-MM-dd"), newsType, status);
                sqlconn = new SqlConnection(ComFunction.SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "GetRecordFromPage";
                sqlcmd.Parameters.Add("@selectlist", SqlDbType.VarChar);//选择字段列表
                sqlcmd.Parameters.Add("@SubSelectList", SqlDbType.VarChar); //内部子查询字段列表
                sqlcmd.Parameters.Add("@TableSource", SqlDbType.VarChar); //表名或视图表 
                sqlcmd.Parameters.Add("@TableOrder", SqlDbType.VarChar); //排序后的表名称 即子查询结果集的别名
                sqlcmd.Parameters.Add("@SearchCondition", SqlDbType.VarChar); //查询条件
                sqlcmd.Parameters.Add("@OrderExpression", SqlDbType.VarChar); //排序 表达式
                sqlcmd.Parameters.Add("@PageIndex", SqlDbType.Int);
                sqlcmd.Parameters.Add("@PageSize", SqlDbType.Int);
                sqlcmd.Parameters.Add("@PageCount", SqlDbType.Int);

                sqlcmd.Parameters["@SubSelectList"].Value = "ID,NewsTitle,NewsContent,PubPerson,PubTime,Status,NewsType,OverView ";
                sqlcmd.Parameters["@selectlist"].Value = "ID,NewsTitle,NewsContent,PubPerson,PubTime,Status,NewsType,OverView ";

                sqlcmd.Parameters["@TableSource"].Value = "Trade_News";

                sqlcmd.Parameters["@TableOrder"].Value = "a";//取L_Trade_Order的别名
                sqlcmd.Parameters["@SearchCondition"].Value = sqlcondition;

                sqlcmd.Parameters["@OrderExpression"].Value = "order by PubTime desc";
                sqlcmd.Parameters["@PageIndex"].Value = pageindex;
                sqlcmd.Parameters["@PageSize"].Value = pagesize;
                sqlcmd.Parameters["@PageCount"].Direction = ParameterDirection.Output;

                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    TradeNews TdNews = new TradeNews();
                    TdNews.ID = sqldr["ID"].ToString();
                    TdNews.NewsTitle = sqldr["NewsTitle"].ToString();
                    TdNews.NewsContent = sqldr["NewsContent"].ToString();
                    TdNews.PubPerson = sqldr["PubPerson"].ToString();
                    TdNews.PubTime = Convert.ToDateTime(sqldr["PubTime"]);
                    TdNews.Status = (NewsStatus)sqldr["Status"];
                    TdNews.NType = (NewsType)sqldr["NewsType"];
                    TdNews.OverView = System.DBNull.Value != sqldr["OverView"] ? sqldr["OverView"].ToString() : string.Empty;
                    tradeNewsInfo.TradeNewsInfoList.Add(TdNews);
                }
                sqlconn.Close();
                page = Convert.ToInt32(sqlcmd.Parameters["@PageCount"].Value);

                tradeNewsInfo.Result = true;
                tradeNewsInfo.Desc = "查询成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (null != tradeNewsInfo.TradeNewsInfoList && tradeNewsInfo.TradeNewsInfoList.Count > 0)
                {
                    tradeNewsInfo.TradeNewsInfoList.Clear();
                }
                tradeNewsInfo.Result = false;
                tradeNewsInfo.Desc = "查询失败";
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
            return tradeNewsInfo;
        }

        /// <summary>
        /// 获取历史数据
        /// </summary>
        /// <param name="pricecode">行情编码</param>
        /// <param name="weekflg">周期（M1,M5,M15,...MN）</param>
        /// <returns></returns>
        public List<HisData> GetHisData(string pricecode, string weekflg)
        {
            List<HisData> list = new List<HisData>();

            System.Data.Common.DbDataReader dbreader = null;

            try
            {
                string sql = string.Format("select top 100 weektime, openprice, highprice, lowprice, closeprice, volnum from data_{0} where pricecode='{1}' order by weektime desc",
                        weekflg, pricecode);
                dbreader = DbHelper2.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    HisData hisdata = new HisData();
                    hisdata.weektime = System.DBNull.Value != dbreader["weektime"] ? dbreader["weektime"].ToString() : string.Empty;
                    hisdata.openprice = System.DBNull.Value != dbreader["openprice"] ? dbreader["openprice"].ToString() : string.Empty;
                    hisdata.highprice = System.DBNull.Value != dbreader["highprice"] ? dbreader["highprice"].ToString() : string.Empty;
                    hisdata.lowprice = System.DBNull.Value != dbreader["lowprice"] ? dbreader["lowprice"].ToString() : string.Empty;
                    hisdata.closeprice = System.DBNull.Value != dbreader["closeprice"] ? dbreader["closeprice"].ToString() : string.Empty;
                    hisdata.volnum = System.DBNull.Value != dbreader["volnum"] ? dbreader["volnum"].ToString() : string.Empty;
                    list.Add(hisdata);
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
        /// 获取历史数据
        /// </summary>
        /// <param name="pricecode">行情编码</param>
        /// <param name="weekflg">周期（M1,M5,M15,...MN）</param>
        /// <param name="weektime">时间数字串</param>
        /// <returns></returns>
        public List<HisData> GetHisDataWithTime(string pricecode, string weekflg,string weektime)
        {
            List<HisData> list = new List<HisData>();

            System.Data.Common.DbDataReader dbreader = null;

            try
            {
                string weektimecondition = string.Empty;

                if (!string.IsNullOrEmpty(weektime))
                {
                    weektimecondition = string.Format(" and weektime>={0}",weektime);
                }
                string sql = string.Format("select top 1440 weektime, openprice, highprice, lowprice, closeprice, volnum from data_{0} where pricecode='{1}' {2} order by weektime desc",
                        weekflg, pricecode, weektimecondition);
                dbreader = DbHelper2.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    HisData hisdata = new HisData();
                    hisdata.weektime = System.DBNull.Value != dbreader["weektime"] ? dbreader["weektime"].ToString() : string.Empty;
                    hisdata.openprice = System.DBNull.Value != dbreader["openprice"] ? dbreader["openprice"].ToString() : string.Empty;
                    hisdata.highprice = System.DBNull.Value != dbreader["highprice"] ? dbreader["highprice"].ToString() : string.Empty;
                    hisdata.lowprice = System.DBNull.Value != dbreader["lowprice"] ? dbreader["lowprice"].ToString() : string.Empty;
                    hisdata.closeprice = System.DBNull.Value != dbreader["closeprice"] ? dbreader["closeprice"].ToString() : string.Empty;
                    hisdata.volnum = System.DBNull.Value != dbreader["volnum"] ? dbreader["volnum"].ToString() : string.Empty;
                    list.Add(hisdata);
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
        /// 获取历史数据
        /// </summary>
        /// <param name="pricecode">行情编码</param>
        /// <param name="weekflg">周期（M1,M5,M15,...MN）</param>
        /// <param name="weektime">时间数字串</param>
        /// <param name="mdtime">修改时间数字字符串</param>
        /// <returns></returns>
        public ComHisData GetHisDataWithMdtime(string pricecode, string weekflg, string weektime, DateTime mdtime)
        {
            ComHisData comhisdata = new ComHisData();
            comhisdata.HisDataList = new List<HisData>();

            System.Data.Common.DbDataReader dbreader = null;

            try
            {
                DateTime nearmdtime = ComFunction.GetMdTime(weekflg, pricecode);
                comhisdata.mdtime = nearmdtime;

                string nearmdtimecondition = string.Empty;

                if (nearmdtime > mdtime)//说明数据被修改过
                {
                    nearmdtimecondition = string.Format(" or (pricecode='{0}' and mdtime>'{1}')",pricecode,mdtime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                }
                string weektimecondition = string.Empty;

                if (!string.IsNullOrEmpty(weektime))
                {
                    weektimecondition = string.Format(" and weektime>={0}", weektime);
                }
                string sql = string.Format("select top 1440 weektime, openprice, highprice, lowprice, closeprice, volnum,mdtime from data_{0} where (pricecode='{1}' {2}) {3} order by weektime desc",
                        weekflg, pricecode, weektimecondition, nearmdtimecondition);
                dbreader = DbHelper2.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    HisData hisdata = new HisData();
                    hisdata.weektime = System.DBNull.Value != dbreader["weektime"] ? dbreader["weektime"].ToString() : string.Empty;
                    hisdata.openprice = System.DBNull.Value != dbreader["openprice"] ? dbreader["openprice"].ToString() : string.Empty;
                    hisdata.highprice = System.DBNull.Value != dbreader["highprice"] ? dbreader["highprice"].ToString() : string.Empty;
                    hisdata.lowprice = System.DBNull.Value != dbreader["lowprice"] ? dbreader["lowprice"].ToString() : string.Empty;
                    hisdata.closeprice = System.DBNull.Value != dbreader["closeprice"] ? dbreader["closeprice"].ToString() : string.Empty;
                    hisdata.volnum = System.DBNull.Value != dbreader["volnum"] ? dbreader["volnum"].ToString() : string.Empty;
                    hisdata.mdtime = System.DBNull.Value != dbreader["volnum"] ? Convert.ToDateTime(dbreader["mdtime"]) : new DateTime(1970,1,1);
                    comhisdata.HisDataList.Add(hisdata);
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (null != comhisdata.HisDataList && comhisdata.HisDataList.Count > 0)
                {
                    comhisdata.HisDataList.Clear();
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
            return comhisdata;
        }



        /// <summary>
        /// 修改历史数据
        /// </summary>
        /// <param name="TdUser"></param>
        /// <param name="pricecode"></param>
        /// <param name="weekflg"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        public ResultDesc ModifyHisData(HisData hisdata,string pricecode,string weekflg)
        {
            ResultDesc rsdc = new ResultDesc();
            try
            {

                if (string.IsNullOrEmpty(hisdata.weektime)
                    || string.IsNullOrEmpty(hisdata.openprice)
                    || string.IsNullOrEmpty(hisdata.highprice)
                    || string.IsNullOrEmpty(hisdata.lowprice)
                    || string.IsNullOrEmpty(hisdata.closeprice))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "有空数据，请检查";
                    return rsdc;
                }
                
                //SQL语句
                List<string> sqlList = new List<string>();
                string mdtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //构造修改用户信息的sql语句
                StringBuilder strbld = new StringBuilder();
                strbld.AppendFormat("update data_{0} set ",weekflg);

                strbld.AppendFormat("openprice='{0}',", hisdata.openprice);

                strbld.AppendFormat("highprice='{0}',", hisdata.highprice);
                strbld.AppendFormat("lowprice='{0}',", hisdata.lowprice);
                strbld.AppendFormat("closeprice='{0}',", hisdata.closeprice);
                strbld.AppendFormat("mdtime='{0}'", mdtime);
             
                strbld.AppendFormat(" where pricecode='{0}' and  weektime={1} ",pricecode,hisdata.weektime);
                sqlList.Add(strbld.ToString());
      
                sqlList.Add(string.Format(@"if not exists (select mdtime from Data_Stamp where pricecode='{0}' and weekflg='{1}')
                            insert into Data_Stamp(pricecode, weekflg,mdtime) values('{0}','{1}','{2}')
                            else update Data_Stamp set mdtime = '{2}' where pricecode='{0}' and weekflg='{1}'", pricecode, weekflg, mdtime));

                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改历史数据出错";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "修改历史数据成功";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "修改历史数据失败";
            }
            return rsdc;
        }

        /// <summary>
        /// 分页获取历史数据
        /// </summary>
        /// <param name="HisCon">查询条件</param>
        /// <param name="pageindex">第几页,从1开始</param>
        /// <param name="pagesize">每页多少条</param>
        /// <param name="page">输出参数(总页数)</param>
        /// <returns></returns>
        public List<HisData> GetHisDataWithPage(HisQueryCon HisCon, int pageindex, int pagesize, ref int page)
        {
            SqlConnection sqlconn = null;
            SqlCommand sqlcmd = null;
            SqlDataReader sqldr = null;
            List<HisData> list = new List<HisData>();
            try
            {
                string weektime = string.Empty;
                if(!string.IsNullOrEmpty(HisCon.weektime))
                {
                    weektime = string.Format(" and weektime={0}", HisCon.weektime);
                }
                string sqlcondition = string.Format("where pricecode='{0}' {3} and weektime>={1} and weektime<={2} ", 
                    HisCon.pricecode,HisCon.StartTime.ToString("yyyyMMdd0000"),HisCon.EndTime.ToString("yyyyMMdd0000"),weektime);
                sqlconn = new SqlConnection(ComFunction.SqlConnectionString);
                sqlconn.Open();
                sqlcmd = sqlconn.CreateCommand();
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "GetRecordFromPage";
                sqlcmd.Parameters.Add("@selectlist", SqlDbType.VarChar);//选择字段列表
                sqlcmd.Parameters.Add("@SubSelectList", SqlDbType.VarChar); //内部子查询字段列表
                sqlcmd.Parameters.Add("@TableSource", SqlDbType.VarChar); //表名或视图表 
                sqlcmd.Parameters.Add("@TableOrder", SqlDbType.VarChar); //排序后的表名称 即子查询结果集的别名
                sqlcmd.Parameters.Add("@SearchCondition", SqlDbType.VarChar); //查询条件
                sqlcmd.Parameters.Add("@OrderExpression", SqlDbType.VarChar); //排序 表达式
                sqlcmd.Parameters.Add("@PageIndex", SqlDbType.Int);
                sqlcmd.Parameters.Add("@PageSize", SqlDbType.Int);
                sqlcmd.Parameters.Add("@PageCount", SqlDbType.Int);

                sqlcmd.Parameters["@SubSelectList"].Value = "weektime, openprice, highprice, lowprice, closeprice, volnum ";
                sqlcmd.Parameters["@selectlist"].Value = "weektime, openprice, highprice, lowprice, closeprice, volnum ";

                sqlcmd.Parameters["@TableSource"].Value = string.Format("data_{0}", HisCon.weekflg);

                sqlcmd.Parameters["@TableOrder"].Value = "a";//取L_Trade_Order的别名
                sqlcmd.Parameters["@SearchCondition"].Value = sqlcondition;

                sqlcmd.Parameters["@OrderExpression"].Value = "order by weektime desc";
                sqlcmd.Parameters["@PageIndex"].Value = pageindex;
                sqlcmd.Parameters["@PageSize"].Value = pagesize;
                sqlcmd.Parameters["@PageCount"].Direction = ParameterDirection.Output;

                sqldr = sqlcmd.ExecuteReader();
                while (sqldr.Read())
                {
                    HisData hsd = new HisData();
                    hsd.weektime = sqldr["weektime"].ToString();
                    hsd.openprice = sqldr["openprice"].ToString();
                    hsd.highprice = sqldr["highprice"].ToString();
                    hsd.lowprice = sqldr["lowprice"].ToString();
                    hsd.closeprice = sqldr["closeprice"].ToString();
                    hsd.volnum = sqldr["volnum"].ToString();
                    list.Add(hsd);
                }
                sqlconn.Close();
                page = Convert.ToInt32(sqlcmd.Parameters["@PageCount"].Value);

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
    }
}