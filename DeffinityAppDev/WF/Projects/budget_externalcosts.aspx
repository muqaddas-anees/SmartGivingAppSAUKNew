<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" 
    CodeFile="budget_externalcosts.aspx.cs" Inherits="budget_externalcosts" EnableEventValidation="false" %>

<%@ Register Src="controls/ProjectTabs.ascx" TagName="ProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/Project_BudgetSubTab.ascx" TagName="BudgetTab" TagPrefix="uc4"%>
<%@ Register Src="controls/ProjectCost.ascx" TagName="ProjectCost" TagPrefix="uc2" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
     <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="data_carrier">
                <h1 class="section1">
                        <span>External Costs</span>
                </h1>
              <div class="flds_11">
                     <pref:projectref id="ProjectRef1" runat="server" />
                     <span style="float:right">
                                         <asp:HyperLink ID="btnBack" runat="server" NavigateUrl="~/ProjectPipeline.aspx?Status=2"
                                                                 ImageUrl="~/media/btn_back_proj_pipe.gif"></asp:HyperLink>
                     </span>
              </div>
             <div class="data_carrier_block p_section1">
                       <div>
                                <uc4:BudgetTab ID="BudgetTab1" runat="server" />
                       </div>
                 <div>
           </div>
             </div>
          </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ProgressImage" Runat="Server">
</asp:Content>

