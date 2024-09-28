<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FundraiserView.aspx.cs" Inherits="DeffinityAppDev.FundriserView" %>


<%@ Register Src="~/App/controls/FundraiserDetailsCtrl.ascx" TagPrefix="Pref" TagName="FaithGivingListCtrl" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head><base href="../../">
		<title>Fundraiser</title>
			<meta name="description" content='<%:sitedesciption() %>' />
		<meta name="keywords" content='<%:sitekey() %> ' />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<meta charset="utf-8" />
		<meta property="og:locale" content="en_US" />
		<meta property="og:type" content="article" />
		<meta property="og:title" content='<%:sitetitle() %>' />
		<meta property="og:url" content='<%:siteurl() %>' />
		<meta property="og:site_name" content='<%:sitetitle() %>' />
<meta property="og:type" content="website" />
<meta property="og:image" content='<%:siteimage() %>' />
<meta property="og:description" content='<%:sitedesciption() %>' />
		<meta name="twitter:card" content='<%:siteimage() %>'>
		<meta name="twitter:image:alt" content='<%:sitetitle() %>'>
		<%--<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />--%>
		<!--begin::Fonts-->
		<link rel="stylesheet" href="" />
	 <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
		<link href='<%:ResolveClientUrl("~/assets/plugins/custom/leaflet/leaflet.bundle.css")%>' rel="stylesheet" type="text/css" />
	
		<!--end::Page Vendor Stylesheets-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<link href='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.css")%>' rel="stylesheet" type="text/css" />
		<link href='<%:ResolveClientUrl("~/assets/css/style.bundle.css")%>' rel="stylesheet" type="text/css" />
			<link href="Content/AjaxControlToolkit/Styles/Calendar.min.css" rel="stylesheet" />
		 <%= System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
	<style type="text/css">
  .BigCheckBox input { width:25px; height:25px;   border-radius: 50%; background-color:#7239EA; padding:40px; padding-bottom:4px;  }
   .BigCheckBox label { font-size:17px; margin-left:8px;margin-bottom:5px;padding-bottom:5px;  }

  .checkbox-round {
    width: 1.3em;
    height: 1.3em;
    background-color: white;
    border-radius: 50%;
    vertical-align: middle;
    border: 1px solid #ddd;
   
    -webkit-appearance: none;
    outline: none;
    cursor: pointer;
}

  .checkbox-round:checked {
    background-color: gray;
}

</style>
		<!--end::Global Stylesheets Bundle-->
		 <style>
     .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
	 .margin-full{
		 margin:7%;
	 }
   /* .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 300px;
        height: 140px;
    }*/
        </style>
	</head>
<body>
	
    <form id="form1" runat="server">
       
          
			<body id="kt_body" class="page-loading-enabled page-loading " style="">
		<!--begin::Main-->
		
				<div class="page-loader">
			<span class="spinner-border text-primary" role="status">
				<span class="visually-hidden">Loading...</span>
			</span>
		</div>

				  <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
       <Scripts>
          <%-- <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />--%>
		    <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />
       </Scripts>
   </asp:ScriptManager>
			<!--begin::Content-->
				<div class="margin-full">
					<div class="d-flex flex-column flex-column-fluid" id="kt_content">



					

						
							<div class="row">
							   <asp:HiddenField ID="htitle" runat="server" ClientIDMode="Static" />
                                                 <asp:HiddenField ID="hpath" runat="server" ClientIDMode="Static" />
			  <asp:HiddenField ID="hurl" runat="server" ClientIDMode="Static" />
			  <asp:HiddenField ID="himage" runat="server" ClientIDMode="Static" />
			  <asp:HiddenField ID="hdescription" runat="server" ClientIDMode="Static" />
			  <asp:HiddenField ID="hkey" runat="server" ClientIDMode="Static" />
                                             
                        	
					 <Pref:FaithGivingListCtrl runat="server" ID="FaithGivingListCtrl" />
							  </div>
							</div>

						</div>
				</body>

         
			
    </form>


	<script src='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/scripts.bundle.js")%>'></script>
		<!--end::Global Javascript Bundle-->
		<!--begin::Page Vendors Javascript(used by this page)-->
		<script src='<%:ResolveClientUrl("~/assets/plugins/custom/leaflet/leaflet.bundle.js")%>'></script>
	<%--<script src='<%:ResolveClientUrl("~/assets/plugins/custom/fullcalendar/fullcalendar.bundle.js")%>'></script>--%>
	
	
<script src='<%:ResolveClientUrl("~/assets/plugins/custom/datatables/datatables.bundle.js")%>'></script>

	<%--<script src='<%:ResolveClientUrl("~/assets/js/custom/documentation/forms/tagify.js")%>'></script>--%>
		<!--end::Page Vendors Javascript-->
		<!--begin::Page Custom Javascript(used by this page)-->
		<script src='<%:ResolveClientUrl("~/assets/js/custom/modals/select-location.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/custom/widgets.js")%>'></script>
		<%--<script src='<%:ResolveClientUrl("~/assets/js/custom/apps/chat/chat.js")%>'></script>--%>
		<%--<script src='<%:ResolveClientUrl("~/assets/js/custom/modals/create-app.js")%>'></script>
		<script src='<%:ResolveClientUrl("~/assets/js/custom/modals/upgrade-plan.js")%>'></script>--%>
	  <%: System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
    <script src='<%:ResolveClientUrl("~/Scripts/Utility.js?id=10")%>'></script>
</body>
</html>

