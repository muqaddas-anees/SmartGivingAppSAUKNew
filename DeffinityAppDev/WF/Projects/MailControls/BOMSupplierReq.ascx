<%@ Control Language="C#" AutoEventWireup="true" Inherits="MailControls_BOMSupplierReqNew" Codebehind="BOMSupplierReq.ascx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>
Quote Management
</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<style type="text/css">
body{
margin:0;
font-family:Verdana, Arial, Helvetica, sans-serif;
font-size:12px;
}
table td{
padding:5px;
}

.hdr1{
font-size:17px;
padding-top:15px;
text-align:right;
}

.hilite{
color:#4b0049;

}

.hdr td{
font-size:12px;
font-weight:bold;
color:#fff;
background:#8595a6;
text-align:left;
}

.cont_row td{
border:#8595a6 1px solid;
color:#219de6;
font-weight:bold
}

.bo_line{
border-bottom:#999 10px solid;
}

.hdrt {
font-size:12px;
font-weight:bold;
color:#fff;
background:#8595a6;
text-align:left;
}

table.Grid_table {
border:#e4dcd3 1px solid;
}

.Grid_table td{
height:15px;
padding:5px 0 0 10px;
}

.odd_row{
background:#f2f1f1;
color:#636363;
}

.even_row{
background:#fff;
color:#636363;
}

    .style1
    {
        width: 62%;
    }

    </style>

</head>

<body>


<table align="center" width="600" style="border:1px solid #8595a6; margin-top:10px;" cellspacing="0" cellpadding="0">
  <tr>
    <td height="30" valign="top" class="style1"  colspan="4"><asp:Image ID="imgLogo" runat="server" style="float:left" />
   
    
    <table width="300" border="0" cellspacing="0" cellpadding="0" align="right" style="float:right">
  <tr>
    <td class="hdr1"  colspan="4" >Supplier Requisition</td>
  </tr>
</table> 
   </td>
  </tr>
  <tr>
    <td height="9" class="style1"  colspan="4" ><asp:Image ID="ImgBorder" runat="server" /></td>
  </tr>
    <tr>
        <td class="style1" colspan="4">
            Dear
            <asp:Label ID="lblrecievername" runat="server" ForeColor="Navy"></asp:Label>,
            <br />
            <br />
            Please find attached a copy of a supplier requisition from <i>
                <asp:Label ID="lblInstanceName" runat="server" Text="" ForeColor="DarkBlue" Font-Bold="true"></asp:Label></i>&nbsp;for the  Project: <asp:Label ID="lblprojectref" runat='server' ForeColor="DarkBlue" Font-Bold="true"></asp:Label>
            <br />
            <br />
            Project&nbsp;Owner:<asp:Label ID="lblOwnerName" runat="server" Text="" ForeColor="DarkBlue" Font-Bold="true"></asp:Label>
            <br />Telephone&nbsp;Number: <asp:Label ID="lblTelephone" runat="server" ForeColor="DarkBlue" Font-Bold="true"> </asp:Label>.
            <br />  Email&nbsp;Address:<asp:Label ID="lblOwnerEmail" runat="server" Text="" ForeColor="DarkBlue" Font-Bold="true"></asp:Label>
            <br />
            <br />
            <%--If you would like to discuss this with the Project Manager please email 
            <asp:Label ID="lblmanager" runat="server" ForeColor="DarkBlue" Font-Bold="true"> </asp:Label>
             &nbsp; on &nbsp;
            <asp:Label ID="lblemail" runat='server' ForeColor="DarkBlue"></asp:Label>&nbsp; 
            <asp:Label ID="lblmobile" runat="server" ForeColor="DarkBlue"> </asp:Label>.
            <br />
            <br />--%>
            Thank You </tr>
</table>
</body>
</html>