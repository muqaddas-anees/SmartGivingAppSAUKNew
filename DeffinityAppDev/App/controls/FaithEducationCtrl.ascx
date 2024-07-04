<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FaithEducationCtrl.ascx.cs" Inherits="DeffinityAppDev.App.controls.FaithEducationCtrl" %>
<asp:ListView ID="ListFaithGiving" runat="server" OnItemCommand="ListFaithGiving_ItemCommand">
														<LayoutTemplate>
        
			<div runat="server"  id="itemPlaceholder"></div>
			
			
															</LayoutTemplate>

														<ItemTemplate>

                                                            <div class="col-md-4">
												<!--begin::Publications post-->
												<div class="card-xl-stretch me-md-6">
													<a
    class="d-block bgi-no-repeat bgi-size-cover bgi-position-center rounded position-relative min-h-175px"
    style= "background-image:url('https://img.youtube.com/vi/<%# Eval("VideoID") %>/0.jpg') "
    data-fslightbox="lightbox-youtube"
    href="https://www.youtube.com/embed/<%# Eval("VideoID") %>"
    >
    <!--begin::Icon-->
    <img src="../../assets/media/svg/misc/video-play.svg"  class="position-absolute top-50 start-50 translate-middle" alt=""/>
    <!--end::Icon-->
</a>
													<!--begin::Overlay-->
													<%--<a class="d-block overlay mb-4" data-fslightbox="lightbox-hot-sales" href='<%# GetImageUrl(Eval("ID").ToString()) %>'>
														<!--begin::Image-->
														<div class="overlay-wrapper bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-175px" style="background-image:url('<%# GetImageUrl(Eval("ID").ToString()) %>')"></div>
														<!--end::Image-->
														<!--begin::Action-->
														<div class="overlay-layer bg-dark card-rounded bg-opacity-25">
															<i class="bi bi-eye-fill fs-2x text-white"></i>
														</div>
														<!--end::Action-->
													</a>--%>
													<!--end::Overlay-->
													<!--begin::Body-->
													<div class="m-0">
														<!--begin::Title-->
														<a href="#" class="fs-4 text-dark fw-bolder text-hover-primary text-dark lh-base"> <%# Eval("Title") %> </a>
														<!--end::Title-->
														<!--begin::Text-->
														<%--<div class="fw-bold fs-5 text-gray-600 text-dark mt-3 mb-5"><%# Eval("Title") %></div>--%>
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
																<%-- <div class="card-footer">
        <asp:Button style="float:right;" ID="Button1" runat="server" CssClass="btn btn-dark" CommandName="amount" Text="Find out more"  CommandArgument='<%# Eval("ID") %>'  />
    </div>--%>
												<!--end::Publications post-->
											</div>







														
														</ItemTemplate>
													</asp:ListView>