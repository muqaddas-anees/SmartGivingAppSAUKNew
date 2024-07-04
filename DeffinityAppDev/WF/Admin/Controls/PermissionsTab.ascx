<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_PermissionsTab" Codebehind="PermissionsTab.ascx.cs" %>

<script runat="server">
   
    protected string[] Purl = { "ManageTeamMembersNew.aspx", "ProgrammePermission.aspx", "ProgrammeManagement.aspx" };

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
    <li><a href="ManageTeamMembersNew.aspx" target="_self">
    <span class="hidden-xs">Groups</span></a></li>
     <li><a href="ProgrammePermission.aspx" target="_self">
    <span class="hidden-xs">Permissions
</span></a></li>
<li><a href="ProgrammeManagement.aspx" target="_self">
    <span class="hidden-xs">Programme Management
</span></a></li>
    

    
</ul>
    </div>
<%--<ul class="tabs_list5" style="float:right;">
    <li class="current1"><a id="lbtn_Navigate" target="_self" href="Admin.aspx?tab=3"><span id="lbtn_NavigateText" runat="server">Return to Security Section</span></a></li>
   
</ul>--%>

 <%: System.Web.Optimization.Scripts.Render("~/bundles/tabs") %>

