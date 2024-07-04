<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="PartnersList.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.PartnersList" %>
<%@ Register Src="~/WF/Admin/Controls/AdminTabCtrl.ascx" TagPrefix="Pref" TagName="AdminTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Admin
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:AdminTabCtrl runat="server" ID="AdminTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Partners
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form-group">
         <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         </div>
    <div class="form-group">
        <asp:Button ID="btnAdd" runat="server" SkinID="btnDefault" Text="Add" style="float:right;" OnClick="btnAdd_Click" />
        </div>
    <asp:GridView ID="GridPartner" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridPartner_RowCommand" >
        <Columns>
             <asp:TemplateField ItemStyle-Width="5%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnID" runat="server" Text="" CommandName="editmodule" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkEdit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Partner" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:Label ID="lblPartnerName" runat="server" Text='<%# Bind("PartnerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Web URL" ItemStyle-Width="10%">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWebSite" runat="server" Text='<%# Bind("PartnerWebSite") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="123 Portal Url" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblParnerPortal" runat="server" Text='<%# Bind("ParnerPortal") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Tracker Url" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTrackerUrl" runat="server" Text='<%# Bind("TrackerUrl") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Active" ItemStyle-Width="5%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="false"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Portal name" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPortalname" runat="server" Text='<%# Bind("Portalname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Support Email" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupportEmail" runat="server" Text='<%# Bind("SupportEmail") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="From Email" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromEmail" runat="server" Text='<%# Bind("FromEmail") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
               <asp:TemplateField HeaderText="Logo" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Image ID="imglogo" runat="server" ImageUrl='<%# DisplayLogo( Eval("ID").ToString()) %>' Visible='<%# DisplayLogoVisble( Eval("ID").ToString()) %>'></asp:Image>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
               <asp:TemplateField HeaderText="Background Image" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgbacklogo" Height="100" runat="server" ImageUrl='<%# DisplayBackLogo( Eval("ID").ToString()) %>' Visible='<%# DisplayBackLogoVisble( Eval("ID").ToString()) %>'></asp:Image>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Theme" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Center" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTheme" runat="server" Text='<%# Bind("Theme") %>' Font-Bold="true"></asp:Label>
                                                        <br />
                                                        <asp:Button ID="btnSettheme" runat="server" CommandName="Theme" CommandArgument='<%# Bind("ID") %>' Text="Set Theme" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right" Visible="false">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                       <asp:LinkButton SkinID="BtnLinkDelete" ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    

     <ajaxToolkit:ModalPopupExtender ID="mdlManageOptions" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlManagePassword" CancelControlID="lbtnClosePop" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlManagePassword" runat="server" BackColor="White" Style="display:none;"
                       Width="750px" Height="620px" CssClass="panel panel-color panel-info" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="panel-heading">
							<h3 class="panel-title"><asp:Label ID="lblOptions" runat="server" Text="Partner Details"></asp:Label>  </h3>
							
							<div class="panel-options">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="panel-body">
        <div class="form-group">
                   <div class="col-md-12 form-inline">
                       <asp:HiddenField ID="huid" runat="server" />
                       
                       <asp:ValidationSummary ID="valSumm" runat="server" ValidationGroup="pay" />
                       </div>
            </div>

         
       
        <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Partner name </label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtPartnerName" runat="server" MaxLength="255"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="rfPartner" runat="server" ControlToValidate="txtPartnerName" ValidationGroup="pay" ErrorMessage="Please enter partner name" Display="None" ></asp:RequiredFieldValidator>
                                          </div>
                                      </div>
    </div>
 
        <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Web URL</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtWebSite" runat="server" MaxLength="500"></asp:TextBox>
                                           
                                          </div>
                                      </div>
    </div>
          <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">123 Portal URL</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtPortalUrl" runat="server" MaxLength="500"></asp:TextBox>
                                           
                                          </div>
                                      </div>
    </div>

         <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Portal name</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtPortalName" runat="server" MaxLength="255"></asp:TextBox>
                                           
                                          </div>
                                      </div>
    </div>
         <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Support Email</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtSupportEmail" runat="server" MaxLength="255"></asp:TextBox>
                                           
                                          </div>
                                      </div>
    </div>
         <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">From Email</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtFromEmail" runat="server" MaxLength="500"></asp:TextBox>
                                           
                                          </div>
                                      </div>
    </div>
          <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Tracker URL</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtTrackerUrl" runat="server" MaxLength="500"></asp:TextBox>
                                           
                                          </div>
                                      </div>
    </div>

           <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Active</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:CheckBox ID="chkIsActive" runat="server" ></asp:CheckBox>
                                           
                                          </div>
                                      </div>
    </div>
         <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Logo </label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:FileUpload ID="FileUpload1" runat="server" />
                                           
                                          </div>
                                      </div>
    </div>

           <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Background Image </label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:FileUpload ID="FileUpload2" runat="server" />
                                           
                                          </div>
                                      </div>
    </div>
       
           <div class="form-group">
                   <div class="col-md-12 form-inline">
                        <label class="col-sm-2 control-label"></label>
                       <div class="col-sm-10 form-inline">
                       <asp:Button ID="btnSubmitPop" runat="server" SkinID="btnDefault" Text="Save" OnClick="btnSubmitSettings_Click" ValidationGroup="pay" />
                       <asp:Button Visible="false" ID="btnCancelPop" runat="server" SkinID="btnCancel"  />
                           </div>
                       </div>
               </div>
        </div>
                   <%--  </ContentTemplate>
               <Triggers >
                   <asp:PostBackTrigger ControlID="lbtnClosePop" />
               </Triggers>
           </asp:UpdatePanel>--%>
           </asp:Panel>
     <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>


</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
