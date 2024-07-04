<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_TrainingSubTabs" Codebehind="TrainingSubTabs.ascx.cs" %>
<script type="text/javascript">
    $(document).ready(function () {
        $(".navbar-nav a").each(function (index, element) {
            var cu = $(location).attr('href').toLowerCase();
            var ck = $(element).attr('href').toLowerCase();
            if (cu.indexOf($.trim(ck)) > -1) {
                $(element).attr('class', 'active');
                $(element).parents('li').attr('class', 'active');
                //return false;
            }
        });
    });
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
                        <li>
                        <asp:HyperLink ID="lbtnTrCategory" NavigateUrl="~/WF/Training/trCategory.aspx" Target="_self" runat="server" Text="Training Directory"></asp:HyperLink>
                            </li><li>
                        <asp:HyperLink ID="lbtnTrDepartment" NavigateUrl="~/WF/Training/trDeptReq.aspx" Target="_self" runat="server" Text="Department Requirements"></asp:HyperLink>
                        </li><li>
                        <asp:HyperLink ID="lbtnTrNotification" NavigateUrl="~/WF/Training/trAdminNotification.aspx" Target="_self" runat="server" Text="Notification"></asp:HyperLink>
                        </li></ul>
                </div>
