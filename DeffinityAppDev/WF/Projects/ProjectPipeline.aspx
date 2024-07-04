<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Projects_ProjectPipleline" Title="Deffinity" Codebehind="ProjectPipeline.aspx.cs" EnableEventValidation="false" %>

<%@ Register Src="controls/PipelineTab.ascx" TagName="PipelineTab" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:PipelineTab ID="PipelineTab1" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.ProjectPipeline%>
 </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="panel_title" runat="Server">
    <%= Resources.DeffinityRes.SearchDetails%>
    </asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
   
     <asp:UpdatePanel runat="server" ID="Updatesearch">
        <ContentTemplate>
                            <div class="form-group">
                                <div class="col-md-6">
                                     <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Customer%></label>
									<div class="col-sm-8">
                                     <asp:DropDownList ID="ddlPortfolio" runat="server" DataTextField="PortFolio" DataValueField="ID"
                                             DataSourceID="SqlDataSourceTitle2" AutoPostBack="True" OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSourceTitle2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                            SelectCommand="Project_PermissionCustomer" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" DefaultValue="0" />
                                               
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        </div>
                                    </div>
                                 <div class="col-md-6">
                                      <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Programme%></label>
                                     <div class="col-sm-8">
                                      <asp:DropDownList ID="ddlprogram" runat="server"  OnSelectedIndexChanged="ddlprogram_SelectedIndexChanged"
                                            AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="OperationsOwners"
                                            DataValueField="ID">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                            SelectCommand="Project_AssignedProgramme" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" DefaultValue="0" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                         </div>
                                    </div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-6">
                                     <label class="col-sm-4 control-label">   <%= Resources.DeffinityRes.SubProgramme%></label>
                                    <div class="col-sm-8">
                                     <asp:DropDownList ID="ddlsubprogram" runat="server"  DataSourceID="SqlDataSource1"
                                            DataValueField="ID" DataTextField="OPERATIONSOWNERS">
                                            <%--<asp:ListItem Text=" Please select..." Value="0" Selected="True"></asp:ListItem> --%>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                            SelectCommand="Project_AssignedSubProgramme" SelectCommandType="StoredProcedure" ProviderName="<%$ ConnectionStrings:DBstring.ProviderName %>">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="UserID" SessionField="UID" Type="Int32" DefaultValue="0" />
                                                <asp:ControlParameter ControlID="ddlprogram" DefaultValue="0" Name="PROGRAMMEID"
                                                    PropertyName="SelectedValue" Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        </div>
                                    </div>
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.Site%></label>
                                      <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlsite" runat="server" DataSourceID="ObjDS_Site" DataTextField="Site"
                                            DataValueField="Site" >
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="ObjDS_Site" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="Sitelist" TypeName="Deffinity.Bindings.DefaultDatabind">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="0" Name="PortfolioID" SessionField="Portfolio"
                                                    Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                          </div>
                                    </div>
                                 </div>

                            <div class="form-group">
                                <div class="col-md-6">
                                     <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.Owner%></label>
                                     <div class="col-sm-8">
                                       <asp:DropDownList ID="ddlowner" runat="server">
                                        </asp:DropDownList>
                                         </div>
                                    </div>
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label">  <%= Resources.DeffinityRes.ProjectDescription%> </label>
                                    <div class="col-sm-8">
                                     <asp:TextBox ID="txtprojectdesc" runat="server" ></asp:TextBox>
                                        </div>
                                    </div>
                                 </div>

                            <div class="form-group">
                                <div class="col-md-6">
                                     <label class="col-sm-4 control-label">
                                            <%= Resources.DeffinityRes.ProjectReference%></label>
                                     <div class="col-sm-8">
                                         <input type="text" id="lblProjectPrefix" class="col-sm-2 form-control" runat="server" style="width: 70px;" maxlength="3" />
                                         <asp:TextBox ID="txtProjectReference" runat="server" MaxLength="10" SkinID="txt_70"></asp:TextBox>
                                         </div>
                                    </div>
                                  <div class="col-md-6">
                                       <label class="col-sm-4 control-label"> Country</label>
                                     <div class="col-sm-8">
                                      <asp:DropDownList ID="ddlCountry" runat="server" >
                                        </asp:DropDownList>
                                         </div>
                                    </div>
                                 </div>
                               
                            <div class="form-group">
                                 <div class="col-md-12 col-md-offset-8">
                                       <asp:Button ID="btn_Searchprojects" runat="server" OnClick="btn_Searchprojects_Click" SkinID="btnSearch" />
                                        <asp:Button ID="btn_clear" runat="server" OnClick="btn_clear_Click" SkinID="btnClear" />
                                     </div>
                              
                                </div>
             </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_Searchprojects" />
        </Triggers>
    </asp:UpdatePanel>
             
    <asp:Panel ID="pnlProjectGrid" runat="server" CssClas="row"  >
         <div class="col-md-12">
         <h3 class="panel-title"><label id="lblHead" runat="server">
                    </label></h3>
             <hr />
            </div>

        
