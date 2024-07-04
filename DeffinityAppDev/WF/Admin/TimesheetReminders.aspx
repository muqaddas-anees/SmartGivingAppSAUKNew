<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="TimesheetReminders" Codebehind="TimesheetReminders.aspx.cs" %>
<%@ Register Src="controls/ResourcePlannerTabs.ascx" TagName="ResourcePlannerTabs" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
        <uc3:ResourcePlannerTabs ID="ResourcePlannerTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Resources%> 
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
  
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      Timesheet Reminders
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
             <div class="col-md-12">
                  <asp:Label ID="Lblmsg" runat="server" ForeColor="Green" EnableViewState="false"></asp:Label>
</div>
</div>
    <div class="form-group">
             <div class="col-md-12 form-inline">
                  <asp:CheckBox ID="checkboxalert" runat="server" /><span>Switch on timesheet alerts</span>
</div>
</div>
    <div class="form-group">
             <div class="col-md-12 form-inline">
                 <span>Send reminders at&nbsp;&nbsp;&nbsp; </span><asp:TextBox ID="txttime" SkinID="Time" runat="server" MaxLength="5"></asp:TextBox>(HH:MM)
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txttime"
                               ErrorMessage="Please enter valid time" Text="*" ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$"
                                SetFocusOnError="true" />
</div>
</div>
    <div class="form-group">
             <div class="col-md-12">
                  <asp:CheckBox ID="CheckboxAvoid" runat="server" Checked="true" /><span>Avoid weekends</span>
</div>
</div>
    <div class="form-group">
             <div class="col-md-12">
                  <asp:Button ID="BtnSubmit" runat="server" Text="Submit" SkinID="btnSubmit" OnClick="BtnSubmit_Click"/>
                            
                                <asp:Button ID="BtnUpdate" runat="server" Text="Submit" SkinID="btnUpdate" OnClick="BtnUpdate_Click" />
</div>
</div>
   
</asp:Content>


