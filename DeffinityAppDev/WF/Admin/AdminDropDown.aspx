<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" SmartNavigation="False"
    AutoEventWireup="true" Inherits="AdminDropDown_page" Title="Admin Dropdown"
    MaintainScrollPositionOnPostback="true" EnableEventValidation="false" Codebehind="AdminDropDown.aspx.cs" %>

<%@ Register Src="controls/AdminDropdownTab.ascx" TagName="AdminDropdownTab" TagPrefix="uc1" %>
<%@ Register Src="controls/Use.ascx" TagName="UseDropdown" TagPrefix="uc2" %>
<%@ Register Src="controls/ProjectSalesStaff.ascx" TagName="ProjectSalesStaff" TagPrefix="uc3" %>

<%@ Register Src="controls/VariationPermission.ascx" TagName="VariationPermission"
    TagPrefix="uc5" %>
<%@ Register Src="controls/CustomFieldsCtrl.ascx" TagName="CustomeFieldsCtrl" TagPrefix="uc3" %>
<%@ Register Src="controls/ProjectTaskCategoryCtrl.ascx" TagName="TaskCategory" TagPrefix="utc1" %>
<%@ Register Src="controls/ProjectGroupAdminCtrl.ascx" TagName="ProjectAdmins" TagPrefix="utc1" %>
<%@ Register src="controls/ManufacturerCtrl.ascx" tagname="ManufacturerCtrl" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
 <asp:Literal ID="litHeader" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
   <%= Resources.DeffinityRes.AdminDropdownLists%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc1:AdminDropdownTab ID="AdminDropdownTab1" runat="server" />
    <div class="form-group">
                              
                                       <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
			
</div>
    <asp:Panel ID="pnlProject" runat="server" Visible="false">
                       
                       
                  
    <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>    <%= Resources.DeffinityRes.ProjectCategory%></strong>
            <hr class="no-top-margin" />
            </div>
</div>
                                    <div class="form-group">
             <div class="col-md-12">
                 <div>
                     <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-sm-3 control-label">
                                                <asp:Label ID="Label7" runat="server" Text="<%$ Resources:DeffinityRes, Category%>"></asp:Label>
                                            </div>
                                            <div class="col-sm-9 form-inline">
                                                <asp:TextBox ID="txtCategory" runat="server" Visible="False" ValidationGroup="Category1"
                                                    SkinID="txt_80"></asp:TextBox>
                                                <asp:DropDownList ID="ddlCategory" runat="server" DataSourceID="SqlDataSource8" DataTextField="CategoryName"
                                                    DataValueField="CategoryID" ValidationGroup="loged" SkinID="ddl_80">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                    SelectCommand="DEFFINITY_ADMINCATEGORY" SelectCommandType="StoredProcedure">
                                                </asp:SqlDataSource>
                                                 <asp:LinkButton runat="server" ID="BtnDelCategory" OnClientClick="return confirm('Do you want to delete the record?');"
                                                    ValidationGroup="Category2" OnClick="BtnDelCategory_Click" SkinID="BtnLinkDelete" />
                                            </div>
                                            
                                        </div>
                         </div>
                     <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-sm-3 control-label">
                                            </div>
                                            <div class="col-sm-9 form-inline">
                                                <asp:Button ID="btnAddCategory" runat="server" OnClick="btnAddCategory_Click"
                                                    SkinID="btnAdd" />
                                                <asp:Button ID="btnEditCategory" runat="server" ValidationGroup="Category2"
                                                    OnClick="btnEditCategory_Click" SkinID="btnEdit" />
                                                <asp:Button ID="btnSubmitCategory" runat="server" Visible="false" ValidationGroup="Category1"
                                                    OnClick="btnSubmitCategory_Click" SkinID="btnSubmit" />
                                                <asp:Button ID="btnCancelCategory" runat="server" OnClick="btnCancelCategory_Click"
                                                    SkinID="btnCancel" />
                                            </div>
                                        </div>
                         </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlCategory"
                                                    ErrorMessage="<%$ Resources:DeffinityRes, PleaseSelect%>" InitialValue="0" ValidationGroup="Category2"><%= Resources.DeffinityRes.PleaseselectCategory%></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtCategory"
                                                    ErrorMessage="<%$ Resources:DeffinityRes,PleaseEnterCategory%>" ValidationGroup="Category1"><%= Resources.DeffinityRes.PleaseEnterCategory%></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
</div>
</div>

                                    </div>
                                  <div class="col-md-6">
                                      <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>  <%= Resources.DeffinityRes.ExpensesEntryType%></strong>
            <hr class="no-top-margin" />
            </div>
</div>
                                      <div class="form-group">
             <div class="col-md-12">
                 <div>
                     <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-sm-3 control-label">
                                                <%= Resources.DeffinityRes.ExpensesEntryType%>
                                            </div>
                                            <div class="col-sm-9 form-inline">
                                                <asp:TextBox ID="txt_ExpensesEntryTyp" runat="server" Visible="False" SkinID="txt_80"></asp:TextBox>
                                                <asp:DropDownList ID="ExpensesEntryType" runat="server" DataSourceID="SqlDataSource14"
                                                    DataTextField="ExpensesentryType" DataValueField="ID" AutoPostBack="true" ValidationGroup="ProVal3"
                                                    SkinID="ddl_80" OnSelectedIndexChanged="ExpensesEntryType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:LinkButton runat="server" ID="ImageExpensesEntryType" OnClientClick="return confirm('Do you want to delete the record?');"
                                                    SkinID="BtnLinkDelete" OnClick="ImageExpensesEntryType_Click" />
                                            </div>
                                        </div>
                         </div>
                     <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-sm-3 control-label">
                                                <%= Resources.DeffinityRes.BuyingPrice%>
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txt_UnitPrice" runat="server" SkinID="Price_100px"></asp:TextBox>
                                            </div>
                                        </div>
                         </div>
                     <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-sm-3 control-label">
                                                <%= Resources.DeffinityRes.SellingPrice%>
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txt_sellingprice" runat="server" SkinID="Price_100px"></asp:TextBox>
                                            </div>
                                        </div>
                         </div>
                     <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="col-sm-3 control-label">
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:SqlDataSource ID="SqlDataSource14" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                    SelectCommand="DEFFINITY_ADMINExpensesType" SelectCommandType="StoredProcedure">
                                                </asp:SqlDataSource>
                                                <asp:Button ID="btnAdd12" runat="server" SkinID="btnAdd" OnClick="ImageButton1_Click" />
                                                <asp:Button ID="btnedit" runat="server" SkinID="btnEdit" OnClick="btnedit_Click"
                                                    ValidationGroup="EntryVal2" />
                                                <asp:Button ID="btnSubmit" runat="server" Visible="false" SkinID="btnSubmit"
                                                    ValidationGroup="EntryVal1" OnClick="btnSubmit_Click" />
                                                <asp:Button ID="btnCancel" runat="server" SkinID="btnCancel" OnClick="btnCancel_Click" />
                                            </div>
                                        </div>
                         </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <asp:ValidationSummary ID="GETVAL" ValidationGroup="EntryVal2" runat="server" />
                                                <asp:ValidationSummary ID="TESTENTRT" ValidationGroup="EntryVal1" runat="server" />
                                                <asp:RequiredFieldValidator ID="RA10" runat="server" ControlToValidate="ExpensesEntryType"
                                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseselectentrytype%>"
                                                    InitialValue="0" ValidationGroup="EntryVal2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="ReDS8" Display="None" runat="server" ControlToValidate="txt_ExpensesEntryTyp"
                                                    ErrorMessage="<%$ Resources:DeffinityRes,Pleaseenterentrytype%>" ValidationGroup="EntryVal1"> </asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="Comr5" runat="server" ControlToValidate="txt_UnitPrice"
                                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidprice%>"
                                                    Operator="DataTypeCheck" Type="Double" ValidationGroup="EntryVal1"></asp:CompareValidator>
                                                <asp:CompareValidator ID="ComparER1" runat="server" ControlToValidate="txt_sellingprice"
                                                    Display="None" ErrorMessage="<%$ Resources:DeffinityRes,Pleaseentervalidprice%>"
                                                    Operator="DataTypeCheck" Type="Double" ValidationGroup="EntryVal1"></asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>
</div>
</div>
                                    </div>
                                 </div>
    <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>  Variations Permission </strong>
            <hr class="no-top-margin" />
            </div>
</div>
                                    <div class="form-group">
             <div class="col-md-12">
                  <uc5:VariationPermission ID="variationPermission" runat="server" />
