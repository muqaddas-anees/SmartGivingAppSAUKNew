<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Reports_BenefitTrackingSummaryReport" Codebehind="BenefitTrackingSummaryReport.aspx.cs" %>
   
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  
</head>
<body>
    <form id="form1" runat="server">
    <asp:Image ID="imgExcel" runat="server" ImageUrl="~/media/ico_excel.png" ImageAlign="AbsMiddle"/>
    <asp:LinkButton ID="btnExportExcel" runat="server" Font-Bold="true" 
                            onclick="btnExportExcel_Click" ForeColor="Navy">Export to Excel</asp:LinkButton><br />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <div style="z-index: -1000;">
                <iframe id="TimesheetSummary" name="TimesheetSummary" runat="server" frameborder="0"
                    width="100%" height="600px" scrolling="auto"></iframe>
            </div>
        </ContentTemplate>
     </asp:UpdatePanel>
    </form>
</body>
</html>
