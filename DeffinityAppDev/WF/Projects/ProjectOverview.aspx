<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="ProjectOverview1" MaintainScrollPositionOnPostback="true" Codebehind="ProjectOverview.aspx.cs" %>

<%-- --%>
<%@ Register Src="~/WF/MailControls/ProjectTasklist.ascx" TagName="ProjectTasklist" TagPrefix="uc2" %>
<%@ Register Src="controls/ProjectTabs.ascx" TagName="ProjectTabs" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:ProjectTabs ID="ProjectTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectManagement%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.ProjectOverview%> - <Pref:ProjectRef ID="ProjectRef1" runat="server" /> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
     <asp:HyperLink ID="HyperLink1" runat="server" SkinID="BackToPipeline"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--   <script src="../../Scripts/GridDesingFix.js"></script>
    <script src="../../Scripts/respond.min.js"></script>
    	<script src="../../Content/assets/js/rwd-table/js/rwd-table.min.js"></script>
    <link href="../../Content/AjaxControlToolkit/Styles/Calendar.css" rel="stylesheet" />--%>
    <asp:UpdatePanel runat="server" ID="IDPOP">
        <ContentTemplate>
            <div>
                <ajaxToolkit:ModalPopupExtender ID="mdlPopViewInvoice" runat="server" PopupControlID="pnlViewInvoice"
                    TargetControlID="btnShowModalPopup" CancelControlID="imgCancel" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
            </div>
            <asp:Panel ID="pnlViewInvoice" runat="server" BackColor="White" Style="display: none"
                Width="830px" BorderStyle="Double" BorderColor="LightSteelBlue" Visible="true"
                Height="400px">
                <div style="float: right">
                    <asp:Button ID="imgCancel" runat="server" SkinID="btnCancel" /></div>
                <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 750px;">
                    <asp:Label ID="lblRef" runat="server" Text="Enter PO Information" Visible="true"></asp:Label></div>
                <div>
                    <asp:ValidationSummary ID="vld" runat="server" ValidationGroup="POPUP" />
                    <asp:Label ID="lblPOPError" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                    <asp:Label ID="lblPOMsg" runat="server" Text="" Visible="false" ForeColor="Green"></asp:Label>
                    <asp:Label ID="lblNote" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                </div>
                <table cellpadding="0" width="100%" cellspacing="0" border="0" style="padding-left: 15px">
                    <tr>
                        <td>
                    <%= Resources.DeffinityRes.PODatabase_PONumber%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPONumber" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter PO Number"
                                Display="None" ValidationGroup="POPUP" ControlToValidate="txtPONumber"></asp:RequiredFieldValidator>
                        </td>
                       
                        <td>
                   <%= Resources.DeffinityRes.FromDate %>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDateRaised" runat="server" SkinID="Date" ></asp:TextBox>
                            <asp:Label ID="imgDateRaised" runat="server" SkinID="Calender" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender7"  runat="server"
                                PopupButtonID="imgDateRaised" TargetControlID="txtDateRaised" >
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter date"
                                Display="None" ValidationGroup="POPUP" ControlToValidate="txtDateRaised"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                        <%= Resources.DeffinityRes.ExpiryDt %> 
                        </td>
                        <td>
                            <asp:TextBox ID="txtPOExpDate" runat="server" SkinID="Date" ></asp:TextBox>
                            <asp:Label ID="imgPOExpDate" runat="server" SkinID="Calender" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4"  runat="server" 
                                PopupButtonID="imgPOExpDate" TargetControlID="txtPOExpDate" >
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter date"
                                Display="None" ValidationGroup="POPUP" ControlToValidate="txtPOExpDate"></asp:RequiredFieldValidator>
                        </td>
                        <%--<td></td>
              <td></td>--%>
                    </tr>
                    <tr>
                        <td>
                       <%= Resources.DeffinityRes.Numberofdays%>     
                        </td>
                        <td>
                            <asp:TextBox ID="txtDurationDays" runat="server" SkinID="txt_40"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please enter days"
                                Display="None" ValidationGroup="POPUP" ControlToValidate="txtDurationDays"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                       <%= Resources.DeffinityRes.Value%> 
                        </td>
                        <td>
                            <asp:TextBox ID="txtValue" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td>
                            <%= Resources.DeffinityRes.Notes%>
                        </td>
                        <td align="center" colspan="3">
                            <asp:TextBox ID="txtDetails" runat="server" TextMode="MultiLine" Width="200px" Height="40px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <asp:LinkButton ID="imgSave" runat="server" SkinID="BtnLinkAdd" OnClick="imgSave_Click"
                                ValidationGroup="POPUP" />
                        </td>
                    </tr>
                </table>
                <tr>
                    <div>
                        <asp:GridView ID="gridPO" runat="server" Width="100%" AutoGenerateColumns="false"
                            OnRowCommand="gridPO_RowCommand" OnRowDeleting="gridPO_RowDeleting" OnRowDataBound="gridPO_RowDataBound"
                            OnRowCancelingEdit="gridPO_RowCancelingEdit" OnRowEditing="gridPO_RowEditing"
                            OnRowUpdating="gridPO_RowUpdating">
                            <Columns>
                                <asp:TemplateField >
                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID")%>' Visible="false"> </asp:Label>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit"  ToolTip="Edit">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div style="width: 45px">
                                            <div style="width: 20px; float: left">
                                                <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                                    CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate" ToolTip="Update"
                                                    ValidationGroup="Group3"></asp:LinkButton></div>
                                            <div style="width: 20px; float: left">
                                                <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                                    SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton></div>
                                        </div>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:DeffinityRes,PODatabase_PONumber%>">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPO" runat="server" Text='<%# Bind("PONumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,FromDate%>" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromDate" runat="server" Text='<%#Bind("DateRaised","{0:d}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFromDateGrid" SkinID="Date" runat="server" Text='<%#Bind("DateRaised","{0:d}")%>'></asp:TextBox>
                                        <asp:Label ID="imgFromDateGrid" runat="server" SkinID="Calender" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender41"  runat="server"
                                            PopupButtonID="imgFromDateGrid" TargetControlID="txtFromDateGrid" >
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Please enter date"
                                            Display="None" ValidationGroup="POPUP" ControlToValidate="txtFromDateGrid"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ToDate%>" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToDate" runat="server" Text='<%#Bind("POExpiryDate","{0:d}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtToDateGrid" SkinID="Date" runat="server" Text='<%#Bind("POExpiryDate","{0:d}")%>'></asp:TextBox>
                                        <asp:Label ID="imgToDateGrid" runat="server" SkinID="Calender" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender42"  runat="server"
                                            PopupButtonID="imgToDateGrid" TargetControlID="txtToDateGrid" >
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Please enter date"
                                            Display="None" ValidationGroup="POPUP" ControlToValidate="txtToDateGrid"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Days%>" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDDays" runat="server" Width="50px" Text='<%#Bind("DDays")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDDays" runat="server" Text='<%#Bind("DDays")%>' Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Value%>" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblValue" runat="server" Width="75px" Text='<%#Bind("Value","{0:f2}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtValues" runat="server" Text='<%#Bind("Value")%>' Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Notes%>" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNotes" Width="120px" runat="server" Text='<%#Bind("DetailsOfPO")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDetailsOfPO" runat="server" Text='<%#Bind("DetailsOfPO")%>'>></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="15px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImageButton1" Visible='<%#IsExist(Eval("PONumber").ToString())%>'
                                            runat="server" CausesValidation="false" CommandName="Delete" SkinID="BtnLinkDelete"
                                            CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                        </asp:GridView>
                    </div>
                    
                    <asp:Button runat="server" ID="btnShowModalPopup" Style="display: none" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div >
    <div class="form-group-no-margin">
        <div class="col-md-6">
            </div>
          <div class="col-md-6 ">
              <div class="pull-right">
               <asp:Button ID="imgSaveCopy" runat="server" SkinID="btnSave" OnClick="imgSaveCopy_Click" />
                  </div>
              </div>
        </div>
     <div class="form-group">
          <div class="col-md-12">
               <asp:Label ID="lblMsg" runat="server" ForeColor="Red" EnableViewState="false"></asp:Label>
                    <asp:Label ID="Lbllocation" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                    <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="Submit"
                        DisplayMode="List" HeaderText="<b>Please check following:</b>"></asp:ValidationSummary>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="Submit"
                        Display="None" ErrorMessage="Please enter Project title" ControlToValidate="txtProjectTitle"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ValidationGroup="Submit"
                        Display="None" ErrorMessage="Please enter Email" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
              </div>
         </div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ProjectTitle%></label>
                                      <div class="col-sm-8">
                                            <asp:TextBox ID="txtProjectTitle" runat="server" SkinID="txt_80" ValidationGroup="Submit"
                                            MaxLength="100"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Status%></label>
                                      <div class="col-sm-8">
                                          <asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_80">
                    </asp:DropDownList>
					</div>
				</div>
