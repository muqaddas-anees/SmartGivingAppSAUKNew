<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Reports_BOMExportDataReport1" Codebehind="BOMExportDataReport1.aspx.cs" %>

<%@ Register src="controls/ExportDataTab.ascx" tagname="ExportDataTab" tagprefix="uc2" %>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <asp:Label ID="lblMsg" runat="server" Text="Project BOM Report"></asp:Label>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc2:ExportDataTab ID="ExportDataTab1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<script  type="text/javascript">
       var retval;
       function Setheight() {
           if (retval == null || retval == true) {
               retval = false;
               document.getElementById("div1").style.height = 270;
           }
           else {
               retval = true;
               document.getElementById("div1").style.height = 140;
           }
           
           return false;
       }
       function calendarShown(sender, args) {  Setheight(); }
   </script>
    <div class="form-group">
          <div class="col-md-12">
              <asp:Label ID="lblErrorMsg" runat="server" Text="" Visible="false"></asp:Label>
	</div>
</div>

   <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Projects%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlProjects" SkinID="ddl_90" runat="server">
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Supplier%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlVendor" SkinID="ddl_90" runat="server" ></asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">

            </div>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ProjectReference%></label>
            <div class="col-sm-1">
                <input type="text" id="lblProjectPrefix" disabled="true" runat="server" style="width:35px;height:32px" />
                </div>
           <div class="col-sm-7 form-inline">
               
               <asp:TextBox ID="txtProRef" SkinID="txt_90" runat="server"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.FromDate%></label>
           <div class="col-sm-8 form-inline">
                 <asp:TextBox ID="txt_FromDate" runat="server" SkinID="Date" MaxLength="10" />
                        <asp:Label ID="Image3" runat="server" SkinID="Calender"  />
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender3"  runat="server"
                            PopupButtonID="Image3" TargetControlID="txt_FromDate" CssClass="MyCalendar"  OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ToDate%></label>
           <div class="col-sm-8 form-inline">
                 <asp:TextBox ID="txt_ToDate" runat="server" SkinID="Date" MaxLength="10" />
                        <asp:Label ID="Image1" runat="server" SkinID="Calender"  />
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                            PopupButtonID="Image1" TargetControlID="txt_ToDate" CssClass="MyCalendar"  OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>
            </div>
	</div>
</div>
    <div class="form-group pull-right">
          <div class="col-md-12 form-inline">
               <asp:Button ID="btn_Submitt" runat="server" SkinID="btnDefault" Text="View Report" 
                            ValidationGroup="prjecRef" OnClick="btn_Submitt_Click"  />
               
                 <asp:Button ID="lnkButtonExcel" runat="server" 
                       onclick="lnkButtonExcel_Click" SkinID="btnDefault" Text="Excel Export 1"></asp:Button>
                  
                  
                   <asp:Button ID="lnkButtonExcel1" runat="server" 
                        onclick="lnkButtonExcel1_Click" SkinID="btnDefault" Text="Excel Export 2"></asp:Button>
	</div>
</div>
     <br />
       
         
         <div class="form-group">
        <div class="col-md-12">
           <strong>View Reports</strong> 
            <hr class="no-top-margin" />
            </div>
     
               <div style="overflow:auto"> 
                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatepanel1">
    <ProgressTemplate>
    <asp:Label SkinID="Loading" runat="server"></asp:Label>
    </ProgressTemplate>
    </asp:UpdateProgress> 
                <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                <div style="z-index:-1000;">
                 <iframe id="TimesheetSummary" name="TimesheetSummary" runat="server" frameborder="0" width="100%" height="600px" scrolling="auto"></iframe>
                    
                </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btn_Submitt" />

                </Triggers>
                </asp:UpdatePanel>
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


