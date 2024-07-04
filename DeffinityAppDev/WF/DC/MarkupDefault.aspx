<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="MarkupDefault.aspx.cs" Inherits="DeffinityAppDev.WF.DC.MarkupDefault" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Set Default Mark-up Percentage
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Set Default Mark-up Percentage
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/CustomerAdmin/InventoryItemslist.aspx">
<i class="fa fa-arrow-left"></i>Return to Inventory</asp:HyperLink>

</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-group row mb-6">
          <div class="col-md-12">
              <asp:Label ID="lblMsg" runat="server" EnableViewState="false" SkinID="GreenBackcolor"></asp:Label>
              </div>
         </div>
    <div class="form-group row mb-6">
         
 <label class="col-sm-1 control-label">Mark-up</label>
           <div class="col-sm-5 form-inline">
                <asp:TextBox ID="txtMarkup" runat="server" MaxLength="5" SkinID="Price_150px" Text="0"></asp:TextBox>
               <br />
               <asp:Button ID="btnSave" runat="server" SkinID="btnSave" OnClick="btnSave_Click" />
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMarkup" ValidChars="0123456789."></ajaxToolkit:FilteredTextBoxExtender>
                 </div>
	
</div>
     
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
