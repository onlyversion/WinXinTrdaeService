//*******************************************************************************
//  文 件 名：IManager.cs
//  版    本：Ver 1.0.0.0
//  文件说明：

//  创建者：  游官君 
//  创建日期：2013/07/01
//
//  修改标识：
//  修改说明：
//*******************************************************************************

using System.ServiceModel;
using System.Collections.Generic;
using System;
using WcfInterface.model;
using WcfInterface.model.WJY;

namespace GssManager
{
    /// <summary>
    /// 后台管理接口
    /// </summary>
    [ServiceContract(Name = "IManager", Namespace = "www.IManager.com")]
    public partial interface IManager
    {
        /// <summary>
        /// 新增交易用户
        /// </summary>
        /// <param name="TdUser">用户信息</param>
        /// <param name="UType">用户类型</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>结果描述</returns>
        [OperationContract]
        ResultDesc AddTradeUser(TradeUser TdUser,UserType UType, string LoginId);

        /// <summary>
        /// 新增交易用户
        /// </summary>
        /// <param name="TdUser">用户信息</param>
        /// <param name="Fdinfo">资金信息</param>
        /// <param name="UType">用户类型</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>结果描述</returns>
        [OperationContract]
        ResultDesc AddTradeUserEx(TradeUser TdUser,Fundinfo Fdinfo, UserType UType, string LoginId);

        /// <summary>
        /// 获取用户资金信息
        /// </summary>
        /// <param name="LoginId">登陆标识</param>
        /// <param name="TradeAccount">客户账号</param>
        /// <returns>用户资金信息</returns>
        [OperationContract]
        UserFundinfo GetUserFundinfo(string LoginId,string TradeAccount);

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="TradeAccount">要删除的客户账号</param>
        /// <param name="LoginId">管理员或组织登陆标识</param>
        /// <returns>删除结果</returns>
        [OperationContract]
        ResultDesc DelTradeUser(string TradeAccount, string LoginId);

        /// <summary>
        /// 修改客户资料
        /// </summary>
        /// <param name="TdUser">用户信息</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>修改结果</returns>
        [OperationContract]
        ResultDesc ModifyTradeUser(TradeUser TdUser, string LoginId);

        /// <summary>
        /// 修改客户资料
        /// </summary>
        /// <param name="TdUser">用户信息</param>
        /// <param name="Fdinfo">资金信息</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>修改结果</returns>
        [OperationContract]
        ResultDesc ModifyTradeUserEx(TradeUser TdUser,Fundinfo Fdinfo, string LoginId);

        /// <summary>
        /// 客户资金修改
        /// </summary>
        /// <param name="Fdinfo">资金信息</param>
        /// <param name="tradeAccount">用户帐号</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>修改结果</returns>
        [OperationContract]
        ResultDesc ModifyUserFundinfo(Fundinfo Fdinfo, string tradeAccount,string LoginId);

        /// <summary>
        /// 管理员登陆
        /// </summary>
        /// <param name="AdminId">管理员帐号</param>
        /// <param name="Password">管理员密码</param>
        /// <param name="mac">MAC地址</param>
        /// <returns>管理员权限</returns>
        [OperationContract]
        ReAdminAuth AdminLogin(string AdminId, string Password, string mac);


        /// <summary>
        /// 获取交易日信息
        /// </summary>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>交易日信息</returns>
        [OperationContract]
        DateSetInfo GetDateSetInfo(string LoginId);

        /// <summary>
        /// 修改交易日
        /// </summary>
        /// <param name="DtSet">交易日信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        [OperationContract]
        ResultDesc ModifyDateSet(DateSet DtSet, string LoginId);


        /// <summary>
        /// 获取交易日信息
        /// </summary>
        /// <param name="pricecode">行情编码</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>交易日信息</returns>
        [OperationContract]
        DateSetInfo GetDateSetInfoEx(string pricecode,string LoginId);

