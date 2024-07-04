<%@ Control Language="C#"  AutoEventWireup="true" Inherits="MailControls_IssueCustomerSignOffMail" Codebehind="IssueCustomerSignOffMail.ascx.cs" %>
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
    <td class="hdr1"> </td>
  </tr>
</table> 
   </td>
  </tr>
  <tr>
    <td height="9" colspan="2" ><asp:Image ImageUrl="~/images/border.gif" height="5px" width="580px" ID="ImgBorder" runat="server" /></td>
  </tr>
  <tr>
    <td colspan="2">
    <br />
    Dear <b class="hilite"><label id="lblReciver" runat="server"></label> </b>, <br />
        <p> The following issue has been signed off by <span class="hilite"><label id="lblSignOffBy" runat="server"></label></span> </p>
        
       
    
</td>
</tr>
<tr>
                <td style="padding-top: 10px;">
                   Project Title:
                </td>
                <td style="padding-top: 10px;">
                    <b><asp:Label ID="lblProjectTitle" runat="server"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 10px;">
                   Issue:
                </td>
                <td style="padding-top: 10px;">
                    <b><asp:Label ID="lblIssue" runat="server"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 10px;">
                    Date Signed off:
                </td>
                <td style="padding-top: 10px;">
                    <b><asp:Label ID="lblDateSignedOff" runat="server"></asp:Label></b>
                </td>
            </tr>
            
           
<tr>
  
    <td colspan="2">
    <p style="font-weight:bold;">Customer Comments</p>
        <asp:GridView ID="Gridview_Comments" runat="server" AutoGenerateColumns="False" 
            HorizontalAlign="Left" CellPadding="0" CellSpacing="1" Width="100%" EmptyDataText="No Comments Found">
            <HeaderStyle CssClass="tab_header" Font-Bold="False" Height="30px" />    
  <RowStyle  CssClass="even_row" />
  <AlternatingRowStyle CssClass="odd_row" />
            <Columns>
                <asp:BoundField HeaderText="Date" DataField="CommentDate"  DataFormatString="{0:d}"/>
                <asp:BoundField HeaderText="Time" DataField="CommentDate" DataFormatString="{0:HH:mm}" />
                <asp:BoundField HeaderText="Comments" DataField="Comments" />
                <asp:BoundField HeaderText="Submitted By" DataField="ContractorName"  ItemStyle-HorizontalAlign="Center" />
                
            </Columns>
        </asp:GridView>  
       
    </td>
</tr>
     <tr>
                    <td style="padding-top: 20px;" colspan="2">
                       To access the system please &nbsp<a style="color: Blue" id="lnkRef"
                             runat="server">click here</a><br />
                            <asp:HyperLink ID="linkWebsiteFooter" runat="server"></asp:HyperLink>
                        <br />
                       
                    </td>
                </tr>

</table>
</body>
</html>
