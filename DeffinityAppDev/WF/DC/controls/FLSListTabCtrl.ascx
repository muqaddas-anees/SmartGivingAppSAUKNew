<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FLSListTabCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.FLSListTabCtrl" %>

   <script type="text/javascript">
       $(document).ready(function () {
           $(".navbar-nav a").each(function (index, element) {

               var cu = $(location).attr('href').toLowerCase();
               var ck = $(element).attr('href').toLowerCase();
               if (cu.indexOf($.trim(ck)) > -1) {
                   $(element).attr('class', 'active');
                   $(element).parents('li').attr('class', 'active');
                   return false;
               }
           });


       });

       function activeTab(name) {
           $(".navbar-nav span").each(function (index, element) {
               var cu = name.toLowerCase();
               var ck = $(element).html().toLowerCase();
               if (cu.indexOf($.trim(ck)) > -1) {
                   $(element).closest('li').attr('class', 'active');
               }
           });
       }

</script>
   <div class="navbar-header">
					<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-2">
						<span class="sr-only">Toggle navigation</span>
						<i class="fa-bars"></i>
					</button>
					<%--<a class="navbar-brand" href="#">Navbar</a>--%>
				</div>
 <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
     <li><a id="link_Dashboard" runat="server" href="~/WF/DC/ResourceSchedular.aspx"
            target="_self" visible="false">Dispatch Board </a></li>
     <li><a id="A1" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server"
            target="_self"><%= Resources.DeffinityRes.ServiceDesk%></a></li>
          <li style="display:none;visibility:hidden;"><a id="link_flsreport" href="~/WF/DC/FLSReport.aspx" runat="server"
            target="_self" visible="false"><%= Resources.DeffinityRes.ServiceDesk%> <%= Resources.DeffinityRes.Report%></a></li>
        <li><a id="link_unitstatus" href="~/WF/DC/SDUnitStatus.aspx?type=fls" runat="server" target="_self"
            visible="false">Unit <%= Resources.DeffinityRes.Dashboard%></a></li>
       
        <li><a id="link_deliveryreport" runat="server" href="~/WF/DC/DeliveryReport.aspx" 
            target="_self" visible="false"><%= Resources.DeffinityRes.Delivery%> <%= Resources.DeffinityRes.Report%></a></li>
        <li><a id="link_accessreport" runat="server" href="~/WF/DC/AccessReport.aspx"
            target="_self" visible="false"><%= Resources.DeffinityRes.AccessControl%> <%= Resources.DeffinityRes.Report%></a></li>
     <li class="dropdown" id="li1" runat="server" visible="false"><a href="#" data-toggle="dropdown" class="dropdown-toggle"><%= Resources.DeffinityRes.Dashboard%><b class="caret"></b></a>
            <ul class="dropdown-menu dropdown-menu-left">
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/ResourceSchedular.aspx") %>'>Scheduler</a></li>
                <li><a  href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS&") %>'>Open Claims</a></li>
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS&") %>'>Claim Pay-outs</a></li>
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/CompletedSalesJournal.aspx") %>'><strong>Completed Sales</strong> </a></li>
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS&") %>'>Cancellations</a></li>
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS&") %>'>Overall Sales Revenue (New Customers and Renewals)</a></li>
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS&") %>'>Sales Person Report</a></li>
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS&") %>'>Product Type</a></li>
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS&") %>'>Commission Reporting</a></li>
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS&") %>'>Sales Person Leaderboard</a></li>
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS&") %>'>Average Cost per Ticket</a></li>
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS&") %>'>Total Claims by Month</a></li>
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS&") %>'>Preferred Dispatch Times</a></li>
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS&") %>'>Frequency of Covered Item Type by Category</a></li>
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS&") %>'>Feedback from Surveys</a></li>
                 <li><a  href='<%:ResolveClientUrl("~/WF/DC/FLSJlist.aspx?type=FLS&") %>'>Service Provider Average Review Score</a></li>
               
            </ul>
        </li>
        <li class="dropdown" id="link_menu" runat="server" style="visibility:hidden;display:none;"><a href="#" data-toggle="dropdown" class="dropdown-toggle"><%= Resources.DeffinityRes.Setup%><b class="caret"></b></a>
            <ul class="dropdown-menu dropdown-menu-left">
                <li><a id="a1_link" href='<%:ResolveClientUrl("~/WF/DC/FLSDefault.aspx?tab=fls") %>'> Jobs Admin</a></li>
               <%--  <li><a id="a8_link" href='<%:ResolveClientUrl("~/WF/DC/FRPApprovals.aspx") %>'> Fixed Rate Price Approval</a></li>--%>
                 <%--<li><a id="a8_link" href='<%:ResolveClientUrl("~/WF/DC/AdminPolicyType.aspx") %>' style="display:none;"> Admin Policy Type</a></li>--%>
                <%--<li><a id="a2_link" href='<%:ResolveClientUrl("~/WF/CustomerAdmin/PortfolioContacts.aspx") %>'><%= Resources.DeffinityRes.CustomerContacts%></a></li>--%>
                <li><a id="a6_link" href='<%:ResolveClientUrl("~/WF/DC/SDFieldsConfig.aspx?type=fls") %>'>Configurable Fields</a></li>
                 <li><a id="a7_link" href='<%:ResolveClientUrl("~/WF/DC/FLSCustomFormDesigner.aspx?type=fls") %>'>Custom Form Designer</a></li>
                <%--<li><a id="a3_link" href='<%:ResolveClientUrl("~/WF/CustomerAdmin/ServiceCatalogue.aspx?type=fls") %>'><%= Resources.DeffinityRes.ServiceCatalogue%></a></li>--%>
                <%--<li><a id="a5_link" href='<%:ResolveClientUrl("~/WF/CustomerAdmin/SDTeamMember.aspx") %>'><%= Resources.DeffinityRes.ServiceDeskTeams%></a></li>--%>
                <%--li><a id="a4_link" href='<%:ResolveClientUrl("~/WF/CustomerAdmin/PortfolioSLA.aspx?type=fls") %>'><%= Resources.DeffinityRes.SLA%></a></%--li>--%>
            </ul>
        </li>
       
    </ul>
   </div>