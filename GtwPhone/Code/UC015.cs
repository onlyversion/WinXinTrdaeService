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
	/// 注册模拟账号
	/// </summary>
	public class UC015 : ICode
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

				string UserName = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/RegInfo/UserName").InnerText;
				string TradeAccount = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/RegInfo/TradeAccount").InnerText;
				string TradePwd = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/RegInfo/TradePwd").InnerText;
				string PhoneNum = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/RegInfo/PhoneNum").InnerText;
				string Email = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/RegInfo/Email").InnerText;
				string CardNum = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/RegInfo/CardNum").InnerText;

				ResultDesc rdsc = trade.RegTestTradeUser(UserName, TradeAccount, TradePwd, PhoneNum, Email, CardNum);

				if (!rdsc.Result)
				{
					string CodeDesc = ResCode.UL005Desc;
					string ReturnCode = GssGetCode.GetCode(rdsc.ReturnCode,rdsc.Desc, ref CodeDesc);
					ResXml = GssResXml.GetResXml(ReqCode, ReturnCode, CodeDesc, string.Format("<DataBody></DataBody>"));
				}
				else
				{
					ResXml = GssResXml.GetResXml(ReqCode, ResCode.UL004, ResCode.UL004Desc, string.Format("<DataBody></DataBody>"));
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
