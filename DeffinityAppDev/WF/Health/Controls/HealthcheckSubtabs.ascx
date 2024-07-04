<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_HealthcheckSubtabs" Codebehind="HealthcheckSubtabs.ascx.cs" %>

<div class="form-wizard" style="margin-bottom:15px">
    <ul class="tabs">
        <li class="ms-hover">
 <asp:HyperLink ID="lbtnHealthcheckmain"   NavigateUrl="~/WF/Health/HealthCheckForm.aspx" Target="_self" runat="server" Text="<%$ Resources:DeffinityRes, HealthCheck%>"> <%= Resources.DeffinityRes.HealthCheck%></asp:HyperLink>
 </li>
 <li class="ms-hover">
 <asp:HyperLink ID="lbtnHealthcheckIssues" NavigateUrl="~/WF/Health/HealthCheckFormIssues.aspx" Target="_self" runat="server" Text="<%$ Resources:DeffinityRes, RecommendationsIssues%>"><%= Resources.DeffinityRes.RecommendationsIssues%></asp:HyperLink>
 </li> 
 <li class="ms-hover">
 <asp:HyperLink ID="lbtnHealthcheckDocs" NavigateUrl="~/WF/Health/HealthCheckFormDocs.aspx" Target="_self" runat="server" Text="<%$ Resources:DeffinityRes, Documents%>"><%= Resources.DeffinityRes.Documents%></asp:HyperLink>
 </li>
  
 </ul>
 </div>
<%: System.Web.Optimization.Scripts.Render("~/bundles/subtabs") %>