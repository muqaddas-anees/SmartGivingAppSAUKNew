<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="IssuesRisksExport" Title="Deffinity" Codebehind="IssuesRisksExport.aspx.cs" %>
<%@ Register src="controls/ExportDataTab.ascx" tagname="ExportDataTab" tagprefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.IssuesAndRisks%> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ExportDataTab ID="ExportDataTab1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    
 <asp:Panel ID="pnlsearch" runat="server" Visible="true">
   
<asp:Panel ID="Panel4" runat="server" >
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Projects%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlProjects" runat="server" >
                            </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-8">
                 <asp:DropDownList ID="ddlIncidentStatus" runat="server" DataSourceID="objDsStatus" 
                        DataTextField="Status" DataValueField="ID" ondatabound="ddlIncidentStatus_DataBound">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="objDsStatus" runat="server" 
                        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
                        TypeName="DeffinityManager.DAL.PmNewIssuesTableAdapters.adaItemstatus"></asp:ObjectDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlPortfolio" runat="server" >
                            </asp:DropDownList>  
            </div>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
         
	</div>
	<div class="col-md-4">
          
	</div>
	<div class="col-md-4 form-inline">
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
     
           
<asp:Panel ID="Panel2" runat="server" width="100%" >
    <div class="form-group">
          <div class="col-md-12">
               <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
	</div>
</div>
    <div class="form-group">
          <div class="col-md-12">
               <asp:Button ID="btnExportToExcel" runat="server" SkinID="btnDefault" Text="Export to Excel" Visible="false"
                        OnClick="btnExportToExcel_Click" />
	</div>
</div>
 

 <asp:Panel ID="pnlProjectGrid" runat="server" ScrollBars="Auto">
<asp:GridView ID="Gridview1" runat="server" 
        AutoGenerateColumns="False"  Width="100%" GridLines="None"         
        EmptyDataText="No Records Exists"      
        AllowSorting="True" >
    <Columns>

 <asp:BoundField DataField="ScheduledDate" HeaderText="Scheduled Date"/>           
       <asp:BoundField DataField="Issue" HeaderText="Issue" />
       <asp:BoundField DataField="ProjectReferenceWithPrefix" HeaderText="Project Ref"/>
       <asp:BoundField DataField="ProjectTitle" HeaderText="Title"/>
       <asp:BoundField DataField="StatusName" HeaderText="Status"/>
       <asp:BoundField DataField="IssueTypeName" HeaderText="Issue Type"/>       
       <asp:BoundField DataField="ResolvedDate" HeaderText="Resolved Date"/>
       <asp:BoundField DataField="Resolution" HeaderText="Resolution"/>
       
       
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

