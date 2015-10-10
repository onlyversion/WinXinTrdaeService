<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="NewsInfo.aspx.cs"
    Inherits="TradeService.NewsInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新闻公告</title>
    <link rel="stylesheet" href="kindeditor/themes/default/default.css" />
    <link rel="stylesheet" href="kindeditor/plugins/code/prettify.css" />
    <script type="text/javascript" src="../kindeditor/kindeditor.js"></script>
    <script type="text/javascript" charset="utf-8" src="kindeditor/lang/zh_CN.js"></script>
    <script type="text/javascript" charset="utf-8" src="kindeditor/plugins/code/prettify.js"></script>
    <link rel="Stylesheet" href="Css/Common.css" />
    <script type="text/javascript">
        KindEditor.ready(function (K) {
            var editor1 = K.create('#NewsContent', {
                cssPath: 'kindeditor/plugins/code/prettify.css',
                uploadJson: 'kindeditor/asp.net/upload_json.ashx',
                fileManagerJson: 'kindeditor/asp.net/file_manager_json.ashx',
                allowFileManager: true,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=form1]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=form1]')[0].submit();
                    });
                }
            });
        });

        function Check() {
            var name = document.getElementById("txtTitle").value;
            if (name == "") {
                alert("请填写标题,标题不能为空!");
                return false;
            }
            //            var NewsContent = document.getElementById("NewsContent").value;
            //            if (NewsContent == "") {
            //                alert("请填写内容,内容不能为空!");
            //                return false;
            //            }
            return true;
        }
    </script>
    <style type="text/css">
	body{ background:#f5f5f5;}
    .newsinfo_tab{}
	.newsinfo_tab td{
		padding:5px 0;
		/*vertical-align: top;*/
		font-size: 14px;
		color: #666;
		font-family:"Microsoft Yahei", "黑体", "宋体", Arial}
	.newsinfo_tab input[type=text]{
		border: 1px solid #d8dad9;
		width:100%;
		min-width:400px;
		padding: 4px 2px;
		border-radius: 3px;}
	.newsinfo_tab select{
		width: ;
		padding: 4px 0;
		margin-right:20px;
		border: 1px solid #d8dad9;
		border-radius: 3px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" class="newsinfo_tab">
          <tr>
            <td colspan="2" bgcolor="#d0e1e9" style="height: 24px">
              <img alt="" src="Img/titleIcon.gif" /><span style="color: #4073B6;" runat="server" id="spanMessage"> 新闻公告信息</span>
            </td>
          </tr>
          <tr>
            <td width="70" align="left" style="padding-left:13px;">
                标&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;题：
            </td>
            <td style="padding-right:10px;">
            	<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td width="70" align="left" style="padding-left:13px;">
                类型：
            </td>
            <td>
              <asp:DropDownList runat="server" ID="ddlType">
                  <asp:ListItem Value="">-请选择-</asp:ListItem>
                  <asp:ListItem Value="1">新闻</asp:ListItem>
                  <asp:ListItem Value="2">公告</asp:ListItem>
              </asp:DropDownList>
              状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
              <asp:RadioButton ID="rbEnable" runat="server" Checked="True" GroupName="0" Text="启  用" />&nbsp;
              <asp:RadioButton ID="rbDisable" runat="server" GroupName="0" Text="禁  用" />
            </td>
          </tr>
          <tr>
            <td width="70" align="left" style="padding-left:13px;">
                摘&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;要：
            </td>
            <td style="padding-right:10px;">
            	<asp:TextBox ID="txtOverView" runat="server"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td align="left" style="padding-left:13px;">
                内&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;容：
            </td>
            <td style="padding-right:10px;">
              <textarea id="NewsContent" cols="100" rows="8" style="width: 100%; height: 380px; visibility: hidden;" runat="server" tabindex="15"></textarea>
            </td>
          </tr>
          <tr>
            <td colspan="2" style="padding-left:380px;">
                <asp:ImageButton ID="btnSave" OnClientClick="return Check();" runat="server"  
                    onclick="btnSave_Click" ImageUrl="~/Img/bc.gif" />
            </td>
          </tr>
        </table>
    </div>
    </form>
</body>
</html>
