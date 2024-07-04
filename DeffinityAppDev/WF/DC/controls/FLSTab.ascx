<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_FLSTab" Codebehind="FLSTab.ascx.cs" %>

<div class="card mb-5 mb-xl-10">
								<div class="card-body pt-9 pb-0">
									<!--begin::Navs-->
									<div class="d-flex overflow-auto h-55px">
										<ul class="nav nav-stretch nav-line-tabs nav-line-tabs-2x border-transparent fs-5 fw-bolder flex-nowrap">    
                                            <li class="nav-item"><a class="nav-link text-active-primary me-6" href="<%=getUrl(0)%>"  title="Overview"><%= Resources.DeffinityRes.Overview%> </a></li>
     <li id="li3" runat="server" class="nav-item"><a class="nav-link text-active-primary me-6" href="<%=getUrl(18)%>"  title="Donations">Donations</a> </li>
                                            <li id="li5" runat="server" class="nav-item"><a class="nav-link text-active-primary me-6" href="<%=getUrl(19)%>"  title="Donations">Cost</a> </li>
                                             <li id="li4" runat="server" class="nav-item"><a class="nav-link text-active-primary me-6" href="<%=getUrl(20)%>"  title="Form">Tasks </a> </li>
                                             <li id="li1" runat="server" class="nav-item"><a class="nav-link text-active-primary me-6" href="<%=getUrl(21)%>"  title="Form">Progress </a> </li>
   <%-- <li id="li1" runat="server" class="nav-item" visible="false" ><a class="nav-link text-active-primary me-6" href="<%=getUrl(4)%>"  title="Quotation" style="display:none;visibility:hidden;">Quotation</a> </li>
                                            
    <li id="li2" runat="server" class="nav-item" style="display:none;visibility:hidden;"><a class="nav-link text-active-primary me-6" href="<%=getUrl(16)%>"  title="Assign Sales Rep"> Assign Sales Rep</a> </li>
     <li id="link_equipment" runat="server" style="display:none;visibility:hidden;" class="nav-item"><a class="nav-link text-active-primary me-6" href="<%=getUrl(13)%>"  title="Equipment">Equipment </a> </li>
    <li id="link_BOM" runat="server" class="nav-item" visible="false"><a class="nav-link text-active-primary me-6" href="<%=getUrl(12)%>"  title="Internal Costs">Internal Costs </a> </li>
     <li id="link_quotation" runat="server" style="display:none;visibility:hidden;" class="nav-item"><a class="nav-link text-active-primary me-6" href="<%=getUrl(4)%>"  title="Customer Estimates" >Customer Estimates</a></li>
     <li id="link_assigntechnician" runat="server" class="nav-item"><a class="nav-link text-active-primary me-6" href="<%=getUrl(2)%>"  title="Assign Smart Tech" >Assign Staff</a></li>
     <li id="link_Inventory" runat="server" class="nav-item"><a class="nav-link text-active-primary me-6" href="<%=getUrl(8)%>"  title="Inventory">Inventory </a> </li>
    <li id="link_invoice" runat="server" class="nav-item" visible="false"><a class="nav-link text-active-primary me-6" href="<%=getUrl(3)%>"  title="Invoice" style="display:none;visibility:hidden;">Invoice</a></li>
    <li id="link_Forms" runat="server" class="nav-item"><a class="nav-link text-active-primary me-6" href="<%=getUrl(10)%>"  title="Form">Form </a> </li>--%>
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