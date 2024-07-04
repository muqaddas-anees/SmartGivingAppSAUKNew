<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="PortfolioSLA" Title="Untitled Page" Codebehind="PortfolioSLA.aspx.cs" EnableEventValidation="false" %>
<%@ Register Src="controls/PortfolioMenuTab.ascx" TagName="Menu" TagPrefix="LineManagement" %>

<%@ Register src="controls/PortfolioMenuTab.ascx" tagname="PortfolioMenuTab" tagprefix="uc1" %>
<%@ Register src="controls/PortfolioDdlCtr.ascx" tagname="PortfolioDdlCtr" tagprefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerAdmin%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
      <%= Resources.DeffinityRes.SLA%> - <uc2:PortfolioDdlCtr ID="PortfolioDdlCtr1" runat="server" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="linkBack" runat="Server" NavigateUrl="~/WF/DC/FLSJlist.aspx?type=FLS"><i class="fa fa-arrow-left"></i> Return to Ticket Journal</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong>    Service Level Targets  </strong>
            <hr class="no-top-margin" />
            </div>
</div>
     <ajaxToolkit:ModalPopupExtender ID="mdlSimpleDetails" runat="server" CancelControlID="lnkCancel"
                    BackgroundCssClass="modalBackground" TargetControlID="btnPopUp" PopupControlID="pnlSimpleDetails" />  
                      <asp:Panel ID="pnlSimpleDetails" runat="server" BackColor="White"
                    Style="display: none" Width="350px" BorderStyle="Double" BorderColor="LightSteelBlue">
                          <div class="form-group row">
                               <asp:RequiredFieldValidator ID="reqval" runat="server" ControlToValidate="txtmastercategory"
                                    Text="Please enter Category" ErrorMessage="Please enter Category" ForeColor="Red"
                                    ValidationGroup="Group11"></asp:RequiredFieldValidator>
                              </div>
                          <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Category%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtmastercategory" runat="server" SkinID="txt_80" ></asp:TextBox>
					</div>
				</div>
</div>
                          <div class="form-group row">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8">
                                           <asp:Button ID="lnkOk" runat="server" Text="OK" OnClick="lnkOk_Click" SkinID="ImgSubmit"
                                    ValidationGroup="Group11" />
                            
                                <asp:Button ID="lnkCancel" runat="server" Text="Close" SkinID="ImgCancel" />
					</div>
				</div>
</div>
                </asp:Panel>

    <div class="form-group row">
         <asp:Label ID="lblError" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Group1" />
                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="GridGroup" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCategory"
                            Display="None" ErrorMessage="Please select category" InitialValue="0"
                            ValidationGroup="Group1"></asp:RequiredFieldValidator>
        </div>
    <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.TypeofRequest%></label>
                                      <div class="col-sm-8 form-inline">
                                       <asp:DropDownList ID="ddlRequestType" runat="server" SkinID="ddl_80" ClientIDMode="Static">
            </asp:DropDownList>
                                      <ajaxToolkit:CascadingDropDown ID="ccdRequestType" runat="server" TargetControlID="ddlRequestType"
                BehaviorID="ccdType" Category="type" PromptText="Please select..." PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetRequestType" LoadingText="[Loading...]" ClientIDMode="Static" />
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="*" InitialValue="0" ErrorMessage="Please select type of request" ControlToValidate="ddlRequestType"
                     ValidationGroup="Group1"></asp:RequiredFieldValidator>
                                          </div>
                                      </div>
      
        </div>
     <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Category%></label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:DropDownList ID="ddlmastercategory" runat="server" SkinID="ddl_80"></asp:DropDownList>
                                          <ajaxToolkit:CascadingDropDown ID="ccdCategory" runat="server" TargetControlID="ddlmastercategory" BehaviorID="ccdCate"
                Category="Site" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetCategoryByTypeOfRequest" LoadingText="[Loading category...]" ParentControlID="ddlRequestType" ClientIDMode="Static" />
                     <asp:Label ID="btnPopUp" runat="server" SkinID="Add" 
                      EnableViewState="false" AlternateText="Add master category" ImageAlign="AbsMiddle" Visible="false" /> 
                 <asp:RequiredFieldValidator ID="ReqMaster" runat="server" Text="*" InitialValue="0" ErrorMessage="Please select category" ControlToValidate="ddlmastercategory"
                     ValidationGroup="Group1"></asp:RequiredFieldValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.SubCategory%></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:DropDownList ID="ddlCategory" runat="server"  
                            SkinID="ddl_80">
                        </asp:DropDownList>
                                          <ajaxToolkit:CascadingDropDown ID="ccdSubCategory" runat="server" TargetControlID="ddlCategory" BehaviorID="ccdSubcate"
                Category="SubCategoryId" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/webservices/DCServices.asmx"
                ServiceMethod="GetSubCategory" LoadingText="[Loading...]" ParentControlID="ddlmastercategory" ClientIDMode="Static" />
                        <asp:TextBox ID="txtNewCategory" runat="server" Visible="False" SkinID="txt_80"></asp:TextBox>
                        <asp:LinkButton ID="btnAddCategory" runat="server" 
                            OnClick="btnAddCategory_Click" SkinID="BtnLinkAdd" ToolTip="Add Category" Visible="false" />
                        <asp:LinkButton ID="bntCancelCategory" runat="server" 
                            OnClick="bntCancelCategory_Click" SkinID="BtnLinkCancel" ToolTip="Cancel" />
                        <asp:LinkButton ID="btnDeleteCateogry" runat="server" 
                            OnClick="btnDeleteCateogry_Click" 
                            OnClientClick="return confirm('Do you want to delete the category?');" 
                            SkinID="BtnLinkDelete" ToolTip="Delete" Visible="false" />
					</div>
				</div>
