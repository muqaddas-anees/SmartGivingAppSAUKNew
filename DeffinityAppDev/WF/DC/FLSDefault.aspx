<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
     Inherits="FLSDefault1" EnableEventValidation="false" Codebehind="FLSDefault.aspx.cs" %>
<%@ Register Src="~/WF/DC/controls/SecurityAccessMail.ascx" TagName="SecurityAccessMail"
    TagPrefix="ucSA" %>
<%@ Register src="~/WF/CustomerAdmin/controls/PortfolioDdlCtr.ascx" tagname="PortfolioDdlCtr" tagprefix="uc2" %>
<%@ Register Src="~/WF/DC/controls/AccessEmailId.ascx" TagName="AccessEmailId" TagPrefix="UcAE" %>
<%--<%@ Register Src="~/WF/Admin/Controls/AdminDropdownTab.ascx" TagName="AdminDropdownTab" TagPrefix="uc1" %>--%>
<%@ Register Src="~/WF/DC/controls/FLSAdminDropDown.ascx" TagName="FLSAdminDropDown"
    TagPrefix="uc2" %>
<%@ Register Src="~/WF/DC/controls/OurSite.ascx" TagName="OurSite" TagPrefix="ucSite" %>
<%@ Register Src="~/WF/DC/controls/SADropDown.ascx" TagName="SA" TagPrefix="uc6" %>
<%@ Register Src="~/WF/DC/controls/EmailFooter.ascx" TagName="EmailFooter" TagPrefix="uc3" %>
<%@ Register Src="~/WF/DC/controls/CategoryCtrl.ascx" TagName="CategoryCtrl" TagPrefix="ucc" %>
<%@ Register Src="~/WF/CustomerAdmin/controls/PortfolioContactsDepartmentCtrl.ascx" TagName="DepartmentCtrl"
    TagPrefix="ucd" %>
<%@ Register Src="~/WF/DC/controls/SourceOfRequestCtrl.ascx" TagName="SourceOfRequestCtrl"
    TagPrefix="ucsr" %>
<%@ Register Src="~/WF/DC/controls/FLSEmailNotificationCtrl.ascx" TagName="FLSEmailNotificationCtrl"
    TagPrefix="ucEmail" %>
<%@ Register Src="~/WF/DC/controls/FLSDocumentVisibilityCtrl.ascx" TagName="FLSDocumentVisibilityCtrl"
    TagPrefix="ucDoc" %>
<%@ Register Src="controls/Status.ascx" TagName="StatusCtrl" TagPrefix="Status" %>
<%--<%@ Register Src="controls/FieldsVisibilityCtrl.ascx" TagName="FieldsVisibilityCtrl" TagPrefix="uc4" %>--%>

