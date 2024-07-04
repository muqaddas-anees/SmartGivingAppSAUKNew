<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactTabCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Controls.ContactTabCtrl" %>

<div class="card mb-5 mb-xl-10">
								<div class="card-body pt-9 pb-0">
									<!--begin::Navs-->
									<div class="d-flex overflow-auto h-55px">
										<ul class="nav nav-stretch nav-line-tabs nav-line-tabs-2x border-transparent fs-5 fw-bolder flex-nowrap">   
    <li><a href="<%=getUrl(0)%>"  title="Customer Details">Customer Details </a></li>
     <li id="link_Addressmaintenaceplan"><a href="<%=getUrl(1)%>"  title="Address and Maintenance Plan" >Address and Maintenance Plan</a></li>
    <li><a href="<%=getUrl(5)%>"  title="Contacts" >Contacts</a></li>
    <li id="link_equipment" runat="server"><a href="<%=getUrl(2)%>"  title="Equipment" >Equipment</a></li>
     <li id="li1" runat="server"><a href="<%=getUrl(6)%>"  title="Communication" > Communication</a></li>
    <li id="link_remainders" runat="server"><a href="<%=getUrl(3)%>"  title="Reminders" >Reminders</a></li>
    <li id="link_payments" runat="server"><a href="<%=getUrl(4)%>"  title="Payments" >Payments</a></li>
   
</ul>
 <%--  <ul class="nav navbar-nav" style="float:right;">
    <li> <a id ="link_return" href="~/WF/CustomerAdmin/PortfolioContacts.aspx" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Back to Contact list</a></li>

       </ul>--%>
     
</div>
                                    </div>
    </div>

<%--<%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %>
<script type="text/javascript">
    //sideMenuActive('<%= Resources.DeffinityRes.Customer%>');
</script>
<script type="text/javascript">
    var cu = $(location).attr('href').toLowerCase();
    var ck = 'contactaddressdetailsbasic.aspx';
    debugger;
    //if (cu.indexOf($.trim(ck)) == -1) {
       
    //    activeTab('Address');
    //}
</script>--%>


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