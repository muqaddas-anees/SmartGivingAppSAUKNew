<%@ Control Language="C#" AutoEventWireup="true" Inherits="WF_SideMenu" Codebehind="SideMenu.ascx.cs" %>
<!-- Add "fixed" class to make the sidebar fixed always to the browser viewport. -->
		<!-- Adding class "toggle-others" will keep only one menu item open at a time. -->
		<!-- Adding class "collapsed" collapse sidebar root elements and show only icons. -->

         <div class="sidebar-menu toggle-others collapsed">
			
			<div class="sidebar-menu-inner">	
        <header class="logo-env">
			<!-- logo -->
					<div class="logo">
						<a id="link_logo" runat="server" href="~/WF/Projects/ProjectHome.aspx" class="logo-expanded">
							<img   alt="" runat="server" id="img_logo" />
						</a>
						<a id="link_small_logo" runat="server" href="~/WF/Projects/ProjectHome.aspx" class="logo-collapsed">
							<img alt=""  runat="server" id="img_logo_small" class="img-responsive" style="padding:5px" />
						</a>
                        <%--<a href="#" data-toggle="settings-pane" data-animate="true">
					<i class="linecons-cog"></i>
				</a>--%>
					</div>
					<!-- Mobile Toggles Links -->
			<%--<div class="nav navbar-mobile">--%>
			
				<!-- This will toggle the mobile menu and will be visible only on mobile devices -->
				<div class="mobile-menu-toggle visible-xs">
					<a href="#" data-toggle="user-info-menu">
							<i class="fa-bell-o"></i>
							<span class="badge badge-success">7</span>
						</a>
						<a href="#" data-toggle="mobile-menu">
							<i class="fa-bars"></i>
						</a>
				</div>
				<!-- This will open the popup with user profile settings, you can use for any purpose, just be creative -->
					<div class="settings-icon" style="display:none;visibility:hidden;">
						<a href="#" data-toggle="settings-pane" data-animate="true">
							<i class="linecons-cog"></i>
						</a>
					</div>
			<%--</div>--%>
			</header>
				<div  style="position:absolute;z-index:5;transform:rotate(270deg);padding-right:60px;margin-top:300px;margin-left:-80px" id="lbtnShowUpgrade" runat="server">
					<div  style="visibility: visible; height: 3px;">
						<div><span class="badge badge-warning" style="padding:12px;font-size:large;"><a href='<%: this.ResolveClientUrl("~/WF/CustomerAdmin/UpgradePlanV2.aspx") %>' style="color:white;"> Upgrade</a></span></div></div></div>
               <%-- <div class="navbar-mobile-clear"></div>--%>
				<ul id="main-menu" class="main-menu">
                      <%--<li>
						<a href='<%:ResolveClientUrl("~/WF/CustomerAdmin/PortfolioContacts.aspx") %>' data-toggle="tooltip" title='<%= Resources.DeffinityRes.Contacts%>'>
							<i class="fa-group"></i>
							<span class="title"><%= Resources.DeffinityRes.Contacts%></span>
						</a>
                       </li>--%>
					 <li id="li2" runat="server">
						<a href='<%:ResolveClientUrl("~/WF/DC/DashboardV2.aspx") %>' data-toggle="tooltip" title='Home'>
							<i class="fa-home"></i>
							<span class="title">Home</span>
						</a>
                    </li>
					 <li id="li1" runat="server" >
						<a href='<%:ResolveClientUrl("~/WF/DC/DashboardV2.aspx") %>' data-toggle="tooltip" title='Quick Pay'>
							<i class="fa-credit-card"></i>
							<span class="title">Quick Pay</span>
						</a>
                    </li>
					   <li runat="server" id="li_Lead" >
						<a href='<%: this.ResolveClientUrl("~/WF/CustomerAdmin/ContactLeads.aspx") %>' data-toggle="tooltip" title='Leads'>
							<i class="fa-lightbulb-o"></i>
							<span class="title">Leads </span>
						</a>
                    </li>
                    <li>
						<a href="#" data-toggle="tooltip" title='<%= Resources.DeffinityRes.Contacts%>'>
							<i class="fa-group "></i>
							<span class="title"><%= Resources.DeffinityRes.Contacts%></span>
						</a>
                        <ul>
							 <li>
								<a href='<%:ResolveClientUrl("~/WF/CustomerAdmin/PortfolioContacts.aspx") %>' title='<%= Resources.DeffinityRes.View%> <%= Resources.DeffinityRes.Contacts%>'>
													
													<span class="title"><%= Resources.DeffinityRes.View%> <%= Resources.DeffinityRes.Contacts%></span>
												</a>
                                 </li>
                             <li>
								<a href='<%:ResolveClientUrl("~/WF/CustomerAdmin/ContactDetails.aspx") %>'>
									
									<span class="title"><%= Resources.DeffinityRes.AddNewContact%></span>
								</a>
                                 </li>
                        </ul>
                    </li>
					 
                     <li id="link_servicedesk" runat="server">
						<a href='#' data-toggle="tooltip" title='<%= Resources.DeffinityRes.ServiceDesk%>'>
							<i class="fa-wrench"></i>
							<span class="title"><%= Resources.DeffinityRes.ServiceDesk%></span>
						</a>
						  <ul>
							 <li>
								<a href='<%:ResolveClientUrl("~/WF/DC/FLSForm.aspx") %>' title='<%= Resources.DeffinityRes.LogNewServiceReq%> '>
													<span class="title"><%= Resources.DeffinityRes.LogNewServiceReq%></span>
												</a>
                                 </li>
                             <li>
								<a href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS") %>'>
									
									<span class="title"><%= Resources.DeffinityRes.View%> <%= Resources.DeffinityRes.ServiceDesk%></span>
								</a>
                                 </li>

							  </ul>

							  
									 
                    </li>
					 <li id="link_timesheet" runat="server">
						<a href="#" data-toggle="tooltip" title='<%= Resources.DeffinityRes.Timesheets%>'>
							<i class="linecons-clock "></i>
							<span class="title"><%= Resources.DeffinityRes.Timesheets%></span>
						</a>
                       <ul>

							 <li>
								<a href='<%:ResolveClientUrl("~/WF/DC/Timesheets/AddTimesheets.aspx") %>' title='Add Timesheet'>
													<span class="title">Add Timesheets</span>
												</a>
                                 </li>
										 <li>
								<a href='<%:ResolveClientUrl("~/WF/DC/Timesheets/ApproveTimesheets.aspx") %>' title='Approve Timesheets'>
													<span class="title">Approve Timesheets</span>
												</a>
                                 </li>
										 
											 <li>
								<a href='<%:ResolveClientUrl("~/WF/DC/Timesheets/TimesheetReport.aspx") %>' title='Timesheets Report'>
													<span class="title">Timesheet Report</span>
												</a>
                                 </li>
										 <li>
								<a href='<%:ResolveClientUrl("~/WF/DC/Timesheets/Payroll.aspx") %>' title='Payroll'>
													<span class="title">Payroll</span>
												</a>
                                 </li>
										
								 </ul>
                    </li>
					 <li id="link_expenses" runat="server">
						<a href="#" data-toggle="tooltip" title='<%= Resources.DeffinityRes.Expenses%>'>
							<i class="linecons-money"></i>
							<span class="title"><%= Resources.DeffinityRes.Expenses%></span>
						</a>
                      <ul>
							 <li>
								<a href='<%:ResolveClientUrl("~/WF/DC/Expenses/AddExpenses.aspx") %>' title='Add Expenses'>
													<span class="title">Add Expenses</span>
												</a>
                                 </li>
										  <li>
								<a href='<%:ResolveClientUrl("~/WF/DC/Expenses/ExpensesReport.aspx") %>' title='Expenses Report'>
													<span class="title"> Expenses Report</span>
												</a>
                                 </li>

										
                        </ul>
                    </li>
					
					 <li id="link_invoices" runat="server">
						<a href='<%: this.ResolveClientUrl("~/WF/DC/FRPApprovals.aspx") %>' data-toggle="tooltip" title='<%= Resources.DeffinityRes.Invoices%>'>
							<i class="fa-list"></i>
							<span class="title"><%= Resources.DeffinityRes.Invoices%></span>
						</a>
                    </li>
					
					 <li id="link_maintenancePlan" runat="server" style="display:none;visibility:hidden">
						<a href='<%: this.ResolveClientUrl("~/WF/CustomerAdmin/ContactMaintenanceSchedule.aspx") %>' data-toggle="tooltip" title='<%= Resources.DeffinityRes.MaintenancePlans%>'>
							<i class="fa-truck"></i>
							<span class="title"><%= Resources.DeffinityRes.MaintenancePlans%></span>
						</a>
                    </li>
					 <li style="display:none;visibility:hidden;">
						<a href='<%: this.ResolveClientUrl("~/WF/CustomerAdmin/CustomerEquimentListReport.aspx") %>' data-toggle="tooltip" title='<%= Resources.DeffinityRes.Equipment%>'>
							<i class="fa-cubes"></i>
							<span class="title"><%= Resources.DeffinityRes.Equipment%></span>
						</a>
                    </li>
					
					 <li id="link_docs" runat="server" >
						<a href='<%: this.ResolveClientUrl("~/WF/CustomerAdmin/PortfolioDocs.aspx") %>' data-toggle="tooltip" title='<%= Resources.DeffinityRes.Documents%>'>
							<i class="fa-folder-open-o"></i>
							<span class="title"><%= Resources.DeffinityRes.Documents%></span>
						</a>
                    </li>
					 <li runat="server" id="link_market">
						<a href='<%: this.ResolveClientUrl("~/WF/CustomerAdmin/Campaign/CampaignList.aspx") %>' data-toggle="tooltip" title='<%= Resources.DeffinityRes.Marketing%>'>
							<i class="fa-rocket"></i>
							<span class="title"><%= Resources.DeffinityRes.Marketing%></span>
						</a>
                    </li>
                      <li id="link_inventory" runat="server">
						<a href='<%: this.ResolveClientUrl("~/WF/CustomerAdmin/InventoryItemslist.aspx") %>' data-toggle="tooltip" title='<%= Resources.DeffinityRes.Inventory%>'>
							<i class="fa-archive"></i>
							<span class="title"><%= Resources.DeffinityRes.Inventory%></span>
						</a>
                    </li>
					  <li runat="server" id="link_forms">
						<a href='<%: this.ResolveClientUrl("~/WF/Health/HC/FormList.aspx") %>' data-toggle="tooltip" title='<%= Resources.DeffinityRes.Forms%>'>
							<i class="fa-edit"></i>
							<span class="title"><%= Resources.DeffinityRes.Forms%></span>
						</a>
                    </li>
					
                     <li id="link_reports" runat="server">
						<a href='#' data-toggle="tooltip" title='Reports'>
							<i class="fa-pie-chart "></i>
							<span class="title"><%= Resources.DeffinityRes.Reports%></span>
						</a>
                         <ul>
									<li>
										<a href='<%: this.ResolveClientUrl("~/WF/DC/FRPApprovals.aspx") %>'>
											<span class="title">Invoice</span>
										</a>
									</li>
							  <li >
						<a href='<%: this.ResolveClientUrl("~/WF/DC/QuoteList.aspx") %>' data-toggle="tooltip" title='<%= Resources.DeffinityRes.Quotations%>'>
							<span class="title"><%= Resources.DeffinityRes.Quotations%></span>
						</a>
                    </li>
                             <li>
										<a href='<%: this.ResolveClientUrl("~/WF/CustomerAdmin/MaintenanceScheduleForm.aspx") %>'>
											<span class="title">Maintenance Schedule</span>
										</a>
									</li>
                              <li>
										<a href='<%: this.ResolveClientUrl("~/WF/DC/FLSReport.aspx") %>'>
											<span class="title">Jobs Report</span>
										</a>
									</li>
                              <li>
										<a href='<%: this.ResolveClientUrl("~/WF/DC/SalesReport.aspx") %>'>
											<span class="title">Smart Tech Report</span>
										</a>
									</li>
                            <li  id="link_equipment" runat="server">
										<a href='<%: this.ResolveClientUrl("~/WF/CustomerAdmin/CustomerEquimentListReport.aspx") %>'>
											<span class="title">Equiment <%= Resources.DeffinityRes.Report%></span>
										</a>
									</li>
							  <li id="link_remainders" runat="server">
						<a href='<%: this.ResolveClientUrl("~/WF/CustomerAdmin/MaintenanceScheduleForm.aspx") %>' data-toggle="tooltip" title='<%= Resources.DeffinityRes.Reminders%>'>
							<%--<i class="fa-bell-o"></i>--%>
							<span class="title"><%= Resources.DeffinityRes.Reminders%></span>
						</a>
                    </li>
								</ul>
                    </li>
					   <li id="LinkFeedback" runat="server">
                        <a href='<%: this.ResolveClientUrl("~/WF/DC/DCFeedbackList.aspx") %>' data-toggle="tooltip" title='Feedback'>
							<i class="fa-comments-o"></i>
							<span class="title">Feedback</span>
						</a>
                    </li>
                    
                     <li runat="server" id="link_settings">
						<a href='<%: this.ResolveClientUrl("~/WF/DC/AdminSettings.aspx?tab=fls") %>' data-toggle="tooltip" title='<%= Resources.DeffinityRes.Settings%>'>
							<i class="fa-cogs"></i>
							<span class="title"><%= Resources.DeffinityRes.Settings%></span>
						</a>
                    </li>

					   <li runat="server" id="link_upgrade" visible="false">
						<a href='<%: this.ResolveClientUrl("~/WF/CustomerAdmin/UpgradePlanV2.aspx") %>' data-toggle="tooltip" title='Upgrade User Licences'>
							<i class="fa-cogs"></i>
							<span class="title">Upgrade User Licences</span>
						</a>
                    </li>

					 <li runat="server" id="li_premium" visible="false">
						<a href='<%: this.ResolveClientUrl("~/WF/CustomerAdmin/UpgradeModules.aspx") %>' data-toggle="tooltip" title='Upgrade to Premium '>
							<i class="fa-rocket"></i>
							<span class="title">Upgrade to Premium </span>
						</a>
                    </li>

					 <li runat="server" id="li_purchasetraining" visible="false" >
						<a href='<%: this.ResolveClientUrl("~/WF/CustomerAdmin/PurchaseTraining.aspx") %>' data-toggle="tooltip" title='Purchase Training'>
							<i class="fa-graduation-cap"></i>
							<span class="title">Purchase Training </span>
						</a>
                    </li>

                    
                
                     
                </ul>		
						
			</div>
		</div>

<%: System.Web.Optimization.Scripts.Render("~/bundles/sidemenu") %>