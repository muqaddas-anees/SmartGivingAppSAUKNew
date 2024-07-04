<%@ Page Language="C#" AutoEventWireup="true"  Theme="Theme1" Inherits="Default15" Codebehind="Default15.aspx.cs" %>



<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Deffinity Reports</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
   <form id="form1" runat="server">
     
    <div class="bodyframe">
<div class="header" >
<div style="clear:both"></div>
<div class="content">
<div class="content_bodyfull1">
<%--<div class="sections">--%>
<div class="clr"></div>
<div  class="sec_body1">
<div class="sec_header">
 Financial Summary
 </div>
<div class="sec_table1">
	<div>
        <table width="80%" border="0" cellspacing="0" cellpadding="0">
        <tr>
        <td>
        Select Owner
        </td>
        <td>
        <asp:DropDownList ID="ddlOwners" runat="server"
         ToolTip="Select the owner" Width="244px" Font-Names="Verdana" CssClass="txt_field">
        </asp:DropDownList>
       <asp:ImageButton ID="btnReport" runat="server" OnClick="btnReport_Click"
        ImageUrl="~/images/btn_view_report.gif" ToolTip="Click here to view the report" AlternateText="Get Report" Font-Names="Verdana" ImageAlign="AbsMiddle" />
        </td>
        </tr>
       
        </table>
        <br />
        <br />
        <br />
        
        <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="True"
            height="1039px" reportsourceid="CrystalReportSource1" width="901px" BorderColor="LightGray" BorderStyle="Groove" DisplayGroupTree="False" HasToggleGroupTreeButton="False" ToolbarStyle-BackColor="Silver"></cr:crystalreportviewer>
        <cr:crystalreportsource id="CrystalReportSource1" runat="server">
<Report FileName="Report15.rpt"><Parameters>
<CR:ControlParameter PropertyName="SelectedValue" ReportName="" Name="@OwnerID" DefaultValue="" ConvertEmptyStringToNull="False" ControlID="ddlOwners"></CR:ControlParameter>
</Parameters>
</Report>
</cr:crystalreportsource>
    

        
    </div>
  </div>
    </div>
	</div>
</div>
</div>

<div class="clr"></div>




</div>
    </form>
</body>
</html>
