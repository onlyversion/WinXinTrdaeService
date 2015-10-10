//*******************************************************************************
//  文 件 名：ITrade.cs
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
using System.ServiceModel;
using WcfInterface.model;

namespace Trade
{
    /// <summary>
    /// 交易接口
    /// </summary>
    [ServiceContract(Name = "ITrade", Namespace = "www.ITrade.com")]
    public partial interface ITrade
    {
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="tradeAccount">用户账号</param>
        /// <param name="tradePwd">用户密码</param>
        /// <param name="mac">Mac地址</param>
        /// <returns>登陆信息</returns>
        [OperationContract]
        Loginfo GetLogin(string tradeAccount, string tradePwd,string mac);


        /// <summary>
        /// 行情客户端用户登陆
        /// </summary>
        /// <param name="tradeAccount">用户账号</param>
        /// <param name="tradePwd">用户密码</param>
        /// <param name="mac">Mac地址</param>
        /// <returns>登陆信息</returns>
        [OperationContract]
        Loginfo GetLoginEx(string tradeAccount, string tradePwd, string mac);


        /// <summary>
        /// 商品配置
        /// </summary>
        /// <param name="LoginID">用户登陆标识</param>
        /// <returns>商品列表</returns>
        [OperationContract]
        List<ProductConfig> GetProductConfig(string LoginID);

        /// <summary>
        /// 商品配置
        /// </summary>
        /// <param name="LoginID">用户登陆标识</param>
        /// <returns>商品列表</returns>
        [OperationContract]
        List<ProductConfig> GetProductConfigEx(string LoginID);

        /// <summary>
        /// 用户资金库存查询
        /// </summary>
        /// <param name="LoginID">用户登陆标识</param>
        /// <returns>资金库存</returns>
        [OperationContract]
        MoneyInventory GetMoneyInventory(string LoginID);

        /// <summary>
        /// 资金库存信息查询
        /// </summary>
        /// <param name="account">要被查询的账号</param>
        /// <param name="LoginID">登陆标识</param>
        /// <returns>资金库存信息</returns>
        [OperationContract]
        MoneyInventory GetMoneyInventoryEx(string account, string LoginID);

        /// <summary>
        /// 市价单查询
        /// </summary>
        /// <param name="account">被查询的账号</param>
        /// <param name="LoginID">登陆标识</param>
        /// <returns>市价单记录</returns>
        [OperationContract]
        List<TradeOrder> GetTradeOrderEx(string account, string LoginID);

        /// <summary>
        /// 挂单查询
        /// </summary>
        /// <param name="account">被查询的账号</param>
        /// <param name="LoginID">登陆标识</param>
        /// <returns>挂单记录</returns>
         [OperationContract]
        List<TradeHoldOrder> GetTradeHoldOrderEx(string account, string LoginID);

        /// <summary>
        /// 市价单历史查询
        /// </summary>
        /// <param name="Lqc">查询条件</param>
        /// <param name="Ltype">"1"平仓历史 "2"入库历史</param>
        /// <param name="pageindex">第几页,从1开始</param>
        /// <param name="pagesize">每页多少条</param>
        /// <param name="page">输出参数(总页数)</param>
        /// <returns>市价单历史记录</returns>
        [OperationContract]
        List<LTradeOrder> GetLTradeOrder(LQueryCon Lqc,string Ltype, int pageindex, int pagesize, ref int page);

        /// <summary>
        /// 挂单历史查询
        /// </summary>
        /// <param name="Lqc">查询条件</param>
        /// <param name="pageindex">第几页,从1开始</param>
        /// <param name="pagesize">每页多少条</param>
        /// <param name="page">输出参数(总页数)</param>
        /// <returns>挂单历史记录</returns>
        [OperationContract]
        List<LTradeHoldOrder> GetLTradeHoldOrder(LQueryCon Lqc, int pageindex, int pagesize, ref int page);
        /// <summary>
        /// 下单延迟多少秒
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [OperationContract]
        double GetDelayPlaceOrder(string userid);
        /// <summary>
        /// 平仓延迟多少秒
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [OperationContract]
        double GetDelayFlatOrder(string userid);
    }
}