</div>
</div>

                                    </div>
                                  <div class="col-md-6">
                                      <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   <%= Resources.DeffinityRes.AdditionalProjectFields%> </strong>
            <hr class="no-top-margin" />
            </div>
</div>
                                       <div class="form-group">
        <div class="col-md-12">
                                       <uc3:CustomeFieldsCtrl ID="CustomFieldsCtrl" runat="server" />
            </div>
                                           </div>
                                      
                                    </div>
                                 </div>
    <div class="row">
                                <div class="col-md-6">
                                    
<div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>PO Drain Selection (Pipeline Dashboard)</strong>
            <hr class="no-top-margin" />
            </div>
</div>

                                    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> Select&nbsp;Type</label>
                                      <div class="col-sm-9">
                                           <asp:DropDownList ID="ddlHoursType" runat="server" SkinID="ddl_80">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                                    ControlToValidate="ddlHoursType" ErrorMessage="Plese select type" ValidationGroup="GroupPO"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
                                    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-9"> <asp:Button ID="imgApply" runat="server" SkinID="btnApply" ValidationGroup="GroupPO"
                                                    OnClick="imgApply_Click" />   
					</div>
				</div>
                </div>
                                  
                                    <div class="form-group">
             <div class="col-md-12">
                   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="GroupPO" />
                  <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                            
                 
</div>
</div>
                                    <asp:GridView ID="gridHoursType" Width="100%" runat="server" AutoGenerateColumns="false"
                                                    OnRowCommand="gridHoursType_RowCommand" OnRowDeleting="gridHoursType_RowDeleting">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Hours Type" DataField="HourName" HeaderStyle-CssClass="header_bg_l" />
                                                        <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="HID" runat="server" Value='<%# Bind("ID") %>' />
                                                                <asp:LinkButton ID="deletebut" runat="server" CommandName="delete" SkinID="BtnLinkDelete"
                                                                    OnClientClick="return confirm('Do you want to delete ?');" ToolTip="<%$ Resources:DeffinityRes,Delete%>"
                                                                    Visible="True" CommandArgument='<%# Bind("ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                    </div>
                                  <div class="col-md-6">
                                      
<div class="form-group">
        <div class="col-md-12 text-bold">
             <strong> PO Drain Round Up (Pipeline Dashboard)</strong>
            <hr class="no-top-margin" />
            </div>
</div>
 <div class="form-group">
             <div class="col-md-12">
                          <asp:Label ID="lblMsgRoundUp" runat="server" Text=""></asp:Label>
                                    <asp:ValidationSummary ID="ValidationSummary2" ValidationGroup="GropCHk" runat="server" />
                 <br />
                 <asp:CheckBox ID="chkRoundUp" runat="server" Text="Round Up" />             
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> Half&nbsp;Day&nbsp;Hours</label>
                                      <div class="col-sm-9 form-inline"> <asp:TextBox ID="txtHalfDay" runat="server" SkinID="Price_100px" MaxLength="5"></asp:TextBox>
                                                (HH:MM)
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter half day value"
                                                    Display="None" ControlToValidate="txtHalfDay" ValidationGroup="GropCHk"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtHalfDay"
                                                    ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="GropCHk"
                                                    Display="None" Text="*" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidTime%> "></asp:RegularExpressionValidator>
					</div>
				</div>
                </div>
            <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label">Full&nbsp;Day&nbsp;Hours</label>
                                      <div class="col-sm-9 form-inline"><asp:TextBox ID="txtFullDay" runat="server" SkinID="Price_100px" MaxLength="5"></asp:TextBox>
                                                (HH:MM)
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter full day value"
                                                    Display="None" ControlToValidate="txtFullDay" ValidationGroup="GropCHk"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="regex1" runat="server" ControlToValidate="txtFullDay"
                                                    ValidationExpression="^(20|21|22|23|[01]\d|\d)(([:][0-5]\d){1,2})$" ValidationGroup="GropCHk"
                                                    Display="None" Text="*" ErrorMessage="<%$ Resources:DeffinityRes,PleaseentervalidTime%> "></asp:RegularExpressionValidator>
					</div>
				</div>
                </div>
            <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-9"><asp:Button ID="imgApply1" runat="server" SkinID="btnApply" ValidationGroup="GropCHk"
                                                    OnClick="imgApply1_Click" />
					</div>
				</div>
                </div>
                                    </div>
                                 </div>
    <div class="row" id="pnl_projectclass_head" runat="server">
                                <div class="col-md-6">
                                    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>  Project Class</strong>
            <hr class="no-top-margin" />
            </div>
</div>
 <div id="pnl_projectclass_content" runat="server">
                                    <div class="form-group">
             <div class="col-md-12">
                             <p>
                                        This section allows you to apply a checklist to a project class. When a project
                                        class is applied to a project, the project manager cannot move the project to the
                                        next phase unless someone with QA/UAT or an Administrator signs off the current
                                        phase</p>          
				</div>
                </div>      
                                    <div class="form-group">
             <div class="col-md-12">
                                   <asp:Label ID="lblmsgProjectclass" runat="server" EnableViewState="false"></asp:Label>   
				</div>
                </div>      
          <div class="form-group">
             <div class="col-md-12">
                  <asp:CheckBox ID="ChkEnableClass" runat="server" Text="Enable Project Class" AutoPostBack="True"
                                                    OnCheckedChanged="ChkEnableClass_CheckedChanged1" />
                 </div>
              </div>
 <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.ProjectClass%></label>
                                      <div class="col-sm-9 form-inline"><asp:DropDownList ID="ddlProjectClass" runat="server" SkinID="ddl_80" OnSelectedIndexChanged="ddlProjectClass_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtProjectClass" runat="server" SkinID="txt_80"></asp:TextBox>
                                           <asp:LinkButton ID="btnAddProjectClass" runat="server" SkinID="BtnLinkAdd" OnClick="btnAddProjectClass_Click" />
                                                <asp:LinkButton ID="btnInsertProjectClass" runat="server" SkinID="BtnLinkUpdate"
                                                    OnClick="btnInsertProjectClass_Click" />
                                                <asp:LinkButton ID="btnCancelProjectClass" runat="server" SkinID="BtnLinkCancel"
                                                    OnClick="btnCancelProjectClass_Click" />
                                                <asp:LinkButton ID="btnDeleteProjectClass" runat="server" SkinID="BtnLinkDelete" OnClick="btnDeleteProjectClass_Click" />
                                           <asp:HyperLink ID="linkProjectpipeline" runat="server" Text="Go to Project Pipeline"
                                                    Font-Bold="true" NavigateUrl="~/WF/Projects/ProjectPipeline.aspx?status=2"></asp:HyperLink>
					</div>
				</div>
                </div>
 <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> </label>
                                      <div class="col-sm-9"> <asp:RadioButtonList ID="radioClassSelect" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Text="From Pending to Live" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="From Live to Complete"></asp:ListItem>
                                                </asp:RadioButtonList>
					</div>
				</div>
                </div>
 <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.CheckList%></label>
                                      <div class="col-sm-9"><asp:DropDownList ID="ddlChecklist" runat="server" SkinID="ddl_80">
                                                </asp:DropDownList>
                                           <asp:HyperLink ID="HyperLink1" runat="server" Text="Manage checklists" Font-Bold="true"
                                                    NavigateUrl="~/WF/Admin/adminmasterlists.aspx?setval=6&type=class"></asp:HyperLink>   
					</div>
				</div>
                </div>
                
                     <div class="form-group">
             <div class="col-md-12 col-md-offset-3">
                           <asp:Button ID="btnProjectClassApply" runat="server" SkinID="btnApply" OnClick="btnProjectClassApply_Click" />           
				</div>
                </div>      
     <asp:GridView ID="gridClass" runat="server" OnRowCommand="gridClass_RowCommand" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="ClassName" HeaderText="Project Class" HeaderStyle-CssClass="header_bg_l" />
                                                        <asp:BoundField DataField="assignedStatusName" HeaderText="Status Change" />
                                                        <asp:BoundField DataField="MasterChecklistName" HeaderText="Applied Checklist" />
                                                        <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnClassDelete" runat="server" SkinID="BtnLinkDelete" CommandArgument="<%# Bind('ID')%>"
                                                                    CommandName="delete_item" OnClientClick="javascript:confirm('do you want to delete?')" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
     </div>

                                     

                                    </div>
                                  <div class="col-md-6">
                                      <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>Sales Staff </strong>
            <hr class="no-top-margin" />
            </div>
</div>
                                      
<div class="form-group">
             <div class="col-md-12">
                  <div id="Td1" runat="server">
                                    <uc3:ProjectSalesStaff runat="server" />
                                </div>

