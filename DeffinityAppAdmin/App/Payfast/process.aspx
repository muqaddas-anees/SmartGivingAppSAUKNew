<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="process.aspx.cs" Inherits="DeffinityAppDev.App.Payfast.process" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" action="https://sandbox.payfast.co.za​/eng/process">
        <div>
            <input type="hidden" name="merchant_id" value="10028239"/>
   <input type="hidden" name="merchant_key" value="e6y8289zjke4t"/>
            <input type="hidden" name="return_url" value="http://localhost:51382/App/Payfast/success.aspx"/>
<input type="hidden" name="cancel_url" value="http://localhost:51382/App/Payfast/cancel.aspx"/>
<input type="hidden" name="notify_url" value="http://localhost:51382/App/Payfast/notify.aspx"/>
   <input type="hidden" name="amount" value="100.00"/>
   <input type="hidden" name="item_name" value="Test Product"/>
   <input type="submit"/>
        </div>
    </form>
</body>
</html>
