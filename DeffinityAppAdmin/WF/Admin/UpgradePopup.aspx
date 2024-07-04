<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="UpgradePopup.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.UpgradePopup" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
<%@ Register Src="~/WF/Admin/Controls/AdminTabCtrl.ascx" TagPrefix="Pref" TagName="AdminTabCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     <%: Resources.DeffinityRes.Admin %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <Pref:AdminTabCtrl runat="server" ID="AdminTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Upgrade Pop up
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
      <div class="form-group">
         <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         </div>
    <div class="form-group">
      <div class="col-md-6 form-inline">

          <label>Partner:</label> <asp:DropDownList ID="ddlPartner" runat="server" SkinID="ddl_80" AutoPostBack="true" OnSelectedIndexChanged="ddlPartner_SelectedIndexChanged"></asp:DropDownList>

          </div>
         </div>
     <div class="form-group">
      <div class="col-md-10">
          <label class="col-sm-12 control-label"> </label>
          <div class="col-sm-12 form-inline">
               <CKEditor:CKEditorControl ID="CKEditor1" BasePath="~/Scripts/ckeditor/" runat="server"
                         Height="300px" ClientIDMode="Static"></CKEditor:CKEditorControl>

             
              </div>
          </div>
          </div>

     <div class="form-group">
      <div class="col-md-10">
          
          <div class="col-sm-10 form-inline">
           <asp:Button ID="btnSave" runat="server"  SkinID="btnSave" OnClick="btnSave_Click"/>
              </div>
          </div>
          </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
