<%@ Page Title="" Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="true" Inherits="CustomerProjectOverview" Codebehind="CustomerProjectOverview.aspx.cs" %>


<%@ Register Assembly="Evyatar.Web.Controls" Namespace="Evyatar.Web.Controls" TagPrefix="evy" %>
<%@ Register src="MailControls/IssueCustomerSignOffMail.ascx" tagname="IssueCustomerSignOffMail" tagprefix="uc2" %>
<%--<%@ Register Src="controls/CustomerHomeTabs.ascx" TagName="CustomerHomeTabs" TagPrefix="uc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
   
    <uc2:IssueCustomerSignOffMail ID="IssueCustomerSignOffMail1" runat="server" Visible="false" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_title" runat="Server">
    <%: Resources.DeffinityRes.CustomerPortal %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="panel_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectOverview%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <%-- <asp:UpdatePanel ID="pn" runat="server" >
    <ContentTemplate>--%>
  
  <%--  <script type="text/javascript" language="javascript" src="js/ext-base.js"></script>
    <script type="text/javascript" language="javascript" src="js/ext-core.js"></script>
    <script type="text/javascript" language="javascript" src="js/ext-all.js"></script>--%>
   <Pref:ProjectRef ID="ProjectRef1" runat="server" />
     <div>
                    <%= Resources.DeffinityRes.ProjectTitle%>:<b><label id="lblTitle" runat="server"></label></b>
                </div>
                <div>
                    <%= Resources.DeffinityRes.Owner%>:<b><label id="lblOwner" runat="server"></label></b>
                </div>
                <div>
                    <%= Resources.DeffinityRes.Email%>:<b><label id="lblEmail" runat="server"></label></b>
                </div>
     <asp:Panel ID="PanelCsv1" runat="server" Width="100%" Style="overflow: hidden">
                                <div style="width: 100%; margin-left: 5px">
                                    <iframe id="iframeMpp" height="800px" width="100%" src='<%=RetUrl1() %>' scrolling="auto"
                                        frameborder="0"></iframe>
                                </div>
                            </asp:Panel>
    <div class="form-group">
        <div class="col-md-12">
           <strong><%= Resources.DeffinityRes.Issue%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>


                            <asp:UpdatePanel ID="Issues" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chkIssues" runat="server" Text="Alert me when new issues are added"
                                        AutoPostBack="True" OnCheckedChanged="chkIssues_CheckedChanged" Font-Bold="True" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button ID="imgIssuesSav" runat="server" SkinID="btnSave" OnClick="imgIssuesSav_Click"
                                Visible="False" />
                            <asp:GridView ID="gvBindIssues" runat="server" DataKeyNames="ID" GridLines="None"
                                EmptyDataText="<%$ Resources:DeffinityRes, Nodataexist %>" Width="100%" AutoGenerateColumns="False"
                                HorizontalAlign="Left" BorderColor="White" BorderStyle="None" CellPadding="0"
                               ShowFooter="True" OnRowDataBound="gvBindIssues_RowDataBound"
                                OnRowCommand="gvBindIssues_RowCommand">
                                <Columns>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="header_bg_l"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Reference" HeaderStyle-CssClass="header_bg_l">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkIssueRef" runat="server" Text='<%# Prefix()+ Eval("Projectreference") + "-" + Eval("ID")  %>'
                                                CommandName="cmdEdit" CommandArgument='<%# Bind("ID") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkComments" runat="server" Text="Customer Comments" CommandName="Comments"
                                                CausesValidation="false" CommandArgument='<%# Bind("ID") %> '></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Issue%>">
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
                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Type%>">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssuseType" runat="server" Text='<%# Bind("IssueType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, AssignedTo%>">
                                        <ItemStyle Width="100px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssignto" runat="server" Text='<%# Bind("AssignTo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, ExpectedOutcome%>">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpectedOutcome" runat="server" Text='<%# Bind("ExpectedOutcome") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="110px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Notes%>">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNotes" runat="server" Text='<%# Bind("Notes") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="110px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action Plan">
                                        <ItemTemplate>
                                            <asp:Label ID="lblap" runat="server" Text='<%# Bind("ActionPlan") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="110px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Status%>">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="70px" HeaderText="RAG Status" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Literal ID="Image1" runat="server" Visible='<%#SetVisibleIss(Eval("RAG").ToString())%>'
                                                Text='<%#GetImageissues(Eval("RAG").ToString())%>' />
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSignOff" runat="server" Text='<%# Eval("SignOff").ToString()=="True"?"Signed-off":"Sign-off" %>' CommandName="SignOff" Enabled='<%# GetSignOffEnable(DataBinder.Eval(Container.DataItem, "SignOff").ToString())%>'
                                                CausesValidation="false" CommandArgument='<%# Bind("ID") %> '></asp:LinkButton>
                                                
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" CssClass="header_bg_r" />
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes, Documents%>">
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                        <ItemTemplate>
                         <asp:LinkButton ToolTip="<%$ Resources:DeffinityRes, Clickheretodownloaddocuments%>" runat="server" ID="lnkDocuments" Enabled='<%#GetDocsEnable(DataBinder.Eval(Container.DataItem, "CountFiles").ToString())%>'  CommandArgument='<%# Bind("ID") %>' Text='<%# Eval("CountFiles", " ({0})") %>'  CommandName="cmdViewExt" CausesValidation="false">Here</asp:LinkButton>
                         <HeaderStyle CssClass="header_bg_r" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                            
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetData" TypeName="PmNewIssuesTableAdapters.adaIssues">
                                <SelectParameters>
                                    <asp:Parameter Name="ID" Type="Int32" />
                                    <asp:Parameter Name="IssueType" Type="Int32" />
                                    <asp:Parameter Name="IssueSection" Type="String" />
                                    <asp:Parameter Name="AssignTo" Type="Int32" />
                                    <asp:Parameter Name="Status" Type="Int32" />
                                    <asp:Parameter Name="Issue" Type="String" />
                                    <asp:Parameter Name="FristTime" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
     <div id="Div1" runat="server" style="width: 100%; overflow: auto;">
         <div class="form-group">
        <div class="col-md-12">
           <strong> Project Updates</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
                            
                                <asp:UpdatePanel runat="server" ID="Update">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="chkProjectUpdate" runat="server" Text="Alert me when new project updates are added"
                                            AutoPostBack="True" OnCheckedChanged="chkProjectUpdate_CheckedChanged" Font-Bold="True" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:Button ID="imgProjectUpdateSav" runat="server" SkinID="btnSave" OnClick="imgProjectUpdateSav_Click"
                                    Visible="False" />
                                <asp:GridView ID="GridMeetings" runat="server" Width="100%" AllowSorting="true">
                                    <Columns>
                                        <asp:HyperLinkField HeaderStyle-CssClass="header_bg_l" DataNavigateUrlFields="ProjectReference,ID"
                                            DataNavigateUrlFormatString="~/AddMeeting.aspx?Project={0}&meeting={1}" Text="Edit"
                                            Visible="false">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:HyperLinkField>
                                        <asp:BoundField HeaderStyle-CssClass="header_bg_l" DataField="MeetingDate" DataFormatString="{0:d}"
                                            HtmlEncode="false" HeaderText="Date Logged" ItemStyle-Width="90" />
                                        <asp:BoundField DataField="MeetingTime" HeaderText="Time" ItemStyle-Width="70" />
                                        <asp:TemplateField HeaderText="Project Update" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectUpdate" runat="server" Text='<%# GetShortDescription(Eval("GeneralNotes").ToString(),Eval("ProjectReference").ToString(),Eval("ID").ToString())%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lessons Learnt" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLessonsLearnt" runat="server" Text='<%# GetShortDescription(Eval("LessonsLearnt").ToString(),Eval("ProjectReference").ToString(),Eval("ID").ToString())%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Key Achievements Last Week" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKeyAchievements" runat="server" Text='<%# GetShortDescription(Eval("KeyAchievements").ToString(),Eval("ProjectReference").ToString(),Eval("ID").ToString())%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Key Tasks This Week" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKeyTasks" runat="server" Text='<%# GetShortDescription(Eval("KeyTasks").ToString(),Eval("ProjectReference").ToString(),Eval("ID").ToString())%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="KeyTasks" HeaderText="Key Tasks This Week" Visible="false"
                                            ItemStyle-Width="200" />
                                        <asp:TemplateField HeaderText="RAG Status" SortExpression="RAGStatus" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Literal ID="Image1" runat="server" Visible='<%#SetVisible(Eval("RAGStatus").ToString())%>'
                                                    Text='<%#GetImage(Eval("RAGStatus").ToString())%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:HyperLinkField HeaderStyle-CssClass="header_bg_r" DataNavigateUrlFields="ID"
                                            DataNavigateUrlFormatString="~/Reports/ProjectMeeting.aspx?meeting={0}&customer=0"
                                            Text="Report" Target="_blank" HeaderText="Report"
                                            ItemStyle-Width="50px">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <label>
                                            No Updates logged.</label>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
   
    <asp:Label ID="imgShowpopup1" runat="server" />
    <asp:Label ID="imgShowpopup2" runat="server" />
    <asp:Label ID="imgShowpopup3" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="popIssues1" BackgroundCssClass="modalBackground"
        runat="server" PopupControlID="PaneladdNew" TargetControlID="imgShowpopup1" />
    <ajaxToolkit:ModalPopupExtender ID="popupComment" BackgroundCssClass="modalBackground"
        runat="server" PopupControlID="pnlCustomerComments" TargetControlID="imgShowpopup2" />
         <ajaxToolkit:ModalPopupExtender ID="popupSignOff" BackgroundCssClass="modalBackground"
        runat="server" PopupControlID="pnlSignOff" TargetControlID="imgShowpopup3" />
    <asp:Panel Width="80%" Height="530px" ScrollBars="Auto" runat="server" ID="PaneladdNew"
        BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue">
        <div class="form-group">
        <div class="col-md-12">
           <strong> <%= Resources.DeffinityRes.AddEditIssue_Header%> </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
     
        <div style="float: right">
            <div id="Panel_Insert" runat="server">
                <asp:Button ID="btnAddnew" runat="server" SkinID="btnSubmit" OnClick="btnAddnew_Click"
                    CausesValidation="false" />
                <asp:Button ID="lnkCancel" runat="server" SkinID="btnCancel"
                    OnClick="btnIssueCancel_Click" CausesValidation="false" AlternateText="<%$ Resources:DeffinityRes,Close%>" />
            </div>
        </div>
        <div>
        <asp:Label ID="lblRefID" runat="server" Font-Bold="true"></asp:Label>
        </div>
        <asp:Panel runat="server" ID="pnlAssociated">

            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                 
                    <td class="style3">
                        <asp:Label Text="" ID="lblAssociatedTo" runat="server"></asp:Label><asp:Label ID="txtAssociatedTo" runat="server" Text="" ></asp:Label>
                        <br />
                        <asp:Label ID="lblAssociatedTo0" runat="server" Text="<%$ Resources:DeffinityRes, Associatedto %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAssociatedTo" runat="server" AutoPostBack="true" ValidationGroup="valAddNew"
                            Width="127px">
                            <asp:ListItem Value="1">Project</asp:ListItem>
                            <asp:ListItem Value="2">Health Check</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlProjectList" runat="server" DataSourceID="objDsProject"
                            AutoPostBack="true" DataTextField="Title" DataValueField="ID" Height="22px" Width="240px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlHealthCheck" runat="server" DataSourceID="objDsHealthCheck"
                            DataTextField="Title" DataValueField="ID" Height="21px" Width="203px">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="objDsProject" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetDataProjects" TypeName="Deffinity.GlobalIssues.ProjectDeviation">
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="objDsHealthCheck" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetDataHealthChecks" TypeName="Deffinity.GlobalIssues.ProjectDeviation">
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div>
            <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.DateLogged%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtDate" runat="server" MaxLength="10" Enabled="false" Width="100px"></asp:TextBox>
                        <%-- <asp:Image ID="Img4" runat="server" SkinID="Calender" />--%>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Status%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="Status" DataValueField="ID"
                            Enabled="false" Width="150px" DataSourceID="objDsStatus">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="objDsStatus" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetData" TypeName="DeffinityManager.DAL.PmNewIssuesTableAdapters.adaItemstatus"></asp:ObjectDataSource>
            </div>
	</div>
	<div class="col-md-4">
          
	</div>
