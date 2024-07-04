<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_UserAssetsMove" Codebehind="UserAssetsMove.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

   <div>
   <asp:Label id="lblerror" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label> 
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SearchAssest" />
      <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Group9" />
  
  <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Grouplist" />
  
  <%--<asp:RegularExpressionValidator ID="E" runat="server"   ValidationExpression="^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$" ErrorMessage ="Please enter valid IP Addres" ControlToValidate="sourceIPAdress" ValidationGroup="Group9" Display="None" ></asp:RegularExpressionValidator>
  <asp:RegularExpressionValidator ID="S" runat="server"   ValidationExpression="^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$" ErrorMessage ="Please enter valid Subnet Addres" ControlToValidate="sourcetxtsubnet" ValidationGroup="Group9" Display="None" ></asp:RegularExpressionValidator>--%>
        
          <%--<asp:RegularExpressionValidator ID="ReT" runat="server"   ValidationExpression="^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$" ErrorMessage ="Please enter valid IP Addres" ControlToValidate="txtipadress" ValidationGroup="Group9" Display="None" ></asp:RegularExpressionValidator>
  <asp:RegularExpressionValidator ID="D" runat="server"   ValidationExpression="^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$" ErrorMessage ="Please enter valid Subnet Addres" ControlToValidate="txtsubnet" ValidationGroup="Group9" Display="None" ></asp:RegularExpressionValidator>--%>
        
      
  
  
   <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="AddedAssets" />
   
      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAttributeText" Display="None" ValidationGroup="SearchAssest"
          ErrorMessage="Please enter asset text" ></asp:RequiredFieldValidator>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="ddlattribute" InitialValue="0" Display="None" ValidationGroup="SearchAssest"
          ErrorMessage="Please select assest Type"></asp:RequiredFieldValidator>
          
           <%--<asp:RegularExpressionValidator ID="P1" runat="server"   ValidationExpression="^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$" ErrorMessage ="Please enter valid IP Addres" ControlToValidate="sourceIPAdress" ValidationGroup="AddedAssets" Display="None" ></asp:RegularExpressionValidator>
  <asp:RegularExpressionValidator ID="R11" runat="server"   ValidationExpression="^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$" ErrorMessage ="Please enter valid Subnet Addres" ControlToValidate="sourcetxtsubnet" ValidationGroup="AddedAssets" Display="None" ></asp:RegularExpressionValidator>--%>
        
          <%--<asp:RegularExpressionValidator ID="Redf1" runat="server"   ValidationExpression="^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$" ErrorMessage ="Please enter valid IP Addres" ControlToValidate="txtipadress" ValidationGroup="AddedAssets" Display="None" ></asp:RegularExpressionValidator>
  <asp:RegularExpressionValidator ID="Rdfd" runat="server"   ValidationExpression="^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$" ErrorMessage ="Please enter valid Subnet Addres" ControlToValidate="txtsubnet" ValidationGroup="AddedAssets" Display="None" ></asp:RegularExpressionValidator>--%>
        
        
   </div>
<div>
     <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  Search</strong>
            <hr class="no-top-margin" />
            </div>
    </div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Name%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txtAttributeText" runat="server"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-7">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Type%></label>
                                      <div class="col-sm-10 form-inline"><asp:DropDownList ID="ddlattribute" runat="server"  SkinID="ddl_50" >
        <asp:ListItem Value="0" Text="Please Select...">Please Select...</asp:ListItem>
         <asp:ListItem Value="1" Text="Asset Number">Asset Number</asp:ListItem>
          <asp:ListItem Value="2" Text="Serial Number">Serial Number</asp:ListItem>
            <asp:ListItem Value="3" Text="Serial Number">Owner</asp:ListItem>
           <asp:ListItem Value="4" Text="Make">Make</asp:ListItem>
            <asp:ListItem Value="5" Text="Model">Model</asp:ListItem>
             <asp:ListItem Value="6" Text="Model">Site</asp:ListItem>
             <asp:ListItem Value="7" Text="Floor">Floor</asp:ListItem>
               <asp:ListItem Value="8" Text="Building">Building</asp:ListItem>
        </asp:DropDownList>  <asp:Button ID="btn_searchAssets" runat="server" 
            SkinID="btnSearch"  onclick="btn_searchAssets_Click"  
                                             ValidationGroup="SearchAssest"    />
					</div>
				</div>
