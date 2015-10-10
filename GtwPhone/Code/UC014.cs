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
	/// 系统设置查询
	/// </summary>
	public class UC014 : ICode
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

				TradeSetInfo setinfo = trade.GetTradeSetInfo(LoginID);
				StringBuilder strb = new StringBuilder();
				foreach (TradeSet st in setinfo.TdSetList)
				{
					strb.Append("<Configuration>");
					strb.AppendFormat("<Key>{0}</Key>", st.ObjCode);
					strb.AppendFormat("<Value>{0}</Value>", st.ObjValue);
					strb.Append("</Configuration>");
				}
				if (strb.Length > 0)
				{
					//响应消息体
					string DataBody = string.Format("<DataBody><Configurations>{0}</Configurations></DataBody>", strb);

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