</div>
            
<div class="form-group">
      <div class="col-md-12">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Issue%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtIssue" runat="server" Height="63px" TextMode="MultiLine" Width="700px"
                            Enabled="false" />
            </div>
	</div>

</div>
            <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.AssignedTo%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlAssign" runat="server" ValidationGroup="valAddNew" Width="200px"
                            Enabled="false">
                        </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Location%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddllocation" runat="server" ValidationGroup="valAddNew" Width="200px"
                            Enabled="false" DataValueField="SiteID" DataSourceID="objSiteByProjectRef" DataTextField="Site">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="objSite" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetData" TypeName="PmNewIssuesTableAdapters.adaSite"></asp:ObjectDataSource>
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
	<div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.IssueRaisedBy%></label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlIssueraisedBy" runat="server" ValidationGroup="valAddNew"
                            Width="200px" Enabled="false">
                        </asp:DropDownList>
            </div>
	</div>
</div>
            <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.Category%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlQAtype" runat="server" ValidationGroup="valAddNew" Width="203px"
                            DataSourceID="objDsIssueType" DataValueField="ID" DataTextField="IssueTypeName"
                            Enabled="false">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="objDsIssueType" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetData" TypeName="DeffinityManager.DAL.PmNewIssuesTableAdapters.adaIssueType"></asp:ObjectDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.CheckedBy%></label>
           <div class="col-sm-9">
               <asp:DropDownList ID="ddlcheckedby" runat="server" ValidationGroup="valAddNew" Width="200px"
                            Enabled="false">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="objCheckedBy" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetData" TypeName="DeffinityManager.DAL.PmNewIssuesTableAdapters.adaContractors"></asp:ObjectDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.DateChecked%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtdatechecked" runat="server" ValidationGroup="valAddNew" Width="100px"
                            MaxLength="10" Enabled="false"></asp:TextBox>
            </div>
	</div>
