<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_TimesheetMonthEndExceptionfrm" Codebehind="TimesheetMonthEndException.aspx.cs" %>
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
   <script language="javascript" type="text/javascript">
       function ValidateAnswerRadio() {

           var list = document.getElementById('chkEntryType').childNodes;

           for (i = 0; i < list.length; i++)              

               if (list[i].checked) return true;

           return false;

       }
   </script>
   
     <div style="font-family:Verdana, Arial, Helvetica, sans-serif;font-size:13px;">
        <div style="padding-top:10px">&nbsp; </div>
       <div class="sec_header"> Month End Exception Report</div>
       <div> <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="one" />
                  
 <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txt_StartDate" ControlToValidate="txt_EndDate"
                        Display="none" Type="Date" Operator="GreaterThanEqual" ErrorMessage="<%$ Resources:DeffinityRes, startdatecantgrthenenddate%>" ValidationGroup="one" ></asp:CompareValidator>      
                        
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_StartDate"
                        Display="None" ErrorMessage="<%$ Resources:DeffinityRes, Entervaliddtindtfld%>" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one" >*</asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_EndDate"
                        Display="None" ErrorMessage="<%$ Resources:DeffinityRes, Plsentervaliddateindatefield%>" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one">*</asp:RegularExpressionValidator>
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="<%$ Resources:DeffinityRes, Label%>" Visible="False"></asp:Label></div>
           <div id="div2">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
               <tr>
               <td style="width:90px;">&nbsp;<%= Resources.DeffinityRes.Customer%></td>
               <td style="width:160px;"><asp:DropDownList ID="ddlCustomers" Width="150px" runat="server"
                AutoPostBack="false" OnSelectedIndexChanged="ddlCustomers_SelectedIndexChanged" 
                DataTextField="PortFolio" DataValueField="ID" 
                       DataSourceID="SqlDataSourceTitle2"  ></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_PermissionCustomer" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
               </td>
               <td style="width:90px;">&nbsp;<%= Resources.DeffinityRes.Team%></td>
               <td style="width:160px;"><asp:DropDownList ID="ddlTeam" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlTeam_SelectedIndexChanged"  >
                   </asp:DropDownList></td>
               <td style="width:90px;"><%= Resources.DeffinityRes.Resource%>&nbsp;</td>
               <td style="width:160px;">
                   <asp:DropDownList ID="ddlResource" runat="server" Width="150px"  >
                   </asp:DropDownList></td>
                   </tr>
                   <tr>
                    <td width="70px">
                        <%= Resources.DeffinityRes.StartDate%></td>
                    <td width="125px">
                        <asp:TextBox ID="txt_StartDate" runat="server" Width="65px" MaxLength="10">
                        </asp:TextBox>
                      
                        <asp:Image ID="Image2" runat="server" SkinID="Calender"  />
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender2"  runat="server"
                            PopupButtonID="Image2" TargetControlID="txt_StartDate" CssClass="MyCalendar" OnClientShown="calendarShown" OnClientHidden="calendarShown" >
                        </ajaxToolkit:CalendarExtender>
                       
                    </td>
                    <td width="70px">
                        <%= Resources.DeffinityRes.EndDate%></td>
                   <td width="125px">
                   
                        <asp:TextBox ID="txt_EndDate" runat="server" Width="65px" MaxLength="10" />
                       
                        <asp:Image ID="Image3" runat="server" SkinID="Calender"  />
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender3"  runat="server"
                            PopupButtonID="Image3" TargetControlID="txt_EndDate" CssClass="MyCalendar"  OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>
                        
                        </td>
                   <td width="125px">
                   
                       Type:</td>
                   <td width="125px">
                   
                        <evy:ScrollableListBox ID="chkEntryType" RepeatColumns="1" 
                       RepeatDirection="Vertical" runat="server" BorderStyle="Inset" Height="72px" 
                       Width="200px" ></evy:ScrollableListBox>
                        </td>
                        <td style="text-align:left;">
                            &nbsp;</td>
                </tr>
               <tr>
               <td > Order&nbsp;By</td>
               <td >
                    <asp:DropDownList ID="ddlSortOption" runat="server">
            <asp:ListItem Selected="True" Value="1">First Name</asp:ListItem>
            <asp:ListItem  Value="2">Surname</asp:ListItem>
            </asp:DropDownList> </td>
               <td>
        <asp:ImageButton ID="btn_Submitt" runat="server" ImageUrl="~/media/btn_view_report.gif" 
                            ValidationGroup="one" OnClick="btn_Submitt_Click"  />
                 </td>
               <td >
                 <asp:LinkButton ID="lnkButtonExcel" runat="server" Font-Bold="True" 
                       ForeColor="#004080" onclick="lnkButtonExcel_Click">Excel&nbsp;Export&nbsp;1</asp:LinkButton>
                   &nbsp;
                   </td>
               <td style="width:90px;">
                   <asp:LinkButton ID="lnkButtonExcel1" runat="server" Font-Bold="True" 
                       ForeColor="#004080" onclick="lnkButtonExcel1_Click">Excel&nbsp;Export&nbsp;2</asp:LinkButton>
                   </td>
               
                </tr>
                </table>
             
    </div>
    </div>
     

      <div class="tab_subheader" style="border-bottom:solid 1px Silver;width:100%;">View Reports</div>
                 <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
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
