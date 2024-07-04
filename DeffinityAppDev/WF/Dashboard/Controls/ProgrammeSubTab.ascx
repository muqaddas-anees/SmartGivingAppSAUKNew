<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ProgrammeSubTab" Codebehind="ProgrammeSubTab.ascx.cs" %>
<div class="row">
 <div class="form-wizard">
<ul id="countrytabs" class="tabs">
<li class="ms-hover"><asp:HyperLink ID="HyperLink7"  target="_self" runat="server">Dashboard Configurator </asp:HyperLink></li>
<li class="ms-hover"><asp:HyperLink ID="HyperLink8"  target="_self" runat="server"> Workstream </asp:HyperLink></li>
<li class="ms-hover"><asp:HyperLink ID="HyperLink1"  target="_self" runat="server"><%= Resources.DeffinityRes.ProjectSummary%> </asp:HyperLink></li>
<li class="ms-hover"><asp:HyperLink ID="HyperLink2" Target="_self" runat="server"><%= Resources.DeffinityRes.Reports%></asp:HyperLink> </li>
<li class="ms-hover"><asp:HyperLink ID="HyperLink3"  target="_self" runat="server"><%= Resources.DeffinityRes.CategoryView%></asp:HyperLink></li>
<li class="ms-hover"><asp:HyperLink ID="HyperLink4" Target="_self" runat="server"><%= Resources.DeffinityRes.RiskMatrix%></asp:HyperLink> </li>
<li class="ms-hover"><asp:HyperLink ID="HyperLink5" Target="_self" runat="server"><%= Resources.DeffinityRes.ProgramAssessment%> </asp:HyperLink> </li>
<li class="ms-hover"><asp:HyperLink ID="HyperLink6" Target="_self" runat="server"><%= Resources.DeffinityRes.Dependency%> </asp:HyperLink> </li>
</ul>
</div>
    </div>

<%: System.Web.Optimization.Scripts.Render("~/bundles/subtabs") %>

<script  type="text/javascript">
activeTab('Programme');
</script>