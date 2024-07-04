<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_BOMExportDataReport" Codebehind="BOMExportDataReport.aspx.cs" %>
    

    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <link href="http://www.deffinity.com/dlite/media//favicon1.ico" rel="shortcut icon" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_frame.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_color_scheme.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_custom.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/ajaxtabs.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/customer_admin.css" />
    
</head>
<body>
     <form id="form2" runat="server">
     <script language="javascript" type="text/javascript">
       var retval;
       function Setheight() {
           if (retval == null || retval == true) {
               retval = false;
               document.getElementById("div1").style.height = 270;
           }
           else {
               retval = true;
               document.getElementById("div1").style.height = 140;
           }
           
           return false;
       }
       function calendarShown(sender, args) {  Setheight(); }
   </script>
   
      <div  style="font-family:Verdana, Arial, Helvetica, sans-serif;font-size:13px">
    <div class="sec_header" style="width:98%" >
        <asp:Label ID="lblMsg" runat="server" Text="Project BOM Report"></asp:Label></div>
        </div>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
        <div id="div1" style="font-family:Verdana, Arial, Helvetica, sans-serif;font-size:12px">
        
       
        <table>
        
                
                <tr>
                <td>
                Select Project:<asp:DropDownList ID="ddlProjects" Width="150px" runat="server">
                </asp:DropDownList></td>
                
                
                <td> Select Supplier:
                <asp:DropDownList ID="ddlVendor" Width="150px" runat="server" ></asp:DropDownList>
                </td>
                <td>From Date:
                <asp:TextBox ID="txt_FromDate" runat="server" Width="65px" MaxLength="10" />
                       
                        <asp:Image ID="Image3" runat="server" SkinID="Calender"  />
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender3"  runat="server"
                            PopupButtonID="Image3" TargetControlID="txt_FromDate" CssClass="MyCalendar"  OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>
                 </td>
                 <td>
                 To Date:
                  <asp:TextBox ID="txt_ToDate" runat="server" Width="65px" MaxLength="10" />
                       
                        <asp:Image ID="Image1" runat="server" SkinID="Calender"  />
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                            PopupButtonID="Image1" TargetControlID="txt_ToDate" CssClass="MyCalendar"  OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>
                 </td>
                 
                 
                </tr>
                
                <tr>
                <td>
                 OR Enter Project Reference:
                 <asp:TextBox ID="txtProRef" Width="60px" runat="server"></asp:TextBox>
                </td>
                <td>
                  <asp:ImageButton ID="btn_Submitt" runat="server" ImageUrl="~/media/btn_view_report.gif" 
                            ValidationGroup="one" OnClick="btn_Submitt_Click"  />
                &nbsp;
                 <asp:LinkButton ID="lnkButtonExcel" runat="server" Font-Bold="True" 
                       ForeColor="#004080" onclick="lnkButtonExcel_Click">Excel&nbsp;Export&nbsp;1</asp:LinkButton>
                   &nbsp;
                  
                   <asp:LinkButton ID="lnkButtonExcel1" runat="server" Font-Bold="True" 
                       ForeColor="#004080" onclick="lnkButtonExcel1_Click">Excel&nbsp;Export&nbsp;2</asp:LinkButton>
                   </td>
                </tr>
        </table>
     
         
       </div>  
         
         <div style="overflow:auto">
     <div class="tab_subheader" style="border-bottom:solid 1px Silver;width:100%;">View Reports</div>
               <div style="overflow:auto"> 
                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatepanel1">
    <ProgressTemplate>
    <img src="../media/ico_loading.gif" alt="loading" />
    </ProgressTemplate>
    </asp:UpdateProgress> 
                <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                <div style="z-index:-1000;">
                 <iframe id="TimesheetSummary" name="TimesheetSummary" runat="server" frameborder="0" width="100%" height="600px" scrolling="auto"></iframe>
                    
                </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btn_Submitt" />

                </Triggers>
                </asp:UpdatePanel>
                </div>
                </div>
    </form>
</body>
</html>