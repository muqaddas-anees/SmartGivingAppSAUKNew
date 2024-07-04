<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainFrame.Master" AutoEventWireup="true" Inherits="ChangeControlDashBoardCharts" EnableEventValidation="false" Codebehind="ChangeControlDashBoardCharts.aspx.cs" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebChart" tagprefix="igchart" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Resources.Appearance" tagprefix="igchartprop" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Data" tagprefix="igchartdata" %>

<%@ Register src="controls/ChangeControlPieCharts.ascx" tagname="ChangeControlPieCharts" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:Panel ID="pnlFilter" runat="server" >
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.StartDate%></label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtFromDate" runat="server" SkinID="Date" MaxLength="10">
                        </asp:TextBox>
                        <asp:Label ID="Image2" runat="server" SkinID="Calender" />
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><%= Resources.DeffinityRes.EndDate%></label>
           <div class="col-sm-8 form-inline">
                <asp:TextBox ID="txtEndDate" runat="server" SkinID="Date" MaxLength="10" />
                        <asp:Label ID="Image3" runat="server" SkinID="Calender"  />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
                                    CssClass="MyCalendar"  PopupButtonID="Image3" 
                                    TargetControlID="txtEndDate">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                    CssClass="MyCalendar"  PopupButtonID="Image2" 
                                    TargetControlID="txtFromDate">
                                </ajaxToolkit:CalendarExtender>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><asp:Label ID="lblArea" runat="server" Text="Area"></asp:Label></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlArea" runat="server" SkinID="ddl_90"></asp:DropDownList>
            </div>
	</div>
</div>
    <div class="form-group">
      <div class="col-md-4">
           <label class="col-sm-4 control-label"><asp:Label ID="lblCategory" runat="server"  Visible="true" Text="Category"></asp:Label></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlCategory" runat="server" SkinID="ddl_90">
                        </asp:DropDownList>
            </div>
	</div>
	<div class="col-md-4">
           <label class="col-sm-4 control-label"><asp:Label ID="lblSubCategory" runat="server"  Visible="true" Text="Sub Category"></asp:Label></label>
           <div class="col-sm-8">
               <asp:DropDownList ID="ddlSubCategory" runat="server"
                             Width="175px">
                        </asp:DropDownList><ajaxToolkit:CascadingDropDown ID="casCadSubCategory" runat="server" TargetControlID="ddlSubCategory"
                        Category="Task1" PromptText="Please select..." PromptValue="0" ServicePath="~/WF/DC/ServiceMgr.asmx"
                        ServiceMethod="GetProjectSubCategory" ParentControlID="ddlCategory" />
            </div>
	</div>
	<div class="col-md-4">
             <label class="col-sm-4 control-label"></label>
           <div class="col-sm-8">
                <asp:Button ID="imgViewChart" runat="server" Text="View Chart"
                                        SkinID="btnDefault" onclick="imgViewChart_Click" />
            </div>
	</div>
</div>
 
 </asp:Panel>

<asp:Panel ID="pnl_byCategory" runat="server">
    <div class="form-group">
        <div class="col-md-12">
           <strong> Volume&nbsp;of&nbsp;Completed &nbsp;Requests&nbsp;by&nbsp;Category </strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
 <div>
<igchart:UltraChart ID="chart_bycategory" runat="server" BackgroundImageFileName="" 
            BorderColor="Gray" BorderWidth="1px" ChartType="StackColumnChart" 
            EmptyChartText="" 
            Height="500px" Width="1000px" Version="7.2" 
         onchartdrawitem="chrtCS_ChartDrawItem" >
<Axis>
<PE ElementType="None" Fill="Cornsilk"></PE>

<X Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0" 
        TickmarkIntervalType="Ticks">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</X>

<Y Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="200">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" 
        VerticalAlign="Center" Orientation="Horizontal" formatstring="">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Y>

<Y2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="200">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
        VerticalAlign="Center" Orientation="Horizontal" formatstring="">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Y2>

<X2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" 
        VerticalAlign="Center" Orientation="VerticalLeftFacing">
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
            <Border CornerRadius="10" Color="Gray" Thickness="1" />

            <ColumnChart ColumnSpacing="1">
             <ChartText>
                        <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="-2" 
                            ItemFormatString="&lt;DATA_VALUE_ITEM&gt;" Row="-2" Visible="True" />
                    </ChartText>
            </ColumnChart>
            <Legend Location="Bottom" Visible="True" BorderColor="Gray">
            </Legend>

<ColorModel ModelStyle="CustomLinear" AlphaLevel="150"></ColorModel>
        </igchart:UltraChart>
 </div>
</asp:Panel>

<asp:Panel ID="pnl_byYear" runat="server">
    <div class="form-group">
        <div class="col-md-12">
           <strong>Volume&nbsp;of&nbsp;Completed &nbsp;Requests&nbsp;by&nbsp;Category&nbsp;Year&nbsp;On&nbsp;Year</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>

 <div>

 
<igchart:UltraChart ID="chart_byYear" runat="server" BackgroundImageFileName="" 
            BorderColor="Gray" BorderWidth="1px" 
            EmptyChartText="" 
            Height="500px" Width="1000px" Version="7.2" 
         onchartdrawitem="chrtCS_ChartDrawItem" >
<Axis>
<PE ElementType="None" Fill="Cornsilk"></PE>

<X Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0" 
        TickmarkIntervalType="Ticks">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</X>

<Y Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="20">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" 
        VerticalAlign="Center" Orientation="VerticalLeftFacing" formatstring="">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Y>

