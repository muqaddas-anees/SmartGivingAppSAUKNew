<%@ Control Language="C#" ClassName="CustomerOrderTabs" %>

<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li ><a href="~/WF/Portal/CustomerSC.aspx" target="_self" runat="server">Services/Products</a></li>
    <li ><a href="~/WF/Portal/DCNewOrder.aspx" target="_self" runat="server">Details</a></li>
</ul>
     </div>
 <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %> 
<script type="text/javascript">
    sideMenuActive('Browse Service Catelogue');
</script>
  

