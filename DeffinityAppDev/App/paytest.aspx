<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paytest.aspx.cs" Inherits="DeffinityAppDev.App.paytest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="hidden" id="txtsetup" name="setup" runat="server" value='{
  "split_payment" : {
    "merchant_id":10000105,
    "percentage":10,
    "min":100,
    "max":100000
  }
}'/>
            <asp:Button ID="btnPay" runat="server" Text="Pay Now" OnClick="btnPay_Click" />
        </div>
    </form>
</body>
</html>
