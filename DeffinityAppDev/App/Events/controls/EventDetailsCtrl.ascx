<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventDetailsCtrl.ascx.cs" Inherits="DeffinityAppDev.App.Events.controls.EventDetailsCtrl" %>

    <!--begin::Head-->
	<head>
		<title>Event Name and Details</title>
		
		<link rel="canonical" href="Https://preview.keenthemes.com/metronic8" />
		<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />
		<!--begin::Fonts-->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" />
		<!--end::Fonts-->
		<!--begin::Global Stylesheets Bundle(used by all pages)-->
		<link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />
		<!--end::Global Stylesheets Bundle-->
	</head>
	<!--end::Head-->
	<!--begin::Body-->
	<body id="kt_body" class="header-fixed header-tablet-and-mobile-fixed toolbar-enabled toolbar-fixed aside-enabled aside-fixed" style="--kt-toolbar-height:55px;--kt-toolbar-height-tablet-and-mobile:55px">
		<!--begin::Main-->
		



			<!--begin::Content-->
					<div class="content d-flex flex-column flex-column-fluid" id="kt_content">


<%--
						<div class="toolbar" id="kt_toolbar">
							<!--begin::Container-->
							<div id="kt_toolbar_container" class="container-fluid d-flex flex-stack">
								<!--begin::Page title-->
								<div data-kt-swapper="true" data-kt-swapper-mode="prepend" data-kt-swapper-parent="{default: '#kt_content_container', 'lg': '#kt_toolbar_container'}" class="page-title d-flex align-items-center flex-wrap me-3 mb-5 mb-lg-0">
									<!--begin::Title-->
									<%--<h1 class="d-flex align-items-center text-dark fw-bolder fs-3 my-1">     <%# Eval("Title") %>     </h1>
									
									<span class="h-20px border-gray-200 border-start mx-4"></span>
									<!--end::Separator-->
									<!--begin::Breadcrumb-->
									<ul class="breadcrumb breadcrumb-separatorless fw-bold fs-7 my-1">
										<!--begin::Item-->
										<li class="breadcrumb-item text-muted">
											<%--<a href="EventList.aspx" class="text-muted text-hover-primary">Back to Events</a>
										</li>
										<!--end::Item-->
										<!--begin::Item-->
									
										<!--end::Item-->
										<!--begin::Item-->
										
										<!--end::Item-->
										<!--begin::Item-->
										
										<!--end::Item-->
									</ul>
									<!--end::Breadcrumb-->
								</div>
								<!--end::Page title-->
								<!--begin::Actions-->
							
								<!--end::Actions-->
							</div>
							<!--end::Container-->
						</div>--%>



						  <div class="form-group row mb-5">

                                                
                                             
                        	
						<!--end::Toolbar-->
						<!--begin::Post-->
						<div class="post d-flex flex-column-fluid" id="kt_post">
							<!--begin::Container-->
							<div id="kt_content_container" class="container-xxl">
								<!--begin::Post card-->
								<div class="card">
									<!--begin::Body-->
									<div class="card-body p-lg-20 pb-lg-0">

						<asp:ListView runat="server" ID="ListEventDetails" GroupPlaceholderID="groupplaceholder" ItemPlaceholderID="itemplaceholder">

                                        <LayoutTemplate>
                                            <table>



                                                <tr id="groupplaceholder" runat="server"></tr>

                                            </table>
                                        </LayoutTemplate>

                                        <GroupTemplate>
                                            <tr>
                                                <tr id="itemplaceholder" runat="server"></tr>
                                            </tr>
                                        </GroupTemplate>


                                        <ItemTemplate>



                                          
										<!--begin::Layout-->
										<div class="d-flex flex-column flex-xl-row">
											<!--begin::Content-->
											<div class="flex-lg-row-fluid me-xl-15">
												<!--begin::Post content-->
												<div class="mb-17">
													<!--begin::Wrapper-->
													<div class="mb-8">
														<!--begin::Info-->
														
														<!--end::Info-->
														<!--begin::Title-->
														<div  class="text-dark text-hover-primary fs-2 fw-bolder">   
														<h1 class="d-flex align-items-center text-dark fw-bolder fs-3 my-1">     <%# Eval("Title") %>     </h1>
															<!--end::Title-->
															<!--begin::Container-->
															<div class="overlay mt-8">
																<!--begin::Image-->
																<%--<div class="bgi-no-repeat bgi-position-center bgi-size-cover card-rounded min-h-350px"  style=background-image:url('<%# GetImmage(Eval("unid").ToString()) %>')></div>  --%>
																<asp:Image ID="img_top" runat="server" CssClass="img-fluid" ImageUrl='<%# GetImmage(Eval("unid").ToString()) %>' />
																<!--end::Image-->
																<!--begin::Links-->
																
																<!--end::Links-->
															
															</div>
														</div>
														<!--end::Container-->
													</div>
													<!--end::Wrapper-->
													<!--begin::Description-->
														<div class="fs-5 ">

														 <%# Eval("Description") %> 
														</div>
													<!--end::Description-->
													<!--begin::Description-->
													
													<!--begin::Icons-->
													<div class="d-flex flex-center" style="display:none;visibility:hidden;">
														<!--begin::Icon-->
														<a href="#" class="mx-4">          
															<%--<img src="E:/Event MNGT/dist/assets/media/svg/brand-logos/facebook-4.svg" class="h-20px my-2" alt="" /> 1--%>

															<img src="../../assets/media/svg/social-logos/facebook.svg" class="h-20px my-2" alt="" /> 

														</a>
														<!--end::Icon-->
														<!--begin::Icon-->
														<a href="#" class="mx-4">
															<img src="../../assets/media/svg/social-logos/instagram.svg" class="h-20px my-2" alt="" /> 
														</a>
														<!--end::Icon-->
														<!--begin::Icon-->
														<a href="#" class="mx-4">
															<img src="../../assets/media/svg/social-logos/github.svg" class="h-20px my-2" alt="" /> 
														</a>
														<!--end::Icon-->
														<!--begin::Icon-->
														<a href="#" class="mx-4">
															<img src="../../assets/media/svg/brand-logos/behance.svg" class="h-20px my-2" alt="" /> 
														</a>
														<!--end::Icon-->
														<!--begin::Icon-->
														<a href="#" class="mx-4">
															<img src="../../assets/media/svg/social-logos/pinterest-p.svg" class="h-20px my-2" alt="" /> 
														</a>
														<!--end::Icon-->
														<!--begin::Icon-->
														<a href="#" class="mx-4">
															<img src="../../assets/media/svg/social-logos/twitter.svg" class="h-20px my-2" alt="" /> 
														</a>
														<!--end::Icon-->
														<!--begin::Icon-->
														<a href="#" class="mx-4">
															<img src="../../assets/media/svg/social-logos/dribbble-icon-1.svg" class="h-20px my-2" alt="" />
														</a>
														<!--end::Icon-->
													</div>
													<!--end::Icons-->
												</div>
												<!--end::Post content-->
											</div>
											<!--end::Content-->
											<!--begin::Sidebar-->
											<div class="flex-column flex-lg-row-auto w-100 w-xl-300px mb-10">
												<!--begin::Search blog-->
												
												<!--end::Search blog-->
												<!--begin::Catigories-->
												<div class="mb-16">
													<h4 class="text-black mb-7">Event details</h4>
													<!--begin::Item-->

													<div class="d-flex flex-stack fw-bold fs-5 text-muted mb-4">
														<!--begin::Text-->
														<div  class="text-muted text-hover-primary pe-1   ">Start Date & time</div>
														<!--end::Text-->
														<!--begin::Number-->
														<div class="m-0"><%# Eval("StartDateTime","{0:dd/MM/yyyy HH:mm}") %></div>
														<!--end::Number-->
													</div>

													<div class="d-flex flex-stack fw-bold fs-5 text-muted mb-4">
														<!--begin::Text-->
														<div class="text-muted text-hover-primary pe-2"> End Date & time</div>
														<!--end::Text-->
														<!--begin::Number-->
														<div class="m-0"><%# Eval("EndDateTime" ,"{0:dd/MM/yyyy HH:mm}") %></div>
														<!--end::Number-->
													</div>


													<div class="d-flex flex-stack fw-bold fs-5 text-muted mb-4">
														<!--begin::Text-->
														<div href="#" class="text-muted text-hover-primary pe-2">Price</div>
														<!--end::Text-->
														<!--begin::Number-->
														<div class="m-0"><%# Eval("Price") %></div>
														<!--end::Number-->
													</div>
													<!--end::Item-->
													<!--begin::Item-->
													<div class="d-flex flex-stack fw-bold fs-5 text-muted mb-10">
														<%--<!--begin::Text-->
														<a href="#" class="text-muted text-hover-primary pe-2">By Company News</a>--%>
														<!--end::Text-->
														<!--begin::Number-->
													
														<!--end::Number-->
													</div>
													<!--end::Item-->
													<!--begin::Item-->
													<div class="d-flex flex-stack fw-bold fs-5 text-muted mb-4">
														<!--begin::Text-->
														<a href="#" class="text-muted text-hover-primary pe-2"><%--800  Followers--%></a>
														<!--end::Text-->
														<!--begin::Number-->
														<div class="d-flex my-0">
															<%--<a href="#" class="btn btn-sm me-0 btn-light" id="kt_user_follow_button">
																<!--begin::Svg Icon | path: icons/duotune/arrows/arr012.svg-->
																<span class="svg-icon svg-icon-3 ">
																	<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
																		<path opacity="0.3" d="M10 18C9.7 18 9.5 17.9 9.3 17.7L2.3 10.7C1.9 10.3 1.9 9.7 2.3 9.3C2.7 8.9 3.29999 8.9 3.69999 9.3L10.7 16.3C11.1 16.7 11.1 17.3 10.7 17.7C10.5 17.9 10.3 18 10 18Z" fill="currentColor"></path>
																		<path d="M10 18C9.7 18 9.5 17.9 9.3 17.7C8.9 17.3 8.9 16.7 9.3 16.3L20.3 5.3C20.7 4.9 21.3 4.9 21.7 5.3C22.1 5.7 22.1 6.30002 21.7 6.70002L10.7 17.7C10.5 17.9 10.3 18 10 18Z" fill="currentColor"></path>
																	</svg>
																</span>
																<!--end::Svg Icon-->
																<!--begin::Indicator-->
																<span class="indicator-label">Follow</span>
																<span class="indicator-progress">Please wait... 
																<span class="spinner-border spinner-border-sm align-middle ms-2"></span></span>
																<!--end::Indicator-->
															</a>--%>
														</div>
														<!--end::Number-->
													</div>
													<!--end::Item-->
													<!--begin::Item-->
													
													<!--end::Item-->
													<!--begin::Item-->
													
													<!--end::Item-->
													<!--begin::Item-->
													
													<!--end::Item-->
												</div>
												<!--end::Catigories-->
												<asp:Button ID="btnBookTickets" ClientIDMode="Static" runat="server" CssClass="btn btn-primary btn-lg btn-block w-100" Text="Tickets" OnClick="btnBookTickets_Click" />
												<%--<button class="" data-bs-toggle="modal" data-bs-target="#kt_modal_create_campaign">Tickets</button>--%>
												<a class="btn btn-primary btn-lg btn-block mt-3 w-100" id="livestream">Livestream</a>
												<!--begin::Recent posts-->
												<div class="mt-10" id="location">
													
