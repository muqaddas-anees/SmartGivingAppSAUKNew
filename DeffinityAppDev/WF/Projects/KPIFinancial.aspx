<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="KPIFinancial" Codebehind="KPIFinancial.aspx.cs" %>

<%@ Register Src="controls/KPITab.ascx" TagName="KPITabs" TagPrefix="UCKpi" %>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
    <%= Resources.DeffinityRes.KPIFinancial%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_options" runat="Server">
   <a id="lbtn_Navigate" target="_self" href="FMResources.aspx"><span id="lbtn_NavigateText" runat="server"><i class="fa fa-arrow-left"></i> Return to Finance Section</span></a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <UCKpi:KPITabs ID="kpiTab" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlCustomer" runat="server" >
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Programme%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlProgramme" AutoPostBack="false"   runat="server" >
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.SubProgramme%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlSubprogramme" runat="server" >
                </asp:DropDownList>
                <ajaxToolkit:CascadingDropDown ID="CascadingDropDown21" runat="server" TargetControlID="ddlSubprogramme"
                                    Category="Title" PromptText="Please select..." ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                    ServiceMethod="GetSubProgramme" ParentControlID="ddlProgramme" />
            </div>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.FilterbyYear%> </label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtFromDate" runat="server" SkinID="Date"></asp:TextBox>
                                <asp:Label ID="imgbtnenddate" runat="server" SkinID="Calender" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender"  Format="yyyy" runat="server"
                                    PopupButtonID="imgbtnenddate" TargetControlID="txtFromDate" CssClass="MyCalendar">
                                </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-6">
         
	</div>
	<div class="col-md-2 pull-right">
        <span class="pull-right">
         <asp:Button ID="imgView" SkinID="btnDefault" Text="Filter" runat="server" 
                    onclick="imgView_Click" />
          <asp:TextBox ID="txtToDate" runat="server" Width="80px" Visible="False"></asp:TextBox>
                <asp:Label ID="imgbtnenddate1" runat="server" SkinID="Calender" 
                    Visible="False" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                    CssClass="MyCalendar"  PopupButtonID="imgbtnenddate1" 
                    TargetControlID="txtToDate">
                </ajaxToolkit:CalendarExtender>
            </span>
	</div>
</div>

   <div class="form-group">
          <div class="col-md-12">
               <asp:Literal ID="ltlDisplay" runat="server"></asp:Literal>
	</div>
</div>
   
    
   <script type="text/javascript">
       //document.getElementById("mainTable").onload = function () { grid_responsive_display(); };
      
        //grid_responsive();
       
       $(window).load(function () {
           grid_responsive_display();
                     $("button:contains('Display all')").click(function (e) {
                e.preventDefault();
                $(".dropdown-menu li")
          .find("input[type='checkbox']")
          .prop('checked', 'checked').trigger('change');
            });
                 });
    </script> 

    
</asp:Content>


