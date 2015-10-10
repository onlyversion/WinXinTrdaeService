using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.individual.helper;
using WcfInterface;
using System.Net;
using System.Xml;
using System.Configuration;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
           //测试通联支付 出金接口
            string address = string.Format(@"{1}?accountName={0}&accountNO={2}&amount={3}&bankCode={4}", "张国光",
                ConfigurationManager.AppSettings["BankAddress"], "6225882516636351", 100, "0105");
            try
            {
                WebClient wc = new WebClient();
                byte[] bResponse = wc.DownloadData(address);
                string strResponse = Encoding.UTF8.GetString(bResponse);
                XmlDocument rexml = new XmlDocument();
                rexml.LoadXml(strResponse);
                string ret_code = rexml.SelectSingleNode("AIPG/TRANSRET/RET_CODE").InnerXml;
                string err_msg = rexml.SelectSingleNode("AIPG/TRANSRET/ERR_MSG").InnerXml;
                Console.WriteLine(strResponse);
                Console.WriteLine(ret_code);
                Console.WriteLine(err_msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

    }
}
