<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminPolicyTypeCtrl.ascx.cs" Inherits="DeffinityAppDev.WF.DC.controls.AdminPolicyTypeCtrl" %>
 <%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 
 <script src="../../Content/assets/js/ckeditor/ckeditor.js"></script>
    <script src="../../Content/assets/js/ckeditor/adapters/jquery.js"></script>
<div class="form-group row mb-6">
     <div class="col-md-12">
         <asp:Label  id="lblSuccess" runat="server" SkinID="GreenBackcolor" EnableViewState="false"></asp:Label>
         <asp:Label  id="lblError" runat="server" SkinID="RedBackcolor" EnableViewState="false"></asp:Label>
         <asp:HiddenField ID="hid" runat="server" Value ="0" />
         </div>
    </div>
<div class="form-group row mb-6">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Maintenance Plans</label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlPolicyType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPolicyType_SelectedIndexChanged"></asp:DropDownList>
               <asp:TextBox ID="txtPolicyType" runat="server" Visible="false" MaxLength="200"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6 form-inline">
           <asp:LinkButton ID="btnAddPolicy" runat="server" SkinID="btnAdd" Text="Add" ToolTip="Add New Policy Type" OnClick="btnAddPolicy_Click" />
         <asp:LinkButton ID="btnEditPolicy" runat="server"  SkinID="btnEdit" Text="Edit" OnClick="btnEditPolicy_Click" />
        <asp:LinkButton ID="btnSave" runat="server" SkinID="btnSave" OnClick="btnSave_Click" />
         <asp:LinkButton ID="btnCancel" runat="server" SkinID="btnCancel" Visible="false" OnClick="btnCancel_Click" />
        <asp:LinkButton ID="btnDelete" runat="server" SkinID="BtnLinkDelete" OnClick="btnDelete_Click" OnClientClick="return confirm('Do you want to delete the record?');"></asp:LinkButton>
	</div>
	
</div>
<div class="form-group row mb-6">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Maintenance Plan Prefix</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtPolicyTypePrefix" runat="server" MaxLength="20"></asp:TextBox>
               </div>
          </div>
    </div>
 <div class="form-group row mb-6" style="display:none;visibility:hidden;">
          <div class="col-md-6">
 <label class="col-sm-3 control-label"></label>
              <div class="col-sm-9">
            <asp:RadioButtonList ID="btnft" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btnft_SelectedIndexChanged">
                   <asp:ListItem Value="0" Text="LESS THAN 5,000 SQ FT" Selected="True"></asp:ListItem>
                   <asp:ListItem Value="1" Text="MORE THAN 5,000 SQ FT"  ></asp:ListItem>
               </asp:RadioButtonList>
                  </div>
	</div>
</div>
<div class="form-group row mb-6">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Monthly Cost</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtMonthly" runat="server" MaxLength="20" Text="0.00" SkinID="Price"></asp:TextBox>
               </div>
          </div>
    </div>
<div class="form-group row mb-6">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Yearly Cost</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtYearly" runat="server" MaxLength="20" Text="0.00" SkinID="Price"></asp:TextBox>
               </div>
          </div>
    </div>
<div class="form-group row mb-6">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Discount (%)</label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtDiscount" runat="server" MaxLength="5" Text="0.00" SkinID="Price"></asp:TextBox>
               </div>
          </div>
    </div>
<div class="form-group row mb-6">
      <div class="col-md-8">
           <CKEditor:CKEditorControl ID="txtPolicyDetails" BasePath="~/Scripts/ckeditor/" runat="server"
                          Width="600px" Height="400px" ClientIDMode="Static"  ></CKEditor:CKEditorControl>
          <%-- <asp:TextBox runat="server"
        ID="txtPolicyDetails" 
        TextMode="MultiLine" 
        Width="600px" Height="400px"
         />
    
    <ajaxToolkit:HtmlEditorExtender 
        ID="htmlEditorExtender1" 
        TargetControlID="txtPolicyDetails" 
        runat="server" DisplaySourceTab="true" >
    </ajaxToolkit:HtmlEditorExtender>--%>
             
          </div>
    </div>
<div class="form-group row mb-6">
      <div class="col-md-8">
          <asp:LinkButton ID="btnBottomSave" runat="server" SkinID="btnSave" OnClick="btnBottomSave_Click" />
          </div>
    </div>
