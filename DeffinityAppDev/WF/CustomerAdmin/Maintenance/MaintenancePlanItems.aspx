<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="MaintenancePlanItems.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Maintenance.MaintenancePlanItems" EnableEventValidation="false" %>

<%@ Register Src="~/WF/CustomerAdmin/Maintenance/Controls/MaintenancePlanTabCtrl.ascx" TagPrefix="Pref" TagName="MaintenancePlanTabCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    Maintenance Plan
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
    <Pref:MaintenancePlanTabCtrl runat="server" ID="MaintenancePlanTabCtrl" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    <asp:Label ID="lblTitile" runat="server" Text="Add Equipment"></asp:Label>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <asp:Button ID="btnSave" runat="server" SkinID="btnSave" OnClick="btnSave_Click" /> <asp:Button ID="btnBackEq" runat="server" SkinID="btnDefault" Text="Back" OnClick="btnBackEq_Click" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <style>
        .delli{
            padding-left:10px;
        }
        .btn {
    outline: none;
    border: 1px solid transparent;
     margin-bottom: 0px; 
}
.modal {
    background: rgba(0,0,0,0.5);
}

    </style>
     <div class="form-group row">
       <div class="col-md-12">
           <asp:Label ID="lblMsg" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
           <asp:Label ID="lblMsgError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
           </div>
         </div>

    <div class="well">

       <div class="form-group row">
          <div class="col-md-4">
 <label class="col-sm-3 control-label">Type of Equipment</label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlCategory" runat="server" ClientIDMode="Static"></asp:DropDownList>
                <asp:HiddenField ID="hCategory" runat="server" ClientIDMode="Static" Value="0"  />
            </div>
              
	</div>
             <div class="col-md-4">
 <label class="col-sm-3 control-label">Checklist</label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlSubCategory" runat="server" ClientIDMode="Static"></asp:DropDownList>
               <asp:HiddenField ID="hSubCategory" runat="server" ClientIDMode="Static" Value="0"  />
            </div>
              
	</div>
             <div class="col-md-4">
 <label class="col-sm-3 control-label">Start Month</label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlMonth" runat="server" ClientIDMode="Static">
                   <asp:ListItem Text="Please select..." Value="" ></asp:ListItem>
                   <asp:ListItem Text="January" Value="January" ></asp:ListItem>
                   <asp:ListItem Text="February" Value="February" ></asp:ListItem>
                   <asp:ListItem Text="March" Value="March" ></asp:ListItem>
                   <asp:ListItem Text="April" Value="April" ></asp:ListItem>
                   <asp:ListItem Text="May" Value="May" ></asp:ListItem>
                   <asp:ListItem Text="June" Value="June" ></asp:ListItem>
                   <asp:ListItem Text="July" Value="July" ></asp:ListItem>
                   <asp:ListItem Text="August" Value="August" ></asp:ListItem>
                   <asp:ListItem Text="September" Value="September" ></asp:ListItem>
                   <asp:ListItem Text="October" Value="October" ></asp:ListItem>
                   <asp:ListItem Text="November" Value="November" ></asp:ListItem>
                   <asp:ListItem Text="December" Value="December" ></asp:ListItem>
               </asp:DropDownList>
            </div>
              
	</div>
</div>


      <div class="form-group row">

              <div class="col-md-4">
 <label class="col-sm-3 control-label">Manufacturer</label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlManufacturer" runat="server" ClientIDMode="Static"></asp:DropDownList>
                <asp:HiddenField ID="hManufacturer" runat="server" ClientIDMode="Static" Value="0"  />
            </div>
              
	</div>

            <div class="col-md-4">
 <label class="col-sm-3 control-label">QTY</label>
           <div class="col-sm-8">
              <asp:TextBox ID="txtQTY" runat="server" SkinID="Price_150px" Text="1"></asp:TextBox>
            </div>
              
	</div>
          </div>

     <div class="form-group row">
            <div class="col-md-4">
 <label class="col-sm-3 control-label">Model Number</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtModelNumber" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
              
	</div>

          <div class="col-md-4">
 <label class="col-sm-3 control-label">Time Per Year</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtTimeperyear" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
              
	</div>

         </div>

      <div class="form-group row">
            <div class="col-md-4">
 <label class="col-sm-3 control-label">Serial Number</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtSerialNumber" runat="server" ClientIDMode="Static"></asp:TextBox>
            </div>
              
	</div>
          <div class="col-md-4">
              <div class="col-sm-8">
                    <asp:Button ID="Button1" runat="server" SkinID="btnSave" OnClick="btnSave_Click" />
              </div>
          </div>
          </div>
     
        <div class="row">
          <div class="col-md-12">
 <strong> Material</strong> 
