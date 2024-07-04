<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="DefaultJobs.aspx.cs" Inherits="DeffinityAppDev.WF.Admin.DefaultJobs" %>

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
    Default Jobs
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

     <div class="form-group">
         <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         </div>
      <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-1 control-label">Sector</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:DropDownList ID="ddlSectorTop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSectorTop_SelectedIndexChanged" SkinID="ddl_50">
                                              
                                          </asp:DropDownList>
                                           <asp:Button ID="btnAdd" runat="server" SkinID="btnDefault" Text="Add Default Job" OnClick="btnAdd_Click"/>
                                          </div>
                                      </div>
    </div>
    <asp:GridView ID="GridModules" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridModules_RowCommand" >
        <Columns>
             <asp:TemplateField ItemStyle-Width="5%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnID" runat="server" Text="" CommandName="editmodule" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkEdit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             
             <asp:TemplateField HeaderText="Job Details">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("JobDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            
             <asp:TemplateField HeaderText="" ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                       <asp:LinkButton ID="btnDel" runat="server" SkinID="BtnLinkDelete" CommandName="del" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
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
                       Width="750px" Height="300px" CssClass="panel panel-color panel-info" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="panel-heading">
							<h3 class="panel-title"><asp:Label ID="lblOptions" runat="server" Text="Job Details"></asp:Label>  </h3>
							
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
                                       <label class="col-sm-1 control-label">Details</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtDescription" runat="server" MaxLength="1000" TextMode="MultiLine" SkinID="txtMulti_80"></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="rvDes" runat="server" ControlToValidate="txtDescription" ErrorMessage="Please enter details" Display="None" ValidationGroup="pay"></asp:RequiredFieldValidator>
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