</div>

</div>

<asp:Panel ID="SearchGridPanel" runat="server" ScrollBars="Horizontal" Width="100%" Visible="false" >

<div class="form-group"><asp:Button ID="btnAssign" Text="Assign to Project" runat="server" OnClick="btnAssign_Click" Visible="false" /></div>
<asp:GridView ID="GridView2" runat="server"  AutoGenerateColumns="False" width="100%" DataKeyNames="ID" PageSize="8" EmptyDataText="No assets found for this search criteria." >
  <Columns>
      <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSelectAll" runat="server" />
                        <asp:HiddenField ID="HID" runat="server" Value="<%# Bind('ID')%>" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle  HorizontalAlign="Center" Width="20px"/>
                </asp:TemplateField>
  <asp:BoundField DataField="ID" HeaderText="ID" Visible="False"/>
      <asp:BoundField HeaderText="Asset Num" DataField="AssetNo" />
      <asp:BoundField HeaderText="Serial Num" DataField="serialno" />
      <asp:BoundField HeaderText="Owner" DataField="FromOwner" />
      <asp:BoundField HeaderText="Make" DataField="Make" />
      <asp:BoundField HeaderText="Model" DataField="Model" />
      <asp:BoundField HeaderText="Site" DataField="Site1" />
      <asp:BoundField HeaderText="Floor" DataField="FromFloor" />
      <asp:BoundField HeaderText="Building" DataField="FromBuilding" HeaderStyle-CssClass="header_bg_r" />                   
          </Columns>   
  </asp:GridView>

</asp:Panel>
<div class="row">
                                <div class="col-md-4" ID="AssetsSource" runat="server">
                                    <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  Source Details </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> Asset Number</label>
                                      <div class="col-sm-8 form-inline"> <asp:TextBox ID="txt_assetno" runat="server" EnableTheming="True"   AutoCompleteType="Disabled" MaxLength="50" /><asp:LinkButton ID="imgbtnserialnum" runat="server" SkinID="BtnLinkSearch" Visible="false"   />
    <asp:RegularExpressionValidator id="RF" runat="server" ErrorMessage="Assetnumber should not allow special characters" ControlToValidate="txt_assetno" ValidationExpression="[^%$#@!~`(*&^%+_=|\/?<>;]*" Display="none"  ValidationGroup="Group1"></asp:RegularExpressionValidator >
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> Serial Number</label>
                                      <div class="col-sm-8 form-inline"> <asp:TextBox ID="txt_serialno" runat="server" EnableTheming="True"   AutoCompleteType="Disabled" MaxLength="50" ></asp:TextBox>
     <asp:LinkButton ID="imgbtnAssetsnum" runat="server" SkinID="BtnLinkSearch" 
         onclick="imgbtnAssetsnum_Click" Visible="false" />
 <asp:RegularExpressionValidator id="R1" runat="server" ErrorMessage="Serialnumber should not allow special characters" ControlToValidate="txt_serialno" ValidationExpression="[^%$#@!~`(*&^%+_=|\/?<>;]*" Display="none"  ValidationGroup="Group1"></asp:RegularExpressionValidator >
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Type%></label>
                                      <div class="col-sm-8 form-inline"><asp:DropDownList ID="dt_Type" runat="server" DataTextField="Type" 
        DataValueField="TypeID" Width="160px" EnableTheming="True"></asp:DropDownList>
    <asp:TextBox ID="txt_type" runat="server" AutoCompleteType="Disabled" 
        EnableTheming="True" MaxLength="100" Visible="false" Width="125px"></asp:TextBox>
     <asp:LinkButton ID="imagetype" runat="server" OnClick="imagetype_Click" 
         SkinID="BtnLinkAdd"/>
