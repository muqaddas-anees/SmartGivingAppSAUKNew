<%@ Page Title="" Language="C#" MasterPageFile="~/MainTab.master" AutoEventWireup="true" Inherits="Default3" EnableEventValidation="false" Codebehind="FLSForm.aspx.cs" %>
<%@ Register Src="~/WF/DC/controls/FLS.ascx" TagName="FC" TagPrefix="uc1" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>
<%@ Register Src="~/WF/DC/controls/QuickSearchCtrl.ascx" TagName="QuickSearchCtrl" TagPrefix="QSCtrl"%>
<%@ Register src="~/WF/DC/controls/PortfolioDdlCtr.ascx" tagname="PortfolioDdlCtr" tagprefix="uc2" %>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= sessionKeys.JobDisplayName%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<%--<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
  
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <style>
       
.modal {
    background: rgba(0,0,0,0.5);
}
    </style>
   <%-- <nav class="navbar navbar-default" role="navigation" id="navTab">--%>
    <uc2:FlsTab ID="flstab1" runat="server" />
        <%--</nav>--%>
    
           <%-- <div class="card shadow-sm">
						<div class="card-header">
							<h3 class="panel-title form-inline"> 
                                 <label id="lblTitle" runat="server"></label>  
     <uc2:portfolioddlctr ID="PortfolioDdlCtr1" runat="server" Visible="false" /></h3>
							<div class="card-toolbar">
								
                                <a id ="link_return" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to  <%= Resources.DeffinityRes.ServiceDesk%></a>
							</div>
						</div>
						<div class="panel-body">
                            <div class="row">
             
                                </div>
                </div>
            </div>--%>
        



   <uc1:FC ID="FlsForm" runat="server"/>
</asp:Content>


