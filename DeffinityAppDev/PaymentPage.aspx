<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentPage.aspx.cs" Inherits="DeffinityAppDev.PaymentPage" %>

<!DOCTYPE html>
<html>
<head>
    <title>Stripe Payment Page</title>
</head>
<body>
     <script src="https://js.stripe.com/v3/"></script>
    <form id="form1" runat="server">
        <div>
            <h2>Stripe Payment</h2>
            <asp:Label ID="lblCardNumber" runat="server" Text="Card Number:"></asp:Label>
            <asp:TextBox ID="txtCardNumber" runat="server" CssClass="form-control"></asp:TextBox><br />

            <asp:Label ID="lblExpiryMonth" runat="server" Text="Expiry Month (MM):"></asp:Label>
            <asp:TextBox ID="txtExpiryMonth" runat="server" CssClass="form-control"></asp:TextBox><br />

            <asp:Label ID="lblExpiryYear" runat="server" Text="Expiry Year (YY):"></asp:Label>
            <asp:TextBox ID="txtExpiryYear" runat="server" CssClass="form-control"></asp:TextBox><br />

            <asp:Label ID="lblCVC" runat="server" Text="CVC:"></asp:Label>
            <asp:TextBox ID="txtCVC" runat="server" CssClass="form-control"></asp:TextBox><br />

            <asp:Label ID="lblAmount" runat="server" Text="Amount (USD):"></asp:Label>
            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox><br />

            <asp:Button ID="btnPay" runat="server" Text="Pay Now" OnClick="btnPay_Click" CssClass="btn btn-primary" />
            <asp:Label ID="lblMessage" runat="server" CssClass="form-control" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>