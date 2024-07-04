
<%@ Page Language="C#" AutoEventWireup="true" Inherits="Message" Codebehind="Message.aspx.cs" %>
<%@ Register Src="~/WF/Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<meta name="description" content="" />
	<meta name="author" content="" />
    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
<title> Message </title>
<meta name="description" content=""/>
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Arimo:400,700,400italic"/>
	<%: System.Web.Optimization.Styles.Render("~/bundles/bootstarpcss") %>
    <style type="text/css">
        .login-page.login-light{background: #eeeeee url("../Content/images/deffi_coffee.jpg") top center no-repeat;}
        input:-webkit-autofill {
            background-color: white !important;
        }
    </style>
	<%--<%: System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>--%>
</head>
<body class="page-body login-page login-light">
   <%-- <form id="form1" runat="server">--%>
	<div class="login-container">
<form id="form2" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"> </asp:ScriptManager>


    <div class="card shadow-sm">
						<div class="card-header">
							Sorry for the inconvenience
						</div>
        <div class="panel-body">
            <p>
You've stumbled upon a temporary problem we're having with Deffinity. Usually this problem gets resolved quickly, without you doing a thing. In fact it may be taken care of now. 
</p>
<p>
Try pressing the Reload or Refresh button on your browser, or <a href="./default.aspx">logging out</a> then back into your Deffinity account. Hopefully that will take care of things. 
If that doesn't fix the problem, please be patient while we sort it out and try again shortly. The fact that you're reading this page means we've been automatically notified of the issue, and chances are we're working on it now. 
</p>
<p>
If you think you've been more than patient and tried the tricks above, feel free to contact <a href="mailto:support@123smartpro.com">Customer Care.</a> </p>
<p>&nbsp;</p>
<p>
Thanks, 
</p>
<p>
123 Smart Pro Team
</p>

            </div>
        </div>

<div class="data_carrier" style="color:gray;">


<uc1:Footer ID="ctrl_footer" runat="server" />
</div>

</form>
        </div>
      
                     <%: System.Web.Optimization.Scripts.Render("~/bundles/xenonjs") %>
  
	 <script type="text/javascript">
	     $(window).load(function () {
	         debugger
	         disableAutofill();
	     });
	     //if (navigator.userAgent.toLowerCase().indexOf("chrome") >= 0 || navigator.userAgent.toLowerCase().indexOf("safari") >= 0) {
	     //}
	 </script>
</body>
</html>
