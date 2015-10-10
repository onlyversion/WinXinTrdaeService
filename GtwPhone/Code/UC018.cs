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
	/// 获取历史数据
	/// </summary>
	public class UC018 : ICode
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
				//string loginid = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/LoginId").InnerText;
                string PriceCode = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/PriceCode").InnerText;
                string WeekFlg = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/WeekFlg").InnerText;

				List<HisData> list = trade.GetHisData(PriceCode,WeekFlg);

				StringBuilder strb = new StringBuilder();
                foreach (HisData his in list)
				{
                    strb.Append("<NewQuotationEntity>");
                    strb.AppendFormat("<time>{0}</time>", his.weektime);
                    strb.AppendFormat("<open>{0}</open>", his.openprice);
                    strb.AppendFormat("<hight>{0}</hight>", his.highprice);
                    strb.AppendFormat("<lower>{0}</lower>", his.lowprice);
                    strb.AppendFormat("<close>{0}</close>", his.closeprice);
                    strb.AppendFormat("<count>{0}</count>", his.volnum);
                    strb.Append("</NewQuotationEntity>");
				}
				if (strb.Length > 0)
				{
					//响应消息体
                    string DataBody = string.Format("<DataBody><NewQuotationEntitys>{0}</NewQuotationEntitys></DataBody>", strb);

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
