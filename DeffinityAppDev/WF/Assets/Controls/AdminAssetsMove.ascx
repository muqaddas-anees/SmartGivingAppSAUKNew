<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_AdminAssetsMove" Codebehind="AdminAssetsMove.ascx.cs" %>
<asp:Panel ID="pnlSearch" runat="server">
    <div class="form-group row">
      <div class="col-md-5">
           <label class="col-sm-4 control-label">Serial Number</label>
           <div class="col-sm-8">
               <asp:TextBox ID="txtSearchSerialno" runat="server" SkinID="txt_90"></asp:TextBox>
               </div>
          </div>
         <div class="col-md-5">
              <label class="col-sm-4 control-label">Customer</label>
             <div class="col-sm-8">
                 <asp:DropDownList ID="ddlCustomerUser" runat="server" SkinID="ddl_90"></asp:DropDownList>
                 <ajaxToolkit:CascadingDropDown ID="ccdName" runat="server" TargetControlID="ddlCustomerUser"
                BehaviorID="ccdNa" Category="Name" PromptText="Please select..." PromptValue="0"
                ServicePath="~/WF/DC/webservices/DCServices.asmx" ServiceMethod="GetNameByCompanySession"
                 LoadingText="[Loading customer...]" ClientIDMode="Static" />
                 </div>
             </div>
        </div>
    <div class="form-group row">
      <div class="col-md-5">
           <div class="col-sm-12 form-inline">
             <label>  Warranties Due To Expire Within</label> <asp:TextBox ID="txtSearchExpire" runat="server" SkinID="txt_100px" MaxLength="3"></asp:TextBox> <label>Days</label>
               </div>
          </div>
        <div class="col-md-5">
            <label class="col-sm-4 control-label">List Expired Products</label>
             <div class="col-sm-8">
                 <asp:CheckBox ID="chkExpired" runat="server" />
                 </div>
          </div>
        <div class="col-md-2">
            <asp:Button ID="btnSearch" runat="server" SkinID="btnSearch" OnClick="btnSearch_Click" />
          </div>
        </div>

    </asp:Panel>
<asp:Panel ID="pnlAdd" runat="server">

<div class="form-group row">
          <div class="col-md-12">
               <asp:ValidationSummary ID="Group1" runat="server" ValidationGroup="Group1"/>
   <asp:ValidationSummary ID="Group2" runat="server"  ValidationGroup="Group2"/>
    <asp:ValidationSummary ID="Gruop3" runat="server" ValidationGroup="Group3"/>
    <asp:ValidationSummary ID="Group4" runat="server" ValidationGroup="Group4" />
    <asp:ValidationSummary ID="Group5" runat="server" ValidationGroup="Group5" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="GROUP9" />
 <%--   <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="GROUP10" />--%>
    <asp:ValidationSummary ID="Group6" runat="server" ValidationGroup="Group6"/><asp:ValidationSummary ID="Group7" runat="server" ValidationGroup="Group7"/>
        <asp:Label id="lblerror" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
  <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="file"
                            Display="None" ErrorMessage="Please select CSV File." ValidationGroup="GROUP10"></asp:RequiredFieldValidator>--%>
  <asp:RegularExpressionValidator ID="P1" runat="server"   ValidationExpression="^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$" ErrorMessage ="Please enter valid IP Addres" ControlToValidate="txtipadress" ValidationGroup="Group1" Display="None" ></asp:RegularExpressionValidator>
  <asp:RegularExpressionValidator ID="R11" runat="server"   ValidationExpression="^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$" ErrorMessage ="Please enter valid Subnet Addres" ControlToValidate="txtsubnet" ValidationGroup="Group1" Display="None" ></asp:RegularExpressionValidator>
	</div>
</div>

<div class="form-group row">
        <div class="col-md-12">
           <strong>Add/Edit Product </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
<div class="form-group row">
          <div class="col-md-12">
              <asp:Label ID="lbl_Results" runat="server"></asp:Label>
              <asp:Label ID="lblCustomer" runat="server" Font-Bold="true" style="display:none;"></asp:Label>
              <asp:Label ID="lblContactUser" runat="server" Font-Bold="true"></asp:Label>
	</div>
</div>


