<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="AutoComplete.aspx.cs" Inherits="DeffinityAppDev.App.AutoComplete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Auto complete
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
   
 <style type="text/css">
   /*AutoComplete flyout */
.completionList {
border:solid 1px #444444;
margin:0px;
padding:2px;
height: 100px;
overflow:auto;
background-color: #FFFFFF;
}

.listItem {
color: #1C1C1C;
}

.itemHighlighted {
background-color: #ffc0c0;
}
   </style>
    <div class="row">
        <div class="col-lg-12">
            Search text
            <asp:TextBox ID="txtContactsSearch" runat="server"></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender ID="txtContactsSearch_AutoCompleteExtender" MinimumPrefixLength="1" ServiceMethod="SearchCustomers" CompletionInterval="100" EnableCaching="false"
  CompletionSetCount="10" TargetControlID="txtContactsSearch" runat="server" FirstRowSelected="false" ServicePath="autocomplete.asmx" CompletionListCssClass="completionList"
     CompletionListHighlightedItemCssClass="itemHighlighted"
     CompletionListItemCssClass="listItem" >
  </ajaxToolkit:AutoCompleteExtender>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
