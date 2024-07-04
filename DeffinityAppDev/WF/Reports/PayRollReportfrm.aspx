<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_PayRollReportfrm" Codebehind="PayRollReportfrm.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payroll Report</title>
    <link href="http://www.deffinity.com/dlite/media//favicon1.ico" rel="shortcut icon" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_frame.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_color_scheme.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_custom.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/ajaxtabs.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/customer_admin.css" />
</head>
<body>
    <form id="form1" runat="server">
     <div>
    <script language="javascript" type="text/javascript">
       var retval;
       function Setheight() {
           if (retval == null || retval == true) {
               retval = false;
               document.getElementById("div1").style.height = 180;
           }
           else {
               retval = true;
               document.getElementById("div1").style.height = 20;
           }
           
           return false;
       }
       function calendarShown(sender, args) {  Setheight(); }
   </script>
     <div style="font-family:Verdana, Arial, Helvetica, sans-serif;font-size:13px">
     <div style="padding-top:10px">&nbsp;</div>
     <div class="sec_header">Payroll Summary of Hours by type and Status</div>
     <div>
     <asp:ValidationSummary ID="V1" runat="server" ValidationGroup="one" />
                  
 <asp:CompareValidator ID="c1" runat="server" ControlToCompare="txt_StartDate" ControlToValidate="txt_EndDate"
                        Display="none" Type="Date" Operator="GreaterThanEqual" ErrorMessage="start date can not greater then end date" ValidationGroup="one" ></asp:CompareValidator>      
                        
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txt_StartDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one" >*</asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_EndDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one">*</asp:RegularExpressionValidator>
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
     </div>
      <div id="div1">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                
               <tr>
               <td >Customer:</td>
               <td ><asp:DropDownList ID="ddlCustomers" Width="150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomers_SelectedIndexChanged" ></asp:DropDownList>
               </td>
               <td >Team:</td>
               <td ><asp:DropDownList ID="ddlTeam" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlTeam_SelectedIndexChanged"  >
                   </asp:DropDownList></td>
               <td >Resource:</td>
               <td>
                   <asp:DropDownList ID="ddlcontractors" runat="server" Width="150px"  >
                   </asp:DropDownList></td>
                   
               <td>Sort By </td>    
               <td><asp:DropDownList ID="ddlSort" runat="server" Width="150px">
               <asp:ListItem Text="FirstName" Value="1">First Name</asp:ListItem>
               <asp:ListItem Text="LastName" Value="2">Surname</asp:ListItem>
               </asp:DropDownList> </td>
                </tr>
                
               <tr>
               <td >Start&nbsp;Date:</td>
               <td >
                        <asp:TextBox ID="txt_StartDate" runat="server" Width="65px" MaxLength="10">
                        </asp:TextBox>
                        <asp:Image ID="imgbtnenddate6" runat="server" SkinID="Calender" />
               </td>
               <td >End&nbsp;date:</td>
               <td >
                        <asp:TextBox ID="txt_EndDate" runat="server" Width="65px" MaxLength="10" />
                        <asp:Image ID="Image1" runat="server" SkinID="Calender"  /></td>
               <td>
        <asp:ImageButton ID="btn_Submitt" runat="server" ImageUrl="~/media/btn_view_reportt.gif" 
                            ValidationGroup="one" OnClick="btn_Submitt_Click"  />
                   </td>
               <td >
                   <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="true" 
                       ForeColor="Navy" onclick="LinkButton2_Click">Excel&nbsp;format1</asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="LinkButton3" runat="server" Font-Bold="true" 
                            ForeColor="Navy" onclick="LinkButton3_Click" >Excel&nbsp;format2</asp:LinkButton>
                   </td>
                </tr>
                </table>
             </div>
                <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                            PopupButtonID="imgbtnenddate6" TargetControlID="txt_StartDate" CssClass="MyCalendar" OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                            PopupButtonID="Image1" TargetControlID="txt_EndDate" CssClass="MyCalendar" OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>
                </div>
              
                
                </div>
    </div>
    
    <div class="clr"></div>
   <div class="tab_subheader" style="border-bottom:solid 1px Silver;width:100%;">View Report</div>
                
                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatepanel1">
    <ProgressTemplate>
    <img src="../media/ico_loading.gif" alt="loading" />
    </ProgressTemplate>
    </asp:UpdateProgress> 
                <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                <div style="z-index:1000;">
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
