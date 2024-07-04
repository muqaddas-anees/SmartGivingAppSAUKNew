<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminTabCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.Admin.Controls.AdminTabCtrl" %>

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
   <div class="card mb-5 mb-xl-10">
								<div class="card-body pt-9 pb-0">
									<!--begin::Navs-->
									<div class="d-flex overflow-auto h-55px">
										<ul class="nav nav-stretch nav-line-tabs nav-line-tabs-2x border-transparent fs-5 fw-bolder flex-nowrap">
     <li class="nav-item"><a class="nav-link text-active-primary me-6" id="A1" href="~/WF/Admin/Premium.aspx" runat="server"
            target="_self">Premium</a></li>
     <li class="nav-item"><a class="nav-link text-active-primary me-6" id="A2" href="~/WF/Admin/Instances.aspx" runat="server"
            target="_self">Instances and Users</a></li>
    <li class="dropdown" style="display:none;visibility:hidden;"><a href="#" data-toggle="dropdown" class="dropdown-toggle" >Billing<b class="caret"></b></a>
		<ul class="dropdown-menu dropdown-menu-left">
        <li runat="server"><a class="nav-link text-active-primary me-6" href="~/WF/Admin/BillingConfig.aspx" runat="server">Billing Plans </a></li>
		<li runat="server"><a class="nav-link text-active-primary me-6" href="~/WF/Admin/Billing.aspx" runat="server">Billing </a></li>
        <li runat="server"><a class="nav-link text-active-primary me-6" href="~/WF/Admin/UpgradePopup.aspx" runat="server">Upgrade Pop up</a></li>
		</ul>
	</li>
          <%--<li ><a id="link_flsreport" href="~/WF/Admin/Billing.aspx" runat="server"
            target="_self" >Billing</a></li>
     <li ><a id="A12" href="~/WF/Admin/BillingConfig.aspx" runat="server"
            target="_self" >Billing Plans</a></li>
     <li><a id="A13" href="~/WF/Admin/UpgradePopup.aspx" runat="server"
            target="_self" >Upgrade Pop up</a></li>--%>
     <li class="nav-item"><a class="nav-link text-active-primary me-6" id="A3" href="~/WF/Admin/OurInformation.aspx" runat="server"
            target="_self" >Our Information</a></li>
   <%--  <li class="nav-item"><a class="nav-link text-active-primary me-6" id="A4" href="~/WF/Admin/AdminUsers.aspx" runat="server"
            target="_self" >123 Admin Users</a></li>--%>
    <%-- <li class="nav-item"><a class="nav-link text-active-primary me-6" id="A5" href="~/WF/Admin/Categories.aspx" runat="server"
            target="_self" >Categories</a></li>--%>
   <%-- <li class="nav-item"><a class="nav-link text-active-primary me-6" id="A6" href="~/WF/Admin/Modules.aspx" runat="server"
            target="_self" >Modules</a></li>--%>
     <li class="nav-item"><a class="nav-link text-active-primary me-6" id="A7" href="~/WF/Admin/DefaultJobs.aspx" runat="server"
            target="_self" >Default Jobs</a></li>
        <%-- <li class="nav-item"><a class="nav-link text-active-primary me-6" id="A8" href="~/WF/Admin/Training.aspx" runat="server"
            target="_self" >Training</a></li>--%>
     <li class="nav-item"><a class="nav-link text-active-primary me-6" id="A9" href="~/WF/Admin/PartnersList.aspx" runat="server"
            target="_self" >Partners</a></li>
     <li class="nav-item"><a class="nav-link text-active-primary me-6" id="A11" href="~/WF/Admin/CCPopup.aspx" runat="server"
            target="_self" >Pop up</a></li>
   <%-- <li class="nav-item"><a class="nav-link text-active-primary me-6" id="A10" href="~/WF/Admin/UsersTrackJournal.aspx" runat="server"
            target="_self" >Journal</a></li>--%>
    <li class="nav-item"><a class="nav-link text-active-primary me-6" id="A12" href="~/WF/Admin/BlogList.aspx" runat="server"
            target="_self" >Business Services</a></li>
    </ul>
                                        </div>
   </div>

       </div>

<script type="text/javascript">
    $(document).ready(function () {
        $(".nav-stretch a").each(function (index, element) {

            var cu = $(location).attr('href').toLowerCase();
            var ck = $(element).attr('href').toLowerCase().replace('..', '');

            console.log(ck);
            console.log(cu);
            if (cu.indexOf($.trim(ck)) > -1) {
                console.log(ck);
                $(element).attr('class', 'nav-link text-active-primary me-6 active');
                //$(element).parents('li').attr('class', 'active');
                return false;
            }
        });


    });

    function activeTab(name) {
        $(".nav-stretch a").each(function (index, element) {
            var cu = name.toLowerCase();
            var ck = $(element).html().toLowerCase();
            if (cu.indexOf($.trim(ck)) > -1) {
                $(element).attr('class', 'active');
                //$(element).closest('li').attr('class', 'active');
            }
        });
    }

</script>
