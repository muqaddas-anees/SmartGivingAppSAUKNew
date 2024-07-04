<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectTracker_Variations" Codebehind="ProjectTracker_Variations.aspx.cs" %>
<%@ Register Src="controls/ProjectTabs.ascx" TagName="BuildProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="Controls/Project_FinancialSubtab.ascx" TagName="Project_FinalcialSubtab" TagPrefix="uc2" %>
    <%@ Register Src="Controls/PTVariation.ascx" TagName="Variations" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
<uc1:BuildProjectTabs ID="BuildProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      Variations Tracker - <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
    <div class="form-group">
    <div class="col-md-6">
                                      </div>
         <div class="col-md-6">
             <div class="pull-right form-inline">
                  <a href="pandldisplay.aspx?Project=<%=getProject%>" target="_blank" style="font-weight: bold;">
                        <%= Resources.DeffinityRes.ViewPandLaccount%>
                    </a>
                    <asp:LinkButton ID="btnPandL" runat="server" Text="<%$ Resources:DeffinityRes,ViewPandLaccount%>"
                        OnClick="btnPandL_Click" Font-Bold="True" Visible="false"></asp:LinkButton>
             </div>
           </div>
        </div>
    <uc2:Project_FinalcialSubtab ID="Project_FinalcialSubtab1" runat="server" />

     <uc3:Variations ID="Variations1" runat="server"/>
    
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    activeTab('Project Tracker');
    GridResponsiveCss();
 </script> 

    
    
</asp:Content>


