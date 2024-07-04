<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master"
         AutoEventWireup="true" Inherits="RFI_VendorSites_page" Codebehind="RFIVendorSites.aspx.cs" %>
<%@ Register src="controls/RFIVendorMainTabNew.ascx" tagname="RFIVendorTabs" tagprefix="uc1" %>
<%@ Register src="controls/VendorRef.ascx" tagname="RFIVendorRef" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
   <uc1:RFIVendorTabs ID="RFIVendorTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    Supplier:  <uc2:RFIVendorRef id="VendorRef1" runat="server"></uc2:RFIVendorRef>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
   Sites
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="form-group row">
            <asp:Label ID="lblmsg" runat="server"  ForeColor="Red"></asp:Label>
            <asp:ValidationSummary ID="valsum1" runat="server" ValidationGroup="grpsite" ForeColor="Red" />
     </div>
     <div class="form-group row">
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="objds_grid"
          Width="100%" DataKeyNames="VENDORSITEID" 
          OnRowCommand="GridView1_RowCommand" onrowdatabound="GridView1_RowDataBound" 
          onrowupdating="GridView1_RowUpdating" onrowupdated="GridView1_RowUpdated">
          <Columns>
              <asp:TemplateField Visible="false">
                  <ItemTemplate>
                      <asp:Label ID="lblId" runat="server" Text='<%# Bind("VENDORSITEID") %>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField ShowHeader="False">
                  <EditItemTemplate>
                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" SkinID="BtnLinkUpdate"></asp:LinkButton>
                      &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" SkinID="BtnLinkCancel"></asp:LinkButton>
                  </EditItemTemplate>
                  <ItemTemplate>
                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" SkinID="BtnLinkEdit"></asp:LinkButton>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField Visible="false">
              <EditItemTemplate><asp:Label ID="lblvendorid" runat="server" Text='<%#Eval("VendorID") %>'></asp:Label></EditItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Site Name">
                  <ItemTemplate>
                      <asp:Label ID="lblSitename" runat="server" Text='<%#Eval("SiteName") %>'></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                  <asp:TextBox ID="txtSitename" runat="server" Text='<%#Eval("SiteName") %>'></asp:TextBox>
                      <asp:RequiredFieldValidator ID="reqsite1" runat="server" ControlToValidate="txtSiteName"
                           Display="None" ErrorMessage="Please enter site name" ValidationGroup="grpsite"></asp:RequiredFieldValidator>
                  </EditItemTemplate>
                  <FooterTemplate>
                      <asp:TextBox ID="txtSitename1" runat="server"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="reqsite2" runat="server" ControlToValidate="txtSiteName1"
                           Display="None" ErrorMessage="Please enter site name" ValidationGroup="grpsite"></asp:RequiredFieldValidator>
                  </FooterTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Address">
                  <ItemTemplate>
                      <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'></asp:Label>
                  </ItemTemplate>
                   <EditItemTemplate>
                  <asp:TextBox ID="txtAddress" runat="server" Text='<%#Eval("Address") %>'></asp:TextBox>
                  </EditItemTemplate>
                  <FooterTemplate>
                      <asp:TextBox ID="txtAddress1" runat="server"></asp:TextBox>
                  </FooterTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Postcode">
                  <ItemTemplate>
                      <asp:Label ID="lblPostcode" runat="server" Text='<%#Eval("Postcode") %>'></asp:Label>
                  </ItemTemplate>
                   <EditItemTemplate>
                  <asp:TextBox ID="txtPostcode" runat="server" Text='<%#Eval("Postcode") %>' MaxLength="15"></asp:TextBox>
                  </EditItemTemplate>
                  <FooterTemplate>
                      <asp:TextBox ID="txtPostcode1" runat="server" MaxLength="15"></asp:TextBox>
                  </FooterTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Switchboard Number">
                  <ItemTemplate>
                      <asp:Label ID="lblSwitchno" runat="server" Text='<%#Eval("SwitchboardNumber") %>'></asp:Label>
                  </ItemTemplate>
                   <EditItemTemplate>
                  <asp:TextBox ID="txtSwitchno" runat="server" Text='<%#Eval("SwitchboardNumber") %>'></asp:TextBox>
                   <asp:RegularExpressionValidator ID="RegularExpressionnumber" runat="server" ErrorMessage="Enter the Mob.No."
                            ControlToValidate="txtSwitchno" ValidationGroup="grpsite" ValidationExpression="[0-9+-_\s]*" Display="None"></asp:RegularExpressionValidator>
                       <asp:RequiredFieldValidator ID="reqswichbrd1" runat='server' ControlToValidate="txtSwitchno"
                            ErrorMessage="Please enter switchboard number" ValidationGroup="grpsite" Display="None"></asp:RequiredFieldValidator>
                      </EditItemTemplate>
                  <FooterTemplate>
                      <asp:TextBox ID="txtSwitchno1" runat="server"></asp:TextBox>
                      <asp:RegularExpressionValidator ID="RegularExpressionnumber" runat="server" ErrorMessage="Enter the Mob.No."
                            ControlToValidate="txtSwitchno1" ValidationGroup="grpsite" ValidationExpression="[0-9+-_\s]*"></asp:RegularExpressionValidator>
                  </FooterTemplate>
              </asp:TemplateField>
              <asp:TemplateField>
      <ItemTemplate>
          <asp:LinkButton ID="LinkButtonDelete1" runat="server" CommandName="Delete"
                             CommandArgument='<%# Bind("VENDORSITEID")%>' SkinID="BtnLinkDelete"></asp:LinkButton>
               
      </ItemTemplate>
      <FooterTemplate>
          <asp:LinkButton runat="server" ID="btnaddsite" ValidationGroup="grpsite"
                                                          CommandName="AddSite" ToolTip="Add Site" SkinID="BtnLinkUpload"></asp:LinkButton>
          <asp:LinkButton runat="server" ID="imgbtncancel" SkinID="BtnLinkCancel"
                                          CommandName="Cancel" ToolTip="Cancel" />
      </FooterTemplate>
  </asp:TemplateField>
          </Columns>
      </asp:GridView>

         <asp:ObjectDataSource ID="objds_grid" runat='server' TypeName="Deffinity.BLL.RFI_VendorSites_Base_SVC"
                  SelectMethod="Fill" DeleteMethod="Delete" UpdateMethod="Update">
             <SelectParameters>
                 <asp:QueryStringParameter DefaultValue="0" Name="vendorid" QueryStringField="VendorID" />
             </SelectParameters>
             <UpdateParameters>
                 <asp:Parameter Name="oRFI_VendorSites" DefaultValue="_vendorsites"/></UpdateParameters>
         </asp:ObjectDataSource>
     </div>
      <div class="form-group row">
           <div class="col-md-6">
                  <asp:Button SkinID="btnDefault" runat="server" ID="imgbtnBack" Text="Back" onclick="imgbtnBack_Click" />
           </div>
           <div class="col-md-6" style="float:right;text-align:right;">
                 <asp:Button ID="btnnext" runat="server" SkinID="btnDefault" Text="Next"
                                                  ToolTip="Next" onclick="btnnext_Click" />
           </div>
      </div>
     <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
GridResponsiveCss();
 </script>
</asp:Content>

