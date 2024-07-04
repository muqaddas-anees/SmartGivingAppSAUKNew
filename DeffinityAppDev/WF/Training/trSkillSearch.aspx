<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="trSkillSearch" Codebehind="trSkillSearch.aspx.cs" %>
<%@ Register Src="~/WF/Training/controls/SkillSearchCtrl.ascx" TagName="SkillSearch"
    TagPrefix="uc1" %>
<%@ Register Src="~/WF/Training/controls/SkillManagerSubTabCtrl.ascx" TagName="SkillManagerSubTab"
    TagPrefix="uc1" %>
    <%@ Register src="~/WF/Admin/controls/ResourcePlannerTabs.ascx" tagname="ResourcePlannerTabs" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
  <uc3:ResourcePlannerTabs ID="ResourcePlannerTabs1" runat="server" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Training%>
 </asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     <label id="lblTitle" runat="server"> Skills Search
                  </label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:SkillManagerSubTab ID="SkillManagerSubTab1" runat="server" />
              
                    <uc1:SkillSearch ID="ManageUserSkills1" runat="server" />
                 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script> 

</asp:Content>


