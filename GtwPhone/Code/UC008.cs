using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Trade;
using WcfInterface.model;
using WcfInterface;
namespace JtwPhone.Code
{
    /// <summary>
    /// 平仓记录查询
    /// </summary>
    public class UC008 : ICode
    {
        public String AnalysisXml(string ReqXml)
        {
            string ResXml = string.Empty;
            string ReqCode = string.Empty;
            try
            {
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
                xmldoc.LoadXml(ReqXml);

                //请求指令
                ReqCode = xmldoc.SelectSingleNode("JTW91G/MsgData/ReqHeader/ReqCode").InnerText;

                Trade.CTrade trade = new Trade.CTrade();

                string Ltype = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/Lqc/Ltype").InnerText;
                int pageindex = Convert.ToInt32(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/Lqc/PageIndex").InnerText);
                int pagesize = Convert.ToInt32(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/Lqc/PageSize").InnerText); ;
                int page = 0;
                LQueryCon Lcn = new LQueryCon();
                Lcn.LoginID = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/LoginId").InnerText;
                Lcn.TradeAccount = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/TradeAccount").InnerText;
             //   Lcn.UserType = 0;//客户端没有传递这个值 内部调用默认赋值0 表示普通用户
                Lcn.StartTime = Convert.ToDateTime(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/Lqc/StartTime").InnerText);
                Lcn.EndTime = Convert.ToDateTime(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/Lqc/EndTime").InnerText);
                Lcn.OrderType = "ALL";
                Lcn.ProductName = "ALL";
                List<LTradeOrder> list = trade.GetLTradeOrder(Lcn, Ltype, pageindex, pagesize, ref page);
                StringBuilder strb = new StringBuilder();
                foreach (LTradeOrder td in list)
                {
                    strb.Append("<HistoryOrder>");
                    strb.AppendFormat("<HistoryOrderId>{0}</HistoryOrderId>", td.HistoryOrderId);
                    strb.AppendFormat("<ProductName>{0}</ProductName>", td.ProductName);
                    strb.AppendFormat("<ProductCode>{0}</ProductCode>", td.ProductCode);
                    strb.AppendFormat("<OrderPrice>{0}</OrderPrice>", td.OrderPrice);
                    strb.AppendFormat("<OverType>{0}</OverType>", td.OverType);
                    strb.AppendFormat("<OverPrice>{0}</OverPrice>", td.OverPrice);
                    strb.AppendFormat("<OrderType>{0}</OrderType>", td.OrderType);
                    strb.AppendFormat("<ProfitValue>{0}</ProfitValue>", td.ProfitValue);
                    strb.AppendFormat("<TradeFee>{0}</TradeFee>", td.TradeFee);
                    strb.AppendFormat("<StorageFee>{0}</StorageFee>", td.StorageFee);
                    strb.AppendFormat("<OverTime>{0}</OverTime>", td.OverTime.ToString(Const.dateformat));
                    strb.AppendFormat("<OrderTime>{0}</OrderTime>", td.OrderTime.ToString(Const.dateformat));
                    strb.AppendFormat("<OrderId>{0}</OrderId>", td.OrderId);
                    strb.AppendFormat("<ProductMoney>{0}</ProductMoney>", td.ProductMoney);
                    strb.AppendFormat("<Quantity>{0}</Quantity>", td.Quantity);
                    strb.AppendFormat("<LossPrice>{0}</LossPrice>", td.LossPrice);
                    strb.AppendFormat("<ProfitPrice>{0}</ProfitPrice>", td.ProfitPrice);
                    strb.Append("</HistoryOrder>");
                }
                if (strb.Length > 0)
                {
                    //响应消息体
                    string DataBody = string.Format("<DataBody><PageCount>{0}</PageCount><HistoryOrders>{1}</HistoryOrders></DataBody>", page, strb);

                    ResXml = GssResXml.GetResXml(ReqCode, ResCode.UL004, ResCode.UL004Desc, DataBody);
                }
                else
                {
                    ResXml = GssResXml.GetResXml(ReqCode, ResCode.UL034, ResCode.UL034Desc, string.Format("<DataBody></DataBody>"));
                }

            }
            catch (Exception ex)
            {
                com.individual.helper.LogNet4.WriteErr(ex);

                //业务处理失败
                ResXml = GssResXml.GetResXml(ReqCode, ResCode.UL005, ResCode.UL005Desc, string.Format("<DataBody></DataBody>"));
            }
            return ResXml;
        }
    }
}
