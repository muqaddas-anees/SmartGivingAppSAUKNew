<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_trCourseReoccurrence" Codebehind="trCourseReoccurrence.aspx.cs" %>

<%@ Register src="controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc1:trainingtabs ID="TrainingTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script language="javascript" type="text/javascript">
    function MutExChkList(chk) {
        var chkList = chk.parentNode.parentNode.parentNode;
        var chks = chkList.getElementsByTagName("input");
        for (var i = 0; i < chks.length; i++) {
            if (chks[i] != chk && chk.checked) {
                chks[i].checked = false;
            }
        }
    }

</script>
 <table class="data_carrier" width="100%" cellpadding="0" cellspacing="0" border="0">
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
  <div class="tab_subheader" style="border-bottom:solid 1px Silver;width:98%;">
      Course Re-Occurrence</div>
      <div>
      <div>
       <asp:ValidationSummary ID="validSummryUpdate" runat="server" ValidationGroup="Group3" ShowSummary="true" />
      </div>
      <table >
  <tr> <td>Course reoccurres:</td><td>
      <asp:TextBox ID="txtCourseReoccurs" runat="server" Width="100px"></asp:TextBox>
      <asp:RangeValidator ID="rngCO" runat="server" 
          ErrorMessage="Enter course reoccurs between 1 and 12" 
          ControlToValidate="txtCourseReoccurs" Display="None" MaximumValue="12" 
          MinimumValue="0" ValidationGroup="Group3" Type="Integer"></asp:RangeValidator>
      </td>
      <td>Reoccurrence Frequency:</td><td>
          <asp:DropDownList ID="ddlReoccursFrequencey" runat="server" Width="150px">
          </asp:DropDownList>
      </td></tr>
      <tr><td align="left">On Which Day:</td><td>
          <asp:CheckBoxList ID="chkDays" runat="server" RepeatDirection="Horizontal"  >
          <asp:ListItem Value="Monday">Mon</asp:ListItem>
          <asp:ListItem Value="Tuesday">Tues</asp:ListItem>
          <asp:ListItem Value="Wednesday">Wed</asp:ListItem>
          <asp:ListItem Value="Thursday">Thu</asp:ListItem>
            <asp:ListItem Value="Friday">Fri</asp:ListItem>
            <asp:ListItem Value="Saturday">Sat</asp:ListItem>
            <asp:ListItem Value="Sunday">Sun</asp:ListItem>
          
          </asp:CheckBoxList>
      </td><td>Until Date:</td><td> <asp:TextBox ID="txtUntilDate" runat="server" Width="100px"></asp:TextBox>
                        <asp:Image ID="imgUntilDate" runat="server" ImageAlign="Middle" SkinID="Calender" />
                        <ajaxToolkit:CalendarExtender PopupButtonID="imgUntilDate" TargetControlID="txtUntilDate"
                        runat="server" CssClass="MyCalendar"  ID="CalendarExtender1"></ajaxToolkit:CalendarExtender>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please enter end date" ValidationGroup="Group3" ControlToValidate="txtUntilDate"
                        Display="None" ></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="RegularExpressionValidator6" runat="server" ErrorMessage="Please enter valid date" Display="None" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                             ControlToValidate="txtUntilDate" ValidationGroup="Group3"></asp:RegularExpressionValidator></td></tr>
      <tr><td align="left"></td><td>
          <asp:ImageButton ID="imgAdd" runat="server" SkinID="ImgAdd" 
              onclick="imgAdd_Click" />
      </td><td>&nbsp;</td><td> &nbsp;</td></tr>
      <tr><td colspan="4"></td></tr>
  </table>
      </div>
  </td>
  </tr>
  </table>
</asp:Content>


