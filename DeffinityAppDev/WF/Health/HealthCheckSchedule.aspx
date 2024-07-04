<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="HealthCheckSchedule" Title="Health Check Schedule" Codebehind="HealthCheckSchedule.aspx.cs" %>

<%--<%@ Register Src="controls/RFIVendorMainTabNew.ascx" TagName="RFIVendorTabsNew" TagPrefix="ucNew1" %>--%>
<%@ Register Src="~/WF/Resource/Controls/MyProjectsTab.ascx" TagName="ProjectStatus" TagPrefix="uc1" %>
<%@ Register Src="controls/HealthCheckCtrl.ascx" TagName="HealthCheckCtrl" TagPrefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Tabs" runat="Server">
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <asp:Label SkinID="Loading" runat="server"></asp:Label>
        </ProgressTemplate>
    </asp:UpdateProgress>
    
    <script type="text/javascript" >
        //Check all emailId in the inner grid 
        function checkAll(checkbox) {
            var ext = checkbox.id;
            var tbl = $(checkbox).parents("table:first");
            var tblid = tbl.get(0).id;
            debugger;
            var innerGridId = ext.substring(0, ext.indexOf("gridInner"));
            var gvInner = document.getElementById(tblid);//document.getElementById(innerGridId + "gridInner");
            for (i = 1; i < gvInner.rows.length - 1; i++) {
                gvInner.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = checkbox.checked;
            }
        }
    </script>
    <uc1:ProjectStatus ID="tabControl" runat="server" />
    <uc2:HealthCheckCtrl ID="HealthCheckCtrl1" runat="server" Visible="false" />
   <%-- <ucNew1:RFIVendorTabsNew ID="RFIVendorTabs1" runat="server" Visible="false" />--%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.HealthChecks%>
    </asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="panel_title" runat="Server">
   <%= Resources.DeffinityRes.HealthCheckSchedule%>
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    
                <asp:Panel ID="pnlSearch" runat="server">

                    
<div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><asp:Label ID="lblPortfolio" Text='<%$ Resources:DeffinityRes,Customer%>' runat="server" /></label>
           <div class="col-sm-8"><asp:DropDownList ID="ddlPortfolio" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged" DataSourceID="objPortfolio"
                                        DataTextField="Portfolio" DataValueField="ID">
                                        <asp:ListItem Text="Please Select.." Value="0" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rqdPortfolio" runat="server" ErrorMessage="Please select customer"
                                        Display="Dynamic" ControlToValidate="ddlPortfolio" InitialValue="0" Text="*" />

            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><asp:Label ID="lblHealthCheckTitle" runat="server" Text='<%$ Resources:DeffinityRes,HealthChecks%>'></asp:Label></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlHealthCheckTitle" runat="server" DataSourceID="objHealthCheckDDLFiller"
                                        DataTextField="Title" DataValueField="ID" AppendDataBoundItems="true">
                                        <asp:ListItem Text="Please Select.." Value="0" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rdqHealthCheckTitle" runat="server" ControlToValidate="ddlHealthCheckTitle"
                                        InitialValue="0" Text="*" Display="Dynamic" ErrorMessage="Please select a health check title"
                                        ValidationGroup="Form" />
                                    <asp:ObjectDataSource ID="objHealthCheckDDLFiller" runat="server" TypeName="DataHelperClass"
                                        OldValuesParameterFormatString="original_{0}" SelectMethod="LoadPortfolioHealthCheckTitle" />
            </div>
	</div>
	<div class="col-md-4">
          
	</div>