</div>

     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Owner%></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:DropDownList ID="ddlOwner" runat="server" ValidationGroup="Submit"
                                            OnSelectedIndexChanged="ddlOwner_SelectedIndexChanged" AutoPostBack="True" SkinID="ddl_80">
                                        </asp:DropDownList>
                                        <asp:Label runat="server" ID="imgHelp" SkinID="Help" ToolTip="Select the owner of the project" />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Country%></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                            AutoPostBack="True" SkinID="ddl_80">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="btnCountry" OnClick="btnCountry_Click" runat="server" CausesValidation="False"
                                            SkinID="BtnLinkAdd"></asp:LinkButton>
					</div>
				</div>
</div>

     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Email%> </label>
                                      <div class="col-sm-8">
                                            <asp:TextBox ID="txtEmail" runat="server" Width="250px" ValidationGroup="Submit"
                                            MaxLength="100"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.City%> :</label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:DropDownList ID="ddlCity" runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"
                                            AutoPostBack="True" SkinID="ddl_80">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="btnCity" OnClick="btnCity_Click" runat="server" CausesValidation="False"
                                            SkinID="BtnLinkAdd"></asp:LinkButton>
					</div>
				</div>
</div>

        <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">Assigned Sales Executive </label>
                                      <div class="col-sm-8"><asp:DropDownList ID="ddlAssignedSalesExecutive" runat="server" SkinID="ddl_80"></asp:DropDownList>
                                          </div>
                                      </div>
             <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%=Resources.DeffinityRes.ProjectManager%></label>
                                      <div class="col-sm-8  form-inline"> <asp:DropDownList ID="ddlProjectManager" runat="server" SkinID="ddl_80"></asp:DropDownList>
                                           <asp:Label runat="server" ID="Label3" SkinID="Help" ToolTip="Select the project co-ordinator. This is the person that has overall charge of the programme" />
                                          </div>
                                      </div>
            </div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ProjectCustomer%>  </label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:DropDownList ID="ddlPortfolio" runat="server" ValidationGroup="Submit"
                                            DataTextField="PortFolio" DataValueField="ID" OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged"
                                            DataSourceID="SqlDataSourceTitle2" AutoPostBack="True" SkinID="ddl_80">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                            SelectCommand="Project_PermissionCustomer" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <asp:LinkButton ID="btnPortfolio" OnClick="btnPortfolio_Click" runat="server" CausesValidation="False"
                                            SkinID="BtnLinkAdd" ToolTip="If a customer is not shown here you can create a new one using this button."></asp:LinkButton>
                                        <br />
                                        <asp:CheckBox ID="chkCustomer" runat="server" Width="150px" Font-Bold="false" Text="Visible&nbsp;to&nbsp;customer">
                                        </asp:CheckBox>
                                        <asp:Label runat="server" ID="imgHelp0" SkinID="Help" ToolTip="Enable this to give customers visibility of this project. You will need to add the customer users from the permissions page for them to see the project within the customer portal." />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Site%> :</label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:DropDownList ID="ddlSite" runat="server" SkinID="ddl_80">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="btnSite" OnClick="btnSite_Click" runat="server" CausesValidation="False"
                                            SkinID="BtnLinkAdd"></asp:LinkButton>
					</div>
				</div>
