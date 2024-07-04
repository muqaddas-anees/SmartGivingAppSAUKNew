<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="QuoteAndAssignSales.aspx.cs" Inherits="DeffinityAppDev.WF.DC.QuoteAndAssignSales" %>
<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagName="FlsTab" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
	 <%= Resources.DeffinityRes.ServiceDesk%> -  <label id="lblTitle" runat="server"></label>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
	 <nav class="navbar navbar-default" role="navigation" id="navTab">
    <uc2:FlsTab ID="flstab1" runat="server" />
        </nav>

      <div class="row">
        <div class="col-sm-6" >			
					<div class="xe-widget xe-counter" data-count=".num" data-from="0" data-to="99.9" data-suffix="%" data-duration="2">
						<div class="xe-icon">
							<a style="cursor:pointer;" runat="server" id="link_Assign_Sales_rep1"> <i class="fa-users"></i></a>
						</div>
						<div class="xe-label">
							<a style="font-size:large;"  id="link_Assign_Sales_rep2" runat="server">Assign Sales Rep</a>
						</div>
					</div>
					
				</div>	
        <div class="col-sm-6">
					<div class="xe-widget xe-counter xe-counte" data-count=".num" data-from="1" data-to="117" data-suffix="k" data-duration="3" data-easing="false">
						<div class="xe-icon">
							<a style="cursor:pointer;" runat="server" id="link_create_Quotation1"> <i class="fa-wrench"></i> </a>
						</div>
						<div class="xe-label">
								<a style="font-size:large;"  id="link_create_Quotation2" runat="server">Create Quotation</a>
						</div>
					</div>
				
				</div>
        
   </div>


</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
