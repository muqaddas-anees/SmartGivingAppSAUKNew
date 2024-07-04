<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTabSub.master" AutoEventWireup="true" Inherits="FLSCustomFormDesigner" EnableEventValidation="false" Codebehind="FLSCustomFormDesigner.aspx.cs" %>


<%@ Register Src="~/WF/CustomerAdmin/controls/PortfolioMenuTab.ascx" TagName="PortfolioMenuTab" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
   
  <%--  <uc1:PortfolioMenuTab ID="PortfolioMenuTab1" runat="server" />--%>
   <%-- <script type="text/javascript" src="../Scripts/jquery-1.9.0.js"></script>--%>
    <script type="text/javascript">
        //CheckboxList Single selection using jquery
        $(document).ready(function () {
            var checkboxes = $('#<%=chkListPostion.ClientID %>').find('input:checkbox');
            checkboxes.click(function () {
                var selectedIndex = checkboxes.index($(this));

                var items = $('#<% = chkListPostion.ClientID %> input:checkbox');
                for (i = 0; i < items.length; i++) {
                    if (i == selectedIndex)
                        items[i].checked = true;
                    else
                        items[i].checked = false;
                }
            });
        });
    </script>
     <style>
           .mycheckBig input {width:18px; height:18px;margin-left:10px}
           .mycheckBig label {padding-left:8px}
       </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
   Settings
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     Custom Fields
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
      <a class="btn btn-video" id="btn_video" runat="server" style="background-color:#50CD89;color:white;"  data-class="d-block" data-fslightbox="lightbox-vimeo" href="#vimeo">
   <i class="bi bi-camera-video-fill btn-weight fs-4 me-2 btn-weight"></i> Video Tutorial</a>
                  <iframe id="vimeo" style="display:none" src="https://player.vimeo.com/video/833744044?h=de60579cca" width="1920px" height="1080px" frameBorder="0" allow="autoplay; fullscreen" allowFullScreen></iframe>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   <style>
       .mychk input{
           height:25px;
       }
   </style>
     <div class="form-group row">
          <div class="col-md-6">
               <asp:Label ID="lblMsg1" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         <asp:Label ID="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
     <asp:ValidationSummary ID="vcat_e" runat="server" ValidationGroup="cat_e" />
               <asp:ValidationSummary ID="vcat_f" runat="server" ValidationGroup="cat_f" />
            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlForms"
                        Display="None" ErrorMessage="Please select form" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="cat_e"></asp:RequiredFieldValidator>--%>
              </div>
         </div>
    <%-- <div class="form-group row">
          <div class="col-md-6">
           <label class="col-sm-3 control-label">Forms</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlForms" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlForms_SelectedIndexChanged"></asp:DropDownList>
               <asp:TextBox ID="txtForms" runat="server" Visible="false"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6 form-inline">
           <asp:HiddenField ID="hcid" runat="server" Value="0" />
               <asp:LinkButton ID="btnEditForm" runat="server" SkinID="BtnLinkEdit" ValidationGroup="cat_e" OnClick="btnEditForm_Click" />
               <asp:LinkButton ID="btnAddForm" runat="server" SkinID="BtnLinkAdd" OnClick="btnAddForm_Click" />
               <asp:LinkButton ID="btnDelForm" runat="server" SkinID="BtnLinkDelete"   ValidationGroup="cat_e" OnClientClick="return confirm('Do you want to delete the record?');" OnClick="btnDelForm_Click" />
               <asp:Button ID="btnSubmitForm" runat="server" SkinID="btnSubmit" Visible="false" OnClick="btnSubmitForm_Click"  />
               <asp:Button ID="btnCancelForm" runat="server" SkinID="btnCancel" Visible="false" OnClick="btnCancelForm_Click"  />
	</div>
         </div>
      <div class="form-group row">
          <div class="col-md-6">
           <label class="col-sm-3 control-label">Associate Type of Request</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlTypeofRequest" runat="server"></asp:DropDownList>
               
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTypeofRequest"
                        Display="None" ErrorMessage="Please select type of request" InitialValue="0" SetFocusOnError="True"
                        ValidationGroup="cat_f"></asp:RequiredFieldValidator>
            </div>
	</div>
          <div class="col-md-6">
              <asp:Button ID="btnSave" runat="server" SkinID="btnSave" ValidationGroup="cat_f" OnClick="btnSave_Click"/>
              </div>
          </div>--%>
    <asp:Panel ID="pnlFormDesign" runat="server" >
    <div class="form-group row">
        <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="form" DisplayMode="BulletList" />
        </div>
    <div class="form-group row" style="display:none;visibility:hidden;">
                                  <div class="col-md-7">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Customer%> </label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlCustomer" runat="server" Width="250px" ClientIDMode="Static"
                                OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                           <%-- <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlCustomer"
                                InitialValue="0" ErrorMessage="Please select customer" ValidationGroup="form"
                                SetFocusOnError="true">*</asp:RequiredFieldValidator>--%>
                            <ajaxToolkit:CascadingDropDown ID="ccdCustomer" runat="server" TargetControlID="ddlCustomer"
                                BehaviorID="ccdCom" Category="company" PromptText="Please select..." PromptValue="0"
                                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetCompany" LoadingText="[Loading customer...]" />
					</div>
				</div>
        </div>
        
 <%--   <div class="form-group row mb-6">
        <div class="col-md-12 text-bold">
        <strong>   Add / Update Field </strong>
            <hr class="no-top-margin" />
            </div>
    </div>--%>
    <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
    <div class="form-group row  mb-6">
                                  <div class="col-md-7">
                                       <label class="col-sm-3 control-label"> Select Type of Field</label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="ddlTypeOfField" runat="server" SkinID="ddl_90" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlTypeOfField_SelectedIndexChanged">
                                <asp:ListItem Value="Text Box" Text="Text Box"> </asp:ListItem>
                                <asp:ListItem Value="Text Area" Text="Text Area"> </asp:ListItem>
                                <%--  <asp:ListItem Value="Instruction" Text="Instruction"> </asp:ListItem>--%>
                                <asp:ListItem Value="Dropdown List" Text="Dropdown List"> </asp:ListItem>
                                <asp:ListItem Value="Radio Button" Text="Radio Button"> </asp:ListItem>
                                <asp:ListItem Value="Checkbox" Text="Checkbox"> </asp:ListItem>
                                <asp:ListItem Value="Date" Text="Date"> </asp:ListItem>
                                <asp:ListItem Value="Number Field" Text="Number Field"> </asp:ListItem>
                                <asp:ListItem Value="Url" Text="URL"> </asp:ListItem>
                            </asp:DropDownList>
					</div>
				</div>
        
