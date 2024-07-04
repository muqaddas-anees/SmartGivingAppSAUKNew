<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Vendor_overview" Codebehind="RFIVendorOverview.aspx.cs" %>

<%@ Register src="controls/RFIVendorMainTabNew.ascx" tagname="RFIVendorTabsNew" tagprefix="ucNew1" %>
<%@ Register src="controls/VendorRef.ascx" tagname="VendorRef" tagprefix="uc2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    Supplier Management -  <uc2:VendorRef ID="VendorRef2" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    Supplier Account Information
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
 <ucNew1:RFIVendorTabsNew ID="RFIVendorTabs1" runat="server" />   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
          <div class="col-md-12">
              <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    <asp:ValidationSummary id="ValidationSummary1" runat="server" ValidationGroup="Submit" DisplayMode="List" ForeColor="Red"></asp:ValidationSummary>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-4">
           <label class="col-sm-4 control-label">Supplier Name</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtUserName" Width="100%" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="ReqtxtUser" runat="server" Display="None" ErrorMessage="Please enter supplier name" ValidationGroup="Submit" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
</div>
    <div class="form-group row" style="display:none;visibility:hidden">
      <div class="col-md-4">
           <label class="col-sm-4 control-label">Login Name</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox>
    <%--<asp:RequiredFieldValidator ControlToValidate="txtLoginName" ID="ReqtxtLogin" runat="server" Display="None" ErrorMessage="Please enter login name" ValidationGroup="Submit" ></asp:RequiredFieldValidator>--%>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label">Password</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
    <%--<asp:RequiredFieldValidator ControlToValidate="txtPassword" ID="Reqtxtpwd" runat="server" Display="None" ErrorMessage="Please enter Password" ValidationGroup="Submit" ></asp:RequiredFieldValidator>--%>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label">Email</label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtEmailAddress" runat="server" SkinID="txtEmail"></asp:TextBox>
    <%--<asp:RequiredFieldValidator ControlToValidate="txtEmailAddress" ID="ReqtxtEmail" runat="server" Display="None" ErrorMessage="Please enter email" ValidationGroup="Submit" ></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator runat="server" ID="regtxtEmail" Display="None" 
            ControlToValidate="txtEmailAddress" 
            ErrorMessage="Please enter email in Correct Format" ValidationGroup="Submit" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ></asp:RegularExpressionValidator>--%>
            </div>
	</div>
</div>
    <div class="form-group row" style="display:none;visibility:hidden">
      <div class="col-md-4">
           <label class="col-sm-4 control-label">Registration Number</label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtRegNo" runat="server"></asp:TextBox>
    <%--<asp:RequiredFieldValidator ControlToValidate="txtRegNo" ID="ReqtxtRegNo" runat="server" Display="None" ErrorMessage="Please enter Reg. No." ValidationGroup="Submit" ></asp:RequiredFieldValidator>--%>

            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label">VAT Number</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtVATNumber" runat="server"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
</div>
    <div class="form-group row">
      <div class="col-md-4">
           <label class="col-sm-4 control-label">Address</label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
            </div>
	</div>
	
	<div class="col-md-4">
           <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">

            </div>
	</div>
</div>
    <div class="form-group row">
        <div class="col-md-4">
           <label class="col-sm-4 control-label">Zipcode</label>
           <div class="col-sm-8">
                <asp:TextBox ID="txtPostCode" runat="server"></asp:TextBox>
            </div>
	</div>

        </div>
    <div style="display:none;visibility:hidden;">
<div class="row">
          <div class="col-md-12">
 <strong>Trading History </strong> 
<hr class="no-top-margin" />
	</div>
</div>
    <div class="form-group row">
         <div class="col-md-3">
             
             </div>
      <div class="col-md-3 pull-right">
           <label id="lblLastyr1" runat="server" class="control-label">2016</label>
	</div>
	<div class="col-md-3  pull-right">
          <label id="lblLastyr2" runat="server" class="control-label">2015</label>
	</div>
	<div class="col-md-3 pull-right">
          <label id="lblLastyr3" runat="server" class="control-label">2014</label>
	</div>
</div>
 
    <div class="form-group row">
         <div class="col-md-3">
             Turnover:
             </div>
      <div class="col-md-3">
          <asp:TextBox ID="txtLastyr1" runat="server" SkinID="Price_150px" Text="0.00"></asp:TextBox>
  <asp:CompareValidator ID="cmptxtlastyr1" runat="server" ControlToValidate="txtLastyr1" Display="None"
                            ErrorMessage="Please enter valid Turnover" Operator="DataTypeCheck" Type="Currency" ValidationGroup="Submit"></asp:CompareValidator>
	</div>
	<div class="col-md-3">
          <asp:TextBox ID="txtLastyr2" runat="server" SkinID="Price_150px" Text="0.00"></asp:TextBox>
  <asp:CompareValidator ID="cmptxtlastyr2" runat="server" ControlToValidate="txtLastyr2" Display="None"
                            ErrorMessage="Please enter valid Turnover" Operator="DataTypeCheck" Type="Currency" ValidationGroup="Submit"></asp:CompareValidator>
	</div>
	<div class="col-md-3">
          <asp:TextBox ID="txtLastyr3" runat="server" SkinID="Price_150px" Text="0.00"></asp:TextBox>
  <asp:CompareValidator ID="cmptxtlastyr3" runat="server" ControlToValidate="txtLastyr3" Display="None"
                            ErrorMessage="Please enter valid Turnover" Operator="DataTypeCheck" Type="Currency" ValidationGroup="Submit"></asp:CompareValidator>
	</div>
