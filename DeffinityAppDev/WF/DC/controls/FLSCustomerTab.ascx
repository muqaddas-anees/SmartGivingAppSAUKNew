<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_FLSCustomerTab" Codebehind="FLSCustomerTab.ascx.cs" %>

<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation
						<i class="fa-bars"></i></span>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li id="tab1"><a href="<%=getUrl(0)%>"  title="Overview"><span>Overview</span> </a></li>
    <li id="tab2" style="display:none;visibility:hidden;"><a href="<%=getUrl(1)%>"  title="Chat" ><span>Chat</span></a></li>
   <%-- <li id="tab2" style="display:none;visibility:hidden;"><a href="<%=getUrl(1)%>"  title="Services" ><span>Services</span></a></li>--%>
   
</ul>
<asp:HiddenField ID="hidPortfolioID" runat="server" /><asp:HiddenField ID="hsdid" runat="server" Value="0"/></div>

<%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %>
<script type="text/javascript">
    //sideMenuActive('<%= Resources.DeffinityRes.ServiceDesk%>');
</script>