<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CampaignTabs.ascx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Campaign.Controls.CampaignTabs" %>


  <div class="card mb-5 mb-xl-10">
								<div class="card-body pt-9 pb-0">
									<!--begin::Navs-->
									<div class="d-flex overflow-auto h-55px">
										<ul class="nav nav-stretch nav-line-tabs nav-line-tabs-2x border-transparent fs-5 fw-bolder flex-nowrap">   
     <li class="nav-item"><a class="nav-link text-active-primary me-6" id="A1" href="~/WF/CustomerAdmin/Campaign/CampaignList.aspx" runat="server"
            target="_self">Email Templates</a></li>
     <li class="nav-item"><a class="nav-link text-active-primary me-6" id="A2" href="~/WF/CustomerAdmin/Campaign/CampaignSchedule.aspx" runat="server"
            target="_self">Email Schedule</a></li>
          <li  class="nav-item"><a class="nav-link text-active-primary me-6" id="link_flsreport" href="~/WF/CustomerAdmin/Campaign/ViewCampaigns.aspx" runat="server"
            target="_self" >View Calendar</a></li>
        
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