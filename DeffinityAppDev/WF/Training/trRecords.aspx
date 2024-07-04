<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_trRecords" Codebehind="trRecords.aspx.cs" %>

<%@ Register src="controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:TrainingTabs ID="TrainingTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>    
      <td>
          <h1 class="section2">
              <span>
                  <label id="lblTitle" runat="server">
                  </label>
              </span>
          </h1>
          
      </td>
  </tr>
  <tr>    
    <td class="p_section2 data_carrier_block" valign="top">
    <div class="tab_subheader" style="border-bottom:solid 1px Silver;width:90%;">
Training Booking Record
</div>
<div><div style="float:right;width:150">Department</div> <div style="float:right;width:150"><asp:DropDownList ID="ddlDepartment" runat="server" 
        Width="250px"></asp:DropDownList></div> </div>
    <div>
    <asp:GridView ID="grid_training" runat="server" 
            onrowdatabound="grid_training_RowDataBound">
    <Columns>
     <asp:TemplateField  HeaderStyle-CssClass="header_bg_l">
                                        <HeaderStyle Width="45px" />
                                        <ItemStyle Width="45px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                CommandArgument="<%# Bind('ID')%>" ImageUrl="~/media/ico_edit.png" ToolTip="Edit">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                CommandArgument="<%# Bind('ID')%>" ValidationGroup="Group2" ImageUrl="~/media/ico_update.png"
                                                ToolTip="Update"></asp:ImageButton>
                                            <asp:ImageButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                ImageUrl="~/media/ico_cancel.png" ToolTip="Cancel"></asp:ImageButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date of Course">
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle Width="110px" />
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDate" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'
                                                Width="70px"></asp:TextBox>
                                            <asp:Image ID="imgbtnenddate6" runat="server" SkinID="Calender" ImageAlign="Middle" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                                                PopupButtonID="imgbtnenddate6" TargetControlID="txtDate" CssClass="MyCalendar">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDate"
                                                Display="None" ErrorMessage="Please enter date" ValidationGroup="Group2"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDate"
                                                Display="None" ErrorMessage="Please enter valid date " ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                                ValidationGroup="Group2">*</asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("DateEntered","{0:d}") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Width="110px" />
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtDate_footer" runat="server" Width="75px"></asp:TextBox><asp:Image
                                                ID="imgbtnenddate_footer" runat="server" SkinID="Calender" ImageAlign="Middle" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_footer"  runat="server"
                                                PopupButtonID="imgbtnenddate_footer" TargetControlID="txtDate_footer"
                                                CssClass="MyCalendar">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RT6" runat="server" ControlToValidate="txtDate_footer"
                                                Display="None" ErrorMessage="Please enter date" ValidationGroup="TimeSheetFooter"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="ReT6" runat="server" ControlToValidate="txtDate_footer"
                                                Display="None" ErrorMessage="Please enter valid date" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                                                ValidationGroup="TimeSheetFooter">*</asp:RegularExpressionValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Employee/User">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="140px" />
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlEmployee" runat="server" Width="140px">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployee" runat="server" Width="140px" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlEmployee_footer" runat="server" Width="140px">
                                            </asp:DropDownList>
                                              </FooterTemplate>
                                        <FooterStyle Width="140px" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Course category">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="140px" />
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlCategory" runat="server" Width="140px" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory" runat="server" Width="140px" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlCategory_footer" runat="server" Width="140px" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_footer_SelectedIndexChanged">
                                            </asp:DropDownList>
                                              </FooterTemplate>
                                        <FooterStyle Width="140px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Course">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="140px" />
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlCourse" runat="server" Width="140px">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCourse" runat="server" Width="140px" Text='<%# Bind("CourseTitle") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlCourse_footer" runat="server" Width="140px">
                                            </asp:DropDownList>
                                              </FooterTemplate>
                                        <FooterStyle Width="140px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" />
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtComments" runat="server" Text='<%# Bind("Comments") %>'
                                                Width="80px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblComments" runat="server" Text='<%# Bind("Comments") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtComments_footer" runat="server" Width="80px"></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="140px" />
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlStatus" runat="server" Width="140px">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Width="140px" Text='<%# Bind("StatusName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlStatus_footer" runat="server" Width="140px">
                                            </asp:DropDownList>
                                              </FooterTemplate>
                                        <FooterStyle Width="140px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="More options">
                                    <ItemTemplate>
                                    <asp:ImageButton ID="btnMoreOptions" runat="server" OnClick="btnMoreOptions_Click" CommandName="More" CommandArgument='<%# Bind("ID") %>' ImageUrl="~/media/ico_more_options.png" />
                                    </ItemTemplate>
                                    </asp:TemplateField>
    </Columns>
    </asp:GridView>
    </div>
    <div class="clr"></div>
    <div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
    <td>Checked By</td><td><asp:DropDownList ID="ddlCheckedBy" runat="server"></asp:DropDownList> </td>
    <td>Chekced Date</td><td><asp:TextBox ID="txtChekcedDate" runat="server" Width="100px"></asp:TextBox>
                                            <asp:Image ID="imgChekcedDate" runat="server" SkinID="Calender" ImageAlign="Middle" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                                                PopupButtonID="imgChekcedDate" TargetControlID="txtChekcedDate" CssClass="MyCalendar">
                                            </ajaxToolkit:CalendarExtender></td>
    <td>Required By Date</td><td><asp:TextBox ID="txtRequiredDate" runat="server" Width="100px"></asp:TextBox>
                                            <asp:Image ID="imgRequireddate" runat="server" SkinID="Calender" ImageAlign="Middle" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                                PopupButtonID="imgRequireddate" TargetControlID="txtRequiredDate" CssClass="MyCalendar">
                                            </ajaxToolkit:CalendarExtender></td>
    </tr>
     <tr>
    <td>Instructor</td><td><asp:TextBox ID="txtInstructor" runat="server" ></asp:TextBox></td>
    <td>Course Venue</td><td><asp:TextBox ID="txtCourseVenue" runat="server" ></asp:TextBox></td>
    <td>Notify days prior</td><td><asp:TextBox ID="txtNotify" runat="server" ></asp:TextBox></td>
    </tr>
     <tr>
    <td>Cost of Course</td><td><asp:TextBox ID="txtCostCourse" runat="server" ></asp:TextBox></td>
    <td>Notiry users</td><td><asp:DropDownList ID="ddlNotifyUsers" runat="server"></asp:DropDownList></td>
    <td></td><td></td>
    </tr>
     <tr>
    <td>FeedBack</td><td colspan="5"><asp:TextBox ID="txtFeedback" runat="server" Width="550px" Height="60px" TextMode="MultiLine"></asp:TextBox> </td>
    </tr>
     <tr>
    <td></td><td><asp:HiddenField ID="h_bookingid" runat="server" Value="0" /> 
         <asp:ImageButton ID="btnSubmit" runat="server" SkinID="ImgSubmit" 
             onclick="btnSubmit_Click" /> &nbsp; <asp:ImageButton ID="btnCancel" 
             runat="server" SkinID="ImgCancel" onclick="btnCancel_Click" /> </td>
    <td></td><td></td>
    <td></td><td></td>
    </tr>
    </table>
    </div>
    </td>
    </tr>
    </table>


</asp:Content>


