<%@ Control Language="C#" AutoEventWireup="true" Inherits="MailControls_TimesheetApproveDetails" Codebehind="TimesheetApproveDetails.ascx.cs" %>
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
      <td height="30" valign="top">
          <asp:Image ID="imgLogo" runat="server" Style="float: left" />
          <table width="300" border="0" cellspacing="0" cellpadding="0" align="right" style="float: right">
              <tr>
                  <td class="hdr1">
                   <%= Resources.DeffinityRes. TimesheetDetails%> 
                  </td>
              </tr>
          </table>
      </td>
  </tr>
  <tr>
    <td height="9" ><asp:Image ID="ImgBorder" runat="server" /></td>
  </tr>
  <tr>
<td>
 <%= Resources.DeffinityRes. Dear %> <asp:Label ID="resourecName" runat="server" ForeColor="Navy" Font-Bold="true"></asp:Label>
</td>
</tr>
<tr>
    <td>
    <br />
The following Timesheet has been submitted for approval for week commencing <asp:Label ID="litapprovereject" runat="server" ForeColor="Navy" Font-Bold="true" Visible="false"></asp:Label>   <asp:Label ID="lblWCDate" runat="server" ForeColor="Navy" Font-Bold="true"></asp:Label>
      <asp:Literal ID="litprojectname" runat="server"></asp:Literal><br />
</td>
</tr>
<tr><td colspan="4">
    <asp:GridView ID="Gridview1" runat="server" Width="100%" CellPadding="0" CellSpacing="1"
        CssClass="Grid_table">
        <HeaderStyle CssClass="hdrt" />
        <RowStyle CssClass="even_row" />
        <AlternatingRowStyle CssClass="odd_row" />
        <Columns>
        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ProjectTitle%>">
               <ItemStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="ProjectTitle" runat="server" Text='<%# Bind("ProjectTitle") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ProjectTask%>">
               <ItemStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="ProjectTask" runat="server" Text='<%# Bind("ProjectTask") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ResourceName%>">
               <ItemStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ContractorName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Status" HeaderText="<%$ Resources:DeffinityRes,Status%>" HtmlEncode="false" />
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,DateEntered%>">
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="Label31" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TypeOfHours%>">
               <ItemStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("TypeOfHours") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Hours%>">
                <ItemStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# ChangeHoues(Eval("Hours").ToString())%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Notes%>">
              <ItemStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("notes") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</td></tr>



  

 
<tr >
 <td colspan="4">
    
Please <asp:HyperLink ID="linkWebsite" runat="server" Text="click here"></asp:HyperLink> &nbsp;to access the system.<br /> <br /> 

Thank you.<br /><br />

<b class="hilite"><asp:HyperLink ID="linkWebsiteFooter" runat="server"></asp:HyperLink> </b>    </td>
</tr>

   
</table>
</body>
</html>