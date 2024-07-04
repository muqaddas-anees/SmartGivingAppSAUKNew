<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="MaintenancePlanSetup.aspx.cs" Inherits="DeffinityAppDev.WF.CustomerAdmin.Maintenance.MaintenancePlanSetup" %>

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
    Maintenance Plan
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
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
        <%-- <div id="loading">
        <asp:Label SkinID="Loading" ID="lblloading" runat="server" ClientIDMode="Static"></asp:Label>
             </div>--%>
         
<script>

    //$(document).ready(function () {
    //    // invoked when sending ajax request
    //    $(document).ajaxSend(function () {
    //        $("#loading").show();
    //    });

    //    // invoked when sending ajax completed
    //    $(document).ajaxComplete(function () {
    //        $("#loading").hide();
    //    });
       
    //});
  
    </script>
      <div class="col-md-10">
          <label id="lblmsg" style="width:100%"></label>
          <asp:Label ID="lblSuccess" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
          </div>
         </div>

      <div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Your Price Based on Your Plan</label>
           <div class="col-sm-6 form-inline">
               <asp:TextBox ID="txtPlanPrice" runat="server" ClientIDMode="Static" SkinID="Price_150px" MaxLength="15"></asp:TextBox>  
               <asp:Button ID="btnSaveData" runat="server" SkinID="btnSave" ClientIDMode="Static" OnClick="btnSaveData_Click" />
            </div>
              
	</div>
</div>
   

     <%-- <div class="form-group row">
            <div class="col-md-12">
          <div id="eqlist" class="eqlist"></div>
                </div>

          </div>--%>
    <div class="card shadow-sm">
                     <div class="card-header">
                               Equipment(s) <asp:HiddenField ID="hplanid" runat="server" />
                             </div>
                     <div class="panel-body">
                                <asp:ListView ID="list_Customfields" runat="server" InsertItemPosition="LastItem" OnItemCanceling="list_Customfields_ItemCanceling" OnItemCommand="list_Customfields_ItemCommand" OnItemDataBound="list_Customfields_ItemDataBound" OnItemEditing="list_Customfields_ItemEditing">
           <LayoutTemplate>
              <div class="form-group ">
        <div class="col-md-12">
                    <asp:PlaceHolder id="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
                  </div>
              </LayoutTemplate>
                                    <InsertItemTemplate>
                                        <asp:Label ID="lbl" runat="server"></asp:Label>
                                    </InsertItemTemplate>
          <ItemTemplate>

              <div class="well">

       <div class="form-group row">
          <div class="col-md-4">
 <label class="col-sm-3 control-label">Type of Equipment</label>
           <div class="col-sm-8">
              <asp:TextBox ID="txtTypeofEquipment" runat="server" Text='<%# Eval("TypeOfEquipmentName") %>'></asp:TextBox>
                <asp:Label ID="lbleqid" runat="server" Text='<%# Eval("EquipmentID") %>' Visible="false"></asp:Label>
            </div>
              
	</div>
             <div class="col-md-4">
 <label class="col-sm-3 control-label">Checklist</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtChecklist" runat="server" Text='<%# Eval("ChecklistName") %>'></asp:TextBox>
            </div>
              
	</div>
             <div class="col-md-4">
 <label class="col-sm-3 control-label">Start Month</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtStartMonth" runat="server" Text='<%# Eval("StartMonth") %>'></asp:TextBox>
            </div>
              
	</div>
