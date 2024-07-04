<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_trCRDashboard" Codebehind="trCRDashboard.aspx.cs" %>

<%@ Register src="controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>
<%@ Register Src="controls/dropdownView.ascx" TagName="DropDownList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
  <uc1:TrainingTabs ID="TrainingTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<style type="text/css">
div#tipDiv {
    padding:4px;
   
    color:#000; font-size:13px; line-height:1.2;
    background-color:#F5F5F5; border:1px solid #667295; 
    width:200px; 
}
</style>
<script src="../js/dw_event.js" type="text/javascript"></script>
<script src="../js/dw_viewport.js" type="text/javascript"></script>
<script src="../js/dw_tooltip.js" type="text/javascript"></script>
<script src="../js/dw_tooltip_aux.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
    function employeeDetails(s) {

        // alert(s)E1E5F1;
        var name,title,date,status
        var splitResult = s.split("$");

        name = splitResult[0].replace("_", " ");
        title = splitResult[1].replace("_", " ");
        date = splitResult[2].replace("_", " ");
        status = splitResult[3].replace("_", " ");
        status = status.replace("_", " ");
       // alert(status[1]);
        dw_Tooltip.defaultProps = {
            hoverable: true
        }

        dw_Tooltip.content_vars = {

        L2: '<b>Name:  </b>' + name + '</br><b>Course Title:  </b>' + title + '<br><b>Booking Date:  </b>' + date + '</br><b>Status:  </b>' + status
        }
    }
    function emp() {
        dw_Tooltip.defaultProps = {
            hoverable: true
        }

        dw_Tooltip.content_vars = {

            L2: 'cgjdskdskjfdskfjbd dfjdsfkjfb jdfkjdfdjf jfjdfdjfdf fddfds'
        }
    }

</script>

 <table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>    
      <td>
          <h1 class="section2">
              <span>
                  <label id="lblTitle" runat="server">
                  </label>
              </span>
          </h1>
          
      </td>
  </tr>
  <tr>    
    <td class="p_section2 data_carrier_block" valign="top">
   <div style="float:right;width:270px">
    <uc2:DropDownList ID="dropDownList" runat="server" />
    </div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td valign="top" >

<div class="tab_subheader" style="border-bottom:solid 1px Silver;width:95%;">
Operator Validation Report 
</div>
<div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
</div>
<div>
<label id="lblDepartment" runat="server" style="font-weight:bold">Department</label>
    <asp:DropDownList ID="ddlDepartment" runat="server" Width="150px">
    </asp:DropDownList>
    
    <label id="Label1" runat="server" style="font-weight:bold">Course</label>
     <asp:DropDownList ID="ddlCourse" runat="server" Width="250px">
    </asp:DropDownList>
     <label id="Label2" runat="server" style="font-weight:bold">From Date</label>
    <asp:TextBox ID="txtFromDate" runat="server" Width="100px"></asp:TextBox><asp:Image ID="imgFromDate"
        runat="server" SkinID="Calender" />
    
    <ajaxToolkit:CalendarExtender PopupButtonID="imgFromDate" TargetControlID="txtFromDate"
                        runat="server" CssClass="MyCalendar"  ID="CalendarExtender1"></ajaxToolkit:CalendarExtender>
    <asp:RegularExpressionValidator ID="REV1" runat="server" ErrorMessage="Please enter valid from date"
                    ControlToValidate="txtFromDate" ValidationGroup="Group1" 
        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" 
        Display="None"></asp:RegularExpressionValidator>
      <label id="Label3" runat="server" style="font-weight:bold">To Date</label>
    <asp:TextBox ID="txtToDate" runat="server" Width="100px"></asp:TextBox>
      <asp:Image ID="imgToDate"
        runat="server" SkinID="Calender" />
    <ajaxToolkit:CalendarExtender runat="server"  CssClass="MyCalendar" PopupButtonID="imgToDate" TargetControlID="txtToDate" >
    </ajaxToolkit:CalendarExtender>
  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid to date"
                    ControlToValidate="txtToDate" ValidationGroup="Group1" 
        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" 
        Display="None"></asp:RegularExpressionValidator>
    <asp:CompareValidator ID="CompareValidator1" runat="server"  
        ControlToValidate="txtToDate" ControlToCompare="txtFromDate"
    ErrorMessage="To date must be greater then from date" Type="Date" 
        Display="None" Operator="GreaterThan" ValidationGroup="Group1"></asp:CompareValidator>
    <asp:ImageButton ID="btnView" runat="server" SkinID="ImgView" 
         ValidationGroup="Group1" onclick="btnView_Click" />
</div>
<div style="height:20px">&nbsp;</div>
<div>
<asp:Panel ID="Panel1" Width="1150px" Height="700px" ScrollBars="Auto" runat="server">
    <asp:Literal ID="ltlCourseReOccurrence" runat="server"></asp:Literal>
    </asp:Panel>
</div>
</td>
</tr>
</table>
</td>
</tr>
    </table>

</asp:Content>