<hr class="no-top-margin" />
	</div>
</div>
     <div class="form-group row">

         
              <div class="col-md-4">
 <label class="col-sm-3 control-label">Material</label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlMaterial" runat="server" ClientIDMode="Static"></asp:DropDownList>
            </div>
              
	</div>
              <div class="col-md-4 form-inline">
 
              <asp:Button ID="btnCreateNew" runat="server" SkinID="btnDefault" Text="Create New" OnClick="btnCreateNew_Click" ClientIDMode="Static" />
                  <asp:Button ID="btnAddMaterialtoPlan" runat="server" SkinID="btnDefault" Text="Add Material to Plan" OnClick="btnAddMaterialtoPlan_Click" ClientIDMode="Static" />
	</div>


         </div>

    <div class="form-group row">

        <asp:GridView ID="gridMaterials" runat="server" OnRowCommand="gridMaterials_RowCommand" Width="80%">
            <Columns>
                <asp:TemplateField ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:LinkButton ID="link_edit" runat="server" SkinID="BtnLinkEdit" CommandName="item_edit" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Material">
                <ItemTemplate>
                    <asp:Label ID="lblMaterial" runat="server" Text='<%# Bind("Material") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("Price","{0:N2}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
                 <asp:TemplateField HeaderText="Qty Per Visit" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("QtyPerVisit") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
               
                 <asp:TemplateField  ItemStyle-Width="5%">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="item_delete" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkDelete" OnClientClick="if (!confirm('do you want delete item?')) return false;"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>


        
     <ajaxToolkit:ModalPopupExtender ID="mdlMaterial" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="btnAddOptions" PopupControlID="pnlMaterial" CancelControlID="lbl_lbtnClosePassword" >
