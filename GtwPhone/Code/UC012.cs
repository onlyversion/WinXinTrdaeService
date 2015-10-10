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
	/// 资金库存查询
	/// </summary>
	public class UC012 : ICode
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
				string TradeAccount = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/TradeAccount").InnerText;
				MoneyInventory miy = trade.GetMoneyInventory(LoginID);
				if (!miy.Result)
				{
					string CodeDesc = ResCode.UL005Desc;
					string ReturnCode = GssGetCode.GetCode(miy.ReturnCode, miy.Desc,ref CodeDesc);
					ResXml = GssResXml.GetResXml(ReqCode, ReturnCode, CodeDesc, string.Format("<DataBody></DataBody>"));
				}
				else
				{
					StringBuilder strb = new StringBuilder();
					strb.Append("<FundInfo>");
					strb.AppendFormat("<CashUser>{0}</CashUser>", miy.FdInfo.CashUser);
					strb.AppendFormat("<SubUser>{0}</SubUser>", miy.FdInfo.SubUser);
					strb.AppendFormat("<TanUser>{0}</TanUser>", miy.FdInfo.TanUser);
					strb.AppendFormat("<State>{0}</State>", miy.FdInfo.State);
					strb.AppendFormat("<Money>{0}</Money>", miy.FdInfo.Money);
					strb.AppendFormat("<OccMoney>{0}</OccMoney>", miy.FdInfo.OccMoney);
					strb.AppendFormat("<FrozenMoney>{0}</FrozenMoney>", miy.FdInfo.FrozenMoney);
					strb.AppendFormat("<ConBankType>{0}</ConBankType>", miy.FdInfo.ConBankType);
					strb.AppendFormat("<OpenBank>{0}</OpenBank>", miy.FdInfo.OpenBank);
					strb.AppendFormat("<BankAccount>{0}</BankAccount>", miy.FdInfo.BankAccount);
					strb.AppendFormat("<AccountName>{0}</AccountName>", miy.FdInfo.AccountName);
					strb.Append("</FundInfo>");

					strb.Append("<Storages>");

                    //strb.Append("<Storage>");
                    //strb.Append("<Key>XAU</Key>");
                    //strb.Append("<Name>黄金</Name>");
                    //strb.AppendFormat("<Value>{0}</Value>", miy.StorageQuantity.xau);
                    //strb.Append("</Storage>");

                    //strb.Append("<Storage>");
                    //strb.Append("<Key>XAG</Key>");
                    //strb.Append("<Name>白银</Name>");
                    //strb.AppendFormat("<Value>{0}</Value>", miy.StorageQuantity.xag);
                    //strb.Append("</Storage>");

                    //strb.Append("<Storage>");
                    //strb.Append("<Key>XPT</Key>");
                    //strb.Append("<Name>铂金</Name>");
                    //strb.AppendFormat("<Value>{0}</Value>", miy.StorageQuantity.xpt);
                    //strb.Append("</Storage>");

                    //strb.Append("<Storage>");
                    //strb.Append("<Key>XPD</Key>");
                    //strb.Append("<Name>钯金</Name>");
                    //strb.AppendFormat("<Value>{0}</Value>", miy.StorageQuantity.xpd);
                    //strb.Append("</Storage>");

					strb.Append("</Storages>");
					//响应消息体
					string DataBody = string.Format("<DataBody>{0}</DataBody>", strb);

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
