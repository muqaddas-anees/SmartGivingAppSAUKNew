<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ProjectOverviewCtrl" Codebehind="ProjectOverviewCtrl.ascx.cs" %>
<div>
<asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
</div>
<asp:Panel ID="pnlProjectdetails" runat="server">
    <asp:UpdateProgress ID="uProgress" runat="server">
        <ProgressTemplate>
            <asp:Label SkinID="Loading" runat="server"></asp:Label>
        </ProgressTemplate>
    </asp:UpdateProgress>
   <asp:UpdatePanel ID="upnl" runat="server">
       <ContentTemplate>

     
         <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ProjectTitle%></label>
                                      <div class="col-sm-8">
                                          <asp:TextBox ID="txtProjectTitle" runat="server" Width="350px"></asp:TextBox>
      <asp:RequiredFieldValidator ID="rfv_title" runat="server" 
          ControlToValidate="txtProjectTitle" Display="None" 
          ErrorMessage="Please enter project title"  ValidationGroup="project"></asp:RequiredFieldValidator>
                                          </div>
                                    </div>
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Customer%></label>
                                       <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlCustomer" runat="server" 
  DataSourceID="SqlDataSourceTitle2" DataTextField="PortFolio" DataValueField="ID"></asp:DropDownList> 
   <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_PermissionCustomer" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
      <asp:RequiredFieldValidator ID="rfv_customer" runat="server" 
          ControlToValidate="ddlCustomer" Display="None" 
          ErrorMessage="<%$ Resources:DeffinityRes,PlsSelectCustomer%>" InitialValue="0"  ValidationGroup="project"></asp:RequiredFieldValidator>
                                           </div>
                                    </div>
                                </div>

         <div class="form-group" runat="server" id="DivProgrammeSubProgramme">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.Programme%></label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlProgramme" runat="server"  DataSourceID="SqlDataSource8"
                DataTextField="OperationsOwners" DataValueField="ID" AutoPostBack="true" 
                onselectedindexchanged="ddlProgramme_SelectedIndexChanged">
            </asp:DropDownList>
             <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_AssignedProgramme" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.SubProgramme%> </label>
                                      <div class="col-sm-8">
                                             <asp:DropDownList ID="ddlSubprogramme" runat="server" >
           <%-- <asp:DropDownList ID="ddlSubprogramme" runat="server" Width="300px"  DataSourceID="SqlDataSourcesubprogram" 
             DataValueField="ID" DataTextField="OPERATIONSOWNERS">--%>
            </asp:DropDownList>
          <%--   <asp:SqlDataSource ID="SqlDataSourcesubprogram" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                        SelectCommand="Project_AssignedSubProgramme" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                            <asp:ControlParameter ControlID="ddlProgramme" DefaultValue="0" 
                                        Name="PROGRAMMEID" PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>--%>
					</div>
				</div>
</div> 

         <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Status%></label>
                                      <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlStatus" runat="server" > </asp:DropDownList>
            <asp:LinkButton ID="btnClassApply" runat="server" OnClick="btnClassApply_Click" CssClass="col-sm-1" ></asp:LinkButton>
         <div runat="server" ID="btnClassApply_d" title="Project Class"  Style="display: none"></div>

					</div>
				</div>
             <div class="col-md-6">
                 <div class="col-md-4"></div>
                 <div class="col-md-8"><asp:Button ID="btnCreate" runat="server" 
                onclick="btnCreate_Click" SkinID="btnOrange" Text="Submit" ValidationGroup="project" />
                                       <ajaxToolkit:ModalPopupExtender ID="modalClassPnl" runat="server" 
                BackgroundCssClass="modalBackground" CancelControlID="btnClassClose" 
                PopupControlID="PanelClass" TargetControlID="btnClassApply_d"> </ajaxToolkit:ModalPopupExtender></div>
                                      
           
				</div>
 
</div>

         <div class="form-group" runat="server" id="pnlAssignTemplate">
                           <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.AssignChecklist%></label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlMasterTemplate" runat="server" DataSourceID="objectDS_Templates" DataTextField="Description" DataValueField="ID" >
        </asp:DropDownList> <asp:ObjectDataSource ID="objectDS_Templates" runat="server" OldValuesParameterFormatString="original_{0}"
                                TypeName="Deffinity.Bindings.DefaultDatabind" SelectMethod="b_ChecklistbyType" >
                                <SelectParameters>
                                <asp:Parameter Name="ChecklistType" Type="Int32" DefaultValue="1" />         
                                </SelectParameters>
                                </asp:ObjectDataSource> 
                                          
					</div>
				</div>       
 <div class="col-md-6">
                                      
                        <asp:Button ID="btnAssign" runat="server" Text="Assign" OnClick="btnAssign_Click" ToolTip="Assign" />
				</div>
