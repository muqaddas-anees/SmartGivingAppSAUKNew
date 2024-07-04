<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testpost.aspx.cs" Inherits="DeffinityAppDev.testpost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <asp:Literal ID="newform" runat="server" Text="show"></asp:Literal>
  
    <form id="form1" runat="server">
        <div>
             
    <asp:Button ID="btnSubmit" runat="server" Text="post" OnClick="btn_Click" />
        </div>
    </form>
</body>
</html>
