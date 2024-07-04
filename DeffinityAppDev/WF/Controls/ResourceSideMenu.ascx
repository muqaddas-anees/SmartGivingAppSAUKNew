<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResourceSideMenu.ascx.cs" Inherits="DeffinityAppDev.WF.Controls.ResourceSideMenu" %>
<!-- Add "fixed" class to make the sidebar fixed always to the browser viewport. -->
		<!-- Adding class "toggle-others" will keep only one menu item open at a time. -->
		<!-- Adding class "collapsed" collapse sidebar root elements and show only icons. -->

         <script type="text/javascript">
            <%-- $(document).ready(function () {
                 $.ajax({
                     type: 'POST',
                     dataType: 'json',
                     contentType: 'application/json',
                     url: '<%: this.ResolveClientUrl("~/WF/Projects/ProjectHome.aspx/CreatedSmartApps") %>',
                     data: "{}",
                     success: function (data) {
                         var datatable1 = [];
                         var Newdt = jQuery.parseJSON(data.d);
                         //debugger;

                         var x = "<li><a href='<%: this.ResolveClientUrl("~/WF/SmartApp/AppManager.aspx") %>' title='Smart Apps'>";
                         x = x + "<i class='fa-tachometer'></i><span class='title'>Create Smart App</span></a></li>";

                         for (var i = 0; i < Newdt.length; i++) {

                             var AppId = Newdt[i].ID;
                             var Name = Newdt[i].Name;
                             var Css = Newdt[i].Css;
                             var AppUrl = "/WF/SmartApp/AppFormList.aspx?Appid=" + AppId;

                             x = x + "<li><a href='" + AppUrl + "' title='Smart Apps'>";
                             x = x + "<i class='" + Css + "'></i><span class='title'>" + Name + "</span></a></li>";
                         }
                         $("#CreatedSmartApps").html("");
                         $("#CreatedSmartApps").append(x);
                     }
                 });
             });--%>
         </script>
        
			<!-- logo -->
				<%--	<div class="navbar-brand">
						<a id="link_logo" runat="server" href="~/WF/DC/FLSJlist.aspx?type=FLS" class="logo">
							<img   alt="" runat="server" id="img_logo" />
						</a>
						<a id="link_small_logo" runat="server" href="~/WF/DC/FLSJlist.aspx?type=FLS" class="logo-collapsed" style="visibility:hidden;display:none;">
							<img alt=""  runat="server" id="img_logo_small" />
						</a>
                        <a href="#" data-toggle="settings-pane" data-animate="true">
					<i class="linecons-cog"></i>
				</a>
					</div>
					<!-- Mobile Toggles Links -->
			<div class="nav navbar-mobile">
			
				<!-- This will toggle the mobile menu and will be visible only on mobile devices -->
				<div class="mobile-menu-toggle">
					<!-- This will open the popup with user profile settings, you can use for any purpose, just be creative -->
					<a href="#" data-toggle="settings-pane" data-animate="true">
						<i class="linecons-cog"></i>
					</a>
					
					<a href="#" data-toggle="user-info-menu-horizontal">
						<i class="fa-bell-o"></i>
						<span class="badge badge-success">7</span>
					</a>
					
					<!-- data-toggle="mobile-menu-horizontal" will show horizontal menu links only -->
					<!-- data-toggle="mobile-menu" will show sidebar menu links only -->
					<!-- data-toggle="mobile-menu-both" will show sidebar and horizontal menu links -->
					<a href="#" data-toggle="mobile-menu-horizontal">
						<i class="fa-bars"></i>
					</a>
				</div>
				
			</div>
                <div class="navbar-mobile-clear"></div>--%>
						
						
			<%--</div>
		</div>--%>