<%@ Register Src="~/WF/DC/controls/PriorityLevelCntl.ascx" TagName="PriorityCtrl" TagPrefix="Priority" %>
<%@ Register Src="~/WF/DC/controls/MailSendingWithPriorityCntl.ascx" TagName="M_s_p" TagPrefix="MailsendingPriority" %>
<%@ Register Src="~/WF/DC/controls/AdminPolicyTypeCtrl.ascx" TagPrefix="MailsendingPriority" TagName="AdminPolicyTypeCtrl" %>
<%@ Register Src="~/WF/DC/controls/AssignedtoDepartmentCtrl.ascx" TagPrefix="MailsendingPriority" TagName="AssignedtoDepartmentCtrl" %>
<%@ Register Src="~/WF/DC/controls/FixedRatePriceApproverCtrl.ascx" TagPrefix="MailsendingPriority" TagName="FixedRatePriceApproverCtrl" %>
<%@ Register Src="~/WF/DC/controls/JobAcceptanceTimeCtrl.ascx" TagPrefix="MailsendingPriority" TagName="JobAcceptanceTimeCtrl" %>
<%@ Register Src="~/WF/DC/controls/spSchedulingCtrl.ascx" TagPrefix="MailsendingPriority" TagName="spSchedulingCtrl" %>
<%@ Register Src="~/WF/DC/controls/FixedRateTypeCtrl.ascx" TagPrefix="MailsendingPriority" TagName="FixedRateTypeCtrl" %>
<%@ Register Src="~/WF/DC/controls/PolicyNumberFormatCtrl.ascx" TagPrefix="MailsendingPriority" TagName="PolicyNumberFormatCtrl" %>
<%@ Register Src="~/WF/DC/controls/TicketManagerCtrl.ascx" TagPrefix="MailsendingPriority" TagName="TicketManagerCtrl" %>
<%@ Register Src="~/WF/DC/controls/ProductAddonPricesCtrl.ascx" TagPrefix="MailsendingPriority" TagName="ProductAddonPricesCtrl" %>
<%@ Register Src="~/WF/DC/controls/MaintenanceTypeCtrl.ascx" TagPrefix="MailsendingPriority" TagName="MaintenanceTypeCtrl" %>
<%@ Register Src="~/WF/DC/controls/PortfolioPaymentSettingsCtrl.ascx" TagPrefix="MailsendingPriority" TagName="PortfolioPaymentSettingsCtrl" %>
<%@ Register Src="~/WF/DC/controls/TypeofRequestCtrl.ascx" TagPrefix="MailsendingPriority" TagName="TypeofRequestCtrl" %>
<%@ Register Src="~/WF/DC/controls/VATCtrl.ascx" TagPrefix="MailsendingPriority" TagName="VATCtrl" %>
<%@ Register Src="~/WF/DC/controls/AdminServiceChargeCtrl.ascx" TagPrefix="MailsendingPriority" TagName="AdminServiceChargeCtrl" %>
<%@ Register Src="~/WF/DC/controls/TypeCtrl.ascx" TagPrefix="MailsendingPriority" TagName="TypeCtrl" %>
<%@ Register Src="~/WF/DC/controls/InvoiceSeedCtrl.ascx" TagPrefix="MailsendingPriority" TagName="InvoiceSeedCtrl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
   <%= Resources.DeffinityRes.Settings%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    <asp:Label ID="lblPanelTitle" runat="server"></asp:Label>
    <%--<span>--%>
        
   <%-- <%= Resources.DeffinityRes.AdminDropdownLists%>  --%>
    <MailsendingPriority:TypeCtrl runat="server" id="TypeCtrl" Visible="false" />
    <%--<asp:DropDownList ID="ddlType" runat="server" ClientIDMode="Static" SkinID="ddl_50">
       
    </asp:DropDownList>--%>
   <%--</span>--%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <a class="btn btn-video" id="btn_video" runat="server" style="background-color:#50CD89;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo" href="#MainContent_MainContent_panel_options_vimeo">
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                  <iframe id="vimeo" runat="server" style="display:none" src="https://player.vimeo.com/video/773366112?h=4d14ac395f" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>
      <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/DC/FLSJlist.aspx?type=FLS"><i class="fa fa-arrow-left"></i> Return to Job list</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
     <uc2:portfolioddlctr ID="PortfolioDdlCtr1" runat="server" Visible="false" /> 
    <asp:CheckBox ID="Check1" runat="server" Text="Default Config for all Customers" OnCheckedChanged="Check1_CheckedChanged" AutoPostBack="true"  Visible="false" />
    <asp:Button ID="BtnApplyToAllCustomer" runat="server" SkinID="btnApply" Text="Default Config To All Customer" OnClick="BtnApplyToAllCustomer_Click"  Visible="false" />
  <%--  <div class="form-group row mb-6">
    <uc1:AdminDropdownTab ID="AdminDropdownTab1" runat="server" />
        </div>--%>
    <div class="form-group row mb-6">
        <div class="col-md-12">
            <asp:Label ID="lblDefaultCustomerMsg" SkinID="GreenBackcolor" runat="server"></asp:Label>
             <asp:Label ID="lblDefaultError" SkinID="RedBackcolor" runat="server"></asp:Label>
        </div>
    </div>
   
     <div class="form-group row mb-6" id="pnlAdmin" runat="server" visible="false">
        <div class="col-md-6">
            <uc2:FLSAdminDropDown ID="FLSAdmin" runat="server" />
            </div>
         </div>
      <div class="form-group row mb-6" id="pnlAssignDep" runat="server" visible="false">
        <div class="col-md-8">
            <MailsendingPriority:AssignedtoDepartmentCtrl runat="server" ID="AssignedtoDepartmentCtrl" />
            </div>
          </div>
     <div class="form-group row mb-6"  id="pnlPrioity" runat="server" visible="false">
        <div class="col-md-12">
             <div class="row">
          <div class="col-md-12">
 <strong>Priority Level </strong> 
