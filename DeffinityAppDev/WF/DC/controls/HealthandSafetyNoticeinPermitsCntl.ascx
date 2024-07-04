<%@ Control Language="C#" AutoEventWireup="true" Inherits="DC_controls_HealthandSafetyNoticeinPermitsCntl" Codebehind="HealthandSafetyNoticeinPermitsCntl.ascx.cs" %>
<%--<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>--%>
 <script>
     $(document).ready(function () {
         $("#lblmsg").fadeOut(5000)
     });
</script>

<div class="form-group row">
        <div class="col-md-12 text-bold">
             <strong>  Health and Safety Notice in Permits</strong>
            <hr class="no-top-margin" />
            </div>
</div>
<div class="row">
          <div class="col-md-12">
              <asp:Label ID="lblmsg" runat="server" ClientIDMode="Static"></asp:Label>
	</div>
</div>
<div class="form-group row">
          <div class="col-md-12">
 <label class="col-sm-4 control-label">Select customer</label>
           <div class="col-sm-8 form-inline">
                <asp:DropDownList ID="ddlcustomer"  DataTextField="PortFolio" DataSourceID="SqlDataSourceTitle2" AutoPostBack="true"
                     DataValueField="ID" runat="server" Width="180px" OnSelectedIndexChanged="ddlcustomer_SelectedIndexChanged"></asp:DropDownList>
                  <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_PermissionCustomer" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
               <asp:RequiredFieldValidator ID="req1" runat="server" InitialValue="0"
                     ControlToValidate="ddlcustomer" ForeColor="Red" ErrorMessage="Please select customer."></asp:RequiredFieldValidator>
            </div>
	</div>
</div>

<div class="row">
          <div class="col-md-12">
               <asp:TextBox runat="server"
        ID="editfooter" 
        TextMode="MultiLine" 
        Width="600px" Height="400px"/>
                <ajaxToolkit:HtmlEditorExtender 
        ID="htmlEditorExtender1" 
        TargetControlID="editfooter" 
        runat="server" >
    </ajaxToolkit:HtmlEditorExtender>
	</div>
</div>
<div class="row">
          <div class="col-md-12">
              <asp:CheckBox ID="CheckPermit" runat="server" Text="Display Health and Safety Notice in Customer View" />
	</div>
</div>
<div class="row">
          <div class="col-md-12 form-inline">
               <asp:Button ID="imgbtnsubmit" runat="server" SkinID="btnSubmit" Text="Submit" OnClick="imgbtnsubmit_Click" />
         <asp:Button ID="btnCopyToAllCustomer" runat="server"  SkinID="btnCopytoAllCustomers" OnClick="btnCopyToAllCustomer_Click" />
	</div>
</div>
