<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="TagsTest.aspx.cs" Inherits="DeffinityAppDev.App.TagsTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
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
    <link href="../assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css"/>
<script src="../assets/plugins/global/plugins.bundle.js"></script>

    <div class="mb-10">
    <label class="form-label">Default input style</label>
    <input class="form-control" value="tag1, tag2, tag3" id="kt_tagify_1"/>
</div>

<div class="mb-0">
    <label class="form-label">Solid background style</label>
    <input class="form-control form-control-solid" value="tag1, tag2, tag3" id="kt_tagify_2"/>
</div>

    <script>
        // The DOM elements you wish to replace with Tagify
        var input1 = document.querySelector("#kt_tagify_1");
        var input2 = document.querySelector("#kt_tagify_2");

        // Initialize Tagify components on the above inputs
        new Tagify(input1);
        new Tagify(input2);
    </script>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
