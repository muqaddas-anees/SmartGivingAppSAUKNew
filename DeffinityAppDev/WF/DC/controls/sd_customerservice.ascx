<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Servicedesk_sdcontrols_sd_customerservice1" Codebehind="sd_customerservice.ascx.cs" %>
  
    <%@ Register src="~/WF/DC/MailControls/SDCustomerApproveMail.ascx" tagname="SDCustomerApproveMail" tagprefix="uc1" %>
    <%@ Register src="~/WF/DC/MailControls/SDCustomerDeclineMail.ascx" tagname="SDCustomerDeclineMail" tagprefix="uc2" %>
     <%@ Register Src="~/WF/DC/controls/CustomerOrder.ascx" TagName="CustomerOrder"
    TagPrefix="uc3" %>
      <%@ Register src="~/WF/DC/MailControls/CustomerOrderToSDteam.ascx" tagname="CustomerOrderToSDteam" tagprefix="uc4" %>
     <uc1:SDCustomerApproveMail ID="SDCustomerApproveMail1" runat="server" Visible="false" />
     <uc2:SDCustomerDeclineMail ID="SDCustomerDeclineMail1" runat="server" Visible="false" />
     <uc4:CustomerOrderToSDteam  ID="CustomerOrderToSDteam1" runat="server" Visible="false"/>

     <asp:Label ID="lblcstatus" runat="server" ForeColor="Gray" Font-Size="Small" Font-Bold="true" ></asp:Label>
     <asp:Panel ID="pnlCustomer"  runat="server" >

     <asp:Panel ID="pnlOrder" runat="server" Width="100%">
    <uc3:CustomerOrder ID="CustomerOrder1" runat="server" />
    
    </asp:Panel>
    <br />
    <div class="clr"></div>
    <br />
    <div class="clr"></div>
  
     <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 90%;">
                            Services</div>
    <div>
<asp:GridView ID="Grid_services" runat="server" AutoGenerateColumns="False" OnRowCommand="Grid_services_RowCommand"
    Width="100%" OnRowUpdated="Grid_services_RowUpdated" OnRowUpdating="Grid_services_RowUpdating"
    OnRowCancelingEdit="Grid_services_RowCancelingEdit" OnRowEditing="Grid_services_RowEditing">
    <Columns>
        
        <asp:TemplateField HeaderText="Service Name">
         <HeaderStyle  CssClass="header_bg_l" />
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="20%" />
            <%--<ItemStyle Width="250px" />--%>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Notes">
         
            <ItemTemplate>
                <asp:Label ID="lblgridNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle Width="20%" />
            <%--<ItemStyle Width="250px" />--%>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="QTY">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
            </ItemTemplate>
           
            <ItemStyle Width="8%" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Unit Price">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("SellingPrice","{0:F2}" ) %>'></asp:Label>
            </ItemTemplate>
            
            <ItemStyle Width="10%" HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total" HeaderStyle-CssClass="header_bg_r">
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# Bind("Total","{0:F2}") %>'></asp:Label>
            </ItemTemplate>
            
            <ItemStyle Width="15%" HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Units" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lbluc" runat="server" Text='<%# Bind("UnitConsumption","{0:F2}" ) %>'></asp:Label>
            </ItemTemplate>
            
            <ItemStyle Width="10%" HorizontalAlign="Right" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total Units" Visible="false" >
            <ItemTemplate>
                <asp:Label ID="lbltu" runat="server" Text='<%# Bind("TotalUnits","{0:F2}") %>'></asp:Label>
            </ItemTemplate>
            
            <ItemStyle Width="15%" HorizontalAlign="Right" />
        </asp:TemplateField>
        
    </Columns>
</asp:GridView>

<asp:ObjectDataSource ID="obj_services" runat="server" SelectMethod="Services_SelectByIncidentID"
    TypeName="Deffinity.IncidentService.ServiceManager" OnUpdated="obj_services_Updated"
    UpdateMethod="Services_Update" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="0" Name="IncidentID" SessionField="IncidentID"
            Type="Int32" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="ID" />
        <asp:Parameter Name="Description" />
        <asp:Parameter Name="QTY" />
        <asp:Parameter Name="SellingPrice" />
    </UpdateParameters>
</asp:ObjectDataSource>

</div>
<asp:Panel ID="pnlService" runat="server">
<div style="clear:both;padding-bottom:10px">

</div>
<div style="clear:both;padding-bottom:10px"></div>
    <div>
                
    <table width="80%" border="0" cellpadding="0" cellspacing="0">
     <asp:Panel ID="pnlStatus" runat="server" Visible="false"> 
    <tr>
    <td style="width:30%">
       Customer Approval Status:
         </td><td><label id="lblStatus" runat="server" style="font-weight:bold;"></label></td>
    </tr>
    </asp:Panel>
    <tr>
    <td style="width:20%">Total Price:</td><td><label id="lblTotalPrice" runat="server" style="font-weight:bold;"></label> </td>
    <td style="width:75%">&nbsp;&nbsp;</td>
    </tr>
    <tr>
    <td>Discount %:</td><td><label id="lblDiscountPer" runat="server" style="font-weight:bold;"></label> </td>
    </tr>
    <tr>
    <td>Discount Value:</td><td><label ID="lblDiscountValue" runat="server"  style="font-weight:bold;"></label></td>
    </tr>
    <tr>
    <td>Revised Price:</td><td><label ID="lblRevisedPrice" runat="server" style="font-weight:bold;" ></label></td>
    </tr>
     <tr>
    <td><label id="lblUnit_title" runat="server">Unit Consumption: </label></td><td><label id="lbluc" runat="server" style="font-weight:bold;"></label></td>
    </tr>
    <tr>
    <td style="vertical-align:top;padding-top:4px" >Notes:</td><td style="width:75%"><label ID="lblNotes" runat="server"  style="font-weight:bold;Width:400px;Height:60px"></label>
    <%--<uc3:HtmlEditor ID="txtNotes" runat="server" Width="600" Height="125" />--%>
    </td>
    </tr>
    <tr>
    <td><asp:Label ID="lblPonumber" runat="server" Text="PO Number"></asp:Label> </td>
    <td><asp:TextBox ID="txtPONumber" runat="server"></asp:TextBox> </td>
    </tr>
    <tr>
    <td></td>
        <td><asp:CheckBox ID="chek_confirm"  runat="server" Text=" I accept this order and agree to the standard terms and conditions" Font-Bold="true" /> </td>
    </tr>
    <tr>
    <td></td>
    <td>
     <asp:Button ID="btnProcessorder" runat="server" 
          Text="Accept" onclick="btnProcessorder_Click" SkinID="btnDefault"/>
        <asp:Button ID="btnDeclain" runat="server" Text="Decline" 
            onclick="btnDeclain_Click" SkinID="btnDefault"/> 
             <asp:Button Text="Request Quote" ID="btnRequestQuote" SkinID="btnDefault" runat="server" onclick="btnRequestQuote_Click" />
           </td>
    </tr>
    </table>
    </div>
    </asp:Panel>
    </asp:Panel>