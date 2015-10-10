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
    /// 限价挂单查询
    /// </summary>
    public class UC002 : ICode
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

                string LoginId = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/LoginId").InnerText;
                string TradeAccount = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/TradeAccount").InnerText;

                List<TradeHoldOrder> list = trade.GetTradeHoldOrder( LoginId);

                StringBuilder strb = new StringBuilder();
                foreach (TradeHoldOrder td in list)
                {
                    strb.Append("<HoldOrder>");
                    strb.AppendFormat("<ProductName>{0}</ProductName>", td.ProductName);
                    strb.AppendFormat("<HoldOrderId>{0}</HoldOrderId>", td.HoldOrderID);
                    strb.AppendFormat("<ProductCode>{0}</ProductCode>", td.ProductCode);
                    strb.AppendFormat("<HoldPrice>{0}</HoldPrice>", td.HoldPrice);
                    strb.AppendFormat("<Quantity>{0}</Quantity>", td.Quantity);
                    strb.AppendFormat("<FrozenMoney>{0}</FrozenMoney>", td.FrozenMoney);
                    strb.AppendFormat("<ValidTime>{0}</ValidTime>", td.ValidTime.ToString(Const.dateformat));
                    strb.AppendFormat("<LossPrice>{0}</LossPrice>", td.LossPrice);
                    strb.AppendFormat("<ProfitPrice>{0}</ProfitPrice>", td.ProfitPrice);
                    strb.AppendFormat("<OrderType>{0}</OrderType>", td.OrderType);
                    strb.AppendFormat("<OrderTime>{0}</OrderTime>", td.OrderTime.ToString(Const.dateformat));
                    strb.Append("<CancelTime></CancelTime>");
                    strb.Append("</HoldOrder>");
                }
                if (strb.Length > 0)
                {
                    //响应消息体
                    string DataBody = string.Format("<DataBody><HoldOrders>{0}</HoldOrders></DataBody>", strb);

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
