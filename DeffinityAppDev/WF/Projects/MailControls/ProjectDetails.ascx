<%@ Control Language="C#" AutoEventWireup="true" Inherits="MailControls_ProjectDetails1" Codebehind="ProjectDetails.ascx.cs" %>

<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr class="hdr">
    <td width="33%" >Project Title</td>
    <td width="33%">Staus</td>
    <td width="34%">Project Owner</td>    
  </tr>
  <tr  class="cont_row">
    <td><label id="lblProjectTitle" runat="server"></label>&nbsp;</td>
    <td><label id="lblProjectStatus" runat="server"></label>&nbsp;</td>
    <td> <label id="lblProjectOwner" runat="server"></label>&nbsp;</td>
  </tr>
  <tr >
    <td colspan="4">&nbsp;</td>
    </tr>
    <tr class="hdr">
    <td colspan="4"  >Email</td>
    </tr>
  <tr class="cont_row" >
    <td colspan="4"> <label id="lblEmail" runat="server"></label>&nbsp;</td>
    </tr>
    <tr >
    <td colspan="4">&nbsp;</td>
    </tr>
  <tr class="hdr">
    <td width="33%">Customer</td>
    <td width="33%">Programme</td>
    <td width="34%">Sub Programme</td>    
  </tr>
  <tr  class="cont_row">
    <td><label id="lblPortfolio" runat="server"></label>&nbsp;</td>
    <td><label id="lblPrograme" runat="server"></label>&nbsp;</td>
    <td><label id="lblSubProgram" runat="server"></label>&nbsp;</td>
  </tr>
  <tr >
    <td colspan="4">&nbsp;</td>
    </tr>
  <tr class="hdr">
    <td width="33%" >Start Date</td>
    <td width="33%">Expected End Date</td>
    <td width="34%">Priority</td>
      
  </tr>
  <tr  class="cont_row">
    <td><label id="lblStartDate" runat="server"></label>&nbsp;</td>
    <td><label id="lblEndDate" runat="server"></label>&nbsp;</td>
    <td><label id="lblPriority" runat="server"></label>&nbsp;</td>
  </tr>
  <tr >
    <td colspan="4">&nbsp;</td>
    </tr>
  <tr class="hdr">
    <td width="33%" >Country</td>
    <td width="33%">City</td>
    <td width="34%">Site</td>    
  </tr>
  <tr  class="cont_row">
    <td> <label id="lblCountry" runat="server"></label>&nbsp;</td>
    <td><label id="lblcity" runat="server"></label>&nbsp;</td>
    <td> <label id="lblSite" runat="server"></label>&nbsp;</td>
  </tr>
  <tr >
    <td colspan="4">&nbsp;</td>
    </tr>
   <tr class="hdr">
    <td width="33%">RAG Status</td>
    <td width="34%">Category</td>    
    <td width="33%">Primary QA</td>
  </tr>
  <tr  class="cont_row">
    <td><asp:Image ID="ImgRag" runat="server" />&nbsp;</td>
    <td><label id="lblCategory" runat="server"></label>&nbsp;</td>
     <td><label id="lblPrimaryQA" runat="server"></label>&nbsp;</td>
  </tr>
  <tr >
    <td colspan="4">&nbsp;</td>
    </tr>

  <tr class="hdr">
    <td colspan="4">Details</td>
    </tr>
  <tr class="cont_row" >
    <td colspan="4"> <label id="lblDescription" runat="server"></label></td>
    </tr>
   <tr>
    <td colspan="4">&nbsp;</td>
    </tr>
    
</table>




<%--<ul >
<li><span>Project Title</span><br /> <label id="lblProjectTitle" runat="server"></label></li>
<li><span>Staus</span><br /> <label id="lblProjectStatus" runat="server"></label></li>
<li><span>Project Owner</span><br /> <label id="lblProjectOwner" runat="server"></label></li>

</ul>
<ul style="border-right:none" >
<li><span>Email</span> </li>
<li style="width:424px"> <label id="lblEmail" runat="server"></label> </li>
</ul>
<ul>
<li><span>Portfolio</span><br /> <label id="lblPortfolio" runat="server"></label></li>
<li><span>Program</span><br /><label id="lblPrograme" runat="server"></label></li>
<li><span>Sub Program</span><br /><label id="lblSubProgram" runat="server"></label></li>
</ul>
<ul >
<li><span>Start Date</span><br /> <label id="lblStartDate" runat="server"></label></li>
<li><span>Expected End Date</span><br /> <label id="lblEndDate" runat="server"></label></li>
</ul>
<ul>
<li><span>Country</span><br /> <label id="lblCountry" runat="server"></label> </li>
<li><span>City</span><br /> <label id="lblcity" runat="server"></label></li>
<li><span>Site</span><br /> <label id="lblSite" runat="server"></label></li>
</ul>
<ul>
<li><span>Priority</span><br /> <label id="lblPriority" runat="server"></label> </li>
<li><span>RAG Status </span><br /><asp:Image ID="ImgRag" runat="server" /></li>

<li><span>Category</span><br /> <label id="lblCategory" runat="server"></label> </li>
</ul>
<ul style="border-right:none" >
<li><span>Details</span> </li>
<li style="width:424px"> <label id="lblDescription" runat="server"></label> </li>
</ul>
--%>