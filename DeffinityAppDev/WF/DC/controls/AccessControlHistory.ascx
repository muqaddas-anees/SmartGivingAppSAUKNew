<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_AccessControlHistory" Codebehind="AccessControlHistory.ascx.cs" %>


<div class="form-group row">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.History%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

<table width="100%">
<tr>
<td>
<asp:UpdateProgress ID="updateprgs" runat="server" AssociatedUpdatePanelID="updatepnl_delivery">
<ProgressTemplate>

    <asp:Label runat="server" SkinID="Loading"></asp:Label>

</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="updatepnl_delivery" runat="server" >
<ContentTemplate>
<div>
<asp:Label ID="lblmsg_history" runat="server" ForeColor="Green"></asp:Label>
<asp:HiddenField ID="hid" runat="server" />
</div>
        <asp:Panel ID="pnlhistory" runat="server" Height="300px" ScrollBars="Auto">
                          <asp:DataList ID="dlstHistory" runat="server" 
                              Width="100%" 
                              GridLines="Horizontal" onitemdatabound="dlstHistory_ItemDataBound">
                          <ItemTemplate>                     
                           <table width="100%">
                           <tr id="trdata" runat="server">
                            <td width="230px"> Visitor Name:</td><td><asp:Label ID="lblvname" runat="server"></asp:Label></td></tr>
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
                  
                        <tr><td id="mBy" runat="server">Modified By:</td><td width="230px" id="cBy" runat="server">Created By</td><td> <asp:Label ID="lblmby" runat="server"></asp:Label></td>
                        </tr>
                         
                          </table>      
                          </ItemTemplate>
                          <FooterTemplate>
<asp:Label Visible='<%#bool.Parse((dlstHistory.Items.Count==0).ToString())%>' runat="server" ID="lblNoRecord" Text="No History Found!"></asp:Label>
</FooterTemplate>
                          </asp:DataList></asp:Panel>
                                                 
</ContentTemplate>
</asp:UpdatePanel>
 </td>
</tr>
</table>