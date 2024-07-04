<%@ Page Language="C#" MasterPageFile="~/WF/CustomerMainTab.master" AutoEventWireup="True" Inherits="CustomerHealthCheckMgmt"
    Title="Health Checks" MaintainScrollPositionOnPostback="true" Codebehind="CustomerHealthCheckMgmt.aspx.cs" %>

<%@ Register TagName="Charts" TagPrefix="Deffinity" Src="~/WF/Portal/Controls/HealthCheckSummaryCharts.ascx" %>
<%@ Register Src="~/WF/Health/Controls/CustomerhealthCheckCntrl.ascx" TagName="CustomerTabs" TagPrefix="uc1" %>
<%--<%@ Register Src="~/controls/CustomerhealthCheckCntrl.ascx" TagName="CustomerTabs" TagPrefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.CustomerPortal%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
    Health check Summary for <%=DateTime.Now.Date.ToString(Deffinity.systemdefaults.GetDateformat()) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
      <uc1:CustomerTabs ID="CustomerTabs1" runat="server" />
    <%--<uc1:CustomerTabs ID="CustomerTabs1" runat="server" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   
       <div>
                <asp:UpdatePanel ID="uPnlCheckListSummary" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="panel1" runat="server">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                DataSourceID="sqlGridfiller" Width="100%">
                                <Columns>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Overall<br />RAG" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="20px" HeaderStyle-CssClass="header_bg_l">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="imgRAG" runat="server" ToolTip=<%#Eval("OverAllStatus").ToString() %>><a href="<%#getRagUrl(Eval("OverAllStatus").ToString()) %>"></a></asp:Label>--%>
                                             <asp:Image ID="imgRAG" runat="server" ToolTip='<%#Eval("OverAllStatus").ToString() %>' ImageUrl='<%#getRagUrl(Eval("OverAllStatus").ToString())%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Form Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHealthCheckTitle" runat="server" Text='<%#Eval("HealthTitle")%>' />
                                        </ItemTemplate>
                                        <ItemStyle Wrap="True" Width="250px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issues">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssue" runat="server" Text='<%#Eval("Issues")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Health Check Item" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHealthCheck" runat="server" Text='<%#Eval("HealthCheck")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Notes">
                                        <ItemTemplate>
                                            <asp:Literal ID="litNotes" runat="server" Text='<%#Eval("Notes")%>' />
                                        </ItemTemplate>
                                        <ControlStyle Width="230px"></ControlStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RAG" ItemStyle-Width="20px" HeaderStyle-CssClass="header_bg_r">
                                        <ItemTemplate>
                                            <asp:Image ID="imgRAG" runat="server" ImageUrl='<%#getRagUrl(Eval("Status").ToString())%>'
                                                ToolTip='<%#Eval("Status").ToString() %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
         
        <div>
            <asp:Panel ID="panel2" runat="server" ScrollBars="Auto">
                <Deffinity:Charts ID="charts" runat="Server" />
            </asp:Panel>
            </div><div>
            <asp:Panel ID="panel3" runat="server" ScrollBars="Auto">
                <asp:GridView ID="gridHCSummary" runat="server" DataSourceID="sqlHCSummary" AutoGenerateColumns="False"
                    HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Bottom" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Health Check" HtmlEncode="false" HeaderText="Form Title<br/>(Complete, In Progress, Pending, Critical and Not Applicable)"
                            HeaderStyle-CssClass="header_bg_l">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Total this Week" HeaderText="Total<br/>this Week" HtmlEncode="false">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <br />
                                <img src="../images/indcate_red .png" alt="Red" align="bottom" />
                            </HeaderTemplate>
                            <HeaderStyle BackColor="Transparent" />
                            <ItemTemplate>
                                <%#Eval("RWeek").ToString()%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <br />
                                <img src="images/indcate_yellow.png" alt="Amber" align="bottom" />
                            </HeaderTemplate>
                            <HeaderStyle BackColor="Transparent" />
                            <ItemTemplate>
                                <%#Eval("AWeek").ToString()%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <br />
                                <img src="images/indcate_green.png" alt="Green" align="bottom" />
                            </HeaderTemplate>
                            <HeaderStyle BackColor="Transparent" />
                            <ItemTemplate>
                                <%#Eval("GWeek").ToString()%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Total this Month" HeaderText="Total<br/>this Month" HtmlEncode="false">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <br />
                                <img src="../images/indcate_red .png" alt="Red" align="bottom" />
                            </HeaderTemplate>
                            <HeaderStyle BackColor="Transparent" />
                            <ItemTemplate>
                                <%#Eval("RMonth").ToString()%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <br />
                                <img src="images/indcate_yellow.png" alt="Amber" align="bottom" />
                            </HeaderTemplate>
                            <HeaderStyle BackColor="Transparent" />
                            <ItemTemplate>
                                <%#Eval("AMonth").ToString()%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <br />
                                <img src="images/indcate_green.png" alt="Green" align="bottom" />
                            </HeaderTemplate>
                            <HeaderStyle BackColor="Transparent" />
                            <ItemTemplate>
                                <%#Eval("GMonth").ToString()%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Total this Year" HeaderText="Total<br/>this Year" HtmlEncode="false">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <br />
                                <img src="../images/indcate_red .png" alt="Red" align="bottom" />
                            </HeaderTemplate>
                            <HeaderStyle BackColor="Transparent" />
                            <ItemTemplate>
                                <%#Eval("RYear").ToString()%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <br />
                                <img src="images/indcate_yellow.png" alt="Amber" align="bottom" />
                            </HeaderTemplate>
                            <HeaderStyle BackColor="Transparent" />
                            <ItemTemplate>
                                <%#Eval("AYear").ToString()%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                            <HeaderTemplate>
                                <br />
                                <img src="images/indcate_green.png" alt="Green" align="bottom" />
                            </HeaderTemplate>
                            <HeaderStyle BackColor="Transparent" />
                            <ItemTemplate>
                                <%#Eval("GYear").ToString()%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sqlHCSummary" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="HealthCheckRAGSummary" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="PortfolioID" SessionField="PortfolioID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </asp:Panel>
            </div>
            <asp:Panel ID="panelHelathCheck" runat="server">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <asp:Label SkinID="Loading" runat="server" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableViewState="false" />
            </asp:Panel>
           
    <div class="form-group">
        <div class="col-md-12">
           <strong> Form Issue Summary</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
            <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"> Form Title</label>
           <div class="col-sm-6">
               <asp:DropDownList ID="ddlHealthCheckTitles" runat="server"></asp:DropDownList>
                    <%--   <asp:DropDownList ID="ddlHealthCheckTitles" runat="server" AppendDataBoundItems="true"
                            DataSourceID="objHealthCheckDDLFiller" DataTextField="Title" DataValueField="ID"
                            ValidationGroup="Issues">
                            <asp:ListItem Text="Please Select.." Value="0" />
                        </asp:DropDownList>--%>
                        <asp:RequiredFieldValidator ID="rqdHealthCheckTitles" runat="server" ControlToValidate="ddlHealthCheckTitles"
                            ErrorMessage="Please select Health Check Title" Text="*" Display="Dynamic" ValidationGroup="Issues" />
                           <%-- <ajaxToolkit:CascadingDropDown ID="ccdForms" runat="server" TargetControlID="ddlHealthCheckTitles"
                                Category="SubName" PromptText="Please select..." PromptValue="0" ServicePath="~/HC/HCWebService.asmx"
                                ServiceMethod="GetForms" ParentControlID="ddlPortfolio" LoadingText="[Loading Forms...]"
                                BehaviorID="ccdrn" ClientIDMode="Static" />--%>
                        <asp:ObjectDataSource ID="objHealthCheckDDLFiller" runat="server" TypeName="DataHelperClass"
                            OldValuesParameterFormatString="original_{0}" SelectMethod="LoadPortfolioHealthCheckTitle" />
            </div>
           <div class="col-sm-3">
               <asp:Button ID="btnView" runat="server" SkinID="btnView" OnClick="btnView_Click" Text="View"
                            ValidationGroup="Issues" />
               </div>
	</div>
	<div class="col-md-6">
           
	</div>