        /// <summary>
        /// 修改交易日
        /// </summary>
        /// <param name="DtSet">交易日信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        [OperationContract]
        ResultDesc ModifyDateSetEx(DateSet DtSet, string LoginId);

        /// <summary>
        /// 获取节假日信息
        /// </summary>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>节假日信息</returns>
        [OperationContract]
        DateHolidayInfo GetHolidayInfo(string LoginId);

        /// <summary>
        /// 修改节假日
        /// </summary>
        /// <param name="Hliday">节假日信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        [OperationContract]
        ResultDesc ModifyHoliday(DateHoliday Hliday, string LoginId);

        /// <summary>
        /// 添加节假日
        /// </summary>
        /// <param name="Hliday">节假日信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <param name="ID">返回ID</param>
        /// <returns>添加结果</returns>
        [OperationContract]
        ResultDesc AddHoliday(DateHoliday Hliday, string LoginId,ref string ID);

        /// <summary>
        /// 删除节假日
        /// </summary>
        /// <param name="ID">ID标识</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>删除结果</returns>
        [OperationContract]
        ResultDesc DelHoliday(string ID, string LoginId);


        /// <summary>
        /// 添加交易设置
        /// </summary>
        /// <param name="TdSet">交易设置信息</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>添加结果</returns>
        [OperationContract]
        ResultDesc AddTradeSet(TradeSet  TdSet,String LoginId);

        /// <summary>
        /// 删除交易设置
        /// </summary>
        /// <param name="ObjCode">名称编码</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>删除结果</returns>
        [OperationContract]
        ResultDesc DelTradeSet(string ObjCode, String LoginId);

        /// <summary>
        /// 修改交易设置
        /// </summary>
        /// <param name="TdSet">交易设置</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        [OperationContract]
        ResultDesc ModifyTradeSet(TradeSet TdSet, string LoginId);

        /// <summary>
        /// 新闻公告添加
        /// </summary>
        /// <param name="TdNews">新闻</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>添加结果</returns>
        [OperationContract]
        ResultDesc AddTradeNews(TradeNews TdNews, String LoginId);

        /// <summary>
        /// 新闻公告修改
        /// </summary>
        /// <param name="TdNews">新闻</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>修改结果</returns>
        [OperationContract]
        ResultDesc ModifyTradeNews(TradeNews TdNews, String LoginId);

        /// <summary>
        /// 新闻公告删除
        /// </summary>
        /// <param name="Id">ID标识</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>删除结果</returns>
        [OperationContract]
        ResultDesc DelTradeNews(string Id, String LoginId);

        /// <summary>
        /// 获取过滤IP
        /// </summary>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>过滤IP信息</returns>
        [OperationContract]
        TradeIpInfo GetTradeIpInfo(String LoginId);

        /// <summary>
        /// 修改过滤IP
        /// </summary>
        /// <param name="TdIp">过滤IP信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        [OperationContract]
        ResultDesc ModifyTradeIp(TradeIp TdIp, String LoginId);

        /// <summary>
        /// 添加过滤IP
        /// </summary>
        /// <param name="TdIp">过滤IP信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <param name="ID">返回ID</param>
        /// <returns>添加结果</returns>
        [OperationContract]
        ResultDesc AddTradeIp(TradeIp TdIp,String LoginId,ref string ID);

        /// <summary>
        /// 删除过滤IP
        /// </summary>
        /// <param name="ID">ID标识唯一记录</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>删除结果</returns>
        [OperationContract]
        ResultDesc DelTradeIp(string ID,String LoginId);

        /// <summary>
        /// 获取过滤MAC
        /// </summary>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>过滤MAC信息</returns>
        [OperationContract]
        TradeMacInfo GetTradeMacInfo(String LoginId);

        /// <summary>
        /// 修改过滤MAC
        /// </summary>
        /// <param name="TdMac">过滤MAC信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        [OperationContract]
        ResultDesc ModifyTradeMac(TradeMac TdMac, String LoginId);

