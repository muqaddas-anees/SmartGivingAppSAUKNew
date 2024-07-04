<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" Inherits="ServiceCatelogAdminFileUpload" Codebehind="ServiceCatelogAdminFileUpload.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div>
        <asp:FileUpload ID="FileUploadControl" runat="server"></asp:FileUpload>
        <br />
        
        <asp:ImageButton ID="btnUpload" runat="server" Text="Upload" onclick="btnUpload_Click" SkinID="ImgUpload" />
        <asp:ImageButton ID="btnCancel" runat="server" Text="Cancel"  SkinID="ImgCancel" />
        <br />
        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>   
    </div>
</asp:Content>