</div>


     <div class="form-group" style="display:none;">
                                  
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.PrimaryQA%> </label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:DropDownList ID="ddlPrimQA" runat="server" ValidationGroup="Submit" SkinID="ddl_80">
                                        </asp:DropDownList>
                                        <asp:Label runat="server" ID="imgHelp8" SkinID="Help" ToolTip="The user selected here will be able to carry out an post project check in the QA section." />
                                   
					</div>
				</div>

         <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ScheduledQAdate%> </label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtQAdate" runat="server" MaxLength="10" ValidationGroup="Submit"
                                            SkinID="Date"></asp:TextBox>
                                        <asp:Label ID="img3" runat="server" SkinID="Calender" ToolTip="Pick a date" />
                                        <asp:Label runat="server" ID="imgHelp7" SkinID="Help" ToolTip="The user selected as Primary QA will be notified via email on the date specified here." />
                                   
					</div>
				</div>
</div>

     <div class="form-group" runat="server" id="DivProgrammeSubProgramme">
         <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Programme%>  </label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:DropDownList ID="ddlGroup" runat="server" DataSourceID="SqlDataSource8"
                                            DataTextField="OperationsOwners" DataValueField="ID" ValidationGroup="Submit"
                                            OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="True" SkinID="ddl_80">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="btnOwners" runat="server" CausesValidation="False" OnClick="btnOwners_Click"
                                            SkinID="BtnLinkAdd" />
                                        <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                            SelectCommand="Project_AssignedProgramme" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
					</div>
				</div>
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.SubProgramme%>  </label>
                                      <div class="col-sm-8 form-inline">
                                            <asp:DropDownList ID="ddlSubprogramme" runat="server" DataSourceID="SqlDataSource2"
                                            DataValueField="ID" DataTextField="OPERATIONSOWNERS" ValidationGroup="Submit" SkinID="ddl_80">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                            SelectCommand="Project_AssignedSubProgramme" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                                                <asp:ControlParameter ControlID="ddlGroup" DefaultValue="0" Name="PROGRAMMEID" PropertyName="SelectedValue"
                                                    Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <asp:LinkButton ID="btnProgramme" runat="server" CausesValidation="False" OnClick="btnProgramme_Click"
                                            SkinID="BtnLinkAdd" />
					</div>
				</div>
                                 
