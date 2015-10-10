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
    /// 限价下单
    /// </summary>
    public class UC001 : ICode
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
                OrdersLncoming orderln = new OrdersLncoming();

                orderln.Mac = xmldoc.SelectSingleNode("JTW91G/MsgData/ReqHeader/Mac").InnerText;
                orderln.LoginID = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/LoginId").InnerText;
                orderln.TradeAccount = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/TradeAccount").InnerText;
                orderln.CurrentTime = Convert.ToDateTime(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/HoldOrders/HoldOrder/CurrentTime").InnerText);

                //可以不设置止盈止损价 如果为空 不能直接转换 
                string ProfitPrice = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/HoldOrders/HoldOrder/ProfitPrice").InnerText;
                string LossPrice = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/HoldOrders/HoldOrder/LossPrice").InnerText;
                orderln.ProfitPrice = Convert.ToDouble(string.IsNullOrEmpty(ProfitPrice) ? "0" : ProfitPrice);
                orderln.LossPrice = Convert.ToDouble(string.IsNullOrEmpty(LossPrice) ? "0" : LossPrice);

                orderln.OrderType = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/HoldOrders/HoldOrder/OrderType").InnerText;
                orderln.ProductCode = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/HoldOrders/HoldOrder/ProductCode").InnerText;
                orderln.HoldPrice = Convert.ToDouble(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/HoldOrders/HoldOrder/HoldPrice").InnerText);
                orderln.Quantity = Convert.ToDouble(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/HoldOrders/HoldOrder/Quantity").InnerText);
                orderln.RtimePrices = Convert.ToDouble(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/HoldOrders/HoldOrder/RtimePrices").InnerText);
                orderln.ValidTime = Convert.ToDateTime(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/HoldOrders/HoldOrder/ValidTime").InnerText);//ValidTime

                orderln.UserType = 0; //客户端没有传递这个值 内部调用默认赋值0 表示普通用户
                orderln.OrderMoney = 0; //客户端没有传递这个值 这个值本身也没有使用 随便赋个值
                Orders orders = trade.GetOrders(orderln);

                if (!orders.Result)
                {
                    string CodeDesc = ResCode.UL005Desc;
                    string ReturnCode = GssGetCode.GetCode(orders.ReturnCode,orders.Desc, ref CodeDesc);
                    ResXml = GssResXml.GetResXml(ReqCode, ReturnCode, CodeDesc, string.Format("<DataBody></DataBody>"));
                }
                else
                {
                    //返回的结构不一样 不能直接转换
                    //string holdorder = XmlUtil.SerializeObjToXml(typeof(TradeHoldOrder), orders.TradeHoldOrder);

                    StringBuilder strb = new StringBuilder();
                    strb.AppendFormat("<HoldOrderId>{0}</HoldOrderId>", orders.TradeHoldOrder.HoldOrderID);
                    strb.AppendFormat("<ProductCode>{0}</ProductCode>", orders.TradeHoldOrder.ProductCode);
                    strb.AppendFormat("<HoldPrice>{0}</HoldPrice>", orders.TradeHoldOrder.HoldPrice);
                    strb.AppendFormat("<Quantity>{0}</Quantity>", orders.TradeHoldOrder.Quantity);
                    strb.AppendFormat("<FrozenMoney>{0}</FrozenMoney>", orders.TradeHoldOrder.FrozenMoney);
                    strb.AppendFormat("<ValidTime>{0}</ValidTime>", orders.TradeHoldOrder.ValidTime.ToString(Const.dateformat));
                    strb.AppendFormat("<LossPrice>{0}</LossPrice>", orders.TradeHoldOrder.LossPrice);
                    strb.AppendFormat("<ProfitPrice>{0}</ProfitPrice>", orders.TradeHoldOrder.ProfitPrice);
                    strb.AppendFormat("<OrderType>{0}</OrderType>", orders.TradeHoldOrder.OrderType);
                    strb.AppendFormat("<OrderTime>{0}</OrderTime>", orders.TradeHoldOrder.OrderTime.ToString(Const.dateformat));
                    strb.Append("<CancelTime></CancelTime>");
                    StringBuilder fundinfo = new StringBuilder();
                    fundinfo.Append("<FundInfo>");
                    fundinfo.AppendFormat("<Money>{0}</Money>", orders.MoneyInventory.FdInfo.Money);
                    fundinfo.AppendFormat("<OccMoney>{0}</OccMoney>", orders.MoneyInventory.FdInfo.OccMoney);
                    fundinfo.AppendFormat("<FrozenMoney>{0}</FrozenMoney>", orders.MoneyInventory.FdInfo.FrozenMoney);
                    fundinfo.Append("</FundInfo>");
                    //响应消息体
                    string DataBody = string.Format("<DataBody><HoldOrders><HoldOrder>{0}</HoldOrder></HoldOrders>{1}</DataBody>", strb.ToString(), fundinfo.ToString());

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
