using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using com.individual.helper;
using WcfInterface;
using WcfInterface.model;
using WcfInterface.model.WJY;
using WcfInterface.ServiceReference1;

namespace Trade
{
    /// <summary>
    /// 用于微交易
    /// 马友春
    /// </summary>
    public partial class CTrade
    {
        #region 1下单
        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="marorderln">下单信息</param>
        /// <returns>下单结果</returns>
        public Marketorders GetWXMarketorders(MarOrdersLn marorderln, List<UserExperience>  experiences)
        {
            ComFunction.WriteErr(new Exception("1"));
            Marketorders marketorders = new Marketorders();
            marketorders.MoneyInventory = new MoneyInventory();
            marketorders.TradeOrder = new TradeOrder();
            ProductConfig ptc = new ProductConfig();
            TradeUser TdUser = new TradeUser();
            DateTime dt = DateTime.Now;//服务器当前本地时间
            double _occMoney = 0;//占用资金
            double realprice = 0;//服务器端实时价格(即此时此刻的建仓价)
            double _tradefee = 0;//工费
            double fxrate = 0;//风险率
            string orderid = string.Empty;//市价单ID
            string userId = string.Empty;
            string allowStore = "1";//是否允许入库 "1"允许入库 "0" 不允许入库
            string operUser = marorderln.TradeAccount;
            string ipmac = string.Empty;
            int operUserType = 0;
            bool isExperience=false;//是否以体验券下单
            try
            {
                if (experiences != null && experiences.Count > 0)
                    isExperience = true;

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
                if (!ComFunction.GetDateset(ptc.PriceCode, dt))
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
                ComFunction.WriteErr(new Exception("2"));
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
                realprice = Math.Round(realprice, ptc.AdjustCount, MidpointRounding.AwayFromZero);//把多余的小数位去掉
                marketorders.MoneyInventory = ComFunction.GetMoneyInventoryByUserId(userId);
               
                #region 计算占用资金和工费
                if (!marketorders.MoneyInventory.Result)
                {
                    marketorders.Result = false;
                    marketorders.Desc = "未能获取资金库存";
                    return marketorders;
                }

                //计算本次下单的占用资金,根据产品表中[保证金计算公式]进行计算
                _occMoney = Math.Round(ComFunction.Getfee(ptc.OrderMoneyFee, marorderln.OrderMoney, realprice, marorderln.Quantity), 2, MidpointRounding.AwayFromZero);
                if (!isExperience)//体验券下单不扣工费
                {
                    //计算本次下单工费,根据产品表中[交易工费公式]进行计算
                    _tradefee = Math.Round(ComFunction.Getfee(ptc.ExpressionFee, marorderln.OrderMoney, realprice, marorderln.Quantity), 2, MidpointRounding.AwayFromZero);
                }
                #endregion

                #region 新的风险率判断代码
                if (!isExperience)//体验券下单不用计算风险率
                {
                    double DongJieMoney = 0;//冻结资金
                    if (marketorders.MoneyInventory.FdInfo.DongJieMoney > ComFunction.dzero)
                    {
                        DongJieMoney = marketorders.MoneyInventory.FdInfo.DongJieMoney;
                    }
                    //客户总共的使用金额=本次下单的保证金+客户账户中的保证金+客户账户中的预付款
                    double UseMoney = _occMoney + marketorders.MoneyInventory.FdInfo.OccMoney + marketorders.MoneyInventory.FdInfo.FrozenMoney;
                    if (UseMoney <= ComFunction.dzero)//如果使用金额为0 则说明有问题 使用金额不可能小于0
                    {
                        marketorders.Result = false;
                        marketorders.Desc = "市价单下单失败!";
                        return marketorders;
                    }

                    //如果客户账户余额-客户账户冻结资金-本次下单工费-本次下单的占用资金-客户账户的保证金-客户账户的预付款<=0
                    if (marketorders.MoneyInventory.FdInfo.Money - DongJieMoney - _tradefee - _occMoney - marketorders.MoneyInventory.FdInfo.OccMoney
                        - marketorders.MoneyInventory.FdInfo.FrozenMoney <= ComFunction.dzero)
                    {
                        marketorders.Result = false;
                        marketorders.ReturnCode = ResCode.UL012;
                        marketorders.Desc = ResCode.UL012Desc;
                        return marketorders;
                    }

                    //用户的盈亏
                    double yingkui = ComFunction.GetUserYingKui(userId);
                    //风险率=（当前帐户余额-冻结资金-本次的工费+盈亏）/客户账户总共的使用金额
                    fxrate = (marketorders.MoneyInventory.FdInfo.Money - DongJieMoney - _tradefee + yingkui) / UseMoney;//分子=当前帐户余额-冻结资金-本次的工费
                    if (fxrate - ComFunction.fenxian_rate <= ComFunction.dzero)
                    {
                        marketorders.Result = false;
                        marketorders.ReturnCode = ResCode.UL013;
                        marketorders.Desc = ResCode.UL013Desc;
                        return marketorders;
                    }
                }
                #endregion
                
                #region 计算止盈止损价
                double lose = marorderln.LossPrice;//微交易下单时传送的是一个百分比
                if (lose > 0)
                {
                    //止损价
                    //买涨：止损价=实时价-（保证金*止损百分比/点值）*点差基值
                    //买跌：止损价=实时价+（保证金*止损百分比/点值）*点差基值
                    var temp = (marorderln.OrderMoney * lose / ptc.ValueDot) * ptc.AdjustBase;//保证金没有乘数量所以公式也没有数量

                    if ("0" == marorderln.OrderType)//买涨
                    {
                        marorderln.LossPrice = realprice - temp;
                    }
                    else//买跌
                    {
                        marorderln.LossPrice = realprice + temp;
                    }
                    marorderln.LossPrice = Math.Round(marorderln.LossPrice, ptc.AdjustCount);
                }
                double gain = marorderln.ProfitPrice;

                if (gain > 0)//微交易下单时传送的是一个百分比
                {
                    //止盈价
                    //买涨：止盈价=实时价+（(保证金*止盈百分比+手续费)/点值）*点差基值
                    //买跌：止盈价=实时价-（(保证金*止盈百分比+手续费)/点值）*点差基值
                    var temp = (marorderln.OrderMoney * gain / ptc.ValueDot) * ptc.AdjustBase;//保证金没有乘数量所以公式也没有数量

                    if ("0" == marorderln.OrderType)//买涨
                    {
                        marorderln.ProfitPrice = realprice + temp;
                    }
                    else//买跌
                    {
                        marorderln.ProfitPrice = realprice - temp;
                    }
                    marorderln.ProfitPrice = Math.Round(marorderln.ProfitPrice, ptc.AdjustCount);
                } 
                #endregion
                ComFunction.WriteErr(new Exception("3"));
                //下单的时候不计算仓储费,直接赋值为0
                marketorders.TradeOrder.StorageFee = 0;

                orderid = ComFunction.GetOrderId(ComFunction.Order);
                #region 数据库事务处理
                List<string> sqlList = new List<string>();
                sqlList.Add(string.Format("INSERT INTO Trade_Order([userId],[Orderid],[productCode],[Ordertype],[Orderprice],[usequantity],[quantity],[lossprice],[profitPrice]," +
                                          "[OccMoney],[tradefee],[storagefee],[Ordertime],[OperType],[ip],[mac],[AllowStore],[IsExperience]) " +
                                          "VALUES('{0}','{1}','{2}',{3},{4},{5},{6},{7},{8},{9},{10},{11},'{12}',{13},'{14}','{15}',{16},'{17}')",
                    userId, orderid, marorderln.ProductCode, marorderln.OrderType, realprice, marorderln.Quantity,
                    marorderln.Quantity, marorderln.LossPrice, marorderln.ProfitPrice, _occMoney, _tradefee, marketorders.TradeOrder.StorageFee,
                    dt.ToString("yyyy-MM-dd HH:mm:ss.fff"), 1, ComFunction.GetClientIp(), marorderln.Mac, allowStore,isExperience));
                if (!isExperience)//体验券下单不扣用户资金
                {
                    sqlList.Add(string.Format("update Trade_FundInfo set occMoney = occMoney+{0},[money]=[money]-{1} where userid='{2}' and [state]<>'4'", _occMoney, _tradefee, userId));

                    sqlList.Add(string.Format("INSERT INTO Fund_Change([userId],[reason],[Oldvalue],[NewValue],[OperUser],[OperTime],[RelaOrder],[ChangeValue],[CashUser]) VALUES('{0}','{1}',{2},{3},'{4}','{5}','{6}',{7},'{8}')",
                       userId, 10, marketorders.MoneyInventory.FdInfo.Money, marketorders.MoneyInventory.FdInfo.Money - _tradefee, operUser, dt.ToString("yyyy-MM-dd HH:mm:ss.fff"), orderid, -_tradefee, marketorders.MoneyInventory.FdInfo.CashUser));
                }

                //添加操作记录
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, operUserType, string.Format("{1}在手订单{0}", orderid, ipmac)));

