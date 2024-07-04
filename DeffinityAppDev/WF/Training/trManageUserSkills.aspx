<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="trManageUserSkills" Codebehind="trManageUserSkills.aspx.cs" %>

<%@ Register Src="controls/ManageUserSkillsCtrl.ascx" TagName="ManageUserSkills"
    TagPrefix="uc1" %>
<%@ Register Src="controls/SkillManagerSubTabCtrl.ascx" TagName="SkillManagerSubTab"
    TagPrefix="uc1" %>
      <%@ Register src="~/WF/Admin/controls/ResourcePlannerTabs.ascx" tagname="ResourcePlannerTabs" tagprefix="uc3" %>
<asp:Content ID="Content3" ContentPlaceHolderID="Tabs" Runat="Server">
  <uc3:ResourcePlannerTabs ID="ResourcePlannerTabs2" runat="server" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Training%>
 </asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <label id="lblTitle" runat="server"> Manage Users Skills
                  </label>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc1:SkillManagerSubTab ID="SkillManagerSubTab1" runat="server" />
     <uc1:ManageUserSkills ID="ManageUserSkills1" runat="server" />
   <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script> 
</asp:Content>