<div class="col-md-12 col-xxl-12">

														<a href="#" class="fs-4 text-gray-800 text-hover-primary fw-bolder mb-0">Location</a>
														
														<div class=" ">
															<div class="fs-7 fw-bolder text-gray-700"> <%#  GetAddress( Eval("Venue_Name"),Eval("Address1"), Eval("Address2"),Eval("City"),Eval("state_Province"), Eval("Postalcode"),Eval("Country")) %> </div>
														<%--	<div class="fw-bold text-gray-400 mb-6 mt-5 "><a href=""> 
																<i class="bi bi-pin-map-fill"></i>
																Google map location</a> </div>--%>
														</div>
													


													</div>
													<!--end::Item-->
												</div>

												<div class="mt-10">
													

													<div class="col-md-12 col-xxl-12">
														<a href="#" class="fs-4 text-gray-600 text-hover-primary fw-bolder mb-0">Refund Policy</a>
														
														<div class=" ">
															<div class="fs-7 fw-bolder text-gray-700"><asp:Literal ID="lblRefund" runat="server" Text='<%# Eval("RefundPolicy") %>'></asp:Literal></div>
															
														</div>
													


													</div>
													<!--end::Item-->
												</div>
												<!--end::Recent posts-->
											</div>
											<!--end::Sidebar-->
										</div>
										<!--end::Layout-->
										<!--begin::Section-->
										
										<!--end::Section-->
										<!--begin::Section-->
										
										<!--end::Section-->
									


                                          

                                        </ItemTemplate>


                                    </asp:ListView>


											<div class="fs-5 fw-bold text-gray-600 col-lg-12 overflow-scroll">

														<asp:GridView runat="server" ID="BannerList" >

												<Columns>
													<asp:TemplateField Visible="false">
														<ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtn" runat="server" CommandArgument='<%# Eval("Speaker_ID") %>' CommandName="edit1" SkinID="BtnLinkEdit"></asp:LinkButton>
                                                            
															 <%-- <input id="Button1" type="button" value="Edit" onclick="EditSpeakerDetails('<%# Eval("Speaker_ID") %>');" class="btn btn-gray" > </input >--%>
                                                                                    
														</ItemTemplate>
													</asp:TemplateField>
													<%--<asp:TemplateField>
														<ItemTemplate>
															
														</ItemTemplate>
													</asp:TemplateField>--%>
													<asp:TemplateField HeaderText="Speaker(s)" ItemStyle-Width="80%">
														<ItemTemplate>
															<div class="row mb-6">
																	<div class="col-lg-3 text-center">
																		 <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# GetImageUrl(Eval("Speaker_Photo").ToString()) %>' Width="100px" Height="100px" />
																		</div>
																	<div class="col-lg-9">
																		<asp:Label ID="lblSpeaker" runat="server" Text=' <%# Eval("Speaker_Name") %> ' Font-Bold="true" Font-Size="22"></asp:Label>
															<br />

															<asp:Label ID="lblBio" runat="server" Text=' <%# Eval("Speaker_Bio") %>'></asp:Label>
															<br />
															<asp:HyperLink ID="linkedinlink" runat="server" CssClass="btn btn-primary" Text="View LinkedIn Profile" NavigateUrl='<%# Eval("LinkedIn") %>' Target="_blank"></asp:HyperLink>
																		</div>

															</div>

															
															
														</ItemTemplate>
													</asp:TemplateField>
												
													
												</Columns>
											</asp:GridView>

														</div>
										<div class="fs-5 fw-bold text-gray-600 col-lg-12 mb-6">

														<asp:GridView runat="server" ID="gridSponsors" >

												<Columns>
													<asp:TemplateField Visible="false">
														<ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtn" runat="server" CommandArgument='<%# Eval("SponsorId") %>' CommandName="edit1" SkinID="BtnLinkEdit"></asp:LinkButton>
                                                            
															 <%-- <input id="Button1" type="button" value="Edit" onclick="EditSpeakerDetails('<%# Eval("Speaker_ID") %>');" class="btn btn-gray" > </input >--%>
                                                                                    
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField>
														<ItemTemplate>
															 <asp:Image ID="imgLogo" runat="server" ImageUrl='<%# GetSponsorImageUrl(Eval("SponsorId").ToString()) %>' Width="100px" Height="100px" />
														</ItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderText="Sponsors" ItemStyle-Width="80%">
														<ItemTemplate>
															<asp:Label ID="lblSpeaker" runat="server" Text=' <%# Eval("CompanyName") %> ' Font-Bold="true" Font-Size="22"></asp:Label>
															<br />

															<asp:Label ID="lblBio" runat="server" Text=' <%# Eval("AboutCompany") %>'></asp:Label>
															<br />
															<asp:HyperLink ID="linkedinlink" runat="server" CssClass="btn btn-primary" Text="View LinkedIn Profile" NavigateUrl='<%# Eval("SponsorUNID") %>' Target="_blank" Visible="false"></asp:HyperLink>
															
														</ItemTemplate>
													</asp:TemplateField>
												
													
												</Columns>
											</asp:GridView>

														</div>

										</div>
									<!--end::Body-->
								</div>
								<!--end::Post card-->
							</div>
							<!--end::Container-->
						</div>
						<!--end::Post-->
                                            </div>
					

					
					</div>
					<!--end::Content-->



		<asp:HiddenField ID="hIsinperson" ClientIDMode="Static" runat="server" />
		<asp:HiddenField ID="hunid" ClientIDMode="Static" runat="server" />
	<script>
        window.onload = function () {
            // Get the values from the hidden fields
            var isInPerson = document.getElementById('<%= hIsinperson.ClientID %>').value;
        var unid = document.getElementById('<%= hunid.ClientID %>').value;

            // Get the livestream link
			var livestreamLink = document.getElementById("livestream");
			var location = document.getElementById("location");


            // Check if isInperson is "false"
            if (isInPerson === "false") {
				// Display the livestream button and set the href
				location.style.display = "none";
                livestreamLink.style.display = "block";
                livestreamLink.href = "../liveevent.aspx?unid=" + unid;
            } else {
                // Hide the livestream button if in-person
				livestreamLink.style.display = "none";
				location.style.display="block"
            }
        };