</div>
</div>
                                    </div>
                                 </div>
    <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>KPI Category</strong>
            <hr class="no-top-margin" />
            </div>
</div>
 
 <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <asp:Label ID="lblKbi" runat="server" Text="KPI Category"></asp:Label></label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtKpiCategory" runat="server" Visible="False" ValidationGroup="Categorykpi2"
                                                SkinID="txt_80"></asp:TextBox>
                                            <asp:DropDownList ID="ddlKPIcatgory" runat="server" DataSourceID="SqlDataSource1"
                                                DataTextField="KPiCatogeryName" DataValueField="ID" ValidationGroup="loged" SkinID="ddl_80">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                SelectCommand="DEFFINITY_KPI_CATEGORY" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                          <asp:LinkButton runat="server" ID="imgKPIdelete" OnClientClick="return confirm('Do you want to delete the record?');"
                                                ValidationGroup="Categorykpi1" SkinID="BtnLinkDelete" OnClick="imgKPIdelete_Click" />
					</div>
				</div>
                </div>
<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> </label>
                                      <div class="col-sm-9 form-inline"> <asp:Button ID="imgAddKpi" runat="server" SkinID="btnAdd" OnClick="imgAddKpi_Click" />
                                            <asp:Button ID="imgEditKpi" runat="server" ValidationGroup="Categorykpi1" SkinID="btnEdit"
                                                OnClick="imgEditKpi_Click" />
                                            <asp:Button ID="imgKPISubmit" runat="server" Visible="false" ValidationGroup="Categorykpi2"
                                                SkinID="btnSubmit" OnClick="imgKPISubmit_Click" />
                                            <asp:Button ID="imgKPICancel" runat="server" SkinID="btnCancel" OnClick="imgKPICancel_Click" />
					</div>
				</div>
                </div>
<div class="form-group">
             <div class="col-md-12">
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4c" runat="server" ControlToValidate="ddlKPIcatgory"
                                                ErrorMessage="<%$ Resources:DeffinityRes, PleaseSelect%>" InitialValue="0" ValidationGroup="Categorykpi1"><%= Resources.DeffinityRes.PleaseselectCategory%></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15c" runat="server" ControlToValidate="txtKpiCategory"
                                                ErrorMessage="<%$ Resources:DeffinityRes,PleaseEnterCategory%>" ValidationGroup="Categorykpi2"><%= Resources.DeffinityRes.PleaseEnterCategory%></asp:RequiredFieldValidator>
				</div>
                </div>
<div class="form-group">
             <div class="col-md-12">
                                      
				</div>
                </div>
                                                               


                                    </div>
                                  <div class="col-md-6">
                                      <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong> KPI Labels</strong>
            <hr class="no-top-margin" />
            </div>
</div>
 <asp:Panel Visible="false" runat="server" ID="pnl">
<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label">  KPI Label</label>
                                      <div class="col-sm-9"><asp:TextBox ID="txtAddLable" runat="server" SkinID="txt_80"></asp:TextBox>
					</div>
				</div>
                </div>
 <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label">  Select Category</label>
                                      <div class="col-sm-9"><asp:DropDownList ID="ddlCategory_KPI" runat="server" SkinID="ddl_80" DataSourceID="SqlDataSource1"
                                                    DataTextField="KPiCatogeryName" DataValueField="ID">
                                                </asp:DropDownList>
					</div>
				</div>
                </div>
 <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> Select Page</label>
                                      <div class="col-sm-9">
                                          <asp:DropDownList ID="ddlPageNo" runat="server" SkinID="ddl_80">
                                                    <asp:ListItem Selected="True" Text="Please select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Please select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="KPI Financial" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="KPI Resources" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="KPI Customers" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="KPI Service" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="KPI Internal Perspective" Value="5"></asp:ListItem>
                                                </asp:DropDownList>
                                               <asp:Button ID="imgAdd" SkinID="btnAdd" runat="server" ValidationGroup="KPI"
                                                    OnClick="imgAdd_Click" />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                      <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="KPI" />
                                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="KPIGrid" />
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator15w" runat="server"
                                                    ControlToValidate="txtAddLable" ErrorMessage="Please enter label name" ValidationGroup="KPI"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4w" runat="server"
                                                    ControlToValidate="ddlCategory_KPI" InitialValue="0" ValidationGroup="KPI" ErrorMessage="Please select category"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4sd" runat="server"
                                                    ControlToValidate="ddlPageNo" InitialValue="0" ValidationGroup="KPI" ErrorMessage="Please select page type"></asp:RequiredFieldValidator>
                 </div>
                                          </div>
      </asp:Panel>
 <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label">  Select&nbsp;Page:</label>
                                      <div class="col-sm-9">
                                           <asp:DropDownList ID="ddlPageType" runat="server" SkinID="ddl_80" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlPageType_SelectedIndexChanged">
                                                <asp:ListItem Text="KPI-Financial" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="KPI-Resources" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="KPI-Customers" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="KPI-Service" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="KPI-Internal Perspective" Value="5"></asp:ListItem>
                                            </asp:DropDownList>
					</div>
				</div>
                </div>
 
 <asp:Panel ID="grid_scroll" runat="server" Height="350px" ScrollBars="Vertical">                                  
  <asp:GridView ID="gridKPILables" runat="server" AutoGenerateColumns="false" Width="100%"
                                                EmptyDataText="No Records Found" OnRowCancelingEdit="gridKPILables_RowCancelingEdit"
                                                OnRowCommand="gridKPILables_RowCommand" OnRowDataBound="gridKPILables_RowDataBound"
                                                OnRowDeleting="gridKPILables_RowDeleting" OnRowEditing="gridKPILables_RowEditing"
                                                OnRowUpdating="gridKPILables_RowUpdating">
                                                <Columns>
                                                    <asp:TemplateField ControlStyle-CssClass="form-inline" ItemStyle-CssClass="form-inline" >
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle Width="40px" />
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID")%>' Visible="false"> </asp:Label>
                                                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                                                        CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="Edit">
                                                                    </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                             <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                                        CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate" ToolTip="Update"
                                                                        ValidationGroup="KPIGrid"></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                                        SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lable Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLabelName" runat="server" Text='<%# Bind("LabelsName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtKpiLabelName" runat="server" Width="150px" Text='<%#Bind("LabelsName")%>'></asp:TextBox>
                                                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator15grid" runat="server"
                                                                ControlToValidate="txtKpiLabelName" ErrorMessage="Please enter label name" ValidationGroup="KPIGrid"></asp:RequiredFieldValidator>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblCategoryID" runat="server" Text='<%# Bind("KPICategoryID") %>'
                                                                Visible="false"></asp:Label>
                                                            <asp:DropDownList ID="ddlCategoryKpi" runat="server" Width="150px" DataSourceID="SqlDataSource1"
                                                                DataTextField="KPiCatogeryName" DataValueField="ID">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4grid" runat="server"
                                                                ControlToValidate="ddlCategoryKpi" InitialValue="0" ValidationGroup="KPIGrid"
                                                                ErrorMessage="Please select category"></asp:RequiredFieldValidator>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="deletebut0" runat="server" CommandArgument='<%# Bind("ID") %>'
                                                                CommandName="delete" OnClientClick="return confirm('Do you want to delete ?');"
                                                                SkinID="BtnLinkDelete" ToolTip="<%$ Resources:DeffinityRes,Delete%>" Visible="True" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>                                  
                                      </asp:Panel> 
                                    </div>
                                 </div>
        <div class="row">
             <div class="col-md-6">
                 &nbsp;
                 </div>
              <div class="col-md-6">
                 </div>
            </div>

         <div class="row">
              <div class="col-md-6">
                   <utc1:TaskCategory ID="TaskCategory1"  runat="server" ></utc1:TaskCategory>
              </div>
             <div class="col-md-6">
                 <div class="form-group">
        <div class="col-md-12">
           <strong>Project Admin Group Distribution list </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                 
                                    
                                    
                        <utc1:ProjectAdmins ID="CtrlProjectAdmin" runat="server" />
             </div>
             </div>

          <div class="row">
              <div class="col-md-6">
                   <uc4:ManufacturerCtrl ID="ManufacturerCtrl1" runat="server" />
              </div>
             <div class="col-md-6">

             </div>
             </div>
        
</asp:Panel>
     <asp:Panel ID="pnlTS" runat="server" Visible="false">
    <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>  Timesheet Entry Type </strong>
            <hr class="no-top-margin" />
            </div>
