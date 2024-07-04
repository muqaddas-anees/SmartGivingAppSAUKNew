<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InventoryItemsCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Controls.InventoryItemsCtrl" %>
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
          <li><a id="link_flsreport" href="~/WF/CustomerAdmin/InventoryItemslist.aspx" runat="server" target="_self" >Inventory </a></li>
        <li class="dropdown" id="link_menu" runat="server"><a href="#" data-toggle="dropdown" class="dropdown-toggle"><%= Resources.DeffinityRes.Setup%><b class="caret"></b></a>
            <ul class="dropdown-menu dropdown-menu-left">
                <li><a id="a1_link" href='<%:ResolveClientUrl("~/WF/DC/FLSDefault.aspx?tab=fls&type=category&page=inventory") %>'> Inventory Category </a></li>
                <li><a id="a6_link" href='<%:ResolveClientUrl("~/WF/WM/WMdetails.aspx") %>'>Storage Location</a></li>
                 <li><a id="a7_link" href='<%:ResolveClientUrl("~/WF/Vendors/RFIVendors.aspx") %>'>Supplier Management</a></li>
               
            </ul>
        </li>
       
    </ul>
   </div>