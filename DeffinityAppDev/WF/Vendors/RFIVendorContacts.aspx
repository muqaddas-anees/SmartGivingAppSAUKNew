<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true"
         Inherits="RFI_VendorContacts" Codebehind="RFIVendorContacts.aspx.cs" %>

<%@ Register src="controls/RFIVendorMainTabNew.ascx" tagname="RFIVendorTabs" tagprefix="uc1" %>
<%@ Register src="controls/RFIVendorContacts.ascx" tagname="RFIVendorContacts" tagprefix="uc2" %>
<%@ Register src="controls/VendorRef.ascx" tagname="VendorRef" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:RFIVendorTabs ID="RFIVendorTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    Supplier <uc2:VendorRef ID="VendorRef2" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     Key Contacts
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <div class="form-group row">
             <uc2:RFIVendorContacts ID="RFIVendorContacts1" runat="server" />
      </div>
      <div class="form-group row">
             <div class="col-md-6">
                   <asp:Button SkinID="btnDefault" runat="server" ID="imgbtnBack" Text="Back"
                                                                               onclick="imgbtnBack_Click" />
                <%--   <asp:LinkButton ID="l1" runat="server" SkinID="BtnLinkEdit"></asp:LinkButton>
                 <asp:LinkButton ID="LinkButton1" runat="server" SkinID="BtnLinkUpdate"></asp:LinkButton>
                 <asp:LinkButton ID="LinkButton2" runat="server" SkinID="BtnLinkCancel"></asp:LinkButton>--%>
             </div>
           <div class="col-md-6" style="float:right;text-align:right;display:none;">
                    <asp:Button ID="btnnext" runat="server" SkinID="btnDefault" Text="Next"
                                                        ToolTip="Next"  onclick="btnnext_Click" />
             </div>
      </div>
</asp:Content>


