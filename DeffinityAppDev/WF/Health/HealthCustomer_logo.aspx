<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Customer_logo" Codebehind="HealthCustomer_logo.aspx.cs" %>

<%@ Register Src="~/WF/CustomerAdmin/controls/PortfolioDdlCtr.ascx" TagName="PortfolioDdlCtr" TagPrefix="uc2" %>
<%@ Register Src="~/WF/CustomerAdmin/controls/PortfolioMenuTab.ascx" TagName="PortfolioMenuTab" TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.HealthChecks%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     Select Logo on Email Notification - <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" Runat="Server">
    <asp:HyperLink runat="Server" NavigateUrl="~/WF/Health/HealthCheckSchedule.aspx">
<i class="fa fa-arrow-left"></i>Return to Health check</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        

<div class="form-group">
      <div class="col-md-12">
           <asp:Label ID="lblMsg" ForeColor="Green" ClientIDMode="Static" runat="server"></asp:Label>
	</div>

</div>
    

<div class="form-group">
      <div class="col-md-12">
           <asp:RadioButtonList ID="rblist" runat="server" RepeatDirection="Vertical">
                                 <asp:ListItem Text="Apply Instance Logo to the Email" Value="1"></asp:ListItem>
                                 <asp:ListItem Text="Apply Customer Logo to the Email" Value="2"></asp:ListItem>
                             </asp:RadioButtonList>
	</div>

</div>
    

<div class="form-group">
      <div class="col-md-12">
          <asp:Button ID="Btnsave" runat="server" Text="Submit" OnClick="Btnsave_Click" />
	</div>

</div>
    
      
</asp:Content>


