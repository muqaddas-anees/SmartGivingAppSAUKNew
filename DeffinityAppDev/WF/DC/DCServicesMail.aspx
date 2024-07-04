<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DCServicesMail.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DCServicesMail" EnableEventValidation="false" %>

<%@ Register Src="~/WF/Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register Src="~/WF/DC/controls/sd_services.ascx" TagPrefix="uc1" TagName="sd_services" %>
<%@ Register Src="~/WF/DC/controls/CustomerOrder.ascx" TagPrefix="uc1" TagName="CustomerOrder" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge"/>
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<meta name="description" content="" />
	<meta name="author" content="" />
    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
<title>Price </title>
<meta name="description" content=""/>
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Arimo:400,700,400italic"/>
	<%: System.Web.Optimization.Styles.Render("~/bundles/bootstarpcss") %>
    
<link href="../../Content/AjaxControlToolkit/Styles/Calendar.min.css" rel="stylesheet" />
	<script src='<%:ResolveClientUrl("~/Content/assets/js/jquery-1.11.1.min.js") %>'></script>
    <style type="text/css">
        /*.login-page.login-light{background: #eeeeee url("../Content/images/deffi_coffee.jpg") top center no-repeat;}
        input:-webkit-autofill {
            background-color: white !important;
        }*/
    </style>
     <style type="text/css">
        .login-page.login-light{background: #eeeeee url("../Content/images/deffi_coffee.jpg") top center no-repeat;}
        input:-webkit-autofill {
            background-color: white !important;
        }

       
        .login-container1 {
    max-width: 98%;
    margin: 0 auto;
}
    </style>
    <style>
   .ralert-success {
    background-color: #8dc63f;
    border-color: #8dc63f;
    color: #ffffff;
}

.ralert {
    padding: 15px;
    margin-bottom: 18px;
    border: 1px solid transparent;
    border-radius: 0px;
}
</style>
	<%--<%: System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>--%>
</head>
<body class="page-body">
   <%-- <form id="form1" runat="server">--%>
	<div class="login-container1">
<form id="form2" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
       <Scripts>
           <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />
       </Scripts>
   </asp:ScriptManager>
    <div class="card shadow-sm">
						<div class="card-header">
							<label id="lblTitle" runat="server"></label>   
						</div>
        <div class="panel-body">
          <%--  <uc1:CustomerOrder runat="server" ID="CustomerOrder" />--%>
            <uc1:sd_services runat="server" ID="sd_services"  Type="FLS" />
             

            </div>
        </div>

    

<div class="data_carrier" style="color:gray;">


<uc1:Footer ID="ctrl_footer" runat="server" />
</div>

</form>
        </div>
      
                     <%: System.Web.Optimization.Scripts.Render("~/bundles/xenonjs") %>
    <%: System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
  
	 <script type="text/javascript">
	     $(window).load(function () {
	         
	        // disableAutofill();
	     });
	     //if (navigator.userAgent.toLowerCase().indexOf("chrome") >= 0 || navigator.userAgent.toLowerCase().indexOf("safari") >= 0) {
	     //}
	 </script>
</body>
</html>
