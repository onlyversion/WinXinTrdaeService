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
    /// 限价单取消
    /// </summary>
    public class UC003 : ICode
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
                DelHoldInfo DhInfo = new DelHoldInfo();

                DhInfo.LoginID = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/LoginId").InnerText;
                DhInfo.TradeAccount = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/TradeAccount").InnerText;
                DhInfo.CurrentTime = Convert.ToDateTime(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/HoldOrders/HoldOrder/CurrentTime").InnerText);
                DhInfo.HoldOrderID = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/HoldOrders/HoldOrder/HoldOrderId").InnerText;

                DhInfo.UserType = 0; //客户端没有传递这个值 内部调用默认赋值0 表示普通用户
                DhInfo.ReasonType = "1"; //手动取消
                MarDelivery mdy = trade.DelHoldOrder(DhInfo);

                if (!mdy.Result)
                {
                    string CodeDesc = ResCode.UL005Desc;
                    string ReturnCode = GssGetCode.GetCode(mdy.ReturnCode,mdy.Desc, ref CodeDesc);
                    ResXml = GssResXml.GetResXml(ReqCode, ReturnCode, CodeDesc, string.Format("<DataBody></DataBody>"));
                }
                else
                {
                    StringBuilder fundinfo = new StringBuilder();
                    fundinfo.Append("<FundInfo>");
                    fundinfo.AppendFormat("<Money>{0}</Money>", mdy.MoneyInventory.FdInfo.Money);
                    fundinfo.AppendFormat("<OccMoney>{0}</OccMoney>", mdy.MoneyInventory.FdInfo.OccMoney);
                    fundinfo.AppendFormat("<FrozenMoney>{0}</FrozenMoney>", mdy.MoneyInventory.FdInfo.FrozenMoney);
                    fundinfo.Append("</FundInfo>");
                    ResXml = GssResXml.GetResXml(ReqCode, ResCode.UL004, ResCode.UL004Desc, string.Format("<DataBody>{0}</DataBody>", fundinfo.ToString()));
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
