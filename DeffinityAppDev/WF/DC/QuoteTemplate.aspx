<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="QuoteTemplate.aspx.cs" Inherits="DeffinityAppDev.WF.DC.QuoteTemplate" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
 <%@ Register Src="~/WF/DC/controls/TypeCtrl.ascx" TagPrefix="MailsendingPriority" TagName="TypeCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script src="../../Content/assets/js/ckeditor/ckeditor.js"></script>
    <script src="../../Content/assets/js/ckeditor/adapters/jquery.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    System Configuration
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     Quotation Templates  <MailsendingPriority:TypeCtrl runat="server" id="TypeCtrl" Visible="false" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
    <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/DC/FLSJlist.aspx?type=FLS"><i class="fa fa-arrow-left"></i> Return to Job list</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">


     <div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
               <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
              </div>
         </div>
     <div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Template Name</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlTitle" runat="server" OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
               <asp:TextBox ID="txtTitle" runat="server" MaxLength="255" Visible="false"></asp:TextBox>
            </div>
	</div>
	
	<div class="col-md-6 form-inline">
          <asp:Button ID="btnEdit" runat="server" SkinID="btnEdit" Visible="false" OnClick="btnEdit_Click" />
         <asp:Button ID="btnSave" runat="server" SkinID="btnSave" OnClick="btnSave_Click" />
        <asp:Button ID="btnCreate" runat="server" SkinID="btnDefault" Text="Create New Template" OnClick="btnCreate_Click" />
        <asp:LinkButton ID="btnDelete" runat="server" SkinID="BtnLinkDelete" OnClick="btnDelete_Click" OnClientClick="return confirm('Do you want to delete the record?');"></asp:LinkButton>
	</div>
       
</div>
    <div class="form-group row">
      <div class="col-md-8">
           <CKEditor:CKEditorControl ID="txtTemplate" BasePath="~/Scripts/ckeditor/" runat="server"
                          Width="800px" Height="400px" ClientIDMode="Static"  ></CKEditor:CKEditorControl>
        
             
          </div>
    </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
  <script>
      hidetabs();
  </script>
</asp:Content>