</div>

     <div class="form-group">
                                  
         <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RequestersName%> </label>
                                      <div class="col-sm-8">
                                           <asp:TextBox ID="txtRequestName" runat="server" SkinID="txt_80" ValidationGroup="Submit"></asp:TextBox>
					</div>
				</div>

 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Priority%></label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlPriority" runat="server" ValidationGroup="Submit" SkinID="ddl_80">
                                            <asp:ListItem Text="High" Value="High"></asp:ListItem>
                                            <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                                            <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                                        </asp:DropDownList>
					</div>
				</div>
</div>

     <div class="form-group" style="display:none;">

         <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.StartDate%></label>
                                      <div class="col-sm-8 form-inline">
                                            <asp:TextBox ID="txtStartDate" runat="server" MaxLength="10" ValidationGroup="Submit"
                                            SkinID="Date"></asp:TextBox>
                                        <asp:Label ID="Img1" runat="server" SkinID="Calender" />
					</div>
				</div>

                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ExpectCompletionDate%> </label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:TextBox ID="txtEndDate" runat="server" MaxLength="10" ValidationGroup="Submit"
                                            SkinID="Date"></asp:TextBox>
                                        <asp:Label ID="img2" runat="server" SkinID="Calender" />
					</div>
				</div>
 
</div>
     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><asp:Label ID="lblcustom1" runat="server"></asp:Label></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:TextBox ID="txtCustom1" runat="server" SkinID="txt_80" ValidationGroup="Submit"
                                            MaxLength="50"></asp:TextBox>
                                        <asp:LinkButton ID="btnPOnumber" runat="server" SkinID="BtnLinkAdd" OnClick="btnPOnumber_Click" ToolTip="Enter the purchase order number from the customer. If you have more than one purchase order you can add them using the “+” button." />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RequestersEmail%></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:TextBox ID="txtRequestEmail" runat="server" SkinID="txt_80" ValidationGroup="Submit"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorReqEmail" runat="server"
                                            ControlToValidate="txtRequestEmail" Display="None" ErrorMessage="Please enter valid Requestor email"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                  
					</div>
				</div>
