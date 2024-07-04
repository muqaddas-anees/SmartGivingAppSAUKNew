<%@ Control Language="C#" AutoEventWireup="true" Inherits="MailControls_ProjectInvoiceMail" Codebehind="ProjectInvoiceMail.ascx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>
 Interim Invoice for Project
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
font-size:18px;
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

</style>

</head>

<body>


<table align="center" width="600" style="border:1px solid #8595a6; margin-top:10px;" cellspacing="0" cellpadding="0">
  <tr>
    <td height="30" valign="top" ><asp:Image ID="imgLogo" runat="server" style="float:left" />
    <table width="300" border="0" cellspacing="0" cellpadding="0" align="right" style="float:right">
  <tr>
    <td class="hdr1">Interim Invoice for Project</td>
  </tr>
</table>    </td>
  </tr>
  <tr>
    <td height="9" ><asp:Image ID="ImgBorder" runat="server" /></td>
  </tr>
  <tr>
    <td>
   <%-- Dear <b class="hilite"><label id="lblReciver" runat="server"></label> </b> <br /><br />--%>
    <p>Project Reference: <b class="hilite"><label id="lblProjectReference" runat="server"></label></b><br />
    Project Title: <b class="hilite"><label id="lblTitle" runat="server"></label></b><br />
    Invoice Reference: <b class="hilite"><label id="lblInvoice" runat="server"></label></b><br />
     Total&nbsp;Project&nbsp;Value: <b class="hilite"><label id="lblTotalPV" runat="server"></label></b></p>
<p></p>
</td>
</tr>
<tr><td>
<asp:GridView ID="GridView1" runat="server"  CellPadding="0" CellSpacing="1" CssClass="Grid_table" >
                 <HeaderStyle CssClass="hdrt" />
<RowStyle  CssClass="even_row" />
<AlternatingRowStyle CssClass="odd_row" />
                 <Columns>
                    <asp:TemplateField HeaderText="Task">
                    <ItemStyle Width="300px" />
                    <HeaderStyle Width="300px" />
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ItemDescription") %>'></asp:Label>
                            <asp:HiddenField ID="HID" runat="server" Value='<%# Bind("ID") %>' />
                        </ItemTemplate>
                        
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QTY">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
                        </ItemTemplate>
                      
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Selling Price">
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label31" runat="server" Text='<%# Bind("Price","{0:F2}") %>'></asp:Label>
                        </ItemTemplate>
                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total" >
                     <ItemStyle HorizontalAlign="Right" Width="100px"/>
                     <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Total","{0:F2}") %>'></asp:Label>
                        </ItemTemplate>
                     
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="% Complete">
                     <ItemStyle HorizontalAlign="Center" Width="100px"/>
                    <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("PercentComplete") %>'></asp:Label>
                        </ItemTemplate>
                      
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Claimed Total">
                     <ItemStyle HorizontalAlign="Right" Width="100px"/>
                     <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("ClaimedTotal","{0:F2}") %>'></asp:Label>
                        </ItemTemplate>
                       
                    </asp:TemplateField>
                </Columns>
                </asp:GridView>
               
 
   </td>
  </tr>
  <tr>
  <td>
  <table width="100%" border="0" cellpadding="0" cellspacing="0">
 <tr>
            <td style="width: 178px">
               Sub&nbsp;Total:&nbsp;&nbsp;</td>
            <td style="width: 100px" align="right">
                <asp:Label ID="lblTotalInvoice" runat="server" Font-Bold="true"></asp:Label></td>
            <td>
            </td>
        </tr>
 <tr>
            <td style="width: 178px">
                Vat (<asp:Label ID="lblVat" runat="server" Font-Bold="true"></asp:Label>):</td>
            <td style="width: 100px" align="right">
                <asp:Label ID="lblVatcal" runat="server" Font-Bold="true"></asp:Label></td>
            <td>
                &nbsp;</td>
        </tr>
 <tr>
            <td style="width: 178px">
               Invoice&nbsp;Total:</td>
            <td style="width: 100px" align="right">
               <asp:Label ID="lblInvoiceTotal" runat="server" Font-Bold="true"></asp:Label></td>
            <td>
                &nbsp;</td>
        </tr>
  <tr>
            <td style="width: 178px">
                Revised&nbsp;Project&nbsp;Value&nbsp;: </td>
            <td style="width: 100px" align="right">
                <asp:Label ID="lblRevisedProjectValue" runat="server" Font-Bold="true"></asp:Label></td>
            <td>
            </td>
        </tr>
       
<tr >
    <td colspan="3">
    <br /> <br />
        Please&nbsp;<asp:HyperLink ID="linkWebsite" runat="server" Text="click here"></asp:HyperLink>&nbsp;to access the system.<br /> <br /> 
Thank you.<br /><br />
<b class="hilite"><asp:HyperLink ID="linkWebsiteFooter" runat="server"></asp:HyperLink> </b>    </td>
</tr>
</table>
  </td>
  </tr>
</table>
</body>
</html>
