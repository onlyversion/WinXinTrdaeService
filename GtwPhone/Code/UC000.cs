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
    /// 登陆
    /// </summary>
    public class UC000 : ICode
    {
        /// <summary>
        /// 私钥
        /// </summary>
        private const string RsaPrivateKey = @"MIICcwIBADANBgkqhkiG9w0BAQEFAASCAl0wggJZAgEAAoGBAINK5LR8kh6k53Q8252VxviJSgbvuGqO
                                                Ae2ll7T1XBPjo9wklACrYSB17Z2aEKhIfOIcvbSTtDqHobBK4oRqgJdkAmXkZ3lTBsmhCoiW65G/X1Zw
                                                Yhknvxr7jeIrTU6OmxMnH974D3RSLYj609Ob23zAYg0bW3wkZkLQ2+T8xgiNAgEDAoGAV4dDIv22vxia
                                                TX3nvmPZ+wYxWfUlnF6r88O6eKOSt+0X6BhiqxzravlJE7wLGtr97BMpIw0i0a/BIDHsWEcAY/jOAZVM
                                                Ln+u/M5t8csy+fnPLwYy/tpIoQWcvP98mU3zAwYiChi7Bs85JWDRertXGtgUGyFPLUUdMjUInu6cvdMC
                                                QQC8aPSUso3K4INEvYRPhFasH/yEfJgo87qfrrBGdFhsib2huwM2chO/0ARP0cfTjas9CHo40zRrQ2Rh
                                                o7x54p5LAkEAsmRu78KlyJ/LJqgZlprEHIiTYpkCt15u03QWZZ4QLSTQ/DHMnITWW4fMmcfTr0spPztq
                                                MJGD0XcTH6s6HPhNhwJAfZtNuHcJMesCLdOtilg5yBVTAv26xffRv8nK2aLlnbEpFnyszva31TVYNTaF
                                                N7PHfgWm0IzNnNeYQRfS++xphwJAdu2fSoHD2xUyGcVmZGctaFsM7GYBz5RJ4k1kQ761c23gqCEzEwM5
                                                klqIZoU3yjIbf3zxdbZX4PoMv8d8E1AzrwJAD/sEdjM/slLR1I9uKM69ufhhpMQfGW2JIxyfx9sGXd4z
                                                OkD8K/PHIY2U6p+uNQOKgQbRMTbwiynKh3HvdW7LRQ==";

        public String AnalysisXml(string ReqXml)
        {
            string ResXml = string.Empty;
            string ReqCode = string.Empty;
            try
            {
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
                xmldoc.LoadXml(ReqXml);
                //获取账号
                string TradeAccount = xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/TradeAccount").InnerText;
                //获取密码并解码
                string TradePwd = com.individual.helper.RsaHelper.Decrypt(xmldoc.SelectSingleNode("JTW91G/MsgData/DataBody/TradePwd").InnerText, RsaPrivateKey);
                //获取MAC地址
                string Mac = xmldoc.SelectSingleNode("JTW91G/MsgData/ReqHeader/Mac").InnerText;

                //请求指令
                ReqCode = xmldoc.SelectSingleNode("JTW91G/MsgData/ReqHeader/ReqCode").InnerText;

                Trade.CTrade trade = new Trade.CTrade();
                Loginfo lg = trade.GetLogin(TradeAccount, TradePwd, Mac);

                if ("-1" == lg.LoginID)
                {
                    //登陆失败
                    ResXml = GssResXml.GetResXml(ReqCode, ResCode.UL002, ResCode.UL002Desc, string.Format("<DataBody></DataBody>"));
                }
                else
                {
                    StringBuilder UserLimitInfo = new StringBuilder();
                    UserLimitInfo.AppendFormat("<MinTrade>{0}</MinTrade>", lg.TdUser.MinTrade);
                    UserLimitInfo.AppendFormat("<OrderUnit>{0}</OrderUnit>", lg.TdUser.OrderUnit);
                    UserLimitInfo.AppendFormat("<MaxTrade>{0}</MaxTrade>", lg.TdUser.MaxTrade);
                    UserLimitInfo.AppendFormat("<PermitRcash>{0}</PermitRcash>", lg.TdUser.PermitRcash ? 1 : 0);
                    UserLimitInfo.AppendFormat("<PermitCcash>{0}</PermitCcash>", lg.TdUser.PermitCcash ? 1 : 0);
                    UserLimitInfo.AppendFormat("<PermitDhuo>{0}</PermitDhuo>", lg.TdUser.PermitDhuo ? 1 : 0);
                    UserLimitInfo.AppendFormat("<PermitHshou>{0}</PermitHshou>", lg.TdUser.PermitHshou ? 1 : 0);
                    UserLimitInfo.AppendFormat("<PermitRstore>{0}</PermitRstore>", lg.TdUser.PermitRstore ? 1 : 0);
                    UserLimitInfo.AppendFormat("<PermitDelOrder>{0}</PermitDelOrder>", lg.TdUser.PermitDelOrder ? 1 : 0);
                    //响应消息体
                    string DataBody = string.Format("<DataBody><LoginId>{0}</LoginId><QuotesAddressIP>{1}</QuotesAddressIP><QuotesPort>{2}</QuotesPort><UserLimitInfo>{3}</UserLimitInfo></DataBody>",
                        lg.LoginID, lg.QuotesAddressIP, lg.QuotesPort, UserLimitInfo.ToString());

                    ResXml = GssResXml.GetResXml(ReqCode, ResCode.UL001, ResCode.UL001Desc, DataBody);
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
