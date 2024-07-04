<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="TimesheetandCostsExport" Title="Deffinity" Codebehind="TimesheetandCostsExport.aspx.cs" %>
<%@ Register src="controls/ExportDataTab.ascx" tagname="ExportDataTab" tagprefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.TimesheetandCosts%> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ExportDataTab ID="ExportDataTab1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    
 <asp:Panel ID="pnlsearch" runat="server" Visible="true">
     <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.SearchDetails%>  </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

<asp:Panel ID="Panel4" runat="server" >
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
               <asp:TextBox ID="txt_EndDate" runat="server" SkinID="Date"> </asp:TextBox><asp:Label ID="img_to" runat="server" SkinID="Calender" /> <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
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
               <asp:DropDownList ID="ddlResource" runat="server">
                            </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ProjectReference%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlProjects" runat="server" >
                            </asp:DropDownList>
            </div>
	</div>
</div>
     <div class="form-group">
      <div class="col-md-6">
          
	</div>
	<div class="col-md-6 pull-right">
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
           <strong><label id="lblHead" runat="server" style="font:bold;">
        </label></strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
    
        
<asp:Panel ID="Panel2" runat="server" width="100%" >
    <div class="form-group">
          <div class="col-md-12" style="float:right;text-align:right;">
               <asp:Button ID="btnExportToExcel" runat="server" SkinID="btnDefault" Text="Export to Excel" Visible="false"
                        OnClick="btnExportToExcel_Click" />
	</div>
</div>

 <asp:Panel ID="pnlProjectGrid" runat="server" >
<asp:GridView ID="Gridview1" runat="server" 
        AutoGenerateColumns="False"  Width="100%" GridLines="None"         
        EmptyDataText="No Records Exist"      
        >
    <Columns>
    	
    <asp:BoundField DataField="DateEntered" HeaderText="Date"/>           
        <asp:BoundField DataField="ContractorName" HeaderText="Resource"/>              
        <asp:BoundField DataField="ProjectTitle" HeaderText="Title"/>  
        <asp:BoundField DataField="ProjectOwner" HeaderText="Project Owner" />                 
       <asp:BoundField DataField="EntryType" HeaderText="Type"/>
       <asp:BoundField DataField="Hours" HeaderText="Hours"/>
       <asp:BoundField DataField="Notes" HeaderText="Notes"/>
       <asp:BoundField DataField="Status" HeaderText="Status"/>
       <asp:BoundField DataField="PrimaryTimesheetApproverName" HeaderText="Approve 1"/>
       <asp:BoundField DataField="PrimeApprovedDate" HeaderText="Date Approved" />
       <asp:BoundField DataField="SecondaryTimesheetApproverName" HeaderText="Approve 2"/>
       <asp:BoundField DataField="SecondApprovedDate" HeaderText="Date Approved"/>    
      
    </Columns>
</asp:GridView>  
</asp:Panel>
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

