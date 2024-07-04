<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/WF/MainTab.master" Inherits="ProjectPlanApprove" Codebehind="ProjectPlanApprove.aspx.cs" %>
<%@ Register src="controls/ProjectProposalTab.ascx" tagname="ProjectProposalTab" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc2:ProjectProposalTab ID="ProjectProposalTab1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectProposal%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
   <%= Resources.DeffinityRes.Approve%> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink runat="Server" Text="Back to Project Proposal" NavigateUrl="~/WF/ProjectPlan/ProjectPipeline.aspx?Status=8"><i class="fa fa-arrow-left"></i> Return to Project Proposal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="form-group">
             <div class="col-md-12 text-bold">
                 <p>Please make sure all sections of the plan are completed correctly 
     before you submit the plan for approval. 
     The Approval Board will assess your requirements and will confirm their verdict as soon as possible.</p>
</div>
</div>
     <div class="form-group">
             <div class="col-md-12">
                 <asp:Button ID="Btnsubmit" runat="server" onclick="Btnsubmit_Click" Text="Send for Approval" />
                 </div>
         </div>

</asp:Content>
