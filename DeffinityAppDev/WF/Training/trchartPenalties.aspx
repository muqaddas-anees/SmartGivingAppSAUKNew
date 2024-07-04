<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_trchartPenalties" Codebehind="trchartPenalties.aspx.cs" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>

<%@ Register src="controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>
<%@ Register Src="controls/dropdownView.ascx" TagName="DropDownList" TagPrefix="uc2" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebChart" tagprefix="igchart" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Resources.Appearance" tagprefix="igchartprop" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Data" tagprefix="igchartdata" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebChart" tagprefix="igchart" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebChart" tagprefix="igchart" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebChart" tagprefix="igchart" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Tabs" runat="Server">
    <uc1:TrainingTabs ID="TrainingTabs2" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="page_title" runat="Server">
    <%= Resources.DeffinityRes.TrainingMgmt%>
 </asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="panel_title" runat="Server">
    Dashboard - <label id="lblTitle" runat="server"></label><br />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="panel_options" Runat="Server">
  <uc2:DropDownList ID="dropDownList1" runat="server" /></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

      <div  class="form-group">
       <div class="col-xs-6">
             <label id="lblDepartment" runat="server" class="col-sm-3 control-label"><%= Resources.DeffinityRes.Department%></label>
             <div class="col-sm-9">
                     <asp:DropDownList ID="ddlDepartment" runat="server" SkinID="ddl_70"
                                 AutoPostBack="True" onselectedindexchanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
             </div>
       </div>
           <div class="col-xs-6">
             <label id="Label1" runat="server" class="col-sm-3 control-label"><%= Resources.DeffinityRes.Area%></label>
             <div class="col-sm-9">
                 <asp:DropDownList ID="ddlArea" runat="server" Width="150px" SkinID="ddl_70"></asp:DropDownList>
             </div>
       </div>
     </div>
     <div  class="form-group">
       <div class="col-xs-6 form-inline">
             <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.FromDate%></label>
             <div class="col-sm-9 form-inline">
                    <asp:TextBox ID="txtFromDate" SkinID="Date" runat="server"></asp:TextBox>
                <asp:Label ID="imgFromDate" runat="server" SkinID="Calender"></asp:Label>
                <ajaxToolkit:CalendarExtender ID="cldExtender1"   CssClass="MyCalendar"
                TargetControlID="txtFromDate" PopupButtonID="imgFromDate" runat="server">
                </ajaxToolkit:CalendarExtender>
                    <asp:RegularExpressionValidator ID="REV1" runat="server" ErrorMessage="Please enter valid from date"
                    ControlToValidate="txtFromDate" ValidationGroup="Group4" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" Display="None">
                    </asp:RegularExpressionValidator >
             </div>
       </div>
           <div class="col-xs-6 form-inline">
             <label class="col-sm-3 control-label"><%= Resources.DeffinityRes.ToDate%></label>
             <div class="col-sm-9">
                 <asp:TextBox ID="txtToDate" SkinID="Date" runat="server"></asp:TextBox>
                <asp:Label ID="imgToDate" runat="server" SkinID="Calender"></asp:Label>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1"   CssClass="MyCalendar"
                TargetControlID="txtToDate" PopupButtonID="imgToDate" runat="server"></ajaxToolkit:CalendarExtender>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Please enter valid to date"
                    ControlToValidate="txtToDate" ValidationGroup="Group4" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" Display="None">
                    </asp:RegularExpressionValidator>
              <asp:CompareValidator ID="CompareValidator4" runat="server" Display="None" ControlToCompare="txtFromDate"
                ErrorMessage="To date should be greater than from date" Type="Date" ControlToValidate="txtToDate"
                   Operator="GreaterThan" ValidationGroup="Group4"></asp:CompareValidator>
                   <asp:Button ID="btnView" runat="server" SkinID="btnDefault" Text="View" ValidationGroup="Group4" onclick="btnView_Click" />
             </div>
       </div>
     </div>
      <div  class="form-group">
            <asp:ValidationSummary ID="ValidationSummaryFilter" runat="server" ValidationGroup="Group4" ShowSummary="true" />
                <asp:Label ID="lblException" runat="server" ></asp:Label>
      </div>
       <div  class="form-group">
    <igchart:UltraChart ID="UltraChart1" runat="server" Version="7.2" Width="750px" 
        Height="400px" BorderColor="#E0E0E0" BorderWidth="0px">
<Axis>
<PE ElementType="None" Fill="Cornsilk"></PE>

<X Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" 
        FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" 
        Orientation="VerticalLeftFacing">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Center" 
        VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</X>

<Y Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="20">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" 
        FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" 
        Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" 
        VerticalAlign="Center" Orientation="VerticalLeftFacing" FormatString="">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Y>

<Y2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="20">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" FontColor="Gray" 
        HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
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

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" 
        FontColor="Gray" HorizontalAlign="Far" VerticalAlign="Center" 
        Orientation="VerticalLeftFacing">
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
<TitleTop FontColor="Gray" FontSizeBestFit="True">
                </TitleTop>
        <Border Color="224,224,224" Raised="true" CornerRadius="10" Thickness="0" />

<Effects><Effects>
<igchartprop:GradientEffect></igchartprop:GradientEffect>
</Effects>
</Effects>

        <Legend BackgroundColor="White" BorderThickness="0" SpanPercentage="15" 
            Visible="True" FontColor="Gray" Font="Verdana, 7pt" Location="Bottom">
           <Margins Bottom="0" Left="150" Right="10" Top="10" />
            </Legend>
        <Data ZeroAligned="True">
        </Data>

<ColorModel ModelStyle="CustomLinear" AlphaLevel="150"></ColorModel>
    </igchart:UltraChart>
           </div>
</asp:Content>


