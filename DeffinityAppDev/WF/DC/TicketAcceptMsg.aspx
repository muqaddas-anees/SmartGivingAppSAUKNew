<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketAcceptMsg.aspx.cs" Inherits="DeffinityAppDev.WF.DC.TicketAcceptMsg" %>

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
							Message
						</div>
        <div class="panel-body">
              <asp:Panel id="pnlStatus" runat="server" Visible="false">
           <div class="row">
          <div class="col-md-12" style="text-align:center;">
              <asp:Label ID="lblMsgStatus" runat="server" Font-Bold="true" Text="" CssClass="ralert ralert-success"></asp:Label>
	</div>
</div>
                 </asp:Panel>
             <asp:Panel id="pnlError" runat="server" Visible="false">
           <div class="row">
          <div class="col-md-12" style="text-align:center;">
              <asp:Label ID="lblErrorpnlError" runat="server" Font-Bold="true" Text="" CssClass="ralert ralert-success"></asp:Label>
	</div>
</div>
                 </asp:Panel>
             <asp:Panel id="pnlmsg" runat="server">
           <div class="row">
          <div class="col-md-12" style="text-align:center;">
              <asp:Label ID="LblMsg" runat="server" Font-Bold="true" SkinID="GreenBackcolor" Text="Great! We've assigned this ticket to you and we've notified the customer"></asp:Label>
	</div>
</div>
                 </asp:Panel>
             <asp:Panel id="pnlAccept" runat="server">
        <div class="row">
          <div class="col-md-12" style="text-align:left;">
              <asp:Label ID="lblerror" runat="server" SkinID="RedBackcolor"></asp:Label>
               <asp:Label ID="lblSuccessMsg" runat="server" SkinID="GreenBackcolor"></asp:Label>
	</div>
</div>
                 <br />
   <%-- <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Call</label>
           <div class="col-sm-9">

            </div>
	</div>
       
</div>--%>
         <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><strong>Ticket Ref:</strong></label>
           <div class="col-sm-9">
               <asp:Label ID="lblJob" runat="server"></asp:Label>
            </div>
	</div>
       
</div>
         <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><strong>Details:</strong></label>
           <div class="col-sm-9">
                <asp:Label ID="lblDetails" runat="server"></asp:Label>
            </div>
	</div>
       
</div>
         <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><strong>Requester:</strong></label>
           <div class="col-sm-9">
                <asp:Label ID="lblRequester" runat="server"></asp:Label>
            </div>
	</div>
       
</div>
         <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><strong>Town:</strong></label>
           <div class="col-sm-9">
                <asp:Label ID="lblTown" runat="server"></asp:Label>
            </div>
	</div>
       
</div>
         <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><strong>City:</strong></label>
           <div class="col-sm-9">
                <asp:Label ID="lblCity" runat="server"></asp:Label>
            </div>
	</div>
       
</div>
         <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><strong>Zipcode:</strong></label>
           <div class="col-sm-9">
                <asp:Label ID="lblZipcode" runat="server"></asp:Label>
            </div>
	</div>
       
</div>
                  <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><strong>Preferred Date / Time:</strong></label>
           <div class="col-sm-9">
                <asp:Label ID="lblPreferreddate" runat="server"></asp:Label>
            </div>
	</div>
       
</div>
                   <div class="form-group row" id="pnlpre2" runat="server">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><strong>Preferred Date / Time 2:</strong></label>
           <div class="col-sm-9">
                <asp:Label ID="lblPre2" runat="server"></asp:Label>
            </div>
	</div>
       
</div>
                   <div class="form-group row"  id="pnlpre3" runat="server">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"><strong>Preferred Date / Time 3:</strong></label>
           <div class="col-sm-9">
                <asp:Label ID="lblpre3" runat="server"></asp:Label>
            </div>
	</div>
       
</div>
          <div class="form-group row">
          <div class="col-md-12" style="margin-bottom: 10px;">
 <label class="col-sm-3 control-label"><strong>I can attend this job on:</strong></label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox id="txtDate" runat="server" SkinID="Date"></asp:TextBox>
               <asp:Label ID="imgSeheduledDate" runat="server"  SkinID="Calender" ClientIDMode="Static" />
            <ajaxToolkit:CalendarExtender ID="calSeheduledDate" runat="server" CssClass="MyCalendar"
                 PopupButtonID="imgSeheduledDate" TargetControlID="txtDate">
            </ajaxToolkit:CalendarExtender>
            </div>
	</div>
       
</div>
                 <br />
          <div class="form-group row">
          <div class="col-md-12" style="margin-bottom: 10px;">
 <label class="col-sm-3 control-label"><strong>At this time:</strong></label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox id="txtTime" runat="server" SkinID="Time"></asp:TextBox>
               
            </div>
	</div>
       
</div>
                 <br />
         <div class="form-group row"  style="margin-bottom: 10px;">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
                <asp:Button runat="server" SkinID="btnSubmit" id="btnSubmit" OnClick="btnSubmit_Click" />
               
            </div>
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

