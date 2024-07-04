<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BlogDisplay.aspx.cs" Inherits="DeffinityAppDev.WF.BlogDisplay" %>

<%--<%@ Register Src="~/WF/Controls/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>--%>

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
       /* .login-page.login-light{background: #eeeeee url("../Content/images/deffi_coffee.jpg") top center no-repeat;}*/
        input:-webkit-autofill {
            background-color: white !important;
        }
        .content {
  max-width: 85%;
  margin: auto;
}

         .carousel-item img {
    margin: 0 auto;
}
    </style>
	<%--<%: System.Web.Optimization.Scripts.Render("~/bundles/jquery") %>--%>
</head>
<body class="page-body content">
   <%-- <form id="form1" runat="server">--%>
	<div class="">
<form id="form2" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"> </asp:ScriptManager>


    <div class="panel panel-default">
					
        <div class="panel-body">

           <%-- <a class="d-block overlay mb-4" data-fslightbox="lightbox-hot-sales" href='<%# GetBlogImageUrl(Eval("BlogRef").ToString()) %>'>
														<!--begin::Image-->
														<div class="overlay-wrapper bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-175px" style="background-image:url('<%# GetBlogImageUrl(Eval("BlogRef").ToString()) %>')"></div>
														<!--end::Image-->
														<!--begin::Action-->
														<div class="overlay-layer bg-dark card-rounded bg-opacity-25">
															<i class="bi bi-eye-fill fs-2x text-white"></i>
														</div>
														<!--end::Action-->
													</a>--%>
             <div class="row d-flex justify-content-center">
                <div class="text-center">
            <img  class="img-fluid carousel-item" runat="server" id="img" />
                    </div>
                 </div>
            <br /><br />
             <div class="row">
                <div class="col-md-12 text-center">
          <h5> <asp:Label ID="lit_title" runat="server" style="font-size:25px"></asp:Label></h5>
                    </div>
                 </div>
             <div class="row">
                <div class="col-md-12">
           <asp:Literal ID="lblContent" runat="server"></asp:Literal>
                    </div>
                 </div>


            <div class="row">
                <div class="col-md-12">
                    <asp:HyperLink ID="link_nav" runat="server" CssClass="btn btn-primary"></asp:HyperLink>
                    </div>
            </div>
            </div>
        </div>

<div class="data_carrier" style="color:gray;">


<%--<uc1:Footer ID="ctrl_footer" runat="server" />--%>
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
