<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_BudgetSavingGrid" Codebehind="BudgetSavingGrid.ascx.cs" %>
   <h5 class="sec_header"><%= Resources.DeffinityRes.QtySaving%> </h5>
<hr />
    <asp:GridView ID="GridSavingRecord" runat="server" AutoGenerateColumns="false" Width="100%" EmptyDataText="No records found">
        <Columns>
            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Item%>" HeaderStyle-CssClass="header_bg_l">
                <ItemTemplate>
                    <asp:Label ID="lblItem" runat="server" Text='<%#Bind("BOMIdName") %>'></asp:Label>
                    <asp:Label ID="LblId" Visible="false" runat="server" Text='<%#Bind("BOMId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Budget%>" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblBudget" runat="server" Text='<%#Bind("Budget","{0:F2}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ActualUsed%>" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblActualUsed" runat="server" Text='<%#Bind("Actual","{0:F2}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Saving%>" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblSaving" runat="server" Text='<%#Bind("Saving","{0:F2}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField HeaderText="<%$ Resources:DeffinityRes,CostSaving%>" DataField="CostSaving" HeaderStyle-CssClass="header_bg_r"
                                                     DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
              <%--<asp:TemplateField HeaderText="Cost Saving" HeaderStyle-CssClass="header_bg_r" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="LblCostSaving" runat="server" Text='<%#Bind("CostSaving")%>' DataFormatString="{0:F2}"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>
    </asp:GridView>
