using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json ;
using System.IO;
using System.Data;
using System.Xml.Serialization;

namespace JinTong.Jyrj.Common
{
    /// <summary>
    /// 字符串转换XML格式
    /// </summary>
    public class StringToXML
    {
        /// <summary>
        /// xml字符串转换为DataTable
        /// </summary>
        /// <param name="sJson">xml字符串</param>
        /// <returns></returns>
        public static DataTable JsonDataTable(string sJson)
        {
            DataSet ds = new DataSet();
            StringReader stream = new StringReader(sJson);
            XmlTextReader reader = new XmlTextReader(stream);
            ds.ReadXml(reader);          
            return ds.Tables[0];
        }
        /// <summary>
        /// DataTable字符串转换为 xml
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string SerializeDataTableXml(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            serializer.Serialize(writer, dt);
            writer.Close();
            return sb.ToString();
        }
    }
}
