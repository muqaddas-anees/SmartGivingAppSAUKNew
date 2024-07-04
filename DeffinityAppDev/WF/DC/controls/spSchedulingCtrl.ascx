<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="spSchedulingCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.spSchedulingCtrl" %>

<div class="row">
          <div class="col-md-12">
 <strong>Service Provider Scheduling Algorithm </strong> 
<hr class="no-top-margin" />
	</div>
</div>
<div class="row">
          <div class="col-md-12">
              <asp:Label ID="lblsuccessmsg" runat="server" SkinID="GreenBackcolor"></asp:Label>
	</div>
</div>
   
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-2 control-label">Select Type of Scheduling</label>
           <div class="col-sm-10">
               <div class="col-sm-12">
                   <asp:RadioButton ID="rdSequence" runat="server" Text="Schedule by Sequence One Service Provider at a Time" ClientIDMode="Static"  />
                   <div class="col-sm-12 form-inline">
                        <label class="col-sm-4 control-label">Interval In Mins Before Next SP Selected</label>
           <div class="col-sm-6">
               <asp:TextBox ID="txtSequenceInterval" runat="server" SkinID="txt_100px" MaxLength="5"></asp:TextBox>
            </div>
                       
                       <ajaxToolkit:FilteredTextBoxExtender ID="txtFiltertxtSequenceInterval" runat="server" ValidChars="0123456789" TargetControlID="txtSequenceInterval"></ajaxToolkit:FilteredTextBoxExtender>
                       </div>
                   </div>
                <div class="col-sm-12">
                   <asp:RadioButton ID="rdBlast" runat="server" Text="Schedule by Blast - Sends to Multiple Service Providers at a time" ClientIDMode="Static" />
                     <div class="col-sm-12 form-inline">
                        <label class="col-sm-2 control-label">Initial Batch Qty</label>
           <div class="col-sm-2">
               <asp:TextBox ID="txtIntialBatchQty" runat="server" SkinID="txt_75px" MaxLength="5"></asp:TextBox>
            </div>
                          <label class="col-sm-2 control-label">Min Before Next Blast</label>
           <div class="col-sm-2 form-inline">
               <asp:TextBox ID="txtMinBeforeNextBlast" runat="server" SkinID="txt_75px" MaxLength="5"></asp:TextBox> <label class="control-label">Mins</label>
            </div>
                          <label class="col-sm-2 control-label">Second Batch Qty</label>
           <div class="col-sm-2">
               <asp:TextBox ID="txtSecondBatchQty" runat="server"  SkinID="txt_75px" MaxLength="5"></asp:TextBox>
            </div>
                       
                       <ajaxToolkit:FilteredTextBoxExtender ID="FilteredtxtIntialBatchQty" runat="server" ValidChars="0123456789" TargetControlID="txtIntialBatchQty"></ajaxToolkit:FilteredTextBoxExtender>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredtxtMinBeforeNextBlast" runat="server" ValidChars="0123456789" TargetControlID="txtMinBeforeNextBlast"></ajaxToolkit:FilteredTextBoxExtender>
                         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredtxtSecondBatchQty" runat="server" ValidChars="0123456789" TargetControlID="txtSecondBatchQty"></ajaxToolkit:FilteredTextBoxExtender>
                       </div>
                   </div>


                <div class="col-sm-12">
                    <asp:LinkButton ID="btnSubmit" runat="server" SkinID="btnSubmit" OnClick="btnSubmit_Click"></asp:LinkButton> 
                    </div>

            </div>
	</div>
</div>
<script type="text/javascript">

    $(document).ready(function () {

        $('#rdSequence').click(function () {
            $('#rdBlast').attr('checked', false);
        })
        $('#rdBlast').click(function () {
            $('#rdSequence').attr('checked', false);
        })
        //var status = $('#rdBlast').is(':checked');
        //alert(status)
    })

</script>

 <div class="form-group row">
        <div class="col-md-12">
           <strong> Criteria And Weighting </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
   
    <div class="form-group row">
        <div class="col-md-12">
             <asp:Label ID="lblMsg_a" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
            <asp:Label ID="lblError_a" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
             <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ValidationGroup="AInsertSum" />
<asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ValidationGroup="AUpdateSum" />
            </div>
                  </div>
     <div class="form-group row">
        <div class="col-md-12">
            
     
      <asp:ListView ID="listCriteria" runat="server" InsertItemPosition="LastItem" OnItemCanceling="listCriteria_ItemCanceling" OnItemCommand="listCriteria_ItemCommand" OnItemDataBound="listCriteria_ItemDataBound" OnItemEditing="listCriteria_ItemEditing">
           <LayoutTemplate>
            <table style="width:100%" class="table table-small-font table-bordered table-striped datatable" >
                <thead>
                    <tr class="tab_header" style="font-weight:bold;margin:5px 5px 5px 5px;height:30px;">
                        <td></td>
                        <td>Selected</td>
                        <td>Criteria</td>
                        <td>Weighting</td>
                    </tr>
                </thead>
                <tbody>
                    <tr id="ItemPlaceholder" runat="server"></tr>
                </tbody>
                <tfoot>
                </tfoot>
            </table>
              </LayoutTemplate>
          <ItemTemplate>
              <tr class="even_row">  
                  <td style="width:110px" class="form-inline">
                       <asp:LinkButton ID="btnEdit" runat="server" SkinID="BtnLinkEdit" CommandName="Edit" Text="Edit" CommandArgument='<%# Eval("CWID") %>' />
                       <asp:LinkButton ID="btnDel" runat="server" CommandArgument='<%# Eval("CWID") %>' CommandName="Del" SkinID="BtnLinkDelete" OnClientClick="if (!confirm('do you want delete item?')) return false;" Text="Delete" ImageAlign="AbsMiddle" />
                   </td>
                  <td>
                      <asp:CheckBox ID="chkSelected" runat="server" Checked='<%# Eval("IsEnable")%>' />
                  </td>
                 <td>
                      <asp:Label ID="lblCriteria" runat="server" Text='<%# Eval("Criteria")%>'></asp:Label>
                     
                 </td>
                   <td>
                      <asp:Label ID="lblWeighting" runat="server" Text='<%# Eval("Weighting")%>'></asp:Label>
                 </td>
                   
                </tr>     
          </ItemTemplate>
          <EditItemTemplate>
              <tr >  
                   <td style="width:110px" class="form-inline">
                       <asp:LinkButton ID="btnUpdate" runat="server" SkinID="BtnLinkUpdate" CommandName="UpdateItem" CommandArgument='<%# Eval("CWID")%>' Text="Update" ValidationGroup="AUpdateSum" />
                       <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CausesValidation="false" Text="Cancel" SkinID="BtnLinkCancel" />
                       
                   </td>
                   <td>
                      <asp:CheckBox ID="chkSelected_e" runat="server" Checked='<%# Eval("IsEnable")%>' />
                  </td>
                 <td>
                     <asp:Label ID="lblID" runat="server" Text='<%# Eval("CWID")%>' Visible="false"></asp:Label>
                     <asp:TextBox ID="txtCriteria_e" runat="server"  Text='<%# Eval("Criteria")%>' MaxLength="300"></asp:TextBox>
                     
                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorC2" runat="server" ErrorMessage="Please enter Criteria" ControlToValidate="txtCriteria_e" Display="None" ValidationGroup="AUpdateSum"></asp:RequiredFieldValidator>
                 </td>
               <td>
                     <asp:TextBox ID="txtWeighting_e" runat="server"  SkinID="txt_100px" Text='<%# Eval("Weighting")%>' MaxLength="5" ></asp:TextBox>
                   <asp:RequiredFieldValidator  ID="RequiredFieldValidatorW2" runat="server" ErrorMessage="Please enter Weighting" ControlToValidate="txtWeighting_e" Display="None" ValidationGroup="AUpdateSum"></asp:RequiredFieldValidator>
                 </td>
                  
                </tr>   
          </EditItemTemplate>
          <InsertItemTemplate>
            <tr>  
                 <td style="width:110px" class="form-inline">
                       <asp:LinkButton  ID="btnAdd" runat="server" CommandName="Add" ValidationGroup="AInsertSum" Text="Add" SkinID="BtnLinkAdd" />
                       <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CausesValidation="false" SkinID="BtnLinkCancel"  />
                   </td>
                <td>
                     <asp:CheckBox ID="chkSelected" runat="server" />
                </td>
                <td>
                     <asp:TextBox ID="txtCriteria" runat="server" MaxLength="300"></asp:TextBox>
                     
                     <asp:RequiredFieldValidator  ID="RequiredFieldValidatorC1" runat="server" ErrorMessage="Please enter Criteria" ControlToValidate="txtCriteria" Display="None" ValidationGroup="AInsertSum"></asp:RequiredFieldValidator>
                 </td>
               <td>
                     <asp:TextBox ID="txtWeighting" runat="server"  SkinID="txt_100px" MaxLength="5"></asp:TextBox>
                   <asp:RequiredFieldValidator  ID="RequiredFieldValidatorW1" runat="server" ErrorMessage="Please enter Weighting" ControlToValidate="txtWeighting" Display="None" ValidationGroup="AInsertSum"></asp:RequiredFieldValidator>
                 </td>
                  
                </tr>    
          </InsertItemTemplate>
      </asp:ListView>
            </div>
                  </div>

 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
