<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_OvertimeCutOffReport" Codebehind="OvertimeCutOffReport.aspx.cs" %>
<%@ Register Assembly="Evyatar.Web.Controls" Namespace="Evyatar.Web.Controls" TagPrefix="evy" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="http://www.deffinity.com/dlite/media//favicon1.ico" rel="shortcut icon" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_frame.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_color_scheme.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_custom.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/ajaxtabs.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/customer_admin.css" />
</head>
<body>
    <form id="form1" runat="server">
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
    <div id="div1" style="font-family:Verdana, Arial, Helvetica, sans-serif;font-size:13px;overflow:auto">
    <div class="sec_header" style="width:98%" >
        <asp:Label ID="lblMsg" runat="server" Text="Overttime Cut-off Report"></asp:Label></div>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
    <td colspan="9"><asp:ValidationSummary ID="V1" runat="server" ValidationGroup="one" />
     <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                   <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                            PopupButtonID="imgbtnenddate6" TargetControlID="txtFromDate" CssClass="MyCalendar" OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                            PopupButtonID="Image1" TargetControlID="txtToDate" CssClass="MyCalendar" OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>
 <asp:CompareValidator ID="c1" runat="server" ControlToCompare="txtFromDate" ControlToValidate="txtToDate"
                        Display="none" Type="Date" Operator="GreaterThanEqual" ErrorMessage="start date can not greater then end date" ValidationGroup="one" ></asp:CompareValidator>      
                        
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter from date"
                         ControlToValidate="txtFromDate" Display="None" ValidationGroup="one"></asp:RequiredFieldValidator>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldVal2" runat="server" ErrorMessage="Please enter to date"
                         ControlToValidate="txtToDate" Display="None" ValidationGroup="one"></asp:RequiredFieldValidator>
                         
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtFromDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one" >*</asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtToDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one">*</asp:RegularExpressionValidator>
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
    </tr>
    <tr><td>From&nbsp;Date :</td> 
       <td>  
           <asp:TextBox ID="txtFromDate" runat="server" Width="75px"></asp:TextBox>
            <asp:Image ID="imgbtnenddate6" runat="server" SkinID="Calender" /></td>
        <td> To&nbsp;Date:</td>
         <td>
            <asp:TextBox ID="txtToDate"  Width="75px" runat="server"></asp:TextBox> <asp:Image ID="Image1" runat="server" SkinID="Calender"  /> </td>
         <td>
             Cut&nbsp;Off:</td>
         <td>
             <asp:TextBox ID="txtCutOff" runat="server" Width="50"  MaxLength="5"></asp:TextBox>(hh:mm)
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                 ErrorMessage="Please enter valid time. Format hh:mm " Display="None" ValidationGroup="one"
                  ControlToValidate="txtCutOff" ></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="regex1" runat="server" ControlToValidate="txtCutOff"
                                                ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="one"
                                                Display="None"  ErrorMessage="Please enter valid time. Format hh:mm "></asp:RegularExpressionValidator></td>
         <td>
              Type:</td>
         <td>
            <%-- <asp:ListBox ID="lstEntryType" runat="server" Width="175px" 
                 SelectionMode="Multiple">
                 <asp:ListItem Selected="True" Value="1">Normal Hours</asp:ListItem>
                 <asp:ListItem Selected="True" Value="2">Overtime</asp:ListItem>
                 <asp:ListItem Selected="True" Value="3">Public Holiday</asp:ListItem>
                 <asp:ListItem Selected="True" Value="4">Unplanned Absence</asp:ListItem>
                 <asp:ListItem Selected="True" Value="5">Training</asp:ListItem>
                 <asp:ListItem Selected="True" Value="6">Travel</asp:ListItem>
             </asp:ListBox>--%>
              <evy:ScrollableListBox ID="lstEntryType" RepeatColumns="1" 
                       RepeatDirection="Vertical" runat="server" Height="72px" 
                       Width="200px" BorderStyle="Inset" ></evy:ScrollableListBox>
         </td>
         <td>
              <asp:ImageButton ID="btn_Submitt" runat="server" SkinID="ImgView" 
                            ValidationGroup="one" onclick="btn_Submitt_Click" /></td>
         </tr>
    </table>
    <div> <asp:LinkButton ID="btnExportExcel" runat="server" Font-Bold="true" 
                            onclick="btnExportExcel_Click" ForeColor="Navy">Excel&nbsp;format</asp:LinkButton></div>
    </div>
     <div class="tab_subheader" style="border-bottom:solid 1px Silver;width:100%;">View Reports</div>
                
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
    </form>
</body>
</html>