</div>
            
           
            <asp:Panel ID="panelSummary" runat="server" Width="100%">
                <asp:UpdatePanel ID="updatePanelIssueSummary" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gridHealthCheckIssues" runat="server" AutoGenerateColumns="false"
                            Width="100%" EmptyDataText="No issues logged" EnableViewState="false" AllowPaging="true"
                            OnPageIndexChanging="gridHealthCheckIssues_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Date" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-CssClass="header_bg_l">
                                    <ItemTemplate>
                                        <asp:Literal ID="litDate" runat="server" Text='<%#getDate(Eval("IssueDate")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issues" HeaderStyle-CssClass="header_bg_r">
                                    <ItemTemplate>
                                        <asp:Literal ID="litIssues" runat="server" Text='<%#Eval("Issues")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
           
    <div class="form-group">
        <div class="col-md-12">
           <strong>Form History</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
            <asp:Panel ID="PanelHistory" runat="server" Width="100%">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gridHealthChecks" runat="server" AutoGenerateColumns="False" Width="100%"
                            EmptyDataText="No Items Found" EnableViewState="true" AllowSorting="true" OnSorting="gridHealthChecks_Sorting"
                            OnPageIndexChanging="gridHealthChecks_PageIndexChanging" AllowPaging="true" PageSize="5">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Add Issue" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="header_bg_l">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIssue" runat="server" EnableViewState="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email Address" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Literal ID="litID" runat="server" Text='<%#Eval("HealthCheckListID")%>' Visible="false" />
                                        <asp:Literal ID="litMainID" runat="server" Text='<%#Eval("ID")%>' Visible="false" />
                                        <asp:Panel ID="pnlEmailGrid" runat="server" Style="display: none" BackColor="White"
                                            BorderStyle="Double" BorderColor="LightSteelBlue">
                                            <table>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="pnlInnerEmail" runat="server" Height="400px" ScrollBars="Both">
                                                            <asp:GridView ID="gridInner" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                                                                BackColor="White" HorizontalAlign="Justify" DataKeyNames="ID">
                                                                <Columns>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Literal ID="litMailID" runat="server" Text='<%#Eval("ID")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkMailable" runat="server" Checked="false" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Email Address">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEmailID" runat="server" Text='<%#Eval("EmailID")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <div class="sec_header" style="width: 100%">
                                                                        No Email Addresses
                                                                    </div>
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                            <asp:Label ID="lblHealthCheckID" runat="server" Text='<%#Eval("HealthCheckListID")%>'
                                                                Visible="false" />
                                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                                                SelectCommand="SELECT Distinct Mail.ID AS ID,Mail.Name AS Name,Mail.EmailID AS EmailID,ISNULL(HCL_Mail.MailID,0) AS Assigned 
                                    FROM healthchecknamemailid Mail Left Outer Join healthchecklist_healthchecknamemailid HCL_Mail
                                    ON (Mail.ID=HCL_Mail.MailID) WHERE ([PortfolioHealthCheckID] = @PortfolioHealthCheckID)">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="lblHealthCheckID" Name="PortfolioHealthCheckID"
                                                                        PropertyName="Text" Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td align="right">
                                                        <asp:LinkButton ID="lnkPopupClose" runat="server" Text="Close" SkinID="BtnLinkClose" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <ajaxToolkit:ModalPopupExtender ID="popUp" TargetControlID="imgPopUp" PopupControlID="pnlEmailGrid"
                                            BackgroundCssClass="modalBackground" runat="server" CancelControlID="lnkPopupClose" />
                                        <asp:Image ID="imgPopUp" runat="server" ImageUrl="~/WF/media/ico_mail.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PDF">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" ImageUrl="~/WF/Portal/images/icon_pdf.gif" NavigateUrl='<%# "~/WF/Reports/HealthCheckItems.aspx?pdf=true&healthCheckID=" + Eval("ID").ToString()%>'
                                            ID="linkPDF" Text="PDF" Target="_blank" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" SortExpression="DateRaised">
                                    <ItemTemplate>
                                        <asp:Literal ID="litDateRaised" Text='<%#Eval("DateRaised","{0:d}")%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LocationName" HeaderText="Location" SortExpression="LocationName" />
                                <asp:BoundField DataField="HealthCheckTitle" HeaderText="Form" SortExpression="HealthCheckTitle" />
                                <asp:BoundField DataField="AssignedTeamName" HeaderText="Assigned Team" SortExpression="AssignedTeamName" />
                                <asp:TemplateField HeaderText="Overall<br/> RAG" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="20px" HeaderStyle-CssClass="header_bg_r">
                                    <ItemTemplate>
                                        <asp:Image ID="imgRAG" runat="server" ImageUrl='<%#getRagUrl(Eval("Status").ToString())%>'
                                            ToolTip='<%#Eval("Status").ToString() %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Panel ID="PanelIssue" runat="server" Width="100%">
                <div class="form-group">
      <div class="col-md-6">
           <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.Issue%></label>
           <div class="col-sm-6">
                <asp:TextBox ID="txtIssue" runat="server" Width="400px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqIssue" runat="server" Text="*" ErrorMessage="Issue cannot be blank"
                    ControlToValidate="txtIssue" EnableViewState="false" />
            </div>
          <div class="col-sm-3">
               <asp:Button ID="btnUpdateIssue" runat="server" SkinID="btnUpdate"
                    OnClick="btnUpdateIssue_OnClick" />
              </div>
	</div>
	<div class="col-md-6">
           <label class="col-sm-3 control-label"></label>
           <div class="col-sm-9">

            </div>
	</div>
</div>
                <div class="form-group">
          <div class="col-md-12">
               <asp:Label ID="lblMessage" runat="server" ForeColor="Red" EnableViewState="false" />
	</div>
</div>
               
               
               
               
                
                <asp:SqlDataSource ID="sqlGridfiller" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                    SelectCommand="HealthCheckListItemsSelectAllWithOutID_Status" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:SessionParameter Name="PortfolioID" SessionField="PortfolioID" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </asp:Panel>
        
  
 <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
GridResponsiveCss();
 </script> 
</asp:Content>