<asp:LinkButton ID="itype_submitt" runat="server" Visible="false" 
        ValidationGroup="Group4" 
        SkinID="BtnLinkUpdate" onclick="itype_submitt_Click"   />
                  <asp:LinkButton ID="itype_cancel" runat="server" Visible="false" 
        OnClick="itype_cancel_Click"  SkinID="BtnLinkCancel" />
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Make%></label>
                                      <div class="col-sm-8 form-inline"><asp:DropDownList ID="dt_make" runat="server"  EnableTheming="True" 
        DataTextField="Make" DataValueField="MakeID" Width="160px"></asp:DropDownList>
<asp:RequiredFieldValidator ID="R8" 
        runat="server" ErrorMessage="Please enter make" Display="None" 
        ControlToValidate="txtmkae" ValidationGroup="Group2"
       ></asp:RequiredFieldValidator>
    <asp:TextBox ID="txtmkae" runat="server" Visible="false" Width="125px" 
        AutoCompleteType="Disabled" MaxLength="100" EnableTheming="True" ></asp:TextBox>
   <asp:LinkButton ID="imagemake" runat="server" OnClick="imagemake_Click" 
         SkinID="BtnLinkAdd" />
<asp:LinkButton ID="i_makesubmitt" runat="server" Visible="false" 
       ValidationGroup="Group2" 
        SkinID="BtnLinkUpdate" onclick="i_makesubmitt_Click"  />
                          <asp:LinkButton ID="i_makecancel" runat="server" 
        Visible="false" OnClick="i_makecancel_Click" 
        SkinID="BtnLinkCancel"  />
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Model%></label>
                                      <div class="col-sm-8 form-inline"><asp:DropDownList ID="dt_Model" runat="server"  EnableTheming="True" 
        DataTextField="Model" DataValueField="ModelID" Width="160px"></asp:DropDownList>

    <asp:RequiredFieldValidator ID="R9" runat="server" 
        ControlToValidate="txt_model" Display="None" ErrorMessage="Please enter model" 
        ValidationGroup="Group3"></asp:RequiredFieldValidator>

  <asp:TextBox ID="txt_model" runat="server" Visible="false" Width="125px" 
        AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox>
   <asp:LinkButton ID="imagemodel" runat="server" OnClick="imagemodel_Click" 
         SkinID="BtnLinkAdd"/>
<asp:LinkButton ID="imodel_submitt" runat="server" Visible="false" 
        ValidationGroup="Group3" 
        SkinID="BtnLinkUpdate" onclick="imodel_submitt_Click" />
                   <asp:LinkButton ID="imodel_cancel" runat="server" 
        Visible="false" OnClick="imodel_cancel_Click" 
        SkinID="BtnLinkCancel" />
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.RequestedMoveDate%></label>
                                      <div class="col-sm-8 form-inline"><asp:TextBox ID="txt_commision" runat="server" AutoCompleteType="Disabled" 
                MaxLength="10" SkinID="Date"></asp:TextBox>
            <asp:Label ID="Image1" runat="server" SkinID="Calender" />
            <asp:RegularExpressionValidator ID="RM1" runat="server" 
                ControlToValidate="txt_commision" Display="None"
                ErrorMessage="Please enter Date in dd/mm/yyyy format" 
                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" 
                ValidationGroup="AddedAssets"></asp:RegularExpressionValidator>
                
                <asp:RegularExpressionValidator ID="ReES1" runat="server" 
                ControlToValidate="txt_commision" Display="None"
                ErrorMessage="Please enter Date in dd/mm/yyyy format" 
                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" 
                ValidationGroup="Group9"></asp:RegularExpressionValidator>
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ScheduleMoveDate%></label>
                                      <div class="col-sm-8 form-inline"> <asp:TextBox ID="txt_DateMoved" runat="server" AutoCompleteType="Disabled" 
                EnableTheming="True" MaxLength="10" SkinID="Date"></asp:TextBox>
            <asp:Label ID="img1" runat="server" SkinID="Calender" />
            <asp:RegularExpressionValidator ID="RM2" runat="server" 
                ControlToValidate="txt_DateMoved"  Display="None"
                ErrorMessage="Please enter Date in dd/mm/yyyy format" 
                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" 
                ValidationGroup="AddedAssets"></asp:RegularExpressionValidator>
                
                <asp:RegularExpressionValidator ID="RegDS1" runat="server" 
                ControlToValidate="txt_DateMoved"  Display="None"
                ErrorMessage="Please enter Date in dd/mm/yyyy format" 
                ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" 
                ValidationGroup="Group9"></asp:RegularExpressionValidator>
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Owner%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txt_Owner" runat="server" AutoCompleteType="Disabled" 
                MaxLength="50"  ></asp:TextBox>
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Site%></label>
                                      <div class="col-sm-8 form-inline"> <asp:DropDownList ID="dt_Site" runat="server" DataTextField="Site" EnableTheming="True" 
                DataValueField="ID" SkinID="ddl_80">
            </asp:DropDownList>
            <asp:LinkButton ID="imagelocation" runat="server" SkinID="BtnLinkAdd" 
                onclick="imagelocation_Click" CausesValidation="false" />
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Building%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txt_Bulding" runat="server" AutoCompleteType="Disabled" 
                MaxLength="50"  ></asp:TextBox>
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.DeskorLocation%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txt_Cab" runat="server" AutoCompleteType="Disabled" 
                MaxLength="50"  ></asp:TextBox>
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.DataPort%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="txt_DataPort" runat="server" AutoCompleteType="Disabled" 
                MaxLength="50"  ></asp:TextBox>
					</div>
				</div>