<div class="form-group row" style="display:none;visibility:hidden;">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Customer%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlCustomer" runat="server" SkinID="ddl_90" AutoPostBack="True" 
                                              onselectedindexchanged="ddlCustomer_SelectedIndexChanged" ></asp:DropDownList>
            </div>
      </div>
     <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.AssignedTechnician%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="DdlAssignedTechnician" runat="server" SkinID="ddl_90"></asp:DropDownList>
           </div>
     </div>

	<div class="col-md-6" style="display:none;">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Building%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txt_Bulding" runat="server" SkinID="txt_90" AutoCompleteType="Disabled" ></asp:TextBox>
            </div>
	</div>
</div>

<div class="form-group row" style="display:none;visibility:hidden;">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.AssetNumber%></label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txt_assetno" runat="server" EnableTheming="True" SkinID="txt_90" AutoCompleteType="Disabled" />
<asp:LinkButton ID="imgbtnAssetsnum" runat="server"  
        OnClick="imgbtnAssetsnum_Click"  ValidationGroup="Group6" 
        SkinID="BtnLinkSearch" />
<asp:RegularExpressionValidator id="REF" runat="server" ErrorMessage="Assetnumber should not allow Special Characters" ControlToValidate="txt_assetno" ValidationExpression="[^%$#@!~`(*&^%+_=|\/?<>;]*" Display="none"  ValidationGroup="Group1"></asp:RegularExpressionValidator >
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Floor%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txt_Floor" runat="server" EnableTheming="True" SkinID="txt_90" AutoCompleteType="Disabled" ></asp:TextBox>
            </div>
	</div>
</div>
<div class="form-group row">
     <div class="col-md-6" style="visibility:hidden;display:none;">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Owner%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txt_Owner" runat="server" SkinID="txt_90" AutoCompleteType="Disabled" ></asp:TextBox>
            </div>
	</div>
	<div class="col-md-6" style="display:none;visibility:hidden">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Room%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txt_Room" runat="server" SkinID="txt_90" AutoCompleteType="Disabled" ></asp:TextBox>
            </div>
	</div>
    <div class="col-md-6" style="display:none;visibility:hidden;">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.DeskorLocation%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txt_Cab" runat="server" SkinID="txt_90" AutoCompleteType="Disabled" ></asp:TextBox>
            </div>
	</div>
    <div class="col-md-6" style="display:none;visibility:hidden;">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.IPAddress%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtipadress" runat="server" SkinID="txt_90" AutoCompleteType="Disabled" MaxLength="15"/>  
            </div>
	</div>

	<div class="col-md-6" style="display:none;visibility:hidden;">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.SubNet%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtsubnet" runat="server" SkinID="txt_90" AutoCompleteType="Disabled" />
            </div>
	</div>
</div>
<div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Product <%= Resources.DeffinityRes.Type%></label>
           <div class="col-sm-9 form-inline">
               
<asp:DropDownList ID="dt_Type" runat="server"  EnableTheming="True" DataTextField="Type" DataValueField="TypeID" SkinID="ddl_90"></asp:DropDownList>
<asp:RequiredFieldValidator ID="RA" runat="server" ErrorMessage="Please enter type name" Display="None" ControlToValidate="txt_type" ValidationGroup="Group4"></asp:RequiredFieldValidator>
  <asp:TextBox ID="txt_type" runat="server" Visible="false"   EnableTheming="True" SkinID="txt_80" AutoCompleteType="Disabled"></asp:TextBox>
        <asp:LinkButton ID="imagetype" runat="server" OnClick="imagetype_Click" 
         SkinID="BtnLinkAdd"/>
<asp:LinkButton ID="itype_submitt" runat="server" Visible="false" 
        OnClick="itype_submitt_Click" ValidationGroup="Group4"
        SkinID="BtnLinkUpdate"   />
                  <asp:LinkButton ID="itype_cancel" runat="server" Visible="false" 
        OnClick="itype_cancel_Click" SkinID="BtnLinkCancel" />
            </div>
	</div>
     <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.SerialNumber%></label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txt_serialno" runat="server" EnableTheming="True" SkinID="txt_90" AutoCompleteType="Disabled" ></asp:TextBox>
<asp:LinkButton ID="imgbtnserialnum" runat="server"  
        OnClick="imgbtnserialnum_Click"  
        SkinID="BtnLinkSearch"  Visible="false"  />
<asp:RegularExpressionValidator id="REF1" runat="server" ErrorMessage="Serialnumber should not allow Special Characters" ControlToValidate="txt_serialno" ValidationExpression="[^%$#@!~`(*&^%+_=|\/?<>;]*" Display="none"  ValidationGroup="Group1"></asp:RegularExpressionValidator >
            </div>
	</div>
	
