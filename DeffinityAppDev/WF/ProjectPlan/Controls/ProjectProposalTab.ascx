<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ProjectProposalTab" Codebehind="ProjectProposalTab.ascx.cs" %>
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

    function activeTab(name)
    {
        $(".nav-tabs span").each(function (index, element) {
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
    <li><a href="<%=getUrl(0) %>" target="_self"><%= Resources.DeffinityRes.Project%></a></li>
    <li><a href="<%=getUrl(1) %>" target="_self"><%= Resources.DeffinityRes.Scope%></a></li>
    <li><a href="<%=getUrl(2) %>" target="_self"><%= Resources.DeffinityRes.Funding%></a></li>
    <li><a href="<%=getUrl(3) %>" target="_self" ><%= Resources.DeffinityRes.BusinessRequirements%></a></li>
    <li><a href="<%=getUrl(4) %>" target="_self"><%= Resources.DeffinityRes.Activities%></a></li>
    <li><a href="<%=getUrl(5) %>" target="_self"><%= Resources.DeffinityRes.Approve%></a></li>
    
</ul>
     </div>


