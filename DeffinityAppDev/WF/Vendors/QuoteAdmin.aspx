<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="QuoteAdmin1" Codebehind="QuoteAdmin.aspx.cs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Admin%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.QuoteAdmin%> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:Panel ID="pnlAdminEntry" runat="server">
     <table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
         <tr>
             <td class="p_section3 data_carrier_block">
                 <table width="100%">
                     <tr>
                         <td colspan="3">
                         <asp:Label ID="lblmsg" runat='server' ForeColor="Red"></asp:Label>
                         <asp:ValidationSummary ID="valsum" runat='server' ForeColor="Red" ValidationGroup="ValAdd" />
                         <asp:HiddenField ID="HD_ID" runat="server" Value="0" />
                         </td>
                     </tr>
                     <tr>
                         <td style="width: 271px">
                         Customer :
                         </td>
                         <td colspan="2">
                         <asp:DropDownList ID="ddlcustomer" runat="server" Width="200px" AutoPostBack="true" 
                                 onselectedindexchanged="ddlcustomer_SelectedIndexChanged" ></asp:DropDownList>
                             <asp:RequiredFieldValidator ID="Req1" ControlToValidate="ddlcustomer" runat='server'
                                 ErrorMessage="Please select a customer" Text="*" InitialValue="0" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                     <tr>
                         <td style="width: 271px">
                             Quote Number Prefix :
                         </td>
                         <td style="width: 200px">
                             <asp:TextBox ID="txtprefix" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="Req2" ControlToValidate="txtprefix" runat='server'
                                 ErrorMessage="Please enter Prefix" Text="*" InitialValue="0" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
                         </td>
                         <td rowspan="2">
                             Standard Header :<br />
                             <asp:TextBox ID="txtheader" runat="server"  TextMode="MultiLine" Height="81px" Width="693px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="Req5" runat='server' ControlToValidate="txtheader" ErrorMessage="Please enter Header" Text="*" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                     <tr>
                         <td style="width: 271px">
                             Quote Start Point
                         </td>
                         <td style="width: 165px">
                             <asp:TextBox ID="txtstartpoint" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="Req3" runat='server' ControlToValidate="txtstartpoint"
                                 ErrorMessage="Please enter Start Point Quote" Text="*" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
                             <%--<asp:CompareValidator ID="cmp1" runat="server" ControlToCompare="txtstartpoint" ControlToValidate="txtstartpoint"
                                 Type="Integer" ErrorMessage="Please enter valid Start Point Quote" Text="*" ValidationGroup="ValAdd"></asp:CompareValidator>--%>
                             <asp:RegularExpressionValidator ID="RegEx2" runat="server" ControlToValidate="txtstartpoint" ValidationExpression="^\d+$"
                                     ErrorMessage="Please enter positive integers for Start point" Text="*" ValidationGroup="ValAdd">
                                 </asp:RegularExpressionValidator>    
                         </td>
                     </tr>
                     <tr>
                         <td style="width: 271px">
                         VAT Rate(%) :
                         </td>
                         <td style="width: 165px">
                             <asp:TextBox ID="txtvat" runat="server" Text="17.5" SkinID="Price"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="Req4" runat='server' ControlToValidate="txtvat" ErrorMessage="Please enter VAT"
                                 Text="*" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
                             <%--<asp:CompareValidator ID="cmp2" runat="server" ControlToValidate="txtvat" Type="Double"
                                 ErrorMessage="Please enter valid VAT" Text="*" ValidationGroup="ValAdd"></asp:ComparValidator>--%>
                                 <asp:RegularExpressionValidator ID="Regex1" runat="server" ControlToValidate="txtvat" ValidationExpression="^\d*\.?\d*$"
                                     ErrorMessage="Please enter valid VAT" Text="*" ValidationGroup="ValAdd">
                                 </asp:RegularExpressionValidator>    
                         </td>
                         <td rowspan="2">
                         Standard Footer :<br /> <asp:TextBox ID="txtfooter" runat="server" Height="81px"  TextMode="MultiLine" 
                                 Width="693px" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="Req6" runat='server' ControlToValidate="txtfooter" ErrorMessage="Please enter Footer" Text="*" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                     <tr>
                     <td style="width: 271px">Default Folder Name</td>
                     <td style="width: 165px"> <asp:TextBox ID="txtfolder" runat="server"></asp:TextBox></td>
                     </tr>
                     <tr>
                     <td style="width: 271px">Contact Name</td>
                     <td colspan="2"><asp:TextBox ID="txtcontactname" runat="server" Width="250px"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="Req7" runat="server" ControlToValidate="txtcontactname" ErrorMessage="Please enter Name" Text="*" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
                     </td>
                     </tr>
                      <tr>
                     <td style="width: 271px">Email</td>
                     <td colspan="2"><asp:TextBox ID="txtemail" runat="server" Width="250px" ></asp:TextBox>
                     <asp:RequiredFieldValidator ID="Req8" runat="server" ControlToValidate="txtemail" ErrorMessage="Please enter Email" Text="*" ValidationGroup="ValAdd"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="Reg1" runat='server' 
                             ControlToValidate="txtemail" 
                             ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                             ErrorMessage="Enter valid Email" Text="*" ValidationGroup="ValAdd"></asp:RegularExpressionValidator>
                     </td>
                     </tr>
                     <tr>
                     <td style="width: 271px">Contact Number</td>
                     <td style="width: 165px"><asp:TextBox ID="txtcontactno" runat="server" Width="154px"></asp:TextBox></td>
                     <td style="direction:rtl"><asp:Button ID="btnsubmit" runat="server" 
                             SkinID="btnSubmit" onclick="btnsubmit_Click" ValidationGroup="ValAdd" /></td>
                     </tr>
                 </table>
             </td>
         </tr>
     </table>
    </asp:Panel>
</asp:Content>

