<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="TradeService.News" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新闻信息</title>
    <link rel="Stylesheet" href="Css/Common.css" />
    <style type="text/css">
	*{ margin:0; padding:0}
	.newsinfo_box{
		margin:0 10px;
		border:#f3f3f3 1px solid;
		background:#fafafa;}
	.newsinfo_box table{}
	.newsinfo_title{
		padding:10px 0;
		border-bottom:1px solid  #ddd;}
	.newsinfo_title h1{		
		LINE-HEIGHT: 35px;
		/*HEIGHT: 35px;*/
		FONT-SIZE: 16px;
		font-weight: bold;
		FONT-FAMILY: "Microsoft Yahei", "微软雅黑", Verdana, Tahoma, Arial, STHeiti, sans-serif;
		}	
    #divContent{
		margin-top:10px;
		BORDER-TOP: #fff 1px solid;
		 font-size:16px;
		 line-height:30px;}
	#divContent p{
		text-align:left;}
	#divContent img
	{
	    max-width:600px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="newsinfo_box">
      <div class="newsinfo_title">
      	<h1><asp:Label runat="server" ID="labTitle"></asp:Label></h1>
      </div>
      <div runat="server" id="divContent">
      </div>
    </div>
    </form>
</body>
</html>
