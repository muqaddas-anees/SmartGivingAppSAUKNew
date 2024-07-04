<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="PortfolioCustomerConfig" Title="Untitled Page" Codebehind="PortfolioCustomerConfig.aspx.cs" %>

<%--<%@ Register src="controls/PortfolioMenuTab.ascx" tagname="PortfolioMenuTab" tagprefix="uc1" %>--%>
<%@ Register src="controls/PortfolioDdlCtr.ascx" tagname="PortfolioDdlCtr" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
   <%-- <uc1:PortfolioMenuTab ID="PortfolioMenuTab1" runat="server" />--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerAdmin%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.PortalConfig%>  <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" />

</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
     
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="form-group row"><asp:Label ID="lblCustomerMsg" runat="server" ForeColor="Green" EnableViewState="false"></asp:Label> </div>
   

<div class="col-md-12 no-left-padding">
<div class="col-md-4 form-inline no-left-padding">
<asp:LinkButton ID="Update" Text="Enable" runat="server" Font-Bold="true" 
        onclick="Update_Click"></asp:LinkButton>&nbsp;&nbsp;<asp:LinkButton ID="btnDisable" Text="Disable" runat="server" Font-Bold="true" onclick="btnDisable_Click"></asp:LinkButton>
</div>
<div class="col-md-5 pull-right">
<asp:Button ID="btnApplyAllCustomers" runat="server" 
       SkinID="btnDefault"  Text="Apply to all customers" onclick="btnApplyAllCustomers_Click" Font-Bold="true"></asp:Button>
</div>
</div>
        <div class="form-group row">
            <div class="col-md-9">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="ObjectDataSource1" Width="100%" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                        <asp:HiddenField ID="HID" runat="server" Value='<%# Bind("ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SectionName" HeaderText="Section Name" ItemStyle-Width="50%" />
                <asp:BoundField DataField="Visible" HeaderText="Visible" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnApply" Text="Apply this option to all customers" runat="server" CommandName="apply" CommandArgument='<%# Bind("SectionID") %>' SkinID="btnDefault" AlternateText="Apply this section to all customers" ToolTip="Apply this section to all customers"></asp:Button>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
               </div>
            </div>
        
        
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="CustomerConfig_Select" 
            TypeName="Deffinity.CustomerConfig.CustomerConfigManager">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="0" Name="PortfolioID" 
                    SessionField="PortfolioID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
     //grid_responsive();
     grid_responsive_display();
     $(window).load(function () {
         $("button:contains('Display all')").click(function (e) {
             e.preventDefault();
             $(".dropdown-menu li")
       .find("input[type='checkbox']")
       .prop('checked', 'checked').trigger('change');
         });
     });
    </script> 
</asp:Content>


