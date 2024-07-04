<%@ Page Language="C#" AutoEventWireup="true" Inherits="Reports_TimesheetSummaryofHoursandType" MasterPageFile="~/WF/MainFrame.Master"
         Codebehind="TimesheetSummaryofHoursandType.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
    <asp:Label ID="lbl1" runat="server" Text="Timesheet Summary of Hours by type and Status" Font-Size="Large"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
          <script language="javascript" type="text/javascript">
           var retval;
           function Setheight() {
               if (retval == null || retval == true) {
                   retval = false;
                   document.getElementById("div1").style.height = 180;
               }
               else {
                   retval = true;
                   document.getElementById("div1").style.height = 20;
               }

               return false;
           }
           function calendarShown(sender, args) { Setheight(); }
   </script>
      <div class="form-group">
           <asp:ValidationSummary ID="V1" runat="server" ValidationGroup="one" />
                  
                 <asp:CompareValidator ID="c1" runat="server" ControlToCompare="txt_StartDate" ControlToValidate="txt_EndDate"
                        Display="none" Type="Date" Operator="GreaterThanEqual" ErrorMessage="start date can not greater then end date" ValidationGroup="one" ></asp:CompareValidator>      
                        
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txt_StartDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one" >*</asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_EndDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one">*</asp:RegularExpressionValidator>
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
      </div>
       <div id="div1">
           <div class="form-group">
      <div class="col-xs-4">
           <label class="col-sm-3 control-label">Customer:</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlCustomers" runat="server" AutoPostBack="True" SkinID="ddl_70"
                                      OnSelectedIndexChanged="ddlCustomers_SelectedIndexChanged" ></asp:DropDownList>
            </div>
	</div>
    <div class="col-xs-4">
           <label class="col-sm-3 control-label">Team:</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlTeam" runat="server" AutoPostBack="True" SkinID="ddl_70"
                                       OnSelectedIndexChanged="ddlTeam_SelectedIndexChanged"></asp:DropDownList>
            </div>
	</div>
	<div class="col-xs-4">
           <label class="col-sm-3 control-label">Resource:</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlcontractors" runat="server" SkinID="ddl_70"></asp:DropDownList>
            </div>
	</div>
</div>
           <div class="form-group">
      <div class="col-xs-4">
           <label class="col-sm-3 control-label">Sort By </label>
           <div class="col-sm-9">
                 <asp:DropDownList ID="ddlSort" runat="server" SkinID="ddl_70">
                     <asp:ListItem Text="FirstName" Value="1">First Name</asp:ListItem>
                     <asp:ListItem Text="LastName" Value="2">Surname</asp:ListItem>
                 </asp:DropDownList>
            </div>
	</div>
	<div class="col-xs-4">
           <label class="col-sm-3 control-label">Start&nbsp;Date:</label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txt_StartDate" runat="server" MaxLength="10" SkinID="Date"></asp:TextBox>
               <asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender"></asp:Label>
            </div>
	</div>
	<div class="col-xs-4">
           <label class="col-sm-3 control-label">End&nbsp;date:</label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txt_EndDate" runat="server" MaxLength="10" SkinID="Date"></asp:TextBox>
               <asp:Label ID="Image1" runat="server" SkinID="Calender"></asp:Label>
               <asp:Button ID="btn_Submitt" runat="server" Text="View Reports" SkinID="btnDefault" ValidationGroup="one" OnClick="btn_Submitt_Click"  />
                 <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                            PopupButtonID="imgbtnenddate6" TargetControlID="txt_StartDate" CssClass="MyCalendar" OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                            PopupButtonID="Image1" TargetControlID="txt_EndDate" CssClass="MyCalendar" OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>
            </div>
	</div>
</div>
       </div>
      <div class="form-group">
              <div class="col-xs-4" style="float:right;text-align:right;">
                   <label class="col-sm-3 control-label"></label>
                   <div class="col-sm-9 form-inline">
                        <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="true" 
                       ForeColor="Navy" onclick="LinkButton2_Click">Excel&nbsp;format1</asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="LinkButton3" runat="server" Font-Bold="true" 
                            ForeColor="Navy" onclick="LinkButton3_Click" >Excel&nbsp;format2</asp:LinkButton>
                   </div>
              </div>
          </div>


    <div class="form-group">
        <div class="col-md-12">
           <strong>View Reports </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
      

       <div class="form-group">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatepanel1">
                        <ProgressTemplate>
                            <asp:Label ID="lblLoadding" runat="server" SkinID="Loading"></asp:Label>
                        </ProgressTemplate>
                </asp:UpdateProgress> 
                <asp:UpdatePanel ID="updatepanel1" runat="server">
                    <ContentTemplate>
                        <div style="z-index:1000;">
                            <iframe id="TimesheetSummary" name="TimesheetSummary" runat="server" frameborder="0" width="100%" height="600px" scrolling="auto"></iframe>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btn_Submitt" />
                    </Triggers>
                </asp:UpdatePanel>
       </div>
</asp:Content>
