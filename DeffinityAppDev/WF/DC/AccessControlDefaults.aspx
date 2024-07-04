<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="DC_AccessControlDefaults" Codebehind="AccessControlDefaults.aspx.cs" %>
<%@ Register src="controls/EmailFooter.ascx" tagname="EmailFooter" tagprefix="uc1" %>
<%@ Register src="controls/SecurityAccessMail.ascx" tagname="SecurityAccessMail" tagprefix="uc7" %>
<%@ Register Src="controls/AccessEmailId.ascx" TagName="AccessEmailId" TagPrefix="uc2" %>
<%@ Register Src="controls/AccessNo.ascx" TagName="AccessNo" TagPrefix="uc3" %>
<%@ Register Src="controls/PurposeOfVisit.ascx" TagName="PurposeOfVisit" TagPrefix="uc4" %>
<%@ Register Src="~/WF/Admin/controls/AdminDropdownTab.ascx" TagName="AdminDropdownTab" TagPrefix="uc5" %>
<%@ Register Src="controls/SADropDown.ascx" TagName="SA" TagPrefix="uc6" %>
<%@ Register Src="controls/StorageLocation.ascx" TagName="StorageLocation" TagPrefix="sl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
   <%= Resources.DeffinityRes.AccessControl%> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.AdminDropdownLists%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    
<div class="row">
          <div class="col-md-12">
               <uc6:SA ID="sa1" runat="server"  Visible="false"  />
	</div>
</div>
    

<div class="form-group row">
      <div class="col-md-6">
            <uc2:AccessEmailId ID="AccessEmailId1" runat="server" TypeofEmail="Access Control Email"  />
	</div>
	<div class="col-md-6">
           <uc4:PurposeOfVisit ID="PurposeOfVisit" runat="server" />
	</div>
	
</div>
    

<div class="form-group row">
      <div class="col-md-6">
           <uc3:AccessNo ID="AccessNo" runat="server" />
	</div>
	<div class="col-md-6">
          <uc7:SecurityAccessMail ID="samAccessControl" runat="server" />
	</div>
	
</div>
    

<div class="form-group row">
      <div class="col-md-6">
           <uc1:EmailFooter ID="EmailFooter1" runat="server" RequestTypeID="3" />
	</div>
	<div class="col-md-6">
          
	</div>
	
</div>
   
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    GridResponsiveCss();
 </script> 

</asp:Content>
