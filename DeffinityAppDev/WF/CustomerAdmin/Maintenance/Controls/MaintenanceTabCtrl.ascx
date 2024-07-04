<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaintenanceTabCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Maintenance.Controls.MaintenanceTabCtrl" %>
<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation
						<i class="fa-bars"></i></span>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li><a href="<%=getUrl(6)%>"  title="Checklist">Checklist</a></li>
    <li><a href="<%=getUrl(0)%>"  title="Manufacturers">Manufacturers</a></li>
     <li ><a href="<%=getUrl(1)%>"  title="Materials" >Materials</a></li>
    <li><a href="<%=getUrl(2)%>"  title="Times Per Year" >Times Per Year</a></li>
    <li ><a href="<%=getUrl(3)%>"  title="Hourly Rate" >Hourly Rate</a></li>
     <li><a href="<%=getUrl(4)%>"  title="Travel Time Options" > Travel Time Options</a></li>
    <li ><a href="<%=getUrl(5)%>"  title="Standard Terms and Conditions" >Standard Terms and Conditions </a></li>
   
   
</ul>
  
     
</div>
<%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %>
<script type="text/javascript">
    //sideMenuActive('<%= Resources.DeffinityRes.Customer%>');
</script>
<script type="text/javascript">
    //var cu = $(location).attr('href').toLowerCase();
    //var ck = 'contactaddressdetailsbasic.aspx';
    debugger;
    //if (cu.indexOf($.trim(ck)) == -1) {
       
    //    activeTab('Address');
    //}
</script>