</div>

                    
<div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><asp:Label ID="lblLocation" runat="server" Text='<%$ Resources:DeffinityRes,Location%>'></asp:Label></label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlLocation" runat="server" DataSourceID="objLocationDDLFiller"
                                        DataTextField="Site" DataValueField="ID" AppendDataBoundItems="false" OnDataBound="ddlLocation_DataBound">
                                        <asp:ListItem Text="Please Select.." Value="0" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rqdLocation" runat="server" ControlToValidate="ddlLocation"
                                        InitialValue="0" Text="*" Display="Dynamic" ErrorMessage="Please select a location"
                                        ValidationGroup="Form" />
                                    <asp:ObjectDataSource ID="objLocationDDLFiller" runat="server" TypeName="DataHelperClass"
                                        OldValuesParameterFormatString="original_{0}" SelectMethod="LoadSiteByPortfolio">
                                        <SelectParameters>
                                            <asp:ControlParameter Name="portfolioID" ControlID="ddlPortfolio" PropertyName="SelectedValue"
                                                DefaultValue="0" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><asp:Label ID="lblDate" runat="server" Text='<%$ Resources:DeffinityRes,Date%>'></asp:Label></label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtDate" runat="server" SkinID="Date"></asp:TextBox>
                                    <asp:Label ID="imgCalendar" runat="server" SkinID="Calender" />
                                    <asp:RequiredFieldValidator ID="rqdDate" runat="server" ControlToValidate="txtDate"
                                        ErrorMessage="Date is required" Text="*" ValidationGroup="Form" Display="Dynamic" />
                                    <asp:CompareValidator ID="cmpDate" runat="server" ControlToValidate="txtDate" Operator="DataTypeCheck"
                                        Type="Date" ValidationGroup="Form" Text="*" ErrorMessage="Invalid Date" />
                                    <ajaxtoolkit:calendarextender id="calDate" runat="server"  cssclass="MyCalendar"
                                        targetcontrolid="txtDate" popupbuttonid="imgCalendar" />
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><asp:Label ID="lblTime" runat="server" Text='<%$ Resources:DeffinityRes,Time%>' /></label>
           <div class="col-sm-8 form-inline">
               <asp:TextBox ID="txtTime" runat="server" MaxLength="5" SkinID="Time" />
                                    <asp:RequiredFieldValidator ID="rqdTime" runat="server" Text="*" ErrorMessage="Time Required"
                                        ControlToValidate="txtTime" ValidationGroup="Form" />
                                    <asp:RegularExpressionValidator ID="rgeTime" runat="server" ControlToValidate="txtTime"
                                        Display="Dynamic" ErrorMessage="Invalid Time" Text="*" ValidationExpression="^((0?[1-9]|1[012])(:[0-5]\d){0,2}(\ M))$|^([01]\d|2[0-3])(:[0-5]\d){1,2}$"
                                        ValidationGroup="Form" />
                                    <span style="color: Gray">&nbsp;(HH:MM)</span>
            </div>
	</div>
