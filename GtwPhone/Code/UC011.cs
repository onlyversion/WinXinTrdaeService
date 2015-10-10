using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Trade;
using WcfInterface.model;
using WcfInterface;
using WcfInterface;
namespace JtwPhone.Code
{

	/// <summary>
	/// 新闻公告查询
	/// </summary>
	public class UC011 : ICode
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

                int NType = Convert.ToInt32(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/Lqc/NewsType").InnerText);
                NewsLqc Lqc = new NewsLqc();
                Lqc.LoginID = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/LoginId").InnerText;
                Lqc.StartTime = Convert.ToDateTime(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/Lqc/StartTime").InnerText);
                Lqc.EndTime = Convert.ToDateTime(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/Lqc/EndTime").InnerText);
                Lqc.NType = (NewsType)NType;
                int pageindex = Convert.ToInt32(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/Lqc/PageIndex").InnerText);
                int pagesize = Convert.ToInt32(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/Lqc/PageSize").InnerText);
                int page = 0;
                TradeNewsInfo newsinfo = trade.GetTradeNewsInfoWithPage(Lqc, pageindex, pagesize, ref page);
                StringBuilder strb = new StringBuilder();
                foreach (TradeNews tn in newsinfo.TradeNewsInfoList)
                {
                    strb.Append("<News>");
                    strb.AppendFormat("<NewsId>{0}</NewsId>", tn.ID);
                    strb.AppendFormat("<Title>{0}</Title>", tn.NewsTitle);
                    strb.AppendFormat("<NewsType>{0}</NewsType>", (int)tn.NType);
                    strb.AppendFormat("<Detail>{0}</Detail>", tn.NewsContent.Replace("src=\"/", string.Format("src=\"http://{0}/", ComFunction.NewsHostAddr)));
                    strb.AppendFormat("<Time>{0}</Time>", tn.PubTime.ToString(Const.dateformat));
                    strb.AppendFormat("<Publisher>{0}</Publisher>", tn.PubPerson);
                    strb.AppendFormat("<Status>{0}</Status>", (int)tn.Status);
                    strb.Append("</News>");
                }
                if (strb.Length > 0)
                {
                    //响应消息体
                    string DataBody = string.Format("<DataBody><PageCount>{0}</PageCount><AllNews>{1}</AllNews></DataBody>", page, strb);

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
