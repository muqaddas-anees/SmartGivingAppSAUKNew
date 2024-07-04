<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_trDeptReq" Codebehind="trDeptReq.aspx.cs" %>

<%@ Register src="controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>
<%@ Register src="controls/TrainingSubTabs.ascx" tagname="TrainingSubTabs" tagprefix="uc2" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:TrainingTabs ID="TrainingTabs2" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.TrainingMgmt%>
 </asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
    <label id="lblTitle" runat="server"></label><br />
       <uc2:trainingsubtabs ID="TrainingSubTabs2" runat="server" />
    </asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="panel_options" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
            <div class="form-group">
     <asp:Label ID="lblException" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label> 
 </div>
            <div class="form-group">
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
  </div>
            <div class="form-group">
                   <h3 class="panel-title">
                <label><%= Resources.DeffinityRes.DepartmentRequirement%></label>
                         </h3><hr />
            </div>
            <div class="form-group">
            <div class="col-xs-2">
                 <%= Resources.DeffinityRes.Department%>
            </div>
            <div class="col-xs-10 form-inline">
                  <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" SkinID= "ddl_30"
                      onselectedindexchanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
                  <asp:Button ID="BtnAddDepartment" runat="server" SkinID="btnAdd" />
                     <asp:Button ID="btnEditDepartment" runat="server" SkinID="btnEdit" onclick="btnEditDepartment_Click" />
        <asp:Button ID="btnDeleteDepartment" runat="server" SkinID="btnDefault" Text="Delete" onclick="btnDeleteDepartment_Click"
             OnClientClick="return confirm('Do you want to delete the department?');"/>
            </div>
            </div>
            <div class="form-group">
            <div class="col-xs-2">
                   <%= Resources.DeffinityRes.Area%>
            </div>
            <div class="col-xs-10 form-inline">
                 <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="True" SkinID="ddl_30"
                             onselectedindexchanged="ddlArea_SelectedIndexChanged"></asp:DropDownList>
                   <asp:Button ID="imgBtnArea" runat="server" SkinID="btnAdd" />
                      <asp:Button ID="btnDeleteArea" runat="server" SkinID="btnDefault" Text="Delete"
                          OnClick="btnDeleteArea_Click" OnClientClick="return confirm('Do you want to delete the area?');" />
            </div>
            </div>
            <div class="form-group">
            <div class="col-xs-2">
                   <%= Resources.DeffinityRes.Customer%>
            </div>
            <div class="col-xs-10">
                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" SkinID="ddl_30"
                                onselectedindexchanged="ddlCustomer_SelectedIndexChanged"></asp:DropDownList>
            </div>
            </div>
            <div class="form-group">
            <div class="col-xs-2">
                   <%= Resources.DeffinityRes.Site%>
            </div>
            <div class="col-xs-10">
                <asp:DropDownList ID="ddlSite" runat="server" SkinID="ddl_30"></asp:DropDownList>
            </div>
            </div>
            <div class="form-group">
            <div class="col-xs-2">
                   <%= Resources.DeffinityRes.Course%>
            </div>
            <div class="col-xs-10">
                  <asp:DropDownList ID="ddlDepCourse" runat="server" SkinID="ddl_30"
                              AutoPostBack="True" onselectedindexchanged="ddlDepCourse_SelectedIndexChanged"></asp:DropDownList>
            </div>

            </div>
            <div class="form-group">
            <div class="col-xs-2">
                   <%= Resources.DeffinityRes.MinimumNumberReq%>
            </div>
            <div class="col-xs-10">
                   <asp:TextBox ID="txtNumberReq" runat="server" SkinID="txt_30"></asp:TextBox>
    <asp:RequiredFieldValidator
        ID="RequiredFieldValidator2" runat="server"  
        ErrorMessage="Please enter minimum number required" Display="None" 
        ValidationGroup="Group1" ControlToValidate="txtNumberReq"></asp:RequiredFieldValidator> 
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ControlToValidate="txtNumberReq" Display="None" 
        ErrorMessage="Please enter numbers only" ValidationExpression="^\d+$" 
        ValidationGroup="Group1"></asp:RegularExpressionValidator>
            </div>
            </div>
            <div class="form-group">
            <div class="col-xs-2">
                   <%= Resources.DeffinityRes.Target%>
            </div>
            <div class="col-xs-10 form-inline">
                    <asp:TextBox ID="txtTarget" runat="server" SkinID="txt_30"></asp:TextBox><span>%</span>
    <asp:RequiredFieldValidator 
        ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTarget"  
        Display="None" ErrorMessage="Please enter target" ValidationGroup="Group1"></asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="txtTarget" Display="None" 
        ErrorMessage="Enter between 1 and 100" MaximumValue="100" MinimumValue="1" 
        Type="Integer" ValidationGroup="Group1"></asp:RangeValidator>
            </div>
            </div>
            <div class="form-group">
            <div class="col-xs-2">
                   <%= Resources.DeffinityRes.FromDate%>
            </div>
            <div class="col-xs-10 form-inline">
                 <asp:TextBox ID="txtBookingDate" runat="server" SkinID="Date"></asp:TextBox>
                        <asp:Label ID="imgCalenderBookingDate" runat="server" SkinID="Calender"></asp:Label>
                        <ajaxToolkit:CalendarExtender PopupButtonID="imgCalenderBookingDate" TargetControlID="txtBookingDate"
                        runat="server" CssClass="MyCalendar"  ID="cldExtenderBooking"></ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator  ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please enter booking date" Display="None"
                        ControlToValidate="txtBookingDate" ValidationGroup="Group3" >
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ControlToValidate="txtBookingDate" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                            ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter valid date" Display="None" ValidationGroup="Group3"></asp:RegularExpressionValidator>
            </div>
            </div>
            <div class="form-group">
            <div class="col-xs-2">
                   <%= Resources.DeffinityRes.ToDate%>
            </div>
           
            <div class="col-xs-10 form-inline">
                  <asp:TextBox ID="txtEndDate" runat="server" SkinID="Date"></asp:TextBox>
                        <asp:Label ID="imgEndDate" runat="server" SkinID="Calender"></asp:Label>
                        <ajaxToolkit:CalendarExtender PopupButtonID="imgEndDate" TargetControlID="txtEndDate"
                        runat="server" CssClass="MyCalendar"  ID="CalendarExtender2"></ajaxToolkit:CalendarExtender>
                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please enter end date" ValidationGroup="Group3" ControlToValidate="txtEndDate"
                        Display="None" ></asp:RequiredFieldValidator>--%><asp:RegularExpressionValidator
                            ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please enter valid date" Display="None"
                             ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                             ControlToValidate="txtEndDate" ValidationGroup="Group3"></asp:RegularExpressionValidator>
            </div>
            </div>
            <div class="form-group">
                  <div class="col-xs-6">
                       <asp:Button ID="btnDepSubmit" runat="server" SkinID="btnSubmit"
                           onclick="btnDepSubmit_Click" ValidationGroup="Group1" /> &nbsp;
                      <asp:Button ID="btnDeptCancel" runat="server" SkinID="btnCancel"
                           onclick="btnDeptCancel_Click" />
                   <asp:HiddenField ID="H_DeptoCus" runat="server" Value="0" />
                  </div>
              </div>
 

      <div class="form-group">
                   <h3 class="panel-title">
                <label><%= Resources.DeffinityRes.DepartmentRequirement%></label>
                         </h3><hr />
            </div>

