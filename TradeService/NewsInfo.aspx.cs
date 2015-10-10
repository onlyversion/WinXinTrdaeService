using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WcfInterface.model;
using GssManager;
using System.Data;

namespace TradeService
{
    public partial class NewsInfo : PageBase
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

        /// <summary>
        /// 信息状态（新增，修改)
        /// </summary>
        protected string State
        {
            get
            {
                return Request.QueryString["Mode"] == null ? string.Empty : Request.QueryString["Mode"].ToString();
            }
        }

        /// <summary>
        /// 发布人
        /// </summary>
        protected string Account
        {
            get
            {
                return Request.QueryString["Account"] == null ? string.Empty : Request.QueryString["Account"].ToString();
            }
        }
        private CManager _cManager;
        private CManager GetManager
        {
            get
            {
              if (_cManager == null)_cManager = new CManager(); return _cManager; 
            }           
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (GetManager.CheckUserLogin(LoginId) == false)
                {
                    Response.Redirect("Error.aspx");
                }
                else
                {
                    if (!string.IsNullOrEmpty(ID))
                    {
                        DataTable dt = new DataTable();
                        dt = GetManager.GetNewsInfo(ID);
                        if (dt.Rows.Count > 0)
                        {
                            txtTitle.Text = System.DBNull.Value != dt.Rows[0]["NewsTitle"] ? dt.Rows[0]["NewsTitle"].ToString() : string.Empty;
                            NewsContent.InnerText = dt.Rows[0]["NewsContent"].ToString();
                            //labPubPerson.Text = dt.Rows[0]["PubPerson"].ToString();
                            rbEnable.Checked = dt.Rows[0]["Status"].ToString() == "1" ? true : false;
                            rbDisable.Checked = dt.Rows[0]["Status"].ToString() == "0" ? true : false;
                            ddlType.SelectedValue = dt.Rows[0]["NewsType"].ToString();
                            txtOverView.Text = System.DBNull.Value != dt.Rows[0]["OverView"] ? dt.Rows[0]["OverView"].ToString() : string.Empty;
                            //labPubTime.Text = dt.Rows[0]["PubTime"].ToString();
                        }
                    }
                    //else
                    //{
                    //    labPubTime.Text = DateTime.Now.ToString();
                    //    labPubPerson.Text = Account;
                    //}
                }
            }
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            Save();
        }
        private void Save()
        {
            TradeNews news = new TradeNews();
            news.NewsTitle = txtTitle.Text;
            news.OverView = txtOverView.Text;
            news.NewsContent = NewsContent.InnerText;
            if (string.IsNullOrEmpty(news.NewsContent))
            {
                MessageBox("请填写内容!");
                return;
            }
            if (string.IsNullOrEmpty(news.NewsTitle))
            {
                MessageBox("请填写标题!");
                return;
            }
            if (string.IsNullOrEmpty(news.OverView))
            {
                MessageBox("请填写摘要!");
                return;
            }
            if (news.NewsContent.Length > 4000)
            {
                MessageBox("内容不能超过4000字符!");
                return;
            }
            if (news.NewsTitle.Length > 200)
            {
                MessageBox("标题不能超过200字符!");
                return;
            }
            if (news.OverView.Length > 2000)
            {
                MessageBox("摘要不能超过2000字符!");
                return;
            }
            int type = 0;
            if (!string.IsNullOrEmpty(ddlType.SelectedItem.Value))
            {
                type = int.Parse(ddlType.SelectedItem.Value);
            }
            else
            {
                MessageBox("请选择新闻类型!");
                return;
            }
            news.NType = (NewsType)type;
            news.PubPerson = Account;
            int status=1;
            status=rbEnable.Checked ? 1 : 0;
            news.Status = (NewsStatus)status;
           
            ResultDesc result = new ResultDesc();

            if (string.IsNullOrEmpty(ID))
            {
             news.ID = Guid.NewGuid().ToString("n");               
             result=   GetManager.AddTradeNews(news, LoginId);
            }
            else
            {
                news.ID = ID;
                result = GetManager.ModifyTradeNews(news, LoginId);
            }
            MessageBox(result.Desc);
        }
    }
}