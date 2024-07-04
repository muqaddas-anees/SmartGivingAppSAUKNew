<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_TimesheetSubTabs" Codebehind="TimesheetSubTabs.ascx.cs" %>
<%--<script type="text/javascript">
    $(document).ready(function () {
        $(".tabs a").each(function (index, element) {
            if (index < 7) {
                var cu = $(location).attr('href').toLowerCase();
                var ck = $(element).attr('href').toLowerCase();
                if (cu.indexOf($.trim(ck)) > -1) {
                    $(element).attr('class', 'active');
                    $(element).parents('li').attr('class', 'active');
                    return false;
                }
            }
        });
    });
</script>--%>

 <div class="form-wizard">
 <ul id="countrytabs" class="tabs">
 <li class="ms-hover">
 <asp:HyperLink ID="lbtnTimesheet"   NavigateUrl="~/WF/Admin/Timesheet.aspx" Target="_self" runat="server" Text="Awaiting Approval"></asp:HyperLink>
 </li><li class="ms-hover">
 <asp:HyperLink ID="lbtnTimeSheetNotSubmit" NavigateUrl="~/WF/Admin/TimesheetNotSubmit.aspx" Target="_self" runat="server" Text="Timesheets not submitted"></asp:HyperLink>
 </li>
 <li class="ms-hover">
 <asp:HyperLink ID="lbtnTimesheetDeclined" NavigateUrl="~/WF/Admin/TimesheetDeclined.aspx" Target="_self" runat="server" Text="Declined Timesheets"></asp:HyperLink>
 </li>
 <li class="ms-hover">
 <asp:HyperLink ID="lbtnTimeSheetJournal" NavigateUrl="~/WF/Admin/TimesheetJournal.aspx" Target="_self" runat="server" Text="Timesheet Journal"></asp:HyperLink>
 </li> 
<%-- <li>
 <asp:HyperLink ID="lbtnTimeSheetalerts" NavigateUrl="~/TimesheetApproverMailAlert.aspx" Target="_self" runat="server" Text="Timesheet Approver Alert"></asp:HyperLink>
 </li>--%>
 </ul>
 </div>
  


<%: System.Web.Optimization.Scripts.Render("~/bundles/subtabs") %>