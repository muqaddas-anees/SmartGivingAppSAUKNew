<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="ManageChat.aspx.cs" Inherits="DeffinityAppDev.App.ManageChat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Manage Chat
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
     <div class="row mb-6">
         <asp:Label ID="lblMsg" runat="server" EnableViewState="false"></asp:Label>

         </div>
    <div class="row mb-6">
        <div class="">Organizations </div>
        <div> <asp:DropDownList ID="ddlOrg" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrg_SelectedIndexChanged"></asp:DropDownList> </div>
      
    </div>
      <div class="row mb-6"><asp:Label ID="lblorgdetails" runat="server" EnableViewState="false"></asp:Label> </div>
     <div class="row md-6">
        <asp:Button ID="btnCreateChannel" runat="server" OnClick="btnCreateChannel_Click" SkinID="btnDefault" Text="Create Channel"  Width="200"  />
    </div>
    <br />
    <br />
    <br />
    <div class="row mb-6">
        <div class="">Members </div>
        <div> <asp:DropDownList ID="ddlMember" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged"></asp:DropDownList> </div>
      
    </div>
      <div class="row mb-6"><asp:Label ID="lblMembersDetails" runat="server" EnableViewState="false"></asp:Label> </div>

     <div class="row md-6">
        <asp:Button ID="btnAddModarator" runat="server" OnClick="btnAddModarator_Click" SkinID="btnDefault" Text="Add Modarator" Width="200" /> &nbsp;&nbsp;
          <asp:Button ID="btnAddMember" runat="server" OnClick="btnAddMember_Click" SkinID="btnDefault" Text="Add Member"  Width="200"  />
    </div>
   

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
