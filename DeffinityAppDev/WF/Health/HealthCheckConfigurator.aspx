<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="HealthCheckConfigurator_page" Codebehind="HealthCheckConfigurator.aspx.cs" %>

<%@ Register Src="~/WF/CustomerAdmin/controls/PortfolioDdlCtr.ascx" TagName="PortfolioDdlCtr" TagPrefix="uc2" %>
<%@ Register Src="~/WF/CustomerAdmin/controls/PortfolioMenuTab.ascx" TagName="PortfolioMenuTab" TagPrefix="uc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.HealthChecks%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     Configure Fields - <uc2:portfolioddlctr ID="PortfolioDdlCtr1" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" Runat="Server">
    <asp:HyperLink runat="Server" NavigateUrl="~/WF/Health/HealthCheckSchedule.aspx">
<i class="fa fa-arrow-left"></i>Return to Health check</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     
<div class="form-group row">
      <div class="col-md-12">
           <asp:Label ID="LblMsg" runat="server" ClientIDMode="Static" ForeColor="Green"></asp:Label>
	</div>

</div>

    
<div class="form-group row">
      <div class="col-md-6">
            
                    
                     <asp:GridView ID="gridHcRecords" runat="server" OnRowDataBound="gridHcRecords_RowDataBound" ShowFooter="false">
                         <Columns>
                             <asp:TemplateField HeaderStyle-CssClass="header_bg_l" HeaderText="Name">
                                 <ItemTemplate>
                                     <asp:Label ID="LblId" runat="server" Text='<%# Bind("ID")%>' Visible="false"></asp:Label>
                                     <asp:Label ID="LblName" runat="server" Text='<%# Bind("FieldName")%>'></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-CssClass="header_bg_r" HeaderText="Visibility" ItemStyle-HorizontalAlign="Center">
                                 <ItemTemplate>
                                     <asp:Label ID="Lblvisibility" runat="server" Text='<%#Bind("visibility")%>' Visible="false"></asp:Label>
                                     <asp:CheckBox ID="checkvisibility" runat="server" />
                                 </ItemTemplate>
                             </asp:TemplateField>
                         </Columns>
                     </asp:GridView>
                
	</div>
    <div class="col-md-6">
        </div>

</div>

    
<div class="form-group row">
      <div class="col-md-12">
           <asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click" />
	</div>

</div>
     
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    GridResponsiveCss();
 </script> 

</asp:Content>