</div>
     <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">Take&nbsp;In&nbsp;Hand</label>
                                      <div class="col-sm-8 form-inline"> <asp:TextBox ID="txtTakeInHand" runat="server" SkinID="Time"></asp:TextBox>(Ex: 45M/45H/45D)<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTakeInHand"
                            Display="None" ErrorMessage="Please enter valid take In Hand" SetFocusOnError="True" ValidationExpression="^[0-9]\d?[MHDmhd]{1}$"
                            ValidationGroup="Group1"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTakeInHand"
                            Display="None" ErrorMessage="Plese enter take In Hand" ValidationGroup="Group1"></asp:RequiredFieldValidator>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label">Time&nbsp;to&nbsp;Resolve</label>
                                      <div class="col-sm-8 form-inline"> <asp:TextBox ID="txtTimetoresolve" runat="server" SkinID="Time"></asp:TextBox>(Ex: 45M/45H/45D)<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTimetoresolve"
                            Display="None" ErrorMessage="Please enter valid time to resolve" SetFocusOnError="True" ValidationExpression="^[0-9]\d?[MHDmhd]{1}$"
                            ValidationGroup="Group1"></asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTimetoresolve"
                            Display="None" ErrorMessage="Plese enter time to resolve" ValidationGroup="Group1"></asp:RequiredFieldValidator>
					</div>
				</div>
</div>
     <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">Service/Problem <br />Description</label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="750px" Height="50px"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8">
					</div>
				</div>
</div>
     <div class="form-group row">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8">  <asp:Button ID="btnADDSLA" runat="server" OnClick="btnADDSLA_Click" 
                            SkinID="btnAdd" ValidationGroup="Group1" />
                        <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" 
                            SkinID="btnDefault" Text="View" ValidationGroup="Group2" Visible="False" />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"></label>
                                      <div class="col-sm-8">
					</div>
				</div>
</div>
   
    
          
        <asp:GridView ID="GridView1" runat="server" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowCommand="GridView1_RowCommand"
                    OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" 
                    OnRowUpdating="GridView1_RowUpdating" AllowPaging="True">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle Width="45px" />
                            <ItemStyle Width="45px" />
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonEdit1" runat="server" CausesValidation="false" CommandName="Edit"
                                    CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButtonUpdate1" runat="server" CommandName="Update"
                                    CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate" ToolTip="Update"
                                     ValidationGroup="GridGroup"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonCancel1" runat="server" CausesValidation="false" CommandName="Cancel"
                                    SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                        <asp:BoundField DataField="PortfolioID" HeaderText="Portfolio" Visible="False" />
                        <asp:BoundField DataField="MasterCategoryName" HeaderText=" Category" Visible="false"  />
                        <asp:TemplateField HeaderText="Type of Request">
                        <ItemTemplate><asp:Label ID="lblltypeofrequest" runat="server" Text='<%# Bind("TypeofRequestName") %>'></asp:Label></ItemTemplate>
                        <EditItemTemplate><asp:Label ID="lbleTypeofrequest" runat="server" Text='<%# Bind("TypeofRequestName") %>'></asp:Label></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category">
                        <ItemTemplate><asp:Label ID="lbldCategory" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label></ItemTemplate>
                        <EditItemTemplate><asp:Label ID="lbleCategory" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label></EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Category" HeaderText="Sub Category" Visible="False" />
                        <asp:TemplateField HeaderText="Sub Category">
                            <EditItemTemplate>
                                <asp:Label ID="lblesCategory" runat="server" Text='<%# Bind("SubcategoryName") %>'></asp:Label>
                               <%-- <asp:DropDownList ID="ddlCategory1" runat="server" DataSource='<%# BindCategoryData() %>'
                                    DataValueField="CategoryID" DataTextField="CategoryName" SelectedValue='<%# Bind("Category") %>'
                                    Width="175px">
                                <asp:DropDownList>--%>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("SubcategoryName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Take In Hand">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTakeInHand" runat="server" Text='<%# Bind("TimeInHand") %>'></asp:TextBox>
                                <asp:RegularExpressionValidator ID="Rev_Grid_TakeInHand" runat="server" ControlToValidate="txtTakeInHand"
                            Display="None" ErrorMessage="Please enter valid take In Hand" SetFocusOnError="True" ValidationExpression="^[0-9]\d?[MHDmhd]{1}$"
                            ValidationGroup="GridGroup"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="Rfv_Grid_TakeInHand" runat="server" ControlToValidate="txtTakeInHand"
                            Display="None" ErrorMessage="Plese enter take In Hand" ValidationGroup="GridGroup"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblTimeInHand" runat="server" Text='<%# Bind("TimeInHand") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Time to Resolve">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTimetoResolve1" runat="server" Text='<%# Bind("TimetoResolve") %>'></asp:TextBox>
                                <asp:RegularExpressionValidator ID="Rev_Grid_resolve" runat="server" ControlToValidate="txtTimetoResolve1"
                            Display="None" ErrorMessage="Please enter valid time to resolve" SetFocusOnError="True" ValidationExpression="^[0-9]\d?[MHDmhd]{1}$"
                            ValidationGroup="GridGroup"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="Rfv_Grid_resolve" runat="server" ControlToValidate="txtTimetoResolve1"
                            Display="None" ErrorMessage="Plese enter time to resolve" ValidationGroup="GridGroup"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("TimetoResolve") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Service Description">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescription1" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField >
                            <EditItemTemplate>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonDelete1" runat="server" CommandName="Delete"
                                    CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkDelete" ToolTip="Delete" OnClientClick="return confirm('Do you want to delete record?');">
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
  
 
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    GridResponsiveCss();
 </script> 
</asp:Content>
