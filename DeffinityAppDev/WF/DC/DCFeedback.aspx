<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="DCFeedback.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DCFeedback" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
     Service Desk
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <uc2:FlsTab ID="flstab1" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
     <label id="lblTitle" runat="server">Feedback</label> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <a id ="link_return" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to Ticket Journal</a>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    

    <asp:GridView ID="gridFeedback" runat="server">
        <Columns>
              <asp:TemplateField HeaderText="Ticket ref" Visible="false">
                <EditItemTemplate>
                    <asp:TextBox ID="txtTicket1" runat="server" Text='<%# Bind("CallRef") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblTicket" runat="server" Text='<%# Bind("CallRef") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Customer">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CustomerName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date and Time" ItemStyle-HorizontalAlign="Right">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("CreatedDate") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("CreatedDate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="How satisfied are you with the Service Provider Technician?">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("likelyService") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("likelyService") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="How satisfied are you with LHG customer service you spoke with?">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Worksatisifaction") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Worksatisifaction") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="How satisfied are you with the customer portal to submit your claim?">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Rating") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Rating") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Improvement">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Didwell") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Didwell") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="How satisfied are you with your overall claim experience?">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("recommend") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("recommend") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Additional Comments">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("servicefeedback") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("servicefeedback") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("StatusName") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("StatusName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
GridResponsiveCss();
 </script> 
</asp:Content>
