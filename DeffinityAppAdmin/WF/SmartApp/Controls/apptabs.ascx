<%@ Control Language="C#" AutoEventWireup="true" Inherits="App_Controls_apptabs" Codebehind="apptabs.ascx.cs" %>
 <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %>
<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li id="tab_app_list" runat="server" ><a href="<%=getUrl(0)%>"  title="Form"><label id="lblform" runat="server"></label></a></li>
    <li id="tab_app_search" runat="server"><a href="<%=getUrl(1)%>"  title="Search" >Search</a></li>
    <li class="dropdown" ><a href="#" data-toggle="dropdown" class="dropdown-toggle" ><%= Resources.DeffinityRes.Setup%><b class="caret"></b></a>
		<ul class="dropdown-menu dropdown-menu-right">
            
                <li runat="server"><a id="a1_link" href="<%=getUrl(2)%>">Permission Manager</a></li>
                 <li runat="server"><a href="<%=getUrl(3)%>">Columns Grid</a></li>
            </ul>
        </li>
</ul>
     </div>

<script type="text/javascript">
    sideMenuActive('Smart Apps');
</script>