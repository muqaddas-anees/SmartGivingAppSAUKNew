<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="WMdetails.aspx.cs" Inherits="DeffinityAppDev.WF.WM.WMdetails" %>

<%@ Register Src="~/WF/WM/Controls/WSTabs.ascx" TagPrefix="Pref" TagName="WSTabs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Storage Location Management
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <%--<Pref:WSTabs runat="server" ID="WSTabs" />--%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Storage Location Details
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
    <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/CustomerAdmin/InventoryItemslist.aspx">
<i class="fa fa-arrow-left"></i>Return to Inventory</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-group row">
          <div class="col-md-12">
               <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="wv" style="display:none;" />
              <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
              <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
              <%--<asp:ValidationSummary ID="valsum" runat="server" ValidationGroup="sum" />--%>
              </div>
         </div>
    <div class="form-group row mb-6" id="pnlDDLWarehouse" runat="server">
         
 <label class="col-sm-2 control-label">Storage Location</label>
           <div class="col-sm-5">
               <asp:DropDownList ID="ddlWareshouse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWareshouse_SelectedIndexChanged"></asp:DropDownList>
              
            </div>
                <div class="col-sm-4 form-inline">
                    <asp:Button ID="btnAdd" runat="server" SkinID="btnAdd" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnEdit" runat="server" SkinID="btnEdit" OnClick="btnEdit_Click" Visible="false" />
                    <asp:LinkButton ID="btnDel" runat="server" SkinID="BtnLinkDelete" OnClick="btnDel_Click" OnClientClick="return confirm('Do you want to delete the storage location?');"></asp:LinkButton>
            </div>
	
</div>
     <div class="form-group row mb-6">
        
 <label class="col-sm-2 control-label">Storage Location Name</label>
           <div class="col-sm-5">
                <asp:TextBox ID="txtWareshouse" runat="server" MaxLength="250" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtWareshouse"
                                        ErrorMessage="Please enter storage location name" Display="None" ValidationGroup="wv"></asp:RequiredFieldValidator>
               <asp:Button ID="btnCancelAdd" runat="server" CausesValidation="false" OnClick="btnCancelAdd_Click" SkinID="btnCancel" Visible="false" />
            </div>
	
</div>
     <div class="form-group row mb-6">
         
 <label class="col-sm-2 control-label">Address 1</label>
           <div class="col-sm-5">
               <asp:TextBox ID="txtAddress1" runat="server" MaxLength="500"></asp:TextBox>
               <%-- <asp:RequiredFieldValidator ID="rv_address" runat="server" ControlToValidate="txtAddress1"
                                        ErrorMessage="Please enter address" Display="None" ValidationGroup="wv"></asp:RequiredFieldValidator>--%>
            </div>
	
</div>
     <div class="form-group row mb-6">
         
 <label class="col-sm-2 control-label">Address 2</label>
           <div class="col-sm-5">
               <asp:TextBox ID="txtAddress2" runat="server"  MaxLength="500"></asp:TextBox>
            </div>
	
</div>
     <div class="form-group row mb-6">
          
 <label class="col-sm-2 control-label">City</label>
           <div class="col-sm-5">
               <asp:TextBox ID="txtcity" runat="server" MaxLength="100"  ></asp:TextBox>
            </div>
	
</div>
     <div class="form-group row mb-6">
          
 <label class="col-sm-2 control-label">Town</label>
           <div class="col-sm-5">
                <asp:TextBox ID="txtTown" runat="server" MaxLength="100"  ></asp:TextBox>
            </div>
	
</div>
     <div class="form-group row mb-6">
          
 <label class="col-sm-2 control-label">Postcode</label>
           <div class="col-sm-5">
                <asp:TextBox ID="txtPostcode" runat="server" MaxLength="20"  ></asp:TextBox>
            </div>
	
</div>
     <div class="form-group row mb-6">
         
 <label class="col-sm-2 control-label">Storage Location Manager</label>
           <div class="col-sm-5">
               <asp:DropDownList ID="ddlWareshouseManager" runat="server"></asp:DropDownList>
            </div>
	
</div>
     <div class="form-group row mb-6">
          
 <label class="col-sm-2 control-label">Email Address</label>
           <div class="col-sm-5">
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="250"></asp:TextBox>
             <%--  <asp:RequiredFieldValidator ID="rv_email" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="Please enter  email" Display="None" ValidationGroup="wv"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="validmail" runat="server" ControlToValidate="txtEmail"
                                        Display="None" ErrorMessage=" Please enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="wv"></asp:RegularExpressionValidator>--%>
           
	</div>
</div>
     <div class="form-group row mb-6">
         
 <label class="col-sm-2 control-label">Mobile</label>
           <div class="col-sm-5">
                <asp:TextBox ID="txtMobile" runat="server" MaxLength="20"></asp:TextBox>
            </div>
	
</div>
    <div class="form-group row mb-6">
          <div class="col-md-12">
 <label class="col-sm-2 control-label"></label>
           <div class="col-sm-5">
               <asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" OnClick="btnSubmit_Click" ValidationGroup="wv" />
               <asp:Button ID="btnCancel" runat="server" SkinID="btnCancel" OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
	</div>
</div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    <script type="text/javascript">
        hidetabs();
    </script>
</asp:Content>
