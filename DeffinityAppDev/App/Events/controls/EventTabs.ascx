<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventTabs.ascx.cs" Inherits="DeffinityAppDev.App.Events.controls.EventTabs" %>
<div class="card mb-5 mb-xl-10">
								<div class="card-body pt-9 pb-0">
									<!--begin::Navs-->
									<div class="d-flex overflow-auto h-55px">
										<ul class="nav nav-stretch nav-line-tabs nav-line-tabs-2x border-transparent fs-5 fw-bolder flex-nowrap">    
                                            <li class="nav-item"><a class="nav-link text-active-primary me-6" href="<% = getUrl(0)%>"  title="Basic Information">Basic Info </a></li>
     <li id="li3" runat="server" class="nav-item"><a class="nav-link text-active-primary me-6" href="<%=getUrl(1)%>"  title="Details">Details</a> </li>
    <li id="li1" runat="server" class="nav-item" ><a class="nav-link text-active-primary me-6" href="<%=getUrl(2)%>"  title="Speaker(s)">Speaker(s)</a> </li>
                                            <li id="li5" runat="server" class="nav-item" ><a class="nav-link text-active-primary me-6" href="<%=getUrl(5)%>"  title="Sponsers">Sponsors</a> </li>
                                             <li id="li4" runat="server" class="nav-item"><a class="nav-link text-active-primary me-6"  href="<%=getUrl(3)%>"  title="Tickets">Tickets </a> </li>
                                             <li id="li2" runat="server" class="nav-item"><a class="nav-link text-active-primary me-6"  href="<%=getUrl(4)%>"  title="Publish">Publish </a> </li>
    
</ul>
     <%--  <ul class="nav navbar-nav" style="float:right;">
    <li>  <a id ="link_return" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to  <%= Resources.DeffinityRes.ServiceDesk%></a></li>

       </ul>--%>
     <asp:HiddenField ID="hidPortfolioID" runat="server" /><asp:HiddenField ID="hsdid" runat="server" Value="0"/>
</div>
                                    </div>
    </div>

<%--<%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %>
<script type="text/javascript">
    //sideMenuActive('<%= Resources.DeffinityRes.Customer%>');
</script>--%>

<%--<a href="../../Activities.aspx">../../Activities.aspx</a>--%>
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