<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/WF/MainFrame.Master"
        Inherits="Reports_ProjectTimeBookingReport" Codebehind="ProjectTimeBookingReport.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
    <asp:Label ID="lbl1" runat="server" Text="Project Time Booking Report" Font-Size="Large"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <script language="javascript" type="text/javascript">
        var retval;
        function Setheight() {
            if (retval == null || retval == true) {
                retval = false;
                document.getElementById("div1").style.height = 250;
            }
            else {
                retval = true;
                document.getElementById("div1").style.height = 120;
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
                 <div class="col-xs-8">
                        <div class="form-group">
                             <div class="col-xs-8">
                                  <label class="col-sm-3 control-label">Customer</label>
                                   <div class="col-sm-9">
                                          <asp:DropDownList ID="ddlCustomers" runat="server" DataTextField="PortFolio" SkinID="ddl_70"
                    DataValueField="ID" DataSourceID="SqlDataSourceTitle2" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomers_SelectedIndexChanged"  ></asp:DropDownList>
               <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_PermissionCustomer" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                                   </div>
                             </div>
                        </div>
                     <div class="form-group">
                             <div class="col-xs-8">
                                  <label class="col-sm-3 control-label">Project</label>
                                   <div class="col-sm-9">
                                           <asp:DropDownList ID="ddlProejcts"  runat="server" AutoPostBack="True" SkinID="ddl_70" 
                                               OnSelectedIndexChanged="ddlProejcts_SelectedIndexChanged" ondatabound="ddlProejcts_DataBound"></asp:DropDownList>
                                   </div>
                             </div>
                        </div>
                     <div class="form-group">
                             <div class="col-xs-8">
                                  <label class="col-sm-3 control-label">Project Task</label>
                                   <div class="col-sm-9">
                                       <asp:DropDownList ID="ddProjectTaks" runat="server" SkinID="ddl_70" ondatabound="ddProjectTaks_DataBound"></asp:DropDownList>
                                   </div>
                             </div>
                        </div>
                     <div class="form-group">
                             <div class="col-xs-8">
                                  <label class="col-sm-3 control-label">Start&nbsp;Date</label>
                                   <div class="col-sm-9 form-inline">
                                        <asp:TextBox ID="txt_StartDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                                        <asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender"></asp:Label>
                                   </div>
                             </div>
                        </div>
                     <div class="form-group">
                             <div class="col-xs-8">
                                  <label class="col-sm-3 control-label"> End&nbsp;date</label>
                                   <div class="col-sm-9 form-inline">
                                        <asp:TextBox ID="txt_EndDate" runat="server" SkinID="Date" MaxLength="10"></asp:TextBox>
                                        <asp:Label ID="Image1" runat="server" SkinID="Calender"></asp:Label>
                                           <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                            PopupButtonID="imgbtnenddate6" TargetControlID="txt_StartDate" CssClass="MyCalendar" OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                            PopupButtonID="Image1" TargetControlID="txt_EndDate" CssClass="MyCalendar" OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>

                                   </div>
                             </div>
                        </div>
                     <div class="form-group">
                             <div class="col-xs-8">
                                  <label class="col-sm-3 control-label"></label>
                                   <div class="col-sm-9">
                                           <asp:Button ID="btn_Submitt" runat="server" SkinID="btnDefault" Text="View Reports"
                                                                           ValidationGroup="one" OnClick="btn_Submitt_Click"   />
                                           &nbsp;&nbsp;<asp:LinkButton ID="btnExportExcel" runat="server" Font-Bold="true"
                                                                           onclick="btnExportExcel_Click" ForeColor="Navy">Excel&nbsp;format1</asp:LinkButton>
                                                       <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="true" 
                                                                                 ForeColor="Navy" onclick="LinkButton1_Click">Excel&nbsp;format2</asp:LinkButton>
                                   </div>
                             </div>
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
