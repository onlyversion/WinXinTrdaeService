using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WcfInterface;
namespace JtwPhone.Code
{
    /// <summary>
    /// 卖单入库
    /// </summary>
    public class UC010:ICode
    {
        public String AnalysisXml(string ReqXml)
        {
            UC009 uc009 = new UC009();
            return uc009.AnalysisXml(ReqXml);
        }
    }
}
