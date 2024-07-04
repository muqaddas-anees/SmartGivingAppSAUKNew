<%@ Control Language="C#" AutoEventWireup="true" Inherits="Resource_MailControls_TaskNotes" Codebehind="TaskNotes.ascx.cs" %>
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

    .style1
    {
        width: 62%;
    }

</style>

</head>

<body>


<table align="center" width="600" style="border:1px solid #8595a6; margin-top:10px;" cellspacing="0" cellpadding="0">
  <tr>
    <td height="30" valign="top" class="style1"  colspan="3"><asp:Image ID="imgLogo" runat="server" style="float:left" />
   
    
    <table width="300" border="0" cellspacing="0" cellpadding="0" align="right" style="float:right">
  <tr>
    <td class="hdr1"> Issue</td>
  </tr>
</table> 
   </td>
  </tr>
  <tr>
    <td height="9" class="style1"  colspan="3" ><asp:Image ID="ImgBorder" runat="server" /></td>
  </tr>
  <tr>
      <td class="style1" colspan="3">
          <br />
          Dear <b class="hilite">
              <label id="lblReciver" runat="server">
              </label>
          </b>
          <br />
          <br />
          The following issue has been raised for Task:
          <asp:Literal ID="littask" runat="server"></asp:Literal><br />
          <br />
      </td>
</tr>
 <tr class="hdr">
    <td colspan="3">Note</td> 
  </tr>  
<tr class="cont_row" >
    <td colspan="3"> <asp:Literal ID="litnotes" runat="server"></asp:Literal></td>
</tr>
 <tr >
    <td colspan="3">&nbsp;</td>
    </tr>
<tr class="hdr">   
    <td width="33%">Raised by</td>  
     <td width="33%">Date Raised</td> 
     <td width="33%"></td> 
</tr>       
<tr class= "cont_row" >
<td width="33%"> <asp:Literal ID="litraisedby" runat="server"></asp:Literal></td>
<td width="33%"> <asp:Literal ID="litdateraised" runat="server"></asp:Literal></td>
<td></td>
</tr>
 

 <tr >
    <td colspan="4">
    Please <asp:HyperLink ID="linkWebsite" runat="server">click here</asp:HyperLink> to access the system.<br /> <br /> 

Thank you.<br /><br />

<b class="hilite"><asp:HyperLink ID="linkWebsiteFooter" runat="server"></asp:HyperLink> </b>    </td>
</tr>

   
</table>
</body>
</html>
   



