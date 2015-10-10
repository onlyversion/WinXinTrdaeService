using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WcfInterface;
namespace JtwPhone
{

    public class Phone : Iphone
    {
        public String Process(String ReqXml)
        {
            String ResXml = string.Empty;
            string ReqCode = string.Empty;
            try
            {
                com.individual.helper.LogNet4.WriteMsg(ReqXml);
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
                xmldoc.LoadXml(ReqXml);

                //待验证数据
                string MsgData = xmldoc.SelectSingleNode("JTW91G/MsgData").OuterXml;

                //数字签名
                string Signature = xmldoc.SelectSingleNode("JTW91G/SignData/Signature").InnerText;

                //RSA公钥
                string RsaPubKey = xmldoc.SelectSingleNode("JTW91G/SignData/RsaPubKey").InnerText;

                //请求指令
                ReqCode = xmldoc.SelectSingleNode("JTW91G/MsgData/ReqHeader/ReqCode").InnerText;

                if (com.individual.helper.RsaSha1Helper.verify(MsgData, RsaPubKey, Signature))
                {                    //系统类型
                    string OsType = xmldoc.SelectSingleNode("JTW91G/MsgData/ReqHeader/OsType").InnerText;
                    if (string.IsNullOrEmpty(OsType) || ("0" != OsType && "1" != OsType && "2" != OsType))
                    {
                        return GssResXml.GetResXml(ReqCode, ResCode.UL037, ResCode.UL037Desc, string.Format("<DataBody></DataBody>"));
                    }
                    //反射出具体的实现类
                    Type t = Type.GetType("JtwPhone.Code." + ReqCode);
                    //获取方法
                    System.Reflection.MethodInfo minfo = t.GetMethod("AnalysisXml");
                    //执行方法
                    ResXml = minfo.Invoke(System.Activator.CreateInstance(t), new Object[] { ReqXml }).ToString();
                }
                else //验证签名失败
                {
                    ResXml = GssResXml.GetResXml(ReqCode, ResCode.UL006, ResCode.UL006Desc, string.Format("<DataBody></DataBody>"));
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
