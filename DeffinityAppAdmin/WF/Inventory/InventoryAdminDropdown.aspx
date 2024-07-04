<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="InventoryAdminDropdown"
     MaintainScrollPositionOnPostback="true" EnableEventValidation="false" Codebehind="InventoryAdminDropdown.aspx.cs" %>
<%@ Register Src="~/WF/Inventory/Controls/Use.ascx" TagName="UseDropdown" TagPrefix="uc2" %>
<%@ Register Src="~/WF/Inventory/Controls/GridFieldConfigurator.ascx" TagName="GridFieldConfigurator" TagPrefix="uc6" %>
<%@ Register Src="~/WF/Inventory/Controls/InventoryCustomForm.ascx" TagName="InventoryCustomForm" TagPrefix="uc4" %>
<%@ Register Src="~/WF/Inventory/Controls/Inventory_Fields.ascx" TagName="fields" TagPrefix="uc2" %>
<%@ Register Src="~/WF/Inventory/Controls/InventoryConfigureFields.ascx" TagName="ConfigureFields" TagPrefix="uc2" %>
<%@ Register src="~/WF/CustomerAdmin/Controls/PortfolioDdlCtr.ascx" tagname="PortfolioDdlCtr" tagprefix="uc2" %>
<%@ Register Src="~/WF/Inventory/Controls/EmailDistributionList.ascx" TagName="DistributionList" TagPrefix="uc5" %>
<%@ Register src="~/WF/Inventory/Controls/CondtionsCntl.ascx" tagname="CondtionsCntl" tagprefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
 <%--   <ul class="tabs_list1">
        <li style="float:right;" ><a href="InventoryManager.aspx?status=0&project.aspx"
         target="_self"  title="Return to Inventory"><span>Return to Inventory</span></a></li>
        </ul>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
       Inventory Admin Dropdown
 </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_title" runat="Server">   
            <uc2:portfolioddlctr ID="PortfolioDdlCtr1" runat="server" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
                 
 <asp:Panel ID="pnlInventory" runat="server">
     <div class="form-group">
          <div class="col-md-12">
               <uc2:UseDropdown ID="UseDropdown1" runat="server" />
          </div>
    </div>
     <div class="form-group">
         <div class="col-md-12 text-bold">
             <strong>Batch Control Option</strong>
             <hr class="no-top-margin" />
         </div>
    </div>
   <div class="form-group">
         <div class="col-md-12">
               <div class="col-sm-12">
                   <asp:Label ID="lblMsg" runat="server" ForeColor="Green" EnableViewState="false"></asp:Label>
               </div>
         </div>
     </div>
    <div class="form-group">
         <div class="col-md-12">
              <div class="col-sm-12">
                  <asp:CheckBox ID="CheckControlOption" runat="server" Text="Set Batch Control" />
              </div>
         </div>
    </div>
     <div class="form-group">
         <div class="col-md-12">
              <div class="col-sm-12" style="padding-left:20px;">
                  <asp:Button ID="BtnSave" runat="server" SkinID="btnSubmit" OnClick="BtnSave_Click" />
              </div>
         </div>
    </div>


     <div class="form-group">
          <div class="col-md-12">
                 <uc6:CondtionsCntl ID="CondtionsCntl1" runat="server" />
          </div>
    </div>
     <div class="form-group">
          <div class="col-md-12">
               <uc6:GridFieldConfigurator id="GridFieldConfigurator1" runat="server" />
          </div>
    </div>
     <div class="form-group">
          <div class="col-md-12">
               <uc2:fields ID="Fields1" runat="server" />
          </div>
    </div>
     <div class="form-group">
          <div class="col-md-12">
               <uc4:InventoryCustomForm ID="inventoryFields" runat="server" />
          </div>
    </div>
     <div class="form-group">
          <div class="col-md-12">
               <uc5:DistributionList ID="DistributionList1" runat="server" />
               <uc2:ConfigureFields ID="cfields" runat="server" Visible="false" />
          </div>
    </div>
     <div class="form-group">
          <div class="col-md-12">

          </div>
    </div>
     </asp:Panel>
</asp:Content>

