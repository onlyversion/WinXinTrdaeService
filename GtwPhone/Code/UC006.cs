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
    /// 市价单修改
    /// </summary>
    public class UC006 : ICode
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
                MarketLnEnter orderln = new MarketLnEnter();

                orderln.LoginID = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/LoginId").InnerText;
                orderln.TradeAccount = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/TradeAccount").InnerText;
                orderln.CurrentTime = Convert.ToDateTime(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/Orders/Order/CurrentTime").InnerText);
                orderln.OrderId = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/Orders/Order/OrderId").InnerText;
                //可以不设置止盈止损价 如果为空 不能直接转换 
                string ProfitPrice = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/Orders/Order/ProfitPrice").InnerText;
                string LossPrice = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/Orders/Order/LossPrice").InnerText;
                orderln.ProfitPrice = Convert.ToDouble(string.IsNullOrEmpty(ProfitPrice) ? "0" : ProfitPrice);
                orderln.LossPrice = Convert.ToDouble(string.IsNullOrEmpty(LossPrice) ? "0" : LossPrice);

                Marketorders orders = trade.ModifyMarketorders(orderln);

                if (!orders.Result)
                {
                    string CodeDesc = ResCode.UL005Desc;
                    string ReturnCode = GssGetCode.GetCode(orders.ReturnCode,orders.Desc, ref CodeDesc);
                    ResXml = GssResXml.GetResXml(ReqCode, ReturnCode, CodeDesc, string.Format("<DataBody></DataBody>"));
                }
                else
                {
                    StringBuilder strb = new StringBuilder();
                    strb.Append("<Order>");
                    strb.AppendFormat("<OrderId>{0}</OrderId>", orders.TradeOrder.OrderId);
                    strb.AppendFormat("<ProductName>{0}</ProductName>", orders.TradeOrder.ProductName);
                    strb.AppendFormat("<ProductCode>{0}</ProductCode>", orders.TradeOrder.ProductCode);
                    strb.AppendFormat("<PriceCode>{0}</PriceCode>", orders.TradeOrder.PriceCode);
                    strb.AppendFormat("<OrderPrice>{0}</OrderPrice>", orders.TradeOrder.OrderPrice);
                    strb.AppendFormat("<Quantity>{0}</Quantity>", orders.TradeOrder.Quantity);
                    strb.AppendFormat("<UseQuantity>{0}</UseQuantity>", orders.TradeOrder.UseQuantity);
                    strb.AppendFormat("<OccMoney>{0}</OccMoney>", orders.TradeOrder.OccMoney);
                    strb.AppendFormat("<LossPrice>{0}</LossPrice>", orders.TradeOrder.LossPrice);
                    strb.AppendFormat("<ProfitPrice>{0}</ProfitPrice>", orders.TradeOrder.ProfitPrice);
                    strb.AppendFormat("<OrderType>{0}</OrderType>", orders.TradeOrder.OrderType);
                    strb.AppendFormat("<OrderTime>{0}</OrderTime>", orders.TradeOrder.OrderTime.ToString(Const.dateformat));
                    strb.AppendFormat("<TradeFee>{0}</TradeFee>", orders.TradeOrder.TradeFee);
                    strb.AppendFormat("<StorageFee>{0}</StorageFee>", orders.TradeOrder.StorageFee);
                    strb.AppendFormat("<TotalWeight></TotalWeight>");
                    strb.Append("</Order>");
                    //响应消息体
                    string DataBody = string.Format("<DataBody><Orders>{0}</Orders></DataBody>", strb);

                    ResXml = GssResXml.GetResXml(ReqCode, ResCode.UL004, ResCode.UL004Desc, DataBody);
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
