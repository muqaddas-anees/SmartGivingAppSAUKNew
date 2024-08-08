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
                <button style="display:none;" id="checkout-button">Checkout</button>
            <div id="payment-request-button" style="display:none;"></div>
        </div>
    </form>
   <script src="https://js.stripe.com/v3/"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
         <script>         var stripe = Stripe('pk_live_51M91uyCR8tWIOz63gZPS53dqPPDHGZFgZdOyQQEneKK0iMtPgJKShHWaGyGKkrZOhn2mhH9y1JbCcASOdbHISc4R00g2rV2Mxm'); // Replace with your Stripe publishable key


              $(document).ready(function () {


        $('#checkout-button').on('click', function () {
            $.ajax({
                url: 'PaymentForm.aspx/CreateCheckoutSession',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ id: yourPaymentTrackerId }), // Replace with your payment tracker ID
                success: function(session) {
                    stripe.redirectToCheckout({ sessionId: session.id })
                    .then(function(result) {
                        if (result.error) {
                            alert(result.error.message);
                        }
                    });
                },
                error: function(xhr, status, error) {
                    console.error('Error:', error);
                }
            });
        });

        // Adding Apple Pay and Google Pay support
        var paymentRequest = stripe.paymentRequest({
            country: 'US',
            currency: 'usd',
            total: {
                label: 'Service Charge', // Use a descriptive label
                amount: 1999, // This will be dynamically updated in the on('paymentmethod') event
            },
            requestPayerName: true,
            requestPayerEmail: true,
            requestPayerPhone: true,
            paymentMethodTypes: ['card'], // Supports both Apple Pay and Google Pay
        });

        var elements = stripe.elements();
        var prButton = elements.create('paymentRequestButton', {
            paymentRequest: paymentRequest,
            style: {
                paymentRequestButton: {
                    type: 'default', // 'default', 'donate', 'buy' or 'branded'
                    theme: 'dark', // 'dark', 'light', or 'light-outline'
                    height: '64px', // Defaults to '40px'
                },
            },
        });

        paymentRequest.canMakePayment().then(function(result) {
            if (result) {
                prButton.mount('#payment-request-button');
            } else {
                document.getElementById('payment-request-button').style.display = 'none';
            }
        });

        paymentRequest.on('paymentmethod', function(ev) {
            $.ajax({
                url: 'PaymentForm.aspx/CreateCheckoutSession',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ id: yourPaymentTrackerId }), // Replace with your payment tracker ID
                success: function(session) {
                    stripe.confirmCardPayment(
                        session.client_secret,
                        {
                            payment_method: ev.paymentMethod.id,
                        },
                        { handleActions: false }
                    ).then(function(confirmResult) {
                        if (confirmResult.error) {
                            ev.complete('fail');
                            alert(confirmResult.error.message);
                        } else {
                            ev.complete('success');
                            if (confirmResult.paymentIntent.status === 'requires_action') {
                                stripe.confirmCardPayment(session.client_secret);
                            } else {
                                // Payment succeeded, handle success
                                window.location.href = '/success.html'; // Redirect to your success page
                            }
                        }
                    });
                },
                error: function(xhr, status, error) {
                    console.error('Error:', error);
                    ev.complete('fail');
                }
            });
        });
    });
    </script>

   <script>
       $(document).ready(function () {
           // Wait for 5 seconds before triggering the button click
          var button= document.getElementById('checkout-button');
           console.log(button);
               
           setTimeout(function () {
               $('#checkout-button').click();
           }, 5000); // 5000 milliseconds = 5 seconds
       });
  </script>
</body>
</html>