</div>

 <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"><asp:Label ID="Label5" runat="server" Text="<%$ Resources:DeffinityRes, TimesheetType%>"></asp:Label></label>
                                      <div class="col-sm-9 form-inline">
                                           <asp:TextBox ID="txtTimeType" runat="server" Visible="False" ValidationGroup="ttg2"
                                                    SkinID="txt_80"></asp:TextBox><asp:DropDownList ID="ddlTimeType" runat="server" DataSourceID="SqlDataSource6"
                                                        DataTextField="EntryType" DataValueField="ID" ValidationGroup="ttg1" SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                    SelectCommand="DEFFINITY_ADMINTimesheetType" SelectCommandType="StoredProcedure">
                                                </asp:SqlDataSource>
                                          <asp:LinkButton runat="server" ID="imgDeleteTimeType" SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete the record?');"
                                                    ValidationGroup="ttg1" OnClick="imgDeleteTimeType_Click" />
					</div>
				</div>
                </div>
 <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-9 form-inline"><asp:Button ID="ImgAddTimeType" runat="server" SkinID="btnAdd" OnClick="ImgAddTimeType_Click" />
                                                <asp:Button ID="ImgEditTimeType" runat="server" SkinID="btnEdit" ValidationGroup="ttg1"
                                                    OnClick="ImgEditTimeType_Click" />
                                                <asp:Button ID="ImgSubmitTimeType" runat="server" SkinID="btnSubmit" Visible="false"
                                                    ValidationGroup="ttg2" OnClick="ImgSubmitTimeType_Click" />
                                                <asp:Button ID="ImgCancelTimeType" runat="server" SkinID="btnCancel" OnClick="ImgCancelTimeType_Click" />
					</div>
				</div>
                </div>
<div class="form-group">
             <div class="col-md-12">
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlTimeType"
                                                    ErrorMessage="<%$ Resources:DeffinityRes, PleaseselectTimesheettype%>" InitialValue="0"
                                                    ValidationGroup="ttg1"><%= Resources.DeffinityRes.PleaseselectTimesheettype%></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtTimeType"
                                                        ErrorMessage="<%$ Resources:DeffinityRes,PleaseEnterTimesheettype%>" ValidationGroup="ttg2"><%= Resources.DeffinityRes.PleaseEnterTimesheettype%></asp:RequiredFieldValidator>
				</div>
                </div>
                                  

                                    </div>
                                  <div class="col-md-6">
                                      <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>    Timesheet Category</strong>
            <hr class="no-top-margin" />
            </div>
</div>
 <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> Category</label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtCategoryName" runat="server" Visible="False" ValidationGroup="Cat2"
                                                    SkinID="txt_80"></asp:TextBox><asp:DropDownList ID="ddlCategoryName" runat="server"
                                                        DataSourceID="SqlDataSourceCategory" DataTextField="CategoryName" DataValueField="ID"
                                                        ValidationGroup="Cat1" SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSourceCategory" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                    SelectCommand="DEFFINITY_ADMINTimesheetCategory" SelectCommandType="StoredProcedure">
                                                </asp:SqlDataSource>
                                           <asp:LinkButton runat="server" ID="btnCatDelete" SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete the record?');"
                                                    ValidationGroup="Cat2" OnClick="btnCatDelete_Click" />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-9 form-inline"> <asp:Button ID="btnCatAdd" runat="server" SkinID="btnAdd" OnClick="btnCatAdd_Click" />
                                                <asp:Button ID="btnCatEdit" runat="server" SkinID="btnEdit" ValidationGroup="Cat1"
                                                    OnClick="btnCatEdit_Click" />
                                                <asp:Button ID="btnCatSubmit" runat="server" SkinID="btnSubmit" Visible="false"
                                                    ValidationGroup="Cat2" OnClick="btnCatSubmit_Click" />
                                                <asp:Button ID="btnCatCancel" runat="server" SkinID="btnCancel" OnClick="btnCatCancel_Click" />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <asp:RequiredFieldValidator ID="RfvCat" runat="server" ControlToValidate="ddlCategoryName"
                                                    ErrorMessage="Please select Category" InitialValue="0" ValidationGroup="Cat1"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtCategoryName"
                                                        ErrorMessage="Please enter Category" ValidationGroup="Cat2"></asp:RequiredFieldValidator>
				</div>
                </div>
                                            

                                    </div>
                                 </div>
   

    
                        
                    </asp:Panel>




     <asp:Panel ID="pnlResources" runat="server" Visible="false">
                   
    <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong> <%= Resources.DeffinityRes.ExperienceClassification%> </strong>
            <hr class="no-top-margin" />
            </div>
</div>

<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.ExperienceClassification%></label>
                                      <div class="col-sm-9 form-inline"><asp:TextBox ID="txtExperienceClassification" runat="server" Visible="False" ValidationGroup="IssueVal1"
                                                    SkinID="txt_80"></asp:TextBox><asp:DropDownList ID="ddlExperienceClassification" runat="server"
                                                        DataSourceID="SqlDataSource11" DataTextField="ExpClassification" DataValueField="ID"
                                                        ValidationGroup="IssueVal3" SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                <asp:LinkButton runat="server" ID="BtnDelExperienceClassification" OnClientClick="return confirm('Do you want to delete the record?');"
                                                    ValidationGroup="ExperienceClassification2" SkinID="BtnLinkDelete" OnClick="BtnDelExperienceClassification_Click" />
					</div>
				</div>
                </div>
 <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> </label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:SqlDataSource ID="SqlDataSource11" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                    SelectCommand="Deffinity_ExperienceClassificationSelect" SelectCommandType="StoredProcedure">
                                                </asp:SqlDataSource>
                                                <asp:Button ID="btnaddExperienceClassification" runat="server" SkinID="btnAdd"
                                                    OnClick="btnaddExperienceClassification_Click" />
                                                <asp:Button ID="btneditExperienceClassification" runat="server" ValidationGroup="ExperienceClassification2"
                                                    SkinID="btnEdit" OnClick="btneditExperienceClassification_Click" />
                                                <asp:Button ID="btnsubmitExperienceClassification" runat="server" Visible="false"
                                                    ValidationGroup="ExperienceClassification1" SkinID="btnSubmit" OnClick="btnsubmitpExperienceClassification_Click" />
                                                <asp:Button ID="btncancelExperienceClassification" runat="server" SkinID="btnCancel"
                                                    OnClick="btncancelExperienceClassification_Click" />
					</div>
				</div>
                </div>
 <div class="form-group">
             <div class="col-md-12">
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="ddlExperienceClassification"
                                                    ErrorMessage="<%$ Resources:DeffinityRes, PleaseSelect%>" InitialValue="0" ValidationGroup="ExperienceClassification2"><%= Resources.DeffinityRes.PleaseselectExperienceclassification%></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtExperienceClassification"
                                                        ErrorMessage="<%$ Resources:DeffinityRes,PleaseenterExperienceclassification%>"
                                                        ValidationGroup="ExperienceClassification1">  <%= Resources.DeffinityRes.PleaseenterExperienceclassification%></asp:RequiredFieldValidator>       
				</div>
                </div>                          


                                    </div>
                                  <div class="col-md-6">
                                      <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>  <%= Resources.DeffinityRes.LabourCategoryType%> </strong>
            <hr class="no-top-margin" />
            </div>
</div>
 
<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.CasualLabourType%></label>
                                      <div class="col-sm-9 form-inline">
                                           <asp:TextBox ID="txt_lobourTypename" runat="server" SkinID="txt_80" Visible="false"></asp:TextBox><asp:DropDownList
                                                    ID="ddllobourNames" runat="server" DataSourceID="SqlDataSource15" DataTextField="LabourType"
                                                    DataValueField="ID" SkinID="ddl_80">
                                                </asp:DropDownList>
                                                <asp:LinkButton runat="server" ID="btn_deleteLobour" OnClientClick="return confirm('Do you want to delete the record?');"
                                                    SkinID="BtnLinkDelete" />
					</div>
				</div>
                </div>
<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-9 form-inline">
                                           <asp:SqlDataSource ID="SqlDataSource15" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                    SelectCommand="Deffinity_LabourCategory" SelectCommandType="StoredProcedure">
                                                </asp:SqlDataSource>
                                                <asp:Button ID="btn_addLoabour" runat="server" SkinID="btnAdd" OnClick="btn_addLoabour_Click" />
                                                <asp:Button ID="btn_edditLoabour" runat="server" ValidationGroup="clt2" SkinID="btnEdit"
                                                    OnClick="btn_edditLoabour_Click" />
                                                <asp:Button ID="btn_submittLabour" runat="server" Visible="false" ValidationGroup="clt1"
                                                    SkinID="btnSubmit" OnClick="btn_submittLabour_Click" />
                                                <asp:Button ID="btn_CancelLobour" runat="server" SkinID="btnCancel" OnClick="btn_CancelLobour_Click" />
					</div>
				</div>
                </div>
