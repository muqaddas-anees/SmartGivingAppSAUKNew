<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="Modules.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.Modules" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
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
    Modules
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group">
         <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         </div>
    
    <asp:GridView ID="GridModules" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridModules_RowCommand" >
        <Columns>
             <asp:TemplateField ItemStyle-Width="5%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnID" runat="server" Text="" CommandName="editmodule" CommandArgument='<%# Bind("ModuleID") %>' SkinID="BtnLinkEdit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Module" ItemStyle-Width="20%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ModuleID") %>'></asp:Label>
                                                        <asp:Label ID="lblModuleName" runat="server" Text='<%# Bind("ModuleName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Description">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("ModuleDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Image" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModuleImage" runat="server" Text='<%# Bind("ModuleImage") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Part of Premium" ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" Enabled="false" runat="server" Checked='<%# Bind("IsPaid") %>'></asp:CheckBox>
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
                       Width="750px" Height="390px" CssClass="panel panel-color panel-info" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="panel-heading">
							<h3 class="panel-title"><asp:Label ID="lblOptions" runat="server" Text="Module Details"></asp:Label>  </h3>
							
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
                                       <label class="col-sm-2 control-label">Module</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:DropDownList ID="ddlModule" runat="server">
                                              <asp:ListItem></asp:ListItem>
                                          </asp:DropDownList>
                                           
                                          </div>
                                      </div>
    </div>
 
        <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Description</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtDescription" runat="server" MaxLength="1000" TextMode="MultiLine" SkinID="txtMulti_80"></asp:TextBox>
                                           
                                          </div>
                                      </div>
    </div>
         <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Image (Tag)</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtImage" runat="server" MaxLength="100" SkinID="txt_200px"></asp:TextBox>
                                           
                                          </div>
                                      </div>
    </div>
         <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Part of Premium</label>
                                      <div class="col-sm-8 form-inline">
                                         <asp:CheckBox ID="chkPartofPremium" runat="server"  />
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

     <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-12 control-label">Module Description</label>
                                      <div class="col-sm-12 form-inline">
                                          <CKEditor:CKEditorControl ID="CKEditor1" BasePath="~/Scripts/ckeditor/" runat="server"
                         Height="300px" ClientIDMode="Static"></CKEditor:CKEditorControl>
                                           
                                          </div>
                                      </div>
    </div>
    <div class="form-group">
                                  <div class="col-md-12">
                                      <asp:Button ID="btnUpdateDescription" runat="server" SkinID="btnDefault" Text="Submit" OnClick="btnUpdateDescription_Click" />
                                      </div>
        </div>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
