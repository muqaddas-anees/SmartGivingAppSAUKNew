<%@ Page Language="C#" AutoEventWireup="true" Inherits="PageNotfound" Codebehind="PageNotfound.aspx.cs" %>
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

    <div class="panel panel-default">
						<div class="panel-heading">
							Page Not Found
						</div>
						
						<div class="panel-body">
                            <p>
The page you requested cannot be found. The page you are looking for is temporarily unavailable. 
</p>
                            <p>
<asp:Label runat="Server" ID="lblmsg" Text="" ForeColor="red"></asp:Label>
</p>
                            <p>
                                
<label ID="lbldate" runat="server" visible="false"></label>
   <asp:LinkButton ID="btnSignOut" runat="server" Text="Logout" OnClick="btnSignOut_Click" CausesValidation="False" Visible="false"></asp:LinkButton>

   
<label id="lblName" runat="server" visible="false"></label> 
                            </p>
                            </div>
        </div>

<%--<a target="_self" runat="server" id="A1"><img src="../Content/assets/images/logo@2x.png" alt="" /></a>--%>


<uc1:Footer ID="ctrl_footer" runat="server" />


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

