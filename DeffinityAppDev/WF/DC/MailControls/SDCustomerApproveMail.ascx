<%@ Control Language="C#"  AutoEventWireup="true" Inherits="MailControls_SDCustomerApproveMail" Codebehind="SDCustomerApproveMail.ascx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>
Issue
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

.even_row{
background:#f2f9ec;
}

.odd_row{
background:#f5f5fa;
}

.tab_header{
background:#a3a3a3;
color:#fff;
padding:3px;
}


</style>

</head>

<body>


<table align="center" width="600" style="border:5px solid #b3b3b3; margin-top:10px;" cellspacing="0" cellpadding="0">
  <tr>
    <td height="30" valign="top" ><asp:Image SkinID="MailLogo" ID="imgLogo" runat="server" style="float:left" />
    </td>
    <td>
      
    <table width="300" border="0" cellspacing="0" cellpadding="0" align="right" style="float:right">
  <tr>
    <td class="hdr1"><label id="lblTypeOfRequest" runat="server"></label> </td>
  </tr>
</table> 
   </td>
  </tr>
  <tr>
    <td height="9" colspan="2" ><asp:Image height="5px" width="580px" ID="ImgBorder" runat="server" /></td>
  </tr>
  <tr>
    <td colspan="2">
    <br />
    Dear <b class="hilite"><label id="lblReciver" runat="server"></label> </b>, <br /><br />
        <p> <b class="hilite"><label id="lblIncidentID" runat="server"></label>&nbsp<label id="lbldiscription" runat="server"></label>  </b>has been authorised by the customer. </p>
        <p>The purchase order number provided is:<b class="hilite"><label id="lblpono" runat="server"></label></b></p>
        <p style="font-weight:bold;">Here are the details of the order:</p> 
        <p>Requesters Name: <b class="hilite"><label id="lblrequster" runat="server"></label></b> </p>
        <p>Details:<b class="hilite"><label id="lbldetails" runat="server"></label></b> </p>
        
    

</td>
</tr>
<tr>
    <td colspan="2">
           <asp:GridView ID="Gridview_services" runat="server" AutoGenerateColumns="False" 
            DataSourceID="obj_services" HorizontalAlign="Left" CellPadding="0" CellSpacing="1" Width="100%">
            <HeaderStyle CssClass="tab_header" Font-Bold="False" Height="30px" />    
  <RowStyle  CssClass="even_row" />
  <AlternatingRowStyle CssClass="odd_row" />
            <Columns>
                <asp:BoundField HeaderText="Item" DataField="Description" />
                <asp:BoundField HeaderText="Notes" DataField="Notes" />
                <asp:BoundField HeaderText="Unit Price" DataField="SellingPrice" HtmlEncode="false" DataFormatString="{0:f2}"  ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField HeaderText="QTY" DataField="QTY"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Total" DataField="Total" HtmlEncode="false" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField HeaderText="Units" DataField="TotalUnits" HtmlEncode="false" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" Visible="false" />
            </Columns>
        </asp:GridView>  
        <asp:ObjectDataSource ID="obj_services" runat="server" 
            SelectMethod="Services_SelectByIncidentID" 
            TypeName="Deffinity.IncidentService.ServiceManager">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="0" Name="IncidentID" 
                    SessionField="IncidentID" Type="Int32" />
                    <asp:ControlParameter Name="Type" ControlID="hfType" PropertyName="Value" />
            </SelectParameters>
           
        </asp:ObjectDataSource>
        <asp:HiddenField ID="hfType" runat="server" Value="0"/>
    </td>
</tr>
<tr>
<td colspan="2">
<table style="border:1px solid black;border-collapse:collapse;width:60%;"  >
<tr>
<td style="border:1px solid black;">Total&nbsp;original&nbsp;value:
</td>
<td align="right" style="border:1px solid black;">
 <label id="lblTotalValue" runat="server"></label>
</td>
</tr>
<tr>
<td style="border:1px solid black;">Discount %:
</td>
<td align="right" style="border:1px solid black;">
<label id="lblDiscount" runat="server"></label>
</td>
</tr>
<tr>
<td style="border:1px solid black;"><b>Revised&nbsp;Price:</b>
</td>
<td style="border:1px solid black;" align="right"> 
<b><label id="lblRevisedPrice" runat="server"></label></b>
</td>
</tr>
<tr>
<td style="border:1px solid black;">Notes:
</td>
<td style="border:1px solid black;">
<label id="lblNotes1" runat="server"></label>
</td>
</tr>
</table>
</td>
</tr>
<tr >
    <td colspan="2">
  
    <br /> <br /> 
Thank you.<br /><br />

<b class="hilite"><asp:HyperLink ID="linkWebsiteFooter" runat="server"></asp:HyperLink> </b>    </td>
</tr>
</table>
</body>
</html>