<div class="form-group">
      <div class="col-md-12">
          <asp:Label ID="lblmsg" runat="server" EnableViewState="false"></asp:Label>
	</div>

</div>
                        
    <div class="form-group">
      <div class="col-md-6">
          <asp:Button ID="btncopyproject"  runat="server" SkinID="btnDefault" Text="Copy To a New Project" OnClick="btncopyproject_Click" />
	</div>
	<asp:Panel CssClass="col-md-6 pull-right" Style="padding-right:15px;float:right;" runat="server" id="pnlCreateProject">
        <div style="float:right;">
          <asp:HyperLink ID="btnNewProject" runat="server" SkinID="ButtonOrange" Text="Create a New Project" NavigateUrl="~/WF/Projects/ProjectOverviewV4.aspx?project=0"></asp:HyperLink>
            </div>
	</asp:Panel>
	
</div>
                       <asp:GridView ID="gviewclientprojectstatus" runat="server" AutoGenerateColumns="False"
                            Width="100%" GridLines="None" OnSelectedIndexChanging="gviewclientprojectstatus_SelectedIndexChanging"
                            EmptyDataText="<%$ Resources:DeffinityRes,NoRecordsExists%>" AllowPaging="True"
                            OnPageIndexChanging="gviewclientprojectstatus_PageIndexChanging" PageSize="25"
                            OnRowDataBound="gviewclientprojectstatus_RowDataBound" AllowSorting="True" OnSorting="gviewclientprojectstatus_Sorting"
                            OnRowCommand="gviewclientprojectstatus_RowCommand" SkinID="gv_responsive" >
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="20px" HeaderStyle-Width="20px" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="rdbGVRow1" runat="server" />
                                       
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID" HeaderText="<%$ Resources:DeffinityRes,ID%>" Visible="False" />
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ProjectReference%>" SortExpression="ProjectReference"  >
                                    <ItemStyle Width="50px" />
                                    <ItemTemplate>
                                         <asp:HiddenField ID="HID" runat="server" Value='<%# Bind("ProjectReference") %>' />
                                        <%--<asp:HyperLink ID="hpref" runat="server" Text="<%# Bind("ProjectReference") %>" NavigateUrl="~/BuildProject.aspx?ProjectReference="+<%# Bind("ProjectReference") %>+"--%>
                                        <a href="./ProjectOverviewV4.aspx?project=<%# DataBinder.Eval(Container.DataItem, "ProjectReference")%>">
                                            <%# DataBinder.Eval(Container.DataItem, "Project")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false" >
                                    <ItemTemplate>
                                        <a href="#" onclick="window.open('LoginJournal.aspx?project=<%#DataBinder.Eval(Container.DataItem,"ProjectReference")%>',
        null,'height=450 width=500 scrollbars=yes')">
                                           <i class="fa fa-history"></i></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Watch" Visible="false" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProjectTitle" HeaderText="<%$ Resources:DeffinityRes,ProjectTitle%>"
                                    SortExpression="ProjectTitle" HeaderStyle-CssClass="optional" />
                                <asp:TemplateField HeaderText="BoM">
                                    <ItemTemplate>
                                        <a href="./ProjectBOM.aspx?project=<%# DataBinder.Eval(Container.DataItem, "ProjectReference")%>">
                                            BoM</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GR" Visible="false">
                                    <ItemTemplate>
                                        <a href="./GoodsReceipt.aspx?project=<%# DataBinder.Eval(Container.DataItem, "ProjectReference")%>">
                                            GR</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="OwnerName" HeaderText="Owner" SortExpression="Owner"  />
                                <asp:BoundField DataField="PortfolioName" HeaderText="<%$ Resources:DeffinityRes,Customer%>"
                                    SortExpression="PortfolioName"  />
                                <asp:BoundField DataField="ProjectStatusName" HeaderText="<%$ Resources:DeffinityRes,Status%>"
                                    SortExpression="ProjectStatusName" Visible="false" />
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TargetDate%>" SortExpression="ProjectEndDate">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "ProjectEndDate", "{0:d}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SiteName" HeaderText="<%$ Resources:DeffinityRes,SiteName%>"
                                    SortExpression="SiteName" HeaderStyle-CssClass="optional" />
                               
                                <asp:TemplateField HeaderText="Over Budget">
                                    <ItemTemplate>
                                        
                                        <asp:Literal ID="imgOverBudget" runat="server" Text='<%#LoadOverBudget((double)Eval("OverBudget"))%>'></asp:Literal>
                                    </ItemTemplate>
                                     <ItemStyle Width="5px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Overdue Tasks">
                                    <ItemTemplate>
                                        <a href="#" onclick="window.open('OverdueTaskItems.aspx?project=<%#DataBinder.Eval(Container.DataItem,"ProjectReference")%>',null,'height=450 width=750 scrollbars=yes')">
                                            <%#DataBinder.Eval(Container.DataItem,"OverdueTasks")%>
                                        </a>
                                    </ItemTemplate>
                                    <ItemStyle Width="5px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Late">
                                    <ItemTemplate>
                                        <asp:Literal ID="imgLate" runat="server" Text='<%#LoadLate((int)Eval("Late"))%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="5px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Variation%>" ItemStyle-HorizontalAlign="Right"
                                    Visible="false">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "variation")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <a href="#" onclick="window.open('TaskItemsPopup.aspx?project=<%#DataBinder.Eval(Container.DataItem,"ProjectReference")%>&ragstatus=red',null,'height=450 width=750 scrollbars=yes')">
                                            <span  style="color:red;" 
                                                runat="server" id="imgRed" visible='<%#getImage(DataBinder.Eval(Container.DataItem, "StatusRed").ToString())%>' ><i class="fa fa-circle"></i></span>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <a href="#" onclick="window.open('TaskItemsPopup.aspx?project=<%#DataBinder.Eval(Container.DataItem,"ProjectReference")%>&ragstatus=amber',null,'height=450 width=750 scrollbars=yes')">
                                            <span style="color:yellow;" alt="<%$ Resources:DeffinityRes,Amber%>"
                                                runat="server" id="imgAmber" visible='<%#getImage(DataBinder.Eval(Container.DataItem, "StatusAmber").ToString())%>' ><i class="fa fa-circle"></i> </span> </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Priority%>" Visible="false">
                                    <ItemTemplate>
                                     
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:HyperLinkField DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Projects/ProjectMeetings.aspx?project={0}"
                                    Text="<i class='fa fa-arrow-circle-o-up'></i>"
                                    Target="_blank" HeaderText="Updates" >
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:HyperLinkField>
                                <asp:HyperLinkField DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Reports/Tasks.aspx?Project={0}"
                                    Text="tasks" Target="_blank" HeaderText="<%$ Resources:DeffinityRes,ProjectTasks%>"
                                    Visible="false">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:HyperLinkField>
                                <%-- <asp:HyperLinkField DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/Reports/InvoiceSummary.aspx?Project={0}"
                    Text="&lt;img src='media/ico_invoice_sumamry.png' border='0'/&gt;" Target="_blank" HeaderText="<%$ Resources:DeffinityRes,InvoiceSummary%>">
            <ItemStyle HorizontalAlign="Center" Width="50px" />
        </asp:HyperLinkField> --%>
                                <asp:HyperLinkField DataNavigateUrlFields="ProjectReference,CountryID,ProgrammeId,SubProgrammeId"
                                    DataNavigateUrlFormatString="~/WF/Reports/ProjectStatusReportViewer.aspx?Projects={0}&amp;countryid={1}&amp;programmeid={2}&amp;subprogrammeid={3}"
                                    Text="<i class='fa fa-qrcode'></i>" Target="_blank" HeaderText="Project Report" HeaderStyle-CssClass="optional">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:HyperLinkField>
                                <asp:HyperLinkField DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Reports/RiskReport.aspx?Project={0}"
                                    Text="risk" Target="_blank"
                                    HeaderText="<%$ Resources:DeffinityRes,RiskIssuesReport%>" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:HyperLinkField>
                                <asp:HyperLinkField DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Reports/ProjectReport.aspx?Project={0}"
                                    Text="rpt" Target="_blank"
                                    HeaderText="<%$ Resources:DeffinityRes,ProjectReport%>" Visible="False">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:HyperLinkField>
                                <asp:HyperLinkField Text="timesheet"
                                    DataNavigateUrlFields="ProjectReference" DataNavigateUrlFormatString="~/WF/Reports/Timeandexpenses.aspx?Project={0}"
                                    Target="_blank" HeaderText="<%$ Resources:DeffinityRes,TimeandExpenses%>" Visible="false">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:HyperLinkField>
                                <asp:HyperLinkField Text="<%$ Resources:DeffinityRes,VarianceReport%>" DataNavigateUrlFields="ProjectReference"
                                    Visible="False" />
                                <asp:HyperLinkField HeaderStyle-CssClass="header_bg_r" Visible="false" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="50px" HeaderText="<%$ Resources:DeffinityRes,ManageInvoices%>"
                                    Text="invoice" DataNavigateUrlFields="ProjectReference"
                                    DataNavigateUrlFormatString="~/WF/Projects/ProjectFinancials.aspx?Project={0}&Invoice=Invoice">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="CustomerReference" HeaderText="Customer PO Number" SortExpression="CustomerReference" HeaderStyle-CssClass="optional"/>
                                <asp:TemplateField HeaderText="PO Days Remaining" ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100px" />
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblDays" runat="server" Text='<%#Bind("Days")%>' Visible="true"></asp:Label>--%>
                                        <%--<asp:Label ID="Label3" runat="server" Text='<%#ReturnDays(DataBinder.Eval(Container.DataItem,"Days").ToString())%>'></asp:Label>--%>
                                       <%-- <asp:Label ID="Label3" runat="server" Visible='<%#POVisible(DataBinder.Eval(Container.DataItem,"POCheck").ToString())%>'
                                            ForeColor='<%#foreColor(DataBinder.Eval(Container.DataItem,"Days").ToString())%>'
                                            Text='<%#ReturnDays(DataBinder.Eval(Container.DataItem,"Days").ToString(),DataBinder.Eval(Container.DataItem,"CustomerReference").ToString(),DataBinder.Eval(Container.DataItem,"DDays").ToString(),DataBinder.Eval(Container.DataItem,"TotalHrs").ToString())%>'></asp:Label>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="% PO Remaining" ItemStyle-VerticalAlign="Top" ItemStyle-Width="100px"
                                    ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblCR" runat="server" Text='<%#Bind("CustomerReference")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblDDays" runat="server" Text='<%#Bind("DDays")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblTotalDays" runat="server" Text='<%#Bind("TotalHrs")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblDyas" runat="server" Text='<%#Bind("Days")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblProgress" runat="server" Text="" Visible='<%#POVisible(DataBinder.Eval(Container.DataItem,"POCheck").ToString())%>'></asp:Label>
                                        <asp:Label ID="lblPer" runat="server" Text='<%# Bind("per")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Comments%>" Visible="false">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Project")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>

     <asp:Panel ID="PanlePending" runat="server" Width="100%" Visible="false">
       
    </asp:Panel>
    <asp:Panel ID="PanelCpl" runat="server" Visible="false" Width="100%">
         <div class="col-md-12">
         <h3 class="panel-title"><%= Resources.DeffinityRes.ListofCompletedProjects%></h3>
             <hr />
             </div>
       
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" EmptyDataText="
<%$ Resources:DeffinityRes,NoRecordsExists%>" GridLines="None" Width="100%" AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging"
                        PageSize="25" OnRowDataBound="GridView2_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="<%$ Resources:DeffinityRes,ID%>" Visible="False" />
                            <asp:BoundField DataField="ProjectReference" HeaderText="<%$ Resources:DeffinityRes,ProjectReference%>"
                                Visible="false" />
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,ProjectReference%>" ItemStyle-Width="125px"
                                HeaderStyle-Width="125px">
                                <ItemTemplate>
                                    <a href='ProjectOverviewV4.aspx?project=<%# DataBinder.Eval(Container.DataItem, "ProjectReference")%>'>
                                        <%# DataBinder.Eval(Container.DataItem, "Project")%></a>
                                </ItemTemplate>
                                <ItemStyle Width="125px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProjectTitle" HeaderText="<%$ Resources:DeffinityRes,ProjectTitle%>" />
                            <asp:BoundField DataField="PortfolioName" HeaderText="<%$ Resources:DeffinityRes,Customer%>" />
                            <asp:BoundField DataField="CostCentre" HeaderText="<%$ Resources:DeffinityRes,CostCentre%>"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,TargetDate%>" ItemStyle-HorizontalAlign="Center"
                                SortExpression="StartDate">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "StartDate", "{0:d}")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="SiteName" HeaderText="<%$ Resources:DeffinityRes,SiteName%>" />
                            <asp:TemplateField HeaderText="<%$ Resources:DeffinityRes,Archive%>" Visible="false">
                                <ItemStyle />
                                <ItemTemplate>
                                    <%# Archive(DataBinder.Eval(Container.DataItem, "ProjectReference").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Approval(DataBinder.Eval(Container.DataItem, "ProjectReference").ToString(), DataBinder.Eval(Container.DataItem, "ProjectStatusID").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
    </asp:Panel>
          <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    //GridResponsiveCss();
    //$(window).load(function () {
    //    $("button:contains('Display all')").click(function (e) {
    //        e.preventDefault();
    //        $(".dropdown-menu li")
    //  .find("input[type='checkbox']")
    //  .prop('checked', 'checked').trigger('change');
    //    });
    //});
 </script> 
    <script type="text/javascript">
        //$(document).ready(function () {
        //    setpagename('Project Pipeline');
        //    //
        //    //btn-toolbar
           
        //});
        //$(window).bind("load", function () {
        //    //$('.dropdown-btn-group').appendTo('.btnCopy');
        //});
        </script>
    <script type="text/javascript">

        grid_responsive();
        // grid_responsive_display();

        $(window).load(function () {
          //  $(".dropdown-menu li")
          //.find("input[type='checkbox']")
          //.prop('checked', 'checked').trigger('change');
            //hide columns in display bord
            [0,2].map(function (item) {
                $(".checkbox-row").eq(item).hide();
            })
            //$(".btn-toolbar").hide();
            //var cols = [];
            //$(".dropdown-menu li").each(function () {
            //    $(this).hide();
            //});
            //$(".checkbox-row").eq(1).hide();
            //$(".dropdown-menu li[class='checkbox-row']").each([0, 1], function (index, value) {
            //    $(".checkbox-row").eq(value).hide();
            //});

            $("button:contains('Display all')").click(function (e) {
                e.preventDefault();
                $(".dropdown-menu li")
          .find("input[type='checkbox']")
          .prop('checked', 'checked').trigger('change');
            });
           
        });
    </script>
</asp:Content>
