<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Deffinity.GlobalIssues.controls_ProjectPmNewIssues" Codebehind="ProjectPmNewIssues.ascx.cs" %>
<%@ Register Src="~/WF/Projects/MailControls/ProjectIssue.ascx" TagName="ProjectIssue" TagPrefix="PD1" %>
<%@ Register Src="~/WF/Projects/MailControls/IssueAlertMail.ascx" TagName="ProjectIssueAlert"
    TagPrefix="PD2" %>
<style type="text/css">
    .style2
    {
        width: 209px;
    }
    .style3
    {
        width: 148px;
    }
</style>
<div>
 <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
</div>
<div id="pnlIssuetype" runat="server">
    <ajaxToolkit:ModalPopupExtender ID="popIssues" BackgroundCssClass="modalBackground"
        runat="server" PopupControlID="PaneladdNew" TargetControlID="lblImgAddButton" CancelControlID="DropDownListAssignedTo" />
    <asp:Label ID="lblImgAddButton" runat="server"></asp:Label>

       <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="modalBackground"
        runat="server" PopupControlID="PnlAddNewIssue" TargetControlID="ImgAddButton" CancelControlID="DropDownListAssignedTo" />


     <div class="form-group">
      <div class="col-md-4" runat="server" id="DivCategory">
           <label class="col-sm-3 control-label" runat="server" id="tdCategory"><%= Resources.DeffinityRes.Category%></label>
           <div class="col-sm-9" runat="server" id="tdddlCategory">
                 <asp:DropDownList ID="ddtype" runat="server" SkinID="ddl_80" AutoPostBack="True" DataSourceID="objDsTypewithALL"
                    DataValueField="ID" DataTextField="IssueTypeName">
                </asp:DropDownList>
                <asp:ObjectDataSource ID="objDsTypewithALL" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="SelectTypeWithALL" TypeName="DeffinityManager.DAL.PmNewIssuesTableAdapters.adaIssueType">
                </asp:ObjectDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.AssignedTo%></label>
           <div class="col-sm-9">
                  <asp:DropDownList ID="DropDownListAssignedTo" runat="server" AutoPostBack="true"
                    SkinID="ddl_80"></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="Project_AssignedTo" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" />
                        <asp:QueryStringParameter Name="ProjectRefrence" DefaultValue="0" QueryStringField="Project"
                            Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
	</div>
</div>
     <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="DropDownListStatus" runat="server" DataTextField="Status" DataValueField="ID"
                    SkinID="ddl_80" AutoPostBack="true" DataSourceID="objDsStatus">
                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label">Search by Issue</label>
           <div class="col-sm-9">
                  <asp:TextBox ID="txtSearchIssue" runat="server" SkinID="txt_80"></asp:TextBox>
            </div>
	</div>
  <div class="col-md-4">
         <div class="col-sm-9 form-inline">
            <asp:Button ID="ImageButton1" runat="server" SkinID="btnSearch"
                    CausesValidation="false" />
                  <asp:Button ID="ImgAddButton" runat="server" SkinID="btnAddNew" CssClass="btn btn-orange"
                    CausesValidation="false" />
        </div>

    </div>