<hr class="no-top-margin" />
	</div>
</div>
                                    <%--<Status:StatusCtrl ID="Status" runat="server" />--%>
                                      <Priority:PriorityCtrl ID="Priority1" runat="server" />
            </div>
         </div>
    <div class="form-group row mb-6" id="pnlSDesk" runat="server" visible="false">
        <div class="col-md-12">

            <div class="form-group row mb-6">
        <div class="col-md-10">
           <strong>Service Desk Distribution List</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                              
                                <div>
                                    <div class="form-group row mb-6">
                                         <div class="col-md-12">
                                    <asp:Label ID="lblMsg" runat="server" ClientIDMode="Static" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
                                     <asp:Label ID="lblError" runat="server" ClientIDMode="Static" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
                                             </div>
                                        </div>
                                    <div class="form-group row">
          <div class="col-md-12">
           <div class="col-sm-8">
                <asp:RadioButtonList ID="rbList" runat="server" RepeatDirection="Horizontal">
                                                  <asp:ListItem Text="Common ticket distribution list" Selected="True" Value="1"></asp:ListItem>
                                                  <asp:ListItem Text="Email Group by Priority" Value="2"></asp:ListItem>
                                              </asp:RadioButtonList>
            </div>
               <div class="col-sm-4">
                    <asp:LinkButton ID="btnSavetype" runat="server" Text="Save" OnClick="btnSavetype_Click" SkinID="BtnLinkButtonSave" />
            </div>
	</div>
