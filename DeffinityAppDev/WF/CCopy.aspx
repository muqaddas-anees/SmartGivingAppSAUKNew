<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="CCopy.aspx.cs" Inherits="DeffinityAppDev.WF.CCopy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Data Copy
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-group row">
          <div class="col-md-12">
 <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">From Customer</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlFromCustomer" runat="server"></asp:DropDownList>
            </div>
	</div>
</div>

    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label">From Customer</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlToCustomer" runat="server"></asp:DropDownList>
            </div>
	</div>
</div>
    <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9 form-inline">
              <asp:Button ID="btnCopyUsers" runat="server" SkinID="btnDefault" Text="Copy Users" OnClick="btnCopyUsers_Click" />
               <asp:Button ID="btnCopyCRM" runat="server" SkinID="btnDefault" Text="Copy CRM Data" OnClick="btnCopyCRM_Click" />
            </div>
	</div>
</div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
