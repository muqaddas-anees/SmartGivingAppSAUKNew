<%@ Page Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="CustomerProjects"  EnableEventValidation="false" Codebehind="FMProjects.aspx.cs" %>

<%@ Register Src="controls/FinanceModuleTab.ascx" TagName="FMTab" TagPrefix="uc2" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebChart" tagprefix="igchart" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Resources.Appearance" tagprefix="igchartprop" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Data" tagprefix="igchartdata" %>

<asp:Content ID="Content3" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.FinanceSection%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="page_description" runat="Server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="panel_title" runat="Server">
     <%= Resources.DeffinityRes.Projects%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_options" runat="Server">
    <asp:Button ID="BtnDownload" runat="server" Text="Download Expense Template" CausesValidation="false" OnClick="BtnDownload_Click" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" runat="Server">
    <uc2:FMTab ID="fmID" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <asp:Panel ID="Panel_fileupdload" runat="server" Width="100%">
                <div class="btn_align_right" style="width: 100%">
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                        TargetControlID="PanelCsv" ExpandControlID="PnlTitle" CollapseControlID="PnlTitle"
                        TextLabelID="Lbl1" CollapsedText=" Upload Project External Expenses " ExpandedText=" Upload Project External Expenses "
                        ImageControlID="UploadImg"
                        Collapsed="true" SuppressPostBack="true">
                    </ajaxToolkit:CollapsiblePanelExtender>
                    <asp:Panel Width="100%" ID="PnlTitle1" runat="server" HorizontalAlign="Right">
                        <div id="PnlTitle" runat="server">
                           
                            <asp:Label ID="Lbl1" runat="server" Text=" Upload Project External Expenses " Font-Bold="true"
                                ForeColor="Black" Style="cursor:pointer;"></asp:Label><asp:Label runat="server" ID="UploadImg" SkinID="Add"
                                     Style="cursor: pointer;" /></div>
                    </asp:Panel>
                </div>
                <asp:Panel ID="PanelCsv" runat="server" Width="100%" Style="overflow: hidden">

                    <div class="form-group">
                        <div class="col-md-12">
                            <strong> <%= Resources.DeffinityRes.UploadProjectExternalExpenses%>   </strong> 
                            <hr class="no-top-margin" />
                        </div>
                    </div>
                    <div class="form-group">
                         <div class="col-md-12">
                        <iframe id="iframe1" height="100px" width="100%" src='<%=RetUrl() %>' scrolling="no"
                            frameborder="0"></iframe>
                             </div>
                    </div>
                </asp:Panel>
            </asp:Panel>

    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Customer%>  </label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlCustomer" runat="server" SkinID="ddl_90">
                                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Site%>     </label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlSite" runat="server" SkinID="ddl_90">
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="casCadSite" runat="server" TargetControlID="ddlSite"
                                    Category="Task" PromptText="Please select..." ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                    ServiceMethod="GetSites" ParentControlID="ddlCustomer" />
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Status%>      </label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlStatus" runat="server" SkinID="ddl_90">
                                </asp:DropDownList>
            </div>
	</div>
</div>

    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.ProjectOwner%> </label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlPOwner" runat="server" SkinID="ddl_90">
                                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.Programme%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlTask" runat="server" SkinID="ddl_90" Visible="false">
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="casCadTasks" runat="server" TargetControlID="ddlTask"
                                    Category="Task" PromptText="Please select..." ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                    ServiceMethod="GetProjectsTasks" ParentControlID="ddlProject" />
                                <asp:DropDownList ID="ddlProgramme" runat="server" Width="150px">
                                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.SubProgramme%>  </label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlSubprogramme" runat="server" SkinID="ddl_90">
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="CascadingDropDown21" runat="server" TargetControlID="ddlSubprogramme"
                                    Category="Title" PromptText="Please select..." ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                    ServiceMethod="GetSubProgramme" ParentControlID="ddlProgramme" />
            </div>
	</div>
