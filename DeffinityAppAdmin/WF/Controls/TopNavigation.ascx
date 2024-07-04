<%@ Control Language="C#" AutoEventWireup="true" Inherits="WF_Controls_TopNavigation" Codebehind="TopNavigation.ascx.cs" %>
<!-- User Info, Notifications and Menu Bar -->
			<%--<ul class="nav nav-userinfo navbar-right">--%>
				<nav class="navbar user-info-navbar" role="navigation">
				<!-- Left links for user info navbar -->
				<ul class="user-info-menu left-links list-inline list-unstyled">
					<li class="hidden-sm hidden-xs" >
						<a href="#" data-toggle="sidebar">
							<i class="fa-bars"></i>
						</a>
					</li>
					
				</ul>
				
				
				<!-- Right links for user info navbar -->
				<ul class="user-info-menu right-links list-inline list-unstyled">
					<li class="dropdown user-profile">
						<a href="#" data-toggle="dropdown">
							<%--<img src='<%:ResolveClientUrl("~/Content/assets/images/user-4.png") %>' alt="user-image" class="img-circle img-inline userpic-32" width="28" />--%>
                            <%--<img id="imgUser" runat="server"  src="~/WF/Admin/ImageHandler.ashx?type=user&id=0" alt="user-image" class="img-circle img-inline userpic-32" width="28" />--%>
							<span>
                                <asp:Literal ID="lblUserName" runat="server"></asp:Literal>
								<i class="fa-angle-down"></i>
							</span>
						</a>
						
						<ul class="dropdown-menu user-profile-menu list-unstyled">
							
						
						
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