</div>
                                 
                                </div>
                              <div style="padding-top:0px;">
                                <ajaxToolkit:TabContainer ID="Tab1" runat="server" Width="95%">
                                    <ajaxToolkit:TabPanel ID="Narmaldistributionlist" runat="server">
                                        <HeaderTemplate>
                                         Distribution list
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <ucSA:SecurityAccessMail ID="SecurityAccess1" runat="server" />
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="WithPriority" runat="server">
                                        <HeaderTemplate>
                                           Email Group by Priority
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                             <MailsendingPriority:M_s_p ID="P1" runat="server" />
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                              </div>   

        </div>

    </div>

     <div class="form-group row mb-6"  id="pnlEmailFooter" runat="server" visible="false">
        <div class="col-md-8">
             <uc3:EmailFooter ID="EmailFooter1" runat="server" RequestTypeID="6" />
            </div>
       </div>
     <div class="form-group row mb-6"  id="pnlCategory" runat="server" visible="false">
        <div class="col-md-6">
             <ucc:CategoryCtrl ID="Categoryctrl1" runat="server" />
            </div>
       </div>
     <div class="form-group row mb-6"  id="pnlOurSite" runat="server" visible="false">
        <div class="col-md-6">
             <ucSite:OurSite ID="OurSite1" runat="server" />
            </div>
       </div>
    
     <div class="form-group row mb-6"  id="pnlSourceofRequest" runat="server" visible="false">
        <div class="col-md-6">
            <ucsr:SourceOfRequestCtrl ID="SourceOfRequestCtrl1" runat="server" />
            </div>
       </div>
     <div class="form-group row mb-6"  id="pnlDepartment" runat="server" visible="false">
        <div class="col-md-6">
            <ucd:DepartmentCtrl ID="DepartmentCtrl1" runat="server" />
            </div>
       </div>
     <div class="form-group row mb-6"  id="pnlAccessEmail" runat="server" visible="false">
        <div class="col-md-6">
            <UcAE:AccessEmailId ID="AccessEmailId1" runat="server" TypeofEmail="Service Desk Email" />
            </div>
       </div>
     <div class="form-group row mb-6"  id="pnlNotification" runat="server" visible="false">
        <div class="col-md-6">
            <ucEmail:FLSEmailNotificationCtrl ID="FLSEmailNotificationCtrl1" runat="server" />
            </div>
       </div>
     <div class="form-group row mb-6"  id="pnlDocuments" runat="server" visible="false">
        <div class="col-md-6">
            <ucDoc:FLSDocumentVisibilityCtrl ID="FLSDocumentVisibilityCtrl1" runat="server" />
            </div>
       </div>
     <div class="form-group row mb-6" id="pnlPolicy" runat="server" visible="false">
        <div class="col-md-10">
            <MailsendingPriority:AdminPolicyTypeCtrl runat="server" ID="AdminPolicyTypeCtrl" />
            </div>
         </div>
      <div class="form-group row mb-6" id="pnlJobtime" runat="server" visible="false">
        <div class="col-md-10">
    <MailsendingPriority:JobAcceptanceTimeCtrl runat="server" ID="JobAcceptanceTimeCtrl" />
            </div>
          </div>
      <div class="form-group row mb-6" id="pnlFixedRate" runat="server" visible="false">
        <div class="col-md-10">
    <MailsendingPriority:FixedRatePriceApproverCtrl runat="server" ID="FixedRatePriceApproverCtrl" />
            </div>
          </div>
    <div class="form-group row mb-6" id="pnlSchedule" runat="server" visible="false">
        <div class="col-md-12">

            <MailsendingPriority:spSchedulingCtrl runat="server" id="spSchedulingCtrl" />
            </div>
        </div>

     <div class="form-group row mb-6" id="pnlServiceType" runat="server" visible="false">
        <div class="col-md-12">
            <MailsendingPriority:FixedRateTypeCtrl runat="server" ID="FixedRateTypeCtrl" />
            </div>
         </div>
     <div class="form-group row mb-6" id="pnlPolicyFormat" runat="server" visible="false">
        <div class="col-md-12">
            <MailsendingPriority:PolicyNumberFormatCtrl runat="server" id="PolicyNumberFormatCtrl" />
            </div>
         </div>
     <div class="form-group row mb-6" id="pnlTicketManager" runat="server" visible="false">
        <div class="col-md-12">
            <MailsendingPriority:TicketManagerCtrl runat="server" ID="TicketManagerCtrl" />
            </div>
         </div>
     <div class="form-group row mb-6" id="pnlAddon" runat="server" visible="false">
        <div class="col-md-12">
            <MailsendingPriority:ProductAddonPricesCtrl runat="server" id="ProductAddonPricesCtrl" />
            </div>
         </div>
     <div class="form-group row mb-6" id="pnlMaintenanceType" runat="server" visible="false">
        <div class="col-md-12">
            <MailsendingPriority:MaintenanceTypeCtrl runat="server" id="MaintenanceTypeCtrl" />
            </div>
         </div>
      <div class="form-group row mb-6" id="pnlPortfolioPaymentsettings" runat="server" visible="false">
        <div class="col-md-8">
    <MailsendingPriority:PortfolioPaymentSettingsCtrl runat="server" ID="PortfolioPaymentSettingsCtrl" />
            </div>
          </div>
    <div class="form-group row mb-6" id="pnlTypeofRequest" runat="server" visible="false">
        <div class="col-md-8">
    <MailsendingPriority:TypeofRequestCtrl runat="server" id="TypeofRequestCtrl" />
            </div>
        </div>
    <div class="form-group row mb-6" id="pnlVat" runat="server" visible="false">
        <div class="col-md-8">
            <MailsendingPriority:VATCtrl runat="server" id="VATCtrl" />
            </div>
        </div>
     <div class="form-group row mb-6" id="pnlServiceCharge" runat="server" visible="false">
        <div class="col-md-8">
            <MailsendingPriority:AdminServiceChargeCtrl runat="server" ID="AdminServiceChargeCtrl" />
            </div>
         </div>
    <div class="form-group row mb-6" id="pnlInvoiceSeed" runat="server" visible="false">
        <div class="col-md-8">
            <MailsendingPriority:InvoiceSeedCtrl runat="server" id="InvoiceSeedCtrl" />
            </div>
         </div>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="Scripts_Section" runat="Server">

 <script type="text/javascript">
     $(document).ready(function () {
         hidetabs();
         $("#lblDefaultCustomerMsg").fadeOut(8000);
     });

    
//$(function() {
//    $('#ddlType').change(function () {
//        // alert($(this).val())
//        window.location.href = "FLSDefault.aspx?tab=fls&type=" + $(this).val();
//    })
//})

</script>
    <style>
        h3.panel-title{
            width:75%;
        }
    </style>
</asp:Content>