</div>

    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.FromDate%>  </label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtFromDate" runat="server" SkinID="Date"></asp:TextBox>
                                <asp:Label ID="imgbtnenddate" runat="server" SkinID="Calender" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender"  runat="server"
                                    PopupButtonID="imgbtnenddate" TargetControlID="txtFromDate" CssClass="MyCalendar">
                                </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"> <%= Resources.DeffinityRes.ToDate%>    </label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtToDate" runat="server" SkinID="Date"></asp:TextBox>
                                <asp:Label ID="imgbtnenddate1" runat="server" SkinID="Calender" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server"
                                    PopupButtonID="imgbtnenddate1" TargetControlID="txtToDate" CssClass="MyCalendar">
                                </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.Project%>  </label>
           <div class="col-sm-8">
                <asp:DropDownList ID="ddlProject" runat="server" SkinID="ddl_90">
                                </asp:DropDownList>
                                <ajaxToolkit:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlProject"
                                    Category="Title" PromptText="Please select..." ServicePath="~/WF/DC/webservices/ServiceMgr.asmx"
                                    ServiceMethod="GetAllProjectRef" ParentControlID="ddlCustomer" />
            </div>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.PO%></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlPO" runat="server" Width="150px">
                                </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">

            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">
               <asp:Button ID="ImageButton1" runat="server" onclick="ImageButton1_Click" 
                                    SkinID="btnDefault" Text="View" />
              
            </div>
	</div>
</div>
    <asp:GridView ID="grdProjets" runat="server" AutoGenerateColumns="false"
             Width="100%" EmptyDataText="No Records Found" AllowPaging="True" 
                 PageSize="15" onpageindexchanging="grdProjets_PageIndexChanging" >
             <Columns>
             <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,ProjectRef%>"
HeaderStyle-CssClass="header_bg_l">
             <ItemTemplate>
                 <%--<asp:Label ID="lblProjectReferenceWithPrefix" runat="server" Text="<%#Bind('ProjectReferenceWithPrefix') %>" Visible="true"></asp:Label>--%>
               <a href="ProjectOverviewV4.aspx?project=<%# DataBinder.Eval(Container.DataItem, "ProjectReference")%>">  <%# DataBinder.Eval(Container.DataItem, "ProjectReferenceWithPrefix")%></a>
             </ItemTemplate>
             </asp:TemplateField>
                 
                  <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,Title%>" ItemStyle-CssClass="col-nowrap" ItemStyle-Width="200px" ControlStyle-Width="200px">
                  <ItemStyle Width="200px" />
             <ItemTemplate>
                 <asp:Label ID="lblProjectTitle" runat="server" Text='<%#Bind("ProjectTitle") %>' Visible="true"></asp:Label>
               
             </ItemTemplate>
             </asp:TemplateField>
                 <%--<asp:BoundField HeaderText="Title" DataField="ProjectTitle" ItemStyle-Width="200px" />--%>
                 <%-- <asp:TemplateField HeaderText="Customer">
             <ItemTemplate>
                 <asp:Label ID="lblCustomer" runat="server" Text="<%#Bind('PortfolioName') %>"></asp:Label>
             </ItemTemplate>
             </asp:TemplateField>--%>
                 
             <asp:BoundField HeaderText="<%$Resources:DeffinityRes,PONumber%>"  DataField="PONumber"/>
              <asp:BoundField HeaderText="<%$Resources:DeffinityRes,Budget%>" DataField="BudgetaryCost" 
              ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                <asp:BoundField HeaderText="<%$Resources:DeffinityRes,BudgetCost%>" DataField="AccrualsPriorFinancial" 
              ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
               <asp:BoundField HeaderText="<%$Resources:DeffinityRes,Variations%>" DataField="GrossProfit"
                  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />
                   <asp:BoundField HeaderText="<%$Resources:DeffinityRes,PredictedGP%> " DataField="ProjectForecast"
                  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />
                 <asp:BoundField HeaderText="<%$Resources:DeffinityRes,ActualGPtoDate%>" DataField="ProjectCostForecast"
                  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" />
                 
                 
                   <asp:TemplateField HeaderText="<%$Resources:DeffinityRes,BOMTotal%>"  ItemStyle-HorizontalAlign="Right" >
                  <ItemStyle Width="75px" />
             <ItemTemplate>
                 
               <a href="ProjectBOM.aspx?project=<%# DataBinder.Eval(Container.DataItem, "ProjectReference")%>"><asp:Label ID="lblProjectTitle" runat="server" Text="<%#Bind('BudgetaryCostLevel3','{0:N2}') %>" Visible="true"></asp:Label>  </a>
             </ItemTemplate>
             </asp:TemplateField>
                 
                <%-- <asp:BoundField HeaderText="BOM Total" DataField="BudgetaryCostLevel3"
                    DataFormatString="{0:N2}" />--%>
                  <asp:BoundField HeaderText="<%$Resources:DeffinityRes,BOMReceived%> " DataField="BuyingPrice"  DataFormatString="{0:N2}"  ItemStyle-HorizontalAlign="Right"/>
                  <asp:BoundField HeaderText="<%$Resources:DeffinityRes,InvoicedToDate%>" DataField="ActualCost"  DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                   <asp:BoundField HeaderText="<%$Resources:DeffinityRes,POValue%>"  DataField="AccrualsCurrentFinancial"  DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" 
                   />
                    
             </Columns>
             
             </asp:GridView>
    <asp:Label ID="lblCount" runat="server" Text="" Visible="false"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblValue" runat="server" Text="" Visible="false"></asp:Label>
    <div class="form-group">
      <div class="col-md-6">
           &nbsp;<igchart:UltraChart ID="UltraChart1" runat="server" 
                     Version="7.2">
