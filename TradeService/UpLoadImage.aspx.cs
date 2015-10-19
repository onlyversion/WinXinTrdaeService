using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WcfInterface;

namespace TradeService
{
    public partial class UpLoadImage : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //foreach (string f in Request.Files.AllKeys)
            //{
            //    HttpPostedFile file = Request.Files[f];
            //    file.SaveAs(@"d:/" + file.FileName);
            //}
            try
            {
                HttpPostedFile file = Request.Files[0];
                //file.SaveAs(MapPath("\\AdviceImage\\" + file.FileName));
                file.SaveAs(ConfigurationManager.AppSettings["ImgAddress"]+ file.FileName);
                //ComFunction.WriteErr(new Exception(ConfigurationManager.AppSettings["ImgAddress"] + file.FileName));
                Response.Write("Success\r\n");
            }
            catch (Exception ex)
            {
                ComFunction.WriteErr(ex);
                
            }

        }
    }
}