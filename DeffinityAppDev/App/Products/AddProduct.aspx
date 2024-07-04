<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="DeffinityAppDev.App.Products.AddProduct" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <style>
		.txt_right{
			text-align:right;
		}
	</style>
     <div class="card mb-5 mb-xl-10">
		 <div class="card-body">
			 <div class="row gap-3">
				 <div class="col-lg-6">
			<h1 class="text-dark fw-bolder my-1 fs-1 lh-1">	 <asp:Label ID="lblTopTitle" runat="server" Text="Product Details"></asp:Label> </h1>
					 </div>
				 <div class="col-lg-5 d-flex d-inline justify-content-end gap-3">
					 <div class="form-check form-switch form-check-custom form-check-solid me-10">
    <input class="form-check-input h-30px w-50px" type="checkbox" value="" id="chkActive" runat="server" checked="checked"/>
    <label class="form-check-label" for="flexSwitch30x50">
       Active
    </label>
</div>
					
					 <asp:Button ID="btnSave" runat="server" Text="Save Product" OnClick="btnSave_Click" />



					 
                      <a class="btn btn-video" style="background-color:#50CD89;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo" href="#vimeo">
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                  <iframe id="vimeo" style="display:none" src="https://player.vimeo.com/video/836626081?h=d2d3d21798" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>
                    



					 </div>
				
			 </div>
		 </div>
		 </div>

	<div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"> Title</h3>
									</div>

				</div>
		 <div class="card-body">

			 <div class="row">
				 <div class="col-lg-8">
					<asp:TextBox ID="txtTitle" runat="server" ></asp:TextBox>
				 </div>
			 </div>

			 </div>
		</div>

	<div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"> Details</h3>
									</div>

				</div>
		 <div class="card-body">

			 <div class="row">
				 <div class="col-lg-8">
						<CKEditor:CKEditorControl ID="txtDescriptionArea" BasePath="~/Scripts/ckeditor_4.20.1/" Skin="moono-lisa" runat="server"
                         Height="500px" ClientIDMode="Static" ></CKEditor:CKEditorControl>
				 </div>
			 </div>

			 </div>
		</div>
	<div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"> Category</h3>
									</div>

				</div>
		 <div class="card-body">

			 <div class="row">
				 <div class="col-lg-8">
				<asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
				 </div>
			 </div>

			 </div>
		</div>
	<div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0">Product Image</h3>
									</div>

				</div>
		 <div class="card-body">
	<%-- <div class="row mb-6"><label> The banner should be 1920 X 1080 pixels for the best quality. </label> </div>--%>

			 <div class="row">
				 <div class="col-lg-6">
					  <asp:FileUpload runat="server" id="imgBanner" CssClass="form-control" Text="Upload" />
					 <br />
					<%-- <asp:Button ID="btnSaveBanner" runat="server" OnClick="btnSaveBanner_Click" Text="Upload" />--%>

				 </div>
				  <div class="col-lg-6">
					   <asp:Image ID="img" runat="server" CssClass="img-responsive" style="max-height:250px" />
				 </div>

			 </div>
			

			 </div>
		</div>
	<div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"> Amount</h3>
									</div>

				</div>
		 <div class="card-body">

			 <div class="row">
				 <div class="col-lg-8">
					<asp:TextBox ID="txtAmount" runat="server" SkinID="Price_200px" Text="0.00" ></asp:TextBox>
				 </div>
			 </div>

			 </div>
		</div>

	<div class="card mb-5 mb-xl-10">

			<div class="card-header border-0 cursor-pointer" >
									<!--begin::Card title-->
									<div class="card-title m-0">
										<h3 class="fw-bolder m-0"> Total Units</h3>
									</div>

				</div>
		 <div class="card-body">

			 <div class="row">
				 <div class="col-lg-8">
					<asp:TextBox ID="txtTotalUnits" runat="server" SkinID="Price_200px" Text="0" ></asp:TextBox>
				 </div>
			 </div>

			 </div>
		</div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
