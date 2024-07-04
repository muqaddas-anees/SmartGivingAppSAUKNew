<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="CCApproval" Title="Change Control - Approval" Codebehind="CCApproval.aspx.cs" %>

<%@ Register Src="~/WF/Resource/controls/MyProjectsTab.ascx" TagName="ProjectStatus" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ChangeControl%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
 Change Control - Approval
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:ProjectStatus ID="ProjectStatus1" runat="server" EnableViewState="false" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:GridView ID="gridChanges" runat="server" AutoGenerateColumns="false" 
        DataKeyNames="ID" onrowcommand="gridChanges_RowCommand" Width="100%">
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Requester" HeaderStyle-CssClass="header_bg_l">
                <ItemTemplate>
                    <a href="mailto:<%#Eval("RequesterEmailID")%>">
                        <%#Eval("RequesterName")%>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <%#Eval("Title")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <%#Eval("ChangeDescription")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Justification">
                <ItemTemplate>
                    <%#Eval("Justification")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Raised">
                <ItemTemplate>
                    <%#Eval("DateRaised")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Release Date">
                <ItemTemplate>
                    <%#Eval("TargetReleaseDate")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Resource Impact">
                <ItemTemplate>
                    <%#Eval("ResourceImpact")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                <ItemTemplate>
                    <asp:LinkButton ID="btnApprove" runat="server" CommandArgument='<%#Eval("ID")%>' Text="Approve" CommandName="Approve" />
                    /<asp:LinkButton ID="btnDeny" runat="server" CommandArgument='<%#Eval("ID")%>' Text="Deny" CommandName="Deny" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No changes are found..
        </EmptyDataTemplate>
    </asp:GridView>
    



    
</asp:Content>
