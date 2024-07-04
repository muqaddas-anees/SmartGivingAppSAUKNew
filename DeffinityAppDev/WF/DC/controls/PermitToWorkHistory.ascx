<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_PermitToWorkHistory" Codebehind="PermitToWorkHistory.ascx.cs" %>
<table width="100%">
<tr>
<td><div class="tab_subheader" style="border-bottom:solid 1px Silver;width:98%;">History</div></td>
</tr>
<tr>
<td>
<asp:UpdateProgress ID="updateprgs" runat="server" AssociatedUpdatePanelID="updatepnl_delivery">
<ProgressTemplate>
<img alt="loading.." src="../media/ico_loading.gif" />

</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="updatepnl_delivery" runat="server">
<ContentTemplate>
<div>
<asp:Label ID="lblmsg_history" runat="server" ForeColor="Green"></asp:Label>
<asp:HiddenField ID="hid" runat="server" />
</div>
        <asp:Panel ID="pnlhistory" runat="server" Height="300px" ScrollBars="Auto">
                          <asp:DataList ID="dlstHistory" runat="server"                             
                              GridLines="Horizontal" onitemdatabound="dlstHistory_ItemDataBound" Width="100%">
                          <ItemTemplate>                     
                           <table>
                          <%--<tr>
                            <td width="150px"> Date and Time of Change:</td><td><asp:Label ID="lbldate" runat="server"></asp:Label></td></tr>
                            <tr><td><asp:PlaceHolder ID="placeholder1" runat="server"></asp:PlaceHolder></td><td><asp:PlaceHolder ID="placeholder2" runat="server"></asp:PlaceHolder></td></tr>
                  
                        <tr><td>Modified By:</td><td> <asp:Label ID="lblmby" runat="server"></asp:Label></td></tr>--%>
                        <tr>
                            <td width="230px" id="mDate" runat="server"> Date and Time of Change:</td><td width="230px" id="cDate" runat="server">Created On:</td><td><asp:Label ID="lbldate" runat="server"></asp:Label>&nbsp;
                            <asp:CheckBox ID="chk_visibility" runat="server" AutoPostBack="true" Text="Visible to Customer" OnCheckedChanged="chk_visibility_CheckedChanged" />
                            </td>
                             <td>
                            <asp:HiddenField ID="h_callid" runat="server" />
                            <asp:HiddenField ID="h_date" runat="server" />
                              </td>
                            </tr>
                            <tr><td><asp:PlaceHolder ID="placeholder1" runat="server"></asp:PlaceHolder></td><td><asp:PlaceHolder ID="placeholder2" runat="server"></asp:PlaceHolder></td></tr>
                  
                        <tr><td id="mBy" runat="server">Modified By:</td><td width="230px" id="cBy" runat="server">Created By</td><td> <asp:Label ID="lblmby" runat="server"></asp:Label></td></tr>
                         
                          </table>      
                          </ItemTemplate>
                          <FooterTemplate>
<asp:Label Visible='<%#bool.Parse((dlstHistory.Items.Count==0).ToString())%>' runat="server" ID="lblNoRecord" Text="No History Found!"></asp:Label>
</FooterTemplate>
                          </asp:DataList></asp:Panel></td>
                        
</ContentTemplate>
</asp:UpdatePanel>
 </td>
</tr>
</table>