</div>

                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Floor%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txt_Floor" runat="server" AutoCompleteType="Disabled" 
                EnableTheming="True" MaxLength="50"  ></asp:TextBox>
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Room%></label>
                                      <div class="col-sm-8"> <asp:TextBox ID="txt_Room" runat="server" AutoCompleteType="Disabled" 
                MaxLength="50"  ></asp:TextBox>
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.IPAddress%></label>
                                      <div class="col-sm-8">
                                           <asp:TextBox ID="sourceIPAdress" runat="server" AutoCompleteType="Disabled" 
                EnableTheming="True" MaxLength="50"  ></asp:TextBox>
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.SubNet%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="sourcetxtsubnet" runat="server" AutoCompleteType="Disabled" 
                EnableTheming="True" MaxLength="50"  ></asp:TextBox>
					</div>
				</div>
</div>
                                    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.VLan%></label>
                                      <div class="col-sm-8"><asp:TextBox ID="sourceVLAN" runat="server" AutoCompleteType="Disabled" 
                EnableTheming="True" MaxLength="50"  ></asp:TextBox>
					</div>
				</div>
</div>
                                    
                                    </div>
                                  <div class="col-md-6">
                                      <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.DestinationDetails%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>

                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.NewOwner%></label>
                                      <div class="col-sm-5"> <asp:TextBox ID="NewOwner" runat="server" EnableTheming="True"   AutoCompleteType="Disabled" MaxLength="50" ></asp:TextBox>
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.Site%></label>
                                      <div class="col-sm-5 form-inline"><asp:DropDownList ID="dt_deatinationSite" runat="server"  DataTextField="Site" DataValueField="ID" EnableTheming="True"  Width="160px"></asp:DropDownList>
    <asp:LinkButton ID="btn_add1" runat="server" 
        SkinID="BtnLinkAdd"   />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.Building%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txt_tobuilding" runat ="server"   AutoCompleteType="Disabled" MaxLength="50" />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.DeskorLocation%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txt_ToDesklocation" runat="server"   AutoCompleteType="Disabled" MaxLength="50" />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.DataPort%></label>
                                      <div class="col-sm-5"> <asp:TextBox ID="txt_toDataport" runat="server"   AutoCompleteType="Disabled" MaxLength="50" />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.Floor%></label>
                                      <div class="col-sm-5"> <asp:TextBox ID="txt_tofloor" runat="server"   AutoCompleteType="Disabled" MaxLength="50" />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.Room%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txt_toRoom" runat="server"   
        AutoCompleteType="Disabled" MaxLength="50" 
        />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.IPAddress%></label>
                                      <div class="col-sm-5"> <asp:TextBox ID="txtipadress" runat="server"   AutoCompleteType="Disabled" MaxLength="50"/>
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.SubNet%></label>
                                      <div class="col-sm-5"> <asp:TextBox ID="txtsubnet" runat="server"   AutoCompleteType="Disabled" MaxLength="50" />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.VLan%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txtvlan" runat="server"   AutoCompleteType="Disabled" MaxLength="50"/>
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.Notes%></label>
                                      <div class="col-sm-5"><asp:TextBox ID="txt_Notes"  SkinID="txtMulti" runat="server"  EnableTheming="True" AutoCompleteType="Disabled"></asp:TextBox>
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> </label>
                                      <div class="col-sm-5"><asp:HiddenField ID="HiddenField7" runat="server" />
                                          <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="Image1" TargetControlID="txt_commision"  CssClass="MyCalendar">
          </cc1:CalendarExtender>
          <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  PopupButtonID="img1" TargetControlID="txt_DateMoved" CssClass="MyCalendar"></cc1:CalendarExtender>
                                          <asp:HiddenField ID="HiddenField6" runat="server" />
					</div>
				</div>
                </div>
                                      <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.ProjectTitle%></label>
                                      <div class="col-sm-5">
					</div>
				</div>
                </div>

                                    </div>
                                    
                                 </div>