</div>
    
    <div class="form-group row">
         <div class="col-md-3">
             Net Profit:
             </div>
      <div class="col-md-3">
           <asp:TextBox ID="txtNetProfit1" runat="server" SkinID="Price_150px" Text="0.00"></asp:TextBox>
  <asp:CompareValidator ID="cmptxtNetProfit1" runat="server" ControlToValidate="txtNetProfit1" Display="None"
                            ErrorMessage="Please enter valid Net Profit" Operator="DataTypeCheck" Type="Currency" ValidationGroup="Submit"></asp:CompareValidator>
	</div>
	<div class="col-md-3">
           <asp:TextBox ID="txtNetProfit2" runat="server" SkinID="Price_150px" Text="0.00"></asp:TextBox>
  <asp:CompareValidator ID="cmptxtNetProfit2" runat="server" ControlToValidate="txtNetProfit2" Display="None"
                            ErrorMessage="Please enter valid Net Profit" Operator="DataTypeCheck" Type="Currency" ValidationGroup="Submit"></asp:CompareValidator>
	</div>
	<div class="col-md-3">
          <asp:TextBox ID="txtNetProfit3" runat="server" SkinID="Price_150px" Text="0.00"></asp:TextBox>
  <asp:CompareValidator ID="cmptxtNetProfit3" runat="server" ControlToValidate="txtNetProfit3" Display="None"
                            ErrorMessage="Please enter valid Net Profit" Operator="DataTypeCheck" Type="Currency" ValidationGroup="Submit"></asp:CompareValidator>
	</div>
</div>
   
    <div class="form-group row">
         <div class="col-md-3">
              Permanent/Sub Contract Staff Ratio:
             </div>
      <div class="col-md-3">
          <asp:TextBox ID="txtStaffRatio1" runat="server" SkinID="Price_150px" Text="0.00"></asp:TextBox>
  <asp:CompareValidator ID="cmptxtSR1" runat="server" ControlToValidate="txtStaffRatio1" Display="None"
                            ErrorMessage="Please enter valid Ratio" Operator="DataTypeCheck" Type="String" ValidationGroup="Submit"></asp:CompareValidator>
	</div>
	<div class="col-md-3">
          <asp:TextBox ID="txtStaffRatio2" runat="server" SkinID="Price_150px" Text="0.00"></asp:TextBox>
  <asp:CompareValidator ID="cmptxtSR2" runat="server" ControlToValidate="txtStaffRatio2" Display="None"
                            ErrorMessage="Please enter valid Ratio" Operator="DataTypeCheck" Type="String" ValidationGroup="Submit"></asp:CompareValidator>
	</div>
	<div class="col-md-3">
           <asp:TextBox ID="txtStaffRatio3" runat="server" SkinID="Price_150px" Text="0.00"></asp:TextBox>
  <asp:CompareValidator ID="cmptxtSR3" runat="server" ControlToValidate="txtStaffRatio3" Display="None"
                            ErrorMessage="Please enter valid Ratio" Operator="DataTypeCheck" Type="String" ValidationGroup="Submit"></asp:CompareValidator>
	</div>
</div>
        </div>
    <div class="form-group row">
        <div class="col-md-4">
            <label class="col-sm-4 control-label">Specialist Skills / Key Information</label>
             <div class="col-sm-8">
                  <asp:TextBox ID="txtSkills" runat="server" Width="100%" TextMode="MultiLine" SkinID="txtMulti"></asp:TextBox>
            </div>
            </div>
        <div class="col-md-3">
            
            </div>
      <div class="col-md-3">
          
	</div>
	
</div>
    
<div class="form-group row">
           <div class="col-md-4">
                <label class="col-sm-4 control-label"></label>
                <div class="col-sm-8">
               <asp:Button ID="btnnext" runat="server" SkinID="btnSubmit" 
        ToolTip="Next" ValidationGroup="Submit" onclick="btnnext_Click" />
                    </div>
	</div>
</div>


     <asp:UpdatePanel runat="Server" Visible="false">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td style="vertical-align:middle;width:100px">
                                    BBBEE Rating
                                </td>
                                <td style="vertical-align:middle" >
                                    <asp:DropDownList ID="ddlBBBEERating" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBBBEERating_SelectedIndexChanged" Width="200px">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtBBBEERating" runat="server" Visible="false" Width="200px"></asp:TextBox>
                                </td>
                                <td style="vertical-align:middle;width:110px">
                                    Current Points
                                </td>
                                <td style="vertical-align:middle">
                                    <asp:TextBox ID="txtCurrentPoints" runat="server" Text="0"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
</asp:Content>