<div class="form-group">
             <div class="col-md-12">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="ddllobourNames"
                                                    ErrorMessage="<%$ Resources:DeffinityRes, PleaseSelect%>" InitialValue="0" ValidationGroup="clt2"> <%= Resources.DeffinityRes.Pleaseselectlabourtype%></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator28" runat="server" ControlToValidate="txt_lobourTypename"
                                                        ErrorMessage="<%$ Resources:DeffinityRes,PleaseEnterCategory%>" ValidationGroup="clt1"><%= Resources.DeffinityRes.Pleaseenterlabourtype%></asp:RequiredFieldValidator>      
				</div>
                </div>
                                                                   

                                    </div>
                                 </div>
          </asp:Panel>
    <asp:Panel ID="pnlIssues" runat="server" Visible="false">
                    
    <div class="row">
                                <div class="col-md-6">
                                    
<div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   <%= Resources.DeffinityRes.IssuesAndRisks%> </strong>
            <hr class="no-top-margin" />
            </div>
</div>
<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label">  <%= Resources.DeffinityRes.Category%></label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtIssuetype" runat="server" Visible="False" ValidationGroup="IssueVal1"
                                                    SkinID="txt_80"></asp:TextBox><asp:DropDownList ID="ddlIssuestype" runat="server" DataSourceID="SqlDataSource10"
                                                        DataTextField="IssueTypeName" DataValueField="ID" ValidationGroup="IssueVal3"
                                                        SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                <asp:LinkButton runat="server" ID="BtnDelIssuetype" OnClientClick="return confirm('Do you want to delete the record?');"
                                                    ValidationGroup="IssueVal2" SkinID="BtnLinkDelete" OnClick="BtnDelIssuetype_Click" />
					</div>
				</div>
                </div>
<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> </label>
                                      <div class="col-sm-9 form-inline">
                                           <asp:SqlDataSource ID="SqlDataSource10" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                    SelectCommand="Deffinity_IssuetypeSelect" SelectCommandType="StoredProcedure">
                                                </asp:SqlDataSource>
                                                <asp:Button ID="btnaddIssuetype" runat="server" SkinID="btnAdd" OnClick="btnaddIssuetype_Click" />
                                                <asp:Button ID="btneditIssuetype" runat="server" ValidationGroup="IssueVal2"
                                                    SkinID="btnEdit" OnClick="btneditpIssuetype_Click" />
                                                <asp:Button ID="btnsubmitIssuetype" runat="server" Visible="false" ValidationGroup="IssueVal1"
                                                    SkinID="btnSubmit" OnClick="btnsubmitpIssuetype_Click" />
                                                <asp:Button ID="btncancelIssuetype" runat="server" SkinID="btnCancel" OnClick="btncancelIssuetype_Click" />
					</div>
				</div>
                </div>
 <div class="form-group">
             <div class="col-md-12">
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="ddlIssuestype"
                                                    ErrorMessage="<%$ Resources:DeffinityRes, PleaseSelect%>" InitialValue="0" ValidationGroup="IssueVal2"><%= Resources.DeffinityRes.PleaseselectIssuetype%></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtIssuetype"
                                                        ErrorMessage="<%$ Resources:DeffinityRes,PleaseEnterCategory%>" ValidationGroup="IssueVal1"> <%= Resources.DeffinityRes.PleaseenterIssuetype%></asp:RequiredFieldValidator>              
				</div>
                </div>                                    


                                    </div>
                                  <div class="col-md-6">
                                      

                                      


                                    </div>
                                 </div>

        </asp:Panel>

     <asp:Panel ID="pnlAssets" runat="server" Visible="false">
                       <div class="row">
                                <div class="col-md-6">
                                    </div>
                            <div class="col-md-6">
                                <asp:HyperLink style="float:right;" ID="linkBackProduct" runat="Server" class="" Text="Back to Product" NavigateUrl="~/WF/Assets/AssetsAdmin.aspx"><i class="fa fa-arrow-left"></i> Back to Product</asp:HyperLink>
                                    </div>
                           </div> 
                  
    <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>  Product type</strong>
            <hr class="no-top-margin" />
            </div>
</div>
<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"><asp:Label ID="Label2" runat="server" Text="Product type"></asp:Label></label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtAssetType" runat="server" Visible="False" ValidationGroup="atg2"
                                                   SkinID="txt_80"></asp:TextBox><asp:DropDownList ID="ddlAssetType" runat="server" DataSourceID="SqlDataSource3"
                                                        DataTextField="Type" DataValueField="TypeID" ValidationGroup="atg1" SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                    SelectCommand="DEFFINITY_ADMINAssetsType" SelectCommandType="StoredProcedure">
                                                </asp:SqlDataSource>
                                           <asp:LinkButton runat="server" ID="ImgDeleteAssetType" SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete the record?');"
                                                    ValidationGroup="atg1" OnClick="ImgDeleteAssetType_Click" />
					</div>
				</div>
                </div>
 <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> </label>
                                      <div class="col-sm-9 form-inline"><asp:Button ID="ImdAddAssetType" runat="server" OnClick="ImdAddAssetType_Click"
                                                    SkinID="btnAdd" />
                                                <asp:Button ID="ImgEditAssetType" runat="server" ValidationGroup="atg1" OnClick="ImgEditAssetType_Click"
                                                    SkinID="btnEdit" />
                                                <asp:Button ID="ImgSubmitAssetType" runat="server" Visible="false" ValidationGroup="atg2"
                                                    OnClick="ImgSubmitAssetType_Click" SkinID="btnSubmit" />
                                                <asp:Button ID="ImgCancelAssetType" runat="server" OnClick="ImgCancelAssetType_Click"
                                                    SkinID="btnCancel" />
					</div>
				</div>
                </div>
 <div class="form-group">
             <div class="col-md-12">
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAssetType"
                                                    ErrorMessage="<%$ Resources:DeffinityRes, PleaseSelect%>" InitialValue="0" ValidationGroup="atg1"><%= Resources.DeffinityRes.PleaseselectAssetType%></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAssetType"
                                                        ErrorMessage="<%$ Resources:DeffinityRes, PleaseEnterAssetType%>" ValidationGroup="atg2"><%= Resources.DeffinityRes.PleaseEnterAssetType%></asp:RequiredFieldValidator>
				</div>
                </div>
               

                                    </div>
                                  <div class="col-md-6">
                                      <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>  Make</strong>
            <hr class="no-top-margin" />
            </div>
</div>
 
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"><asp:Label ID="Label3" runat="server" Text="Make"></asp:Label></label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtAssetMake" runat="server" Visible="False" ValidationGroup="amag2"
                                                    SkinID="txt_80"></asp:TextBox><asp:DropDownList ID="ddlAssetMake" runat="server" DataSourceID="SqlDataSource4"
                                                        DataTextField="Make" DataValueField="MakeID" ValidationGroup="amag1" SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                    SelectCommand="DEFFINITY_ADMINAssetsMake" SelectCommandType="StoredProcedure">
                                                </asp:SqlDataSource>
                                          <asp:LinkButton runat="server" ID="ImgDeleteAssetMake" SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete the record?');"
                                                    ValidationGroup="amag1" OnClick="ImgDeleteAssetMake_Click" />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:Button ID="ImgAddAssetMake" runat="server" SkinID="btnAdd" OnClick="ImgAddAssetMake_Click" />
                                                <asp:Button ID="ImgEditAssetMake" runat="server" SkinID="btnEdit" ValidationGroup="amag1"
                                                    OnClick="ImgEditAssetMake_Click" />
                                                <asp:Button ID="ImgSubmitAssetMake" runat="server" SkinID="btnSubmit" Visible="false"
                                                    ValidationGroup="amag2" OnClick="ImgSubmitAssetMake_Click" />
                                                <asp:Button ID="ImgCancelAssetMake" runat="server" SkinID="btnCancel" OnClick="ImgCancelAssetMake_Click" />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlAssetMake"
                                                    ErrorMessage="<%$ Resources:DeffinityRes, PleaseSelect%>" InitialValue="0" ValidationGroup="amag1"><%= Resources.DeffinityRes.PleaseselectAssetMake%></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAssetMake"
                                                        ErrorMessage="<%$ Resources:DeffinityRes, PleaseEnterAssetMake%>" ValidationGroup="amag2"><%= Resources.DeffinityRes.PleaseEnterAssetMake%></asp:RequiredFieldValidator>        
				</div>
                </div>                           

                                    </div>
                                 </div>
    <div class="row">
                                <div class="col-md-6">
                                    
