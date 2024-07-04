<%@ Control Language="C#" AutoEventWireup="true" Inherits="Training_controls_SkillManagerSubTabCtrl" Codebehind="SkillManagerSubTabCtrl.ascx.cs" %>

<div class="form-wizard" style="padding-bottom:15px;">
    <ul class="tabs">
        <li class="ms-hover">
            <asp:HyperLink ID="lbtnSkillSearch"   NavigateUrl="~/WF/Training/trSkillSearch.aspx" Target="_self" runat="server" Text="Skills Search">Skills Search</asp:HyperLink>
        </li>
        <li class="ms-hover">
            <asp:HyperLink ID="lbtnDashboard" NavigateUrl="~/WF/Training/trManageSkills.aspx" Target="_self" runat="server" Text="Dashboard">Dashboard</asp:HyperLink>
        </li>
        <li  class="ms-hover">
            <asp:HyperLink ID="lbtnManageUserSkills" NavigateUrl="~/WF/Training/trManageUserSkills.aspx" Target="_self" runat="server" Text="Manage Users Skills">Manage Users Skills</asp:HyperLink>

        </li>
       
		</ul>
	
</div>
<%: System.Web.Optimization.Scripts.Render("~/bundles/subtabs") %>
<script type="text/javascript">
    var cu = $(location).attr('href').toLowerCase();
    var ck = 'trmanageskills.aspx';
    if (cu.indexOf($.trim(ck)) == -1) {
        activeTab('Skills Manager');
    }
    
</script>