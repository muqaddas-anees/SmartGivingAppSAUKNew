<%@ Control Language="C#" AutoEventWireup="true" Inherits="WF_Controls_TopNavigation" Codebehind="TopNavigation.ascx.cs" %>
<!-- User Info, Notifications and Menu Bar -->
			<%--<ul class="nav nav-userinfo navbar-right">--%>
				<nav class="navbar user-info-navbar" role="navigation">
				<!-- Left links for user info navbar -->
				<ul class="user-info-menu left-links list-inline list-unstyled">
					<li class="hidden-sm hidden-xs">
						<a href="#" data-toggle="sidebar">
							<i class="fa-bars"></i>
						</a>
					</li>
					<li class="dropdown xs-left" runat="server" id="li1">
						<a href="<%:ResolveClientUrl("~/WF/DC/DashboardV2.aspx") %>" >
							<i class="fa-home"></i>
							<span class=""><%= Resources.DeffinityRes.Home%></span>
						</a>
					</li>
				
					<li class="dropdown xs-left" runat="server" id="link_DispatchBorad">
						<a href="<%:ResolveClientUrl("~/WF/DC/ResourceSchedular.aspx") %>" >
							<i class="fa-wrench"></i>
							<span class=""> <%= Resources.DeffinityRes.DispatchBoard%></span>
						</a>
							
						
					</li>
				
				<li class="dropdown xs-left" runat="server" id="link_newContact" >
						<a href="<%:ResolveClientUrl("~/WF/CustomerAdmin/ContactDetails.aspx") %>" >
							<i class="fa-group "></i>
							<span> <%= Resources.DeffinityRes.NewContact%></span>
						</a>
			</li>
						
			
				<li class="dropdown xs-left" runat="server">
						<a id="linknewjob" runat="server" href="~/WF/DC/FLSForm.aspx" >
							<i class="fa-wrench"></i>
							<span><%= Resources.DeffinityRes.LogNewServiceReq%></span>
						</a>
					</li>
						
				<li class="dropdown xs-left" runat="server" id="link_Dashboard">
						<a href="<%:ResolveClientUrl("~/WF/DC/Dashboard.aspx") %>" >
							<i class="fa-tachometer"></i>
							<span class=""><%= Resources.DeffinityRes.Dashboard%></span>
						</a>
					</li>
				</ul>
				
				
				<!-- Right links for user info navbar -->
				<ul class="user-info-menu right-links list-inline list-unstyled">
					<li class="dropdown user-profile" style="padding-top:5px;display:none;visibility:hidden;">
						<asp:Button ID="btnUpgrade" SkinID="btnDefault" runat="server" Text="View Premium Features" CssClass="btn btn-info" OnClick="btnUpgrade_Click" style="height:70px" />
						</li>
					<li class="dropdown user-profile">
						<a href="#" data-toggle="dropdown">
							<%--<img src='<%:ResolveClientUrl("~/Content/assets/images/user-4.png") %>' alt="user-image" class="img-circle img-inline userpic-32" width="28" />--%>
                            <img id="imgUser" runat="server"  src="~/WF/Admin/ImageHandler.ashx?type=user&id=0" alt="user-image" class="img-circle img-inline userpic-32" width="28" />
							<span>
                                <asp:Literal ID="lblUserName" runat="server"></asp:Literal>
								<i class="fa-angle-down"></i>
							</span>
						</a>
						
						<ul class="dropdown-menu user-profile-menu list-unstyled">
							
							<%--<li id="linkProfile" runat="server">
								<a href="#profile" id="linkProfileurl" runat="server">
									<i class="fa-user"></i>
									My Covered Properties
								</a>
							</li>--%>
						<%--	<li style="display:none;visibility:hidden;">
								<a href="#help">
									<i class="fa-info"></i>
									Help
								</a>
							</li>--%>
							 <li>
                                <a  href='<%:ResolveClientUrl("~/WF/CustomerAdmin/SubscriptionDetails.aspx") %>'>
                                    <i class="fa-info"></i>
                                    Subscription
                                </a>
                            </li>
                              <li>
                                <a id="link_ChangePwd" runat="server">
                                    <i class="fa-unlock-alt"></i>
                                    Change Password
                                </a>
                            </li>
							<li class="last">
								<a href='<%:ResolveClientUrl("~/WF/logout.aspx") %>'>
									<i class="fa-lock"></i>
									Logout
								</a>
							</li>
						</ul>
					</li>
					

					
				<%--</ul>--%>
				
			</ul>


					</nav>
<%--<% if(HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("projecthome.aspx") == false) { %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/angularjs") %>
   <script src="../../Scripts/api/ProjectApp.js"></script>
    <script src="../../Scripts/api/ProjectHomeController.js"></script>
   <script src="../../Scripts/api/InboxController.js"></script>
<% } %>
        <script type="text/javascript">

            $(document).ready(function () {
                $("body").attr("ng-app", "ProjectApp");
            });
</script>--%>