<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="dtest.aspx.cs" Inherits="DeffinityAppDev.WF.Projects.dtest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="server">
    <asp:Label ID="dd" runat="server" Text=""><i class="fa fa-info"></i></asp:Label>
    <asp:Label ID="Label1" runat="server" Text=""><i class="fa fa-calendar"></i></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <div class="form-group no-right-padding">
        <div class="col-xs-6" runat="server" id="div_Sourceofrequest">
        <div class="lab col-sm-5 control-label" id="li_lblSourceOfRequest" runat="server">
            <asp:Label ID="lblSourceOfRequest" runat="server" Text="Source of Request"></asp:Label></div>
        <div class="col-sm-7" id="li_ddlSourceOfRequest" runat="server">
            <asp:DropDownList ID="ddlSourceOfRequest" runat="server"  SkinID="ddl_80" ClientIDMode="Static">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvSourceOfRequest" runat="server" ControlToValidate="ddlSourceOfRequest"
                Display="Dynamic" ErrorMessage="Please select source of request" InitialValue="0"
                SetFocusOnError="True" ValidationGroup="fls">*</asp:RequiredFieldValidator>
        </div>
            </div>
        <div class="col-xs-6" runat="server" id="div_Site">
        <div class="lab col-sm-5 control-label" id="li_lblSite" runat="server">
            <asp:Label ID="lblSite" runat="server" Text="Site"></asp:Label></div>
        <div class="col-sm-7" id="li_ddlSite" runat="server">
         
            <asp:DropDownList ID="ddlSite" runat="server" SkinID="ddl_80" ClientIDMode="Static">
            </asp:DropDownList>
           
            <asp:RequiredFieldValidator ID="rfvSite" runat="server" ControlToValidate="ddlSite"
                Display="Dynamic" ErrorMessage="Please select site" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="fls">*</asp:RequiredFieldValidator>
            <asp:HiddenField ID="htid" runat="server" Value="0" ClientIDMode="Static" />
            
        </div>
            </div>
        <div class="col-xs-6"  runat="server" id="div_Company">
        <div class="lab col-sm-5 control-label" id="li_lblCompany" runat="server">
            <asp:Label ID="lblCompany" runat="server" Text="Customer"></asp:Label></div>
        <div class="col-sm-7" id="li_ddlCompany" runat="server">
            <asp:DropDownList ID="ddlCompany" runat="server" CausesValidation="true" ClientIDMode="Static" AutoPostBack="true" SkinID="ddl_80"
               >
            </asp:DropDownList>
            <asp:HiddenField ID="hfCustomerID" runat="server" Value="0" />
          
            <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ControlToValidate="ddlCompany"
                Display="Dynamic" ErrorMessage="Please select customer" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="fls">*</asp:RequiredFieldValidator>
        </div>
            </div>
        <div class="col-xs-6" runat="server" id="div_Status">
        <div class="lab col-sm-5 control-label" id="li_lblStatus" runat="server">
            <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label></div>
        <div class="col-sm-7" id="li_ddlStatus" runat="server">
            <asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_80" ClientIDMode="Static">
            </asp:DropDownList>
           
            <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus"
                Display="Dynamic" ErrorMessage="Please select status" InitialValue="0" SetFocusOnError="True"
                ValidationGroup="fls">*</asp:RequiredFieldValidator>
            <asp:HiddenField ID="h_status" runat="server" Value="0" />
        </div>
            </div>
         </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
