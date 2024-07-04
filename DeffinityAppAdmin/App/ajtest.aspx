<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajtest.aspx.cs" Inherits="DeffinityAppDev.App.ajtest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
     .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 300px;
        height: 140px;
    }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" />
 
<!-- ModalPopupExtender -->
<ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" style = "display:none">
    This is an ASP.Net AJAX ModalPopupExtender Example<br />
    <asp:Button ID="btnClose" runat="server" Text="Close" />
</asp:Panel>
    </form>
</body>
</html>
