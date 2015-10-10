using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GssManager;

namespace TradeService
{
    public partial class News : PageBase
    {
        /// <summary>
        /// 登录ID
        /// </summary>
        private string LoginId
        {
            get
            {
                return Request.QueryString["LoginId"] == null ? string.Empty : Request.QueryString["LoginId"].ToString();
            }
        }
        /// <summary>
        /// 新闻ID
        /// </summary>
        private string ID
        {
            get
            {
                return Request.QueryString["ID"] == null ? string.Empty : Request.QueryString["ID"].ToString();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (GetManager.CheckUserLogin(LoginId) == false)
                //{
                //    Response.Redirect("Error.aspx");
                //}
                //else
                //{
                    InitPage();
                //}
            }
        }
        private CManager _cManager;
        private CManager GetManager
        {
            get
            {
                if (_cManager == null) _cManager = new CManager(); return _cManager;
            }
        }
        private void InitPage()
        {
            if (!string.IsNullOrEmpty(ID))
            {
                DataTable dt = new DataTable();
                dt = GetManager.GetNewsInfo(ID);
                if (dt.Rows.Count > 0)
                {
                    labTitle.Text = dt.Rows[0]["NewsTitle"].ToString();
                    divContent.InnerHtml = dt.Rows[0]["NewsContent"].ToString();
                    //labPubPerson.Text = dt.Rows[0]["PubPerson"].ToString();
                    //labPubTime.Text = dt.Rows[0]["PubTime"].ToString();
                }
                else
                {
                    MessageBox("没有查到新闻数据.");
                }
            }
            else
            {
                MessageBox("新闻id不能为空.");
            }
        }
    }
}