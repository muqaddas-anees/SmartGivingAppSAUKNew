<%@ Page Language="C#" AutoEventWireup="true"
    Culture="en-GB" Inherits="MailTemplates_HealthCheckDetails" EnableViewState="false" Codebehind="HealthCheckDetails.aspx.cs" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Health Check Issue </title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td align="right">
                    <img id="imgDeffinityLogo" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlIssueText" runat="server">
            Dear
            <b>
            <asp:Literal ID="lblTeam" runat="server" EnableViewState="false" /></b>,
            <p>
                An issue has been reported for the health check
                <b>
                <asp:Literal ID="lblHealthCheckTitle" runat="server" EnableViewState="false" /></b>
                that was logged
                <b>
                <asp:Literal ID="lblDate" runat="server" EnableViewState="false" />. </b>&nbsp;&nbsp;
                </p>
                <p>Details of the issue are as follows:</p>
            ISSUE: <b> <asp:Literal ID="lblIssueDetails" runat="server" EnableViewState="false" /></b>
            <br />
        </asp:Panel>
        <br />
        <br />
        <asp:DetailsView ID="DetailsView1" runat="server" CellPadding="4" DataSourceID="objHealthCheckList"
            ForeColor="#333333" GridLines="None" Height="50px" 
            AutoGenerateRows="False" Width="100%">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Fields>
                <asp:TemplateField HeaderText="Health Check">
                    <ItemTemplate>
                        <asp:Literal ID="litHealthCheck" runat="server" Text='<%#Eval("HealthCheckTitle")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Raised">
                    <ItemTemplate>
                        <asp:Literal ID="litDateRaised" runat="server" Text='<%#Eval("DateRaised","{0:d}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Location">
                    <ItemTemplate>
                        <asp:Literal ID="litLocation" runat="server" Text='<%#Eval("LocationName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Team">
                    <ItemTemplate>
                        <asp:Literal ID="litTeam" runat="server" Text='<%#Eval("AssignedTeamName")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" Visible="false">
                    <ItemTemplate>
                        <asp:Literal ID="litStatus" runat="server" Text='<%#Eval("Status")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue Status">
                    <ItemTemplate>
                        <asp:Literal ID="litIssueStatus" runat="server" Text='<%#Eval("IssueStatus")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Notes">
                    <ItemTemplate>
                        <asp:Literal ID="litNotes" runat="server" Text='<%#Eval("Notes")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Fields>
        </asp:DetailsView>
        <asp:ObjectDataSource ID="objHealthCheckList" runat="server" TypeName="Health.DAL.HealthCheckListHelper"
            OldValuesParameterFormatString="original_{0}" SelectMethod="LoadHealthcheckByID"
            DataObjectTypeName="Health.Entity.HealthCheckList" DeleteMethod="Delete">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="0" Name="healthCheckID" QueryStringField="healthcheckid"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <br />
        <br />
        <asp:GridView ID="gridHealthCheckListItems" DataSourceID="objHealthCheckListItems" Width="100%"
            runat="server" CellPadding="4" EnableTheming="False" ForeColor="#333333" GridLines="Vertical" OnRowDataBound="gridHealthCheckListItems_RowDataBound" AutoGenerateColumns="false">
            <PagerSettings Visible="False" />
            <FooterStyle Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="HealthCheck" HeaderText="Health Check" SortExpression="HealthCheck" />
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%#getStatus(Eval("Status").ToString())%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="AssigneeName" HeaderText="Assigned To" SortExpression="AssigneeName" />
                <%--<asp:TemplateField HeaderText="Date Verified" Visible="false">
                    <ItemTemplate>
                        <asp:Literal ID="litDateCompleted" runat="server" Text='<%#convertDate(Eval("DateCompleted","{0:d}"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Y/N">
                    <ItemTemplate>
                        <asp:Literal ID="litChecked" runat="server" Text='<%#GetCheckedValue(Eval("IsChecked"))%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RAG" HeaderText="RAG" />
                <asp:BoundField DataField="Issues" HeaderText="Issues" SortExpression="Issues" />
                <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
            </Columns>
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
        <asp:ObjectDataSource ID="objHealthCheckListItems" runat="server" TypeName="Health.DAL.HealthCheckListItemsHelper"
            OldValuesParameterFormatString="original_{0}" SelectMethod="LoadAllHealthCheckListItems"
            DataObjectTypeName="Health.Entity.HealthCheckListItems" DeleteMethod="Delete">
            <SelectParameters>
                <asp:QueryStringParameter Name="healthCheckId" QueryStringField="healthcheckid" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