</div>
<div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Make%></label>
           <div class="col-sm-9 form-inline">
               <asp:DropDownList id="dt_make" runat="server" EnableTheming="True" DataTextField="Make" DataValueField="MakeID" SkinID="ddl_90"></asp:DropDownList>
<asp:RequiredFieldValidator ID="RD" runat="server" InitialValue="0" ErrorMessage="Please enter make name" Display="None" ControlToValidate="txtmkae" ValidationGroup="Group2"
       ></asp:RequiredFieldValidator>
            <asp:TextBox ID= "txtmkae" runat="server"  Visible="false" EnableTheming="True" SkinID="txt_80" AutoCompleteType="Disabled"  ></asp:TextBox>
<asp:LinkButton ID="imagemake" runat="server" OnClick="imagemake_Click1" 
         SkinID="BtnLinkAdd" />
<asp:LinkButton ID="i_makesubmitt" runat="server" Visible="false" 
        OnClick="i_makesubmitt_Click"  ValidationGroup="Group2" 
        SkinID="BtnLinkUpdate"  />
        <asp:LinkButton ID="i_makecancel" runat="server" 
        Visible="false" OnClick="i_makecancel_Click" 
        SkinID="BtnLinkCancel"  />
            </div>
	</div>
	  <div class="col-md-6">
        <label class="col-sm-3 control-label">Color</label>
        <div class="col-sm-9">
            <asp:TextBox ID="TxtColor" runat="server" SkinID="txt_70"></asp:TextBox>
        </div>
    </div>
</div>
<div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Model%></label>
           <div class="col-sm-9 form-inline">
               <asp:DropDownList ID="dt_Model" runat="server"  EnableTheming="True" DataTextField="Model" DataValueField="ModelID" SkinID="ddl_90"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RC" runat="server" ErrorMessage="Please enter Model name" Display="None" ControlToValidate="txt_model" ValidationGroup="Group3"
       ></asp:RequiredFieldValidator>
    <asp:TextBox ID="txt_model" runat="server" Visible="false" SkinID="txt_80" AutoCompleteType="Disabled" ></asp:TextBox>
   <asp:LinkButton ID="imagemodel" runat="server" OnClick="imagemodel_Click" 
         SkinID="BtnLinkAdd"/>
<asp:LinkButton ID="imodel_submitt" runat="server" Visible="false" 
        OnClick="imodel_submitt_Click" ValidationGroup="Group3"  
        SkinID="BtnLinkUpdate" />
                   <asp:LinkButton ID="imodel_cancel" runat="server" 
        Visible="false" OnClick="imodel_cancel_Click"  
        SkinID="BtnLinkCancel" />
            </div>
	</div>
    <div class="form-group row">
    <div class="col-md-6">
        <label class="col-sm-3 control-label">Status</label>
        <div class="col-sm-9">
            <asp:DropDownList ID="DdlStatus" SkinID="ddl_80" runat="server"></asp:DropDownList>
        </div>
    </div>
  
</div>
    
</div>
<div class="form-group row" style="display:none;visibility:hidden;">
      <div class="col-md-6" style="display:none">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Site%></label>
           <div class="col-sm-9 form-inline">
               <asp:DropDownList ID="dt_Site" runat="server" SkinID="ddl_90" DataTextField="Site" DataValueField="ID"></asp:DropDownList>

<asp:TextBox ID="txt_site" runat="server" Visible="false" SkinID="txt_90" AutoCompleteType="Disabled"></asp:TextBox>
<asp:LinkButton ID="imagelocation" runat="server" OnClick="imagelocation_Click" 
         SkinID="BtnLinkAdd" Visible="false"  />
<asp:LinkButton ID="ilocation_submitt" runat="server" Visible="false" 
        OnClick="ilocation_submitt_Click" ValidationGroup="Group5" 
         SkinID="BtnLinkUpdate"  />
                  <asp:LinkButton ID="ilocation_cancel" runat="server" 
        Visible="false" OnClick="ilocation_cancel_Click" 
        SkinID="BtnLinkCancel"  />  
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.VLan%></label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtvlan" runat="server" SkinID="txt_90"/>
            </div>
	</div>
</div>
<div class="form-group row" style="display:none;visibility:hidden;">
      
	<div class="col-md-6" style="display:none;">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Supplier%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlVendors" runat="server" SkinID="ddl_90">
    </asp:DropDownList>
            </div>
	</div>
