<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="FMSalesForeCast" Codebehind="FMSalesForeCast.aspx.cs" %>

<%@ Register Src="controls/FinanceModuleTab.ascx" TagName="FMTab" TagPrefix="uc2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.RevenueForecast%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc2:FMTab ID="fmID" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlCustomer" runat="server" SkinID="ddl_90" 
                        AutoPostBack="True" onselectedindexchanged="ddlCustomer_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="DropDownListStatus" runat="server" SkinID="ddl_90" 
                                               >
                                            </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
          
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.FromDate%> </label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtFromDate" runat="server" SkinID="Date"></asp:TextBox>
                                <asp:Label ID="imgbtnenddate" runat="server" SkinID="Calender" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender"  runat="server"
                                    PopupButtonID="imgbtnenddate" TargetControlID="txtFromDate" CssClass="MyCalendar">
                                </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ToDate%> :</label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtToDate" runat="server" SkinID="Date"></asp:TextBox>
                                <asp:Label ID="imgbtnenddate1" runat="server" SkinID="Calender" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                    PopupButtonID="imgbtnenddate1" TargetControlID="txtToDate" CssClass="MyCalendar">
                                </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-4">
           <asp:Button ID="imgView" runat="server" SkinID="btnDefault" Text="View" 
                                    onclick="imgView_Click" />
	</div>
</div>
    <div class="form-group" >
          <div class="col-md-12"  style="overflow:auto">
              <asp:Literal ID="ltlSalesForecast" runat="server"></asp:Literal>
	</div>
</div>
    
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