</div>

     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><asp:Label ID="lblCustom2" runat="server"></asp:Label></label>
                                      <div class="col-sm-8">
                                            <asp:TextBox ID="txtCustom2" runat="server" SkinID="txt_80" ValidationGroup="Submit"
                                            MaxLength="50"></asp:TextBox>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Costcenter%></label>
                                      <div class="col-sm-8">
                                           <asp:TextBox ID="txtCostCenter" runat="server" SkinID="txt_80" ValidationGroup="Submit"></asp:TextBox>
					</div>
				</div>
</div>

     <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ActualCosttoDate%></label>
                                      <div class="col-sm-8">
                                           <asp:TextBox ID="txtActualCost" runat="server" ValidationGroup="Submit"
                                            MaxLength="20" ReadOnly="True" SkinID="txt_80" ></asp:TextBox>
					</div>
				</div>
         <div class="col-md-6">
             <label class="col-sm-4 control-label">Sales Person</label>
                                      <div class="col-sm-8">
                                          <asp:DropDownList ID="ddlSalesStaff" runat="server" SkinID="ddl_80" >
                                        </asp:DropDownList>
                                          </div>
                                      </div>


 <div class="col-md-6" style="display:none;">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.POusagevisible%> :</label>
                                      <div class="col-sm-8 form-inline">  <asp:CheckBox ID="chkPO" runat="server" />
                                        <asp:Label runat="server" ID="imgHelp6" SkinID="Help" ToolTip="Enable this option if you want to show the purchase order drain on the project pipeline page." />
                                   
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-6" runat="server" id="DivProjectClass">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ProjectClass%>  </label>
                                      <div class="col-sm-8 form-inline">
                                            <asp:DropDownList ID="ddlCategory" runat="server" SkinID="ddl_80">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="btnCategory" OnClick="btnCategory_Click" runat="server" CausesValidation="False"
                                            SkinID="BtnLinkAdd" ToolTip="Project class refers to a classification of your project and is linked to the programme you have selected. You can use Project Class to carry out a check/audit between project phases (Pending to Live and then Live to Complete)."></asp:LinkButton>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                                             PopupButtonID="img2" TargetControlID="txtEndDate">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                             PopupButtonID="img1" TargetControlID="txtStartDate">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                             PopupButtonID="img3" TargetControlID="txtQAdate">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:HiddenField ID="HiddenStatus" runat="server" Visible="false"></asp:HiddenField>
					</div>
				</div>
 <div class="col-md-6" style="display:none;">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.RAGsStatus%> :</label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:DropDownList ID="ddlRagstatus" runat="server" ValidationGroup="Submit" SkinID="ddl_80">
                                            <asp:ListItem Text="GREEN" Value="GREEN"></asp:ListItem>
                                            <asp:ListItem Text="RED" Value="RED"></asp:ListItem>
                                            <asp:ListItem Text="AMBER" Value="AMBER"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label runat="server" ID="imgHelp5" SkinID="Help" ToolTip="Overall RAG status of the project. This is a manual selection." />
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-6" style="display:none;">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.CustomerTimesheetApprover%></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:DropDownList ID="ddlCustomerUsers" runat="server" SkinID="ddl_80">
                                        </asp:DropDownList>
                                        <asp:Label runat="server" ID="imgHelp3" SkinID="Help" ToolTip="Select the customer user who has rights to approve timesheets." />
					</div>
				</div>
 <div class="col-md-6" id="pnlSetdays" runat="server" visible="false">
                                       <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Setprojectbackby%></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:TextBox ID="txtdays" runat="server" SkinID="txt_75px" ValidationGroup="Group2" MaxLength="4"></asp:TextBox>
                                      <%= Resources.DeffinityRes.Days%><asp:LinkButton ID="btnDays" OnClick="btnDays_Click" runat="server" ValidationGroup="Group2"
                                            Text="Back" Height="20px"></asp:LinkButton>
                                        <asp:RequiredFieldValidator ID="ValidateDays" runat="server" ValidationGroup="Group2"
                                            Display="None" ErrorMessage="Please enter days" ControlToValidate="txtdays"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator10" runat="server" ValidationGroup="Group2"
                                            Display="None" ErrorMessage="Please enter numeric values in days " ControlToValidate="txtdays"
                                            Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
					</div>
				</div>
           <div class="col-md-6"> 
                                        <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.BaseCurrency%></label>
                                      <div class="col-sm-8">
                                          <asp:DropDownList ID="ddlCurrency" runat="server" ValidationGroup="Submit" SkinID="ddl_80">
                                        </asp:DropDownList>
                                          </div>
                                      </div>
