<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="Training.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.Training" %>
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
    Training
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
    <asp:GridView ID="GridTraining" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridTraining_RowCommand" >
        <Columns>
             <asp:TemplateField ItemStyle-Width="5%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnID" runat="server" Text="" CommandName="editmodule" CommandArgument='<%# Bind("TrainingID") %>' SkinID="BtnLinkEdit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Training" ItemStyle-Width="20%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("TrainingID") %>'></asp:Label>
                                                        <asp:Label ID="lblTrainingName" runat="server" Text='<%# Bind("TrainingName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Description">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("TrainingDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Image" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTrainingImage" runat="server" Text='<%# Bind("TrainingImage") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Amount" ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Right">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server"  Text='<%# Bind("Amount","{0:F2}") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
              <asp:TemplateField HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                       <asp:LinkButton SkinID="BtnLinkDelete" ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Bind("TrainingID") %>' OnClientClick="return confirm('Do you want to delete the record?');"></asp:LinkButton>
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
                                       <label class="col-sm-2 control-label">Name</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtTrainingName" runat="server"></asp:TextBox>
                                           
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
                                       <label class="col-sm-2 control-label">Amount</label>
                                      <div class="col-sm-8 form-inline">
                                         <asp:TextBox ID="txtAmount" runat="server" SkinID="Price_150px" MaxLength="10"></asp:TextBox>
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
                                       <label class="col-sm-12 control-label">Training Description</label>
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

