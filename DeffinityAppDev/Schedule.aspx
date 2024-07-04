<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Schedule.aspx.cs" Inherits="DeffinityAppDev.Schedule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnStart" runat="server" Text="Start" OnClick="btnStart_Click" />
        </div>


        <script language="javascript" type="text/javascript">
            //$(document).ready(function () {
            //    setInterval("location.reload(true)", 300000);
            //});

            function autoRefreshPage() {
                window.location = window.location.href;
            }
            setInterval('autoRefreshPage()', 300000);
        </script>
    </form>
</body>
</html>
