<%@ Page Title="Deffinity" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ExportofProjectOverviewdata" Codebehind="ExportofProjectOverviewdata.aspx.cs" %>

<%@ Register src="controls/ExportDataTab.ascx" tagname="ExportDataTab" tagprefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    <label id="lblHead" runat="server">
        </label>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:ExportDataTab ID="ExportDataTab1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    
 <asp:Panel ID="pnlsearch" runat="server" Visible="true">
     <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.FromDate%></label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txt_startDate" runat="server" SkinID="Date"> </asp:TextBox>
                            <asp:Label ID="img_from" runat="server" SkinID="Calender" />
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
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlPortfolio" runat="server" >
                            </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Owner%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlowner" runat="server" DataSourceID="ObjDS_Owners" 
                                DataTextField="ContractorName" DataValueField="ID" 
                                ondatabound="ddlowner_DataBound" >
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjDS_Owners" runat="server" 
                                OldValuesParameterFormatString="original_{0}" 
                                SelectMethod="UserSelectAdmin_Withselect" 
                                TypeName="Deffinity.Bindings.DefaultDatabind"></asp:ObjectDataSource>
            </div>
	</div>
</div>
     <div class="form-group pull-right">

          <div class="col-md-12 form-inline">
               <asp:Button ID="btn_Searchprojects" runat="server" 
                                onclick="btn_Searchprojects_Click" SkinID="btnSearch" />
                            <asp:Button ID="btn_clear" runat="server" onclick="btn_clear_Click" 
                                SkinID="btnClear" />
	</div>
</div>

    </asp:Panel>
 <asp:Panel ID="PanlePending" runat="server" width="100%" Visible="true" >
         
<asp:Panel ID="Panel2" runat="server" width="100%" >
 
    <div class="form-group">
          <div class="col-md-12">
              <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
	</div>
</div>
    <div class="form-group pull-right">
          <div class="col-md-12 pull-right">
               <asp:Button ID="btnExportToExcel2" runat="server" SkinID="btnDefault" Text="Export to Excel" Visible="false"
                        OnClick="btnExportToExcel2_Click" /> 
	</div>
</div>
 <asp:Panel ID="pnlProjectGrid" runat="server" >
<asp:GridView ID="Gridview1" runat="server" 
        AutoGenerateColumns="False"  Width="100%" GridLines="None"         
        EmptyDataText="No Records Exists"      
        AllowSorting="True" >
    <Columns>
        <asp:BoundField DataField="Project" HeaderText="<%$ Resources:DeffinityRes,ProjectRef %>" ItemStyle-HorizontalAlign="Left" />           
        <asp:BoundField DataField="Customer" HeaderText="<%$ Resources:DeffinityRes,Customer %>" ItemStyle-HorizontalAlign="Left" />
       <asp:BoundField DataField="ProjectTitle" HeaderText="<%$ Resources:DeffinityRes,ProjectTitle %>" ItemStyle-HorizontalAlign="Left" />
       <asp:BoundField DataField="PM" HeaderText="<%$ Resources:DeffinityRes,ProjectManager %>" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
       <asp:BoundField DataField="Description" HeaderText="<%$ Resources:DeffinityRes,Description %>" ItemStyle-HorizontalAlign="Left" />
       <asp:BoundField DataField="Value" HeaderText="<%$ Resources:DeffinityRes,Value %>" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}" />
       <asp:BoundField DataField="BudgetGP" HeaderText="<%$ Resources:DeffinityRes,BudgetGP%>" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}%"/>
       <asp:BoundField DataField="BOMtoDate" HeaderText="<%$ Resources:DeffinityRes,BOMtoDate%>" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}" />
       <asp:BoundField DataField="BudgetedHours" HeaderText="<%$ Resources:DeffinityRes,BudgetedHours%>" ItemStyle-HorizontalAlign="Right" />
       <asp:BoundField DataField="ActualHours" HeaderText="<%$ Resources:DeffinityRes,ActualHours%>" ItemStyle-HorizontalAlign="Right" />
       <asp:BoundField DataField="TargetDate" HeaderText="<%$ Resources:DeffinityRes,TargetDate%>" DataFormatString="{0:d}" ItemStyle-HorizontalAlign="Right" />
       <asp:BoundField DataField="InvoiceToDate" HeaderText="<%$ Resources:DeffinityRes,InvoiceToDate%>" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:f2}"  />
                                  
       
       
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

