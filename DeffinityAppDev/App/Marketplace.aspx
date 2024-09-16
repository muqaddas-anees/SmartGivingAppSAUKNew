<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Marketplace.aspx.cs" Inherits="DeffinityAppDev.Marketplace" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <asp:Label ID="lblPagetitle" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">



<style>
 .pnlheight{
	 min-height:400px;
 }
 h2.page-header{
     font-size:21px;
 }
  .card-body {
     height: 240px; /* Adjust card body height */
 }

</style>
	
															 <div class="col-md-4">
																<div class="card card-bordered mb-6">
												<!--begin::Publications post-->
												<div class="card-xl-stretch me-md-6">
													<!--begin::Overlay-->
													<a class="d-block overlay mb-4" data-fslightbox="lightbox-hot-sales" href=''>
														<!--begin::Image-->
														<div class="overlay-wrapper bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-175px" style="background-image:url('https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQzk92qOx7c5k5fybjVbUkwg6BGW_ptjgID9A&s')"></div>
														<!--end::Image-->
														<!--begin::Action-->
														<div class="overlay-layer bg-dark card-rounded bg-opacity-25">
															<i class="bi bi-eye-fill fs-2x text-white"></i>
														</div>
														<!--end::Action-->
													</a>
													<!--end::Overlay-->
													<!--begin::Body-->
													<div class="m-0 p-2">
														<!--begin::Title-->
														<a href="#" class="fs-4 text-dark fw-bolder text-hover-primary text-dark lh-base"> Plegit AI</a>
														<!--end::Title-->
														<!--begin::Text-->
														<div class="fw-bold fs-5 text-gray-600 text-dark mt-3 mb-5" style="min-height:140px">Description</div>
														<!--end::Text-->
														<!--begin::Content-->
														<%--<div class="fs-6 fw-bolder">
															<!--begin::Author-->
															<a href="../../demo4/dist/pages/projects/users.html" class="text-gray-700 text-hover-primary">Jane Miller</a>
															<!--end::Author-->
															<!--begin::Date-->
															<span class="text-muted">on Mar 21 2021</span>
															<!--end::Date-->
														</div>--%>
														<!--end::Content-->
													</div>
													<!--end::Body-->
												</div>
																 <div class="card-footer gap-3" style="display: flex; justify-content: space-between;">
																	
																	 
																	<a class="btn btn-primary" style="padding:10px 40px">Watch Video</a>
																	 																	<a class="btn btn-primary" style="padding:10px 70px">Try for 7 Days</a>

																	<%-- OnClientClick='<%# String.Format("return myFunction(\"{0}\");", Eval("QRcode")) %>'--%>
    </div>
												<!--end::Publications post-->
											</div>
</div>




    </asp:Content>