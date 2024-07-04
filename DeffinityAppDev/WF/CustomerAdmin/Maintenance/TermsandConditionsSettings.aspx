<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="TermsandConditionsSettings.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Maintenance.TermsandConditionsSettings" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
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
    Terms and Conditions
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
      <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/DC/AdminSettings.aspxx"><i class="fa fa-arrow-left"></i> Return to Settings</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
      <div class="form-group row">
                                  <div class="col-md-12">
                                      <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                                      </div>
          </div>
     <div class="form-group row">
                                  <div class="col-md-12">
                                     <%--  <label class="col-sm-12 control-label">Terms and Conditions</label>--%>
                                      <div class="col-sm-12 form-inline">
                                          <CKEditor:CKEditorControl ID="CKEditor1" BasePath="~/Scripts/ckeditor/" runat="server"
                         Height="400px" ClientIDMode="Static"></CKEditor:CKEditorControl>
                                           
                                          </div>
                                      </div>

          <div class="col-md-12">
                <div class="col-sm-12 form-inline">
                    <asp:Button ID="btnSave" runat="server" SkinID="btnSave" OnClick="btnSave_Click" />
                    </div>
              </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