</div>
</div>
<PD2:ProjectIssueAlert ID="MailCnt" Visible="false" runat="server" />

            <asp:Label ID="imgShowpopup" SkinID="Back" runat="server" EnableViewState="false"
                Style="display: none" />
            <asp:GridView ID="gvBindIssues" runat="server" DataKeyNames="ID" DataSourceID="objDsBindGrid"
                GridLines="None" EmptyDataText="<%$ Resources:DeffinityRes, Nodataexist%>" Width="100%"
                AutoGenerateColumns="False" HorizontalAlign="Left" BorderColor="White" BorderStyle="None"
                CellPadding="0" CellSpacing="1" ShowFooter="True" OnRowCommand="gvBindIssues_RowCommand"
                OnRowDataBound="gvBindIssues_RowDataBound" OnRowDeleting="gvBindIssues_RowDeleting">
                <Columns>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                            <asp:LinkButton runat="server" ID="hlEdit1" CommandArgument='<%# Bind("ID") %> '
                                Enabled="<%#CommandField()%>" CommandName="cmdEdit" CausesValidation="false"
                                SkinID="BtnLinkEdit" ToolTip="<%$ Resources:DeffinityRes, Edit%>">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="hlEdit" CommandArgument='<%# Bind("ID") %> '
                                CommandName="cmdEdit" CausesValidation="false" SkinID="BtnLinkEdit"
                                ToolTip="<%$ Resources:DeffinityRes, Edit%>"></asp:LinkButton>
                            <%-- <asp:Label ID="lblID1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="35px" />
                    </asp:TemplateField>
                    <asp:CommandField Visible="false" ShowEditButton="True" ButtonType="Image" CancelImageUrl="~/media/ico_cancel.png"
                        EditImageUrl="~/media/ico_edit.png" UpdateImageUrl="~/media/ico_update.png">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:CommandField>
                     <asp:TemplateField HeaderText="Issue<br/>Reference">
                                        <ItemTemplate>
                                             <asp:Label ID="lnkIssueRef" runat="server" Text='<%# Prefix()+ Eval("ProjectRef") + "-" + Eval("ID")  %>'
                                              ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Issue%>" ItemStyle-CssClass="col-nowrap_300" >
                        <ItemTemplate>
                            <asp:Label ID="lblIssue" runat="server" Text='<%# Bind("Issue") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditIssue" runat="server" Width="100%" Text='<%# Bind("Issue") %>'
                                TextMode="MultiLine"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, DateRaised%>">
                        <ItemTemplate>
                            <asp:Label ID="lblSdate" runat="server" Text='<%# Bind("ScheduledDate","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEdate" runat="server" Width="70px" Text='<%# Bind("ScheduledDate","{0:d}") %>'></asp:TextBox>
                            <asp:Label ID="Image3" runat="server" SkinID="calender" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                PopupButtonID="Image3" TargetControlID="txtEdate" CssClass="MyCalendar">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="<%$ Resources:DeffinityRes, Plsentervalidenddate%>"
                                ControlToValidate="txtEdate" Text="*" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((1[6-9]|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Next Review Date">
                        <ItemTemplate>
                            <asp:Label ID="lblNextReviewDate" runat="server" Text='<%#Bind("NextReviewDate","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Type%>">
                        <ItemTemplate>
                            <asp:Label ID="lblIssuseType" runat="server" Text='<%# Bind("IssueTypeName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList Width="110px" ID="ddlQAtype1" runat="server" DataTextField="IssueTypeName"
                                DataValueField="ID" DataSourceID="objDsIssueType">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, AssignedTo%>">
                        <ItemStyle Width="100px" />
                        <ItemTemplate>
                            <asp:Label ID="lblAssignto" runat="server" Text='<%# Bind("AssignTo1") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList Width="100%" DataSourceID="objCheckedBy" ID="ddlAssignedTo" runat="server"
                                DataTextField="ContractorName" DataValueField="ID" SelectedValue='<%# Bind("ID") %>'>
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%#Bind("AssignTo1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   

                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, ExpectedOutcome%>">
                        <ItemTemplate>
                            <asp:Label ID="lblExpectedOutcome" runat="server" Text='<%# Bind("ExpectedOutcome") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox TextMode="MultiLine" ID="txtExpectedOutcome" runat="server" Width="100px"
                                Text='<%# Bind("ExpectedOutcome") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="110px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Notes%>">
                        <ItemTemplate>
                            <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox TextMode="MultiLine" ID="txtNotes" runat="server" Width="100%" Text='<%# Bind("Notes") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="110px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action Plan">
                        <ItemTemplate>
                            <asp:Label ID="lblAP" runat="server" Text='<%# Bind("ActionPlan") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox TextMode="MultiLine" ID="txtAP" runat="server" Width="100%" Text='<%# Bind("ActionPlan") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="110px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Status%>">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status1") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="Status" DataValueField="ID"
                                Width="80px" DataSourceID="objDsStatus" SelectedValue='<%# Bind("Status") %>'>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderText="RAG Status">
                        <ItemTemplate>
                            <asp:Literal ID="litRAG" runat="server" Text='<%#GetImage(Eval("RAGStatus").ToString())%>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Documents%>">
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ToolTip="<%$ Resources:DeffinityRes, Clickheretodownloaddocuments%>"
                                runat="server" ID="lnkDocuments" Enabled='<%#GetDocsEnable(DataBinder.Eval(Container.DataItem, "CountFiles").ToString())%>'
                                CommandArgument='<%# Bind("ID") %>' Text='<%# Eval("CountFiles", " ({0})") %>'
                                CommandName="cmdViewExt" CausesValidation="false">Here</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, MarkasComplete%>">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="btnApprove" runat="server" Enabled="<%#CommandField()%>" CommandName="Approve"
                                OnClientClick="return confirm('Do you want to Mark as Complete?');" Visible='<%#CompletedVisibilityNot(DataBinder.Eval(Container.DataItem, "Status").ToString())%>'
                                CommandArgument='<%# Bind("ID")%>'  SkinID="BtnLinkApply" ToolTip="<%$ Resources:DeffinityRes, MarkasComplete%>" />
                            <asp:LinkButton ID="btnApproveCompleted" Enabled="false" runat="server" Visible='<%#CompletedVisibility(DataBinder.Eval(Container.DataItem, "Status").ToString())%>'
                                 SkinID="BtnLinkApply" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Customer<br/>Sign off" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Panel ID="pnlHover" runat="server" Visible='<%#CustomerSignOffVisible(DataBinder.Eval(Container.DataItem, "SignOff").ToString())%>' >
                                    <table width="250px" cellpadding="0" cellspacing="1" style="border-color: ButtonFace;
                                        border-width: 3px; border-style: double">
                                        <tr>
                                            <td class="tab_header_Bold" style="width: 50%">
                                                <asp:Literal ID="title" runat="server" Text="Signed-off By" />
                                            </td>
                                            <td style="color: #333333; background-color: #F7F6F3;" style="width: 50%">
                                                <b>
                                                    <asp:Literal ID="litProjectTitle" runat="server" Text='<%#Eval("SignOffBy")%>' />
                                                </b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tab_header_Bold">
                                                <asp:Literal ID="DateandTime" runat="server" Text="Date and Time" />
                                            </td>
                                            <td style="color: #333333; background-color: #F7F6F3;">
                                                <b>
                                                    <asp:Literal ID="litDescription" runat="server" Text='<%#Eval("SignOffDate","{0:dd/MM/yyyy HH:mm}")%>' />
                                                </b>
                                            </td>
                                        </tr>
                                       
                                    </table>
                                </asp:Panel>
                                <asp:LinkButton ID="imgPopUp"  runat="server" SkinID="BtnLinkApply" Visible='<%#CustomerSignOffVisible(DataBinder.Eval(Container.DataItem, "SignOff").ToString())%>' />
                                <ajaxToolkit:HoverMenuExtender ID="hoverMenu" runat="server" PopDelay="25" PopupControlID="pnlHover"
                                    TargetControlID="imgPopUp" CacheDynamicResults="false" PopupPosition="Left" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle CssClass="header_bg_r" />
                        </asp:TemplateField>
                 
                </Columns>
            </asp:GridView>
       

   
    <asp:ObjectDataSource ID="objDsBindGrid" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="DeffinityManager.DAL.PmNewIssuesTableAdapters.adaIssues" OnSelecting="objDsBindGrid_Selecting">
        <SelectParameters>
            <asp:Parameter Name="ID" Type="Int32" DefaultValue="0" />
            <asp:ControlParameter ControlID="ddtype" DefaultValue="0" Name="IssueType" PropertyName="SelectedValue"
                Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="IssueSection" Type="String" />
            <asp:ControlParameter ControlID="DropDownListAssignedTo" DefaultValue="0" Name="AssignTo"
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="DropDownListStatus" DefaultValue="0" Name="Status"
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="txtSearchIssue" DefaultValue="" Name="Issue" PropertyName="Text"
                Type="String" />
            <asp:SessionParameter SessionField="FristTime" DefaultValue="0" Type="int32" Name="FristTime" />
            <asp:SessionParameter SessionField="UID" DefaultValue="0" Type="Int32" Name="UserID" />
        </SelectParameters>
    </asp:ObjectDataSource>

<div>
    <ajaxToolkit:ModalPopupExtender ID="mplDocuments" runat="server" CancelControlID="img_mcat_cancel"
        BackgroundCssClass="modalBackground" PopupControlID="pnlDocs" TargetControlID="imgShowpopup" />
    <asp:Panel ID="pnlDocs" runat="server" BackColor="White" Style="display: none" Width="350px"
        BorderStyle="Double" BorderColor="LightSteelBlue" Visible="true">
        <div class="form-group">
                                  <div class="col-md-12">
                                       <asp:ObjectDataSource ID="objDsDocuments" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="DeffinityManager.DAL.PmNewIssuesTableAdapters.adaProjectDocuments">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="gvBindDocuments" DefaultValue="0" Name="issueID"
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                                      <asp:GridView ID="gvBindDocuments" DataKeyNames="DocId" runat="server" OnRowCommand="gvBindDocuments_RowCommand"
                        OnRowDataBound="gvBindDocuments_RowDataBound" OnRowCancelingEdit="gvBindDocuments_RowCancelingEdit"
                        OnRowEditing="gvBindDocuments_RowEditing" OnRowUpdated="gvBindDocuments_RowUpdated"
                        OnRowUpdating="gvBindDocuments_RowUpdating" OnRowDeleting="gvBindDocuments_RowDeleting"
                        Width="340px">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Document%>" SortExpression="DocumentName"
                                HeaderStyle-CssClass="header_bg_l">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocumentName" runat="server" Text='<%# Eval("DocumentName") +"."+Eval("Extension")%> '></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="300px" />
                                <HeaderStyle Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lbnDownload" SkinID="BtnLinkDownload"
                                        CommandName="cmdDownload" CommandArgument='<%# Bind("DocId") %> ' ToolTip="<%$ Resources:DeffinityRes, Download%>">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="hldelete" CommandName="cmdDelete" SkinID="BtnLinkDelete"
                                        ToolTip="<%$ Resources:DeffinityRes, Delete%>" CommandArgument='<%# Bind("DocId") %> '>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDocId" runat="server" Text='<%# Bind("DocId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("IssueID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblExtension" runat="server" Text='<%# Bind("Extension") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                                      
				</div>
            </div>
        <div class="form-group">
              <div class="col-md-12">
             <asp:Button ID="img_mcat_cancel" runat="server" SkinID="btnClose" />
                  </div>
        </div>
    </asp:Panel>
</div>
<asp:Panel Width="70%" Height="530px" ScrollBars="Auto" runat="server" ID="PaneladdNew"
    BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue" Style="display: none">
     <div class="form-group">
        <div class="col-md-12 text-bold">
       <%= Resources.DeffinityRes.AddEditIssue_Header%>
            <hr class="no-top-margin" />
            </div>
    </div>
    
   
    <div class="form-group">
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtIssue"
            Display="None" ErrorMessage="<%$ Resources:DeffinityRes, PlsenterIssue %>" ValidationGroup="valAddNew"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDate"
            Display="None" ErrorMessage="<%$ Resources:DeffinityRes, PlsenterDateraised %>"
            ValidationGroup="valAddNew"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAssign"
            Display="None" ErrorMessage="<%$ Resources:DeffinityRes, PlsselectvalidAssignto %>"
            ValidationGroup="valAddNew" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlQAtype"
            Display="None" ErrorMessage="<%$ Resources:DeffinityRes, PlsselectvalidIssuetype %>"
            ValidationGroup="valAddNew" InitialValue="0"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtDate"
            Display="None" ErrorMessage="<%$ Resources:DeffinityRes, Plsntervaliddataindateraised %>"
            Operator="DataTypeCheck" Type="Date" ValidationGroup="valAddNew"></asp:CompareValidator>
        <asp:ValidationSummary ID="VS_Addnew" runat="server" ValidationGroup="valAddNew" />
    </div>
    <asp:Panel runat="server" ID="pnlAssociated">
        <div class="form-group">
                                  <div class="col-md-12">
                                       <div class="col-sm-2"> 
                                           <asp:Label Text="" ID="lblAssociatedTo" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="lblAssociatedTo0" runat="server" Text="<%$ Resources:DeffinityRes, Associatedto %>"></asp:Label></div>
                                      <div class="col-sm-8 form-inline">
                                           <asp:Label ID="txtAssociatedTo" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlAssociatedTo" runat="server" AutoPostBack="true" ValidationGroup="valAddNew"
                        SkinID="ddl_80" OnSelectedIndexChanged="ddlAssociatedTo_SelectedIndexChanged">
                        <asp:ListItem Value="1">Project</asp:ListItem>
                        <asp:ListItem Value="2">Health Check</asp:ListItem>
                        <asp:ListItem Value="3">Global</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlProjectList" runat="server" DataSourceID="objDsProject"
                        AutoPostBack="true" DataTextField="Title" DataValueField="ID" SkinID="ddl_80"
                        OnSelectedIndexChanged="ddlProjectList_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlHealthCheck" runat="server" DataSourceID="objDsHealthCheck"
                        DataTextField="Title" DataValueField="ID" SkinID="ddl_80">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="objDsProject" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataProjects" TypeName="Deffinity.GlobalIssues.ProjectDeviation">
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="objDsHealthCheck" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataHealthChecks" TypeName="Deffinity.GlobalIssues.ProjectDeviation">
                    </asp:ObjectDataSource>
					</div>
				</div>
            </div>
    </asp:Panel>
     <div class="form-group">
                                  <div class="col-md-12">
                                       <asp:Label ID="lblRefID" runat="server" Font-Bold="true"></asp:Label>
                                      </div>
         </div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.DateLogged%></label>
                                      <div class="col-sm-8 form-inline">
                                          <asp:TextBox ID="txtDate" runat="server" MaxLength="10" ValidationGroup="valAddNew"
                        SkinID="Date"></asp:TextBox>
                    <asp:Label ID="Img4" runat="server" SkinID="Calender" />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label">   <%= Resources.DeffinityRes.Status%></label>
                                      <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="Status" DataValueField="ID"
                        SkinID="ddl_80" DataSourceID="objDsStatus">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="objDsStatus" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="DeffinityManager.DAL.PmNewIssuesTableAdapters.adaItemstatus"></asp:ObjectDataSource>
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label">  <%= Resources.DeffinityRes.Issue%></label>
                                      <div class="col-sm-10">
                                           <asp:TextBox ID="txtIssue" runat="server" SkinID="txtMulti" /> <br />
                                          <asp:CheckBox ID="chklesson" runat="server" Text="Add to lessons learnt" />
					</div>
				</div>

				
</div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.AssignedTo%></label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="ddlAssign" runat="server" ValidationGroup="valAddNew" SkinID="ddl_80">
                    </asp:DropDownList>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.Location%></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:DropDownList ID="ddllocation" runat="server" ValidationGroup="valAddNew" SkinID="ddl_80"
                        DataValueField="SiteID" DataSourceID="objSiteByProjectRef" DataTextField="Site">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="objSite" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="DeffinityManager.DAL.PmNewIssuesTableAdapters.adaSite"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="objSiteByProjectRef" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetDataSitesByProjectRef" TypeName="DeffinityManager.DAL.PmNewIssuesTableAdapters.adaSiteByProjectRef">
                        <SelectParameters>
                            <%--<asp:QueryStringParameter Name="ProjectRefrence" DefaultValue="0" QueryStringField="Project" Type="Int32" />--%>
                            <asp:SessionParameter DefaultValue="0" Name="ProjectReference" SessionField="Project"
                                Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.IssueRaisedBy%></label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="ddlIssueraisedBy" runat="server" ValidationGroup="valAddNew"
                        SkinID="ddl_80">
                    </asp:DropDownList>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Category%></label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlQAtype" runat="server" ValidationGroup="valAddNew" SkinID="ddl_80"
                        DataSourceID="objDsIssueType" DataValueField="ID" DataTextField="IssueTypeName">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="objDsIssueType" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="DeffinityManager.DAL.PmNewIssuesTableAdapters.adaIssueType"></asp:ObjectDataSource>
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.CheckedBy%></label>
                                      <div class="col-sm-8">
                                          <asp:DropDownList ID="ddlcheckedby" runat="server" ValidationGroup="valAddNew" SkinID="ddl_80">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="objCheckedBy" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetData" TypeName="DeffinityManager.DAL.PmNewIssuesTableAdapters.adaContractors"></asp:ObjectDataSource>
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.DateChecked%></label>
                                      <div class="col-sm-8 form-inline">
                                            <asp:TextBox ID="txtdatechecked" runat="server" ValidationGroup="valAddNew" SkinID="Date"
                        MaxLength="10"></asp:TextBox>
                    <asp:Label ID="Img5" runat="server" SkinID="Calender" />
					</div>
				</div>

</div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">   <%= Resources.DeffinityRes.DateCompleted%></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:TextBox ID="txtDateCompleted" runat="server" ValidationGroup="valAddNew" SkinID="Date"
                        MaxLength="10"></asp:TextBox>
                    <asp:Label ID="Img6" runat="server" SkinID="Calender" />
					</div>
				</div>
 <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.NextReviewDate%></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:TextBox ID="txtNextReviewDate" runat="server" ValidationGroup="valAddNew" SkinID="Date"
                        MaxLength="10"></asp:TextBox>
                    <asp:Label ID="Image1" runat="server" SkinID="Calender" />
					</div>
				</div>
</div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.RAGStatus%></label>
                                      <div class="col-sm-8">
                                           <asp:DropDownList ID="ddlRagstatus" runat="server" SkinID="ddl_80" ValidationGroup="Submit">
                        <asp:ListItem Text="Please select..." Value="0"></asp:ListItem>
                        <asp:ListItem Text="GREEN" Value="1"></asp:ListItem>
                        <asp:ListItem Text="AMBER" Value="2"></asp:ListItem>
                        <asp:ListItem Text="RED" Value="3"></asp:ListItem>
                    </asp:DropDownList>
					</div>
				</div>
        </div>
    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.ExpectedOutcome%></label>
                                      <div class="col-sm-10">
                                          <asp:TextBox ID="txtExpectedOutcome" runat="server" 
                        ValidationGroup="valAddNew" SkinID="txtMulti"></asp:TextBox>
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="MyCalendar"
                         PopupButtonID="Img4" TargetControlID="txtDate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                         PopupButtonID="Img5" TargetControlID="txtdatechecked">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
                         PopupButtonID="Img6" TargetControlID="txtDateCompleted">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" CssClass="MyCalendar"
                         PopupButtonID="Image1" TargetControlID="txtNextReviewDate">
                    </ajaxToolkit:CalendarExtender>
					</div>
				</div>
        </div>
    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"><%= Resources.DeffinityRes.PMNotes%></label>
                                      <div class="col-sm-10"> <asp:TextBox ID="txtNotes" runat="server"  ValidationGroup="valAddNew"
                        SkinID="txtMulti"></asp:TextBox></div>
                                      </div>
        </div>
    <div class="form-group">
                                  <div class="col-md-12">
                                       <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.ActionPlan%></label>
                                      <div class="col-sm-10"> <asp:TextBox ID="txtActionPlan" runat="server" SkinID="txtMulti"
                        ValidationGroup="valAddNew" ></asp:TextBox>
					</div>
				</div>
        </div>
    <div class="form-group">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> Issue visible to customer</label>
                                      <div class="col-sm-8"><asp:CheckBox ID="chkEnbleCust" runat="server" />
					</div>
				</div>
        </div>
    <div class="form-group" runat="server" id="pnlUploadfile">
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.Uploadfiles%></label>
                                      <div class="col-sm-8 form-inline">
                                           <asp:Panel ID="PnlFileUpload" Font-Bold="true" runat="server" ScrollBars="Auto">
                        <asp:FileUpload ID="FileUpload1" runat="server" maxlength="5" class="multi" />
                        <br />
                    </asp:Panel>
					</div>
				</div>
        </div>
        <div class="form-group" id="Panel_Insert" runat="server">
               <label class="col-sm-2 control-label"></label>
        <div class="col-md-8">
            <span>
             <asp:Button ID="btnAddnew" runat="server" SkinID="btnSubmit" ValidationGroup="valAddNew"
                OnClick="btnAddnew_Click" />
            <asp:Button ID="lnkCancel" runat="server" SkinID="btnCancel"
                OnClick="btnIssueCancel_Click" CausesValidation="false" AlternateText="<%$ Resources:DeffinityRes,Close%>" />
                </span>
        </div>
     </div>
  

    <div class="form-group">
                                  <div class="col-md-12">
                                      <asp:GridView ID="gvCustomerComments" runat="server" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("CommentDate","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Time">
                            <ItemTemplate>
                                <asp:Label ID="lblTime" runat="server" Text='<%# Bind("CommentDate","{0:HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Comments">
                            <ItemTemplate>
                                <asp:Label ID="lblTime" runat="server" Text='<%# Bind("Comments") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Submitted By">
                            <ItemTemplate>
                                <asp:Label ID="lblTime" runat="server" Text='<%# Bind("ContractorName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                                      </div>
        </div>
    <div>
        
    </div>
</asp:Panel>
<asp:HiddenField ID="hidIssueID" runat="server" />
<asp:HiddenField ID="HiddenIssueSection" runat="server" />

<asp:Panel ID="PnlAddNewIssue" runat="server" BackColor="White" Style="display: none" Width="800px"
        BorderStyle="Double" BorderColor="LightSteelBlue" Visible="true">
       <div class="form-group">
        <div class="col-md-10 text-bold">
                     <%= Resources.DeffinityRes.AddIssue%>
             <hr class="no-top-margin" />
        </div>
        <div style="float:right;padding-right:15px;">
            <asp:LinkButton ID="lnkCancelInNewIssue" runat="server" SkinID="BtnLinkCancel" OnClick="lnkCancelInNewIssue_Click"></asp:LinkButton>
        </div>
    </div>
     <div class="form-group">
           <div class="col-md-12">


                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtNewIssue"
                         Display="None" ErrorMessage="<%$ Resources:DeffinityRes, PlsenterIssue %>" ValidationGroup="valAddNewIssue">
                     </asp:RequiredFieldValidator>

                  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlAssigntoInNewIssue"
            Display="None" ErrorMessage="<%$ Resources:DeffinityRes, PlsselectvalidAssignto %>"
            ValidationGroup="valAddNewIssue" InitialValue="0"></asp:RequiredFieldValidator>

                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlCategoryInNewIssue"
            Display="None" ErrorMessage="<%$ Resources:DeffinityRes, PlsselectvalidAssignto %>"
            ValidationGroup="valAddNewIssue" InitialValue="0"></asp:RequiredFieldValidator>

              



                   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valAddNewIssue" />

              



           </div>
     </div>

      <div class="form-group">
            <div class="col-md-12">
                 <label class="col-sm-2 control-label"> <%= Resources.DeffinityRes.Issue%></label>
                <div class="col-sm-9 control-label">
                    <asp:TextBox ID="TxtNewIssue" runat="server" TextMode="MultiLine"></asp:TextBox>
                  
                </div>
            </div>
      </div>
    <div class="form-group">
         <div class="col-md-6">
                 <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Assignto%></label>
                  <div class="col-sm-8 control-label">
                      <asp:DropDownList ID="ddlAssigntoInNewIssue" runat="server"></asp:DropDownList>

                  </div>
         </div>
         <div class="col-md-6">
               <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Category%></label>
                <div class="col-sm-8 control-label">
                    <asp:DropDownList ID="ddlCategoryInNewIssue" runat="server"
                         DataSourceID="objDsIssueType" DataValueField="ID" DataTextField="IssueTypeName"></asp:DropDownList>
                </div>
         </div>
    </div>
    <div class="form-group">
         <div class="col-md-6">
               <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.DateLogged%></label>
                <div class="col-sm-8 control-label form-inline">
                    <asp:TextBox ID="txtDateLoggedInNewIssue" runat="server" SkinID="Date"></asp:TextBox>
                    <asp:Label ID="lbl1" runat="server" SkinID="Calender"></asp:Label>
                      <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" CssClass="MyCalendar"
                         PopupButtonID="lbl1" TargetControlID="txtDateLoggedInNewIssue">
                    </ajaxToolkit:CalendarExtender>
                </div>
         </div>
         <div class="col-md-6">
               <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.NextReviewDate%></label>
                <div class="col-sm-8 control-label form-inline">
                         <asp:TextBox ID="TxtNextReviewDateinNewIssue" runat="server" SkinID="Date"></asp:TextBox>
                          <asp:Label ID="Label1" runat="server" SkinID="Calender"></asp:Label>
                      <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" CssClass="MyCalendar"
                         PopupButtonID="Label1" TargetControlID="TxtNextReviewDateinNewIssue">
                    </ajaxToolkit:CalendarExtender>
                </div>
         </div>
    </div>
     <div class="form-group">
           <div class="col-md-12 full-right" style="text-align:right;">
               <asp:Button ID="btnAddIssue" runat="server" SkinID="btnSubmit" ValidationGroup="valAddNewIssue" OnClick="btnAddIssue_Click" />
           </div>
     </div>
</asp:Panel>

<div>
</div>
