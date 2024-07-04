<%@ Page Language="C#" AutoEventWireup="true" Inherits="MailTemplates_HelpCenterMailDetails" Title="Details Mail Page" Codebehind="HelpCenterMailDetails.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table width="100%">
            <tr>
                <td colspan="2" align="right">
                    <img id="imgDeffinityLogo" runat="server" alt="Deffinity Logo" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DetailsView ID="Maildetails" runat="server" EnableViewState="False" DataSourceID="DS_MailDetails"
                        CellPadding="4" ForeColor="#333333" GridLines="None" 
                        AutoGenerateRows="False" HeaderText="Details of the Mail">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <Fields>
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="ContactNumber" HeaderText="Contact Number" />                            
                            <asp:BoundField DataField="SenderEmail" HeaderText="Sender's Email Address" />
                            <asp:BoundField DataField="Details" HeaderText="Details" />
                            <asp:BoundField DataField="DateRaised" HeaderText="Date Raised" DataFormatString="{0:d}" HtmlEncode="false" />
                            <%--<asp:TemplateField HeaderText="Date Time" >
                                <ItemTemplate>
                                    <asp:Literal ID="litTimeRequested" runat="server" Text='<%#Eval("DateRaised","{0:d}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Fields>
                        <HeaderStyle BackColor="#E9EFEF" Font-Bold="True" ForeColor="#666666" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:DetailsView>
                    <asp:ObjectDataSource ID="DS_MailDetails" runat="server" OldValuesParameterFormatString="original_{0}"
                        TypeName="Deffinity.HelpCenterManagers.HelpCenterManager" SelectMethod="GetMailDetails">
                    </asp:ObjectDataSource>
                </td>
                <td valign="top">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>