<Y2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="20">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
        VerticalAlign="Center" Orientation="VerticalLeftFacing" formatstring="">
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
            <Border CornerRadius="10" Color="Gray" Thickness="1" />

            <ColumnChart ColumnSpacing="1">
             <ChartText>
                        <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="-2" 
                            ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="-2" />
                    </ChartText>
            </ColumnChart>
            <Legend Location="Bottom" Visible="True" BorderColor="Gray">
            </Legend>

<ColorModel ModelStyle="CustomLinear" AlphaLevel="150"></ColorModel>
        </igchart:UltraChart>
 
 
 </div>
</asp:Panel>

<asp:Panel ID="pnl_chartByAreaCategory" runat="server" Visible="false">
    <div class="form-group">
        <div class="col-md-12">
           <strong>Volume&nbsp;of&nbsp;Requests&nbsp;by&nbsp;Area&nbsp;Stacked&nbsp;by&nbsp;Category</strong> 
            <hr class="no-top-margin" />
            </div>
    </div>
 <div>
<igchart:UltraChart ID="chart_byArea_category" runat="server" BackgroundImageFileName="" 
            BorderColor="Gray" BorderWidth="1px" 
            EmptyChartText="" 
            Height="500px" Width="1000px" Version="7.2" 
         onchartdrawitem="chrtCS_ChartDrawItem" ChartType="StackColumnChart" >
<Axis>
<PE ElementType="None" Fill="Cornsilk"></PE>

<X Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0" 
        TickmarkIntervalType="Ticks">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</X>

<Y Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="200">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" 
        VerticalAlign="Center" Orientation="Horizontal" formatstring="">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Y>

<Y2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="200">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" 
        VerticalAlign="Center" Orientation="Horizontal" formatstring="">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Y2>

<X2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" 
        VerticalAlign="Center" Orientation="VerticalLeftFacing">
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
            <Border CornerRadius="10" Color="Gray" />

            <ColumnChart ColumnSpacing="1">
             <ChartText>
                        <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="-2" 
                            ItemFormatString="&lt;DATA_VALUE_ITEM&gt;" Row="-2" Visible="True" />
                    </ChartText>
            </ColumnChart>
            <Legend Location="Bottom" Visible="True" BorderColor="Gray">
            </Legend>

<ColorModel ModelStyle="CustomLinear" AlphaLevel="150"></ColorModel>
        </igchart:UltraChart>
 </div>

 </asp:Panel>
 <asp:Panel ID="pnl_pieCharts" runat="server" Visible="false">
     <uc1:ChangeControlPieCharts ID="ChangeControlPieCharts1" runat="server" />
   <div class="form-group">
       <div class="col-md-6">
           <label class="col-sm-3 control-label">Status:</label>
           <div class="col-sm-9">
                <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlstatus_SelectedIndexChanged" SkinID="ddl_150px">
                                    <asp:ListItem Value="All">All</asp:ListItem>
                                     <asp:ListItem Value="New">New</asp:ListItem>
                                    <asp:ListItem Value="In Hand">In Hand</asp:ListItem>
                                    <asp:ListItem Value="Closed">Closed</asp:ListItem>
                                </asp:DropDownList>
            </div>
	</div>
         
</div>
             <asp:Panel ID="pnlChangeControl" runat="server">
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
                            <asp:GridView ID="gridChangeControl" runat="server" 
                        DataSourceID="gridFiller" AutoGenerateColumns="False"
                                OnRowCommand="gridChangeControl_RowCommand"  AllowPaging="True"
                                OnRowCreated="gridChangeControl_RowCreated" 
                        EmptyDataText="No change controls exist." 
                        EnableModelValidation="True" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Reference" SortExpression="ID">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnChangeControl" runat="server" Text='<%#"Change:"+Eval("ID") %>'
                                                CommandName="Change" CommandArgument='<%#Eval("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                    <asp:BoundField DataField="Portfolioname" HeaderText="Customer" SortExpression="Customer" Visible="false" />
                                    <asp:BoundField DataField="ChangeDescription" HeaderText="Description" SortExpression="ChangeDescription" />
                                    <asp:BoundField DataField="Justification" HeaderText="Justification" SortExpression="Justification" />
                                     <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                    <asp:TemplateField HeaderText="Date Raised" SortExpression="DateRaised">
                                        <ItemTemplate>
                                            <asp:Literal ID="litDateRaised" runat="server" Text='<%#Eval("DateRaised","{0:d}")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Release Date" SortExpression="TargetReleaseDate">
                                        <ItemTemplate>
                                            <asp:Literal ID="litReleaseDate" runat="server" Text='<%#Eval("TargetReleaseDate","{0:d}")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="ResourceImpact" HeaderText="Resource Impact" SortExpression="ResourceImpact"> </asp:BoundField>
                                    <asp:BoundField DataField="CoOrdinatorName" HeaderText="Co-ordinator" HeaderStyle-CssClass="header_bg_r" >
                                    </asp:BoundField>
                                     
                                </Columns>
                            </asp:GridView>
                       
                    <asp:ObjectDataSource ID="gridFiller" runat="server" TypeName="DeffinityManager.DAL.DBChangeControlTableAdapters.dtChangeControlTableAdapter"
                        OldValuesParameterFormatString="original_{0}" 
                        SelectMethod="GetChangeControlByStatus"  >
                        <SelectParameters>
                        <asp:SessionParameter Name="PortfolioID" SessionField="PortfolioID" Type="Int32" DefaultValue="0" /> 
                        <asp:ControlParameter ControlID="ddlstatus" DefaultValue="All" Name="status" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>
 </asp:Panel>

    <%: System.Web.Optimization.Scripts.Render("~/bundles/grid") %>

<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(GridResponsiveCss);
   GridResponsiveCss();
</script> 
</asp:Content>