</div>
            <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.DateCompleted%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtDateCompleted" runat="server" ValidationGroup="valAddNew" Width="100px"
                            MaxLength="10" Enabled="false"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.NextReviewDate%></label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtNextReviewDate" runat="server" ValidationGroup="valAddNew" Width="100px"
                            MaxLength="10" Enabled="false"></asp:TextBox>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-3 control-label"> RAG&nbsp;Status</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlRagstatus" runat="server" Width="175px" ValidationGroup="Submit">
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
           <label class="col-sm-3 control-label"> <%= Resources.DeffinityRes.ExpectedOutcome%></label>
           <div class="col-sm-9">
               <asp:TextBox ID="txtExpectedOutcome" runat="server" Height="63px" TextMode="MultiLine"
                            ValidationGroup="valAddNew" Width="700px" Enabled="false"></asp:TextBox>
            </div>
	</div>
</div>
            <div class="form-group">
      <div class="col-md-12">
           <label class="col-sm-3 control-label">PM Notes</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtNotes" runat="server" Height="63px" TextMode="MultiLine" ValidationGroup="valAddNew"
                            Width="700px" Enabled="false"></asp:TextBox>
            </div>
	</div>
	
</div>
            <div class="form-group">
      <div class="col-md-12">
           <label class="col-sm-3 control-label">Action Plan</label>
           <div class="col-sm-9">
                <asp:TextBox ID="txtActionPlan" runat="server" Height="63px" TextMode="MultiLine"
                            ValidationGroup="valAddNew" Width="700px" Enabled="false"></asp:TextBox>
            </div>
	</div>
	