</div>
<div class="form-group row">
    
	<div class="col-md-6">
           <label class="col-sm-3 control-label">Date of Purchase</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtPurchasedDate" runat="server" SkinID="Date"></asp:TextBox>
    <asp:Label ID ="ImgPurchasedDate" runat="server" SkinID="Calender" />
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
    ErrorMessage="Please enter Date in dd/mm/yyyy format" ControlToValidate="txtPurchasedDate" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ValidationGroup="Group1">*</asp:RegularExpressionValidator>
            </div>
	</div>
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Warranty Period</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txt_WarrantyPeriod" runat="server" SkinID="txt_100px" AutoCompleteType="Disabled" ></asp:TextBox>
            </div>
	</div>
</div>
<div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label">Warranty Start Date </label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txt_commision" runat="server" SkinID="Date" AutoCompleteType="Disabled" MaxLength="10"></asp:TextBox>
<asp:Label ID ="imgdatecomm" runat="server" SkinID="Calender" />
<asp:RegularExpressionValidator ID="RM1" runat="server" ErrorMessage="Please enter Date in dd/mm/yyyy format"
     ControlToValidate="txt_commision" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ValidationGroup="Group1">*</asp:RegularExpressionValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label">Warranty End Date </label>
           <div class="col-sm-9 form-inline">
               <asp:TextBox ID="txtExpDate" runat="server" SkinID="Date"></asp:TextBox>
    <asp:Label ID ="ImgExpDate" runat="server" SkinID="Calender" />
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
     ErrorMessage="Please enter Date in dd/mm/yyyy format" ControlToValidate="txtExpDate"
     ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ValidationGroup="Group1">*</asp:RegularExpressionValidator>
            </div>
	</div>
</div>
<div class="form-group row" style="display:none;visibility:hidden">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"> Schedule Move Date</label>
           <div class="col-sm-9 form-inline">
                <asp:TextBox ID="txtScheduleMoveDate" runat="server" SkinID="Date" AutoCompleteType="Disabled" MaxLength="10"></asp:TextBox>
   <asp:Label ID ="ScheduleMoveDate" runat="server" SkinID="Calender" />
   <asp:RegularExpressionValidator ID="Regularxxx1" runat="server"
         ErrorMessage="Please enter Date in dd/mm/yyyy format" ControlToValidate="txtScheduleMoveDate"
        ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" ValidationGroup="Group1">*</asp:RegularExpressionValidator>
            </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Value%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtValue" runat="server" SkinID="txt_150px"></asp:TextBox>
            </div>
	</div>
</div>

<div class="form-group row">
          <div class="col-md-6">
 <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Notes%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txt_Notes"  TextMode="MultiLine"  runat="server"  
        EnableTheming="True" Height="60px" SkinID="txtMulti"></asp:TextBox>
            </div>
	</div>
      <div class="col-md-6">
          </div>
</div>
<div class="form-group row">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.ImageUpload%></label>
           <div class="col-sm-9">
               <asp:FileUpload ID="FileUpload1" runat="server" /> <div> <asp:image ID="imgAsset" runat="server" /> </div>
            </div>
	</div>
	<div class="col-md-6" >
               <asp:LinkButton ID="imgViewSoftware" runat="server" 
        OnClick="imgViewSoftware_Click"  Visible="false" 
        SkinID="BtnLinkView" AlternateText="View Installed S/W" />
	
	</div>
</div>
   
<div class="form-group row">
          <div class="col-md-6">
 <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">
               <asp:Button ID="ImgBtnSubmit" runat="server" SkinID="btnSubmit" OnClick="ImgBtnSubmit_Click" ValidationGroup="Group1" />
  <asp:Button ID="ImgBtnUpdate" runat="server" SkinID="btnUpdate" Visible="false" OnClick="ImgBtnUpdate_Click" ValidationGroup="Group1" />
  <asp:Button ID="btn_cancel" runat="server" SkinID="btnCancel" Visible="false" OnClick="ImgBtnCancel_Click"  />
            </div>
	</div>
    <div class="col-md-6">
        </div>
</div>

    </asp:Panel>
<div>
     

<div class="form-group row">
          <div class="col-md-12">
<asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" width="100%"
     DataKeyNames="ID" OnRowDeleting="GridView1_RowDeleting1" OnPageIndexChanging="GridView1_PageIndexChanging"
     PageSize="20" OnRowEditing="GridView1_RowEditing" OnRowCommand="GridView1_RowCommand" >
  <Columns>
  <asp:BoundField DataField="ID" HeaderText="ID" Visible="False"/>
          <asp:TemplateField ItemStyle-HorizontalAlign="Center"> 
                 <ItemStyle Wrap="True"/>     
            <ItemTemplate>
                <asp:LinkButton ID="LinkButtonEdit" runat="server" 
                    CausesValidation="false" CommandName="Selected" CommandArgument='<%# Bind("ID")%>'
                      SkinID="BtnLinkEdit" ToolTip="Edit" ></asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField>
                                    <ItemTemplate>
                                       <%-- <ajaxToolkit:AnimationExtender ID="MyExtender" runat="server" TargetControlID="pnlOriginalImage">
                                            <Animations>
            <OnMouseOver>
            <FadeIn Duration=".5" Fps="20" />
            </OnMouseOver>
                                            </Animations>
                                        </ajaxToolkit:AnimationExtender>--%>
                                      <%--  <ajaxToolkit:HoverMenuExtender ID="hmeDetails" runat="server" TargetControlID="imgContractor"
                                            PopupControlID="pnlOriginalImage" PopDelay="0" PopupPosition="Left" EnableViewState="false"
                                            OffsetY="26" />--%>
                                        <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((int)DataBinder.Eval(Container.DataItem,"ID"),ImageManager.ThumbnailSize.MediumSmaller) %>'
                                            Visible='<%# CheckImageVisibility((int)DataBinder.Eval(Container.DataItem,"ID"))%>' />
                                      <%--  <div id="pnlOriginalImage" runat="server" class="PrepRecipeDetails" style="display: none;">
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.OriginalData) %>'
                                                Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
                                        </div>--%>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
      <asp:HyperLinkField DataTextField="CustomerName" DataNavigateUrlFields="CustomerID" DataNavigateUrlFormatString="~/WF/CustomerAdmin/ContactDetails.aspx?ContactID={0}"
            HeaderText="Customer" ItemStyle-Width = "150" Target="_blank" >
<ItemStyle Width="150px"></ItemStyle>
      </asp:HyperLinkField>
           <asp:BoundField DataField="ID" Visible="False" HeaderText="ID"  />
           <asp:BoundField DataField="ID" Visible="False" HeaderText="ID"  />
           <asp:BoundField DataField="AssetNo" HeaderText="Asset Num" Visible="false" ></asp:BoundField>            
         
           <asp:BoundField DataField="SerialNo" HeaderText="Serial Num" ItemStyle-HorizontalAlign="Right">
