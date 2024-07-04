<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_trDueSoon_DashBoard_" Codebehind="trDueSoon.aspx.cs" %>

<%@ Register src="controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>
<%@ Register Src="controls/dropdownView.ascx" TagName="DropDownList" TagPrefix="uc2" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:TrainingTabs ID="TrainingTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.Training%>
 </asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
  <label id="lblTitle" runat="server">
                  </label> -  <%= Resources.DeffinityRes.TrainingDueSoon%> 
    </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="panel_options" runat="Server">
      
    <uc2:DropDownList ID="dropDownList" runat="server" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <%--<div class="row">
        <div class="col-lg-12 pull-right">--%>
          
      <%--  </div>
    </div>--%>
  
     <div class="form-group">
      <asp:ValidationSummary ID="validSummryEdit" runat="server" ValidationGroup="Group1" ShowSummary="true" />
  </div>

     <div class="form-group">
                                  <div class="col-xs-4">
                                       <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Course%></label>
                                      <div class="col-sm-9">
                                           <asp:DropDownList ID="ddlCourse" SkinID="ddl_90" runat="server">
                   </asp:DropDownList>
                                            
					</div>
				</div>
 <div class="col-xs-3">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.FromDate%></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:TextBox ID="txtFromDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                   <asp:Label ID="imgFromDate" runat="server" SkinID="Calender" ></asp:Label>
                   <ajaxToolkit:CalendarExtender runat="server" ID="calFromDate" TargetControlID="txtFromDate" PopupButtonID="imgFromDate"
                   CssClass="MyCalendar" ></ajaxToolkit:CalendarExtender>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid from date" 
                   ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ValidationGroup="Group1" Display="None" ControlToValidate="txtFromDate">
                   </asp:RegularExpressionValidator>
                                          
					</div>
     </div>
     <div class="col-xs-3">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ToDate%></label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtToDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                   <asp:Label ID="imgToDate" runat="server"  SkinID="Calender" />
                   <ajaxToolkit:CalendarExtender runat="server" ID="calToDate" TargetControlID="txtToDate" PopupButtonID="imgToDate"
                   CssClass="MyCalendar" ></ajaxToolkit:CalendarExtender>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter valid to date" 
                   ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ValidationGroup="Group1" Display="None" ControlToValidate="txtToDate">
                   </asp:RegularExpressionValidator>
                   <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please enter to date greater than from date" 
                   Display="None" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" 
                       Operator="GreaterThanEqual" ValidationGroup="Group1" ></asp:CompareValidator>
                                          </div>

         </div>
     <div class="col-xs-2">
         <asp:Button ID="btnFilter" runat="server" SkinID="btnDefault" Text="Filter"  ValidationGroup="Group1"
                       onclick="btnFilter_Click" />

         </div>
				
</div>
  
         
            <asp:GridView ID="grd_trBookingDueSoon" runat="server" onrowdatabound="grd_trBookingDueSoon_RowDataBound" Width="100%"
                          AllowPaging="True" onpageindexchanging="grd_trBookingDueSoon_PageIndexChanging" PageSize="10" >
            <Columns>
            <asp:TemplateField HeaderText="Date of Course" HeaderStyle-CssClass="header_bg_l">
            <ItemTemplate>
                <asp:Label ID="lblDateOfCourse" runat="server" Text='<%#Bind("DateofCourse","{0:d}") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Course">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblCourse" runat="server" Text='<%#Bind("CourseTitle")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="User">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblEmployee" runat="server" Text='<%#Bind("EmployeeName")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
  <asp:TemplateField HeaderText="Department" HeaderStyle-CssClass="header_bg_r">
                                     <HeaderStyle HorizontalAlign="Center" />        
                                        <ItemTemplate>
                                            <asp:Label ID="lblDepartment" runat="server" Text='<%# Bind("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                              <asp:TemplateField HeaderText="Vendor" HeaderStyle-CssClass="header_bg_r" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                       <%-- <ItemTemplate>
                                            <asp:Label ID="lblVendor" runat="server" Width="140px" Text='<%# Bind("DepartmentName") %>'></asp:Label>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                                                  
            </Columns>
            </asp:GridView>
   
       <script src="../../Scripts/respond.min.js"></script>
    <script src="../../Content/assets/js/rwd-table/js/rwd-table.min.js"></script>
    <script src="../../Scripts/GridDesingFix.js"></script>
    <script type="text/javascript">
        //grid_responsive();
        grid_responsive_display();

        $(window).load(function () {
              $(".dropdown-menu li")
            .find("input[type='checkbox']")
            .prop('checked', 'checked').trigger('change');
            $(".btn-toolbar").hide();
            //var cols = [];
            //$(".dropdown-menu li").each(function () {
            //    $(this).hide();
            //});
            //$(".checkbox-row").eq(1).hide();
            //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
            //    $(".checkbox-row").eq(value).hide();
            //});
        });
    </script>

</asp:Content>


