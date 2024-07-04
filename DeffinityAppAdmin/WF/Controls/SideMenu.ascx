<%@ Control Language="C#" AutoEventWireup="true" Inherits="WF_SideMenu" Codebehind="SideMenu.ascx.cs" %>
<!-- Add "fixed" class to make the sidebar fixed always to the browser viewport. -->
		<!-- Adding class "toggle-others" will keep only one menu item open at a time. -->
		<!-- Adding class "collapsed" collapse sidebar root elements and show only icons. -->

         <div class="sidebar-menu toggle-others collapsed">
			
			<div class="sidebar-menu-inner">	
        <header class="logo-env">
			<!-- logo -->
					<div class="logo">
						<a id="link_logo" runat="server" href="~/WF/Admin/Premium.aspx" class="logo-expanded">
							<img   alt="" runat="server" id="img_logo" />
						</a>
						<a id="link_small_logo" runat="server" href="~/WF/Admin/Premium.aspx" class="logo-collapsed">
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
					<%--<a href="#" data-toggle="user-info-menu">
							<i class="fa-bell-o"></i>
							<span class="badge badge-success">7</span>
						</a>
						<a href="#" data-toggle="mobile-menu">
							<i class="fa-bars"></i>
						</a>--%>
				</div>
				<!-- This will open the popup with user profile settings, you can use for any purpose, just be creative -->
					<div class="settings-icon" style="display:none;visibility:hidden;">
						<a href="#" data-toggle="settings-pane" data-animate="true">
							<i class="linecons-cog"></i>
						</a>
					</div>
			<%--</div>--%>
			</header>
               <%-- <div class="navbar-mobile-clear"></div>--%>
				<ul id="main-menu" class="main-menu">
                    
                  
					 <li id="Li3" runat="server">
						<a href='~/WF/Admin/Premium.aspx' data-toggle="tooltip" title='Premium' runat="server">
							<i class="fa-tasks"></i>
							<span class="title">Premium</span>
						</a>
                    </li>
					  <li id="Li1" runat="server">
						<a href='~/WF/Admin/Instances.aspx' data-toggle="tooltip" title='Instances and Users' runat="server">
							<i class="fa-tasks"></i>
							<span class="title">Instances and Users</span>
						</a>
                    </li>
					 <li id="Li2" runat="server">
						<a href='~/WF/Admin/Billing.aspx' data-toggle="tooltip" title='Billing' runat="server">
							<i class="fa-tasks"></i>
							<span class="title">Billing</span>
						</a>
                    </li>
					 <li id="Li4" runat="server">
						<a href='~/WF/Admin/OurInformation.aspx' data-toggle="tooltip" title='Our Information' runat="server">
							<i class="fa-tasks"></i>
							<span class="title">Our Information</span>
						</a>
                    </li>
					 <li id="Li5" runat="server">
						<a href='~/WF/Admin/AdminUsers.aspx' data-toggle="tooltip" title='123 Admin Users' runat="server">
							<i class="fa-tasks"></i>
							<span class="title">123 Admin Users</span>
						</a>
                    </li>
					 <li id="Li6" runat="server">
						<a href='~/WF/Admin/Categories.aspx' data-toggle="tooltip" title='Categories' runat="server">
							<i class="fa-tasks"></i>
							<span class="title">Categories</span>
						</a>
                    </li>
                    
                   
                </ul>		
						
			</div>
		</div>

<%: System.Web.Optimization.Scripts.Render("~/bundles/sidemenu") %>