</ajaxToolkit:ModalPopupExtender>
     <asp:Label ID="btnAddOptions" runat="server"></asp:Label>
        <asp:Label ID="lbl_lbtnClosePassword" runat="server"></asp:Label>
       <asp:Panel ID="pnlMaterial" runat="server" BackColor="White" Style="display:none;"
                       Width="550px" Height="350px" CssClass="panel panel-color panel-info" ScrollBars="None">
          <%-- <asp:UpdatePanel ID="upanle_options" runat="server" UpdateMode="Conditional">
               <ContentTemplate>--%>

             
             <div class="card-header">
							<h3 class="card-body"><asp:Label ID="lblOptions" runat="server" Text="Add Material"></asp:Label>  </h3>
							
							<div class="card-toolbar">
								
								 <asp:LinkButton ID="lbtnClosePop" runat="server" CssClass="cs" SkinID="BtnLinkCloseNoCss" />
								
							</div>
						</div>
    <div class="panel-body">
        <div class="form-group row">
                   <div class="col-md-12 form-inline">
                       <asp:HiddenField ID="huid" runat="server" />
                       
                       <asp:ValidationSummary ID="valSumm" runat="server" ValidationGroup="pay" />
                       </div>
            </div>

           <div class="form-group row">
      <div class="col-md-10">
          <label class="col-sm-3 control-label"> Material</label>
          <div class="col-sm-9">
            
			<asp:TextBox ID="txtMaterial" runat="server" ClientIDMode="Static" SkinID="txt_70"></asp:TextBox>
              <asp:HiddenField ID="hid" runat="server" ClientIDMode="Static" />
              <asp:HiddenField ID="htype" runat="server" ClientIDMode="Static" />
             

              </div>
          </div>
              </div>
                      <div class="form-group row">
      <div class="col-md-10">
          <label class="col-sm-3 control-label"> Price</label>
          <div class="col-sm-9">
            
			<asp:TextBox ID="txtPrice" runat="server" ClientIDMode="Static" SkinID="Price_150px"></asp:TextBox>
             

              </div>
          </div>
              </div>

                       <div class="form-group row">
      <div class="col-md-10">
          <label class="col-sm-3 control-label"> Quantity Per Visit</label>
          <div class="col-sm-9">
            
			<asp:TextBox ID="txtQtyPerVisit" runat="server" ClientIDMode="Static" SkinID="Price_150px" Text="1"></asp:TextBox>
             

              </div>
          </div>
              </div>
					
       
           <div class="form-group row">
                   <div class="col-md-12 form-inline">
                        <label class="col-sm-3 control-label"></label>
                       <div class="col-sm-9 form-inline">
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

        </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnCreateNew').click(function () {

            });
        });
    </script>

    <script type="text/javascript">

        $(document).ready(function () {
            SetCategoryContactData();
            SetSubCategoryContactData();
            SetManufactureData();
        });

        $(function () {

            //SetCategoryContactData();
            $("[id*=ddlCategory]").change(function () {
                $("[id*=hCategory").val($(this).val());
                SetSubCategoryContactData();
            });
            $("[id*=ddlSubCategory]").change(function () {
                $("[id*=hSubCategory").val($(this).val());
                //SetSubCategoryContactData();
            });
            $("[id*=ddlManufacturer]").change(function () {
                $("[id*=hManufacturer").val($(this).val());
                //SetSubCategoryContactData();
            });
        });

        function setCategoryDropdownValue() {
            if ($("[id*=hCategory").val() != '') {
                $("[id*=ddlCategory]").val($("[id*=hCategory").val());
            }
        }

        function SetCategoryContactData() {
            debugger;
            var type_section = 'Maintenance';
            var id ;
          
                id = "1";

            var dataObject = JSON.stringify({
                'typeid': id,
                'section': type_section
            });
           
            if (id != "0") {
                $.ajax({
                    type: "POST",
                    url: "../../../WF/CustomerAdmin/Maintenance/services/ChecklistServices.asmx/CategoryGet",
                    data: dataObject,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (r) {
                        debugger;
                        var ddlCustomers = $("[id*=ddlCategory]");

                        ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                        $.each(r.d, function () {
                            ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                        //$("[id*=hCategory]").val('0');
                        setCategoryDropdownValue();
                    }
                });
            }
            else {
                var ddlCustomers = $("[id*=ddlCategory]");

                ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            }
        }
        function setSubCategoryDropdownValue() {
            if ($("[id*=hSubCategory").val() != '') {
                $("[id*=ddlSubCategory]").val($("[id*=hSubCategory").val());
            }
        }
        function SetSubCategoryContactData() {
            var category = $("[id*=ddlCategory]").val();
            if (category == null)
                category = "0";
            else if (category == "")
                category = "0";

            if (category != "0") {
                $.ajax({
                    type: "POST",
                    url: "../../../WF/CustomerAdmin/Maintenance/services/ChecklistServices.asmx/SubCategoryGet",
                    data: "{category:'" + category + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //async: false,
                    success: function (r) {
                        var ddlCustomers = $("[id*=ddlSubCategory]");
                        //
                        ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                        $.each(r.d, function () {
                            ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                        //$("[id*=hSubCategory]").val('0');
                        setSubCategoryDropdownValue();
                    }
                });
            }
            else {
                var ddlCustomers = $("[id*=ddlSubCategory]");
                //
                ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
            }
        }
        function settManufactureDropdownValue() {
            if ($("[id*=hManufacturer").val() != '') {
                $("[id*=ddlManufacturer]").val($("[id*=hManufacturer").val());
            }
        }
        function SetManufactureData() {
            var id = 2;
            if (id == null)
                id = "0";
            if (id == "")
                id = "0";
            debugger;
            if (id != "0") {
                $.ajax({
                    type: "POST",
                    url: "../../../WF/CustomerAdmin/Maintenance/services/ManufacturerServices.asmx/CategoryGet",
                    data: "{typeid:'" + id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (r) {
                        var ddl = $("[id*=ddlManufacturer]");
                        debugger;
                        ddl.empty().append('<option selected="selected" value="0">Please select</option>');
                        $.each(r.d, function () {
                            ddl.append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                       // $("[id*=hCategory]").val('0');
                        settManufactureDropdownValue();
                    }
                });
            }
            else {
                var ddl = $("[id*=ddlManufacturer]");
                debugger;
                ddl.empty().append('<option selected="selected" value="0">Please select</option>');
            }
        }
    </script>

    </div>

</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
