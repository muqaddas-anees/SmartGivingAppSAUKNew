<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlogListCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.BlogListCtrl" %>
<asp:ListView ID="ListFaithGiving" runat="server" OnItemCommand="ListFaithGiving_ItemCommand" >
														<LayoutTemplate>
        <div class="row d-flex d-inline">
			<div runat="server"  id="itemPlaceholder" ></div>
			</div>
			
			
															</LayoutTemplate>

														<ItemTemplate>
															 <div class="col-md-4">
																<div class="card card-bordered">
												<!--begin::Publications post-->
												<div class="card-xl-stretch me-md-6">
													<!--begin::Overlay-->

													<a class="d-block overlay mb-4 ps-5" data-fslightbox="lightbox-hot-sales" href='<%# GetBlogImageUrl(Eval("BlogRef").ToString()) %>'>
														<!--begin::Image-->
														<div class="overlay-wrapper bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-600px" style="background-image:url('<%# GetBlogImageUrl(Eval("BlogRef").ToString()) %>') "></div>
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
														<a href="#" class="fs-4 text-dark fw-bolder text-hover-primary text-dark lh-base"> <%# Eval("BlogTitle") %> </a>
														<!--end::Title-->
														<!--begin::Text-->
														<div class="fw-bold fs-5 text-gray-600 text-dark mt-3 mb-5" style="height:450px;overflow-y:scroll;">  <asp:Literal ID="lblcontent" runat="server" Text='<%# HttpUtility.HtmlDecode( Eval("BlogContent").ToString()) %>'> </asp:Literal> </div>
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
																 <div class="card-footer d-flex justify-content-between">
																	 <asp:HyperLink ID="btnlink1" runat="server" Target="_blank" CssClass="btn btn-primary" NavigateUrl='<%# Eval("Button1Link") %>' Text='<%# Eval("Button1Title") %>' Visible='<%# showButton(Eval("Button1Title")) %>'></asp:HyperLink>

																	  <asp:Button Visible="false" style="float:right;margin-right:10px;margin-left:10px" ID="btnEdit" runat="server"  CommandName="Edit1" Text='<%# Eval("Button1Title") %>'  CommandArgument='<%# Eval("ID") %>'  />
        <asp:Button style="float:right;" Visible="false" ID="Button1" runat="server" CssClass="btn btn-dark" CommandName="amount" Text="Find out more"  CommandArgument='<%# Eval("BlogRef") %>' CausesValidation="false"  />

																	  <asp:HyperLink ID="btnLink2" style="float:right;" runat="server" Target="_blank" CssClass="btn btn-primary" NavigateUrl='<%# Eval("Button2Link") %>' Text='<%# Eval("Button2Title") %>' Visible='<%# showButton(Eval("Button2Title")) %>'></asp:HyperLink>

																	  <%--<asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="btn btn-primary" NavigateUrl='<%# "~/WF/BlogDisplay.aspx?blogref=" +  Eval("BlogRef").ToString() %>' Text='<%# Eval("Button1Title") %>'></asp:HyperLink>--%>
    </div>
												<!--end::Publications post-->
											</div>
</div>


														
														</ItemTemplate>
													</asp:ListView>