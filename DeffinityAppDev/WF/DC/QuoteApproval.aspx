<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuoteApproval.aspx.cs" Inherits="DeffinityAppDev.WF.DC.QuoteApproval" %>

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
<title> Call Status </title>
    <style>
   .ralert-success {
    background-color: #cc3f44;
    border-color: #cc3f44;
    color: #ffffff;
}

.ralert {
    padding: 15px;
    margin-bottom: 18px;
    border: 1px solid transparent;
    border-radius: 0px;
}

.galert-success {

    background-color: #8dc63f;
    border-color: #8dc63f;
    color: #ffffff;

}
.galert {

    padding: 15px;
    margin-bottom: 18px;
    border: 1px solid transparent;
        border-top-color: transparent;
        border-right-color: transparent;
        border-bottom-color: transparent;
        border-left-color: transparent;
    border-radius: 0px;

}
</style>
<meta name="description" content=""/>
    <link rel="stylesheet" href="//fonts.googleapis.com/css?family=Arimo:400,700,400italic"/>
	<%: System.Web.Optimization.Styles.Render("~/bundles/bootstarpcss") %>
    
<link href="../../Content/AjaxControlToolkit/Styles/Calendar.min.css" rel="stylesheet" />
	<script src='<%:ResolveClientUrl("~/Content/assets/js/jquery-1.11.1.min.js") %>'></script>
    <style type="text/css">
        .login-page.login-light{background: #eeeeee url("../Content/images/deffi_coffee.jpg") top center no-repeat;}
        input:-webkit-autofill {
            background-color: white !important;
        }
    </style>
	<%--<%: System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>--%>
</head>
<body class="page-body">
   <%-- <form id="form1" runat="server">--%>
	<div class="login-container">
<form id="form2" runat="server">
<%-- <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
       <Scripts>
           <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />
       </Scripts>
   </asp:ScriptManager>--%>
    <div class="card shadow-sm">
						<div class="card-header">
							Quotation Approval - Job Reference - <asp:Label ID="lblJob" runat="server"></asp:Label>
						</div>
        <div class="panel-body">
           
            <asp:Panel id="pnlmsg" runat="server">
           <div class="row">
          <div class="col-md-12" style="text-align:center;">
              <asp:Label ID="LblMsg" runat="server" Font-Bold="true" CssClass="galert galert-success" Text="Quote had been accepted "></asp:Label>
	</div>
</div>
                 </asp:Panel>

             <asp:Panel id="pnlReject" runat="server">
           <div class="row">
          <div class="col-md-12" style="text-align:center;">
              <asp:Label ID="lblError" runat="server" Font-Bold="true" CssClass="ralert ralert-success" Text="Quote had been rejected"></asp:Label>
	</div>
</div>
                 </asp:Panel>
            </div>
        </div>

<div class="data_carrier" style="color:gray;">


<uc1:Footer ID="ctrl_footer" runat="server" />
</div>

</form>
        </div>
      
                     <%: System.Web.Optimization.Scripts.Render("~/bundles/xenonjs") %>
   <%-- <%: System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>--%>
  
	<%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
</body>
</html>