<div class="form-group">
       
            <div class="col-md-12">

                                        <div>Image</div>
    <div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
      </div>

                                        <div id="ImageGet" runat="server" visible="false">

  <asp:GridView ID="GetImage" runat="server" AutoGenerateColumns="false" 
          PageSize="1" onrowdatabound="GetImage_RowDataBound" Width="70%">
  <Columns>
      <asp:TemplateField HeaderText="Image preview" >
                    <ItemTemplate>
              
                       <%-- <asp:Image id="img1" runat="server"  ImageUrl='<%# "~/AssetsImageHandlers.ashx?id="+Eval("ID")%>' />--%>
                      
                        <ajaxToolkit:AnimationExtender id="MyExtender"
          runat="server" TargetControlID="pnlOriginalImage" >
          <Animations>
           <OnMouseOver>
            <FadeIn Duration=".5" Fps="20" />
            </OnMouseOver>
          </Animations>
</ajaxToolkit:AnimationExtender>
            <ajaxToolkit:HoverMenuExtender ID="hmeDetails" runat="server" 
                            TargetControlID="imgContractor" 
                            PopupControlID="pnlOriginalImage" 
                            PopDelay="0"
                            PopupPosition="Left" 
                            EnableViewState="false"
                            OffsetY="26" />
                            <asp:Image ID="imgContractor" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.MediumSmaller) %>'  Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>'/>
                    
            <div id="pnlOriginalImage" runat="server" class=""  style="display: none;">

            <asp:Image ID="Image1" runat="server" ImageUrl='<%# GetImageUrl((Guid)DataBinder.Eval(Container.DataItem,"Image"),ImageManager.ThumbnailSize.OriginalData) %>' Visible='<%# CheckImageVisibility((Guid)DataBinder.Eval(Container.DataItem,"Image"))%>' />
            </div>
                    </ItemTemplate>
                </asp:TemplateField>
  </Columns>
  </asp:GridView>
  
                                             <div>Documents</div>
  <div>
    <asp:FileUpload ID="FileUpload2" runat="server"  class="multi"  />
  </div>
  </div>
                                        <div id="GetDocmentID" runat="server" visible="false">
    <asp:GridView ID="GridView3" runat="server"  BorderWidth="0" 
          AutoGenerateColumns="false" onrowcommand="GridView3_RowCommand" Width="70%" >
            <Columns>
                <asp:BoundField DataField="DocumentName" HeaderText="Document" HeaderStyle-CssClass="header_bg_l" />
                  
                   <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="Link1" runat="server" Text='<%# Bind("AssetID") %>'
                             >
                        </asp:LinkButton>
                    </ItemTemplate>
                    </asp:TemplateField>
                  
               <asp:TemplateField>
                    <ItemTemplate>
                     <asp:LinkButton ID="LinkButtonDocDelete" SkinID="BtnLinkDownload" runat="server" CommandName="DownLoad" CommandArgument='<%# Bind("ID") %>' />
                        <%--<asp:LinkButton ID="" runat="" CommandName="" Text="DownLoad" CommandArgument='<%# Bind("ID") %>'
                             ToolTip="DownLoad" >
                        </asp:LinkButton>--%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderStyle-CssClass="header_bg_r" >
                    <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonDelete" SkinID="BtnLinkDelete" runat="server" CommandName="Remove" CommandArgument='<%# Bind("ID") %>' />
                        <%--<asp:LinkButton ID="LinkButtonDelete" runat="server" CommandName="" Text="Remove" CommandArgument='<%# Bind("ID") %>'
                             ToolTip="Remove" >
                        </asp:LinkButton>--%>
                    </ItemTemplate>
                    </asp:TemplateField>
                    
                    
            </Columns>
            </asp:GridView>
  </div>
                                    </div>
      
    </div>
	
	<div class="form-group">
        <div class="col-md-12 form-inline">
         <asp:Button ID="Imgupdation" runat="server" 
         SkinID="btnSubmit"  ValidationGroup="AddedAssets"
        onclick="Imgupdation_Click" />
