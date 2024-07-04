<%@ Page Language="C#" AutoEventWireup="true" Inherits="TimeSheetReport" Codebehind="TimeSheetReport.aspx.cs" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Time Sheet Report</title>
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

<div class="sec_table1">
	<div>
        <table width="80%" border="0" cellspacing="0" cellpadding="0">
  </table>
           <br />
           <br />
           <br />
              <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="True"
                        height="1039px" reportsourceid="CrystalReportSource1" width="901px" BorderColor="LightGray" BorderStyle="Groove" DisplayGroupTree="False" HasToggleGroupTreeButton="False" ToolbarStyle-BackColor="Silver"></cr:crystalreportviewer>
              <cr:crystalreportsource id="CrystalReportSource1" runat="server">
            <Report FileName="Report3.rpt"></Report>
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