</div>
         </ContentTemplate>
       <Triggers>
           <asp:PostBackTrigger ControlID="btnAssign" />
           <asp:PostBackTrigger ControlID="btnCreate" />
            <asp:PostBackTrigger ControlID="btnClassApply" />
       </Triggers>
   </asp:UpdatePanel>

  <div><asp:Panel runat="server" ID="PanelClass" Width="850" Height="400px" Style="display: none" BackColor="White">
<asp:Panel ID="pnlClassHeader" runat="server" Width="850"  BackColor="White">
    
<div class="row">
          <div class="col-md-10">
 <strong><%= Resources.DeffinityRes.ProjectClass%> </strong> 
<hr class="no-top-margin" />
	</div>
     <div class="col-md-2"><asp:LinkButton runat="server" ID="btnClassClose" SkinID="BtnLinkCancel" />
         </div>
</div>


</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Width="848px" Height="370px" ScrollBars="Auto"  BackColor="White">
<asp:UpdatePanel ID="upClass" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<asp:GridView ID="gridProjectClass" runat="server" 
        onrowcommand="gridProjectClass_RowCommand" Width="100%" 
        onrowcancelingedit="gridProjectClass_RowCancelingEdit" 
        onrowediting="gridProjectClass_RowEditing" 
        onrowupdated="gridProjectClass_RowUpdated" 
        onrowupdating="gridProjectClass_RowUpdating">
<Columns>
 <asp:TemplateField ShowHeader="False"  HeaderStyle-CssClass="header_bg_l">
                    <EditItemTemplate>
                        <asp:LinkButton ID="btnUpdate" runat="server" 
                            CommandName="Update"  SkinID="BtnLinkUpdate"
                               CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="False" 
                            CommandName="Cancel" 
                           SkinID="BtnLinkCancel"></asp:LinkButton>
                    
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSelect" runat="server" CausesValidation="False" Enabled="<%#CommandField()%>"
                            CommandName="Edit" 
                            SkinID="BtnLinkEdit"  ></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="55px" HorizontalAlign="Center" />
                </asp:TemplateField>
    <asp:templatefield HeaderText="<%$ Resources:DeffinityRes,ItemDescription%>" >
        <itemtemplate>
                <asp:label id="lblItemDesc" runat="server" text='<%# Eval("ItemDesc") %>' />
        </itemtemplate>
        <EditItemTemplate>
            <asp:label id="lbleItemDesc" runat="server" text='<%# Eval("ItemDesc") %>' />
        </EditItemTemplate>
</asp:templatefield>
    <asp:TemplateField>
        <ItemTemplate>
            <asp:LinkButton ID="btnAudit" runat="server" CommandName="Audited" Text="Audited" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
        </ItemTemplate>
    </asp:TemplateField>
 <asp:templatefield HeaderText="<%$Resources:DeffinityRes,DateAudited%>"  >
        <itemtemplate>
                <asp:label id="lblDate" runat="server" text='<%# Eval("dateAudited", "{0:d}") %>' />
        </itemtemplate>
        <EditItemTemplate>
            <asp:label id="lbleDate" runat="server" text='<%# Eval("dateAudited", "{0:d}") %>' />
        </EditItemTemplate>
</asp:templatefield>
<asp:templatefield HeaderText="<%$Resources:DeffinityRes,Auditedby%>" >
        <itemtemplate>
                <asp:label id="lblAuditedBy" runat="server" text='<%# Eval("AuditedBy") %>' />
        </itemtemplate>
        <EditItemTemplate>
            <asp:label id="lbleAuditedBy" runat="server" text='<%# Eval("AuditedBy") %>' />
        </EditItemTemplate>
</asp:templatefield>
<asp:templatefield HeaderText="<%$Resources:DeffinityRes,Notes%>">
        <itemtemplate>
                <asp:label id="lblNotes" runat="server" text='<%# Eval("Notes") %>' />
        </itemtemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtNotes" runat="server" Width="250px" Text='<%# Eval("Notes") %>'></asp:TextBox>
        </EditItemTemplate>
</asp:templatefield>
</Columns>
</asp:GridView>
</ContentTemplate>
<Triggers>

</Triggers>
</asp:UpdatePanel>
<asp:UpdateProgress AssociatedUpdatePanelID="" ID="upProgress" runat="server">
<ProgressTemplate>
<asp:Label ID="imgloding" runat="server" SkinID="Loading" />
</ProgressTemplate>
</asp:UpdateProgress>
</asp:Panel>
</asp:Panel></div>
  </asp:Panel>