<asp:Button ID="ImgBtnUpdate" runat="server" Visible="false" 
        ValidationGroup="Group9" 
        SkinID="btnUpdate" onclick="ImgBtnUpdate_Click"  />
        <asp:Button ID="ImgBtnCancel" runat="server" Visible="false" 
        SkinID="btnCancel" onclick="ImgBtnCancel_Click" />
        </div>
	</div>
	
	<div class="form-group"><asp:LinkButton id="btn_Add" runat="server" SkinID="BtnLinkAdd" 
        onclick="btn_Add_Click"  ValidationGroup="AddedAssets"/>
<asp:LinkButton ID="btn_Update" runat="server" SkinID="BtnLinkUpdate" 
        onclick="btn_Update_Click" ValidationGroup="Group9" Visible="false" />
        <asp:LinkButton ID="btnCancel" runat="server" Visible="false" 
        SkinID="BtnLinkCancel" onclick="ImgBtnCancel_Click" />
<asp:Button id="btn_copy" runat="server" SkinID="btnDefault" Text="Copy" 
        onclick="btn_copy_Click" />
        
       
        </div>

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="ID" onrowcancelingedit="GridView1_RowCancelingEdit" 
            onrowcommand="GridView1_RowCommand" onrowediting="GridView1_RowEditing" 
             width="100%">
            <Columns>
                <asp:BoundField DataField="AssetsID" Visible="False" />
                <asp:TemplateField HeaderStyle-CssClass="header_bg_l">
                    <ItemStyle Width="40px" Wrap="True" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" 
                            CommandArgument='<%# Bind("ID")%>' CommandName="Select" 
                            SkinID="BtnLinkEdit" ToolTip="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" Visible="False" />
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                    <ItemTemplate>
                        <asp:Label ID="LinkNew" runat="server" 
                            Visible='<%# getVisible1( DataBinder.Eval(Container.DataItem, "NewAsset").ToString())%>'></asp:Label>
                        <asp:Label ID="imgshow" runat="server" ForeColor="red" Text="New" 
                            Visible='<%# getVisible( DataBinder.Eval(Container.DataItem, "NewAsset").ToString())%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Owner">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="lblowner" runat="server" Text='<%# Bind("FromOwner") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Asset number">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="lblAssetnum" runat="server" Text='<%# Bind("AssetNo") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Serial number">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="lblSerialnum" runat="server" Text='<%# Bind("SerialNo") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="hdr_orange" HeaderText="To Site" 
                    SortExpression="DestinationSite1" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblDestinationSite" runat="server" 
                            Text='<%# ValidateData( DataBinder.Eval(Container.DataItem, "ToSite").ToString())%>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="lblAssettype" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Make" 
                    SortExpression="Make">
                    <ItemTemplate>
                        <asp:Label ID="lblmake" runat="server" Text='<%# Bind("Make") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Model" 
                    SortExpression="Model">
                    <ItemTemplate>
                        <asp:Label ID="lblmodel" runat="server" Text='<%# Bind("Model") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Desk/Location" 
                    >
                    <ItemTemplate>
                        <asp:Label ID="lbllocation" runat="server" Text='<%# Bind("FromLocation") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="Data Port" 
                    SortExpression="FromPort">
                    <ItemTemplate>
                        <asp:Label ID="lblport" runat="server" Text='<%# Bind("FromPort") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="hdr_orange" HeaderText="New Owner" 
                    Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblNewOwner" runat="server" Text='<%# Bind("ToOwner") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="hdr_orange" HeaderText="To DataPort" 
                    Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblToDataPort" runat="server" Text='<%# Bind("ToPort") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderStyle-CssClass="hdr_orange" HeaderText="To Building" 
                    Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblToBuilding" runat="server" Text='<%# Bind("ToBuilding") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                    <%--   <EditItemTemplate>
   <asp:TextBox ID="txt_ToBuilding" runat="server"  Text='<%# Bind("ToBuilding") %>' Width="120px" ></asp:TextBox>
   </EditItemTemplate>--%></asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="hdr_orange" HeaderText="To Floor" 
                    Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblToFloor" runat="server" Text='<%# Bind("ToFloor") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                    <%-- <EditItemTemplate>
   <asp:TextBox ID="txt_ToFloor" runat="server" Text='<%# Bind("ToRoom") %>' Width="120px" ></asp:TextBox>
   </EditItemTemplate>--%></asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="hdr_orange" HeaderText="To Room" 
                    Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblToRoom" runat="server" Text='<%# Bind("ToRoom") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                    <%--  <EditItemTemplate>
   <asp:TextBox ID="txt_ToRoom" runat="server" Text='<%# Bind("ToRoom") %>' Width="120px"></asp:TextBox>
   </EditItemTemplate>--%></asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="hdr_orange" HeaderText="To IP Address" 
                    Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblToIPAddress" runat="server" Text='<%# Bind("ToIPAddress") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    <%--   <EditItemTemplate>
   <asp:TextBox ID="txt_ToIPAddress" runat="server" Text='<%# Bind("ToIPAddress") %>' Width="80px" ></asp:TextBox>
   </EditItemTemplate>--%></asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="hdr_orange" HeaderText="To Subnet" 
                    Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblToSubnet" runat="server" Text='<%# Bind("ToSubnet") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    <%--   <EditItemTemplate>
   <asp:TextBox ID="txt_ToSubnet" runat="server" Text='<%# Bind("ToSubnet") %>' Width="80px" ></asp:TextBox>
   </EditItemTemplate>--%></asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="hdr_orange" HeaderText="To VLan" 
                    Visible="false">
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    <ItemTemplate>
                        <asp:Label ID="lblToVlan" runat="server" Text='<%# Bind("ToVLAN") %>'></asp:Label>
                    </ItemTemplate>
                    <%--    <EditItemTemplate>
   <asp:TextBox ID="txt_ToVlan" runat="server" Text='<%# Bind("ToVLAN") %>' Width="80px" ></asp:TextBox>
   </EditItemTemplate>--%></asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="header_bg_r" HeaderText="Notes">
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    <ItemTemplate>
                        <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("ToNotes") %>' />
                    </ItemTemplate>
                    <%--<EditItemTemplate>
  <asp:TextBox ID="txt_Notepad" runat="server" TextMode="MultiLine" Text='<%# Bind("ToNotes") %>' Width="120px"  ></asp:TextBox>
  </EditItemTemplate>  --%></asp:TemplateField>
            </Columns>
        </asp:GridView>
        

<div class="clr"></div>
	<div>
	  <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <asp:HiddenField ID="HiddenField4" runat="server" />
    <asp:HiddenField ID="HiddenField5" runat="server" />
    <asp:HiddenField ID="HiddenField3" runat="server" />
	</div>
	
	<br />
	
	
	

  