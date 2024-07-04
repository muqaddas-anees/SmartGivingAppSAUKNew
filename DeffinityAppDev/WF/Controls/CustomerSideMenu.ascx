<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerSideMenu.ascx.cs" Inherits="DeffinityAppDev.WF.Controls.CustomerSideMenu" %>
<!-- Add "fixed" class to make the sidebar fixed always to the browser viewport. -->
		<!-- Adding class "toggle-others" will keep only one menu item open at a time. -->
		<!-- Adding class "collapsed" collapse sidebar root elements and show only icons. -->
		<div class="sidebar-menu toggle-others collapsed">
			
			<div class="sidebar-menu-inner">	
				
				<header class="logo-env">
					
					<!-- logo -->
					<div class="logo">
						<a id="link_logo" runat="server" href="~/WF/Portal/Home.aspx" class="logo-expanded">
							<img   alt="" runat="server" id="img_logo" />
						</a>
						
						<a id="link_small_logo" runat="server" href="~/WF/Portal/Home.aspx" class="logo-collapsed">
							<img alt=""  runat="server" id="img_logo_small" />
						</a>
					</div>
					
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
					
								
				</header>
				<ul id="main-menu" class="main-menu">
                    <li class="active opened active" runat="server" id="link_Projects" style="display:none;visibility:hidden;">
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/CustomerHome.aspx?customer=0") %>'">
							<i class="fa-tasks"></i>
							<span class="title"><%: Resources.DeffinityRes.MyProjects %></span>
						</a>
                    </li>
                     <li  runat="server" id="link_ServiceRequest">
                        
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/Home.aspx") %>'>
							<i class="fa-headphones"></i>
							<span class="title">Home</span>
						</a>
                    </li>
					 <li class="active">
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/Home.aspx") %>'">
							<i class="fa-wrench"></i>
							<span class="title"><%= Resources.DeffinityRes.ServiceDesk%></span>
						</a>
                    </li>
                     <li  runat="server" id="li1">
						<a id="linkProfileurl" runat="server" href='~/WF/Portal/CustomerWarrantyDocs.aspx'>
							<i class="fa-exchange"></i>
							<span class="title">Customer Profile</span>
						</a>
                    </li>
                    
                     <li  runat="server" id="link_ServiceCatalgues"  style="display:none;visibility:hidden;">
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/CustomerSC.aspx?customer=0") %>'>
							<i class="fa-exchange"></i>
							<span class="title">Browse Service Catalogue</span>
						</a>
                    </li>
                     <li runat="server" id="link_HealthChecks"  style="display:none;visibility:hidden;">
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/HealthCheckSchedule_CPortal.aspx?customer=0") %>'>
							<i class="fa-stethoscope"></i>
							<span class="title">Health Checks / Audits</span>
						</a>
                    </li>
                     <li  runat="server" id="link_Docs"  style="display:none;visibility:hidden;">
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/CustomerDocs.aspx?customer=0") %>'>
							<i class="fa-files-o"></i>
							<span class="title">Document Library</span>
						</a>
                    </li>
                     <li  runat="server" id="link_FlowChats"  style="display:none;visibility:hidden;">
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/FlowChartDownLoad.aspx?customer=0") %>'>
							<i class="fa-exchange"></i>
							<span class="title">Processes & Procedures</span>
						</a>
                    </li>
                     <li  runat="server" id="link_OrgCharts"  style="display:none;visibility:hidden;">
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/OrgnChartsDownLoad.aspx?customer=0") %>'>
							<i class="fa-files-o"></i>
							<span class="title">Organisation Charts</span>
						</a>
                    </li>
                     <li  runat="server" id="link_MyTasks"  style="display:none;visibility:hidden;">
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/CustomerMyTasks.aspx?customer=0") %>'>
							<i class="fa-tasks"></i>
							<span class="title">My Tasks</span>
						</a>
                    </li>
                     <li  runat="server" id="link_Timesheet"  style="display:none;visibility:hidden;">
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/CustomerTimesheet.aspx?customer=0") %>'>
							<i class="linecons-cog"></i>
							<span class="title">Timesheet Approval</span>
						</a>
                    </li>
                    <%-- <li id='link_ChangeControl'>
						<a href="dashboard-1.html">
							<i class="linecons-cog"></i>
							<span class="title">Change Control</span>
						</a>
                    </li>--%>
                   <%--  <li id='link_Units'>
						<a href="dashboard-1.html">
							<i class="linecons-cog"></i>
							<span class="title">Unit Consumption Dashboard</span>
						</a>
                    </li>--%>
                     <li  runat="server" id="link_delivery"  style="display:none;visibility:hidden;">
						<a href='<%: this.ResolveClientUrl("~/WF/DC/DCCustomerJlist.aspx?type=Delivery") %>'>
							<i class="linecons-truck"></i>
							<span class="title">Delivery</span>
						</a>
                    </li>
                     <li  runat="server" id="link_accesscontrol"  style="display:none;visibility:hidden;">
						<a href='<%: this.ResolveClientUrl("~/WF/DC/DCCustomerJlist.aspx?type=AccessControl") %>'>
							<i class="fa fa-key"></i>
							<span class="title">Access Control</span>
						</a>
                    </li>
                     <li  runat="server" id="link_permittowork"  style="display:none;visibility:hidden;">
						<a href='<%: this.ResolveClientUrl("~/WF/DC/DCCustomerJlist.aspx?type=PermittoWork") %>'>
							<i class="fa-sign-in"></i>
							<span class="title">Permit to Work</span>
						</a>
                    </li>
                     <%-- <li  runat="server" id="link_commondocuments">
						<a href="dashboard-1.html">
							<i class="fa-slideshare"></i>
							<span class="title">Common Documents</span>
						</a>
                    </li>--%>
                     
                </ul>		
			</div>
			
		</div>


<%: System.Web.Optimization.Scripts.Render("~/bundles/sidemenu") %>