<%@ Control Language="C#" AutoEventWireup="true" Inherits="MailControls_Timesheet_Pendingmail" Codebehind="Timesheet_Pendingmail.ascx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>
    Time sheet Details
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
height:30px;
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
    <td height="30" valign="top" class="style1"><asp:Image ID="imgLogo" runat="server" style="float:left" />
   
    
    <table width="300" border="0" cellspacing="0" cellpadding="0" align="right" style="float:right">
 <%-- <tr id="Heading" runat="server" visible="false">
    <td class="hdr1">Timesheet Details</td>
  </tr>--%>
  <tr id="Headingtwo" runat="server" visible="false">
    <td class="hdr1">Timesheet Reminder</td>
  </tr>
</table> 
   </td>
  </tr>
  <tr>
    <td height="9" class="style1" ><asp:Image ID="ImgBorder" runat="server" /></td>
  </tr>

<tr>
<td>
    Dear <asp:Label ID="resourecName" runat="server" ForeColor="Navy" Font-Bold="true"></asp:Label>
</td>
</tr>

<%--<tr id="GetApprove" runat="server" visible="false">
    <td>
    <br />
        Your timesheet for WC <asp:Label ID="lblWCDate" runat="server" ForeColor="Navy" Font-Bold="true"></asp:Label> 
        has been <asp:Label ID="lblapproval" runat="server" ForeColor="Navy" Font-Bold="true"></asp:Label>  
      
        <br />
</td>
</tr>--%>

<tr id="Getsubmitt" runat="server" visible="false">
    <td>
        Please submit your timesheet for week commencing <asp:Label ID="lblweekCDate11" runat="server" ForeColor="Navy" Font-Bold="true"></asp:Label>
        .To log into the system <asp:HyperLink id="getlogin" runat="server" Text="click here"></asp:HyperLink> <asp:Label ID="Label1" runat="server" ForeColor="Navy" Font-Bold="true" Visible="false"></asp:Label>    </td>
</tr>


<tr><td>
   <%-- <asp:GridView ID="Gridview1" runat="server" Width="100%" CellPadding="0" CellSpacing="1"
        CssClass="Grid_table">
        <HeaderStyle CssClass="hdrt" />
        <RowStyle CssClass="even_row" />
        <AlternatingRowStyle CssClass="odd_row" />
        <Columns>
            <asp:TemplateField HeaderText="Resource Name">
               <ItemStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ContractorName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Status" HeaderText="Status" HtmlEncode="false" />
            <asp:TemplateField HeaderText="Date Entered">
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="Label31" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Entry Type">
               <ItemStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("EntryType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hours">
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# ChangeHoues(Eval("Hours").ToString())%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Notes">
              <ItemStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("notes") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>--%>
</td></tr>
<tr>
<br />
<td>
<%--<asp:Label ID="lblNotes" runat="server" Visible="False" Width="582px" Height="54px"></asp:Label>--%>
</td>
</tr>
 <tr id="GetFooter" runat="server" visible="false">
    <td>
        To log into the system please <asp:HyperLink id="linkWebsite" runat="server" Text="click here"></asp:HyperLink>
        .
       <br /> <br /> 

  </td>
</tr>
<tr>

<td>
<br /> <br /> 
    Thank you.<br /><br />

<b class="hilite"><asp:HyperLink ID="linkWebsiteFooter" runat="server"></asp:HyperLink> </b>  </td>
</tr>

</table>
</body>
</html>