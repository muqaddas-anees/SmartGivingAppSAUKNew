<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectBudget" MaintainScrollPositionOnPostback="true" Codebehind="ProjectBudget.aspx.cs" %>
<%-- --%>
<%--<%@ Register Assembly="Evyatar.Web.Controls" Namespace="Evyatar.Web.Controls" TagPrefix="evy" %>--%>
<%@ Register src="controls/ProjectTabs.ascx" tagname="ProjectTabs" tagprefix="uc1" %>
<%@ Register Src="controls/ProjectCost.ascx" TagName="ProjectCost" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
 <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.PricingSchedule%> - <Pref:ProjectRef ID="ProjectRef2" runat="server" /> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="form-group">
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
          RepeatDirection="Horizontal" AutoPostBack="True" 
          onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" CssClass="rlist">
          <asp:ListItem Selected="True" Value="1">Project&nbsp;Budget</asp:ListItem>
           <asp:ListItem Value="5">Budget&nbsp;by&nbsp;Task</asp:ListItem>
          <asp:ListItem Value="2">Bill&nbsp;of&nbsp;Materials</asp:ListItem>         
        <%--  <asp:ListItem Value="4">Goods&nbsp;Receipt</asp:ListItem>--%>
           <asp:ListItem Value="3">Project&nbsp;Benefit&nbsp;Budget</asp:ListItem>
      </asp:RadioButtonList>
            
         </div>
    <div class="form-group"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>  
                    
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
          ValidationGroup="project" />
      </div>
    <div class="form-group">
     <uc2:ProjectCost ID="ProjectCost1" runat="server" />
        </div>
<style type="text/css">
  .fixHeader
  {
 font-weight:bold;  position:relative; 
 top:expression(this.parentNode.parentNode.parentNode.scrollTop-1);
   
  }
</style>
    <script type="text/javascript">
        activeTab('budget');
    </script>
</asp:Content>


