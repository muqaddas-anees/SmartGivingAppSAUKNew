<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_PortfolioContactsActivities" Codebehind="PortfolioContactsActivities.ascx.cs" %>
<asp:Panel ID="Panel1" runat="server">
<table>
       <tr>
      <%-- <td><asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ActSubmtCntrl" ForeColor="Red" DisplayMode="List" Enabled="true" ShowSummary="false" />
</td>--%>
<td><asp:Label ID="lblnotesErrMsg" runat="server" Visible="false" ForeColor="Red"></asp:Label> </td>
       </tr> 
        <tr>
        <td>Subject :</td>
        <td><asp:TextBox ID="txttasksubject" runat="server" TextMode="MultiLine" ValidationGroup="ActSubmtCntrl" ></asp:TextBox> 
            
           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Please enter the Subject" ControlToValidate="txttasksubject" ValidationGroup="ActSubmtCntrl"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
        <td>Activity Type :</td>
        <td><asp:Label ID="lblacitity" runat="server"></asp:Label> </td>
        </tr>
        <tr>
        <td>Status :</td>
        <td><asp:DropDownList ID="drpstatus" runat="server">
                <asp:ListItem Value="1" Text ="Not Started" Selected="True"></asp:ListItem>
                <asp:ListItem Value="2" Text ="Started" ></asp:ListItem>
                <asp:ListItem Value="3" Text ="Pending"></asp:ListItem>
                <asp:ListItem Value="4" Text="Completed"></asp:ListItem>
            </asp:DropDownList> </td>
        </tr>
        <tr>
        <td>Due Date :</td>            
        <td><asp:TextBox ID="txtTaskDate" runat="server"></asp:TextBox><asp:Image ID="Image1" runat="server" SkinID="Calender" ToolTip="Pick a date" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTaskDate"
       ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
       ValidationGroup="ADDActivity" Text="*" ErrorMessage="Please enter valid Due Date"></asp:RegularExpressionValidator>
       <ajaxToolkit:CalendarExtender ID="CalendarExtenderDueDate" runat="server" TargetControlID="txtTaskDate"  CssClass="MyCalendar" PopupButtonID="Image1"></ajaxToolkit:CalendarExtender>

 </td>
 </tr>
 <tr>
 <td>Owner name :</td>
 <td><asp:DropDownList ID="drpContacts" runat="server" ></asp:DropDownList> </td>
 </tr>
 <tr>
 <td style="text-align:center"><asp:ImageButton ID="btnADD" runat="server"  ValidationGroup="ActSubmtCntrl"  SkinID="ImgAdd" OnClick="btnADD_Click"  />
    &nbsp;<asp:ImageButton id="btnCancel" runat="server" SkinID="ImgCancel" CausesValidation="False" OnClick="btnCancel_Click" /> </td>
<asp:HiddenField ID="hiddencontactid" runat="server" />
        </tr>
        
       </table>

</asp:Panel>
