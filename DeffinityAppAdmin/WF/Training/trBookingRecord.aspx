<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_trBookingRecord" Codebehind="trBookingRecord.aspx.cs" %>

<%@ Register src="controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>


<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:TrainingTabs ID="TrainingTabs2" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.TrainingMgmt%>
 </asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
       <label style="font:bold"><%= Resources.DeffinityRes.TrainingBookingRcd%></label>
 </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="panel_options" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="form-group">
        <ajaxToolkit:ModalPopupExtender ID="mdlPopUpDownload" CancelControlID="imgClose" BackgroundCssClass="modalBackground"
        TargetControlID="btnShowModalPopup" PopupControlID="pnlAreaDownload" runat="server">
        </ajaxToolkit:ModalPopupExtender> 
         <ajaxToolkit:ModalPopupExtender ID="modelPopupAddCourse" CancelControlID="imgCancelCourse" BackgroundCssClass="modalBackground"
        TargetControlID="imgaddcourse" PopupControlID="pnlAddCourse" runat="server"></ajaxToolkit:ModalPopupExtender>
          <asp:ValidationSummary ID="validSummryEdit" runat="server" ValidationGroup="Group1" ShowSummary="true" />
      <asp:ValidationSummary ID="validSummryAddNew" runat="server" ValidationGroup="Group2" ShowSummary="true" />
      <asp:ValidationSummary ID="validSummryUpdate" runat="server" ValidationGroup="Group3" ShowSummary="true" />
      <asp:ValidationSummary ID="ValidationSummaryFilter" runat="server" ValidationGroup="Group4" ShowSummary="true" />
      <asp:Label ID="lblException" runat="server" EnableViewState="False" 
          ForeColor="#FF3300" ></asp:Label>
    </div>

      <div class="form-group">
          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Department%></label>
              <div class="col-sm-8">
                     <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True"
                                    onselectedindexchanged="ddlDepartment_SelectedIndexChanged" ></asp:DropDownList>
              </div>
          </div>
          <div class="col-xs-6">
                 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Area%></label>
              <div class="col-sm-8">
                    <asp:DropDownList ID="ddlArea" runat="server"></asp:DropDownList>
              </div>
          </div>
      </div>
      <div class="form-group">
          <div class="col-xs-6">
       <label class="col-sm-4 control-label">Employee</label>
              <div class="col-sm-8">
                     <asp:DropDownList ID="ddlEmployees" runat="server"></asp:DropDownList>
              </div></div>
          <div class="col-xs-6">
                 <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Course%></label>
              <div class="col-sm-8 form-inline">
                      <asp:DropDownList ID="ddlEmpCourses" runat="server"></asp:DropDownList>
                     <asp:Button ID="imgbtnView" SkinID="btnDefault" runat="server" Text="View"
                          ValidationGroup="Group4" onclick="imgbtnView_Click" />
              </div>
          </div>
      </div>
    <div class="form-group">
        <asp:GridView ID="grd_trBookingRecord" runat="server" 
          onrowdatabound="grd_trBookingRecord_RowDataBound" 
          onrowcommand="grd_trBookingRecord_RowCommand" 
          onrowediting="grd_trBookingRecord_RowEditing" 
          onrowupdating="grd_trBookingRecord_RowUpdating" 
          onrowcancelingedit="grd_trBookingRecord_RowCancelingEdit" 
          SelectedRowStyle-BackColor="BlanchedAlmond" Width="100%" 
         AllowPaging="true" PageSize="20" 
          onpageindexchanging="grd_trBookingRecord_PageIndexChanging" >
            <Columns>
            <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            <ItemTemplate >
                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit" 
                CommandArgument='<%#Bind("ID")%>'  ToolTip="Edit" SkinID="BtnLinkEdit" />
            </ItemTemplate>
             <EditItemTemplate>
             
                  <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                    CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate" CausesValidation="true"
                              ValidationGroup="Group1"                  ToolTip="Update"></asp:LinkButton>
                   <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                        SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                        <%-- <asp:ImageButton ID="LinkButtonAdd" runat="server" CausesValidation="true" ValidationGroup="Group2" CommandName="AddNew" 
                CommandArgument="<%#Bind('ID')%>" ToolTip="Edit" ImageUrl="~/media/btn_add_new.gif" />--%>
                 <asp:LinkButton ID="LinkButtonAddNew" runat="server" CommandName="AddNew" 
                    CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkAdd" CausesValidation="true"
                              ValidationGroup="Group2"         ToolTip="AddNew"></asp:LinkButton>
                              <asp:LinkButton ID="LinkButtonCancel1" runat="server" CausesValidation="false"
                                   CommandName="Clear" SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                        </FooterTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Date of Course">
            <EditItemTemplate>
            <asp:TextBox Text='<%#Bind("DateofCourse","{0:d}") %>' ID="txtDate" runat="server" SkinID="Date">
            </asp:TextBox>
                <asp:Label ID="imgDate" runat="server" SkinID="Calender"></asp:Label>
                <ajaxToolkit:CalendarExtender ID="cldExtender1"   CssClass="MyCalendar"
                TargetControlID="txtDate" PopupButtonID="imgDate" runat="server">
                </ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="RV1" runat="server" ErrorMessage="Please enter date"
                ControlToValidate="txtDate" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="REV1" runat="server" ErrorMessage="Please enter valid date"
                    ControlToValidate="txtDate" ValidationGroup="Group1" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" Display="None">
                    </asp:RegularExpressionValidator>
                
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="lblDateOfCourse" runat="server" Text='<%#Bind("DateofCourse","{0:d}") %>'></asp:Label>
            </ItemTemplate>
      
            <FooterTemplate >
            <asp:TextBox ID="txtDateFooter" runat="server" SkinID="Date" ></asp:TextBox>
                <asp:Label ID="imgDateFooter" runat="server" SkinID="Calender"></asp:Label>
                <ajaxToolkit:CalendarExtender ID="cldExtenderFooter" PopupButtonID="imgDateFooter" TargetControlID="txtDateFooter"
                 CssClass="MyCalendar" runat="server"></ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="RVFooter" runat="server" ErrorMessage="Please enter date"
                ControlToValidate="txtDateFooter" Display="None"  ValidationGroup="Group2"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RExpFooter" runat="server" ErrorMessage="Please Enter valid date" 
                ControlToValidate="txtDateFooter" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ValidationGroup="Group2"
                 Display="None"></asp:RegularExpressionValidator>
            </FooterTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Employee/User">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label ID="lblEmployee" runat="server" Text='<%#Bind("EmployeeName")%>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlEmployee"  runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Group1" Display="None"
                ErrorMessage="Please select Employee/User." ControlToValidate="ddlEmployee" InitialValue="0"></asp:RequiredFieldValidator>
            </EditItemTemplate>
            <FooterTemplate>
                <asp:DropDownList ID="ddlEmployee_footer" ValidationGroup="Group2" runat="server"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select Employee/User."
                Display="None"  InitialValue="0" ControlToValidate="ddlEmployee_footer" ValidationGroup="Group2"></asp:RequiredFieldValidator>
               
            </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Course category">
                  <HeaderStyle HorizontalAlign="Center" />
                    
                     <EditItemTemplate>
                        <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"  >
                          </asp:DropDownList>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please select category." 
                         InitialValue="0" ControlToValidate="ddlCategory" Display="None" ValidationGroup="Group1" >
                         </asp:RequiredFieldValidator>
                      </EditItemTemplate>
                     <ItemTemplate>
                     <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                      </ItemTemplate>
                      <FooterTemplate>
                     <asp:DropDownList ID="ddlCategory_footer" ValidationGroup="Group2" runat="server" 
                         AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_footer_SelectedIndexChanged" >
                     </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please select Category." 
                          ControlToValidate="ddlCategory_footer" InitialValue="0" Display="None" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                       </FooterTemplate>
                      </asp:TemplateField>
          <asp:TemplateField HeaderText="Course">
                     <HeaderStyle HorizontalAlign="Center" />
                       <EditItemTemplate>
                        <asp:DropDownList ID="ddlCourse"  runat="server"></asp:DropDownList>
                           <asp:LinkButton ID="imgAddCourse" runat="server" ImageAlign="Right" SkinID="BtnLinkAdd"
                                CommandName="AddCourse" CommandArgument='<%# Bind("ID")%>' />
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="Group1"
                            ErrorMessage="Please select course." Display="None" InitialValue="0" ControlToValidate="ddlCourse"></asp:RequiredFieldValidator>
                         </EditItemTemplate>
                       <ItemTemplate>
                       <asp:Label ID="lblCourse" runat="server" Text='<%# Bind("CourseTitle") %>'></asp:Label>
                         </ItemTemplate>
                         <FooterTemplate>
                         <asp:DropDownList ID="ddlCourse_footer"   runat="server"></asp:DropDownList>
                           <asp:LinkButton ID="imgAddCourse" runat="server" ImageAlign="Right" SkinID="BtnLinkAdd" CommandName="AddCourse"
                                CommandArgument='<%# Bind("ID")%>' />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please select course."
                              Display="None" ControlToValidate="ddlCourse_footer" ValidationGroup="Group2" InitialValue="0" ></asp:RequiredFieldValidator>
                          </FooterTemplate>
                         </asp:TemplateField>    
  <asp:TemplateField HeaderText="Comments">
                                     <HeaderStyle HorizontalAlign="Center" />
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtComments" runat="server" Text='<%# Bind("Comments") %>'></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="txtComments"
                                            Display="None" ValidationGroup="Group1" ErrorMessage="Please enter comments."></asp:RequiredFieldValidator>--%>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblComments" runat="server" Text='<%# Bind("Comments") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtComments_footer" runat="server"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RVComments" runat="server" ValidationGroup="Group2"
                                              Display="None" ControlToValidate="txtComments_footer"  ErrorMessage="Please enter comments."></asp:RequiredFieldValidator>--%>
                                        </FooterTemplate>
                                    </asp:TemplateField> 
                                    
                              <asp:TemplateField HeaderText="Attachments" ItemStyle-HorizontalAlign="Center">
                              <ItemTemplate> 
                               <asp:ImageButton ID="btnDownload" ToolTip="Download" runat="server" CommandName="Download"
                                    CommandArgument='<%# Bind("ID") %>' Visible='<%#isFileExist(DataBinder.Eval(Container.DataItem,"FileName").ToString())%>'
                                    ImageUrl="~/media/ico_download.png" />
                               
                               </ItemTemplate>
                              </asp:TemplateField>
                               <asp:TemplateField HeaderText="Feedback Received" ItemStyle-HorizontalAlign="Center">
                              <ItemTemplate> 
                               <asp:ImageButton ID="btnView" runat="server"  ToolTip="View" CommandName="View" CommandArgument='<%# Bind("ID") %>' Visible='<%#isFeedbackExist(DataBinder.Eval(Container.DataItem,"ID").ToString())%>' ImageUrl="~/media/ico_tick.png" />
                               <%--<asp:Image ID="btnMailSent" runat="server"   CommandArgument='<%# Bind("ID") %>' Visible='<%#IsMailSent(DataBinder.Eval(Container.DataItem,"ID").ToString())%>' ImageUrl="~/media/ico_tick_grey.png" />--%>
                                  <asp:Image ID="imgMail" ToolTip="Mail Sent" runat="server" Visible='<%#IsMailSent(DataBinder.Eval(Container.DataItem,"ID").ToString())%>' ImageUrl="~/media/ico_tick_grey.png" />
                               </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="Status">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlStatus" runat="server" Width="110px" >
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please select status."
                                             InitialValue="0" ControlToValidate="ddlStatus" Display="None" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("StatusName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlStatus_footer" ValidationGroup="Group2" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  Display="None" InitialValue="0"
                                            ErrorMessage="Please select status." ControlToValidate="ddlStatus_footer" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                              </FooterTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="More options" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header_bg_r" ItemStyle-Width="75px" FooterStyle-Width="75px">
                                    <ItemTemplate>
                                    <asp:LinkButton ID="btnMoreOptions" runat="server"   CommandName="Select"
                                         CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkMore" />
                                    </ItemTemplate>
                                    </asp:TemplateField>                                     
            </Columns>
            </asp:GridView>
    </div>

      <div class="form-group">
          <div class="col-xs-6">
               <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.CategoryType%></label>
              <div class="col-sm-8">
                      <asp:DropDownList ID="ddlCategoryUpdate" runat="server" 
                            onselectedindexchanged="ddlCategoryUpdate_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList> 
              </div>
          </div>
          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.CourseTitle%></label>
              <div class="col-sm-8">
                    <asp:DropDownList ID="ddlCourseUpdate" runat="server"></asp:DropDownList>
              </div>
          </div>
      </div>
    <div class="form-group">
          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.CourseVenue%></label>
              <div class="col-sm-8">
                       <asp:TextBox ID="txtCourseVenue" runat="server"></asp:TextBox>
              </div>
        </div>
          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.BookingDate%></label>
              <div class="col-sm-8 form-inline">
                    <asp:TextBox ID="txtBookingDate" runat="server" SkinID="Date"></asp:TextBox>
                        <asp:Label ID="imgCalenderBookingDate" runat="server" SkinID="Calender"></asp:Label>
                        <ajaxToolkit:CalendarExtender PopupButtonID="imgCalenderBookingDate" TargetControlID="txtBookingDate"
                        runat="server" CssClass="MyCalendar"  ID="cldExtenderBooking"></ajaxToolkit:CalendarExtender>
                        <asp:RequiredFieldValidator  ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please enter booking date" Display="None"
                        ControlToValidate="txtBookingDate" ValidationGroup="Group3" >
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ControlToValidate="txtBookingDate" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                            ID="RegularExpressionValidator1" runat="server" 
                            ErrorMessage="Please enter valid date" Display="None" ValidationGroup="Group3"></asp:RegularExpressionValidator>
              </div>
          </div>
      </div>
    <div class="form-group">
          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Assessor%></label>
              <div class="col-sm-8">
                     <asp:DropDownList ID="ddlCheckedBy" runat="server"></asp:DropDownList>
              </div>
          </div>
          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Instructor%></label>
              <div class="col-sm-8">
                     <asp:TextBox ID="txtInstructor" runat="server"></asp:TextBox>
              </div>
          </div>
      </div>
    <div class="form-group">
          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.StartDate%></label>
              <div class="col-sm-8 form-inline">
                    <asp:TextBox ID="txtStartTime" SkinID="txt_70" runat="server"></asp:TextBox><span>(hh:mm)</span>
                      <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="Please enter start time"
                        ControlToValidate="txtStartTime" Display="None" ValidationGroup="Group3" ></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                            ControlToValidate="txtStartTime" Display="None" 
                            ErrorMessage="Please enter valid start time" 
                            ValidationExpression="(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])$)|(^([0-9]|[1][0-9]|[2][0-4])$)" 
                            ValidationGroup="Group3"></asp:RegularExpressionValidator>
              </div>
          </div>
          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Status%></label>
              <div class="col-sm-8">
                     <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
              </div>
          </div>
      </div>
    <div class="form-group">
          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Budget%></label>
              <div class="col-sm-8">
                   <asp:TextBox ID="txtBudget" runat="server"></asp:TextBox>
                        <%-- <asp:RequiredFieldValidator ID="reqBudget" runat="server" ErrorMessage="Please enter budget" ValidationGroup="Group3"
                         Display="None" ControlToValidate="txtBudget"></asp:RequiredFieldValidator>--%>
                          <asp:CompareValidator ID="cmpBudget" runat="server" 
                            ControlToValidate="txtBudget" Display="None" 
                            ErrorMessage="Please enter valid budget" Type="Double" ValidationGroup="Group3" 
                            Operator="DataTypeCheck"></asp:CompareValidator>
              </div>
          </div>
          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.DateAssessed%></label>
              <div class="col-sm-8 form-inline">
                   <asp:TextBox ID="txtCheckedDate" runat="server" SkinID="Date"></asp:TextBox>
                        <asp:Label ID="imgCheckedDate" runat="server" SkinID="Calender"></asp:Label>
                        <ajaxToolkit:CalendarExtender PopupButtonID="imgCheckedDate" TargetControlID="txtCheckedDate"
                        runat="server" CssClass="MyCalendar"  ID="CalendarExtenderChkDate"></ajaxToolkit:CalendarExtender>
                      <%--  <asp:RequiredFieldValidator ValidationGroup="Group3" Display="None" ControlToValidate="txtCheckedDate"
                            ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please enter checked date"></asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ID="RegularExpressionValidator2" 
                            runat="server" ErrorMessage="Please enter valid date" Display="None" ValidationGroup="Group3" ControlToValidate="txtCheckedDate">
                        </asp:RegularExpressionValidator>
              </div>
          </div>
      </div>
     <div class="form-group">
          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Outcome%></label>
              <div class="col-sm-8">
                     <asp:DropDownList ID="ddlOutCome" runat="server"></asp:DropDownList>
              </div>
         </div>
         <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ActualCost%></label>
              <div class="col-sm-8">
                   <asp:TextBox ID="txtCostOfCourse" runat="server"></asp:TextBox>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Please enter cost of course" ValidationGroup="Group3"
                         Display="None" ControlToValidate="txtCostOfCourse"></asp:RequiredFieldValidator>--%>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                            ControlToValidate="txtCostOfCourse" Display="None" 
                            ErrorMessage="Enter cost of course" Type="Double" ValidationGroup="Group3" 
                            Operator="DataTypeCheck"></asp:CompareValidator>
              </div>
          </div>

      </div>
    <div class="form-group">
          
          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.EndTime%></label>
              <div class="col-sm-8 form-inline">
                   <asp:TextBox ID="txtEndOfTime" runat="server" SkinID="txt_70"></asp:TextBox>
                        <span>(hh:mm)</span><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server"
                            ErrorMessage="Please enter end time" ControlToValidate="txtEndOfTime" ValidationGroup="Group3" Display="None"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator 
                            ID="RegularExpressionValidator4" runat="server" 
                            ControlToValidate="txtEndOfTime" Display="None" 
                            ErrorMessage="Please enter valid end time" 
                            ValidationExpression="(^([0-9]|[0-1][0-9]|[2][0-3]):([0-5][0-9])$)|(^([0-9]|[1][0-9]|[2][0-4])$)" 
                            ValidationGroup="Group3"></asp:RegularExpressionValidator>
              </div>
          </div>
        <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.NotifydaysPrior%></label>
              <div class="col-sm-8">
                   <asp:TextBox ID="txtNotifyDaysPrior" runat="server"></asp:TextBox>
                                 <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                                        ControlToValidate="txtNotifyDaysPrior" Display="None" 
                                        ErrorMessage="Please enter notify day prior" ValidationGroup="Group3"></asp:RequiredFieldValidator>--%>
                                    <asp:CompareValidator ID="CompareValidator5" runat="server" 
                                        ControlToValidate="txtNotifyDaysPrior" 
                                        Display="None" ErrorMessage="Please enter valid notify days" Type="Double" 
                                        ValidationGroup="Group3" Operator="DataTypeCheck"></asp:CompareValidator> 
              </div>
          </div>
      </div>
    <div class="form-group">
          
          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Expenses%></label>
              <div class="col-sm-8">
                   <asp:TextBox ID="txtExpenses" runat="server"></asp:TextBox>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                            ControlToValidate="txtExpenses" Display="None" 
                            ErrorMessage="Please enter expenses" ValidationGroup="Group3"></asp:RequiredFieldValidator>--%>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" 
                            ControlToValidate="txtExpenses" Display="None" 
                            ErrorMessage="Please enter valid expenses" Type="Double" 
                            ValidationGroup="Group3" Operator="DataTypeCheck"></asp:CompareValidator>
              </div>
          </div>
          <div class="form-group">
          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.DurationinDays%></label>
              <div class="col-sm-8">
                   <asp:TextBox ID="txtDurationInDays" runat="server" Width="100px"></asp:TextBox>
                       
                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                            ControlToValidate="txtDurationInDays" Display="None" 
                            ErrorMessage="Please enter duration in days" ValidationGroup="Group3"></asp:RequiredFieldValidator>--%>
                             <asp:CompareValidator ID="CompareValidator6" runat="server" 
                            ControlToValidate="txtDurationInDays" 
                            Display="None" ErrorMessage="Please enter valid  days" Type="Integer" 
                            ValidationGroup="Group3" Operator="DataTypeCheck"></asp:CompareValidator>
              </div>
          </div>
          
      </div>
      </div>
     
     <div class="form-group">

         <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Penalties%></label>
              <div class="col-sm-8">
                   <asp:TextBox ID="txtPenalities" runat="server"></asp:TextBox>
                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                            ControlToValidate="txtPenalities" Display="None" 
                            ErrorMessage="Please enter penalties" ValidationGroup="Group3"></asp:RequiredFieldValidator>--%>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" 
                            ControlToValidate="txtPenalities" Display="None" 
                            ErrorMessage="Please enter valid penalties" Type="Double" 
                            ValidationGroup="Group3" Operator="DataTypeCheck"></asp:CompareValidator>
              </div>
          </div>


          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.EndDate%></label>
              <div class="col-sm-8 form-inline">
                       <asp:TextBox ID="txtEndDate" runat="server" SkinID="Date"></asp:TextBox>
                        <asp:Label ID="imgEndDate" runat="server" SkinID="Calender"></asp:Label>
                        <ajaxToolkit:CalendarExtender PopupButtonID="imgEndDate" TargetControlID="txtEndDate"
                        runat="server" CssClass="MyCalendar"  ID="CalendarExtender2"></ajaxToolkit:CalendarExtender>
                      <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please enter end date" ValidationGroup="Group3" ControlToValidate="txtEndDate"
                        Display="None" ></asp:RequiredFieldValidator>--%><asp:RegularExpressionValidator
                            ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please enter valid date" Display="None" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                             ControlToValidate="txtEndDate" ValidationGroup="Group3"></asp:RegularExpressionValidator>
              </div>
          </div>


          
      </div>
    
     <div class="form-group">

         <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Feedback%></label>
              <div class="col-sm-8">
                     <asp:TextBox ID="txtFeedBack" runat="server" Height="75px" TextMode="MultiLine" 
                            Width="600px"></asp:TextBox>
              </div>
          </div>


          <div class="col-xs-6">
              <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Uploadfile%></label>
              <div class="col-sm-8">
                    <asp:FileUpload ID="bookingFileUpload" runat="server"/>
              </div>
          </div>
         
      </div>
         <div class="form-group">
        <div class="col-xs-6">
              <label class="col-sm-4 control-label">  <asp:Label ID="Image1" SkinID="Mail" runat="server"></asp:Label>
                  <%= Resources.DeffinityRes.NotifyOfUsers%></label>
              <div class="col-sm-6">
              <asp:ListBox ID="lstNotifyUser" runat="server"
                             SelectionMode="Multiple"></asp:ListBox>
              </div>
          </div>
    </div>
     <div class="form-group">
          <div class="col-xs-6">
              <asp:Button ID="imgSubmitButton" runat="server" SkinID="btnSubmit" ValidationGroup="Group3" 
                            onclick="imgSubmitButton_Click" />&nbsp;
                            <asp:Button ID="imgCancel" runat="server" SkinID="btnCancel"
                            onclick="imgCancel_Click" />
                            &nbsp;
                            <asp:Button ID="btnFeedBack" SkinID="btnDefault" Text="Submit FeedBack"
                                 runat="server" onclick="btnFeedBack_Click" />
                            <asp:Button ID="btnCourseFeedback" Text="Course Feedback" SkinID="btnDefault"
                                 runat="server" onclick="btnCourseFeedback_Click"  
                            />
                            <asp:HiddenField ID="trid" runat="server" Value="0" />
                            <asp:HiddenField ID="crId" runat="server" Value="0" />
                            <br />
          </div>
      </div>
    <script language="javascript" type="text/javascript">
    function MutExChkList(chk) {
        var chkList = chk.parentNode.parentNode.parentNode;
        var chks = chkList.getElementsByTagName("input");
        for (var i = 0; i < chks.length; i++) {
            if (chks[i] != chk && chk.checked) {
                chks[i].checked = false;
            }
        }
    }

</script>
 
            
          
    
                    <div runat="server" id="divCourseReOccurence">
                      <div class="tab_subheader" style="border-bottom:solid 1px Silver;width:98%;"> Course Reoccurrence  </div>
                        <table >
      <tr> <td>Course reoccurs:</td><td>
      <asp:TextBox ID="txtCourseReoccurs" runat="server" Width="100px"></asp:TextBox>
      <asp:RangeValidator ID="rngCO" runat="server" 
          ErrorMessage="Enter course reoccurs between 1 and 12" 
          ControlToValidate="txtCourseReoccurs" Display="None" MaximumValue="12" 
          MinimumValue="0" ValidationGroup="Group3" Type="Integer"></asp:RangeValidator>
      </td>
      <td>Reoccurrence Frequency:</td><td>
          <asp:DropDownList ID="ddlReoccursFrequencey" runat="server" Width="150px">
          </asp:DropDownList>
      </td></tr>
      <tr><td align="left">On Which Day:</td><td>
          <asp:CheckBoxList ID="chkDays" runat="server" RepeatDirection="Horizontal"  >
          <asp:ListItem Value="9">Mon</asp:ListItem>
          <asp:ListItem Value="10">Tues</asp:ListItem>
          <asp:ListItem Value="11">Wed</asp:ListItem>
          <asp:ListItem Value="12">Thu</asp:ListItem>
            <asp:ListItem Value="13">Fri</asp:ListItem>
            <asp:ListItem Value="14">Sat</asp:ListItem>
            <asp:ListItem Value="15">Sun</asp:ListItem>
          
          </asp:CheckBoxList>
      </td><td>Until Date:</td><td > <asp:TextBox ID="txtUntilDate" runat="server" Width="100px"></asp:TextBox>
                        <asp:Image ID="imgUntilDate" runat="server" ImageAlign="Middle" SkinID="Calender" />
                        <ajaxToolkit:CalendarExtender PopupButtonID="imgUntilDate" TargetControlID="txtUntilDate"
                        runat="server" CssClass="MyCalendar"  ID="CalendarExtender1"></ajaxToolkit:CalendarExtender>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please enter end date" ValidationGroup="Group3" ControlToValidate="txtEndDate"
                        Display="None" ></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="RegularExpressionValidator6" runat="server" ErrorMessage="Please enter valid date" Display="None" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                             ControlToValidate="txtUntilDate" ValidationGroup="Group3"></asp:RegularExpressionValidator><asp:CompareValidator
                                 ID="CompareValidator4" runat="server" 
              ControlToCompare="txtEndDate" ControlToValidate="txtUntilDate" Display="None" ValidationGroup="Group3"
                                  Type="Date"
                                  
              ErrorMessage="Untile date should be greater than end date" 
              Operator="GreaterThan"></asp:CompareValidator></td></tr>
     
  </table>
               </div>       
            <div><asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label> </div>
         
         
        
        <asp:Panel ID="pnlAreaDownload" runat="server"  BackColor="White" style="display:none" Width="400px"
            BorderStyle="Double" BorderColor="LightSteelBlue" Visible="true">
            <div style="float:right" >
                   <asp:ImageButton ID="imgClose" runat="server"  SkinID="ImgSymCancel" />

            </div>
           <div class="tab_subheader" style="border-bottom:solid 1px Silver;width:98%">Download</div>
           <asp:GridView ID="grdDownloadFiles" runat="server" AutoGenerateColumns="false" 
    onrowcommand="grdDownloadFiles_RowCommand"
     onrowdatabound="grdDownloadFiles_RowDataBound" 
        EmptyDataText="No records found" Visible="true" Width="100%" >
    <Columns>
   <%--<asp:TemplateField HeaderText="Sl_No" HeaderStyle-CssClass="header_bg_l" ItemStyle-HorizontalAlign="Center">
    <ItemStyle Width="90px" />
                              <FooterStyle Width="50px" />
    <ItemTemplate>
        <asp:Label ID="lblSlNo" runat="server" ></asp:Label>
    </ItemTemplate>
    
    </asp:TemplateField>--%>
    <asp:TemplateField HeaderText="File Name" ItemStyle-HorizontalAlign="Center" 
            HeaderStyle-CssClass="header_bg_l" >
    <ItemTemplate>
        <asp:Label ID="lblFileName" runat="server" Text='<%#Bind("FileName")%>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
     <asp:TemplateField HeaderText="Download"
            ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header_bg_r">
                              <ItemTemplate> 
                               <asp:LinkButton ID="btnDownloadFile" runat="server" CommandName="Download" 
                                      CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkDownload"></asp:LinkButton>
                               </ItemTemplate>
                              </asp:TemplateField>
    </Columns>
    </asp:GridView>
            <asp:Button runat="server" ID="btnShowModalPopup" style="display:none"/>
      </asp:Panel>
      
       
          <asp:Panel ID="pnlAddCourse" runat="server"  BackColor="White" style="display:none" Width="400px"
              BorderStyle="Double" BorderColor="LightSteelBlue" Visible="true">

               <div class="form-group">
                                <div class="col-md-12 text-bold">
                                       <strong>   <%= Resources.DeffinityRes.Course%></strong>
                                </div>
                </div>
                <div class="form-group">
                                 <div class="col-xs-12">
                                        <div class="form-inline">
                                              <asp:TextBox ID="txtAddCourse" runat="server"></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ErrorMessage="Please enter course title"
                                                              Display="None" ControlToValidate="txtAddCourse" ValidationGroup="Group5" ></asp:RequiredFieldValidator>
                                               <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="Group5" />
                                                 <asp:Label ID="lblError" runat="server"></asp:Label>
                                        </div>
                                 </div>
                    </div>
               <div class="form-group">
                                 <div class="col-xs-12">
                                        <div class="form-inline">
                                            <asp:Button ID="imgBtnAdd" runat="server" SkinID="btnSubmit"
                                                 ValidationGroup="Group5" OnClick="btnAddCourse_Click" />
                                            <asp:Button ID="imgCancelCourse" runat="server" SkinID="btnCancel" />
                                            <asp:Button runat="server" ID="imgaddcourse" style="display:none"/>

                                        </div>
                                 </div>
                    </div>
<div>

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


