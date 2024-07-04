<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="FinancialPO" Codebehind="ProjectFinancial_CustomerPO.aspx.cs" %>
     

<%@ Register Src="controls/ProjectTabs.ascx" TagName="BuildProjectTabs" TagPrefix="uc1" %>
<%@ Register Src="controls/Project_FinancialSubtab.ascx" TagName="Project_FinalcialSubtab"
    TagPrefix="uc2" %>
<%@ Register Src="controls/CustomerPO.ascx" TagName="CustomerPO" TagPrefix="uc3" %>
<%@ Register Src="controls/ProjectFinancialPOtab.ascx" TagName="POTab" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:BuildProjectTabs ID="BuildProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <h1 class="section1">
                    <span>
                        <%= Resources.DeffinityRes.Financials%></span></h1>
                <div class="flds_11">
                    <Pref:ProjectRef ID="ProjectRef1" runat="server" />
                    <span class="space_r50" style="float: right">
                        <asp:HyperLink ID="btnBack" runat="server" NavigateUrl="~/WF/Projects/ProjectPipeline.aspx?Status=2"
                            ImageUrl="~/media/btn_back_proj_pipe.gif"></asp:HyperLink>
                    </span>
                </div>
            </td>
        </tr>
        <tr>
            <td class="p_section1 data_carrier_block">
                <uc2:Project_FinalcialSubtab ID="Project_FinalcialSubtab1" runat="server" />
                <div class="clr"></div>
                <br />
               <uc4:POTab ID="POTab1"  runat="server" />
                <uc3:CustomerPO ID="CustomerPO1" runat="server" />
               
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
</asp:Content>
