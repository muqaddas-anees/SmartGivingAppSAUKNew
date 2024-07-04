<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pwatest.aspx.cs" Inherits="DeffinityAppDev.pwatest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="manifest" href="/manifest.json" crossorigin="use-credentials"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            PWA First test
        </div>


        <script>
    if ('serviceWorker' in navigator) {
        navigator.serviceWorker
            .register('serviceworker.js')
            .then(function () { console.log('Service Worker Registered'); });
    }
        </script>
    </form>
</body>
</html>
