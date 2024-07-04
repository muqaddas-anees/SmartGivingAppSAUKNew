<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="TestTouchSpin.aspx.cs" Inherits="DeffinityAppDev.App.TestTouchSpin" %>
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
   
    <div class="row">
        <input type="number" value="0" min="0" max="100" step="1" runat="server" id="t1" />
        <br />
         <input type="number" value="0" min="0" max="100" step="1" runat="server" id="t2" />
        <br />
        <asp:TextBox ID="t3" runat="server" type="number" value="0" min="0" max="100" step="1"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="demo2" class="col-md-4 control-label">Example:</label> <input id="demo2" type="text" value="0" name="demo2" class="col-md-8 form-control">
    </div>

     <script src="../Scripts/bootstrap-input-spinner.js" type="text/javascript"></script>
<script>
    $("input[type='number']").inputSpinner();
    //$("input[name='demo2']").TouchSpin({
    //    min: -1000000000,
    //    max: 1000000000,
    //    stepinterval: 50,
    //    maxboostedstep: 10000000,
    //    prefix: '$'
    //});
</script>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