</div>

                   
                    <div class="form-group pull-right">
          <div class="col-md-12">
                        <asp:Button  ID="ImageButton1" runat="server" SkinID="btnDefault" Text="Create" ValidationGroup="Form"
                            OnClick="ImageButton1_Click" />
                    </div>
                        </div>
                </asp:Panel>
                 <div class="form-group">
                    <div class="col-md-12">
                    <asp:Label ID="lblMessage" runat="server" EnableViewState="false" ForeColor="Red" />
                </div>
                     </div>
     <div class="form-group">
                    <div class="col-md-12">
                <asp:Panel ID="pnlGrid" runat="server" ScrollBars="None" Width="100%" Height="100%">
                    <%--<asp:UpdatePanel ID="gridUpdatePanel" runat="server">
            <ContentTemplate>--%>
                    <asp:GridView ID="gridHealthChecks" runat="server" DataSourceID="objGridFiller" AutoGenerateColumns="False"
                        OnRowDeleting="gridHealthChecks_RowDeleting" Width="100%" AllowSorting="true"
                        AllowPaging="true" PageSize="20" OnRowCreated="gridHealthChecks_RowCreated" EmptyDataText="No Items Found"
                        OnSelectedIndexChanging="gridHealthChecks_SelectedIndexChanging" OnRowDataBound="gridHealthChecks_RowDataBound">
                        <Columns>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblHealthCheckID" runat="server" Text='<%#Eval("HealthCheckListID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink_main" SkinID="Editicon" 
                                        ToolTip="Edit" runat="server" NavigateUrl='<%# "HealthCheckForm.aspx?R=Y&type=main&HealthCheckId=" + Eval("ID").ToString()+"&PID="+Eval("HealthCheckListID").ToString()%>' />
                                    <asp:HyperLink ID="HyperLink_resource" SkinID="Editicon"
                                        ToolTip="Edit" runat="server" NavigateUrl='<%# "HealthCheckForm.aspx?R=Y&type=resource&HealthCheckId=" + Eval("ID").ToString()+"&PID="+Eval("HealthCheckListID").ToString()%>' />
                                    <asp:HyperLink ID="HyperLink_health" SkinID="Editicon"
                                        ToolTip="Edit" runat="server" NavigateUrl='<%# "HealthCheckForm.aspx?R=Y&type=vendor&HealthCheckId=" + Eval("ID").ToString()+"&PID="+Eval("HealthCheckListID").ToString()%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Report" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink2" SkinID="LinkPrint" ToolTip="Report"
                                        runat="server" NavigateUrl='<%#"Reports/HealthCheckItems.aspx?healthCheckID="+ Eval("ID").ToString()%>'
                                        Target="_blank" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date" SortExpression="DateRaised" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Literal ID="litDateRaised" Text='<%#Eval("DateRaised","{0:d}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Literal ID="litTimeRaised" Text='<%#Eval("DateRaised","{0:t}")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location" ItemStyle-HorizontalAlign="Center" SortExpression="LocationName">
                                <ItemTemplate>
                                    <asp:Literal ID="litLocation" Text='<%#Eval("LocationName")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Health Check" ItemStyle-HorizontalAlign="Center" SortExpression="HealthCheckTitle" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="200px">
                                <ItemTemplate>
                                    <asp:Literal ID="litHealthCheckTitle" Text='<%#Eval("HealthCheckTitle")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer" SortExpression="PortfolioName">
                                <ItemTemplate>
                                    <asp:Label ID="lblPortfolioName" runat="server" Text='<%#Eval("PortfolioName")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned Team" SortExpression="AssignedTeamName">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssignedTeam" runat="server" Text='<%#Eval("AssignedTeamName")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Overall RAG" SortExpression="OverallRAG">
                                <ItemTemplate>
                                    <asp:Label ID="lblOverallRag" runat="server" Text='<%# GetRAGStatus(Eval("RAG"))%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-BorderWidth="0px">
                                <ItemTemplate>
                                    <asp:Label ID="imgPopUp" runat="server" SkinID="Mail"></asp:Label>
                                    <asp:Literal ID="litID" runat="server" Text='<%#Eval("HealthCheckListID")%>' Visible="false" />
                                    <asp:Literal ID="litMainID" runat="server" Text='<%#Eval("ID")%>' Visible="false" />
                                    <asp:GridView ID="gridInner" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                                        BackColor="White" HorizontalAlign="Justify" DataKeyNames="ID" Style="display: none" Width="40%">
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Literal ID="litMailID" runat="server" Text='<%#Eval("ID")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMailable" runat="server" />
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
                                            <div style="width: 100%">
                                                No Email Addresses
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>"
                                        SelectCommand="SELECT Distinct Mail.ID AS ID,Mail.Name AS Name,Mail.EmailID AS EmailID,ISNULL(HCL_Mail.MailID,0) AS Assigned 
                                    FROM healthchecknamemailid Mail Left Outer Join healthchecklist_healthchecknamemailid HCL_Mail
                                    ON (Mail.ID=HCL_Mail.MailID) WHERE ([PortfolioHealthCheckID] = @PortfolioHealthCheckID)">
                                        <SelectParameters>
                                            <%--<asp:ControlParameter ControlID="lblHealthCheckID" Name="HealthCheckID" PropertyName="Text"
                                            Type="Int32" />--%>
                                            <asp:ControlParameter ControlID="lblHealthCheckID" Name="PortfolioHealthCheckID"
                                                PropertyName="Text" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <ajaxtoolkit:hovermenuextender id="hme2" runat="Server" popupcontrolid="gridInner"
                                        popupposition="Left" targetcontrolid="imgPopUp" popdelay="25" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkMail" runat="server" Text="Send Mail" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" OnClientClick='return confirm("Do you really want to delete");'
                                        CommandName="Delete" SkinID="BtnLinkDelete" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="clr">
                    </div>
                    <asp:Label ID="lblErrorMessage" runat="server" EnableViewState="false" ForeColor="Red" />
                    <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
                    <asp:ObjectDataSource ID="objPortfolio" runat="server" TypeName="Deffinity.Bindings.DefaultDatabind"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="b_Portfolio_Byuser">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="section" DefaultValue="" QueryStringField="type" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="objGridFiller" runat="server" TypeName="Health.DAL.HealthCheckListHelper"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="LoadAllHealthCheckLists_byUser"
                        DataObjectTypeName="Health.Entity.HealthCheckList" DeleteMethod="Delete" SortParameterName="sortExpression">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="section" DefaultValue="" QueryStringField="type" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>
                        </div>
         </div>
                 <div class="form-group pull-right">
          <div class="col-md-12">
                    <asp:Button ID="btnUpdate" runat="server" SkinID="ImgUpdate" OnClick="btnUpdate_Click"
                        Visible="false" />
                </div>
                     </div>
   <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(NestedGridResponsiveCss);
    NestedGridResponsiveCss();
 </script> 
    <script  type="text/javascript">
        $(document).ready(function () {

            sideMenuActive('<%= Resources.DeffinityRes.HealthChecks%>');
        });
       </script>
</asp:Content>
