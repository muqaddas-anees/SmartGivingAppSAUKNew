<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" CodeBehind="AddExpenses.aspx.cs" Inherits="DeffinityAppDev.WF.DC.Expenses.AddExpenses" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <%= Resources.DeffinityRes.Expenses %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Add Expenses
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="form-group row">
         <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         </div>
    <div class="form-group row  mb-6">

         <div class="col-sm-12 d-flex justify-content-end">
        <asp:Button ID="btnAdd" runat="server" SkinID="btnDefault" Text="Add"  OnClick="btnAdd_Click" />
             </div>
        </div>
    <asp:GridView ID="GridPartner" runat="server" Width="100%" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCommand="GridPartner_RowCommand" >
        <Columns>
             <asp:TemplateField ItemStyle-Width="3%" >
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnID" runat="server" Text="" CommandName="editmodule" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkEdit" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>'
                                            Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
              <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# Bind("ID") %>'></asp:Label>
                                                        <asp:Label ID="lblTimeExpensesDate" runat="server" Text='<%# Bind("TimeExpensesDate","{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Item" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItem" runat="server" Text='<%# Bind("Item") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Details" ItemStyle-Width="30%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDetails" runat="server" Text='<%# Bind("Details") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="7%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblamount" runat="server" Text='<%# Bind("amount","{0:N2}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Reimburse To" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReimburseToName" runat="server" Text='<%# Bind("ReimburseToName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Job" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProjectName" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
            <asp:TemplateField HeaderText="Accounting Code" ItemStyle-Width="10%" >
                                                    <HeaderStyle />
                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccountingCodesName" runat="server" Text='<%# Bind("AccountingCodesName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
         
            
           
              <asp:TemplateField HeaderText="" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right" >
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
                       Width="750px" Height="750px" CssClass="panel panel-color panel-info" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblOptions" runat="server" Text="Add Expenses"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="card-body">
        <div class="form-group row mb-6">
                   <div class="col-md-12 form-inline">
                       <asp:HiddenField ID="huid" runat="server" />
                       
                       <asp:ValidationSummary ID="valSumm" runat="server" ValidationGroup="Group2" />
                       </div>
            </div>

         
       
        <div class="form-group row mb-6">
                                
                                       <label class="col-sm-2 control-label">Date </label>
                                      <div class="col-sm-9 d-flex d-inline">
                                           <asp:TextBox ID="txtDate" runat="server" SkinID="Date"></asp:TextBox>
                            <asp:Label ID="imgbtnenddate7" runat="server" SkinID="Calender" ToolTip="<%$ Resources:DeffinityRes,Pickadate%>" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender7"   runat="server"
                                PopupButtonID="imgbtnenddate7" TargetControlID="txtDate" CssClass="MyCalendar">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterdate%>"
                                Display="None" ValidationGroup="Group2" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                                          </div>
                                     
    </div>
 
      <div class="form-group row mb-6">
                                 
                                       <label class="col-sm-2 control-label">Item Name </label>
                                      <div class="col-sm-9 d-flex d-inline">
                                          <asp:TextBox ID="txtItemName" runat="server" MaxLength="1000"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter item name>"
                                Display="None" ValidationGroup="Group2" ControlToValidate="txtItemName"></asp:RequiredFieldValidator>
                                           
                                          
                                      </div>
    </div>

        <div class="form-group row mb-6">
                                 
                                       <label class="col-sm-2 control-label">Details</label>
                                      <div class="col-sm-9 d-flex d-inline">
                                          <asp:TextBox ID="txtDetails" runat="server" MaxLength="1000" TextMode="MultiLine" SkinID="txtMulti_80"></asp:TextBox>
                                           
                                         
                                      </div>
    </div>
           <div class="form-group row mb-6">
                                  
                                       <label class="col-sm-2 control-label">Total </label>
                                      <div class="col-sm-9 d-flex d-inline">
                                          <asp:TextBox ID="txtTotal" runat="server" SkinID="Price_150px" MaxLength="20"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter total>"
                                Display="None" ValidationGroup="Group2" ControlToValidate="txtItemName"></asp:RequiredFieldValidator>
                                           
                                          
                                      </div>
    </div>
         <div class="form-group row mb-6">
                                  
                                       <label class="col-sm-2 control-label"> Reimburse to</label>
                                      <div class="col-sm-9 d-flex d-inline">
                                         <asp:DropDownList ID="ddlReimburseto" runat="server"></asp:DropDownList>
                                          </div>
                                      
    </div>
         <div class="form-group row mb-6">
                                  
                                       <label class="col-sm-2 control-label"> <%:sessionKeys.JobDisplayName %></label>
                                      <div class="col-sm-9 d-flex d-inline">
                                         <asp:DropDownList ID="ddlJobs" runat="server" SkinID="ddl_90">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredProjectTile" runat="server" ErrorMessage="Please select job"
                                ControlToValidate="ddlJobs" Display="None" ValidationGroup="Group2" InitialValue="0"></asp:RequiredFieldValidator>
                                          </div>
                                      
    </div>
          <div class="form-group row mb-6">
                                 
                                       <label class="col-sm-2 control-label"> Accounting code</label>
                                      <div class="col-sm-9 d-flex d-inline">
                                         <asp:DropDownList ID="ddlAccountingcode" runat="server"></asp:DropDownList>
                                          </div>
                                     
    </div>
           <div class="form-group row mb-6">
                       
                                       <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.UploadImage%></label>
                                      <div class="col-sm-9"><asp:FileUpload ID="FileUploadMaterial" runat="server"></asp:FileUpload> <asp:HiddenField ID="hImageID" runat="server" Value="00000000-0000-0000-0000-000000000000" />
                                
					
				</div>

                        </div>
       
           <div class="form-group row  mb-6">
                  
                        <label class="col-sm-2 control-label"></label>
                       <div class="col-sm-10 d-flex d-inline">
                       <asp:Button ID="btnSubmitPop" runat="server" SkinID="btnDefault" Text="Save" OnClick="btnSubmitSettings_Click" ValidationGroup="Group2" />
                       <asp:Button Visible="false" ID="btnCancelPop" runat="server" SkinID="btnCancel"  />
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
    <script>
        hidetabs();
    </script>
</asp:Content>
