<%@ Page Language="C#" MasterPageFile="~/WF/MainFrame.Master"  AutoEventWireup="true"
     Inherits="Reports_MyTasksTimeSheetReport" Codebehind="MyTasksTimeSheetReport.aspx.cs" %>
  <%--  <link href="http://www.deffinity.com/dlite/media//favicon1.ico" rel="shortcut icon" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_frame.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_color_scheme.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/deffinity_custom.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/ajaxtabs.css" />
    <link rel="stylesheet" type="text/css" href="../stylcss/customer_admin.css" />--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
     <asp:Label ID="lblMsg" runat="server" Font-Size="Large"></asp:Label>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div  id="div1">
      <div class="form-group">
           <div class="col-xs-12">
             <asp:ValidationSummary ID="V1" runat="server" ValidationGroup="one" />
              <ajaxToolkit:CalendarExtender ID="CalendarExtender6"  runat="server"
                            PopupButtonID="imgbtnenddate6" TargetControlID="txtFromDate" CssClass="MyCalendar"
                        OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                            PopupButtonID="Image1" TargetControlID="txtToDate" CssClass="MyCalendar"
                             OnClientShown="calendarShown" OnClientHidden="calendarShown">
                        </ajaxToolkit:CalendarExtender>

                      <asp:CompareValidator ID="c1" runat="server" ControlToCompare="txtFromDate" ControlToValidate="txtToDate"
                        Display="none" Type="Date" Operator="GreaterThanEqual" ErrorMessage="start date can not greater then end date"
                           ValidationGroup="one" ></asp:CompareValidator>      
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter from date"
                         ControlToValidate="txtFromDate" Display="None" ValidationGroup="one"></asp:RequiredFieldValidator>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter to date"
                         ControlToValidate="txtToDate" Display="None" ValidationGroup="one"></asp:RequiredFieldValidator>
                         
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtFromDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one" >*</asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtToDate"
                        Display="None" ErrorMessage="Please enter valid date in date field" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
                        ValidationGroup="one">*</asp:RegularExpressionValidator>
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
               </div>
      </div>
      <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.FromDate%></label>
           <div class="col-sm-6 form-inline">
                 <asp:TextBox ID="txtFromDate" runat="server" SkinID="Date"></asp:TextBox>
                 <asp:Label ID="imgbtnenddate6" runat="server" SkinID="Calender"></asp:Label>
            </div>
	</div>
	  <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.ToDate%></label>
           <div class="col-sm-6 form-inline">
                 <asp:TextBox ID="txtToDate" SkinID="Date" runat="server"></asp:TextBox> 
                 <asp:Label ID="Image1" runat="server" SkinID="Calender"></asp:Label>
            </div>
	   </div>
      <div class="col-md-4">
                <asp:Button ID="btn_Submitt" runat="server" SkinID="btnView"
                                                 ValidationGroup="one" onclick="btn_Submitt_Click" />
                &nbsp;   <asp:LinkButton ID="btnExportExcel" runat="server" Font-Bold="true" 
                                                   onclick="btnExportExcel_Click" ForeColor="Navy">Excel&nbsp;format</asp:LinkButton>
            </div>
     </div>
     <div class="form-group">
         <div class="form-group">
            <div class="col-md-12">
               <strong>View Reports </strong> 
               <hr class="no-top-margin" />
            </div>
        </div>



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
</div>
    
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
    
         
</asp:Content>



      



  
