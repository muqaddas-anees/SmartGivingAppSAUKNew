<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_Checkpoint_tabs" Codebehind="Checkpoint_tabs.ascx.cs" %>

 <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %> 
<div>
<asp:Label ID="lblmsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
</div>
 <div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li><a href="<%=getUrl(0)%>" target="_self"><%= Resources.DeffinityRes.Overview%></a></li>
     <li><a href="<%=getUrl(14)%>" target="_self"><%= Resources.DeffinityRes.Checkpoint%></a></li>
    <li><a href="<%=getUrl(15)%>" target="_self"><%= Resources.DeffinityRes.Form%></a></li>
    <li><a href="<%=getUrl(6)%>" target="_self"><%= Resources.DeffinityRes.Feedback%></a></li>
    <li><a href="<%=getUrl(7)%>" target="_self"><%= Resources.DeffinityRes.CSI%></a></li>
    <li><a href="<%=getUrl(13)%>" target="_self"><%= Resources.DeffinityRes.Docs%></a></li>
    <li><a href="<%=getUrl(8)%>" target="_self"><%= Resources.DeffinityRes.Recommendations%></a></li>
</ul>
    </div>

<script type="text/javascript">
    sideMenuActive('<%= Resources.DeffinityRes.Projects%>');
</script>
 
 