</script>
		<!--end::Main-->
		<!--begin::Javascript-->
		<!--begin::Global Javascript Bundle(used by all pages)-->
		<%--<script src="assets/plugins/global/plugins.bundle.js"></script>
		<script src="assets/js/scripts.bundle.js"></script>--%>
		<!--end::Global Javascript Bundle-->
		<!--begin::Page Vendors Javascript(used by this page)-->
		<%--<script src="assets/plugins/custom/fslightbox/fslightbox.bundle.js"></script>--%>
		<!--end::Page Vendors Javascript-->
		<!--begin::Page Custom Javascript(used by this page)-->
		<%--<script src="assets/js/custom/widgets.js"></script>
		<script src="assets/js/custom/apps/chat/chat.js"></script>
		<script src="assets/js/custom/modals/create-app.js"></script>
		<script src="assets/js/custom/modals/upgrade-plan.js"></script>--%>
		<!--end::Page Custom Javascript-->
		<!--end::Javascript-->
	</body>
	<!--end::Body-->













	 <%--<asp:ListView runat="server" ID="BannerList" GroupPlaceholderID="groupplaceholder" ItemPlaceholderID="itemplaceholder">

                                        <LayoutTemplate>
                                           
                                        </LayoutTemplate>

                                        <GroupTemplate>
                                            <tr>
                                                <tr id="itemplaceholder" runat="server"></tr>
                                            </tr>
                                        </GroupTemplate>


                                        <ItemTemplate>




                                        </ItemTemplate>


                                    </asp:ListView>--%>








	