</div>
           
        </div>
    </asp:Panel>
    
    <asp:Panel Width="60%" Height="530px" ScrollBars="Auto" runat="server" ID="pnlCustomerComments"
        BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue">
         <asp:UpdatePanel ID="UpdatePanelComments" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 98%;">
            Customer Comments
        </div>
        <div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="valComments"
                DisplayMode="BulletList" />
        </div>
        <div>
        
        <table>
        <tr>
<td>Comments</td>
<td><asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="80px" Width="500px"></asp:TextBox> 
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter comments"
                ControlToValidate="txtComments" ValidationGroup="valComments" SetFocusOnError="true">*</asp:RequiredFieldValidator><br /></td>
        </tr>
            
            <tr>
            <td>&nbsp</td>
            <td><asp:Button ID="btnSubmit" runat="server" SkinID="btnSubmit" OnClick="btnSubmit_Click"
                ValidationGroup="valComments" />
            <asp:Button ID="btnCancel" runat="server" SkinID="btnCancel"
                CausesValidation="false" AlternateText="<%$ Resources:DeffinityRes,Close%>" OnClick="btnCancel_Click" /></td>
            </tr>
              </table>
           
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
  
    </ContentTemplate>
    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="click" />                                        
                                    </Triggers>
            </asp:UpdatePanel>
              </asp:Panel>
            
    <asp:Panel Width="40%" Height="200" ScrollBars="Auto" runat="server" ID="pnlSignOff"
        BackColor="White" BorderStyle="Double" BorderColor="LightSteelBlue">
        <div class="tab_subheader" style="border-bottom: solid 1px Silver; width: 98%;">
            Customer Sign-off
        </div>
        
        By signing off this issue indicates that you are happy with the outcome and that
        the issue has been resolved satisfactorily.
        <br />
        <table>
            <tr>
                <td>
                    <asp:CheckBox ID="chkAccepted" runat="server" Text="Accepted" />
                </td>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    Date and Time
                </td>
                <td>
                    <asp:Label ID="lblDateTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
            <td>&nbsp</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSignOffSubmit" runat="server" SkinID="btnSubmit" ValidationGroup="valComments"
                        OnClick="btnSignOffSubmit_Click" CausesValidation="false" />
                    <asp:Button ID="btnSignOffCancel" runat="server" SkinID="btnCancel"
                        CausesValidation="false" AlternateText="<%$ Resources:DeffinityRes,Close%>" OnClick="btnSignOffCancel_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="hidIssueID" runat="server" />
    <asp:HiddenField ID="HiddenIssueSection" runat="server" />
    <asp:HiddenField ID="hfIssueID" runat="server" Value="0" />
    <%--</ContentTemplate>
  
    </asp:UpdatePanel>--%>
    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script> 
</asp:Content>
