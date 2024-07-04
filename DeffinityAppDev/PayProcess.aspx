<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayProcess.aspx.cs" Inherits="DeffinityAppDev.App.PayProcess" %>




<!DOCTYPE html>

<html lang="en">
	<!--begin::Head-->
	<head><base href="./">
		<title><asp:Literal ID="lblTitle" runat="server"></asp:Literal> </title>
		<meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
<meta http-equiv="Pragma" content="no-cache" />
<meta http-equiv="Expires" content="0" />
	
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<meta charset="utf-8" />
		<meta property="og:locale" content="en_US" />
		<meta property="og:type" content="article" />

		



		<link rel="canonical" href="" />
		<%--<link rel="shortcut icon" href="../../assets/media/logos/favicon.ico" />--%>
		<!--begin::Fonts-->
		<link rel="stylesheet" href="" />
		 <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
		<link href='<%:ResolveClientUrl("~/assets/plugins/custom/leaflet/leaflet.bundle.css")%>' rel="stylesheet" type="text/css" />
	
		<!--end::Page Vendor Stylesheets-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<link href='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.css")%>' rel="stylesheet" type="text/css" />
		<link href='<%:ResolveClientUrl("~/assets/css/style.bundle.css")%>' rel="stylesheet" type="text/css" />
			<link href="../../Content/AjaxControlToolkit/Styles/Calendar.min.css" rel="stylesheet" />
		 <%= System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
		<!--end::Global Stylesheets Bundle-->
		 <style>
		 /*	.photo-panel {
				 text-align:center;
		 	}*/

			 .card {
    position: relative;
    display: inherit;
    flex-direction: column;
    min-width: 0;
    word-wrap: break-word;
    background-color: #ffffff;
    background-clip: border-box;
    border: 1px solid #EFF2F5;
    border-radius: 0.475rem;
    box-shadow: 0px 0px 20px 0px rgb(76 87 125 / 2%);
}
				.ios-strech-fix
{
      height: intrinsic;
}
				.row:before, .row:after {display: none !important;}
     .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
  
        </style>
	</head>
	<!--end::Head-->
	<!--begin::Body-->
	<body id="kt_body" class="page-loading-enabled page-loading">

		<div class="container d-flex align-items-center justify-content-center">
		<div class="page-loader">
			<span class="spinner-border text-primary" role="status">
				<span class="visually-hidden">Loading...</span>
			</span>
		</div>
       
			<div class="card" id="pnlNoCard" runat="server" visible="false">
				<div class="card-body d-flex flex-column flex-center">
			<div class="row my-30 mb-2">
       <asp:Label ID="Label1" runat="server" Font-Size="45px" Text=" " ></asp:Label>
			  <asp:Label ID="Label2" runat="server"  Text=""></asp:Label>
     </div>
			<div class="py-10 text-center">
		 <i class="bi bi-exclamation-triangle theme-light-show w-200px" style="font-size:200px;color:red"></i>
				</div>
				  <h1 class="fw-semibold text-gray-800 text-center lh-lg" style="font-size:30px">
					  Hey <asp:Literal ID="lblUser" runat="server"></asp:Literal>,  <br />before you can process a donation your payment gateway account needs to be activated. Please contact your provider to check the status of your application. <br /> Thank you. 
    
            </h1>
					</div>
				</div>
    
			<div class="card"  id="pnlDefault" runat="server" visible="true">
				<div class="card-body">
			<div class="row my-30">
       <asp:Label ID="lblMessage" runat="server" Font-Size="45px" Text=" " ></asp:Label>
			  <asp:Label ID="lbloutput" runat="server"  Text=""></asp:Label>
     </div>
			<div class="row my-30">
		 <img src="assets/payprocess.png" id="img" runat="server" class="img-fluid" />
				</div>
				  <div id="div_form" runat="server">
     <asp:Literal ID="lblsubform" runat="server"></asp:Literal>
            </div>
					</div>
				</div>
      

			

       </div>
   

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