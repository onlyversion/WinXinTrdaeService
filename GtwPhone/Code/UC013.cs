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
	/// 商品配置查询
	/// </summary>
	public class UC013 : ICode
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
				string loginid = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/LoginId").InnerText;
				List<ProductConfig> list = trade.GetProductConfig(loginid);

				StringBuilder strb = new StringBuilder();
				foreach (ProductConfig pcg in list)
				{
					strb.Append("<Product>");
					strb.AppendFormat("<ProductCode>{0}</ProductCode>", pcg.ProductCode);
					strb.AppendFormat("<ProductName>{0}</ProductName>", pcg.ProductName);
					strb.AppendFormat("<GoodsCode>{0}</GoodsCode>", pcg.GoodsCode);
					strb.AppendFormat("<PriceCode>{0}</PriceCode>", pcg.PriceCode);
					strb.AppendFormat("<AdjustBase>{0}</AdjustBase>", pcg.AdjustBase);
					strb.AppendFormat("<AdjustCount>{0}</AdjustCount>", pcg.AdjustCount);
					strb.AppendFormat("<PriceDot>{0}</PriceDot>", pcg.PriceDot);
					strb.AppendFormat("<ValueDot>{0}</ValueDot>", pcg.ValueDot);
					strb.AppendFormat("<SetBase>{0}</SetBase>", pcg.SetBase);
					strb.AppendFormat("<HoldBase>{0}</HoldBase>", pcg.HoldBase);
					strb.AppendFormat("<OrdeMoney>{0}</OrdeMoney>", pcg.OrderMoney);
					strb.AppendFormat("<MaxPrice>{0}</MaxPrice>", pcg.MaxPrice);
					strb.AppendFormat("<MinPrice>{0}</MinPrice>", pcg.MinPrice);
					strb.AppendFormat("<MaxTime>{0}</MaxTime>", pcg.MaxTime);
					strb.AppendFormat("<State>{0}</State>", pcg.State);
					strb.AppendFormat("<Unit>{0}</Unit>", pcg.Unit);
					strb.Append("</Product>");
				}
				if (strb.Length > 0)
				{
					//响应消息体
					string DataBody = string.Format("<DataBody><Products>{0}</Products></DataBody>", strb);

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
