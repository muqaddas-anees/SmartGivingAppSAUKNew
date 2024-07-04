<%@ Page Language="C#" AutoEventWireup="true" Theme="Theme1" Inherits="Default5" Codebehind="Default5.aspx.cs" %>



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
Live Projects By Sites
 </div>
<div class="sec_table1">
	<div>
        <table width="80%" border="0" cellspacing="0" cellpadding="0">
         <%--   <tr>
                <td colspan="3" class="tab_header"><span><font color="#ff6633" size="4"><b> </b></font></span></td>
               
            </tr>--%>
            <tr> 
               <td width="10%"><asp:Label ID="Label1" runat="server" Text="Select Country" Font-Names="Verdana" Font-Size="X-Small"></asp:Label></td>
                <td width="10%"> <asp:DropDownList ID="ddlCountries" runat="server" Width="140px" CssClass="txt_field"> </asp:DropDownList></td>
                <td width="10%"><asp:ImageButton ID="btnReport" runat="server" OnClick="btnReport_Click"      AlternateText="Get Report" ImageUrl="~/images/btn_view_report.gif"/></td>
            </tr> 
            
            </table>
            <br />
            <br />
            <br />
                    <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="True"
                        height="1039px" reportsourceid="CrystalReportSource1" width="901px" OnInit="CrystalReportViewer1_Init" BorderColor="LightGray" BorderStyle="Groove" DisplayGroupTree="False" HasToggleGroupTreeButton="False" ToolbarStyle-BackColor="Silver" ShowAllPageIds="True"></cr:crystalreportviewer>
                    <cr:crystalreportsource id="CrystalReportSource1" runat="server">
                        <Report FileName="Report5.rpt"><Parameters>
                        <CR:ControlParameter PropertyName="SelectedValue" ReportName="" Name="@CountryID" DefaultValue="" ConvertEmptyStringToNull="False" ControlID="ddlCountries"></CR:ControlParameter>
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
