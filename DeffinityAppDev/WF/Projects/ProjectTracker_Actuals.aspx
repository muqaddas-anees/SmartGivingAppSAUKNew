<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectTracker_Actuals" EnableEventValidation="false" Codebehind="ProjectTracker_Actuals.aspx.cs" MaintainScrollPositionOnPostback="true" %>
<%@ Register Src="controls/ProjectTabs.ascx" TagName="BuildProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/Project_FinancialSubtab.ascx" TagName="Project_FinalcialSubtab"
    TagPrefix="uc2" %>
    <%@ Register Src="controls/PTActuals.ascx" TagName="Actuals" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc1:BuildProjectTabs ID="BuildProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      Project Tracker - <Pref:ProjectRef ID="ProjectRef2" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

     <a href="pandldisplay.aspx?Project=<%=getProject%>" target="_blank" style="font-weight: bold;">
                        <%= Resources.DeffinityRes.ViewPandLaccount%>
                    </a>
                    <asp:LinkButton ID="btnPandL" runat="server" Text="<%$ Resources:DeffinityRes,ViewPandLaccount%>"
                        OnClick="btnPandL_Click" Font-Bold="True" Visible="false"></asp:LinkButton>
    <uc2:Project_FinalcialSubtab ID="Project_FinalcialSubtab1" runat="server" />
     <uc3:Actuals ID="Actuals1" runat="server"/>
     
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
    activeTab('Project Tracker');
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
    GridResponsiveCss();
</script> 
</asp:Content>


