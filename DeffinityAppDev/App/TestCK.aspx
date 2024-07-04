<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="TestCK.aspx.cs" Inherits="DeffinityAppDev.App.TestCK" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    	<CKEditor:CKEditorControl ID="txtDescriptionArea" BasePath="~/Scripts/ckeditor_4.20.1/" Skin="moono-lisa" runat="server"
                         Height="500px" ClientIDMode="Static" ></CKEditor:CKEditorControl>


    <asp:Button ID="btnsubmit" runat="server" SkinID="btnSubmit" OnClick="btnsubmit_Click" />
    <br />
    <asp:Literal ID="lbl" runat="server"></asp:Literal>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