<div>
<asp:GridView ID="GridDeptReq" runat="server" Width="100%" Font-Size="X-Small" EmptyDataText="No records found">
<Columns>
<asp:BoundField DataField="DepartmentName" HeaderText="Department" HeaderStyle-CssClass="header_bg_l" />
<asp:BoundField DataField="AreaName" HeaderText="Area" />
<asp:BoundField DataField="CourseName" HeaderText="Course"   />
<asp:BoundField DataField="MinRequired" HeaderText="MinRequired"/>
<asp:BoundField DataField="Target" HeaderText="Target"   HeaderStyle-CssClass="header_bg_r"/>

</Columns>
</asp:GridView>
</div>
      <div class="form-group">
                   <h3 class="panel-title">
                <label>Users that Belong to this department</label>
                         </h3><hr />
            </div>
<div>
<asp:GridView ID="Grid_DepartmentUsers" runat="server" Width="100%" Font-Size="X-Small" EmptyDataText="No records found">
<Columns>
<asp:BoundField DataField="DepartmentName" HeaderText="Department" HeaderStyle-CssClass="header_bg_l" />
<asp:BoundField DataField="AreaName" HeaderText="Area" />
<asp:BoundField DataField="ContractorName" HeaderText="User"   HeaderStyle-CssClass="header_bg_r"/>
</Columns>
</asp:GridView>
</div>
<div>
    <ajaxToolkit:ModalPopupExtender ID="mdlDepartment" runat="server" CancelControlID="btnModelDepartmentCancel"
                    BackgroundCssClass="modalBackground" TargetControlID="BtnAddDepartment" PopupControlID="pnlDepartment" />
                    <asp:Panel ID="pnlDepartment" runat="server" BackColor="White"
                    Style="display: none" Width="230px" BorderStyle="Double" BorderColor="LightSteelBlue">

                         <div class="form-group">
                                <div class="col-md-12 text-bold">
                                       <strong>   <%= Resources.DeffinityRes.Department%></strong>
                                </div>
                        </div>
                          <div class="form-group">
                                 <div class="col-xs-12">
                                        <div class="form-inline">
                                            <asp:TextBox ID="txtModelDepartment" runat="server" Width="210px"></asp:TextBox>
                               <asp:HiddenField ID="H_Department" runat="server" Value="0" />
                                <asp:Label ID="lblMsgDepartment" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtModelDepartment"
                                    ErrorMessage="Please enter department" ForeColor="Red" ValidationGroup="Group_Department"></asp:RequiredFieldValidator>
                                        </div>
                                     </div>
                              </div>
                         <div class="form-group">
                                 <div class="col-xs-12">
                                        <div class="form-inline">
                                             <asp:Button ID="btnModelDepartmentInsert" runat="server"
                                                  OnClick="btnModelDepartmentInsert_Click" SkinID="btnSubmit" ValidationGroup="Group_Department" />
                                              <asp:Button ID="btnModelDepartmentCancel" runat="server" Text="Close" SkinID="btnCancel" />                                        </div>
                                     </div>
                             </div>
                    
                </asp:Panel>
