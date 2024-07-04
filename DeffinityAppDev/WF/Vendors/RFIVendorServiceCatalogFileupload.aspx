<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master"
     AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
         Inherits="RFIVendorServiceCatalogFileupload"
     Codebehind="RFIVendorServiceCatalogFileupload.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group row">
        <div class="col-md-12">
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>   
        </div>
    </div>
    <div class="form-group row">
        <div class="col-md-12">
            <div class="col-sm-3">
                <asp:FileUpload ID="FileUploadControl" runat="server"></asp:FileUpload>
            </div>
            <div class="col-sm-3 form-inline">
                 <asp:Button ID="btnUpload" runat="server" Text="Upload" onclick="btnUpload_Click" SkinID="btnUpload" />
                 <asp:Button ID="btnCancel" runat="server" Text="Cancel"  SkinID="btnCancel" />
            </div>
        </div>
    </div>
</asp:Content>