        /// <summary>
        /// 添加过滤MAC
        /// </summary>
        /// <param name="TdMac">过滤MAC信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <param name="ID">返回ID</param>
        /// <returns>添加结果</returns>
        [OperationContract]
        ResultDesc AddTradeMac(TradeMac TdMac, String LoginId, ref string ID);

        /// <summary>
        /// 删除过滤MAC
        /// </summary>
        /// <param name="MacName">ID标识唯一记录</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>删除结果</returns>
        [OperationContract]
        ResultDesc DelTradeMac(string ID, String LoginId);

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>商品信息信息</returns>
        [OperationContract]
        TradeProductInfo GetTradeProductInfo(String LoginId);

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="TdProduct">商品信息信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        [OperationContract]
        ResultDesc ModifyTradeProduct(TradeProduct TdProduct, String LoginId);

        /// <summary>
        /// 添加商品信息
        /// </summary>
        /// <param name="TdProduct">商品信息信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>添加结果</returns>
        [OperationContract]
        ResultDesc AddTradeProduct(TradeProduct TdProduct, String LoginId);

        /// <summary>
        /// 删除商品信息
        /// </summary>
        /// <param name="ProductCode">要删除的商品编码</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>删除结果</returns>
        [OperationContract]
        ResultDesc DelTradeProduct(string ProductCode, String LoginId);

        /// <summary>
        /// 获取汇率和水
        /// </summary>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>汇率和水信息</returns>
        [OperationContract]
        TradeRateInfo GetTradeRateInfo(String LoginId);

        /// <summary>
        /// 修改汇率和水
        /// </summary>
        /// <param name="TdRate">汇率和水信息</param>
        /// <param name="LoginId">管理员登陆标识</param>
        /// <returns>修改结果</returns>
        [OperationContract]
        ResultDesc ModifyTradeRate(TradeRate TdRate, String LoginId);

        /// <summary>
        /// 获取交易对冲信息
        /// </summary>
        /// <param name="LoginId">登陆标识</param>
        /// <param name="Starttime">开始时间</param>
        /// <param name="Endtime">结束时间</param>
        /// <returns>交易对冲信息</returns>
        [OperationContract]
        HedgingInfo GetHedgingInfo(string LoginId, DateTime Starttime, DateTime Endtime);


        /// <summary>
        /// 手工调账或库存结算
        /// </summary>
        /// <param name="LoginId">登陆标识</param>
        /// <param name="TradeAccount">用户交易账号</param>
        /// <param name="money">调整资金</param>
        /// <param name="ReasonType">原因(3-库存结算,6-手工调账)</param>
        /// <returns>调整结果</returns>
        [OperationContract]
        ResultDesc ModifyUserMoney(string LoginId, string TradeAccount, double money, int ReasonType);


        /// <summary>
        /// 导出报表
        /// </summary>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="reporttype">报表类型(0有效订单,1限价挂单,2入库单,3平仓,4资金变动)</param>
        /// <param name="LoginID">管理员或组织登陆标识</param>
        /// <returns>报表地址</returns>
        [OperationContract]
        ReportForms GetReportForms(DateTime starttime, DateTime endtime, int reporttype, string LoginID);

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
        [OperationContract]
        ReportForms GetReportFormsUser(string TradeAccount, string UserName, string TelPhone, string Broker,
            string orgid, string IsBroker,
            DateTime starttime, DateTime endtime, int reporttype, string LoginID);

        /// <summary>
        /// 有效订单分页查询
        /// </summary>
        /// <param name="Cxqc">查询条条</param>
        /// <param name="pageindex">第几页,从1开始</param>
        /// <param name="pagesize">每页多少条</param>
        /// <param name="page">输出参数(总页数)</param>
        /// <returns>订单信息</returns>
         [OperationContract]
        TradeOrderInfo GetMultiTradeOrderWithPage(CxQueryCon Cxqc, int pageindex, int pagesize, ref int page);


