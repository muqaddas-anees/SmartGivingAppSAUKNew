<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="TimesheetApproverMailAlert" Codebehind="TimesheetApproverMailAlert.aspx.cs" %>
<%@ Register src="controls/ResourcePlannerTabs.ascx" tagname="ResourcePlannerTabs" tagprefix="uc3" %>
<%@ Register src="controls/TimesheetSubTabs.ascx" tagname="TimesheetSubTabs" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc3:ResourcePlannerTabs ID="ResourcePlannerTabs2" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%> 
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     Timesheet Approver Email Alert
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
             <div class="col-md-12">
                 <asp:Label ID="lblmsg" runat="server" ForeColor="Green" EnableViewState="false"></asp:Label>
</div>
</div>
    <div class="form-group">
             <div class="col-md-12 form-inline">
                 <asp:CheckBox ID="chkEnable" runat="server" AutoPostBack="true" 
                            oncheckedchanged="chkEnable_CheckedChanged" Checked="true" /> <label> Enable receive mail from resources  </label>
</div>
</div>
    
</asp:Content>


