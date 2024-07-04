<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="InventoryCategoryAdmin.aspx.cs" EnableEventValidation="false" Inherits="DeffinityAppDev.WF.CustomerAdmin.InventoryCategoryAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     Inventory Category
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     Inventory Category
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
    <asp:HyperLink runat="Server" NavigateUrl="~/WF/CustomerAdmin/InventoryItemslist.aspx">
<i class="fa fa-arrow-left"></i>Return to Inventory</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-group row">
         <div class="col-md-12">
                <asp:Label ID="lblmsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
             <asp:ValidationSummary ID="vcat_e" runat="server" ValidationGroup="cat_e" />
             <asp:ValidationSummary ID="vsubcat_e" runat="server" ValidationGroup="subcat_e" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCategory"
                        Display="None" ErrorMessage="Please select category" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="cat_e"></asp:RequiredFieldValidator>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSubCategory"
                        Display="None" ErrorMessage="Please select sub category" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="subcat_e"></asp:RequiredFieldValidator>
               </div>
        </div>
    <div class="form-group row">
      <div class="col-md-7">
           <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.Category%></label>
           <div class="col-sm-6">
               <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
               <asp:TextBox ID="txtCategory" runat="server" Visible="false" Text=""></asp:TextBox>
               <ajaxToolkit:CascadingDropDown ID="ccdCategory" runat="server" TargetControlID="ddlCategory"
                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetInventoryCategory" LoadingText="[Loading ...]" />
            </div>
           <div class="col-sm-4 form-inline">
               <asp:HiddenField ID="hcid" runat="server" Value="0" />
               <asp:LinkButton ID="btnEditCategory" runat="server" SkinID="BtnLinkEdit" OnClick="btnEditCategory_Click" ValidationGroup="cat_e" />
               <asp:LinkButton ID="btnAddCategory" runat="server" SkinID="BtnLinkAdd" OnClick="btnAddCategory_Click" />
               <asp:LinkButton ID="btnDelCategory" runat="server" SkinID="BtnLinkDelete" OnClick="btnDelCategory_Click"  ValidationGroup="cat_e" OnClientClick="return confirm('Do you want to delete the record?');" />
               <asp:Button ID="btnSubmitCategory" runat="server" SkinID="btnSubmit" Visible="false" OnClick="btnSubmitCategory_Click" />
               <asp:Button ID="btnCancelCategory" runat="server" SkinID="btnCancel" Visible="false" OnClick="btnCancelCategory_Click" />
               </div>
	</div>
	<div class="col-md-6">
          
	</div>
</div>
     <div class="form-group row">
      <div class="col-md-7">
           <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.SubCategory%></label>
           <div class="col-sm-6">
               <asp:DropDownList ID="ddlSubCategory" runat="server"></asp:DropDownList>
               <asp:TextBox ID="txtSubCategory" runat="server" Visible="false" Text=""></asp:TextBox>
                <ajaxToolkit:CascadingDropDown ID="ccdSubCategory" runat="server" TargetControlID="ddlSubCategory"
                        Category="Name" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                        ServiceMethod="GetInventorySubCategory" LoadingText="[Loading...]" ParentControlID="ddlCategory" />
            </div>
           <div class="col-sm-4 form-inline">
               <asp:HiddenField ID="hsid" runat="server" Value="0" />
               <asp:LinkButton ID="btnEditSubCategory" runat="server" SkinID="BtnLinkEdit" OnClick="btnEditSubCategory_Click" ValidationGroup="subcat_e" />
               <asp:LinkButton ID="btnAddSubCategory" runat="server" SkinID="BtnLinkAdd" OnClick="btnAddSubCategory_Click" />
               <asp:LinkButton ID="btnDelSubCategory" runat="server" SkinID="BtnLinkDelete" OnClick="btnDelSubCategory_Click"  ValidationGroup="subcat_e" OnClientClick="return confirm('Do you want to delete the record?');" />
                <asp:Button ID="btnSubmitSubcategory" runat="server" SkinID="btnSubmit" Visible="false" OnClick="btnSubmitSubcategory_Click" />
               <asp:Button ID="btnCancelSubcategory" runat="server" SkinID="btnCancel" Visible="false" OnClick="btnCancelSubcategory_Click" />
               </div>
	</div>
	<div class="col-md-6">
          
	</div>
</div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script>
        hidetabs();
    </script>
</asp:Content>
