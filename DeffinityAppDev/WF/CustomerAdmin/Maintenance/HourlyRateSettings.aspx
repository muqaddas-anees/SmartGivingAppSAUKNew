<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="HourlyRateSettings.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Maintenance.HourlyRateSettings" %>

<%@ Register Src="~/WF/CustomerAdmin/Maintenance/Controls/MaintenanceTabCtrl.ascx" TagPrefix="Pref" TagName="MaintenanceTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Maintenance Admin
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:MaintenanceTabCtrl runat="server" ID="MaintenanceTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Hourly Rate Settings
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/DC/AdminSettings.aspxx"><i class="fa fa-arrow-left"></i> Return to Settings</asp:HyperLink>

</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="col-md-10">
          <asp:Label id="lblmsg" runat="server" SkinID="GreenBackcolor"></asp:Label>
          </div>
         
     <div class="form-group row">
      <div class="col-md-10">
          <label class="col-sm-2 control-label"> Hourly Rate</label>
          <div class="col-sm-10 form-inline">
              <asp:TextBox ID="txtHourlyRate" runat="server" SkinID="Price_150px" MaxLength="10"></asp:TextBox>
              </div>
          </div>
         </div>
     <div class="form-group row">
      <div class="col-md-10">
          <label class="col-sm-2 control-label"></label>
          <div class="col-sm-10 form-inline">
              <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="btnSubmit" />
              </div>
          </div>
         </div>


</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
