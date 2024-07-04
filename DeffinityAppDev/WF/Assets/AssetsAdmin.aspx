<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="AssetsAdmin" Title="Assets Admin" Codebehind="AssetsAdmin.aspx.cs" EnableEventValidation="false" %>


<%@ Register Src="controls/AdminAssetsMove.ascx" TagName="AdminAsset1" TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    Products Database
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    Product 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
     <div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
          <li><a id="link_setup" href="~/WF/Admin/AdminDropDown.aspx?Panel=5" runat="server"
            target="_self">Setup</a></li>
    </ul>
   </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:HyperLink ID="btncontact" runat="Server" class="" Text="Back to Contacts" NavigateUrl="~/WF/CustomerAdmin/PortfolioContacts.aspx"><i class="fa fa-arrow-left"></i> Back to Contacts</asp:HyperLink>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group row">
          <div class="col-md-12">
    <uc1:AdminAsset1 id="AdminAsset1" runat="server">
    </uc1:AdminAsset1>
    </div>
        </div>
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();
        
    </script> 
</asp:Content>

