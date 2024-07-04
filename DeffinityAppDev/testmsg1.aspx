<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testmsg1.aspx.cs" Inherits="DeffinityAppDev.testmsg1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:TextBox ID="txttest" runat="server"></asp:TextBox>
    <asp:Button ID="btnSUbmit" runat="server" Text="submit" OnClick="btnSUbmit_Click" />
    <asp:Label ID="lblresult" runat="server"></asp:Label>
    <asp:Button ID="ttt" runat="server" SkinID="btnAdd" />
    <asp:Label ID="lblRed" runat="server" SkinID="RedBackcolor" Text="My test"></asp:Label>
     <asp:Label ID="Label1" runat="server" SkinID="RedBackcolor" Text=""></asp:Label>
    <br />
    <asp:Label ID="lblGreen" runat="server" SkinID="GreenBackcolor" Text="my new test"></asp:Label>
    </div>
    </form>
</body>
</html>
