<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" CodeBehind="DCMaintenance.aspx.cs" Inherits="DeffinityAppDev.WF.DC.DCMaintenance" %>

<%@ Register Src="~/WF/DC/controls/FLSTab.ascx" TagPrefix="Pref" TagName="FLSTab" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="server">
    <%= Resources.DeffinityRes.ServiceDesk%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_description" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="server">
     <Pref:FLSTab runat="server" ID="FLSTab" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="server">
    Maintenance
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="server">
     <asp:Button ID="btnRaiseInvoice" runat="server" Text="Save" />
     <a id ="link_return" visible="false" href="~/WF/DC/FLSJlist.aspx?type=FLS" runat="server" target="_self"><i class="fa fa-arrow-left"></i> Return to <%= Resources.DeffinityRes.ServiceDesk%></a>

</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hplanid" runat="server" Value="0" />
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


</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="Scripts_Section" runat="server">
</asp:Content>
