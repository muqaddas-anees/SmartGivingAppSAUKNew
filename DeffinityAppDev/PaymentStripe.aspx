<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentStripe.aspx.cs" Inherits="DeffinityAppDev.PaymentStripe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="https://js.stripe.com/v3/"></script>
    <style>
        /* Style the card element form */
        #card-element {
            border: 1px solid #ccc;
            padding: 10px;
            margin-bottom: 10px;
            border-radius: 5px;
            background-color: #f8f8f8;
        }
        #paymentform button {
            background-color: #6772e5;
            color: white;
            border: none;
            padding: 10px;
            border-radius: 5px;
            cursor: pointer;
        }
        #paymentform button:hover {
            background-color: #5469d4;
        }
    </style>
</head>
<body>
    <form id="paymentform" runat="server">
       <div id="card-element"></div>
        <button type="submit">Pay Now</button>
        <!-- Error message container -->
        <div id="card-errors" role="alert"></div>
    </form>

     <script>
         var stripe = Stripe('pk_test_51P9j0YSAzteR3BT7K0ef2mwTp2olrPmWibRB9tFyXJjalKAhDWS7fSipiYShaGeQM0z1ZGtVjXEeDSAfFXihiXwc00NIJvBtGV');
         var elements = stripe.elements();

         var style = {
             base: {
                 color: "#32325d",
             }
         };

         var card = elements.create('card', { style: style });
         card.mount('#card-element');

         card.addEventListener('change', function (event) {
             var displayError = document.getElementById('card-errors');
             if (event.error) {
                 displayError.textContent = event.error.message;
             } else {
                 displayError.textContent = '';
             }
         });

         var form = document.getElementById('paymentform');
         form.addEventListener('submit', function (event) {
             event.preventDefault();

             stripe.createToken(card).then(function (result) {
                 if (result.error) {
                     // Inform the user if there was an error.
                     var errorElement = document.getElementById('card-errors');
                     errorElement.textContent = result.error.message;
                 } else {
                     // Send the token to your server.
                     $.ajax({
                         url: 'PaymentHandler.ashx',
                         method: 'POST',
                         data: { stripeToken: result.token.id },
                         success: function (response) {
                             alert('Payment success: ' + response);
                         },
                         error: function () {
                             alert('Error processing payment');
                         }
                     });
                 }
             });
         });
     </script>
</body>
</html>
