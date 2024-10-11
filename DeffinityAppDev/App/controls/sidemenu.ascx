<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="sidemenu.ascx.cs" Inherits="DeffinityAppDev.App.controls.sidemenu" %>


<%--<div class="aside-nav d-flex flex-column align-lg-center flex-column-fluid w-100 pt-5 pt-lg-0" id="kt_aside_nav">
						<!--begin::Primary menu-->
						<div id="kt_aside_menu" class="menu menu-column menu-title-gray-600 menu-state-primary menu-state-icon-primary menu-state-bullet-primary menu-arrow-gray-500 fw-bold fs-6" data-kt-menu="true" >--%>
<div class="aside-menu flex-column-fluid">
    <!--begin::Aside Menu-->
    <div class="hover-scroll-overlay-y my-5 my-lg-5" id="kt_aside_menu_wrapper" data-kt-scroll="true" data-kt-scroll-activate="{default: false, lg: true}" data-kt-scroll-height="auto" data-kt-scroll-dependencies="#kt_aside_logo, #kt_aside_footer" data-kt-scroll-wrappers="#kt_aside_menu" data-kt-scroll-offset="0">
        <!--begin::Menu-->
        <div class="menu menu-column menu-title-gray-800 menu-state-title-primary menu-state-icon-primary menu-state-bullet-primary menu-arrow-gray-500" id="#kt_aside_menu" data-kt-menu="true">

            <%--<div class="menu-item py-2" style="display:none;visibility:hidden;">
								<a class="menu-link active menu-center" href='<%:ResolveClientUrl("~/App/Home.aspx")%>' title="Home" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
									<span class="menu-icon me-0">
										<i class="bi bi-house fs-2"></i>
									</span>
								</a>
							</div>--%>

            <%--	<div class="menu-item py-2">
								<a  href='<%:ResolveClientUrl("~/App/TithingDetails1.aspx")%>' class="menu-link " title="Organizations" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
									<span class="menu-icon me-0">
										<i class="fa fa-money-bill-wave fs-2"></i>
									</span>
								</a>
								
							</div>--%>
            <%--	<div class="menu-item">
									<div class="menu-content pb-2">
										<span class="menu-section text-muted text-uppercase fs-8 ls-1">Dashboard</span>
									</div>
								</div>--%>
            <div class="app-sidebar-footer flex-column-auto pt-2 pb-2 px-6" id="kt_app_sidebar_footer" runat="server">
                <a href="~/App/OnboardingGuide.aspx" id="a_tithing" runat="server" style="background-color: #50CD89; color: white;" class="btn btn-flex flex-center btn-custom btn-video overflow-hidden text-nowrap px-0 h-40px w-100" title="Getting Started" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
                    <span class="menu-icon">
                        <i class="bi bi-play-circle-fill fs-2" style="color: white;"></i>
                    </span>
                    <span class="btn-label">Getting Started</span>
                </a>

            </div>
            <div class="menu-item py-2" id="Div3" runat="server">
                <a href="~/App/Dashboard.aspx" id="a2" runat="server" class="menu-link " title="Donation Dashboard" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
                    <span class="menu-icon">
                        <i class="fas fa-tachometer-alt fs-2"></i>
                    </span>
                    <span class="menu-title">Donation Dashboard</span>
                </a>

            </div>
            <div data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2" id="Div6" runat="server">
                <span class="menu-link " title="" data-bs-toggle="tooltip"
                    data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Use this section to log donations">
                    <span class="menu-icon">
                        <i class="bi bi-play-circle-fill fs-2"></i>
                    </span>
                    <span class="menu-title">Donations</span>
                    <span class="menu-arrow"></span>
                </span>
                <div class="menu-sub menu-sub-accordion menu-active-bg">
                    <div data-kt-menu-trigger="click" class="menu-item menu-accordion">
                        <%--<div class="menu-item">
										<div class="menu-content">
											<span class="menu-section fs-5 fw-bolder ps-1 py-1">Members</span>
										</div>
									</div>--%>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~//App/Donations.aspx")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">Donate</span>
                            </a>
                        </div>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~/App/OtherDonationList.aspx?type=inkind")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">In-Kind Donations</span>
                            </a>
                        </div>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~/App/OtherDonationList.aspx?type=cash")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">Cash Donations</span>
                            </a>
                        </div>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~/App/DashboardReport.aspx")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">Report</span>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2" id="link_members" runat="server">
                <span class="menu-link " title="" data-bs-toggle="tooltip"
                    data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Donor CRM">
                    <span class="menu-icon">
                        <i class="fas fa-user-cog fs-2"></i>
                    </span>
                    <span class="menu-title">Donor CRM</span>
                    <span class="menu-arrow"></span>
                </span>
                <div class="menu-sub menu-sub-accordion menu-active-bg">
                    <div data-kt-menu-trigger="click" class="menu-item menu-accordion">
                        <%--<div class="menu-item">
										<div class="menu-content">
											<span class="menu-section fs-5 fw-bolder ps-1 py-1">Members</span>
										</div>
									</div>--%>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~/App/DonorCRM.aspx")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">Donor List</span>
                            </a>
                        </div>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~/App/Member.aspx?type=2")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">Add New Donor</span>
                            </a>
                        </div>
                    </div>
                </div>
            </div>

            <div data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2" id="link_faithgiving" runat="server">
                <span class="menu-link " title="" data-bs-toggle="tooltip"
                    data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Fundraisers">
                    <span class="menu-icon">
                        <i class="fas fa-hands fs-2"></i>
                    </span>
                    <span class="menu-title">Fundraisers</span>
                    <span class="menu-arrow"></span>
                </span>
                <div class="menu-sub menu-sub-accordion menu-active-bg">
                    <div data-kt-menu-trigger="click" class="menu-item menu-accordion">
                        <%--<div class="menu-item">
										<a class="menu-link" href='<%:ResolveClientUrl("~/App/FundraiserDashbord.aspx")%>'>
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">Dashboard</span>
										</a>
									</div>--%>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~/App/FundraiserListView.aspx")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">View Fundraising Campaigns</span>
                            </a>
                        </div>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~/App/Fundraiser/AddFundraiser.aspx?type=main")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">Create New Fundraising Campaign</span>
                            </a>
                        </div>
                        <%--<div class="menu-item">
										<a class="menu-link" href='<%:ResolveClientUrl("~/App/Fundraiser/FundraiserListView.aspx?type=main")%>'>
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">View Fundraising Campaigns V2</span>
										</a>
									</div>
										<div class="menu-item">
										<a class="menu-link" href='<%:ResolveClientUrl("~/App/Fundraiser/AddFunraiser.aspx?type=main")%>'>
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">Create New Fundraising Campaign V2</span>
										</a>
									</div>--%>
                    </div>
                </div>
            </div>
             <div class='menu-item py-2' id='pnlFundCamp' runat='server'>
     <a href='<%:ResolveClientUrl("~/App/FundraiserListView.aspx?type=camp")%>' class='menu-link' title='Landing Page' data-bs-toggle='tooltip' data-bs-trigger='hover' data-bs-dismiss='click' data-bs-placement='right'>
         <span class='menu-icon'>
             <i class='fas fa-hands fs-2'></i>
         </span>
         <span class='menu-title'>P2P Participation</span>
     </a>
 </div>
   <div data-kt-menu-trigger='click' data-kt-menu-placement='right-start' data-kt-menu-flip='bottom'  class='menu-item py-2' id='link_eventmanagement' runat='server'>
       <span class='menu-link' title='' data-bs-toggle='tooltip' data-bs-trigger='hover' data-bs-dismiss='click' data-bs-placement='right' data-bs-original-title='<%:sessionKeys.JobsDisplayName %>'>
           <span class='menu-icon'>
               <i class='fas fa-project-diagram fs-2'></i>
           </span>
           <span class='menu-title'><%:sessionKeys.JobsDisplayName %></span>
           <span class='menu-arrow'></span>
       </span>
       <div class='menu-sub menu-sub-accordion menu-active-bg'>
           <div data-kt-menu-trigger='click' class='menu-item menu-accordion'>
               <div class='menu-item'>
                   <a class='menu-link' href='<%:ResolveClientUrl("~/WF/DC/Joblist.aspx")%>'>
                       <span class='menu-bullet'>
                           <span class='bullet bullet-dot'></span>
                       </span>
                       <span class='menu-title'>View Current Projects</span>
                   </a>
               </div>
               <div class='menu-item'>
                   <a class='menu-link' href='<%:ResolveClientUrl("~/WF/DC/FLSForm.aspx")%>'>
                       <span class='menu-bullet'>
                           <span class='bullet bullet-dot'></span>
                       </span>
                       <span class='menu-title'>Add <%:sessionKeys.JobDisplayName %></span>
                   </a>
               </div>
               <div class='menu-item' style='display: none; visibility: hidden;'>
                   <a class='menu-link' href='<%:ResolveClientUrl("~/WF/DC/FRPApprovals.aspx")%>'>
                       <span class='menu-bullet'>
                           <span class='bullet bullet-dot'></span>
                       </span>
                       <span class='menu-title'>Invoice Journal</span>
                   </a>
               </div>
           </div>
       </div>
   </div>            <div class="menu-item py-2" id="link_pagebuilder" runat="server" visible="true">
                <a href='<%:ResolveClientUrl("~/App/PublicViewSetup.aspx")%>' class="menu-link " title="Landing Page" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
                    <span class="menu-icon">
                        <i class="fab fa-sellsy fs-2"></i>
                    </span>
                    <span class="menu-title">Landing Page</span>
                </a>

            </div>
            <div class="menu-item py-2" id="link_TextToDonate" runat="server">
                <a href="~/App/TextToDonate.aspx" id="a1" runat="server" class="menu-link " title="SMS Marketing" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
                    <span class="menu-icon">
                        <i class="far fa-comment-dots fs-2"></i>
                    </span>
                    <span class="menu-title">SMS Marketing</span>
                </a>

            </div>

            <div data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2" id="link_activites" runat="server">
                <span class="menu-link " title="" data-bs-toggle="tooltip"
                    data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Events Management">
                    <span class="menu-icon">
                        <i class="fas fa-theater-masks  fs-2"></i>
                    </span>
                    <span class="menu-title">Events Management</span>
                    <span class="menu-arrow"></span>
                </span>
                <div class="menu-sub menu-sub-accordion menu-active-bg">
                    <div data-kt-menu-trigger="click" class="menu-item menu-accordion">
                        <%--<div class="menu-item">
										<div class="menu-content">
											<span class="menu-section fs-5 fw-bolder ps-1 py-1">Events</span>
										</div>
									</div>--%>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~/App/Events/EventList.aspx")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">View Events</span>
                            </a>
                        </div>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~/App/Events/BasicInfo.aspx")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">Create An Event</span>
                            </a>
                        </div>
                        <%--	<div class="menu-item">
										<a class="menu-link" href='<%:ResolveClientUrl("~/App/Events/EventsReport.aspx")%>'>
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">View Report</span>
										</a>
									</div>--%>
                    </div>
                </div>
            </div>
            <div data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2" id="Div4" runat="server" style="display: none; visibility: hidden;">
                <span class="menu-link " title="" data-bs-toggle="tooltip"
                    data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Online Shop">
                    <span class="menu-icon">
                        <i class="bi bi-basket fs-2"></i>
                    </span>
                    <span class="menu-title">Online Shop</span>
                    <span class="menu-arrow"></span>
                </span>
                <div class="menu-sub menu-sub-accordion menu-active-bg">
                    <div data-kt-menu-trigger="click" class="menu-item menu-accordion">
                        <%--<div class="menu-item">
										<div class="menu-content">
											<span class="menu-section fs-5 fw-bolder ps-1 py-1">Events</span>
										</div>
									</div>--%>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~/App/Products/AddProduct.aspx")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">List a Product</span>
                            </a>
                        </div>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~/App/Products/ViewProducts.aspx")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">View Products</span>
                            </a>
                        </div>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~/App/Products/ViewOrders.aspx")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">View Orders</span>
                            </a>
                        </div>

                    </div>
                </div>
            </div>

            <div data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2" id="Div2" runat="server" visible="false">
                <span class="menu-link " title="" data-bs-toggle="tooltip"
                    data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Activities">
                    <span class="menu-icon">
                        <i class="fas fa-theater-masks  fs-2"></i>
                    </span>
                    <span class="menu-title">Activities</span>
                    <span class="menu-arrow"></span>
                </span>
                <div class="menu-sub menu-sub-accordion menu-active-bg">
                    <div data-kt-menu-trigger="click" class="menu-item menu-accordion">
                        <%--<div class="menu-item">
										<div class="menu-content">
											<span class="menu-section fs-5 fw-bolder ps-1 py-1">Events</span>
										</div>
									</div>--%>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~/App/Activity/Activitylist.aspx")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">View Activities</span>
                            </a>
                        </div>
                        <div class="menu-item">
                            <a class="menu-link" href='<%:ResolveClientUrl("~/App/Activity/AddActivity.aspx")%>'>
                                <span class="menu-bullet">
                                    <span class="bullet bullet-dot"></span>
                                </span>
                                <span class="menu-title">Add Activity</span>
                            </a>
                        </div>


                    </div>
                </div>
            </div>









            <div class="menu-item py-2" id="Div1" runat="server" style="display: none; visibility: hidden;">
                <a href='<%:ResolveClientUrl("~/WF/DC/BusinessServices.aspx")%>' class="menu-link " title="Business Services" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
                    <span class="menu-icon">
                        <i class="fab fa-buffer fs-2"></i>
                    </span>
                    <span class="menu-title">Business Services</span>
                </a>

            </div>




            <div class="menu-item py-2" id="link_education" runat="server" visible="false">
                <a href='https://my.socialimpacthubacademy.com' target="_blank" class="menu-link " title="Settings" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
                    <span class="menu-icon">
                        <i class="fab fa-youtube fs-2"></i>
                    </span>
                    <span class="menu-title">Academy</span>
                </a>

            </div>


            <%--<div data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2" >
								<span class="menu-link " title="" data-bs-toggle="tooltip" 
									data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Academy">
									<span class="menu-icon">
										<i class="fab fa-youtube fs-2"></i>
									</span>
									<span class="menu-title">Academy</span>
									<span class="menu-arrow"></span>
								</span>
								<%--<div class="menu-sub menu-sub-accordion menu-active-bg">
									<div data-kt-menu-trigger="click" class="menu-item menu-accordion">
									
									<div class="menu-item">
										<a class="menu-link" href='<%:ResolveClientUrl("~/App/FaithEducationList.aspx")%>'>
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">View Academy Videos</span>
										</a>
									</div>
									<div class="menu-item">
										<a class="menu-link" href='<%:ResolveClientUrl("~/App/AddEducationalVideo.aspx")%>'>
											<span class="menu-bullet">
												<span class="bullet bullet-dot"></span>
											</span>
											<span class="menu-title">Add Academy Video</span>
										</a>
									</div>
										</div>
								</div>
							</div>--%>
            <%--<div class="menu-item py-2">
								<a href='<%:ResolveClientUrl("~/App/FaithEducationList.aspx")%>' class="menu-link " title="Faith Union On Demand Courses" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
									<span class="menu-icon">
										<i class="fa fa-chalkboard-teacher fs-2"></i>
									</span>
								</a>
								
							</div>--%>

            <%--<div class="menu-item py-2">
								<a href='<%:ResolveClientUrl("~/WF/PageBuilder.aspx")%>' class="menu-link " title="Public View" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
									<span class="menu-icon">
										<i class="bi bi-file-text fs-2"></i>
									</span>
								</a>
								
							</div>--%>

            <div data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2" id="link_timesheets" runat="server" style="display: none; visibility: hidden">
                <span class="menu-link " title="" data-bs-toggle="tooltip"
                    data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Timesheet">
                    <span class="menu-icon">
                        <i class="far fa-clock  fs-2"></i>
                    </span>
                    <span class="menu-title">Timesheet</span>
                </span>
                <div class="menu-sub menu-sub-dropdown w-225px px-1 py-4">
                    <div class="menu-item">
                        <div class="menu-content">
                            <span class="menu-section fs-5 fw-bolder ps-1 py-1">Timesheets</span>
                        </div>
                    </div>
                    <div class="menu-item">
                        <a class="menu-link" href='<%:ResolveClientUrl("~/WF/DC/Timesheets/AddTimesheets.aspx") %>'>
                            <span class="menu-bullet">
                                <span class="bullet bullet-dot"></span>
                            </span>
                            <span class="menu-title">Add Timesheets</span>
                        </a>
                    </div>
                    <div class="menu-item">
                        <a class="menu-link" href='<%:ResolveClientUrl("~/WF/DC/Timesheets/ApproveTimesheets.aspx") %>'>
                            <span class="menu-bullet">
                                <span class="bullet bullet-dot"></span>
                            </span>
                            <span class="menu-title">Approve Timesheets</span>
                        </a>
                    </div>
                    <div class="menu-item">
                        <a class="menu-link" href='<%:ResolveClientUrl("~/WF/DC/Timesheets/TimesheetReport.aspx") %>'>
                            <span class="menu-bullet">
                                <span class="bullet bullet-dot"></span>
                            </span>
                            <span class="menu-title">Timesheet Report</span>
                        </a>
                    </div>
                    <div class="menu-item">
                        <a class="menu-link" href='<%:ResolveClientUrl("~/WF/DC/Timesheets/Payroll.aspx") %>'>
                            <span class="menu-bullet">
                                <span class="bullet bullet-dot"></span>
                            </span>
                            <span class="menu-title">Payroll</span>
                        </a>
                    </div>
                </div>
            </div>

            <div data-kt-menu-trigger="click" data-kt-menu-placement="right-start" data-kt-menu-flip="bottom" class="menu-item py-2" id="link_expenses" runat="server" style="display: none; visibility: hidden">
                <span class="menu-link " title="" data-bs-toggle="tooltip"
                    data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right" data-bs-original-title="Expenses">
                    <span class="menu-icon">
                        <i class="far fa-money-bill-alt  fs-2"></i>
                    </span>
                    <span class="menu-title">Expenses</span>
                </span>
                <div class="menu-sub menu-sub-dropdown w-225px px-1 py-4">
                    <div class="menu-item">
                        <div class="menu-content">
                            <span class="menu-section fs-5 fw-bolder ps-1 py-1">Expenses</span>
                        </div>
                    </div>
                    <div class="menu-item">
                        <a class="menu-link" href='<%:ResolveClientUrl("~/WF/DC/Expenses/AddExpenses.aspx") %>'>
                            <span class="menu-bullet">
                                <span class="bullet bullet-dot"></span>
                            </span>
                            <span class="menu-title">Add Expenses</span>
                        </a>
                    </div>
                    <div class="menu-item">
                        <a class="menu-link" href='<%:ResolveClientUrl("~/WF/DC/Expenses/ExpensesReport.aspx") %>'>
                            <span class="menu-bullet">
                                <span class="bullet bullet-dot"></span>
                            </span>
                            <span class="menu-title">Expenses Report</span>
                        </a>
                    </div>
                </div>
            </div>
            <div class="menu-item py-2" style="display: none; visibility: hidden;">
                <a href='<%:ResolveClientUrl("~/WF/CustomerAdmin/Campaign/CampaignList.aspx")%>' class="menu-link " title="Messaging" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
                    <span class="menu-icon">
                        <i class="fas fa-mail-bulk fs-2"></i>
                    </span>
                    <span class="menu-title">Messaging</span>
                </a>
            </div>
            <div class="menu-item py-2" style="display: none; visibility: hidden;">
                <a href='<%:ResolveClientUrl("~/App/sessions/LiveSessions.aspx")%>' class="menu-link " title="Live Session" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
                    <span class="menu-icon">
                        <i class="fas fa-mail-bulk fs-2"></i>
                    </span>
                    <span class="menu-title">Live Session</span>
                </a>
            </div>

            <div class=" menu-item py-2" style="display:none" id="Div5" runat="server">
    <a href='<%:ResolveClientUrl("~/App/Beneficiaries/GetBeneficiaries.aspx")%>' class="menu-link " title="Beneficiaries" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
        <span class="menu-icon">
            <i class="bi bi-box2-heart"></i>      

        </span>
        <span class="menu-title">Beneficiaries</span>
    </a>

</div>
            <div class="menu-item py-2" id="link_settings" runat="server">
                <a href='<%:ResolveClientUrl("~/App/Settings.aspx")%>' class="menu-link " title="Settings" data-bs-toggle="tooltip" data-bs-trigger="hover" data-bs-dismiss="click" data-bs-placement="right">
                    <span class="menu-icon">
                        <i class="fas fa-cogs fs-2"></i>
                    </span>
                    <span class="menu-title">Settings</span>
                </a>

            </div>
        </div>
        <!--end::Primary menu-->
    </div>

</div>