<Axis>
<PE ElementType="None" Fill="Cornsilk"></PE>

<X Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Center" 
        VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</X>

<Y Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="100" 
        Extent="45">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" 
        VerticalAlign="Center" Orientation="VerticalLeftFacing" FormatString="">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Y>

<Y2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="100">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
        VerticalAlign="Center" Orientation="VerticalLeftFacing" FormatString="">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Y2>

<X2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Center" 
        VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</X2>

<Z Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" 
        VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Z>

<Z2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
        VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Z2>
</Axis>

<Effects><Effects>
   
</Effects>
</Effects>

                     <TitleTop Font="Microsoft Sans Serif, 8pt, style=Bold" HorizontalAlign="Center" 
                         Text="Budget  vs  Actual ">
                     </TitleTop>

                     <Legend Location="Bottom" Visible="True" DataAssociation="LineData"></Legend>
                     <ColorModel ModelStyle="CustomLinear" AlphaLevel="150"></ColorModel>
                 </igchart:UltraChart>

	</div>
	<div class="col-md-6">
          <igchart:UltraChart ID="UltraChart2" runat="server" Version="7.2">
<Axis>
<PE ElementType="None" Fill="Cornsilk"></PE>

<X Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</X>

<Y Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="50">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Y>

<Y2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="50">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Y2>

<X2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</X2>

<Z Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Z>

<Z2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Z2>
</Axis>

                             <TitleTop Font="Microsoft Sans Serif, 8pt, style=Bold" HorizontalAlign="Center" 
                                 Text="Invoiced  vs Outstanding">
                             </TitleTop>

<Effects><Effects>
    
</Effects>
</Effects>

                             <Legend Location="Bottom" Visible="True"></Legend>
                             <Data ZeroAligned="True"></Data><ColorModel ModelStyle="CustomLinear" AlphaLevel="150"></ColorModel>
                         </igchart:UltraChart>
	</div>
</div>
   
      <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script>  
</asp:Content>
