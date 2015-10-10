using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace TradeService
{
    public class PageBase : System.Web.UI.Page
    {
        /// <summary>
        /// 显示 浏览器的Alert。
        /// </summary>
        /// <param name="text">消息文本。</param>
        public void MessageBox(string text)
        {
            text = text == null ? "" : text;
            ClientScriptManager csm = this.ClientScript;
            Type csType = this.GetType();
            if (!csm.IsStartupScriptRegistered(csType, this.UniqueID + "alert"))
            {
                text = text.Replace("\r", "\\r");
                text = text.Replace("\n", "\\n");
                text = text.Replace("\"", "\\'");
                string js = "window.alert(\"" + text + "\");";
                csm.RegisterStartupScript(csType, this.UniqueID + "alert", js, true);
            }
        }
    }
}