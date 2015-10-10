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
    /// 市价单查询
    /// </summary>
    public class UC004 : ICode
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

                string LoginID = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/LoginId").InnerText;
                string TradeAccount = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/TradeAccount").InnerText;

             //   int UserType = 0; //客户端没有传递这个值 内部调用默认赋值0 表示普通用户

                List<TradeOrder> list = trade.GetTradeOrder(LoginID);
                StringBuilder strb = new StringBuilder();
                foreach (TradeOrder td in list)
                {
                    strb.Append("<Order>");
                    strb.AppendFormat("<OrderId>{0}</OrderId>", td.OrderId);
                    strb.AppendFormat("<ProductName>{0}</ProductName>", td.ProductName);
                    strb.AppendFormat("<ProductCode>{0}</ProductCode>", td.ProductCode);
                    strb.AppendFormat("<PriceCode>{0}</PriceCode>", td.PriceCode);
                    strb.AppendFormat("<OrderPrice>{0}</OrderPrice>", td.OrderPrice);
                    strb.AppendFormat("<Quantity>{0}</Quantity>", td.Quantity);
                    strb.AppendFormat("<UseQuantity>{0}</UseQuantity>", td.UseQuantity);
                    strb.AppendFormat("<OccMoney>{0}</OccMoney>", td.OccMoney);
                    strb.AppendFormat("<LossPrice>{0}</LossPrice>", td.LossPrice);
                    strb.AppendFormat("<ProfitPrice>{0}</ProfitPrice>", td.ProfitPrice);
                    strb.AppendFormat("<OrderType>{0}</OrderType>", td.OrderType);
                    strb.AppendFormat("<OrderTime>{0}</OrderTime>", td.OrderTime.ToString(Const.dateformat));
                    strb.AppendFormat("<TradeFee>{0}</TradeFee>", td.TradeFee);
                    strb.AppendFormat("<StorageFee>{0}</StorageFee>", td.StorageFee);
                    strb.AppendFormat("<TotalWeight>{0}</TotalWeight>", td.TotalWeight);
                    strb.Append("</Order>");
                }
                if (strb.Length > 0)
                {
                    //响应消息体
                    string DataBody = string.Format("<DataBody><Orders>{0}</Orders></DataBody>", strb);

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