<div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   Model </strong>
            <hr class="no-top-margin" />
            </div>
</div>
       <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <asp:Label ID="Label4" runat="server" Text="<%$ Resources:DeffinityRes, AssetModel%>"></asp:Label></label>
                                      <div class="col-sm-9 form-inline">
                                           <asp:TextBox ID="txtAssetModel" runat="server" ValidationGroup="amg2" Visible="False"
                                                    SkinID="txt_80"></asp:TextBox><asp:DropDownList ID="ddlAssetModel" runat="server" DataSourceID="SqlDataSource5"
                                                        DataTextField="Model" DataValueField="ModelID" ValidationGroup="amg1" SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                    SelectCommand="DEFFINITY_ADMINAssetsModel" SelectCommandType="StoredProcedure">
                                                </asp:SqlDataSource>
                                          <asp:LinkButton ID="ImgDeleteAssetModel" runat="server" OnClick="ImgDeleteAssetModel_Click"
                                                    OnClientClick="return confirm('Do you want to delete the record?');" SkinID="BtnLinkDelete"
                                                    ValidationGroup="amg1" />
					</div>
				</div>
                </div>
                                    <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> </label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:Button ID="ImgAddAssetModel" runat="server" OnClick="ImgAddAssetModel_Click"
                                                    SkinID="btnAdd" />
                                                <asp:Button ID="ImgEditAssetModel" runat="server" OnClick="ImgEditAssetModel_Click"
                                                    SkinID="btnEdit" ValidationGroup="amg1" />
                                                <asp:Button ID="ImgSubmitAssetModel" runat="server" OnClick="ImgSubmitAssetModel_Click"
                                                    SkinID="btnSubmit" ValidationGroup="amg2" Visible="false" />
                                                <asp:Button ID="ImgCancelAssetModel" runat="server" OnClick="ImgCancelAssetModel_Click"
                                                    SkinID="btnCancel" />
					</div>
				</div>
                </div>
                                    <div class="form-group">
             <div class="col-md-12">
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlAssetModel"
                                                    ErrorMessage="<%$ Resources:DeffinityRes, PleaseSelect%>" InitialValue="0" ValidationGroup="amg1"><%= Resources.DeffinityRes.PleaseselectAssetModel%></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtAssetModel"
                                                        ErrorMessage="<%$ Resources:DeffinityRes, PleaseEnterAssetModel%>" ValidationGroup="amg2"><%= Resources.DeffinityRes.PleaseEnterAssetModel%></asp:RequiredFieldValidator>                   
				</div>
                </div>                             

                                    </div>
        <div class="col-md-6">
                                      <div class="form-group">
                                          <div class="col-md-12 text-bold">
                                              <strong>  Status</strong>
                                              <hr class="no-top-margin" />
                                          </div>
                                      </div>
                                      <div class="form-group">
                                          <div class="col-md-12">
                                               <label class="col-sm-3 control-label">Status Name</label>
                                                <div class="col-sm-9 form-inline">
                                                       <asp:TextBox ID="txtStatusName" runat="server" ValidationGroup="amg2" Visible="False"
                                                                                                                      SkinID="txt_80"></asp:TextBox>
                                          <asp:DropDownList ID="DdlStatusName"
                                               runat="server" ValidationGroup="amg1" SkinID="ddl_80"></asp:DropDownList>
                                                     <asp:LinkButton ID="LnlStatusDelete" runat="server"
                                                          OnClientClick="return confirm('Do you want to delete the record?');"
                                                         SkinID="BtnLinkDelete" ValidationGroup="AssetStatus1" OnClick="LnlStatusDelete_Click"></asp:LinkButton>

                                                </div> 
                                          </div>
                                      </div>
                                      <div class="form-group">
                                          <div class="col-md-12">
                                               <label class="col-sm-3 control-label"> </label>
                                              <div class="col-sm-9 form-inline">
                                                <asp:Label ID="lblType" runat="server" Visible="false"></asp:Label>
                                                <asp:Button ID="BtnaddStatus" runat="server" SkinID="btnAdd" OnClick="BtnaddStatus_Click" />
                                                <asp:Button ID="BtnEditStatus" runat="server" SkinID="btnEdit" ValidationGroup="AssetStatus1" OnClick="BtnEditStatus_Click"/>
                                                <asp:Button ID="BtnSubmitStatus" runat="server" SkinID="btnSubmit" ValidationGroup="AssetStatus2"
                                                    Visible="false" OnClick="BtnSubmitStatus_Click"/>
                                                <asp:Button ID="BtnStatusCancel" runat="server" SkinID="btnCancel" OnClick="BtnStatusCancel_Click" />
                                              </div>
                                            </div>
                                      </div>
                                      <div class="form-group">
                                           <div class="col-md-12">
                                               <asp:Label ID="lblMsgInStatus" runat="server" EnableViewState="false"></asp:Label>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="DdlStatusName"
                                                    ErrorMessage="Please select status" InitialValue="0" ValidationGroup="AssetStatus1"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtStatusName"
                                                    ErrorMessage="Please enter status name" ValidationGroup="AssetStatus2"></asp:RequiredFieldValidator>
                                           </div>
                                      </div>



                                    </div>
                                  
                                 </div>
    <div class="row">
        <div class="col-md-6" style="display:none;visibility:hidden;">
                                      
<div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>  Asset Equipment Type</strong>
            <hr class="no-top-margin" />
            </div>
</div>

<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <asp:Label ID="lblequipment" runat="server" Text="Asset Equipment Type"></asp:Label></label>
                                      <div class="col-sm-9 form-inline"><asp:TextBox ID="txtEquipment" runat="server" Visible="False" ValidationGroup="amag2"
                                                    SkinID="txt_80"></asp:TextBox><asp:DropDownList ID="ddlEquipment" runat="server" DataSourceID="SqlDataSourceEQ"
                                                        DataTextField="EquipmentType" DataValueField="ID" ValidationGroup="aEQ1" SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSourceEQ" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                    SelectCommand="DEFFINITY_ADMINAssetsEquipmentType" SelectCommandType="StoredProcedure">
                                                </asp:SqlDataSource>
                                          <asp:LinkButton runat="server" ID="ImgEquipment_delete" SkinID="BtnLinkDelete" OnClientClick="return confirm('Do you want to delete the record?');"
                                                    ValidationGroup="aEQ1" OnClick="ImgEquipment_delete_Click" />
					</div>
				</div>
                </div>
<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> </label>
                                      <div class="col-sm-9 form-inline"><asp:Button ID="ImgAddEquipment" runat="server" SkinID="btnAdd" OnClick="ImgAddEquipment_Click" />
                                                <asp:Button ID="ImgEditEquipment" runat="server" SkinID="btnEdit" ValidationGroup="aEQ1"
                                                    OnClick="ImgEditEquipment_Click" />
                                                <asp:Button ID="ImgSubmitEquipment" runat="server" SkinID="btnSubmit" Visible="false"
                                                    ValidationGroup="aEQ2" OnClick="ImgSubmitEquipment_Click" />
                                                <asp:Button ID="ImgCancelEquipment" runat="server" SkinID="btnCancel" OnClick="ImgCancelEquipment_Click" />
					</div>
				</div>
                </div>
<div class="form-group">
             <div class="col-md-12">
                       <asp:RequiredFieldValidator ID="RequiredFieldValidatorEQ" runat="server" ControlToValidate="ddlEquipment"
                                                    ErrorMessage="Please select Asset Equipment Type" InitialValue="0" ValidationGroup="aEQ1"></asp:RequiredFieldValidator>
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorEQ2" runat="server" ControlToValidate="txtEquipment"
                                                    ErrorMessage="Please enter Equipment Type" ValidationGroup="aEQ2"></asp:RequiredFieldValidator>                
				</div>
                </div>
                 

                                    </div>
  <div class="col-md-6" style="display:none;visibility:hidden;">
                                   
                                    
<div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>  Speaker Bus Type</strong>
            <hr class="no-top-margin" />
            </div>
