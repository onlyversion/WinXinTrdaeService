//*******************************************************************************
//  文 件 名：ITrade.ygj.cs
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
    public partial interface ITrade
    {
        /// <summary>
        /// 市价单查询
        /// </summary>
        /// <param name="LoginID">用户登陆标识</param>
        /// <returns>市价单记录</returns>
        [OperationContract]
        List<TradeOrder> GetTradeOrder(string LoginID);

        /// <summary>
        /// 挂单查询
        /// </summary>
        /// <param name="LoginID">登陆标识</param>
        /// <returns>挂单记录</returns>
        [OperationContract]
        List<TradeHoldOrder> GetTradeHoldOrder(string LoginID);

        /// <summary>
        /// 挂单下单接口
        /// </summary>
        /// <param name="orderln">下单信息</param>
        /// <returns>下单结果</returns>
        [OperationContract]
        Orders GetOrders(OrdersLncoming orderln);

        /// <summary>
        /// 挂单取消接口
        /// </summary>
        /// <param name="DhInfo">取消信息</param>
        /// <returns>取消结果</returns>
        [OperationContract]
        MarDelivery DelHoldOrder(DelHoldInfo DhInfo);

        /// <summary>
        /// 市价单下单接口
        /// </summary>
        /// <param name="marorderln">下单信息</param>
        /// <returns>下单结果</returns>
        [OperationContract]
        Marketorders GetMarketorders(MarOrdersLn marorderln);

        /// <summary>
        /// 市价单修改接口
        /// </summary>
        /// <param name="marln">修改信息</param>
        /// <returns>修改结果</returns>
        [OperationContract]
        Marketorders ModifyMarketorders(MarketLnEnter marln);

        /// <summary>
        /// 市价单取消接口
        /// </summary>
        /// <param name="delen">市价单取消信息</param>
        /// <returns>市价单取消结果</returns>
        [OperationContract]
        Marketorders DelOrder(DeliveryEnter delen);

        /// <summary>
        /// 用户出入金
        /// </summary>
        /// <param name="remit">出入金信息</param>
        /// <returns>出入金结果</returns>
        [OperationContract]
        ResultDesc HuaXiaRemitMoney(RemitInfo remit);

        /// <summary>
        /// 华夏银行签约,本行开户
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="bankcard">华夏卡号</param>
        /// <returns>签约结果</returns>
        [OperationContract]
        ResultDesc ContactToHuaxiaSelfBank(string userid,string bankcard);

        /// <summary>
        /// 华夏银行签约,他行开户
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>签约结果</returns>
        [OperationContract]
        ResultDesc ContactToHuaxiaOhterBank(string userid, OpenBankInfo OpenBank);

        /// <summary>
        /// 华夏银行解约
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>解约结果</returns>
        [OperationContract]
        ResultDesc RescindHuaxiaBank(string userid);

        /// <summary>
        /// 测试获取实时行情价格
        /// </summary>
        /// <param name="pcode">行情编码</param>
        /// <param name="x">测试错误输入0</param>
        /// <returns>实时价格</returns>
        [OperationContract]
        double TestGetRealPrice(string pcode,int x);

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
        [OperationContract]
        ResultDesc RegTestTradeUser(string UserName, string TradeAccount, string TradePwd, string PhoneNum, string Email, string CardNum);

        /// <summary>
        /// 获取交易设置信息
        /// </summary>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>交易设置信息</returns>
        [OperationContract]
        TradeSetInfo GetTradeSetInfo(string LoginId);

        /// <summary>
        /// 新闻公告查询
        /// </summary>
        /// <param name="LoginId">登陆标识</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="NType">新闻类型</param>
        /// <returns>新闻信息</returns>
        [OperationContract]
        TradeNewsInfo GetTradeNewsInfo(string LoginId, DateTime starttime, DateTime endtime, NewsType NType);

        /// <summary>
        /// 新闻公告分页查询查询
        /// </summary>
        /// <param name="lqc">新闻查询条件</param>
        /// <param name="pageindex">第几页,从1开始</param>
        /// <param name="pagesize">每页多少条</param>
        /// <param name="page">输出参数(总页数)</param>
        /// <returns>新闻信息</returns>
        [OperationContract]
        TradeNewsInfo GetTradeNewsInfoWithPage(NewsLqc lqc, int pageindex, int pagesize, ref int page);

        /// <summary>
        /// 获取历史数据
        /// </summary>
        /// <param name="pricecode">行情编码</param>
        /// <param name="weekflg">周期（M1,M5,M15,...MN）</param>
        /// <returns></returns>
         [OperationContract]
        List<HisData> GetHisData(string pricecode,string weekflg);

                /// <summary>
        /// 获取历史数据
        /// </summary>
        /// <param name="pricecode">行情编码</param>
        /// <param name="weekflg">周期（M1,M5,M15,...MN）</param>
        /// <param name="weektime">时间数字串</param>
        /// <returns></returns>
         [OperationContract]
         List<HisData> GetHisDataWithTime(string pricecode, string weekflg, string weektime);

         /// <summary>
         /// 获取历史数据
         /// </summary>
         /// <param name="pricecode">行情编码</param>
         /// <param name="weekflg">周期（M1,M5,M15,...MN）</param>
         /// <param name="weektime">时间数字串</param>
         /// <param name="mdtime">修改时间数字字符串</param>
         /// <returns></returns>
           [OperationContract]
         ComHisData GetHisDataWithMdtime(string pricecode, string weekflg, string weektime, DateTime mdtime);

        /// <summary>
         /// 获取历史数据分页查询
        /// </summary>
        /// <param name="HisCon"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
         [OperationContract]
         List<HisData> GetHisDataWithPage(HisQueryCon HisCon, int pageindex, int pagesize, ref int page);

        /// <summary>
        /// 修改历史数据
        /// </summary>
        /// <param name="hisdata"></param>
        /// <param name="pricecode"></param>
        /// <param name="weekflg"></param>
        /// <returns></returns>
         [OperationContract]
         ResultDesc ModifyHisData(HisData hisdata, string pricecode, string weekflg);

    }
}
