<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="AnnualLeaveandAbsenceexport" Title="Deffinity" Codebehind="AnnualLeaveandAbsenceexport.aspx.cs" %>
<%@ Register src="controls/ExportDataTab.ascx" tagname="ExportDataTab" tagprefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.AnnualLeaveandAbsence%> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ExportDataTab ID="ExportDataTab1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    
 <asp:Panel ID="pnlsearch" runat="server" Visible="true">
     <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.SearchDetails%></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

<asp:Panel ID="Panel4" runat="server">
     <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.FromDate%></label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txt_startDate" runat="server" SkinID="Date"> </asp:TextBox><asp:Label ID="img_from" runat="server" SkinID="Calender" />
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ToDate%></label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txt_EndDate" runat="server" SkinID="Date"> </asp:TextBox><asp:Label ID="img_to" runat="server" SkinID="Calender" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                        PopupButtonID="img_from" TargetControlID="txt_startDate" CssClass="MyCalendar">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2"  runat="server"
                        PopupButtonID="img_to" TargetControlID="txt_EndDate" CssClass="MyCalendar">
                        </ajaxToolkit:CalendarExtender> 
            </div>
	</div>
</div>
 <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Resource%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlResource" runat="server" >
                            </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.AbsenceType%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlAbsenceType" runat="server" >
                        </asp:DropDownList>  
            </div>
	</div>
</div>
 <div class="form-group">
      <div class="col-md-6">
         
	</div>
	<div class="col-md-6 form-inline">
        <span class="pull-right">
           <asp:Button ID="btn_Searchprojects" runat="server" 
                                onclick="btn_Searchprojects_Click" SkinID="btnSearch" />
                            <asp:Button ID="btn_clear" runat="server" onclick="btn_clear_Click" 
                                SkinID="btnClear" />
            </span>
	</div>
</div>
                </asp:Panel>
           
    </asp:Panel>
   
 <asp:Panel ID="PanlePending" runat="server" width="100%" Visible="true" >
     <div class="form-group">
        <div class="col-md-12">
           <strong><label id="lblHead" runat="server">
        </label></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
     <div class="form-group">
          <div class="col-md-12">
              <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
	</div>
</div>
     <div class="form-group pull-right">
          <div class="col-md-12">
              <asp:Button ID="btnExportToExcel" runat="server" Visible="false" Text="Export to Excel"
                        OnClick="btnExportToExcel_Click" />
	</div>
</div>
    <asp:Panel ID="pnlProjectGrid" runat="server" ScrollBars="Auto">
<asp:GridView ID="Gridview1"  runat="server" 
        AutoGenerateColumns="False"  Width="100%" GridLines="None"         
        EmptyDataText="No Records Exists"      
        AllowSorting="True" >
    <Columns>		
        
        <asp:BoundField DataField="ContractorName" HeaderText="Resource" />
       <asp:BoundField DataField="AbsenseType" HeaderText="Type"/>
       <asp:BoundField DataField="startdate" HeaderText="From Date"/>
       <asp:BoundField DataField="enddate" HeaderText="To Date"/>
       <asp:BoundField DataField="notes" HeaderText="Notes"/>
       <asp:BoundField DataField="ApproverName" HeaderText="Approver"/>
       <asp:BoundField DataField="Allowance" HeaderText="Allowance" />
       <asp:BoundField DataField="DaysinLieu" HeaderText="Days in Lieu"/>
       <asp:BoundField DataField="Remaining" HeaderText="Remaining"/>

    </Columns>
</asp:GridView>  
</asp:Panel>

</asp:Panel>


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

