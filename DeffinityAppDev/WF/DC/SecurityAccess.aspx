<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="SecurityAccess1" EnableEventValidation="false" Codebehind="SecurityAccess.aspx.cs" %>


<%@ Register Src="controls/RequestType.ascx" TagName="RequestType" TagPrefix="rt" %>
<%@ Register Src="controls/Status.ascx" TagName="Status" TagPrefix="st" %>
<%@ Register Src="controls/EmailFooter.ascx" TagName="EmailFooter" TagPrefix="uc3" %>
 <%@ Register Src="controls/OurSite.ascx" TagName="OurSite" TagPrefix="ucSite" %>
<%--<%@ Register Src="~/DC/controls/SecurityAccessMail.ascx" TagName="SecurityAccessMail"
    TagPrefix="uc1" %>--%>
    <%@ Register Src="controls/SADropDown.ascx" TagName="SA" TagPrefix="uc6" %>
<%@ Register Src="controls/FLSAdminDropDown.ascx" TagName="FLSAdminDropDown"
    TagPrefix="uc4" %>
<%@ Register Src="~/WF/Admin/controls/AdminDropdownTab.ascx" TagName="AdminDropdownTab" TagPrefix="uc2" %>
<%@ Register Src="controls/AccessEmailId.ascx" TagName="AccessEmailId" TagPrefix="UcAE" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <h1 class="section1">
                    <span>
                        <%= Resources.DeffinityRes.AdminDropdownLists%>
                    </span>
                </h1>
            </td>
        </tr>
        <tr>
            <td class="p_section1 data_carrier_block">
                <div class="sec_table">
                    <uc2:AdminDropdownTab ID="AdminDropdownTab1" runat="server" />
                    <table width="100%" border="0">
                        <tr>
                        <td align="right">
                        <uc6:SA ID="sa" runat="server" />
                        </td>
                        </tr>
                        <asp:Panel ID="pnlSA" runat="server">
                            <tr>
                                <td>
                                    <rt:RequestType ID="rt1" runat="server" Visible="false" />
                                     <st:Status ID="st1" runat="server" />
                                </td>
                                <td>
                                     <UcAE:AccessEmailId ID="AccessEmailId1" runat="server" TypeofEmail="Support team email" />
                                  
                                </td>
                            </tr>
                            <tr>
                                <%--<td>
                                 <div class="sec_header" style="width: 550px">
                Security Access Email Distribution</div>
                                    <uc1:SecurityAccessMail ID="SAMail" runat="server" />
                                </td>--%>
                                <td style="vertical-align:top">
                                    <ucSite:OurSite ID="Oursite1" runat="server" />
                                </td>
                                <td>
                                 <uc3:EmailFooter ID="EmailFooter1" runat="server" />
                                   
                                </td>
                            </tr>
                            <tr>
                            <td>
                            
                            </td>
                            </tr>
                        </asp:Panel>
                      
                        
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
