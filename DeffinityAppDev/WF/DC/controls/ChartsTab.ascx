<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChartsTab.ascx.cs"
     Inherits="DeffinityAppDev.WF.Projects.Controls.ChartsTab" %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/subtabs") %>


<div class="form-wizard" style="padding-bottom:15px;">
    <ul class="tabs">
        <li class="ms-hover"><a href="ServiceDeskHomeCustomer.aspx" target="_self">Customer</a></li>
        <li class="ms-hover">
    <a href="ServiceDeskHome.aspx" target="_self">Engineer</a></li>
    <li class="ms-hover"><a href="ServiceDeskHomeCategoryCharts.aspx" target="_self">Category</a></li>
    <li class="ms-hover"><a href="ServiceDeskHomeSiteCharts.aspx" target="_self">Site</a></li>
    <li class="ms-hover"><a href="ServiceDeskHomeBillingCharts.aspx" target="_self">Billing</a></li>
</ul>
    </div>
<div>
<asp:Label ID="lblmsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
</div>


<script type="text/javascript">
    sideMenuActive('<%= Resources.DeffinityRes.ServiceDesk%>');
</script>