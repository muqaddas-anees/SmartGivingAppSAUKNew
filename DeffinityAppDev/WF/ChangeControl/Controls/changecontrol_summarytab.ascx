<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_changecontrol_summarytab" Codebehind="changecontrol_summarytab.ascx.cs" %>
 <div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
<li><a  target="_self" runat="server" id="link2"><%= Resources.DeffinityRes.Dashboard%></a></li>
    <li><a  target="_self" runat="server" id="link1">Change Control Management</a></li>
    <li class="dropdown"><a href="#" data-toggle="dropdown" class="dropdown-toggle" ><span><%= Resources.DeffinityRes.Setup%> &nbsp;</span><b class="caret"></b></a>
		<ul class="dropdown-menu dropdown-menu-right">
            <li><a href="ChangeControlAdmin.aspx"><%= Resources.DeffinityRes.AdminDropdown%></a></li>
            </ul>
        </li>
  </ul>
     </div>


 <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %> 
<script type="text/javascript">
    sideMenuActive('<%= Resources.DeffinityRes.ChangeControl%>');
</script>
