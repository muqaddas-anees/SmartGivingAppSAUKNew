<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_CustomerhealthCheckCntrl" Codebehind="CustomerhealthCheckCntrl.ascx.cs" %>
<%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %> 
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
     <ul class="nav navbar-nav">
         <li><a href ="~/WF/Portal/HealthCheckSchedule_CPortal.aspx" runat="server" target="_self">Health Checks</a></li>
         <li><a href = "~/WF/Portal/CustomerHealthCheckMgmt.aspx?customer=0" runat="server" target="_self">Forms</a></li>
         <%--<li style="float:right;text-align:right;"><a href="../../WF/Portal/Home.aspx" target="_self"><span>Back to Customer Home</span></a></li>--%>
     </ul>
 </div>
<%--<script type="text/javascript">
    $(document).ready(function () {
        $(".tabs_list6 a").each(function (index, element) {

            var cu = $(location).attr('href').toLowerCase();
            var ck = $(element).attr('href').toLowerCase();
            if (cu.indexOf($.trim(ck)) > -1) {
                $(element).attr('class', 'active');
                $(element).parents('li').attr('class', 'active');
                return false;
            }
        });
    });

    </script>--%>