</div>
    <div class="form-group">
                               
        
        </div>
    </div>
    <br />
    <br />

     <div class="form-group">
        <div class="col-md-12">
           <strong>Additional Project Fields </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
     <div class="form-group">
        <div class="col-md-12">
               <asp:UpdatePanel ID="updatepanel_additional" runat="server">
                    <ContentTemplate>
                         <asp:PlaceHolder ID="ph" runat="server">
                </asp:PlaceHolder>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="imgSaveCopy" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
         </div>
    
    <br />
    <br />
    <div class="form-group">
        <div class="col-md-12">
        <strong>   <%= Resources.DeffinityRes.ProjectDescription%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
     <div class="row">
        <div class="col-md-12">
            <asp:TextBox ID="txtDesc" runat="server" Width="100%" MaxLength="20" Height="60px"
                    TextMode="MultiLine"></asp:TextBox>
            </div>
         </div>
   
    <br />
    <br />
    <div class="form-group">
        <div class="col-md-12 ">
        <strong>   <%= Resources.DeffinityRes.RolesandResponsibilities%></strong>
            <hr class="no-top-margin" />
            </div>
    </div>
    <div class="form-group">
        <div class="col-md-12">
           <div>
                    <asp:Label ID="lblError" runat="server" Visible="false"></asp:Label></div>
                <asp:GridView ID="gridProjectGoal" runat="server" Width="700px" AutoGenerateColumns="false"
                    OnRowCommand="gridProjectGoal_RowCommand" OnRowDataBound="gridProjectGoal_RowDataBound"
                    OnRowDeleting="gridProjectGoal_RowDeleting" OnRowEditing="gridProjectGoal_RowEditing"
                    OnRowUpdating="gridProjectGoal_RowUpdating" OnRowCancelingEdit="gridProjectGoal_RowCancelingEdit" EmptyDataText="No data exists.">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="30px" ItemStyle-CssClass="form-inline">
                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID")%>' Visible="false"> </asp:Label>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ID")%>' Visible="false"> </asp:Label>
                                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="false" Enabled="<%#CommandField()%>"
                                            CommandName="Edit" CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkEdit"
                                            ToolTip="Edit"></asp:LinkButton></div>
                            </ItemTemplate>
                            <EditItemTemplate>
                               
                                        <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update"
                                            CommandArgument='<%# Bind("ID")%>' SkinID="BtnLinkUpdate" ToolTip="Update">
                                        </asp:LinkButton></div>
                                   
                                        <asp:LinkButton ID="LinkButtonCancel" runat="server" CausesValidation="false" CommandName="Cancel"
                                            SkinID="BtnLinkCancel" ToolTip="Cancel"></asp:LinkButton></div>
                               
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="LinkButtonInsert" runat="server" Enabled="<%#CommandField()%>"
                                    CommandName="Insert" Text="<%$ Resources:DeffinityRes,Insert%>" SkinID="BtnLinkAdd"
                                    ToolTip="<%$ Resources:DeffinityRes,Insert%>"></asp:LinkButton>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Resource%>">
                            <ItemStyle Width="150px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPGName" runat="server" Text='<%# Bind("ResourceName")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblPGResourceID" runat="server" Visible="false" Text='<%# Bind("ResourceID")%>'></asp:Label>
                                <asp:DropDownList ID="ddlPGResourceNameGrid" runat="server" Width="150px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlPGResourceNameFooter" runat="server" Width="150px">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Role%>">
                            <ItemStyle Width="150px" />
                            <ItemTemplate>
                                <asp:Label ID="lblRoleName" runat="server" Text='<%# Bind("RoleName")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblPGRoleID" runat="server" Visible="false" Text='<%# Bind("RoleID")%>'></asp:Label>
                                <asp:DropDownList ID="ddlPGRoleGrid" runat="server" Width="150px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlPGRoleFooter" runat="server" Width="150px">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="100px" HeaderText="<%$ Resources:DeffinityRes,Notes%>">
                            <ItemStyle Width="150px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPGNotes" runat="server" Text='<%# Bind("Notes")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNotesPG" runat="server" Width="150px" Text='<%# Bind("Notes")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtNotesPGFooter" runat="server" Width="150px"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="ImageButton1" runat="server" Enabled="<%#CommandField()%>" CausesValidation="false"
                                    CommandName="Delete" SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>' OnClientClick="return confirm('Do you want to delete the record?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
           
            </div>
    </div>
    
    <br />
    <br />
     <div class="form-group">
        <div class="col-md-12 text-bold">
         <strong>  <%= Resources.DeffinityRes.KeyMilestone%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
     <div class="form-group">
        <div class="col-md-12">
           <asp:ValidationSummary ID="Project_RAGs" ValidationGroup="GridValid" runat="server" />
                <asp:GridView ID="GridViewProjectRag" runat="server" DataKeyNames="ID" Width="100%"
                    OnRowCancelingEdit="GridViewProjectRag_RowCancelingEdit" OnRowCommand="GridViewProjectRag_RowCommand"
                    OnRowEditing="GridViewProjectRag_RowEditing" OnRowDeleting="GridViewProjectRag_RowDeleting"
                    OnRowUpdated="GridViewProjectRag_RowUpdated" OnRowUpdating="GridViewProjectRag_RowUpdating"
                    OnRowDataBound="GridViewProjectRag_RowDataBound" EmptyDataText="No data exists.">
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                    SkinID="BtnLinkUpdate" ValidationGroup="GridValid"
                                    CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                    SkinID="BtnLinkCancel"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                    SkinID="BtnLinkEdit"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="75px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,KeyMilestone%>">
                            <ItemStyle Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblRAGName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblRAGName1" runat="server"></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,KeyIssue%>">
                            <ItemStyle Width="300px" />
                            <ItemTemplate>
                                <asp:Label ID="lblKeyIssue" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtKeyIssue" runat="server" Width="300px"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Status%>">
                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="imgStatus" SkinID="BtnLinkList" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlRAGStatus" runat="server">
                                    <asp:ListItem Text="Please select..." Value="0"></asp:ListItem>
                                    <asp:ListItem Text="RED" Value="RED"></asp:ListItem>
                                    <asp:ListItem Text="GREEN" Value="GREEN"></asp:ListItem>
                                    <asp:ListItem Text="AMBER" Value="AMBER"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,PlannedDate%>" ItemStyle-CssClass="form-inline">
                            <ItemTemplate>
                                <asp:Label ID="lblPlannedDate" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                               
                                            <asp:TextBox ID="txtPlannedDate" runat="server" MaxLength="10" SkinID="Date"></asp:TextBox>
                                      
                                            <asp:Label ID="Img_Planned" SkinID="Calender" runat="server" Style="padding-left: 0px;
                                                padding-right: 0px" />
                                            <ajaxToolkit:CalendarExtender ID="CE_Planneddate" runat="server" 
                                                PopupButtonID="Img_Planned" TargetControlID="txtPlannedDate" >
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RFV_1" runat="server" ErrorMessage="Please enter Planned Date"
                                                ControlToValidate="txtPlannedDate" ValidationGroup="GridValid" Display="None"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CV_1" runat="server" ControlToValidate="txtPlannedDate"
                                                ErrorMessage="Please enter valid Planned Date" Operator="DataTypeCheck" Type="Date"
                                                ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                                        
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ActualDate%>" ItemStyle-CssClass="form-inline">
                            <ItemTemplate>
                                <asp:Label ID="lblActualDate" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                               
                                            <asp:TextBox ID="txtActualDate" runat="server" MaxLength="10" SkinID="Date"></asp:TextBox>
                                       
                                            <asp:Label ID="Img_ActualDate" SkinID="Calender" runat="server" Style="padding-left: 0px;
                                                padding-right: 0px" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender_ActualDate" runat="server" 
                                                PopupButtonID="Img_ActualDate" TargetControlID="txtActualDate" >
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RFV_ActualDate" runat="server" ErrorMessage="Please enter Actual Date"
                                                ControlToValidate="txtActualDate" ValidationGroup="GridValid" Display="None"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CV_ActualDate" runat="server" ControlToValidate="txtActualDate"
                                                ErrorMessage="Please enter valid Actual Date" Operator="DataTypeCheck" Type="Date"
                                                ValidationGroup="GridValid" Display="None"></asp:CompareValidator>
                                        
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton_del" runat="server" CausesValidation="false" CommandName="Delete"
                                    SkinID="BtnLinkDelete" CommandArgument='<%# Bind("ID") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                           
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                       <%= Resources.DeffinityRes.NoWorkstreamavailable%>
 
                    </EmptyDataTemplate>
                </asp:GridView>
            
            </div>
    </div>
   
    <br />
    <br />
     <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  <%= Resources.DeffinityRes.ProjectCheckpoints%> </strong>
            <hr class="no-top-margin" />
            </div>
    </div>
     <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="GridView1" runat="server" DataKeyNames="ID" Width="100%" AutoGenerateColumns="False"
                    ShowFooter="True" DataSourceID="SqlDataSource1" EnableViewState="false" EmptyDataText="No data exists.">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" ButtonType="Link" 
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                        </asp:CommandField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,StartDate%>">
                            <ItemTemplate>
                                <asp:Label ID="lblSdate" runat="server" Text='<%# Bind("ScheduledDate","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="105px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Tasks%>">
                            <ItemTemplate>
                                <asp:Label ID="lbltask1" runat="server" Text='<%# Bind("Tasks") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="160px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Assigned_To%>">
                            <ItemStyle Width="150px" />
                            <ItemTemplate>
                                <asp:Label ID="lblAssignto" runat="server" Text='<%# Bind("assignto") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Status%>">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Notes%>">
                            <ItemTemplate>
                                <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="170px" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                      <%= Resources.DeffinityRes.NoCheckpointsavailable%>.
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="DN_selectQAShedule" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:QueryStringParameter Type="int32" Name="Project" QueryStringField="Project"
                            DefaultValue="80" />
                    </SelectParameters>
                </asp:SqlDataSource>
           
            </div>
    </div>
   
    <br />
    <br />
     <div class="row">
        <div class="col-md-12">
              <asp:Button ID="btnNext1" runat="server" OnClick="btnNext1_Click" ValidationGroup="Submit"
            SkinID="ImgSave" Height="20px" Visible="false" />
         <asp:Panel ID="panleEmail" runat="server" EnableTheming="false">
        <uc2:ProjectTasklist ID="ProjectTasklist1" runat="server" Visible="false" />
    </asp:Panel>
            </div>
    </div>

     
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    GridResponsiveCss();
 </script> 

</asp:Content>
