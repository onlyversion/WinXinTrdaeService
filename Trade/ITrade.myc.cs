using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using WcfInterface.model;
using WcfInterface.model.WJY;

namespace Trade
{
    /// <summary>
    /// 微交易用接口
    /// </summary>
    public partial interface ITrade
    {
        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="marorderln">下单信息</param>
        /// <returns>下单结果</returns>
        [OperationContract]
        Marketorders GetWXMarketorders(MarOrdersLn marorderln, List<UserExperience> experiences);

        /// <summary>
        /// 市价单取消接口 结算盈亏 退还保证金(平仓)
        /// </summary>
        /// <param name="delen">市价单取消信息</param>
        /// <returns>市价单取消结果</returns>
        [OperationContract]
        Marketorders WXDelOrder(DeliveryEnter delen);

        /// <summary>
        /// 订单历史
        /// </summary>
        /// <param name="lqc">查询条件</param>
        /// <param name="ltype">"1"平仓历史 "2"入库历史</param>
        /// <param name="pageindex">第几页,从1开始</param>
        /// <param name="pagesize">每页多少条</param>
        /// <param name="page">输出参数(总页数)</param>
        /// <returns>市价单历史记录</returns>
        [OperationContract]
        List<LTradeOrder> GetWXLTradeOrder(LQueryCon lqc, string ltype, int pageindex, int pagesize, ref int page);

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="loginId">用户登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <returns></returns>
        [OperationContract]
        WUserInfo GetWXUserInfo(string loginId, string wUserId);

        /// <summary>
        /// 修改交易密码
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <param name="newPassword">新交易密码</param>
        /// <returns>ResultDesc</returns>
        [OperationContract]
        ResultDesc ModifyTradePassword(string loginId, string wUserId, string newPassword);

        /// <summary>
        /// 商品配置数据
        /// </summary>
        /// <param name="loginID">用户登陆标识</param>
        /// <returns>商品列表</returns>
        [OperationContract]
        List<ProductConfig> GetWXProductConfig(string loginID);

        /// <summary>
        /// 获取历史数据
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="pricecode">行情编码</param>
        /// <param name="weekflg">周期（M1,M5,M15,...MN）</param>
        /// <param name="maxCount">最大返回记录数</param>
        /// <returns></returns>
        [OperationContract]
        List<HisData> GetWXHisData(string loginId, string pricecode, string weekflg, int maxCount);

        /// <summary>
        /// 判断能否成为经纪人
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <returns></returns>
        [OperationContract]
        ResultDesc IsCanEconomicMan(string loginId, string wUserId);

        /// <summary>
        /// 经纪人支付保证金
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <param name="money">保证金金额</param>
        /// <returns>PayCashFundInfo</returns>
        [OperationContract]
        PayCashFundInfo PayEconomicManCashFund(string loginId, string wUserId, double money);

        /// <summary>
        /// 普通用户转经纪人
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <returns>ResultDesc</returns>
        [OperationContract]
        ResultDesc BecomeEconomicMan(string loginId, string wUserId);

        /// <summary>
        /// 能否返还经纪人的保证金
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <returns>ReturnCashFundInfo</returns>
        [OperationContract]
        ReturnCashFundInfo IsCanReturnEconomicManCashFund(string loginId, string wUserId);

        /// <summary>
        /// 返还转经纪人时的保证金
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <param name="money">返还保证金金额</param>
        /// <returns>ResultDesc</returns>
        [OperationContract]
        ResultDesc ReturnEconomicManCashFund(string loginId, string wUserId, double money);

        /// <summary>
        /// 经纪人查询客户信息及客户的交易信息
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="wUserId">微交易用户ID</param>
        /// <returns>UserTradeInfo</returns>
        [OperationContract]
        UserTradeInfo GetEconomicManUserInfo(string loginId, string wUserId);

        /// <summary>
        /// 获取组织机构微信二维码地址
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="orgID">组织机构ID</param>
        /// <returns>OrgTicketUrlInfo</returns>
        [OperationContract]
        OrgTicketUrlInfo GetOrgTicketUrl(string loginId, string orgID);

        /// <summary>
        /// 设置会员佣金比例
        /// </summary>
        /// <param name="loginId">登录标识</param>
        /// <param name="ratio1">一级佣金比例</param>
        /// <param name="ratio2">二级佣金比例</param>
        /// <param name="ratio3">三级佣金比例</param>
        /// <param name="orgIDList">待设置的会员列表</param>
        /// <returns>ResultDesc</returns>
        [OperationContract]
        ResultDesc SetCommissionRatio(string loginId, double ratio1, double ratio2, double ratio3,
            List<string> orgIDList);

        /// <summary>
        /// 获取会员佣金比例
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="orgID">组织机构ID</param>
        /// <returns>OrgTicketUrlInfo</returns>
        [OperationContract]
        CommissionRatioSetInfo GetCommissionRatio(string loginId, string orgID,bool type);
    }
}
