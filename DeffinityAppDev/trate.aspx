<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="trate.aspx.cs" Inherits="DeffinityAppDev.trate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       
    <ajaxToolkit:Rating ID="Rating1" AutoPostBack="true" runat="server"
    StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star"
    FilledStarCssClass="FilledStar">
        </ajaxToolkit:Rating>
    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </form>
</body>
</html>