         /// <summary>
         /// 限价挂单分页查询
         /// </summary>
         /// <param name="Cxqc">查询条条</param>
         /// <param name="pageindex">第几页,从1开始</param>
         /// <param name="pagesize">每页多少条</param>
         /// <param name="page">输出参数(总页数)</param>
         /// <returns>订单信息</returns>
         [OperationContract]
         TradeHoldOrderInfo GetMultiTradeHoldOrderWithPage(CxQueryCon Cxqc, int pageindex, int pagesize, ref int page);

        /// <summary>
        /// 订单历史分页查询
        /// </summary>
        /// <param name="Lqc"></param>
        /// <param name="Ltype"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
         [OperationContract]
         LTradeOrderInfo GetMultiLTradeOrderWithPage(LQueryCon Lqc, string Ltype, int pageindex, int pagesize, ref int page);

         /// <summary>
         /// 日志记录分页查询
         /// </summary>
         /// <param name="Logqc">查询条条</param>
         /// <param name="pageindex">第几页,从1开始</param>
         /// <param name="pagesize">每页多少条</param>
         /// <param name="page">输出参数(总页数)</param>
         /// <returns>日志信息</returns>
         [OperationContract]
         TradeALogInfo GetTradeALogInfoWithPage(LogQueryCon Logqc, int pageindex, int pagesize, ref int page);

         /// <summary>
         /// 客户资料分页查询
         /// </summary>
         /// <param name="Uqc">查询条条</param>
         /// <param name="pageindex">第几页,从1开始</param>
         /// <param name="pagesize">每页多少条</param>
         /// <param name="page">输出参数(总页数)</param>
         /// <returns>客户信息</returns>
        [OperationContract]
         UserBaseInfo GetUserBaseInfoWithPage(UserQueryCon Uqc, int pageindex, int pagesize, ref int page);

       
        /// <summary>
        /// 验证客户证件号码(在启用状态的客户中进行检查)
        /// </summary>
        /// <param name="TradeAccount">要添加或修改的客户账号</param>
        /// <param name="CardNum">证件号码</param>
        /// <param name="LoginId">登陆标识</param>
        /// <returns>验证结果</returns>
         [OperationContract]
        CheckResult CheckTradeUser(string TradeAccount, string CardNum, string LoginId);

        /// <summary>
        /// 手动报价
        /// </summary>
        /// <param name="LoginId">登陆标识</param>
        /// <param name="PriceCode">行情编码</param>
        /// <param name="RealPrice">实时价</param>
        /// <returns>结果描述</returns>
         [OperationContract]
         ResultDesc ManualPrice(String LoginId, String PriceCode, double RealPrice);

        /// <summary>
        /// 出金查询
        /// </summary>
        /// <param name="Cjqc"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [OperationContract]
         TradeChuJinInfo GetMultiTradeChuJinWithPage(CJQueryCon Cjqc, int pageindex, int pagesize, ref int page);

        /// <summary>
        /// 资金报表
        /// </summary>
        /// <param name="Fcqc"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
       [OperationContract]
        FundChangeInfo GetMultiFundChangeWithPage(FcQueryCon Fcqc, int pageindex, int pagesize, ref int page);

       /// <summary>
       /// 出金付款处理
       /// </summary>
       /// <param name="ApplyId"></param>
       /// <param name="LoginId"></param>
       /// <param name="state">处理状态，"1"-已付款,"2"已拒绝 "3"处理中 "4"处理失败</param>
       /// <returns></returns>
       [OperationContract]
       ResultDesc ProcessChuJin(int ApplyId, String LoginId, ref string state);

        /// <summary>
        /// 拒绝出金
        /// </summary>
        /// <param name="ApplyId"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        [OperationContract]
        ResultDesc RefusedChuJin(int ApplyId, String LoginId);


        /// <summary>
        /// 解约查询
        /// </summary>
        /// <param name="JYqc"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [OperationContract]
        TradeJieYueInfo GetMultiTradeJieYueWithPage(JYQueryCon JYqc, int pageindex, int pagesize, ref int page);

