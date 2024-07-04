<%@ Control Language="C#" AutoEventWireup="true" Inherits="MailControls_ProjectTasklist"  Codebehind="ProjectTasklist.ascx.cs" %>
<%@ Register src="~/WF/MailControls/ProjectDetails.ascx" tagname="ProjectDetails" tagprefix="PD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>
Live Project
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
    <td class="hdr1"> Live Project</td>
  </tr>
</table>    </td>
  </tr>
  <tr>
    <td height="9" ><asp:Image ID="ImgBorder" runat="server" /></td>
  </tr>
  <tr>
    <td>
    <br />
    Dear <b class="hilite"><label id="lblReciver" runat="server"></label> </b> <br /><br />
    This is to let you know that Project <b class="hilite"><label id="lblProjectReference" runat="server"></label> </b> has been made live by <b class="hilite"><label id="lblSender" runat="server"></label></b>.<br /><br />

Details of the project are as follows:<br /><br />

<pd:ProjectDetails ID="ProjectDetails1" runat="server" />
<p>The list of activities assigned to you are as follows:</p>

</td>
</tr>
<tr><td>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr><td>

<asp:GridView ID="Gridview1" runat="server" Width="100%" CellPadding="0" CellSpacing="1" CssClass="Grid_table">
<HeaderStyle CssClass="hdrt" />
<RowStyle  CssClass="even_row" />
<AlternatingRowStyle CssClass="odd_row" />
<Columns>

<asp:TemplateField  HeaderText="Task" >
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                    <asp:Label ID="lblDescription" Width="165px" runat="server" Text='<%#getItemDes(DataBinder.Eval(Container.DataItem, "IndentLevel").ToString(),DataBinder.Eval(Container.DataItem, "ItemDescription1").ToString()) %>'  ></asp:Label>                                           
                    </ItemTemplate>
                    </asp:TemplateField>
<asp:BoundField DataField="ProjectStartDate" HeaderText="StartDate" DataFormatString="{0:d}" HtmlEncode="false" />
<asp:BoundField DataField="ProjectEndDate" HeaderText="End Date"  DataFormatString="{0:d}" HtmlEncode="false"/>
</Columns>
</asp:GridView>
</td></tr> <tr >
    <td colspan="4">
    For an up to date list of activities and to update the project <asp:HyperLink ID="linkWebsite" runat="server">please click</asp:HyperLink> here to access the system.<br /> <br /> 

Thank you.<br /><br />

<b class="hilite"><asp:HyperLink ID="linkWebsiteFooter" runat="server"></asp:HyperLink> </b>    </td>
</tr>
</table> 
   </td>
  </tr>
</table>
</body>
</html>
