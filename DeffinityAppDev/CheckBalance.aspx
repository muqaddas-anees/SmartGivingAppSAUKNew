<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckBalance.aspx.cs" Inherits="DeffinityAppDev.CheckBalance" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <div>
            <h2>Stripe Balance</h2>
            <asp:Label ID="lblAvailableBalance" runat="server" Text="Available Balance: "></asp:Label>
            <br />
            <asp:Label ID="lblPendingBalance" runat="server" Text="Pending Balance: "></asp:Label>
        </div>
    </form>
</body>
</html>
