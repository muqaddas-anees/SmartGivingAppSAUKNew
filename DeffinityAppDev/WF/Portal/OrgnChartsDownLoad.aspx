<%@ Page Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" Inherits="OrgnCharts" Title="Customer Organization Charts" Codebehind="OrgnChartsDownLoad.aspx.cs" %>
<%--<%@ Register Src="controls/CustomerHomeTabs.ascx" TagName="CustomerTabs" TagPrefix="uc3" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerPortal%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.OrganizationCharts%> 
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" Runat="Server">
    <%--<uc3:CustomerTabs ID="CustomerTabs1" runat="server" />--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    
<asp:Panel ID="Panel1" runat="Server" Width="100%" Height="100%" ScrollBars="auto">
        <asp:GridView ID="gridFlowChart" runat="server" AutoGenerateColumns="false" DataKeyNames="ID"
            EmptyDataText="No Charts Available" Width="100%" OnRowCommand="gridFlowChart_RowCommand"
            OnRowDeleting="gridFlowChart_RowDeleting">
            <Columns>
                <asp:TemplateField HeaderText="Reference">
                    <ItemTemplate>
                        <asp:Label ID="lblReference" runat="Server" Text='<%#Eval("Reference") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Title")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Version" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:Label ID="lblVersion" runat="server" Text='<%#Eval("Version")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="Server" Text='<%#Eval("Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="top">
                    <ItemTemplate>
                        <asp:LinkButton ID="ImageButton1" runat="Server" SkinID="BtnLinkDelete"
                            AlternateText="Download file" CausesValidation="false" CommandName="DownLoad"
                            CommandArgument='<%#Eval("ID")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
    </asp:Panel>

   
</asp:Content>