        /// <summary>
        /// 解约处理-审核
        /// </summary>
        /// <param name="ApplyId"></param>
        /// <param name="userid"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        [OperationContract]
        ResultDesc ProcessJieYue(int ApplyId, string userid, String LoginId);

        /// <summary>
        /// 解约处理-拒绝
        /// </summary>
        /// <param name="ApplyId"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        [OperationContract]
        ResultDesc RefusedJieYue(int ApplyId, String LoginId);

        /// <summary>
        /// 居间管理
        /// </summary>
        /// <param name="JJqc">查询条件</param>
        /// <returns></returns>
        [OperationContract]
        TradeJuJianInfo GetTradeJuJianInfo(JJQueryCon JJqc);


        /// <summary>
        /// 获取子级组织编码
        /// </summary>
        /// <param name="orgid">上级组织orgid</param>
        /// <param name="Telephone">上级组织编码</param>
        /// <returns></returns>
        [OperationContract]
        string GetOrgcode(string orgid, string Telephone);

        /// <summary>
        /// 获取出入金和解约记录
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        TradeMoneyInfo GetTradeMoneyInfo();
        /// <summary>
        /// 获取银行集合
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<TradeBank> GetTradeBank();

        #region 客户分组接口定义
        /// <summary>
        /// 2.3.4.6	删除客户组的客户
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="UserGroupId"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        [OperationContract]
        ResultDesc DelUserFromUserGroups(string UserId, string UserGroupId, String LoginId);

        /// <summary>
        /// 2.3.4.5	添加客户到客户组
        /// </summary>
        /// <param name="account">客户账号</param>
        /// <param name="UserGroupId">客户组ID</param>
        /// <param name="LoginId"></param>
        /// <param name="tduser">添加成功后,返回的被添加的客户的信息</param>
        /// <returns></returns>
        [OperationContract]
        ResultDesc AddUserToUserGroups(string account, string UserGroupId, String LoginId, ref TradeUser tduser);
        /// <summary>
        /// 2.3.4.4	修改客户组
        /// </summary>
        /// <param name="ugs"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        [OperationContract]
        ResultDesc ModifyUserGroups(UserGroups ugs, String LoginId);
        /// <summary>
        /// 2.3.4.3	删除客户组
        /// </summary>
        /// <param name="UserGroupId"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        [OperationContract]
        ResultDesc DelUserGroups(string UserGroupId, String LoginId);
        /// <summary>
        /// 2.3.4.2	添加客户组
        /// </summary>
        /// <param name="ugs"></param>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        [OperationContract]
        ResultDesc AddUserGroups(UserGroups ugs, String LoginId);
        /// <summary>
        /// 客户分组查询
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        UserGroupsInfo  GetUserGroupsInfo();
        #endregion

        /// <summary>
        /// 获取体验券信息
        /// </summary>
        /// <param name="loginId">登录标识</param>
        /// <param name="type">类型</param>
        /// <param name="isEffciive">是否启用</param>
        /// <param name="endTime">到期时间</param>
        /// <returns>ExperienceInfo</returns>
        [OperationContract]
        ExperienceInfo GetExperienceInfo(string loginId, int type, int isEffciive, DateTime? endTime);

        /// <summary>
        /// 添加体验券
        /// </summary>
        /// <param name="loginId">登录标识</param>
        /// <param name="exp">体验券</param>
        /// <returns>ResultDesc</returns>
        [OperationContract]
        ResultDesc AddExperience(string loginId, Experience exp);

        /// <summary>
        /// 编辑体验券
        /// </summary>
        /// <param name="loginId">登录标识</param>
        /// <param name="exp">体验券</param>
        /// <returns>ResultDesc</returns>
        [OperationContract]
        ResultDesc EditExperience(string loginId, Experience exp);

        /// <summary>
        /// 删除体验券
        /// </summary>
        /// <param name="loginId">登录标识</param>
        /// <param name="id">体验券标识</param>
        /// <returns>ResultDesc</returns>
        [OperationContract]
        ResultDesc DelExperience(string loginId, int id);
    }
}
