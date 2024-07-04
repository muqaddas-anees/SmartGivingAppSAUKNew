<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_QAtabs" Codebehind="QAtabs.ascx.cs" %>
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
<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-2">
<ul class="nav navbar-nav">
    <li><a href="<%=getUrl(0)%>" target="_self"><%= Resources.DeffinityRes.Overview%></a></li>
    <li><a href="<%=getUrl(1)%>" target="_self"><%= Resources.DeffinityRes.QAchecklist%></a></li>
    <li><a href="<%=getUrl(3)%>" target="_self"><%= Resources.DeffinityRes.CSI%></a></li>
    <li><a href="<%=getUrl(4)%>" target="_self"><%= Resources.DeffinityRes.Docs%></a></li>
    <li><a href="<%=getUrl(5)%>" target="_self"><%= Resources.DeffinityRes.LessonsLearnt%></a></li>
    <li><a href="<%=getUrl(6)%>" target="_self"><%= Resources.DeffinityRes.Approval%></a></li>
</ul>
    </div>