</div>
     <div class="form-group row">
                                  <div class="col-md-7">
                                      <label class="col-sm-3 control-label"> <asp:Label ID="lblLableName" runat="server" Text="Label"></asp:Label></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtLabelName" runat="server" SkinID="txt_90"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLabel" runat="server" ControlToValidate="txtLabelName"
                                ErrorMessage="Please enter label" ValidationGroup="form" SetFocusOnError="true">*</asp:RequiredFieldValidator></div>
                                     </div>
          </div>
    <div class="form-group row">
          <div class="col-md-7">
              <label class="col-sm-3 control-label"><asp:Label ID="lblDefaultText" runat="server" Text="Default Value/Text"></asp:Label> </label>
              <div class="col-sm-8"> <asp:TextBox ID="txtDefaultText" runat="server" SkinID="txtMulti"></asp:TextBox></div>
                                     </div>
         </div>
    <div class="form-group row  mb-6">
                                  <div class="col-md-7">
                                       <label class="col-sm-3 control-label"> <asp:Label ID="lblMinimumValue" runat="server" Text="Minimum Value"></asp:Label></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtMinimumValue" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMin" runat="server" ControlToValidate="txtMinimumValue"
                                ErrorMessage="Please enter minimum value" Display="None" ValidationGroup="form"
                                SetFocusOnError="true">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvMin" runat="server" ControlToValidate="txtMinimumValue"
                                Display="None" ErrorMessage="Please enter valid mimimum value" Operator="DataTypeCheck"
                                Type="Double" SetFocusOnError="true" ValidationGroup="form">*</asp:CompareValidator>
					</div>
				</div>
         </div>
    <div class="form-group row  mb-6">
 <div class="col-md-7">
                                       <label class="col-sm-3 control-label"> <asp:Label ID="lblMaximumValue" runat="server" Text="Maximum Value"></asp:Label></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtMaximumValue" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMax" runat="server" ControlToValidate="txtMaximumValue"
                                ErrorMessage="Please enter maximum value" Display="None" ValidationGroup="form"
                                SetFocusOnError="true">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvMax" runat="server" ControlToValidate="txtMaximumValue"
                                Display="None" ErrorMessage="Please enter valid maximum value" Operator="DataTypeCheck"
                                Type="Double" SetFocusOnError="true" ValidationGroup="form">*</asp:CompareValidator>
					</div>
				</div>
