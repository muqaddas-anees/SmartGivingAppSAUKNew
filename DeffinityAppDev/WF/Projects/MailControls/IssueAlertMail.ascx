<%@ Control Language="C#" AutoEventWireup="true" Inherits="MailControls_IssueAlertMail" Codebehind="IssueAlertMail.ascx.cs" %>
<%@ Register src="ProjectDetails.ascx" tagname="ProjectDetails" tagprefix="PD" %>

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



</style>

</head>

<body>


<table align="center" width="600" style="border:1px solid #8595a6; margin-top:10px;" cellspacing="0" cellpadding="0">
  <tr>
    <td height="30" valign="top" ><asp:Image SkinID="MailLogo" ID="imgLogo" runat="server" style="float:left" />
   
    
    <table width="300" border="0" cellspacing="0" cellpadding="0" align="right" style="float:right">
  <tr>
    <td class="hdr1"> Issue</td>
  </tr>
</table>    </td>
  </tr>
  <tr>
    <td height="9" ><asp:Image ImageUrl="~/images/emailer_bg_bott.gif" ID="ImgBorder" runat="server" /></td>
  </tr>
  <tr>
    <td>
    <br />
    Dear <b class="hilite"><label id="lblReciver" runat="server"></label> </b>, <br /><br />
       The following issue has been raised against project <b class="hilite"><label id="lblProjectReference" runat="server"></label> </b> The details of the update are as follows: <label id="lblSender" runat="server" visible="false"></label>
    
    <br /><br />

</td>
</tr>
<tr><td>
<table width="100%" border="0" cellpadding="0" cellspacing="0">


 <tr class="hdr">
    <td colspan="4">Issue</td>
    </tr>
  <tr class="cont_row" >
    <td colspan="4"> <label id="lblIssue" runat="server"></label>&nbsp;</td>
    </tr>
    <tr >
    <td colspan="4">&nbsp;</td>
    </tr>
  <tr class="hdr">
    <td width="50%" >Date raised</td>
    <td width="50%">Issue type</td>
    
   <%-- <td width="34%">Assign to</td>    --%>
  </tr>
  <tr  class="cont_row">
    <td><label id="lblDateraised" runat="server"></label>&nbsp;</td>
    <td><label id="lblIssueType" runat="server"></label>&nbsp;</td>
   <%-- <td> <label id="lblAssignTo" runat="server"></label>&nbsp;</td>--%>
  </tr>
   <tr >
    <td colspan="4">&nbsp;</td>
    </tr>
  <tr class="hdr">
    <td width="50%" >Raised By</td>
    <td width="50%">Status</td>
   <%-- <td width="34%">&nbsp;</td>--%>
  </tr>
  <tr  class="cont_row">
    <td><label id="lblIssueRaisedBy" runat="server"></label>&nbsp;</td>
    <td><label id="lblStatus" runat="server"></label>&nbsp;</td>
    <%--<td> <label id="lblCompletedBy" runat="server"></label>&nbsp;</td>--%>
  </tr>
  <tr >
    <td colspan="4">&nbsp;</td>
    </tr>
   <tr class="hdr">
    <td width="50%"  >RAG Status</td>
    <td width="50%" visible="false">&nbsp;</td>
   <%-- <td width="34%">&nbsp;</td>--%>
  </tr>
  
  <tr  class="cont_row">
   
    <td><label id="lblRag" runat="server"></label>&nbsp; <label ID="ltlDisplay" runat="server"></label></td>
     <td><label id="lblRag1" runat="server"></label>&nbsp;</td>
    <%--<td> <label id="lblCompletedBy" runat="server"></label>&nbsp;</td>--%>
  </tr>

  <tr >
    <td colspan="4">&nbsp;</td>
    </tr>
     <tr class="hdr">
    <td colspan="4">Expected outcome</td>
    </tr>
  <tr class="cont_row" >
    <td colspan="4"><label id="lblExpectedOutcome" runat="server"></label>&nbsp;</td>
    </tr>
    <tr >
    <td colspan="4">&nbsp;</td>
    </tr>
    
<tr >
    <td colspan="4">
   <%-- To update the project--%> <asp:HyperLink ID="linkWebsite" runat="server" Visible="false">please click</asp:HyperLink> <%-- here to access the system.--%><br /> <br /> 

Thank you.<br /><br />

<b class="hilite"><asp:HyperLink ID="linkWebsiteFooter" runat="server"></asp:HyperLink> </b>    </td>
</tr>
</table> 
   </td>
  </tr>
</table>
</body>
</html>