<ItemStyle HorizontalAlign="Right"></ItemStyle>
           </asp:BoundField>
           <%--<asp:BoundField DataField="Type" HeaderText="Type" >
           </asp:BoundField>--%>
           <asp:BoundField DataField="FromSiteName" HeaderText="Site" Visible="false">
           </asp:BoundField>
            <asp:BoundField DataField="FromOwner" HeaderText="Owner" Visible="false">
                 </asp:BoundField>  
                  <asp:TemplateField HeaderText="Type">
                      <EditItemTemplate>
                          <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("TypeName") %>'></asp:TextBox>
                      </EditItemTemplate>
                      <ItemTemplate>
                          <asp:Label ID="Label3" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                      </ItemTemplate>
                     
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Make" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="100px"  ControlStyle-Width="100px" FooterStyle-Width="100px">
          <EditItemTemplate>
              <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("MakeName") %>'></asp:TextBox>
          </EditItemTemplate>
          <ItemTemplate>
              <asp:Label ID="Label2" runat="server" Text='<%# Bind("MakeName") %>'></asp:Label>
          </ItemTemplate>
         
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Model" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="100px"  ControlStyle-Width="100px" FooterStyle-Width="100px">
          <EditItemTemplate>
              <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ModelName") %>'></asp:TextBox>
          </EditItemTemplate>
          <ItemTemplate>
              <asp:Label ID="Label1" runat="server" Text='<%# Bind("ModelName") %>'></asp:Label>
          </ItemTemplate>
         
      </asp:TemplateField>
           <asp:BoundField DataField="FromBuilding" HeaderText="Building" Visible="false">
                 </asp:BoundField>  
           <asp:BoundField DataField="FromFloor" HeaderText="Floor" Visible="false">
                 </asp:BoundField>  
           <asp:BoundField DataField="FromRoom" HeaderText="Room" Visible="false">
                 </asp:BoundField> 
                     <%-- <asp:BoundField DataField="FromOwner" HeaderText="Owner" >
                 </asp:BoundField> --%>
                 <%-- <asp:BoundField DataField="FromIPAddress" HeaderText="IP Address" >
                  
                 </asp:BoundField> --%>
                 <asp:BoundField DataField="ID" HeaderText="ID" Visible="False"/>
                  <asp:TemplateField HeaderText="Software" Visible="false">
                    <ItemStyle Width="5%" />  
                    <ItemTemplate>
                       <%--<asp:ImageButton ID="imgViewSoft" runat="server" CausesValidation="false" CommandName="View"
                            SkinID="ImgView" CommandArgument="<%# Bind('ID')%>"  ToolTip="View" />--%>
                            
                        <asp:LinkButton ID="lnkViewSoft" runat="server" CommandArgument='<%# Bind("ID")%>' CommandName="View" CausesValidation="false" ToolTip="Warranty Docs"><i class="fa fa-folder-o"></i></asp:LinkButton>
                    </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Date<br> Purchased" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="LblDatePurchased" runat="server" Text='<%#Bind("PurchasedDate","{0:d}")%>'></asp:Label> 
                     </ItemTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Warranty<br> Start Date" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="LblWarrantyStartDate" Text='<%#Bind("Datecommision","{0:d}")%>' runat="server"></asp:Label> 
                     </ItemTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                 </asp:TemplateField>
               <asp:TemplateField HeaderText="Warranty<br> End Date" ItemStyle-HorizontalAlign="Right">
                     <ItemTemplate>
                         <asp:Label ID="LblWarrantyEndDate" Text='<%#Bind("ExpDate","{0:d}")%>' runat="server"></asp:Label> 
                     </ItemTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                 </asp:TemplateField>
       <asp:TemplateField HeaderText="Warranty<br> Period">
                     <ItemTemplate>
                         <asp:Label ID="LblWarranty"
                              Text=<%#Bind("FromPort") %>
                              runat="server"></asp:Label> 
                     </ItemTemplate>
                 </asp:TemplateField>
              <asp:TemplateField HeaderText="Days Before<br> Warranty Expires" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="col-nowrap" >
                     <ItemTemplate>
                         <asp:Label ID="LblDaysBeforeWarrantyExpires"
                              Text='<%#Bind("DifferenceInDays") %>'
                              runat="server"></asp:Label> 
                     </ItemTemplate>

<ItemStyle HorizontalAlign="Right" CssClass="col-nowrap"></ItemStyle>
                 </asp:TemplateField>
        <asp:TemplateField HeaderText="Notes" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="200px"  ControlStyle-Width="200px" FooterStyle-Width="200px">
               <ItemTemplate>
                   <asp:Label ID="lblNotes" runat="server" Text=<%#Bind("FromNotes") %>></asp:Label>
               </ItemTemplate>

<ControlStyle Width="200px"></ControlStyle>

<FooterStyle Width="200px"></FooterStyle>

<ItemStyle CssClass="col-nowrap" Width="200px"></ItemStyle>
           </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false" CommandName="delete"
                            SkinID="BtnLinkDelete"  CommandArgument='<%# Bind("ID") %>'
                             OnClientClick="return confirm('Do you want to delete the Asset?');" ToolTip="delete"></asp:LinkButton>
                            
                    </ItemTemplate>
                       
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                       
                </asp:TemplateField>
                   
          </Columns>
   
       
  </asp:GridView>
 <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField4" runat="server" />
    <asp:HiddenField ID="HiddenField5" runat="server" />
      <asp:HiddenField ID="HiddenAssID" runat="server" />
 
 <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgdatecomm" TargetControlID="txt_commision"  CssClass="MyCalendar">
          </ajaxToolkit:CalendarExtender>
          <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ScheduleMoveDate" TargetControlID="txtScheduleMoveDate"  CssClass="MyCalendar">
          </ajaxToolkit:CalendarExtender>
           <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="ImgExpDate" TargetControlID="txtExpDate"  CssClass="MyCalendar">
          </ajaxToolkit:CalendarExtender>
           <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="ImgPurchasedDate" TargetControlID="txtPurchasedDate"  CssClass="MyCalendar">
          </ajaxToolkit:CalendarExtender>
              </div>
    </div>

</div>