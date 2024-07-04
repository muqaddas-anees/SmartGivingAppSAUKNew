<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MaintenancePlanTabCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Maintenance.Controls.MaintenancePlanTabCtrl" %>
<div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation
						<i class="fa-bars"></i></span>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li><a href="<%=getUrl(0)%>"  title="Equipment">Equipment</a></li>
    <li><a href="<%=getUrl(1)%>"  title="View Plan">View Plan</a></li>
     <li ><a href="<%=getUrl(2)%>"  title="Calender" >Calendar</a></li>
    <li><a href="<%=getUrl(3)%>"  title="Agreement" >Agreement</a></li>
   
   
   
</ul>
   <ul class="nav navbar-nav" style="float:right;">
    <li> <a id ="link_return" href="~/WF/CustomerAdmin/PortfolioContacts.aspx" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Back to Contact Address</a></li>

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

<%--
<div class="form-wizard">
    <ul class="tabs">
        <li class="ms-hover">
            <asp:HyperLink ID="link_Equipment"  Target="_self" runat="server" Text="Customer PO">Equipment</asp:HyperLink></li>
         <li class="ms-hover">
              <asp:HyperLink ID="link_Viewplan"  Target="_self" runat="server" Text="Internal PO">View Plan</asp:HyperLink>
             </li>
          <li class="ms-hover">
              <asp:HyperLink ID="link_Calender"  Target="_self" runat="server" Text="Internal PO">Calender</asp:HyperLink>
             </li>
          <li class="ms-hover">
              <asp:HyperLink ID="link_Agreement"  Target="_self" runat="server" Text="Internal PO">Agreement</asp:HyperLink>
             </li>
        </ul>
    </div>--%>