</div>
<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> <asp:Label ID="lblspeaker" runat="server" Text="Speaker Bus Type"></asp:Label></label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:TextBox ID="txtSpkrBusType" runat="server" ValidationGroup="amg2" Visible="False"
                                                    SkinID="txt_80"></asp:TextBox>
                                          <asp:DropDownList ID="ddlSpkrBusType" runat="server"
                                                        DataSourceID="SqlDataSourceSpkr" DataTextField="SpeakerBusType" DataValueField="SpeakerBusID"
                                                        ValidationGroup="amg1" SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSourceSpkr" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                    SelectCommand="DEFFINITY_ADMIN_DV_SpeakerBusType" SelectCommandType="StoredProcedure">
                                                </asp:SqlDataSource>
                                          <asp:LinkButton ID="ImgSpkrDelete" runat="server" OnClientClick="return confirm('Do you want to delete the record?');"
                                                    SkinID="BtnLinkDelete" ValidationGroup="Spkr1" OnClick="ImgSpkrDelete_Click" />
					</div>
				</div>
                </div>
  <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> </label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:Button ID="ImgSpkrAdd" runat="server" SkinID="btnAdd" OnClick="ImgSpkrAdd_Click" />
                                                <asp:Button ID="ImgSpkrEdit" runat="server" SkinID="btnEdit" ValidationGroup="Spkr1"
                                                    OnClick="ImgSpkrEdit_Click" />
                                                <asp:Button ID="ImgSpkrSubmit" runat="server" SkinID="btnSubmit" ValidationGroup="Spkr2"
                                                    Visible="false" OnClick="ImgSpkrSubmit_Click" />
                                                <asp:Button ID="ImgSpkrCancel" runat="server" SkinID="btnCancel" OnClick="ImgSpkrCancel_Click" />
					</div>
				</div>
                </div>
 <div class="form-group">
             <div class="col-md-12">
                                     
                                          <asp:RequiredFieldValidator ID="RFVSpkr1" runat="server" ControlToValidate="ddlSpkrBusType"
                                                    ErrorMessage="Please select Speaker Bus Type" InitialValue="0" ValidationGroup="Spkr1"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RFVSpkr2" runat="server" ControlToValidate="txtSpkrBusType"
                                                    ErrorMessage="Please enter Speaker Bus Type" ValidationGroup="Spkr2"></asp:RequiredFieldValidator>
					
				</div>
                </div>                                    

                                    </div>
                                  
                                 </div>
           </asp:Panel>

     <asp:Panel ID="pnlVendors" runat="server" Visible="false">
                 
    <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   Vendor Attributes </strong>
            <hr class="no-top-margin" />
            </div>
</div>

<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label">Attributes</label>
                                      <div class="col-sm-10 form-inline">
                                          <asp:TextBox ID="txtVendorAttributes" runat="server" Visible="False" ValidationGroup="101"
                                                    SkinID="txt_80"></asp:TextBox><asp:DropDownList ID="ddlVendorAttributes" runat="server"
                                                        DataSourceID="sql101" DataTextField="AttributeName" DataValueField="ID" ValidationGroup="102"
                                                        SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                <asp:SqlDataSource ID="sql101" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                    SelectCommand="Deffinity_TBLATTRIBUTES_FILL" SelectCommandType="StoredProcedure">
                                                </asp:SqlDataSource>
                                          <asp:LinkButton runat="server" ID="btndeleteVendorAttributes" OnClientClick="return confirm('Do you want to delete the attribute?');"
                                                    ValidationGroup="102" SkinID="BtnLinkDelete" OnClick="btndeleteVendorAttributes_Click" />
					</div>
				</div>
                </div>
<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> </label>
                                      <div class="col-sm-10 form-inline"> <asp:Button ID="btnaddVendorAttributes" runat="server" SkinID="btnAdd" OnClick="btnaddVendorAttributes_Click" />
                                                <asp:Button ID="btneditVendorAttributes" runat="server" ValidationGroup="102"
                                                    SkinID="btnEdit" OnClick="btneditVendorAttributes_Click" />
                                                <asp:Button ID="btnsubmitVendorAttributes" runat="server" Visible="false" ValidationGroup="101"
                                                    SkinID="btnSubmit" OnClick="btnsubmitVendorAttributes_Click" />
                                                <asp:Button ID="btncancelVendorAttributes" runat="server" SkinID="btnCancel"
                                                    OnClick="btncancelVendorAttributes_Click" />
					</div>
				</div>
                </div>
<div class="form-group">
             <div class="col-md-12">
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator102" runat="server" ControlToValidate="ddlVendorAttributes"
                                                    ErrorMessage="Please Select Attributes" InitialValue="0" ValidationGroup="102"></asp:RequiredFieldValidator>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator101" runat="server" ControlToValidate="txtVendorAttributes"
                                                    ErrorMessage="Please Enter Attributes" ValidationGroup="101"> Please Enter Attributes</asp:RequiredFieldValidator>
                 <br />
                 <a href="/WF/Vendors/RFIVendors.aspx" target="_self">Return to Vendor Management</a>
				</div>
                </div>

                           

                                    </div>
                                  <div class="col-md-6">
                                    </div>
                                 </div>
          
                    </asp:Panel>

    <asp:Panel ID="pnlBBBEE" runat="server" Visible="false">
                    
    <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   BBBEE Rating </strong>
            <hr class="no-top-margin" />
            </div>
</div>
                                    <asp:GridView ID="gvBBBEERating" AllowPaging="True" AllowSorting="True" BackColor="White"
                                        Font-Size="X-Small" AutoGenerateColumns="False" Font-Names="Verdana" runat="server"
                                        OnPageIndexChanging="gvBBBEERating_PageIndexChanging" OnRowEditing="gvBBBEERating_RowEditing"
                                        GridLines="None" OnRowCancelingEdit="gvBBBEERating_CancelingEdit" OnRowDataBound="gvBBBEERating_RowDataBound"
                                        OnRowCommand="gvBBBEERating_RowCommand" OnRowDeleting="gvBBBEERating_RowDeleting"
                                        OnRowUpdating="gvBBBEERating_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" ">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditGV1" runat="server" CausesValidation="false" CommandName="Edit"
                                                        SkinID="BtnLinkEdit" ToolTip="Edit" CommandArgument='<%# Bind("ID") %> ' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="btnUpdateGV1" runat="server" CommandName="Update" SkinID="BtnLinkUpdate"
                                                        CommandArgument='<%# Bind("ID") %> ' ToolTip="Update" ValidationGroup="grpScore" />
                                                    <asp:LinkButton ID="btncancelGV1" runat="server" CausesValidation="false" CommandName="cancel"
                                                        SkinID="BtnLinkCancel" ToolTip="Cancel" />
                                                </EditItemTemplate>
                                                <ItemStyle CssClass="form-inline" />
                                                <ControlStyle CssClass="form-inline" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Minimum Points">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMinPoints" runat="server" Text='<%#Eval("MinimumPoints") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMinPoints" runat="server" Text='<%#Eval("MinimumPoints") %>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqMinPoints" runat="server" ControlToValidate="txtMinPoints"
                                                        Display="None" ErrorMessage="Please enter minimum points" ValidationGroup="grpScore"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtMinPoints1" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqMinPoints1" runat="server" ControlToValidate="txtMinPoints1"
                                                        Display="None" ErrorMessage="Please enter minimum points" ValidationGroup="grpScore1"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Maximum Points">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMaxPoints" runat="server" Text='<%#Eval("MaximumPoints") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMaxPoints" runat="server" Text='<%#Eval("MaximumPoints") %>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqMaxPoints" runat="server" ControlToValidate="txtMaxPoints"
                                                        Display="None" ErrorMessage="Please enter maximum points" ValidationGroup="grpScore"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtMaxPoints1" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqMaxPoints1" runat="server" ControlToValidate="txtMaxPoints1"
                                                        Display="None" ErrorMessage="Please enter maximum points" ValidationGroup="grpScore1"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BEE Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBEEStatus" runat="server" Text='<%#Eval("BEEStatus") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtBEEStatus" runat="server" Text='<%#Eval("BEEStatus") %>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqBEEStatus" runat="server" ControlToValidate="txtBEEStatus"
                                                        Display="None" ErrorMessage="Please enter status" ValidationGroup="grpScore"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtBEEStatus1" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqBEEStatus1" runat="server" ControlToValidate="txtBEEStatus1"
                                                        Display="None" ErrorMessage="Please enter status" ValidationGroup="grpScore1"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Recognition Level">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRecogLevel" runat="server" Text='<%# Eval("RecognitionLevel","{0:N2}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtRecogLevel" runat="server" Text='<%# Eval("RecognitionLevel","{0:N2}") %>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqRecogLevel" runat="server" ControlToValidate="txtRecogLevel"
                                                        Display="None" ErrorMessage="Please enter Recognition Level points" ValidationGroup="grpScore"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtRecogLevel1" runat="server" Text='<%#Eval("RecognitionLevel","{0:N2}") %>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqRecogLevel1" runat="server" ControlToValidate="txtRecogLevel1"
                                                        Display="None" ErrorMessage="Please enter Recognition Level points" ValidationGroup="grpScore1"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelGV1" runat="server" CommandName="Delete" CommandArgument='<%# Bind("ID")%>'
                                                        SkinID="BtnLinkDelete" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton runat="server" ID="btnAddGV1" SkinID="BtnLinkAdd"
                                                        ValidationGroup="grpScore1" CommandName="Add" CommandArgument='<%# Bind("ID")%>' />
                                                    <asp:LinkButton runat="server" ID="btnCanGV1" SkinID="BtnLinkCancel"
                                                        CommandName="CancleEntry" CommandArgument='<%# Bind("ID")%>' />
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <%-- <a href="RFIMain.aspx" target="_self">Return to Tender Home Page</a>--%>
                                    </div>
                                  <div class="col-md-6">
                                    </div>
                                 </div>
    </asp:Panel>

     <asp:Panel ID="pnlCRM" runat="server" Visible="false">
                        
                 
 <div class="row">
                                <div class="col-md-6">
                                    
