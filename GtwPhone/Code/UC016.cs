using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WcfInterface;
namespace JtwPhone.Code
{
	/// <summary>
	/// 获取最后一根蜡烛图
	/// </summary>
	public class UC016 : ICode
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

				string PCode = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/PriceCode").InnerText;
				string WFlg = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/WeekFlg").InnerText;

				//行情编码错误
                //if (PriceCode.XAUUSD != PCode && PriceCode.XPTUSD != PCode && PriceCode.XPDUSD != PCode && PriceCode.XAGUSD != PCode)
                //{
                //    return GssResXml.GetResXml(ReqCode, ResCode.UL032, ResCode.UL032Desc, string.Format("<DataBody></DataBody>"));
                //}
				//周期编码错误
				if (WeekFlg.M1 != WFlg && WeekFlg.M5 != WFlg && WeekFlg.M15 != WFlg &&
					WeekFlg.M30 != WFlg && WeekFlg.H1 != WFlg && WeekFlg.H4 != WFlg &&
					WeekFlg.D1 != WFlg && WeekFlg.W1 != WFlg && WeekFlg.MN != WFlg)
				{
					return GssResXml.GetResXml(ReqCode, ResCode.UL033, ResCode.UL033Desc, string.Format("<DataBody></DataBody>"));
				}

				LastPillar.CPillar cp = new LastPillar.CPillar();
				com.gss.common.datatype dtype = com.gss.common.datatype.D1;
				switch (WFlg)
				{
					case "M1":
						dtype = com.gss.common.datatype.M1;
						break;
					case "M5":
						dtype = com.gss.common.datatype.M5;
						break;
					case "M15":
						dtype = com.gss.common.datatype.M15;
						break;
					case "M30":
						dtype = com.gss.common.datatype.M30;
						break;
					case "H1":
						dtype = com.gss.common.datatype.H1;
						break;
					case "H4":
						dtype = com.gss.common.datatype.H4;
						break;
					case "D1":
						dtype = com.gss.common.datatype.D1;
						break;
					case "W1":
						dtype = com.gss.common.datatype.W1;
						break;
					case "MN":
						dtype = com.gss.common.datatype.MN;
						break;
				}
				string Candle = cp.GetLastPillar(PCode, dtype);

				ResXml = GssResXml.GetResXml(ReqCode, ResCode.UL004, ResCode.UL004Desc, string.Format("<DataBody><Candle>{0}\t00</Candle></DataBody>", Candle));

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
