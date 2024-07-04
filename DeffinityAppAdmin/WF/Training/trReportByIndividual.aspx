<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_trReportByIndividual" Codebehind="trReportByIndividual.aspx.cs" %>

<%@ Register src="controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>
<%@ Register Src="controls/dropdownView.ascx" TagName="DropDownList" TagPrefix="uc2" %>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:TrainingTabs ID="TrainingTabs2" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.TrainingMgmt%>
 </asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
     Dashboard - <label id="lblTitle" runat="server"></label>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="panel_options" runat="Server">
    <uc2:DropDownList ID="dropDownList1" runat="server" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="form-group">
         <asp:ValidationSummary ID="validSummryEdit" runat="server" ValidationGroup="Group1" ShowSummary="true" />
    </div>

    <div class="form-group">
         <div class="col-xs-4">
            <label class="col-sm-5 control-label"><%= Resources.DeffinityRes.UserResource%></label>
            <div class="col-sm-7 form-inline">
                 <asp:DropDownList ID="ddlUser" runat="server"></asp:DropDownList>
            </div>
        </div>
         <div class="col-xs-3">
               <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.FromDate%></label>
            <div class="col-sm-7 form-inline">
                  <asp:TextBox ID="txtFromDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                   <asp:Label ID="imgFromDate" runat="server"  SkinID="Calender"></asp:Label>
                  
                   <ajaxToolkit:CalendarExtender runat="server" ID="calFromDate" TargetControlID="txtFromDate" PopupButtonID="imgFromDate"
                   CssClass="MyCalendar" ></ajaxToolkit:CalendarExtender>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid from date" 
                   ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ValidationGroup="Group1" Display="None" ControlToValidate="txtFromDate">
                   </asp:RegularExpressionValidator>
            </div>
        </div>
         <div class="col-xs-4">
               <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Department%></label>
            <div class="col-sm-8 form-inline">
                  <asp:TextBox ID="txtToDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                   <asp:Label ID="imgToDate" runat="server" SkinID="Calender"></asp:Label>
                   
                   <ajaxToolkit:CalendarExtender runat="server" ID="calToDate" TargetControlID="txtToDate" PopupButtonID="imgToDate"
                   CssClass="MyCalendar" ></ajaxToolkit:CalendarExtender>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter valid to date" 
                   ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ValidationGroup="Group1" Display="None" ControlToValidate="txtToDate">
                   </asp:RegularExpressionValidator>
                   <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please enter to date greater than from date" 
                   Display="None" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" 
                       Operator="GreaterThan" ValidationGroup="Group1" ></asp:CompareValidator>
                 <asp:Button ID="btnFilter" runat="server" SkinID="btnDefault" Text="View" ValidationGroup="Group1"
                                            onclick="btnFilter_Click" />
            </div>
        </div>
    </div>
    <div class="col-md-12">
              <h3 class="panel-title"><%= Resources.DeffinityRes.UserDetails%></h3><hr />
    </div>
     <div class="form-group">
         <div class="col-sm-3">
             <asp:Literal ID="ltrUserImg"  runat="server"></asp:Literal>
         </div>
         <div class="col-sm-8">
              <asp:Literal ID="ltrUserdetails" runat="server"></asp:Literal>
         </div>
     </div>
   <div class="col-md-12">
           <h3 class="panel-title"> <%= Resources.DeffinityRes.ActionPlan%></h3><hr />
    </div>
     <div class="form-group">
         <asp:GridView ID="grdPostCourseAction" runat="server" 
                EmptyDataText="No records found"  AllowPaging="true">
                  <Columns>
                  <asp:TemplateField HeaderText="Course title" HeaderStyle-CssClass="header_bg_l">
                  <ItemTemplate>
                      <asp:Label ID="lblCourse" runat="server" Text='<%#Bind("CourseTitle") %>'></asp:Label>
                  </ItemTemplate>
                  </asp:TemplateField>
                <asp:BoundField DataFormatString="{0:d}" DataField="DateofCourse" HtmlEncode="false" HeaderText="Date of course"/>
                   <asp:TemplateField HeaderText="Action plan" >
                  <ItemTemplate>
                      <asp:Label ID="lblActionPlan" runat="server" Text='<%#Bind("ActionPlan") %>' ></asp:Label>
                  </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField DataFormatString="{0:d}" DataField="EndDate" HtmlEncode="false" HeaderText="End Date"/>
                   <asp:TemplateField HeaderText="Status name" HeaderStyle-CssClass="header_bg_r"  >
                  <ItemTemplate>
                      <asp:Label ID="lblStatusName" runat="server" Text='<%#Bind("StatusName") %>' ></asp:Label>
                  </ItemTemplate>
                  </asp:TemplateField>
                 
                  </Columns>
               </asp:GridView>
     </div>
      <div class="form-group" id="divType" runat="server">
        
                        
          
          <div class="form-group" style="float:right;">
                 <div class="panel-title text-right"><h4> <%= Resources.DeffinityRes.FilterbyType%></h4> </div>
                  <asp:DropDownList ID="ddlByType" runat="server"
                                                  AutoPostBack="True" onselectedindexchanged="ddlByType_SelectedIndexChanged"></asp:DropDownList>
              </div>
      </div>
      <div class="form-group" id="tblReport" runat="server">
             <div class="col-xs-6">
                <h3  class="panel-title"><%= Resources.DeffinityRes.PendingCoursesTraining%> </h3><hr />
                <asp:GridView ID="grdCoursePending" runat="server" 
           Width="100%"  EmptyDataText="No records found" 
                       onpageindexchanging="grdCoursePending_PageIndexChanging" AllowPaging="true" PageSize="10">
            <Columns>
           
            
            <asp:TemplateField HeaderText="Training/Course" HeaderStyle-CssClass="header_bg_l">
             <%--<HeaderStyle Width="150px" />
            <ItemStyle Width="150px" />--%>
                     
            <ItemTemplate>
                <asp:Label ID="lblCourseTitle" runat="server" Text='<%#Bind("CourseTitle") %>' ></asp:Label>
            </ItemTemplate>
                      
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Type">
           <%-- <HeaderStyle HorizontalAlign="Center" />--%>
            
            <ItemTemplate>
                <asp:Label ID="lblCategory" runat="server" Text='<%#Bind("CategoryName")%>'></asp:Label>
            </ItemTemplate>
           
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DueDate">
            <HeaderStyle HorizontalAlign="Center" />
           
            <ItemTemplate>
                <asp:Label ID="lblDuedate" runat="server" Text='<%#Bind("EndDate","{0:d}")%>'></asp:Label>
            </ItemTemplate>
           
            </asp:TemplateField>
            
        
  <asp:TemplateField HeaderText="Cost" >
                                     <HeaderStyle HorizontalAlign="Center" />
                                        
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lblCostOfCourse" runat="server" Text='<%# Bind("CostofCourse") %>'></asp:Label>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField> 
                                    
                            
                              <asp:TemplateField HeaderText="Vendor" HeaderStyle-CssClass="header_bg_r" >
                                        <HeaderStyle HorizontalAlign="Center" />
                                        
                                       
                                       <ItemTemplate>
                                            <asp:Label ID="lblVendor" runat="server" ></asp:Label>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>
                                    
                                                                  
            </Columns>
            </asp:GridView>
          </div>
            <div class="col-xs-6">
                  <h3 class="panel-title"> <%= Resources.DeffinityRes.CompletedCoursesTraining%></h3><hr />
                <asp:GridView ID="grdCourseCompleted" runat="server" 
           Width="100%" EmptyDataText="No records found" AllowPaging="true" PageSize="10" 
                       onpageindexchanging="grdCourseCompleted_PageIndexChanging">
            <Columns>
           
            
            <asp:TemplateField HeaderText="Training/Course" HeaderStyle-CssClass="header_bg_l">
            <%-- <HeaderStyle Width="150px" />
            <ItemStyle Width="150px" />--%>
                     
            <ItemTemplate>
                <asp:Label ID="lblCourse" runat="server" Text='<%#Bind("CourseTitle") %>'></asp:Label>
            </ItemTemplate>
                      
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Type">
            <HeaderStyle HorizontalAlign="Center" />
            
            <ItemTemplate>
                <asp:Label ID="lblType" runat="server" Text='<%#Bind("CategoryName")%>'></asp:Label>
            </ItemTemplate>
           
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Completed">
            <HeaderStyle HorizontalAlign="Center" />
           
            <ItemTemplate>
                <asp:Label ID="lblEmployee" runat="server" Text='<%#Bind("EndDate","{0:d}")%>'></asp:Label>
            </ItemTemplate>
           
            </asp:TemplateField>
            
        
  <asp:TemplateField HeaderText="Cost" >
                                     <HeaderStyle HorizontalAlign="Center" />
                                        
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lblCost" runat="server" Text='<%# Bind("CostofCourse") %>'></asp:Label>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField> 
                                    
                          <asp:TemplateField HeaderText="Expenses" HeaderStyle-CssClass="header_bg_r">
                                     <HeaderStyle HorizontalAlign="Center" />
                                        
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpenses" runat="server" Text='<%# Bind("Expenses") %>'></asp:Label>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>   
                              
                                   
                                                                  
            </Columns>
            </asp:GridView>
           </div>
      </div>
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


