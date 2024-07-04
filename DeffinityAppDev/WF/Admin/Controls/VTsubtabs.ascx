<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_VTsubtabs" Codebehind="VTsubtabs.ascx.cs" %>
<div class="form-wizard">
<ul class="tabs">
<li class="ms-hover"><asp:HyperLink ID="lbtnBooking"  target="_self" runat="server"   Text="<%$ Resources:DeffinityRes,Bookings%>"></asp:HyperLink></li>
<li class="ms-hover"><asp:HyperLink ID="lbtnAwaiting"  target="_self" runat="server"  Text="<%$ Resources:DeffinityRes,RequestsAwaitingApproval%>"></asp:HyperLink></li>
<li class="ms-hover"><asp:HyperLink ID="lbtnArchived"  target="_self" runat="server"  Text="<%$ Resources:DeffinityRes,Archived%>"></asp:HyperLink></li>

</ul>
</div>
<%: System.Web.Optimization.Scripts.Render("~/bundles/subtabs") %>
<script type="text/javascript">
    sideMenuActive('<%= Resources.DeffinityRes.Resources%>');
    activeTab('<%= Resources.DeffinityRes.VacationTracker%>');
</script>
