<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentForm.aspx.cs" Inherits="DeffinityAppDev.PaymentForm" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Form</title>
   <%-- <script src="https://js.stripe.com/v3/"></script>--%>
</head>
<body>
  <form id="Form1" runat="server" class="payment-form">
        <div id="d" style="width: 300px; margin: auto; padding: 20px; border: 1px solid #ccc; border-radius: 4px;">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

              
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="Label1" runat="server" Text="You will be redirected in 5 seconds."></asp:Label>
                    <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick">
                    </asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
            <asp:Button ID="submitButton" runat="server" Text="Submit Payment" OnClick="SubmitButton_Click" Visible="false" />
        </div>
    </form>
   
       
</body>
</html>