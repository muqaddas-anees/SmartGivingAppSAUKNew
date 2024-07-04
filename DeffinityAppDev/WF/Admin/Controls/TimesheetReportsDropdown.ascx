<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_TimesheetReportsDropdown" Codebehind="TimesheetReportsDropdown.ascx.cs" %>
<div class="form-group">
          <div class="col-md-12">
               <label class="col-sm-4 control-label">Timesheet Report :</label>
               <div class="col-sm-8">
                   <select id='ddlReport' >
    <option value="0">Please select...</option>
    <option value="../../WF/Reports/TimeSheetSummaryBasedonEntryType.aspx" >Timesheet Summary – Approved entries only</option>
     <option value="../../WF/Reports/TimesheetSummaryComplete.aspx">Timesheet Summary – Complete Journal</option>
    <option value="../../WF/Reports/TimesheetTaksSumaryaspx.aspx">Timesheet Task Summary</option>
    <option value="../../WF/Reports/TimeBookingforTask.aspx">Task Time Booking Summary</option>
    <option value="../../WF/Reports/AbsenceReport.aspx">Absence Summary</option>
    <option value="../../WF/Reports/ApproversReport.aspx">Approvers</option>
   <!-- <option value="Reports/MyTasksTimeSheetReport.aspx">My Tasks Timesheet</option>
    <option value="Reports/OvertimeCutOffReport.aspx">Overtime Cut Off</option> -->
    <option value="../../WF/Reports/ProjectTimeBookingReport.aspx">Project Time Booking </option>
   <!-- <option value="Reports/TimesheetMonthEndException.aspx">Timesheet Month End Exception</option> -->
    <option value="../../WF/Reports/TimesheetSummaryofHoursandType.aspx">Summary of hours by type</option>
</select>
               </div>
          </div>
</div>
 

                          
                           <script type="text/javascript">
                               $('#ddlReport').change(function() {
                                   if ($(this).val() != "0") {
                                       window.open($(this).val(), '_blank');
                                       $(this).val("0");
                                   }
                               });
                              
                           </script>