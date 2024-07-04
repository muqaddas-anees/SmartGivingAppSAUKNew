<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivitiesCtrl.ascx.cs" Inherits="DeffinityAppDev.App.controls.ActivitiesCtrl" %>

<%--<asp:UpdatePanel ID="upanelactivites" runat="server" UpdateMode="Conditional">
	<ContentTemplate>--%>
		
		<div  class="row d-flex d-inline">

											<asp:ListView ID="ListActivites" runat="server" OnItemCommand="ListActivites_ItemCommand" >
														<LayoutTemplate>
       	<div runat="server"  id="itemPlaceholder" class="row d-flex d-inline"></div>
			
															</LayoutTemplate>

														<ItemTemplate>

                                                            <div class="col-md-4 mb-6">
												<!--begin::Publications post-->
												<div class="card-xl-stretch me-md-6">
													<!--begin::Overlay-->
													<a class="d-block overlay mb-4" data-fslightbox="lightbox-hot-sales" href="#">
														<!--begin::Image-->
														<asp:Image ID="img_event" runat="server" ImageUrl='<%# GetImmage(Eval("unid").ToString()) %>' CssClass="img-fluid img-thumbnail min-h-175px" />
														<%--<div class="overlay-wrapper bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-175px" style="background-image:url('<%# GetImmage(Eval("unid").ToString()) %>')"></div>--%>
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
														<a href="#" class="fs-4 text-dark fw-bolder text-hover-primary text-dark lh-base" style="min-height:125px;max-height:150px"> <%# Eval("Title") %> </a>
														<!--end::Title-->
														<!--begin::Text-->
														<div class="fw-bold fs-5 text-gray-600 text-dark mt-3 mb-5 overflow-scroll" style="min-height:125px;max-height:150px"><%# GetAddress(Eval("Description")) %></div>
													
													</div>
													<!--end::Body-->
												</div>
																 <div class="card-footer">
        <asp:Button style="float:right;" ID="Button1" runat="server" CssClass="btn btn-dark" CommandName="amount" Text="Tickets"  CommandArgument='<%# Eval("unid") %>'  />
    </div>
												<!--end::Publications post-->
											</div>







														</ItemTemplate>
													</asp:ListView>

			</div>									
									



	