<%--<%: System.Web.Optimization.Scripts.Render("~/bundles/sidemenu") %>--%>
<!-- Add "fixed" class to make the sidebar fixed always to the browser viewport. -->
		<!-- Adding class "toggle-others" will keep only one menu item open at a time. -->
		<!-- Adding class "collapsed" collapse sidebar root elements and show only icons. -->
		<div class="sidebar-menu toggle-others collapsed">
			
			<div class="sidebar-menu-inner">	
				
				<header class="logo-env">
					
					<!-- logo -->
					<div class="logo">
						<a id="link_logo" runat="server" href="~/WF/DC/FLSJlist.aspx?type=FLS" class="logo-expanded">
							<img runat="server" id="img_logo" alt="" />
						</a>
						
						<a id="link_small_logo" runat="server" href="~/WF/DC/FLSJlist.aspx?type=FLS" class="logo-collapsed">
							<img runat="server" id="img_logo_small" alt="" />
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
					<%--<div class="settings-icon">
						<a href="#" data-toggle="settings-pane" data-animate="true">
							<i class="linecons-cog"></i>
						</a>
					</div>--%>
					
								
				</header>
				<ul id="main-menu" class="main-menu">
                    <li class="active opened active">
						<a href='<%: this.ResolveClientUrl("~/WF/DC/FLSResourceList.aspx?type=FLS") %>'">
							<i class="fa-wrench"></i>
							<span class="title"><%= Resources.DeffinityRes.ServiceDesk%></span>
						</a>
                    </li>
                    <%-- <li>
                        
						<a href='<%: this.ResolveClientUrl("~/WF/Resource/TimeSheetResourcesDaily.aspx") %>'>
							<i class="fa-headphones"></i>
							<span class="title"><%= Resources.DeffinityRes.Timesheet%></span>
						</a>
                    </li>
                     <li>
						<a href='<%: this.ResolveClientUrl("~/WF/Resource/VT.ResourceVacationRequest.aspx") %>'>
							<i class="fa-users"></i>
							<span class="title"><%= Resources.DeffinityRes.AnnualLeave%></span>
						</a>
                    </li>
                     <li>
						<a href='<%: this.ResolveClientUrl("~/WF/Resource/ResourceNewChitChat.aspx") %>'>
							<i class="fa-ioxhost"></i>
							<span class="title"><%= Resources.DeffinityRes.ChitChat%></span>
						</a>
                    </li>--%>
                    <%-- <li>
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/CustomerDocs.aspx?customer=0") %>'>
							<i class="fa-newspaper-o"></i>
							<span class="title">Document Library</span>
						</a>
                    </li>
                     <li>
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/FlowChartDownLoad.aspx?customer=0") %>'>
							<i class="fa-exchange"></i>
							<span class="title">Processes & Procedures</span>
						</a>
                    </li>
                     <li>
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/OrgnChartsDownLoad.aspx?customer=0") %>'>
							<i class="fa-files-o"></i>
							<span class="title">Organisation Charts</span>
						</a>
                    </li>
                     <li>
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/CustomerMyTasks.aspx?customer=0") %>'>
							<i class="fa-exclamation"></i> &nbsp;
							<span class="title">My Tasks</span>
						</a>
                    </li>
                     <li>
						<a href='<%: this.ResolveClientUrl("~/WF/Portal/CustomerTimesheet.aspx?customer=0") %>'>
							<i class="linecons-cog"></i>
							<span class="title">Timesheet Approval</span>
						</a>
                    </li>
                     <li>
						<a href='<%: this.ResolveClientUrl("~/WF/DC/DCCustomerJlist.aspx?type=Delivery") %>'>
							<i class="fa-sign-out"></i>
							<span class="title">Delivery</span>
						</a>
                    </li>
                     <li>
						<a href='<%: this.ResolveClientUrl("~/WF/DC/DCCustomerJlist.aspx?type=AccessControl") %>'>
							<i class="fa-sign-in"></i>
							<span class="title">Access Control</span>
						</a>
                    </li>
                     <li>
						<a href='<%: this.ResolveClientUrl("~/WF/DC/DCCustomerJlist.aspx?type=PermittoWork") %>'>
							<i class="linecons-cog"></i>
							<span class="title">Permit to Work</span>
						</a>
                    </li>--%>
                   
                     
                </ul>		
			</div>
			
		</div>


<%: System.Web.Optimization.Scripts.Render("~/bundles/sidemenu") %>