</div>
    <div class="form-group row  mb-6">
                                  <div class="col-md-7">
                                       <div class="col-sm-6 control-label d-flex d-inline"> <asp:Label ID="lblMandatoryField" runat="server" Text="Mandatory field"></asp:Label>
                                           <asp:CheckBox ID="chkMandatoryField" runat="server" CssClass="mycheckBig" /></div>
                                    <%--  <div class="col-sm-8">
					</div>--%>
				</div>
         </div>
    <div class="form-group row  mb-6">
 <div class="col-md-7">
                                       <label class="col-sm-3 control-label"> <asp:Label ID="lblListValues" runat="server" Text="List Values"></asp:Label></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txtListValues" runat="server"></asp:TextBox>
					</div>
				</div>
</div>
    <div class="form-group row  mb-6">
                                  <div class="col-md-7">
                                       <label class="col-sm-3 control-label"> </label>
                                      <div class="col-sm-8"> <asp:HiddenField ID="hfId" runat="server" Value="0" />
                            <asp:Button ID="imgAdd" runat="server" SkinID="btnAdd" OnClick="imgAdd_Click"
                                ValidationGroup="form" />&nbsp;
                            <asp:Button ID="imgUpdate" runat="server" SkinID="btnUpdate" OnClick="imgUpdate_Click"
                                ValidationGroup="form" />&nbsp;
                            <asp:Button ID="imgCancel" runat="server" SkinID="btnCancel" OnClick="imgCancel_Click" />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> </label>
                                      <div class="col-sm-8">
					</div>
				</div>
</div>
     <asp:Panel ID="pnlPosition" runat="server" Visible="false">
                      
                                <asp:CheckBoxList ID="chkListPostion" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                    BorderStyle="Solid" BorderWidth="1px" CellSpacing="28">
                                    <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                    <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                    <asp:ListItem Text="F" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="G" Value="G"></asp:ListItem>
                                    <asp:ListItem Text="H" Value="H"></asp:ListItem>
                                    <asp:ListItem Text="I" Value="I"></asp:ListItem>
                                    <asp:ListItem Text="J" Value="J"></asp:ListItem>
                                    <asp:ListItem Text="K" Value="K"></asp:ListItem>
                                    <asp:ListItem Text="L" Value="L"></asp:ListItem>
                                    <asp:ListItem Text="M" Value="M"></asp:ListItem>
                                    <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="O" Value="O"></asp:ListItem>
                                    <asp:ListItem Text="P" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="Q" Value="Q"></asp:ListItem>
                                    <asp:ListItem Text="R" Value="R"></asp:ListItem>
                                    <asp:ListItem Text="S" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="T" Value="T"></asp:ListItem>
                                    <asp:ListItem Text="U" Value="U"></asp:ListItem>
                                    <asp:ListItem Text="V" Value="V"></asp:ListItem>
                                    <asp:ListItem Text="W" Value="W"></asp:ListItem>
                                    <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                </asp:CheckBoxList>
                          
                    </asp:Panel>

        <div class="row  mb-6">
    <asp:GridView ID="gvForm" runat="server" Width="100%" OnRowCommand="gvForm_RowCommand"
                        EmptyDataText="" OnRowDeleting="gvForm_RowDeleting" OnRowEditing="gvForm_RowEditing">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="20px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Linkedit" runat="server" CausesValidation="false" CommandName="Edit"
                                        CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes,Edit%>">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Label">
                                <ItemTemplate>
                                    <asp:Label ID="lblLabel" runat="server" Text='<% #Bind("LabelName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblTypeOfField" runat="server" Text='<% #Bind("TypeOfField") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Values">
                            <ItemStyle Width="400px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblValues" runat="server" Text='<% #Eval("ListValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Default Text">
                                <ItemTemplate>
                                    <asp:Label ID="lblDefaultText" runat="server" Text='<% #Eval("DefaultText") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Minimum Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblMinimumValue" runat="server" Text='<% #Eval("MinimumValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Maximum Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblMaximumValue" runat="server" Text='<% #Eval("MaximumValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mandatory">
                                <ItemTemplate>
                                    <asp:Label ID="lblMandatory" runat="server" Text='<% #Eval("Mandatory").ToString() == "True"?"Yes":"No" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Position">
                                <ItemTemplate>
                                    <asp:Label ID="lblPosition" runat="server" Text='<% #Bind("FieldPosition") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="15px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="imgDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                        SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                                </ItemTemplate>
                                <FooterStyle Width="45px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
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

