<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Paytest.aspx.cs" Inherits="DeffinityAppDev.Paytest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnPay" runat="server" Text="Pay Now" OnClick="btnPay_Click" />
             <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
        </div>
    </form>
</body>
</html>
