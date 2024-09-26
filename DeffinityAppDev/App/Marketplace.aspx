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
	
		<asp:ListView ID="lvCards" runat="server">
    <LayoutTemplate>
        <div class="row">
            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
        </div>
    </LayoutTemplate>
    <ItemTemplate>
        <div class="col-md-4">
            <div class="card card-bordered mb-6">
                <!--begin::Publications post-->
                <div class="card-xl-stretch me-md-6">
                    <!--begin::Overlay-->
                    <a class="d-block overlay mb-4" data-fslightbox="lightbox-hot-sales" href='<%# Eval("VideoLink") %>'>
                        <!--begin::Image-->
                        <div class="overlay-wrapper bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-175px" 
                            style='background-image:url(<%# Eval("ImageUrl") %>)'></div>
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
                        <a href="#" class="fs-4 text-dark fw-bolder text-hover-primary text-dark lh-base">
                            <%# Eval("Title") %>
                        </a>
                        <!--end::Title-->
                        <!--begin::Text-->
                        <div class="fw-bold fs-5 text-gray-600 text-dark mt-3 mb-5" style="min-height:140px">
                            <%# Eval("Description") %>
                        </div>
                        <!--end::Text-->
                    </div>
                    <!--end::Body-->
                </div>
                <div class="card-footer gap-3" style="display: flex; justify-content: space-between;">
                    <a class="btn btn-primary" style="padding:10px 40px" href='<%# Eval("VideoLink") %>'><%# Eval("videoText") %></a>
                    <a class="btn btn-primary" style="padding:10px 70px" href='<%# Eval("TrialLink") %>'><%# Eval("buyText") %></a>
                </div>
                <!--end::Publications post-->
            </div>
        </div>
    </ItemTemplate>
</asp:ListView>



























































    </asp:Content>