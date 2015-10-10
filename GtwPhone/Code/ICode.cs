using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JtwPhone.Code
{
    public interface ICode
    {
        /// <summary>
        /// 用户请求
        /// </summary>
        /// <param name="reqXml"></param>
        /// <returns></returns>
        String AnalysisXml(String reqXml);
    }

}