<div class="form-group">
        <div class="col-md-12 text-bold">
             <strong> Sector</strong>
            <hr class="no-top-margin" />
            </div>
</div>
<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> Sector</label>
                                      <div class="col-sm-9">
                                          <asp:TextBox ID="txtSectors" runat="server" Visible="False" ValidationGroup="St101"
                                                                SkinID="txt_80"></asp:TextBox><asp:DropDownList ID="ddlSectors" runat="server" DataSourceID="SqlDataSource2"
                                                                    DataTextField="Name" DataValueField="ID" ValidationGroup="St102" SkinID="ddl_80">
                                                                </asp:DropDownList>
                                                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                                SelectCommand="Sectors_FILL" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                          <asp:LinkButton runat="server" ID="btndeleteSectors" OnClientClick="return confirm('Do you want to delete the sector?');"
                                                                ValidationGroup="St102" SkinID="BtnLinkDelete" OnClick="btndeleteSectors_Click" />
					</div>
				</div>
                </div>
 <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> </label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:Button  ID="btnaddSectors" runat="server" SkinID="btnAdd" OnClick="btnaddSectors_Click" />
                                                            <asp:Button ID="btneditSectors" runat="server" ValidationGroup="St102" SkinID="btnEdit"
                                                                OnClick="btneditSectors_Click" />
                                                            <asp:Button ID="btnsubmitSectors" runat="server" Visible="false" ValidationGroup="St101"
                                                                SkinID="btnSubmit" OnClick="btnsubmitSectors_Click" />
                                                            <asp:Button ID="btncancelSectors" runat="server" SkinID="btnCancel" OnClick="btncancelSectors_Click" />
					</div>
				</div>
                </div>
<div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> </label>
                                      <div class="col-sm-9">
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSectors"
                                                                ErrorMessage="Please Select Sectors" InitialValue="0" ValidationGroup="St102"></asp:RequiredFieldValidator>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtSectors"
                                                                ErrorMessage="Please Enter Sectors" ValidationGroup="St101"> Please Enter Attributes</asp:RequiredFieldValidator>
					</div>
				</div>
                </div>                                    

                                    </div>
                                  <div class="col-md-6">
                                      
<div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>   Revenue Type</strong>
            <hr class="no-top-margin" />
            </div>
</div>

                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label">Revenue Type</label>
                                      <div class="col-sm-9 form-inline"> <asp:TextBox ID="txtRevenueType" runat="server" Visible="False" ValidationGroup="RT201"
                                                                SkinID="txt_80"></asp:TextBox><asp:DropDownList ID="ddlRevenueType" runat="server"
                                                                    DataSourceID="sql201" DataTextField="Name" DataValueField="ID" ValidationGroup="RT202"
                                                                    SkinID="ddl_80">
                                                                </asp:DropDownList>
                                                            <asp:SqlDataSource ID="sql201" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                                SelectCommand="RevenueType_FILL" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                          <asp:LinkButton runat="server" ID="btndeleteRevenueType" OnClientClick="return confirm('Do you want to delete the RevenueType?');"
                                                                ValidationGroup="RT202" SkinID="BtnLinkDelete" OnClick="btndeleteRevenueType_Click" />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:Button ID="btnaddRevenueType" runat="server" SkinID="btnAdd" OnClick="btnaddRevenueType_Click" />
                                                            <asp:Button ID="btneditRevenueType" runat="server" ValidationGroup="RT202" SkinID="btnEdit"
                                                                OnClick="btneditRevenueType_Click" />
                                                            <asp:Button ID="btnsubmitRevenueType" runat="server" Visible="false" ValidationGroup="RT101"
                                                                SkinID="btnSubmit" OnClick="btnsubmitRevenueType_Click" />
                                                            <asp:Button ID="btncancelRevenueType" runat="server" SkinID="btnCancel" OnClick="btncancelRevenueType_Click" />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"> </label>
                                      <div class="col-sm-9">
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlRevenueType"
                                                                ErrorMessage="Please Select RevenueType" InitialValue="0" ValidationGroup="RT202"></asp:RequiredFieldValidator>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtRevenueType"
                                                                ErrorMessage="Please Enter RevenueType" ValidationGroup="RT201"> Please Enter Attributes</asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
                                                                        

                                    </div>
                                 </div>
       </asp:Panel>
    <asp:Panel ID="pnlInventory" runat="server">
          <div class="form-group">
             <div class="col-md-6">
                  <uc2:UseDropdown ID="UseDropdown1" runat="server" />
                 </div>
              </div>
                       
                       
                    </asp:Panel>
                    <div>
                        <asp:Button ID="btnBack" runat="server" SkinID="ImgBack" CausesValidation="False"
                            OnClick="btnBack_Click" Visible="False" />
                    </div>
    
   <asp:Panel ID="pnlSD" runat="server" Visible="false">
       <div class="form-group">
        <div class="col-md-12 text-bold">
             <strong>  <%= Resources.DeffinityRes.ServiceRequestSection%> </strong>
            <hr class="no-top-margin" />
            </div>
</div>
       <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label">  <%= Resources.DeffinityRes.Priority%></label>
                                      <div class="col-sm-9">
                                          <asp:TextBox ID="txtpriority" runat="server" Visible="False" ValidationGroup="IssueVal1"
                                                    SkinID="txt_80"></asp:TextBox><asp:DropDownList ID="ddlpriority" runat="server" DataSourceID="SqlDataSource13"
                                                        DataTextField="Prioritylevel" DataValueField="ID" ValidationGroup="ProVal3" SkinID="ddl_80">
                                                    </asp:DropDownList>
                                                <asp:LinkButton runat="server" ID="BtnDelPriority" OnClientClick="return confirm('Do you want to delete the record?');"
                                                    ValidationGroup="Prioritylevel2" SkinID="BtnLinkDelete" OnClick="BtnDelPriority_Click" />
					</div>
				</div>
                </div>
       <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-9 form-inline">
                                          <asp:SqlDataSource ID="SqlDataSource13" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                    SelectCommand="DN_GetIncidentpriority" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                <asp:Button ID="btnaddpriority" runat="server" SkinID="btnAdd" ValidationGroup="Prioritylevel1"
                                                    OnClick="btnaddpriority_Click" />
                                                <asp:Button ID="btneditpriority" runat="server" ValidationGroup="Prioritylevel2"
                                                    SkinID="btnEdit" OnClick="btneditpriority_Click" />
                                                <asp:Button ID="btnsubmitpriority" runat="server" Visible="false" ValidationGroup="Prioritylevel1"
                                                    SkinID="btnSubmit" OnClick="btnsubmitpriority_Click" />
                                                <asp:Button ID="tncancelpriority" runat="server" SkinID="btnCancel" OnClick="tncancelpriority_Click" />
					</div>
				</div>
                </div>
       <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-3 control-label"></label>
                                      <div class="col-sm-9">
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlpriority"
                                                    ErrorMessage="<%$ Resources:DeffinityRes, PleaseSelect%>" InitialValue="0" ValidationGroup="Prioritylevel2"><%= Resources.DeffinityRes.PleaseselectPrioritylevel%></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtpriority"
                                                        ErrorMessage="<%$ Resources:DeffinityRes,PleaseenterPrioritylevel%>" ValidationGroup="Prioritylevel1"> <%= Resources.DeffinityRes.PleaseenterPrioritylevel%></asp:RequiredFieldValidator>
					</div>
				</div>
                </div>
                       
                    </asp:Panel>
     
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
 <script type="text/javascript">
     //grid_responsive();
     grid_responsive_display();
     $(window).load(function () {
         $("button:contains('Display all')").click(function (e) {
             e.preventDefault();
             $(".dropdown-menu li")
       .find("input[type='checkbox']")
       .prop('checked', 'checked').trigger('change');
         });
     });
    </script> 

</asp:Content>
