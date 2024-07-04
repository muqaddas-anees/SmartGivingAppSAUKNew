<%@ Page Language="C#" AutoEventWireup="true" Theme="Theme1" Inherits="Reports_ProjOutstandingTasks" Codebehind="Proj_OutstandingTasks.aspx.cs" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Deffinity Reports</title>
    <link href="../css/defficss.css" rel="stylesheet" type="text/css" />
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
 Outstanding Elements for Live Projects by City
 </div>
<div class="sec_table1">
	<div>
        <table width="80%" border="0" cellspacing="0" cellpadding="0">
         <%--   <tr>
                <td colspan="3" class="tab_header"><span> <font color="#ff6633" size="4"><b> </b></font></span></td>
               
            </tr>--%>
            <tr> 
                <td width="6%"><b>site</b></td>
                <td width="19%"><asp:DropDownList ID="ddlSite" runat="server" Width="142px" CssClass="txt_field"> </asp:DropDownList></td>
                <td> <asp:ImageButton ID="btnReport" runat="server" ImageUrl="~/images/btn_view_report.gif" OnClick="btnReport_Click" AlternateText="Get Report"/></td>
            </tr> 
            </table>
            <br />
            <br />
            <br />
           
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True"
                        BorderColor="LightGray" BorderStyle="Groove" Height="1039px" ReportSourceID="CrystalReportSource1"
                        Width="901px" DisplayGroupTree="False" HasToggleGroupTreeButton="False" ToolbarStyle-BackColor="Silver" ShowAllPageIds="True" />
                    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                        <Report FileName="Report8.rpt">
                        </Report>
                    </CR:CrystalReportSource>
         
    </div>
  </div>
    </div>
	</div>
</div>


<div class="clr"></div>


</div>
</div>
   </form>
</body>
</html>
