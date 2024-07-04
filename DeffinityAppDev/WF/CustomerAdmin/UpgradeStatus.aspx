<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="UpgradeStatus.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.UpgradeStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Upgrade
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Upgrade Status
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

    
       <div class="form-group row">
      <div class="col-md-12">
          <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>

            <asp:Label ID="lblError" runat="server" EnableViewState="false" SkinID="RedBackcolor"></asp:Label>
          </div>
              </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
