<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WF/MainFrame.Master" 
    Inherits="Reports_AbsenceReportfrm" Codebehind="AbsenceReport.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Absence Report" Font-Size="Large"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">
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
        function calendarShown(sender, args) { Setheight(); }
   </script>
    <div class="form-group">
         <asp:ValidationSummary ID="V1" runat="server" ValidationGroup="one" />
     
                   <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                            PopupButtonID="imgbtnenddate6" TargetControlID="txtFromDate" CssClass="MyCalendar" OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                            PopupButtonID="Image1" TargetControlID="txtToDate" CssClass="MyCalendar" OnClientShown="calendarShown" OnClientHidden="calendarShown"></ajaxToolkit:CalendarExtender>
                         <asp:CompareValidator ID="c1" runat="server" ControlToCompare="txtFromDate" ControlToValidate="txtToDate"
                                        Display="none" Type="Date" Operator="GreaterThanEqual" ErrorMessage="start date can not greater then end date" ValidationGroup="one" ></asp:CompareValidator>      
                        
                       <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter from date"
                         ControlToValidate="txtFromDate" Display="None" ValidationGroup="one"></asp:RequiredFieldValidator>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldVal2" runat="server" ErrorMessage="Please enter to date"
                         ControlToValidate="txtToDate" Display="None" ValidationGroup="one"></asp:RequiredFieldValidator>--%>
                         
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtFromDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one" >*</asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtToDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one">*</asp:RegularExpressionValidator>
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
    </div>
    <div id="div1">
         <div class="form-group">
                    <div class="col-xs-4">
           <label class="col-sm-3 control-label">Customer:</label>
           <div class="col-sm-9">
                  <asp:DropDownList ID="ddlCustomers" runat="server" AutoPostBack="false" 
                       OnSelectedIndexChanged="ddlCustomers_SelectedIndexChanged" DataTextField="PortFolio"
                        DataValueField="ID" DataSourceID="SqlDataSourceTitle2"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_PermissionCustomer" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
            </div>
	</div>
                	<div class="col-xs-4">
           <label class="col-sm-3 control-label">Team:</label>
           <div class="col-sm-9">
                        <asp:DropDownList ID="ddlTeam" runat="server"
                                                    SkinID="ddl_70" AutoPostBack="True" OnSelectedIndexChanged="ddlTeam_SelectedIndexChanged"></asp:DropDownList>
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
           <label class="col-sm-3 control-label">From&nbsp;Date:</label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtFromDate" runat="server" SkinID="Date"></asp:TextBox>
               <asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender"></asp:Label>
            </div>
	</div>
                 	<div class="col-xs-4">
           <label class="col-sm-3 control-label">To&nbsp;Date:</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtToDate" SkinID="Date" runat="server"></asp:TextBox> 
                <asp:Label ID="Image1" runat="server" SkinID="Calender"></asp:Label>
            </div>
	</div>
                 	<div class="col-xs-4">
           <label class="col-sm-3 control-label"> Order&nbsp;By</label>
           <div class="col-sm-9 form-inline">
                 <asp:DropDownList ID="ddlSortOption" runat="server" SkinID="ddl_60">
                     <asp:ListItem Selected="True" Value="1">First Name</asp:ListItem>
                     <asp:ListItem  Value="2">Surname</asp:ListItem>
                 </asp:DropDownList>
                <asp:Button ID="btn_Submitt" runat="server" SkinID="btnDefault" Text="View Report"
                                                onclick="btn_Submitt_Click" ValidationGroup="one" />
                 <asp:DropDownList ID="ddlAbsenseType" runat="server" Visible="false"></asp:DropDownList>
            </div>
	</div>
         </div>
         <div class="form-group">
              <div class="col-xs-4" style="float:right;text-align:right;">
                   <label class="col-sm-3 control-label"></label>
                   <div class="col-sm-9 form-inline">
                   &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnExportExcel" runat="server" Font-Bold="true" 
                            onclick="btnExportExcel_Click" ForeColor="Navy">Excel&nbsp;format1</asp:LinkButton>
               <asp:LinkButton ID="btnExportExcel0" runat="server" Font-Bold="true" 
                             ForeColor="Navy" onclick="btnExportExcel0_Click">Excel&nbsp;format2</asp:LinkButton>
                       </div>
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
                   <asp:Label ID="lblLoading" runat="server" SkinID="Loading"></asp:Label>
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

</asp:Content>