<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_CART" Codebehind="CART.ascx.cs" %>
<div style="width: 100%;vertical-align:top;" >
    <asp:Repeater ID="rptCart" runat="server" DataSourceID="SqlDataSource1" OnItemCommand="rptCart_ItemCommand"
        OnItemDataBound="rptCart_ItemDataBound">
        <HeaderTemplate>
        <table width="100%" border="0" cellspacing="1" cellpadding="0" >
                <tr class="tab_header" style="font-weight:bold">
              
                    <td width="250px" align="center" class="header_bg_l" >
                        Item
                    </td>
                    <td width="50px" align="center">
                        Qty
                    </td>
                    <td width="70px" align="center">
                    Unit Price
                    </td>
                    
                    <td width="100px" align="center"  >
                        Total
                    </td>
                    <td width="100px" align="center"  >
                        Units
                    </td>
                     <td width="100px" align="center"  >
                        Total Units
                    </td>

                    <td  width="30px" class="header_bg_r">&nbsp;</td>
                   
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr style="height: 15px" class="odd_row">
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "Item")%>
                </td>
                <td align="right">
                    <asp:TextBox ID="txtqty" Style="text-align:right"  CssClass="text" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>' Width="35px"></asp:TextBox><asp:RegularExpressionValidator Id="regexno" runat="server" ControlToValidate="txtqty" ValidationExpression="^[0-9]*" ErrorMessage="*" ForeColor="Red" ValidationGroup="grpnum"></asp:RegularExpressionValidator>
                </td>
                <td align="right">
                    <%# DataBinder.Eval(Container.DataItem, "UnitPrice", "{0:f2}")%>
                </td>
                <td align="right">
                    <%# DataBinder.Eval(Container.DataItem, "Total", "{0:f2}")%>
                </td>
                 <td align="right">
                    <%# DataBinder.Eval(Container.DataItem, "UnitConsumption", "{0:f2}")%>
                </td>
                 <td align="right">
                    <%# DataBinder.Eval(Container.DataItem, "TotalUnits", "{0:f2}")%>
                </td>
                <td>
                    <asp:LinkButton ID="DelBut" runat="server" CommandName="Delete" CommandArgument='<%# Eval("ID") %>'
                        SkinID="BtnLinkDelete" ToolTip="Delete"></asp:LinkButton>
                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' Visible="false"></asp:Label>
                </td>
                
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="height: 15px" class="even_row">
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "Item")%>
                </td>
                <td align="right">
                    <asp:TextBox ID="txtqty" Style="text-align:right" CssClass="text" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>' Width="35px"></asp:TextBox><asp:RegularExpressionValidator Id="regexno" runat="server" ControlToValidate="txtqty" ValidationExpression="^[0-9]*" ErrorMessage="*" ForeColor="Red" ValidationGroup="grpnum"></asp:RegularExpressionValidator>
                </td>
                <td align="right">
                    <%# DataBinder.Eval(Container.DataItem, "UnitPrice", "{0:f2}")%>
                </td>
                <td align="right">
                    <%# DataBinder.Eval(Container.DataItem, "Total", "{0:f2}")%>
                </td>
                 <td align="right">
                    <%# DataBinder.Eval(Container.DataItem, "UnitConsumption", "{0:f2}")%>
                </td>
                <td align="right">
                    <%# DataBinder.Eval(Container.DataItem, "TotalUnits", "{0:f2}")%>
                </td>
                <td>
                    <asp:LinkButton ID="DelBut" runat="server" CommandName="Delete" CommandArgument='<%# Eval("ID") %>'
                        SkinID="BtnLinkDelete" ToolTip="Delete"></asp:LinkButton>
                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' Visible="false"></asp:Label>
                </td>
                
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            <tr style="background-color:#ecf6ef;font-weight:bold;">
                <td>
                  <asp:Label ID="lbltotal" runat="server" Text="Total" Font-Bold="true"></asp:Label>  
                </td>
                <td align="right">
                 <asp:Label ID="lbltqty" runat="server" Text='<%#TotalQuantity%>'></asp:Label>
                </td>
                <td>
                </td>
                <td align="right">
                  <asp:Label ID="lbltotalSP" runat="server" Text='<%#string.Format("{0:F2}", TotalSP)%>'></asp:Label>
                </td>
                <td>
                </td>
                <td align="right">
                  <asp:Label ID="lbltotalUnits" runat="server" Text='<%#string.Format("{0:F2}", TotalUnits)%>'></asp:Label>
                </td>
                <td>
                </td>
            </tr>
        </FooterTemplate>
    </asp:Repeater>
   
    
    </div> 
<div class="form-group row">
             <div class="col-md-12">
                  <asp:Button ID="btnupdate" runat="server" SkinID="btnUpdate"
                    OnClick="btnupdate_Click" ValidationGroup="grpnum" />
                    <asp:Button ID="btnRequestQuote" runat="server"  
                    SkinID="Request Quote" onclick="btnRequestQuote_Click" />
                    <asp:Button ID="btnProcessOrder" runat="server" 
                    SkinID="btnOrange" Text="Process Order" onclick="btnProcessOrder_Click" />
                <asp:Button ID="btnClear" runat="server" SkinID="btnClear" OnClick="btnClear_Click" />
</div>
</div>  
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    SelectCommand="DEFFINITY_VIEWCART" SelectCommandType="StoredProcedure">
    <SelectParameters>
            <asp:SessionParameter DefaultValue="00000000-0000-0000-0000-000000000000" Name="UserID" SessionField="cartID" Type="String" />
    </SelectParameters>
</asp:SqlDataSource>
