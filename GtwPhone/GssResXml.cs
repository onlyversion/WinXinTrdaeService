using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JtwPhone
{
    /// <summary>
    /// 获取响应的XML
    /// </summary>
    public class GssResXml
    {
        /// <summary>
        /// 获取响应的XML
        /// </summary>
        /// <param name="ReqCode">请求代码</param>
        /// <param name="ResCode">响应代码</param>
        /// <param name="ResMsg">响应消息</param>
        /// <param name="DataBody">消息体</param>
        /// <returns></returns>
        public static string GetResXml(string ReqCode, string ResCode, string ResMsg, string DataBody)
        {
            string ResXml = string.Empty;
            try
            {
                //响应消息头
                string ResHeader = string.Format("<ResHeader><ServerTime>{0}</ServerTime><Status><ReqCode>{1}</ReqCode><ResCode>{2}</ResCode><ResMsg>{3}</ResMsg></Status></ResHeader>",
                                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                ReqCode, ResCode, ResMsg);
                //初始化RSA密钥对 
                com.individual.helper.RsaHelper.IniRsaKeyPairGenerator();
                //用RSA私钥对消息进行数字签名
                string SignData = string.Format("<SignData><Signature>{0}</Signature><RsaPubKey>{1}</RsaPubKey></SignData>",
                            com.individual.helper.RsaSha1Helper.sign(string.Format("<MsgData>{0}{1}</MsgData>", ResHeader, DataBody), com.individual.helper.RsaHelper.RsaPrivateKey),
                            com.individual.helper.RsaHelper.RsaPublicKey);
                ResXml = string.Format("<JTW91G><MsgData>{0}{1}</MsgData>{2}</JTW91G>", ResHeader, DataBody, SignData);
            }
            catch (Exception ex)
            {
                com.individual.helper.LogNet4.WriteErr(ex);
            }
            return ResXml;
        }
    }
}
