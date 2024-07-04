<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_FlsHistory"  Codebehind="FlsHistory.ascx.cs" %>

<%if (!Request.RawUrl.ToLower().Contains("flscustomer")) { %>
<style>
    .page-container .main-content {
    display: table-cell;
    position: relative;
    z-index: 1;
    padding: 5px;
    padding-bottom: 0;
    vertical-align: top;
    background-color:white;
    /* word-break: break-word; */
}
        .page-container .main-content .page-title {
            background: #f8f8f8;
            margin: -30px;
            /* margin-bottom: 30px;
    padding: 20px 0; */
            -webkit-box-shadow: 0 1px 0 rgba(0,1,1,.08), inset 0 1px 0 #ededed;
            -moz-box-shadow: 0 1px 0 rgba(0,1,1,.08), inset 0 1px 0 #ededed;
            box-shadow: 0 1px 0 rgba(0,1,1,.08), inset 0 1px 0 #ededed;
        }
</style>
<%} %>

<div class="form-group row">
<asp:UpdateProgress ID="updateprgs" runat="server" AssociatedUpdatePanelID="updatepnl_delivery">
<ProgressTemplate>
<asp:Label runat="server" SkinID="Loading" />

</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="updatepnl_delivery" runat="server" UpdateMode="Always">
<ContentTemplate>
<div>
<asp:Label ID="lblmsg_history" runat="server" SkinID="GreenBackcolor"></asp:Label>
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
                            <td width="230px" id="mDate" runat="server"> Date and Time of Change:</td>
                            <td width="230px" id="cDate" runat="server">Created On:</td>
                            <td><asp:Label ID="lbldate" runat="server"></asp:Label> &nbsp;<asp:CheckBox ID="chk_visibility" runat="server" AutoPostBack="true" Text="Visible to Customer" OnCheckedChanged="chk_visibility_CheckedChanged"  /></td>

                            <td>
                            <asp:HiddenField ID="h_callid" runat="server" />
                            <asp:HiddenField ID="h_date" runat="server" />
                            </td>
                              
                            </tr>
                            <tr><td><asp:PlaceHolder ID="placeholder1" runat="server"></asp:PlaceHolder></td><td><asp:PlaceHolder ID="placeholder2" runat="server"></asp:PlaceHolder></td></tr>
                  
                        <tr><td id="mBy" runat="server">Modified By:</td>
                        <td width="230px" id="cBy" runat="server">Created By</td>
                        <td> <asp:Label ID="lblmby" runat="server"></asp:Label></td></tr>
                         
                          </table>  
                              
                          </ItemTemplate>
                          <FooterTemplate>
<asp:Label Visible='<%#bool.Parse((dlstHistory.Items.Count==0).ToString())%>' runat="server" ID="lblNoRecord" Text="No History Found!"></asp:Label>
</FooterTemplate>
                          </asp:DataList></asp:Panel>
                          
</ContentTemplate>
</asp:UpdatePanel>
</div>