</div>
<ajaxToolkit:ModalPopupExtender ID="mdlArea" CancelControlID="imgAreaCancel" runat="server" BackgroundCssClass="modalBackground"
TargetControlID="imgBtnArea" PopupControlID="pnlArea"></ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlArea" runat="server"  BackColor="White" style="display:none" Width="230px" 
BorderStyle="Double" BorderColor="LightSteelBlue">
      <div class="form-group">
                                <div class="col-md-12 text-bold">
                                       <strong>   <%= Resources.DeffinityRes.Area%></strong>
                                </div>
                        </div>
      <div class="form-group">
                                 <div class="col-xs-12">
                                        <div class="form-inline">
                                             <asp:TextBox ID="txtArea" runat="server" Width="210px"></asp:TextBox>
                               <asp:HiddenField ID="H_Area" runat="server" Value="0" />
                                <asp:Label ID="lblArea" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtArea"
                                    ErrorMessage="Please enter area" ForeColor="Red" ValidationGroup="Group_Area"></asp:RequiredFieldValidator>
                                        </div>
                                     </div>
          </div>
     <div class="form-group">
                                 <div class="col-xs-12">
                                        <div class="form-inline">
                                              <asp:Button ID="imgAreaSubmit" runat="server" Text="OK"  SkinID="btnSubmit"
                                    OnClick="imgAreaSubmit_Click"  ValidationGroup="Group_Area"/>
                                    <asp:Button ID="imgAreaCancel" runat="server" Text="Close" SkinID="btnCancel" />
                                        </div>
                                     </div>
          </div>
</asp:Panel>


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


