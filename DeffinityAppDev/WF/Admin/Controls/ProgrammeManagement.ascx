<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ProgrammeManagement" Codebehind="ProgrammeManagement.ascx.cs" %>

<script runat="server">
   
    protected string[] Purl = { "ProgrammeManagement.aspx", "ProgrammeAssessment.aspx", "ProgrammeKeyMilestones.aspx","ProgrammePermission.aspx","ProjectCheckPoints.aspx" };

    protected string GetCssClass(int i)
    {
        string rtValue = string.Empty;
        //i = 0;
        if (i < Purl.Length)
        {
            string stemp = Purl[i];
            if ((Request.Url.ToString().ToLower()).Contains(Purl[i].ToLower()) == true)
            {
                rtValue = "current1";
            }
            else
            {
                rtValue = string.Empty;
            }
        }
        return rtValue;
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
    <li><a href="ProgrammeManagement.aspx" target="_self">
    Programme Management</a></li>
     <li><a href="ProgrammePermission.aspx" target="_self">
    Permission Manager
</a></li>
    <li><a href="ProgrammeAssessment.aspx" target="_self">
    <%= Resources.DeffinityRes.ProgramAssessment%>
</a></li>
 <li><a href="ProgrammeKeyMilestones.aspx" target="_self">
    <%= Resources.DeffinityRes.KeyMilestone%>
</a></li>
<li><a href="ProjectCheckPoints.aspx" target="_self">
    Check Points
</a></li>
</ul>
    </div>

 <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %>