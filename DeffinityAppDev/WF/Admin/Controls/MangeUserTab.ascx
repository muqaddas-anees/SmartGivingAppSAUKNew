<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_MangeUserTab" Codebehind="MangeUserTab.ascx.cs" %>
<div class="form-wizard" style="margin-bottom:15px">
<ul class="tabs">
<li class="ms-hover"><asp:HyperLink ID="lbtnUsers"  target="_self" runat="server"   Text="User">User</asp:HyperLink></li>
<li class="ms-hover" style="display:none;visibility:hidden"><asp:HyperLink ID="lbtnPermissionManage"  target="_self" runat="server"   Text="Permission Manager">Permission Manager</asp:HyperLink></li>
<li class="ms-hover"><asp:HyperLink ID="lbtnDetails" Target="_self" runat="server" Text="Address">Address</asp:HyperLink> </li>
    <li class="ms-hover"><asp:HyperLink ID="lbtnAppliancesCovered" Target="_self" runat="server" Text="Appliances Covered">Services Covered</asp:HyperLink> </li>
<li class="ms-hover" style="display:none;visibility:hidden;"><asp:HyperLink ID="lbtnRates"  target="_self" runat="server"  Text="Rates">Rate</asp:HyperLink></li>
<li class="ms-hover"  style="display:none;visibility:hidden;"><asp:HyperLink ID="lbtnAnnualLeave"  target="_self" runat="server"   Text="AnnualLeave">Annual Leave</asp:HyperLink></li>
<li class="ms-hover" style="display:none;visibility:hidden;"> <asp:HyperLink ID="lbtnPermissions" Target="_self" runat="server" Text="Permissions">Permissions</asp:HyperLink> </li>
    <li class="ms-hover"><asp:HyperLink ID="lbtnCertificates"  target="_self" runat="server"   Text="Certificates">Documents </asp:HyperLink></li>
  <li class="ms-hover" style="display:none;visibility:hidden;"> <asp:HyperLink ID="lbtnSkills" Target="_self" runat="server" Text="SkillsTraining">Skills/Training </asp:HyperLink> </li>

     <li class="dropdown" style="display:none;visibility:hidden;" ><a href="#" data-toggle="dropdown" class="dropdown-toggle" ><%= Resources.DeffinityRes.More%><b class="caret"></b></a>
		<ul class="dropdown-menu dropdown-menu-right">
            <%--<li class="ms-hover"> <asp:HyperLink ID="lbtnSkills" Target="_self" runat="server" Text="SkillsTraining">Skills/Training </asp:HyperLink> </li>--%>
            <%--<li class="ms-hover"> <asp:HyperLink ID="lbtnQuickLinks" Target="_self" runat="server" Text="QuickLinks">Quick&nbsp;Links </asp:HyperLink> </li>--%>
<%--<li class="ms-hover"> <asp:HyperLink ID="lbtnTheraphy" Target="_self" runat="server" Text="Therapy">Therapy </asp:HyperLink> </li>--%>
<li class="ms-hover"> <asp:HyperLink ID="lbtnMoves" Target="_self" runat="server" Text="Moves">Moves </asp:HyperLink> </li>
            

            </ul>

</ul>
</div>
<%: System.Web.Optimization.Scripts.Render("~/bundles/subtabs") %>
<%--<script type="text/javascript">
    $(document).ready(function () {
        $(".tabs a").each(function (index, element) {
            //if (index < 7)
            //    {
            var cu = $(location).attr('href').toLowerCase();
            var ck = $(element).attr('href').toLowerCase();
            if (cu.indexOf($.trim(ck)) > -1) {
                $(element).attr('class', 'active');
                $(element).parents('li').attr('class', 'active');
                return false;
           // }
            }
        });
    });
</script>--%>
