<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentPage.aspx.cs" Inherits="DeffinityAppDev.stripe.PaymentPage" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Payment Page</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://js.stripe.com/v3/"></script>
</head>
<body>
   <div class="container mt-5">
        <h2>Payment Form</h2>
        <form id="paymentForm" class="needs-validation" novalidate>
            <div class="mb-3">
                <label for="amount" class="form-label">Payment Amount</label>
                <input type="number" class="form-control" id="amount" placeholder="Enter amount" required>
                <div class="invalid-feedback">
                    Please enter a valid amount.
                </div>
            </div>
            <button type="submit" class="btn btn-primary">Submit Payment</button>
        </form>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#paymentForm').on('submit', function (e) {
                e.preventDefault();
                var amount = $('#amount').val();

                $.ajax({
                    url: 'PaymentHandler.ashx',
                    type: 'POST',
                    data: { amount: amount },
                    success: function (response) {
                        var data = JSON.parse(response);
                        if (data.status === 'success') {
                            alert('Payment processed successfully');
                        } else {
                            alert('Payment failed: ' + data.message);
                        }
                    }
                });
            });
        });
    </script>
</body>
</html>