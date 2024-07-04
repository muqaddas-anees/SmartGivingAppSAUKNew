<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserTypesInUserList.ascx.cs" Inherits="DeffinityAppDev.WF.Projects.Controls.UserCntlInUserList" %>
<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
     <ul class="nav navbar-nav">
             <li>
                  <a href="Userswithrole.aspx">All Members</a> 
             </li>
             <li>
                 <a href="Userswithrole.aspx?Type=Administrators">Administrators</a>
             </li>
             <li>
                 <a href="Userswithrole.aspx?Type=Resource">Resources</a>
             </li>
             <li>
                 <a href="Userswithrole.aspx?Type=CasualLabour">Casual Labour</a>
             </li>
        </ul>
 </div>