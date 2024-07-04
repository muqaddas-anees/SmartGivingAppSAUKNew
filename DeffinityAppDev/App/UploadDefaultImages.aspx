<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" CodeBehind="UploadDefaultImages.aspx.cs" Inherits="DeffinityAppDev.App.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Upload Default Images
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            Upload Default Logo
        </div>
    </div>

    <div class="row mb-6">
        <asp:FileUpload ID="filelogo" runat="server" />
    </div>
    <div class="row">
        <div class="col-lg-12">
            Upload Default User Image
        </div>
    </div>

    <div class="row mb-6">
        <asp:FileUpload ID="fileUser" runat="server" />
    </div>

    <div class="row mb-6">
        <div class="col-lg-12">
            Upload Default Image

        </div>
    </div>
    <div class="row mb-6">
        <asp:FileUpload ID="fileimage" runat="server" />
    </div>
    <div class="row mb-6">
         <div class="col-lg-12">
        <asp:Button ID="btnUpload" runat="server" SkinID="btnUpload" OnClick="btnUpload_Click" />
             </div>
    </div>

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