</div>


      <div class="form-group row">

              <div class="col-md-4">
 <label class="col-sm-3 control-label">Manufacturer</label>
           <div class="col-sm-8">
              <asp:TextBox ID="txtManufacturer" runat="server" Text='<%# Eval("ManufacturerName") %>'></asp:TextBox>
            </div>
              
	</div>

            <div class="col-md-4">
 <label class="col-sm-3 control-label">QTY</label>
           <div class="col-sm-8">
              <asp:TextBox ID="txtQTY" runat="server" SkinID="Price_150px"  Text='<%# Eval("QTY") %>'></asp:TextBox>
            </div>
              
	</div>
          </div>

     <div class="form-group row">
            <div class="col-md-4">
 <label class="col-sm-3 control-label">Model Number</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtModelNumber" runat="server" ClientIDMode="Static" Text='<%# Eval("ModelNumber") %>'></asp:TextBox>
            </div>
              
	</div>

          <div class="col-md-4">
 <label class="col-sm-3 control-label">Time Per Year</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtTimeperyear" runat="server" ClientIDMode="Static" Text='<%# Eval("TimePerYear") %>'></asp:TextBox>
            </div>
              
	</div>

         </div>

      <div class="form-group row">
            <div class="col-md-4">
 <label class="col-sm-3 control-label">Serial Number</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtSerialNumber" runat="server" ClientIDMode="Static" Text='<%# Eval("SerialNumber") %>'></asp:TextBox>
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
        <asp:GridView ID="gridMaterials" runat="server" Width="80%">
            <Columns>
                <asp:TemplateField ItemStyle-Width="5%" Visible="false">
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
                 <asp:TemplateField  ItemStyle-Width="5%"  Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="item_delete" CommandArgument='<%# Bind("ID") %>' SkinID="BtnLinkDelete" OnClientClick="if (!confirm('do you want delete item?')) return false;"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>

        </div>

                   <div class="form-group row">
            <div class="col-md-12">
                 <asp:HyperLink ID="hlink" runat="server" Text="View" SkinID="Button" style="float:right;" ></asp:HyperLink>
                </div>
                       </div>

    </div>


              

              </ItemTemplate>
               </asp:ListView>

                           <div class="form-group row">
          <div class="col-md-10">
              <asp:Button ID="btnAddEquipment" runat="server" SkinID="btnDefault" Text="Add Equipment" ClientIDMode="Static" OnClick="btnAddEquipment_Click" />
              </div>

         </div>
                            </div>
                    </div>
   
    <script>
        var dropdown = "dropdown";
        var textbox = "textbox";
        var type_section = 'Maintenance';
        $(document).ready(function () {

            var eqtype = 

            //$("[id*=btnAddEquipment]").click(function () {
            //    //generate new panel
            //    $("[id*=eqlist]").append(generatePanle());

            //    return false;
            //});

           
            function getEqData() {


            }
            function generateType(controltype, controlID, controlValue)
                {
                var str = "";
                
                str = str + "<div class='col-sm-9'>";
                if (controltype == dropdown) {
                    str = str + "<select class='form-control' name='ctl_ddl_" + controlID + "' id='ctl_ddl_" + controlID + "' style='width:80%'>";
                    str = str +  "<option value=''>" + "Please select..." + "</option>";
                  
                    str = str + "</select>";
                }
                else if (controltype == textbox) {
                    str = str + "<input type='text' id='ctl_txt_" + controlID + "' class='form-control' value='" + controlValue + "' style='width:80%'/>";
                   
                }
               
                str = str + "";
                str = str + "</div>";
                return str;

            }


            function generateCell(name,controltype,controlID,controlValue) {

                var str = "";
                str = str + "<div class='col-md-4'><label class='col-sm-3 control-label'>" + name + "</label>" + generateType(controltype, controlID, controlValue)+"</div>";
                return str;

            }

            function generatePanle() {

                var str = "";
                str = str + "<div class='form-group well'>";
                debugger;
                //first row
                str = str + "<div class='form-group '>";
                str = str + generateCell("Type of Equipment", dropdown, "1", "");
                str = str + generateCell("Check list", dropdown, "2", "");
                str = str + generateCell("Start Month", dropdown, "3", "");
                str = str + "</div>";
                //second row
                str = str + "<div class='form-group'>";
                str = str + generateCell("Manufacture", textbox, "4", "");
                str = str + generateCell("QTY", textbox, "5", "");
                str = str + "</div>";

                //third row
                str = str + "<div class='form-group'>";
                str = str + generateCell("Model Number", textbox, "7", "");
                str = str + generateCell("Times Per Year", dropdown, "8", "");
                str = str + "</div>";

                //fourth row
                str = str + "<div class='form-group'>";
                str = str + generateCell("Serial Number", textbox, "7", "");
                str = str + "</div>";

                str = str + "";
                str = str + "";
                str = str + "";
                str = str + "";
                str = str + "";
                str = str + "";

               
                str = str + "</div>";

                return str;

            }


            function SetCategoryContactData() {
                var id = $("[id*=hSector]").val();
                if (id == null)
                    id = "0";
                if (id == "")
                    id = "0";

                var dataObject = JSON.stringify({
                    'typeid': 2,
                    'section': type_section
                });
                console.log("id:" + id);
                if (id != "0") {
                    $.ajax({
                        type: "POST",
                        url: "../../../WF/CustomerAdmin/Maintenance/services/ChecklistServices.asmx/CategoryGet",
                        data: dataObject,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        success: function (r) {
                            var ddlCustomers = $("[id*=ddlCategory]");

                            ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                            $.each(r.d, function () {
                                ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
                            });
                            $("[id*=hCategory]").val('0');
                            setCategoryDropdownValue();
                        }
                    });
                }
                else {
                    var ddlCustomers = $("[id*=ddlCategory]");

                    ddlCustomers.empty().append('<option selected="selected" value="0">Please select</option>');
                }
            }


        });

    </script>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
