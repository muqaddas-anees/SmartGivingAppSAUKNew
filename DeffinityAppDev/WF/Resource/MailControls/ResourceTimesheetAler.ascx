<%@ Control Language="C#" AutoEventWireup="true" Inherits="Resource_MailControls_ResourceTimesheetAler" Codebehind="ResourceTimesheetAler.ascx.cs" %>

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
    <td class="hdr1"> Time Alert for Project <label id="lblProjectReference" runat="server" ></label> </td>
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
        
        
       
    
</td>
</tr>
<tr>
    <td colspan="2">
       Please note that <label id="lblResource" runat="server" style="font-weight:bold"></label> has booked <label id="lblTotalHrsboked" runat="server" style="font-weight:bold"></label>  so far for this project. The maximum allocated for this project is <label id="lblMaxhrs" runat="server" style="font-weight:bold"></label>. 
        
    </td>
</tr>
<tr >
    <td colspan="2">
  Please <asp:HyperLink ID="linkWebsite" runat="server">click here</asp:HyperLink> to access the sytem.
    <br /> <br /> 
Thank you.<br /><br />

<b class="hilite"><asp:HyperLink ID="linkWebsiteFooter" runat="server"></asp:HyperLink> </b>    </td>
</tr>
</table>
</body>
</html>