                if (isExperience)
                {
                    foreach (UserExperience item in experiences)
                    {
                        sqlList.Add(string.Format("UPDATE [USER_EXPERIENCE] SET [USE_NUM] ={0}  WHERE ID={1}", item.Num, item.ID));
                    }
                }

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
                marketorders.Desc = "市价单下单失败" + ex.Message + ex.InnerException;

            }
            return marketorders;
        }
        #endregion

        #region 2平仓
        /// <summary>
        /// 市价单取消接口 结算盈亏 退还保证金(平仓)
        /// </summary>
        /// <param name="delen">市价单取消信息</param>
        /// <returns>市价单取消结果</returns>
        public Marketorders WXDelOrder(DeliveryEnter delen)
        {
            var marketorders = new Marketorders { TradeOrder = new TradeOrder(), MoneyInventory = new MoneyInventory() };
            DateTime dt = DateTime.Now;//服务器当前本地时间
            var tdUser = new TradeUser();
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
            bool IsCanOrgTrade = false;
            try
            {

                #region 判断用户登陆标识是否过期

                if (!ComFunction.ExistUserLoginID(delen.LoginID, ref tdUser))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL003;
                    marketorders.Desc = ResCode.UL003Desc;
                    return marketorders;
                }
                operUser = tdUser.Account;
                ipmac = ComFunction.GetIpMac(tdUser.Ip, tdUser.Mac);
                operUserType = (int)tdUser.UType;
                if (UserType.NormalType == tdUser.UType)
                {
                    userId = tdUser.UserID;
                }
                else
                {
                    userId = ComFunction.GetUserId(delen.TradeAccount, ref tdUser);
                }
                #endregion

                #region 交易手数验证
                if (!ComFunction.ValidateQuantity(delen.Quantity, tdUser.OrderUnit))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL044;
                    //marketorders.Desc = ResCode.UL044Desc;
                    marketorders.Desc = string.Format("交易手数必须是{0}的倍数", tdUser.OrderUnit);
                    return marketorders;
                }
                #endregion

                #region 判断用户是否允许平仓
                if (!tdUser.PermitDelOrder)
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
                ProductConfig ptc = ComFunction.GetProductInfo(marketorders.TradeOrder.ProductCode);
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
                if (!ComFunction.GetDateset(ptc.PriceCode, dt))
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
                //if (!(System.Math.Abs(delen.RtimePrices - realprice) - delen.MaxPrice * ptc.AdjustBase <= ComFunction.dzero))
                if ((System.Math.Abs(delen.RtimePrices - realprice) - delen.MaxPrice * ptc.AdjustBase > ComFunction.dzero))
                {
                    marketorders.Result = false;
                    marketorders.ReturnCode = ResCode.UL016;
                    marketorders.Desc = ResCode.UL016Desc;
                    //marketorders.Desc = delen.RtimePrices + " " + realprice + " " + delen.MaxPrice + " " + ptc.AdjustBase;
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

                realprice = Math.Round(realprice, ptc.AdjustCount, MidpointRounding.AwayFromZero);//把多余的小数位去掉
                
                #region 计算占用资金 盈亏
                _occmoney = Math.Round(marketorders.TradeOrder.OccMoney * delen.Quantity / marketorders.TradeOrder.UseQuantity, 2, MidpointRounding.AwayFromZero);
                //卖
                if ("1" == marketorders.TradeOrder.OrderType)
                {
                    //_yingkuifee =Math.Round(-(realprice - marketorders.TradeOrder.OrderPrice) / ptc.AdjustBase * ptc.ValueDot * delen.Quantity - (marketorders.TradeOrder.TradeFee * xs + marketorders.TradeOrder.StorageFee) * delen.Quantity / marketorders.TradeOrder.UseQuantity, 2, MidpointRounding.AwayFromZero);
                    _yingkuifee = Math.Round(-(realprice - marketorders.TradeOrder.OrderPrice) / ptc.AdjustBase * ptc.ValueDot * delen.Quantity, 2, MidpointRounding.AwayFromZero);

                }
                //买
                else
                {
                    //_yingkuifee =Math.Round((realprice - marketorders.TradeOrder.OrderPrice) / ptc.AdjustBase * ptc.ValueDot * delen.Quantity - (marketorders.TradeOrder.TradeFee * xs + marketorders.TradeOrder.StorageFee) * delen.Quantity / marketorders.TradeOrder.UseQuantity, 2, MidpointRounding.AwayFromZero);
                    _yingkuifee = Math.Round((realprice - marketorders.TradeOrder.OrderPrice) / ptc.AdjustBase * ptc.ValueDot * delen.Quantity, 2, MidpointRounding.AwayFromZero);
                }
                //体验券下单，用户赚钱了把钱转入用户账户，赔钱了不扣用户账户上的钱
                if (marketorders.TradeOrder.IsExperience && _yingkuifee < 0)
                    _yingkuifee = 0;


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
                double ResultOccMoney = marketorders.MoneyInventory.FdInfo.OccMoney - _occmoney;//用户账户的保证金
                if (ResultOccMoney < ComFunction.dzero)
                {
                    ResultOccMoney = 0;
                }
                if (ConfigurationManager.AppSettings["VersionFlag"] == "XS")
                {
                    double money = 0;
                    IsCanOrgTrade = ComFunction.IsCanOrgTrade(tdUser.OrgId, _yingkuifee,ref money);
                    //ComFunction.WriteErr(new Exception("IsCanOrgTrade:" + IsCanOrgTrade + "\t_yingkuifee:" + _yingkuifee + "\tOrgId:" + tdUser.OrgId + "\tmoney:" + money + "\tOrderID:" + delen.Orderid));
                    if (IsCanOrgTrade)//如果会员承接头寸
                    {
                        sqlList.Add(string.Format(@"update Trade_OrgFund set money=money - {0} where OrgID='{1}'",
                            _yingkuifee, tdUser.OrgId));
                        sqlList.Add(
                            string.Format(@"INSERT INTO [ORG_FundChange] ([orgId] ,[reason],[Oldvalue],[NewValue] ,[OperUser],[OperTime],[RelaOrder] ,
                        [ChangeValue]) VALUES ('{0}' ,'{1}' ,{2} ,{3} ,'{4}' ,'{5}' ,'{6}' ,{7})", tdUser.OrgId, "6",
                                money, money - _yingkuifee, operUser,
                                dt.ToString("yyyy-MM-dd HH:mm:ss.fff"), delen.Orderid, -_yingkuifee));
                    }
                    else//会员不承接头寸
                    {
                        sqlList.Add(string.Format(@"update Base_Org set IsTrade =0 where OrgID='{0}'",tdUser.OrgId));
                        sqlList.Add(string.Format(@"update Trade_OrgFund set money=money  -  {0} where OrgID='{1}'",
                            _yingkuifee, "system"));
                        sqlList.Add(
                            string.Format(@"INSERT INTO [ORG_FundChange] ([orgId] ,[reason],[Oldvalue],[NewValue] ,[OperUser],[OperTime],[RelaOrder] ,
                        [ChangeValue]) VALUES ('{0}' ,'{1}' ,{2} ,{3} ,'{4}' ,'{5}' ,'{6}' ,{7})", "system", "6",
                                money, money - _yingkuifee, operUser,
                                dt.ToString("yyyy-MM-dd HH:mm:ss.fff"), delen.Orderid,-_yingkuifee));
                    }
                    //string ex = "";
                    //foreach (var item in sqlList)
                    //    ex += item+"\t";
                    //ComFunction.WriteErr(new Exception(ex));
                }
                
                //体验券下单，下单时只计算所需保证金，并存入订单表，但未冻结用户账户上的保证金，故平仓时也就没有解除冻结的保证金操作
                if (marketorders.TradeOrder.IsExperience)
                {
                    if (_yingkuifee > 0)//用户此单赚钱了，则把钱转入用户账户余额，赔钱了不扣用户账户余额
                    {
                        sqlList.Add(string.Format("update Trade_FundInfo set money=money+{0} where userid='{1}' and [state]<>'4'", _yingkuifee, userId));
                        sqlList.Add(string.Format("INSERT INTO Fund_Change([userId],[reason],[Oldvalue],[NewValue],[OperUser],[OperTime],[RelaOrder],[ChangeValue],[CashUser]) VALUES('{0}','{1}',{2},{3},'{4}','{5}','{6}',{7},'{8}')",
                     userId, 11, marketorders.MoneyInventory.FdInfo.Money, marketorders.MoneyInventory.FdInfo.Money + _yingkuifee, operUser, dt.ToString("yyyy-MM-dd HH:mm:ss.fff"), delen.Orderid, _yingkuifee, marketorders.MoneyInventory.FdInfo.CashUser));
                  
                    }
                    sqlList.Add(tmpsql);  
                }
                else
                {
                    sqlList.Add(string.Format("update Trade_FundInfo set occMoney={0}, money=money+{1} where userid='{2}' and [state]<>'4'", ResultOccMoney, _yingkuifee, userId));
                    sqlList.Add(string.Format("INSERT INTO Fund_Change([userId],[reason],[Oldvalue],[NewValue],[OperUser],[OperTime],[RelaOrder],[ChangeValue],[CashUser]) VALUES('{0}','{1}',{2},{3},'{4}','{5}','{6}',{7},'{8}')",
                        userId, 11, marketorders.MoneyInventory.FdInfo.Money, marketorders.MoneyInventory.FdInfo.Money + _yingkuifee, operUser, dt.ToString("yyyy-MM-dd HH:mm:ss.fff"), delen.Orderid, _yingkuifee, marketorders.MoneyInventory.FdInfo.CashUser));
                    sqlList.Add(tmpsql);
                  
                }
                if (ConfigurationManager.AppSettings["VersionFlag"] == "XS")
                {
                 
                    sqlList.Add(string.Format("INSERT INTO L_Trade_Order([userId],[historyOrderId],[Orderid],[productCode],[OrderType],[Orderprice],[overprice],[quantity]," +
                                           "[lossprice],[profitPrice],[OccMoney],[tradefee],[storagefee],[Ordertime],[OperType],[profitValue],[Overtime],[overtype],[ip],[mac],[IsSystemPay])" +
                                           " VALUES('{0}','{1}','{2}','{3}',{4},{5},{6},{7},{8},{9},{10},{11},{12},'{13}',{14},{15},'{16}',{17},'{18}','{19}',{20})",
                     userId, ComFunction.GetOrderId(ComFunction.Order_His), marketorders.TradeOrder.OrderId, marketorders.TradeOrder.ProductCode, marketorders.TradeOrder.OrderType,
                     marketorders.TradeOrder.OrderPrice, realprice, delen.Quantity, marketorders.TradeOrder.LossPrice, marketorders.TradeOrder.ProfitPrice,
                     _occmoney, marketorders.TradeOrder.TradeFee * delen.Quantity / marketorders.TradeOrder.UseQuantity, marketorders.TradeOrder.StorageFee * delen.Quantity / marketorders.TradeOrder.UseQuantity,
                     marketorders.TradeOrder.OrderTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                     marketorders.TradeOrder.OperType, _yingkuifee, dt.ToString("yyyy-MM-dd HH:mm:ss.fff"), 1, order_ip, order_mac, Convert.ToByte(!IsCanOrgTrade)));
                }
                else
                {
                    sqlList.Add(string.Format("INSERT INTO L_Trade_Order([userId],[historyOrderId],[Orderid],[productCode],[OrderType],[Orderprice],[overprice],[quantity]," +
                                           "[lossprice],[profitPrice],[OccMoney],[tradefee],[storagefee],[Ordertime],[OperType],[profitValue],[Overtime],[overtype],[ip],[mac])" +
                                           " VALUES('{0}','{1}','{2}','{3}',{4},{5},{6},{7},{8},{9},{10},{11},{12},'{13}',{14},{15},'{16}',{17},'{18}','{19}')",
                     userId, ComFunction.GetOrderId(ComFunction.Order_His), marketorders.TradeOrder.OrderId, marketorders.TradeOrder.ProductCode, marketorders.TradeOrder.OrderType,
                     marketorders.TradeOrder.OrderPrice, realprice, delen.Quantity, marketorders.TradeOrder.LossPrice, marketorders.TradeOrder.ProfitPrice,
                     _occmoney, marketorders.TradeOrder.TradeFee * delen.Quantity / marketorders.TradeOrder.UseQuantity, marketorders.TradeOrder.StorageFee * delen.Quantity / marketorders.TradeOrder.UseQuantity,
                     marketorders.TradeOrder.OrderTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                     marketorders.TradeOrder.OperType, _yingkuifee, dt.ToString("yyyy-MM-dd HH:mm:ss.fff"), 1, order_ip, order_mac));
                }
               

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
                marketorders.TradeOrder.OccMoney = Math.Round(marketorders.TradeOrder.OccMoney * (marketorders.TradeOrder.UseQuantity - delen.Quantity) / marketorders.TradeOrder.UseQuantity, 2, MidpointRounding.AwayFromZero);
                marketorders.TradeOrder.TradeFee = Math.Round(marketorders.TradeOrder.TradeFee * (marketorders.TradeOrder.UseQuantity - delen.Quantity) / marketorders.TradeOrder.UseQuantity, 2, MidpointRounding.AwayFromZero);
                marketorders.TradeOrder.StorageFee = Math.Round(marketorders.TradeOrder.StorageFee * (marketorders.TradeOrder.UseQuantity - delen.Quantity) / marketorders.TradeOrder.UseQuantity, 2, MidpointRounding.AwayFromZero);
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

        #region 3订单历史
        /// <summary>
        /// 订单历史
        /// </summary>
        /// <param name="lqc">查询条件</param>
        /// <param name="ltype">"1"平仓历史 "2"入库历史</param>
        /// <param name="pageindex">第几页,从1开始</param>
        /// <param name="pagesize">每页多少条</param>
        /// <param name="page">输出参数(总页数)</param>
        /// <returns>市价单历史记录</returns>
        public List<LTradeOrder> GetWXLTradeOrder(LQueryCon lqc, string ltype, int pageindex, int pagesize, ref int page)
        {
            var list = new List<LTradeOrder>();
            DbDataReader dbreader = null;
            var tdUser = new TradeUser();
            DbParameter outputParam = DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
             "@PageCount", DbParameterType.String, 0, ParameterDirection.Output);
            try
            {
                string andStr = string.Empty;
                string partSearchCondition = string.Empty;
                string parentOrgID = string.Empty;
                if (!ComFunction.ExistUserLoginID(lqc.LoginID, ref tdUser))
                {
                    return list;
                }

                if (UserType.NormalType == tdUser.UType) //普通用户
                {
                    andStr += string.Format("and userid='{0}' ", tdUser.UserID);
                }
                else
                {
                    if (UserType.OrgType == tdUser.UType && !string.IsNullOrEmpty(tdUser.OrgId))
                    {
                        partSearchCondition = " and orgid in (select orgid from #tmp) ";
                        parentOrgID = tdUser.OrgId;
                    }
                    if (!string.IsNullOrEmpty(lqc.TradeAccount))
                    {
                        andStr += string.Format(" and [Account] like '{0}%' ", lqc.TradeAccount);
                    }
                }
                if (!string.IsNullOrEmpty(lqc.OrgName))
                {
                    andStr += string.Format(" and [orgname] like '{0}%' ", lqc.OrgName);
                }

                //入库单查询
                if ("2" == ltype)
                {
                    andStr += " and overtype='2'";
                }
                else
                {
                    andStr += " and overtype<>'2'";//平仓单查询
                }
                if ("ALL" != lqc.ProductName.ToUpper())
                {
                    andStr += string.Format(" and ProductName='{0}'", lqc.ProductName);
                }
                if ("ALL" != lqc.OrderType.ToUpper())
                {
                    andStr += string.Format(" and ordertype='{0}'", lqc.OrderType);
                }


                //内部子查询字段列表
                const string subSelectList = "orgname,Account,ProductName,historyOrderId,productcode,lossprice,profitPrice,Orderprice,overType,overprice,profitValue,tradefee,storagefee,Overtime,Orderid,ordertype,quantity,ordertime,adjustbase,valuedot,unit,lowerprice ";
                //选择字段列表
                const string selectlist = "orgname,Account,ProductName,historyOrderId,productcode,lossprice,profitPrice,Orderprice,overType,overprice,profitValue,tradefee,storagefee,Overtime,Orderid,ordertype,quantity,ordertime,adjustbase,valuedot,unit,lowerprice ";

                //查询条件
                string searchCondition = string.Format("where overtime >= '{0}' and overtime <='{1}' {2} {3} ",
                    lqc.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), lqc.EndTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), andStr, partSearchCondition);


                dbreader = DbHelper.RunProcedureGetDataReader("GetRecordFromPageEx",
                     new[]{
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@selectlist",DbParameterType.String,selectlist,ParameterDirection.Input),
                      DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SubSelectList",DbParameterType.String,subSelectList,ParameterDirection.Input),
                       DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableSource",DbParameterType.String,"V_L_Trade_Order",ParameterDirection.Input), //表名或视图表 
                        DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@TableOrder",DbParameterType.String,"a",ParameterDirection.Input), //排序后的表名称 即子查询结果集的别名
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@SearchCondition",DbParameterType.String,searchCondition,ParameterDirection.Input),
                          DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@OrderExpression",DbParameterType.String,"order by overtime desc",ParameterDirection.Input),//排序 表达式
                         DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@ParentOrgID",DbParameterType.String,parentOrgID,ParameterDirection.Input),//父级组织ID
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageIndex",DbParameterType.Int,pageindex,ParameterDirection.Input),
                           DbHelper.CreateDbParameter(JinTong.Jyrj.Data.DataBase.Type,
                    "@PageSize",DbParameterType.Int,pagesize,ParameterDirection.Input),
                            outputParam});
                while (dbreader.Read())
                {
                    var ltradeOrder = new LTradeOrder
                    {
                        OrgName = DBNull.Value != dbreader["orgname"] ? dbreader["orgname"].ToString() : string.Empty,
                        TradeAccount =
                            DBNull.Value != dbreader["Account"] ? dbreader["Account"].ToString() : string.Empty,
                        HistoryOrderId =
                            DBNull.Value != dbreader["historyOrderId"]
                                ? dbreader["historyOrderId"].ToString()
                                : string.Empty,
                        ProductName =
                            DBNull.Value != dbreader["ProductName"] ? dbreader["ProductName"].ToString() : string.Empty,
                        ProductCode =
                            DBNull.Value != dbreader["productcode"] ? dbreader["productcode"].ToString() : string.Empty,
                        OrderPrice =
                            DBNull.Value != dbreader["Orderprice"] ? Convert.ToDouble(dbreader["Orderprice"]) : 0,
                        OverType = DBNull.Value != dbreader["overType"] ? dbreader["overType"].ToString() : string.Empty,
                        OverPrice = DBNull.Value != dbreader["overprice"] ? Convert.ToDouble(dbreader["overprice"]) : 0,
                        ProfitValue =
                            DBNull.Value != dbreader["profitValue"] ? Convert.ToDouble(dbreader["profitValue"]) : 0,
                        TradeFee = DBNull.Value != dbreader["tradefee"] ? Convert.ToDouble(dbreader["tradefee"]) : 0,
                        StorageFee =
                            DBNull.Value != dbreader["storagefee"] ? Convert.ToDouble(dbreader["storagefee"]) : 0,
                        LossPrice = DBNull.Value != dbreader["lossprice"] ? Convert.ToDouble(dbreader["lossprice"]) : 0,
                        ProfitPrice =
                            DBNull.Value != dbreader["profitPrice"] ? Convert.ToDouble(dbreader["profitPrice"]) : 0,
                        OverTime =
                            DBNull.Value != dbreader["Overtime"]
                                ? Convert.ToDateTime(dbreader["Overtime"])
                                : DateTime.MinValue,
                        OrderId = DBNull.Value != dbreader["Orderid"] ? dbreader["Orderid"].ToString() : string.Empty,
                        OrderType =
                            DBNull.Value != dbreader["ordertype"] ? dbreader["ordertype"].ToString() : string.Empty,
                        Quantity = DBNull.Value != dbreader["quantity"] ? Convert.ToDouble(dbreader["quantity"]) : 0,
                        OrderTime =
                            DBNull.Value != dbreader["ordertime"]
                                ? Convert.ToDateTime(dbreader["ordertime"])
                                : DateTime.MinValue
                    };
                    ltradeOrder.ProductMoney = "0" == ltradeOrder.OrderType ? Math.Round(ltradeOrder.OverPrice / Convert.ToDouble(dbreader["adjustbase"]) * Convert.ToDouble(dbreader["valuedot"]) * Convert.ToDouble(dbreader["quantity"]), 2, MidpointRounding.AwayFromZero) : 0;
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
                    dbreader.Close(); dbreader.Dispose();
                    page = Convert.ToInt32(outputParam.Value);
                }
            }
            return list;
        }
        #endregion

        #region 4用户信息
        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="loginId">用户登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <returns></returns>
        public WUserInfo GetWXUserInfo(string loginId, string wUserId)
        {
            var info = new WUserInfo();
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
                info.WUser = ComFunction.GetWUser(string.Format("SELECT [WUserId],[PhoneNum],[Name],[WAccount],[PWUserId]  ,[UserID],[AccountType],[Ticket] ,[Url] " +
                                                                "FROM [Base_WUser] where WUserId={0}", wUserId));
                info.TdUser = ComFunction.GetTdUser(string.Format("SELECT * " +
                                                                "FROM [Base_User] where UserID='{0}'", ComFunction.GetWUserID(wUserId)));
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
        #endregion

        #region 5修改交易密码
        /// <summary>
        /// 修改交易密码
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <param name="newPassword">新交易密码</param>
        /// <returns>ResultDesc</returns>
        public ResultDesc ModifyTradePassword(string loginId, string wUserId, string newPassword)
        {
            var rsdc = new ResultDesc();
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
                string operUser = tdUser.Account;//操作人
                var sqlList = new List<string>();
                string uSql = string.Format(@"UPDATE [Base_User] SET CashPwd='{0}' WHERE UserID='{1}'", Des3.Des3EncodeCBC(newPassword), ComFunction.GetWUserID(wUserId));
                sqlList.Add(uSql);

                //添加操作记录
                string ipmac = ComFunction.GetIpMac(tdUser.Ip, tdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)tdUser.UType, string.Format("{0}修改微交易交易密码", ipmac)));
                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "修改交易密码出错！";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "修该交易密码成功！";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "";
            }
            return rsdc;
        }
        #endregion

        #region 6商品配置数据
        /// <summary>
        /// 商品配置数据
        /// </summary>
        /// <param name="loginID">用户登陆标识</param>
        /// <returns>商品列表</returns>
        public List<ProductConfig> GetWXProductConfig(string loginID)
        {
            var list = new List<ProductConfig>();

            DbDataReader dbreader = null;
            try
            {
                if (!ComFunction.ExistUserLoginID(loginID))
                {
                    return list;
                }

                Dictionary<string, ProPrice> prodic = ComFunction.GetProPrice();

                dbreader = DbHelper.ExecuteReader(ComFunction.GetProductSql);
                while (dbreader.Read())
                {
                    var ptc = new ProductConfig
                    {
                        ProductCode =
                            DBNull.Value != dbreader["productcode"] ? dbreader["productcode"].ToString() : string.Empty,
                        ProductName =
                            DBNull.Value != dbreader["ProductName"] ? dbreader["ProductName"].ToString() : string.Empty,
                        GoodsCode =
                            DBNull.Value != dbreader["goodscode"] ? dbreader["goodscode"].ToString() : string.Empty,
                        AdjustBase =
                            DBNull.Value != dbreader["adjustbase"] ? Convert.ToDouble(dbreader["adjustbase"]) : 0,
                        AdjustCount =
                            DBNull.Value != dbreader["adjustcount"] ? Convert.ToInt32(dbreader["adjustcount"]) : 0,
                        PriceDot = DBNull.Value != dbreader["pricedot"] ? Convert.ToInt32(dbreader["pricedot"]) : 0,
                        ValueDot = DBNull.Value != dbreader["valuedot"] ? Convert.ToDouble(dbreader["valuedot"]) : 0,
                        SetBase = DBNull.Value != dbreader["setBase"] ? Convert.ToInt32(dbreader["setBase"]) : 0,
                        HoldBase = DBNull.Value != dbreader["holdbase"] ? Convert.ToInt32(dbreader["holdbase"]) : 0,
                        OrderMoney = DBNull.Value != dbreader["Ordemoney"] ? Convert.ToDouble(dbreader["Ordemoney"]) : 1,
                        MaxPrice = DBNull.Value != dbreader["maxprice"] ? Convert.ToDouble(dbreader["maxprice"]) : 8000,
                        MinPrice = DBNull.Value != dbreader["minprice"] ? Convert.ToDouble(dbreader["minprice"]) : 1,
                        MaxTime = DBNull.Value != dbreader["maxtime"] ? Convert.ToDouble(dbreader["maxtime"]) : 60,
                        State = DBNull.Value != dbreader["state"] ? dbreader["state"].ToString() : string.Empty,
                        Unit = DBNull.Value != dbreader["unit"] ? Convert.ToDouble(dbreader["unit"]) : 1,
                        PriceCode =
                            DBNull.Value != dbreader["pricecode"] ? dbreader["pricecode"].ToString() : string.Empty
                    };

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
        #endregion

        #region 7获取历史数据
        /// <summary>
        /// 获取历史数据
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="pricecode">行情编码</param>
        /// <param name="weekflg">周期（M1,M5,M15,...MN）</param>
        /// <param name="maxCount">最大返回记录数</param>
        /// <returns></returns>
        public List<HisData> GetWXHisData(string loginId, string pricecode, string weekflg, int maxCount)
        {
            var list = new List<HisData>();
            DbDataReader dbreader = null;
            try
            {
                string sql = string.Format("select top {2} weektime, openprice, highprice, lowprice, closeprice, volnum from data_{0} where pricecode='{1}' order by weektime desc",
                        weekflg, pricecode, maxCount);
                dbreader = DbHelper2.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    var hisdata = new HisData
                    {
                        weektime = DBNull.Value != dbreader["weektime"] ? dbreader["weektime"].ToString() : string.Empty,
                        openprice =
                            DBNull.Value != dbreader["openprice"] ? dbreader["openprice"].ToString() : string.Empty,
                        highprice =
                            DBNull.Value != dbreader["highprice"] ? dbreader["highprice"].ToString() : string.Empty,
                        lowprice = DBNull.Value != dbreader["lowprice"] ? dbreader["lowprice"].ToString() : string.Empty,
                        closeprice =
                            DBNull.Value != dbreader["closeprice"] ? dbreader["closeprice"].ToString() : string.Empty,
                        volnum = DBNull.Value != dbreader["volnum"] ? dbreader["volnum"].ToString() : string.Empty
                    };
                    list.Add(hisdata);
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                if (list.Count > 0)
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
        #endregion

        #region 8判断能否成为经纪人
        /// <summary>
        /// 判断能否成为经纪人
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <returns></returns>
        public ResultDesc IsCanEconomicMan(string loginId, string wUserId)
        {
            var rsdc = new ResultDesc();
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
                string sql = string.Format(@"select sum(a) countOccMoney from (select sum(OccMoney) a from L_Trade_Order where userId='{0}' 
                           union select sum(OccMoney) a from Trade_Order where userId='{0}') b", ComFunction.GetWUserID(wUserId));
                if (ComFunction.IsCanEconomicMan(sql))
                {
                    rsdc.Result = true;
                    rsdc.Desc = "允许该用户转为经纪人";
                }
                else
                {
                    rsdc.Result = false;
                    rsdc.Desc = "该用户未达到转为经纪人的条件";
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "服务器返回错误";
            }
            return rsdc;
        }
        #endregion

        #region 9经纪人支付保证金
        /// <summary>
        /// 经纪人支付保证金
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <param name="money">保证金金额</param>
        /// <returns>PayCashFundInfo</returns>
        public PayCashFundInfo PayEconomicManCashFund(string loginId, string wUserId, double money)
        {
            var rsdc = new PayCashFundInfo();
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
                string operUser = tdUser.Account;//操作人
                var sqlList = new List<string>();
                string uSql = string.Format(@"update Trade_FundInfo set CashFund={0} where userId='{1}'", money, ComFunction.GetWUserID(wUserId));
                sqlList.Add(uSql);

                //添加操作记录
                string ipmac = ComFunction.GetIpMac(tdUser.Ip, tdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)tdUser.UType, string.Format("{0}经纪人支付保证金", ipmac)));
                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "经纪人支付保证金出错！";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "经纪人支付保证金成功！";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "服务器返回错误";
            }
            return rsdc;
        }
        #endregion

        #region 10普通用户转经纪人
        /// <summary>
        /// 普通用户转经纪人
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <returns>ResultDesc</returns>
        public ResultDesc BecomeEconomicMan(string loginId, string wUserId)
        {
            var rsdc = new ResultDesc();
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
                string operUser = tdUser.Account;//操作人
                var sqlList = new List<string>();
                string uSql = string.Format(@"update Base_WUser set AccountType='4' where WUserId='{0}'", wUserId);
                sqlList.Add(uSql);

                //添加操作记录
                string ipmac = ComFunction.GetIpMac(tdUser.Ip, tdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)tdUser.UType, string.Format("{0}转经纪人", ipmac)));
                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "转经纪人出错！";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "转经纪人成功！";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "服务器返回错误";
            }
            return rsdc;
        }
        #endregion

        #region 11能否返还经纪人的保证金  未完成
        /// <summary>
        /// 能否返还经纪人的保证金
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <returns>ReturnCashFundInfo</returns>
        public ReturnCashFundInfo IsCanReturnEconomicManCashFund(string loginId, string wUserId)
        {
            var rsdc = new ReturnCashFundInfo();
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

                string sql = string.Format(@"");
                double cashFund = 0;
                if (ComFunction.IsCanReturnEconomicManCashFund(sql, ref cashFund))
                {
                    rsdc.Result = true;
                    rsdc.Desc = "可以返回经纪人的保证金";
                    rsdc.Money = cashFund;
                }
                else
                {
                    rsdc.Result = false;
                    rsdc.Desc = "不能返回该经济人的保证金";
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "服务器返回错误";
            }
            return rsdc;
        }
        #endregion

        #region 12返还转经纪人时的保证金
        /// <summary>
        /// 返还转经纪人时的保证金
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <param name="money">返还保证金金额</param>
        /// <returns>ResultDesc</returns>
        public ResultDesc ReturnEconomicManCashFund(string loginId, string wUserId, double money)
        {
            var rsdc = new ResultDesc();
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

                string operUser = tdUser.Account;//操作人
                var sqlList = new List<string>();
                string uSql = string.Format(@"update Trade_FundInfo set CashFund=CashFund-{0} where userId='{1}'", money, ComFunction.GetWUserID(wUserId));
                sqlList.Add(uSql);

                //添加操作记录
                string ipmac = ComFunction.GetIpMac(tdUser.Ip, tdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)tdUser.UType, string.Format("{0}返回经纪人保证金", ipmac)));
                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "返回经纪人保证金出错！";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "返回经纪人保证金成功！";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "服务器返回错误";
            }
            return rsdc;
        }
        #endregion

        #region 13经纪人查询客户信息及客户的交易信息
        /// <summary>
        /// 经纪人查询客户信息及客户的交易信息
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <returns>UserTradeInfo</returns>
        public UserTradeInfo GetEconomicManUserInfo(string loginId, string wUserId)
        {
            var info = new UserTradeInfo();
            DbDataReader dbreader = null;
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
                string sql = string.Format(@"select T1.WUserId,T1.UserID,T1.PhoneNum,T1.Name,T1.WAccount ,T1.PWUserId ,T1.AccountType,T1.Ticket ,T1.Url,Ct.CCount
                                 from Base_WUser as T1 left join Base_User as T2  
                                 on T1.UserID=T2.UserID left join 
                                 (select userid,sum(wcount) as CCount from
                                 (select userid,sum(usequantity) wcount from Trade_Order group by userid
                                  union
                                  select userid,sum(quantity) wcount  from L_Trade_Order group by userid) as T
                                  group by T.userid) as Ct
                                  on T2.UserID=Ct.userid
                                  where t1.PWuserId='{0}'", wUserId);
                dbreader = DbHelper2.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    var wuInfo = new WTradeUserInfo
                    {
                        TdUser = { Account = "" },
                        WUser =
                        {
                            AccountType =
                                DBNull.Value != dbreader["AccountType"]
                                    ? dbreader["AccountType"].ToString()
                                    : string.Empty,
                            Name = DBNull.Value != dbreader["Name"] ? dbreader["Name"].ToString() : string.Empty,
                            PhoneNum =
                                DBNull.Value != dbreader["PhoneNum"] ? dbreader["PhoneNum"].ToString() : string.Empty,
                            PWUserId =
                                DBNull.Value != dbreader["PWUserId"] ? dbreader["PWUserId"].ToString() : string.Empty,
                            Ticket = DBNull.Value != dbreader["Ticket"] ? dbreader["Ticket"].ToString() : string.Empty,
                            Url = DBNull.Value != dbreader["Url"] ? dbreader["Url"].ToString() : string.Empty,
                            UserID = DBNull.Value != dbreader["UserID"] ? dbreader["UserID"].ToString() : string.Empty,
                            WAccount =
                                DBNull.Value != dbreader["WAccount"] ? dbreader["WAccount"].ToString() : string.Empty,
                            WUserId =
                                DBNull.Value != dbreader["WUserId"] ? dbreader["WUserId"].ToString() : string.Empty
                        },
                        Count = DBNull.Value != dbreader["CCount"] ? Convert.ToInt16(dbreader["CCount"]) : 0
                    };
                    info.Users.Add(wuInfo);
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
            return info;
        }
        #endregion



        #region 获取组织机构微信二维码地址
        /// <summary>
        /// 获取组织机构微信二维码地址
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="orgID">组织机构ID</param>
        /// <returns>OrgTicketUrlInfo</returns>
        public OrgTicketUrlInfo GetOrgTicketUrl(string loginId, string orgID)
        {
            var info = new OrgTicketUrlInfo();
            DbDataReader dbreader = null;
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
                string sql = string.Format(@"SELECT Url FROM [base_Org] where OrgID='{0}'", orgID);
                dbreader = DbHelper2.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    info.Url = DBNull.Value != dbreader["Url"] ? dbreader["Url"].ToString() : string.Empty;
                }
                if (string.IsNullOrEmpty(info.Url))
                {
                    info.Result = false;
                    info.Desc = "未生成二维码，请联系管理员";
                }
                else
                {
                    info.Result = true;
                    info.Desc = "加载成功！";
                }
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                info.Result = false;
                info.Desc = "加载异常，请联系管理员";
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return info;
        }
        #endregion

        #region 设置会员佣金比例
        /// <summary>
        /// 设置会员佣金比例
        /// </summary>
        /// <param name="loginId">登录标识</param>
        /// <param name="ratio1">一级佣金比例</param>
        /// <param name="ratio2">二级佣金比例</param>
        /// <param name="ratio3">三级佣金比例</param>
        /// <param name="orgIDList">待设置的会员列表</param>
        /// <returns>ResultDesc</returns>
        public ResultDesc SetCommissionRatio(string loginId, double ratio1, double ratio2, double ratio3, List<string> orgIDList)
        {
            var rsdc = new ResultDesc();
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

                string operUser = tdUser.Account;//操作人
                var sqlList = new List<string>();

                foreach (string orgID in orgIDList)
                {
                    sqlList.Add(string.Format("DELETE FROM Trade_CommissionRatioSet WHERE OrgID='{0}'", orgID));
                    sqlList.Add(string.Format("INSERT INTO Trade_CommissionRatioSet ([ID],[OrgID],[Ratio1],[Ratio2],[Ratio3]) VALUES ('{0}','{1}',{2},{3},{4})",
                        Guid.NewGuid().ToString("n"), orgID, ratio1, ratio2, ratio3));
                }


                //添加操作记录
                string ipmac = ComFunction.GetIpMac(tdUser.Ip, tdUser.Mac);
                sqlList.Add(string.Format("insert into Base_OperrationLog([OperTime],[Account],[UserType],[Remark]) values('{0}','{1}',{2},'{3}')",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), operUser, (int)tdUser.UType, string.Format("{0}设置会员佣金比例", ipmac)));
                if (!ComFunction.SqlTransaction(sqlList))
                {
                    rsdc.Result = false;
                    rsdc.Desc = "设置会员佣金比例出错！";
                    return rsdc;
                }
                rsdc.Result = true;
                rsdc.Desc = "设置会员佣金比例成功！";
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                rsdc.Result = false;
                rsdc.Desc = "服务器返回错误";
            }
            return rsdc;
        } 
        #endregion


        #region 获取会员佣金比例
        /// <summary>
        /// 获取会员佣金比例
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="orgID">组织机构ID</param>
        /// <returns>OrgTicketUrlInfo</returns>
        public CommissionRatioSetInfo GetCommissionRatio(string loginId, string orgID, bool type)
        {
            var info = new CommissionRatioSetInfo();
            DbDataReader dbreader = null;
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

                string sql;
                sql = string.Format(@"SELECT [ID],[OrgID] ,[Ratio1] ,[Ratio2] ,[Ratio3] FROM [Trade_CommissionRatioSet] where OrgID='{0}'", orgID);
                if (type)
                    sql = string.Format(@"SELECT [ID],[OrgID] ,[Ratio1] ,[Ratio2] ,[Ratio3] FROM [Trade_CommissionRatioSet] where OrgID='{0}'", "system");
                dbreader = DbHelper2.ExecuteReader(sql);
                while (dbreader.Read())
                {
                    info.ID = DBNull.Value != dbreader["ID"] ? dbreader["ID"].ToString() : string.Empty;
                    info.OrgID = DBNull.Value != dbreader["OrgID"] ? dbreader["OrgID"].ToString() : string.Empty;
                    info.Ratio1 = DBNull.Value != dbreader["Ratio1"] ? Convert.ToDouble(dbreader["Ratio1"]) : 0;
                    info.Ratio2 = DBNull.Value != dbreader["Ratio2"] ? Convert.ToDouble(dbreader["Ratio2"]) : 0;
                    info.Ratio3 = DBNull.Value != dbreader["Ratio3"] ? Convert.ToDouble(dbreader["Ratio3"]) : 0;
                }
                if (string.IsNullOrEmpty(info.ID))
                {
                    string sql2 = string.Format(@"SELECT [ID],[OrgID] ,[Ratio1] ,[Ratio2] ,[Ratio3] FROM [Trade_CommissionRatioSet] where OrgID='{0}'", "system");
                    dbreader = DbHelper2.ExecuteReader(sql2);
                    while (dbreader.Read())
                    {
                        info.ID = DBNull.Value != dbreader["ID"] ? dbreader["ID"].ToString() : string.Empty;
                        info.OrgID = DBNull.Value != dbreader["OrgID"] ? dbreader["OrgID"].ToString() : string.Empty;
                        info.Ratio1 = DBNull.Value != dbreader["Ratio1"] ? Convert.ToDouble(dbreader["Ratio1"]) : 0;
                        info.Ratio2 = DBNull.Value != dbreader["Ratio2"] ? Convert.ToDouble(dbreader["Ratio2"]) : 0;
                        info.Ratio3 = DBNull.Value != dbreader["Ratio3"] ? Convert.ToDouble(dbreader["Ratio3"]) : 0;
                    }
                }
                info.Result = true;
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                info.Result = false;
                info.Desc = "加载异常，请联系管理员";
            }
            finally
            {
                if (null != dbreader)
                {
                    dbreader.Close();
                    dbreader.Dispose();
                }
            }
            return info;
        }
        #endregion
    }
}






