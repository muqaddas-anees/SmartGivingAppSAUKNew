<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ChangeControl" Codebehind="ChangeControl.ascx.cs" %>
<div class="sec_tab">
    <table border="0" id="table1" cellspacing="0" cellpadding="0" class="ImageAlign">
        <tr>
            <td>
                <a href="ChangeControl.aspx">
                    <img border="0" src="<%=GetImage(0) %>" alt="Change Control" /></a>
            </td>
            <td>
                <a href="CCScheduledTasks.aspx">
                    <img border="0" src="<%=GetImage(1) %>" alt="Scheduled Tasks" /></a>
            </td>
          
            <td>
                <a href="CCRiskAssessment.aspx">
                    <img border="0" src="<%=GetImage(2) %>" alt="Risk Assessment" /></a>
            </td>
            <td>
                <a href="CCAddApproval.aspx">
                    <img border="0" src="<%=GetImage(3) %>" alt="Approvals" /></a>
            </td>
        </tr>
    </table>
</div>