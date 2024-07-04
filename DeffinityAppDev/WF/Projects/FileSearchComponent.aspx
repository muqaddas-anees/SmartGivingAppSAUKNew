<%@ Page Title="" Language="C#" AutoEventWireup="true"
    Inherits="FileSearchComponent" EnableViewState="false" Codebehind="FileSearchComponent.aspx.cs" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <asp:TextBox ID="txtSearchBox" runat="server" Width="900px" Style="height: 30px;
            font-size: 20px; font-weight: bold" Visible="false" /><br />
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" Visible="false" />
    </center>
    <div id="divSearchResults" runat="server">
    </div>
    </form>
</body>
</html>
