<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiveEvent.aspx.cs" Inherits="DeffinityAppDev.App.Events.LiveEvent" %>

<%@ Register Src="~/App/controls/taithingeventCtrl.ascx" TagPrefix="Pref" TagName="taithingeventCtrl" %>





<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head><base href="../../">
		<title>Event</title>
		<meta name="description" content="" />
		<meta name="keywords" content="" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<meta charset="utf-8" />
		<meta property="og:locale" content="en_US" />
		<meta property="og:type" content="article" />
		<meta property="og:title" content="" />
		<meta property="og:url" content="" />
		<meta property="og:site_name" content="" />
		<link rel="canonical" href="" />
		<%--<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />--%>
		<!--begin::Fonts-->
		<link rel="stylesheet" href="" />
		<link href='<%:ResolveClientUrl("~/assets/plugins/custom/leaflet/leaflet.bundle.css")%>' rel="stylesheet" type="text/css" />
	<script src="Scripts/jquery-1.10.2.min.js"></script>
		<!--end::Page Vendor Stylesheets-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<link href='<%:ResolveClientUrl("~/assets/plugins/global/plugins.bundle.css")%>' rel="stylesheet" type="text/css" />
		<link href='<%:ResolveClientUrl("~/assets/css/style.bundle.css")%>' rel="stylesheet" type="text/css" />
			<link href="Content/AjaxControlToolkit/Styles/Calendar.min.css" rel="stylesheet" />
		 <%= System.Web.Optimization.Styles.Render("~/Content/AjaxControlToolkit/Styles/Bundle") %>
	


    <style>
        body {
            font-size: 10px !important;
        }

        .col-xxl-12 {
            width: 100%;
        }

        .w-500 {
            width: 50%;
        }
        .Speakerimg{
            width:100%;
        }

       .video-title {
             font-size: 24px;
        font-weight: bold;
        margin-top: 10px;
    }

    .video-description {
        font-size: 16px;
        color: #666;
        margin-top: 5px;
    }

    @media (max-width: 767px) {
        .video-container {
            padding-bottom: 75%; /* Adjust for mobile view */
        }
        .video-title {
            font-size: 20px;
        }
        .video-description {
            font-size: 14px;
        }
    }
        .custom-media-query {
            display: flex;
            align-content:start;
            align-items:start;
            flex-wrap: wrap;
            margin:20px;
        }

        .custom-media-query > div {
            flex: 1 1 50%; /* Default: Two items per row */
        }    
        .yt-video {
        position: relative;
        padding-bottom: 33%; /* 16:9 aspect ratio */
        height: 0;
        overflow: hidden;
        max-width: 100%; /* Ensure it doesn’t exceed its container's width */
    }
         @media (max-width: 767px) {
        .yt-video {
            padding-bottom: 75%; /* Adjust aspect ratio for mobile if needed */
        }
    }

    @media (min-width: 768px) and (max-width: 1199px) {
        .yt-video {
            padding-bottom: 56.25%; /* Default aspect ratio for tablets */
        }
    }

    @media (min-width: 1200px) {
        .yt-video {
            padding-bottom: 33%; /* Default aspect ratio for desktops */
        }
    }


    .yt-video iframe {
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
    }

    @media (max-width: 767px) {
        .yt-video {
            padding-bottom: 75%; /* Adjust aspect ratio for mobile if needed */
        }
    }

        /* Media Queries */
        @media (max-width: 1199px) {
            .custom-media-query > div {
                flex: 1 1 50%; /* Two items per row on medium screens */
            }
        }

        @media (max-width: 991px) {
            .custom-media-query > div {
                flex: 1 1 100%; /* Full-width items on small screens */
            }
        }

        @media (max-width: 767px) {
            .custom-media-query {
                flex-direction: column; /* Stack items vertically on extra small screens */
            }

            .w-500 {
                width: 100% !important; /* Full-width items on extra small screens */
            }

       
        }

   
    </style>
    	</head>
<body id="kt_body" class="page-loading-enabled page-loading">
	<div class="page-loader">
			<span class="spinner-border text-primary" role="status">
				<span class="visually-hidden">Loading...</span>
			</span>
		</div>
    <form id="form1" runat="server">
        <div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" LoadScriptsBeforeUI="true">
    <Scripts>
       <%-- <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />--%>
   <asp:ScriptReference Path="~/Scripts/AjaxControlToolkit/Bundle" />
    </Scripts>
</asp:ScriptManager>
    <div class="custom-media-query">
        <asp:Literal runat="server" ID="ytLiveStream" ClientIDMode="Static" />
        <div class="w-500">
            <Pref:taithingeventCtrl runat="server" ClientIDMode="Static" ID="taithingeventCtrl" />
        </div>
    </div>
    <div class="d-flex" style="margin: 20px;background: #fff;margin-top: 0px;padding: 18px;border-radius: 10px;" >
        <div class="w-500">
           <asp:Literal runat="server" ID="videoDesc" />
        </div>

  <div class="card w-500 p-3" style="
  
">
    <div class="card-header" style="background-color: #fff; border-bottom: 1px solid #ccc;">
        <h5 class="mb-0" style="font-size:24px">Speakers</h5>
    </div>
        <div class="card-body">

    <asp:Literal runat="server" ID="speakers"></asp:Literal>
</div>
        </div>

    </div>



           			</div>
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