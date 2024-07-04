<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Default2" Codebehind="PasswordExpiry.aspx.cs" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    Admin
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      Password&nbsp;Expiry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="form-group row">
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Exp"
                    DisplayMode="BulletList" />
        </div>
      <div class="form-group row">
          <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Green"></asp:Label>
          </div>
    <div class="form-group row">
                                  <div class="col-md-5">
                                       <label class="col-sm-4 control-label"> Expiry Type</label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlExpiryType" runat="server" SkinID="ddl_80">
                    <asp:ListItem Value="0" Text="Please Select..."></asp:ListItem>
                    <asp:ListItem Value="Monthly" Text="Monthly"></asp:ListItem>
                    <asp:ListItem Value="Quarterly" Text="Quarterly"></asp:ListItem>
                    <asp:ListItem Value="Not Applicable" Text="Not Applicable"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlExpiryType"
                    Display="Dynamic" ErrorMessage="Please select expiry type" InitialValue="0" SetFocusOnError="True"
                    ValidationGroup="Exp">*</asp:RequiredFieldValidator>
					</div>
				</div>
 <div class="col-md-5">
                                       <label class="col-sm-4 control-label">  Expiry Date</label>
                                      <div class="col-sm-8 form-inline">
                                            <asp:TextBox ID="txtExpiryDate" runat="server" SkinID="Date"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFVExp" runat="server" ControlToValidate="txtExpiryDate"
                    Display="Dynamic" ErrorMessage="Please enter expiry date" SetFocusOnError="True"
                    ValidationGroup="Exp">*</asp:RequiredFieldValidator>
                     <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter valid date"
                    ControlToValidate="txtExpiryDate" ValidationGroup="Exp" Type="Date" Operator="DataTypeCheck"
                    Text="*" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                <asp:Label ID="imgExpiryDate" runat="server"  SkinID="Calender" />
                <ajaxToolkit:CalendarExtender ID="calExpDate" runat="server" CssClass="MyCalendar"
                     PopupButtonID="imgExpiryDate" TargetControlID="txtExpiryDate">
                </ajaxToolkit:CalendarExtender>
					</div>
				</div>
<div class="col-md-2">
     <asp:Button ID="btnsubmit" runat="server" SkinID="btnSubmit" ValidationGroup="Exp"
                    OnClick="btnsubmit_Click"  />
                                      
				</div>
</div>
  
</asp:Content>
